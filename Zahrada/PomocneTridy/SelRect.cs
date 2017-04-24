using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Trida pro vyber ostatnich elementu na Platne vyjma Polygonu - je to vyber pomoci ctverce
    /// tyka se to pro obecne operace nad elemety typu: redim/move/rotate
    /// </summary>
    [Serializable]
    public class SelRect : AbstractSel
    {
        #region Konstruktor tridy SelRect

        public SelRect(Ele el) : base(el)
        {
            Setup();
        }

        //Nastavuje uchopy objektu - samotnemu elementu, nebo pozdeji - skupine elementu (Group)
        public void Setup()
        {
            if (!AmIaGroup)
            {
                //NW
                handles.Add(new RedimHandle(this, "NW"));
                //SE
                handles.Add(new RedimHandle(this, "SE"));
                if (!iAmAline)
                {
                    //N
                    handles.Add(new RedimHandle(this, "N"));
                    if (rot)
                    {
                        //ROT
                        handles.Add(new RotHandle(this, "ROT"));
                    }
                    //NE
                    handles.Add(new RedimHandle(this, "NE"));
                    //E
                    handles.Add(new RedimHandle(this, "E"));
                    //S
                    handles.Add(new RedimHandle(this, "S"));
                    //SW
                    handles.Add(new RedimHandle(this, "SW"));
                    //W
                    handles.Add(new RedimHandle(this, "W"));
                }
            }
            else
            {
                //N
                this.handles.Add(new RedimHandle(this, "N"));
                if (rot)
                {
                    //ROT
                    this.handles.Add(new RotHandle(this, "ROT"));
                }
                //E
                this.handles.Add(new RedimHandle(this, "E"));
                //S
                this.handles.Add(new RedimHandle(this, "S"));
                //W
                this.handles.Add(new RedimHandle(this, "W"));
                //ZOOM
                this.handles.Add(new ZoomHandle(this, "ZOOM"));
            }
        }

        #endregion       

        #region Prepsane zdedene metody

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            base.Draw(g, dx, dy, zoom);
            Pen myPen = new Pen(Color.Blue, 1.5f);
            myPen.DashStyle = DashStyle.Dash;
            if (iAmAline)
                g.DrawLine(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 + dx) * zoom, (Y1 + dy) * zoom);
            else
                g.DrawRectangle(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
            myPen.Dispose();

        }

        #endregion

    }
}
