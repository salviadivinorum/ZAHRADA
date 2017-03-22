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
            Rotation = 0;
            rot = true;
        }





        #endregion

        #region Vlastnosti, kterym jsem priradil navic jmeno kategorie a description - pro muj Property Grid

        [CategoryAttribute("1"), DescriptionAttribute("Obdélník")]
        public string ObjectType
        {
            get
            {
                return "Obdélník";
            }
        }


        [Category("Rotace pod úhlem"), Description("Úhel rotování")]
        public int Rotation
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






        #endregion


        #region Prepsane zdedene metody  - Overridden methods

        public override Ele Copy()
        {
            Rect newE = new Rect(X, Y, X1, Y1);

            newE.PenColor = PenColor;
            newE.PenWidth = PenWidth;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.DashStyleMy = DashStyleMy;
            newE.Alpha = Alpha;
            newE.iAmAline = iAmAline;
            newE.Rotation = Rotation;
            newE.ShowBorder = ShowBorder;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.CopyGradProp(this);

            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            Rotation = ((Rect)ele).Rotation;
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
                texture2 = new TextureBrush(ObrBitmap);
            }
            else
            {
                ObrImage = texture.Image;
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


            Pen myPen = new Pen(PenColor, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }

            // Vztvori Graphics Path a prida tam obdelnik:
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddRectangle(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));


            // tranformace vlozeneho obdelniku v Graphics Path pomoci GDI+ ... respektive pomoci Matrix tridy
            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this.Rotation, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

            // Konecne nakresli transformovany ctverec na obrazovku
            if (TextureFilled || ColorFilled)
            {
                if (TextureFilled)
                    g.FillPath(texture2, myPath);
                else
                    g.FillPath(myBrush, myPath);

                if (ShowBorder)
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
