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
            this.SuspendLayout();
            // 
            // Platno
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Name = "Platno";
            this.Size = new System.Drawing.Size(378, 248);
            //this.Click += new System.EventHandler(this.Platno_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Platno_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Platno_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Platno_MouseUp);
            this.Resize += new System.EventHandler(this.Platno_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
