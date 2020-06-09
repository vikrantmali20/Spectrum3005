namespace Spectrum.BO
{
    partial class frmCommonSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonSearch));
            this.btnCancel = new Spectrum.Controls.Button(this.components);
            this.btnOK = new Spectrum.Controls.Button(this.components);
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gridData = new Spectrum.Controls.FlexGrid(this.components);
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(634, 433);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.MinimumSize = new System.Drawing.Size(20, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(729, 433);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.MinimumSize = new System.Drawing.Size(20, 28);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 3;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.mainPanel.Controls.Add(this.btnOK, 2, 1);
            this.mainPanel.Controls.Add(this.btnCancel, 1, 1);
            this.mainPanel.Controls.Add(this.gridData, 0, 0);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 2;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainPanel.Size = new System.Drawing.Size(820, 464);
            this.mainPanel.TabIndex = 4;
            // 
            // gridData
            // 
            this.gridData.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.gridData.ColumnInfo = "5,0,0,0,0,110,Columns:0{Width:65;Style:\"TextAlign:GeneralCenter;ImageAlign:Center" +
    "Center;\";}\t";
            this.mainPanel.SetColumnSpan(this.gridData, 3);
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.ExtendLastCol = true;
            this.gridData.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridData.Location = new System.Drawing.Point(3, 3);
            this.gridData.Name = "gridData";
            this.gridData.Rows.Count = 1;
            this.gridData.Rows.DefaultSize = 22;
            this.gridData.Size = new System.Drawing.Size(814, 423);
            this.gridData.StyleInfo = resources.GetString("gridData.StyleInfo");
            this.gridData.TabIndex = 3;
            this.gridData.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            this.gridData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridData_MouseDoubleClick);
            // 
            // frmCommonSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 464);
            this.Controls.Add(this.mainPanel);
            this.Name = "frmCommonSearch";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.frmCommonSearch_Load);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Button btnCancel;
        private Controls.Button btnOK;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private Controls.FlexGrid gridData;
    }
}