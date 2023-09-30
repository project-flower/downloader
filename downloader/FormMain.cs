using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace downloader
{
    public partial class FormMain : Form
    {
        #region Public Methods

        public FormMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
            saveFileDialog.InitialDirectory = Application.StartupPath;
        }

        #endregion

        #region Private Fields

        private readonly FormAddBatch formAddBatch = new FormAddBatch();

        #endregion

        #region Private Methods

        private int AddItem(string fileName, string address)
        {
            var item = new ListViewItem(new string[]
            {
                Path.GetFileName(fileName), address, string.Empty
            });

            item.Tag = new WebClientEx(fileName, item, webClient_DownloadFileCompleted, webClient_DownloadProgressChanged);
            item.UseItemStyleForSubItems = false;
            item.SubItems[columnHeaderProgress.DisplayIndex].ForeColor = Color.Blue;
            listViewUrls.Items.Add(item);
            return listViewUrls.Items.Count;
        }

        private void BeginDownload()
        {
            foreach (ListViewItem item in listViewUrls.Items)
            {
                var webClient = item.Tag as WebClientEx;

                if (webClient.Completed)
                {
                    continue;
                }

                if (webClient.IsBusy)
                {
                    return;
                }

                webClient.DownloadFileAsync(new Uri(item.SubItems[columnHeaderUrl.Index].Text), webClient.SaveFileName);
                break;
            }
        }

        private void DisposeWebClient(ListViewItem listViewItem)
        {
            var webClient = listViewItem.Tag as WebClientEx;

            if (webClient == null)
            {
                return;
            }

            webClient.Dispose();
            listViewItem.Tag = null;
        }

        private void Explore()
        {
            ListView.SelectedIndexCollection indices = listViewUrls.SelectedIndices;

            if (indices.Count < 1)
            {
                return;
            }

            try
            {
                string saveFileName = (listViewUrls.Items[indices[0]].Tag as WebClientEx).SaveFileName;

                if (!(File.Exists(saveFileName)))
                {
                    return;
                }

                Process.Start("explorer.exe", string.Format("/select,\"{0}\"", saveFileName));
            }
            catch
            {
            }
        }

        private ListViewItem.ListViewSubItem GetSubItem(ListViewItem listViewItem, ColumnHeader header)
        {
            return listViewItem.SubItems[header.DisplayIndex];
        }

        private void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private DialogResult ShowMessage(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(this, text, Text, buttons, icon);
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var webClient = sender as WebClientEx;
            ListViewItem item = webClient.ListViewItem;
            listViewUrls.BeginUpdate();
            ListViewItem.ListViewSubItem subItem = GetSubItem(item, columnHeaderProgress);
            Color foreColor;
            string description;

            if (e.Cancelled)
            {
                foreColor = Color.Gray;
                description = "Cancelled";
            }
            else if (e.Error != null)
            {
                foreColor = Color.Red;
                description = e.Error.Message;
            }
            else
            {
                foreColor = Color.Black;
                description = "Completed";
            }

            subItem.ForeColor = foreColor;
            subItem.Text = description;
            listViewUrls.EndUpdate();
            BeginDownload();
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var webClient = sender as WebClientEx;
            ListViewItem item = webClient.ListViewItem;
            listViewUrls.BeginUpdate();
            ListViewItem.ListViewSubItem subItem = GetSubItem(item, columnHeaderProgress);
            subItem.Text = string.Format("{0}%[{1}/{2}]bytes", e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);
            listViewUrls.EndUpdate();
        }

        #endregion

        // Designer's Methods

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                var uri = new Uri(comboBoxUrl.Text);
                string[] segments = uri.Segments;
                string fileName;

                if (segments.Length > 0)
                {
                    fileName = segments[segments.Length - 1];
                }
                else
                {
                    fileName = string.Empty;
                }

                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBatch_Click(object sender, EventArgs e)
        {
            string[] items;

            do
            {
                if (formAddBatch.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    items = formAddBatch.GetValues();
                    break;
                }
                catch (Exception exception)
                {
                    ShowErrorMessage(exception.Message);
                }
            }
            while (true);

            string message = items[0];
            string newLine = Environment.NewLine;

            if (items.Length > 1)
            {
                message += $"{newLine}...{newLine}{items[items.Length - 1]}";
            }

            if (ShowMessage($"以下のファイルを追加します。よろしいですか？{newLine}{newLine}{message}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            listViewUrls.BeginUpdate();

            try
            {
                foreach (string item in items)
                {
                    Uri uri;

                    try
                    {
                        uri = new Uri(item);
                    }
                    catch (Exception exception)
                    {
                        ShowErrorMessage(exception.Message);
                        return;
                    }

                    string[] segments = uri.Segments;
                    AddItem(Path.Combine(folderBrowserDialog.SelectedPath, segments[segments.Length - 1]), item);
                }
            }
            finally
            {
                listViewUrls.EndUpdate();
            }

            BeginDownload();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = listViewUrls.SelectedItems;

            foreach (ListViewItem item in selectedItems)
            {
                var webClient = item.Tag as WebClientEx;

                if (webClient == null)
                {
                    continue;
                }

                if (!webClient.IsBusy)
                {
                    continue;
                }

                webClient.CancelAsync();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = listViewUrls.SelectedIndices;
            var selectedItems = new List<ListViewItem>(selectedIndices.Count);

            foreach (int index in selectedIndices)
            {
                selectedItems.Add(listViewUrls.Items[index]);
            }

            bool abort = false;

            foreach (ListViewItem item in selectedItems)
            {
                var webClient = item.Tag as WebClientEx;

                if (webClient == null)
                {
                    continue;
                }

                if (webClient.IsBusy)
                {
                    if (!abort && (ShowMessage("ダウンロード中です。中止しますか？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes))
                    {
                        return;
                    }

                    abort = true;

                    try
                    {
                        webClient.CancelAsync();
                    }
                    catch
                    {
                    }
                }

                DisposeWebClient(item);
            }

            listViewUrls.BeginUpdate();

            foreach (ListViewItem item in selectedItems)
            {
                listViewUrls.Items.Remove(item);
            }

            listViewUrls.EndUpdate();
        }

        private void buttonShow_Click(object sender, System.EventArgs e)
        {
            Explore();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ListViewItem item in listViewUrls.Items)
            {
                try
                {
                    (item.Tag as WebClientEx).Dispose();
                }
                catch
                {
                }
            }
        }

        private void listViewUrls_DoubleClick(object sender, EventArgs e)
        {
            Explore();
        }

        private void toolStripMenuItemCopyUrl_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listViewUrls.SelectedIndices;

            if (indices.Count < 1)
            {
                return;
            }

            try
            {
                Clipboard.SetText(GetSubItem(listViewUrls.Items[indices[0]], columnHeaderUrl).Text);
            }
            catch
            {
            }
        }

        private void toolStripMenuItemShowFile_Click(object sender, EventArgs e)
        {
            Explore();
        }
    }
}
