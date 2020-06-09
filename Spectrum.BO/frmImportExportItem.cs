using System.IO;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spectrum.BL;
using C1.C1Excel;
using Spectrum.BL.BusinessInterface;
namespace Spectrum.BO
{
    public partial class frmImportExportItem : Spectrum.Controls.RibbonForm
    {
        private readonly int importExportItemval;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        public frmImportExportItem(int importExportItem)
        {
            InitializeComponent();
            importExportItemval = importExportItem;
            articleManager = new ArticleManager();
            this.commonManager = new CommonManager();
            this.supplierManager = new SupplierManager();
            this.taxManager = new TaxManager();
            this.siteManager = new SiteManager();
            materialManager = new MaterialManager();
        }

        private static List<ItemHierarchy> _selectedItemNode;
        OpenFileDialog openFileDialog;
        ErrorProvider articleProvider = new ErrorProvider();

        const int xlscolumnRow = 0;

        IQueryable<TaxModel> listTaxModel;
        IQueryable<ArticleTypeModel> listItemTypeModel;
        IQueryable<UoMModel> listUoMModel;
        IQueryable<EANModel> listEANModel;
        IQueryable<ManufacturerModel> listManufacturerModel;
        IQueryable<BrandModel> listBrandModel;
        IQueryable<SiteModel> listSiteModel;
        DropDownModel taxdataList;
        IQueryable<CharacteristicsModel> listCharacteristicsModel;
        IQueryable<MasterTypeModel> masterTypeList;
        IList<DropDownModel> supplier;


        ISupplierManager supplierManager;
        ICommonManager commonManager;
        ITaxManager taxManager;
        IArticleManager articleManager;
        ISiteManager siteManager;

        //code added for Material Import export functionality - ashma - 25 April 2018
        ErrorProvider materialProvider = new ErrorProvider();
        IMaterialManager materialManager;

        private void frmImportExportItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void frmImportExportItem_Load(object sender, EventArgs e)
        {
            if (CommonFunc.Themeselect == "Theme 1")
            {
                ThemeChange();
            }
            // ashma 25-4-18
            flowLayoutPanel2.Visible = false;

            grpUploadItem.Visible = false;
            grpEAXR.Visible = false;
            grpEAH.Visible = false;
            switch (importExportItemval)
            {
                case (int)CommonFunc.enumImportExportItem.UploadItem:
                    grpUploadItem.Visible = true;
                    openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "Select article file";
                    // mantis id 3276 - 4 may 2018 ashma
                    openFileDialog.Filter = "Excel xls|*.xls|Excel xlsx|*.xlsx";
                    openFileDialog.DefaultExt = ".xls";
                    openFileDialog.RestoreDirectory = true;

                    articleProvider.BlinkRate = 1000;
                    articleProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
                    //ashma 25-4-18
                    this.Size = new Size(783, 300);
                    break;
                case (int)CommonFunc.enumImportExportItem.ExportArticleXlsReport:
                    grpEAXR.Visible = true;
                    break;
                case (int)CommonFunc.enumImportExportItem.ExportArticleHierarchy:
                    grpEAH.Visible = true;
                    break;
                //ashma 25-4-18
                case (int)CommonFunc.enumImportExportItem.ImportExportMaterial:
                    openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "Select Material file";
                    // mantis id 3276 - 4 may 2018 ashma
                    openFileDialog.Filter = "Excel xls|*.xls|Excel xlsx|*.xlsx";
                    openFileDialog.DefaultExt = ".xls";
                    openFileDialog.RestoreDirectory = true;

                    materialProvider.BlinkRate = 1000;
                    materialProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
                    flowLayoutPanel1.Visible = false;
                    flowLayoutPanel2.Visible = true;
                    flowLayoutPanel2.Location = new Point(3, 3);
                    this.Size = new Size(783, 300);
                    break;
            }

        }

        #region MyRegion

        #endregion

        #region Upload_Item

        private void btnSampleUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Path.Combine(Application.StartupPath, "Resources\\Article_XLS.xls");
                System.Diagnostics.Process.Start(appPath);
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
             //   if (".xls,.xlsx".Contains(Path.GetExtension(openFileDialog.FileName)))
                // mantis id 3276 - 4 may 2018 ashma
                if (".xls,.xlsx".Contains(Path.GetExtension(openFileDialog.FileName)))
                {
                    txtArticleFilePath.ReadOnly = false;
                    txtArticleFilePath.Value = openFileDialog.FileName;
                    txtArticleFilePath.ReadOnly = true;
                }
                else
                {
                    CommonFunc.ShowMessage("Please Browse Excel File", MessageType.Information);
                    return;
                }
            }
        }

        private void btnUploadItemCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                
                if (txtArticleFilePath.Text == "")
                {
                    //CommonFunc.ShowMessage("Please Browse File", MessageType.Information);
                    if (!CommonFunc.SetErrorProvidertoControl(ref articleProvider, ref txtArticleFilePath, "Please Browse File"))
                    {
                        this.txtArticleFilePath.Focus();
                    }
                    return;
                }
                else
                {
                    C1XLBook book = new C1XLBook();
                    book.Load(Path.GetFullPath(txtArticleFilePath.Text));
                    XLSheetCollection sheets = book.Sheets;

                    //XLSheet articleSheet = sheets[0];
                    XLSheet articleDetails = null, taxDetails = null, charDetails = null, barCodeDetails = null, purchaseDetails = null;

                    AssignRespectiveSheets(sheets, ref articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails);

                    lblMsg.Text = "Please Wait...Perfoming Validations"; //vipin
                    lblMsg.Refresh();
                    System.Windows.Forms.Application.DoEvents();
                    ////// Do Validations for each Sheets ...
                    string errorMsg = string.Empty;
                    articleManager.ImportExcelValidations(ref articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails,ref errorMsg);
                     if (errorMsg == ""  || string.IsNullOrEmpty(errorMsg))
                     {
                         lblMsg.Text = "Please Wait...Importing Excel";  //vipin
                         System.Windows.Forms.Application.DoEvents();
                         lblMsg.Refresh();
                         //// Import (Save) Data ...
                         if (articleManager.SaveImportExcel(ref articleDetails, ref taxDetails, ref charDetails, ref barCodeDetails, ref purchaseDetails))
                         {
                             lblMsg.Text = "";
                             System.Windows.Forms.Application.DoEvents();
                             MessageBox.Show("Article Excel Import Successfully");
                         }
                         else
                         {
                             lblMsg.Text = "";
                             System.Windows.Forms.Application.DoEvents();  ///vipin on 10-04-2017
                             MessageBox.Show("Article Excel Import Failed !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                     }
                     else
                     {
                         lblMsg.Text = "";
                         lblMsg.Refresh();
                         System.Windows.Forms.Application.DoEvents();
                         MessageBox.Show(errorMsg);
                     }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }
        }

        private static void AssignRespectiveSheets(XLSheetCollection sheets, ref XLSheet articleDetails, ref XLSheet taxDetails, ref XLSheet charDetails, ref XLSheet barCodeDetails, ref XLSheet purchaseDetails)
        {
            ///// Get assign to all respective Sheets ...
            for (int sheetCntr = 0; sheetCntr < sheets.Count; sheetCntr++)
            {
                switch (sheets[sheetCntr].Name.ToString().Trim().ToUpper())
                {
                    case "ARTICLE DATA":
                        articleDetails = sheets[sheetCntr];
                        break;
                    case "ARTICLE TAX":
                        taxDetails = sheets[sheetCntr];
                        break;
                    case "ARTICLE CHAR":
                        charDetails = sheets[sheetCntr];
                        break;
                    case "BARCODE PRICING":
                        barCodeDetails = sheets[sheetCntr];
                        break;
                    case "ADD. PURCHASE UOMS":
                        purchaseDetails = sheets[sheetCntr];
                        break;
                    default:
                        switch (sheetCntr)
                        {
                            case 0:
                                articleDetails = sheets[sheetCntr];
                                break;
                            case 1:
                                taxDetails = sheets[sheetCntr];
                                break;
                            case 2:
                                charDetails = sheets[sheetCntr];
                                break;
                            case 3:
                                barCodeDetails = sheets[sheetCntr];
                                break;
                            case 4:
                                purchaseDetails = sheets[sheetCntr];
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }


        //private void fillArticleBasicDataToModel()
        //{
        //    try
        //    {
        //        articleModel = new ArticleModel();
        //        this.articleModel.ArticleCode = string.IsNullOrEmpty(txtArticleCode.Text) ? "" : txtArticleCode.Text.Trim();
        //        this.articleModel.IsAutoNumber = string.IsNullOrEmpty(txtArticleCode.Text) ? true : false;

        //        this.articleModel.ArticalTypeCode = (cboItemType.SelectedValue != null) ? cboItemType.SelectedValue.ToString() : "";
        //        this.articleModel.LegacyArticleCode = txtLegecyCode.Text.Trim();
        //        this.articleModel.ArticleName = txtItemDescription.Text.Trim();
        //        this.articleModel.ArticleShortName = txtItemShortName.Text.Trim();
        //        this.articleModel.Salable = (rdoSaleableYes.Checked) ? true : false;
        //        this.articleModel.ArticleActive = (rdoStatusActive.Checked) ? true : false;
        //        this.articleModel.isExpiry = (rdoExpirableYes.Checked) ? true : false;
        //        this.articleModel.Printable = (rdoPrintableYes.Checked) ? true : false;
        //        this.articleModel.IsMrpOpen = (rdoOpenMRPYes.Checked) ? true : false;
        //        this.articleModel.ToleranceValue = (rdoOpenQtyYes.Checked) ? true : false;

        //        this.articleModel.ParentArt = string.IsNullOrEmpty(txtParentItemCode.Text) ? "" : txtParentItemCode.Text.Trim();
        //        this.articleModel.TreeID = string.IsNullOrEmpty(txtItemTree.Text) ? "" : txtItemTree.Text.Trim();
        //        this.articleModel.LastNodeCode = string.IsNullOrEmpty(txtLastNodeCode.Tag.ToString()) ? "" : txtLastNodeCode.Tag.ToString();


        //        this.articleModel.ManufacturerCode = (cboManufacturer.SelectedValue != null) ? cboManufacturer.SelectedValue.ToString() : "";
        //        this.articleModel.BrandCode = (cboBrand.SelectedValue != null) ? cboBrand.SelectedValue.ToString() : "";


        //        this.articleModel.NetWeight = decimal.Parse(string.IsNullOrEmpty(txtNetWeight.Text.Trim()) ? "0" : txtNetWeight.Text.Trim());
        //        this.articleModel.GrossWeight = decimal.Parse(string.IsNullOrEmpty(txtGrossWeight.Text.Trim()) ? "0" : txtGrossWeight.Text.Trim());
        //        this.articleModel.BaseUnitofMeasure = (cboBaseUnitMeasure.SelectedValue != null) ? cboBaseUnitMeasure.SelectedValue.ToString() : "";
        //        this.articleModel.OrderUnitofMeasure = (cboOrderUnitMeasure.SelectedValue != null) ? cboOrderUnitMeasure.SelectedValue.ToString() : "";
        //        this.articleModel.VolumeUOM = string.IsNullOrEmpty(txtOrderValue.Text) ? "" : txtOrderValue.Text.Trim();

        //        this.articleModel.CostPrice = decimal.Parse(string.IsNullOrEmpty(txtCostPrice.Text.Trim()) ? "0" : txtCostPrice.Text.Trim());
        //        this.articleModel.SellPrice = decimal.Parse(string.IsNullOrEmpty(txtSalePrice.Text.Trim()) ? "0" : txtSalePrice.Text.Trim());
        //        this.articleModel.MRP = decimal.Parse(string.IsNullOrEmpty(txtMRP.Text.Trim()) ? "0" : txtMRP.Text.Trim());
        //        this.articleModel.Margin = decimal.Parse(string.IsNullOrEmpty(txtMargin.Text.Trim()) ? "0" : txtMargin.Text.Trim());


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private void fillArticleEANDataToModel()
        //{
        //    try
        //    {
        //        eanModel = new List<EANModel>();
        //        for (int rowBarCode = 1; rowBarCode < dgBarCode.Rows.Count; rowBarCode++)
        //        {
        //            this.eanModel.Add(
        //                new EANModel
        //                {
        //                    EAN = dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeValue].ToString(),
        //                    Discription = dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeType].ToString(),
        //                    IsAutoNumber = string.IsNullOrEmpty(dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeValue].ToString()) ? true : false,
        //                    DefaultEAN = (bool)true//dgBarCode.Rows[rowBarCode][(int)enumBarCode.isdefault]
        //                });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void fillArticleTaxDataToModel()
        //{
        //    try
        //    {
        //        siteArticleTaxMappingModel = new List<SiteArticleTaxMappingModel>();
        //        for (int rowTaxCode = 1; rowTaxCode < dgTax.Rows.Count; rowTaxCode++)
        //        {
        //            this.siteArticleTaxMappingModel.Add(
        //                new SiteArticleTaxMappingModel
        //                {
        //                    TaxName = dgTax.Rows[rowTaxCode][(int)enumTax.tax].ToString(),
        //                    TaxCode = dgTax.Rows[rowTaxCode][(int)enumTax.taxValue].ToString(),
        //                    SupplierCode = CommonFunc.DefaultSupplier
        //                });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void fillArticleCharacteristicDataToModel()
        //{
        //    try
        //    {
        //        articleCharacteristicMatrixModel = new List<ArticleCharacteristicMatrixModel>();
        //        string defaultProfileName = CommonFunc.GetEnumStringValue(enumCharacteristics.defaultProfileName);
        //        string colourName = CommonFunc.GetEnumStringValue(enumCharacteristics.colour);
        //        string sizeName = CommonFunc.GetEnumStringValue(enumCharacteristics.size);
        //        string styleName = CommonFunc.GetEnumStringValue(enumCharacteristics.style);
        //        string fabricName = CommonFunc.GetEnumStringValue(enumCharacteristics.fabric);

        //        // COLOR ...

        //        if (cboColour.SelectedIndex != -1)
        //        {
        //            this.articleCharacteristicMatrixModel.Add(
        //                new ArticleCharacteristicMatrixModel
        //                {
        //                    ProfileCode = defaultProfileName,
        //                    CharType = true,
        //                    CharCode = colourName,
        //                    CharValue = (cboColour.SelectedValue != null) ? cboColour.SelectedValue.ToString() : "",
        //                    RowKey = 1
        //                });
        //        }

        //        // SIZE ...
        //        if (cboSize.SelectedIndex != -1)
        //        {
        //            this.articleCharacteristicMatrixModel.Add(
        //                    new ArticleCharacteristicMatrixModel
        //                    {
        //                        ProfileCode = defaultProfileName,
        //                        CharType = true,
        //                        CharCode = sizeName,
        //                        CharValue = (cboSize.SelectedValue != null) ? cboSize.SelectedValue.ToString() : "",
        //                        RowKey = 1
        //                    });
        //        }

        //        // STYLE ...
        //        if (cboStyle.SelectedIndex != -1)
        //        {
        //            this.articleCharacteristicMatrixModel.Add(
        //                    new ArticleCharacteristicMatrixModel
        //                    {
        //                        ProfileCode = defaultProfileName,
        //                        CharType = true,
        //                        CharCode = styleName,
        //                        CharValue = (cboStyle.SelectedValue != null) ? cboStyle.SelectedValue.ToString() : "",
        //                        RowKey = 1

        //                    });
        //        }

        //        // FABRIC ...
        //        if (cboFabric.SelectedIndex != -1)
        //        {
        //            this.articleCharacteristicMatrixModel.Add(
        //                    new ArticleCharacteristicMatrixModel
        //                    {
        //                        ProfileCode = defaultProfileName,
        //                        CharType = true,
        //                        CharCode = fabricName,
        //                        CharValue = (cboFabric.SelectedValue != null) ? cboFabric.SelectedValue.ToString() : "",
        //                        RowKey = 1
        //                    });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #endregion

        #region Export_Article_XLS_Report

        private void txtEAXRItemHierarchy_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    using (frmItemHierarchyPopup objItemHierarchyPopup = new frmItemHierarchyPopup())
            //    {
            //        frmItemHierarchyPopup.ShowCheckBox = false;
            //        if (objItemHierarchyPopup.ShowDialog() == DialogResult.OK)
            //        {
            //            _selectedItemNode = (List<ItemHierarchy>)objItemHierarchyPopup.selectedItemNode;
            //            if (_selectedItemNode != null)
            //            {
            //                if (_selectedItemNode[0].NodeName.Equals("CCE"))
            //                {
            //                    MessageBox.Show("Please Select Valid Hierarchy");
            //                    return;
            //                }
            //                txtEAXRItemHierarchy.Text = _selectedItemNode[0].NodeName;
            //                txtEAXRItemHierarchy.Tag = _selectedItemNode[0].Nodecode;
            //            }
            //        }
            //    }
            //}

            try
            {
              GotoPopUp:  using (frmItemHierarchyPopup objItemHierarchyPopup = new frmItemHierarchyPopup())  // added by vipin on 30-03-2017
                {
                    frmItemHierarchyPopup.ShowCheckBox = false;
                    if (objItemHierarchyPopup.ShowDialog() == DialogResult.OK)
                    {
                        _selectedItemNode = (List<ItemHierarchy>)objItemHierarchyPopup.selectedItemNode;
                        if (_selectedItemNode.Count > 0)
                        {
                            if (_selectedItemNode != null)
                            {
                                //if (_selectedItemNode[0] != null) //vipin1
                                //{
                                if (_selectedItemNode[0].NodeName.Equals("CCE"))
                                {
                                    MessageBox.Show("Please Select Valid Hierarchy");
                                    return;


                                }
                                txtEAXRItemHierarchy.ReadOnly = false;
                                txtEAXRItemHierarchy.Text = _selectedItemNode[0].NodeName;
                                txtEAXRItemHierarchy.Tag = _selectedItemNode[0].Nodecode;
                                _selectedItemNode.Clear();
                                txtEAXRItemHierarchy.ReadOnly = true;
                                // }
                                //else
                                //{
                                //    txtEAXRItemHierarchy.Enabled = false;
                                //}
                            }
                        }
                        else
                        {
                            goto GotoPopUp;
                        }



                    }
                    else
                    {
                        this.BeginInvoke(new MethodInvoker(this.Close)); 
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void btnEAXRExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEAXRItemHierarchy.Text.ToString()))
                {

                    // Set cursor as hourglass
                    Cursor.Current = Cursors.WaitCursor;
                    DialogResult result = fbd.ShowDialog();
                    ArticleManager articleHierarchyManager = new ArticleManager();
                    ArticleDataExportModel articleDataExportModel = articleHierarchyManager.GetArticleExportData((string)txtEAXRItemHierarchy.Tag, CommonModel.SiteCode);
                    if (result == DialogResult.OK)
                    {
                        //ConvertListToExcel.ToExcelNoInterop( articleDataExportModel,Path.Combine(fbd.SelectedPath, "ArticelHierarchy.csv").ToString(),"Nodecode,NodeName,TreeCode", "", "");
                        //ConvertListToExcel.ToExcel(EAH, "", "");
                       // string path = Path.Combine(fbd.SelectedPath, " ArticelHierarchy " + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString();

                        string path = Path.Combine(fbd.SelectedPath, " ArticleExport " + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString();  //vipin on 02-04-2016

                        if (ConvertListsToExcel.ArticleDataExportListToExcel(articleDataExportModel, path))
                        {
                            MessageBox.Show(" Export Article XLS Report " + path + " Successfully ");
                        }
                        else
                        {
                            MessageBox.Show(" Export Article XLS Report failed ");
                        }
                    }
                    // Set cursor as default arrow
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    MessageBox.Show(" please choose Export Article XLS ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEAXRCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Export_Article_Hierarchy

        private void txtEAHItemHierarchy_Enter(object sender, EventArgs e)
        {
            string treeCode, nodeCode;
            try
            {
        GotoPopUp:  using (frmItemHierarchyPopup objItemHierarchyPopup = new frmItemHierarchyPopup())
                {
                    frmItemHierarchyPopup.ShowCheckBox = true;
                    objItemHierarchyPopup.selectedItemNode = _selectedItemNode;
                    if (objItemHierarchyPopup.ShowDialog() == DialogResult.OK)
                    {

                            _selectedItemNode = (List<ItemHierarchy>)objItemHierarchyPopup.selectedItemNode;
                            // if (_selectedItemNode != null)
                            if (_selectedItemNode != null && _selectedItemNode.Count() > 0)  // vipin on 27.03.2017
                            {
                                //if (_selectedItemNode[0].NodeName == null)
                                //{
                                //    CommonFunc.ShowMessage(" Please Select Valid Hierarchy", MessageType.Information);
                                //    return;
                                //}
                                txtEAHItemHierarchy.ReadOnly = false;
                                txtEAHItemHierarchy.Text = _selectedItemNode[0].NodeName;
                                txtEAHItemHierarchy.Tag = _selectedItemNode[0].Nodecode;
                                txtEAHItemHierarchy.ReadOnly = true;
                                treeCode = _selectedItemNode[0].TreeCode;
                                if (treeCode == null)
                                {
                                    nodeCode = _selectedItemNode[0].Nodecode;
                                    if (nodeCode == null)
                                    {
                                        // nodeCode = mstArticleEntity.getArticleNodeCodeName();
                                    }
                                }
                                _selectedItemNode[0].Nodecode = "";
                            }
                            else
                            {
                                CommonFunc.ShowMessage(" Please Select Hierarchy", MessageType.Information);
                                goto GotoPopUp;
                            }


                    }
                    else
                    {
                        this.BeginInvoke(new MethodInvoker(this.Close)); 
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }


        private void btnEAHExportLastNode_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();
            ArticleHierarchyManager articleHierarchyManager = new ArticleHierarchyManager();
            //            Last Node Code	Last Node Code Name	Parent Node Code	Parent Node Code Name	Hierarchy

            var EAHLastNode = (from var in _selectedItemNode
                               where var.ISThisLastNode == true
                               select new
                               {
                                   LastNodeCode = var.Nodecode,
                                   LastNodeCodeName = var.NodeName,
                                   ParentNodeCode = var.ParentNodecode,
                                   ParentNodeCodeName = var.ParentNodeName,
                                   Hierarchy = articleHierarchyManager.GetArticleHierarchyString(var.Nodecode, true).ToString()
                               }).ToList();

            if (result == DialogResult.OK)
            {
                //ConvertListToExcel.ToExcel(EAH, "", "");
                ConvertListToExcel.ToExcelNoInterop(EAHLastNode, Path.Combine(fbd.SelectedPath, "ArticelLastNodeHierarchy.csv"), "", "");
            }
        }

        private void btnEAHExport_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();

            //var EAH = (from var in _selectedItemNode select new{NodeCode = var.Nodecode,NodeName = var.NodeName,TreeCode = var.TreeCode,LanguageCode = CommonFunc.LanguageCode }).ToList();
            if (result == DialogResult.OK)
            {
                //ConvertListToExcel.ToExcel(EAH, "", "");
                ConvertListToExcel.ToExcelNoInterop(_selectedItemNode, Path.Combine(fbd.SelectedPath, "ArticelHierarchy.csv"), "Nodecode,NodeName,TreeCode", "");
            }
        }

        private void btnEAHCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }

        }

        #endregion
        public void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);

            btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnBrowse.BackColor = Color.Transparent;
            btnBrowse.BackColor = Color.FromArgb(0, 107, 163);
            btnBrowse.ForeColor = Color.FromArgb(255, 255, 255);
            btnBrowse.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnEAHCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEAHCancel.BackColor = Color.Transparent;
            btnEAHCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnEAHCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnEAHCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEAHCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEAHCancel.FlatAppearance.BorderSize = 0;
            btnEAHCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnEAHExport.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEAHExport.BackColor = Color.Transparent;
            btnEAHExport.BackColor = Color.FromArgb(0, 107, 163);
            btnEAHExport.ForeColor = Color.FromArgb(255, 255, 255);
            btnEAHExport.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEAHExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEAHExport.FlatAppearance.BorderSize = 0;
            btnEAHExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnEAXRCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEAXRCancel.BackColor = Color.Transparent;
            btnEAXRCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnEAXRCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnEAXRCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEAXRCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEAXRCancel.FlatAppearance.BorderSize = 0;
            btnEAXRCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnEAXRExport.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEAXRExport.BackColor = Color.Transparent;
            btnEAXRExport.BackColor = Color.FromArgb(0, 107, 163);
            btnEAXRExport.ForeColor = Color.FromArgb(255, 255, 255);
            btnEAXRExport.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEAXRExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEAXRExport.FlatAppearance.BorderSize = 0;
            btnEAXRExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            btnSampleUploadFile.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSampleUploadFile.BackColor = Color.Transparent;
            btnSampleUploadFile.BackColor = Color.FromArgb(0, 107, 163);
            btnSampleUploadFile.ForeColor = Color.FromArgb(255, 255, 255);
            btnSampleUploadFile.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSampleUploadFile.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSampleUploadFile.FlatAppearance.BorderSize = 0;
            btnSampleUploadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnUpload.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnUpload.BackColor = Color.Transparent;
            btnUpload.BackColor = Color.FromArgb(0, 107, 163);
            btnUpload.ForeColor = Color.FromArgb(255, 255, 255);
            btnUpload.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnUpload.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnUploadItemCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnUploadItemCancel.BackColor = Color.Transparent;
            btnUploadItemCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnUploadItemCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnUploadItemCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnUploadItemCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnUploadItemCancel.FlatAppearance.BorderSize = 0;
            btnUploadItemCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnEAHExportLastNode.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEAHExportLastNode.BackColor = Color.Transparent;
            btnEAHExportLastNode.BackColor = Color.FromArgb(0, 107, 163);
            btnEAHExportLastNode.ForeColor = Color.FromArgb(255, 255, 255);
            btnEAHExportLastNode.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEAHExportLastNode.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEAHExportLastNode.FlatAppearance.BorderSize = 0;
            btnEAHExportLastNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            grpEAH.BackColor = Color.FromArgb(134, 134, 134);
            grpEAXR.BackColor = Color.FromArgb(134, 134, 134);

            grpUploadItem.BackColor = Color.FromArgb(134, 134, 134);

            grpEAH.ForeColor = Color.White;
            grpEAXR.ForeColor = Color.White;
            grpUploadItem.ForeColor = Color.White;

            flowLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134);
            lblSelectItemHierarchy1.BackColor = Color.FromArgb(212, 212, 212);

            lblSelectItemHierarchy2.BackColor = Color.FromArgb(212, 212, 212);
            lblUploadItems.BackColor = Color.FromArgb(212, 212, 212);
            lblSelectItemHierarchy1.BackColor = Color.FromArgb(212, 212, 212);
            lblSelectItemHierarchy1.BackColor = Color.FromArgb(212, 212, 212);

            //4 may 2018 ashma
            #region Import Export Material
            btnBrowseMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnBrowseMaterial.BackColor = Color.Transparent;
            btnBrowseMaterial.BackColor = Color.FromArgb(0, 107, 163);
            btnBrowseMaterial.ForeColor = Color.FromArgb(255, 255, 255);
            btnBrowseMaterial.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnBrowseMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnBrowseMaterial.FlatAppearance.BorderSize = 0;
            btnBrowseMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnUploadMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnUploadMaterial.BackColor = Color.Transparent;
            btnUploadMaterial.BackColor = Color.FromArgb(0, 107, 163);
            btnUploadMaterial.ForeColor = Color.FromArgb(255, 255, 255);
            btnUploadMaterial.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnUploadMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnUploadMaterial.FlatAppearance.BorderSize = 0;
            btnUploadMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSampleUploadMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSampleUploadMaterial.BackColor = Color.Transparent;
            btnSampleUploadMaterial.BackColor = Color.FromArgb(0, 107, 163);
            btnSampleUploadMaterial.ForeColor = Color.FromArgb(255, 255, 255);
            btnSampleUploadMaterial.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSampleUploadMaterial.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSampleUploadMaterial.FlatAppearance.BorderSize = 0;
            btnSampleUploadMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnMaterialExport.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnMaterialExport.BackColor = Color.Transparent;
            btnMaterialExport.BackColor = Color.FromArgb(0, 107, 163);
            btnMaterialExport.ForeColor = Color.FromArgb(255, 255, 255);
            btnMaterialExport.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnMaterialExport.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnMaterialExport.FlatAppearance.BorderSize = 0;
            btnMaterialExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            #endregion
        }
        #region Material Import Export - ashma - 25-4-18
        //private static void AssignRespectiveSheets(XLSheetCollection sheets, ref XLSheet articleLblPrint, ref XLSheet material, ref XLSheet materialArticleMap)
        //{
        //    ///// Get assign to all respective Sheets ...
        //    for (int sheetCntr = 0; sheetCntr < sheets.Count; sheetCntr++)
        //    {
        //        switch (sheets[sheetCntr].Name.ToString().Trim().ToUpper())
        //        {
        //            case "ARTICLE PRINT":
        //                articleLblPrint = sheets[sheetCntr];
        //                break;
        //            case "MATERIAL":
        //                material = sheets[sheetCntr];
        //                break;
        //            case "MATERIAL ARTICLE MAP":
        //                materialArticleMap = sheets[sheetCntr];
        //                break;
        //            default:
        //                switch (sheetCntr)
        //                {
        //                    case 0:
        //                        articleLblPrint = sheets[sheetCntr];
        //                        break;
        //                    case 1:
        //                        material = sheets[sheetCntr];
        //                        break;
        //                    case 2:
        //                        materialArticleMap = sheets[sheetCntr];
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                break;
        //        }
        //    }
        //}

        private static void AssignRespectiveSheets(XLSheetCollection sheets, ref XLSheet material, ref XLSheet materialArticleMap)
        {
            //ashm 3 may 2018 - merging excel sheets
            ///// Get assign to all respective Sheets ...
            for (int sheetCntr = 0; sheetCntr < sheets.Count; sheetCntr++)
            {
                switch (sheets[sheetCntr].Name.ToString().Trim().ToUpper())
                {
                    case "MATERIAL":
                        material = sheets[sheetCntr];
                        break;
                    case "MATERIAL ARTICLE MAP":
                        materialArticleMap = sheets[sheetCntr];
                        break;
                    default:
                        switch (sheetCntr)
                        {
                            case 0:
                                material = sheets[sheetCntr];
                                break;
                            case 1:
                                materialArticleMap = sheets[sheetCntr];
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }
        private void btnBrowseMaterial_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // mantis id 3276 - 4 may 2018 ashma
                if (".xls,.xls".Contains(Path.GetExtension(openFileDialog.FileName)))
                {
                    txtMaterialFilePath.ReadOnly = false;
                    txtMaterialFilePath.Value = openFileDialog.FileName;
                    txtMaterialFilePath.ReadOnly = true;
                }
                else
                {
                    CommonFunc.ShowMessage("Please Browse Excel File", MessageType.Information);
                    return;
                }
            }
        }

        private void btnSampleUploadMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Path.Combine(Application.StartupPath, "Resources\\Material_XLS.xls");
                System.Diagnostics.Process.Start(appPath);
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                if (txtMaterialFilePath.Text == "")
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref materialProvider, ref txtMaterialFilePath, "Please Browse File"))
                    {
                        this.txtMaterialFilePath.Focus();
                    }
                    return;
                }
                else
                {
                    C1XLBook book = new C1XLBook();
                    book.Load(Path.GetFullPath(txtMaterialFilePath.Text));
                    XLSheetCollection sheets = book.Sheets;

                    //XLSheet articleLblPrint = null, material = null, materialArticleMap = null;
                    //AssignRespectiveSheets(sheets, ref articleLblPrint, ref material, ref materialArticleMap);

                    //ashma 3 may 2018 - merging excel sheets
                    XLSheet material = null, materialArticleMap = null;
                    AssignRespectiveSheets(sheets, ref material, ref materialArticleMap);

                    lblMsg.Text = "Please Wait...Perfoming Validations";
                    lblMsg.Refresh();
                    System.Windows.Forms.Application.DoEvents();
                    ////// Do Validations for each Sheets ...
                    string errorMsg = string.Empty;
                    //string path = "E:\\ashma";
                    C1XLBook failedMaterialbook = new C1XLBook();
                    if (materialManager.Save(ref material, ref materialArticleMap, ref failedMaterialbook))
                    {
                        //check failedMaterialBook is null
                        if (failedMaterialbook.Sheets[0].Rows.Count > 1 || failedMaterialbook.Sheets[1].Rows.Count > 1)
                        {
                            DialogResult result = fbd.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                string failedMaterialpath = Path.Combine(fbd.SelectedPath, " FailedMaterialExport " + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString();
                                failedMaterialbook.Save(failedMaterialpath);
                                System.Windows.Forms.Application.DoEvents();
                                MessageBox.Show("Failed Material Excel Exported");
                                this.Dispose();
                                this.Close();
                            }
                        }
                        else
                        {
                            System.Windows.Forms.Application.DoEvents();
                            MessageBox.Show("Material Excel Imported Successfully");
                            this.Dispose();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }
        }

        //private void btnUploadMaterialCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
        //        {
        //            this.Dispose();
        //            this.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex, Logger.LogingLevel.Error);
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnMaterialExport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string path = Path.Combine(fbd.SelectedPath, " MaterialExport " + DateTime.Now.ToString("dd-MM-yyyy-hhmm") + ".xls").ToString(); 

                    //List<Spectrum.DAL.ArticlelblPrintDtl> objArticleLblPrint = null;
                    List<Spectrum.DAL.MstMaterial> objMaterial = null;
                    List<Spectrum.DAL.MstMaterialArticleMap> objMaterialArticleMap = null;
                    
                    //if (materialManager.ExportToExcel(ref  objArticleLblPrint, ref  objMaterial, ref  objMaterialArticleMap, ref path))
                    if (materialManager.ExportToExcel(ref  objMaterial, ref  objMaterialArticleMap, ref path))
                    {
                        lblMsg.Text = "";
                        System.Windows.Forms.Application.DoEvents();
                        MessageBox.Show("Material Excel Exported Successfully");
                        this.Dispose();
                        this.Close();
                    }
                    else
                    {
                        lblMsg.Text = "";
                        System.Windows.Forms.Application.DoEvents();
                        MessageBox.Show("Material Excel Export Failed !!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void btnMaterialExportCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
        //        {
        //            this.Dispose();
        //            this.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex, Logger.LogingLevel.Error);
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion
    }
}



#region "delete"

//private void FillMasterData()
//  {
//      fillSupplierList();
//      fillTaxMst();
//      fillBarCode();
//      fillItemTypeMst();
//      fillUoMMst();
//      fillManufacturerMst();
//      fillCharacteristicsMst();
//      fillEANMst();
//      fillSiteList();
//  }

//  private void fillSupplierList()
//  {
//      try
//      {
//          this.supplier = (from result in this.supplierManager.GetSupplierList()
//                           select new DropDownModel
//                           {
//                               Code = result.Code,
//                               Description = result.Name
//                           }).ToList();
//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }
//  }

//  private void fillSiteList()
//  {
//      try
//      {
//          listSiteModel = this.siteManager.GetSiteList();
//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }
//  }

//  private void fillTaxMst()
//  {
//      try
//      {
//          listTaxModel = taxManager.GetTaxList();
//      }
//      catch (Exception ex)
//      {
//          CommonFunc.ShowMessage(ex.Message, MessageType.Information);
//          Logger.Log(ex.Message, Logger.LogingLevel.Error);
//      }

//  }

//  private void fillBarCode()
//  {
//      try
//      {
//          masterTypeList = commonManager.GetMasterTypeList();
//          //var barCodeModesList = (from m in masterTypeList
//          //                        where m.CodeType == "site.defaultEan"
//          //                        select new DropDownModel { Code = m.Code, Description = m.ShortDesc }).ToList();



//      }
//      catch (Exception)
//      {

//          throw;
//      }
//  }

//  private void fillItemTypeMst()
//  {
//      try
//      {
//          listItemTypeModel = commonManager.GetArticleTypeList();
//          //var itemTypedataList = (from t in listItemTypeModel
//          //                        select (
//          //                        new DropDownModel { Code = t.ArticalTypeCode, Description = t.ArticalTypeName }
//          //                        )).ToList();

//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }
//  }

//  private void fillUoMMst()
//  {
//      try
//      {
//          listUoMModel = commonManager.GetUoMList();
//          //var uoMdataList = (from t in listUoMModel
//          //                   select (
//          //                   new DropDownModel { Code = t.UOMCode, Description = t.UOM }
//          //                   )).ToList();


//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }

//  }

//  private void fillEANMst()
//  {
//      try
//      {
//          listEANModel = articleManager.GetArticleEAN();
//          //var uoMdataList = (from t in listUoMModel
//          //                   select (
//          //                   new DropDownModel { Code = t.UOMCode, Description = t.UOM }
//          //                   )).ToList();


//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }

//  }

//  private void fillManufacturerMst()
//  {
//      try
//      {
//          listManufacturerModel = commonManager.GetManufacturerList();
//          //var manufacturerdataList = (from t in listManufacturerModel
//          //                            select (
//          //                            new DropDownModel { Code = t.ManufacturerCode, Description = t.ManufacturerName }
//          //                            )).ToList();



//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }

//  }

//  private void fillCharacteristicsMst()
//  {
//      try
//      {
//          listCharacteristicsModel = commonManager.GetCharacteristicsList();

//          //string defaultProfileName = CommonFunc.GetEnumStringValue(enumCharacteristics.defaultProfileName);
//          //string colourName = CommonFunc.GetEnumStringValue(enumCharacteristics.colour);
//          //string sizeName = CommonFunc.GetEnumStringValue(enumCharacteristics.size);
//          //string styleName = CommonFunc.GetEnumStringValue(enumCharacteristics.style);
//          //string fabricName = CommonFunc.GetEnumStringValue(enumCharacteristics.fabric);

//          //var colourList = (from t in listCharacteristicsModel
//          //                  where t.ProfileName == defaultProfileName && t.CharName == colourName
//          //                  select (
//          //                       new DropDownModel { Code = t.CharValue, Description = t.CharValue }
//          //                          )).ToList();


//          //var sizeList = (from t in listCharacteristicsModel
//          //                where t.ProfileName == defaultProfileName && t.CharName == sizeName
//          //                select (
//          //                new DropDownModel { Code = t.CharValue, Description = t.CharValue }
//          //                )).ToList();

//          //          var styleList = (from t in listCharacteristicsModel
//          //                 where t.ProfileName == defaultProfileName && t.CharName == styleName
//          //                 select (
//          //                 new DropDownModel { Code = t.CharValue, Description = t.CharValue }
//          //                 )).ToList();

//          //var fabricList = (from t in listCharacteristicsModel
//          //                  where t.ProfileName == defaultProfileName && t.CharName == fabricName
//          //                  select (
//          //                  new DropDownModel { Code = t.CharValue, Description = t.CharValue }
//          //                  )).ToList();

//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }

//  }

//  private void fillBrandMst()
//  {
//      try
//      {
//          listBrandModel = commonManager.GetBrandList();
//          //var branddataList = (from t in listBrandModel
//          //                     where t.ManufacturerCode == cboManufacturer.SelectedValue.ToString()
//          //                     select (
//          //                     new DropDownModel { Code = t.BrandCode, Description = t.BrandName }
//          //                     )).ToList();

//      }
//      catch (Exception ex)
//      {
//          throw ex;
//      }

//  }

#endregion 