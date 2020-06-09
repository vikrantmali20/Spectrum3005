Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.Text
Imports System.IO
Imports SpectrumBL
Public Class frmPCSTRReminders
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Protected controlList As New ArrayList
    Private _SONumber As String
    Public Property SONumber() As String
        Get
            Return _SONumber
        End Get
        Set(ByVal value As String)
            _SONumber = value
        End Set
    End Property

#Region "Events"
    ''' <summary>
    ''' Action Button Click Event 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs)
        Try

            SONumber = DirectCast(sender, Button).Tag
            If (MsgBox("If you raise STR now, all unsaved data of previous screen will be lost.Do you want to continue to raise STR? Click ‘No’ to stay on the previous screen", MsgBoxStyle.YesNo, "STR0001 - " & getValueByKey("CLAE04")) = MsgBoxResult.No) Then
                Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgSTRNotification_Paint(sender As Object, e As PaintEventArgs) Handles dgSTRNotification.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub


    Private Sub frmPCSTRReminders_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim dtSTRNotificationData As New DataTable
            Dim objcomm As New clsCommon
            dtSTRNotificationData = objcomm.GetSTRNotificationData(clsAdmin.SiteCode)
            If dtSTRNotificationData.Rows.Count = 0 Then
                Me.Close()
                Exit Sub
            End If
            dgSTRNotification.DataSource = dtSTRNotificationData
            GridSetting()
            For index As Integer = 1 To dgSTRNotification.Rows.Count - 1
                AddButtonControlInGrid(index)
            Next
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try

    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Function"
    ''' <summary>
    ''' Grid Setting of STR Notification Window
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GridSetting()
        Try
            dgSTRNotification.Rows.MinSize = 28
            dgSTRNotification.Cols("SaleOrderNumber").Width = 140
            dgSTRNotification.Cols("SaleOrderNumber").Caption = "SO Number"
            dgSTRNotification.Cols("SaleOrderNumber").AllowEditing = False

            dgSTRNotification.Cols("NameOnCard").Caption = "Customer Name"
            dgSTRNotification.Cols("NameOnCard").Width = 250
            dgSTRNotification.Cols("NameOnCard").AllowEditing = False

            dgSTRNotification.Cols("DeliveryDate").Caption = "Delivery Date"
            dgSTRNotification.Cols("DeliveryDate").Width = 150
            dgSTRNotification.Cols("DeliveryDate").AllowEditing = False
            dgSTRNotification.Cols("DeliveryDate").Format = "dd-mm-yyyy"

            dgSTRNotification.Cols("DeliveryTime").Caption = "Delivery Time"
            dgSTRNotification.Cols("DeliveryTime").Width = 150
            dgSTRNotification.Cols("DeliveryTime").AllowEditing = False
            dgSTRNotification.Cols("DeliveryTime").TextAlign = TextAlignEnum.CenterCenter
            dgSTRNotification.Cols("DeliveryTime").TextAlignFixed = TextAlignEnum.CenterCenter

            dgSTRNotification.Cols("action").Caption = "Action"
            dgSTRNotification.Cols("action").Width = 60

            dgSTRNotification.Cols("UpdatedOn").Visible = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub ShowChildForm(ByRef ChildForm As System.Windows.Forms.Form, Optional ByVal isdoctoparent As Boolean = False)
        gmdiclientheight = ChildForm.Height
        gmdiclientwidth = ChildForm.Width

        Try
            For Each frmS As Form In MdiChildren
                If ChildForm.Name = frmS.Name Then
                    Exit Sub
                End If
            Next
            If LoginStatus = False And ChildForm.Name <> "frmNLogin" Then
                MsgBox(getValueByKey("MDI04"), "MDI04 - " & MsgBoxStyle.Critical, AcceptButton)
                ChildForm.Close()
                Exit Sub
            End If

            ChildForm.MdiParent = MDISpectrum
            ChildForm.Text = ChildForm.Text '& m_ChildFormNumber

            If isdoctoparent = True Then
                ChildForm.Dock = DockStyle.Fill
                'Rakesh-21.08.2013:Issue-7606-->Disappears title text
                ChildForm.MaximizeBox = False
            Else
                ChildForm.StartPosition = FormStartPosition.CenterScreen
            End If

            ChildForm.StartPosition = FormStartPosition.CenterScreen
            If ChildForm.Name <> "frmNLogin" Then
                'Close all child forms of the parent.
                For Each vChildForm As Form In Me.MdiChildren
                    If ChildForm.Name <> vChildForm.Name Then
                        vChildForm.Close()
                    End If
                Next
            Else
                ChildForm.StartPosition = FormStartPosition.CenterScreen
                MDISpectrum.MenuStrip.Show()
            End If

            ChildForm.Select()
            Try
                ChildForm.Show()
            Catch ex As Exception
                LogException(ex)
                ChildForm.Close()
            End Try

        Catch ex As Exception
            LogException(ex)
            ChildForm.Close()
        End Try

    End Sub
    ''' <summary>
    ''' Function For Adding Button Control in Grid
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer) As Boolean
        Try

            Dim getColumnType As String = String.Empty
            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = dgSTRNotification.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange
            dgSTRNotification.Rows(rowIndex).HeightDisplay = 30

            Dim ButtonX As Integer = dgSTRNotification.Cols("action").WidthDisplay

            'Create some new controls
            Dim btnBrowse As New CtrlBtn()
            btnBrowse.Tag = dgSTRNotification.Rows(rowIndex)("SaleOrderNumber").ToString()
            btnBrowse.MaximumSize = New System.Drawing.Size(ButtonX, 25)
            'btnBrowse.SetRowIndex = rowIndex
            btnBrowse.Text = "Raise"
            btnBrowse.Name = "btnaction" '+ dgSTRNotification.Rows(rowIndex)("SaleOrderNumber").ToString()
            'Insert hosted control into grid
            btnBrowse.TabStop = False
            dgSTRNotification.Controls.Add(btnBrowse)

            'host them in the C1FlexGrid
            controlList.Add(New HostedControl(dgSTRNotification, btnBrowse, rowIndex, dgSTRNotification.Cols("action").Index, ButtonX, ButtonX))

            'ImagePathRowIndex = rowIndex
            assignCellStyles = dgSTRNotification.GetCellRange(rowIndex, 3)
            assignCellStyles.Style = dgSTRNotification.Styles("CellImageType")

            AddHandler btnBrowse.Click, AddressOf BrowseImagePath

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                btnBrowse.BackColor = Color.Transparent
                btnBrowse.BackColor = Color.FromArgb(0, 107, 163)
                btnBrowse.ForeColor = Color.FromArgb(255, 255, 255)
                btnBrowse.Font = New Font("Neo Sans", 8, FontStyle.Bold)
                btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                btnBrowse.FlatStyle = FlatStyle.Flat
                btnBrowse.FlatAppearance.BorderSize = 0
                btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
                btnBrowse.TextAlign = ContentAlignment.MiddleCenter
                'btnOk.Size = New Size(94, 25)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel1.BorderStyle = BorderStyle.None

        CtrlLabel2.ForeColor = Color.White
        CtrlLabel2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlLabel2.BackColor = Color.Transparent
        CtrlLabel2.BorderStyle = BorderStyle.None


        btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnOk.TextAlign = ContentAlignment.MiddleCenter
        'btnOk.Size = New Size(94, 25)

        dgSTRNotification.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgSTRNotification.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgSTRNotification.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgSTRNotification.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgSTRNotification.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSTRNotification.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSTRNotification.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSTRNotification.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgSTRNotification.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

    End Function
#End Region
End Class