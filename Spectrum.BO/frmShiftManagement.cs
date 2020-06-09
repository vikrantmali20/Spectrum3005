using C1.Win.C1FlexGrid;
using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL;
using Spectrum.Models;
using Spectrum.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Spectrum.BO
{
    public partial class frmShiftManagement : Spectrum.Controls.RibbonForm
    {
        int comboValue;
        public frmShiftManagement()
        {
            InitializeComponent();
            shiftManagementManager = new ShiftManagementManager();
            this.commonManager = new CommonManager();
        }
        IShiftManagementManager shiftManagementManager;
        ICommonManager commonManager;
        List<MstShift> objShift = new List<MstShift>();

        private void frmShiftManagement_Load(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                FillCombo();
                FillGrid();
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
            }
            catch (Exception ex) { }
        }

        #region Methods
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            lblSiteCode.BackColor = Color.FromArgb(212, 212, 212);

            btnAdd.BackColor = Color.Transparent;
            btnAdd.BackColor = Color.FromArgb(0, 107, 163);
            btnAdd.ForeColor = Color.FromArgb(255, 255, 255);
            btnAdd.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSave.BackColor = Color.Transparent;
            btnSave.BackColor = Color.FromArgb(0, 107, 163);
            btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnClear.BackColor = Color.Transparent;
            btnClear.BackColor = Color.FromArgb(0, 107, 163);
            btnClear.ForeColor = Color.FromArgb(255, 255, 255);
            btnClear.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            grdShift.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            grdShift.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            grdShift.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            grdShift.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            grdShift.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdShift.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdShift.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdShift.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdShift.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            grdShift.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            grdShift.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);
        }
        protected void FillCombo()
        {
            try
            {
                cmbSiteCode.Items.Clear();
                cmbSiteCode.Items.Add(this.commonManager.GetSiteName());
                cmbSiteCode.SelectedItem = this.commonManager.GetSiteName();
            }
            catch (Exception ex) { }
        }
        protected void FillGrid()
        {
            try
            {
                if (shiftManagementManager.LoadData(ref objShift))
                {
                    GridSetting();
                }
                else { }
            }
            catch (Exception ex) { }

        }
        protected void GridSetting()
        {
            grdShift.DataSource = null;
            grdShift.DataSource = objShift;

            DataTable table = new DataTable();
            table.Columns.Add("Edit", typeof(bool));
            table.Columns.Add("Shift Name");
            table.Columns.Add("Sequence");
            table.Columns.Add("Status");
            table.Columns.Add("ShiftId");
            for (int i = 0; i < objShift.Count; i++)
            {
                DataRow datarow = table.NewRow();
                datarow["Edit"] = false;
                datarow["Shift Name"] = objShift[i].ShiftName;
                datarow["Sequence"] = Convert.ToString(objShift[i].Sequence);
                if (Convert.ToBoolean(objShift[i].STATUS) == true)
                    datarow["Status"] = "Active";
                else
                    datarow["Status"] = "Deactive";
                datarow["ShiftId"] = objShift[i].ShiftId;
                table.Rows.Add(datarow);
            }
            grdShift.DataSource = table;

            ColumnSettings();
        }
        protected void ColumnSettings()
        {
            Column chkboxEdit = grdShift.Cols["Edit"];
            chkboxEdit.Width = 83;
            chkboxEdit.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftCenter;

            Column shiftName = grdShift.Cols["Shift Name"];
            shiftName.Width = 250;

            Column sequence = grdShift.Cols["Sequence"];
            sequence.Width = 125;

            Column status = grdShift.Cols["Status"];
            status.ComboList = "Active|Deactive";
            status.Width = 250;

            ColumnVisibility();
        }
        protected void ColumnVisibility()
        {
            grdShift.AllowEditing = true;
            grdShift.Cols[0].AllowEditing = true;
            grdShift.Cols[1].AllowEditing = false;
            grdShift.Cols[2].AllowEditing = false;
            grdShift.Cols[3].AllowEditing = false;
            grdShift.Cols[4].Visible = false;
        }
        #endregion

        #region Events
        private void frmShiftManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void grdShift_CellChecked(object sender, RowColEventArgs e)
        {
            try
            {
                if (grdShift.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    btnSave.Enabled = true;
                    grdShift.AllowEditing = true;
                    for (int rowindex = 1; rowindex < grdShift.Rows.Count; rowindex++)
                    {
                        if (rowindex == e.Row)
                        {
                            grdShift.Rows[rowindex].AllowEditing = true;
                            grdShift.Cols[1].AllowEditing = true;
                            grdShift.Cols[3].AllowEditing = true;
                        }
                        else
                        {
                            grdShift.Rows[rowindex].AllowEditing = false;
                        }
                    }
                    grdShift.Cols[2].AllowEditing = false;
                }
                else
                {
                    for (int rowindex = 1; rowindex < grdShift.Rows.Count; rowindex++)
                    {
                        grdShift.Rows[rowindex].AllowEditing = true;
                    }
                    ColumnVisibility();
                }
            }
            catch (Exception ex) { }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                btnSave.Enabled = true;
                grdShift.AllowEditing = true;
                grdShift.Cols[2].AllowEditing = false;

                DataTable table = new DataTable();
                table.Columns.Add("Edit", typeof(bool));
                table.Columns.Add("Shift Name");
                table.Columns.Add("Sequence");
                table.Columns.Add("Status");
                table.Columns.Add("ShiftId");
                for (int i = 1; i < grdShift.Rows.Count; i++)
                {
                    DataRow datarow = table.NewRow();
                    datarow["Edit"] = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(grdShift.Rows[i][1])))
                    {
                        datarow["Shift Name"] = grdShift[i, 1];
                        datarow["Sequence"] = grdShift[i, 2];
                        datarow["Status"] = grdShift[i, 3];
                        datarow["ShiftId"] = grdShift[i, 4];
                        table.Rows.Add(datarow);
                    }
                    else
                    {
                        flag = true;
                        CommonFunc.ShowMessage("Please Add Shift Name", MessageType.Information);
                        break;
                    }
                }
                if (!flag)
                {
                    DataRow blankrow = table.NewRow();
                    blankrow["Edit"] = false;
                    blankrow["Shift Name"] = "";
                    blankrow["Sequence"] = grdShift.Rows.Count;
                    blankrow["Status"] = "Active";
                    blankrow["ShiftId"] = grdShift.Rows.Count;
                    table.Rows.Add(blankrow);
                    grdShift.DataSource = table;
                    grdShift.SetCellCheck(grdShift.Rows.Count - 1, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    ColumnSettings();
                    for (int gridrow = 1; gridrow < grdShift.Rows.Count; gridrow++)
                    {
                        if (gridrow == grdShift.Rows.Count - 1)
                        {
                            grdShift.Rows[gridrow].AllowEditing = true;
                            grdShift.Cols[1].AllowEditing = true;
                            grdShift.Cols[3].AllowEditing = true;
                        }
                        else
                        {
                            grdShift.Rows[gridrow].AllowEditing = false;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdShift.Rows.Count > 1)
                {
                    bool flag = false;
                    List<MstShift> items = new List<MstShift>();
                    for (int i = 1; i < grdShift.Rows.Count; i++)
                    {
                        MstShift item = new MstShift();
                        item.SiteCode = CommonModel.SiteCode;
                        if (!string.IsNullOrEmpty(Convert.ToString(grdShift.Rows[i][1])))
                        {
                            item.ShiftName = grdShift.Rows[i][1].ToString();
                            item.Sequence = Convert.ToInt32(grdShift.Rows[i][2]);
                            if (grdShift.Rows[i][3].ToString() == "Active")
                                item.STATUS = true;
                            else
                                item.STATUS = false;
                            item.ShiftId = Convert.ToInt32(grdShift.Rows[i][4]);
                            items.Add(item);
                        }
                        else
                        {
                            flag = true;
                            CommonFunc.ShowMessage("Please Add Shift Name", MessageType.Information);
                            break;
                        }
                    }
                    if (!flag)
                    {
                        if (shiftManagementManager.SaveData(ref items))
                        {
                            System.Windows.Forms.Application.DoEvents();
                            CommonFunc.ShowMessage("Shift(s) added successfully!", MessageType.Information);
                            FillGrid();
                        }
                        else
                        {
                            System.Windows.Forms.Application.DoEvents();
                            CommonFunc.ShowMessage("Failure in saving Shift data!", MessageType.Information);
                        }
                    }
                    grdShift.AllowEditing = true;
                    grdShift.Cols[0].AllowEditing = true;
                    grdShift.Cols[3].AllowEditing = false;
                    grdShift.Cols[4].AllowEditing = false;
                    grdShift.Cols[11].AllowEditing = false;
                }
                else
                {
                    System.Windows.Forms.Application.DoEvents();
                    CommonFunc.ShowMessage("Failure in saving Shift data!", MessageType.Information);
                }
            }
            catch (Exception ex) { }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                FillGrid();
                //if (grdShift.Rows.Count > 1)
                //{
                //    string SiteCode = CommonModel.SiteCode;
                //    int ShiftId = Convert.ToInt32(grdShift.Rows[grdShift.Rows.Count - 1][2]);
                //    if (shiftManagementManager.GetShift(ref SiteCode, ref ShiftId))
                //    {
                //        grdShift.RemoveItem(grdShift.Rows.Count - 1);
                //    }
                //    for (int rowindex = 1; rowindex < grdShift.Rows.Count; rowindex++)
                //    {
                //        grdShift.Rows[rowindex].AllowEditing = true;
                //        if (grdShift.GetCellCheck(rowindex, 0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                //        {
                //            grdShift.SetCellCheck(rowindex, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                //        }
                //    }
                //    ColumnVisibility();
                    
                //}
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}