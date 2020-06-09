using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace Spectrum.BO
{
    public partial class MDISpectrumLite : C1.Win.C1Ribbon.C1RibbonForm
    {
        int childFormNumber;
        int gmdiclientheight;
        int gmdiclientwidth;
        
        public MDISpectrumLite()
        {
            InitializeComponent();
            RegisterEvents();
            this.BackgroundImage = Properties.Resources.main_bg;

        }

        private void RegisterEvents()
        {
            tenderMenuItem.Click += new EventHandler(tenderMenuItem_Click);
            taxMenuItem.Click += new EventHandler(taxMenuItem_Click);
            itemHierarchyPopupMenuItem.Click += new EventHandler(itemHierarchyPopupMenuItem_Click);
            itemMasterMenuItem.Click += new EventHandler(itemMasterMenuItem_Click);
            stockInMenuItem.Click += new EventHandler(stockInToolStripMenuItem_Click);
            stockOutMenuItem.Click += new EventHandler(stockOutMenuItem_Click);
            itemHierarchyMenuItem.Click += new EventHandler(itemHierarchyMenuItem_Click);
            userMenuItem.Click += new EventHandler(userMenuItem_Click);
            manualPromotionMenuItem.Click += new EventHandler(manualPromotionMenuItem_Click);
            siteDetailsMenuItem.Click += new EventHandler(siteDetailsMenuItem_Click);
            barcodeToolStripMenuItem.Click += new EventHandler(barcodeToolStripMenuItem_Click);
        }

        private void MDISpectrumLite_Load(object sender, EventArgs e)
        {

        }
        private void barcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var childForm = container.Resolve<frmTender>();
            var ChildForm = new frmBarcode();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }
        private void siteDetailsMenuItem_Click(object sender, EventArgs e)
        {
            //var childForm = container.Resolve<frmTender>();
            var ChildForm = new frmSiteDetails();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }
        private void userMenuItem_Click(object sender, EventArgs e)
        {
            //var childForm = container.Resolve<frmTender>();
            var ChildForm = new frmUser();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }
        private void manualPromotionMenuItem_Click(object sender, EventArgs e)
        {
            //var childForm = container.Resolve<frmTender>();
            var ChildForm = new frmManualPromotion();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }
        private void tenderMenuItem_Click(object sender, EventArgs e)
        {
            //var childForm = container.Resolve<frmTender>();
            var ChildForm = new frmTender();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void itemHierarchyPopupMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmItemHierarchyPopup();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void itemMasterMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmItemMaster();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void supplierMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmSupplier();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void taxMenuItem_Click(object sender, EventArgs e)
        {
             var ChildForm = new frmTax();

            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }

        }

        public void ShowChildForm(Controls.RibbonForm ChildForm, bool isdoctoparent = false)
        {

            gmdiclientheight = ChildForm.Height;
            gmdiclientwidth = ChildForm.Width;

            try
            {
                // Make it a child of this MDI form before showing it.
                foreach (Form frmS in MdiChildren)
                {
                    if (ChildForm.Name == frmS.Name)
                    {
                        return;
                    }
                }

                ChildForm.MdiParent = this;
                childFormNumber += 1;
                ChildForm.Text = ChildForm.Text;

                //lblTitle.Text = fnMakeTitle(, ChildForm.Text)

                if (isdoctoparent == true)
                {
                    ChildForm.Dock = DockStyle.Fill;
                    ChildForm.MainMenuStrip = MenuStrip;

                    //Rakesh-21.08.2013:Issue-7606-->Disappears title text
                    ChildForm.MaximizeBox = false;
                }
                else
                {
                    ChildForm.StartPosition = FormStartPosition.CenterScreen;
                }

                ChildForm.StartPosition = FormStartPosition.CenterScreen;
                if (ChildForm.Name != "frmNLogin")
                {
                    //Close all child forms of the parent.
                    foreach (Form vChildForm in this.MdiChildren)
                    {
                        if (ChildForm.Name != vChildForm.Name)
                        {
                            vChildForm.Close();
                            ChildForm.Dispose();
                        }
                    }
                }
                else
                {
                    ChildForm.StartPosition = FormStartPosition.CenterScreen;
                    this.MenuStrip.Show();
                }

                ChildForm.Select();
                try
                {
                    ChildForm.Show();
                }
                catch (Exception ex)
                {
                    ChildForm.Close();
                    ChildForm.Dispose();
                }

            }
            catch (Exception ex)
            {
                ChildForm.Close();
                ChildForm.Dispose();
            }

        }

        private void stockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmArticleStockIn();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void stockOutMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmArticleStockOut();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void characteristicsMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmCharacteristics();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, false);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void itemHierarchyMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmItemHierarchy();
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, true);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }


        private void exportArticleXLSReportMenuItem_Click(object sender, EventArgs e)
        {
              var ChildForm = new frmImportExportItem((int)CommonFunc.enumImportExportItem.ExportArticleXlsReport);
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, true);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }
  

        private void exportArticleHierarchyMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmImportExportItem((int)CommonFunc.enumImportExportItem.ExportArticleXlsReport);
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, true);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }

        private void uploadItemMenuItem_Click(object sender, EventArgs e)
        {
            var ChildForm = new frmImportExportItem((int)CommonFunc.enumImportExportItem.UploadItem);
            try
            {
                if (ChildForm.Name != string.Empty)
                {
                    ShowChildForm(ChildForm, true);
                }
            }
            catch (Exception ex)
            {
                ChildForm.Close();
            }
        }



    }
}
