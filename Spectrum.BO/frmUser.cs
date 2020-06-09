using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Spectrum.Cryptography;
using System.Drawing;
using System.Reflection;
using System.Net.Mail;
namespace Spectrum.BO
{
    public partial class frmUser : Spectrum.Controls.RibbonForm
    {
        public frmUser()
        {
            InitializeComponent();
            this.userManager = new UserManager();
            this.commonManager = new CommonManager();
              this.articleHierarchyManager = new ArticleHierarchyManager();
        }

        #region "Class Variable"
         IArticleHierarchyManager articleHierarchyManager;
     //   Spectrum.BL.ArticleHierarchyManager articleHierarchyManager=new ArticleHierarchyManager();
        IUserManager userManager;
        ICommonManager commonManager;
        //IQueryable<MstTransactionModel> node;
        IList<DropDownModel> Roles;
        IList<DropDownModel> Sites;
        bool flagAddEdit = false;
        bool flagRoleAddEdit = false;
        bool flagSalesAddEdit = false;
        string sitecode;
        string username;
        private const string SYSTEM_PASSWORD = "creative";
        //string user;
        //string role;
        //bool isExit;
        #endregion

        #region "Events"

        #region " Other Events "

        private void frmUser_Load(object sender, EventArgs e)
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
                txtUserName.KeyPress += new KeyPressEventHandler(txtValidateSpecialCharSpace);
              //  setTabIndex();
                fillRoles();
                cboRole.SelectedIndex = 0; 
                fillSites();
                setInitialFormsValidations();
                // CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);
                lblSearch.Visible = true;
                 sitecode = CommonModel.SiteCode;
                 treeView1.Nodes.Clear();
                 txtSearch.Enabled = false; 
                               
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }



        private void txtValidateSpecialCharSpace(object sender, KeyPressEventArgs e)
        {
            if (!CommonFunc.validateNumberAlphabet(e.KeyChar.ToString()))
            {
                e.Handled = true;
                //CommonFunc.ShowMessage(" Only Characters and numbers allowed ", MessageType.Information);
                //  CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtUserName, "User Name allows Alphanumeric Character", false);
            }

        }

        private void chkSalesPerson_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSalesPerson.Checked == true)
                {
                    pnlSalesField.Visible = true;
                }
                else
                {
                    pnlSalesField.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion

        #region " Buttons Events "

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ((IsFormValidate()))
                {
                    username = txtName.Text.ToString();
                    UserModel userModel = new UserModel();
                    userModel.AuthUserModel = FillAuthUserDataToModel();
                    userModel.AuthUserSiteRoleMapModel = null;
                    userModel.MstSalesPersonModel = null;
                    userModel.AuthUserModel.Password = Encrypter.getEncryptedPassword(userModel.AuthUserModel.Password);

                    //if (chkSalesPerson.Checked == true && cboRole.SelectedIndex != -1)
                    //    userModel.AuthUserSiteRoleMapModel = FillAuthUserSiteRoleMap();
                    //else if (cboRole.SelectedIndex != -1)
                    //    userModel.AuthUserSiteRoleMapModel = FillAuthUserSiteRoleMap();
                    //if (chkSalesPerson.Checked == true)
                    //    userModel.MstSalesPersonModel = FillMstSalesPersonModel();

                    //ashma 24 may 2018
                    if (cboRole.SelectedValue != null)
                    {
                        if (chkSalesPerson.Checked == true)
                        {
                            userModel.MstSalesPersonModel = FillMstSalesPersonModel();
                         
                        }

                        userModel.AuthUserSiteRoleMapModel = FillAuthUserSiteRoleMap();
                        string newusername = userModel.AuthUserModel.UserID;

                        bool Status = false;
                        if (flagAddEdit == true)
                        {
                            Status = this.userManager.UpdateUser(userModel, flagRoleAddEdit, flagSalesAddEdit);
                            if (this.articleHierarchyManager.UpdateAuthUserSite(dtNodes, newusername))
                            {
                                if (Status == true)
                                    CommonFunc.ShowMessage("User " + userModel.AuthUserModel.UserID + " Updated Successfully.", MessageType.Information);
                                else
                                    CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                            }
                        }
                        else
                        {
                            Status = this.userManager.AddUser(userModel);
                            if (this.articleHierarchyManager.AddAuthUserSite(dtNodes, newusername))
                            {
                                if (Status == true)
                                {
                                    CommonFunc.ShowMessage("User " + userModel.AuthUserModel.UserID + " Added Successfully.", MessageType.Information);
                                    sendmail();
                                }
                                else
                                {
                                    CommonFunc.ShowMessage("Error!!!", MessageType.Information);
                                }
                            }
                        }
                        clearForm();
                    }
                    //ashma 24 may 2018
                    else
                    {
                        CommonFunc.ShowMessage("Please Assign Role", MessageType.Information);
                        this.cboRole.Focus();
                    }
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                //clearForm();// Commented By Mahesh No need to clear form here ..just check user name is available or not 
                lblMessage.Text = string.Empty;
                Regex alphanumericpatt = new Regex("^[A-Za-z0-9!@#$%&*()-{}.,/ ]+$");
                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtUserName, "Login Code is Required"))
                    {
                        this.txtUserName.Focus();
                        // lblMessage.Text = "User Name Required";
                    }
                }
                else
                {
                    if (alphanumericpatt.IsMatch(txtUserName.Text) == false)
                    {
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtUserName, "Login Code allows Alphanumeric Character", false))
                        {
                            this.txtUserName.Focus();
                            //lblMessage.Text = "User Name allows Alphanumeric Character";
                        }
                    }
                    else
                    {
                        errorProvider.SetError(txtUserName, string.Empty);
                        txtUserName.BorderColor = CommonFunc.DefaultBorderColor;
                        // lblMessage.Visible = true;
                        var IsUserExist = userManager.IsUserExist(txtUserName.Text.Trim(), CommonModel.SiteCode);
                        if (IsUserExist != null)
                        {
                            lblMessage.Text = "Login Code already Exists";
                            lblMessage.Visible = true;
                        }
                        else
                            lblMessage.Text = "Valid Login Code";
                            lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunc.ShowMessage(ex.Message, MessageType.Information);
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //AuthUserModelEdit userSelectedList = null;
                var userList = this.userManager.GetUserListEdit();
                foreach (AuthUserModelEdit authUserEdit in userList)
                {
                    if (authUserEdit.Active == "A")
                        authUserEdit.Active = "Active";
                    else
                        authUserEdit.Active = "In-active";
                }
                //frmCommonSearch objSearch = new frmCommonSearch();
                //objSearch.DataList = userList;
                frmCommonSearchTrueGrid objSearch = new frmCommonSearchTrueGrid(multipleSelect: false, defaultFilter: true);
                objSearch.Text = "User Search";
                objSearch.boolWildSearch = false;
                DataTable dtUser = CommonFunc.ConvertListToDataTable(userList);
                //dtUser.Columns[0].ColumnName = "s";
                dtUser.Columns[0].ColumnName = "Login Name";
                dtUser.Columns[1].ColumnName = "User Name";
                dtUser.Columns[2].ColumnName = "Designation";
                dtUser.Columns[3].ColumnName = "Email Address";
                dtUser.Columns[4].ColumnName = "Is User Active?";
                objSearch.dtcommonSearch = dtUser;
                DataTable dtSelectedUser = new DataTable();
                if (objSearch.ShowDialog() == DialogResult.OK)
                    dtSelectedUser = objSearch.dtSelectedList;
                //userSelectedList = (objSearch.neTbl).Cast<AuthUserModelEdit>().FirstOrDefault();

                if (dtSelectedUser.Rows.Count > 0)
                {
                    clearForm();
                    txtUserName.ReadOnly = false;
                    var userDetail = this.userManager.GetUserById(dtSelectedUser.Rows[0]["Login Name"].ToString());
                    txtPassword.Visible = false;
                    txtConfirmPassword.Visible = false;
                    lblpassword.Visible = false;
                    lblConfirmPassword.Visible = false;
                    txtUserName.Text = userDetail.UserID;
                    txtUserName.ReadOnly = true;
                    txtName.Text = userDetail.UserName;
                    txtEmail.Text = userDetail.EmailId;
                    txtDesignation.Text = userDetail.Designation;
                    if (userDetail.Active == "A")
                        chkUserActive.Checked = true;
                    else
                        chkUserActive.Checked = false;
                   // cboSite.SelectedValue = userDetail.SiteCode;
                    cboSite.SelectedValue = CommonModel.SiteCode;
                    fillRoles();
                    var roleDetails = this.userManager.GetRolesDataById(dtSelectedUser.Rows[0]["Login Name"].ToString());
                    if (roleDetails != null)
                    {
                        foreach (var item in cboRole.Items)
                        {
                            if (roleDetails.GRoleid == ((DropDownModel)item).Code)
                            {
                                flagRoleAddEdit = true;
                                cboRole.SelectedValue = roleDetails.GRoleid;
                            }
                        }
                    }
                    if (userDetail.issalesperson == true)
                    {
                        chkSalesPerson.Checked = true;
                        flagSalesAddEdit = true;
                        var salesPersonDetails = this.userManager.GetSalesPersonById(dtSelectedUser.Rows[0]["Login Name"].ToString());
                        if (salesPersonDetails != null)
                        {
                            pnlSalesField.Visible = true;
                            txtSalesArea.Text = salesPersonDetails.SalesArea;
                            txtSalesSection.Text = salesPersonDetails.SalesSection;
                        }
                    }
                    else
                        chkSalesPerson.Checked = false;

                    txtIdCard.Text = (!string.IsNullOrEmpty(userDetail.IDNumber) ? userDetail.IDNumber : "");
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

        #endregion

        #endregion

        #region " Functions "

        private AuthUserModel FillAuthUserDataToModel()
        {
            try
            {
                return new AuthUserModel
                {
                    UserID = txtUserName.Text.Trim(),
                    SiteCode = cboSite.SelectedValue.ToString(),
                    UserName = txtName.Text.Trim(),
                    CountryCode = "IND",
                    Designation = (!string.IsNullOrEmpty(txtDesignation.Text.Trim())) ? txtDesignation.Text.Trim() : "",
                    IDNumber = (!string.IsNullOrEmpty(txtIdCard.Text.Trim())) ? txtIdCard.Text.Trim() : "",
                    Password = txtPassword.Text.Trim(),
                    PasswordUpdateDate = DateTime.Now,
                    PasswordChangeNextDate = DateTime.Now.AddDays(90),
                    EmailId = txtEmail.Text.Trim(),
                    PasswordExpiredon = DateTime.Now.AddDays(365),
                    Active = (chkUserActive.Checked) ? "A" : "D",
                    CreatedAt = CommonModel.SiteCode,
                    CreatedBy = CommonModel.UserID,
                    CreatedOn = CommonModel.CurrentDate,
                    UpdatedAt = CommonModel.SiteCode,
                    UpdatedBy = CommonModel.UserID,
                    UpdatedOn = CommonModel.CurrentDate,
                    Status = true,
                    issalesperson = (chkSalesPerson.Checked) ? true : false

                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }

        }

        private AuthUserSiteRoleMapModel FillAuthUserSiteRoleMap()
        {
            try
            {
                return new AuthUserSiteRoleMapModel
                {
                    UserID = txtUserName.Text.Trim(),
                    SiteCode = cboSite.SelectedValue.ToString(),
                    GRoleid = cboRole.SelectedValue.ToString(),
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

        private MstSalesPersonModel FillMstSalesPersonModel()
        {
            try
            {
                return new MstSalesPersonModel
                {

                    SiteCode = cboSite.SelectedValue.ToString(),
                    Empcode = txtUserName.Text.Trim(),
                    SalesPersonName = txtName.Text.Trim(),
                    SalesPersonFullName = txtName.Text.Trim(),
                    SalesArea = (!string.IsNullOrEmpty(txtSalesArea.Text.Trim())) ? txtSalesArea.Text.Trim() : "",
                    SalesSection = (!string.IsNullOrEmpty(txtSalesSection.Text.Trim())) ? txtSalesSection.Text.Trim() : "",
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

        private void setInitialFormsValidations()
        {
            try
            {
                /// Change length Mahesh 22082014
                //txtUserName.MaxLength = 45;
                //txtName.MaxLength = 50;
                txtUserName.MaxLength = 15;
                txtName.MaxLength = 45;
                txtDesignation.MaxLength = 50;
                txtEmail.MaxLength = 100;

                chkUserActive.Checked = true;
                txtPassword.MaxLength = 15;
                txtConfirmPassword.MaxLength = 15;
                txtIdCard.MaxLength = 150;
                lblMessage.Visible = false;
                lblMesIdCard.Visible = false;
                txtUserName.ReadOnly = false;
                pnlSalesField.Visible = false;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void fillRoles()
        {
            try
            {
                this.Roles = (from result in this.commonManager.GetRoles()
                              select new DropDownModel
                              {
                                  Code = result.RoleID,
                                  Description = result.Description
                              }).ToList();
                Roles.Insert(0, new DropDownModel { Code = null, Description = "" });
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboRole, this.Roles);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void fillSites()
        {
            try
            {
                this.Sites = (from result in this.commonManager.GetSiteList()
                              select new DropDownModel
                              {
                                  Code = result.SiteCode,
                                  Description = result.SiteShortName
                              }).ToList();
                CommonFunc.PopulateComboBoxDataForWindowsForm(ref cboSite, this.Sites);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void clearForm()
        {
            
            this.txtUserName.Value = String.Empty;            
            txtUserName.ReadOnly = false;
            this.txtName.Value = String.Empty;
            this.txtDesignation.Value = String.Empty;
            this.txtEmail.Value = String.Empty;
          //  this.cboSite.SelectedIndex = -1;

            chkUserActive.Checked = true;

            lblpassword.Visible = true;
            lblConfirmPassword.Visible = true;
            txtPassword.Visible = true;
            txtConfirmPassword.Visible = true;
            this.txtPassword.Value = String.Empty;
            this.txtConfirmPassword.Value = String.Empty;
            this.txtIdCard.Value = String.Empty;
                     
            
            if (pnlSalesField.Visible == true)
            {
                txtSalesArea.Text = "";
                txtSalesSection.Text = "";
                pnlSalesField.Visible = false;
            }
            flagAddEdit = false;
            flagRoleAddEdit = false;
            flagSalesAddEdit = false;
            chkSalesPerson.Checked = false;
            lblMesIdCard.Text = "";
            lblMessage.Text = "";
            //remove error indicator
            errorProvider.SetError(txtUserName, string.Empty);
            txtUserName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtName, string.Empty);
            txtName.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtDesignation, string.Empty);
            txtDesignation.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtEmail, string.Empty);
            txtEmail.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtPassword, string.Empty);
            txtPassword.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtConfirmPassword, string.Empty);
            txtConfirmPassword.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(txtIdCard, string.Empty);
            txtIdCard.BorderColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(cboRole, string.Empty);
            cboRole.BackColor = CommonFunc.DefaultBorderColor;
            errorProvider.SetError(cboSite, string.Empty);
            cboSite.BackColor = CommonFunc.DefaultBorderColor;
            treeView1.Nodes.Clear();
            dtNodes.Clear();
            //cboRole.Items.RemoveAt(0);
           // cboRole.SelectedValue = "";
         //   cboRole.SelectedIndex = -1;
            cboRole.SelectedIndex = 0;
        }

        private bool IsFormValidate()
        {
            try
            {
                bool isValid = true;

                //Regex emailpatt = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //Regex patt = new Regex("^[A-Za-z0-9]+$");
                //Regex alphanumericpatt = new Regex("[!@#$%&*()-{}.,/ ]+$");
                char[] SpecialChars = "[!@#$%&*()-{}.,/]+$".ToCharArray();   //vipin

                if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtUserName, "Login Code is Required"))
                    {
                        this.txtUserName.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    int indexOf = txtUserName.Text.IndexOfAny(SpecialChars);
                    if (indexOf != -1)
                   // if (alphanumericpatt.IsMatch(txtUserName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtUserName, "Login Code Allows Alphanumeric Character", false))
                        {
                            this.txtUserName.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtUserName, string.Empty);
                        txtUserName.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtName, "User Name Required"))
                    {
                        this.txtName.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    int indexOf = txtName.Text.IndexOfAny(SpecialChars);
                    if (indexOf != -1)
                   // if (alphanumericpatt.IsMatch(txtName.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtName, "User Name Allows Alphanumeric Character", false))
                        {
                            this.txtName.Focus();
                        }

                 
                    }
                    else
                    {
                        errorProvider.SetError(txtName, string.Empty);
                        txtName.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (!string.IsNullOrEmpty(txtDesignation.Text.Trim()))
                {
                    int indexOf = txtDesignation.Text.IndexOfAny(SpecialChars);
                    if (indexOf != -1)
                   // if (alphanumericpatt.IsMatch(txtDesignation.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtDesignation, "Designation Allows Alphanumeric Character", false))
                        {
                            this.txtDesignation.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtDesignation, string.Empty);
                        txtDesignation.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {

                    if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtEmail, "Email Address is Required"))
                    {
                        this.txtEmail.Focus();
                        isValid = false;
                    }
                }
                else
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
                if (cboSite.SelectedIndex == -1 || cboSite.SelectedValue == null)
                {
                    if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref errorProvider, ref cboSite, "Please Select Site"))
                    {
                        this.cboSite.Focus();
                        isValid = false;
                    }
                }
                else
                {
                    errorProvider.SetError(cboSite, string.Empty);
                    cboSite.BackColor = CommonFunc.DefaultBorderColor;
                }
                if (txtPassword.Visible && txtConfirmPassword.Visible)
                {
                    if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtPassword, "Password is Required"))
                        {
                            this.txtPassword.Focus();
                            isValid = false;
                        }
                    }
                    else
                    {

                        if (txtPassword.Text.Trim().Length < 6 || txtPassword.Text.Trim().Length > 15)
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtPassword, "Password lenght should be between 6 to 15 Characters", false))
                            {
                                this.txtPassword.Focus();
                            }

                        }
                        else
                        {
                            errorProvider.SetError(txtPassword, string.Empty);
                            txtPassword.BorderColor = CommonFunc.DefaultBorderColor;
                        }

                    }
                    if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                    {
                        if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtConfirmPassword, "Please Confirm Password"))
                        {
                            this.txtConfirmPassword.Focus();
                            isValid = false;
                        }
                    }
                    else
                    {

                        if (txtConfirmPassword.Text.Trim().Length < 6 || txtConfirmPassword.Text.Trim().Length > 15)
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtConfirmPassword, "Password lenght should be between 6 to 15 Characters", false))
                            {
                                this.txtConfirmPassword.Focus();
                            }

                        }
                        else if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                        {
                            isValid = false;
                            if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtConfirmPassword, "Passwords do not match", false))
                            {
                                this.txtConfirmPassword.Focus();
                            }

                        }
                        else
                        {
                            errorProvider.SetError(txtConfirmPassword, string.Empty);
                            txtConfirmPassword.BorderColor = CommonFunc.DefaultBorderColor;
                        }

                    }
                }

                if (!string.IsNullOrEmpty(txtIdCard.Text.Trim()))
                {
                    int indexOf = txtIdCard.Text.IndexOfAny(SpecialChars);
                    if (indexOf != -1)

                  //  if (alphanumericpatt.IsMatch(txtIdCard.Text) == false)
                    {
                        isValid = false;
                        if (!CommonFunc.SetCustomErrorProvidertoControl(ref errorProvider, ref txtIdCard, "Id Card allows Alphanumeric Character", false))
                        {
                            this.txtIdCard.Focus();
                        }

                    }
                    else
                    {
                        errorProvider.SetError(txtIdCard, string.Empty);
                        txtIdCard.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                }
                if (chkSalesPerson.Checked == false)
                {
                    if (cboRole.SelectedIndex == -1 || cboRole.SelectedValue == null)
                    {
                        if (!CommonFunc.SetErrorProvidertoControlForWindowsForm(ref errorProvider, ref cboRole, "Please Select Role"))
                        {
                            this.cboRole.Focus();
                            isValid = false;
                        }
                    }
                    else
                    {
                        errorProvider.SetError(cboRole, string.Empty);
                        cboRole.BackColor = CommonFunc.DefaultBorderColor;
                    }
                }
                else
                {
                    errorProvider.SetError(cboRole, string.Empty);
                    cboRole.BackColor = CommonFunc.DefaultBorderColor;
                }
                if (!string.IsNullOrEmpty(txtUserName.Text.Trim()) && flagAddEdit == false)
                {

                    lblMessage.Visible = true;
                    var IsUserExist = userManager.IsUserExist(txtUserName.Text.Trim(), CommonModel.SiteCode);
                    if (IsUserExist != null)
                    {
                        isValid = false;
                        //if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtUserName, "User Name Is Already Exist"))
                        //{
                        //    this.txtUserName.Focus();

                        //}
                        lblMessage.Text = "Login Code already Exists";
                    }
                    else
                    {
                        lblMessage.Text = "";
                        //errorProvider.SetError(txtUserName, string.Empty);
                        //txtUserName.BorderColor = CommonFunc.DefaultBorderColor;
                    }

                }
                if (!string.IsNullOrEmpty(txtIdCard.Text.Trim()))
                {

                    lblMesIdCard.Visible = true;
                    if (flagAddEdit == false)
                    {
                        var IsCardIdExist = userManager.IsIdCardExist(txtIdCard.Text.Trim());
                        if (IsCardIdExist != null)
                        {
                            isValid = false;
                            //if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtIdCard, "This Card Already Assigned to Other User."))
                            //{
                            //    this.txtIdCard.Focus();

                            //}
                            lblMesIdCard.Text = "This Card Already Assigned to Other User.";
                        }
                        else
                        {
                            lblMesIdCard.Text = "";
                            //errorProvider.SetError(txtIdCard, string.Empty);
                            //txtIdCard.BorderColor = CommonFunc.DefaultBorderColor;
                        }
                    }
                    else
                    {
                        var IsCardIdExistforOtherUser = userManager.IsIdCardExistForOtherUser(txtIdCard.Text.Trim(), txtUserName.Text.Trim());
                        if (IsCardIdExistforOtherUser != null)
                        {
                            isValid = false;
                            //if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtIdCard, "This Card Already Assigned to Other User."))
                            //{
                            //    this.txtIdCard.Focus();

                            //}
                            lblMesIdCard.Text = "This Card Already Assigned to Other User.";
                        }
                        else
                        {
                            //errorProvider.SetError(txtIdCard, string.Empty);
                            //txtIdCard.BorderColor = CommonFunc.DefaultBorderColor;
                            lblMesIdCard.Text = "";
                        }
                    }

                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void setTabIndex()
        {
            try
            {
                txtUserName.TabIndex = 0;
                btnSearch.TabIndex = 1;
                btnCheck.TabIndex = 2;
                txtName.TabIndex = 3;
                txtPassword.TabIndex = 4;
                txtDesignation.TabIndex = 5;
                txtConfirmPassword.TabIndex = 6;
                txtEmail.TabIndex = 7;
                txtIdCard.TabIndex = 8;
                cboSite.TabIndex = 9;
                cboRole.TabIndex = 10;
                chkUserActive.TabIndex = 11;
                chkSalesPerson.TabIndex = 12;
                pnlSalesField.TabIndex = 13;
                txtSearch.TabIndex = 14;
                txtSalesArea.TabIndex = 1;
                txtSalesSection.TabIndex = 2;
                tenderActionButton.btnSave.TabIndex = 15;
                tenderActionButton.btnCancel.TabIndex = 16;
                tenderActionButton.btnClear.TabIndex = 17;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            groupBox1.BackColor = Color.FromArgb(134, 134, 134);
            groupBox1.ForeColor = Color.FromArgb(255, 255, 255);
            c1Sizer1.BackColor = Color.FromArgb(134, 134, 134);
            //lblConfirmPassword.BackColor = Color.FromArgb(212, 212, 212);
            //lblDesignation.BackColor = Color.FromArgb(212, 212, 212);
            //lblEmail.BackColor = Color.FromArgb(212, 212, 212);
            //lblIdCard.BackColor = Color.FromArgb(212, 212, 212);
            //lblMesIdCard.BackColor = Color.FromArgb(212, 212, 212);
            //lblMessage.BackColor = Color.FromArgb(212, 212, 212);
            //lblName.BackColor = Color.FromArgb(212, 212, 212);
            //lblpassword.BackColor = Color.FromArgb(212, 212, 212);
            //lblRole.BackColor = Color.FromArgb(212, 212, 212);
            //lblSalesArea.BackColor = Color.FromArgb(212, 212, 212);
            //lblSalesPerson.BackColor = Color.FromArgb(212, 212, 212);
            //lblSalesSection.BackColor = Color.FromArgb(212, 212, 212);
            //lblSiteCode.BackColor = Color.FromArgb(212, 212, 212);
            //lblUserActive.BackColor = Color.FromArgb(212, 212, 212);
            //lblUserName.BackColor = Color.FromArgb(212, 212, 212);
         

            btnCheck.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCheck.BackColor = Color.Transparent;
            btnCheck.BackColor = Color.FromArgb(0, 107, 163);
            btnCheck.ForeColor = Color.FromArgb(255, 255, 255);
            btnCheck.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCheck.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCheck.FlatAppearance.BorderSize = 0;
            btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


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

            pnlSalesField.BackColor = Color.FromArgb(212, 212, 212);
        }
        #endregion

        ImageList _imageList;
        public ImageList ImageList
        {
            get
            {
                if (_imageList == null)
                {
                    _imageList = new ImageList();
                    //   _imageList.Images.Add("treeImage", Properties.Resources.search_2);
                    _imageList.ImageSize = new Size(16, 16);

                    //' Add some system icons to the ImageList
                    _imageList.Images.Add(Properties.Resources.iconFolder);
                    _imageList.Images.Add(Properties.Resources.iconLeaf);
                    _imageList.Images.Add(Properties.Resources.tree);
                    _imageList.Images.Add(Properties.Resources.treeColapse);
                    _imageList.Images.Add(Properties.Resources.treeExpand);
                }
                return _imageList;
            }
        }

        private void cboRole_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Enabled = true;
                txtSearch.Text = "";
                string site = txtName.Text;
                txtSearch.Focus();
                
                //if (cboRole.SelectedValue != "")
                //{
                //ashma 24 may 2018
                if (cboRole.SelectedValue != null)
                {
                    if (cboRole.SelectedIndex != 0)
                    {
                        //ashma 24 may 2018
                        if (string.IsNullOrEmpty(txtName.Text.Trim()))
                        {
                            if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtName, "User Name Required"))
                            {
                                this.txtName.Focus();
                            }
                        }
                        else
                        {
                            if (treeView1.Nodes.Count > 0)
                            {
                                treeView1.Nodes.Clear();
                            }
                            TreeView();
                        }
                    }
                    else
                    {
                        treeView1.Nodes.Clear();
                    }
                }
                //ashma 24 may 2018
                else
                {
                    treeView1.Nodes.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void TreeView()
        {
            try
            {           
                        treeView1.ImageList = ImageList;
                        treeView1.CheckBoxes = true;
                        var res = this.articleHierarchyManager.GetRoleHierarchyList(sitecode);
                        // var nodes = res.Select(o => o.MainFunction).Distinct().ToList();
                        var nodes = (from r in res orderby r.MainFunction select r.MainFunction).Distinct().ToList();

                        TreeNode rootNodeNode = new TreeNode();

                        rootNodeNode.Text = "Transaction";
                        rootNodeNode.Tag = "Transaction";
                        foreach (var item in nodes)
                        {
                            TreeNode parentnode = new TreeNode();
                            parentnode.Text = item;
                            parentnode.Tag = item;
                            //parentnode.SelectedImageIndex = 1;

                            populateTreeView(item, ref parentnode);
                            rootNodeNode.Nodes.Add(parentnode);
                        }
                        treeView1.Nodes.Add(rootNodeNode);
                        treeView1.Visible = true;
                        treeView1.ExpandAll();
                   }
            catch (Exception ex)
            {
                
                throw ex ;
            }
        }
        public void populateTreeView(string parentid,ref TreeNode parentnode)
        {
            try
            {
                string user = txtUserName.Text;
                string role = ((DropDownModel)cboRole.SelectedItem).Code;
                var rest = this.articleHierarchyManager.GetRoleHierarchyList(sitecode);
                var child = rest.Where(x => x.MainFunction == parentid).Distinct().ToList();
               // var childnodes = child.Select(a=>a.subfunction).Distinct().ToList;
                var childnodes = (from r in child orderby r.subfunction select r.subfunction).Distinct().ToList();
                var GetStatus = this.articleHierarchyManager.GetUserRoleMap(role);
                var statusrole = GetStatus.Select(x => x.TransactionName).ToList();
                //var isExit = this.articleHierarchyManager.isUserExits(user);
                //var getNodeTable = this.articleHierarchyManager.GetNodeTableForUpdate(user);
                //var getUpdateTable = getNodeTable.Select(x => new { x.TransactionCode, x.TransactionName }).ToList();
                //var getNodeNewTable = this.articleHierarchyManager.GetNodeTableForNewUpdate(user);
                //var getUpdateNewTable = getNodeNewTable.Select(x => new { x.TransactionCode, x.TransactionName }).ToList();
                foreach (var items in childnodes)
                {
                    if (parentnode != null)
                    {
                            TreeNode childnode = new TreeNode();
                            childnode.Text = items;
                            childnode.Tag = items;
                            foreach (var maketrue in statusrole)
                            {
                                if (maketrue == items)
                                {
                                    childnode.Checked = true;
                                }
                            }
                            populateGrandChildTreeView(parentid,items, ref childnode);
                            parentnode.Nodes.Add(childnode);
                            //if (isExit)
                            //{

                            //    foreach (var i in getUpdateTable)
                            //    {
                            //        if (items == i.TransactionName)
                            //        {
                            //            var checkUsrStatus = this.articleHierarchyManager.CheckUserStatus(i.TransactionCode, user);
                            //            if (checkUsrStatus)
                            //            {
                            //                childnode.Checked = true;
                            //            }
                            //        }
                            //    }

                            //    foreach (var i in getUpdateNewTable)
                            //    {
                            //        if (items == i.TransactionName)
                            //        {
                            //            var checkUsrNewStatus = this.articleHierarchyManager.CheckUserNewStatus(i.TransactionCode, user);
                            //            if (checkUsrNewStatus)
                            //            {
                            //                childnode.Checked = false;
                            //            }
                            //        }
                            //    }

                            //}
                       
                    }
                   
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void populateGrandChildTreeView(string parentid,string GrandChildid, ref TreeNode childnode)
        {
            try
            {
                string user = txtUserName.Text;
                string role = ((DropDownModel)cboRole.SelectedItem).Code;
                var rest = this.articleHierarchyManager.GetRoleHierarchyList(sitecode);
                var Grandchild = rest.Where(x => x.MainFunction == parentid && x.subfunction== GrandChildid).Distinct().ToList();
                // var childnodes = child.Select(a=>a.subfunction).Distinct().ToList;
                var childnodes = (from r in Grandchild orderby r.TransactionName select r.TransactionName).Distinct().ToList();
                var GetStatus1 = this.articleHierarchyManager.GetUserRoleMap(role);
                var statusrole1 = GetStatus1.Select(x => x.TransactionName).ToList();
                var isExit = this.articleHierarchyManager.isUserExits(user);
                var getNodeTable = this.articleHierarchyManager.GetNodeTableForUpdate(user);
                var getUpdateTable = getNodeTable.Select(x => new {x.TransactionCode,x.TransactionName }).ToList();
                var getNodeNewTable = this.articleHierarchyManager.GetNodeTableForNewUpdate(user);
                var getUpdateNewTable = getNodeNewTable.Select(x => new { x.TransactionCode, x.TransactionName }).ToList();
                foreach (var family in childnodes)
                {
                    TreeNode Grandchildnode = new TreeNode();
                    Grandchildnode.Text = family;
                    Grandchildnode.Tag = family;

                    foreach (var maketrue in statusrole1)
                    {
                        if (maketrue == family)
                        {
                            Grandchildnode.Checked = true;
                        }
                    }


                       
                        Grandchildnode.SelectedImageIndex = 1;
                        Grandchildnode.ImageIndex = 1;

                        childnode.Nodes.Add(Grandchildnode);
                        //Grandchildnode.Expand();

                        if (isExit)
                        {
                            
                                foreach (var i in getUpdateTable)
                                {
                                    if (family == i.TransactionName)
                                    {
                                        var checkUsrStatus = this.articleHierarchyManager.CheckUserStatus(i.TransactionCode, user);
                                        if (checkUsrStatus)
                                        {
                                            Grandchildnode.Checked = true;
                                        }
                                    }
                                }
                           
                                foreach (var i in getUpdateNewTable)
                                {
                                    if (family == i.TransactionName)
                                    {
                                        var checkUsrNewStatus = this.articleHierarchyManager.CheckUserNewStatus(i.TransactionCode, user);
                                        if (checkUsrNewStatus)
                                        {
                                            Grandchildnode.Checked = false;
                                        }
                                    }
                                }

                        }
                        
                    }
                
            
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckedUncheckedNodes(TreeNode treeNode, bool chk)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.Checked = chk;
                CheckedUncheckedNodes(tn, chk);
            }
        }

        DataTable dtNodes = new DataTable();
      
      

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                string role = ((DropDownModel)cboRole.SelectedItem).Code;
                var GetStatus = this.articleHierarchyManager.GetUserRoleMap(role);
                var statusrole = GetStatus.Select(x => new { x.TransactionName, x.TransactionCode }).ToList();
                if (!dtNodes.Columns.Contains("Nodes"))
                {
                    dtNodes.Columns.Add("Nodes", System.Type.GetType("System.String"));
                    dtNodes.Columns.Add("Status", System.Type.GetType("System.String"));
                }

                if (e.Node.Checked == true)
                {
                    string Tcode = this.articleHierarchyManager.GetTransCode(e.Node.Text);
                    bool exists = dtNodes.Select().ToList().Exists(row => row["Nodes"].ToString().ToUpper() == Tcode);
                    if (exists)
                    {
                        for (int i = 0; i < dtNodes.Rows.Count; i++)
                        {
                            if (dtNodes.Rows[i]["Nodes"].ToString() == Tcode.ToString())
                            {
                                dtNodes.Rows[i]["Status"] = true;
                            }
                        }
                    }
                    else
                        if (!(string.IsNullOrEmpty(Tcode)))
                        {
                            dtNodes.Rows.Add(Tcode, "True");
                        }
                }
                if (e.Node.Checked == false)
                {
                    string Tcode = this.articleHierarchyManager.GetTransCode(e.Node.Text);
                    bool exists = dtNodes.Select().ToList().Exists(row => row["Nodes"].ToString().ToUpper() == Tcode);
                    if (exists)
                    {
                        for (int i = 0; i < dtNodes.Rows.Count; i++)
                        {
                            if (dtNodes.Rows[i]["Nodes"].ToString() == Tcode.ToString())
                            {
                                dtNodes.Rows[i]["Status"] = false;
                            }
                        }
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(Tcode)))
                        {
                            dtNodes.Rows.Add(Tcode, "False");
                        }
                    }
                }
                bool chk = e.Node.Checked;
                CheckedUncheckedNodes(e.Node, chk);
                if (dtNodes.Rows.Count > 0)
                {
                    dtNodes = dtNodes.DefaultView.ToTable(true, "Nodes", "Status");
                }
            }
           
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
        private bool NodeFiltering(TreeNode Nodo, string Texto)
        {
            bool resultado = false;

            if (Nodo.Nodes.Count == 0)
            {
                if (Nodo.Text.ToUpper().Contains(Texto.ToUpper()))
                {
                    resultado = true;
                }
                else
                {
                    Nodo.Remove();
                }
            }
            else
            {
                for (int i = Nodo.Nodes.Count; i > 0; i--)
                {
                    if (NodeFiltering(Nodo.Nodes[i - 1], Texto))
                        resultado = true;
                }

                if (!resultado)
                    Nodo.Remove();
                
            }

            return resultado;
        }

      

        

      //  private void txtSearch_TextChanged(object sender, EventArgs e)
   
      //{
      //      try
      //      {
      //        ////  cboRole_TextChanged(sender, e);
      //         // treeView1.BeginUpdate();
      //         string search = txtSearch.Text.Trim();


      //         if (!string.IsNullOrEmpty(search))
      //         {
      //             TreeView mytreeview = new TreeView();
      //             if (treeView1.Nodes.Count == 0)
      //             {
      //                 TreeView();
      //             }
      //             for (int i = treeView1.Nodes.Count; i > 0; i--)
      //             {
      //                 NodeFiltering(treeView1.Nodes[i - 1], search);
      //             }
      //             //  treeView1.EndUpdate();
      //         }
      //         else
      //         {
      //             if (string.IsNullOrEmpty(search))
      //             {
      //                 treeView1.Nodes.Clear();
      //                 TreeView();
                   
      //             }
                   
      //         }

                               
      //      }
      //      catch (Exception ex)
      //      {
      //          throw ex;
      //      }

      //  }

        public void sendmail()
        {
            try
            {
                string host, port;
                var MailFrom = this.articleHierarchyManager.GetSiteEmilId(CommonModel.SiteCode);
                var sendE_mail = this.articleHierarchyManager.GetUsernamePassword();
                DataTable dt = ToDataTable(sendE_mail.ToList());
                //var host = sendE_mail.Where(x => x.FldLabel == "SMTP.HOST").Select(o => o.FldValue).ToList();
                //var port = sendE_mail.Where(x => x.FldLabel == "SMTP.IP").Select(o => o.FldValue).ToList();
                //var username1 = sendE_mail.Where(x => x.FldLabel == "SMTP.UserName").Select(o => o.FldValue).ToList();
                //var password1 = sendE_mail.Where(x => x.FldLabel == "SMTP.Password").Select(o => o.FldValue).ToList();

                DataRow[] row = dt.Select("FldLabel='SMTP.HOST'");
                DataRow[] row1 = dt.Select("FldLabel='SMTP.IP'");
                DataRow[] row2 = dt.Select("FldLabel='SMTP.UserName'");
                DataRow[] row3 = dt.Select("FldLabel='SMTP.Password'");

                string Address_To = txtEmail.Text;
                MailMessage mail = new MailMessage(MailFrom, Address_To);
                //  SmtpClient smtp = new SmtpClient("smtp.gmail.com",587);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = row[0]["FldValue"].ToString();
                smtp.Port = Convert.ToInt32(row1[0]["FldValue"]);

                mail.From = new MailAddress(MailFrom);

                string body = "Dear " + txtName.Text + "," + Environment.NewLine.ToString();
                body += "         Your account has been created successfully." + Environment.NewLine.ToString();
                body += "LoginId : " + txtUserName.Text + " " + Environment.NewLine.ToString();
                body += "Password: " + txtPassword.Text + " ";
                //   smtp.Credentials = new System.Net.NetworkCredential("critimail1@gmail.com", "criti@123");
                smtp.Credentials = new System.Net.NetworkCredential(row2[0]["FldValue"].ToString(), row3[0]["FldValue"].ToString());
                
                mail.Body = body;
                mail.IsBodyHtml = false;
                mail.Subject = "Test";

                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, Logger.LogingLevel.Error);
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void frmUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (IsFormValidate())
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string search = txtSearch.Text.Trim();

                    if (String.IsNullOrEmpty(search))
                    {
                        return;
                    };
                    if (!(treeView1.Nodes.Count > 0))
                    {
                        txtSearch.Clear();
                        return;
                    }
                    TreeNode tn = treeView1.SelectedNode;
                    if (tn != null)
                    {
                        this.treeView1.SelectedNode.BackColor = Color.White;
                    }

                    if (LastSearchText != search)
                    {
                        //It's a new Search
                        CurrentNodeMatches.Clear();
                        LastSearchText = search;
                        LastNodeIndex = 0;
                        SearchNodes(search, treeView1.Nodes[0]);
                    }

                    if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
                    {
                        TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                        LastNodeIndex++;
                        SearchNodes(search, treeView1.Nodes[0]);
                        this.treeView1.SelectedNode = selectedNode;
                        this.treeView1.SelectedNode.Expand();
                        this.treeView1.SelectedNode.BackColor = Color.Gray;

                        // this.treeView1.Select();

                    }
                    else
                    {
                        if (CommonFunc.ShowMessage("No such Transaction Exist.", Models.Enums.MessageType.OKCancel) == DialogResult.OK)
                        {
                            txtSearch.Clear();
                        }
                    }

                }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private TreeNode SearchNodes(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };
            return node;
        }

        private void cboRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                string site = txtName.Text;
                txtSearch.Focus();
                //if (cboRole.SelectedValue != "")
                //{
                //ashma 24 may 2018
                if (cboRole.SelectedValue != null)
                {
                    if (cboRole.SelectedIndex != 0)
                    {
                        if (string.IsNullOrEmpty(txtName.Text.Trim()))
                        {
                            //ashma 24 may 2018
                            if (!CommonFunc.SetErrorProvidertoControl(ref errorProvider, ref txtName, "User Name Required"))
                            {
                                this.txtName.Focus();
                                CommonFunc.ShowMessage("User Name Required", MessageType.Information);
                                //ashma 29 may 2018 -- 3270
                                cboRole.SelectedValue = "";
                            }
                        }
                        else
                        {
                            if (treeView1.Nodes.Count > 0)
                            {
                                treeView1.Nodes.Clear();
                            }
                            TreeView();
                        }
                    }
                    else
                    {
                        treeView1.Nodes.Clear();
                    }
                }
                //ashma 24 may 2018
                else
                {
                    treeView1.Nodes.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
