using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Zahrada
{
    /// <summary>
    /// Uzivatelsky ovladaci prvek nad tridou Platno
    /// </summary>
    public partial class Nastroje : UserControl
    {
        #region Clenske promenne tridy Nastroje
        
        private Platno mojeplatno;
        // Trida Nastroje sinajde a pak uchovava ovladaci prvky nadrazeneho formulare HlavniForm
        private ToolStrip nalezenyToolStripvMainForm;
        private ToolStripButton nalezenyUndoBtn;
        private ToolStripButton nalezenyRedoBtn;
        private List<string> rbuttons = new List<string>();

        #endregion

        #region Konstruktor tridy Nastroje
        public Nastroje()
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

        #region Obsluha meho upraveneho Filtered Property Gridu

        // obsluha meho Property gridu
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
                

        // po klikani na objekt - obsluha Property Gridu a nastavuji viditelnost tlacitek
        // timto lze pozdeji zviditelnit VYBRANE vlastnosti v Property Gridu - karta Vlastnosti !
        private void OnObjectSelected(object sender, PropertyEventArgs e)
        {
            if (e.ele.Length == 0)
            {                     
                mujFilteredPropertyGrid.SelectedObject = sender;
                try
                {   // na platno si potlacuju nektere vlastnosti podle Category !!! ....
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("Element,  Plán - popis"));                    
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - na vraceni zpet musi byt
                   
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
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("Element,  Vzhled"));
                    mujFilteredPropertyGrid.Refresh();
                    mujFilteredPropertyGrid.BrowsableAttributes = ParseAttributes(ParseText("")); // pozor - na vraceni zpet musi byt

                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(aex.Message);
                }
                catch { }
            }
            UpravViditelnostUndoRedoTlacitek(e);
            

        }

       
        private void mujFilteredPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.mojeplatno.PropertyChanged();
            this.mojeplatno.Refresh();
        }

        #endregion

        #region Obsluha udalosti Click na tlacitka v casti VYTVORIT + jednoducha Napoveda dole ve Status Baru

        // Cara
        public void lineBtn_Click(object sender, EventArgs e)
        {            
            DeselectAllButtons();
            //lineBtn.BackColor = Color.FromArgb(Color.Gray);
            lineBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "LINE";
            mojeplatno.krivka = true;
            mojeplatno.infoStatLabel.Text = "Přímá čára - pro opakování akce možno použít DVOJKLIK levým tlačítkem myši na již nakreslenou čáru";
        }        

        // Polygon
        public void polygonBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();            
            polygonBtn.BackColor = SystemColors.GradientActiveCaption; 
            mojeplatno.option = "POLY";
            mojeplatno.krivka = true;
            mojeplatno.infoStatLabel.Text = "Polygon - kreslený z rovných čar. NOVVÝ BOD = při stisku levého tlačítka myši stisknout klávesu A";

        }

        // Volna ruka
        public void freeHandBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            freeHandBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "PEN";
            mojeplatno.krivka = true;
            mojeplatno.infoStatLabel.Text = "Polygon - kreslený volným tahem myši";

        }

        // Obdelnik
        public void rectBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            rectBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "DR";
            mojeplatno.krivka = false;
            mojeplatno.infoStatLabel.Text = "Obdélník - myší vybrat umístění v plánu - z horního levého rohu - do spodního pravého rohu";

        }       

        // Elipsa - kruznice
        public void circBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            circBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "ELL";
            mojeplatno.krivka = false;
            mojeplatno.infoStatLabel.Text = "Elipsa - myší vybrat umístění v plánu - z horního levého rohu - do spodního pravého rohu";
            //infoStatLabel.Text = "Elipsa";
        }       


        // Jednoduchy text
        public void simpleTextBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            simpleTextBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "STB";
            mojeplatno.krivka = false;
            mojeplatno.infoStatLabel.Text = "Vložit jednoduchý text - myší vybrat oblast kam umístit text - poté se zobrazí dialogové okno";
        }
                 

        // vkladani obrazku
        public void imageBtn_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            imageBtn.BackColor = SystemColors.GradientActiveCaption;
            mojeplatno.option = "IB";
            mojeplatno.krivka = false;
            mojeplatno.infoStatLabel.Text = "Vložit zahradní prvek z knihovny - myší vybrat obdélníkovou oblast na plánu - poté se zobrazí knihovna prvků";

        }




        #endregion      

        #region Klikani na tlacitka v Nastroje

        public void groupBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.GroupSelected();
        }

        public void deGroupBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.DeGroupSelected();
        }

        public void toFrontBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.MoveFront();
        }

        public void toBackBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.MoveBack();
        }

        public void deleteBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.RmSelected();
        }

        public void copyBtn_Click(object sender, EventArgs e)
        {
            mojeplatno.CpSelected();
        }


        public void btnEscape_Click(object sender, EventArgs e)
        {
            DeselectAllButtons();
            mojeplatno.ForceEsc();
        }

        public void btnVybratVse_Click(object sender, EventArgs e)
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
                mojeplatno.infoStatLabel.Text = "";
                mojeplatno.Redraw(true);

            }
            if (e.KeyCode == Keys.Escape & tabControlProNastroje.SelectedIndex == 1)
            {
                mojeplatno.Focus();
            }
            
        }
        #endregion

        #region Karta Pruvodce navrhem ... povidani
        
        // pro obsluhu Pruvodce
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

        #region Karta Seznam prani - obsluha zatrhnutych Radiobutonů
        // ulozit checked RButtony
        public void CheckedRBSave()
        {


            for (int r = 6; r <= 34; r++)
            {
                Control ct = TableLayoutPanelSeznamPrani.GetControlFromPosition(1, r);

                if (ct != null)
                {
                    if (ct.GetType() == typeof(Panel))
                    {
                        Panel pa = (Panel)ct;
                        foreach (Control co in pa.Controls)
                        {
                            RadioButton f = (RadioButton)(co);
                            if (f.Checked)
                            {

                                rbuttons.Add(f.Name);

                            }

                        }

                    }
                }

            }

            mojeplatno.shapes.listradiobuttonu = rbuttons;
        }

        // nacist checke Rbuttony
        public void CheckRBLoad2()
        {



            foreach (string b in mojeplatno.shapes.listradiobuttonu)
            {
                //jmenorb = "radioButton" + i.ToString();


                if (radioButton1.Name == b) radioButton1.Checked = true;
                if (radioButton2.Name == b) radioButton2.Checked = true;
                if (radioButton3.Name == b) radioButton3.Checked = true;
                if (radioButton4.Name == b) radioButton4.Checked = true;
                if (radioButton5.Name == b) radioButton5.Checked = true;
                if (radioButton6.Name == b) radioButton6.Checked = true;
                if (radioButton7.Name == b) radioButton7.Checked = true;
                if (radioButton8.Name == b) radioButton8.Checked = true;
                if (radioButton9.Name == b) radioButton9.Checked = true;
                if (radioButton10.Name == b) radioButton10.Checked = true;
                if (radioButton11.Name == b) radioButton11.Checked = true;
                if (radioButton12.Name == b) radioButton12.Checked = true;
                if (radioButton13.Name == b) radioButton13.Checked = true;
                if (radioButton25.Name == b) radioButton25.Checked = true;
                if (radioButton27.Name == b) radioButton27.Checked = true;
                if (radioButton28.Name == b) radioButton28.Checked = true;
                if (radioButton29.Name == b) radioButton29.Checked = true;
                if (radioButton30.Name == b) radioButton30.Checked = true;
                if (radioButton31.Name == b) radioButton31.Checked = true;
                if (radioButton32.Name == b) radioButton32.Checked = true;
                if (radioButton33.Name == b) radioButton33.Checked = true;
                if (radioButton34.Name == b) radioButton34.Checked = true;
                if (radioButton35.Name == b) radioButton35.Checked = true;
                if (radioButton36.Name == b) radioButton36.Checked = true;
                if (radioButton37.Name == b) radioButton37.Checked = true;
                if (radioButton38.Name == b) radioButton38.Checked = true;
                if (radioButton39.Name == b) radioButton39.Checked = true;
                if (radioButton40.Name == b) radioButton40.Checked = true;
                if (radioButton41.Name == b) radioButton41.Checked = true;
                if (radioButton43.Name == b) radioButton42.Checked = true;
                if (radioButton43.Name == b) radioButton43.Checked = true;
                if (radioButton44.Name == b) radioButton44.Checked = true;
                if (radioButton45.Name == b) radioButton45.Checked = true;
                if (radioButton46.Name == b) radioButton46.Checked = true;
                if (radioButton47.Name == b) radioButton47.Checked = true;
                if (radioButton48.Name == b) radioButton48.Checked = true;
                if (radioButton49.Name == b) radioButton49.Checked = true;
                if (radioButton50.Name == b) radioButton50.Checked = true;
                if (radioButton51.Name == b) radioButton51.Checked = true;
                if (radioButton52.Name == b) radioButton52.Checked = true;
                if (radioButton53.Name == b) radioButton53.Checked = true;
                if (radioButton54.Name == b) radioButton54.Checked = true;
                if (radioButton55.Name == b) radioButton55.Checked = true;
                if (radioButton56.Name == b) radioButton56.Checked = true;
                if (radioButton57.Name == b) radioButton57.Checked = true;
                if (radioButton58.Name == b) radioButton58.Checked = true;
                if (radioButton59.Name == b) radioButton59.Checked = true;
                if (radioButton60.Name == b) radioButton60.Checked = true;
                if (radioButton61.Name == b) radioButton61.Checked = true;
                if (radioButton62.Name == b) radioButton62.Checked = true;
                if (radioButton63.Name == b) radioButton63.Checked = true;
                if (radioButton64.Name == b) radioButton64.Checked = true;
                if (radioButton65.Name == b) radioButton65.Checked = true;
                if (radioButton66.Name == b) radioButton66.Checked = true;
                if (radioButton67.Name == b) radioButton67.Checked = true;
                if (radioButton68.Name == b) radioButton68.Checked = true;
                if (radioButton69.Name == b) radioButton69.Checked = true;
                if (radioButton70.Name == b) radioButton70.Checked = true;
                if (radioButton71.Name == b) radioButton71.Checked = true;
                if (radioButton72.Name == b) radioButton72.Checked = true;
                if (radioButton73.Name == b) radioButton73.Checked = true;
                if (radioButton74.Name == b) radioButton74.Checked = true;
                if (radioButton75.Name == b) radioButton75.Checked = true;
                if (radioButton76.Name == b) radioButton76.Checked = true;
                if (radioButton77.Name == b) radioButton77.Checked = true;
                if (radioButton78.Name == b) radioButton78.Checked = true;
                if (radioButton79.Name == b) radioButton79.Checked = true;
                if (radioButton80.Name == b) radioButton80.Checked = true;
                if (radioButton81.Name == b) radioButton81.Checked = true;
                if (radioButton82.Name == b) radioButton82.Checked = true;
                if (radioButton83.Name == b) radioButton83.Checked = true;
                if (radioButton84.Name == b) radioButton84.Checked = true;

            }




        }



        public void CheckedRBLoad()
        {

            for (int s = 0; s <= 3; s++)
            {
                for (int r = 0; r <= 34; r++)
                {
                    Control ct = TableLayoutPanelSeznamPrani.GetControlFromPosition(s, r);

                    if (ct != null)
                    {
                        if (ct.GetType() == typeof(Panel))
                        {
                            Panel pa = (Panel)ct;
                            foreach (Control co in pa.Controls)
                            {
                                RadioButton f = (RadioButton)(co);
                                for (int i = 1; i <= 84; i++)
                                {

                                    string b = "radioButton" + i.ToString();
                                    foreach (string button in mojeplatno.shapes.listradiobuttonu)
                                    {
                                        //MessageBox.Show(f.Name.ToString());
                                        if (button == b)
                                        {

                                            f.Checked = true;
                                            // tabControlProNastroje.Refresh();

                                        }

                                    }

                                }






                            }

                        }
                    }


                }


            }





        }
        #endregion        
       
        #region Pomocne a ostatni metody

        // zrusi podbarveni pozadi tlacitek...
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

        // Musim aktualizovat muj PropertyGrid po stisku na karte Vlastnosti
        private void tabControlProNastroje_MouseUp(object sender, MouseEventArgs e)
        {
            mojeplatno.PushSelectionToShowInCustomGrid();


        }

        #endregion

    }
}

