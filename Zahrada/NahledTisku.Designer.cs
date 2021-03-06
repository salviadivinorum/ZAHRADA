﻿namespace Zahrada
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
            this.ZoomToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.docToPrint = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.NastaveniToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintPreviewToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrintPreviewToolStrip
            // 
            this.PrintPreviewToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PrintPreviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NastaveniToolStripButton,
            this.toolStripSeparator1,
            this.PrintToolStripButton,
            this.toolStripSeparator2,
            this.ZoomToolStripSplitButton,
            this.toolStripSeparator3,
            this.CloseToolStripButton});
            this.PrintPreviewToolStrip.Location = new System.Drawing.Point(0, 0);
            this.PrintPreviewToolStrip.Name = "PrintPreviewToolStrip";
            this.PrintPreviewToolStrip.Size = new System.Drawing.Size(782, 27);
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
            this.CloseToolStripButton.ToolTipText = "Zavřít okno Náhled tisku";
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
            this.PrintToolStripButton.ToolTipText = "Tisk náhledu na zvolenou tiskárnu";
            this.PrintToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
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
            this.ZoomToolStripSplitButton.Size = new System.Drawing.Size(92, 24);
            this.ZoomToolStripSplitButton.Text = "Náhled %";
            this.ZoomToolStripSplitButton.ToolTipText = "Náhled projektu v %";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem1.Text = "25";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem2.Text = "50";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem3.Text = "75";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem4.Text = "100";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem5.Text = "150";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem6.Text = "200";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem7.Text = "500";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // PrintPreviewControl
            // 
            this.PrintPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintPreviewControl.Location = new System.Drawing.Point(0, 27);
            this.PrintPreviewControl.Name = "PrintPreviewControl";
            this.PrintPreviewControl.Size = new System.Drawing.Size(782, 426);
            this.PrintPreviewControl.TabIndex = 1;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // NastaveniToolStripButton
            // 
            this.NastaveniToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NastaveniToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NastaveniToolStripButton.Image")));
            this.NastaveniToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NastaveniToolStripButton.Name = "NastaveniToolStripButton";
            this.NastaveniToolStripButton.Size = new System.Drawing.Size(132, 24);
            this.NastaveniToolStripButton.Text = "Nastavení tiskárny";
            this.NastaveniToolStripButton.ToolTipText = "Nastavení předvolby tiskárny";
            this.NastaveniToolStripButton.Click += new System.EventHandler(this.NastaveniToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // NahledTisku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.PrintPreviewControl);
            this.Controls.Add(this.PrintPreviewToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NahledTisku";
            this.Text = " Náhled tisku";
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
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripButton NastaveniToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}