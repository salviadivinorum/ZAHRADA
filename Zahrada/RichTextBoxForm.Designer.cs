namespace Zahrada
{
    partial class RichTextBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextBoxForm));
            this.mujRichTextBox = new System.Windows.Forms.RichTextBox();
            this.RTFOKBtn = new System.Windows.Forms.Button();
            this.RTFCancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mujRichTextBox
            // 
            this.mujRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mujRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mujRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.mujRichTextBox.Name = "mujRichTextBox";
            this.mujRichTextBox.Size = new System.Drawing.Size(509, 117);
            this.mujRichTextBox.TabIndex = 0;
            this.mujRichTextBox.Text = "";
            // 
            // RTFOKBtn
            // 
            this.RTFOKBtn.Location = new System.Drawing.Point(346, 149);
            this.RTFOKBtn.Name = "RTFOKBtn";
            this.RTFOKBtn.Size = new System.Drawing.Size(80, 35);
            this.RTFOKBtn.TabIndex = 1;
            this.RTFOKBtn.Text = "OK";
            this.RTFOKBtn.UseVisualStyleBackColor = true;
            this.RTFOKBtn.Click += new System.EventHandler(this.RTFOKBtn_Click);
            // 
            // RTFCancelBtn
            // 
            this.RTFCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.RTFCancelBtn.Location = new System.Drawing.Point(441, 149);
            this.RTFCancelBtn.Name = "RTFCancelBtn";
            this.RTFCancelBtn.Size = new System.Drawing.Size(80, 35);
            this.RTFCancelBtn.TabIndex = 2;
            this.RTFCancelBtn.Text = "Storno";
            this.RTFCancelBtn.UseVisualStyleBackColor = true;
            this.RTFCancelBtn.Click += new System.EventHandler(this.RTFCancelBtn_Click);
            // 
            // RichTextBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.RTFCancelBtn;
            this.ClientSize = new System.Drawing.Size(533, 204);
            this.Controls.Add(this.RTFCancelBtn);
            this.Controls.Add(this.RTFOKBtn);
            this.Controls.Add(this.mujRichTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RichTextBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Editor textu";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox mujRichTextBox;
        private System.Windows.Forms.Button RTFOKBtn;
        private System.Windows.Forms.Button RTFCancelBtn;
    }
}