using System.Windows.Forms;

namespace downloader
{
    public class DoubleBufferedListView : ListView
    {
        #region Protected Methods

        protected override bool DoubleBuffered
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        #endregion
    }
}
