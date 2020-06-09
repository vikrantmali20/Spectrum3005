using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spectrum.Logging;
using Spectrum.Models;
using Spectrum.BL.BusinessInterface;
using Spectrum.BL;
using System.Resources;
using Spectrum.Models.Enums;

namespace Spectrum.BO
{
    public partial class frmItemHierarchyPopup : Spectrum.Controls.RibbonForm
    {
        public frmItemHierarchyPopup()
        {
            InitializeComponent();
            this.commonManager = new CommonManager();
            this.articleHierarchyManager = new ArticleHierarchyManager();
        }

        public static bool ShowCheckBox { get; set; }
        public bool AllowedOnlyLastNodeSelection { get; set; }

        ICommonManager commonManager;
        private IArticleHierarchyManager articleHierarchyManager; 
        IQueryable<ItemHierarchy> itemHierarchyList;
        public List<ItemHierarchy> selectedItemNode { get; set; }
        TreeNode parentNode = null;
        
        private void frmItemHierarchy_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                if (ShowCheckBox)
                {
                    treeView1.CheckBoxes = true;
                }
               this.itemHierarchyList = this.articleHierarchyManager.GetItemHierarchyList();
                //get all nodes which will be our parent nodes
                //commented on 8th dec by ashma
                //if (selectedItemNode != null)
                //{
                //    return;
                //}
                var nodes = this.itemHierarchyList.Where(a => (a.ParentNodecode ?? "") == "").ToList();
              
                treeView1.ImageList = ImageList;
                TreeNode rootNodeNode = new TreeNode();
               
                rootNodeNode.Text = "Root";
                rootNodeNode.Tag = "Root";
                rootNodeNode.SelectedImageIndex = 2;
                foreach (var item in nodes)
                {
                    parentNode = new TreeNode();
                    parentNode.Text = item.NodeName;
                    parentNode.Tag = item.Nodecode;
                    parentNode.SelectedImageIndex = 2;
                    
                    if (selectedItemNode != null)
                    {
                        var result = selectedItemNode.Select(x => x.Nodecode.Equals(item.Nodecode.ToString())).FirstOrDefault();
                        if (result != null && result)
                        {
                            parentNode.Checked = true;
                        }
                    }
                    PopulateTreeView(item.Nodecode, ref parentNode);
                    rootNodeNode.Nodes.Add(parentNode);
                }
                treeView1.Nodes.Add(rootNodeNode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ImageList _imageList;
        public  ImageList ImageList
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

        private void PopulateTreeView(string parentId, ref TreeNode parentNode)
        {
            var childNodes = this.itemHierarchyList.Where(a => a.ParentNodecode == parentId).ToList();
            
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
                        childNode.ImageIndex= 0;
                        childNode.SelectedImageIndex = 2;
                    }
                    else
                    {
                        childNode.SelectedImageIndex = 1;
                        childNode.ImageIndex = 1;
                    }
                    if (selectedItemNode != null)
                    {
                        var result = selectedItemNode.Any(x => x.Nodecode.Equals(item.Nodecode));
                       var result2 = ( from p in  selectedItemNode
                                   where p.Nodecode.Equals(item.Nodecode)
                                   select  p  );

                        if (result != null && result)
                        {
                            childNode.Checked = true;
                        }
                    }
                    PopulateTreeView(item.Nodecode,ref childNode);
                }
                parentNode.Nodes.Add(childNode);
            }
        }

   
        private void btnOK_Click(object sender, EventArgs e)
        {
            selectedItemNode = new List<ItemHierarchy>();
            try
            {
                if (ShowCheckBox)
                {
                    FillCheckedNodes(treeView1.Nodes[0]);
                }
                else
                {
                   selectedItemNode.Add(this.itemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)treeView1.SelectedNode.Tag).ToList().FirstOrDefault());
                   if (selectedItemNode[0] == null)
                   {
                       MessageBox.Show("Please select Items from the Hierarchy");
                       return;
                   }
                    if (AllowedOnlyLastNodeSelection && !(bool)selectedItemNode[0].ISThisLastNode)
                   {
                       CommonFunc.ShowMessage(" Please select the Last Node which has no child nodes associated ", MessageType.Information);
                       return;
                   }
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillCheckedNodes(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Checked)
                {
                   selectedItemNode.Add(this.itemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)tn.Tag).ToList().FirstOrDefault());
                }
                FillCheckedNodes(tn);    
            }
        }


        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            selectedItemNode = new List<ItemHierarchy>();
            try
            {
                selectedItemNode.Add(this.itemHierarchyList.Where(a => (a.Nodecode ?? "") == (string)treeView1.SelectedNode.Tag).ToList().FirstOrDefault());
                if (AllowedOnlyLastNodeSelection && !(bool)selectedItemNode[0].ISThisLastNode)
                {
                    CommonFunc.ShowMessage(" Please select the Last Node which has no child nodes associated ", MessageType.Information);
                    return;
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();     // added by vipin on 30-03-2017
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                bool chk = e.Node.Checked;
                CheckedUncheckedNodes(e.Node, chk);
            }
            catch (Exception ex)
            {
             MessageBox.Show(ex.Message);
             Logger.Log(ex,Logger.LogingLevel.Error);   
            }
          
        }

        private void CheckedUncheckedNodes(TreeNode treeNode, bool chk)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.Checked = chk;
                CheckedUncheckedNodes(tn,chk);
            }
        }

        private void ThemeChange()
        {

            this.BackgroundColor = Color.FromArgb(134, 134, 134);


            btnOK.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnOK.BackColor = Color.Transparent;
            btnOK.BackColor = Color.FromArgb(0, 107, 163);
            btnOK.ForeColor = Color.FromArgb(255, 255, 255);
            btnOK.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

        }              
    }
}
