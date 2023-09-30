
namespace downloader
{
    partial class FormAddBatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelUrl = new System.Windows.Forms.Label();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.numericUpDownFrom = new System.Windows.Forms.NumericUpDown();
            this.labelTo = new System.Windows.Forms.Label();
            this.numericUpDownTo = new System.Windows.Forms.NumericUpDown();
            this.labelWildcardBytes = new System.Windows.Forms.Label();
            this.numericUpDownWildcardBytes = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWildcardBytes)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(12, 15);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(29, 12);
            this.labelUrl.TabIndex = 0;
            this.labelUrl.Text = "&URL:";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrl.Location = new System.Drawing.Point(47, 12);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(310, 19);
            this.textBoxUrl.TabIndex = 1;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(45, 34);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(129, 12);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Use (*) for the wildcard.";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(12, 51);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(33, 12);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "&From:";
            // 
            // numericUpDownFrom
            // 
            this.numericUpDownFrom.Location = new System.Drawing.Point(51, 49);
            this.numericUpDownFrom.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownFrom.Name = "numericUpDownFrom";
            this.numericUpDownFrom.Size = new System.Drawing.Size(60, 19);
            this.numericUpDownFrom.TabIndex = 4;
            this.numericUpDownFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(117, 51);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(20, 12);
            this.labelTo.TabIndex = 5;
            this.labelTo.Text = "&To:";
            // 
            // numericUpDownTo
            // 
            this.numericUpDownTo.Location = new System.Drawing.Point(143, 49);
            this.numericUpDownTo.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownTo.Name = "numericUpDownTo";
            this.numericUpDownTo.Size = new System.Drawing.Size(60, 19);
            this.numericUpDownTo.TabIndex = 6;
            this.numericUpDownTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelWildcardBytes
            // 
            this.labelWildcardBytes.AutoSize = true;
            this.labelWildcardBytes.Location = new System.Drawing.Point(209, 51);
            this.labelWildcardBytes.Name = "labelWildcardBytes";
            this.labelWildcardBytes.Size = new System.Drawing.Size(82, 12);
            this.labelWildcardBytes.TabIndex = 7;
            this.labelWildcardBytes.Text = "Wi&ldcard bytes:";
            // 
            // numericUpDownWildcardBytes
            // 
            this.numericUpDownWildcardBytes.Location = new System.Drawing.Point(297, 49);
            this.numericUpDownWildcardBytes.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDownWildcardBytes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWildcardBytes.Name = "numericUpDownWildcardBytes";
            this.numericUpDownWildcardBytes.Size = new System.Drawing.Size(60, 19);
            this.numericUpDownWildcardBytes.TabIndex = 8;
            this.numericUpDownWildcardBytes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownWildcardBytes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(201, 74);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 9;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(282, 74);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormAddBatch
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(369, 109);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownWildcardBytes);
            this.Controls.Add(this.labelWildcardBytes);
            this.Controls.Add(this.numericUpDownTo);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.numericUpDownFrom);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.labelUrl);
            this.Name = "FormAddBatch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Batch";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWildcardBytes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.NumericUpDown numericUpDownFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.NumericUpDown numericUpDownTo;
        private System.Windows.Forms.Label labelWildcardBytes;
        private System.Windows.Forms.NumericUpDown numericUpDownWildcardBytes;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}