Imports SpectrumCommon
Imports System.ComponentModel
Imports SpectrumBL


Public Class frmDayCloseMain
    Public Delegate Sub DayClosedDelegate()
    Dim d1 As DayClosedDelegate

    Public Delegate Sub DayCloseClosingDelegate()
    Dim screenCloser As DayCloseClosingDelegate
    Dim DayCloseStokeTakeScreenData As String = String.Empty
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.      

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private _dayCloseController As DayCloseController
    Public Property DayCloseController As DayCloseController
        Get
            Return _dayCloseController
        End Get
        Set(ByVal value As DayCloseController)
            _dayCloseController = value
        End Set
    End Property

    Private Sub frmDayCloseMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.CheckForIllegalCrossThreadCalls = False
            DayCloseController = New DayCloseController()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmDayCloseMain_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(DayCloseBankDetails) Then
                If DayCloseBankDetails.FormCloseByUserBankDtl = True Then
                    writeDaycloseStokeTakeLog("manually dayclose form close by user")
                End If
            Else
                writeDaycloseStokeTakeLog("dayclose form close by Application")
            End If
        Catch ex2 As Exception
            LogException(ex2)
        End Try
        'added by sagar after day close done;data should not updated from dayclosewastage and dayclosestocktake table
        Dim objclsCommon As New clsCommon
        Dim Status As Boolean = objclsCommon.GetDayOpenOrClose(clsAdmin.DayOpenDate, clsAdmin.SiteCode)
        If Not Status Then
            If Not screenCloser Is Nothing Then
                screenCloser()
            End If
        End If
    End Sub
    Public Sub GetNextScreen()
        If DayCloseController.CurrentScreen IsNot Nothing Then
            If SaveDayCloseData(DayCloseController.CurrentScreen) = False Then
                'ShowMessage("Error in Saving Data", "Error")
                Exit Sub
            End If
        End If
        Dim screenInfo As DayCloseScreenConfig = DayCloseController.GetNextScreen()
        If screenInfo IsNot Nothing Then
            ScreenVisiblityController(screenInfo)
        End If

        If (DayCloseController.CurrentScreen IsNot Nothing) Then
            btnNext.Enabled = Not DayCloseController.IsLastScreen
            btnBack.Enabled = Not DayCloseController.IsFirstScreen
        Else
            btnNext.Enabled = False
            btnBack.Enabled = False
        End If

    End Sub

    Public Sub GetPreviousScreen()
        Dim screenInfo As DayCloseScreenConfig = DayCloseController.GetPreviousScreen()
        If screenInfo IsNot Nothing Then
            ScreenVisiblityController(screenInfo)
        End If
        btnNext.Enabled = Not DayCloseController.IsLastScreen
        btnBack.Enabled = Not DayCloseController.IsFirstScreen
    End Sub

    Private Function SaveDayCloseData(ByRef screenInfo As DayCloseScreenConfig) As Boolean
        Try
            Dim result As Boolean = False
            Select Case screenInfo.ScreenName
                Case DayCloseScreens.SubProductDetails.ToString()
                    If SubProductDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = SubProductDetails.SubProductList
                        result = SubProductDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.FinishedProductDetails.ToString()
                    If FinishedProductDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of FinishedProductDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = FinishedProductDetails.FinishedProductList
                        result = FinishedProductDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.WastageDetails.ToString()
                    If WastageDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of WastageDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = WastageDetails.WastageDetailsList
                        result = WastageDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                    End If
                Case DayCloseScreens.StockTakedetails.ToString()
                    If StockTakeDetails.IsTotalDataValid() Then
                        Dim dayCloseSaveDataReq As New DayCloseDataRequestModel(Of StockTakeDetails)
                        dayCloseSaveDataReq.DayCloseDate = clsAdmin.DayOpenDate
                        dayCloseSaveDataReq.SiteCode = clsAdmin.SiteCode
                        dayCloseSaveDataReq.UserId = clsAdmin.UserCode
                        dayCloseSaveDataReq.DayCloseData = StockTakeDetails.StockTakeList
                        result = StockTakeDetails.Instance.SaveDayCloseData(dayCloseSaveDataReq)
                        'log for stock take  code added by vipul
                        DayCloseStokeTakeScreenData = String.Empty
                        DayCloseStokeTakeScreenData = LogForStokeTake(StockTakeDetails.StockTakeList)

                        If result = True Then
                            writeDaycloseStokeTakeLog(vbNewLine + DayCloseStokeTakeScreenData + vbNewLine + "save data successfully")
                        Else
                            writeDaycloseStokeTakeLog(vbNewLine + DayCloseStokeTakeScreenData + vbNewLine + "data not save successfully")
                        End If
                    End If
            End Select
            Return result
        Catch ex As Exception
            LogException(ex)
            ShowMessage(getValueByKey("daycloseerrormsg"), getValueByKey("CLAE05"))
            Return False
        End Try
    End Function

    Private Sub ScreenVisiblityController(ByRef screenInfo As DayCloseScreenConfig)
        Try
            Select Case screenInfo.ScreenName
                Case DayCloseScreens.SubProductDetails.ToString()
                    WastageDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    SubProductDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf SubProductDetails.EnableReadOnlyMode))
                    SubProductDetails.GetDayCloseData(New DayCloseDataRequestModel(Of SpectrumCommon.SubProductDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    Me.Text = "Day Close - SubProductDetails"
                Case DayCloseScreens.FinishedProductDetails.ToString()
                    SubProductDetails.Visible = False
                    WastageDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    FinishedProductDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf FinishedProductDetails.EnableReadOnlyMode))
                    FinishedProductDetails.GetDayCloseData(New DayCloseDataRequestModel(Of FinishedProductDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    Me.Text = "Day Close - FinishedProductDetails"
                Case DayCloseScreens.WastageDetails.ToString()
                    SubProductDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    StockTakeDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    WastageDetails.Visible = True
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf WastageDetails.EnableReadOnlyMode))
                    WastageDetails.GetDayCloseData(New DayCloseDataRequestModel(Of WastageDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    screenCloser = System.Delegate.Combine(screenCloser, New DayCloseClosingDelegate(AddressOf WastageDetails.CtrlWastageDetails_Closing))
                    'added by Khusrao Adil
                    ' Jk Sprint 15
                    'Me.Text = "Day Close - WastageDetails"
                    Me.Text = "Wastage Details"
                Case DayCloseScreens.StockTakedetails.ToString()
                    SubProductDetails.Visible = False
                    FinishedProductDetails.Visible = False
                    WastageDetails.Visible = False
                    DayCloseBankDetails.Visible = False
                    StockTakeDetails.Visible = True
                    StockTakeDetails.btnRawMaterialClosing.Visible = False
                    d1 = System.Delegate.Combine(d1, New DayClosedDelegate(AddressOf StockTakeDetails.EnableReadOnlyMode))
                    StockTakeDetails.GetStockGroupDetails()
                    StockTakeDetails.GetDayCloseData(New DayCloseDataRequestModel(Of StockTakeDetails) With {.DayCloseDate = clsAdmin.DayOpenDate, .Query = screenInfo.Script, .SiteCode = clsAdmin.SiteCode})
                    screenCloser = System.Delegate.Combine(screenCloser, New DayCloseClosingDelegate(AddressOf StockTakeDetails.CtrlStockTakeDetails_Closing))
                    'added by Khusrao Adil
                    ' Jk Sprint 15
                    'Me.Text = "Day Close - StockTakeDetails"
                    Me.Text = "Closing Stock"
                    writeDaycloseStokeTakeLog("Currently User on Day Close - StockTakeDetails screen")
                Case DayCloseScreens.RawMaterialDetails.ToString()
                    GoTo BankScreen
                Case DayCloseScreens.BankDetails.ToString()
BankScreen:         If DayCloseBankDetails.CheckIfAllTerminalAreCLosed() Then
                        SubProductDetails.Visible = False
                        FinishedProductDetails.Visible = False
                        WastageDetails.Visible = False
                        StockTakeDetails.Visible = False
                        DayCloseBankDetails.Visible = True
                        DayCloseBankDetails.DayClosed = AddressOf EnableReadOnlyMode
                        DayCloseBankDetails.GetDayCloseBankDetailsData(New DayCloseBankDetailsRequest With {.DayCloseDate = clsAdmin.DayOpenDate, .FinYear = clsAdmin.Financialyear, .SiteCode = clsAdmin.SiteCode, .IsShiftManagement = clsDefaultConfiguration.ShiftManagement})
                        Me.Text = "Day Close - BankDetails"
                    Else
                        Dim screenInfo1 As DayCloseScreenConfig = DayCloseController.GetPreviousScreen()
                        If clsDefaultConfiguration.ShiftManagement Then
                            ShowMessage(getValueByKey("daycloseshiftclosemsg"), getValueByKey("CLAE04"))
                        Else
                            ShowMessage(getValueByKey("daycloseterminalclosemsg"), getValueByKey("CLAE04"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            GetNextScreen()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            GetPreviousScreen()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub EnableReadOnlyMode()
        Try
            If d1 IsNot Nothing Then
                d1()
            End If           
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        DayCloseHeader1.BackColor = Color.FromArgb(134, 134, 134)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBack.BackColor = Color.Transparent
        btnBack.BackColor = Color.FromArgb(0, 107, 163)
        btnBack.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnBack.Size = New Size(71, 30)
        btnBack.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 0
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnNext.BackColor = Color.Transparent
        btnNext.BackColor = Color.FromArgb(0, 107, 163)
        btnNext.ForeColor = Color.FromArgb(255, 255, 255)
        '     btnBack.Size = New Size(71, 30)<
        btnNext.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnNext.FlatStyle = FlatStyle.Flat
        btnNext.FlatAppearance.BorderSize = 0
        btnNext.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Function
    Private Sub writeDaycloseStokeTakeLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
End Class