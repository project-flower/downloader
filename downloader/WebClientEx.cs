using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace downloader
{
    public class WebClientEx : WebClient
    {
        #region Public Fields

        public readonly ListViewItem ListViewItem;
        public readonly string SaveFileName;

        #endregion

        #region Private Fields

        private readonly AsyncCompletedEventHandler downloadFileCompleted;
        private readonly DownloadProgressChangedEventHandler downloadProgressChanged;
        private bool completed = false;

        #endregion

        #region Public Properties

        public bool Completed
        {
            get { return completed; }
        }

        #endregion

        #region Public Methods

        public WebClientEx(string saveFileName, ListViewItem listViewItem, AsyncCompletedEventHandler downloadFileCompleted, DownloadProgressChangedEventHandler downloadProgressChanged)
        {
            ListViewItem = listViewItem;
            SaveFileName = saveFileName;
            this.downloadFileCompleted = downloadFileCompleted;
            this.downloadProgressChanged = downloadProgressChanged;
            //DownloadDataCompleted += downloadDataCompleted;
            DownloadFileCompleted += webClientEx_DownloadFileCompleted;
            DownloadFileCompleted += downloadFileCompleted;
            DownloadProgressChanged += downloadProgressChanged;
        }

        public new void Dispose()
        {
            //DownloadDataCompleted -= downloadFileCompleted;
            DownloadFileCompleted -= downloadFileCompleted;
            DownloadProgressChanged -= downloadProgressChanged;
        }

        #endregion

        #region Private Methods

        private void webClientEx_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            completed = true;
        }

        #endregion
    }
}
