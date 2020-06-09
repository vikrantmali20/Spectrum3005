Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class frmPCAddSTRDetails

    Dim _dsPackagingDeliverySTR As New DataSet
    Public Property dsPackagingDeliverySTR() As DataSet
        Get
            Return _dsPackagingDeliverySTR
        End Get
        Set(ByVal value As DataSet)
            _dsPackagingDeliverySTR = value
        End Set
    End Property

    Dim _dtCombo As New DataTable
    Public Property dtCombo() As DataTable
        Get
            Return _dtCombo
        End Get
        Set(ByVal value As DataTable)
            _dtCombo = value
        End Set
    End Property
    Dim _strIndex As Integer
    Public Property StrIndex() As Integer
        Get
            Return _strIndex
        End Get
        Set(ByVal value As Integer)
            _strIndex = value
        End Set
    End Property
    Dim _filter As String
    Public Property filter() As String
        Get
            Return _filter
        End Get
        Set(ByVal value As String)
            _filter = value
        End Set
    End Property
    Dim _isEdit As Boolean = False
    Public Property IsEdit() As Boolean
        Get
            Return _isEdit
        End Get
        Set(ByVal value As Boolean)
            _isEdit = value
        End Set
    End Property
    Dim _SONumber As String
    Public Property SONumber() As String
        Get
            Return _SONumber
        End Get
        Set(ByVal value As String)
            _SONumber = value
        End Set
    End Property
    Dim _comboNonSTrArticles As String = ""
    Public Property comboNonSTrArticles() As String
        Get
            Return _comboNonSTrArticles
        End Get
        Set(ByVal value As String)
            _comboNonSTrArticles = value
        End Set
    End Property
    Public _dtSTR As New DataTable
    Public Property dtSTR() As DataTable
        Get
            Return _dtSTR
        End Get
        Set(ByVal value As DataTable)
            _dtSTR = value
        End Set
    End Property

    Public _dtFinalSTR As New DataTable
    Public Property dtFinalSTR() As DataTable
        Get
            Return _dtFinalSTR
        End Get
        Set(ByVal value As DataTable)
            _dtFinalSTR = value
        End Set
    End Property

    Private _VsoDate As DateTime
    Public Property VsoDate() As DateTime
        Get
            Return _VsoDate
        End Get
        Set(ByVal value As DateTime)
            _VsoDate = value
        End Set
    End Property
    Private _DtFactoryRemarks As DataTable
    Public Property DtFactoryRemarks() As DataTable
        Get
            Return _DtFactoryRemarks
        End Get
        Set(ByVal value As DataTable)
            _DtFactoryRemarks = value
        End Set
    End Property
    Dim objPCSO As New clsSalesOrderPC
    Dim indexGrd As Integer = 1
    Dim drLowerVariation() As DataRow
    Dim drLowerVariationCombo() As DataRow
    Dim comboQuantity As String
    Dim comboUOM As String
    Dim IsDisplaySrNo As Boolean = False
    Private Sub frmPCAddSTRDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CompareAtLoad(dtSTR, dtFinalSTR)
            If Not dtSTR Is Nothing And dtSTR.Rows.Count > 0 Then
                For Each drUpdateDateTime As DataRow In dtSTR.Select("STRQty=0")
                    drUpdateDateTime("STRDate") = Nothing
                    drUpdateDateTime("STRTime") = Nothing
                Next
            End If

            'Dim dtDisplay = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("", "SrNo").CopyToDataTable()
            'lblArticleNameVal.Text = dtDisplay.Rows(filter - 1)("Discription")
            'lblArticleTypeVal.Text = dtDisplay.Rows(filter - 1)("ArticleType")

            dgLowerGrid.Rows.RemoveRange(1, dgLowerGrid.Rows.Count - 1)
            dgUpperGrid.Rows.RemoveRange(1, dgUpperGrid.Rows.Count - 1)

            dgLowerGrid.Rows.MinSize = 28
            dgUpperGrid.Rows.MinSize = 28
            '-----Upper Grid
            UpperGridSetting()
            Dim dv As New DataView(_dsPackagingDeliverySTR.Tables("PackagingDelivery"))
            If IsEdit Then
                Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("deliverylineno='" + filter + "'")
                dv.RowFilter = "billlineno in (" & result(0)("BillLineNo") & ")"
            Else
                Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryIndex='" + filter + "'")
                dv.RowFilter = "rowindex in (" & result(0)("RowIndex") & ")"
            End If

            Dim dtS = dv.ToTable()

            lblArticleNameVal.Text = dtS.Rows(0)("Discription")
            If IsEdit Then
                If dtS.Rows(0)("IsCombo") Then
                    lblArticleTypeVal.Text = "Combo"
                Else
                    lblArticleTypeVal.Text = "Single"
                End If
            Else

                lblArticleTypeVal.Text = dtS.Rows(0)("ArticleType")
            End If

            '----------Added By Prasad for Showing Data by Delivery Index Wise
            If IsEdit Then
                dtS = dtS.Select("", "deliverylineno").CopyToDataTable()
            Else
                dtS = dtS.Select("", "DeliveryIndex").CopyToDataTable()
            End If

            BindUpperGrid(dtS)

            '-----In case Of combo
            comboQuantity = dtS.Compute("Sum(Quantity)", "")

            '            comboQuantity = dtS.Rows(0)("Quantity").ToString()
            If IsEdit Then
                comboUOM = dtS.Rows(0)("UnitOfMeasure").ToString()
            Else
                comboUOM = dtS.Rows(0)("UOM").ToString()
            End If




            '-----Lower Grid
            LowerGridSetting()
            If Not IsEdit Then
                If dtSTR.Rows.Count > 0 Then

                    Dim dvv As New DataView(dtSTR)
                    dvv.RowFilter = "deliveryindex in (" & filter & ")"

                    If dvv.Count = 0 Then

                        If lblArticleTypeVal.Text.Trim() = "Single" Then
                            If IsEdit Then
                                drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("DeliveryLineNo='" & filter & "'")
                            Else
                                drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("Deliveryindex='" & filter & "'")
                            End If
                            IsDisplaySrNo = True
                            SetInDataTable(drLowerVariation)
                        Else
                            Dim tempDtCombo As New DataTable
                            tempDtCombo = dtCombo.Copy()
                            Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryIndex='" + filter + "'")
                            If comboNonSTrArticles <> "" Then
                                Dim articles As Array = comboNonSTrArticles.Split(",")
                                If articles.Length > 0 Then
                                    For art As Integer = 0 To articles.Length - 1
                                        tempDtCombo = tempDtCombo.Select("articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                    Next
                                End If
                            End If
                            drLowerVariationCombo = dtCombo.Select("ComboSrNo='" & result(0)("RowIndex") & "'")
                            IsDisplaySrNo = True
                            SetInDataTable(drLowerVariationCombo)
                        End If

                        Dim dvvv As New DataView(dtSTR)
                        dvvv.RowFilter = "Deliveryindex in (" & filter & ")"
                        Dim dtSs = dvvv.ToTable()
                        BindLowerGrid(dtSs)

                    Else

                        ' dvv.RowFilter = "Deliveryindex in (" & filter & ")"
                        Dim tempDt As New DataTable
                        tempDt = dvv.ToTable()
                        If lblArticleTypeVal.Text = "Combo" Then
                            Dim tempDtCombo As New DataTable
                            tempDtCombo = dtCombo.Copy()
                            If comboNonSTrArticles <> "" Then
                                Dim articles As Array = comboNonSTrArticles.Split(",")
                                If articles.Length > 0 Then
                                    For art As Integer = 0 To articles.Length - 1
                                        tempDtCombo = tempDtCombo.Select("articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                    Next
                                End If
                            End If
                            indexGrd = tempDt.Select("IsimageREq=1").Count
                            Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryIndex='" + filter + "'")
                            For Each dr As DataRow In tempDtCombo.Rows
                                If result(0)("rowindex") = dr("ComboSrNo") Then
                                    drLowerVariation = tempDt.Select("EAN='" & dr("EAN") & "' and IsCombo=True")
                                    If drLowerVariation.Length = 0 Then
                                        Dim drLowerVariationNew() As DataRow
                                        drLowerVariationNew = dtCombo.Select("ComboSrNo='" & result(0)("rowindex") & "' and EAN='" & dr("EAN") & "'")
                                        IsDisplaySrNo = False
                                        indexGrd = indexGrd + 1
                                        SetInDataTable(drLowerVariationNew)
                                    End If
                                End If


                            Next
                            Dim dvvs As New DataView(dtSTR)
                            dvvs.RowFilter = "deliveryindex in (" & filter & ")"
                            Dim dtSs = dvvs.ToTable()
                            dtSs = dtSs.Select("", "SrNo").CopyToDataTable()
                            BindLowerGrid(dtSs)
                        Else
                            Dim dtSs = dvv.ToTable()
                            dtSs = dtSs.Select("", "SrNo").CopyToDataTable()
                            BindLowerGrid(dtSs)
                        End If

                    End If

                Else

                    dtSTR = objPCSO.GetCollectionOfSTR()
                    dtSTR.Rows.Clear()
                    dtFinalSTR = dtSTR.Copy()

                    If lblArticleTypeVal.Text.Trim() = "Single" Then
                        If IsEdit Then
                            drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("DeliveryLineNo='" & filter & "'")
                        Else
                            drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("Deliveryindex='" & filter & "'")
                        End If
                        IsDisplaySrNo = True
                        SetInDataTable(drLowerVariation)
                    Else
                        Dim tempDtCombo As New DataTable
                        tempDtCombo = dtCombo.Copy()
                        Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryIndex='" + filter + "'")
                        If comboNonSTrArticles <> "" Then
                            Dim articles As Array = comboNonSTrArticles.Split(",")
                            If articles.Length > 0 Then
                                For art As Integer = 0 To articles.Length - 1
                                    tempDtCombo = tempDtCombo.Select("articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                Next
                            End If
                            drLowerVariationCombo = tempDtCombo.Select("ComboSrNo='" & result(0)("RowIndex") & "' ")
                        Else
                            drLowerVariationCombo = dtCombo.Select("ComboSrNo='" & result(0)("RowIndex") & "' ")
                        End If
                        IsDisplaySrNo = True
                        SetInDataTable(drLowerVariationCombo)
                    End If

                    BindLowerGrid(dtSTR)
                End If

            Else
                If dtSTR.Rows.Count > 0 Then

                    Dim dvv As New DataView(dtSTR)


                    dvv.RowFilter = "deliveryindex in (" & filter & ")"



                    If dvv.Count = 0 Then

                        If lblArticleTypeVal.Text.Trim() = "Single" Then
                            If IsEdit Then
                                drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("DeliveryLineNo='" & filter & "'")
                            Else
                                drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("Deliveryindex='" & filter & "'")
                            End If
                            IsDisplaySrNo = True
                            SetInDataTable(drLowerVariation)
                        Else
                            Dim tempDtCombo As New DataTable
                            tempDtCombo = dtCombo.Copy()

                            Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryLineNo='" + filter + "'")
                            If comboNonSTrArticles <> "" Then
                                Dim articles As Array = comboNonSTrArticles.Split(",")
                                If articles.Length > 0 Then
                                    For art As Integer = 0 To articles.Length - 1
                                        tempDtCombo = tempDtCombo.Select("articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                    Next
                                End If
                                drLowerVariationCombo = tempDtCombo.Select("ComboSrNo='" & result(0)("BillLineNo") & "'")
                            Else
                                drLowerVariationCombo = dtCombo.Select("ComboSrNo='" & result(0)("BillLineNo") & "'")
                            End If
                            IsDisplaySrNo = True
                            SetInDataTable(drLowerVariationCombo)
                        End If

                        Dim dvvv As New DataView(dtSTR)

                        dvvv.RowFilter = "Deliveryindex in (" & filter & ")"


                        Dim dtSs = dvvv.ToTable()
                        BindLowerGrid(dtSs)

                    Else

                        ' dvv.RowFilter = "Deliveryindex in (" & filter & ")"
                        Dim tempDt As New DataTable
                        tempDt = dvv.ToTable()
                        If lblArticleTypeVal.Text = "Combo" Then
                            Dim tempDtCombo As New DataTable
                            tempDtCombo = dtCombo.Select("Status=True").CopyToDataTable
                            If comboNonSTrArticles <> "" Then
                                Dim articles As Array = comboNonSTrArticles.Split(",")
                                If articles.Length > 0 Then
                                    For art As Integer = 0 To articles.Length - 1
                                        tempDtCombo = tempDtCombo.Select("articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                    Next
                                End If
                            End If
                            tempDt = tempDt.Select("Status=True").CopyToDataTable
                            indexGrd = tempDt.Select("IsimageREq=1").Count
                            Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryLineNo='" + filter + "'")
                            For Each dr As DataRow In tempDtCombo.Rows
                                If result(0)("BillLineNo") = dr("ComboSrNo") Then
                                    drLowerVariation = tempDt.Select("EAN='" & dr("EAN") & "' and IsCombo=True")
                                    If drLowerVariation.Length = 0 Then
                                        Dim drLowerVariationNew() As DataRow
                                        drLowerVariationNew = tempDtCombo.Select("ComboSrNo='" & result(0)("BillLineNo") & "' and EAN='" & dr("EAN") & "'")
                                        IsDisplaySrNo = False
                                        indexGrd = indexGrd + 1
                                        SetInDataTable(drLowerVariationNew)
                                    End If
                                End If


                            Next
                            Dim dvvs As New DataView(dtSTR)
                            dvvs.RowFilter = "deliveryindex in (" & filter & ")"
                            Dim dtSs = dvvs.ToTable()
                            dtSs = dtSs.Select("Status=True", "SrNo").CopyToDataTable()
                            BindLowerGrid(dtSs)
                        Else
                            Dim dtSs = dvv.ToTable()
                            dtSs = dtSs.Select("Status=True", "SrNo").CopyToDataTable()
                            BindLowerGrid(dtSs)
                        End If




                    End If

                Else

                    dtSTR = objPCSO.GetCollectionOfSTR()
                    dtSTR.Rows.Clear()
                    dtFinalSTR = dtSTR.Copy()

                    If lblArticleTypeVal.Text.Trim() = "Single" Then
                        If IsEdit Then
                            drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("DeliveryLineNo='" & filter & "'")
                        Else
                            drLowerVariation = _dsPackagingDeliverySTR.Tables("PackagingDelivery").Select("Deliveryindex='" & filter & "'")
                        End If
                        IsDisplaySrNo = True
                        SetInDataTable(drLowerVariation)
                    Else
                        Dim tempDtCombo As New DataTable
                        tempDtCombo = dtCombo.Copy()
                        Dim result As DataRow() = _dsPackagingDeliverySTR.Tables(0).Select("DeliveryLineNo='" + filter + "'")

                        Dim combosrno = 0
                        If comboNonSTrArticles <> "" Then
                            Dim articles As Array = comboNonSTrArticles.Split(",")
                            If articles.Length > 0 Then
                                For art As Integer = 0 To articles.Length - 1
                                    tempDtCombo = tempDtCombo.Select("Status=True AND articlecode<>'" & articles(art).ToString() & "' ").CopyToDataTable()
                                Next
                            End If

                        End If
                        drLowerVariationCombo = dtCombo.Select("Status=True AND ComboSrNo='" & result(0)("BillLineNo") & "'")
                        IsDisplaySrNo = True
                        SetInDataTable(drLowerVariationCombo)
                    End If

                    BindLowerGrid(dtSTR)
                End If

            End If




            AddButtonControlInGrid(1)

            '--- Apply Theme Here 
            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        Call Themechange()
                    Case 2

                    Case Else

                End Select
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Try
            C1Sizer4.BackColor = Color.FromArgb(134, 134, 134)
            With btnAddRemark
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With
            With btnCancel
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With
            With btnSave
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

            With dgUpperGrid
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
                .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
                .Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
                .Rows.MinSize = 25
                .Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
                .Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
            End With

            With dgLowerGrid
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
                .Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
                .Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
                .Rows.MinSize = 25
                .Styles.Normal.Font = New Font("Neo Sans", 9, FontStyle.Regular)
                .Styles.Fixed.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Highlight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.Focus.Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
            End With

            Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)
            With lblArticleName
                .Font = lblFont
                '   .Dock = DockStyle.Top
                .BackColor = Color.Transparent
                .ForeColor = Color.White
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblArticleNameVal
                .Font = lblFont
                '  .Dock = DockStyle.Top
                .BackColor = Color.Transparent
                .ForeColor = Color.White
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblArticleType
                .Font = lblFont
                '    .Dock = DockStyle.Top
                .BackColor = Color.Transparent
                .ForeColor = Color.White
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblArticleTypeVal
                .Font = lblFont
                '   .Dock = DockStyle.Top
                .BackColor = Color.Transparent
                .ForeColor = Color.White
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

        Catch ex As Exception

        End Try
    End Function

    Public Sub BindUpperGrid(ByVal dtUpper As DataTable)
        Try

            Dim indexP As Integer = 1
            For Each dr As DataRow In dtUpper.Rows

                dgUpperGrid.Rows.Add()
                dgUpperGrid.Rows(indexP)("DeliveryDate") = dr("DeliveryDate")
                dgUpperGrid.Rows(indexP)("DeliveryTime") = dr("DeliveryTime")
                If IsEdit Then
                    dgUpperGrid.Rows(indexP)("UOM") = dr("UnitOfMeasure").ToString()
                Else
                    dgUpperGrid.Rows(indexP)("UOM") = dr("UOM").ToString()
                End If

                dgUpperGrid.Rows(indexP)("OrderQty") = dr("Quantity").ToString()

                indexP = indexP + 1

            Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub BindLowerGrid(ByVal dtLower As DataTable)
        Try


            dgLowerGrid.Rows.RemoveRange(1, dgLowerGrid.Rows.Count - 1)
            Dim index As Integer = 1
            Dim indexP As Integer = 1
            For Each dr As DataRow In dtLower.Rows
                If dr("Status") Is DBNull.Value Then
                    dgLowerGrid.Rows.Add()
                    'If dr("IsImageReq") = "1" Then
                    '    dgLowerGrid.Rows(indexP)("DEL") = Nothing
                    'Else
                    '    dgLowerGrid.Rows(indexP)("DEL") = "1"
                    'End If
                    dgLowerGrid.Rows(indexP)("SrNo") = dr("SrNo")
                    dgLowerGrid.Rows(indexP)("Code") = dr("ArticleCode")
                    dgLowerGrid.Rows(indexP)("ArticleDescription") = dr("Discription").ToString()

                    If lblArticleTypeVal.Text = "Single" Then
                        dgLowerGrid.Rows(indexP)("Qty. Per Box") = "-"
                        dgLowerGrid.Rows(indexP)("Wt. Per Piece") = "-"
                        dgLowerGrid.Rows(indexP)("Wt. Per Box") = "-"
                    Else
                        dgLowerGrid.Rows(indexP)("Qty. Per Box") = dr("QtyPerBox")
                        dgLowerGrid.Rows(indexP)("Wt. Per Piece") = dr("WtPerPiece")
                        dgLowerGrid.Rows(indexP)("Wt. Per Box") = dr("WtPerBox")
                    End If



                    dgLowerGrid.Rows(indexP)("STR UOM") = dr("STRUOM")
                    dgLowerGrid.Rows(indexP)("STRQty") = IIf((dr("STRQty") Is DBNull.Value), "0", dr("STRQty"))
                    dgLowerGrid.Rows(indexP)("STRDate") = IIf((dr("STRDate") Is DBNull.Value), "0", dr("STRDate"))
                    dgLowerGrid.Rows(indexP)("STRTime") = IIf((dr("STRTime") Is DBNull.Value), "0", dr("STRTime"))
                    dgLowerGrid.Rows(indexP)("IsImageReq") = dr("IsImageReq")
                    dgLowerGrid.Rows(indexP)("Deliveryindex") = dr("Deliveryindex")
                    dgLowerGrid.Rows(indexP)("strindex") = dr("strindex")
                    indexP = indexP + 1
                ElseIf dr("Status") Then
                    dgLowerGrid.Rows.Add()
                    'If dr("IsImageReq") = "1" Then
                    '    dgLowerGrid.Rows(indexP)("DEL") = Nothing
                    'Else
                    '    dgLowerGrid.Rows(indexP)("DEL") = "1"
                    'End If
                    dgLowerGrid.Rows(indexP)("SrNo") = dr("SrNo")
                    dgLowerGrid.Rows(indexP)("Code") = dr("ArticleCode")
                    dgLowerGrid.Rows(indexP)("ArticleDescription") = dr("Discription").ToString()

                    If lblArticleTypeVal.Text = "Single" Then
                        dgLowerGrid.Rows(indexP)("Qty. Per Box") = "-"
                        dgLowerGrid.Rows(indexP)("Wt. Per Piece") = "-"
                        dgLowerGrid.Rows(indexP)("Wt. Per Box") = "-"
                    Else
                        dgLowerGrid.Rows(indexP)("Qty. Per Box") = dr("QtyPerBox")
                        dgLowerGrid.Rows(indexP)("Wt. Per Piece") = dr("WtPerPiece")
                        dgLowerGrid.Rows(indexP)("Wt. Per Box") = dr("WtPerBox")
                    End If



                    dgLowerGrid.Rows(indexP)("STR UOM") = dr("STRUOM")
                    dgLowerGrid.Rows(indexP)("STRQty") = IIf((dr("STRQty") Is DBNull.Value), "0", dr("STRQty"))
                    dgLowerGrid.Rows(indexP)("STRDate") = IIf((dr("STRDate") Is DBNull.Value), "0", dr("STRDate"))
                    dgLowerGrid.Rows(indexP)("STRTime") = IIf((dr("STRTime") Is DBNull.Value), "0", dr("STRTime"))
                    dgLowerGrid.Rows(indexP)("IsImageReq") = dr("IsImageReq")
                    dgLowerGrid.Rows(indexP)("Deliveryindex") = dr("Deliveryindex")
                    dgLowerGrid.Rows(indexP)("strindex") = dr("strindex")
                    indexP = indexP + 1
                End If

            Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub UpperGridSetting()
        Try
            dgUpperGrid.Cols("DeliveryDate").Width = 100
            dgUpperGrid.Cols("DeliveryDate").Caption = "Delivery Date"
            dgUpperGrid.Cols("DeliveryDate").DataType = Type.GetType("System.DateTime")
            dgUpperGrid.Cols("DeliveryDate").Format = "d"
            dgUpperGrid.Cols("DeliveryDate").AllowEditing = False
            dgUpperGrid.Cols("DeliveryDate").TextAlign = TextAlignEnum.CenterCenter

            dgUpperGrid.Cols("DeliveryTime").Width = 100
            dgUpperGrid.Cols("DeliveryTime").Caption = "Delivery Time"
            dgUpperGrid.Cols("DeliveryTime").DataType = Type.GetType("System.DateTime")
            dgUpperGrid.Cols("DeliveryTime").Format = "t"
            dgUpperGrid.Cols("DeliveryTime").AllowEditing = False
            dgUpperGrid.Cols("DeliveryTime").TextAlign = TextAlignEnum.CenterCenter

            dgUpperGrid.Cols("UOM").Caption = "UOM"
            dgUpperGrid.Cols("UOM").Width = 82
            dgUpperGrid.Cols("UOM").AllowEditing = False

            dgUpperGrid.Cols("OrderQty").Width = 82
            dgUpperGrid.Cols("OrderQty").Caption = "Order Qty"
            dgUpperGrid.Cols("OrderQty").DataType = Type.GetType("System.Decimal")
            dgUpperGrid.Cols("OrderQty").Format = "0.000"
            dgUpperGrid.Cols("OrderQty").AllowEditing = False

            dgUpperGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub LowerGridSetting()
        Try
            dgLowerGrid.Cols("DEL").Caption = ""
            dgLowerGrid.Cols("DEL").Width = 20
            dgLowerGrid.Cols("DEL").ComboList = "..."
            dgLowerGrid.Cols("PLUS").Caption = ""
            dgLowerGrid.Cols("PLUS").Width = 24
            dgLowerGrid.Cols("SrNo").Width = 50
            dgLowerGrid.Cols("SrNo").Caption = "Sr. No."
            dgLowerGrid.Cols("SrNo").DataType = Type.GetType("System.Decimal")
            dgLowerGrid.Cols("SrNo").AllowEditing = False

            dgLowerGrid.Cols("Code").Caption = "Code"
            dgLowerGrid.Cols("Code").Width = 55
            dgLowerGrid.Cols("Code").AllowEditing = False

            dgLowerGrid.Cols("ArticleDescription").Caption = "Article Description"
            dgLowerGrid.Cols("ArticleDescription").Width = 370
            dgLowerGrid.Cols("ArticleDescription").AllowEditing = False

            dgLowerGrid.Cols("Qty. Per Box").Caption = "Qty. Per Box"
            dgLowerGrid.Cols("Qty. Per Box").Width = 82
            'dgUpperGrid.Cols("Qty. Per Box").DataType = Type.GetType("System.Decimal")
            'dgUpperGrid.Cols("Qty. Per Box").Format = "0"
            dgLowerGrid.Cols("Qty. Per Box").AllowEditing = False

            dgLowerGrid.Cols("Wt. Per Piece").Caption = "Wt. Per Piece"
            dgLowerGrid.Cols("Wt. Per Piece").Width = 88
            'dgUpperGrid.Cols("Wt. Per Piece").DataType = Type.GetType("System.Decimal")
            'dgUpperGrid.Cols("Wt. Per Piece").Format = "0"
            dgLowerGrid.Cols("Wt. Per Piece").AllowEditing = False

            dgLowerGrid.Cols("Wt. Per Box").Caption = "Wt. Per Box"
            dgLowerGrid.Cols("Wt. Per Box").Width = 78
            'dgUpperGrid.Cols("Wt. Per Box").DataType = Type.GetType("System.Decimal")
            'dgUpperGrid.Cols("Wt. Per Box").Format = "0"
            dgLowerGrid.Cols("Wt. Per Box").AllowEditing = False

            dgLowerGrid.Cols("STR UOM").Caption = "STR UOM"
            dgLowerGrid.Cols("STR UOM").Width = 67
            dgLowerGrid.Cols("STR UOM").AllowEditing = False

            dgLowerGrid.Cols("STRQty").Caption = "STR Qty"
            dgLowerGrid.Cols("STRQty").Width = 59
            dgLowerGrid.Cols("STRQty").DataType = Type.GetType("System.Decimal")
            dgLowerGrid.Cols("STRQty").Format = "0.000"

            dgLowerGrid.Cols("STRDate").Caption = "STR Date"
            dgLowerGrid.Cols("STRDate").Width = 87
            dgLowerGrid.Cols("STRDate").DataType = Type.GetType("System.DateTime")
            dgLowerGrid.Cols("STRDate").Format = "d"

            dgLowerGrid.Cols("STRTime").Caption = "STR Time"
            dgLowerGrid.Cols("STRTime").Width = 70
            dgLowerGrid.Cols("STRTime").DataType = Type.GetType("System.DateTime")
            dgLowerGrid.Cols("STRTime").Format = "t"

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Protected controlList As New ArrayList
    Public Function AddButtonControlInGrid(ByVal rowIndex As Integer) As Boolean
        Try

            For Each drGridRow As C1.Win.C1FlexGrid.Row In dgLowerGrid.Rows

                If Not (drGridRow.Index = 0) Then
                    If dgLowerGrid.Rows(drGridRow.Index)("IsImageReq").ToString() = "1" Then


                        Dim getColumnType As String = String.Empty

                        'Create styles with data types, formats, etc
                        Dim cellStyle As C1.Win.C1FlexGrid.CellStyle

                        cellStyle = dgLowerGrid.Styles.Add("CellImageType")
                        cellStyle.DataType = Type.GetType("System.String")
                        cellStyle.TextAlign = TextAlignEnum.LeftCenter
                        cellStyle.WordWrap = True

                        'Assign styles to editable cells
                        Dim assignCellStyles As CellRange
                        dgLowerGrid.Rows(drGridRow.Index).HeightDisplay = 16

                        Dim ButtonX As Integer = dgLowerGrid.Cols("PLUS").WidthDisplay

                        'Create some new controls
                        Dim btnBrowse As New Button
                        If lblArticleTypeVal.Text.Trim() = "Single" Then
                            btnBrowse.Tag = dgLowerGrid.Rows(drGridRow.Index)("deliveryindex").ToString()
                        Else
                            btnBrowse.Tag = dgLowerGrid.Rows(drGridRow.Index)("deliveryindex").ToString() & "-" & dgLowerGrid.Rows(drGridRow.Index)("Code").ToString() & "-" & dgLowerGrid.Rows(drGridRow.Index)("SrNo").ToString()
                        End If

                        btnBrowse.MaximumSize = New System.Drawing.Size(18, 28)
                        btnBrowse.Anchor = AnchorStyles.None
                        Dim s As Size = New System.Drawing.Size(8, 7)
                        Dim img As Image
                        'btn.imageLayout = Nothing

                        img = Image.FromFile(Application.StartupPath & "\images\yes.png")

                        btnBrowse.Image = New Bitmap(img, s)
                        btnBrowse.ImageAlign = ContentAlignment.MiddleCenter
                        'btnBrowse.SetRowIndex = rowIndex
                        'btnBrowse.Text = "Barcode"
                        'btnBrowse.Name = "btnPlus" + grdScanItem.Rows(drGridRow.Index)("EAN").ToString()
                        btnBrowse.Name = "btnPlus" '+ dgLowerGrid.Rows(drGridRow.Index)("DeliveryIndex").ToString()
                        'Insert hosted control into grid
                        btnBrowse.ImageAlign = ContentAlignment.MiddleCenter
                        btnBrowse.Padding = New Padding(4)
                        btnBrowse.BackgroundImage = Nothing
                        btnBrowse.BackColor = Color.AliceBlue
                        'btnBrowse.
                        'Insert hosted control into grid
                        btnBrowse.TabStop = False
                        dgLowerGrid.Controls.Add(btnBrowse)

                        'host them in the C1FlexGrid
                        controlList.Add(New HostedControl(dgLowerGrid, btnBrowse, drGridRow.Index, dgLowerGrid.Cols("PLUS").Index, ButtonX, ButtonX))

                        'ImagePathRowIndex = rowIndex
                        assignCellStyles = dgLowerGrid.GetCellRange(drGridRow.Index, dgLowerGrid.Cols("PLUS").Index)
                        assignCellStyles.Style = dgLowerGrid.Styles("CellImageType")
                        AddHandler btnBrowse.Click, AddressOf BrowseImagePath
                        'Else
                        '    controlList.Remove(New HostedControl(grdScanItem, btnBrowse, drGridRow.Index, grdScanItem.Cols("PLUS").Index, ButtonX, ButtonX))
                    End If
                End If
            Next


            ' AddHandler btnBrowse.Click, AddressOf BrowseImagePath



        Catch ex As Exception
            MessageBox.Show(ex.Message)
            LogException(ex)
        End Try
    End Function

    Private Sub dgLowerGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles dgLowerGrid.AfterEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = dgLowerGrid.Row
            Dim objcomm As New clsCommon
            Dim vCurrentDate As Date = objcomm.GetCurrentDate

            Dim result As DataRow() = dtSTR.Select("strindex='" + dgLowerGrid.Item(CurrentRow, "strindex").ToString() + "'")
            If dgLowerGrid.Cols(CurrentCell).Name = "STRQty" Then
                If (dgLowerGrid.Item(CurrentRow, "STRQty") < 0) Then
                    ShowMessage("STR Qty cannot be less than 0", getValueByKey("CLAE04"))
                    dgLowerGrid.Item(CurrentRow, "STRQty") = 0
                    Exit Sub
                End If
                result(0)("STRQty") = IIf(dgLowerGrid.Item(CurrentRow, "STRQty") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRQty"))
            End If
            If dgLowerGrid.Cols(CurrentCell).Name = "STRDate" Then

                'For i = 1 To dgLowerGrid.Rows.Count - 1
                '    If dgLowerGrid.Rows(i)("IsImageReq") = 0 Then

                '        If dgLowerGrid.Rows(CurrentRow)("STRDate") = dgLowerGrid.Rows(i)("STRDate") AndAlso CurrentRow <> i Then
                '            ShowMessage("STR date can not be Same or Equal for Variation ", "SO009 - " & getValueByKey("CLAE04"))
                '            dgLowerGrid.Item(CurrentRow, "STRDate") = Nothing
                '            Exit Sub
                '        End If
                '    End If
                'Next
                Dim resVardr As DataRow() = dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                For Each resdr As DataRow In dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                    Dim strdt = IIf(resdr("STRDate") Is DBNull.Value, Nothing, resdr("STRDate"))
                    If resdr("SrNo") <> dgLowerGrid.Item(CurrentRow, "SrNo").ToString() Then
                        'If dgLowerGrid.Item(CurrentRow, "STRDate").Date = strdt Then
                        '    ShowMessage("STR date can not be Same or Equal for Variation ", "SO009 - " & getValueByKey("CLAE04"))
                        '    dgLowerGrid.Item(CurrentRow, "STRDate") = Nothing
                        'End If
                    End If
                Next
                '--For Checking if same date then can't be sane time
                Dim CrntSTRTime As DateTime = dgLowerGrid.Item(CurrentRow, "STRTime")
                CrntSTRTime = CrntSTRTime.ToString("hh:mm tt")
                '  Dim resVardr As DataRow() = dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                For Each resdr As DataRow In dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                    Dim strdt = IIf(resdr("STRDate") Is DBNull.Value, Nothing, resdr("STRDate"))
                    strdt = Convert.ToDateTime(strdt).ToShortDateString
                    If resdr("SrNo") <> dgLowerGrid.Item(CurrentRow, "SrNo").ToString() Then
                        If dgLowerGrid.Item(CurrentRow, "STRDate").Date = strdt Then
                            Dim STRTime = IIf(resdr("STRTime") Is DBNull.Value, Nothing, resdr("STRTime"))
                            STRTime = IIf(STRTime Is Nothing, Nothing, Convert.ToDateTime(STRTime).ToShortTimeString)
                            If STRTime <> Nothing Then
                                If CrntSTRTime = STRTime Then
                                    ShowMessage("STR Time cannot be Same or Equal for Variation ", "SO009 - " & getValueByKey("CLAE04"))
                                    dgLowerGrid.Item(CurrentRow, "STRTime") = Nothing
                                    result(0)("STRTime") = Nothing
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                Next

                If dgLowerGrid.Item(CurrentRow, "STRDate").Date < vCurrentDate.ToShortDateString Then
                    ShowMessage("STR date can not be back dated", "SO009 - " & getValueByKey("CLAE04"))
                    dgLowerGrid.Item(CurrentRow, "STRDate") = Nothing
                    Exit Sub
                End If
                result(0)("STRDate") = IIf(dgLowerGrid.Item(CurrentRow, "STRDate") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRDate"))
            End If
            If dgLowerGrid.Cols(CurrentCell).Name = "STRTime" Then
                Dim CrntSTRTime As DateTime = dgLowerGrid.Item(CurrentRow, "STRTime")
                CrntSTRTime = CrntSTRTime.ToString("hh:mm tt")
                Dim resVardr As DataRow() = dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                For Each resdr As DataRow In dtSTR.Select("DeliveryIndex='" + filter + "' AND ArticleCode='" + dgLowerGrid.Item(CurrentRow, "Code").ToString() + "'")
                    Dim strdt = IIf(resdr("STRDate") Is DBNull.Value, Nothing, resdr("STRDate"))
                    strdt = Convert.ToDateTime(strdt).ToShortDateString
                    If resdr("SrNo") <> dgLowerGrid.Item(CurrentRow, "SrNo").ToString() Then
                        If dgLowerGrid.Item(CurrentRow, "STRDate").Date = strdt Then
                            Dim STRTime = IIf(resdr("STRTime") Is DBNull.Value, Nothing, resdr("STRTime"))
                            STRTime = IIf(STRTime Is Nothing, Nothing, Convert.ToDateTime(STRTime).ToShortTimeString)
                            If STRTime <> Nothing Then
                                If CrntSTRTime = STRTime Then
                                    ShowMessage("STR Time cannot be Same or Equal for Variation ", "SO009 - " & getValueByKey("CLAE04"))
                                    dgLowerGrid.Item(CurrentRow, "STRTime") = Nothing
                                    result(0)("STRTime") = Nothing
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                Next

                If dgLowerGrid.Item(CurrentRow, "STRDate").Date = vCurrentDate.ToShortDateString Then

                    If DateTime.Compare(CrntSTRTime, DateTime.Now.ToString("hh:mm tt")) < 1 Then
                        ShowMessage("STR Time can not be back dated", "SO009 - " & getValueByKey("CLAE04"))
                        dgLowerGrid.Item(CurrentRow, "STRTime") = DateTime.Now.AddDays(+1)
                        result(0)("STRTime") = IIf(dgLowerGrid.Item(CurrentRow, "STRTime") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRTime"))
                        dtSTR.AcceptChanges()
                        Exit Sub
                    End If
                End If
            End If
            dtSTR.AcceptChanges()

            '---FinalTable
            Dim result1 As DataRow() = dtSTR.Select("Deliveryindex='" + filter + "' And SrNo= '" + dgLowerGrid.Item(CurrentRow, "SrNo").ToString() + "' AND ArticleCode='" & dgLowerGrid.Item(CurrentRow, "Code") & "' AND Status=True")
            If result1.Length > 0 Then
                If dgLowerGrid.Cols(CurrentCell).Name = "STRQty" Then
                    result1(0)("STRQty") = IIf(dgLowerGrid.Item(CurrentRow, "STRQty") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRQty"))
                End If
                If dgLowerGrid.Cols(CurrentCell).Name = "STRDate" Then
                    result1(0)("STRDate") = IIf(dgLowerGrid.Item(CurrentRow, "STRDate") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRDate"))
                End If
                If dgLowerGrid.Cols(CurrentCell).Name = "STRTime" Then
                    result1(0)("STRTime") = IIf(dgLowerGrid.Item(CurrentRow, "STRTime") Is DBNull.Value, 0, dgLowerGrid.Item(CurrentRow, "STRTime"))
                End If
                dtSTR.AcceptChanges()

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgLowerGrid_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles dgLowerGrid.Paint
        For Each hosted As HostedControl In controlList
            hosted.UpdatePosition()
        Next
    End Sub

    Private Sub BrowseImagePath(sender As Object, e As EventArgs)
        Try
            Dim leadIndex As Double = 0
            Dim drVariation As DataRow

            If lblArticleTypeVal.Text.Trim() = "Single" Then
                drVariation = dtSTR.Select("deliveryindex='" & DirectCast(sender, Button).Tag & "'")(0)
                Dim drPackgVariationCount As DataRow()
                drPackgVariationCount = dtSTR.Select("deliveryindex='" & DirectCast(sender, Button).Tag & "' and status=true")
                If drPackgVariationCount.Length > 0 Then
                    leadIndex = Convert.ToDouble("1" & "." & drPackgVariationCount.Length)
                End If
            Else
                Dim str = DirectCast(sender, Button).Tag
                Dim result As String() = str.Split("-")
                drVariation = dtSTR.Select("deliveryindex='" & result(0) & "' And ArticleCode='" & result(1) & "'  and status=true")(0)
                Dim drPackgVariationCount As DataRow()
                drPackgVariationCount = dtSTR.Select("deliveryindex='" & result(0) & "' And ArticleCode='" & result(1) & "' and status=true")
                If drPackgVariationCount.Length > 0 Then
                    leadIndex = Convert.ToDouble(result(2) & "." & drPackgVariationCount.Length)
                End If

            End If



            SetSTRVarion(drVariation, leadIndex, True)
            'dtSTR = dtSTR.Select("", "deliveryindex").CopyToDataTable()
            'BindLowerGrid(dtSTR)

            If dtSTR.Rows.Count > 0 Then
                'Dim dvv As New DataView(dtSTR)
                'dvv.RowFilter = "deliveryindex in (" & filter & ")"
                'If dvv.Count > 0 Then
                Dim dvvv As New DataView(dtSTR)
                dvvv.RowFilter = "deliveryindex in (" & filter & ")"
                Dim dtSs = dvvv.ToTable()
                dtSs = dtSs.Select("", "SrNo").CopyToDataTable()
                BindLowerGrid(dtSs)
                'End If
            End If

            AddButtonControlInGrid(1)


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim rowAddress As DataRow
    Private Function SetInDataTable(ByVal drItemsRow() As DataRow)
        Try
            ' Dim Value = dtSTR.Compute("Max(StrIndex)", "")
            ' strindex = Value + 1

            For i = 0 To drItemsRow.Length - 1

                rowAddress = dtSTR.NewRow()
                rowAddress("SiteCode") = clsAdmin.SiteCode
                rowAddress("SaleOrderNumber") = SONumber
                rowAddress("FinYear") = clsAdmin.Financialyear
                rowAddress("SrNo") = indexGrd
                rowAddress("EAN") = drItemsRow(i)("EAN")
                rowAddress("ArticleCode") = drItemsRow(i)("ArticleCode")

                rowAddress("Status") = True
                If lblArticleTypeVal.Text = "Single" Then
                    rowAddress("Discription") = drItemsRow(i)("Discription").ToString()
                    rowAddress("QtyPerBox") = "-"
                    rowAddress("WtPerPiece") = "-"
                    rowAddress("WtPerBox") = "-"
                    If IsEdit Then
                        rowAddress("STRUOM") = drItemsRow(i)("UnitOfMeasure")
                    Else
                        rowAddress("STRUOM") = drItemsRow(i)("UOM")
                    End If

                Else
                    rowAddress("Discription") = drItemsRow(i)("ArticleDescription").ToString()
                    rowAddress("QtyPerBox") = drItemsRow(i)("Qty") & " " & comboUOM
                    rowAddress("WtPerPiece") = drItemsRow(i)("Weight").ToString() & " " & "KGS" 'drItemsRow(i)("BaseUOM").ToString()
                    rowAddress("WtPerBox") = FormatNumber((drItemsRow(i)("Qty") * drItemsRow(i)("Weight")), 3) & " " & "KGS" 'drItemsRow(i)("BaseUOM").ToString()
                    'rowAddress("QtyPerBox") = (comboQuantity * drItemsRow(i)("Qty")) & " " & comboUOM
                    'rowAddress("WtPerPiece") = drItemsRow(i)("Weight").ToString() & " " & "KGS"
                    'rowAddress("WtPerBox") = ((comboQuantity * drItemsRow(i)("Qty")) * drItemsRow(i)("Weight")) & " " & "KGS" 'drItemsRow(i)("BaseUOM").ToString()
                    rowAddress("STRUOM") = drItemsRow(i)("BaseUOM")
                End If

                rowAddress("IsImageReq") = "1"
                rowAddress("DeliveryIndex") = filter
                If dtSTR.Rows.Count = 0 Then
                    rowAddress("StrIndex") = StrIndex
                Else
                    rowAddress("StrIndex") = StrIndex + 1
                    StrIndex = StrIndex + 1
                End If

                If lblArticleTypeVal.Text = "Single" Then
                    rowAddress("IsCombo") = False
                Else
                    rowAddress("IsCombo") = True
                End If


                dtSTR.Rows.Add(rowAddress)
                If IsDisplaySrNo = True Then
                    indexGrd += 1
                End If
            Next


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function SetSTRVarion(ByVal drItemsRow As DataRow, Optional ByVal leadIndex As Double = 0, Optional ByVal isVaiationAdded As Boolean = False) As Boolean
        Try
            Dim drAddItemPackaging As DataRow
            drAddItemPackaging = dtSTR.NewRow()

            drAddItemPackaging("SiteCode") = drItemsRow("SiteCode")
            drAddItemPackaging("SaleOrderNumber") = drItemsRow("SaleOrderNumber")
            drAddItemPackaging("FinYear") = drItemsRow("FinYear")

            If isVaiationAdded AndAlso leadIndex > 0 Then
                drAddItemPackaging("SrNo") = leadIndex
            End If
            drAddItemPackaging("Status") = True
            drAddItemPackaging("EAN") = drItemsRow("EAN")
            drAddItemPackaging("ArticleCode") = drItemsRow("ArticleCode")
            drAddItemPackaging("Discription") = drItemsRow("Discription").ToString()

            If lblArticleTypeVal.Text = "Single" Then
                drAddItemPackaging("QtyPerBox") = "-"
                drAddItemPackaging("WtPerPiece") = "-"
                drAddItemPackaging("WtPerBox") = "-"
            Else
                drAddItemPackaging("QtyPerBox") = drItemsRow("QtyPerBox")
                drAddItemPackaging("WtPerPiece") = drItemsRow("WtPerPiece")
                drAddItemPackaging("WtPerBox") = drItemsRow("WtPerBox")
            End If

            drAddItemPackaging("STRUOM") = drItemsRow("STRUOM")
            'drAddItemPackaging("STRQty") = ""
            'drAddItemPackaging("STRDate") = ""
            'drAddItemPackaging("STRTime") = ""

            drAddItemPackaging("IsImageReq") = "0"
            drAddItemPackaging("DeliveryIndex") = drItemsRow("DeliveryIndex")
            drAddItemPackaging("StrIndex") = StrIndex + 1
            StrIndex = StrIndex + 1
            drAddItemPackaging("IsCombo") = drItemsRow("IsCombo")

            dtSTR.Rows.Add(drAddItemPackaging)
            dtSTR.AcceptChanges()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    Private Sub dgLowerGrid_CellButtonClick(sender As Object, e As RowColEventArgs) Handles dgLowerGrid.CellButtonClick
        Try
            If dgLowerGrid.Rows(dgLowerGrid.RowSel)("IsImageReq").ToString() = "0" Then
                Dim selectedRows() As DataRow = dtSTR.Select("SrNo=" & dgLowerGrid.Rows(dgLowerGrid.RowSel)("SrNo").ToString() & " And Deliveryindex='" + filter + "' ", "", DataViewRowState.CurrentRows)
                For Each dr As DataRow In selectedRows
                    If dr("sortindex") Is DBNull.Value Then
                        dr.Delete()
                    Else
                        dr("Status") = False
                    End If

                Next
                dtSTR.AcceptChanges()
                If lblArticleTypeVal.Text.Trim() = "Single" Then
                    Dim count = 1
                    For indexp = 0 To dtSTR.Rows.Count - 1
                        If dtSTR.Rows(indexp)("deliveryindex") = filter Then
                            If Not dtSTR.Rows(indexp)("IsImageReq") = "1" AndAlso dtSTR.Rows(indexp)("status") Then
                                dtSTR.Rows(indexp)("SrNo") = "1" & "." & count
                                count += 1
                            End If
                        End If
                    Next

                Else

                    dtSTR = dtSTR.Select("", "SrNo").CopyToDataTable()

                    Dim count = 1
                    Dim header As Integer
                    For indexp = 0 To dtSTR.Rows.Count - 1
                        If dtSTR.Rows(indexp)("deliveryindex") = filter Then

                            If dtSTR.Rows(indexp)("IsImageReq") = "1" Then
                                header = dtSTR.Rows(indexp)("SrNo")
                                count = 1
                            End If

                            If Not dtSTR.Rows(indexp)("IsImageReq") = "1" Then
                                dtSTR.Rows(indexp)("SrNo") = header & "." & count
                                count += 1
                            End If

                        End If

                    Next
                End If

                Dim dvvv As New DataView(dtSTR)
                dvvv.RowFilter = "deliveryindex in (" & filter & ")"
                Dim dtSs = dvvv.ToTable()
                dtSs = dtSs.Select("", "SrNo").CopyToDataTable()
                BindLowerGrid(dtSs)

                AddButtonControlInGrid(1)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try


            'If dtFinalSTR.Rows.Count = 0 Then
            '    dtFinalSTR = dtSTR.Copy()
            'Else

            '    For Each dr As DataRow In dtSTR.Rows
            '        Dim result As DataRow() = dtFinalSTR.Select("StrIndex=" + dr("StrIndex").ToString())

            '        If result.Count = 0 Then
            '            Dim row As DataRow = dtSTR.Select("StrIndex=" + dr("StrIndex").ToString())(0)
            '            dtFinalSTR.ImportRow(row)
            '            dtFinalSTR.AcceptChanges()
            '        End If
            '    Next

            'End If

            Dim drSTRQty() As DataRow = dtSTR.Select("Status=True And STRQty > 0 AND Deliveryindex='" & filter & "'")
            If drSTRQty.Length = 0 Then
                ShowMessage("Please Enter STR Details for atleast one item", getValueByKey("CLAE04"))
                Exit Sub
            End If
            For Each dr As DataRow In dtSTR.Select("Status=True AND STRQty >0 AND Deliveryindex='" & filter & "'")

                If dr("STRQty") Is DBNull.Value Then
                    ShowMessage("STR Qty can't be less than or equal to zero", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If dr("STRDate") Is DBNull.Value Then
                    ShowMessage("STR Date can't be can not kept blank", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If dr("STRTime") Is DBNull.Value Then
                    ShowMessage("STR Time can't be can not kept blank", getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Next
            For Each drVariations As DataRow In dtSTR.Select("Status=True AND IsImageReq=0 AND Deliveryindex='" & filter & "'")

                Dim drHeader() As DataRow = dtSTR.Select("Status=True and IsImageReq=1 and EAN='" & drVariations("EAN") & "' and ArticleCode='" & drVariations("ArticleCode") & "'AND Deliveryindex='" & filter & "'")
                drVariations("STRQty") = IIf(drVariations("STRQty") Is DBNull.Value, 0, drVariations("STRQty"))
                drHeader(0)("STRQty") = IIf(drHeader(0)("STRQty") Is DBNull.Value, 0, drHeader(0)("STRQty"))
                If drHeader.Length > 0 Then
                    If drVariations("STRQty") = 0 Then
                        ShowMessage("Please Remove Variation whose STR Qty is 0", getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                    If drHeader(0)("STRQty") = 0 Then
                        ShowMessage("STR Details can't be blank for Parent article", getValueByKey("CLAE04"))
                        Exit Sub
                    End If
                End If
            Next


            dtFinalSTR = dtSTR.Copy()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Public Function CompareAtLoad(ByVal dtStr As DataTable, ByVal dtfinal As DataTable) As Boolean

        Try

            For indexlen = 0 To dtStr.Rows.Count - 1
                Dim result As DataRow() = dtfinal.Select("StrIndex=" & dtStr.Rows(indexlen)("StrIndex"))
                If result.Count = 0 Then
                    dtStr.Rows(indexlen).Delete()
                End If
            Next


            'For Each dr As DataRow In dtStr.Rows
            '    Dim result As DataRow() = dtfinal.Select("StrIndex=" + dr("StrIndex").ToString())
            '    If result.Count = 0 Then
            '        'dr.Delete()
            '        dtStr.Rows.Remove(dr)
            '    End If
            'Next
            dtStr.AcceptChanges()


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub btnAddRemark_Click(sender As Object, e As EventArgs) Handles btnAddRemark.Click
        Try

            Using objfactoryRemark As New frmPCSTRFactoryRemark
                objfactoryRemark.DtFactoryRemarks = DtFactoryRemarks
                Dim dialogResult = objfactoryRemark.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    Exit Sub
                ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                    DtFactoryRemarks = objfactoryRemark.DtFactoryRemarks
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
End Class