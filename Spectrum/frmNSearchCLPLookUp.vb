Imports SpectrumBL
Public Class frmNSearchCLPLookUp
    Dim objClp As New clsCLPCustomer
    Dim _CardType As String
    Dim _AddressType As String
    Dim _dtCustmInfo As DataTable
    Private OnlyAtCreatedSite As Boolean
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
        End Set
    End Property
    Public ReadOnly Property CardType() As String
        Get
            Return _CardType
        End Get

    End Property
    Public ReadOnly Property AddressType() As String
        Get
            Return _AddressType
        End Get

    End Property

    Dim _isMobileNoViewAllowed As Boolean

    Dim dvCustmInfo As DataView
    Dim dtCustmData As DataTable
    Dim vCustmType, vSiteCode, vCustCode As String
    Dim vFilterCustmInfo As String = String.Empty

    ''' <summary>
    ''' Load CLP Customer Data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSearchCLPLookUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            _isMobileNoViewAllowed = CheckAuthorisation(clsAdmin.UserCode, "CLP_VIEW")

            dtCustmData = objClp.GetCustomerInformation(vCustmType, vSiteCode, clsAdmin.CLPProgram, vCustCode, , OnlyAtCreatedSite)

            Dim dvCustmData As DataView
            If (dtCustmData.Columns.Contains("AddressType")) Then
                dvCustmData = New DataView(dtCustmData, "AddressType = '1'", "", DataViewRowState.OriginalRows)
            Else
                dvCustmData = New DataView(dtCustmData, "", "", DataViewRowState.OriginalRows)
            End If

            grdCLPCustomerList.DataSource = dvCustmData
            'Dim i As Int16
            'For i = 0 To dtCustmData.Rows.Count - 1
            '    grdCLPCustomerList
            'Next
            grdCLPCustomerList.ExtendRightColumn = True
            grdCLPCustomerList.Style.WrapText = False
            For Each col In grdCLPCustomerList.Columns
                'col.filterdropdown = True
                col.FilterEscape = ""
                'grdCLPCustomerList.HeadingStyle.ResetHorizontalAlignment()
                'grdCLPCustomerList.HeadingStyle.ResetWrapText()
                grdCLPCustomerList.HeadingStyle.WrapText = False

            Next

            GridColumnSettings()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        If vCustmType = "SO" Then
            Me.Text = getValueByKey("frmnsearchclplookupothercust")
        Else
            Me.Text = getValueByKey("frmnsearchclplookupclp")
        End If
        grdCLPCustomerList.Select()
        grdCLPCustomerList.FilterActive = True
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.BackgroundColor = Color.FromArgb(134, 134, 134)

            grdCLPCustomerList.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
            grdCLPCustomerList.Splits(0).Style.BackColor = Color.FromArgb(255, 255, 255)
            grdCLPCustomerList.Splits(0).Style.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdCLPCustomerList.Font = New Font("Neo Sans", 8, FontStyle.Regular)
            grdCLPCustomerList.Splits(0).HeadingStyle.BackColor = Color.FromArgb(177, 227, 253)
            grdCLPCustomerList.Splits(0).HighLightRowStyle.BackColor = Color.LightBlue
            grdCLPCustomerList.Splits(0).HighLightRowStyle.BackColor2 = Color.LightBlue
            grdCLPCustomerList.RowHeight = 24
            grdCLPCustomerList.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
            grdCLPCustomerList.Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdCLPCustomerList.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
            grdCLPCustomerList.Styles(5).BackColor = Color.FromArgb(242, 242, 242)
            grdCLPCustomerList.Styles(7).BackColor = Color.FromArgb(242, 242, 242)


            BtnNew.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            BtnNew.BackColor = Color.Transparent
            BtnNew.BackColor = Color.FromArgb(0, 107, 163)
            BtnNew.ForeColor = Color.FromArgb(255, 255, 255)
            BtnNew.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
            BtnNew.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            BtnNew.FlatStyle = FlatStyle.Flat
            BtnNew.FlatAppearance.BorderSize = 0
            BtnNew.TextAlign = ContentAlignment.MiddleCenter
            BtnNew.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)




            BtnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            BtnOK.BackColor = Color.Transparent
            BtnOK.BackColor = Color.FromArgb(0, 107, 163)
            BtnOK.ForeColor = Color.FromArgb(255, 255, 255)
            BtnOK.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
            BtnOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            BtnOK.FlatStyle = FlatStyle.Flat
            BtnOK.FlatAppearance.BorderSize = 0
            BtnOK.TextAlign = ContentAlignment.MiddleCenter
            BtnOK.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



            BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            BtnCancel.BackColor = Color.Transparent
            BtnCancel.BackColor = Color.FromArgb(0, 107, 163)
            BtnCancel.ForeColor = Color.FromArgb(255, 255, 255)
            BtnCancel.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
            BtnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            BtnCancel.FlatStyle = FlatStyle.Flat
            BtnCancel.FlatAppearance.BorderSize = 0
            BtnCancel.TextAlign = ContentAlignment.MiddleCenter
            BtnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        End If
    End Sub

    ''' <summary>
    ''' Set CLP Customer Information to Parrent Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Try
            If grdCLPCustomerList.RowCount > 0 Then

                vFilterCustmInfo = "CUSTOMERNO='" & grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CUSTOMERNO") & "' "
                If Not IsDBNull(grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")) Then
                    _AddressType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")
                    'Else
                    '    _AddressType = 1
                End If

                ' _AddressType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")

                If vCustmType = "CLP" Then
                    If Not IsDBNull(grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CARDTYPE")) Then
                        _CardType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CARDTYPE")
                    Else
                        _CardType = ""
                    End If
                Else
                    _CardType = ""
                End If


                dvCustmInfo = New DataView(dtCustmData, vFilterCustmInfo, "", DataViewRowState.OriginalRows)
                _dtCustmInfo = dvCustmInfo.ToTable()
                Me.Close()
            Else
                MsgBox(getValueByKey("CLPLK001"), , "CLPLK001 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Set CLP Customer Information to Parrent Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdCLPCustomerList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCLPCustomerList.DoubleClick
        Try
            BtnOK_Click(sender, e)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Application
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Try
            _dtCustmInfo = Nothing
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Public Sub New(ByVal _vCustmType As String, ByVal _vSiteCode As String, ByVal _vCustmCode As String, Optional ByVal _OnlyAtCreatedSite As Boolean = False)

        vCustmType = _vCustmType
        vSiteCode = _vSiteCode
        vCustCode = _vCustmCode
        OnlyAtCreatedSite = _OnlyAtCreatedSite
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If vCustmType = "SO" Then
            BtnNew.Visible = True
        Else
            BtnNew.Visible = False
        End If

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub BtnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.Hide()
            Me.Close()
            Dim objCreateNewCustm As New frmNSOCustomer
            objCreateNewCustm.Tag = "NEW"
            objCreateNewCustm.ShowDialog()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub GridColumnSettings()
        If grdCLPCustomerList.Columns.Count > 0 Then
            Dim displayColumns As String = "MobileNo,CustomerNo,CustomerName,FirstName,SurName,EmailID,BirthDate,BalancePoint,AddressLn1,City,State,Country"
            Dim columnsList = displayColumns.ToUpper().Split(",")

            For colIndex = 0 To grdCLPCustomerList.Splits(0).DisplayColumns.Count - 1 Step 1
                If columnsList.Contains(grdCLPCustomerList.Splits(0).DisplayColumns(colIndex).Name.ToUpper()) Then
                    grdCLPCustomerList.Splits(0).DisplayColumns(colIndex).Visible = True
                Else
                    grdCLPCustomerList.Splits(0).DisplayColumns(colIndex).Visible = False
                End If
            Next

            grdCLPCustomerList.Splits(0).DisplayColumns("MobileNo").Visible = _isMobileNoViewAllowed
            grdCLPCustomerList.Splits(0).DisplayColumns("MobileNo").DataColumn.Caption = "Mobile Number"

            grdCLPCustomerList.Splits(0).DisplayColumns("CustomerNo").DataColumn.Caption = "Customer Number"
            grdCLPCustomerList.Splits(0).DisplayColumns("CustomerName").DataColumn.Caption = "Customer Name"
            grdCLPCustomerList.Splits(0).DisplayColumns("FirstName").DataColumn.Caption = "First Name"
            grdCLPCustomerList.Splits(0).DisplayColumns("SurName").DataColumn.Caption = "Last Name"
            grdCLPCustomerList.Splits(0).DisplayColumns("EmailID").DataColumn.Caption = "Email ID"
            grdCLPCustomerList.Splits(0).DisplayColumns("BirthDate").DataColumn.Caption = "DOB"
            grdCLPCustomerList.Splits(0).DisplayColumns("AddressLn1").DataColumn.Caption = "Address Line 1"
            grdCLPCustomerList.Splits(0).DisplayColumns("City").DataColumn.Caption = "City"
            grdCLPCustomerList.Splits(0).DisplayColumns("State").DataColumn.Caption = "State"
            grdCLPCustomerList.Splits(0).DisplayColumns("Country").DataColumn.Caption = "Country"

            If (dtCustmData.Columns("BalancePoint") IsNot Nothing) Then
                grdCLPCustomerList.Splits(0).DisplayColumns("BalancePoint").DataColumn.Caption = "Balance Points"
            End If

            For colIndex1 = 0 To grdCLPCustomerList.Splits(0).DisplayColumns.Count - 1 Step 1
                grdCLPCustomerList.Splits(0).DisplayColumns(colIndex1).AutoSize()
            Next
 
        End If
    End Sub

End Class
