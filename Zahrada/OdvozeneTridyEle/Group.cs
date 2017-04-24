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
    /// Trida Group slouzi k seskupeni elementu
    /// </summary>
    [Serializable]
    public class Group : Ele
    {

        #region Clenske promenne tridy Group, navic oproti Ele

        // podobjekty obsazene ve skupine Group
        ArrayList objs;

        // obsluhuji objekty v Group jako GraphicsPath
        bool _grapPath = false;
        bool _xMirror = false;
        bool _yMirror = false;

        GroupDisplay _groupDisplay = GroupDisplay.Default;

        string _name = "";
        public static int ngrp; // pouziva se ke generovani jmen skupin, pozdeji vyuziju


        #endregion

        #region Konstruktor tridy Group

        public Group(ArrayList a)
        {
            IamGroup = true;
            objs = a;

            int minX = +32000;
            int maxX = -32000;
            int minY = +32000;
            int maxY = -32000;

            foreach (Ele e in objs)
            {
                if (e.GetX < minX)
                    minX = e.GetX;
                if (e.GetY < minY)
                    minY = e.GetY;
                if (e.GetX1 > maxX)
                    maxX = e.GetX1;
                if (e.GetY1 > maxY)
                    maxY = e.GetY1;
                e.selected = false;
            }

            X = minX;
            Y = minY;
            X1 = maxX;
            Y1 = maxY;
            selected = true;
            EndMoveRedim();
            Rotace = 0;
            rot = true; 
            GetSetName = "Itm" + ngrp.ToString();
            ngrp++;

            Image obr = Properties.Resources.trava_velmi_husta;
            ImageOfTexture = (Bitmap)obr;
            TextureFilled = true;

        }
        #endregion

        #region Vlastnosti public + urcene pro muj Property Grid

        [Category("Element"), Description("Seskupení elementů")]
        public string Typ
        {
            get
            {
                return "Seskupení elementů";
            }
        }

        [Category("Group"), Description("Seznam tvarů Shape")]
        public Ele[] Objs
        {
            get
            {
                Ele[] aar = new Ele[objs.Count];
                int i = 0;
                foreach (Ele e in objs)
                {
                    aar[i++] = e;
                }
                return aar;
            }
        }

        [Category("Group"), Description("Zobrazení skupiny")]
        public GroupDisplay DisplayOfGroup
        {
            get
            {
                
                return _groupDisplay;
            }
            set
            {
                _groupDisplay = value;
            }
        }

        
        [Category("Group"), Description("Zrcadlení podle X - ON/OFF ")]
        public bool XMirror
        {
            get
            {
                return _xMirror;
            }
            set
            {
                _xMirror = value;
            }
        }
        [Category("Group"), Description("Zrcadlení podle Y - ON/OFF ")]
        public bool YMirror
        {
            get
            {
                return _yMirror;
            }
            set
            {
                _yMirror = value;
            }
        }
        


        [Category("Group"), Description("Měřítko X Zoom ")]
        public float GrpZoomX
        {
            get
            {
                return gprZoomX;
            }
            set
            {
                if (value > 0)
                    gprZoomX = value;
            }
        }

        [Category("Group"), Description("Měřítko Y Zoom ")]
        public float GrpZoomY
        {
            get
            {
                return this.gprZoomY;
            }
            set
            {
                if (value > 0)
                    this.gprZoomY = value;
            }
        }

  
        [Description("Obsluhuje skupinu Group jako GraphicsPath")]
        public bool graphPath
        {
            get
            {
                return _grapPath;
            }
            set
            {

                _grapPath = value;
            }
        }

        [Category("Vzhled"), Description("Úhel rotace")]        
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


        //[Category("Vzhled"), Description("Vybrat cestu k nové Textuře")]
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

        // [Category("Vzhled"), Description("Nastavit barvu Pera")]
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

       // [Category("Vzhled"), Description("Nastavit šířku Pera")]
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

        #region Ostatni public Vlastnosti tridy Group
        public string GetSetName
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != "")
                    _name = value;
            }
        }

        public void SetZoom(int x, int y)
        {
            float dx = (X1 - x) * 2;
            float dy = (Y1 - y) * 2;

            GrpZoomX = (Sirka - dx) / Sirka;
            GrpZoomY = (Sirka - dy) / Vyska;
        }
        #endregion

        #region Prepsane override zdedene Vlastnosti

        public override bool ColorFilled
        {
            get
            {
                return base.ColorFilled;
            }
            set
            {
                base.ColorFilled = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.ColorFilled = value;
                    }
            }
        }

        // doplneno k texture - musel jsem prepsat puvodni metodu a to pro vsecheny Ele v Group ...
        public override bool TextureFilled
        {
            get
            {
               
                return base.TextureFilled;
            }
            set
            {
                base.TextureFilled = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.TextureFilled = value;
                    }
            }
        }


        public override int Alpha
        {
            get
            {
                return base.Alpha;

            }
            set
            {
                base.Alpha = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.Alpha = value;
                        e.Průhlednost = Průhlednost;
                    }
            }
        }

        public override Color FillColor
        {
            get
            {
                return base.FillColor;
            }
            set
            {
                base.FillColor = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.FillColor = value;
                    }
            }
        }

        // doplneno k texture - musel jsem prepsat puvodni metodu a to pro vsecheny Ele v Group ...
        public override TextureBrush FillTexture        
        {
            get
            {
                return base.FillTexture;
            }
            set
            {
                base.FillTexture = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.FillTexture = value;
                    }
            }
        }

        

       



        //[Category("Vzhled"), Description("Zobrazit hraniční čáru elementu")]
        public override bool Ohraničení
        {
            get
            {
                return base.Ohraničení;
            }
            set
            {
                base.Ohraničení = value;
                if (objs != null)
                    foreach (Ele e in objs)
                    {
                        e.Ohraničení = value;
                    }
            }
        }

        public override DashStyle DashStyleMy
        {
            get
            {
                return base.DashStyleMy;
            }

            set
            {
                base.DashStyleMy = value;
                if (objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.DashStyleMy = value;
                    }
            }
        }

        #endregion

        #region Prepsane override zdedene metody

        public override void AfterLoad()
        {
            base.AfterLoad();
            foreach (Ele e in objs)
            {
                e.AfterLoad();
            }
        }

        public override void EndMoveRedim()
        {
            base.EndMoveRedim();
            foreach (Ele e in objs)
            {
                e.EndMoveRedim();
            }
        }

        public override ArrayList DeGroup()
        {
            return objs;
        }

        public override void Move(int x, int y)
        {
            foreach (Ele e in objs)
            {
                e.Move(x, y);
            }
            X = startX - x;
            Y = startY - y;
            X1 = startX1 - x;
            Y1 = startY1 - y;
        }


        public override Ele Copy()
        {

            //Zkopiruje vsechny potomky Ele v seznamu objs
            ArrayList l1 = new ArrayList();
            foreach (Ele e in objs)
            {
                Ele e1 = e.Copy();
                l1.Add(e1);
            }

            Group newE = new Group(l1);
            
            newE.Rotace = Rotace;
            newE._grapPath = _grapPath;
            newE.gprZoomX = gprZoomX;
            newE.gprZoomY = gprZoomY;
            newE.IamGroup = IamGroup;
            newE._name = GetSetName + "_" + ngrp.ToString();
            newE.OnGrpDClick = OnGrpDClick;
            newE.OnGrpXRes = OnGrpXRes;
            newE.OnGrpX1Res = OnGrpX1Res;
            newE.OnGrpYRes = OnGrpYRes;
            newE.OnGrpY1Res = OnGrpY1Res;

            newE.DisplayOfGroup = DisplayOfGroup;          

            if (newE._grapPath)
            {
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

                       

            }

            return newE;

        }

        public override void CopyFrom(Ele ele)
        {
            CopyStdProp(ele, this);
        }

        public override void Select()
        {
            undoEle = Copy();
        }

        public override void Redim(int x, int y, string redimSt)
        {
            foreach (Ele e in objs)
            {
                switch (redimSt)
                {
                    case "N":
                        base.Redim(x, y, redimSt);
                        if (e.OnGrpYRes != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpYRes == OnGroupResize.Move)
                            {
                                e.Move(0, -y);
                            }
                            else
                            {
                                e.Redim(0, y, redimSt);
                            }
                        }
                        break;
                    case "E":
                        base.Redim(x, y, redimSt);
                        if (e.OnGrpX1Res != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpX1Res == OnGroupResize.Move)
                            {
                                e.Move(-x, 0);
                            }
                            else
                            {
                                e.Redim(x, 0, redimSt);
                            }
                        }
                        break;
                    case "S":
                        base.Redim(x, y, redimSt);
                        if (e.OnGrpY1Res != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpY1Res == OnGroupResize.Move)
                            {
                                e.Move(0, -y);
                            }
                            else
                            {
                                e.Redim(0, y, redimSt);
                            }
                        }
                        break;
                    case "W":
                        base.Redim(x, y, redimSt);
                        if (e.OnGrpXRes != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpXRes == OnGroupResize.Move)
                            {
                                e.Move(-x, 0);
                            }
                            else
                            {
                                e.Redim(x, 0, redimSt);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            foreach (Ele e in objs)
            {
                e.AddGraphPath(gp, dx, dy, zoom);
            }
        }



        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            GraphicsState gs = g.Save();  //Uloz predchozi transformaci objektu
            Matrix mx = g.Transform; // predchozi transformace

            PointF p = new PointF(zoom * (X + dx + (X1 - X) / 2), zoom * (Y + dy + (Y1 - Y) / 2));
            if (Rotace > 0)
                mx.RotateAt(Rotace, p, MatrixOrder.Append); //pridej transformaci

            //X MIRROR  //Y MIRROR
            if (_xMirror || _yMirror)
            {
                mx.Translate(-(X + Sirka / 2 + dx) * zoom, -(Y + Vyska / 2 + dy) * zoom, MatrixOrder.Append);
                if (_xMirror)
                    mx.Multiply(new Matrix(-1, 0, 0, 1, 0, 0), MatrixOrder.Append);
                if (_yMirror)
                    mx.Multiply(new Matrix(1, 0, 0, -1, 0, 0), MatrixOrder.Append);
                mx.Translate((X + Sirka / 2 + dx) * zoom, (Y + Vyska / 2 + dy) * zoom, MatrixOrder.Append);
            }

            if (GrpZoomX > 0 && GrpZoomY > 0)
            {
                mx.Translate((-1) * zoom * (X + dx + (X1 - X) / 2), (-1) * zoom * (Y + dy + (Y1 - Y) / 2), MatrixOrder.Append);
                mx.Scale(GrpZoomX, GrpZoomY, MatrixOrder.Append);
                mx.Translate(zoom * (X + dx + (X1 - X) / 2), zoom * (Y + dy + (Y1 - Y) / 2), MatrixOrder.Append);
            }
            g.Transform = mx;
           

            if (_grapPath)
          
            {
                Brush myBrush = GetBrush(dx, dy, zoom);   
                Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
                myPen.DashStyle = DashStyleMy;

                if (selected)
                {
                    myPen.Color = Color.Red;
                    myPen.Color = Transparency(myPen.Color, 120);
                    myPen.Width = myPen.Width + 1;
                }

                GraphicsPath gp = new GraphicsPath();
               
                foreach (Ele e in objs)
                {
                    e.AddGraphPath(gp, dx, dy, zoom);
                   
                }               

               
                if (myBrush != null)
                    myBrush.Dispose();
                myPen.Dispose();
            }
           
            else
            {
                //Obsluha GroupDisplay
                Region rr = new Region();
                if (DisplayOfGroup != GroupDisplay.Default)
                {
                    bool first = true;
                    foreach (Ele e in objs)
                    {
                        GraphicsPath gp = new GraphicsPath(FillMode.Winding);                        
                        e.AddGraphPath(gp, dx, dy, zoom);
                        if (first)
                        {
                            rr.Intersect(gp);
                        }
                        else
                        {
                            switch (DisplayOfGroup)
                            {
                                case GroupDisplay.Intersect:
                                    rr.Intersect(gp);
                                    break;
                                case GroupDisplay.Xor:
                                    rr.Xor(gp);
                                    break;
                                case GroupDisplay.Exclude:
                                    rr.Exclude(gp);
                                    break;
                                default:
                                    break;
                            }
                        }
                        first = false;
                    }
                    g.SetClip(rr, CombineMode.Intersect);
                }

                foreach (Ele e in objs)
                {
                    e.Draw(g, dx, dy, zoom);
                }
                if (DisplayOfGroup != GroupDisplay.Default)
                {
                    g.ResetClip();
                }
            }

            g.Restore(gs);//obnov predchozi transformaci


            if (selected)
            {
                Brush myBrush = GetBrush(dx, dy, zoom);    
                


                Pen myPen = new Pen(Pero_barva, ScaledPenWidth(zoom));
                myPen.DashStyle = DashStyleMy;
                myPen.Color = Color.Red;
                myPen.Color = Transparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
                g.DrawRectangle(myPen, (X + dx) * zoom, (Y + dy) * zoom, (X1 - X) * zoom, (Y1 - Y) * zoom);
                      

                if (myBrush != null)
                    myBrush.Dispose();
                myPen.Dispose();
            }

            mx.Dispose();

        }


        #endregion

        
    }
}
