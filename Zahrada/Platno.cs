using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO; 						//streamer io
using System.Runtime.Serialization;     // io
using System.Runtime.Serialization.Formatters.Binary; // io
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using Zahrada.OdvozeneTridyEle;
using Zahrada.PomocneTridy;
using Zahrada.UndoRedoBufferTridy;
using System.Windows.Input;

namespace Zahrada{
    
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
       


        public bool showDebug;

        private float _zoom = 1f;
        private bool _A4 = true;
        // private bool paintUdalostObslouzena = false;

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
        
        
        // Grid mrizka
        public int _gridSize = 0;
        public bool fit2grid = true;

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
        private NahledForm nahledForm;

        //RichTextBox formularove okno
        private RichTextBoxForm editorForm;
        public RichTextBox r; // extrahuju si pozdeji pouze RichTextBox z formularoveho okna RTBF

        // Barevnost pera, Tloustka Pera, Barevnost vyplne, Otazka vyplne - zadavani vseobecne pro vsechno co lze
        public Color creationPenColor;
        public float creationPenWidth;
        public Color creationFillColor;
        public bool creationFilled;
        public bool creationClosed;

        // prirazeni event-handleru mym udalostem na platne (pozdeji budu prirazovat take pro toolbox, ale se zmenymi parametry)
        public event OptionChangedEventHandler OptionChanged;
        public event ObjectSelectedEventHandler ObjectSelected;

        System.Windows.Forms.Cursor addPointCursor = GetCursor("newPoint3.cur", System.Windows.Forms.Cursors.Cross);
        System.Windows.Forms.Cursor delPointCursor = GetCursor("delPoint3.cur", System.Windows.Forms.Cursors.Default);  // v projektu neni nikdy pouzity

        public int kolikNasobneZoom = 1;
        public int rozdilSirek;
        public int rozdilVysek;

        public ToolStripComboBox nalezenyZoomCBvToolStrip;
        public ToolStrip nalezenyToolStripvMainForm;
        public ToolStripComboBox nalezenyClosedCBvToolStrip;

        int indexClosed;
        public bool uzavrenaKrivka;
        public bool krivka = false;

        ArrayList bb = new ArrayList();
        



        //public Form prirazenyFormular = null;

        #endregion       

        #region Konstruktor tridy Platno

        /*
        public Platno(Form prirad)
        {
            prirazenyFormular = prirad as HlavniForm;
            InitializeComponent();
        }
        */


        public Platno()
        {
            InitializeComponent();
            myInit();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); //to je pro optimalizaci vykreslovani

            // Event Handler - obsluha udalosti MouseWheel pro User Control Platno (musel jsem ji k Platnu vytvorit externe a zde ji pridavam)
            MouseWheelHandler.Add(this, MyOnMouseWheel); // ovladaci metoda udalosti - k presnemu zoomovani         
            bb.Add(new PointWrapper(0, 0));

        }

        /*
        public void SetMainForm(Form formular)
        {
            prirazenyFormular = formular;

        }
        */
       

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


            //s = new Shapes((pd.PrinterSettings.DefaultPageSettings.PrinterResolution.X / g.DpiX), pd.PrinterSettings.DefaultPageSettings.PrinterResolution.Y);
            shapes = new Shapes(g.DpiX * (x_pr / x_vi), g.DpiY * (y_pr / y_vi));
            

            //            undoB = new UndoBuffer(5);
            g.Dispose();

            editorForm = new RichTextBoxForm();
            r = editorForm.mujRichTextBox; // tady si vytahuju z formulare pouze ten richtextbox control

            //zakladni nastaveni kreslicich barev/tloustek/vyplnovani
            creationPenColor = Color.Black;
            creationPenWidth = 1f;
            creationFillColor = Color.Black;
            creationFilled = false;

            // skutecne prirazeni (new) udalosti v inicializaci MyInit() prvku platno
            OptionChanged += new OptionChangedEventHandler(OnBasicOptionChanged);
            ObjectSelected += new ObjectSelectedEventHandler(OnBasicObjectSelected);

            offScreenBackBmp = new Bitmap(this.Width, this.Height);
            offScreenBmp = new Bitmap(this.Width, this.Height);
            

        }

       // metodu volam pri Zoomovani koleckem
        public void UpravZoomVComboBoxu(int index)
        {
            nalezenyZoomCBvToolStrip.SelectedIndex = index;
        }

        // pomocna metoda k nalezeni formularoveho prvku
        public void NajdiZoomComboBoxvMainForm()
        {
            var najdiToolStripVMainForm = Parent.Controls.Find("toolStrip1", true);
            nalezenyToolStripvMainForm = (ToolStrip)najdiToolStripVMainForm.First();
            var najdiZoomCB = nalezenyToolStripvMainForm.Items.Find("zoomToolStripComboBox", true);
            nalezenyZoomCBvToolStrip = (ToolStripComboBox)najdiZoomCB.First();
            //var najdiClosedCB = nalezenyToolStripvMainForm.Items.Find("closedToolStripComboBox", true);
            //nalezenyClosedCBvToolStrip = (ToolStripComboBox)najdiClosedCB.First();

        }

        public void NajdiClosedCBvMainForm()
        {
            var najdiToolStripVMainForm = Parent.Controls.Find("toolStrip1", true);
            nalezenyToolStripvMainForm = (ToolStrip)najdiToolStripVMainForm.First();
            var najdiClosedCB = nalezenyToolStripvMainForm.Items.Find("closedToolStripComboBox", true);
            nalezenyClosedCBvToolStrip = (ToolStripComboBox)najdiClosedCB.First();
            indexClosed = nalezenyClosedCBvToolStrip.SelectedIndex;

        }



        /*
        public void NajdiZoomComboBoxvToolStripu()
        {
            var hledamToolStripVMainForm = Parent.Controls.Find("toolStrip1", true);
            ToolStrip nalezenyToolStripvMainForm = (ToolStrip)hledamToolStripVMainForm.First();
            var hledamZoomCB = nalezenyToolStripvMainForm.Items.Find("zoomToolStripComboBox", true);
            ToolStripComboBox naleyenyZoomCB = (ToolStripComboBox)hledamZoomCB.First();
        }
        */



        #endregion

        #region Vlastnosti pro Graphics, kterym navic nastavuji Category a Description v mem Property Gridu

        // Nastavuji si viditelnost promenne showDebug pro pomocne vypisky pri kresleni
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



        [Category("1"), Description("Plátno")]
        public string ObjectType
        {
            get
            {
                return "Plátno";
            }
        }

        [Category("Plátno - popis"), Description("Velikost mřížky")]
        public int gridSize
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


        [CategoryAttribute("Plátno - popis"), DescriptionAttribute("Plátno Zoom")]
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


        [CategoryAttribute("Plátno - popis"), DescriptionAttribute("Zobraz A4 rámeček")]
        public bool A4
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

        [CategoryAttribute("Plátno - popis"), DescriptionAttribute("Plátno OriginX")]
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

        [CategoryAttribute("Plátno - popis"), DescriptionAttribute("Plátno OriginY")]
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

        private void ChangeStatus(string s)
        {
            status = s;
        }

        private void ChangeOption(string s)
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
                shapes.selEle.PenColor = c;
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

        public void SetFilled(bool f)
        {
            creationFilled = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.Filled = f;
            }
        }

        public void SetPenWidth(float f)
        {
            creationPenWidth = f;
            if (shapes.selEle != null)
            {
                shapes.selEle.PenWidth = f;
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
            if (fit2grid & gridSize > 0)
            {
                //kolikjeGrid = gridSize;
                //dx = gridSize * ((dx) / gridSize);
               // dy = gridSize * ((dy) / gridSize);

                startX = gridSize * (startX / gridSize);
                startY = gridSize * (startY / gridSize);
            }
            /*
            if (Zoom == 2f)
            {
                startX = startX - 2000;
                startY = startY - 2000;
            }
            *
            */
            /*
           
            */
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
                if (gridSize > 0)
                {
                    //backG.TransformPoints(CoordinateSpace.Page, CoordinateSpace.World);
                    //GraphicsUnit u = backG.PageUnit;
                    //backG.PageUnit = GraphicsUnit.Millimeter;

                    Pen myPen = new Pen(Color.LightGray, 0.1f);
                   
                    int nX = (int)(Width / (gridSize * Zoom));
                    for (int i = 0; i <= nX; i++)
                    {
                        backG.DrawLine(myPen, i * gridSize * Zoom, 0, i * gridSize * Zoom, Height);
                    }
                    int nY = (int)(Height / (gridSize * Zoom));
                    for (int i = 0; i <= nY; i++)
                    {
                        backG.DrawLine(myPen, 0, i * gridSize * Zoom, Width, i * gridSize * Zoom);
                    }

                    //backG.PageUnit = u;
                    myPen.Dispose(); // uvolnuji zdroje
                }

                // Nakresli NEVYBRANE objekty:
                // tohle neudela vubec nic .....
                /*
                if (Zoom == 0.5f)
                {
                    dx = dx + 200;
                    dy = dy + 200;
                }
                */

                shapes.DrawUnselected(backG, dx, dy, Zoom); // posledni hodnota byla Zoom

               // shapes.DrawUnselected(backG,  (int)(dx-dx/Zoom), (int)(dy-dy/Zoom), Zoom);

                backG.Dispose();
            }

            //AKce pro Double Buffering
            Graphics offScreenDC;
            offScreenDC = Graphics.FromImage(offScreenBmp);
            offScreenDC.TextRenderingHint = TextRenderingHint.AntiAlias;
            offScreenDC.SmoothingMode = SmoothingMode.AntiAlias;
            offScreenDC.Clear(BackColor);


            // Kreslim BackGroundBitmap se statickymi objekty

            //if (gridSize > 0)
            //    gridSize = kolikjeGrid;

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


                //if (option == "LINE" || option == "POLY" || option == "GRAPH")
                if (option == "LINE"  || option == "GRAPH")
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
            if (A4)
            {
                //GraphicsUnit u = offScreenDC.PageUnit;
                //offScreenDC.PageUnit = GraphicsUnit.Millimeter;
                Pen myPen = new Pen(Color.Blue, 0.5f);
                myPen.DashStyle = DashStyle.Dash;
                //offScreenDC.DrawRectangle(myPen, (1 + dx) * Zoom, (1 + dy) * Zoom, 810 * Zoom, 1140 * Zoom);

                // toto mi nevyslo - budu resit pozdeji ...
                /*
                float onePointX = offScreenDC.DpiX;
                float onePointY = offScreenDC.DpiY;

                float xA4size = onePointX  *210/25.4f;
                float yA4size = onePointY*297/25.4f;
                */


                // vykresleni jednotek
                StringFormat drawFormat = new StringFormat();
                Font drawFont = new Font("Arial", 7);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                // nechavam to na 1pixel = 1mm
                // + 20 znaci primarni odstup pri vzkreslovani ramecku
                offScreenDC.DrawRectangle(myPen, (dx) * Zoom, (dy) * Zoom, (2100) * Zoom, (2970) * Zoom);

               

                for (int i =0; i <= 2100; i=i+10)
                {
                    offScreenDC.DrawLine(myPen, (dx+i)*Zoom, (dy+3)*Zoom, (dx+i)*Zoom, ((dy)*Zoom));


                    // vykresleni souradnic X nad rameckem
                    string metry = (i/100).ToString();
                   
                    if (i == 0)
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx + i) * Zoom) - 15), (((dy) * Zoom) - 10 ));
                    if (((i / 10) % 20) == 0 & (i !=0))
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx + i ) * Zoom) - 6), (((dy) * Zoom) - 20));


                }   
                for (int a = 0; a <= 2970; a = a + 10)
                {
                    offScreenDC.DrawLine(myPen, (dx) * Zoom, (dy + a) * Zoom, (dx + 3) * Zoom, ((dy+ a) * Zoom));
                    
                    // vykresleni souradnic Y nad rameckem
                    string metry = (a / 100).ToString();
                    if (((a / 10) % 20) == 0 & (a != 0))
                        offScreenDC.DrawString(metry, drawFont, drawBrush, (((dx) * Zoom) - 20), (((dy + a) * Zoom) - 6));
                }
                //offScreenDC.PageUnit = u;
                myPen.Dispose();
                //malickost na odstup od kraje 20 bodu
                //dx = 20;
                //dy = 20;

                

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
            //if (gridSize > 0)
            //    gridSize = kolikjeGrid;
            g.DrawImageUnscaled(offScreenBmp, 0, 0);
            //g.DrawImageUnscaled(offScreenBmp, (int)(dx-dx/Zoom), (int)(dy-dy/Zoom));

            // uvolnim zdroje
            offScreenDC.Dispose();
            
        }        

        // budu pouzivat pri pohyby mysi - bude mi to hlasit pozici kurzoru
        private void DrawDebugInfo(Graphics g)
        {
            
            //Kontrolni zprava pri Draw ...
            if (ShowDebug)
            {
                msg = " Status : " + this.status;
                msg = msg + " Option : " + this.option;
                msg = msg + " redimStatus : " + this.redimStatus;
                Font tmpf = new System.Drawing.Font("Arial", 7);
                g.DrawString(msg, tmpf, new SolidBrush(Color.Gray), new PointF((tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom), StringFormat.GenericDefault);
                tmpf.Dispose();
            }
        }
        
        // tuto metodu jsem nepouzil nikde
        // Nakresli merici jednotky po 5 milimterech na A4 ....
        private void DrawMeasure(Graphics g)
        {
            // A4 je 210 x 297 mm
            /* TEST*/
            GraphicsUnit u = g.PageUnit; // k navraceni zpet do puvodniho (Graficke jednotky = display)
            g.PageUnit = GraphicsUnit.Millimeter; // nasatvuju si milimetry

            // draws in millimeters
            Pen myPen = new Pen(Color.LightGray, 0.5f);
            //g.DrawLine(myPen, 0, 5, 210, 5);
            for (int i = 0; i <= 210; i = i + 10)
            {
                g.DrawLine(myPen, i, 0, i, 5);
            }
            g.PageUnit = u; // navraceni zpet na puvodni jednotky (display)

        }
        #endregion

        #region Metody pro obsluhu udalosti pro MYS

        private void Platno_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            startX = (int)((e.X) / Zoom - dx);
            startY = (int)((e.Y) / Zoom - dy);
            //this.startX = (int)( e.X- this.dx  );
            //this.startY = (int)( e.Y - this.dy );

            truestartX = (int)(e.X/Zoom); // spravna prace s prostrednim tlacitkem
            truestartY = (int)(e.Y/Zoom); // spravna prace s prostrednim tlacitkem

            //this.truestartX = (int)e.X;
            //this.truestartY = (int)e.Y;

            if (e.Button == MouseButtons.Left)
            {
                #region START LEFT MOUSE BUTTON PRESSED
                mouseSx = true; // I start pressing SX

               



                switch (option)
                {
                    case "select":

                        if (redimStatus != "")
                        {
                            // I'm over an object or Object redim handle
                            ChangeStatus("redim");// I'm starting redim/move action
                        }
                        else
                        {
                            // I'm pressing SX in an empty space, I'm starting a select rect
                            ChangeStatus("selrect");// I'm starting a select rect
                        }

                        break;
                    case "PEN":
                        penPointList = new ArrayList();//reset pints buffer
                        visPenPointList = new ArrayList();//reset pints buffer
                        penPrecX = startX;
                        penPrecY = startY;
                        penPointList.Add(new PointWrapper(0, 0));
                        visPenPointList.Add(new PointWrapper(0, 0));                        
                        break;
                    case "POLY":
                        //bb = new ArrayList();
                        //bb.Add(new PointWrapper(0, 0));

                        penPointList = new ArrayList();//reset pints buffer
                        visPenPointList = new ArrayList();//reset pints buffer
                        penPointList.Add(new PointWrapper(0, 0));
                        visPenPointList.Add(new PointWrapper(0, 0));

                        //ChangeStatus("drawrect");
                        break;
                    default:

                        /* if Option != "select" then I'm going tocreate an object*/
                        //                    this.MouseSx = true; // I start pressing SX
                        ChangeStatus("drawrect"); //I 'm startring drawing a new object - toto je njdulezitejsi
                        

                        break;
                    
                        

                }
                #endregion
            }
            else if (e.Button == MouseButtons.Right)
            {
                // tady bude obsluha del/copy/cut/paste
                #region START RIGHT MOUSE BUTTON PRESSED
                startDX = dx;
                startDY = dy;
                //Cursor = System.Windows.Forms.Cursors.Cross; 
                
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

       

        

        private void Platno_MouseMove(object sender,System.Windows.Forms.MouseEventArgs e)
        {


            if (e.Button == MouseButtons.Left)
            #region left mouse button pressed
            {
                if (mouseSx)
                {
                    tempX = (int)(e.X / Zoom);
                    tempY = (int)(e.Y / Zoom);
                    if (fit2grid & gridSize > 0)
                    {                       
                        tempX = gridSize * (int)((e.X / Zoom) / gridSize);
                        tempY = gridSize * (int)((e.Y / Zoom) / gridSize);
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
                    

                    if (fit2grid & gridSize > 0)
                    {
                        tmpX = gridSize * (int)((e.X / Zoom - dx) / gridSize);
                        tmpY = gridSize * (int)((e.Y / Zoom - dy) / gridSize);
                        tmpstartX = gridSize * (startX / gridSize);
                        tmpstartY = gridSize * (startY / gridSize);     

                        shapes.Fit2Grid(gridSize);
                        shapes.sRec.Fit2grid(gridSize);
                        
                    }

                    switch (redimStatus)
                    {
                        //Poly's point
                        
                         
                        case "POLY":
                            // Move selected 
                            
                           
                            
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.MovePoint(tmpstartX - tmpX, tmpstartY - tmpY);
                               
                            }
                            if (fit2grid & gridSize > 0)
                            {
                                shapes.Fit2Grid(gridSize);
                                shapes.sRec.Fit2grid(gridSize);
                            }
                            break;
                           



                        case "C":
                            // Move selected
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.Move(tmpstartX - tmpX, tmpstartY - tmpY);

                                shapes.sRec.Move(tmpstartX - tmpX, tmpstartY - tmpY);
                            }
                            break;
                        case "ROT":
                            // rotate selected
                            if (shapes.selEle != null & shapes.sRec != null)
                            {
                                shapes.selEle.Rotate(tmpX, tmpY);
                                shapes.sRec.Rotate(tmpX, tmpY);
                            }
                            break;
                        case "ZOOM":
                            // rotate selected
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
                    if (this.option == "PEN")
                    {                       
                        
                        //this.s.addEllipse(tempX,tempY,tempX+1,tempY+1,Color.Blue,Color.Blue,1f,false);
                        visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                        if (Math.Sqrt(Math.Pow(penPrecX - tempX, 2) + Math.Pow(penPrecY - tempY, 2)) > 15)
                        {
                            penPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                            penPrecX = this.tempX;
                            penPrecY = this.tempY;
                        }
                        //ChangeOption("");
                        this.Redraw(false);
                        
                    }

                    // posouva se mi spravne cervene voditko kudy kreslit polyline ... podle posledniho bodu mysi
                    if(option == "POLY")
                    {
                       
                        if (visPenPointList.Count % 2 == 0)
                        {
                            visPenPointList.RemoveAt((visPenPointList.Count - 1));
                            visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                            //ChangeOption("");
                            Redraw(false);
                        }
                        else
                        {
                            visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                            //ChangeOption("");
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
                    // spravne nastaveno - pohyb A4 papiru po plose pri stisku Prostr. tlacitka
                   dx = (int)((startDX - truestartX)  + (e.X  / Zoom)) ;
                   dy = (int)((startDY - truestartY) + (e.Y) / (Zoom));

                   //this.dx = (this.startDX + this.truestartX - e.X);
                   // this.dy = (this.startDY + this.truestartY - e.Y);
                if (fit2grid & gridSize > 0)
                    {                    
                        dx = gridSize * ((dx) / gridSize); // toto moc nechapu, ale funguje to dobre - prichytava to A4 papir na mrizku
                        dy = gridSize * ((dy) / gridSize);
                    
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
                                //To change the cursor
                               // Cursor cc = new Cursor("NewPoint.ico");
                                //this.Cursor = cc;
                                
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
            //redraw();
        }












        // stisk Shif pri kresleni polygonu !!!!
        // stisk Esc udela deselec vseho krome platna
        private void Platno_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {           

            if (option == "POLY" && Keyboard.IsKeyDown(Key.LeftShift))
            {
                //bb.Add(new PointWrapper(tempX - startX, tempY - startY));
                penPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                visPenPointList.Add(new PointWrapper(tempX - startX, tempY - startY));
                Redraw(false);

            }
            if (e.KeyCode == Keys.Escape)
            {
                shapes.DeSelect();
                PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                ObjectSelected(this, e1);// raise event

                Redraw(true); //redraw all=true    

            }
        }

        public void Platno_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // podle comboboxu v Main Form ridim uzavreni POLY/PEN
            NajdiClosedCBvMainForm();
            if (indexClosed == 0)
                uzavrenaKrivka = false;
            else uzavrenaKrivka = true;

            if (e.Button == MouseButtons.Left)
            #region left up
            {
                int tmpX = (int)((e.X) / Zoom - dx);
                int tmpY = (int)((e.Y) / Zoom - dy);
                if (fit2grid & this.gridSize > 0)
                {
                    tmpX = gridSize * (int)((e.X / Zoom - dx) / gridSize);
                    tmpY = gridSize * (int)((e.Y / Zoom - dy) / gridSize);
                }

                switch (this.option)
                {
                    #region selectrect
                    case "select":
                        if (this.status != "redim")
                        {
                            this.shapes.ClickOnShape((int)((e.X) / Zoom - this.dx), (int)((e.Y) / Zoom - this.dy), this.r);
                        }
                        else
                        {
                            if (this.shapes.selEle is PointSet)
                            {//POLY MANAGEMENT START
                                shapes.AddPoint();
                                

                                //((PointSet)this.shapes.selEle).RePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.shapes.Fit2Grid(this.gridSize);
                                    
                                    //this.shapes.sRec = new SelPoly(this.shapes.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case "ROT":
                                        this.shapes.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                }//POLY MANAGEMENT END
                            }

                            //Graph nebudu pouzivat
                            /*
                            if (this.shapes.selEle is Graph)
                            {//GRAPH MANAGEMENT START
                                s.AddPoint();
                                //((PointSet)this.s.selEle).rePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.s.Fit2grid(this.gridSize);
                                    //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case "ROT":
                                        this.s.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                }//GRAPH MANAGEMENT END
                            }
                            */

                        }

                        if (this.status == "selrect")
                        {
                            if ((((e.X) / Zoom - this.dx - this.startX) + ((e.Y) / Zoom - this.dy - this.startY)) > 12)
                            {
                                // manage multi objeect selection
                                this.shapes.MultiSelect(this.startX, this.startY, (int)((e.X) / Zoom - this.dx), (int)((e.Y) / Zoom - this.dy), this.r);
                            }
                        }

                        ChangeStatus("");
                        break;
                    #endregion
                    #region Rect
                    case "DR": //DrawRect

                        if (this.status == "drawrect")
                        {
                            this.shapes.AddRect(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            //this.Option = "select";
                            ChangeOption("select");
                        }
                        break;
                    #endregion
                    #region LINk TEST
                    case "LINK": //Link//test
                        if (this.status == "drawrect")
                        {


                            ChangeOption("select");
                        }
                        break;
                    #endregion
                    #region Arc
                    case "ARC": //Arc
                        if (this.status == "drawrect")
                        {
                            this.shapes.AddArc(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            ChangeOption("select");
                        }
                        break;
                    #endregion
                    #region Poly & Pen & Graph
                    case "PEN":
                        //if (this.Status == "drawrect")
                        //{ 
                       
                        this.shapes.AddPoly(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled, penPointList, true, uzavrenaKrivka);
                        penPointList = null;
                        visPenPointList = null;
                        ChangeOption("select");
                        krivka = true;
                        //ChangeStatus("");
                        //}
                        break;
                    case "POLY": //polygon/pointSet/curvedshape..
                       // if (this.status == "drawrect")
                        {
                           
                            // toto je tehdy kdyz jsem nestiskl shift pri zadavani polygonu ...
                            if(penPointList.Count == 1)
                            {
                                penPointList.Add(new PointWrapper(tmpX - startX, tmpY - startY));

                            }
                            
                            
                            shapes.AddPoly(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled, penPointList, false, uzavrenaKrivka);
                            //bb = null;
                            penPointList = null;
                            visPenPointList = null;
                            ChangeOption("select");
                            krivka = true;
                        }
                        break;

                    
                    #endregion
                    #region RRect

                    // Rounded rectangel - zatim nepouzivam
                    /*
                    case "DRR": //DrawRRect

                        if (this.status == "drawrect")
                        {

                            this.shapes.AddRRect(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            //this.Option = "select";
                            ChangeOption("select");
                        }
                        break;
                    */

                    #endregion
                    #region Ellipse
                    // Elpisu zatim nevkladam - pozdeji

                    case "ELL": //DrawEllipse

                        if (this.status == "drawrect")
                        {

                            this.shapes.AddEllipse(startX, startY, tmpX, tmpY, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            //this.Option = "select";
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
                        if (this.status == "drawrect")
                        {
                            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            editorForm.ShowDialog();
                            this.Cursor = System.Windows.Forms.Cursors.Arrow;
                            this.shapes.AddSimpleTextBox(startX, startY, tmpX, tmpY, r, this.creationPenColor, creationFillColor, creationPenWidth, creationFilled);
                            ChangeOption("select");
                        }
                        break;
                    #endregion
                    #region ImgBox
                     
                      // Zatim nepracuju s obrazky
                    
                    case "IB": //DrawImgBox

                        if (status == "drawrect")
                        {
                            // load image                         

                            string f_name = ImgLoader();
                            shapes.AddImageBox(startX, startY, tmpX, tmpY, f_name, this.creationPenColor, creationPenWidth);
                            //this.Option = "select";
                            ChangeOption("select");
                        }
                        break;
                       
                    #endregion
                    #region Line
                    case "LINE": //Draw Line

                        if (status == "drawrect")
                        {

                            shapes.AddLine(startX, startY, tmpX, tmpY, creationPenColor, creationPenWidth);
                            //this.Option = "select";
                            ChangeOption("select");
                        }
                        break;

                        // slepa ulicka - verikala
                        /*
                    case "VLINE":
                        if (status == "drawrect")
                        {

                            shapes.AddVLine(startX, startY, tmpX, tmpY, creationPenColor, creationPenWidth);
                            //this.Option = "select";
                            ChangeOption("select");
                        }
                        break;

                        */
                    #endregion
                    default:
                        //this.Status = "";
                        ChangeStatus("");
                        break;
                }


                // store start X,Y,X1,Y1 of selected item
                if (shapes.selEle != null)
                {
                    
                    if (shapes.selEle is PointSet)
                    {//POLY MANAGEMENT START 
                       // shapes.EndMove();
                        ((PointSet)shapes.selEle).SetupSize();
                        shapes.sRec = new SelPoly(shapes.selEle);//create handling rect
                        
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
                // show properties
                PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                ObjectSelected(this, e1);// raise event

                Redraw(true); //redraw all=true                

                this.mouseSx = false; // end pressing SX
            }
            #endregion
            else
            #region right up
            {
                // show properties
                PropertyEventArgs e1 = new PropertyEventArgs(this.shapes.GetSelectedArray(), this.shapes.RedoEnabled(), this.shapes.UndoEnabled());
                ObjectSelected(this, e1);// raise event

            }
            #endregion
        }


        // 2x klik na prostredni tlacitko = Zoom 100%
        private void Platno_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (fit2grid & gridSize > 0)
                {
                    dx = gridSize * ((100) / gridSize);
                    dy = gridSize * ((100) / gridSize);

                }
                else
                {
                    dx = 100;
                    dy = 100;
                }


                Zoom = 0.25f;
            }
        }


        


        #endregion

        #region Metody PropertyChanged, ShowHelpForm

        public void PropertyChanged()
        {
            shapes.PropertyChanged();       


        }





        // opravid + doplnit - pozdeji .....
        /*
         
        public void HelpForm(string s)
        {
            Help h = new Help();
            h.setMsg(s);
            h.ShowDialog();
        }
        
        
        */

        #endregion

        #region Metody pro tisk

        public void PreviewBeforePrinting(float zoom)
        {
            // InitializePrintPreviewControl(zoom);
        }

        // zprovoznit pozdeji .....

        /*
        public void PrintMe()
        {
            this.s.DeSelect();

            nahledForm = new Anteprima();
            nahledForm.PrintPreviewControl1.Name = "PrintPreviewControl1";
            nahledForm.PrintPreviewControl1.Document = nahledForm.docToPrint;

            nahledForm.PrintPreviewControl1.Zoom = 1;

            nahledForm.PrintPreviewControl1.Document.DocumentName = "Anteprima";

            nahledForm.PrintPreviewControl1.UseAntiAlias = true;

            nahledForm.docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(
                docToPrint_PrintPage);

            // Per stampare
            nahledForm.docToPrint.Print();

            nahledForm.Dispose();

        }

        private void InitializePrintPreviewControl(float zoom)
        {
            this.s.DeSelect();

            nahledForm = new Anteprima();
            // Construct the PrintPreviewControl.
            //AnteprimaFrm.PrintPreviewControl1 = new PrintPreviewControl();

            // Set location, name, and dock style for PrintPreviewControl1.
            //AnteprimaFrm.PrintPreviewControl1.Location = new Point(88, 80);
            nahledForm.PrintPreviewControl1.Name = "Preview";
            //AnteprimaFrm.PrintPreviewControl1.Dock = DockStyle.Fill;

            // da testare??
            //AnteprimaFrm.PrintPreviewControl1.BackColor = this.BackColor;
            //AnteprimaFrm.PrintPreviewControl1.BackgroundImage = this.BackgroundImage;


            // Set the Document property to the PrintDocument 
            // for which the PrintPage event has been handled.
            nahledForm.PrintPreviewControl1.Document = nahledForm.docToPrint;

            // Set the zoom to 25 percent.
            nahledForm.PrintPreviewControl1.Zoom = zoom;

            // Set the document name. This will show be displayed when 
            // the document is loading into the control.
            nahledForm.PrintPreviewControl1.Document.DocumentName = "Preview";

            // Set the UseAntiAlias property to true so fonts are smoothed
            // by the operating system.
            nahledForm.PrintPreviewControl1.UseAntiAlias = true;

            // Add the control to the form.
            //AnteprimaFrm.Controls.Add(AnteprimaFrm.PrintPreviewControl1);

            // Associate the event-handling method with the
            // document's PrintPage event.
            nahledForm.docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(
                docToPrint_PrintPage);

            // Per stampare
            //AnteprimaFrm.docToPrint.Print();
            nahledForm.ShowDialog();
        }

        // The PrintPreviewControl will display the document
        // by handling the documents PrintPage event
        private void docToPrint_PrintPage(
            object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            // Insert code to render the page here.
            // This code will be called when the control is drawn.

            // The following code will render a simple
            // message on the document in the control.
            //string text = "In docToPrint_PrintPage method.";
            //System.Drawing.Font printFont =
            //    new Font("Arial", 35, FontStyle.Regular);
            //  e.Graphics.DrawString(text, printFont,
            //      Brushes.Black, 10, 10);



            Graphics g = e.Graphics;




            //Do Double Buffering
            //Bitmap offScreenBmp;
            // Graphics offScreenDC;
            //offScreenBmp = new Bitmap(this.Width, this.Height);
            //
            //offScreenDC = Graphics.FromImage(offScreenBmp);

            //offScreenDC.Clear(this.BackColor);

            //background image
            //if ((this.loadImage) & (this.visibleImage))
            //    offScreenDC.DrawImage(this.loadImg, 0, 0);
            //

            //offScreenDC.SmoothingMode=SmoothingMode.AntiAlias;

            // test
            //MessageBox.Show("dipx : " + g.DpiX + " ; dipy : " + g.DpiY);

            //s.Draw(offScreenDC);
            if (this.BackgroundImage != null)
                g.DrawImage(this.BackgroundImage, 0, 0);

            //TEST !!!!!!!!!!!!!!!!!!!!!!!!!!!1
            //this.DrawMesure(g);

            s.Draw(g, 0, 0, 1);

            //if (this.MouseSx)
            //{
            //    System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            //    offScreenDC.DrawRectangle(myPen, new Rectangle(this.startX, this.startY, tmpX - this.startX, tmpY - this.startY));
            //    myPen.Dispose();
            //}

            //g.DrawImageUnscaled(offScreenBmp, 0, 0);

            g.Dispose();
        }

        */
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
                kolikNasobneZoom = (int)Zoom;

            }
        }

        public void ZoomOut()
        {
            

            if (Zoom > 0.21f && Zoom <= 21f)
            {
                Zoom = (float)(Zoom / 2);
                
                kolikNasobneZoom = (int)Zoom;

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




        private void MyOnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            

            int index = 1;
            

            if (e.Delta < 0 && (Zoom > 0.21f && Zoom <= 21f) )
            {
                if (fit2grid & gridSize > 0)
                {
                    //int gr = gridSize;
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));

                    dx = gridSize * ((dx) / gridSize);
                    dy = gridSize * ((dy) / gridSize);

                    ZoomOut();
                    //gridSize = gr;
                }
                else
                {
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));
                    ZoomOut();
                }



                /*
                if (gridSize > 0)
                {
                    int gr = gridSize;
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));
                    ZoomOut();
                    gridSize = gr;
                }
                else
                {
                    dx = (int)(dx + (e.X / (Zoom)));
                    dy = (int)(dy + (e.Y / (Zoom)));
                    ZoomOut();
                }
                */




                /*
                if (fit2grid & gridSize > 0)
                {

                    shapes.Fit2Grid(gridSize);
                    shapes.sRec.Fit2grid(gridSize);
                }

                */

                // ZoomOut();



            }
            else if (e.Delta > 0 && (Zoom <= 15f))
            {


                if (fit2grid & gridSize > 0)
                {
                    //int grid = gridSize;
                    dx = (int)(dx - (e.X / (Zoom * 2)));
                    dy = (int)(dy - (e.Y / (Zoom * 2)));

                    dx = gridSize * ((dx) / gridSize); 
                    dy = gridSize * ((dy) / gridSize);

                    ZoomIn();
                    //gridSize = grid;
                }else
                {
                    dx = (int)(dx - (e.X / (Zoom * 2)));
                    dy = (int)(dy - (e.Y / (Zoom * 2)));
                    ZoomIn();
                }

               
                
                /*
                
                if (gridSize > 0)
                {

                    dx = gridSize * (int)(dx - (e.X / (Zoom * 2)))/gridSize;
                    dy = gridSize * (int)(dy - (e.Y / (Zoom * 2)))/gridSize;
                }
                else
                {
                    dx = (int)(dx - (e.X / (Zoom * 2)));
                    dy = (int)(dy - (e.Y / (Zoom * 2)));
                }
                
                */


                //ZoomIn();

                /*
                if (fit2grid & gridSize > 0)
                {

                    shapes.Fit2Grid(gridSize);
                    shapes.sRec.Fit2grid(gridSize);
                }
                */


            }

            if (Zoom == 0.125f)
                index = 0;
            else if (Zoom == 0.25f)
                index = 1;
            else if (Zoom == 0.5d)
                index = 2;
            else if (Zoom == 1f)
                index = 3;
            else if (Zoom == 2f)
                index = 4;
            else if (Zoom == 4f)
                index = 5;
            else if (Zoom == 8f)
                index = 6;
            else if (Zoom == 16f)
                index = 7;

            UpravZoomVComboBoxu(index);


            //UpravZoomVComboBoxu(index);


        }

        


        #endregion


        #region Image Loader

        // zakladni vstup obrazku zde touto metodou...
        public string ImgLoader()
        {
            try
            {
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.Title = "Load background image";
                DialogueCharger.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                //DialogueCharger.DefaultExt = "frame";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    return (DialogueCharger.FileName);
                }
            }
            catch { }
            return null;
        }






        #endregion

        
    }
}
