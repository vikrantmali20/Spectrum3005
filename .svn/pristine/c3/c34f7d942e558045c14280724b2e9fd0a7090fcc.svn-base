Imports System
Imports System.Text
Imports System.Security.Cryptography

Public Class clsEncrypter

    Private clsEncrypter()

    Public Function getEncryptedPassword(ByVal password As String) As String

        Try
            Dim salt As Byte() = getSalt(Nothing)


            Dim data As Byte() = GetEncryptedData(salt, password)

            ' Create a new Stringbuilder to collect the bytes
            ' and create a string.
            Dim sBuilder As New StringBuilder()

            For i As Integer = 0 To salt.Length - 1
                sBuilder.Append(CInt(salt(i)) & ",")
            Next

            sBuilder.Append(":-:")

            ' Loop through each byte of the hashed data 
            ' and format each one as a hexadecimal string.
            For i As Integer = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next

            ' Return the hexadecimal string.
            Return sBuilder.ToString()
        Catch Ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function authenticatePassword(ByVal password As [String], ByVal encryptedPasswordFromDB As [String]) As Boolean
        Try

            Dim NwEncryptedPassword As String = encryptedPasswordFromDB.Substring(encryptedPasswordFromDB.LastIndexOf(":-:") + 3)

            Dim salt As Byte() = getSalt(encryptedPasswordFromDB)
            Dim data As Byte() = GetEncryptedData(salt, password)

            ' Create a new Stringbuilder to collect the bytes and create a string.
            Dim sBuilder As New StringBuilder()

            ' Loop through each byte of the hashed data and format each one as a hexadecimal string.
            For i As Integer = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next

            Return NwEncryptedPassword.Equals(sBuilder.ToString())
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetEncryptedData(ByVal salt As Byte(), ByVal PlainPassword As String) As Byte()
        Dim PlainTextBytes As Byte() = Encoding.UTF8.GetBytes(PlainPassword)
        Dim plainTextWithSaltBytes As Byte() = New Byte(salt.Length + (PlainTextBytes.Length - 1)) {}
        For i As Integer = 0 To salt.Length - 1
            plainTextWithSaltBytes(i) = salt(i)
        Next
        For i As Integer = 0 To PlainTextBytes.Length - 1
            plainTextWithSaltBytes(salt.Length + i) = PlainTextBytes(i)
        Next

        ' Create a new instance of the MD5CryptoServiceProvider object.
        Dim md5Hasher As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        'byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        Dim data As Byte() = md5Hasher.ComputeHash(plainTextWithSaltBytes)

        Return data
    End Function

    Private Function getSalt(ByVal encriptedPassword As String) As Byte()
        Dim salt As Byte() = New Byte(11) {}

        If String.IsNullOrEmpty(encriptedPassword) Then
            Dim random As New RNGCryptoServiceProvider()
            salt = New Byte(11) {}
            random.GetNonZeroBytes(salt)
        Else
            Dim NwEncryptedPassword As String() = encriptedPassword.Split(New String() {":-:"}, StringSplitOptions.RemoveEmptyEntries)
            Dim SaltArry As String() = NwEncryptedPassword(0).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To SaltArry.Length - 1
                If (Convert.ToInt32(SaltArry(i)) > 0) Then
                    salt(i) = CByte(Convert.ToInt32(SaltArry(i)))
                Else
                    salt(i) = BitConverter.GetBytes(Convert.ToInt32(SaltArry(i)))(0)
                End If
            Next
        End If
        Return salt
    End Function

End Class
