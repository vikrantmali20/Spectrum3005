Imports SpectrumBL
Public Class frmNAdvanceSale
    Dim FormNormalHeight, SpliterDistance As Double
    Dim Article, StrFilter As String
    Dim _dtTemp, _dtGv, dtGVDet, dtGVDeno As DataTable
    Dim dvGV As DataView
    Dim AnyDenomination As Boolean = False
    Dim _city, _cardType, _saleType As String
    Public _incrementNo As Int32
    Dim _TransactionID As TransactionTypes
    Private _BirthListID As String
    Private _TotalDenomination As Decimal
    Private _IsBirthListGV As Boolean
    Private _IsFormCanceled As Boolean = True


    ''' <summary>
    '''  Used by "TransactionID" property. 
    ''' </summary>
    '''<UsedBy>frmBirthListSales.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks>"TransactionTypes" defined into comFunctionModule </remarks>    
    ''' TODO: Required whenever we call this form by BirthlistSales.    
    ''' <returns></returns>
    ''' 
    Public Property TransactionID() As String
        Get
            Return _TransactionID.ToString()
        End Get
        Set(ByVal value As String)
            _TransactionID = value
        End Set
    End Property
    ''' <summary>
    ''' TODO: Total Amount for vouchers.
    ''' </summary>
    ''' <UsedBy>frmBirthListUpdate.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks>Set for only BirtListUpdate screen.</remarks>
    Public Property TotalDenomination() As Decimal
        Get
            _TotalDenomination = Math.Round(_TotalDenomination, 2)
            Return _TotalDenomination
        End Get
        Set(ByVal value As Decimal)
            _TotalDenomination = value
        End Set
    End Property
    ''' <summary>
    ''' TODO: Check For BirthList
    ''' </summary>
    ''' <UsedBy>frmBirthListUpdate.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks>Set for only BirtListUpdate screen.</remarks>
    Public Property IsBirthListGV() As Boolean
        Get
            Return _IsBirthListGV
        End Get
        Set(ByVal value As Boolean)
            _IsBirthListGV = value
        End Set
    End Property
    ''' <summary>
    ''' Is Form forcefully closed or transaction canceled
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsFormCanceled() As Boolean
        Get
            Return _IsFormCanceled
        End Get
        Set(ByVal value As Boolean)
            _IsFormCanceled = value
        End Set
    End Property
    ''' <summary>
    ''' BirthList ID 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BirthListID() As String
        Get
            Return _BirthListID
        End Get
        Set(ByVal value As String)
            _BirthListID = value
        End Set
    End Property
    Public WriteOnly Property SaleType() As String
        Set(ByVal value As String)
            _saleType = value
        End Set
    End Property
    Public Property DataSource() As DataTable
        Get
            Return _dtTemp
        End Get
        Set(ByVal value As DataTable)
            _dtTemp = value
        End Set
    End Property
    Public Property GVDetail() As DataTable
        Get
            Return _dtGv
        End Get
        Set(ByVal value As DataTable)
            _dtGv = value
        End Set
    End Property
    Public Property CLPCity() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property
    Public Property ClpCard() As String
        Get
            Return _cardType
        End Get
        Set(ByVal value As String)
            _cardType = value
        End Set
    End Property
    Private Function CheckIfValidGiftVoucher() As Boolean
        If dtGVDet Is Nothing Or dtGVDet.Rows.Count = 0 Or (clsDefaultConfiguration.AllowNonPrePrintedGV = False AndAlso clsDefaultConfiguration.AllowPrePrintedGV = False) Then
            ShowMessage("Please Enter Proper Gift Voucher Details", "Information")
            Me.Close()
            Return False
        End If
        Return True
    End Function
    Private Sub frmAdvanceSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FormNormalHeight = Me.Height
            'SpliterDistance = SplitContainer1.SplitterDistance
            'rbGiftVoucher.Checked = True
            SetformLayout(1)
            Dim obj As New clsAdvanceSale()
            dtGVDet = obj.GetVoucherProg(clsAdmin.SiteCode, NegativeTenderType.GiftVoucherI.ToString())
            If Not CheckIfValidGiftVoucher() Then
                Exit Sub
            End If
            dtGVDeno = obj.GetVoucherDenom()
            dvGV = New DataView(dtGVDet, StrFilter, "VOUCHERCODE", DataViewRowState.CurrentRows)
            If Not dtGVDet Is Nothing Then
                cmbVoucherProgram.DataSource = dvGV
                cmbVoucherProgram.DisplayMember = "VOUCHERDESC"
                cmbVoucherProgram.ValueMember = "VOUCHERCODE"
                cmbVoucherProgram.SelectedIndex = 0
                pC1ComboSetDisplayMember(cmbVoucherProgram)
            End If
            If Not _dtGv Is Nothing Then
                dgMainGrid.DataSource = _dtGv
                SetGrid()
                '_dtGv.Clear()
            End If

            'If clsDefaultConfiguration.CLPPointSaleAllowed = False Then
            '    rbClp.Enabled = False
            '    rbClp.Checked = False
            'Else
            '    rbClp.Enabled = True
            'End If
            'If clsDefaultConfiguration.GVsaleAllowed = False Then
            '    rbGiftVoucher.Enabled = False
            '    rbGiftVoucher.Checked = False
            'Else
            '    rbGiftVoucher.Enabled = True
            'End If

            'Added by rahul
            'start  
            'If IsBirthListGV Then
            '    If clsDefaultConfiguration.BLGVSale = False Then
            '        rbGiftVoucher.Enabled = False
            '        rbGiftVoucher.Checked = False
            '    Else
            '        rbClp.Checked = True
            '        rbClp.Enabled = False
            '        rbGiftVoucher.Enabled = True
            '        rbGiftVoucher.Checked = True
            '        cmdOk.Visible = False
            '        SetformLayout(2)
            '        rbNonPrePrinted.Enabled = True
            '        rbNonPrePrinted.Checked = True
            '        rbPrePrinted.Enabled = False
            '    End If
            'End If
            'End
            If _saleType = "GV" Then
                'rbGiftVoucher.Checked = True
                'cmdOk_Click(sender, e)
                SetformLayout(2)
                If clsDefaultConfiguration.AllowPrePrintedGV AndAlso clsDefaultConfiguration.AllowNonPrePrintedGV Then
                    'rbNonPrePrinted.Visible = False
                    rbPrePrinted.Checked = True
                    txtVoucher.Select()
                ElseIf clsDefaultConfiguration.AllowPrePrintedGV Then
                    rbNonPrePrinted.Visible = False
                    rbPrePrinted.Checked = True
                    txtVoucher.Select()
                ElseIf clsDefaultConfiguration.AllowNonPrePrintedGV Then
                    rbPrePrinted.Visible = False
                    rbNonPrePrinted.Checked = True
                    txtDenomination.Select()
                End If
            ElseIf _saleType = "CLP" Then
                'rbClp.Checked = True
                'cmdOk_Click(sender, e)
                SetformLayout(3)
            End If
            dgMainGrid.AllowEditing = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        '------ Apply Theme here 
        If clsDefaultConfiguration.ThemeSelect <> "Default" Then
            Select Case clsDefaultConfiguration.ThemeSelect
                Case "Theme 1"
                    Call ThemeChange()
                Case 2

                Case Else

            End Select
        End If
    End Sub

    Private Sub ThemeChange()
        Try

            Dim Lblbackcolor As Color = Color.FromArgb(134, 134, 134)
            Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)
            If _saleType = "CLP" Then
                sizCLP.BackColor = Color.FromArgb(134, 134, 134)
                tplCLP.BackColor = Color.FromArgb(134, 134, 134)
                cmdClpSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
                cmdClpSearch.Image = Global.Spectrum.My.Resources.Resources.CustomerSearch_Normal
                cmdClpSearch.Text = String.Empty

                With cmdClpOk
                    .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                    .BackColor = Color.Transparent
                    .BackColor = Color.FromArgb(0, 81, 120)
                    .ForeColor = Color.FromArgb(255, 255, 255)
                    .Size = New Size(71, 30)
                    .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                    .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

                End With

                With lblCLpMessage
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = New Font("Neo Sans", 13, FontStyle.Regular)
                    .BackColor = Color.Transparent
                    .ForeColor = Color.White
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    .Text = "LOYALITY POINTS"
                End With
                With lblSwipeCard
                    .Margin = New Padding(3, 0, 0, 0)
                    .Dock = DockStyle.Top
                    .AutoSize = False
                    .Font = lblFont

                    .Size = New Size(txtSwipeCard.Left - .Left, txtSwipeCard.Height)
                    .Top = txtSwipeCard.Top
                    .BackColor = Color.FromArgb(212, 212, 212)

                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtSwipeCard.Margin = New Padding(0)

                End With
                With lblName
                    .Margin = New Padding(3, 0, 0, 0)
                    .Dock = DockStyle.Top
                    .Font = lblFont
                    .Size = New Size(txtName.Left - .Left, txtName.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtName.Margin = New Padding(0)
                End With

                With lblClpNumber
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtClpNumber.Left - .Left, txtClpNumber.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtClpNumber.Margin = New Padding(0)
                End With
                With lblAddress
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtAddress.Left - .Left, lblAddress.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtAddress.Margin = New Padding(0)
                End With
                With lblPoints
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtPoints.Left - .Left, txtPoints.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtPoints.Margin = New Padding(0)
                End With
                With lblMobileNo
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtMobileno.Left - .Left, txtMobileno.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtMobileno.Margin = New Padding(0)
                End With
                With lblEmail
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtEmail.Left - .Left, txtEmail.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtEmail.Margin = New Padding(0)
                End With
                With lblBalancePoint
                    .Margin = New Padding(3, 0, 0, 0)
                    .Font = lblFont
                    .Dock = DockStyle.Top
                    .Size = New Size(txtBalPoint.Left - .Left, txtBalPoint.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                    txtBalPoint.Margin = New Padding(0)
                End With

            ElseIf _saleType = "GV" Then
                Me.Size = New Size(550, 414)
                sizGiftVoucher.BackColor = Color.FromArgb(134, 134, 134)
                rbNonPrePrinted.BackColor = Color.FromArgb(134, 134, 134)
                rbNonPrePrinted.Font = lblFont
                rbPrePrinted.Font = lblFont
                rbPrePrinted.BackColor = Color.FromArgb(134, 134, 134)
                rbNonPrePrinted.ForeColor = Color.White
                rbPrePrinted.ForeColor = Color.White
                With lblVoucherNo
                    .Font = lblFont
                    '  .Size = New Size(txtVoucher.Left - .Left, txtVoucher.Height)
                    .BackColor = Color.Transparent
                    .ForeColor = Color.White
                    ' .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                End With
                With lblVoucherProgram
                    .AutoSize = False
                    .Font = lblFont
                    .Size = New Size(202, 21)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    '  .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                End With
                With lblDenomination
                    .Font = lblFont
                    '.Size = New Size(txt.Left - .Left, txtVoucher.Height)
                    .BackColor = Color.Transparent
                    .ForeColor = Color.White
                    '  .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                End With
                With lblQtyRequired
                    .Font = lblFont
                    '.Size = New Size(txt.Left - .Left, txtVoucher.Height)
                    .BackColor = Color.FromArgb(212, 212, 212)
                    '  .AutoSize = False
                    .TextAlign = ContentAlignment.MiddleLeft
                    .BorderStyle = BorderStyle.None
                End With

                With dgMainGrid
                    .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                    .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
                    .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
                    .Rows.MinSize = 25
                    .Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
                    .Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                    .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                    .Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                    .Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                    .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
                End With

                With cmdVoucherOk
                    .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                    .BackColor = Color.Transparent
                    .BackColor = Color.FromArgb(0, 81, 120)
                    .ForeColor = Color.FromArgb(255, 255, 255)
                    .Size = New Size(71, 30)
                    .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                    .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
                    .TextAlign = ContentAlignment.MiddleCenter
                End With
                With cmdAdd
                    .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                    .BackColor = Color.Transparent
                    .BackColor = Color.FromArgb(0, 81, 120)
                    .ForeColor = Color.FromArgb(255, 255, 255)
                    .Size = New Size(71, 25)
                    .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                    .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                    .FlatStyle = FlatStyle.Flat
                    .FlatAppearance.BorderSize = 0
                    .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
                    .TextAlign = ContentAlignment.MiddleCenter
                End With

            End If



        Catch ex As Exception

        End Try
    End Sub


    Private Sub SetformLayout(ByVal Level As Integer)
        If Level = 1 Then
            'Dim i As Integer
            'i = SplitContainer1.Panel2.Height
            'i = FormNormalHeight - i
            'Me.Height = i
            'SplitContainer1.Panel2Collapsed = True
        ElseIf Level = 2 Then
            'Me.Height = FormNormalHeight
            'SplitContainer1.Panel2Collapsed = False
            'SplitContainer1.SplitterDistance = SpliterDistance
            sizCLP.Visible = False
            sizGiftVoucher.Visible = True
        ElseIf Level = 3 Then
            'Me.Height = FormNormalHeight
            'SplitContainer1.Panel2Collapsed = False
            'SplitContainer1.SplitterDistance = SpliterDistance
            sizCLP.Visible = True
            sizGiftVoucher.Visible = False
        End If
    End Sub
    Private Sub cmdClpSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClpSearch.Click
        Try
            Dim objCust As New frmNSearchCLPLookUp("CLP", clsAdmin.SiteCode, "")
            objCust.ShowDialog()
            Dim dtCust As DataTable = objCust.dtCustmInfo()
            Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                Dim dv As New DataView(dtCust, "isnull(AddressType,'')='" & type & "'", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    txtClpNumber.Text = dv.Item(0)("CUSTOMERNO")
                    txtName.Text = dv.Item(0)("CustomerName").ToString()
                    txtAddress.Text = dv.Item(0)("Address").ToString()
                    txtBalPoint.Text = dv.Item(0)("BalancePoint").ToString()
                    txtEmail.Text = dv.Item(0)("EMAILID").ToString()
                    txtMobileno.Text = dv.Item(0)("MOBILENO").ToString()
                    _city = dv.Item(0)("City").ToString()
                    _cardType = dv.Item(0)("CardType").ToString()
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

    End Sub
    Public Sub New(ByVal TransactionTypes As String)
        InitializeComponent()
        IsBirthListGV = True
    End Sub
    Private Sub txtClpNumber_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClpNumber.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim objClpCustm As New clsCLPCustomer
                Dim dtTemp As DataTable = objClpCustm.ValidateClpNo(txtClpNumber.Text.Trim, clsAdmin.SiteCode, clsAdmin.CLPProgram)
                If Not dtTemp Is Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    Dim Add As String = dtTemp.Rows(0)("AddressLn1")
                    Add = Add & ", " & dtTemp.Rows(0)("AddressLn2")
                    Add = Add & ", " & dtTemp.Rows(0)("AddressLn3")
                    Add = Add & ", " & dtTemp.Rows(0)("AddressLn4")
                    Add = Add & ",City- " & dtTemp.Rows(0)("City")
                    Add = Add & ",State- " & dtTemp.Rows(0)("State")
                    Add = Add & ",Country- " & dtTemp.Rows(0)("Country")
                    Add = Add & ",Pin- " & dtTemp.Rows(0)("PinCode")

                    txtClpNumber.Text = dtTemp.Rows(0)("CARDNO")
                    txtBalPoint.Text = dtTemp.Rows(0)("BalancePoint")
                    txtAddress.Text = Add
                    txtEmail.Text = dtTemp.Rows(0)("EMAILID")
                    txtMobileno.Text = dtTemp.Rows(0)("Mobileno")
                    txtName.Text = dtTemp.Rows(0)("CUSTOMERNAME")
                    txtClpNumber.ReadOnly = True
                End If

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If rbGiftVoucher.Checked = True Then
        '    SetformLayout(2)
        '    rbPrePrinted.Checked = True
        'ElseIf rbClp.Checked = True Then
        '    SetformLayout(3)
        'End If
    End Sub
    Private Sub cmdClpOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClpOk.Click
        Try
            If txtClpNumber.Text.Trim = String.Empty Then
                ShowMessage(getValueByKey("ADS001"), "ADS001 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select Clp Customer First", "Information")
                Exit Sub
            End If
            If txtPoints.Text.Trim = String.Empty Then
                ShowMessage(getValueByKey("ADS002"), "ADS002 - " & getValueByKey("CLAE04"))
                'ShowMessage("please enter Points to Purchased", "Information")
                Exit Sub
            End If
            If clsAdmin.ClpArticle = String.Empty Then
                ShowMessage(getValueByKey("ADS011"), "ADS011 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If (TransactionID = TransactionTypes.BirthListSales.ToString()) Then
                AddClpPointForSale_BirthList()
            Else
                AddClpPointforSale()
            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub AddClpPointforSale()
        Try
            Dim ObjArt As New clsIteamSearch
            Dim dt As DataTable = ObjArt.GetItemData(clsAdmin.ClpArticle, clsAdmin.LangCode)
            If dt.Rows.Count <= 0 Then
                ShowMessage(getValueByKey("ADS011"), "ADS011 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim dr As DataRow = _dtTemp.NewRow
            dr("Btype") = "S"
            dr("Quantity") = txtPoints.Text
            dr("SellingPrice") = 1
            dr("EAN") = dt.Rows(0)("EAN")
            dr("ArticleCode") = dt.Rows(0)("ArticleCode")
            dr("Discription") = dt.Rows(0)("DISCRIPTION")
            dr("UnitofMeasure") = dt.Rows(0)("UnitofMeasure")
            dr("PROMOTIONID") = "NO"
            dr("MANUALPROMO") = "NO"
            dr("FIRSTLEVEL") = "NO"
            dr("TOPLEVEL") = "NO"
            dr("CLPRequire") = True
            dr("BillLineNo") = 1

            _dtTemp.Rows.Add(dr)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Adding GV to Table , when we    select CLP
    ''' </summary>
    ''' <UsedBy>frmBirthListSales.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks></remarks>
    Private Sub AddClpPointForSale_BirthList()
        Try
            Dim objclsComman As New clsCommon
            Dim ObjArt As New clsIteamSearch
            Dim dt As DataTable = ObjArt.GetItemData(clsAdmin.ClpArticle)
            Dim dr As DataRow = _dtTemp.NewRow
            dr("SiteCode") = clsAdmin.SiteCode
            dr("BirthListId") = _dtTemp.Rows(0)("BirthListId")
            dr("CreatedAt") = clsAdmin.SiteCode
            dr("CreatedBy") = clsAdmin.UserName
            dr("CreatedOn") = objclsComman.GetCurrentDate()
            dr("UpdatedAt") = clsAdmin.SiteCode
            dr("UpdatedBy") = clsAdmin.UserName
            dr("UpdatedOn") = objclsComman.GetCurrentDate()
            dr("Status") = True
            dr("BOOKEDQTY") = 0
            dr("RequestedQty") = txtPoints.Text
            dr("SellingPrice") = 1
            dr("EAN") = dt.Rows(0)("EAN")
            dr("ArticleCode") = dt.Rows(0)("ArticleCode")
            dr("Discription") = dt.Rows(0)("DISCRIPTION")
            dr("CLPRequire") = True
            dr("BillLineNo") = 1

            'dr("UnitofMeasure") = dt.Rows(0)("UnitofMeasure")
            _dtTemp.Rows.Add(dr)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Adding GV to Table , when we not select CLP
    ''' </summary>
    ''' <UsedBy>frmBirthListSales.vb</UsedBy>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <remarks></remarks>
    Private Sub AddGVForSale_BirthList()
        Try
            Dim ObjArt As New clsIteamSearch
            If Not _dtGv Is Nothing Then
                For Each drGV As DataRow In _dtGv.Rows
                    For Each dr As DataRow In dtGVDet.Select("VOUCHERCODE='" & drGV("VOUCHERCODE").ToString() & "'", "Articlecode", DataViewRowState.CurrentRows)
                        Article = dr("ArticleCode").ToString()
                    Next
                    Dim dv As New DataView(_dtTemp, "ArticleCode='" & Article & "'", "", DataViewRowState.CurrentRows)
                    If dv.Count > 0 Then
                        dv.AllowEdit = True
                        For Each dr As DataRowView In dv
                            dr("RequstedQty") = dr("RequstedQty") + drGV("Quantity")
                            dr("NetAmount") = dr("NetAmount") + drGV("NetAmount")
                            dr("SellingPrice") = dr("NetAmount") / dr("RequstedQty")
                            dr("BOOKEDQTY") = dr("RequstedQty")
                        Next
                    Else
                        Dim objclsComman As New clsCommon
                        Dim dt As DataTable = ObjArt.GetItemData(Article, clsAdmin.LangCode)
                        Dim dr As DataRow = _dtTemp.NewRow
                        dr("SiteCode") = clsAdmin.SiteCode
                        dr("BirthListId") = BirthListID
                        'dr("CreatedAt") = clsAdmin.SiteCode
                        'dr("CreatedBy") = clsAdmin.UserName
                        'dr("CreatedOn") = objclsComman.GetCurrentDate()
                        'dr("UpdatedAt") = clsAdmin.SiteCode
                        'dr("UpdatedBy") = clsAdmin.UserName
                        'dr("UpdatedOn") = objclsComman.GetCurrentDate()
                        dr("Status") = True
                        dr("NetAmount") = drGV("NetAmount")
                        dr("SellingPrice") = drGV("VALUEOFVOUCHER").ToString()
                        dr("EAN") = dt.Rows(0)("EAN")
                        dr("ArticleCode") = dt.Rows(0)("ArticleCode")
                        dr("Discription") = dt.Rows(0)("DISCRIPTION")
                        'dr("ConvToVoucher") = 0
                        'dr("ReservedQty") = 0
                        dr("CLPRequire") = True
                        dr("BillLineNo") = 1

                        If Not drGV("ISPREPRINTED") Is DBNull.Value AndAlso drGV("ISPREPRINTED") = True Then
                            dr("RequstedQty") = 1
                        Else
                            dr("RequstedQty") = drGV("Quantity")
                        End If
                        dr("BOOKEDQTY") = dr("RequstedQty")
                        _dtTemp.Rows.Add(dr)
                    End If
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub rbPrePrinted_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPrePrinted.CheckedChanged
        Try
            lblVoucherNo.Visible = True
            lblQtyRequired.Visible = False
            If lblQtyRequired.Visible = True Then
                txtVoucher.Text = 1
            Else
                txtVoucher.Text = String.Empty
            End If
            If rbPrePrinted.Checked = True Then
                StrFilter = "ISPREPRINTED=True"
                If Not dvGV Is Nothing Then
                    dvGV.RowFilter = StrFilter
                    cmbVoucherProgram.SelectedIndex = 0
                End If

            End If
            txtVoucher.Select()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub rbNonPrePrinted_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbNonPrePrinted.CheckedChanged
        Try
            lblVoucherNo.Visible = False
            lblQtyRequired.Visible = True
            If lblQtyRequired.Visible = True Then
                txtVoucher.Text = 1
            Else
                txtVoucher.Text = String.Empty
            End If
            If rbNonPrePrinted.Checked = True Then
                StrFilter = "ISPREPRINTED=False"
                If Not dvGV Is Nothing Then
                    dvGV.RowFilter = StrFilter
                    cmbVoucherProgram.SelectedIndex = 0
                End If

            End If
            txtDenomination.Select()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            'Added BY Rahul
            'start
            If IsBirthListGV Then
                If Not (checkDenomination()) Then

                    Dim strMsg As String = String.Format(getValueByKey("ADS003"), clsAdmin.CurrencySymbol, TotalDenomination)

                    'Dim strMsg As String = String.Format("You can not take G.V. more than  {0}{1}", clsAdmin.CurrencySymbol, TotalDenomination)
                    ShowMessage(strMsg, "ADS003 - " & getValueByKey("CLAE04"))
                    Exit Sub

                End If
            End If
            'end

            If AnyDenomination = True Then
                If txtDenomination.Text.Trim = String.Empty Then
                    ShowMessage(getValueByKey("ADS004"), "ADS004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Enter the Denomination of Voucher", "Information")
                    Exit Sub
                End If
            Else
                If cmbDenomination.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("ADS004"), "ADS004 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            End If

            If txtVoucher.Text.Trim = String.Empty Then
                If rbPrePrinted.Checked = True Then
                    ShowMessage(getValueByKey("ADS005"), "ADS005 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please enter the Voucher Number", "Information")
                Else
                    ShowMessage(getValueByKey("ADS006"), "ADS006 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please enter Number of Quantity Required", "Information")
                End If
                Exit Sub
            End If
            If cmbVoucherProgram.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("ADS007"), "ADS007 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please select the Voucher Program", "Information")
                Exit Sub
            End If
            'For Each dr As DataRow In dtGVDet.Select("VOUCHERCODE='" & cmbVoucherProgram.SelectedValue & "'", "", DataViewRowState.CurrentRows)
            '    If dr("AllowAnyDeno") = True Then
            '        AnyDenomination = True
            '    Else
            '        AnyDenomination = False
            '    End If

            'Next
            Dim objBLLAcceptPayment As New clsAcceptPayment
            Dim msg = String.Empty, valueOfVoucher As String = String.Empty

            If rbPrePrinted.Checked = True Then
                If Not _dtGv Is Nothing Then
                    For Each dr As DataRow In _dtGv.Select("Vouchercode='" & cmbVoucherProgram.SelectedValue & "' AND VOURCHERSERIALNBR='" & txtVoucher.Text.Trim & "'", "", DataViewRowState.CurrentRows)
                        ShowMessage(getValueByKey("ADS012"), "ADS012 - " & getValueByKey("CLAE04"))
                        txtVoucher.Focus()
                        Exit Sub
                    Next
                End If
                If objBLLAcceptPayment.FnGiftVoucherValidate(txtVoucher.Text, msg, 0, Nothing, True, cmbVoucherProgram.SelectedValue, clsAdmin.SiteCode, valueOfVoucher) = False Then
                    'msg = msg.Replace("Gift Voucher", "Credit Voucher")
                    ShowMessage(msg, getValueByKey("CLAE04"))
                    txtVoucher.Focus()
                    Exit Sub
                End If
            End If

            If AnyDenomination = True Then
                GetGVStruc(valueOfVoucher)
            Else
                If CheckDenominationValid() = True Then
                    GetGVStruc(valueOfVoucher)
                    cmbDenomination.SelectedIndex = 0
                    txtVoucher.Select()
                Else
                    ShowMessage(getValueByKey("ADS008"), "ADS008 - " & getValueByKey("CLAE04"))
                    'ShowMessage("This Denomination is not avaliable for the selected Gift Voucher", "Information")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub
    ''' <summary>
    '''  TODO:Checking Denomination is complete 
    ''' </summary>
    ''' <CreatedBy>Rahul </CreatedBy>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkDenomination() As Boolean
        Try


            If Not _dtGv Is Nothing Then
                If Not _dtGv.Rows.Count <= 0 Then
                    Dim decTotalDenomination As Decimal = _dtGv.Compute("sum(NetAmount)", "")
                    Dim decVoucherAmount As Decimal = CDbl(txtDenomination.Text)
                    Dim inoQty As Integer = CInt(txtVoucher.Text)
                    Dim decTotalEnteredAmount As Decimal = decVoucherAmount * inoQty
                    Dim TotalAmountD As Decimal = Decimal.Add(decTotalDenomination, decTotalEnteredAmount)
                    If (TotalAmountD <= TotalDenomination) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Dim decVoucherAmount As Decimal = CDbl(txtDenomination.Text)
                    Dim inoQty As Integer = CInt(txtVoucher.Text)
                    Dim decTotalEnteredAmount As Decimal = decVoucherAmount * inoQty
                    If (decTotalEnteredAmount > TotalDenomination) Then
                        Return False
                    Else
                        Return True
                    End If

                End If

            Else
                If Not (txtDenomination.Text = String.Empty) Then
                    Dim decVoucherAmount As Decimal = CDbl(txtDenomination.Text)
                    Dim inoQty As Integer = CInt(txtVoucher.Text)
                    Dim decTotalEnteredAmount As Decimal = decVoucherAmount * inoQty

                    If (decTotalEnteredAmount > TotalDenomination) Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    ShowMessage(getValueByKey("ADS009"), "ADS009 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Enter the amount for Gift Voucher ")
                    Return False
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Sub AddGVforSale()
        Try
            Dim ObjArt As New clsIteamSearch
            If Not _dtGv Is Nothing Then
                For Each drGV As DataRow In _dtGv.Rows
                    Dim Voucher As String = ""
                    For Each dr As DataRow In dtGVDet.Select("VOUCHERCODE='" & drGV("VOUCHERCODE").ToString() & "'", "Articlecode", DataViewRowState.CurrentRows)
                        Article = dr("ArticleCode").ToString()
                        Voucher = drGV("VOUCHERCODE").ToString()
                    Next

                    'Dim dv As New DataView(_dtTemp, "ArticleCode='" & Article & "' AND SellingPrice=" & drGV("VALUEOFVOUCHER").ToString(), "", DataViewRowState.CurrentRows)


                    'If dv.Count > 0 Then
                    '    dv.AllowEdit = True
                    '    For Each dr As DataRowView In dv
                    '        dr("Quantity") = dr("Quantity") + IIf(IsDBNull(drGV("ISPREPRINTED")) = False AndAlso drGV("ISPREPRINTED") = True, 1, drGV("Quantity"))
                    '    Next
                    'Else
                    If Article = String.Empty Then
                        'ShowMessage(Voucher & " - this Gift voucher program have no article attached", "Error")
                        ShowMessage(String.Format(getValueByKey("ADS013"), Voucher), "ADS013 - " & getValueByKey("CLAE04"))
                    Else
                        Dim dt As DataTable = ObjArt.GetItemData(Article, clsAdmin.LangCode)
                        Dim dr As DataRow = _dtTemp.NewRow
                        dr("Btype") = "S"
                        dr("EAN") = dt.Rows(0)("EAN")
                        dr("ArticleCode") = dt.Rows(0)("ArticleCode")
                        dr("Discription") = dt.Rows(0)("DISCRIPTION")
                        dr("UnitofMeasure") = dt.Rows(0)("UnitofMeasure")
                        dr("SellingPrice") = drGV("VALUEOFVOUCHER").ToString()
                        If IsDBNull(drGV("ISPREPRINTED")) = False AndAlso drGV("ISPREPRINTED") = True Then
                            dr("Quantity") = 1
                        Else
                            dr("Quantity") = drGV("Quantity")
                        End If
                        dr("Section") = drGV("IssuedDocNumber")
                        dr("PROMOTIONID") = "NO"
                        dr("MANUALPROMO") = "NO"
                        dr("FIRSTLEVEL") = "NO"
                        dr("TOPLEVEL") = "NO"
                        dr("CLPRequire") = True
                        dr("BillLineNo") = 1

                        _dtTemp.Rows.Add(dr)
                    End If
                    ' End If
                Next
            End If
            'GetGVStruc()
        Catch ex As Exception
            ShowMessage(getValueByKey("ADS010"), "ADS010 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in GV calculation", "GV")
        End Try
    End Sub
    Private Sub SetGrid()
        Try
            For colno = 1 To dgMainGrid.Cols.Count - 1
                If dgMainGrid.Cols(colno).Name.ToUpper() <> "VALUEOFVOUCHER".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "VOURCHERSERIALNBR".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "VOUCHERDESC".ToUpper() _
                      AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                Else
                    dgMainGrid.Cols(colno).AllowEditing = False
                End If
            Next
            'dgMainGrid.Cols("VALUEOFVOUCHER").Caption = "Denomination"

            dgMainGrid.Cols("VALUEOFVOUCHER").Format = "0.00"
            'dgMainGrid.Cols("VOURCHERSERIALNBR").Caption = "Voucher No"
            dgMainGrid.Cols("VOURCHERSERIALNBR").Caption = getValueByKey("frmnadvancesale.dgmaingrid.vourcherserialnbr")
            dgMainGrid.Cols("VOURCHERSERIALNBR").Format = "0"
            'gMainGrid.Cols("VOUCHERDESC").Caption = "Voucher Program"
            dgMainGrid.Cols("VOUCHERDESC").Caption = getValueByKey("frmnadvancesale.dgmaingrid.voucherdesc")
            dgMainGrid.Cols("VOUCHERDESC").Width = 150

            dgMainGrid.Cols("VALUEOFVOUCHER").Width = 100
            dgMainGrid.Cols("VALUEOFVOUCHER").Caption = getValueByKey("frmnadvancesale.dgmaingrid.valueofvoucher")
            dgMainGrid.Cols("VOURCHERSERIALNBR").Width = 120
            dgMainGrid.Cols("VOURCHERSERIALNBR").Caption = getValueByKey("frmnadvancesale.dgmaingrid.vourcherserialnbr")
            dgMainGrid.Cols("Quantity").Width = 70
            dgMainGrid.Cols("Quantity").Format = 0
            dgMainGrid.Cols("Quantity").Caption = getValueByKey("frmnadvancesale.dgmaingrid.quantity")
            dgMainGrid.Cols("NetAmount").Format = "0.00"
            dgMainGrid.Cols("NetAmount").Caption = getValueByKey("frmnadvancesale.dgmaingrid.netamount")
            dgMainGrid.Cols("SEL").Caption = ""
            dgMainGrid.Cols("SEL").Width = 35
            dgMainGrid.Cols("SEL").ComboList = "..."
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub GetGVStruc(ByVal voucherOfValue As String)
        Try
            If _dtGv Is Nothing Then
                Dim obj As New clsAdvanceSale()
                _dtGv = obj.GetVoucherStru()
                _dtGv.TableName = "VOUCHERDTLS"
            End If
            If IsBirthListGV Then
                _dtGv.Columns("ExpiryInDays").DataType = System.Type.GetType("System.Int32")
            End If

            Dim dr As DataRow = _dtGv.NewRow()
            dr("SITECODE") = clsAdmin.SiteCode
            dr("VOUCHERCODE") = cmbVoucherProgram.SelectedValue
            dr("VOUCHERDESC") = cmbVoucherProgram.Text.Trim
            If rbPrePrinted.Checked = True Then
                dr("VOURCHERSERIALNBR") = txtVoucher.Text.Trim
                dr("ISPREPRINTED") = 1
                dr("Quantity") = 1
            Else
                dr("Quantity") = txtVoucher.Text.Trim
            End If
            Dim dvView As DataView = New DataView(dtGVDet, "VOUCHERCODE='" & dr("VOUCHERCODE") & "'", "", DataViewRowState.CurrentRows)
            Dim expiryDays As Object
            For Each dvViewRow As DataRowView In dvView
                If Not dvViewRow("ExpiryAfterDays") Is DBNull.Value Then
                    expiryDays = dvViewRow("ExpiryAfterDays")
                End If
            Next


            dr("VALUEOFVOUCHER") = IIf(cmbDenomination.Visible = False, txtDenomination.Text, voucherOfValue)
            dr("ISACTIVE") = 1
            dr("ISISSUED") = 1
            dr("NetAmount") = dr("Quantity") * dr("VALUEOFVOUCHER")
            dr("ISSUEDATSITE") = clsAdmin.SiteCode
            dr("ISSUEDONDATE") = clsAdmin.DayOpenDate.Date
            dr("IssuedDocNumber") = _incrementNo + 1
            _incrementNo = _incrementNo + 1
            'If IsBirthListGV Then
            If Not expiryDays Is Nothing Then
                dr("ExpiryInDays") = CInt(expiryDays)
                dr("ExpiryDate") = DateAdd(DateInterval.Day, expiryDays, Now)
            Else
                dr("ExpiryInDays") = 0
                dr("ExpiryDate") = DateAdd(DateInterval.Day, 0, Now)
            End If

            'End If

            _dtGv.Rows.Add(dr)
            dgMainGrid.DataSource = _dtGv
            SetGrid()

            txtDenomination.Text = String.Empty
            cmbDenomination.SelectedIndex = -1
            txtVoucher.Text = String.Empty
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
    Private Sub cmdVoucherOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVoucherOk.Click
        Try
            Dim res As Int32 = 6
            If (TransactionID = TransactionTypes.BirthListUpdate.ToString()) Then

            ElseIf (TransactionID = TransactionTypes.BirthListSales.ToString()) Then
                AddGVForSale_BirthList()
            Else
                AddGVforSale()
            End If
            IsFormCanceled = False
            If _dtGv Is Nothing Then
                res = MsgBox(getValueByKey("ADS014"), MsgBoxStyle.YesNo, "ADS014 - " & getValueByKey("CLAE04"))
                If res = MsgBoxResult.Yes Then
                    Me.Close()
                Else
                    'Rakesh-21.10.203-8263: Avoid closed dialog message after click 'NO'
                    Exit Sub
                End If
            Else
                Me.Close()
            End If
            If res = MsgBoxResult.Yes Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function CheckDenominationValid() As Boolean
        Try
            Dim filter As String
            filter = "VOUCHERCODE='" & cmbVoucherProgram.SelectedValue & "'"
            If cmbDenomination.Visible = False Then
                filter = filter & " And DENOMINATIONAMT=" & ConvertToEnglish(CDbl(IIf(txtDenomination.Text <> String.Empty, txtDenomination.Text, 0)))
            End If
            Dim dv As New DataView(dtGVDeno, filter, "", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    Private Sub dgMainGrid_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.CellButtonClick
        Try
            dgMainGrid.Rows.Remove(dgMainGrid.Row)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub dgMainGrid_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgMainGrid.KeyDown
        Try
            If e.KeyCode = Keys.Delete AndAlso dgMainGrid.Row >= 0 Then
                dgMainGrid.Rows.Remove(dgMainGrid.Row)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbVoucherProgram_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVoucherProgram.SelectedValueChanged
        Try
            For Each dr As DataRow In dtGVDet.Select("VOUCHERCODE='" & cmbVoucherProgram.SelectedValue & "'", "", DataViewRowState.CurrentRows)
                If dr("AllowAnyDeno") = True Then
                    AnyDenomination = True
                Else
                    AnyDenomination = False
                End If

            Next
            If AnyDenomination = False Then
                Dim filter As String
                filter = "VOUCHERCODE='" & cmbVoucherProgram.SelectedValue & "'"
                'filter = filter & " And DENOMINATIONAMT=" & txtDenomination.Text
                Dim dv As New DataView(dtGVDeno, filter, "", DataViewRowState.CurrentRows)
                cmbDenomination.DataSource = dv
                cmbDenomination.DisplayMember = "DENOMINATIONAMT"
                cmbDenomination.ValueMember = "DENOMINATIONAMT"
                cmbDenomination.SelectedIndex = 0
                cmbDenomination.Visible = True
                pC1ComboSetDisplayMember(cmbDenomination)
            Else
                cmbDenomination.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub txtDenomination_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDenomination.KeyDown
        If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Or e.KeyValue = 190 Or e.KeyValue = 188 Then
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub txtVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVoucher.KeyDown
        If lblQtyRequired.Visible = True Then
            'If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
            '    e.SuppressKeyPress = False
            'Else
            '    e.SuppressKeyPress = True
            'End If

            If e.KeyCode = Keys.Enter Then
                cmdAdd_Click(sender, e)
                cmdVoucherOk_Click(sender, e)
            End If
        ElseIf lblVoucherNo.Visible = True Then
            If e.KeyCode = Keys.Enter Then
                cmdAdd_Click(sender, e)              
            End If
        End If
    End Sub


End Class
