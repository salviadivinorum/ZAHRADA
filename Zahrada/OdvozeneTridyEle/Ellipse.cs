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
            Rotation = 0;
            rot = true;
        }

        #endregion


        #region Vlastnosti, kterym jsem navic pridal Category a Description v mem Property Gridu

        [Description("Úhel rotace")]
        public int Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        [Category("1"), Description("Elipsa")]
        public string ObjectType
        {
            get { return "Elipsa"; }
        }

        #endregion


        #region Vlastnosti tridy Ellipse
        // zatim bez obecnych vlastnosti

        #endregion


        #region Prepsane zdedene metody

        public override Ele Copy()
        {
            Ellipse newE = new Ellipse(X, Y, X1, Y1);
            newE.PenColor = PenColor;
            newE.PenWidth = PenWidth;
            newE.FillColor = FillColor;
            newE.Filled = Filled;
            newE.iAmAline = iAmAline;
            newE.Alpha = Alpha;
            newE.DashStyleMy = DashStyleMy;
            newE.ShowBorder = ShowBorder;
            newE.Rotation = Rotation;

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
            Pen myPen = new Pen(PenColor, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;
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
            translateMatrix.RotateAt(Rotation, new PointF((X + dx + (X1 - X) / 2) * zoom, (Y + dy + (Y1 - Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

            // Nakresli transformovanou elipsu na obrazovku
            if (Filled)
            {
                g.FillPath(myBrush, myPath);
                if (ShowBorder)
                    g.DrawPath(myPen, myPath);
            }
            else
                g.DrawPath(myPen, myPath);

            myPath.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }

        #endregion


    }
}
