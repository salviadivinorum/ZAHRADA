namespace Zahrada
{
    partial class NahledTisku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NahledTisku));
            this.PrintPreviewToolStrip = new System.Windows.Forms.ToolStrip();
            this.CloseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PrintPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.docToPrint = new System.Drawing.Printing.PrintDocument();
            this.ZoomToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrintPreviewToolStrip
            // 
            this.PrintPreviewToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PrintPreviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseToolStripButton,
            this.PrintToolStripButton,
            this.ZoomToolStripSplitButton});
            this.PrintPreviewToolStrip.Location = new System.Drawing.Point(0, 0);
            this.PrintPreviewToolStrip.Name = "PrintPreviewToolStrip";
            this.PrintPreviewToolStrip.Size = new System.Drawing.Size(982, 27);
            this.PrintPreviewToolStrip.TabIndex = 0;
            this.PrintPreviewToolStrip.Text = "toolStrip1";
            // 
            // CloseToolStripButton
            // 
            this.CloseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CloseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseToolStripButton.Image")));
            this.CloseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseToolStripButton.Name = "CloseToolStripButton";
            this.CloseToolStripButton.Size = new System.Drawing.Size(51, 24);
            this.CloseToolStripButton.Text = "Zavřít";
            this.CloseToolStripButton.Click += new System.EventHandler(this.CloseToolStripButton_Click);
            // 
            // PrintToolStripButton
            // 
            this.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PrintToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PrintToolStripButton.Image")));
            this.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton.Name = "PrintToolStripButton";
            this.PrintToolStripButton.Size = new System.Drawing.Size(38, 24);
            this.PrintToolStripButton.Text = "Tisk";
            this.PrintToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // PrintPreviewControl
            // 
            this.PrintPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintPreviewControl.Location = new System.Drawing.Point(0, 27);
            this.PrintPreviewControl.Name = "PrintPreviewControl";
            this.PrintPreviewControl.Size = new System.Drawing.Size(982, 626);
            this.PrintPreviewControl.TabIndex = 1;
            // 
            // ZoomToolStripSplitButton
            // 
            this.ZoomToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ZoomToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.ZoomToolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToolStripSplitButton.Image")));
            this.ZoomToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomToolStripSplitButton.Name = "ZoomToolStripSplitButton";
            this.ZoomToolStripSplitButton.Size = new System.Drawing.Size(84, 24);
            this.ZoomToolStripSplitButton.Text = "Zoom %";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem1.Text = "25";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem2.Text = "50";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem3.Text = "75";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem4.Text = "100";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem5.Text = "150";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem6.Text = "200";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem7.Text = "500";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // NahledTisku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.PrintPreviewControl);
            this.Controls.Add(this.PrintPreviewToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NahledTisku";
            this.Text = "Náhled tisku";
            this.Resize += new System.EventHandler(this.NahledTisku_Resize);
            this.PrintPreviewToolStrip.ResumeLayout(false);
            this.PrintPreviewToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip PrintPreviewToolStrip;
        private System.Windows.Forms.ToolStripButton CloseToolStripButton;
        private System.Windows.Forms.ToolStripButton PrintToolStripButton;
        public System.Windows.Forms.PrintPreviewControl PrintPreviewControl;
        public System.Drawing.Printing.PrintDocument docToPrint;
        private System.Windows.Forms.ToolStripSplitButton ZoomToolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
    }
}