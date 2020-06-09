Module ModBLCommon

    Public Function Encrypt(ByVal sPasswordEnc As String) As String
        ''This Function gets the string as input and returns the encrypted value of the string
        Dim AscEncrypt As String
        Dim nCount As Integer
        Dim sChar As String
        Dim strEncrypt As String = ""
        For nCount = 1 To Len(sPasswordEnc)
            sChar = Mid(sPasswordEnc, nCount, 1)
            AscEncrypt = Chr(Asc(sChar) - Asc(30))
            strEncrypt = strEncrypt & AscEncrypt
        Next
        Return strEncrypt
    End Function

    Public Function Decrypt(ByVal sPasswordDec As String) As String
        ''This Function gets the encrypted string as input and returns the decrypted value of the string
        Dim AscDecrypt As String
        Dim nCount As Integer
        Dim sChar As String
        Dim strDecrypt As String = ""
        sPasswordDec = Trim(sPasswordDec)
        For nCount = 1 To Len(sPasswordDec)
            sChar = Mid(sPasswordDec, nCount, 1)
            AscDecrypt = Chr(Asc(sChar) + Asc(30))
            strDecrypt = strDecrypt & AscDecrypt
        Next
        Return strDecrypt
    End Function

End Module
