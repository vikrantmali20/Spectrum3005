using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;  
namespace Spectrum.BO
{
    public partial class frmTax : Spectrum.Controls.RibbonForm
    {
        public frmTax()
        {
            InitializeComponent();
            this.taxManager = new TaxManager();
            this.commonManager = new CommonManager();
        }

        #region "Class Variable"
        bool flag = false;
        string taxCode = "";
        string doc = "";
        string taxName = "";
        string Dupdoc = "";
        ITaxManager taxManager;
        ICommonManager commonManager;
        IList<DropDownModel> document;
        IList<DropDownModel> AppliedOn; 
        enum enumTax
        {
            Select,
            TaxCode,
            DocumentType,
            TaxName,
            TaxValue,
            TaxValueType
        }

        #endregion

        #region "Events"

        #region " Buttons Events "

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
               
                if (ValidateTax())
                {
                    TaxModel taxdata = new TaxModel();
                    TaxSiteMappingModel taxSiteMap = new TaxSiteMappingModel();
                    TaxSiteDocMapModel taxsitedoc = new TaxSiteDocMapModel(); 
                 
                    taxdata.TaxName = txtTaxName.Text;
                    if (rdoTaxInclusive.Checked == true)
                    {
                        taxdata.Inclusive = true;
                        taxsitedoc.Inclusive = true;
                    }
                    else
                    {
                        taxdata.Inclusive = false;
                        taxsitedoc.Inclusive = false;
                    }

                    if (rdoTVTPercent.Checked == true)
                    {
                        taxdata.Type = "Per";
                        taxsitedoc.IsPercentageValue = true;
                    }
                    else
                    {
                        taxdata.Type = "Val";
                        taxsitedoc.IsPercentageValue = false;
                    }
                    if (chkInterStateTax.Checked == true)
                        taxdata.InterStateTax = true;
                    else
                        taxdata.InterStateTax = false;

                    taxdata.TaxSeqProfile = "A";
                    taxdata.Value = Convert.ToDecimal(txtTaxValue.Text);
                    taxsitedoc.TaxValue = Convert.ToDecimal(txtTaxValue.Text);
                    taxdata.UpdatedAt = CommonModel.SiteCode;
                    taxdata.UpdatedBy = CommonModel.UserID;
                    
                    bool Status = false;
                    if (flag == true && taxCode != "")
                    {
                        Dupdoc = cmbDocumentType.SelectedValue.ToString();
                       // taxName = txtTaxName.Text;
                        bool Duplicate = false;
                        taxdata.TaxCode = taxCode;
                        taxsitedoc.TaxCode = taxCode;
                        taxsitedoc.DocumentType = cmbDocumentType.SelectedValue.ToString();
                        taxsitedoc.UpdatedAt = CommonModel.SiteCode;
                        taxsitedoc.UpdatedBy = CommonModel.UserID;
                        taxsitedoc.TaxName = txtTaxName.Text;
                        taxsitedoc.SiteCode = CommonModel.SiteCode;
                        taxsitedoc.IsDocumentLevelTax = false;
                        taxsitedoc.Appliedon = cmbAppliedon.SelectedValue.ToString();

                        Duplicate = this.taxManager.DuplicateRecords(taxName, Dupdoc, taxCode);
                        //if (taxsitedoc.DocumentType != doc)
                        //{
                        //    Status = this.taxManager.UpdateTax(taxdata, taxsitedoc);
                        //    if (Status == true)
                        //        CommonFunc.ShowMessage("Tax " + taxCode + " Updated Successfully.", MessageType.Information);
                        //    else
                        //        CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                        //}
                        if (Duplicate == false)
                        {

                            Status = this.taxManager.UpdateTax(taxdata, taxsitedoc);
                            CommonFunc.ShowMessage("Tax " + taxCode + " Updated Successfully.", MessageType.Information);
                        }
                        else
                        {
                            Status = this.taxManager.UpdateTaxDoc(taxdata, taxsitedoc);
                            CommonFunc.ShowMessage("Tax " + taxCode + " Updated Successfully.", MessageType.Information);

                        }
                    }
                    else
                    {
                        string newtaxcode = GetTaxCode("C");
                        taxdata.TaxCode = newtaxcode;
                        taxSiteMap.Taxcode = newtaxcode;
                        taxSiteMap.Sitecode = CommonModel.SiteCode;
                        taxSiteMap.Defaultsite = false;

                        taxsitedoc.TaxCode = newtaxcode;
                        taxsitedoc.SiteCode = CommonModel.SiteCode;

                        taxsitedoc.DocumentType = cmbDocumentType.SelectedValue.ToString();
                        taxsitedoc.Appliedon = cmbAppliedon.SelectedValue.ToString();

                        taxsitedoc.TaxName = txtTaxName.Text;
                        taxsitedoc.IsDocumentLevelTax = false;
                        Status = this.taxManager.SaveTax(taxdata, taxSiteMap, taxsitedoc);
                     

                        if (Status == true)
                        {
                            this.commonManager.UpdateNextID(CommonModel.SiteCode, "TX");
                            CommonFunc.ShowMessage("Tax " + newtaxcode + " Added Successfully.", MessageType.Information);
                        }
                        else
                            CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                    }

                    Fillgrid();

                    clearForm();
                }
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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("Are You Sure? All the changes will be lost", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("Are You Sure? All entered data will be clear", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    clearForm();
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnArticleDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool rowDeleted = false;
                for (int rowBarCode = 1; rowBarCode < dgTax.Rows.Count; rowBarCode++)
                {
                    if (Convert.ToBoolean(dgTax.Rows[rowBarCode][(int)enumTax.Select]) == true)
                    {
                        rowDeleted = true;
                        break;
                    }

                }
                if (rowDeleted == false)
                {
                    if (dgTax.Rows.Count > 1 )
                    {
                        CommonFunc.ShowMessage("Please select at least 1 record to delete", Models.Enums.MessageType.Information);
                    }
                    else
                    {
                        CommonFunc.ShowMessage("No Record found", Models.Enums.MessageType.Information);
                    }
                   return;
                }
                if (CommonFunc.ShowMessage("The selected record(s) will be deleted. Are you sure?” ", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    int ArticleCodeRowCount = dgTax.Rows.Count;
                    var deleteArticleCodeRow = new List<C1.Win.C1FlexGrid.Row>();
                    bool status = false;
                    for (int row = 1; row < ArticleCodeRowCount; row++)
                    {
                        if (Convert.ToBoolean(dgTax.Rows[row][(int)enumTax.Select]) == true)
                        {
                           //status = this.taxManager.DeleteByID(dgTax.Rows[row][(int)enumTax.TaxCode].ToString());
                            status = this.taxManager.DeleteByID(dgTax.Rows[row][(int)enumTax.TaxCode].ToString(), dgTax.Rows[row][(int)enumTax.DocumentType].ToString());
                        }
                    }

                    Fillgrid();

                    chkAll.Checked = false;
                }


            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region " Other Events "

        private void frmTax_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }

                this.document = (from result in this.commonManager.GetDocumentType(CommonModel.SiteCode)
                                 select new DropDownModel
                                 {
                                     Code = result.DocumentType,
                                     Description = result.DocumentTypeDescription
                                 }).ToList();
                document.Insert(0, new DropDownModel { Code = null, Description = "Select" });

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbDocumentType, this.document);

                this.AppliedOn = (from result in this.commonManager.GetAppliedOn(CommonModel.SiteCode)
                                  select new DropDownModel
                                  {
                                      Code = result.LongDesc,
                                      Description = result.LongDesc
                                  }).ToList();
                AppliedOn.Insert(0, new DropDownModel { Code = null, Description = "Select" });

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbAppliedon, this.AppliedOn);


                //DataTable dt = new DataTable();
                //dt.Columns.Add("Code");
                //dt.Columns.Add("Description");
                //dt.Rows.Add("0", "Select");
                //dt.Rows.Add("MRP", "MRP");
                //dt.Rows.Add("NetAmount", "Net Amount");
                //dt.Rows.Add("GrossAmount", "Gross Amount");
                //CommonFunc.PopulateComboBoxDataForWindowsForm(ref cmbAppliedon, dt);

                this.TaxActionButton.btnSave.Click += new System.EventHandler(btnSave_Click);
                this.TaxActionButton.btnCancel.Click += new System.EventHandler(btnCancel_Click);
                this.TaxActionButton.btnClear.Click += new EventHandler(btnClear_Click);

                setInitialFormsValidations();

                Fillgrid();
                //CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);
                lblAppliedOn.MandatoryLabelText = "*";
                lblAppliedOn.NormalLabelText = "Applied On";
                lblTaxName.MandatoryLabelText = "*";
                lblTaxValue.MandatoryLabelText = "*";
                lblTaxValueType.MandatoryLabelText = "*";
                lbldocumenttype.MandatoryLabelText = "*";
                DefaultGridSetting();
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }

        private void dgTax_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                 int gridCount = dgTax.Rows.Count;
                 if (gridCount > 1)
                 {
                     clearForm();
                     taxCode = dgTax.Rows[dgTax.Row][1].ToString();
                     doc = dgTax.Rows[dgTax.Row][2].ToString();
                     taxName = dgTax.Rows[dgTax.Row][3].ToString();
                     if (taxCode != "Tax Code")
                     {
                         TaxModel taxdata = new TaxModel();
                         TaxSiteDocMapModel taxsitedocmap = new TaxSiteDocMapModel(); 
                         taxdata = this.taxManager.GetTaxByID(taxCode);
                         taxsitedocmap = this.taxManager.GetTaxByDoc(taxCode, doc); 
                         if (taxdata != null)
                         {
                             txtTaxName.Text = taxdata.TaxName;
                             txtTaxValue.Value = taxdata.Value;
                             cmbDocumentType.SelectedValue = taxsitedocmap.DocumentType;
                             cmbAppliedon.SelectedValue = taxsitedocmap.Appliedon;   
                             if (taxdata.Type == "Per")
                                 rdoTVTPercent.Checked = true;
                             else
                                 rdoTVTValue.Checked = true;

                             if (taxdata.Inclusive == true)
                                 rdoTaxInclusive.Checked = true;
                             else
                                 rdoTaxExclusive.Checked = true;

                             if (taxdata.InterStateTax == true)
                                 chkInterStateTax.Checked = true;
                             else
                                 chkInterStateTax.Checked = false;
                             flag = true;
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = chkAll.Checked;
                for (int rowArticleCode = 1; rowArticleCode < dgTax.Rows.Count; rowArticleCode++)
                {
                    dgTax.Rows[rowArticleCode][(int)enumTax.Select] = chk;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #endregion

        #region " Functions "

        private bool ValidateTax()
        {
            try
            {
                bool isValid = true;

                if (string.IsNullOrEmpty(txtTaxName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtTaxName, "Tax Name Required"))
                    {
                        this.txtTaxName.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    Regex patt = new Regex("^[A-Za-z0-9-.% ]+$");
                    if (patt.IsMatch(txtTaxName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxName, "Tax Name Allows AlphaNumeric,space,%,-", false))
                        {
                            this.txtTaxName.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtTaxName, string.Empty);
                        txtTaxName.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (string.IsNullOrEmpty(txtTaxValue.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtTaxValue, "Tax Value Required"))
                    {
                        this.txtTaxValue.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    decimal taxval = Convert.ToDecimal(txtTaxValue.Value.ToString().Trim ());
                    if (taxval <= 0)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxValue, "Value Should be Greater Than 0", false))
                        {
                            this.txtTaxValue.Focus();
                            return isValid;
                        }
                    }
                    if (rdoTVTPercent.Checked == true)
                    {
                        if (taxval > Convert.ToDecimal("100.00"))
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxValue, "Tax Value Can Not be Greater Than 100", false))
                            {
                                this.txtTaxValue.Focus();
                                return isValid;
                            }

                        }
                    }
                    else
                    {
                        if (taxval > Convert.ToDecimal("1000000.00"))
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxValue, "Tax Value Can Not be Greater Than 1000000", false))
                            {
                                this.txtTaxValue.Focus();
                                return isValid;
                            }

                        }
                    }

                    if (txtTaxValue.Text.Contains("."))
                    {
                        string[] decKeys = txtTaxValue.Text.Split('.');
                        if (decKeys[1].Length > 2)
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxValue, "Only Two Decimal Digits Allowed", false))
                            {
                                this.txtTaxValue.Focus();
                            }

                        }
                        else
                        {
                            errorProvider.SetError(txtTaxValue, string.Empty);
                            txtTaxValue.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                    }

                    else
                    {
                        errorProvider.SetError(txtTaxValue, string.Empty);
                        txtTaxValue.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                if ((cmbDocumentType.SelectedValue == null))
                {
                    if (!CommonFunc.SetErrorProviderComboControl(ref errorProvider, ref cmbDocumentType, "Please Select Document Type", false))
                    {
                        this.cmbDocumentType.Focus();
                        isValid = false;
                    }
                    //CommonFunc.ShowMessage("Required", Models.Enums.MessageType.Information);

                }

                if ((cmbAppliedon.SelectedValue == null))
                {
                    if (!CommonFunc.SetErrorProviderComboControl(ref errorProvider, ref cmbAppliedon, "Please Select Applied On ", false))
                    {
                        this.cmbAppliedon.Focus();
                        isValid = false;
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void setInitialFormsValidations()
        {
            try
            {
                txtTaxName.MaxLength = 50;
                rdoTVTPercent.Checked = true;
                // txtTaxValue.DataType = typeof(double);
                //rdoTaxInclusive.Checked = true;
                rdoTaxExclusive.Checked = true;
                txtTaxName.Focus();
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Fillgrid()
        {
            try
            {

                int gridCount = dgTax.Rows.Count;
                if (gridCount > 1)
                    dgTax.Rows.RemoveRange(1, dgTax.Rows.Count - 1);

                var taxList = (from tax in this.taxManager.GetTaxList()
                               select tax).ToList();

                var taxDoc = (from doc in this.taxManager.GetTaxDoc()
                              select doc).ToList();

                var taxData = (from tl in taxList join td in taxDoc on tl.TaxCode equals td.TaxCode where tl.Status == true && td.Status == true select new { tl.TaxCode, tl.TaxName, tl.Value, tl.Type, td.DocumentType }).ToList();

                //var taxData = (from tax in this.taxManager.GetTaxList()
                //               select tax).ToList();
                foreach (var item in taxData)
                {
                    dgTax.Rows.Add();
                    dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxCode] = item.TaxCode.ToString();
                    dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.DocumentType] = item.DocumentType.ToString();  
                    dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxName] = item.TaxName.ToString();
                    dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxValue] = Convert.ToDecimal(item.Value);
                    dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxValueType] = item.Type.ToString();

                }

                DefaultGridSetting();
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
                int gridCount = dgTax.Rows.Count;
                if (gridCount>1)
                {
                    dgTax.Cols["TaxCode"].AllowSorting = true;
                    dgTax.Cols["DocumentType"].AllowSorting = true;
                    dgTax.Cols["TaxName"].AllowSorting = true;
                    dgTax.Cols["taxvaluetype"].AllowSorting = true;
                    dgTax.Cols["taxvalue"].AllowSorting = true;
                }
                else
                {
                    dgTax.Cols["TaxCode"].AllowSorting = false;
                    dgTax.Cols["DocumentType"].AllowSorting = false;
                    dgTax.Cols["TaxName"].AllowSorting = false;
                    dgTax.Cols["taxvaluetype"].AllowSorting = false;
                    dgTax.Cols["taxvalue"].AllowSorting = false;
                }
                dgTax.Cols["Select"].Width = 45;
                dgTax.Cols["Select"].Caption = string.Empty;
                dgTax.Cols["Select"].AllowEditing = true;
                dgTax.Cols["Select"].AllowSorting = false;
                dgTax.Cols["Select"].AllowResizing = false;
                dgTax.Cols["TaxCode"].Width = 200;
                
                dgTax.Cols["TaxCode"].AllowEditing = false;
                dgTax.Cols["TaxCode"].AllowResizing = false;

                dgTax.Cols["DocumentType"].Width = 150;
                dgTax.Cols["DocumentType"].Caption = "Document Type";
                dgTax.Cols["DocumentType"].AllowEditing = false;
                dgTax.Cols["DocumentType"].AllowResizing = false;
                dgTax.Cols["TaxName"].Width = 150;
                
                dgTax.Cols["TaxName"].AllowEditing = false;
                dgTax.Cols["TaxName"].AllowResizing = false;
                dgTax.Cols["taxvaluetype"].Width = 105;
                
                dgTax.Cols["taxvaluetype"].AllowEditing = false;
                dgTax.Cols["taxvaluetype"].AllowResizing = false;
                dgTax.Cols["taxvalue"].Width = 135;
                
                dgTax.Cols["taxvalue"].AllowEditing = false;
                dgTax.Cols["taxvalue"].AllowResizing = false;

                flag = false;
                taxCode = "";
                //txtTaxName.Focus();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void clearForm()
        {
            this.txtTaxName.Value = String.Empty;
            this.txtTaxValue.Value = String.Empty;
           // rdoTaxInclusive.Checked = true;
            rdoTaxExclusive.Checked = true;
            rdoTVTPercent.Checked = true;
            flag = false;
            taxCode = "";
            txtTaxValue.BorderColor = CommonFunc.DefaultBorderColor;
            txtTaxName.BorderColor = CommonFunc.DefaultBorderColor;
            //clear error indicator
            errorProvider.SetError(txtTaxName, string.Empty);
            txtTaxName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtTaxValue, string.Empty);
            txtTaxValue.BorderColor = CommonFunc.DefaultBorderColor;
            cmbDocumentType.SelectedValue = string.Empty;
            cmbAppliedon.SelectedValue = string.Empty;
            chkInterStateTax.Checked = false;
            errorProvider.SetError(cmbDocumentType, string.Empty);
            errorProvider.SetError(cmbAppliedon, string.Empty);
        }

        private string GetTaxCode(string car)
        {
            string sitecode = CommonModel.SiteCode;
            int nextNo = commonManager.GetNextID(sitecode, "TX");
            nextNo = nextNo + 1;
            int padLimit = 10;
            if (nextNo.ToString().Length > 1)
                padLimit = padLimit + (nextNo.ToString().Length - 1);
            string strlastcode = string.Format("{0}", nextNo.ToString().PadLeft(padLimit - Convert.ToString(nextNo).Length, '0'));
            //string strlastcode = string.Format("0000{0}", nextNo.ToString().PadLeft(8 - Convert.ToString(nextNo).Length, '0'));
            string strtaxcode = "TX" + car + sitecode.Substring(sitecode.Length - 3) + strlastcode;
            return strtaxcode;
        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            groupBox1.BackColor = Color.FromArgb(134, 134, 134);
            groupBox2.BackColor = Color.FromArgb(134, 134, 134);

            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);

            TaxActionButton.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            TaxActionButton.btnCancel.BackColor = Color.Transparent;
            TaxActionButton.btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            TaxActionButton.btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            TaxActionButton.btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            TaxActionButton.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            TaxActionButton.btnCancel.FlatAppearance.BorderSize = 0;
            TaxActionButton.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            TaxActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            TaxActionButton.btnSave.BackColor = Color.Transparent;
            TaxActionButton.btnSave.BackColor = Color.FromArgb(0, 107, 163);
            TaxActionButton.btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            TaxActionButton.btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            TaxActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            TaxActionButton.btnSave.FlatAppearance.BorderSize = 0;
            TaxActionButton.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            TaxActionButton.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            TaxActionButton.btnClear.BackColor = Color.Transparent;
            TaxActionButton.btnClear.BackColor = Color.FromArgb(0, 107, 163);
            TaxActionButton.btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            TaxActionButton.btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            TaxActionButton.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            TaxActionButton.btnClear.FlatAppearance.BorderSize = 0;
            TaxActionButton.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

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

          //  panel1.BackColor = Color.FromArgb(212, 212, 212);
          ////  panel2.BackColor = Color.FromArgb(212, 212, 212);
          //  lblTaxName.BackColor = Color.FromArgb(212, 212, 212);
          //  lblTaxValue.BackColor = Color.FromArgb(212, 212, 212);
          //  lblTaxValueType.BackColor = Color.FromArgb(212, 212, 212);
          //  lbldocumenttype.BackColor = Color.FromArgb(212, 212, 212);
          //  lblAppliedOn.BackColor = Color.FromArgb(212, 212, 212);

        }
        #endregion
        private void frmTax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }

        }

        private void cmbDocumentType_TextChanged(object sender, EventArgs e)
        {
            if (cmbDocumentType.SelectedValue != null)
            {
                if (cmbDocumentType.SelectedValue.ToString() == "CMS" || cmbDocumentType.SelectedValue.ToString() == "SO201")
                {
                    cmbAppliedon.SelectedValue = "MRP";
                }
                //else
                //{
                //    cmbAppliedon.SelectedValue = string.Empty;
                //    cmbAppliedon.SelectedIndex = 0;  
                //}
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {


            FilterCreditSales();

        }


        private void FilterCreditSales()
        {
            try
            {
                using (var context = Spectrum.DAL.ContextFactory.CreateContext())

                // using (var context = Spectrum.DAL.ContextFactory.CreateContext())
                {

                    var query = (from t in context.MstTax join tm in context.TaxSiteDocMap on t.TaxCode equals tm.TaxCode where t.STATUS == true && tm.STATUS == true select new { t.TaxCode, t.TaxName, t.Value, t.Type, tm.DocumentType }).ToList();

                    dgTax.Rows.RemoveRange(1, dgTax.Rows.Count - 1);
                    foreach (var item in query)
                    {
                        if (item.TaxName.ToUpper().Contains(txtSearch.Text.Trim().ToUpper()) || item.DocumentType.ToUpper().Contains(txtSearch.Text.Trim().ToUpper()))
                        {
                            dgTax.Rows.Add();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxCode] = item.TaxCode.ToString();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.DocumentType] = item.DocumentType.ToString();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxName] = item.TaxName.ToString();
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxValue] = Convert.ToDecimal(item.Value);
                            dgTax.Rows[dgTax.Rows.Count - 1][(int)enumTax.TaxValueType] = item.Type.ToString();
                        }
                    }

                }



                DefaultGridSetting();
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
            }
        }


        private void txtTaxName_Click(object sender, EventArgs e)
        {
            onTouchKeyBoard();
        }

        private void onTouchKeyBoard()
        {

            try
            {

                using (var context = Spectrum.DAL.ContextFactory.CreateContext())
                {
                    var query = (from t in context.DefaultConfig where t.Sitecode == CommonModel.SiteCode && t.STATUS == true && t.FldLabel == "AllowOnScreenKeyBoard" select t.FldValue).SingleOrDefault();
                    if (query.ToUpper() == "TRUE")
                    {
                        var processs = Process.GetProcessesByName("osk");

                        if (processs.Length == 0)
                        {
                            Process.Start("osk");
                        }
                        else
                        {
                            foreach (Process processShow in Process.GetProcessesByName("osk"))
                            {
                                if (processShow.ProcessName == "osk")
                                {
                                    processShow.Kill();
                                    Process.Start("osk");
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
            }
        }

        private void txtTaxValue_Click(object sender, EventArgs e)
        {
            onTouchKeyBoard();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            onTouchKeyBoard();
        }

    }
}
