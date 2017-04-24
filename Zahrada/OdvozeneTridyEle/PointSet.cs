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
    /// <summary>
    /// PoinSet je volna cara nebo rovny polygon
    /// </summary>
    [Serializable]
    public class PointSet : Ele
    {
        #region Clenske promenne tridy PointSet - tj. Polygonu
        public ArrayList points;
        private bool _curved = false;
        private bool _closed = false;
        #endregion

        #region Konstruktor tridy PointSet
        public PointSet(int x, int y, int x1, int y1, ArrayList a)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
            selected = true;
            //
            points = a;
            SetupSize();
            //
            EndMoveRedim();
            Rotace = 0;
            rot = true; //muze rotovat

            Image obr = Properties.Resources.trava_velmi_husta;
            ImageOfTexture = (Bitmap)obr;
            TextureFilled = true;
        }

        #endregion        

        #region Vlastnosti public + urcene pro muj Property Grid

        // Zde mela vlozen zvlastni editor bodu pro vlastnost 
        [Category("Polygon"),Description("Body polygonu")]
        public ArrayList Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        [Category("Vzhled"), Description("Zakřivení polygonu podél bodů")]
        public bool Zakřivení
        {
            get
            {
                return _curved;
            }
            set
            {
                _curved = value;
            }
        }

        [Category("Polygon"), Description("Uzavření polygonu")]
        public bool Closed
        {
            get
            {
                return _closed;
            }
            set
            {
                _closed = value;
            }
        }

        [Category("Element"), Description("Polygon")]
        public string Typ
        {
            get
            {
                return "Polygon";
            }
        }

        [Category("Vzhled"),Description("Rotace pod úhlem")]
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

        [Category("Vzhled"), Description("Vybrat cestu k nové Textuře")]
        public override string Nová_Textura
        {
            get
            {
                return base.Nová_Textura;
            }
            set
            {
                base.Nová_Textura = value;
            }
        }

        [Category("Vzhled"), Description("Zobrazit hraniční čáru elementu")]
        public override bool Ohraničení
        {
            get
            {
                return base.Ohraničení;
            }
            set
            {
                base.Ohraničení = value;
            }
        }

        [Category("Vzhled"), Description("Nastavit barvu Pera")]
        public override Color Pero_barva
        {
            get
            {
                return base.Pero_barva;
            }
            set
            {
                base.Pero_barva = value;

            }
        }

        [Category("Vzhled"), Description("Nastavit šířku Pera")]
        public override float Pero_šířka
        {
            get
            {
                return base.Pero_šířka;

            }
            set
            {
                base.Pero_šířka = value;

            }
        }

        #endregion

        #region Verejne public metody - pro tridu PointSet

        //Nastav konecnou zmenenou velikost Elementu SetPoint, jeho bodu
        public void SetupSize()
        {
            if (points != null)
            {
                int maxX = 0;
                int maxY = 0;
                foreach (PointWrapper p in this.points)
                {
                    if (p.X > maxX)
                        maxX = p.X;
                    if (p.Y > maxY)
                        maxY = p.Y;
                }
                Y1 = Y + maxY;
                X1 = X + maxX;
                RePos(); //!
            }
        }

        // repozice bodu polygonu ...
        public void RePos()
        {
            if (points != null)
            {
                int minNegativeX = 0;
                int minNegativeY = 0;
                foreach (PointWrapper p in points)
                {

                    minNegativeX = p.X;
                    minNegativeY = p.Y;
                    break;
                }
                foreach (PointWrapper p in points)
                {
                    if (p.X < minNegativeX)
                        minNegativeX = p.X;
                    if (p.Y < minNegativeY)
                        minNegativeY = p.Y;
                }
                foreach (PointWrapper p in points)
                {
                    p.X = p.X - minNegativeX; ;
                    p.Y = p.Y - minNegativeY; ;
                }
                X = X + minNegativeX;
                Y = Y + minNegativeY;
            }
        }

        // predloz mi skutecne body ....
        public ArrayList GetRealPosPoints()
        {
            ArrayList a = new ArrayList();
            foreach (PointWrapper p in points)
            {
                a.Add(new PointWrapper(p.X + X, p.Y + Y));
            }
            return a;
        }

        
        //Prida bod do polygonu
        public void AddPoint(Point p)
        {
            points.Add(p);
        }


       
        // Odstrani bod z polygonu        
        public void RmPoint(Point p)
        {
            points.Remove(p);
        }

       
        #endregion
       
        #region Prepsane override zdedene metody

        /// <summary>
        /// Volano na konci po dokonceni Move/Redim Elemenetu PointSet. Navic pro kazdy bod polygonu ukoncuje Zoom.
        /// </summary>
        public override void EndMoveRedim()
        {
            base.EndMoveRedim();
            
            foreach (PointWrapper p in points)
            {
                p.EndZoom();
                
            }
            
        }

        /// <summary>
        /// Zmena rozmeru Elementu PointSet podle toho jakym smerem podle svetovych stran a o kolik. Navic zvetsi kady bod o dx, dy
        /// </summary>     
        public override void Redim(int x, int y, string redimSt)
        {
            base.Redim(x, y, redimSt);
            float dx = (float)(X1 - X) /(float)(startX1 - startX); // sice visual studio hlasi redundaci (float), ale bez toho float2x to pocita spatne !!!
            float dy = (float)(Y1 - Y) / (float)(startY1 - startY);
            foreach (PointWrapper p in points)
            {
                p.Zoom(dx, dy);
            }
        }

        /// <summary>
        /// Vraci pravdu, pokud Element obsahuje v sobe bod (x,y). Kolem vsech bodu PointSet udela obdelnik a vraci true, kdyz tam bod je.
        /// </summary>
        public override bool Contains(int x, int y)
        {
            int minX = X;
            int minY = Y;
            int maxX = X1;
            int maxY = Y1;
            foreach (PointWrapper p in points)
            {

                if (minX > X + p.X)
                    minX = X + p.X;
                if (minY > Y + p.Y)
                    minY = Y + p.Y;
                if (maxX < X + p.X)
                    maxX = X + p.X;
                if (maxY < Y + p.Y)
                    maxY = Y + p.Y;
            }
            return new Rectangle(minX, minY, maxX - minX, maxY - minY).Contains(x, y);
            
        }

        /// <summary>
        /// Adaptuj Element PointSet na velikost mrizky (Grid)
        /// </summary>
        public override void Fit2grid(int gridsize)
        {
            base.Fit2grid(gridsize);

            if (points != null)
            {
                foreach (PointWrapper p in points)
                {
                    p.X = gridsize * (int)(p.X / gridsize);
                    p.Y = gridsize * (int)(p.Y / gridsize);
                }
            }

        }

        /// <summary>
        /// Potvrdit rotaci Elementu podle bodu (x,y) podle stredu otaceni
        /// </summary>
        public override void CommitRotate(float x, float y)
        {
            //base.CommitRotate(x, y);
            if(Rotace > 0)
            {
                // nastaveni stredu otaceni
                float midX, midY = 0;
                midX = (X1 - X) / 2;
                midY = (Y1 - Y) / 2;

                foreach (PointWrapper p in points)
                {
                    p.RotateAt(midX, midY, Rotace);
                }
                Rotace = 0;
            }
        }


        /// <summary>
        /// Potvrdit zrcadleni Elementu PointSet podle xmirr nebo ymirr nebo oboji
        /// </summary>
        public override void CommitMirror(bool xmirr, bool ymirr)
        {
            foreach(PointWrapper p in Points)
            {
                if (xmirr)
                    p.XMirror(Sirka);
                if (ymirr)
                    p.YMirror(Vyska);
            }
            SetupSize();
        }



        /// <summary>
        /// Zrus vyber vsech bodu v Elementu PointSet
        /// </summary>
        public override void DeSelect()
        {
            foreach (PointWrapper p in points)
            {
                p.selected = false;
            }
        }


        /// <summary>
        /// Vyber bodu polygonu podle parametru
        /// </summary>
        public override void Select(int sX, int sY, int eX, int eY)
        {
            foreach (PointWrapper p in points)
            {
                p.selected = false;
                if (new Rectangle(sX, sY, eX - sX, eY - sY).Contains(new Point(p.X + X, p.Y + Y)))
                    p.selected = true;
            }
        }

        /// <summary>
        /// Vyber tento Element polygon
        /// </summary>
        public override void Select()
        {
            undoEle = Copy();
        }

        /// <summary>
        /// Klonuj Element PointSet
        /// </summary>
        public override Ele Copy()
        {
            ArrayList aa = new ArrayList();
            if (points != null)
            {
                foreach (PointWrapper p in points)
                {
                    aa.Add(p.Copy());
                }
            }

            PointSet newE = new PointSet(X, Y, X1, Y1, aa);
            newE.Pero_barva = Pero_barva;
            newE.Pero_šířka = Pero_šířka;
            newE.FillColor = FillColor;
            newE.ColorFilled = ColorFilled;
            newE.TextureFilled = TextureFilled;
            newE.ImageOfTexture = ImageOfTexture;


            newE.DashStyleMy = DashStyleMy;
            newE.Alpha = Alpha;
            newE.iAmAline = iAmAline;
            newE.Rotace = Rotace;
            newE.Ohraničení = Ohraničení;

            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;

            //newE.CopyGradProp(this);

            newE.Closed = Closed;
            newE.Zakřivení = Zakřivení;

            return newE;

        }

        /// <summary>
        /// Zkopiruj vlastnosti z jineho Elementu PointSet
        /// </summary>
        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
            Rotace = ((PointSet)ele).Rotace;
            Zakřivení = ((PointSet)ele).Zakřivení;
            Closed = ((PointSet)ele).Closed;
        }

        /// <summary>
        /// Prida do Graphics Path zvetsene body polygonu
        /// </summary>
        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            // To ARRAY
            PointF[] myArr = new PointF[points.Count];
            int i = 0;
            foreach (PointWrapper p in this.points)
            {
                myArr[i++] = new PointF((p.X + X + dx) * zoom, (p.Y + Y + dy) * zoom);// p.point;
            }

            if (i < 2)
                gp.AddLines(myArr);
            else
                if (this.Zakřivení)
                gp.AddCurve(myArr);
            else
                gp.AddPolygon(myArr);
        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            Brush myBrush = GetBrush(dx, dy, zoom);

            // prace s texturou a osetreni pri Load / Save protoze C# neumi serializaci TextureBrush tridy
            // musim zde zbytecne tvorit 2 tridy Texturebrush ....
            TextureBrush texture = GetTextureBrush();
            TextureBrush texture2;

            if (texture == null)
            {
                ObrBitmap = ImageOfTexture;
                ObrBitmap = ChangeOpacity(ObrBitmap, (Průhlednost / 100));
                texture2 = new TextureBrush(ObrBitmap);
            }
            else
            {
                ObrImage = texture.Image;
                ObrImage = ChangeOpacity(ObrImage, (Průhlednost / 100));
                texture2 = new TextureBrush(ObrImage);
                PrevodImageNaBitmap = new Bitmap(ObrImage);
                ImageOfTexture = PrevodImageNaBitmap;


            }

            float scalX = zoom;
            float scalY = zoom;
            texture2.Transform = new Matrix(
                scalX,
                0.0f,
                0.0f,
                scalY,
                0.0f,
                0.0f);

            Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
            myPen.DashStyle = DashStyleMy;
            myPen.Color = Transparency(Pero_barva, Alpha);

            if (selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            // Vytvori Graphics Path a prida tam objekt PointSet pomoci ArrayList pole
            GraphicsPath myPath = new GraphicsPath();

            PointF[] myArr = new PointF[points.Count];
            int i = 0;
            foreach (PointWrapper p in points)
            {
                myArr[i++] = new PointF((p.X + X + dx) * zoom, (p.Y + Y + dy) * zoom);
            }

            if (myArr.Length < 3 | !Zakřivení)
            {
                if (Closed & myArr.Length >= 3)
                    myPath.AddPolygon(myArr);
                else
                    myPath.AddLines(myArr);
            }
            else
            {
                if (Closed)
                    myPath.AddClosedCurve(myArr);
                else
                    myPath.AddCurve(myArr);
                
            }

            // pouzije vlstnost GDI+ ...trida  Matrix predstavuje geometricke transformace. Zde konkretne delam retaci kolem PointF bodu
            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(Rotace, new PointF((X + dx + (X1 - X) / 2) * zoom, (Y + dy + (Y1 - Y) / 2) * zoom));

            // Na muj Graphics Path pouziji transformacni Matrix
            myPath.Transform(translateMatrix);

            // A konecne nakresli transformovany objekt polygon na obrazovku:
            if (TextureFilled || ColorFilled)
            {
                if (TextureFilled)
                    g.FillPath(texture2, myPath);
                else
                    g.FillPath(myBrush, myPath);

                if (Ohraničení)
                    g.DrawPath(myPen, myPath);
            }
            else
                g.DrawPath(myPen, myPath);

            texture2.Dispose();
            //obr.Dispose();
            myPath.Dispose();
            myPen.Dispose();
            translateMatrix.Dispose();


            if (myBrush != null)
                myBrush.Dispose();

        }

        #endregion


    }
}
