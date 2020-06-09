Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Module ExtensionModule

    'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing parameter
    Private _PrintLineLength As Integer
    Public Property PrintLineLength() As Integer
        Get
            Return _PrintLineLength
        End Get
        Set(ByVal value As Integer)
            _PrintLineLength = value
        End Set
    End Property

    Private _TemplateName As String
    Public Property TemplateName() As String
        Get
            Return _TemplateName
        End Get
        Set(ByVal value As String)
            _TemplateName = value
        End Set
    End Property

    Private _TemplateXmlName As String
    Public Property TemplateXmlName() As String
        Get
            Return _TemplateXmlName
        End Get
        Set(ByVal value As String)
            _TemplateXmlName = value
        End Set
    End Property

    Private _DrawLineTextCode As Integer
    Public Property DrawLineTextCode() As Integer
        Get
            Return _DrawLineTextCode
        End Get
        Set(ByVal value As Integer)
            _DrawLineTextCode = value
        End Set
    End Property

    Private _ComboItemPrintingAllowed As Boolean
    Public Property ComboItemPrintingAllowed() As Boolean
        Get
            Return _ComboItemPrintingAllowed
        End Get
        Set(ByVal value As Boolean)
            _ComboItemPrintingAllowed = value
        End Set
    End Property

    <Extension()> _
    Public Function FormatText(ByVal stringTextValue As String, ByVal fieldLength As Integer, fieldAlignment As String, ByVal IsCropText As Boolean) As String

        If String.IsNullOrEmpty(stringTextValue) Then
            Return String.Empty
        End If

        Dim results As String = String.Empty

        If stringTextValue.Length > PrintLineLength Then
            stringTextValue = SplitStringText(stringTextValue, fieldLength).Trim()

        ElseIf stringTextValue.Length > fieldLength AndAlso IsCropText Then
            stringTextValue = stringTextValue.Substring(0, fieldLength)
        End If

        If (fieldAlignment.Equals("Left")) Then
            results = stringTextValue.PadRight(fieldLength)

        ElseIf (fieldAlignment.Equals("Right")) Then
            results = stringTextValue.PadLeft(fieldLength)

        ElseIf (fieldAlignment.Equals("Center")) Then
            results = String.Concat(Space(Convert.ToInt16((PrintLineLength - stringTextValue.Length) / 2)), stringTextValue)
        End If

        Return results
    End Function

    Public Function SplitStringText(ByVal stringValue As String, ByVal stringLength As Integer, Optional ByVal blankSpace As Integer = 0) As String
        Try
            Dim outputValue As New StringBuilder

            Dim currentLength As Integer = stringLength
            Dim IsAddedBlankSpace, IsAddedFirstLine As Boolean
            Dim currentString As String = stringValue
            Dim strLineValueOld As String = String.Empty, strLineValueNew As String = String.Empty

            If (stringValue.Trim().Length < stringLength) Then
                outputValue.Append(stringValue + vbCrLf)
            Else
                While (Not currentString.Trim().Length = 0)

                    For Each strValue As String In currentString.Split(Space(1))
                        strLineValueNew += strValue + Space(1)

                        If (strLineValueNew.Length <= currentLength) Then
                            strLineValueOld = strLineValueNew
                        Else
                            Exit For
                        End If
                    Next

                    outputValue.Append(IIf(IsAddedBlankSpace, Space(blankSpace) + strLineValueOld.TrimEnd() + vbCrLf, strLineValueOld.Trim + vbCrLf))
                    If (Not String.IsNullOrEmpty(outputValue.ToString().Trim())) Then
                        currentString = currentString.Replace(strLineValueOld.TrimEnd(), String.Empty)
                    Else
                        If (strLineValueNew.Length > currentLength) Then
                            Dim newString1 As String = String.Empty
                            Dim newString2 As String = String.Empty

                            For Each charValue As Char In strLineValueNew.ToArray()
                                If (newString1.Length < currentLength) Then
                                    newString1 += charValue
                                Else
                                    newString2 += newString1 + vbCrLf
                                    newString1 = String.Empty
                                End If
                            Next

                            strLineValueOld = newString2 + newString1

                            outputValue.Append(strLineValueOld + Space(1))
                            currentString = currentString.Replace(strLineValueNew.TrimEnd(), String.Empty)
                        End If
                    End If

                    strLineValueNew = String.Empty
                    strLineValueOld = String.Empty

                    If (Not IsAddedBlankSpace AndAlso blankSpace > 0) Then
                        IsAddedBlankSpace = True
                        currentLength = stringLength - blankSpace
                    End If

                    If (Not IsAddedFirstLine) Then
                        IsAddedFirstLine = True
                        currentLength = 40 - blankSpace
                    End If

                End While

            End If

            Dim output As String = outputValue.ToString().Substring(0, outputValue.ToString().Length - 1)

            outputValue = New StringBuilder
            outputValue.Append(output)

            Return outputValue.ToString

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return String.Empty
        End Try
    End Function

    'Rakesh:09-July-2013-->End: Template based cashmemo bill printing parameter


    <Extension()>
    Public Function ToBindingList(Of T)(ByRef collec As IEnumerable(Of T)) As BindingList(Of T)
        Dim bindingList As New BindingList(Of T)
        For Each Item In collec
            bindingList.Add(Item)
        Next
        Return bindingList
    End Function


    Public Function DeepClone(Of T)(obj As T) As T
        Dim objResult As T
        Using ms As New MemoryStream()
            Dim bf As New BinaryFormatter()
            bf.Serialize(ms, obj)
            ms.Position = 0
            objResult = DirectCast(bf.Deserialize(ms), T)
        End Using
        Return objResult
    End Function


    <Extension()>
    Public Function SetWeighingScaleBarcodeElementValues(ByVal Seqlist As List(Of Sequence), ByVal Barcode As String) As List(Of Sequence)
        Dim Processfromend = False
        Dim Str = Barcode
        While Seqlist.Where(Function(w) w.IsProcess = False).Count() > 0
            Dim CurrSequence As Sequence
            If Processfromend Then
                CurrSequence = Seqlist.Where(Function(w) w.IsProcess = False).LastOrDefault()
            Else
                CurrSequence = Seqlist.Where(Function(w) w.IsProcess = False).FirstOrDefault()
            End If


            If CurrSequence.Element = "EAN" Then
                If Seqlist.Where(Function(w) w.IsProcess = False).Count() = 1 Then
                    CurrSequence.SeqValue = Str
                    CurrSequence.IsProcess = True
                Else
                    Processfromend = True
                End If
            Else
                If Processfromend Then
                    Dim length = Str.Length
                    CurrSequence.SeqValue = Str.Substring(length - CurrSequence.SeqLength, CurrSequence.SeqLength)
                    Str = Str.Remove(length - CurrSequence.SeqLength, CurrSequence.SeqLength)
                    CurrSequence.IsProcess = True
                Else
                    CurrSequence.SeqValue = Str.Substring(0, CurrSequence.SeqLength)
                    Str = Str.Remove(0, CurrSequence.SeqLength)
                    CurrSequence.IsProcess = True
                End If

            End If

        End While
        Return Seqlist
    End Function

End Module
