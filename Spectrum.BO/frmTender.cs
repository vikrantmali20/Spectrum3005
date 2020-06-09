using System.Windows.Forms;
using System;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL;
using System.Collections.Generic;
using Spectrum.Models;
using System.Linq;
using System.Text.RegularExpressions;
using Spectrum.Models.Enums;
using Spectrum.Logging;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmTender : Spectrum.Controls.RibbonForm
    {
        public frmTender()
        {
            InitializeComponent();
            this.tenderManager = new TenderManager();
            this.siteManager = new SiteManager();
            this.commonManager = new CommonManager();
        }

        #region "Class Variable"
        ITenderManager tenderManager;
        ISiteManager siteManager;
        ICommonManager commonManager;
        IList<DropDownModel> tenderTypes;
        IList<DropDownModel> programCodes;
        enum enumTenderGrid
        {
            Select,
            TenderCode,
            TenderName,
            Tendertype,
            ProgramCode,
            ReturnedIssue,
            RefundIssue
        }

        #endregion

        #region "Events"

        #region " Buttons Events "

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ((IsFormvalidate()))
                {
                    TenderModel tendermodel = new TenderModel();
                    tendermodel.TenderHeadCode = txtTenderCode.Text.Trim();
                    tendermodel.TenderHeadName = txtTenderName.Text.Trim();

                    if (chkRefundIssue.Checked == false && chkReturnedIssue.Checked == false)
                        tendermodel.Positive_Negative = "+";
                    else if (chkRefundIssue.Checked == true && chkReturnedIssue.Checked == true)
                        tendermodel.Positive_Negative = "*";
                    else if (chkRefundIssue.Checked == true)
                        tendermodel.Positive_Negative = "*";
                    else if (chkReturnedIssue.Checked == true)
                        tendermodel.Positive_Negative = "-";

                    tendermodel.SiteCode = CommonModel.SiteCode;
                    tendermodel.TenderType = cboTenderType.SelectedValue.ToString();
                    if (cboProgramCode.SelectedIndex != -1)
                        tendermodel.ProgramId = cboProgramCode.SelectedValue.ToString();

                    bool status = false;

                    status = this.tenderManager.SaveTender(tendermodel);
                    
                    if (status == true)
                    {
                        CommonFunc.ShowMessage(tendermodel.TenderHeadName + " Tender Saved Successfully.", MessageType.Information);
                        Fillgrid();
                    }
                    else
                        CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                    clearForm();
                }
            }
            catch (System.Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
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
                for (int rowBarCode = 1; rowBarCode < dgTender.Rows.Count; rowBarCode++)
                {
                    if (Convert.ToBoolean(dgTender.Rows[rowBarCode][(int)enumTenderGrid.Select]) == true)
                    {
                        rowDeleted = true;
                        break;
                    }
                }
                if (rowDeleted == false)
                {
                    CommonFunc.ShowMessage("Please select at least one item", Models.Enums.MessageType.Information);
                    return;
                }
                if (CommonFunc.ShowMessage("Are you sure you wish to delete the selected items", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                {
                    int ArticleCodeRowCount = dgTender.Rows.Count;
                    var deleteArticleCodeRow = new List<C1.Win.C1FlexGrid.Row>();
                    bool status = false;
                    for (int row = 1; row < ArticleCodeRowCount; row++)
                    {
                        if (Convert.ToBoolean(dgTender.Rows[row][(int)enumTenderGrid.Select]) == true)
                        {
                            status = this.tenderManager.DeleteByID(dgTender.Rows[row][(int)enumTenderGrid.TenderCode].ToString(), CommonModel.SiteCode);
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

        private void frmTender_Load(object sender, System.EventArgs e)
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


                cboSite.Enabled = false;
                cboProgramCode.Enabled = false;
                this.tenderTypes = (from result in this.commonManager.GetTenderTypeList()
                                    select new DropDownModel
                                    {
                                        Code = result.TenderType,
                                        Description = result.Description
                                    }).ToList();
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboTenderType, this.tenderTypes);
                //cboTenderType.DataSource = tenderTypes;
                //cboTenderType.DisplayMember = "Description";
                //cboTenderType.ValueMember = "Code";
                setInitialFormsValidations();
                Fillgrid();
                setTabIndex();
                // CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);

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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = chkAll.Checked;
                for (int rowArticleCode = 1; rowArticleCode < dgTender.Rows.Count; rowArticleCode++)
                {
                    dgTender.Rows[rowArticleCode][(int)enumTenderGrid.Select] = chk;
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void cboTenderType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboTenderType.SelectedValue != null)
            {
                if (cboTenderType.SelectedValue.ToString() == "CreditVouc(I)" || cboTenderType.SelectedValue.ToString() == "GiftVoucher(I)")
                {
                    cboProgramCode.Enabled = true;
                    this.programCodes = (from result in this.commonManager.GetVouchers(cboTenderType.SelectedValue.ToString())
                                         select new DropDownModel
                                         {
                                             Code = result.VoucherCode,
                                             Description = result.VoucherDesc
                                         }).ToList();
                    //"ExtendRightColumn=true" tendertypes.Insert(0, new DropDownModel { Code = null, Description = "Select" });
                    //cboProgramCode.DataSource = programCodes;
                    //cboProgramCode.DisplayMember = "Description";
                    //cboProgramCode.ValueMember = "Code";
                     
                     CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboProgramCode, this.programCodes);
                     cboProgramCode.SelectedText = "";
                }
                //else if (cboTenderType.SelectedValue.ToString() == "CLPPoint")
                //{
                //    cboProgramCode.Enabled = true;
                //    this.programCodes = (from result in this.commonManager.getclpprogram(CommonModel.SiteCode)
                //                         select new DropDownModel
                //                         {
                //                             Code = result.CLPProgramId,
                //                             Description = result.CLPProgramName
                //                         }).ToList();


                //    CommonFunc.PopulateComboBoxData(ref cboProgramCode, this.programCodes);
                //}
                else
                {
                    cboProgramCode.SelectedIndex = -1;
                    cboProgramCode.Enabled = false;
                }
            }
        }

        #endregion

        #endregion

        #region " Functions "

        private void setInitialFormsValidations()
        {
            try
            {
                txtTenderCode.MaxLength = 15;
                txtTenderName.MaxLength = 100;
                cboSite.Visible = false;
                lblSite.Visible = false;
                //this.tenderActionButton.btnClear.Visible = false;
                txtTenderCode.Focus();
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
                List<TenderModelList> tenders = new List<TenderModelList>();

                tenders = (from tender in this.tenderManager.GetTenderList()
                           select tender).ToList();


                dgTender.DataSource = tenders;

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
                dgTender.Cols["Select"].Width = 55;
                dgTender.Cols["Select"].Caption = string.Empty;
                dgTender.Cols["Select"].AllowEditing = true;
                dgTender.Cols["Select"].AllowResizing = false;
                dgTender.Cols["TenderHeadCode"].AllowEditing = false;
                dgTender.Cols["TenderHeadCode"].Width = 150;
                dgTender.Cols["TenderHeadCode"].AllowResizing = false;
                dgTender.Cols["TenderHeadName"].AllowEditing = false;
                dgTender.Cols["TenderHeadName"].Width = 160;
                dgTender.Cols["TenderType"].AllowResizing = false;
                dgTender.Cols["TenderType"].AllowEditing = false;
                dgTender.Cols["TenderType"].Width = 110;
                dgTender.Cols["ProgramId"].AllowResizing = false;
                dgTender.Cols["ProgramId"].AllowEditing = false;
                dgTender.Cols["ProgramId"].Width = 130;
                dgTender.Cols["ReturnIssue"].AllowResizing = false;
                dgTender.Cols["ReturnIssue"].AllowEditing = false;
                dgTender.Cols["ReturnIssue"].Width = 100;
                dgTender.Cols["RefundIssue"].AllowResizing = false;
                dgTender.Cols["RefundIssue"].AllowEditing = false;
                dgTender.Cols["RefundIssue"].Width = 100;

            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        private void clearForm()
        {
            this.txtTenderCode.Value = String.Empty;
            this.txtTenderName.Value = String.Empty;
            this.cboTenderType.SelectedIndex = -1;
            this.cboProgramCode.SelectedIndex = -1;
            this.cboProgramCode.Enabled = false;
            this.cboSite.Enabled = false;
            this.chkRefundIssue.Checked = false;
            this.chkReturnedIssue.Checked = false;
            //remove error indicator
            errorProvider.SetError(txtTenderCode, string.Empty);
            txtTenderCode.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtTenderName, string.Empty);
            txtTenderName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(cboTenderType, string.Empty);
            cboTenderType.BackColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(cboProgramCode, string.Empty);
            cboProgramCode.BackColor = CommonFunc.DefaultBorderColor;
        }

        private bool IsFormvalidate()
        {
            try
            {
                bool isValid = true;
                bool isTenderChecked = false;
                // Regex patt = new Regex("^[A-Za-z0-9]+$");
                Regex alphanumericpatt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");
                if (string.IsNullOrEmpty(txtTenderCode.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtTenderCode, "Tender Code required"))
                    {
                        this.txtTenderCode.Focus();
                        isValid = false;
                    }
                }
                else
                {

                    if (alphanumericpatt.IsMatch(txtTenderCode.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTenderCode, "Tender Code allows Alphanumeric Character", false))
                        {
                            this.txtTenderCode.Focus();
                        }

                    }
                    else
                    {
                        isTenderChecked = true;
                        errorProvider.SetError(txtTenderCode, string.Empty);
                        txtTenderCode.BorderColor = CommonFunc.DefaultBorderColor;
                    }


                }
                if (string.IsNullOrEmpty(txtTenderName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtTenderName, "Tender Name required"))
                    {
                        this.txtTenderName.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    if (alphanumericpatt.IsMatch(txtTenderName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTenderName, "Tender Name allows Alphanumeric Character", false))
                        {
                            this.txtTenderName.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtTenderName, string.Empty);
                        txtTenderName.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (cboTenderType.SelectedIndex == -1 || cboTenderType.SelectedValue == null)
                {
                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref errorProvider, ref cboTenderType, "Select Tender Type"))
                    {
                        this.cboTenderType.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    errorProvider.SetError(cboTenderType, string.Empty);
                    cboTenderType.BackColor = CommonFunc.DefaultBorderColor;
                    if (cboTenderType.SelectedValue.ToString() == "CreditVouc(I)" || cboTenderType.SelectedValue.ToString() == "GiftVoucher(R)" || cboTenderType.SelectedValue.ToString() == "CLPPoint")
                    {
                        if (cboProgramCode.SelectedIndex == -1 || cboProgramCode.SelectedValue == null)
                        {
                            if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref errorProvider, ref cboProgramCode, "Select Program Code"))
                            {
                                this.cboProgramCode.Focus();
                                isValid = false;
                            }
                        }
                        else
                        {
                            errorProvider.SetError(cboProgramCode, string.Empty);
                            cboProgramCode.BackColor = CommonFunc.DefaultBorderColor;
                        }
                    }
                    else
                    {
                        errorProvider.SetError(cboProgramCode, string.Empty);
                        cboProgramCode.BackColor = CommonFunc.DefaultBorderColor;
                    }
                }


                if (!string.IsNullOrEmpty(txtTenderCode.Text.Trim()) && isTenderChecked == true)
                {
                    var tenderDetails = this.tenderManager.GetTenderByID(txtTenderCode.Text.Trim());
                    if (tenderDetails != null)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtTenderCode, "Tender Code Already Exists For this Site", false))
                        {
                            this.txtTenderCode.Focus();
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtTenderCode, string.Empty);
                        txtTenderCode.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }

                if (isTenderChecked == false)
                    txtTenderCode.Focus();

                return isValid;
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
                txtTenderCode.TabIndex = 1;
                cboTenderType.TabIndex = 2;
                txtTenderName.TabIndex = 3;
                cboProgramCode.TabIndex = 4;
                chkReturnedIssue.TabIndex = 5;
                chkRefundIssue.TabIndex = 6;

                this.tenderActionButton.btnSave.TabIndex = 7;
                this.tenderActionButton.btnCancel.TabIndex = 8;
                this.tenderActionButton.btnClear.TabIndex = 9;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ThemeChange()
        {

            dgTender.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            dgTender.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            dgTender.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            dgTender.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            dgTender.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTender.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTender.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTender.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgTender.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            dgTender.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            dgTender.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            groupBox1.BackColor = Color.FromArgb(134, 134, 134);
            groupBox2.BackColor = Color.FromArgb(134, 134, 134);

            groupBox1.ForeColor = Color.FromArgb(255, 255, 255);
            groupBox2.ForeColor = Color.FromArgb(255, 255, 255);
            sizerTender.BackColor = Color.FromArgb(134, 134, 134);

            btnArticleDelete.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnArticleDelete.BackColor = Color.Transparent;
            btnArticleDelete.BackColor = Color.FromArgb(0, 107, 163);
            btnArticleDelete.ForeColor = Color.FromArgb(255, 255, 255);
            btnArticleDelete.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnArticleDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnArticleDelete.FlatAppearance.BorderSize = 0;
            btnArticleDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


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

            tenderActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            tenderActionButton.btnSave.BackColor = Color.Transparent;
            tenderActionButton.btnSave.BackColor = Color.FromArgb(0, 107, 163);
            tenderActionButton.btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            tenderActionButton.btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            tenderActionButton.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            tenderActionButton.btnSave.FlatAppearance.BorderSize = 0;
            tenderActionButton.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            lblProgramCode.BackColor = Color.FromArgb(212, 212, 212);
            lblRefundIssue.BackColor = Color.FromArgb(212, 212, 212);
            lblReturnedIssue.BackColor = Color.FromArgb(212, 212, 212);
            lblSite.BackColor = Color.FromArgb(212, 212, 212);
            lblTenderCode.BackColor = Color.FromArgb(212, 212, 212);
            lblTenderName.BackColor = Color.FromArgb(212, 212, 212);
            lblTenderType.BackColor = Color.FromArgb(212, 212, 212);

        }
        #endregion

         

    }
}
