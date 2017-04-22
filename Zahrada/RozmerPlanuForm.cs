using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zahrada
{
    /// <summary>
    /// Formularove okno pro zadani rozmeru zahrady
    /// </summary>
    public partial class RozmerPlanuForm : Form
    {

        #region Automatické vlastnosti okna RozmerPlanuFormu
        // tímto jsou clenske promenne zepouzdreny, nikdo nevi jak vypadaji, JSOU ANONYMNI, znam jen jejich vlastnosti x a y.
        // znamy to trik, ktery jsem zacal pouzivat az od tohoto projektu vyse

        public float x { get; set; }
        public float y { get; set; }

        #endregion

        #region Konstruktor okna

        public RozmerPlanuForm()
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

        #region Obsluha tlacitek ve formularovem okne

        private void buttonOKrozmer_Click(object sender, EventArgs e)
        {


            try
            {
                x = float.Parse(XdimensionTextBox.Text);
                y = float.Parse(YdimensionTextBox.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Prosím, zadejte hodnotu ve správném formátu" + Environment.NewLine + "(například: 25,7)", "Chyba v zadání hodnoty ...", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void buttonStornoRozmer_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
