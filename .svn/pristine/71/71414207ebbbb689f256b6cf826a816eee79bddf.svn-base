Imports SpectrumBL
Public Class CtrlSalesPerson
    Dim objComn As New clsCommon
    Private _AlignChange As String
    Public Property AlignChange() As String
        Get
            Return _AlignChange
        End Get
        Set(ByVal value As String)
            _AlignChange = value
        End Set
    End Property
    Private Sub CtrlSalesPerson_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
          
            Me.CtrlCmdSearch.Text = getValueByKey("ctrlcmdsearch")
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            Me.CtrlCmdSearch.Text = "Search"
        End Try

        LoadsalesData()

    End Sub
    Private Sub LoadsalesData()
        Try
            Dim dt As DataTable
            dt = objComn.GetSalesPerson(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                CtrlSalesPersons.DataSource = dt
                CtrlSalesPersons.DisplayMember = dt.Columns("SalesPersonName").ToString()
                CtrlSalesPersons.ValueMember = dt.Columns("EmpCode").ToString()
                CtrlSalesPersons.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In CtrlSalesPersons.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> CtrlSalesPersons.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                CtrlSalesPersons.SelectedIndex = -1
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub fnIsSalesPersonApplicable(ByVal TrueFalse As Boolean)
        CtrllabelSalesPerson.Visible = TrueFalse
        CtrlSalesPersons.Visible = TrueFalse
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CtrlSalesPersons_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlSalesPersons.SelectedValueChanged
        CtrlTxtBox.Select()
    End Sub
    Private Sub AndroidSearchTextBox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles AndroidSearchTextBox.Leave 'Handles txtSearchItem.Leave
        AndroidSearchTextBox.Text = ""
        If (AndroidSearchTextBox.IsListBoxVisible And AndroidSearchTextBox.IsMouseOverList = False) Then
            AndroidSearchTextBox.ResetListBox()
        End If
    End Sub
    Private Function ThemeChange()
        If AlignChange = "Cash Memo" Then
            '  Me.C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            'Me.CtrlTxtBox.Visible = False
            'Me.CtrllabelSalesPerson.BackColor = Color.FromArgb(255, 255, 255)
            'Me.CtrllabelSalesPerson.ForeColor = Color.FromArgb(37, 37, 37)
            'Me.CtrllabelSalesPerson.Font = New Font("Neo Sans", 8, FontStyle.Bold)
            'Me.CtrllabelSalesPerson.BorderStyle = Windows.Forms.BorderStyle.None
            '  Me.C1Sizer1.Grid.Rows(0).Size = 23
            ' Me.CtrlTxtBox.Visible = False
            ' AndroidSearchTextBox.MinimumSize = New Size(0, 22)
            Me.C1Sizer1.Grid.Columns(0).Size = 100
            Me.C1Sizer1.Grid.Columns(0).IsFixedSize = True
            Me.C1Sizer1.Grid.Columns(1).Size = 200
            Me.C1Sizer1.Grid.Columns(2).Size = 284
            Me.C1Sizer1.Grid.Columns(3).Size = 30
            Me.C1Sizer1.Grid.Columns(3).IsFixedSize = False
            Me.CtrllabelSalesPerson.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrllabelSalesPerson.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrllabelSalesPerson.BorderStyle = Windows.Forms.BorderStyle.None
            CtrllabelSalesPerson.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrllabelSalesPerson.Font = New Font("Neo Sans", 8, FontStyle.Regular)

            Me.CtrlSalesPersons.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlSalesPersons.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Black
            Me.CtrlSalesPersons.EditorBackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Font = New Font("Neo Sans", 8, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(0).BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(1).Font = New Font("Neo Sans", 9, FontStyle.Regular)



            Me.CtrlTxtBox.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlTxtBox.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlTxtBox.Font = New Font("Neo Sans", 8, FontStyle.Regular)


            Me.CtrlCmdSearch.Text = ""
            Me.CtrlCmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
            Me.CtrlCmdSearch.Image = Nothing
            Me.CtrlCmdSearch.BackgroundImage = Global.Spectrum.My.Resources.Resources.SearchItems
            Me.CtrlCmdSearch.FlatStyle = FlatStyle.Flat
            Me.CtrlCmdSearch.BackgroundImageLayout = ImageLayout.Stretch
        ElseIf AlignChange = "Fast Cash Memo" Then

            'Me.CtrllabelSalesPerson.BackColor = Color.FromArgb(255, 255, 255)
            'Me.CtrllabelSalesPerson.ForeColor = Color.FromArgb(37, 37, 37)
            'Me.CtrllabelSalesPerson.Font = New Font("Neo Sans", 8, FontStyle.Bold)
            'Me.CtrllabelSalesPerson.BorderStyle = Windows.Forms.BorderStyle.None


            'Me.CtrlSalesPersons.BackColor = Color.FromArgb(255, 255, 255)
            'Me.CtrlSalesPersons.ForeColor = Color.FromArgb(37, 37, 37)
            'Me.CtrlSalesPersons.EditorBackColor = Color.FromArgb(255, 255, 255)
            'Me.CtrlSalesPersons.Font = New Font("Neo Sans", 8, FontStyle.Regular)


            'Me.CtrlTxtBox.BackColor = Color.FromArgb(255, 255, 255)
            'Me.CtrlTxtBox.ForeColor = Color.FromArgb(37, 37, 37)
            'Me.CtrlTxtBox.Font = New Font("Neo Sans", 8, FontStyle.Regular)

            C1Sizer1.SplitterWidth = 2
            Me.C1Sizer1.Grid.Rows(0).Size = 23
            Me.C1Sizer1.Grid.Columns(0).Size = 23
            Me.C1Sizer1.Grid.Columns(0).IsFixedSize = True
            Me.C1Sizer1.Grid.Columns(1).Size = 196
            Me.C1Sizer1.Grid.Columns(2).Size = 153
            Me.C1Sizer1.Grid.Columns(3).Size = 186
            Me.C1Sizer1.Grid.Columns(3).IsFixedSize = False
            Me.C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            Me.CtrlCmdSearch.Location = New System.Drawing.Point(0, 0)
            Me.CtrlCmdSearch.Size = New System.Drawing.Size(48, 24)
            Me.CtrlCmdSearch.Dock = DockStyle.Left
            CtrlCmdSearch.FlatStyle = FlatStyle.Flat
            CtrlCmdSearch.FlatAppearance.BorderSize = 0
            ' Me.CtrlTxtBox.Visible = False
            Me.CtrlTxtBox.Location = New System.Drawing.Point(48, 0)
            Me.CtrlTxtBox.Size = New System.Drawing.Size(196, 20)
            Me.CtrlTxtBox.Dock = DockStyle.None
            Me.CtrlTxtBox.Location = New System.Drawing.Point(48, 0)
            Me.CtrlTxtBox.MinimumSize = New Size(0, 24)
            Me.CtrlTxtBox.Size = New System.Drawing.Size(196, 24)
            Me.CtrlTxtBox.Dock = DockStyle.None
            CtrlTxtBox.BorderStyle = Windows.Forms.BorderStyle.None
            ' CtrlTxtBox.Visible = False
            Me.AndroidSearchTextBox.Location = New System.Drawing.Point(48, 0)
            Me.AndroidSearchTextBox.MinimumSize = New Size(0, 24)
            Me.AndroidSearchTextBox.Size = New System.Drawing.Size(196, 24)
            Me.AndroidSearchTextBox.Dock = DockStyle.None
            AndroidSearchTextBox.BorderStyle = Windows.Forms.BorderStyle.None
            Me.CtrllabelSalesPerson.Location = New System.Drawing.Point(256, 0)
            Me.CtrllabelSalesPerson.Size = New System.Drawing.Size(153, 24)
            Me.CtrllabelSalesPerson.Dock = DockStyle.None
            Me.CtrlSalesPersons.Location = New System.Drawing.Point(409, 0)
            Me.CtrlSalesPersons.MinimumSize = New Size(0, 24)
            Me.CtrlSalesPersons.Size = New System.Drawing.Size(186, 24)
            Me.CtrlSalesPersons.Dock = DockStyle.Right
            CtrlSalesPersons.Font = New Font("Neo Sans", 9, FontStyle.Regular)
            Me.CtrllabelSalesPerson.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllabelSalesPerson.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrllabelSalesPerson.Font = New Font("Neo Sans", 8, FontStyle.Bold)
            Me.CtrllabelSalesPerson.BorderStyle = Windows.Forms.BorderStyle.None
            CtrlSalesPersons.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver
            CtrllabelSalesPerson.MaximumSize = New Size(0, 24)
            CtrlSalesPersons.MaximumSize = New Size(0, 22)

            Me.CtrlSalesPersons.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlSalesPersons.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Black
            Me.CtrlSalesPersons.EditorBackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Font = New Font("Neo Sans", 8, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(0).BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(1).Font = New Font("Neo Sans", 9, FontStyle.Regular)

            Me.CtrlTxtBox.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlTxtBox.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlTxtBox.Font = New Font("Neo Sans", 8, FontStyle.Regular)


            Me.CtrlCmdSearch.Text = ""
            Me.CtrlCmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
            Me.CtrlCmdSearch.Image = Nothing
            Me.CtrlCmdSearch.BackgroundImage = Global.Spectrum.My.Resources.Resources.SearchItems
            Me.CtrlCmdSearch.FlatStyle = FlatStyle.Flat
            Me.CtrlCmdSearch.BackgroundImageLayout = ImageLayout.Stretch
            CtrlCmdSearch.MaximumSize = New Size(52, 23)
        ElseIf AlignChange = "Sales Order Old" Then
            C1Sizer1.SplitterWidth = 2
            ' Me.C1Sizer1.Grid.Rows(0).Size = 23
            ' Me.CtrlTxtBox.Visible = False
            ' AndroidSearchTextBox.MinimumSize = New Size(0, 22)
            ' Me.C1Sizer1.Grid.Columns(0).Size = 135
            ' Me.C1Sizer1.Grid.Columns(0).IsFixedSize = True
            ' Me.C1Sizer1.Grid.Columns(1).Size = 158
            ' Me.C1Sizer1.Grid.Columns(2).IsFixedSize = True
            ' Me.C1Sizer1.Grid.Columns(2).Size = 250
            Me.C1Sizer1.Grid.Columns(3).Size = 26
            Me.CtrllabelSalesPerson.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrllabelSalesPerson.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrllabelSalesPerson.BorderStyle = Windows.Forms.BorderStyle.None
            CtrllabelSalesPerson.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrllabelSalesPerson.Font = New Font("Neo Sans", 8, FontStyle.Bold)

            Me.CtrlSalesPersons.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlSalesPersons.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Black
            Me.CtrlSalesPersons.EditorBackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Font = New Font("Neo Sans", 8, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(0).BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlSalesPersons.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Regular)
            Me.CtrlSalesPersons.Styles(1).Font = New Font("Neo Sans", 9, FontStyle.Regular)



            Me.CtrlTxtBox.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlTxtBox.ForeColor = Color.FromArgb(37, 37, 37)
            Me.CtrlTxtBox.Font = New Font("Neo Sans", 8, FontStyle.Regular)


            Me.CtrlCmdSearch.Text = ""
            Me.CtrlCmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
            Me.CtrlCmdSearch.Image = Nothing
            Me.CtrlCmdSearch.BackgroundImage = Global.Spectrum.My.Resources.Resources.SearchItems
            Me.CtrlCmdSearch.FlatStyle = FlatStyle.Flat
            Me.CtrlCmdSearch.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Function

End Class
