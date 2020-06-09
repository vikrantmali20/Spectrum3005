namespace Spectrum.BO
{
    partial class frmShiftManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShiftManagement));
            this.lblSiteCode = new System.Windows.Forms.Label();
            this.cmbSiteCode = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grdShift = new Spectrum.Controls.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdShift)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSiteCode
            // 
            this.lblSiteCode.Location = new System.Drawing.Point(25, 18);
            this.lblSiteCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSiteCode.Name = "lblSiteCode";
            this.lblSiteCode.Size = new System.Drawing.Size(75, 24);
            this.lblSiteCode.TabIndex = 3;
            this.lblSiteCode.Text = "Site Name";
            // 
            // cmbSiteCode
            // 
            this.cmbSiteCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteCode.FormattingEnabled = true;
            this.cmbSiteCode.Location = new System.Drawing.Point(108, 15);
            this.cmbSiteCode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSiteCode.Name = "cmbSiteCode";
            this.cmbSiteCode.Size = new System.Drawing.Size(207, 24);
            this.cmbSiteCode.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(524, 461);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(632, 461);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(416, 461);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 28);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grdShift
            // 
            this.grdShift.AllowEditing = false;
            this.grdShift.ColumnInfo = "5,0,0,0,0,110,Columns:0{Width:81;Style:\"TextAlign:GeneralCenter;ImageAlign:Center" +
    "Center;\";}\t1{Width:142;}\t2{Width:136;}\t3{Width:174;}\t4{Width:175;}\t";
            this.grdShift.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdShift.Location = new System.Drawing.Point(25, 53);
            this.grdShift.Margin = new System.Windows.Forms.Padding(4);
            this.grdShift.Name = "grdShift";
            this.grdShift.Rows.Count = 1;
            this.grdShift.Rows.DefaultSize = 22;
            this.grdShift.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.grdShift.Size = new System.Drawing.Size(712, 400);
            this.grdShift.StyleInfo = resources.GetString("grdShift.StyleInfo");
            this.grdShift.TabIndex = 10;
            this.grdShift.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.grdShift.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.grdShift_CellChecked);
            // 
            // frmShiftManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(761, 502);
            this.Controls.Add(this.grdShift);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbSiteCode);
            this.Controls.Add(this.lblSiteCode);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmShiftManagement";
            this.Text = "Shift Management";
            this.Load += new System.EventHandler(this.frmShiftManagement_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmShiftManagement_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdShift)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSiteCode;
        internal System.Windows.Forms.ComboBox cmbSiteCode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private Controls.FlexGrid grdShift;
    }
}