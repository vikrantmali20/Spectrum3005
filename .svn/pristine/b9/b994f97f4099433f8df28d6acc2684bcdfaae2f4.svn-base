Imports System.Text
Imports System.Security.Cryptography
Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management

Public Class ClsLicense
    Implements ILicense

    Dim buffer As Byte() = New Byte() {&HF2, &HA1, 3, &H9D, &H63, &H87, &H35, &H5E}
    Dim buffer2 As Byte() = New Byte() {&HAB, &HB8, &H94, &H7E, &H1D, &HE5, &HD1, &H33}
    Public Function FetchLicenseFromDB(ByVal hardwareKey As String) As LicenseModel Implements ILicense.FetchLicenseFromDB
        Try
            Dim License As New LicenseModel
            Dim cmd As New SqlCommand("SELECT * FROM SpectrumLicense WHERE HardwareKey='" & hardwareKey & "' ", SpectrumCon())
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim rdr = cmd.ExecuteReader()

            While rdr.Read()
                License = New LicenseModel With {
                    .LicenseID = rdr("LicenseID"),
                    .SiteCode = rdr("SiteCode").ToString(),
                    .TerminalID = rdr("TerminalID").ToString(),
                    .ActivationDate = Convert.ToDateTime(rdr("ActivationDate").ToString()),
                    .VersionType = CType(Convert.ToInt32(rdr("Version").ToString()), VersionType),
                    .HardwareKey = rdr("HardwareKey").ToString(),
                    .LicenseKey = rdr("ValidationKey").ToString(),
                    .Status = Convert.ToBoolean(rdr("Status").ToString()),
                    .CreatedAt = rdr("CreatedAt").ToString(),
                    .CreatedBy = rdr("CreatedBy").ToString(),
                    .CreatedOn = Convert.ToDateTime(rdr("CreatedOn").ToString()),
                    .UpdatedAt = rdr("UpdatedAt").ToString(),
                    .UpdatedBy = rdr("UpdatedBy").ToString(),
                .UpdatedOn = Convert.ToDateTime(rdr("UpdatedOn").ToString())
                }
            End While

            rdr.Close()
            Return License
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function FetchLicenseFromDB(ByVal Sitecode As String, ByVal HDDKey As String, ByVal TerminaliId As String) As LicenseModel Implements ILicense.FetchLicenseFromDB
        Try
            Dim License As LicenseModel
            ' Dim cmd As New SqlCommand("SELECT * FROM SpectrumLicense WHERE SiteCode='" & Sitecode & "' and TerminalID='" & TerminalID & "' ", SpectrumCon())
            Dim cmd As New SqlCommand("SELECT * FROM SpectrumLicense WHERE SiteCode='" & Sitecode & "' and HardwareKey='" & HDDKey & "' and TerminalID='" & TerminaliId & "'", SpectrumCon())
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim rdr = cmd.ExecuteReader()

            While rdr.Read()
                License = New LicenseModel With {
                    .LicenseID = rdr("LicenseID"),
                    .SiteCode = rdr("SiteCode").ToString(),
                    .TerminalID = rdr("TerminalID").ToString(),
                    .ActivationDate = Convert.ToDateTime(rdr("ActivationDate").ToString()),
                    .VersionType = CType(Convert.ToInt32(rdr("Version").ToString()), VersionType),
                    .HardwareKey = rdr("HardwareKey").ToString(),
                    .LicenseKey = rdr("ValidationKey").ToString(),
                    .Status = Convert.ToBoolean(rdr("Status").ToString()),
                    .CreatedAt = rdr("CreatedAt").ToString(),
                    .CreatedBy = rdr("CreatedBy").ToString(),
                    .CreatedOn = Convert.ToDateTime(rdr("CreatedOn").ToString()),
                    .UpdatedAt = rdr("UpdatedAt").ToString(),
                    .UpdatedBy = rdr("UpdatedBy").ToString(),
                    .UpdatedOn = Convert.ToDateTime(rdr("UpdatedOn").ToString())
                }
            End While

            rdr.Close()
            Return License
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function SaveLicenseinDB(ByVal license As LicenseModel) As Boolean Implements ILicense.SaveLicenseinDB
        Try
            If license.LicenseID Is Nothing OrElse license.LicenseID = 0 Then

                Dim sqlQuery As New StringBuilder()
                sqlQuery.Append("INSERT INTO SpectrumLicense (SiteCode, TerminalID, ActivationDate, Version, HardwareKey, ValidationKey, " & vbCrLf)
                sqlQuery.Append("CreatedAt, CreatedBy, CreatedOn, UpdatedAt, UpdatedBy, UpdatedOn, Status) " & vbCrLf)
                sqlQuery.Append("VALUES('" & license.SiteCode & "','" & license.TerminalID & "',Getdate()," & DirectCast(license.VersionType, Integer) & ", " & vbCrLf)
                sqlQuery.Append("'" & license.HardwareKey & "', " & vbCrLf)
                sqlQuery.Append("'" & license.LicenseKey & "', " & vbCrLf)
                sqlQuery.Append("'" & license.SiteCode & "','" & license.CreatedBy & "',Getdate(), ")
                sqlQuery.Append("'" & license.SiteCode & "','" & license.UpdatedBy & "',Getdate(),'" & license.Status & "') " & vbCrLf)

                '"INSERT INTO [SpectrumLicense]([SiteCode],[TerminalID],[ValidationKey],[Status],[Version],[ActivationDate])VALUES('" & license.SiteCode & "','" & license.TerminalID & "','" & license.LicenseKey & "', '" & license.Status & "','" & DirectCast(license.VersionType, Integer) & "',getdate())"
                Dim cmd As New SqlCommand(sqlQuery.ToString(), SpectrumCon())
                If Not SpectrumCon().State = ConnectionState.Open Then
                    SpectrumCon().Open()
                End If
                If cmd.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Dim cmd As New SqlCommand("Update SpectrumLicense SET  ValidationKey='" & license.LicenseKey & "', ActivationDate = Getdate(), Version = '" & DirectCast(license.VersionType, Integer) & "' WHERE SiteCode = '" & license.SiteCode & "' AND TerminalID = '" & license.TerminalID & "'", SpectrumCon())
                If cmd.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateLicenseInDB(ByVal license As LicenseModel) As Boolean Implements ILicense.UpdateLicenseInDB
        Try
            Dim sqlQuery As New StringBuilder()

            Dim licenseKey As String = String.Empty

            Dim decryLicenseKey = DecryptKey(license.LicenseKey)
            Dim KeyValue = GetdatafromKey(decryLicenseKey)

            Dim expireLicenseKey = DateTime.Now.AddDays(-1).ToString("ddMMyyyy") + "*" + KeyValue.Split("*").Last()
            Dim expireLicenseKeyByte As Byte() = Encoding.UTF8.GetBytes(expireLicenseKey)

            Dim keyProvider As New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}

            Dim EncProductInfo As Byte() = keyProvider.CreateEncryptor().TransformFinalBlock(expireLicenseKeyByte, 0, expireLicenseKeyByte.Length)
            licenseKey = ToHex(EncProductInfo)

            Dim encryLicenseKey = EncryptKey(licenseKey)

            sqlQuery.Append("DELETE FROM SpectrumLicense " & vbCrLf)
            sqlQuery.Append("WHERE SiteCode = '" & license.SiteCode & "' AND TerminalID = '" & license.TerminalID & "' " & vbCrLf)

            'sqlQuery.Append("Update SpectrumLicense SET Status = '0', ValidationKey = '" & encryLicenseKey & "', " & vbCrLf)
            'sqlQuery.Append("UpdatedAt = '" & license.UpdatedAt & "', UpdatedBy = '" & license.UpdatedBy & "', UpdatedOn = GETDATE() " & vbCrLf)
            'sqlQuery.Append("WHERE SiteCode = '" & license.SiteCode & "' AND TerminalID = '" & license.TerminalID & "' " & vbCrLf)

            Dim cmd As New SqlCommand(sqlQuery.ToString(), SpectrumCon())
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function DecryptKey(EncryptedKey As String) As String Implements ILicense.DecryptKey
        Dim DecryptedText = ""
        Dim DescProvider = New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}
        Dim Info = Convert.FromBase64String(EncryptedKey)
        Dim DecryptedArray = DescProvider.CreateDecryptor.TransformFinalBlock(Info, 0, Info.Length)
        DecryptedText = Encoding.UTF8.GetString(DecryptedArray)
        Return DecryptedText
    End Function

    Private Function EncryptKey(DecryptedKey As String) As String Implements ILicense.EncryptKey
        Dim EncryptedText = ""
        Dim Info = Encoding.UTF8.GetBytes(DecryptedKey)
        Dim DescProvider = New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}
        Dim EncryptedArray = DescProvider.CreateEncryptor().TransformFinalBlock(Info, 0, Info.Length)
        EncryptedText = Convert.ToBase64String(EncryptedArray)
        Return EncryptedText
    End Function

    Private Function FromHex(HexString As String) As Byte() Implements ILicense.FromHex
        HexString = Strip(HexString, vbTab & vbCr & vbLf & " -")

        Dim HexBytes As Byte() = New Byte(HexString.Length / 2 - 1) {}
        Dim startindex As Integer = 0
        Dim i As Integer = 0
        While startindex < HexString.Length
            Dim tempstr As String = HexString.Substring(startindex, 2)
            HexBytes(i) = Byte.Parse(tempstr, NumberStyles.HexNumber, CultureInfo.InvariantCulture)
            startindex += 2
            i += 1
        End While
        Return HexBytes
    End Function

    Private Function GetdatafromKey(LicenseKey As String) As String Implements ILicense.GetdatafromKey
        Dim Producdectinf As Byte() = FromHex(LicenseKey)
        Dim DescProvider = New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}
        Dim DecProductInfo As Byte() = DescProvider.CreateDecryptor().TransformFinalBlock(Producdectinf, 0, Producdectinf.Length)
        Dim DecryptedString As String = Encoding.UTF8.GetString(DecProductInfo)
        Return DecryptedString
    End Function

    Public Function ValidateKey(LicenseKey As String) As Boolean Implements ILicense.ValidateKey
        Try
            Dim KeyValue = GetdatafromKey(LicenseKey)
            Dim Components As List(Of String) = KeyValue.Split("*").ToList()
            Dim HDDNO = identifier("Win32_DiskDrive", "SerialNumber").Trim()
            If Components(Components.Count - 1).ToUpper() = HDDNO.ToUpper() Then

                If DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture) >= DateTime.Now Then
                    'Write Here code for Validity Popup
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ValidateHardwareKey(LicenseKey As String) As Boolean Implements ILicense.ValidateHardwareKey
        Try
            Dim KeyValue = GetdatafromKey(LicenseKey)
            Dim Components As List(Of String) = KeyValue.Split("*").ToList()
            Dim HDDNO = identifier("Win32_DiskDrive", "SerialNumber").Trim()
            If Components(Components.Count - 1).ToUpper() = HDDNO.ToUpper() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Strip(value As String, characters As String) As String Implements ILicense.Strip
        If value Is Nothing Then
            Return Nothing
        End If
        Dim builder As New StringBuilder()
        For Each ch As Char In value
            If characters.IndexOf(ch, 0) < 0 Then
                builder.Append(ch)
            End If
        Next
        Return builder.ToString()
    End Function

    Private Function identifier(wmiClass As String, wmiProperty As String) As String Implements ILicense.identifier
        'Return a hardware identifier
        Dim result As String = ""
        Dim mc As New ManagementClass(wmiClass)
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        For Each mo As ManagementObject In moc
            'Only get the first one
            If result = "" Then
                Try
                    result = mo(wmiProperty).ToString()
                    Exit Try
                Catch
                End Try
            End If
        Next
        Return result
    End Function

    Public Function ValidateCurrentLicense(ByVal SiteCode As String, ByVal HDDKey As String, ByVal TerminaliId As String) As LicenseStatus Implements ILicense.ValidateCurrentLicense
        Dim CurrentLicense = FetchLicenseFromDB(SiteCode, HDDKey, TerminaliId)

        If CurrentLicense Is Nothing Then
            Return LicenseStatus.None
        Else
            Dim DecryptedKey = DecryptKey(CurrentLicense.LicenseKey)

            If (ValidateKey(DecryptedKey)) Then
                Return LicenseStatus.Valid
            Else
                If (Not ValidateHardwareKey(CurrentLicense.HardwareKey)) Then
                    Return LicenseStatus.InvalidHardwareKey
                Else
                    Return LicenseStatus.Invalid
                End If
            End If
        End If

    End Function

    Public Function InstallNewLicense(ByVal License As LicenseModel) As Boolean Implements ILicense.InstallNewLicense
        If License IsNot Nothing Then
            If ValidateKey(License.LicenseKey) Then
                License.LicenseKey = EncryptKey(License.LicenseKey)
                License.HardwareKey = GetEncryptedHDDKey()
                Return SaveLicenseinDB(License)
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Function CreateTrialLicense(ByVal SiteCode As String, ByVal TerminlID As String, ByVal userCode As String, Optional ByVal OffLineLicenseActivationDuration As Integer = 30) As Boolean Implements ILicense.CreateTrialLicense
        Dim HDDKey = identifier("Win32_DiskDrive", "SerialNumber").Trim()

        'Dim ProductInfo = DateTime.Now.AddDays(30).ToString("ddMMyyyy") + "*" + HDDKey
        'added by khusrao adil on 20-07-2018  for Dynamic License Duration setting
        Dim ProductInfo = DateTime.Now.AddDays(Convert.ToString(OffLineLicenseActivationDuration)).ToString("ddMMyyyy") + "*" + HDDKey
        Dim Productinf As Byte() = Encoding.UTF8.GetBytes(ProductInfo)
        Dim provider As New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}

        Dim EncProductInfo As Byte() = provider.CreateEncryptor().TransformFinalBlock(Productinf, 0, Productinf.Length)
        Dim LicenseKey As String = ToHex(EncProductInfo)

        Dim NewLicense As New LicenseModel With {
            .HardwareKey = GetEncryptedHDDKey(),
            .LicenseKey = LicenseKey,
            .SiteCode = SiteCode,
            .TerminalID = TerminlID,
            .Status = True,
            .VersionType = VersionType.Trial,
            .CreatedBy = userCode,
        .UpdatedBy = userCode
        }
        Return InstallNewLicense(NewLicense)
    End Function

    Public Function OnlineLicense(ByVal License As LicenseModel) As Boolean Implements ILicense.OnlineLicense
        If License IsNot Nothing Then
            License.VersionType = VersionType.Licensed
            Return InstallNewLicense(License)
        Else
            Return False
        End If

    End Function

    Private Function ToHex(data As Byte()) As String
        Dim builder As New StringBuilder()
        For i As Integer = 0 To data.Length - 1
            If (i > 0) AndAlso ((i Mod 2) = 0) Then
                builder.Append("-")
            End If
            builder.Append(data(i).ToString("X2", CultureInfo.InvariantCulture))
        Next
        Return builder.ToString()
    End Function

    Public Function GetEncryptedHDDKey() As String Implements ILicense.GetEncryptedHDDKey
        Dim HDDKey = identifier("Win32_DiskDrive", "SerialNumber").Trim()
        Dim Productinf As Byte() = Encoding.UTF8.GetBytes(HDDKey)
        Dim provider As New DESCryptoServiceProvider With {.IV = buffer2, .Key = buffer}
        Dim EncProductInfo As Byte() = provider.CreateEncryptor().TransformFinalBlock(Productinf, 0, Productinf.Length)
        Dim HDDHex As String = ToHex(EncProductInfo)
        Return HDDHex
    End Function

    Public Function GetDatetoExpire(ByVal Sitecode As String, ByVal HDDKey As String, ByVal TerminalID As String) As DateTime
        Dim CurrentLicense = FetchLicenseFromDB(Sitecode, HDDKey, TerminalID)
        If CurrentLicense Is Nothing Then
            Return DateTime.Now
        Else
            Dim DecryptedKey = DecryptKey(CurrentLicense.LicenseKey)
            Dim KeyValue = GetdatafromKey(DecryptedKey)
            Dim Components As List(Of String) = KeyValue.Split("*").ToList()
            If DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture) >= DateTime.Now Then
                'Write Here code for Validity Popup
                Return DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture)
            Else
                Return DateTime.Now
            End If
        End If
    End Function

    Public Function GetDaystoExpire(ByVal Sitecode As String, ByVal HDDKey As String, ByVal TerminalID As String) As Integer
        Dim CurrentLicense = FetchLicenseFromDB(Sitecode, HDDKey, TerminalID)
        If CurrentLicense Is Nothing Then
            Return 0
        Else
            Dim DecryptedKey = DecryptKey(CurrentLicense.LicenseKey)
            Dim KeyValue = GetdatafromKey(DecryptedKey)
            Dim Components As List(Of String) = KeyValue.Split("*").ToList()
            If CurrentLicense.ActivationDate > DateTime.Now Then

                Return DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture).Subtract(CurrentLicense.ActivationDate).TotalDays

            ElseIf DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture) >= DateTime.Now Then
                'Write Here code for Validity Popup
                Return DateTime.ParseExact(Components(Components.Count - 2), "ddMMyyyy", CultureInfo.InvariantCulture).Subtract(DateAdd(DateInterval.Day, -1, DateTime.Now)).TotalDays
            Else
                Return 0
            End If
        End If
    End Function

    Public Function GetSupportEmailID(ByVal Sitecode As String) As String
        Dim supportEmailID = FetchSupportEmailIDFromDB(Sitecode)
        If supportEmailID Is Nothing Or String.IsNullOrEmpty(supportEmailID) Then
            Return ""
        Else
            Return supportEmailID
        End If
    End Function

    Public Function GetSupportPhoneNo(ByVal Sitecode As String) As String
        Dim supportPhoneNo = FetchSupportPhoneFromDB(Sitecode)
        If supportPhoneNo Is Nothing Or String.IsNullOrEmpty(supportPhoneNo) Then
            Return ""
        Else
            Return supportPhoneNo
        End If
    End Function

    Public Function FetchSupportEmailIDFromDB(ByVal Sitecode As String) As String Implements ILicense.FetchSupportEmailIDFromDB
         Try
            Dim SupportEmailId As String = String.Empty
            Dim cmd As New SqlCommand("SELECT * FROM DefaultConfig WHERE SiteCode='" & Sitecode & "' and Upper(FldLabel)='LICENCESUPPORTEMAILID' ", SpectrumCon())
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim rdr = cmd.ExecuteReader()
            While rdr.Read()
                If Not IsDBNull(rdr("FldValue")) Then
                    SupportEmailId = rdr("FldValue")
                End If
            End While
            rdr.Close()
            Return SupportEmailId
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function FetchSupportPhoneFromDB(ByVal Sitecode As String) As String Implements ILicense.FetchSupportPhoneFromDB
        Try
            Dim SupportPhoneNo As String = String.Empty
            Dim cmd As New SqlCommand("SELECT * FROM DefaultConfig WHERE SiteCode='" & Sitecode & "' and Upper(FldLabel)='LICENCESUPPORTPHONENO' ", SpectrumCon())
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim rdr = cmd.ExecuteReader()
            While rdr.Read()
                If Not IsDBNull(rdr("FldValue")) Then
                    SupportPhoneNo = rdr("FldValue")
                End If
            End While
            rdr.Close()
            Return SupportPhoneNo
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function InstallBackDatedLicense(ByVal License As LicenseModel) As Boolean Implements ILicense.InstallBackDatedLicense
        If License IsNot Nothing Then
            If ValidateKey(License.LicenseKey) Then
                License.LicenseKey = EncryptKey(License.LicenseKey)
                License.HardwareKey = GetEncryptedHDDKey()
                Return SaveBackDatedLicenseinDB(License)
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Private Function SaveBackDatedLicenseinDB(ByVal license As LicenseModel) As Boolean Implements ILicense.SaveBackDatedLicenseinDB
        Try
            '  If license.LicenseID Is Nothing OrElse license.LicenseID = 0 Then

            Dim sqlQuery As New StringBuilder()
            sqlQuery.Append("Delete from SpectrumLicense where HardwareKey= '" & license.HardwareKey & "';" & vbCrLf)
            sqlQuery.Append("INSERT INTO SpectrumLicense (SiteCode, TerminalID, ActivationDate, Version, HardwareKey, ValidationKey, " & vbCrLf)
            sqlQuery.Append("CreatedAt, CreatedBy, CreatedOn, UpdatedAt, UpdatedBy, UpdatedOn, Status) " & vbCrLf)
            sqlQuery.Append("VALUES('" & license.SiteCode & "','" & license.TerminalID & "',@ActivationDate," & DirectCast(license.VersionType, Integer) & ", " & vbCrLf)
            sqlQuery.Append("'" & license.HardwareKey & "', " & vbCrLf)
            sqlQuery.Append("'" & license.LicenseKey & "', " & vbCrLf)
            sqlQuery.Append("'" & license.SiteCode & "','" & license.CreatedBy & "',Getdate(), ")
            sqlQuery.Append("'" & license.SiteCode & "','" & license.UpdatedBy & "',Getdate(),'" & license.Status & "') " & vbCrLf)



            '"INSERT INTO [SpectrumLicense]([SiteCode],[TerminalID],[ValidationKey],[Status],[Version],[ActivationDate])VALUES('" & license.SiteCode & "','" & license.TerminalID & "','" & license.LicenseKey & "', '" & license.Status & "','" & DirectCast(license.VersionType, Integer) & "',getdate())"
            Dim cmd As New SqlCommand(sqlQuery.ToString(), SpectrumCon())
            cmd.Parameters.Add("@ActivationDate", SqlDbType.DateTime)
            cmd.Parameters("@ActivationDate").Value = license.ActivationDate
            If Not SpectrumCon().State = ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            'Else
            'Dim cmd As New SqlCommand("Update SpectrumLicense SET  ValidationKey='" & license.LicenseKey & "', ActivationDate = Getdate(), Version = '" & DirectCast(license.VersionType, Integer) & "' WHERE SiteCode = '" & license.SiteCode & "' AND TerminalID = '" & license.TerminalID & "'", SpectrumCon())
            'If cmd.ExecuteNonQuery() > 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
            'End If


        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

End Class
