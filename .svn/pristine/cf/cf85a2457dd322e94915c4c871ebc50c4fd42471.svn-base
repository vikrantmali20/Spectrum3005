Imports SpectrumBL
Imports System.Data.SqlClient
Public Class frmManualPromotions
    Dim dsPromotions As DataSet
    Dim objPromo As New clsApplyPromotion
    Private Sub frmManualPromotions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dtPromo As DataTable
            dtPromo = objPromo.GetManualPromotions(clsAdmin.SiteCode, 0)
            dsPromotions = New DataSet
            dtPromo.Columns("Status").DefaultValue = 0 '0000155
            dsPromotions.Tables.Add(dtPromo)
            SetBindings()
            CreateNewRecord(Me, dsPromotions, "MANUALPROMOTION")
            SetCulture(Me, Me.Name)
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub SetBindings()
        Try
            txtPromotionId.DataBindings.Add("Text", dsPromotions.Tables(0), "PromotionId")
            txtPromotionName.DataBindings.Add("Text", dsPromotions.Tables(0), "PromotionName")
            txtValue.DataBindings.Add("Text", dsPromotions.Tables(0), "PromotionValue")
            chkActive.DataBindings.Add("Checked", dsPromotions.Tables(0), "STATUS")
            'rbDisc.DataBindings.Add("Checked", dsPromotions.Tables(0), "DiscPer")
            'rbSellingPrice.DataBindings.Add("Checked", dsPromotions.Tables(0), "FixedSelling")
            'rbFixedPriceOff.DataBindings.Add("Checked", dsPromotions.Tables(0), "FixedPriceOff")

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Me.BindingContext(dsPromotions.Tables(0)).EndCurrentEdit()
            selectDiscType(True)
            Dim dtTemp As DataTable = dsPromotions.Tables(0).Copy()
            If objPromo.SaveManualPromotions(dtTemp) = True Then
                ShowMessage(getValueByKey("MPR001"), "MPR001 - " & getValueByKey("CLAE04"))
                'ShowMessage("Promotion Saved Sucessfully", "Information")
                ClearData()
            Else
                ShowMessage(getValueByKey("MPR002"), "MPR002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Promotion not Saved ", "Information")
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub ClearData()
        Try
            For Each dt As DataTable In dsPromotions.Tables
                dt.Clear()
            Next
            rbDisc.Checked = False
            rbFixedPriceOff.Checked = False
            rbSellingPrice.Checked = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub selectDiscType(ByVal checked As Boolean)
        Try
            If checked = True Then
                If rbDisc.Checked = True Then
                    dsPromotions.Tables(0).Rows(0)("DISCPER") = True
                End If
                If rbFixedPriceOff.Checked = True Then
                    dsPromotions.Tables(0).Rows(0)("FIXEDPRICEOFF") = True
                End If
                If rbSellingPrice.Checked = True Then
                    dsPromotions.Tables(0).Rows(0)("FIXEDSELLING") = True
                End If
            Else
                If Not dsPromotions.Tables(0).Rows(0)("DISCPER") Is DBNull.Value AndAlso dsPromotions.Tables(0).Rows(0)("DISCPER") = True Then
                    rbDisc.Checked = True
                End If
                If Not dsPromotions.Tables(0).Rows(0)("FIXEDPRICEOFF") Is DBNull.Value AndAlso dsPromotions.Tables(0).Rows(0)("FIXEDPRICEOFF") = True Then
                    rbFixedPriceOff.Checked = True
                End If
                If Not dsPromotions.Tables(0).Rows(0)("FIXEDSELLING") Is DBNull.Value AndAlso dsPromotions.Tables(0).Rows(0)("FIXEDSELLING") = True Then
                    rbSellingPrice.Checked = True
                End If
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Try
            ClearData()
            Dim dt As DataTable
            dt = objPromo.GetManualPromotions(clsAdmin.SiteCode)
            Dim objSearch As New frmNCommonView()
            objSearch.SetData = dt
            objSearch.ShowDialog()
            If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                dt = objPromo.GetManualPromotions(clsAdmin.SiteCode, objSearch.search(0))
                dsPromotions.Tables(0).Merge(dt, False, MissingSchemaAction.Ignore)
                selectDiscType(False)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            ClearData()
            CreateNewRecord(Me, dsPromotions, "MANUALPROMOTION")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
End Class