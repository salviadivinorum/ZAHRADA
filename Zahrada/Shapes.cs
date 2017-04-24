using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Zahrada.PomocneTridy;
using Zahrada.OdvozeneTridyEle;
using Zahrada.UndoRedoBufferTridy;

namespace Zahrada
{
	#region Delegati (metody) pro nasledujici Udalosti nad objekty - pouziti je v mych dvou UserControl: Nastroje a Platno
	
	/// <summary>
	/// Metoda pro obsluhu ZMEN vlastnosti - delegat pro udalost event a jeji argumenty
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="OptionEventArgs"/> instance containing the event data.</param>
	public delegate void OptionChangedEventHandler(object sender, OptionEventArgs e);
	

	  
	/// <summary>
	/// Metoda pro obsluhu VYBRANÉHO objektu - delegat pro udalost event a jeji argumenty   
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="PropertyEventArgs"/> instance containing the event data.</param>
	public delegate void ObjectSelectedEventHandler(object sender, PropertyEventArgs e);

	
	
	/// <summary>
	/// Udalostni trida s vnitrnimi daty objektu - o jeho stavu, ktery ma vysilat - pro zmenu svych vlastnosti   
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class PropertyEventArgs : EventArgs 
	{
		public Ele[] ele; // pole elementu obsazenych v nejakem objektu
		public bool undoable; // zda-li je objekt undo - mozny
		public bool redoable; // zda-li je objekt redo - mozny

		// konstruktor udalostni tridy
		public PropertyEventArgs(Ele[] a, bool r, bool u)
		{
			ele = a;
			undoable = u;
			redoable = r;
		}
	}
	

	/// <summary>
	/// Udalostni trida s vnitrni textovou promennou urcujici moznosti prace nad objektem
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class OptionEventArgs : EventArgs 
	{
		public string option;

		// konstruktor tridy
		public OptionEventArgs(string s)
		{
			option = s; // moznosti co delat s objektem - zadavam prubezne textovou formou - select"
			// toto je dulezite - objekty vysilaji textovou zpravu "select: a tu posloucha muj toolBox Nastroje 
		}
	}


	#endregion

	/// <summary>
	/// Hlavni trida Shapes - kolekce objektu Ele. Spravuje sadu vektorovych objektu
	/// </summary>    
	[Serializable]
	public class Shapes
	{
		#region Clenske promenne tridy Shapes
		public ArrayList List; // ZAKLADNI Seznam objektu na platne "Platno"
		public AbstractSel sRec; // promenna objektu Uchop - k manipulaci s objekty pomoci uchopu
		public Ele selEle; // Vybrany element Ele
		public int minDim = 10;

		//undo/redo buffer 
		[NonSerialized]
		private UndoBuffer undoB;

		public int dxSave;
		public int dySave;
		public int AxSave;
		public int AySave;
		public int indeOfSavedPlan;
		public float ZoomSave;
		public List<string> listradiobuttonu; 

		#endregion

		#region Konstruktor tridy Shapes

		public Shapes(float dpix, float dpiy)
		{
			List = new ArrayList();
			InitUndoBuff();
			Ele.dpix = dpix; // staticky clen tridy Ele
			Ele.dpiy = dpiy; // staticky clen tridy Ele
			listradiobuttonu = new List<string>();
		}

		#endregion		

		#region Privatni metody tridy Shapes

		/// <summary>
		/// Inicializace UndoBufferu na hodnotu 20 elementu
		/// </summary>
		private void InitUndoBuff()
		{
			undoB = new UndoBuffer(20);
		}


		/// <summary>
		/// Pomocna metoda k ovladani v Property Gridu 
		/// </summary>
		private int CountSelected()
		{
			int i = 0;
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					i++;
				}
			}
			return i;
		}


		#endregion

		#region Verejne metody tridy Shapes

		public bool IsEmpty()
		{
			return (List.Count > 0);
		}

		public void AfterLoad() // Tato metoda zdvojuje praci metody InitUndoBuff()
		{
			InitUndoBuff();
		   
			foreach (Ele el in List)
			{
				el.Průhlednost = el.SavedPruhlednost;
			}

		}

		public void CopyMultiSelected(int dx, int dy)
		{
			ArrayList tmpList = new ArrayList();
			foreach (Ele elem in List)
			{
				if (elem.selected)
				{
					Ele eL = elem.Copy();
					elem.selected = false;
					eL.Move(dx, dy);
					tmpList.Add(eL);
					//
					sRec = new SelRect(eL);
					selEle = eL;
					selEle.EndMoveRedim();
				}
			}
			foreach (Ele tmpElem in tmpList)
			{
				List.Add(tmpElem);
				// Uloz operaci do undo/redo bufferu
				StoreDo("I", tmpElem);
			}
		}

			  
		//Vraci kopii vybraneho elementu
		public Ele CpSelected()
		{
			if (selEle != null)
			{
				Ele l = selEle.Copy();
				return l;
			}
			return null;
		}

				
		// Kopiruje oznaceny objekt, oznaci ho jako vybrany		
		public void CopySelected(int dx, int dy)
		{
			if (selEle != null)
			{

				Ele L = CpSelected();
				L.Move(dx, dy);
				DeSelect();
				List.Add(L);

				// uklada operaci do undo/redo bufferu
				StoreDo("I", L);

				sRec = new SelRect(L);
				//sRec.sonoUnaLinea = L.sonoUnaLinea;
				selEle = L;
				selEle.EndMoveRedim();
			}
		}

			
		// Smaze vybrany objekt		
		public void RmSelected()
		{
			ArrayList tmpList = new ArrayList();
			foreach (Ele elem in List)
			{
				if (elem.selected)
				{
					tmpList.Add(elem);
				}
			}

			if (selEle != null)
			{
				selEle = null;
				sRec = null;
			}

			foreach (Ele tmpElem in tmpList)
			{
				List.Remove(tmpElem);
				StoreDo("D", tmpElem);

			}
		}

		
		// Seskupi oznacene objekty		
		public void GroupSelected()
		{
			ArrayList tmpList = new ArrayList();
			foreach (Ele elem in List)
			{
				if (elem.selected)
				{
					tmpList.Add(elem);
				}
			}

			if (selEle != null)
			{
				selEle = null;
				sRec = null;
			}

			foreach (Ele tmpElem in tmpList)
			{
				List.Remove(tmpElem);
			}

			Group g = new Group(tmpList);

			List.Add(g);

			sRec = new SelRect(g);
			selEle = g;
			selEle.Select();

			// kdyz seskupuji/rusim seskupeni musim resetovat UndoBuffer
			undoB = new UndoBuffer(20);
		}

		
		// Rozlozi skupinu elementu	
		public void DeGroupSelected()
		{
			ArrayList tmpList = new ArrayList();
			foreach (Ele elem in List)
			{
				if (elem.selected)
				{
					tmpList.Add(elem);
				}
			}

			if (selEle != null)
			{
				selEle = null;
				sRec = null;
			}
			bool found = false;
			foreach (Ele tmpElem in tmpList)
			{
				ArrayList tmpL = tmpElem.DeGroup();

				if (tmpL != null)
				{
					foreach (Ele e1 in tmpL)
					{
						List.Add(e1);
					}
					List.Remove(tmpElem);
					found = true;
				}
			}
			if (found)
			{
				// kdyyz seskupuji / rozkladam Group musim resetovat UndoBuffer
				undoB = new UndoBuffer(20);
			}

		}

		public void MovePoint(int dx, int dy)
		{
			((SelPoly)sRec).MovePoints(dx, dy);
			((SelPoly)sRec).ReCreateCreationHandles((PointSet)selEle);
		}

		

		public void AddPoint()
		{
			if (sRec is SelPoly)
			{
				PointWrapper p = ((SelPoly)sRec).GetNewPoint();
				int i = ((SelPoly)this.sRec).GetIndex();
				if (i > 0)
				{
					((PointSet)this.selEle).points.Insert(i - 1, p);
					sRec = new SelPoly(selEle);// vytvori uchopovy obdelnik
				}
			}			

		}      
	  

		public void Move(int dx, int dy)
		{
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					e.Move(dx, dy);
				}
			}
		}

		// Zakladni metoda pro prichytavani elementu k mrizce
		public void Fit2Grid(int gridsize)
		{
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					e.Fit2grid(gridsize);
				}
			}
		}

		public void EndMove()
		{
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					e.EndMoveRedim();
					if (!e.AmIaGroup)
						StoreDo("U", e);
				}
			}
		}


		// Volam postupne nahoru od tridy Shapes pres platno do toolboxu, 
		// v nemz je pro mujPropertyGrid obsluha udalosti PropertyValueChanged		
		public void PropertyChanged()
		{
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					StoreDo("U", e);
				}
			}
		}

		
		// Vraci pole s vybranymi elementy. Pouziti je pro Property Grid	
		public Ele[] GetSelectedArray()
		{
			Ele[] myArray = new Ele[CountSelected()];
			int i = 0;
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					myArray[i] = e;
					i++;
				}

			}
			return myArray;
		}

		//  Vraci seznam s vybranymi elementy. Pouziti je v metode SaveObj	
		public ArrayList GetSelectedList()
		{
			ArrayList tmpL = new ArrayList();
			foreach (Ele e in List)
			{
				if (e.selected)
				{
					tmpL.Add(e);
				}
			}
			return tmpL;
		}

	
		// Nastavuje seznam elementu List. Pouziti je v metode LoadObj		
		public void SetList(ArrayList a)
		{
			foreach (Ele e in a)
			{
				List.Add(e);

			}
		}

		
		// Posun do popredi	
		public void MoveFront()
		{
			if (selEle != null)
			{
				List.Remove(selEle);
				List.Add(selEle); // posune element na konec seznamu
			}
		}

		
		// Posun do pozadi	
		public void MoveBack()
		{
			if (selEle != null)
			{
				List.Remove(selEle);
				List.Insert(0, selEle); // vlozi element na zacatek seznamu index 0
				this.DeSelect();
			}
		}

		
		// Zrus vyber vsech elementu		
		public void DeSelect()
		{
			foreach (Ele obj in List)
			{
				obj.selected = false;
			}
			selEle = null;
			sRec = null;
		}


		
		// Vybere posledni shape obsahujici souradnice x,y		
		public void ClickOnShape(int x, int y, RichTextBox r)
		{
			sRec = null;
			selEle = null;
			foreach (Ele obj in this.List)
			{
				obj.selected = false;
				obj.DeSelect();
				if (obj.Contains(x, y))
				{
					selEle = obj; // ulozi referenci nalezeneho objektu
								  // break;
								  // nedelej break - pro prekryvajici se objekty beru posledni pridany objekt 
				}
			}
			if (selEle != null)
			{
				selEle.selected = true;
				selEle.Select();
				selEle.Select(r);
				// nyni vytvorim uchopovy objekt
				if (selEle is PointSet)
					sRec = new SelPoly(selEle); //vytvori uchopovy obdelnik typu polygonovy vyber
				else
					sRec = new SelRect(selEle); //vytvori uchopovy obdelnik typu obdelnikovy vyber
			}
		}

	
		// Vybere vsechny elementy (objekty) ve Vyberovem obdelniku	
		public void MultiSelect(int startX, int startY, int endX, int endY, RichTextBox r)
		{
			sRec = null;
			selEle = null;
			foreach (Ele obj in List)
			{

				obj.selected = false;
				obj.DeSelect();  // pouziti pro deselekci bodu v polygonech
				int x = obj.GetX;
				int x1 = obj.GetX1;
				int y = obj.GetY;
				int y1 = obj.GetY1;
				int c = 0;
				if (x > x1)
				{
					c = x;
					x = x1;
					x1 = c;
				}
				if (y > y1)
				{
					c = y;
					y = y1;
					y1 = c;
				}
				if (x <= endX & x1 >= startX & y <= endY & y1 >= startY)
				{
					selEle = obj; //ulozi referenci nalezeneho objektu
					obj.selected = true; //oznaci objekt jako vybrany
					obj.Select();
					obj.Select(r);
					obj.Select(startX, startY, endX, endY);
				}
			}
			if (selEle != null)
			{
				if (selEle is PointSet)
					sRec = new SelPoly(selEle);//vytvori uchopovy obdelnik typu polygonovy vyber
				else
					sRec = new SelRect(selEle);//vytvori uchopovy obdelnik typu obdlenikovy vyber
											   
			}
		}

		#endregion

		#region Verejne metody tridy Shapes pro Undo/Redo operace

		// Prace s Unod/Redo nad objekty v undobufferu
		public bool UndoEnabled()
		{
			return undoB.UndoAble();
		}

		public bool RedoEnabled()
		{
			return undoB.RedoAble();
		}

		public void StoreDo(string option, Ele e)
		{
			Ele olde = null;
			if (e.undoEle != null)
				olde = e.undoEle.Copy();
			Ele newe = e.Copy();
			BufferedElement buff = new BufferedElement(e, newe, olde, option);
			undoB.Add2Buff(buff);
			e.undoEle = e.Copy();
		}

		public void Undo()
		{
			BufferedElement buff = (BufferedElement)undoB.Undo();
			if (buff != null)
			{
				switch (buff.op)
				{
					case "U":
						buff.objRef.CopyFrom(buff.oldE);
						break;
					case "I":
						List.Remove(buff.objRef);
						break;
					case "D":
						List.Add(buff.objRef);
						break;
					default:
						break;
				}
			}
		}

		public void Redo()
		{
			BufferedElement buff = (BufferedElement)undoB.Redo();
			if (buff != null)
			{
				switch (buff.op)
				{
					case "U":
						buff.objRef.CopyFrom(buff.newE);
						break;
					case "I":
						List.Add(buff.objRef);
						break;
					case "D":
						List.Remove(buff.objRef);
						break;
					default:
						break;
				}
			}

		}

		#endregion

		#region Verejne metody pro obecne vykreslovani Draw (vsech/vybranych) objektu
				
		// Vykresli Vsechny objekty		
		public void Draw(Graphics g, int dx, int dy, float zoom)
		{
			bool almostOneSelected = false; 
			foreach (Ele obj in List)
			{
				obj.Draw(g, dx, dy, zoom);
				if (obj.selected) // zohlednuje predchozi vyber objektu
					almostOneSelected = true;
			}
			if (almostOneSelected)
				if (sRec != null)
					sRec.Draw(g, dx, dy, zoom);
		}

			
		// Vykresli vsechny Nevybrane objekty	
		public void DrawUnselected(Graphics g, int dx, int dy, float zoom)
		{
			g.PageScale = 10f; // nevim proc tu je to 10f
			//bool almostOneSelected = false;
			foreach (Ele obj in List)
			{

				if (!obj.selected)
				{
					obj.Draw(g, dx, dy, zoom);
				}
			}
		}

		
		// Vykresli vsechny vybrane obejkty	
		public void DrawSelected(Graphics g, int dx, int dy, float zoom)
		{
			bool almostOneSelected = false;

			foreach (Ele obj in List)
			{
				if (obj.selected)
				{
					obj.Draw(g, dx, dy, zoom);
					almostOneSelected = true;
				}
			}
			if (almostOneSelected)
				if (sRec != null)
				{
					sRec.Draw(g, dx, dy, zoom);
				}
		}

		#endregion

		#region Verejne metody pro pridavani Elementu do seznamu List - pouzivam v udalostech MouseUp nad platnem
	  
		// Do Listu prida Polygon - vseobecny	
		public void AddPoly(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa, bool curv, bool closed, bool textureFilled, TextureBrush textura)
		{    
			/*if (x1 - minDim <= x)
				x1 = x + minDim;
			if (y1 - minDim <= y)
				y1 = y + minDim;*/
			
			DeSelect();
			PointSet r = new PointSet(x, y, x1, y1, aa);

			r.Closed = closed;
			r.Pero_barva = penC;
			r.Pero_šířka = penW;
			r.FillColor = fillC;
			r.ColorFilled = filled;
			r.Zakřivení = curv;
			r.TextureFilled = textureFilled;
			r.FillTexture = textura;

			List.Add(r);            
			StoreDo("I", r); // uloz do undo/redo bufferu

			sRec = new SelPoly(r);
			selEle = r;
			selEle.Select();
		}


		
		// Do Listu prida Obdelnik	
		public void AddRect(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, bool textureFilled, TextureBrush textura)
		{
			if (x1 - minDim <= x)
				x1 = x + minDim;
			if (y1 - minDim <= y)
				y1 = y + minDim;

			DeSelect();
			Rect r = new Rect(x, y, x1, y1);
			r.Pero_barva = penC;
			r.Pero_šířka = penW;
			r.FillColor = fillC;
			r.ColorFilled = filled;
			r.TextureFilled = textureFilled;
			r.FillTexture = textura;

			List.Add(r);
			
			StoreDo("I", r);

			sRec = new SelRect(r);
			selEle = r;
			selEle.Select();
		}
		
		
		// Do Listu prida Caru	
		public void AddLine(int x, int y, int x1, int y1, Color penC, float penW)
		{

			DeSelect();
			Line r = new Line(x, y, x1, y1);	
			r.Pero_barva = penC;
			r.Pero_šířka = penW;

			List.Add(r);
			StoreDo("I", r);
			sRec = new SelRect(r);

			selEle = r;
			selEle.Select();
		}
		
		
		// Do Listu prida Jednoduchy text	
		public void AddSimpleTextBox(int x, int y, int x1, int y1, RichTextBox t, Color penC, Color fillC, float penW, bool filled, bool textureFilled, TextureBrush textura)
		{
			if (x1 - minDim <= x)
				x1 = x + minDim;
			if (y1 - minDim <= y)
				y1 = y + minDim;

			DeSelect();
			Stext r = new Stext(x, y, x1, y1);

			r.Text = t.Text;
			r.CharFont = t.SelectionFont;  //t.Font;


			r.Pero_barva = penC;
			r.Pero_šířka = penW;
			r.FillColor = fillC;
			r.ColorFilled = filled;
			r.TextureFilled = textureFilled;
			r.FillTexture = textura;

			List.Add(r);
			
			StoreDo("I", r);

			sRec = new SelRect(r);
			selEle = r;
			selEle.Select();
		}


		
		// Do Listu prida ImageBox - obrazek = zahradni prvek	
		public void AddImageBox(int x, int y, int x1, int y1, string st, Color penC, float penW)
		{
			if (x1 - (minDim*10) <= x)
				x1 = x + (minDim*10);
			if (y1 - (minDim+10) <= y)
				y1 = y + (minDim*10);

			DeSelect();
			ImageBox r = new ImageBox(x, y, x1, y1);
			r.Pero_barva = penC;
			r.Pero_šířka = penW;

			List.Add(r);
			
			StoreDo("I", r);

			if (!(st == null))
			{
				try
				{
					Bitmap loadTexture = new Bitmap(st);
					r.Prvek = loadTexture;
				}
				catch { }
			}


			sRec = new SelRect(r);
			selEle = r;
			selEle.Select();
		}

		
		// Do Listu prida Elipsu	
		public void AddElipse(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool colorFilled, bool textureFilled, TextureBrush textura)
		{
			if (x1 - minDim <= x)
				x1 = x + minDim;
			if (y1 - minDim <= y)
				y1 = y + minDim;

			DeSelect();
			Ellipse r = new Ellipse(x, y, x1, y1);
			r.Pero_barva = penC;
			r.Pero_šířka = penW;
			r.FillColor = fillC;
			r.ColorFilled = colorFilled;
			r.TextureFilled = textureFilled;
			r.FillTexture = textura;

			List.Add(r);
			
			StoreDo("I", r);

			sRec = new SelRect(r);
			selEle = r;
			selEle.Select();
		}
		
		#endregion

	}
}



