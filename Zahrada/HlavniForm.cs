using System;
using System.Collections;
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
    public partial class HlavniForm : Form
    {

        #region Clenske promenne Hlavniho formulare

        // promenna udalost na klikani tlacitek
        public event EventHandler OnButtonZoomPusClick;
        public event EventHandler OnButtonZoomMinusClick;
        private bool mrizZapnuta = true;
        int ulozGrid = 0;

        //public event ObjectSelectedEventHandler ObjectSelected;

        #endregion

        #region Konstruktor Hlavniho okna
        public HlavniForm()
        {
            InitializeComponent();
            myInit();
        }

        /// <summary>
        /// Na vlozeny toolBox Nastroje nalinkuje platno vlozenePlatno
        /// </summary>
        private void myInit()  // vlozenyToolBox ovlada vlozenePlatno
        {
            vlozenyToolBox.SetPlatno(vlozenePlatno); // nalinkuju vlozenyToolBox z navrhare na vlozenePlatno z navrhare
                                                     //vlozenePlatno.Platno_DoubleClick(null, null); // timto si pomaham - inicializuju vlastne mujFiltredPropertyGrid a Platno - ramecek A4

            // uprava platna do Zoom = 0.25, posun o 100 pixelu dolu a doprava, zmena indexu na 1 v ZoomCB
            vlozenePlatno.dx = 100;
            vlozenePlatno.dy = 100;
            vlozenePlatno.Zoom = 0.25f;
            vlozenePlatno.NajdiZoomComboBoxvMainForm();
            vlozenePlatno.UpravZoomVComboBoxu(1);

            vlozenyToolBox.NajdiUndoReodBtnsVmainForm();

            //this.vlozenePlatno.ParentForm = this;
            OnButtonZoomPusClick += new EventHandler(KlikNaPlus); // priradim udalost na tlacitko + Zoom
            OnButtonZoomMinusClick += new EventHandler(KlikNaMinus); // priradim udal na tlac - Zoom

           // ObjectSelected += new ObjectSelectedEventHandler(OnBasicObjectSelected);
            //vlozenePlatno.SetMainForm(this);
        }
        #endregion

        #region Obsluha podruznych udalosti - Load, apod
        private void hlavniFormularoveOkno_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; // maximalizace formul. okna
            penWidthtoolStripComboBox.SelectedIndex = 0; // comboboxy nahe v toolstripu
            fillingOnOffToolStripComboBox.SelectedIndex = 0;
            gridToolStripComboBox.SelectedIndex = 0;
            zoomToolStripComboBox.SelectedIndex = 1;
            closedToolStripComboBox.SelectedIndex = 0;
            vlozenePlatno.Zoom = 0.25f;
            // vlozenePlatno.Scale(new Size(2f, 2f))
        }

        #endregion

        #region Obsluha Click udalosti na polozky v Hlavnim formulari - menustrip, toolstrip, statusstrip
       // private void OnBasicObjectSelected(object sender, PropertyEventArgs e)
       // { }
        // Pen color - generalni
        private void penColortoolStripButton_Click(object sender, EventArgs e)
        {
            mujColorDialog.Color = penColortoolStripButton.BackColor;
            mujColorDialog.ShowDialog(this);
            penColortoolStripButton.BackColor = mujColorDialog.Color;
            vlozenePlatno.SetPenColor(mujColorDialog.Color);
            //vlozenePlatno.Focus();
        }

        // Fill color - generalni
        private void fillColorToolStripButton_Click(object sender, EventArgs e)
        {
            mujColorDialog.Color = fillColorToolStripButton.BackColor;
            mujColorDialog.ShowDialog(this);
            fillColorToolStripButton.BackColor = mujColorDialog.Color;
            vlozenePlatno.SetFillColor(mujColorDialog.Color);
            //vlozenePlatno.Focus();
        }
        
        // Zmena Grid
        private void gridToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {


            string hodnota = gridToolStripComboBox.Text;
            int startI = 5;
            int endI = hodnota.Length;
            int delkaSub = endI - startI;
            string kolik = hodnota.Substring(startI, delkaSub);
            if (kolik != "Vyp")
            {
                int grsize = int.Parse(kolik);
                vlozenePlatno.gridSize = grsize;
                mrizZapnuta = true;
            }
            else vlozenePlatno.gridSize = 0;
            vlozenePlatno.Focus();
            //vlozenyToolBox.Select();
           
        }

        // zakladni Zoom In + 
        public void zoomINtoolStripButton_Click(object sender, EventArgs e)
        {
            //vlozenePlatno.dx = 1;
            //vlozenePlatno.dy = 1;
            OnButtonZoomPusClick(this, null);

            //vlozenePlatno.dx = (int)(vlozenePlatno.dx / vlozenePlatno.Zoom);
            //vlozenePlatno.dy = (int)(vlozenePlatno.dy / vlozenePlatno.Zoom);

            //vlozenePlatno.dx = (int)(vlozenePlatno.dx - 200);
            //vlozenePlatno.dy = (int)(vlozenePlatno.dy - 200);
            //vlozenePlatno.Redraw(false);
            int sirkaPlatna = vlozenePlatno.Size.Width;
            int vyskaPlatna = vlozenePlatno.Height;
            
            vlozenePlatno.ZoomIn(sirkaPlatna, vyskaPlatna);
            vlozenePlatno.ZoomIn();
            //vlozenePlatno.Focus();


            //vlozenePlatno.Redraw(true);
            // zoomToolStripComboBox_SelectedIndexChanged(sender, null);


        }

        // zkaldni Zoom Out -
        public void zoomOUTtoolStripButton_Click(object sender, EventArgs e)
        {
            OnButtonZoomMinusClick(this, null);
            vlozenePlatno.ZoomOut();
            //vlozenePlatno.Focus();
            //zoomToolStripComboBox_SelectedIndexChanged(sender, null);
        }


              
        // metoda pro ovladac udalosti Zoom +
        public void KlikNaPlus(object sender, EventArgs e)
        {
            
            int index = zoomToolStripComboBox.SelectedIndex;
            if (index <= 6)
            {
                zoomToolStripComboBox.SelectedIndex = index + 1;
                //zoomToolStripComboBox_SelectedIndexChanged(sender, null);
            }
            //vlozenePlatno.Focus();

        }

        public void KlikNaMinus(object sender, EventArgs e)
        {
            int index = zoomToolStripComboBox.SelectedIndex;
            if (index >= 1)
                zoomToolStripComboBox.SelectedIndex = index - 1;
            //zoomToolStripComboBox_SelectedIndexChanged(sender, null);
            //vlozenePlatno.Focus();
        }


        

        private void closedToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            int index = closedToolStripComboBox.SelectedIndex;
            if (index == 0)
            {
                vlozenePlatno.SetClosed(false);


            }
            else
            {
                vlozenePlatno.SetClosed(true);

            }
            vlozenePlatno.Redraw(true);
            vlozenePlatno.Focus();
        }


        private void fillingOnOffToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (fillingOnOffToolStripComboBox.SelectedIndex == 0)
            {
                // fillColorToolStripButton.BackColor = Color.Transparent;
                vlozenePlatno.SetFilled(false);                
                vlozenePlatno.Redraw(true);             
            }
            else if (fillingOnOffToolStripComboBox.SelectedIndex == 1)
            {
                
                if (fillColorToolStripButton.BackColor.Equals((zoomInToolStripButton.BackColor)))
                {
                    vlozenePlatno.SetFillColor(fillColorToolStripButton.BackColor);
                    fillColorToolStripButton.BackColor = Color.Black;
                    fillColorToolStripButton.ForeColor = Color.White;
                }

                    
                vlozenePlatno.SetFilled(true);
                
                vlozenePlatno.Redraw(true);
            }
            vlozenePlatno.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        



        
        // osetreni SPACE jako ovladani mrize - zapnout/vypnout
        private void vlozenePlatno_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            // doladit esc klavesu at odznaci vse
            if(e.KeyCode == Keys.Escape)
            {
                
                ArrayList seznam = vlozenePlatno.GetElements;
                foreach (Ele obj in seznam)
                {
                    obj.selected = false;
                    obj.DeSelect();
                    
                }
                vlozenePlatno.Redraw(true);
                
                //PropertyEventArgs e1 = new PropertyEventArgs(this.vlozenePlatno.shapes.GetSelectedArray(), false, false);
               // ObjectSelected(null, null);// raise event
                vlozenePlatno.Redraw(true);

                //PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                //ObjectSelected(this, e1);

                //vlozenePlatno.Platno_MouseUp(null, null);

            }
            */

            if (e.KeyCode == Keys.Space)
            {
                if(mrizZapnuta == true)
                {
                    int kolik = gridToolStripComboBox.SelectedIndex;
                    int gr = 0;
                    switch (kolik)
                    {
                        #region index -> grid
                        case 0:
                            gr = 0;
                            break;
                        case 1:
                            gr = 1;
                            break;
                        case 2:
                            gr = 5;
                            break;
                        case 3:
                            gr = 10;
                            break;
                        case 4:
                            gr = 25;
                            break;
                        case 5:
                            gr = 50;
                            break;
                        case 6:
                            gr = 100;
                            break;
                        case 7:
                            gr = 250;
                            break;
                        case 8:
                            gr = 500;
                            break;
                        default:
                            gr = 0;
                            break; 
                            #endregion
                    }
                    ulozGrid = gr;
                    vlozenePlatno.gridSize = 0;
                    gridToolStripComboBox.SelectedIndex = 0;
                    mrizZapnuta = false;
                }
                else
                {

                    vlozenePlatno.gridSize = ulozGrid;
                    mrizZapnuta = true;
                    int index = 0;
                    switch (ulozGrid)                        
                    {
                        #region grid -> index
                        case 0:
                            index = 0;
                            break;
                        case 1:
                            index = 1;
                            break;
                        case 5:
                            index = 2;
                            break;
                        case 10:
                            index = 3;
                            break;
                        case 25:
                            index = 4;
                            break;
                        case 50:
                            index = 5;
                            break;
                        case 100:
                            index = 6;
                            break;
                        case 250:
                            index = 7;
                            break;
                        case 500:
                            index = 8;
                            break; 
                            #endregion
                    }
                    gridToolStripComboBox.SelectedIndex = index;

                }  

            }

        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Undo();
            undoToolStripButton.Enabled = vlozenePlatno.UndoEnabled();
            redoToolStripButton.Enabled = vlozenePlatno.RedoEnabled();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Redo();
            redoToolStripButton.Enabled = vlozenePlatno.RedoEnabled();
            undoToolStripButton.Enabled = vlozenePlatno.UndoEnabled();
        }

        private void penWidthtoolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            vlozenePlatno.Focus();
        }

        

        private void zoomToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            vlozenePlatno.Focus();

        }












        #endregion


        // dodelat ...
        /*
        private void zoomToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = zoomToolStripComboBox.SelectedIndex;
            float zoo = 1f;

            if (index == 0)
                zoo = 0.125f;
            else if (index == 1)
                zoo = 0.25f;
            else if (index == 2)
                zoo = 0.5f;
            else if (index == 3)
                zoo = 1;
            else if (index == 4)
                zoo = 2;
            else if (index == 5)
                zoo = 4;
            else if (index == 6)
                zoo = 8;
            else if (index == 7)
                zoo = 16;
            vlozenePlatno.Zoom = zoo;

        }
        */
    }
    
}
