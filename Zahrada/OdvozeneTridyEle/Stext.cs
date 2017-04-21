using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zahrada.OdvozeneTridyEle
{
	/// <summary>
	/// Element Jednoduchy text
	/// </summary>
	[Serializable]
	public class Stext : Ele
	{
		#region Clenske promenne tridy Stext
		private Font f;
		private StringAlignment sa;
		private string text;

		#endregion

		#region Konstruktor tridy Stext
		public Stext(int x, int y, int x1, int y1)
		{
			X = x;
			Y = y;
			X1 = x1;
			Y1 = y1;
			selected = true;
			EndMoveRedim();

			Rotace = 0;
			// CharFont = new Font(FontFamily.GenericMonospace, 8); ;
			rot = true;
		}

		#endregion

		#region Vlastnosti tridy Stext
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public StringAlignment StrAlign
		{
			get { return sa; }
			set { sa = value; }
		}

		public Font CharFont
		{
			get { return f; }
			set { f = value; }
		}

		#endregion

		#region Vlastnosti, kterym jsem priradil navic jmeno kategorie a description - pro muj Property Grid

		[Category("Element"), Description("Jednoduchý text")]
		public string ObjectType
		{
			get { return "Text"; }
		}

		[Category("Vzhled"), Description("Rotace pod úhlem")]
		public int Rotace
		{
			get { return _rotation; }
			set { _rotation = value; }
		}

		[Category("Vzhled"), Description("Vybrat cestu k nové Textuře")]
		public override string Nová_Textura
		{
			get
			{
				return base.Nová_Textura;
			}
			set
			{
				base.Nová_Textura = value;
			}
		}

		[Category("Vzhled"), Description("Zadaný text")]
		public string Text_
		{
			get { return text; }
			set { text = value; }
		}

        [Category("Vzhled"), Description("Zvolený font textu")]
        public Font Font
        {
            get { return f; }
            set { f = value; }
        }


        [Category("Vzhled"), Description("Nastavit barvu Pera")]
        public override Color Pero_barva
        {
            get
            {
                return base.Pero_barva;
            }
            set
            {
                base.Pero_barva = value;

            }
        }
        


        #endregion

        #region Prepsane zdedene metody tridy Stext

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
		{
			gp.AddRectangle(new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom));

			/*
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = this.sa;
			stringFormat.LineAlignment = StringAlignment.Near;
			FontFamily family = new FontFamily(this.CharFont.FontFamily.Name);
			//int fontStyle = (int)FontStyle.Bold;
			gp.AddString(this.Text, family, fontStyle, this.CharFont.Size * zoom, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom), stringFormat);
			*/
		}

		public override Ele Copy()
		{
			Stext newE = new Stext(X, Y, X1, Y1);
			newE.Pero_barva = Pero_barva;
			newE.Pero_šířka = Pero_šířka;
			newE.FillColor = FillColor;
			newE.ColorFilled = ColorFilled;
			newE.TextureFilled = TextureFilled;
			newE.ImageOfTexture = ImageOfTexture;


			newE.iAmAline = iAmAline;
			newE.Alpha = Alpha;
			newE.DashStyleMy = DashStyleMy;
			newE.Ohraničení = Ohraničení;

			newE.OnGrpXRes = OnGrpXRes;
			newE.OnGrpX1Res = OnGrpX1Res;
			newE.OnGrpYRes = OnGrpYRes;
			newE.OnGrpY1Res = OnGrpY1Res;

			newE.CopyGradProp(this);

			newE.Text = Text;
			newE.CharFont = CharFont;
			newE.StrAlign = StrAlign;
			//newE.rtf = this.rtf;

			return newE;
		}

		public override void CopyFrom(Ele ele)
		{
			CopyStdProp(ele, this);

		}

		public override void Select()
		{
			undoEle = Copy();
		}


		public override void Draw(Graphics g, int dx, int dy, float zoom)
		{
			GraphicsState gs = g.Save();//ulozi predchozi transformaci
			Matrix mx = g.Transform; // vrati predchozi transformaci

			PointF p = new PointF(zoom * (X + dx + (X1 - X) / 2), zoom * (Y + dy + (Y1 - Y) / 2));
			if (Rotace > 0)
				mx.RotateAt(Rotace, p, MatrixOrder.Append); //prida transformaci

			g.Transform = mx;

			// pozadi textove bloku
			Brush myBrush = GetBrush(dx, dy, zoom);

			// prace s texturou a osetreni pri Load / Save protoze C# neumi serializaci TextureBrush tridy
			// musim zde zbytecne tvorit 2 tridy Texturebrush ....
			TextureBrush texture = GetTextureBrush();
			TextureBrush texture2;

			if (texture == null)
			{
				ObrBitmap = ImageOfTexture;
                ObrBitmap = ChangeOpacity(ObrBitmap, (Průhlednost / 100));
                texture2 = new TextureBrush(ObrBitmap);
			}
			else
			{
				ObrImage = texture.Image;
                ObrImage = ChangeOpacity(ObrImage, (Průhlednost / 100));
                texture2 = new TextureBrush(ObrImage);
				PrevodImageNaBitmap = new Bitmap(ObrImage);
				ImageOfTexture = PrevodImageNaBitmap;

			}

			float scalX = zoom;
			float scalY = zoom;            
			texture2.Transform = new Matrix(
				scalX,
				0.0f,
				0.0f,
				scalY,
				0.0f,
				0.0f);



			Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
			myPen.DashStyle = DashStyleMy;
            myPen.Color = Transparency(Pero_barva, Alpha);

            if (selected)
			{
				myPen.Color = Color.Red;
				myPen.Color = this.Transparency(myPen.Color, 120);
				myPen.Width = myPen.Width + 1;
			}




			if (TextureFilled || ColorFilled)
			{
				if (TextureFilled)
					g.FillRectangle(texture2, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
				//g.FillPath(texture2, myPath);
				else
					g.FillRectangle(myBrush, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
				//g.FillPath(myBrush, myPath);

				if (Ohraničení || selected)
					g.DrawRectangle(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
				//g.DrawPath(myPen, myPath);
			}
			//else
			   // g.DrawPath(myPen, myPath);














			/*

			if (ColorFilled)
			{
				g.FillRectangle(myBrush, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
			}
			if (ShowBorder || selected)
				g.DrawRectangle(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
			*/

			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = sa;
			stringFormat.LineAlignment = StringAlignment.Near;

			Font tmpf = new Font(CharFont.FontFamily, CharFont.Size * zoom, CharFont.Style);
			g.DrawString(Text, tmpf, new SolidBrush(this.Pero_barva), new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom), stringFormat);



			texture2.Dispose();
			//obr.Dispose();
			// myPath.Dispose();
			myPen.Dispose();
			// translateMatrix.Dispose();


			tmpf.Dispose();
			myPen.Dispose();



			if (myBrush != null)
				myBrush.Dispose();

			g.Restore(gs);// obnovi predchozi transformaci







		}
		#endregion


	}
}