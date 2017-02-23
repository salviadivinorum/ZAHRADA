using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;
using Zahrada.UndoRedoBufferTridy;


namespace Zahrada.UndoRedoBufferTridy
{
    /// <summary>
    /// Dvou elementovy seznam ze 2 zakladnich undo/redo elementu
    /// </summary>
    public class BufferedObject
    {
        #region Clenske promenne tridy BufferedObject
        public BufferedObject next;
        public BufferedObject prev;
        public object elem;
        #endregion

        #region Konstruktor tridy BufferedObject
        public BufferedObject(object o)
        {
            elem = o;
        }
        #endregion

    }
}
