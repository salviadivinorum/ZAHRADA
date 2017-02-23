namespace Zahrada
{
    public partial class Nastroje
    {        

        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.funkce = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.vytvoritGroupBox = new System.Windows.Forms.GroupBox();
            this.lineBtn = new System.Windows.Forms.Button();
            this.freeHandBtn = new System.Windows.Forms.Button();
            this.rectBtn = new System.Windows.Forms.Button();
            this.polygonBtn = new System.Windows.Forms.Button();
            this.circBtn = new System.Windows.Forms.Button();
            this.arcBtn = new System.Windows.Forms.Button();
            this.imageBtn = new System.Windows.Forms.Button();
            this.simpleTextBtn = new System.Windows.Forms.Button();
            this.zmenitGroupBox = new System.Windows.Forms.GroupBox();
            this.copyBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.toBackBtn = new System.Windows.Forms.Button();
            this.toFrontBtn = new System.Windows.Forms.Button();
            this.mirrorYbtn = new System.Windows.Forms.Button();
            this.mirrorXbtn = new System.Windows.Forms.Button();
            this.deGroupBtn = new System.Windows.Forms.Button();
            this.groupBtn = new System.Windows.Forms.Button();
            this.strucneVlastnosti = new System.Windows.Forms.TabPage();
            this.rozsireneVlastnosti = new System.Windows.Forms.TabPage();
            this.mujPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.knihovna = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.mujFilteredPropertyGrid = new Zahrada.OdvozenyPropertyGrid.FilteredPropertyGrid();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.funkce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.vytvoritGroupBox.SuspendLayout();
            this.zmenitGroupBox.SuspendLayout();
            this.strucneVlastnosti.SuspendLayout();
            this.rozsireneVlastnosti.SuspendLayout();
            this.knihovna.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.funkce);
            this.tabControl.Controls.Add(this.strucneVlastnosti);
            this.tabControl.Controls.Add(this.rozsireneVlastnosti);
            this.tabControl.Controls.Add(this.knihovna);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(320, 600);
            this.tabControl.TabIndex = 0;
            this.tabControl.Click += new System.EventHandler(this.tabControl_Click);
            // 
            // funkce
            // 
            this.funkce.Controls.Add(this.splitContainer);
            this.funkce.Location = new System.Drawing.Point(4, 25);
            this.funkce.Name = "funkce";
            this.funkce.Padding = new System.Windows.Forms.Padding(3);
            this.funkce.Size = new System.Drawing.Size(312, 571);
            this.funkce.TabIndex = 0;
            this.funkce.Text = "Funkce";
            this.funkce.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.vytvoritGroupBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.zmenitGroupBox);
            this.splitContainer.Size = new System.Drawing.Size(306, 565);
            this.splitContainer.SplitterDistance = 261;
            this.splitContainer.TabIndex = 0;
            // 
            // vytvoritGroupBox
            // 
            this.vytvoritGroupBox.Controls.Add(this.lineBtn);
            this.vytvoritGroupBox.Controls.Add(this.freeHandBtn);
            this.vytvoritGroupBox.Controls.Add(this.rectBtn);
            this.vytvoritGroupBox.Controls.Add(this.polygonBtn);
            this.vytvoritGroupBox.Controls.Add(this.circBtn);
            this.vytvoritGroupBox.Controls.Add(this.arcBtn);
            this.vytvoritGroupBox.Controls.Add(this.imageBtn);
            this.vytvoritGroupBox.Controls.Add(this.simpleTextBtn);
            this.vytvoritGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vytvoritGroupBox.Location = new System.Drawing.Point(0, 0);
            this.vytvoritGroupBox.Name = "vytvoritGroupBox";
            this.vytvoritGroupBox.Size = new System.Drawing.Size(306, 261);
            this.vytvoritGroupBox.TabIndex = 11;
            this.vytvoritGroupBox.TabStop = false;
            this.vytvoritGroupBox.Text = "Vytvořit";
            // 
            // lineBtn
            // 
            this.lineBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.lineBtn.Location = new System.Drawing.Point(8, 21);
            this.lineBtn.Name = "lineBtn";
            this.lineBtn.Size = new System.Drawing.Size(45, 45);
            this.lineBtn.TabIndex = 1;
            this.lineBtn.Text = "Čára";
            this.lineBtn.UseVisualStyleBackColor = true;
            this.lineBtn.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // freeHandBtn
            // 
            this.freeHandBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.freeHandBtn.Location = new System.Drawing.Point(110, 21);
            this.freeHandBtn.Name = "freeHandBtn";
            this.freeHandBtn.Size = new System.Drawing.Size(45, 45);
            this.freeHandBtn.TabIndex = 10;
            this.freeHandBtn.Text = "Volná čára";
            this.freeHandBtn.UseVisualStyleBackColor = true;
            this.freeHandBtn.Click += new System.EventHandler(this.freeHandBtn_Click);
            // 
            // rectBtn
            // 
            this.rectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.rectBtn.Location = new System.Drawing.Point(161, 21);
            this.rectBtn.Name = "rectBtn";
            this.rectBtn.Size = new System.Drawing.Size(45, 45);
            this.rectBtn.TabIndex = 0;
            this.rectBtn.Text = "Obdél";
            this.rectBtn.UseVisualStyleBackColor = false;
            this.rectBtn.Click += new System.EventHandler(this.rectBtn_Click);
            // 
            // polygonBtn
            // 
            this.polygonBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.polygonBtn.Location = new System.Drawing.Point(59, 21);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(45, 45);
            this.polygonBtn.TabIndex = 9;
            this.polygonBtn.Text = "Poly gon";
            this.polygonBtn.UseVisualStyleBackColor = true;
            this.polygonBtn.Click += new System.EventHandler(this.polygonBtn_Click);
            // 
            // circBtn
            // 
            this.circBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.circBtn.Location = new System.Drawing.Point(59, 72);
            this.circBtn.Name = "circBtn";
            this.circBtn.Size = new System.Drawing.Size(45, 45);
            this.circBtn.TabIndex = 2;
            this.circBtn.Text = "Elipsa";
            this.circBtn.UseVisualStyleBackColor = true;
            this.circBtn.Click += new System.EventHandler(this.circBtn_Click);
            // 
            // arcBtn
            // 
            this.arcBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.arcBtn.Location = new System.Drawing.Point(8, 72);
            this.arcBtn.Name = "arcBtn";
            this.arcBtn.Size = new System.Drawing.Size(45, 45);
            this.arcBtn.TabIndex = 8;
            this.arcBtn.Text = "Obl";
            this.arcBtn.UseVisualStyleBackColor = true;
            this.arcBtn.Click += new System.EventHandler(this.arcBtn_Click);
            // 
            // imageBtn
            // 
            this.imageBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.imageBtn.Location = new System.Drawing.Point(161, 72);
            this.imageBtn.Name = "imageBtn";
            this.imageBtn.Size = new System.Drawing.Size(45, 45);
            this.imageBtn.TabIndex = 7;
            this.imageBtn.Text = "Obr";
            this.imageBtn.UseVisualStyleBackColor = true;
            this.imageBtn.Click += new System.EventHandler(this.imageBtn_Click);
            // 
            // simpleTextBtn
            // 
            this.simpleTextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.simpleTextBtn.Location = new System.Drawing.Point(110, 72);
            this.simpleTextBtn.Name = "simpleTextBtn";
            this.simpleTextBtn.Size = new System.Drawing.Size(45, 45);
            this.simpleTextBtn.TabIndex = 5;
            this.simpleTextBtn.Text = "Text";
            this.simpleTextBtn.UseVisualStyleBackColor = true;
            this.simpleTextBtn.Click += new System.EventHandler(this.simpleTextBtn_Click);
            // 
            // zmenitGroupBox
            // 
            this.zmenitGroupBox.Controls.Add(this.copyBtn);
            this.zmenitGroupBox.Controls.Add(this.deleteBtn);
            this.zmenitGroupBox.Controls.Add(this.toBackBtn);
            this.zmenitGroupBox.Controls.Add(this.toFrontBtn);
            this.zmenitGroupBox.Controls.Add(this.mirrorYbtn);
            this.zmenitGroupBox.Controls.Add(this.mirrorXbtn);
            this.zmenitGroupBox.Controls.Add(this.deGroupBtn);
            this.zmenitGroupBox.Controls.Add(this.groupBtn);
            this.zmenitGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zmenitGroupBox.Location = new System.Drawing.Point(0, 0);
            this.zmenitGroupBox.Name = "zmenitGroupBox";
            this.zmenitGroupBox.Size = new System.Drawing.Size(306, 300);
            this.zmenitGroupBox.TabIndex = 12;
            this.zmenitGroupBox.TabStop = false;
            this.zmenitGroupBox.Text = "Změnit";
            // 
            // copyBtn
            // 
            this.copyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.copyBtn.Location = new System.Drawing.Point(161, 72);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(45, 45);
            this.copyBtn.TabIndex = 9;
            this.copyBtn.Text = "Kopie";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deleteBtn.Location = new System.Drawing.Point(110, 72);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(45, 45);
            this.deleteBtn.TabIndex = 8;
            this.deleteBtn.Text = "Smaž";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // toBackBtn
            // 
            this.toBackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toBackBtn.Location = new System.Drawing.Point(59, 72);
            this.toBackBtn.Name = "toBackBtn";
            this.toBackBtn.Size = new System.Drawing.Size(45, 45);
            this.toBackBtn.TabIndex = 7;
            this.toBackBtn.Text = "Vzad";
            this.toBackBtn.UseVisualStyleBackColor = true;
            this.toBackBtn.Click += new System.EventHandler(this.toBackBtn_Click);
            // 
            // toFrontBtn
            // 
            this.toFrontBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toFrontBtn.Location = new System.Drawing.Point(8, 72);
            this.toFrontBtn.Name = "toFrontBtn";
            this.toFrontBtn.Size = new System.Drawing.Size(45, 45);
            this.toFrontBtn.TabIndex = 6;
            this.toFrontBtn.Text = "Vpřed";
            this.toFrontBtn.UseVisualStyleBackColor = true;
            this.toFrontBtn.Click += new System.EventHandler(this.toFrontBtn_Click);
            // 
            // mirrorYbtn
            // 
            this.mirrorYbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mirrorYbtn.Location = new System.Drawing.Point(161, 21);
            this.mirrorYbtn.Name = "mirrorYbtn";
            this.mirrorYbtn.Size = new System.Drawing.Size(45, 45);
            this.mirrorYbtn.TabIndex = 5;
            this.mirrorYbtn.Text = "Zrcadli Y";
            this.mirrorYbtn.UseVisualStyleBackColor = true;
            // 
            // mirrorXbtn
            // 
            this.mirrorXbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mirrorXbtn.Location = new System.Drawing.Point(110, 21);
            this.mirrorXbtn.Name = "mirrorXbtn";
            this.mirrorXbtn.Size = new System.Drawing.Size(45, 45);
            this.mirrorXbtn.TabIndex = 4;
            this.mirrorXbtn.Text = "Zracdli X";
            this.mirrorXbtn.UseVisualStyleBackColor = true;
            // 
            // deGroupBtn
            // 
            this.deGroupBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.deGroupBtn.Location = new System.Drawing.Point(59, 21);
            this.deGroupBtn.Name = "deGroupBtn";
            this.deGroupBtn.Size = new System.Drawing.Size(45, 45);
            this.deGroupBtn.TabIndex = 3;
            this.deGroupBtn.Text = "Rozkl skup";
            this.deGroupBtn.UseVisualStyleBackColor = true;
            this.deGroupBtn.Click += new System.EventHandler(this.deGroupBtn_Click);
            // 
            // groupBtn
            // 
            this.groupBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBtn.Location = new System.Drawing.Point(8, 21);
            this.groupBtn.Name = "groupBtn";
            this.groupBtn.Size = new System.Drawing.Size(45, 45);
            this.groupBtn.TabIndex = 2;
            this.groupBtn.Text = "Skup";
            this.groupBtn.UseVisualStyleBackColor = true;
            this.groupBtn.Click += new System.EventHandler(this.groupBtn_Click);
            // 
            // strucneVlastnosti
            // 
            this.strucneVlastnosti.Controls.Add(this.mujFilteredPropertyGrid);
            this.strucneVlastnosti.Location = new System.Drawing.Point(4, 25);
            this.strucneVlastnosti.Name = "strucneVlastnosti";
            this.strucneVlastnosti.Padding = new System.Windows.Forms.Padding(3);
            this.strucneVlastnosti.Size = new System.Drawing.Size(312, 571);
            this.strucneVlastnosti.TabIndex = 2;
            this.strucneVlastnosti.Text = "Struč vlast";
            this.strucneVlastnosti.UseVisualStyleBackColor = true;
            // 
            // rozsireneVlastnosti
            // 
            this.rozsireneVlastnosti.Controls.Add(this.mujPropertyGrid);
            this.rozsireneVlastnosti.Location = new System.Drawing.Point(4, 25);
            this.rozsireneVlastnosti.Name = "rozsireneVlastnosti";
            this.rozsireneVlastnosti.Padding = new System.Windows.Forms.Padding(3);
            this.rozsireneVlastnosti.Size = new System.Drawing.Size(312, 571);
            this.rozsireneVlastnosti.TabIndex = 1;
            this.rozsireneVlastnosti.Text = "Rozšířené vlast";
            this.rozsireneVlastnosti.UseVisualStyleBackColor = true;
            // 
            // mujPropertyGrid
            // 
            this.mujPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mujPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.mujPropertyGrid.Name = "mujPropertyGrid";
            this.mujPropertyGrid.Size = new System.Drawing.Size(306, 565);
            this.mujPropertyGrid.TabIndex = 0;
            this.mujPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mujPropertyGrid_PropertyValueChanged);
            // 
            // knihovna
            // 
            this.knihovna.Controls.Add(this.label3);
            this.knihovna.Controls.Add(this.label2);
            this.knihovna.Controls.Add(this.checkBox5);
            this.knihovna.Controls.Add(this.checkBox4);
            this.knihovna.Controls.Add(this.checkBox3);
            this.knihovna.Controls.Add(this.checkBox2);
            this.knihovna.Controls.Add(this.checkBox1);
            this.knihovna.Controls.Add(this.label1);
            this.knihovna.Location = new System.Drawing.Point(4, 25);
            this.knihovna.Name = "knihovna";
            this.knihovna.Size = new System.Drawing.Size(312, 571);
            this.knihovna.TabIndex = 3;
            this.knihovna.Text = "Knihovna";
            this.knihovna.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(2, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "A) Zakreslení stávajícího stavu:";
            // 
            // mujFilteredPropertyGrid
            // 
            this.mujFilteredPropertyGrid.BrowsableProperties = null;
            this.mujFilteredPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mujFilteredPropertyGrid.HiddenAttributes = null;
            this.mujFilteredPropertyGrid.HiddenProperties = null;
            this.mujFilteredPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.mujFilteredPropertyGrid.Name = "mujFilteredPropertyGrid";
            this.mujFilteredPropertyGrid.Size = new System.Drawing.Size(306, 565);
            this.mujFilteredPropertyGrid.TabIndex = 0;
            this.mujFilteredPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mujFilteredPropertyGrid_PropertyValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 47);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(175, 21);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Tvar pozemku zahrady";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 74);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(147, 21);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Oplocení pozemku";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 102);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(287, 21);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Stavby na pozemku nebo na jeho hranici";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 130);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(220, 21);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "Zpevněné plochy na pozemku";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(6, 158);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(212, 21);
            this.checkBox5.TabIndex = 5;
            this.checkBox5.Text = "Stávající rostliny na pozemku";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(265, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stručný průvodce návrhem zahrady";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(3, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "B) Zakreslení nového stavu:";
            // 
            // Nastroje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "Nastroje";
            this.Size = new System.Drawing.Size(320, 600);
            this.tabControl.ResumeLayout(false);
            this.funkce.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.vytvoritGroupBox.ResumeLayout(false);
            this.zmenitGroupBox.ResumeLayout(false);
            this.strucneVlastnosti.ResumeLayout(false);
            this.rozsireneVlastnosti.ResumeLayout(false);
            this.knihovna.ResumeLayout(false);
            this.knihovna.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage funkce;
        private System.Windows.Forms.TabPage rozsireneVlastnosti;
        private System.Windows.Forms.TabPage strucneVlastnosti;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox vytvoritGroupBox;
        private System.Windows.Forms.Button lineBtn;
        private System.Windows.Forms.Button freeHandBtn;
        private System.Windows.Forms.Button rectBtn;
        private System.Windows.Forms.Button polygonBtn;
        private System.Windows.Forms.Button circBtn;
        private System.Windows.Forms.Button arcBtn;
        private System.Windows.Forms.Button imageBtn;
        private System.Windows.Forms.Button simpleTextBtn;
        private System.Windows.Forms.GroupBox zmenitGroupBox;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button toBackBtn;
        private System.Windows.Forms.Button toFrontBtn;
        private System.Windows.Forms.Button mirrorYbtn;
        private System.Windows.Forms.Button mirrorXbtn;
        private System.Windows.Forms.Button deGroupBtn;
        private System.Windows.Forms.Button groupBtn;
        public System.Windows.Forms.PropertyGrid mujPropertyGrid;
        private System.Windows.Forms.TabPage knihovna;
        public OdvozenyPropertyGrid.FilteredPropertyGrid mujFilteredPropertyGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
