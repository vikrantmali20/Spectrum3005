Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class FrmDefineKit

    Dim Objcm As New clsCommon


    Dim objArticle As New clsArticleCombo
    Dim dtCustmData As New DataTable
    Dim dtArticle As New DataTable
    Dim objItem As New clsIteamSearch
    Dim dtGuest As New DataTable
    Dim DtUpGradableItem As New DataTable
    Dim StrFilter As String
    Dim dtTree As DataTable
    Dim dt As DataTable
    Dim opendate As Date
    Dim _IsNetSale As Boolean = False

    Dim DtArticleCOmbo As DataTable
    Dim articleCode As String = ""
    Dim UpGradableArticlecode As String = ""
    Dim IsArticleMapForKit As Boolean = False
    Dim PlaceHolderString As String = "Please select the kit"
    Dim screenWidth As Integer = 0
    Dim screenHeight As Integer = 0



#Region "Events"
    Private Sub frmProductNotificationPopups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
      

            screenWidth = Screen.PrimaryScreen.Bounds.Width
            screenHeight = Screen.PrimaryScreen.Bounds.Height
            Dim condition As String

            Dim dtBind As DataTable = Objcm.GetKitArticle(clsAdmin.SiteCode)
            If Not dtBind Is Nothing AndAlso dtBind.Rows.Count > 0 Then
                Call SetWildSearchTextBox(dtBind, txtFilterArticle, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If



            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'and A.ArticalTypeCode ='Single'"
            Dim GdtAllArticle = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
             If GdtAllArticle.Rows.Count > 0 Then
                Call SetWildSearchTextBox(GdtAllArticle, txtSingleItemFliter, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If

            If GdtAllArticle.Rows.Count > 0 Then
                If Not dtBind Is Nothing AndAlso dtBind.Rows.Count > 0 Then
                    Dim view As New DataView(dtBind, "", "", DataViewRowState.CurrentRows)
                End If
                ' TxtupGradableItem.DtSearchData = view.ToTable
                ' Call SetWildSearchTextBox(GdtAllArticle, TxtupGradableItem, key:="Key", Value:="Value", searchData:="SearchData")
            End If


            dtGuest = objArticle.GetDetailsForKitArticle()
            dtGuest.Clear()

            DtUpGradableItem = objArticle.GetDetailsForGuest()
            DtUpGradableItem.Clear()

            GrpKitDetail.Visible = True
            BlankItemDetailSection()
            DtArticleCOmbo = objItem.GetMstArticleComboStruct()



            hideAllPanelAndButton()
            ReadOnlyField()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

            If screenWidth > 1024 Then
                txtFilterArticle.IsSetLocation = True
                txtFilterArticle.ListBoxXCoordinate = 403
                txtFilterArticle.ListBoxYCoordinate = 143
                txtSingleItemFliter.IsSetLocation = True
                txtSingleItemFliter.ListBoxXCoordinate = 403
                txtSingleItemFliter.ListBoxYCoordinate = 383
            Else
                txtFilterArticle.IsSetLocation = True
                txtFilterArticle.ListBoxXCoordinate = 233
                txtFilterArticle.ListBoxYCoordinate = 143
                txtSingleItemFliter.IsSetLocation = True
                txtSingleItemFliter.ListBoxXCoordinate = 233
                txtSingleItemFliter.ListBoxYCoordinate = 383
            End If

            '    txtFilterArticle.IsCallFromPosTab = True
            ' txtSingleItemFliter.IsCallFromPosTab = True
            ' Me.GrdShowData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black
            BtnCancel.Visible = True
            BtnCancel.Text = "Close"
            AddHandler txtFilterArticle.Click, AddressOf TxtClick
            AddHandler txtFilterArticle.Leave, AddressOf TxtLeave

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        Me.BackColor = Color.FromArgb(76, 76, 76)
        BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSave.BackColor = Color.Transparent
        BtnSave.BackColor = Color.FromArgb(0, 107, 163)
        BtnSave.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSave.FlatStyle = FlatStyle.Flat
        BtnSave.FlatAppearance.BorderSize = 0
        BtnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnCancel.BackColor = Color.Transparent
        BtnCancel.BackColor = Color.FromArgb(0, 107, 163)
        BtnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        BtnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnCancel.FlatStyle = FlatStyle.Flat
        BtnCancel.FlatAppearance.BorderSize = 0
        BtnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        LblKitShortName.ForeColor = Color.White
        LblMaterialType.ForeColor = Color.White
        LblStatus.ForeColor = Color.White
               LblKitName.ForeColor = Color.White
        LblParentItemCode.ForeColor = Color.White
        lblLastNodeCode.ForeColor = Color.White
        Label11.ForeColor = Color.White
        LblKitCode.ForeColor = Color.White
            LblItemType.ForeColor = Color.White
        LblSalesPrice.ForeColor = Color.White
        GrpKitDetail.ForeColor = Color.White
        ' GroupBox3.ForeColor = Color.White
        GroupBox4.ForeColor = Color.White
        LblStatusActiveOrInActive.ForeColor = Color.White
        Label14.ForeColor = Color.White

    End Function
    Private Sub txtFilterArticle_TextChanged(sender As Object, e As EventArgs) Handles txtFilterArticle.TextChanged
        If Not String.IsNullOrEmpty(txtFilterArticle.Text) AndAlso txtFilterArticle.IsItemSelected Then
            txtFilterArticle.IsItemSelected = False
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtFilterArticle_Leave(sender, eKeyDown)
        End If
    End Sub
    Private Sub txtFilterArticle_Leave(sender As System.Object, e As System.EventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            txtFilterArticle.Text = txtFilterArticle.Text.ToString().Split(" ")(0)

            DtArticleCOmbo = Objcm.GetKiTDefinedDetail(txtFilterArticle.Text)
            If DtArticleCOmbo.Rows.Count > 0 Then
                GrpShowData.Visible = True
                GrpKitDetail.Visible = True
                '  GroupBox3.Visible = True
                GroupBox4.Visible = False
                TableLayoutPanel8.Visible = False
                txtSingleItemFliter.ReadOnly = True
                ' LblStatusActiveOrInActive.Text = "Active"
                ButtonAndPanelVisibility(False)
                BindShowDataGrid()
                GridSizeChangeRequired(True)
                dtGuest.Clear()
                IsArticleMapForKit = True
            Else
                GrpShowData.Visible = True
                GrpKitDetail.Visible = True


                TableLayoutPanel8.Visible = True
                '  GroupBox3.Visible = True
                GroupBox4.Visible = True
                GrdShowData.Enabled = True
                txtSingleItemFliter.ReadOnly = False
        
                ' LblStatusActiveOrInActive.Text = "In-active"
                ButtonAndPanelVisibility(True)
                BindShowDataGrid()
                GridSizeChangeRequired(False)
                dtGuest.Clear()
                IsArticleMapForKit = False
            End If
            If txtFilterArticle.Text.Length >= 1 Then
                Call SetSelectedArticle(txtFilterArticle.Text)
                txtFilterArticle.Focus()
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
 
  
   

  
    Private Function DeleteGuestDetails(srNo As Integer) As Boolean '...
        Try
            If dtGuest.Rows.Count > 0 Then
                Dim drDtl() = dtGuest.Select("SrNo=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        dtGuest.Rows.Remove(row)
                    Next
                End If
                dtGuest.AcceptChanges()
                Dim count = 1
                For index = 0 To dtGuest.Rows.Count - 1
                    dtGuest.Rows(index)("SrNo") = count
                    count += 1
                Next
                srNo = count
            End If
            If dtGuest.Rows.Count = 0 Then

                DtUpGradableItem.Clear()


            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
  

#End Region
#Region "Function"
    Private Sub SetSelectedArticle(ByVal article As String)
        Try
            Dim DtArtcleData As DataTable = Objcm.GetComboDtl(clsAdmin.SiteCode, article)
            If DtArtcleData.Rows.Count > 0 Then

                TxtLastNodeCode.Text = DtArtcleData.Rows(0)("lastnodecode").ToString.Trim()
                TxtParentItemCode.Text = DtArtcleData.Rows(0)("ParentArt").ToString.Trim()
                TxtItemTree.Text = DtArtcleData.Rows(0)("Treeid").ToString.Trim()

                txtItemCode.Text = DtArtcleData.Rows(0)("ArticleCOde").ToString.Trim()
                TxtKitShortName.Text = DtArtcleData.Rows(0)("ArticleShortNAme").ToString.Trim()

                txtItemType.Text = DtArtcleData.Rows(0)("ArticalTypeCode").ToString.Trim()
                TxtmaterialType.Text = DtArtcleData.Rows(0)("MaterialTypeCode").ToString.Trim()
                TxtSalesPrice.Text = IIf(DtArtcleData.Rows(0)("SellingPrice").ToString.Trim() = "", "0", DtArtcleData.Rows(0)("SellingPrice").ToString.Trim())
                txtFilterArticle.Text = DtArtcleData.Rows(0)("ArticleShortNAme").ToString.Trim()
                LblStatusActiveOrInActive.Text = DtArtcleData.Rows(0)("ArticleActive").ToString.Trim()

                If IsArticleMapForKit = True Then
                    If LblStatusActiveOrInActive.Text.ToUpper = "ACTIVE" Then
                        BtnSave.Text = "De-activate"
                    Else
                        BtnSave.Text = "Activate"
                    End If
                Else
                    BtnSave.Text = "Save"
                End If
            Else

                BlankItemDetailSection()
            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BindAddArticle(ByVal article As String)
        Try
            dtCustmData = Objcm.GetComboDtl(clsAdmin.SiteCode, article)
            If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                    If dtCustmData.Rows.Count > 0 Then
                        For index = 0 To dtGuest.Rows.Count - 1
                            Dim dtRow As Int32 = -1
                            Dim result As DataRow() = dtGuest.Select("ArticleCode='" + article.Trim + "' ")
                            If result.Length > 0 Then
                                ShowMessage("Record Already exist", "Information")
                                Exit Sub
                            End If
                        Next
                    End If
                    Dim rowGuest As DataRow

                    rowGuest = dtGuest.NewRow()
                    rowGuest("SrNo") = dtGuest.Rows.Count + 1
                    rowGuest("Ean") = dtCustmData.Rows(0)("EAN")
                    rowGuest("ArticleCode") = dtCustmData.Rows(0)("ArticleCode")
                    rowGuest("ArticleShortName") = dtCustmData.Rows(0)("ArticleShortName")
                    rowGuest("SellingPrice") = dtCustmData.Rows(0)("sellingprice")
                    rowGuest("SaleUnitofMeasure") = dtCustmData.Rows(0)("BaseUnitofMeasure")
                    rowGuest("ArticleShortName") = dtCustmData.Rows(0)("ArticleShortName")
                    rowGuest("Quantity") = "0"
                    dtGuest.Rows.Add(rowGuest)


                Else
                    GrdShowData.DataSource = dtCustmData
                    dtArticle = dtCustmData
                    GrdShowData.Refresh()
                End If
                gridArticleDetailsSetting()



            ElseIf Not dtCustmData Is Nothing And dtCustmData.Rows.Count = 0 Then
                ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))

                txtFilterArticle.Focus()
                Exit Sub
            Else
                GrdShowData.DataSource = dtCustmData
                dtArticle = dtCustmData
                GrdShowData.Refresh()
            End If
         

            GrdShowData.Visible = True
            GrpShowData.Visible = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
   
    Private Sub BlankItemDetailSection()
        Try
            TxtLastNodeCode.Text = ""
            TxtParentItemCode.Text = ""
            TxtItemTree.Text = ""

           
            txtItemCode.Text = ""

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub BlankStepData()
        Try
            txtItemCode.Text = ""
            TxtSalesPrice.Text = ""
            dtGuest.Clear()
            DtUpGradableItem.Clear()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub gridArticleDetailsSetting()
        Try
            GrdShowData.DataSource = dtGuest ' dtArticle
            ''grdArticleSearch.Cols("Del").Caption = ""
            ''grdArticleSearch.Cols("Del").Width = 20
            ''grdArticleSearch.Cols("Del").ComboList = "..."
            ''grdArticleSearch.Cols("Del").Visible = True
            'grdAdditem.Cols("SrNo").Visible = True
            'grdAdditem.Cols("ArticleCode").Width = 100
            'grdAdditem.Cols("ArticleCode").DataType = Type.GetType("System.String")
            'grdAdditem.Cols("ArticleCode").AllowEditing = False
            'grdAdditem.Cols("ArticleCode").Name = "ArticleCode"
            'grdAdditem.Cols("ArticleCode").Caption = "Article Code"
            'grdAdditem.Cols("ArticleCode").TextAlign = TextAlignEnum.LeftCenter

            'grdAdditem.Cols("ArticleName").Caption = "Article Name"
            'grdAdditem.Cols("ArticleName").Width = 300
            'grdAdditem.Cols("ArticleName").AllowEditing = False
            'grdAdditem.Cols("ArticleName").DataType = Type.GetType("System.String")
            'grdAdditem.Cols("ArticleName").Name = "ArticleName"
            'grdAdditem.Cols("ArticleName").TextAlign = TextAlignEnum.LeftCenter

            'grdAdditem.Cols("LastNodeCode").Width = 150
            'grdAdditem.Cols("LastNodeCode").Caption = "Last Node"
            'grdAdditem.Cols("LastNodeCode").AllowEditing = False
            'grdAdditem.Cols("LastNodeCode").DataType = Type.GetType("System.String")


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BindShowDataGrid()
        Try
            GrdShowData.DataSource = DtArticleCOmbo
          
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region

  
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Try

            If IsArticleMapForKit = False Then
                Dim eventType As Int32
                If GrdShowData.Rows.Count > 1 Then
                    ShowMessage("you will loose all data. Are you sure you wish to proceed?", "Information", eventType, "No", "Yes")
                    If eventType = 1 Then
                        Me.Close()
                    Else
                        Exit Sub
                    End If
                Else
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSaveCombo_Click(sender As Object, e As EventArgs) Handles BtnSave.Click



        Try
            If IsArticleMapForKit Then
                Dim Active As Boolean = True
                If LblStatusActiveOrInActive.Text.ToUpper = "ACTIVE" Then
                    Active = False
                End If

                If Objcm.ActivateOrDeActivateKit(txtItemCode.Text.Trim, clsAdmin.SiteCode, clsAdmin.UserCode, Active) Then

                    If Active = True Then
                        ShowMessage(txtItemCode.Text & "-" & TxtKitShortName.Text & " Kit Activated Successfully", getValueByKey("CLAE04"))

                    Else
                        ShowMessage(txtItemCode.Text & "-" & TxtKitShortName.Text & " Kit De-Activated Successfully", getValueByKey("CLAE04"))

                    End If

                    hideAllPanelAndButton()
                    BlankItemDetailSection()
                    BlankStepData()
                    DtArticleCOmbo.Clear()

                    BindShowDataGrid()
                    txtFilterArticle.Text = ""
                    TxtLeave()

                    Exit Sub

                Else

                    If Active = True Then
                        ShowMessage("Error in Kit Activated", getValueByKey("CLAE04"))

                    Else
                        ShowMessage("Error in Kit De-Activated", getValueByKey("CLAE04"))

                    End If
                    TxtLeave()
                    Exit Sub

                End If
            End If

            If GrdShowData.Rows.Count < 2 Then
                Exit Sub
            End If
            If IsSellingPriceMatch() Then
            Else
                ShowMessage("Kit Parent Item Sales Price must be equal to sum of its Child Items Sales Price", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If IsZeroQtyArticlePresentInGrid() Then
                ShowMessage("Quantity value must be greater than or equal to 0.001", getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim eventType As Int32
            ShowMessage("Kit once created cannot be edited. Are you sure you wish to proceed?", "Information", eventType, "No", "Yes")
            If eventType = 1 Then

            Else
                Exit Sub
            End If



            Dim DsKit As New DataSet
            Dim DtDefineKitForSave As New DataTable
            DtDefineKitForSave = Objcm.GetMstArticleKitStruct
            For Each Drrow1 In dtGuest.Rows

                Dim drsave = DtDefineKitForSave.NewRow()
                drsave("Ean") = Drrow1("Ean")
                drsave("ArticleCode") = Drrow1("ArticleCode")
                drsave("KitArticleCode") = txtItemCode.Text.Trim
                drsave("SellingPrice") = Drrow1("SellingPrice")
                drsave("SaleUnitofMeasure") = Drrow1("SaleUnitofMeasure")
                drsave("Quantity") = Drrow1("Quantity")
                drsave("CREATEDAT") = clsAdmin.SiteCode
                drsave("CREATEDBY") = clsAdmin.UserCode
                drsave("CREATEDON") = DateTime.Now
                drsave("UPDATEDAT") = clsAdmin.SiteCode
                drsave("UPDATEDBY") = clsAdmin.UserCode
                drsave("UPDATEDON") = DateTime.Now
                drsave("STATUS") = 1
                DtDefineKitForSave.Rows.Add(drsave)

            Next

            DtDefineKitForSave.TableName = "MstArticleKit"
            DsKit.Tables.Add(DtDefineKitForSave)


            If objItem.SaveMstArticleKit(DsKit, txtItemCode.Text.Trim, clsAdmin.SiteCode, clsAdmin.UserCode) Then
                ShowMessage(txtItemCode.Text & "-" & TxtKitShortName.Text & " Kit Defined Succesfully", getValueByKey("CLAE04"))
                hideAllPanelAndButton()
            Else
                ShowMessage("Error in Define Kit ", getValueByKey("CLAE04"))
            End If

            BlankItemDetailSection()
            BlankStepData()
            DtArticleCOmbo.Clear()
            DtDefineKitForSave.Clear()
            BindShowDataGrid()
            txtFilterArticle.Text = ""
            TxtLeave()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    
    Private Sub GrdShowData_CellButtonClick(sender As Object, e As RowColEventArgs) Handles GrdShowData.CellButtonClick
        Try
            Dim SrNo = GrdShowData.Item(GrdShowData.Row, "SrNo")

            DeleteGuestDetails(SrNo)
   
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ReadOnlyField()
        TxtLastNodeCode.ReadOnly = True
        TxtParentItemCode.ReadOnly = True
        TxtItemTree.ReadOnly = True
        txtItemCode.ReadOnly = True
        TxtKitShortName.ReadOnly = True
        txtItemType.ReadOnly = True
        TxtmaterialType.ReadOnly = True
        TxtSalesPrice.ReadOnly = True
        txtSingleItemFliter.ReadOnly = True      
    End Sub

    Private Sub txtSingleItemFliter_TextChanged(sender As Object, e As EventArgs) Handles txtSingleItemFliter.TextChanged
        If Not String.IsNullOrEmpty(txtSingleItemFliter.Text) AndAlso txtSingleItemFliter.IsItemSelected Then
            txtSingleItemFliter.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtSingleItemFliter_Leave(sender, eKeyDown)
        End If
    End Sub

    Private Sub txtSingleItemFliter_Leave(sender As Object, e As EventArgs) ' Handles txtSingleItemFliter.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            txtSingleItemFliter.Text = txtSingleItemFliter.Text.ToString().Split(" ")(0)
            If txtSingleItemFliter.Text.Length >= 1 Then
                Dim membershipmaparticle = txtSingleItemFliter.Text
                Dim objItemSch As New clsIteamSearch
                ' If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
                Call BindAddArticle(txtSingleItemFliter.Text)
                GrdShowData.Visible = True
                txtSingleItemFliter.Focus()

            End If
            txtSingleItemFliter.Text = ""
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    Private Function IsSellingPriceMatch() As Boolean

        Dim IsSumMatch As Boolean = False
        Dim SumOfSellingPrice As Double
        For index As Integer = 1 To GrdShowData.Rows.Count - 1
            SumOfSellingPrice += Convert.ToDecimal(GrdShowData.Rows(index)("SellingPrice"))
        Next

        If SumOfSellingPrice = Convert.ToDecimal(TxtSalesPrice.Text) Then
            IsSumMatch = True

        Else
            IsSumMatch = False
        End If
        Return IsSumMatch
    End Function


    Private Function IsZeroQtyArticlePresentInGrid() As Boolean

        Dim IsZeroQtyArticlePresent As Boolean = False

        For index As Integer = 1 To GrdShowData.Rows.Count - 1
            If GrdShowData.Rows(index)("Quantity") <= 0 Then
                IsZeroQtyArticlePresent = True
                Return IsZeroQtyArticlePresent
            End If
        Next

        IsZeroQtyArticlePresent = False

     
        Return IsZeroQtyArticlePresent
    End Function

    Private Sub hideAllPanelAndButton()
        ' GroupBox3.Visible = False
        GroupBox4.Visible = False
        GrpKitDetail.Visible = False
        BtnSave.Visible = False
        BtnCancel.Visible = True
        GrpShowData.Visible = False
    End Sub

    Private Sub FrmDefineKit_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If IsArticleMapForKit = False Then
                If GrdShowData.Rows.Count > 1 Then
                    Dim eventType As Int32
                    ShowMessage("you will loose all data. Are you sure you wish to proceed?", "Information", eventType, "No", "Yes")
                    If eventType = 1 Then
                        Me.Close()
                    Else
                        Exit Sub
                    End If
                Else
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End If

    End Sub

    Private Sub GrdShowData_AfterEdit(sender As Object, e As RowColEventArgs) Handles GrdShowData.AfterEdit

        If String.IsNullOrEmpty(GrdShowData.Rows(e.Row)("Quantity").ToString) Then

            GrdShowData.Rows(e.Row)("Quantity") = 0
        End If

        If String.IsNullOrEmpty(GrdShowData.Rows(e.Row)("SellingPrice").ToString) Then

            GrdShowData.Rows(e.Row)("SellingPrice") = 0
        End If

        ' If Not GrdShowData.Rows(e.Row)("Quantity") Is Nothing And GrdShowData.Rows(e.Row)("Quantity") <= 0 Then
        'ShowMessage("Quantity value must be greater than or equal to 0.001", getValueByKey("CLAE04"))
        'GrdShowData.Rows(e.Row)("Quantity") = 0
        ' End If

    End Sub

    Private Sub ButtonAndPanelVisibility(ByVal value As Boolean)
        BtnSave.Visible = True
        BtnCancel.Visible = True

        GrdShowData.Cols("SellingPrice").AllowEditing = value
        GrdShowData.Cols("Quantity").AllowEditing = value

    End Sub
    Private Sub GridSizeChangeRequired(ByVal IsGridSizeLargeRequired As Boolean)
        If IsGridSizeLargeRequired Then
           
            GrpShowData.Size = New Size(870, 256)
            GrdShowData.Size = New Size(858, 226)

        Else
           
            GrpShowData.Size = New Size(870, 186)
            GrdShowData.Size = New Size(858, 156)
            txtSingleItemFliter.Focus()
        End If

    End Sub
    Private Sub TxtLeave()
        If txtFilterArticle.Text = "" And TxtLastNodeCode.Text = "" Then
            txtFilterArticle.Text = PlaceHolderString
        End If

    End Sub
    Private Sub TxtClick()
        If txtFilterArticle.Text <> "" And TxtLastNodeCode.Text = "" Then
            txtFilterArticle.Text = ""

        End If
    End Sub
End Class