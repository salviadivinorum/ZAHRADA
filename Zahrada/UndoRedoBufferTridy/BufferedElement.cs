﻿using System;

namespace Zahrada.UndoRedoBufferTridy
{
    /// <summary>
    /// Zakladni undo/redo element
    /// </summary>
    [Serializable]
    public class BufferedElement
    {
        #region Clenske promenne tridy BufferedElement

        public Ele objRef;
        public string op; // U=update, I=Insert, D=Delete
        public Ele oldE; // start point
        public Ele newE; // end point

        #endregion

        #region Konstruktor tridy BufferedElement

        public BufferedElement(Ele refe, Ele newe, Ele olde, string o)
        {
            objRef = refe;
            oldE = olde;
            newE = newe;
            op = o;
        }


        #endregion

    }
}
