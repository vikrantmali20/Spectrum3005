Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Interface IDayCloseBankDetails
    Function GetDayCloseBankDetails(ByVal request As DayCloseBankDetailsRequest, Optional ByVal AllowQtyZero As Boolean = False, Optional ByVal ClientForMail As String = "") As DayCloseBankDetailsResponse

    Function SaveBankDetailsData(ByVal request As BankDetailsSaveDataRequest, ByVal reportPath As String, Optional isUpdateStockOnDayCloseWastage As Boolean = False) As Boolean

    Function CheckIfAllTerminalAreClosed(ByVal siteCode As String) As Boolean

    Function ResetGLNoRangeObjects(obj As String, SiteCode As String) As Boolean

    Function SaveDayCloseArticleStockBalance(ByVal DayCloseDate As Date, SiteCode As String, UserCode As String) As Boolean

End Interface
