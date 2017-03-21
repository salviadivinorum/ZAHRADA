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
    public class Arc : Ele
    {
        #region Clenske promenne tridy Arc

        private int _startAng;
        private int _lenAng;
        private LineCap _startCap;
        private LineCap _endCap;

        #endregion

        #region Konstruktor tridy Arc
        public Arc(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            selected = true;
            EndMoveRedim();
            StartAng = 0;
            LenAng = 90;
            StartCap = LineCap.Custom;
            EndCap = LineCap.Custom;
        }

        #endregion

        #region Vlastnosti, kterym jsem priradil navic jmeno kategorie a description - pro muj Property Grid

        [Category("1"), Description("Oblouk")]
        public string ObjectType
        {
            get
            {
                return "Oblouk";
            }
        }

        [Category("Čára - vzhled"), Description("Tvar počátku čáry")]
        public LineCap StartCap
        {
            get
            {
                return _startCap;
            }
            set
            {
                _startCap = value;
            }
        }

        [Category("Čára - vzhled"), Description("Tvar konce čáry")]
        public LineCap EndCap
        {
            get
            {
                return _endCap;
            }
            set
            {
                _endCap = value;
            }
        }

        [Description("Sartovací úhel")]
        public int StartAng
        {
            get
            {
                return _startAng;
            }
            set
            {
                _startAng = value;
            }
        }

        [Description("Délka úhlu")]
        public int LenAng
        {
            get
            {
                return _lenAng;
            }
            set
            {
                _lenAng = value;
            }
        }


        #endregion

        #region Prepsane zdedene metody  - Overridden methods

        /// <summary>
        /// Klonuje Element Arc
        /// </summary>
        public override Ele Copy()
        {
            Arc newE = new Arc(X, Y, X1, Y1);
            newE.PenColor = PenColor;
            newE.PenWidth = PenWidth;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.DashStyleMy = DashStyleMy;
            newE.Alpha = Alpha;
            newE.iAmAline = iAmAline;
            newE.StartAng = StartAng;
            newE.LenAng = LenAng;
            newE.ShowBorder = ShowBorder;
            newE.EndCap = EndCap;
            newE.StartCap = StartCap;

            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;

            newE.CopyGradProp(this);

            return newE;

        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            StartAng = ((Arc)ele).StartAng;
            LenAng = ((Arc)ele).LenAng;
            StartCap = ((Arc)ele).StartCap;
            EndCap = ((Arc)ele).EndCap;
        }

        public override void Select()
        {
            undoEle = Copy();
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddArc((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom, StartAng, LenAng);
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            Brush myBrush = GetBrush(dx, dy, zoom);
            Pen myPen = new Pen(PenColor, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;
            myPen.EndCap = EndCap;
            myPen.StartCap = StartCap;

            if (selected)
            {
                Pen myPen1 = new Pen(PenColor, ScaledPenWidth(zoom));
                myPen1.Width = 0.5f;
                myPen1.DashStyle = DashStyle.Dot;
                g.DrawEllipse(myPen1, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
                myPen1.Dispose();
               
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            // Vytvori Graphics Path a prida tam objekt Arc
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddArc((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom, StartAng, LenAng);

            // toto je vynechano - mozna pro test ucely zprovoznit na chvili - pozdeji:
            //Matrix translateMatrix = new Matrix();
            //translateMatrix.RotateAt(this.Rotation, new Point(this.X + (int)(this.X1 - this.X) / 2, this.Y + (int)(this.Y1 - this.Y) / 2));
            //myPath.Transform(translateMatrix);

            // Konecne nakresli transformovany Arc na obrazovku:
            if (ColorFilled)
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
