Imports System.Text
Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Public Class CtrlRbnBaseForm
    Inherits C1.Win.C1Ribbon.C1RibbonForm
   


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'Me.MdiParent = frmMainMdi
        ' Add any initialization after the InitializeComponent() call.

        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
    End Sub
    Private _IsNetWorkConnected As Boolean
    Public Property IsNetWorkConnected() As Boolean
        Get
            Return _IsNetWorkConnected
        End Get
        Set(ByVal value As Boolean)
            _IsNetWorkConnected = value
            'Dim ob As C1.Win.C1Ribbon.RibbonLabel
            'ob = C1StatusBar1.LeftPaneItems("lblUserId")
            If value = True Then
                rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap

                rbnLStatus.Text = Spectrum.My.Resources.ConnectedText

            Else
                'rbnLStatus.Text = "Disconnected"
                rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
            End If

        End Set
    End Property
    'Private _IsProgressBarStarted As Boolean
    'Public Property IsProgressBarStarted() As Boolean
    '    Get
    '        Return _IsProgressBarStarted
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsProgressBarStarted = value
    '        Timer1.Enabled = value
    '        Timer1.Start()
    '        If value = False Then
    '            RibbonProgressBar1.Value = RibbonProgressBar1.Minimum
    '            Timer1.Stop()
    '        End If
    '    End Set
    'End Property

    Private _IsDocToParent As Boolean = False
    Public Property IsDocToParent() As Boolean
        Get
            Return _IsDocToParent
        End Get
        Set(ByVal value As Boolean)
            _IsDocToParent = value
        End Set
    End Property

    Public FrmTranCode As String
    Public Property _FrmTranCode() As String
        Get
            Return FrmTranCode
        End Get
        Set(ByVal value As String)
            FrmTranCode = value
        End Set
    End Property

    Private Sub CtrlRbnBaseForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        IsNetWorkConnected = OnlineConnect
        Fnprogressbar(False)
    End Sub

    Private Sub CtrlRbnBaseForm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        IsNetWorkConnected = OnlineConnect
    End Sub

    Private Sub CtrlRbnBaseForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetCulture(Me, Me.Name)
        lblLoggedIn.Text = clsAdmin.UserName
        lblTodayDate.Text = FormatDateTime(Now, DateFormat.ShortDate)
        'lblTerminalId.Text = "Terminal Id"
        lblTerminalId.Text = getValueByKey("ctrlrbnbaseform.lblterminalid") & " " & clsAdmin.TerminalID
        lblVersion.Text = "Ver(1.1.0.9)"
        'below line is comment so that new deployment will not required reconfiguration . this change is temp
        'lblVersion.Text = "Ver(" & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() & ")"
        IsNetWorkConnected = OnlineConnect
        'rbnLStatus.Text = IIf(OnlineConnect = True, "Online", "Offline")

        ProgressIndicator1.Top = C1StatusBar1.Top
        ProgressIndicator1.Left = C1StatusBar1.Left + (C1StatusBar1.Width / 2)
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    If RibbonProgressBar1.Value < RibbonProgressBar1.Maximum Then
    '        RibbonProgressBar1.Increment(RibbonProgressBar1.Step)
    '    Else
    '        RibbonProgressBar1.Value = RibbonProgressBar1.Minimum
    '    End If
    '    ' We call DoEvents and refresh the status bar so that the drawitem event fires.
    '    RibbonProgressBar1.StatusBar.Refresh()
    'End Sub
    Public Sub Fnprogressbar(ByVal StartStop As Boolean)
        If StartStop = True Then
            ProgressIndicator1.Show()
            ProgressIndicator1.BringToFront()
            ProgressIndicator1.Start()
        Else
            ProgressIndicator1.Stop()
            ProgressIndicator1.Hide()
            ProgressIndicator1.SendToBack()
        End If
    End Sub

    Public Sub fnSetUserName()
        lblLoggedIn.Text = clsAdmin.UserName
    End Sub

End Class