Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Imports SpectrumPrint
Imports System.IO
Public Class frmGenerateBarcode
    Private _dtCreditSales As DataTable
    Dim Objcm As New clsCommon
    'Dim Objcom As cls
    Public Property dtCreditSales() As DataTable
        Get
            Return _dtCreditSales
        End Get
        Set(ByVal value As DataTable)
            _dtCreditSales = value
        End Set
    End Property
    Private _PopupType As String
    Public Property PopupType() As String
        Get
            Return _PopupType
        End Get
        Set(ByVal value As String)
            _PopupType = value
        End Set
    End Property


    Dim dtArticle As New DataTable
    Dim DtGridData As New DataTable
    Dim ObjC As New clsCommon
    Dim Articledata As New DataTable
    Dim dtHierarchyArticle As New DataTable
    'Dim ColumnChecked As Boolean = True
    Dim screenWidth As Integer = 0
    Dim screenHeight As Integer = 0
    Dim ArticleCodeDescLen As Integer = 0

#Region "Events"
    Private Sub frmProductNotificationPopups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            screenWidth = Screen.PrimaryScreen.Bounds.Width
            screenHeight = Screen.PrimaryScreen.Bounds.Height
            If screenWidth > 1024 Then
                Me.Size = New Size(Me.Size.Width + 230, Me.Size.Height)
            End If

            'Dim condition As String

            'Dim dtBind As DataTable = Objcm.GetComboArticle(clsAdmin.SiteCode)
            'If dtBind.Rows.Count > 0 Then

            'End If


            'Dim dtGroupID As DataTable = Objcm.GetGroupDtl(clsAdmin.SiteCode)
            'If dtGroupID.Rows.Count > 0 Then
            '    ' Call SetWildSearchTextBox(dtGroupID, TxtGroupId, key:="Groupid", Value:="GROUPNAME", searchData:="GroupIdName")
            'End If

            'dtGroupID.Clear()
            'dtGroupID = Objcm.GetGroupDtl(clsAdmin.SiteCode)
            'If dtGroupID.Rows.Count > 0 Then
            '    'Call SetWildSearchTextBox(dtGroupID, TxtUpgradableGroup, key:="Groupid", Value:="GROUPNAME", searchData:="GroupIdName")
            'End If

            'condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "' AND  A.ArticalTypeCode in ('Kit','Single')"
            'Dim GdtAllArticle = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)



            'dtGuest = objArticle.GetDetailsForGuest()
            'dtGuest.Clear()

            'DtUpGradableItem = objArticle.GetDetailsForGuest()
            'DtUpGradableItem.Clear()

            ''GrpAddStep.Visible = True
            'BlankItemDetailSection()
            'DtArticleCOmbo = objItem.GetMstArticleComboStruct()

            Dim condition As String
            Dim objItem As New clsIteamSearch
            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
            ' Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            Dim dtBind = objItem.GetEANDataforLablePrint(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            dtHierarchyArticle = dtBind.Copy
            If dtBind.Rows.Count > 1 Then
                ArticleCodeDescLen = dtHierarchyArticle.AsEnumerable().Select(Function(r) r.Field(Of String)("ArticleCodeDesc").Length).Max()
                'Dim listSource As List(Of [String]) = (From row In dtBind Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("Discription"))).Distinct().ToList()
                'CtrlSalesPersons.AndroidSearchTextBox.lstNames = listSource
                Call SetWildSearchTextBox(dtBind, TxtArticleFliter, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If




            DtGridData = Objcm.GetStructOfBarcodeArticle()

            RadioHierarchy_Click(sender, e)
            '  GrdShowData.DataSource = DtGridData
            GridSetting()

            lblHierarchyName.Text = ""
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
                Me.GrdShowData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black
            Else
                Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Blue

            End If

            If screenWidth > 1024 Then
                TxtArticleFliter.IsSetLocation = True
                TxtArticleFliter.ListBoxXCoordinate = 253
                TxtArticleFliter.ListBoxYCoordinate = 160
                txtHierarachyArticle.IsSetLocation = True
                txtHierarachyArticle.ListBoxXCoordinate = 480
                txtHierarachyArticle.ListBoxYCoordinate = 138
                TxtArticleFliter.ArticleCodeDesclength = ArticleCodeDescLen
                TxtArticleFliter.isCalledFromlablePrint = True
            Else
                TxtArticleFliter.IsSetLocation = True
                TxtArticleFliter.ListBoxXCoordinate = 195
                TxtArticleFliter.ListBoxYCoordinate = 163
                txtHierarachyArticle.IsSetLocation = True
                txtHierarachyArticle.ListBoxXCoordinate = 422
                txtHierarachyArticle.ListBoxYCoordinate = 141
            End If

            DtPKDDate.Value = DateTime.Now
            DtPKDDate.Visible = False
            TxtQty.Value = 1
            AddHandler TxtArticleFliter.KeyDown, AddressOf EnterEventOftxtFilterArticle
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Function"



    'Private Sub BlankItemDetailSection()
    '    Try

    '        TxtHierarchy.Text = ""

    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try
    'End Sub



#End Region






    Private Sub TxtHierarchy_Click(sender As Object, e As EventArgs) Handles TxtHierarchy.Click
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup.CheckedHierarachy = True
            If ObjHierPopup.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ' TxtHierarchy.Text = ObjHierPopup.SelectedNodeCode

                Articledata = ObjC.GetArticleOfAnHierarachy(ObjHierPopup.SelectedNodeCode.ToString, clsAdmin.SiteCode)
                ' TxtHierarchy.Text=
                If Not Articledata Is Nothing And Articledata.Rows.Count = 0 Then
                    ShowMessage("No Article present in Selected Hierarachy", getValueByKey("CLAE04")) ' lblHierarchyName.Text 
                    ' lblHierarchyName.Text = ""
                    Exit Sub
                End If
                lblHierarchyName.Text = ObjHierPopup.SelectedNodeName
                AssignDataToHierarachyArticleFliter(ObjHierPopup.SelectedNodeCode.ToString)
                DtGridData.Rows.Clear()
                For Each dr In Articledata.Rows
                    Dim DrArtCombo1 = DtGridData.NewRow()
                    DrArtCombo1("SElECT") = 0
                    DrArtCombo1("EAN") = dr("EAN")
                    DrArtCombo1("ArticleCode") = dr("ArticleCode")
                    DrArtCombo1("ArticleShortName") = dr("ArticleShortName")
                    DrArtCombo1("SellingPrice") = dr("SellingPrice")
                    DrArtCombo1("NoOfCopies") = 1

                    DrArtCombo1("ExpiryInDays") = dr("ExpiryInDays")
                    DrArtCombo1("Neutrition") = dr("Neutrition")
                    DrArtCombo1("Ingredient") = dr("Ingredient")
                    DrArtCombo1("BatchNo") = dr("BatchNo")
                    DrArtCombo1("Characteristics") = dr("Characteristics")




                    DtGridData.Rows.Add(DrArtCombo1)
                Next

                GrdShowData.DataSource = DtGridData
                GridSetting()
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub TxtUpgradableHierarchy_Click(sender As Object, e As EventArgs)
        Try
            Dim ObjHierPopup As New frmHierarchyPopUp
            ObjHierPopup.ShowDialog()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub EnterEventOftxtFilterArticle(sender As Object, e As KeyEventArgs)
        Try
            If (e.KeyCode = Keys.Enter) Then
                Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
                Call txtFilterArticle_Leave(sender, eKeyDown)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub TxtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtQty.KeyDown
        ' If (e.KeyCode = Keys.Enter) Then
        'For Each dr In DtGridData.Rows
        '    If dr("SElECT") = True Then
        '        dr("NoOfCopies") = TxtQty.Value
        '    Else
        '        dr("NoOfCopies") = dr("NoOfCopies")
        '    End If

        '    dr("SElECT") = dr("SElECT")
        '    dr("ArticleCode") = dr("ArticleCode")
        '    dr("ArticleShortName") = dr("ArticleShortName")
        '    dr("SellingPrice") = dr("SellingPrice")
        'Next

        'GrdShowData.DataSource = DtGridData
        'GridSetting()
        ' End If
    End Sub
    Private Sub GridSetting()

        If Not GrdShowData.Columns(0) Is Nothing Then
            GrdShowData.Columns("EAN").Caption = "EAN"
            GrdShowData.Columns("ArticleCode").Caption = "Article Code"
            GrdShowData.Columns("ArticleShortName").Caption = "Article Name"
            GrdShowData.Columns("SellingPrice").Caption = "Selling Price"
            GrdShowData.Columns("NoOfCopies").Caption = "No. of Print"
            GrdShowData.Columns("ExpiryInDays").Caption = "Expiry In Days"
            GrdShowData.Columns("Neutrition").Caption = "Nutrition"
            Me.GrdShowData.Splits(0).DisplayColumns("ArticleShortName").Locked = True
            Me.GrdShowData.Splits(0).DisplayColumns("ArticleCode").Locked = True
            Me.GrdShowData.Splits(0).DisplayColumns("SellingPrice").Locked = True


            Me.GrdShowData.Splits(0).DisplayColumns("ExpiryInDays").Locked = True
            Me.GrdShowData.Splits(0).DisplayColumns("Neutrition").Locked = True
            Me.GrdShowData.Splits(0).DisplayColumns("Ingredient").Locked = True
            Me.GrdShowData.Splits(0).DisplayColumns("BatchNo").Locked = True
            GrdShowData.Columns("Characteristics").Caption = "Characteristics"
            If screenWidth > 1024 Then
                'Me.GrdShowData.Splits(0).DisplayColumns(0).Width = 60
                'Me.GrdShowData.Splits(0).DisplayColumns(1).Width = 120
                'Me.GrdShowData.Splits(0).DisplayColumns(2).Width = 220
                'Me.GrdShowData.Splits(0).DisplayColumns(3).Width = 100
                'Me.GrdShowData.Splits(0).DisplayColumns(4).Width = 100


                'Me.GrdShowData.Splits(0).DisplayColumns(5).Width = 100
                'Me.GrdShowData.Splits(0).DisplayColumns(6).Width = 200
                'Me.GrdShowData.Splits(0).DisplayColumns(7).Width = 200
                'Me.GrdShowData.Splits(0).DisplayColumns(8).Width = 50
                Me.GrdShowData.Splits(0).DisplayColumns(1).Width = 110
                Me.GrdShowData.Splits(0).DisplayColumns(2).Width = 120
                Me.GrdShowData.Splits(0).DisplayColumns(3).Width = 160
                Me.GrdShowData.Splits(0).DisplayColumns(4).Width = 90


                Me.GrdShowData.Splits(0).DisplayColumns(5).Width = 80
                Me.GrdShowData.Splits(0).DisplayColumns(6).Width = 100
                Me.GrdShowData.Splits(0).DisplayColumns(7).Width = 100
                Me.GrdShowData.Splits(0).DisplayColumns(8).Width = 100

                Me.GrdShowData.Splits(0).DisplayColumns(9).Width = 60
                Me.GrdShowData.Splits(0).DisplayColumns(10).Width = 70

            End If




            '  GrdShowData.Columns("NoOfCopies").DataType = Type.GetType("System.Int32")
            ' GrdShowData.Columns("NoOfCopies").NumberFormat = "0"


        End If
    End Sub


    Private Sub frmGenerateBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub GrdShowData_FilterChange(sender As Object, e As EventArgs) Handles GrdShowData.FilterChange

        'GrdShowData.AllowFilter = True
        'Dim dt As New DataTable()
        'Dim sb As New System.Text.StringBuilder()
        'Dim dc As C1.Win.C1TrueDBGrid.C1DataColumn
        'For Each dc In Me.GrdShowData.Columns

        '    If dc.DataField = "Select" Then
        '        If Not String.IsNullOrEmpty(dc.FilterText) Then
        '            dt = TryCast(GrdShowData.DataSource, DataTable)
        '            AssignDataToGrid(dt, ColumnChecked)
        '            Exit For
        '        End If

        '    End If
        'Next dc
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim Copy4Label
            Dim objLblPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
            Dim checkStr As String = ""
            DtGridData = GrdShowData.DataSource
            If DtGridData.Rows.Count < 1 Then
                Exit Sub
            End If


            Dim IsItemSelected As Boolean = False
            For Each dr In DtGridData.Rows
                If dr("SELECT") = True Then
                    IsItemSelected = True
                    Exit For
                End If
            Next


            If IsItemSelected = False Then
                ShowMessage(" Please select atleast one article", "Information")
                Exit Sub
            End If
            Dim pathLable As String
            Dim BarCodePath As String
            Dim LabelPath As String
            If chkBarcode.Checked = False AndAlso chkLabel.Checked = False Then
                ShowMessage("Please select printing option ", "Information")
                Exit Sub
            End If
            If chkBarcode.Checked = True Then
                BarCodePath = clsDefaultConfiguration.DayCloseReportPath + "\BarCode\" + "" & DateTime.Today.ToString("ddmmmyyyy") & ""
                If Not Directory.Exists(BarCodePath) Then
                    Directory.CreateDirectory(BarCodePath)
                End If
            End If
            If chkLabel.Checked = True Then
                LabelPath = clsDefaultConfiguration.DayCloseReportPath + "\Label\" + "" & DateTime.Now.ToString("dd-MM-yyyy") & ""
                If Not Directory.Exists(LabelPath) Then
                    Directory.CreateDirectory(LabelPath)
                End If
            End If
            ' Dim objLblPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)

            Dim PKDDate As Date = clsAdmin.DayOpenDate
            If CboChangePKD.Checked = True Then
                PKDDate = DtPKDDate.Value
            Else
                PKDDate = clsAdmin.DayOpenDate
            End If

            If Not DtGridData Is Nothing And DtGridData.Rows.Count > 0 Then
                'For Each dr In DtGridData.Rows
                '    If dr("NoOfCopies") <> 0 And dr("SELECT") = True Then
                '        dr("NoOfCopies") = Math.Ceiling(dr("NoOfCopies") / 2)
                '        objLblPrint.SpectrumArticleLabel(dr("ArticleCode"), clsAdmin.SiteCode, clsDefaultConfiguration.DayCloseReportPath, CboChangePKD.Checked, PKDDate, dr("NoOfCopies"))
                '    End If
                'Next
                For Each dr In DtGridData.Rows
                    If dr("NoOfCopies") <> 0 And dr("SELECT") = True Then
                        checkStr += (dr("ArticleCode") + "|" + (dr("NoOfCopies").ToString) + ",")
                    End If
                Next
                If clsDefaultConfiguration.labelPrintFormatNo = 0 Then
                    If checkStr <> "" Then
                        If chkBarcode.Checked = True Then
                            If objLblPrint.BarcodePrintingFormat4(checkStr, clsAdmin.SiteCode, BarCodePath, CboChangePKD.Checked, PKDDate) = True Then
                                ShowMessage("Barcodes are generated at Location " & BarCodePath & " ", "Information")
                            End If
                        End If
                        If chkLabel.Checked = True Then
                            If objLblPrint.LabelPrintingFormat4(checkStr, clsAdmin.SiteCode, LabelPath, CboChangePKD.Checked, PKDDate) = True Then
                                ShowMessage("Labels are generated at Location " & LabelPath & " ", "Information")
                            End If
                        End If
                    End If
                End If
            End If
            lblHierarchyName.Text = ""
            DtGridData.Rows.Clear()
            GrdShowData.DataSource = DtGridData
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click_1(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub GrdShowData_AfterColEdit(sender As Object, e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles GrdShowData.AfterColEdit


        'Dim ArticleCode As String = ""
        'Dim Status As Boolean = False
        'Dim NoOfCopies As Integer = 1
        'Dim aa = GrdShowData.SelectedRo

        'ArticleCode = GrdShowData.Rows(GrdShowData.Row)("ArticleCode")
        'Status = GrdShowData.Rows(GrdShowData.Row)("SELECT")
        'NoOfCopies = GrdShowData.Rows(GrdShowData.Row)("NoOfCopies")
        'If e.Column.ToString.ToUpper = "SELECT" Then
        '    For Each dr In DtGridData.Rows
        '        If dr("ArticleCode") = ArticleCode Then
        '            dr("SElECT") = Status
        '        End If
        '    Next
        'End If
        'If e.Column.ToString.ToUpper = "NOOFCOPIES" Then
        '    For Each dr In DtGridData.Rows
        '        If dr("ArticleCode") = ArticleCode Then
        '            dr("NoOfCopies") = NoOfCopies
        '        End If
        '    Next
        'End If




    End Sub
    Private Sub AssignDataToGrid(ByVal dt As DataTable, ByVal Check As Boolean)
        Try
            For Each dr As DataRow In dt.Rows
                dr("SElECT") = Check
                dr("EAN") = dr("EAN")
                dr("ArticleCode") = dr("ArticleCode")
                dr("ArticleShortName") = dr("ArticleShortName")
                dr("SellingPrice") = dr("SellingPrice")
                dr("NoOfCopies") = dr("NoOfCopies")
                dr("ExpiryInDays") = dr("ExpiryInDays")
                dr("Neutrition") = dr("Neutrition")
                dr("Ingredient") = dr("Ingredient")
                dr("BatchNo") = dr("BatchNo")
                dr("Characteristics") = dr("Characteristics")
                dr.AcceptChanges()
            Next
            DtGridData = dt.Copy
            GrdShowData.DataSource = DtGridData
            GridSetting()

            'If Check = True Then
            '    ColumnChecked = False
            'Else
            '    ColumnChecked = True
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub TxtArticleFliter_TextChanged(sender As Object, e As EventArgs) Handles TxtArticleFliter.TextChanged
        If Not String.IsNullOrEmpty(TxtArticleFliter.Text) AndAlso TxtArticleFliter.IsItemSelected Then
            TxtArticleFliter.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtFilterArticle_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub txtFilterArticle_Leave(sender As System.Object, e As System.EventArgs) 'Handles txtFilterArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            TxtArticleFliter.Text = TxtArticleFliter.Text.ToString().Split(" ")(0)
            If TxtArticleFliter.Text.Length >= 1 Then
                Dim membershipmaparticle = TxtArticleFliter.Text
                Dim objItemSch As New clsIteamSearch
                ' If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
                Call bindSelectedArticle(TxtArticleFliter.Text)
                'End If
                'End If
                TxtArticleFliter.Text = ""
                TxtArticleFliter.Focus()
            End If
        Catch ex As Exception
            'ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub




    Private Sub bindSelectedArticle(ByVal ArticleCode As String) ''checkout As DateTime,
        Try


            Dim dt As New DataTable
            dt = TryCast(GrdShowData.DataSource, DataTable)

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    DtGridData = dt.Copy
                    GrdShowData.DataSource = DtGridData
                    GridSetting()
                End If
            Else
                DtGridData.Rows.Clear()
            End If

            Dim dtCustmData = ObjC.GetLblSingleArticleDetails(ArticleCode, clsAdmin.SiteCode)
            ' If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
            If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                If dtCustmData.Rows.Count > 0 Then
                    For index = 0 To DtGridData.Rows.Count - 1
                        Dim dtRow As Int32 = -1
                        Dim result As DataRow() = DtGridData.Select("EAN='" + ArticleCode.Trim + "' ")
                        'Dim result As DataRow() = DtGridData.Select("ArticleCode='" + ArticleCode.Trim + "' ")
                        If result.Length > 0 Then
                            ShowMessage("Record Already exist", "Information")
                            Exit Sub
                        End If
                    Next

                End If
                Dim dr = DtGridData.NewRow()
                For Each dr In dtCustmData.Rows
                    Dim DrArtCombo1 = DtGridData.NewRow()
                    DrArtCombo1("SElECT") = 1
                    DrArtCombo1("EAN") = dr("EAN")
                    DrArtCombo1("ArticleCode") = dr("ArticleCode")
                    DrArtCombo1("ArticleShortName") = dr("ArticleShortName")
                    DrArtCombo1("SellingPrice") = dr("SellingPrice")
                    If Not String.IsNullOrEmpty(TxtQty.Value.ToString) Then
                        DrArtCombo1("NoOfCopies") = TxtQty.Value
                    Else
                        DrArtCombo1("NoOfCopies") = 1
                    End If

                    DrArtCombo1("ExpiryInDays") = dr("ExpiryInDays")
                    DrArtCombo1("Neutrition") = dr("Neutrition")
                    DrArtCombo1("Ingredient") = dr("Ingredient")
                    DrArtCombo1("BatchNo") = dr("BatchNo")
                    DrArtCombo1("Characteristics") = dr("Characteristics")




                    DtGridData.Rows.Add(DrArtCombo1)
                Next
                GrdShowData.DataSource = DtGridData
                GridSetting()
            Else
                ShowMessage("No Such Article Found or Article is Not Saleable", "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RadioArticle_Click(sender As Object, e As EventArgs) Handles RadioArticle.Click
        DtGridData.Rows.Clear()
        GrdShowData.DataSource = DtGridData
        GridSetting()
        TxtHierarchy.Visible = False
        lblHierarchyName.Visible = False
        lblHierarchy.Visible = False
        LblHierarachyCode.Visible = False
        TxtArticleFliter.Visible = True
        txtHierarachyArticle.Visible = False
        LblArt.Visible = True
    End Sub

    Private Sub RadioHierarchy_Click(sender As Object, e As EventArgs) Handles RadioHierarchy.Click
        DtGridData.Rows.Clear()
        GrdShowData.DataSource = DtGridData
        GridSetting()

        TxtHierarchy.Visible = True
        lblHierarchyName.Visible = True
        lblHierarchy.Visible = True
        LblHierarachyCode.Visible = True
        TxtArticleFliter.Visible = False
        txtHierarachyArticle.Visible = True
        LblArt.Visible = False
        lblHierarchyName.Text = ""
    End Sub


    Private Sub txtHierarachyArticle_TextChanged(sender As Object, e As EventArgs) Handles txtHierarachyArticle.TextChanged
        If Not String.IsNullOrEmpty(txtHierarachyArticle.Text) AndAlso txtHierarachyArticle.IsItemSelected Then
            txtHierarachyArticle.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtHierarachyArticle_Leave(sender, eKeyDown)
        End If
    End Sub


    Private Sub txtHierarachyArticle_Leave(sender As System.Object, e As System.EventArgs) 'Handles txtFilterArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            txtHierarachyArticle.Text = txtHierarachyArticle.Text.ToString().Split(" ")(0)
            If txtHierarachyArticle.Text.Length >= 1 Then
                Dim membershipmaparticle = txtHierarachyArticle.Text
                Dim objItemSch As New clsIteamSearch
                ' If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
                Call bindSelectedHierarchyArticle(txtHierarachyArticle.Text)
                txtHierarachyArticle.Text = ""
                txtHierarachyArticle.Focus()
            End If
        Catch ex As Exception
            ' ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub





    Private Sub bindSelectedHierarchyArticle(ByVal ArticleCode As String) ''checkout As DateTime,
        Try


            Dim dt As New DataTable
            dt = TryCast(GrdShowData.DataSource, DataTable)

            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    DtGridData = dt.Copy
                    GrdShowData.DataSource = DtGridData
                    GridSetting()
                End If
            End If
            Dim result As DataRow() = DtGridData.Select("EAN='" + ArticleCode.Trim + "' ")
            'Dim result As DataRow() = DtGridData.Select("ArticleCode='" + ArticleCode.Trim + "' ")
            result(0)("Select") = True
            DtGridData.AcceptChanges()
            GrdShowData.DataSource = DtGridData
            GridSetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub




    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Try
            If Not DtGridData Is Nothing Then
                If DtGridData.Rows.Count > 0 Then
                    For Each dr In DtGridData.Rows
                        If dr("SElECT") = True Then
                            dr("NoOfCopies") = TxtQty.Value
                        Else
                            dr("NoOfCopies") = dr("NoOfCopies")
                        End If

                        dr("SElECT") = dr("SElECT")
                        dr("ArticleCode") = dr("ArticleCode")
                        dr("ArticleShortName") = dr("ArticleShortName")
                        dr("SellingPrice") = dr("SellingPrice")

                        dr("ExpiryInDays") = dr("ExpiryInDays")
                        dr("Neutrition") = dr("Neutrition")
                        dr("Ingredient") = dr("Ingredient")
                        dr("BatchNo") = dr("BatchNo")
                    Next
                    GrdShowData.DataSource = DtGridData
                    GridSetting()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtHierarachyArticle_Leave_1(sender As Object, e As EventArgs) Handles txtHierarachyArticle.Leave

        txtHierarachyArticle.Text = ""
        If (txtHierarachyArticle.IsListBoxVisible And txtHierarachyArticle.IsMouseOverList = False) Then
            txtHierarachyArticle.ResetListBox()
        End If

    End Sub

    Private Sub TxtArticleFliter_Leave(sender As Object, e As EventArgs) Handles TxtArticleFliter.Leave
        TxtArticleFliter.Text = ""
        If (TxtArticleFliter.IsListBoxVisible And TxtArticleFliter.IsMouseOverList = False) Then
            TxtArticleFliter.ResetListBox()
        End If
    End Sub

    Private Sub AssignDataToHierarachyArticleFliter(ByVal filter As String)

        Try
            If dtHierarchyArticle.Rows.Count > 1 Then
                Dim sftr As String = "Nodes in (" & filter & ")"
                Dim dv As New DataView(dtHierarchyArticle, sftr, "", DataViewRowState.CurrentRows)

                Dim DtHierarchy As New DataTable
                DtHierarchy = dv.ToTable
                Call SetWildSearchTextBox(DtHierarchy, txtHierarachyArticle, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub


    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)

        Me.BackColor = Color.FromArgb(76, 76, 76)

        LblHierarachyCode.ForeColor = Color.White
        lblHierarchy.ForeColor = Color.White

        LblQuantity.ForeColor = Color.White




        lblHierarchyName.ForeColor = Color.White
        LblArt.ForeColor = Color.White
        '  GrpAddStep.ForeColor = Color.White
        GroupBox3.ForeColor = Color.White


        Me.BackColor = Color.FromArgb(76, 76, 76)

        Me.BackColor = Color.FromArgb(76, 76, 76)
        'BtnAddStep.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'BtnAddStep.BackColor = Color.Transparent
        'BtnAddStep.BackColor = Color.FromArgb(0, 107, 163)
        'BtnAddStep.ForeColor = Color.FromArgb(255, 255, 255)
        ''  BtnAddStepck.Size = New Size(71, 30)
        'BtnAddStep.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'BtnAddStep.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'BtnAddStep.FlatStyle = FlatStyle.Flat
        'BtnAddStep.FlatAppearance.BorderSize = 0
        'BtnAddStep.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'BtnSaveStep.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'BtnSaveStep.BackColor = Color.Transparent
        'BtnSaveStep.BackColor = Color.FromArgb(0, 107, 163)
        'BtnSaveStep.ForeColor = Color.FromArgb(255, 255, 255)
        'BtnSaveStep.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'BtnSaveStep.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'BtnSaveStep.FlatStyle = FlatStyle.Flat
        'BtnSaveStep.FlatAppearance.BorderSize = 0
        'BtnSaveStep.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        'BtnActivateDeactivate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'BtnActivateDeactivate.BackColor = Color.Transparent
        'BtnActivateDeactivate.BackColor = Color.FromArgb(0, 107, 163)
        'BtnActivateDeactivate.ForeColor = Color.FromArgb(255, 255, 255)
        'BtnActivateDeactivate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'BtnActivateDeactivate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'BtnActivateDeactivate.FlatStyle = FlatStyle.Flat
        'BtnActivateDeactivate.FlatAppearance.BorderSize = 0
        'BtnActivateDeactivate.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'BtnSaveCombo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'BtnSaveCombo.BackColor = Color.Transparent
        'BtnSaveCombo.BackColor = Color.FromArgb(0, 107, 163)
        'BtnSaveCombo.ForeColor = Color.FromArgb(255, 255, 255)
        'BtnSaveCombo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'BtnSaveCombo.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'BtnSaveCombo.FlatStyle = FlatStyle.Flat
        'BtnSaveCombo.FlatAppearance.BorderSize = 0
        'BtnSaveCombo.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'BtnCancel.BackColor = Color.Transparent
        'BtnCancel.BackColor = Color.FromArgb(0, 107, 163)
        'BtnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        'BtnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'BtnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'BtnCancel.FlatStyle = FlatStyle.Flat
        'BtnCancel.FlatAppearance.BorderSize = 0
        'btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCancel.BackColor = Color.Transparent
        btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0

        btnApply.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnApply.BackColor = Color.Transparent
        btnApply.BackColor = Color.FromArgb(0, 107, 163)
        btnApply.ForeColor = Color.FromArgb(255, 255, 255)
        btnApply.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnApply.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnApply.FlatStyle = FlatStyle.Flat
        btnApply.FlatAppearance.BorderSize = 0
        btnApply.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)






        btnPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnPrint.BackColor = Color.Transparent
        btnPrint.BackColor = Color.FromArgb(0, 107, 163)
        btnPrint.ForeColor = Color.FromArgb(255, 255, 255)
        btnPrint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnPrint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.FlatAppearance.BorderSize = 0
        btnPrint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)



        btnExcludeOrInclude.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnExcludeOrInclude.BackColor = Color.Transparent
        btnExcludeOrInclude.BackColor = Color.FromArgb(0, 107, 163)
        btnExcludeOrInclude.ForeColor = Color.FromArgb(255, 255, 255)
        btnExcludeOrInclude.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnExcludeOrInclude.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnExcludeOrInclude.FlatStyle = FlatStyle.Flat
        btnExcludeOrInclude.FlatAppearance.BorderSize = 0
        btnExcludeOrInclude.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        'txtItemCode.ForeColor = Color.White
        'TxtItemSearchName.ForeColor = Color.White
        'TxtmaterialType.ForeColor = Color.White
        'TxtSalePRice.ForeColor = Color.White
        'txtItemType.ForeColor = Color.White
        'TxtStatus.ForeColor = Color.White
        'Label1.ForeColor = Color.White
        'lblTotCost.ForeColor = Color.White
        'LblTotQty.ForeColor = Color.White

        'Label2.ForeColor = Color.White
        'Label3.ForeColor = Color.White
        'Label4.ForeColor = Color.White
        'Label5.ForeColor = Color.White
        'Label6.ForeColor = Color.White
        'Label7.ForeColor = Color.White

        'Label8.ForeColor = Color.White
        'Label9.ForeColor = Color.White
        'Label10.ForeColor = Color.White
        'Label11.ForeColor = Color.White
        'Label12.ForeColor = Color.White
        'Label13.ForeColor = Color.White

        'Label14.ForeColor = Color.White
        'Label15.ForeColor = Color.White
        'Label16.ForeColor = Color.White
        'Label17.ForeColor = Color.White
        'Label18.ForeColor = Color.White
        'Label19.ForeColor = Color.White
        'Label20.ForeColor = Color.White

        GroupBox1.ForeColor = Color.White
        'GrpAddStep.ForeColor = Color.White
        GroupBox3.ForeColor = Color.White
        '  GroupBox4.ForeColor = Color.White
        'PanelUpgradableItem.ForeColor = Color.White

    End Function

    Private Sub btnExcludeOrInclude_Click(sender As Object, e As EventArgs) Handles btnExcludeOrInclude.Click
        Try
            DtGridData = GrdShowData.DataSource
            If DtGridData.Rows.Count < 1 Then
                Exit Sub
            End If

            GrdShowData.AllowFilter = True
            Dim dt As New DataTable()
            Dim Check As Boolean = False
            If btnExcludeOrInclude.Text.ToUpper.Equals("EXCLUDE ALL") Then
                Check = False
                btnExcludeOrInclude.Text = "Include All"
            Else
                Check = True
                btnExcludeOrInclude.Text = "Exclude All"
            End If

            dt = TryCast(GrdShowData.DataSource, DataTable)
            AssignDataToGrid(dt, Check)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CboChangePKD_CheckedChanged(sender As Object, e As EventArgs) Handles CboChangePKD.CheckedChanged
        Try
            If CboChangePKD.Checked = True Then
                DtPKDDate.Visible = True
            Else
                DtPKDDate.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class