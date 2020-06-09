Imports System.Xml.Serialization
Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class FrmShowItems


  

    Public Property _dsItem As DataTable
    Public Property dsItem As DataTable
        Get
            Return _dsItem
        End Get
        Set(value As DataTable)
            _dsItem = value
        End Set
    End Property

#Region "Events"
    Private Sub frmProductNotificationPopups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


           
            BindAddArticle(dsItem)

        
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        Me.BackColor = Color.FromArgb(76, 76, 76)

    End Function

#End Region
#Region "Function"
    Private Sub BindAddArticle(ByVal dt As DataTable)
        Try

            GrdShowData.DataSource = dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BindShowDataGrid()
        Try
            ' GrdShowData.DataSource = DtArticleCOmbo

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region
    Private Sub CtrlBtn1_Click(sender As Object, e As EventArgs) Handles CtrlBtn1.Click
        Me.Close()
    End Sub
End Class