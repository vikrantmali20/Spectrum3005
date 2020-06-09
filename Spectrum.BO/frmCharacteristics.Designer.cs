namespace Spectrum.BO
{
    partial class frmCharacteristics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCharacteristics));
            this.grdSize = new Spectrum.Controls.FlexGrid(this.components);
            this.grdStyle = new Spectrum.Controls.FlexGrid(this.components);
            this.grdFabric = new Spectrum.Controls.FlexGrid(this.components);
            this.grdColour = new Spectrum.Controls.FlexGrid(this.components);
            this.btnDeleteSize = new Spectrum.Controls.Button(this.components);
            this.chkSelectSize = new System.Windows.Forms.CheckBox();
            this.btnDeleteStyle = new Spectrum.Controls.Button(this.components);
            this.chkSelectStyle = new System.Windows.Forms.CheckBox();
            this.chkSelectColour = new System.Windows.Forms.CheckBox();
            this.btnDeleteColour = new Spectrum.Controls.Button(this.components);
            this.chkSelectFabric = new System.Windows.Forms.CheckBox();
            this.btnDeleteFabric = new Spectrum.Controls.Button(this.components);
            this.btnSave = new Spectrum.Controls.Button(this.components);
            this.btnCancel = new Spectrum.Controls.Button(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFabric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdColour)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSize
            // 
            this.grdSize.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.grdSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSize.CellButtonImage = ((System.Drawing.Image)(resources.GetObject("grdSize.CellButtonImage")));
            this.grdSize.ColumnInfo = resources.GetString("grdSize.ColumnInfo");
            this.grdSize.ExtendLastCol = true;
            this.grdSize.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSize.Location = new System.Drawing.Point(18, 9);
            this.grdSize.Name = "grdSize";
            this.grdSize.Rows.Count = 6;
            this.grdSize.Rows.DefaultSize = 22;
            this.grdSize.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSize.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.grdSize.Size = new System.Drawing.Size(355, 225);
            this.grdSize.StyleInfo = resources.GetString("grdSize.StyleInfo");
            this.grdSize.TabIndex = 0;
            this.grdSize.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            // 
            // grdStyle
            // 
            this.grdStyle.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.grdStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdStyle.CellButtonImage = ((System.Drawing.Image)(resources.GetObject("grdStyle.CellButtonImage")));
            this.grdStyle.ColumnInfo = "2,0,0,0,0,110,Columns:0{Width:40;Name:\"Select\";Style:\"DataType:System.Boolean;Tex" +
    "tAlign:LeftCenter;ImageAlign:CenterCenter;\";}\t1{Width:265;Name:\"Style\";Caption:\"" +
    "Style\";}\t";
            this.grdStyle.ExtendLastCol = true;
            this.grdStyle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdStyle.Location = new System.Drawing.Point(429, 9);
            this.grdStyle.Name = "grdStyle";
            this.grdStyle.Rows.Count = 6;
            this.grdStyle.Rows.DefaultSize = 22;
            this.grdStyle.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdStyle.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.grdStyle.Size = new System.Drawing.Size(355, 225);
            this.grdStyle.StyleInfo = resources.GetString("grdStyle.StyleInfo");
            this.grdStyle.TabIndex = 1;
            this.grdStyle.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            // 
            // grdFabric
            // 
            this.grdFabric.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.grdFabric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFabric.CellButtonImage = ((System.Drawing.Image)(resources.GetObject("grdFabric.CellButtonImage")));
            this.grdFabric.ColumnInfo = resources.GetString("grdFabric.ColumnInfo");
            this.grdFabric.ExtendLastCol = true;
            this.grdFabric.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFabric.Location = new System.Drawing.Point(18, 245);
            this.grdFabric.Name = "grdFabric";
            this.grdFabric.Rows.Count = 6;
            this.grdFabric.Rows.DefaultSize = 22;
            this.grdFabric.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdFabric.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.grdFabric.Size = new System.Drawing.Size(355, 225);
            this.grdFabric.StyleInfo = resources.GetString("grdFabric.StyleInfo");
            this.grdFabric.TabIndex = 2;
            this.grdFabric.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            // 
            // grdColour
            // 
            this.grdColour.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.grdColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdColour.CellButtonImage = ((System.Drawing.Image)(resources.GetObject("grdColour.CellButtonImage")));
            this.grdColour.ColumnInfo = resources.GetString("grdColour.ColumnInfo");
            this.grdColour.ExtendLastCol = true;
            this.grdColour.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdColour.Location = new System.Drawing.Point(429, 245);
            this.grdColour.Name = "grdColour";
            this.grdColour.Rows.Count = 6;
            this.grdColour.Rows.DefaultSize = 22;
            this.grdColour.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdColour.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdColour.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.grdColour.Size = new System.Drawing.Size(355, 225);
            this.grdColour.StyleInfo = resources.GetString("grdColour.StyleInfo");
            this.grdColour.TabIndex = 3;
            this.grdColour.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.grdColour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdColour_KeyDown);
            // 
            // btnDeleteSize
            // 
            this.btnDeleteSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSize.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSize.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSize.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteSize.Image = global::Spectrum.BO.Properties.Resources.del_icon;
            this.btnDeleteSize.Location = new System.Drawing.Point(39, 13);
            this.btnDeleteSize.MaximumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteSize.MinimumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteSize.Name = "btnDeleteSize";
            this.btnDeleteSize.Size = new System.Drawing.Size(15, 15);
            this.btnDeleteSize.TabIndex = 97;
            this.btnDeleteSize.UseVisualStyleBackColor = false;
            this.btnDeleteSize.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSelectSize
            // 
            this.chkSelectSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectSize.AutoSize = true;
            this.chkSelectSize.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSelectSize.Location = new System.Drawing.Point(24, 14);
            this.chkSelectSize.Name = "chkSelectSize";
            this.chkSelectSize.Size = new System.Drawing.Size(12, 11);
            this.chkSelectSize.TabIndex = 98;
            this.chkSelectSize.UseVisualStyleBackColor = false;
            // 
            // btnDeleteStyle
            // 
            this.btnDeleteStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteStyle.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteStyle.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteStyle.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteStyle.Image = global::Spectrum.BO.Properties.Resources.del_icon;
            this.btnDeleteStyle.Location = new System.Drawing.Point(448, 14);
            this.btnDeleteStyle.MaximumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteStyle.MinimumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteStyle.Name = "btnDeleteStyle";
            this.btnDeleteStyle.Size = new System.Drawing.Size(15, 15);
            this.btnDeleteStyle.TabIndex = 99;
            this.btnDeleteStyle.UseVisualStyleBackColor = false;
            this.btnDeleteStyle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSelectStyle
            // 
            this.chkSelectStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectStyle.AutoSize = true;
            this.chkSelectStyle.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSelectStyle.Location = new System.Drawing.Point(433, 15);
            this.chkSelectStyle.Name = "chkSelectStyle";
            this.chkSelectStyle.Size = new System.Drawing.Size(12, 11);
            this.chkSelectStyle.TabIndex = 100;
            this.chkSelectStyle.UseVisualStyleBackColor = false;
            // 
            // chkSelectColour
            // 
            this.chkSelectColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectColour.AutoSize = true;
            this.chkSelectColour.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSelectColour.Location = new System.Drawing.Point(435, 251);
            this.chkSelectColour.Name = "chkSelectColour";
            this.chkSelectColour.Size = new System.Drawing.Size(12, 11);
            this.chkSelectColour.TabIndex = 101;
            this.chkSelectColour.UseVisualStyleBackColor = false;
            // 
            // btnDeleteColour
            // 
            this.btnDeleteColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteColour.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteColour.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteColour.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteColour.Image = global::Spectrum.BO.Properties.Resources.del_icon;
            this.btnDeleteColour.Location = new System.Drawing.Point(450, 249);
            this.btnDeleteColour.MaximumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteColour.MinimumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteColour.Name = "btnDeleteColour";
            this.btnDeleteColour.Size = new System.Drawing.Size(15, 15);
            this.btnDeleteColour.TabIndex = 102;
            this.btnDeleteColour.UseVisualStyleBackColor = false;
            this.btnDeleteColour.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSelectFabric
            // 
            this.chkSelectFabric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectFabric.AutoSize = true;
            this.chkSelectFabric.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectFabric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSelectFabric.Location = new System.Drawing.Point(24, 252);
            this.chkSelectFabric.Name = "chkSelectFabric";
            this.chkSelectFabric.Size = new System.Drawing.Size(12, 11);
            this.chkSelectFabric.TabIndex = 103;
            this.chkSelectFabric.UseVisualStyleBackColor = false;
            // 
            // btnDeleteFabric
            // 
            this.btnDeleteFabric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFabric.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteFabric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteFabric.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFabric.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteFabric.Image = global::Spectrum.BO.Properties.Resources.del_icon;
            this.btnDeleteFabric.Location = new System.Drawing.Point(39, 251);
            this.btnDeleteFabric.MaximumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteFabric.MinimumSize = new System.Drawing.Size(15, 15);
            this.btnDeleteFabric.Name = "btnDeleteFabric";
            this.btnDeleteFabric.Size = new System.Drawing.Size(15, 15);
            this.btnDeleteFabric.TabIndex = 104;
            this.btnDeleteFabric.UseVisualStyleBackColor = false;
            this.btnDeleteFabric.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(547, 481);
            this.btnSave.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 26);
            this.btnSave.TabIndex = 105;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(661, 481);
            this.btnCancel.MinimumSize = new System.Drawing.Size(15, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 26);
            this.btnCancel.TabIndex = 106;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCharacteristics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 512);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDeleteFabric);
            this.Controls.Add(this.chkSelectFabric);
            this.Controls.Add(this.btnDeleteColour);
            this.Controls.Add(this.chkSelectColour);
            this.Controls.Add(this.chkSelectStyle);
            this.Controls.Add(this.btnDeleteStyle);
            this.Controls.Add(this.chkSelectSize);
            this.Controls.Add(this.btnDeleteSize);
            this.Controls.Add(this.grdColour);
            this.Controls.Add(this.grdFabric);
            this.Controls.Add(this.grdStyle);
            this.Controls.Add(this.grdSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "frmCharacteristics";
            this.Text = "Characteristics";
            this.Load += new System.EventHandler(this.frmCharacteristics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFabric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdColour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.FlexGrid grdSize;
        private Controls.FlexGrid grdStyle;
        private Controls.FlexGrid grdFabric;
        private Controls.FlexGrid grdColour;
        private Controls.Button btnDeleteSize;
        private System.Windows.Forms.CheckBox chkSelectSize;
        private Controls.Button btnDeleteStyle;
        private System.Windows.Forms.CheckBox chkSelectStyle;
        private System.Windows.Forms.CheckBox chkSelectColour;
        private Controls.Button btnDeleteColour;
        private System.Windows.Forms.CheckBox chkSelectFabric;
        private Controls.Button btnDeleteFabric;
        private Controls.Button btnSave;
        private Controls.Button btnCancel;
    }
}