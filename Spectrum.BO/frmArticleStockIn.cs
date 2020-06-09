using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C1.C1Excel;
using C1.Win.C1FlexGrid;
using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.Windows;
namespace Spectrum.BO
{
    public partial class frmArticleStockIn : Spectrum.Controls.RibbonForm
    {
        #region "Class Variables"
        public frmArticleStockIn()
        {
            InitializeComponent();

            this.commonManager = new CommonManager();
            this.supplierManager = new SupplierManager();
            this.articleManager = new ArticleManager();
            this.articleStockManager = new ArticleStockBalancesManager();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select article stock file";
            openFileDialog.Filter = "Excel xlsx|*.xlsx|Excel xls|*.xls";
            openFileDialog.DefaultExt = ".xlsx";
            openFileDialog.RestoreDirectory = true;
        }

        ICommonManager commonManager;
        ISupplierManager supplierManager;
        IArticleManager articleManager;
        IArticleStockBalancesManager articleStockManager;
        private IList<ArticlePurchaseModel> ArticleStockList { get; set; }
        private ArticleStockInModel articleStockInOutModel;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        IList<DropDownModel> supplier;
        OpenFileDialog openFileDialog;
        string grnNumber, finYear, documentNumber, invoiceNumber;
        List<FailedArticleModel> faildArticlesList = new List<FailedArticleModel>();
        Boolean GridSelect;
        private enum GridColumnArticles
        {
           Select,
            ArticleCode,
            ArticleDescription,
            EAN,
            UOM,
            Quantity,
            CostPrice,
            TaxAmount,
            NetAmount,
        }

        #endregion

        #region "Events"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmArticleStockIn_Load(object sender, EventArgs e)
        {
            txtArticleFilePath.Enabled = false; //vipin
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                this.supplier = (from result in this.commonManager.GetSupplierBySite(CommonModel.SiteCode)
                                 select new DropDownModel
                                 {
                                     Code = result.SupplierCode,
                                     Description = result.SupplierName
                                 }).ToList();
                supplier.Insert(0, new DropDownModel { Code = null, Description = "Select" });

                //cmbSupplierName.DataSource = supplier;
                //cmbSupplierName.DisplayMember = "Description";
                //cmbSupplierName.ValueMember = "Code";
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbSupplierName, this.supplier);
                ResetArticleStockInData();
                // CommonFunc.WriteResourceFile(this);
              //  CommonFunc.SetCultureFromResource(this);
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseArticleFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (".xls,.xlsx".Contains(Path.GetExtension(openFileDialog.FileName)))
                        txtArticleFilePath.Value = openFileDialog.FileName;
                    else
                    {
                        CommonFunc.ShowMessage("Please Browse Excel File", MessageType.Information);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadArticle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtArticleFilePath.Text == "")
                {
                    CommonFunc.ShowMessage("Please Browse File", MessageType.Information);
                    return;
                }
                else
                {
                    faildArticlesList = new List<FailedArticleModel>();
                    C1XLBook book = new C1XLBook();
                    book.Load(Path.GetFullPath(txtArticleFilePath.Text));
                    XLSheetCollection sheets = book.Sheets;

                    XLSheet articleSheet = sheets[0];
                    var articleCodes = new List<string>();
                    var quantity = new Dictionary<string, string>();
                    var totalColumns = articleSheet.Columns.Count;
                    if (totalColumns != 2)
                    {
                        CommonFunc.ShowMessage("Please Browse Proper Format File", MessageType.Information);
                      //  txtArticleFilePath.Text = "";
                        return;
                    }
                    else if (totalColumns == 2)
                    {
                        if (articleSheet[0, 0].Value.ToString() == "ArticleCode" && articleSheet[0, 1].Value.ToString() == "Qty")
                        {
                        }
                        else
                        {
                            CommonFunc.ShowMessage("Please Browse Proper Format File", MessageType.Information);
                         //   txtArticleFilePath.Text = "";
                            return;
                        }
                    }
                    if (articleSheet.Rows.Count > 1)
                    {
                        for (int rowIndex = 1; rowIndex < articleSheet.Rows.Count; rowIndex++)
                        {
                            var checkQty = articleSheet[rowIndex, 1].Value;
                            Regex patt = new Regex("^[0-9.]+$");

                            if (articleSheet[rowIndex, 1].Value != null)
                            {
                                if (patt.IsMatch(articleSheet[rowIndex, 1].Value.ToString()) == false)
                                {
                                }
                                else
                                {
                                    articleCodes.Add(articleSheet[rowIndex, 0].Value.ToString());
                                    quantity.Add(articleSheet[rowIndex, 0].Value.ToString(), articleSheet[rowIndex, 1].Value.ToString());
                                    FailedArticleModel faildArticles = new FailedArticleModel();
                                    faildArticles.ArticleCode = articleSheet[rowIndex, 0].Value.ToString();
                                    faildArticles.Quantity = articleSheet[rowIndex, 1].Value.ToString();
                                    faildArticlesList.Add(faildArticles);

                                }
                            }
                        }
                    }
                    else
                    {
                        CommonFunc.ShowMessage("Please Browse Proper Format File", MessageType.Information);
                       // txtArticleFilePath.Text = "";
                        return;
                    }
                    var selectedArticles = this.articleManager.GetArticlePurchaseList(string.Empty, articleCodes);
                    if (selectedArticles.Count > 0)
                    {
                        for (int rowIndex = 0; rowIndex < selectedArticles.Count; rowIndex++)
                        {
                           
                            var article = selectedArticles.ElementAt(rowIndex);
                             
                            article.Quantity = decimal.Parse(quantity.Where(a => a.Key == article.ArticleCode).FirstOrDefault().Value);
                            article.NetAmount = article.Quantity * article.Cost.Value;
                            var itemToRemove = faildArticlesList.Single(r => r.ArticleCode == article.ArticleCode);
                            faildArticlesList.Remove(itemToRemove);
                        }
                        AddSelectedArticlesIntoGrid(selectedArticles);
                    }
                    else
                    {
                        CommonFunc.ShowMessage("Articles does not exist.", MessageType.Information);
                    }
                  //  txtArticleFilePath.Text = "";
                }
            }
            catch (Exception ex)
            {

                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScanArticle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtScanArticle.Text.Trim()))
                {
                    var articleCodes = new List<string>();
                    articleCodes.Add(txtScanArticle.Text.Trim());

                    var selectedArticles = this.articleManager.GetArticlePurchaseList(string.Empty, articleCodes);

                    if (selectedArticles != null && selectedArticles.Count > 0)
                    {
                        var articleDefaultEan = this.commonManager.GetDefaultEANbyArticle(txtScanArticle.Text.Trim());
                        var selectedArticlesfirst = articleDefaultEan.Select(x => x.EAN);
                        foreach (var item in selectedArticles)
                        {
                            item.EAN = selectedArticlesfirst.First();
                        }
                        AddSelectedArticlesIntoGrid(selectedArticles);

                    }
                    txtScanArticle.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked)
                {
                    for (int rowArticleCode = 1; rowArticleCode < gridScanArticle.Rows.Count; rowArticleCode++)
                    {
                        gridScanArticle.Rows[rowArticleCode][(int)GridColumnArticles.Select] = chkSelectAll.Checked;
                    }

                }
                else
                {
                    if (GridSelect == false)
                    {
                        for (int rowArticleCode = 1; rowArticleCode < gridScanArticle.Rows.Count; rowArticleCode++)
                        {
                            gridScanArticle.Rows[rowArticleCode][(int)GridColumnArticles.Select] = chkSelectAll.Checked;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArticleDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool rowDeleted = false;
                var articleCodes = new List<string>();
                if (gridScanArticle.Rows.Count == 1)
                    return;

                for (int rowIndex = 1; rowIndex < gridScanArticle.Rows.Count; rowIndex++)
                {
                    if (Convert.ToBoolean(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Select]))
                    {
                        if (gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.ArticleCode] != null)
                        {
                            rowDeleted = true;
                            articleCodes.Add(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.ArticleCode].ToString());
                        }
                    }
                }

                if (!rowDeleted)
                {
                    CommonFunc.ShowMessage("Please select at least one item.", MessageType.Information);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddArticle_Click(object sender, EventArgs e)
        {
            try
            {
                string supplierCode = (cmbSupplierName.SelectedValue != null) ? cmbSupplierName.SelectedValue.ToString() : string.Empty;
                var articleList = this.articleManager.GetArticlePurchaseList(supplierCode, new List<string>());


                if (articleList.Count > 0)
                {

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
                        if(dtSelectedItems!=null)
                        {
                        List<ArticlePurchaseModel> selectedIArticleList = DataTableToList.ToList<ArticlePurchaseModel>(dtSelectedItems);

                        AddSelectedArticlesIntoGrid(selectedIArticleList);
                        }
                    }
                    objSearch.Dispose();

                }
                else
                {
                    CommonFunc.ShowMessage("Article not exist", MessageType.Information);
                }

                //frmCommonSearch objSearch = new frmCommonSearch(true);
                //objSearch.DataList = articleList;





            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void gridScanArticle_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Row > 0 && (e.Col == 5 || e.Col == 6 || e.Col == 7))
                {

                    Int64 intqty = Convert.ToInt64(gridScanArticle.Rows[e.Row][(int)GridColumnArticles.Quantity]);

                    //code added by vipul for issue id 2834
                    decimal Qty = Convert.ToDecimal(gridScanArticle.Rows[e.Row][(int)GridColumnArticles.Quantity]);
                    if (gridScanArticle.Rows[e.Row][(int)GridColumnArticles.UOM].ToString().Equals("NOS"))
                    {
                        decimal d = (decimal)Qty;
                        if ((d % 1) > 0)
                        {
                            CommonFunc.ShowMessage("Quantity cannot be in decimal for NOS article", MessageType.Information);
                            return;
                        }

                    }
                    if (intqty < 0)
                    {
                        CommonFunc.ShowMessage("Quantity should be more than 0", MessageType.Information);
                        return;
                    }
                    Decimal intcost = (gridScanArticle.Rows[e.Row][(int)GridColumnArticles.CostPrice] == null) ? 0 : Convert.ToInt64(gridScanArticle.Rows[e.Row][(int)GridColumnArticles.CostPrice]);
                    if (intcost < 0)
                    {
                        CommonFunc.ShowMessage("Cost Price should be more than 0", MessageType.Information);
                        return;
                    }
                    Decimal inttax = (gridScanArticle.Rows[e.Row][(int)GridColumnArticles.TaxAmount] == null) ? 0 : Convert.ToInt64(gridScanArticle.Rows[e.Row][(int)GridColumnArticles.TaxAmount]);
                    if (inttax < 0)
                    {
                        CommonFunc.ShowMessage("Tax Amount should be more than 0", MessageType.Information);
                        return;
                    }
                    gridScanArticle.Rows[e.Row][(int)GridColumnArticles.NetAmount] = Convert.ToDecimal(string.Format("{0:0.00}", (intqty * Convert.ToDecimal(string.Format("{0:0.00}", intcost))) + Convert.ToDecimal(string.Format("{0:0.00}", inttax))));

                }

            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateArticleStock())
                {
                    if (CommonFunc.ShowMessage("Are you sure? The stock Quantity will be updated in the system.", MessageType.OKCancel) == DialogResult.OK)
                    {
                        bool status = false;
                        DateTime dt = DateTime.Now;
                        string stryear = dt.ToString("yy");
                        finYear = dt.ToString("yyyy");

                        string sitecodenew = CommonModel.SiteCode.ToString();
                        sitecodenew = sitecodenew.Substring(sitecodenew.Length - 3);

                        int grnNextNo = commonManager.GetNextID(CommonModel.SiteCode, "GR");
                        grnNextNo = grnNextNo + 1;                        
                        string strlastcode = string.Format("{0}", grnNextNo.ToString().PadLeft( 7, '0'));
                        grnNumber = "GRS" + sitecodenew + stryear + strlastcode;
                        documentNumber = grnNumber;
                        invoiceNumber = "INVS" + sitecodenew + stryear + strlastcode;
                        articleStockInOutModel = new ArticleStockInModel();
                        articleStockInOutModel.OrderDtlModels = FillOrderDtlDataToModel();
                        articleStockInOutModel.OrderHdrModel = FillOrderHdrDataToModel();
                        articleStockInOutModel.ArticleStockBalanceModels = FillArticleStockBalanceDataToModel();
                        articleStockInOutModel.InvoiceModel = FillInvoiceDataToModel();

                        this.articleStockManager.SaveArticleStockInData(articleStockInOutModel);

                        CommonFunc.ShowMessage("Stock Quantity has been updated", MessageType.Information);
                        ResetArticleStockInData();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkFailedArticles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (faildArticlesList.ToList().Count > 0)
                {
                    DataTable dtExport = CommonFunc.ConvertListToDataTable(faildArticlesList.ToList());

                    dtExport.Columns[0].ColumnName = "Article Code";
                    dtExport.Columns[1].ColumnName = "Quantity";


                    Cursor.Current = Cursors.WaitCursor;
                    DialogResult resultShow = fbd.ShowDialog();
                    if (resultShow == DialogResult.OK)
                    {
                        string path = Path.Combine(fbd.SelectedPath, "FailedArticel" + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString();
                        bool IsExported = ConvertListToExcel.DatatableToExcel(dtExport, path);
                        System.Diagnostics.Process.Start(path);
                        if (IsExported)
                            MessageBox.Show("Exported Successfully");
                    }
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    CommonFunc.ShowMessage("Failed Articles does not exist", MessageType.Information);
                }
            }


            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkArtilceExcel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string appPath = Path.Combine(Application.StartupPath, "Resources\\ArticleDemo.xlsx");
                //Code is added by irfan for Mantis issue on 09/02/2018===============================
                C1XLBook book = new C1XLBook();
                book.Load(appPath);
                XLSheetCollection sheets = book.Sheets;
                XLSheet articleSheet = sheets[0];
                if (articleSheet.Rows.Count > 0)
                {
                    if (articleSheet.Rows.Count != 1)
                    {
                        for (int i = 1; i <= articleSheet.Rows.Count; i++)
                        {
                            i = 1;
                            articleSheet.Rows.RemoveAt(i);
                        }
                        book.Save(appPath);
                    }
                }
             //========================================================================================
                System.Diagnostics.Process.Start(appPath);
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridScanArticle.Rows.Count > 1)
                {
                    int totalOn = gridScanArticle.Cols.Count - 1;
                    string caption = "Total ";



                    // calculate three levels of totals
                    gridScanArticle.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;

                    gridScanArticle.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn - 1);
                    gridScanArticle.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn - 2);
                    gridScanArticle.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn - 3);
                    gridScanArticle.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn - 4);
                    //gridScanArticle.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn, caption);
                    //itemgrid.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn-4, caption);
                    //itemgrid.FindRow(itemgrid ,0,4,true);

                    gridScanArticle[gridScanArticle.Rows.Count - 1, 4] = caption;
                }
            }

            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (CommonFunc.ShowMessage("Are You Sure? All the changes will be lost", MessageType.OKCancel) == DialogResult.OK)
            {
                cmbSupplierName.Dispose();

                this.Close();
                this.Dispose();
            }
        }

        #endregion

        #region "Functions"
        /// <summary>
        /// 
        /// </summary>
        private void DefaultGridSetting()
        {
            try
            {
                gridScanArticle.Cols["Select"].Width = 40;
                gridScanArticle.Cols["Select"].Caption = string.Empty;

                gridScanArticle.Cols["ArticleCode"].Width = 100;
                gridScanArticle.Cols["ArticleCode"].AllowEditing = false;
                gridScanArticle.Cols["ArticleName"].Width = 195;
                gridScanArticle.Cols["ArticleName"].AllowEditing = false;
                gridScanArticle.Cols["EAN"].Width = 120;
                gridScanArticle.Cols["EAN"].AllowEditing = false;
                gridScanArticle.Cols["BaseUnitofMeasure"].Width = 65;
                gridScanArticle.Cols["BaseUnitofMeasure"].AllowEditing = false;
                gridScanArticle.Cols["Quantity"].Width = 75;
                gridScanArticle.Cols["Cost"].Width = 95;
                gridScanArticle.Cols["Tax"].Width = 75;
                gridScanArticle.Cols["NetAmount"].Width = 115;
                gridScanArticle.Cols["NetAmount"].AllowEditing = false;
                gridScanArticle.Cols["FILTER"].Visible = false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetArticleStockInData()
        {
            try
            {
                txtScanArticle.MaxLength = 26;
                txtInvoiceNumber.MaxLength = 100;
                txtInvoiceNumber.Value = string.Empty;
                txtInvoiceAmount.Value = string.Empty;
                dtpInvoiceDate.Value = string.Empty;
                txtScanArticle.Value = string.Empty;
                cmbSupplierName.SelectedIndex = -1;
                txtArticleFilePath.Value = string.Empty;

                ArticleStockList = new List<ArticlePurchaseModel>();
                gridScanArticle.DataSource = ArticleStockList;
                DefaultGridSetting();
                chkSelectAll.Checked = false;

                //clear error indicator
                articlePurchaseProvider.SetError(txtInvoiceNumber, string.Empty);
                txtInvoiceNumber.BorderColor = CommonFunc.DefaultBorderColor;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private bool ValidateArticleStock()
        {
            try
            {
                bool isValid = true;
             Boolean IsSingleCheckBoxCheck =false;  //vipin

                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text.Trim()))
                {
                    var invoiceNumber = articleStockManager.CheckInvoiceID(txtInvoiceNumber.Text.Trim());
                    if (invoiceNumber != null)
                    {
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref articlePurchaseProvider, ref txtInvoiceNumber, "Invoice Number Already Exist", false))
                        {
                            this.txtInvoiceNumber.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        articlePurchaseProvider.SetError(txtInvoiceNumber, string.Empty);
                        txtInvoiceNumber.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                int rowcount = gridScanArticle.Rows.Count;
                if (rowcount > 1)
                {
                    for (int rowIndex = 1; rowIndex < gridScanArticle.Rows.Count; rowIndex++)
                    {
                        Int64 intqty = Convert.ToInt64(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Quantity]);
                        Decimal intcost = (gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.CostPrice] == null) ? 0 : Convert.ToInt64(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.CostPrice]);
                        Decimal inttax = (gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.TaxAmount] == null) ? 0 : Convert.ToInt64(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.TaxAmount]);

                        if (IsSingleCheckBoxCheck != true)  //vipin 
                        {
                            if (Convert.ToBoolean(gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.Select]) == true)
                            {
                                IsSingleCheckBoxCheck = true;
                            }
                        }
                        if (IsSingleCheckBoxCheck == false)
                        {
                            CommonFunc.ShowMessage("Select at least one article", MessageType.Information);
                            isValid = false;
                            return isValid;
                        }

                        //changes end by vipin

                        if (gridScanArticle.Rows[rowIndex][(int)GridColumnArticles.ArticleCode] != null)
                        {

                        if (intqty <= 0)
                        {
                            CommonFunc.ShowMessage("Quantity should be more than 0", MessageType.Information);
                            isValid = false;
                            return isValid;
                        }
                        //else if (intcost <= 0)   // Commented by vipin as  per Tester 13-04-2017
                        //{
                        //    CommonFunc.ShowMessage("Cost Price should be more than 0", MessageType.Information);
                        //    isValid = false;
                        //    return isValid;
                        //}
                        else if (inttax < 0)
                        {
                            CommonFunc.ShowMessage("Tax Amount should be more than 0", MessageType.Information);
                            isValid = false;
                            return isValid;
                        }
                        }
                    }
                }
                else
                {
                    CommonFunc.ShowMessage("Please add Item", MessageType.Information);
                    isValid = false;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedArticle"></param>
        private void AddSelectedArticlesIntoGrid(IList<ArticlePurchaseModel> selectedArticle)
        {
            try
            {
                for (int i = 0; i < selectedArticle.Count; i++)
                {
                    var oldArticle = ArticleStockList.Where(a => a.ArticleCode == selectedArticle[i].ArticleCode).FirstOrDefault();

                    if (oldArticle != null)
                    {
                        oldArticle.Quantity += 1;
                        oldArticle.NetAmount = oldArticle.Quantity * oldArticle.Cost.Value;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceModel"></param>
        private InvoiceModel FillInvoiceDataToModel()
        {
            try
            {
                return new InvoiceModel
                {
                    SiteCode = CommonModel.SiteCode,
                    InvoiceCode = invoiceNumber,
                    InvoiceType = "Recieved",
                    InvoiceNumber = txtInvoiceNumber.Text,
                    InvoiceAmount = (!string.IsNullOrEmpty(txtInvoiceAmount.Text.Trim())) ? decimal.Parse(txtInvoiceAmount.Text.Trim()) : 0,
                    InvoiceDate = (dtpInvoiceDate.Value is DBNull) ? (Nullable<DateTime>)null : (DateTime)dtpInvoiceDate.Value,
                    SupplierCode = (cmbSupplierName.SelectedValue != null) ? cmbSupplierName.SelectedValue.ToString() : null,
                    GRNNumber = grnNumber,
                    CreatedAt = CommonModel.SiteCode,
                    CreatedBy = CommonModel.UserID,
                    CreatedOn = CommonModel.CurrentDate,
                    UpdatedAt = CommonModel.SiteCode,
                    UpdatedBy = CommonModel.UserID,
                    UpdatedOn = CommonModel.CurrentDate,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<ArticleStockBalanceModel> FillArticleStockBalanceDataToModel()
        {
            try
            {
                var articleStockBalanceModels = new List<ArticleStockBalanceModel>();

                for (int rowIndex = 0; rowIndex < ArticleStockList.Count; rowIndex++)
                {
                    if (ArticleStockList[rowIndex].Select == true)  //vipin
                    {
                        articleStockBalanceModels.Add(new ArticleStockBalanceModel
                        {
                            SiteCode = CommonModel.SiteCode,
                            ArticleCode = ArticleStockList[rowIndex].ArticleCode,
                            EAN = ArticleStockList[rowIndex].EAN,
                            PhysicalQty = ArticleStockList[rowIndex].Quantity,
                            TotalPhysicalSaleableQty = ArticleStockList[rowIndex].Quantity,
                            TotalSaleableQty = ArticleStockList[rowIndex].Quantity,
                            UpdatedAt = CommonModel.SiteCode,
                            UpdatedBy = CommonModel.UserID,
                            UpdatedOn = CommonModel.CurrentDate,
                            Status = true
                        });
                    }
                }

                return articleStockBalanceModels;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderhdrModel"></param>
        private OrderHdrModel FillOrderHdrDataToModel()
        {
            try
            {
                return new OrderHdrModel
                {
                    SiteCode = CommonModel.SiteCode,
                    FinYear = finYear,
                    DocumentNumber = grnNumber,
                    DocumentType = "GR",
                    DocDate = CommonModel.CurrentDate,
                    SupplierCode = (cmbSupplierName.SelectedValue != null) ? cmbSupplierName.SelectedValue.ToString() : null,
                    DeliverySiteCode = CommonModel.SiteCode,
                    ExpectedDeliveryDt = CommonModel.CurrentDate,
                    DocNetValue = articleStockInOutModel.OrderDtlModels.Sum(a => a.NetCostPrice),
                    DocGrossValue = articleStockInOutModel.OrderDtlModels.Sum(a => a.NetCostPrice),
                    ReturnReasonCode = "1",
                    CreatedAt = CommonModel.SiteCode,
                    CreatedBy = CommonModel.UserID,
                    CreatedOn = CommonModel.CurrentDate,
                    UpdatedAt = CommonModel.SiteCode,
                    UpdatedBy = CommonModel.UserID,
                    UpdatedOn = CommonModel.CurrentDate,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orddetmodel"></param>
        /// <param name="row"></param>
        private IList<OrderDtlModel> FillOrderDtlDataToModel()
        {
            try
            {
                var OrderDtlModels = new List<OrderDtlModel>();

                for (int rowIndex = 0; rowIndex < ArticleStockList.Count; rowIndex++)
                {
                    if (ArticleStockList[rowIndex].Select == true)  //vipin
                    {
                        OrderDtlModels.Add(new OrderDtlModel
                        {
                            SiteCode = CommonModel.SiteCode,
                            FinYear = finYear,
                            DocumentNumber = documentNumber,
                            ArticleCode = ArticleStockList[rowIndex].ArticleCode,
                            EAN = ArticleStockList[rowIndex].EAN,
                            LineNumber = rowIndex + 1,
                            UnitofMeasure = ArticleStockList[rowIndex].BaseUnitofMeasure,
                            Qty = ArticleStockList[rowIndex].Quantity,
                            CostPrice = ArticleStockList[rowIndex].Cost,
                            NetCostPrice = ArticleStockList[rowIndex].NetAmount,
                            ItemTaxAmt = ArticleStockList[rowIndex].Tax,
                            CreatedAt = CommonModel.SiteCode,
                            CreatedBy = CommonModel.UserID,
                            CreatedOn = CommonModel.CurrentDate,
                            UpdatedAt = CommonModel.SiteCode,
                            UpdatedBy = CommonModel.UserID,
                            UpdatedOn = CommonModel.CurrentDate,
                            Status = true
                        });
                    }
                }

                return OrderDtlModels;
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


            btnBrowseArticleFile.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnBrowseArticleFile.BackColor = Color.Transparent;
            btnBrowseArticleFile.BackColor = Color.FromArgb(0, 107, 163);
            btnBrowseArticleFile.ForeColor = Color.FromArgb(255, 255, 255);
            btnBrowseArticleFile.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnBrowseArticleFile.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnBrowseArticleFile.FlatAppearance.BorderSize = 0;
            btnBrowseArticleFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnCalculate.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCalculate.BackColor = Color.Transparent;
            btnCalculate.BackColor = Color.FromArgb(0, 107, 163);
            btnCalculate.ForeColor = Color.FromArgb(255, 255, 255);
            btnCalculate.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCalculate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCalculate.FlatAppearance.BorderSize = 0;
            btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnUpdateStock.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnUpdateStock.BackColor = Color.Transparent;
            btnUpdateStock.BackColor = Color.FromArgb(0, 107, 163);
            btnUpdateStock.ForeColor = Color.FromArgb(255, 255, 255);
            btnUpdateStock.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnUpdateStock.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnUpdateStock.FlatAppearance.BorderSize = 0;
            btnUpdateStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnUploadArticle.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnUploadArticle.BackColor = Color.Transparent;
            btnUploadArticle.BackColor = Color.FromArgb(0, 107, 163);
            btnUploadArticle.ForeColor = Color.FromArgb(255, 255, 255);
            btnUploadArticle.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnUploadArticle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnUploadArticle.FlatAppearance.BorderSize = 0;
            btnUploadArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            lblInvoiceAmount.BackColor = Color.FromArgb(212, 212, 212);

            lblInvoiceDate.BackColor = Color.FromArgb(212, 212, 212);
            lblInvoiceNumber.BackColor = Color.FromArgb(212, 212, 212);
            lblScanItem.BackColor = Color.FromArgb(212, 212, 212);
            lblSupplierName.BackColor = Color.FromArgb(212, 212, 212);

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



        private void gridScanArticle_CellChecked(object sender, RowColEventArgs e)
        {
            int gridRowCount = gridScanArticle.Rows.Count - 1;
            int selectCount = 0;
            for (int i = 1; i < gridScanArticle.Rows.Count; i++)
            {
                if (Convert.ToBoolean((gridScanArticle.Rows[i]["Select"])))
                {
                    selectCount = selectCount + 1;
                }
            }
            if (chkSelectAll.Checked)
            {
                GridSelect = true;
                chkSelectAll.Checked = false;
                GridSelect = false;
            }
            if (selectCount == gridRowCount)
            {
                GridSelect = false;
                chkSelectAll.Checked = true;
            }
        }

        private void frmArticleStockIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtInvoiceNumber.Focus();  //vipin
        }

        private void frmArticleStockIn_FormClosed(object sender, FormClosedEventArgs e)   // added by vipin on 13-04-2017
        {
            txtInvoiceNumber.Focus();
        }
        //code added by vipul for issue id 2834
      
        private bool validateDate()
        {
            DateTime Systemdate = Convert.ToDateTime(DateTime.Today.ToString());
            DateTime InvoiceDate = Convert.ToDateTime(dtpInvoiceDate.Value.ToString());
            if (Systemdate > InvoiceDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {

            if (!(dtpInvoiceDate.Text.ToString().Equals("")))
            {
                if (validateDate() == false)
                {
                    CommonFunc.ShowMessage("Invoice Date cannot be back dated.", MessageType.Information);
                    dtpInvoiceDate.Text = "";
                }
            }      
        }

    }
}
        #endregion

       
