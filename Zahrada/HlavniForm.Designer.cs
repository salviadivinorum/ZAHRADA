namespace Zahrada
{
    public partial class HlavniForm
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

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HlavniForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToJpgpngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.moveToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pencilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nápovedaCelkovatoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.redoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.penColortoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.penWidthtoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.fillColorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fillingOnOffToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.closedToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.gridToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.zoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.zoomToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.zoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mujColorDialog = new System.Windows.Forms.ColorDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.vlozenyToolBox = new Zahrada.UserControlNastroje();
            this.vlozenePlatno = new Zahrada.Platno();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.drawToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1327, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDocumentToolStripMenuItem,
            this.openDocumentToolStripMenuItem,
            this.saveDocumentToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToJpgpngToolStripMenuItem,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.fileToolStripMenuItem.Text = "Soubor";
            // 
            // newDocumentToolStripMenuItem
            // 
            this.newDocumentToolStripMenuItem.Name = "newDocumentToolStripMenuItem";
            this.newDocumentToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.newDocumentToolStripMenuItem.Text = "Nový dokument";
            // 
            // openDocumentToolStripMenuItem
            // 
            this.openDocumentToolStripMenuItem.Name = "openDocumentToolStripMenuItem";
            this.openDocumentToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.openDocumentToolStripMenuItem.Text = "Open Document";
            // 
            // saveDocumentToolStripMenuItem
            // 
            this.saveDocumentToolStripMenuItem.Name = "saveDocumentToolStripMenuItem";
            this.saveDocumentToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveDocumentToolStripMenuItem.Text = "Ulož dokument";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.saveAsToolStripMenuItem.Text = "Ulož jako ...";
            // 
            // exportToJpgpngToolStripMenuItem
            // 
            this.exportToJpgpngToolStripMenuItem.Name = "exportToJpgpngToolStripMenuItem";
            this.exportToJpgpngToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.exportToJpgpngToolStripMenuItem.Text = "Export do jpg/png...";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.printToolStripMenuItem.Text = "Tisk ...";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.printPreviewToolStripMenuItem.Text = "Náhled tisku ...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.exitToolStripMenuItem.Text = "Ukončit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.moveToFrontToolStripMenuItem,
            this.moveToBackToolStripMenuItem,
            this.toolStripSeparator3,
            this.propertiesToolStripMenuItem1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.editToolStripMenuItem.Text = "Upravit";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.selectAllToolStripMenuItem.Text = "Vybrat vše";
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.unselectAllToolStripMenuItem.Text = "Zrušit vybrat vše";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.deleteToolStripMenuItem.Text = "Smazat";
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.deleteAllToolStripMenuItem.Text = "Smazat vše";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
            // 
            // moveToFrontToolStripMenuItem
            // 
            this.moveToFrontToolStripMenuItem.Name = "moveToFrontToolStripMenuItem";
            this.moveToFrontToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.moveToFrontToolStripMenuItem.Text = "Přesunout vpřed";
            // 
            // moveToBackToolStripMenuItem
            // 
            this.moveToBackToolStripMenuItem.Name = "moveToBackToolStripMenuItem";
            this.moveToBackToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.moveToBackToolStripMenuItem.Text = "Přesunout dozadu";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(200, 6);
            // 
            // propertiesToolStripMenuItem1
            // 
            this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
            this.propertiesToolStripMenuItem1.Size = new System.Drawing.Size(203, 26);
            this.propertiesToolStripMenuItem1.Text = "Vlastnosti";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scaleToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.viewToolStripMenuItem.Text = "Zobrazit";
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.scaleToolStripMenuItem.Text = "Měřítko ...";
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointerToolStripMenuItem,
            this.rectangleToolStripMenuItem,
            this.ellipseToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.pencilToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.drawToolStripMenuItem.Text = "Kreslit";
            // 
            // pointerToolStripMenuItem
            // 
            this.pointerToolStripMenuItem.Name = "pointerToolStripMenuItem";
            this.pointerToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.pointerToolStripMenuItem.Text = "Čára";
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.rectangleToolStripMenuItem.Text = "Obdélník";
            // 
            // ellipseToolStripMenuItem
            // 
            this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.ellipseToolStripMenuItem.Text = "Elipsa";
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.lineToolStripMenuItem.Text = "Multičára";
            // 
            // pencilToolStripMenuItem
            // 
            this.pencilToolStripMenuItem.Name = "pencilToolStripMenuItem";
            this.pencilToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.pencilToolStripMenuItem.Text = "Volná čára";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nápovedaCelkovatoolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.helpToolStripMenuItem.Text = "Nápověda";
            // 
            // nápovedaCelkovatoolStripMenuItem
            // 
            this.nápovedaCelkovatoolStripMenuItem.Name = "nápovedaCelkovatoolStripMenuItem";
            this.nápovedaCelkovatoolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.nápovedaCelkovatoolStripMenuItem.Text = "Nápověda aplikace";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.aboutToolStripMenuItem.Text = "O aplikaci";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.saveAsToolStripButton,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.undoToolStripButton,
            this.redoToolStripButton,
            this.toolStripLabel2,
            this.toolStripSeparator5,
            this.penColortoolStripButton,
            this.penWidthtoolStripComboBox,
            this.fillColorToolStripButton,
            this.fillingOnOffToolStripComboBox,
            this.closedToolStripComboBox,
            this.gridToolStripComboBox,
            this.toolStripLabel3,
            this.zoomInToolStripButton,
            this.zoomToolStripComboBox,
            this.zoomOutToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1327, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = global::Zahrada.Properties.Resources.newdoc;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.newToolStripButton.Text = "toolStripButton1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Zahrada.Properties.Resources.open;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.openToolStripButton.Text = "toolStripButton1";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Zahrada.Properties.Resources.save;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.saveToolStripButton.Text = "toolStripButton1";
            // 
            // saveAsToolStripButton
            // 
            this.saveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAsToolStripButton.Image = global::Zahrada.Properties.Resources.saveas;
            this.saveAsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsToolStripButton.Name = "saveAsToolStripButton";
            this.saveAsToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.saveAsToolStripButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(17, 27);
            this.toolStripLabel1.Text = "  ";
            // 
            // undoToolStripButton
            // 
            this.undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoToolStripButton.Image = global::Zahrada.Properties.Resources.undo;
            this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStripButton.Name = "undoToolStripButton";
            this.undoToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.undoToolStripButton.Text = "toolStripButton1";
            this.undoToolStripButton.Click += new System.EventHandler(this.undoToolStripButton_Click);
            // 
            // redoToolStripButton
            // 
            this.redoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoToolStripButton.Image = global::Zahrada.Properties.Resources.redo;
            this.redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoToolStripButton.Name = "redoToolStripButton";
            this.redoToolStripButton.Size = new System.Drawing.Size(24, 27);
            this.redoToolStripButton.Text = "toolStripButton1";
            this.redoToolStripButton.Click += new System.EventHandler(this.redoToolStripButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(17, 27);
            this.toolStripLabel2.Text = "  ";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 30);
            // 
            // penColortoolStripButton
            // 
            this.penColortoolStripButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.penColortoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.penColortoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.penColortoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("penColortoolStripButton.Image")));
            this.penColortoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.penColortoolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.penColortoolStripButton.Name = "penColortoolStripButton";
            this.penColortoolStripButton.Size = new System.Drawing.Size(84, 28);
            this.penColortoolStripButton.Text = "Barva pera";
            this.penColortoolStripButton.Click += new System.EventHandler(this.penColortoolStripButton_Click);
            // 
            // penWidthtoolStripComboBox
            // 
            this.penWidthtoolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.penWidthtoolStripComboBox.Items.AddRange(new object[] {
            "Pero 0.5",
            "Pero 1",
            "Pero 1.5",
            "Pero 2",
            "Pero 3",
            "Pero 4",
            "Pero 5"});
            this.penWidthtoolStripComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.penWidthtoolStripComboBox.Name = "penWidthtoolStripComboBox";
            this.penWidthtoolStripComboBox.Size = new System.Drawing.Size(121, 28);
            this.penWidthtoolStripComboBox.ToolTipText = "Šířka pera";
            this.penWidthtoolStripComboBox.DropDownClosed += new System.EventHandler(this.penWidthtoolStripComboBox_DropDownClosed);
            // 
            // fillColorToolStripButton
            // 
            this.fillColorToolStripButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.fillColorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillColorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fillColorToolStripButton.Image")));
            this.fillColorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillColorToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.fillColorToolStripButton.Name = "fillColorToolStripButton";
            this.fillColorToolStripButton.Size = new System.Drawing.Size(97, 28);
            this.fillColorToolStripButton.Text = "Barva výplně";
            this.fillColorToolStripButton.Click += new System.EventHandler(this.fillColorToolStripButton_Click);
            // 
            // fillingOnOffToolStripComboBox
            // 
            this.fillingOnOffToolStripComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Filling On",
            "Filling Off"});
            this.fillingOnOffToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fillingOnOffToolStripComboBox.Items.AddRange(new object[] {
            "Vyplnit Vyp",
            "Vyplnit Zap"});
            this.fillingOnOffToolStripComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.fillingOnOffToolStripComboBox.Name = "fillingOnOffToolStripComboBox";
            this.fillingOnOffToolStripComboBox.Size = new System.Drawing.Size(121, 28);
            this.fillingOnOffToolStripComboBox.ToolTipText = "Filling zapnout/vypnout";
            this.fillingOnOffToolStripComboBox.DropDownClosed += new System.EventHandler(this.fillingOnOffToolStripComboBox_DropDownClosed);
            // 
            // closedToolStripComboBox
            // 
            this.closedToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.closedToolStripComboBox.Items.AddRange(new object[] {
            "Uzavřít Vyp",
            "Uzavřít Zap"});
            this.closedToolStripComboBox.Name = "closedToolStripComboBox";
            this.closedToolStripComboBox.Size = new System.Drawing.Size(121, 30);
            this.closedToolStripComboBox.DropDownClosed += new System.EventHandler(this.closedToolStripComboBox_DropDownClosed);
            // 
            // gridToolStripComboBox
            // 
            this.gridToolStripComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Grid OFF",
            "Grid 3",
            "Grid 5",
            "Grid 8",
            "Grid 10",
            "Grid 12",
            "Grid 15",
            "Grid 20"});
            this.gridToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gridToolStripComboBox.Items.AddRange(new object[] {
            "Mříž Vyp",
            "Mříž 1",
            "Mříž 5",
            "Mříž 10",
            "Mříž 25",
            "Mříž 50",
            "Mříž 100",
            "Mříž 250",
            "Mříž 500"});
            this.gridToolStripComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.gridToolStripComboBox.Name = "gridToolStripComboBox";
            this.gridToolStripComboBox.Size = new System.Drawing.Size(121, 28);
            this.gridToolStripComboBox.ToolTipText = "Mřížku zapnout/vypnout";
            this.gridToolStripComboBox.DropDownClosed += new System.EventHandler(this.gridToolStripComboBox_DropDownClosed);
            this.gridToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.gridToolStripComboBox_DropDownClosed);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(29, 27);
            this.toolStripLabel3.Text = "     ";
            // 
            // zoomInToolStripButton
            // 
            this.zoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInToolStripButton.Image = global::Zahrada.Properties.Resources.zoomout;
            this.zoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomInToolStripButton.Name = "zoomInToolStripButton";
            this.zoomInToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.zoomInToolStripButton.Size = new System.Drawing.Size(24, 28);
            this.zoomInToolStripButton.Text = "toolStripButton1";
            this.zoomInToolStripButton.Click += new System.EventHandler(this.zoomOUTtoolStripButton_Click);
            // 
            // zoomToolStripComboBox
            // 
            this.zoomToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoomToolStripComboBox.Items.AddRange(new object[] {
            "Zobrazení 0,5x",
            "Zobrazení 1x",
            "Zobrazení 2x",
            "Zobrazení 4x",
            "Zobrazení 8x",
            "Zobrazení 16x",
            "Zobrazení 32x",
            "Zobrazení 64x"});
            this.zoomToolStripComboBox.Margin = new System.Windows.Forms.Padding(1);
            this.zoomToolStripComboBox.MaxDropDownItems = 15;
            this.zoomToolStripComboBox.Name = "zoomToolStripComboBox";
            this.zoomToolStripComboBox.Size = new System.Drawing.Size(140, 28);
            this.zoomToolStripComboBox.ToolTipText = "Zoom in/out";
            this.zoomToolStripComboBox.DropDownClosed += new System.EventHandler(this.zoomToolStripComboBox_DropDownClosed);
            // 
            // zoomOutToolStripButton
            // 
            this.zoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutToolStripButton.Image = global::Zahrada.Properties.Resources.zoomin;
            this.zoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutToolStripButton.Margin = new System.Windows.Forms.Padding(1);
            this.zoomOutToolStripButton.Name = "zoomOutToolStripButton";
            this.zoomOutToolStripButton.Size = new System.Drawing.Size(24, 28);
            this.zoomOutToolStripButton.Text = "toolStripButton1";
            this.zoomOutToolStripButton.Click += new System.EventHandler(this.zoomINtoolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 547);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1327, 25);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(264, 20);
            this.toolStripStatusLabel.Text = "Začni kreslit zahradu nebo něco jiného";
            // 
            // vlozenyToolBox
            // 
            this.vlozenyToolBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vlozenyToolBox.AutoScroll = true;
            this.vlozenyToolBox.Location = new System.Drawing.Point(0, 62);
            this.vlozenyToolBox.Margin = new System.Windows.Forms.Padding(4);
            this.vlozenyToolBox.Name = "vlozenyToolBox";
            this.vlozenyToolBox.Size = new System.Drawing.Size(350, 473);
            this.vlozenyToolBox.TabIndex = 8;
            // 
            // vlozenePlatno
            // 
            this.vlozenePlatno.A4 = true;
            this.vlozenePlatno.AllowDrop = true;
            this.vlozenePlatno.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vlozenePlatno.AutoScroll = true;
            this.vlozenePlatno.BackColor = System.Drawing.Color.Moccasin;
            this.vlozenePlatno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vlozenePlatno.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.vlozenePlatno.dx = -172;
            this.vlozenePlatno.dy = 114;
            this.vlozenePlatno.gridSize = 0;
            this.vlozenePlatno.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.vlozenePlatno.Location = new System.Drawing.Point(356, 62);
            this.vlozenePlatno.Margin = new System.Windows.Forms.Padding(0);
            this.vlozenePlatno.Name = "vlozenePlatno";
            this.vlozenePlatno.ShowDebug = false;
            this.vlozenePlatno.Size = new System.Drawing.Size(962, 473);
            this.vlozenePlatno.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.vlozenePlatno.TabIndex = 9;
            this.vlozenePlatno.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.vlozenePlatno.Zoom = 2F;
            this.vlozenePlatno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vlozenePlatno_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "funkce.ico");
            this.imageList1.Images.SetKeyName(1, "vlastnosti.ico");
            this.imageList1.Images.SetKeyName(2, "pruvodce.ico");
            this.imageList1.Images.SetKeyName(3, "prani.ico");
            // 
            // HlavniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 572);
            this.Controls.Add(this.vlozenyToolBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.vlozenePlatno);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HlavniForm";
            this.Text = "Navrhování zahrad";
            this.Load += new System.EventHandler(this.hlavniFormularoveOkno_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToJpgpngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem moveToFrontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pencilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ColorDialog mujColorDialog;
        public UserControlNastroje vlozenyToolBox;
        public Platno vlozenePlatno;
        public System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripComboBox penWidthtoolStripComboBox;
        private System.Windows.Forms.ToolStripButton penColortoolStripButton;
        private System.Windows.Forms.ToolStripButton fillColorToolStripButton;
        public  System.Windows.Forms.ToolStripComboBox zoomToolStripComboBox;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox fillingOnOffToolStripComboBox;
        private System.Windows.Forms.ToolStripButton zoomOutToolStripButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton zoomInToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem nápovedaCelkovatoolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox closedToolStripComboBox;
        private System.Windows.Forms.ToolStripComboBox gridToolStripComboBox;
        private System.Windows.Forms.ToolStripButton undoToolStripButton;
        private System.Windows.Forms.ToolStripButton redoToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ImageList imageList1;
    }
}

