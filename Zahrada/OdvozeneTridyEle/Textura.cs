using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO; 						//streamer io  -- pokus Undo Redo prace
using System.Runtime.Serialization;     // io
using System.Runtime.Serialization.Formatters.Binary; // io
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using Zahrada.OdvozeneTridyEle;
using Zahrada.PomocneTridy;
using Zahrada.UndoRedoBufferTridy;
using System.Windows.Input;

// prvni textura vsech vzplnenych obejktu....
namespace Zahrada.OdvozeneTridyEle
{
    //[NonSerialized]
    public class Textura
    {
        Image obr;
        TextureBrush tBrush;

        public Textura()
        {
            
        }

        public TextureBrush InicilizujPrviTexturu()
        {
            obr = Properties.Resources.trava_velmi_husta; // toto funguje dobre
            tBrush = new TextureBrush(obr);
            tBrush.WrapMode = WrapMode.Tile;
            return tBrush;
        }
        

        
        
    }
}
