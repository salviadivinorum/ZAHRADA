﻿using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Zahrada.PomocneTridy;


namespace Zahrada
{
	
	/// <summary>
	/// Zakladni abstraktni trida Ele - je to fitkivni Element od ktereho se dedi dalsi tridy pro kresleni
	/// </summary>
	[Serializable]
	public abstract class Ele
	{
		#region Clenske promenne tridy Ele		
		// poznamka k zpouzdreni mych promennych ....
		// private - pristupny pouze zevnitr teto jedne tridy
		// internal - clenove pristupni pouze z aktualniho sestaveni - tzn. pouze z jednoho exe souboru nebo dll knihovny
		// public - tento clen je pristupny absolutne odevsad
		// static - v kazdem dedenem elementu Ele je tento clen stejny - staticky
		// Abstract modifikator - znaci, ze to je trida, ktera je urcena pouze jako korenova trida pro dalsi tridy. 
		// Abstraktni trida vyzaduje dedicnost dalsich trid. Abstraktni trida nemuze byt instancovana !
		// Sealed modifikator - znaci, ze to je trida, ktera zakazuje dedicnost od sebe. Tento modif. nepouzivam ve svem reseni.


		// poznamka k memu polymorfismu .....
		// virtual (možná metoda, virtuální)= virtualni metoda - je metoda definovana v teto zakladni tride a muze byt prepsana v odvozenych tridach: public virtual Blabla()
		// override (potlacení metody) = u odvozenych trid se muze prepisovat puvodni virtual metoda. A to tak, ze se pred nazev metody da: public override Blabla()
		// protected = tento clen je pristupny z abstarktni tridy a ve vsech zdedenych tridach

		protected bool IamGroup = false; 
		protected bool rot; // Muze rotovat

		// startovaci bod elementu
		protected int X;
		protected int Y;

		// koncovy bod elementu
		protected int X1;
		protected int Y1;

		// pozor - nektere clenske promenne nemam spravne zapouzdrene - pres vlastnosti Get/Set		
		public static float dpix; 
		public static float dpiy;

		
		/// <summary>
		/// Vycet pro operace se spojovanymi elementy - move, resize a nothing, obecne {0,1,2}  
		/// </summary>
		public enum OnGroupResize { Move, Resize, Nothing };


		/// <summary>
		/// Vycet pro pruniky seskupovanych elementu, obecne {0,1,2,3}  
		/// </summary>
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

		// vnitrni stavy elementu - dle jejich nazvu ....
		protected int _rotation; 
		private Color _penColor;
		private float _penWidth;
		private Color _fillColor;
		private bool _colorFilled;
		private bool _textureFilled;
		private bool _showBorder;
		private DashStyle _dashStyle; 	
		private bool _closed;

		// nasatveni pruhlednosti prvkum
		[NonSerialized]
		private int _aplha;

		[NonSerialized]
		private float _pruhlednost = 100f;

		private float _savedPruhlednost =100f;				

		// Group objekt - zoomovani:
		protected float gprZoomX = 1f;
		protected float gprZoomY = 1f;
		public bool iAmAline; // jsem linkou
		public bool selected;
		public bool deleted;
		public Ele undoEle;

		// cesta k me nove texture pro tvary ...
		private string filePath;
		
		// pouziti Textury:       
		[NonSerialized]
		private TextureBrush _texture;
		private Bitmap imgOfTexture;


		// prace se swapovanim textur do Bitmap / Image pri Save / Load:
		// automaticke vlastnosti get+set, jmeno zapozdrene promenne je nevylovitelne-unspeakable....
		public Bitmap ObrBitmap { get; set; }
		public Image ObrImage { get; set; }        
		public Bitmap PrevodImageNaBitmap { get; set; }

		#endregion

		#region Konstruktor tridy Ele
		public Ele()
		{
			// cerna barva vyplne - implicitne pro vsechny Ele ....
			FillColor = Color.Black;
			// zakladni barva pera = oraniceni, tj. Cerna ... 
			Pero_barva = Color.Black; 

			// ostatni predvyplnene vlastnosti elementu ...
			Pero_šířka = 1f;			
			ColorFilled = false;
			Ohraničení = true;
			DashStyleMy = DashStyle.Solid;
			Alpha = 255;

			// moznost, ktera neni vyuzita ....
			//Image obr = Properties.Resources.trava_velmi_husta; 
			//ImageOfTexture = (Bitmap)obr;
		}


		#endregion

		#region Destruktor tridy Ele - jen pro zajimavost kdy zafunguje Garbage Collector        

		/// <summary>
		/// Destruktor instancí tříd Ele. O to se v C# stará Garbage Collector.
		/// Uvedeno pouze pro testovací účely.
		/// </summary>
		~Ele()
		{
			string vystup = "Znicene objekty Carbage Collectorem: " + GetX.ToString() + " " + GetY.ToString();
			System.Diagnostics.Trace.WriteLine(vystup);
		}
		#endregion

		#region Základní vlastnosti - GETTERY, SETTERY - pro tridu Ele
		// vkladani img textury elementu
		public Bitmap ImageOfTexture
		{
			get { return imgOfTexture; }
			set { imgOfTexture = value; }
		}
		

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
			
		}


		#endregion

		#region Vlastnosti public, ktere ale nejsou urcene pro muj Filtered Property Grid	

		// je mozne je pozdeji pridat do Filtered Property Gridu jednoduchou upravou ve tride Nastroje
		// v metode OnObjectSelected()

		[DisplayName("Orientace")]
		[Category("Pozice"), Description("X ")]
		public int PosX
		{
			get
			{
				return X;
			}
		}

		[Category("Pozice"), Description("Y ")]
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

		[Category("Group chování"), Description("On Group Resize X")]
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

		[Category("Group chování"), Description("On Group Resize X1")]
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

		[Category("Group chování"), Description("On Group Resize Y")]
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

		[Category("Group chování"), Description("On Group Resize Y1")]
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

		[Category("Group chování"), Description("Řízení Group - Double Click")]
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

		#region Vlastnosti urcene pro muj Filtered Property Grid a jsou typu  public a virtual - budu je prepisovat ve zdedenych tridach Ele
		//[Category("Vzhled"), Description("Nastaví styl hraniční čáry")]
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

		//[Category("Vzhled"), Description("Uzavřený Vypnout / Zapnout")]
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

		//[Category("Vzhled"), Description("zobrazit hraniční čáru")]
		public virtual bool Ohraničení
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


		

		// jakou barvoy vyplneny:
		//[Category("Vzhled"), Description("Nastav barvu výplně")]
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
		//[Category("Vzhled"), Description("Vybrat cestu k nové Textuře")]
		[Editor(typeof(FileLocationEditor), typeof(UITypeEditor))]
		public virtual string Nová_Textura
		{
			get
			{
				//string cesta = _texture.Image(FilePath);
				return filePath;
			}
			set
			{
				try
				{
					filePath = value;
					Bitmap bitm = new Bitmap(filePath);
					TextureBrush tBrush = new TextureBrush(bitm);
					tBrush.WrapMode = WrapMode.Tile;
					FillTexture = tBrush;
				}
				catch
				{
					MessageBox.Show("Textura nebyla načtena !", "Otevření selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					filePath = "";
				}
				
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
		//[Category("Vzhled"), Description("Vyplněný Vypnout / Zapnout")]
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
		//[Category("Vzhled"), Description("Vyplněný Vypnout / Zapnout")]
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

		//[Category("Vzhled"), Description("Nastavit barvu Pera")]
		public virtual Color Pero_barva
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

		//[Category("Vzhled"), Description("Nastavit šířku Pera")]
		public virtual float Pero_šířka
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



		//[Category("Vzhled"), Description("ALPHA - Průhlednost")]
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

		[Category("Vzhled"), Description("Průhlednost prvku v procentech. 100 procent znamená neprůhledný." )]
		public float Průhlednost
		{
			get
			{
				return _pruhlednost;
			}
			set
			{
				if (value <= 1)
				{
					_pruhlednost = 1;
				}
				else if (value >100)
				{
					_pruhlednost = 100;
				}
				else
				{
					_pruhlednost = value;
				}                    
				Alpha = (int)(_pruhlednost * 2.55);
				_savedPruhlednost = _pruhlednost;

			}
		}

		// jedna z poslednich vlastnosti - pred dokoncenim projektu - nutnost ulozit pruhlednot do jine promenne v Ele
		public float SavedPruhlednost
		{
			get
			{
				return _savedPruhlednost;
			}

			set
			{
				_pruhlednost = _savedPruhlednost;
			}
		}


		#endregion

		#region Verejne pristupne metody - Public - pro tridu Ele

		/// <summary>
		/// Elelemtu umozni rotaci o velikost (int a)
		/// </summary>
		public void AddRotation(int a)

		{
			SetRotation = (GetRotation + a);
		}


		/// <summary>
		/// Metoda pro resize textury v pozadi Elementu.       
		/// </summary>
		public static Image ResizeImage(Image imgToResize, Size size)
		{
			return (Image)(new Bitmap(imgToResize, size));
		}

		
		
		/// <summary>
		/// Vraci TextureBrush zvoleneho Ele
		/// </summary>
		public TextureBrush GetTextureBrush()
		{
			return FillTexture;
		}



		// Dulezite - na pruhlednost - kazdemu prvku         
		/// <summary>
		/// Obecna zmena pruhlednosti obrazku a textur ve formatu Imege
		/// </summary>
		public Bitmap ChangeOpacity(Image img, float opacityvalue)
		{
			Bitmap bmp = new Bitmap(img.Width, img.Height); // Urci Sirku a Vysku zdrojoveho Image - dulezite !
			Graphics graphics = Graphics.FromImage(bmp);
			ColorMatrix colormatrix = new ColorMatrix();
			colormatrix.Matrix33 = opacityvalue;
			ImageAttributes imgAttribute = new ImageAttributes();
			imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
			graphics.Dispose();   // Releasing all resource used by graphics 
			return bmp;
		}



		#endregion

		#region Virtualni metody pro tridu Ele - Public Virtual - (k prepsani override v potomcich teto tridy)

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

		#region Chranene - Protected - Metody pro tridu Ele a nasledne POUZE pro jeji prime potomky 
			
		/// <summary>
		/// Pouziva se k definici tloustky pera
		/// </summary>
		protected float ScaledPenWidth(float zoom)
		{
			if (zoom < 0.1f) // pozor tady si menim zobrazovanou tlousku vsech elementu pri Zoomovani 
				zoom = 0.1f; // sandardni tloustka 1 pixel - vpohode
			return Pero_šířka * zoom;
		}


		/// <summary>
		/// Prebira uhel rotace z vertikalni linky z centra Elementu a z linky z centra Elementu do bodu (x,y)
		/// </summary>		
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


		

		/// <summary>
		/// Vybere stetec Brush z vlastnosti Elementu (pokud je Element vyplnen Filled)
		/// </summary>
		protected Brush GetBrush(int dx, int dy, float zoom)
		{
			if (ColorFilled)
			{
				
				return new SolidBrush(Transparency(FillColor, Alpha));
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
			
			to.Alpha = from.Alpha;			
			to.FillColor = from.FillColor;
			to.ColorFilled = from.ColorFilled;
			to.Pero_barva = from.Pero_barva;
			to.Pero_šířka = from.Pero_šířka;
			to.Ohraničení = from.Ohraničení;	
			
		}

		/// <summary>
		/// Vraci vzdalenost 2 bodu
		/// </summary>
		// dulezite !
		protected int Dist(int x, int y, int x1, int y1)
		{
			return (int)Math.Sqrt(Math.Pow((x - x1), 2) + Math.Pow((y - y1), 2));
		}
				
		#endregion

		#region Ciste soukrome metody - Private - pro tridu Ele

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

}
