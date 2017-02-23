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
            this.mujRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // mujRichTextBox
            // 
            this.mujRichTextBox.Location = new System.Drawing.Point(12, 90);
            this.mujRichTextBox.Name = "mujRichTextBox";
            this.mujRichTextBox.Size = new System.Drawing.Size(831, 250);
            this.mujRichTextBox.TabIndex = 0;
            this.mujRichTextBox.Text = "";
            // 
            // RichTextBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 352);
            this.Controls.Add(this.mujRichTextBox);
            this.Name = "RichTextBoxForm";
            this.Text = "Editor textu";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox mujRichTextBox;
    }
}