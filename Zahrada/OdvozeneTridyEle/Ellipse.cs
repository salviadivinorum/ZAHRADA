using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using Zahrada.PomocneTridy;

namespace Zahrada.OdvozeneTridyEle
{
    [Serializable]
    public class Ellipse : Ele
    {
        #region Konstruktor tridy Ellipse

        public Ellipse(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            selected = true;
            EndMoveRedim();
            Rotace = 0;
            rot = true;
        }

        #endregion


        #region Vlastnosti, kterym jsem navic pridal Category a Description v mem Property Gridu
        [Category("Vzhled"), Description("Úhel rotace")]     
        public int Rotace
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        [Category("Element"), Description("Elipsa")]
        public string Typ
        {
            get { return "Elipsa"; }
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


        #region Vlastnosti tridy Ellipse
        // zatim bez obecnych vlastnosti

        #endregion


        #region Prepsane zdedene metody

        public override Ele Copy()
        {
            Ellipse newE = new Ellipse(X, Y, X1, Y1);
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
            newE.Rotace = Rotace;

            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;

            newE.CopyGradProp(this);

            return newE;
        }

        public override void Select()
        {
            undoEle = Copy();

        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddEllipse((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
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

            float scalX = zoom;
            float scalY = zoom;            
            texture2.Transform = new Matrix(
                scalX,
                0.0f,
                0.0f,
                scalY,
                0.0f,
                0.0f);
                
          
            // timto lze take zmenit velikost TextureBrush ....  
            //texture2.ScaleTransform(scalX, scalY);


            Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;
            myPen.Color = Transparency(Pero_barva, Alpha);

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }
            

            // Vytvori Graphics path a prida tam objekt Ellipse
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);

            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(Rotace, new PointF((X + dx + (X1 - X) / 2) * zoom, (Y + dy + (Y1 - Y) / 2) * zoom));
            
            myPath.Transform(translateMatrix);

            // Nakresli transformovanou elipsu na obrazovku
            /*
            if (ColorFilled)
            {
                //g.FillPath(myBrush, myPath);
                g.FillPath(texture2, myPath);
                if (ShowBorder)
                    g.DrawPath(myPen, myPath);
            }
            */

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
            //obr.Dispose();    
            myPath.Dispose();
            myPen.Dispose();
            translateMatrix.Dispose();

            if (myBrush != null)
                myBrush.Dispose();
        }

        #endregion


    }
}
