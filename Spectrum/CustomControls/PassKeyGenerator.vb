Imports SpectrumCommon
Imports SpectrumBL
Public Class PassKeyGenerator


    Private Customer As CLPCustomerDTO
    Public PassKey As PassKey
    Private CLPDS As DataSet
    Public Sub New(ByVal clpcustomer As CLPCustomerDTO, ByVal ds As DataSet)
        Customer = clpcustomer
        CLPDS = ds
        InitializeComponent()
    End Sub
    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click
        Dim points = 0.0
        Dim DayredemptionLimit = 0.0
        Dim IsDayRedemptionLimit As Boolean = False
        If Not String.IsNullOrEmpty(txtPointstoredeem.Text) AndAlso CDec(txtPointstoredeem.Text) <= Customer.BalancePoints Then
            Double.TryParse(txtPointstoredeem.Text, points)
            If points > 0 Then
                If points <= Customer.BalancePoints Then
                    CLP_Data.Sitecode = clsAdmin.SiteCode
                    Dim DayRedemptionpoints = CLP_Data.GetdayRedemptionValue(Customer.CardNumber, clsAdmin.CurrentDate)
                    If CLPDS IsNot Nothing AndAlso CLPDS.Tables.Contains("CLPHeader") Then
                        If CLPDS.Tables("CLPHeader").Rows.Count > 0 Then
                            If CLPDS.Tables("CLPHeader").Rows(0)("IsDayLimtOnRedemption") Then
                                IsDayRedemptionLimit = CLPDS.Tables("CLPHeader").Rows(0)("IsDayLimtOnRedemption")
                                DayredemptionLimit = CLPDS.Tables("CLPHeader").Rows(0)("ValueDayLimtRedemption")
                            End If
                        End If

                    End If
                    If IsDayRedemptionLimit = False Or (IsDayRedemptionLimit AndAlso (points <= DayredemptionLimit - DayRedemptionpoints)) Then

                        Dim MinpointsforRed As Boolean = False
                        Dim MinpointsforRedVal As Double = 0.0
                        If CLPDS IsNot Nothing AndAlso CLPDS.Tables.Contains("CLPHeader") Then
                            If CLPDS.Tables("CLPHeader").Rows.Count > 0 Then
                                If CLPDS.Tables("CLPHeader").Rows(0)("IsMinPointsForRedemption") Then
                                    MinpointsforRed = CLPDS.Tables("CLPHeader").Rows(0)("IsDayLimtOnRedemption")
                                    MinpointsforRedVal = CLPDS.Tables("CLPHeader").Rows(0)("ValueMinPointsForRedemption")
                                End If
                            End If
                        End If

                        If MinpointsforRed = False Or (MinpointsforRed AndAlso Customer.BalancePoints >= MinpointsforRedVal) Then
                            'Dim passkeyn As String = clsAdmin.SiteCode.Substring(0, 3).ToString() & New Random(100).Next(999).ToString() & DateTime.Now.TimeOfDay.Hours & DateTime.Now.TimeOfDay.Minutes & DateTime.Now.TimeOfDay.Seconds
                            'C.	passkey field is a random number of n digit . n value will pick up from default Config .In case n value is greater than 15 consider its value 15 .
                            Dim strNoOfDigit As Integer = CLP_Data.GetPassKeyDigits()
                            'clsDefaultConfiguration.changes by mahesh because it return same number again n again

                            'Dim passkeyn As String = New Random(100).Next(Val("1".PadRight(strNoOfDigit + 1, "0")) - 1)
                            'Dim IMaxValue As Int64 = Val("1".PadRight(strNoOfDigit + 1, "0") - 1)
                            'Dim IMinValue As Int64 = Val("1".PadRight(strNoOfDigit, "0"))
                            'Dim passkeyn As String = GenRandomInt64(IMinValue, IMaxValue).ToString()

                            Dim myRandom As New Random
                            Dim passkeyn As String = GetRandomLong(strNoOfDigit, myRandom)

                            PassKey = New PassKey With {.CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode, .IsRedeemed = False, .Passkey = passkeyn, .PasskeyValue = txtPointstoredeem.Text, .SiteCode = clsAdmin.SiteCode, .Status = True, .UpdatedAt = clsAdmin.SiteCode, .UpdatedBy = clsAdmin.UserCode, .DocumentDate = clsAdmin.DayOpenDate}
                            ' Dim clpsms As Spectrum.SMSService.ClpCustomer
                            'clpsms.sendSMS( New Spectrum.SMSService.sendSMSRequest() With{ .sendSMS= new Spectrum.SMSService.sendSMS() With{.arg0= New Spectrum.SMSService.clpCustomerDTO() with    { .}} })
                            DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                        Else
                            MessageBox.Show(String.Format(getValueByKey("LOY014"), MinpointsforRedVal))
                        End If

                    Else
                        If DayredemptionLimit - DayRedemptionpoints <= 0 Then
                            MessageBox.Show(getValueByKey("LOY018"))
                        Else
                            MessageBox.Show(String.Format(getValueByKey("LOY015"), DayredemptionLimit))
                        End If

                    End If

                Else
                    MessageBox.Show(String.Format(getValueByKey("LOY016"), Customer.BalancePoints))
                End If

            Else
                MessageBox.Show(getValueByKey("LOY017"))
            End If
        Else
            MessageBox.Show(getValueByKey("LOY016"))
        End If
    End Sub

    '' <summary>
    ''' Generates a random Integer with any (inclusive) minimum or (inclusive) maximum values, with full range of Int32 values.
    ''' </summary>
    ''' <param name="inMin">Inclusive Minimum value. Lowest possible return value.</param>
    ''' <param name="inMax">Inclusive Maximum value. Highest possible return value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenRandomInt64(inMin As Int64, inMax As Int64) As Int64
        Static staticRandomGenerator As New System.Random
        If inMin > inMax Then Dim t = inMin : inMin = inMax : inMax = t
        If inMax < Int32.MaxValue Then Return staticRandomGenerator.Next(inMin, inMax + 1)
        ' now max = Int32.MaxValue, so we need to work around Microsoft's quirk of an exclusive max parameter.
        If inMin > Int32.MinValue Then Return staticRandomGenerator.Next(inMin - 1, inMax) + 1 ' okay, this was the easy one.
        ' now min and max give full range of integer, but Random.Next() does not give us an option for the full range of integer.
        ' so we need to use Random.NextBytes() to give us 4 random bytes, then convert that to our random int.
        Dim bytes(3) As Byte ' 4 bytes, 0 to 3
        staticRandomGenerator.NextBytes(bytes) ' 4 random bytes
        Return BitConverter.ToInt64(bytes, 0) ' return bytes converted to a random Int32
    End Function

    Public Function GetRandomLong(NumberLength As Integer, ByVal RandomObject As Random) As String

        'STRINGBUILDER TO HOLD OUR PSEUDO LONG
        Dim myLongString As New System.Text.StringBuilder

        'GRAB A RANDOM NUMBER BETWEEN 1 AND 9 FOR THE FIRST DIGIT
        myLongString.Append(RandomObject.Next(1, 10))

        'GRAB A RANDOM NUMBER BETWEEN 0 AND 9 FOR THE FINAL 15 DIGITS
        For i As Integer = 1 To NumberLength - 1
            myLongString.Append(RandomObject.Next(0, 10))
        Next
        'RETURN A LONG
        Return myLongString.ToString
    End Function

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        If Customer IsNot Nothing Then
            lblAvailPointsVal.Text = Customer.BalancePoints
        End If
        MyBase.OnLoad(e)
    End Sub
End Class