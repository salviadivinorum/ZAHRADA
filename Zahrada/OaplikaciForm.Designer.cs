namespace Zahrada
{
    partial class OaplikaciForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OaplikaciForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InfoTextBox = new System.Windows.Forms.TextBox();
            this.CopyrightTextBox = new System.Windows.Forms.TextBox();
            this.OKbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Navrhování zahrad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(88, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "verze 1.0";
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.InfoTextBox.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.InfoTextBox.Location = new System.Drawing.Point(27, 87);
            this.InfoTextBox.Multiline = true;
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(222, 44);
            this.InfoTextBox.TabIndex = 2;
            this.InfoTextBox.TabStop = false;
            this.InfoTextBox.Text = "Aplikace umožňuje navržení vzhledu zahrady";
            this.InfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CopyrightTextBox
            // 
            this.CopyrightTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CopyrightTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CopyrightTextBox.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CopyrightTextBox.Location = new System.Drawing.Point(6, 137);
            this.CopyrightTextBox.Multiline = true;
            this.CopyrightTextBox.Name = "CopyrightTextBox";
            this.CopyrightTextBox.ReadOnly = true;
            this.CopyrightTextBox.Size = new System.Drawing.Size(260, 50);
            this.CopyrightTextBox.TabIndex = 3;
            this.CopyrightTextBox.TabStop = false;
            this.CopyrightTextBox.Text = "(c) 2017 David Jaroš Freeware pro akademické a nekomerční použití";
            this.CopyrightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OKbutton
            // 
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OKbutton.Location = new System.Drawing.Point(92, 214);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(80, 35);
            this.OKbutton.TabIndex = 4;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // OaplikaciForm
            // 
            this.AcceptButton = this.OKbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.OKbutton;
            this.ClientSize = new System.Drawing.Size(271, 261);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.CopyrightTextBox);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OaplikaciForm";
            this.Text = "O aplikaci";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InfoTextBox;
        private System.Windows.Forms.TextBox CopyrightTextBox;
        private System.Windows.Forms.Button OKbutton;
    }
}