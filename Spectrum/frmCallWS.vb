﻿Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports System.Web
Imports System.Net.WebClient
Imports SpectrumBL
Imports Spectrum
Imports SpectrumCommon
Imports Spectrum.WebServices
Imports Spectrum.MyClasses
Imports System.Globalization
Imports System.Reflection
Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.IO.Compression.FileSystem
Imports ComponentAce.Compression.ZipForge
Imports Ionic.Zip
Imports Rebex.IO.Compression
Imports System.Net

Public Class frmCallWS
    Dim strArticleCode As String = ""
    Dim strEAN As String = ""
    Dim strSitecode As String = ""
    Dim objcls As clsDefaultConfiguration
    Dim objClscommon As New clsCommon
    Dim dtPrimaryKey As New DataTable
    Dim dsPrimaryKey As New DataSet
    Dim dtRecords As New DataTable
    Dim dtShow As New DataTable
    Dim FromDateForSync As Date
    Dim reqID As Integer = 0
    Dim waitPopupMsg As frmSpecialPrompt
    Dim strTableUpdated As String = ""
    Private Sub frmNStockCheck_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim FormattedDate As Date
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            btnGet_Click(sender, e)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)
        btnGet.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnGet.BackColor = Color.Transparent
        btnGet.BackColor = Color.FromArgb(0, 107, 163)
        btnGet.ForeColor = Color.FromArgb(255, 255, 255)
        btnGet.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnGet.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnGet.FlatStyle = FlatStyle.Flat
        btnGet.FlatAppearance.BorderSize = 0
        btnGet.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        btnGet.TextAlign = ContentAlignment.MiddleCenter
        btnGet.Size = New Size(142, 39)
    End Function
    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click

        Try
            btnGet.Enabled = False
            Dim responseUomSynchronizer, RespCSV, responseArticleSynchronizer, responseBillingSynchronizer, responseIndependentMaster, responseAuthenticationSynchronizer, responseArticleHierarchySynchronizer, responseCSVMstEan As Boolean
            Dim responseCSVArticlestock, responseCSVMstArticle, responseCSVPurchase, responseCSVSalesInfo As Boolean
            Dim strCSVArticlestock, strCSVMstArticle, strCSVPurchase, strCSVSalesInfo, strCSVMstEan As String
            Dim IpAddr, port, syncport As String
            Dim objs As New clsCommon
            Dim dtInfo As New DataTable
            dtInfo = objs.GetRemoteIpPort()
            If dtInfo.Rows.Count > 0 Then
                For Each row In dtInfo.Rows
                    If row("FldLabel").ToString() = "WebService.Remote.IP" Then
                        IpAddr = row("FldValue").ToString()
                    ElseIf row("FldLabel").ToString() = "WebService.Remote.PORT" Then
                        port = row("FldValue").ToString()
                    ElseIf row("FldLabel").ToString() = "SYNCH_SERVER_LOCAL_PORT" Then
                        syncport = row("FldValue").ToString()
                    End If
                Next
            End If
            If Not dtInfo Is Nothing Then
                writeDaycloseLog("Checking internet connectivity...")
                ' ShowMessage("Checking internet connectivity...", getValueByKey("CLAE04"), False)
                Try
                    Using client = New WebClient()
                        Using stream = client.OpenRead(clsDefaultConfiguration.UrlForCheckingInternetConnection)
                        End Using
                    End Using
                Catch ex As Exception
                    LogException(ex)
                    ShowMessage("Unable to update. Please check your internet connection and try again!", getValueByKey("CLAE04"), False)
                    Exit Sub
                End Try

                ' ShowMessage("Checking " + clsDefaultConfiguration.UrlForCheckingInternetConnection.ToString + " as internet connected success", getValueByKey("CLAE04"), False)
                If dtInfo.Rows.Count > 0 Then
                    writeDaycloseLog("Checking CCE server Connectivity...")
                    '  ShowMessage("Checking CCE server Connectivity...", getValueByKey("CLAE04"), False)
                    Dim client As New WebClient()
                    client.Headers("Content-type") = "application/json"
                    ' invoke the REST method
                    Dim data As Byte()
                    Try
                        data = client.DownloadData("http://" & IpAddr & ":" & port & "/posSeam/rest/internetlog/check")
                    Catch ex As Exception
                        LogException(ex)
                        ShowMessage("Unable to connect to server. Please contact support", getValueByKey("CLAE04"), False)
                        Exit Sub
                    End Try

                    Dim vOut As String = System.Text.Encoding.UTF8.GetString(data)
                    If vOut = "SUCCESS" Then
                        ' ShowMessage("Checking CCE server as connected Success", getValueByKey("CLAE04"), False)
                    Else
                        writeDaycloseLog("Checking CCE server as connected Failed ")
                        ShowMessage("Unable to connect to server. Please contact support", getValueByKey("CLAE04"), False)
                    End If
                Else
                    writeDaycloseLog("CCE Details are not  configured properly ")
                    ShowMessage("Unable to connect to server. Please contact support", getValueByKey("CLAE04"), False)
                    Exit Sub
                End If

            End If

            ' ShowMessage("Started getting Item Updates", getValueByKey("CLAE04"), False)
            writeDaycloseLog("Started getting Item Updates")

            strSitecode = clsAdmin.SiteCode

            'IndependentMastersSynchronizer
            writeDaycloseLog("Getting Updates from IndependentMastersSynchronizer ")
            responseIndependentMaster = IndependentMastersSynchronizer(strSitecode)
            If responseIndependentMaster = True Then
                writeDaycloseLog("Data Updated successfully for IndependentMaster")
            Else
                writeDaycloseLog("Data Not Updated for IndependentMaster")
            End If



            'CsvFileSyncronizer
            '  Dim JsonDirectoryPath As String = Application.StartupPath & "\Sync\ZipCSVFiles_" + Now.ToString("ddMMyyyyhhmmss") + ""
            Dim JsonDirectoryPath As String = Application.StartupPath & "\ZipCSVFiles_" + Now.ToString("ddMMyyyyhhmmss") + ""
            writeDaycloseLog("Calling CsvFileSyncronizer for MstEan")
            strCSVMstEan = CsvFileSyncronizer(strSitecode, "MstEan", JsonDirectoryPath)
            writeDaycloseLog("Calling CsvFileSyncronizer for MstArticle")
            strCSVMstArticle = CsvFileSyncronizer(strSitecode, "MstArticle", JsonDirectoryPath)
            writeDaycloseLog("Calling CsvFileSyncronizer for PurchaseInfoRecord")
            strCSVPurchase = CsvFileSyncronizer(strSitecode, "PurchaseInfoRecord", JsonDirectoryPath)
            writeDaycloseLog("Calling CsvFileSyncronizer for ArticleStockBalances")
            strCSVArticlestock = CsvFileSyncronizer(strSitecode, "ArticleStockBalances", JsonDirectoryPath)
            writeDaycloseLog("Calling CsvFileSyncronizer for SalesInfoRecord")
            strCSVSalesInfo = CsvFileSyncronizer(strSitecode, "SalesInfoRecord", JsonDirectoryPath)


            'UomSynchronizer
            writeDaycloseLog("Getting Updates from UomSynchronizer ")
            responseUomSynchronizer = UomSynchronizer(strSitecode)
            If responseUomSynchronizer = True Then
                writeDaycloseLog("Data Updated successfully for ArticleUOM")
            Else
                writeDaycloseLog("Data Not Updated for ArticleUOM")
            End If


            Try
                Dim errormsg1 As String = ""
                DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
                Dim tran1 As SqlTransaction = Nothing
                Dim con1 As New SqlConnection(DataBaseConnection._OnlineConn)
                con1.Open()
                tran1 = con1.BeginTransaction()

                'CsvFileSyncronizer
                If strCSVMstEan <> "" Then
                    responseCSVMstEan = SaveCSVFiles(JsonDirectoryPath, "MstEan", strCSVMstEan, tran1, con1, errormsg1)
                    If errormsg1 <> "" Then
                        tran1.Rollback()
                        con1.Close()
                        ShowMessage(errormsg1, "Information", False)
                        Me.Close()
                    End If
                Else
                    writeDaycloseLog("No Response from CsvFileSyncronizer for MstEan")
                End If
                If strCSVMstArticle <> "" Then
                    responseCSVMstArticle = SaveCSVFiles(JsonDirectoryPath, "MstArticle", strCSVMstArticle, tran1, con1, errormsg1)
                    If errormsg1 <> "" Then
                        tran1.Rollback()
                        con1.Close()
                        ShowMessage(errormsg1, "Information", False)
                        Me.Close()
                    End If
                Else
                    writeDaycloseLog("No Response from CsvFileSyncronizer for MstArticle")
                End If
                If strCSVPurchase <> "" Then
                    responseCSVPurchase = SaveCSVFiles(JsonDirectoryPath, "PurchaseInfoRecord", strCSVPurchase, tran1, con1, errormsg1)
                    If errormsg1 <> "" Then
                        tran1.Rollback()
                        con1.Close()
                        ShowMessage(errormsg1, "Information", False)
                        Me.Close()
                    End If
                Else
                    writeDaycloseLog("No Response from CsvFileSyncronizer for PurchaseInfoRecord")
                End If
                If strCSVArticlestock <> "" Then
                    responseCSVArticlestock = SaveCSVFiles(JsonDirectoryPath, "ArticleStockBalances", strCSVArticlestock, tran1, con1, errormsg1)
                    If errormsg1 <> "" Then
                        tran1.Rollback()
                        con1.Close()
                        ShowMessage(errormsg1, "Information", False)
                        Me.Close()
                    End If
                Else
                    writeDaycloseLog("No Response from CsvFileSyncronizer for ArticleStockBalances")
                End If
                If strCSVSalesInfo <> "" Then
                    responseCSVSalesInfo = SaveCSVFiles(JsonDirectoryPath, "SalesInfoRecord", strCSVSalesInfo, tran1, con1, errormsg1)
                    If errormsg1 <> "" Then
                        tran1.Rollback()
                        con1.Close()
                        ShowMessage(errormsg1, "Information", False)
                        Me.Close()
                    End If
                Else
                    writeDaycloseLog("No Response from CsvFileSyncronizer for SalesInfoRecord")
                End If
                If errormsg1 = "" Then
                    tran1.Commit()
                    con1.Close()
                End If
            Catch ex As Exception
                LogException(ex)
                ShowMessage(ex.Message, "", False)
                Me.Close()
            End Try

            ''CsvFileSyncronizer
            'If strCSVMstEan <> "" Then
            '    responseCSVMstEan = SaveCSVFiles(JsonDirectoryPath, "MstEan", strCSVMstEan)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for MstEan")
            'End If
            'If strCSVMstArticle <> "" Then
            '    responseCSVMstArticle = SaveCSVFiles(JsonDirectoryPath, "MstArticle", strCSVMstArticle)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for MstArticle")
            'End If
            'If strCSVPurchase <> "" Then
            '    responseCSVPurchase = SaveCSVFiles(JsonDirectoryPath, "PurchaseInfoRecord", strCSVPurchase)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for PurchaseInfoRecord")
            'End If
            'If strCSVArticlestock <> "" Then
            '    responseCSVArticlestock = SaveCSVFiles(JsonDirectoryPath, "ArticleStockBalances", strCSVArticlestock)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for ArticleStockBalances")
            'End If
            'If strCSVSalesInfo <> "" Then
            '    responseCSVSalesInfo = SaveCSVFiles(JsonDirectoryPath, "SalesInfoRecord", strCSVSalesInfo)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for SalesInfoRecord")
            'End If

            'ArticleSynchronizer
            writeDaycloseLog("Getting Updates from ArticleSynchronizer ")
            responseArticleSynchronizer = ArticleSynchronizer(strSitecode)
            If responseArticleSynchronizer = True Then
                writeDaycloseLog("Data Updated successfully for ArticleSynchronizer")
            Else
                writeDaycloseLog("Data Not Updated for ArticleSynchronizer")
            End If

            'ArticleHierarchySynchronizer
            writeDaycloseLog("Getting Updates from ArticleHierarchySynchronizer ")
            responseArticleHierarchySynchronizer = ArticleHierarchySynchronizer(strSitecode)
            If responseArticleHierarchySynchronizer = True Then
                writeDaycloseLog("Data Updated successfully for ArticleHierarchySynchronizer")
            Else
                writeDaycloseLog("Data Not Updated for ArticleHierarchySynchronizer")
            End If

            'Authentication
            writeDaycloseLog("Getting Updates from AuthenticationSynchronizer ")
            responseAuthenticationSynchronizer = AuthenticationSynchronizer(strSitecode)
            If responseAuthenticationSynchronizer = True Then
                writeDaycloseLog("Data Updated successfully for AuthenticationSynchronizer")
            Else
                writeDaycloseLog("Data Not Updated for AuthenticationSynchronizer")
            End If

            writeDaycloseLog("Getting Updates from BillingSynchronizer ")
            responseBillingSynchronizer = BillingSynchronizer(strSitecode)
            If responseBillingSynchronizer = True Then
                writeDaycloseLog("Data Updated successfully for BillingSynchronizer")
            Else
                writeDaycloseLog("Data Not Updated for BillingSynchronizer")
            End If

            ' The data has been updated successfully. Following updates were made new item (item name) created. Item price for (item name) updated.”

            If responseUomSynchronizer = True OrElse responseArticleSynchronizer = True OrElse responseBillingSynchronizer = True OrElse responseAuthenticationSynchronizer = True OrElse
             responseIndependentMaster = True OrElse responseArticleHierarchySynchronizer = True OrElse
            responseCSVMstArticle = True OrElse responseCSVSalesInfo = True OrElse responseCSVArticlestock = True Then
                writeDaycloseLog("The data has been updated successfully")
                strTableUpdated = strTableUpdated.TrimEnd(CChar(","))
                ' ShowMessage("The data has been updated successfully.Changes has been made to Following tables : " & strTableUpdated & "", getValueByKey("CLAE04"), False)

                ShowMessage("The data has been updated successfully.", getValueByKey("CLAE04"), False)
                If dtRecords.Rows.Count > 0 Then
                    Dim objShowItems As New FrmShowItems
                    objShowItems.dsItem = dtRecords
                    If objShowItems.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        objShowItems.Dispose()
                    End If
                End If

                Me.Close()
            Else
                writeDaycloseLog("The data has not been updated.")
                ShowMessage("The data has not been updated.Please Try again...", getValueByKey("CLAE04"), False)
                Me.Close()
            End If
            Application.Exit()
            Me.Close()

            'ShowMessage("Started getting Item Updates", getValueByKey("CLAE04"), False)
            'writeDaycloseLog("Started getting Item Updates")

            'strSitecode = clsAdmin.SiteCode
            ''UomSynchronizer
            'writeDaycloseLog("Getting Updates from UomSynchronizer ")
            'responseUomSynchronizer = UomSynchronizer(strSitecode)
            'If responseUomSynchronizer = True Then
            '    writeDaycloseLog("Data Updated successfully for ArticleUOM")
            'Else
            '    writeDaycloseLog("Data Not Updated for ArticleUOM")
            'End If

            ''ArticleHierarchySynchronizer
            'writeDaycloseLog("Getting Updates from ArticleHierarchySynchronizer ")
            'responseArticleHierarchySynchronizer = ArticleHierarchySynchronizer(strSitecode)
            'If responseArticleHierarchySynchronizer = True Then
            '    writeDaycloseLog("Data Updated successfully for ArticleHierarchySynchronizer")
            'Else
            '    writeDaycloseLog("Data Not Updated for ArticleHierarchySynchronizer")
            'End If
            'writeDaycloseLog("Getting Updates from ArticleSynchronizer ")
            'responseArticleSynchronizer = ArticleSynchronizer(strSitecode)
            'If responseArticleSynchronizer = True Then
            '    writeDaycloseLog("Data Updated successfully for ArticleSynchronizer")
            'Else
            '    writeDaycloseLog("Data Not Updated for ArticleSynchronizer")
            'End If
            'writeDaycloseLog("Getting Updates from BillingSynchronizer ")
            'responseBillingSynchronizer = BillingSynchronizer(strSitecode)
            'If responseBillingSynchronizer = True Then
            '    writeDaycloseLog("Data Updated successfully for BillingSynchronizer")
            'Else
            '    writeDaycloseLog("Data Not Updated for BillingSynchronizer")
            'End If

            'writeDaycloseLog("Getting Updates from AuthenticationSynchronizer ")
            'responseAuthenticationSynchronizer = AuthenticationSynchronizer(strSitecode)
            'If responseAuthenticationSynchronizer = True Then
            '    writeDaycloseLog("Data Updated successfully for AuthenticationSynchronizer")
            'Else
            '    writeDaycloseLog("Data Not Updated for AuthenticationSynchronizer")
            'End If

            'Dim JsonDirectoryPath As String = clsDefaultConfiguration.DayCloseReportPath & "\Sync\ZipCSVFiles_" + Now.ToString("ddMMyyyyhhmmss") + ""

            'writeDaycloseLog("Calling CsvFileSyncronizer for ArticleStockBalances")
            'strCSVArticlestock = CsvFileSyncronizer(strSitecode, "ArticleStockBalances", JsonDirectoryPath)
            'writeDaycloseLog("Calling CsvFileSyncronizer for MstArticle")
            'strCSVMstArticle = CsvFileSyncronizer(strSitecode, "MstArticle", JsonDirectoryPath)
            'writeDaycloseLog("Calling CsvFileSyncronizer for PurchaseInfoRecord")
            'strCSVPurchase = CsvFileSyncronizer(strSitecode, "PurchaseInfoRecord", JsonDirectoryPath)
            'writeDaycloseLog("Calling CsvFileSyncronizer for SalesInfoRecord")
            'strCSVSalesInfo = CsvFileSyncronizer(strSitecode, "SalesInfoRecord", JsonDirectoryPath)

            'writeDaycloseLog("Getting Updates from IndependentMastersSynchronizer ")
            'responseIndependentMaster = IndependentMastersSynchronizer(strSitecode)
            'If responseIndependentMaster = True Then
            '    writeDaycloseLog("Data Updated successfully for IndependentMaster")
            'Else
            '    writeDaycloseLog("Data Not Updated for IndependentMaster")
            'End If

            'If strCSVArticlestock <> "" Then
            '    responseCSVArticlestock = SaveCSVFiles(JsonDirectoryPath, "ArticleStockBalances", strCSVArticlestock)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for ArticleStockBalances")
            'End If
            'If strCSVMstArticle <> "" Then
            '    responseCSVMstArticle = SaveCSVFiles(JsonDirectoryPath, "MstArticle", strCSVMstArticle)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for MstArticle")
            'End If
            'If strCSVSalesInfo <> "" Then
            '    responseCSVSalesInfo = SaveCSVFiles(JsonDirectoryPath, "SalesInfoRecord", strCSVSalesInfo)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for SalesInfoRecord")
            'End If
            'If strCSVPurchase <> "" Then
            '    responseCSVPurchase = SaveCSVFiles(JsonDirectoryPath, "PurchaseInfoRecord", strCSVPurchase)
            'Else
            '    writeDaycloseLog("No Response from CsvFileSyncronizer for PurchaseInfoRecord")
            'End If



            'If responseUomSynchronizer = True And responseArticleSynchronizer = True And responseBillingSynchronizer = True And responseAuthenticationSynchronizer = True And
            ' responseIndependentMaster = True And responseArticleHierarchySynchronizer = True And
            'responseCSVMstArticle = True And responseCSVSalesInfo = True And responseCSVArticlestock = True Then
            '    writeDaycloseLog("The data has been updated successfully")
            '    ShowMessage("The data has been updated successfully", getValueByKey("CLAE04"), False)
            '    Me.Close()
            'Else
            '    writeDaycloseLog("The data has NOT been  updated successfully")
            '    ShowMessage("The data has NOT been  updated successfully...Please Try again..", getValueByKey("CLAE04"), False)
            '    Me.Close()
            'End If

            'Me.Close()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"), False)
            LogException(ex)
            Me.Close()
        End Try
    End Sub
    Public Shared Sub RunCommandCom(exePath As String, args As String, permanent As Boolean)
        Try
            System.Diagnostics.Process.Start(exePath, args)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Function SaveCSV(ByVal PATH As String, ByVal TName As String, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try

            Dim SR As StreamReader = New StreamReader(PATH)
            Dim line As String = SR.ReadLine()
            If Not line Is Nothing Then
                Dim strArray As String() = line.Split(","c)
                Dim dt As DataTable = New DataTable()
                Dim dtnew As New DataTable
                dtnew = objClscommon.getStruct(TName, trans, con, Errormsg)
                Dim row As DataRow
                For i As Integer = 0 To dtnew.Columns.Count - 1
                    dt.Columns.Add(New DataColumn())
                    dt.Columns(i).ColumnName = dtnew.Columns(i).ColumnName
                    dt.AcceptChanges()
                Next
                'Do
                '    line = SR.ReadLine
                '    If Not line = String.Empty Then
                '        row = dt.NewRow()
                '        row.ItemArray = line.Split(","c)
                '        dt.Rows.Add(row)
                '    Else
                '        Exit Do
                '    End If
                'Loop
                'Return dt
                Do
                    row = dt.NewRow()
                    row.ItemArray = line.Split(",")
                    dt.Rows.Add(row)
                    line = SR.ReadLine
                Loop While Not line = String.Empty
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Shared Sub CompressFile(ByVal directorySelected As DirectoryInfo, ByVal GZDirectoryPath As String, ByRef GZFilePath As String)
        For Each fileToCompress As FileInfo In directorySelected.GetFiles()

            Using originalFileStream As FileStream = fileToCompress.OpenRead()

                If (File.GetAttributes(fileToCompress.FullName) And FileAttributes.Hidden) <> FileAttributes.Hidden And fileToCompress.Extension <> ".gz" Then

                    Using compressedFileStream As FileStream = File.Create(fileToCompress.FullName & ".gz")

                        Using compressionStream As GZipStream = New GZipStream(compressedFileStream, CompressionMode.Compress)
                            originalFileStream.CopyTo(compressionStream)
                        End Using
                    End Using

                    GZFilePath = GZDirectoryPath & "\" & fileToCompress.Name & ".gz"
                    Dim info As FileInfo = New FileInfo(GZDirectoryPath & "\" & fileToCompress.Name & ".gz")
                    '   Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString())
                End If
            End Using
        Next
    End Sub
    Public Function CreateDataTablefromArraylist(ByVal TArray As Array, ByVal TableName As String) As DataTable
        Try
            Dim StrCols As String = ""
            Dim TArrayList As ArrayList = New ArrayList(TArray)
            Dim dtMain As New System.Data.DataTable()
            writeDaycloseLog("Creating table " & TableName & "")
            'Looping to get col names
            Dim GenericObjectCol As Object = TArrayList.Item(0)
            For Each item As PropertyInfo In GenericObjectCol.GetType().GetProperties()
                Dim NbrProp As Integer = GenericObjectCol.GetType().GetProperties().Count
                Try
                    Dim column = New DataColumn()
                    Dim ColName As String = item.Name.ToString()
                    If TableName.ToUpper = "BUTTONARTICLE" Then
                        If ColName.ToUpper = "BUTTONARTICLEID" Then
                            StrCols += "ID" + ","
                        Else
                            StrCols += ColName + ","
                        End If

                    ElseIf TableName.ToUpper = "MSTARTICLECOMBO" Then
                        If ColName.ToUpper = "UPDATEGROUPIDS" Then
                            StrCols += "UpgradeGroupId" + ","
                        Else
                            StrCols += ColName + ","
                        End If
                    Else
                        StrCols += ColName + ","
                    End If

                Catch ex As Exception
                    LogException(ex)
                End Try
            Next
            StrCols = StrCols.TrimEnd(",")
            dtMain = objClscommon.GetTableStruct(TableName, strCols:=StrCols)
            writeDaycloseLog("Filling " & TableName & " with values obtained from response")
            If Not dtMain Is Nothing Then
                'Looping to insert values into datatable
                For i As Integer = 0 To TArrayList.Count - 1
                    Dim GenericObjectbuttonGroup As Object = TArrayList.Item(i)
                    Dim NbrProp As Integer = GenericObjectbuttonGroup.GetType().GetProperties().Count
                    Dim row As DataRow = dtMain.NewRow()
                    Dim RtrnValue As New Object
                    Dim j As Integer = 0
                    For Each item As PropertyInfo In GenericObjectbuttonGroup.GetType().GetProperties()
                        Dim type1 = dtMain.Columns(j).DataType.Name.ToString()
                        RtrnValue = SwitchCase(type1, item.GetValue(GenericObjectbuttonGroup, Nothing))
                        row(j) = RtrnValue
                        j += 1
                    Next
                    dtMain.Rows.Add(row)
                Next
                writeDaycloseLog("Datatable " & TableName & " filled successfully")
                Return dtMain
            Else
                Return Nothing
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function SwitchCase(ByVal Case1 As String, ByVal ColValue As Object) As Object
        Try
            Select Case Case1.ToUpper()
                Case "VARCHAR"
                    Exit Select
                Case "NUMERIC"
                    ColValue = CInt(ColValue)
                    Exit Select
                Case "BIT"
                    ColValue = CBool(ColValue)
                    Exit Select
                Case "DATETIME"
                    If ColValue Is Nothing Then
                        ColValue = DBNull.Value
                    Else
                        ColValue = CDate(ColValue)
                    End If
                    Exit Select
                Case "DATE"
                    ColValue = CDate(ColValue)
                    Exit Select
                Case "DECIMAL"
                    ColValue = CDec(ColValue)
                    Exit Select
                Case "BOOLEAN"
                    ColValue = CBool(ColValue)
                    Exit Select
            End Select
            Return ColValue
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function ArticleSynchronizer(ByVal Sitecode As String)
        Try
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()


            Dim dtmstArticleCombo1, dtarticleReplenishment1, dtmstArticleGroupDtl1, dtmstArticleImage1, dtmstArticleKit1 As New DataTable
            Dim dtarticleComp, dtarticleListed, dtarticleMatrix, dtarticleReplenishment, dtean, dtmasterArticleMap, dtmstArticleCombo,
             dtmstArticle, dtmstArticleGroupDtl, dtmstArticleGroup, dtmstArticleImage, dtmstArticleKit, dtmstArticleTree, dtmstArticleType,
             dtmstItemCategory, dtmstSpecialFeature, dtpurchaseInfoRecord, dtsalesInfoRecord, dtsiteArticleHierarchyMap, dtsiteArticleTaxMap,
             dtuoMconversion As Array
            Dim dsMainArticleSynchronizer, dsMainArticleSynchronizerCopy As New DataSet
            Dim SaveRsponse As Boolean
            'clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.ArticleSynchronizer

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.ArticleSynchronizer)

            Dim serviceStock As New ServiceReference4.ArticleSynchronizerClient(binding, address)
            'Dim serviceStock As New ServiceReference4.ArticleSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference4.retrieveArticleDetails
            Dim objRequestDTO As New ServiceReference4.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference4.soapWsHeader
            Dim objresponse As ServiceReference4.retrieveArticleDetailsResponse
            '  Dim ss As WebServiceDate

            'PARAMETERS

            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)

            Dim tempDate = Convert.ToDateTime("2010-01-01").ToString("yyyy-MM-dd")
            Dim FromDT = clsAdmin.DayOpenDate
            objRequestDTO.siteCode = Sitecode
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True
            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode
            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            writeDaycloseLog("sending request parameters to ArticleSynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            'Get response
            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "ArticleSynchronizer", clsAdmin.UserCode)
            objresponse = serviceStock.retrieveArticleDetails(service1)
            Dim errormsg As String = ""
            Try

                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    Dim dtos As ServiceReference4.articleDTOs = objresponse.return
                    writeDaycloseLog("Response from ArticleSynchronizer:SUCCESS")

                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleSynchronizer", clsAdmin.UserCode, reqID, "")
                    '  ArticleReplenishment()
                    'If Not dtos.articleReplenishmentDTO Is Nothing Then ''''
                    '    If dtos.articleReplenishmentDTO.Length > 0 Then
                    '        dtarticleReplenishment = dtos.articleReplenishmentDTO.Clone()
                    '        If dtarticleReplenishment.Length > 0 Then
                    '            dtarticleReplenishment1 = CreateDataTablefromArraylist(dtarticleReplenishment, "ArticleReplenishment")
                    '        End If
                    '    End If
                    'Else
                    '    writeDaycloseLog("Data Not Found for articlenodemap")
                    'End If
                    '   MstArticleCombo()
                    If Not dtos.mstArticleImageDTO Is Nothing Then '''
                        If dtos.mstArticleImageDTO.Length > 0 Then
                            dtmstArticleImage = dtos.mstArticleImageDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtmstArticleImage, "MstArticleImage")
                            If dtmain IsNot Nothing Then
                                dtmstArticleImage1 = objClscommon.ExecMstArticleImage(dtmain, tran, con, errormsg)
                                If dtmstArticleImage1 IsNot Nothing Then
                                    strTableUpdated += "MstArticleImage,"
                                    writeDaycloseLog("Data Updated Successfully for MstArticleImage")
                                Else
                                    writeDaycloseLog("Data Not Updated for MstArticleImage")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for MstArticleImage")
                    End If
                    If Not dtos.mstArticleComboDTO Is Nothing Then '''
                        If dtos.mstArticleComboDTO.Length > 0 Then
                            dtmstArticleCombo = dtos.mstArticleComboDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtmstArticleCombo, "MstArticleCombo")
                            If dtmain IsNot Nothing Then
                                dtmstArticleCombo1 = objClscommon.ExecMstArticleCombo(dtmain, tran, con, errormsg)
                                If Not dtmstArticleCombo1 Is Nothing Then
                                    strTableUpdated += "MstArticleCombo,"
                                    writeDaycloseLog("Data Updated Successfully for MstArticleCombo")
                                Else
                                    writeDaycloseLog("Data Not Updated for MstArticleCombo")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for MstArticleCombo")
                    End If

                    If Not dtos.mstArticleGroupDTO Is Nothing Then
                        If dtos.mstArticleGroupDTO.Length > 0 Then
                            dtmstArticleGroup = dtos.mstArticleGroupDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtmstArticleGroup, "MstArticleGroup")
                            If dtmain IsNot Nothing Then
                                dtmstArticleCombo1 = objClscommon.ExecMSTArticleGroup(dtmain, tran, con, errormsg)
                                If Not dtmstArticleCombo1 Is Nothing Then
                                    strTableUpdated += "MstArticleGroup,"
                                    writeDaycloseLog("Data Updated Successfully for MstArticleGroup")
                                Else
                                    writeDaycloseLog("Data Not Updated for MstArticleGroup")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for MstArticleGroup")
                    End If

                    If Not dtos.mstArticleGroupDtlDTO Is Nothing Then '''
                        If dtos.mstArticleGroupDtlDTO.Length > 0 Then
                            dtmstArticleGroupDtl = dtos.mstArticleGroupDtlDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtmstArticleGroupDtl, "mstArticleGroupDtl")
                            If Not dtmain Is Nothing Then
                                dtmstArticleGroupDtl1 = objClscommon.ExecMSTArticleGroupDtl(dtmain, tran, con, errormsg)
                                If Not dtmstArticleGroupDtl1 Is Nothing Then
                                    strTableUpdated += "MstArticleGroupDtl,"
                                    writeDaycloseLog("Data Updated Successfully for mstArticleGroupDtl")
                                Else
                                    writeDaycloseLog("Data Not Updated for mstArticleGroupDtl")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for mstArticleGroupDtl")
                    End If

                    If Not dtos.mstArticleKitDTO Is Nothing Then
                        If dtos.mstArticleKitDTO.Length > 0 Then
                            dtmstArticleKit = dtos.mstArticleKitDTO.Clone()
                            Dim dtmain As New DataTable
                            If dtmain IsNot Nothing Then
                                dtmain = CreateDataTablefromArraylist(dtmstArticleKit, "MstArticleKit")
                                If dtmain IsNot Nothing Then
                                    dtmstArticleKit1 = objClscommon.ExecMstArticleKit(dtmain, tran, con, errormsg)
                                    If Not dtmstArticleKit1 Is Nothing Then
                                        strTableUpdated += "MstArticleKit,"
                                        writeDaycloseLog("Data Updated Successfully for MstArticleKit")
                                    Else
                                        writeDaycloseLog("Data Not Updated for MstArticleKit")
                                    End If
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for MstArticleKit")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleSynchronizer", clsAdmin.UserCode, reqID, "Response from ArticleSynchronizer:FAILED")
                    writeDaycloseLog("Response from ArticleSynchronizer:FAILED")
                    Return False
                End If
            Catch ex As Exception
                LogException(ex)
                tran.Rollback()
                con.Close()
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
        Catch ex As Exception
            LogException(ex)
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
        End Try
    End Function
    Public Function AuthenticationSynchronizer(ByVal Sitecode As String)
        Try
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()

            Dim dtAuthusers , dtauthusersiterolemap , dtauthroletransactionmap As Array
            Dim dtauthroletransactionmap1 , dtauthusersiterolemap1 , dtAuthusers1 As New DataTable
            Dim dsAuthenticationSynchronizerMain As New DataSet
            ' clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.AuthenticationSynchronizer
            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()
            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.AuthenticationSynchronizer)
            Dim serviceStock As New ServiceReference5.AuthenticationSynchronizerClient(binding, address)
            'Dim serviceStock As New ServiceReference3.BillingSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference5.retrieveAuthenticationRecords
            Dim objRequestDTO As New ServiceReference5.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference5.soapWsHeader
            Dim objresponse As ServiceReference5.retrieveAuthenticationRecordsResponse
            ' Dim ss As WebServiceDate
            'PARAMETERS
            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)
            objRequestDTO.siteCode = Sitecode
            Dim tempDate = Convert.ToDateTime("2010-01-01").ToString("yyyy-MM-dd")
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True
            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode
            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "AuthenticationSynchronizer", clsAdmin.UserCode)
            writeDaycloseLog("sending request parameters to AuthenticationSynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            objresponse = serviceStock.retrieveAuthenticationRecords(service1)

            Dim errormsg As String = ""
            Try
                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    writeDaycloseLog("Response from AuthenticationSynchronizer:SUCCESS")
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "AuthenticationSynchronizer", clsAdmin.UserCode, reqID, "")
                    Dim dtos As ServiceReference5.authenticationDto = objresponse.return
                    If Not dtos.authUsersDTO Is Nothing Then
                        If dtos.authUsersDTO.Length > 0 Then
                            dtAuthusers = dtos.authUsersDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtAuthusers, "Authusers")
                            If Not dtmain Is Nothing Then
                                dtAuthusers1 = objClscommon.ExecAuthUsers(dtmain, tran, con, errormsg)
                                If Not dtAuthusers1 Is Nothing Then
                                    strTableUpdated += "Authusers,"
                                    writeDaycloseLog("Data Updated Successfully for  Authusers")
                                Else
                                    writeDaycloseLog("Data Not Updated for  Authusers")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for Authusers")
                    End If
                    If Not dtos.authUserSiteRoleMapDTO Is Nothing Then
                        If dtos.authUserSiteRoleMapDTO.Length > 0 Then
                            dtauthusersiterolemap = dtos.authUserSiteRoleMapDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtauthusersiterolemap, "authusersiterolemap")
                            If Not dtmain Is Nothing Then
                                dtauthusersiterolemap1 = objClscommon.ExecAuthUserSiteRoleMap(dtmain, tran, con, errormsg)
                                If Not dtauthusersiterolemap1 Is Nothing Then
                                    strTableUpdated += "Authusersiterolemap,"
                                    writeDaycloseLog("Data Updated Successfully for  authusersiterolemap")
                                Else
                                    writeDaycloseLog("Data Not Updated for  authusersiterolemap")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for authusersiterolemap")
                    End If
                    If Not dtos.authRoleTransactionMapDTO Is Nothing Then
                        If dtos.authRoleTransactionMapDTO.Length > 0 Then
                            dtauthroletransactionmap = dtos.authRoleTransactionMapDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtauthroletransactionmap, "authroletransactionmap")
                            If Not dtmain Is Nothing Then
                                dtauthroletransactionmap1 = objClscommon.ExecAuthRoleTransactionMap(dtmain, tran, con, errormsg)
                                If Not dtauthroletransactionmap1 Is Nothing Then
                                    strTableUpdated += "authroletransactionmap,"
                                    writeDaycloseLog("Data Updated Successfully for  authroletransactionmap")
                                Else
                                    writeDaycloseLog("Data Not Updated for  authroletransactionmap")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for authroletransactionmap")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "AuthenticationSynchronizer", clsAdmin.UserCode, reqID, "Response from AuthenticationSynchronizer:FAILED")
                    writeDaycloseLog("Response from AuthenticationSynchronizer:FAILED")
                    Return False
                End If
            Catch ex As Exception
                LogException(ex)
                tran.Rollback()
                con.Close()
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "AuthenticationSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
        Catch ex As Exception
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "AuthenticationSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            LogException(ex)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
        End Try
    End Function
    Public Function IndependentMastersSynchronizer(ByVal Sitecode As String)
        Try
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()


            Dim dsDefaultconfig As New DataTable
            Dim dtDefaultconfig As Array
            Dim SaveRsponse As Boolean

            Dim dsDefaultconfigMain As New DataSet
            ' clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.IndependentMastersSynchronizer

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.IndependentMastersSynchronizer)

            Dim serviceStock As New ServiceReference6.IndependentMastersSynchronizerClient(binding, address)
            ' Dim serviceStock As New ServiceReference1.UomSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference6.retreiveIndependentMastersDetails
            Dim objRequestDTO As New ServiceReference6.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference6.soapWsHeader
            Dim objresponse As ServiceReference6.retreiveIndependentMastersDetailsResponse
            '   Dim ss As WebServiceDate
            'Parameters
            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)

            objRequestDTO.siteCode = Sitecode
            Dim tempDate = Convert.ToDateTime("1999-01-01").ToString("yyyy-MM-dd")
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True
            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode
            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            writeDaycloseLog("sending request parameters to IndependentMastersSynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "IndependentMastersSynchronizer", clsAdmin.UserCode)
            objresponse = serviceStock.retreiveIndependentMastersDetails(service1)

            Dim errormsg As String = ""

            Try
                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    writeDaycloseLog("Response from IndependentMastersSynchronizer:SUCCESS")
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "IndependentMastersSynchronizer", clsAdmin.UserCode, reqID, "")
                    Dim dtos As ServiceReference6.independentMastersDTO = objresponse.return
                    If Not dtos.defaultConfigDTO Is Nothing Then
                        If dtos.defaultConfigDTO.Length > 0 Then
                            dtDefaultconfig = dtos.defaultConfigDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtDefaultconfig, "Defaultconfig")
                            If dtmain IsNot Nothing Then
                                dsDefaultconfig = objClscommon.ExecDefaultConfig(dtmain, tran, con, errormsg)
                                If Not dsDefaultconfig Is Nothing Then
                                    strTableUpdated += "Defaultconfig,"
                                    writeDaycloseLog("Data Updated Successfully for Defaultconfig")
                                Else
                                    writeDaycloseLog("Data Not Updated for Defaultconfig")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for Defaultconfig")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "IndependentMastersSynchronizer", clsAdmin.UserCode, reqID, "Response from IndependentMastersSynchronizer:FAILED")
                    writeDaycloseLog("Response from IndependentMastersSynchronizer:FAILED")
                    Return False
                End If

            Catch ex As Exception
                LogException(ex)
                tran.Rollback()
                con.Close()
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "IndependentMastersSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
        Catch ex As Exception
            LogException(ex)
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "IndependentMastersSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
        End Try
    End Function
    Public Function ArticleHierarchySynchronizer(ByVal Sitecode As String)
        Try
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()

            Dim daarticlenodemap As Array
            Dim dtarticlenodemap As New DataTable

            Dim daArticleMAP As Array
            Dim dtArticleMAP As New DataTable

            Dim damstarticlenode As Array
            Dim dtmstarticlenode As New DataTable

            Dim daArticleTreeNodeMap As Array
            Dim dtArticleTreeNodeMap As New DataTable


            Dim dsArticleHierarchySynchronizerMain As New DataSet

            ' clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.ArticleHierarchySynchronizer

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.ArticleHierarchySynchronizer)

            Dim serviceStock As New ServiceReference7.ArticleHierarchySynchronizerClient(binding, address)

            'Dim serviceStock As New ServiceReference3.BillingSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference7.retrieveArticleHierarchyRecords
            Dim objRequestDTO As New ServiceReference7.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference7.soapWsHeader
            Dim objresponse As ServiceReference7.retrieveArticleHierarchyRecordsResponse
            '   Dim ss As WebServiceDate
            'PARAMETERS

            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)


            objRequestDTO.siteCode = Sitecode
            Dim tempDate = Convert.ToDateTime("2009-04-01").ToString("yyyy-MM-dd")
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True

            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode

            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO

            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "ArticleHierarchySynchronizer", clsAdmin.UserCode)
            writeDaycloseLog("sending request parameters to ArticleHierarchySynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            objresponse = serviceStock.retrieveArticleHierarchyRecords(service1)

            Dim errormsg As String = ""
            Try
                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    writeDaycloseLog("Response from ArticleHierarchySynchronizer:SUCCESS")
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleHierarchySynchronizer", clsAdmin.UserCode, reqID, "")
                    Dim dtos As ServiceReference7.articleHierarchyDto = objresponse.return

                   
                    If Not dtos.articleMapDTO Is Nothing Then
                        If dtos.articleMapDTO.Length > 0 Then
                            daArticleMAP = dtos.articleMapDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(daArticleMAP, "ArticleMAP")
                            If Not dtmain Is Nothing Then
                                dtArticleMAP = objClscommon.ExecArticleMAP(dtmain, tran, con, errormsg)
                                If Not dtArticleMAP Is Nothing Then
                                    strTableUpdated += "ArticleMAP,"
                                    writeDaycloseLog("Data Updated Successfully for  ArticleMAP")
                                Else
                                    writeDaycloseLog("Data Not Updated for  ArticleMAP")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for articlenodemap")
                    End If
                    If Not dtos.mstArticleNodeDTO Is Nothing Then
                        If dtos.mstArticleNodeDTO.Length > 0 Then
                            damstarticlenode = dtos.mstArticleNodeDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(damstarticlenode, "mstarticlenode")
                            If Not dtmain Is Nothing Then
                                dtmstarticlenode = objClscommon.ExecMstArticleNode(dtmain, tran, con, errormsg)
                                If Not dtmstarticlenode Is Nothing Then
                                    strTableUpdated += "mstarticlenode,"
                                    writeDaycloseLog("Data Updated Successfully for  mstarticlenode")
                                Else
                                    writeDaycloseLog("Data Not Updated for  mstarticlenode")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for articlenodemap")
                    End If
                    If Not dtos.articleTreeNodeMapDTO Is Nothing Then
                        If dtos.articleTreeNodeMapDTO.Length > 0 Then
                            daArticleTreeNodeMap = dtos.articleTreeNodeMapDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(daArticleTreeNodeMap, "ArticleTreeNodeMap")
                            If Not dtmain Is Nothing Then
                                dtArticleTreeNodeMap = objClscommon.ExecArticleTreeNodeMap(dtmain, tran, con, errormsg)
                                If Not dtArticleTreeNodeMap Is Nothing Then
                                    strTableUpdated += "ArticleTreeNodeMap,"
                                    writeDaycloseLog("Data Updated Successfully for  ArticleTreeNodeMap")
                                Else
                                    writeDaycloseLog("Data Not Updated for  ArticleTreeNodeMap")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for ArticleTreeNodeMap")
                    End If
                    If Not dtos.articleNodeMapDTO Is Nothing Then
                        If dtos.articleNodeMapDTO.Length > 0 Then
                            daarticlenodemap = dtos.articleNodeMapDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(daarticlenodemap, "articlenodemap")
                            If dtmain IsNot Nothing Then
                                dtarticlenodemap = objClscommon.Execarticlenodemap(dtmain, tran, con, errormsg)
                                If Not dtarticlenodemap Is Nothing Then
                                    strTableUpdated += "articlenodemap,"
                                    writeDaycloseLog("Data Updated Successfully for articlenodemap")
                                Else
                                    writeDaycloseLog("Data Not Updated for  articlenodemap")
                                End If
                            End If

                        End If
                    Else
                        writeDaycloseLog("Data Not Found for articlenodemap")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleHierarchySynchronizer", clsAdmin.UserCode, reqID, "Response from ArticleHierarchySynchronizer:FAILED")
                    writeDaycloseLog("Response from ArticleHierarchySynchronizer:FAILED")
                    Return False
                End If
            Catch ex As Exception
                tran.Rollback()
                con.Close()
                LogException(ex)
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleHierarchySynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
        Catch ex As Exception
            LogException(ex)
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "ArticleHierarchySynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
        End Try
    End Function
    Public Function UomSynchronizer(ByVal Sitecode As String)
        Try
         
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()


            Dim Resp As Boolean
            Dim dsarticleUOM As New DataTable
            Dim dtarticleUOM As Array
            Dim SaveRsponse As Boolean
            Dim dtmstUoM As Array
            Dim dsMainUomSynchronizer As New DataSet
            Dim dsPrim As New DataSet
            'clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.UomSynchronizer
            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.UomSynchronizer)

            Dim serviceStock As New ServiceReference1.UomSynchronizerClient(binding, address)
            ' Dim serviceStock As New ServiceReference1.UomSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference1.retrieveUomRecords
            Dim objRequestDTO As New ServiceReference1.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference1.soapWsHeader
            Dim objresponse As ServiceReference1.retrieveUomRecordsResponse
            '  Dim ss As WebServiceDate
            'Parameters

            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)

            objRequestDTO.siteCode = Sitecode
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True
            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode
            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            writeDaycloseLog("sending request parameters to UomSynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")

            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "UomSynchronizer", clsAdmin.UserCode)

            objresponse = serviceStock.retrieveUomRecords(service1)
            Dim errormsg As String = ""
            Try
                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    writeDaycloseLog("Response from UomSynchronizer:SUCCESS")
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "UomSynchronizer", clsAdmin.UserCode, reqID, "")
                    Dim dtos As ServiceReference1.uomdtOs = objresponse.return
                    If Not dtos.articleUOMDTO Is Nothing Then
                        If dtos.articleUOMDTO.Length > 0 Then
                            dtarticleUOM = dtos.articleUOMDTO.Clone()
                        End If
                    End If
                    Dim dtmainsync As New DataTable
                    If Not dtarticleUOM Is Nothing Then
                        If dtarticleUOM.Length > 0 Then
                            dtmainsync = CreateDataTablefromArraylist(dtarticleUOM, "ArticleUOM")
                            If Not dtmainsync Is Nothing Then
                                If dtmainsync.Rows.Count > 0 Then
                                    dsarticleUOM = objClscommon.ExecArticleUOM(dtmainsync, tran, con, errormsg)
                                    If Not dsarticleUOM Is Nothing Then
                                        strTableUpdated += "ArticleUOM,"
                                        writeDaycloseLog("Data Updated sucessfully for ArticleUOM")

                                    Else
                                        writeDaycloseLog("Data Not Updated for ArticleUOM")
                                    End If
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for ArticleUOM")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "UomSynchronizer", clsAdmin.UserCode, reqID, "Response from UomSynchronizer:FAILED")
                    writeDaycloseLog("Response from UomSynchronizer:FAILED")
                    Return False
                End If
            Catch ex As Exception
                tran.Rollback()
                con.Close()
                LogException(ex)
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "UomSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
        Catch ex As Exception
            LogException(ex)
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "UomSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
        End Try
    End Function
    Public Function CsvFileSyncronizer(ByVal Sitecode As String, ByVal TableNameWS As String, ByVal JsonP As String) As String
        Try


            Dim dtimageSync As Array
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.CsvFileSyncronizer)
            Dim serviceStockCsvFileSync As New ServiceReference2.CsvFileSyncronizerClient(binding, address)
            CType(serviceStockCsvFileSync.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference2.syncronizeCsvFile
            Dim objRequestDTO As New ServiceReference2.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference2.soapWsHeader
            Dim objresponse As ServiceReference2.syncronizeCsvFileResponse
            ' Dim ss As WebServiceDate
            objRequestDTO.siteCode = Sitecode
            Dim tempDate = Convert.ToDateTime("2000-01-01").ToString("yyyy-MM-dd")
            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True
            objRequestDTO.tableName = TableNameWS
            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode

            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            writeDaycloseLog("sending request parameters to CsvFileSyncronizer_ objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode)
            objresponse = serviceStockCsvFileSync.syncronizeCsvFile(service1)
            Dim errormsg As String = ""
            If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                Dim dtos As ServiceReference2.imageSyncResponseDTO = objresponse.return
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode, reqID, "")
                Dim GZFilePath As String = Application.StartupPath & "\Sync"

                If dtos.imageSyncDTO.Length > 0 Then
                    Dim FileByte As Byte() = dtos.imageSyncDTO(0).fileBytes
                    Dim FileName As String = dtos.imageSyncDTO(0).fileName
                    Dim filen As String = Replace(FileName, ".zip", "")
                    If Not dtos.imageSyncDTO Is Nothing Then
                        If dtos.imageSyncDTO.Length > 0 Then
                            dtimageSync = dtos.imageSyncDTO.Clone()
                        End If
                    End If
                    Dim TableName As String = Replace(filen, "_0001", "")
                    '  Dim JsonDirectoryPath As String = clsDefaultConfiguration.DayCloseReportPath & "\Sync\ZipCSVFiles" + Now.ToString("ddMMyyyyhhmmss") + ""
                    If (Not System.IO.Directory.Exists(JsonP)) Then
                        System.IO.Directory.CreateDirectory(JsonP)
                    End If
                    Dim JsonFilePath As String = JsonP & "\" & filen & ".zip"
                    Dim filePrp As New FileInfo(JsonFilePath)
                    If Not System.IO.File.Exists(JsonFilePath) Then

                        File.WriteAllBytes(JsonFilePath, FileByte)
                    Else
                        File.Delete(JsonFilePath)
                        System.IO.File.Delete(JsonFilePath)
                        System.IO.File.Create(JsonFilePath).Dispose()
                        File.WriteAllBytes(JsonFilePath, FileByte)

                    End If
                    Dim sb As New System.Text.StringBuilder
                    Dim exePath As String = """" & Application.StartupPath & "\Sync\7z.exe"""
                    Dim args As String = "e """ + JsonP + "\" + FileName + "" + """ -O""" + JsonP + "\ExtractedFiles"""

                    sb.AppendLine("" & exePath & " " & args & "")
                    Dim pathF As String = "" & Application.StartupPath & "\Sync\Unzip" + Now.ToString("yyyyMMddHHmmssfff") + ".bat"
                    System.IO.File.WriteAllText(pathF, sb.ToString())
                    writeDaycloseLog("" & pathF & "")
                    'Dim exePath As String = "" & Application.StartupPath & "\Sync\7z.exe"
                    'Dim args As String = "e " + JsonP + "\" + FileName + "" + " -O""" + JsonP + "\ExtractedFiles"""
                    writeDaycloseLog("path & args = " + exePath + " " + args + "")
                    ' RunCommandCom(exePath, args, True)
                    RunCommandCom(pathF, args, True)
                    Return filen
                    writeDaycloseLog("filen=" + filen + "")
                Else
                    writeDaycloseLog("Record not available for " + TableNameWS + " in CsvFileSyncronizer")
                    Return ""
                End If
            Else
                writeDaycloseLog("Response from CsvFileSyncronizer:FAILED")
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode, reqID, "Response from CsvFileSyncronizer:FAILED")
                Return ""
            End If
        Catch ex As Exception
            LogException(ex)
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer", clsAdmin.UserCode, reqID, ex.Message)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return ""
        End Try
    End Function
    'Public Function CsvFileSyncronizer(ByVal Sitecode As String, ByVal TableNameWS As String, ByVal JsonP As String) As String
    '    Try


    '        Dim dtimageSync As Array
    '        Dim binding As New System.ServiceModel.BasicHttpBinding()

    '        'Specify the address to be used for the client.
    '        Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.CsvFileSyncronizer)
    '        Dim serviceStockCsvFileSync As New ServiceReference2.CsvFileSyncronizerClient(binding, address)
    '        CType(serviceStockCsvFileSync.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
    '        Dim service1 As New ServiceReference2.syncronizeCsvFile
    '        Dim objRequestDTO As New ServiceReference2.wsRequestDTO
    '        Dim webserviceRequestHeader As New ServiceReference2.soapWsHeader
    '        Dim objresponse As ServiceReference2.syncronizeCsvFileResponse
    '        ' Dim ss As WebServiceDate
    '        objRequestDTO.siteCode = Sitecode
    '        Dim tempDate = Convert.ToDateTime("2000-01-01").ToString("yyyy-MM-dd")
    '        Dim theirTime As Date = Date.Now
    '        theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
    '        Dim myFormat = "yyyy-MM-dd"
    '        Dim myTime = Format(CDate(theirTime), myFormat)
    '        objRequestDTO.syncFromDate = myTime
    '        objRequestDTO.syncFromDateSpecified = True
    '        objRequestDTO.tableName = TableNameWS
    '        objRequestDTO.pushOrPull = True
    '        webserviceRequestHeader.userName = "admin"
    '        webserviceRequestHeader.password = "admin"
    '        webserviceRequestHeader.siteCode = Sitecode

    '        objRequestDTO.soapWsHeader = webserviceRequestHeader
    '        service1.arg0 = objRequestDTO
    '        writeDaycloseLog("sending request parameters to CsvFileSyncronizer_ objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
    '        reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode)
    '        objresponse = serviceStockCsvFileSync.syncronizeCsvFile(service1)
    '        Dim errormsg As String = ""
    '        If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
    '            Dim dtos As ServiceReference2.imageSyncResponseDTO = objresponse.return
    '            objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode, reqID, "")
    '            Dim GZFilePath As String = Application.StartupPath & "\Sync"

    '            If dtos.imageSyncDTO.Length > 0 Then
    '                Dim FileByte As Byte() = dtos.imageSyncDTO(0).fileBytes
    '                Dim FileName As String = dtos.imageSyncDTO(0).fileName
    '                Dim filen As String = Replace(FileName, ".zip", "")
    '                If Not dtos.imageSyncDTO Is Nothing Then
    '                    If dtos.imageSyncDTO.Length > 0 Then
    '                        dtimageSync = dtos.imageSyncDTO.Clone()
    '                    End If
    '                End If
    '                Dim TableName As String = Replace(filen, "_0001", "")
    '                '  Dim JsonDirectoryPath As String = clsDefaultConfiguration.DayCloseReportPath & "\Sync\ZipCSVFiles" + Now.ToString("ddMMyyyyhhmmss") + ""
    '                If (Not System.IO.Directory.Exists(JsonP)) Then
    '                    System.IO.Directory.CreateDirectory(JsonP)
    '                End If
    '                Dim JsonFilePath As String = JsonP & "\" & filen & ".zip"
    '                Dim filePrp As New FileInfo(JsonFilePath)
    '                If Not System.IO.File.Exists(JsonFilePath) Then

    '                    File.WriteAllBytes(JsonFilePath, FileByte)
    '                Else
    '                    File.Delete(JsonFilePath)
    '                    System.IO.File.Delete(JsonFilePath)
    '                    System.IO.File.Create(JsonFilePath).Dispose()
    '                    File.WriteAllBytes(JsonFilePath, FileByte)

    '                End If
    '                Dim exePath As String = "" & Application.StartupPath & "\7z.exe"
    '                Dim args As String = "e " + JsonP + "\" + FileName + "" + " -o""" + JsonP + """"
    '                RunCommandCom(exePath, args, True)
    '                Return filen
    '            Else
    '                writeDaycloseLog("Record not available for " + TableNameWS + " in CsvFileSyncronizer")
    '                Return ""
    '            End If
    '        Else
    '            writeDaycloseLog("Response from CsvFileSyncronizer:FAILED")
    '            objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer_" + TableNameWS + "", clsAdmin.UserCode, reqID, "Response from CsvFileSyncronizer:FAILED")
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '        objClscommon.UpdatePullSyncAuditLog(strSitecode, "CsvFileSyncronizer", clsAdmin.UserCode, reqID, ex.Message)
    '        ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
    '        Me.Close()
    '        Return ""
    '    End Try
    'End Function
    Public Function SaveCSVFiles(ByVal Path As String, ByVal TableNameWS As String, ByVal filen As String, ByRef tran As SqlTransaction, ByRef con As SqlConnection, ByRef errormsg As String) As Boolean

        Try


            Dim SaveCSV1 As New DataTable
            Dim dtMainf As New DataTable
            Dim dtNewds As New DataTable
            Dim Filsss As String = "" + Path + "\ExtractedFiles\" + filen + ".csv"
            'Dim SavePath As String = System.IO.Path.Combine(JsonP + "\ExtractedFiles", Filsss) 'combines the saveDirectory and the filename to get a fully qualified path.

            If System.IO.File.Exists(Filsss) Then

                SaveCSV1 = SaveCSV("" + Path + "\ExtractedFiles\" + filen + ".csv", TableNameWS, tran, con, errormsg)
                If Not SaveCSV1 Is Nothing Then
                    For i As Integer = 0 To SaveCSV1.Rows.Count - 1

                        Dim dr As DataRow = SaveCSV1.Rows(i)

                        For j As Integer = 0 To SaveCSV1.Columns.Count - 1
                            If (dr(j).ToString().Contains("""")) Then

                                dr(j) = Replace(dr(j), """", String.Empty)

                                dr.AcceptChanges()
                            End If
                        Next
                        SaveCSV1.AcceptChanges()
                    Next
                    dtNewds = SaveCSV1.Copy
                    Dim dtnew As New DataTable
                    dtnew = objClscommon.getStruct(TableNameWS, tran, con, errormsg)

                    For Each dr As DataRow In SaveCSV1.Rows
                        Dim destRow As DataRow = dtnew.NewRow()
                        For k As Integer = 0 To SaveCSV1.Columns.Count - 1
                            If Not IsDBNull(dr(k)) Then
                                If Not String.IsNullOrEmpty(dr(k)) Then
                                    destRow(k) = dr(k)
                                End If
                            End If
                        Next
                        dtnew.Rows.Add(destRow)
                        dtnew.AcceptChanges()
                    Next
                    Dim GenericObjectbuttonGroup As Object = dtnew
                    dtMainf = objClscommon.getStruct(TableNameWS, tran, con, errormsg)
                    Dim RtrnValue As New Object
                    Dim x As Integer = 0
                    For i As Integer = 0 To dtnew.Rows.Count - 1
                        Dim rows As DataRow = dtMainf.NewRow()
                        For k As Integer = 0 To GenericObjectbuttonGroup.Columns.Count - 1

                            Dim type1 = GenericObjectbuttonGroup.Columns(k).DataType.Name.ToString()
                            If Not IsDBNull(dtnew(i).Item(k)) Then
                                If Not String.IsNullOrEmpty(dtnew(i).Item(k)) Then
                                    RtrnValue = SwitchCase(type1, dtnew(i).Item(k))
                                    rows.Item(k) = RtrnValue
                                Else
                                    rows.Item(k) = String.Empty
                                End If
                            Else
                                rows.Item(k) = DBNull.Value
                            End If
                        Next
                        dtMainf.Rows.Add(rows)
                        dtMainf.AcceptChanges()
                    Next
                    dtMainf.TableName = TableNameWS
                    If TableNameWS.ToUpper = "MSTARTICLE" Then
                        dtMainf.Columns.Add("Descriptions", GetType(String))
                    End If

                    If TableNameWS.ToUpper = "MSTARTICLE" Then
                        dtRecords = objClscommon.Exec_USP_MatchRecords(dtMainf, strSitecode, tran, con)
                    End If
                    Try
                        If Not dtMainf Is Nothing Then
                            If dtMainf.Rows.Count > 0 Then
                                Dim dtMainfs As New DataTable
                                If TableNameWS.ToUpper = "MSTEAN" Then
                                    Dim dtReturnA As New DataTable
                                    dtMainfs = objClscommon.ExecMstEAN(dtMainf, tran, con, errormsg)
                                    If dtMainfs IsNot Nothing Then
                                        strTableUpdated += "MSTEAN,"
                                        writeDaycloseLog("Data Updated Successfully for MSTEAN")
                                        ' Return True
                                    Else
                                        writeDaycloseLog("Data Not Updated for MSTEAN")
                                        'Return False
                                    End If
                                ElseIf TableNameWS.ToUpper = "ARTICLESTOCKBALANCES" Then
                                    Dim dtReturnA As New DataTable
                                    dtMainfs = objClscommon.ExecArticleStockBalances(dtMainf, tran, con, errormsg)
                                    If dtMainfs IsNot Nothing Then
                                        strTableUpdated += "ARTICLESTOCKBALANCES,"
                                        writeDaycloseLog("Data Updated Successfully for ARTICLESTOCKBALANCES")
                                        'Return True
                                    Else
                                        writeDaycloseLog("Data Not Updated for ARTICLESTOCKBALANCES")
                                        ' Return False
                                    End If
                                ElseIf TableNameWS.ToUpper = "MSTARTICLE" Then
                                    dtMainfs = objClscommon.ExecMstArticle(dtMainf, tran, con, errormsg)
                                    If dtMainfs IsNot Nothing Then
                                        strTableUpdated += "MSTARTICLE,"
                                        writeDaycloseLog("Data Updated Successfully for MSTARTICLE")
                                        '  Return True
                                    Else
                                        writeDaycloseLog("Data Updated Successfully for MSTARTICLE")
                                        ' Return False
                                    End If
                                ElseIf TableNameWS.ToUpper = "SALESINFORECORD" Then
                                    dtMainfs = objClscommon.ExecSalesInfoRecord(dtMainf, tran, con, errormsg)
                                    If dtMainfs IsNot Nothing Then
                                        strTableUpdated += "SALESINFORECORD,"
                                        writeDaycloseLog("Data Updated Successfully for SALESINFORECORD")
                                        ' Return True
                                    Else
                                        writeDaycloseLog("Data Updated Successfully for SALESINFORECORD")
                                        ' Return False
                                    End If

                                ElseIf TableNameWS.ToUpper = "PURCHASEINFORECORD" Then
                                    dtMainfs = objClscommon.ExecPurchaseInfoRecord(dtMainf, tran, con, errormsg)
                                    For Each dr As DataRow In dtMainf.Rows
                                        Dim SiteCode1, SupplierCode1, EAN1 As String
                                        Dim CPBaseCurr1, CPLocalCurr1 As Double
                                        SiteCode1 = dr("SiteCode")
                                        SupplierCode1 = dr("SupplierCode")
                                        EAN1 = dr("EAN")
                                        CPBaseCurr1 = Convert.ToDouble(dr("CPBaseCurr"))
                                        CPLocalCurr1 = Convert.ToDouble(dr("CPLocalCurr"))
                                        Dim res As Boolean
                                        res = objClscommon.UpdatePurchaseInfoRecord(SiteCode1, SupplierCode1, EAN1, CPBaseCurr1, CPLocalCurr1, tran, con, errormsg)
                                    Next

                                    If Not dtMainfs Is Nothing Then
                                        strTableUpdated += "PURCHASEINFORECORD,"
                                        writeDaycloseLog("Data Updated Successfully for PURCHASEINFORECORD")
                                        'Return True
                                    Else
                                        writeDaycloseLog("Data Updated Successfully for PURCHASEINFORECORD")
                                        'Return False
                                    End If
                                End If
                            End If

                        Else
                            writeDaycloseLog("Data Not Available...")
                            Return False
                            Exit Function
                        End If

                    Catch ex As Exception
                        tran.Rollback()
                        con.Close()
                        LogException(ex)
                        'objClscommon.UpdatePullSyncAuditLog(strSitecode, "CSVFileSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                        ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                        Return False
                    End Try
                Else
                    writeDaycloseLog("Data Not Available...")
                    Return False
                    Exit Function
                End If
            Else
                writeDaycloseLog("file not found at " + Filsss + "")
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'Public Function SaveCSVFiles(ByVal Path As String, ByVal TableNameWS As String, ByVal filen As String) As Boolean

    '    Try
    '        DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
    '        Dim tran As SqlTransaction = Nothing
    '        Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
    '        con.Open()
    '        tran = con.BeginTransaction()

    '        Dim SaveCSV1 As New DataTable
    '        Dim dtMainf As New DataTable
    '        Dim dtNewds As New DataTable
    '        Dim Filsss As String = "" + Path + "\ExtractedFiles\" + filen + ".csv"
    '        'Dim SavePath As String = System.IO.Path.Combine(JsonP + "\ExtractedFiles", Filsss) 'combines the saveDirectory and the filename to get a fully qualified path.
    '        Dim errormsg As String = ""
    '        If System.IO.File.Exists(Filsss) Then

    '            SaveCSV1 = SaveCSV("" + Path + "\ExtractedFiles\" + filen + ".csv", TableNameWS)
    '            If Not SaveCSV1 Is Nothing Then
    '                For i As Integer = 0 To SaveCSV1.Rows.Count - 1

    '                    Dim dr As DataRow = SaveCSV1.Rows(i)

    '                    For j As Integer = 0 To SaveCSV1.Columns.Count - 1
    '                        If (dr(j).ToString().Contains("""")) Then

    '                            dr(j) = Replace(dr(j), """", String.Empty)

    '                            dr.AcceptChanges()
    '                        End If
    '                    Next
    '                    SaveCSV1.AcceptChanges()
    '                Next
    '                dtNewds = SaveCSV1.Copy
    '                Dim dtnew As New DataTable
    '                dtnew = objClscommon.getStruct(TableNameWS)

    '                For Each dr As DataRow In SaveCSV1.Rows
    '                    Dim destRow As DataRow = dtnew.NewRow()
    '                    For k As Integer = 0 To SaveCSV1.Columns.Count - 1
    '                        If Not IsDBNull(dr(k)) Then
    '                            If Not String.IsNullOrEmpty(dr(k)) Then
    '                                destRow(k) = dr(k)
    '                            End If
    '                        End If
    '                    Next
    '                    dtnew.Rows.Add(destRow)
    '                    dtnew.AcceptChanges()
    '                Next
    '                Dim GenericObjectbuttonGroup As Object = dtnew
    '                dtMainf = objClscommon.getStruct(TableNameWS)
    '                Dim RtrnValue As New Object
    '                Dim x As Integer = 0
    '                For i As Integer = 0 To dtnew.Rows.Count - 1
    '                    Dim rows As DataRow = dtMainf.NewRow()
    '                    For k As Integer = 0 To GenericObjectbuttonGroup.Columns.Count - 1

    '                        Dim type1 = GenericObjectbuttonGroup.Columns(k).DataType.Name.ToString()
    '                        If Not IsDBNull(dtnew(i).Item(k)) Then
    '                            If Not String.IsNullOrEmpty(dtnew(i).Item(k)) Then
    '                                RtrnValue = SwitchCase(type1, dtnew(i).Item(k))
    '                                rows.Item(k) = RtrnValue
    '                            Else
    '                                rows.Item(k) = String.Empty
    '                            End If
    '                        Else
    '                            rows.Item(k) = DBNull.Value
    '                        End If
    '                    Next
    '                    dtMainf.Rows.Add(rows)
    '                    dtMainf.AcceptChanges()
    '                Next
    '                dtMainf.TableName = TableNameWS
    '                If TableNameWS.ToUpper = "MSTARTICLE" Then
    '                    dtMainf.Columns.Add("Descriptions", GetType(String))
    '                End If

    '                If TableNameWS.ToUpper = "MSTARTICLE" Then
    '                    dtRecords = objClscommon.Exec_USP_MatchRecords(dtMainf, strSitecode, tran, con)
    '                End If
    '                Try
    '                    If Not dtMainf Is Nothing Then
    '                        If dtMainf.Rows.Count > 0 Then
    '                            Dim dtMainfs As New DataTable
    '                            If TableNameWS.ToUpper = "MSTEAN" Then
    '                                Dim dtReturnA As New DataTable
    '                                dtMainfs = objClscommon.ExecMstEAN(dtMainf, tran, con, errormsg)
    '                                If dtMainfs IsNot Nothing Then
    '                                    strTableUpdated += "MSTEAN,"
    '                                    writeDaycloseLog("Data Updated Successfully for MSTEAN")
    '                                    '  Return True
    '                                Else
    '                                    writeDaycloseLog("Data Not Updated for MSTEAN")
    '                                    ' Return False
    '                                End If
    '                            ElseIf TableNameWS.ToUpper = "ARTICLESTOCKBALANCES" Then
    '                                Dim dtReturnA As New DataTable
    '                                dtMainfs = objClscommon.ExecArticleStockBalances(dtMainf, tran, con, errormsg)
    '                                If dtMainfs IsNot Nothing Then
    '                                    strTableUpdated += "ARTICLESTOCKBALANCES,"
    '                                    writeDaycloseLog("Data Updated Successfully for ARTICLESTOCKBALANCES")
    '                                    ' Return True
    '                                Else
    '                                    writeDaycloseLog("Data Not Updated for ARTICLESTOCKBALANCES")
    '                                    'Return False
    '                                End If
    '                            ElseIf TableNameWS.ToUpper = "MSTARTICLE" Then
    '                                dtMainfs = objClscommon.ExecMstArticle(dtMainf, tran, con, errormsg)
    '                                If dtMainfs IsNot Nothing Then
    '                                    strTableUpdated += "MSTARTICLE,"
    '                                    writeDaycloseLog("Data Updated Successfully for MSTARTICLE")
    '                                    'Return True
    '                                Else
    '                                    writeDaycloseLog("Data Not Updated for for MSTARTICLE")
    '                                    ' Return False
    '                                End If
    '                            ElseIf TableNameWS.ToUpper = "SALESINFORECORD" Then
    '                                dtMainfs = objClscommon.ExecSalesInfoRecord(dtMainf, tran, con, errormsg)
    '                                If dtMainfs IsNot Nothing Then
    '                                    strTableUpdated += "SALESINFORECORD,"
    '                                    writeDaycloseLog("Data Updated Successfully for SALESINFORECORD")
    '                                    'Return True
    '                                Else
    '                                    writeDaycloseLog("Data Not Updated for SALESINFORECORD")
    '                                    ' Return False
    '                                End If

    '                            ElseIf TableNameWS.ToUpper = "PURCHASEINFORECORD" Then
    '                                dtMainfs = objClscommon.ExecPurchaseInfoRecord(dtMainf, tran, con, errormsg)
    '                                If Not dtMainfs Is Nothing Then
    '                                    strTableUpdated += "PURCHASEINFORECORD,"
    '                                    writeDaycloseLog("Data Updated Successfully for PURCHASEINFORECORD")
    '                                    'Return True
    '                                Else
    '                                    writeDaycloseLog("Data Not Updated for PURCHASEINFORECORD")
    '                                    'Return False
    '                                End If
    '                            End If
    '                        End If
    '                        If errormsg <> "" Then
    '                            tran.Rollback()
    '                            con.Close()
    '                            Return False
    '                        Else
    '                            tran.Commit()
    '                            con.Close()
    '                            Return True
    '                        End If
    '                    Else
    '                        writeDaycloseLog("Data Not Available...")
    '                        Return False
    '                        Exit Function
    '                    End If

    '                Catch ex As Exception
    '                    tran.Rollback()
    '                    con.Close()
    '                    LogException(ex)
    '                    'objClscommon.UpdatePullSyncAuditLog(strSitecode, "CSVFileSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
    '                    ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
    '                    Return False
    '                End Try
    '            Else
    '                writeDaycloseLog("Data Not Available...")
    '                Return False
    '                Exit Function
    '            End If
    '        Else
    '            writeDaycloseLog("file not found at " + Filsss + "")
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function
    'Public Function SaveCSVFiles(ByVal Path As String, ByVal TableNameWS As String, ByVal filen As String) As Boolean

    '    Try

    '        DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
    '        Dim tran As SqlTransaction = Nothing
    '        Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
    '        con.Open()
    '        tran = con.BeginTransaction()

    '        Dim SaveCSV1 As New DataTable
    '        Dim dtMainf As New DataTable
    '        Dim dtNewds As New DataTable
    '        Dim Filsss As String = "" + Path + "\ExtractedFiles\" + filen + ".csv"
    '        'Dim SavePath As String = System.IO.Path.Combine(JsonP + "\ExtractedFiles", Filsss) 'combines the saveDirectory and the filename to get a fully qualified path.

    '        If System.IO.File.Exists(Filsss) Then

    '            SaveCSV1 = SaveCSV("" + Path + "\ExtractedFiles\" + filen + ".csv", TableNameWS)
    '            For i As Integer = 0 To SaveCSV1.Rows.Count - 1

    '                Dim dr As DataRow = SaveCSV1.Rows(i)

    '                For j As Integer = 0 To SaveCSV1.Columns.Count - 1
    '                    If (dr(j).ToString().Contains("""")) Then

    '                        dr(j) = Replace(dr(j), """", String.Empty)

    '                        dr.AcceptChanges()
    '                    End If
    '                Next
    '                SaveCSV1.AcceptChanges()
    '            Next
    '            dtNewds = SaveCSV1.Copy
    '            Dim dtnew As New DataTable
    '            dtnew = objClscommon.getStruct(TableNameWS)

    '            For Each dr As DataRow In SaveCSV1.Rows
    '                Dim destRow As DataRow = dtnew.NewRow()
    '                For k As Integer = 0 To SaveCSV1.Columns.Count - 1
    '                    If Not IsDBNull(dr(k)) Then
    '                        If Not String.IsNullOrEmpty(dr(k)) Then
    '                            destRow(k) = dr(k)
    '                        End If
    '                    End If
    '                Next
    '                dtnew.Rows.Add(destRow)
    '                dtnew.AcceptChanges()
    '            Next
    '            Dim GenericObjectbuttonGroup As Object = dtnew
    '            dtMainf = objClscommon.getStruct(TableNameWS)
    '            Dim RtrnValue As New Object
    '            Dim x As Integer = 0
    '            For i As Integer = 0 To dtnew.Rows.Count - 1
    '                Dim rows As DataRow = dtMainf.NewRow()
    '                For k As Integer = 0 To GenericObjectbuttonGroup.Columns.Count - 1

    '                    Dim type1 = GenericObjectbuttonGroup.Columns(k).DataType.Name.ToString()
    '                    If Not IsDBNull(dtnew(i).Item(k)) Then
    '                        If Not String.IsNullOrEmpty(dtnew(i).Item(k)) Then
    '                            RtrnValue = SwitchCase(type1, dtnew(i).Item(k))
    '                            rows.Item(k) = RtrnValue
    '                        Else
    '                            rows.Item(k) = String.Empty
    '                        End If
    '                    Else
    '                        rows.Item(k) = DBNull.Value
    '                    End If
    '                Next
    '                dtMainf.Rows.Add(rows)
    '                dtMainf.AcceptChanges()
    '            Next
    '            dtMainf.TableName = TableNameWS
    '            If TableNameWS.ToUpper = "MSTARTICLE" Then
    '                dtMainf.Columns.Add("Descriptions", GetType(String))
    '            End If
    '            If TableNameWS.ToUpper = "MSTARTICLE" Then
    '                dtRecords = objClscommon.Exec_USP_MatchRecords(dtMainf)
    '            End If
    '            If Not dtMainf Is Nothing Then
    '                If dtMainf.Rows.Count > 0 Then
    '                    Dim dtMainfs As New DataTable
    '                    If TableNameWS.ToUpper = "ARTICLESTOCKBALANCES" Then
    '                        Dim dtReturnA As New DataTable
    '                        ' dtMainfs = objClscommon.ExecArticleStockBalances(dtMainf)
    '                        If dtMainfs IsNot Nothing Then
    '                            writeDaycloseLog("Data Updated Successfully for ARTICLESTOCKBALANCES")
    '                            Return True
    '                        Else
    '                            writeDaycloseLog("Data Not Updated for ARTICLESTOCKBALANCES")
    '                            Return False
    '                        End If
    '                    ElseIf TableNameWS.ToUpper = "MSTARTICLE" Then
    '                        ' dtMainfs = objClscommon.ExecMstArticle(dtMainf)
    '                        If dtMainfs IsNot Nothing Then
    '                            writeDaycloseLog("Data Updated Successfully for MSTARTICLE")
    '                            Return True
    '                        Else
    '                            writeDaycloseLog("Data Updated Successfully for MSTARTICLE")
    '                            Return False
    '                        End If
    '                    ElseIf TableNameWS.ToUpper = "SALESINFORECORD" Then
    '                        ' dtMainfs = objClscommon.ExecSalesInfoRecord(dtMainf)
    '                        If dtMainfs IsNot Nothing Then
    '                            writeDaycloseLog("Data Updated Successfully for SALESINFORECORD")
    '                            Return True
    '                        Else
    '                            writeDaycloseLog("Data Updated Successfully for SALESINFORECORD")
    '                            Return False
    '                        End If
    '                    ElseIf TableNameWS.ToUpper = "PURCHASEINFORECORD" Then
    '                        '  dtMainfs = objClscommon.ExecPurchaseInfoRecord(dtMainf)
    '                        If Not dtMainfs Is Nothing Then
    '                            writeDaycloseLog("Data Updated Successfully for PURCHASEINFORECORD")
    '                            Return True
    '                        Else
    '                            writeDaycloseLog("Data Updated Successfully for PURCHASEINFORECORD")
    '                            Return False
    '                        End If
    '                    End If
    '                End If

    '            Else
    '                writeDaycloseLog("Data Not Available...")
    '                Return False
    '                Exit Function
    '            End If
    '        Else
    '            writeDaycloseLog("file not found at " + Filsss + "")
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function
    Public Function BillingSynchronizer(ByVal Sitecode As String)
        Try
            DataBaseConnection._OnlineConn = ReadSpectrumParamFile("ServerConnectionString")
            Dim tran As SqlTransaction = Nothing
            Dim con As New SqlConnection(DataBaseConnection._OnlineConn)
            con.Open()
            tran = con.BeginTransaction()


            Dim dtbuttonArticle1 As New DataTable
            Dim dtbuttonArticle, dtbuttonGroup, dtcmReceiptTender, dtcmTenderValid, dtmstReceipt, dtmstSalesPerson, dtposDeviceProfile, dtprintingDetail As Array
            Dim dsBillingSynchronizerMain As New DataSet
            Dim dsBillingSynchronizerMainCopy As DataSet
            Dim dtButnArticle, dtButngroup, dtPrinterInfo1, dtButnArticle1, dtButngroup1 As New DataTable


            '  clsDefaultConfiguration.WebserviceStockURL = clsDefaultConfiguration.BillingSynchronizer

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.BillingSynchronizer)

            Dim serviceStock As New ServiceReference3.BillingSynchronizerClient(binding, address)

            'Dim serviceStock As New ServiceReference3.BillingSynchronizerClient
            CType(serviceStock.Endpoint.Binding, ServiceModel.BasicHttpBinding).MaxReceivedMessageSize = Int32.MaxValue
            Dim service1 As New ServiceReference3.retrieveBillingDetails
            Dim objRequestDTO As New ServiceReference3.wsRequestDTO
            Dim webserviceRequestHeader As New ServiceReference3.soapWsHeader
            Dim objresponse As ServiceReference3.retrieveBillingDetailsResponse
            ' Dim ss As WebServiceDate
            'PARAMETERS
            Dim theirTime As Date = Date.Now
            theirTime = theirTime.AddDays(-clsDefaultConfiguration.NO_OF_BACK_DAYS_FOR_SYNC)
            Dim myFormat = "yyyy-MM-dd"
            Dim myTime = Format(CDate(theirTime), myFormat)

            objRequestDTO.siteCode = Sitecode
            Dim tempDate = Convert.ToDateTime("2010-01-01").ToString("yyyy-MM-dd")
            objRequestDTO.syncFromDate = myTime
            objRequestDTO.syncFromDateSpecified = True

            objRequestDTO.pushOrPull = True
            webserviceRequestHeader.userName = "admin"
            webserviceRequestHeader.password = "admin"
            webserviceRequestHeader.siteCode = Sitecode

            objRequestDTO.soapWsHeader = webserviceRequestHeader
            service1.arg0 = objRequestDTO
            reqID = objClscommon.SavePullSyncAuditLog(strSitecode, "BillingSynchronizer", clsAdmin.UserCode)
            writeDaycloseLog("sending request parameters to BillingSynchronizer objRequestDTO.siteCode=" & strSitecode & ",objRequestDTO.syncFromDate = " & myTime & "")
            objresponse = serviceStock.retrieveBillingDetails(service1)
            Dim errormsg As String = ""

            Try
                If objresponse.return.faultDTO.faultStackTrace Is Nothing Then
                    writeDaycloseLog("Response from BillingSynchronizer:SUCCESS")
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "BillingSynchronizer", clsAdmin.UserCode, reqID, "")
                    Dim dtos As ServiceReference3.billingDTOs = objresponse.return

                    If Not dtos.buttonArticleDTO Is Nothing Then
                        If dtos.buttonArticleDTO.Length > 0 Then
                            dtbuttonArticle = dtos.buttonArticleDTO.Clone()
                            Dim dtmain As New DataTable
                            dtmain = CreateDataTablefromArraylist(dtbuttonArticle, "Buttonarticle")
                            If dtmain IsNot Nothing Then
                                Try
                                    dtbuttonArticle1 = objClscommon.ExecButtonarticle(dtmain, tran, con, errormsg)
                                Catch ex As Exception
                                    LogException(ex)
                                End Try
                                If Not dtbuttonArticle1 Is Nothing Then
                                    strTableUpdated += "Buttonarticle,"
                                    writeDaycloseLog("Data Updated Successfully for Buttonarticle")
                                Else
                                    writeDaycloseLog("Data Not Updated for Buttonarticle")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for Buttonarticle")
                    End If
                    If Not dtos.buttonGroupDTO Is Nothing Then
                        If dtos.buttonGroupDTO.Length > 0 Then
                            dtbuttonGroup = dtos.buttonGroupDTO.Clone()
                            dtButngroup = CreateDataTablefromArraylist(dtbuttonGroup, "Buttongroup")
                            If Not dtButngroup Is Nothing Then
                                Try
                                    dtButngroup1 = objClscommon.ExecBUTTONGROUP(dtButngroup, tran, con, errormsg)
                                Catch ex As Exception
                                    LogException(ex)
                                End Try
                                If Not dtButngroup1 Is Nothing Then
                                    strTableUpdated += "Buttongroup,"
                                    writeDaycloseLog("Data Updated Successfully for Buttongroup")
                                Else
                                    writeDaycloseLog("Data Not Updated for Buttongroup")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for Buttongroup")
                    End If
                    If Not dtos.printingDetailDTO Is Nothing Then
                        If dtos.printingDetailDTO.Length > 0 Then
                            dtprintingDetail = dtos.printingDetailDTO.Clone()
                            dtPrinterInfo = CreateDataTablefromArraylist(dtprintingDetail, "Printingdetail")
                            If Not dtPrinterInfo Is Nothing Then
                                Try
                                    dtPrinterInfo1 = objClscommon.Execprintingdetail(dtPrinterInfo, tran, con, errormsg)
                                Catch ex As Exception
                                    LogException(ex)
                                End Try
                                If Not dtPrinterInfo1 Is Nothing Then
                                    strTableUpdated += "Printingdetail,"
                                    writeDaycloseLog("Data Updated Successfully for Printingdetail")
                                Else
                                    writeDaycloseLog("Data Not Updated for Printingdetail")
                                End If
                            End If
                        End If
                    Else
                        writeDaycloseLog("Data Not Found for Printingdetail")
                    End If
                    If errormsg <> "" Then
                        tran.Rollback()
                        con.Close()
                        Return False
                    Else
                        tran.Commit()
                        con.Close()
                        Return True
                    End If
                Else
                    objClscommon.UpdatePullSyncAuditLog(strSitecode, "BillingSynchronizer", clsAdmin.UserCode, reqID, "Response from BillingSynchronizer:FAILED")
                    writeDaycloseLog("Response from BillingSynchronizer:FAILED")
                    Return False
                End If
            Catch ex As Exception
                tran.Rollback()
                con.Close()
                LogException(ex)
                objClscommon.UpdatePullSyncAuditLog(strSitecode, "BillingSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
                ShowMessage("Internal Error Occurred..Please Try Later", getValueByKey("CLAE05"), False)
                Me.Close()
                Return False
            End Try
            Catch ex As Exception
            objClscommon.UpdatePullSyncAuditLog(strSitecode, "BillingSynchronizer", clsAdmin.UserCode, reqID, ex.Message)
            LogException(ex)
            ShowMessage("Server Unavailable..Please Try Later", getValueByKey("CLAE05"), False)
            Me.Close()
            Return False
            End Try
    End Function
    Private Sub writeDaycloseLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
End Class