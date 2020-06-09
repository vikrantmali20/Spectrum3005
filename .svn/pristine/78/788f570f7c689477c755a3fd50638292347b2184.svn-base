Imports System.Windows.Forms

''' <summary>
''' This Class is Used as Special Prompt
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
''' 
Public Class frmBigSpecialPrompt

#Region "Global Variable For Class"
    Dim Result As String
    Dim strCaption As String
    Dim _EventType As Int32
    Public callFromGiftMsg As String = ""
#End Region

#Region "Global Property's"
    ''' <summary>
    ''' Show TextBox And Hide Panel
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowTextBox() As Boolean
        Set(ByVal value As Boolean)
            pnlMessage.Visible = True
            txtValue.Visible = True
            CmdCancel.Visible = True
            cmdHold.Visible = False
            Me.AcceptButton = cmdOk
        End Set
    End Property

    Public WriteOnly Property LicenseMessage() As Boolean
        Set(ByVal value As Boolean)
            ShowMessage = value
            cmdOk.Visible = Not value
        End Set
    End Property

    ''' <summary>
    ''' Show Message Panel And Hide Text Box
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowMessage() As Boolean
        Set(ByVal value As Boolean)
            pnlMessage.Visible = value
            txtValue.Visible = False
            CmdCancel.Visible = False
            cmdHold.Visible = False
        End Set
    End Property

    Private _allowDecimal As Boolean = False

    ''' <summary>
    ''' Check for allowing decimal value in input field 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowDecimal() As Boolean
        Get
            Return _allowDecimal
        End Get
        Set(ByVal value As Boolean)
            _allowDecimal = value
        End Set
    End Property

    Private Property _allowText As Boolean = False

    ''' <summary>
    ''' Check for allowing text value in input field 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowText As Boolean
        Get
            Return _allowText
        End Get
        Set(ByVal value As Boolean)
            _allowText = value
        End Set
    End Property



    ''' <summary>
    ''' Return's TextBox Value 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetResult()
        Get
            Return Result
        End Get
    End Property
    Public ReadOnly Property getEventType() As Int32
        Get
            Return _EventType
        End Get
    End Property

    Private _ReadWeightFromCOM As Boolean
    Public Property ReadWeightFromCOM() As Boolean
        Get
            Return _ReadWeightFromCOM
        End Get
        Set(ByVal value As Boolean)
            _ReadWeightFromCOM = value
        End Set
    End Property

    Private _COMPortName As String
    Public Property COMPortName() As String
        Get
            Return _COMPortName
        End Get
        Set(ByVal value As String)
            _COMPortName = value
        End Set
    End Property

#End Region

#Region "Class Events"
    Public Sub New(ByVal caption As String)
        strCaption = Space(4) & caption
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ' BitmapRegion.CreateControlRegion(cmdOK, Global.Spectrum.My.Resources.ok)
        SetCulture(Me, "frmSpecialPrompt")

        If (caption.Equals("Enter Quantity")) Then
            txtValue.MaxLength = 9
        End If

        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private Sub frmSpecialPrompt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'txtValue.Text = String.Empty
            Me.Text = strCaption
            txtValue.Focus()
            'If txtValue.Visible = True Then
            '    cmdCancel.Visible = True
            'Else
            '    cmdCancel.Visible = False
            'End If

            If CmdCancel.Visible = True And cmdHold.Visible = True Then
                cmdOk.Text = getValueByKey("mod009")
            End If
            If ReadWeightFromCOM Then
                TryReceiveWeight()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            txtValue.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub TryReceiveWeight()
        Try
            Dim weight = COMPortRead(COMPortName)
            If weight IsNot Nothing AndAlso weight.Length >= 7 Then
                txtValue.Text = weight.Substring(0, 7)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F2
                    TryReceiveWeight()
            End Select
        End If

        Return MyBase.ProcessKeyPreview(m)
    End Function
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            If txtValue.Visible = False Then
                Me.Close()
            End If

            If callFromGiftMsg = "GiftMsg" Then
                If txtValue.Text = String.Empty Then
                    txtValue.Text = ".."
                End If

            End If

            If txtValue.Text <> String.Empty Then
                Result = txtValue.Text
                txtValue.Text = String.Empty
                Me.Close()
            End If
            _EventType = 1
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        _EventType = 2
        Me.Close()
    End Sub

    Private Sub cmdHold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHold.Click
        _EventType = 3
        Me.Close()
    End Sub

    Private Sub txtValue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtValue.KeyDown
        If AllowText = False Then
            If ((e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Or e.KeyValue = 110 Or e.KeyValue = 190) Or e.KeyValue = 188 And Not (txtValue.Text = String.Empty And (e.KeyValue = 110 Or e.KeyValue = 190)) And Not (txtValue.Text.Contains(".") And (e.KeyValue = 110 Or e.KeyValue = 190)) Then
                If AllowDecimal = False And ((e.KeyValue = 110 Or e.KeyValue = 190) Or e.KeyValue = 188) Then
                    e.SuppressKeyPress = True
                Else
                    e.SuppressKeyPress = False
                End If
            Else
                e.SuppressKeyPress = True
            End If
        End If
    End Sub
    Private Function Themechange()
        '  Me.Size = New Size(430, 238)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        CmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdCancel.BackColor = Color.Transparent
        CmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        CmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        CmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdCancel.FlatStyle = FlatStyle.Flat
        CmdCancel.FlatAppearance.BorderSize = 0
        CmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CmdCancel.Size = New Size(63, 29)

        cmdHold.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdHold.BackColor = Color.Transparent
        cmdHold.BackColor = Color.FromArgb(0, 107, 163)
        cmdHold.ForeColor = Color.FromArgb(255, 255, 255)
        cmdHold.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdHold.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdHold.FlatStyle = FlatStyle.Flat
        cmdHold.FlatAppearance.BorderSize = 0
        cmdHold.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        lblMessage.ForeColor = Color.White
        lblMessage.Font = New Font("Neo Sans", 12, FontStyle.Bold)
    End Function
End Class