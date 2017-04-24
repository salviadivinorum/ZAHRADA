using System;
using System.Drawing;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Uchop pouzity v polygonech pro chyceni a vytazeni casti prostredku polygonu ven z polygonu
    /// </summary>
    [Serializable]
    public class PointHandle : Handle
    {
        #region Clenske promenne tridy PointHandle

        PointWrapper linkedPoint;
        Ele el;

        #endregion

        #region Konstruktor tridy PointWrapper

        public PointHandle(Ele e, string o, PointWrapper p)
        {
            op = o;
            FillColor = Color.BlueViolet;
            linkedPoint = p;
            el = e;
            RePosition(e);
        }

        #endregion       

        #region Vlastnosti tridy PointHandle
        public PointWrapper GetPoint
        {
            get { return linkedPoint; }
        }

        #endregion

        #region Prepsane zdedene metody

        public override bool IsSelected()
        {
            return (selected | linkedPoint.selected);
        }

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            linkedPoint.X = X + 2 - el.GetX;
            linkedPoint.Y = Y + 2 - el.GetY;
        }

        public override void RePosition(Ele e)
        {
            X = (linkedPoint.X + e.GetX - 2);
            Y = (linkedPoint.Y + e.GetY - 2);
            X1 = X + 5;
            Y1 = Y + 5;
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            SolidBrush myBrush = new SolidBrush(FillColor);
            myBrush.Color = Transparency(myBrush.Color, 80);
            Pen whitePen = new Pen(Color.White);

            Pen fillPen = new Pen(FillColor);
            if (IsSelected())
                fillPen.Color = Color.Red;

            g.FillRectangle(myBrush, new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom));
            g.DrawRectangle(whitePen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);

            g.DrawRectangle(fillPen, (X + dx - 1) * zoom, (Y + dy - 1) * zoom, (X1 - X + 2) * zoom, (Y1 - Y + 2) * zoom);

            myBrush.Dispose();
            whitePen.Dispose();
            fillPen.Dispose();


        } 
        #endregion

    }       
}
