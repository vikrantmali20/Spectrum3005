Imports SpectrumCommon
Imports System.Collections
Public Interface IPettyCashVoucher
    Function GetVoucherDetails(ByVal request As GetVoucherEntryRequest) As GetVoucherEntryResponse

    Function SaveVoucherData(ByVal request As SaveVoucherRequest) As Boolean

    Function GetAllVoucher(Optional ByVal noOfRecords As String = "") As ViewVoucherResponse

    Function GetSiteInfo(ByVal siteCode As String) As DataTable

    Function GetPettyCashBalanceAmount(ByVal request As GetVoucherBalanceRequest) As Decimal

    Function GetTotalPettyCashExpense(ByVal request As GetVoucherBalanceRequest) As Decimal

    Function GetTotalPettyCashReceipt(ByVal request As GetVoucherBalanceRequest) As Decimal

    Function GetPettyCashOpeningBalance(ByVal request As GetVoucherBalanceRequest) As Decimal

    Function CheckIfPettyCashTerminalIsOpen(ByVal request As ValidationRequest) As Boolean

    Function GetTotalPettyCashExpenseShiftWise(ByVal request As GetVoucherBalanceRequest) As Decimal

    Function GetTotalPettyCashReceiptShiftWise(ByVal request As GetVoucherBalanceRequest) As Decimal
End Interface
