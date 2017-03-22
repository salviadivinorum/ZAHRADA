using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Zahrada.PomocneTridy;
using Zahrada.UndoRedoBufferTridy;

namespace Zahrada.UndoRedoBufferTridy
{
    [Serializable]
    public class UndoBuffer
    {
        #region Clenske promenne tridy UndoBuffer
        private BufferedObject top;
        private BufferedObject bottom;
        private BufferedObject current;
        private int _buffSize;
        private int _nElem;
        private bool atBottom;
        #endregion

        #region Konstruktor tridy UndoBuffer

        public UndoBuffer(int i)
        {
            BuffSize = i;
            _nElem = 0; // pocitadlo elementu - otestuj to
            bottom = null;
            top = null;
            current = null;
            atBottom = true;

        }

        #endregion

        #region Vlastnosti tridy UndoBuffer
        public int BuffSize
        {
            get
            {
                return _buffSize;
            }
            set
            {
                _buffSize = value;
            }
        } 

        public int Get_nElem
        {
            get
            {
                return _nElem;
            }
        }

        #endregion

        #region Verejne metody tridy UndoBuffer

        public void Add2Buff(object o)
        {
            if(o !=null)
            {
                BufferedObject g = new BufferedObject(o);
                if (Get_nElem == 0)
                {
                    g.next = null;
                    g.prev = null;
                    top = g;
                    bottom = g;
                    current = g;
                }
                else
                {
                    g.prev = current;
                    g.next = null;
                    current.next = g;
                    top = g;
                    current = g;
                    if (Get_nElem == 1)
                    {
                        bottom.next = g;
                    }
                }

                _nElem++;
                if (BuffSize < Get_nElem)
                {
                    bottom = bottom.next;
                    bottom.prev = null;
                    _nElem--;
                }
                atBottom = false;

            }
        }

        public object Undo()
        {
            if (current != null)
            {
                object obj = current.elem;
                if (current.prev != null)
                {
                    current = current.prev;
                    _nElem--;
                    atBottom = false;
                }
                else
                {
                    atBottom = true;
                }
                return obj;

            }
            return null;
        }

        public object Redo()
        {
            if (current != null)
            {
                object obj;
                if (!atBottom)
                {
                    if (current.next != null)
                    {
                        current = current.next;
                        _nElem++;
                    }
                }
                else
                {
                    atBottom = false;
                }
                obj = current.elem;

                return obj;
            }
            //this._N_elem = count();
            return null;
        }

        public bool UndoAble()
        {
            return !atBottom;
        }

        public bool RedoAble()
        {
            if (current == null) // protoze na samem zacatku mam current null
                return false;
            if (current.next == null) // protoze v prubehu muzu mit current neco, ale next je null
                return false;
            
                return true;
        }
        




        #endregion

    }
}
