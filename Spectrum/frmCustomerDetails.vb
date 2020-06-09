Public Class frmCustomerDetails

    Private dtCustomerDetailsD As DataTable

    Private Sub cboAddressType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub New(ByVal drCustomerDetails As DataTable)
        If Not drCustomerDetails Is Nothing Then
            dtCustomerDetailsD = drCustomerDetails
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
        Else
            ShowMessage(getValueByKey("CD01"), "CD01 - " & getValueByKey("CLAE04"))

        End If
        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub frmCustomerDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            DataBindToControl()
            pC1ComboSetDisplayMember(cboAddressType)
            SetCulture(Me, Me.Name)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Function DataBindToControl() As Boolean
        Try
            lblCustomerID.DataBindings.Add("Text", dtCustomerDetailsD, "CustomerNO")
            lblCustomerName.DataBindings.Add("Text", dtCustomerDetailsD, "CustomerName")
            lblCustomerType.DataBindings.Add("Text", dtCustomerDetailsD, "CustomerType")
            lblEmailID.DataBindings.Add("Text", dtCustomerDetailsD, "EmailID")
            lblMobileNumber.DataBindings.Add("Text", dtCustomerDetailsD, "MOBILENO")
            'cboAddressType.DataBindings.Add("SelectedText", dtCustomerDetailsD, "ADDRESSTYPENAME")
            'cboAddressType.DataBindings.Add("SelectedValue", dtCustomerDetailsD, "ADDRESSTYPE")
            cboAddressType.DataSource = dtCustomerDetailsD
            cboAddressType.ValueMember = "ADDRESSTYPE"
            cboAddressType.DisplayMember = "ADDRESSTYPENAME"
            rtxtAddress.DataBindings.Add("Text", dtCustomerDetailsD, "ADDRESS", True, DataSourceUpdateMode.OnPropertyChanged)
            lblBirthDate.DataBindings.Add("Text", dtCustomerDetailsD, "BirthDate", True, DataSourceUpdateMode.Never, Nothing, "d")
            lblGender.DataBindings.Add("Text", dtCustomerDetailsD, "Gender")

            If lblCustomerType.Text = "CLP" Then
                'Me.Text = "CLP Customer Details"
                Me.Text = getValueByKey("frmcustomerdetails1")
                grpBoxCLpCustomerDetails.Visible = True
                lblCardType.DataBindings.Add("Text", dtCustomerDetailsD, "CardType")
                lblPointsAccumlated.DataBindings.Add("Text", dtCustomerDetailsD, "PointsAccumlated")

                lblBalancePoint.DataBindings.Add("Text", dtCustomerDetailsD, "BalancePoint")
                If Not lblPointsAccumlated.Text Is String.Empty Then
                    lblPointsAccumlated.Text = Math.Round(CDbl(lblPointsAccumlated.Text), 2).ToString()
                End If


            Else
                'Me.Text = "SO Customer Details"
                Me.Text = getValueByKey("frmcustomerdetails2")
                grpBoxCLpCustomerDetails.Visible = False
            End If

            'cboAddressType.SelectedIndex = -1
            'cboAddressType.SelectedIndex = 0

            'If cboAddressType.SelectedIndex = -1 Then

            'End If
            cboAddressType.Text = cboAddressType.WillChangeToText

            Return True

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub
    Public Function Themechange() As String
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        Me.Label1.ForeColor = Color.Black
        Me.Label1.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label1.Width = 172
        Me.Label1.Height = 27

        Me.Label2.ForeColor = Color.Black
        Me.Label2.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label2.Width = 172
        Me.Label2.Height = 27

        Me.Label3.ForeColor = Color.Black
        Me.Label3.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label3.Width = 172
        Me.Label3.Height = 27

        Me.Label4.ForeColor = Color.Black
        Me.Label4.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label4.Width = 172
        Me.Label4.Height = 35

        Me.Label5.ForeColor = Color.Black
        Me.Label5.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label5.Width = 172
        Me.Label5.Height = 27

        Me.Label6.ForeColor = Color.Black
        Me.Label6.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label6.Width = 172
        Me.Label6.Height = 63
        Me.Label6.Location = New System.Drawing.Point(12, 273)

        Me.Label7.ForeColor = Color.Black
        Me.Label7.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label7.Width = 172
        Me.Label7.Height = 27

        Me.Label8.ForeColor = Color.Black
        Me.Label8.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label8.Width = 172
        Me.Label8.Height = 27

        Me.Label10.ForeColor = Color.Black
        Me.Label10.BackColor = Color.FromArgb(212, 212, 212)
        Me.Label10.Width = 172
        Me.Label10.Height = 27

        Me.lblCustomerID.BackColor = Color.White
        Me.lblCustomerID.Width = 309
        Me.lblCustomerID.Height = 27

        Me.lblCustomerType.BackColor = Color.White
        Me.lblCustomerType.Width = 309
        Me.lblCustomerType.Height = 27

        Me.lblCustomerName.BackColor = Color.White
        Me.lblCustomerName.Width = 309
        Me.lblCustomerName.Height = 27

        Me.lblMobileNumber.BackColor = Color.White
        Me.lblMobileNumber.Width = 309
        Me.lblMobileNumber.Height = 27

        Me.lblGender.BackColor = Color.White
        Me.lblGender.Width = 309
        Me.lblGender.Height = 27

        Me.lblBirthDate.BackColor = Color.White
        Me.lblBirthDate.Width = 309
        Me.lblBirthDate.Height = 27

        Me.lblEmailID.BackColor = Color.White
        Me.lblEmailID.Width = 309
        Me.lblEmailID.Height = 27

        Me.cboAddressType.Location = New System.Drawing.Point(184, 234)
        Me.cboAddressType.Width = 309
        Me.cboAddressType.Height = 35

        Me.rtxtAddress.Location = New System.Drawing.Point(183, 272)

        Me.grpBoxCLpCustomerDetails.Location = New System.Drawing.Point(119, 340)


        'Me.cmdOK
        '
        Me.cmdOK.Dock = DockStyle.None
        Me.cmdOK.BackColor = Color.FromArgb(0, 107, 163)
        Me.cmdOK.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdOK.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        Me.cmdOK.Location = New System.Drawing.Point(224, 440)
        Me.cmdOK.Size = New System.Drawing.Size(68, 30)
        Me.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.cmdOK.FlatStyle = FlatStyle.Flat
        Me.cmdOK.FlatAppearance.BorderSize = 0
        Me.cmdOK.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

    End Function
End Class
