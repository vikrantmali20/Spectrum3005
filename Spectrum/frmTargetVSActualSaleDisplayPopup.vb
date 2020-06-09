Imports Spectrum
Imports SpectrumBL
Public Class frmTargetVSActualSaleDisplayPopup

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmTargetVSActualSaleDisplayPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetTargetValues()
        GetActualValues()
    End Sub
    Sub GetTargetValues()
        Try
            Dim objCls As New clsCommon
            Dim dtTargetValues As DataTable
            Dim _targetSales As Decimal
            Dim _targetBuncount As Decimal
            Dim _targetConversion As Decimal
            dtTargetValues = objCls.GetTargetValue(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            If Not dtTargetValues.Rows.Count - 1 Then
                _targetSales = dtTargetValues.Rows(0)("TargetSales").ToString()
                _targetBuncount = dtTargetValues.Rows(0)("BunCount").ToString()
                _targetConversion = dtTargetValues.Rows(0)("Conversion").ToString()
            End If
            lblTargetSales.Text = Math.Round(_targetSales, 2)
            lblTargetBunCount.Text = Math.Round(_targetBuncount, 2)
            lblTargetConversion.Text = Math.Round(_targetConversion, 2)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Sub GetActualValues()
        Try
            Dim objCls As New clsCommon
            Dim ds As New DataSet
            Dim _actualSale As Decimal
            Dim _actualBunCount As Decimal
            Dim _actualConversion As Double
            ds = objCls.GetSalesValue(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            If Not ds.Tables("tblAcutalSale").Rows.Count - 1 Then
                _actualSale = ds.Tables("tblAcutalSale").Rows(0)("P_NetSales").ToString()
                _actualSale = Math.Round(_actualSale, 2)
                If _actualSale = 0 Then
                    _actualSale = 0
                End If
            Else
                _actualSale = 0
            End If
            If Not ds.Tables("tblActualBunCount").Rows.Count - 1 Then
                _actualBunCount = ds.Tables("tblActualBunCount").Rows(0)("BunCount").ToString()
                _actualBunCount = Math.Round(_actualBunCount, 2)
                If _actualBunCount = 0 Then
                    _actualBunCount = 0
                End If
            Else
                _actualBunCount = 0
            End If
            lblActualSales.Text = _actualSale
            lblActualBunCount.Text = _actualBunCount
            _actualConversion = (_actualSale / _actualBunCount)
            _actualConversion = Math.Round(_actualConversion, 2)
            lblActualConversion.Text = _actualConversion
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

End Class