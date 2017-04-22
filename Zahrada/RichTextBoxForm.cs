using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zahrada.OdvozeneTridyEle;
using Zahrada.PomocneTridy;
using Zahrada.UndoRedoBufferTridy;

namespace Zahrada
{
    /// <summary>
    /// Formularove okno pro zadani textu ve formatu RTF - Rich text format - do planu zahrady
    /// </summary>
    public partial class RichTextBoxForm : Form
    {
        #region Konstruktor okna

        public RichTextBoxForm()
        {
            InitializeComponent();
            ReallyCenterToScreen();
        }

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

        #region Obsluha OK a CANCEL tlacitka

        private void RTFOKBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RTFCancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        } 

        #endregion

    }
}
