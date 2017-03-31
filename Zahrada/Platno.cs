using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO; 	
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Input;
using Zahrada.OdvozeneTridyEle;
using Zahrada.PomocneTridy;
using System.Linq;

namespace Zahrada
{
    [Serializable]
    public partial class Platno : UserControl
    {       
        
        #region Clenske promenne tridy Platno
        private string status;
        public string option;
        private string redimStatus = "";
        private string msg = "";
        private int startX;
        private int startY;
        public Shapes shapes;
       


        public bool showDebug = true;

        private float _zoom = 1f;
        private bool _A4 = true;
        private int _Ax = 2100;
        private int _Ay = 2970;
       

        private int _dx = 0;
        private int _dy = 0;
        private int startDX = 0;
        private int startDY = 0;
        private int truestartX = 0;
        private int truestartY = 0;

        //PEN TOOL START
        private ArrayList visPenPointList;
        private ArrayList penPointList;
        private int penPrecX;
        private int penPrecY;
        //PEN TOOL END

        // pouziti pro vykreslovani dynamickych objektu
        private Bitmap offScreenBmp;
        // pouziti pro vykreslovani statickych objektu
        private Bitmap offScreenBackBmp;

        // pouziti pro Export to
        private Graphics DCscreen;
        
        
        // Grid mrizka
        public int _gridSize = 0;
        //public bool Fit2grid = true;
        public bool Fit2grid { get; set; } = true;

        //Graphic - zakladni nastaveni System.Drawing
        private CompositingQuality _compositingQuality = CompositingQuality.Default;
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothingMode = SmoothingMode.AntiAlias;
        private InterpolationMode _interpolationMode = InterpolationMode.Default;

        // krsleni obdelniku
        private bool mouseSx;
        private int tempX;
        private int tempY;

        //Print Preview a Print formularove okno
        private NahledTisku nahledForm;

        //RichTextBox formularove okno
        private RichTextBoxForm editorForm;
        public RichTextBox r; // extrahuju si pozdeji pouze RichTextBox z formularoveho okna RTBF

        // Barevnost pera, Tloustka Pera, Barevnost vyplne, Otazka vyplne - zadavani vseobecne pro vsechno co lze
        public Color creationPenColor;
        public float creationPenWidth;
        public Color creationFillColor;
        public bool creationColorFilled;
        public bool creationTextureFilled;
        public bool creationClosed;

        //[Serializable]
        public TextureBrush creationTexturePattern;

        // prirazeni event-handleru mym udalostem na platne (pozdeji budu prirazovat take pro toolbox, ale se zmenymi parametry)
        public event OptionChangedEventHandler OptionChanged;
        public event ObjectSelectedEventHandler ObjectSelected;

        System.Windows.Forms.Cursor addPointCursor = GetCursor("newPoint3.cur", System.Windows.Forms.Cursors.Cross);
        System.Windows.Forms.Cursor delPointCursor = GetCursor("delPoint3.cur", System.Windows.Forms.Cursors.Default);  // v projektu neni nikdy pouzity

        
        public int rozdilSirek;
        public int rozdilVysek;

        public ToolStripComboBox nalezenyZoomCBvToolStrip;
        public ToolStrip nalezenyToolStripvMainForm;
        public ToolStripComboBox nalezenyClosedCBvToolStrip;
        
        public bool uzavrenaKrivka;
        public bool krivka = false;

        // pridano - kvuli polygonu
        ArrayList bb = new ArrayList();

        // pridano ToolTip pro polygon
        private ToolTip tip = new ToolTip();

        //[NonSerialized]
        private Textura textura = new Textura();

        // neco pro mys a mi ukayuje zmenu rozmeru obbjektu pri pohybu mysi...
        private int plusX;
        private int plusY;
        private int pocX;
        private int pocY;
        private int delkaElementu;
        private int celkovaDelkaPenElementu;

        //save/load pomucka
        public bool SaveSuccess = false;
        public bool LoadSucces = false;
        public string jmenoNoveOtevreneho = "";
        private string plneJmenoNoveOtevreneho = "";

        // pro Load/save
        public float kolikNasobneZoom = 1;
        
        #endregion       

        #region Konstruktor tridy Platno
        public Platno()
        {
            InitializeComponent();
            myInit();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); //to je pro optimalizaci vykreslovani
            // Event Handler - obsluha udalosti MouseWheel pro User Control Platno (musel jsem ji k Platnu vytvorit externe a zde ji pridavam)
            MouseWheelHandler.Add(this, MyOnMouseWheel); // ovladaci metoda udalosti - k presnemu zoomovani         
            bb.Add(new PointWrapper(0, 0));
            tip.ForeColor = Color.LightGray;
        }

       
        // moje dodatecna inicializace tridy
        private void myInit()
        {          
            ChangeStatus("");
            option = "select";

            // g je hlavni Graphics
            Graphics g = CreateGraphics();

            // obsluha tiskarny
            PrintDocument pd = new PrintDocument();
            //MessageBox.Show( pd.PrinterSettings.DefaultPageSettings.PrinterResolution.X.ToString() );
            //MessageBox.Show(pd.PrinterSettings.DefaultPageSettings.PrinterResolution.Y.ToString() );

            //g_pr je Graphics pro tisk
            Graphics g_pr = pd.PrinterSettings.CreateMeasurementGraphics();

            SizeF sizef;
            float x_pr, y_pr = 0;

            sizef = g_pr.MeasureString("YourStringHere", Font);
            x_pr = sizef.Width;
             y_pr = sizef.Height;
            //y_pr = Font.Height;

            float x_vi, y_vi = 0;

            sizef = g.MeasureString("YourStringHere", Font);
            x_vi = sizef.Width;
            y_vi = sizef.Height;            
            shapes = new Shapes(g.DpiX * (x_pr / x_vi), g.DpiY * (y_pr / y_vi)); 
            g.Dispose();

            editorForm = new RichTextBoxForm();
            r = editorForm.mujRichTextBox; // tady si vytahuju z formulare pouze ten richtextbox control

            //zakladni nastaveni kreslicich barev/tloustek/vyplnovani
            creationPenColor = Color.Black;
            creationPenWidth = 1f;
            creationFillColor = Color.Black;
            creationColorFilled = false;
            creationTextureFilled = false;

            // zakladni textura mych Pathu            
            Image obr = Properties.Resources.trava_velmi_husta;  
            TextureBrush tBrush = new TextureBrush(obr);
            tBrush.WrapMode = WrapMode.Tile;  

            // pomocnu tridou si nastavuju texturu takto ...
            creationTexturePattern = textura.InicilizujPrviTexturu();

            // skutecne prirazeni (new) udalosti v inicializaci MyInit() prvku platno
            OptionChanged += new OptionChangedEventHandler(OnBasicOptionChanged);
            ObjectSelected += new ObjectSelectedEventHandler(OnBasicObjectSelected);

            offScreenBackBmp = new Bitmap(this.Width, this.Height);
            offScreenBmp = new Bitmap(this.Width, this.Height);            
        }        
        

        #endregion

        #region Vlastnosti pro Graphics, kterym navic nastavuji Category a Description v mem Property Gridu

        // Nastavuji si viditelnost promenne showDebug pro pomocne vypisky pri kresleni
        [CategoryAttribute("Plán - popis"), DescriptionAttribute("Šířka plánu zahrady (cm)")]
        public int Šířka
        {
            get { return _Ax; }
            set { _Ax = value; }
        }

        [CategoryAttribute("Plán - popis"), DescriptionAttribute("Vška plánu zahrady (cm)")]
        public int Výška
        {
            get { return _Ay; }
            set { _Ay = value; }
        }

        [CategoryAttribute("Plán - popis"), DescriptionAttribute("Zobrazit rámeček plánu")]
        public bool Rámeček
        {
            get
            {
                return _A4;
            }
            set
            {
                _A4 = value;
            }
        }



        [Category("Seznam elementů"),Description("Seznam elelemntů na kreslícím plátně")]
        public ArrayList GetElements
        {
            get { return shapes.List; }
        }

        


        [Category("Debug"), Description("ShowDebugInfo")]
        public bool ShowDebug
        {
            get { return showDebug; }
            set { showDebug = value; }
        }

        [Category("Graphics"), Description("Interp.Mode")]
        public InterpolationMode InterpolationMode
        {
            get
            {
                return _interpolationMode;
            }
            set
            {
                _interpolationMode = value;
            }
        }


        [Category("Graphics"), Description("Smooth.Mode")]
        public SmoothingMode SmoothingMode
        {
            get
            {
                return _smoothingMode;
            }
            set
            {
                _smoothingMode = value;
            }
        }

        [Category("Graphics"), Description("Txt.Rend.Hint")]
        public TextRenderingHint TextRenderingHint
        {
            get
            {
                return _textRenderingHint;
            }
            set
            {
                _textRenderingHint = value;
            }
        }

        [Category("Graphics"), Description("Comp.Quality")]
        public CompositingQuality CompositingQuality
        {
            get
            {
                return _compositingQuality;
            }
            set
            {
                _compositingQuality = value;
            }
        }



        [Category("Element"), Description("Plán")]
        public string Typ
        {
            get
            {
                return "Plán";
            }
        }

        [CategoryAttribute("Plán - popis"), DescriptionAttribute("Nastavit barvu pozadí plánu")]
        public Color Pozadí
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
                this.Redraw(true);
            }
        }


        [Category("Plán - popis"), Description("Velikost mřížky plánu")]
        public int Mřížka
        {
            get
            {
                return _gridSize;
            }
            set
            {
                if (value >= 0)
                {
                    _gridSize = value;
                }
                if (_gridSize > 0)
                {
                    this.dx = _gridSize * (int)(this.dx / _gridSize);
                   this.dy = _gridSize * (int)(this.dy / _gridSize);
                }
                this.Redraw(true);
            }
        }


        //[CategoryAttribute("Plán - popis"), DescriptionAttribute("Plán Zoom")]
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                if (value > 0)
                {
                    _zoom = value;
                    this.Redraw(true);
                }
                else
                {
                    _zoom = 1;
                    this.Redraw(true);
                }
            }
        }


        [Category("Plán - popis"), Description("Plán - zvětšení nebo zmenšení")]
        public string Zvětšení
        {
            get { return ((_zoom * 4*100).ToString()+" %"); }
            //set { }
        }
        



        //[CategoryAttribute("Plátno - popis"), DescriptionAttribute("Plátno OriginX")]
        public int dx
        {
            get
            {
                return _dx;
            }
            set
            {
                _dx = value;
            }
        }

        //[CategoryAttribute("Plátno - popis"), DescriptionAttribute("Plátno OriginY")]
        public int dy
        {
            get
            {
                return _dy;
            }
            set
            {
                _dy = value;
            }
        }


         





        #endregion

        #region Metody pro dodatecnou obsluhu udalosti EVENTS nad platnem

        // nedela nic, jen preposila nahoru k objektu, ktery je nad Platnem - Nastroje
        // pouziti je v deklaraci clenske promenne - udalosti - OptionChanged a ObjectSelected
        private void OnBasicOptionChanged(object sender, OptionEventArgs e)
        { }

        private void OnBasicObjectSelected(object sender, PropertyEventArgs e)
        { }

        public void ChangeStatus(string s)
        {
            status = s;

           
        }

        public void ChangeOption(string s)
        {
            option = s;
            // oznam "option" zmenu smerem k poslouchajicim objektum - napr. toolBoxu Nastroje
            OptionEventArgs e = new OptionEventArgs(option);
            OptionChanged(this, e); // vyvola timto udalost event

            

        }

        #endregion

        #region Verejne metody tridy Platno

        // definice kurzoru
        public static System.Windows.Forms.Cursor GetCursor(string a, System.Windows.Forms.Cursor defCur)
        {
            try
            {
                return new System.Windows.Forms.Cursor(a);
            }
            catch
            {
                return defCur;
            }
        }
                

        
        

        #endregion

        #region Undo/Redo metody nad platnem
        public bool UndoEnabled()
        {
            return shapes.UndoEnabled();
        }

        public bool RedoEnabled()
        {
            return shapes.RedoEnabled();
        }

        public void Undo()
        {           
           
            
            shapes.Undo();
            shapes.DeSelect();
            Redraw(true);
        }

        public void Redo()
        {

            
            shapes.Redo();
            shapes.DeSelect();
            Redraw(true);
        }

        #endregion

        #region Metody volane po stisku nejake VOLBY z Nastroje, ToolStrip, MenuStrip


        public void SetPenColor(Color c)
        {
            creationPenColor = c;
            if (shapes.selEle != null)
            {
                shapes.selEle.Pero_barva = c;
            }
        }

        public void SetFillColor(Color c)
        {
            creationFillColor = c;
            if (shapes.selEle != null)
            {
                shapes.selEle.FillColor = c;
            }
        }

        public void SetTexture(TextureBrush t)
        {
            creationTexturePattern = t;
            if (shapes.selEle != null)
            {
                shapes.selEle.FillTexture = t;
            }
        }

        public void SetColorFilled(bool f)
        {
            creationColorFilled = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.ColorFilled = f;
            }
        }

        public void SetTextureFilled(bool f)
        {
            creationTextureFilled = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.TextureFilled = f;
            }
           
        }

        public void SetPenWidth(float f)
        {
            creationPenWidth = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.Pero_šířka = f;
            }
        }

        public void SetClosed(bool f)
        {
            creationClosed = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.EleClosed = f;
            }
        }


        public void GroupSelected()
        {
            shapes.GroupSelected();

            // ukaze vlastnosti Group
            PropertyEventArgs e1 = new PropertyEventArgs(shapes.GetSelectedArray(), shapes.RedoEnabled(), shapes.UndoEnabled());
            ObjectSelected(this, e1); // vyvola novou udalost k tomu kdo posloucha ....


            Redraw(true);
        }


        public void DeGroupSelected()
        {
            shapes.DeGroupSelected();
            // ukaze vlastnosti Group
            PropertyEventArgs e1 = new PropertyEventArgs(shapes.GetSelectedArray(), shapes.RedoEnabled(), shapes.UndoEnabled());
            ObjectSelected(this, e1); // vyvola novou udalost k tomu kdo posloucha ....

            Redraw(true);
        }



        public void RmSelected()
        {

            shapes.RmSelected();
            Redraw(true);
        }

        //Test
        public void CpSelected()
        {
            //this.s.CopySelected(30, 20);
            shapes.CopyMultiSelected(25, 15);
            Redraw(true);
        }

        public void CpSelected(int x, int y)
        {
            shapes.CopyMultiSelected(x, y);
            Redraw(true);
        }

        public void MoveFront()
        {
            shapes.MoveFront();
            Redraw(true);
        }

        public void MoveBack()
        {
            shapes.MoveBack();
            Redraw(true);
        }

        


        #endregion

        #region Metoda REDRAW a k ni pomocne metody 


        // Zakladni nastaveni Grpahics GDI+ plochy - podle toho co jsem si zmenil v Property Gridu
        private void GraphicSetUp(Graphics g)
        {
            g.CompositingQuality = CompositingQuality;
            g.TextRenderingHint = TextRenderingHint;
            g.SmoothingMode = SmoothingMode;
            g.InterpolationMode = InterpolationMode;
            // g.PageUnit = GraphicsUnit.Millimeter; // muzu si nastavit mimiletry - vyuziju pozdeji
        }

        
        // pouze pro testovaci ucely - nevyuzito
        public void saveBmp()
        {
            offScreenBackBmp.Save("test.bmp");
        }
        

        // obsluha zakladnich udalosti Paint na Platno ...
        public void Redraw(Graphics g, bool All)
        {            
            RedrawBody(g, All);             
        }
        
        // Redraw volani pro vynucene prekresleni Platna po nejake akci ....
        public void Redraw(bool All)
        {
            Graphics g = CreateGraphics();
            RedrawBody(g, All);
            g.Dispose(); // uvolnuji prostredky

        }

        // Zaklad pro praci s prekreslevanim pomoci Double Bufferu ....
        /// <summary>
        /// Prekresluje this.shapes pres ovladaci prvek (Platno)
        /// All=true : prekresli cely vsechny objekty
        /// All=false : prekresli pouze vybrane objekty
        /// </summary>

        public void RedrawBody(Graphics g, bool All)
        {
           

            //int kolikjeGrid = gridSize;
            if (Fit2grid & Mřížka > 0)
            {

                startX = Mřížka * (startX / Mřížka);
                startY = Mřížka * (startY / Mřížka);
            }
            
            GraphicSetUp(g);


            if (All)
            {
                // Prekresli vsechny staticke objekty na pozadi
                Graphics backG;
                backG = Graphics.FromImage(offScreenBackBmp);
                GraphicSetUp(backG);
                backG.Clear(BackColor);

                if (BackgroundImage != null)
                    backG.DrawImage(BackgroundImage, 0, 0);

                // Vykresleni Mrizky Grid 
                if (Mřížka > 0)
                {                    
                    Pen myPen = new Pen(Color.LightGray, 0.1f);                   
                    int nX = (int)(Width / (Mřížka * Zoom));
                    for (int i = 0; i <= nX; i++)
                    {
                        backG.DrawLine(myPen, i * Mřížka * Zoom, 0, i * Mřížka * Zoom, Height);
                    }
                    int nY = (int)(Height / (Mřížka * Zoom));
                    for (int i = 0; i <= nY; i++)
                    {
                        backG.DrawLine(myPen, 0, i * Mřížka * Zoom, Width, i * Mřížka * Zoom);
                    }

                    //backG.PageUnit = u;
                    myPen.Dispose(); // uvolnuji zdroje
                }

                // Nakresli NEVYBRANE objekty:                
                shapes.DrawUnselected(backG, dx, dy, Zoom); // posledni hodnota byla Zoom   
                backG.Dispose();
            }

            //AKce pro Double Buffering
            Graphics offScreenDC;
            offScreenDC = Graphics.FromImage(offScreenBmp);
            offScreenDC.TextRenderingHint = TextRenderingHint.AntiAlias;
            offScreenDC.SmoothingMode = SmoothingMode.AntiAlias;
            offScreenDC.Clear(BackColor);


            // Kreslim BackGroundBitmap se statickymi objekty    
            offScreenDC.DrawImageUnscaled(offScreenBackBmp, 0, 0);           

            shapes.DrawSelected(offScreenDC, dx, dy, Zoom); // posledni hodnota byla Zoom


            // Nyni kreslim graficke efekty - Vytvoreni/Vyber/PenPoint a navic A4 ramecek

            //Nakresli cerveny ramecek pro vytvoreni - obdelnik nebo Cara
            if (mouseSx & status == "drawrect")
            {
                Pen myPen = new Pen(Color.Red, 1.5f);
                myPen.DashStyle = DashStyle.Dot;
                myPen.StartCap = LineCap.DiamondAnchor;
                myPen.EndCap = LineCap.ArrowAnchor;

                // toto ukazuje spravne cervenou carku pri tvorbe polygonu .... od posledniho visualniho bodu smerem dal ....
                if (option == "POLY" && penPointList.Count >1)
                {
                    int kolik = visPenPointList.Count; // deklaraci promennych pozdeji presunout do sekce Clenske promenne
                    PointWrapper a = (PointWrapper)visPenPointList[kolik - 1];
                    int xa = a.X;
                    int ya = a.Y;

                    offScreenDC.DrawLine(myPen, (startX+xa+dx)*Zoom, (startY+ya+dy)*Zoom, (tempX + dx) * Zoom, (tempY + dy) * Zoom);
                }


                
                if (option == "LINE")
                {
                    offScreenDC.DrawLine(myPen, (startX + dx) * Zoom, (startY + dy) * Zoom, (tempX + dx) * Zoom, (tempY + dy) * Zoom);
                }

                // budu kreslit zaverecny cerveny obdelnik, pouze kdyz se nejedna o krivku (POLY nebo PEN)
               if (!krivka)   
                {
                    offScreenDC.DrawRectangle(myPen, (startX + dx) * Zoom, (startY + dy) * Zoom, (tempX - startX) * Zoom, (tempY - startY) * Zoom);
                }
                myPen.Dispose();
            }

            //Nakresli zeleny obdelnik pro VYBER objektu
            if (mouseSx & status == "selrect")
            {
                Pen myPen = new Pen(Color.Green, 1.5f);
                myPen.DashStyle = DashStyle.Dash;
                offScreenDC.DrawRectangle(myPen, (startX + dx) * Zoom, (startY + dy) * Zoom, (tempX - startX) * Zoom, (tempY - startY) * Zoom);
                myPen.Dispose();
            }

            // Zde si muzu nastavit podruzne informace pri kresleni .... napr souradnice pohyby kursoru
            // zalezi to natom zda mam get/set vlastnost bool ShowDebug  true/false
            DrawDebugInfo(offScreenDC);

            //Nakresli A4 ramecek 210 x 297 mm a pomocne merici linky po 10mm
            if (Rámeček)
            {
                
                Pen myPen = new Pen(Color.Blue, 0.5f);
                myPen.DashStyle = DashStyle.Dash;

                // vykresleni jednotek
                StringFormat drawFormat = new StringFormat();
                Font drawFont = new Font("Arial", 7);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);

                // nechavam to na 1pixel = 1mm
                // + 20 znaci primarni odstup pri vzkreslovani ramecku
                // nakresli samotny ramecek
                offScreenDC.DrawRectangle(myPen, (dx) * Zoom, (dy) * Zoom, (Šířka) * Zoom, (Výška) * Zoom);

               
                // nakresli cisla nad ramecek
                for (int i =0; i <= Šířka; i=i+10)
                {
                    offScreenDC.DrawLine(myPen, (dx+i)*Zoom, (dy+3)*Zoom, (dx+i)*Zoom, ((dy)*Zoom));

                    // vykresleni souradnic X nad rameckem
                    string metry = (i/100).ToString();
                   
                    if (i == 0)
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx + i) * Zoom) - 15), (((dy) * Zoom) - 10 ));
                    if (((i / 10) % 20) == 0 & (i !=0))
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx + i ) * Zoom) - 6), (((dy) * Zoom) - 20));


                }   
                for (int a = 0; a <= Výška; a = a + 10)
                {
                    offScreenDC.DrawLine(myPen, (dx) * Zoom, (dy + a) * Zoom, (dx + 3) * Zoom, ((dy+ a) * Zoom));
                    
                    // vykresleni souradnic Y nad rameckem
                    string metry = (a / 100).ToString();
                    if (((a / 10) % 20) == 0 & (a != 0))
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx) * Zoom) - 20), (((dy + a) * Zoom) - 6));
                }
                
                myPen.Dispose(); 

            }

            //Kresli Pero Volny styl objekt
            if (penPointList != null)
            {
                Pen myPen = new Pen(Color.Red, 1.5f);
                myPen.DashStyle = DashStyle.Dot;
                myPen.StartCap = LineCap.DiamondAnchor;

                // To ARRAY
                PointF[] myArr = new PointF[visPenPointList.Count];
                int i = 0;
                foreach (PointWrapper p in visPenPointList)
                {
                    myArr[i++] = new PointF((startX + p.X + dx) * Zoom, (startY + p.Y + dy) * Zoom);// p.point;

                }

                
                if (myArr.Length > 1)
                {
                    if (option == "PEN")
                        offScreenDC.DrawCurve(myPen, myArr);
                    if (option == "POLY")
                        offScreenDC.DrawCurve(myPen, myArr, 0); // pohlidal jsem si aby ta krivka mela nulovy uhel (pri polyline)

                }
                
                    
            }



            // Vykresluju buffer se statickymi objekty do Graphics gl            
            g.DrawImageUnscaled(offScreenBmp, 0, 0);
            // uvolnim zdroje
            //DCscreen = offScreenDC;
            offScreenDC.Dispose();
            
        }        

        // budu pouzivat pri pohyby mysi - bude mi to hlasit pozici kurzoru // funguje ale pouzil jsem radeji mouse tool tip
        private void DrawDebugInfo(Graphics g)
        {
            
            //Kontrolni zprava pri Draw ...
            if (ShowDebug)
            {               
                Font tmpf = new Font("Arial", 7);               
                msg = "      dx= " + (plusX).ToString() + " , dy= " + (plusY).ToString();
                g.DrawString(msg, tmpf, new SolidBrush(Color.Gray), new PointF((tempX + dx) * Zoom, (tempY + dy) * Zoom), StringFormat.GenericTypographic);
                tmpf.Dispose();
            }
        }
        
       
        #endregion

        #region Metody pro obsluhu udalosti pro MYS

        public void Platno_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            pocX = (int)(e.X);
            pocY = (int)(e.Y);
            celkovaDelkaPenElementu = 0;          

            startX = (int)((e.X) / Zoom - dx);
            startY = (int)((e.Y) / Zoom - dy);
            
            truestartX = (int)(e.X/Zoom); // spravna prace s prostrednim tlacitkem
            truestartY = (int)(e.Y/Zoom); // spravna prace s prostrednim tlacitkem

            if (e.Button == MouseButtons.Left)
            {
                #region START LEFT MOUSE BUTTON PRESSED
                mouseSx = true; // I start pressing SX

                switch (option)
                {
                    case "select":

                        if (redimStatus != "")
                        {
                            // JSem nad objektem a dělám redim
                            ChangeStatus("redim");// Startuju redim/move akci
                        }
                        else
                        {
                            // Stiskl jsem SX nad volnym prostorem a zacinam selection-rectangle
                            ChangeStatus("selrect");
                      
                        }

                        break;
                    case "PEN":
                        penPointList = new ArrayList();  //resetuje points buffer
                        visPenPointList = new ArrayList();  //resetuje points buffer
                        penPrecX = startX;
                        penPrecY = startY;
                        penPointList.Add(new PointWrapper(0, 0));
                        visPenPointList.Add(new PointWrapper(0, 0));                        
                        break;
                    case "POLY":   
                        penPointList = new ArrayList(); //resetuje points buffer
                        visPenPointList = new ArrayList(); //resetuje points buffer
                        penPointList.Add(new PointWrapper(0, 0));
                        visPenPointList.Add(new PointWrapper(0, 0));                        
                        break;
                    default:

                        // jestlize Option != "select" pak jdu tvorit novy element  - toto je nejdulezitejsi
                        ChangeStatus("drawrect"); 
                        break;
                }
                #endregion
            }
            else if (e.Button == MouseButtons.Right)
            {
              
                #region START RIGHT MOUSE BUTTON PRESSED
                startDX = dx;
                startDY = dy;

                if (shapes.selEle != null & shapes.sRec != null & Cursor == System.Windows.Forms.Cursors.Hand)
                {
                    Point p = new Point();
                    p = (((Platno)sender).PointToScreen(e.Location));                    
                    p.Y = p.Y + 15;
                    contextMenuStripProPlatno.Show(p);
                }
                
                #endregion
            }
            else
            {
                #region START MIDDLE MOUSE BUTTON PRESSED
                // pouze  pro akci PAN
                startDX = dx;
                startDY = dy;
                Cursor = System.Windows.Forms.Cursors.Hand;
            }            
            
            #endregion
        }


        #region CONTEXT MENU na PRAVEM TLACITKU - oblusha zde
        // Co delat kdyz dam na context menu - vymazat
        private void DeleteContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shapes.selEle != null & shapes.sRec != null)
            {
                RmSelected();
                Redraw(true);
            }

        }

        // kopiruje po stisku context menu - copy selected
        private void CopyContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CpSelected();
        }

        private void AllContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Ele el in shapes.List)
            {
                el.selected = true;
            }
            Redraw(true);
        }

        #endregion

        private void Platno_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e)
        {

            string hlaseni;
            if (e.Button == MouseButtons.Left)
            #region left mouse button pressed
            {
                //ShowDebug = true;
                
                if (option == "POLY" & Keyboard.IsKeyDown(Key.A))
                {
                    pocX = e.X;
                    pocY = e.Y;
                }
                

                plusX = (int)((e.X - pocX) / Zoom);
                plusY = (int)((e.Y - pocY) / Zoom);
                delkaElementu = (int)(Math.Sqrt(plusX * plusX + plusY * plusY));

                tip.Active = true;


                // dodatecne info na ToolTipu behem pohybu mysi ....
                // ukazovat dx,dy pri pohybu mysi .... po platne
                if (option == "POLY" || option =="LINE" || option=="PEN")
                {
                    hlaseni = "delka=" + delkaElementu.ToString() + "cm" + Environment.NewLine +
                    "dx=" + plusX.ToString() + " dy=" + plusY.ToString();
                    if(option == "POLY")
                        hlaseni += Environment.NewLine + "A=další bod";
                    if(option == "PEN")
                    {
                        hlaseni ="delka=" + celkovaDelkaPenElementu.ToString()+"cm" + Environment.NewLine +
                    "dx=" + plusX.ToString() + " dy=" + plusY.ToString();
                    }
                    tip.Show(hlaseni, this, e.X + 15, e.Y + 15);
                }                    
                else
                    tip.Show("dx=" + plusX.ToString() + " dy=" + plusY.ToString(), this, e.X + 15, e.Y + 15);

                if (mouseSx)
                {
                    tempX = (int)(e.X / Zoom);
                    tempY = (int)(e.Y / Zoom);

                    if (Fit2grid & Mřížka > 0)
                    {                       
                        tempX = Mřížka * (int)((e.X / Zoom) / Mřížka);
                        tempY = Mřížka * (int)((e.Y / Zoom) / Mřížka);
                    }
                    tempX = tempX - dx;
                    tempY = tempY - dy;

                }

                if (status == "redim")
                {                 


                    int tmpX = (int)(e.X / Zoom - dx);
                    int tmpY = (int)(e.Y / Zoom - dy);
                    int tmpstartX = startX;
                    int tmpstartY = startY;



                    if (Fit2grid & Mřížka > 0)
                    {
                        tmpX = Mřížka * (int)((e.X / Zoom - dx) / Mřížka);
                        tmpY = Mřížka * (int)((e.Y / Zoom - dy) / Mřížka);
                        tmpstartX = Mřížka * (startX / Mřížka);
                        tmpstartY = Mřížka * (startY / Mřížka);     

                        shapes.Fit2Grid(Mřížka);
                        shapes.sRec.Fit2grid(Mřížka);
                        
                    }

                    switch (redimStatus)
                    {
                        case "POLY":
                            // Move vybrán
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.MovePoint(tmpstartX - tmpX, tmpstartY - tmpY);
                               
                            }
                            if (Fit2grid & Mřížka > 0)
                            {
                                shapes.Fit2Grid(Mřížka);
                                shapes.sRec.Fit2grid(Mřížka);
                            }
                            break;
                           



                        case "C":
                            // Move vybrán
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.Move(tmpstartX - tmpX, tmpstartY - tmpY);

                                shapes.sRec.Move(tmpstartX - tmpX, tmpstartY - tmpY);
                            }
                            break;
                        case "ROT":
                            // Rotate vybráno
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.selEle.Rotate(tmpX, tmpY);
                                shapes.sRec.Rotate(tmpX, tmpY);
                            }
                            break;
                        case "ZOOM":
                            // Rotate vybráno
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                if (shapes.selEle is Group)
                                {
                                    ((Group)shapes.selEle).SetZoom(tmpX, tmpY);
                                    shapes.sRec.SetZoom(((Group)shapes.selEle).GrpZoomX, ((Group)shapes.selEle).GrpZoomY);
                                }
                            }
                            break;
                        default:
                            // redim selected
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.selEle.Redim(tmpX - tmpstartX, tmpY - tmpstartY, redimStatus);
                                shapes.sRec.Redim(tmpX - tmpstartX, tmpY - tmpstartY, redimStatus);
                            }
                            break;
                    }

                }
                else
                {
                    if (option == "PEN")
                    {                       
                        //Volná čára - přidávání bodů
                        visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                        if (Math.Sqrt(Math.Pow(penPrecX - tempX, 2) + Math.Pow(penPrecY - tempY, 2)) > 15)
                        {
                            penPointList.Add(new PointWrapper(tempX - startX, tempY - startY));

                            plusX = (int)((e.X - pocX) / Zoom);
                            plusY = (int)((e.Y - pocY) / Zoom);
                            delkaElementu = (int)(Math.Sqrt(plusX * plusX + plusY * plusY));
                            pocX = e.X;
                            pocY = e.Y;
                            celkovaDelkaPenElementu += delkaElementu;

                            penPrecX = tempX;
                            penPrecY = tempY;
                        }                        
                        Redraw(false);                        
                    }

                    // posouva se mi spravne cervene voditko kudy kreslit polyline ... podle posledniho bodu mysi
                    if(option == "POLY")
                    {

                        if (visPenPointList.Count % 2 == 0)
                        {
                            visPenPointList.RemoveAt((visPenPointList.Count - 1));
                            visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));                          
                            Redraw(false);
                        }
                        else
                        {
                            visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));                            
                            Redraw(false);
                        }                       
                    }   
                }

                Redraw(false);
            }
            #endregion
            else
                if (e.Button == MouseButtons.Middle)
            #region MIDDLE Buttun pressed
                {
                    Cursor = System.Windows.Forms.Cursors.Hand;
                    // spravne nastaveno - pohyb A4/A3 papiru po plose pri stisku Prostr. tlacitka
                    dx = (int)((startDX - truestartX)  + (e.X  / Zoom)) ;
                    dy = (int)((startDY - truestartY) + (e.Y) / (Zoom));
                if (Fit2grid & Mřížka > 0)
                    {                    
                        dx = Mřížka * ((dx) / Mřížka); // funguje dobre - prichytava to A4/A3 papir na mrizku
                        dy = Mřížka * ((dy) / Mřížka);
                    }
                    Redraw(true);
                }
            #endregion
            else
            #region NO muse button pressed
            {
                if (option == "select")
                {
                    if (shapes.sRec != null)
                    {
                        string st = shapes.sRec.IsOver((int)(e.X / Zoom) - dx, (int)(e.Y / Zoom - dy));
                        redimStatus = st;

                        switch (st)
                        {
                            case "NEWP":
                                Cursor = System.Windows.Forms.Cursors.SizeAll;
                                Cursor = addPointCursor;
                                break;
                            case "POLY":
                                Cursor = System.Windows.Forms.Cursors.SizeAll;
                                break;
                            case "GRAPH":
                                Cursor = System.Windows.Forms.Cursors.SizeAll;
                                break;
                            case "ROT":
                                Cursor = System.Windows.Forms.Cursors.SizeAll;
                                break;
                            case "C":
                                Cursor = System.Windows.Forms.Cursors.Hand;
                                break;
                            case "NW":
                                Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                                break;
                            case "N":
                                Cursor = System.Windows.Forms.Cursors.SizeNS;
                                break;
                            case "NE":
                                Cursor = System.Windows.Forms.Cursors.SizeNESW;
                                break;
                            case "E":
                                Cursor = System.Windows.Forms.Cursors.SizeWE;
                                break;
                            case "SE":
                                Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                                break;
                            case "S":
                                Cursor = System.Windows.Forms.Cursors.SizeNS;
                                break;
                            case "SW":
                                Cursor = System.Windows.Forms.Cursors.SizeNESW;
                                break;
                            case "W":
                                Cursor = System.Windows.Forms.Cursors.SizeWE;
                                break;
                            case "ZOOM":
                                Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                                break;
                            default:
                                Cursor = System.Windows.Forms.Cursors.Default;
                                redimStatus = "";
                                break;
                        }
                    }
                    else
                    {
                        Cursor = System.Windows.Forms.Cursors.Default;
                        redimStatus = "";
                    }
                }
                else
                {
                    Cursor = System.Windows.Forms.Cursors.Default;
                    redimStatus = "";                    
                }

                #endregion
            }
            
        }


        // pomocna metoda pri stisku Vlastnosti v mem Custom PropertyGridu
        public void PushSelectionToShowInCustomGrid()
        {
            infoStatLabel.Text = "";
            PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
            ObjectSelected(this, e1);// raise event  
            
            Redraw(true); //redraw all=true 
            
        }

        // posila proveden zmeny do CsutomPropertyGridu ... zjednodusena verye bez prekreslovani obrazovky
        public void PushPlease()
        {
            infoStatLabel.Text = "";
            PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
            ObjectSelected(this, e1);// raise event  
           
        }

        // simuluje stisk Esc klavesy;
        public void ForceEsc()
        {

            infoStatLabel.Text = "";
            shapes.DeSelect();
            PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
            ObjectSelected(this, e1);  // vyvolavam udalost....            

            Redraw(true); //redraw all=true   
        }




        // stisk "A" pri kresleni polygonu !!!!
        // stisk Esc udela deselect vseho krome platna
        // stsik Delete vymaze vybrane elementy z platna
        private void Platno_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {           

            if (option == "POLY" && (Keyboard.IsKeyDown(Key.A)))
            {
                penPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                Redraw(false);

            }
            if (e.KeyCode == Keys.Escape)
            {             
                ForceEsc();
            }
            if (e.KeyCode == Keys.Delete)
            {
                RmSelected();
                Redraw(true);
            }

           
        }

        public void Platno_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //showDebug = false;
            tip.Active = false;
            infoStatLabel.Text = "";
           
            if (e.Button == MouseButtons.Left)
            #region Mouse Left up
            {
                int tmpX = (int)((e.X) / Zoom - dx);
                int tmpY = (int)((e.Y) / Zoom - dy);
                if (Fit2grid & this.Mřížka > 0)
                {
                    tmpX = Mřížka * (int)((e.X / Zoom - dx) / Mřížka);
                    tmpY = Mřížka * (int)((e.Y / Zoom - dy) / Mřížka);
                }

                switch (option)
                {
                    #region selectrect
                    case "select":
                        if (status != "redim")
                        {
                            shapes.ClickOnShape((int)((e.X) / Zoom - dx), (int)((e.Y) / Zoom - dy), r);
                        }
                        else
                        {
                            if (this.shapes.selEle is PointSet)
                            {//POLYGON MANAGEMENT START
                                shapes.AddPoint();
                                
                                if (Fit2grid & Mřížka > 0)
                                {
                                    shapes.Fit2Grid(Mřížka);
                                }
                                switch (redimStatus)
                                {
                                    case "ROT":
                                        shapes.selEle.CommitRotate(tmpX, tmpY);                                                                            
                                        break;
                                    default:
                                        break;
                                }//POLY MANAGEMENT END
                            }
                            

                        }

                        if (status == "selrect")
                        {
                            if ((((e.X) / Zoom - dx - startX) + ((e.Y) / Zoom - dy - startY)) > 12)
                            {
                                // timto ridim multi-object vyber
                                shapes.MultiSelect(startX, startY, (int)((e.X) / Zoom - dx), (int)((e.Y) / Zoom - dy), r);
                            }
                        }

                        ChangeStatus("");
                        break;
                    #endregion
                    #region Rect - obdelnik
                    case "DR": //DrawRect

                        if (status == "drawrect")
                        {
                            shapes.AddRect(startX, startY, tmpX, tmpY, creationPenColor, creationFillColor, creationPenWidth, creationColorFilled, creationTextureFilled, creationTexturePattern);
                            ChangeOption("select");
                        }
                        break;
                    #endregion  
                    #region Polygon & Pen - volna cara
                    case "PEN": 
                        shapes.AddPoly(startX, startY, tmpX, tmpY, creationPenColor, creationFillColor, creationPenWidth, creationColorFilled, penPointList, true, uzavrenaKrivka, creationTextureFilled, creationTexturePattern);
                        penPointList = null;
                        visPenPointList = null;
                        ChangeOption("select");
                        krivka = true;
                        break;
                    case "POLY":                        
                        {
                            // toto je tehdy kdyz jsem nestiskl "A" pri zadavani polygonu ...
                            if(penPointList != null)
                            {
                                if (penPointList.Count == 1)
                                {
                                    penPointList.Add(new PointWrapper(tmpX - startX, tmpY - startY));
                                }
                            }
                            
                            shapes.AddPoly(startX, startY, tmpX, tmpY, creationPenColor, creationFillColor, creationPenWidth, creationColorFilled, penPointList, false, uzavrenaKrivka, creationTextureFilled, creationTexturePattern);                           
                            penPointList = null;
                            visPenPointList = null;
                            ChangeOption("select");
                            krivka = true;
                        }
                        break;

                    
                    #endregion
                    
                    #region Elipsa                
                    case "ELL": 

                        if (status == "drawrect")
                        {
                            shapes.AddElipse(startX, startY, tmpX, tmpY, creationPenColor, creationFillColor, creationPenWidth, creationColorFilled, creationTextureFilled, creationTexturePattern);
                            ChangeOption("select");
                        }
                        break;
                        
                        
                    #endregion
                    #region DrawTextBox

                    // tento box zatim nevkladam
                    /*
                    case "TB": //DrawTextBox
                        if (this.status == "drawrect")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            editorForm.ShowDialog();
                            this.Cursor = Cursors.Arrow;
                            this.shapes.AddTextBox(startX, startY, tmpX, tmpY, r, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            ChangeOption("select");
                        }
                        break;
                        */
                    #endregion
                    #region DrawSimpleTextBox
                    case "STB": //DrawSimpleTextBox
                        if (status == "drawrect")
                        {
                            Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            DialogResult dr = editorForm.ShowDialog();
                            if(dr == DialogResult.OK)
                            {
                                Cursor = System.Windows.Forms.Cursors.Arrow;
                                shapes.AddSimpleTextBox(startX, startY, tmpX, tmpY, r, creationPenColor, creationFillColor, creationPenWidth, creationColorFilled, creationTextureFilled, creationTexturePattern);
                                ChangeOption("select");

                            }
                            else
                            {
                                //shapes.DeSelect();
                                //Cursor = System.Windows.Forms.Cursors.Arrow;
                                //Redraw(true);
                                status = "";
                                ChangeOption("select");

                            }
                            //editorForm.ShowDialog();
                            
                           
                        }
                        break;
                    #endregion
                    #region Obrazek
                    case "IB": 
                        if (status == "drawrect")
                        {
                            // load obrazku ...
                            string f_name = ImgLoader();
                            shapes.AddImageBox(startX, startY, tmpX, tmpY, f_name, creationPenColor, creationPenWidth);                            
                            ChangeOption("select");
                        }
                        break;
                       
                    #endregion
                    #region Prima cara
                    case "LINE":

                        if (status == "drawrect")
                        {
                            shapes.AddLine(startX, startY, tmpX, tmpY, creationPenColor, creationPenWidth);                          
                            ChangeOption("select");
                        }
                        break;
                       
                    #endregion
                    default:
                        ChangeStatus("");
                        break;
                }


                // ukladam si start X,Y,X1,Y1 vybranych elementu ...
                if (shapes.selEle != null)
                {
                    
                    if (shapes.selEle is PointSet)
                    {
                        ((PointSet)shapes.selEle).SetupSize();
                        shapes.sRec = new SelPoly(shapes.selEle);  // vytvorim ovladaci obdelnik kolem ...
                        
                    }

                    if (redimStatus != "")
                    {
                        shapes.EndMove();
                    }

                    if (shapes.sRec != null)
                    {
                        shapes.sRec.EndMoveRedim();
                    }
                }
                // A to nejdulezitejsi - ukaz vlastnosti objektu v Property Gridu ....
                PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                ObjectSelected(this, e1);  // vyvolá uměle událost

                Redraw(true); //redraw all=true                

                mouseSx = false; // end funkce SX !!
            }
            #endregion
            else
            #region Mouse Right up
            {
                // A to nejdulezitejsi - ukaz vlastnosti objektu v Property Gridu ....
                PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                ObjectSelected(this, e1); // vyvolá uměle událost
            }
            #endregion
        }


        // 2x klik na prostredni tlacitko = Zoom 100%
        private void Platno_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (Fit2grid & Mřížka > 0)
                {
                    dx = Mřížka * ((100) / Mřížka);
                    dy = Mřížka * ((100) / Mřížka);

                }
                else
                {
                    dx = 100;
                    dy = 100;
                }


                Zoom = 0.25f;
            }

            if(e.Button == MouseButtons.Left)
            {
                object ob = shapes.selEle;

                if (ob != null)
                {

                    if (ob.GetType() == typeof(Line))
                    {
                        
                        shapes.DeSelect();
                        ChangeOption("LINE");

                    }

                    /*
                    if (ob.GetType() == typeof(Stext))
                        ChangeOption("STB");                    
                    else if (ob.GetType() == typeof(Line))                        
                        ChangeOption("LINE");
                    else if (ob.GetType() == typeof(Rectangle)) // nejde
                        ChangeOption("DR");                    
                    else if (ob.GetType() == typeof(Ellipse))
                        ChangeOption("ELL");                   
                    else if (ob.GetType() == typeof(PointSet)) // nejde
                        ChangeOption("POLY");                    
                    else if (ob.GetType() == typeof(ImageBox))
                        ChangeOption("IB");   
                        */

                }
                
                
            }
        }

        #endregion

        #region Metody PropertyChanged - pro muj ProperyGrid

        public void PropertyChanged()
        {
            shapes.PropertyChanged(); 
            


        }
       

        #endregion        

        #region Zakladni udalosti pro Platno: Paint, Resize
        // Zakladni metoda Paint pro Platno
        private void Platno_Paint(object sender, PaintEventArgs e)
        {
            Redraw(e.Graphics, false); // false = prekresluji POUZE DYNAMICKE objekty na platne Platno
        }


        // Prispusobuju velikost mych Bitmap pro Graphics - podle skutecne velikosti platna Platno
        private void Platno_Resize(object sender, EventArgs e)
        {
            if (Width > 0 & Height > 0)
            {
                offScreenBackBmp = new Bitmap(Width, Height);
                offScreenBmp = new Bitmap(Width, Height);
                Redraw(true);
            }
        }

        #endregion

        #region Metody pro ZOOM
        public void ZoomIn(int sirka, int vyska)
        {
            rozdilSirek = sirka - dx;
            rozdilVysek = vyska - dy;
        }

        public void ZoomIn()
        {
            if (Zoom <= 15f)
            { 
                Zoom = (float)(Zoom * 2);                
                kolikNasobneZoom = Zoom;                
                PushPlease();
            }
        }

        

        public void ZoomOut()
        {
            if (Zoom > 0.01f && Zoom <= 21f)
            {
                Zoom = (float)(Zoom / 2);                
                kolikNasobneZoom = Zoom;                
                PushPlease();
            }
        }

        #endregion

        #region Konkretni metoda pro Event OnMouseWheel udalost nad platnem

        List<T> GetControlByName<T>(
        Control controlToSearch, string nameOfControlsToFind, bool searchDescendants)
        where T : class
        {
            List<T> result;
            result = new List<T>();
            foreach (Control c in controlToSearch.Controls)
            {
                if (c.Name == nameOfControlsToFind && c.GetType() == typeof(T))
                {
                    result.Add(c as T);
                }
                if (searchDescendants)
                {
                    result.AddRange(GetControlByName<T>(c, nameOfControlsToFind, true));
                }
            }
            return result;
        }
        #endregion


        #region Zoomovani koleckem - uz konkretni hodnoty
        private void MyOnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            // toto je Zoom Out
            if (e.Delta < 0 && (Zoom > 0.01f && Zoom <= 21f))
            {
                if (Fit2grid & Mřížka > 0)
                {
                    //int gr = gridSize;
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));

                    dx = Mřížka * ((dx) / Mřížka);
                    dy = Mřížka * ((dy) / Mřížka);

                    ZoomOut();
                    //gridSize = gr;
                }
                else
                {
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));
                    ZoomOut();
                }

            }


            //toto je Zoom In
            else if (e.Delta > 0 && (Zoom <= 15f))
            {
                if (Fit2grid & Mřížka > 0)
                {
                    //int grid = gridSize;
                    dx = (int)(dx - (e.X / (Zoom * 2)));
                    dy = (int)(dy - (e.Y / (Zoom * 2)));

                    dx = Mřížka * ((dx) / Mřížka);
                    dy = Mřížka * ((dy) / Mřížka);

                    ZoomIn();
                    //gridSize = grid;
                }
                else
                {
                    dx = (int)(dx - (e.X / (Zoom * 2)));
                    dy = (int)(dy - (e.Y / (Zoom * 2)));
                    ZoomIn();
                }
            }
        }

        #endregion


        #region Image Loader - zakladni Zahradni objekty
        // zakladni vstup obrazku zde touto metodou...
        public string ImgLoader()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                // nastavuje aktualni cestu k exe souboru zahrada.exe
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dlg.InitialDirectory = currentDirectory;
                dlg.RestoreDirectory = true;
                dlg.Title = "Vybrat zahradní prvek";
                dlg.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";               
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return (dlg.FileName);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Zahradní prvek nebyl načten !", "Otevření selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Výjimka:" + e.ToString(), "Load error:");
            }
            return null;
        }

        #endregion
        

        #region Texture Loader - textury pro me vectory
        public void TextureLoader()
        {

            try
            {
                using (OpenFileDialog dil = new OpenFileDialog())
                {
                    // nastavuje aktualni cestu k exe souboru zahrada.exe
                    string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    dil.InitialDirectory = currentDirectory;
                    dil.RestoreDirectory = true;

                    dil.Title = "Vyber texturu";
                    dil.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";

                    if (dil.ShowDialog() == DialogResult.OK)
                    {
                        Image obr = new Bitmap(dil.FileName);
                        TextureBrush tBrush = new TextureBrush(obr);
                        tBrush.WrapMode = WrapMode.Tile;
                        SetTexture(tBrush);

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Textura nebyla načtena !", "Otevření selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Výjimka:" + e.ToString(), "Load error:");
            }

        }

        #endregion


        #region Load/Save projektu
        // Zakladni Load/Save mych souboru:
        public bool Saver()
        {
            try
            {

                Stream StreamWrite;
                SaveFileDialog dlg = new SaveFileDialog();
                // nastavuje aktualni cestu k exe souboru zahrada.exe
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dlg.InitialDirectory = currentDirectory;
                dlg.RestoreDirectory = true;
                dlg.DefaultExt = "bin";
                dlg.Title = "Uložit soubor jako";
                dlg.Filter = "Zahrada soubor (*.bin)|*.bin|Všechny soubory (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamWrite = dlg.OpenFile()) != null)
                    {
                        BeforeSave(); // ukladam si zakladni promenne o vykresu dx,dy, Ax, Ay, Zoom, atd ...   
                        BinaryFormatter BinaryWrite = new BinaryFormatter();
                        BinaryWrite.Serialize(StreamWrite, shapes); // ukladam si instanci tridy Shapes - to je vse !
                        StreamWrite.Close();

                        string fn = Path.GetFileNameWithoutExtension(dlg.FileName);
                        plneJmenoNoveOtevreneho = dlg.FileName;
                        jmenoNoveOtevreneho = fn;
                        SaveSuccess = true;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Projekt nebyl uložen !", "Uložení selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Výjimka:" + e.ToString(), "Save error:");
            }
            SaveSuccess = false;
            return false;
        }


        public bool SimpleSaver()
        {
            try
            {
                Stream StreamWrite;
                SaveFileDialog dlg = new SaveFileDialog();
                // nastavuje aktualni cestu k exe souboru zahrada.exe
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dlg.InitialDirectory = currentDirectory;
                dlg.RestoreDirectory = true;
                //dlg.DefaultExt = "bin";
                //dlg.Title = "Uložit soubor";
                //dlg.Filter = "Zahrada soubor (*.bin)|*.bin|Všechny soubory (*.*)|*.*";
                dlg.FileName = plneJmenoNoveOtevreneho;

                if ((StreamWrite = dlg.OpenFile()) != null)
                    {
                        BeforeSave(); // ukladam si zakladni promenne o vykresu dx,dy, Ax, Ay, Zoom, atd ...   
                        BinaryFormatter BinaryWrite = new BinaryFormatter();
                        BinaryWrite.Serialize(StreamWrite, shapes); // ukladam si instanci tridy Shapes - to je vse !
                        StreamWrite.Close();

                        string fn = Path.GetFileNameWithoutExtension(dlg.FileName);
                        jmenoNoveOtevreneho = fn;
                        SaveSuccess = true;
                        return true;
                    }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Projekt nebyl uložen !", "Uložení selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Výjimka:" + e.ToString(), "Save error:");
            }
            SaveSuccess = false;
            return false;
        }



        public bool Loader()
        {
            try
            {   

                Stream StreamRead;
                OpenFileDialog dlg = new OpenFileDialog();
                // nastavuje aktualni cestu k exe souboru zahrada.exe
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dlg.InitialDirectory = currentDirectory;
                dlg.RestoreDirectory = true;

                dlg.DefaultExt = "bin";
                dlg.Title = "Otevřít soubor";
                dlg.Filter = "Zahrada soubor (*.bin)|*.bin|Všechny soubory (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamRead = dlg.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryRead = new BinaryFormatter();
                        shapes = (Shapes)BinaryRead.Deserialize(StreamRead);
                        StreamRead.Close();

                        AfterLoad(); // po uspesne deserializaci - nastavuju zakladni promenne planu dx,dz,rozmery vykresu Ax,Ay, Zoom

                        //shapes.AfterLoad();

                        Redraw(true);
                        LoadSucces = true;
                        string fn = Path.GetFileNameWithoutExtension(dlg.FileName);
                        plneJmenoNoveOtevreneho = dlg.FileName;
                        jmenoNoveOtevreneho = fn;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Výjimka:" + e.ToString(), "Load error:");
                MessageBox.Show("Projekt nebyl načten !", "Otevření selhalo",  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadSucces = false;
            return false;
        }

        // dve pomocne metody pred ulozenim / po nahrani
        private void BeforeSave()
        {
            //saving udaju pro rozmery platna
            shapes.dxSave = dx;
            shapes.dySave = dy;
            shapes.AxSave = Šířka;
            shapes.AySave = Výška;
            shapes.ZoomSave = Zoom;


            // maze undo Buffer !!!
            shapes.AfterLoad();
            

        }

        private void AfterLoad()
        {
            dx = shapes.dxSave;
            dy = shapes.dySave;
            Šířka = shapes.AxSave;
            Výška = shapes.AySave;
            Zoom = shapes.ZoomSave;
            shapes.AfterLoad();
            PushPlease();
        }

        #endregion


        #region Print & Preview

        public PrintDocument docToPrint2 = new PrintDocument();

        public void PreviewBeforePrinting(float zoom)
        {
            Color c = Pozadí;
            Pozadí = Color.White;

            InitializePrintPreviewControl(zoom); // volano po stiski tlacitka preview
            Pozadí = c;
           // InicializujDocToPrint();

        }

        public void PrintMe() // volano po stisku Tisk tlacitka ...
        {
            Color c = Pozadí;
            Pozadí = Color.White;

            InicializujDocToPrint2(); // priradil jsem spravne docToPrin2
            shapes.DeSelect();           

            printDialog1.AllowSomePages = true;
            printDialog1.ShowHelp = true;
            printDialog1.Document = docToPrint2;   
            
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {                
                PrinterSettings mysettings = printDialog1.PrinterSettings;
                docToPrint2.PrinterSettings = mysettings;                
                printPreviewDialog1.Document = docToPrint2;                
                printPreviewDialog1.ShowDialog();
             
            }
            Pozadí = c;

        }

      

        

        private void InitializePrintPreviewControl(float zoom)
        {
            shapes.DeSelect();

            nahledForm = new NahledTisku();
            //nahledForm.PrintPreviewControl.Name = "Preview";           
            nahledForm.PrintPreviewControl.Document = nahledForm.docToPrint;
            nahledForm.PrintPreviewControl.Document.DocumentName = "Výkres zahrady";
     
            nahledForm.PrintPreviewControl.Zoom = zoom;   
            nahledForm.PrintPreviewControl.UseAntiAlias = true;
         
            nahledForm.docToPrint.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);

           
            
             nahledForm.ShowDialog(); // modalni okno
            // nahledForm.Show(); // modeless okno (nemodalni)

        }

       // udalost co vlastne tisknout - Graphics muj ...
        private void docToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics g = e.Graphics;
            //shapes.Draw(g, 0, 0, 1f); // tady je Zoom pro tisk !!
           
            g.DrawImageUnscaled(offScreenBmp, 0, 0);
            
           
            //g.Dispose();
        }

        // udalost pro docToPrint2
        private void InicializujDocToPrint2()
        {
            shapes.DeSelect();
            docToPrint2.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
        }









        #endregion


        #region Export nakresleneho do PNG/JPG/GIF
        public void ExportTo()
        {

            ForceEsc();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();           
            saveFileDialog1.Filter = "Png files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif";
            //saveFileDialog1.InitialDirectory = Config.Instance().GetSetting("FileDir/Path", Application.StartupPath);
            //string flnm = docManager.fileName;
            string flnm = "Export.png";           
            saveFileDialog1.FileName = flnm;
            DialogResult res = saveFileDialog1.ShowDialog(this);
            if (res != DialogResult.OK)
                return;
            flnm = saveFileDialog1.FileName;           
            
            Bitmap bp3 = offScreenBmp;          
            Color tohle = Pozadí;
            Pozadí = Color.White;            
            SaveImage(bp3, flnm);
            Pozadí = tohle;
        }

        void SaveImage(Image img, string flnm)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            string mimetype = GetMimeType(flnm);
            if (mimetype.Length == 0)
                return;
            myImageCodecInfo = GetEncoderInfo(mimetype);
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, (long)55);
            myEncoderParameters.Param[0] = myEncoderParameter;

            img.Save(flnm, myImageCodecInfo, myEncoderParameters);
        }

        string GetMimeType(string flnm)
        {
            if (flnm.LastIndexOf("jpg") > 0)
                return "image/" + "jpeg";
            if (flnm.LastIndexOf("png") > 0)
                return "image/" + "png";
            if (flnm.LastIndexOf("gif") > 0)
                return "image/" + "gif";
            return "";
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; j++)
            {
                if (encoders[j].MimeType == mimeType)
                {
                    return encoders[j];
                }
            }
            return null;
        }







        #endregion


        // pri inicializaci si hledam StatusStrip/InfoStatLabel v MainForm ... volano z MainForm

        StatusStrip mujStatusStrip;
        public ToolStripStatusLabel infoStatLabel;
        public void NajdiStatusStripVmainForm()
        {
            var najdiStrip = Parent.Controls.Find("statusStrip", true);
            mujStatusStrip = (StatusStrip)najdiStrip.First();

            var najdiInfoLabelVeStStripu = mujStatusStrip.Items.Find("InfoToolStripStatusLabel", true);
            infoStatLabel = (ToolStripStatusLabel)najdiInfoLabelVeStStripu.First();

            infoStatLabel.Text = "Ahoj";

        }







    }
}
