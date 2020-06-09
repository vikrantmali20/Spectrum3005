using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmSiteDetails : Spectrum.Controls.RibbonForm
    {
        public frmSiteDetails()
        {
            InitializeComponent();
            this.commonManager = new CommonManager();
            this.siteManager = new SiteManager();
        }

        #region "Class Variable"

        ICommonManager commonManager;
        ISiteManager siteManager;
        IQueryable<AreaCodeModel> countryCodeList;
        //IQueryable<MstCurrencyModel> currencyCodeList;
        AutoCompleteStringCollection countryCollection = new AutoCompleteStringCollection();
        AutoCompleteStringCollection currencyCollection = new AutoCompleteStringCollection();
        bool flagAddEdit = false;

        #endregion

        #region " Functions "        

        private void fillCountries()
        {
            this.countryCodeList = commonManager.GetAreaCodeList();
            var countryList = (from p in countryCodeList
                               where p.AreaType == (int)MaterType.Country
                               select new DropDownModel
                               {
                                   Code = p.AreaCode,
                                   Description = p.Description
                               }).ToList();
            if (countryList.Count > 0)
            {
                foreach (var cntryItem in countryList)
                {
                    countryCollection.Add(cntryItem.Code.ToString());
                }
            }
            txtCountry.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCountry.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void fillCurrencies()
        {


            var currencyList = (from result in this.commonManager.GetCurrency()
                                select new DropDownModel
                                {
                                    Code = result.CurrencyCode,
                                    Description = result.CurrencyDescription
                                }).ToList();
            if (currencyList.Count > 0)
            {
                foreach (var currencyItem in currencyList)
                {
                    currencyCollection.Add(currencyItem.Code.ToString());
                }
            }
            txtCurrencyCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCurrencyCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private bool IsFormValidate()
        {
            try
            {
                bool isValid = true;
                bool isSiteValid = true;


                Regex sitecodepatt = new Regex("^[A-Za-z0-9]+$");
                Regex alphanumericpatt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");
                Regex numberpatt = new Regex("^[0-9]+$");
                Regex alphapatt = new Regex("^[A-Za-z ]+$");
                if (string.IsNullOrEmpty(txtSiteCode.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtSiteCode, "Site Code Required"))
                    {
                        this.txtSiteCode.Focus();
                        isValid = false;
                       
                    }
                }
                else
                {
                    if (sitecodepatt.IsMatch(txtSiteCode.Text) == false)
                    {
                        isValid = false;
                        isSiteValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtSiteCode, "Site Code Allows Alphanumeric Character", false))
                        {
                            this.txtSiteCode.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtSiteCode, string.Empty);
                        txtSiteCode.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtCorporateName.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtCorporateName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtCorporateName, "Corporate Name Allows Alphanumeric Character", false))
                        {
                            this.txtCorporateName.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtCorporateName, string.Empty);
                        txtCorporateName.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }


                if (string.IsNullOrEmpty(txtOfficialName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtOfficialName, "Site Name Required"))
                    {
                        this.txtSiteCode.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    if (alphanumericpatt.IsMatch(txtOfficialName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtOfficialName, "Site Name Allows Alphanumeric Character", false))
                        {
                            this.txtSiteCode.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtOfficialName, string.Empty);
                        txtOfficialName.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtAddress1.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtAddress1.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtAddress1, "Address Allows Alphanumeric Character", false))
                        {
                            this.txtAddress1.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtAddress1, string.Empty);
                        txtAddress1.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtAddress2.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtAddress2.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtAddress2, "Address Allows Alphanumeric Character", false))
                        {
                            this.txtAddress2.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtAddress2, string.Empty);
                        txtAddress2.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtAddress3.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtAddress3.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtAddress3, "Address Allows Alphanumeric Character", false))
                        {
                            this.txtAddress3.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtAddress3, string.Empty);
                        txtAddress3.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (string.IsNullOrEmpty(txtCountry.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtCountry, "Country Code Required"))
                    {
                        this.txtCountry.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    if (alphapatt.IsMatch(txtCountry.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtCountry, "Country Allows Character Only", false))
                        {
                            this.txtCountry.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtCountry, string.Empty);
                        txtCountry.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                if (string.IsNullOrEmpty(txtCurrencyCode.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtCurrencyCode, "Currency Code Required"))
                    {
                        this.txtCurrencyCode.Focus();
                        isValid = false;
                    }
                }

                else
                {
                    errorProvider.SetError(txtCurrencyCode, string.Empty);
                    txtCurrencyCode.BorderColor = CommonFunc.DefaultBorderColor;
                    var IsCurrencyExist = this.siteManager.GetCurrencySymbol(txtCurrencyCode.Text.Trim());
                    if (IsCurrencyExist == null)
                    {
                        if (string.IsNullOrEmpty(txtCurrencySymol.Text.Trim()))
                        {
                            if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtCurrencySymol, "Currency Symbol Required"))
                            {
                                this.txtCurrencySymol.Focus();
                                isValid = false;
                            }
                        }
                        else
                        {
                            errorProvider.SetError(txtCurrencySymol, string.Empty);
                            txtCurrencySymol.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtCurrencySymol, string.Empty);
                        txtCurrencySymol.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    if (!CommonFunc.validateEmailId(this.txtEmail.Text.ToString()) && !string.IsNullOrEmpty(this.txtEmail.Text.ToString()))
                    {
                        if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtEmail, "E-mail Should in Standard E-mail Format.", true))
                        {
                            isValid = false;
                            txtEmail.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtEmail, string.Empty);
                        txtEmail.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtContactPerson.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtContactPerson, "Contact Person Allows Alphanumeric Character", false))
                        {
                            this.txtContactPerson.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtContactPerson, string.Empty);
                        txtContactPerson.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtContactNumber.Text.Trim()))
                {
                    if (numberpatt.IsMatch(txtContactNumber.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtContactNumber, "Contact Number Allows Numeric Only", false))
                        {
                            this.txtContactNumber.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtContactNumber, string.Empty);
                        txtContactNumber.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtFaxNo.Text.Trim()))
                {
                    if (numberpatt.IsMatch(txtFaxNo.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtFaxNo, "Fax Number Allows Numeric Only", false))
                        {
                            this.txtFaxNo.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtFaxNo, string.Empty);
                        txtFaxNo.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtTaxNo.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtTaxNo.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTaxNo, "Tax Number Allows Alphanumeric Only", false))
                        {
                            this.txtTaxNo.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtTaxNo, string.Empty);
                        txtTaxNo.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtLicenceNo.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtLicenceNo.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtLicenceNo, "License Number Allows Numeric Only", false))
                        {
                            this.txtLicenceNo.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtLicenceNo, string.Empty);
                        txtLicenceNo.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                if (string.IsNullOrEmpty(finYearBeginDate.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref finYearBeginDate, "Financial Year Begin Date required"))
                    {
                        this.finYearBeginDate.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    errorProvider.SetError(finYearBeginDate, string.Empty);
                    finYearBeginDate.BorderColor = CommonFunc.DefaultBorderColor;
                }
                if (string.IsNullOrEmpty(finYearEndDate.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref finYearEndDate, "Financial Year End Date required"))
                    {
                        this.finYearEndDate.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    errorProvider.SetError(finYearEndDate, string.Empty);
                    finYearEndDate.BorderColor = CommonFunc.DefaultBorderColor;
                }
                if (!string.IsNullOrEmpty(finYearBeginDate.Text.Trim()))
                {
                    if (string.IsNullOrEmpty(finYearEndDate.Text.Trim()))
                    {
                        isValid = false;
                        if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref finYearEndDate, "Please select a valid End Date"))
                        {
                            this.finYearEndDate.Focus();
                        }
                    }
                    else
                    {
                        DateTime beginDate = Convert.ToDateTime(finYearBeginDate.Value);
                        DateTime endDate = Convert.ToDateTime(finYearEndDate.Value);
                        int addDays=364;
                        string FiscYear = DateTime.Now.Year.ToString();
                        if (Convert.ToInt32(FiscYear.Substring(FiscYear.Length - 2)) % 4 == 0)
                            addDays = 365;
                        if (endDate >= beginDate.AddDays(addDays))
                        {
                            
                            
                            errorProvider.SetError(finYearEndDate, string.Empty);
                            finYearEndDate.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                        else
                        {
                            isValid = false;
                            if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref finYearEndDate, "Difference between the Start Date and End Date should be 1 year. Please select a valid End Date"))
                            {
                                this.finYearEndDate.Focus();
                            }
                        }



                    }
                }
                if (!string.IsNullOrEmpty(txtSiteCode.Text.Trim()) && flagAddEdit == false && isSiteValid==true)
                {

                    var IsSiteExist = this.siteManager.GetSiteByID(txtSiteCode.Text.Trim());
                    if (IsSiteExist != null)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtSiteCode, "Site Name Already Exist", false))
                        {
                            this.txtSiteCode.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtSiteCode, string.Empty);
                        txtSiteCode.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void clearForm()
        {
            txtSiteCode.ReadOnly = false;
            this.txtSiteCode.Value = String.Empty;
            this.txtCorporateName.Text = String.Empty;
            this.txtOfficialName.Value = String.Empty;
            this.txtAddress1.Value = String.Empty;
            this.txtAddress2.Value = String.Empty;
            this.txtAddress3.Value = String.Empty;
            this.txtLicenceNo.Text = String.Empty;
            txtCountry.ReadOnly = false;
            txtCurrencyCode.ReadOnly = false;
            if (txtCurrencySymol.Enabled)
                txtCurrencySymol.ReadOnly = false;
            else
                txtCurrencySymol.Enabled = true;
            this.txtCountry.Text = String.Empty;
            this.txtCurrencyCode.Text = String.Empty;
            this.txtCurrencySymol.Text = String.Empty;
            this.dtpInstallationDate.Value = String.Empty;

            this.txtEmail.Value = String.Empty;
            this.txtContactPerson.Value = String.Empty;
            this.txtContactNumber.Value = String.Empty;
            this.txtFaxNo.Value = String.Empty;
            this.txtTaxNo.Value = String.Empty;
            finYearBeginDate.ReadOnly = false;
            finYearEndDate.ReadOnly = false;
            this.finYearBeginDate.Value = string.Empty;
            this.finYearEndDate.Value = string.Empty;

            flagAddEdit = false;

            lblInstallationDate.Visible = false;
            dtpInstallationDate.Visible = false;

            //remove error indicator
            errorProvider.SetError(txtSiteCode, string.Empty);
            txtSiteCode.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtCorporateName, string.Empty);
            txtCorporateName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtOfficialName, string.Empty);
            txtOfficialName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtAddress1, string.Empty);
            txtAddress1.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtAddress2, string.Empty);
            txtAddress2.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtAddress3, string.Empty);
            txtAddress3.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtCountry, string.Empty);
            txtCountry.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtCurrencyCode, string.Empty);
            txtCurrencyCode.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtCurrencySymol, string.Empty);
            txtCurrencySymol.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtEmail, string.Empty);
            txtEmail.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtContactPerson, string.Empty);
            txtContactPerson.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtContactNumber, string.Empty);
            txtContactNumber.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtFaxNo, string.Empty);
            txtFaxNo.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtTaxNo, string.Empty);
            txtTaxNo.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtLicenceNo, string.Empty);
            txtLicenceNo.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(finYearBeginDate, string.Empty);
            finYearBeginDate.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(finYearEndDate, string.Empty);
            finYearEndDate.BorderColor = CommonFunc.DefaultBorderColor;
            this.txtSiteCode.Focus();
        }

        private void setInitialFormsValidations()
        {
            try
            {
                txtSiteCode.MaxLength = 15;
                txtOfficialName.MaxLength = 200;
                txtAddress1.MaxLength = 250;
                txtAddress2.MaxLength = 250;
                txtAddress3.MaxLength = 250;
                txtEmail.MaxLength = 150;
                txtContactPerson.MaxLength = 100;
                txtContactNumber.MaxLength = 50;
                dtpInstallationDate.Clear();
                finYearBeginDate.Clear();
                finYearEndDate.Clear();
                flagAddEdit = false;
                lblInstallationDate.Visible = false;
                dtpInstallationDate.Visible = false;
                this.txtSiteCode.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setTabIndex()
        {
            try
            {
                txtSiteCode.TabIndex = 0;
                btnSearch.TabIndex = 1;
                txtCorporateName.TabIndex = 2;
                txtOfficialName.TabIndex = 3;
                txtAddress1.TabIndex = 4;
                txtAddress2.TabIndex = 5;
                txtAddress3.TabIndex = 6;
                txtLicenceNo.TabIndex = 7;
                txtCountry.TabIndex = 8;
                txtCurrencyCode.TabIndex = 9;
                txtCurrencySymol.TabIndex = 10;
                txtContactPerson.TabIndex = 11;
                txtEmail.TabIndex = 12;
                txtContactNumber.TabIndex = 13;
                txtFaxNo.TabIndex = 14;
                txtTaxNo.TabIndex = 15;
                finYearBeginDate.TabIndex = 16;
                finYearEndDate.TabIndex = 17;
                tenderActionButton.btnSave.TabIndex = 18;
                tenderActionButton.btnCancel.TabIndex = 19;
                tenderActionButton.btnClear.TabIndex = 20;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MstFinYearModel FillFinYearModel()
        {
            try
            {
                return new MstFinYearModel
                {
                    SiteCode = txtSiteCode.Text.Trim() ,
                    FinYear = Convert.ToString(DateTime.Now.Year),
                    ValidFromDt = Convert.ToDateTime(finYearBeginDate.Value), //finYearBeginDate.Value == null ? null : finYearBeginDate.Value , //finYearEndDate is DBNull) ? (Nullable<DateTime>)null : (DateTime)dtpInvoiceDate.Value,
                    ValidToDt = Convert.ToDateTime(finYearEndDate.Value),
                    FinStatus = true,
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

        private SiteModel FillSiteModel()
        {
            try
            {
                return new SiteModel
                {
                    SiteCode = txtSiteCode.Text.Trim(),
                    BusinessCode = "Store",
                    SiteShortName = txtOfficialName.Text.Trim(),
                    SiteOfficialName = txtCorporateName.Text.Trim(),
                    SiteAddressLn1 = txtAddress1.Text.Trim(),
                    SiteAddressLn2 = txtAddress2.Text.Trim(),
                    SiteAddressLn3 = txtAddress3.Text.Trim(),
                    EmailId = txtEmail.Text.Trim(),
                    ContactPerson = txtContactPerson.Text.Trim(),
                    SiteTelephone1 = txtContactNumber.Text.Trim(),
                    CountryCode = txtCountry.Value.ToString(),
                    LanguageCode = "EN",
                    defaultean = "EAN13",
                    FaxNo = txtFaxNo.Text.Trim(),
                    LocalSalesTaxNo = txtTaxNo.Text.Trim(),
                    CentralSalesTaxNo = txtLicenceNo.Text.Trim(),
                    LocalCurrancyCode = txtCurrencyCode.Text.Trim(),
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

        private MstSiteCurrancyMapModel FillSiteCurrancyMapModel()
        {
            try
            {
                return new MstSiteCurrancyMapModel
                {
                    SiteCode = txtSiteCode.Text.Trim(),
                    LocalCurrancyCode = txtCurrencyCode.Text.Trim(),
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

        private MstCurrencySiteModel FillCurrencyModel()
        {
            try
            {
                return new MstCurrencySiteModel
                {
                    CurrencyCode = txtCurrencyCode.Text.Trim(),
                    CurrencyDescription = txtCurrencyCode.Text.Trim(),
                    CurrencySymbol = txtCurrencySymol.Text.Trim(),
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

        private MstAreaCodeModel FillAreaCodeModel()
        {
            try
            {
                return new MstAreaCodeModel
                {
                    AreaCode = txtCountry.Text.Trim(),
                    Description = txtCountry.Text.Trim(),
                    AreaType = 101,
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
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);
            c1Sizer2.BackColor = Color.FromArgb(134, 134, 134);
            tenderActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            tenderActionButton.btnSave.BackColor = Color.Transparent;
            tenderActionButton.btnSave.BackColor = Color.FromArgb(0, 107, 163);
            tenderActionButton.btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            tenderActionButton.btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            tenderActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            tenderActionButton.btnSave.FlatAppearance.BorderSize = 0;
            tenderActionButton.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            tenderActionButton.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            tenderActionButton.btnCancel.BackColor = Color.Transparent;
            tenderActionButton.btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            tenderActionButton.btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            tenderActionButton.btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            tenderActionButton.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            tenderActionButton.btnCancel.FlatAppearance.BorderSize = 0;
            tenderActionButton.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            tenderActionButton.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            tenderActionButton.btnClear.BackColor = Color.Transparent;
            tenderActionButton.btnClear.BackColor = Color.FromArgb(0, 107, 163);
            tenderActionButton.btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            tenderActionButton.btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            tenderActionButton.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            tenderActionButton.btnClear.FlatAppearance.BorderSize = 0;
            tenderActionButton.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            lblAddress.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress2.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress3.BackColor = Color.FromArgb(212, 212, 212);
            lblContactNumber.BackColor = Color.FromArgb(212, 212, 212);
            lblContactPerson.BackColor = Color.FromArgb(212, 212, 212);
            lblCorporateName.BackColor = Color.FromArgb(212, 212, 212);
            lblCountry.BackColor = Color.FromArgb(212, 212, 212);
            lblCurrencyCode.BackColor = Color.FromArgb(212, 212, 212);
            lblCurrencySymbol.BackColor = Color.FromArgb(212, 212, 212);
            lblEmail.BackColor = Color.FromArgb(212, 212, 212);
            lblFaxNo.BackColor = Color.FromArgb(212, 212, 212);
            lblFinYearBeginDate.BackColor = Color.FromArgb(212, 212, 212);
            lblFinYearEndDate.BackColor = Color.FromArgb(212, 212, 212);
            lblInstallationDate.BackColor = Color.FromArgb(212, 212, 212);
            lblLicenseNo.BackColor = Color.FromArgb(212, 212, 212);
            lblName.BackColor = Color.FromArgb(212, 212, 212);
            lblSiteCode.BackColor = Color.FromArgb(212, 212, 212);
            lblTaxNo.BackColor = Color.FromArgb(212, 212, 212);

            groupBox1.BackColor = Color.FromArgb(134, 134, 134);
            groupBox1.ForeColor = Color.White;
            groupBox2.BackColor = Color.FromArgb(134, 134, 134);
            groupBox2.ForeColor = Color.White;
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }
        #endregion

        #region "Events"

        #region " Other Events "

        private void frmSiteDetails_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                this.tenderActionButton.btnSave.Click += new System.EventHandler(btnSave_Click);
                this.tenderActionButton.btnCancel.Click += new System.EventHandler(btnCancel_Click);
                this.tenderActionButton.btnClear.Click += new EventHandler(btnClear_Click);                
                fillCountries();
                fillCurrencies();
                setTabIndex();
                setInitialFormsValidations();
                // CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);
                lblTaxNo.Text = "GSTNo.";
                 
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCountry.Text = txtCountry.Text;
                txtCountry.AutoCompleteCustomSource = countryCollection;
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void txtCurrencyCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCurrencyCode.Text = txtCurrencyCode.Text;
                txtCurrencyCode.AutoCompleteCustomSource = currencyCollection;
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }
        private void txtCurrencyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCurrencyCode.Enabled==true  && txtCurrencySymol.Enabled ==true )
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (!string.IsNullOrEmpty(txtCurrencyCode.Text.Trim()))
                    {
                        var CurrencyModel = this.siteManager.GetCurrencySymbol(txtCurrencyCode.Text.Trim());
                        if (CurrencyModel != null)
                        {
                            string CurrencySymbol = CurrencyModel.CurrencySymbol;
                            if (txtCurrencySymol.Enabled)
                            {
                                
                                
                                txtCurrencySymol.Text = CurrencySymbol;
                                txtCurrencySymol.Enabled = false;
                                txtContactPerson.Focus();
                            }
                            else
                            {
                                txtCurrencySymol.Enabled = true;
                                txtCurrencySymol.Text = CurrencySymbol;
                                txtCurrencySymol.Enabled = false;
                                txtContactPerson.Focus();
                            }
                        }
                        else
                        {
                            if (txtCurrencySymol.Enabled)
                            {
                                txtCurrencySymol.Text = "";
                                txtCurrencySymol.Focus();
                            }
                            else
                            {
                                txtCurrencySymol.Enabled = true;
                                txtCurrencySymol.Text = "";
                                txtCurrencySymol.Focus();
                            }
                        }
                    }
                }
            }
        }

         

        #endregion

        #region " Button Events "

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ((IsFormValidate()))
                {
                    SiteDALModel siteDalModel = new SiteDALModel();
                    siteDalModel.SiteModel = FillSiteModel();
                    siteDalModel.MstFinYearModel = FillFinYearModel();

                    bool Status = false;
                    if (flagAddEdit == true)
                    {

                        Status = this.siteManager.UpdateSite(siteDalModel.SiteModel, siteDalModel.MstFinYearModel);
                        if (Status == true)
                            CommonFunc.ShowMessage("Site " + txtSiteCode.Text.Trim() + " Updated Successfully", MessageType.Information);
                        else
                            CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                    }
                    else
                    {

                        
                        siteDalModel.MstAreaCodeModel = FillAreaCodeModel();
                        siteDalModel.MstCurrencySiteModel = FillCurrencyModel();
                        siteDalModel.MstSiteCurrancyMapModel = FillSiteCurrancyMapModel();
                        Status = this.siteManager.SaveSite(siteDalModel);

                        if (Status == true)
                            CommonFunc.ShowMessage("Site " + txtSiteCode.Text.Trim() + " Added Successfully", MessageType.Information);
                        else
                            CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                    }

                    clearForm();
                }
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSiteCode.ReadOnly = false;
                finYearBeginDate.ReadOnly = false;
                finYearEndDate.ReadOnly = false;
                txtCountry.ReadOnly = false;
                txtCurrencyCode.ReadOnly = false;
                txtCurrencySymol.ReadOnly = false;
                clearForm();
                var siteList = this.siteManager.GetSiteEditList().ToList();
                frmCommonSearchTrueGrid objSearch = new frmCommonSearchTrueGrid();
                objSearch.Text = "Site Search";
                DataTable dtSites = CommonFunc.ConvertListToDataTable(siteList);

                //dtUser.Columns[0].ColumnName = "s";
                dtSites.Columns[0].ColumnName = "Site Code";
                dtSites.Columns[1].ColumnName = "Site Name";
                dtSites.Columns[2].ColumnName = "Contact Person";
                dtSites.Columns[3].ColumnName = "Email Address";

                objSearch.dtcommonSearch = dtSites;
                DataTable dtSelectedSite = new DataTable();
                if (objSearch.ShowDialog() == DialogResult.OK)
                    dtSelectedSite = objSearch.dtSelectedList;
                if (dtSelectedSite.Rows.Count > 0)
                {

                    var siteDetail = this.siteManager.GetSiteByID(dtSelectedSite.Rows[0]["Site Code"].ToString());
                    var finYearDetails = this.siteManager.GetFinYearDetailsBySiteID(dtSelectedSite.Rows[0]["Site Code"].ToString());
                    var siteCurrencyDetails = this.siteManager.GetCurrencySymbol(siteDetail.LocalCurrancyCode);

                    txtSiteCode.Text = siteDetail.SiteCode;
                    txtSiteCode.ReadOnly = true;
                    txtOfficialName.Text = (!string.IsNullOrEmpty(siteDetail.SiteShortName) ? siteDetail.SiteShortName : "");
                    txtCorporateName.Text = (!string.IsNullOrEmpty(siteDetail.SiteOfficialName) ? siteDetail.SiteOfficialName : "");
                    txtAddress1.Text = (!string.IsNullOrEmpty(siteDetail.SiteAddressLn1) ? siteDetail.SiteAddressLn1 : "");
                    txtAddress2.Text = (!string.IsNullOrEmpty(siteDetail.SiteAddressLn2) ? siteDetail.SiteAddressLn2 : "");
                    txtAddress3.Text = (!string.IsNullOrEmpty(siteDetail.SiteAddressLn3) ? siteDetail.SiteAddressLn3 : "");
                    txtEmail.Text = (!string.IsNullOrEmpty(siteDetail.EmailId) ? siteDetail.EmailId : "");
                    txtContactPerson.Text = (!string.IsNullOrEmpty(siteDetail.ContactPerson) ? siteDetail.ContactPerson : "");
                    txtContactNumber.Text = (!string.IsNullOrEmpty(siteDetail.SiteTelephone1) ? siteDetail.SiteTelephone1 : "");
                    if (siteDetail.FaxNo != "")
                        txtFaxNo.Text = siteDetail.FaxNo;
                    txtTaxNo.Text = (!string.IsNullOrEmpty(siteDetail.LocalSalesTaxNo) ? siteDetail.LocalSalesTaxNo : "");
                    txtLicenceNo.Text = (!string.IsNullOrEmpty(siteDetail.CentralSalesTaxNo) ? siteDetail.CentralSalesTaxNo : "");
                    // fillCountries();
                    txtCountry.Text = siteDetail.CountryCode;
                    txtCurrencyCode.Text = siteDetail.LocalCurrancyCode;
                    txtCurrencySymol.Text = siteCurrencyDetails.CurrencySymbol;
                    txtCountry.ReadOnly = true;
                    txtCurrencyCode.ReadOnly = true;
                    txtCurrencySymol.ReadOnly = true;
                    if (finYearDetails != null)
                    {
                        finYearBeginDate.SelectedText = (!string.IsNullOrEmpty(finYearDetails.ValidFromDt.ToString()) ? finYearDetails.ValidFromDt.ToString() : "");
                        finYearEndDate.SelectedText = (!string.IsNullOrEmpty(finYearDetails.ValidToDt.ToString()) ? finYearDetails.ValidToDt.ToString() : "");
                        finYearBeginDate.ReadOnly = true;
                        finYearEndDate.ReadOnly = true;
                    }
                    lblInstallationDate.Visible = true;
                    dtpInstallationDate.Visible = true;
                    dtpInstallationDate.ReadOnly = false;
                    dtpInstallationDate.SelectedText = (!string.IsNullOrEmpty(siteDetail.CreatedOn.ToString()) ? siteDetail.CreatedOn.ToString() : "");
                    dtpInstallationDate.ReadOnly = true;
                    flagAddEdit = true;
                }
                objSearch.Dispose();

            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (CommonFunc.ShowMessage("All the data entered will be lost. Do you wish to proceed?", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
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
                if (CommonFunc.ShowMessage("All the data entered by you shall be cleared. Do You wish to proceed?", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
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

        private void frmSiteDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtSiteCode.Focus();  //vipin
        }

        
        //private void btnClose_Click(object sender, System.EventArgs e)
        //{
        //    this.Dispose();
        //    this.Close();
        //}

        #endregion

      

        #endregion

    }
}
