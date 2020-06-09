Imports System.Windows.Forms
''' <summary>
''' This Class is Used as Special Prompt
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmSpecialPromptPC
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
            cmdCancel.Visible = False
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

    Private _isNumeric As Boolean = False

    Public Property IsNumeric() As Boolean
        Get
            Return _isNumeric
        End Get
        Set(ByVal value As Boolean)
            _isNumeric = value
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
    Private _ShowHoverBox As Boolean = False
    Public Property ShowHoverBox() As Boolean
        Get
            Return _ReadWeightFromCOM
        End Get
        Set(ByVal value As Boolean)
            _ReadWeightFromCOM = value
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

            PictureBox1.Visible = ShowHoverBox
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
            If ReadWeightFromCOM() Then
                TryReceiveWeight()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
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
    Public Function isNumericText(ByVal val As String, ByVal NumberStyle As System.Globalization.NumberStyles) As Boolean
        Dim result As Double
        Return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, result)
    End Function
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            If _isNumeric = True Then
                If txtValue.Text <> String.Empty Then
                    If isNumericText(txtValue.Text, System.Globalization.NumberStyles.Integer Or System.Globalization.NumberStyles.AllowDecimalPoint) = False Then
                        MsgBox(getValueByKey("SP0001"), , "SP0001 - " & getValueByKey("CLAE04"))
                        'ShowMessage(getValueByKey("SP0001"), "SP0001 - " & getValueByKey("CLAE04")) '  SP0001()
                        Exit Sub
                    End If
                End If
            End If
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
        ' Me.Size = New Size(430, 238)
        Me.Controls.Remove(TableLayoutPanel1)
        TableLayoutPanel1.SendToBack()

        'Me.Controls.Add(cmdOk)
        'Me.Controls.Add(CmdCancel)
        'Me.Controls.Add(cmdHold)
        'Me.Controls.Add(lblMessage)
        'Me.Controls.Add(Panel1)

        Me.BackgroundColor = Color.FromArgb(134, 134, 134)


        Me.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdCancel.Location = New System.Drawing.Point(329, 132)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.SetArticleCode = Nothing
        Me.CmdCancel.SetRowIndex = 0
        Me.CmdCancel.Size = New System.Drawing.Size(60, 40)
        Me.CmdCancel.TabIndex = 16
        ' Me.CmdCancel.Text = "Cancel"
        Me.CmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CmdCancel.UseVisualStyleBackColor = True

        Me.cmdOk.Location = New System.Drawing.Point(266, 132)
        Me.cmdHold.Location = New System.Drawing.Point(202, 132)
        Me.PictureBox1.Location = New System.Drawing.Point(316, 37)
        '
        'Panel1
        '
        Dim Panel1, Panel2 As New Panel
        Panel1.Location = New System.Drawing.Point(0, 132)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(650, 70)
        Panel1.TabIndex = 18
        Panel1.BackColor = Color.White
        '
        'Panel2
        '

        Panel2.Location = New System.Drawing.Point(12, -1)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(391, 118)
        Panel2.TabIndex = 19


        Me.Controls.Add(Me.pnlMessage)
        Me.Controls.Add(Me.CmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdHold)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Panel1)
        Me.Controls.Add(Panel2)


        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.Size = New Size(62, 40)
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        CmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdCancel.BackColor = Color.Transparent
        CmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        CmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        CmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdCancel.FlatStyle = FlatStyle.Flat
        CmdCancel.FlatAppearance.BorderSize = 0
        CmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        CmdCancel.Size = New Size(63, 40)

        cmdHold.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdHold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        cmdHold.BackColor = Color.Transparent
        cmdHold.BackColor = Color.FromArgb(0, 107, 163)
        cmdHold.ForeColor = Color.FromArgb(255, 255, 255)
        cmdHold.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdHold.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdHold.FlatStyle = FlatStyle.Flat
        cmdHold.FlatAppearance.BorderSize = 0
        cmdHold.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdHold.Size = New Size(63, 40)
        pnlMessage.MinimumSize = New Size(255, 112)
        pnlMessage.Size = New Size(255, 112)
        lblMessage.MinimumSize = New Size(255, 112)
        lblMessage.Size = New Size(255, 112)
        lblMessage.ForeColor = Color.White
        lblMessage.Font = New Font("Neo Sans", 11, FontStyle.Bold)

        cmdOk.TabIndex = 0
        CmdCancel.TabIndex = 2
        cmdHold.TabIndex = 1
        cmdOk.Focus()
    End Function
End Class