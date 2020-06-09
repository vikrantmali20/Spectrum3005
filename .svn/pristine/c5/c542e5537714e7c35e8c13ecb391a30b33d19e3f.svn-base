using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using Spectrum.Models;
using C1.C1Excel;    // added by vipin on 30-03-2017
namespace Spectrum.BL
{
    /// <summary>
    /// Author - Created By Mahesh on 09072014 for export collection of lists to Excels ..
    /// </summary>
    public static class ConvertListsToExcel
    {

        public static Boolean ArticleDataExportListToExcel(ArticleDataExportModel articleDataExportModel, string FilePathName)
        {
            Boolean result = false;
            //Application ExcelApp = new Application();
            //Workbook ExcelWorkBook = null;
            //Worksheet ExcelWorkSheet = null;
            //ExcelApp.Visible = false;

            //// Get a new workbook. 
            //ExcelWorkBook = ExcelApp.Workbooks.Add(Missing.Value);
            ////ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //try
            //{
            //    IList<PurchaseDetails> listPurchaseDetails = new List<PurchaseDetails>();
            //    listPurchaseDetails = articleDataExportModel.PurchaseDetails;
            //    ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook    
            //    ExcelWorkSheet.Name = "PurchaseDetails";
            //    ListsToExcel(listPurchaseDetails, ref ExcelWorkSheet);

            //    IList<SalesDetails> listSalesDetails = new List<SalesDetails>();
            //    listSalesDetails = articleDataExportModel.SalesDetails;
            //    ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
            //    ExcelWorkSheet.Name = "SalesDetails";
            //    ListsToExcel(listSalesDetails, ref ExcelWorkSheet);

            //    IList<CharDetails> listCharDetails = new List<CharDetails>();
            //    listCharDetails = articleDataExportModel.CharDetails;
            //    ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
            //    ExcelWorkSheet.Name = "CharDetails";
            //    ListsToExcel(listCharDetails, ref ExcelWorkSheet);

            //    IList<TaxDetails> listTaxDetails = new List<TaxDetails>();
            //    listTaxDetails = articleDataExportModel.TaxDetails;
            //    ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
            //    ExcelWorkSheet.Name = "TaxDetails";
            //    ListsToExcel(listTaxDetails, ref ExcelWorkSheet);

            //    IList<ArticleDetails> listArticleDetails = new List<ArticleDetails>();
            //    listArticleDetails = articleDataExportModel.ArticleDetails;
            //    ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
            //    ExcelWorkSheet.Name = "ArticleDetails";
            //    ListsToExcel(listArticleDetails, ref  ExcelWorkSheet);

            //    ExcelWorkBook.SaveAs(FilePathName);
            //    ExcelWorkBook.Close();
            //    ExcelApp.Quit();
            //    Marshal.ReleaseComObject(ExcelWorkSheet);
            //    Marshal.ReleaseComObject(ExcelWorkBook);
            //    Marshal.ReleaseComObject(ExcelApp);
            //    result = true;

            C1XLBook ExcelWorkBook = new C1XLBook();  //vipin on 02-04-2016
            try
            {
                IList<ArticleDetails> listArticleDetails = new List<ArticleDetails>();
                listArticleDetails = articleDataExportModel.ArticleDetails;
                // ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
                XLSheet ExcelWorkSheet4 = ExcelWorkBook.Sheets[0];
                //  ExcelWorkSheet4.Name = "ArticleDetails";Article Data
                ExcelWorkSheet4.Name = "Article Data";
                ListsToExcelUsingC1(listArticleDetails, ref  ExcelWorkSheet4, "Article Data");


                IList<TaxDetails> listTaxDetails = new List<TaxDetails>();
                listTaxDetails = articleDataExportModel.TaxDetails;
                // ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
                XLSheet ExcelWorkSheet3 = ExcelWorkBook.Sheets.Add();
                ExcelWorkSheet3.Name = "Article Tax";
                ListsToExcelUsingC1(listTaxDetails, ref ExcelWorkSheet3, "Article Tax");

                IList<CharDetails> listCharDetails = new List<CharDetails>();
                listCharDetails = articleDataExportModel.CharDetails;
                // ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
                XLSheet ExcelWorkSheet2 = ExcelWorkBook.Sheets.Add();
                ExcelWorkSheet2.Name = "Article Char";
                ListsToExcelUsingC1(listCharDetails, ref ExcelWorkSheet2, "Article Char");


                IList<SalesDetails> listSalesDetails = new List<SalesDetails>();
                listSalesDetails = articleDataExportModel.SalesDetails;
                // ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook
                XLSheet ExcelWorkSheet1 = ExcelWorkBook.Sheets.Add();
                ExcelWorkSheet1.Name = "Barcode Pricing";
                ListsToExcelUsingC1(listSalesDetails, ref ExcelWorkSheet1, "Barcode Pricing");

     

                IList<PurchaseDetails> listPurchaseDetails = new List<PurchaseDetails>();
                listPurchaseDetails = articleDataExportModel.PurchaseDetails;
                //  ExcelWorkSheet = ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook    
                // Worksheet ExcelWorkSheet = ExcelWorkBook.Worksheets[1];
                XLSheet ExcelWorkSheet0 = ExcelWorkBook.Sheets.Add();
                ExcelWorkSheet0.Name = "Add. Purchase UOMs";
                ListsToExcelUsingC1(listPurchaseDetails, ref ExcelWorkSheet0,"Add. Purchase UOMs");
       

                ExcelWorkBook.Save(FilePathName);
                //   ExcelWorkBook.Close();
                //   xlApp.Quit();
                // Marshal.ReleaseComObject(ExcelWorkSheet);
                //  Marshal.ReleaseComObject(ExcelWorkBook);
                //    Marshal.ReleaseComObject(xlApp);
                result = true;


            }
            catch (Exception exHandle)
            {
                Logging.Logger.Log(exHandle, Logging.Logger.LogingLevel.Error);
                throw exHandle;
            }
            finally
            {

                foreach (Process process in Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }
            }
            return result;
        }

        public static void ListsToExcel<T>(this IList<T> list, ref Worksheet ExcelWorkSheet)
        {
            try
            {
                int row = 1; // Initialize Excel Row Start Position  = 1

                PropertyInfo[] props = typeof(T).GetProperties();
                List<PropertyInfo> propList = new List<PropertyInfo>();
                propList.AddRange(props.ToList());

                //Writing Columns Name in Excel Sheet
                for (int col = 1; col <= propList.Count  ; col++)
                {
                    ExcelWorkSheet.Cells[row, col] = propList[col-1].Name;
                }
                row = row + 1;

                //Writing Rows into Excel Sheet
                foreach (var item in list)
                {
                    int colIdx = 1;
                    // Excel row and column start positions for writing Row=1 and Col=1
                    foreach (var prop in propList)
                    {
                        ExcelWorkSheet.Cells[row, colIdx] = prop.GetValue(item, null);// list[listRow][col - 1].ToString();
                        colIdx = colIdx + 1;
                    }
                    row = row + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ListsToExcelUsingC1<T>(this IList<T> list, ref XLSheet ExcelWorkSheet,string SheetName) //vipin
        {
            try
            {
               // int row = 1; // Initialize Excel Row Start Position  = 1
                int row = 0;
                PropertyInfo[] props = typeof(T).GetProperties();
                List<PropertyInfo> propList = new List<PropertyInfo>();
                propList.AddRange(props.ToList());

                //Writing Columns Name in Excel Sheet
                for (int col = 1; col <= propList.Count; col++)
                {
                  //  ExcelWorkSheet[row, col].Value = propList[col - 1].Name;
                  //  ExcelWorkSheet[row, col-1].Value = propList[col - 1].Name; //vipin
                    ExcelWorkSheet[row, col - 1].Value = ArtilceDetailsColumnColumnChange(propList[col - 1].Name.ToString().Trim(), SheetName);

                }
                row = row + 1;

                //Writing Rows into Excel Sheet
                foreach (var item in list)
                {
                   // int colIdx = 1;
                    int colIdx = 0;    //vipin 
                    // Excel row and column start positions for writing Row=1 and Col=1
                    foreach (var prop in propList)
                    {
                        ExcelWorkSheet[row, colIdx].Value = prop.GetValue(item, null);// list[listRow][col - 1].ToString();
                        colIdx = colIdx + 1;
                    }
                    row = row + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  //ADDED BY VIPIN ON 30-03-2017
        public static String ArtilceDetailsColumnColumnChange(String ExcelColumnName, String SheetName) //vipin
        {
            string ModifiedExcelColumnName="";

           try
            {
            if(SheetName == "Article Data")
            {

                switch (ExcelColumnName.Trim())
                {
                    case "ArticleCode":

                        ModifiedExcelColumnName = "ARTICLE CODE";
                        break;

                    case "ArticleName":
                        ModifiedExcelColumnName = "ARTICLE NAME";
                        break;

                    case "ArticleShortName":
                        ModifiedExcelColumnName = "ARTICLE SHORT NAME";
                        break;

                    case "Description":                                                                     //column created by irfan on 02/11/2017
                        ModifiedExcelColumnName = "Description";
                        break;

                    case "ArticleType":
                        ModifiedExcelColumnName = "ARTICLE TYPE";
                        break;

                    case "MaterialType":
                        ModifiedExcelColumnName = "MATERIAL TYPE";
                        break;


                    case "MasterArticle":
                        ModifiedExcelColumnName = "MASTER ARTICLE";
                        break;

                    case "ParentArticleNode":                                                                    //column change by irfan on 02/11/2017
                        ModifiedExcelColumnName = "PARENT ARTICLE NODE";
                        break;

                    case "LastNode":
                        ModifiedExcelColumnName = "LAST NODE";
                        break;

                    case "ArticleHierarchy":
                        ModifiedExcelColumnName = "Article Hierarchy";
                        break;

                    case "EAN":                                                                       //column created by irfan on 02/11/2017
                        ModifiedExcelColumnName = "EAN";
                        break;

                    case "Barcode":
                        ModifiedExcelColumnName = "BARCODE";
                        break;

                    case "BarcodeType":
                        ModifiedExcelColumnName = "Barcode Type";
                        break;

                    case "DefaultBarCode":                                                               //column change by irfan on 02/11/2017
                        ModifiedExcelColumnName = "Default Barcode";
                        break;

                    case "StoreID":

                        ModifiedExcelColumnName = "STOREID";
                        break;


                    case "SupplierCode":
                        ModifiedExcelColumnName = "SUPPLIER CODE";
                        break;

                    case "DefaultSupplier":
                        ModifiedExcelColumnName = "Default Supplier";
                        break;

                    case "CostPrice":
                        ModifiedExcelColumnName = "COST PRICE";
                        break;

                    case "SellingPrice":
                        ModifiedExcelColumnName = "SELLING PRICE";
                        break;

                    case "MRP":
                        ModifiedExcelColumnName = "MRP";
                        break;

                    case "LanguageCode":
                        ModifiedExcelColumnName = "Language Code";
                        break;

                    case "Image":
                        ModifiedExcelColumnName = "Image";
                        break;

                    case "BaseUoM":
                        ModifiedExcelColumnName = "Base UoM";
                        break;

                    case "OrderUoM":
                        ModifiedExcelColumnName = "Order UoM";
                        break;

                    case "OUOMvalue":
                        ModifiedExcelColumnName = "OUOM value";
                        break;

                    case "SaleUOM":
                        ModifiedExcelColumnName = "Sale UOM";
                        break;

                    case "SaleValue":
                        ModifiedExcelColumnName = "Sale Value";
                        break;

                    case "DistributionUOM":
                        ModifiedExcelColumnName = "Distribution UOM";
                        break;

                    case "DistributionValue":
                        ModifiedExcelColumnName = "Distribution Value";
                        break;

                    case "Expiry":
                        ModifiedExcelColumnName = "Expiry";
                        break;

                    case "Saleable":
                        ModifiedExcelColumnName = "Saleable";
                        break;

                    case "Status":
                        ModifiedExcelColumnName = "Status";
                        break;

                    case "Printable":
                        ModifiedExcelColumnName = "Printable";
                        break;


                    case "MAP":
                        ModifiedExcelColumnName = "MAP";
                        break;

                    case "NetWeight":
                        ModifiedExcelColumnName = "Net Weight";
                        break;

                    case "NetWeightUOM":
                        ModifiedExcelColumnName = "Net Weight UOM";
                        break;

                    case "GrossWeight":
                        ModifiedExcelColumnName = "Gross Weight";
                        break;

                    case "GrossWeightUOM":
                        ModifiedExcelColumnName = "Gross Weight UOM";
                        break;

                    case "OpenQty":
                        ModifiedExcelColumnName = "Open Qty";
                        break;

                    case "OpenSellingPrice":
                        ModifiedExcelColumnName = "Open Selling Price";
                        break;

                    case "ReorderQty":
                        ModifiedExcelColumnName = "Reorder Qty";
                        break;

                    case "SafetyQty":
                        ModifiedExcelColumnName = "safety Qty";
                        break;

                    case "SupplierStatus":
                        ModifiedExcelColumnName = "Supplier Status";
                        break;

                    case "LegacyCode":
                        ModifiedExcelColumnName = "Legacy Code";
                        break;

                    default:
                        break;
                }
            }


            if (SheetName == "Article Tax")
            {

                switch (ExcelColumnName.Trim())
                {
                    // Article Tax

                    case "ArticleCode":
                        ModifiedExcelColumnName = "Article Code";
                        break;

                    case "ArticleName":
                        ModifiedExcelColumnName = "Article Name";
                        break;

                    case "StoreID":
                        ModifiedExcelColumnName = "StoreID";
                        break;

                    case "TaxName":
                        ModifiedExcelColumnName = "Tax Name";
                        break;

                    case "TaxCode":
                        ModifiedExcelColumnName = "Tax Code";
                        break;

                    case "Tax":
                        ModifiedExcelColumnName = "String Tax";
                        break;

                    case "Status":
                        ModifiedExcelColumnName = "Tax Status";
                        break;

                    case "SupplierCode":
                        ModifiedExcelColumnName = "Supplier Code";
                        break;
                }
            }

            if (SheetName == "Article Char")
            {
                switch (ExcelColumnName.Trim())
                {
                    case "ArticleCode":
                        ModifiedExcelColumnName = "Article Code";
                        break;

                    case "ArticleName":
                        ModifiedExcelColumnName = "Article Name";
                        break;

                    case "Barcode":
                        ModifiedExcelColumnName = "Barcode";
                        break;

                    case "Profile":
                        ModifiedExcelColumnName = "Profile";
                        break;

                    case "CharID":
                        ModifiedExcelColumnName = "Char ID";
                        break;

                    case "CharStatus":
                        ModifiedExcelColumnName = "Char Status";
                        break;
                }
            }

            if (SheetName == "Barcode Pricing")
            {
                switch (ExcelColumnName.Trim())
                {
                    case "StoreID":
                        ModifiedExcelColumnName = "StoreID";
                        break;

                    case "ArticleCode":
                        ModifiedExcelColumnName = "Article Code";
                        break;

                    case "Barcode":
                        ModifiedExcelColumnName = "Barcode";
                        break;

                    case "SellingPrice":
                        ModifiedExcelColumnName = "Selling Price";
                        break;

                    case "Status":
                        ModifiedExcelColumnName = "Status";
                        break;
                }
            }

            if (SheetName == "Add. Purchase UOMs")
            {
                switch (ExcelColumnName.Trim())
                {
                    case "ArticleCode":
                        ModifiedExcelColumnName = "Article Code";
                        break;

                    case "ArticleName":
                        ModifiedExcelColumnName = "Article Name";
                        break;

                    case "OrderUOM":
                        ModifiedExcelColumnName = "Order UOM";
                        break;

                    case "OrderValue":
                        ModifiedExcelColumnName = "Order Value";
                        break;

                    case "Status":
                        ModifiedExcelColumnName = "Status";
                        break;
                }
            }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ModifiedExcelColumnName;
        }
    }
}

