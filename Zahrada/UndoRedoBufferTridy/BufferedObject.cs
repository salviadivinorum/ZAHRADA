﻿using System;

namespace Zahrada.UndoRedoBufferTridy
{
    /// <summary>
    /// Dvou elementovy seznam ze 2 zakladnich undo/redo elementu
    /// </summary>
    [Serializable]
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
