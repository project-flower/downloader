using downloader.Globals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace downloader
{
    public partial class FormAddBatch : Form
    {
        #region Public Methods

        public FormAddBatch()
        {
            InitializeComponent();
            MaximumSize = new Size(int.MaxValue, Height);
        }

        public string[] GetValues()
        {
            string text = textBoxUrl.Text;
            string placeholder = "(*)";

            if (!text.Contains(placeholder))
            {
                throw new Exception("プレース ホルダ (*) が含まれていません。");
            }

            int value1 = numericUpDownFrom.GetIntValue();
            int value2 = numericUpDownTo.GetIntValue();
            int from = Math.Min(value1, value2);
            int to = Math.Max(value1, value2);
            int bytes = numericUpDownWildcardBytes.GetIntValue();
            var list = new List<string>(to - from + 1);
            string format = string.Format("{{0:D{0}}}", bytes);

            for (int i = from; i <= to; ++i)
            {
                list.Add(text.Replace(placeholder, string.Format(format, i)));
            }

            return list.ToArray();
        }

        #endregion

        // Designer's Methods

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
