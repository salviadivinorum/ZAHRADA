using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;


namespace Zahrada.OdvozeneTridyEle
{
    /// <summary>
    /// Obrazek = zahradni prvek v planu ! 
    /// </summary>
    [Serializable]
    public class ImageBox : Ele
    {
        #region Clenske promenne tridy ImageBox, navic oproti Ele

        private Bitmap _img;      

        #endregion

        #region Konstruktor tridy ImageBox

        public ImageBox(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            selected = true;
            EndMoveRedim();
            rot = true;
            Ohraničení = false;
            TextureFilled = false;
        }

        #endregion

        #region Vlastnosti public + urcene pro muj Property Grid

        [Category("Element"), Description("Zahradní prvek")]
        public string Typ
        {
            get
            {
                return "Zahradní prvek";
            }
        }        

        [Category("Vzhled"), Description("Načíst prvek .... ")]
        public Bitmap Prvek
        {
            get {return _img; }
            set {_img = value;}

                         
        }
              

        [Category("Vzhled"), Description("Rotace pod úhlem")]
        public int Rotace
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }

        #endregion

        #region Prepsane override zdedene metody

        public override Ele Copy()
        {
            ImageBox newE = new ImageBox(X, Y, X1, Y1);
            newE.Pero_barva = Pero_barva;
            newE.Pero_šířka = Pero_šířka;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.TextureFilled = TextureFilled;
            newE.ImageOfTexture = ImageOfTexture;


            newE.iAmAline = iAmAline;
            newE.Alpha = Alpha;
            newE.DashStyleMy = DashStyleMy;
            newE.Ohraničení = Ohraničení;
            newE.Průhlednost = Průhlednost;
            newE.Rotace = Rotace;

            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;

            newE.Prvek = Prvek;           

            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            Prvek = ((ImageBox)ele).Prvek;
        }

        public override void Select()
        {
            undoEle = Copy();
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            if (Prvek != null)
            {
                Color backColor = Prvek.GetPixel(0, 0); //Vezme barvu pozadi z prvniho pixelu obrazku (horni-levy roh)  
                //Vytvori tmp Bitmapu a tim i Graphics object
                //Rozmer tmp Bitmapy musi dovolit rotaci obrazku
                int dim = (int)Math.Sqrt(Prvek.Width * Prvek.Width + Prvek.Height * Prvek.Height);

                Bitmap curBitmap = new Bitmap(dim, dim);
                //Bitmap curBitmap = new Bitmap(Img.Width, Img.Height);

                Graphics curG = Graphics.FromImage(curBitmap);

                if (Rotace > 0)
                {

                    // aktivuje rotaci na grafickem objektu
                    Matrix mX = new Matrix();

                    mX.RotateAt(Rotace, new PointF(curBitmap.Width / 2, curBitmap.Height / 2));

                    //mX.RotateAt(Rotation, new PointF(((X1-X)*zoom)/2, ((Y1-Y)*zoom/2)));
                    curG.Transform = mX;
                    mX.Dispose();

                }


                // Kreslim img pres tmp Bitmapu    
                curG.DrawImage(Prvek, (dim - Prvek.Width) / 2, (dim - Prvek.Height) / 2, Prvek.Width, Prvek.Height);

                // Zmena pruhllednosti obrazku .....
                curBitmap = ChangeOpacity(curBitmap, (Průhlednost / 100));

                curG.Save();
                // zde kreslim tmp Bitmapu na platno - to je to this
                g.DrawImage(curBitmap, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                curG.Dispose();
                curBitmap.Dispose();
            }

            if (Ohraničení)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);

            myPen.Dispose();

        }
        #endregion      

    }
}
