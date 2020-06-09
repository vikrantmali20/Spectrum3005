using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmArticleStockOut : Spectrum.Controls.RibbonForm
    {
        #region "Class Variables"
        public frmArticleStockOut()
        {
            InitializeComponent();
            this.commonManager = new CommonManager();
            this.supplierManager = new SupplierManager();
            this.articleManager = new ArticleManager();
            this.articleStockManager = new ArticleStockBalancesManager();
        }

        ISupplierManager supplierManager;
        ICommonManager commonManager;
        IArticleManager articleManager;
        IArticleStockBalancesManager articleStockManager;

        private ArticleStockOutModel articleStockOutModel;
        private IList<ArticlePurchaseStockoutModel> ArticleStockList { get; set; }
        IList<DropDownModel> stockOutReasonList;
        IList<DropDownModel> fromLocationList;
        IList<DropDownModel> supplierList;

        string grnNumber, finYear, documentNumber;
        private enum GridColumnArticles
        {
            Select,
            ArticleCode,
            ArticleDescription,
            EAN,
            UOM,
            AvailableQty,
            AdjustmentQty,
            Reason
        }

        #endregion

        #region "Events"
        private void frmStockOut_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                PopulateComboBoxStockOut();
                ResetArticleStockOutData();
                CommonFunc.SetCultureFromResource(this);
                //code added by roshan for issue id 2835
                lblStockOutReasons.NormalLabelText = "Stock Out Reason";
                lblStockOutReasons.MandatoryLabelText = "*";
                lblStockOutReasons.ForeColor = Color.Red;  
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnAddArticle_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = true;
                if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref Itemerrorprovider, ref cmbStockOutReason, "Stock Out Reason required"))
                {
                    this.cmbStockOutReason.Focus();
                    isValid = false;
                }
                if ((string)cmbStockOutReason.SelectedValue == "SupplierReturn" || (string)cmbStockOutReason.SelectedValue == "WriteOff")
                {
                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref Itemerrorprovider, ref cmbFromLocation, "From Location Reason required"))
                    {
                        this.cmbFromLocation.Focus();
                        isValid = false;
                    }
                }

                if (!isValid)
                    return;
                string supplierCode = "";
                if (cmbSupplierName.SelectedValue !=null)
                 supplierCode = (cmbSupplierName.SelectedValue != "Select") ? cmbSupplierName.SelectedValue.ToString() : string.Empty;
                var articleList = this.articleManager.GetArticlePurchaseList(supplierCode);

                
                   

                frmCommonSearchTrueGrid objSearch = new frmCommonSearchTrueGrid(multipleSelect: true, defaultFilter: false);
                DataTable dtItems = CommonFunc.ConvertListToDataTable(articleList);

                objSearch.Text = "Item Search";
                objSearch.boolWildSearch = true;
                objSearch.dtcommonSearch = dtItems;
                DataTable dtSelectedItems = new DataTable();

                if (objSearch.ShowDialog() == DialogResult.OK)
                {
                    dtSelectedItems = objSearch.dtSelectedList;
                    //  List<ArticlePurchaseModel> selectedIArticleList = DataTableToList.ConvertDataTableToList<ArticlePurchaseModel>(dtSelectedItems);
                    List<ArticlePurchaseStockoutModel> selectedIArticleList = DataTableToList.ToList<ArticlePurchaseStockoutModel>(dtSelectedItems);

                    AddSelectedArticlesIntoGrid(selectedIArticleList);
                }
                objSearch.Dispose();

                
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnArticleDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool rowDeleted = false;
                var articleCodes = new List<string>();

                for (int rowIndex = 1; rowIndex < gridScanArticle.Rows.Count; rowIndex++)
                {
                    if (Convert.ToBoolean(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Select]))
                    {
                        rowDeleted = true;
                        articleCodes.Add(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.ArticleCode].ToString());
                    }
                }

                if (!rowDeleted)
                {
                    CommonFunc.ShowMessage("Please select at least 1 record to delete.", MessageType.Information);
                    return;
                }
                else
                {
                    if (CommonFunc.ShowMessage("The selected record(s) will be deleted. Are you sure?", MessageType.OKCancel) == DialogResult.OK)
                    {
                        var articleList = ArticleStockList.Where(a => articleCodes.Contains(a.ArticleCode)).ToList();

                        for (int iRow = 0; iRow < articleList.Count(); iRow++)
                            ArticleStockList.Remove(articleList[iRow]);

                        gridScanArticle.DataSource = ArticleStockList.ToList();
                        DefaultGridSetting();

                        chkSelectAll.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int rowIndex = 1; rowIndex < gridScanArticle.Rows.Count; rowIndex++)
                    gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Select] = chkSelectAll.Checked;
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ResetArticleStockOutData();
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CommonFunc.ShowMessage("Are You Sure? All the changes will be lost", MessageType.OKCancel) == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }
        
        private void cmbStockOutReason_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSupplierName.Enabled = (cmbStockOutReason.SelectedValue.ToString() == "SupplierReturn") ? true : false;
                if (cmbSupplierName.SelectedIndex != -1) 
                cmbSupplierName.SelectedIndex = (cmbStockOutReason.SelectedValue.ToString() == "SupplierReturn") ? 0 : -1;

                cmbFromLocation.Enabled = ("SupplierReturn, WriteOff".Contains(cmbStockOutReason.SelectedValue.ToString())) ? true : false;
                if (cmbFromLocation.SelectedIndex != -1)
                cmbFromLocation.SelectedIndex = ("SupplierReturn, WriteOff".Contains(cmbStockOutReason.SelectedValue.ToString())) ? 0 : -1;
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateArticleStock())
                {
                    if (CommonFunc.ShowMessage("Are you sure? The stock quantity will be updated in the system.", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                    {
                        articleStockOutModel = new ArticleStockOutModel();
                        articleStockOutModel.StockOutReason = GetStockOutReason();
                        if (cmbFromLocation.SelectedIndex != -1)
                        {
                          articleStockOutModel.StockFromLocation = GetStockFromLocation();
                        }
                        
                        articleStockOutModel.OrderDtlModels = FillOrderDtlDataToModel();
                        articleStockOutModel.Reason = txtRemark.Text.Trim();

                        this.articleStockManager.SaveArticleStockOutData(articleStockOutModel);

                        CommonFunc.ShowMessage("Stock Quantity has been updated", MessageType.Information);
                        ResetArticleStockOutData();
                    }
                }
                
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region "Functions"

        private void PopulateComboBoxStockOut()
        {
            try
            {
                this.stockOutReasonList = this.commonManager.GetStockOutReasons();
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbStockOutReason, this.stockOutReasonList);
                //cmbStockOutReason.DataSource = stockOutReasonList;
                //cmbStockOutReason.DisplayMember = "Description";
                //cmbStockOutReason.ValueMember = "Code";

                this.supplierList = (from result in this.commonManager.GetSupplierBySite(CommonModel.SiteCode)
                                 select new DropDownModel
                                 {
                                     Code = result.SupplierCode,
                                     Description = result.SupplierName
                                 }).ToList();
                 
                
                supplierList.Insert(0, new DropDownModel { Code = "Select", Description = "-- Select --" });
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbSupplierName, this.supplierList);
                //cmbSupplierName.DataSource = supplierList;
                //cmbSupplierName.DisplayMember = "Description";
                //cmbSupplierName.ValueMember = "Code";

                this.fromLocationList = this.commonManager.GetFromLocation();
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbFromLocation, this.fromLocationList);
                //cmbFromLocation.DataSource = fromLocationList;
                //cmbFromLocation.DisplayMember = "Description";
                //cmbFromLocation.ValueMember = "Code";
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void DefaultGridSetting()
        {
            try
            {
                gridScanArticle.Cols["Select"].Width = 45;
                gridScanArticle.Cols["Select"].Caption = string.Empty;

                gridScanArticle.Cols["ArticleCode"].Width = 105;
                gridScanArticle.Cols["ArticleCode"].AllowEditing = false;
                gridScanArticle.Cols["ArticleName"].Width = 195;
                gridScanArticle.Cols["ArticleName"].AllowEditing = false;
                gridScanArticle.Cols["EAN"].Width = 80;
                gridScanArticle.Cols["EAN"].AllowEditing = false;
                gridScanArticle.Cols["BaseUnitofMeasure"].Width = 45;
                gridScanArticle.Cols["BaseUnitofMeasure"].AllowEditing = false;
                gridScanArticle.Cols["AvailableQty"].Width = 120;
                gridScanArticle.Cols["AvailableQty"].AllowEditing = false;
                gridScanArticle.Cols["AdjustmentQty"].Width = 135;
                gridScanArticle.Cols["Reason"].Width = 75;
                gridScanArticle.Cols["FILTER"].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void ResetArticleStockOutData()
        {
            try
            {
                grpFilters.Enabled = true;
                cmbStockOutReason.SelectedIndex = 0;

                cmbSupplierName.SelectedIndex = 0;
                cmbSupplierName.Enabled = true;

                cmbFromLocation.SelectedIndex = 0;
                cmbFromLocation.Enabled = true;

                txtRemark.Value = string.Empty;
                txtRemark.MaxLength = 100;
                ArticleStockList = new List<ArticlePurchaseStockoutModel>();
                gridScanArticle.DataSource = ArticleStockList;
                DefaultGridSetting();
                //remove error indicator
                Itemerrorprovider.SetError(txtRemark, string.Empty);
                txtRemark.BorderColor = CommonFunc.DefaultBorderColor;
                Itemerrorprovider.SetError(cmbFromLocation, string.Empty);
                cmbFromLocation.BackColor = CommonFunc.DefaultBorderColor;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void AddSelectedArticlesIntoGrid(IList<ArticlePurchaseStockoutModel> selectedArticle)
        {
            try
            {
                for (int i = 0; i < selectedArticle.Count; i++)
                {
                    var oldArticle = ArticleStockList.Where(a => a.ArticleCode == selectedArticle[i].ArticleCode).FirstOrDefault();

                    if (oldArticle != null)
                    {
                        oldArticle.AdjustmentQty += 1;
                    }
                    else
                    {
                        selectedArticle[i].Select = false;
                        ArticleStockList.Add(selectedArticle[i]);
                    }
                }

                gridScanArticle.DataSource = ArticleStockList.ToList();
                DefaultGridSetting();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private bool ValidateArticleStock()
        {
            try
            {
                bool valfields = true, valAdjQuantity = true, valAdjNegativeQuantity = true;
                int ValidReasonLengthCounter = 0;
                int ValidReasonTextCounter = 0;
                string a = (string)cmbStockOutReason.SelectedValue;
                Regex patt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");


                if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref Itemerrorprovider, ref cmbStockOutReason, "Stock Out Reason required"))
                {
                    this.cmbStockOutReason.Focus();
                    valfields = false;
                }

                if ((string)cmbStockOutReason.SelectedValue == "SupplierReturn" || (string)cmbStockOutReason.SelectedValue == "WriteOff")
                {
                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref Itemerrorprovider, ref cmbFromLocation, "From Location Reason required"))
                    {
                        this.cmbFromLocation.Focus();
                        valfields = false;
                    }
                    else
                    {
                        Itemerrorprovider.SetError(cmbFromLocation, string.Empty);
                        cmbFromLocation.BackColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtRemark.Text.Trim()))
                {

                    if (patt.IsMatch(txtRemark.Text) == false)
                    {
                        valfields = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref Itemerrorprovider, ref txtRemark, "Remark Allows Alphanumeric Character", false))
                        {
                            this.txtRemark.Focus();
                        }
                    }
                    else
                    {
                        Itemerrorprovider.SetError(txtRemark, string.Empty);
                        txtRemark.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                int rowcount = gridScanArticle.Rows.Count;
                if (rowcount > 1)
                {
                    for (int rowIndex = 1; rowIndex < gridScanArticle.Rows.Count; rowIndex++)
                    {
                        Int64 intavailqty = Convert.ToInt64(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.AvailableQty]);
                        Int64 LookupValue;
                        bool LookupValueIsANumber = Int64.TryParse(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.AdjustmentQty].ToString(), out LookupValue);

                        if (LookupValueIsANumber == false)
                        {
                            CommonFunc.ShowMessage("Please Enter Proper Adjustment quantity", Models.Enums.MessageType.Information);
                            gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.AdjustmentQty] = 0;
                            return false;
                        }
                        Int64 intadjustqty = Convert.ToInt64(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.AdjustmentQty]);
                        string reason = Convert.ToString(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Reason]);
                        if (intadjustqty > 0 && intadjustqty > intavailqty)
                            valAdjQuantity = false;
                        else if (intadjustqty <= 0)
                            valAdjNegativeQuantity = false;
                        if (reason.Trim() != "" && reason.Trim().Length > 30)
                            ValidReasonLengthCounter = ValidReasonLengthCounter + 1;
                        else if (reason.Trim() != "" && patt.IsMatch(reason.Trim()) == false)
                            ValidReasonTextCounter = ValidReasonTextCounter + 1;

                    }
                }
                else
                {
                    if (valfields)
                    {
                        CommonFunc.ShowMessage("Please add Item", MessageType.Information);
                        valfields = false;
                        return valfields;
                    }
                }
                if (!valAdjQuantity)
                {
                    CommonFunc.ShowMessage("Adjustment quantity must be less than Available quantity", Models.Enums.MessageType.Information);
                    return false;
                }
                if (!valAdjNegativeQuantity)
                {
                    CommonFunc.ShowMessage("Adjustment quantity must be greater than Zero(0)", Models.Enums.MessageType.Information);
                    return false;
                }
                if (ValidReasonLengthCounter > 0)
                {
                    CommonFunc.ShowMessage("Reason allows 30 characters only", Models.Enums.MessageType.Information);
                    return false;
                }
                if (ValidReasonTextCounter > 0)
                {
                    CommonFunc.ShowMessage("Reason allows alphnumeric characters only", Models.Enums.MessageType.Information);
                    return false;
                }
                return valfields;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }
        
        private IList<OrderDtlModel> FillOrderDtlDataToModel()
        {
            try
            {
                var OrderDtlModels = new List<OrderDtlModel>();

                for (int rowIndex = 0; rowIndex < ArticleStockList.Count; rowIndex++)
                {
                    OrderDtlModels.Add(new OrderDtlModel
                    {
                        SiteCode = CommonModel.SiteCode,
                        FinYear = finYear,
                        DocumentNumber = grnNumber,
                        ArticleCode = ArticleStockList[rowIndex].ArticleCode,
                        EAN = ArticleStockList[rowIndex].EAN,
                        LineNumber = rowIndex + 1,
                        UnitofMeasure = ArticleStockList[rowIndex].BaseUnitofMeasure,
                        AdjustmentQty = ArticleStockList[rowIndex].AdjustmentQty,
                        Reason = ArticleStockList[rowIndex].Reason,
                        CreatedAt = CommonModel.SiteCode,
                        CreatedBy = CommonModel.UserID,
                        CreatedOn = CommonModel.CurrentDate,
                        UpdatedAt = CommonModel.SiteCode,
                        UpdatedBy = CommonModel.UserID,
                        UpdatedOn = CommonModel.CurrentDate,
                        Status = true
                    });
                }

                return OrderDtlModels;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private StockOutReason GetStockOutReason()
        {
            try
            {
                string stockOutReason = cmbStockOutReason.SelectedValue.ToString();
                switch (stockOutReason)
                {
                    case "WriteOff":
                        return StockOutReason.WriteOff;
                    case "Damaged":
                        return StockOutReason.Damaged;
                    case "NonSaleable":
                        return StockOutReason.NonSaleable;
                    case "SupplierReturn":
                        return StockOutReason.ReturnToSupplier;
                    default:
                        return StockOutReason.None;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private StockFromLocation GetStockFromLocation()
        {
            try
            {
                string stockFromLocation = cmbFromLocation.SelectedValue.ToString();
                switch (stockFromLocation)
                {
                    case "Damaged":
                        return StockFromLocation.Damaged;
                    case "Saleable":
                        return StockFromLocation.Saleable;
                    case "NonSaleable":
                        return StockFromLocation.NonSaleable;
                    default:
                        return StockFromLocation.None;
                }
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

            btnAddArticle.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnAddArticle.BackColor = Color.Transparent;
            btnAddArticle.BackColor = Color.FromArgb(0, 107, 163);
            btnAddArticle.ForeColor = Color.FromArgb(255, 255, 255);
            btnAddArticle.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnAddArticle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnAddArticle.FlatAppearance.BorderSize = 0;
            btnAddArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnArticleDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnArticleDelete.BackColor = Color.Transparent;
            btnArticleDelete.BackColor = Color.FromArgb(0, 107, 163);
            btnArticleDelete.ForeColor = Color.FromArgb(255, 255, 255);
            btnArticleDelete.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnArticleDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnArticleDelete.FlatAppearance.BorderSize = 0;
            btnArticleDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnClear.BackColor = Color.Transparent;
            btnClear.BackColor = Color.FromArgb(0, 107, 163);
            btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSave.BackColor = Color.Transparent;
            btnSave.BackColor = Color.FromArgb(0, 107, 163);
            btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            lblfromlocation.BackColor = Color.FromArgb(212, 212, 212);

            lblremark.BackColor = Color.FromArgb(212, 212, 212);
           lblStockOutReasons.BackColor = Color.FromArgb(212, 212, 212);
          //  lblStockOutReasons.ForeColor = Color.Black;
           lblSupplier.BackColor = Color.FromArgb(212, 212, 212);
            grpFilters.BackColor = Color.FromArgb(134, 134, 134);
            grpFilters.ForeColor = Color.White;
            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);

            gridScanArticle.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            gridScanArticle.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            gridScanArticle.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            gridScanArticle.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            gridScanArticle.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridScanArticle.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridScanArticle.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridScanArticle.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridScanArticle.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            gridScanArticle.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            gridScanArticle.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
        }
        #endregion

         

       

    }
}
