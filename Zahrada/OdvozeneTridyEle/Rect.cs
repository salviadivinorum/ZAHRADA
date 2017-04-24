using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Zahrada.OdvozeneTridyEle
{
    /// <summary>
    /// Rectangle je obdelnik - univerzalni tvar
    /// </summary>
    [Serializable]
    public class Rect : Ele
    {

        #region Konstruktor tridy Rect

        public Rect(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            selected = true;
            EndMoveRedim();
            Rotace = 0;
            rot = true;

            Image obr = Properties.Resources.trava_velmi_husta;
            ImageOfTexture = (Bitmap)obr;
            TextureFilled = true;
        }
        #endregion

        #region Vlastnosti public + urcene pro muj Property Grid

        [CategoryAttribute("Element"), DescriptionAttribute("Obdélník")]
        public string Typ
        {
            get
            {
                return "Obdélník";
            }
        }


        [Category("Vzhled"), Description("Rotace pod úhlem")]
        public int Rotace
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
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


        [Category("Vzhled"), Description("Zobrazit hraniční čáru elementu")]
        public override bool Ohraničení
        {
            get
            {
                return base.Ohraničení;
            }
            set
            {
                base.Ohraničení = value;
            }
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

        [Category("Vzhled"), Description("Nastavit šířku Pera")]
        public override float Pero_šířka
        {
            get
            {
                return base.Pero_šířka;

            }
            set
            {
                base.Pero_šířka = value;

            }
        }

        #endregion

        #region Prepsane override zdedene metody

        public override Ele Copy()
        {
            Rect newE = new Rect(X, Y, X1, Y1);

            newE.Pero_barva = Pero_barva;
            newE.Pero_šířka = Pero_šířka;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.TextureFilled = TextureFilled;
            newE.ImageOfTexture = ImageOfTexture;


            newE.DashStyleMy = DashStyleMy;
            newE.Alpha = Alpha;
            newE.iAmAline = iAmAline;
            newE.Rotace = Rotace;
            newE.Ohraničení = Ohraničení;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;
            
            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            Rotace = ((Rect)ele).Rotace;
        }

        public override void Select()
        {
            undoEle = Copy();
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddRectangle(new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom));
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {

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


            //Nova textura zvetsujici se podle zoomu
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
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }

            // Vytvori Graphics Path a prida tam obdelnik:
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddRectangle(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));


            // tranformace vlozeneho obdelniku v Graphics Path pomoci GDI+ ... respektive pomoci Matrix tridy
            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this.Rotace, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

            // Konecne nakresli transformovany ctverec na obrazovku
            if (TextureFilled || ColorFilled)
            {
                if (TextureFilled)
                    g.FillPath(texture2, myPath);
                else
                    g.FillPath(myBrush, myPath);

                if (Ohraničení)
                    g.DrawPath(myPen, myPath);
            }
            else
                g.DrawPath(myPen, myPath);

            texture2.Dispose();            
            myPath.Dispose();
            myPen.Dispose();
            translateMatrix.Dispose();

            if (myBrush != null)
                myBrush.Dispose();
        }



        #endregion


    }
}
