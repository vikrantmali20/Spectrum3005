using System;
using System.ComponentModel;
using System.Windows.Forms;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL;
using Spectrum.Models;
using System.Collections.Generic;
using System.Linq;
using Spectrum.Models.Enums;
using Spectrum.Logging;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using Microsoft;
using System.Net;
namespace Spectrum.BO
{
    public partial class frmSupplier : Spectrum.Controls.RibbonForm
    {
        public frmSupplier()
        {
            InitializeComponent();
            this.supplierManager = new SupplierManager();
            this.commonManager = new CommonManager();
        }

        #region "Class Variable"

        ISupplierManager supplierManager;
        ICommonManager commonManager;
        SupplierModel supplierModel;

        IQueryable<AreaCodeModel> areaCodeList;
        IQueryable<MasterTypeModel> masterTypeList;
        private string editSupplierCode = string.Empty;

        #endregion

        #region "Events"
      
        private void frmSupplier_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                } 
                this.supplierActionButtons.btnSave.Click += new System.EventHandler(btnSave_Click);
                this.supplierActionButtons.btnCancel.Click += new System.EventHandler(btnCancel_Click);
                this.supplierActionButtons.btnClear.Click += new EventHandler(btnClear_Click);
                this.btnSearch.Click += new EventHandler(btnSearch_Click);
                txtSupplierCode.KeyPress += new KeyPressEventHandler(txtValidateSpecialCharSpace);

               
                this.setInitialFormsValidations();

                this.areaCodeList = commonManager.GetAreaCodeList();

                var countryList = (from p in areaCodeList
                                   where p.AreaType == (int)MaterType.Country
                                   select new DropDownModel
                                   {
                                       Code = p.AreaCode,
                                       Description = p.Description
                                   }).ToList();

                masterTypeList = commonManager.GetMasterTypeList();
                var paymentModesList = (from m in masterTypeList
                                        where m.CodeType == MaterType.VendorPaymentTerms.ToString()
                                        select new DropDownModel { Code = m.Code, Description = m.ShortDesc }).ToList();

                var shipmentModesList = (from m in masterTypeList
                                         where m.CodeType == MaterType.VendorShipmentType.ToString()
                                         select new DropDownModel { Code = m.Code, Description = m.ShortDesc }).ToList();

                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboCountry, countryList);
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboPaymentMethods, paymentModesList);
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboShipmentMethod, shipmentModesList);
                clearForm();
                CommonFunc.SetCultureFromResource(this);
                //code added by roshan for issue id 2835
                label3.Text = "Name";
                lblVatTinNo.Text = "GSTNo.";
                lblVatTinDate.Visible = false;
                lblCstTinNo.Visible=false;
                lblCstTinDate.Visible = false;
                lblTinNo.Visible = false;
                lblServiceTaxNo.Visible=false;
                dtpVatTinDate.Visible=false;
                dtpCstTinDate.Visible = false;
                txtCstTinNo.Visible = false;
                txtTinNo.Visible = false;
                txtServiceTaxNo.Visible = false;
            }
            catch (System.Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                //MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
     
        #region " Buttons Events "

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupplierCode.Text) || !string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtContactPerson.Text))
                {
                    if (CommonFunc.ShowMessage(" All the data entered will be lost. Do you wish to proceed?", Models.Enums.MessageType.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                clearForm();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                //MessageBox.Show(ex.Message);
            }

        }

          /// <summary>
        /// Form will be set for Edit,Save,Update transaction .in Edit mode it will enable all controls and allow to update . in case form is in new mode it will allow for save[add]...
        /// </summary>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                    if (IsFormvalidate())
                    {
                        bool updateNextNo = false;
                        this.supplierModel = new SupplierModel();
                        FillSupplierDataToModel();
                        if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()))
                        {
                            supplierModel.SupplierCode = GetSupplierCode();
                            updateNextNo = true;
                            
                        }
                        string autoSupplierCode = txtSupplierCode.Text;

                        if ((string.IsNullOrEmpty(editSupplierCode)) ? this.supplierManager.SaveSupplier(this.supplierModel, ref autoSupplierCode) : this.supplierManager.UpdateSupplier(this.supplierModel))
                        {
                           CommonFunc.ShowMessage("Supplier code " + autoSupplierCode + " Supplier details added successfully.", Models.Enums.MessageType.Information);
                            if(updateNextNo==true)
                                this.commonManager.UpdateNextID(CommonModel.SiteCode, "SU"); 
                           clearForm();
                        }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  Form will delete  Supplier , after delete form will be cleared and make form open for New Mode .
        /// </summary>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                //If no item is selected, then on click system displays “Please select at least one item.”
                if (string.IsNullOrEmpty(editSupplierCode))
                {
                    MessageBox.Show("Please select at least one item.");
                    return;
                }

                //On click of delete the system prompts “ Are you sure you wish to delete the selected items”

                if (MessageBox.Show("Are you sure you wish to delete the selected items", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (supplierManager.DeleteByID(this.supplierModel.SupplierCode))
                    {
                        MessageBox.Show("Delete successfully");
                        clearForm();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtSupplierCode.Text) || !string.IsNullOrEmpty(txtName.Text) ||  !string.IsNullOrEmpty(txtContactPerson.Text))
            //{
                if (CommonFunc.ShowMessage("All the data entered by you shall be cleared. Do You wish to proceed?", Models.Enums.MessageType.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
             //}5
                //   this.Dispose();
            this.Close();
            
        }

        /// <summary>
        ///  on search click it will open search box of all supplier ...and after selecting the supplier , fecth supplier details and bind to supplier model .
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var supplierList = this.supplierManager.GetSupplierList().ToList();

                frmCommonSearchTrueGrid objSearch = new frmCommonSearchTrueGrid(multipleSelect: false);
                DataTable dtSupplier = CommonFunc.ConvertListToDataTable(supplierList);

                objSearch.Text = "Supplier Search";
                objSearch.boolWildSearch = false;
                objSearch.dtcommonSearch = dtSupplier;
                DataTable dtSelectedSupplier = new DataTable();
                
                if (objSearch.ShowDialog() == DialogResult.OK)
                {
                    dtSelectedSupplier = objSearch.dtSelectedList;
                    if (dtSelectedSupplier.Rows.Count > 0)
                    {
                        editSupplierCode = dtSelectedSupplier.Rows[0]["Code"].ToString();
                        this.supplierModel = this.supplierManager.GetSupplierByID(editSupplierCode);
                            
                    FillModelDataToSupplier();
                    //For Edit Supplier, all the fields except supplier code are editable.
                    txtSupplierCode.ReadOnly = true;
                    //code added by irfan on 14-7-2017 issue id 0002254
                    supplierErrorProvider.SetError(txtContactPerson, string.Empty);
                    supplierErrorProvider.SetError(txtName, string.Empty);

                    txtContactPerson.BorderColor = CommonFunc.DefaultBorderColor;
                    txtName.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                    
                }
                objSearch.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        #endregion

        #region "Form Events "
        /// <summary>
        ///  on country change bind state list of that country ..
        /// </summary>
        private void cboCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboState.SelectedIndex = -1;
                if (cboCountry.SelectedValue != null)
                {
                    var stateList = (from p in areaCodeList
                                     where p.AreaType == (int)MaterType.State && p.ParentCode == cboCountry.SelectedValue.ToString()
                                     select new DropDownModel
                                     {
                                         Code = p.AreaCode,
                                         Description = p.Description
                                     }).ToList();

                    CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboState, stateList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  on state change bind city list of that state ..
        /// </summary>
        private void cboState_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboCity.SelectedIndex = -1;
                if (cboState.SelectedValue != null)
                {
                    var cityList = (from p in areaCodeList
                                    where p.AreaType == (int)MaterType.City && p.ParentCode == cboState.SelectedValue.ToString()
                                    select new DropDownModel
                                    {
                                        Code = p.AreaCode,
                                        Description = p.Description
                                    }).ToList();

                    CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboCity, cityList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cboPaymentMethods_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboPaymentMethods.SelectedIndex > -1)
            {
                cboPaymentMethods.SelectedIndex = -1;
            }
        }

        private void cboShipmentMethod_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboShipmentMethod.SelectedIndex > -1)
            {
                cboShipmentMethod.SelectedIndex = -1;
            }
        }

      

        #endregion

        #endregion

        #region " Functions "


        private string GetSupplierCode()
        {
            string sitecode = CommonModel.SiteCode;
            int nextNo = commonManager.GetNextID(sitecode, "SU");
            nextNo = nextNo + 1;
            int padLimit = 10;
            if (nextNo.ToString().Length > 1)
                padLimit = padLimit + (nextNo.ToString().Length - 1);
            string strlastcode = string.Format("{0}", nextNo.ToString().PadLeft(padLimit - Convert.ToString(nextNo).Length, '0'));
            //string strlastcode = string.Format("0000{0}", nextNo.ToString().PadLeft(8 - Convert.ToString(nextNo).Length, '0'));
            string strtaxcode = "SUS" +  sitecode.Substring(sitecode.Length - 3) + strlastcode;
            return strtaxcode;
        }

        //
        ////Changes Made By irfan on 6/7/2017  issue id 2252
        // HANDLING speccial char 
        private void txtValidateSpecialCharSpace(object sender, KeyPressEventArgs e)
        {
            //if (!CommonFunc.validateNumberAlphabet(e.KeyChar.ToString()))
            //{
            //    e.Handled = true;
            //    CommonFunc.ShowMessage(" Only Characters and numbers allowed ", MessageType.Information);
            //}

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtLandLineNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        ////Changes Made By irfan on 6/7/2017  dd/mm/yyyy

        //



        // HANDLING speccial char 
        //code commented by irfan on 06-07-2017
        //private void txtValidateSpecialCharSpace(object sender, KeyPressEventArgs e)
        //{
        //    if (!CommonFunc.validateNumberAlphabet(e.KeyChar.ToString()))
        //    {
        //        e.Handled = true;
        //        CommonFunc.ShowMessage(" Only Characters and numbers allowed ", MessageType.Information);
        //    }

        //}

        /// <summary>
        /// apply Form level default validation accroding to SRS
        /// </summary>
        private void setInitialFormsValidations()
        {
            try
            {
                //Supplier Code* Alphanumeric text box. At least 1 char and max 15 characters. 
                txtSupplierCode.MaxLength = 15;
                //Name*	Alphanumeric text box. Mandatory 200 characters max
                txtName.MaxLength = 200;
                //Contact Person*	Alphanumeric text box 200 characters max
                txtContactPerson.MaxLength = 200;
                // E-mail Address	Alphanumeric Text box. Not Mandatory 200 characters max
                txtEmailAddress.MaxLength = 200;
                // Minimum margin	Numeric text box 18 characters max
                txtMinimumMargin.MaxLength = 9;
                txtMinimumMargin.DataType = typeof(Int64);
                //Is Active	Checkbox. Checked by default.If this is unchecked the status of the supplier will be inactive in the system.
                chkActive.Checked = true;
                //Payment methods	Pre-populated drop down •	Cash •	Cheque •	Net Transfer
                // Shipment Method	Pre-populated drop down •	By Road •	By Ship •	By Air
                //Delivery Days	Numeric text box 15 characters max
                txtDeliveryDays.MaxLength = 9;
                txtDeliveryDays.DataType = typeof(Int64);
                // Payment Days	Numeric text box. 15 characters max
                txtPaymentDays.MaxLength = 9;
                txtPaymentDays.DataType = typeof(Int64);
                //Address 1	Alphanumeric text box 45 characters max
                txtAddress1.MaxLength = 45;
                //Address 2	Alphanumeric text box 45 characters max
                txtAddress2.MaxLength = 45;
                //Address 3	Alphanumeric text box 45 characters max
                txtAddress3.MaxLength = 45;
                //Address 4	Alphanumeric text box 45 characters max
                txtAddress4.MaxLength = 45;
                //Zip/Pin code	Numeric text box. 45 characters max
                txtZipCode.MaxLength = 19;
                txtZipCode.DataType = typeof(Int64);
                //  Country	Drop Down. Pre-populated from the system.

                // Mobile  number	Numeric Text box 45 characters max 
                txtMobileNo.MaxLength = 10;
                txtMobileNo.DataType = typeof(Int64);
                // txtLandLineNo   Numeric Text box 45 characters max 
                txtLandLineNo.MaxLength = 13;
                txtLandLineNo.DataType = typeof(Int64);
                // Fax No	Numeric Text box 45 characters max
                txtFaxNo.MaxLength = 15;
                txtFaxNo.DataType = typeof(Int64);
                //VAT TIN Number	Alphanumeric Text box 45 characters max
                txtVatTanNo.MaxLength = 20;
                //CST TIN Number	Alphanumeric Text box 45 characters max
                txtCstTinNo.MaxLength = 20;
                //TIN Number	Alphanumeric Text box 45 characters max
                txtTinNo.MaxLength = 11;
                //PAN Number	Alphanumeric Text box 45 characters max
                txtPanNo.MaxLength = 20;
                //Service Tax Number Alphanumeric Text box 45 characters max
                txtServiceTaxNo.MaxLength = 45;
               

                dtpVatTinDate.Clear();
                dtpCstTinDate.Clear();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  fill supplier model from forms controls for Save/Update
        /// </summary>
        private void FillSupplierDataToModel()
        {
            try
            {
                this.supplierModel.SupplierCode = string.IsNullOrEmpty(txtSupplierCode.Text) ? "" : txtSupplierCode.Text.Trim();
                this.supplierModel.IsAutoNumber = string.IsNullOrEmpty(txtSupplierCode.Text) ? true :false;
                this.supplierModel.SupplierName = txtName.Text.Trim();
                this.supplierModel.address1 = txtAddress1.Text.Trim();
                this.supplierModel.address2 = txtAddress2.Text.Trim();
                this.supplierModel.address3 = txtAddress3.Text.Trim();
                this.supplierModel.address4 = txtAddress4.Text.Trim();
                this.supplierModel.CountryCode = (cboCountry.SelectedValue != null) ? cboCountry.SelectedValue.ToString() : "";
                this.supplierModel.StateCode = (cboState.SelectedValue != null) ? cboState.SelectedValue.ToString() : "";
                this.supplierModel.CityCode = (cboCity.SelectedValue != null) ? cboCity.SelectedValue.ToString() : "";
                this.supplierModel.TelephoneNo = txtMobileNo.Text.Trim();
                this.supplierModel.FaxNo = txtFaxNo.Text.Trim();
                this.supplierModel.TelexNo = txtLandLineNo.Text.Trim(); 
                //this.supplierModel.Pincode = null;
                this.supplierModel.Pincode = txtZipCode.Text.Trim();
                this.supplierModel.LocalSalesTaxNo = txtVatTanNo.Text.Trim();
                this.supplierModel.CentralSalesTaxNo = txtCstTinNo.Text.Trim();
                this.supplierModel.Status = true;

                //DateTime.TryParse(dtpVatTinDate.Value.ToString(), out this.supplierModel.LocalSalesTaxDate);
                //this.supplierModel.LocalSalesTaxDate =(DateTime)??null;
                if (!dtpVatTinDate.ValueIsDbNull)
                    this.supplierModel.LocalSalesTaxDate = (DateTime)dtpVatTinDate.Value;
                if (!dtpCstTinDate.ValueIsDbNull)
                    this.supplierModel.CentralSalesTaxDate = (DateTime)dtpCstTinDate.Value;

                this.supplierModel.MinMargin = decimal.Parse(string.IsNullOrEmpty(txtMinimumMargin.Text.Trim()) ? "0" : txtMinimumMargin.Text.Trim());

                this.supplierModel.PaymentMethod = (cboPaymentMethods.SelectedValue != null) ? cboPaymentMethods.SelectedValue.ToString() : "";
                this.supplierModel.ShipmentMethod = (cboShipmentMethod.SelectedValue != null) ? cboShipmentMethod.SelectedValue.ToString() : "";
                this.supplierModel.EmailId = txtEmailAddress.Text.Trim();
                this.supplierModel.ContactPerson = txtContactPerson.Text.Trim();
                this.supplierModel.CurrencyCode = null;
                this.supplierModel.LanguageCode = null;

                this.supplierModel.DeliveryDays = int.Parse(string.IsNullOrEmpty(txtDeliveryDays.Text.Trim()) ? "0" : txtDeliveryDays.Text.Trim());
                this.supplierModel.PaymentDays = int.Parse(string.IsNullOrEmpty(txtPaymentDays.Text.Trim()) ? "0" : txtPaymentDays.Text.Trim());

                this.supplierModel.TINNo = txtTinNo.Text.Trim();
                this.supplierModel.PANNo = txtPanNo.Text.Trim();
                this.supplierModel.ServiceTaxRegNo = txtServiceTaxNo.Text.Trim();
                this.supplierModel.VendorPostingGroup = null;
                this.supplierModel.GenBusPostingGroup = null;
                this.supplierModel.isActive = chkActive.Checked;

                this.supplierModel.CreatedOn = CommonModel.CurrentDate;
                this.supplierModel.CreatedBy = CommonModel.UserID;
                this.supplierModel.CreatedAt = CommonModel.SiteCode;
                this.supplierModel.UpdatedOn = CommonModel.CurrentDate;
                this.supplierModel.UpdatedBy = CommonModel.UserID;
                this.supplierModel.UpdatedAt = CommonModel.UserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        ///  fill forms  controls from supplier model for edit supplier existing recodes
        /// </summary>
        private void FillModelDataToSupplier()
        {
            try
            {
                txtSupplierCode.Value = this.supplierModel.SupplierCode;
                txtName.Value = this.supplierModel.SupplierName;
                txtAddress1.Value = this.supplierModel.address1;
                txtAddress2.Value = this.supplierModel.address2;
                txtAddress3.Value = this.supplierModel.address3;
                txtAddress4.Value = this.supplierModel.address4;
                cboCountry.SelectedValue = this.supplierModel.CountryCode;
                cboState.SelectedValue = this.supplierModel.StateCode;
                cboCity.SelectedValue = this.supplierModel.CityCode;
                txtMobileNo.Value = this.supplierModel.TelephoneNo;
                txtLandLineNo.Value = this.supplierModel.TelexNo;
                txtFaxNo.Value = this.supplierModel.FaxNo;
                //this.supplierModel.TelexNo 
                txtZipCode.Value = this.supplierModel.Pincode;
                txtVatTanNo.Value = this.supplierModel.LocalSalesTaxNo;
                txtCstTinNo.Value = this.supplierModel.CentralSalesTaxNo;

                dtpVatTinDate.Value = this.supplierModel.LocalSalesTaxDate;
                dtpCstTinDate.Value = this.supplierModel.CentralSalesTaxDate;

                txtMinimumMargin.Value = this.supplierModel.MinMargin;

                cboPaymentMethods.SelectedValue = this.supplierModel.PaymentMethod;
                cboShipmentMethod.SelectedValue = this.supplierModel.ShipmentMethod;
                txtEmailAddress.Value = this.supplierModel.EmailId;
                txtContactPerson.Value = this.supplierModel.ContactPerson;
                txtDeliveryDays.Value = this.supplierModel.DeliveryDays;
                txtPaymentDays.Value = this.supplierModel.PaymentDays;

                txtTinNo.Value = this.supplierModel.TINNo;
                txtPanNo.Value = this.supplierModel.PANNo;
                txtServiceTaxNo.Value = this.supplierModel.ServiceTaxRegNo;
                //this.supplierModel.VendorPostingGroup = null;
                //this.supplierModel.GenBusPostingGroup = null;
                chkActive.Checked = (bool)this.supplierModel.isActive;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// clears the value of all forms controls 
        /// </summary>
        private void clearForm()
        {
            try
            {
                this.txtPanNo.Value = String.Empty;
                this.txtServiceTaxNo.Value = String.Empty;
                this.txtTinNo.Value = String.Empty;
                this.txtCstTinNo.Value = String.Empty;
                this.txtVatTanNo.Value = String.Empty;
                this.txtFaxNo.Value = String.Empty;
                this.txtMobileNo.Value = String.Empty;
                this.txtLandLineNo.Value = String.Empty;
                this.txtZipCode.Value = String.Empty;
                this.txtAddress4.Value = String.Empty;
                this.txtAddress3.Value = String.Empty;
                this.txtAddress2.Value = String.Empty;
                this.txtAddress1.Value = String.Empty;
                this.txtSupplierCode.Value = String.Empty;
                this.txtPaymentDays.Value = String.Empty;
                this.txtMinimumMargin.Value = String.Empty;
                this.txtContactPerson.Value = String.Empty;
                this.txtEmailAddress.Value = String.Empty;
                this.txtName.Value = String.Empty;
                supplierErrorProvider.Clear();
                txtName.BorderColor = CommonFunc.DefaultBorderColor;
                txtContactPerson.BorderColor = CommonFunc.DefaultBorderColor;

                this.txtDeliveryDays.Value = String.Empty;

              //  this.dtpCstTinDate.Clear();
              //  this.dtpVatTinDate.Clear();
                this.cboCountry.SelectedIndex = -1;
                this.cboState.SelectedIndex = -1;
                this.cboCity.SelectedIndex = -1;
                this.cboCity.DataSource = null ;
                this.cboState.DataSource = null;
                this.cboPaymentMethods.SelectedIndex = -1;
                this.cboShipmentMethod.SelectedIndex = -1;

                editSupplierCode = string.Empty;
                //  txtSupplierCode.ReadOnly = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///   Check all form controls have valid data if not display user to message and return fail 
        /// </summary>
        /// <returns> form validation sucuss or Fail </returns>
        private bool IsFormvalidate()
        {
            try
            {
                bool validateResult = true;
                bool isControlFocused = true;
                Regex alphanumericpatt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");
                //--- Supplier code already exists --//--
                if (!string.IsNullOrEmpty(txtSupplierCode.Text) && string.IsNullOrEmpty(editSupplierCode))
                {
                    var IssupplierModel = supplierManager.GetSupplierByID(txtSupplierCode.Text);
                    if (IssupplierModel != null)
                    {
                        CommonFunc.SetErrorProvidertoControl(ref supplierErrorProvider, ref txtSupplierCode, "Supplier code already exists", true);
                        txtSupplierCode.Focus();
                        validateResult = false;
                        isControlFocused = false;
                    }
                    else
                    {
                        supplierErrorProvider.SetError(txtSupplierCode, string.Empty);
                        txtSupplierCode.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }


                if (!CommonFunc.SetErrorProvidertoControl(ref supplierErrorProvider, ref txtName, "Supplier Name value required")  )
                {
                    if (isControlFocused) 
                        this.txtName.Focus();
                    
                    validateResult = false;
                    isControlFocused = false;
                }
                if (!CommonFunc.SetErrorProvidertoControl(ref supplierErrorProvider, ref txtContactPerson, "Contact Person value required"))
                {
                    if (isControlFocused)
                    this.txtContactPerson.Focus();
                    validateResult = false;
                    isControlFocused = false;
                }

                if (!CommonFunc.validateEmailId(this.txtEmailAddress.Text.ToString()) && !string.IsNullOrEmpty(this.txtEmailAddress.Text.ToString()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref supplierErrorProvider, ref txtEmailAddress, "E-mail should in standard E-mail format.", true))
                    {
                        if (isControlFocused)
                        txtEmailAddress.Focus();
                        validateResult = false;
                        isControlFocused = false;
                    }
                }
                else
                {
                    supplierErrorProvider.SetError(txtEmailAddress, string.Empty);
                    txtEmailAddress.BorderColor = CommonFunc.DefaultBorderColor ;

                }
                if (!string.IsNullOrEmpty(txtVatTanNo.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtVatTanNo.Text) == false)
                    {
                        validateResult = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref supplierErrorProvider, ref txtVatTanNo, "Vat Tin No allows Alphanumeric Character", false))
                        {
                            this.txtVatTanNo.Focus();
                        }

                    }
                    else
                    {
                        supplierErrorProvider.SetError(txtVatTanNo, string.Empty);
                        txtVatTanNo.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (!string.IsNullOrEmpty(txtCstTinNo.Text.Trim()))
                {
                    if (alphanumericpatt.IsMatch(txtCstTinNo.Text) == false)
                    {
                        validateResult = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref supplierErrorProvider, ref txtCstTinNo, "CST Tin No allows Alphanumeric Character", false))
                        {
                            this.txtCstTinNo.Focus();
                        }

                    }
                    else
                    {
                        supplierErrorProvider.SetError(txtCstTinNo, string.Empty);
                        txtCstTinNo.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
               return validateResult;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void ThemeChange()
        {

            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            sizerSupplierDetails.BackColor = Color.FromArgb(134, 134, 134);

            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            supplierActionButtons.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            supplierActionButtons.btnSave.BackColor = Color.Transparent;
            supplierActionButtons.btnSave.BackColor = Color.FromArgb(0, 107, 163);
            supplierActionButtons.btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            supplierActionButtons.btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            supplierActionButtons.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            supplierActionButtons.btnSave.FlatAppearance.BorderSize = 0;
            supplierActionButtons.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            supplierActionButtons.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            supplierActionButtons.btnClear.BackColor = Color.Transparent;
            supplierActionButtons.btnClear.BackColor = Color.FromArgb(0, 107, 163);
            supplierActionButtons.btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            supplierActionButtons.btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            supplierActionButtons.btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            supplierActionButtons.btnClear.FlatAppearance.BorderSize = 0;
            supplierActionButtons.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            supplierActionButtons.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            supplierActionButtons.btnCancel.BackColor = Color.Transparent;
            supplierActionButtons.btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            supplierActionButtons.btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            supplierActionButtons.btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            supplierActionButtons.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            supplierActionButtons.btnCancel.FlatAppearance.BorderSize = 0;
            supplierActionButtons.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            lblActive.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress1.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress2.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress3.BackColor = Color.FromArgb(212, 212, 212);
            lblAddress4.BackColor = Color.FromArgb(212, 212, 212);
            lblCity.BackColor = Color.FromArgb(212, 212, 212);
            lblContactPerson.BackColor = Color.FromArgb(212, 212, 212);
            lblCountry.BackColor = Color.FromArgb(212, 212, 212);
            lblCstTinDate.BackColor = Color.FromArgb(212, 212, 212);
            lblCstTinNo.BackColor = Color.FromArgb(212, 212, 212);
            lblDeliveryDays.BackColor = Color.FromArgb(212, 212, 212);
            lblEmailAddress.BackColor = Color.FromArgb(212, 212, 212);
            lblFaxNo.BackColor = Color.FromArgb(212, 212, 212);
            lblLandlineNo.BackColor = Color.FromArgb(212, 212, 212);
            lblMinimumMargin.BackColor = Color.FromArgb(212, 212, 212);
            lblMobileNo.BackColor = Color.FromArgb(212, 212, 212);
            lblPanNo.BackColor = Color.FromArgb(212, 212, 212);
            lblPaymentsDays.BackColor = Color.FromArgb(212, 212, 212);
            lblPaymentsMode.BackColor = Color.FromArgb(212, 212, 212);
            lblServiceTaxNo.BackColor = Color.FromArgb(212, 212, 212);
            lblShipmentMethods.BackColor = Color.FromArgb(212, 212, 212);
            lblState.BackColor = Color.FromArgb(212, 212, 212);
            lblSupplierCode.BackColor = Color.FromArgb(212, 212, 212);
            lblTinNo.BackColor = Color.FromArgb(212, 212, 212);
            lblVatTinDate.BackColor = Color.FromArgb(212, 212, 212);
            lblVatTinNo.BackColor = Color.FromArgb(212, 212, 212);
            lblZipCode.BackColor = Color.FromArgb(212, 212, 212);
            label1.BackColor = Color.FromArgb(212, 212, 212);
            label2.BackColor = Color.FromArgb(212, 212, 212);
           
            chkActive.BackColor = Color.FromArgb(212, 212, 212);
            //code added by roshan for issue id 2835
            label3.BackColor = Color.FromArgb(212, 212, 212);

        }
        #endregion

        private void frmSupplier_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtContactPerson.Focus();  //vipin
        }

        private void btnSearch_Click_1(object sender, EventArgs e)  // added by vipin on 04-04-2017
        {

        }

        private void supplierActionButtons_Load(object sender, EventArgs e)
        {

        }

           }
}
