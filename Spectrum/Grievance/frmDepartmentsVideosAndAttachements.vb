Imports SpectrumBL
Imports System.Net
Imports System.IO
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Windows.Forms

Public Class frmDepartmentsVideosAndAttachements
    Dim objclsCommon As New clsCommon
    Dim FtpVideosFolders As DataTable
    Dim lblPathLink As LinkLabel
    Dim lblDepartmentName As CtrlLabel
    Private Sub frmDepartmentsVideosAndAttachements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Themechange()
        BindDepartment()
    End Sub
    Sub BindDepartment()
        Try
            FtpVideosFolders = objclsCommon.GetFtpVideosFolders()
            If FtpVideosFolders.Rows.Count > 0 Then
                'tblPnlDepartments.RowStyles.Clear()
                'tblPnlDepartments.RowCount = 0
                Dim rowIndex = 0
                rowIndex = rowIndex + 1
                For Each dr As DataRow In FtpVideosFolders.Rows
                    tblPnlDepartments.AutoSize = True
                    tblPnlDepartments.Dock = DockStyle.Fill
                    lblDepartmentName = New CtrlLabel
                    lblPathLink = New LinkLabel
                    tblPnlDepartments.Margin = New Padding(20, 3, 3, 3)
                    AddHandler lblPathLink.Click, AddressOf lblPathLink_Click
                    tblPnlDepartments.RowStyles.Add(New RowStyle(SizeType.Absolute, 25))
                    tblPnlDepartments.Controls.Add(lblDepartmentName, 0, rowIndex)
                    tblPnlDepartments.Controls.Add(lblPathLink, 1, rowIndex)
                    If FtpVideosFolders.Rows.Count <> tblPnlDepartments.RowCount Then
                        If FtpVideosFolders.Rows.Count - 1 = tblPnlDepartments.RowCount Then
                        Else
                            tblPnlDepartments.Size = New Size(1050, tblPnlDepartments.Height + 20)
                            tblPnlDepartments.MinimumSize = New Size(1050, tblPnlDepartments.Height + 20)
                        End If


                        tblPnlDepartments.RowCount += 1
                        rowIndex += 1
                        lblDepartmentName.Text = dr("DeptName")
                        lblDepartmentName.ForeColor = Color.Black
                        lblDepartmentName.AutoSize = True
                        'lblDepartmentName.Size = New Size(75, 18)
                        lblDepartmentName.SendToBack()
                        lblDepartmentName.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                        lblDepartmentName.TextAlign = ContentAlignment.MiddleLeft
                        lblDepartmentName.BackColor = Color.Transparent
                        lblDepartmentName.BorderColor = Color.Transparent
                        lblDepartmentName.BorderStyle = BorderStyle.None

                        ' lblPathLink.Text = dr("VideoLink")
                        lblPathLink.Text = dr("folderNames")
                        lblPathLink.Tag = dr("deptShortName") & "/" & dr("folderNames")
                        lblPathLink.ForeColor = Color.White
                        lblPathLink.AutoSize = True
                        lblPathLink.SendToBack()
                        lblPathLink.Font = New Font("Neo Sans", 10, FontStyle.Bold)
                        lblPathLink.TextAlign = ContentAlignment.MiddleLeft
                        lblPathLink.BackColor = Color.Transparent
                        lblPathLink.LinkColor = Color.FromArgb(0, 102, 204)
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        tblPnlDepartments.RowCount = 0
        tblPnlDepartments.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        Dim rowIndex = 0
        lblDepartmentName = New CtrlLabel
        Dim lblLocations = New CtrlLabel
        tblPnlDepartments.Controls.Add(lblDepartmentName, 0, rowIndex)
        tblPnlDepartments.Controls.Add(lblLocations, 1, rowIndex)
        lblDepartmentName.Text = "Departments"
        lblDepartmentName.ForeColor = Color.Black
        lblDepartmentName.AutoSize = True
        lblDepartmentName.SendToBack()
        lblDepartmentName.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblDepartmentName.TextAlign = ContentAlignment.MiddleLeft
        lblDepartmentName.BackColor = Color.Transparent
        lblDepartmentName.BorderStyle = BorderStyle.None

        lblLocations.Text = "Location"
        lblLocations.ForeColor = Color.Black
        lblLocations.AutoSize = True
        'lblLocations.Size = New Size(75, 18)
        lblLocations.SendToBack()
        lblLocations.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblLocations.TextAlign = ContentAlignment.MiddleLeft
        lblLocations.BackColor = Color.Transparent
        lblLocations.BorderStyle = BorderStyle.None
        tblPnlDepartments.RowStyles.Add(New RowStyle(SizeType.Absolute, 25))
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            tblPnlDepartments.BackColor = Color.White
        End If
        tblPnlDepartments.AutoScroll = True

        btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnClose.BackColor = Color.Transparent
        btnClose.BackColor = Color.FromArgb(0, 107, 163)
        btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        btnClose.Font = New Font("Neo Sans", 7.5, FontStyle.Bold)
        btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.TextAlign = ContentAlignment.MiddleCenter
        btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
    End Function
    Public Sub lblPathLink_Click(sender As Object, e As EventArgs)
        Dim folderName As String = sender.tag
        OpenFTPLocation(folderName)
    End Sub
    Public Sub OpenFTPLocation(ByVal folderName As String)
        Dim request As FtpWebRequest = Nothing
        Dim response As FtpWebResponse = Nothing
        Try
            'folderName = folderName.Replace("/", "-")
            Dim FTPURL, FTP_DIRECTORY, FTP_USER_NAME, FTP_USER_PASSWORD As String
            Dim objClsDayClose As New clsDayClose
            Dim dt = objclsCommon.GetFTPDetails()
            For Each row In dt.Rows
                If row("FldLabel").ToString() = "SPECTRUM_FTP_URL" Then
                    'NetworkCred.Password = row("FldValue").ToString()
                    FTPURL = "ftp://" & row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SPECTRUM_FTP_VIDEO_DIRECTORY" Then
                    'NetworkCred.UserName = row("FldValue").ToString()
                    FTP_DIRECTORY = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SPECTRUM_FTP_FO_VIDEO_USER_NAME" Then
                    FTP_USER_NAME = row("FldValue").ToString()
                ElseIf row("FldLabel").ToString() = "SPECTRUM_FTP_FO_VIDEO_USER_PASSWORD" Then
                    FTP_USER_PASSWORD = row("FldValue").ToString()
                End If
            Next
            Dim splitfolderName As String() = folderName.Split("/")
            request = DirectCast(WebRequest.Create(FTPURL & FTP_DIRECTORY & "/" & folderName), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            request.Credentials = New NetworkCredential(FTP_USER_NAME, FTP_USER_PASSWORD)
            request.UsePassive = True
            request.UseBinary = True
            request.EnableSsl = False
            response = DirectCast(request.GetResponse(), FtpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                'Read the Response as String and split using New Line character.
                'entries = reader.ReadToEnd().Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList()
                'Dim directoryExist As Boolean = False
                'For Each entry As String In entries
                '    Dim splits As String() = entry.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)

                '    If splits(3) = splitfolderName(0) Then
                '        directoryExist = True
                '    End If
                'Next
                'If directoryExist Then
                Dim FolderDirectory = FTPURL & FTP_DIRECTORY & "/" & folderName
                'Process.Start("explorer.exe", FolderDirectory)
                Process.Start(FolderDirectory)
                'End If
            End Using
        Catch Webex As WebException
            Dim mes = "Ftp exception: File or directory may not be available error code:550"
            writeDaycloseLog(mes)
            writeDaycloseLog(Webex.Message)
            ShowMessage("Directory may not exist or directory may be changed", "Information")

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub writeDaycloseLog(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
