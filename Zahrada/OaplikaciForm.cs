﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zahrada
{
    /// <summary>
    /// Okno s informací o tvůrci applikace
    /// </summary>
    public partial class OaplikaciForm : Form
    {
        #region Konstruktor okna
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
                + Environment.NewLine +
                "a nekomerční použití";
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
        
        #region Stisk OK tlacitka
        private void OKbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }

}
