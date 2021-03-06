﻿using System;
using System.Drawing;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Uchop Handle pouzity v Polygonech pro vytvoreni noveho uchopu uprostred mezi stavajicimi uchopy
    /// </summary>
    [Serializable]
    public class NewPointHandle : Handle
    {
        #region Clenske promenne tridy NewPointHandle

        PointWrapper linkedPoint;
        PointWrapper realPoint;  
        Ele el;
        public int index = 0;

        #endregion

        #region Konstruktor tridy NewPointHandle

        public NewPointHandle(Ele e, string o, PointWrapper p, int i)
        {
            index = i;
            op = o;
            FillColor = Color.YellowGreen;
            linkedPoint = p;
            el = e;
            RePosition(e);

        }


        #endregion

        #region Vlastnosti tridy NewPointHandle

        public  PointWrapper GetRealPoint
        {
            get { return realPoint; }
        }

        public void SetRealPoint(PointWrapper p) 
        {
            realPoint = p;
        }

        public PointWrapper GetPoint
        {
            get { return linkedPoint; }
        }

        #endregion       

        #region Prepsane zdedene metody

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            linkedPoint.X = X + 2 - el.GetX;
            linkedPoint.Y = Y + 2 - el.GetY;

        }

        public override void RePosition(Ele e)
        {
            X = (linkedPoint.X + e.GetX - 1);
            Y = (linkedPoint.Y + e.GetY - 1);
            X1 = X + 3;
            Y1 = Y + 3;
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
