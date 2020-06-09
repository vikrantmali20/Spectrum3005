Public Class CtrlCashSummary

    Public Property hdrText() As String
        Get
            Return CtrlHeader1.HdrText
        End Get
        Set(ByVal value As String)
            CtrlHeader1.HdrText = value
        End Set
    End Property

    Public Property lbl1() As String
        Get
            Return CtrlLabel1.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel1.Text = value
        End Set
    End Property

    Public Property lbl2() As String
        Get
            Return CtrlLabel2.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel2.Text = value
        End Set
    End Property

    Public Property lbl3() As String
        Get
            Return CtrlLabel3.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel3.Text = value
        End Set
    End Property

    Public Property lbl4() As String
        Get
            Return CtrlLabel4.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel4.Text = value
        End Set
    End Property
    Public Property lbl5() As String
        Get
            Return CtrlLabel5.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel5.Text = value
        End Set
    End Property

    Public Property lbl6() As String
        Get
            Return CtrlLabel6.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel6.Text = value
        End Set
    End Property

    Public Property lbl7() As String
        Get
            Return CtrlLabel7.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel7.Text = value
        End Set
    End Property

    Public Property lbl8() As String
        Get
            Return CtrlLabel8.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel8.Text = value
        End Set
    End Property

    Public Property lbl9() As String
        Get
            Return CtrlLabel09.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel09.Text = value
        End Set
    End Property

    Public Property lbl10() As String
        Get
            Return CtrlLabel010.Text
        End Get
        Set(ByVal value As String)
            CtrlLabel010.Text = value
        End Set
    End Property


    '------------------------
    'for swiping the postions of labels in case we need to show credit sale 
    Public Property lbl1location As Point
        Get
            Return CtrlLabel1.Location
        End Get
        Set(value As Point)
            CtrlLabel1.Location = value
        End Set
    End Property

    Public Property lbl2Location As Point
        Get
            Return CtrlLabel2.Location
        End Get
        Set(value As Point)
            CtrlLabel2.Location = value
        End Set
    End Property

    Public Property lbl3Location As Point
        Get
            Return CtrlLabel3.Location
        End Get
        Set(value As Point)
            CtrlLabel3.Location = value
        End Set
    End Property
    Public Property lbl4Location As Point
        Get
            Return CtrlLabel4.Location
        End Get
        Set(value As Point)
            CtrlLabel4.Location = value
        End Set
    End Property
    Public Property lbl5Location As Point
        Get
            Return CtrlLabel5.Location
        End Get
        Set(value As Point)
            CtrlLabel5.Location = value
        End Set
    End Property


    Public Property lbl6Location As Point
        Get
            Return CtrlLabel6.Location
        End Get
        Set(value As Point)
            CtrlLabel6.Location = value
        End Set
    End Property

    Public Property lbl7Location As Point
        Get
            Return CtrlLabel7.Location
        End Get
        Set(value As Point)
            CtrlLabel7.Location = value
        End Set
    End Property
    Public Property lbl8Location As Point
        Get
            Return CtrlLabel8.Location
        End Get
        Set(value As Point)
            CtrlLabel8.Location = value
        End Set
    End Property
    Public Property lbl9Location As Point
        Get
            Return CtrlLabel9.Location
        End Get
        Set(value As Point)
            CtrlLabel9.Location = value
        End Set

    End Property
    Public Property lbl10Location As Point
        Get
            Return CtrlLabel010.Location
        End Get
        Set(value As Point)
            CtrlLabel010.Location = value
        End Set

    End Property


    '--------------------------------------

    Public Property lbltxt1() As String
        Get
            Return CtrllblGrossAmt.Text
        End Get
        Set(ByVal value As String)
            CtrllblGrossAmt.Text = value
        End Set
    End Property

    Public Property lbltxt2() As String
        Get
            Return CtrllblTaxAmt.Text
        End Get
        Set(ByVal value As String)
            CtrllblTaxAmt.Text = value
        End Set
    End Property

    Public Property lbltxt3() As String
        Get
            Return CtrllblDiscAmt.Text
        End Get
        Set(ByVal value As String)
            CtrllblDiscAmt.Text = value
        End Set
    End Property

    Public Property lbltxt4() As String
        Get
            Return CtrllblNetAmt.Text
        End Get
        Set(ByVal value As String)
            CtrllblNetAmt.Text = value
        End Set
    End Property

    Public Property lbltxt5() As String
        Get
            Return CtrlLabeltxt5.Text
        End Get
        Set(ByVal value As String)
            CtrlLabeltxt5.Text = value
        End Set
    End Property
    Public Property lbltxt6() As String
        Get
            Return CtrlLabelTxt6.Text
        End Get
        Set(ByVal value As String)
            CtrlLabelTxt6.Text = value
        End Set
    End Property
    Public Property lbltxt7() As String
        Get
            Return CtrlLabelTxt7.Text
        End Get
        Set(ByVal value As String)
            CtrlLabelTxt7.Text = value
        End Set
    End Property

    Public Property lbltxt8() As String
        Get
            Return CtrlLabelTxt8.Text
        End Get
        Set(ByVal value As String)
            CtrlLabelTxt8.Text = value
        End Set
    End Property
    Public Property lbltxt9() As String
        Get
            Return CtrlLabelTxt9.Text
        End Get
        Set(ByVal value As String)
            CtrlLabelTxt9.Text = value
        End Set
    End Property
    Public Property lbltxt10() As String
        Get
            Return CtrlLabelTxt10.Text
        End Get
        Set(ByVal value As String)
            CtrlLabelTxt10.Text = value
        End Set
    End Property

    '------------------
    Public Property lbltxt1location As Point
        Get
            Return CtrllblGrossAmt.Location
        End Get
        Set(ByVal value As Point)
            CtrllblGrossAmt.Location = value
        End Set
    End Property

    Public Property lbltxt2location As Point
        Get
            Return CtrllblTaxAmt.Location
        End Get
        Set(ByVal value As Point)
            CtrllblTaxAmt.Location = value
        End Set
    End Property

    Public Property lbltxt3location As Point
        Get
            Return CtrllblDiscAmt.Location
        End Get
        Set(ByVal value As Point)
            CtrllblDiscAmt.Location = value
        End Set
    End Property

    Public Property lbltxt4location As Point
        Get
            Return CtrllblNetAmt.Location
        End Get
        Set(ByVal value As Point)
            CtrllblNetAmt.Location = value
        End Set
    End Property

    Public Property lbltxt5location As Point
        Get
            Return CtrlLabeltxt5.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabeltxt5.Location = value
        End Set
    End Property


    Public Property lbltxt6location As Point
        Get
            Return CtrlLabelTxt6.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabelTxt6.Location = value
        End Set
    End Property
    Public Property lbltxt7location As Point
        Get
            Return CtrlLabelTxt7.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabelTxt7.Location = value
        End Set
    End Property

    Public Property lbltxt8location As Point
        Get
            Return CtrlLabelTxt8.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabelTxt8.Location = value
        End Set
    End Property
    Public Property lbltxt9location As Point
        Get
            Return CtrlLabelTxt9.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabelTxt9.Location = value
        End Set
    End Property
    Public Property lbltxt10location As Point
        Get
            Return CtrlLabelTxt10.Location
        End Get
        Set(ByVal value As Point)
            CtrlLabelTxt10.Location = value
        End Set
    End Property
    Private _AlignChangeForCashSummary As String
    Public Property AlignChangeForCashSummary() As String
        Get
            Return _AlignChangeForCashSummary
        End Get
        Set(ByVal value As String)
            _AlignChangeForCashSummary = value
        End Set
    End Property
    Public Sub pSetFontSize(ByVal lblSeqNbr As Int16)
        Dim bigfont As New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Select Case lblSeqNbr
            Case 1
                CtrllblGrossAmt.Font = bigfont
            Case 2
                CtrllblTaxAmt.Font = bigfont
            Case 3
                CtrllblDiscAmt.Font = bigfont
            Case 4
                CtrllblNetAmt.Font = bigfont
            Case 5
                CtrlLabeltxt5.Font = bigfont
            Case 6
                CtrlLabelTxt6.Font = bigfont
            Case 7
                CtrlLabelTxt7.Font = bigfont
            Case 8
                CtrlLabelTxt8.Font = bigfont
            Case 9
                CtrlLabelTxt9.Font = bigfont
            Case 10
                CtrlLabelTxt10.Font = bigfont

            Case Else

        End Select

    End Sub
    Public Function ThemeChange()

        If AlignChangeForCashSummary = "Fast Cash Memo" Or AlignChangeForCashSummary = "Cash Memo" Then

            Me.C1Sizer1.Grid.Clear()
            ' Me.Size = New System.Drawing.Size(208, 240)
            '   Me.Dock = DockStyle.Right
            ' C1Sizer1.SplitterWidth = 1
            C1Sizer1.SplitterWidth = 1
            C1Sizer1.Border.Color = Color.Transparent
            Me.BackColor = Color.FromArgb(49, 49, 49)
            ''sizer
            ' C1Sizer1.Size = New System.Drawing.Size(208, 240)
            ' C1Sizer1.Location = New System.Drawing.Point(5, 121)
            C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            ''cashmemo summary label
            Me.CtrlHeader1.Size = New System.Drawing.Size(158, 21)
            Me.CtrlHeader1.Location = New System.Drawing.Point(5, 5)
            'Me.CtrlHeader1.HdrText = Me.CtrlHeader1.HdrText.ToUpper
            Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)


            ''grossamt lbl
            Me.CtrlLabel1.Size = New System.Drawing.Size(97, 24)
            Me.CtrlLabel1.Location = New System.Drawing.Point(4, 33)
            Me.CtrlLabel1.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
            'Me.CtrlLabel1.Text = Me.CtrlLabel1.Text.ToUpper
            ''grosamt txt
            Me.CtrllblGrossAmt.Size = New System.Drawing.Size(115, 24)
            Me.CtrllblGrossAmt.Location = New System.Drawing.Point(102, 33)
            Me.CtrllblGrossAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblGrossAmt.BringToFront()
            ' Me.CtrllblGrossAmt.Text = Me.CtrllblGrossAmt.Text.ToUpper
            ''tax amt lbl
            Me.CtrlLabel2.Size = New System.Drawing.Size(97, 24)
            Me.CtrlLabel2.Location = New System.Drawing.Point(4, 58)
            Me.CtrlLabel2.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
            'Me.CtrlLabel2.Text = Me.CtrlLabel2.Text.ToUpper

            Me.CtrllblTaxAmt.Size = New System.Drawing.Size(115, 24)
            Me.CtrllblTaxAmt.Location = New System.Drawing.Point(102, 58)
            Me.CtrllblTaxAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblTaxAmt.BringToFront()
            ' Me.CtrllblTaxAmt.Text = Me.CtrllblTaxAmt.Text.ToUpper

            ''disc lbl
            Me.CtrlLabel3.Size = New System.Drawing.Size(97, 24)
            Me.CtrlLabel3.Location = New System.Drawing.Point(4, 83)
            Me.CtrlLabel3.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
            ' Me.CtrlLabel3.Text = Me.CtrlLabel3.Text.ToUpper

            Me.CtrllblDiscAmt.Size = New System.Drawing.Size(115, 24)
            Me.CtrllblDiscAmt.Location = New System.Drawing.Point(102, 83)
            Me.CtrllblDiscAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblDiscAmt.BringToFront()
            ' Me.CtrllblDiscAmt.Text = Me.CtrllblDiscAmt.Text.ToUpper

            '' net amt lbl
            Me.CtrlLabel4.Size = New System.Drawing.Size(97, 24)
            Me.CtrlLabel4.Location = New System.Drawing.Point(4, 108)
            Me.CtrlLabel4.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
            'Me.CtrlLabel4.Text = Me.CtrlLabel4.Text.ToUpper

            Me.CtrllblNetAmt.Size = New System.Drawing.Size(115, 24)
            Me.CtrllblNetAmt.Location = New System.Drawing.Point(102, 108)
            Me.CtrllblNetAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblNetAmt.BringToFront()
            ' Me.CtrllblNetAmt.Text = Me.CtrllblNetAmt.Text.ToUpper


            Me.CtrlLabel5.Size = New System.Drawing.Size(97, 24)
            Me.CtrlLabel5.Location = New System.Drawing.Point(4, 133)
            Me.CtrlLabel5.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel5.TextAlign = ContentAlignment.MiddleLeft
            ' Me.CtrlLabel5.Text = Me.CtrlLabel5.Text.ToUpper

            Me.CtrlLabeltxt5.Size = New System.Drawing.Size(115, 24)
            Me.CtrlLabeltxt5.Location = New System.Drawing.Point(102, 133)
            Me.CtrlLabeltxt5.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabeltxt5.BringToFront()
            'Me.CtrlLabeltxt5.Text = Me.CtrlLabeltxt5.Text.ToUpper

        ElseIf AlignChangeForCashSummary = "Sales Order Old" Then
            ' Me.C1Sizer1.Grid.Clear()
            ' Me.Size = New System.Drawing.Size(208, 240)
            '   Me.Dock = DockStyle.Right
            Me.BackColor = Color.FromArgb(49, 49, 49)
            ''sizer
            ' C1Sizer1.Size = New System.Drawing.Size(208, 240)
            ' C1Sizer1.Location = New System.Drawing.Point(5, 121)
            C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            ''cashmemo summary label
            ' Me.CtrlHeader1.Size = New System.Drawing.Size(158, 21)
            ' Me.CtrlHeader1.Location = New System.Drawing.Point(5, 14)
            ' Me.CtrlHeader1.HdrText = Me.CtrlHeader1.HdrText.ToUpper
            Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)
            ''grossamt lbl
            Me.CtrlLabel1.Size = New System.Drawing.Size(127, 20)
            ' Me.CtrlLabel1.Location = New System.Drawing.Point(4, 49)
            Me.CtrlLabel1.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel1.Text = Me.CtrlLabel1.Text.ToUpper
            ''grosamt txt
            ' Me.CtrllblGrossAmt.Size = New System.Drawing.Size(92, 20)
            ' Me.CtrllblGrossAmt.Location = New System.Drawing.Point(132, 27)
            Me.CtrllblGrossAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblGrossAmt.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrllblGrossAmt.BringToFront()
            Me.CtrllblGrossAmt.Text = Me.CtrllblGrossAmt.Text.ToUpper
            ''tax amt lbl
            ' Me.CtrlLabel2.Size = New System.Drawing.Size(127, 18)
            ' Me.CtrlLabel2.Location = New System.Drawing.Point(3, 75)
            Me.CtrlLabel2.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel2.Text = Me.CtrlLabel2.Text.ToUpper

            ' Me.CtrllblTaxAmt.Size = New System.Drawing.Size(92, 18)
            ' Me.CtrllblTaxAmt.Location = New System.Drawing.Point(132, 49)
            Me.CtrllblTaxAmt.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrllblTaxAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblTaxAmt.BringToFront()
            Me.CtrllblTaxAmt.Text = Me.CtrllblTaxAmt.Text.ToUpper

            ''disc lbl
            ' Me.CtrlLabel3.Size = New System.Drawing.Size(127, 18)
            '  Me.CtrlLabel3.Location = New System.Drawing.Point(4, 101)
            Me.CtrlLabel3.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel3.Text = Me.CtrlLabel3.Text.ToUpper

            ' Me.CtrllblDiscAmt.Size = New System.Drawing.Size(92, 18)
            ' Me.CtrllblDiscAmt.Location = New System.Drawing.Point(132, 69)
            Me.CtrllblDiscAmt.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrllblDiscAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblDiscAmt.BringToFront()
            Me.CtrllblDiscAmt.Text = Me.CtrllblDiscAmt.Text.ToUpper

            '' net amt lbl
            ' Me.CtrlLabel4.Size = New System.Drawing.Size(127, 18)
            ' Me.CtrlLabel4.Location = New System.Drawing.Point(4, 127)
            Me.CtrlLabel4.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel4.Text = Me.CtrlLabel4.Text.ToUpper

            '  Me.CtrllblNetAmt.Size = New System.Drawing.Size(92, 18)
            ' Me.CtrllblNetAmt.Location = New System.Drawing.Point(132, 89)
            Me.CtrllblNetAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblNetAmt.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrllblNetAmt.BringToFront()
            Me.CtrllblNetAmt.Text = Me.CtrllblNetAmt.Text.ToUpper

            Me.CtrlLabel5.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel5.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel5.Text = Me.CtrlLabel5.Text.ToUpper

            '  Me.CtrlLabeltxt5.Size = New System.Drawing.Size(92, 17)
            '  Me.CtrlLabeltxt5.Location = New System.Drawing.Point(132, 109)
            Me.CtrlLabeltxt5.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabeltxt5.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabeltxt5.BringToFront()
            Me.CtrlLabeltxt5.Text = Me.CtrlLabeltxt5.Text.ToUpper


            Me.CtrlLabel6.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel6.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel6.Text = Me.CtrlLabel6.Text.ToUpper


            ' Me.CtrlLabelTxt6.Size = New System.Drawing.Size(92, 18)
            ' Me.CtrlLabelTxt6.Location = New System.Drawing.Point(132, 128)
            Me.CtrlLabelTxt6.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabelTxt6.BringToFront()
            Me.CtrlLabelTxt6.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabelTxt6.Text = Me.CtrlLabelTxt6.Text.ToUpper

            Me.CtrlLabel7.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel7.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel7.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel7.Text = Me.CtrlLabel7.Text.ToUpper
            ' CtrlLabel7.Size = New Size(129, 17.5)

            '  Me.CtrlLabelTxt7.Size = New System.Drawing.Size(92, 18)
            '  Me.CtrlLabelTxt7.Location = New System.Drawing.Point(132, 148)
            Me.CtrlLabelTxt7.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabelTxt7.BringToFront()
            Me.CtrlLabelTxt7.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabelTxt7.Text = Me.CtrlLabelTxt7.Text.ToUpper


            Me.CtrlLabel010.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel010.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel010.TextAlign = ContentAlignment.MiddleLeft

            Me.CtrlLabel010.Text = Me.CtrlLabel010.Text.ToUpper


            ' Me.CtrlLabelTxt10.Size = New System.Drawing.Size(92, 19)
            ' Me.CtrlLabelTxt10.Location = New System.Drawing.Point(132, 173)
            Me.CtrlLabelTxt10.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabelTxt10.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabelTxt10.BringToFront()
            Me.CtrlLabelTxt10.Text = Me.CtrlLabelTxt10.Text.ToUpper
        ElseIf AlignChangeForCashSummary = "Sales Order Old Cancel" Then
            Me.C1Sizer1.Grid.Clear()
            ' Me.Size = New System.Drawing.Size(208, 240)
            '   Me.Dock = DockStyle.Right
            Me.BackColor = Color.FromArgb(49, 49, 49)
            ''sizer
            ' C1Sizer1.Size = New System.Drawing.Size(208, 240)
            ' C1Sizer1.Location = New System.Drawing.Point(5, 121)
            C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            ''cashmemo summary label
            ' Me.CtrlHeader1.Size = New System.Drawing.Size(158, 21)
            ' Me.CtrlHeader1.Location = New System.Drawing.Point(5, 14)
            ' Me.CtrlHeader1.HdrText = Me.CtrlHeader1.HdrText.ToUpper
            Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)
            ''grossamt lbl
            ' Me.CtrlLabel1.Size = New System.Drawing.Size(99, 22)
            ' Me.CtrlLabel1.Location = New System.Drawing.Point(4, 49)
            Me.CtrlLabel1.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel1.Text = Me.CtrlLabel1.Text.ToUpper
            ''grosamt txt
            Me.CtrllblGrossAmt.Size = New System.Drawing.Size(92, 20)
            Me.CtrllblGrossAmt.Location = New System.Drawing.Point(132, 27)
            Me.CtrllblGrossAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblGrossAmt.BringToFront()
            Me.CtrllblGrossAmt.Text = Me.CtrllblGrossAmt.Text.ToUpper
            ''tax amt lbl
            Me.CtrlLabel2.Size = New System.Drawing.Size(127, 18)
            ' Me.CtrlLabel2.Location = New System.Drawing.Point(3, 75)
            Me.CtrlLabel2.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel2.Text = Me.CtrlLabel2.Text.ToUpper

            Me.CtrllblTaxAmt.Size = New System.Drawing.Size(92, 18)
            Me.CtrllblTaxAmt.Location = New System.Drawing.Point(132, 49)
            Me.CtrllblTaxAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblTaxAmt.BringToFront()
            Me.CtrllblTaxAmt.Text = Me.CtrllblTaxAmt.Text.ToUpper

            ''disc lbl
            Me.CtrlLabel3.Size = New System.Drawing.Size(127, 19)
            Me.CtrlLabel3.Location = New System.Drawing.Point(5, 69)
            Me.CtrlLabel3.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel3.Text = Me.CtrlLabel3.Text.ToUpper

            Me.CtrllblDiscAmt.Size = New System.Drawing.Size(92, 19)
            Me.CtrllblDiscAmt.Location = New System.Drawing.Point(132, 69)
            Me.CtrllblDiscAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblDiscAmt.BringToFront()
            Me.CtrllblDiscAmt.Text = Me.CtrllblDiscAmt.Text.ToUpper

            '' net amt lbl
            Me.CtrlLabel4.Size = New System.Drawing.Size(127, 19)
            Me.CtrlLabel4.Location = New System.Drawing.Point(5, 89)
            Me.CtrlLabel4.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel4.Text = Me.CtrlLabel4.Text.ToUpper

            Me.CtrllblNetAmt.Size = New System.Drawing.Size(92, 19)
            Me.CtrllblNetAmt.Location = New System.Drawing.Point(132, 89)
            Me.CtrllblNetAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblNetAmt.BringToFront()
            Me.CtrllblNetAmt.Text = Me.CtrllblNetAmt.Text.ToUpper

            Me.CtrlLabel5.Size = New System.Drawing.Size(127, 19)
            Me.CtrlLabel5.Location = New System.Drawing.Point(5, 109)
            Me.CtrlLabel5.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel5.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel5.Text = Me.CtrlLabel5.Text.ToUpper

            Me.CtrlLabeltxt5.Size = New System.Drawing.Size(92, 18)
            Me.CtrlLabeltxt5.Location = New System.Drawing.Point(132, 109)
            Me.CtrlLabeltxt5.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabeltxt5.BringToFront()
            Me.CtrlLabeltxt5.Text = Me.CtrlLabeltxt5.Text.ToUpper

            Me.CtrlLabel6.Size = New System.Drawing.Size(127, 19)
            Me.CtrlLabel6.Location = New System.Drawing.Point(5, 129)
            Me.CtrlLabel6.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel6.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel6.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel6.Text = Me.CtrlLabel6.Text.ToUpper


            Me.CtrlLabelTxt6.Size = New System.Drawing.Size(92, 19)
            Me.CtrlLabelTxt6.Location = New System.Drawing.Point(132, 128)
            Me.CtrlLabelTxt6.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabelTxt6.BringToFront()
            Me.CtrlLabelTxt6.Text = Me.CtrlLabelTxt6.Text.ToUpper

            Me.CtrlLabel7.Size = New System.Drawing.Size(127, 20)
            Me.CtrlLabel7.Location = New System.Drawing.Point(5, 149)
            Me.CtrlLabel7.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel7.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel7.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel7.Text = Me.CtrlLabel7.Text.ToUpper
            CtrlLabel7.Size = New Size(129, 18)

            Me.CtrlLabelTxt7.Size = New System.Drawing.Size(92, 19)
            Me.CtrlLabelTxt7.Location = New System.Drawing.Point(132, 148)
            Me.CtrlLabelTxt7.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabelTxt7.BringToFront()
            Me.CtrlLabelTxt7.Text = Me.CtrlLabelTxt7.Text.ToUpper

            Me.CtrlLabel010.Location = New System.Drawing.Point(5, 168)
            Me.CtrlLabel010.Font = New Font("NeoSans", 8, FontStyle.Bold)
            Me.CtrlLabel010.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel010.TextAlign = ContentAlignment.MiddleLeft
            Me.CtrlLabel010.Text = Me.CtrlLabel010.Text.ToUpper


            Me.CtrlLabelTxt10.Size = New System.Drawing.Size(92, 18)
            Me.CtrlLabelTxt10.Location = New System.Drawing.Point(132, 168)
            Me.CtrlLabelTxt10.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrlLabelTxt10.BringToFront()
            Me.CtrlLabelTxt10.Text = Me.CtrlLabelTxt10.Text.ToUpper

        ElseIf AlignChangeForCashSummary = "Tab Fast Cash Memo" Then
            ' Me.C1Sizer1.Grid.Clear()
            ' Me.Size = New System.Drawing.Size(208, 240)
            '   Me.Dock = DockStyle.Right
            C1Sizer1.SplitterWidth = 1
            C1Sizer1.Border.Color = Color.Transparent
            Me.BackColor = Color.FromArgb(49, 49, 49)
            ''sizer
            ' C1Sizer1.Size = New System.Drawing.Size(208, 240)
            ' C1Sizer1.Location = New System.Drawing.Point(5, 121)
            C1Sizer1.BackColor = Color.FromArgb(49, 49, 49)
            ''cashmemo summary label
            ' Me.CtrlHeader1.Size = New System.Drawing.Size(158, 21)
            ' Me.CtrlHeader1.Location = New System.Drawing.Point(5, 5)
            'Me.CtrlHeader1.HdrText = Me.CtrlHeader1.HdrText.ToUpper
            ' Me.CtrlHeader1.Font = New Font("Neo Sans", 14, FontStyle.Bold)


            ''grossamt lbl
            ' Me.CtrlLabel1.Size = New System.Drawing.Size(99, 22)
            ' Me.CtrlLabel1.Location = New System.Drawing.Point(4, 33)
            Me.CtrlLabel1.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel1.TextAlign = ContentAlignment.MiddleLeft
            ' Me.CtrlLabel1.Text = Me.CtrlLabel1.Text.ToUpper
            ''grosamt txt
            ' Me.CtrllblGrossAmt.Size = New System.Drawing.Size(130, 22)
            ' Me.CtrllblGrossAmt.Location = New System.Drawing.Point(102, 33)
            Me.CtrllblGrossAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblGrossAmt.BringToFront()
            '  Me.CtrllblGrossAmt.Text = Me.CtrllblGrossAmt.Text.ToUpper
            ''tax amt lbl
            '  Me.CtrlLabel2.Size = New System.Drawing.Size(99, 22)
            ' Me.CtrlLabel2.Location = New System.Drawing.Point(3, 58)
            Me.CtrlLabel2.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel2.TextAlign = ContentAlignment.MiddleLeft
            '  Me.CtrlLabel2.Text = Me.CtrlLabel2.Text.ToUpper

            ' Me.CtrllblTaxAmt.Size = New System.Drawing.Size(130, 22)
            ' Me.CtrllblTaxAmt.Location = New System.Drawing.Point(102, 58)
            Me.CtrllblTaxAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblTaxAmt.BringToFront()
            ' Me.CtrllblTaxAmt.Text = Me.CtrllblTaxAmt.Text.ToUpper

            ''disc lbl
            ' Me.CtrlLabel3.Size = New System.Drawing.Size(99, 22)
            ' Me.CtrlLabel3.Location = New System.Drawing.Point(4, 83)
            Me.CtrlLabel3.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel3.TextAlign = ContentAlignment.MiddleLeft
            ' Me.CtrlLabel3.Text = Me.CtrlLabel3.Text.ToUpper

            '  Me.CtrllblDiscAmt.Size = New System.Drawing.Size(130, 22)
            ' Me.CtrllblDiscAmt.Location = New System.Drawing.Point(102, 83)
            Me.CtrllblDiscAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblDiscAmt.BringToFront()
            ' Me.CtrllblDiscAmt.Text = Me.CtrllblDiscAmt.Text.ToUpper

            '' net amt lbl
            ' Me.CtrlLabel4.Size = New System.Drawing.Size(99, 22)
            'Me.CtrlLabel4.Location = New System.Drawing.Point(4, 108)
            Me.CtrlLabel4.Font = New Font("NeoSans", 9, FontStyle.Bold)
            Me.CtrlLabel4.BackColor = Color.FromArgb(212, 212, 212)
            Me.CtrlLabel4.TextAlign = ContentAlignment.MiddleLeft
            ' Me.CtrlLabel4.Text = Me.CtrlLabel4.Text.ToUpper

            '  Me.CtrllblNetAmt.Size = New System.Drawing.Size(130, 22)
            ' Me.CtrllblNetAmt.Location = New System.Drawing.Point(102, 108)
            Me.CtrllblNetAmt.BackColor = Color.FromArgb(255, 255, 255)
            Me.CtrllblNetAmt.BringToFront()
            ' Me.CtrllblNetAmt.Text = Me.CtrllblNetAmt.Text.ToUpper

        End If
    End Function



#Region "Visible"

    Public Property lblVisible1() As Boolean
        Get
            Return CtrlLabel1.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel1.Visible = value
        End Set
    End Property

    Public Property lblVisible2() As Boolean
        Get
            Return CtrlLabel2.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel2.Visible = value
        End Set
    End Property

    Public Property lblVisible3() As Boolean
        Get
            Return CtrlLabel3.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel3.Visible = value
        End Set
    End Property

    Public Property lblVisible4() As Boolean
        Get
            Return CtrlLabel4.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel4.Visible = value
        End Set
    End Property
    Public Property lblVisible5() As Boolean
        Get
            Return CtrlLabel5.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel5.Visible = value
        End Set
    End Property

    Public Property lblVisible6() As Boolean
        Get
            Return CtrlLabel6.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel6.Visible = value
        End Set
    End Property

    Public Property lblVisible7() As Boolean
        Get
            Return CtrlLabel7.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel7.Visible = value
        End Set
    End Property

    Public Property lblVisible8() As Boolean
        Get
            Return CtrlLabel8.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel8.Visible = value
        End Set
    End Property

    Public Property lblVisible9() As Boolean
        Get
            Return CtrlLabel09.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel09.Visible = value
        End Set
    End Property

    Public Property lblVisible10() As Boolean
        Get
            Return CtrlLabel010.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabel010.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible1() As Boolean
        Get
            Return CtrllblGrossAmt.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrllblGrossAmt.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible2() As Boolean
        Get
            Return CtrllblTaxAmt.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrllblTaxAmt.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible3() As Boolean
        Get
            Return CtrllblDiscAmt.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrllblDiscAmt.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible4() As Boolean
        Get
            Return CtrllblNetAmt.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrllblNetAmt.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible5() As Boolean
        Get
            Return CtrlLabeltxt5.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabeltxt5.Visible = value
        End Set
    End Property
    Public Property lbltxtVisible6() As Boolean
        Get
            Return CtrlLabelTxt6.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabelTxt6.Visible = value
        End Set
    End Property
    Public Property lbltxtVisible7() As Boolean
        Get
            Return CtrlLabelTxt7.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabelTxt7.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible8() As Boolean
        Get
            Return CtrlLabelTxt8.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabelTxt8.Visible = value
        End Set
    End Property

    Public Property lbltxtVisible9() As Boolean
        Get
            Return CtrlLabelTxt9.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabelTxt9.Visible = value
        End Set
    End Property
    Public Property lbltxtVisible10() As Boolean
        Get
            Return CtrlLabelTxt10.Visible
        End Get
        Set(ByVal value As Boolean)
            CtrlLabelTxt10.Visible = value
        End Set
    End Property


#End Region

    Public Property RowCount() As Integer
        Get
            Return C1Sizer1.Grid.Rows.Count
        End Get
        Set(ByVal value As Integer)
            C1Sizer1.Grid.Rows.Count = value

        End Set
    End Property

    'Private Sub CtrlCashSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Dim i As Int16
    '    For i = 0 To C1Sizer1.Grid.Rows.Count
    '        If Me.RowCount < i Then
    '            C1Sizer1.Grid.Rows(i).Size = 0
    '            C1Sizer1.Grid.Rows.Remove(i)
    '        End If
    '    Next
    'End Sub
    Private Sub CtrlCashSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
