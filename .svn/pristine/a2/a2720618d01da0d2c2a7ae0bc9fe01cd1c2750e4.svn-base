using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Spectrum.Models;
using Spectrum.Logging;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL;
using System.Collections;
using C1.Win.C1FlexGrid;
using Spectrum.Models.Enums;
using System.Text.RegularExpressions;
using Spectrum.DAL.CustomEntities;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
namespace Spectrum.BO
{
    public partial class frmItemMaster : Spectrum.Controls.RibbonForm
    {
      //  private BackgroundWorker myWorker = new BackgroundWorker();
        bool isSalePriceValid = false;
        public frmItemMaster()
        {
            InitializeComponent();


            //myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);   //vipin
            //myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
            //myWorker.ProgressChanged += new ProgressChangedEventHandler(myWorker_ProgressChanged);
            //myWorker.WorkerReportsProgress = true;
            //myWorker.WorkerSupportsCancellation = true;


            articleManager = new ArticleManager();
            this.commonManager = new CommonManager();
            this.supplierManager = new SupplierManager();
            this.taxManager = new TaxManager();
            articleManager.defaultProfileName = CommonFunc.GetEnumStringValue(enumCharacteristics.defaultProfileName);
        }

        #region "Class Variables Declaration"

        ItemHierarchy itemHierarchy;
        IArticleManager articleManager;

        ISupplierManager supplierManager;
        ICommonManager commonManager;
        ITaxManager taxManager;
        //SupplierModel supplierModel;
        IList<DropDownModel> supplier;
        public List<ItemHierarchy> _selectedItemNode;
        //IList<DropDownModel> leftSupplierList, rightSupplierList;
        List<DropDownModel> leftSupplierList = new List<DropDownModel>();
        List<DropDownModel> rightSupplierList = new List<DropDownModel>();

        TaxModel taxModel;
        IQueryable<TaxModel> listTaxModel;
        IQueryable<ArticleTypeModel> listItemTypeModel;
        IQueryable<UoMModel> listUoMModel;
        IQueryable<ManufacturerModel> listManufacturerModel;
        IQueryable<BrandModel> listBrandModel;
        DropDownModel taxdataList;
        IQueryable<CharacteristicsModel> listCharacteristicsModel;
        IQueryable<MasterTypeModel> masterTypeList;

        ArticleModel articleModel;
        ArticleImageModel articleImageModel;
        List<EANModel> eanModel;
        List<SiteArticleTaxMappingModel> siteArticleTaxMappingModel;
        List<ArticleCharacteristicMatrixModel> articleCharacteristicMatrixModel;
        MasterArticleMapModel masterArticleMapModel;

        public string action { get; set; }
        private string editArticleCode = string.Empty;

        private ToolTip ttItemName = null;
        Boolean IsHirarchyPopupShow = false;
        private string LastSupllierAddRemoved = string.Empty;

        enum enumTax
        {
            select,
            taxName,
            taxValue,
            isInclusive,
            taxCode
        }

        enum enumBarCode
        {
            select,
            barCodeType,
            barCodeValue,
            isdefault,
            isAuto
        }

        enum enumTabs
        {
            basic,
            barCode,
            tax,
            supplier
        }

        #endregion

        private void frmItemMaster_Load(object sender, EventArgs e)
        {
            try
            {

            

                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                txtLegecyCode.KeyPress += new KeyPressEventHandler(txtValidateSpecialCharSpace);
                txtArticleCode.KeyPress += new KeyPressEventHandler(txtValidateSpecialCharSpace);
                txtOrderValue.KeyPress += new KeyPressEventHandler(txtOrderValue_KeyPress);

                //code added by irfan spectrum lite issue
                ////txtMRP.Enabled = false;
                ////txtSalePrice.Enabled = false;
                //=======================================

                setInitialFormsValidations();
             //   myWorker.RunWorkerAsync(); //vipin

                this.supplier = (from result in this.supplierManager.GetSupplierList() // put in BackGround Worker
                                 select new DropDownModel
                                 {
                                     Code = result.Code,
                                     Description = result.Name
                                 }).ToList();

                leftSupplierList.AddRange(supplier);

                fillSupplierList();
                fillTaxMst();
                fillBarCode();
                fillItemTypeMst();
                fillUoMMst();
                fillManufacturerMst();
                fillCharacteristicsMst();

   

                rdoSaleableYes.Checked = true;
                rdoStatusActive.Checked = true;
                rdoExpirableNo.Checked = true;
                rdoPrintableYes.Checked = true;
                rdoOpenMRPNo.Checked = true;
                rdoOpenQtyNo.Checked = true;

                txtLastNodeCode_Enter(sender, e);
             
#if DEBUG
                //    fillTestData();
#endif

            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }
        //protected void myWorker_DoWork(object sender, DoWorkEventArgs e)  //vipin
        //{
        //    this.supplier = (from result in this.supplierManager.GetSupplierList() 
        //                     select new DropDownModel
        //                     {
        //                         Code = result.Code,
        //                         Description = result.Name
        //                     }).ToList();
        //    leftSupplierList.AddRange(supplier);

        //}
        //protected void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{

        //}
        //protected void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //}


        private void frmItemMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ProcessTabKey(true);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
            }
        }

        private void fillTestData()
        {
            try
            {
                Random r = new Random();
                int rno = r.Next(100000);

                txtArticleCode.Value = "testa1" + rno.ToString();
                cboItemType.SelectedValue = "Loose";
                txtLegecyCode.Value = "l1" + rno.ToString();
                txtItemDescription.Value = "test21" + rno.ToString();
                txtItemShortName.Value = "test short" + rno.ToString();

                txtParentItemCode.Value = "Toothpaste";
                txtItemTree.Value = "2";
                txtLastNodeCode.Value = "Colgate";
                txtLastNodeCode.Tag = "ANCCCE000000184";

                cboManufacturer.SelectedValue = "messi";
                cboBrand.SelectedValue = "test";

                txtNetWeight.Value = 10;
                txtGrossWeight.Value = 20;
                cboBaseUnitMeasure.SelectedValue = "BAG";
                cboOrderUnitMeasure.SelectedValue = "CAS";
                txtOrderValue.Value = "10";

                txtCostPrice.Value = 100;
                txtSalePrice.Value = 150;
                txtMRP.Value = 150;
                txtMargin.Value = 50;
                try
                {
                    cboColour.SelectedIndex = 0;
                    cboSize.SelectedIndex = 0;
                    cboFabric.SelectedIndex = 0;
                    cboStyle.SelectedIndex = 0;

                    txtItemImage.Value = "C:\\images\\Amaranth.jpg";
                    this.articleImageModel = new ArticleImageModel();
                    this.articleImageModel.ArticleImage = "Amaranth.jpg";
                    this.articleImageModel.sourcePath = "C:/images/";
                    this.articleImageModel.targetPath = @"D:/";

                    cboTax.SelectedIndex = 0;
                    btnAddTax_Click(null, null);

                    cboBarCodeType.SelectedIndex = 0;
                    txtBarCodeValue.Value = "ean" + rno.ToString();
                    btnAddBarCode_Click(null, null);

                    lstSupplierLeft.SelectedIndex = 0;
                    DropDownModel item = this.supplier.Where(x => x.Code == lstSupplierLeft.SelectedValue).FirstOrDefault();
                    rightSupplierList.Add(item);
                    leftSupplierList.RemoveAll(x => x.Code == item.Code);

                    lstSupplierLeft.SelectedIndex = 1;
                    DropDownModel item2 = this.supplier.Where(x => x.Code == lstSupplierLeft.SelectedValue).FirstOrDefault();
                    rightSupplierList.Add(item2);
                    leftSupplierList.RemoveAll(x => x.Code == item2.Code);

                    fillSupplierList();





                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        #region BarCode

        private void fillBarCode()
        {
            try
            {
                masterTypeList = commonManager.GetMasterTypeList();
                var barCodeModesList = (from m in masterTypeList
                                        //where m.CodeType == "site.defaultEan"
                                        where m.CodeType == "MSTEanType"
                                        select new DropDownModel { Code = m.Code, Description = m.ShortDesc }).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboBarCodeType, barCodeModesList);


                //Checkbox List column
                //            Column radioListCol = dgBarCode.Cols[(int) enumBarCode.isdefault];
                ////            Radio checkListEditor = new CheckListEditor(new string[] { "Danish", "Dutch", "English", "Finnish", "French", "German", "Italian",
                ////"Norwegian", "Polish", "Portuguese", "Spanish", "Swedish" });

                //RadioButton rdoIsDefault = new RadioButton();

                //radioListCol.Caption = "radio button";
                //radioListCol.Editor = new UITypeEditorControl(rdoIsDefault, false);
                //radioListCol.Width = 150;

                //for (int i = 1; i < dgBarCode.Rows.Count; i++)
                //{
                //    Load combos data
                //    dgBarCode[i, (int)enumBarCode.isdefault] = currencies.Split('|')[i - 1];

                //}

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cboBarCodeType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboBarCodeType.SelectedIndex >= 0)
                {
                    if (cboBarCodeType.SelectedValue.ToString().ToUpper().Contains("EAN"))
                    {
                        txtBarCodeValue.MaxLength = 13;
                    }
                    else
                    {
                        txtBarCodeValue.MaxLength = 12;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void chkAutoBarCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtBarCodeValue.Enabled = true;
                if (chkAutoBarCode.Checked)
                {
                    txtBarCodeValue.Value = string.Empty;
                    txtBarCodeValue.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnAddBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBarCodeType.SelectedIndex != -1)
                {
                    if (!chkAutoBarCode.Checked)
                    {
                        if (txtBarCodeValue.Value.ToString().Trim().Length == 0)
                        {
                            MessageBox.Show("Please Enter BarCode Value");  //changes made by irfan issue id 2255
                            return;
                        }
                        if (IsBarCodeExist(txtBarCodeValue.Value.ToString()))
                        {
                            MessageBox.Show(txtBarCodeValue.Value + " is Exist, Please Other enter barCode Value");
                            return;
                        }
                    }
                    dgBarCode.Rows.Add();
                    dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeType] = cboBarCodeType.Text;
                    dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeValue] = txtBarCodeValue.Value.ToString();
                    //dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeValue] = chkAutoBarCode.Checked ;

                    if (dgBarCode.Rows.Count - 1 == 1)
                    {
                        dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.isdefault] = true;
                    }
                    else
                    {
                        dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.isdefault] = false;
                    }

                    txtBarCodeValue.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        public bool IsBarCodeExist(string eanCode)
        {
            return articleManager.IsBarCodeExist(eanCode);
        }

        private void chkSelectAllBarCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = chkSelectAllBarCode.Checked;
                for (int rowBarCode = 1; rowBarCode < dgBarCode.Rows.Count; rowBarCode++)
                {
                    dgBarCode.Rows[rowBarCode][(int)enumBarCode.select] = chk;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }

        private void btnBarCodeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool isItemSelected = false;
                for (int rowBarCode = 1; rowBarCode < dgBarCode.Rows.Count; rowBarCode++)
                {
                    if (dgBarCode.Rows[rowBarCode][(int)enumBarCode.select] != null && Convert.ToBoolean(dgBarCode.Rows[rowBarCode][(int)enumBarCode.select]) == true)
                    {
                        isItemSelected = true;
                    }
                }

                if (!isItemSelected)
                {
                    CommonFunc.ShowMessage(" Please select at least 1 record to delete ", Models.Enums.MessageType.Information);
                }

                if (dgBarCode.Rows.Count > 1 && isItemSelected && CommonFunc.ShowMessage("The selected record(s) will be deleted. Are you sure?” ", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    int barCodeRowCount = dgBarCode.Rows.Count;
                    var deleteBarCodeRow = new List<C1.Win.C1FlexGrid.Row>();

                    for (int row = 1; row < barCodeRowCount; row++)
                    {
                        if (dgBarCode.Rows[row][0] != null && (bool)dgBarCode.Rows[row][0])
                        {
                            deleteBarCodeRow.Add(dgBarCode.Rows[row]);
                        }
                    }
                    for (int i = 0; i < deleteBarCodeRow.Count; i++)
                    {
                        dgBarCode.Rows.Remove(deleteBarCodeRow[i]);
                    }

                }

                //if (rowDeleted)
                //{
                //    CommonFunc.ShowMessage("Selected records have been deleted", Models.Enums.MessageType.Information);
                //}
                //else
                //{
                //    CommonFunc.ShowMessage("Please select at least 1 record to delete", Models.Enums.MessageType.Information);
                //}
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region Tax

        private void fillTaxMst()
        {
            try
            {
                listTaxModel = taxManager.GetTaxList();
                var taxdataList = (from t in listTaxModel
                                   select (
                                   new DropDownModel { Code = t.TaxCode, Description = t.TaxName }
                                   )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboTax, taxdataList);
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }

        private void btnAddTax_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsTaxIdRepat =false;
                if (cboTax.SelectedIndex != -1)
                {
                    var selectTaxItem = listTaxModel.Where(x => x.TaxCode == cboTax.SelectedValue.ToString()).FirstOrDefault();
                    if (selectTaxItem != null)
                    {
                        for (int g = 0; g < dgTax.Rows.Count-1; g++)
                        {
                            if (dgTax.Rows[g+1][(int)enumTax.taxCode].ToString().Trim() == cboTax.SelectedValue.ToString().Trim())
                            {
                                IsTaxIdRepat = true;
                            }
                        }

                        if (IsTaxIdRepat == true)
                        {
                            CommonFunc.ShowMessage("Tax Code " + selectTaxItem.TaxName + " already added.", Models.Enums.MessageType.Information);
                        }
                        else
                        {
                            dgTax.Rows.Add();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxName] = selectTaxItem.TaxName;
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxCode] = selectTaxItem.TaxCode;
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxValue] = selectTaxItem.Value.ToString();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.isInclusive] = (selectTaxItem.Inclusive == true) ? "Yes" : "No";
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

        private void chkSelectAllTax_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = chkSelectAllTax.Checked;
                for (int rowtax = 1; rowtax < dgTax.Rows.Count; rowtax++)
                {
                    dgTax.Rows[rowtax][(int)enumTax.select] = chk;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnTaxDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool isItemSelected = false;
                for (int rowTax = 1; rowTax < dgTax.Rows.Count; rowTax++)
                {
                    if (dgTax.Rows[rowTax][(int)enumBarCode.select] != null && Convert.ToBoolean(dgTax.Rows[rowTax][(int)enumBarCode.select]) == true)
                    {
                        isItemSelected = true;
                    }
                }

                if (!isItemSelected)
                {
                    CommonFunc.ShowMessage(" Please select at least 1 record to delete ", Models.Enums.MessageType.Information);
                }

                if (dgTax.Rows.Count > 1 && isItemSelected && CommonFunc.ShowMessage("The selected record(s) will be deleted. Are you sure?” ", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    int taxRowCount = dgTax.Rows.Count;
                    var deleteTaxRow = new List<C1.Win.C1FlexGrid.Row>();

                    for (int row = 1; row < taxRowCount; row++)
                    {
                        if (dgTax.Rows[row][0] != null && (bool)dgTax.Rows[row][0])
                        {
                            deleteTaxRow.Add(dgTax.Rows[row]);
                        }
                    }

                    for (int i = 0; i < deleteTaxRow.Count; i++)
                    {
                        dgTax.Rows.Remove(deleteTaxRow[i]);
                    }

                }

                //if (rowDeleted)
                //{
                //    CommonFunc.ShowMessage("Selected records have been deleted", Models.Enums.MessageType.Information);
                //}
                //else
                //{
                //    CommonFunc.ShowMessage("Please select at least 1 record to delete", Models.Enums.MessageType.Information);
                //}
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region Supplier

        public void HideColumns(ref C1.Win.C1List.C1List list, string columns)
        {
            try
            {
                for (int colIndex = 0; colIndex < list.Splits[0].DisplayColumns.Count; colIndex++)
                {
                    if (!columns.Contains(list.Splits[0].DisplayColumns[colIndex].Name))
                        list.Splits[0].DisplayColumns[colIndex].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void fillSupplierList()
        {
            try
            {
                //---  Bind supplier Left list box to left Supplier list ...
                //lstSupplierLeft.ClearItems();
                lstSupplierLeft.DataSource = null;
                lstSupplierLeft.DataSource = leftSupplierList.OrderBy(s => s.Description).ToList();
                lstSupplierLeft.ValueMember = "Code";
                lstSupplierLeft.DisplayMember = "Description";
                lstSupplierLeft.Splits[0].DisplayColumns[0].Visible = false;
                lstSupplierLeft.SelectedIndex = 0;
                //this.lstSupplierLeft.Splits[0].ColumnCaptionHeight = this.lstSupplierLeft.Splits[0].ColumnCaptionHeight *2;
                this.lstSupplierLeft.RowDivider.Style = C1.Win.C1List.LineStyleEnum.Inset;
                //this.lstSupplierLeft.Splits[0].  = System.Drawing.Color.Red ;
                this.lstSupplierLeft.Styles["Heading"].HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
                System.Drawing.Color supplierHeaderColor = System.Drawing.Color.FromArgb(109, 170, 243);
                this.lstSupplierLeft.Styles["Heading"].BackColor = supplierHeaderColor;
                this.lstSupplierLeft.CaptionStyle.Font = new System.Drawing.Font(this.lstSupplierLeft.CaptionStyle.Font, System.Drawing.FontStyle.Bold);
                //this.lstSupplierLeft.Styles["Caption"].Font = new System.Drawing.Font(this.lstSupplierLeft.Styles("Caption").Font, System.Drawing.FontStyle.Bold);

                //---  Bind supplier right list box to right Supplier list ...
                //lstSupplierRight.ClearItems();
                lstSupplierRight.DataSource = null;

                //code commented for order by isDefault supplier desc - ashma 26-4-18
                //lstSupplierRight.DataSource = rightSupplierList.OrderBy(s => s.Description).ToList();
                lstSupplierRight.DataSource = rightSupplierList.ToList();
                //code added to populate label - ashma 26-4-18
                if (rightSupplierList.Count != 0)
                {
                    lblDefaultSupplierValue.Text = rightSupplierList[0].Code;
                    lblDefaultSupplier.Text = rightSupplierList[0].Description;
                }
               
                lstSupplierRight.ValueMember = "Code";
                lstSupplierRight.DisplayMember = "Description";
                lstSupplierRight.Splits[0].DisplayColumns[0].Visible = false;
                lstSupplierRight.SelectedIndex = 0;
                this.lstSupplierRight.RowDivider.Style = C1.Win.C1List.LineStyleEnum.Inset;
                this.lstSupplierRight.Styles["Heading"].HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
                this.lstSupplierRight.Styles["Heading"].BackColor = supplierHeaderColor;
                this.lstSupplierRight.CaptionStyle.Font = new System.Drawing.Font(this.lstSupplierLeft.CaptionStyle.Font, System.Drawing.FontStyle.Bold);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSupplierLeft.SelectedIndex != -1 && lstSupplierLeft.SelectedIndex != lstSupplierLeft.ListCount)
                {
                    LastSupllierAddRemoved = string.Empty;
                    LastSupllierAddRemoved = lstSupplierLeft.SelectedValue.ToString(); //added on 09/11/2017
                    DropDownModel item = this.supplier.Where(x => x.Code == lstSupplierLeft.SelectedValue).FirstOrDefault();
                    rightSupplierList.Add(item);
                    leftSupplierList.RemoveAll(x => x.Code == item.Code);
                    fillSupplierList();
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSupplierRight.SelectedIndex != -1 && lstSupplierRight.SelectedIndex != lstSupplierRight.ListCount)
                {
                     LastSupllierAddRemoved = string.Empty;
                     LastSupllierAddRemoved = lstSupplierRight.SelectedValue.ToString(); //added on 09/11/2017
                    DropDownModel item = this.supplier.Where(x => x.Code == lstSupplierRight.SelectedValue).FirstOrDefault();
                    rightSupplierList.RemoveAll(x => x.Code == item.Code);
                    leftSupplierList.Add(item);
                    fillSupplierList();
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnRightAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.leftSupplierList.Clear();
                this.rightSupplierList.Clear();
                this.rightSupplierList.AddRange(this.supplier);
                fillSupplierList();
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.rightSupplierList.Clear();
                this.leftSupplierList.Clear();
                this.leftSupplierList.AddRange(this.supplier);
                fillSupplierList();
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region Basic

        private void fillItemTypeMst()
        {
            try
            {
                listItemTypeModel = commonManager.GetArticleTypeList();
                var itemTypedataList = (from t in listItemTypeModel
                                        select (
                                        new DropDownModel { Code = t.ArticalTypeCode, Description = t.ArticalTypeName }
                                        )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboItemType, itemTypedataList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillUoMMst()
        {
            try
            {
                listUoMModel = commonManager.GetUoMList();
                var uoMdataList = (from t in listUoMModel
                                   select (
                                   new DropDownModel { Code = t.UOMCode, Description = t.UOM }
                                   )).ToList();

                List<DropDownModel> uoMdataList2 = new List<DropDownModel>();
                uoMdataList2.AddRange(uoMdataList);
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboBaseUnitMeasure, uoMdataList);
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboOrderUnitMeasure, uoMdataList2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void fillManufacturerMst()
        {
            try
            {
                listManufacturerModel = commonManager.GetManufacturerList();
                var manufacturerdataList = (from t in listManufacturerModel
                                            select (
                                            new DropDownModel { Code = t.ManufacturerCode, Description = t.ManufacturerName }
                                            )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboManufacturer, manufacturerdataList);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void fillCharacteristicsMst()
        {
            try
            {
                listCharacteristicsModel = commonManager.GetCharacteristicsList();

                string defaultProfileName = CommonFunc.GetEnumStringValue(enumCharacteristics.defaultProfileName);
                string colourName = CommonFunc.GetEnumStringValue(enumCharacteristics.colour);
                string sizeName = CommonFunc.GetEnumStringValue(enumCharacteristics.size);
                string styleName = CommonFunc.GetEnumStringValue(enumCharacteristics.style);
                string fabricName = CommonFunc.GetEnumStringValue(enumCharacteristics.fabric);

                var colourList = (from t in listCharacteristicsModel
                                  where t.ProfileName == defaultProfileName && t.CharName == colourName
                                  select (
                                       new DropDownModel { Code = t.CharValue, Description = t.CharValue }
                                          )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboColour, colourList);

                var sizeList = (from t in listCharacteristicsModel
                                where t.ProfileName == defaultProfileName && t.CharName == sizeName
                                select (
                                new DropDownModel { Code = t.CharValue, Description = t.CharValue }
                                )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboSize, sizeList);

                var styleList = (from t in listCharacteristicsModel
                                 where t.ProfileName == defaultProfileName && t.CharName == styleName
                                 select (
                                 new DropDownModel { Code = t.CharValue, Description = t.CharValue }
                                 )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboStyle, styleList);

                var fabricList = (from t in listCharacteristicsModel
                                  where t.ProfileName == defaultProfileName && t.CharName == fabricName
                                  select (
                                  new DropDownModel { Code = t.CharValue, Description = t.CharValue }
                                  )).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboFabric, fabricList);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void cboManufacturer_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboBrand.SelectedIndex = -1;
                fillBrandMst();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void fillBrandMst()
        {
            try
            {
                if (cboManufacturer.SelectedIndex != -1)
                {

                    listBrandModel = commonManager.GetBrandList();
                    var branddataList = (from t in listBrandModel
                                         where t.ManufacturerCode == cboManufacturer.SelectedValue.ToString()
                                         select (
                                         new DropDownModel { Code = t.BrandCode, Description = t.BrandName }
                                         )).ToList();

                    CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboBrand, branddataList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fold = new OpenFileDialog())
            {
                fold.Filter = "Pictures|*.jpg;*.bmp;*.png";
                fold.Title = "Select file";
                if (fold.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // fold.FilterIndex = 2
                    fold.RestoreDirectory = true;
                    txtItemImage.Value = fold.FileName;

                    this.articleImageModel = new ArticleImageModel();
                    this.articleImageModel.ArticleImage = fold.SafeFileName;
                    this.articleImageModel.sourcePath = fold.FileName.Substring(0, fold.FileName.Length - fold.SafeFileName.ToString().Length);
                    this.articleImageModel.targetPath = @"D:/";
                }
            }
        }

        #endregion

        private void txtLastNodeCode_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtLastNodeCode.Text == String.Empty &&  IsHirarchyPopupShow==false)
                {
                Popup: frmItemHierarchyPopup objItemHierarchyPopup = new frmItemHierarchyPopup();
                    DialogResult dialogRes = objItemHierarchyPopup.ShowDialog();
            //        if (dialogRes == DialogResult.OK)
            //        {
            //            _selectedItemNode = objItemHierarchyPopup.selectedItemNode;
            //            //added on 8th dec by ashma
            //            if(_selectedItemNode.Count==0)
            //            //if (_selectedItemNode[0] == null)
            //            {
            //               // MessageBox.Show("Please select Items from the Hierarchy");
            //                goto Popup;
            //                //  this.BeginInvoke(new MethodInvoker(Close));
            //              //  return;
            //            }
            //            if ((bool)_selectedItemNode[0].ISThisLastNode)
            //            {
            //                txtLastNodeCode.Value = _selectedItemNode[0].NodeName;
            //                txtLastNodeCode.Tag = _selectedItemNode[0].Nodecode;
            //                txtParentItemCode.Value = _selectedItemNode[0].ParentNodeName;
            //                // code added by khusrao adil
            //                txtParentItemCode.Tag = _selectedItemNode[0].ParentNodecode;
            //                txtItemTree.Value = _selectedItemNode[0].TreeCode;
            //            }
            //        }
            //        else if (dialogRes == DialogResult.Cancel)
            //        {
            //            this.BeginInvoke(new MethodInvoker(Close));
            //        }
            //    }
            //}

                    if (dialogRes == DialogResult.OK)                   //added by vipin on 30-03-2017
                    {
                        _selectedItemNode = objItemHierarchyPopup.selectedItemNode;
                        //added on 8th dec by ashma
                        if (_selectedItemNode.Count > 0)
                        {
                            if (_selectedItemNode[0] == null)
                            //if (_selectedItemNode[0] == null)
                            {
                                // MessageBox.Show("Please select Items from the Hierarchy");
                                goto Popup;
                                //  this.BeginInvoke(new MethodInvoker(Close));
                                //  return;
                            }

                            if (_selectedItemNode[0] != null)
                                if ((bool)_selectedItemNode[0].ISThisLastNode)
                                {
                                    txtLastNodeCode.Value = _selectedItemNode[0].NodeName;
                                    txtLastNodeCode.Tag = _selectedItemNode[0].Nodecode;
                                    txtParentItemCode.Value = _selectedItemNode[0].ParentNodeName;
                                    // code added by khusrao adil
                                    txtParentItemCode.Tag = _selectedItemNode[0].ParentNodecode;
                                    txtItemTree.Value = _selectedItemNode[0].TreeCode;

                                    txtLastNodeCode.Text = _selectedItemNode[0].NodeName.ToString().Trim();
                                    txtParentItemCode.Text = _selectedItemNode[0].ParentNodecode.ToString().Trim();
                                    txtItemTree.Text = _selectedItemNode[0].TreeCode.ToString().Trim();
                                }
                                else   // added by vipin on 11-04-2017
                                {
                                    goto Popup;
                                }
                        }
                        else
                        {
                            goto Popup;
                        }

                    }
                    else if (dialogRes == DialogResult.Cancel)
                    {
                        this.Close();
                       // this.BeginInvoke(new MethodInvoker(Close));
                      //  objItemHierarchyPopup.Close();
                       // return;
                    }
                }
            }



            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void txtArticleCode_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (ttItemName == null)
                {
                    ttItemName = new ToolTip();
                    ttItemName.InitialDelay = 0;
                    ttItemName.IsBalloon = true;
                }
                ttItemName.Show("Input an Article Code of your choice. If left blank system will generate an article Code and associate with the record", txtArticleCode, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtArticleCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ttItemName != null)
                {
                    ttItemName.Hide(txtArticleCode);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CommonFunc.ShowMessage("Are You Sure? All the data will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
            {

                this.BeginInvoke(new MethodInvoker(Close));
                // this.Close();
                //  this.Dispose();
                //this.Dispose();
                // this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormvalidate())
                {
                    fillArticleBasicDataToModel();
                    fillArticleEANDataToModel();
                    fillArticleCharacteristicDataToModel();
                    fillArticleTaxDataToModel();
                    string autoArticleCode = txtArticleCode.Text;

                    //if ((string.IsNullOrEmpty(editArticleCode))

                    //            ?

                    //    this.articleManager.SaveArticle(this.articleModel, this.articleCharacteristicMatrixModel, this.articleImageModel,
                    //            this.eanModel, this.siteArticleTaxMappingModel, masterArticleMapModel, rightSupplierList, ref autoArticleCode)

                    //            :

                    //if (this.articleManager.SaveArticle(this.articleModel, this.articleCharacteristicMatrixModel, this.articleImageModel,
                    //           this.eanModel, this.siteArticleTaxMappingModel, masterArticleMapModel, rightSupplierList, ref autoArticleCode))

                    //added to save default supplier in db - ashma 26-4-18
                    LastSupllierAddRemoved = lblDefaultSupplierValue.Text;

                    if (this.articleManager.SaveArticle(this.articleModel, this.articleCharacteristicMatrixModel, this.articleImageModel,
                     this.eanModel, this.siteArticleTaxMappingModel, masterArticleMapModel, rightSupplierList, leftSupplierList, ref autoArticleCode, ref LastSupllierAddRemoved))
                    {
                        if (this.articleManager.UpdateMstArticle(autoArticleCode, LastSupllierAddRemoved))
                        {
                            MessageBox.Show(" Article code  " + autoArticleCode + " added successfully. ");
                            clearForm();
                            IsHirarchyPopupShow = true;
                            BasicTabPage.Show();
                        }
                    }
                    else
                    {
                        if (cboBarCodeType.SelectedIndex == -1)
                        {
                            //c1DockingTab1.SelectedTab = 1;
                            c1DockingTab1.SelectedTab = TabPageBarCode;
                        }
                        else if (cboTax.SelectedIndex == -1)
                        {
                            c1DockingTab1.SelectedTab = TabPageTax;
                        }
                        else if (lstSupplierRight.ListCount == 0)
                        {
                            c1DockingTab1.SelectedTab = TabPageSuppplier;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillArticleMasterModel()
        {
            try
            {
                masterArticleMapModel = new MasterArticleMapModel();
                masterArticleMapModel.ArticleCode = string.IsNullOrEmpty(txtArticleCode.Text) ? "" : txtArticleCode.Text.Trim();
                masterArticleMapModel.MasterArticleCode = txtParentItemCode.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillArticleBasicDataToModel()
        {
            try
            {
                articleModel = new ArticleModel();
                this.articleModel.ArticleCode = string.IsNullOrEmpty(txtArticleCode.Text) ? "" : txtArticleCode.Text.Trim();
                this.articleModel.IsAutoNumber = string.IsNullOrEmpty(txtArticleCode.Text) ? true : false;

                this.articleModel.ArticalTypeCode = (cboItemType.SelectedValue != null) ? cboItemType.SelectedValue.ToString() : "";
                this.articleModel.LegacyArticleCode = txtLegecyCode.Text.Trim();
                this.articleModel.ArticleName = txtItemDescription.Text.Trim();
                this.articleModel.ArticleShortName = txtItemShortName.Text.Trim();
                this.articleModel.Salable = (rdoSaleableYes.Checked) ? true : false;
                this.articleModel.ArticleActive = (rdoStatusActive.Checked) ? true : false;
                this.articleModel.isExpiry = (rdoExpirableYes.Checked) ? true : false;
                this.articleModel.Printable = (rdoPrintableYes.Checked) ? true : false;
                this.articleModel.IsMrpOpen = (rdoOpenMRPYes.Checked) ? true : false;
                this.articleModel.ToleranceValue = (rdoOpenQtyYes.Checked) ? true : false;
                //code comented by khusrao adil 
                //this.articleModel.ParentArt = string.IsNullOrEmpty(txtParentItemCode.Text) ? "" : txtParentItemCode.Text.Trim();
                this.articleModel.ArticalCatCode = "1";
                if (txtParentItemCode.Tag == null)
                {
                    this.articleModel.ParentArt = string.IsNullOrEmpty(txtParentItemCode.Text) ? "" : txtParentItemCode.Text.Trim();
                }
                else
                {
                    this.articleModel.ParentArt = string.IsNullOrEmpty(txtParentItemCode.Tag.ToString()) ? "" : txtParentItemCode.Tag.ToString();
                }
                //this.articleModel.ParentArt = string.IsNullOrEmpty(txtParentItemCode.Text) ? "" : txtParentItemCode.Text.Trim();
                this.articleModel.TreeID = string.IsNullOrEmpty(txtItemTree.Text) ? "" : txtItemTree.Text.Trim();
                this.articleModel.LastNodeCode = string.IsNullOrEmpty(txtLastNodeCode.Tag.ToString()) ? "" : txtLastNodeCode.Tag.ToString();


                this.articleModel.ManufacturerCode = (cboManufacturer.SelectedValue != null) ? cboManufacturer.SelectedValue.ToString() : "";
                this.articleModel.BrandCode = (cboBrand.SelectedValue != null) ? cboBrand.SelectedValue.ToString() : "";


                this.articleModel.NetWeight = decimal.Parse(string.IsNullOrEmpty(txtNetWeight.Text.Trim()) ? "0" : txtNetWeight.Text.Trim());
                this.articleModel.GrossWeight = decimal.Parse(string.IsNullOrEmpty(txtGrossWeight.Text.Trim()) ? "0" : txtGrossWeight.Text.Trim());
                this.articleModel.BaseUnitofMeasure = (cboBaseUnitMeasure.SelectedValue != null) ? cboBaseUnitMeasure.SelectedValue.ToString() : "";
                this.articleModel.DistributionUnitofMeasure = (cboBaseUnitMeasure.SelectedValue != null) ? cboBaseUnitMeasure.SelectedValue.ToString() : "";
                this.articleModel.SaleUnitofMeasure = (cboBaseUnitMeasure.SelectedValue != null) ? cboBaseUnitMeasure.SelectedValue.ToString() : "";
                this.articleModel.OrderUnitofMeasure = (cboOrderUnitMeasure.SelectedValue != null) ? cboOrderUnitMeasure.SelectedValue.ToString() : "";
                this.articleModel.VolumeUOM = string.IsNullOrEmpty(txtOrderValue.Text) ? "0" : txtOrderValue.Text.Trim();
                this.articleModel.ConsumptionUoM = (cboBaseUnitMeasure.SelectedValue != null) ? cboBaseUnitMeasure.SelectedValue.ToString() : "";
                this.articleModel.CostPrice = decimal.Parse(string.IsNullOrEmpty(txtCostPrice.Text.Trim()) ? "0" : txtCostPrice.Text.Trim());
                this.articleModel.SellPrice = decimal.Parse(string.IsNullOrEmpty(txtSalePrice.Text.Trim()) ? "0" : txtSalePrice.Text.Trim());
                this.articleModel.MRP = decimal.Parse(string.IsNullOrEmpty(txtMRP.Text.Trim()) ? "0" : txtMRP.Text.Trim());
                this.articleModel.Margin = decimal.Parse(string.IsNullOrEmpty(txtMargin.Text.Trim()) ? "0" : txtMargin.Text.Trim());
                this.articleModel.IsPremaman = false;
                this.articleModel.MaterialTypeCode = "FDPT";
                this.articleModel.DistributionUomValue = 1;
                this.articleModel.SalesUomValue = 1;

                // = txtItemDescription.Text.Trim();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void fillArticleEANDataToModel()
        {
            try
            {
                eanModel = new List<EANModel>();
                for (int rowBarCode = 1; rowBarCode < dgBarCode.Rows.Count; rowBarCode++)
                {
                    this.eanModel.Add(
                        new EANModel
                        {
                            EAN = dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeValue].ToString(),
                            Discription = dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeType].ToString(),
                            IsAutoNumber = string.IsNullOrEmpty(dgBarCode.Rows[rowBarCode][(int)enumBarCode.barCodeValue].ToString()) ? true : false,
                            DefaultEAN = (bool)true//dgBarCode.Rows[rowBarCode][(int)enumBarCode.isdefault]
                        });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillArticleTaxDataToModel()
        {
            try
            {
                siteArticleTaxMappingModel = new List<SiteArticleTaxMappingModel>();
                for (int rowTaxCode = 1; rowTaxCode < dgTax.Rows.Count; rowTaxCode++)
                {
                    this.siteArticleTaxMappingModel.Add(
                        new SiteArticleTaxMappingModel
                        {
                            TaxName = dgTax.Rows[rowTaxCode][(int)enumTax.taxName].ToString(),
                            TaxCode = dgTax.Rows[rowTaxCode][(int)enumTax.taxCode].ToString(),
                            SupplierCode = CommonFunc.DefaultSupplier
                        });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void fillArticleCharacteristicDataToModel()
        {
            try
            {
                articleCharacteristicMatrixModel = new List<ArticleCharacteristicMatrixModel>();
                string defaultProfileName = CommonFunc.GetEnumStringValue(enumCharacteristics.defaultProfileName);
                string colourName = CommonFunc.GetEnumStringValue(enumCharacteristics.colour);
                string sizeName = CommonFunc.GetEnumStringValue(enumCharacteristics.size);
                string styleName = CommonFunc.GetEnumStringValue(enumCharacteristics.style);
                string fabricName = CommonFunc.GetEnumStringValue(enumCharacteristics.fabric);

                // COLOR ...

                if (cboColour.SelectedIndex != -1)
                {
                    this.articleCharacteristicMatrixModel.Add(
                        new ArticleCharacteristicMatrixModel
                        {
                            ProfileCode = defaultProfileName,
                            CharType = true,
                            CharCode = colourName,
                            CharValue = (cboColour.SelectedValue != null) ? cboColour.SelectedValue.ToString() : "",
                            RowKey = 1
                        });
                }

                // SIZE ...
                if (cboSize.SelectedIndex != -1)
                {
                    this.articleCharacteristicMatrixModel.Add(
                            new ArticleCharacteristicMatrixModel
                            {
                                ProfileCode = defaultProfileName,
                                CharType = true,
                                CharCode = sizeName,
                                CharValue = (cboSize.SelectedValue != null) ? cboSize.SelectedValue.ToString() : "",
                                RowKey = 1
                            });
                }

                // STYLE ...
                if (cboStyle.SelectedIndex != -1)
                {
                    this.articleCharacteristicMatrixModel.Add(
                            new ArticleCharacteristicMatrixModel
                            {
                                ProfileCode = defaultProfileName,
                                CharType = true,
                                CharCode = styleName,
                                CharValue = (cboStyle.SelectedValue != null) ? cboStyle.SelectedValue.ToString() : "",
                                RowKey = 1

                            });
                }

                // FABRIC ...
                if (cboFabric.SelectedIndex != -1)
                {
                    this.articleCharacteristicMatrixModel.Add(
                            new ArticleCharacteristicMatrixModel
                            {
                                ProfileCode = defaultProfileName,
                                CharType = true,
                                CharCode = fabricName,
                                CharValue = (cboFabric.SelectedValue != null) ? cboFabric.SelectedValue.ToString() : "",
                                RowKey = 1
                            });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region " Functions "

        private void txtValidateSpecialCharSpace(object sender, KeyPressEventArgs e)
        {
            if (!CommonFunc.validateNumberAlphabet(e.KeyChar.ToString()))
            {
                e.Handled = true;
                CommonFunc.ShowMessage(" Only Characters and numbers allowed ", MessageType.Information);
            }

        }

        private void txtOrderValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboOrderUnitMeasure.SelectedIndex == -1)
            {
                CommonFunc.ShowMessage("Please select a Purchase UOM first", MessageType.Information);
                e.Handled = true;
            }
        }

        /// <summary>
        /// apply Form level default validation accroding to SRS .... 
        /// </summary>
        private void setInitialFormsValidations()
        {
            try
            {
                // ArticleCode text box. Accepts max 26 characters Alpha numeric.  No special characters or space
                txtArticleCode.MaxLength = 26;
                // Item Description Mandatory.Text Box. Accepts alphanumeric, space & special characters Char limit 100 chars.
                txtItemDescription.MaxLength = 100;
                // Item Short Name		Mandatory. Text Box.  Accepts alphanumeric, space & special characters. Char limit 30 chars
                txtItemShortName.MaxLength = 30;
                // Legacy Code Text box. Accepts max 26 characters Alpha numeric. No special characters or space
                txtLegecyCode.MaxLength = 26;

                // Net weight	To capture net weight of the item	Numeric Field.  10 characters
                txtNetWeight.MaxLength = 10;
                txtNetWeight.DataType = typeof(Int32);

                // Gross Weight	To capture gross weight of the item	Numeric Field.  10 characters
                txtGrossWeight.MaxLength = 10;
                txtGrossWeight.DataType = typeof(Int32);
                //Purchase UOM Value		Numeric text field. 10 char max. Mandatory. 
                txtOrderValue.MaxLength = 10;
                txtOrderValue.DataType = typeof(Int32);
                //Cost Price Numeric Field. Mandatory. Accepts Integers and whole numbers. Max Length 10 chars
                txtCostPrice.MaxLength = 10;
                txtCostPrice.DataType = typeof(double);
                //Sales Price Numeric Field. Mandatory. Accepts Integers and whole numbers. Max Length 10 chars. If CP and Margin have been added, then SP is auto calculated
                txtSalePrice.MaxLength = 10;
                txtSalePrice.DataType = typeof(double);
                //Margin Numeric Field. Mandatory. Accepts Integers and whole numbers. Max Length 10 chars. If CP and SP have been added, then Margin% is auto calculated
               // txtMargin.MaxLength = 10;
                txtMargin.DataType = typeof(double);
                //MRP	 Disabled if Open MRP is set to yes in basic page.Numeric Field. Mandatory. Accepts Integers and whole numbers. Max Length 10 chars
                txtMRP.MaxLength = 10;
                txtMRP.DataType = typeof(double);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsFormvalidate(int tabvalue = 5)
        {
            try
            {
                bool validateResult = true;
                bool isControlFocused = true;

                //---------- Item Tab ---------------
                //if (tabvalue == (int)enumTabs.basic || tabvalue > 4)
                //{

                    if (!string.IsNullOrEmpty(txtArticleCode.Text) && string.IsNullOrEmpty(editArticleCode))
                    {
                        //var IssupplierModel = articleManager.Getar(txtArticleCode.Text);
                        //if (IssupplierModel != null)
                        //{
                        //    CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtArticleCode, "Supplier code already exists", true);
                        //    txtArticleCode.Focus();
                        //    validateResult = false;
                        //    isControlFocused = false;
                        //}
                        //else
                        //{
                        //    ItemErrorProvider.SetError(txtArticleCode, string.Empty);
                        //    txtArticleCode.BorderColor = CommonFunc.DefaultBorderColor;
                        //}
                    }

                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref ItemErrorProvider, ref cboItemType, "Item Type value required"))
                    {
                        if (isControlFocused)
                            this.cboItemType.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }

                    if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtItemDescription, "Item Description value required"))
                    {
                        if (isControlFocused)
                            this.txtItemDescription.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }

                    if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtItemShortName, "Item Short Name value required"))
                    {
                        if (isControlFocused)
                            this.txtItemShortName.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }


                    if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtCostPrice, "Item Cost Price required"))  // added by vipin
                    {
                        if (isControlFocused)
                            this.txtLastNodeCode.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }

                //Code is commented by irfan on 5/2/2018 form mantis issue.
                    //if (isSalePriceValid == false)   //vipin on 21-04-2014
                    //{
                    //    if (!CommonFunc.SetCustomErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, "Sale price should greater than cost price."))
                    //    {

                    //    }
                    //    validateResult = false;
                    //    isControlFocused = false;
                    //}

                    if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, "Item Sales Price required"))
                    {
                        if (isControlFocused)
                            this.txtLastNodeCode.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }
                    //if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtMargin, "Item Margin required"))   // vipin 20-04-2017
                    //{
                    //    if (isControlFocused)
                    //        this.txtLastNodeCode.Focus();

                    //    validateResult = false;
                    //    isControlFocused = false;
                    //}
                    if (!CommonFunc.SetErrorProvidertoControl(ref ItemErrorProvider, ref txtMRP, "Item MRP required"))
                    {
                        if (isControlFocused)
                            this.txtLastNodeCode.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }


                // Ended by vipin




                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref ItemErrorProvider, ref cboBaseUnitMeasure, "Base Unit Measure value required"))
                    {
                        if (isControlFocused)
                            this.cboItemType.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }


                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref ItemErrorProvider, ref cboOrderUnitMeasure, "Order UnitMeasure value required"))
                    {
                        if (isControlFocused)
                            this.cboItemType.Focus();

                        validateResult = false;
                        isControlFocused = false;
                    }

                 
                 
                   // TabPage t = c1DockingTab1. =3;

                    if (validateResult == false)
                    {
                        c1DockingTab1.SelectedIndex = 0;
                    }

             //  }

                //if (tabvalue == (int)enumTabs.barCode || tabvalue > 4)
                //{
                    //dgBarCode grid

                    //if (dgBarCode.Rows.Count==1)
                    //{
                    //     if (isControlFocused)
                    //        this.cboBarCodeType.Focus();

                    //    validateResult = false;
                    //    isControlFocused = false;
                    //}
                    if (validateResult == true)  // added by vipin
                    {
                        if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref ItemErrorProvider, ref cboBarCodeType, "Barcode value required"))
                        {
                                validateResult = false;
                                isControlFocused = false;
                      
                            if (validateResult == false)
                            {
                                c1DockingTab1.SelectedIndex = 1;
                            }

                        }
                        else
                        {
                            if (dgBarCode.Rows.Count <= 1)
                            {
                                if (isControlFocused)
                                    this.cboBarCodeType.Focus();

                                validateResult = false;
                                isControlFocused = false;

                                if (validateResult == false)
                                {
                                    c1DockingTab1.SelectedIndex = 1;
                                    CommonFunc.ShowMessage("Add at least one BarCode ", MessageType.Information); //vipin on 04-04-201
                                }
                            }

                        }

                    }
             //   }

                if (tabvalue == (int)enumTabs.tax || tabvalue > 4)
                {
                }


                //if (tabvalue == (int)enumTabs.supplier || tabvalue > 4)
                //{
                if (validateResult == true)   // added by vipin
                {
                    ///lstSupplierRight
                    if (lstSupplierRight.ListCount <= 0)
                    {

                        if (isControlFocused)
                            this.btnRight.Focus();
                 
                        validateResult = false;
                        isControlFocused = false;

                        if (validateResult == false)
                        {
                            c1DockingTab1.SelectedIndex = 3;
                            CommonFunc.ShowMessage("Select at least one Supplier ", MessageType.Information); //vipin on 04-04-201
                        }
                    }
                }
               // }

                return validateResult;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// clears the value of all forms controls  .....
        /// </summary>
        private void clearForm()
        {
            try
            {
                editArticleCode = string.Empty;
                txtArticleCode.ReadOnly = false;
                txtArticleCode.Clear();
                cboItemType.SelectedIndex = -1;
                txtLegecyCode.Value = string.Empty;
                txtItemDescription.Value = string.Empty;
                txtItemShortName.Value = string.Empty;

                txtParentItemCode.Value = string.Empty;
                txtItemTree.Value = string.Empty;
                txtLastNodeCode.Value = string.Empty;

                cboManufacturer.SelectedIndex = -1;
                cboBrand.SelectedIndex = -1;

                txtNetWeight.Value = string.Empty;
                txtGrossWeight.Value = string.Empty;
                cboBaseUnitMeasure.SelectedIndex = -1;
                cboOrderUnitMeasure.SelectedIndex = -1;
                txtOrderValue.Value = string.Empty;

                txtCostPrice.Value = string.Empty;
                txtSalePrice.Value = string.Empty;
                txtMRP.Value = string.Empty;
                txtMargin.Value = string.Empty;
                cboColour.SelectedIndex = -1;
                cboSize.SelectedIndex = -1;
                cboFabric.SelectedIndex = -1;
                cboStyle.SelectedIndex = -1;
                txtItemImage.Value = string.Empty;
                cboTax.SelectedIndex = -1;
                dgTax.Rows.Count = 1;

                cboBarCodeType.SelectedIndex = -1;
                txtBarCodeValue.Value = string.Empty;
                dgBarCode.Rows.Count = 1;
                txtMargin.Text = string.Empty;  // vipin on 20-04-2017

                //CODE ADDED BY IRFAN ON 02/11/2017 FOR SPECTRUMLITE ISSUES
                ////txtMRP.Enabled = false;
                ////txtSalePrice.Enabled = false;
                //=========================================================
                //code added by vipul for issue id 3261
                cboItemType.Enabled = true;
                btnLeftAll_Click(null, null);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void txtItemDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "'")
            {
                e.Handled = true;
            }
        }

        private void dgBarCode_CellChecked(object sender, RowColEventArgs e)
        {
            if (e.Col == 3)
            {
                int selectedRowBarCode = dgBarCode.Row;
                bool isdefaultTrue = false;
                if ((bool)dgBarCode.Rows[selectedRowBarCode][(int)enumBarCode.isdefault])
                {
                    for (int rowBarCode = 1; rowBarCode < dgBarCode.Rows.Count; rowBarCode++)
                    {
                        dgBarCode.Rows[rowBarCode][(int)enumBarCode.isdefault] = false;
                        if (rowBarCode == selectedRowBarCode)
                        {
                            dgBarCode.Rows[rowBarCode][(int)enumBarCode.isdefault] = true;
                            isdefaultTrue = true;
                        }
                    }
                }
                if (isdefaultTrue == false)
                {
                    dgBarCode.Rows[1][(int)enumBarCode.isdefault] = true;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var articleList = this.articleManager.GetArticleSearchList();
                frmCommonSearch objSearch = new frmCommonSearch();
                objSearch.DataList = articleList.ToList();

                if (objSearch.ShowDialog() == DialogResult.OK)
                {
                    if (objSearch.SelectedRows.Count > 0)
                    {
                        clearForm();
                        editArticleCode = ((Spectrum.Models.ArticleSearchModel)(objSearch.SelectedRows)[0]).ArticleCode;
                        ArticleDataModel articleDataModel = this.articleManager.GetArticleData(editArticleCode);


                        //---  For Edit Item, all the fields except Article code are not editable.
                        fillArticleData(articleDataModel);
                        fillArticleCharactesticsData(articleDataModel);
                        fillArticleEANData(articleDataModel);
                        fillArticleTaxData(articleDataModel);
                        fillSupplierListData(articleDataModel);

                        cboBarCodeType.SelectedIndex = 0;   //vipin 19-04-2017
                        txtArticleCode.ReadOnly = false;
                        txtArticleCode.Text = editArticleCode;
                        txtArticleCode.ReadOnly = true;
                        cboItemType.Enabled = false; 

                    }
                }
                objSearch.Dispose();
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillArticleData(ArticleDataModel articleDataModel)
        {
            try
            {
                if (articleDataModel != null)
                {
                    txtArticleCode.Value = articleDataModel.articleModel.ArticleCode;
                    cboItemType.SelectedValue = articleDataModel.articleModel.ArticalTypeCode;
                    txtLegecyCode.Value = articleDataModel.articleModel.LegacyArticleCode;
                    txtItemDescription.Value = articleDataModel.articleModel.ArticleName;
                    txtItemShortName.Value = articleDataModel.articleModel.ArticleShortName;

                    txtParentItemCode.Value = articleDataModel.articleModel.ParentArt;
                    txtItemTree.Value = articleDataModel.articleModel.TreeID;
                    txtLastNodeCode.Value = articleDataModel.articleModel.ParentArticleName;
                    txtLastNodeCode.Tag = articleDataModel.articleModel.LastNodeCode;

                    if (articleDataModel.articleModel.ManufacturerCode != null)
                    {
                        cboManufacturer.SelectedValue = articleDataModel.articleModel.ManufacturerCode;
                        cboBrand.SelectedValue = articleDataModel.articleModel.BrandCode;
                    }

                    txtNetWeight.Value = Convert.ToInt64(articleDataModel.articleModel.NetWeight);
                    txtGrossWeight.Value = Convert.ToInt64(articleDataModel.articleModel.GrossWeight);
                    cboBaseUnitMeasure.SelectedValue = articleDataModel.articleModel.BaseUnitofMeasure;
                    cboOrderUnitMeasure.SelectedValue = articleDataModel.articleModel.OrderUnitofMeasure;
                    txtOrderValue.Value = articleDataModel.articleModel.VolumeUOM;

                    txtCostPrice.Value = articleDataModel.articleModel.CostPrice;
                    txtSalePrice.Value = articleDataModel.articleModel.SellPrice;
                    txtMRP.Value = articleDataModel.articleModel.MRP;
                    txtMargin.Value = articleDataModel.articleModel.Margin;

                    txtItemImage.Value = articleDataModel.articleModel.ArticleImage;

                    if (articleDataModel.articleModel.isExpiry != null && (bool)articleDataModel.articleModel.isExpiry)
                    {
                        rdoExpirableYes.Checked = true;
                    }
                    else
                    {
                        rdoExpirableNo.Checked = true;
                    }

                    if (articleDataModel.articleModel.IsMrpOpen != null && (bool)articleDataModel.articleModel.IsMrpOpen)
                    {
                        rdoOpenMRPYes.Checked = true;
                    }
                    else
                    {
                        rdoOpenMRPNo.Checked = true;
                    }

                    if (articleDataModel.articleModel.ToleranceValue != null && (bool)articleDataModel.articleModel.ToleranceValue)
                    {
                        rdoOpenQtyYes.Checked = true;
                    }
                    else
                    {
                        rdoOpenQtyNo.Checked = true;
                    }

                    if (articleDataModel.articleModel.Printable != null && (bool)articleDataModel.articleModel.Printable)
                    {
                        rdoPrintableYes.Checked = true;
                    }
                    else
                    {
                        rdoPrintableNo.Checked = true;
                    }

                    if (articleDataModel.articleModel.Salable != null && (bool)articleDataModel.articleModel.Salable)
                    {
                        rdoSaleableYes.Checked = true;
                    }
                    else
                    {
                        rdoSaleableNo.Checked = true;
                    }

                   // if (articleDataModel.articleModel.Status != null && (bool)articleDataModel.articleModel.Status)
                    if (articleDataModel.articleModel.ArticleActive != null && (bool)articleDataModel.articleModel.ArticleActive)  //vipin
                    {
                        rdoStatusActive.Checked = true;
                    }
                    else
                    {
                        rdoStatusInActive.Checked = true;
                    }

                    txtMargin.Text = ((double.Parse(txtSalePrice.Text.ToString().Trim()) - double.Parse(txtCostPrice.Text.ToString().Trim())) * 100).ToString();   //vipin on 20-04-2017
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillArticleCharactesticsData(ArticleDataModel articleDataModel)
        {
            try
            {
                string size = CommonFunc.GetEnumStringValue(enumCharacteristics.size).ToUpper();
                string fabric = CommonFunc.GetEnumStringValue(enumCharacteristics.fabric).ToUpper();
                string colour = CommonFunc.GetEnumStringValue(enumCharacteristics.colour).ToUpper();
                string style = CommonFunc.GetEnumStringValue(enumCharacteristics.style).ToUpper();
                for (int charId = 0; charId < articleDataModel.articleCharacteristicMatrixModel.Count; charId++)
                {
                    if (articleDataModel.articleCharacteristicMatrixModel[charId].CharCode.ToString().ToUpper() == size)
                    {
                        cboSize.SelectedValue = articleDataModel.articleCharacteristicMatrixModel[charId].CharValue.ToString();
                    }
                    else
                    {
                        if (articleDataModel.articleCharacteristicMatrixModel[charId].CharCode.ToString().ToUpper() == colour)
                        {
                            cboColour.SelectedValue = articleDataModel.articleCharacteristicMatrixModel[charId].CharValue.ToString();
                        }
                        else
                        {
                            if (articleDataModel.articleCharacteristicMatrixModel[charId].CharCode.ToString().ToUpper() == fabric)
                            {
                                cboFabric.SelectedValue = articleDataModel.articleCharacteristicMatrixModel[charId].CharValue.ToString();
                            }
                            else
                            {
                                if (articleDataModel.articleCharacteristicMatrixModel[charId].CharCode.ToString().ToUpper() == style)
                                {
                                    cboStyle.SelectedValue = articleDataModel.articleCharacteristicMatrixModel[charId].CharValue.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillArticleEANData(ArticleDataModel articleDataModel)
        {
            try
            {
                if (articleDataModel.eanModel != null)
                {
                    for (int BarCodeListcounter = 0; BarCodeListcounter < articleDataModel.eanModel.Count; BarCodeListcounter++)
                    {
                        dgBarCode.Rows.Add();
                        dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeType] = articleDataModel.eanModel[BarCodeListcounter].Discription;
                        dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeValue] = articleDataModel.eanModel[BarCodeListcounter].EAN;
                        //dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.barCodeValue] = chkAutoBarCode.Checked ;

                        dgBarCode.Rows[dgBarCode.Rows.Count - 1][(int)enumBarCode.isdefault] = articleDataModel.eanModel[BarCodeListcounter].DefaultEAN;

                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillArticleTaxData(ArticleDataModel articleDataModel)
        {
            try
            {
                if (articleDataModel.siteArticleTaxMappingModelList != null)
                {
                    for (int taxListcounter = 0; taxListcounter < articleDataModel.siteArticleTaxMappingModelList.Count; taxListcounter++)
                    {
                        dgTax.Rows.Add();
                        dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxName] = articleDataModel.siteArticleTaxMappingModelList[taxListcounter].TaxName;
                        dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxCode] = articleDataModel.siteArticleTaxMappingModelList[taxListcounter].TaxCode;
                        dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.taxValue] = articleDataModel.siteArticleTaxMappingModelList[taxListcounter].TaxValue.ToString();
                        dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.isInclusive] = (articleDataModel.siteArticleTaxMappingModelList[taxListcounter].Inclusive == true) ? "Yes" : "No";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void fillSupplierListData(ArticleDataModel articleDataModel)
        {
            if (articleDataModel.purchaseInfoRecordModelList != null)
            {
                for (int lstSupplierCount = 0; lstSupplierCount < articleDataModel.purchaseInfoRecordModelList.Count; lstSupplierCount++)
                {
                    DropDownModel searchitem = this.rightSupplierList.Where(x => x.Code == articleDataModel.purchaseInfoRecordModelList[lstSupplierCount].SupplierCode).FirstOrDefault();
                    if (searchitem == null)
                    {
                        DropDownModel item = this.supplier.Where(x => x.Code == articleDataModel.purchaseInfoRecordModelList[lstSupplierCount].SupplierCode).FirstOrDefault();
                        rightSupplierList.Add(item);
                        leftSupplierList.RemoveAll(x => x.Code == item.Code);
                        fillSupplierList();
                    }
                }
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
            groupBox4.ForeColor = Color.FromArgb(255, 255, 255);
            BasicTabPage.TabForeColor = Color.Black;
            TabPageBarCode.TabForeColor = Color.Black;
            TabPageSuppplier.TabForeColor = Color.Black;
            TabPageTax.TabForeColor = Color.Black;
            BasicTabPage.TabForeColorSelected = Color.FromArgb(255, 255, 255);
            TabPageBarCode.TabForeColorSelected = Color.FromArgb(255, 255, 255);
            TabPageSuppplier.TabForeColorSelected = Color.FromArgb(255, 255, 255);
            TabPageTax.TabForeColorSelected = Color.FromArgb(255, 255, 255);
            TabPageBarCode.TabBackColorSelected = Color.FromArgb(0, 107, 163);
            TabPageSuppplier.TabBackColorSelected = Color.FromArgb(0, 107, 163);
            TabPageTax.TabBackColorSelected = Color.FromArgb(0, 107, 163);
            BasicTabPage.TabBackColorSelected = Color.FromArgb(0, 107, 163);
            c1DockingTab1.BackColor = Color.FromArgb(134, 134, 134);

            btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnBrowse.BackColor = Color.Transparent;
            btnBrowse.BackColor = Color.FromArgb(0, 107, 163);
            btnBrowse.ForeColor = Color.FromArgb(255, 255, 255);
            btnBrowse.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

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

            btnAddBarCode.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnAddBarCode.BackColor = Color.Transparent;
            btnAddBarCode.BackColor = Color.FromArgb(0, 107, 163);
            btnAddBarCode.ForeColor = Color.FromArgb(255, 255, 255);
            btnAddBarCode.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnAddBarCode.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnAddBarCode.FlatAppearance.BorderSize = 0;
            btnAddBarCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            btnAddTax.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnAddTax.BackColor = Color.Transparent;
            btnAddTax.BackColor = Color.FromArgb(0, 107, 163);
            btnAddTax.ForeColor = Color.FromArgb(255, 255, 255);
            btnAddTax.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnAddTax.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnAddTax.FlatAppearance.BorderSize = 0;
            btnAddTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnBarCodeDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnBarCodeDelete.BackColor = Color.Transparent;
            btnBarCodeDelete.BackColor = Color.FromArgb(0, 107, 163);
            btnBarCodeDelete.ForeColor = Color.FromArgb(255, 255, 255);
            btnBarCodeDelete.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnBarCodeDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnBarCodeDelete.FlatAppearance.BorderSize = 0;
            btnBarCodeDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            btnLeft.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnLeft.BackColor = Color.Transparent;
            btnLeft.BackColor = Color.FromArgb(0, 107, 163);
            btnLeft.ForeColor = Color.FromArgb(255, 255, 255);
            btnLeft.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnLeft.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnLeft.FlatAppearance.BorderSize = 0;
            btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            btnLeftAll.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnLeftAll.BackColor = Color.Transparent;
            btnLeftAll.BackColor = Color.FromArgb(0, 107, 163);
            btnLeftAll.ForeColor = Color.FromArgb(255, 255, 255);
            btnLeftAll.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnLeftAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnLeftAll.FlatAppearance.BorderSize = 0;
            btnLeftAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnRight.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnRight.BackColor = Color.Transparent;
            btnRight.BackColor = Color.FromArgb(0, 107, 163);
            btnRight.ForeColor = Color.FromArgb(255, 255, 255);
            btnRight.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnRight.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnRight.FlatAppearance.BorderSize = 0;
            btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnRightAll.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnRightAll.BackColor = Color.Transparent;
            btnRightAll.BackColor = Color.FromArgb(0, 107, 163);
            btnRightAll.ForeColor = Color.FromArgb(255, 255, 255);
            btnRightAll.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnRightAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnRightAll.FlatAppearance.BorderSize = 0;
            btnRightAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;




            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            rdoExpirableNo.BackColor = Color.FromArgb(134, 134, 134);
            rdoExpirableYes.BackColor = Color.FromArgb(134, 134, 134);
            rdoOpenMRPNo.BackColor = Color.FromArgb(134, 134, 134);
            rdoOpenMRPYes.BackColor = Color.FromArgb(134, 134, 134);
            rdoOpenQtyNo.BackColor = Color.FromArgb(134, 134, 134);
            rdoOpenQtyYes.BackColor = Color.FromArgb(134, 134, 134);
            rdoPrintableNo.BackColor = Color.FromArgb(134, 134, 134);
            rdoPrintableYes.BackColor = Color.FromArgb(134, 134, 134);
            rdoSaleableNo.BackColor = Color.FromArgb(134, 134, 134);
            rdoSaleableYes.BackColor = Color.FromArgb(134, 134, 134);
            rdoStatusActive.BackColor = Color.FromArgb(134, 134, 134);
            rdoStatusInActive.BackColor = Color.FromArgb(134, 134, 134);

            rdoExpirableNo.ForeColor = Color.White;
            rdoExpirableYes.ForeColor = Color.White;
            rdoOpenMRPNo.ForeColor = Color.White;
            rdoOpenMRPYes.ForeColor = Color.White;
            rdoOpenQtyNo.ForeColor = Color.White;
            rdoOpenQtyYes.ForeColor = Color.White;
            rdoPrintableNo.ForeColor = Color.White;
            rdoPrintableYes.ForeColor = Color.White;
            rdoSaleableNo.ForeColor = Color.White;
            rdoSaleableYes.ForeColor = Color.White;
            rdoStatusActive.ForeColor = Color.White;
            rdoStatusInActive.ForeColor = Color.White;


            btnTaxDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnTaxDelete.BackColor = Color.Transparent;
            btnTaxDelete.BackColor = Color.FromArgb(0, 107, 163);
            btnTaxDelete.ForeColor = Color.FromArgb(255, 255, 255);
            btnTaxDelete.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnTaxDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnTaxDelete.FlatAppearance.BorderSize = 0;
            btnTaxDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            lblArticleCode.BackColor = Color.FromArgb(212, 212, 212);

            lblBarCodeType.BackColor = Color.FromArgb(212, 212, 212);
            lblBaseUnitofMeasure.BackColor = Color.FromArgb(212, 212, 212);
            lblBrand.BackColor = Color.FromArgb(212, 212, 212);
            lblCostPrice.BackColor = Color.FromArgb(212, 212, 212);
            lblExpirable.BackColor = Color.FromArgb(212, 212, 212);
            lblItemDescription.BackColor = Color.FromArgb(212, 212, 212);
            lblItemShortName.BackColor = Color.FromArgb(212, 212, 212);
            lblItemTree.BackColor = Color.FromArgb(212, 212, 212);
            lblItemType.BackColor = Color.FromArgb(212, 212, 212);
            lblLastNodeCode.BackColor = Color.FromArgb(212, 212, 212);
            lblLegacyCode.BackColor = Color.FromArgb(212, 212, 212);

            lblMargin.BackColor = Color.FromArgb(212, 212, 212);

            lblMRP.BackColor = Color.FromArgb(212, 212, 212);
            lblOrderUnitofMeasure.BackColor = Color.FromArgb(212, 212, 212);
            lblParentItemCode.BackColor = Color.FromArgb(212, 212, 212);


            label1.BackColor = Color.FromArgb(212, 212, 212);
            label10.BackColor = Color.FromArgb(212, 212, 212);
            label11.BackColor = Color.FromArgb(212, 212, 212);
            label12.BackColor = Color.FromArgb(212, 212, 212);
            label13.BackColor = Color.FromArgb(212, 212, 212);
            label14.BackColor = Color.FromArgb(212, 212, 212);
            label15.BackColor = Color.FromArgb(212, 212, 212);
            label18.BackColor = Color.FromArgb(212, 212, 212);
            label2.BackColor = Color.FromArgb(212, 212, 212);
            label3.BackColor = Color.FromArgb(212, 212, 212);
            label4.BackColor = Color.FromArgb(212, 212, 212);
            label5.BackColor = Color.FromArgb(212, 212, 212);
            label6.BackColor = Color.FromArgb(212, 212, 212);
            label7.BackColor = Color.FromArgb(212, 212, 212);
            label8.BackColor = Color.FromArgb(212, 212, 212);
            label9.BackColor = Color.FromArgb(212, 212, 212);

            chkAutoBarCode.BackColor = Color.FromArgb(212, 212, 212);
            lblPrintable.BackColor = Color.FromArgb(212, 212, 212);
            lblSaleable.BackColor = Color.FromArgb(212, 212, 212);
            lblSalePrice.BackColor = Color.FromArgb(212, 212, 212);
            lblStatus.BackColor = Color.FromArgb(212, 212, 212);
            lblTax.BackColor = Color.FromArgb(212, 212, 212);

            sizerBasicItemDetails.BackColor = Color.FromArgb(134, 134, 134);
            TabPageBarCode.BackColor = Color.FromArgb(134, 134, 134);
            TabPageSuppplier.BackColor = Color.FromArgb(134, 134, 134);
            TabPageTax.BackColor = Color.FromArgb(134, 134, 134);
            BasicTabPage.BackColor = Color.FromArgb(134, 134, 134);

            panel1.BackColor = Color.FromArgb(134, 134, 134);
            panel2.BackColor = Color.FromArgb(134, 134, 134);
            panel3.BackColor = Color.FromArgb(134, 134, 134);
            panel4.BackColor = Color.FromArgb(134, 134, 134);

            panel5.BackColor = Color.FromArgb(134, 134, 134);
            label18.BackColor = Color.FromArgb(212, 212, 212);
            panel6.BackColor = Color.FromArgb(134, 134, 134);
            groupBox4.BackColor = Color.FromArgb(134, 134, 134);
            dgBarCode.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            dgBarCode.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            dgBarCode.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            dgBarCode.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            dgBarCode.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgBarCode.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgBarCode.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgBarCode.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            dgBarCode.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
            //dgReprint.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold);
            dgBarCode.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            dgTax.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            dgTax.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            dgTax.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            dgTax.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            dgTax.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTax.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTax.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTax.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTax.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            dgTax.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            dgTax.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);

            BtnBack.VisualStyle = C1.Win.C1Input.VisualStyle.System;  // added by vipin on 23-03-2016 : Redirect to article List Btn
            BtnBack.BackColor = Color.Transparent;
            BtnBack.BackColor = Color.FromArgb(0, 107, 163);
            BtnBack.ForeColor = Color.FromArgb(255, 255, 255);
            BtnBack.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            BtnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            BtnBack.FlatAppearance.BorderSize = 0;
            BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            clearForm();
            txtLastNodeCode.Text = string.Empty; //vipin  on 27.03.2017
            IsHirarchyPopupShow = false;
            txtLastNodeCode_Enter(sender, e);
            return;
        }



        private void txtSalePrice_Leave(object sender, EventArgs e)
        {
            if (txtSalePrice.Text != "")
            {
                isSalePriceValid = true;
                if (txtCostPrice.Text != "")
                {
                    if (double.Parse(txtSalePrice.Text.ToString().Trim()) < double.Parse(txtCostPrice.Text.ToString().Trim()))
                        {
                            //txtSalePrice.Focus();
                            txtMargin.Text = string.Empty;
                            isSalePriceValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, "Sale price should greater than cost price."))
                            {

                            }
                            //    CommonFunc.ShowMessage("Sale price should greater than cost price.", MessageType.Information); //vipin on 21-04-2014

                        }
                        else
                        {
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, ""))
                            {

                            }
                            isSalePriceValid = true;

                            // txtMargin.Text = ((Int32.Parse(txtSalePrice.Text.ToString().Trim()) - Int32.Parse(txtCostPrice.Text.ToString().Trim())) * 100).ToString();   //vipin on 20-04-2017
                            //Code is commented by irfan on 5/2/2018 for Margin calculation.
                            double salesprice = double.Parse(txtSalePrice.Text.ToString().Trim());
                            double costprice = double.Parse(txtCostPrice.Text.ToString().Trim());
                            double diff = salesprice - costprice;
                            double prf = Math.Round((double)(diff), 2);
                            double profit = prf / costprice;
                            profit = Math.Round((profit), 5);
                            txtMargin.Text = (profit * 100).ToString();
                            return;
                        }
                }
                else
                    txtMargin.Text = string.Empty;
                isSalePriceValid = false;
                if (!CommonFunc.SetCustomErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, "Enter The Cost Price First."))
                {
                    return;
                }
            }
            else                             //code added by irfan for spectrum lite issues.
            {
                txtMargin.Text = string.Empty;
                isSalePriceValid = false;
                if (!CommonFunc.SetCustomErrorProvidertoControl(ref ItemErrorProvider, ref txtSalePrice, "Sale price cannot be left Blank ."))
                {
                    return;
                }
            }


        }

        //code added by irfan for spectrum lite issues.
        private void txtCostPrice_Leave(object sender, EventArgs e)
        {
            txtSalePrice.Enabled = true;
            txtMRP.Enabled = true;
            txtSalePrice.Focus();
        }
        //code added by irfan for spectrum lite issues.
        private void txtCostPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtMRP.Enabled = true;
                //txtSalePrice.Enabled = true;
            }
        }
        //added to fill default supplier label - ashma 26-4-18
        private void lstSupplierRight_DoubleClick(object sender, EventArgs e)
        {
            lblDefaultSupplierValue.Text = lstSupplierRight.SelectedValue.ToString();
            lblDefaultSupplier.Text = lstSupplierRight.SelectedText;
        }

        //code added by vipul for issue id 3260
        private void cboItemType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboItemType.Text == "Kit" || cboItemType.Text == "Combo")
            {
                rdoSaleableNo.Checked = true;
                rdoStatusInActive.Checked = true;
            }
            else
            {
                rdoSaleableYes.Checked = true;
                rdoStatusActive.Checked = true;
            }
        }

    }
}
