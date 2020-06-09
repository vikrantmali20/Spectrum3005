using Spectrum.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;



namespace Spectrum.BL
{
    public static class ConvertListToExcel
    {
        public static string ToString<T>(this IList<T> list, string include = "", string exclude = "", string sheetName="")
        {

            try
            {
                //Variables for build string
            string propStr = string.Empty;
            StringBuilder sb = new StringBuilder();

            //Get property collection and set selected property list
            PropertyInfo[] props = typeof (T).GetProperties();
            List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

            //Add list name and total count
            string typeName = sheetName;
            if (!string.IsNullOrEmpty(sheetName))
            {
                typeName = GetSimpleTypeName(list);
            }
               
            sb.AppendLine(string.Format("{0} List - Total Count: {1}", typeName, list.Count.ToString()));

            //Iterate through data list collection
            foreach (var item in list)
            {
                sb.AppendLine("");
                //Iterate through property collection
                foreach (var prop in propList)
                {
                    //Construct property name and value string
                    propStr = prop.Name + ": " + prop.GetValue(item, null);
                    sb.AppendLine(propStr);
                }
            }
            return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public static void ToCSV<T>(this IList<T> list, string path = "", string include = "", string exclude = "", string sheetName = "")
        {
            try
            {
                CreateCsvFile(list, path, include, exclude,sheetName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static void ToExcelNoInterop<T>(this IList<T> list, string path = "", string include = "",string exclude = "",string sheetName="")
        {
            try
            {

                if (path == "")
                    path = Path.GetTempPath() + @"ListDataOutput.csv";
                var rtnPath = CreateCsvFile(list, path, include, exclude, sheetName);

                //Open Excel from the file
                Process proc = new Process();
                //Quotes wrapped path for any space in folder/file names
                proc.StartInfo = new ProcessStartInfo("excel.exe", "\"" + rtnPath + "\"");
                proc.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string CreateCsvFile<T>(IList<T> list, string path, string include, string exclude, string sheetName = "")
        {
            try
            {
                //Variables for build CSV string
                StringBuilder sb = new StringBuilder();
                List<string> propNames;
                List<string> propValues;
                bool isNameDone = false;

                //Get property collection and set selected property list
                PropertyInfo[] props = typeof (T).GetProperties();
                List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

                //Add list name and total count
                string typeName = sheetName;
                if (!string.IsNullOrEmpty(sheetName))
                {
                    typeName = GetSimpleTypeName(list);
                }


                sb.AppendLine(string.Format("{0} List - Total Count: {1}", typeName, list.Count.ToString()));

                //Iterate through data list collection
                foreach (var item in list)
                {
                    sb.AppendLine("");
                    propNames = new List<string>();
                    propValues = new List<string>();

                    //Iterate through property collection
                    foreach (var prop in propList)
                    {
                        //Construct property name string if not done in sb
                        if (!isNameDone) propNames.Add(prop.Name);

                        //Construct property value string with double quotes for issue of any comma in string type data
                        var val = prop.PropertyType == typeof (string) ? "\"{0}\"" : "{0}";
                        propValues.Add(string.Format(val, prop.GetValue(item, null)));
                    }
                    //Add line for Names
                    string line = string.Empty;
                    if (!isNameDone)
                    {
                        line = string.Join(",", propNames);
                        sb.AppendLine(line);
                        isNameDone = true;
                    }
                    //Add line for the values
                    line = string.Join(",", propValues);
                    sb.Append(line);
                }
                if (!string.IsNullOrEmpty(sb.ToString()) && path != "")
                {
                    File.WriteAllText(path, sb.ToString());
                }
                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ToExcel<T>(this IList<T> list, string include = "", string exclude = "",string sheetName="")
        {
            try
            {
                //Get property collection and set selected property list ...
                PropertyInfo[] props = typeof (T).GetProperties();
                List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);

                //Get simple type name ...
                string typeName = sheetName;
                if (!string.IsNullOrEmpty(sheetName))
                {
                    typeName = GetSimpleTypeName(list);
                }

                //Convert list to array for selected properties
                object[,] listArray = new object[list.Count + 1, propList.Count];

                //Add property name to array as the first Row
                int colIdx = 0;
                foreach (var prop in propList)
                {
                    listArray[0, colIdx] = prop.Name;
                    colIdx++;
                }
                //Iterate through data list collection for Rows
                int rowIdx = 1;
                foreach (var item in list)
                {
                    colIdx = 0;
                    //Iterate through property collection for columns
                    foreach (var prop in propList)
                    {
                        //Do property value
                        listArray[rowIdx, colIdx] = prop.GetValue(item, null);
                        colIdx++;
                    }
                    rowIdx++;
                }
                //Processing for Excel
                object oOpt = System.Reflection.Missing.Value;
                Excel.Application oXL = new Excel.Application();
                Excel.Workbooks oWBs = oXL.Workbooks;
                Excel.Workbook oWB = oWBs.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                oSheet.Name = typeName;
                Excel.Range oRng = oSheet.get_Range("A1", oOpt).get_Resize(list.Count + 1, propList.Count);
                oRng.set_Value(oOpt, listArray);
                //Open Excel
                oXL.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            try
            {
                List<PropertyInfo> propList = new List<PropertyInfo>();
                if (include != "") //Do include first
                {
                    var includeProps = include.ToLower().Split(',').ToList();
                    foreach (var item in props)
                    {
                        var propName = includeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                        if (!string.IsNullOrEmpty(propName))
                            propList.Add(item);
                    }
                }
                else if (exclude != "") //Then do exclude
                {
                    var excludeProps = exclude.ToLower().Split(',');
                    foreach (var item in props)
                    {
                        var propName = excludeProps.Where(a => a == item.Name.ToLower()).FirstOrDefault();
                        if (string.IsNullOrEmpty(propName))
                            propList.Add(item);
                    }
                }
                else //Default
                {
                    propList.AddRange(props.ToList());
                }
                return propList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetSimpleTypeName<T>(IList<T> list)
        {
            try
            {
                string typeName = list.GetType().ToString();
                int pos = typeName.IndexOf("[") + 1;
                typeName = typeName.Substring(pos, typeName.LastIndexOf("]") - pos);
                typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
                return typeName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static  bool DatatableToExcel(DataTable dt,string path)
        {
             
 

           

            //open file
            StreamWriter wr = new StreamWriter(path);

            try
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                }

                wr.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (dt.Rows.Count); i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    //go to next line
                    wr.WriteLine();
                }
                //close file
                wr.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

    }
}



