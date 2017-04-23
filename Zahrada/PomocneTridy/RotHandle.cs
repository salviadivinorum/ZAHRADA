using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Uchop pro rotaci elementu
    /// </summary>
    [Serializable]
    public class RotHandle : Handle
    {
        #region Konstruktor tridy RotHandle

        public RotHandle(Ele e, string o) :base(e,o)  // to je konstruktor, ktery vola konstruktor sveho predka (base)
        { }

        #endregion

        #region Prepsane zdedene metody

        public override void RePosition(Ele e)
        {
            float midX, midY = 0;
            midX = (e.GetX1 - e.GetX) / 2;
            midY = (e.GetY1 - e.GetY) / 2;
            PointF Hp = new PointF(0, -25);
            PointF RotHP = this.RotatePoint(Hp, e.GetRotation);
            midX += RotHP.X;
            midY += RotHP.Y;

            X = e.GetX + (int)midX - 2;
            Y = e.GetY + (int)midY - 2;
            _rotation = e.GetRotation;

            X1 = this.X + 5;
            Y1 = this.Y + 5;

        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            SolidBrush myBrush = new SolidBrush(Color.Black);
            myBrush.Color = this.Transparency(Color.Black, 80);
            Pen whitePen = new Pen(Color.White);
            Pen myPen = new Pen(Color.Blue, 1.5f);
            myPen.DashStyle = DashStyle.Dash;

            g.FillRectangle(myBrush, new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom));
            g.DrawRectangle(whitePen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);

            //Centralni bod:
            float midX, midY = 0;
            midX = (X1 - X) / 2;
            midY = (Y1 - Y) / 2;

            PointF Hp = new PointF(0, 25);

            PointF RotHP = RotatePoint(Hp, _rotation);

            RotHP.X += X;
            RotHP.Y += Y;
            g.FillEllipse(myBrush, (RotHP.X + midX + dx - 3) * zoom, (RotHP.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
            g.DrawEllipse(whitePen, (RotHP.X + midX + dx - 3) * zoom, (RotHP.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
            g.DrawLine(myPen, (X + midX + dx) * zoom, (Y + midY + dy) * zoom, (RotHP.X + midX + dx) * zoom, (RotHP.Y + midY + dy) * zoom);

            myPen.Dispose();
            myBrush.Dispose();
            whitePen.Dispose();
        }

        #endregion

    }
}
