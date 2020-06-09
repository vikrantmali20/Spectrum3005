Imports SpectrumBL
Public Class frmAutoCancel

    Dim objclsRes As New clsReservation
    Private _CustNo As String
    Public Property CustNo() As String
        Get
            Return _CustNo
        End Get
        Set(ByVal value As String)
            _CustNo = value
        End Set
    End Property

    Private _PhoneNo As String
    Public Property PhoneNo() As String
        Get
            Return _PhoneNo
        End Get
        Set(ByVal value As String)
            _PhoneNo = value
        End Set
    End Property

    Private _ReservationId As String
    Public Property ReservationId() As String
        Get
            Return _ReservationId
        End Get
        Set(ByVal value As String)
            _ReservationId = value
        End Set
    End Property

    Private _FromTime As DateTime
    Public Property FromTime() As DateTime
        Get
            Return _FromTime
        End Get
        Set(ByVal value As DateTime)
            _FromTime = value
        End Set
    End Property

    Private _ListOfTables As String
    Public Property ListOfTables() As String
        Get
            Return _ListOfTables
        End Get
        Set(ByVal value As String)
            _ListOfTables = value
        End Set
    End Property
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Dim ReservationidList As New List(Of String)
            ReservationidList.Add(ReservationId)
            If objclsRes.UpdateCancelReservationDetails(ReservationidList, clsAdmin.SiteCode) Then

            End If
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub

    Private Sub btnExtend_Click(sender As Object, e As EventArgs) Handles btnExtend.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Retry
            Dim ExtendTime As Integer = clsDefaultConfiguration.ExtendTime
            If Not ExtendTime = 0 Then
                If objclsRes.UpdateReservationTimeDetails(ReservationId, FromTime.AddMinutes(ExtendTime), clsAdmin.SiteCode) Then
                    Me.Close()
                End If
            End If
           
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub

    Private Sub frmAutoCancel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim objCustm As New clsCLPCustomer()
        Dim dtCustInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustNo)
        If dtCustInfo IsNot Nothing AndAlso dtCustInfo.rows.count > 0 Then
            lblCustValue.Text = dtCustInfo.Rows(0)("CustomerName")
            lblPhoneValue.Text = dtCustInfo.Rows(0)("MobileNo")
            lblTableNoValue.Text = ListOfTables
        End If
    End Sub
End Class