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
    public partial class NahledTisku : Form
    {
        public NahledTisku()
        {
            InitializeComponent();
        }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            docToPrint.Print();
        }

        private void NahledTisku_Resize(object sender, EventArgs e)
        {
            PrintPreviewControl.Width = Width - 10;
            PrintPreviewControl.Height = Height - PrintPreviewToolStrip.Height - 37;
        }

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
    }

}
