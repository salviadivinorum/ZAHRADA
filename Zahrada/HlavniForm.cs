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
        private int ulozGrid = 0;
        private const int RGBMAX = 255;
        private string CustomPlanSizeString { get; set; } // automaticka vlastnost pro vlastni velikost planu ...
        private float CustomX { get; set; } // custom sirka planu
        private float CustomY { get; set; }// custom vyska planu

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
            colorFillingOnOffToolStripComboBox.SelectedIndex = 0;
            textureFillingOnOffToolStripComboBox.SelectedIndex = 0;
            gridToolStripComboBox.SelectedIndex = 0;
            zoomToolStripComboBox.SelectedIndex = 1;
            closedToolStripComboBox.SelectedIndex = 0;
            vlozenePlatno.Zoom = 0.25f;
            
            
            // vlozenePlatno.Scale(new Size(2f, 2f))
        }

        #endregion

       
        

        // pomocnametoda - zneguje mi barvu ....
        private Color InvertMeAColour(Color ColourToInvert)
        {
            return Color.FromArgb(RGBMAX - ColourToInvert.R,
              RGBMAX - ColourToInvert.G, RGBMAX - ColourToInvert.B);
        }


        // Label namisto tlacitka PenColor        
        #region PenColorLabel
        private void PokusToolStripLabel_Click(object sender, EventArgs e)
        {
            mujColorDialog.ShowDialog(this);
            vlozenePlatno.SetPenColor(mujColorDialog.Color);
            vlozenePlatno.ChangeOption("select");
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }

        private void PenColorToolStripLabel_MouseEnter(object sender, EventArgs e)
        {
            Color barva = vlozenePlatno.creationPenColor;
            Color barvaPera = InvertMeAColour(vlozenePlatno.creationPenColor);
            // Cast to allow reuse of method.
            ToolStripItem tsi = (ToolStripItem)sender;

            // Create semi-transparent picture.
            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    //bm.SetPixel(x, y, Color.FromArgb(150, barva));
                    bm.SetPixel(x, y, barva);
            }

            // Set background.
            tsi.BackgroundImage = bm;
            tsi.ForeColor = barvaPera;

        }


        private void PenColorToolStripLabel_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
            (sender as ToolStripItem).ForeColor = Color.Black;
        }

        #endregion

        
        // Label namisto tlacitka FillColor
        #region FillColor Label
        private void fillColorToolStripLabel_Click(object sender, EventArgs e)
        {
            //mujColorDialog.Color = fillColorToolStripButton.BackColor;
            mujColorDialog.ShowDialog(this);
            //fillColorToolStripButton.BackColor = mujColorDialog.Color;
            vlozenePlatno.SetFillColor(mujColorDialog.Color);
            vlozenePlatno.ChangeOption("select");
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }


        private void fillColorToolStripLabel_MouseEnter(object sender, EventArgs e)
        {
            Color barva = vlozenePlatno.creationFillColor;
            Color barvaPera = InvertMeAColour(vlozenePlatno.creationFillColor);
            // Cast to allow reuse of method.
            ToolStripItem tsi = (ToolStripItem)sender;

            // Create semi-transparent picture.
            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    //bm.SetPixel(x, y, Color.FromArgb(150, barva));
                bm.SetPixel(x, y, barva);
            }

            // Set background.
            tsi.BackgroundImage = bm;
            tsi.ForeColor = barvaPera;
        }


        private void fillColorToolStripLabel_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
            (sender as ToolStripItem).ForeColor = Color.Black;
        }
        #endregion


        // Label namisto tlacitka VzorTextury
        #region Vzor Textury Label
        private void texturaToolStripLabel_Click(object sender, EventArgs e)
        {
            /*
            using (OpenFileDialog dil = new OpenFileDialog())
            {
                
                // nastavuje aktualni cestu k exe souboru zahrada.exe
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dil.InitialDirectory = currentDirectory;
                dil.RestoreDirectory = true;

                dil.Title = "Vyber texturu";
                dil.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";

                if (dil.ShowDialog() == DialogResult.OK)
                {
                    Image obr = new Bitmap(dil.FileName);
                    TextureBrush tBrush = new TextureBrush(obr);
                    tBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                    vlozenePlatno.SetTexture(tBrush);

                }

            }

            */
            vlozenePlatno.TextureLoader();
            vlozenePlatno.ChangeOption("select");
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }

        

        private void texturaToolStripLabel_MouseEnter(object sender, EventArgs e)
        {
            
            Image bitm = vlozenePlatno.creationTexturePattern.Image;
            

            // Cast to allow reuse of method.
            ToolStripItem tsi = (ToolStripItem)sender;

            
            // Set background.
            tsi.BackgroundImage = bitm;
            tsi.ForeColor = Color.White;
            //tsi.ImageTransparentColor = Color.LimeGreen;


        }

        private void texturaToolStripLabel_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
            (sender as ToolStripItem).ForeColor = Color.Black;
        }




        #endregion


        


        // Zmena Grid
        private void gridToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            int index = gridToolStripComboBox.SelectedIndex;
            int grs;
            switch (index)
            {
                case 0:
                    grs = 0;
                    break;
                case 1:
                    grs = 1;
                    break;
                case 2:
                    grs = 5;
                    break;
                case 3:
                    grs = 10;
                    break;
                case 4:
                    grs = 25;
                    break;
                case 5:
                    grs = 50;
                    break;
                case 6:
                    grs = 100;
                    break;
                case 7:
                    grs = 250;
                    break;
                case 8:
                    grs = 500;
                    break;
                default:
                    grs = 0;
                    break;

            }

            vlozenePlatno.gridSize = grs;
            vlozenePlatno.Focus();

            /*
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
            */
           
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


              
        // metoda pro ovladac udalosti Zoom + -
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
            
        // objekt Closed ano/ne
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
           
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }

        // FILLING barvou On/Off
        private void fillingOnOffToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            vlozenePlatno.Focus();
            if (colorFillingOnOffToolStripComboBox.SelectedIndex == 0)
            {
                // fillColorToolStripButton.BackColor = Color.Transparent;
                vlozenePlatno.SetColorFilled(false);                
                vlozenePlatno.Redraw(true);             
            }
            else if (colorFillingOnOffToolStripComboBox.SelectedIndex == 1)
            {
                textureFillingOnOffToolStripComboBox.SelectedIndex = 0;
                vlozenePlatno.SetTextureFilled(false);               

                    
                vlozenePlatno.SetColorFilled(true);
                
                vlozenePlatno.Redraw(true);
                vlozenePlatno.ChangeOption("select");
            }
           
        }

        // FILLING texturou Ano/Ne
        private void textureFillingOnOffToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            



            if (textureFillingOnOffToolStripComboBox.SelectedIndex == 0)
            {
                vlozenePlatno.SetTextureFilled(false);
               


            }
            else
            {
                vlozenePlatno.SetTextureFilled(true);
                colorFillingOnOffToolStripComboBox.SelectedIndex = 0;
                vlozenePlatno.SetColorFilled(false);
               

            }

            vlozenePlatno.Redraw(true);
            vlozenePlatno.Focus();
        }


        // Exit a Closed()
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

                
        // osetreni klavesy SPACE jako ovladani mrize - zapnout/vypnout
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

        // osetreni undo/redo sipek
        #region Undo/Redo sipky
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
        #endregion

        // Dodelat Pen Width - sirku pera ....
        private void penWidthtoolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            vlozenePlatno.Focus();
        }

        
        // dodelat ZOOM ComboBox ....
        private void zoomToolStripComboBox_DropDownClosed(object sender, EventArgs e)
        {
            vlozenePlatno.Focus();

        }

        // Save As tlacitko ...
        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Saver();
            //vlozenePlatno.SerializujBinarne();
        }

        // Open tlacitko ...
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Loader();
            //vlozenePlatno.DeserializujBinarne();
        }

        // Print Preview tlacitko ...
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.PreviewBeforePrinting(0.25f);
        }

        // Print tlacitko ...
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.PrintMe();
        }

        // Export To tlacitko ...
        private void exportToJpgpngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.ExportTo();
        }



        #region Zadavani rozmeru planu
        // Rozmery platna na DropDownButton ... pro jednotlive polozky tohoto DropDown tlacitka
        private void A3PortraitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Rámeček = true;
            vlozenePlatno.Ax = 2970;
            vlozenePlatno.Ay = 4200;
            Unmark();
            A3PortraitToolStripMenuItem.Checked = true;
            FrameToolStripDropDownButton.Text = A3PortraitToolStripMenuItem.Text; //"Plán 29,7m x 42m";
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }

        private void A3LandcapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Rámeček = true;
            vlozenePlatno.Ax = 4200;
            vlozenePlatno.Ay = 2970;
            Unmark();
            A3LandcapeToolStripMenuItem.Checked = true;
            FrameToolStripDropDownButton.Text = A3LandcapeToolStripMenuItem.Text; //"Plán 42m x 29,7m";
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }

        private void A4PortraitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Rámeček = true;
            vlozenePlatno.Ax = 2100;
            vlozenePlatno.Ay = 2970;
            Unmark();
            A4PortraitToolStripMenuItem.Checked = true;
            FrameToolStripDropDownButton.Text = A4PortraitToolStripMenuItem.Text;   //"Plán 21m x 29,7m";
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);
        }


        private void A4LandcapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Rámeček = true;
            vlozenePlatno.Ax = 2970;
            vlozenePlatno.Ay = 2100;
            Unmark();
            A4LandcapeToolStripMenuItem.Checked = true;
            FrameToolStripDropDownButton.Text = A4LandcapeToolStripMenuItem.Text; //"Plán 29,7m x 21m";
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);

        }

        private void CustumSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RozmerPlanuForm customSizeWindow = new RozmerPlanuForm())
            {
                if (CustomPlanSizeString != "")
                {
                    customSizeWindow.XdimensionTextBox.Text = CustomX.ToString();
                    customSizeWindow.YdimensionTextBox.Text = CustomY.ToString();

                }
                DialogResult dr = customSizeWindow.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    vlozenePlatno.Rámeček = true;
                    vlozenePlatno.Ax = (int)(customSizeWindow.x * 100);
                    vlozenePlatno.Ay = (int)(customSizeWindow.y * 100);
                    CustomX = customSizeWindow.x;
                    CustomY = customSizeWindow.y;
                    CustomPlanSizeString = "Vlastní plán " + customSizeWindow.x.ToString() + "m x " + customSizeWindow.y.ToString() + "m";
                    FrameToolStripDropDownButton.Text = CustomPlanSizeString;
                    CustumSizeToolStripMenuItem.Text = CustomPlanSizeString;
                    Unmark();
                    CustumSizeToolStripMenuItem.Checked = true;

                }
                else
                {
                    Unmark2();

                }

            }
            vlozenePlatno.Focus();
            vlozenePlatno.Redraw(true);

        }

        // pomocna metoda k un-check v tool strip DropDownButton - vsem polozkam Item
        private void Unmark()
        {
            object ob1;
            object ob2;
            ToolStripMenuItem b;
            foreach (object a in FrameToolStripDropDownButton.DropDownItems)
            {
                ob1 = a.GetType();
                ob2 = typeof(ToolStripSeparator);

                if (ob1 != ob2)
                {
                    b = (ToolStripMenuItem)a;
                    b.Checked = false;
                }
            }
        }
        // odznaci ve Vlastni rozmeru to co je potreba ... a oznaci co je treba
        private void Unmark2()
        {
            object ob1;
            object ob2;
            ToolStripMenuItem b;
            foreach (object a in FrameToolStripDropDownButton.DropDownItems)
            {
                ob1 = a.GetType();
                ob2 = typeof(ToolStripSeparator);

                if (ob1 != ob2)
                {
                    b = (ToolStripMenuItem)a;
                    //b.Checked = false;
                    if (b.Checked)
                    {
                        FrameToolStripDropDownButton.Text = b.Text;
                        if (b.Name == "CustumSizeToolStripMenuItem")
                        {
                            FrameToolStripDropDownButton.Text = CustomPlanSizeString;
                            b.Text = CustomPlanSizeString;
                        }
                    }
                }
            }
        }

        private void OffFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Unmark();
            OffFrameToolStripMenuItem.Checked = true;
            vlozenePlatno.Rámeček = false;
            FrameToolStripDropDownButton.Text = "Rozměry plánu vypnuty";
            vlozenePlatno.Redraw(true);

        }

       

        private void YsnapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Fit2grid = true;
            SnapToolStripDropDownButton.Text = "Přichytávat";
            YsnapToolStripMenuItem.Checked = true;
            NsnapToolStripMenuItem.Checked = false;

           
        }

        private void NsnapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlozenePlatno.Fit2grid = false;
            SnapToolStripDropDownButton.Text = "Nepřichytávat";
            NsnapToolStripMenuItem.Checked = true;
            YsnapToolStripMenuItem.Checked = false;
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
