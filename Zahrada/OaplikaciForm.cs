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
    public partial class OaplikaciForm : Form
    {
        public OaplikaciForm()
        {
            InitializeComponent();
            MyInit();
            ReallyCenterToScreen();
        }


        private void MyInit()
        {
            CopyrightTextBox.Text = "(c) 2017 David Jaroš"
                + Environment.NewLine +
                "Freeware pro akademické" 
                + Environment.NewLine+
                "a nekomerční použití";
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
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
