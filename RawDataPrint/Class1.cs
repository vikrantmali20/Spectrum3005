using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Text;
namespace RawDataPrint
{
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        private static string autoCutCommond = Convert.ToString((char)27) + "@" + Convert.ToString((char)29) + "V" + (char)1;
        private static string feedCommond = Convert.ToChar(29) + "V" + Convert.ToChar(65) + Convert.ToChar(0);
        private static string drawerCommond = Convert.ToString((char)27) + "p" + Convert.ToString((char)00) + Convert.ToString((char)64) + Convert.ToString((char)64);

        public static bool PrintInvoiceWithAutocutAndFeedPaper(string printerName, string printString,bool feedAndCut)
        {
            SendStringToPrinter(printerName, printString);
            if (feedAndCut)
            {
                SendStringToPrinter(printerName, autoCutCommond);
                //SendStringToPrinter(printerName, feedCommond);
                SendStringToPrinter(printerName, drawerCommond); // for cash drawer added by sagar''natural issue
            }

            return true;
        }

        //added on 12 may - ashma - for Innoviti
        [DllImport("innovEFT.dll")]
        public static extern int innovEFT_GUI(int TRANSACTION_MODE, int TRANSACTION_TYPE, string REQUEST_PARAMETERS, byte[] RESPONSE_PARAMETER);
        public static Dictionary<string, string> PaymentThroughEDC(string invoiceNumber, int amount, string posReferenceNumber, string transactionTime)
        {
            Dictionary<string, string> str = new Dictionary<string, string>();
            byte[] responseXML = new byte[15000];
            string requestXML = "<purchase-request> <TransactionInput ID=\"" + invoiceNumber + "\"><Card> <IsManualEntry>true</IsManualEntry><CardNumber>1111111111111111</CardNumber><ExpirationDate> <MM>00</MM> <YY>00</YY> </ExpirationDate></Card> <Amount> <BaseAmount>" + amount + "</BaseAmount> <discount>00</discount>       <CurrencyCode>INR</CurrencyCode><Amnt>" + amount + "</Amnt></Amount> <POS> <POSReferenceNumber>" + posReferenceNumber + "</POSReferenceNumber> <TransactionTime>" + transactionTime + "</TransactionTime></POS> <TrackingNumber>000</TrackingNumber></TransactionInput> </purchase-request>";
            int retVal = innovEFT_GUI(int.Parse("0"), int.Parse("0"), requestXML, responseXML);

            string response = Encoding.UTF8.GetString(responseXML, 0, 15000);
            XmlTextReader reader = new XmlTextReader(new StringReader(response));

            while (reader.Read())
            {

                switch (reader.Name.ToString())
                {

                    case "StatusCode":

                        str.Add("StatusCode", reader.ReadString());
                        break;
                    case "StatusMessage":

                        str.Add("StatusMessage", reader.ReadString());
                        break;

                    case "TransactionTime":

                        str.Add("TransactionTime", reader.ReadString());
                        break;

                    case "RetrievalRefrenceNumber":

                        str.Add("RetrievalReferenceNumber", reader.ReadString());
                        break;
                    case "CardNumber":  // added by ashma on 18-05-2017
                        string cardNo = reader.ReadString();
                        if (cardNo != "")
                        {
                            cardNo = cardNo.Substring(cardNo.Length - 4);
                            str.Add("CardNumber", cardNo);
                        }
                        break;

                }


            }
            return str;
        }
    }
}
