using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace Spectrum.BO
{
    public partial class frmCommonSearchTrueGrid : Spectrum.Controls.RibbonForm
    {
        bool setFilter = true;
        bool IsHEeaderClick = false;
        public frmCommonSearchTrueGrid(bool multipleSelect = false,bool defaultFilter=true )//ref DataTable dtSearch ,
        {
            InitializeComponent();
            if (CommonFunc.Themeselect == "Theme 1")
            {
                ThemeChange();
            }
            boolmultipleSelect = multipleSelect;
            setFilter = defaultFilter;
            //dtcommonSearch = dtSearch;
             
               
        }

        public object DataList { get; set; }
        public DataTable dtcommonSearch { get; set; }
        public DataTable dtSelectedList { get; set; }
        public List<object> SelectedRows { get; set; }
        public bool boolmultipleSelect;

        public bool boolWildSearch { get; set; }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            mainPanel.BackColor = Color.FromArgb(134, 134, 134);
            chkSelectAll.BackColor = Color.FromArgb(212, 212, 212);

            gridData.Styles[0].BackColor = Color.FromArgb(255, 255, 255);
            gridData.Styles[0].Font = new Font("Neo Sans", 10, FontStyle.Regular);
            gridData.Styles[1].Font = new Font("Neo Sans", 10, FontStyle.Bold);
            gridData.Styles[1].BackColor = Color.FromArgb(177, 227, 253);
            gridData.Styles[1].ForeColor = Color.Black;
            gridData.Splits[0].Style.Font = new Font("Neo Sans", 9, FontStyle.Regular);
            gridData.Styles[5].Font = new Font("Neo Sans", 9, FontStyle.Bold);
            gridData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom;
            gridData.HighLightRowStyle.BackColor = Color.LightBlue;
            gridData.HighLightRowStyle.ForeColor = Color.Black;

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


            btnWildSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnWildSearch.BackColor = Color.Transparent;
            btnWildSearch.BackColor = Color.FromArgb(0, 107, 163);
            btnWildSearch.ForeColor = Color.FromArgb(255, 255, 255);
            btnWildSearch.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnWildSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnWildSearch.FlatAppearance.BorderSize = 0;
            btnWildSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }

        private void frmCommonSearch2_Load(object sender, EventArgs e)
        {
            try
            {
                if (boolWildSearch == false)
                {
                    txtwildsearch.Visible = false;
                   

                }
                btnWildSearch.Visible = false;
                chkSelectAll.Visible = boolmultipleSelect;

                //gridData.DataSource = DataList;
                gridData.DataSource = dtcommonSearch;                
                gridSettings();
                txtwildsearch.Focus();
                txtwildsearch.Select();
                
                //this.gridData.Select();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gridSettings()
        {
            gridData.ColumnHeaders = false;
            foreach (C1.Win.C1TrueDBGrid.C1DisplayColumn column in gridData.Splits[0].DisplayColumns)
            {
                //column.AutoSize();
                column.Width = 150;

            }
            gridData.AllowFilter = setFilter;
            gridData.FilterBar = setFilter;
            gridData.FilterActive = setFilter;
            gridData.EditActive = setFilter;
           
            // this.gridData.Splits[0].DisplayColumns[0].FetchStyle = true;
            // Lock columns except first column in case multiple select is allow 
            for (int colIndex = 0; colIndex < this.gridData.Splits[0].DisplayColumns.Count ; colIndex++)
            {
                if (!(colIndex == 0 && boolmultipleSelect))
                {
                    this.gridData.Splits[0].DisplayColumns[colIndex].Locked = true;
                }
                if ((colIndex == this.gridData.Splits[0].DisplayColumns.Count -1) && boolWildSearch)
                {
                    this.gridData.Splits[0].DisplayColumns[colIndex].Visible = false;
                }

            }
                        
            gridData.ColumnHeaders = true;
            this.gridData.ExtendRightColumn = true;
           // this.gridData.Select();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //dtSelectedList = dtcommonSearch.Clone();
                SelectedRows = new List<object>();
                if (boolmultipleSelect)
                {

                    var a = (from selected in dtcommonSearch.AsEnumerable()
                             where selected.Field<bool>("Select") == true
                             select selected).AsDataView();

                    IEnumerable<DataRow> query = from selected in dtcommonSearch.AsEnumerable()
                                                 where selected.Field<bool>("Select") == true
                                                 select selected;
                    
                    // Create a table from the query.

                    //DataView isRowSelected = new DataView();
                    //isRowSelected = dtcommonSearch.DefaultView;
                    //isRowSelected.RowFilter = "Select = True";
                    //if (isRowSelected.Count > 0)
                    //{
                    //    dtSelectedList = query.CopyToDataTable<DataRow>();
                    //}
                    if (a.Count > 0)
                    {
                        dtSelectedList = query.CopyToDataTable<DataRow>();
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        CommonFunc.ShowMessage("Please Select Article", MessageType.Information);
                        this.DialogResult = DialogResult.None;
                    }
                    //for (int Rowindex = 0; Rowindex < gridData.RowCount - 1; Rowindex++)
                    //{
                    //    if (gridData[Rowindex, 0] != null && Convert.ToBoolean(gridData[Rowindex, 0]) == true)
                    //    {
                    //        //SelectedRows.Add(gridData[Rowindex]);
                    //        //SelectedRows.Add(gridData.Rows[rowBarCode].DataSource);

                    //        var desRow = dtSelectedList.NewRow();
                    //        var sourceRow = ((DataRowView)(gridData[Rowindex])).Row;
                    //        desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    //        dtSelectedList.Rows.Add(desRow.ItemArray);
                    //    }
                    //}

                }
                else
                {
                    dtSelectedList = dtcommonSearch.Clone();
                    var desRow = dtSelectedList.NewRow();
                    var sourceRow = ((DataRowView)(gridData[gridData.Row])).Row;
                    desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    dtSelectedList.Rows.Add(desRow.ItemArray);

                    //SelectedRows.Add(gridData[gridData.Row]);
                    //SelectedRows.Add(dtcommonSearch.Rows[gridData.Row]);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gridData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsHEeaderClick != true)
                {
                dtSelectedList = dtcommonSearch.Clone();
                SelectedRows = new List<object>();
                if (boolmultipleSelect)
                {
                    //for (int Rowindex = 0; Rowindex < gridData.RowCount - 1; Rowindex++)
                    //{
                    //    if (gridData[Rowindex, 0] != null && Convert.ToBoolean(gridData[Rowindex, 0]) == true)
                    //    {
                    //        //SelectedRows.Add(gridData[Rowindex]);
                    //        //SelectedRows.Add(gridData.Rows[rowBarCode].DataSource);
                    //        var desRow = dtSelectedList.NewRow();
                    //        var sourceRow = dtcommonSearch.Rows[Rowindex];
                    //        desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    //        dtSelectedList.Rows.Add(desRow.ItemArray);
                    //    }
                    //}

                }
                else
                {
                    var desRow = dtSelectedList.NewRow();
                    var sourceRow = ((DataRowView)(gridData[gridData.Row])).Row;
                    desRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    dtSelectedList.Rows.Add(desRow.ItemArray);

                    //SelectedRows.Add(gridData[gridData.Row]);
                    //SelectedRows.Add(dtcommonSearch.Rows[gridData.Row]);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }

                }
            }
     
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnWildSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string rowfilterString = string.Empty;
                System.Text.StringBuilder filterString = new System.Text.StringBuilder();

                if ((!string.IsNullOrEmpty(txtwildsearch.Text.Trim())))
                {
                    for (int colIndex = 0; colIndex <= gridData.Columns.Count - 1; colIndex += 1)
                    {
                        //   if (columnsList.Contains(gridData.Columns[colIndex].DataType == typeof("String") ))
                        // {
                        //           filterString.AppendFormat("{0} LIKE '%{1}%' OR ", gridData.Columns[colIndex].Caption, txtwildsearch.Text.Trim());
                        ////  }
                        //else
                        //  {
                        filterString.AppendFormat("Convert({0}, 'System.String') LIKE '%{1}%' OR ", gridData.Columns[colIndex].Caption, txtwildsearch.Text.Trim());
                        //  }
                    }
                }
                rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 3);
                dtcommonSearch.DefaultView.RowFilter = rowfilterString;
                gridData.DataSource = dtcommonSearch.DefaultView;
                txtwildsearch.Value = string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chkSelectAll.Checked == true)
                if (boolmultipleSelect)
                {
                    DataView selArticleDataview = new DataView();
                    selArticleDataview = dtcommonSearch.DefaultView;
                    for (int rowIndex = 0; rowIndex < selArticleDataview.Count; rowIndex++)
                    {
                        // gridData.Rows[rowIndex][0] = chkSelectAll.Checked;
                        dtcommonSearch.Rows[rowIndex][0] = chkSelectAll.Checked;
                        
                    }
                    gridData.Refresh();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Wild Search"

        private void txtwildsearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnWildSearch_Click(btnWildSearch ,new System.EventArgs);
            //}
        }

        private void txtwildsearch_TextChanged(object sender, EventArgs e)
        {
            //if (txtwildsearch.Text.ToString().Length > 1)
            //{
                if (dtcommonSearch != null && dtcommonSearch.Rows.Count > 0)
                {
                    if (boolWildSearch)
                    {
                        FilterSearchRecords();
                    }
                    else
                    {
                        btnWildSearch_Click(btnWildSearch, null);
                    }
                }
            //    txtwildsearch.Select();
            //    txtwildsearch.Select(txtwildsearch.Text.Length, 0);
            //}
            //if (txtwildsearch.Text.ToString().Length == 0)
            //{
            //    gridData.DataSource = dtcommonSearch;
            //    gridSettings();
            //}
           
        }

        private void FilterSearchRecords()
        {
            try
            {
                string rowfilterString = string.Empty;
                System.Text.StringBuilder filterString = new System.Text.StringBuilder();

                string[] filterText = txtwildsearch.Text.ToString().Trim().Split(' ') ;

                for (int index = 0; index <= filterText.Length- 1; index++)
                {
                    filterString.AppendFormat("Convert({0}, 'System.String') LIKE '%{1}%' AND ", "FILTER", filterText[index]);
                }

                rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 4);

                dtcommonSearch.DefaultView.RowFilter = rowfilterString ;
                 
                gridData.DataSource =dtcommonSearch.DefaultView;
                gridSettings();
                txtwildsearch.Select();
                txtwildsearch.Select(txtwildsearch.Text.Length, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void gridData_HeadClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            IsHEeaderClick = true;
        }
        // ashma 24 may 2018
        private void frmCommonSearchTrueGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }




    }
}
