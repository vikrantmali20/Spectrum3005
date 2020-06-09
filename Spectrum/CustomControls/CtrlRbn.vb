Imports C1.Win.C1Ribbon
Imports SpectrumPrint
Public Class CtrlRbn
    Inherits C1Ribbon
    Public WithEvents ExitButton As New C1.Win.C1Ribbon.RibbonButton
    Dim objParentForm As Object
    Dim WithEvents DbtnCash As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnSOnew As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnSOedit As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnSoCancel As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnBLnew As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnBLedit As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnBLSale As New C1.Win.C1Ribbon.RibbonButton

    ''' <summary>
    ''' Added For Quotation
    ''' </summary>
    ''' <remarks></remarks>
    Dim WithEvents DbtnQuotationnew As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnQuotationedit As New C1.Win.C1Ribbon.RibbonButton
    Dim WithEvents DbtnQuotationCancel As New C1.Win.C1Ribbon.RibbonButton

    Public WithEvents DbtnPayNEFT As New C1.Win.C1Ribbon.RibbonButton  '' added by vipin for PC
    Public WithEvents DbtnPayRTGS As New C1.Win.C1Ribbon.RibbonButton

    Public DgrpQuotation As New C1.Win.C1Ribbon.RibbonGroup

    Public WithEvents DbtnPay As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnpayCheque As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnPayCash As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnPayCard As New C1.Win.C1Ribbon.RibbonButton
    Public DgrpPayments As New C1.Win.C1Ribbon.RibbonGroup

    Public DgrpCommon As New C1.Win.C1Ribbon.RibbonGroup
    Public WithEvents DbtnSwitchUser As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnTopRightExit As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DtbnCloseForm As New Button
    Public WithEvents DbtnonScreenKey As New C1.Win.C1Ribbon.RibbonButton
    '----Dash Icon
    Public WithEvents DbtnSlash1 As New C1.Win.C1Ribbon.RibbonSeparator
    Public WithEvents DbtnSlash2 As New C1.Win.C1Ribbon.RibbonSeparator
    Public WithEvents DbtnSlash3 As New C1.Win.C1Ribbon.RibbonSeparator
    Public WithEvents DbtnSlash4 As New C1.Win.C1Ribbon.RibbonSeparator
    ' help key configuration
    Public WithEvents DbtnSplitKeyHelp As New C1.Win.C1Ribbon.RibbonSplitButton
    Public WithEvents DbtnF12 As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnF2 As New C1.Win.C1Ribbon.RibbonButton
    Public WithEvents DbtnOpenDrawer As New C1.Win.C1Ribbon.RibbonButton
    ' help key configuration

    ' Global CashMemo Form 
    Public WithEvents DbtnGlobalCashMemo As New C1.Win.C1Ribbon.RibbonButton
    ' Global CashMemo Form call from anywhere

    Public DgrpExtraKey As New C1.Win.C1Ribbon.RibbonGroup
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)



        'Add your custom paint code here
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub pInitRbn()
        If Me.ApplicationMenu.LargeImage Is Nothing Then
            Me.ApplicationMenu.LargeImage = Global.Spectrum.My.Resources.Resources.logo
        End If
        'Me.ApplicationMenu.BottomPaneItems.Add(ExitButton)
        'ExitButton.Text = "Exit"
        'Me.ApplicationMenu.BottomPaneItems.Item(0).Visible = True
        'Me.ApplicationMenu.BottomPaneItems.EndUpdate()
        objParentForm = Me.Parent.FindForm()
        pAddRbnTab()
        'PAddCommonGroup()
        pAddConfigTools()
        pSetTransactionCode()
        Me.AllowMinimize = False
        If objParentForm.Name <> "frmCashMemo" AndAlso objParentForm.Name <> "frmFastCashMemo" Or UCase(objParentForm.name) = "FRMTABCASHMEMO" Then
            objParentForm.CancelButton = DtbnCloseForm
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            DbtnSOnew.LargeImage = My.Resources.NewSO1
            DbtnSOedit.LargeImage = My.Resources.EditSO1
            DbtnSoCancel.LargeImage = My.Resources.CancelSO1
            DbtnBLnew.LargeImage = My.Resources.NewSO1
            DbtnBLedit.LargeImage = My.Resources.EditSO1
            DbtnBLSale.LargeImage = My.Resources.CancelSO1
            DbtnQuotationnew.LargeImage = My.Resources.NewSO1
            DbtnQuotationedit.LargeImage = My.Resources.EditSO1
            DbtnQuotationCancel.LargeImage = My.Resources.CancelSO1
            DbtnCash.LargeImage = My.Resources.NewCashMemo
            DbtnF2.LargeImage = My.Resources.ChangeQuantity
            DbtnSOnew.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnSOedit.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnSoCancel.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnBLnew.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnBLedit.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnBLSale.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnQuotationnew.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnQuotationedit.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnQuotationCancel.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnCash.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnF2.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
            DbtnSOnew.Text = DbtnSOnew.Text.ToUpper
            DbtnSOedit.Text = DbtnSOedit.Text.ToUpper
            DbtnSoCancel.Text = DbtnSoCancel.Text.ToUpper
            DbtnBLnew.Text = DbtnBLnew.Text.ToUpper
            DbtnBLedit.Text = DbtnBLedit.Text.ToUpper
            DbtnBLSale.Text = DbtnBLSale.Text.ToUpper
            DbtnQuotationnew.Text = DbtnQuotationnew.Text.ToUpper
            DbtnQuotationedit.Text = DbtnQuotationedit.Text.ToUpper
            DbtnQuotationCancel.Text = DbtnQuotationCancel.Text.ToUpper
            DbtnCash.Text = DbtnCash.Text.ToUpper
            DbtnF2.Text = DbtnF2.Text.ToUpper

            DgrpQuotation.ForeColorInner = Color.FromArgb(54, 54, 54)
            DgrpPayments.ForeColorInner = Color.FromArgb(54, 54, 54)
            DgrpCommon.ForeColorInner = Color.FromArgb(54, 54, 54)
            DgrpExtraKey.ForeColorInner = Color.FromArgb(54, 54, 54)


            DgrpQuotation.ForeColorOuter = Color.FromArgb(0, 107, 163)
            DgrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)
            DgrpCommon.ForeColorOuter = Color.FromArgb(0, 107, 163)
            DgrpExtraKey.ForeColorOuter = Color.FromArgb(0, 107, 163)
        End If

    End Sub

    Private Sub ExitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        If Not objParentForm Is Nothing Then
            If ExitButton.Tag = "" Then
                objParentForm.close()
            End If
        End If
    End Sub

    Private Sub pAddRbnTab()
        'DbtnCash.Text = "New Bill"
        DbtnCash.Text = getValueByKey("dbtncash")
        DbtnCash.Name = "btnCM1"
        DbtnCash.LargeImage = Global.Spectrum.My.Resources.Resources._new

        'DbtnSOnew.Text = "New sales Order"
        DbtnSOnew.Text = getValueByKey("dbtnsonew")
        DbtnSOnew.Name = "btnSOnew"
        DbtnSOnew.LargeImage = Global.Spectrum.My.Resources.Resources._new


        'DbtnSOedit.Text = "Edit/Close sales Order"
        DbtnSOedit.Text = getValueByKey("dbtnsoedit")
        DbtnSOedit.Name = "btnSOedit"
        DbtnSOedit.LargeImage = Global.Spectrum.My.Resources.Resources.edit

        'DbtnSoCancel.Text = "Cancel sales Order"
        DbtnSoCancel.Text = getValueByKey("dbtnsocancel")
        DbtnSoCancel.Name = "btnSOcancel"
        DbtnSoCancel.LargeImage = Global.Spectrum.My.Resources.Resources.cancel_so


        'DbtnBLnew.Text = "New Birthlist"
        DbtnBLnew.Text = getValueByKey("dbtnblnew")
        DbtnBLnew.Name = "btnBLnew"
        DbtnBLnew.LargeImage = Global.Spectrum.My.Resources.Resources.new_bl

        'DbtnBLedit.Text = "Edit/Close Birthlist"
        DbtnBLedit.Text = getValueByKey("dbtnbledit")
        DbtnBLedit.Name = "btnBLedit"
        DbtnBLedit.LargeImage = Global.Spectrum.My.Resources.Resources.edit_bl

        'DbtnBLSale.Text = "sales For Birthlist"
        DbtnBLSale.Text = getValueByKey("dbtnblsale")
        DbtnBLSale.Name = "btnBLsales"
        DbtnBLSale.LargeImage = Global.Spectrum.My.Resources.Resources.cancel_bl



        DbtnQuotationnew.Text = getValueByKey("frmnquotationcreation.rbbtnsonew")
        DbtnQuotationnew.Name = "btnBLnew"
        DbtnQuotationnew.LargeImage = Global.Spectrum.My.Resources.Resources.new_bl

        'DbtnBLedit.Text = "Edit/Close Birthlist"
        DbtnQuotationedit.Text = getValueByKey("frmnquotationcreation.rbbtnsoedit")
        DbtnQuotationedit.Name = "btnBLedit"
        DbtnQuotationedit.LargeImage = Global.Spectrum.My.Resources.Resources.edit_bl

        'DbtnBLSale.Text = "sales For Birthlist"
        DbtnQuotationCancel.Text = getValueByKey("frmnquotationcreation.rbbtnsocancel")
        DbtnQuotationCancel.Name = "btnBLsales"
        DbtnQuotationCancel.LargeImage = Global.Spectrum.My.Resources.Resources.cancel_bl


        DbtnQuotationnew.LargeImage = Global.Spectrum.My.Resources.Resources.Create_Quotation
        DbtnQuotationedit.LargeImage = Global.Spectrum.My.Resources.Resources.Edit_Quotation
        DbtnQuotationCancel.LargeImage = Global.Spectrum.My.Resources.Resources.Cancel_Quotation


        If Not objParentForm Is Nothing Then

            If UCase(objParentForm.name) = "FRMNSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMNSALESORDERCANCEL" Or UCase(objParentForm.name) = "FRMNSALESORDERUPDATE" Or _
                UCase(objParentForm.name) = "FRMPCSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCNSALESORDERUPDATE" Or _
               UCase(objParentForm.name) = "FRMNBIRTHLISTCREATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTSALES" Or UCase(objParentForm.name) = "FRMNBIRTHLISTUPDATE" Or UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then

                Dim DtbCash As New C1.Win.C1Ribbon.RibbonTab
                Dim DgrpCash As New C1.Win.C1Ribbon.RibbonGroup
                'DtbCash.Text = "CashMemo"
                DtbCash.Text = getValueByKey("dtbcash")
                DtbCash.Name = "tbCM1"
                'DgrpCash.Text = "Billing"
                DgrpCash.Text = getValueByKey("dgrpcash")
                DgrpCash.Name = "grpCM1"
                Me.Tabs.Insert(0, DtbCash)
                'Me.Tabs.Add(DtbCash)
                DtbCash.Groups.Add(DgrpCash)
                DgrpCash.Items.Add(DbtnCash)
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    DgrpCash.ForeColorInner = Color.FromArgb(54, 54, 54)
                    DgrpCash.ForeColorOuter = Color.FromArgb(0, 107, 163)
                    DtbCash.Text = DtbCash.Text.ToUpper
                    DtbCash.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
                End If
            End If

            If UCase(objParentForm.name) = "FRMCASHMEMO" Or _
               UCase(objParentForm.name) = "FRMNBIRTHLISTCREATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTSALES" Or UCase(objParentForm.name) = "FRMNBIRTHLISTUPDATE" Or UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then

                Dim DtbSO As New C1.Win.C1Ribbon.RibbonTab
                Dim DgrpSO As New C1.Win.C1Ribbon.RibbonGroup
                'DtbSO.Text = "Sales Order"
                DtbSO.Text = getValueByKey("dtbso")
                DtbSO.Name = "tbSO1"
                'DgrpSO.Text = "Sales Order"
                DgrpSO.Text = getValueByKey("dgrpso")
                DgrpSO.Name = "grpSO1"
                Me.Tabs.Insert(1, DtbSO)
                'Me.Tabs.Add(DtbSO)
                DtbSO.Groups.Add(DgrpSO)
                DgrpSO.Items.Add(DbtnSOnew)
                DgrpSO.Items.Add(DbtnSOedit)
                DgrpSO.Items.Add(DbtnSoCancel)
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    DgrpSO.ForeColorInner = Color.FromArgb(54, 54, 54)
                    DgrpSO.ForeColorOuter = Color.FromArgb(0, 107, 163)
                    DtbSO.Text = DtbSO.Text.ToUpper
                    DtbSO.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
                End If
            End If

            If UCase(objParentForm.name) = "FRMCASHMEMO" Or _
                UCase(objParentForm.name) = "FRMPCSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCNSALESORDERUPDATE" Or _
               UCase(objParentForm.name) = "FRMNSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMNSALESORDERCANCEL" Or UCase(objParentForm.name) = "FRMNSALESORDERUPDATE" Or UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then

                If Not dtAuthUserTran Is Nothing AndAlso dtAuthUserTran.Rows.Count > 0 Then
                    Dim dv As New DataView(dtAuthUserTran, "Rights=1 AND AUTHTRANSACTIONCODE IN ('BirthList','BirthListCreate','BirthListSales','BirthListUpdate','ReprintBL')", "", DataViewRowState.CurrentRows)

                    If dv.Count > 0 Then
                        Dim DtbBL As New C1.Win.C1Ribbon.RibbonTab
                        Dim DgrpBL As New C1.Win.C1Ribbon.RibbonGroup
                        'DtbBL.Text = "Birth list"
                        DtbBL.Text = getValueByKey("dtbbl")
                        DtbBL.Name = "tbBL1"
                        'DgrpBL.Text = "Birth list"
                        DgrpBL.Text = getValueByKey("dgrpbl")
                        DgrpBL.Name = "grpBL1"
                        If CheckAuthorisation(clsAdmin.UserCode, "BLMain") Then
                            Me.Tabs.Insert(2, DtbBL)
                            DtbBL.Groups.Add(DgrpBL)
                            DgrpBL.Items.Add(DbtnBLnew)
                            DgrpBL.Items.Add(DbtnBLedit)
                            DgrpBL.Items.Add(DbtnBLSale)
                        End If
                        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                            DgrpBL.ForeColorInner = Color.FromArgb(54, 54, 54)
                            DgrpBL.ForeColorOuter = Color.FromArgb(0, 107, 163)
                            DtbBL.Text = DtbBL.Text.ToUpper
                            DtbBL.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
                        End If
                    End If
                End If
            End If

            If UCase(objParentForm.name) = "FRMCASHMEMO" Or _
                UCase(objParentForm.name) = "FRMPCSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCNSALESORDERUPDATE" Or _
               UCase(objParentForm.name) = "FRMNSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMNSALESORDERCANCEL" Or UCase(objParentForm.name) = "FRMNSALESORDERUPDATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTCREATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTSALES" Or UCase(objParentForm.name) = "FRMNBIRTHLISTUPDATE" Then

                If Not dtAuthUserTran Is Nothing AndAlso dtAuthUserTran.Rows.Count > 0 Then
                    Dim dv As New DataView(dtAuthUserTran, "Rights=1 AND AUTHTRANSACTIONCODE = 'SHW_Quotation'", "", DataViewRowState.CurrentRows)

                    If dv.Count > 0 Then
                        Dim DtbBL As New C1.Win.C1Ribbon.RibbonTab
                        Dim DgrpBL As New C1.Win.C1Ribbon.RibbonGroup
                        'DtbBL.Text = "Birth list"
                        DtbBL.Text = getValueByKey("frmnquotationcreation.rbntabso")
                        DtbBL.Name = "tbQt1"
                        'DgrpBL.Text = "Birth list"
                        DgrpBL.Text = getValueByKey("frmnquotationcreation.rbgrpso")
                        DgrpBL.Name = "grpQt1"
                        Me.Tabs.Add(DtbBL)
                        DtbBL.Groups.Add(DgrpBL)
                        DgrpBL.Items.Add(DbtnQuotationnew)
                        DgrpBL.Items.Add(DbtnQuotationedit)
                        DgrpBL.Items.Add(DbtnQuotationCancel)
                        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                            DgrpBL.ForeColorInner = Color.FromArgb(54, 54, 54)
                            DgrpBL.ForeColorOuter = Color.FromArgb(0, 107, 163)
                            DtbBL.Text = DtbBL.Text.ToUpper
                            DtbBL.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
                        End If
                    End If
                End If
            End If


            '' Add Payment Group
            If UCase(objParentForm.name) = "FRMNSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMNSALESORDERCANCEL" Or UCase(objParentForm.name) = "FRMNSALESORDERUPDATE" Or UCase(objParentForm.name) = "FRMPCSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCNSALESORDERUPDATE" Then
                PAddPaymentGroup(1)
            ElseIf UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then
                If CheckAuthorisation(clsAdmin.UserCode, "BLMain") Then
                    PAddPaymentGroup(3)
                Else
                    PAddPaymentGroup(2)
                End If

            ElseIf UCase(objParentForm.name) = "FRMNBIRTHLISTSALES" Or UCase(objParentForm.name) = "FRMNBIRTHLISTUPDATE" Then
                PAddPaymentGroup(2)
            End If

            '' end of payment group


        End If
    End Sub

    Private Sub PAddPaymentGroup(ByVal RbnTabIndex As Int16)

        '  If Me.Tabs(0).Name.ToUpper = "RBNTABSO" Then

        'DgrpPayments.Text = "Payments"
        DgrpPayments.Name = "grpPayments"
        If UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then
            DgrpPayments.Text = "Save"
        ElseIf getValueByKey(DgrpPayments.Name.ToLower) <> String.Empty Then
            DgrpPayments.Text = getValueByKey(DgrpPayments.Name.ToLower)
        End If

        DbtnPay.Text = "Pay  (F4)"

        DbtnPay.Name = "btnPay"
        If getValueByKey(DbtnPay.Name.ToLower) <> String.Empty Then
            DbtnPay.Text = getValueByKey(DbtnPay.Name.ToLower)

        End If
        DbtnPay.LargeImage = Global.Spectrum.My.Resources.Resources.payment_16
        DbtnPay.ShortcutKeys = Keys.F4
        DbtnPay.ShortcutKeyDisplayString = "F4"
        'DbtnPay.ToolTip = " Pay by one or more Tender Mode"
        DbtnPay.ToolTip = getValueByKey("tp003")



        DbtnPayCash.Name = "btnPayCash"
        If UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then
            DbtnPayCash.Text = getValueByKey("QO075")
            DbtnPay.Text = getValueByKey("QO076")
        ElseIf getValueByKey(DbtnPayCash.Name.ToLower) <> String.Empty Then
            DbtnPayCash.Text = getValueByKey(DbtnPayCash.Name.ToLower)
        End If
        'DbtnPayCash.Text = "Pay by Cash (F5)"
        DbtnPayCash.LargeImage = Global.Spectrum.My.Resources.Resources.cash
        DbtnPayCash.ShortcutKeys = Keys.F5
        DbtnPayCash.ShortcutKeyDisplayString = "F5"
        'DbtnPayCash.ToolTip = " Pay by Cash Only"
        DbtnPayCash.ToolTip = getValueByKey("tp004")


        DbtnPayCard.Name = "btnPayCard"
        If getValueByKey(DbtnPayCard.Name.ToLower) <> String.Empty Then
            DbtnPayCard.Text = getValueByKey(DbtnPayCard.Name.ToLower)
        End If
        'DbtnPayCard.Text = "Pay by Card (F6)"
        DbtnPayCard.LargeImage = Global.Spectrum.My.Resources.Resources.card2
        DbtnPayCard.ShortcutKeys = Keys.F6
        DbtnPayCard.ShortcutKeyDisplayString = "F6"
        'DbtnPayCard.ToolTip = " Pay by Card Only"
        DbtnPayCard.ToolTip = getValueByKey("tp001")


        DbtnpayCheque.Name = "btnPayCheque"
        If getValueByKey(DbtnpayCheque.Name.ToLower) <> String.Empty Then
            DbtnpayCheque.Text = getValueByKey(DbtnpayCheque.Name.ToLower)
        End If
        'DbtnpayCheque.Text = "Pay by Cheque (F7)"
        DbtnpayCheque.LargeImage = Global.Spectrum.My.Resources.Resources.chque
        DbtnpayCheque.ShortcutKeys = Keys.F7
        DbtnpayCheque.ShortcutKeyDisplayString = "F7"
        'DbtnpayCheque.ToolTip = " Pay by Cheque Only"
        DbtnpayCheque.ToolTip = getValueByKey("tp002")

        '' 'vipin PC SO Merge 03-05-2018
        DbtnPayNEFT.Name = "btnPayNEFT"
        DbtnPayNEFT.Text = "Pay by NEFT"
        DbtnPayNEFT.LargeImage = Global.Spectrum.My.Resources.Resources.cash_btm
        If getValueByKey(DbtnPayNEFT.Name.ToLower) <> String.Empty Then
            DbtnPayNEFT.Text = getValueByKey(DbtnPayNEFT.Name.ToLower)
        End If
        DbtnPayNEFT.ToolTip = "Pay by NEFT only"

        DbtnPayRTGS.Name = "btnPayRTGS"
        DbtnPayRTGS.Text = "Pay by RTGS"
        If getValueByKey(DbtnPayRTGS.Name.ToLower) <> String.Empty Then
            DbtnPayRTGS.Text = getValueByKey(DbtnPayRTGS.Name.ToLower)
        End If
        DbtnPayRTGS.LargeImage = Global.Spectrum.My.Resources.Resources.RTGS
        DbtnPayRTGS.ToolTip = "Pay by RTGS only"
        '----------------------------
        Me.Tabs(RbnTabIndex).Groups.Add(DgrpPayments)
        If UCase(objParentForm.name) <> "FRMNQUOTATIONCREATION" AndAlso UCase(objParentForm.name) <> "frmNQuotationUpdate".ToUpper() AndAlso UCase(objParentForm.name) <> "frmNQuotationCancel".ToUpper() Then
            DgrpPayments.Items.Add(DbtnPay)
            DgrpPayments.Items.Add(DbtnPayCard)
            DgrpPayments.Items.Add(DbtnpayCheque)
            DgrpPayments.Items.Add(DbtnPayNEFT)  '' 'vipin PC SO Merge 03-05-2018
            DgrpPayments.Items.Add(DbtnPayRTGS)
        End If
        If UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Then
            DbtnPay.LargeImage = My.Resources.Converttosales_order
            DgrpPayments.Items.Add(DbtnPay)
        End If
        DgrpPayments.Items.Add(DbtnPayCash)
        'DgrpPayments.Items.Add(DbtnPay)
        ' End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            DgrpPayments.Text = DgrpPayments.Text.ToUpper
        End If
    End Sub
    Private Sub pAddConfigTools()

        Dim intBtnSeq As Int16 = 0

        ' key help menu


        If UCase(objParentForm.name) = "FRMCASHMEMO" Or UCase(objParentForm.name) = "FRMNSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCSALESORDERCREATION" Or UCase(objParentForm.name) = "FRMPCNSALESORDERUPDATE" Or UCase(objParentForm.name) = "FRMNQUOTATIONCREATION" Or UCase(objParentForm.name) = "FRMFASTCASHMEMO" Or UCase(objParentForm.name) = "FRMTABCASHMEMO" Or UCase(objParentForm.name) = "FRMNSALESORDERUPDATE" Or UCase(objParentForm.name) = "frmNQuotationUpdate".ToUpper() Or UCase(objParentForm.name) = "frmNQuotationCancel".ToUpper() Then
            'DbtnSplitKeyHelp.Text = "ExtraKeys-&Z"
            'DbtnSplitKeyHelp.Name = "DbtnSplitKeyHelp"
            'DbtnSplitKeyHelp.LargeImage = Global.Spectrum.My.Resources.Resources.payment_16
            'Me.ConfigToolBar.Items.Add(DbtnSplitKeyHelp)
            'Me.ConfigToolBar.Items(intBtnSeq).Description = "Extra ShortCut Keys"
            'Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
            'Me.ConfigToolBar.Items(intBtnSeq).Visible = True

            Me.Tabs(0).Groups.Add(DgrpExtraKey)

            DgrpExtraKey.Visible = True
            If UCase(objParentForm.name) = "FRMCASHMEMO" Then
                'DbtnF12.Text = "F12-Price Change"
                DbtnF12.Text = getValueByKey("dbtnf12")
                'DbtnSplitKeyHelp.Items.Add(DbtnF12)
                DgrpExtraKey.Items.Add(DbtnF12)
            End If

            'DbtnF2.Text = "F2-Change Qty"
            DbtnF2.Text = getValueByKey("dbtnf2")
            'DbtnSplitKeyHelp.Items.Add(DbtnF2)
            DgrpExtraKey.Items.Add(DbtnF2)
            'intBtnSeq = intBtnSeq + 1
        End If

        'Change by Ashish for CR 5679 - Birthlist Price change
        If UCase(objParentForm.name) = "FRMNBIRTHLISTCREATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTUPDATE" Or UCase(objParentForm.name) = "FRMNBIRTHLISTSALES" Then
            Me.Tabs(2).Groups.Add(DgrpExtraKey)
            'DbtnF12.Text = "F12-Price Change"
            DbtnF12.Text = getValueByKey("dbtnf12")
            'DbtnSplitKeyHelp.Items.Add(DbtnF12)
            DgrpExtraKey.Items.Add(DbtnF12)
        End If
        'End of change

        'DbtnSwitchUser
        'Rakesh:29-July-2013:CR-00> Switch user rename as Log Off user
        'DbtnSwitchUser.Text = "Switch &User"
        'If UCase(objParentForm.name) = "FRMFASTCASHMEMO" Then
        '    DbtnSwitchUser.Text = "Log off"
        'Else
        DbtnSwitchUser.Text = getValueByKey("dbtnswitchuser1")
        'End If

        DbtnSwitchUser.Name = "btnSwitchUser"
        DbtnSwitchUser.LargeImage = Global.Spectrum.My.Resources.Resources.payment_16
        Me.ConfigToolBar.Items.Add(DbtnSwitchUser)

        'Me.ConfigToolBar.Items(intBtnSeq).Description = "Switch User"
        Me.ConfigToolBar.Items(intBtnSeq).Description = getValueByKey("tp005")
        Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
        Me.ConfigToolBar.Items(intBtnSeq).Visible = True


        intBtnSeq = intBtnSeq + 1
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.ConfigToolBar.Items.Add(DbtnSlash1)
        End If

        'DbtnonScreenKey.Text = "OnScreen&Key"
        DbtnonScreenKey.Text = getValueByKey("dbtnonscreenkey")
        DbtnonScreenKey.Name = "DbtnonScreenKey"
        DbtnonScreenKey.LargeImage = Global.Spectrum.My.Resources.Resources.close
        Me.ConfigToolBar.Items.Add(DbtnonScreenKey)

        'Me.ConfigToolBar.Items(intBtnSeq).Description = "On Screen Keyboard"
        Me.ConfigToolBar.Items(intBtnSeq).Description = getValueByKey("tp006")
        Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
        Me.ConfigToolBar.Items(intBtnSeq).Visible = True
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            DbtnSwitchUser.Font = New Font("Neo Sans", 7, FontStyle.Regular)
            DbtnonScreenKey.Font = New Font("Neo Sans", 7, FontStyle.Regular)
            DbtnGlobalCashMemo.Font = New Font("Neo Sans", 7, FontStyle.Regular)
            DbtnOpenDrawer.Font = New Font("Neo Sans", 7, FontStyle.Regular)
            DbtnTopRightExit.Font = New Font("Neo Sans", 7, FontStyle.Regular)
            DbtnonScreenKey.ToolTip = DbtnonScreenKey.Text.Replace("&", "")
            DbtnSwitchUser.ToolTip = DbtnSwitchUser.Text
            DbtnSwitchUser.Text = ""
            DbtnonScreenKey.Text = ""
            DbtnonScreenKey.LargeImage = My.Resources.onscreen_key
            DbtnSwitchUser.LargeImage = My.Resources.LogOffFlatIcon
            Me.ConfigToolBar.Items.Add(DbtnSlash2)
        End If
        If UCase(objParentForm.name) <> "FRMCASHMEMO" Then
            intBtnSeq = intBtnSeq + 1
            'DbtnGlobalCashMemo.Text = "CashMemo&Y"
            DbtnGlobalCashMemo.Text = getValueByKey("dbtnglobalcashmemo")
            DbtnGlobalCashMemo.Name = "DbtnGlobalCashMemo"
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                DbtnGlobalCashMemo.ToolTip = DbtnGlobalCashMemo.Text.Replace("&", "").Replace("Y", "")
                DbtnGlobalCashMemo.Text = ""
                DbtnGlobalCashMemo.LargeImage = My.Resources.CashMemoTopRight
            Else
                DbtnGlobalCashMemo.LargeImage = Global.Spectrum.My.Resources.Resources.cash_btm
            End If
            Me.ConfigToolBar.Items.Add(DbtnGlobalCashMemo)
            'Me.ConfigToolBar.Items(intBtnSeq).Description = "ShortCut For CashMemo"
            Me.ConfigToolBar.Items(intBtnSeq).Description = getValueByKey("tp007")
            Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
            Me.ConfigToolBar.Items(intBtnSeq).Visible = True
        End If

        ' add drawer open button


        ' add drawer open button
        intBtnSeq = intBtnSeq + 1
        DbtnOpenDrawer.Text = getValueByKey("opendrawer")
        DbtnOpenDrawer.Name = "DbtnOpenDrawer"
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.ConfigToolBar.Items.Add(DbtnSlash3)
            DbtnOpenDrawer.ToolTip = DbtnOpenDrawer.Text
            DbtnOpenDrawer.Text = ""
            DbtnOpenDrawer.LargeImage = My.Resources.Open_Drawer
        Else
            DbtnOpenDrawer.LargeImage = Global.Spectrum.My.Resources.Resources.close
        End If
        Me.ConfigToolBar.Items.Add(DbtnOpenDrawer)
        Me.ConfigToolBar.Items(intBtnSeq).Description = getValueByKey("tp008")
        Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
        Me.ConfigToolBar.Items(intBtnSeq).Visible = True


        intBtnSeq = intBtnSeq + 1
        'DbtnTopRightExit.Text = "E&Xit"
        DbtnTopRightExit.Text = getValueByKey("dbtntoprightexit")
        DbtnTopRightExit.Name = "btnTopRightExit"
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            Me.ConfigToolBar.Items.Add(DbtnSlash4)
            DbtnSlash4.Enabled = False
            DbtnTopRightExit.ToolTip = DbtnTopRightExit.Text.Replace("&", "")
            DbtnTopRightExit.Text = ""
            DbtnTopRightExit.LargeImage = My.Resources.Exitflat
        Else
            DbtnTopRightExit.LargeImage = Global.Spectrum.My.Resources.Resources.close
        End If
        Me.ConfigToolBar.Items.Add(DbtnTopRightExit)
        'Me.ConfigToolBar.Items(intBtnSeq).Description = "Exit"
        Me.ConfigToolBar.Items(intBtnSeq).Description = getValueByKey("tp008")
        Me.ConfigToolBar.Items(intBtnSeq).Enabled = True
        Me.ConfigToolBar.Items(intBtnSeq).Visible = True

        'Me.Tabs(0).Groups.Add(DgrpCommon)
        'DgrpCommon.Items.Add(DbtnSwitchUser)

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            DbtnGlobalCashMemo.Text = DbtnGlobalCashMemo.Text.ToUpper
            DbtnOpenDrawer.Text = DbtnOpenDrawer.Text.ToUpper
            DbtnTopRightExit.Text = DbtnTopRightExit.Text.ToUpper
            DbtnSlash1.Enabled = False
            DbtnSlash2.Enabled = False
            DbtnSlash3.Enabled = False
            DbtnSlash4.Enabled = False

            If UCase(objParentForm.name) = "FRMCASHMEMO" Then
                DbtnSlash3.Visible = False
            ElseIf UCase(objParentForm.name) = "FRMFASTCASHMEMO" Or UCase(objParentForm.name) = "FRMTABCASHMEMO" Then
                DbtnSlash2.Visible = False
                DbtnSlash3.Visible = False
                DbtnSlash4.Visible = False

            End If

        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            DgrpExtraKey.Text = DgrpExtraKey.Text.ToUpper
        End If
    End Sub
    Private Sub PAddCommonGroup()

        'DgrpCommon.Text = "Common"
        DgrpCommon.Text = getValueByKey("dgrpcommon")
        DgrpCommon.Name = "grpCommon"

        'DbtnSwitchUser
        'DbtnSwitchUser.Text = "Switch User"
        DbtnSwitchUser.Text = getValueByKey("tp005")
        DbtnSwitchUser.Name = "btnSwitchUser"
        DbtnSwitchUser.LargeImage = Global.Spectrum.My.Resources.Resources.payment_16

        Me.Tabs(0).Groups.Add(DgrpCommon)
        DgrpCommon.Items.Add(DbtnSwitchUser)

    End Sub

    Private Sub DbtnCash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnCash.Click
        Dim frm As New frmCashMemo
        MDISpectrum.MenuStrip.Hide()
        MDISpectrum.ShowChildForm(frm, True)
        ' MDISpectrum.MenuStrip.Hide()
    End Sub

    Private Sub DbtnBLedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnBLedit.Click
        Dim frm As New frmNBirthListUpdate
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnBLnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnBLnew.Click
        Dim frm As New frmNBirthListCreate
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnBLSale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnBLSale.Click
        Dim frm As New frmNBirthListSales
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnSoCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnSoCancel.Click
        Dim frm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            frm = New frmPCNSalesOrderUpdate
            frm.SoCancel = True
        Else
            frm = New frmNSalesOrderCancel
        End If
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnSOedit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnSOedit.Click
        Dim frm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            frm = New frmPCNSalesOrderUpdate
        Else
            frm = New frmNSalesOrderUpdate
        End If
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnSOnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnSOnew.Click
        Dim frm
        If clsDefaultConfiguration.IsNewSalesOrder Then
            frm = New frmPCSalesOrderCreation
        Else
            frm = New frmNSalesOrderCreation
        End If
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnQuotationnew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DbtnQuotationnew.Click
        Dim frm As New frmNQuotationCreation
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnQuotationedit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DbtnQuotationedit.Click
        Dim frm As New frmNQuotationUpdate
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnQuotationCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DbtnQuotationCancel.Click
        Dim frm As New frmNQuotationCancel
        MDISpectrum.ShowChildForm(frm, True)
    End Sub

    Private Sub DbtnSwitchUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnSwitchUser.Click
        Dim eventType As Int32

        'Rakesh:29-July-2013:CR-00> Switch user - when click, message should show as – Do you want to log off  Yes / No.
        'ShowMessage(clsAdmin.UserName & " are you sure you want to Log off?", "CM014 - " & getValueByKey("CLAE04"), eventType, True, "Exit")

        Dim confirmLogOffMsg As String = getValueByKey("dbtnswitchuserMsg")
        ShowMessage(getValueByKey("dbtnswitchuserMsg"), "CM014 - " & getValueByKey("CLAE04"), eventType, True, "No", "Yes")
        'ShowMessage(clsAdmin.UserName & " are you sure you want to Log off?", "CM014 - " & getValueByKey("CLAE04"), eventType, True, "Exit")
        'ShowMessage("You have a bill on the Screen Hold first.", "Information", eventType)

        If eventType = 1 Then
            If (String.Equals(objParentForm.name, frmFastCashMemo.Name)) Or (String.Equals(objParentForm.name, frmTabCashMemo.Name)) Then
                objParentForm.close()
            End If

            Dim frm As New frmNLogin
            frm.btnCancel.Text = getValueByKey("tp008")
            'frm.btnCancel.Text = "Exit"
            LoginStatus = False
            frm.ShowDialog()
            If CheckAuthorisation(clsAdmin.UserCode, objParentForm.frmtrancode) = False Then
                ShowMessage(getValueByKey("SPCM001"), "SPCM001")
                'ShowMessage("You have not Sufficent Rights", "Information")
                objParentForm.close()
                Exit Sub

                'objParentForm.fnSetUserName()
            End If

        ElseIf eventType = 2 Then
            Exit Sub
        End If
    End Sub

    Private Sub DbtnTopRightExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnTopRightExit.Click
        If Not objParentForm Is Nothing Then
            If ExitButton.Tag = "" Then
                objParentForm.close()
            End If
        End If
    End Sub

    Private Sub DtbnCloseForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtbnCloseForm.Click
        objParentForm.close()
    End Sub

    Private Sub DbtnonScreenKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnonScreenKey.Click
        Process.Start("osk.exe")
    End Sub
    Private Sub pSetTransactionCode()
        If Not objParentForm Is Nothing Then
            Select Case UCase(objParentForm.Name)
                Case "FRMCASHMEMO"
                    objParentForm.frmtrancode = "Billing"
                Case "FRMNBIRTHLISTCREATE"
                    objParentForm.frmtrancode = "BirthListCreate"
                Case "FRMNBIRTHLISTSALES"
                    objParentForm.frmtrancode = "BirthListSales"
                Case "FRMNBIRTHLISTUPDATE"
                    objParentForm.frmtrancode = "BirthListUpdate"
                Case "FRMNSALESORDERCREATION"
                    objParentForm.frmtrancode = "SOCreation"
                Case "FRMNSALESORDERUPDATE"
                    objParentForm.frmtrancode = "SOUpdation"
                Case "FRMNSALESORDERCANCEL"
                    objParentForm.frmtrancode = "SOCancel"
                Case "FRMNQUOTATIONCREATION"
                    objParentForm.frmtrancode = "Create_Quotat"
                Case "FRMNQUOTATIONCANCEL"
                    objParentForm.frmtrancode = "Cancel_Quotat"
                Case "FRMNQUOTATIONUPDATE"
                    objParentForm.frmtrancode = "Update_Quotat"
            End Select
        End If
    End Sub

    Private Sub DbtnGlobalCashMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnGlobalCashMemo.Click
        DbtnCash_Click(sender, e)
    End Sub

    Private Sub DbtnOpenDrawer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DbtnOpenDrawer.Click
        Try
            If CheckAuthorisation(clsAdmin.UserCode, "OPENDRAWER") Then
                Dim cA4Print As New clsA4Print
                cA4Print.CashDrawerWithoutDriver = clsAdmin.CashDrawerWithoutDriver
                cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
            Else
                Dim objAuthUser As New frmNUserAuthorisation
                objAuthUser.ShowDialog()
                If objAuthUser.Authorized = True Then
                    Dim cA4Print As New clsA4Print
                    cA4Print.CashDrawerWithoutDriver = clsAdmin.CashDrawerWithoutDriver
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
                objAuthUser.Close()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
