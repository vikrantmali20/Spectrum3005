namespace Spectrum.BO
{
    partial class frmCommonSearchTrueGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonSearchTrueGrid));
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gridData = new Spectrum.Controls.TrueGrid(this.components);
            this.btnOK = new Spectrum.Controls.Button(this.components);
            this.btnCancel = new Spectrum.Controls.Button(this.components);
            this.btnWildSearch = new Spectrum.Controls.Button(this.components);
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.txtwildsearch = new Spectrum.Controls.TextBox(this.components);
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwildsearch)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 4;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainPanel.Controls.Add(this.gridData, 0, 1);
            this.mainPanel.Controls.Add(this.btnOK, 3, 2);
            this.mainPanel.Controls.Add(this.btnCancel, 2, 2);
            this.mainPanel.Controls.Add(this.btnWildSearch, 3, 0);
            this.mainPanel.Controls.Add(this.chkSelectAll, 0, 0);
            this.mainPanel.Controls.Add(this.txtwildsearch, 1, 0);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 3;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainPanel.Size = new System.Drawing.Size(987, 525);
            this.mainPanel.TabIndex = 5;
            // 
            // gridData
            // 
            this.gridData.AllowUpdateOnBlur = false;
            this.gridData.CaptionHeight = 17;
            this.mainPanel.SetColumnSpan(this.gridData, 4);
            this.gridData.FilterBar = true;
            this.gridData.GroupByCaption = "Drag a column header here to group by that column";
            this.gridData.Images.Add(((System.Drawing.Image)(resources.GetObject("gridData.Images"))));
            this.gridData.Location = new System.Drawing.Point(3, 35);
            this.gridData.Name = "gridData";
            this.gridData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridData.PreviewInfo.ZoomFactor = 75D;
            this.gridData.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridData.PrintInfo.PageSettings")));
            this.gridData.RowHeight = 15;
            this.gridData.ScrollTips = true;
            this.gridData.Size = new System.Drawing.Size(981, 454);
            this.gridData.TabIndex = 3;
            this.gridData.Text = "trueGrid1";
            this.gridData.HeadClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridData_HeadClick);
            this.gridData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridData_MouseDoubleClick);
            this.gridData.PropBag = resources.GetString("gridData.PropBag");
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(890, 496);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.MinimumSize = new System.Drawing.Size(20, 28);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(792, 496);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.MinimumSize = new System.Drawing.Size(20, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnWildSearch
            // 
            this.btnWildSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWildSearch.ForeColor = System.Drawing.Color.Black;
            this.btnWildSearch.Location = new System.Drawing.Point(890, 4);
            this.btnWildSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnWildSearch.MinimumSize = new System.Drawing.Size(20, 28);
            this.btnWildSearch.Name = "btnWildSearch";
            this.btnWildSearch.Size = new System.Drawing.Size(71, 28);
            this.btnWildSearch.TabIndex = 4;
            this.btnWildSearch.Text = "&Search";
            this.btnWildSearch.UseVisualStyleBackColor = true;
            this.btnWildSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnWildSearch.Click += new System.EventHandler(this.btnWildSearch_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(3, 3);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(89, 20);
            this.chkSelectAll.TabIndex = 6;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // txtwildsearch
            // 
            this.txtwildsearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.txtwildsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.SetColumnSpan(this.txtwildsearch, 2);
            this.txtwildsearch.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.txtwildsearch.Location = new System.Drawing.Point(101, 3);
            this.txtwildsearch.Name = "txtwildsearch";
            this.txtwildsearch.Size = new System.Drawing.Size(765, 21);
            this.txtwildsearch.TabIndex = 5;
            this.txtwildsearch.Tag = null;
            this.txtwildsearch.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtwildsearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtwildsearch.TextChanged += new System.EventHandler(this.txtwildsearch_TextChanged);
            this.txtwildsearch.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtwildsearch_PreviewKeyDown);
            // 
            // frmCommonSearchTrueGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 525);
            this.Controls.Add(this.mainPanel);
            this.Name = "frmCommonSearchTrueGrid";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.frmCommonSearch2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCommonSearchTrueGrid_KeyDown);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwildsearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private Controls.Button btnOK;
        private Controls.Button btnCancel;
        private Controls.TrueGrid gridData;
        private Controls.TextBox txtwildsearch;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private Controls.Button btnWildSearch;
    }
}