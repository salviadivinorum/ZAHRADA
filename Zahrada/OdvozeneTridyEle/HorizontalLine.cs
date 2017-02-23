using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace Zahrada.OdvozeneTridyEle
{
    /// <summary>
    /// Vodorovna cara
    /// </summary>
    public class HorizontalLine : Line
    {
        public HorizontalLine(int x, int y, int x1, int y1)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y;
            this.iAmAline = true;
            this.selected = true;
            this.StartCap = LineCap.Custom;
            this.EndCap = LineCap.Custom;
            this.EndMoveRedim();
            this.rot = false; 

        }

        //TEST
        public override void Redim(int x, int y, string redimSt)
        {
            switch (redimSt)
            {
                case "NW":
                    this.X = this.startX + x;
                    //this.X1 = this.startX1 + x;
                    //this.Y = this.startY + y;
                    break;
                case "SE":
                    //this.X = this.startX + x;
                    this.X1 = this.startX1 + x;
                    //this.Y1 = this.startY1 + y;
                    break;
                default:
                    break;
            }

            if (!this.iAmAline)
            {   // manage redim limits
                if (this.X1 <= this.X)
                    this.X1 = this.X + 10;
                if (this.Y1 <= this.Y)
                    this.Y1 = this.Y + 10;
            }

        }



    }
}
