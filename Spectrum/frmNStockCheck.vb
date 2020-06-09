Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL

Public Class frmNStockCheck
    Dim dt As Array
    Private Sub frmNStockCheck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetCulture(Me, Me.Name)
            txtItemCode.Select()
            AddHandler txtItemCode.TextChanged, AddressOf txtItemCode_textchange
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            Dim condition As String
            Dim objItem As New clsIteamSearch
            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
            Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition)
            If dtBind.Rows.Count > 1 Then
                Dim listSource As List(Of [String]) = (From row In dtBind Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("Discription"))).Distinct().ToList()
                txtItemCode.lstNames = listSource
            End If
            '------ Apply Theme here 
            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        ThemeChange()
                    Case 2

                    Case Else

                End Select
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Sub ThemeChange()
        Try
            C1Sizer1.SplitterWidth = 2
            C1Sizer1.BackColor = Color.FromArgb(134, 134, 134)
            Panel1.BackColor = Color.White
            With btnSearchBirthListID
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
                .Image = Global.Spectrum.My.Resources.search_2
            End With

            With btnOK
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .TextAlign = ContentAlignment.MiddleCenter
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
                .Size = New Point(.Width, .Height + 10)
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
                .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
            End With
            With BtnGetStock
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
            End With

            With dgItemSearch
                .Styles(0).BackColor = Color.FromArgb(255, 255, 255)
                .Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Regular)
                .Styles(1).Font = New Font("Neo Sans", 10, FontStyle.Bold)
                .Styles(1).BackColor = Color.FromArgb(177, 227, 253)
                .Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
                .Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
                .HighLightRowStyle.BackColor = Color.LightBlue
                .HighLightRowStyle.ForeColor = Color.WhiteSmoke
            End With


            Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)


            With lblOR
                .Font = lblFont
                '  .Dock = DockStyle.Top
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                '  .Size = New Size(txtFirstName.Left - .Left, txtFirstName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label1
                .Font = lblFont
                'Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label3
                .Font = lblFont
                '   .Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label4
                .Font = lblFont
                '  .Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label5
                .Font = lblFont
                '  .Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblEAN
                .Font = lblFont
                ' .Margin = New Padding(0)
                .Size = New Size(txtEAN.Left - .Left, txtEAN.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblAvailableQty
                .Font = lblFont
                .BackColor = Color.FromArgb(255, 255, 255)
                ' .Margin = New Padding(0)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblPhysicalQty
                ' .Margin = New Padding(0)
                .Font = lblFont
                .BackColor = Color.FromArgb(255, 255, 255)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCalArticleDescri
                '   .Margin = New Padding(0)
                .Font = lblFont
                .BackColor = Color.FromArgb(255, 255, 255)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCalCWSStatus
                ' .Margin = New Padding(0)
                .Font = lblFont
                ' .Dock = DockStyle.Top
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .BackColor = Color.FromArgb(255, 255, 255)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblNextAvailabelDate
                .Font = lblFont
                '  .Margin = New Padding(0)
                ' .Dock = DockStyle.Top
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .BackColor = Color.FromArgb(255, 255, 255)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label6
                .Font = lblFont
                '   .Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With Label2
                .Font = lblFont
                '   .Margin = New Padding(0)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With



        Catch ex As Exception

        End Try
    End Sub


    Private Function SearchArticleStock(ByVal strArticleCode As String, ByVal strEan As String, ByVal strSiteCode As String) As Boolean
        Try
            lblCalCWSStatus.Text = "N/A"
            lblNextAvailabelDate.Text = "N/A"

            'clsDefaultConfiguration.WebserviceStockURL = "http://10.10.180.68:8080/posSeam/webservices/ArticleStockBalancesSynchronizer?wsdl"

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.WebserviceStockURL)

            'Create a client that is configured with this address and binding.
            Dim seriveStock As New ArticleStockServices.ArticleStockBalancesSynchronizerClient(binding, address)

            Dim webserviceRequestDTO As New ArticleStockServices.articleStockBalancesRequestDTO
            Dim webserviceRequestHeader As New ArticleStockServices.soapWsHeader
            Dim webserviceRequestBase As New ArticleStockServices.retrieveArticleStockBalancesRecords
            webserviceRequestDTO.articleCode = strArticleCode.Trim().ToString()
            webserviceRequestDTO.ean = strEan.Trim().ToString()
            webserviceRequestDTO.siteCode = strSiteCode
            webserviceRequestDTO.pushOrPull = False
            webserviceRequestHeader.userName = "webservice"
            webserviceRequestHeader.password = "admin"

            webserviceRequestDTO.soapWsHeader = webserviceRequestHeader
            webserviceRequestBase.arg0 = webserviceRequestDTO
            Dim response As ArticleStockServices.retrieveArticleStockBalancesRecordsResponse
            response = seriveStock.retrieveArticleStockBalancesRecords(webserviceRequestBase)
            Dim dtos As ArticleStockServices.articleStockBalancesDtos = response.return
            If (dtos.articleStockBalancesDTO Is Nothing) Then
                ShowMessage(getValueByKey("STCHK01"), "STCHK01 - " & getValueByKey("CLAE04"))
                dgItemSearch.DataSource = Nothing
                dgItemSearch.SetDataBinding(Nothing, "", True)
                Return True
            End If
            Dim dt As Array = dtos.articleStockBalancesDTO.Clone()
            dgItemSearch.DataSource = Nothing
            dgItemSearch.SetDataBinding(dt, "", True)
            lblNextAvailabelDate.DataBindings.Clear()
            lblCalCWSStatus.DataBindings.Clear()
            lblNextAvailabelDate.DataBindings.Add("Text", dt, "nextAvailableDate", True, DataSourceUpdateMode.OnPropertyChanged, "N/A")

            lblCalCWSStatus.DataBindings.Add("Text", dt, "stockStatus", True, DataSourceUpdateMode.OnPropertyChanged, "N/A")
            GetNextExpectedDateAtCentralWare()

            Return True
        Catch ex As Exception
            LogException(ex)
            GetNextExpectedDateAtCentralWare()
            Return False
        End Try
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Function ValidateInputs() As Boolean
        Try
            If (txtEAN.Text = String.Empty And txtItemCode.Text = String.Empty) Then
                ShowMessage(getValueByKey("STCHK02"), "STCHK02 - " & getValueByKey("CLAE04"))
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub txtItemCode_textchange(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(txtItemCode.Text) AndAlso txtItemCode.IsItemSelected Then
            txtItemCode.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.PreviewKeyDownEventArgs(Keys.Enter)
            Call txtItemCode_PreviewKeyDown(sender, eKeyDown)
        End If
    End Sub
    Private Sub txtItemCode_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtItemCode.PreviewKeyDown, txtEAN.PreviewKeyDown
        Try

            If (e.KeyCode = Keys.Enter) Then
                txtItemCode.Text = txtItemCode.Text.ToString().Split(" ")(0)
                'Added by Rohit to correct the stock search function when article code and ean fields have values for different articles
                If CType(sender, TextBox).Name = "txtItemCode" Then
                    txtEAN.Text = String.Empty
                ElseIf CType(sender, TextBox).Name = "txtEAN" Then
                    txtItemCode.Text = String.Empty
                End If
                'Change end

                SearchStock()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function SearchStock() As Boolean
        Try
            If (ValidateInputs()) Then
                Dim strArticleCode As String = ""
                Dim strEAN As String = ""
                Dim strErrorMsg As String = ""
                If getArticleInfo(strArticleCode, strEAN, strErrorMsg) Then
                    If Not (SearchArticleStock(strArticleCode, strEAN, clsAdmin.SiteCode)) Then
                        ShowMessage(getValueByKey("STCHK03"), "STCHK03 - " & getValueByKey("CLAE04"))
                    End If
                Else
                    dgItemSearch.DataSource = Nothing
                    dgItemSearch.SetDataBinding(Nothing, "", True)
                    ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function getArticleInfo(ByRef strArticle As String, ByRef strEAN As String, Optional ByRef strErrorMsg As String = "") As Boolean
        Try
            Dim objclsIteamSearch As New SpectrumBL.clsIteamSearch
            Dim drArticleInfo() As DataRow = Nothing
            Dim dtArticle As New DataTable
            Dim barcode As String = txtEAN.Text.Trim
            If txtEAN.Text <> String.Empty Then
                If clsDefaultConfiguration.IsBatchManagementReq Then

                    txtEAN.Text = objclsIteamSearch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, txtEAN.Text.Trim).Trim
                End If
                dtArticle = objclsIteamSearch.GetEANData(clsAdmin.SiteCode, txtEAN.Text, "", "", dtItemScanData)
            ElseIf txtItemCode.Text <> String.Empty Then
                dtArticle = objclsIteamSearch.GetEANData(clsAdmin.SiteCode, txtItemCode.Text, "", "", dtItemScanData)
            End If

            If Not dtArticle Is Nothing AndAlso dtArticle.Rows.Count > 0 Then
                If clsDefaultConfiguration.IsBatchManagementReq And txtEAN.Text <> String.Empty Then
                    If txtEAN.Text <> String.Empty Or txtItemCode.Text <> String.Empty Then
                        drArticleInfo = dtArticle.Select("ArticleCode='" + txtEAN.Text + "' ")
                    End If
                Else
                    If txtEAN.Text <> String.Empty Or txtItemCode.Text <> String.Empty Then
                        drArticleInfo = dtArticle.Select("EAN='" + txtEAN.Text + "' or ArticleCode='" + txtItemCode.Text + "' ")
                    End If
                End If


                If Not drArticleInfo Is Nothing Then
                    strArticle = drArticleInfo(0)("ArticleCode")
                    strEAN = drArticleInfo(0)("EAN")
                    txtEAN.Text = strEAN
                    txtItemCode.Text = strArticle
                    lblCalArticleDescri.Text = drArticleInfo(0)("Discription")
                    lblAvailableQty.Text = drArticleInfo(0)("AvailableQty")
                    lblPhysicalQty.Text = drArticleInfo(0)("PhysicalQty")
                    If clsDefaultConfiguration.IsBatchManagementReq Then txtEAN.Text = barcode
                Else
                    lblCalArticleDescri.Text = ""
                    lblAvailableQty.Text = ""
                    lblPhysicalQty.Text = ""
                    'strErrorMsg = "Article not found."
                    strErrorMsg = getValueByKey("EM002")
                    Return False
                End If
            Else
                'strErrorMsg = "Article not found."
                strErrorMsg = getValueByKey("EM002")
                Return False
            End If


            Return True
        Catch ex As Exception
            LogException(ex)
            'strErrorMsg = "Error in finding Article information."
            strErrorMsg = getValueByKey("EM007")
            Return False
        End Try
    End Function

    Private Sub btnSearchBirthListID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBirthListID.Click
        Try
            Dim objsearchitem As New frmNItemSearch
            objsearchitem.ShowDialog()
            Dim dritemInfo As DataRow = objsearchitem.ItemRow
            If Not dritemInfo Is Nothing Then

                txtItemCode.Text = dritemInfo("ArticleCode")
                txtEAN.Text = dritemInfo("EAN")
                lblCalArticleDescri.Text = dritemInfo("Discription")
                txtItemCode.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub BtnGetStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetStock.Click
        Try
            SearchStock()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub GetNextExpectedDateAtCentralWare()
        'Get the Next Available Expected Date At CentralWare from Local DB
        If ((lblCalCWSStatus.Text = "N/A" Or lblCalCWSStatus.Text = String.Empty) And (lblNextAvailabelDate.Text = "N/A" Or lblNextAvailabelDate.Text = String.Empty)) Then
            Try
                Dim dt As New DataTable
                Dim clsCommn As New SpectrumBL.clsCommon
                Dim strSql As String = ""
                strSql = "SELECT  STOCKSTATUS,NEXTAVAILABLEDATE FROM ARTICLESTOCKBALANCES A WHERE A.SITECODE='" & clsAdmin.SiteCode & "' AND ArticleCode = '" & txtItemCode.Text & "' OR  EAN = '" & txtEAN.Text & "'"
                dt = clsCommn.GetFilledTable(strSql)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblCalCWSStatus.Text = dt.Rows(0)("STOCKSTATUS").ToString()
                    lblNextAvailabelDate.Text = dt.Rows(0)("NEXTAVAILABLEDATE").ToString()
                End If
            Catch ex As Exception
                LogException(ex)
            End Try
        End If

        Try
            If Not lblNextAvailabelDate.Text = String.Empty Then
                Dim dtDate As DateTime
                dtDate = CDate(lblNextAvailabelDate.Text)
                lblNextAvailabelDate.Text = dtDate.ToString
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

End Class
