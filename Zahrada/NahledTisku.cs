using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Zahrada
{
    /// <summary>
    /// Formularove okno pro tisk planu
    /// </summary>
    public partial class NahledTisku : Form
    {
        #region Konstruktor okna
        public NahledTisku()
        {
            InitializeComponent();
            ReallyCenterToScreen();
        }

        #endregion

        #region Klikani na tlacitka ve formu
        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        // po stisku primy TISK ...
        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            docToPrint.Print();
        }

        // Pouze meni hezky velikost okna ...
        private void NahledTisku_Resize(object sender, EventArgs e)
        {
            PrintPreviewControl.Width = Width - 10;
            PrintPreviewControl.Height = Height - PrintPreviewToolStrip.Height - 37;
        }

        // stisk Nastaveni tisku ...
        private void NastaveniToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult dlg = printDialog1.ShowDialog();
            // neumim to osetrit lepe - idealni by bylo tlacitko "Pouzit" z Dialogu, ale to WinForms nezna
            if (dlg != DialogResult.Cancel)
            {
                PrinterSettings mysettings = printDialog1.PrinterSettings;
                docToPrint.PrinterSettings = mysettings;
                PrintPreviewControl.Document = docToPrint;
                PrintPreviewControl.Refresh();
            }
        } 
        #endregion

        #region Zoomování náhledu tisku - klikani na tlacitka
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem1.Text) / 100;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem2.Text) / 100;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem3.Text) / 100;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem4.Text) / 100;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem5.Text) / 100;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem6.Text) / 100;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            PrintPreviewControl.Zoom = (float)Convert.ToDouble(toolStripMenuItem7.Text) / 100;
        }
        #endregion

        #region Pomocna centrovaci metoda vzskakovaciho okna

        // pomocna metoda - vycentruje modalni vyskakovaci okna do stredu obrazovky
        protected void ReallyCenterToScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };
        }

        #endregion

    }

}
