namespace Zahrada
{
    partial class RozmerPlanuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RozmerPlanuForm));
            this.label1 = new System.Windows.Forms.Label();
            this.XdimensionTextBox = new System.Windows.Forms.TextBox();
            this.toolTipProVlastniRozmerPlanu = new System.Windows.Forms.ToolTip(this.components);
            this.YdimensionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOKrozmer = new System.Windows.Forms.Button();
            this.buttonStornoRozmer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(30, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Šířka plánu v metrech:";
            // 
            // XdimensionTextBox
            // 
            this.XdimensionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XdimensionTextBox.Location = new System.Drawing.Point(203, 33);
            this.XdimensionTextBox.Name = "XdimensionTextBox";
            this.XdimensionTextBox.Size = new System.Drawing.Size(122, 24);
            this.XdimensionTextBox.TabIndex = 1;
            this.toolTipProVlastniRozmerPlanu.SetToolTip(this.XdimensionTextBox, "Zadej šířku zahrady v metrech");
            // 
            // YdimensionTextBox
            // 
            this.YdimensionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.YdimensionTextBox.Location = new System.Drawing.Point(203, 68);
            this.YdimensionTextBox.Name = "YdimensionTextBox";
            this.YdimensionTextBox.Size = new System.Drawing.Size(122, 24);
            this.YdimensionTextBox.TabIndex = 3;
            this.toolTipProVlastniRozmerPlanu.SetToolTip(this.YdimensionTextBox, "Zadej výšku zahrady v metrech");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(30, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Výška plánu v metrech:";
            // 
            // buttonOKrozmer
            // 
            this.buttonOKrozmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOKrozmer.Location = new System.Drawing.Point(143, 127);
            this.buttonOKrozmer.Name = "buttonOKrozmer";
            this.buttonOKrozmer.Size = new System.Drawing.Size(80, 35);
            this.buttonOKrozmer.TabIndex = 4;
            this.buttonOKrozmer.Text = "OK";
            this.buttonOKrozmer.UseVisualStyleBackColor = true;
            this.buttonOKrozmer.Click += new System.EventHandler(this.buttonOKrozmer_Click);
            // 
            // buttonStornoRozmer
            // 
            this.buttonStornoRozmer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStornoRozmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStornoRozmer.Location = new System.Drawing.Point(245, 127);
            this.buttonStornoRozmer.Name = "buttonStornoRozmer";
            this.buttonStornoRozmer.Size = new System.Drawing.Size(80, 35);
            this.buttonStornoRozmer.TabIndex = 5;
            this.buttonStornoRozmer.Text = "Storno";
            this.buttonStornoRozmer.UseVisualStyleBackColor = true;
            this.buttonStornoRozmer.Click += new System.EventHandler(this.buttonStornoRozmer_Click);
            // 
            // RozmerPlanuForm
            // 
            this.AcceptButton = this.buttonOKrozmer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonStornoRozmer;
            this.ClientSize = new System.Drawing.Size(370, 186);
            this.Controls.Add(this.buttonStornoRozmer);
            this.Controls.Add(this.buttonOKrozmer);
            this.Controls.Add(this.YdimensionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.XdimensionTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(388, 233);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(388, 233);
            this.Name = "RozmerPlanuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Vlastní rozměr plánu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTipProVlastniRozmerPlanu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOKrozmer;
        private System.Windows.Forms.Button buttonStornoRozmer;
        public System.Windows.Forms.TextBox XdimensionTextBox;
        public System.Windows.Forms.TextBox YdimensionTextBox;
    }
}