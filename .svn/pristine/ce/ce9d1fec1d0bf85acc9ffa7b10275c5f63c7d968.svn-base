using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL;
using System.Text.RegularExpressions;
using C1.Win.C1FlexGrid;

namespace Spectrum.BO
{
    public partial class frmItemHierarchy : Spectrum.Controls.RibbonForm
    {
        public frmItemHierarchy()
        {
            InitializeComponent();
            this.commonManager = new CommonManager();
            this.articleHierarchyManager = new ArticleHierarchyManager();
        }

        #region "formVaraibles"
        ICommonManager commonManager;
        private IArticleHierarchyManager articleHierarchyManager;
        IQueryable<ItemHierarchy> ItemHierarchyList;
        public ItemHierarchy SelectedItemNode { get; set; }
        TreeNode _parentNode = null;
        private int SaveMode = 0;
        private Boolean IsLastNode = false;
        private string _editCode = string.Empty;
        private int _previousLevel = 0;
        ImageList _imageList;

        private enum EnumSave
        {
            None = 0,
            Tree,
            Node
        };

        private enum EnumDgDefineLevel
        {
            LevelCode = 0,
            LevelName
        };


        #endregion

        #region " Events"

        private void frmItemHierarchy_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                BindTree();
                HideAllControls();
                setInitialFormsValidations();
                //CommonFunc.WriteResourceFile(this);
                CommonFunc.SetCultureFromResource(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblItemHierarchyDetails.Visible = true;
                // Root 
                // Tree Root
                // Other node

                
                if (treeView1.SelectedNode.Tag == "Root")
                {
                    HideAllControls();
                    lblItemHierarchyDetails.Visible = true;
                    btnNewTree.Visible = true;

                }
                else
                {
                    SelectedItemNode = this.ItemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)treeView1.SelectedNode.Tag).ToList().FirstOrDefault();
                    if (SelectedItemNode.TreeCode == SelectedItemNode.Nodecode && SelectedItemNode.NodeType == (int)EnumSave.Tree)
                    {
                        SaveMode = (int)EnumSave.Tree;
                    }
                    else
                    {
                        SaveMode = (int)EnumSave.Node;
                    }
                    HideAllControls();
                    lblItemHierarchyDetails.Visible = true;
                    tbllayoutPanelItemHierarchy1.Visible = true;
                    panelScreenAction.Visible = true;

                    lbltp1TreeCode.Text = SelectedItemNode.TreeCode;
                    lbltp1TreeName.Text = SelectedItemNode.TreeName;
                    lbltp1LevelName.Text = SelectedItemNode.LevelName;
                    lbltp1ItemCodeName.Text = SelectedItemNode.NodeName;
                    lbltp1NodeCode.Text = SelectedItemNode.Nodecode;
                    lbltp1ParentNodeCode.Text = SelectedItemNode.ParentNodecode;
                    lbltp1NodeLevel.Text = SelectedItemNode.LevelCode;
                    IsLastNode = (bool)SelectedItemNode.ISThisLastNode;
                    if (SaveMode != (int)EnumSave.Tree)
                    {
                        lbltp1ActiveSubNode.Text = this.articleHierarchyManager.GetChildArticleNoCount(SelectedItemNode.Nodecode).ToString();
                        lbltp1ActiveArticles.Text = this.articleHierarchyManager.GetActiveArticlesNoCount(SelectedItemNode.Nodecode).ToString();
                        lbltp1NodeHierarachy.Text = this.articleHierarchyManager.GetArticleHierarchyString(SelectedItemNode.Nodecode, true).ToString();
                        lbltp1NodeHierarachy.UseMnemonic = false;
                    }
                    else
                    {
                        lbltp1ActiveSubNode.Text = "NA";
                        lbltp1ActiveArticles.Text = "NA";
                        lbltp1NodeHierarachy.Text = "NA";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void txtDefineLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_editCode))
                {
                    dgDifineLevel.Rows.Count = 1;
                    for (Int32 index = 1; index <= Convert.ToInt32(txtDefineLevel.Text); index++)
                    {
                        dgDifineLevel.Rows.Add();
                        dgDifineLevel.Rows[index][0] = index;
                    }
                }
                else
                {
                    if (_previousLevel <= Convert.ToInt32(txtDefineLevel.Text))
                    {
                        dgDifineLevel.Rows.Count = _previousLevel + 1;
                        for (Int32 index = _previousLevel + 1; index <= Convert.ToInt32(txtDefineLevel.Text); index++)
                        {
                            dgDifineLevel.Rows.Add();
                            dgDifineLevel.Rows[index][0] = index;
                        }
                    }
                    else
                    {
                        txtDefineLevel.Text = _previousLevel.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void dgDifineLevel_SetupEditor(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == 1)
                {
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)dgDifineLevel.Editor;
                    textBox.MaxLength = 15;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void btnNewTree_Click(object sender, EventArgs e)
        {
            try
            {
                dgDifineLevel.Clear();
                flContainer.Top = btnNewTree.Bottom + 10;
                flContainer.Visible = true;
                flowLtDefinesLevels.Visible = true;
                panelDbAction.Visible = true;

                lblFlTreeCode.Text = "ATC"
                                    + CommonModel.SiteCode.ToString().Substring(CommonModel.SiteCode.ToString().Length - 3, 3)
                                    + string.Format("{0}", this.commonManager.GetTranNextNo("AT").ToString().PadLeft(9, '0'));

                SaveMode = (int)EnumSave.Tree;
                txtDefineLevel.Text = "0";
                txtFlTreeName.Text = string.Empty;
                dgDifineLevel.Cols.Count = 0;
                dgDifineLevel.Rows.Count = 1;


                // create a column, assign it a name and get the new index 
                Column LevelCode = dgDifineLevel.Cols.Add();
                LevelCode.Name = "LevelCode";
                LevelCode.DataType = typeof(int);
                LevelCode.Caption = "Level Code";
                LevelCode.AllowEditing = false;

                Column LevelName = dgDifineLevel.Cols.Add();
                LevelName.Name = "LevelName";
                LevelName.DataType = typeof(string);
                LevelName.Caption = "Level Name";

                txtFlTreeName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }


        }

        private void btnAddItemHierarchy_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddArticleNode())
                {
                    flContainer.Visible = true;
                    flContainer.Top = panelScreenAction.Bottom + 10;
                    lblItemHierarchyDetails2.Text = "Item Hierarchy Details(Add)";
                    tbllayoutPanelItemHierarchy2.Visible = true;
                    flowLtDefinesLevels.Visible = false;
                    panelDbAction.Visible = true;
                    _editCode = string.Empty;
                    SaveMode = (int)EnumSave.Node;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void btnEditItemHierarchy_Click(object sender, EventArgs e)
        {
            try
            {
                flContainer.Visible = true;
                SelectedItemNode =
                    this.ItemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)treeView1.SelectedNode.Tag)
                        .ToList()
                        .FirstOrDefault();

                if (SelectedItemNode.TreeCode == SelectedItemNode.Nodecode &&
                    SelectedItemNode.NodeType == (int)EnumSave.Tree)
                {
                    SaveMode = (int)EnumSave.Tree;
                }
                else
                {
                    SaveMode = (int)EnumSave.Node;
                }

                switch (SaveMode)
                {
                    case (int)EnumSave.Tree:
                        lblItemHierarchyDetails2.Text = "Item Hierarchy Details(Edit)";
                        lblItemHierarchyDetails2.Visible = true;
                        flowLtDefinesLevels.Visible = true;
                        tbllayoutPanelItemHierarchy2.Visible = false;
                        panelDbAction.Visible = true;
                        FillTree();
                        flContainer.Top = panelScreenAction.Bottom + 10;
                        break;
                    case (int)EnumSave.Node:
                        lblItemHierarchyDetails2.Text = "Item Hierarchy Details(Edit)";
                        lblItemHierarchyDetails2.Visible = true;
                        tbllayoutPanelItemHierarchy2.Visible = true;
                        panelDbAction.Visible = true;
                        FillNode();
                        flContainer.Top = panelScreenAction.Bottom + 10;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void btnCancelItemHierarchy_Click(object sender, EventArgs e)
        {
            try
            {
                switch (SaveMode)
                {
                    case (int)EnumSave.Tree:

                        if (MessageBox.Show(CommonFunc.getResourceString("IH001") , "CNF",
                                MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        }
                        HideAllControls();
                        lblItemHierarchyDetails.Visible = true;
                        btnNewTree.Visible = true;
                        break;

                    case (int)EnumSave.Node:
                        if (MessageBox.Show(CommonFunc.getResourceString("IH001"), "CNF",
                              MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        }
                        lblItemHierarchyDetails2.Visible = false;
                        tbllayoutPanelItemHierarchy2.Visible = false;
                        panelDbAction.Visible = false;
                        SaveMode = (int)EnumSave.None;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void btnDeleteItemHierarchy_Click(object sender, EventArgs e)
        {
            try
            {
                // check how Many active child node if not then can be Delete .
                switch (SaveMode)
                {
                    case (int)EnumSave.Tree:
                        int treeChildArticleNoCount = this.articleHierarchyManager.GetTreeChildArticleNoCount(lbltp1TreeCode.Text);
                        if (treeChildArticleNoCount == 0)
                        {
                            if (this.articleHierarchyManager.DeleteTree(lbltp1NodeCode.Text))
                            {
                                MessageBox.Show(CommonFunc.getResourceString("IH002"));
                                BindTree();
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0}{1}",CommonFunc.getResourceString("IH003") , treeChildArticleNoCount)) ;
                        }
                        break;

                    case (int)EnumSave.Node:
                        int childArticleNoCount = this.articleHierarchyManager.GetChildArticleNoCount(lbltp1NodeCode.Text);
                        if (childArticleNoCount == 0)
                        {
                            if (this.articleHierarchyManager.DeleteNode(lbltp1NodeCode.Text))
                            {
                                MessageBox.Show(CommonFunc.getResourceString("IH004"));
                                BindTree();
                                HideAllControls();
                            }
                        }
                        else
                        {
                            string message = string.Format(CommonFunc.getResourceString("IH005"), childArticleNoCount);
                            MessageBox.Show(message);
                        }

                        break;
                }
                TreeNode node = treeView1.Nodes[0]; // vipin
                treeView1.SelectedNode = node;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }


        #endregion

        #region " funtions"

        /// <summary>
        /// apply Form level default validation accroding to SRS
        /// </summary>
        private void setInitialFormsValidations()
        {
            try
            {
                //Tree Name Max length 18 characters.
                txtFlTreeName.MaxLength = 18;
                //Node Name* Alphanumeric text box. At least 1 char and max 15 characters. 
                txttp2NodeName.MaxLength = 18;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HideAllControls()
        {
            flContainer.Visible = false;
            tbllayoutPanelItemHierarchy1.Visible = false;
            tbllayoutPanelItemHierarchy2.Visible = false;
            flowLtDefinesLevels.Visible = false;
            lblItemHierarchyDetails.Visible = false;
            lblItemHierarchyDetails2.Visible = false;
            panelScreenAction.Visible = false;
            panelDbAction.Visible = false;
            btnNewTree.Visible = false;
        }

        private void BindTree()
        {
            treeView1.Nodes.Clear();
            this.ItemHierarchyList = this.articleHierarchyManager.GetItemHierarchyList();
            //get all nodes which will be our parent nodes
            var nodes = this.ItemHierarchyList.Where(a => (a.ParentNodecode ?? "") == "").ToList();
            treeView1.ImageList = ImageList;

            TreeNode rootNodeNode = new TreeNode();
            rootNodeNode.Text = "Root";
            rootNodeNode.Tag = "Root";
            rootNodeNode.SelectedImageIndex = 2;
            foreach (var item in nodes)
            {
                _parentNode = new TreeNode();
                _parentNode.Text = item.NodeName;
                _parentNode.Tag = item.Nodecode;
                _parentNode.SelectedImageIndex = 2;

                PopulateTreeView(item.Nodecode, ref _parentNode);
                rootNodeNode.Nodes.Add(_parentNode);
            }
            treeView1.Nodes.Add(rootNodeNode);
            //treeView1.ExpandAll(); 
        }

        private void PopulateTreeView(string parentId, ref TreeNode parentNode)
        {
            var childNodes = this.ItemHierarchyList.Where(a => a.ParentNodecode == parentId).ToList();

            foreach (var item in childNodes)
            {
                TreeNode childNode = new TreeNode();
                if (parentNode == null)
                {
                    return;
                }
                else
                {
                    childNode.Text = item.NodeName;
                    childNode.Tag = item.Nodecode;
                    if (!item.ISThisLastNode == true)
                    {
                        childNode.ImageIndex = 0;
                        childNode.SelectedImageIndex = 2;
                    }
                    else
                    {
                        childNode.SelectedImageIndex = 1;
                        childNode.ImageIndex = 1;
                    }
                    PopulateTreeView(item.Nodecode, ref childNode);
                }
                parentNode.Nodes.Add(childNode);
            }
        }
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
                    //_imageList.Images.Add(Properties.Resources.treeColapse);
                    //_imageList.Images.Add(Properties.Resources.treeExpand);
                }
                return _imageList;
            }
        }

        public bool AddArticleNode()
        {
            chkIsLastNode.Enabled = true;
            chkIsLastNode.Checked = false;
            lbltp2TreeCode.Text = lbltp1TreeCode.Text;
            lbltp2TreeName.Text = lbltp1TreeName.Text;
            lbltp2NodeCode.Text = "ANC"
                                  +
                                  CommonModel.SiteCode.ToString()
                                      .Substring(CommonModel.SiteCode.ToString().Length - 3, 3)
                                  +
                                  string.Format("{0}", this.commonManager.GetTranNextNo("AN").ToString().PadLeft(9, '0'));


            SelectedItemNode = this.ItemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)treeView1.SelectedNode.Tag).ToList().FirstOrDefault();

            int maxLevel = articleHierarchyManager.GetMaxTreeLevelCode(lbltp1TreeCode.Text);
            if (!string.IsNullOrEmpty(lbltp1TreeCode.Text))
            {
                string levelCode = "1";
                if (maxLevel == int.Parse(lbltp1NodeLevel.Text))
                {
                    IsLastNode = true;
                    string message = CommonFunc.getResourceString("IH013"); // SpectrumResources.GetString("IH013");
                    MessageBox.Show(message);
                    return false;
                }
                else
                {
                    if ((bool)SelectedItemNode.ISThisLastNode)
                    {
                        MessageBox.Show(CommonFunc.getResourceString("IH006"));
                        return false;
                    }
                    else
                    {
                        int levelcode = int.Parse(lbltp1NodeLevel.Text);
                        int newLevel = levelcode + 1;
                        if (newLevel <= maxLevel)
                        {
                            if (newLevel == maxLevel)
                            {
                                IsLastNode = true;
                                chkIsLastNode.Checked = true;
                                chkIsLastNode.Enabled = false;
                            }
                            lbltp2LevelCode.Text = newLevel.ToString();
                            lbltp2LevelName.Text = articleHierarchyManager.GetTreeLevelName(lbltp1TreeCode.Text, newLevel);
                        }
                        //else
                        //{
                        //    MessageBox.Show(" Maximum Number of levels defined have been reached. You cannot add another level at this node ");
                        //    return false;
                        //}

                    }
                }
            }
            return true;
        }

        private void FillTree()
        {
            lblFlTreeCode.Text = lbltp1TreeCode.Text;
            txtFlTreeName.Text = lbltp1TreeName.Text;
            _editCode = lblFlTreeCode.Text;
            IList<ArticleTreeLevelModel> treeLevel = articleHierarchyManager.GetTreeLevels(lblFlTreeCode.Text);

            txtDefineLevel.Text = treeLevel.Count().ToString();
            _previousLevel = Convert.ToInt32(txtDefineLevel.Text);
            dgDifineLevel.Rows.Count = 1;
            for (int rowIndex = 1; rowIndex <= treeLevel.Count(); rowIndex++)
            {
                dgDifineLevel.Rows.Add();
                dgDifineLevel.Rows[rowIndex][(int)EnumDgDefineLevel.LevelCode] = treeLevel[rowIndex - 1].LevelCode;
                dgDifineLevel.Rows[rowIndex][(int)EnumDgDefineLevel.LevelName] = treeLevel[rowIndex - 1].LevelName;
            }
        }

        private void FillNode()
        {
            string nodeCode = lbltp1NodeCode.Text;
            SelectedItemNode = this.ItemHierarchyList.Where(a => (a.Nodecode ?? "") == nodeCode).ToList().FirstOrDefault();
            lbltp2TreeCode.Text = SelectedItemNode.TreeCode;
            lbltp2TreeName.Text = SelectedItemNode.TreeName;
            txttp2NodeName.Text = SelectedItemNode.NodeName;
            lbltp2LevelName.Text = SelectedItemNode.LevelName;
            lbltp2LevelCode.Text = SelectedItemNode.LevelCode;
            lbltp2NodeCode.Text = nodeCode;
            chkIsLastNode.Enabled = true;
            if (SelectedItemNode.ISThisLastNode != null && (bool)SelectedItemNode.ISThisLastNode)
            {
                chkIsLastNode.Checked = true;
                IsLastNode = true;
                int maxLevel = articleHierarchyManager.GetMaxTreeLevelCode(lbltp1TreeCode.Text);
                if (maxLevel == int.Parse(lbltp1NodeLevel.Text))
                {
                    chkIsLastNode.Enabled = false;
                }
            }
            else
            {
                SelectedItemNode = this.ItemHierarchyList.Where(a => (a.ParentNodecode ?? "") == nodeCode).ToList().FirstOrDefault();
                if (SelectedItemNode != null)
                {
                    chkIsLastNode.Checked = false;
                    chkIsLastNode.Enabled = false;
                }
            }
            _editCode = nodeCode;

        }

        private void btnSaveItemHierarchy_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsformValidateSuccess = false;
                switch (SaveMode)
                {
                    case (int)EnumSave.Tree:
                        if (IsFormvalidate())
                        {
                            string autoCode = string.Empty;
                            // Save Tree ...
                            ArticleTreeModel atm = new ArticleTreeModel();
                            List<ArticleTreeLevelModel> atlm = new List<ArticleTreeLevelModel>();
                            FillTreeModel(ref atm, ref atlm);
                            if ((string.IsNullOrEmpty(_editCode))
                                ? this.articleHierarchyManager.SaveTree(atm, atlm, ref autoCode)
                                : this.articleHierarchyManager.UpdateTree(atm, atlm))
                            {
                                StringBuilder message = new StringBuilder();
                                message.Length = 0;
                                if (!string.IsNullOrEmpty(autoCode)) message.Append("Tree code" + autoCode);
                                message.Append(CommonFunc.getResourceString("IH007"));
                                MessageBox.Show(message.ToString());
                                ClearTree();
                                BindTree();
                                treeView1.ExpandAll();
                            }
                            IsformValidateSuccess = true;
                        }
                        break;
                    case (int)EnumSave.Node:
                        if (IsFormvalidate())
                        {
                            string autoCode = string.Empty;
                            // Save Node ...
                            ArticleNodeModel anmModel = new ArticleNodeModel();
                            ArticleTreeNodeMapModel atnmModel = new ArticleTreeNodeMapModel();

                            FillNodeModel(ref anmModel, ref atnmModel);
                            if ((string.IsNullOrEmpty(_editCode))
                                ? this.articleHierarchyManager.SaveNode(anmModel, atnmModel, ref autoCode)
                                : this.articleHierarchyManager.UpdateNode(anmModel))
                            {
                                StringBuilder message = new StringBuilder();
                                message.Length = 0;
                                if (!string.IsNullOrEmpty(autoCode)) message.Append("Node Code " + autoCode + " ");
                                message.Append(CommonFunc.getResourceString("IH008"));
                                MessageBox.Show(message.ToString());
                                ClearNode();
                                BindTree();
                            }
                            IsformValidateSuccess = true;
                        }
                        break;
                }
                if (IsformValidateSuccess )
                {
                    HideAllControls();    
                }
                

            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                MessageBox.Show(ex.Message);
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
                Regex alphaNumericPatt = new Regex("^[A-Za-z0-9- ]+$");
                bool validateResult = true;
                bool isControlFocused = true;

                if ((int)EnumSave.Tree == SaveMode)
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref epArticleHierarchy, ref txtFlTreeName, "Tree Name value required"))
                    {
                        this.txtFlTreeName.Focus();
                        validateResult = false;
                        isControlFocused = false;
                    }
                    // Tree Name Mandatory. Alphanumeric Textbox. Accepts space but no special characters. 
                    
                    if (alphaNumericPatt.IsMatch(txtFlTreeName.Text) == false)
                    {
                        validateResult = false;
                        if (!CommonFunc.SetErrorProvidertoControl(ref epArticleHierarchy, ref txtFlTreeName, " Tree Name is Alphanumeric Textbox. Accepts space but no special characters.", true))
                        {
                            this.txtFlTreeName.Focus();
                        }
                    }
                    else
                    {
                        epArticleHierarchy.SetError(txtFlTreeName, string.Empty);
                        txtFlTreeName.BorderColor = CommonFunc.DefaultBorderColor;
                    }
                    if (!CommonFunc.SetErrorProvidertoControl(ref epArticleHierarchy, ref txtDefineLevel, "Level value required"))
                    {
                        if (isControlFocused)
                            this.txtDefineLevel.Focus();
                        validateResult = false;
                        isControlFocused = false;
                    }
                    // Check Level Names 

                    for (int rowIndex = 1; rowIndex < dgDifineLevel.Rows.Count; rowIndex++)
                    {
                        if (string.IsNullOrEmpty(dgDifineLevel.Rows[rowIndex][1] as string))
                        {
                            MessageBox.Show(CommonFunc.getResourceString("IH012"));
                            this.dgDifineLevel.Focus();
                            validateResult = false;
                            isControlFocused = false;
                            goto gotLevelResult;
                        }
                        else
                        {
                            if (alphaNumericPatt.IsMatch(dgDifineLevel.Rows[rowIndex][1] as string) == false)
	                        {
                                this.dgDifineLevel.Focus();
                                validateResult = false;
                                MessageBox.Show("Tree level at Level Code- " + rowIndex + " is Alphanumeric Textbox. Accepts space but no special characters.");
                                isControlFocused = false;
                                goto gotLevelResult;
	                        } 
                        }
                    }

                }
                else
                {
                    if (!CommonFunc.SetErrorProvidertoControl(ref epArticleHierarchy, ref txttp2NodeName, "Set Tree Name"))
                    {
                        this.txttp2NodeName.Focus();
                        validateResult = false;
                        isControlFocused = false;
                    }
                }
            gotLevelResult:
                return validateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FillTreeModel(ref ArticleTreeModel atm, ref List<ArticleTreeLevelModel> atlm)
        {
            atm.TreeCode = lblFlTreeCode.Text;
            atm.TreeName = txtFlTreeName.Text;
            atm.Status = true;
            for (int rowIndex = 1; rowIndex <= dgDifineLevel.Rows.Count - 1; rowIndex++)
            {
                ArticleTreeLevelModel rowAtlm = new ArticleTreeLevelModel();
                rowAtlm.TreeCode = lblFlTreeCode.Text;
                rowAtlm.LevelCode = dgDifineLevel.Rows[rowIndex][(int)EnumDgDefineLevel.LevelCode].ToString();
                rowAtlm.LevelName = dgDifineLevel.Rows[rowIndex][(int)EnumDgDefineLevel.LevelName].ToString();
                rowAtlm.Status = true;
                atlm.Add(rowAtlm);
            }

        }

        private void FillNodeModel(ref ArticleNodeModel alm, ref ArticleTreeNodeMapModel atnmModel)
        {
            alm.Nodecode = lbltp2NodeCode.Text;
            alm.NodeName = txttp2NodeName.Text;
            alm.ISThisLastNode = chkIsLastNode.Checked;
            alm.LevelCode = lbltp2LevelCode.Text;
            alm.Treecode = lbltp2TreeCode.Text;

            atnmModel.Nodecode = lbltp2NodeCode.Text;
            atnmModel.ParentNodecode = lbltp1NodeCode.Text;
            atnmModel.Treecode = lbltp2TreeCode.Text;
            atnmModel.ToleranceValue = null;


        }


        private void ClearTree()
        {
            txtFlTreeName.Text = string.Empty;
            txtDefineLevel.Text = "0";
            dgDifineLevel.Rows.Count = 1;
            _editCode = string.Empty;
            _previousLevel = 0;
            lblFlTreeCode.Text = "ATC"
                 + CommonModel.SiteCode.ToString().Substring(CommonModel.SiteCode.ToString().Length - 3, 3)
                 + string.Format("{0}", this.commonManager.GetTranNextNo("AT").ToString().PadLeft(9, '0'));
        }

        private void ClearNode()
        {
            txttp2NodeName.Text = string.Empty;
            chkIsLastNode.Checked = false;
            chkIsLastNode.Enabled = true;
            lblItemHierarchyDetails2.Visible = false;
            tbllayoutPanelItemHierarchy2.Visible = false;
            panelDbAction.Visible = false;
            _editCode = string.Empty;
        }

        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);

            btnAddItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnAddItemHierarchy.BackColor = Color.Transparent;
            btnAddItemHierarchy.BackColor = Color.FromArgb(0, 107, 163);
            btnAddItemHierarchy.ForeColor = Color.FromArgb(255, 255, 255);
            btnAddItemHierarchy.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnAddItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnAddItemHierarchy.FlatAppearance.BorderSize = 0;
            btnAddItemHierarchy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnCancelItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancelItemHierarchy.BackColor = Color.Transparent;
            btnCancelItemHierarchy.BackColor = Color.FromArgb(0, 107, 163);
            btnCancelItemHierarchy.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancelItemHierarchy.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancelItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancelItemHierarchy.FlatAppearance.BorderSize = 0;
            btnCancelItemHierarchy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            btnEditItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnEditItemHierarchy.BackColor = Color.Transparent;
            btnEditItemHierarchy.BackColor = Color.FromArgb(0, 107, 163);
            btnEditItemHierarchy.ForeColor = Color.FromArgb(255, 255, 255);
            btnEditItemHierarchy.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnEditItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnEditItemHierarchy.FlatAppearance.BorderSize = 0;
            btnEditItemHierarchy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnNewTree.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnNewTree.BackColor = Color.Transparent;
            btnNewTree.BackColor = Color.FromArgb(0, 107, 163);
            btnNewTree.ForeColor = Color.FromArgb(255, 255, 255);
            btnNewTree.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnNewTree.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnNewTree.FlatAppearance.BorderSize = 0;
            btnNewTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;



            btnDeleteItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnDeleteItemHierarchy.BackColor = Color.Transparent;
            btnDeleteItemHierarchy.BackColor = Color.FromArgb(0, 107, 163);
            btnDeleteItemHierarchy.ForeColor = Color.FromArgb(255, 255, 255);
            btnDeleteItemHierarchy.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnDeleteItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnDeleteItemHierarchy.FlatAppearance.BorderSize = 0;
            btnDeleteItemHierarchy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSaveItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSaveItemHierarchy.BackColor = Color.Transparent;
            btnSaveItemHierarchy.BackColor = Color.FromArgb(0, 107, 163);
            btnSaveItemHierarchy.ForeColor = Color.FromArgb(255, 255, 255);
            btnSaveItemHierarchy.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSaveItemHierarchy.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSaveItemHierarchy.FlatAppearance.BorderSize = 0;
            btnSaveItemHierarchy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            lblDefinesLevel.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1NodeHierachy.ForeColor = Color.FromArgb(255, 255, 255);
            lblFlTreeCode.ForeColor = Color.FromArgb(255, 255, 255);
            //lblItemHierarchyDetails.ForeColor = Color.FromArgb(255, 255, 255);
            lblItemHierarchyDetails2.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1ActiveArticle.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1ActiveSubNodes.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1ItemNodeName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1LevelName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1NodeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1NodeLevel.ForeColor = Color.FromArgb(255, 255, 255);
            lblt1TreeCode.ForeColor = Color.FromArgb(255, 255, 255);

            lblt1TreeName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2IsLastNode.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2ItemNodeName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2LevelCode.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2LevelName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2NodeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lblt2TreeCode.ForeColor = Color.FromArgb(255, 255, 255);


            lblt2TreeName.ForeColor = Color.FromArgb(255, 255, 255);
            lblt3TreeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lblt3TreeName.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1ActiveArticles.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1ActiveSubNode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1ItemCodeName.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1LevelName.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1NodeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1NodeHierarachy.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1NodeLevel.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1ParentNodeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1TreeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp1TreeName.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp2LevelCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp2LevelName.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp2NodeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp2TreeCode.ForeColor = Color.FromArgb(255, 255, 255);
            lbltp2TreeName.ForeColor = Color.FromArgb(255, 255, 255);

            dgDifineLevel.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            dgDifineLevel.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            dgDifineLevel.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            dgDifineLevel.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            dgDifineLevel.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgDifineLevel.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgDifineLevel.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgDifineLevel.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            dgDifineLevel.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            dgDifineLevel.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            dgDifineLevel.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
        }
        #endregion

    }
}
