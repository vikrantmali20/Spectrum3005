namespace Spectrum.BO
{
    partial class MDISpectrumLite
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.mastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplierMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHierarchyPopupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMasterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHierarchyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArticleXLSReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArticleHierarchyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characteristicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualPromotionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siteDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mastersToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(897, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip";
            // 
            // mastersToolStripMenuItem
            // 
            this.mastersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tenderMenuItem,
            this.supplierMenuItem,
            this.taxMenuItem,
            this.itemMasterToolStripMenuItem,
            this.stockInMenuItem,
            this.stockOutMenuItem,
            this.userMenuItem,
            this.characteristicsMenuItem,
            this.manualPromotionMenuItem,
            this.siteDetailsMenuItem,
            this.barcodeToolStripMenuItem});
            this.mastersToolStripMenuItem.Name = "mastersToolStripMenuItem";
            this.mastersToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.mastersToolStripMenuItem.Text = "Masters";
            // 
            // tenderMenuItem
            // 
            this.tenderMenuItem.Name = "tenderMenuItem";
            this.tenderMenuItem.Size = new System.Drawing.Size(174, 22);
            this.tenderMenuItem.Text = "Tender";
            // 
            // supplierMenuItem
            // 
            this.supplierMenuItem.Name = "supplierMenuItem";
            this.supplierMenuItem.Size = new System.Drawing.Size(174, 22);
            this.supplierMenuItem.Text = "Supplier";
            this.supplierMenuItem.Click += new System.EventHandler(this.supplierMenuItem_Click);
            // 
            // taxMenuItem
            // 
            this.taxMenuItem.Name = "taxMenuItem";
            this.taxMenuItem.Size = new System.Drawing.Size(174, 22);
            this.taxMenuItem.Text = "Tax";
            // 
            // itemMasterToolStripMenuItem
            // 
            this.itemMasterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemHierarchyPopupMenuItem,
            this.itemMasterMenuItem,
            this.itemHierarchyMenuItem,
            this.importExportToolStripMenuItem});
            this.itemMasterToolStripMenuItem.Name = "itemMasterToolStripMenuItem";
            this.itemMasterToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.itemMasterToolStripMenuItem.Text = "Item Master";
            // 
            // itemHierarchyPopupMenuItem
            // 
            this.itemHierarchyPopupMenuItem.Name = "itemHierarchyPopupMenuItem";
            this.itemHierarchyPopupMenuItem.Size = new System.Drawing.Size(190, 22);
            this.itemHierarchyPopupMenuItem.Text = "Item Hierarchy Popup";
            // 
            // itemMasterMenuItem
            // 
            this.itemMasterMenuItem.Name = "itemMasterMenuItem";
            this.itemMasterMenuItem.Size = new System.Drawing.Size(190, 22);
            this.itemMasterMenuItem.Text = "Item Master";
            // 
            // itemHierarchyMenuItem
            // 
            this.itemHierarchyMenuItem.Name = "itemHierarchyMenuItem";
            this.itemHierarchyMenuItem.Size = new System.Drawing.Size(190, 22);
            this.itemHierarchyMenuItem.Text = "Item Hierarchy";
            this.itemHierarchyMenuItem.Click += new System.EventHandler(this.itemHierarchyMenuItem_Click);
            // 
            // importExportToolStripMenuItem
            // 
            this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadItemMenuItem,
            this.exportArticleXLSReportMenuItem,
            this.exportArticleHierarchyMenuItem});
            this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
            this.importExportToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.importExportToolStripMenuItem.Text = "Import/Export";
            // 
            // uploadItemMenuItem
            // 
            this.uploadItemMenuItem.Name = "uploadItemMenuItem";
            this.uploadItemMenuItem.Size = new System.Drawing.Size(213, 22);
            this.uploadItemMenuItem.Text = "1. Upload Item";
            this.uploadItemMenuItem.Click += new System.EventHandler(this.uploadItemMenuItem_Click);
            // 
            // exportArticleXLSReportMenuItem
            // 
            this.exportArticleXLSReportMenuItem.Name = "exportArticleXLSReportMenuItem";
            this.exportArticleXLSReportMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exportArticleXLSReportMenuItem.Text = "2.Export Article XLS Report";
            this.exportArticleXLSReportMenuItem.Click += new System.EventHandler(this.exportArticleXLSReportMenuItem_Click);
            // 
            // exportArticleHierarchyMenuItem
            // 
            this.exportArticleHierarchyMenuItem.Name = "exportArticleHierarchyMenuItem";
            this.exportArticleHierarchyMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exportArticleHierarchyMenuItem.Text = "3. Export Article Hierarchy";
            this.exportArticleHierarchyMenuItem.Click += new System.EventHandler(this.exportArticleHierarchyMenuItem_Click);
            // 
            // stockInMenuItem
            // 
            this.stockInMenuItem.Name = "stockInMenuItem";
            this.stockInMenuItem.Size = new System.Drawing.Size(174, 22);
            this.stockInMenuItem.Text = "Stock In";
            // 
            // stockOutMenuItem
            // 
            this.stockOutMenuItem.Name = "stockOutMenuItem";
            this.stockOutMenuItem.Size = new System.Drawing.Size(174, 22);
            this.stockOutMenuItem.Text = "Stock Out";
            // 
            // userMenuItem
            // 
            this.userMenuItem.Name = "userMenuItem";
            this.userMenuItem.Size = new System.Drawing.Size(174, 22);
            this.userMenuItem.Text = "User";
            // 
            // characteristicsMenuItem
            // 
            this.characteristicsMenuItem.Name = "characteristicsMenuItem";
            this.characteristicsMenuItem.Size = new System.Drawing.Size(174, 22);
            this.characteristicsMenuItem.Text = "Characteristics";
            this.characteristicsMenuItem.Click += new System.EventHandler(this.characteristicsMenuItem_Click);
            // 
            // manualPromotionMenuItem
            // 
            this.manualPromotionMenuItem.Name = "manualPromotionMenuItem";
            this.manualPromotionMenuItem.Size = new System.Drawing.Size(174, 22);
            this.manualPromotionMenuItem.Text = "Manual Promotion";
            // 
            // siteDetailsMenuItem
            // 
            this.siteDetailsMenuItem.Name = "siteDetailsMenuItem";
            this.siteDetailsMenuItem.Size = new System.Drawing.Size(174, 22);
            this.siteDetailsMenuItem.Text = "Site Details";
            // 
            // barcodeToolStripMenuItem
            // 
            this.barcodeToolStripMenuItem.Name = "barcodeToolStripMenuItem";
            this.barcodeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.barcodeToolStripMenuItem.Text = "Barcode Print";
            // 
            // MDISpectrumLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 399);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MDISpectrumLite";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpectrumLite";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDISpectrumLite_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.ToolStripMenuItem mastersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supplierMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemHierarchyPopupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemMasterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characteristicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemHierarchyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualPromotionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem siteDetailsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportArticleXLSReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportArticleHierarchyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barcodeToolStripMenuItem;
    }
}