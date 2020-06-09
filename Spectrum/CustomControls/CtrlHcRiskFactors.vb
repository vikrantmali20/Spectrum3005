Public Class CtrlHcRiskFactors

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        cboDurationIn.AddItem("Day(s)")
        cboDurationIn.AddItem("Month(s)")
        cboDurationIn.AddItem("Year(s)")

        cboDurationIn.SelectedIndex = 0
    End Sub


    Private _riskFactorName As String
    Public Property RiskFactorName() As String
        Get
            Return _riskFactorName
        End Get
        Set(ByVal value As String)
            _riskFactorName = value
        End Set
    End Property

    Private Sub CtrlHcRiskFactors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CheckBoxRiskFactors.Text = RiskFactorName
    End Sub

    Public Sub fnEnable(ByVal flag As Boolean)

        Me.CheckBoxRiskFactors.Enabled = flag
        Me.txtDurationIn.ReadOnly = (flag)
        Me.cboDurationIn.Enabled = flag

    End Sub

End Class
