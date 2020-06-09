using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Spectrum.BO
{
    public partial class frmBarcode : Spectrum.Controls.RibbonForm
    {
        public frmBarcode()
        {
            InitializeComponent();
            this.commonManager = new CommonManager();
        }

        #region "Class Variables "

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        ICommonManager commonManager;
        IQueryable<MasterTypeModel> masterTypeList;
        enum enumBarcode
        {
            ItemCode,
            EAN,
            ItemDescription,
            BarcodeType,
            NoOfPrints,
            Exclude
        }
        string NodeCode = "";

        #endregion

        #region "Functions"

        private void setTabIndex()
        {
            try
            {
                rdoItemHierchy.TabStop = false;
                cboBarCodeType.TabIndex = 1;
                txtLevel.TabIndex = 2;
                txtNoOfPts.TabIndex = 3;
                btnApplyAll.TabIndex = 4;
                btnExport.TabIndex = 5;
                btnGetData.TabIndex = 6;
                btnCancel.TabIndex = 7;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsFormValidate()
        {
            try
            {
                bool isValid = true;
                Regex alphanumericpatt = new Regex("^[0-9]+$");
                if (string.IsNullOrEmpty(txtLevel.Text.Trim()))
                {
                    CommonFunc.ShowMessage("Please select level first", MessageType.Information);
                    isValid = false;
                }
                if (!string.IsNullOrEmpty(txtNoOfPts.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtNoOfPts.Text.Trim ()) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtNoOfPts, "Allows Numbers only", false))
                        {
                            this.txtNoOfPts.Focus();
                        }

                    }
                    else if (Convert.ToInt32(txtNoOfPts.Text.Trim()) <= 0)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtNoOfPts, "Value should be grater than 0 ", false))
                        {
                            this.txtNoOfPts.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtNoOfPts, string.Empty);
                        txtNoOfPts.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void fillBarCode()
        {
            try
            {
                masterTypeList = commonManager.GetMasterTypeList();
                var barCodeModesList = (from m in masterTypeList
                                        where m.CodeType == "MSTEanType"
                                        select new DropDownModel { Code = m.ShortDesc, Description = m.ShortDesc }).ToList();

                barCodeModesList.Insert(0, new DropDownModel { Code = null, Description = "All" });
                //cboBarCodeType.DataSource = barCodeModesList;
                //cboBarCodeType.DisplayMember = "Description";
                //cboBarCodeType.ValueMember = "Code";

                 CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboBarCodeType, barCodeModesList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setInitialFormsValidations()
        {
            try
            {
                rdoDocument.Visible = false;
                rdoItemHierchy.Visible = true;
                rdoItemHierchy.Checked = true;
                pnlHierchy.Visible = true;
                // pnlDoc.Visible = false;
                btnExport.Visible = false;
                //btnPrint.Visible = false;
                //lblNoOfPts.Visible = false;
                //btnApplyAll.Visible = false;
                //txtNoOfPts.Visible = false;
                txtNoOfPts.DataType = typeof(Int64);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DefaultGridSetting()
        {
            try
            {

                gridBarcode.Cols["ItemCode"].Width = 150;
                gridBarcode.Cols["TaxCode"].AllowEditing = false;
                gridBarcode.Cols["TaxCode"].AllowResizing = false;
                gridBarcode.Cols["EAN"].Width = 250;
                gridBarcode.Cols["EAN"].AllowEditing = false;
                gridBarcode.Cols["EAN"].AllowResizing = false;
                gridBarcode.Cols["ItemDescription"].Width = 200;
                gridBarcode.Cols["ItemDescription"].AllowEditing = false;
                gridBarcode.Cols["ItemDescription"].AllowResizing = false;
                gridBarcode.Cols["BarcodeType"].Width = 135;
                gridBarcode.Cols["BarcodeType"].AllowEditing = false;
                gridBarcode.Cols["BarcodeType"].AllowResizing = false;
                gridBarcode.Cols["NoOfPoints"].Width = 100;
                gridBarcode.Cols["NoOfPoints"].AllowEditing = false;
                gridBarcode.Cols["NoOfPoints"].AllowResizing = false;
                gridBarcode.Cols["Exclude"].Width = 135;
                gridBarcode.Cols["Exclude"].AllowEditing = true;
                gridBarcode.Cols["Exclude"].AllowResizing = false;


            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            groupBox1.BackColor = Color.FromArgb(134, 134, 134);
            groupBox2.BackColor = Color.FromArgb(134, 134, 134);
            groupBox3.BackColor = Color.FromArgb(134, 134, 134);

            groupBox1.ForeColor = Color.FromArgb(255, 255, 255);
            groupBox2.ForeColor = Color.FromArgb(255, 255, 255);
            groupBox3.ForeColor = Color.FromArgb(255, 255, 255);

            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);

            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnExport.BackColor = Color.Transparent;
            btnExport.BackColor = Color.FromArgb(0, 107, 163);
            btnExport.ForeColor = Color.FromArgb(255, 255, 255);
            btnExport.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnGetData.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnGetData.BackColor = Color.Transparent;
            btnGetData.BackColor = Color.FromArgb(0, 107, 163);
            btnGetData.ForeColor = Color.FromArgb(255, 255, 255);
            btnGetData.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnGetData.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnGetData.FlatAppearance.BorderSize = 0;
            btnGetData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnApplyAll.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnApplyAll.BackColor = Color.Transparent;
            btnApplyAll.BackColor = Color.FromArgb(0, 107, 163);
            btnApplyAll.ForeColor = Color.FromArgb(255, 255, 255);
            btnApplyAll.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnApplyAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnApplyAll.FlatAppearance.BorderSize = 0;
            btnApplyAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            gridBarcode.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            gridBarcode.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            gridBarcode.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            gridBarcode.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            gridBarcode.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridBarcode.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridBarcode.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridBarcode.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridBarcode.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            gridBarcode.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            gridBarcode.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
            lblBarcodeType.BackColor = Color.FromArgb(212, 212, 212);
            lblItemBy.BackColor = Color.FromArgb(212, 212, 212);
            lblLevel.BackColor = Color.FromArgb(212, 212, 212);
            lblNoOfPts.BackColor = Color.FromArgb(212, 212, 212);

            pnlHierchy.BackColor = Color.FromArgb(134, 134, 134);
            panel2.BackColor = Color.FromArgb(134, 134, 134);

        }
        #endregion

        #region "Events"

        #region " Other Events "

        private void frmBarcode_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                setInitialFormsValidations();
                fillBarCode();
                // CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);
                setTabIndex();
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
            finally
            {

            }
        }

         
        private void chkSelectAll_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                for (int rowArticleCode = 1; rowArticleCode < gridBarcode.Rows.Count; rowArticleCode++)
                    gridBarcode.Rows[rowArticleCode][(int)enumBarcode.Exclude] = chkSelectAll.Checked;
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }
        private void rdoItemHierchy_Click(object sender, EventArgs e)
        {
            pnlHierchy.Visible = true;
            // pnlDoc.Visible = false;
        }

        private void rdoDocument_Click(object sender, EventArgs e)
        {
            pnlHierchy.Visible = false;
            // pnlDoc.Visible = true;
        }

        private void txtLevel_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (frmItemHierarchyPopup objItemHierarchyPopup = new frmItemHierarchyPopup())
                {
                    if (objItemHierarchyPopup.ShowDialog() == DialogResult.OK)
                    {
                        NodeCode = "";
                        var _selectedItemNode = objItemHierarchyPopup.selectedItemNode;
                        if (_selectedItemNode[0] != null)
                        {
                            if (_selectedItemNode[0].NodeName != null)
                            {
                                txtLevel.Text = _selectedItemNode[0].NodeName;
                                NodeCode = _selectedItemNode[0].Nodecode;
                            }
                        }
                        else
                            txtLevel.Text = "";
                    }
                    objItemHierarchyPopup.Dispose();
                }

            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }

        #endregion

        #region " Button Events "

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormValidate())
                {
                    int gridCount = gridBarcode.Rows.Count;
                    if (gridCount > 1)
                    {
                        if (pnlHierchy.Visible == true && gridBarcode.Rows.Count > 1)
                        {
                            for (int i = 1; i <= gridBarcode.Rows.Count - 1; i++)
                            {
                                gridBarcode.Rows[i][(int)enumBarcode.NoOfPrints] = Convert.ToInt32(txtNoOfPts.Text.ToString());
                            }
                        }
                    }
                    else
                        CommonFunc.ShowMessage("Data does Not Exist", MessageType.Information);
                }
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ArticleManager articleHierarchyManager = new ArticleManager();
                ArticleDataExportModel articleDataExportModel = articleHierarchyManager.GetArticlesForBarcodeExportData(NodeCode, CommonModel.SiteCode);
                if (cboBarCodeType.SelectedValue != null)
                {
                   // articleDataExportModel.ArticleDetails = articleDataExportModel.ArticleDetails.Where(x => x.DISCRIPTION == cboBarCodeType.SelectedValue.ToString()).ToList();
                    articleDataExportModel.ArticleDetails = articleDataExportModel.ArticleDetails.Where(x => x.BarcodeType == cboBarCodeType.SelectedValue.ToString()).ToList();  //vipin
                }
                var barCodeList = (from result in articleDataExportModel.ArticleDetails
                                   select new BarcodeModelExcel
                                   {
                                       ArticleName = result.ArticleName,
                                       ArticleCode = result.ArticleCode,
                                      // BARCODE = result.EAN,
                                      BARCODE = result.Barcode,   // added by vipin
                                       SellingPrice = result.SellingPrice,
                                       MRP = result.MRP,
                                       UOM = result.BaseUoM,
                                       NetWeight = result.NetWeight,
                                       ExpiryDate = ""
                                   }).ToList();
                DataTable dtExport = CommonFunc.ConvertListToDataTable(barCodeList);

                dtExport.Columns[0].ColumnName = "Article Name";
                dtExport.Columns[1].ColumnName = "Article Code";
                dtExport.Columns[2].ColumnName = "BARCODE";
                dtExport.Columns[3].ColumnName = "Selling Price";
                dtExport.Columns[4].ColumnName = "MRP";
                dtExport.Columns[5].ColumnName = "UOM";
                dtExport.Columns[6].ColumnName = "Net Weight";
                dtExport.Columns[7].ColumnName = "Expiry Date";
                dtExport.Columns.Add("IsExclude");

                var deleteArticleCodeRow = new List<ItemExclude>();
                for (int i = 1; i < gridBarcode.Rows.Count - 1; i++)
                {
                    ItemExclude itemExclude = new ItemExclude();
                    if (gridBarcode.Rows[i][(int)enumBarcode.Exclude].ToString().ToLower() == "true")
                    {
                        itemExclude.ArticleCode = gridBarcode.Rows[i][(int)enumBarcode.ItemCode].ToString();
                        deleteArticleCodeRow.Add(itemExclude);
                    }

                }


                foreach (DataRow dr in dtExport.Rows)
                {
                    foreach (var item in deleteArticleCodeRow)
                    {
                        if (dr["Article Code"].ToString() == item.ArticleCode)
                        {
                            dr["IsExclude"] = "Exclude";
                            break;
                        }
                        else
                            dr["IsExclude"] = "Include";
                    }

                }
                DataView dv = new DataView(dtExport);
                dv.RowFilter = "IsExclude = 'Include'";
                DataTable dt = new DataTable();
                dt = dv.ToTable();
                dt.Columns.Remove("IsExclude");
                Cursor.Current = Cursors.WaitCursor;
                DialogResult resultShow = fbd.ShowDialog();
                if (resultShow == DialogResult.OK)
                {
                    string path = Path.Combine(fbd.SelectedPath, "ArticelHierarchy" + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString();
                    bool IsExported = ConvertListToExcel.DatatableToExcel(dt, path);
                    System.Diagnostics.Process.Start(path);
                    if (IsExported)
                        MessageBox.Show("Exported Successfully");
                }
                Cursor.Current = Cursors.Default;

            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {                
                if (IsFormValidate())
                {
                    int gridCount = gridBarcode.Rows.Count;
                    if (gridCount > 1)
                        gridBarcode.Rows.RemoveRange(1, gridBarcode.Rows.Count - 1);
                    ArticleManager articleHierarchyManager = new ArticleManager();
                    ArticleDataExportModel articleDataExportModel = articleHierarchyManager.GetArticleExportData(NodeCode, CommonModel.SiteCode);
                    
                    if (cboBarCodeType.SelectedValue != null)
                    {
                     //   articleDataExportModel.ArticleDetails = articleDataExportModel.ArticleDetails.Where(x => x.DISCRIPTION == cboBarCodeType.SelectedValue.ToString()).ToList();
                        articleDataExportModel.ArticleDetails = articleDataExportModel.ArticleDetails.Where(x => x.BarcodeType == cboBarCodeType.SelectedValue.ToString()).ToList();  //vipin
                    }
                    if (articleDataExportModel.ArticleDetails.Count > 0)
                    {
                        btnExport.Visible = true;
                        foreach (var item in articleDataExportModel.ArticleDetails)
                        {
                            gridBarcode.Rows.Add();
                            gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.ItemCode] = item.ArticleCode.ToString();
                           // gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.EAN] = item.EAN.ToString();
                            gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.EAN] = item.Barcode.ToString();
                            gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.ItemDescription] = item.ArticleShortName.ToString();
                           // gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.BarcodeType] = item.DISCRIPTION.ToString();
                            gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.BarcodeType] = item.BarcodeType.ToString();  //vipin
                            gridBarcode.Rows[gridBarcode.Rows.Count - 1][(int)enumBarcode.Exclude] = false;
                        }
                    }
                    else
                    {                     
                        btnExport.Visible = false;
                        CommonFunc.ShowMessage("Data Not Exist", MessageType.Information);
                    }
                }
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CommonFunc.ShowMessage("Are You Sure? All the changes will be lost", MessageType.OKCancel) == DialogResult.OK)
            {
                this.Close();
                this.Dispose();
            }
        }

        #endregion

        

        #endregion

    }
}
