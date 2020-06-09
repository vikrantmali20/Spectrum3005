Imports System.IO
Imports SpectrumBL
Imports System.Resources
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports SpectrumPrint
Imports C1.Win.C1BarCode
Imports System.Text

Public Class frmMembershipEnrollment
#Region "Global Variables"
    Protected controlList As New ArrayList
    Public pSearchCust As String = ""
    Dim GiftMsg As String = ""
    Dim objclsMemb As New clsMembership
    Dim dsMemb As New DataSet
    Dim dtItemData As New DataTable
    Dim dtItemDataCopy As New DataTable
    Dim membershiptypes As New DataTable
    Dim serviceType As New DataTable
    Dim SearchMembership As String = String.Empty
    Dim SearchCarType As String = String.Empty
    Dim SearchService As String = String.Empty
    Dim SearchCarNo As String = String.Empty
    Dim SearchTenderType As String = String.Empty
    Dim SearchMainPromo As String = String.Empty
    Dim SearchAddPromo As String = String.Empty
    Dim ctrlTxtPoints As String
    Dim CLPCardType As String
    Dim dtMainTax As New DataTable
    Dim CustomerBalancePoint As String
    Dim customerType As String
    Dim CLPCustomerId As String
    Dim dtMemberDetails As New DataTable
    Dim dtMemberDetailsHdr As New DataTable
    Dim dtCashMemohdrData As New DataTable
    Dim dtCashMemoBodyData As DataTable
    Dim dtcashmemofooterData As New DataTable
    Dim dtMemberPromoDetails As New DataTable
    Dim CarType As New DataTable
    Dim TenderType As New DataTable
    Dim PromotionType As New DataTable
    Dim StartDate As DateTime
    Dim EndDate As DateTime
    Dim path As String = ""
    Dim BarCodestring As String
    Dim AddPromotionType As New DataTable
    Dim CashMemoid As String = ""
    Dim dtView As New DataTable
    Dim Membershipid As String = String.Empty
    Dim dtScan As New DataTable
    Dim isCashierPromoSelected As Boolean
    Dim vIsPrintPreviewAllowed As Boolean = False
    Public _paidAmt As String
    Dim CustomerNo As String = ""
    Dim NetAmt As Double
    Dim Dtcombo As New DataTable
    Dim getColumnType As String = ""
    Dim StrTagLine As String
    Dim ds As DataSet
    Dim DtUnique As DataTable
    Dim _printType As String
    Public _billAmt As String
    Dim SearchBillNo As String = ""
    Dim GridData As DataTable
    Dim dtCust As DataTable
    Dim IsOnlysave As Boolean
    Dim OBjCM1 As New clsCommon
    Dim LoadExistingCust As DataTable
    Dim LoadExistingNewCust As DataTable
    Dim dtOldRemark As New DataTable




    Public _IscalledFromIsEnquiry As Integer
    Public Property IscalledFromIsEnquiry As Integer
        Get
            Return _IscalledFromIsEnquiry
        End Get
        Set(value As Integer)
            _IscalledFromIsEnquiry = value
        End Set
    End Property
    Public _IsEnquiryBillNo As String = ""
    Public Property IsEnquiryBillNo As String
        Get
            Return _IsEnquiryBillNo
        End Get
        Set(value As String)
            _IsEnquiryBillNo = value
        End Set
    End Property


    Public _CardNo As String = ""
    Public Property CardNo As String
        Get
            Return _CardNo
        End Get
        Set(value As String)
            _CardNo = value
        End Set
    End Property

    Public _IsEnquiryAmount As Decimal
    Public Property IsEnquiryAmount As Decimal
        Get
            Return _IsEnquiryAmount
        End Get
        Set(value As Decimal)
            _IsEnquiryAmount = value
        End Set
    End Property

#End Region

    Public _IsSameForm As Boolean = True
    Public Property IsSameForm As Boolean
        Get
            Return _IsSameForm
        End Get
        Set(value As Boolean)
            _IsSameForm = value
        End Set
    End Property
#Region "Function"
    ''' <summary>
    ''' Bind Membership data to gridview
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Public Sub BindSOItemGridData(ByVal dt As DataTable)
        Try
            grdScanItem.SuspendLayout()
            grdScanItem.DataSource = dt
            grdScanItem.ResumeLayout()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
    Public Sub LoadSummarySection()
        Try

            Dim GrossAmt As Decimal = 0
            Dim Disc As Decimal = 0

            For index As Integer = 1 To grdScanItem.Rows.Count - 1

                GrossAmt = GrossAmt + IIf(IsDBNull(grdScanItem.Rows(index)("Price")), 0, grdScanItem.Rows(index)("Price"))
                Disc = Disc + IIf(IsDBNull(grdScanItem.Rows(index)("Discount")), 0, grdScanItem.Rows(index)("Discount"))

            Next
            CtrlCashSummary1.CtrllblTaxAmt.Text = 0
            CtrlCashSummary1.CtrllblGrossAmt.Text = GrossAmt
            CtrlCashSummary1.CtrllblDiscAmt.Text = Disc
            CtrlCashSummary1.CtrllblNetAmt.Text = GrossAmt - Disc
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Display Customer informations 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SearchAndDisplayData(sender As Object, e As EventArgs)

        If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = "NEW" Then
            'If dtScan.Rows.Count = 0 Then
            '    controlList.Clear()
            'End If
            'If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
            '    dtScan.Clear()
            'End If
            ' BindSOItemGridData(dtScan)
            'gridsetting()
            'txtAddres.Text = ""
            'txtName.Text = ""
            'txtMobile.Text = ""
            Dim objCust As New frmSearchCustomer
            objCust.CheckTransactionRights = True
            objCust.FormName = Me.Name()
            Dim dialog = objCust.ShowDialog()
            If dialog = Windows.Forms.DialogResult.Cancel Then
                If CustomerNo = String.Empty Then
                    btnAdd.Enabled = False
                Else
                    btnAdd.Enabled = True
                End If
                Me.Close()
                Exit Sub
            End If

            'If clsDefaultConfiguration.DetailedCustomerCreationformat = "1" Then
            '    Exit Sub
            'End If
            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            dtCust = objCust.dtCustmInfo()
            txtName.Text = Convert.ToString(dtCust.Rows(0)("CustomerName"))
            CustomerNo = Convert.ToString(dtCust.Rows(0)("CustomerNo"))
            txtAddres.Text = Convert.ToString(dtCust.Rows(0)("ADDRESSLN1")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN2")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN3")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN4"))
            txtMobile.Text = Convert.ToString(dtCust.Rows(0)("MOBILENO"))
            If (dtCust.Rows(0)("CustomerType") = "CLP") Then
                ctrlTxtPoints = dtCust.Rows(0)("BALANCEPOINT").ToString()
                CustomerBalancePoint = dtCust.Rows(0)("BalancePoint").ToString()
            End If
            customerType = dtCust.Rows(0)("CustomerType")
            If CustomerNo = String.Empty Then
                Me.Close()
            Else
                btnAdd.Enabled = True
                If dtScan.Rows.Count = 0 Then
                    controlList.Clear()
                End If
                LoadExistingCust = OBjCM1.LoadExistingCustForMembership(CustomerNo)
                GettingDetailsofMembership()
                Exit Sub
            End If
        End If

        If IscalledFromIsEnquiry = 1 Then



            Dim dt As DataSet
            Dim objCustm As New clsCLPCustomer
            dtCust = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CardNo)
            dt = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, CardNo)
            '  dtCust = dt.Tables(0).Copy

            '  dtCust = objCust.dtCustmInfo()
            txtName.Text = Convert.ToString(dtCust.Rows(0)("CustomerName"))
            CustomerNo = Convert.ToString(dtCust.Rows(0)("CustomerNo"))
            txtAddres.Text = Convert.ToString(dtCust.Rows(0)("ADDRESSLN1")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN2")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN3")) & " " & Convert.ToString(dtCust.Rows(0)("ADDRESSLN4"))
            txtMobile.Text = Convert.ToString(dtCust.Rows(0)("MOBILENO"))
            If (dtCust.Rows(0)("CustomerType") = "CLP") Then
                ctrlTxtPoints = dtCust.Rows(0)("BALANCEPOINT").ToString()
                CustomerBalancePoint = dtCust.Rows(0)("BalancePoint").ToString()
            End If
            customerType = dtCust.Rows(0)("CustomerType")
            If CustomerNo = String.Empty Then
                Me.Close()
            Else
                btnAdd.Enabled = True
                If dtScan.Rows.Count = 0 Then
                    controlList.Clear()
                End If
                LoadExistingCust = OBjCM1.LoadExistingCustForMembership(CustomerNo)
                GettingDetailsofMembership()
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' Getting schema for membership And Cash Memo and bind to datatable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GettingDetailsofMembership()
        Try

            'If LoadExistingCust.Rows.Count > 0 Then
            '    For Each drRow1 In LoadExistingCust.Rows
            '        grdScanItem.Item(drRow1("SrNo"), "ArticleCode") = drRow1("ArticleCode")
            '        grdScanItem.Item(drRow1("SrNo"), "Period") = drRow1("PeriodName")
            '        grdScanItem.Item(drRow1("SrNo"), "StartDate") = drRow1("StartDate")
            '        grdScanItem.Item(drRow1("SrNo"), "EndDate") = drRow1("EndDate")
            '        grdScanItem.Item(drRow1("SrNo"), "Price") = drRow1("Amount")
            '        grdScanItem.Item(drRow1("SrNo"), "DIscountPer") = drRow1("DiscountPercentage")
            '        grdScanItem.Item(drRow1("SrNo"), "DIscount") = drRow1("DiscountAmount")
            '        grdScanItem.Item(drRow1("SrNo"), "NetAmt") = drRow1("NetAmount")
            '    Next
            'End If


            If dtScan.Rows.Count = 0 Then
                controlList.Clear()
            End If
            dtScan = objclsMemb.GetMembershipSchema
            dsMemb = objclsMemb.GetMembershipTableStructure(clsAdmin.SiteCode, CustomerNo)
            If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
                dtScan.Rows.Clear()
            End If
            If Not dsMemb Is Nothing AndAlso dsMemb.Tables(0).Rows.Count > 0 Then
                Dim index As Integer = 1
                For Each dr As DataRow In dsMemb.Tables(0).Rows
                    Dim SearchRefBill As String
                    If Not dr Is Nothing Then
                        SearchRefBill = dr("RefBillNo").ToString
                    End If
                    dsMemb = objclsMemb.GetMembershipTableStructure(clsAdmin.SiteCode, CustomerNo, SearchRefBill)
                    Dim rowDtls As DataRow = dtScan.NewRow
                    rowDtls("SrNo") = index
                    Dim drMembership() = membershiptypes.Select("ArticleCode='" & dr("MembershipCode") & "'")
                    If drMembership.Count > 0 Then
                        rowDtls("Membership") = drMembership(0)("ArticleShortName").ToString
                    End If
                    Dim drService() = serviceType.Select("ArticleCode='" & dr("ServiceCode") & "'")
                    If drService.Count > 0 Then
                        rowDtls("Service") = drService(0)("ArticleShortName").ToString
                    End If
                    Dim drCarType() = CarType.Select("ArticleCode='" & dr("CarTypeCode") & "'")
                    If drCarType.Count > 0 Then
                        rowDtls("CarType") = drCarType(0)("ArticleShortName").ToString
                    End If
                    rowDtls("CarNo") = dr("CarNo")

                    rowDtls("StartDate") = dr("StartDate").ToString
                    rowDtls("EndDate") = dr("EndDate").ToString
                    For Each drPromo As DataRow In dsMemb.Tables(1).Select("MemberShipId='" & dr("MemberShipId") & "'")
                        If IIf(drPromo("IsMainPromo") Is DBNull.Value, Nothing, drPromo("IsMainPromo")) Then
                            Dim drMainPromo() = PromotionType.Select("OfferNo='" & drPromo("PromotionId") & "' ") 'and IsMainPromo=True
                            If drMainPromo.Count > 0 Then
                                rowDtls("MainPromotion") = drMainPromo(0)("OfferName").ToString
                            End If
                        Else
                            Dim drAddPromoid() = AddPromotionType.Select("OfferNo='" & drPromo("PromotionId") & "'")
                            If drAddPromoid.Count > 0 Then
                                rowDtls("AddPromotion") = drAddPromoid(0)("OfferName").ToString
                            End If
                        End If
                    Next

                    Dim drTender() = dsMemb.Tables("CashMemoReceipt").Select("BillNo='" & dr("RefBillNo") & "'")
                    If drTender.Count > 0 Then
                        rowDtls("TenderType") = drTender(0)("TenderTypeCode").ToString()
                    End If
                    rowDtls("BillNo") = dr("RefBillNo")

                    index = index + 1
                    rowDtls("Sel") = False

                    dtScan.Rows.Add(rowDtls)

                Next

            End If
            BindSOItemGridData(dtScan)

            gridsetting()


            If LoadExistingCust.Rows.Count > 0 Then
                If LoadExistingCust.Rows(0)("isConvertedToMember") = True Then
                    grdScanItem.DataSource = LoadExistingCust
                    btnAdd.Visible = False
                    BtnSave.Visible = False
                    btnPayments.Visible = False
                    btnpayEnquiry.Visible = False
                    ShowMessage("Member Already Registered.", "Information")
                Else
                    btnAdd.Visible = False
                    BtnSave.Visible = False
                    btnPayments.Visible = False
                    btnpayEnquiry.Visible = True
                    btnPost.Visible = True
                End If
                '  LoadSummarySection()
            Else
                btnAdd.Visible = True
                BtnSave.Visible = True
                btnPayments.Visible = True
                btnpayEnquiry.Visible = False
            End If
            LoadExistingNewCust = LoadExistingCust.Copy

            ' DeleteColumnFromDataTable(LoadExistingCust, "isConvertedToMember")
            If LoadExistingCust.Columns.Contains("isConvertedToMember") Then
                LoadExistingCust.Columns.Remove("isConvertedToMember")
            End If

            If Not LoadExistingCust Is Nothing And LoadExistingCust.Rows.Count = 0 Then
                '  grdScanItem.DataSource = Nothing
            Else
                grdScanItem.DataSource = LoadExistingCust
            End If

            grdScanItem.Cols("ArticleCode").Width = 150
            grdScanItem.Cols("Period").Width = 150
            grdScanItem.Cols("StartDate").Width = 150
            grdScanItem.Cols("EndDate").Width = 150
            grdScanItem.Cols("Price").Width = 150
            grdScanItem.Cols("DiscountPer").Width = 150
            grdScanItem.Cols("Discount").Width = 150
            grdScanItem.Cols("NetAmt").Width = 150
            grdScanItem.AllowResizing = True

            grdScanItem.Cols("ArticleCode").Caption = "Service Description"
            grdScanItem.Cols("StartDate").Caption = "Start Date"
            grdScanItem.Cols("EndDate").Caption = "End Date"
            grdScanItem.Cols("DiscountPer").Caption = "Discount %"
            grdScanItem.Cols("NetAmt").Caption = "Net Amount"





            Dim GrossAmt As Decimal = 0
            Dim Disc As Decimal = 0

            If LoadExistingCust.Rows.Count > 0 Then
                For Each dtRow1 In LoadExistingCust.Rows
                    GrossAmt = GrossAmt + dtRow1("Price")
                    Disc = Disc + dtRow1("Discount")
                Next
                '  grdScanItem.Enabled = False
                'code is added by irfan on 13/4/2018 for membership.
                If LoadExistingCust.Rows.Count > 0 Then
                    If LoadExistingNewCust.Rows(0)("isConvertedToMember") = False Then
                        Dim DtPackgingTypes = objCM.GetArticlesForMember(clsAdmin.SiteCode)
                        Dim PackagingTypeList As String
                        PackagingTypeList = PackagingTypeList & " " & "|"
                        For index = 0 To DtPackgingTypes.Rows.Count - 1
                            PackagingTypeList = PackagingTypeList & DtPackgingTypes(index)(0) & "|"
                        Next index
                        If PackagingTypeList.Length > 0 Then
                            PackagingTypeList = PackagingTypeList.Substring(0, PackagingTypeList.Length - 1)
                        End If

                        grdScanItem.Cols("ArticleCode").AllowEditing = True
                        grdScanItem.Cols("ArticleCode").ComboList = PackagingTypeList
                        grdScanItem.Cols("Period").AllowEditing = False
                        grdScanItem.Cols("EndDate").AllowEditing = False
                        grdScanItem.Cols("Price").AllowEditing = False
                        grdScanItem.Cols("Discount").AllowEditing = False
                        grdScanItem.Cols("NetAmt").AllowEditing = False
                        fpRemarks.Visible = True
                        dtOldRemark = OBjCM1.DisplayReamrks(CustomerNo)
                        If Not dtOldRemark Is Nothing Then
                            For i = 0 To dtOldRemark.Rows.Count - 1
                                'count = i + 1
                                AddRemarks(i + 1, dtOldRemark.Rows(i)("Remarks"), dtOldRemark.Rows(i)("CreatedOn"))         ', oldremark.Rows(i)("CreatedOn")
                            Next
                            AddRemarks(0, "")          ', oldremark.Rows(i)("CreatedOn")
                        Else
                            AddRemarks(0, "")          ', oldremark.Rows(i)("CreatedOn")
                        End If


                    End If
                End If
            Else
                grdScanItem.Enabled = True
            End If
            CtrlCashSummary1.CtrllblTaxAmt.Text = 0
            CtrlCashSummary1.CtrllblGrossAmt.Text = GrossAmt
            CtrlCashSummary1.CtrllblDiscAmt.Text = Disc
            CtrlCashSummary1.CtrllblNetAmt.Text = GrossAmt - Disc

        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Sub

    ''' <summary>
    ''' Function For Adding Button of Generate Card Control in Grid
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer) As Boolean
        Try

            getColumnType = String.Empty
            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = grdScanItem.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange
            grdScanItem.Rows(rowIndex).HeightDisplay = 30

            Dim ButtonX As Integer = grdScanItem.Cols("GenerateCard").WidthDisplay

            'Create some new controls
            Dim btnBrowse As New CtrlBtn()
            ' btnBrowse.Tag = grdScanItem.Rows(rowIndex)("SrNo").ToString()
            btnBrowse.MaximumSize = New System.Drawing.Size(100, 60)
            'btnBrowse.SetRowIndex = rowIndex
            btnBrowse.Text = "Generate"
            btnBrowse.Name = "btnaction" '+ grdScanItem.Rows(rowIndex)("SaleOrderNumber").ToString()
            'Insert hosted control into grid
            btnBrowse.TabStop = False
            grdScanItem.Controls.Add(btnBrowse)

            'host them in the C1FlexGrid
            controlList.Add(New HostedControl(grdScanItem, btnBrowse, rowIndex, grdScanItem.Cols("GenerateCard").Index, ButtonX + 10, ButtonX + 10))

            'ImagePathRowIndex = rowIndex
            assignCellStyles = grdScanItem.GetCellRange(rowIndex, 3)
            assignCellStyles.Style = grdScanItem.Styles("CellImageType")

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
                btnBrowse.TextAlign = ContentAlignment.MiddleCenter
                btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
    End Function

    ''' <summary>
    ''' Adding button of Payment Control in grid
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function btnPayment(ByVal rowIndex As Integer) As Boolean
        Try
            getColumnType = String.Empty
            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

            cellStyle = grdScanItem.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True

            'Assign styles to editable cells
            Dim assignCellStyles As CellRange
            grdScanItem.Rows(rowIndex).HeightDisplay = 30

            Dim ButtonX As Integer = grdScanItem.Cols("Payment").WidthDisplay

            'Create some new controls
            Dim btnBrowse As New CtrlBtn()
            ' btnBrowse.Tag = grdScanItem.Rows(rowIndex)("Membership").ToString()
            btnBrowse.MaximumSize = New System.Drawing.Size(100, 60)
            'btnBrowse.SetRowIndex = rowIndex
            btnBrowse.Text = "Payment"
            btnBrowse.Name = "btnaction" '+ grdScanItem.Rows(rowIndex)("SaleOrderNumber").ToString()
            'Insert hosted control into grid
            btnBrowse.TabStop = False
            grdScanItem.Controls.Add(btnBrowse)

            'host them in the C1FlexGrid
            controlList.Add(New HostedControl(grdScanItem, btnBrowse, rowIndex, grdScanItem.Cols("Payment").Index, ButtonX + 10, ButtonX + 10))

            'ImagePathRowIndex = rowIndex
            assignCellStyles = grdScanItem.GetCellRange(rowIndex, 3)
            assignCellStyles.Style = grdScanItem.Styles("CellImageType")

            AddHandler btnBrowse.Click, AddressOf BrowsePaymentPath
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                btnBrowse.BackColor = Color.Transparent
                btnBrowse.BackColor = Color.FromArgb(0, 107, 163)
                btnBrowse.ForeColor = Color.FromArgb(255, 255, 255)
                btnBrowse.Font = New Font("Neo Sans", 8, FontStyle.Bold)
                btnBrowse.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                btnBrowse.FlatStyle = FlatStyle.Flat
                btnBrowse.FlatAppearance.BorderSize = 0
                btnBrowse.TextAlign = ContentAlignment.MiddleCenter
                btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Adding button for Promotion
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function btnAddPromo(ByVal rowIndex As Integer) As Boolean
        Try
            Dim getColumnType As String = String.Empty
            'Create styles with data types, formats, etc
            Dim cellStyle As C1.Win.C1FlexGrid.CellStyle
            cellStyle = grdScanItem.Styles.Add("CellImageType")
            cellStyle.DataType = Type.GetType("System.String")
            cellStyle.TextAlign = TextAlignEnum.LeftCenter
            cellStyle.WordWrap = True
            'Assign styles to editable cells
            Dim assignCellStyles As CellRange
            grdScanItem.Rows(rowIndex).HeightDisplay = 30

            Dim ButtonX As Integer = grdScanItem.Cols("AddPromotion").WidthDisplay

            'Create some new controls
            Dim btnBrowse As New CtrlBtn()
            ' btnBrowse.Tag = grdScanItem.Rows(rowIndex)("SrNo").ToString()
            btnBrowse.MaximumSize = New System.Drawing.Size(80, 60)
            'btnBrowse.SetRowIndex = rowIndex
            btnBrowse.Text = "Add Promo"
            btnBrowse.Name = "btnaction" '+ grdScanItem.Rows(rowIndex)("SaleOrderNumber").ToString()
            'Insert hosted control into grid
            btnBrowse.TabStop = False
            grdScanItem.Controls.Add(btnBrowse)

            'host them in the C1FlexGrid
            controlList.Add(New HostedControl(grdScanItem, btnBrowse, rowIndex, grdScanItem.Cols("AddPromotion").Index, ButtonX + 10, ButtonX + 10))

            'ImagePathRowIndex = rowIndex
            assignCellStyles = grdScanItem.GetCellRange(rowIndex, 3)
            assignCellStyles.Style = grdScanItem.Styles("CellImageType")

            AddHandler btnBrowse.Click, AddressOf BrowsePaymentPath
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
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Make Payment and save customer  Membership
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrepareandSaveData()
        Try
            'If Not ds Is Nothing Then

            objnew.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            'obj.IsInclusiveTax = IsInclusiveTax
            objnew.MainTable = "CASHMEMODTL"
            objnew.ExclusiveTaxFieldName = "EXCLUSIVETAX"
            objnew.TotalDiscountField = "TOTALDISCOUNT"
            objnew.GrossAmtField = "GROSSAMT"
            objnew.Condition = "BTYPE='S'"

            dsMain = objCM.GetStruc("0", "0")
            Dim dtTempItem As New DataTable
            If PrepareCashMemoDtlDataForSave(dtItemDataCopy) = False Then
                Exit Sub
            End If
            dsMain.Tables("CashMemoDtl").Merge(dsMemb.Tables("CashMemoDtl"))
            If dsMain.Tables("CashMemoDtl").Columns.Contains("LastNodeCode") Then
                dsMain.Tables("CashMemoDtl").Rows(0)("LastNodeCode") = dtItemData.Rows(0)("LastNodeCode")
            End If

            objnew.CalculatedDs(dsMain, clsAdmin.SiteCode)
            If PrepareCashMemoDtlDataForSave(dsMain.Tables("CashMemoDtl")) = False Then
                Exit Sub
            End If
            For Each row As DataRow In dsMemb.Tables("CashMemoDtl").Select("ISNULL(FIRSTLEVEL,'')<>'' OR ISNULL(TOPLEVEL,'')<>'' ", "EAN", DataViewRowState.CurrentRows)
                row("PROMOTIONID") = row("FIRSTLEVEL")
                If row("TopLEVEL").ToString() <> "" Then
                    row("PROMOTIONID") = row("PROMOTIONID") & "," & row("TOPLEVEL")
                End If
            Next

            If PrepareCashMemoHdrDataForSave(dsMemb) = False Then
                Exit Sub
            End If


            If PrepareClpCustomerServiceArticlePeriodMap(dsMemb) = False Then
                Exit Sub
            End If

            'If PrepareMemberShipDataforSave(dsMemb) = False Then
            '    Exit Sub
            'End If
            'If PrepareTaxdata(dsMemb) = False Then
            '    Exit Sub
            'End If

            'Dim ItemTax As Double = IIf(dsMemb.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", "") Is DBNull.Value, 0.0, dsMemb.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", ""))
            'Dim TotalItemTax As Double = IIf(dsMemb.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", "") Is DBNull.Value, 0.0, dsMemb.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", ""))

            'For Each drdtl As DataRow In dsMemb.Tables("CashMemoDtl").Rows
            '    Dim excTaxAmount = dtMainTax.Compute("SUM(TAXAMOUNT)", String.Format("Inclusive=0 AND BillLineNo={0}", drdtl("BillLineNo")))

            '    drdtl("NETAMOUNT") = drdtl("NETAMOUNT") + Math.Round(Val(excTaxAmount.ToString()), clsDefaultConfiguration.DecimalPlaces)
            '    NetAmt = drdtl("NETAMOUNT")
            '    drdtl("EXCLUSIVETAX") = Math.Round(Val(excTaxAmount.ToString()), clsDefaultConfiguration.DecimalPlaces)
            'Next
            'For Each drHdr As DataRow In dsMemb.Tables("CashMemoHdr").Rows
            '    drHdr("NetAmt") = FormatNumber(NetAmt, 0)
            '    drHdr("PaymentAmt") = FormatNumber(NetAmt, 0)
            '    drHdr("ExclusiveTax") = dsMemb.Tables("CashMemoDtl").Compute("SUM(ExclusiveTax)", "")
            '    drHdr("TaxAmount") = dsMemb.Tables("CashMemoDtl").Compute("SUM(TotalTaxAmount)", "")
            'Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub SaveAllData()

        If IscalledFromIsEnquiry = 0 Then


            If Not ds Is Nothing Then
                If PrepareCashMemoReceiptforSave(ds) = False Then
                    Exit Sub
                End If
            End If

            If objclsMemb.UpdateMemberShipDetails(dsMemb) Then
                If IsOnlysave = True Then
                    ShowMessage("Enquiry Saved  Successfully", "Information")
                Else
                    ShowMessage("Membership Saved Successfully", "Information")
                End If

                'Exit Sub
            End If
            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(CashMemoid, clsAdmin.SiteCode, clsAdmin.LangCode)
            DtUnique = obj.GetBillDetailsDataForPrint(CashMemoid, clsAdmin.SiteCode, clsAdmin.LangCode)
            Dtcombo = obj.GetComboDetailsDataForPrint(CashMemoid, clsAdmin.SiteCode, clsAdmin.LangCode)
            CashmemoHdrDetailsData()
            CashmemoBodyDetailsData()
            CashmemoFooterDetailsData()
            If IsOnlysave = False Then
                GenerateKOTDetailsPrint()
            End If
            CtrlCashSummary1.CtrllblTaxAmt.Text = 0
            CtrlCashSummary1.CtrllblGrossAmt.Text = 0
            CtrlCashSummary1.CtrllblDiscAmt.Text = 0
            CtrlCashSummary1.CtrllblNetAmt.Text = 0

        Else

            If Not ds Is Nothing Then
                If PrepareCashMemoReceiptforSave(ds) = False Then
                    Exit Sub
                End If
            End If

            Dim i As Int32 = 0
            For i = dsMemb.Tables.Count - 1 To 0 Step -1
                If Not dsMemb.Tables(i).TableName = "CashMemoReceipt" Then
                    dsMemb.Tables.RemoveAt(i)
                End If
            Next

            Dim objItem As New clsIteamSearch
            If objItem.SaveCashMemoReciptFromEnquiry(dsMemb, IsEnquiryBillNo, clsAdmin.SiteCode, clsAdmin.UserCode) Then
                ShowMessage("Payment Done Successfully", "Information")

                Dim obj As New SpectrumBL.clsCashMemo()
                dtView = obj.GetCashMemo(IsEnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
                DtUnique = obj.GetBillDetailsDataForPrint(IsEnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
                Dtcombo = obj.GetComboDetailsDataForPrint(IsEnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
                CashmemoHdrDetailsData()
                CashmemoBodyDetailsData()
                CashmemoFooterDetailsData()

                GenerateKOTDetailsPrint()
                ' Me.Close()
                If IsSameForm Then

                    CtrlCashSummary1.CtrllblTaxAmt.Text = 0
                    CtrlCashSummary1.CtrllblGrossAmt.Text = 0
                    CtrlCashSummary1.CtrllblDiscAmt.Text = 0
                    CtrlCashSummary1.CtrllblNetAmt.Text = 0


                    If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
                        dtScan.Clear()
                    End If
                    LoadExistingCust = OBjCM1.LoadExistingCustForMembership("")
                    BindSOItemGridData(dtScan)
                    gridsetting()
                    GettingDetailsofMembership()
                    CtrlCashSummary1.CtrllblTaxAmt.Text = 0
                    CtrlCashSummary1.CtrllblGrossAmt.Text = 0
                    CtrlCashSummary1.CtrllblDiscAmt.Text = 0
                    CtrlCashSummary1.CtrllblNetAmt.Text = 0



                Else
                    Me.Close()
                End If
            End If
        End If

    End Sub






    ''' <summary>
    ''' Grid setting
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub gridsetting()
        Try
            'andalso grdscanitem.cols(colno).name.toupper() <> "ean".toupper() _
            ' grdScanItem.AllowEditing = True
            '1=packaging types
            grdScanItem.Cols("Sel").DataType = Type.GetType("System.Boolean")
            grdScanItem.Cols("Sel").AllowEditing = True
            grdScanItem.Cols("Sel").Caption = "Select"
            grdScanItem.Cols("Sel").Width = 50
            Dim membershiplist As String = ""
            grdScanItem.Cols("SrNo").Visible = False
            grdScanItem.Cols("Del").ComboList = "..."
            grdScanItem.Cols("Del").Caption = ""
            grdScanItem.Cols("Del").Width = 25

            'For index = 0 To membershiptypes.Rows.Count - 1
            '    membershiplist = membershiplist & membershiptypes(index)(0) & "|"
            'Next index
            'If membershiplist.Length > 0 Then
            '    membershiplist = membershiplist.Substring(0, membershiplist.Length - 1)
            'End If

            'grdScanItem.Cols("Membership").Width = 140
            'grdScanItem.Cols("Membership").Caption = "Membership"
            'grdScanItem.Cols("Membership").AllowEditing = True
            'grdScanItem.Cols("Membership").ComboList = membershiplist

            '1=packaging types
            'Dim serviceTypelist As String = ""
            'For index = 0 To serviceType.Rows.Count - 1
            '    serviceTypelist = serviceTypelist & serviceType(index)(0) & "|"
            'Next index
            'If serviceTypelist.Length > 0 Then
            '    serviceTypelist = serviceTypelist.Substring(0, serviceTypelist.Length - 1)
            'End If

            'grdScanItem.Cols("Service").Width = 140
            'grdScanItem.Cols("Service").Caption = "Service"
            'grdScanItem.Cols("Service").AllowEditing = True
            'grdScanItem.Cols("Service").ComboList = serviceTypelist


            '1=packaging types
            'Dim CarTypelist As String = ""
            'For index = 0 To CarType.Rows.Count - 1
            '    CarTypelist = CarTypelist & CarType(index)(0) & "|"
            'Next index
            'If CarTypelist.Length > 0 Then
            '    CarTypelist = CarTypelist.Substring(0, CarTypelist.Length - 1)
            'End If
            'grdScanItem.Cols("CarType").Width = 140
            'grdScanItem.Cols("CarType").Caption = "Car type"
            'grdScanItem.Cols("CarType").AllowEditing = True
            'grdScanItem.Cols("CarType").ComboList = CarTypelist


            'grdScanItem.Cols("CarNo").Width = 100
            'grdScanItem.Cols("CarNo").DataType = Type.GetType("System.Numeric")
            'grdScanItem.Cols("CarNo").Format = "0"
            'grdScanItem.Cols("CarNo").Name = "CarNo"
            'grdScanItem.Cols("CarNo").TextAlign = TextAlignEnum.LeftCenter
            'grdScanItem.Cols("BillNo").Visible = False
            'grdScanItem.Cols("StartDate").Width = 120
            'grdScanItem.Cols("EndDate").Width = 120

            grdScanItem.Cols("StartDate").AllowEditing = True
            grdScanItem.Cols("StartDate").Format = "d"

            grdScanItem.Cols("EndDate").Format = "d"
            grdScanItem.Cols("EndDate").AllowEditing = True

            '1=packaging types
            Dim DtPackgingTypes = objCM.GetArticlesForMember(clsAdmin.SiteCode)
            Dim PackagingTypeList As String
            PackagingTypeList = PackagingTypeList & " " & "|"
            For index = 0 To DtPackgingTypes.Rows.Count - 1
                PackagingTypeList = PackagingTypeList & DtPackgingTypes(index)(0) & "|"
            Next index
            If PackagingTypeList.Length > 0 Then
                PackagingTypeList = PackagingTypeList.Substring(0, PackagingTypeList.Length - 1)
            End If


            grdScanItem.Cols("ArticleCode").AllowEditing = True
            grdScanItem.Cols("ArticleCode").ComboList = PackagingTypeList


            'Dim AddPromotionTypelist As String = ""
            'For index = 0 To AddPromotionType.Rows.Count - 1
            '    AddPromotionTypelist = AddPromotionTypelist & AddPromotionType(index)(0) & "|"
            'Next index
            'If AddPromotionTypelist.Length > 0 Then
            '    AddPromotionTypelist = AddPromotionTypelist.Substring(0, AddPromotionTypelist.Length - 1)
            'End If
            'grdScanItem.Cols("AddPromotion").Width = 165
            'grdScanItem.Cols("AddPromotion").Caption = "Additional Promotion"
            'grdScanItem.Cols("AddPromotion").AllowEditing = True
            'grdScanItem.Cols("AddPromotion").ComboList = AddPromotionTypelist

            'Dim TenderTypeList As String = ""
            'TenderTypeList = TenderTypeList & "Cash" & "|" & "Card"

            'grdScanItem.Cols("TenderType").Width = 99
            'grdScanItem.Cols("TenderType").Caption = "Tender Type"
            'grdScanItem.Cols("TenderType").AllowEditing = True
            'grdScanItem.Cols("TenderType").ComboList = TenderTypeList
            grdScanItem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            Me.grdScanItem.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            Me.grdScanItem.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
            Me.grdScanItem.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To grdScanItem.Cols.Count - 1
            '        grdScanItem.Cols(i).Caption = grdScanItem.Cols(i).Caption.ToUpper
            '    Next
            'End If
            grdScanItem.Cols("ArticleCode").Width = 200
            grdScanItem.Cols("Period").Width = 100
            grdScanItem.Cols("StartDate").Width = 100
            grdScanItem.Cols("EndDate").Width = 100
            grdScanItem.Cols("Price").Width = 80
            grdScanItem.Cols("DiscountPer").Width = 90
            grdScanItem.Cols("Discount").Width = 80
            grdScanItem.Cols("NetAmt").Width = 80
            grdScanItem.AllowResizing = True

            grdScanItem.Cols("ArticleCode").Caption = "Service Description"
            grdScanItem.Cols("StartDate").Caption = "Start Date"
            grdScanItem.Cols("EndDate").Caption = "End Date"
            grdScanItem.Cols("DiscountPer").Caption = "Discount %"
            grdScanItem.Cols("NetAmt").Caption = "Net Amount"
            grdScanItem.Cols("ArticleCode").AllowResizing = True
            grdScanItem.Cols("Period").AllowResizing = True
            grdScanItem.Cols("StartDate").AllowResizing = True
            grdScanItem.Cols("EndDate").AllowResizing = True
            grdScanItem.Cols("Price").AllowResizing = True
            grdScanItem.Cols("DiscountPer").AllowResizing = True
            grdScanItem.Cols("Discount").AllowResizing = True
            grdScanItem.Cols("NetAmt").AllowResizing = True

            grdScanItem.Cols("ArticleCode").AllowDragging = True
            grdScanItem.Cols("Period").AllowDragging = True
            grdScanItem.Cols("StartDate").AllowDragging = True
            grdScanItem.Cols("EndDate").AllowDragging = True
            grdScanItem.Cols("Price").AllowDragging = True
            grdScanItem.Cols("DiscountPer").AllowDragging = True
            grdScanItem.Cols("Discount").AllowDragging = True
            grdScanItem.Cols("NetAmt").AllowDragging = True
            grdScanItem.Cols("Sel").Visible = False
            '    grdScanItem.AutoSizeCol = True
            grdScanItem.Cols("Price").AllowEditing = False
            grdScanItem.Cols("Discount").AllowEditing = False
            grdScanItem.Cols("NetAmt").AllowEditing = False
            grdScanItem.Cols("Period").AllowEditing = False
            grdScanItem.Cols("EndDate").AllowEditing = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Preparing data for Membership Details
    ''' </summary>
    ''' <param name="dsMemb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepareMemberShipDataforSave(ByRef dsMemb As DataSet) As Boolean
        Dim findKey(2) As Object
        Try
            Dim ObjclsCommon As New clsCommon
            Dim objCM As New clsCashMemo()
            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim objType = "FO_DOC"
            Dim docno As String = objCM.getDocumentNo("Membershipid", clsAdmin.SiteCode, objType)
            Membershipid = GenDocNo("MEM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            If Not dsMemb Is Nothing Then
                Dim drNew As DataRow = dsMemb.Tables(0).NewRow
                drNew("MembershipId") = Membershipid
                drNew("SiteCode") = clsAdmin.SiteCode
                drNew("FinYear") = clsAdmin.Financialyear
                drNew("StartDate") = StartDate
                drNew("EndDate") = EndDate
                'drNew("EndDate") = ServerDate.AddYears(1).AddDays(-1)
                Dim drMembType() = membershiptypes.Select("ArticleShortName='" & SearchMembership & "'")
                If drMembType.Length > 0 Then
                    drNew("MembershipCode") = drMembType(0)("ArticleCode")
                End If
                Dim drServicetype() = serviceType.Select("ArticleShortName='" & SearchService & "'")
                If drServicetype.Length > 0 Then
                    drNew("ServiceCode") = drServicetype(0)("ArticleCode").ToString
                End If
                Dim drCartype() = CarType.Select("ArticleShortName='" & SearchCarType & "'")
                If drCartype.Length > 0 Then
                    drNew("CarTypeCode") = drCartype(0)("ArticleCode").ToString
                End If

                drNew("CarNo") = grdScanItem.Rows(grdScanItem.Row)("CarNo")
                drNew("CustomerNo") = CustomerNo
                drNew("MemberShipPrice") = dsMemb.Tables("CashMemoDtl").Compute("Sum(NetAmount)", "")
                drNew("CreatedAt") = clsAdmin.SiteCode
                drNew("CreatedBy") = clsAdmin.UserCode
                drNew("CreatedOn") = ServerDate
                drNew("UpdatedAt") = clsAdmin.SiteCode
                drNew("UpdatedBy") = clsAdmin.UserCode
                drNew("UpdatedOn") = ServerDate
                drNew("Status") = True
                drNew("RefBillNo") = CashMemoid
                dsMemb.Tables(0).Rows.Add(drNew)

                Dim lstPromo As New Dictionary(Of Boolean, String)
                lstPromo.Clear()
                Dim drmainPromo() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                If drmainPromo.Length > 0 Then
                    lstPromo.Add(1, drmainPromo(0)("OfferNo"))
                End If
                Dim drAddPromo() = AddPromotionType.Select("OfferName='" & SearchAddPromo & "'")
                If drAddPromo.Length > 0 Then
                    lstPromo.Add(0, drAddPromo(0)("OfferNo"))
                End If
                For lstLen = 0 To lstPromo.Count - 1
                    Dim drPromoNew As DataRow = dsMemb.Tables(1).NewRow
                    Dim PromoMapid As Integer = 1
                    Dim MainPromoIndex As Integer = 1
                    drPromoNew("MembershipId") = Membershipid
                    drPromoNew("SiteCode") = clsAdmin.SiteCode
                    drPromoNew("FinYear") = clsAdmin.Financialyear
                    drPromoNew("PromotionId") = lstPromo.Values(lstLen)
                    drPromoNew("IsMainPromo") = lstPromo.Keys(lstLen)
                    If Not dsMemb Is Nothing AndAlso dsMemb.Tables(1).Rows.Count > 0 Then
                        PromoMapid = dsMemb.Tables(1).Compute("Max(PromoMapId)", "") + 1
                    End If
                    drPromoNew("PromoMapId") = PromoMapid
                    drPromoNew("CreatedAt") = clsAdmin.SiteCode
                    drPromoNew("CreatedBy") = clsAdmin.UserCode
                    drPromoNew("CreatedOn") = ServerDate
                    drPromoNew("UpdatedAt") = clsAdmin.SiteCode
                    drPromoNew("UpdatedBy") = clsAdmin.UserCode
                    drPromoNew("UpdatedOn") = ServerDate
                    drPromoNew("Status") = True
                    dsMemb.Tables(1).Rows.Add(drPromoNew)
                Next
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Preparing data for Cashmemo  Details
    ''' </summary>
    ''' <param name="dsMemb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepareCashMemoDtlDataForSave(ByRef dtItemDataCopy As DataTable) As Boolean
        dsMemb.Tables("CashMemoDtl").Rows.Clear()
        Try
            Dim LineNo As Integer = 1
            For Each drItem As DataRow In dtItemDataCopy.Rows
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()

                Dim drdtl As DataRow = dsMemb.Tables("CashMemoDtl").NewRow()
                drdtl("SiteCode") = clsAdmin.SiteCode
                drdtl("FinYear") = clsAdmin.Financialyear
                drdtl("BillNo") = CashMemoid
                drdtl("BillLineNo") = LineNo
                drdtl("EAN") = drItem("EAN")
                drdtl("ArticleCode") = drItem("ArticleCode")
                drdtl("SellingPrice") = drItem("SellingPrice")
                drdtl("CostPrice") = drItem("CostPrice")
                drdtl("Quantity") = 1
                drdtl("GrossAmt") = drItem("SellingPrice") * drdtl("Quantity")
                If dtItemDataCopy.Columns.Contains("LineDiscount") Then
                    drdtl("TotalDiscount") = IIf(IsDBNull(drItem("LineDiscount")), 0, drItem("LineDiscount"))
                Else
                    drdtl("TotalDiscount") = IIf(IsDBNull(drItem("TotalDiscount")), 0, drItem("TotalDiscount"))
                End If
                drdtl("NetAmount") = drdtl("GrossAmt") - IIf(IsDBNull(drdtl("TOTALDISCOUNT")), 0, drdtl("TOTALDISCOUNT"))
                drdtl("BillDate") = clsAdmin.DayOpenDate.Date
                drdtl("BillTime") = ServerDate
                Dim drmainPromo() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                If drmainPromo.Length > 0 Then
                    '   drdtl("PromotionId") = drmainPromo(0)("OfferNo")
                End If
                If dtItemDataCopy.Columns.Contains("PromotionId") Then
                    drdtl("PromotionId") = IIf(drItem("PromotionId") Is Nothing, 0, drItem("PromotionId"))
                End If
                drdtl("SerialNbr") = drItem("SerialNbr")
                If dtItemDataCopy.Columns.Contains("LineDiscount") Then
                    drdtl("LineDiscount") = IIf(drItem("LineDiscount") Is Nothing, 0, drItem("LineDiscount"))
                End If
                drdtl("CLPRequire") = drItem("CLPRequire")
                drdtl("UnitofMeasure") = drItem("UnitofMeasure")
                If dtItemDataCopy.Columns.Contains("TotalDiscPercentage") Then
                    drdtl("TotalDiscPercentage") = (IIf(IsDBNull(drItem("LineDiscount")), 0, drItem("LineDiscount")) * 100) / drdtl("GROSSAMT")
                Else
                    drdtl("TotalDiscPercentage") = 0
                End If
                drdtl("ExclusiveTax") = drItem("ExclusiveTax")
                drdtl("TotalTaxAmount") = drItem("TotalTaxAmount")
                drdtl("AuthUserId") = clsAdmin.UserCode
                drdtl("AuthUserRemarks") = clsAdmin.UserCode
                drdtl("Btype") = drItem("Btype")
                Dim Month = clsAdmin.DayOpenDate.Month
                Dim Day = clsAdmin.DayOpenDate.Day
                Dim Quarter = Math.Ceiling(Month / 3)
                Dim Months = GetEnglishMonthNames(clsAdmin.DayOpenDate.Month)
                Months = Microsoft.VisualBasic.Strings.Left(Months, 3)

                drdtl("Quarter") = "Q" & Quarter
                drdtl("Month") = Months
                drdtl("Day") = Day
                drdtl("CREATEDAT") = clsAdmin.SiteCode
                drdtl("CREATEDBY") = clsAdmin.UserCode
                drdtl("CREATEDON") = ServerDate
                drdtl("UPDATEDAT") = clsAdmin.SiteCode
                drdtl("UPDATEDBY") = clsAdmin.UserCode
                drdtl("UPDATEDON") = ServerDate
                drdtl("STATUS") = True
                Dim drmainPromo1() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                'If drmainPromo.Length > 0 Then
                If dtItemDataCopy.Columns.Contains("FIRSTLEVELDISC") Then
                    drdtl("FIRSTLEVELDISC") = IIf(drItem("FIRSTLEVELDISC") Is Nothing, 0, drItem("FIRSTLEVELDISC"))
                End If
                If dtItemDataCopy.Columns.Contains("TOPLEVELDISC") Then
                    drdtl("TOPLEVELDISC") = IIf(drItem("TOPLEVELDISC") Is Nothing, 0, drItem("TOPLEVELDISC"))
                End If
                If dtItemDataCopy.Columns.Contains("FIRSTLEVEL") Then
                    drdtl("FIRSTLEVEL") = IIf(drItem("FIRSTLEVEL") Is Nothing, 0, drItem("FIRSTLEVEL"))
                End If
                If dtItemDataCopy.Columns.Contains("TOPLEVEL") Then
                    drdtl("TOPLEVEL") = IIf(drItem("TOPLEVEL") Is Nothing, 0, drItem("TOPLEVEL"))
                End If

                If dsMemb.Tables("CashMemoDtl").Columns.Contains("LastNodeCode") Then
                    drdtl("LastNodeCode") = drItem("LastNodeCode")
                End If



                'End If
                drdtl("BatchBarcode") = ""
                dsMemb.Tables("CashMemoDtl").Rows.Add(drdtl)
                LineNo = LineNo + 1
            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Preparing data for CashMemoHdr Details
    ''' </summary>
    ''' <param name="dsMemb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepareCashMemoHdrDataForSave(ByRef dsMemb As DataSet)
        Try
            dsMemb.Tables("CashMemoHdr").Rows.Clear()
            For Each drItem As DataRow In dtItemData.Rows
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()
                Dim LineNo As Integer = 1
                Dim drHdr As DataRow = dsMemb.Tables("CashMemoHdr").NewRow()
                drHdr("SiteCode") = clsAdmin.SiteCode
                drHdr("FinYear") = clsAdmin.Financialyear
                drHdr("BillNo") = CashMemoid
                drHdr("TerminalId") = clsAdmin.TerminalID
                drHdr("TransactionCode") = "CMS"
                drHdr("CostAmt") = dtItemData.Compute("Sum(CostPrice)", "")
                drHdr("GrossAmt") = dsMemb.Tables("CashMemoDtl").Compute("Sum(GrossAmt)", "")
                drHdr("NetAmt") = FormatNumber(dsMemb.Tables("CashMemoDtl").Compute("Sum(NetAmount)", ""), 0)
                NetAmt = dsMemb.Tables("CashMemoDtl").Compute("Sum(NetAmount)", "")
                drHdr("PaymentAmt") = dsMemb.Tables("CashMemoDtl").Compute("Sum(NetAmount)", "")
                drHdr("RoundedAmt") = dsMemb.Tables("CashMemoDtl").Compute("Sum(NetAmount)", "")
                drHdr("BillDate") = clsAdmin.DayOpenDate.Date
                drHdr("BillTime") = ServerDate
                Dim drmainPromo() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                If drmainPromo.Length > 0 Then
                    'drHdr("PromotionId") = drmainPromo(0)("OfferNo")
                End If
                drHdr("LineItems") = dsMemb.Tables("CashMemoDtl").Compute("Max(BillLineNo)", "")
                drHdr("TotalItems") = dsMemb.Tables("CashMemoDtl").Compute("Max(BillLineNo)", "")
                drHdr("BillInterMediateStatus") = "isclosed"
                drHdr("TotalDiscount") = dsMemb.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", "")

                drHdr("DISCOUNTAMT") = dsMemb.Tables("CASHMEMODTL").Compute("Sum(LINEDISCOUNT)", "")
                If drHdr("DISCOUNTAMT").ToString <> "" AndAlso Math.Abs(drHdr("DISCOUNTAMT")) > 0 Then
                    drHdr("DiscountPercentage") = (drHdr("DISCOUNTAMT") * 100) / drHdr("GROSSAMT")
                End If
                drHdr("CLPNo") = CustomerNo
                ' drHdr("TotalDiscount") = dsMemb.Tables("CASHMEMODTL").Compute("Sum(LINEDISCOUNT)", "")
                drHdr("ExclusiveTax") = drItem("ExclusiveTax")
                drHdr("TaxAmount") = drItem("TotalTaxAmount")
                Dim Month = clsAdmin.DayOpenDate.Month
                Dim Day = clsAdmin.DayOpenDate.Day
                Dim Quarter = Math.Ceiling(Month / 3)
                'Months = Format(DayOpendate, "MMM")
                Dim Months = GetEnglishMonthNames(clsAdmin.DayOpenDate.Month)
                Months = Microsoft.VisualBasic.Strings.Left(Months, 3)
                drHdr("Quarter") = "Q" & Quarter
                drHdr("Month") = Months
                drHdr("Day") = Day
                drHdr("CREATEDAT") = clsAdmin.SiteCode
                drHdr("CREATEDBY") = clsAdmin.UserCode
                drHdr("CREATEDON") = ServerDate
                drHdr("UPDATEDAT") = clsAdmin.SiteCode
                drHdr("UPDATEDBY") = clsAdmin.UserCode
                drHdr("UPDATEDON") = ServerDate
                drHdr("STATUS") = True

                If CustomerNo <> String.Empty Then 'AndAlso customerType.Equals("CLP") 

                    drHdr("CLPSCHEME") = clsAdmin.CLPProgram
                    drHdr("CLPPOINTS") = IIf(dsMemb.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", "") Is DBNull.Value, 0, dsMemb.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", ""))
                    drHdr("CLPDISCOUNT") = IIf(dsMemb.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", "") Is DBNull.Value, 0, dsMemb.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", ""))
                    drHdr("BALANCEPOINTS") = IIf(ctrlTxtPoints <> String.Empty, ctrlTxtPoints, 0) + IIf(drHdr("CLPPOINTS") Is DBNull.Value, 0, drHdr("CLPPOINTS"))
                End If
                dsMemb.Tables("CashMemoHdr").Rows.Add(drHdr)

            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    Private Function PrepareClpCustomerServiceArticlePeriodMap(ByRef dsMemb As DataSet)
        Try
            Dim Srno As Integer = 1
            dsMemb.Tables("ClpCustomerServiceArticlePeriodMap").Rows.Clear()
            For Each drItem As DataRow In GridData.Rows
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()
                Dim LineNo As Integer = 1
                Dim DrCmDtl As DataRow() = dsMemb.Tables("CashMemoDtl").Select("BillLineNo= '" & drItem("SrNo") & "'")
                Dim DtPeriod = objCM.GetGridDataForMemberOnArticleCodeSelection(drItem("ArticleCOde"))
                Dim drHdr As DataRow = dsMemb.Tables("ClpCustomerServiceArticlePeriodMap").NewRow()
                drHdr("CardNo") = dsMemb.Tables("CashMemoHdr").Rows(0)("ClpNo")
                If DtPeriod.Rows.Count > 0 Then
                    drHdr("PeriodId") = DtPeriod.Rows(0)("PeriodID")
                End If
                drHdr("ClpProgramID") = dtCust.Rows(0)("ClpProgramId")
                drHdr("SiteCode") = clsAdmin.SiteCode
                drHdr("SrNo") = DtPeriod.Rows(0)("SrNO")
                drHdr("EAN") = DtPeriod.Rows(0)("EAN")
                drHdr("BillNo") = dsMemb.Tables("CashMemoHdr").Rows(0)("BillNo")
                drHdr("Amount") = drItem("NetAmt")
                drHdr("isEnquiry") = 0
                drHdr("IsConvertedToMember") = 1
                drHdr("StartDate") = drItem("StartDate")
                drHdr("EndDate") = drItem("EndDate")
                drHdr("DiscountAmount") = drItem("Discount")
                drHdr("discountInPercentage") = drItem("DiscountPer")
                drHdr("CREATEDAT") = clsAdmin.SiteCode
                drHdr("CREATEDBY") = clsAdmin.UserCode
                drHdr("CREATEDON") = DateTime.Now()
                drHdr("UPDATEDAT") = clsAdmin.SiteCode
                drHdr("UPDATEDBY") = clsAdmin.UserCode
                drHdr("UPDATEDON") = DateTime.Now()
                drHdr("STATUS") = True
                dsMemb.Tables("ClpCustomerServiceArticlePeriodMap").Rows.Add(drHdr)
            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Preparing data for Cash Memo Receipt Details
    ''' </summary>
    ''' <param name="dsReceipt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrepareCashMemoReceiptforSave(ByRef dsReceipt As DataSet)
        Try
            For Each drReceipt As DataRow In dsReceipt.Tables(0).Rows
                Dim drRecData As DataRow = dsMemb.Tables("CashMemoReceipt").NewRow()
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()
                drRecData("SiteCode") = clsAdmin.SiteCode
                drRecData("FinYear") = clsAdmin.Financialyear
                If IscalledFromIsEnquiry = 1 Then
                    drRecData("BillNo") = IsEnquiryBillNo
                Else
                    drRecData("BillNo") = CashMemoid
                End If


                drRecData("CMRECPTLINENO") = drReceipt("SrNo")
                drRecData("TerminalId") = clsAdmin.TerminalID
                drRecData("CARDNO") = drReceipt("Number")
                drRecData("EXCHANGERATE") = drReceipt("ExchangeRate")
                drRecData("TENDERTYPECODE") = drReceipt("RecieptTypeCode")
                drRecData("AMOUNTTENDERED") = drReceipt("Amount")
                drRecData("CURRENCYCODE") = drReceipt("CurrencyCode")
                drRecData("AMOUNTINCURRENCY") = drReceipt("AmountInCurrency")
                drRecData("CmRcptDate") = clsAdmin.DayOpenDate.Date
                drRecData("CmRcptTime") = ServerDate
                drRecData("REFDATE") = drReceipt("Date")
                drRecData("CREATEDAT") = clsAdmin.SiteCode
                drRecData("CREATEDBY") = clsAdmin.UserCode
                drRecData("CREATEDON") = ServerDate
                drRecData("UPDATEDAT") = clsAdmin.SiteCode
                drRecData("UPDATEDBY") = clsAdmin.UserCode
                drRecData("UPDATEDON") = ServerDate
                drRecData("STATUS") = True
                drRecData("TENDERHEADCODE") = drReceipt("RecieptType")
                drRecData("BankAccNo") = drReceipt("BankAccNo")
                dsMemb.Tables("CashMemoReceipt").Rows.Add(drRecData)
            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function


    Private Function PrepareCashMemoServiceDtlDataForSave(ByRef dsMain As DataSet, ByVal dtServiceItem As DataTable) As Boolean
        Try
            For Each drItem As DataRow In dtServiceItem.Rows
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()
                Dim LineNo As Integer = 1
                Dim drdtl As DataRow = dsMain.Tables("CashMemoDtl").NewRow()
                drdtl("SiteCode") = clsAdmin.SiteCode
                drdtl("FinYear") = clsAdmin.Financialyear
                drdtl("BillNo") = CashMemoid
                drdtl("BillLineNo") = LineNo
                drdtl("EAN") = drItem("EAN")
                drdtl("ArticleCode") = drItem("ArticleCode")
                drdtl("SellingPrice") = drItem("SellingPrice")
                drdtl("CostPrice") = drItem("CostPrice")
                drdtl("Quantity") = 1
                drdtl("GrossAmt") = drItem("SellingPrice") * drdtl("Quantity")
                drdtl("NetAmount") = drItem("SellingPrice") * drdtl("Quantity")
                drdtl("BillDate") = clsAdmin.DayOpenDate.Date
                drdtl("BillTime") = ServerDate
                Dim drmainPromo() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                If drmainPromo.Length > 0 Then
                    drdtl("PromotionId") = drmainPromo(0)("OfferNo")
                End If
                drdtl("SerialNbr") = drItem("SerialNbr")
                drdtl("LineDiscount") = 0
                drdtl("CLPRequire") = drItem("CLPRequire")
                drdtl("UnitofMeasure") = drItem("UnitofMeasure")
                drdtl("TotalDiscount") = drItem("TotalDiscount")
                drdtl("TotalDiscPercentage") = 0
                drdtl("ExclusiveTax") = drItem("ExclusiveTax")
                drdtl("TotalTaxAmount") = drItem("TotalTaxAmount")
                drdtl("AuthUserId") = clsAdmin.UserCode

                drdtl("AuthUserRemarks") = clsAdmin.UserCode
                drdtl("Btype") = drItem("Btype")
                Dim Month = clsAdmin.DayOpenDate.Month
                Dim Day = clsAdmin.DayOpenDate.Day
                Dim Quarter = Math.Ceiling(Month / 3)
                Dim Months = GetEnglishMonthNames(clsAdmin.DayOpenDate.Month)
                Months = Microsoft.VisualBasic.Strings.Left(Months, 3)

                drdtl("Quarter") = "Q" & Quarter
                drdtl("Month") = Months
                drdtl("Day") = Day
                drdtl("CREATEDAT") = clsAdmin.SiteCode
                drdtl("CREATEDBY") = clsAdmin.UserCode
                drdtl("CREATEDON") = ServerDate
                drdtl("UPDATEDAT") = clsAdmin.SiteCode
                drdtl("UPDATEDBY") = clsAdmin.UserCode
                drdtl("UPDATEDON") = ServerDate
                drdtl("STATUS") = True
                Dim drmainPromo1() = PromotionType.Select("OfferName='" & SearchMainPromo & "'")
                'If drmainPromo.Length > 0 Then
                '    drdtl("FIRSTLEVELDISC") = drmainPromo(0)("OfferNo").ToString
                '    drdtl("TOPLEVELDISC") = drmainPromo(0)("OfferNo")
                'End If
                drdtl("LastNodeCode") = drItem("LastNodeCode")
                drdtl("BatchBarcode") = ""
                dsMain.Tables("CashMemoDtl").Rows.Add(drdtl)

            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    Dim dsMain As New DataSet
    Dim objCM As New clsCashMemo
    Dim strArticle As String = ""
    Dim Ean As String = ""
    Dim objnew As New clsApplyPromotion
    Private Function PrepareTaxdata(ByRef dsMemb As DataSet) As Boolean
        Try
            Dim sprice As Double = 0.0
            sprice = dtItemData.Rows(0)("SellingPrice")
            Ean = dtItemData.Rows(0)("EAN").ToString()
            Dim filterScanArticle As String = String.Empty
            Dim objArticleCombo As New clsArticleCombo
            Dim originalTaxAmt As Double
            Dim objfcm As New frmFastCashMemo
            If objArticleCombo.CheckIfComboArticle(strArticle) Then
                filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND SellingPrice = '{1}'", Ean.Trim(), sprice)
            Else
                filterScanArticle = String.Format("Btype='S' AND EAN='{0}'", Ean.Trim())
            End If
            Dim view As New DataView(dsMain.Tables("CASHMEMODTL"), filterScanArticle, "EAN", DataViewRowState.CurrentRows)
            dtMainTax = objCM.getTax("", "", "", "0", "")
            dtMainTax.TableName = "TAXDTLS"
            If view.Count = 0 Then
                objfcm.CreateDataSetForTaxCalculation(originalTaxAmt, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, dtItemData.Rows(0)("ArticleCode"), sprice, 1, 1, dtItemData.Rows(0)("EAN"), dtMainTax, dsMemb.Tables("CashMemoDtl"))
            Else
                objfcm.CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dtItemData.Rows(0)("ArticleCode"), sprice * view(0)("Quantity"), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString(), dtMainTax, dsMemb.Tables("CashMemoDtl"))
            End If
            If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then
                dsMemb.Tables.Add(dtMainTax)
            End If
            CreatingLineNO(dsMemb, "CASHMEMODTL")
            Dim serverDate As DateTime
            serverDate = GetCurrentDate()
            dsMemb.Tables("CashMemoTaxDtls").Clear()
            If Not dsMemb.Tables("TAXDTLS") Is Nothing AndAlso dsMemb.Tables("TAXDTLS").Rows.Count > 0 Then
                For Each drTaxFind As DataRow In dsMemb.Tables("TAXDTLS").Rows
                    Dim drTax As DataRow = dsMemb.Tables("CashMemoTaxDtls").NewRow
                    drTax("CreatedAt") = clsAdmin.SiteCode
                    drTax("CREATEDBY") = clsAdmin.UserCode
                    drTax("CREATEDON") = serverDate
                    drTax("STATUS") = 1
                    drTax("UPDATEDAT") = clsAdmin.SiteCode
                    drTax("UPDATEDBY") = clsAdmin.UserCode
                    drTax("UPDATEDON") = serverDate
                    drTax("SITECODE") = clsAdmin.SiteCode
                    drTax("BILLNO") = CashMemoid
                    drTax("BillLineNo") = drTaxFind("BillLineNo")
                    drTax("TaxLineNo") = drTaxFind("StepNo")
                    drTax("TaxLabel") = drTaxFind("TaxCode")
                    drTax("TaxValue") = drTaxFind("TaxAmount")
                    dsMemb.Tables("CashMemoTaxDtls").Rows.Add(drTax)
                Next
                dsMemb.Tables.Remove("TaxDtls")
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Function MemberShipDetailsHeaderData() As Boolean
        Try
            dtMemberDetailsHdr = objclsMemb.GetMembershipHeaderTableStruc()
            dtMemberDetailsHdr.Rows.Clear()
            Dim objcomm As New clsCommon
            Dim DtSiteInfo As DataTable = objcomm.GetSiteInfo(clsAdmin.SiteCode)
            If Not DtSiteInfo Is Nothing AndAlso DtSiteInfo.Rows.Count > 0 Then
                For Each drMembDetailssite As DataRow In DtSiteInfo.Rows
                    Dim drMemdetsitenew = dtMemberDetailsHdr.NewRow()
                    drMemdetsitenew("ClientName") = "M/s Lee Grand Car Spa"
                    drMemdetsitenew("SiteName") = drMembDetailssite("SiteOfficialName")
                    drMemdetsitenew("SiteAddress") = drMembDetailssite("SiteAddressLn1") + " " + drMembDetailssite("SiteAddressLn2") + "  Cell:" + drMembDetailssite("SiteTelephone2")
                    dtMemberDetailsHdr.Rows.Add(drMemdetsitenew)
                    Exit For
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    ''' Getting Membership details structure for Print
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MemberShipDetailsData() As Boolean
        Try
            dtMemberDetails = objclsMemb.GetMembershipTableStruc()
            dtMemberDetails.Rows.Clear()
            If Not dsMemb.Tables("MemberShipDetails") Is Nothing AndAlso dsMemb.Tables("MemberShipDetails").Rows.Count > 0 Then
                For Each drMembDetails As DataRow In dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & SearchBillNo & "'")
                    Dim drMemdetnew = dtMemberDetails.NewRow()

                    drMemdetnew("DetailsHeader") = "Yearly Membership Programme Form"
                    drMemdetnew("CustomerName") = txtName.Text
                    ' drMemdetnew("CustDate") = String.Empty
                    drMemdetnew("CustContact") = txtMobile.Text
                    drMemdetnew("CustAddress") = txtAddres.Text
                    drMemdetnew("CarModel") = String.Empty
                    drMemdetnew("CarNo") = drMembDetails("CarNo")
                    drMemdetnew("CarType") = SearchCarType
                    drMemdetnew("TotalService") = String.Empty
                    drMemdetnew("CustomerId") = CustomerNo
                    drMemdetnew("ModeOfPayment") = SearchTenderType
                    drMemdetnew("TotalAmount") = drMembDetails("MemberShipPrice")
                    drMemdetnew("StartDate") = drMembDetails("StartDate")
                    drMemdetnew("ExpiryDate") = drMembDetails("EndDate")
                    drMemdetnew("MembershipId") = drMembDetails("MembershipId")
                    dtMemberDetails.Rows.Add(drMemdetnew)
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    Private Function CashmemoHdrDetailsData() As Boolean
        Try
            dtCashMemohdrData = objclsMemb.CashmemoHdrStruct()
            dtCashMemohdrData.Rows.Clear()
            If Not dtView Is Nothing AndAlso dtView.Rows.Count > 0 Then
                For Each drHdr As DataRow In dtView.Rows
                    Dim drData = dtCashMemohdrData.NewRow()
                    drData("ClientName") = clsDefaultConfiguration.ClientName
                    drData("StoreName") = drHdr("SITEOFFICIALNAME")
                    drData("Address") = drHdr("ADDRESSLINE1") + drHdr("ADDRESSLINE2") + drHdr("ADDRESSLINE3") + drHdr("ADDRESSPINCODE")
                    drData("PhoneNumber") = drHdr("TELNO")
                    drData("CashMemoNo") = drHdr("BILLNO")
                    drData("Date") = drHdr("BILLDATE")
                    drData("Time") = drHdr("BILLTIME")
                    drData("Cashier") = drHdr("CREATEDBY")
                    drData("GiftMsg") = GiftMsg
                    drData("DineIn") = ""
                    drData("TokenNo") = drHdr("BILLNO").ToString().Trim.Substring(drHdr("BILLNO").ToString().Trim.Length - 2, 2)
                    dtCashMemohdrData.Rows.Add(drData)
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    Private Function CashmemoBodyDetailsData() As Boolean
        Try
            dtCashMemoBodyData = objclsMemb.GetCashmemoKOTBodyStruct()
            dtCashMemoBodyData.Rows.Clear()
            If Not DtUnique Is Nothing AndAlso DtUnique.Rows.Count > 0 Then
                For Each drBody As DataRow In DtUnique.Rows
                    Dim drBodyData = dtCashMemoBodyData.NewRow()
                    drBodyData("Description") = drBody("DISCRIPTION")
                    drBodyData("Qty") = drBody("QUANTITY")
                    drBodyData("Amt") = drBody("GROSSAMT")
                    dtCashMemoBodyData.Rows.Add(drBodyData)
                    ' Exit For
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Print Payment static information in the footer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CashmemoFooterDetailsData() As Boolean
        Try
            dtcashmemofooterData = objclsMemb.GetCashmemoKOTFooterStruct()
            dtcashmemofooterData.Rows.Clear()
            Dim VatTanString As String = "VAT/TIN NO: 27120029370 U/V"
            Dim LbtString As String = "LBT NO: TMC-LBT0005578-13"
            Dim msgString As String = "THANK YOU ... VISIT AGAIN"
            Dim drFooterData = dtcashmemofooterData.NewRow()

            drFooterData("VatTinString") = VatTanString
            drFooterData("LBTNo") = LbtString
            drFooterData("ThankYouMesg") = msgString
            dtcashmemofooterData.Rows.Add(drFooterData)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function


    ''' <summary>
    ''' Getting Membership Promo Data for Print
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MemberShipDetailsPromoData() As Boolean
        Try
            Dim MemberCode As String = ""
            Dim CarTypeCode As String = ""
            Dim ServiceTypeCode As String = ""
            dtMemberPromoDetails = objclsMemb.GetMemberPromotionTableStruc()
            dtMemberPromoDetails.Rows.Clear()
            If Not dsMemb.Tables("MemberShipDetails") Is Nothing AndAlso dsMemb.Tables("MemberShipDetails").Rows.Count > 0 Then
                Dim drMem() = dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & SearchBillNo & "'")
                If drMem.Count > 0 Then
                    Membershipid = drMem(0)("Membershipid")
                    MemberCode = drMem(0)("MembershipCode")
                    CarTypeCode = drMem(0)("CarTypeCode")
                    ServiceTypeCode = drMem(0)("ServiceCode")
                    Dim obj As New clsApplyPromotion
                    obj.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
                    'obj.IsInclusiveTax = IsInclusiveTax
                    obj.MainTable = "CASHMEMODTL"
                    obj.ExclusiveTaxFieldName = "EXCLUSIVETAX"
                    obj.TotalDiscountField = "TOTALDISCOUNT"
                    obj.GrossAmtField = "GROSSAMT"
                    obj.Condition = "BTYPE='S'"
                    Dim dsMain As New DataSet
                    Dim objCM As New clsCashMemo
                    dsMain = objCM.GetStruc("0", "0")
                    Dim dtServiceItem As New DataTable
                    dtServiceItem = objCM.GetItemDetails(clsAdmin.SiteCode, ServiceTypeCode, False, clsAdmin.LangCode, False)
                    PrepareCashMemoServiceDtlDataForSave(dsMain, dtServiceItem)
                    Dim drMembPromo() = dsMemb.Tables("MembershipPromoMapping").Select("MemberShipId='" & Membershipid & "' and IsMainPromo=True")
                    If drMembPromo.Count > 0 Then
                        obj.CalculatePromotionsByCustomer(dsMain, clsAdmin.SiteCode, drMembPromo(0)("PromotionId").ToString)
                    End If
                    Dim CarWashPrice = 0
                    If Not dsMain Is Nothing Then
                        If dsMain.Tables("CashMemoDtl").Rows.Count > 0 Then
                            CarWashPrice = dsMain.Tables("CashMemoDtl").Rows(0)("NetAmount").ToString()
                        End If
                    End If
                    Dim AddPromoamount = String.Empty
                    Dim drAddPromo() = dsMemb.Tables("MembershipPromoMapping").Select("MemberShipId='" & Membershipid & "' and IsMainPromo=False")
                    If drAddPromo.Length > 0 Then
                        AddPromoamount = objclsMemb.GetAddDiscount(drAddPromo(0)("Promotionid")).ToString
                    Else
                        AddPromoamount = String.Empty
                    End If
                    Dim drMemPromo As DataRow = dtMemberPromoDetails.NewRow()
                    Dim drMainPromo() = dsMemb.Tables("MembershipPromoMapping").Select("MemberShipId='" & Membershipid & "' and IsMainPromo=True")
                    drMemPromo("CarWashPrice") = CarWashPrice
                    drMemPromo("AdditionalServicedDiscount") = AddPromoamount
                    dtMemberPromoDetails.Rows.Add(drMemPromo)
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Generating Barcode Value 
    ''' </summary>
    ''' <param name="CodeString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImageToBase64(ByVal CodeString As String) As String
        Try
            Dim VarBarcode As C1BarCode
            Dim s As C1BarCode = GetBarcode(CodeString)
            VarBarcode = s
            Dim mImage = VarBarcode.Image
            Dim uPix As GraphicsUnit = GraphicsUnit.Pixel
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                mImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                ' Convert byte[] to Base64 String
                Dim base64String As String = Convert.ToBase64String(imageBytes)
                Return base64String
            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Printing Membership Card with Barcode scan
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateMemberShipDetailsPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\MembershipCard.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("BarCode", BarCodestring))
            Dim DataSource1 As New ReportDataSource("dsMemberShipHeader", dtMemberDetailsHdr)
            Dim DataSource2 As New ReportDataSource("dsMemberShipDetails", dtMemberDetails)
            Dim DataSource3 As New ReportDataSource("dsMemberPromoDetails", dtMemberPromoDetails)

            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\MemberShipCardDetails" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "CashMemo", "Billing")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Generate KOT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateKOTDetailsPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\KOTANDBILL.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim DataSource1 As New ReportDataSource("dsCashMemoHdr", dtCashMemohdrData)
            Dim DataSource2 As New ReportDataSource("dsCashmemodtls", dtCashMemoBodyData)
            Dim DataSource3 As New ReportDataSource("DsFooterDetails", dtcashmemofooterData)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\KOTANDBILL" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "CashMemo", "Billing")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        '        TableLayoutPanel2.
        grdScanItem.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdScanItem.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdScanItem.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdScanItem.Rows.MinSize = 30
        grdScanItem.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdScanItem.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        grdScanItem.Styles.Highlight.ForeColor = Color.Black
        grdScanItem.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdScanItem.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.Highlight.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdScanItem.CellButtonImage = Global.Spectrum.My.Resources.Delete

        btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnAdd.BackColor = Color.Transparent
        btnAdd.BackColor = Color.FromArgb(0, 107, 163)
        btnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        btnAdd.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.TextAlign = ContentAlignment.MiddleCenter
        btnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'btnAdd.Size = New Size(90, 30)
        'BtnSave
        BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSave.BackColor = Color.Transparent
        BtnSave.BackColor = Color.FromArgb(0, 107, 163)
        BtnSave.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSave.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        BtnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.FlatAppearance.BorderSize = 0
        BtnSave.TextAlign = ContentAlignment.MiddleCenter
        BtnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'btnpost is added by irfan on 13/4/2018 for membership.
        btnPost.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnPost.BackColor = Color.Transparent
        btnPost.BackColor = Color.FromArgb(0, 107, 163)
        btnPost.ForeColor = Color.FromArgb(255, 255, 255)
        btnPost.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnPost.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnPost.FlatStyle = FlatStyle.Flat
        btnPost.FlatAppearance.BorderSize = 0
        btnPost.TextAlign = ContentAlignment.MiddleCenter
        btnPost.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnpayEnquiry.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnpayEnquiry.BackColor = Color.Transparent
        btnpayEnquiry.BackColor = Color.FromArgb(0, 107, 163)
        btnpayEnquiry.ForeColor = Color.FromArgb(255, 255, 255)
        btnpayEnquiry.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnpayEnquiry.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnpayEnquiry.FlatStyle = FlatStyle.Flat
        btnpayEnquiry.FlatAppearance.BorderSize = 0
        btnpayEnquiry.TextAlign = ContentAlignment.MiddleCenter
        btnpayEnquiry.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)






        btnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSearch.BackColor = Color.Transparent
        btnSearch.BackColor = Color.FromArgb(0, 107, 163)
        btnSearch.ForeColor = Color.FromArgb(255, 255, 255)
        btnSearch.Size = New Size(90, 30)
        btnSearch.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnSearch.TextAlign = ContentAlignment.MiddleCenter
        btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnPayments.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnPayments.BackColor = Color.Transparent
        btnPayments.BackColor = Color.FromArgb(0, 107, 163)
        btnPayments.ForeColor = Color.FromArgb(255, 255, 255)
        'btnPayments.Size = New Size(90, 30)
        btnPayments.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnPayments.TextAlign = ContentAlignment.MiddleCenter
        btnPayments.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnPayments.FlatStyle = FlatStyle.Flat
        btnPayments.FlatAppearance.BorderSize = 0
        btnPayments.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnGenerateCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnGenerateCard.BackColor = Color.Transparent
        btnGenerateCard.BackColor = Color.FromArgb(0, 107, 163)
        btnGenerateCard.ForeColor = Color.FromArgb(255, 255, 255)
        ' btnGenerateCard.Size = New Size(90, 30)
        btnGenerateCard.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnGenerateCard.TextAlign = ContentAlignment.MiddleCenter
        btnGenerateCard.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnGenerateCard.FlatStyle = FlatStyle.Flat
        btnGenerateCard.FlatAppearance.BorderSize = 0
        btnGenerateCard.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        lblName.BackColor = Color.FromArgb(212, 212, 212)
        lblName.AutoSize = False
        lblName.Size = New Size(186, 31)
        ' lblName.BorderStyle = BorderStyle.None
        lblName.Location = New Point(4, 0)
        lblName.Margin = New Padding(0, 0, 0, 0)

        txtName.Margin = New Padding(0, 0, 0, 0)
        txtAddres.Margin = New Padding(0, 0, 0, 0)
        txtCustomerName.Margin = New Padding(0, 0, 0, 0)
        txtMobile.Margin = New Padding(0, 0, 0, 0)

        lblAddress.BackColor = Color.FromArgb(212, 212, 212)
        lblAddress.AutoSize = False
        lblAddress.Size = New Size(183, 45)
        lblAddress.Margin = New Padding(0, 0, 0, 0)

        ' lblAddress.BorderStyle = BorderStyle.None
        lblContact.Margin = New Padding(0, 0, 0, 0)
        lblContact.BackColor = Color.FromArgb(212, 212, 212)
        lblContact.AutoSize = False
        lblContact.Size = New Size(183, 26)


        CtrlLabel1.ForeColor = Color.White
        CtrlLabel1.BackColor = Color.Transparent

        lblHeader.ForeColor = Color.White
        lblHeader.AutoSize = False
        '    lblHeader.BorderStyle = BorderStyle.None

        lblMembDetails.ForeColor = Color.White
        lblMembDetails.AutoSize = False
        lblMembDetails.BackColor = Color.Transparent
    End Function
#End Region
#Region "Events"

    Private Sub grdScanItem_BeforeEdit(sender As Object, e As RowColEventArgs) Handles grdScanItem.BeforeEdit
        'If e.Row > 0 Then
        '    If grdScanItem.Cols(e.Col).Name = "Membership" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "Service" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "CarType" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "CarNo" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "MainPromotion" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "AddPromotion" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "TenderType" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "StartDate" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        '    If grdScanItem.Cols(e.Col).Name = "EndDate" Then
        '        If Not grdScanItem.Rows(e.Row)("BillNo") Is DBNull.Value Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        'End If
    End Sub



    ''' <summary>
    ''' Delete row in gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdScanItem.CellButtonClick
        Try
            '  Dim drDataFound() As DataRow = dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & grdScanItem.Rows(grdScanItem.Row)("BillNo") & "'")
            'If drDataFound.Length > 0 Then
            '    ShowMessage("You can not delete this record,Payment has been done", "Information")
            '    Exit Sub
            '          (grdScanItem.Rows.Count > 1) Then
            grdScanItem.Select(grdScanItem.Rows.Count - 1, 2)
            grdScanItem.Rows.Remove(grdScanItem.Row)

            BindSOItemGridData(dtScan)
            gridsetting()
            LoadSummarySection()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Update the position of Button in Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdScanItem_Paint(sender As Object, e As PaintEventArgs) Handles grdScanItem.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub

    ''' <summary>
    ''' Event For Making Payment for Membership
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BrowsePaymentPath(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    ''' <summary>
    ''' Event For Printing Card
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BrowseImagePath(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    ''' <summary>
    ''' Getting All structure Membership and Display 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMembershipEnrollment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            membershiptypes = objclsMemb.GetMembership()
            serviceType = objclsMemb.GetServices()
            CarType = objclsMemb.GetCarTypes()
            PromotionType = objclsMemb.GetMainPromotion(clsAdmin.SiteCode)
            AddPromotionType = objclsMemb.GetAdditionalPromotion(clsAdmin.SiteCode)
            SetCulture(Me, Me.Name)
            Dim objDefault As New clsDefaultConfiguration("CMS")
            objDefault.GetDefaultSettings()
            vIsPrintPreviewAllowed = clsDefaultConfiguration.PrintPreivewReq
            SearchAndDisplayData(sender, e)

            'CtrlCashSummary1.lbltxtVisible2 = False
            'CtrlCashSummary1.lblVisible2 = False

            'Dim DtFinalData As DataTable = objCM.GetAllCustomerServiceArticlePeriodMap(CustomerNo)
            'Dim DrRow1 As DataRow() = DtFinalData.Select("isEnquiry=1")
            'If DrRow1.Count > 0 Then
            '    btnpay.Visible = True
            'Else
            '    btnpay.Visible = False
            'End If
            '  ComboBox1.SelectedIndex = 0
            '  ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Adding New Entry in Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dr = dtScan.NewRow()
            Dim index As Integer = 1
            If Not dtScan Is Nothing AndAlso dtScan.Rows.Count > 0 Then
                index = dtScan.Compute("Max(SrNo)", "") + 1
            End If
            dr("SrNo") = index
            dr("Sel") = False
            dtScan.Rows.Add(dr)
            ' controlList.Clear()

            '---------------------------------------
            Dim DtPackgingTypes = objCM.GetArticlesForMember(clsAdmin.SiteCode)
            Dim PackagingTypeList As String
            PackagingTypeList = PackagingTypeList & " " & "|"
            For index = 0 To DtPackgingTypes.Rows.Count - 1
                PackagingTypeList = PackagingTypeList & DtPackgingTypes(index)(0) & "|"
            Next index
            If PackagingTypeList.Length > 0 Then
                PackagingTypeList = PackagingTypeList.Substring(0, PackagingTypeList.Length - 1)
            End If


            grdScanItem.Cols("ArticleCode").AllowEditing = True
            grdScanItem.Cols("ArticleCode").ComboList = PackagingTypeList

            '--------------------------------------------------------------------------------------
            BindSOItemGridData(dtScan)
            gridsetting()
            grdScanItem.Select(grdScanItem.Rows.Count - 1, 1)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Redirect to Search of Customer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            'If dtScan.Rows.Count = 0 Then
            '    controlList.Clear()
            'End If
            'If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
            '    dtScan.Clear()
            'End If
            '  BindSOItemGridData(dtScan)
            'gridsetting()
            txtAddres.Text = ""
            txtName.Text = ""
            txtMobile.Text = ""
            SearchAndDisplayData(sender, e)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
#End Region



    Private Sub btnGenerateCard_Click(sender As Object, e As EventArgs) Handles btnGenerateCard.Click
        Try
            If String.IsNullOrEmpty(CustomerNo) Then
                ShowMessage("Please Search Customer", "Information")
                Exit Sub
            End If
            If grdScanItem.Rows.Count = 1 Then
                ShowMessage("Please Add Membership", "Information")
                Exit Sub
            End If
            Dim SelCount As Integer = 0
            For i = 1 To grdScanItem.Rows.Count - 1
                If grdScanItem.Rows(i)("Sel") = True Then
                    SelCount = SelCount + 1
                End If
            Next
            If SelCount = 0 Then
                ShowMessage("Please Select one Membership", "Information")
                Exit Sub
            End If

            Dim drFoundData() As DataRow = dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & SearchBillNo & "'")
            If drFoundData.Count = 0 Then
                ShowMessage("Please Make Payment First and Then Generate Card", "Information")
                Exit Sub
            End If
            MemberShipDetailsHeaderData()
            MemberShipDetailsData()
            MemberShipDetailsPromoData()
            BarCodestring = ImageToBase64(Membershipid)
            GenerateMemberShipDetailsPrint()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnPayments_Click(sender As Object, e As EventArgs) Handles btnPayments.Click
        Try
            IsOnlysave = False
            GridData = grdScanItem.DataSource
            If String.IsNullOrEmpty(CustomerNo) Then
                ShowMessage("Please Search Customer", "Information")
                Exit Sub
            End If

            If grdScanItem.Rows.Count = 1 Then
                ShowMessage("Please Add Membership", "Information")
                Exit Sub
            End If
            Dim SelCount As Integer = 0
            For i = 1 To grdScanItem.Rows.Count - 1
                If grdScanItem.Rows(i)("Sel") = True Then
                    SelCount = SelCount + 1
                End If
            Next
            'If SelCount = 0 Then
            '    ShowMessage("Please Select one Membership", "Information")
            '    Exit Sub
            'End If
            Dim drDataFound() As DataRow = dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & SearchBillNo & "'")
            If drDataFound.Length > 0 Then
                ShowMessage("Payment Already Done,You can Generate Card", "Information")
                Exit Sub
            End If
            'If String.IsNullOrEmpty(SearchMembership) Then
            '    ShowMessage("Please Select MemberShip Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchService) Then
            '    ShowMessage("Please Select Service Type", "Information")
            '    Exit Sub
            'End If

            If StartDate = "#12:00:00 AM#" Then
                ShowMessage("Please Select Start Date", "Information")
                Exit Sub
            End If

            If EndDate = "#12:00:00 AM#" Then
                ShowMessage("Please Select End Date", "Information")
                Exit Sub
            End If

            'If String.IsNullOrEmpty(SearchCarType) Then
            '    ShowMessage("Please Select Car Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchCarNo) Then
            '    ShowMessage("Car Number is Mandatory", "Information")
            '    Exit Sub
            'End If
            '   If String.IsNullOrEmpty(SearchMainPromo) Then
            '    ShowMessage("Please Select Main Promotion Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchBillNo) Then
            '    If String.IsNullOrEmpty(SearchTenderType) Then
            '        ShowMessage("Please Select Tender Type", "Information")
            '        Exit Sub
            '    End If
            'End If
            Dim filterMembercode As String = ""
            Dim filterservicecode As String = ""
            Dim filtercartypecode As String = ""
            Dim drMembercode() = membershiptypes.Select("ArticleShortName='" & SearchMembership & "'")
            If drMembercode.Length > 0 Then
                filterMembercode = drMembercode(0)("ArticleCode").ToString
            End If
            Dim drServicecode() = serviceType.Select("ArticleShortName='" & SearchService & "'")
            If drServicecode.Length > 0 Then
                filterservicecode = drServicecode(0)("ArticleCode").ToString
            End If
            Dim drCartypeCode() = CarType.Select("ArticleShortName='" & SearchCarType & "'")
            If drCartypeCode.Length > 0 Then
                filtercartypecode = drCartypeCode(0)("ArticleCode").ToString
            End If

            'Dim drSearchEntry() = dsMemb.Tables("MemberShipDetails").Select("CarNo='" & SearchCarNo & "' AND EndDate >'" & DateTime.Now & "'")
            'If drSearchEntry.Count > 0 Then
            '    ShowMessage("Car Number should be unique", "Information")
            '    Exit Sub
            'End If

            Dim objcm As New clsCashMemo
            Dim objType = "FO_DOC"
            Dim docno As String = objcm.getDocumentNo("CM", clsAdmin.SiteCode, objType)

            Dim Month, day, Quarter As Int32
            Month = clsAdmin.DayOpenDate.Month
            day = clsAdmin.DayOpenDate.Day
            If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                CashMemoid = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            Else
                CashMemoid = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            End If
            Dim Id = DirectCast(sender, Button).Tag
            Dim EAN As String = ""
            Dim drMembType() = membershiptypes.Select("ArticleShortName='" & SearchMembership & "'")
            If drMembType.Length > 0 Then
                EAN = drMembType(0)("EAN")
            End If




            'Dim ArticleCode = GridData.Rows(0)("ArticleCode")
            'Dim Dttemo1 = objcm.GetGridDataForMemberOnArticleCodeSelection(ArticleCode)
            'EAN = Dttemo1.Rows(0)("EAN")
            'dtItemData = objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)
            ''Dim NetAmt
            'For Each dritem As DataRow In dtItemData.Rows
            '    dritem("Quantity") = 1
            '    NetAmt = dritem("SellingPrice") * dritem("Quantity")
            'Next

            Dim dtcopy As New DataTable
            For index As Integer = 1 To grdScanItem.Rows.Count - 1
                Dim ArticleCode = grdScanItem.Rows(index)("ArticleCode")
                Dim Dttemo1 = objcm.GetGridDataForMemberOnArticleCodeSelection(ArticleCode)
                EAN = Dttemo1.Rows(0)("EAN")
                dtItemData = objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)
                dtItemDataCopy = dtItemData.Copy  'objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)
                dtcopy.Merge(dtItemDataCopy)
            Next
            dtItemDataCopy.Clear()
            dtItemDataCopy.Merge(dtcopy)
            Dim indesxcopy As Integer = 0
            For Each dritem As DataRow In dtItemDataCopy.Rows
                dritem("Quantity") = 1
                dritem("SellingPrice") = GridData.Rows(indesxcopy)("NetAmt")
                indesxcopy = indesxcopy + 1
            Next




            PrepareandSaveData()
            SearchTenderType = "Cash"
            If SearchTenderType = "Cash" Then

                'Dim objPaymentByCash As New frmNAcceptPaymentByCash
                'objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
                'NetAmt = FormatNumber(NetAmt, 0)
                'objPaymentByCash.TotalBillAmount = CtrlCashSummary1.CtrllblNetAmt.Text


                Dim objPaymentByCash = New frmNAcceptPayment(False)

                objPaymentByCash.IsFastCashMemo = True

                objPaymentByCash.TotalBillAmount = CtrlCashSummary1.CtrllblNetAmt.Text
                NetAmt = FormatNumber(NetAmt, 0)
                objPaymentByCash.ParentRelation = "CashMemo"

                objPaymentByCash.TopMost = True
                objPaymentByCash.RoundAt = clsDefaultConfiguration.BillRoundOffAt





                objPaymentByCash.ShowDialog()

                If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                    If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        ds = objPaymentByCash.ReciptTotalAmount
                        _billAmt = objPaymentByCash.TotalBillAmount
                        _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                        objPaymentByCash.Close()
                        If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                            SaveAllData()
                        ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            GiftMsg = objPaymentByCash.GiftReceiptMessage
                            SaveAllData()
                        End If
                    Else
                        ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                    End If
                    If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
                        dtScan.Clear()
                    End If
                    BindSOItemGridData(dtScan)
                    gridsetting()
                    GettingDetailsofMembership()
                End If
            ElseIf SearchTenderType = "Card" Then
                Dim objPayment As New frmNAcceptPaymentByCard()
                objPayment.TotalBillAmount = CtrlCashSummary1.CtrllblNetAmt.Text
                objPayment.ShowDialog()
                Dim selectedTenderName As String = objPayment.SelectedTenderName
                Dim strCardTenderCode As String = objPayment.CardTenderCode
                objPayment.Close()
                If Not (objPayment.IsCancelAcceptPayment) Then
                    If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        ds = objPayment.ReciptTotalAmount
                        objPayment.Close()
                        If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                            SaveAllData()
                        ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            GiftMsg = objPayment.GiftReceiptMessage
                            SaveAllData()
                        End If
                    Else
                        ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                    End If
                    If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
                        dtScan.Clear()
                    End If
                    BindSOItemGridData(dtScan)
                    gridsetting()
                    GettingDetailsofMembership()
                End If

            End If
            'CtrlCashSummary1.CtrllblTaxAmt.Text = 0
            'CtrlCashSummary1.CtrllblGrossAmt.Text = 0
            'CtrlCashSummary1.CtrllblDiscAmt.Text = 0
            'CtrlCashSummary1.CtrllblNetAmt.Text = 0

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub grdScanItem_AfterEdit(sender As Object, e As RowColEventArgs) Handles grdScanItem.AfterEdit
        Dim CurrentCell As Integer = e.Col
        Dim CurrentRow As Integer = grdScanItem.Row '-- e.Row
        Dim SelectedArticleCode = IIf(grdScanItem.Item(CurrentRow, "ArticleCode") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "ArticleCode"))
        Dim DtMembershipData = objCM.GetGridDataForMemberOnArticleCodeSelection(SelectedArticleCode)
        If grdScanItem.Cols(e.Col).Name = "Sel" OrElse grdScanItem.Cols(e.Col).Name = "articlecode" Then
            'For i = 1 To grdScanItem.Rows.Count - 1
            '    If e.Row <> i Then
            '        grdScanItem.Rows(i)("Sel") = False
            '    End If
            'Next
            LoadSummarySection()
        End If
        If grdScanItem.Cols(e.Col).Name = "ArticleCode" Then


            Dim Enddate As DateTime = clsAdmin.DayOpenDate

            If Not DtMembershipData Is Nothing Then
                If DtMembershipData.Rows.Count > 0 Then
                    If DtMembershipData.Rows(0)("PeriodID") = 1 Then
                        Enddate = clsAdmin.DayOpenDate
                    ElseIf DtMembershipData.Rows(0)("PeriodID") = 2 Then
                        Enddate = DateAdd(DateInterval.Day, 7, clsAdmin.DayOpenDate)
                    ElseIf DtMembershipData.Rows(0)("PeriodID") = 3 Then
                        Enddate = DateAdd(DateInterval.Month, 1, clsAdmin.DayOpenDate)
                    ElseIf DtMembershipData.Rows(0)("PeriodID") = 4 Then
                        Enddate = DateAdd(DateInterval.Month, 3, clsAdmin.DayOpenDate)
                    ElseIf DtMembershipData.Rows(0)("PeriodID") = 5 Then
                        Enddate = DateAdd(DateInterval.Month, 6, clsAdmin.DayOpenDate)
                    ElseIf DtMembershipData.Rows(0)("PeriodID") = 6 Then
                        Enddate = DateAdd(DateInterval.Year, 1, clsAdmin.DayOpenDate)
                    End If
                    grdScanItem.Item(CurrentRow, "Period") = DtMembershipData.Rows(0)("PeriodName")
                    grdScanItem.Item(CurrentRow, "StartDate") = clsAdmin.DayOpenDate
                    grdScanItem.Item(CurrentRow, "EndDate") = Enddate
                    grdScanItem.Item(CurrentRow, "Price") = DtMembershipData.Rows(0)("Price")
                    grdScanItem.Item(CurrentRow, "DIscountPer") = 0
                    grdScanItem.Item(CurrentRow, "DIscount") = 0
                    grdScanItem.Item(CurrentRow, "NetAmt") = DtMembershipData.Rows(0)("Price")
                End If
            End If

        End If
        If grdScanItem.Cols(e.Col).Name = "DiscountPer" Then
            Dim DiscountPer = IIf(grdScanItem.Item(CurrentRow, "DiscountPer") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "DiscountPer"))
            Dim Price = IIf(grdScanItem.Item(CurrentRow, "Price") Is DBNull.Value, 0, grdScanItem.Item(CurrentRow, "Price"))
            grdScanItem.Item(CurrentRow, "Discount") = (Price * DiscountPer) / 100
            grdScanItem.Item(CurrentRow, "NetAmt") = grdScanItem.Item(CurrentRow, "Price") - grdScanItem.Item(CurrentRow, "Discount")
        End If
        If grdScanItem.Cols(e.Col).Name = "StartDate" Then
            If Not grdScanItem.Rows(e.Row)("StartDate") Is DBNull.Value Then
                If grdScanItem.Item(e.Row, "StartDate").Date < clsAdmin.DayOpenDate Then
                    ShowMessage("Start Date can not be back date", "SO009 - " & getValueByKey("CLAE04"))
                    grdScanItem.Item(e.Row, "StartDate") = clsAdmin.DayOpenDate
                    ' Disable date
                End If
                Dim enddate As Date = grdScanItem.Item(e.Row, "StartDate").Date
                grdScanItem.Item(e.Row, "EndDate") = enddate.AddYears(1)
            End If

            Dim Stardate As Date = grdScanItem.Item(CurrentRow, "StartDate")
            Dim Enddate1 As DateTime
            If DtMembershipData.Rows.Count > 0 Then
                If DtMembershipData.Rows(0)("PeriodID") = 1 Then
                    Enddate1 = Stardate
                ElseIf DtMembershipData.Rows(0)("PeriodID") = 2 Then
                    Enddate1 = DateAdd(DateInterval.Day, 7, Stardate)
                ElseIf DtMembershipData.Rows(0)("PeriodID") = 3 Then
                    Enddate1 = DateAdd(DateInterval.Month, 1, Stardate)
                ElseIf DtMembershipData.Rows(0)("PeriodID") = 4 Then
                    Enddate1 = DateAdd(DateInterval.Month, 3, Stardate)
                ElseIf DtMembershipData.Rows(0)("PeriodID") = 5 Then
                    Enddate1 = DateAdd(DateInterval.Month, 6, Stardate)
                ElseIf DtMembershipData.Rows(0)("PeriodID") = 6 Then
                    Enddate1 = DateAdd(DateInterval.Year, 1, Stardate)
                End If
                grdScanItem.Item(CurrentRow, "EndDate") = Enddate1
            End If
            Exit Sub
        End If
        If grdScanItem.Cols(e.Col).Name = "EndDate" Then
            If Not grdScanItem.Rows(e.Row)("StartDate") Is DBNull.Value AndAlso Not grdScanItem.Rows(e.Row)("EndDate") Is DBNull.Value Then
                If grdScanItem.Item(e.Row, "EndDate").Date < grdScanItem.Item(e.Row, "StartDate").Date Then
                    ShowMessage("End Date cannot be less than Start Date ", "SO009 - " & getValueByKey("CLAE04"))
                    grdScanItem.Item(e.Row, "EndDate") = Nothing
                    ' Disable date
                    Exit Sub
                End If
            End If

        End If
        If grdScanItem.Cols(e.Col).Name = "EndDate" Then
            If grdScanItem.Rows(e.Row)("StartDate") Is DBNull.Value Then
                ShowMessage("Select Start  Date", "SO009 - " & getValueByKey("CLAE04"))
                grdScanItem.Item(e.Row, "EndDate") = Nothing
            End If
        End If
        For i = 1 To grdScanItem.Rows.Count - 1
            '  If grdScanItem.Rows(i)("Sel") = True Then
            'SearchMembership = IIf(IsDBNull(grdScanItem.Rows(i)("MemberShip")), String.Empty, grdScanItem.Rows(i)("MemberShip"))
            'SearchService = IIf(IsDBNull(grdScanItem.Rows(i)("Service")), String.Empty, grdScanItem.Rows(i)("Service"))
            'SearchCarType = IIf(IsDBNull(grdScanItem.Rows(i)("CarType")), String.Empty, grdScanItem.Rows(i)("CarType"))
            ' grdScanItem.Rows(i)("CarNo") = IIf(IsDBNull(grdScanItem.Rows(i)("CarNo")), String.Empty, grdScanItem.Rows(i)("CarNo")).ToString.Replace("'", "").Trim
            ' SearchCarNo = grdScanItem.Rows(i)("CarNo")
            If grdScanItem.Rows(i)("StartDate") Is DBNull.Value Then
                StartDate = Nothing
            Else
                StartDate = Convert.ToDateTime(grdScanItem.Rows(i)("StartDate"))
            End If
            If grdScanItem.Rows(i)("EndDate") Is DBNull.Value Then
                EndDate = Nothing
            Else
                EndDate = Convert.ToDateTime(grdScanItem.Rows(i)("EndDate"))
            End If

            LoadSummarySection()
            'SearchMainPromo = IIf(IsDBNull(grdScanItem.Rows(i)("MainPromotion")), String.Empty, grdScanItem.Rows(i)("MainPromotion"))
            'SearchAddPromo = IIf(IsDBNull(grdScanItem.Rows(i)("AddPromotion")), String.Empty, grdScanItem.Rows(i)("AddPromotion"))
            'SearchBillNo = IIf(IsDBNull(grdScanItem.Rows(i)("BillNo")), String.Empty, grdScanItem.Rows(i)("BillNo"))
            'SearchTenderType = IIf(IsDBNull(grdScanItem.Rows(i)("TenderType")), String.Empty, grdScanItem.Rows(i)("TenderType"))
            ' End If
        Next
    End Sub
    Public Sub CreatingLineNO(ByRef ds As DataSet, ByVal TableName As String)
        Try
            Dim oldLineNo = 0, newLineNo As Integer = 0
            Dim dt As DataTable = ds.Tables(TableName)

            For i = 0 To dt.Rows.Count - 1
                oldLineNo = dt.Rows(i)("BillLineNo")
                newLineNo = i + 1

                dt.Rows(i)("BillLineNo") = newLineNo
                If ds.Tables.Contains("TAXDTLS") = True Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In ds.Tables("TAXDTLS").Select("BillLineNo = '" & oldLineNo & "' AND EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = newLineNo
                        dr("StepNo") = j
                        j = j + 1
                    Next
                End If

                If Not dtMainTax Is Nothing Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In dtMainTax.Select("BillLineNo = " & oldLineNo & " AND EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = newLineNo
                        dr("StepNo") = j
                        j = j + 1
                    Next
                End If

            Next

            'Set the Costprice of each line Item Except Return Item
            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, ds.Tables(TableName), clsAdmin.SiteCode, "costprice")
            'set the costprice of each line item except Return item

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            IsOnlysave = True
            GridData = grdScanItem.DataSource
            If String.IsNullOrEmpty(CustomerNo) Then
                ShowMessage("Please Search Customer", "Information")
                Exit Sub
            End If

            If grdScanItem.Rows.Count = 1 Then
                ShowMessage("Please Add Membership", "Information")
                Exit Sub
            End If
            Dim SelCount As Integer = 0
            For i = 1 To grdScanItem.Rows.Count - 1
                If grdScanItem.Rows(i)("Sel") = True Then
                    SelCount = SelCount + 1
                End If
            Next
            'If SelCount = 0 Then
            '    ShowMessage("Please Select one Membership", "Information")
            '    Exit Sub
            'End If
            Dim drDataFound() As DataRow = dsMemb.Tables("MemberShipDetails").Select("RefBillNo='" & SearchBillNo & "'")
            If drDataFound.Length > 0 Then
                ShowMessage("Payment Already Done,You can Generate Card", "Information")
                Exit Sub
            End If
            'If String.IsNullOrEmpty(SearchMembership) Then
            '    ShowMessage("Please Select MemberShip Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchService) Then
            '    ShowMessage("Please Select Service Type", "Information")
            '    Exit Sub
            'End If

            If StartDate = "#12:00:00 AM#" Then
                ShowMessage("Please Select Start Date", "Information")
                Exit Sub
            End If

            If EndDate = "#12:00:00 AM#" Then
                ShowMessage("Please Select End Date", "Information")
                Exit Sub
            End If

            'If String.IsNullOrEmpty(SearchCarType) Then
            '    ShowMessage("Please Select Car Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchCarNo) Then
            '    ShowMessage("Car Number is Mandatory", "Information")
            '    Exit Sub
            'End If
            '   If String.IsNullOrEmpty(SearchMainPromo) Then
            '    ShowMessage("Please Select Main Promotion Type", "Information")
            '    Exit Sub
            'End If
            'If String.IsNullOrEmpty(SearchBillNo) Then
            '    If String.IsNullOrEmpty(SearchTenderType) Then
            '        ShowMessage("Please Select Tender Type", "Information")
            '        Exit Sub
            '    End If
            'End If
            Dim filterMembercode As String = ""
            Dim filterservicecode As String = ""
            Dim filtercartypecode As String = ""
            Dim drMembercode() = membershiptypes.Select("ArticleShortName='" & SearchMembership & "'")
            If drMembercode.Length > 0 Then
                filterMembercode = drMembercode(0)("ArticleCode").ToString
            End If
            Dim drServicecode() = serviceType.Select("ArticleShortName='" & SearchService & "'")
            If drServicecode.Length > 0 Then
                filterservicecode = drServicecode(0)("ArticleCode").ToString
            End If
            Dim drCartypeCode() = CarType.Select("ArticleShortName='" & SearchCarType & "'")
            If drCartypeCode.Length > 0 Then
                filtercartypecode = drCartypeCode(0)("ArticleCode").ToString
            End If

            'Dim drSearchEntry() = dsMemb.Tables("MemberShipDetails").Select("CarNo='" & SearchCarNo & "' AND EndDate >'" & DateTime.Now & "'")
            'If drSearchEntry.Count > 0 Then
            '    ShowMessage("Car Number should be unique", "Information")
            '    Exit Sub
            'End If

            Dim objcm As New clsCashMemo
            Dim objType = "FO_DOC"
            Dim docno As String = objcm.getDocumentNo("CM", clsAdmin.SiteCode, objType)

            Dim Month, day, Quarter As Int32
            Month = clsAdmin.DayOpenDate.Month
            day = clsAdmin.DayOpenDate.Day
            If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                CashMemoid = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            Else
                CashMemoid = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
            End If
            Dim Id = DirectCast(sender, Button).Tag
            Dim EAN As String = ""
            Dim drMembType() = membershiptypes.Select("ArticleShortName='" & SearchMembership & "'")
            If drMembType.Length > 0 Then
                EAN = drMembType(0)("EAN")
            End If
            'Dim ArticleCode = GridData.Rows(0)("ArticleCode")
            'Dim Dttemo1 = objcm.GetGridDataForMemberOnArticleCodeSelection(ArticleCode)
            'EAN = Dttemo1.Rows(0)("EAN")
            'dtItemData = objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)






            Dim dtcopy As New DataTable
            For index As Integer = 1 To grdScanItem.Rows.Count - 1
                Dim ArticleCode = grdScanItem.Rows(index)("ArticleCode")
                Dim Dttemo1 = objcm.GetGridDataForMemberOnArticleCodeSelection(ArticleCode)
                EAN = Dttemo1.Rows(0)("EAN")
                dtItemData = objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)
                dtItemDataCopy = dtItemData.Copy  'objcm.GetItemDetails(clsAdmin.SiteCode, EAN, False, clsAdmin.LangCode, False)
                dtcopy.Merge(dtItemDataCopy)
            Next
            dtItemDataCopy.Clear()
            dtItemDataCopy.Merge(dtcopy)
            Dim indesxcopy As Integer = 0
            For Each dritem As DataRow In dtItemDataCopy.Rows
                dritem("Quantity") = 1
                dritem("SellingPrice") = GridData.Rows(indesxcopy)("NetAmt")
                indesxcopy = indesxcopy + 1
            Next



            'Dim NetAmt
            'For Each dritem As DataRow In dtItemData.Rows
            '    dritem("Quantity") = 1
            '    NetAmt = dritem("SellingPrice") * dritem("Quantity")
            'Next
            PrepareandSaveData()
            SearchTenderType = ""
            'If SearchTenderType = "Cash" Then

            '    Dim objPaymentByCash As New frmNAcceptPaymentByCash
            '    objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
            '    NetAmt = FormatNumber(NetAmt, 0)
            '    objPaymentByCash.TotalBillAmount = NetAmt
            '    objPaymentByCash.ShowDialog()

            '    If Not (objPaymentByCash.IsCancelAcceptPayment) Then
            '        If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
            '            ds = objPaymentByCash.ReciptTotalAmount
            '            _billAmt = objPaymentByCash.TotalBillAmount
            '            _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
            '            objPaymentByCash.Close()
            '            If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
            '                SaveAllData()
            '            ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
            '                GiftMsg = objPaymentByCash.GiftReceiptMessage
            '                SaveAllData()
            '            End If
            '        Else
            '            ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
            '        End If
            '        If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
            '            dtScan.Clear()
            '        End If
            '        BindSOItemGridData(dtScan)
            '        gridsetting()
            '        GettingDetailsofMembership()
            '    End If
            'ElseIf grdScanItem.Rows(grdScanItem.Row)("TenderType").ToString = "Card" Then
            '    Dim objPayment As New frmNAcceptPaymentByCard()
            '    objPayment.TotalBillAmount = NetAmt
            '    objPayment.ShowDialog()
            '    Dim selectedTenderName As String = objPayment.SelectedTenderName
            '    Dim strCardTenderCode As String = objPayment.CardTenderCode
            '    objPayment.Close()
            '    If Not (objPayment.IsCancelAcceptPayment) Then
            '        If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
            '            ds = objPayment.ReciptTotalAmount
            '            objPayment.Close()
            '            If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
            '                SaveAllData()
            '            ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
            '                GiftMsg = objPayment.GiftReceiptMessage
            '                SaveAllData()
            '            End If
            '        Else
            '            ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
            '        End If
            '        If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
            '            dtScan.Clear()
            '        End If
            '        BindSOItemGridData(dtScan)
            '        gridsetting()
            '        GettingDetailsofMembership()
            '    End If

            'End If
            For Each drMemb In dsMemb.Tables("ClpCustomerServiceArticlePeriodMap").Rows
                drMemb("isEnquiry") = 1
                drMemb("isConvertedToMember") = 0
            Next
            SaveAllData()
            If Not (dtScan Is Nothing) AndAlso dtScan.Rows.Count > 0 Then
                dtScan.Clear()
            End If
            BindSOItemGridData(dtScan)
            gridsetting()
            GettingDetailsofMembership()
            CtrlCashSummary1.CtrllblTaxAmt.Text = 0
            CtrlCashSummary1.CtrllblGrossAmt.Text = 0
            CtrlCashSummary1.CtrllblDiscAmt.Text = 0
            CtrlCashSummary1.CtrllblNetAmt.Text = 0

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnpayEnquiry_Click(sender As Object, e As EventArgs) Handles btnpayEnquiry.Click



        If IscalledFromIsEnquiry = 0 Then
            Dim objC As New clsCommon
            Dim EnquiryDt As New DataTable
            EnquiryDt = objC.GetCustomerEnquiryData(CustomerNo)
            IsEnquiryAmount = EnquiryDt(0)("Amount").ToString
            IsEnquiryBillNo = EnquiryDt(0)("BillNo").ToString
            IscalledFromIsEnquiry = 1
        End If
        '   If IscalledFromIsEnquiry = 1 Then


        Dim objPaymentByCash = New frmNAcceptPayment(False)

        ' objPaymentByCash.IsFastCashMemo = True

        objPaymentByCash.TotalBillAmount = IsEnquiryAmount
        NetAmt = FormatNumber(NetAmt, 0)
        objPaymentByCash.ParentRelation = "CashMemo"

        objPaymentByCash.TopMost = True
        objPaymentByCash.RoundAt = clsDefaultConfiguration.BillRoundOffAt




        '   Dim objPaymentByCash As New frmNAcceptPaymentByCash
        '  objPaymentByCash.TotalBillAmount = IsEnquiryAmount
        objPaymentByCash.ShowDialog()

        If Not (objPaymentByCash.IsCancelAcceptPayment) Then
            If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                ds = objPaymentByCash.ReciptTotalAmount
                ' _billAmt = objPaymentByCash.TotalBillAmount
                ' _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                objPaymentByCash.Close()
                If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                    SaveAllData()
                ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                    ' GiftMsg = objPaymentByCash.GiftReceiptMessage
                    SaveAllData()
                End If

                'dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, "")
                'grdCreditSales.DataSource = dtMembershipdata
                'GridColumnSettings()


            Else
                ' ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
            End If

        End If
        'code is added by irfan on 13/4/2018 for membership.
        grdScanItem.AllowEditing = False
        btnPost.Visible = False
        fpRemarks.Visible = False


        '    End If



    End Sub

    Private Sub txtPost_Click(sender As System.Object, e As System.EventArgs) Handles btnPost.Click
        Dim remarks As String = ""
        Dim value As String
        If Not dtOldRemark Is Nothing AndAlso dtOldRemark.Rows.Count > 0 Then
            value = Convert.ToString(dtOldRemark.Rows.Count - 1)
        Else
            value = "0"
        End If
        Dim txtAlternateTesxtBoxNumber = "txtAlternate" + value
        remarks = DirectCast(fpRemarks.Controls.Find(txtAlternateTesxtBoxNumber, True)(0), TextBox).Text.Trim()
        'TxtRemarks = DirectCast(fpRemarks.Controls.Find("txtAlternate0", True)(0), TextBox).Text.Trim()
        If remarks.Length > 350 Then
            ShowMessage("Remarks cannot be greater than 350 Characters.", "Information")
            Exit Sub
        End If

        Dim cardno As String = OBjCM1.GetCardforremarks(txtMobile.Text)
        Dim programid As String = OBjCM1.GetProgramId(clsAdmin.SiteCode)
        Dim sitecode As String = clsAdmin.SiteCode
        Dim CreatedOn As DateTime = clsAdmin.CurrentDate
        Dim CreatedBy As String = clsAdmin.UserCode
        Dim CreatedAt As String = clsAdmin.SiteCode
        Dim UpdatedOn As DateTime = clsAdmin.CurrentDate
        Dim UpdatedBy As String = clsAdmin.UserCode
        Dim UpdatedAt As String = clsAdmin.SiteCode

        If OBjCM1.SaveServiceArticleRemarks(cardno, programid, sitecode, remarks, CreatedOn, CreatedBy, CreatedAt, UpdatedOn, UpdatedBy, UpdatedAt) Then
            ShowMessage("Remarks saved successfully.", "Inforamtion")
        Else
            Exit Sub
        End If

        fpRemarks.Controls.Clear()
        dtOldRemark = OBjCM1.DisplayReamrks(CustomerNo)
        If dtOldRemark.Rows.Count > 0 Then
            ' dtOldRemark.Rows.Add("")
            For i = 0 To dtOldRemark.Rows.Count - 1
                'count = i + 1
                AddRemarks(i + 1, dtOldRemark.Rows(i)("Remarks"), dtOldRemark.Rows(i)("CreatedOn"))
            Next
            AddRemarks(0, "")
        Else
            AddRemarks(0, "")
        End If



    End Sub

    Dim lb As Label
    Dim count As Integer
    ' Dim CreatedTime As DateTime = clsAdmin.CurrentDate
    Dim TempStrLastRemarkIdForGrvHistory As String = ""
    Dim txt As Spectrum.CtrlTextBox
    Dim toolTip As ToolTip
    Dim pn As TableLayoutPanel
    Dim TxtboxLastCount
    Public Sub AddRemarks(Optional ByRef count As Integer = 0, Optional ByRef oldremark As String = "", Optional ByRef curdate As DateTime? = Nothing)
        Try
            lb = New Label()
            lb.AutoSize = True

            Dim tip As ToolTip = New ToolTip()

            Dim _rrmktemp As String = ""
            If count = 0 Then
                lb.Text = "Remark "
            ElseIf String.IsNullOrEmpty(oldremark) Then
                lb.Text = "Remark   " & count & ""
            Else
                ' lb.Text = "Remark   " & count & "  " + curdate.ToString("MM/dd/yyyy HH:mm:ss")
                ' _rrmktemp = "Remark   " & count & "  " + " Changed on :" + curdate.ToString("MM/dd/yyyy HH:mm:ss")


                lb.Text = "Remark   " & count & "  " + CDate(curdate).ToString 'curdate.ToString("MM/dd/yyyy HH:mm:ss")
                _rrmktemp = "Remark   " & count & "  " + " Changed on :" + CDate(curdate).ToString 'curdate.ToString("MM/dd/yyyy HH:mm:ss")

            End If

            lb.Padding = New Padding(0, 10, 0, 4)
            lb.TextAlign = ContentAlignment.TopLeft

            txt = New Spectrum.CtrlTextBox
            txt.Multiline = True

            txt.Name = "txtAlternate" & count
            'txt.Size = New System.Drawing.Size(830, 21)
            txt.Size = New System.Drawing.Size(600, 28)

            txt.Dock = DockStyle.Fill
            txt.Text = oldremark
            txt.MaxLength = 1000

            Dim size As Size = TextRenderer.MeasureText(oldremark, txt.Font)
            Dim factor As Integer = Math.Ceiling(size.Width / txt.Width)
            ''txt.Width = size.Width
            'txt.Height = txt.Height + ((factor - 1) * size.Height)

            If 5 + size.Height > txt.Height + ((factor - 1) * size.Height) Then
                txt.Height = 6 + size.Height

            Else
                txt.Height = txt.Height + ((factor - 1) * size.Height)

            End If

            txt.Margin = New Padding(5, 4, 0, 0)
            Dim btn As New LinkLabel

            btn.Size = New Size(150, 21)

            If oldremark = "" Then
                txt.Enabled = True
                btn.Enabled = True
            Else
                toolTip = New ToolTip
                txt.Enabled = False
                txt.ReadOnly = True
            End If

            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            pn.AutoScroll = False
            'pn.MaximumSize = New System.Drawing.Size(840, 900)
            pn.MaximumSize = New System.Drawing.Size(900, 900)
            pn.RowCount = 2
            ' pn.ColumnCount = 1
            pn.ColumnCount = 2
            'pn.SetColumnSpan(lb, 2)
            pn.Controls.Add(lb, 0, 0)
            pn.Controls.Add(txt, 0, 1)
            pn.Controls.Add(txt, 0, 1)
            pn.Controls.Add(btn, 1, 1)


            fpRemarks.Controls.Add(pn)

            'fpRemarks.MinimumSize = New Size(900, 225)
            'fpRemarks.MaximumSize = New Size(900, 225)
            'fpRemarks.Size = New Size(900, 225)
            pn.ResumeLayout()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

End Class



