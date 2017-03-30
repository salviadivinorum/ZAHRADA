using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// poznamka 2



namespace Zahrada
{
    public partial class UserControlNastroje : UserControl
    {
        #region Clenske promenne tridy Nastroje
        //jedina promenna je objekt Platno
        private Platno mojeplatno;
        private ToolStrip nalezenyToolStripvMainForm;
        private ToolStripButton nalezenyUndoBtn;
        private ToolStripButton nalezenyRedoBtn;




        #endregion

        #region Konstruktor tridy Nastroje
        public UserControlNastroje()
        {
            InitializeComponent();
            PruvodceListBox.SelectedIndex = 0;          
        }
        

        

        #endregion

        #region Obsluha udalosti na User Control  Platno

        /// <summary>
        /// Metoda pouzita v hlavnim okne MainForm pro sparovani tridy Nastroje s tridou Platno
        /// </summary>
        // v MainForm potom pro konkretni instanci tridy vlozenyToolBox nastavim konkretni instanci tridy vlozenePlatno
        public void SetPlatno(Platno platno)
        {
            mojeplatno = platno;           
            mojeplatno.OptionChanged += new OptionChangedEventHandler(OnOptionChanged);
            mojeplatno.ObjectSelected += new ObjectSelectedEventHandler(OnObjectSelected);
            
        }


        // po klikani na objekt - nastavim vlastnost objektu jako "vybrany" a nastavim viditelnost tlacitek
        private void OnOptionChanged(object sender, OptionEventArgs e)
        {
            if (e.option == "select")
            {
                // zde donastavit enabled/disabled tlacitek
                DeselectAllButtons();  
            }
        }



        #endregion

        #region Obsluha meho upraveneho Custom Property Gridu
        // obsluha meho Custom Property gridu
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
                mujFilteredPropertyGrid.SelectedObject = sender;
                try
                {   // na platno si potlacuju nektere vlastnosti podle Category !!! ....
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("Element,  Plán - popis"));                    
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - navraceni zpet musi byt
                   
                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(aex.Message);
                }
                catch { }

            }
            else
            {                
                mujFilteredPropertyGrid.SelectedObject = e.ele.First();  // kdyz - (SelectedObjectS umi zobrazit vybrane objekty vsechny) - jinak zobrazi vlastnosti vybraneho objektu 
                try
                {
                    // vyber urcitych vlastnosti elementu - pro zobrazeni v PG - opet podle Category !!!:
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("Element,  Vzhled"));
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
        /*
        private void mujPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mojeplatno.PropertyChanged();
            this.mojeplatno.Refresh();
        }
        */

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
            DeselectAllButtons();
            //lineBtn.BackColor = Color.FromArgb(Color.Gray);
            lineBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "LINE";
            mojeplatno.krivka = true;
        }        

        // Polygon
        private void polygonBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();            
            polygonBtn.BackColor = SystemColors.GradientActiveCaption; 
            mojeplatno.option = "POLY";
            mojeplatno.krivka = true;
        }

        // Volna ruka
        private void freeHandBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            freeHandBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "PEN";
            mojeplatno.krivka = true;

        }

        // Obdelnik
        private void rectBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            rectBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "DR";
            mojeplatno.krivka = false;

        }       

        // Elipsa - kruznice
        private void circBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            circBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "ELL";
            mojeplatno.krivka = false;
        }       


        // Jednoduchy text
        private void simpleTextBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            simpleTextBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "STB";
            mojeplatno.krivka = false;
        }
                 

        // vkladani obrazku
        private void imageBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            imageBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "IB";
            mojeplatno.krivka = false;

        }




        #endregion

        #region Pomocne a ostatni metody
        private void DeselectAllButtons()
        {
            // obsluha viditelnosti tlacitek
            List<Button> seznamTlacitek = new List<Button>()
            { lineBtn, rectBtn, circBtn, simpleTextBtn, polygonBtn, freeHandBtn, imageBtn, groupBtn, deGroupBtn,
               toFrontBtn, toBackBtn, deleteBtn, copyBtn };

            foreach (Button b in seznamTlacitek)
            {
                b.BackColor = Color.Transparent;
            }           

        }

        // Musim aktualizovat muj PropertyGrid po stisku na Tab Vlastnosti
        private void tabControlProNastroje_MouseUp(object sender, MouseEventArgs e)
        {
            mojeplatno.PushSelectionToShowInCustomGrid();
        }
        #endregion

        #region Klikani na tlacitka v Nastroje
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


        private void btnEscape_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            mojeplatno.ForceEsc();
        }

        private void btnVybratVse_Click(object sender, EventArgs e)
        {
            foreach (Ele el in mojeplatno.shapes.List)
            {
                el.selected = true;
            }
            mojeplatno.Redraw(true);
        }

        #endregion

        #region Stisk ESC pri volbe na Nastrojich
        // stisk ESC behem kresleni ....
        private void tabControlProNastroje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape & tabControlProNastroje.SelectedIndex == 0)
            {
                DeselectAllButtons();
                foreach (Ele el in mojeplatno.shapes.List)
                {
                    el.selected = false;
                }
                mojeplatno.Redraw(true);
                mojeplatno.ChangeStatus("selrect");
                mojeplatno.ChangeOption("select");
                mojeplatno.Redraw(true);

            }
            if (e.KeyCode == Keys.Escape & tabControlProNastroje.SelectedIndex == 1)
            {
                mojeplatno.Focus();
            }


        }
        #endregion

        #region Tab Pruvodce - navrhem ... povidani
        // na obsluhu Pruvodce noveho
        private void PruvodceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = PruvodceListBox.SelectedIndex;
            if (ind == 0)
            {
                PruvodceLabel1.Text = "A) Zakreslení stávajícího stavu";
                PruvodceTextBox.Text = "a) Tvar pozemku a světové strany" +
                    Environment.NewLine + Environment.NewLine +
                    "b) Oplocení pozemku" +
                    Environment.NewLine + Environment.NewLine +
                    "c) Stavby na pozemku nebo na jeho hranici" +
                    Environment.NewLine + Environment.NewLine +
                    "d) Zpevněné plochy na pozemku" +
                    Environment.NewLine + Environment.NewLine +
                    "e) Stávající rostliny na pozemku";
            }

            if (ind == 1)
            {
                PruvodceLabel1.Text = "B) Zakreslení nového stavu ";
                PruvodceTextBox.Text = "a) Nové oplocení, stavby a zpevněné plochy" +
                    Environment.NewLine + Environment.NewLine +
                    "b) Vybrat Klíčové a Vedlejší rostliny" +
                    Environment.NewLine + Environment.NewLine +
                    "c) Rozmístit plánované rostliny na pozemku";
            }

            if (ind == 2)
            {
                PruvodceLabel1.Text = "C) Návrh základního motivu";
                PruvodceTextBox.Text = "FORMÁLNÍ TYP ZAHRADY" +
                    Environment.NewLine +
                    "------------------------------------------------------------" +
                    Environment.NewLine +
                    "a) PRAVOÚHLÉ tvary" +
                    Environment.NewLine +
                    "- objekty budou osazeny kolmo na převažující směr zahrady" +
                    Environment.NewLine + Environment.NewLine +
                    "b) DIAGONÁLNÍ tvary" +
                    Environment.NewLine +
                    "- ojekty budou osazeny pod úhlem k některé ze stran zahrady" +
                    Environment.NewLine + Environment.NewLine +
                    "c) KRUHOVÉ tvary" +
                    Environment.NewLine +
                    "- na zahradě převažují objekty kruhového nebo elipsovitého tvaru" +
                    Environment.NewLine + Environment.NewLine + Environment.NewLine +
                    "NEFORMÁLNÍ TYP ZAHRADY" +
                    Environment.NewLine +
                    "------------------------------------------------------------" +
                    Environment.NewLine +
                    "a) UDRŽOVATELNÁ" +
                    Environment.NewLine +
                    "- na zahradě budou převažovat volné tvary, v mezích možností všechny živé prvky budou udržovatelné" +
                    Environment.NewLine + Environment.NewLine +
                    "b) BEZÚDRŽBOVÁ" +
                    Environment.NewLine +
                    "- navržená zahrada bude pokud možno maximálně bezúdržbová";


            }

            if (ind == 3)
            {
                PruvodceLabel1.Text = "D) Návrh funkce zahrady";
                PruvodceTextBox.Text =
                    "a) OKRASNÁ - důraz na estetiku" +
                    Environment.NewLine + Environment.NewLine +
                    "b) ZELENINOVÁ - důraz na užitkovost" +
                    Environment.NewLine + Environment.NewLine +
                    "c) RELAXAČNÍ - důraz na doplňkové prvky" +
                    Environment.NewLine + Environment.NewLine +
                    "d) KOMBINACE v různém poměru";

            }

            if (ind == 4)
            {
                PruvodceLabel1.Text = "E) Zohlednit důležité podmínky";
                PruvodceTextBox.Text =
                    "a) Orientace pozemku ke světovým stranám" +
                    Environment.NewLine + Environment.NewLine +
                    "b) Pozemek svažitý, jsou nutné terénní zlomy" +
                    Environment.NewLine + Environment.NewLine +
                    "c) Odtékání srážkových a podzemních vod" +
                    Environment.NewLine + Environment.NewLine +
                    "d) Pohyb zvířat na pozemku";

            }

            if (ind == 5)
            {
                PruvodceLabel1.Text = "F) Předzahrádka před domem";
                PruvodceTextBox.Text =
                    "a) MINIMÁLNÍ ÚDRŽBA" +
                    Environment.NewLine +
                    "- např. jen trávník nebo zpevněné plochy" +
                    Environment.NewLine + Environment.NewLine +
                    "b) VYŠŠÍ NÁROKY NA ÚDRŽBU" +
                    Environment.NewLine +
                    "- např. květinové záhony, nižší keře a stromy" +
                    Environment.NewLine + Environment.NewLine +
                    "c) FUNKCE STÍNĚNÍ DOMU" +
                    Environment.NewLine +
                    "- převážně stínění domu a odhlučnění přilehlých staveb a komunikace";

            }
        }

        #endregion


       
    }
}

