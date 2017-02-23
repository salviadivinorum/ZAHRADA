using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace Zahrada.PomocneTridy
{
    
    /// <summary>
    /// trida PointWrapper - slouzi k ukladani a k aktualizaci bodu - points - v ArrayListu
    /// </summary>
    [Serializable]
    public class PointWrapper
    {
        #region Clenske promenne tridy PointWrapper
        private Point p;
        public bool selected = false;
        private Point startp;
        public int id;
        #endregion

        #region Konstruktory tridy PointWrapper

        public PointWrapper(Point pp)
        {
            p = pp;
            startp = p;
        }

        public PointWrapper(int x, int y)
        {
            p.X = x;
            p.Y = y;
            startp = p;
        }



        #endregion

        #region Verejne pristupne - public - metody tridy PointWrapper

        public void Zoom(float dx, float dy)
        {
            X = (int)(startp.X * dx);
            Y = (int)(startp.Y * dy);
        }

        public void EndZoom()
        {
            startp = p;
        }

        public PointWrapper Copy()
        {
            return new PointWrapper(X, Y);
        }

        public void RotateAt(float x, float y, int rotAngle)
        {
            float tmpX = X - x;
            float tmpY = Y - y;
            PointF p = RotatePoint(new PointF(tmpX, tmpY), rotAngle);
            p.X = p.X + x;
            p.Y = p.Y + y;
            X = (int)p.X;
            Y = (int)p.Y;
        }

        public PointF RotatePoint(PointF p, int rotAngle)
        {
            double rotAngF = rotAngle * Math.PI / 180;
            double sinVal = Math.Sin(rotAngF);
            double cosVal = Math.Cos(rotAngF);
            float nX = (float)(p.X * cosVal - p.Y * sinVal);
            float nY = (float)(p.Y * cosVal + p.X * sinVal);
            return new PointF(nX, nY);
        }

        public void XMirror(int wid)
        {
            X = (-1) * p.X + wid;
            startp = p;
        }

        public void YMirror(int hei)
        {
            Y = (-1) * p.Y + hei;
            startp = p;
        }



        #endregion


        #region Vlastnosti, kterym jsem priradil navic jmeno kategorie a description - pro muj Property Grid
        [Category("Pozice"), Description("hodnota X ")]
        public int X
        {
            get
            {
                return p.X;
            }
            set
            {
                p.X = value;
            }
        }

        [Category("Pozice"), Description("hodnota Y ")]
        public int Y
        {
            get
            {
                return p.Y;
            }
            set
            {
                p.Y = value;
            }
        }

        [Category("Pozice"), Description("Vlastní bod ")]
        public Point Point
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }



        #endregion

    }
}
