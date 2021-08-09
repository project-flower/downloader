using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace downloader
{
    public partial class FormMain : Form
    {
        #region Private Classes

        private class WebClientEx : WebClient
        {
            public int Index
            {
                get;
                set;
            }
        }

        #endregion

        #region Public Methods

        public FormMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
            saveFileDialog.InitialDirectory = Application.StartupPath;
        }

        #endregion

        #region Private Fields

        private readonly List<ListViewItem> listViewItems = new List<ListViewItem>();
        private readonly List<WebClientEx> webClients = new List<WebClientEx>();

        #endregion

        #region Private Methods

        private int AddItem(FileInfo file, string address)
        {
            var item = new ListViewItem(new string[]
            {
                file.Name, address, string.Empty
            })
            {
                Tag = file.FullName
            };

            item.UseItemStyleForSubItems = false;
            item.SubItems[columnHeaderProgress.DisplayIndex].ForeColor = Color.Blue;
            listViewItems.Add(item);
            ++(listViewUrls.VirtualListSize);
            return (listViewUrls.VirtualListSize - 1);
        }

        private void DisposeWebClient(WebClientEx webClient)
        {
            webClient.DownloadDataCompleted -= webClient_DownloadFileCompleted;
            webClient.DownloadProgressChanged -= webClient_DownloadProgressChanged;
            webClient.Dispose();
            webClient = null;
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
                var file = new FileInfo(listViewItems[indices[0]].Tag.ToString());

                if (!(file.Exists))
                {
                    return;
                }

                Process.Start("explorer.exe", string.Format("/select,\"{0}\"", file.FullName));
            }
            catch
            {
            }
        }

        private ListViewItem.ListViewSubItem GetSubItem(ListViewItem listViewItem, ColumnHeader header)
        {
            return listViewItem.SubItems[header.DisplayIndex];
        }

        private void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var client = sender as WebClientEx;
            DisposeWebClient(client);
            ListViewItem item = listViewItems[client.Index];
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
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var client = sender as WebClientEx;
            ListViewItem item = listViewItems[client.Index];
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

                var client = new WebClientEx();
                client.DownloadFileCompleted += webClient_DownloadFileCompleted;
                client.DownloadProgressChanged += webClient_DownloadProgressChanged;
                var file = new FileInfo(saveFileDialog.FileName);
                saveFileDialog.InitialDirectory = file.DirectoryName;
                client.Index = AddItem(file, uri.AbsoluteUri);
                webClients.Add(client);
                client.DownloadFileAsync(uri, saveFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = listViewUrls.SelectedIndices;

            foreach (int index in selectedIndices)
            {
                WebClientEx client = webClients[index];

                if (client == null)
                {
                    continue;
                }

                if (!(client.IsBusy))
                {
                    continue;
                }

                client.CancelAsync();
            }
        }

        private void buttonRemove_Click(object sender, System.EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndices = listViewUrls.SelectedIndices;
            var selectedItems = new List<ListViewItem>(selectedIndices.Count);

            foreach (int index in selectedIndices)
            {
                selectedItems.Add(listViewItems[index]);
            }

            listViewUrls.BeginUpdate();
            listViewUrls.VirtualListSize -= selectedItems.Count;

            foreach (ListViewItem item in selectedItems)
            {
                listViewItems.Remove(item);
            }

            listViewUrls.EndUpdate();
        }

        private void buttonShow_Click(object sender, System.EventArgs e)
        {
            Explore();
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            foreach (WebClientEx client in webClients)
            {
                DisposeWebClient(client);
            }
        }

        private void listViewUrls_DoubleClick(object sender, EventArgs e)
        {
            Explore();
        }

        private void listViewUrls_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = listViewItems[e.ItemIndex];
        }

        private void toolStripMenuItemShowFile_Click(object sender, EventArgs e)
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
                Clipboard.SetText(GetSubItem(listViewItems[indices[0]], columnHeaderUrl).Text);
            }
            catch
            {
            }
        }
    }
}
