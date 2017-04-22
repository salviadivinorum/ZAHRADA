
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Zahrada.OdvozeneTridyEle
{
    // prvni textura vsech vyplnenych objektu
    // jedna se o pomocnou tridu
    
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
