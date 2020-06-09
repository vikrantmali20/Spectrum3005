Imports SpectrumBL
Imports com.criti.spectrum.encrypt
Imports System.Data
Imports System.Data.SqlClient
Public Class frmNUserAuthorisation
    Dim FormNormalHeight, SpliterDistance As Integer
    Dim TransactionType, _Promotion As String
    Dim dtUserAuth As DataTable
    Dim _Auhorized As Boolean
    Dim _decTotalAmount As Decimal = Decimal.Zero
    Dim _decTotalDiscount As Decimal
    Dim _decTotalOpenAmount As Decimal
    Private _IssueType As String
    Private _IsFormCacel As Boolean = True
    Private filter As String = ""
    Public IsPromoSelected As Boolean
    Private dtCashMemoDtls As DataTable
    Public isPromotionsDetailClearRequired As Boolean = False
    Dim IsCSTApplicable As Boolean = False
    Dim _strCustNo As String
    Dim _NewTotalDiscount As Decimal 'vipin PC SO Merge 03-05-2018
    ''' <summary>
    ''' Total Amount send to form
    ''' </summary>
    ''' <value>Decimal</value>
    ''' <returns>Decimal</returns>
    ''' <UsedBy>frmBirthListUpdation.vb</UsedBy>
    ''' <remarks>Only needs to send from BirthList</remarks>
    Public Property TotalAmount() As Decimal
        Get
            Return _decTotalAmount
        End Get
        Set(ByVal value As Decimal)
            _decTotalAmount = value
        End Set
    End Property
    Public WriteOnly Property Extrafilter()
        Set(ByVal value)
            filter = value
        End Set
    End Property
    ''' <summary>
    '''  Issue Type is either GiftVoucher or CreditVoucher 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IssueType() As String
        Get
            Return _IssueType
        End Get
        Set(ByVal value As String)
            _IssueType = value
        End Set
    End Property
    Public WriteOnly Property PromotionId() As String
        Set(ByVal value As String)
            _Promotion = value
        End Set
    End Property
    ''' <summary>
    ''' Total Discount amount 
    ''' </summary>
    ''' <value></value>
    ''' <returns>Decimal </returns>
    ''' <remarks>Return calculated discount amount </remarks>
    Public ReadOnly Property TotalDiscountAmount() As Decimal
        Get
            Return _decTotalDiscount
        End Get
    End Property
    Public ReadOnly Property NewDiscountAmount() As Decimal 'vipin
        Get
            Return _NewTotalDiscount
        End Get
    End Property
    Public WriteOnly Property TotalOpenAmount() As Decimal
        Set(ByVal value As Decimal)
            _decTotalOpenAmount = value
        End Set
    End Property
    Public ReadOnly Property Authorized() As Boolean
        Get
            Return _Auhorized
        End Get
    End Property
    Public WriteOnly Property SetAuthorization() As Boolean
        Set(ByVal value As Boolean)
            _Auhorized = value
        End Set
    End Property
    Public Shared _IsNewSalaesOrder As Boolean = False
    Public Property IsNewSalaesOrder() As Boolean
        Get
            Return _IsNewSalaesOrder
        End Get
        Set(ByVal value As Boolean)
            _IsNewSalaesOrder = value
        End Set
    End Property
    Public TotalPickUpDisc As DataTable
    Public TaxPer As DataTable
    Public Shared _IsRemarkEnable As Boolean = False
    Public Property IsRemarkEnable() As Boolean
        Get
            Return _IsRemarkEnable
        End Get
        Set(ByVal value As Boolean)
            _IsRemarkEnable = value
        End Set
    End Property

    Private _balancetoPay As Decimal = Decimal.Zero
    Public Property balancetoPay() As Decimal
        Get
            Return _balancetoPay
        End Get
        Set(ByVal value As Decimal)
            _balancetoPay = value
        End Set
    End Property
    Private _DiscountInPer As Decimal = Decimal.Zero
    Public Property DiscountInPer() As Decimal
        Get
            Return _DiscountInPer
        End Get
        Set(ByVal value As Decimal)
            _DiscountInPer = value
        End Set
    End Property

    Private _DiscountInVal As Decimal = Decimal.Zero
    Public Property DiscountInVal() As Decimal
        Get
            Return _DiscountInVal
        End Get
        Set(ByVal value As Decimal)
            _DiscountInVal = value
        End Set
    End Property

    'added by khusrao adil on 21-06-2017 for natural juhu CR
    Private _CSAAuthUserName As String
    Public Property CSAAuthUserName() As String
        Get
            Return _CSAAuthUserName
        End Get
        Set(ByVal value As String)
            _CSAAuthUserName = value
        End Set
    End Property
    'added by khusrao adil on 21-06-2017 for natural juhu CR
    Private _SendFromCSAScreen As Boolean
    Public Property SendFromCSAScreen() As Boolean
        Get
            Return _SendFromCSAScreen
        End Get
        Set(ByVal value As Boolean)
            _SendFromCSAScreen = value
        End Set
    End Property
    Public DiscountPercentage As Decimal
    Public chkCustomer As String = ""
    Private Sub frmUserAuthorisation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            remarks.Clear()
            rbDocument.Checked = True
            _strCustNo = chkCustomer
            'txtUserId.Text = String.Empty
            If TransactionType = "BL_CLOSE" Then
                If (TotalAmount > Decimal.Zero) Then
                    txtTotalDiscountPercentage.Enabled = False
                    txtTotalDiscountPercentage.Text = clsDefaultConfiguration.BLCloseDiscountPercentage
                    lblCurrVoucherAmt.Text = clsAdmin.CurrencyCode
                    lblCurrTotalDis.Text = clsAdmin.CurrencyCode
                    CalDiscount()
                    txtTotalApplicableAmountForVoucher.Enabled = False
                    txtTotalDiscountAmount.Enabled = False

                Else
                    Dim dgrsult As DialogResult = MessageBox.Show(getValueByKey("UA001"), "UA001 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
                    If (dgrsult = Windows.Forms.DialogResult.Yes) Then
                        IsFormCancel = False
                    ElseIf (dgrsult = Windows.Forms.DialogResult.Yes) Then
                        IsFormCancel = True
                    End If
                    Me.Dispose()
                    Exit Sub
                End If
            End If
            FormNormalHeight = Me.Height
            SpliterDistance = SplitContainer1.SplitterDistance
            SetformLayout(1)
            If _Auhorized = True Then
                SplitContainer1.Panel1Collapsed = True
                UpdateTran()
            End If
            If IsRemarkEnable = True Then
                lblRemarks.Visible = False
                txtRemarks.Visible = False
            End If
           
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
        If TransactionType = "BL_CLOSE" Then
            'Me.Text = "Birth List Apply Discount And Close"
            Me.Text = getValueByKey("frmuserauthorisation.blclose")
        End If
        If IsNewSalaesOrder Then
            rbItem.Visible = False
        End If
        If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
            lblRemarks.Visible = False
            txtRemarks.Visible = False
        End If
        txtUserId.Select()
        txtUserId.Focus()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub
    ''' <summary>
    ''' Check Discount is   apply   successfully or canceled.
    '''<CreatedBy>Rahul</CreatedBy>
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IsFormCancel() As Boolean
        Get
            Return _IsFormCacel
        End Get
        Set(ByVal value As Boolean)
            _IsFormCacel = value
        End Set
    End Property
    Private Sub SetformLayout(ByVal Level As Integer)
        If Level = 1 Then
            Dim i As Integer
            i = SplitContainer1.Panel2.Height
            i = FormNormalHeight - i
            Me.Height = i
            SplitContainer1.Panel2Collapsed = True
        ElseIf Level = 2 Then
            Me.Height = FormNormalHeight
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = SpliterDistance
            sizBirthList.Visible = False
            sizDiscLevel.Visible = True
        ElseIf Level = 3 Then
            Me.Height = FormNormalHeight
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = SpliterDistance
            sizBirthList.Visible = True
            sizDiscLevel.Visible = False
            Me.Text = getValueByKey("frmnuserauthorisation1")
        End If
    End Sub
    Private Function ValidateHdr() As Boolean
        Try
            If txtUserId.Text.Trim = String.Empty Then
                ShowMessage(getValueByKey("UA002"), "UA002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Enter Userid", "Information")
                txtUserId.Select()
                Return False
            End If
            If txtPassWord.Text.Trim = String.Empty Then
                ShowMessage(getValueByKey("UA003"), "UA003 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Enter Your Password", "Information")
                txtPassWord.Select()
                Return False
            End If
            If IsRemarkEnable = True Then
                If txtRemarks.Text.Trim = String.Empty Then
                    ShowMessage(getValueByKey("UA004"), "UA004 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Enter the Remarks", "Information")
                    txtRemarks.Select()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            'added by khusrao adil on 21-06-2017 for natural juhu CR
            CSAAuthUserName = String.Empty
            If (String.IsNullOrEmpty(txtUserId.Text.Trim())) Then
                ShowMessage(getValueByKey("LG001"), getValueByKey("CLAE04"))
                txtUserId.Select()
                txtUserId.Focus()
                Exit Sub
            ElseIf (String.IsNullOrEmpty(txtPassWord.Text.Trim())) Then
                ShowMessage(getValueByKey("LG002"), getValueByKey("CLAE04"))
                txtPassWord.Select()
                txtPassWord.Focus()
                Exit Sub
            ElseIf clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
            ElseIf (String.IsNullOrEmpty(txtRemarks.Text.Trim())) Then
                ShowMessage("Blank remark not allowed ", getValueByKey("CLAE04"))
                txtRemarks.Select()
                txtRemarks.Focus()
                Exit Sub
            End If

            Dim StrMsg As String = ""
            Dim objLog As New clsLogin()
            Dim dbPassword As String
            Dim isIdNumber As Boolean = False
            Dim strUserName As String = String.Empty
            If objLog.CheckAuthorizeUser(txtUserId.Text, txtPassWord.Text, StrMsg, dbPassword, clsAdmin.SiteCode, isIdNumber, strUserName) = True Then
                If isIdNumber = False Then

                    If ValidateHdr() = True Then

                        If dbPassword.Length < 4 Then
                            StrMsg = getValueByKey("UA006")
                            ShowMessage(StrMsg, "UA006 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        Else
                            Dim deCrpt As New clsEncrypter
                            If deCrpt.authenticatePassword(txtPassWord.Text, dbPassword) Then
                                If CheckAuthorisation(txtUserId.Text, TransactionType) Then

                                    If (TransactionType = TransactionTypes.BL_ChngDisc.ToString()) Then
                                        txtTotalDiscountPercentage.Enabled = True
                                        'Change Discount Percentage while closing birthlist
                                    End If
                                    _Auhorized = True
                                    remarks.Text += txtRemarks.Text
                                    UpdateTran()
                                    If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                                        CSAAuthUserName = txtUserId.Text
                                        Me.DialogResult = Windows.Forms.DialogResult.Yes
                                        Me.Close()
                                    Else
                                        Me.DialogResult = Windows.Forms.DialogResult.No
                                        Exit Sub
                                    End If
                                Else
                                    _Auhorized = False
                                    ShowMessage(getValueByKey("UA005"), "UA005 - " & getValueByKey("CLAE04"))
                                    'ShowMessage("User have no authorisation ", "Information")
                                End If
                            Else
                                StrMsg = getValueByKey("UA006")
                                txtPassWord.Text = String.Empty
                                ShowMessage(StrMsg, "UA006 - " & getValueByKey("CLAE04"))
                            End If
                        End If



                    End If
                Else
                    If txtUserId.Text = String.Empty Then
                        txtUserId.Text = strUserName
                    End If

                    If CheckAuthorisation(txtUserId.Text, TransactionType) Then

                        If (TransactionType = TransactionTypes.BL_ChngDisc.ToString()) Then
                            txtTotalDiscountPercentage.Enabled = True
                            'Change Discount Percentage while closing birthlist
                        End If
                        _Auhorized = True
                        remarks.Text += txtRemarks.Text
                        UpdateTran()
                        Exit Sub
                    Else
                        _Auhorized = False
                        ShowMessage(getValueByKey("UA005"), "UA005 - " & getValueByKey("CLAE04"))
                        'ShowMessage("User have no authorisation ", "Information")
                    End If
                End If
            Else
                ShowMessage(StrMsg, getValueByKey("CLAE04"))
                txtUserId.Select()
                txtUserId.Focus()
                Exit Sub
            End If
            'Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Checking Transaction type 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateTran() As Boolean
        If TransactionType.ToUpper() = "ORD".ToUpper() Then
            sizTop.Enabled = False
            SetformLayout(2)
            dgMainGrid.DataSource = dtUserAuth
            rbValue.Checked = True
            SetGridLayout()
            rbPercentage.Checked = True
            Exit Function
        ElseIf (TransactionType = TransactionTypes.BirthListUpdate.ToString() Or TransactionType = TransactionTypes.BL_CLOSE.ToString Or TransactionType = TransactionTypes.BL_ChngDisc.ToString()) Then
            If (TotalAmount > Decimal.Zero) Then
                sizTop.Enabled = False
                'FormNormalHeight = Me.Height
                SetformLayout(3)
                'txtTotalApplicableAmountForVoucher.Text = _decTotalOpenAmount
            Else
                ShowMessage(getValueByKey("UA007"), "UA007 - " & getValueByKey("CLAE04"))
                'MessageBox.Show("Discount not applicable")
            End If
        Else
            If Not dtUserAuth Is Nothing Then
                For Each row As DataRow In dtUserAuth.Rows
                    row("AuthUserId") = txtUserId.Text.Trim()
                    row("AuthUserRemarks") = txtRemarks.Text.Trim()
                Next
            End If
            Me.Close()
        End If
    End Function
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Public Sub New(ByVal Transaction As String, ByRef dt As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TransactionType = Transaction
        If dt IsNot Nothing Then
            dtUserAuth = dt.Copy
            dtCashMemoDtls = dt
        End If
        
        ' Add any initialization after the InitializeComponent() call. 
    End Sub
    Private Sub SetGridLayout()
        Try
            For colno = 0 To dgMainGrid.Cols.Count - 1
                If dgMainGrid.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALDISCPERCENTAGE".ToUpper() _
                     AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALDISCOUNT".ToUpper() _
                      AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "DISCOUNT".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                End If
            Next
            dgMainGrid.Cols("DISCRIPTION").Width = 180
            dgMainGrid.Cols("DISCRIPTION").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.discription")
            dgMainGrid.Cols("DISCRIPTION").AllowEditing = False
            'dgMainGrid.Cols("STOCK").Width = 62
            'dgMainGrid.Cols("STOCK").Caption = "Stock"
            'dgMainGrid.Cols("STOCK").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Width = 100
            dgMainGrid.Cols("ArticleCode").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.articlecode")
            dgMainGrid.Cols("ArticleCode").AllowEditing = False
            dgMainGrid.Cols("Quantity").Width = 45
            dgMainGrid.Cols("Quantity").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.quantity")
            dgMainGrid.Cols("Quantity").AllowEditing = False
            dgMainGrid.Cols("SellingPrice").Width = 58
            dgMainGrid.Cols("SellingPrice").Caption = getValueByKey("frmNUserAuthorisation.dgMainGrid.sellingprice")
            dgMainGrid.Cols("SellingPrice").AllowEditing = False
            dgMainGrid.Cols("SellingPrice").Format = "0.00"

            If dtUserAuth.Columns.Contains("TotalDiscount") Then
                dgMainGrid.Cols("TotalDiscount").Width = 85
                dgMainGrid.Cols("TotalDiscount").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.totaldiscount")
                dgMainGrid.Cols("TotalDiscount").AllowEditing = False
                dgMainGrid.Cols("TotalDiscount").Format = "0.00"
            End If
            If dtUserAuth.Columns.Contains("Discount") Then
                dgMainGrid.Cols("Discount").Width = 56
                dgMainGrid.Cols("Discount").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.discount")
                dgMainGrid.Cols("Discount").AllowEditing = False
                dgMainGrid.Cols("Discount").Format = "0.00"
            End If
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Width = 85
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.totaldiscpercentage")
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").Width = 60
            dgMainGrid.Cols("NETAMOUNT").Caption = getValueByKey("frmnuserauthorisation.dgmaingrid.netamount")
            dgMainGrid.Cols("NETAMOUNT").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").AllowEditing = False
            dgMainGrid.AllowSorting = True
            
            'dgMainGrid.Sort(C1.Win.C1FlexGrid.SortFlags.Descending, dgMainGrid.Cols("BillLineNO").Index)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbDocument_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDocument.CheckedChanged
        Try
            SetDetialLayot()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub SetDetialLayot()
        Try
            If rbDocument.Checked = True Then
                txtValue.Visible = True
                dgMainGrid.Visible = False
            End If
            If rbItem.Checked = True Then
                txtValue.Visible = False
                dgMainGrid.Visible = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdFlush_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFlush.Click
        Try
            Dim FilterCondition As String = filter & " isnull(Toplevel,'')=''" 'changed by sagar-15042016 by sagar issue : discount apply on delete article also status<>deletd 
            If IsNewSalaesOrder Then
                FilterCondition = FilterCondition & " and IsStatus <> 'Deleted'"
            End If
            If clsDefaultConfiguration.IsNewSalesOrder Then
                Dim ObCreation As New frmPCSalesOrderCreation
                If txtValue.Text.Trim <> "" Then
                    ObCreation.DiscAmt = CDbl(txtValue.Text)
                End If
            End If
            If rbDocument.Checked = True Then
                Dim percentage, totalDiscValue As Double
                'ShowMessage(getValueByKey("UA014"), "UA014 - " & getValueByKey("CLAE04"))
                If rbPercentage.Checked = True Then
                    If txtValue.Text.Trim = String.Empty Then 'Or Not IsNumeric(txtValue.Text.Trim)
                        ShowMessage(getValueByKey("UA015"), "UA015 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Please Enter Valid Discount", "Information")
                        Exit Sub
                    End If
                    If Not IsNumeric(txtValue.Text.Trim) And Not IsNewSalaesOrder Then
                        ShowMessage(getValueByKey("UA015"), "UA015 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Please Enter Valid Discount", "Information")
                        Exit Sub
                    End If

                    If txtValue.Text.Trim > 100 Then
                        ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Discount Percent Greater than 100 is not Possible", "Information")
                        Exit Sub
                    End If
                    txtValue.Text = txtValue.Text.Trim '+ DiscountInPer
                    Dim discount As Decimal
                    Dim Newdiscount As Decimal '$$ Ketan 
                    For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                        discount += (dr("GROSSAMT") * (txtValue.Text.Trim)) / 100 'vipin PC SO Merge 03-05-2018
                        If IsNewSalaesOrder Then
                            Newdiscount += (dr("SellingPrice") * (dr("Quantity") - dr("DeliveredQty"))) * (txtValue.Text.Trim) / 100
                        End If
                    Next
                    If balancetoPay <> Decimal.Zero Then

                        If balancetoPay > 0 Then ''vipin PC SO Merge 03-05-2018
                            If balancetoPay <= discount Then
                                ShowMessage("Discount amount cannot be greater than balance to pay amount", "Information")
                                Exit Sub
                            End If
                        End If
                    End If
                    Dim totalGAmount1 As Double
                    If IsNewSalaesOrder Then
                        For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                            totalGAmount1 = totalGAmount1 + (dr("GROSSAMT") * ((dr("QUANTITY") - dr("DELIVEREDQTY")) / dr("QUANTITY")))
                        Next
                    End If
                    For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                        dr("TOTALDISCPERCENTAGE") = txtValue.Text.Trim
                        dr("LINEDISCOUNT") = (dr("GROSSAMT") * (txtValue.Text.Trim)) / 100
                        dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                        'dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")

                        If IsNewSalaesOrder Then
                            'totalDiscValue = FormatNumber((totalGAmount1 * (txtValue.Text.Trim)) / 100, 0) + DiscountInVal
                            totalDiscValue = discount + DiscountInVal
                            percentage = ((dr("GROSSAMT") * ((((dr("QUANTITY") - dr("DELIVEREDQTY")) / dr("QUANTITY"))))) / totalGAmount1) * 100
                            dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                            dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                            dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                            percentage = (dr("GROSSAMT") / totalGAmount1) * 100
                            dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100

                        End If


                        If Not IsNewSalaesOrder Then
                            'dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                            'CODE ADDED BY IRFAN FOR IGST CAKEKRAFT ON 24/10/2017
                            If dtUserAuth.Columns.Contains("TaxPer") = True Then
                                dr("EXCLUSIVETAX") = (((dr("GROSSAMT") - dr("TOTALDISCOUNT"))) * dtUserAuth.Rows(0)("TaxPer")) / 100
                                dr("TOTALTAXAMT") = dr("EXCLUSIVETAX")
                                dr("NETAMOUNT") = (dr("GROSSAMT") - dr("TOTALDISCOUNT")) + dr("EXCLUSIVETAX")
                            Else
                                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                            End If
                        Else
                            Dim PendingDesc = ((dr("LineDiscount") * ((dr("Quantity") - dr("DeliveredQty")) / dr("Quantity"))))
                            If TotalPickUpDisc IsNot Nothing Then
                                Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + dr("pkgLineNo").ToString() + "'")
                                If result.Length > 0 Then
                                    dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - PendingDesc - result(0)("DiscountAmount")
                                Else
                                    dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - PendingDesc
                                End If
                            Else
                                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - PendingDesc
                            End If
                        End If

                        dr("AuthUserId") = txtUserId.Text.Trim()
                        dr("AuthUserRemarks") = txtUserId.Text.Trim()
                        dr("PROMOTIONID") = _Promotion
                        dr("TOPLEVEL") = _Promotion
                        dr("FirstLEVEL") = _Promotion
                        _NewTotalDiscount = Newdiscount
                    Next
                End If
                Dim totalGAmount As Double
                If rbValue.Checked = True Then
                    If txtValue.Text.Trim = String.Empty Or Not IsNumeric(txtValue.Text.Trim) Then
                        ShowMessage(getValueByKey("UA015"), "UA015 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Please Enter Valid Discount", "Information")
                        Exit Sub
                    End If
                    'totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)
                    If IsNewSalaesOrder Then
                        For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                            totalGAmount = totalGAmount + (dr("GROSSAMT") * ((dr("QUANTITY") - dr("DELIVEREDQTY")) / dr("QUANTITY")))
                        Next
                    Else
                        'code added for issue id 1532
                        If isPromotionsDetailClearRequired = True Then
                            For Each dr As DataRow In dtUserAuth.Rows
                                If dr("Toplevel").ToString() <> "" Then
                                    dr("Toplevel") = ""
                                End If
                                If dr("PromotionId").ToString() <> "" Then
                                    dr("PromotionId") = ""
                                End If
                                If dr("AuthUserId").ToString() <> "" Then
                                    dr("AuthUserId") = ""
                                End If
                                If dr("AuthUserRemarks").ToString() <> "" Then
                                    dr("AuthUserRemarks") = ""
                                End If
                            Next
                        End If
                        If Not IsDBNull(dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)) Then
                            totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)
                        Else
                            For i As Integer = 0 To dtUserAuth.Rows.Count - 1
                                totalGAmount += dtUserAuth.Rows(i)("GROSSAMT")
                            Next
                        End If
                        'totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)

                    End If
                    totalDiscValue = txtValue.Text.Trim + DiscountInVal
                    If balancetoPay <> Decimal.Zero Then
                        If balancetoPay <= totalDiscValue Then
                            ShowMessage("Discount amount cannot be greater than balance to pay amount", "Information")
                            Exit Sub
                        End If
                    End If
                    If (totalGAmount - totalDiscValue) < 0 Then
                        ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Discount Percent Greater than 100 is not Possible", "Information")
                        Exit Sub
                    End If
                    For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                        If Not IsNewSalaesOrder Then
                            If dtUserAuth.Columns.Contains("TaxPer") = True Then
                                'code is added by irfan for cakekraft 
                                percentage = (dr("GROSSAMT") / totalGAmount) * 100
                                dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                                'dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                                Dim strLINEDISCOUNT As Decimal = dr("LINEDISCOUNT")
                                Dim strGrossAmt As Decimal = dr("GrossAmt")
                                If strGrossAmt = 0 Then
                                    strGrossAmt = 1
                                End If

                                dr("TOTALDISCPERCENTAGE") = (strLINEDISCOUNT / strGrossAmt) * 100
                                dr("LINEDISCOUNT") = FormatNumber(CDbl(dr("LINEDISCOUNT")), 2)
                                dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                                dr("EXCLUSIVETAX") = (((dr("GROSSAMT") - dr("TOTALDISCOUNT"))) * dtUserAuth.Rows(0)("TaxPer")) / 100 ' adil code 26-
                                dr("NETAMOUNT") = (dr("GROSSAMT") - dr("TOTALDISCOUNT")) + dr("EXCLUSIVETAX")
                                dr("EXCLUSIVETAX") = FormatNumber(CDbl(dr("EXCLUSIVETAX")), 2)
                                dr("NETAMOUNT") = FormatNumber(CDbl(dr("NETAMOUNT")), 2)
                            Else
                                percentage = (dr("GROSSAMT") / totalGAmount) * 100
                                dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                                Dim strLINEDISCOUNT As Decimal = dr("LINEDISCOUNT")
                                Dim strGrossAmt As Decimal = dr("GrossAmt")
                                If strGrossAmt = 0 Then
                                    strGrossAmt = 1
                                End If

                                dr("TOTALDISCPERCENTAGE") = (strLINEDISCOUNT / strGrossAmt) * 100
                                ' dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                                dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                            End If

                        Else
                            percentage = ((dr("GROSSAMT") * ((((dr("QUANTITY") - dr("DELIVEREDQTY")) / dr("QUANTITY"))))) / totalGAmount) * 100
                            dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                            Dim strLINEDISCOUNT As Decimal = dr("LINEDISCOUNT")
                            Dim strGrossAmt As Decimal = dr("GrossAmt")
                            If strGrossAmt = 0 Then
                                strGrossAmt = 1
                            End If

                            dr("TOTALDISCPERCENTAGE") = (strLINEDISCOUNT / strGrossAmt) * 100
                            ' dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                            dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                            percentage = (dr("GROSSAMT") / totalGAmount) * 100
                            dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                            dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")

                            If TotalPickUpDisc IsNot Nothing Then
                                Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + dr("pkgLineNo").ToString() + "'")
                                If result.Length > 0 Then
                                    dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT") - result(0)("DiscountAmount") ' (dr("TOTALDISCOUNT") * ((dr("QUANTITY") - dr("DELIVEREDQTY")) / dr("QUANTITY"))) - result(0)("DiscountAmount")
                                Else
                                    dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                                End If
                            Else
                                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                            End If
                        End If
                        dr("AuthUserId") = txtUserId.Text.Trim()
                        dr("AuthUserRemarks") = txtUserId.Text.Trim()
                        dr("PROMOTIONID") = _Promotion
                        dr("TOPLEVEL") = _Promotion
                        dr("FirstLEVEL") = _Promotion
                        _NewTotalDiscount = txtValue.Text

                        'vipin PC SO Merge 03-05-2018
                    Next
                End If
            End If
            If rbItem.Checked = True Then
                If balancetoPay <> Decimal.Zero Then
                    Dim discountt As Decimal
                    For Each dr As DataRow In dtUserAuth.Select("", "Ean", DataViewRowState.CurrentRows)
                        discountt += dr("LINEDISCOUNT")
                    Next
                    If balancetoPay <= discountt Then
                        ShowMessage("Discount amount cannot be greater than balance to pay amount", "Information")
                        Exit Sub
                    End If
                End If
            End If

            Dim dvDtls = dtUserAuth.Select(String.Empty, String.Empty, DataViewRowState.CurrentRows)
            If (dvDtls.Count > 0) Then
                For rowIndex = 0 To dtUserAuth.Rows.Count - 1
                    For colIndex = 0 To dtUserAuth.Columns.Count - 1
                        dtCashMemoDtls(rowIndex)(colIndex) = dtUserAuth(rowIndex)(colIndex)
                    Next
                Next
            End If
            ShowMessage(getValueByKey("UA014"), "UA014 - " & getValueByKey("CLAE04"))
            IsPromoSelected = True
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub rbItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbItem.CheckedChanged
        Try
            SetDetialLayot()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub rbPercentage_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPercentage.CheckedChanged
        Try
            If rbPercentage.Checked = True Then
                If dgMainGrid.Cols.Contains("TotalDiscount") Then
                    dgMainGrid.Cols("TotalDiscount").AllowEditing = False
                    dgMainGrid.Cols("TotalDiscount").Visible = False
                End If
                If dgMainGrid.Cols.Contains("TOTALDISCPERCENTAGE") Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = True
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").Visible = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub rbValue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbValue.CheckedChanged
        Try
            If rbValue.Checked = True Then
                dgMainGrid.Cols("TotalDiscount").AllowEditing = True
                dgMainGrid.Cols("TotalDiscount").Visible = True
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").Visible = False
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If TransactionType = TransactionTypes.BL_ChngDisc.ToString() Then
            UpdateTran()
        Else
            Me.Close()
        End If

    End Sub
    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            If dgMainGrid.Cols(e.Col).Name.ToUpper() = "TOTALDISCOUNT" Then
                If dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") > dgMainGrid.Rows(e.Row)("GROSSAMT") Then
                    ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = 0.0
                    'Exit Sub
                End If
                dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = (dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") * 100) / dgMainGrid.Rows(e.Row)("GROSSAMT")
            ElseIf dgMainGrid.Cols(e.Col).Name.ToUpper() = "TOTALDISCPERCENTAGE" Then
                If dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") > 100 Then
                    ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = 0.0
                    'Exit Sub
                End If
                dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GROSSAMT")) / 100
            End If
            dgMainGrid.Rows(e.Row)("NETAMOUNT") = (dgMainGrid.Rows(e.Row)("GROSSAMT") + IIf(dgMainGrid.Rows(e.Row)("EXCLUSIVETAX") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("EXCLUSIVETAX"))) - dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")
            dgMainGrid.Rows(e.Row)("LINEDISCOUNT") = dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")
            dgMainGrid.Rows(e.Row)("AuthUserId") = txtUserId.Text.Trim
            dgMainGrid.Rows(e.Row)("AuthUserRemarks") = txtUserId.Text.Trim()
            dgMainGrid.Rows(e.Row)("PROMOTIONID") = _Promotion
            dgMainGrid.Rows(e.Row)("TOPLEVEL") = _Promotion
            dgMainGrid.Rows(e.Row)("FirstLEVEL") = _Promotion
            dgMainGrid.Rows(e.Row)("TOPLEVELDISC") = dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.BeforeEdit
        Try
            If dgMainGrid.Rows(dgMainGrid.Row)("BTYPE") = "S" Then
                If rbPercentage.Checked = True Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = True
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
                End If
                If rbValue.Checked = True Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = True
                End If
            Else
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgMainGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgMainGrid.KeyDown
        Try
            If dgMainGrid.Rows(dgMainGrid.Row)("BTYPE") = "S" Then
                If rbPercentage.Checked = True Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = True
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
                End If
                If rbValue.Checked = True Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = True
                End If
            Else
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Validate textbox entries
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BLValidate(Optional ByRef strErrorMsg As String = "") As Boolean
        If (txtTotalDiscountPercentage.Text = String.Empty) Then

            strErrorMsg = getValueByKey("UA009")
            'strErrorMsg = "Please enter discount percentage."
            Return False
        Else
            Try
                Dim decCal As Decimal = CDbl(txtTotalDiscountPercentage.Text)
                Return False
            Catch ex As Exception
                ShowMessage(getValueByKey("UA010"), "UA010 - " & getValueByKey("CLAE05"))
                'MessageBox.Show("Non numeric numbers are not allowed")
                Return True
            End Try
        End If
    End Function
    Private Sub btnApplyDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyDiscount.Click
        Try
            If (rbIssueCreditVoucher.Checked) Then
                IssueType = "CreditVoucher"
            Else
                IssueType = "GiftVoucher"
            End If
            IsFormCancel = False
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Function CalculateDiscount() As Boolean
        Try
            Dim decCalTotalDicountAmount As Decimal = Decimal.Zero
            Dim decTotalDiscountPer As Decimal
            If (txtTotalDiscountPercentage.Text = String.Empty) Then
                decTotalDiscountPer = Decimal.Zero
            Else
                decTotalDiscountPer = CDbl(txtTotalDiscountPercentage.Text)
            End If
            DiscountPercentage = decTotalDiscountPer
            decCalTotalDicountAmount = TotalAmount * (decTotalDiscountPer / 100)
            _decTotalDiscount = Decimal.Add(decCalTotalDicountAmount, _decTotalOpenAmount)
            txtTotalDiscountAmount.Text = FormatNumber(decCalTotalDicountAmount, 2)
            txtTotalApplicableAmountForVoucher.Text = FormatNumber(_decTotalDiscount, 2)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Sub txtTotalDiscountPercentage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalDiscountPercentage.TextChanged


        CalDiscount()
    End Sub

    Private Function CalDiscount() As Boolean
        Try
            If Not (BLValidate()) Then
                If Not (CalculateDiscount()) Then
                    ShowMessage(getValueByKey("UA011"), "UA011 - " & getValueByKey("CLAE04"))
                    'MessageBox.Show("Problem in discount calculation")
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Birthlist Discout cancel event 
    ''' </summary>
    ''' <CreatedBy>Rahul</CreatedBy>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelBirthlistDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelBirthlistDiscount.Click

        Try
            Dim dialogResult As DialogResult = MessageBox.Show(getValueByKey("UA012"), "UA012 - " & getValueByKey("CLAE04"), MessageBoxButtons.YesNo)
            If (dialogResult = Windows.Forms.DialogResult.Yes) Then
                IsFormCancel = True
                Me.Close()
            Else : dialogResult = Windows.Forms.DialogResult.No
                IsFormCancel = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnChangeDiscountPercentage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeDiscountPercentage.Click
        Try
            If (CheckAuthorisation(clsAdmin.UserCode, TransactionTypes.BL_ChngDisc.ToString())) Then
                txtTotalDiscountPercentage.Enabled = True
            Else
                TransactionType = TransactionTypes.BL_ChngDisc.ToString()
                sizTop.Enabled = True
                SetformLayout(1)
                Me.Height = 290

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function ThemeChange()

        Me.BackgroundColor = Color.FromArgb(134, 134, 134)


        Me.rbItem.ForeColor = Color.White
        Me.rbDocument.ForeColor = Color.White
        Me.Label1.ForeColor = Color.White

        Me.rbValue.ForeColor = Color.White
        Me.rbPercentage.ForeColor = Color.White

        'dgMainGrid
        '
        Me.dgMainGrid.Height = 235
        Me.dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.dgMainGrid.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.dgMainGrid.Styles.Highlight.ForeColor = Color.Black
        Me.dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgMainGrid.Rows.MinSize = 26
        Me.dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgMainGrid.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.dgMainGrid.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        Me.btnChangeDiscountPercentage.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnChangeDiscountPercentage.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnChangeDiscountPercentage.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnChangeDiscountPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChangeDiscountPercentage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnChangeDiscountPercentage.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnChangeDiscountPercentage.FlatStyle = FlatStyle.Flat
        Me.btnChangeDiscountPercentage.FlatAppearance.BorderSize = 0
        Me.btnChangeDiscountPercentage.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'cmdFlush
        '
        Me.cmdFlush.Location = New System.Drawing.Point(470, 340)
        Me.cmdFlush.Size = New System.Drawing.Size(93, 25)
        Me.cmdFlush.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdFlush.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdFlush.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.cmdFlush.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdFlush.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdFlush.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdFlush.FlatStyle = FlatStyle.Flat
        Me.cmdFlush.FlatAppearance.BorderSize = 0
        Me.cmdFlush.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.lblCurrVoucherAmt.BackColor = Color.White
        Me.lblCurrVoucherAmt.BackColor = Color.White
        Me.lblDiscountPercentage.BackColor = Color.White
        Me.lblAppAmount.BackColor = Color.White
        Me.lblCurrTotalDis.BackColor = Color.White
        Me.lblCurrVoucherAmt.BackColor = Color.White

        Me.btnChangeDiscountPercentage.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnChangeDiscountPercentage.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnChangeDiscountPercentage.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnChangeDiscountPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChangeDiscountPercentage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnChangeDiscountPercentage.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnChangeDiscountPercentage.FlatStyle = FlatStyle.Flat
        Me.btnChangeDiscountPercentage.FlatAppearance.BorderSize = 0
        Me.btnChangeDiscountPercentage.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)


        Me.btnCancelBirthlistDiscount.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancelBirthlistDiscount.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancelBirthlistDiscount.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.btnCancelBirthlistDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancelBirthlistDiscount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancelBirthlistDiscount.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancelBirthlistDiscount.FlatStyle = FlatStyle.Flat
        Me.btnCancelBirthlistDiscount.FlatAppearance.BorderSize = 0
        Me.btnCancelBirthlistDiscount.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
            If SendFromCSAScreen = True Then
                Me.Size = New Size(530, 165)
                lblUserid.Location = New Point(110, 13)
                lblPassWord.Location = New Point(110, 40)
                txtUserId.Location = New Point(210, 13)
                txtPassWord.Location = New Point(210, 40)
                cmdOk.Location = New Point(160, 91)
                cmdCancel.Location = New Point(270, 91)
            Else
                SendFromCSAScreen = False
            End If
        End If
        'lblUserid
        Me.lblUserid.BackColor = Color.Transparent
        Me.lblUserid.BorderStyle = BorderStyle.None
        Me.lblUserid.ForeColor = Color.White
        Me.lblUserid.Font = New Font("Verdana", 9.0!, FontStyle.Bold)
        Me.lblUserid.Text = Me.lblUserid.Text.ToUpper()

        'lblPassWord
        Me.lblPassWord.BackColor = Color.Transparent
        Me.lblPassWord.BorderStyle = BorderStyle.None
        Me.lblPassWord.ForeColor = Color.White
        Me.lblPassWord.Font = New Font("Verdana", 9.0!, FontStyle.Bold)
        Me.lblPassWord.Text = Me.lblPassWord.Text.ToUpper()
        'cmdOk
        Me.cmdOk.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System

        'cmdCancel
        Me.cmdCancel.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
    End Function
End Class
