using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zahrada.PomocneTridy;
using Zahrada.OdvozenyPropertyGrid;


namespace Zahrada
{
    public partial class Nastroje : UserControl
    {
        #region Clenske promenne tridy Nastroje
        //jedina promenna je objekt Platno
        private Platno mojeplatno;
        private ToolStrip nalezenyToolStripvMainForm;
        private ToolStripButton nalezenyUndoBtn;
        private ToolStripButton nalezenyRedoBtn;




        #endregion

        #region Konstruktor tridy Nastroje
        public Nastroje()
        {

            InitializeComponent();
           // panelVpruvodci.HorizontalScroll.Enabled = false;
            //pruvodce.HorizontalScroll.Enabled = false;
            //NajdiUndoReodBtnsVmainForm();
        }


        /*
        public void MyInit()
        {
            mbrPropertyGrid.PropertyItemCategory catItem;
            catItem = new mbrPropertyGrid.PropertyItemCategory("Hlavni kategorie");
            //mujPropertyGrid.CategoryAdd("Hlavni k", catItem);
            
           
        }
        */

        #endregion

        #region Obsluha udalosti na User Control  Platno

        /// <summary>
        /// Metoda pouzita v hlavnim okne MainForm pro sparovani tridy Nastroje s tridou Platno
        /// </summary>
        // v MainForm potom pro konkretni instanci tridy vlozenyToolBox nastavim konkretni instanci tridy vlozenePlatno
        public void SetPlatno(Platno platno)
        {
            this.mojeplatno = platno;
            // prirazuju 2 nove udalosti pri konstrukci MainForm
            this.mojeplatno.OptionChanged += new OptionChangedEventHandler(OnOptionChanged);
            this.mojeplatno.ObjectSelected += new ObjectSelectedEventHandler(OnObjectSelected);
            //mujFilteredPropertyGrid.SelectedObject = mojeplatno;
            //mujFilteredPropertyGrid.Refresh();
            
        }


        // po klikani na objekt - nastavim vlastnost objektu jako "vybrany" a nastavim viditelnost tlacitek
        private void OnOptionChanged(object sender, OptionEventArgs e)
        {
            if (e.option == "select")
            {
                // zde donastavit enabled/disabled tlacitek
                DeselectAll();
                // SelectBtn.Checked = true; // select button je navic a zbytecny

            }
        }



        #endregion

        #region Obsluha Property Gridu - obou dvou
        // obsluha mensiho Property gridu
        private AttributeCollection ParseAttributes(string[] categorynames)
        {
            if (categorynames == null) return null;
            Attribute[] attributes = new Attribute[categorynames.Length];
            for (int iattribute = 0; iattribute < categorynames.Length; iattribute++)
            {
                attributes[iattribute] = new CategoryAttribute(categorynames[iattribute]);
            }
            return new AttributeCollection(attributes);
        }

        // obsluha mensiho Property gridu
        private string[] ParseText(string Text)
        {
            return Text.Length > 0 ? Text.Replace("  ", "").Split(new char[] { ',' }) : null;
        }
        
        
        // volano v ObjectSelected ...
        public void UpravViditelnostUndoRedoTlacitek(PropertyEventArgs e)
        {
            nalezenyUndoBtn.Enabled = e.undoable;
            nalezenyRedoBtn.Enabled = e.redoable;


        }

        // pri inicializaci si hledam 2 tlacitka v MainForm ... volano z MainForm
        public void NajdiUndoReodBtnsVmainForm()
        {
            var najdiToolStripVMainForm = Parent.Controls.Find("toolStrip1", true);
            nalezenyToolStripvMainForm = (ToolStrip)najdiToolStripVMainForm.First();    

            var najdiUndoBtnvToolstrip = nalezenyToolStripvMainForm.Items.Find("undoToolStripButton", true);
            nalezenyUndoBtn = (ToolStripButton)najdiUndoBtnvToolstrip.First();           

            var najdiRedoBtnvToolStrip = nalezenyToolStripvMainForm.Items.Find("redoToolStripButton", true);
            nalezenyRedoBtn = (ToolStripButton)najdiRedoBtnvToolStrip.First();
          
        }

        // po klikani na objekt - obsluha Property Gridu a nastavuji viditelnost tlacitek
        private void OnObjectSelected(object sender, PropertyEventArgs e)
        {

            if (e.ele.Length == 0)
            {
                mujPropertyGrid.SelectedObject = sender; // zobrazuje vlastnosti platna                     
                mujFilteredPropertyGrid.SelectedObject = sender;
                try
                {   // na platno  si potlacuju nektere vlastnosti ....
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Plátno - popis,  Seznam elementů"));
                    //mujFilteredPropertyGrid.HiddenAttributes = ParseAttributes(ParseText("Chování, Rozložení, Vzhled, Fokus, Graphics, Usnadnění, Data, Debug "));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt
                    //mujFilteredPropertyGrid.HiddenAttributes = ParseAttributes(ParseText("")); // pozor - navraceni zpet musi byt
                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(aex.Message);

                }
                catch { }

            }
            else
            {
                mujPropertyGrid.SelectedObject = e.ele.First(); // kdyz - (SelectedObjectS umi zobrazit vybrane objekty vsechny) - jinak zobrazi vlastnosti vybraneho objektu 
                mujFilteredPropertyGrid.SelectedObject = e.ele.First();
                try
                {
                    // vyber urcitych vlastnosti elementu - pro zobrazeni v PG:
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Rozměry,  Vzhled"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt

                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(aex.Message);

                }
                catch { }
            }
            UpravViditelnostUndoRedoTlacitek(e);
            

        }

        // reakce na zmeny, ktere provadim v PropertyGridu
        private void mujPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mojeplatno.PropertyChanged();
            this.mojeplatno.Refresh();
        }

        private void mujFilteredPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mojeplatno.PropertyChanged();
            this.mojeplatno.Refresh();
        }

        #endregion

        #region Obsluha udalosti Click na tlacitka v casti VYTVORIT

        // Cara
        private void lineBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            lineBtn.BackColor = Color.White;
            mojeplatno.option = "LINE";
            mojeplatno.krivka = false;
        }

        // slepa ulicka - vertikala
        /*
        private void verticalBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            verticalBtn.BackColor = Color.White;
            mojeplatno.option = "VLINE";
        }
        */

        // Polygon
        private void polygonBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            polygonBtn.BackColor = Color.White;
            mojeplatno.option = "POLY";
            mojeplatno.krivka = true;
        }

        // Volna ruka
        private void freeHandBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            freeHandBtn.BackColor = Color.White;
            mojeplatno.option = "PEN";
            mojeplatno.krivka = true;

        }

        // Obdelnik
        private void rectBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            rectBtn.BackColor = Color.White;
            mojeplatno.option = "DR";
            mojeplatno.krivka = false;

        }

        // Zaulaceny obdelnik
        /*
        private void rrectBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            rrectBtn.BackColor = Color.White;
            mojeplatno.option = "DRR";

        }
        */

        // Elipsa - kruznice
        private void circBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            circBtn.BackColor = Color.White;
            mojeplatno.option = "ELL";
            mojeplatno.krivka = false;
        }


        // Oblouk
        private void arcBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            arcBtn.BackColor = Color.White;
            mojeplatno.option = "ARC";
            mojeplatno.krivka = false;
        }


        // Jednoduchy text
        private void simpleTextBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            simpleTextBtn.BackColor = Color.White;
            mojeplatno.option = "STB";
            mojeplatno.krivka = false;
        }
                 

        // vkladani obrazku
        private void imageBtn_Click(object sender, EventArgs e)
        {
            DeselectAll();
            imageBtn.BackColor = Color.White;            
            mojeplatno.option = "IB";
            mojeplatno.krivka = false;

        }




                #endregion

        #region Pomocne a ostatni metody

        private void DeselectAll()
        {
            // obsluha viditelnosti tlacitek
            List<Button> seznamTlacitek = new List<Button>()
            { lineBtn, rectBtn, circBtn, simpleTextBtn, arcBtn,
                polygonBtn, freeHandBtn, imageBtn, groupBtn, deGroupBtn,
               mirrorXbtn, toFrontBtn, toBackBtn, deleteBtn, copyBtn };

            foreach (Button b in seznamTlacitek)
            {
                b.BackColor = Color.Transparent;
            }

            

        }










        #endregion

        #region Klik na Tab Control
        private void tabControl_Click(object sender, EventArgs e)
        {
            // toto byl posledni pokus a neuspesny
            /*
            ArrayList list = mojeplatno.GetElements;
            if (list != null)
            {                
               
                
                if (list.Count > 0 && (list[list.Count-1].GetType() != typeof(Platno)))
                {
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Rozměry,  Vzhled"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt

                }
                else
                {
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Plátno - popis,  Seznam elementů"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt

                }

            }

            */

            /*
            if (list != null)
            {
                int a = list.Count;
                object obj;

                if (a == 0)
                    obj = list[0];
                else obj = list[a - 1];
                
                //mujFilteredPropertyGrid.SelectedObject = (a - 1);
                if (obj.GetType() == typeof(Platno))
                {
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Plátno - popis,  Seznam elementů"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt
                   
                }
                else
                {
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("1,  Pozice,  Rozměry,  Vzhled"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navracebi zpet musi byt

                }

            }
            */




        }

        #endregion

        private void groupBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.GroupSelected();
        }

        private void deGroupBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.DeGroupSelected();
        }

        private void toFrontBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.MoveFront();
        }

        private void toBackBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.MoveBack();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.RmSelected();
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.CpSelected();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}

