using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Uchop pro ZOOMovani elementu. Urceno pro pozdejsi dokonceni !
    /// </summary>
    [Serializable]
    public class ZoomHandle : Handle
    {
        #region Konstruktor tridy ZoomHandle

        public ZoomHandle(Ele e, string o) :base(e,o)
        {
            FillColor = Color.Red;

        }

        #endregion

        #region Prepsane zdedene metody

        public override void RePosition(Ele e)
        {
            float zx = (e.Sirka - (e.Sirka * e.GetGprZoomX)) / 2;
            float zy = (e.Vyska - (e.Vyska * e.GetGprZoomY)) / 2;
            X = (int)((e.GetX1 - 2) - zx);
            Y = (int)((e.GetY1 - 2) - zy);
            X1 = X + 5;
            Y1 = Y + 5;
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            SolidBrush myBrush = new SolidBrush(FillColor);
            myBrush.Color = Transparency(myBrush.Color, 80);
            Pen whitePen = new Pen(Color.White);
            Pen fillPen = new Pen(FillColor);

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
