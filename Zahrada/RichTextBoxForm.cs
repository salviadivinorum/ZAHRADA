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
    /// Toto bude formular pro zadavani textu ve formatu RTF Rich text format
    /// </summary>
    public partial class RichTextBoxForm : Form
    {
        public RichTextBoxForm()
        {
            InitializeComponent();
            ReallyCenterToScreen();
        }

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


    }
}
