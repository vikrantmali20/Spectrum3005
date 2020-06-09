Public Class CtrlPromo
    Public _id As String = ""
    Public Property id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property
    Public _name As String = ""
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public _points As String = ""
    Public Property points() As String
        Get
            Return _points
        End Get
        Set(ByVal value As String)
            _points = value
        End Set
    End Property
    Public _value As String = ""
    Public Property value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property
    Public _category As String = ""
    Public Property category() As String
        Get
            Return _category
        End Get
        Set(ByVal value As String)
            _category = value
        End Set
    End Property
    Public _pos_item_id As String = ""
    Public Property pos_item_id() As String
        Get
            Return _pos_item_id
        End Get
        Set(ByVal value As String)
            _pos_item_id = value
        End Set
    End Property
    Public _is_item_off As String = ""
    Public Property is_item_off() As String
        Get
            Return _is_item_off
        End Get
        Set(ByVal value As String)
            _is_item_off = value
        End Set
    End Property
    Public _pos_category_id As String = ""
    Public Property pos_category_id() As String
        Get
            Return _pos_category_id
        End Get
        Set(ByVal value As String)
            _pos_category_id = value
        End Set
    End Property
    Public _is_category_off As String = ""
    Public Property is_category_off() As String
        Get
            Return _is_category_off
        End Get
        Set(ByVal value As String)
            _is_category_off = value
        End Set
    End Property
    Public _HashtagRewardId As String = ""
    Public Property HashtagRewardId As String
        Get
            Return _HashtagRewardId
        End Get
        Set(ByVal value As String)
            _HashtagRewardId = value
        End Set
    End Property

    Private Sub CtrlPromovb_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function Themechange() As String
        '  Me.ctrlbtnApply.BackColor = Color.FromArgb(0, 107, 163)
        '  Me.ctrlbtnApply.ForeColor = Color.FromArgb(255, 255, 255)
        Me.ctrlbtnApply.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.ctrlbtnApply.BringToFront()
        Me.ctrlbtnApply.Image = Nothing
        Me.ctrlbtnApply.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ctrlbtnApply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        ' Me.ctrlbtnApply.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ' Me.ctrlbtnApply.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.ctrlbtnApply.FlatStyle = FlatStyle.Flat
        Me.ctrlbtnApply.FlatAppearance.BorderSize = 1
    End Function
     


End Class
