using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Zakladni 1 odvozeny uchop Handle (od base class), ktery slouzi pro vsechny akce typu redim/rotate/move
    /// </summary>
    [Serializable]
    public class RedimHandle : Handle
    {
        #region Konstruktor tridy RedimHanlde

        public RedimHandle(Ele e, string o) : base(e,o) // timto volam konstruktor tridy Handle , ktery byl parametricky (e,o)
        {
            FillColor = Color.Black;
        }
        #endregion

        #region Prepsane zdedene metody

        public override void RePosition(Ele e)
        {
            switch (op)
            {
                case "NW":
                    X = e.GetX - 2;
                    Y = e.GetY - 2;
                    break;
                case "N":
                    X = e.GetX - 2 + ((e.GetX1 - e.GetX) / 2);
                    Y = e.GetY - 2;
                    break;
                case "NE":
                    X = e.GetX1 - 2;
                    Y = e.GetY - 2;
                    break;
                case "E":
                    X = e.GetX1 - 2;
                    Y = e.GetY - 2 + (e.GetY1 - e.GetY) / 2;
                    break;
                case "SE":
                    X = e.GetX1 - 2;
                    Y = e.GetY1 - 2;
                    break;
                case "S":
                    X = e.GetX - 2 + (e.GetX1 - e.GetX) / 2;
                    Y = e.GetY1 - 2;
                    break;
                case "SW":
                    X = e.GetX - 2;
                    Y = e.GetY1 - 2;
                    break;
                case "W":
                    X = e.GetX - 2;
                    Y = e.GetY - 2 + (e.GetY1 - e.GetY) / 2;
                    break;
                default:
                    break;
            }
            X1 = X + 5;
            Y1 = Y + 5;

        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {           
            SolidBrush myBrush = new SolidBrush(FillColor);
            myBrush.Color = Transparency(Color.Black, 80);
            Pen whitePen = new Pen(Color.White);
            g.FillRectangle(myBrush, new RectangleF((X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom));
            g.DrawRectangle(whitePen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
            myBrush.Dispose();
            whitePen.Dispose();
        }





        #endregion

    }
}
