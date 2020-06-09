Imports C1.Win.C1TrueDBGrid
Public Class frmHcCommonView

#Region "Global Varibale's & Property's"
    Private drView As DataRowView

    ''' <summary>
    ''' Set Form Caption Text
    ''' </summary>
    ''' <remarks></remarks>
    Private _CaptionText As String = String.Empty
    Public Property SetCaptionText() As String
        Get
            Return _CaptionText
        End Get
        Set(ByVal value As String)
            _CaptionText = value
        End Set
    End Property

    Private _InfoType As String = String.Empty
    Public WriteOnly Property InfoType() As String
        Set(ByVal value As String)
            _InfoType = value
        End Set
    End Property

    ''' <summary>
    ''' Set Display Column in the Data List
    ''' </summary>
    ''' <remarks></remarks>
    Private _DisplayColumns As String = String.Empty
    Public WriteOnly Property DisplayColumns() As String
        Set(ByVal value As String)
            _DisplayColumns = value
        End Set
    End Property

    ''' <summary>
    ''' Set the data from Outside to Show
    ''' </summary>
    ''' <remarks></remarks>
    Private _SetDataTable As DataTable = Nothing
    Public WriteOnly Property SetDataTable() As DataTable
        Set(ByVal value As DataTable)
            _SetDataTable = value
        End Set
    End Property

    ''' <summary>
    ''' Get Selected Record from the Data List
    ''' </summary>
    ''' <remarks></remarks>
    Private _selectRow As DataRow
    Public ReadOnly Property GetResultRow() As DataRow
        Get
            Return _selectRow
        End Get
    End Property
#End Region

    Private Sub frmHcCommonView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            If (_SetDataTable IsNot Nothing) Then
                GridDisplayInfo.DataSource = _SetDataTable

                LblTotalNoRecords.Text = _SetDataTable.Rows.Count
            End If

            If Not (String.IsNullOrEmpty(_DisplayColumns)) Then
                For Each ColmInfo As C1DisplayColumn In GridDisplayInfo.Splits(0).DisplayColumns
                    If (_DisplayColumns.ToUpper.IndexOf(ColmInfo.Name.ToUpper) <> -1) Then
                        ColmInfo.Visible = True
                        ColmInfo.AutoSize()
                    Else
                        ColmInfo.Visible = False
                    End If
                Next

                GridDisplayInfo.Select()
                GridDisplayInfo.FilterActive = True
            End If

            If (_InfoType = "RefDoctor") Then
                GridDisplayInfo.Splits(0).DisplayColumns("EmployeeCode").DataColumn.Caption = "Ref Doctor Code"
                GridDisplayInfo.Splits(0).DisplayColumns("EmployeeCode").Width = 110
            End If

            Me.Text = _CaptionText

        Catch ex As Exception
            ShowMessage(ex.Message, _CaptionText & " Error ", True)
        End Try
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Try
            drView = Nothing
            drView = GridDisplayInfo.Item(GridDisplayInfo.Row)
            _selectRow = drView.Row

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowMessage(ex.Message, "Row Selection Error ", True)
        End Try
    End Sub
    Private Sub GridDisplayInfo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDisplayInfo.DoubleClick
        BtnOk_Click(Nothing, Nothing)
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Try
            _SetDataTable = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnNewPatient_Click(sender As Object, e As EventArgs) Handles BtnNewPatient.Click
        Try
            Using objHcPatientReg As New frmHCPatientRegistration
                objHcPatientReg.IsNewRegn = True
                If Not String.IsNullOrEmpty(GridDisplayInfo.Columns("MobileNo").FilterText) Then
                    objHcPatientReg.SearchedValue = GridDisplayInfo.Columns("MobileNo").FilterText
                End If
                Dim dialogResult = objHcPatientReg.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    Exit Sub
                ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                    '----Track Customers
                End If

            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class