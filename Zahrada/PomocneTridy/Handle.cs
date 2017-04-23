using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Zakladni objektu Uchop=Handle - abstraktni korenova trida pro dalsi mozne typy uchopu
    /// </summary>
    [Serializable]
    public abstract class Handle : Ele
    { 
        #region Clenske promenne tridy Handle

        public string op;
        public bool visible = true;

        #endregion

        #region Konstruktory tridy Handle

        // konstruktor pro odvozene tridy od tridy Handle
        public Handle()
        {

        }

        public Handle(Ele e, string o)
        {
            op = o;
            RePosition(e);
        }

        #endregion

        #region Verejne metody tridy Handle

        public string IsOver(int x, int y)
        {
            Rectangle r = new Rectangle(X, Y, X1 - X, Y1 - Y);
            if (r.Contains(x,y))
            {
                selected = true;
                return op;
            }
            else
            {
                selected = false;
                return "";
            }

        }

        #endregion

        #region Virtualni metody pro tridu Handle

        public virtual bool IsSelected()
        {
            return selected;
        }
        
        public virtual void RePosition(Ele e)
        {

        }

        #endregion

    }
}
