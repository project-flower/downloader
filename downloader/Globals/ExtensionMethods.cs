using System.Windows.Forms;

namespace downloader.Globals
{
    public static class ExtensionMethods
    {
        #region Public Methods

        public static int GetIntValue(this NumericUpDown numericUpDown)
        {
            return (int)numericUpDown.Value;
        }

        #endregion
    }
}
