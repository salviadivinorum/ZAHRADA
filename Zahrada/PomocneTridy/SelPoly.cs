using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.OdvozeneTridyEle;


namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Trida pro vyber polygonu - Rovneho nebo typu Volna cara
    /// </summary>
    [Serializable]
    public class SelPoly : AbstractSel
    {
        #region Konstruktor tridy SelPoly

        public SelPoly(Ele el) : base(el) // volam konstruktor predka + novinky
        {
            Setup((PointSet)el);
        }

        public void Setup(PointSet el)
        {
            if (rot)
            {
                handles.Add(new RotHandle(this, "ROT"));
            }

            PointWrapper prec = null;
            int c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;

            foreach (PointWrapper p in el.points)
            {
                c++;
                handles.Add(new PointHandle(this, "POLY", p));
                if (prec != null)
                {
                    minx = Math.Min(p.X, prec.X);
                    miny = Math.Min(p.Y, prec.Y);
                    maxx = Math.Max(p.X, prec.X);
                    maxy = Math.Max(p.Y, prec.Y);
                    PointWrapper newP = new PointWrapper(minx + ((maxx - minx) / 2), miny + ((maxy - miny) / 2));
                    handles.Add(new NewPointHandle(this, "NEWP", newP, c));
                }
                prec = p;
            }

            if (c > 0)
            {
                PointWrapper newP = new PointWrapper(prec.X + 7, prec.Y + 7);
                handles.Add(new NewPointHandle(this, "NEWP", newP, c + 1));
            }

            //SE
            handles.Add(new RedimHandle(this, "SE"));
            //S
            handles.Add(new RedimHandle(this, "S"));
            //E
            handles.Add(new RedimHandle(this, "E"));
            //W
            handles.Add(new RedimHandle(this, "W"));
            //SW
            handles.Add(new RedimHandle(this, "SW"));
            //NW
            handles.Add(new RedimHandle(this, "NW"));
            //N
            handles.Add(new RedimHandle(this, "N"));
            //NE
            handles.Add(new RedimHandle(this, "NE"));
        }

        #endregion

        #region Prepsane zdedene metody

        public override void Redim(int x, int y, string redimSt)
        {
            base.Redim(x, y, redimSt);
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Rotate(float x, float y)
        {
            base.Rotate(x, y);
            foreach (Handle h in handles)
            {
                if (h is PointHandle | h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            base.Draw(g, dx, dy, zoom);
            Pen myPen = new Pen(Color.Blue, 1f);
            myPen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
            myPen.Dispose();
        }


        #endregion

        #region Verejne pristupne metody pro tridu SelPoly
               
        public void ReCreateCreationHandles(PointSet el)
        {
            ArrayList tmp = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                    tmp.Add(h);
            }
            foreach (Handle h in tmp)
            {
                handles.Remove(h);
            }

            PointWrapper prec = null;
            int c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;
            foreach (PointWrapper p in el.points)
            {
                c++;
                if (prec != null)
                {
                    minx = Math.Min(p.X, prec.X);
                    miny = Math.Min(p.Y, prec.Y);
                    maxx = Math.Max(p.X, prec.X);
                    maxy = Math.Max(p.Y, prec.Y);
                    PointWrapper newP = new PointWrapper(minx + (int)((maxx - minx) / 2), miny + (int)((maxy - miny) / 2));
                    handles.Add(new NewPointHandle(this, "NEWP", newP, c));
                }
                prec = p;
            }

            if (c > 0)
            {
                PointWrapper newP = new PointWrapper(prec.X + 7, prec.Y + 7);
                handles.Add(new NewPointHandle(this, "NEWP", newP, c + 1));
            }

        }

        public ArrayList GetSelPoints()
        {
            ArrayList a = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is PointHandle & h.IsSelected())
                {
                    a.Add(((PointHandle)h).GetPoint);
                }
            }
            return a;
        }

        public int GetIndex()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.selected)
                    {
                        return ((NewPointHandle)h).index;
                    }
                }
            }
            return 0;
        }

        public void MovePoints(int dx, int dy)
        {
            foreach (Handle h in handles)
            {
                if (h is PointHandle)
                {
                    if (h.IsSelected())
                    {
                        h.Move(dx, dy);
                    }
                }
            }
        }

        public PointWrapper GetNewPoint()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.selected)
                    {
                        return ((NewPointHandle)h).GetPoint;
                    }
                }
            }
            return null;
        }



        #endregion

    }
}
