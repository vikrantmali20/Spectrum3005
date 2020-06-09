Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' Used in Advance Search  
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsAdvanceSearch
    Inherits clsCommon
#Region "Global variable for Class"
    Dim daObject As SqlDataAdapter
#End Region
#Region "Public Method's & Function's"
    ''' <summary>
    ''' Get diffrent Objects
    ''' </summary>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Function GetObjects() As DataTable
        Try
            Dim dtObjects As New DataTable
            daObject = New SqlDataAdapter("SELECT OBJECT_ID as OBJECTSID,OBJECT_NAME as OBJECTSNAME,VIEWNAME AS VIEWNAMES FROM GLQUERYOBJECTM", ConString)
            daObject.Fill(dtObjects)
            dtObjects.TableName = "Objects"
            Return dtObjects
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get List of Attributes Based on Objects
    ''' </summary>
    ''' <param name="ObjectId">ObjectId</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetAttributeList(ByVal ObjectId As String) As DataTable
        Try
            Dim dtAttr As New DataTable
            daObject = New SqlDataAdapter("SELECT DISTINCT A.RELFIELD_NAME,A.ATTRIBUTEDATATYPE,B.ATTRIBUTE_NAME FROM GLQUERYOBJECTSATTRIBUTEC A " & _
                                            "INNER JOIN GLQUERYATTRIBUTEM B on A.ATTRIBUTE_ID=B.ATTRIBUTE_ID WHERE " & _
                                            "A.OBJECT_ID =" & ObjectId, ConString)
            daObject.Fill(dtAttr)
            Return dtAttr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Logical Operators
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetLogicalOperators() As DataTable
        Try
            Dim dtLoper As New DataTable
            daObject = New SqlDataAdapter("SELECT * FROM VW_LOGICALOPERATORS", ConString)
            daObject.Fill(dtLoper)
            Return dtLoper
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Operator's
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetOperators() As DataTable
        Try
            Dim dtOper As New DataTable
            daObject = New SqlDataAdapter("SELECT * FROM VW_OPERATORS", ConString)
            daObject.Fill(dtOper)
            Return dtOper
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Structure of Where,for Advance search
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetWhereStru() As DataTable
        Try
            Dim dt As New DataTable("Where")
            AddColumnToDataTable(dt, "Loper", "System.String")
            AddColumnToDataTable(dt, "Attribute", "System.String")
            AddColumnToDataTable(dt, "AttributeCode", "System.String")
            AddColumnToDataTable(dt, "Oper", "System.String")
            AddColumnToDataTable(dt, "OperCode", "System.String")
            AddColumnToDataTable(dt, "Value1", "System.String")
            AddColumnToDataTable(dt, "Value2", "System.String")
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get the Data from resulted Query
    ''' </summary>
    ''' <param name="StrQuery">Query</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetResultedData(ByVal StrQuery As String) As DataTable
        Try
            Dim dtOper As New DataTable
            daObject = New SqlDataAdapter(StrQuery, ConString)
            daObject.Fill(dtOper)
            Return dtOper
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
   
End Class
