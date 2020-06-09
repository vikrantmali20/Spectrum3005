using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using C1.Win.C1FlexGrid;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Spectrum.Models.Enums;
using System.Resources;
using Spectrum.Controls;
using System.Globalization;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace Spectrum.BO
{
    public class CommonFunc
    {
        public enum enumActionButton
        {
            aBNew = 1,
            aBEdit,
            aBLock
        }

        private static String ThemeSelect;
        public static String Themeselect
        {
            get { return ThemeSelect; }
            set { ThemeSelect = value; }
        }
        public enum enumImportExportItem
        {
            UploadItem = 1,
            ExportArticleXlsReport,
            ExportArticleHierarchy,
            ImportExportMaterial
        }

        public const string DefaultSupplier = "Internal";
        public const string LanguageCode = "EN";

        public static ResourceManager SpectrumResources { get; set; }

        public static System.Drawing.Color DefaultBorderColor { get { return System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214))))); } }

        #region "Logger"
        /// <summary>
        ///  Property Store Log file path and its name 
        /// </summary>
        public String LogFilePath { get; set; }

        #endregion

        public string GetEnumDescription(Enum EnumConstant)
        {

            FieldInfo fi = EnumConstant.GetType().GetField(EnumConstant.ToString());
            DescriptionAttribute[] attr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attr.Length > 0)
            {
                return attr[0].Description;
            }
            else
            {
                return EnumConstant.ToString();
            }
        }

        /// <summary>
        /// Hides the Column's From Grid
        /// </summary>
        /// <param name="dgGrid"></param>
        /// <param name="show"></param>
        /// <param name="colName"></param>
        public void HideColumns(C1FlexGrid dgGrid, bool show, params string[] colName)
        {
            try
            {
                int i = 0;
                for (i = 0; i <= colName.Length - 1; i++)
                {
                    if (dgGrid.Cols[colName[i]] != null)
                    {
                        dgGrid.Cols[colName[i]].Visible = show;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// validating Email Id 
        /// </summary>
        /// <param name="emailid"></param>
        /// <returns>true false </returns>
        /// <see cref="raja"/>
        public static bool validateEmailId(string emailid)
        {
            try
            {
                bool isEmail = Regex.IsMatch(emailid, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

              //string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                //var test = Regex.Match(emailid, pattern);
               // return  test.Success;
                return isEmail;

            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public static bool validateNumberAlphabet(string textVal)
        {
            try
            {
                string allowNumberAlphabatpattern = "^[A-Za-z0-9\b]+$";
                var test = Regex.Match(textVal, allowNumberAlphabatpattern);
                return test.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Data attach to combo box
        /// </summary>
        /// <UpdatedBy></UpdatedBy>
        /// <UpdatedOn></UpdatedOn>
        /// <param name="dtCombo">list</param>
        /// <param name="objComboBox">Name of ComboBox</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static void PopulateComboBoxData(ref Controls.ComboBox objComboBox, object dataList)
        {
            objComboBox.DataSource = dataList;
            objComboBox.ValueMember = "Code";
            objComboBox.DisplayMember = "Description";
            objComboBox.Splits[0].DisplayColumns[0].Visible = false;

            // objComboBox.AddItem(new { Text = "---Select---", Value = -1 });
        }

        /// <summary>
        /// Populate a column in grid as a combobox 
        /// </summary>
        /// <UpdatedBy></UpdatedBy>
        /// <UpdatedOn></UpdatedOn>
        /// <param name="FlexGrid">Name of the grid</param>
        /// <param name="dtGrid">Data Table</param>
        /// <param name="vColName">Name of the Column</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object PopulateGridComboBox(ref C1FlexGrid FlexGrid, ref DataTable dtGrid, string vColName)
        {

            Hashtable hashTable = new Hashtable();
            DataRow dataRow = null;
            foreach (DataRow dataRow_loopVariable in dtGrid.Rows)
            {
                dataRow = dataRow_loopVariable;
                hashTable.Add(dataRow[0], dataRow[1]);
            }
            FlexGrid.Cols[vColName].DataMap = hashTable;
            return "";
        }

        /// <summary>
        /// Creating New Records for the Binding Datatable
        /// </summary>
        /// <UpdatedBy></UpdatedBy>
        /// <UpdatedOn></UpdatedOn>
        /// <param name="frm">form</param>
        /// <param name="ds">DataSet</param>
        /// <param name="Tablename">Name of the table's</param>
        /// <remarks></remarks>
        public void CreateNewRecord(ref Form frm, ref DataSet ds, params string[] Tablename)
        {
            int i = 0;
            if (Tablename.Length > 0)
            {
                for (i = 0; i <= Tablename.Length - 1; i++)
                {
                    frm.BindingContext[ds.Tables[Tablename[i].ToString()]].AddNew();
                }
            }
        }

        /// <summary>
        /// Creating New Records for the Binding Datatable
        /// </summary>
        /// <UpdatedBy></UpdatedBy>
        /// <UpdatedOn></UpdatedOn>
        /// <param name="frm">form</param>
        /// <param name="dt">DataTable</param>
        /// <param name="Tablename">Name of the table's</param>
        /// <remarks></remarks>
        public void CreateNewRecord(ref Form frm, ref DataTable dt, string Tablename)
        {
            dt.TableName = Tablename;
            frm.BindingContext[dt].AddNew();
        }

        public  static string getResourceString(string resourceString)
        {
            return SpectrumResources.GetString(resourceString);
        }
        public static ResourceManager GetResourceManager()
        {
            try
            {
                // Gets a reference to the same assembly that contains the type that is creating the ResourceManager.
                //Assembly assembly;

                //// Gets a reference to a different assembly.
                //assembly = Assembly.Load("Spectrum.Globalization");

                CultureInfo cultureInfo = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                //dynamic ci = new CultureInfo(clsAdmin.CultureInfo);
                //'add by ram this take care of calendar to specific culture.
                //Thread.CurrentThread.CurrentCulture = ci
                //'end for add by ram 
                // Thread.CurrentThread.CurrentUICulture = ci;

                string ResourceFilePath = Application.StartupPath + "\\Resources";
                //ResourceFilePath = ResourceFilePath + "\\SpectrumLite.en-US";
                CommonFunc.SpectrumResources = ResourceManager.CreateFileBasedResourceManager("SpectrumLite.en-US", ResourceFilePath, null);
                return CommonFunc.SpectrumResources;
                // Creates the ResourceManager.
                //return new ResourceManager("Spectrum.Globalization.SpectrumLite", assembly);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objForm"></param>
        public static void SetCultureFromResource(Controls.RibbonForm objForm)
        {
            try
            {
                var stack = new Stack<Control>();
                objForm.Text =SpectrumResources.GetString(objForm.Name);

                foreach (Control ctrl in objForm.Controls)
                {
                    try
                    {
                        stack.Push(ctrl);

                        if (ctrl.GetType() == typeof(Spectrum.Controls.Label) ||
                            ctrl.GetType() == typeof(Spectrum.Controls.Button))
                          ctrl.Text = SpectrumResources.GetString(string.Format("{0}.{1}", objForm.Name ,ctrl.Name));
                        else if (ctrl.GetType() == typeof(Spectrum.Controls.FlexGrid)) 
                        {
                            C1FlexGrid grid = (C1FlexGrid)ctrl;
                            for (int rowIndex = 0; rowIndex < grid.Cols.Count; rowIndex++)
                                grid.Cols[rowIndex].Caption = SpectrumResources.GetString(string.Format("{0}.{1}.{2}", objForm.Name, ctrl.Name, grid.Cols[rowIndex].Name));

                        }
                    }
                    catch { }
                }

                while (stack.Count > 0)
                {
                    try
                    {
                        var next = stack.Pop();

                        foreach (Control childControl in next.Controls)
                        {
                            stack.Push(childControl);

                            if (childControl.GetType() == typeof(Spectrum.Controls.Label) ||
                                childControl.GetType() == typeof(Spectrum.Controls.Button))
                            {
                                if (childControl.Name != "LabelText")
                                {
                                    childControl.Text = SpectrumResources.GetString(string.Format("{0}.{1}", objForm.Name, childControl.Name));    
                                }
                            }
                            else if (childControl.GetType() == typeof(Spectrum.Controls.LabelMandatory))
                            {
                                ((Spectrum.Controls.LabelMandatory)(childControl)).NormalLabelText = SpectrumResources.GetString(string.Format("{0}.{1}", objForm.Name, childControl.Name));
                            }
                            else if (childControl.GetType() == typeof(Spectrum.Controls.FlexGrid))
                            {
                                C1FlexGrid grid = (C1FlexGrid)childControl;
                                for (int rowIndex = 0; rowIndex < grid.Cols.Count; rowIndex++)
                                    grid.Cols[rowIndex].Caption = SpectrumResources.GetString(string.Format("{0}.{1}.{2}", objForm.Name, childControl.Name, grid.Cols[rowIndex].Name));
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void WriteResourceFile(Controls.RibbonForm objForm)
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, string.Format("{0}.txt", objForm.Name));
                StreamWriter sWriter = File.CreateText(filePath);

                var stack = new Stack<Control>();

                sWriter.WriteLine(objForm.Name + "=" + objForm.Text);

                foreach (Control ctrl in objForm.Controls)
                {
                    try
                    {
                        stack.Push(ctrl);

                        if (ctrl.GetType() == typeof(Spectrum.Controls.Label) ||
                            ctrl.GetType() == typeof(Spectrum.Controls.Button))
                            sWriter.WriteLine(string.Format("{0}.{1}={2}", objForm.Name, ctrl.Name, ctrl.Text));
                        else if (ctrl.GetType() == typeof(Spectrum.Controls.FlexGrid))
                        {
                            C1FlexGrid grid = (C1FlexGrid)ctrl;
                            for (int rowIndex = 0; rowIndex < grid.Cols.Count; rowIndex++)
                                sWriter.WriteLine(string.Format("{0}.{1}.{2}={3}", objForm.Name, ctrl.Name, grid.Cols[rowIndex].Name, grid.Cols[rowIndex].Caption));
                        }
                    }
                    catch { }
                }

                while (stack.Count > 0)
                {
                    try
                    {
                        var next = stack.Pop();

                        foreach (Control childControl in next.Controls)
                        {
                            stack.Push(childControl);

                            if (childControl.GetType() == typeof(Spectrum.Controls.Label) ||
                                childControl.GetType() == typeof(Spectrum.Controls.Button))
                                sWriter.WriteLine(string.Format("{0}.{1}={2}", objForm.Name, childControl.Name, childControl.Text));

                            else if (childControl.GetType() == typeof(Spectrum.Controls.LabelMandatory))
                                sWriter.WriteLine(string.Format("{0}.{1}={2}", objForm.Name, childControl.Name,((LabelMandatory)(childControl)).NormalLabelText));
                            else if (childControl.GetType() == typeof(Spectrum.Controls.FlexGrid))
                            {
                                C1FlexGrid grid = (C1FlexGrid)childControl;
                                for (int rowIndex = 0; rowIndex < grid.Cols.Count; rowIndex++)
                                    sWriter.WriteLine(string.Format("{0}.{1}.{2}={3}", objForm.Name, childControl.Name, grid.Cols[rowIndex].Name, grid.Cols[rowIndex].Caption));
                            }
                        }
                    }
                    catch { }
                }

                sWriter.Flush();
                sWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
         
        public static void setEnableControl(Controls.RibbonForm objForm, object Container, bool enable = false)
        {
            try
            {
                var stack = new Stack<Control>();
                stack.Push((Control)Container);

                while (stack.Count > 0)
                {
                    var next = stack.Pop();
                    if (next.GetType() == typeof(Spectrum.Controls.TextBox)
                        || next.GetType() == typeof(Spectrum.Controls.ComboBox)
                        || next.GetType() == typeof(System.Windows.Forms.CheckBox)
                        || next.GetType() == typeof(System.Windows.Forms.RadioButton)
                        || next.GetType() == typeof(Spectrum.Controls.Button))
                    {
                        next.Enabled = enable;
                    }
                    foreach (Control child in next.Controls)
                    {
                        stack.Push(child);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool SetErrorProvidertoControl(ref ErrorProvider ep, ref NumericUpDown textBox, string errorMessage = "", bool validateFormat = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox.Value.ToString()) && validateFormat)
                {
                    ep.SetError(textBox, errorMessage);
                    ep.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(textBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                  //  textBox.BackColor = System.Drawing.Color.Red;
                    return false;
                }
                else if (string.IsNullOrEmpty(textBox.Value.ToString()))
                {
                    ep.SetError(textBox, errorMessage);
                    ep.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(textBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    //textBox.BackColor = System.Drawing.Color.Red;
                    return false;
                }
            else if ((int)textBox.Value == 0  )
                {
                    ep.SetError(textBox, errorMessage);
                    ep.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(textBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    //textBox.BackColor = System.Drawing.Color.Red;
                    return false;
                }
                else
                {
                    ep.SetError(textBox, string.Empty);
                  //  textBox.BackColor = DefaultBorderColor;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool SetErrorProviderComboControl(ref ErrorProvider ep, ref System.Windows.Forms.ComboBox comboBox, string errorMessage = "", bool validateFormat = false)
        {
            try //Spectrum.Controls.ComboBox comboBox
            {
                if (comboBox.SelectedIndex == -1 || comboBox.SelectedValue == null || comboBox.SelectedValue.ToString() == "Select")
                {
                    ep.SetError(comboBox, errorMessage);
                    ep.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(comboBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

                    //comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
                    return false;
                }
                else
                {
                    ep.SetError(comboBox, string.Empty);
                    // comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool SetErrorProvidertoControl(ref ErrorProvider ep, ref Spectrum.Controls.TextBox textBox, string errorMessage = "", bool validateFormat = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox.Value.ToString()) && validateFormat)
                {
                    ep.SetError(textBox, errorMessage);
                    ep.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(textBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    textBox.BorderColor = System.Drawing.Color.Red;
                    return false;
                }
                else if (string.IsNullOrEmpty(textBox.Value.ToString()))
                {
                    ep.SetError(textBox, errorMessage);
                    ep.SetIconAlignment(textBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(textBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    textBox.BorderColor = System.Drawing.Color.Red;
                    return false;
                }
                else
                {
                    ep.SetError(textBox, string.Empty);
                    textBox.BorderColor = DefaultBorderColor;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool SetErrorProvidertoControl(ref ErrorProvider ep, ref Spectrum.Controls.ComboBox comboBox, string errorMessage = "", bool validateFormat = false)
        {
            try
            {
                if (comboBox.SelectedIndex == -1 || comboBox.SelectedValue == null || comboBox.SelectedValue.ToString() == "Select")
                {
                    ep.SetError(comboBox, errorMessage);
                    ep.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(comboBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
                    return false;
                }
                else
                {
                    ep.SetError(comboBox, string.Empty);
                    comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SetErrorProvidertoControl(ref ErrorProvider ep, ref Spectrum.Controls.DatePicker datePicker, string errorMessage = "")
        {
            try
            {
                if (string.IsNullOrEmpty(datePicker.Value.ToString()))
                {
                    ep.SetError(datePicker, errorMessage);
                    datePicker.BorderColor = System.Drawing.Color.Red;
                    return false;
                }
                else if (!string.IsNullOrEmpty(datePicker.Value.ToString()))
                {
                    ep.SetError(datePicker, errorMessage);
                    datePicker.BorderColor = System.Drawing.Color.Red;

                    return false;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool SetCustomErrorProvidertoControl(ref ErrorProvider ep, ref Spectrum.Controls.TextBox txtBox, string errorMessage = "",bool flag=false )
        {
            try
            {
                if (flag==false)
                {
                    if (errorMessage != "")
                    {
                        ep.SetError(txtBox, errorMessage);
                        txtBox.BorderColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        ep.Clear();
                        txtBox.BorderColor = System.Drawing.Color.LightGray;
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable ConvertListToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public static string GetEnumStringValue(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DialogResult ShowMessage(string message, MessageType messageType)
        {
            var dialogMessage = new frmPopupMessage(messageType);
            dialogMessage.MessageText.Text = message;

            DialogResult dialogResult = dialogMessage.ShowDialog();
            dialogMessage.Dispose();

            return dialogResult;
        }

        #region functions For Windows control
        public static bool SetErrorProvidertoControlForWindowsForm(ref ErrorProvider ep, ref System.Windows.Forms.ComboBox comboBox, string errorMessage = "", bool validateFormat = false)
        {
            try
            {
                if (comboBox.SelectedIndex == -1 || comboBox.SelectedValue == null || comboBox.SelectedValue.ToString() == "Select")
                {
                    ep.SetError(comboBox, errorMessage);
                    ep.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleRight);
                    ep.SetIconPadding(comboBox, 2);
                    ep.BlinkRate = 1000;
                    ep.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
                    // comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
                    return false;
                }
                else
                {
                    ep.SetError(comboBox, string.Empty);
                    //comboBox.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PopulateComboBoxDataForWindowsForm(ref System.Windows.Forms.ComboBox objComboBox, object dataList)
        {
            objComboBox.DataSource = dataList;
            objComboBox.ValueMember = "Code";
            objComboBox.DisplayMember = "Description";
        }
        public static void PopulateComboBoxDataOfStockOutReaseasonforWindowsForm(ref System.Windows.Forms.ComboBox objComboBox, object dataList)
        {
            try
            {

                objComboBox.DataSource = dataList;
                objComboBox.ValueMember = "Code";
                objComboBox.DisplayMember = "Description";
                //objComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                //objComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
