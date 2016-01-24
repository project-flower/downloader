using System.Windows.Forms;

namespace downloader
{
    class DoubleBufferedListView : ListView
    {
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
    }
}
