using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zahrada.PomocneTridy
{      
    /// <summary>
    /// Pomocna trida. Je to prvni textura vsech vyplnenych objektu Polygon, Rect, Ellipse, Stext
    /// </summary>
    public class Textura
    {
        #region Clenske promenne
        Image obr;
        TextureBrush tBrush;
        #endregion

        #region Konstruktor tridy Textura
        public Textura()
        {

        }
        #endregion

        #region Verjne public metody tridy Textura
        public TextureBrush InicilizujPrviTexturu()
        {
            obr = Properties.Resources.trava_velmi_husta; // toto funguje dobre, nastavuji tuto travu ...
            tBrush = new TextureBrush(obr);
            tBrush.WrapMode = WrapMode.Tile;
            return tBrush;
        } 
        #endregion
    }
}
