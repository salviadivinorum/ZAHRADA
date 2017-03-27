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
            ShowBorder = false;
        }

        #endregion

        #region Vlastnosti, kterym jsem navic nastavil Category a Description pro muj Property Grid

        [Category("1"), Description("Image Box")]
        public string ObjectType
        {
            get
            {
                return "Image Box";
            }
        }        

        [Category("Obrázek"), Description("Obrázek ze souboru")]
        public Bitmap Img
        {
            get {return _img; }
            set {_img = value;}
        }

        [Category("Obrázek"), Description("Trasparentní")]
        public bool Transparent
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

        [Description("Úhel rotace")]
        public int Rotation
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
            newE.PenColor = PenColor;
            newE.PenWidth = PenWidth;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.TextureFilled = TextureFilled;
            newE.ImageOfTexture = ImageOfTexture;


            newE.iAmAline = iAmAline;
            newE.Alpha = Alpha;
            newE.DashStyleMy = DashStyleMy;
            newE.ShowBorder = ShowBorder;
            newE.Transparent = Transparent;
            newE.Rotation = Rotation;

            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;


            newE.Img = Img;

            newE.CopyGradProp(this);

            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            Img = ((ImageBox)ele).Img;
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
                    Img = loadTexture;
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

            Pen myPen = new Pen(PenColor, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            if (Img != null)
            {
                Color backColor = Img.GetPixel(0, 0); //Vezme barvu pozadi z prvniho pixelu obrazku (horni-levy roh)  
                //Vytvori tmp Bitmapu a tim i Graphics object
                //Rozmer tmp Bitmapy musi dovoli rotaci obrazku
                int dim = (int)Math.Sqrt(Img.Width * Img.Width + Img.Height * Img.Height);

                Bitmap curBitmap = new Bitmap(dim, dim);
                //Bitmap curBitmap = new Bitmap(Img.Width, Img.Height);

                Graphics curG = Graphics.FromImage(curBitmap);

                if (Rotation > 0)
                {
                    
                    // aktivuje rotaci na grafickem objektu
                    Matrix mX = new Matrix();

                    mX.RotateAt(Rotation, new PointF(curBitmap.Width / 2, curBitmap.Height / 2));

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

                    curG.DrawImage(Img, (dim - Img.Width) / 2, (dim - Img.Height) / 2, Img.Width, Img.Height);
                    //curG.DrawImage(Img, 0, 0, Img.Width, Img.Height);

                    if (Transparent)
                        curBitmap.MakeTransparent(backColor); // zde provadim pruhlednost

                    curG.Save();
                    // zde kreslim tmp Bitmapu na platno - to je to this
                    g.DrawImage(curBitmap, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                    //g.DrawImage(curBitmap, (this.X+dx) * zoom, (this.Y+dy ) * zoom, (this.X1) * zoom, (this.Y1) * zoom);
                    curG.Dispose();
                    curBitmap.Dispose();


                

            }

            if (ShowBorder)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (Y + dy) * zoom, (X1 - this.X) * zoom, (Y1 - Y) * zoom);
                //g.DrawRectangle(myPen, (this.X + dx) * zoom, (Y + dy) * zoom, (X1) * zoom, (Y1) * zoom);

            myPen.Dispose();

        }


        #endregion


    }
}
