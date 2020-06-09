Imports Spectrum
Imports SpectrumBL
Imports System.Text
Imports System.IO
Imports System.Drawing.Graphics
Imports C1.Win.C1Command
Imports System.Linq
Imports System.IO.Directory

Public Class frmPosTabCreation
#Region "-----------------------------Propertises and Variables"
    Dim objCls As New clsCommon
    Dim objCM As New clsCashMemo
    Dim objItem As New clsIteamSearch
    Dim buttonGrpHirarchyLevelCount As Integer = 0
    Dim GroupName As String = ""
    Dim GroupId As String = ""
    Dim ParentGroupId As String = ""
    Dim dtSites As DataTable
    Dim dtGroupsCategory As DataTable
    Dim dtAddGroup As DataTable
    Dim dtCurrentArticle As DataTable
    Dim dtAddFinalArticles As DataTable
    Dim dtArticleCategoryTree As DataTable
    Dim dtCurrentArticlePartialList As DataTable
    Dim dtArticlePartialList As DataTable
    Dim dtArticlePartialListLeft As DataTable
    Dim dtArticlePartialListRight As DataTable
    Dim dtTree As DataTable
    Dim ItemHierarchyList As DataTable
    Dim dtCurrentArticleCategory As DataTable
    Dim dtDeleteViewArticles As DataTable
    '-----------------controls
    Dim ChildGroupLabel As Label
    Dim lblGrpNameHeading As CtrlLabel
    Dim txtGroupNameTextValue As CtrlTextBox
    Dim btnGroupNameUpdate As CtrlBtn
    Dim btnGroupDelete As CtrlBtn

    Dim OuterShellPoints As New Point(10, 20)
    Dim pnlGroupAction As TableLayoutPanel
    Dim TabButtonChildrensGroups As TabControl
    Dim DocTabButtonChildrensGroups As Spectrum.CtrlTab

#End Region
#Region "-----------------------------Add Groups"
#Region "------------------------------------------Events"
#Region "------------------------------Drop down Events"


    Private Sub cmbBaseGroupCategory_TextChanged(sender As Object, e As EventArgs) Handles cmbBaseGroupCategory.TextChanged
        Try
            Dim _GrpId = Convert.ToString(cmbBaseGroupCategory.SelectedValue)
            If _GrpId = "" Or _GrpId = "0" Then
                DropDownVisibility(False)
                ParentGroupId = ""
                Exit Sub
            End If
            Dim dt = BindGroupCategory(_GrpId)
            If dt.Rows.Count > 1 Then
                ParentGroupId = _GrpId
                GroupName = cmbBaseGroupCategory.Text
                cmbChildAddGroup1.Visible = True
                lblChildGroupName1.Visible = True
                BindDropDown(dt, cmbChildAddGroup1)
            Else
                DropDownVisibility(False)
                ParentGroupId = _GrpId
                GroupName = cmbBaseGroupCategory.Text
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbChildAddGroup1_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup1.TextChanged

        Dim _GrpId = Convert.ToString(cmbChildAddGroup1.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup2.Visible = False
            cmbChildAddGroup3.Visible = False
            cmbChildAddGroup4.Visible = False
            cmbChildAddGroup5.Visible = False
            cmbChildAddGroup6.Visible = False
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName2.Visible = False
            lblChildGroupName3.Visible = False
            lblChildGroupName4.Visible = False
            lblChildGroupName5.Visible = False
            lblChildGroupName6.Visible = False
            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup1.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup2, lblChildGroupName2)
    End Sub

    Public Sub VisibleChildGroupLabel(ByRef Ctrllbl As CtrlLabel, ByVal value As Boolean)
        Ctrllbl.Visible = value
    End Sub
    Private Sub cmbChildAddGroup2_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup2.TextChanged

        Dim _GrpId = Convert.ToString(cmbChildAddGroup2.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup3.Visible = False
            cmbChildAddGroup4.Visible = False
            cmbChildAddGroup5.Visible = False
            cmbChildAddGroup6.Visible = False
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName3.Visible = False
            lblChildGroupName4.Visible = False
            lblChildGroupName5.Visible = False
            lblChildGroupName6.Visible = False
            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup2.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup3, lblChildGroupName3)
    End Sub

    Private Sub cmbChildAddGroup3_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup3.TextChanged

        Dim _GrpId = Convert.ToString(cmbChildAddGroup3.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup4.Visible = False
            cmbChildAddGroup5.Visible = False
            cmbChildAddGroup6.Visible = False
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName4.Visible = False
            lblChildGroupName5.Visible = False
            lblChildGroupName6.Visible = False
            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup3.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup4, lblChildGroupName3)
    End Sub

    Private Sub cmbChildAddGroup4_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup4.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup4.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup5.Visible = False
            cmbChildAddGroup6.Visible = False
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName5.Visible = False
            lblChildGroupName6.Visible = False
            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup4.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup5, lblChildGroupName4)
    End Sub

    Private Sub cmbChildAddGroup5_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup5.TextChanged
        fpRemarksAddGroup.AutoScroll = True
        Dim _GrpId = Convert.ToString(cmbChildAddGroup5.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            fpRemarksAddGroup.AutoScroll = False
            cmbChildAddGroup6.Visible = False
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName6.Visible = False
            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False

            Exit Sub
        End If
        GroupName = cmbChildAddGroup5.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup6, lblChildGroupName5)
    End Sub

    Private Sub cmbChildAddGroup6_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup6.TextChanged
        fpRemarksAddGroup.AutoScroll = True
        Dim _GrpId = Convert.ToString(cmbChildAddGroup6.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup7.Visible = False
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName7.Visible = False
            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup6.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup7, lblChildGroupName6)
    End Sub

    Private Sub cmbChildAddGroup7_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup7.TextChanged
        '    ChildGroup(sender, e, cmbChildAddGroup7)
        Dim _GrpId = Convert.ToString(cmbChildAddGroup7.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup8.Visible = False
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName8.Visible = False
            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup7.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup8, lblChildGroupName7)
    End Sub

    Private Sub cmbChildAddGroup8_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup8.TextChanged
        '      ChildGroup(sender, e, cmbChildAddGroup8)
        Dim _GrpId = Convert.ToString(cmbChildAddGroup8.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup9.Visible = False
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName9.Visible = False
            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup8.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup9, lblChildGroupName8)
    End Sub

    Private Sub cmbChildAddGroup9_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup9.TextChanged

        Dim _GrpId = Convert.ToString(cmbChildAddGroup9.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup10.Visible = False
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName10.Visible = False
            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup9.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup10, lblChildGroupName9)
    End Sub

    Private Sub cmbChildAddGroup10_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup10.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup10.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup11.Visible = False
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName11.Visible = False
            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup10.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup11, lblChildGroupName10)
    End Sub
    Private Sub cmbChildAddGroup11_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup11.TextChanged

        Dim _GrpId = Convert.ToString(cmbChildAddGroup11.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup12.Visible = False
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName12.Visible = False
            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup11.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup12, lblChildGroupName11)
    End Sub

    Private Sub cmbChildAddGroup12_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup12.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup12.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup13.Visible = False
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName13.Visible = False
            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup12.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup13, lblChildGroupName12)
    End Sub

    Private Sub cmbChildAddGroup13_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup13.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup13.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup14.Visible = False
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName14.Visible = False
            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup13.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup14, lblChildGroupName13)
    End Sub

    Private Sub cmbChildAddGroup14_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup14.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup14.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup15.Visible = False
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName15.Visible = False
            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup14.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup15, lblChildGroupName14)
    End Sub

    Private Sub cmbChildAddGroup15_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup15.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup15.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup16.Visible = False
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName16.Visible = False
            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup15.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup16, lblChildGroupName15)
    End Sub

    Private Sub cmbChildAddGroup16_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup16.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup16.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup17.Visible = False
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName17.Visible = False
            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup16.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup17, lblChildGroupName16)
    End Sub

    Private Sub cmbChildAddGroup17_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup17.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup17.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup18.Visible = False
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName18.Visible = False
            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup17.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup18, lblChildGroupName17)
    End Sub

    Private Sub cmbChildAddGroup18_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup18.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup18.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup19.Visible = False
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName19.Visible = False
            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup18.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup19, lblChildGroupName18)
    End Sub

    Private Sub cmbChildAddGroup19_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup19.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup19.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup20.Visible = False
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName20.Visible = False
            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup19.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup20, lblChildGroupName19)
    End Sub

    Private Sub cmbChildAddGroup20_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup20.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup20.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup21.Visible = False
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName21.Visible = False
            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup20.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup21, lblChildGroupName20)
    End Sub

    Private Sub cmbChildAddGroup21_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup21.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup21.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup22.Visible = False
            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName22.Visible = False
            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup21.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup22, lblChildGroupName21)
    End Sub

    Private Sub cmbChildAddGroup22_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup22.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup22.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup23.Visible = False
            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName23.Visible = False
            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup22.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup23, lblChildGroupName22)
    End Sub

    Private Sub cmbChildAddGroup23_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup23.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup23.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup24.Visible = False
            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName24.Visible = False
            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup23.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup24, lblChildGroupName23)
    End Sub

    Private Sub cmbChildAddGroup24_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup24.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup24.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup25.Visible = False
            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup24.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup25, lblChildGroupName24)
    End Sub

    Private Sub cmbChildAddGroup25_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup25.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup25.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup26.Visible = False
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName25.Visible = False
            lblChildGroupName26.Visible = False
            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup25.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup26, lblChildGroupName25)
    End Sub

    Private Sub cmbChildAddGroup26_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup26.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup26.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup27.Visible = False
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName27.Visible = False
            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup26.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup27, lblChildGroupName26)
    End Sub

    Private Sub cmbChildAddGroup27_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup27.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup27.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then
            cmbChildAddGroup28.Visible = False
            cmbChildAddGroup29.Visible = False

            lblChildGroupName28.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup27.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup28, lblChildGroupName27)
    End Sub

    Private Sub cmbChildAddGroup28_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup28.TextChanged
        Dim _GrpId = Convert.ToString(cmbChildAddGroup28.SelectedValue)
        If _GrpId = "" Or _GrpId = "0" Then

            cmbChildAddGroup29.Visible = False
            lblChildGroupName29.Visible = False
            Exit Sub
        End If
        GroupName = cmbChildAddGroup28.Text
        ChildGroup(_GrpId, sender, e, cmbChildAddGroup29, lblChildGroupName28)
    End Sub

    Private Sub cmbChildAddGroup29_TextChanged(sender As Object, e As EventArgs) Handles cmbChildAddGroup29.TextChanged
        '   ChildGroup(sender, e, cmbChildAddGroup29)
    End Sub
#End Region
    Private Sub rdBtnAddGroup_Click(sender As Object, e As EventArgs) Handles rdBtnAddGroup.Click, rdBtnViewGroupsOrArticles.Click, rdBtnAddArticle.Click
        CloseAndroidListBox()
        btnSave.Text = "Save"
        If sender.Text = "Add Group" AndAlso sender.checked = True Then
            DisplayAddGroups()
        ElseIf sender.Text = "Add Article" AndAlso sender.checked = True Then
            grpBxAddArticle.Visible = False
            DisplayAddArticles()
        Else
            DisplayViewArticles()
        End If
    End Sub
    Private Sub txtGroupName_Click(sender As Object, e As EventArgs) Handles txtGroupName.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
#End Region
#Region "------------------------------------------methods"
    'ddl main event
    Public Sub ChildGroup(ByVal _GrpId As String, ByRef sender As Object, ByRef e As EventArgs, ByRef ObjComboBox As ctrlCombo, ByRef ctrllbl As CtrlLabel)
        Try
            If _GrpId = "" Then
                Exit Sub
            End If
            Dim dt = BindGroupCategory(_GrpId)
            If dt.Rows.Count > 1 Then
                ParentGroupId = _GrpId
                ObjComboBox.Visible = True
                ctrllbl.Visible = True
                ctrllbl.Padding = New Padding(5, 0, 0, 0)
                BindDropDown(dt, ObjComboBox)
            Else
                ObjComboBox.Visible = False
                ctrllbl.Visible = False
                '  ParentGroupId = ""
                ParentGroupId = _GrpId
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'Public Sub DropDownVisibility(ByVal iCounter As Integer, ByVal value As Boolean)
    '    Dim _controls As Control = DirectCast(TblPanelChildGroups, Control)
    '    For Each ctrl As Control In _controls.Controls
    '        If ctrl.Name.Contains("lblChildGroupName") Or ctrl.Name.Contains("Panel") Then
    '            Continue For
    '        Else
    '            For index = 1 To iCounter
    '                Dim ctrlName = "cmbChildAddGroup" + index.ToString()
    '                If ctrl.Name = ctrlName Then
    '                    ctrl.Visible = value
    '                End If
    '            Next
    '        End If
    '    Next
    'End Sub
    Public Sub DropDownVisibility(ByVal isTrue As Boolean)
        cmbChildAddGroup1.Visible = False
        cmbChildAddGroup2.Visible = False
        cmbChildAddGroup3.Visible = False
        cmbChildAddGroup4.Visible = False
        cmbChildAddGroup5.Visible = False
        cmbChildAddGroup6.Visible = False
        cmbChildAddGroup7.Visible = False
        cmbChildAddGroup8.Visible = False
        cmbChildAddGroup9.Visible = False
        cmbChildAddGroup10.Visible = False
        cmbChildAddGroup11.Visible = False
        cmbChildAddGroup12.Visible = False
        cmbChildAddGroup13.Visible = False
        cmbChildAddGroup14.Visible = False
        cmbChildAddGroup15.Visible = False
        cmbChildAddGroup16.Visible = False
        cmbChildAddGroup17.Visible = False
        cmbChildAddGroup18.Visible = False
        cmbChildAddGroup19.Visible = False
        cmbChildAddGroup20.Visible = False
        cmbChildAddGroup21.Visible = False
        cmbChildAddGroup22.Visible = False
        cmbChildAddGroup23.Visible = False
        cmbChildAddGroup24.Visible = False
        cmbChildAddGroup25.Visible = False
        cmbChildAddGroup26.Visible = False
        cmbChildAddGroup27.Visible = False
        cmbChildAddGroup28.Visible = False
        cmbChildAddGroup29.Visible = False

        lblChildGroupName1.Visible = False
        lblChildGroupName1.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName2.Visible = False
        lblChildGroupName2.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName3.Visible = False
        lblChildGroupName3.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName4.Visible = False
        lblChildGroupName4.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName5.Visible = False
        lblChildGroupName5.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName6.Visible = False
        lblChildGroupName6.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName7.Visible = False
        lblChildGroupName7.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName8.Visible = False
        lblChildGroupName8.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName9.Visible = False
        lblChildGroupName9.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName10.Visible = False
        lblChildGroupName10.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName11.Visible = False
        lblChildGroupName11.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName12.Visible = False
        lblChildGroupName12.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName13.Visible = False
        lblChildGroupName13.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName14.Visible = False
        lblChildGroupName14.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName15.Visible = False
        lblChildGroupName15.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName16.Visible = False
        lblChildGroupName16.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName17.Visible = False
        lblChildGroupName17.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName18.Visible = False
        lblChildGroupName18.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName19.Visible = False
        lblChildGroupName19.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName20.Visible = False
        lblChildGroupName20.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName21.Visible = False
        lblChildGroupName21.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName22.Visible = False
        lblChildGroupName22.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName23.Visible = False
        lblChildGroupName23.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName24.Visible = False
        lblChildGroupName24.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName25.Visible = False
        lblChildGroupName25.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName26.Visible = False
        lblChildGroupName26.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName27.Visible = False
        lblChildGroupName27.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName28.Visible = False
        lblChildGroupName28.Padding = New Padding(5, 0, 0, 0)
        lblChildGroupName29.Visible = False
        lblChildGroupName29.Padding = New Padding(5, 0, 0, 0)


    End Sub
#End Region
#End Region
#Region "-----------------------------Add Group Articles"
#Region "------------------------------------------Events"
    Private Sub cmbGroupCategory_TextChanged(sender As Object, e As EventArgs) Handles cmbGroupCategory.TextChanged
        Dim _GrpId = Convert.ToString(cmbGroupCategory.SelectedValue)
        If _GrpId <> "" Or _GrpId <> "0" Then
            'CtrllblParentGroupHierarchyInfo.Text = 
            CtrllblParentGroupHierarchyInfo.Text = objCls.fetchParentHierarchyInfo("", _GrpId, clsAdmin.SiteCode)
            GetCurrentArticle(_GrpId)
            fpAddArticleSection.Visible = True
            grpBxAddArticle.Visible = True
            txtSearchPartial.Text = String.Empty


            lstPartialArticleRight.DataSource = Nothing
            lstPartialArticleLeft.DataSource = Nothing
            txtAndroidArticleSearchTextBox.Focus()
            'dtCurrentArticlePartialList.Clear()
            'dtArticlePartialListLeft.Clear()
            'dtArticlePartialListRight.Clear()
        Else
            CtrllblParentGroupHierarchyInfo.Text = ""
            fpAddArticleSection.Visible = False
        End If
    End Sub
    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        Try
            If lstPartialArticleLeft.Items.Count = 0 Then Exit Sub
            Dim dtArticlePartialListLeftTemp = dtArticlePartialListLeft.Clone

            For Li = 0 To lstPartialArticleLeft.SelectedItems.Count - 1
                Dim _ArticleCode = DirectCast(lstPartialArticleLeft.SelectedItems.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
                Dim exsist = dtArticlePartialListLeft.Select("ArticleCode='" + _ArticleCode + "'")
                If exsist.Length > 0 Then
                    Dim dr = dtArticlePartialListRight.NewRow()
                    dr("ArticleCode") = exsist(0)("ArticleCode")
                    dr("ArticleName") = exsist(0)("ArticleName")
                    dr("MaterialTypeCode") = exsist(0)("MaterialTypeCode")
                    dr("ArticalTypeCode") = exsist(0)("ArticalTypeCode")
                    dr("BaseUnitOfMeasure") = exsist(0)("BaseUnitOfMeasure")
                    dtArticlePartialListRight.Rows.Add(dr)
                End If
            Next
            For index = 0 To dtArticlePartialListRight.Rows.Count - 1
                Dim exsist = dtArticlePartialListLeft.Select("ArticleCode='" + dtArticlePartialListRight.Rows(index)("ArticleCode") + "'")
                If exsist.Length > 0 Then
                    Dim dtLTemp As New DataTable
                    dtLTemp = dtArticlePartialListLeft.Copy
                    For ind = 0 To dtLTemp.Rows.Count - 1
                        If dtLTemp.Rows(ind)("ArticleCode") = exsist(0)("ArticleCode") Then
                            dtArticlePartialListLeft.Rows.RemoveAt(ind)
                            dtArticlePartialListLeft.AcceptChanges()
                            Exit For
                        End If
                    Next
                End If
            Next
            'dtArticlePartialListRight = dtArticlePartialListLeftTemp
            'dtArticlePartialListLeft = dtArticlePartialListLeftTemp
            BindArticlesList(dtArticlePartialListLeft, dtArticlePartialListRight)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click
        Try
            If lstPartialArticleRight.Items.Count = 0 Then Exit Sub
            Dim dtArticlePartialListRightTemp = dtArticlePartialListRight.Clone
            For Li = 0 To lstPartialArticleRight.SelectedItems.Count - 1
                Dim _ArticleCode = DirectCast(lstPartialArticleRight.SelectedItems.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
                Dim exsist = dtArticlePartialListRight.Select("ArticleCode='" + _ArticleCode + "'")
                If exsist.Length > 0 Then
                    Dim dr = dtArticlePartialListLeft.NewRow()
                    dr("ArticleCode") = exsist(0)("ArticleCode")
                    dr("ArticleName") = exsist(0)("ArticleName")
                    dr("MaterialTypeCode") = exsist(0)("MaterialTypeCode")
                    dr("ArticalTypeCode") = exsist(0)("ArticalTypeCode")
                    dr("BaseUnitOfMeasure") = exsist(0)("BaseUnitOfMeasure")
                    dtArticlePartialListLeft.Rows.Add(dr)
                End If
            Next
            For index = 0 To dtArticlePartialListLeft.Rows.Count - 1
                Dim exsist = dtArticlePartialListRight.Select("ArticleCode='" + dtArticlePartialListLeft.Rows(index)("ArticleCode") + "'")
                If exsist.Length > 0 Then
                    Dim dtLTemp As New DataTable
                    dtLTemp = dtArticlePartialListRight.Copy
                    For ind = 0 To dtLTemp.Rows.Count - 1
                        If dtLTemp.Rows(ind)("ArticleCode") = exsist(0)("ArticleCode") Then
                            dtArticlePartialListRight.Rows.RemoveAt(ind)
                            dtArticlePartialListRight.AcceptChanges()
                            Exit For
                        End If
                    Next
                End If
            Next
            BindArticlesList(dtArticlePartialListLeft, dtArticlePartialListRight)



            'Dim dtArticlePartialListLeftTemp = dtArticlePartialListLeft.Clone

            'For Li = 0 To lstPartialArticleLeft.SelectedItems.Count - 1
            '    Dim _ArticleCode = DirectCast(lstPartialArticleLeft.SelectedItems.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
            '    Dim exsist = dtArticlePartialListLeft.Select("ArticleCode='" + _ArticleCode + "'")
            '    If exsist.Length > 0 Then
            '        Dim dr = dtArticlePartialListRight.NewRow()
            '        dr("ArticleCode") = exsist(0)("ArticleCode")
            '        dr("ArticleName") = exsist(0)("ArticleName")
            '        dr("MaterialTypeCode") = exsist(0)("MaterialTypeCode")
            '        dr("ArticalTypeCode") = exsist(0)("ArticalTypeCode")
            '        dr("BaseUnitOfMeasure") = exsist(0)("BaseUnitOfMeasure")
            '        dtArticlePartialListRight.Rows.Add(dr)
            '    End If
            'Next
            'For index = 0 To dtArticlePartialListRight.Rows.Count - 1
            '    Dim exsist = dtArticlePartialListLeft.Select("ArticleCode='" + dtArticlePartialListRight.Rows(index)("ArticleCode") + "'")
            '    If exsist.Length > 0 Then
            '        Dim dtLTemp As New DataTable
            '        dtLTemp = dtArticlePartialListLeft.Copy
            '        For ind = 0 To dtLTemp.Rows.Count - 1
            '            If dtLTemp.Rows(ind)("ArticleCode") = exsist(0)("ArticleCode") Then
            '                dtArticlePartialListLeft.Rows.RemoveAt(ind)
            '                dtArticlePartialListLeft.AcceptChanges()
            '                Exit For
            '            End If
            '        Next
            '    End If
            'Next
            'Dim dtArticlePartialListRightTemp = dtArticlePartialListRight.Copy
            'For Li = 0 To lstPartialArticleRight.SelectedItems.Count - 1
            '    Dim _ArticleCode = DirectCast(lstPartialArticleRight.SelectedItems.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
            '    If dtArticlePartialListRight.Rows.Count > 0 Then
            '        Dim result As DataRow = dtArticlePartialListRight.Select("articleCode='" + _ArticleCode + "'").FirstOrDefault
            '        If result.ItemArray.Count > 0 Then
            '            Dim innerResult = dtArticlePartialListLeft.Select("articleCode='" + _ArticleCode + "'")
            '            If innerResult.Length = 0 Then
            '                Dim drAdd As DataRow = dtArticlePartialListLeft.NewRow()
            '                drAdd("articleCode") = result("articleCode").ToString() 'result(0).ItemArray(0).ToString()
            '                drAdd("articleName") = result("articleName").ToString() 'result(0).ItemArray(1).ToString()
            '                drAdd("materialTypeCode") = result("materialTypeCode").ToString() 'result(0).ItemArray(2).ToString()
            '                drAdd("articalTypeCode") = result("articalTypeCode").ToString() 'result(0).ItemArray(3).ToString()
            '                drAdd("baseUnitOfMeasure") = result("baseUnitOfMeasure").ToString() 'result(0).ItemArray(4).ToString()
            '                dtArticlePartialListLeft.Rows.Add(drAdd)
            '            End If
            '        End If
            '    End If
            'Next
            'Dim count = dtArticlePartialListRight.Rows.Count - 1
            'For index = 0 To count - 1
            '    Dim dr As DataRow = dtArticlePartialListRight.Rows(index)
            '    Dim result = dtArticlePartialListLeft.Select("articleCode='" + dr("ArticleCode") + "'")
            '    If result.Length > 0 Then
            '        If result(0).ItemArray(0).ToString() = dr("ArticleCode") Then
            '            count = count - 1
            '            If count > 0 Then
            '                dr.Delete()
            '                dtArticlePartialListRight.AcceptChanges()
            '            End If
            '        End If
            '    End If
            'Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnRightAll_Click(sender As Object, e As EventArgs) Handles btnRightAll.Click
        Try
            If lstPartialArticleLeft.Items.Count = 0 Then Exit Sub
            Dim dtArticlePartialListLeftTemp = dtArticlePartialListLeft.Copy
            For Li = 0 To lstPartialArticleLeft.Items.Count - 1
                Dim _ArticleCode = DirectCast(lstPartialArticleLeft.Items.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
                If dtArticlePartialListLeft.Rows.Count > 0 Then
                    Dim result As DataRow = dtArticlePartialListLeft.Select("articleCode='" + _ArticleCode + "'").FirstOrDefault
                    If result.ItemArray.Count > 0 Then
                        Dim innerResult = dtArticlePartialListRight.Select("articleCode='" + _ArticleCode + "'")
                        If innerResult.Length = 0 Then
                            Dim drAdd As DataRow = dtArticlePartialListRight.NewRow()
                            drAdd("articleCode") = result("articleCode").ToString() 'result(0).ItemArray(0).ToString()
                            drAdd("articleName") = result("articleName").ToString() 'result(0).ItemArray(1).ToString()
                            drAdd("materialTypeCode") = result("materialTypeCode").ToString() 'result(0).ItemArray(2).ToString()
                            drAdd("articalTypeCode") = result("articalTypeCode").ToString() 'result(0).ItemArray(3).ToString()
                            drAdd("baseUnitOfMeasure") = result("baseUnitOfMeasure").ToString() 'result(0).ItemArray(4).ToString()
                            dtArticlePartialListRight.Rows.Add(drAdd)
                        End If
                    End If
                End If
            Next
            dtArticlePartialListLeft.Clear()
            BindArticlesList(dtArticlePartialListLeft, dtArticlePartialListRight)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnLeftAll_Click(sender As Object, e As EventArgs) Handles btnLeftAll.Click
        Try
            If lstPartialArticleRight.Items.Count = 0 Then Exit Sub
            Dim dtArticlePartialListRightTemp = dtArticlePartialListRight.Copy
            For Li = 0 To lstPartialArticleRight.Items.Count - 1
                Dim _ArticleCode = DirectCast(lstPartialArticleRight.Items.Item(Li), System.Data.DataRowView).Row.ItemArray(0)
                If dtArticlePartialListRight.Rows.Count > 0 Then
                    Dim result As DataRow = dtArticlePartialListRight.Select("articleCode='" + _ArticleCode + "'").FirstOrDefault
                    If result.ItemArray.Count > 0 Then
                        Dim innerResult = dtArticlePartialListLeft.Select("articleCode='" + _ArticleCode + "'")
                        If innerResult.Length = 0 Then
                            Dim drAdd As DataRow = dtArticlePartialListLeft.NewRow()
                            drAdd("articleCode") = result("articleCode").ToString() 'result(0).ItemArray(0).ToString()
                            drAdd("articleName") = result("articleName").ToString() 'result(0).ItemArray(1).ToString()
                            drAdd("materialTypeCode") = result("materialTypeCode").ToString() 'result(0).ItemArray(2).ToString()
                            drAdd("articalTypeCode") = result("articalTypeCode").ToString() 'result(0).ItemArray(3).ToString()
                            drAdd("baseUnitOfMeasure") = result("baseUnitOfMeasure").ToString() 'result(0).ItemArray(4).ToString()
                            dtArticlePartialListLeft.Rows.Add(drAdd)
                        End If
                    End If
                End If
            Next
            dtArticlePartialListRight.Clear()
            BindArticlesList(dtArticlePartialListLeft, dtArticlePartialListRight)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnArticleAddAndroid_Click(sender As Object, e As EventArgs) Handles btnArticleAddAndroid.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0
            If txtAndroidArticleSearchTextBox.Text.Length = 0 Then
                txtAndroidArticleSearchTextBox.Select()
                txtAndroidArticleSearchTextBox.Focus()
                Exit Sub
            End If
            If (txtAndroidArticleSearchTextBox.Text <> String.Empty) Then
                Dim dt As New DataTable
                dt = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, txtAndroidArticleSearchTextBox.Text.Trim, clsAdmin.LangCode, True, CallFromPostab:=True)
                Dim dtitem = objCM.GetItemDetails(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"), False, clsAdmin.LangCode)
                If dtitem.Rows.Count > 0 Then
                    If Val(dtitem.Rows(0)("SELLINGPRICE")) = 0 Or Val(dtitem.Rows(0)("MRP")) = 0 Or Val(dtitem.Rows(0)("CostPrice")) = 0 Then
                        ShowMessage("Item is not available", getValueByKey("CLAE04"))
                        txtAndroidArticleSearchTextBox.Clear()
                        Exit Sub
                    End If
                    Dim ItemDesc As String = String.Empty
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        AddToCurrentArticle(dt)
                        grdFinalGridSetting()
                        txtAndroidArticleSearchTextBox.Text = String.Empty
                        txtAndroidArticleSearchTextBox.Focus()
                        txtAndroidArticleSearchTextBox.Select()
                        btnSave.Enabled = True
                    End If
                Else
                    ShowMessage("Item is not available", getValueByKey("CLAE04"))
                    txtAndroidArticleSearchTextBox.Clear()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - exc " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
            'txtSearch.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Focus()
        End Try
    End Sub
    Private Sub btnArticleAddPartialSearch_Click(sender As Object, e As EventArgs) Handles btnArticleAddPartialSearch.Click
        Try
            If dtArticlePartialListRight.Rows.Count > 0 And lstPartialArticleRight.Items.Count > 0 Then
                For Each dr As DataRow In dtArticlePartialListRight.Rows
                    Dim Result = dtCurrentArticle.Select("articleCode='" + dr("articleCode") + "'")
                    If Result.Length = 0 Then
                        Dim drSet As DataRow = dtCurrentArticle.NewRow()
                        drSet("articleCode") = dr("articleCode")
                        drSet("articleName") = dr("articleName")
                        drSet("ArticleCodeName") = dr("articleCode") + "==>" + dr("articleName")
                        drSet("AdditonalInfo") = dr("articalTypeCode") + " " + dr("BaseUnitOfMeasure") + " " + dr("materialTypeCode")
                        drSet("Delete") = False
                        dtCurrentArticle.Rows.Add(drSet)
                    End If
                Next
                grdFinalGridSetting()
                'lstPartialArticleRight.DataSource = Nothing
                ShowMessage("New articles added to the list", "Information")
                btnSave.Enabled = True

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnPartialSearch_Click(sender As Object, e As EventArgs) Handles btnPartialSearch.Click
        Try
            Dim txt = txtSearchPartial.Text.Trim()
            dtArticlePartialList = objCls.FetchPartialArticles(clsAdmin.SiteCode, txt)
            dtArticlePartialListLeft = dtArticlePartialList.Copy
            dtArticlePartialListRight = dtArticlePartialListLeft.Clone
            dtCurrentArticlePartialList = dtArticlePartialListLeft.Clone
            If dtArticlePartialListLeft.Rows.Count > 0 Then
                lstPartialArticleRight.DataSource = Nothing
                lstPartialArticleLeft.DataSource = Nothing
                lstPartialArticleLeft.DataSource = dtArticlePartialListLeft
                lstPartialArticleLeft.DisplayMember = "articleName"
                lstPartialArticleLeft.ValueMember = "articleName"
                lstPartialArticleLeft.SelectionMode = SelectionMode.MultiExtended
                lstPartialArticleRight.SelectionMode = SelectionMode.MultiExtended
            Else
                lstPartialArticleLeft.DataSource = Nothing
                lstPartialArticleRight.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnArticleAddSelectedCategory_Click(sender As Object, e As EventArgs) Handles btnArticleAddSelectedCategory.Click
        Try
            Dim dtt As DataTable = dgCategoryArticles.DataSource
            For Each dr As DataRow In dtt.Rows
                Dim Result = dtCurrentArticle.Select("articleCode='" + dr("articleCode") + "'")
                If Result.Length = 0 Then
                    Dim drSet As DataRow = dtCurrentArticle.NewRow()
                    drSet("articleCode") = dr("articleCode")
                    drSet("articleName") = dr("articleName")
                    drSet("ArticleCodeName") = dr("ArticleCodeName")
                    drSet("AdditonalInfo") = dr("AdditonalInfo")
                    drSet("Delete") = dr("Delete")
                    dtCurrentArticle.Rows.Add(drSet)
                End If
            Next
            grdFinalGridSetting()
            dgCategoryArticles.Visible = False
            dgCategoryArticles.DataSource = Nothing
            dtCurrentArticleCategory.Clear()
            dgCategoryArticles.DataSource = dtCurrentArticleCategory
            ShowMessage("New Articles added to List", "Infomarion")
            btnArticleAddSelectedCategory.Visible = False
            btnSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgFinalArticle_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs)
        Dim check = dgFinalArticle.Item(dgFinalArticle.Row, "Delete")
        'Dim res = dgFinalArticle.GetCellCheck(e.Row, e.Col).Checked

    End Sub
    Private Sub trvArticleCategory_DoubleClick(sender As Object, e As EventArgs) Handles trvArticleCategory.DoubleClick
        Try
            If trvArticleCategory.SelectedNode IsNot Nothing Then
                Dim LastNodeCode = ""
                Dim selectedNode = Convert.ToString(trvArticleCategory.SelectedNode.Name)
                Dim rootNode = Convert.ToString(trvArticleCategory.SelectedNode.Tag)
                If selectedNode <> rootNode Then
                    If selectedNode.ToUpper() <> "ROOT" Or rootNode <> Nothing Or rootNode <> "" Then
                        If selectedNode <> "" Then
                            LastNodeCode = selectedNode
                        Else
                            LastNodeCode = rootNode
                        End If
                        Dim dtArticles = objCls.FetchCetegoryWiseArticle(LastNodeCode)
                        If dtArticles.Rows.Count > 0 Then
                            dtCurrentArticleCategory = dtCurrentArticle.Clone
                            For Each dr As DataRow In dtArticles.Rows
                                Dim drSet As DataRow = dtCurrentArticleCategory.NewRow
                                drSet("articleCode") = dr("articleCode")
                                drSet("articleName") = dr("articleName")
                                drSet("ArticleCodeName") = dr("ArticleCodeName")
                                drSet("AdditonalInfo") = dr("AdditonalInfo")
                                drSet("Delete") = dr("Delete")
                                dtCurrentArticleCategory.Rows.Add(drSet)
                            Next
                            grdFinalGridSetting(True)
                            btnArticleAddSelectedCategory.Visible = True
                        Else
                            btnArticleAddSelectedCategory.Visible = False
                            dgCategoryArticles.Visible = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0
            If e.KeyCode = Keys.Delete AndAlso txtAndroidArticleSearchTextBox.Text.Length = 0 Then

                sender.Select()
                sender.Focus()
                Exit Sub
            End If
            If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
                Dim dt As New DataTable
                dt = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, sender.Text.Trim, clsAdmin.LangCode, True)
                If dt.Rows.Count > 0 Then
                    Dim dtitem = objCM.GetItemDetails(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"), False, clsAdmin.LangCode)
                    If dtitem.Rows.Count > 0 Then
                        If Val(dtitem.Rows(0)("SELLINGPRICE")) = 0 Or Val(dtitem.Rows(0)("MRP")) = 0 Or Val(dtitem.Rows(0)("CostPrice")) = 0 Then
                            ShowMessage("Item is not available", getValueByKey("CLAE04"))
                            txtAndroidArticleSearchTextBox.Clear()
                            Exit Sub
                        End If
                        Dim ItemDesc As String = String.Empty
                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            AddToCurrentArticle(dt)
                            grdFinalGridSetting()
                            sender.Text = String.Empty
                            sender.Focus()
                            sender.Select()
                            btnSave.Enabled = True
                        End If
                    Else
                        ShowMessage("Item is not available", getValueByKey("CLAE04"))
                        txtAndroidArticleSearchTextBox.Clear()
                        Exit Sub
                    End If
                End If

            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - exc " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
            'txtSearch.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Focus()
        End Try

    End Sub
    Private Sub txtAndroidArticleSearchTextBox_Click(sender As Object, e As EventArgs) Handles txtAndroidArticleSearchTextBox.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Private Sub txtSearchPartial_Click(sender As Object, e As EventArgs) Handles txtSearchPartial.Click
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
#End Region
#Region "------------------------------------------methods"
    Public Sub GetCurrentArticle(ByVal selectedGroupId As String)
        Try
            dtCurrentArticle.Clear()
            Dim dt = objCls.FetchCurrentArticles(clsAdmin.SiteCode, selectedGroupId)
            If dt.Rows.Count > 0 Then
                dtCurrentArticle = dt.Copy
                'grdFinalArticles.DataBindings()
                grdFinalGridSetting()
                btnSave.Enabled = True
            Else
                dgFinalArticle.DataSource = Nothing
                dgFinalArticle.Visible = False
                btnSave.Enabled = False
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub AddToCurrentArticle(ByVal dt As DataTable)
        Try
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim result = dtCurrentArticle.Select("ArticleCode='" + dr("ArticleCode") + "'")
                    If result.Length = 0 Then
                        Dim drAdd As DataRow = dtCurrentArticle.NewRow
                        Dim ArticleCodeName As String = ""
                        Dim AdditonalInfo As String = ""
                        Dim Delete As Boolean = False
                        drAdd("ArticleCode") = dr("ArticleCode")
                        If dt.Columns.Contains("ArticleCodeName") Then
                            drAdd("ArticleName") = dr("ArticleCodeName")
                            ArticleCodeName = dr("ArticleCode") + "==>" + dr("ArticleCodeName")
                        ElseIf dt.Columns.Contains("ArticleShortName") Then
                            drAdd("ArticleName") = dr("ArticleShortName")
                            ArticleCodeName = dr("ArticleCode") + "==>" + dr("ArticleShortName")
                        End If
                        If dt.Columns.Contains("AdditonalInfo") Then
                            AdditonalInfo = dr("AdditonalInfo")
                        ElseIf dt.Columns.Contains("baseUnitofMeasure") Then
                            AdditonalInfo = dr("baseUnitofMeasure")
                        End If
                        If dt.Columns.Contains("Delete") Then
                            Delete = dr("Delete")
                        Else
                            Delete = False
                        End If
                        drAdd("ArticleCodeName") = ArticleCodeName
                        drAdd("AdditonalInfo") = AdditonalInfo
                        drAdd("Delete") = Delete
                        dtCurrentArticle.Rows.Add(drAdd)
                    Else
                        Continue For
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub LoadArticleCategoryTree()
        Try
            'trvArticleCategory.Nodes.Clear()
            'trvArticleCategory.Nodes.Add("root", getValueByKey("ih001"))
            'dtArticleCategoryTree = objItem.getarticleTrees(clsAdmin.SiteCode)
            'dtTree = objItem.GetArticleTree()
            'If Not dtArticleCategoryTree Is Nothing AndAlso Not dtTree Is Nothing Then
            '    For Each dr As DataRow In dtArticleCategoryTree.Rows
            '        Dim Node As New TreeNode(dr("treename").ToString())
            '        Node.Name = dr("treecode")
            '        Node.Tag = dr("treecode")
            '        'Node.Parent = ""
            '        trvArticleCategory.Nodes("root").Nodes.Add(Node)
            '        AddTreeNodes(dtTree, dr("Treecode"), Node)
            '    Next
            'End If
            trvArticleCategory.Nodes.Clear()
            dtArticleCategoryTree = objItem.getarticleTrees(clsAdmin.SiteCode)
            Dim node = dtArticleCategoryTree.Select("treeCode not is null").ToList
            dtTree = objItem.GetArticleTreeForPosTab()
            ItemHierarchyList = dtTree.Copy
            If Not dtArticleCategoryTree Is Nothing AndAlso Not dtTree Is Nothing Then
                Dim rootNodeNode As New TreeNode
                rootNodeNode.Text = "Root"
                rootNodeNode.Tag = "Root"
                rootNodeNode.SelectedImageIndex = 2
                For Each dr As DataRow In node
                    Dim _parentNode As New TreeNode()
                    _parentNode.Text = dr("TreeName").ToString()
                    _parentNode.Tag = dr("TreeCode").ToString()
                    _parentNode.SelectedImageIndex = 2

                    PopulateTreeView(dr("TreeCode").ToString(), _parentNode)
                    rootNodeNode.Nodes.Add(_parentNode)
                Next
                trvArticleCategory.Nodes.Add(rootNodeNode)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub PopulateTreeView(ByVal parentId As String, ByRef parentNode As TreeNode)

        If ItemHierarchyList.Select("ParentNodecode='" + parentId + "'").Length > 0 Then
            Dim childNodes = ItemHierarchyList.Select("ParentNodecode='" + parentId + "'").CopyToDataTable

            For Each _row As DataRow In childNodes.Rows

                Dim childNode As New TreeNode()
                If String.IsNullOrEmpty(parentId) Then
                    Exit Sub
                Else
                    childNode.Text = _row("NodeName").ToString()
                    childNode.Tag = _row("NODECODE").ToString()
                    If _row("IsThisLastNode") <> True Then
                        childNode.ImageIndex = 0
                        childNode.SelectedImageIndex = 2
                    Else
                        childNode.SelectedImageIndex = 1
                        childNode.ImageIndex = 1
                    End If
                    PopulateTreeView(_row("NODECODE").ToString(), childNode)
                End If
                parentNode.Nodes.Add(childNode)
            Next
            'Else
            'parentNode.Nodes.Add(childNode)
        End If
    End Sub
    Private Sub AddTreeNodes(ByVal dt As DataTable, ByVal Treecode As String, ByRef nodes As TreeNode)
        Dim dv As New DataView(dt, "isnull(ParentNodeCode,'')='' And TreeCode='" & Treecode & "'", "NodeCode", DataViewRowState.CurrentRows)
        For Each row As DataRowView In dv
            Dim Node As New TreeNode(row("NodeName"))
            Node.Name = row("NodeCode")
            Node.Tag = 1
            nodes.Nodes.Add(Node) '.Nodes.Add(Node)
            AddTreeChildNodes(dt, Node, 2, Treecode)
        Next
    End Sub
    Private Sub AddTreeChildNodes(ByVal dt As DataTable, ByRef nod As TreeNode, ByVal Level As Int16, ByVal treecode As String)
        Dim dv1 As New DataView(dt, "isnull(ParentNodeCode,'')='" & nod.Name & "' And TreeCode='" & treecode & "'", "NodeCode", DataViewRowState.CurrentRows)
        For Each row As DataRowView In dv1
            Dim Node As New TreeNode(row("NodeName"))
            Node.Name = row("NodeCode")
            'Node.Tag = row("NodeCode")
            Node.Tag = Level
            nod.Nodes.Add(Node)
            If row("IsThisLastNode") = True Then
                'Exit For
            Else
                AddTreeChildNodes(dt, Node, Level + 1, treecode)
            End If
        Next
    End Sub
    Public Sub BindArticlesList(ByVal dtLeftArticles As DataTable, ByVal dtRightArticles As DataTable)
        lstPartialArticleLeft.DataSource = Nothing
        lstPartialArticleLeft.DataSource = dtLeftArticles
        lstPartialArticleLeft.DisplayMember = "articleName"
        lstPartialArticleLeft.ValueMember = "articleName"

        lstPartialArticleRight.DataSource = Nothing
        lstPartialArticleRight.DataSource = dtRightArticles
        lstPartialArticleRight.DisplayMember = "articleName"
        lstPartialArticleRight.ValueMember = "articleName"
    End Sub
#End Region
#End Region
#Region "-----------------------------View Group Articles"
    'added by khusrao adil on 09-05-2018
    '' in this section tabbutton groups are not in use any more instead of that doct tab control 
    'is used but for future requiredment or  reference  prospective code is not deleted
#Region "------------------------------------------Events"
    Public Sub txtGroupNameTextValue_Click(sender As System.Object, e As System.EventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub
    Public Sub txtGroupNameTextValue_KeyDown(sender As System.Object, e As System.EventArgs)
        'If txtGroupNameTextValue.Text <> txtGroupNameTextValue.Tag Then
        txtGroupNameTextValue.Tag = txtGroupNameTextValue.Text
        '        End If
    End Sub
    Public Sub btnDeleteViewArticle_Click(sender As Object, e As EventArgs)
        Try
            Dim parent As Control = DirectCast(sender, Control).Parent
            Dim lbl1 As Control = DirectCast(parent, Control).Parent
            Dim lbl2 As Control = DirectCast(lbl1, Control).Parent

            Dim artId = parent.Controls("lblArticleCode").Text.Trim
            Dim grpId = sender.Tag.ToString()
            '
            Dim exist = dtDeleteViewArticles.Select("ArticleCode='" + artId + "' and GroupId='" + grpId + "' and isDelete=1")
            If exist.Length > 0 Then
                For Each dr As DataRow In dtDeleteViewArticles.Rows
                    If exist(0)("ArticleCode") = dr("ArticleCode") AndAlso exist(0)("GroupId") = dr("GroupId") Then
                        dtDeleteViewArticles.Rows.Remove(dr)
                        Exit For
                    End If
                Next

                'For Each ctrl As Control In lbl2.Controls
                '    If ctrl.Text <> "X" Then
                '        parent.BackColor = Color.White
                '        ctrl.BackColor = Color.White
                '        'ctrl.BackColor = Color.FromArgb(240, 240, 240)
                '    Else
                '        ctrl.BackColor = Color.FromArgb(0, 107, 163)
                '    End If
                'Next
                lbl1.BackColor = Color.FromArgb(240, 240, 240)
            Else
                Dim dr = dtDeleteViewArticles.NewRow()
                dr("ArticleCode") = artId
                dr("GroupId") = grpId
                dr("isDelete") = 1
                dtDeleteViewArticles.Rows.Add(dr)
                'parent.BackColor = Color.LightGoldenrodYellow
                'For Each ctrl As Control In parent.Controls
                '    ctrl.BackColor = Color.LightGoldenrodYellow
                'Next
                lbl1.BackColor = Color.LightGoldenrodYellow
            End If
            If dtDeleteViewArticles.Rows.Count > 0 Then
                btnSave.Text = "Update"
                btnSave.Enabled = True
            Else
                btnSave.Text = "Save"
                btnSave.Enabled = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub btnGroupNameUpdate_Click(sender As Object, e As EventArgs)
        Try
            Dim strQuery As New StringBuilder
            Dim grpId = sender.Tag.ToString()
            Dim grpName = txtGroupNameTextValue.Text
            strQuery.Length = 0
            strQuery.Append(" update BUTTONGROUP set   GroupName='" + grpName + "' ,UPDATEDAT='" + clsAdmin.SiteCode + "', ")
            strQuery.Append(" UPDATEDBY='" + clsAdmin.UserCode + "',UPDATEDON=GETDATE() where GROUPID='" + grpId + "' and SiteCode in ('" + clsAdmin.SiteCode + "','CCE') ")
            If ViewArticleActions(strQuery.ToString()) Then
                ShowMessage("Group Updated Successfully", "Information")
                DisplayViewArticles()
            Else
                ShowMessage("Group Update Failed", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub btnGroupDelete_Click(sender As Object, e As EventArgs)

        Dim grpId = sender.Tag.ToString()
        Dim str = objCls.GetButtonGroupAndChildForDelete(grpId)
        str = str.Remove(str.Length - 1)
        Dim grpIds() As String = str.Split(",")
        Dim strQuery As New StringBuilder
        strQuery.Length = 0
        strQuery.Append(" update  BUTTONGROUP set STATUS=0,ISACTIVE=0,UPDATEDAT='" + clsAdmin.SiteCode + "',UPDATEDBY='" + clsAdmin.UserCode + "',")
        strQuery.Append(" UPDATEDON=GETDATE() where SiteCode in ('CCE','" + clsAdmin.SiteCode + "') and GROUPID in (")
        Dim listItemCount = grpIds.Count - 1
        For index = 0 To grpIds.Count - 1
            If listItemCount <> index Then
                strQuery.Append("'" + grpIds(index) + "',")
            Else
                strQuery.Append("'" + grpIds(index) + "'")
            End If
        Next
        strQuery.Append(")")

        If ViewArticleActions(strQuery.ToString()) Then
            ShowMessage("Group Deleted Successfully", "Information")
            rdBtnAddGroup_Click(sender, Nothing)
        Else
            ShowMessage("Group Deleted Failed", "Information")
        End If
    End Sub

    Private Sub fpAddArticleSection_Scroll(sender As Object, e As ScrollEventArgs)
        If txtAndroidArticleSearchTextBox.Text <> "" Then
            CloseAndroidListBox()
        End If
    End Sub
#End Region
#Region "------------------------------------------method"
    'Public Function ButtonGroupsActionSection(ByRef fpTabArticles As FlowLayoutPanel, ByVal tabGroupId As String, ByVal tabGroupName As String) As Boolean
    Public Function ButtonGroupsActionSection(ByRef fpTabArticles As TableLayoutPanel, ByVal tabGroupId As String, ByVal tabGroupName As String) As Boolean

        lblGrpNameHeading = New CtrlLabel
        txtGroupNameTextValue = New CtrlTextBox
        txtGroupNameTextValue.Name = tabGroupId
        btnGroupNameUpdate = New CtrlBtn
        btnGroupDelete = New CtrlBtn
        btnGroupNameUpdate.Text = "Update Group"
        btnGroupNameUpdate.Tag = tabGroupId
        btnGroupNameUpdate.SetArticleCode = tabGroupName
        btnGroupNameUpdate.BackColor = Color.FromArgb(0, 107, 163)
        btnGroupNameUpdate.ForeColor = Color.FromArgb(255, 255, 255)
        btnGroupNameUpdate.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnGroupNameUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        btnGroupNameUpdate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnGroupNameUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnGroupNameUpdate.FlatStyle = FlatStyle.Flat
        btnGroupNameUpdate.FlatAppearance.BorderSize = 0
        btnGroupNameUpdate.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        'btnGroupNameUpdate.Location = New Point(OuterShellPoints.X + 340, OuterShellPoints.Y)
        btnGroupNameUpdate.Size = New Size(120, 25)

        Dim p As Drawing2D.GraphicsPath
        p = New Drawing2D.GraphicsPath
        'p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        p.AddLine(10, 0, btnGroupNameUpdate.Width - 10, 0)
        p.AddArc(New Rectangle(btnGroupNameUpdate.Width - 10, 0, 10, 10), -90, 90)
        p.AddLine(btnGroupNameUpdate.Width, 10, btnGroupNameUpdate.Width, btnGroupNameUpdate.Height - 10)
        p.AddArc(New Rectangle(btnGroupNameUpdate.Width - 10, btnGroupNameUpdate.Height - 10, 10, 10), 0, 90)
        p.AddLine(btnGroupNameUpdate.Width - 10, btnGroupNameUpdate.Height, 10, btnGroupNameUpdate.Height)
        p.AddArc(New Rectangle(0, btnGroupNameUpdate.Height - 10, 10, 10), 90, 90)
        p.CloseFigure()
        btnGroupNameUpdate.Region = New Region(p)

        btnGroupDelete.Text = "Delete Group"
        btnGroupDelete.Tag = tabGroupId
        btnGroupDelete.BackColor = Color.FromArgb(0, 107, 163)
        btnGroupDelete.ForeColor = Color.FromArgb(255, 255, 255)
        btnGroupDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnGroupDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        btnGroupDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnGroupDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnGroupDelete.FlatStyle = FlatStyle.Flat
        btnGroupDelete.FlatAppearance.BorderSize = 0
        btnGroupDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnGroupDelete.Location = New Point(OuterShellPoints.X + 470, OuterShellPoints.Y)
        btnGroupDelete.Size = New Size(120, 25)

        p = New Drawing2D.GraphicsPath
        ' p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        p.AddLine(10, 0, btnGroupDelete.Width - 10, 0)
        p.AddArc(New Rectangle(btnGroupDelete.Width - 10, 0, 10, 10), -90, 90)
        p.AddLine(btnGroupDelete.Width, 10, btnGroupDelete.Width, btnGroupDelete.Height - 10)
        p.AddArc(New Rectangle(btnGroupDelete.Width - 10, btnGroupDelete.Height - 10, 10, 10), 0, 90)
        p.AddLine(btnGroupDelete.Width - 10, btnGroupDelete.Height, 10, btnGroupDelete.Height)
        p.AddArc(New Rectangle(0, btnGroupDelete.Height - 10, 10, 10), 90, 90)
        p.CloseFigure()
        btnGroupDelete.Region = New Region(p)

        lblGrpNameHeading.Text = "Group Name:"
        'lblGrpNameHeading.Size = New Size(110, 23)
        'lblGrpNameHeading.Location = OuterShellPoints
        lblGrpNameHeading.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblGrpNameHeading.BackColor = Color.Transparent
        lblGrpNameHeading.BorderColor = Color.Transparent
        lblGrpNameHeading.BorderStyle = BorderStyle.None
        txtGroupNameTextValue.TextDetached = True
        txtGroupNameTextValue.Text = tabGroupName
        txtGroupNameTextValue.Tag = 1
        txtGroupNameTextValue.Size = New Size(200, 25)
        'txtGroupNameTextValue.Location = New Point(OuterShellPoints.X + 120, OuterShellPoints.Y)


        pnlGroupAction = New TableLayoutPanel
        pnlGroupAction.RowCount = 1
        pnlGroupAction.ColumnCount = 5
        'pnlGroupAction.Size = New Size(1000, 50)

        pnlGroupAction.BackColor = Color.Transparent
        'pnlGroupAction.Location = New Point(50, 50)
        pnlGroupAction.Tag = tabGroupId

        pnlGroupAction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15))
        pnlGroupAction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20))
        pnlGroupAction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15))
        pnlGroupAction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15))
        pnlGroupAction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35))
        pnlGroupAction.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100))

        'lblGrpNameHeading.Dock = DockStyle.Left
        'txtGroupNameTextValue.Dock = DockStyle.Left
        'btnGroupNameUpdate.Dock = DockStyle.Left
        'btnGroupDelete.Dock = DockStyle.Left
        Dim grpBxGroupAction = New GroupBox
        'grpBxGroupAction.Name = ""
        grpBxGroupAction.Tag = tabGroupId
        grpBxGroupAction.Text = tabGroupName
        'grpBxGroupAction.Size = New Size(900, 60)

        'grpBxGroupAction.Location = New Point(50, 50)
        'grpBxGroupAction.Controls.Add(pnlGroupAction)
        'pnlGroupAction.Dock = DockStyle.Fill
        'pnlGroupAction.BackColor = Color.LightGray
        'pnlGroupAction.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        'fpTabArticles.Controls.Add(grpBxGroupAction)
        fpTabArticles.Controls.Add(grpBxGroupAction, 0, 0)
        grpBxGroupAction.Dock = DockStyle.Fill
        grpBxGroupAction.Controls.Add(pnlGroupAction)
        pnlGroupAction.Dock = DockStyle.Fill
        ' pnlGroupAction.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        pnlGroupAction.Controls.Add(lblGrpNameHeading, 0, 0)
        pnlGroupAction.Controls.Add(txtGroupNameTextValue, 1, 0)
        pnlGroupAction.Controls.Add(btnGroupNameUpdate, 2, 0)
        pnlGroupAction.Controls.Add(btnGroupDelete, 3, 0)


        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.btnGroupDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.btnGroupDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            Me.btnGroupDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            Me.btnGroupDelete.FlatStyle = FlatStyle.Flat
            Me.btnGroupDelete.FlatAppearance.BorderSize = 0
            Me.btnGroupDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

            Me.btnGroupNameUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.btnGroupNameUpdate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            Me.btnGroupNameUpdate.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            Me.btnGroupNameUpdate.FlatStyle = FlatStyle.Flat
            Me.btnGroupNameUpdate.FlatAppearance.BorderSize = 0
            Me.btnGroupNameUpdate.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        End If
        AddHandler btnGroupNameUpdate.Click, AddressOf btnGroupNameUpdate_Click
        AddHandler btnGroupDelete.Click, AddressOf btnGroupDelete_Click
        AddHandler txtGroupNameTextValue.KeyDown, AddressOf txtGroupNameTextValue_KeyDown
        AddHandler txtGroupNameTextValue.Click, AddressOf txtGroupNameTextValue_Click
    End Function
    Public Function BindGroupCategory(Optional ParentGroupId As String = "", Optional CallFromAddArticle As Boolean = False, Optional CallFromViewArticles As Boolean = False) As DataTable
        Try
            If CallFromViewArticles AndAlso ParentGroupId = "" Then
                BindGroupCategory = objCls.GetButtonGroupsViewArticles(Sitecode:=clsAdmin.SiteCode)
                Return BindGroupCategory
            End If
            If ParentGroupId <> "" Then
                BindGroupCategory = objCls.GetButtonGroupByparentId(parentGroupID:=ParentGroupId, Sitecode:=clsAdmin.SiteCode, callFromViewArticles:=CallFromViewArticles)
                Return BindGroupCategory
            End If
            If ParentGroupId = "" Then
                If CallFromAddArticle = True Then
                    BindGroupCategory = objCls.GetButtonGroupForAddArticles(Sitecode:=clsAdmin.SiteCode)
                    Return BindGroupCategory
                Else
                    BindGroupCategory = objCls.GetButtonGroups(Sitecode:=clsAdmin.SiteCode)
                    Return BindGroupCategory
                End If
            End If
            '           BindGroupCategory = objCls.GetButtonGroup(parentGroupID:=ParentGroupId, Sitecode:=clsAdmin.SiteCode, CallAddGroups:=CallAddGroups, isViewArticleCall:=isViewArticleCall)


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function ViewArticleActions(ByVal strQuery As String)
        Try
            Return objCls.InsertOrUpdateRecord(strQuery)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#End Region

#Region "----------------------------Doc tab control"
#Region "------------------------------------------Events"
    Dim ArticleHirarchyLevel As Integer = 1
    Dim ArticleHirarchyTabLevel As Integer = 1
    Dim PreviousArticleHirarchyLevel As Integer = 0
    Dim PreviousArticleHirarchyTabLevel As Integer = 0
    Private Sub CtrlDocButtonTabGroup_Click(sender As Object, e As EventArgs) Handles CtrlDocButtonTabGroup.Click
        'If TabCategoryHirarchyLevel = 1 Then
        '    If fpTabCategoryDecreasePanelHeight <> 0 Then
        '        TabCategoryHirarchyLevel = TabCategoryHirarchyLevel + 1
        '        TabCategoryHirarchysequence = 1
        '    End If
        'ElseIf TabCategoryHirarchyLevel = 2 Then
        '    TabCategoryHirarchyLevel = TabCategoryHirarchyLevel - 1
        '    TabCategoryHirarchysequence = 1
        'ElseIf TabCategoryHirarchyLevel = 3 Then
        '    TabCategoryHirarchyLevel = TabCategoryHirarchyLevel - 2
        '    TabCategoryHirarchysequence = 1
        'End If
        Dim tabGroupId = sender.SelectedTab.Tag
        If existingGroupId <> sender.SelectedTab.Tag Then
            existingGroupId = sender.SelectedTab.Tag
            Dim tabGroupName = sender.SelectedTab.Text
            If ArticleHirarchyLevel = 1 AndAlso ArticleHirarchyTabLevel = 1 Then
                ArticleHirarchyLevel = 1
                ArticleHirarchyTabLevel = 1
                PreviousArticleHirarchyLevel = 0
                PreviousArticleHirarchyTabLevel = 0
            ElseIf ArticleHirarchyLevel = 1 AndAlso ArticleHirarchyTabLevel = 2 Then

            ElseIf ArticleHirarchyLevel = 1 AndAlso ArticleHirarchyTabLevel = 3 Then

            End If
            AddTabsCategory(CtrlDocButtonTabGroup, tabGroupName, tabGroupId, ArticleHirarchyLevel)
        End If
    End Sub
    Dim existingGroupId As String = ""
    Public Sub TabButtonChildrensGroups_Click(sender As Object, e As EventArgs)
        'If TabCategoryHirarchyLevel = 1 Then
        '    TabCategoryHirarchyLevel = TabCategoryHirarchyLevel + 1
        '    TabCategoryHirarchysequence = 1
        'ElseIf TabCategoryHirarchyLevel = 2 Then
        '    If fpTabCategoryDecreasePanelHeight <> 0 Then
        '        TabCategoryHirarchyLevel = TabCategoryHirarchyLevel + 1
        '        TabCategoryHirarchysequence = 1
        '    End If
        'ElseIf TabCategoryHirarchyLevel = 3 Then
        '    TabCategoryHirarchysequence = 3
        'End If
        Dim tabGroupId = sender.SelectedTab.Tag
        Dim tabGroupName = sender.SelectedTab.Text
        'fpTabCategoryDecreasePanelWidth = 35
        'fpTabCategoryDecreasePanelHeight = 90
        'If TabCategoryHirarchyLevel = 3 Then
        '    ArticleHirarchyLevel = 3
        'Else
        '    ArticleHirarchyLevel = 2
        'End If
        AddTabsCategory(sender, tabGroupName, tabGroupId, ArticleHirarchyLevel)
    End Sub
#End Region
#Region "------------------------------------------method"
    Private Sub AddTabsChild(ByRef TTabButtonGroupsChild As Spectrum.CtrlTab, ByVal GroupName As String, ByVal GroupId As String)

        Dim ButtonGroupPage As New C1.Win.C1Command.C1DockingTabPage
        ButtonGroupPage.Size = New Size(400, 100)
        ButtonGroupPage.Margin = New Padding(0)
        ButtonGroupPage.Padding = New Padding(0)
        ButtonGroupPage.TabBackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        ButtonGroupPage.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        ButtonGroupPage.TabForeColorSelected = Color.White
        ButtonGroupPage.Text = GroupName
        ButtonGroupPage.Tag = GroupId
        ButtonGroupPage.ToolTipText = GroupName
        ' ButtonGroupPage.BackColor = Color.Gray
        TTabButtonGroupsChild.Tag = GroupId
        TTabButtonGroupsChild.ItemSize = New Size(150, 30)
        TTabButtonGroupsChild.ShowToolTips = True

        TTabButtonGroupsChild.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        TTabButtonGroupsChild.TabPages.Add(ButtonGroupPage)
        TTabButtonGroupsChild.Size = New Size(1250, 550)
    End Sub
    Private Sub AddTabs(ByRef TTabButtonGroups As Spectrum.CtrlTab, ByVal GroupName As String, ByVal GroupId As String)

        Dim ButtonGroupPage As New C1.Win.C1Command.C1DockingTabPage
        ButtonGroupPage.Size = New Size(200, 100)
        ButtonGroupPage.Margin = New Padding(0)
        ButtonGroupPage.Padding = New Padding(0)
        ButtonGroupPage.TabBackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        ButtonGroupPage.TabBackColorSelected = Color.FromArgb(0, 107, 163)
        ButtonGroupPage.TabForeColorSelected = Color.White
        ButtonGroupPage.Text = GroupName
        ButtonGroupPage.Tag = GroupId
        TTabButtonGroups.Tag = GroupId
        ButtonGroupPage.ToolTipText = GroupName
        TTabButtonGroups.ItemSize = New Size(150, 30)
        TTabButtonGroups.ShowToolTips = True
        ' ButtonGroupPage.BackColor = Color.Gray

        'TTabButtonGroups.too
        'TTabButtonGroups.Size = New Size(1276, 500)
        TTabButtonGroups.Size = New Size(1250, 550)
        TTabButtonGroups.Size = New Size(1250, 550)
        fpTabCategoryPanelWidth = TTabButtonGroups.Size.Width
        fpTabCategoryPanelHeight = TTabButtonGroups.Size.Height
        TTabButtonGroups.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        TTabButtonGroups.TabPages.Add(ButtonGroupPage)
        'TTabButtonGroups.Size = New Size(1250, 550)
    End Sub
    Public Sub AddCategoryTableLayoutPanel(ByRef TTabButtonGroups As Spectrum.CtrlTab, ByRef _tblLytPnlHeader As TableLayoutPanel, ByVal _CategoryLevel As Integer)
        'Dim _tblLytPnlHeader As New TableLayoutPanel
        ' _tblLytPnlHeader.BackColor = Color.LightYellow
        ' _tblLytPnlHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
        _tblLytPnlHeader.ColumnCount = 1
        _tblLytPnlHeader.RowCount = 2
        _tblLytPnlHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100))
        If _CategoryLevel = 1 Then
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.FirstCallFirstRow))
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.FirstCallSecondRow))
        ElseIf _CategoryLevel = 2 Then
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.SecondCallFirstRow))
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.SecondCallSecondRow))
        ElseIf _CategoryLevel = 3 Then
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.thirdCallFirstRow))
            _tblLytPnlHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, enumTableRowPercent.thirdCallSecondRow))
        End If
        TTabButtonGroups.TabPages(TTabButtonGroups.SelectedIndex).Controls.Add(_tblLytPnlHeader)
        _tblLytPnlHeader.Dock = DockStyle.Fill
    End Sub
    Enum enumTableRowPercent
        FirstCallFirstRow = 13
        FirstCallSecondRow = 87
        SecondCallFirstRow = 17
        SecondCallSecondRow = 83
        thirdCallFirstRow = 25
        thirdCallSecondRow = 75
    End Enum

    'Public Sub AddArticleFLowLayoutPanel(ByRef fpArticle As FlowLayoutPanel, ByRef _tblLytPnlHeader As TableLayoutPanel)
    '    _tblLytPnlHeader.Controls.Add(fpArticle, 0, 1)
    '    fpArticle.Dock = DockStyle.Fill
    '    ' fpArticle.BackColor = Color.AliceBlue
    '    fpArticle.AutoScroll = True
    '    'fpArticle.VerticalScroll.
    'End Sub

    Public Sub AddArticleFLowLayoutPanel(ByRef fpArticle As TableLayoutPanel, ByRef _tblLytPnlHeader As TableLayoutPanel)
        '_tblLytPnlHeader.AutoScroll = True
        _tblLytPnlHeader.Controls.Add(fpArticle, 0, 1)
        fpArticle.MinimumSize = New Size(400, 0)
        fpArticle.Dock = DockStyle.Fill
        fpArticle.BackColor = Color.AliceBlue
        fpArticle.AutoScroll = True
    End Sub

    'Public Sub AddTabsChildCategory(ByRef DocTabButtonChildrensGroups As Spectrum.CtrlTab, ByRef fpTabCategoryPanel As FlowLayoutPanel)
    '    fpTabCategoryPanel.Controls.Add(DocTabButtonChildrensGroups)
    '    DocTabButtonChildrensGroups.TabPages.Clear()
    '    DocTabButtonChildrensGroups.TabSizeMode = TabSizeMode.Normal
    '    DocTabButtonChildrensGroups.ItemSize = New Size(150, 30)
    '    DocTabButtonChildrensGroups.ShowToolTips = True
    '    DocTabButtonChildrensGroups.Dock = DockStyle.Fill
    '    DocTabButtonChildrensGroups.BackColor = Color.Green
    '    DocTabButtonChildrensGroups.Visible = True
    '    DocTabButtonChildrensGroups.MultiLine = True
    '    DocTabButtonChildrensGroups.ResumeLayout()
    '    AddHandler DocTabButtonChildrensGroups.Click, AddressOf TabButtonChildrensGroups_Click

    'End Sub
    Public Sub AddTabsCategory(ByRef TTabButtonGroups As Spectrum.CtrlTab, ByVal GroupName As String, ByVal GroupId As String, ByVal _CategoryLevel As Integer)
        Try
            'TTabButtonGroups.TabSizeMode = TabSizeMode.Normal
            'TTabButtonGroups.ItemSize = New Size(150, 30)
            ''fpTabArticles = New FlowLayoutPanel
            '' fpTabCategoryPanel = GetFlowLayoutPanel((fpTabCategoryPanelWidth - fpTabCategoryDecreasePanelWidth), (fpTabCategoryPanelHeight - fpTabCategoryDecreasePanelHeight))
            ''Dim _width = 1250
            ''Dim _height = 450
            ''fpTabArticles.Size = New Size(_width, _height)
            ''fpTabArticles.AutoScroll = True
            ''TTabButtonGroups.TabPages(TTabButtonGroups.SelectedIndex).Controls.Add(fpTabArticles)
            'fpTabCategoryPanel = New FlowLayoutPanel
            ''TTabButtonGroups.TabPages(TTabButtonGroups.SelectedIndex).Controls.Add(fpTabCategoryPanel)
            'Dim _tblLytPnlHeader As New TableLayoutPanel

            'AddCategoryTableLayoutPanel(TTabButtonGroups, _tblLytPnlHeader)
            'AddArticleFLowLayoutPanel(fpTabCategoryPanel, _tblLytPnlHeader)
            ''_tblLytPnlHeader.ColumnCount = 1
            ''_tblLytPnlHeader.RowCount = 2
            ''_tblLytPnlHeader.Controls.Add(fpTabCategoryPanel, 0, 1)
            ''fpTabCategoryPanel.Dock = DockStyle.Fill
            ''TTabButtonGroups.TabPages(TTabButtonGroups.SelectedIndex).Controls.Add(_tblLytPnlHeader)

            ''TTabButtonGroups.TabPages(TTabButtonGroups.SelectedIndex).Controls.Add(fpTabCategoryPanel)
            'Dim dtParentGroups As DataTable = BindGroupCategory(GroupId, CallFromViewArticles:=True)
            'ButtonGroupsActionSection(_tblLytPnlHeader, GroupId, GroupName)
            ''ButtonGroupsActionSection(fpTabArticles, GroupId, GroupName)
            ' ''ButtonGroupsActionSection(fpTabCategoryPanel, GroupId, GroupName)
            '' ButtonGroupsActionSection(_tblLytPnlHeader, GroupId, GroupName)
            'If dtParentGroups.Rows.Count > 0 Then

            '    DocTabButtonChildrensGroups = New Spectrum.CtrlTab
            '    DocTabButtonChildrensGroups.MultiLine = True
            '    DocTabButtonChildrensGroups.TabPages.Clear()
            '    DocTabButtonChildrensGroups.TabSizeMode = TabSizeMode.Normal
            '    DocTabButtonChildrensGroups.ItemSize = New Size(150, 30)
            '    DocTabButtonChildrensGroups.ShowToolTips = True
            '    '   TabCategoryHirarchyLevel = TabCategoryHirarchyLevel + 1
            '    'AddTabsChildCategory(DocTabButtonChildrensGroups, fpTabCategoryPanel)
            '    'fpTabArticles.Controls.Add(DocTabButtonChildrensGroups)
            '    DocTabButtonChildrensGroups.Visible = True
            '    DocTabButtonChildrensGroups.MultiLine = True
            '    DocTabButtonChildrensGroups.ResumeLayout()
            '    AddHandler DocTabButtonChildrensGroups.Click, AddressOf TabButtonChildrensGroups_Click
            '    Dim btn As CtrlBtn
            '    'DocTabButtonChildrensGroups = New Spectrum.CtrlTab
            '    For i As Integer = 0 To dtParentGroups.Rows.Count - 1
            '        AddTabs(DocTabButtonChildrensGroups, dtParentGroups.Rows(i)("GroupName").ToString(), dtParentGroups.Rows(i)("GroupId").ToString())
            '        'btn = New CtrlBtn
            '        'btn.Text = "Adil Khan " + Convert.ToString(i)
            '        'btn.Size = New Size(300, 100)
            '        'fpTabCategoryPanel.Controls.Add(btn)
            '    Next
            '    'fpTabCategoryPanel.Controls.Add(DocTabButtonChildrensGroups)
            '    'DocTabButtonChildrensGroups.BorderStyle = BorderStyle.FixedSingle
            '    'DocTabButtonChildrensGroups.Size = New Size(100, 100)
            '    'DocTabButtonChildrensGroups.Dock = DockStyle.Fill
            '    'DocTabButtonChildrensGroups.BackColor = Color.GreenYellow
            '    fpTabCategoryPanel.Controls.Add(DocTabButtonChildrensGroups)
            '    DocTabButtonChildrensGroups.Dock = DockStyle.Fill
            '    fpTabCategoryPanel.BackColor = Color.GreenYellow
            'Else
            '    Dim dtgrpArticle As DataTable = objCls.FetchCurrentArticles(clsAdmin.SiteCode, GroupId)
            '    If dtgrpArticle.Rows.Count > 0 Then
            '        'AddArticlesToTabs(fpTabArticles, dtgrpArticle, GroupId, GroupName)
            '        AddArticlesToTabs(fpTabCategoryPanel, dtgrpArticle, GroupId, GroupName)
            '    End If
            'End If
            TTabButtonGroups.TabSizeMode = TabSizeMode.Normal
            TTabButtonGroups.ItemSize = New Size(150, 30)
            'TTabButtonGroups.BackColor = Color.Green
            Dim _tblLytPnlHeader As New TableLayoutPanel
            AddCategoryTableLayoutPanel(TTabButtonGroups, _tblLytPnlHeader, _CategoryLevel)
            Dim dtParentGroups As DataTable = BindGroupCategory(GroupId, CallFromViewArticles:=True)
            ButtonGroupsActionSection(_tblLytPnlHeader, GroupId, GroupName)
            If dtParentGroups.Rows.Count > 0 Then
                DocTabButtonChildrensGroups = New Spectrum.CtrlTab
                DocTabButtonChildrensGroups.MultiLine = True
                DocTabButtonChildrensGroups.TabPages.Clear()
                DocTabButtonChildrensGroups.ItemSize = New Size(150, 30)
                DocTabButtonChildrensGroups.TabSizeMode = TabSizeMode.Normal
                DocTabButtonChildrensGroups.ItemSize = New Size(TTabButtonGroups.ItemSize.Width, TTabButtonGroups.ItemSize.Height)
                DocTabButtonChildrensGroups.ShowToolTips = True
                DocTabButtonChildrensGroups.TabStyle = TabStyleEnum.Classic
                DocTabButtonChildrensGroups.VisualStyle = C1.Win.C1Command.VisualStyle.Classic
                DocTabButtonChildrensGroups.ResumeLayout()
                AddHandler DocTabButtonChildrensGroups.Click, AddressOf TabButtonChildrensGroups_Click
                For i As Integer = 0 To dtParentGroups.Rows.Count - 1
                    AddTabsChild(DocTabButtonChildrensGroups, dtParentGroups.Rows(i)("GroupName").ToString(), dtParentGroups.Rows(i)("GroupId").ToString())
                Next
                _tblLytPnlHeader.Controls.Add(DocTabButtonChildrensGroups, 0, 1)
            Else
                TblTabCategoryPanel = New TableLayoutPanel
                AddArticleFLowLayoutPanel(TblTabCategoryPanel, _tblLytPnlHeader)
                Dim dtgrpArticle As DataTable = objCls.FetchCurrentArticles(clsAdmin.SiteCode, GroupId)
                If dtgrpArticle.Rows.Count > 0 Then
                    'AddArticlesToTabs(fpTabArticles, dtgrpArticle, GroupId, GroupName)
                    AddArticlesToTabs(TblTabCategoryPanel, dtgrpArticle, GroupId, GroupName)
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim CtrlPosArticle As CtrlPosArticles
    Public Function CreatePosArticleButton(ByVal _tabGroupId As String, ByVal _articleCode As String, ByVal _ArticleName As String, ByVal _additonalInfo As String)

        CtrlPosArticle = New CtrlPosArticles(_articleCode)
        CtrlPosArticle.BackColor = Color.LightGray
        CtrlPosArticle.lblArticleName.Text = _ArticleName
        CtrlPosArticle.lblArticleCode.Text = _articleCode
        CtrlPosArticle.lblArticleAdditionalInfo.Text = _additonalInfo
        '  CtrlPosArticle.CtrlbtnDeleteArticle.Tag = _tabGroupId
        CtrlPosArticle.SetArticleCode = _articleCode
        CtrlPosArticle.Size = New Size(180, 80)
        Dim _CtrlRoundedButton As New CtrlRoundedButton
        _CtrlRoundedButton.Image = Global.Spectrum.My.Resources.Exit_Hover
        _CtrlRoundedButton.Tag = _tabGroupId
        _CtrlRoundedButton.BackColor = Color.Transparent
        CtrlPosArticle.TableLayoutPanel2.Controls.Add(_CtrlRoundedButton, 0, 0)
        CtrlPosArticle.TblPnlItemControl.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble

        _CtrlRoundedButton.Dock = DockStyle.Right
        _CtrlRoundedButton.Margin = New Padding(3, 0, 3, 0)
        AddHandler _CtrlRoundedButton.Click, AddressOf btnDeleteViewArticle_Click

        'Dim p As Drawing2D.GraphicsPath
        'p = New Drawing2D.GraphicsPath
        'p.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        'p.AddLine(20, 0, CtrlPosArticle.Width, 0)
        'p.AddArc(New Rectangle(CtrlPosArticle.TblPnlItemControl.Width, 0, 20, 20), -90, 90)
        'p.AddLine(CtrlPosArticle.TblPnlItemControl.Width, 20, CtrlPosArticle.TblPnlItemControl.Width, CtrlPosArticle.TblPnlItemControl.Height)
        'p.AddArc(New Rectangle(CtrlPosArticle.TblPnlItemControl.Width, CtrlPosArticle.TblPnlItemControl.Height, 20, 20), 0, 90)
        'p.AddLine(CtrlPosArticle.TblPnlItemControl.Width, CtrlPosArticle.TblPnlItemControl.Height, 20, CtrlPosArticle.TblPnlItemControl.Height)
        'p.AddArc(New Rectangle(0, CtrlPosArticle.TblPnlItemControl.Height, 20, 20), 90, 90)
        'p.CloseFigure()
        'CtrlPosArticle.Region = New Region(p)

        'Dim _width = 25
        'Dim _width180 = 180
        'Dim _width90 = 90
        'Dim _withMinus90 = -90
        'Dim p As Drawing2D.GraphicsPath
        'p = New Drawing2D.GraphicsPath
        'p.AddArc(New Rectangle(0, 0, _width, _width), _width180, _width90)
        'p.AddLine(_width, 0, CtrlPosArticle.Width - _width, 0)
        ''p.AddLine(20, 0, CtrlPosArticle.Width - 20, 0)
        'p.AddArc(New Rectangle(CtrlPosArticle.Width - _width, 0, _width, _width), _withMinus90, _width90)
        'p.AddLine(CtrlPosArticle.Width, _width, CtrlPosArticle.Width, CtrlPosArticle.Height - _width)
        'p.AddArc(New Rectangle(CtrlPosArticle.Width - _width, CtrlPosArticle.Height - _width, _width, _width), 0, _width90)
        'p.AddLine(CtrlPosArticle.Width - _width, CtrlPosArticle.Height, _width, CtrlPosArticle.Height)
        'p.AddArc(New Rectangle(0, CtrlPosArticle.Height - _width, _width, _width), _width90, _width90)
        'p.CloseFigure()
        'CtrlPosArticle.Region = New Region(p)


        'Dim p As Drawing2D.GraphicsPath
        'p = New Drawing2D.GraphicsPath
        'p.AddArc(New Rectangle(0, 0, 180, 80), 180, 90)
        'p.AddLine(20, 0, CtrlPosArticle.Width - 20, 0)
        'p.AddArc(New Rectangle(CtrlPosArticle.Width - 20, 0, 20, 20), -90, 90)
        'p.AddLine(CtrlPosArticle.Width, 20, CtrlPosArticle.Width, CtrlPosArticle.Height - 20)
        'p.AddArc(New Rectangle(CtrlPosArticle.Width - 20, CtrlPosArticle.Height - 20, 20, 20), 0, 90)
        'p.AddLine(CtrlPosArticle.Width - 20, CtrlPosArticle.Height, 20, CtrlPosArticle.Height)
        'p.AddArc(New Rectangle(0, CtrlPosArticle.Height - 20, 20, 20), 90, 90)
        'p.CloseFigure()
        'CtrlPosArticle.Region = New Region(p)




        'AddHandler CtrlPosArticle.CtrlbtnDeleteArticle.Click, AddressOf btnDeleteViewArticle_Click
        Return CtrlPosArticle
    End Function
    'Public Function AddArticlesToTabs(ByRef _fpTabArticles As FlowLayoutPanel, ByVal dtgrpArticle As DataTable, ByVal tabGroupId As String, ByVal tabGroupName As String, ByVal _CategoryLevel As Integer) As Boolean
    Public Function AddArticlesToTabs(ByRef _fpTabArticles As TableLayoutPanel, ByVal dtgrpArticle As DataTable, ByVal tabGroupId As String, ByVal tabGroupName As String) As Boolean
        Try
            If dtgrpArticle.Rows.Count > 0 Then
                'Dim CtrlPosArticle As CtrlPosArticles
                Dim lblArticleName As Spectrum.CtrlLabel
                Dim lblArticleCode As CtrlLabel
                Dim lblArticleAdditonalInfo As CtrlLabel
                Dim btnDeleteViewArticle As CtrlBtn
                Dim pnlArticles As Panel
                Dim Points As New Point(10, 15)
                Dim _tblPnlLytArticleTable As New TableLayoutPanel
                ' _tblPnlLytArticleTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
                _tblPnlLytArticleTable.MinimumSize = New Size(400, 500)
                _tblPnlLytArticleTable.ColumnCount = 4
                _tblPnlLytArticleTable.RowCount = 4
                Dim _Even = 0
                Dim _tblRowCount = 0
                Dim _ColPercent = 20
                Dim _RowPercent = 25
                For index = 1 To _tblPnlLytArticleTable.ColumnCount
                    _tblPnlLytArticleTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, _ColPercent))
                Next
                For index = 1 To _tblPnlLytArticleTable.RowCount
                    _tblPnlLytArticleTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (_RowPercent)))
                Next
                Dim _rowNumber = 0
                Dim _colNumber = 0
                For art As Integer = 0 To dtgrpArticle.Rows.Count - 1

                    CtrlPosArticle = CreatePosArticleButton(tabGroupId, _
                                                            dtgrpArticle.Rows(art)("ArticleCode").ToString, _
                                                            dtgrpArticle.Rows(art)("ArticleName").ToString, _
                                                            dtgrpArticle.Rows(art)("AdditonalInfo").ToString)
                    _tblPnlLytArticleTable.Controls.Add(CtrlPosArticle, _colNumber, _rowNumber)
                    CtrlPosArticle.Dock = DockStyle.Fill
                    If art = 4 Then
                        _colNumber = 0
                        _rowNumber = _rowNumber + 1
                    Else
                        _colNumber = _colNumber + 1
                    End If
                   

                    'Dim p As Drawing2D.GraphicsPath
                    'p = New Drawing2D.GraphicsPath
                    'p.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
                    'p.AddLine(20, 0, CtrlPosArticle.Width - 20, 0)
                    'p.AddArc(New Rectangle(CtrlPosArticle.Width - 20, 0, 20, 20), -90, 90)
                    'p.AddLine(CtrlPosArticle.Width, 20, CtrlPosArticle.Width, CtrlPosArticle.Height - 20)
                    'p.AddArc(New Rectangle(CtrlPosArticle.Width - 20, CtrlPosArticle.Height - 20, 20, 20), 0, 90)
                    'p.AddLine(CtrlPosArticle.Width - 20, CtrlPosArticle.Height, 20, CtrlPosArticle.Height)
                    'p.AddArc(New Rectangle(0, CtrlPosArticle.Height - 20, 20, 20), 90, 90)
                    'p.CloseFigure()
                    'CtrlPosArticle.Region = New Region(p)

                    'p = New Drawing2D.GraphicsPath
                    'p.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
                    'p.AddLine(20, 0, CtrlPosArticle.Panel1.Width - 20, 0)
                    'p.AddArc(New Rectangle(CtrlPosArticle.Panel1.Width - 20, 0, 20, 20), -90, 90)
                    'p.AddLine(CtrlPosArticle.Panel1.Width, 20, CtrlPosArticle.Panel1.Width, CtrlPosArticle.Panel1.Height - 20)
                    'p.AddArc(New Rectangle(CtrlPosArticle.Panel1.Width - 20, CtrlPosArticle.Panel1.Height - 20, 20, 20), 0, 90)
                    'p.AddLine(CtrlPosArticle.Panel1.Width - 20, CtrlPosArticle.Panel1.Height, 20, CtrlPosArticle.Panel1.Height)
                    'p.AddArc(New Rectangle(0, CtrlPosArticle.Panel1.Height - 20, 20, 20), 90, 90)
                    'p.CloseFigure()
                    'CtrlPosArticle.Panel1.Region = New Region(p)
                Next
                _fpTabArticles.Controls.Add(_tblPnlLytArticleTable)
                _tblPnlLytArticleTable.Dock = DockStyle.Fill
                ' _fpTabArticles.BackColor = Color.LightSalmon
                If dtgrpArticle.Rows.Count >= 12 Then
                    _fpTabArticles.AutoScroll = True
                Else
                    _fpTabArticles.AutoScroll = False
                End If


                Dim parent As Control = DirectCast(_fpTabArticles, Control).Parent


            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Dim fpTabCategoryPanel As FlowLayoutPanel
    Dim TblTabCategoryPanel As TableLayoutPanel
    Dim fpTabCategoryPanelWidth As Integer
    Dim fpTabCategoryPanelHeight As Integer
    Dim fpTabCategoryDecreasePanelWidth As Integer
    Dim fpTabCategoryDecreasePanelHeight As Integer

    Dim TabCategoryHirarchysequence As Integer = 0
    Public Function GetFlowLayoutPanel(ByVal _width As Integer, ByVal _height As Integer) As FlowLayoutPanel
        fpTabCategoryPanel = New FlowLayoutPanel
        fpTabCategoryPanel.Size = New Size(_width, _height)
        'fpTabCategoryPanel.BackColor = Color.LightYellow
        fpTabCategoryPanel.BorderStyle = BorderStyle.None
        ' fpTabCategoryPanel.AutoScroll = True
        Return fpTabCategoryPanel
    End Function
    
#End Region
#End Region
#End Region
#Region "-----------------------------Form Load"
#Region "------------------------------------------Events"
    Private Sub frmPosTabCreation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'TblPnlScrolable.Padding = New Padding(0, 0, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth) 'VerticalScrollBarWidth


            If (Screen.PrimaryScreen.Bounds.Width <= 1030) Then
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height))
                TblPnlScrolable.Padding = New Padding(0, 0, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + 30, 0)
                TblPnlScrolable.Padding = New Padding(0, 0, System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + 30, 0)
            Else
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height))
            End If
            TblPnlTempSizerContainer.Dock = DockStyle.Fill
            Button1.Visible = False

            AddHandler Button1.Click, AddressOf Button1_Click
            FormDesign()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            dtAddGroup = objCls.GetbtnGroupStruc()
            dtCurrentArticle = objCls.GetCurrentArticleStruc()
            dtCurrentArticle.Clear()
            dtAddFinalArticles = objCls.GetFinalArticleStruc()
            dtDeleteViewArticles = objCls.GetDeleteViewArticleStruc()
            dtDeleteViewArticles.Clear()
            LoadSite()
            BindDropDown(BindGroupCategory(""), cmbBaseGroupCategory)
            btnArticleAddSelectedCategory.Visible = False
            dgCategoryArticles.Visible = False
            dgFinalArticle.Visible = False
            grdFinalGridSetting()
            cmbBaseGroupCategory.Visible = True
            TblPanelChildGroups.Visible = True
            fpRemarksAddGroup.AutoScroll = False
            GroupName = ""
            GroupId = ""
            Me.KeyPreview = True
            ParentGroupId = ""
            Dim condition As String
            Dim objItem As New clsIteamSearch
            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
            Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            'AddHandler Me.KeyDown, AddressOf frmPosTabCreation_KeyDown
            AddHandler txtAndroidArticleSearchTextBox.KeyDown, AddressOf txtSearch_KeyDown
            'AddHandler txtAndroidArticleSearchTextBox.Leave, AddressOf txtAndroidArticleSearchTextBox_Leave
            'Dim dtBind = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, "")
            If dtBind.Rows.Count > 1 Then
                'txtAddArticle.lstNames = listSource
                'Call SetWildSearchTextBox(dtBind, txtAndroidArticleSearchTextBox, key:="ArticleCode", Value:="ArticleName", searchData:="ArticleCodeDesc")
                Call SetWildSearchTextBox(dtBind, txtAndroidArticleSearchTextBox, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
                txtAndroidArticleSearchTextBox.IsMovingControl = True
                txtAndroidArticleSearchTextBox.IsSetLocation = True
                txtAndroidArticleSearchTextBox.ListBoxXCoordinate = txtAndroidArticleSearchTextBox.Location.X + 15
                txtAndroidArticleSearchTextBox.ListBoxYCoordinate = txtAndroidArticleSearchTextBox.Location.Y + 240
                'txtAndroidArticleSearchTextBox.Location = New Point(50, 29)
                txtAndroidArticleSearchTextBox.Select()
            End If
            tblAddgroup.Visible = True
            tblAddArticle.Visible = False
            tblViewArticle.Visible = False
            grpBxAddArticle.Visible = False
            rdBtnAddGroup.Checked = True
            rdBtnAddGroup_Click(rdBtnAddGroup, Nothing)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub Button1_Click(sender As Object, e As EventArgs)
        Dim posArticle As CtrlPosArticles
        Dim x = 5, y = 5

        For index = 1 To 2
            posArticle = New CtrlPosArticles(index)
            posArticle.lblArticleName.Text = "Adil"
            posArticle.lblArticleCode.Text = "Adil12"
            posArticle.lblArticleAdditionalInfo.Text = "Adil 123"
            posArticle.Location = New Point(x, y)
            x = posArticle.Location.X + 160
            y = posArticle.Location.Y

            Dim p As Drawing2D.GraphicsPath
            p = New Drawing2D.GraphicsPath
            ' p.StartFigure()
            p.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
            p.AddLine(20, 0, posArticle.Width - 20, 0)
            p.AddArc(New Rectangle(posArticle.Width - 20, 0, 20, 20), -90, 90)
            p.AddLine(posArticle.Width, 20, posArticle.Width, posArticle.Height - 20)
            p.AddArc(New Rectangle(posArticle.Width - 20, posArticle.Height - 20, 20, 20), 0, 90)
            p.AddLine(posArticle.Width - 20, posArticle.Height, 20, posArticle.Height)
            p.AddArc(New Rectangle(0, posArticle.Height - 20, 20, 20), 90, 90)
            p.CloseFigure()
            posArticle.Region = New Region(p)
        Next


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            'GroupName = DirectCast(fpRemarksAddGroup.Controls.Find("txtGroupName", True)(0), TextBox).Text.Trim()
            If rdBtnAddGroup.Checked Then
                GroupName = txtGroupName.Text
                Dim dr = dtAddGroup.NewRow()
                dr("SiteCode") = clsAdmin.SiteCode
                dr("CreatedBy") = clsAdmin.UserCode
                dr("GroupId") = GroupId
                dr("GroupName") = GroupName
                dr("ParentGroupID") = ParentGroupId
                dtAddGroup.Rows.Add(dr)
                If SaveBtnGroup(dtAddGroup) = True Then
                    ShowMessage("Group saved successfully", "Information")
                    dtAddGroup.Clear()
                    GroupId = ""
                    GroupName = ""
                    ParentGroupId = ""
                    TblPanelChildGroups.Visible = False
                    'txtGroupName.Text = String.Empty
                    rdBtnAddArticle.Checked = False
                    rdBtnAddGroup.Checked = False
                    rdBtnViewGroupsOrArticles.Checked = False
                    BindDropDown(BindGroupCategory(""), cmbBaseGroupCategory)
                Else
                    ShowMessage("Group saved failed", "Information")
                End If
            End If
            If rdBtnAddArticle.Checked Then
                If cmbGroupCategory.SelectedValue Is Nothing Or cmbGroupCategory.Text = String.Empty Then
                    ShowMessage("Please select button group", "Information")
                    Exit Sub
                End If
                If dtCurrentArticle.Rows.Count > 0 Then
                    For Each dr As DataRow In dtCurrentArticle.Rows
                        Dim drSet As DataRow = dtAddFinalArticles.NewRow
                        drSet("SITECODE") = clsAdmin.SiteCode
                        drSet("GROUPID") = Convert.ToString(cmbGroupCategory.SelectedValue)
                        drSet("ARTICLECODE") = dr("ARTICLECODE")
                        drSet("ARTICLENAME") = dr("ARTICLENAME")
                        drSet("CREATEDBY") = clsAdmin.UserCode
                        If dr("Delete") = True Then
                            drSet("Status") = False
                        Else
                            drSet("Status") = True
                        End If
                        dtAddFinalArticles.Rows.Add(drSet)
                    Next
                    If SaveBtnArticles(dtAddFinalArticles) = True Then
                        ShowMessage("Group Articles saved successfully", "Information")
                        rdBtnAddGroup.Checked = False
                        rdBtnAddArticle.Checked = False
                        rdBtnViewGroupsOrArticles.Checked = False
                        dtAddFinalArticles.Clear()
                        dtCurrentArticle.Clear()
                        'dtGroupsCategory.Clear()
                        'pnlAddArticle.Visible = False
                        'pnlAddGroup.Visible = False
                        btnSave.Enabled = False
                        GroupId = ""
                        GroupName = ""
                        ParentGroupId = ""
                        'TblPanelChildGroups.Visible = False
                    Else
                        ShowMessage("Group Articles saved failed", "Information")
                    End If
                End If
            End If
            If rdBtnViewGroupsOrArticles.Checked Then
                If dtDeleteViewArticles.Rows.Count > 0 Then
                    Dim result As Boolean = False
                    For Each drDelete As DataRow In dtDeleteViewArticles.Rows
                        Dim strQuery As New StringBuilder
                        strQuery.Length = 0
                        strQuery.Append(" update  buttonArticle set Status=0,isActive=0 ,UPDATEDAT='" + clsAdmin.SiteCode + "',UPDATEDBY='" + clsAdmin.UserCode + "',")
                        strQuery.Append(" UPDATEDON=GETDATE() where GROUPID='" + drDelete("GROUPID") + "' and SiteCode in ('" + clsAdmin.SiteCode + "','CCE') and ARTICLECODE='" + drDelete("ARTICLECODE") + "'")
                        If ViewArticleActions(strQuery.ToString()) Then
                            result = True
                            Continue For
                        Else
                            result = False
                        End If
                    Next
                    If result Then
                        ShowMessage("Articles removed from group successfully ", "Information")
                    Else
                        ShowMessage("Failed to remove Articles from group ", "Information")
                    End If
                    rdBtnAddGroup_Click(sender, Nothing)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        'ShowMessage("Are you sure you", "Information", True)

        If rdBtnAddArticle.Checked Then
            If dtCurrentArticle.Rows.Count > 0 Then
                Dim EventType As Integer
                Dim result As Integer = MessageBox.Show("You may loose your data are you sure you want to continue", "Information", MessageBoxButtons.YesNo)
                'ShowMessage("You may loose your data are you sure you want to continue", "Information", EventType, "No", "Yes")
                If result = DialogResult.Yes Then
                    objCls.SaveKeylog()
                    If txtAndroidArticleSearchTextBox.IsListBoxVisible Then
                        txtAndroidArticleSearchTextBox.ResetListBox()
                    End If
                    Me.Close()
                End If

                'If EventType <> 2 Then
                '    objCls.SaveKeylog()
                '    If txtAndroidArticleSearchTextBox.IsListBoxVisible Then
                '        txtAndroidArticleSearchTextBox.ResetListBox()
                '    End If
                '    Me.Close()
                'End If
            Else
                objCls.SaveKeylog()
                If txtAndroidArticleSearchTextBox.IsListBoxVisible Then
                    txtAndroidArticleSearchTextBox.ResetListBox()
                End If
                Me.Close()
            End If
        Else
            objCls.SaveKeylog()
            If txtAndroidArticleSearchTextBox.IsListBoxVisible Then
                txtAndroidArticleSearchTextBox.ResetListBox()
            End If
            Me.Close()
        End If
    End Sub
    Private Sub frmPosTabCreation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If rdBtnAddArticle.Checked Then
                If dtCurrentArticle.Rows.Count > 0 Then
                    Dim EventType As Integer
                    Dim result As Integer = MessageBox.Show("You may loose your data are you sure you want to continue", "Information", MessageBoxButtons.YesNo)
                    If result = DialogResult.Yes Then
                        CloseAndroidListBox()
                        Me.Close()
                    End If
                Else
                    CloseAndroidListBox()
                    Me.Close()
                End If
            Else
                CloseAndroidListBox()
                Me.Close()
            End If
        End If
    End Sub
#End Region
#Region "------------------------------------------method"
    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(134, 134, 134)
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black

        Me.CtrllblSiteName.BackColor = Color.Transparent 'Color.FromArgb(212, 212, 212)
        Me.CtrllblSiteName.ForeColor = Color.White

        'btnLeft
        Me.btnLeft.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnLeft.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnLeft.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnLeft.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnLeft.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnLeft.FlatStyle = FlatStyle.Flat
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnRight
        Me.btnRight.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnRight.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnRight.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRight.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnRight.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnRight.FlatStyle = FlatStyle.Flat
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnLeftAll
        Me.btnLeftAll.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnLeftAll.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnLeftAll.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnLeftAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnLeftAll.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnLeftAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnLeftAll.FlatStyle = FlatStyle.Flat
        Me.btnLeftAll.FlatAppearance.BorderSize = 0
        Me.btnLeftAll.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnRightAll
        Me.btnRightAll.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnRightAll.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnRightAll.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnRightAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRightAll.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnRightAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnRightAll.FlatStyle = FlatStyle.Flat
        Me.btnRightAll.FlatAppearance.BorderSize = 0
        Me.btnRightAll.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        '
        Me.btnRightAll.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnRightAll.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnRightAll.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnRightAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRightAll.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnRightAll.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnRightAll.FlatStyle = FlatStyle.Flat
        Me.btnRightAll.FlatAppearance.BorderSize = 0
        Me.btnRightAll.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)


        'btnArticleAddPartialSearch
        Me.btnArticleAddPartialSearch.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnArticleAddPartialSearch.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnArticleAddPartialSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnArticleAddPartialSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnArticleAddPartialSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnArticleAddPartialSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnArticleAddPartialSearch.FlatStyle = FlatStyle.Flat
        Me.btnArticleAddPartialSearch.FlatAppearance.BorderSize = 0
        Me.btnArticleAddPartialSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnPartialSearch
        Me.btnPartialSearch.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnPartialSearch.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnPartialSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnPartialSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPartialSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPartialSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnPartialSearch.FlatStyle = FlatStyle.Flat
        Me.btnPartialSearch.FlatAppearance.BorderSize = 0
        Me.btnPartialSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnArticleAddSelectedCategory
        Me.btnArticleAddSelectedCategory.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnArticleAddSelectedCategory.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnArticleAddSelectedCategory.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnArticleAddSelectedCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnArticleAddSelectedCategory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnArticleAddSelectedCategory.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnArticleAddSelectedCategory.FlatStyle = FlatStyle.Flat
        Me.btnArticleAddSelectedCategory.FlatAppearance.BorderSize = 0
        Me.btnArticleAddSelectedCategory.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Me.btnArticleAddAndroid.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnArticleAddAndroid.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnArticleAddAndroid.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnArticleAddAndroid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnArticleAddAndroid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnArticleAddAndroid.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnArticleAddAndroid.FlatStyle = FlatStyle.Flat
        Me.btnArticleAddAndroid.FlatAppearance.BorderSize = 0
        Me.btnArticleAddAndroid.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnSave
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        Dim p As Drawing2D.GraphicsPath
        p = New Drawing2D.GraphicsPath
        ' p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        p.AddLine(10, 0, btnSave.Width - 10, 0)
        p.AddArc(New Rectangle(btnSave.Width - 10, 0, 10, 10), -90, 90)
        p.AddLine(btnSave.Width, 10, btnSave.Width, btnSave.Height - 10)
        p.AddArc(New Rectangle(btnSave.Width - 10, btnSave.Height - 10, 10, 10), 0, 90)
        p.AddLine(btnSave.Width - 10, btnSave.Height, 10, btnSave.Height)
        p.AddArc(New Rectangle(0, btnSave.Height - 10, 10, 10), 90, 90)
        p.CloseFigure()
        btnSave.Region = New Region(p)


        Me.btnClose.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnClose.FlatStyle = FlatStyle.Flat
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        p = New Drawing2D.GraphicsPath
        ' p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        p.AddLine(10, 0, btnClose.Width - 10, 0)
        p.AddArc(New Rectangle(btnClose.Width - 10, 0, 10, 10), -90, 90)
        p.AddLine(btnClose.Width, 10, btnClose.Width, btnClose.Height - 10)
        p.AddArc(New Rectangle(btnClose.Width - 10, btnClose.Height - 10, 10, 10), 0, 90)
        p.AddLine(btnClose.Width - 10, btnClose.Height, 10, btnClose.Height)
        p.AddArc(New Rectangle(0, btnClose.Height - 10, 10, 10), 90, 90)
        p.CloseFigure()
        btnClose.Region = New Region(p)
        'dgCategoryArticles
        Me.dgCategoryArticles.MaximumSize = New Size(1364, 600)
        Me.dgCategoryArticles.Size = New System.Drawing.Size(331, 333)
        Me.dgCategoryArticles.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.dgCategoryArticles.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.dgCategoryArticles.Styles.Highlight.ForeColor = Color.Black
        Me.dgCategoryArticles.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgCategoryArticles.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.dgCategoryArticles.Rows.MinSize = 26
        Me.dgCategoryArticles.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgCategoryArticles.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgCategoryArticles.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.dgCategoryArticles.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgCategoryArticles.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgCategoryArticles.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)


        ' Me.dgFinalArticle.MaximumSize = New Size(1364, 600)
        'Me.dgFinalArticle.Size = New System.Drawing.Size(1364, 600)
        Me.dgFinalArticle.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.dgFinalArticle.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.dgFinalArticle.Styles.Highlight.ForeColor = Color.Black
        Me.dgFinalArticle.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgFinalArticle.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        'Me.dgFinalArticle.Rows.MinSize = 26
        Me.dgFinalArticle.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.dgFinalArticle.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgFinalArticle.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.dgFinalArticle.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.dgFinalArticle.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.dgFinalArticle.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgFinalArticle.Cols("Delete").DataType = Type.GetType("System.Boolean")
        Return ""
    End Function
    Public Sub FormDesign()

        fpRemarksAddGroup.BackColor = Color.Transparent
        txtSearchPartial.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        cmbSites.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbSites.ItemHeight = 20
        cmbSites.MaxDropDownItems = 25

        cmbBaseGroupCategory.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbBaseGroupCategory.ItemHeight = 20
        cmbBaseGroupCategory.MaxDropDownItems = 25
        'cmbBaseGroupCategory.Size = New Size(332, 30)
        cmbBaseGroupCategory.Dock = DockStyle.Fill

        cmbGroupCategory.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbGroupCategory.ItemHeight = 25
        cmbGroupCategory.MaxDropDownItems = 20
        cmbGroupCategory.Margin = New Padding(2, 3, 3, 3)
        cmbGroupCategory.Size = New Size(332, 30)

        cmbChildAddGroup1.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup1.ItemHeight = 20
        cmbChildAddGroup1.MaxDropDownItems = 25
        cmbChildAddGroup1.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup1.Size = New Size(332, 30)

        cmbChildAddGroup2.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup2.ItemHeight = 20
        cmbChildAddGroup2.MaxDropDownItems = 25
        cmbChildAddGroup2.Margin = New Padding(16, 0, 0, 0)
        ' cmbChildAddGroup2.Size = New Size(332, 30)

        cmbChildAddGroup3.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup3.ItemHeight = 20
        cmbChildAddGroup3.MaxDropDownItems = 25
        cmbChildAddGroup3.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup3.Size = New Size(332, 30)

        cmbChildAddGroup4.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup4.ItemHeight = 20
        cmbChildAddGroup4.MaxDropDownItems = 25
        cmbChildAddGroup4.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup4.Size = New Size(332, 30)

        cmbChildAddGroup5.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup5.ItemHeight = 20
        cmbChildAddGroup5.MaxDropDownItems = 25
        cmbChildAddGroup5.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup5.Size = New Size(332, 30)

        cmbChildAddGroup6.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup6.ItemHeight = 20
        cmbChildAddGroup6.MaxDropDownItems = 25
        cmbChildAddGroup6.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup6.Size = New Size(332, 30)

        cmbChildAddGroup7.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup7.ItemHeight = 20
        cmbChildAddGroup7.MaxDropDownItems = 25
        cmbChildAddGroup7.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup7.Size = New Size(332, 30)

        cmbChildAddGroup8.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup8.ItemHeight = 20
        cmbChildAddGroup8.MaxDropDownItems = 25
        cmbChildAddGroup8.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup8.Size = New Size(332, 30)

        cmbChildAddGroup9.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup9.ItemHeight = 20
        cmbChildAddGroup9.MaxDropDownItems = 25
        cmbChildAddGroup9.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup9.Size = New Size(332, 30)

        cmbChildAddGroup10.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup10.ItemHeight = 20
        cmbChildAddGroup10.MaxDropDownItems = 25
        cmbChildAddGroup10.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup10.Size = New Size(332, 30)

        cmbChildAddGroup11.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup11.ItemHeight = 20
        cmbChildAddGroup11.MaxDropDownItems = 25
        cmbChildAddGroup11.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup11.Size = New Size(332, 30)

        cmbChildAddGroup12.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup12.ItemHeight = 20
        cmbChildAddGroup12.MaxDropDownItems = 25
        cmbChildAddGroup12.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup12.Size = New Size(332, 30)

        cmbChildAddGroup13.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup13.ItemHeight = 20
        cmbChildAddGroup13.MaxDropDownItems = 25
        cmbChildAddGroup13.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup13.Size = New Size(332, 30)

        cmbChildAddGroup14.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup14.ItemHeight = 20
        cmbChildAddGroup14.MaxDropDownItems = 25
        cmbChildAddGroup14.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup14.Size = New Size(332, 30)

        cmbChildAddGroup15.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup15.ItemHeight = 20
        cmbChildAddGroup15.MaxDropDownItems = 25
        cmbChildAddGroup15.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup15.Size = New Size(332, 30)

        cmbChildAddGroup16.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup16.ItemHeight = 20
        cmbChildAddGroup16.MaxDropDownItems = 25
        cmbChildAddGroup16.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup16.Size = New Size(332, 30)

        cmbChildAddGroup17.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup17.ItemHeight = 20
        cmbChildAddGroup17.MaxDropDownItems = 25
        cmbChildAddGroup17.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup17.Size = New Size(332, 30)

        cmbChildAddGroup18.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup18.ItemHeight = 20
        cmbChildAddGroup18.MaxDropDownItems = 25
        cmbChildAddGroup18.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup18.Size = New Size(332, 30)

        cmbChildAddGroup19.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup19.ItemHeight = 20
        cmbChildAddGroup19.MaxDropDownItems = 25
        cmbChildAddGroup19.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup19.Size = New Size(332, 30)

        cmbChildAddGroup20.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup20.ItemHeight = 20
        cmbChildAddGroup20.MaxDropDownItems = 25
        cmbChildAddGroup20.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup20.Size = New Size(332, 30)

        cmbChildAddGroup21.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup21.ItemHeight = 20
        cmbChildAddGroup21.MaxDropDownItems = 25
        cmbChildAddGroup21.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup21.Size = New Size(332, 30)

        cmbChildAddGroup22.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup22.ItemHeight = 20
        cmbChildAddGroup22.MaxDropDownItems = 25
        cmbChildAddGroup22.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup22.Size = New Size(332, 30)

        cmbChildAddGroup23.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup23.ItemHeight = 20
        cmbChildAddGroup23.MaxDropDownItems = 25
        cmbChildAddGroup23.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup23.Size = New Size(332, 30)

        cmbChildAddGroup24.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup24.ItemHeight = 20
        cmbChildAddGroup24.MaxDropDownItems = 25
        cmbChildAddGroup24.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup24.Size = New Size(332, 30)

        cmbChildAddGroup25.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup25.ItemHeight = 20
        cmbChildAddGroup25.MaxDropDownItems = 25
        cmbChildAddGroup25.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup25.Size = New Size(332, 30)

        cmbChildAddGroup26.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup26.ItemHeight = 20
        cmbChildAddGroup26.MaxDropDownItems = 25
        cmbChildAddGroup26.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup26.Size = New Size(332, 30)

        cmbChildAddGroup27.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup27.ItemHeight = 20
        cmbChildAddGroup27.MaxDropDownItems = 25
        cmbChildAddGroup27.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup27.Size = New Size(332, 30)

        cmbChildAddGroup28.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup28.ItemHeight = 20
        cmbChildAddGroup28.MaxDropDownItems = 25
        cmbChildAddGroup28.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup28.Size = New Size(332, 30)

        cmbChildAddGroup29.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmbChildAddGroup29.ItemHeight = 20
        cmbChildAddGroup29.MaxDropDownItems = 25
        cmbChildAddGroup29.Margin = New Padding(16, 0, 0, 0)
        'cmbChildAddGroup29.Size = New Size(332, 30)
    End Sub
    Public Sub LoadSite()
        dtSites = objCls.GetSites(clsAdmin.SiteCode)
        BindDropDown(dtSites, cmbSites)
        cmbSites.SelectedIndex = 0
        'pC1ComboSetDisplayMember(cmbBaseGroupCategory)
        'PopulateComboBox(dtSites, cmbBaseGroupCategory)
        'pC1ComboSetDisplayMember(cmbSites)
    End Sub
    Public Sub BindDropDown(ByVal dt As DataTable, ByRef ObjComboBox As ctrlCombo)
        'ObjComboBox.SelectedIndex = 0
        PopulateComboBox(dt, ObjComboBox)
        pC1ComboSetDisplayMember(ObjComboBox)
    End Sub
    Public Sub grdFinalGridSetting(Optional ByVal isCategoryArticleGrid As Boolean = False)
        If isCategoryArticleGrid Then

            dgCategoryArticles.AllowEditing = False
            dgCategoryArticles.DataSource = Nothing
            dgCategoryArticles.DataSource = dtCurrentArticleCategory
            dgCategoryArticles.Cols("ARTICLECODE").Visible = False
            dgCategoryArticles.Cols("ARTICLECODE").Width = 5
            dgCategoryArticles.Cols("ArticleName").Visible = False
            dgCategoryArticles.Cols("ArticleName").Width = 5
            dgCategoryArticles.Cols("ArticleCodeName").Caption = "ArticleCode Detail"
            dgCategoryArticles.Cols("ArticleCodeName").Width = 100
            dgCategoryArticles.Cols("AdditonalInfo").Visible = False
            dgCategoryArticles.Cols("AdditonalInfo").Width = 5
            dgCategoryArticles.Cols("Delete").Visible = False
            dgCategoryArticles.Cols("Delete").Width = 5
            'dgCategoryArticles.Location = New Point(240, 18)
            Me.dgCategoryArticles.Size = New System.Drawing.Size(331, 333)
            dgCategoryArticles.Visible = True
            If dtCurrentArticleCategory.Rows.Count > 1 Then
                dgCategoryArticles.ScrollBars = ScrollBars.Both
                'dgCategoryArticles.ScrollBars = ScrollBars.Horizontal
            Else

            End If
        Else
            dgFinalArticle.Visible = True
            dgFinalArticle.DataSource = Nothing
            dgFinalArticle.DataSource = dtCurrentArticle
            dgFinalArticle.Cols("ARTICLECODE").Visible = False
            dgFinalArticle.Cols("ARTICLECODE").Width = 10 '
            dgFinalArticle.Cols("ArticleName").Visible = False
            dgFinalArticle.Cols("ArticleName").Width = 10
            dgFinalArticle.Cols("ArticleCodeName").Caption = "ArticleCode Name"
            dgFinalArticle.Cols("ArticleCodeName").Width = 250
            dgFinalArticle.Cols("AdditonalInfo").Caption = "Additonal Info"
            dgFinalArticle.Cols("AdditonalInfo").Width = 130
            dgFinalArticle.Cols("Delete").Caption = "Delete?"
            dgFinalArticle.Cols("Delete").Width = 10
        End If
    End Sub
    Public Sub DisplayAddGroups()
        Try
            TblPanelChildGroups.Visible = True
            tblAddgroup.Visible = True
            tblAddArticle.Visible = False
            tblViewArticle.Visible = False
            grpBxAddArticle.Visible = False
            DropDownVisibility(True)
            'DropDownVisibility(29, False)
            BindDropDown(BindGroupCategory(""), cmbBaseGroupCategory)
            txtGroupName.Text = String.Empty
            btnSave.Enabled = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub DisplayAddArticles()
        Try
            TblPanelChildGroups.Visible = True
            tblAddgroup.Visible = False
            tblAddArticle.Visible = True
            tblViewArticle.Visible = False
            grpBxAddArticle.Visible = False
            BindDropDown(BindGroupCategory("", CallFromAddArticle:=True), cmbGroupCategory)
            dgFinalArticle.Visible = False
            LoadArticleCategoryTree()
            fpAddArticleSection.Visible = False
            btnSave.Enabled = False
            dgCategoryArticles.Visible = False
            btnArticleAddSelectedCategory.Visible = False
            dgCategoryArticles.Clear()
            If dtCurrentArticleCategory IsNot Nothing AndAlso dtCurrentArticleCategory.Rows.Count > 0 Then
                dtCurrentArticleCategory.Clear()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub DisplayViewArticles()
        Try
            existingGroupId = ""
            tblAddgroup.Visible = False
            tblAddArticle.Visible = False
            tblViewArticle.Visible = True
            grpBxAddArticle.Visible = False
            dtDeleteViewArticles.Clear()
            'TabCategoryHirarchyLevel = 1
            TabCategoryHirarchysequence = 1
            'Button1.Visible = True
            'CreateGroupsTab("", "")
            Dim dtParentGroups As DataTable = BindGroupCategory("", CallFromViewArticles:=True)
            If dtParentGroups.Rows.Count > 0 Then
                CtrlDocButtonTabGroup.MultiLine = True
                CtrlDocButtonTabGroup.TabPages.Clear()
                CtrlDocButtonTabGroup.TabSizeMode = TabSizeMode.Normal
                CtrlDocButtonTabGroup.ItemSize = New Size(150, 30)
                CtrlDocButtonTabGroup.ShowToolTips = True
                CtrlDocButtonTabGroup.Dock = DockStyle.Fill
                'CtrlDocButtonTabGroup.HScroll = True
                'CtrlDocButtonTabGroup.Size = New Size(972, 249)
                'CtrlDocButtonTabGroup.Size = New Size(972, 450)
                For i As Integer = 0 To dtParentGroups.Rows.Count - 1
                    AddTabs(CtrlDocButtonTabGroup, dtParentGroups.Rows(i)("GroupName").ToString(), dtParentGroups.Rows(i)("GroupId").ToString())
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function SaveBtnGroup(ByVal dt As DataTable) As Boolean
        Try
            SaveBtnGroup = objCls.SaveAndEditBtnGroup(dt, "Add")
            Return SaveBtnGroup
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function SaveBtnArticles(ByVal dt As DataTable) As Boolean
        Try
            SaveBtnArticles = objCls.SaveAndDeleteButtonArticleData(dt, "Add")
            Return SaveBtnArticles
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub CloseAndroidListBox()
        objCls.SaveKeylog()
        If txtAndroidArticleSearchTextBox.IsListBoxVisible Then
            txtAndroidArticleSearchTextBox.ResetListBox()
            txtAndroidArticleSearchTextBox.Text = String.Empty
        End If
    End Sub
#End Region

#End Region

    Private Sub btmTemp_Click(sender As Object, e As EventArgs) Handles btmTemp.Click
        tblAddgroup.Visible = True
        tblAddArticle.Visible = False
        tblViewArticle.Visible = False
    End Sub

    Private Sub btnTemp2_Click(sender As Object, e As EventArgs) Handles btnTemp2.Click
        tblAddgroup.Visible = False
        tblAddArticle.Visible = True
        tblViewArticle.Visible = False
    End Sub

    Private Sub btnTemp3_Click(sender As Object, e As EventArgs) Handles btnTemp3.Click
        tblAddgroup.Visible = False
        tblAddArticle.Visible = False
        tblViewArticle.Visible = True
    End Sub
End Class
