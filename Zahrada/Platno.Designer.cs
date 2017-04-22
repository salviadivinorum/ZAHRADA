namespace Zahrada
{
    public partial class Platno
    {
        //private Platno s;
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Platno));
            this.contextMenuStripProPlatno = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.contextMenuStripProPlatno.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripProPlatno
            // 
            this.contextMenuStripProPlatno.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripProPlatno.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteContextToolStripMenuItem,
            this.CopyContextToolStripMenuItem,
            this.AllContextToolStripMenuItem});
            this.contextMenuStripProPlatno.Name = "contextMenuStripProPlatno";
            this.contextMenuStripProPlatno.Size = new System.Drawing.Size(153, 82);
            // 
            // DeleteContextToolStripMenuItem
            // 
            this.DeleteContextToolStripMenuItem.Image = global::Zahrada.Properties.Resources.smaz;
            this.DeleteContextToolStripMenuItem.Name = "DeleteContextToolStripMenuItem";
            this.DeleteContextToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.DeleteContextToolStripMenuItem.Text = "Vymazat";
            this.DeleteContextToolStripMenuItem.Click += new System.EventHandler(this.DeleteContextToolStripMenuItem_Click);
            // 
            // CopyContextToolStripMenuItem
            // 
            this.CopyContextToolStripMenuItem.Image = global::Zahrada.Properties.Resources.kopiruj;
            this.CopyContextToolStripMenuItem.Name = "CopyContextToolStripMenuItem";
            this.CopyContextToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.CopyContextToolStripMenuItem.Text = "Kopírovat";
            this.CopyContextToolStripMenuItem.Click += new System.EventHandler(this.CopyContextToolStripMenuItem_Click);
            // 
            // AllContextToolStripMenuItem
            // 
            this.AllContextToolStripMenuItem.Image = global::Zahrada.Properties.Resources.select;
            this.AllContextToolStripMenuItem.Name = "AllContextToolStripMenuItem";
            this.AllContextToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.AllContextToolStripMenuItem.Text = "Vybrat vše";
            this.AllContextToolStripMenuItem.Click += new System.EventHandler(this.AllContextToolStripMenuItem_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Platno
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Name = "Platno";
            this.Size = new System.Drawing.Size(378, 248);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Platno_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Platno_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseUp);
            this.Resize += new System.EventHandler(this.Platno_Resize);
            this.contextMenuStripProPlatno.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem DeleteContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllContextToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripProPlatno;
        public System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        public System.Windows.Forms.PrintDialog printDialog1;
    }
}
