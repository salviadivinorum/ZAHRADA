using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.ComponentModel;
using Zahrada.PomocneTridy;

namespace Zahrada.OdvozeneTridyEle
{
    [Serializable]
    public class ImageBox : Ele
    {
        #region Clenske promenne tridy ImageBox

        private Bitmap _img;
        private bool _transparent = false;
        //private float _pruhlednost = 100f;

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
        }

        #endregion

        #region Vlastnosti, kterym jsem navic nastavil Category a Description pro muj Property Grid

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
        // průhlednost - vůbec nepožívat - nikde není potřeba a nemám metody navíc
        /*
       [Category("Vzhled"), Description("Průhlednost zadaného prvku")]
        public bool Průhlednost
        {
            get
            {
                return _transparent;
            }
            set
            {
                _transparent = value;
            }
        }

        */
        
        /*
        [Category("Vzhled"), Description("Průhlednost zadaného prvku")]
        public float Průhlednost
        {
            get
            {
                return _pruhlednost;
            }
            set
            {
                _pruhlednost = value;
                Alpha = (int)(_pruhlednost * 2.55);
                
            }
        }
        */
        

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

        #region Prepsane zdedene metody

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

            newE.CopyGradProp(this);

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
        #endregion

        #region Verejne metody tridy ImageBox

        // tato metoda se pouziva pri dvojkliku na jiz exitujici obrazek
        public void LoadImg()
        {
            string f_name = this.ImgLoader();
            if (f_name != null)
            {
                try
                {
                    Bitmap loadTexture = new Bitmap(f_name);
                    Prvek = loadTexture;
                }
                catch { }
            }
        }

        // tato metoda se pouziva pri double clicku na exist obbrazek
        private string ImgLoader()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Vyber obrázek";
                dialog.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return (dialog.FileName);
                }
            }
            catch { }
            return null;
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //dx = Img.Width;
            //dy = Img.Height;

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

                    /*
                    //curG.DrawImage(Img, (dim - Img.Width) / 2, (dim - Img.Height) / 2, Img.Width, Img.Height);

                    // nove
                    //curG.DrawImage(Img, 0, 0, Img.Width, Img.Width);
                    curG.DrawImage(Img, 0,0 , (X1 - X), (Y + X1 - X));
                    if (Transparent)
                        curBitmap.MakeTransparent(backColor); // zde provadim pruhlednost

                    curG.Save();
                    // zde kreslim tmp Bitmapu na platno - to je to this
                    //g.DrawImage(curBitmap, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                    g.DrawImage(curBitmap, (this.X + dx) * zoom, (Y + dy) * zoom, (X1-X) * zoom, (Y+X1-X) * zoom);
                    curG.Dispose();
                    curBitmap.Dispose();
                    */

                }


                // Kreslim img pres tmp Bitmapu 
                //Prvek = ChangeOpacity((Image)(Prvek), Alpha);

                    curG.DrawImage(Prvek, (dim - Prvek.Width) / 2, (dim - Prvek.Height) / 2, Prvek.Width, Prvek.Height);
                //curG.DrawImage(Img, 0, 0, Img.Width, Img.Height);

                /*
                    if (Průhlednost)
                        curBitmap.MakeTransparent(backColor); // zde provadim pruhlednost
                    */
                //float p = Alpha / 100;
                curBitmap = ChangeOpacity(curBitmap, (Průhlednost/100));
                //curBitmap = SetImageOpacity((Image)(curBitmap), 0.1f);
                //Prvek = SetImageOpacity((Image)(curBitmap), 5f);

                curG.Save();
                    // zde kreslim tmp Bitmapu na platno - to je to this
                    g.DrawImage(curBitmap, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                    //g.DrawImage(curBitmap, (this.X+dx) * zoom, (this.Y+dy ) * zoom, (this.X1) * zoom, (this.Y1) * zoom);
                    curG.Dispose();
                    curBitmap.Dispose();


                

            }

            if (Ohraničení)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                //g.DrawRectangle(myPen, (this.X + dx) * zoom, (Y + dy) * zoom, (X1) * zoom, (Y1) * zoom);

            myPen.Dispose();

        }


        #endregion
        


        /*
        public Bitmap SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }
        */

    }
}
