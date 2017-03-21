using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using Zahrada.PomocneTridy;


namespace Zahrada
{
	
	/// <summary>
	/// Zakladni abstraktni trida Ele - je to nejaky Element od ktereho se dedi dalsi tridy pro kresleni
	/// </summary>
	
	public abstract class Ele
	{
		#region Clenske promenne tridy Ele
		// pozor - nektere clenske promenne nemam spravne zapouzdrene - pres vlastnosti Get/Set
		// copak je to dpix a dpiy - ??? - nevim to, zjistit je potreba
		public static float dpix; // public - tento clen je pristupny absolutne odevsad
		public static float dpiy; // static - v kazdem elementu Ele je tento clen stejny - staticky

		protected bool IamGroup = false; // protected = tento clen je pristupny z abstarktni tridy a ve vsech zdedenych tridach
		protected bool rot; // Muze rotovat

		// private - pristupny pouze zevnitr teto jedne tridy
		// internal - clenove pristupni pouze z aktualniho sestaveni - tzn. pouze z jeddnoho exe souboru nebo dll knihovny

		// startovaci bod elementu
		protected int X;
		protected int Y;

		// koncovy bod elementu
		protected int X1;
		protected int Y1;

		// Vycty vlastnosti (enumerace) pro Spojovane objekty (Grouped objects) - jejich vlastnosti {0,1,2, pripadne 3}
		public enum OnGroupResize { Move, Resize, Nothing };
		public enum GroupDisplay { Default, Intersect, Xor, Exclude };

		// Zmena velikosti objektu pouzivajici X souradnici (zapadni-West)
		protected OnGroupResize _onGroupXRes = OnGroupResize.Resize;

		// Zmena velikosti objektu pouzivajici X1 souradnici (vychodni - East)
		protected OnGroupResize _onGroupX1Res = OnGroupResize.Resize;

		// Zmena velikosti objektu pouzivajici Y souradnici (severni - North)
		protected OnGroupResize _onGroupYRes = OnGroupResize.Resize;

		// Zmena velikosti objektu pouzivajici Y souradnici (jizni- South)
		protected OnGroupResize _onGroupY1Res = OnGroupResize.Resize;

		// Pri dvojkliku na Group objket - posunout objket dopredu do sub-objektu
		protected bool _onGroupDoubleClick = true;

		// virtual (možná metoda, virtuální)= virtualni metoda - je metoda definovana v teto zakladni tride a muze byt prepsana v odvozenych tridach: public virtual Neco()
		// override (potlacení metody) = u odvozenych trid se muze prepisovat puvodni virtual metoda. A to tak, ze se pred nazev metody da: public virtual Neco()
		// toto je příklad polymorfismu !!!

		// Ukladani startovaci pozice behem moving/resizing - presinovani/zmeny velikosti
		protected int startX;
		protected int startY;
		protected int startX1;
		protected int startY1;

		// nejake PEN vlastnosti - tyka se to pera:
		// ukoncovani linek uzaverem ve tvaru ctverecek/kolecko/diamant apod.
		protected LineCap start;
		protected LineCap end;
		protected DashStyle dashstyle; // carkovana care jako vypln objektu

		protected int _rotation; // to nevim co je za clena
		private Color _penColor;
		private float _penWidth;
		private Color _fillColor;
		private bool _colorFilled;
        private bool _textureFilled;
		private bool _showBorder;
		private DashStyle _dashStyle; // proc je to uvedeno podruhe ?
		private int _aplha;
		private bool _closed;

		// pouziti Textury:
		private TextureBrush _texture;

		// Linear Gradient:
		private bool _useGradientLine = false;
		private Color _endColor = Color.White;
		private int _endalpha = 255;
		private int _gradientAngle = 0;
		private int _gradientLen = 0;
		private float _endColorPos = 1f;

		// Group objekt - zoomovani:
		protected float gprZoomX = 1f;
		protected float gprZoomY = 1f;
		public bool iAmAline; // jsem linkou
		public bool selected;
		public bool deleted;
		public Ele undoEle;

        // cesta k me nove texture pro tvary ...
        private string filePath;
        #endregion

        #region Konstruktor tridy Ele
        public Ele()
		{
            // nastavuju si zde vsechny pocatecni vlastnosti abstraktniho objektu Ele



            // predvyplnena textura pro Element
            //Image obr = GetImageByName("trava-velmi-husta"); // ... tohle mel byt pokus univerzlani ....


            Image obr = Properties.Resources.trava_velmi_husta; // toto funguje dobre
			TextureBrush tBrush = new TextureBrush(obr);
			tBrush.WrapMode = WrapMode.Tile;
			FillTexture = tBrush;

			// predvyplnena barva pro Element
			FillColor = Color.Black;

			// zakladni barva pera - Cerna
			PenColor = Color.Black;

			// ostattni predvyplnene vlastnosti elementu
			PenWidth = 1f;			
			ColorFilled = false;
			ShowBorder = true;
			DashStyleMy = DashStyle.Solid;
			Alpha = 255;    
		}

		// Tohle mel byt pokus jak dostat obrazek y Resources
		// blbost to byla ....
		/*
		public static Bitmap GetImageByName(string imageName)
		{
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			string resourceName = asm.GetName().Name + ".Properties.Resources";
			var rm = new System.Resources.ResourceManager(resourceName, asm);
			return (Bitmap)rm.GetObject(imageName);

		}
		*/

		#endregion

		#region Destruktor tridy Ele - jen pro zajimavost

		~Ele()
		{
			string vystup = "Znicene objekty Carbage Collectorem: " + GetX.ToString() + " " + GetY.ToString();
			System.Diagnostics.Trace.WriteLine(vystup);
		}
		#endregion

		#region Základní vlastnosti - neboli GETTERY pro tridu Ele, SETTERY mam pouze v zaloze
		public int GetX
		{
			get { return X; }
			//set { X = value; }            
		}

		public int GetY
		{
			get { return Y; }
			// set { Y = value; }           
		}

		public int GetX1
		{
			get { return X1; }
			// set { X1 = value; }
		}


		public int GetY1
		{
			get { return Y1; }
			//set { Y1 = value; }
		}

		public float GetGprZoomX
		{
			get { return gprZoomX; }
			// set { gprZoomX = value; }
		}

		public float GetGprZoomY
		{
			get { return gprZoomY; }
			// set { gprZoomY = value; }
		}

		public bool CanRotate
		{
			get { return rot; }
			// set { rot = value; }
		}

		public int GetRotation
		{
			get
			{
				if (CanRotate)
					return _rotation;
				else
					return 0;
			}
		}

		public int SetRotation
		{
			set
			{
				if (CanRotate)
					_rotation = value;
			}
		}

		public bool AmIaGroup
		{
			get { return IamGroup; }
			// set { IamGroup = value;}
		}

        




        #endregion

        #region Vlastnosti, kterym jsem priradil navic jmeno kategorie a description - pro muj Property Grid		

        //[DisplayName("Orientace")]
        [Category("Pozice"), Description("X ")]
		public int PosX
		{
			get
			{
				return X;
			}
		}

		[ Category("Pozice"), Description("Y ")]
		public int PosY
		{
			get
			{
				return Y;
			}
		}

		[Category("Rozměry"), Description("Šířka ")]
		public int Sirka
		{
			get
			{
				return Math.Abs(X1 - X);
			}
		}
		
		[Category("Rozměry"), Description("Výška ")]
		public int Vyska
		{
			get
			{
				return Math.Abs(Y1 - Y);
			}
		}


		[Category("Rozměry mm"), Description("Šířka (mm)")]
		public int Sirka_mm
		{
			get
			{
				return (int)(Math.Abs(X1 - X) / dpix * 25.4); // procpak to je takovyto vzorecnevim
			}
		}


		[Category("Rozměry mm"), Description("Výška (mm)")]
		public int Vyska_mm
		{
			get
			{
				return (int)(Math.Abs(Y1 - Y) / dpiy * 25.4); // procpak to je takovyto vzorecnevim
			}
		}

		[Category("Rozměry mm"), Description("toto je dpix")]
		public float dpiX
		{
			get
			{
				return dpix;
			}
		}

		[Category("Rozměry mm"), Description("toto je dpiy")]
		public float dpiY
		{
			get
			{
				return dpiy;
			}
		}

		//[Category("Group chování"), Description("On Group Resize X")]
		public OnGroupResize OnGrpXRes
		{
			get
			{
				return _onGroupXRes;
			}
			set
			{
				_onGroupXRes = value;
			}
		}

		//[Category("Group chování"), Description("On Group Resize X1")]
		public OnGroupResize OnGrpX1Res
		{
			get
			{
				return _onGroupX1Res;
			}
			set
			{
				_onGroupX1Res = value;
			}
		}

		//[Category("Group chování"), Description("On Group Resize Y")]
		public OnGroupResize OnGrpYRes
		{
			get
			{
				return _onGroupYRes;
			}
			set
			{
				_onGroupYRes = value;
			}
		}

		//[Category("Group chování"), Description("On Group Resize Y1")]
		public OnGroupResize OnGrpY1Res
		{
			get
			{
				return _onGroupY1Res;
			}
			set
			{
				_onGroupY1Res = value;
			}
		}

		//[Category("Group chování"), Description("Manage On Group Double Click")]
		public bool OnGrpDClick
		{
			get
			{
				return _onGroupDoubleClick;
			}
			set
			{
				_onGroupDoubleClick = value;
			}
		}

		#endregion

		#region Vlastnosti do Property Gridu typu "virtual", ktere navic budu pozdeji prepisovat ve zdeddenych tridach Ele
		[Category("Vzhled"), Description("Nastaví styl hraniční čáry")]
		public virtual DashStyle DashStyleMy
		{
			get
			{
				return _dashStyle;
			}
			set
			{
				_dashStyle = value;
			}
		}

		[Category("Vzhled"), Description("Uzavřený Vypnout / Zapnout")]
		public virtual bool EleClosed
		{
			get
			{
				return _closed;
			}
			set
			{
				_closed = value;
			}
		}

		[Category("Vzhled"), Description("Ukaž hraniční čáru když je objekt vyplněný nebo obsahuje Text")]
		public virtual bool ShowBorder
		{
			get
			{
				return _showBorder;
			}
			set
			{
				_showBorder = value;
			}
		}

		[Category("Vzhled"), Description("Nastav barvu Pera")]
		public virtual Color PenColor
		{
			get
			{
				return _penColor;
			}
			set
			{
				_penColor = value;
			}
		}

        // jakou barvoy vyplneny:
		[Category("Vzhled"), Description("Nastav barvu výplně")]
		public virtual Color FillColor
		{
			get
			{
				return _fillColor;
			}
			set
			{
				_fillColor = value;
			}
		}

       

        
        // nastavuju cestu k nove texture ... pouzivam zde svou pomocnou tridu FileLocationEditor
        [Category("Vyber Texturu"), Description("Textura vyberem souboru ")]
        [Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
        public string FilePath
        {
            get
            {
                //string cesta = _texture.Image(FilePath);
                return filePath;
            }
            set
            {
                filePath = value;                
                Bitmap bitm = new Bitmap(filePath);   
                TextureBrush tBrush = new TextureBrush(bitm);
                tBrush.WrapMode = WrapMode.Tile;
                FillTexture = tBrush;
            }
        }

        // Nastavuju texturu
        public virtual TextureBrush FillTexture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }




        // vyplneny texturou ANO/ NE
        [Category("Vzhled"), Description("Vyplněný Vypnout / Zapnout")]
        public virtual bool TextureFilled
        {
            get
            {
                return _textureFilled;
            }
            set
            {
                _textureFilled = value;
            }
        }



        // vyplneny barvou ANO/NE
        [Category("Vzhled"), Description("Vyplněný Vypnout / Zapnout")]
		public virtual bool ColorFilled
		{
			get
			{
				return _colorFilled;
			}
			set
			{
				_colorFilled = value;
			}
		}


        [Category("Vzhled"), Description("Nastav šířku pera")]
        public virtual float PenWidth
        {
            get
            {
                return _penWidth;
            }
            set
            {
                _penWidth = value;
            }
        }


        [Category("Vzhled"), Description("Průhlednost")]
		public virtual int Alpha
		{
			get
			{
				return _aplha;
			}
			set
			{
				if (value < 0)
					_aplha = 0;
				else
					if (value > 255)
					_aplha = 255;
				else _aplha = value;

			}
		}

		//[Category("GradientBrush"), Description("True: Use gradient fill color")]
		public virtual bool UseGradientLineColor
		{
			get
			{
				return _useGradientLine;
			}
			set
			{
				_useGradientLine = value;
			}
		}

		//[Category("GradientBrush"), Description("End Color Position [0..1]")]
		public virtual float EndColorPosition
		{
			get
			{
				return _endColorPos;
			}
			set
			{
				if (value > 1)
					value = 1;
				else if (value < 0)
					value = 0;
				_endColorPos = value;
			}
		}

		//[Category("GradientBrush"), Description("Gradient End Color")]
		public virtual Color EndColor
		{
			get
			{
				return _endColor;
			}
			set
			{
				_endColor = value;
			}
		}

		//[CategoryAttribute("GradientBrush"), DescriptionAttribute("End Color Trasparency")]
		public virtual int EndAlpha
		{
			get
			{
				return _endalpha;
			}
			set
			{
				if (value < 0)
					_endalpha = 0;
				else
					if (value > 255)
					_endalpha = 255;
				else
					_endalpha = value;
			}
		}

		//[CategoryAttribute("GradientBrush"), DescriptionAttribute("Gradient Dimension")]
		public virtual int GradientLen
		{
			get
			{
				return _gradientLen;
			}
			set
			{
				if (value >= 0)
					_gradientLen = value;
				else
					_gradientLen = 0;
			}
		}

		//[CategoryAttribute("GradientBrush"), DescriptionAttribute("Gradient Angle")]
		public virtual int GradientAngle
		{
			get
			{
				return _gradientAngle;
			}
			set
			{
				_gradientAngle = value;
			}
		}




		#endregion

		#region Verejne pristupne metody - Public - pro tridu Ele

		public void AddRotation(int a)

		{
			SetRotation = (GetRotation + a);
		}

		// metoda pro resize textury v pozadi Elementu
		public static Image resizeImage(Image imgToResize, Size size)
		{
			return (Image)(new Bitmap(imgToResize, size));
		}
        // jakou TEXTUROU vyplnen

        


        #endregion

        #region Virtualni + verejne pristupne metody pro tridu Ele - Public Virtual - (k prepsani override v potomcich teto tridy)

        /// <summary>
        /// Nakresli tento Element do objektu Graphics
        /// </summary>        
        public virtual void Draw(Graphics g, int dx, int dy, float zoom)
		{ }



		/// <summary>
		/// Nakresli tento Element do Graphics Path
		/// </summary>
		public virtual void AddGraphPath(GraphicsPath gp, int dx, int dy, float zoom)
		{
			GraphicsPath tmpGp = new GraphicsPath();
			AddGp(tmpGp, dx, dy, zoom);
			Matrix translateMatrix = new Matrix();
			translateMatrix.RotateAt(_rotation, new PointF((X + dx + (X1 - X) / 2) * zoom, (Y + dy + (Y1 - Y) / 2) * zoom));
			tmpGp.Transform(translateMatrix);
			gp.AddPath(tmpGp, true);
		}

		/// <summary>
		/// Tato metoda AddGp je urcena k potlaceni - override - ve zdedenych tridach
		/// </summary>        
		public virtual void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
		{ }

		/// <summary>
		/// Pouziti jako rozlozeni skupiny Elementu. Vraci seznam Elementu
		/// </summary>
		public virtual ArrayList DeGroup()
		{
			return null;
		}

		/// <summary>
		///  Vyber tento Element
		/// </summary>
		public virtual void Select()
		{ }


		/// <summary>
		/// Vyber tento Rich Text Box
		/// </summary>
		public virtual void Select(RichTextBox r)
		{ }

		/// <summary>
		/// Vyber podle parametru
		/// </summary>
		public virtual void Select(int sX, int sY, int eX, int eY)
		{ }

		/// <summary>
		/// Zrus vyber
		/// </summary>
		public virtual void DeSelect()
		{ }

		// Zde Musim implementovat vlastni RichForm editor textu RTF
		/*
		/// <summary>
		/// Toto pouziju pro editor textu vkladaneho do obrazku
		/// </summary>
		public virtual void ShowEditor(richForm f)
		{ }
		*/

		/// <summary>
		/// Pouzije se po nahrani ze souboru. Zpracuj zde vytvoreni objektu, ktery neni serializovany
		/// </summary>
		public virtual void AfterLoad()
		{ }

		/// <summary>
		/// Zkopiruj vlastnosti z jineho Elementu
		/// </summary>
		public virtual void CopyFrom(Ele ele)
		{ }


		/// <summary>
		/// Klonuj tento Element
		/// </summary>
		public virtual Ele Copy()
		{
			return null;
		}


		/// <summary>
		/// Adaptuj Element na velikost mrizky (Grid)
		/// </summary>
		/// <param name="gridsize"></param>
		public virtual void Fit2grid(int gridsize)
		{
			/*
			startX = (int)(startX *25.4/dpix);
			startY = (int)(startY*25.4 / dpiy);
			startX1 = (int)(startX1 *25.4 /dpix);
			startY1 = (int)(startY1*25.4 /dpiy);
			*/

			
			startX = gridsize * (int)(startX / gridsize);
			startY = gridsize * (int)(startY / gridsize);
			startX1 = gridsize * (int)(startX1 / gridsize);
			startY1 = gridsize * (int)(startY1 / gridsize);
			
		}

		

		/// <summary>
		/// Rotuj Element podle bodu (x,y)
		/// </summary>
		public virtual void Rotate(float x, float y)
		{
			float tmp = _Rotate(x, y);
			_rotation = (int)tmp;
		}


		/// <summary>
		/// Potvrdit rotaci Elementu podle bodu (x,y)
		/// </summary>
		public virtual void CommitRotate(float x, float y)
		{ }

		/// <summary>
		/// Potvrdit zrcadleni Elementu podle xmirr nebo ymirr nebo oboji
		/// </summary>
		public virtual void CommitMirror(bool xmirr, bool ymirr)
		{ }

		/// <summary>
		/// Vraci pravdu, pokud Element obsahuje v sobe bod (x,y)
		/// </summary>
		public virtual bool Contains(int x, int y)
		{
			if(iAmAline)
			{
				int appo = Dist(x, y, X, Y) + Dist(x, y, X1, Y1);
				int appo1 = Dist(X1, Y1, X, Y) + 7;

				return appo < appo1;
			}
			else
			{
				return new Rectangle(X, Y, X1 - X, Y1 - Y).Contains(x, y); // rekurzivni volani Contains
			}
		}

		/// <summary>
		/// Posun Elementu o hodnotu x,y
		/// </summary>
		public virtual void Move(int x, int y)
		{
			X = startX - x;
			Y = startY - y;
			X1 = startX1 - x;
			Y1 = startY1 - y;
		}

		/// <summary>
		/// Zmena rozmeru Elementu podle toho jakym smerem podle svetovych stran a o kolik
		/// </summary>
		public virtual void Redim(int x, int y, string redimSt)
		{
			switch(redimSt)
			{
				case "NW":
					X = startX + x;
					Y = startY + y;
					break;
				case "N":
					Y = startY + y;
					break;
				case "NE":
					X1 = startX1 + x;
					Y = startY + y;
					break;
				case "E":
					this.X1 = this.startX1 + x;
					break;
				case "SE":
					X1 = startX1 + x;
					Y1 = startY1 + y;
					break;
				case "S":
					Y1 = startY1 + y;
					break;
				case "SW":
					X = startX + x;
					Y1 = startY1 + y;
					break;
				case "W":
					X = startX + x;
					break;
				default:
					break;
			}

			if (!iAmAline)  // tady nastavuju nejmensi limity na zmensovani Elementu
			{  
				if (X1 <= X)
					X1 = X + 10;
				if (Y1 <= Y)
					Y1 = Y + 10;
			}

		}

		/// <summary>
		/// Volano na konci po dokonceni Move/Redim Elemenetu. Uklada startovaci hodnoty
		/// srartX|Y|X1|Y1 pro spravne renderovani behem pohybu Elementu pri akci Move/Redim
		/// </summary>
		public virtual void EndMoveRedim()
		{
			startX = X;
			startY = Y;
			startX1 = X1;
			startY1 = Y1;

		}

		#endregion

		#region Chranene - Protected - Metody pro tridu Ele (ktere dedi uplne stejne potomci tridy Ele)
		/// <summary>
		/// Z daneho objektu Element zkopiruje gradient vlastnosti
		/// </summary>
		/// <param name="ele"></param>
		protected void CopyGradProp(Ele ele)
		{
			_useGradientLine = ele._useGradientLine;
			_endColor = ele._endColor;
			_endalpha = ele._endalpha;
			_gradientLen = ele._gradientLen;
			_gradientAngle = ele._gradientAngle;
			_endColorPos = ele._endColorPos;

		}


		/// <summary>
		/// Vyplnit Element paralelnimi linkami (lines)
		/// </summary>
		protected void FillWithLines(Graphics g, int dx, int dy, float zoom, GraphicsPath myPath, float gridSize, float gridRot)
		{
			GraphicsState gs = g.Save();//store previos trasformation
			g.SetClip(myPath, CombineMode.Intersect);
			Matrix mx = g.Transform; // get previous trasformation
			PointF p = new PointF(zoom * (X + dx + (X1 - X) / 2), zoom * (Y + dy + (Y1 - Y) / 2));
			if (_rotation > 0)
				mx.RotateAt(_rotation, p, MatrixOrder.Append); //add a trasformation
			mx.RotateAt(gridRot, p, MatrixOrder.Append); //add a trasformation
			g.Transform = mx;

			int max = Math.Max(Sirka, Vyska);
			Pen linePen = new Pen(Color.Gray);
			//linePen.DashStyle = DashStyle.Dash;
			int nY = (int)(max * 3 / (gridSize));
			for (int i = 0; i <= nY; i++)
			{
				g.DrawLine(linePen, (X - max + dx) * zoom, (Y - max + dy + i * gridSize) * zoom, (X + dx + max * 2) * zoom, (Y - max + dy + i * gridSize) * zoom);
			}
			linePen.Dispose();
			g.Restore(gs);
			//g.ResetClip();
		}

		/// <summary>
		/// Pouziva se k definici tloustky pera
		/// </summary>
		protected float ScaledPenWidth(float zoom)
		{
			if (zoom < 0.1f) // pozor tady si menim zobrazovanou tlousku vsech elementu pri Zoomovani 
				zoom = 0.1f; // sandardni tloustka 1 pixel - vpohode
			return PenWidth * zoom;
		}


		/// <summary>
		/// Prebira uhel rotace z vertikalni linky z centra Elementu a z linky z centra Elementu do bodu (x,y)
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		protected float _Rotate(float x, float y)
		{
			Point c = new Point((X + (X1 - X) / 2), (Y + (Y1 - Y) / 2));
			float dx = x - c.X;
			float dy = y - c.Y;
			float b = 0f;
			float alpha = 0f;
			float f = 0f;
			if ((dx > 0) & (dy > 0))
			{
				b = 90;
				alpha = (float)Math.Abs((Math.Atan((dy / dx)) * (180 / Math.PI)));
			}
			else
				if ((dx <= 0) & (dy >= 0))
			{
				b = 180;
				if (dy > 0)
				{
					alpha = (float)Math.Abs((Math.Atan((dx / dy)) * (180 / Math.PI)));
				}
				else if (dy == 0)
				{
					b = 270;
				}
			}
			else
					if ((dx < 0) & (dy < 0))
			{
				b = 270;
				alpha = (float)Math.Abs((Math.Atan((dy / dx)) * (180 / Math.PI)));
			}
			else
			{
				b = 0;
				alpha = (float)Math.Abs((Math.Atan((dx / dy)) * (180 / Math.PI)));
			}
			f = (b + alpha);
			return f;
		}


		/// <summary>
		/// Vraci bod ziskany rotaci bodu p podle uhlu rotAng, respektive (0,0)
		/// </summary>
		/// <param name="p"></param>
		/// <param name="rotAng"></param>
		/// <returns></returns>
		protected PointF RotatePoint(PointF p, int rotAng)
		{
			double rotAngF = rotAng * Math.PI / 180;
			double sinVal = Math.Sin(rotAngF);
			double cosVal = Math.Cos(rotAngF);
			float nX = (float)(p.X * cosVal - p.Y * sinVal);
			float nY = (float)(p.Y * cosVal + p.X * sinVal);
			return new PointF(nX, nY);
		}

		/// <summary>
		/// Nastavi barvu c podle alfa kanalu v - pripadne nastavi nepruhlednost
		/// </summary>
		protected Color Transparency(Color c, int v)
		{
			if (v < 0)
				v = 0;
			if (v > 255)
				v = 255;
			return Color.FromArgb(v, c);
		}

		// doplneno at mi Ele vrati texture Brush !!
		protected TextureBrush GetTextureBrush()
		{
			return FillTexture;
		}

		/// <summary>
		/// Vybere stetec Brush z vlastnosti Elementu (pokud je Element vyplnen Filled)
		/// </summary>
		protected Brush GetBrush(int dx, int dy, float zoom)
		{
			if (ColorFilled)
			{
				if (UseGradientLineColor)
				{
					float wid;
					float hei;
					if (GradientLen > 0)
					{
						wid = GradientLen;
						hei = GradientLen;
					}
					else
					{
						wid = ((X1 - X) + 2 * GetDimX()) * zoom;
						hei = ((Y1 - Y) + 2 * GetDimY()) * zoom;
					}
					LinearGradientBrush br = new LinearGradientBrush(
						new RectangleF((X - GetDimX() + dx) * zoom, (Y - GetDimY() + dy) * zoom, wid, hei), 
						Transparency(FillColor, Alpha), Transparency(EndColor, EndAlpha), GradientAngle, true);

					br.SetBlendTriangularShape(EndColorPosition, 0.95f);
					br.WrapMode = WrapMode.TileFlipXY;
					return br;
				}
				else
				{
					return new SolidBrush(Transparency(FillColor, Alpha));
					//return FillTexture;
				   
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Kopirovani standardnich vlastnosti, ktere jsou platne pro vsechny Elementy
		/// </summary>
		protected void CopyStdProp(Ele from, Ele to)
		{
			// nezapouzdrene clenske promenne:
			to.X = from.X;
			to.X1 = from.X1;
			to.Y = from.Y;
			to.Y1 = from.Y1;
			to.start = from.start;
			to.startX = from.startX;
			to.startX1 = from.startX1;
			to.startY = from.startY;
			to.startY1 = from.startY1;
			to.iAmAline = from.iAmAline;
			to.dashstyle = from.dashstyle;

			to._onGroupX1Res = from._onGroupX1Res;
			to._onGroupXRes = from._onGroupXRes;
			to._onGroupY1Res = from._onGroupY1Res;
			to._onGroupYRes = from._onGroupYRes;

			to._useGradientLine = from._useGradientLine;
			to._endColor = from._endColor;
			to._endalpha = from._endalpha;
			to._gradientLen = from._gradientLen;
			to._gradientAngle = from._gradientAngle;
			to._endColorPos = from._endColorPos;

			// zapouzdrene clenske promenne:
			to.Alpha = from.Alpha;			
			to.FillColor = from.FillColor;
			to.ColorFilled = from.ColorFilled;
			to.PenColor = from.PenColor;
			to.PenWidth = from.PenWidth;
			to.ShowBorder = from.ShowBorder;	
			
		}

		/// <summary>
		/// Vraci vzdalenost 2 bodu
		/// </summary>
		protected int Dist(int x, int y, int x1, int y1)
		{
			return (int)Math.Sqrt(Math.Pow((x - x1), 2) + Math.Pow((y - y1), 2));
		}


		/// <summary>
		/// Barvu ztmavuje nebo zesvetluje
		/// </summary>
		protected Color Dark(Color c, int v, int a)
		{
			int r = c.R;
			r = r - v;
			if (r < 0)
				r = 0;
			if (r > 255)
				r = 255;
			int green = c.G;
			green = green - v;
			if (green < 0)
				green = 0;
			if (green > 255)
				green = 255;
			int b = c.B;
			b = b - v;
			if (b < 0)
				b = 0;
			if (b > 255)
				b = 255;
			if (a > 255)
				a = 255;
			if (a < 0)
				a = 0;

			return Color.FromArgb(a, r, green, b);
		}

		




		#endregion

		#region Soukrome metody - Private - pro tridu Ele

		/// <summary>
		/// Vraci hodnotu X souradnice Elementu - nevim kde vsude se tato metoda pouziva
		/// </summary>        
		private float GetDimX()
		{
			return (float)(Math.Sqrt(Math.Pow(Sirka, 2) + Math.Pow(Vyska, 2)) - Sirka) / 2;
		}


		/// <summary>
		/// Vraci hodnotu Y souradnice Elementu - nevim kde vsude se tato metoda pouziva
		/// </summary>
		private float GetDimY()
		{
			return (float)(Math.Sqrt(Math.Pow(Sirka, 2) + Math.Pow(Vyska, 2)) - Vyska) / 2;
		}





		#endregion

	}


	// pak nasledovaly tridy (vceten straslivych metod pro ne), ktere nebyly nikdy v projektu pouzity.
	// public class AbSel : Ele - nastroj pro redim/move/rotate
	// public class SelRectBK : Ele - nastroj pro redim/move/rotate
	// public class OLine : Linea - horizontalni line
	// public class VLine : Linea - vertikalni line



}
