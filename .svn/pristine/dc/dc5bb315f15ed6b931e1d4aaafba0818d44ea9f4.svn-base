Public Class clsAdmin
#Region "Shared Variable"
    Shared _siteCode As String
    Shared _siteStdCode As String
    Shared _terminalID As String
    Shared _PrepStationID As String
    Shared ibranchcode As Integer
    'added by khusrao adil on 20-07-2018  for Dynamic License Duration setting
    Shared _offlinelicenseActivationDuration As Integer
    Shared strbranchname As String
    Shared iCompanyCode As Integer
    Shared strCompanyName As String
    Shared iCurrencyCode As String
    Shared strCurrencyDescription As String
    Shared strBusinessAreaCode As String
    Shared dtFromDate As Date
    Shared strFinancialyear As String
    Shared strPurchasGroup As String
    Shared iPurchasorg As Integer
    Shared iUserCode As String
    Shared strUserName As String
    Shared strLangCode As String
    Shared strLangFontName As String
    Shared dLangFontSize As Single
    Shared _ClpArticle As String

    Shared Terminal As String
    Shared _strCurrencySymbol As String = "INR"
    Shared _SqldbDateFormat As String = "dd-MMM-yyyy"
    Shared _SqldbDateTimeFormat As String = "MM/dd/yyyy hh:mm:ss tt"
    Shared _Cultureinfo As String
    Shared _CLpProgramId, _CVProgramId, _CVBaseArticle As String
    Shared _Fyear As Int32
    Shared _CreditValidDays As Int32 = 0
    Shared _DayOpen As DateTime
    Shared _ShiftName As String
    Shared _ShiftStatus As String
    Shared _DisplayShift As Boolean = False
    Shared _CashDrawerWithoutDriver As String
#End Region
#Region "Shared  Property "

    Shared Property PrepStationID() As String
        Get
            Return _PrepStationID
        End Get
        Set(ByVal value As String)
            _PrepStationID = value
        End Set
    End Property

    Public Shared Property DayOpenDate() As DateTime
        Get
            Return _DayOpen
        End Get
        Set(ByVal value As DateTime)
            _DayOpen = value
        End Set
    End Property
    Public Shared Property ClpArticle() As String
        Get
            Return _ClpArticle
        End Get
        Set(ByVal value As String)
            _ClpArticle = value
        End Set
    End Property
    Public Shared Property CreditValidDays() As Int32
        Get
            Return _CreditValidDays
        End Get
        Set(ByVal value As Int32)
            _CreditValidDays = value
        End Set
    End Property
    Shared Property CVProgram() As String
        Get
            Return _CVProgramId
        End Get
        Set(ByVal value As String)
            _CVProgramId = value
        End Set
    End Property
    Shared Property CVBaseArticle() As String
        Get
            Return _CVBaseArticle
        End Get
        Set(ByVal value As String)
            _CVBaseArticle = value
        End Set
    End Property
    Shared Property CLPProgram() As String
        Get
            Return _CLpProgramId
        End Get
        Set(ByVal value As String)
            _CLpProgramId = value
        End Set
    End Property
    Shared Property CultureInfo() As String
        Get
            Return _Cultureinfo
        End Get
        Set(ByVal value As String)
            _Cultureinfo = value
        End Set
    End Property
    Shared Property SqlDBDateFormat() As String
        Get
            Return _SqldbDateFormat
        End Get
        Set(ByVal value As String)
            _SqldbDateFormat = value
        End Set
    End Property

    Shared Property SqlDBDateTimeFormat() As String
        Get
            Return _SqldbDateTimeFormat
        End Get
        Set(ByVal value As String)
            _SqldbDateTimeFormat = value
        End Set
    End Property


    Shared Property TerminalID() As String
        Get
            Return _terminalID
        End Get
        Set(ByVal value As String)
            _terminalID = value
        End Set
    End Property
    Shared Property BranchCode() As Integer
        Get
            BranchCode = ibranchcode
        End Get
        Set(ByVal value As Integer)
            ibranchcode = value
        End Set
    End Property
    Shared Property BranchName() As String
        Get
            BranchName = strbranchname
        End Get
        Set(ByVal value As String)
            strbranchname = value
        End Set
    End Property
    Shared Property CompanyCode() As Integer
        Get
            CompanyCode = iCompanyCode
        End Get
        Set(ByVal value As Integer)
            iCompanyCode = value
        End Set
    End Property
    Shared Property CompanyName() As String
        Get
            CompanyName = strCompanyName
        End Get
        Set(ByVal value As String)
            strCompanyName = value
        End Set
    End Property
    Shared Property CurrencyCode() As String
        Get
            CurrencyCode = iCurrencyCode
        End Get
        Set(ByVal value As String)
            iCurrencyCode = value
        End Set
    End Property
    Shared Property CurrencyDescription() As String
        Get
            CurrencyDescription = strCurrencyDescription
        End Get
        Set(ByVal value As String)
            strCurrencyDescription = value
        End Set
    End Property
    Shared Property CurrencySymbol() As String
        Get
            Return _strCurrencySymbol
        End Get
        Set(ByVal value As String)
            _strCurrencySymbol = value
        End Set
    End Property
    Shared Property BusinessAreaCode() As String
        Get
            BusinessAreaCode = strBusinessAreaCode
        End Get
        Set(ByVal value As String)
            strBusinessAreaCode = value
        End Set
    End Property
    Shared Property CurrentDate() As Date
        Get
            CurrentDate = dtFromDate
        End Get
        Set(ByVal value As Date)
            dtFromDate = value
        End Set
    End Property
    Shared Property Financialyear() As String
        Get
            Financialyear = strFinancialyear
        End Get
        Set(ByVal value As String)
            strFinancialyear = value
        End Set
    End Property

    Shared Property PurchasGroup() As String
        Get
            PurchasGroup = strPurchasGroup
        End Get
        Set(ByVal value As String)
            strPurchasGroup = value
        End Set
    End Property
    Shared Property PurchaseOrg() As Integer
        Get
            PurchaseOrg = iPurchasorg
        End Get
        Set(ByVal value As Integer)
            iPurchasorg = value
        End Set
    End Property
    Shared Property UserCode() As String
        Get
            UserCode = iUserCode
        End Get
        Set(ByVal value As String)
            iUserCode = value
        End Set
    End Property
    Shared Property UserName() As String
        Get
            UserName = strUserName
        End Get
        Set(ByVal value As String)
            strUserName = value
        End Set
    End Property
    Shared Property LangCode() As String
        Get
            LangCode = strLangCode
        End Get
        Set(ByVal value As String)
            strLangCode = value
        End Set
    End Property
    Shared Property LangFontName() As String
        Get
            LangFontName = strLangFontName
        End Get
        Set(ByVal value As String)
            strLangFontName = value
        End Set
    End Property
    Shared Property LangFontSize() As Single
        Get
            LangFontSize = dLangFontSize
        End Get
        Set(ByVal value As Single)
            dLangFontSize = value
        End Set
    End Property
    Shared Property SiteCode() As String
        Get
            Return _siteCode
        End Get
        Set(ByVal value As String)
            _siteCode = value
        End Set
    End Property
    'added by khusrao adil on 20-07-2018 for Dynamic License Duration setting
    Shared Property OfflinelicenseActivationDuration() As Integer
        Get
            Return _offlinelicenseActivationDuration
        End Get
        Set(ByVal value As Integer)
            _offlinelicenseActivationDuration = value
        End Set
    End Property
    'added by khusrao adil on 02-11-2017 for jk sprint 31
    'modified by khusrao adil on 13-11-2017 for jk sprint 31
    Shared Property SiteStdCode() As String
        Get
            Return _siteStdCode
        End Get
        Set(ByVal value As String)
            _siteStdCode = value
        End Set
    End Property
    Shared _path As String
    Shared Property Articleimagepath() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property

    ''-- Added By Mahesh for check whether Till have Cash Drawer Options or Not ...
    Shared _IsCashDrawer As Boolean
    Shared Property IsCashDrawer() As Boolean
        Get
            Return _IsCashDrawer
        End Get
        Set(ByVal value As Boolean)
            _IsCashDrawer = value
        End Set
    End Property

    '----------------For Shift Management
    Public Shared Property DisplayShift() As Boolean
        Get
            Return _DisplayShift
        End Get
        Set(ByVal value As Boolean)
            _DisplayShift = value
        End Set
    End Property
    Public Shared Property ShiftName() As String
        Get
            Return _ShiftName
        End Get
        Set(ByVal value As String)
            _ShiftName = value
        End Set
    End Property
    Public Shared Property ShiftStatus() As String
        Get
            Return _ShiftStatus
        End Get
        Set(ByVal value As String)
            _ShiftStatus = value
        End Set
    End Property
    Shared Property CashDrawerWithoutDriver() As String
        Get
            Return _CashDrawerWithoutDriver
        End Get
        Set(ByVal value As String)
            _CashDrawerWithoutDriver = value
        End Set
    End Property
#End Region

    ' By khusrao adil
#Region "Health care (Vaidya Notes)"

    Shared _PatientImagePath As String
    Shared Property PatientImagePath() As String
        Get
            Return _PatientImagePath
        End Get
        Set(ByVal value As String)
            _PatientImagePath = value
        End Set
    End Property
    Shared _EmployeeID As String
    Shared Property EmployeeID() As String
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As String)
            _EmployeeID = value
        End Set
    End Property
#End Region
End Class
