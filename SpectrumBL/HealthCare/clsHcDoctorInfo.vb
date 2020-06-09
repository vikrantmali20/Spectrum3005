Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Windows.Forms

Public Class clsHcDoctorInfo

    Dim SqlQry As New StringBuilder     'SqlQry.Append("" & vbCrLf)
    Dim SqlTrans As SqlTransaction
    Dim Sqlda, Sqldau As SqlDataAdapter
    Dim SqlCmdb As SqlCommandBuilder
    Dim Sqlds As DataSet
    Dim Sqldr As DataRow
    Dim SqlDt As DataTable
    Dim PKey(1) As DataColumn

    Dim clsComn As New clsHcCommon
    Dim FilterQuery As String = String.Empty.ToLower

    Public Function GetDoctorsInfo(ByVal vDoctorType As String) As DataSet
        Try
            FilterQuery = String.Empty
            'FilterQuery = " Where DoctorType = 3 ; "

            SqlQry.Length = 0
            SqlQry.Append("Select EmployeeCode, dbo.FnGetDesc('DoctorType',DoctorType,'0000588') As DoctorType" & vbCrLf)
            SqlQry.Append(", EmployeeName, FirstName, LastName, dbo.FnGetDesc('Gender',Gender,'0000588') As Gender" & vbCrLf)
            SqlQry.Append(", dbo.FnGetDesc('BloodGroup',BloodGroup,'0000588') As BloodGroup" & vbCrLf)
            SqlQry.Append(", DateOfBirth, AddressLine1, AddressLine2" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('103',CityCode) As City" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('102',StateCode) As State" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('101',CountryCode) As Country, PinCode" & vbCrLf)
            SqlQry.Append(", dbo.FnGetDesc('Education',Education,'0000588') As Education" & vbCrLf)
            FilterQuery = "where DoctorType = 3 order by EmployeeName"
            SqlQry.Append(", Nationality, EmailId, Mobile from HcMstEmployee " & FilterQuery & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlCmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "HcMstEmployee"

            Return Sqlds

        Catch ex As Exception
            MessageBox.Show("GetDoctorsInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try

    End Function
End Class
