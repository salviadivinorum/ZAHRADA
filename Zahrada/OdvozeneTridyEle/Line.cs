﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Zahrada.OdvozeneTridyEle
{
    /// <summary>
    /// Line je zakladni cara v projektu
    /// </summary>
    [Serializable]
    public class Line : Ele
    {
        #region Clenske promenne tridy Line, navic oproti Ele
        private LineCap _startCap;
        private LineCap _endCap;
        #endregion

        #region Konstruktory tridy Line

        // bezparametricky konstruktor        
        public Line()
        { }
        
    
        // parametricky konstruktor tridy
        public Line(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            iAmAline = true;
            selected = true;
            rot = false; //can rotate?

            StartCap = LineCap.Custom;
            EndCap = LineCap.Custom;
            EndMoveRedim();

            TextureFilled = false;

        }
        #endregion

        #region Vlastnosti public + urcene pro muj Property Grid
        [Category ("Čára - vzhled"), Description("Tvar počátku čáry")]
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

        [Category("Element"), Description("Čára")]
        public string Typ
        {
            get
            {
                return "Čára";
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
        
        #region Prepsane override zdedene metod

        /// <summary>
        /// Klonuj Element Cara
        /// </summary>
        public override Ele Copy()
        {
            Line newE = new Line(X, Y, X1, Y1);

            // Zapouzdreno
            newE.Pero_barva = Pero_barva;
            newE.Pero_šířka = Pero_šířka;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.TextureFilled = TextureFilled;
            newE.ImageOfTexture = ImageOfTexture;


            newE.Alpha = Alpha;
            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;
            
            // nezapouzdreno
            newE.dashstyle = dashstyle;
            newE.iAmAline = iAmAline;
            newE.gprZoomX = gprZoomX;
            newE.gprZoomY = gprZoomY;

            // zapouzdreno v teto tride
            newE.StartCap = StartCap;
            newE.EndCap = EndCap;      

            return newE;

        }

        /// <summary>
        /// Zkopiruj vlastnosti z jineho Elementu Cara
        /// </summary>
        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            EndCap = ((Line)ele).EndCap;
            StartCap = ((Line)ele).StartCap;
        }

        /// <summary>
        /// Vyber tento Element Cara
        /// </summary>
        public override void Select()
        {
            undoEle = Copy();
        }

        /// <summary>
        /// Pripoji Element Line do Graphics Path
        /// </summary>
        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddLine((GetX + dx) * zoom, (GetY + dy) * zoom, (GetX1 + dx) * zoom, (GetY1 + dy) * zoom);
        }
        

        /// <summary>
        /// Nakresli Caru do objektu Graphics
        /// </summary>
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;
            myPen.StartCap = StartCap;
            myPen.EndCap = EndCap;


            myPen.Color = Transparency(Pero_barva, Alpha);

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
                g.DrawEllipse(myPen, (X + dx) * zoom, (Y + dy) * zoom, 3, 3);
            }

            if (X == X1 && Y == Y1)
                g.DrawEllipse(myPen, (X + dx) * zoom, (Y + dy) * zoom, 3, 3);
            else
                g.DrawLine(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 + dx) * zoom, (Y1 + dy) * zoom);

            myPen.Dispose();
        }


        #endregion

    }
}
