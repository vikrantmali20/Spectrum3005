using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Spectrum.Models;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmCommonSearch : Spectrum.Controls.DialogRibbonForm  
    {
        public frmCommonSearch(bool multipleSelect= false )
        {
            InitializeComponent();
            boolmultipleSelect = multipleSelect;
            gridData.AllowFiltering = true;
        }

        public object DataList { get; set; }
        public List <object> SelectedRows { get; set; }
        public bool boolmultipleSelect;
        enum enumcommonsearch
        {
            select,
            itemcode,
            itemdesc,
            UOM,
            quantity,
            cost,
            tax,
            netamount

        }
        //IList<ArticlePurchaseModel> articlelist;
       
        private void frmCommonSearch_Load(object sender, System.EventArgs e)
        {
            if (CommonFunc.Themeselect == "Theme 1")
            {
                ThemeChange();
            }
            gridData.DataSource = DataList;
            gridData.AutoSizeCols();
            //for (int i = 0; i < gridData.Cols.Count - 1; i++)
            //{
            //    gridData.Cols.Insert(i);
            //    return;
            //}
            //    gridData.Splits[0].DisplayColumns[i].AutoSize();
        }
       
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                SelectedRows = new List<object>();
                if (boolmultipleSelect)
                {
                    for (int rowBarCode = 1; rowBarCode < gridData.Rows.Count; rowBarCode++)
                    {
                        if (gridData.Rows[rowBarCode][(int)enumcommonsearch.select] != null && Convert.ToBoolean(gridData.Rows[rowBarCode][(int)enumcommonsearch.select]) == true)
                        {
                            SelectedRows.Add(gridData.Rows[rowBarCode].DataSource);
                            //SelectedRows.
                        }
                    }
                }
                else
                {
                    SelectedRows.Add(gridData.Rows[gridData.Row].DataSource);
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        private void gridData_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                //SelectedRows = gridData.Rows[gridData.Row].DataSource;
                //DialogResult = System.Windows.Forms.DialogResult.OK;
                SelectedRows = new List<object>();
                boolmultipleSelect = false;
                if (boolmultipleSelect)
                {
                    for (int rowBarCode = 1; rowBarCode < gridData.Rows.Count; rowBarCode++)
                    {
                        if (gridData.Rows[rowBarCode][(int)enumcommonsearch.select] != null && Convert.ToBoolean(gridData.Rows[rowBarCode][(int)enumcommonsearch.select]) == true)
                        {
                            SelectedRows.Add(gridData.Rows[rowBarCode].DataSource);
                        }
                    }
                }
                else
                {
                    SelectedRows.Add(gridData.Rows[gridData.Row].DataSource);
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
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);

            gridData.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            gridData.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            gridData.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            gridData.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            gridData.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridData.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridData.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridData.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridData.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            gridData.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            gridData.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);

            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnOK.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnOK.BackColor = Color.Transparent;
            btnOK.BackColor = Color.FromArgb(0, 107, 163);
            btnOK.ForeColor = Color.FromArgb(255, 255, 255);
            btnOK.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }
     
    }
}
