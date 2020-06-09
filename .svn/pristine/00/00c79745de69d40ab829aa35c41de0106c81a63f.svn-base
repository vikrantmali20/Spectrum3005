Imports System.Drawing.Graphics
Imports System.IO
Imports C1.Win.C1Command
Imports System.Linq
Imports System.IO.Directory
Imports SpectrumBL

Public Class CtrlTouchMenu
    Private _NumberOfButtons As Decimal = 4
    Dim flpLabelWidth = ((My.Computer.Screen.WorkingArea.Width * 675) / 1366)
    Dim flpLabelHeight = ((My.Computer.Screen.WorkingArea.Height * 777) / 1366)
    Dim flpLabelHeightArticles = ((My.Computer.Screen.WorkingArea.Height * 575) / 1366)
    Dim ImageArticleName As String

    Private Property NumberOfButtons() As Decimal
        Get
            Return _NumberOfButtons

        End Get
        Set(value As Decimal)
            _NumberOfButtons = value
        End Set
    End Property

    Private _parent As frmTouchCashMemo
    Public Property Parent As frmTouchCashMemo
        Get
            Return _parent
        End Get
        Set(ByVal value As frmTouchCashMemo)
            _parent = value
        End Set
    End Property

    Private _ButtonTotalColumn As Integer = 7
    Private Property ButtonTotalColumn As Integer
        Get
            Return _ButtonTotalColumn
        End Get
        Set(value As Integer)
            _ButtonTotalColumn = value
        End Set
    End Property
    Private _Rowcount As Integer = 1
    Public Property Rowcount As Integer
        Get
            Return _Rowcount
        End Get
        Set(value As Integer)
            _Rowcount = value
        End Set
    End Property
    Private ReadOnly Property ButtonSize As System.Drawing.Size

        Get
            Return New Size(75, 80)
        End Get

    End Property

    Private ReadOnly Property ButtonTabSize As System.Drawing.Size

        Get
            Return New Size(100, 40)
        End Get

    End Property
    Private _ImageInfo As DataTable
    Public Property ImageInfo As DataTable
        Get

            Return _ImageInfo
        End Get
        Set(value As DataTable)
            _ImageInfo = value
        End Set
    End Property

    Private _activeTabId As String
    Public Property ActiveTabId As String
        Get

            Return _activeTabId
        End Get
        Set(ByVal value As String)
            _activeTabId = value
        End Set
    End Property
    Private _isFirstArticle As Boolean
    Public Property isFirstArticle As Boolean
        Get

            Return _isFirstArticle
        End Get
        Set(ByVal value As Boolean)
            _isFirstArticle = value
        End Set
    End Property
    Private _buttonGroup As DataTable
    Public Property ButtonGroup As DataTable
        Get

            Return _buttonGroup
        End Get
        Set(ByVal value As DataTable)
            _buttonGroup = value
        End Set
    End Property


    Private _CtrlSalesPersons As CtrlSalesPerson
    Private Property MSalesPerson As CtrlSalesPerson
        Get

            Return _CtrlSalesPersons
        End Get
        Set(value As CtrlSalesPerson)
            _CtrlSalesPersons = value
        End Set
    End Property
    Dim tipimage As ToolTip = New ToolTip()
    Dim tempToolText As String = ""
    Dim DivCnt As Integer = 1
    Dim OldGroupID As String
    Public Sub AddArticleButton(ByVal articleCode As String, ByVal articleName As String, ByVal buttonId As String, ByVal groupId As String)
        Dim lb As Label
        Dim pn As TableLayoutPanel
        Dim bt As CtrlBtn
        bt = New Spectrum.CtrlBtn()
        tipimage.OwnerDraw = True

        If OldGroupID <> groupId Then
            DivCnt = 1
        End If
        OldGroupID = groupId
        AddHandler tipimage.Draw, AddressOf toolTip1_Draw
        AddHandler tipimage.Popup, AddressOf toolTip1_Popup
        bt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
        bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        bt.Location = New System.Drawing.Point(3, 3)
        bt.Name = "CtrlBtnGoods"
        bt.SetArticleCode = articleCode
        bt.Anchor = AnchorStyles.Top
        bt.Size = ButtonSize
        bt.TabIndex = 1
        bt.Tag = buttonId
        bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        bt.UseVisualStyleBackColor = True
        bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.BackColor = Color.FromArgb(255, 255, 255)
        Else
            bt.BackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        End If
        'bt.BackColor = Color.Maroon
        'Dim tipimage As ToolTip = New ToolTip()
        ' Logic for wrap text vartically if it is not proper visible in screen
        If DivCnt Mod 6 = 0 Or DivCnt Mod 7 = 0 Or DivCnt Mod 8 = 0 Then
            If (articleName.Length > 5) Then
                Dim articlearry = articleName.Split(" ")
                Dim newArticle = ""
                For s As Integer = 0 To articlearry.Length - 1
                    If articlearry(s) <> " " Then
                        If s <> articlearry.Length - 1 Then
                            newArticle &= articlearry(s) & Environment.NewLine
                        Else
                            newArticle &= articlearry(s)
                        End If
                    End If
                Next
                tipimage.SetToolTip(bt, newArticle)
            Else
                tipimage.SetToolTip(bt, articleName)
            End If
        Else
            tipimage.SetToolTip(bt, articleName)
        End If
        DivCnt = DivCnt + 1

        'tipimage.Show("My tooltip", articleName, Cursor.Position.X, Cursor.Position.Y)

        ' tipimage.Show(tempToolText, articleName, 0, 0)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            bt.ContextMenuStrip = ArticleContextMenuStrip
        End If
        bt.Margin = New Padding(0)
        bt.Padding = New Padding(0)
        AddHandler bt.Click, AddressOf Parent.ModMenuCtrlBtn1_Click
        ShowArticleImageMOD(articleCode, bt)

        lb = New Label()
        lb.TextAlign = ContentAlignment.TopLeft
        lb.MaximumSize = New Size(ButtonSize.Width, 60)
        lb.Size = New Size(ButtonSize.Width, 60)
        lb.Margin = New Padding(3, 0, 0, 0)
        'lb.AutoSize = True
        lb.Text = UCase(articleName)
        Dim tip As ToolTip = New ToolTip()
        tip.SetToolTip(lb, articleName)

        lb.Name = "CtrlBtnGoods"
        lb.Anchor = AnchorStyles.Left
        lb.ForeColor = Color.DarkBlue
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            'lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'new
            '  tabControlMain.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            'new
            '  lb.ForeColor = ColorTranslator.FromOle(RGB(37, 37, 37))
            lb.ForeColor = ColorTranslator.FromOle(RGB(37, 37, 37))
            lb.BackColor = Color.FromArgb(255, 255, 255)
            lb.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Else
            lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            lb.ForeColor = Color.Black
        End If
        pn = New TableLayoutPanel()


        'Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Blue)
        'Dim formGraphics As System.Drawing.Graphics
        'formGraphics = Me.CreateGraphics()
        'formGraphics.FillRectangle(myBrush, New Rectangle(0, 0, 200, 300))

        pn.Margin = New Padding(1)
        pn.Padding = New Padding(1)
        pn.Size = New System.Drawing.Size(ButtonSize.Width + 6, ButtonSize.Height + 60)
        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        pn.RowCount = 2
        pn.ColumnCount = 1
        pn.Controls.Add(bt, 0, 0)
        pn.Controls.Add(lb, 0, 1)
        pn.AutoSize = False
        Dim flowPanel = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = groupId Select tab.Controls(0)).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            pn.BackColor = Color.FromArgb(255, 255, 255)
        Else
            pn.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        End If
        'Dim flowPanel As FlowLayoutPanel
        'flowPanel = DirectCast(tabControlMain.SelectedTab.Controls(0), FlowLayoutPanel)
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Add(pn)
        End If
        'iAddedTotalNoButtons = iAddedTotalNoButtons + 1
    End Sub


    Public Sub AddArticleButtonTextMiddle(ByVal articleCode As String, ByVal articleName As String, ByVal buttonId As String, ByVal groupId As String)
        Dim lb As Label
        Dim pn As TableLayoutPanel
        Dim bt As CtrlBtn
        bt = New Spectrum.CtrlBtn()
        tipimage.OwnerDraw = True

        If OldGroupID <> groupId Then
            DivCnt = 1
        End If
        OldGroupID = groupId
        AddHandler tipimage.Draw, AddressOf toolTip1_Draw
        AddHandler tipimage.Popup, AddressOf toolTip1_Popup
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Else
            bt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
        'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
        bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        bt.Location = New System.Drawing.Point(3, 3)
        bt.Name = "CtrlBtnGoods"
        bt.SetArticleCode = articleCode
        bt.Anchor = AnchorStyles.Top
        bt.Size = ButtonSize
        bt.TabIndex = 1
        bt.Tag = buttonId
        bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        bt.UseVisualStyleBackColor = True
        bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        bt.Text = articleName
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.BackColor = Color.FromArgb(212, 212, 212)
        Else
            bt.BackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        End If
        'bt.BackColor = Color.Maroon
        'Dim tipimage As ToolTip = New ToolTip()
        ' Logic for wrap text vartically if it is not proper visible in screen
        If DivCnt Mod 6 = 0 Or DivCnt Mod 7 = 0 Or DivCnt Mod 8 = 0 Then
            If (articleName.Length > 5) Then
                Dim articlearry = articleName.Split(" ")
                Dim newArticle = ""
                For s As Integer = 0 To articlearry.Length - 1
                    If articlearry(s) <> " " Then
                        If s <> articlearry.Length - 1 Then
                            newArticle &= articlearry(s) & Environment.NewLine
                        Else
                            newArticle &= articlearry(s)
                        End If
                    End If
                Next
                tipimage.SetToolTip(bt, newArticle)
            Else
                tipimage.SetToolTip(bt, articleName)
            End If
        Else
            tipimage.SetToolTip(bt, articleName)
        End If
        DivCnt = DivCnt + 1

        'tipimage.Show("My tooltip", articleName, Cursor.Position.X, Cursor.Position.Y)

        ' tipimage.Show(tempToolText, articleName, 0, 0)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            bt.ContextMenuStrip = ArticleContextMenuStrip
        End If
        bt.Margin = New Padding(0)
        bt.Padding = New Padding(0)

        AddHandler bt.Click, AddressOf Parent.ModMenuCtrlBtn1_Click
        ShowArticleImageMOD(articleCode, bt)

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            'lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'new
            '  tabControlMain.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            'new
            '  lb.ForeColor = ColorTranslator.FromOle(RGB(37, 37, 37))
            tabControlMain.BackColor = Color.FromArgb(212, 212, 212)
            tabControlMain.ForeColor = Color.FromArgb(37, 37, 37)
        Else
            'lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            'lb.ForeColor = Color.Black
        End If
        pn = New TableLayoutPanel()


        'Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Blue)
        'Dim formGraphics As System.Drawing.Graphics
        'formGraphics = Me.CreateGraphics()
        'formGraphics.FillRectangle(myBrush, New Rectangle(0, 0, 200, 300))

        pn.Margin = New Padding(1)
        pn.Padding = New Padding(1)
        pn.Size = New System.Drawing.Size(ButtonSize.Width + 6, ButtonSize.Height + 1)
        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        pn.RowCount = 1
        pn.ColumnCount = 1
        pn.Controls.Add(bt, 0, 0)
        pn.AutoSize = False
        Dim flowPanel = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = groupId Select tab.Controls(0)).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            pn.BackColor = Color.FromArgb(255, 255, 255)
        Else
            pn.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        End If
        'Dim flowPanel As FlowLayoutPanel
        'flowPanel = DirectCast(tabControlMain.SelectedTab.Controls(0), FlowLayoutPanel)
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Add(pn)
        End If

        'iAddedTotalNoButtons = iAddedTotalNoButtons + 1
    End Sub
    Public Sub AddSubTabsButton(ByVal articleCode As String, ByVal articleName As String, ByVal buttonId As String, ByVal groupId As String)
        Dim lb As Label
        Dim pn As TableLayoutPanel
        Dim bt As CtrlBtn
        bt = New Spectrum.CtrlBtn()
        tipimage.OwnerDraw = True

        If OldGroupID <> groupId Then
            DivCnt = 1
        End If
        OldGroupID = groupId
        AddHandler tipimage.Draw, AddressOf toolTip1_Draw
        AddHandler tipimage.Popup, AddressOf toolTip1_Popup
        bt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
        bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        bt.Location = New System.Drawing.Point(3, 3)
        bt.Name = "CtrlBtnGoods"
        bt.SetArticleCode = articleCode
        bt.Text = articleName
        bt.Anchor = AnchorStyles.Top
        bt.Size = ButtonTabSize
        bt.TabIndex = 1
        bt.Tag = buttonId
        bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        bt.UseVisualStyleBackColor = True
        bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.BackColor = Color.FromArgb(49, 49, 49)
            bt.ForeColor = Color.White
        Else
            bt.BackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        End If
        'bt.BackColor = Color.Maroon
        'Dim tipimage As ToolTip = New ToolTip()
        ' Logic for wrap text vartically if it is not proper visible in screen

        tipimage.SetToolTip(bt, articleName)



        'tipimage.Show("My tooltip", articleName, Cursor.Position.X, Cursor.Position.Y)

        ' tipimage.Show(tempToolText, articleName, 0, 0)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            bt.ContextMenuStrip = ArticleContextMenuStrip
        End If
        bt.Margin = New Padding(0)
        bt.Padding = New Padding(0)
        AddHandler bt.Click, AddressOf Parent.ModMenuSubCtrlBtn1_Click
        AddHandler bt.Leave, AddressOf Parent.ModMenuCtrlBtn1_Leave
        'ShowArticleImageMOD(articleCode, bt)

        'lb = New Label()
        'lb.TextAlign = ContentAlignment.TopLeft
        'lb.MaximumSize = New Size(ButtonSize.Width, 60)
        'lb.Size = New Size(ButtonSize.Width, 60)
        'lb.Margin = New Padding(3, 0, 0, 0)
        ''lb.AutoSize = True
        'lb.Text = UCase(articleName)
        'Dim tip As ToolTip = New ToolTip()
        'tip.SetToolTip(lb, articleName)

        'lb.Name = "CtrlBtnGoods"
        'lb.Anchor = AnchorStyles.Left
        'lb.ForeColor = Color.DarkBlue
        'lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'lb.ForeColor = Color.Black
        pn = New TableLayoutPanel()


        'Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Blue)
        'Dim formGraphics As System.Drawing.Graphics
        'formGraphics = Me.CreateGraphics()
        'formGraphics.FillRectangle(myBrush, New Rectangle(0, 0, 200, 300))

        pn.Margin = New Padding(1)
        pn.Padding = New Padding(1)
        pn.Size = New System.Drawing.Size(ButtonTabSize.Width, ButtonTabSize.Height)
        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        pn.RowCount = 2
        pn.ColumnCount = 1
        pn.Controls.Add(bt, 0, 0)
        ' pn.Controls.Add(lb, 0, 1)
        pn.AutoSize = False
        Dim flowPanel As FlowLayoutPanel = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = groupId Select tab.Controls(0)).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            pn.BackColor = Color.FromArgb(49, 49, 49)
            flowPanel.BackColor = Color.FromArgb(49, 49, 49)
            flowPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Else
            pn.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        End If

        'Dim flowPanel As FlowLayoutPanel
        'flowPanel = DirectCast(tabControlMain.SelectedTab.Controls(0), FlowLayoutPanel)
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Add(pn)
            NewFlowPanelLocation = pn.Location
        End If
        'iAddedTotalNoButtons = iAddedTotalNoButtons + 1
    End Sub

    Public Sub AddSubArticles(ByVal articleCode As String, ByVal articleName As String, ByVal buttonId As String, ByVal groupId As String, ByRef flowPanel As FlowLayoutPanel, Optional ByVal IsKit As Boolean = False, Optional ByVal KitArticleDesc As String = "")
        Dim lb As Label
        Dim pn As TableLayoutPanel
        Dim bt As CtrlBtn
        bt = New Spectrum.CtrlBtn()
        tipimage.OwnerDraw = True

        If OldGroupID <> groupId Then
            DivCnt = 1
        End If
        OldGroupID = groupId
        AddHandler tipimage.Draw, AddressOf toolTip1_Draw
        AddHandler tipimage.Popup, AddressOf toolTip1_Popup
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            tipimage.BackColor = Color.White
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Else
            bt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
        'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
        bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        bt.Location = New System.Drawing.Point(3, 3)
        bt.Name = "CtrlBtnGoods"
        bt.SetArticleCode = articleCode
        bt.Anchor = AnchorStyles.Top
        bt.Size = ButtonSize
        bt.TabIndex = 1
        bt.Tag = buttonId
        bt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        bt.UseVisualStyleBackColor = True
        bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom

        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            'bt.BackColor = Color.FromArgb(212, 212, 212)
            bt.BackColor = Color.White
        Else
            bt.BackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        End If

        'bt.BackColor = Color.Maroon
        'Dim tipimage As ToolTip = New ToolTip()
        ' Logic for wrap text vartically if it is not proper visible in screen

        ' tipimage.SetToolTip(bt, articleName)
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            If IsKit = True Then
                If Not KitArticleDesc Is Nothing Then
                    tipimage.SetToolTip(bt, KitArticleDesc)
                Else
                    tipimage.SetToolTip(bt, articleName)
                End If
            Else
                tipimage.SetToolTip(bt, articleName)
            End If
        Else
            tipimage.SetToolTip(bt, articleName)
        End If


        'tipimage.Show("My tooltip", articleName, Cursor.Position.X, Cursor.Position.Y)

        ' tipimage.Show(tempToolText, articleName, 0, 0)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            bt.ContextMenuStrip = ArticleContextMenuStrip
        End If
        bt.Margin = New Padding(0)
        bt.Padding = New Padding(0)
        AddHandler bt.Click, AddressOf Parent.ModMenuCtrlBtn1_Click
        ShowArticleImageMOD(articleCode, bt)

        lb = New Label()
        lb.TextAlign = ContentAlignment.TopLeft
        lb.MaximumSize = New Size(ButtonSize.Width, 60)
        lb.Size = New Size(ButtonSize.Width, 60)
        lb.Margin = New Padding(3, 0, 0, 0)
        'lb.AutoSize = True
        Dim _ArticleName = StrConv(articleName, VbStrConv.ProperCase)
        lb.Text = _ArticleName
        Dim tip As ToolTip = New ToolTip()
        tip.SetToolTip(lb, _ArticleName)

        lb.Name = "CtrlBtnGoods"
        lb.Anchor = AnchorStyles.Left
        'lb.ForeColor = Color.DarkBlue
        lb.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            tabControlMain.BackColor = Color.FromArgb(212, 212, 212)
            tabControlMain.ForeColor = Color.FromArgb(37, 37, 37)
            '  tabControlMain.Font = New Font("Neo Sans", 11, FontStyle.Bold)
            'new
            lb.ForeColor = ColorTranslator.FromOle(RGB(37, 37, 37))
            lb.BackColor = Color.Azure
            lb.Font = New Font("Arial", 10, FontStyle.Regular)
        Else
            lb.ForeColor = Color.Black
        End If
        pn = New TableLayoutPanel()
        pn.Margin = New Padding(1)
        pn.Padding = New Padding(1)
        pn.Size = New System.Drawing.Size(ButtonSize.Width + 6, ButtonSize.Height + 60)
        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        pn.RowCount = 2
        pn.ColumnCount = 1
        pn.Controls.Add(bt, 0, 0)
        pn.Controls.Add(lb, 0, 1)
        pn.AutoSize = False

        '---- Code Commented By Mahesh Getting FlowPanel As I am Passing as parameter
        'Dim flowPanel As FlowLayoutPanel = (From tab As FlowLayoutPanel In tabPage.Controls Where flp.Tag = groupId Select tab.controls(1)).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            pn.BackColor = Color.Azure
            flowPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
        Else
            pn.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        End If
        'flowPanel.Refresh()
        'Dim flowPanel As FlowLayoutPanel
        'flowPanel = DirectCast(tabControlMain.SelectedTab.Controls(0), FlowLayoutPanel)
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Add(pn)
        End If
        'iAddedTotalNoButtons = iAddedTotalNoButtons + 1
    End Sub

    Public Sub AddSubArticlesTextMiddle(ByVal articleCode As String, ByVal articleName As String, ByVal buttonId As String, ByVal groupId As String, ByRef flowPanel As FlowLayoutPanel, Optional ByVal IsKit As Boolean = False, Optional ByVal KitArticleDesc As String = "")
        Dim lb As Label
        Dim pn As TableLayoutPanel
        Dim bt As CtrlBtn
        bt = New Spectrum.CtrlBtn()
        tipimage.OwnerDraw = True

        If OldGroupID <> groupId Then
            DivCnt = 1
        End If
        OldGroupID = groupId
        AddHandler tipimage.Draw, AddressOf toolTip1_Draw
        AddHandler tipimage.Popup, AddressOf toolTip1_Popup
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            tipimage.BackColor = Color.White
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.Font = New Font("Arial", 9, FontStyle.Regular)
            bt.BackColor = Color.White
        Else
            bt.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
        'Me.CtrlBtnGoods.Image = Global.Spectrum.My.Resources.Resources._new
        bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        bt.Location = New System.Drawing.Point(3, 3)
        bt.Name = "CtrlBtnGoods"
        bt.SetArticleCode = articleCode
        bt.Anchor = AnchorStyles.Top
        bt.Size = ButtonSize
        bt.TabIndex = 1
        bt.Tag = buttonId
        bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        bt.UseVisualStyleBackColor = True
        bt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Custom
        '  Code commented by vipul as per requirement do n't show article name on image
        '        bt.Text = articleName
        'added by khusrao adil on 29-08-2017 
        'sentence case article
        Dim _ArticleName = StrConv(articleName, VbStrConv.ProperCase)
        bt.Text = _ArticleName
        If clsDefaultConfiguration.DineInButtonWithText = True Then
            'ImageArticleName = ""
            ImageArticleName = _ArticleName
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            bt.BackColor = Color.White
        Else
            bt.BackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        End If

        'bt.BackColor = Color.Maroon
        'Dim tipimage As ToolTip = New ToolTip()
        ' Logic for wrap text vartically if it is not proper visible in screen

        'tipimage.SetToolTip(bt, articleName)
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            If IsKit = True Then
                If Not KitArticleDesc Is Nothing Then
                    tipimage.SetToolTip(bt, KitArticleDesc)
                Else
                    tipimage.SetToolTip(bt, articleName)
                End If
            Else
                tipimage.SetToolTip(bt, articleName)
            End If
        Else
            tipimage.SetToolTip(bt, articleName)
        End If


        'tipimage.Show("My tooltip", articleName, Cursor.Position.X, Cursor.Position.Y)

        ' tipimage.Show(tempToolText, articleName, 0, 0)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            bt.ContextMenuStrip = ArticleContextMenuStrip
        End If
        bt.Margin = New Padding(0)
        bt.Padding = New Padding(0)
        AddHandler bt.Click, AddressOf Parent.ModMenuCtrlBtn1_Click
        ShowArticleImageMOD(articleCode, bt)

        pn = New TableLayoutPanel()
        pn.Margin = New Padding(1)
        pn.Padding = New Padding(1)
        pn.Size = New System.Drawing.Size(ButtonSize.Width + 6, ButtonSize.Height + 1)
        pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        pn.RowCount = 1
        pn.ColumnCount = 1
        pn.Controls.Add(bt, 0, 0)
        pn.AutoSize = False
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            tabControlMain.BackColor = Color.FromArgb(212, 212, 212)
            tabControlMain.ForeColor = Color.FromArgb(37, 37, 37)
            '  tabControlMain.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        End If
        '---- Code Commented By Mahesh Getting FlowPanel As I am Passing as parameter
        'Dim flowPanel As FlowLayoutPanel = (From tab As FlowLayoutPanel In tabPage.Controls Where flp.Tag = groupId Select tab.controls(1)).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            pn.BackColor = Color.Azure
        Else
            pn.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        End If
        'flowPanel.Refresh()
        'Dim flowPanel As FlowLayoutPanel
        'flowPanel = DirectCast(tabControlMain.SelectedTab.Controls(0), FlowLayoutPanel)
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Add(pn)
        End If
        'iAddedTotalNoButtons = iAddedTotalNoButtons + 1
    End Sub
    Dim proposedSize As Size = New Size(500, 500)
    Private Sub toolTip1_Draw(ByVal sender As Object, ByVal e As DrawToolTipEventArgs)
        ' Dim f As Font = New Font("calibri", 20.0F)
        Dim f As Font
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            f = New Font("calibri", 13.0F, FontStyle.Bold)
        Else
            f = New Font("calibri", 20.0F)
        End If
        e.DrawBackground()
        e.DrawBorder()

        tempToolText = e.ToolTipText
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            If e.ToolTipText.Length > 40 Then
                proposedSize = New Size(1000, 1000)
            Else
                proposedSize = New Size(100, 100)
            End If
        Else
            proposedSize = New Size(100, 100)
        End If
        'Dim textBounds As Rectangle = New Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width + 20, e.Bounds.Height)
        Dim textBounds As Rectangle = New Rectangle(e.Bounds.Location, e.Bounds.Size)
        e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, textBounds)
        ' e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, New PointF(2, 2))
        'e.Graphics.DrawString("gdads ada", f, Brushes.Black, New PointF(4, 4))
    End Sub
    Private Sub toolTip1_Popup(ByVal sender As Object, ByVal e As PopupEventArgs)
        'e.ToolTipSize = New Size(200, 100)
        ' on popip set the size of tool tip
        'e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 20.0F)) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))
        'e.
        ' Dim proposedSize As Size = New Size(100, 100)
        tipimage.GetToolTip(e.AssociatedControl)
        Dim flags As TextFormatFlags = TextFormatFlags.WordBreak
        ' e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 20.0F), proposedSize, flags) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))
        If clsDefaultConfiguration.OnTooltipDisplayTheKitArticleIngredients Then
            e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 13.0F, FontStyle.Bold), proposedSize, flags) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))
        Else
            e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 20.0F), proposedSize, flags) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))
        End If
    End Sub

    Private Sub AddCategoryMenuItem_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles ArticleCategoryMenuStrip.ItemClicked

        Dim objCM As New clsCashMemo
        If objCM.CheckIFGlNoRangeIsAvailable(clsAdmin.SiteCode) = False Then
            ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
            Exit Sub
        End If
        If e.ClickedItem.Text = "Add" Then
            objCM.SaveAndEditButtonGroup(0, "New Tab", e.ClickedItem.Text, clsAdmin.UserCode, clsAdmin.SiteCode)
            CategoryField.Text = "New Tab"
            isNewTab = True
            ChangeTabName()
        ElseIf e.ClickedItem.Text = "Remove" Then
            objCM.SaveAndEditButtonGroup(tabControlMain.SelectedTab.Tag, String.Empty, e.ClickedItem.Text, clsAdmin.UserCode, clsAdmin.SiteCode)
            CategoryField.Visible = False
        End If
        'ActiveTabId = tabControlMain.Controls.Count - 1
        RefreshTabs(objCM)
        If tabControlMain.Controls.Count > 0 Then
            tabControlMain.Controls(tabControlMain.Controls.Count - 1).Show()
        End If
    End Sub

    Private Sub RefreshTabs(ByRef objCM As clsCashMemo)
        Dim btnGroup As DataTable
        Dim btnArticle As New DataTable
        btnGroup = objCM.GetButtonGroup(clsAdmin.SiteCode)
        btnArticle = objCM.GetButtonArticle(clsAdmin.SiteCode)
        ButtonGroup = btnGroup
        ImageInfo = btnArticle
        LoadArticle()
    End Sub

    Private Function GetTabPage() As C1DockingTabPage
        Dim articleCategoryPage As New C1.Win.C1Command.C1DockingTabPage
        articleCategoryPage.Font = New System.Drawing.Font(New FontFamily("Verdana"), 16, GraphicsUnit.Point)
        articleCategoryPage.Location = New System.Drawing.Point(1, 24)
        articleCategoryPage.Name = "ArticleCategoryPage"
        articleCategoryPage.Size = New System.Drawing.Size(669, 500)
        articleCategoryPage.Margin = New Padding(0)
        articleCategoryPage.Padding = New Padding(0)
        articleCategoryPage.TabBackColor = ColorTranslator.FromOle(RGB(255, 255, 255))
        articleCategoryPage.TabBackColorSelected = ColorTranslator.FromOle(RGB(255, 192, 128))
        'articleCategoryPage.TabForeColor = Color.BlueViolet
        articleCategoryPage.Width = 10
        'articleCategoryPage.TabBackColor = Color.White
        'articleCategoryPage.MaximumSize = New System.Drawing.Size(800, 1341)
        Return articleCategoryPage
    End Function
    Private Function GetFlowPanel() As FlowLayoutPanel
        Dim flp As New FlowLayoutPanel
        flp.Dock = System.Windows.Forms.DockStyle.Fill
        flp.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        flp.Location = New System.Drawing.Point(0, 0)
        flp.Name = "flpMenuButton"
        flp.Size = New System.Drawing.Size(flpLabelWidth, 500)
        'flp.MaximumSize = New System.Drawing.Size(800, 1341)
        'flp.Padding = New System.Windows.Forms.Padding(0, 35, 0, 0)
        'flp.VerticalScroll.Enabled = True
        'flp.VerticalScroll.Visible = True
        flp.BackColor = New System.Windows.Forms.ToolTip().BackColor

        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            FlowPanelMenuStrip.BackColor = New System.Windows.Forms.ToolTip().BackColor
            flp.ContextMenuStrip = FlowPanelMenuStrip
        End If
        flp.AutoScroll = True
        Return flp
    End Function
    Private Function GetMultiFlowPanel(Optional PanelName As String = "") As FlowLayoutPanel
        Dim flp As New FlowLayoutPanel
        flp.Dock = System.Windows.Forms.DockStyle.None
        flp.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        flp.Location = New System.Drawing.Point(0, 0)
        If Not String.IsNullOrEmpty(PanelName) Then
            flp.Name = PanelName
        Else
            flp.Name = "flpMenuButton"
        End If


        flp.Size = New System.Drawing.Size(flpLabelWidth, 478)

        flp.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255)) ' New System.Windows.Forms.ToolTip().BackColor

        flp.AutoSize = True
        flp.MaximumSize = New System.Drawing.Size(flpLabelWidth, 478)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            FlowPanelMenuStrip.BackColor = New System.Windows.Forms.ToolTip().BackColor
            flp.ContextMenuStrip = FlowPanelMenuStrip
        End If

        flp.AutoScroll = True
        Return flp
    End Function
    Private Function GetFlowPanelArticleswithSize(ByVal x As Integer, ByVal y As Integer) As FlowLayoutPanel
        Dim flp As New FlowLayoutPanel
        flp.Dock = System.Windows.Forms.DockStyle.None
        flp.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        flp.Location = New System.Drawing.Point(0, 60)
        flp.Name = "flpMenuButtonArt"

        flp.Size = New System.Drawing.Size(flpLabelWidth, 478)

        flp.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255)) ' New System.Windows.Forms.ToolTip().BackColor

        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            FlowPanelMenuStrip.BackColor = New System.Windows.Forms.ToolTip().BackColor
            flp.ContextMenuStrip = FlowPanelMenuStrip
        End If


        flp.AutoScroll = True
        'flp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        'flp.WrapContents = False

        flp.MaximumSize = New System.Drawing.Size(flpLabelWidth, 478)
        Return flp
    End Function
    Private Function GetFlowPanelArticles(Optional PanelName As String = "") As FlowLayoutPanel
        Dim flp As New FlowLayoutPanel
        flp.Dock = System.Windows.Forms.DockStyle.None
        flp.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        flp.Location = New System.Drawing.Point(0, NewFlowPanelLocation.Y + 45)
        If Not String.IsNullOrEmpty(PanelName) Then
            flp.Name = PanelName
        Else
            flp.Name = "flpMenuButtonArt"
        End If

        flp.Size = New System.Drawing.Size(flpLabelWidth, flpLabelHeight)

        flp.BackColor = ColorTranslator.FromOle(RGB(255, 255, 255)) ' New System.Windows.Forms.ToolTip().BackColor

        If CheckAuthorisation(clsAdmin.UserCode, "ADDBTN_CASHMEMO") Then
            FlowPanelMenuStrip.BackColor = New System.Windows.Forms.ToolTip().BackColor
            flp.ContextMenuStrip = FlowPanelMenuStrip
        End If


        flp.AutoScroll = True
        'flp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        'flp.WrapContents = False

        flp.MaximumSize = New System.Drawing.Size(flpLabelWidth, flpLabelHeight)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            flp.BackColor = Color.FromArgb(255, 255, 255)
        End If
        Return flp
    End Function
    Dim NewFlowPanelLocation As Point
    'Dim CtrlBtnGoods As Spectrum.CtrlBtn
    Private Sub AddMultiTabs()
        tabControlMain.Controls.Clear()
        tabControlMain.BackColor = ColorTranslator.FromOle(RGB(27, 63, 109))
        'Dim dvButtonGroup As New DataView(ButtonGroup, String.Empty, "GroupName ASC", DataViewRowState.CurrentRows)

        For Each btnGroup As DataRow In ButtonGroup.Rows
            Dim tabPage As C1DockingTabPage = GetTabPage()
            tabPage.Text = btnGroup("GroupName")
            tabPage.Tag = btnGroup("GroupID")
            tabPage.Name = btnGroup("GroupID")

            'AddHandler tabPage.MouseClick, AddressOf Parent.ModMenuTabPage1_MouseClick
            Dim flp As FlowLayoutPanel = GetMultiFlowPanel(btnGroup("GroupID"))


            'Dim flp1 As FlowLayoutPanel = GetFlowPanelArticles()


            ' AddHandler tabPage.MouseClick, AddressOf tabControlMain_Click

            tabPage.Controls.Add(flp)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                'new theme change
                tabPage.TabBackColor = Color.FromArgb(255, 255, 255)
                tabPage.TabBackColorSelected = Color.FromArgb(255, 173, 1)
                ' tabPage.TabForeColorSelected = Color.FromArgb(255, 255, 255)
                tabPage.Font = New Font("Neo Sans", 12, FontStyle.Regular)
                flp.BackColor = Color.FromArgb(255, 255, 255)
                '  tabPage.Text = tabPage.Text.ToUpper
                tabPage.ToolTipText = tabPage.Text
                tabControlMain.BorderStyle = Windows.Forms.BorderStyle.None
            End If
            'tabPage.Controls.Add(flp1)
            tabControlMain.Controls.Add(tabPage)

        Next
        'Dim flp1 As FlowLayoutPanel = GetFlowPanelArticles()
        'tabControlMain.Controls.Add(flp1)

        'tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        ' AddHandler tabControlMain.Click, AddressOf Parent.tabControlMain_Click
        AddHandler tabControlMain.Click, AddressOf Parent.tabControlMain_Click
    End Sub
    Private Sub AddTabs()
        tabControlMain.Controls.Clear()

        'Dim dvButtonGroup As New DataView(ButtonGroup, String.Empty, "GroupName ASC", DataViewRowState.CurrentRows)

        For Each btnGroup As DataRow In ButtonGroup.Rows
            Dim tabPage As C1DockingTabPage = GetTabPage()
            tabPage.Text = btnGroup("GroupName")
            tabPage.Tag = btnGroup("GroupID")
            Dim flp As FlowLayoutPanel = GetFlowPanel()
            tabPage.Controls.Add(flp)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                'new theme change
                tabPage.TabBackColor = Color.FromArgb(255, 255, 255)
                ' tabPage.TabBackColorSelected = Color.FromArgb(61, 89, 171)
                tabPage.TabBackColorSelected = Color.FromArgb(255, 173, 1)
                ' tabPage.TabForeColorSelected = Color.FromArgb(255, 255, 255)
                tabPage.Font = New Font("Neo Sans", 12, FontStyle.Regular)
                flp.BackColor = Color.FromArgb(255, 255, 255)
                'tabPage.Text = tabPage.Text.ToUpper
                tabPage.ToolTipText = tabPage.Text
                tabControlMain.BorderStyle = Windows.Forms.BorderStyle.None
            End If
            tabControlMain.Controls.Add(tabPage)
        Next
    End Sub

    Public Sub CreateButtonsInTabPanel()

        tabControlMain.Font = New System.Drawing.Font(New FontFamily("Verdana"), 14, GraphicsUnit.Point)
        If CheckAuthorisation(clsAdmin.UserCode, "ADDTAB_CASHMEMO") Then
            tabControlMain.ContextMenuStrip = ArticleCategoryMenuStrip
        End If

        'tabControlMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.FillToEnd
        tabControlMain.ShowToolTips = True
        'tabControlMain.VisualStyle = VisualStyle.Custom
        'tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        AddTabs()
        'tabControlMain.VisualStyle = VisualStyle.Custom
        'tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        Dim dvImageInfo As New DataView

        If (clsDefaultConfiguration.DisplayArticleAlphabetChar) Then
            dvImageInfo = New DataView(ImageInfo, String.Empty, "GroupID ASC, ArticleName ASC", DataViewRowState.CurrentRows)
        Else
            dvImageInfo = New DataView(ImageInfo, String.Empty, String.Empty, DataViewRowState.CurrentRows)
        End If

        'ImageInfo.DefaultView.Sort = "GroupID ASC, ArticleName ASC"

        For Each drButton As DataRow In dvImageInfo.ToTable().Rows
            If clsDefaultConfiguration.DineInButtonWithText Then
                AddArticleButtonTextMiddle(drButton("ArticleCode"), drButton("ArticleName"), drButton("ID"), drButton("GroupID"))
            Else
                AddArticleButton(drButton("ArticleCode"), drButton("ArticleName"), drButton("ID"), drButton("GroupID"))
            End If
        Next
        Dim tabPage As C1DockingTabPage = (From page As C1DockingTabPage In tabControlMain.Controls Where page.Tag = ActiveTabId Select page).FirstOrDefault()
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            'tabControlMain.BackColor = Color.FromArgb(255, 255, 255)

            tabControlMain.BackColor = Color.FromArgb(212, 212, 212)
            tabControlMain.ForeColor = Color.FromArgb(37, 37, 37)
            tabControlMain.Font = New Font("Neo Sans", 12, FontStyle.Bold)
            tabControlMain.TabsSpacing = 1
            tabControlMain.ItemSize = New Size(150, 40)
            tabControlMain.Dock = DockStyle.Top
            tabControlMain.BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            tabControlMain.MaximumSize = New Size((My.Computer.Screen.WorkingArea.Width * 750) / 1366, 600)
            tabControlMain.Size = New Size((My.Computer.Screen.WorkingArea.Width * 750) / 1366, 600)
        End If
        If Not tabPage Is Nothing Then
            tabPage.Show()
        End If
    End Sub

    Public Sub CreateMultiButtonsInTabPanel()

        tabControlMain.Font = New System.Drawing.Font(New FontFamily("Tahoma"), 14, GraphicsUnit.Point)
        'tabControlMain.BorderStyle = Windows.Forms.BorderStyle.None
        ' tabControlMain.RowCount = 2
        If CheckAuthorisation(clsAdmin.UserCode, "ADDTAB_CASHMEMO") Then
            tabControlMain.ContextMenuStrip = ArticleCategoryMenuStrip
        End If

        'tabControlMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.FillToEnd
        tabControlMain.ShowToolTips = True
        'tabControlMain.VisualStyle = VisualStyle.Custom
        'tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        AddMultiTabs()
        'tabControlMain.VisualStyle = VisualStyle.Custom
        'tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        Dim dvImageInfo As New DataView

        If (clsDefaultConfiguration.DisplayArticleAlphabetChar) Then
            dvImageInfo = New DataView(ImageInfo, String.Empty, "GroupID ASC, ArticleName ASC", DataViewRowState.CurrentRows)
        Else
            dvImageInfo = New DataView(ImageInfo, String.Empty, String.Empty, DataViewRowState.CurrentRows)
        End If

        'ImageInfo.DefaultView.Sort = "GroupID ASC, ArticleName ASC"

        '''-comment By Mahesh here if group have direct article then load all group articles (Lazy Loading for Fast Initial Load)
        'For Each drButton As DataRow In dvImageInfo.ToTable().Rows
        '    AddArticleButton(drButton("ArticleCode"), drButton("ArticleName"), drButton("ID"), drButton("GroupID"))
        'Next
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            'tabControlMain.BackColor = Color.FromArgb(255, 255, 255)
            tabControlMain.BackColor = Color.FromArgb(212, 212, 212)
            tabControlMain.ForeColor = Color.FromArgb(37, 37, 37)
            tabControlMain.Font = New Font("Neo Sans", 12, FontStyle.Bold)
            tabControlMain.TabsSpacing = 1
            tabControlMain.ItemSize = New Size(150, 40)
            tabControlMain.Dock = DockStyle.Top
            'tabControlMain.MaximumSize = New Size((My.Computer.Screen.WorkingArea.Width * 817) / 1366, 600)
            'tabControlMain.Size = New Size((My.Computer.Screen.WorkingArea.Width * 817) / 1366, 600)
            'tabControlMain.BorderStyle = Windows.Forms.BorderStyle.None
        End If
        Dim tabPage As C1DockingTabPage = (From page As C1DockingTabPage In tabControlMain.Controls Where page.Tag = ActiveTabId Select page).FirstOrDefault()
        If Not tabPage Is Nothing Then
            tabPage.Show()
        End If

    End Sub
    Public Sub CreateSubButtonsInTabPanelByGroup(ByVal dtImageInfos As DataTable, ByVal parentGroup As String)
        ' tabControlMain.SelectedTab.BackColor = ColorTranslator.FromOle(RGB(255, 192, 128))
        'tabControlMain.Font = New System.Drawing.Font(New FontFamily("Verdana"), 16, GraphicsUnit.Point)
        'If CheckAuthorisation(clsAdmin.UserCode, "ADDTAB_CASHMEMO") Then
        '    tabControlMain.ContextMenuStrip = ArticleCategoryMenuStrip
        'End If

        ''tabControlMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.FillToEnd
        'tabControlMain.ShowToolTips = True
        ''tabControlMain.VisualStyle = VisualStyle.Custom
        ''tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        'AddTabs()
        ''tabControlMain.VisualStyle = VisualStyle.Custom
        ''tabControlMain.TabAreaBackColor = ColorTranslator.FromOle(RGB(122, 155, 206))
        'Dim dvImageInfo As New DataView

        'If (clsDefaultConfiguration.DisplayArticleAlphabetChar) Then
        '    dvImageInfo = New DataView(ImageInfo, String.Empty, "GroupID ASC, ArticleName ASC", DataViewRowState.CurrentRows)
        'Else
        '    dvImageInfo = New DataView(ImageInfo, String.Empty, String.Empty, DataViewRowState.CurrentRows)
        'End If

        'ImageInfo.DefaultView.Sort = "GroupID ASC, ArticleName ASC"
        Dim dvImageInfo As New DataView(dtImageInfos)
        Dim flowPanel = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(0)).FirstOrDefault()
        If flowPanel IsNot Nothing Then
            flowPanel.Controls.Clear()
        End If

        'commented By Mahesh no need to clear control as we are keeping all controls in memory and showing container whenever call
        ''Dim flowPanel1 = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(1)).FirstOrDefault()
        ''If flowPanel1 IsNot Nothing Then
        ''    flowPanel1.Controls.Clear()
        ''End If

        '---Commented by Mahesh As we Need to manipulate it while we are loading articles ..for subgroup size will be less that will manipulate later when we load sub Groups
        'flowPanel.Size = New System.Drawing.Point(669, 45 * Rowcount)
        ' ''flowPanel1.Top = (45 * Rowcount)
        ' ''flowPanel1.MaximumSize = New Drawing.Point(tabControlMain.SelectedTab.Size.Width - 2, tabControlMain.SelectedTab.Size.Height - 45 * Rowcount)
        '  flowPanel.Size = New System.Drawing.Point(flpLabelWidth, 478)

        For Each drButton As DataRow In dvImageInfo.ToTable().Rows
            AddSubTabsButton(drButton("GroupID"), drButton("GroupName"), drButton("GroupID"), parentGroup)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                flowPanel.BackColor = Color.FromArgb(255, 255, 255)
            End If
        Next
        Dim tabPage As C1DockingTabPage = (From page As C1DockingTabPage In tabControlMain.Controls Where page.Tag = ActiveTabId Select page).FirstOrDefault()
        If Not tabPage Is Nothing Then
            tabPage.Show()
        End If
    End Sub
    Public Sub ClearChildArticles(ByVal parentGroup As String)
        Dim flowPanel1 = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(1)).FirstOrDefault()
        '---- Changes By Mahesh Now just hiding mail dated 20-12-2015
        flowPanel1.Visible = False
        'If flowPanel1 IsNot Nothing Then
        '    flowPanel1.Controls.Clear()
        'End If
    End Sub
    Public Sub CreateArticlesInTabPanelByGroup(ByVal dtImageInfos As DataTable, ByVal parentGroup As String, ByVal LastSelectedSubGroup As String)

        'tabControlMain.PerformLayout()
        'tabControlMain.Refresh()
        'If isFirstArticle Then
        '    Dim flowPanel0 = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(0)).FirstOrDefault()
        '    If flowPanel0 IsNot Nothing Then
        '        SubGroupFlowLayoutName = flowPanel0.Name
        '        flowPanel0.Size = New System.Drawing.Size(0, 0)
        '        flowPanel0.Controls.Clear()
        '        flowPanel0.Refresh()
        '    End If
        'End If

        '' This is comment By Mahesh here Implement logic of show/hide of Panel
        'Dim flowPanel As FlowLayoutPanel = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(1)).FirstOrDefault()
        'If flowPanel IsNot Nothing Then
        '    flowPanel.Controls.Clear()
        'End If

        Dim dvImageInfo As New DataView(dtImageInfos)
        Dim FlowPanelFound As Boolean = False
        Dim flowPanelx As FlowLayoutPanel
        If isFirstArticle = False Then
            ''comment by mahesh as New Logic will show and hide if panel is loaded previously 

            For Each tab As Control In tabControlMain.Controls
                If tab.Tag = parentGroup Then
                    For Each fl As Control In tab.Controls
                        If TypeOf fl Is FlowLayoutPanel Then
                            If fl.Name = parentGroup Then
                                fl.Size = New System.Drawing.Point(flpLabelWidth, 45 * Rowcount)
                                Continue For
                            End If
                            fl.Visible = False
                            If fl.Name = LastSelectedSubGroup Then
                                fl.Visible = True
                                FlowPanelFound = True
                            End If
                        End If
                    Next
                    Exit For
                End If
            Next
            If FlowPanelFound Then GoTo FlowPanelFound
            '''  tabControlMain.SelectedTab.Controls.Remove(flowPanel)
            flowPanelx = GetFlowPanelArticles(LastSelectedSubGroup)

            ' flowPanelx.Top = (45 * Rowcount)
            flowPanelx.MaximumSize = New Drawing.Point(tabControlMain.SelectedTab.Size.Width - 3, tabControlMain.SelectedTab.Size.Height - 45 * Rowcount)

            tabControlMain.SelectedTab.Controls.Add(flowPanelx)
            flowPanelx.MaximumSize = New System.Drawing.Size(flpLabelWidth, flpLabelHeightArticles)
            flowPanelx.Size = New System.Drawing.Size(flpLabelWidth, flpLabelHeightArticles)
        Else
            flowPanelx = (From tab As C1DockingTabPage In tabControlMain.Controls Where tab.Tag = parentGroup Select tab.Controls(0)).FirstOrDefault()

            If flowPanelx.Controls.Count > 0 Then GoTo FlowPanelFound
            '  flowPanel.Top = (45 * Rowcount)
            '  flowPanel.MaximumSize = New Drawing.Point(tabControlMain.SelectedTab.Size.Width - 2, 520)
            'code change by vipul on 10-08-2017 for adjust grid scroll bar for resolution 1024*768
            flowPanelx.MaximumSize = New System.Drawing.Size(flpLabelWidth + 50, flpLabelHeight + 45)
            flowPanelx.Size = New System.Drawing.Size(flpLabelWidth + 50, flpLabelHeight + 45)

            'flowPanelx.MaximumSize = New System.Drawing.Size(flpLabelWidth + 75, flpLabelHeight + 110)
            'flowPanelx.Size = New System.Drawing.Size(flpLabelWidth + 75, flpLabelHeight + 110)
            flowPanelx.Location = New System.Drawing.Point(0, 0)
            ' flowPanelx.MaximumSize = New Drawing.Point(tabControlMain.SelectedTab.Size.Width - 3, tabControlMain.SelectedTab.Size.Height)
        End If

        For Each drButton As DataRow In dvImageInfo.ToTable().Rows
            If clsDefaultConfiguration.DineInButtonWithText Then
                AddSubArticlesTextMiddle(drButton("ArticleCode"), drButton("ArticleName"), drButton("ID"), parentGroup, flowPanelx, drButton("IsKit"), drButton("KitArticleDesc"))
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    flowPanelx.BackColor = Color.FromArgb(255, 255, 255)
                End If
            Else
                AddSubArticles(drButton("ArticleCode"), drButton("ArticleName"), drButton("ID"), parentGroup, flowPanelx, drButton("IsKit"), drButton("KitArticleDesc"))
            End If
        Next

FlowPanelFound:
        Dim tabPage As C1DockingTabPage = (From page As C1DockingTabPage In tabControlMain.Controls Where page.Tag = ActiveTabId Select page).FirstOrDefault()
        If Not tabPage Is Nothing Then
            tabPage.Show()
        End If

    End Sub
    Public Function CheckIfArticleExistInActibeTab(ByVal articleCode As String) As Boolean
        Dim ctrlBtn As CtrlBtn = (From gridControl As TableLayoutPanel In tabControlMain.SelectedTab.Controls(0).Controls Where DirectCast(gridControl.Controls(0), CtrlBtn).SetArticleCode = articleCode
                                    Select gridControl.Controls(0)).FirstOrDefault()
        If Not ctrlBtn Is Nothing Then
            Return True
        End If
        Return False
    End Function

    Public Sub ShowArticleImage(ByVal StrArticle As String, ByRef btn As Button)
        Try
            Dim url As String
            Dim objComm As New clsCommon
            url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            If url <> String.Empty Then
                Dim img As Image


                Dim ratio As Double
                Dim s As Size
                btn.Image = Nothing
                'btn.imageLayout = Nothing
                If btn.Width > 0 And btn.Height > 0 Then
                    Try
                        img = Image.FromFile(url)

                        ratio = img.Height / img.Width
                        s.Width = btn.Width
                        s.Height = s.Width * ratio
                        If s.Height > btn.Height Then
                            ratio = btn.Height / s.Height
                            s.Width = s.Width * ratio
                            s.Height = s.Height * ratio + 5
                        End If
                        If s.Width > 0 And s.Height > 0 Then
                            btn.Image = New Bitmap(img, s)

                            'btn.imageLayout = ImageLayout.Center
                        End If
                        img.Dispose()
                    Catch ex As Exception

                    End Try
                End If
            Else
                'btn.Image = My.Resources.;
                'btn.imageLayout = ImageLayout.Center
                btn.Image = Nothing

            End If
            'btn.image = Image.FromFile(url)

            'btn.imageLayout = ImageLayout.Center
            'btn.SizeMode = PictureBoxSizeMode.AutoSize


        Catch ex As Exception
            'btn.Image = My.Resources.NA
            'btn.Image = Nothing
        End Try
    End Sub

    Public Sub ShowArticleImageMOD(ByVal StrArticle As String, ByRef btn As Button)
        Try
            Dim url As String
            Dim objComm As New clsCommon
            url = objComm.GetArticleImage(StrArticle, clsAdmin.Articleimagepath)
            If url <> String.Empty Then
                Dim img As Image


                Dim ratio As Double
                Dim s As Size
                btn.Image = Nothing
                'btn.imageLayout = Nothing
                If btn.Width > 0 And btn.Height > 0 Then
                    Try
                        img = Image.FromFile(url)

                        s.Height = btn.Height
                        s.Width = btn.Width

                        'ratio = img.Height / img.Width
                        's.Width = btn.Width
                        's.Height = s.Width * ratio
                        'If s.Height > btn.Height Then
                        '    ratio = btn.Height / s.Height
                        '    s.Width = s.Width * ratio
                        '    s.Height = s.Height * ratio + 5
                        'End If
                        'If s.Width > 0 And s.Height > 0 Then
                        '    btn.Image = New Bitmap(img, s)

                        '    'btn.imageLayout = ImageLayout.Center
                        'End If
                        btn.Image = New Bitmap(img, s)

                        img.Dispose()
                    Catch ex As Exception
                        btn.Text = ImageArticleName
                    End Try
                End If
            Else
                'btn.Image = My.Resources.;
                'btn.imageLayout = ImageLayout.Center
                btn.Image = Nothing

            End If
            'btn.image = Image.FromFile(url)

            'btn.imageLayout = ImageLayout.Center
            'btn.SizeMode = PictureBoxSizeMode.AutoSize


        Catch ex As Exception
            'btn.Image = My.Resources.NA
            'btn.Image = Nothing
        End Try
    End Sub

    Public Shared Function resizeImage(ByVal sourceBMP As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim result As Bitmap = New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(result)
            g.DrawImage(sourceBMP, 0, 0, width, height)
        End Using
        Return result

    End Function

    Public Sub CreateDataTable()
        _ImageInfo = New DataTable()
        _ImageInfo.Columns.Add(New DataColumn("ArticleCode"))
        _ImageInfo.Columns.Add(New DataColumn("ImagePath"))
        _ImageInfo.Columns.Add(New DataColumn("HotKeys"))

        For Each item In Directory.GetFiles("D:\Images")




            '//FileInfo kl= 
            Dim kl As System.IO.FileInfo = New FileInfo(item)
            Dim dr As DataRow = _ImageInfo.NewRow()
            dr("ArticleCode") = kl.Name.Replace(kl.Extension, "")
            dr("ImagePath") = kl.FullName
            _ImageInfo.Rows.Add(dr)

        Next
    End Sub

    Private Sub CtrlBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim objCM As New clsCashMemo()

            Dim articleCode As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            If articleCode.Text <> String.Empty Or articleCode.Image IsNot Nothing Then
                Dim dtable As New DataTable

                If articleCode.SetArticleCode = String.Empty Then
                    dtable = objCM.GetButtonArticle(articleCode.Name)
                    articleCode.SetArticleCode = dtable.Rows(0)("ArticleCode")
                    dtable.Clear()
                End If
                Dim dt As New DataTable
                Dim cdr As New DataTable
                cdr = objCM.GetComponentArticleItems(articleCode.SetArticleCode)

                If cdr IsNot Nothing AndAlso cdr.Rows.Count > 0 Then
                    For index = 0 To cdr.Rows.Count - 1

                        'dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        'setgridview(dt)
                        'dt.Clear()

                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        dt(0)("Quantity") = cdr(index)("Quantity")
                        'compqty = cdr(index)("Quantity")
                        'flag = 3
                        'setgridview(dt)
                        dt.Clear()
                    Next
                Else
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, articleCode.SetArticleCode, False, clsAdmin.LangCode)
                    'setgridview(dt)
                End If

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'CreateDataTable()


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ArticleContextMenuStrip_ItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
        'Try
        '    Dim openMrp As Boolean = False

        '    Cursor.Current = Cursors.WaitCursor
        '    Dim dt As New DataTable

        '    If e.ClickedItem.Text = "Add" Then
        '        If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso MSalesPerson.CtrlSalesPersons.SelectedIndex < 0 Then
        '            ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
        '            Exit Sub
        '        End If

        '        Dim obj As New frmNItemSearch
        '        obj.ShowDialog()

        '        If Not obj.SearchResult Is Nothing Then
        '            dt = objCM.GetItemDetails(clsAdmin.SiteCode, obj.ItemRow("ArticleCode").ToString(), openMrp, clsAdmin.LangCode)
        '            addArticle(ArticleButtonName, dt)
        '            objCM.SaveAndDeleteButtonArticleData(ArticleButtonName.Name, obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(), e.ClickedItem.Text)

        '        End If
        '    ElseIf e.ClickedItem.Text = "Edit" Then
        '        If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso MSalesPerson.CtrlSalesPersons.SelectedIndex < 0 Then
        '            ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
        '            Exit Sub
        '        End If

        '        Dim obj As New frmNItemSearch
        '        obj.ShowDialog()

        '        If Not obj.SearchResult Is Nothing Then
        '            dt = objCM.GetItemDetails(clsAdmin.SiteCode, obj.ItemRow("ArticleCode").ToString(), openMrp, clsAdmin.LangCode)
        '            addArticle(ArticleButtonName, dt)
        '            objCM.SaveAndDeleteButtonArticleData(ArticleButtonName.Name, obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(), e.ClickedItem.Text)
        '        End If
        '    ElseIf e.ClickedItem.Text = "Remove" Then

        '        addArticle(ArticleButtonName, dt, e.ClickedItem.Text)
        '        objCM.SaveAndDeleteButtonArticleData(ArticleButtonName.Name, String.Empty, String.Empty, e.ClickedItem.Text)
        '    End If

        '    Cursor.Current = Cursors.Default
        'Catch ex As Exception
        '    LogException(ex)
        'End Try
    End Sub

    Private Sub bt_ContextChange(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)

        'Dim kl As CtrlBtn = sender
        'ShowMessage(kl.Text, "")
    End Sub

    Private Sub CtrlMODMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'AddHandler ArticleCategoryMenuStrip.ItemClicked, AddressOf AddCategoryMenuItem_Click
    End Sub


    Public Sub LoadArticle()
        Me.SuspendLayout()

        '//this.Width = ButtonSize.Width * ButtonTotalColumn
        '//this.Height = ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2))
        '//this.Size = new System.Drawing.Size((ButtonSize.Width) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))
        'Dim validateRow As Integer = _ImageInfo.Rows.Count
        'NumberOfButtons = validateRow / 100
        'Me.flpMenuButton.Size = New System.Drawing.Size((ButtonSize.Width + 90) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))

        CreateButtonsInTabPanel()
        RemoveHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
        AddHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
    End Sub
    Public Sub LoadMultiArticle()
        Me.SuspendLayout()

        '//this.Width = ButtonSize.Width * ButtonTotalColumn
        '//this.Height = ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2))
        '//this.Size = new System.Drawing.Size((ButtonSize.Width) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))
        'Dim validateRow As Integer = _ImageInfo.Rows.Count
        'NumberOfButtons = validateRow / 100
        'Me.flpMenuButton.Size = New System.Drawing.Size((ButtonSize.Width + 90) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))

        CreateMultiButtonsInTabPanel()
        RemoveHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
        AddHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
    End Sub
    Public Sub LoadArticleByGroup(ByVal dtViews As DataTable, ByVal parentGroup As String, ByVal LastSelectedSubGroup As String)
        Me.SuspendLayout()

        '//this.Width = ButtonSize.Width * ButtonTotalColumn
        '//this.Height = ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2))
        '//this.Size = new System.Drawing.Size((ButtonSize.Width) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))
        'Dim validateRow As Integer = _ImageInfo.Rows.Count
        'NumberOfButtons = validateRow / 100
        'Me.flpMenuButton.Size = New System.Drawing.Size((ButtonSize.Width + 90) * ButtonTotalColumn, ButtonSize.Height * Convert.ToInt32(Math.Ceiling((NumberOfButtons) / 2)))

        CreateArticlesInTabPanelByGroup(dtViews, parentGroup, LastSelectedSubGroup)
        RemoveHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
        AddHandler tabControlMain.DoubleClick, AddressOf TabDoubleClicked
    End Sub
    'Private Sub AddCategoryMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCategoryMenuItem.Click

    'End Sub
    Private isNewTab As Boolean
    Private doubleClickedTabName As String
    Private Sub CategoryField_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles CategoryField.LostFocus
        Dim isExist As C1DockingTabPage
        Dim clickedTab As C1DockingTabPage
        If Not String.IsNullOrEmpty(CategoryField.Text) Then
            If isNewTab Then
                isExist = (From tab As C1DockingTabPage In tabControlMain.Controls Where (UCase(tab.Text) = UCase(CategoryField.Text) AndAlso tab.Tag <> tabControlMain.TabPages(tabControlMain.TabCount - 1).Tag) Select tab).FirstOrDefault()
            Else
                clickedTab = (From tab As C1DockingTabPage In tabControlMain.Controls Where (UCase(tab.Text) = UCase(doubleClickedTabName)) Select tab).FirstOrDefault()
                isExist = (From tab As C1DockingTabPage In tabControlMain.Controls Where (UCase(tab.Text) = UCase(CategoryField.Text) AndAlso tab.Tag <> clickedTab.Tag) Select tab).FirstOrDefault()
            End If
            'isExist = (From tab As C1DockingTabPage In tabControlMain.Controls Where (UCase(tab.Text) = UCase(CategoryField.Text) AndAlso Not (tab Is tabControlMain.SelectedTab)) Select tab).FirstOrDefault()
            If isExist Is Nothing Then
                Dim objCM As New clsCashMemo
                If isNewTab Then
                    objCM.SaveAndEditButtonGroup(tabControlMain.TabPages(tabControlMain.TabCount - 1).Tag, CategoryField.Text, "Edit", clsAdmin.UserCode, clsAdmin.SiteCode)
                    isNewTab = False
                Else
                    clickedTab = (From tab As C1DockingTabPage In tabControlMain.Controls Where (UCase(tab.Text) = UCase(doubleClickedTabName)) Select tab).FirstOrDefault()
                    objCM.SaveAndEditButtonGroup(clickedTab.Tag, CategoryField.Text, "Edit", clsAdmin.UserCode, clsAdmin.SiteCode)
                    doubleClickedTabName = String.Empty
                End If
                ActiveTabId = tabControlMain.SelectedTab.Tag
                RefreshTabs(objCM)
            Else
                'MessageBox.Show("Article Category already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ShowMessage("Article Category already exist", "Warning")
            End If
        End If
        CategoryField.Visible = False
    End Sub



    Private Sub ArticleCategoryMenuStrip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ArticleCategoryMenuStrip.KeyPress

    End Sub

    Private Sub TabDoubleClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        'If CheckAuthorisation(clsAdmin.UserCode, "ADDTAB_CASHMEMO") Then
        '    Dim objCM As New clsCashMemo
        '    If objCM.CheckIFGlNoRangeIsAvailable(clsAdmin.SiteCode) = False Then
        '        ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
        '        Exit Sub
        '    End If
        '    doubleClickedTabName = tabControlMain.SelectedTab.Text
        '    CategoryField.Text = tabControlMain.SelectedTab.Text
        '    ChangeTabName()
        'End If
    End Sub

    Private Sub ChangeTabName()
        CategoryField.Visible = True
        CategoryField.Focus()
        CategoryField.SelectAll()
        'CategoryField.Text = tabControlMain.SelectedTab.Text
    End Sub
End Class
