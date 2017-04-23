using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;

namespace Zahrada.PomocneTridy
{

    /// <summary>
    /// Abstraktni trida - kolekce uchopu kolem elementu - pro operace typu redim/move/rotate nad vsemi elementy
    /// </summary>
    [Serializable]
    public abstract class AbstractSel : Ele
    {
        #region Clenske promenne tridy AbstractSel

        protected ArrayList handles; // drzi si v sobe kolekci uchopu kolem libovolneho Ele

        #endregion

        #region Konstruktor tridy AbstractSel

        public AbstractSel(Ele el)
        {
            handles = new ArrayList();

            X = el.GetX;
            Y = el.GetY;
            X1 = el.GetX1;
            Y1 = el.GetY1;
            selected = false;
            rot = el.CanRotate; // povolena rotace
            _rotation = el.GetRotation;
            gprZoomX = el.GetGprZoomX;
            gprZoomY = el.GetGprZoomY;
            iAmAline = el.iAmAline;
            IamGroup = el.AmIaGroup;            
            EndMoveRedim();
        }


        #endregion

        #region Prepsane override metody zdedene od tridy Ele

        public override void EndMoveRedim()
        {
            base.EndMoveRedim();
            foreach (Handle h in handles)
            {
                h.EndMoveRedim();
            }
        }

        public override void Rotate(float x, float y)
        {
            base.Rotate(x, y);
            foreach (Handle h in handles)
            {
                h.RePosition(this);
            }
        }

        public override void Move(int x, int y)
        {
            base.Move(x, y);
            foreach (Handle h in handles)
            {
                h.RePosition(this);
            }
        }

        public override void Select()
        {
            undoEle = Copy();
        }


        public override void Redim(int x, int y, string redimSt)
        {
            base.Redim(x, y, redimSt);
            foreach (Handle h in handles)
            {
                h.RePosition(this);
            }

        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            foreach (Handle h in handles)
            {
                if (h.visible)
                    h.Draw(g, dx, dy, zoom);
            }
        }

        #endregion

        #region Verejne metody pro tridu AbstractSel

        public void SetZoom(float x, float y)
        {
            gprZoomX = x;
            gprZoomY = y;
            foreach(Handle h in handles)
            {
                h.RePosition(this);
            }
        }

        public void ShowHandles(bool i)
        {
            IamGroup = i;
        }
        
       
        // Rekurzivne se rozhoduje nad kterym uchopem je bod (x,y)        
        public string IsOver(int x, int y)
        {
            string ret;
            foreach (Handle h in handles)
            {
                ret = h.IsOver(x, y);
                if (ret != "")
                    return ret;
            }
            if (Contains(x, y))
                return "C"; 

            return "NO";
        }

        
        #endregion

    }
}
