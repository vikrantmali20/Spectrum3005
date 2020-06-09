using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmManualPromotion : Spectrum.Controls.RibbonForm
    {
        public frmManualPromotion()
        {
            InitializeComponent();
            this.promotionManager = new PromotionManager();
            this.commonManager = new CommonManager();
        }

        #region "Class Variable"

        IPromotionManager promotionManager;
        ICommonManager commonManager;
        bool flagAddEdit = false;
        bool discPer = false;
        bool fixedPriceOff = false;
        bool fixedSelling = false;
        decimal promoValue = 0;

        #endregion

        #region "Events"

        #region " Other Events "

        private void frmManualPromotion_Load(object sender, EventArgs e)
        {
            //code added by vipul for issue id 0002831
            txtPromotionId.ReadOnly = true;
            if (CommonFunc.Themeselect == "Theme 1")
            {
                ThemeChange();
            }
            setInitialFormsValidations();
            //CommonFunc.WriteResourceFile(this);
            CommonFunc.SetCultureFromResource(this);
        }

        #endregion

        #region " Buttons Events "

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((IsFormValidate()))
                {
                    PromotionModel promotionModel = new PromotionModel();

                    if (rdbFlatPrice.Checked == true)
                        fixedSelling = true;
                    else
                        fixedSelling = false;

                    if (rdoAmountOff.Checked == true)
                        fixedPriceOff = true;
                    else
                        fixedPriceOff = false;

                    if (rdoDiscountPer.Checked == true)
                        discPer = true;
                    else
                        discPer = false;


                    if (!string.IsNullOrEmpty(txtPromotionValue.Text.Trim()))
                        promoValue = Convert.ToDecimal(txtPromotionValue.Text.Trim());

                    
                    promotionModel.ManualPromotionModel = FillManualPromotionModel();
                    string prCode = "";
                    if (string.IsNullOrEmpty(txtPromotionId.Text.Trim()))
                    {
                        prCode = GetPRCode();
                        promotionModel.ManualPromotionModel.PromotionId = prCode;
                    }
                    else
                        prCode = txtPromotionId.Text.Trim();
                    bool Status = false;
                    if (flagAddEdit == true)
                    {
                        Status = this.promotionManager.UpdateManualPromotions(promotionModel);
                        if (Status == true)
                            CommonFunc.ShowMessage("Promotion Updated Successfully.", MessageType.Information);
                    }
                    else
                    {
                        promotionModel.PromotionSiteMapModel = FillPromotionSiteMapModel();
                        promotionModel.PromotionSiteMapModel.OfferNo  = prCode;
                        Status = this.promotionManager.AddManualPromotions(promotionModel);
                        if (Status == true)
                        {
                            if (string.IsNullOrEmpty(txtPromotionId.Text.Trim()))
                            {
                                this.commonManager.UpdateNextID(CommonModel.SiteCode, "PR");
                            }
                            CommonFunc.ShowMessage("Promotion " + prCode + " Added Successfully.", MessageType.Information);
                        }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (CommonFunc.ShowMessage("All the data entered by you shall be cleared. Do You wish to proceed?", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
            {
                clearForm();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPromotionId.ReadOnly = false;
                //AuthUserModelEdit userSelectedList = null;
                var promoList = this.promotionManager.GetManualPromotions();
                if (promoList != null && promoList.Count > 0)
                {
                    //frmCommonSearch objSearch = new frmCommonSearch();
                    //objSearch.DataList = userList;
                    frmCommonSearchTrueGrid objSearch = new frmCommonSearchTrueGrid();
                    objSearch.Text = "Promotion Search";
                    DataTable dtPromotion = CommonFunc.ConvertListToDataTable(promoList);

                    // dtPromotion.Columns[0].ColumnName = "All";
                    dtPromotion.Columns[0].ColumnName = "Promotion Id";
                    dtPromotion.Columns[1].ColumnName = "Promotion Name";
                    dtPromotion.Columns[2].ColumnName = "Promotion Value";
                    dtPromotion.Columns[0].Caption = "PromotionId";
                    dtPromotion.Columns[1].Caption = "PromotionName";
                    dtPromotion.Columns[2].Caption = "PromotionValue";

                    objSearch.dtcommonSearch = dtPromotion;
                    DataTable dtSelectedPromotion = new DataTable();
                    if (objSearch.ShowDialog() == DialogResult.OK)
                        dtSelectedPromotion = objSearch.dtSelectedList;
                    //userSelectedList = (objSearch.neTbl).Cast<AuthUserModelEdit>().FirstOrDefault();

                    if (dtSelectedPromotion.Rows.Count > 0)
                    {
                        var promotionDetail = this.promotionManager.GetPromotionById(dtSelectedPromotion.Rows[0]["Promotion Id"].ToString());

                        txtPromotionId.Text = promotionDetail.PromotionId;
                        txtPromotionId.ReadOnly = true;
                        txtPromotionName.Text = promotionDetail.PromotionName;
                        txtPromotionValue.Text = promotionDetail.PromotionValue.ToString();

                        if (promotionDetail.IsApproved == true)
                            chkPromoActive.Checked = true;
                        else
                            chkPromoActive.Checked = false;
                        if (promotionDetail.FixedSelling == true)
                            rdbFlatPrice.Checked = true;
                        else if (promotionDetail.FixedPriceOff == true)
                            rdoAmountOff.Checked = true;
                        else if (promotionDetail.DiscPer == true)
                            rdoDiscountPer.Checked = true;


                        flagAddEdit = true;
                    }
                    else
                    {
                        txtPromotionId.ReadOnly = true;
                    }
                    objSearch.Dispose();
                }
                else
                {
                    CommonFunc.ShowMessage("No Records to display.", MessageType.Information);
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

        private string GetPRCode()
        {
            string FiscYear =  DateTime.Now.Year.ToString() ;
            FiscYear = FiscYear.Substring(FiscYear.Length - 2);
            string sitecode = CommonModel.SiteCode;
            int nextNo = commonManager.GetNextID(sitecode, "PR");
            nextNo = nextNo + 1;
            int padLimit=8;
            if (nextNo.ToString().Length > 1)
                padLimit =padLimit+ (nextNo.ToString().Length - 1);
            string strlastcode = string.Format("{0}", nextNo.ToString().PadLeft(padLimit - Convert.ToString(nextNo).Length, '0'));
            //string strlastcode = string.Format("0000{0}", nextNo.ToString().PadLeft(8 - Convert.ToString(nextNo).Length, '0'));
            string strtaxcode = "PRS"   + sitecode.Substring(sitecode.Length - 3) + FiscYear + strlastcode;
            return strtaxcode;
        }


        private ManualPromotionModel FillManualPromotionModel()
        {
            try
            {
                return new ManualPromotionModel
                {
                    PromotionId = txtPromotionId.Text,
                    PromotionName = txtPromotionName.Text,
                    PromotionValue = promoValue,
                    DiscPer = discPer,
                    FixedPriceOff = fixedPriceOff,
                    FixedSelling = fixedSelling,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(90),
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(90),
                    IsApproved = (chkPromoActive.Checked) ? true : false,
                    OfferActive = (chkPromoActive.Checked) ? true : false,
                    CreatedAt = CommonModel.SiteCode,
                    CreatedBy = CommonModel.UserID,
                    CreatedOn = CommonModel.CurrentDate,
                    UpdatedAt = CommonModel.SiteCode,
                    UpdatedBy = CommonModel.UserID,
                    UpdatedOn = CommonModel.CurrentDate,
                    Status = true,


                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }

        }

        private PromotionSiteMapModel FillPromotionSiteMapModel()
        {
            try
            {
                return new PromotionSiteMapModel
                {
                    OfferNo = txtPromotionId.Text,
                    SiteCode = CommonModel.SiteCode,
                    CreatedAt = CommonModel.SiteCode,
                    CreatedBy = CommonModel.UserID,
                    CreatedOn = CommonModel.CurrentDate,
                    UpdatedAt = CommonModel.SiteCode,
                    UpdatedBy = CommonModel.UserID,
                    UpdatedOn = CommonModel.CurrentDate,
                    Status = true,
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = true,
                    Sunday = true,


                };
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
                txtPromotionId.MaxLength = 15;
                txtPromotionName.MaxLength = 100;
                rdoDiscountPer.Checked = true;
                txtPromotionValue.MaxLength = 18;
                chkPromoActive.Checked = true;
                txtPromotionValue.DataType = typeof(Int64);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void clearForm()
        {
            this.txtPromotionId.Value = String.Empty;
            //txtPromotionId.ReadOnly = false;
            this.txtPromotionName.Value = String.Empty;
            rdoDiscountPer.Checked = true;
            this.txtPromotionValue.Value = String.Empty;
            flagAddEdit = false;
            chkPromoActive.Checked = true;
            discPer = false;
            fixedPriceOff = false;
            fixedSelling = false;
            //remove error indicator
            errorProvider.SetError(txtPromotionId, string.Empty);
            txtPromotionId.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtPromotionName, string.Empty);
            txtPromotionName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtPromotionValue, string.Empty);
            txtPromotionValue.BorderColor = CommonFunc.DefaultBorderColor;

        }

        private bool IsFormValidate()
        {
            try
            {
                bool isValid = true;


                // Regex patt = new Regex("^[A-Za-z0-9]+$");

                Regex alphanumericpatt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");
                if (!string.IsNullOrEmpty(txtPromotionId.Text.Trim()))
                {


                    if (alphanumericpatt.IsMatch(txtPromotionId.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionId, "Promotion Id allows Alphanumeric Character", false))
                        {
                            this.txtPromotionId.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtPromotionId, string.Empty);
                        txtPromotionId.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (!string.IsNullOrEmpty(txtPromotionName.Text.Trim()))
                {


                    if (alphanumericpatt.IsMatch(txtPromotionName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionName, "Promotion Name allows Alphanumeric Character", false))
                        {
                            this.txtPromotionName.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtPromotionName, string.Empty);
                        txtPromotionName.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (string.IsNullOrEmpty(txtPromotionName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtPromotionName, "Promotion name required"))
                    {
                        this.txtPromotionName.Focus();
                        isValid = false;
                    }
                }
                else if (string.IsNullOrEmpty(txtPromotionValue.Text.Trim()))
                {
                    string typeVale = "Discount Value ";
                    if (rdbFlatPrice.Checked == true)
                        typeVale = "Flat Price ";
                    else if (rdoAmountOff.Checked == true)
                        typeVale = "Amount Off Value ";


                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtPromotionValue, typeVale + " required"))
                    {
                        this.txtPromotionValue.Focus();
                        isValid = false;
                    }
                }
                else
                {

                    if (rdoDiscountPer.Checked == true)
                    {

                        if (Convert.ToDecimal(txtPromotionValue.Value) <= Convert.ToDecimal("0"))
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionValue, "Promotion Value Should be Greater Than 0", false))
                            {
                                this.txtPromotionValue.Focus();
                                return isValid;
                            }
                        }
                        if (Convert.ToDecimal(txtPromotionValue.Value) > Convert.ToDecimal("100.00"))
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionValue, "Promotion Value Not Greater Than 100", false))
                            {
                                this.txtPromotionValue.Focus();
                                return isValid;
                            }

                        }
                        else
                        {
                            errorProvider.SetError(txtPromotionValue, string.Empty);
                            txtPromotionValue.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                    }


                    else
                    {
                        if (Convert.ToDecimal(txtPromotionValue.Value) <= Convert.ToDecimal("0"))
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionValue, "Promotion Value Should be Greater Than 0", false))
                            {
                                this.txtPromotionValue.Focus();
                                return isValid;
                            }
                        }
                        else
                        {
                            errorProvider.SetError(txtPromotionValue, string.Empty);
                            txtPromotionValue.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(txtPromotionId.Text.Trim()) && flagAddEdit == false)
                {
                    var promotionDetail = this.promotionManager.GetPromotionById(txtPromotionId.Text.Trim());
                    if (promotionDetail != null)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPromotionId, "Promotion already Exists", false))
                            this.txtPromotionId.Focus();
                    }
                    else
                    {
                        errorProvider.SetError(txtPromotionId, string.Empty);
                        txtPromotionId.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);

            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSave.BackColor = Color.Transparent;
            btnSave.BackColor = Color.FromArgb(0, 107, 163);
            btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnClear.BackColor = Color.Transparent;
            btnClear.BackColor = Color.FromArgb(0, 107, 163);
            btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            panel2.BackColor = Color.FromArgb(134, 134, 134);
            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);
            lblPromoID.BackColor = Color.FromArgb(212, 212, 212);
            lblPromotionName.BackColor = Color.FromArgb(212, 212, 212);
            lblPromotionValue.BackColor = Color.FromArgb(212, 212, 212);
            lblUserActive.BackColor = Color.FromArgb(212, 212, 212);
            rdoAmountOff.BackColor = Color.FromArgb(134, 134, 134);
            rdoDiscountPer.BackColor = Color.FromArgb(134, 134, 134);
            rdbFlatPrice.BackColor = Color.FromArgb(134, 134, 134);
            rdoDiscountPer.ForeColor = Color.White;
            rdoAmountOff.ForeColor = Color.White;
            rdbFlatPrice.ForeColor = Color.White;
        }
        #endregion

        
    }
}
