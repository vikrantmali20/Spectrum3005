using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Spectrum.BL;
using Spectrum.BL.BusinessInterface;
using Spectrum.Models;
using Spectrum.Logging;
using Spectrum.Controls;
using Spectrum.Models.Enums;
using System.Collections;
using C1.Win.C1FlexGrid;
namespace Spectrum.BO
{
    public partial class frmCharacteristics : Spectrum.Controls.RibbonForm
    {
        #region "Class Variable"
        public frmCharacteristics()
        {
            InitializeComponent();

            this.characteristicManager = new CharacteristicManager();

            this.grdSize.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellChanged);
            this.grdColour.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellChanged);
            this.grdStyle.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellChanged);
            this.grdFabric.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellChanged);

            this.btnDeleteSize.Click += new System.EventHandler(this.DeleteAllCharacteristic_Click);
            this.btnDeleteColour.Click += new System.EventHandler(this.DeleteAllCharacteristic_Click);
            this.btnDeleteStyle.Click += new System.EventHandler(this.DeleteAllCharacteristic_Click);
            this.btnDeleteFabric.Click += new System.EventHandler(this.DeleteAllCharacteristic_Click);

            this.chkSelectSize.CheckedChanged += new System.EventHandler(this.SelectAllCharacteristic_CheckedChanged);
            this.chkSelectColour.Click += new System.EventHandler(this.SelectAllCharacteristic_CheckedChanged);
            this.chkSelectStyle.Click += new System.EventHandler(this.SelectAllCharacteristic_CheckedChanged);
            this.chkSelectFabric.Click += new System.EventHandler(this.SelectAllCharacteristic_CheckedChanged);

            this.grdSize.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellButtonClick);
            this.grdColour.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellButtonClick);
            this.grdStyle.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellButtonClick);
            this.grdFabric.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridCharacteristic_CellButtonClick);

            this.grdSize.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.GridCharacteristic_BeforeSelChange);
            this.grdColour.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.GridCharacteristic_BeforeSelChange);
            this.grdStyle.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.GridCharacteristic_BeforeSelChange);
            this.grdFabric.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.GridCharacteristic_BeforeSelChange);

             
            
        }
        

 
 
        ICharacteristicManager characteristicManager;
        private bool stopCellChangedEvent = false;
        private string CharCode { get; set; }
        private string beforeChangeCharValue = string.Empty;
        private string CharHeaderText = string.Empty;
        private FlexGrid grdCharacteristic { get; set; }
        private CheckBox chkCharacteristic { get; set; }

        private IList<DropDownModel> CharacteristicTypes { get; set; }
        private IList<CharacteristicModel> CharacteristicValues { get; set; }
       // private IList<CharacteristicModel> CharacteristicUpdateValues { get; set; }
        #endregion

        #region "Events"
        
        private void frmCharacteristics_Load(object sender, EventArgs e)
        {
            try
            {
                if (CommonFunc.Themeselect == "Theme 1")
                {
                    ThemeChange();
                }
                LoadDefaultCharacteristics();
                NonEditGridCell();
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void DeleteAllCharacteristic_Click(object sender, EventArgs e)
        {
            try
            {
                IList<CharacteristicModel> charList;
                GetControlID((sender as Spectrum.Controls.Button).Name.ToLower());
                FlexGrid grdCharacteristicTemp = grdCharacteristic;

                charList = CharacteristicValues.Where(c => c.CharCode == CharCode && c.Select == true).ToList();

                if (charList.Count == 0)
                {
                    CommonFunc.ShowMessage("Please select at least 1 record to delete.", MessageType.Information);
                    return;
                }

                if (CommonFunc.ShowMessage("The selected record(s) will be deleted. Are you sure?", MessageType.OKCancel) == DialogResult.OK)
                {
                    for (int rowIndex = 0; rowIndex < charList.Count(); rowIndex++)
                    {
                        var isValueAssociate = this.characteristicManager.IsProductAssociate(charList[rowIndex].CharValue);
                        if (isValueAssociate == false)
                            charList[rowIndex].CharStatus = "Deleted";
                        else
                        {                              
                           CommonFunc.ShowMessage(charList[rowIndex].CharValue + " value is associated with Active Products in the system. First remove the Product association.", MessageType.Information);
                           LoadDefaultCharacteristics();
                           return;
                        }
                    }
                    DefaultGridSetting(ref grdCharacteristicTemp, CharCode);
                    chkCharacteristic.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void SelectAllCharacteristic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetControlID((sender as CheckBox).Name.ToLower());
                FlexGrid grdCharacteristicTemp = grdCharacteristic;
                var charList = CharacteristicValues.Where(c => c.CharCode == CharCode).ToList();

                for (int rowIndex = 0; rowIndex < charList.Count(); rowIndex++)
                    charList[rowIndex].Select = (sender as CheckBox).Checked;

                stopCellChangedEvent = true;
                DefaultGridSetting(ref grdCharacteristicTemp, CharCode);
                stopCellChangedEvent = false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void GridCharacteristic_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                FlexGrid grdCharacteristic = sender as FlexGrid;
                CharCode = GetCharCode(grdCharacteristic.Name);

                //var charValue = grdCharacteristic.Rows[grdCharacteristic.Row][grdCharacteristic.Col - 1].ToString();
                //var characteristicModel = CharacteristicValues.Where(c => c.CharCode == CharCode && c.CharValue == charValue).FirstOrDefault();

                //if (characteristicModel != null && CommonFunc.ShowMessage("The selected record will be deleted. Are you sure?", MessageType.OKCancel) == DialogResult.OK)
                //{
                //    characteristicModel.CharStatus = "Deleted";
                //    DefaultGridSetting(ref grdCharacteristic, CharCode);
                //}
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void GridCharacteristic_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (!stopCellChangedEvent)
                {
                    FlexGrid grdCharacteristic = sender as FlexGrid;
                    int rowIndex = grdCharacteristic.Row;
                    var newValCheck = grdCharacteristic.Rows[rowIndex][2].ToString().Trim();
                    if (!string.IsNullOrEmpty(newValCheck.Trim()))
                    {
                        if (newValCheck.Trim().Length > 40)
                        {
                            CommonFunc.ShowMessage(newValCheck.Trim() + Environment.NewLine + " Max Length should be 40 characters.", MessageType.Information);
                            newValCheck= newValCheck.Trim().Substring(0, 40);
                            grdCharacteristic.Rows[rowIndex][2] = newValCheck;
                            return ;
                        }
                    }
                    if (grdCharacteristic.Row == grdCharacteristic.Rows.Count - 1)
                    {
                        CharCode = GetCharCode(grdCharacteristic.Name);

                        CharacteristicValues.Add(new CharacteristicModel
                        {
                            CharCode = GetCharCode(grdCharacteristic.Name),
                            CharValue = grdCharacteristic.Rows[grdCharacteristic.Row][grdCharacteristic.Col].ToString(),
                            CharStatus = "Added"
                        });
                        DefaultGridSetting(ref grdCharacteristic, CharCode);
                    }
                    else
                    {
                        var newVal = grdCharacteristic.Rows[rowIndex][2].ToString().Trim();
                        var prevVal = grdCharacteristic.Rows[rowIndex][6].ToString().Trim();
                        var isValueAssociate = this.characteristicManager.IsProductDeletAssociate(prevVal);
                        if (isValueAssociate != null)
                        {
                            if (isValueAssociate.ToLower() != newVal.ToLower())
                            {
                                CommonFunc.ShowMessage(prevVal + " value is associated with Active Products in the system. You Can not modified.", MessageType.Information);
                               // LoadDefaultCharacteristics();
                                grdCharacteristic.Rows[rowIndex][2] = prevVal;
                                return;
                            }
                             
                        }

                        

                        //if (newVal != prevVal)
                        //{
                        //    CharacteristicUpdateValues.Add(new CharacteristicModel
                        //    {
                        //        CharCode = grdCharacteristic.Rows[rowIndex][2].ToString(),
                        //        CharValue = newVal ,
                        //        CharStatus = "Modified",
                        //        OriginalCharValue=prevVal 
                        //    });
                        //    //grdCharacteristic.Rows[rowIndex][4] = "Modified";
                        //  //  grdCharacteristic.Rows[rowIndex][5] = grdCharacteristic.Rows[rowIndex][6].ToString();
                        //}
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }

        private void GridCharacteristic_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (!stopCellChangedEvent)
                {
                    FlexGrid grdCharacteristic = sender as FlexGrid;
                    int rowIndex = grdCharacteristic.Row;

                    //if (grdCharacteristic.Rows[rowIndex]["OriginalCharValue"] == null)
                    //    grdCharacteristic.Rows[rowIndex]["OriginalCharValue"] = grdCharacteristic.Rows[rowIndex]["CharValue"];

                    //if (grdCharacteristic.Rows[rowIndex]["CharStatus"] == null)
                    //    grdCharacteristic.Rows[rowIndex]["CharStatus"] = "Modified";
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flagAdd = false;
                string statusAddMessage = "Characteristic Saved Successfully.";
                string statusUpdateMessage = "Characteristic Updated Successfully.";
                if (CharacteristicValues.Count > 0)
                {
                    foreach (var IsAdded in CharacteristicValues)
                    {
                        if (  IsAdded.CharStatus == "Added" )
                        {
                            flagAdd = true;
                            break;
                        }
                    }
                    foreach (var IsModify in CharacteristicValues)
                    {
                        if (IsModify.PreValue != IsModify.CharValue && IsModify.CharStatus != "Added" && IsModify.CharStatus != "Deleted")
                        {
                            IsModify.CharStatus = "Modified";
                            IsModify.OriginalCharValue = IsModify.PreValue;
                        }
                    }
                    var toRemove = CharacteristicValues.Where(a => a.CharValue == "" || a.CharValue.ToLower() == "false" || a.CharValue.ToLower() == "true" || a.CharStatus ==null).ToList();

                    foreach (var itemToRemove in toRemove)
                        CharacteristicValues.Remove(itemToRemove);

                    var isModified = CharacteristicValues.Where(a => a.CharStatus == "Modified").ToList();
                     
                    if (CharacteristicValues.Count > 0)
                    {
                        characteristicManager.SaveCharacteristic(CharacteristicValues);
                        if(flagAdd)
                            CommonFunc.ShowMessage(statusAddMessage, MessageType.Information);
                        else
                            CommonFunc.ShowMessage(statusUpdateMessage, MessageType.Information);
                        LoadDefaultCharacteristics();
                    }
                }
                else
                {
                    CommonFunc.ShowMessage("Please Add Items.", MessageType.Information);                   
                    return;
                }
            }
            catch (Exception ex)
            {
                LoadDefaultCharacteristics();
                Logger.Log(ex, Logger.LogingLevel.Error);
                CommonFunc.ShowMessage("Data Not save", MessageType.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
        #endregion

        #region "Functions"
        /// <summary>
        /// 
        /// </summary>
        private void LoadDefaultCharacteristics()
        {
            try
            {
                CharacteristicTypes = this.characteristicManager.GetCharacteristicType();
                //CharacteristicValues = this.characteristicManager.GetCharacteristicList();
                CharacteristicValues = this.characteristicManager.GetCharacteristicListNew().ToList();
                

                stopCellChangedEvent = true;
                DefaultGridSetting(ref grdColour, GetCharCode(grdColour.Name));
                DefaultGridSetting(ref grdSize, GetCharCode(grdSize.Name));
                DefaultGridSetting(ref grdStyle, GetCharCode(grdStyle.Name));
                DefaultGridSetting(ref grdFabric, GetCharCode(grdFabric.Name));
                stopCellChangedEvent = false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DefaultGridSetting(ref FlexGrid flexGrid, string charCode)
        {
            try
            {
                var characteristics = CharacteristicValues.Where(c => c.CharCode == charCode && c.CharStatus != "Deleted").ToList();
                characteristics.RemoveAll(a => a.CharValue == "" || a.CharValue.ToLower()=="false" || a.CharValue.ToLower()=="true");
                var isLastEmptyCode = characteristics.Where(a => a.CharValue == "").ToList();

               // var removeEmptyCodes = characteristics.Remove(isLastEmptyCode);


                
                if (isLastEmptyCode.Count == 0)
                {
                    characteristics.Add(new CharacteristicModel());

                    flexGrid.DataSource = characteristics;

                    flexGrid.Cols["Select"].Width = 40;
                    flexGrid.Cols["Select"].Caption = string.Empty;
                   
                    flexGrid.Cols["CharCode"].Visible = false;

                    flexGrid.Cols["CharValue"].Width = 310; 
                    flexGrid.Cols["CharValue"].Caption = CharHeaderText;
                     

                    flexGrid.Cols["delete"].Width = 2;
                    flexGrid.Cols["delete"].Caption = string.Empty;
                    //flexGrid.Cols["delete"].ComboList  = "...";

                    flexGrid.Cols["CharStatus"].Visible = false;
                    flexGrid.Cols["OriginalCharValue"].Visible = false;
                    flexGrid.Cols["PreValue"].Visible = false;

                  //  flexGrid.Select(characteristics.Count - 1, 2);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns></returns>
        private string GetCharCode(string gridName)
        {
            try
            {
                gridName = gridName.Replace("grd", string.Empty).ToLower();
                  
                switch (gridName)
                {
                        
                    case "colour":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "color" || c.Description.ToLower() == "colour").FirstOrDefault().Code;
                        CharHeaderText = "Color";
                        break;

                    case "size":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "size").FirstOrDefault().Code;
                        CharHeaderText = "Size";
                        break;

                    case "style":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "style").FirstOrDefault().Code;
                        CharHeaderText = "Style";
                        break;

                    case "fabric":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "fabric").FirstOrDefault().Code;
                        CharHeaderText = "Fabric";
                        break;

                    default:
                        break;
                }
                return CharCode;
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlName"></param>
        private void GetControlID(string controlName)
        {
            try
            {
                controlName = controlName.Replace("grd", string.Empty).ToLower();
                controlName = controlName.Replace("btndelete", string.Empty).ToLower();
                controlName = controlName.Replace("chkselect", string.Empty).ToLower();

                switch (controlName)
                {
                    case "colour":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "color" || c.Description.ToLower() == "colour").FirstOrDefault().Code;
                        grdCharacteristic = grdColour;
                        chkCharacteristic = chkSelectColour;
                        CharHeaderText = "Color";
                        break;

                    case "size":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "size").FirstOrDefault().Code;
                        grdCharacteristic = grdSize;
                        chkCharacteristic = chkSelectSize;
                        CharHeaderText = "Size";
                        break;

                    case "style":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "style").FirstOrDefault().Code;
                        grdCharacteristic = grdStyle;
                        chkCharacteristic = chkSelectStyle;
                        CharHeaderText = "Style";
                        break;

                    case "fabric":
                        CharCode = CharacteristicTypes.Where(c => c.Description.ToLower() == "fabric").FirstOrDefault().Code;
                        grdCharacteristic = grdFabric;
                        chkCharacteristic = chkSelectFabric;
                        CharHeaderText = "Fabric";
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex, Logger.LogingLevel.Error);
                throw ex;
            }
        }
        private void NonEditGridCell()
        {
           
            for (int i = 1; i <= grdColour.Rows.Count; i++)
            {             
                var a = grdColour[i, 2];
                //  grdColour[i,2];
            }
             

            //List<C1.Win.C1FlexGrid.CellRange> noneditablelist = new List<C1.Win.C1FlexGrid.CellRange>();
             
            //grdColour.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            //grdColour.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            //string a = grdColour.Cols[1].
            //noneditablelist.Add(grdColour.GetCellRange(1, 3));
             
            //foreach (C1.Win.C1FlexGrid.CellRange lc in noneditablelist)
            //{
            //    lc.StyleNew.BackColor = Color.LightGreen;
            //    var ab = lc.UserData;
            //    grdColour.SetUserData(lc.r1, lc.c1, "Locked");
            //}
             
        }
        private void ThemeChange()
        {
            this.BackgroundColor = Color.FromArgb(134, 134, 134);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BackColor = Color.FromArgb(0, 107, 163);
            btnCancel.ForeColor = Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnDeleteColour.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnDeleteColour.BackColor = Color.Transparent;
            btnDeleteColour.BackColor = Color.FromArgb(0, 107, 163);
            btnDeleteColour.ForeColor = Color.FromArgb(255, 255, 255);
            btnDeleteColour.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnDeleteColour.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnDeleteColour.FlatAppearance.BorderSize = 0;
            btnDeleteColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnDeleteFabric.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnDeleteFabric.BackColor = Color.Transparent;
            btnDeleteFabric.BackColor = Color.FromArgb(0, 107, 163);
            btnDeleteFabric.ForeColor = Color.FromArgb(255, 255, 255);
            btnDeleteFabric.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnDeleteFabric.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnDeleteFabric.FlatAppearance.BorderSize = 0;
            btnDeleteFabric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnDeleteSize.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnDeleteSize.BackColor = Color.Transparent;
            btnDeleteSize.BackColor = Color.FromArgb(0, 107, 163);
            btnDeleteSize.ForeColor = Color.FromArgb(255, 255, 255);
            btnDeleteSize.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnDeleteSize.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnDeleteSize.FlatAppearance.BorderSize = 0;
            btnDeleteSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnDeleteStyle.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnDeleteStyle.BackColor = Color.Transparent;
            btnDeleteStyle.BackColor = Color.FromArgb(0, 107, 163);
            btnDeleteStyle.ForeColor = Color.FromArgb(255, 255, 255);
            btnDeleteStyle.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnDeleteStyle.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnDeleteStyle.FlatAppearance.BorderSize = 0;
            btnDeleteStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            btnSave.BackColor = Color.Transparent;
            btnSave.BackColor = Color.FromArgb(0, 107, 163);
            btnSave.ForeColor = Color.FromArgb(255, 255, 255);
            btnSave.Font = new Font("Neo Sans", 9, FontStyle.Bold);
            btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;


            grdColour.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            grdColour.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            grdColour.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            grdColour.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            grdColour.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdColour.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdColour.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdColour.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdColour.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            grdColour.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            grdColour.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);


            grdFabric.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            grdFabric.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            grdFabric.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            grdFabric.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            grdFabric.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdFabric.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdFabric.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdFabric.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdFabric.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            grdFabric.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            grdFabric.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);

            grdSize.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            grdSize.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            grdSize.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            grdSize.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            grdSize.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdSize.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdSize.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdSize.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdSize.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            grdSize.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            grdSize.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);

            grdStyle.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom;
            grdStyle.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253);
            grdStyle.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255);
            grdStyle.Styles.Normal.Font = new Font("Neo Sans", 10, FontStyle.Regular);
            grdStyle.Styles.Fixed.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdStyle.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdStyle.Styles.Highlight.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdStyle.Styles.Focus.Font = new Font("Neo Sans", 10, FontStyle.Bold);
            grdStyle.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253);
            grdStyle.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212);
            grdStyle.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253);




        }
        #endregion

        private void grdColour_KeyDown(object sender, KeyEventArgs e)
        {
            
                e.Handled = true ;
            
        }

    }
}
