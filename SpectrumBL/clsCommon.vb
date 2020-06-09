﻿Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Globalization
Imports System.Windows.Forms
Imports System.IO.Ports
Imports System.Web.Script.Serialization
Imports System.Security.Cryptography
Public Class clsCommon
    Dim SqlQry As New StringBuilder



    ''' <summary>
    ''' Get DefaultSeting Details
    ''' </summary>
    ''' <param name="strSiteCode">SiteCode</param>
    ''' <param name="Doctype">DocType</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' 
    Private _strLangCode As String = Nothing
    Private _DecimalDigits As Integer
    Public dt As New DataTable
    'Private _getDocumentNo As String
    Public Property DecimalDigits As Integer
        Get
            Return _DecimalDigits
        End Get
        Set(ByVal value As Integer)
            _DecimalDigits = value
        End Set
    End Property
    Public Property strLangCode As String
        Get
            Return _strLangCode
        End Get
        Set(ByVal value As String)
            _strLangCode = value
        End Set
    End Property
    Public Shared _ListOfNumber As Queue(Of Integer)
    Public Property ListOfNumber() As Queue(Of Integer)
        Get
            Return _ListOfNumber
        End Get
        Set(ByVal value As Queue(Of Integer))
            _ListOfNumber = value
        End Set
    End Property
    Public Shared _NextShiftId As Integer
    Public Property NextShiftId() As Integer
        Get
            Return _NextShiftId
        End Get
        Set(ByVal value As Integer)
            _NextShiftId = value
        End Set
    End Property

    Public Shared _PrevShiftId As Integer
    Public Property PrevShiftId() As Integer
        Get
            Return _PrevShiftId
        End Get
        Set(ByVal value As Integer)
            _PrevShiftId = value
        End Set
    End Property

    Public Sub DisplayHelpFile(ByVal formNAme As Windows.Forms.Form, ByVal IndexName As String)
        Try
            Dim CHMFilePath As String = System.IO.Path.Combine(Application.StartupPath, "Front-office_help.chm")
            'Help.ShowHelp()
            Help.ShowHelp(formNAme, CHMFilePath, HelpNavigator.Topic, IndexName)
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    'Property getDocumentNo(ByVal p1 As String) As String
    '    Get
    '        Return _getDocumentNo
    '    End Get
    '    Set(ByVal value As String)
    '        _getDocumentNo = value
    '    End Set
    'End Property
    Public Sub FirstBillQueryExecutes(ByVal SiteCode As String)

        Try

            Dim Query = "SELECT Top 1 A.ARTICLECODE,B.ARTICLECODE,B.EAN,B.QUANTITY,A.ARTICALTYPECODE FROM MSTARTICLE A LEFT JOIN MSTARTICLEKIT B ON A.ARTICLECODE=B.KITARTICLECODE AND B.STATUS=1 "
            Query = Query & " SELECT * FROM CMPRINT ((select top 1 BillNo from CashMemoHdr where SiteCode='" & SiteCode & "' ) ,'" & SiteCode & "','EN' )"
            Query = Query & " SELECT TOPBOTTOM,SEQUENCENO,RECEIPTTEXT,ALIGN,WIDTH,HEIGHT,BOLD FROM PRINTINGDETAIL WHERE DOCUMENTTYPE='CMInvc' or  DOCUMENTTYPE='ALLDocs' "
            Query = Query & " SELECT Top 1 BILLLINENO,TaxLineNo,TaxLabel,TaxValue,CREATEDAT,CREATEDBY,CREATEDON,STATUS,UPDATEDAT,UPDATEDBY,UPDATEDON,SITECODE,BILLNO FROM CASHMEMOTAXDTLS WHERE 1=0 "
            Query = Query & " SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='CM'"
            Query = Query & " SELECT top 1  A.BillNO,   a.ComboArticleCode , a.ArticleCode  ,b.ArticleName , a.Quantity FROM    CASHMEMOCOMBODTL as a inner join mstarticle as b on a.ArticleCode = b.ArticleCode  WHERE A.SiteCode='" & SiteCode & "'"
            Query = Query & " SELECT top 1 A.OFFERNO,A.LEVELON,A.ISX,A.DISCOUNTTYPE,A.VALUE,A.BPSHAREPERCENTAGE,A.ARTICLECODE, A.TREECODE, A.LEVELCODE, A.NODECODE, A.BATCHNO FROM PROMOTIONDETAILS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO	 "

            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            Dim ds As New DataSet
            Dim daDefault As New SqlDataAdapter(Query, ConString)
            daDefault.Fill(ds)
            ds.Clear()
            ' Data is accessible through the DataReader object here.
        Catch ex As Exception

            LogException(ex)

        End Try
    End Sub

    Public Function getValueByKey(ByVal strKey As String) As String
        If Not DataBaseConnection.resourceMgrBL Is Nothing Then
            Return DataBaseConnection.resourceMgrBL.GetString(strKey)
        Else
            'Return "Error in Getting Resource " & strKey
            Return " " & strKey
        End If

    End Function

    Public Shared Function ConvertToEnglish(ByVal dblvalue As Object) As String
        Try
            If Not dblvalue Is Nothing AndAlso Not dblvalue Is DBNull.Value Then
                Return CDbl(dblvalue).ToString("F2", CultureInfo.CreateSpecificCulture("en-US"))
                'Return IIf(dblvalue.ToString.Contains(","), dblvalue.ToString.Replace(",", "."), dblvalue)
            Else
                Return "0"
            End If
        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try
    End Function

    Public Shared Function GetSystemDateFormat() As String
        Dim installed As CultureInfo = CultureInfo.CurrentCulture
        Dim strShortDateFormat As String = installed.DateTimeFormat.ShortDatePattern.ToString
        Return strShortDateFormat
    End Function
    Public Function GetCustomerDetails(ByVal cardno As String, ByVal SiteCode As String) As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            da = New SqlDataAdapter("select  NameOnCard ,CompanyName , Mobileno  from clpcustomers where  CardNo= '" & cardno & "' and SiteCode ='" & SiteCode & "' ", ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetCustDetailForSoPrint(ByVal CardNo As String) As DataTable 'vipin
        Try
            Dim cmd = New SqlCommand("GetCustDetailForSoPrint", SpectrumCon)
            Dim dt As New DataTable
            cmd.Parameters.AddWithValue("@CardNo", CardNo)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetComboDtlForSoPrint(ByVal SONumber As String) As DataTable 'vipin
        Try
            Dim cmd = New SqlCommand("GetComboDtlForSoPrint", SpectrumCon)
            Dim dt As New DataTable
            cmd.Parameters.AddWithValue("@SoNumber", SONumber)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateBatchDtlQtyAllocated(ByVal sitecode As String, ByVal batchBarCode As String, ByVal qty As Decimal, ByVal trans As SqlTransaction) As Boolean
        Try

            'Changed By Sameer for Issue ID 6955
            Dim query As String = "update ArticleBatchBinStockBalances Set TotalPhysicalSaleableQty=ISNULL(TotalPhysicalSaleableQty,0) + - " & qty & " , TotalSaleableQty=ISNULL(TotalSaleableQty,0) + - " & qty & " ,PhysicalQty=ISNULL(PhysicalQty,0) + - " & qty & " , updatedon= getdate() where SiteCode = '" & sitecode & "' and BatchBarcode = '" & batchBarCode & "' "
            InsertOrUpdateRecord(query, trans)

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetDefaultSetting(ByVal strSiteCode As String, ByVal Doctype As String) As DataTable
        Try
            Dim strString As String = "SELECT DOCUMENTTYPE,DESCRIPTION,FLDLABEL,FLDVALUE,FLDTYPE,UPDATEDBY,UPDATEDON FROM DEFAULTCONFIG WHERE DOCUMENTGROUP='FO' AND Status=1 AND "
            If strSiteCode <> "0000" Then
                strString = strString & " SITECODE='" & strSiteCode & "' AND "
            End If
            If Doctype <> String.Empty Then
                strString = strString & "  DOCUMENTTYPE='" & Doctype & "'"
            End If
            If Right(strString, 4) = "AND " Then
                strString = strString.Substring(0, strString.Length - 4)
            End If

            strString = strString & " Order By DOCUMENTTYPE, Sequence, DESCRIPTION"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetDefaultSetting(ByVal smsFilter As String) As DataTable
        Try
            Dim strString As String = "SELECT DOCUMENTTYPE,DESCRIPTION,FLDLABEL,FLDVALUE,FLDTYPE FROM DEFAULTCONFIG WHERE Status=1 AND FLDLABEL LIKE '%" & smsFilter & "%'"

            strString = strString & " Order By DOCUMENTTYPE, Sequence, DESCRIPTION"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '' added by nikhil 'vipin PC SO Merge 03-05-2018
    Public Function GetArticleDetail(ByVal ArticleCode As String) As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            Dim query As String = "select * from MstArticle where articlecode ='" + ArticleCode + "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPackagingBoxSelection(ByVal sitecode As String, ByVal keyType As Integer, ByVal IsFixed As Integer) As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            Dim query As String = "select a.KeyValue,a.KeyValue,a.KeyCode,a.SiteCode,a.isPackagingMandatory  from MstSiteKeyData a  INNER JOIN  PrintNameMapping NM on NM.keytype =A.keytype  AND NM.Sitecode =a.siteCode and NM.KeyValue = a.keyValue and a.KeyCode =nm.KeyCode AND nm.ISFixedCombo=" & IsFixed & " where a.STATUS = 1 AND NM.status =1 and a.SiteCode In('" & sitecode & "','CCE') and a.KeyType = " & keyType & ""
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSummaryCardData(ByVal siteCode As String, ByVal dayCloseDate As Date, ByVal TerminalID As String, ByVal ShiftId As Integer) As DataSet
        Try
            Dim cmd = New SqlCommand("UDP_GetPCShiftCloseReportHeaderData", SpectrumCon)
            Dim ds As New DataSet
            'Dim da As New SqlDataAdapter(query, ConString)
            cmd.Parameters.AddWithValue("@V_SiteCode", siteCode)
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@V_DayCloseDate", dayCloseDate)
            cmd.Parameters.AddWithValue("@V_TerminalId", TerminalID)
            cmd.Parameters.AddWithValue("@V_ShiftId", ShiftId)
            cmd.Parameters.AddWithValue("@P_ShiftOpenDateTime", ShiftId)
            cmd.Parameters.AddWithValue("@P_ShiftCloseDateTime", ShiftId)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            Return ds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function FunGetComboDetailsForSoEdit(ByVal SoNumber As String, ByVal Qty As Integer, ByVal Price As Decimal, ByVal Disc As Decimal, ByVal ComboSrNo As Integer) As DataSet
        Try 'vipin PC SO Merge 03-05-2018
            Dim cmd = New SqlCommand("UDP_GetComboDetailsForSoEdit", SpectrumCon)
            Dim ds As New DataSet
            'Dim da As New SqlDataAdapter(query, ConString)
            cmd.Parameters.AddWithValue("@V_SoNumber", SoNumber)
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@V_Qty", Qty)
            cmd.Parameters.AddWithValue("@V_Price", Price)
            cmd.Parameters.AddWithValue("@v_Disc", Disc)
            cmd.Parameters.AddWithValue("@V_ComboSrNo", ComboSrNo)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            Return ds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#Region "Online Orders"
    Public Function MooveOnlineOrdertoMainTables(ByVal SiteCode As String, ByVal BillNo As String, ByVal CMBillNo As String) As Boolean
        Try
            OpenConnection()
            Dim cmd = New SqlCommand("InsertAcceptonlineOrder", SpectrumCon)
            Dim ds As New DataSet
            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@BillNo", BillNo)
            cmd.Parameters.AddWithValue("@CMBillNo", CMBillNo)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
            CloseConnection()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllRejectionReasons() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select code,longdesc from GeneralCodeMst where codetype ='ZomatoRejectionReason'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
    Public Function GetHSNbyArticle(ByVal ArticleCode As String) As String     'added by vipin 27.07.2017 GST
        Try 'vipin PC SO Merge 03-05-2018
            Dim Query = "SELECT HSNCODE from MstArticle where articlecode = '" + ArticleCode + "'"

            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            Dim ds As New DataSet
            Dim daDefault As New SqlDataAdapter(Query, ConString)
            daDefault.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)(0).ToString.Trim
            Else
                Return ""
            End If

            ds.Clear()
        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try
    End Function
    Public Shared Function GetArticleGSTTax(ByVal ArticleCodeList As String) As DataTable    'vipin GST 31.07.2017 SO Tax changes
        Try 'vipin PC SO Merge 03-05-2018
            Dim dt As DataTable
            Dim strString As String = "select   SUBString (TaxName,1,4)  AS TaxName , VALUE  ,TaxCode from MSTtax  WHERE Taxcode  IN (" + ArticleCodeList + ") and Status=1 "
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetSalesordertaxdtlsBySO(ByVal SoNumber As String) As DataTable    'vipin GST 31.07.2017 SO Tax changes
        Try 'vipin PC SO Merge 03-05-2018
            Dim dt As DataTable
            Dim strString As String = "select * from  Salesordertaxdtls  where Saleordernumber='" + SoNumber + "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDenominationPC(ByVal CurrencyCode As String, Optional ByVal BaseCurrency As String = "") As DataTable  'vipin on 05-05-2017 PC
        Try
            Dim StrQuery, ExcangeRate As String
            Dim dtdenom As New DataTable
            Dim daDenom As SqlDataAdapter
            ExcangeRate = ""
            If BaseCurrency = CurrencyCode Then
                ExcangeRate = "1"
            ElseIf BaseCurrency <> String.Empty Then
                StrQuery = "SELECT EXCHANGERATE FROM MSTCURRENCYRATE WHERE Status=1 AND CURRENCYCODE='" & BaseCurrency & "' AND RELATIONALCURRENCY='" & CurrencyCode & "'"
                daDenom = New SqlDataAdapter(StrQuery, ConString)
                daDenom.Fill(dtdenom)
                If dtdenom.Rows.Count > 0 Then
                    ExcangeRate = dtdenom.Rows(0)("EXCHANGERATE")
                End If
            End If
            StrQuery = ""
            'Changed By Sameer for issue id 6891 on 4-4-2013
            StrQuery = "SELECT 	A.DENOMINATIONAMT,B.CURRENCYSYMBOL, DenominationDesc AS DENOMINATION,0 AS QTY,"
            If ExcangeRate <> String.Empty Then
                StrQuery = StrQuery & "convert(numeric(18,4)," & ExcangeRate & ") AS EXCHANGERATE,0.0 AS BASEAMOUNT,"
            End If
            'StrQuery = StrQuery & "0.0 AS AMOUNT,B.CURRENCYCODE  FROM DENOMINATIONDETAIL A INNER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE where b.CurrencyCode='" & CurrencyCode & "' And A.Status = 1" & _
            StrQuery = StrQuery & "0.0 AS AMOUNT,B.CURRENCYCODE  FROM DENOMINATIONDETAIL A INNER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE where b.CurrencyCode='" & CurrencyCode & "' And A.Status = 1 And A.DenominationAmt <>'0.50' " & _
                        "ORDER BY A.DENOMINATIONAMT"
            daDenom = New SqlDataAdapter(StrQuery, ConString)
            daDenom.Fill(dtdenom)
            ' dtdenom.Columns("DENOMINATION").Expression = "CURRENCYSYMBOL +  + DENOMINATIONAMT"
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code is Added By irfan for CSA cashmemoheader update on 13/03/2018
    Public Function UpdateDate(ByRef code As String, ByRef Bill As String) As Boolean
        Try
            Dim query As String = ""
            Dim con As SqlConnection
            Dim cmd As SqlCommand
            If code = "CM" Then
                query = "update cashmemohdr set UPDATEDON=GETDATE() WHERE BillNo='" & Bill & "'"
            End If
            If code = "SO" Then
                query = "update salesorderhdr set UPDATEDON=GETDATE() WHERE SaleOrderNumber='" & Bill & "'"
            End If

            cmd = New SqlClient.SqlCommand(query, SpectrumCon)
            SpectrumCon.Open()
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            SpectrumCon.Close()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetTranTypeCode(ByVal bill As String) As String
        Try
            Dim query As String = "select TypeCode from creditreceipt where RefBillNo = '" & bill & "'"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    'added by khusrao adil on 19-04-2017 for natural new devolopment

    Public Function GetVisibleTender(ByVal sitecode As String) As DataTable
        Try
            Dim da As SqlDataAdapter
            Dim qry As String = "select t.TenderHeadCode as TenderHeadCode, t.TenderType as TenderType from MStTEnder t inner join MstTenderType tt on t.TenderType=tt.TenderType where sitecode ='" & sitecode & "' and t.isVisible=1 and tt.STATUS =1 and t.status=1"
            da = New SqlClient.SqlDataAdapter(qry, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDeliveryPartners() As DataTable
        Try
            Dim query As String = " SELECT delieverypartnerid,DelieveryPartnerName FROM MstDelieveryPartner WHERE STATUS = 1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCustomerMultipleAddresses(ByVal CardNo As String, ByVal Clpprogramid As String, Optional ByVal DetailedCustomerCreationformat As String = "0") As DataTable 'vipul
        Try
            Dim query As String = ""
            If DetailedCustomerCreationformat = 1 Then
                query = "select '0' As SrNo,'0' As DefaultAddress,' ' As AddressLn1,' ' As AddressLn2,' ' As AddressLn3,' ' As AddressLn4,'Add New Address' As Address,'' PinCode,'' CityCode,'' State,'' Country  UNION ALL "
            End If
            query = query + "select SrNo,DefaultAddress,AddressLn1,AddressLn2,AddressLn3,AddressLn4+' '+CityCode+' '+PinCode As AddressLn4,AddressLn1+' '+AddressLn2+' '+AddressLn3+' '+AddressLn4+' '+CityCode+' '+PinCode As Address,PinCode,CityCode,DBO.FNGETLOCNDESC(StateCode) AS State,DBO.FNGETLOCNDESC(CountryCode) AS Country "
            query = query + "from CLPCustomerAddress where CardNo ='" & CardNo & "' and Clpprogramid='" & Clpprogramid & "' and status=1  "


            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSOAddresses(ByVal custId As String, ByVal programId As String, ByVal ISCustSelected As Boolean, Optional ByVal IsCustAddress As Boolean = False) As DataTable
        Try
            Dim strString As String = ""
            If ISCustSelected Then
                strString = "select  ISNULL(cast (SrNo as varchar),'')   AS Addresskey,ISNULL(AddressLn1,'') + ' ' + ISNULL(AddressLn2,'') + ' ' + ISNULL(AddressLn3,'') + ' ' + ISNULL(AddressLn4,'') AS AddressValue ,'Address' as addresstype,DefaultAddress from CLPCustomerAddress where Clpprogramid='" & programId & "' and cardno='" & custId & "'"
            End If
            If ISCustSelected AndAlso IsCustAddress Then
                strString = strString
            ElseIf ISCustSelected Then
                strString = strString & " union all select SiteCode as Addresskey,SiteShortName as AddressValue,'Store' as addresstype,0 as DefaultAddress from mstsite where BusinessCode ='Store'"
            Else
                strString = strString & " select SiteCode as Addresskey,SiteShortName as AddressValue,'Store' as addresstype,0 as DefaultAddress from mstsite where BusinessCode ='Store'"
            End If

            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ' created by khusrao adil
    Public Function GetOrderStatusId(ByVal _name As String) As String
        Try
            Dim query As String = "select orderStatusID from orderstatus where name = '" & _name & "'"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    'Code is added by irfan on 15/3/2018 for TableManagment
    Public Function GetSitedetail() As DataTable
        Try
            Dim query As String = "select (ROW_NUMBER() over(order by mst.SiteShortName asc)) as ID, mst.SiteShortName  from defaultconfig con inner join MstSite mst on con.Sitecode=mst.sitecode  where con.fldlabel='LocalSiteCode' and con.STATUS=1"
            Dim Sqldt As DataTable
            Dim da As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            da = New SqlDataAdapter(query.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(da)
            Sqldt = New DataTable
            da.Fill(Sqldt)
            Return Sqldt
            '   Return GetFilledTable(query).Rows(0)(0)       (row_number() over (order by sitecode)) as Id,
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code added by irfan on 15/3/2018 for Table Managment
    Public Function GetSeatingArea() As DataTable
        Try
            Dim query As String = "select SeatingAreaId as id,SeatingAreaName as Name from SeatingArea where Status=1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code added by irfan on 15/3/2018 for Table Managment
    Public Function GetSearchDineIn(ByVal id As String, ByVal name As String, ByVal area As String, ByVal capacity As String, ByVal status As String) As DataTable
        Try
            ' Dim query As String = "select DineInNumber,DineInName,SeatingAreaId,Capacity,Status from MstDineIn where "
            Dim query As String = "select DineInNumber as Id,DineInName as Name,area.SeatingAreaName as 'Seating Area' ,mst.Capacity as Capacity,case when mst.Status ='1' then 'Active' else 'Deactive' end as Status from MstDineIn mst left outer join SeatingArea area  on mst.SeatingAreaId=area.SeatingAreaId  where "
            If id.ToString.Trim <> "" Then
                query = query + " DineInNumber= '" & id & "'"
            End If
            If name.ToString.Trim <> "" Then
                If id.ToString.Trim <> "" Then
                    query = query + " AND DineInName like '%" & name & "%'"
                Else
                    query = query + "  DineInName like '%" & name & "%'"
                End If
            End If
            If area.ToString.Trim <> "" Then
                If id.ToString.Trim <> "" OrElse name.ToString.Trim <> "" Then
                    If area.Contains("-1") Then
                        query = query + " AND  mst.SeatingAreaId between 0 and 5 "
                    Else
                        query = query + " AND  mst.SeatingAreaId= '" & area & "'"
                    End If

                Else
                    If area.Contains("-1") Then
                        query = query + "   mst.SeatingAreaId between 0 and 5 "
                    Else
                        query = query + "  mst.SeatingAreaId= '" & area & "'"
                    End If

                End If
            End If
            If capacity.ToString.Trim <> "" Then
                If id.ToString.Trim <> "" OrElse name.ToString.Trim <> "" OrElse area.ToString.Trim <> "" Then
                    query = query + " AND mst.Capacity = '" & capacity & "'"
                Else
                    query = query + "  mst.Capacity = '" & capacity & "'"
                End If
            End If
            If status.ToString.Trim <> "" Then
                If id.ToString.Trim <> "" OrElse name.ToString.Trim <> "" OrElse area.ToString.Trim <> "" OrElse capacity.ToString.Trim <> "" Then
                    If status.Contains("-1") Then
                        query = query + "AND  mst.Status in (0,1)"
                    Else
                        query = query + " AND mst.Status= '" & status & "'"
                    End If

                Else
                    If status.Contains("-1") Then
                        query = query + "  mst.Status in (0,1)"
                    Else
                        query = query + "  mst.Status= '" & status & "'"
                    End If

                End If
            End If
            Dim Sqldt As DataTable
            Dim da As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            da = New SqlDataAdapter(query.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(da)
            Sqldt = New DataTable
            da.Fill(Sqldt)
            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code added by irfan on 17/4/2018 for Table Managment
    Public Function updateDineInStatus(ByVal id As String, ByVal status As Boolean) As Boolean
        Try
            Dim sqlcom As SqlCommand
            Dim query As String
            query = "Update  MstDineIn Set UpdatedOn=GetDate(),Status='" & status & "' where DineInNumber='" & id & "'"
            sqlcom = New SqlCommand(query, SpectrumCon)
            SpectrumCon.Open()
            If sqlcom.ExecuteNonQuery > 0 Then
                SpectrumCon.Close()
                Return True
            Else
                SpectrumCon.Close()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code added by irfan on 15/3/2018 for Table Managment
    Public Function DineInIdExist(ByVal id As String) As Boolean
        Try
            Dim query As String
            query = "select * from MstDineIn where DineInNumber='" & id & "'"
            Dim Sqlda As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            Dim Sqldt As DataTable
            Sqlda = New SqlDataAdapter(query.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqldt = New DataTable
            Sqlda.Fill(Sqldt)
            If Sqldt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'Code added by irfan on 15/3/2018 for Table Managment
    Public Function updateDineIn(ByVal sitecode As String, ByVal id As String, ByVal name As String, ByVal area As String, ByVal capacity As String, ByVal status As Boolean, ByVal createdby As String, ByVal action As Boolean) As DataTable
        Try

            Dim sqlcom As SqlCommand
            Dim query As String
            If action = False Then
                query = "insert into MstDineIn(SiteCode,DineInNumber,DineInName,CreatedAt,CreatedBy,CreatedOn,UpdatedAt,UpdatedBy,UpdatedOn,SeatingAreaId,Capacity,Status) values('" & sitecode & "','" & id & "','" & name & "','" & sitecode & "','" & createdby & "',GetDate(),'" & sitecode & "','" & createdby & "',GetDate(),'" & area & "','" & capacity & "','" & status & "')"
            End If
            If action = True Then
                query = "Update  MstDineIn Set SiteCode='" & sitecode & "',DineInName='" & name & "',CreatedAt='" & sitecode & "',CreatedBy='" & createdby & "',CreatedOn=GetDate(),UpdatedAt='" & sitecode & "',UpdatedBy='" & createdby & "',UpdatedOn=GetDate(),SeatingAreaId='" & area & "',Capacity='" & capacity & "',Status='" & status & "' where DineInNumber='" & id & "'"
            End If
            sqlcom = New SqlCommand(query, SpectrumCon)
            SpectrumCon.Open()
            sqlcom.ExecuteNonQuery()
            SpectrumCon.Close()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveSiteSeatingAreaMapping(ByVal _dt As DataTable) As Boolean
        Try
            Dim sqlcom As SqlCommand

            Dim strQuery As New StringBuilder
            strQuery.Length = 0
            SpectrumCon.Open()
            For Each dr As DataRow In _dt.Rows
                Dim strCheckQuery = "Select * from SiteSeatingAreaMapping where SiteCode='" + dr("SiteCode").ToString() + "' and SeatingAreaId='" + dr("SeatingAreaId").ToString() + "'"
                sqlcom = New SqlCommand(strCheckQuery, SpectrumCon)
                Dim sqlReader As SqlDataReader = sqlcom.ExecuteReader()
                If sqlReader.Read() Then
                    strQuery.Append(" update SiteSeatingAreaMapping set SiteCode='" + dr("SiteCode").ToString() + "' , SeatingAreaId='" + dr("SeatingAreaId").ToString() + "'")
                    strQuery.Append(" ,UPDATEDAT='" + dr("UPDATEDAT") + "', UPDATEDBY='" + dr("UPDATEDBY") + "', UPDATEDON=GETDATE(), Status='" + dr("Status").ToString() + "'")
                    strQuery.Append(" where  SiteCode='" + dr("SiteCode") + "' and SeatingAreaId='" + dr("SeatingAreaId").ToString() + "'; ")
                Else
                    strQuery.Append("insert into SiteSeatingAreaMapping (SiteCode,SeatingAreaId,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,Status) values(")
                    strQuery.Append("'" + dr("SiteCode") + "',")
                    strQuery.Append("'" + dr("SeatingAreaId").ToString() + "',")
                    'strQuery.Append("" + Convert.ToInt64(dr("SeatingAreaId").ToString()) + ",")
                    strQuery.Append("'" + dr("CREATEDAT") + "',")
                    strQuery.Append("'" + dr("CREATEDBY") + "',")
                    strQuery.Append("GETDATE(),")
                    strQuery.Append("'" + dr("UPDATEDAT") + "',")
                    strQuery.Append("'" + dr("UPDATEDBY") + "',")
                    strQuery.Append("GETDATE(),")
                    strQuery.Append("'" + dr("Status").ToString() + "');")
                    'strQuery.Append("('" + dr("SiteCode") + "'," + dr("SeatingAreaId") + ",'" + dr("CREATEDAT") + "','" + dr("CREATEDBY") + "',")
                    'strQuery.Append("GETDATE(),'" + dr("UPDATEDAT") + "','" + dr("UPDATEDBY") + "',GETDATE(),'" + Convert.ToBoolean(dr("Status")) + "') ;")
                End If
                sqlReader.Close()
            Next
            sqlcom = New SqlCommand(strQuery.ToString(), SpectrumCon)
            If sqlcom.ExecuteNonQuery() Then
                Return True
            Else
                Return False
            End If
            SpectrumCon.Close()
        Catch ex As Exception
            SpectrumCon.Close()
            LogException(ex)
        Finally
            SpectrumCon.Close()
        End Try
    End Function
    Public Function GetTableStruc() As DataTable
        Try
            Dim query As String = "select DineInNumber as Id ,DineInName as Name,SeatingAreaId as 'Seating Area',Capacity,Status from MstDineIn where  1=0"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added  by khusrao adil on 21-08-2018 for Seating area mapping
    Public Function GetSiteSeatingAreaMappingTableStruc() As DataTable
        Try
            Dim query As String = "select *, '' as oldSeatingAreaId from SiteSeatingAreaMapping where 1=0"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDineIN(Optional ByVal isSeatingAreaMappingCall As Boolean = False) As DataTable  'mdified by khusrao adil on 21-08-2018 for seating are mapping
        Try
            '  Dim query As String = "select DineInNumber,DineInName,SeatingAreaId,Capacity,Status from MstDineIn"
            'Dim query As String = "select DineInNumber as Id,DineInName as Name,area.SeatingAreaName as 'Seating Area' ,mst.Capacity as Capacity,case when mst.Status ='1' then 'Active' else 'Deactive' end as Status from MstDineIn mst left outer join SeatingArea area  on mst.SeatingAreaId=area.SeatingAreaId"
            Dim query As New StringBuilder
            query.Length = 0
            If True Then
                query.Append("select DineInNumber as Id,DineInName as Name,area.SeatingAreaName as 'Seating Area' ,mst.Capacity as Capacity,case when mst.Status ='1' then 'Active' else 'Deactive' end as ")
                query.Append("Status,area.SeatingAreaId,mst.SiteCode from MstDineIn mst left outer join SeatingArea area  on mst.SeatingAreaId=area.SeatingAreaId")
            Else
                query.Append("select DineInNumber as Id,DineInName as Name,area.SeatingAreaName as 'Seating Area' ,mst.Capacity as Capacity,case when mst.Status ='1' then 'Active' else 'Deactive' end as Status from MstDineIn mst left outer join SeatingArea area  on mst.SeatingAreaId=area.SeatingAreaId")
            End If
            Dim Sqldt As DataTable
            Dim da As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            da = New SqlDataAdapter(query.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(da)
            Sqldt = New DataTable
            da.Fill(Sqldt)
            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTopDineInNumber() As String
        Try
            Dim query As String = "select max(DineInNumber) from MstDineIn"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetSiteName(ByVal sitecode As String) As String
        Try
            Dim query As String = "select SiteShortName  from MstSite where SiteCode = '" & sitecode & "'"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    'Public Function GetTemplateId() As String
    '    Try
    '        Dim query As String = "select mstPrepStationTemplateID from MstPrepStationTemplate "
    '        Return GetFilledTable(query).Rows(0)(0)
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return String.Empty
    '    End Try
    'End Function
    ''Code is Added by irfan on 21/8/2018 for Change Password user Management
    Public Function GetAuthUserdetail(ByVal name As String) As DataTable
        Try
            Dim query As String = " select SiteCode,UserID,CountryCode,Designation,IDNumber,EmailId,Active from authusers where UserID ='" & name & "' Or UserName='" & name & "' and status=1 "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''Code is Added by irfan on 21/8/2018 for Change Password user Management
    Public Function CheckAuthUserDetails(ByVal userid As String) As DataTable
        Try
            Dim query As String = "select Password from authusers where UserID='" & userid & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllAuthUser() As DataTable 'vipul 22-08-2018
        Try
            Dim query As String = "select UserID,UserName,UserID+' '+UserName 'SearchBy' from authusers where STATUS =1 "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''Code is Added by irfan on 21/8/2018 for Change Password user Management
    Public Function UpdateAuthUserDetails(ByVal UserID As String, ByVal Passowrd As String, ByVal PassowrdUpdateDate As Date, ByVal UpdateAt As String, ByVal UpdateBy As String, ByVal UpdatedOn As Date) As Boolean
        Try
            Dim query As String = ""
            Dim nextdata As Date = DateTime.Now.AddDays(90)
            query = "UPdate AuthUsers set Password='" & Passowrd & "',PasswordUpdateDate=getdate(),PasswordChangeNextDate='" & nextdata & "',UPDATEDAT='" & UpdateAt & "',UPDATEDBY='" & UpdateBy & "',UPDATEDON=getdate() where UserID='" & UserID & "'"
            OpenConnection()
            Dim daCheckDtls As New SqlCommand(query, SpectrumCon)
            If daCheckDtls.ExecuteNonQuery > 0 Then
                CloseConnection()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetTemplatedetail() As DataTable
        Try
            Dim query As String = " select '0' as Id,'Create New' as mstPrepStationTemplateID union all  "
            query += " select ROW_NUMBER() over (order by mstPrepStationTemplateID) as Id , mstPrepStationTemplateID from MstPrepStationTemplate where mstPrepStationTemplateID "
            query += " not in ( select m.mstPrepStationTemplateID from MstPrepStation a left join  MstPrepStationTemplate m on a.mstPrepStationTemplateID=m.mstPrepStationTemplateID ) and status=1 "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetNewTemplatedetail(ByVal name As String) As DataTable
        Try
            Dim query As String = " select ROW_NUMBER() over (order by mstPrepStationTemplateID) as Id , mstPrepStationTemplateID from MstPrepStationTemplate where mstPrepStationTemplateID ='" & name & "' and status=1 "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTemplateArticleshortname(ByVal name As String) As DataTable
        Try
            Dim query As String = " select mstPrepStationTemplateID,shortDesc,description,status from MstPrepStationTemplate where mstPrepStationTemplateID='" & name & "'   "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckTemplateNameExist(ByVal name As String) As Boolean
        Try
            Dim query As String
            query = "select * from MstPrepStationTemplate where mstPrepStationTemplateID='" & name & "'"
            Dim Sqlda As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            Dim Sqldt As DataTable
            Sqlda = New SqlDataAdapter(query.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqldt = New DataTable
            Sqlda.Fill(Sqldt)
            If Sqldt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function CheckArticleExistinMappingTemplate(ByVal Articlecode As String, ByVal sitecode As String, ByVal usercode As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable

            Dim sqlComm As New SqlCommand("USP_PrepStationTemplateArticleMapping", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@Articlecode", Articlecode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", sitecode)
            sqlComm.Parameters.AddWithValue("@UserCode", usercode)
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            'OpenConnection()
            'If sqlComm.ExecuteNonQuery > 0 Then
            '    CloseConnection()
            '    Return True
            'End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function
    Public Function GetArticleMapwithTemplate(ByVal name As String) As DataTable
        Try
            Dim query As String = " select ArticleCode from mstarticle where lastnodecode in (select nodecode from MstPrepStationTemplateMstArticleNodeMap where mstPrepStationTemplateID ='" & name & "') "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTemplatedata(ByVal name As String) As DataSet
        Try

            Dim StrQuery As New StringBuilder
            StrQuery.Length = 0
            Dim StoreSateCode As String = ""
            Dim CutomerSateCode As String = ""
            StrQuery.Append(" select mstPrepStationTemplateID,shortDesc,description,status from MstPrepStationTemplate where mstPrepStationTemplateID='" & name & "' ; ")
            StrQuery.Append(" select mstPrepStationID,status from MstPrepStation WHERE mstPrepStationTemplateID ='" & name & "' ; ")
            StrQuery.Append(" select nodeCode,status from MstPrepStationTemplateMstArticleNodeMap where mstPrepStationTemplateID='" & name & "' and status=1 ; ")
            StrQuery.Append(" select * from MstPrepStationTemplateMstArticleMap where mstPrepStationTemplateID='" & name & "' ")

            Dim da As New SqlDataAdapter(StrQuery.ToString(), ConString)
            Dim ds As New DataSet
            da.Fill(ds)
            ds.Tables(0).TableName = "MstPrepStationTemplate"
            ds.Tables(1).TableName = "MstPrepStation"
            ds.Tables(2).TableName = "MstPrepStationTemplateMstArticleNodeMap"
            ds.Tables(3).TableName = "MstPrepStationTemplateMstArticleMap"

            If ds.Tables("MstPrepStationTemplate").Rows(0)(0) <> "" Then
                Return ds
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAddTemplate() As DataTable
        Try
            Dim query As String = " select  ROw_number()  over (order by mstPrepStationTemplateID) as 'Srno' ,shortDesc,status,CreatedOn from MstPrepStationTemplate "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function updateMstArticleNodeMap1(ByVal name As String, ByVal tempname As String, ByVal tempsortname As String, ByVal tempdesc As String, ByVal sitecode As String, ByVal usercode As String, ByVal siteName As String, ByVal tempstatus As Boolean, ByVal tbl As DataTable, ByVal dtgrid As DataTable) As Boolean
        Try
            Dim trans As SqlTransaction
            Dim Tempid As String
            If Not name.Contains(sitecode) Then
                Tempid = name & "-" & sitecode
            Else
                Tempid = name
            End If
            Dim query As String = " "

            If CheckTemplateExist(tempname) Then
                query += ("update MstPrepStationTemplate set shortDesc='" & tempsortname & "' , description='" & tempdesc & "', updatedAt='" & sitecode & "',updatedBy='" & usercode & "' , updatedOn=GetDate() where mstPrepStationTemplateID='" & tempname & "' ")

            Else
                query += "insert into MstPrepStationTemplate values('" & tempname & "','" & tempsortname & "','" & tempdesc & "','" & sitecode & "','" & usercode & "',GetDate(),'" & sitecode & "','" & usercode & "',GetDate(),'" & tempstatus & "') "

            End If

            If CheckTemplateStationIdExist(Tempid) Then
                query += ("update MstPrepStation set mstPrepStationTemplateID='" & tempname & "', updatedAt='" & sitecode & "',updatedBy='" & usercode & "' ,updatedOn=GetDate(),status='" & tempstatus & "' where mstPrepStationID='" & Tempid & "' ")
            Else
                query += "insert into MstPrepStation(mstPrepStationID,mstPrepStationTemplateID,siteCode,createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,status) values('" & Tempid & "','" & tempname & "','" & sitecode & "','" & sitecode & "','" & usercode & "',GetDate(),'" & sitecode & "','" & usercode & "',GetDate(),'" & tempstatus & "') "
            End If

            For d = 0 To tbl.Rows.Count - 1
                If CheckTemplateArticleExist(tbl.Rows(d)("NodeCode"), tempname) Then
                    query += ("update MstPrepStationTemplateMstArticleNodeMap set  updatedAt='" & sitecode & "',updatedBy='" & usercode & "' ,updatedOn=GetDate(),status='" & tbl.Rows(d)("Status") & "'   where nodecode='" & tbl.Rows(d)("NodeCode") & "' and mstPrepStationTemplateID='" & tempname & "'  ")
                    If tbl.Rows(d)("Status") = False Then
                        query += ("update MstPrepStationTemplateMstArticleMap set status='" & tbl.Rows(d)("status") & "' where articlecode in (select articleCode from mstarticle where lastnodecode='" & tbl.Rows(d)("NodeCode") & "')  ")
                        'query +=update MstPrepStationTemplateMstArticleMap set status=0 where articlecode in (select articleCode from mstarticle where lastnodecode='ANCCCE000000010')
                    End If
                Else
                    If tbl.Rows(d)("Status") Then
                        query += "insert into MstPrepStationTemplateMstArticleNodeMap values('" & tempname & "','" & tbl.Rows(d)("NodeCode") & "','" & sitecode & "','" & usercode & "',GetDate(),'" & sitecode & "','" & usercode & "',GetDate(),'" & tbl.Rows(d)("Status") & "') "
                    End If
                End If
            Next
            For y = 0 To dtgrid.Rows.Count - 1
                If CheckTemplateArticleCodeExist(dtgrid.Rows(y)("ArticleCode"), tempname) Then
                    Dim a As Boolean = IIf(dtgrid.Rows(y)("Status") = True, False, True)
                    query += ("update MstPrepStationTemplateMstArticleMap set  updatedAt='" & sitecode & "',updatedBy='" & usercode & "' ,updatedOn=GetDate(),status='" & a & "' where articleCode='" & dtgrid.Rows(y)("ArticleCode") & "' and mstPrepStationTemplateID='" & tempname & "' ")
                Else
                    Dim a As Boolean = IIf(dtgrid.Rows(y)("Status") = True, False, True)
                    query += "insert into MstPrepStationTemplateMstArticleMap values('" & tempname & "','" & dtgrid.Rows(y)("ArticleCode") & "','" & sitecode & "','" & usercode & "',GetDate(),'" & sitecode & "','" & usercode & "',GetDate(),'" & a & "') "
                End If

            Next

            OpenConnection()
            trans = SpectrumCon.BeginTransaction
            Dim daCheckDtls As New SqlCommand(query, SpectrumCon)
            daCheckDtls.Transaction = trans
            If daCheckDtls.ExecuteNonQuery() > 0 Then
                trans.Commit()
                CloseConnection()
                Return True
            Else
                trans.Rollback()
                CloseConnection()
                Return False
            End If

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckTemplateExist(ByVal nodecode As String) As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select mstPrepStationTemplateID from MstPrepStationTemplate where mstPrepStationTemplateID='" & nodecode & "'  "
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function showTemplateArticleExist(ByVal name As String) As DataTable
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select NodeCode from MstPrepStationTemplateMstArticleNodeMap where mstPrepStationTemplateID='" & name & "' and status=1; "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing

        End Try
    End Function
    Public Function CheckTemplateStationIdExist(ByVal nodecode As String) As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select mstPrepStationID from MstPrepStation where mstPrepStationID='" & nodecode & "'  "
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckTemplateArticleExist(ByVal nodecode As String, Optional ByVal name As String = "") As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select mstPrepStationTemplateID from MstPrepStationTemplateMstArticleNodeMap where nodeCode='" & nodecode & "'  "
            If Not String.IsNullOrEmpty(name) Then
                query += " and mstPrepStationTemplateID='" & name & "' "
            End If
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckTemplateArticleCodeExist(ByVal articlecode As String, ByVal name As String) As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select mstPrepStationTemplateID from MstPrepStationTemplateMstArticleMap where articleCode='" & articlecode & "' and mstPrepStationTemplateID='" & name & "' "
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsExistInTempalte(ByVal nodecode As String) As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable

            Dim query As String
            query = "select * from  MstPrepStationTemplateMstArticleNodeMap where nodeCode='" & nodecode & "' "
            dataTable = GetFilledTable(query)
            If Not DataTable Is Nothing AndAlso DataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function preparedatatemplate(ByVal nodecode As String) As DataTable
        Try
            Dim query As String = " "
            query += " select a.ArticleCode,a.ArticleName,(c.NodeName +'_'+ b.NodeName) as ParentArt,a.LastNodeCode,case when a.STATUS ='True' then '0' else '1' end as Status  from MstArticle  a inner join mstarticlenode b on a.LastNodeCode=b.nodecode "
            query += " inner join mstarticlenode c on a.ParentArt=c.nodecode   where a.LastNodeCode in ('" & nodecode.Replace(",", "','") & "') "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetNewpreparedatatemplate(ByVal nodecode As String, ByVal value As String) As DataTable
        Try
            Dim query As String = ""
            query += "select distinct artmap.articleCode,mst.ArticleName,(b.NodeName +'_'+ c.NodeName) as ParentArt,mst.LastNodeCode,case when artmap.status ='True' then '0' else '1' end as status "
            query += " from MstPrepStationTemplateMstArticleMap artmap inner join MstArticle mst on artmap.articleCode= mst.ArticleCode inner join MstPrepStationTemplateMstArticleNodeMap mstnodemap on mstnodemap.nodeCode=mst.lastnodecode inner join MstArticleNode c  "
            query += " on mst.ParentArt=c.Nodecode inner join MstArticleNode b on mst.LastNodeCode=b.Nodecode where mstnodemap.nodeCode in ('" & nodecode.Replace(",", "','") & "') and artmap.mstPrepStationTemplateID='" & value & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function Getpreparedatatemplate(ByVal nodecode As String) As DataTable
        Try
            'Dim query As String = "select SiteTelephone1 + case when SiteTelephone1='' then '' else (case when SiteTelephone2='' then '' else ','   end) end  +SiteTelephone2 from mstsite where SiteCode = '" & sitecode & "'"
            Dim query As String = "select ArticleCode,ArticleShortName,Status from MstArticle where LastNodeCode = '" & nodecode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Public Function showgrid() As DataTable
    '    Try
    '        'Dim query As String = "select distinct mstmap.nodeCode as NodeCode,mstart.NodeName as NodeName,art.ParentArt as ParentName from MstPrepStationTemplateMstArticleNodeMap mstmap "
    '        'query += "inner join  mstarticlenode mstart on mstmap.nodeCode=mstart.Nodecode inner join MstArticle art on mstmap.nodeCode=art.LastNodeCode"
    '        Dim query As String = "SELECT a.NODECODE as NodeCode,b.NodeName as NodeName,c.ParentNodecode as ParentCode,b1.NodeName as ParentName FROM MstPrepStationTemplateMstArticleNodeMap A  LEFT JOIN ArticleTreeNodeMap c on c.Nodecode = a.nodeCode "
    '        query += " left join MSTarticlenode  B on a.nodeCode =b.Nodecode LEFT JOIN  MSTarticlenode B1 on B1.Nodecode =  c.ParentNodecode "
    '        Return GetFilledTable(query)
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function
    Public Function showTempate() As DataTable
        Try
            Dim query As String = " "
            query += " select m.mstPrepStationTemplateID,m.shortDesc,case when m.status ='True' then 'Active' else 'Deactive' end as Status, "
            query += " m.createdOn from MstPrepStationTemplate m inner join MstPrepStation a  on    m.mstPrepStationTemplateID=a.mstPrepStationTemplateID  order by m.createdOn desc  "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function shownewTempate() As DataTable
        Try
            Dim query As String = " select mstPrepStationTemplateID,shortDesc,case when status ='True' then 'Active' else 'Deactive' end as Status,createdOn from MstPrepStationTemplate order by createdOn desc "

            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsTemplateisMappedWithStation(ByVal temp As String) As Boolean
        Try
            Dim sqlcom As String
            Dim dataTable As DataTable
            Dim query As String
            query = "select * from MstPrepStation where mstPrepStationTemplateID='" & temp & "'  "
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function AddTemplate(ByVal tempname As String, ByVal tempsortname As String, ByVal tempdesc As String, ByVal sitecode As String, ByVal usercode As String, ByVal siteName As String, ByVal tempstatus As Boolean) As Boolean
        Try
            Dim query As String = ""
            Dim status As Boolean = True
            If CheckTemplateExist(tempname) Then
                query += ("update MstPrepStationTemplate set shortDesc='" & tempsortname & "' , description='" & tempdesc & "', updatedAt='" & sitecode & "',updatedBy='" & usercode & "' , updatedOn=GetDate(), status='" & tempstatus & "' where mstPrepStationTemplateID='" & tempname & "' ")
            Else
                query += "insert into MstPrepStationTemplate values('" & tempname & "','" & tempsortname & "','" & tempdesc & "','" & sitecode & "','" & usercode & "',GetDate(),'" & sitecode & "','" & usercode & "',GetDate(),'" & tempstatus & "') "
            End If
            OpenConnection()
            Dim sqlcom As New SqlCommand(query, SpectrumCon)
            If sqlcom.ExecuteNonQuery > 0 Then
                CloseConnection()
                Return True
            Else
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function GetSiteTelphoneNo(ByVal sitecode As String) As String
        Try
            'Dim query As String = "select SiteTelephone1 + case when SiteTelephone1='' then '' else (case when SiteTelephone2='' then '' else ','   end) end  +SiteTelephone2 from mstsite where SiteCode = '" & sitecode & "'"
            Dim query As String = "select IsNull(SiteTelephone1,'') As SiteTelephone1 from mstsite where SiteCode = '" & sitecode & "'"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetOrderPreparationSiteList(ByVal CodeType As String) As DataTable
        Try
            Dim query As String = "Select Code,ShortDesc from GeneralCodeMst where CodeType='" & CodeType & "' and status=1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetActiveUOMs() As DataTable
        Try
            Dim query As String = " SELECT UOMCode , UOM  FROM MstUoM WHERE STATUS = 1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetEnquiryData(ByVal sitecode As String, ByVal keyType As Integer) As DataTable
        Try
            Dim query As String = "select KeyValue,KeyValue,KeyCode,SiteCode,isPackagingMandatory  from MstSiteKeyData where STATUS = 1 and SiteCode In('" & sitecode & "','CCE') and KeyType = " & keyType & ""
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllCustomerServiceArticlePeriodMap(ByVal CardNo As String) As DataTable
        Try
            Dim query As String = "select * from ClpCustomerServiceArticlePeriodMap where cardno ='" & CardNo & ""
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPackagingBox(ByVal sitecode As String, ByVal keyType As Integer) As DataTable
        Try
            Dim query As String = "select KeyValue,KeyValue,KeyCode,SiteCode,isPackagingMandatory  from MstSiteKeyData where STATUS = 1 and SiteCode In('" & sitecode & "','CCE') and KeyType = " & keyType & ""
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function ClpCustomerServiceArticlePeriodMap(ByVal CardNo As String) As DataTable
        Try
            Dim query As String = "select * from ClpCustomerServiceArticlePeriodMap where cardno '" & CardNo & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticlesForMember(ByVal sitecode As String) As DataTable
        Try
            Dim query As String = "select ma.ArticleShortName'KeyValue', MA.ArticleCode 'KeyCode' from ServiceArticlePeriodMap A"
            query = query + " INNER JOIN MstEAN ME on me.EAN = a.EAN and ME.STATUS=1"
            query = query + " LEFT JOIN mstarticle MA on MA.ArticleCode = ME.ArticleCode"
            query = query + " where a.status =1 and SiteCode In( '" & sitecode & "')"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetGridDataForMemberOnArticleCodeSelection(ByVal ArticleCode As String) As DataTable
        Try
            Dim query As String = "select a.periodid, B.name'PeriodName',amount'Price',A.EAN,A.SrNo from ServiceArticlePeriodMap A"
            query = query + "  INNER JOIN MstEAN ME on me.EAN = a.EAN and ME.STATUS=1 LEFT JOIN Period B on B.PeriodID =a.PeriodID and b.Status=1 LEFT JOIN MstArticle MAR on MAR.ArticleCode = ME.ArticleCode where  MAR.articleShortname ='" & ArticleCode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function LoadExistingCustForMembership(ByVal CardNo As String) As DataTable
        Try
            Dim query As String = "select  ROw_number()  over (order by d.articlecode) 'Srno' ,d.articleshortname 'articlecode',b.name 'Period',a.StartDate,a.EndDate,a.Amount+a.DiscountAmount 'Price',a.discountInPercentage 'DiscountPer',a.DiscountAmount 'Discount',a.Amount 'NetAmt',a.isConvertedToMember"
            query = query + "  from ClpCustomerServiceArticlePeriodMap A  LEFT JOIN Period   B on b.periodid = a.periodid and b.Status=1"
            query = query + "  INNER JOIN MstEAN ME on me.EAN = a.EAN and ME.STATUS=1 LEFT JOIN MstArticle d on d.ArticleCode = ME.ArticleCode where a.Status=1 and  a.CardNo ='" & CardNo & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticleCodeForMember(ByVal sitecode As String, ByVal keyType As Integer) As DataTable
        Try
            Dim query As String = "select KeyValue,KeyValue,KeyCode,SiteCode,isPackagingMandatory  from MstSiteKeyData where STATUS = 1 and SiteCode In('" & sitecode & "','CCE') and KeyType = " & keyType & ""
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPackagingBoxDataSet(ByVal lastNodeCode As String) As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" SELECT MA.ArticleCode ,ArticalTypeCode ,ArticleShortName,ArticleName,BaseUnitofMeasure,ME.EAN " & vbCrLf)
            vStmtQry.Append(" FROM  mstarticle AS MA  inner join MstEAN ME on ma.ArticleCode=me.ArticleCode inner join " & vbCrLf)
            vStmtQry.Append(" 	  dbo.GetLastNodeCode('" & lastNodeCode & "') AS ln " & vbCrLf)
            vStmtQry.Append(" 	  ON MA.LastNodeCode = ln.lastnodeCode  Where MA.status =1 and Ma.articleactive =1  order by ArticleShortName" & vbCrLf)

            'vStmtQry.Append(" SELECT ArticleCode ,ArticalTypeCode ,ArticleShortName,ArticleName,BaseUnitofMeasure " & vbCrLf)
            'vStmtQry.Append(" FROM  mstarticle AS MA   " & vbCrLf)
            'vStmtQry.Append(" Where	MA.LastNodeCode = '" & lastNodeCode & "'" & vbCrLf)
            Dim Sqlda As SqlDataAdapter
            Dim Sqlcmdb As SqlCommandBuilder
            Dim Sqldt As DataTable
            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqldt = New DataTable
            Sqlda.Fill(Sqldt)

            Sqldt.TableName = "PackagingBox"

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Public Shared Function GetPreviousShiftDetails(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String) As DataTable
    '    Try
    '        Dim dt As DataTable
    '        'Dim strString As String = "SELECT MAX(ShiftId) As ShiftId,MAX(CREATEDON) As CREATEDON FROM ShiftOpenClose WHERE sitecode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND OpenDate= '" & DayOpenDate & "'"
    '        Dim strString As String = "SELECT MAX(ShiftId) As ShiftId,MAX(CREATEDON) As CREATEDON FROM ShiftOpenClose WHERE sitecode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND OpenDate=@DayOpenDate"
    '        Dim cmd As New SqlCommand(strString, SpectrumCon())
    '        cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
    '        cmd.Parameters("@DayOpenDate").Value = DayOpenDate
    '        Dim da As New SqlDataAdapter(cmd)
    '        dt = New DataTable
    '        da.Fill(dt)
    '        Return dt
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function
    Public Function GetAllTaxesAppliedToSite(ByVal sitecode As String, ByVal docType As String) As DataTable
        Try
            Dim query As String = "select TaxName ,TaxCode from TaxSiteDocMap where STATUS = 1 and SiteCode = '" & sitecode & "' and DocumentType = '" & docType & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAllTaxesAppliedToSiteDocumentLevel(ByVal sitecode As String, ByVal docType As String) As DataTable
        Try
            Dim query As String = "select TSDM.TaxCode,TSDM.TaxName   from TaxSiteDocMap as TSDM Inner Join MstTax As mt On mt.TaxCode=TSDm.TaxCode where IsDocumentLevelTax=1 and mt.STATUS=1 and TSDM.STATUS = 1  and TSDM.SiteCode = '" & sitecode & "' and TSDM.DocumentType = '" & docType & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPromotionIdByName(ByVal promoName As String) As String
        Try
            Dim strPromotionId As String = ""
            Dim da As SqlDataAdapter
            Dim qry As String = "Select OfferNo as PromotionId from Promotions Where OfferName='" & promoName & "'"
            da = New SqlClient.SqlDataAdapter(qry, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                strPromotionId = dt.Rows(0)("PromotionId")
            End If
            Return strPromotionId
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '----New Pc Chnages
    Public Function GetAllTaxesDetailAppliedToSite(ByVal TaxName As String, ByVal docType As String) As DataTable
        Try
            Dim query As String = "select * from TaxSiteDocMap where IsDocumentLevelTax=1 and TaxName = '" & TaxName & "' and DocumentType = '" & docType & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDetailsForTax() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'') as TaxName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TaxCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TaxPercent, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Type," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TaxAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Status" & vbCrLf)

            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetArticleDescription(ByVal articleCode As String) As String
        Try
            Dim dataTable As DataTable
            Dim query As String = "select ArticleShortName  from MstArticle where ArticleCode = '" & articleCode & "'"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function CheckIfValidArticleCode(ByVal articleCode As String) As Boolean
        Try
            Dim result As Boolean
            Dim dataTable As DataTable
            Dim query As String = "select ArticleShortName  from MstArticle where ArticleCode = '" & articleCode & "'"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function SaveDefaultSetting(ByVal dtData As DataTable, ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction
        Try
            dtData.TableName = "DefaultConfig"
            If dtData.Columns.Contains("SiteCode") = False Then
                AddColumnToDataTable(dtData, "SiteCode", "System.String", SiteCode)
            End If
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(dtData, SpectrumCon, tran) Then
                tran.Commit()
                CloseConnection()
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
#Region "Save final data"
    ''' <summary>
    ''' Function is used to save the Data 
    ''' </summary>
    ''' <param name="dSet">Send the final dataset to this function</param>
    ''' <returns>true if success else false</returns>
    ''' <remarks></remarks>
    Public Function SaveData(ByVal dSet As DataSet, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try

            Dim i As Integer = 0
            For i = 0 To dSet.Tables.Count - 1
                Dim strSelect As String
                Dim StrTableName As String
                Dim SelectedColumns As String = ""
                StrTableName = dSet.Tables(i).TableName.ToString()
                '                If Not (StrTableName = "BirthListRequestedItems") Then
                Dim j As Integer = 0
                If dSet.Tables(i).Columns.Count > 0 Then
                    For j = 0 To dSet.Tables(i).Columns.Count - 1
                        SelectedColumns = SelectedColumns & "," & dSet.Tables(i).Columns(j).ColumnName.ToString()
                    Next j
                    SelectedColumns = SelectedColumns.Substring(1)
                    strSelect = "SELECT " + SelectedColumns & " FROM " & StrTableName & " WHERE 1=0"
                    Dim dasave As New SqlDataAdapter(strSelect, con)
                    dasave.SelectCommand.CommandTimeout = 0
                    dasave.SelectCommand.Transaction = tran
                    Dim cb As SqlCommandBuilder
                    cb = New SqlCommandBuilder(dasave)
                    dasave = cb.DataAdapter
                    dasave.Update(dSet, StrTableName)
                End If

                '                End If
            Next i


            Return True
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function



    'Public Function fnSaveBLEdit(ByVal dSet As DataSet, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
    '    Try
    '        Dim i As Integer = 0
    '        For i = 0 To dSet.Tables.Count - 1
    '            Dim strSelect As String
    '            Dim StrTableName As String
    '            Dim SelectedColumns As String = ""
    '            StrTableName = dSet.Tables(i).TableName.ToString()
    '            'If Not (StrTableName = "BirthListRequestedItems") Then
    '            Dim j As Integer = 0
    '            For j = 0 To dSet.Tables(i).Columns.Count - 1
    '                SelectedColumns = SelectedColumns & "," & dSet.Tables(i).Columns(j).ColumnName.ToString()
    '            Next j
    '            SelectedColumns = SelectedColumns.Substring(1)
    '            strSelect = "SELECT " + SelectedColumns & " FROM " & StrTableName & " WHERE 1=0"
    '            Dim dasave As New SqlDataAdapter(strSelect, con)
    '            dasave.SelectCommand.CommandTimeout = 0
    '            dasave.SelectCommand.Transaction = tran
    '            Dim cb As SqlCommandBuilder
    '            cb = New SqlCommandBuilder(dasave)
    '            dasave = cb.DataAdapter
    '            dasave.Update(dSet, StrTableName)
    '            ' End If
    '        Next i
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        LogException(ex)
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function


    Public Function SaveData(ByVal dTable As DataTable, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim strSelect As String
            Dim StrTableName As String
            Dim SelectedColumns As String = ""
            StrTableName = dTable.TableName.ToString()
            Dim j As Integer = 0
            For j = 0 To dTable.Columns.Count - 1
                SelectedColumns = SelectedColumns & "," & dTable.Columns(j).ColumnName.ToString()
            Next j
            SelectedColumns = SelectedColumns.Substring(1)
            strSelect = "SELECT " + SelectedColumns & " FROM " & StrTableName & " WHERE 1=0"
            Dim dasave As New SqlDataAdapter(strSelect, con)
            dasave.SelectCommand.CommandTimeout = 0
            dasave.SelectCommand.Transaction = tran
            Dim cb As SqlCommandBuilder
            cb = New SqlCommandBuilder(dasave)
            dasave = cb.DataAdapter
            dasave.Update(dTable)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
#End Region

    Public Shared Function CheckIfBlank(ByVal strValue As String) As String
        Try
            If strValue <> Nothing AndAlso strValue <> String.Empty Then
                Return strValue
            Else
                Return "0"
            End If

        Catch ex As Exception
            LogException(ex)
            Return "0"
        End Try
    End Function

    Public Shared Function GetEnglishMonthNames(ByVal iMonth As Integer) As String
        Dim arrMonthNames() As String
        arrMonthNames = New String() {"JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"}
        Return arrMonthNames(iMonth - 1)

    End Function
    Public Function GetBrandWiseTenderDetailForTillClose(ByVal TerminalId As String, ByVal SiteCode As String, ByVal Dayopendate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UDP_GetBrandWiseTenderDetailForTillClose"
            cmd.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd.Connection = SpectrumCon()
            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd.Parameters.AddWithValue("@DayOpenDate", Dayopendate)
            cmd.Parameters.AddWithValue("@TerminalID", TerminalId)
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function AddColumnToDataTable(ByRef Table As DataTable, ByVal ColumnName As String, ByVal DataType As String, Optional ByVal DefaultValue As Object = Nothing) As Boolean
        Try
            Dim DC As DataColumn = New DataColumn(ColumnName, System.Type.GetType(DataType))
            If Not DefaultValue Is Nothing Then
                DC.DefaultValue = DefaultValue
            End If
            Table.Columns.Add(DC)
            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function DeleteColumnFromDataTable(ByRef table As DataTable, ByVal ColumnName As String) As Boolean
        Try
            table.Columns.Remove(ColumnName)
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Sub AddMode(ByRef ds As DataSet)
        Try
            For Each dt As DataTable In ds.Tables
                AddMode(dt)
            Next
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub AddMode(ByRef dt As DataTable)
        Try
            'Changed by Rohit for Issue No. 0003277 (Internal Mantis)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        dr.AcceptChanges()
                        dr.SetAdded()
                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'code is added by irfan on 13/04/2018 for membership
    Public Function SaveServiceArticleRemarks(ByVal cardno As String, ByVal programid As String, ByVal sitecode As String, ByVal remarks As String, ByVal CreatedOn As DateTime, ByVal CreatedBy As String, ByVal CreatedAt As String, ByVal UpdatedOn As DateTime, ByVal UpdatedBy As String, ByVal UpdatedAt As String) As Boolean
        Try
            Dim strquery As String = ""
            Dim status As Boolean = True
            strquery = "insert into ClpCustomerServiceArticleRemarks values('" & cardno & "','" & programid & "','" & sitecode & "','" & remarks & "',GETDATE(),'" & CreatedBy & "','" & CreatedAt & "',GETDATE(),'" & UpdatedBy & "','" & UpdatedAt & "','" & status & "')"
            OpenConnection()
            Dim sqlcom As New SqlCommand(strquery, SpectrumCon)
            '  CloseConnection()
            If sqlcom.ExecuteNonQuery > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'code is added by irfan on 13/04/2018 for membership
    Public Function DisplayReamrks(ByVal remarks As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim id As New DataTable
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = "select Remarks,CreatedOn from ClpCustomerServiceArticleRemarks where cardno='" & remarks & "' and status=1"
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            ' id = dt.Rows(0)("Remarks")
            Return dt
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'code is added by irfan on 13/04/2018 for membership
    Public Function GetCardforremarks(ByVal contact As String) As String
        Try
            Dim dt As New DataTable
            Dim id As String
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = "select CardNo from CLPCustomers where Mobileno='" & contact & "' and status=1"
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0)("CardNo")
                Return id
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'code is added by irfan on 04/04/2018 for hotel reservation
    Public Function GetRoomRateId(ByVal roomid As String, ByVal checkindate As Date) As String
        Try
            Dim dt As New DataTable
            Dim id As String
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = "select msr.mstStandardRoomRateID from Host_MstStandardRoomRate msr inner join Host_MstRoom mr on mr.mstSiteRoomTypeMap=msr.mstSiteRoomTypeMap where mr.mstRoomID='" & roomid & "' and msr.rateDate='" & checkindate & "'"
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0)("mstStandardRoomRateID")
                Return id
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'code is added by irfan on 04/04/2018 for hotel reservation
    Public Function GetProgramId(ByVal sitecode As String) As String
        Try
            Dim dt As New DataTable
            Dim ProgramId As String
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = "select ClpProgramId from CLPProgramSiteMap where Sitecode='" & sitecode & "' and STATUS=1"
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                ProgramId = dt.Rows(0)("ClpProgramId")
                Return ProgramId
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'Public Function getDocumentNo(ByVal obj As String) As String
    '    Try
    '        Dim DocNo As String = ""
    '        Dim dt As New DataTable
    '        Dim daDoc As New SqlDataAdapter("SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'", ConString)
    '        daDoc.Fill(dt)
    '        If dt.Rows.Count > 0 Then
    '            Dim objTypeId, objId As String
    '            objTypeId = dt.Rows(0)("OBJECTTYPEID")
    '            objId = dt.Rows(0)("OBJECTID")
    '            dt = New DataTable
    '            daDoc.SelectCommand.CommandText = "SELECT CURRENTNO FROM GLNORANGEOBJECTS WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "'"
    '            daDoc.Fill(dt)
    '            If dt.Rows.Count > 0 Then
    '                DocNo = dt.Rows(0)("CURRENTNO")
    '            End If
    '        End If
    '        Return DocNo
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return ""
    '    End Try
    'End Function
    Public Function getDocumentNo(ByVal obj As String, ByVal siteCode As String, Optional objType As String = "") As String
        Try
            Dim DocNo As String = ""
            Dim dt As New DataTable
            Dim sqlQuery = "SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'"
            If Not String.IsNullOrEmpty(objType) Then
                sqlQuery += " AND OBJECTTYPEID='" & objType & "'"
            End If
            Dim daDoc As New SqlDataAdapter(sqlQuery, ConString)
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                dt = New DataTable
                daDoc.SelectCommand.CommandText = "SELECT CURRENTNO FROM GLNORANGEOBJECTS WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & siteCode & "'"
                daDoc.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DocNo = dt.Rows(0)("CURRENTNO")
                End If
            End If
            Return DocNo
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function


    Public Function getDocumentNo(ByVal obj As String, ByVal siteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, Optional objType As String = "") As String
        Try
            Dim DocNo As String = ""
            Dim dt As New DataTable
            Dim sqlQuery = "SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'"
            If Not String.IsNullOrEmpty(objType) Then
                sqlQuery += " AND OBJECTTYPEID='" & objType & "'"
            End If
            Dim daDoc As New SqlDataAdapter(sqlQuery, con)
            daDoc.SelectCommand.Transaction = tran
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                dt = New DataTable
                daDoc.SelectCommand.CommandText = "SELECT CURRENTNO FROM GLNORANGEOBJECTS WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & siteCode & "'"
                daDoc.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DocNo = dt.Rows(0)("CURRENTNO")
                End If
            End If
            Return DocNo
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function



    Public Function getTableIdentityValueId(ByVal tableName As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Int64
        Try
            'Dim DocNo As String = ""
            'Dim dt As New DataTable
            'Dim daDoc As New SqlDataAdapter("SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'", ConString)
            'daDoc.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    Dim objTypeId, objId As String
            '    objTypeId = dt.Rows(0)("OBJECTTYPEID")
            '    objId = dt.Rows(0)("OBJECTID")
            '    dt = New DataTable
            '    daDoc.SelectCommand.CommandText = "SELECT CURRENTNO FROM GLNORANGEOBJECTS WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & siteCode & "'"
            '    daDoc.Fill(dt)
            '    If dt.Rows.Count > 0 Then
            '        DocNo = dt.Rows(0)("CURRENTNO")
            '    End If
            'End If
            'Return DocNo
            Dim cmd As New SqlCommand("Select IDENT_CURRENT('" & tableName & "')", con, tran)
            getTableIdentityValueId = cmd.ExecuteScalar()

        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function
    'code is added by irfan for hotel reservation on 05/04/2018
    Public Function GetCardExpiryDate(ByVal idNO As String) As DateTime
        Try
            Dim dt As New DataTable
            Dim id As DateTime
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = " select EndDate from MstCLPProgram    where CLPProgramid= '" & idNO & "' "
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                id = dt.Rows(0)("EndDate")
                Return id
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'code is added by irfan for hotel reservation on 05/04/2018
    Public Function IsCustomerExists(ByVal MobileNo As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim id As DateTime
            Dim strString As String = ""
            dt.Rows.Clear()
            strString = " select CardNo from clpcustomers where mobileno = '" & MobileNo & "' and STATUS=1"
            Dim da As New SqlDataAdapter(strString, SpectrumCon)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'code is added by irfan for hotel reservation on 04/04/2018
    Public Function updateGLNO(ByVal no As String) As Boolean
        Try
            Dim Tran As SqlTransaction = Nothing
            If UpdateDocumentNo("Customer Loyalty", SpectrumCon, Tran, no) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function UpdateDocumentNo(ByVal obj As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, Optional ByVal UpdateDocNo As String = "", Optional ByVal objType As String = "") As Boolean
        Try
            Dim dt As New DataTable
            Dim SqlQuery = "SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'"
            If Not String.IsNullOrEmpty(objType) Then
                SqlQuery += " AND OBJECTTYPEID='" & objType & "'"
            End If
            Dim daDoc As New SqlDataAdapter(SqlQuery, con)
            daDoc.SelectCommand.Transaction = tran
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                OpenConnection()
                If UpdateDocNo = String.Empty Then UpdateDocNo = "CONVERT(NUMERIC,CURRENTNO) +1"
                Dim cmd As New SqlCommand("UPDATE  GLNORANGEOBJECTS SET UPDATEDON=GETDATE(), CURRENTNO=" & UpdateDocNo & "WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "'", con, tran)
                If cmd.ExecuteNonQuery() > 0 Then
                    Return True
                End If
            End If

            Return False
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function UpdateStock(ByVal site As String, ByVal StrItemCode As String, ByVal Ean As String, ByVal UOM As String, _
    '                            ByVal BQty As Int32, ByVal userid As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, Optional ByVal vReservedQty As Integer = 0) As Boolean
    '    Try
    '        Dim cmdStock As New SqlCommand()
    '        cmdStock.Connection = con
    '        cmdStock.Transaction = tran
    '        SqlQry.Length = 0
    '        SqlQry.Append("SELECT A.ARTICLECODE,B.COMPONENTARTICLE,B.EAN,B.QUANTITY FROM MSTARTICLE A ")
    '        SqlQry.Append(" LEFT OUTER JOIN ARTICLECOMPONENT B ON A.ARTICLECODE=B.ARTICLECODE AND B.STATUS=1 ")
    '        SqlQry.Append(" WHERE A.ARTICLECODE='" & StrItemCode & "'")
    '        Dim da As New SqlDataAdapter(SqlQry.ToString, con)
    '        da.SelectCommand.Transaction = tran
    '        Dim dt As New DataTable
    '        da.Fill(dt)
    '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '            If dt.Rows.Count > 1 Then
    '                For Each dr As DataRow In dt.Rows
    '                    Dim KQty As Double = dr("QUANTITY") * BQty
    '                    Dim KEan As String = dr("EAN").ToString()
    '                    SqlQry.Length = 0
    '                    SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET BALANCEQTY=ISNULL(BALANCEQTY,0) + " & (KQty * -1) & vbCrLf)
    '                    If vReservedQty > 0 Then
    '                        SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQty & vbCrLf)
    '                    End If
    '                    SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
    '                    SqlQry.Append(" WHERE SITECODE='" & site & "'  AND EAN='" & KEan & "'" & vbCrLf)

    '                    cmdStock.CommandText = SqlQry.ToString
    '                    If cmdStock.ExecuteNonQuery() <= 0 Then
    '                        Return False
    '                    End If
    '                Next
    '                SqlQry.Length = 0
    '                SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET BALANCEQTY=ISNULL(BALANCEQTY,0) + " & (BQty * -1) & vbCrLf)
    '                If vReservedQty > 0 Then
    '                    SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQty & vbCrLf)
    '                End If
    '                SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
    '                SqlQry.Append(" WHERE SITECODE='" & site & "'  AND EAN='" & Ean & "'" & vbCrLf)

    '                cmdStock.CommandText = SqlQry.ToString
    '                If cmdStock.ExecuteNonQuery() > 0 Then
    '                    Return True
    '                End If
    '            Else
    '                SqlQry.Length = 0
    '                SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET BALANCEQTY=ISNULL(BALANCEQTY,0) + " & (BQty * -1) & vbCrLf)
    '                If vReservedQty > 0 Then
    '                    SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQty & vbCrLf)
    '                End If
    '                SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
    '                SqlQry.Append(" WHERE SITECODE='" & site & "'  AND EAN='" & Ean & "'" & vbCrLf)

    '                cmdStock.CommandText = SqlQry.ToString
    '                If cmdStock.ExecuteNonQuery() > 0 Then
    '                    Return True
    '                End If
    '            End If
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function UpdateStock(ByVal site As String, ByVal StrItemCode As String, ByVal Ean As String, ByVal UOM As String, _
       ByVal BQty As Decimal, ByVal userid As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal vStorageLocation As String, Optional ByVal vReservedQty As Integer = 0, Optional ByVal BillNo As String = "", Optional ByVal updateArt As Boolean = False, Optional ByVal SerVerDate As DateTime = Nothing, Optional ByVal Fyear As String = Nothing) As Boolean
        Try
            ' Get Default Ean for given Article
            'Dim S As String

            'Dim cmdDefault As New SqlCommand()
            'Dim strDefaulEan As String = "0"
            'cmdDefault.Connection = con
            'cmdDefault.Transaction = tran
            'cmdDefault.CommandText = "select dbo.FnGetDefaultEan('" & StrItemCode & "')"
            'S = StrItemCode & " time " & Now.ToString()
            'strDefaulEan = cmdDefault.ExecuteScalar()
            'S = S & " End Time : " & Now.ToString()
            'If strDefaulEan = "0" Then
            '    strDefaulEan = Ean
            'End If
            'End Default Ean for Give Article
            'Rohit
            'Dim finYear As String = Date.Today.Year
            Dim finYear As String = Fyear
            Dim cmdStock As New SqlCommand()
            cmdStock.Connection = con
            cmdStock.Transaction = tran
            SqlQry.Length = 0

            SqlQry.Append("SELECT A.ARTICLECODE,B.ARTICLECODE,B.EAN,B.QUANTITY,A.ARTICALTYPECODE,B.SaleUnitofMeasure,B.SellingPrice ")
            SqlQry.Append("FROM MSTARTICLE A LEFT JOIN MSTARTICLEKIT B ON A.ARTICLECODE=B.KITARTICLECODE AND B.STATUS=1 ")
            SqlQry.Append("WHERE A.ARTICLECODE='" & StrItemCode & "'")

            Dim da As New SqlDataAdapter(SqlQry.ToString, con)
            da.SelectCommand.Transaction = tran
            Dim dt As New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ' Changed by rama ranjan on 22-jun-2009 for not updating Stock of other article
                If dt.Rows(0)("ArticalTypeCode").ToString.ToLower <> "Single".ToLower AndAlso dt.Rows(0)("ArticalTypeCode").ToString.ToLower <> "Kit".ToLower Then                  'AndAlso dt.Rows(0)("ArticalTypeCode").ToString.ToLower <> "Combo".ToLower

                    Return True
                End If
                'end
                'If dt.Rows.Count > 1 Then

                Dim billLineNo As Integer = 1
                If updateArt Then
                    billLineNo = GetMaxBillLineNoKit(BillNo, site, con, tran)
                    cmdStock.Parameters.Add("@serverDate", SqlDbType.DateTime)
                    cmdStock.Parameters("@serverDate").Value = SerVerDate

                End If


                If dt.Rows(0)("ArticalTypeCode").ToString.ToUpper = "KIT" Then
                    For Each dr As DataRow In dt.Rows
                        '---added by prasad handling null value in case of article kit
                        Dim KQty As Double = IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * -1
                        Dim KEan As String = dr("EAN").ToString()
                        Dim strQty, strKEan As String
                        Dim vReservedQtykit As Decimal = 0

                        'strQty = ConvertToEnglish(KQty)
                        strQty = KQty
                        If (CultureInfo.CreateSpecificCulture("en-US").Equals(CultureInfo.CurrentUICulture)) Then
                            strKEan = KEan
                        Else
                            strKEan = ConvertToEnglish(KEan)
                        End If

                        vReservedQtykit = Val(KQty)

                        SqlQry.Length = 0
                        ' if the sales storage location is system then update the articlestockbalance table only
                        SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET TotalPhysicalSaleableQty=ISNULL(TotalPhysicalSaleableQty,0) + " & strQty & ", TotalSaleableQty=ISNULL(TotalSaleableQty,0) + " & strQty & vbCrLf)
                        If UCase(vStorageLocation) = "SYSTEM" Then
                            SqlQry.Append(",PhysicalQty=ISNULL(PhysicalQty,0) + " & strQty & vbCrLf)
                        End If
                        '''---- Changes By Mahesh Reserve Quantity deduct in KIT Cases ----
                        If Val(vReservedQtykit) <> 0 Then
                            SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQtykit & vbCrLf)
                        End If
                        SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
                        SqlQry.Append(" WHERE SITECODE='" & site & "' AND EAN='" & strKEan & "'" & vbCrLf)

                        ''insert kit child articles in cashmemokitdtl --Added By Mayur
                        '---added by prasad handling null value in case of article kit
                        If updateArt Then
                            SqlQry.Append(" ; insert into cashmemokitdtl values('" & site & "','" & finYear & "','" & BillNo & "'," & billLineNo & ",'" & dr("EAN").ToString() & "','" & dr("ARTICLECODE1").ToString() & "','" & dr("ARTICLECODE").ToString() & "'," & dr("SellingPrice").ToString() & "," & dr("SellingPrice").ToString() & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * dr("SellingPrice") & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * dr("SellingPrice") & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty & ",'" & dr("SaleUnitofMeasure").ToString() & "','" & site & "','" & userid & "',@serverDate,'" & site & "','" & userid & "',@serverDate,1)" & vbCrLf)
                        End If

                        ''
                        ' end of system storage location update
                        cmdStock.CommandText = SqlQry.ToString

                        If cmdStock.ExecuteNonQuery() <= 0 Then

                            MsgBox(getValueByKey("CLCOM01"), MsgBoxStyle.Critical, "CLCOM01 - " & getValueByKey("CLAE04"))

                            Return False
                        End If
                        ' This is to update other storage location stock
                        If UCase(vStorageLocation) <> "SYSTEM" Then
                            SqlQry.Length = 0
                            SqlQry.Append("UPDATE ARTICLESTOCKSTORAGEBALANCE SET QTY=ISNULL(QTY,0) + " & strQty & vbCrLf)

                            SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
                            SqlQry.Append(" WHERE SITECODE='" & site & "' AND STLOCCODE = '" & vStorageLocation & "' AND EAN='" & strKEan & "'" & vbCrLf)
                            cmdStock.CommandText = SqlQry.ToString
                            If cmdStock.ExecuteNonQuery() <= 0 Then

                                MsgBox(getValueByKey("CLCOM02"), MsgBoxStyle.Critical, getValueByKey("CLAE04"))

                                Return False
                            End If
                        End If
                        billLineNo = billLineNo + 1
                        ' End of other storage location stock
                    Next
                    'SqlQry.Length = 0
                    'SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET TotalPhysicalSaleableQty=ISNULL(TotalPhysicalSaleableQty,0) + " & (BQty * -1) & ",TotalSaleableQty=ISNULL(TotalSaleableQty,0) + " & (BQty * -1) & " , PhysicalQty=ISNULL(PhysicalQty,0) + " & (BQty * -1) & vbCrLf)
                    'If vReservedQty > 0 Then
                    ' SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQty & vbCrLf)
                    'End If
                    'SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
                    'SqlQry.Append(" WHERE SITECODE='" & site & "' AND EAN='" & Ean & "'" & vbCrLf)

                    'cmdStock.CommandText = SqlQry.ToString
                    'If cmdStock.ExecuteNonQuery() > 0 Then
                    ' Return True
                    'End If
                End If
            End If

            ' This is to update other storage location stock when storage location is not System
            If UCase(vStorageLocation) <> "SYSTEM" Then
                SqlQry.Length = 0
                SqlQry.Append("UPDATE ARTICLESTOCKSTORAGEBALANCE SET QTY=ISNULL(QTY,0) + " & (BQty * -1) & vbCrLf)

                SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
                SqlQry.Append("From ARTICLESTOCKSTORAGEBALANCE with (nolock) inner join Mstean with (nolock) On ARTICLESTOCKSTORAGEBALANCE.EAN=Mstean.EAN ")
                SqlQry.Append(" WHERE ARTICLESTOCKSTORAGEBALANCE.SITECODE='" & site & "' AND ARTICLESTOCKSTORAGEBALANCE.STLOCCODE = '" & vStorageLocation & "' AND ARTICLESTOCKSTORAGEBALANCE.ArticleCode='" & StrItemCode & "' AND ARTICLESTOCKSTORAGEBALANCE.EAN='" & Ean & "' " & vbCrLf)

                cmdStock.CommandText = SqlQry.ToString
                If cmdStock.ExecuteNonQuery() <= 0 Then

                    MsgBox(getValueByKey("CLCOM02"), MsgBoxStyle.Critical, "CLCOM02 - " & getValueByKey("CLAE05"))

                    Return False
                End If
            End If
            ' End of other storage location stock

            SqlQry.Length = 0
            SqlQry.Append("UPDATE ARTICLESTOCKBALANCES SET TotalPhysicalSaleableQty=ISNULL(TotalPhysicalSaleableQty,0) + " & (BQty * -1) & ", TotalSaleableQty=ISNULL(TotalSaleableQty,0) + " & (BQty * -1) & vbCrLf)

            If UCase(vStorageLocation) = "SYSTEM" Then
                SqlQry.Append(",PhysicalQty=ISNULL(PhysicalQty,0) + " & (BQty * -1) & vbCrLf)
            End If

            'If vReservedQty > 0 Then
            SqlQry.Append(" , RESERVEDQTY= ISNULL(RESERVEDQTY,0) + " & vReservedQty & vbCrLf)
            'End If
            SqlQry.Append(" , UPDATEDAT = '" & site & "',UPDATEDBY = '" & userid & "', UPDATEDON = GETDATE()" & vbCrLf)
            SqlQry.Append(" From ARTICLESTOCKBALANCES with (nolock) ")
            SqlQry.Append(" WHERE ARTICLESTOCKBALANCES.SITECODE='" & site & "' AND ARTICLESTOCKBALANCES.ArticleCode='" & StrItemCode & "' AND ARTICLESTOCKBALANCES.EAN='" & Ean & "' " & vbCrLf)

            cmdStock.CommandText = SqlQry.ToString
            If cmdStock.ExecuteNonQuery() > 0 Then

                Return True
            End If

            Return False
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateKitArticle(ByVal site As String, ByVal StrItemCode As String, ByVal Ean As String, ByVal UOM As String, _
 ByVal BQty As Decimal, ByVal userid As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal vStorageLocation As String, Optional ByVal vReservedQty As Integer = 0, Optional ByVal BillNo As String = "", Optional ByVal updateArt As Boolean = False, Optional ByVal SerVerDate As DateTime = Nothing, Optional ByVal Fyear As String = Nothing) As Boolean
        Try

            Dim finYear As String = Fyear
            Dim cmdStock As New SqlCommand()
            cmdStock.Connection = con
            cmdStock.Transaction = tran
            SqlQry.Length = 0
            SqlQry.Append("SELECT A.ARTICLECODE,B.ARTICLECODE,B.EAN,B.QUANTITY,A.ARTICALTYPECODE,B.SaleUnitofMeasure,B.SellingPrice ")
            SqlQry.Append("FROM MSTARTICLE A LEFT JOIN MSTARTICLEKIT B ON A.ARTICLECODE=B.KITARTICLECODE AND B.STATUS=1 ")
            SqlQry.Append("WHERE A.ARTICLECODE='" & StrItemCode & "'")

            Dim da As New SqlDataAdapter(SqlQry.ToString, con)
            da.SelectCommand.Transaction = tran
            Dim dt As New DataTable
            da.Fill(dt)

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)("ArticalTypeCode") <> "Kit" Then
                    Return True
                End If

                Dim billLineNo As Integer = 1
                If updateArt Then
                    billLineNo = GetMaxBillLineNoKit(BillNo, site, con, tran)
                    cmdStock.Parameters.Add("@serverDate", SqlDbType.DateTime)
                    cmdStock.Parameters("@serverDate").Value = SerVerDate
                End If

                If dt.Rows(0)("ArticalTypeCode").ToString.ToUpper = "KIT" Then
                    For Each dr As DataRow In dt.Rows
                        Dim KQty As Double = IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * -1
                        Dim KEan As String = dr("EAN").ToString()
                        Dim strQty, strKEan As String
                        Dim vReservedQtykit As Decimal = 0
                        strQty = KQty
                        If (CultureInfo.CreateSpecificCulture("en-US").Equals(CultureInfo.CurrentUICulture)) Then
                            strKEan = KEan
                        Else
                            strKEan = ConvertToEnglish(KEan)
                        End If
                        vReservedQtykit = Val(KQty)

                        SqlQry.Length = 0
                        If updateArt Then
                            SqlQry.Append(" insert into cashmemokitdtl values('" & site & "','" & finYear & "','" & BillNo & "'," & billLineNo & ",'" & dr("EAN").ToString() & "','" & dr("ARTICLECODE1").ToString() & "','" & dr("ARTICLECODE").ToString() & "'," & dr("SellingPrice").ToString() & "," & dr("SellingPrice").ToString() & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * dr("SellingPrice") & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty * dr("SellingPrice") & "," & IIf(dr("QUANTITY") Is DBNull.Value, 0, dr("QUANTITY")) * BQty & ",'" & dr("SaleUnitofMeasure").ToString() & "','" & site & "','" & userid & "',@serverDate,'" & site & "','" & userid & "',@serverDate,1)" & vbCrLf)
                        End If
                        cmdStock.CommandText = SqlQry.ToString
                        If cmdStock.ExecuteNonQuery() <= 0 Then
                            MsgBox(getValueByKey("CLCOM01"), MsgBoxStyle.Critical, "CLCOM01 - " & getValueByKey("CLAE04"))
                            Return False
                        End If
                        billLineNo = billLineNo + 1
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GenerateSaleOrderSTR(ByVal saleOrderNumber As String, ByVal SiteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            GenerateSaleOrderSTR = False
            Dim cmdSTR As New SqlCommand("UDP_GenerateSaleOrderSTR", con, tran)
            cmdSTR.CommandType = CommandType.StoredProcedure

            cmdSTR.Parameters.AddWithValue("@saleOrderNumber", saleOrderNumber)
            cmdSTR.Parameters.AddWithValue("@siteCode", SiteCode)

            cmdSTR.ExecuteNonQuery()
            GenerateSaleOrderSTR = True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPerformanceQuery() As Boolean
        Try
            Using cmd As New SqlCommand("UDP_PerformanceQuery", SpectrumCon)
                OpenConnection()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
                CloseConnection()
            End Using
            Return True
        Catch ex As Exception
            LogException(ex)
            'CloseConnection()
            Return Nothing
        End Try
    End Function
    Public Function UpdateStrData(ByVal SaleOrderNo As String, ByVal siteCode As String, ByVal STRlist As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean


        Try

            Dim strString As String = ""

            strString = "UPDATE SalesOrderStrDetails SET strnumber='' where strnumber in (select  * from dbo.fnSplitString('" & STRlist & "',',')) and SaleOrderNumber ='" & SaleOrderNo & "' and   SiteCode='" & siteCode & "' and Status=1;"

            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)
            If cmd.ExecuteNonQuery() > 0 Then

                Return True
            End If

            Return True

        Catch ex As Exception


            LogException(ex)
            Return False
        End Try




    End Function
    Public Function GenerateSaleOrderSTRNew(ByVal saleOrderNumber As String, ByVal SiteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As DataSet
        Dim dsStr As DataSet = Nothing
        Try

            Dim cmdSTR As New SqlCommand("UDP_GENERATESALEORDERSTR_NEW", con, tran)
            cmdSTR.CommandType = CommandType.StoredProcedure

            cmdSTR.Parameters.AddWithValue("@saleOrderNumber", saleOrderNumber)
            cmdSTR.Parameters.AddWithValue("@siteCode", SiteCode)

            Dim da As New SqlDataAdapter(cmdSTR)
            dsStr = New DataSet()
            da.Fill(dsStr)
            Return dsStr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '' added by ketan vaidya Update STR Factory Remark
    Public Function UpdateSTRRemark(ByVal SaleOrderNo As String, ByVal siteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim strString As String = ""
            strString = "UPDATE OD SET od.Remarks =strRemark.remark FROM  OrderHdr OD INNER JOIN SalesOrderSTRFactoryRemark AS strRemark ON  OD.SiteCode = strRemark.FactorySiteCode AND SaleOrderNumber='" & SaleOrderNo & "' AND OD.ReferenceNo = '" & SaleOrderNo & "' " & vbCrLf
            Dim cmdSTR As New SqlCommand(strString, con, tran)
            If cmdSTR.ExecuteNonQuery() > 0 Then
                Return True
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetDayCloseReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date) As DataSet
        Dim dsDayclose As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GetDayCloseReportData", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@V_DayCloseDate", DayCloseDate.Date)
            sqlComm.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter(sqlComm)
            dsDayclose = New DataSet()
            da.Fill(dsDayclose)
            Return dsDayclose
        Catch ex As Exception
            LogException(ex)
            Return dsDayclose
        End Try
    End Function

    Public Function GetDayCloseSyncStatusByDate(ByVal syncType As String, ByVal siteCode As String, ByVal DayOpenDate As DateTime, ByRef LastSyncTym As DateTime) As String
        Dim strResult As String = ""
        Try
            Dim dsDayClose As DataTable


            Dim sqlComm As New SqlCommand("udp_dayclosesuccess", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@P_syntype", syncType)
            sqlComm.Parameters.AddWithValue("@P_sitecode", siteCode)
            sqlComm.Parameters.AddWithValue("@P_dayopendate", DayOpenDate.Date)
            sqlComm.Parameters.AddWithValue("@P_lastsynctime", LastSyncTym)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            If dsDayClose IsNot Nothing Then
                If dsDayClose.Rows.Count > 0 Then
                    strResult = dsDayClose.Rows(0)(0).ToString()
                    LastSyncTym = dsDayClose.Rows(0)(1)
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function

    Public Function UpdateDayCloseStaus(ByVal siteCode As String, ByVal DayOpenDate As DateTime, ByVal IsScussee As Boolean) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strString As String = ""
            If IsScussee Then
                strString = "UPDATE DAYOPENNCLOSE SET DAYCLOSESTATUS = 1 ,UPDATEDON=GETDATE()  WHERE OPENDATE=@DayOpenDate and SiteCode='" & siteCode & "'"
            Else
                strString = "UPDATE DAYOPENNCLOSE SET DAYCLOSESTATUS = 0 ,UPDATEDON=GETDATE()  WHERE OPENDATE=@DayOpenDate and SiteCode='" & siteCode & "'"

            End If
            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)

            cmd.Parameters.Add("@DayOpenDate", SqlDbType.DateTime)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            If cmd.ExecuteNonQuery() > 0 Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            Return False

        Catch ex As Exception

            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetRemoteIpPort() As DataTable
        Try
            'modified by khusrao adil on 12-10-2017 for jk sprint 30
            'Dim strString As String = "select FldLabel ,FldValue  from DefaultConfig  where FldLabel in('WebService.Remote.IP','WebService.Remote.PORT','SYNCH_SERVER_LOCAL_PORT') and Sitecode='BOCommon'"
            Dim strString As String = "select FldLabel ,FldValue  from DefaultConfig  where FldLabel in('WebService.Remote.IP','WebService.Remote.PORT','SYNCH_SERVER_LOCAL_PORT','STOCK_TECH_SYNCH_SERVER_LOCAL_PORT') and Sitecode='BOCommon'"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetBoLevelChanges() As Boolean
        Try
            Dim strString As String = "select FldValue  from defaultconfig  where FldLabel='bo.so.new.changes'"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("FldValue") = True Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetLevels() As DataTable
        Try
            Dim strString As String = " select Code,ShortDesc from GEneralCodeMSt where CodeType='LevelType'"
            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(strString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)
            Return dtCustClass
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added by Vipul for membership
    Public Function GetClpProgramType(ByVal sitecode As String) As DataTable
        Try
            Dim strString As String = "select TypeKey,TypeValue from MstCLPType where typeid='Card' AND STATUS=1 "

            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(strString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)
            Return dtCustClass
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMembershipDetails(ByVal isEnquiry As Integer, ByVal Condition As String) As DataTable
        Try
            Dim strString As String = "select BillNo,CardNo,PeriodID,SrNo,EAN,Amount,isConvertedToMember,StartDate,EndDate,DiscountAmount,discountInPercentage,CreatedBy,CreatedOn  from ClpCustomerServiceArticlePeriodMap where isEnquiry=" & isEnquiry & " AND STATUS=1"
            If Not String.IsNullOrEmpty(Condition) Then
                strString = strString + Condition
            End If

            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(strString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)
            Return dtCustClass
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code is added by irfan on 13/4/2018 for membership.
    Public Function GetCardNo(ByVal cardno As String) As String
        Try
            Dim strString As String = "select CardNo from CLPCustomers where Mobileno ='" & cardno & "' AND STATUS=1"

            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(strString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)
            If dtCustClass.Rows.Count > 0 Then
                Return dtCustClass.Rows(0)("CardNo")
            Else
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSalesPerson(ByVal sitecode As String, Optional ByVal vEmpCode As String = "") As DataTable
        Try
            Dim QrySalesPerson As String = "SELECT MSP.Empcode, MSP.SalesPersonName " & _
                                           "FROM AuthUsers AS AU INNER JOIN  MstSalesPerson AS MSP ON AU.UserID = MSP.Empcode AND AU.SiteCode = MSP.SiteCode " & _
                                           "WHERE (AU.Active ='A') AND (AU.STATUS = 1) AND (MSP.STATUS = 1) AND (AU.Sitecode='" & sitecode & "') "
            If Not (vEmpCode = String.Empty) Then
                QrySalesPerson = QrySalesPerson & " And MSP.EmpCode ='" & vEmpCode & "'"
            End If
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(QrySalesPerson, ConString)
            da.Fill(dt)
            If Not dt Is Nothing Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSQlData(ByVal SQLQuery As String) As DataSet
        Try

            Dim dt As New DataSet
            Dim da As New SqlDataAdapter(SQLQuery, ConString)
            da.Fill(dt)
            If Not dt Is Nothing Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetFilledTable(ByVal strQuery As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetFilledTableByCommand(ByVal cmd As SqlCommand) As DataTable
        Try
            Dim dt As New DataTable
            Dim dtt As New SqlDataAdapter(cmd)
            dtt.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function InsertOrUpdateRecord(ByVal Query As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Try
        OpenConnection()
        Dim cmd As New SqlCommand
        cmd.CommandText = Query
        cmd.Connection = SpectrumCon()
        If trans IsNot Nothing Then
            cmd.Transaction = trans
        End If
        cmd.ExecuteNonQuery()
        Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

    Public Function InsertOrUpdateRecordForDinein(ByVal Query As String, ByVal dateopendate As Date, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Try
        OpenConnection()
        Dim cmd As New SqlCommand
        cmd.CommandText = Query
        cmd.Parameters.Add("@DayOpenDate", SqlDbType.DateTime)
        cmd.Parameters("@DayOpenDate").Value = dateopendate
        cmd.Connection = SpectrumCon()
        If trans IsNot Nothing Then
            cmd.Transaction = trans
        End If
        cmd.ExecuteNonQuery()
        Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

    Public Function IsCheckListAvailabl() As Boolean
        Try
            Dim strString As String = "SELECT CheckListId,SRNo,Questions,TypeofValue from CheckListMaster WHERE  Status=1  "
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCheckListDetails() As DataTable
        Try
            Dim strString As String = "SELECT CheckListId,SRNo,Questions,TypeofValue from CheckListMaster WHERE  Status=1  "
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsCheckListFilled(ByVal Terminal As String, ByVal SiteCode As String) As Boolean
        Try
            Dim DATemp As New SqlDataAdapter("SELECT max(Convert(datetime,Convert(varchar(12),CheckListOpenTime,100))) as CheckListOpenTime, Convert(datetime,Convert(varchar(12),getdate(),100)) as CurrentDate FROM CheckListHdr " & _
            "WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "'", SpectrumCon)
            OpenConnection()
            Dim DTFloatingDetail As New DataTable
            DATemp.Fill(DTFloatingDetail)
            If DTFloatingDetail.Rows.Count > 0 Then
                If String.IsNullOrEmpty(DTFloatingDetail.Rows(0).Item("CheckListOpenTime").ToString()) Then
                    Return False
                End If

                If DTFloatingDetail.Rows(0).Item("CheckListOpenTime").ToShortDateString() = DTFloatingDetail.Rows(0).Item("CurrentDate").ToShortDateString() Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function SaveChecklistData(ByVal CheckListId As String, ByVal SiteCode As String, ByVal UserId As String, ByVal TerminalId As String, ByVal CheckListOpenTime As DateTime, ByVal DocumentNumber As String, ByVal chkDtl As DataTable) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strCkeckListHdrString As String = ""
            strCkeckListHdrString = " Insert into CheckListHdr(CheckListId,SiteCode,DocumentNumber,CheckListOpenTime,CheckListCloseTime,TerminalID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) "
            strCkeckListHdrString = strCkeckListHdrString & " values ('" & CheckListId & "','" & SiteCode & "','" & DocumentNumber & "',@CheckListOpenTime,GetDate(),'" & TerminalId & "','" & SiteCode & "','" & UserId & "',GetDate(),'" & SiteCode & "','" & UserId & "',GetDate(),1) "
            ' Dim cmd As New SqlCommand(strCkeckListHdrString, con, tran)
            Dim cmd As New SqlCommand(strCkeckListHdrString, SpectrumCon, tran)
            cmd.Parameters.Add("@CheckListOpenTime", SqlDbType.DateTime)
            cmd.Parameters("@CheckListOpenTime").Value = CheckListOpenTime
            If cmd.ExecuteNonQuery() > 0 Then
                For i As Integer = 0 To chkDtl.Rows.Count - 1
                    Dim strCkeckListDtlString As String = ""
                    strCkeckListDtlString = " Insert into CheckListDtl(CheckListId,SrNo,SiteCode,DocumentNumber,Question,Answer,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) "
                    strCkeckListDtlString = strCkeckListDtlString & " values ('" & chkDtl.Rows(i).Item("CheckListId").ToString() & "','" & chkDtl.Rows(i).Item("SrNo").ToString() & "','" & SiteCode & "','" & DocumentNumber & "','" & chkDtl.Rows(i).Item("Question").ToString() & "','" & chkDtl.Rows(i).Item("Answer").ToString() & "','" & SiteCode & "','" & UserId & "',GetDate(),'" & SiteCode & "','" & UserId & "',GetDate(),1) "
                    Dim cmdDtl As New SqlCommand(strCkeckListDtlString, SpectrumCon, tran)
                    cmdDtl.ExecuteNonQuery()
                Next
            End If


            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try





    End Function
    Public Function GetReasons(ByVal DocType As String)
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT REASONCODE,REASONNAME,TRNSEQUENCENAME FROM REASONS WHERE DOCTYPE='" & DocType & "'", ConString)
            da.Fill(dt)
            If Not dt Is Nothing Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function FnGiftVoucherValidate(ByVal VoucherNo As String, Optional ByRef errmsg As String = "", Optional ByRef VoucherAmt As Double = 0, Optional ByRef VoucherExpiryDate As DateTime = Nothing, Optional ByVal OnlyIssueCheck As Boolean = False, Optional ByVal VoucherCode As String = "", Optional ByVal SiteCode As String = "", Optional ByRef valueOfVoucher As String = "", Optional ByRef IsGvHasExpiry As Boolean = True) As Boolean
        Try
            Dim dt As New DataTable
            Dim daSql As New SqlDataAdapter("SELECT * FROM VOUCHERDTLS WHERE ISACTIVE=1  AND VOURCHERSERIALNBR='" & VoucherNo & "'", ConString)
            If Not String.IsNullOrEmpty(SiteCode) Then
                daSql.SelectCommand.CommandText = daSql.SelectCommand.CommandText & " AND SiteCode='" & SiteCode & "'"
            End If
            If VoucherCode <> String.Empty Then
                daSql.SelectCommand.CommandText = daSql.SelectCommand.CommandText & " AND VoucherCode='" & VoucherCode & "'"
            End If
            daSql.Fill(dt)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then

                    valueOfVoucher = IIf(dt.Rows(0)("ValueOfVoucher") IsNot DBNull.Value, dt.Rows(0)("ValueOfVoucher"), String.Empty)

                    If OnlyIssueCheck = False Then
                        If IsDBNull(dt.Rows(0)("ISIssued")) = True And dt.Rows(0)("ISIssued") = False Then
                            errmsg = "Gift Voucher is not issued"
                            Return False
                        End If
                        If IsDBNull(dt.Rows(0)("ExpiryDate")) = False Then
                            If dt.Rows(0)("ExpiryDate") < GetCurrentDate() Then
                                VoucherAmt = dt.Rows(0)("ValueofVoucher")
                                errmsg = "Voucher is Already Expired"
                                Return False
                            End If
                        Else
                            IsGvHasExpiry = False
                        End If
                        If IsDBNull(dt.Rows(0)("ISREDEEMED")) = True Then
                            VoucherAmt = dt.Rows(0)("ValueofVoucher")
                            If IsDBNull(dt.Rows(0)("ExpiryDate")) = False Then
                                VoucherExpiryDate = dt.Rows(0)("ExpiryDate")
                            End If
                            Return True
                        Else
                            If dt.Rows(0)("ISREDEEMED") = True Then
                                errmsg = "Gift Voucher is Already Redeemed"
                                Return False
                            Else

                                VoucherAmt = dt.Rows(0)("ValueofVoucher")
                                If IsDBNull(dt.Rows(0)("ExpiryDate")) = False Then
                                    VoucherExpiryDate = dt.Rows(0)("ExpiryDate")
                                End If
                                Return True
                            End If
                        End If
                    Else
                        If IsDBNull(dt.Rows(0)("ISIssued")) = True Then
                            Return True
                        End If
                        If IsDBNull(dt.Rows(0)("ISIssued")) = False AndAlso dt.Rows(0)("ISIssued") = True Then
                            errmsg = "Gift Voucher is already issued"
                            Return False
                        ElseIf Not dt.Rows(0)("ISREDEEMED") Is DBNull.Value AndAlso dt.Rows(0)("ISREDEEMED") = True Then
                            errmsg = "Gift Voucher is Already Redeemed"
                            Return False
                        ElseIf dt.Rows(0)("ISREDEEMED") Is DBNull.Value Or IsDBNull(dt.Rows(0)("ISREDEEMED")) = False Then
                            Return True
                        End If
                    End If
                Else
                    errmsg = "Gift Voucher Number is not valid"
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetStocks(ByVal strSiteCode As String, ByVal strEAN As String, ByVal strArticleCode As String, ByVal ReservedQtyAllowed As Boolean, Optional ByVal isBatchManagementReq As Boolean = False, Optional ByVal batchBarcode As String = "") As Double
        Try
            Dim StockQty As Double
            Dim sbQuery As New StringBuilder
            If String.IsNullOrEmpty(batchBarcode) = False Then
                'query = "select (ISNULL(TOTALPHYSICALSALEABLEQTY,0) -(ISNULL(RESERVEDQTY,0) + ISNULL(TOTALPHYSICALNONSALEABLEQTY,0))) as AvailabelQty from dbo.ArticleBatchBinStockBalances where SiteCode='" & strSiteCode & "' and BatchBarcode='" & batchBarcode & "'"

                sbQuery.Append("SELECT (ISNULL(TOTALPHYSICALSALEABLEQTY,0) " & vbCrLf)
                If (ReservedQtyAllowed) Then
                    sbQuery.Append("-(ISNULL(RESERVEDQTY,0) + ISNULL(TOTALPHYSICALNONSALEABLEQTY,0)) " & vbCrLf)
                End If
                sbQuery.Append(") AS AvailabelQty FROM ArticleBatchBinStockBalances " & vbCrLf)
                sbQuery.Append("WHERE SITECODE='" & strSiteCode & "' AND BatchBarcode = '" & batchBarcode & "' " & vbCrLf)
            Else
                'query = "SELECT (ISNULL(TOTALPHYSICALSALEABLEQTY,0) -(ISNULL(RESERVEDQTY,0) + ISNULL(TOTALPHYSICALNONSALEABLEQTY,0))) as AvailabelQty FROM ARTICLESTOCKBALANCES WHERE SITECODE='" & strSiteCode & "' AND Ean='" & strEAN & "' and ArticleCode='" & strArticleCode & "' "

                sbQuery.Append("SELECT (ISNULL(TOTALPHYSICALSALEABLEQTY,0) " & vbCrLf)
                If (ReservedQtyAllowed) Then
                    sbQuery.Append("-(ISNULL(RESERVEDQTY,0) + ISNULL(TOTALPHYSICALNONSALEABLEQTY,0)) " & vbCrLf)
                End If
                sbQuery.Append(") AS AvailabelQty FROM ARTICLESTOCKBALANCES " & vbCrLf)
                sbQuery.Append("WHERE SITECODE = '" & strSiteCode & "' AND Ean = '" & strEAN & "' " & vbCrLf)
                sbQuery.Append("AND ArticleCode = '" & strArticleCode & "' " & vbCrLf)

            End If
            Dim cmdTrn As New SqlCommand(sbQuery.ToString(), SpectrumCon)
            OpenConnection()
            StockQty = cmdTrn.ExecuteScalar()
            CloseConnection()
            Return StockQty
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function
    ''' <summary>
    ''' TODO: Get article image
    ''' </summary>
    ''' <param name="strArticleCode">ArticleCode</param>
    ''' <returns>Image URL</returns>
    ''' <remarks></remarks>
    Public Function GetArticleImage(ByVal strArticleCode As String, ByVal strImagePath As String) As String
        Dim drStrimageUrl As SqlDataReader
        Dim StrimageUrl As String = ""
        Try
            OpenConnection()
            Dim sqlCmdImageUrl As New SqlCommand("select ArticleImage from MstArticleImage where ArticleCode='" & strArticleCode & "'", SpectrumCon)
            drStrimageUrl = sqlCmdImageUrl.ExecuteReader()
            If (drStrimageUrl.Read()) Then
                If Not (drStrimageUrl.IsDBNull(0)) Then
                    StrimageUrl = drStrimageUrl.GetString(0)
                End If
            End If
            strImagePath = strImagePath.Replace("\\", "\")
            Return strImagePath & "\" & StrimageUrl
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        Finally
            drStrimageUrl.Close()
            CloseConnection()
        End Try
    End Function
    Public Function GetArticleImagepath() As String
        Dim drStrimageUrl As SqlDataReader
        Dim StrimageUrl As String = ""
        Try
            OpenConnection()
            Dim sqlCmdImageUrl As New SqlCommand("select fldvalue from defaultconfig where fldlabel='IMAGE_LOCATION_DIR'", SpectrumCon)
            drStrimageUrl = sqlCmdImageUrl.ExecuteReader()
            If (drStrimageUrl.Read()) Then
                If Not (drStrimageUrl.IsDBNull(0)) Then
                    StrimageUrl = drStrimageUrl.GetString(0)
                End If
            End If
            Return StrimageUrl
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        Finally
            drStrimageUrl.Close()
            CloseConnection()
        End Try
    End Function
    Public Function IsDayCloseSnycProceed(ByVal siteCode As String) As Boolean
        Dim dr As SqlDataReader
        Dim stringVal As String

        Try
            OpenConnection()
            Dim sqlCmd As New SqlCommand("select fldvalue from defaultconfig where fldlabel='SyncProgressCheck' and sitecode='" & siteCode & "'", SpectrumCon)
            dr = sqlCmd.ExecuteReader()
            If (dr.Read()) Then
                If Not (dr.IsDBNull(0)) Then
                    stringVal = dr.GetString(0)
                    If stringVal.ToUpper() = "TRUE" Then
                        dr.Close()
                        Return True
                    ElseIf stringVal.ToUpper() = "FALSE" Then
                        dr.Close()
                        Return False
                    End If
                Else
                    dr.Close()
                    Return True
                End If
            Else
                dr.Close()
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return True
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function IsDayCloseSnycProceed(ByVal syncType As String, ByVal siteCode As String) As Boolean
        Try
            Dim dsDayClose As DataTable
            Dim sqlComm As New SqlCommand("UDP_DayCloseSyncInProgress", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@P_syntype", syncType)
            sqlComm.Parameters.AddWithValue("@P_sitecode", siteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            If dsDayClose IsNot Nothing Then
                If dsDayClose.Rows.Count > 0 Then

                    If dsDayClose.Rows(0)(0).ToString() = "1" Then
                        IsDayCloseSnycProceed = True
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetVoucherStru() As DataTable
        Try
            Dim strQuery As String
            strQuery = "SELECT '' as Sel, SITECODE,VOUCHERCODE,'' as VOUCHERDESC,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE,ISISSUED," & _
                       "ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,CREATEDAT,CREATEDBY,CREATEDON," & _
                       "UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,0 as Quantity,convert(bit,0) as  ISPREPRINTED,0.0 as NetAmount,ExpiryDate,0 as ExpiryInDays FROM VOUCHERDTLS  Where 1=0"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
    Public Function GetInnovitiStruc() As DataTable
        Try
            Dim strQuery As String
            strQuery = "select  '0' as TenderSequenceLineNo ,'0' as 'LineNo','0' as TenderType,'0' as BillNo,'0' as AmountTendered,'0' as CardNo,'0' as RetrievalReferenceNumber,'false' as InnvoitiApplicable"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDenomination(ByVal CurrencyCode As String, Optional ByVal BaseCurrency As String = "") As DataTable
        Try
            Dim StrQuery, ExcangeRate As String
            Dim dtdenom As New DataTable
            Dim daDenom As SqlDataAdapter
            ExcangeRate = ""
            If BaseCurrency = CurrencyCode Then
                ExcangeRate = "1"
            ElseIf BaseCurrency <> String.Empty Then
                StrQuery = "SELECT EXCHANGERATE FROM MSTCURRENCYRATE WHERE Status=1 AND CURRENCYCODE='" & BaseCurrency & "' AND RELATIONALCURRENCY='" & CurrencyCode & "'"
                daDenom = New SqlDataAdapter(StrQuery, ConString)
                daDenom.Fill(dtdenom)
                If dtdenom.Rows.Count > 0 Then
                    ExcangeRate = dtdenom.Rows(0)("EXCHANGERATE")
                End If
            End If
            StrQuery = ""
            'Changed By Sameer for issue id 6891 on 4-4-2013
            StrQuery = "SELECT 	A.DENOMINATIONAMT,B.CURRENCYSYMBOL, DenominationDesc AS DENOMINATION,0 AS QTY,"
            If ExcangeRate <> String.Empty Then
                StrQuery = StrQuery & "convert(numeric(18,4)," & ExcangeRate & ") AS EXCHANGERATE,0.0 AS BASEAMOUNT,"
            End If
            StrQuery = StrQuery & "0.0 AS AMOUNT,B.CURRENCYCODE  FROM DENOMINATIONDETAIL A INNER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE where b.CurrencyCode='" & CurrencyCode & "' And A.Status = 1" & _
                        "ORDER BY A.DENOMINATIONAMT"
            daDenom = New SqlDataAdapter(StrQuery, ConString)
            daDenom.Fill(dtdenom)
            ' dtdenom.Columns("DENOMINATION").Expression = "CURRENCYSYMBOL +  + DENOMINATIONAMT"
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPreviusTerminalData(ByVal CurrencyCode As String, ByVal dayopendate As DateTime) As DataTable
        Try
            Dim StrQuery As String
            Dim dtdenom As New DataTable
            Dim ConectionString As New SqlConnection(ConString)
            StrQuery = "SELECT  A.DenominationAmt, B.CurrencySymbol, A.DenominationDesc AS DENOMINATION, ISNULL(f.Qty,0) AS QTY,isnull(TotalAmount,0) AS AMOUNT, B.CurrencyCode  FROM    DenominationDetail AS A LEFT JOIN MstCurrency AS B ON A.CurrencyCode = B.CurrencyCode LEFT JOIN   FloatingDetail AS f ON A.CurrencyCode = f.CurrencyCode AND A.DENOMINATIONAMT = f.DENOMINATIONAMT   AND (f.Action = 'extraopen')   AND f.FlotDatetime =  @flotdate WHERE (B.CurrencyCode = '" & CurrencyCode & "') AND (A.STATUS = 1)  "
            Dim cmd As New SqlCommand(StrQuery, ConectionString)
            cmd.CommandText = StrQuery.ToString
            cmd.Parameters.AddWithValue("@flotdate", dayopendate)
            Using daDenom As New SqlDataAdapter()

                daDenom.SelectCommand = cmd
                daDenom.Fill(dtdenom)
            End Using
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetPreviusShiftData(ByVal CurrencyCode As String, ByVal shiftid As String, ByVal opendate As DateTime, ByVal terminalid As String) As DataTable
        Try
            ActivityLogForShift(Nothing, String.Format("GetPreviusShiftData() : CurrencyCode : {0},shiftid : {1},opendate : {2},terminalid : {3}", CurrencyCode, shiftid, opendate, terminalid), shiftid)
            Dim StrQuery As String
            Dim dtdenom As New DataTable
            Dim ConectionString As New SqlConnection(ConString)
            StrQuery = "SELECT  A.DenominationAmt, B.CurrencySymbol, A.DenominationDesc AS DENOMINATION, ISNULL(f.Qty,0) AS QTY,isnull(TotalAmount,0) AS AMOUNT, B.CurrencyCode  FROM    DenominationDetail AS A LEFT JOIN MstCurrency AS B ON A.CurrencyCode = B.CurrencyCode LEFT JOIN   FloatingDetail AS f ON A.CurrencyCode = f.CurrencyCode AND A.DENOMINATIONAMT = f.DENOMINATIONAMT   AND (f.Action = 'extraopen') and f.shiftid = '" & shiftid & "'  AND f.FlotDatetime = @flotdate and f.terminalid ='" & terminalid & "'   WHERE (B.CurrencyCode = '" & CurrencyCode & "') AND (A.STATUS = 1)  "
            Dim cmd As New SqlCommand(StrQuery, ConectionString)
            cmd.CommandText = StrQuery.ToString
            cmd.Parameters.AddWithValue("@flotdate", opendate)
            Using daDenom As New SqlDataAdapter()
                daDenom.SelectCommand = cmd
                daDenom.Fill(dtdenom)
            End Using
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCurrencyDenomination() As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT DISTINCT A.CURRENCYCODE,A.CURRENCYDESCRIPTION FROM MSTCURRENCY A INNER JOIN  DENOMINATIONDETAIL B ON A.CURRENCYCODE=B.CURRENCYCODE"
            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTerminalCashDetail(ByVal TerminalId As String, ByVal SiteCode As String, ByVal DayopenDate As DateTime) As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.AMOUNTINCURRENCY,0)=0 THEN A.AMOUNTTENDERED ELSE A.AMOUNTINCURRENCY END AS AMOUNT,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total FROM" & _
            " CASHMEMORECEIPT A LEFT OUTER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE INNER JOIN CASHMEMOHDR C ON A.SITECODE = C.SITECODe AND A.BILLNO = C.BILLNO AND BillIntermediateStatus <> 'Deleted'  WHERE A.status=1 AND CONVERT(VARCHAR(10), CMRCPTDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) AND A.SiteCode='" & SiteCode & "' And A.TerminalId='" & TerminalId & "' AND A.TENDERTYPECODE Like '%CASH%' "
            StrQuery = StrQuery & " UNION ALL " & _
            " SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' OR  convert(numeric(18,2),ISNULL(A.REFNO_1,'0.00'))=0 THEN " & _
            " A.AMOUNTTENDERED ELSE  A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            " FROM SALESINVOICE A LEFT OUTER JOIN MSTCURRENCY B ON       A.CURRENCYCODE=B.CURRENCYCODE " & _
            " inner join salesorderhdr d " & _
            "on a.sitecode=d.sitecode and a.documentnumber=d.saleordernumber " & _
            " WHERE A.status=1 and CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            " AND A.SITECODE= '" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE LIKE '%CASH%' " & _
            " Union all " & _
            " SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' OR  convert(numeric(18,2),ISNULL(A.REFNO_1,'0.00'))=0 THEN " & _
            " A.AMOUNTTENDERED ELSE A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            " FROM SALESINVOICE A LEFT OUTER JOIN MSTCURRENCY B ON       A.CURRENCYCODE=B.CURRENCYCODE " & _
            " inner join Birthlist D " & _
            " on a.sitecode=D.sitecode and a.documentnumber=D.birthlistid and d.BirthListStatus<>'Cancel' " & _
            " WHERE A.status =1 and CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            " AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE LIKE '%CASH%' "
            '----this credit reciept is commented by Mahesh beacause its now seperate we added seperatly credit cash adjustment
            StrQuery = StrQuery & " Union All SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.AMOUNTINCURRENCY,0)=0 THEN A.AMOUNTTENDERED ELSE A.AMOUNTINCURRENCY END AS AMOUNT,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total FROM" & _
          " CreditReceipt A LEFT OUTER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE   WHERE CONVERT(VARCHAR(10), CmRcptDateTime, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) AND A.SiteCode='" & SiteCode & "' And A.TerminalId='" & TerminalId & "' AND A.TENDERTYPECODE Like '%CASH%'  " & _
          "    AND A.RefBillNo NOT IN( select BillNo from CashMemoHdr where billintermediatestatus='Deleted') "

            '"SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' THEN A.AMOUNTTENDERED ELSE A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            '"FROM 	SALESINVOICE A LEFT OUTER JOIN 	MSTCURRENCY B ON 	A.CURRENCYCODE=B.CURRENCYCODE " & _
            '"WHERE CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE LIKE '%CASH%'"

            '"WHERE(CONVERT(VARCHAR(10), SOINVDATE, 101) =  '" & DayopenDate.ToString("MM/dd/yyyy") & "') " & _




            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetShiftCashDetail(ByVal TerminalId As String, ByVal SiteCode As String, ByVal DayopenDate As DateTime, ByVal ShiftCreatedOn As DateTime) As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.AMOUNTINCURRENCY,0)=0 THEN A.AMOUNTTENDERED ELSE A.AMOUNTINCURRENCY END AS AMOUNT,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total FROM" & _
            " CASHMEMORECEIPT A LEFT OUTER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE INNER JOIN CASHMEMOHDR C ON A.SITECODE = C.SITECODe  and a.finyear=c.finyear AND A.BILLNO = C.BILLNO AND BillIntermediateStatus <> 'Deleted'  WHERE A.status=1 AND CONVERT(VARCHAR(10), CMRCPTDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) AND A.SiteCode='" & SiteCode & "' And A.TerminalId='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND A.TENDERTYPECODE Like '%CASH%' "

            StrQuery = StrQuery & " UNION ALL " & _
            " SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' OR  convert(numeric(18,2),ISNULL(A.REFNO_1,'0.00'))=0 THEN " & _
            " A.AMOUNTTENDERED ELSE  A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            " FROM SALESINVOICE A LEFT OUTER JOIN MSTCURRENCY B ON       A.CURRENCYCODE=B.CURRENCYCODE " & _
            " inner join salesorderhdr d " & _
            " on a.sitecode=d.sitecode and a.documentnumber=d.saleordernumber  " & _
            " WHERE A.status=1 and CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            " AND A.SITECODE= '" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND A.TENDERTYPECODE LIKE '%CASH%' " & _
            " Union all " & _
            " SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' OR  convert(numeric(18,2),ISNULL(A.REFNO_1,'0.00'))=0 THEN " & _
            " A.AMOUNTTENDERED ELSE A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            " FROM SALESINVOICE A LEFT OUTER JOIN MSTCURRENCY B ON       A.CURRENCYCODE=B.CURRENCYCODE " & _
            " inner join Birthlist D " & _
            " on a.sitecode=D.sitecode and a.documentnumber=D.birthlistid and d.BirthListStatus<>'Cancel' " & _
            " WHERE A.status =1 and CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            " AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND A.TENDERTYPECODE LIKE '%CASH%' "
            '----this credit reciept is commented by Mahesh beacause its now seperate we added seperatly credit cash adjustment
            StrQuery = StrQuery & " Union All SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.AMOUNTINCURRENCY,0)=0 THEN A.AMOUNTTENDERED ELSE A.AMOUNTINCURRENCY END AS AMOUNT,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total FROM" & _
          " CreditReceipt A LEFT OUTER JOIN MSTCURRENCY B ON A.CURRENCYCODE=B.CURRENCYCODE   WHERE CONVERT(VARCHAR(10), CmRcptDateTime, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) AND A.SiteCode='" & SiteCode & "' And A.TerminalId='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND A.TENDERTYPECODE Like '%CASH%'  " & _
          "    AND A.RefBillNo NOT IN( select BillNo from CashMemoHdr where billintermediatestatus='Deleted') "

            '"SELECT A.CurrencyCode,B.CURRENCYDESCRIPTION AS Currency, CASE WHEN ISNULL(A.REFNO_1,'')='' THEN A.AMOUNTTENDERED ELSE A.REFNO_1 END AS AMOUNT ,A.EXCHANGERATE as Rate,A.AMOUNTTENDERED as Total " & _
            '"FROM 	SALESINVOICE A LEFT OUTER JOIN 	MSTCURRENCY B ON 	A.CURRENCYCODE=B.CURRENCYCODE " & _
            '"WHERE CONVERT(VARCHAR(10), SOINVDATE, 101) =  ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE LIKE '%CASH%'"

            '"WHERE(CONVERT(VARCHAR(10), SOINVDATE, 101) =  '" & DayopenDate.ToString("MM/dd/yyyy") & "') " & _



            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn
            Dim daDenom As New SqlDataAdapter(cmd)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTerminalOtherDetail(ByVal TerminalId As String, ByVal SiteCode As String, ByVal DayopenDate As DateTime) As DataTable
        Try
            Dim StrQuery As String


            '** Ireland StrQuery = "SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE="

            StrQuery = "SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED FROM CASHMEMORECEIPT A INNER JOIN MSTTENDER B ON " & _
            "A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "' INNER JOIN MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE INNER JOIN CASHMEMOHDR D ON A.SITECODE = D.SITECODe and A.FinYear=D.FinYear AND A.BILLNO = D.BILLNO AND BillIntermediateStatus <> 'Deleted' " & _
            "WHERE A.status=1 and CONVERT(VARCHAR(10), CMRCPTDATE, 101) =( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            "AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND  A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)'" & _
            "UNION ALL " & _
            " SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED " & _
            " FROM SALESINVOICE A " & _
            "    INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            "    INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            "    inner join salesorderhdr d " & _
             "   on a.sitecode=d.sitecode  and  a.finyear =d.finyear AND a.documentnumber=d.saleordernumber and d.sostatus<>'Cancel' " & _
            " WHERE A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) " & _
            " from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND " & _
            " A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' " & _
            " union all " & _
            " SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED " & _
            " FROM SALESINVOICE A " & _
            " INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            " INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            " inner join Birthlist D " & _
            " on a.sitecode=D.sitecode and a.documentnumber=D.birthlistid and d.BirthListStatus<>'Cancel' " & _
            " WHERE " & _
            "  A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) " & _
            "  from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND " & _
            " A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' " & _
            "Union All " & _
            "SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED, " & _
            "1 AS ISSUED  FROM SALESINVOICE A " & _
            "INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND " & _
            "B.SITECODE='" & SiteCode & "'    INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE     inner join " & _
            "salesorderhdr d    on a.sitecode=d.sitecode and a.finyear=d.finyear and a.documentnumber=d.saleordernumber " & _
            "and d.sostatus='Cancel'  WHERE A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = " & _
            "( SELECT  CONVERT(VARCHAR(10),opendate,101)  from dayopennclose where sitecode = '" & SiteCode & "' AND " & _
            "dayclosestatus = 0  and status = 1) AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND " & _
            "A.TENDERTYPECODE = 'CREDITVOUC(I)'"
            '----this credit reciept is commented by Mahesh beacause its now seperate we added seperatly credit  adjustment

            '" Union All SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED FROM CreditReceipt A INNER JOIN MSTTENDER B ON " & _
            '"A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "' INNER JOIN MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE  " & _
            '"WHERE CONVERT(VARCHAR(10), CMRCPTDATETime, 101) =( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND  A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)'"

            '"SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' THEN 1 ELSE 0 END) AS ISSUED FROM SALESINVOICE A INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            '"INNER JOIN 	MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            '"WHERE CONVERT(VARCHAR(10), SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)" & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' "




            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetShiftOtherDetail(ByVal TerminalId As String, ByVal SiteCode As String, ByVal DayopenDate As DateTime, ByVal ShiftCreatedOn As DateTime) As DataTable
        Try
            Dim StrQuery As String


            '** Ireland StrQuery = "SELECT B.TENDERTYPE,C.DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE=" --and d.sostatus<>'Cancel'

            StrQuery = "SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED FROM CASHMEMORECEIPT A INNER JOIN MSTTENDER B ON " & _
            "A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "' INNER JOIN MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE INNER JOIN CASHMEMOHDR D ON A.SITECODE = D.SITECODe  AND A.FinYear=D.FinYear AND A.BILLNO = D.BILLNO AND BillIntermediateStatus <> 'Deleted' " & _
            "WHERE A.status=1 and CONVERT(VARCHAR(10), CMRCPTDATE, 101) =( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            "AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND  A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)'" & _
            "UNION ALL " & _
            " SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED " & _
            " FROM SALESINVOICE A " & _
            "    INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            "    INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            "    inner join salesorderhdr d " & _
             "   on a.sitecode=d.sitecode and a.finyear=d.finyear  and a.documentnumber=d.saleordernumber  " & _
            " WHERE A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) " & _
            " from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND " & _
            " A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' " & _
            " union all " & _
            " SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED " & _
            " FROM SALESINVOICE A " & _
            " INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            " INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            " inner join Birthlist D " & _
            " on a.sitecode=D.sitecode and a.documentnumber=D.birthlistid and d.BirthListStatus<>'Cancel' " & _
            " WHERE " & _
            "  A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) " & _
            "  from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND " & _
            " A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' " & _
            "Union All " & _
            "SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED, " & _
            "1 AS ISSUED  FROM SALESINVOICE A " & _
            "INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND " & _
            "B.SITECODE='" & SiteCode & "'    INNER JOIN      MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE     inner join " & _
            "salesorderhdr d    on a.sitecode=d.sitecode and a.documentnumber=d.saleordernumber " & _
            "and d.sostatus='Cancel'  WHERE A.status =1 and CONVERT(VARCHAR(10), a.SOINVDATE, 101) = " & _
            "( SELECT  CONVERT(VARCHAR(10),opendate,101)  from dayopennclose where sitecode = '" & SiteCode & "' AND " & _
            "dayclosestatus = 0  and status = 1) AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' And A.CREATEDON > @ShiftCreatedOn AND " & _
            "A.TENDERTYPECODE = 'CREDITVOUC(I)'"
            '----this credit reciept is commented by Mahesh beacause its now seperate we added seperatly credit  adjustment

            '" Union All SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' OR A.TENDERTYPECODE='CREDIT' THEN 1 ELSE 0 END) AS ISSUED FROM CreditReceipt A INNER JOIN MSTTENDER B ON " & _
            '"A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "' INNER JOIN MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE  " & _
            '"WHERE CONVERT(VARCHAR(10), CMRCPTDATETime, 101) =( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1) " & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND  A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)'"

            '"SELECT  B.TENDERTYPE ,B.TenderHeadcode,B.TenderHeadName  as DESCRIPTION, A.AMOUNTTENDERED,CONVERT(BIT,CASE WHEN A.TENDERTYPECODE='CREDITVOUC(I)' THEN 1 ELSE 0 END) AS ISSUED FROM SALESINVOICE A INNER JOIN MSTTENDER B ON A.TENDERHEADCODE=B.TENDERHEADCODE AND A.TENDERTYPECODE=B.TENDERTYPE AND B.SITECODE='" & SiteCode & "'" & _
            '"INNER JOIN 	MSTTENDERTYPE C ON B.TENDERTYPE=C.TENDERTYPE " & _
            '"WHERE CONVERT(VARCHAR(10), SOINVDATE, 101) = ( SELECT  CONVERT(VARCHAR(10),opendate,101) from dayopennclose where sitecode = '" & SiteCode & "' AND dayclosestatus = 0  and status = 1)" & _
            '"AND A.SITECODE='" & SiteCode & "' AND A.TERMINALID='" & TerminalId & "' AND A.TENDERTYPECODE <> 'CLPPOINT' AND A.TENDERTYPECODE <> 'GIFTVOUCHER(I)' "


            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn

            Dim daDenom As New SqlDataAdapter(cmd)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTerminalGiftVoucherIssue(ByVal TerminalId As String, ByVal SiteCode As String, ByVal Dayopendate As DateTime) As DataTable
        Try
            Dim StrQuery As String
            'StrQuery = "SELECT 'GiftVoucher(I)' AS TENDERTYPE,'Gift Voucher' AS DESCRIPTION,SUM(VALUEOFVOUCHER) AS AMOUNTTENDERED,CONVERT(BIT,1) AS ISSUED  " & _
            '"FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE AND B.VOURCHERTYPE='GIFTVOUCHER(I)'  " & _
            '"INNER JOIN CASHMEMOHDR C ON A.ISSUEDDOCNUMBER=C.BILLNO AND A.SITECODE=C.SITECODE  AND TerminalId='" & TerminalId & "' " & _
            '"WHERE(Convert(VARCHAR(10), Convert(DateTime, A.ISSUEDONDATE, 105)) = Convert(VARCHAR(10), Convert(DateTime, GETDATE(), 105))) AND A.SiteCode='" & SiteCode & "'"
            StrQuery = "SELECT 'GiftVoucher(I)' AS TENDERTYPE,'GiftVoucher(I)' as TenderHeadCode,'" & getValueByKey("CLCOM03") & "' AS DESCRIPTION,SUM(VALUEOFVOUCHER)* -1 AS AMOUNTTENDERED,CONVERT(BIT,1) AS ISSUED  " & _
            "FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE AND B.VOURCHERTYPE='GIFTVOUCHER(I)'  " & _
            "Left Outer JOIN CASHMEMOHDR C ON A.ISSUEDDOCNUMBER=C.BILLNO AND A.SITECODE=C.SITECODE  Left outer Join Salesinvoice D on A.ISSUEDDOCNUMBER=D.DocumentNumber AND A.SITECODE=C.SITECODE and D.status =1" & _
            "WHERE  A.IsActive=1 AND (C.TerminalId='" & TerminalId & "' OR  D.TerminalId='" & TerminalId & "')   AND (Convert(VARCHAR(10), Convert(DateTime, A.ISSUEDONDATE, 105)) = Convert(VARCHAR(10), Convert(DateTime,'" & Dayopendate.ToString("yyyyMMdd") & "'))) AND A.SiteCode='" & SiteCode & "'"
            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetShiftGiftVoucherIssue(ByVal TerminalId As String, ByVal SiteCode As String, ByVal Dayopendate As DateTime, ByVal ShiftCreatedOn As DateTime) As DataTable
        Try
            Dim StrQuery As String
            'StrQuery = "SELECT 'GiftVoucher(I)' AS TENDERTYPE,'Gift Voucher' AS DESCRIPTION,SUM(VALUEOFVOUCHER) AS AMOUNTTENDERED,CONVERT(BIT,1) AS ISSUED  " & _
            '"FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE AND B.VOURCHERTYPE='GIFTVOUCHER(I)'  " & _
            '"INNER JOIN CASHMEMOHDR C ON A.ISSUEDDOCNUMBER=C.BILLNO AND A.SITECODE=C.SITECODE  AND TerminalId='" & TerminalId & "' " & _
            '"WHERE(Convert(VARCHAR(10), Convert(DateTime, A.ISSUEDONDATE, 105)) = Convert(VARCHAR(10), Convert(DateTime, GETDATE(), 105))) AND A.SiteCode='" & SiteCode & "'"
            StrQuery = "SELECT 'GiftVoucher(I)' AS TENDERTYPE,'GiftVoucher(I)' as TenderHeadCode,'" & getValueByKey("CLCOM03") & "' AS DESCRIPTION,SUM(VALUEOFVOUCHER)* -1 AS AMOUNTTENDERED,CONVERT(BIT,1) AS ISSUED  " & _
            "FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE AND B.VOURCHERTYPE='GIFTVOUCHER(I)'  " & _
            "Left Outer JOIN CASHMEMOHDR C ON A.ISSUEDDOCNUMBER=C.BILLNO AND A.SITECODE=C.SITECODE  Left outer Join Salesinvoice D on A.ISSUEDDOCNUMBER=D.DocumentNumber AND A.SITECODE=C.SITECODE and D.status =1 " & _
            "WHERE  A.IsActive=1 AND (C.TerminalId='" & TerminalId & "' OR  D.TerminalId='" & TerminalId & "')   AND (Convert(VARCHAR(10), Convert(DateTime, A.ISSUEDONDATE, 105)) = Convert(VARCHAR(10), Convert(DateTime,'" & Dayopendate.ToString("yyyyMMdd") & "'))) AND A.SiteCode='" & SiteCode & "' And A.CREATEDON > @ShiftCreatedOn"
            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn

            Dim daDenom As New SqlDataAdapter(cmd)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTerminalOtherIncome(ByVal TerminalId As String, ByVal SiteCode As String, ByVal Dayopendate As DateTime) As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT 'Other Income' AS TENDERTYPE,'Ser - ' + B.ArticleName AS DESCRIPTION,SUM(NetAmount)AS AMOUNTTENDERED,CONVERT(BIT,0) AS ISSUED  " & _
                        " FROM cashmemodtl A INNER JOIN MSTArticle B ON A.ArticleCode=B.ArticleCode AND B.ArticalTypeCode='Service'   " & _
                        " inner join Cashmemohdr C on A.sitecode=c.sitecode and A.finyear=c.finyear and a.billno=c.billno  " & _
                        " WHERE C.BillIntermediateStatus <> 'Deleted' AND (Convert(VARCHAR(10), Convert(DateTime, A.BillDate, 105)) = Convert(VARCHAR(10), Convert(DateTime,'" & Dayopendate.ToString("yyyyMMdd") & "'))) AND A.SiteCode='" & SiteCode & "' " & _
                        " group by B.ArticleName union all " & _
                        " SELECT 'Other Income' AS TENDERTYPE,ChargeName AS DESCRIPTION,SUM(ChargeAmount + isnull(TaxAmt,0))AS AMOUNTTENDERED,CONVERT(BIT,0) AS ISSUED  " & _
                        " FROM SalesOrderOtherCharges " & _
                        " WHERE(Convert(VARCHAR(10), Convert(DateTime, DayOpenDate, 105)) = Convert(VARCHAR(10), Convert(DateTime,'" & Dayopendate.ToString("yyyyMMdd") & "'))) AND SiteCode='" & SiteCode & "' group by ChargeName"
            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            Return dtdenom
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetBillDetail(ByVal BillNo As String, ByVal SiteCode As String) As String 'vipin
        Try
            Dim StrQuery As String
            StrQuery = "if exists (select 1 from cashmemohdr where billno ='" & BillNo & "' and STATUS=1 and SiteCode='" & SiteCode & "')BEGIN select CLPNo 'CustomerNo' from cashmemohdr where billno ='" & BillNo & "' and STATUS=1 and SiteCode='" & SiteCode & "' END ELSE" & _
                        "  BEGIN  select CustomerNo from salesorderhdr where SaleOrderNumber =(select DocumentNumber from SalesInvoice where SaleInvNumber='" & BillNo & "' and STATUS=1 and SiteCode='" & SiteCode & "') END   "

            Dim daDenom As New SqlDataAdapter(StrQuery, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtdenom)
            If Not dtdenom Is Nothing AndAlso dtdenom.Rows.Count > 0 Then
                Return dtdenom.Rows(0)(0).ToString.Trim()
            Else
                Return ""
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTenderInfo(ByVal SiteCode As String, Optional ByVal vTenderType As String = "") As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" SELECT A.TenderHeadCode, A.TenderHeadName, A.TenderType, A.Positive_Negative,  ")
            vStmtQry.Append("            A.MaxNo, A.MaxValue, A.MinBillValue, A.DefaultValue  ")
            vStmtQry.Append(" FROM MstTender A  ")
            vStmtQry.Append(" INNER JOIN MstTenderType B ON A.TenderType = B.TenderType  ")
            vStmtQry.Append(" Where A.SiteCode = '" & SiteCode & "'")

            If Not String.IsNullOrEmpty(vTenderType) Then
                vStmtQry.Append(" AND ")
                vStmtQry.Append(" (B.TenderType = '" & vTenderType & "')")
            End If

            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function


    Public Function DeactivateVoucher(ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal sitecode As String, ByVal Billno As String, ByVal userid As String, Optional ByVal Doctype As String = "", Optional ByVal voucherno As String = "") As Boolean
        Try
            Dim strQuery As String = ""
            'strQuery = "UPDATE VOUCHERDTLS SET Status=0,IsActive=0,UPDATEDAT='" & sitecode & "',UPDATEDBY='" & userid & "',UPDATEDON=Getdate()  WHERE SITECODE='" & sitecode & "'And IssuedAtSite='" & sitecode & "'AND IssuedInDoctype='" & Doctype & "' AND IssuedDocNumber='" & Billno & "'"
            '---- Changed By Mahesh for Voucher can be issued again ...
            strQuery = "UPDATE VOUCHERDTLS SET IsIssued = 0 ,UPDATEDAT='" & sitecode & "',UPDATEDBY='" & userid & "',UPDATEDON=Getdate()  WHERE SITECODE='" & sitecode & "'And IssuedAtSite='" & sitecode & "'AND IssuedInDoctype='" & Doctype & "' AND IssuedDocNumber='" & Billno & "'"
            If voucherno <> String.Empty Then
                strQuery = strQuery & " AND VourcherSerialNbr='" & voucherno & "'"
            End If
            Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, con)
            cmdVoucher.Transaction = tran
            cmdVoucher.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            LogException(ex)
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ActiveVoucher(ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal sitecode As String, ByVal Voucherno As String, ByVal UserId As String) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "UPDATE VOUCHERDTLS SET IsRedeemed=0,UPDATEDAT='" & sitecode & "',UPDATEDBY='" & UserId & "',UPDATEDON=Getdate()  WHERE SITECODE='" & sitecode & "'And VourcherSerialNbr='" & Voucherno & "'"
            Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, con)
            cmdVoucher.Transaction = tran
            cmdVoucher.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function CheckVoucherRedemmed(ByVal sitecode As String, ByVal Billno As String, ByVal Doctype As String) As Boolean
        Try
            Dim strQuery As String = "select * from voucherdtls where sitecode='" & sitecode & "' and issuedinDocType='" & Doctype & "' and IssuedDOCNumber='" & Billno & "' AND isnull(isRedeemed,0)=1 Order by ValueOfVoucher"
            Dim dt As New DataTable
            Dim daVoucher As New SqlDataAdapter(strQuery, ConString)
            'daVoucher.SelectCommand.Transaction = tran
            daVoucher.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function AssignVoucherToBill(ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal sitecode As String, ByVal Billno As String, ByVal FinYear As String, ByVal Doctype As String, Optional ByVal Invoiceno As String = "") As Boolean
        Try

            Dim strQuery As String = "select * from voucherdtls where sitecode='" & sitecode & "' and issuedinDocType='" & Doctype & "' and IssuedDOCNumber='" & Billno & "' AND isnull(isRedeemed,0)=0 Order by ValueOfVoucher"
            Dim dt As New DataTable
            Dim daVoucher As New SqlDataAdapter(strQuery, con)
            daVoucher.SelectCommand.Transaction = tran
            daVoucher.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If Doctype = "CMS" Then
                        strQuery = "UPDATE cashmemoreceipt SET CardNo= '" & dr("VourcherSerialNbr").ToString() & "' WHERE status=1 and (TenderTypeCode='CreditVouc(I)' OR TenderTypeCode='GiftVoucher(I)') AND SITECODE='" & sitecode & "' AND FinYear='" & FinYear & "' And  BillNo='" & Billno & "' AND AmountTendered=(" & ConvertToEnglish(dr("ValueOfVoucher")) & " * -1)"
                    Else
                        strQuery = "UPDATE salesinvoice SET RefNO_2= '" & dr("VourcherSerialNbr").ToString() & "' WHERE status =1 and (TenderTypeCode='CreditVouc(I)' OR TenderTypeCode='GiftVoucher(I)') AND SITECODE='" & sitecode & "' AND FinYear='" & FinYear & "' And  DocumentNumber='" & Billno & "'  AND AmountTendered=(" & ConvertToEnglish(dr("ValueOfVoucher")) & " * -1) "
                        If Invoiceno <> String.Empty Then
                            strQuery = strQuery & " And  SaleInvNumber='" & Invoiceno & "'"
                        End If
                    End If
                    Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, con)
                    cmdVoucher.Transaction = tran
                    cmdVoucher.ExecuteNonQuery()
                Next
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetCurrentDate() As DateTime
        Dim currentDate As Date
        Dim drCurrentDate As SqlDataReader
        Try
            OpenConnection()
            Dim cmdCurrentDate As New SqlCommand("select getdate()", SpectrumCon)


            drCurrentDate = cmdCurrentDate.ExecuteReader()
            If (drCurrentDate.Read()) Then
                If Not (drCurrentDate.IsDBNull(0)) Then
                    currentDate = drCurrentDate.GetDateTime(0)
                End If
            End If
            Return currentDate
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            drCurrentDate.Close()
            CloseConnection()
        End Try

    End Function
    Public Function GetSiteInfo(ByVal vSiteCode As String) As DataTable
        Try

            Dim vStmtQry As String = " Select * from MstSite Where SiteCode='" & vSiteCode & "' "
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(vStmtQry, ConString)
            Dim Sqlds As New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds.Tables(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally

        End Try

    End Function
    Public Function GenerateGiftVoucher(ByRef dtGv As DataTable, ByRef Tran As SqlTransaction, ByVal docno As String, ByVal SiteCode As String, ByVal userId As String, ByVal GenTime As DateTime, ByVal Dayopendate As DateTime) As Boolean
        Try
            Dim Counter As Integer = 0
            Dim MaxNo As Int64 = getDocumentNo("GV", SiteCode)
            Dim dtTempGv As DataTable = dtGv.Clone()
            Dim Filter As String = "ISPREPRINTED = True"
            Dim dv As New DataView(dtGv, Filter, "VOUCHERCODE", DataViewRowState.CurrentRows)
            If dv.Count > 0 Then
                dtTempGv = dv.ToTable.Copy()
                For Each dr As DataRow In dtTempGv.Rows
                    Counter = ListOfNumber.Dequeue()
                    If UpdatePrintedGiftVoucher(dr("VourcherSerialNbr").ToString(), dr("VoucherCode").ToString(), dr("IssuedInDoctype").ToString(), docno, dr("IssuedOnDate"), SiteCode, userId, SpectrumCon, Tran, dr("ExpiryInDays").ToString(), Counter) = False Then
                        Throw New Exception(getValueByKey("CLCOM05"))
                    End If
                Next
                dtTempGv.Clear()
                ListOfNumber.Clear()
            End If
            Filter = "ISPREPRINTED is null or ISPREPRINTED=false "
            dv.RowFilter = Filter
            If dv.Count > 0 Then
                dv.AllowEdit = True
                For Each dr As DataRowView In dv
                    Dim i As Int16 = dr("Quantity")
                    For i = 1 To CInt(dr("Quantity"))
                        Dim character As Char = SiteCode.FirstOrDefault(Function(x) x <> "0")
                        'Changed by Rohit to generate Document No. for proper sorting
                        Try
                            Dim strFinyear As String = GetFinancialYear(Dayopendate, SiteCode)
                            dr("VOURCHERSERIALNBR") = GenDocNo("G" & SiteCode.Substring(SiteCode.IndexOf(character)) & strFinyear.Substring(strFinyear.Length - 2, 2), 13, MaxNo)
                        Catch ex As Exception
                            dr("VOURCHERSERIALNBR") = "G" & SiteCode.Substring(SiteCode.IndexOf(character)) & MaxNo
                        End Try
                        'End Change by Rohit

                        'changed by ram dt:24.05.2009 action : add
                        'dr("Createdby") = userId
                        'dr("CreatedAt") = SiteCode
                        'dr("Createdon") = GenTime
                        'dr("Updatedby") = userId
                        'dr("UpdatedAt") = SiteCode
                        'dr("Updatedon") = GenTime
                        'changed by ram dt:24.05.2009 action : add
                        dtTempGv.ImportRow(dr.Row)
                        MaxNo = MaxNo + 1
                    Next
                Next
            End If

            Try
                DeleteColumnFromDataTable(dtTempGv, "Quantity")
                DeleteColumnFromDataTable(dtTempGv, "ISPREPRINTED")
                DeleteColumnFromDataTable(dtTempGv, "ISSUEDDOCNUMBER")
                DeleteColumnFromDataTable(dtTempGv, "ISSUEDONDATE")
                DeleteColumnFromDataTable(dtTempGv, "CREATEDBY")
                DeleteColumnFromDataTable(dtTempGv, "CREATEDAT")
                DeleteColumnFromDataTable(dtTempGv, "CREATEDON")
                DeleteColumnFromDataTable(dtTempGv, "UPDATEDBY")
                DeleteColumnFromDataTable(dtTempGv, "UPDATEDON")
                DeleteColumnFromDataTable(dtTempGv, "UPDATEDAT")
                DeleteColumnFromDataTable(dtTempGv, "STATUS")
                DeleteColumnFromDataTable(dtTempGv, "VOUCHERDESC")
                DeleteColumnFromDataTable(dtTempGv, "NetAmount")
                DeleteColumnFromDataTable(dtTempGv, "SEL")
                DeleteColumnFromDataTable(dtTempGv, "ExpiryInDays")
                DeleteColumnFromDataTable(dtTempGv, "BillLineNo")
            Catch ex As Exception

            End Try

            AddColumnToDataTable(dtTempGv, "ISSUEDDOCNUMBER", "System.String", docno)
            AddColumnToDataTable(dtTempGv, "ISSUEDONDATE", "System.DateTime", Dayopendate)
            'changed by ram dt:24.05.2009 action: comment
            AddColumnToDataTable(dtTempGv, "CREATEDBY", "System.String", userId)
            AddColumnToDataTable(dtTempGv, "CREATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(dtTempGv, "CREATEDON", "System.DateTime", GenTime)
            AddColumnToDataTable(dtTempGv, "UPDATEDBY", "System.String", userId)
            AddColumnToDataTable(dtTempGv, "UPDATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(dtTempGv, "UPDATEDON", "System.DateTime", GenTime)
            AddColumnToDataTable(dtTempGv, "STATUS", "System.Boolean", True)
            'changed by ram dt:24.05.2009 action: comment
            dtTempGv.TableName = "voucherdtls"
            AddMode(dtTempGv)
            If SaveData(dtTempGv, SpectrumCon, Tran) Then
                If UpdateDocumentNo("GV", SpectrumCon, Tran, MaxNo) = True Then
                    dtGv = Nothing
                    dtGv = dtTempGv
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function UpdatePrintedGiftVoucher(ByVal VoucherNo As String, ByVal VoucherCode As String, ByVal DocType As String, ByVal issuedDocNo As String, ByVal IssuedDocDate As DateTime, ByVal SiteCode As String, ByVal userid As String, ByRef con As SqlConnection, ByRef Tran As SqlTransaction, Optional ByVal strExpiryDay As String = "0", Optional ByVal count As Integer = 0) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "UPDATE VOUCHERDTLS SET IsIssued=1,IssuedAtSite='" & SiteCode & "',IssuedInDoctype='" & DocType & "',IssuedOnDate='" & Format(IssuedDocDate, "yyyyMMdd") & _
                "',ISSUEDDOCNUMBER='" & issuedDocNo & "', VoucherRangeFrom='" & count & "', UPDATEDAT='" & SiteCode & "',UPDATEDBY='" & userid & "',UPDATEDON= getdate(), ExpiryDate = getdate()+ " & strExpiryDay & "  WHERE SITECODE='" & SiteCode & "'And VOURCHERSERIALNBR='" & VoucherNo & "' And VoucherCode='" & VoucherCode & "'"
            Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, con)
            cmdVoucher.Transaction = Tran
            If cmdVoucher.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetSyncDetail() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT B.TERMINALNAME AS [Terminal Name],C.TRANSACTIONNAME as [Transaction]," & _
            "CASE WHEN A.PUSHORPULL=1 THEN 'PUSH' ELSE 'PULL' END AS [PushOrPull],A.LASTSYNCTIMESTAMP as [LastSyncTime],A.NoofRecordsAffected as [No of Document Updated]" & _
            " FROM SYNCTERMINAL A INNER JOIN MSTTERMINALID B ON A.TERMINALID=B.TERMINALID INNER JOIN MSTTRANSACTION C ON " & _
            " A.TRANSACTIONCODE=C.TRANSACTIONCODE Where A.Status=1"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code is added by irfan 27/8/2018 For Seating Area wise Article Price Mapping
    Public Function GetPriceMappingDetail() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = ""
            strString = " select asam.SiteCode,asam.ArticleCode,asam.EAN,asam.SeatingAreaId,sa.SeatingAreaName,asam.Price,(case when asam.Status=1 then 1 else 0 end) as Status from ArticleSeatingAreaMap asam inner join seatingarea sa "
            strString += "     on asam.SeatingAreaId=sa.SeatingAreaId"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code is added by irfan 27/8/2018 For Seating Area wise Article Price Mapping
    Public Function CheckSittingPriceMapping(ByVal ArticleCode As String, ByVal SeatingAreaId As String) As Boolean
        Try
            Dim strString As String = ""
            strString = "SELECT * FROM ArticleSeatingAreaMap WHERE ArticleCode = '" & ArticleCode & "' and SeatingAreaId='" & SeatingAreaId & "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code is added by irfan 27/8/2018 For Seating Area wise Article Price Mapping
    Public Function SaveSittingPriceMapping(ByVal dt As DataTable, ByVal sitename As String, ByVal username As String) As Boolean
        Try
            Dim cmd As SqlCommand
            Dim strString As New StringBuilder
            strString.Length = 0
            If dt.Rows.Count > 0 Then
                Dim dv As New DataView(dt, "IsValid ='True'", "", DataViewRowState.CurrentRows)
                Dim dd = dt.Select("IsValid='True'")
                If dd.Length > 0 Then
                    dt = dd.CopyToDataTable
                Else
                    Return False
                End If
                Dim i As Integer
                For i = 0 To dt.Rows.Count - 1
                    Dim isexit As Boolean = CheckSittingPriceMapping(dt.Rows(i)("ArticleCode"), dt.Rows(i)("SeatingAreaId"))
                    If isexit Then
                        strString.Append(" update ArticleSeatingAreaMap set Price='" & dt.Rows(i)("Price") & "',SeatingAreaId='" & dt.Rows(i)("SeatingAreaId") & "', UPDATEDAT='" & sitename & "',UPDATEDBY='" & username & "',UPDATEDON=GetDate(),Status='" & dt.Rows(i)("Status") & "' WHERE ArticleCode='" & dt.Rows(i)("ArticleCode") & "' and SiteCode='" & dt.Rows(i)("SiteCode") & "' and SeatingAreaId='" & dt.Rows(i)("SeatingAreaId").ToString & "' ;")
                    Else
                        strString.Append(" insert into ArticleSeatingAreaMap values('" & dt.Rows(i)("SiteCode") & "','" & dt.Rows(i)("ArticleCode") & "','" & dt.Rows(i)("EAN") & "','" & dt.Rows(i)("SeatingAreaId") & "','" & dt.Rows(i)("Price") & "', ")
                        strString.Append(" '" & sitename & "','" & username & "',GetDate(),'" & sitename & "','" & username & "',GetDate(),'" & dt.Rows(i)("Status") & "') ;")
                    End If
                    cmd = New SqlCommand(strString.ToString(), SpectrumCon)
                    ' cmd.Transaction = trans
                    If cmd.ExecuteNonQuery() > 0 Then
                        strString.Length = 0
                        Continue For
                    Else
                        CloseConnection()
                        Exit For
                        Return False
                    End If
                Next
                'OpenConnection()
                'Dim cmd As New SqlCommand(strString.ToString(), SpectrumCon)
                'If cmd.ExecuteNonQuery() > 0 Then
                '    CloseConnection()
                '    Return True
                'End If
            End If
        Catch ex As Exception
            LogException(ex)
            CloseConnection()
            Return False
        End Try
    End Function
    'Code is added by irfan 27/8/2018 For Seating Area wise Article Price Mapping
    Public Function GetArtDetails(ByVal ArtType As String, ByVal Id As String, ByVal sitename As String) As DataSet
        Try
            Dim dt As DataSet
            Dim strString As String = ""
            strString += "select SiteCode,EAN,SellingPrice from salesinforecord where ArticleCode='" & ArtType & "' and sitecode='" & sitename & "' ;"
            strString += " select SeatingAreaId,SeatingAreaName from seatingarea where SeatingAreaId='" & Id & "';"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataSet
            da.Fill(dt)
            dt.Tables(0).TableName = "salesinforecord"
            dt.Tables(1).TableName = "seatingarea"
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPrintingDetails(ByVal DocType As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT TOPBOTTOM,SEQUENCENO,RECEIPTTEXT,ALIGN,WIDTH,HEIGHT,BOLD " & _
            "FROM PRINTINGDETAIL WHERE DOCUMENTTYPE='" & DocType & "' or  DOCUMENTTYPE='ALLDocs' ORDER BY DOCUMENTTYPE,TOPBOTTOM,SEQUENCENO"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetGrievanceNotification() As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "select Top 1 GrievanceId,GrievanceDesc,IsViewed,Status from GrievanceDetails WHERE IsViewed =0 AND Status=1 Order By UpdatedOn ASc"
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateGrievanceNotification(ByVal GrievanceId As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = "Update GrievanceDetails SET UpdatedOn = GetDate(), IsViewed =1 WHERE GrievanceId  = '" & GrievanceId & "' AND Status=1 "
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '-- added for getting notifications of items on Home Delivery screen - ashma 21 dec 2016
    Public Function GetHomeDeliveryNotification() As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "select Top 1 SiteCode, BillNo, isviewedatstore, Status from CashMemoHdr WHERE isviewedatstore = 0 AND Status=1 Order By UpdatedOn ASc"
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '-- added for updating viewed items on Home Delivery screen - ashma 21 dec 2016
    Public Function UpdateHomeDeliveryNotification(ByVal SiteCodeId As String, ByVal BillNo As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = "Update CashMemoHdr SET UpdatedOn = GetDate(), isviewedatstore=1 WHERE SiteCode  = '" & SiteCodeId & "' AND BillNo ='" & BillNo & "' AND  Status=1"
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '-- added for View order details screen - ashma 2nd jan 2017
    Public Function FillForm(ByVal BillNo As String, ByVal SiteCode As String) As DataTable
        Try
            'Dim strString As String = "select * from MstArticle mstA inner join CashMemoDtl cmd on mstA.ArticleCode=cmd.ArticleCode where cmd.BillNo = '" & BillNo & "' AND cmd.SiteCode= '" & SiteCode & "'    "
            Dim strString As String = "SELECT C.ARTICLENAME AS DISCRIPTION,A.QUANTITY,A.SELLINGPRICE,A.TOTALDISCOUNT,A.TOTALDISCPERCENTAGE,A.EXCLUSIVETAX ,A.GROSSAMT" & _
                                      " FROM CASHMEMODTL A WITH (NOLOCK) INNER JOIN MSTEAN B WITH (NOLOCK) ON A.EAN=B.EAN INNER JOIN MSTARTICLE C WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE  WHERE A.SITECODE='" & SiteCode & "' AND A.BILLNO='" & BillNo & "'"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function FillForm_OnlineOrder(ByVal BillNo As String, ByVal SiteCode As String) As DataTable
        Try
            'Dim strString As String = "select * from MstArticle mstA inner join CashMemoDtl cmd on mstA.ArticleCode=cmd.ArticleCode where cmd.BillNo = '" & BillNo & "' AND cmd.SiteCode= '" & SiteCode & "'    "
            'Dim strString As String = "SELECT C.ARTICLENAME AS DISCRIPTION,A.QUANTITY,A.SELLINGPRICE,A.TOTALDISCOUNT,A.TOTALDISCPERCENTAGE,A.EXCLUSIVETAX ,A.GROSSAMT,A.NetAmount" & _
            '                          " FROM DPACASHMEMODTL A WITH (NOLOCK) INNER JOIN MSTEAN B WITH (NOLOCK) ON A.EAN=B.EAN INNER JOIN MSTARTICLE C WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE  WHERE A.SITECODE='" & SiteCode & "' AND A.BILLNO='" & BillNo & "'"


            Dim strString As String
            'Dim strString As String = " SELECT C.ARTICLENAME AS DISCRIPTION,sum(A.QUANTITY)'QUANTITY',sum(A.SELLINGPRICE)/sum(A.QUANTITY)'SELLINGPRICE',sum(A.TOTALDISCOUNT)'TOTALDISCOUNT'," & _
            '                          " sum(A.TOTALDISCPERCENTAGE) 'TOTALDISCPERCENTAGE',sum(A.EXCLUSIVETAX)'EXCLUSIVETAX' ,sum(A.GROSSAMT)'GROSSAMT',sum(A.NetAmount)'NetAmount'" & _
            '                          " FROM DPACASHMEMODTL A WITH (NOLOCK) INNER JOIN MSTEAN B WITH (NOLOCK) ON A.EAN=B.EAN " & _
            '                          " INNER JOIN MSTARTICLE C WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE   WHERE A.SITECODE='" & SiteCode & "' AND A.BILLNO='" & BillNo & "'" & _
            '                          " group by A.ArticleCode,C.ARTICLENAME"
            If BillNo.ToString().Substring(0, 2) = "UB" Then
                strString = " SELECT C.ARTICLENAME AS DISCRIPTION,sum(A.QUANTITY)'QUANTITY',sum(A.SELLINGPRICE)/sum(A.QUANTITY)'SELLINGPRICE',sum(A.TOTALDISCOUNT)'TOTALDISCOUNT'," & _
                                  " sum(A.TOTALDISCPERCENTAGE) 'TOTALDISCPERCENTAGE',sum(A.EXCLUSIVETAX)'EXCLUSIVETAX' ,sum(A.GROSSAMT)'GROSSAMT',sum(A.NetAmount)'NetAmount'" & _
                                  " FROM DPACASHMEMODTL A WITH (NOLOCK) INNER JOIN MSTEAN B WITH (NOLOCK) ON A.EAN=B.EAN " & _
                                  " INNER JOIN MSTARTICLE C WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE   WHERE A.SITECODE='" & SiteCode & "' AND A.BILLNO='" & BillNo & "'" & _
                                  " group by A.ArticleCode,C.ARTICLENAME"
            Else
                strString = "SELECT C.ARTICLENAME AS DISCRIPTION,A.QUANTITY,A.SELLINGPRICE,A.TOTALDISCOUNT,A.TOTALDISCPERCENTAGE,A.EXCLUSIVETAX ,A.GROSSAMT,A.NetAmount" & _
                                   " FROM DPACASHMEMODTL A WITH (NOLOCK) INNER JOIN MSTEAN B WITH (NOLOCK) ON A.EAN=B.EAN INNER JOIN MSTARTICLE C WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE  WHERE A.SITECODE='" & SiteCode & "' AND A.BILLNO='" & BillNo & "'"
            End If

            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTerminals(ByVal Sitecode As String, Optional ByVal AssignCheck As Boolean = False) As DataTable
        Try
            Dim StrQuery As String
            If AssignCheck = False Then
                StrQuery = "SELECT TERMINALID,TERMINALNAME FROM MSTTERMINALID WHERE STATUS=1 and Sitecode='" & Sitecode & "'"
            Else

                Dim myCompName As String = My.Computer.Name
                StrQuery = "SELECT TERMINALID,TERMINALNAME FROM MSTTERMINALID WHERE STATUS=1 and Sitecode='" & Sitecode & "' AND ( ISnull(TERMINALLOCATION,'')='' )"
            End If
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPrepStations(ByVal Sitecode As String, Optional ByVal AssignCheck As Boolean = False) As DataTable
        Try
            Dim StrQuery As String
            If AssignCheck = False Then
                StrQuery = "SELECT mstPrepStationID FROM MstPrepStation WHERE STATUS=1 and Sitecode='" & Sitecode & "'"
            Else
                Dim myCompName As String = My.Computer.Name
                StrQuery = "SELECT mstPrepStationID FROM MstPrepStation WHERE STATUS=1 and Sitecode='" & Sitecode & "' AND ( ISnull(prepStationLocation,'')='' )"
            End If
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateTerminal(ByVal terminal As String, ByVal Ip As String, ByVal vSiteCode As String, ByVal vUserName As String, ByVal vCurrentDate As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = "Update MstTerminalID Set TerminalLocation='" & Ip & "', UPDATEDAT='" & vSiteCode & "', UPDATEDBY='" & vUserName & "', UPDATEDON = getDate() Where TerminalId='" & terminal & "'"
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function UpdatePrepStation(ByVal PrepStationId As String, ByVal Ip As String, ByVal vSiteCode As String, ByVal vUserName As String, ByVal vCurrentDate As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = "Update MstPrepStation Set prepStationLocation='" & "" & "'  Where prepStationLocation='" & Ip & "' "
            cmd.CommandText += " Update MstPrepStation Set prepStationLocation='" & Ip & "', UPDATEDAT='" & vSiteCode & "', UPDATEDBY='" & vUserName & "', UPDATEDON = getDate() Where mstPrepStationID='" & PrepStationId & "'"
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'added by khusrao adil on 10-07-2017 for print format 6 changes
    Public Function GetCLPProgram() As Boolean
        Try
            Dim retrunValue As Boolean = False
            Dim dt As New DataTable
            '---- AND A.ACCUMLATIONAPPLICABLE=1 remove 0011729: Unable to create customer registration as per Rama say AccumlationApplicable is true not required for customer registration (table name CLPProgramSiteMap)
            Dim da As New SqlDataAdapter("select IsPOSPasskey from MstCLPProgram m inner join CLPPROGRAMSITEMAP p on m.CLPProgramId=p.ClpProgramId  and p.Status=1", ConString)
            'IsPOSPasskey,
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim isPosValue = dt.Rows(0)("IsPOSPasskey").ToString()
                If Not String.IsNullOrEmpty(isPosValue) Then
                    If isPosValue = True Then
                        retrunValue = True
                    End If
                Else
                    retrunValue = False
                End If
                Return retrunValue
            End If
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try

    End Function
    Public Function GetCLPProgram(ByVal SiteCode As String, ByRef CLpArt As String, ByRef ClpRedem As Boolean) As String
        Try
            Dim dt As New DataTable
            '---- AND A.ACCUMLATIONAPPLICABLE=1 remove 0011729: Unable to create customer registration as per Rama say AccumlationApplicable is true not required for customer registration (table name CLPProgramSiteMap)
            Dim da As New SqlDataAdapter("SELECT A.CLPPROGRAMID,B.ARTICLECODE,A.RedemptionApplicable FROM CLPPROGRAMSITEMAP A INNER JOIN MSTCLPPROGRAM B ON A.CLPPROGRAMID=B.CLPPROGRAMID AND B.STATUS=1 WHERE A.SITECODE='" & SiteCode & "'  AND A.STATUS=1 ", ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                CLpArt = dt.Rows(0)("ARTICLECODE").ToString()
                ClpRedem = IIf(dt.Rows(0)("RedemptionApplicable") Is DBNull.Value, False, dt.Rows(0)("RedemptionApplicable"))
                Return dt.Rows(0)("CLPPROGRAMID").ToString()

            End If
            Return ""
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function
    Public Function GetCVPProgram(ByVal SiteCode As String, ByRef VoucherValidityDays As Int32, ByRef CVBaseArticleCode As String) As String
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT A.VOUCHERCODE,isnull(B.EXPIRYAFTERDAYS,0) as EXPIRYAFTERDAYS, B.ArticleCode FROM VOUCHERSITEMAP A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE WHERE B.VourcherType='CreditVouc(I)' And A.SITECODE='" & SiteCode & "' AND A.STATUS=1", ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                VoucherValidityDays = CDbl(dt.Rows(0)("EXPIRYAFTERDAYS").ToString())
                CVBaseArticleCode = dt.Rows(0)("ArticleCode").ToString()
                Return dt.Rows(0)("VoucherCode").ToString()
            End If
            Return ""
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function
    Public Function UpdateClpPoints(ByVal Accu As Boolean, ByVal ClpProgram As String, ByVal CustomerId As String, ByVal Points As Double, ByRef con As SqlConnection, ByRef Tran As SqlTransaction, _
                                    Optional ByVal Sitecode As String = "", Optional ByVal userid As String = "", Optional ByVal billno As String = "", Optional ByVal billdate As DateTime = Nothing, Optional ByVal InsertDetail As Boolean = False, Optional ByVal finyear As String = "") As Boolean
        Try


            Dim Str, strpoints As String

            strpoints = ConvertToEnglish(Points)

            If Accu = True Then
                Str = "UPDATE CLPCUSTOMERS SET UPDATEDON=GetDate() ,TotalBalancePoint=ISNULL(TotalBalancePoint,0)+ " & strpoints & ", POINTSACCUMLATED =ISNULL(POINTSACCUMLATED,0)+ " & strpoints & " WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "'"
            Else
                Str = "UPDATE CLPCUSTOMERS SET  UPDATEDON=GetDate(),TotalBalancePoint=ISNULL(TotalBalancePoint,0)- " & strpoints & ", POINTSREDEEMED =ISNULL(POINTSREDEEMED,0)+ " & strpoints & " WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "';"
                If InsertDetail = True Then
                    '----- Changed By Mahesh for created Date and Updated Date 
                    'Str = Str & "INSERT INTO CLPTRANSACTION(SITECODE,BILLNO,BILLDATE,REDEMPTIONPOINTS,CLPPROGRAMID,CLPCUSTOMERID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                    '                         "VALUES('" & Sitecode & "','" & billno & "','" & Format(billdate, "yyyyMMdd") & "'," & Points & ",'" & ClpProgram & "','" & CustomerId & "','" & Sitecode & "','" & userid & "','" & Format(billdate, "yyyyMMdd") & "','" & Sitecode & "','" & userid & "','" & Format(billdate, "yyyyMMdd") & "',1)"

                    Str = Str & "INSERT INTO CLPTRANSACTION(SITECODE,BILLNO,BILLDATE,REDEMPTIONPOINTS,CLPPROGRAMID,CLPCUSTOMERID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                                             "VALUES('" & Sitecode & "','" & billno & "','" & Format(billdate, "yyyyMMdd") & "'," & Points & ",'" & ClpProgram & "','" & CustomerId & "','" & Sitecode & "','" & userid & "',GetDate(),'" & Sitecode & "','" & userid & "',GetDate(),1)"

                End If
            End If
            Dim cmd As New SqlCommand(Str, con, Tran)
            If cmd.ExecuteNonQuery() > 0 Then

                Return True
            End If

        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function UpdateClpPoints(ByVal Accu As Boolean, ByVal ClpProgram As String, ByVal CustomerId As String, ByVal Points As Double, Optional ByVal Sitecode As String = "", Optional ByVal userid As String = "", _
                                    Optional ByVal billno As String = "", Optional ByVal billdate As DateTime = Nothing, Optional ByVal InsertDetail As Boolean = False, Optional ByVal spectrumtrans As SqlTransaction = Nothing, Optional ByVal finyear As String = "") As Boolean
        Try


            Dim Str As String
            If Accu = True Then
                Str = "UPDATE CLPCUSTOMERS SET UPDATEDON=GetDate() ,TotalBalancePoint=ISNULL(TotalBalancePoint,0)+ " & ConvertToEnglish(Points) & ", POINTSACCUMLATED =ISNULL(POINTSACCUMLATED,0)+ " & ConvertToEnglish(Points) & " WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "'"
            Else
                Str = "UPDATE CLPCUSTOMERS SET UPDATEDON=GetDate() ,TotalBalancePoint=ISNULL(TotalBalancePoint,0)- " & ConvertToEnglish(Points) & ", POINTSREDEEMED =ISNULL(POINTSREDEEMED,0)+ " & ConvertToEnglish(Points) & " WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "';"
                If InsertDetail = True Then
                    'Str = Str & "INSERT INTO CLPTRANSACTION(SITECODE,BILLNO,BILLDATE,REDEMPTIONPOINTS,CLPPROGRAMID,CLPCUSTOMERID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,FinYear)" & _
                    '                         "VALUES('" & Sitecode & "','" & billno & "','" & Format(billdate, "yyyyMMdd") & "'," & ConvertToEnglish(Points) & ",'" & ClpProgram & "','" & CustomerId & "','" & Sitecode & "','" & userid & "','" & Format(billdate, "yyyyMMdd") & "','" & Sitecode & "','" & userid & "','" & Format(billdate, "yyyyMMdd") & "',1,'" + finyear + "')"

                    Str = Str & "INSERT INTO CLPTRANSACTION(SITECODE,BILLNO,BILLDATE,REDEMPTIONPOINTS,CLPPROGRAMID,CLPCUSTOMERID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,FinYear)" & _
                                          "VALUES('" & Sitecode & "','" & billno & "','" & Format(billdate, "yyyyMMdd") & "'," & ConvertToEnglish(Points) & ",'" & ClpProgram & "','" & CustomerId & "','" & Sitecode & "','" & userid & "',Getdate(),'" & Sitecode & "','" & userid & "',GetDate(),1,'" + finyear + "')"


                End If
            End If
            OpenConnection()

            Dim cmd As New SqlCommand(Str, SpectrumCon)

            If Not spectrumtrans Is Nothing Then
                cmd.Transaction = spectrumtrans
            End If



            If cmd.ExecuteNonQuery() > 0 Then
                If spectrumtrans Is Nothing Then
                    CloseConnection()
                End If

                Return True
            Else

            End If
            CloseConnection()
        Catch ex As Exception

            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ReversedCLPPoints(ByVal ClpProgram As String, ByVal CustomerId As String, ByVal Points As Double, ByVal PointsRedeem As Double, Optional ByVal Sitecode As String = "", Optional ByVal userid As String = "", _
                                    Optional ByVal billno As String = "", Optional ByVal billdate As DateTime = Nothing) As Boolean
        Try
            Dim Str As String

            Str = "UPDATE CLPCUSTOMERS SET TotalBalancePoint=ISNULL(TotalBalancePoint,0)+ " & ConvertToEnglish(PointsRedeem - Points) & ", POINTSACCUMLATED =ISNULL(POINTSACCUMLATED,0)- " & ConvertToEnglish(Points) & ",POINTSREDEEMED =ISNULL(POINTSREDEEMED,0)+ " & ConvertToEnglish(PointsRedeem) & ",updatedon='" & Format(billdate, "yyyyMMdd") & "',Updatedby='" & userid & "',Updatedat='" & Sitecode & "' WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "';"
            Str = Str & "UPDATE CLPTRANSACTION SET ACCUMLATIONPOINTS=0, REDEMPTIONPOINTS=0,BALACCUMLATIONPOINTS=0,STATUS=0,updatedon=GetDate(),Updatedby='" & userid & "',Updatedat='" & Sitecode & "' WHERE SITECODE='" & Sitecode & "' AND BILLNO='" & billno & "'; "
            Str = Str & "UPDATE CLPTRANSACTIONSDETAILS SET CLPPOINTS=0,CLPDISCOUNT=0,STATUS=0,updatedon=GetDate(),Updatedby='" & userid & "',Updatedat='" & Sitecode & "' WHERE SITECODE='" & Sitecode & "' AND BILLNO='" & billno & "' "
            'Else
            'Str = "UPDATE CLPCUSTOMERS SET TotalBalancePoint=ISNULL(TotalBalancePoint,0)+ " & Points & ", POINTSREDEEMED =ISNULL(POINTSREDEEMED,0)+ " & Points & " WHERE CardNO='" & CustomerId & "' And CLPProgramId='" & ClpProgram & "';"
            ''If InsertDetail = True Then
            ''    Str = Str & "INSERT INTO CLPTRANSACTION(SITECODE,BILLNO,BILLDATE,REDEMPTIONPOINTS,CLPPROGRAMID,CLPCUSTOMERID,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
            ''                             "VALUES('" & Sitecode & "','" & billno & "','" & billdate & "'," & Points & ",'" & ClpProgram & "','" & CustomerId & "','" & Sitecode & "','" & userid & "','" & billdate & "','" & Sitecode & "','" & userid & "','" & billdate & "',1)"
            ''End If
            'End If
            OpenConnection()
            Dim cmd As New SqlCommand(Str, SpectrumCon)
            If cmd.ExecuteNonQuery() > 0 Then
                CloseConnection()
                Return True
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetFinancialYear(ByRef DayOpendate As DateTime, ByVal Sitecode As String) As String
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT A.FINYEAR,B.OpenDate FROM MSTFINYEAR A INNER JOIN DAYOPENNCLOSE B ON A.SITECODE=B.SITECODE AND A.FINYEAR=B.FINYEAR WHERE A.FINSTATUS=1 AND DAYCLOSESTATUS=0 AND A.Sitecode='" & Sitecode & "'", ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                DayOpendate = dt.Rows(0)("OpenDate")
                Return dt.Rows(0)("FINYEAR").ToString()
            End If
            Return ""
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function GetFinancialYear(ByVal Sitecode As String) As String
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT FINYEAR FROM MSTFINYEAR WHERE FINSTATUS=1  AND Sitecode='" & Sitecode & "'", ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)("FINYEAR").ToString()
            End If
            Return ""
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function GetFinancialYearStartDate(ByVal Sitecode As String) As DateTime
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT ValidFromDt FROM MSTFINYEAR WHERE FINSTATUS=1  AND Sitecode='" & Sitecode & "'", ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)("ValidFromDt")
            End If

        Catch ex As Exception
            LogException(ex)
            Return Date.MinValue
        End Try
    End Function

    ''' <summary> 
    ''' Get Tax for Item Or bill
    ''' </summary>
    ''' <param name="sitecode">siteCode</param>
    ''' <param name="stritem">ArticleCode</param>
    ''' <param name="DocumentType"></param>
    ''' <param name="Ean">Ean Code</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function getTax(ByVal sitecode As String, ByVal stritem As String, ByVal DocumentType As String, ByVal ItemQty As Double, Optional ByVal Ean As String = "", Optional ByVal cstTaxCode As String = "", Optional ByVal considerCst As Boolean = False, Optional ByVal StrSelectedArticles As String = "") As DataTable
        'Public Function getTax(ByVal sitecode As String, ByVal stritem As String, ByVal DocumentType As String, ByVal ItemQty As Double, Optional ByVal Ean As String = "", Optional ByVal cstTaxCode As String = "", Optional ByVal considerCst As Boolean = False) As DataTable
        Try
            Dim strQuery As String
            'chnged by ram 04/03/2009 to add the purchased qty 
            'strQuery = "SELECT 0 as BILLLINENO,Convert(varchar(26),'" & Ean & "') as EAN,A.ArticleCode,A.STEPNO,A.TAXCODE,INCLUSIVE,FROMSTEPNO,TOSTEPNO,TYPE,APPLIEDON,VALUE,0 AS TAXABLE_AMOUNT,0 AS TAXAMOUNT FROM TAXDETAIL A INNER JOIN TAXSITEDOCMAP B ON A.SITECODE=B.SITECODE AND A.TAXCODE=B.TAXCODE AND A.STEPNO=B.STEPNO Where A.SITECODE='" & sitecode & "' AND B.DocumentType='" & DocumentType & "'"
            'strQuery = "SELECT 0 as BILLLINENO,Convert(varchar(26),'" & Ean & "') as EAN,A.ArticleCode,A.STEPNO,A.TAXCODE,INCLUSIVE,FROMSTEPNO,TOSTEPNO,TYPE,APPLIEDON,VALUE,Convert(numeric(18,3),0) AS TAXABLE_AMOUNT,Convert(numeric(18,3),0) AS TAXAMOUNT, " & ItemQty & " AS ITEMQTY FROM TAXDETAIL A INNER JOIN TAXSITEDOCMAP B ON A.SITECODE=B.SITECODE AND A.TAXCODE=B.TAXCODE AND A.STEPNO=B.STEPNO Where A.SITECODE='" & sitecode & "' AND B.DocumentType='" & DocumentType & "'"

            If stritem <> String.Empty Then
                'strQuery = "SELECT 0 AS BILLLINENO,CONVERT(VARCHAR(26),'" & Ean & "') AS EAN,B.ARTICLECODE,A.SEQUENCE AS STEPNO,A.TAXCODE,A.INCLUSIVE,0 AS FROMSTEPNO,0 AS TOSTEPNO,A.ISPERCENTAGEVALUE,A.APPLIEDON,A.TAXVALUE AS VALUE,CONVERT(NUMERIC(18,3),0) AS TAXABLE_AMOUNT,CONVERT(NUMERIC(18,3),0) AS TAXAMOUNT, " & ItemQty & " AS ITEMQTY  " & _
                '   "FROM TAXSITEDOCMAP A INNER JOIN SITEARTICLETAXMAPPING B ON A.SITECODE=B.SITECODE AND A.TAXCODE=B.TAXCODE Where A.Status=1 AND B.Status=1 AND A.IsDocumentLevelTax=0 AND A.SITECODE='" & sitecode & "' AND A.DocumentType='" & DocumentType & "' AND B.ARTICLECODE='" & stritem & "' Order by STEPNO"
                ' rahul comment the code for MOD Changes for composite tax calulation 

                strQuery = "SELECT 0 AS BILLLINENO,CONVERT(VARCHAR(26),'" & Ean & "') AS EAN,B.ARTICLECODE,A.SEQUENCE AS STEPNO,A.TAXCODE,A.INCLUSIVE,0 AS FROMSTEPNO,0 AS TOSTEPNO,A.ISPERCENTAGEVALUE,A.APPLIEDON,A.TAXVALUE AS VALUE,A.TAXTYPE,CONVERT(NUMERIC(18,3),0) AS TAXABLE_AMOUNT,CONVERT(NUMERIC(18,3),0) AS TAXAMOUNT, " & ItemQty & " AS ItemQty " & _
                " FROM TAXSITEDOCMAP A INNER JOIN SITEARTICLETAXMAPPING B ON A.SITECODE=B.SITECODE AND A.TAXCODE=B.TAXCODE Where A.Status=1 AND B.Status=1 AND B.Suppliercode='Internal' AND A.IsDocumentLevelTax=0 AND A.SITECODE='" & sitecode & "' AND A.DocumentType='" & DocumentType & "' AND B.ARTICLECODE='" & stritem & "'"
                If Not String.IsNullOrEmpty(cstTaxCode) Then
                    If considerCst Then
                        strQuery = strQuery & " And A.TAXCODE = '" & cstTaxCode & "'"
                    Else
                        strQuery = strQuery & " And A.TAXCODE <> '" & cstTaxCode & "'"
                    End If
                End If
                strQuery = strQuery & " Order by STEPNO"
            ElseIf stritem = String.Empty AndAlso considerCst Then
                strQuery = "SELECT 0 AS BILLLINENO,'' AS EAN,'' as ARTICLECODE,A.SEQUENCE AS STEPNO,A.TAXCODE,A.INCLUSIVE,0 AS FROMSTEPNO,0 AS TOSTEPNO,A.ISPERCENTAGEVALUE,A.APPLIEDON,A.TAXVALUE AS VALUE,A.TAXTYPE,CONVERT(NUMERIC(18,3),0) AS TAXABLE_AMOUNT,CONVERT(NUMERIC(18,3),0) AS TAXAMOUNT, 1 AS ItemQty  " & _
                           " FROM TAXSITEDOCMAP A Where  A.Status=1  AND A.SITECODE='" & sitecode & "' AND A.DocumentType='" & DocumentType & "' And A.TAXCODE = '" & cstTaxCode & "' "
                strQuery = strQuery & " Order by STEPNO"
            Else
                strQuery = "SELECT 0 AS BILLLINENO,'' AS EAN,'' as ARTICLECODE,A.SEQUENCE AS STEPNO,A.TAXCODE,A.INCLUSIVE,0 AS FROMSTEPNO,0 AS TOSTEPNO,A.ISPERCENTAGEVALUE,A.APPLIEDON,A.TAXVALUE AS VALUE,A.TAXTYPE,CONVERT(NUMERIC(18,3),0) AS TAXABLE_AMOUNT,CONVERT(NUMERIC(18,3),0) AS TAXAMOUNT,1 AS ItemQty  " & _
                           " FROM TAXSITEDOCMAP A Where  A.Status=1 AND  A.IsDocumentLevelTax=1 AND A.SITECODE='" & sitecode & "' AND A.DocumentType='" & DocumentType & "' "
                If Not String.IsNullOrEmpty(cstTaxCode) Then
                    'If considerCst Then
                    '    strQuery = strQuery & " And A.TAXCODE = '" & cstTaxCode & "'"
                    'Else
                    strQuery = strQuery & " And A.TAXCODE <> '" & cstTaxCode & "'"
                    'End If
                End If
                strQuery = strQuery & " Order by STEPNO"
            End If
            Dim da As New SqlDataAdapter(strQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            'RearrangeTaxDetail(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function checkIGSTAplicableForOutSideStateCustomer(ByVal StoreSiteCode As String, ByVal CustomerNo As String) As Boolean
        Try
            Dim StrQuery As New StringBuilder
            StrQuery.Length = 0
            Dim StoreSateCode As String = ""
            Dim CutomerSateCode As String = ""
            StrQuery.Append(" select StateCode from mstSite  where SiteCode='" & StoreSiteCode & "' ;")
            StrQuery.Append("select StateCode from CLPCustomerAddress  where  Defaults=1  and cardNo='" & CustomerNo & "' ;")
            Dim da As New SqlDataAdapter(StrQuery.ToString(), ConString)
            Dim ds As New DataSet
            da.Fill(ds)
            ds.Tables(0).TableName = "MStSite"
            ds.Tables(1).TableName = "CLPCustomerAddress"

            If ds.Tables("CLPCustomerAddress").Rows(0)(0) <> "" Then
                If ds.Tables("MStSite").Rows(0)(0) <> ds.Tables("CLPCustomerAddress").Rows(0)(0) Then
                    Return True
                End If
            End If

            Return False

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function ReturnIGSTTaxID(ByVal dt As DataTable) As String
        Dim StrQuery As New StringBuilder

        Dim taxcode As String = ""
        For Each dr As DataRow In dt.Rows
            StrQuery.Length = 0
            StrQuery.Append(" select TaxCode,TaxName,STATUS from MstTax where status=1 and TaxCode ='" & dr("TaxCode").ToString() & "' and InterStateTax=1;")
            Dim da As New SqlDataAdapter(StrQuery.ToString(), ConString)
            Dim dtt As New DataTable
            da.Fill(dtt)
            If dtt.Rows.Count > 0 Then
                taxcode = dtt.Rows(0)("TaxCode").ToString()
                Exit For
            Else
                taxcode = ""
            End If

        Next
        Return taxcode
    End Function

    'Public Function RearrangeTaxDetail(ByRef dt As DataTable) As DataTable
    '    Try
    '        Dim i As Int16 = 0
    '        For Each dr As DataRow In dt.Rows
    '            dr("StepNo") = i
    '            'dr("FromStepNo") = dt.Rows(0)("StepNo")
    '            'dr("ToStepNo") = dt.Rows(0)("StepNo")
    '            If dr("APPLIEDON").ToString.Contains("Total") Then
    '                Dim str As String = dr("APPLIEDON").ToString()
    '                str = str.Replace("Total(", "")
    '                str = str.Replace(")", "")
    '                Dim c(0) As Char
    '                c(0) = ","
    '                Dim TaxCodes As String()
    '                TaxCodes = str.Split(c)
    '                For Each tax As String In TaxCodes
    '                    Dim dv As New DataView(dt, "TaxCode='" & tax & "'", "StepNo", DataViewRowState.CurrentRows)
    '                    If dv.Count > 0 Then
    '                        For Each StepRow As DataRowView In dv
    '                            dr("FromStepNo") = IIf(dr("FromStepNo") <= StepRow("StepNo"), dr("FromStepNo"), StepRow("StepNo"))
    '                            dr("ToStepNo") = IIf(dr("ToStepNo") > StepRow("StepNo"), dr("ToStepNo"), StepRow("StepNo"))
    '                        Next
    '                    Else
    '                        dr("FromStepNo") = dt.Rows(0)("StepNo") 'IIf(dr("FromStepNo") < StepRow("StepNo"), dr("FromStepNo"), StepRow("StepNo"))
    '                        'dr("ToStepNo") = dt.Rows(0)("StepNo") 'IIf(dr("ToStepNo") > StepRow("StepNo"), dr("ToStepNo"), StepRow("StepNo"))
    '                    End If
    '                Next
    '            Else
    '                'Dim dv As New DataView(dt
    '                dr("FromStepNo") = dt.Rows(0)("StepNo")
    '                dr("ToStepNo") = dt.Rows(0)("StepNo")
    '            End If
    '            i = i + 1
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Function
    ' ''' <summary>
    ' ''' CalCulate the Tax
    ' ''' </summary>
    ' ''' <param name="dsPosTax">TaxTable</param>
    ' ''' <returns>True/False</returns>
    ' ''' <remarks></remarks>
    ' '''Public Function getCalculatedDataSet(ByRef dsPosTax As DataTable) As Boolean
    'Public Function getCalculatedDataSet1(ByRef dsPosTax As DataTable, Optional ByVal ItemQty As Double = 1) As Boolean
    '    Try
    '        Dim baseAmount As Double
    '        Dim iCount As Int16
    '        Dim jCount As Int16
    '        With dsPosTax
    '            baseAmount = .Rows(0)("TAXABLE_AMOUNT")
    '            For iCount = 0 To .Rows.Count - 1
    '                If CDbl(.Rows(iCount)("INCLUSIVE")) = 0 Then
    '                    If .Rows(iCount)("FROMSTEPNO") = .Rows(iCount)("TOSTEPNO") Then
    '                        Dim onAmount As Double
    '                        If .Rows(iCount)("Appliedon").ToString().ToUpper() = "MRP" Then
    '                            onAmount = .Rows(.Rows(iCount)("FROMSTEPNO"))("TAXABLE_AMOUNT")
    '                        ElseIf .Rows(iCount)("Appliedon").ToString().ToUpper() <> "MRP" Then
    '                            onAmount = .Rows(.Rows(iCount)("FROMSTEPNO"))("TAXAMOUNT")
    '                        End If
    '                        'If .Rows(iCount)("TYPE").ToString().ToUpper() = "PER" Then
    '                        If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = onAmount + (onAmount * .Rows(iCount)("VALUE") / 100)
    '                            .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("TAXABLE_AMOUNT") - onAmount
    '                            'ElseIf .Rows(iCount)("TYPE").ToString().ToUpper() = "VAL" Then
    '                        ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = onAmount + .Rows(iCount)("VALUE") * ItemQty
    '                            .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * ItemQty
    '                        End If

    '                    Else
    '                        For jCount = .Rows(iCount)("FROMSTEPNO") To .Rows(iCount)("TOSTEPNO")
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = .Rows(iCount)("TAXABLE_AMOUNT") + .Rows(jCount)("TAXAMOUNT")
    '                        Next
    '                        If .Rows(iCount)("Appliedon").ToString().ToUpper() = "MRP" Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = .Rows(iCount)("TAXABLE_AMOUNT") + baseAmount
    '                        End If
    '                        If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
    '                            .Rows(iCount)("TAXAMOUNT") = (.Rows(iCount)("TAXABLE_AMOUNT") * .Rows(iCount)("VALUE")) / 100
    '                        ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
    '                            .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * ItemQty
    '                        End If
    '                        .Rows(iCount)("TAXABLE_AMOUNT") = .Rows(iCount)("TAXABLE_AMOUNT") + .Rows(iCount)("TAXAMOUNT") * ItemQty
    '                    End If

    '                Else
    '                    If .Rows(iCount)("FROMSTEPNO") = .Rows(iCount)("TOSTEPNO") Then
    '                        Dim onAmount As Double = .Rows(.Rows(iCount)("FROMSTEPNO"))("TAXABLE_AMOUNT")
    '                        If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = (onAmount * 100 / (.Rows(iCount)("VALUE") + 100))
    '                            .Rows(iCount)("TAXAMOUNT") = onAmount - .Rows(iCount)("TAXABLE_AMOUNT")
    '                        ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = onAmount - (.Rows(iCount)("VALUE")) * ItemQty
    '                            .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * ItemQty
    '                        End If

    '                    Else
    '                        For jCount = .Rows(iCount)("FROMSTEPNO") To .Rows(iCount)("TOSTEPNO")
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = .Rows(iCount)("TAXABLE_AMOUNT") + .Rows(jCount)("TAXAMOUNT")
    '                        Next
    '                        .Rows(iCount)("TAXABLE_AMOUNT") = baseAmount - .Rows(iCount)("TAXABLE_AMOUNT")
    '                        'Dim onAmount As Double = .Rows(.Rows(iCount)("FROMSTEPNO"))("TAXABLE_AMOUNT")
    '                        Dim onAmount As Double = .Rows(iCount)("TAXABLE_AMOUNT")
    '                        If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = (onAmount * 100 / (.Rows(iCount)("VALUE") + 100))
    '                            .Rows(iCount)("TAXAMOUNT") = onAmount - .Rows(iCount)("TAXABLE_AMOUNT")
    '                        ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
    '                            '.Rows(iCount)("TAXABLE_AMOUNT") = onAmount - (.Rows(iCount)("VALUE")) 
    '                            ' changed by ram 04/03/2009
    '                            .Rows(iCount)("TAXABLE_AMOUNT") = onAmount - (.Rows(iCount)("VALUE")) * ItemQty
    '                            .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * ItemQty
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End With
    '        getCalculatedDataSet1 = True
    '    Catch ex As Exception
    '        LogException(ex)
    '        getCalculatedDataSet1 = False
    '    End Try
    'End Function
    Public Function getCalculatedDataSet(ByRef dsPosTax As DataTable) As Boolean
        Try
            Dim baseAmount As Double
            Dim iCount As Int16
            Dim jCount As Int16
            With dsPosTax
                baseAmount = .Rows(0)("TAXABLE_AMOUNT")
                For iCount = 0 To .Rows.Count - 1
                    Dim onAmount As Double
                    If .Rows(iCount)("Appliedon").ToString().ToUpper() = "MRP" Then
                        onAmount = baseAmount
                    ElseIf .Rows(iCount)("Appliedon").ToString().ToUpper() <> "MRP" Then
                        If .Rows(iCount)("Appliedon").ToString.Contains("Total") Then
                            onAmount = 0
                            Dim str As String = .Rows(iCount)("Appliedon").ToString()
                            str = str.Replace("Total(", "")
                            str = str.Replace(")", "")
                            Dim c(0) As Char
                            c(0) = ","
                            Dim TaxCodes As String()
                            TaxCodes = str.Split(c)
                            str = ""
                            For Each tax As String In TaxCodes
                                If tax = "MRP" Then
                                    onAmount = onAmount + baseAmount
                                Else
                                    Dim drTax = dsPosTax.Select("StepNo='" + tax + "'").FirstOrDefault()
                                    str = str & "'" & drTax("TaxCode") & "',"
                                    'str = str & "'" & tax & "',"
                                End If

                            Next
                            str = str.Substring(0, str.Length - 1)
                            Dim dv As New DataView(dsPosTax, "TaxCode in (" & str & ")", "StepNo", DataViewRowState.CurrentRows)
                            For Each drtax As DataRowView In dv
                                onAmount = onAmount + drtax("TAXAMOUNT")
                            Next
                        End If
                        'onAmount = 0

                    End If
                    If Not IsDBNull(.Rows(iCount)("INCLUSIVE")) Then
                        If .Rows(iCount)("INCLUSIVE") = False And .Rows(iCount)("TAXTYPE").ToString().ToUpper() <> "Composite" Then
                            If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount
                                .Rows(iCount)("TAXAMOUNT") = (onAmount * .Rows(iCount)("VALUE") / 100) ' .Rows(iCount)("TAXABLE_AMOUNT") - onAmount
                            ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount '+ .Rows(iCount)("VALUE") '* ItemQty
                                .Rows(iCount)("TAXAMOUNT") = 0
                                If Val(onAmount) > 0 Then
                                    .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * .Rows(iCount)("ItemQty")
                                End If
                            End If
                        ElseIf .Rows(iCount)("INCLUSIVE") = True AndAlso .Rows(iCount)("TAXTYPE").ToString() <> "Composite" Then
                            If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
                                'If isInclusiveCalc Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount
                                .Rows(iCount)("TAXAMOUNT") = onAmount * (.Rows(iCount)("VALUE")) / (100 + .Rows(iCount)("VALUE"))
                                ''Else
                                '    .Rows(iCount)("TAXABLE_AMOUNT") = (onAmount * 100 / (.Rows(iCount)("VALUE") + 100))
                                '    .Rows(iCount)("TAXAMOUNT") = onAmount - .Rows(iCount)("TAXABLE_AMOUNT")
                                ''End If
                            ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount '- (.Rows(iCount)("VALUE")) '* ItemQty
                                .Rows(iCount)("TAXAMOUNT") = 0
                                If Val(onAmount) > 0 Then
                                    .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE") * .Rows(iCount)("ItemQty")
                                End If
                            End If
                        ElseIf (.Rows(iCount)("TAXTYPE").ToString() = "Composite") Then
                            If .Rows(iCount)("ISPERCENTAGEVALUE") = True Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount
                                .Rows(iCount)("TAXAMOUNT") = (onAmount * .Rows(iCount)("VALUE")) / 100
                            ElseIf .Rows(iCount)("ISPERCENTAGEVALUE") = False Then
                                .Rows(iCount)("TAXABLE_AMOUNT") = onAmount
                                .Rows(iCount)("TAXAMOUNT") = 0
                                If Val(onAmount) > 0 Then
                                    .Rows(iCount)("TAXAMOUNT") = .Rows(iCount)("VALUE")
                                End If
                            End If

                        End If

                    End If
                    'Changed By Gaurav Danani to perform round off instead of FormatNumber
                    '.Rows(iCount)("TAXAMOUNT") = FormatNumber(.Rows(iCount)("TAXAMOUNT"), 2)
                    '.Rows(iCount)("TAXAMOUNT") = Math.Round(.Rows(iCount)("TAXAMOUNT"), DecimalDigits)
                    'Changed By Sameer for Rounding issue after promotion Issue ID 7624
                    '.Rows(iCount)("TAXAMOUNT") = Math.Round(.Rows(iCount)("TAXAMOUNT") / ItemQty, DecimalDigits) * ItemQty
                    'Change Complete
                Next
            End With
            getCalculatedDataSet = True
        Catch ex As Exception
            LogException(ex)
            getCalculatedDataSet = False
        End Try
    End Function
    Public Shared Function GetDefaultConfigValue(ByVal siteCode As String, ByVal fldLabel As String) As String
        Try
            Dim query As String = String.Empty

            If (String.IsNullOrEmpty(siteCode)) Then
                query = "select FldValue from DefaultConfig where FldLabel = '" & fldLabel & "' "
            Else
                query = "select FldValue from DefaultConfig where FldLabel = '" & fldLabel & "' and Sitecode = '" & siteCode & "'"
            End If

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    'added by khusrao adil on 04-07-2018 for Dynamic License Duration setting
    Public Shared Function GetLicenseDurationDays() As Integer
        Try
            Dim strString As String = "Select * From DefaultConfig Where FldLabel='OffLineLicenseActivationDuration' and SiteCode='BOCommon'"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("FldValue") = True Then
                    Return Convert.ToInt32(dt.Rows(0)("FldValue"))
                End If
            End If
            Return 30
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Birthlist 
    ''' </summary>
    ''' <param name="CVProgram"></param>
    ''' <param name="DocType"></param>
    ''' <param name="NewVoucher"></param>
    ''' <param name="CMNo"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="ServerDateTime"></param>
    ''' <param name="UserId"></param>
    ''' <param name="tran"></param>
    ''' <param name="con"></param>
    ''' <param name="Amount"></param>
    ''' <param name="VoucherNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCreditVoucher(ByVal CVProgram As String, ByVal DocType As String, ByVal NewVoucher As Boolean, ByVal CBirthListNO As String, ByVal SiteCode As String, _
            ByVal ServerDateTime As DateTime, ByVal UserId As String, ByVal dateExpiry As Date, ByRef tran As SqlTransaction, ByRef con As SqlConnection, _
            Optional ByVal Amount As Double = 0, Optional ByVal VoucherNo As String = "0", Optional ByRef genVoucherNo As String = "", Optional ByVal daysForExpiry As Integer = 0) As Boolean
        Try
            Dim cmdVoucher As SqlCommand
            Dim strQuery As String
            If NewVoucher Then
                Dim CVNo As String = getDocumentNo("CV", SiteCode)
                CVNo = CInt(CVNo) + 1

                'Changed by Rohit to generate Document No. for proper sorting
                'genVoucherNo = SiteCode + CVNo
                Try
                    Dim strFinyear As String = GetFinancialYear(ServerDateTime, SiteCode)
                    genVoucherNo = GenDocNo("CV" & SiteCode.Substring(SiteCode.Length - 4, 4) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, CVNo)
                Catch ex As Exception
                    genVoucherNo = "CV" & SiteCode + CVNo
                End Try
                'End Change by Rohit

                'strQuery = "INSERT INTO VOUCHERDTLS(SITECODE,VOUCHERCODE,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE," & _
                '"ISISSUED,ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,EXPIRYDATE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                '"VALUES('" & SiteCode & "','" & CVProgram & "','" & CVNo & "'," & Amount & ",1,1,'" & SiteCode & "'," & _
                '        " '" & ServerDateTime & "' ,'" & DocType & "','" & CBirthListNO & "','" & dateExpiry & "','" & SiteCode & "','" & UserId & "','" & ServerDateTime & "','" & SiteCode & "','" & UserId & "','" & ServerDateTime & "',1)"

                'strQuery = "INSERT INTO VOUCHERDTLS(SITECODE,VOUCHERCODE,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE," & _
                '        "ISISSUED,ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,EXPIRYDATE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                '        "VALUES('" & SiteCode & "','" & CVProgram & "','" & CVNo & "'," & Amount & ",1,1,'" & SiteCode & "'," & _
                '        " getdate() ,'" & DocType & "','" & CBirthListNO & "', Convert(DateTime, '" & dateExpiry & "')  ,'" & SiteCode & "','" & UserId & "',getdate(),'" & SiteCode & "','" & UserId & "',getdate(),1)"


                strQuery = "INSERT INTO VOUCHERDTLS(SITECODE,VOUCHERCODE,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE," & _
                    "ISISSUED,ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,EXPIRYDATE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                    "VALUES('" & SiteCode & "','" & CVProgram & "','" & genVoucherNo & "'," & ConvertToEnglish(Amount) & ",1,1,'" & SiteCode & "'," & _
                    " getdate() ,'" & DocType & "','" & CBirthListNO & "' , getdate() +  " & daysForExpiry & "  ,'" & SiteCode & "','" & UserId & "',getdate(),'" & SiteCode & "','" & UserId & "',getdate(),1)"


                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    If UpdateDocumentNo("CV", con, tran, CVNo) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                strQuery = "UPDATE VOUCHERDTLS SET RedeemedDocNumber='" & CBirthListNO & "',RedeemedAtSite='" & SiteCode & "',RedeemedInDoctype='" & DocType & "',RedeemedOnDate='" & Format(ServerDateTime, "yyyyMMdd") & _
                "',IsRedeemed=1, UPDATEDAT='" & SiteCode & "',UPDATEDBY='" & UserId & "',UPDATEDON='" & Format(ServerDateTime, "yyyyMMdd") & "'   WHERE SITECODE='" & SiteCode & "'And CREDITVOUCHERNO='" & VoucherNo & "'"
                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function CreateDiscSummary(ByVal dayOpendate As DateTime, ByRef dtMain As DataTable, ByVal filterCondition As String, ByVal DocumentType As String, _
                                        ByVal SiteCode As String, ByVal Fyear As String, ByVal Billno As String, ByVal userId As String, _
                                        ByVal ServerDate As DateTime, ByVal ParamArray Columns() As String) As DataTable
        Try
            Dim dtDisc As New DataTable("SalesDiscDtl")
            AddColumnToDataTable(dtDisc, "SITECODE", "System.String", SiteCode)
            AddColumnToDataTable(dtDisc, "FINYEAR", "System.String", Fyear)
            AddColumnToDataTable(dtDisc, "BILLNO", "System.String", Billno)
            AddColumnToDataTable(dtDisc, "DocumentType", "System.String", DocumentType)
            AddColumnToDataTable(dtDisc, "PromotionID", "System.String")
            AddColumnToDataTable(dtDisc, "PromotionValue", "System.String")
            AddColumnToDataTable(dtDisc, "CREATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(dtDisc, "CREATEDBY", "System.String", userId)
            AddColumnToDataTable(dtDisc, "BillDate", "System.DateTime", dayOpendate)
            AddColumnToDataTable(dtDisc, "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(dtDisc, "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(dtDisc, "UPDATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(dtDisc, "UPDATEDBY", "System.String", userId)
            AddColumnToDataTable(dtDisc, "UPDATEDON", "System.DateTime", ServerDate)
            For Each strColumn As String In Columns
                Dim dvpromo As DataView
                If Not filterCondition = String.Empty Then
                    dvpromo = New DataView(dtMain, filterCondition & " AND " & strColumn & " IS NOT NULL AND " & strColumn & "<>''", strColumn, DataViewRowState.CurrentRows)
                Else
                    dvpromo = New DataView(dtMain, strColumn & " IS NOT NULL AND " & strColumn & "<>'' AND " & strColumn & "<> '0'", strColumn, DataViewRowState.CurrentRows)
                End If

                'Dim dtunique As DataTable = dvpromo.ToTable(True, strColumn)
                'For Each drDisc As DataRow In dtunique.Rows

                For Each drmainDataRow As DataRowView In dvpromo
                    Dim filter As String
                    Dim PromoValue As Double = 0
                    Dim PromoId As String
                    If strColumn.ToUpper().Contains("MANUAL") Then
                        filter = "PromotionID='" & drmainDataRow("TOPLEVEL").ToString().Trim() & "'"
                        PromoId = drmainDataRow("TOPLEVEL").ToString().Trim()
                        PromoValue = IIf(drmainDataRow("LineDiscount") Is DBNull.Value, 0, drmainDataRow("LineDiscount"))
                    Else
                        filter = "PromotionID='" & drmainDataRow(strColumn).ToString().Trim() & "'"
                        PromoId = drmainDataRow(strColumn).ToString().Trim()
                        PromoValue = IIf(drmainDataRow(strColumn & "DISC") Is DBNull.Value, 0, drmainDataRow(strColumn & "DISC"))
                    End If
                    Dim dvExistRow As New DataView(dtDisc, filter, "", DataViewRowState.CurrentRows)
                    If dvExistRow.Count > 0 Then
                        dvExistRow.AllowEdit = True
                        dvExistRow(0)("PromotionValue") = dvExistRow(0)("PromotionValue") + PromoValue
                    Else
                        'If PromoValue > 0 Then
                        Dim drNewRow As DataRow = dtDisc.NewRow()
                        drNewRow("PromotionId") = PromoId
                        drNewRow("PromotionValue") = PromoValue
                        dtDisc.Rows.Add(drNewRow)
                        'End If
                    End If
                Next
                'Next 
            Next
            Return dtDisc

        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function CheckExistBillNo(ByVal BillNo As String) As Boolean
        Try
            Dim query As String = ""
            query = "select * from CashMemoHdr where BillNo= '" & BillNo & "'"
            Dim ds As New DataTable
            Dim daDefault As New SqlDataAdapter(query, ConString)
            daDefault.Fill(ds)
            If ds.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Public Function GetPaymentInfo() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            Dim daScan As SqlDataAdapter
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'1') As SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'Cash') As Reciept, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'Cash') As RecieptType, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) As Amount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'1') As Number, " & vbCrLf)
            vStmtQry.Append(" getdate() As Date, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'Cash') As RecieptTypeCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'1') As ExchangeRate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),' ') As CurrencyCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) As AmountInCurrency " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetVoucherDetails(ByVal BillNo As String, ByVal SiteCode As String, ByVal DocType As String, Optional ByVal Voucherno As String = "", Optional ByVal Value As Double = 0) As DataTable
        Try
            Dim strString As String = String.Empty
            Dim dt As DataTable

            strString = "SELECT A.VOUCHERCODE, A.VALUEOFVOUCHER, A.VOURCHERSERIALNBR, A.EXPIRYDATE,B.VOUCHERDESC FROM VOUCHERDTLS A INNER JOIN MSTVOUCHER B ON A.VOUCHERCODE=B.VOUCHERCODE " & vbCrLf
            strString = strString & " WHERE A.ISSUEDDOCNUMBER='" & BillNo & "' AND A.ISSUEDATSITE='" & SiteCode & "' AND A.ISSUEDINDOCTYPE='" & DocType & "'"
            If Voucherno <> String.Empty Then
                strString = strString & " AND VOURCHERSERIALNBR='" & Voucherno & "'"
            End If
            If Value <> 0 Then
                Dim dval As Double = IIf(Value > 0, Value, Value * -1)
                strString = strString & " AND ValueofVoucher=" & ConvertToEnglish(dval)
            End If
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            'LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTermsNCondition(ByVal strSiteCode As String, ByVal DocType As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from  dbo.MstTermsNConditon where sitecode='" + strSiteCode + "' and DocType='" + DocType + "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            'LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPrinterMapping(ByVal pTerminal As String) As DataTable
        Try
            Dim daPrinter As SqlDataAdapter
            Dim qryPrinter As String = "Select TerminalID, PrinterDocument, PrinterType, PrinterName " + _
                                       "from PrinterTillMap Where TerminalID='" & pTerminal & "'"
            daPrinter = New SqlClient.SqlDataAdapter(qryPrinter, SpectrumCon)
            Dim dtPrinterTp As New DataTable
            daPrinter.Fill(dtPrinterTp)

            Return dtPrinterTp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'Added By Rohit for Cr-5938

    ''' <summary>
    ''' Preparing Creadit Check Data
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function PrepareCreditCheckData(ByRef ds As DataSet, ByVal sitecode As String, ByVal UserID As String, ByVal Fyear As String, ByVal doctype As String, ByVal docNo As String, ByVal documentNo As String, ByVal ServerDate As Date, ByVal dDueDate As Date, ByVal strRemarks As String, ByVal strTransType As String, ByVal dbilldate As Date, Optional ByVal isUpdate As Boolean = False)
        Try
            If ds.Tables.Contains("CheckDtls") Then

                Dim dvCheckDtls As DataView
                If strTransType = "CashMemo" Then
                    Dim dvPayment As New DataView(ds.Tables("CASHMEMORECEIPT"), "TENDERHEADCODE IN ('CreditCheque','Cheque')", "CMRECPTLINENO", DataViewRowState.CurrentRows)
                    dvCheckDtls = dvPayment
                ElseIf strTransType = "SO" Then
                    Dim dvPayment As New DataView(ds.Tables("SalesInvoice"), "TENDERHEADCODE IN ('CreditCheque','Cheque')", "SaleInvLineNumber", DataViewRowState.CurrentRows)
                    dvCheckDtls = dvPayment
                End If

                If dvCheckDtls.Count > 0 Then

                    Dim drVwCheckDtls As DataRowView
                    drVwCheckDtls = dvCheckDtls.Item(0)
                    For Each drCheckDtls As DataRow In ds.Tables("CheckDtls").Rows
                        drCheckDtls("SiteCode") = drVwCheckDtls("SiteCode")
                        drCheckDtls("FinYear") = drVwCheckDtls("FinYear")
                        drCheckDtls("BillNo") = docNo
                        drCheckDtls("DocumentNo") = documentNo
                        drCheckDtls("DocumentType") = doctype
                        drCheckDtls("BillDate") = dbilldate.Date
                        drCheckDtls("CREATEDAT") = drVwCheckDtls("CREATEDAT")
                        drCheckDtls("CREATEDBY") = drVwCheckDtls("CREATEDBY")
                        drCheckDtls("CREATEDON") = drVwCheckDtls("CREATEDON")
                        drCheckDtls("UPDATEDAT") = drVwCheckDtls("UPDATEDAT")
                        drCheckDtls("UPDATEDBY") = drVwCheckDtls("UPDATEDBY")
                        drCheckDtls("UPDATEDON") = drVwCheckDtls("UPDATEDON")

                    Next

                    ds.Tables("CheckDtls").AcceptChanges()
                    If ds.Tables.Contains("AuditCheckDtls") Then
                        ds.Tables.Remove("AuditCheckDtls")
                    End If
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try


    End Function

    Public Function UpdateCashier(ByVal userid As String, ByVal sitecode As String, ByVal Trminal As String, ByVal Billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            ''Updated By ketan Change Tender code Issue PC
            'Dim cmdTrn As New SqlCommand("UPDATE SALESORDERHDR SET UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',CREATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "',TERMINALID = '" & Trminal & "'  WHERE SITECODE='" & sitecode & "' AND   SaleOrderNumber='" & Billno & "'", con, tran)
            Dim cmdTrn As New SqlCommand("UPDATE SALESORDERHDR SET UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',CREATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' AND   SaleOrderNumber='" & Billno & "'", con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
#Region "niq-naq"
    Public Function SaveAndUpdateKOTPrintDetails(ByVal _SiteCode As String, ByVal _billNo As String, ByVal _terminalId As String, ByVal _userid As String, ByVal isUpdate As Boolean, ByVal _reason As String) As Boolean
        Try
            SaveAndUpdateKOTPrintDetails = False
            Dim strQuery As String = ""
            If isUpdate = True Then
                strQuery = "update KOTReprint set NoOfPrints=NoOfPrints+1,Reason='" & _reason & "' where Sitecode='" & _SiteCode & "' and BillNo='" & _billNo & "' and TerminalId='" & _terminalId & "'"
            Else
                strQuery = "Insert into KOTReprint(SiteCode,BillNo,TerminalID,Reason,NoOfPrints,CreatedAt,CreatedBy,CreatedOn,UpdatedAt,UpdatedBy,UpdatedOn,Status) values ('" &
                        _SiteCode & "','" & _billNo & "','" & _terminalId & "','','1','" & _SiteCode & "','" & _userid & "',GetDate(),'" & _SiteCode & "','" &
                        _userid & "',GetDate(),'1')"
            End If
            OpenConnection()
            Dim cmd As New SqlCommand(strQuery, SpectrumCon)
            If cmd.ExecuteNonQuery() > 0 Then
                SaveAndUpdateKOTPrintDetails = True
            End If
            Return SaveAndUpdateKOTPrintDetails
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function CheckKOTPrintExist(ByVal _SiteCode As String, ByVal _billNo As String, ByVal _terminalId As String) As Boolean
        CheckKOTPrintExist = False
        Try
            Dim strQuery As String = ""
            strQuery = "Select NoOfPrints from KOTReprint where Sitecode='" & _SiteCode & "' and BillNo='" & _billNo & "' and TerminalId='" & _terminalId & "' and status=1"
            OpenConnection()
            Dim cmd As New SqlCommand(strQuery, SpectrumCon)
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Dim strCount As Integer
                Dim Countvalue As Object = dr.GetValue(dr.GetOrdinal("NoOfPrints"))
                strCount = Convert.ToInt32(Countvalue)
                ' If dr("NoOfPrints") > 2 Then
                If strCount >= 1 Then
                    CheckKOTPrintExist = True
                Else
                    CheckKOTPrintExist = False
                End If
            End If
            Return CheckKOTPrintExist
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function
#End Region
    ''' <summary>
    ''' Retrieve credit cheque details for the current cash memo
    ''' </summary>
    ''' <param name="strBillNo">Bill No</param>
    '''  <param name="strSitecode">Site Code</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>

    Public Function GetDtCheckDtls(ByVal strBillNo As String, ByVal strSitecode As String) As DataTable
        Try
            Dim dtData As New DataTable
            Dim strQuery As String = "SELECT SiteCode, FinYear, BillDate, BillNo, PayLineNo, Amount, CheckNo, DocumentNo, DocumentType, DueDate, Remarks, CREATEDAT, CREATEDBY," & _
                    "CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS FROM CheckDtls WHERE SiteCode = '" & strSitecode & "' and BillNo = '" & strBillNo & "' and STATUS = 1 "
            OpenConnection()
            Dim daCheckdtls As New SqlDataAdapter(strQuery, SpectrumCon)
            daCheckdtls.Fill(dtData)
            CloseConnection()
            Return dtData

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function UpdateCheckDtls(ByVal DsData As DataSet, ByVal DocType As String, ByRef dtCheck As DataTable, Optional ByVal strBillNo As String = "", Optional ByVal strSiteCode As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim dvReceipt As DataView
            ' Dim tempDtCheck As New DataTable

            dt = GetDtCheckDtls(strBillNo, strSiteCode)


            If Not dtCheck Is Nothing And dt.Rows.Count > 0 Then
                'tempDtCheck = dtCheck.Copy
                dtCheck.Merge(dt, True, MissingSchemaAction.Ignore)

                For Each drCheck As DataRow In dtCheck.Rows
                    If DocType = "SO" Then
                        dvReceipt = New DataView(DsData.Tables("SalesInvoice"), "TenderHeadCode = 'CreditCheque' and RefNo_2 = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    Else
                        dvReceipt = New DataView(DsData.Tables("CASHMEMORECEIPT"), "TenderHeadCode = 'CreditCheque' and CardNo = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    End If

                    If dvReceipt.Count = 0 Then
                        drCheck.Delete()
                        'tempDtCheck.AcceptChanges()
                    End If
                Next
                'dtCheck.Clear()
                'dtCheck.Merge(tempDtCheck, True)
                dtCheck.AcceptChanges()
            ElseIf dt.Rows.Count > 0 Then
                dt.TableName = "CheckDtls"
                For Each drCheck As DataRow In dt.Rows
                    If DocType = "SO" Then
                        dvReceipt = New DataView(DsData.Tables("SalesInvoice"), "TenderHeadCode = 'CreditCheque' and RefNo_2 = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    Else
                        dvReceipt = New DataView(DsData.Tables("CASHMEMORECEIPT"), "TenderHeadCode = 'CreditCheque' and CardNo = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    End If

                    If dvReceipt.Count = 0 Then
                        drCheck.Delete()
                    End If
                Next
            End If

            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            LogException(ex)
            Return (Nothing)

        End Try

    End Function

    Public Function DeleteCheckDtls(ByVal DsData As DataSet, ByVal DocType As String, ByRef dtCheck As DataTable, Optional ByVal strBillNo As String = "", Optional ByVal strSiteCode As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim dvReceipt As DataView
            Dim strQuery As String
            Dim tran As SqlTransaction = Nothing


            dt = GetDtCheckDtls(strBillNo, strSiteCode)


            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                ' tempDtCheck = dtCheck.Copy
                'dtCheck.Merge(dt, True, MissingSchemaAction.Ignore)

                For Each drCheck As DataRow In dt.Rows
                    If DocType = "SO" Then
                        dvReceipt = New DataView(DsData.Tables("SalesInvoice"), "TenderHeadCode = 'CreditCheque' and RefNo_2 = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    Else
                        dvReceipt = New DataView(DsData.Tables("CASHMEMORECEIPT"), "TenderHeadCode = 'CreditCheque' and CardNo = '" & drCheck("CheckNo") & "'", "TenderHeadCode", DataViewRowState.CurrentRows)
                    End If

                    Dim dtAuditCheckDtls As New DataTable
                    dtAuditCheckDtls = DsData.Tables("CheckDtls").Copy

                    OpenConnection()
                    Dim dtaCheckDtls As New SqlDataAdapter("Select * from CheckDtls where BillNo = '" & strBillNo & "' and SiteCode = '" & strSiteCode & "' and CheckNo = '" & drCheck("CheckNo") & "'", SpectrumCon)
                    dtaCheckDtls.Fill(dtAuditCheckDtls)
                    dtAuditCheckDtls.TableName = "AuditCheckDtls"
                    CloseConnection()

                    If dvReceipt.Count = 0 Then
                        strQuery = "Delete from CheckDtls where BillNo = '" & strBillNo & "' and SiteCode = '" & strSiteCode & "' and CheckNo = '" & drCheck("CheckNo") & "'"
                        OpenConnection()
                        tran = SpectrumCon.BeginTransaction
                        Dim daCheckDtls As New SqlCommand(strQuery, SpectrumCon)
                        daCheckDtls.Transaction = tran
                        daCheckDtls.ExecuteNonQuery()
                        tran.Commit()
                        CloseConnection()
                        'tempDtCheck.AcceptChanges()
                    End If

                    If Not dtAuditCheckDtls Is Nothing Then
                        DsData.Tables.Add(dtAuditCheckDtls)
                        AddMode(DsData.Tables("AuditCheckDtls"))
                    End If
                Next
                'dtCheck.Clear()
                'dtCheck.Merge(tempDtCheck, True)

            End If



            Return dt
        Catch ex As Exception
            LogException(ex)
            Return (Nothing)

        End Try

    End Function

    ''' <summary>
    ''' Create CheckDtls Table
    ''' </summary>    
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>

    Public Function CreateCheckDtls() As DataTable
        Try
            Dim dt As New DataTable
            dt.TableName = "CheckDtls"
            dt.AcceptChanges()
            AddColumnToDataTable(dt, "SiteCode", "System.String")
            AddColumnToDataTable(dt, "FinYear", "System.String")
            AddColumnToDataTable(dt, "BillNo", "System.String")
            AddColumnToDataTable(dt, "PayLineNo", "System.Int32")
            AddColumnToDataTable(dt, "CheckNo", "System.String")
            AddColumnToDataTable(dt, "DocumentNo", "System.String")
            AddColumnToDataTable(dt, "DocumentType", "System.String")
            AddColumnToDataTable(dt, "BillDate", "System.DateTime")
            AddColumnToDataTable(dt, "Amount", "System.Decimal")
            AddColumnToDataTable(dt, "DueDate", "System.DateTime")
            AddColumnToDataTable(dt, "Remarks", "System.String")
            AddColumnToDataTable(dt, "CREATEDAT", "System.String")
            AddColumnToDataTable(dt, "CREATEDBY", "System.String")
            AddColumnToDataTable(dt, "CREATEDON", "System.DateTime")
            AddColumnToDataTable(dt, "UPDATEDAT", "System.String")
            AddColumnToDataTable(dt, "UPDATEDBY", "System.String")
            AddColumnToDataTable(dt, "UPDATEDON", "System.DateTime")
            AddColumnToDataTable(dt, "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(dt, "BankName", "System.String")
            AddColumnToDataTable(dt, "CustomerName", "System.String")
            AddColumnToDataTable(dt, "TelephoneNumber", "System.String")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function


    ''' <summary>
    ''' Generates Document No.
    ''' </summary>
    ''' <param name="strDocNo">Incomplete document no.</param>
    ''' <param name="iMaxLength">Max length of document no.</param>
    ''' <param name="strSuffix">Current incremented value to be suffixed</param>        
    ''' <remarks></remarks>
    Public Function GenDocNo(ByVal strDocNo As String, ByVal iMaxLength As Integer, ByVal strSuffix As String) As String
        If strDocNo.Length < iMaxLength Then
            Dim iTempLength As Integer = iMaxLength - strDocNo.Length
            For i As Integer = 0 To (iTempLength - strSuffix.Length) - 1
                strDocNo = strDocNo & "0"
            Next
            strDocNo = strDocNo & strSuffix
            Return strDocNo
        End If
        Return strDocNo
    End Function



    Public Function keylogged(ByVal key As String, ByVal formname As String, ByVal time As String, ByVal username As String, ByVal controlname As String, ByVal docno As String) As Boolean

        If dt.Columns.Count = 0 Then

            dt.Columns.Add("DateTime", System.Type.GetType("System.DateTime"))
            dt.Columns.Add("Form", System.Type.GetType("System.String"))
            dt.Columns.Add("Keylogged", System.Type.GetType("System.String"))
            dt.Columns.Add("UserName", System.Type.GetType("System.String"))
            dt.Columns.Add("ControlName", System.Type.GetType("System.String"))
            dt.Columns.Add("DocNO", System.Type.GetType("System.String"))
        End If

        Dim count As Integer = dt.Rows.Count
        Dim dr As DataRow
        dr = dt.NewRow()
        dt.Rows.Add(dr)
        dt.Rows(count)("datetime") = Convert.ToDateTime(time)
        dt.Rows(count)("form") = formname
        dt.Rows(count)("keylogged") = key
        dt.Rows(count)("UserName") = username
        dt.Rows(count)("ControlName") = controlname
        dt.Rows(count)("docno") = docno


        Return True
    End Function
    Public Function SaveKeylog() As Boolean

        Try
            Dim filepath As String = clsLogin.ReadSpectrumParamFile("CASHIERAUDITPATH")

            If filepath <> "" Then
                Dim strbul As New StringBuilder
                Dim strwriter As StreamWriter
                Dim file As System.IO.File
                Dim datetime As String = Convert.ToString(System.DateTime.Now)

                datetime = datetime.Replace(".", "")
                datetime = datetime.Replace("/", "-")
                datetime = datetime.Replace(":", "_")
                datetime = datetime.Replace(" ", "")
                filepath += "_" + datetime + ".txt"
                'filepath.AppendLine("D:\test\testfile")
                'filepath.AppendLine(Trim(datetime))

                'Dim filesecurity As Security.AccessControl.FileSecurity = file.GetAccessControl(filepath)
                'Dim accessrule As Security.AccessControl.FileSystemAccessRule = New Security.AccessControl.FileSystemAccessRule("creativeit\sameer.keluskar", Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow)
                'filesecurity.AddAccessRule(accessrule)

                'file.SetAccessControl(filepath, filesecurity)


                If file.Exists(filepath.ToString()) Then

                    strwriter = file.AppendText(filepath.ToString())

                Else

                    strwriter = file.CreateText(Trim(filepath.ToString()))
                    strbul.AppendLine("--------------------------------------------------------------" & vbCrLf)
                    strbul.AppendLine(dt(0)("DateTime"))
                    strbul.AppendLine(formatetable(dt))
                    strwriter.Write(strbul.ToString())
                    strwriter.Flush()
                    strwriter.Dispose()
                End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Protected Function GetReader(ByRef query As String) As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCmd As New SqlCommand(query, SpectrumCon)
            Return sqlCmd.ExecuteReader()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Function formatetable(ByVal dt As DataTable) As String
        Dim rcount As Int32 = 0
        Dim Ccount As Int32 = 0
        Dim str As String = ""
        For Each dr As DataRow In dt.Rows
            Ccount = 0
            For Each dc As DataColumn In dt.Columns
                str += dr(Ccount)
                str += ","
                Ccount += 1
            Next
            rcount += 1
            str += "" & vbCrLf
        Next
        Return str
    End Function

    Public Function SaveRoundedDetails(ByRef Con As SqlConnection, ByRef tran As SqlTransaction, ByVal Sitecode As String, ByVal DocumentNumber As String, ByVal Invoicenumber As String, ByVal Invoicedate As DateTime, ByVal finYear As String, ByVal RoundedAmt As Double, ByVal DocumentType As String, ByVal userid As String) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "insert into salesroundeddtl values('" & Sitecode & "','" & DocumentNumber & "','" & Invoicenumber & "','" & finYear & _
            "','" & DocumentType & "','" & Format(Invoicedate, "yyyyMMdd") & "'," & RoundedAmt & ",'" & Sitecode & "','" & userid & "',getdate(),'" & Sitecode & "','" & userid & "',getdate(),1)"
            Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, Con)
            cmdVoucher.Transaction = tran
            If cmdVoucher.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Sub New()

    End Sub

    Public Function GetSiteAndSupplierInfo(ByVal DefaultName As String) As DataTable
        Try
            Dim vStmtQry As String = String.Empty

            If (DefaultName = "Site") Then
                vStmtQry = "Select SiteCode, SiteShortName from MstSite Where Status=1; "
            ElseIf (DefaultName = "Supplier") Then
                vStmtQry = "Select SupplierCode, SupplierName from MstSupplier Where Status=1; "
            End If

            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(vStmtQry, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Sqldt.TableName = DefaultName

            Return Sqldt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally

        End Try

    End Function

    Public Function GetBankDetails(ByVal siteCode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT BankAccNo, BankName FROM MSTSiteBankMap WHERE Status = 1 AND SiteCode = '" & siteCode & "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ' Function added by ketan for Outstanding Report (Payment Terms Functionality)  for Client Savoy
    Public Function GetCreditPaymentTermDetails(ByVal siteCode As String, ByVal _ParentForm As String) As DataTable
        'Try
        '    Dim dt As DataTable
        '    Dim strString As String = "SELECT PaymentTermName, PaymentTermId FROM CreditPaymentTermAndConditions WHERE Status = 1 AND SiteCode = '" & siteCode & "'"
        '    Dim da As New SqlDataAdapter(strString, ConString)
        '    dt = New DataTable
        '    da.Fill(dt)
        '    Return dt
        'Catch ex As Exception
        Try
            Dim dt As DataTable
            Dim _docType As String = ""
            If _ParentForm = "CashMemo" Then
                _docType = "CMS"
            ElseIf _ParentForm = "SalesOrder" Then
                _docType = "SO201"
            End If
            'Dim strString As String = "SELECT PaymentTermName  as Name, PaymentTermId  as Id FROM CreditPaymentTermAndConditions WHERE Status = 1 AND SiteCode = '" & siteCode & "'"
            Dim strString As String = "SELECT  MTNC.TnCcode as Id,MTNC.Description as Name   from SiteDocmapTerms SDT inner join MstTermsNConditon MTNC on SDT.Tnccode=MTNC.TnCcode where SDT.Status=1 AND SiteCode = '" & siteCode & "' and SDT.DocType='" & _docType & "'"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetArticleDetails(ByVal ArticleCodeList As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT ArticleCode, ArticleName As ArticleDiscription FROM MstArticle WHERE ArticleCode IN (" + ArticleCodeList + ") "
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetLblSingleArticleDetails(ByVal ArticleCode As String, ByVal SiteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UDP_LblSingleArticleDetail"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@ArticleCode", ArticleCode)
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetArticleFullName(ByVal ArticleCode As String) As String
        Try
            Dim ArticleName As String
            Dim strString As String = "SELECT ArticleName  FROM MstArticle WHERE ArticleCode ='" & ArticleCode & "'"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            ArticleName = cmd.ExecuteScalar()
            Return ArticleName
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetTillClosePrintFormat(ByVal TerminalId As String) As String
        Try
            Dim TillPrintFormat As String
            Dim strString As String = "SELECT TillCloseFormat  FROM MstTerminalID WHERE TerminalID ='" & TerminalId & "'"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            TillPrintFormat = cmd.ExecuteScalar()
            Return TillPrintFormat
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '------Function for scheduler to run the procedure
    Public Function ExecuteSchedulerProcedure() As Boolean
        Try
            Dim dtData As New DataTable
            Dim daData As New SqlDataAdapter("select * from Authusers  ", ConString)
            daData.Fill(dtData)
            If dtData Is Nothing AndAlso dtData.Rows.Count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '--------function for schedualar to run procedure to delete transaction
    Function ExecuteProcedureForDeleteRecord(ByVal FldLabel As String) As Boolean
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "DeleteRecordAdilTestProc"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            Dim ParamDate As DateTime = System.DateTime.Now
            If FldLabel <> "" Then
                'NoOfDaysToMinus = NoOfDaysToMinus.Replace(" ", "")
                'FldLabel = FldLabel.Substring(0, 2).Trim()
                Dim days As Integer = Convert.ToInt64(FldLabel)
                ParamDate = ParamDate.AddDays(-days)
                cmd1.Parameters.AddWithValue("@Date", ParamDate)
                cmd1.ExecuteNonQuery()
            Else
                Return False
                Exit Function
            End If
            CloseConnection()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    '-------------------Fuction For Shift
    Public Shared Function GetNextShiftID(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String) As DataTable
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UdpGetNextShiftData"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@P_SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@P_OpenDate", DayOpenDate)
            cmd1.Parameters.AddWithValue("@P_TerminalId", TerminalID)
            cmd1.Parameters.AddWithValue("@P_NextOpenShiftId", 0)
            cmd1.Parameters.AddWithValue("@P_PreviousCloseShiftId", 0)
            cmd1.Parameters("@P_NextOpenShiftId").Direction = ParameterDirection.Output
            cmd1.Parameters("@P_PreviousCloseShiftId").Direction = ParameterDirection.Output
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            _NextShiftId = IIf(IsDBNull(cmd1.Parameters("@P_NextOpenShiftId").Value), 0, cmd1.Parameters("@P_NextOpenShiftId").Value) 'cmd1.Parameters("@P_NextOpenShiftId").Value
            _PrevShiftId = IIf(IsDBNull(cmd1.Parameters("@P_PreviousCloseShiftId").Value), 0, cmd1.Parameters("@P_PreviousCloseShiftId").Value) 'cmd1.Parameters("@P_PreviousCloseShiftId").Value
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetPreviousShiftDetails(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT MAX(ShiftId) As ShiftId,MAX(CREATEDON) As CREATEDON FROM ShiftOpenClose WHERE sitecode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND OpenDate=@DayOpenDate"

            Dim cmd As New SqlCommand(strString, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetShiftCloseDetails(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String) As String
        Try
            Dim dataTable As New DataTable
            Dim maxshift As String = "SELECT MAX(ShiftId) As ShiftId FROM MstShift WHERE Status='1' And SiteCode='" & SiteCode & "'"
            Dim query = "select OpenCloseStatus FROM ShiftOpenClose as F Left JOIN MstShift As M on F.ShiftId = M.ShiftId " & _
                        "WHERE  F.SiteCode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND M.ShiftId=(" & maxshift & ") AND F.OpenDate=@DayOpenDate And OpenCloseStatus='Close' "
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)

            ' Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetShiftName(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String) As DataTable
        Try
            Dim dt As DataTable
            Dim maxshift As String = "select Max(ShiftId) from ShiftOpenClose WHERE sitecode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND OpenDate=@DayOpenDate"
            Dim strString As String = "SELECT ShiftName,OpenCloseStatus FROM MstShift As shift LEFT JOIN ShiftOpenClose As SOC on shift.ShiftId=SOC.ShiftId WHERE shift.SiteCode ='" & SiteCode & "' AND TerminalId='" & TerminalID & "' AND shift.ShiftId=(" & maxshift & ") AND OpenDate=@DayOpenDate"

            Dim cmd As New SqlCommand(strString, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)
            'Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetShiftWiseDiscount(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal ShiftCreatedOn As DateTime, ByVal TerminalID As String) As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT  [Promotion Id],[Description],isnull(sum([Promotion Value]),0) AS [Promotion Value] FROM (" & _
                        " SELECT A.PROMOTIONID AS [Promotion Id],ISNULL(B.OFFERNAME,M.PromotionName) AS Description,round(sum(hdr.totalDiscount),2) as [Promotion Value] " & _
                        "FROM SALESDISCDTL A Left Outer JOIN  	 PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO Left Outer JOIN 	 ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN CASHMEMOHDR hdr  on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND A.BillNo=hdr.BillNo " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Hdr.TerminalID='" & TerminalID & "' AND A.BILLDATE=@DayOpenDate AND A.CREATEDON > @ShiftCreatedOn AND Hdr.billintermediatestatus <>'Deleted'" & _
                        " GROUP BY A.PROMOTIONID, B.OFFERNAME,M.PromotionName " & _
                        "Union all " & _
                        "SELECT A.PROMOTIONID AS [Promotion Id],ISNULL(B.OFFERNAME,M.PromotionName) AS Description,round(sum(Sdr.totalDiscount),2) as [Promotion Value]   " & _
                        "FROM SALESDISCDTL A Left Outer JOIN     PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO Left Outer JOIN     ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN     SalesOrderHdr Sdr  on A.Sitecode=Sdr.SiteCode and A.FinYear=Sdr.FinYear AND A.BillNo=Sdr.SaleOrderNumber  " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Sdr.TerminalID='" & TerminalID & "' AND A.BILLDATE=@DayOpenDate  AND A.CREATEDON > @ShiftCreatedOn AND Sdr.SOStatus <>'Deleted'" & _
                        "GROUP BY A.PROMOTIONID, B.OFFERNAME,M.PromotionName ) A " & _
                        "GROUP BY  [Promotion Id],[Description]"
            Dim dtData As New DataTable

            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim daData As New SqlDataAdapter(cmd)

            daData.Fill(dtData)
            Return dtData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetFLdValue(ByVal SiteCode As String) As String
        Try
            Dim dataTable As New DataTable
            Dim query As String = "SELECT FldValue FROM DefaultConfig WHERE Status='1' And FldLabel ='ClientForMail' And SiteCode='" & SiteCode & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetShiftNameForPrint(ByVal shiftid As String, ByVal sitecode As String) As String
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select ShiftName  from MstShift where ShiftId='" & shiftid & "' And SiteCode='" & sitecode & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    ''Added by ketan Check Day Open Or close (Shift Open issue)
    Public Function GetDayOpenOrClose(ByVal DayOpenDate As Date, ByVal sitecode As String) As String
        Try
            Dim dataTable As New DataTable
            Dim query As String = "SELECT DayCloseStatus FROM DayOpenNClose WHERE Opendate= @DayOpenDate And SiteCode='" & sitecode & "'"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)
            'Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    

    '' added by nikhil For Hari OM on 10/04/2017

    Public Function GetDayOpenOrCloseForHariOM(ByVal DayOpenDate As Date, ByVal sitecode As String) As Boolean
        Try
            Dim dataTable As New DataTable
            Dim query As String = "SELECT Top 1 * FROM DayOpenNClose WHERE Opendate= @DayOpenDate And SiteCode='" & sitecode & "'"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)
            'Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetOffernoForRoundOff() As String
        Try
            Return "PC_ROUND_OFF"
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetShiftWiseDisc(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal ShiftCreatedOn As DateTime, ByVal TerminalID As String) As String
        Try
            Dim StrQuery As String
            StrQuery = "SELECT ISNULL(SUM(ISNULL(A.Amount,0)) ,0) FROM ( Select ISNULL(SUM(ISNULL(hdr.TotalDiscount,0)) ,0) AS Amount" & _
                        " FROM  SALESDISCDTL A LEFT OUTER JOIN PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO LEFT OUTER JOIN  " & _
                        " ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN CASHMEMOHDR hdr on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND A.BillNo=hdr.BillNo " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Hdr.TerminalID='" & TerminalID & "' AND A.BILLDATE= @DayOpenDate  AND A.CREATEDON > @ShiftCreatedOn AND Hdr.billintermediatestatus <>'Deleted' AND UPPER(OfferNo) <> 'PC_ROUND_OFF'" & _
                        "Union all " & _
                        "SELECT ISNULL(SUM(ISNULL(sdr.TotalDiscount,0)) ,0) AS Amount " & _
                        "FROM SALESDISCDTL A Left Outer JOIN PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO Left Outer JOIN    ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN    SalesOrderHdr Sdr  on A.Sitecode=Sdr.SiteCode and A.FinYear=Sdr.FinYear AND A.BillNo=Sdr.SaleOrderNumber  " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Sdr.TerminalID='" & TerminalID & "' AND A.BILLDATE= @DayOpenDate  AND A.CREATEDON > @ShiftCreatedOn AND Sdr.SOStatus <>'Deleted'  AND UPPER(OfferNo) <> 'PC_ROUND_OFF'" & _
                        "GROUP BY A.PROMOTIONID, B.OFFERNAME,M.PromotionName  ) As A"
            Dim dtData As New DataTable
            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dtData)
            If Not dtData Is Nothing AndAlso dtData.Rows.Count > 0 Then
                Return dtData.Rows(0)(0)
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetShiftWiseRoundOff(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal ShiftCreatedOn As DateTime, ByVal TerminalID As String) As String
        Try
            Dim StrQuery As String
            StrQuery = "SELECT ISNULL(SUM(ISNULL(A.Amount,0)) ,0) From (Select ISNULL(SUM(ISNULL(hdr.TotalDiscount,0)) ,0) AS Amount" & _
                        " FROM  SALESDISCDTL A LEFT OUTER JOIN PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO LEFT OUTER JOIN  " & _
                        " ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN CASHMEMOHDR hdr on A.Sitecode=hdr.SiteCode and A.FinYear=Hdr.FinYear AND A.BillNo=hdr.BillNo " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Hdr.TerminalID='" & TerminalID & "' AND A.BILLDATE=@DayOpenDate  AND A.CREATEDON > @ShiftCreatedOn AND Hdr.billintermediatestatus <>'Deleted' AND UPPER(OfferNo) = 'PC_ROUND_OFF'" & _
                        "Union all " & _
                        "SELECT ISNULL(SUM(ISNULL(sdr.TotalDiscount,0)) ,0) AS Amount " & _
                        "FROM SALESDISCDTL A Left Outer JOIN PROMOTIONS B ON A.PROMOTIONID = B.OFFERNO Left Outer JOIN    ManualPROMOTION M ON A.PROMOTIONID = M.PROMOTIONID LEFT OUTER JOIN    SalesOrderHdr Sdr  on A.Sitecode=Sdr.SiteCode and A.FinYear=Sdr.FinYear AND A.BillNo=Sdr.SaleOrderNumber  " & _
                        "where A.SiteCode ='" & SiteCode & "' AND Sdr.TerminalID='" & TerminalID & "' AND A.BILLDATE= @DayOpenDate  AND A.CREATEDON > @ShiftCreatedOn AND Sdr.SOStatus <>'Deleted'  AND UPPER(OfferNo) = 'PC_ROUND_OFF'" & _
                        "GROUP BY A.PROMOTIONID, B.OFFERNAME,M.PromotionName )As A "

            Dim dtData As New DataTable
            Dim cmd As New SqlCommand(StrQuery, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dtData)
            If Not dtData Is Nothing AndAlso dtData.Rows.Count > 0 Then
                Return dtData.Rows(0)(0)
            End If


        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' for JK sprint 13
    ''' target Vs sales notification popup
    ''' get sales record
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="DayCloseDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalesValue(ByVal SiteCode As String, ByVal DayOpenDate As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim dtActualSales As New DataTable
            Dim dtActualBunCount As New DataTable
            Dim query1 As New StringBuilder
            dtActualSales.TableName = "tblAcutalSale"
            dtActualBunCount.TableName = "tblActualBunCount"
            Dim query As String = "SELECT ( ISNULL(sum(ISNULL(netamount,0)-isnull(returnAmount,0)) ,0)) as 'P_NetSales' FROM    VIEW_SalesReport where SITECODE='" & SiteCode & "' and BILLDATE='" & DayOpenDate & "'"
            query1.Append("select  isnull(sum(NETSALESQTY),0) as 'BunCount' from (SELECT CAST(SUM(CONVERT(NUMERIC(18),ISNULL((A.QUANTITY*MAK.QUANTITY), 0) - ")
            query1.Append("ISNULL((A.RETURNQTY*MAK.QUANTITY), 0))) AS DECIMAL(10,2)) AS NETSALESQTY,ma.articlecode,ma.lastnodecode ")
            query1.Append("FROM VIEW_SALESREPORT AS A ")
            query1.Append("INNER JOIN MSTARTICLEKIT  MAK ON MAK.KITARTICLECODE=A.ARTICLECODE ")
            query1.Append("INNER JOIN VW_HIREARCYLEVEL  I ON A.LASTNODECODE=I.LEVEL1 ")
            query1.Append("INNER JOIN MSTARTICLE MA ON MA.ARTICLECODE=MAK.ARTICLECODE ")
            query1.Append("WHERE A.billdate= '" & DayOpenDate & "'")
            query1.Append("AND A.SITECODE= '" & SiteCode & "'  and  MAK.ARTICLECODE IN (SELECT ARTICLECODE FROM MSTARTICLE  WHERE ARTICALTYPECODE='SINGLE' ) ")
            query1.Append("group by ma.articlecode ,ma.lastnodecode ) ")
            query1.Append("a where lastnodecode='ANCCCE000000005' OR  a.articlecode='310202022'")
            'query1.Append("a where lastnodecode=((select LastNodeCode from Mstarticle where articlecode='310202022')) OR  a.articlecode='310202022'")
            Dim da As New SqlDataAdapter(query, ConString)
            Dim da1 As New SqlDataAdapter(query1.ToString(), ConString)
            da.Fill(dtActualSales)
            da1.Fill(dtActualBunCount)
            ds.Tables.Add(dtActualSales)
            ds.Tables.Add(dtActualBunCount)
            Return ds
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' for JK sprint 13
    ''' target Vs sales notification popup 
    ''' get target records
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="DayOpentDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTargetValue(ByVal SiteCode As String, ByVal DayOpenDate As String) As DataTable
        Try
            Dim dtTargetSales As New DataTable
            dtTargetSales.TableName = "dtTargetSales"
            Dim query As String = "select * from TargetSales where SITECODE='" & SiteCode & "' and Date='" & DayOpenDate & "'and Status='1'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dtTargetSales)
            Return dtTargetSales
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function IsTransactionAccess(ByVal sitecode As String) As Boolean
        Try
            Dim Qry As String = "select * from msttransaction a inner join SiteAllowedTransactions b on a.TransactionCode=b.TransactionCode   where  b.TransactionCode='FO_LOGIN' and b.STATUS=1 and b.Sitecode='" & sitecode & "' "
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(Qry, ConString)
            da.Fill(dt)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return True
        End Try
    End Function
#Region "Dine In"

    '-----------Dinein Kot Fuction
    Public Shared Function GetOldKotData(ByVal billno As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select BillNo,EAN,ArticleCode,SUm(KotQuantity) As KotQty  from DineInKotQtyMap  WHERE billno ='" & billno & "' group by  BillNo,EAN,ArticleCode "

            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetKotData(ByVal billno As String, ByVal ArticleCode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select *  from DineInKotQtyMap  WHERE billno ='" & billno & "' And ArticleCode ='" & ArticleCode & "'  Order by  BillNo,ArticleCode,kotno "

            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMaxKotNo(ByVal billNo As String) As Integer
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select Isnull(MAX(KotNO ),0) as maxKotNo from DineInKotQtyMap where BillNo ='" & billNo & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetRemarkItemWise(ByVal sitecode As String, ByVal billno As String, ByVal articlecode As String, ByVal ean As String) As String
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select Isnull(Remark,'') from DineInCashMemoDtl where sitecode='" & sitecode & "' and billno='" & billno & "' and articlecode='" & articlecode & "' and EAN ='" & ean & "' "
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsGenerateKOTEnable(ByVal billno As String) As Boolean
        Try
            Dim dt As DataTable
            Dim strString As String = "SELECT hdtl.BillNo, hdtl.EAN, art.ArticleShortName as discription, art.ArticleCode, SUM(hdtl.Quantity) - isnull(SUM(kt.KotQuantity),0) as KotQuantity  ,hdtl.Remark " & _
                                      " FROM   DineInCashMemoDtl AS hdtl INNER JOIN MstArticle AS art ON art.ArticleCode = hdtl.ArticleCode LEFT OUTER JOIN " & _
                                      " (SELECT kt.BillNo,kt.ArticleCode,SUM(kt.KotQuantity) AS KotQuantity, MAX(kt.KotNo) + 1 AS KotNo  From DineInKotQtyMap AS kt " & _
                                      " Group BY  kt.BillNo,kt.ArticleCode ) AS kt ON kt.BillNo = hdtl.BillNo AND hdtl.ArticleCode = kt.ArticleCode " & _
                                      " WHERE     (hdtl.BillNo = '" & billno & "') " & _
                                      "GROUP BY hdtl.BillNo, hdtl.EAN, art.ArticleShortName, art.ArticleCode,hdtl.Remark " & _
                                      "HAVING(SUM(hdtl.Quantity) - ISNULL(SUM(kt.KotQuantity), 0)) > 0"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetAllPendingKOT() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = " SELECT hdtl.BillNo, hdtl.EAN, art.ArticleShortName as discription, art.ArticleCode, SUM(hdtl.Quantity) - isnull(SUM(kt.KotQuantity),0) as KotQuantity  ,hdtl.Remark,ODM.DineinNumber" & _
                                      " FROM   DineInCashMemoDtl AS hdtl INNER JOIN MstArticle AS art ON art.ArticleCode = hdtl.ArticleCode LEFT  JOIN " & _
                                      " (SELECT kt.BillNo,kt.ArticleCode,SUM(kt.KotQuantity) AS KotQuantity, MAX(kt.KotNo) + 1 AS KotNo  From DineInKotQtyMap AS kt  where substring(billno,1,3)<>'DIT'" & _
                                      " Group BY  kt.BillNo,kt.ArticleCode ) AS kt ON kt.BillNo = hdtl.BillNo AND hdtl.ArticleCode = kt.ArticleCode " & _
                                      " INNER JOIN orderdineinmap ODM   on ODM.Sitecode = hdtl.SiteCode and ODM.Billno = Hdtl.BillNo and ODM.Status =1 " & _
                                      " where  substring(hdtl.billno,1,3)<>'DIT' GROUP BY hdtl.BillNo, hdtl.EAN, art.ArticleShortName, art.ArticleCode,hdtl.Remark,ODM.DineinNumber " & _
                                      " HAVING(SUM(hdtl.Quantity) - ISNULL(SUM(kt.KotQuantity), 0)) > 0"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsPresentKot(ByVal billno As String) As Boolean
        Try
            Dim dt As DataTable
            Dim strkot As String = "select kotno from DineInKotQtyMap where BillNo='" & billno & "'"
            Dim cmd As New SqlCommand(strkot, SpectrumCon())
            Using da As New SqlDataAdapter(cmd)
                dt = New DataTable
                da.Fill(dt)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Return True
                End If
                Return False
            End Using
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function LoadRemarks(ByVal sitecode As String, ByVal billno As String) As DataTable
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select ArticleCode,EAN,Isnull(Remark,'') As Remark,BillLineNo from DineInCashMemoDtl where sitecode='" & sitecode & "' and billno='" & billno & "'"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMergeBillNo() As DataTable
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select Isnull(billno,'') As billno from DineInMergeOrdersDtl"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateDinInStatus(ByVal userid As String, ByVal sitecode As String, ByVal Billno As String) As Boolean
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strString As String = ""
            strString = "UPDATE OrderDineInMap SET STATUS='False',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' AND   BILLNO='" & Billno & "'"

            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)

            If cmd.ExecuteNonQuery() > 0 Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            CloseConnection()
            Return False

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetBillStatus(ByVal billno As String) As Boolean
        Try
            GetBillStatus = False
            Dim dataTable As New DataTable
            Dim query As String = "select * from DineInCashMemoDtl where BillNo='" & billno & "' "
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    'added by khusrao adil on 19-05-2017 for innoviti edit bill check in f4 
    'not to remove credit card entry
    Public Function CheckEditBillAvailable(ByVal billno As String, ByVal siteCode As String, Optional tenderType As String = "") As Boolean
        Try
            CheckEditBillAvailable = False
            Dim dataTable As New DataTable

            Dim query As String = ""
            If tenderType <> "" Then
                query = "select * from cashmemoreceipt where SiteCode='" & siteCode & "' and BillNo='" & billno & "' and TenderTypeCode='" & tenderType & "' and Status=1 "
            Else
                query = "select * from cashmemoreceipt where SiteCode='" & siteCode & "' and BillNo='" & billno & "' and Status=1 "
            End If

            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

#End Region
#Region "Grievance module"

    Public Function GetNextGrievanceId() As String
        Try
            Dim dataTable As New DataTable
            Dim query As String = "Select  IsNull(Max(GrievanceId) ,'0')+1 from GrievanceDetails "
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' get mobile number by site code
    ''' </summary>
    ''' <param name="siteCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMobileNumberBySiteCode(ByVal siteCode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select A.SplitData as MobileNo from MStSite cross apply fnSplitString (isnull(SiteTelephone2,0),',')  AS A  WHERE  IsActive=1 and SiteCode='" + siteCode + "'"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Function GetDepartmentMobileList(ByVal GrievanceId As String, ByVal SiteCode As String) As DataTable
        Try
            Dim dt As DataTable
            '  Dim strString As String = "select Top 1 MobileList  from GrievanceDetails  where status=1 and GrievanceId ='" + GrievanceId + "' AND  (SiteCode = '" + SiteCode + "' OR SiteCode = 'CCE' )"
            'added by vipul for jk sprint 24
            Dim strString As String = "select Top 1 MobileList  from GrievanceDetails  where status=1 and GrievanceId ='" + GrievanceId + "'"
            'select Top 1 MobileNo from GrievanceSMSDetails WHERE SMSSiteCode = '' AND GrievanceId = '' AND Status= 1'
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' get mobile numbers of department by depaortment id
    ''' </summary>
    ''' <param name="deptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDepartmentMobileNumberByDepartmentId(ByVal deptId As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select Distinct MobileNo  from DepartmentMobileMap  where status=1 and DeptId ='" + deptId + "'"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Function GetMobileNumberBySiteCodeForList(ByVal siteCode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select A.SplitData as MobileNo from MStSite cross apply fnSplitString (isnull(SiteTelephone2,0),',')  AS A  WHERE  IsActive=1 and SiteCode In ('" + siteCode + "')"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' get deparmetn Name by dept Id
    ''' </summary>
    ''' <param name="DeptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDepartmentById(ByVal DeptId As String) As String
        Try
            Dim dt As DataTable
            'Dim strString As String = "select d.DeptId,d.DeptName  from Department  d join UserDepartmentMap mp on d.DeptId = mp.DeptId where mp.UserId = '" & usercode & "' and mp.status ='1' order by d.DeptName asc "
            Dim strString As String = "select DeptId,DeptName  from Department  where DeptId='" & DeptId & "' and status=1 "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Dim name As String
            If dt.Rows.Count > 0 Then
                name = dt.Rows(0)("DeptName")
                Return name
            Else
                Return "Department No. " & DeptId
            End If


        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSiteByCode(ByVal SiteCode As String, Optional ByVal SiteStdCodeRequired As Boolean = False) As String
       
        Try
            Dim name As String = ""
            Dim strString As String = ""
            If SiteStdCodeRequired = True Then
                strString = "select SiteStdCode  from MstSite Where SiteCode  = '" + SiteCode + "'  AND Status = 1 "
            Else
                strString = "select SiteCode,SiteShortName  from MstSite Where SiteCode  = '" + SiteCode + "'  AND Status = 1 "
            End If
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If SiteStdCodeRequired = True Then
                If dt.Rows.Count > 0 Then
                    name = dt.Rows(0)("SiteStdCode")
                    Return name
                Else
                    Return ""
                End If
            Else
                If dt.Rows.Count > 0 Then
                    name = dt.Rows(0)("SiteShortName")
                    Return name
                Else
                    Return "Site Code. " & SiteCode
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetCashDrawerName(ByVal Sitecode As String, ByVal Terminalid As String) As String
        Try
            Dim dt As DataTable
            Dim strString As String = "select  top 1 isnull(Logicalname,'') 'Logicalname' from PosDeviceProfile where Sitecode='" & Sitecode & "' and TerminalID ='" & Terminalid & "' and DeviceType ='Drawer' and status=1"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Dim name As String
            If dt.Rows.Count > 0 Then
                name = dt.Rows(0)("Logicalname")
                Return name
            Else
                Return ""
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDepartment(ByVal userCode As String) As DataTable
        Try
            Dim dt As DataTable
            'Dim strString As String = "select d.DeptId,d.DeptName  from Department  d join UserDepartmentMap mp on d.DeptId = mp.DeptId where mp.UserId = '" & usercode & "' and mp.status ='1' order by d.DeptName asc "
            'Dim strString As String = "select DeptId,DeptName  from Department  where status=1 order by DeptName asc "
            ' Dim strString As String = "select DeptId,DeptName  from Department  where DeptName not like 'CMF%' and status=1 order by DeptName asc"
            Dim strString As String = "select d.DeptId,d.DeptName  from Department  d join UserDepartmentMap mp on d.DeptId = mp.DeptId where mp.UserId = '" & userCode & "' and mp.status ='1' and d.status='1' order by d.DeptName asc "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 09-04-2018 for jk sprint 35
    Public Function GetFtpVideosFolders() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from FtpVideosFolderMap where status=1"

            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 10-04-2018 for jk sprint 35
    Public Function GetFTPDetails() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from defaultconfig where  FldLabel in ('SPECTRUM_FTP_VIDEO_DIRECTORY','SPECTRUM_FTP_FO_VIDEO_USER_NAME','SPECTRUM_FTP_FO_VIDEO_USER_PASSWORD','SPECTRUM_FTP_URL') and SiteCode='BOCommon'and  status=1"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSite(ByVal userCode As String, ByVal Sitecode As String) As DataTable
        Try
            Dim dt As DataTable
            'code added for jk sprint 24 by vipul
            ' Dim strString As String = "select m.siteCode, m.siteShortName from MstSite m where m.status=1 and m.isActive=1 and m.businessCode in ('Store','WH','CCE') and m.siteCode in (select a.siteCode from AuthUserSiteRoleMap a where a.userId= '" & userCode & "'  and a.status= 1 ) order by m.siteShortName; "
            Dim strString As String = "select m.siteCode, m.siteShortName from MstSite m where m.status=1 and m.isActive=1 and m.businessCode in ('Store','WH','CCE') order by m.siteShortName; "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSelectedDepartment(ByVal deptid As String) As DataTable
        Try
            Dim dt As DataTable
            'Dim strString As String = "select d.DeptId,d.DeptName  from Department  d join UserDepartmentMap mp on d.DeptId = mp.DeptId where mp.UserId = '" & usercode & "' and mp.status ='1' order by d.DeptName asc "
            'Dim strString As String = "select * from  DeptGrievanceMapping  where  DeptId = '" + deptid + "' and  Status in ('1')"
            Dim strString As String = "select t.GrievanceTypeId,t.GrievanceTypeName from DeptGrievanceMapping m join GrievanceType t on m.GrivanceTypeId=t.GrievanceTypeId where m.DeptId ='" + deptid + "' and m.status in (1)"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetGrievanceType() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select GrievanceTypeId,GrievanceTypeName from GrievanceType where status=1 order by GrievanceTypeName asc"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetStatus() As DataTable

        Dim table As New DataTable
        table.Columns.Add("Status", GetType(String))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add("New", "New")
        table.Rows.Add("Resolved", "Resolved")
        table.Rows.Add("Re-opened", "Re-opened")
        table.Rows.Add("Closed", "Closed")

        Return table
    End Function

    'Public Function GetGrievanceDetails(ByVal usercode As String, ByVal Sitecode As String, Optional ByVal grieid As String = Nothing, Optional ByVal title As String = Nothing, Optional ByVal grieTypeId As Integer = Nothing, Optional ByVal deptId As Integer = Nothing, Optional ByVal grieStatus As String = Nothing, Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing, Optional ByVal Updatedon As Date = Nothing, Optional ByVal createdby As String = Nothing, Optional ByVal updatedby As String = Nothing, Optional ByVal RaisedBy As String = "") As DataTable
    '    Try
    '        'SqlQuery.Append("" & vbCrLf)
    '        Dim SqlQuery As New StringBuilder
    '        SqlQuery.Length = 0

    '        SqlQuery.Append("SELECT CONVERT(BIT,0)AS [SELECT],GD.GrievanceId As GrievanceID ,isnull(GD.Title,'') As GrievanceTitle ,isnull(Gt.GrievanceTypeName,'') As GrievanceType,Dept.DeptName As Department ,GD.GrievanceStatus As Status,mst.SiteOfficialName AS CreatedAt,gd.CREATEDBY as CreatedBy,gd.CREATEDON  as CreatedOn,  Gd.UPDATEDBY as UpdatedBy,gd.UPDATEDON as UpdatedOn , GD.IsBold As IsRead " & vbCrLf)
    '        SqlQuery.Append("FROM GrievanceDetails As GD " & vbCrLf)
    '        SqlQuery.Append("LEFT OUTER JOIN GrievanceType as GT on GD.GrievanceTypeId =GT.GrievanceTypeId  " & vbCrLf)
    '        SqlQuery.Append("LEFT OUTER JOIN Department As Dept On GD.DeptId = Dept.DeptId  " & vbCrLf)
    '        SqlQuery.Append("Left OUTER join mstsite mst on Gd.SiteCode= mst.SiteCode " & vbCrLf)
    '        ''added by ketan JK Change
    '        SqlQuery.Append("LEFT Outer Join AuthUserSiteRolemAp AuS On AuS.sitecode =  Gd.sitecode and  AuS.userid  =  Gd.createdby and  AUS.Status=1" & vbCrLf)
    '        SqlQuery.Append("LEFT Outer Join AuthUserS  Au On AU.userid =  auS.userid and  AU.Status=1")

    '        'SqlQuery.Append("LEFT OUTER JOIN AuthUsers AS AU ON  GD.CREATEDBY =AU.UserID  left join  AuthUsers AS A ON  GD.UPDATEDBY = A.UserID ")
    '        ' SqlQuery.Append("left outer join UserDepartmentMap mp on gd.DeptId= mp.DeptId  Where mp.UserId='" & usercode & "'")
    '        If RaisedBy = "FO" Then
    '            SqlQuery.Append("Where  GD.Status=1  AND GD.SiteCode='" & Sitecode & "'  --And mp.Status=1 " & vbCrLf)
    '        ElseIf RaisedBy = "BO" Then
    '            SqlQuery.Append("Where  GD.Status=1  AND GD.SiteCode='CCE' and GD.AssignedSiteCode='" & Sitecode & "' --And mp.Status=1 " & vbCrLf)
    '        ElseIf RaisedBy = "" Then
    '            SqlQuery.Append("Where  GD.Status=1 AND GD.SiteCode in ('" & Sitecode & "','CCE') AND GD.AssignedSiteCode in ('" & Sitecode & "','CCE')" & vbCrLf)
    '        ElseIf RaisedBy = "CMF" Then
    '            SqlQuery.Append("Where AU.Designation Like '%CMF%' AND GD.Status=1 AND GD.SiteCode in ('" & Sitecode & "','CCE') AND GD.AssignedSiteCode in ('" & Sitecode & "','CCE')" & vbCrLf)
    '        End If

    '        If grieid <> Nothing Then
    '            SqlQuery.Append("And GD.GrievanceId='" & grieid & "'" & vbCrLf)
    '        End If
    '        If title <> Nothing Then
    '            SqlQuery.Append("And GD.Title like '%" & title & "%'" & vbCrLf)
    '        End If
    '        If grieTypeId <> Nothing Then
    '            SqlQuery.Append("And GD.GrievanceTypeId=" & grieTypeId & "" & vbCrLf)
    '        End If
    '        If deptId <> Nothing Then
    '            SqlQuery.Append("And GD.DeptId=" & deptId & "" & vbCrLf)
    '        End If
    '        If grieStatus <> Nothing Then
    '            SqlQuery.Append("And GD.GrievanceStatus='" & grieStatus & "'" & vbCrLf)
    '        End If
    '        If FromDate <> Nothing Or ToDate <> Nothing Then
    '            SqlQuery.Append("And CAST(GD.UpdatedOn as Date) between  @fromdate and @todate" & vbCrLf)
    '        End If


    '        'If createdOn <> Nothing Then
    '        '    SqlQuery.Append("And  Convert(datetime,Convert(varchar(12),GD.CREATEDON,100)) =  @Createdon" & vbCrLf)
    '        'End If

    '        If Updatedon <> Nothing Then
    '            SqlQuery.Append("And  Convert(datetime,Convert(varchar(12),GD.UPDATEDON,100)) =  @Updatedon" & vbCrLf)
    '        End If

    '        If createdby <> Nothing Then
    '            SqlQuery.Append("And gd.CREATEDBY = @Createdby" & vbCrLf)
    '        End If

    '        If updatedby <> Nothing Then
    '            SqlQuery.Append("And  Gd.UPDATEDBY = @Updatedby  " & vbCrLf)
    '        End If

    '        SqlQuery.Append("order by Gd.UPDATEDON desc")


    '        Dim dtData As New DataTable
    '        Dim cmd As New SqlCommand(SqlQuery.ToString, SpectrumCon())
    '        cmd.Parameters.Add("@fromdate", SqlDbType.Date)
    '        cmd.Parameters("@fromdate").Value = FromDate
    '        cmd.Parameters.Add("@todate", SqlDbType.Date)
    '        cmd.Parameters("@todate").Value = ToDate
    '        If ToDate <> Nothing Then
    '            cmd.Parameters("@todate").Value = ToDate
    '        Else
    '            cmd.Parameters("@todate").Value = Date.Now.Date
    '        End If
    '        'cmd.Parameters.Add("@Createdon", SqlDbType.Date)
    '        'cmd.Parameters("@Createdon").Value = createdOn
    '        cmd.Parameters.Add("@Updatedon", SqlDbType.Date)
    '        cmd.Parameters("@Updatedon").Value = Updatedon
    '        If createdby <> Nothing Then
    '            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar)
    '            cmd.Parameters("@Createdby").Value = createdby
    '        End If
    '        If updatedby <> Nothing Then
    '            cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar)
    '            cmd.Parameters("@Updatedby").Value = updatedby
    '        End If


    '        Dim daData As New SqlDataAdapter(cmd)
    '        daData.Fill(dtData)
    '        Return dtData

    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function
    Public Function GetGrievanceDetails(ByVal usercode As String, ByVal Sitecode As String, Optional ByVal grieid As String = Nothing, Optional ByVal title As String = Nothing, Optional ByVal grieTypeId As Integer = Nothing, Optional ByVal deptId As Integer = Nothing, Optional ByVal grieStatus As String = Nothing, Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing, Optional ByVal Updatedon As Date = Nothing, Optional ByVal createdby As String = Nothing, Optional ByVal updatedby As String = Nothing, Optional ByVal RaisedBy As String = "", Optional ByVal RaisedBySite As String = Nothing, Optional ByVal jkAll As Boolean = False, Optional ByVal jkRaisedbyCMF As Boolean = False, Optional ByVal jkRaisedbyFranchisee As Boolean = False, Optional ByVal jkRaisedbyHo As Boolean = False) As DataTable
        Try
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("Declare @UserId as Varchar (50) = '" & usercode & "'" & vbCrLf)
            SqlQuery.Append("select distinct CONVERT(BIT,0)AS [SELECT],  gd.GrievanceID, gd.Title AS GrievanceTitle,gt.GrievanceTypeName As GrievanceType, " & vbCrLf)
            SqlQuery.Append("case when IsRaisedFromSite='1'then (select siteshortname from mstsite where sitecode=gd.RaisedFromSite) else (select deptname from department where deptid=gd.DeptId) end AS 'RaisedFrom Site/Dept'," & vbCrLf)
            SqlQuery.Append("case when IsRaisedToSite='1'then (select siteshortname from mstsite where sitecode=gd.AssignedSiteCode) else (select deptname from department where deptid=gd.RaisedToDepartment) end 'RaisedToDept/Site' ," & vbCrLf)
            SqlQuery.Append("gd.GrievanceStatus AS Status,  case when IsRaisedFromSite='1'then (select siteshortname from mstsite where sitecode=gd.RaisedFromSite) else (select deptname from department where deptid=gd.DeptId) end AS CreatedAt ,gd.CREATEDBY  " & vbCrLf)
            SqlQuery.Append("as CreatedBy,gd.CREATEDON  as CreatedOn,  Gd.UPDATEDBY as UpdatedBy,gd.UPDATEDON as UpdatedOn , GD.IsBold As IsRead  " & vbCrLf)
            SqlQuery.Append("from GrievanceDetails gd" & vbCrLf)
            SqlQuery.Append("left join (select fd.SiteCode, fd.GrievanceId,fd.Rating  from grievancefeedbackdtl  fd " & vbCrLf)
            SqlQuery.Append("Left join (select siteCode tempSiteCode, GrievanceId tempGrievanceId, max(createdon) as tempCreated from grievancefeedbackdtl " & vbCrLf)
            SqlQuery.Append("group by siteCode, GrievanceId) temp on fd.grievanceid = temp.tempGrievanceId and fd.createdon = temp.tempCreated " & vbCrLf)
            SqlQuery.Append("and fd.siteCode = temp.tempSiteCode where temp.tempGrievanceId is not null) fdb on gd.SiteCode = fdb.SiteCode and gd.GrievanceId= fdb.GrievanceId  " & vbCrLf)
            SqlQuery.Append("left join Department d on d.DeptId = gd.DeptId " & vbCrLf)
            SqlQuery.Append("left join GrievanceType gt on gt.GrievanceTypeId=gd.GrievanceTypeId " & vbCrLf)
            SqlQuery.Append("left join  (select  SiteCode,GrievanceSiteCode,GrievanceId,max(CREATEDON) as createdon  from GrievanceReopenHistoryDetails " & vbCrLf)
            SqlQuery.Append("group by SiteCode,GrievanceSiteCode,GrievanceId) temp2 on gd.SiteCode=temp2.SiteCode and gd.GrievanceId=temp2.GrievanceId" & vbCrLf)
            SqlQuery.Append("cross apply fnSplitString (isnull(CCDepartment,0),',')  AS A " & vbCrLf)
            SqlQuery.Append("cross apply fnSplitString (isnull(CCSite,0),',')  AS B " & vbCrLf)
            SqlQuery.Append(" left join UserDepartmentMap ud on gd.DeptId= ud.DeptId and ud.UserId =@UserId and ud.Status =1 " & vbCrLf)
            SqlQuery.Append("left join UserDepartmentMap ud1 on gd.RaisedToDepartment= ud1.DeptId and ud1.UserId =@UserId and ud1.Status =1" & vbCrLf)

            SqlQuery.Append("left join UserDepartmentMap ud2 on A.Splitdata= ud2.DeptId and ud2.UserId =@UserId and ud2.Status =1 " & vbCrLf)
            SqlQuery.Append("left join AuthUserSiteRoleMap au1 on B.Splitdata= au1.sitecode and au1.UserId =@UserId and au1.Status =1" & vbCrLf)
            SqlQuery.Append("where ( (gd.Status =1 and (ud.departmentOwner = 1 or (gd.GrievanceId is not null and " & vbCrLf)


            'SqlQuery.Append("(gd.SiteCode in (select SiteCode from  AuthUserSiteRoleMap where UserID =@UserId and Status =1 union " & vbCrLf)
            'SqlQuery.Append("select SiteCode from AuthUserSiteTransactionMap where UserID =@UserId and Status =1) and gd.assignedSiteCode in  (select SiteCode from " & vbCrLf)
            'SqlQuery.Append("AuthUserSiteRoleMap where UserID =@UserId and Status =1    union select SiteCode from " & vbCrLf)
            'SqlQuery.Append(" AuthUserSiteTransactionMap where UserID =@UserId and status=1))) OR " & vbCrLf)
            'code added for jk sprint 24 by vipul
            SqlQuery.Append("(gd.assignedSiteCode in (select distinct c.SiteCode from AuthUserSiteRoleMap as c cross apply fnSplitString (isnull(Groleid,0),',') as b " & vbCrLf)
            SqlQuery.Append("inner join AuthRoleTransactionMap a on a.RoleID = b.splitdata and a.AuthTransactionCode = 'Tkt.Search'" & vbCrLf)
            SqlQuery.Append(" where c.UserID = @UserId and c.STATUS = 1) or gd.assignedSiteCode in(select SiteCode from AuthUserSiteTransactionMap where UserID =@UserId and Status =1 and AuthTransactionCode = 'Tkt.Search'))) OR " & vbCrLf)

            SqlQuery.Append("(gd.GrievanceId is not null and  (gd.RaisedFromSite in (select SiteCode from  AuthUserSiteRoleMap where UserID =@UserId and Status =1 union " & vbCrLf)
            SqlQuery.Append("select SiteCode from AuthUserSiteTransactionMap where UserID =@UserId and Status =1) and gd.RaisedFromSite in  (select SiteCode from " & vbCrLf)
            SqlQuery.Append("AuthUserSiteRoleMap where UserID =@UserId and Status =1    union select SiteCode from " & vbCrLf)
            SqlQuery.Append("AuthUserSiteTransactionMap where UserID =@UserId and status=1) and gd.CREATEDBY=@UserId)) )or gd.CREATEDBY=@UserId) or(gd.Status =1 " & vbCrLf)
            SqlQuery.Append("and (ud1.departmentOwner = 1 or (gd.GrievanceId is not null and " & vbCrLf)
            SqlQuery.Append("(gd.SiteCode in (select SiteCode from  AuthUserSiteRoleMap where UserID =@UserId and Status =1 union " & vbCrLf)
            SqlQuery.Append("select SiteCode from AuthUserSiteTransactionMap where UserID =@UserId and Status =1) and gd.assignedSiteCode in  (select SiteCode from " & vbCrLf)
            SqlQuery.Append("AuthUserSiteRoleMap where UserID =@UserId and Status =1    union select SiteCode from " & vbCrLf)
            SqlQuery.Append("AuthUserSiteTransactionMap where UserID =@UserId and status=1)))))" & vbCrLf)
            SqlQuery.Append(" OR( ud2.departmentOwner = 1)or (gd.GrievanceId is not null and " & vbCrLf)
            SqlQuery.Append("(au1.SiteCode in (select SiteCode from  AuthUserSiteRoleMap where UserID =@UserId and Status =1 union " & vbCrLf)

            SqlQuery.Append("select SiteCode from AuthUserSiteTransactionMap where UserID =@UserId and Status =1))) )" & vbCrLf)


            'jk sprint 25
            '    ---------------------------------------
            Dim count As Integer = 0
            If jkAll = True Then

                SqlQuery.Append("AND ( GD.Status=1 AND GD.SiteCode in ('" & Sitecode & "','CCE')) " & vbCrLf)
            Else




                If jkRaisedbyFranchisee = True Then
                    count = count + 1
                End If
                If jkRaisedbyCMF = True Then
                    count = count + 1
                End If
                If jkRaisedbyHo = True Then
                    count = count + 1
                End If


                If count = 1 Then


                    If jkRaisedbyFranchisee = True Then
                        SqlQuery.Append("And ( GD.Status=1  AND GD.SiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND  gd.GrievanceID NOT Like '%GCBCCE%' )" & vbCrLf)
                    ElseIf jkRaisedbyHo = True Then
                        SqlQuery.Append("And ( GD.Status=1  AND GD.SiteCode='CCE' and GD.AssignedSiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND ( gd.GrievanceID  Like '%GCBCCE%' OR  gd.GrievanceID  Like '%GCCCE%')) " & vbCrLf)

                    ElseIf jkRaisedbyCMF = True Then
                        SqlQuery.Append("and (gd.createdby in (select userid from AuthUsers where designation like'%CMF%'))" & vbCrLf)
                    End If

                Else





                    If jkRaisedbyFranchisee = True Then
                        SqlQuery.Append("And ( GD.Status=1  AND GD.SiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND  gd.GrievanceID NOT Like '%GCBCCE%' ) " & vbCrLf)
                        jkRaisedbyFranchisee = False
                        GoTo Line1
                    ElseIf jkRaisedbyHo = True Then
                        SqlQuery.Append("And ( GD.Status=1  AND GD.SiteCode='CCE' and GD.AssignedSiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND ( gd.GrievanceID  Like '%GCBCCE%' OR  gd.GrievanceID  Like '%GCCCE%')) " & vbCrLf)
                        jkRaisedbyHo = False
                        GoTo Line1
                    ElseIf jkRaisedbyCMF = True Then
                        SqlQuery.Append("And ( gd.createdby in (select userid from AuthUsers where designation like'%CMF%'))" & vbCrLf)
                        jkRaisedbyCMF = False
                        GoTo Line1

                    End If



Line1:              If jkRaisedbyFranchisee = True Then
                        SqlQuery.Append("OR  GD.Status=1  AND GD.SiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND  gd.GrievanceID NOT Like '%GCBCCE%' " & vbCrLf)
                        jkRaisedbyFranchisee = False
                        GoTo Line1
                    ElseIf jkRaisedbyHo = True Then
                        SqlQuery.Append("OR  GD.Status=1  AND GD.SiteCode='CCE' and GD.AssignedSiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND ( gd.GrievanceID  Like '%GCBCCE%' OR  gd.GrievanceID  Like '%GCCCE%') " & vbCrLf)
                        jkRaisedbyHo = False
                        GoTo Line1
                    ElseIf jkRaisedbyCMF = True Then
                        SqlQuery.Append("OR GD.Status=1 and  gd.createdby in (select userid from AuthUsers where designation like'%CMF%')" & vbCrLf)
                        jkRaisedbyCMF = False
                        GoTo Line1
                    End If

                End If

            End If



            'If RaisedBy = "FO" Then
            '    SqlQuery.Append("And  GD.Status=1  AND GD.SiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND  gd.GrievanceID NOT Like '%GCBCCE%' " & vbCrLf)
            'ElseIf RaisedBy = "BO" Then
            '    SqlQuery.Append("And  GD.Status=1  AND GD.SiteCode='CCE' and GD.AssignedSiteCode='" & Sitecode & "' AND gd.createdby not in (select userid from AuthUsers where designation like'%CMF%')  AND ( gd.GrievanceID  Like '%GCBCCE%' OR  gd.GrievanceID  Like '%GCCCE%') " & vbCrLf)
            'ElseIf RaisedBy = "" Then
            '    SqlQuery.Append("AND  GD.Status=1 AND GD.SiteCode in ('" & Sitecode & "','CCE') " & vbCrLf)
            'ElseIf RaisedBy = "CMF" Then
            '    SqlQuery.Append("and gd.createdby in (select userid from AuthUsers where designation like'%CMF%')" & vbCrLf)
            'End If

            If grieid <> Nothing Then
                SqlQuery.Append("And GD.GrievanceId='" & grieid & "'" & vbCrLf)
            End If
            If title <> Nothing Then
                SqlQuery.Append("And GD.Title like '%" & title & "%'" & vbCrLf)
            End If
            If grieTypeId <> Nothing Then
                SqlQuery.Append("And GD.GrievanceTypeId=" & grieTypeId & "" & vbCrLf)
            End If
            If deptId <> Nothing Then
                SqlQuery.Append("And GD.RaisedToDepartment=" & deptId & "" & vbCrLf)
            End If
            If RaisedBySite <> Nothing Then
                SqlQuery.Append("And GD.AssignedSiteCode='" & RaisedBySite & "'" & vbCrLf)
            End If
            If grieStatus <> Nothing Then
                SqlQuery.Append("And GD.GrievanceStatus='" & grieStatus & "'" & vbCrLf)
            End If
            If FromDate <> Nothing Or ToDate <> Nothing Then
                SqlQuery.Append("And CAST(GD.UpdatedOn as Date) between  @fromdate and @todate" & vbCrLf)
            End If

            If Updatedon <> Nothing Then
                SqlQuery.Append("And  Convert(datetime,Convert(varchar(12),GD.UPDATEDON,100)) =  @Updatedon" & vbCrLf)
            End If

            If createdby <> Nothing Then
                SqlQuery.Append("And gd.CREATEDBY = @Createdby" & vbCrLf)
            End If

            If updatedby <> Nothing Then
                SqlQuery.Append("And  Gd.UPDATEDBY = @Updatedby  " & vbCrLf)
            End If

            SqlQuery.Append("order by Gd.UPDATEDON desc")


            Dim dtData As New DataTable
            Dim cmd As New SqlCommand(SqlQuery.ToString, SpectrumCon())
            cmd.Parameters.Add("@fromdate", SqlDbType.Date)
            cmd.Parameters("@fromdate").Value = FromDate
            cmd.Parameters.Add("@todate", SqlDbType.Date)
            cmd.Parameters("@todate").Value = ToDate
            If ToDate <> Nothing Then
                cmd.Parameters("@todate").Value = ToDate
            Else
                cmd.Parameters("@todate").Value = Date.Now.Date
            End If

            cmd.Parameters.Add("@Updatedon", SqlDbType.Date)
            cmd.Parameters("@Updatedon").Value = Updatedon
            If createdby <> Nothing Then
                cmd.Parameters.Add("@Createdby", SqlDbType.VarChar)
                cmd.Parameters("@Createdby").Value = createdby
            End If
            If updatedby <> Nothing Then
                cmd.Parameters.Add("@Updatedby", SqlDbType.VarChar)
                cmd.Parameters("@Updatedby").Value = updatedby
            End If
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dtData)
            Return dtData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetGrievanceDetailIdWise(ByVal Sitecode As String, ByVal id As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select REPLACE(GrievanceDesc, CHAR(10), CHAR(13)+CHAR(10)) as GrievanceDesc1, * from GrievanceDetails where GrievanceId='" & id & "' "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function loadTickets(ByVal sitecode As String) As DataTable
        Try
            Dim SqlQuery As New StringBuilder
            SqlQuery.Append("SELECT gr.grievanceid AS 'TicketId',dp.DeptName AS 'Department',")
            SqlQuery.Append(" Gr.GrievanceDesc AS 'TicketDescription',gr.UPDATEDBY AS 'UpdatedBy', '' AS Rating,'' AS Comment ")
            SqlQuery.Append(" FROM GrievanceDetails Gr join Department Dp ON gr.RaisedToDepartment = dp.DeptId WHERE Gr.IsRated = 0 AND Gr.GrievanceStatus='Resolved' AND  gr.Status=1  AND Gr.Sitecode='" & sitecode & "' ")
            Dim cmd As New SqlCommand(SqlQuery.ToString, SpectrumCon())
            Dim daData As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            daData.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


#End Region
#Region "Health care (Vaidya Notes)"
    Public Function GetPatientDocsPath() As String
        Dim drStrDocUrl As SqlDataReader
        Dim StrDocUrl As String = ""
        Try
            OpenConnection()
            Dim sqlCmdDocUrl As New SqlCommand("select fldvalue from defaultconfig where fldlabel='PatientDocs'", SpectrumCon)
            drStrDocUrl = sqlCmdDocUrl.ExecuteReader()
            If (drStrDocUrl.Read()) Then
                If Not (drStrDocUrl.IsDBNull(0)) Then
                    StrDocUrl = drStrDocUrl.GetString(0)
                End If
            End If
            Return StrDocUrl
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        Finally
            drStrDocUrl.Close()
            CloseConnection()
        End Try
    End Function


    Public Function GetNextDocumentNo(ByVal obj As String, ByVal siteCode As String, Optional objType As String = "") As String
        Try
            Dim DocNo As String = ""
            Dim dt As New DataTable
            Dim sqlQuery = "SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'"
            If Not String.IsNullOrEmpty(objType) Then
                sqlQuery += " AND OBJECTTYPEID='" & objType & "'"
            End If
            Dim daDoc As New SqlDataAdapter(sqlQuery, ConString)
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                dt = New DataTable
                daDoc.SelectCommand.CommandText = "SELECT CURRENTNO FROM GLNORANGEOBJECTS WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & siteCode & "'"
                daDoc.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DocNo = dt.Rows(0)("CURRENTNO")
                End If
            End If
            Return DocNo
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function GetCLPProgramId(ByVal siteCode As String) As String
        Try
            Dim ClpProgId As String = ""
            Dim query As String = String.Empty
            query = "select top(1) ClpProgramId from CLPProgramSiteMap where Sitecode='" & siteCode & "' AND Status=1 "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        ClpProgId = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                    Loop
                End If
            End Using
            Return ClpProgId
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function GetCustomerImage(ByVal strCustomerID As String, ByVal strImagePath As String) As String
        Dim drStrimageUrl As SqlDataReader
        Dim StrimageUrl As String = ""
        Try
            OpenConnection()
            'Dim sqlCmdImageUrl As New SqlCommand("select CustomerImage from CUSTOMERSALEORDER where CustomerNo='" & strCustomerID & "'", SpectrumCon)
            Dim sqlCmdImageUrl As New SqlCommand("select PatientImage from PatientDetails where PatientId='" & strCustomerID & "'", SpectrumCon)

            drStrimageUrl = sqlCmdImageUrl.ExecuteReader()
            If (drStrimageUrl.Read()) Then
                If Not (drStrimageUrl.IsDBNull(0)) Then
                    StrimageUrl = drStrimageUrl.GetString(0)
                End If
            End If
            Return strImagePath & StrimageUrl
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        Finally
            drStrimageUrl.Close()
            CloseConnection()
        End Try
    End Function

    Public Sub GetPatientAge(ByVal strCustomerID As String, ByRef strAge As String, ByRef strGender As String)
        Dim drStrAge As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCmdImageUrl As New SqlCommand("select datediff(year,dateofbirth,getdate()) as Age,gender from PatientDetails where PatientId='" & strCustomerID & "'", SpectrumCon)

            drStrAge = sqlCmdImageUrl.ExecuteReader()
            If (drStrAge.Read()) Then
                If Not (drStrAge.IsDBNull(0)) Then
                    strAge = drStrAge.Item(0)
                    strGender = drStrAge.Item(1)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            strAge = "0"
            strGender = "0"
        Finally
            drStrAge.Close()
            CloseConnection()
        End Try
    End Sub

    Public Function GetClinicalHistorySrNo(ByVal ClientHistoryId As String, ByVal PatientId As String, ByVal SiteCode As String, Optional isEdit As Boolean = False) As String
        Try
            GetClinicalHistorySrNo = 1
            Dim dt As New DataTable
            Dim sqlQuery = "select ClinicalHistorySrNo from HcTrnClinicalHistory where ClinicalHistoryId='" + ClientHistoryId + "' and PatientID='" + PatientId + "' and SiteCode='" + SiteCode + "'"
            'select ClinicalHistorySrNo from HcTrnClinicalHistory where ClinicalHistoryId=0 and PatientID='9897456' and SiteCode='0000588'
            Dim daDoc As New SqlDataAdapter(sqlQuery, ConString)
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim _maxClinicalHistorySrNo As Integer
                _maxClinicalHistorySrNo = (dt.Rows(0)("ClinicalHistorySrNo").ToString())
                If dt.Rows(0)("ClinicalHistorySrNo") = "" Or dt.Rows(0)("ClinicalHistorySrNo") = Nothing Then
                    GetClinicalHistorySrNo = 1
                Else
                    If isEdit = False Then
                        GetClinicalHistorySrNo = _maxClinicalHistorySrNo + 1
                    Else
                        GetClinicalHistorySrNo = _maxClinicalHistorySrNo
                    End If
                End If
            End If
            Return GetClinicalHistorySrNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetArticleDetailsById(ByVal ArticleCode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "Select '' AS SrNo,ArticleCode,ArticleName AS ArticleDescription, '' AS EAN," &
                                      "'' AS Qty, '' AS ConsumptionRate,'' AS Duration,'' AS Remarks, '' AS ConsultantsNoteId," &
                                        "'' AS PatientId,'' AS SiteCode,'' AS NoteSrNo,'' AS CREATEDAT,'' AS CREATEDBY," &
                                        "'' AS CREATEDON,'' AS UPDATEDAT,'' AS UPDATEDBY,'' AS UPDATEDON,'' AS STATUS " &
                                        "from MstArticle A where (A.ArticleCode='" + ArticleCode + "' OR A.ArticleShortName='" + ArticleCode + "') and STATUS=1 and ArticleActive=1"
            'Dim strString As String = "select ArticleCode,ArticleShortName,ArticleName from MstArticle A where (A.ArticleCode='" + ArticleCode + "' OR A.ArticleShortName='" + ArticleCode + "') and STATUS=1 and ArticleActive=1"
            'Dim strString As String = "select ArticleCode ,ArticleShortName,ArticleName from MstArticle A where (A.ArticleCode='" + ArticleCode + "' OR A.ArticleShortName='" + ArticleCode + "') and STATUS=1 and ArticleActive=1"
            Dim da As New SqlDataAdapter(strString, ConString)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            'LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
    Public Function GetSmsPCData(ByVal Sitecode As String, ByVal DayOpenDate As Date, ByVal formatNo As Integer) As String
        Try
            Dim query As String = "Exec udp_getpcsmsdata '" & Sitecode & "',@DayOpenDate, " & formatNo & ""
            Dim dt As New DataTable
            'Dim da As New SqlDataAdapter(query, ConString)
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = DayOpenDate
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
            Return String.Empty

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMaxBillLineNoKit(ByVal billNo As String, ByVal StrSiteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Integer
        Try
            Dim dt As New DataTable
            Dim cmdTrn As New SqlCommand("select Max(BillLineNo) as maxLineItem from cashmemokitdtl where BillNo ='" & billNo & "' AND SiteCode='" & StrSiteCode & "'", con, tran)
            Dim daBillLineNo As New SqlDataAdapter(cmdTrn)
            daBillLineNo.Fill(dt)

            If Not IsDBNull(dt.Rows(0)(0)) Then
                Return dt.Rows(0)("maxLineItem") + 1
            Else
                Return 1
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPCAmountGoingToBankDifferenceData(ByVal SiteCode As String, ByVal DayOpenDate As Date) As DataTable
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UDP_GetPCAmountGoingToBankDifferenceData"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@V_DayCloseFromDate", DayOpenDate)
            cmd1.Parameters.AddWithValue("@V_DayCloseToDate", DayOpenDate)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)

            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Function for autocaptilise
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAutocaptiliseWords() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from AutoCapitalise where status=1"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Capital Validation for statement text .
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CapitalValidationStatement(ByVal InputString As String) As String
        Try
            ' Dim data As String = str.Replace(vbCrLf, " ")

            'Dim wordArray = data.Replace(Chr(13), " ").Split(" ")

            '---- Split InputString to MultiPle lines
            Dim MultiLineData = InputString.Split(Chr(13))
            For lineNo = 0 To MultiLineData.Length - 1 Step 1
                '---- Each Line apply autocapitalize & join into Input Field
                Dim data As String = MultiLineData(lineNo)
                Dim wordArray = data.Replace(vbCrLf, "").Replace(vbLf, "").Split(" ")
                Dim pos As Integer = 0
                For pos = 0 To wordArray.Length - 1 Step 1
                    Dim temp = wordArray(pos)
                    If Not temp = String.Empty Then
                        Dim dtAuto = GetAutocaptiliseWords()
                        Dim drHdr() = dtAuto.Select("Word='" & temp.Replace("'", "''") & "'")
                        If drHdr.Count > 0 Then
                            wordArray(pos) = temp
                        Else
                            temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToLower()
                            wordArray(pos) = temp
                        End If
                    End If
                Next pos
                MultiLineData(lineNo) = String.Join(" ", wordArray)
            Next lineNo
            InputString = String.Join(vbCrLf, MultiLineData)
            Return InputString
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Capital Validation for Word  .
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CapitalValidation(ByVal str As String) As String
        Try
            Dim data As String = str
            Dim word = data.Split(" ")
            Dim temp = word(word.Length - 1)

            If Not temp = String.Empty Then
                Dim dtAuto = GetAutocaptiliseWords()
                Dim drHdr() = dtAuto.Select("Word='" & temp & "'")

                If drHdr.Count > 0 Then
                    word(word.Length - 1) = temp
                    str = String.Join(" ", word)
                Else
                    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToLower()
                    word(word.Length - 1) = temp
                    str = String.Join(" ", word)
                End If

            End If
            Return str
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#Region "STR Notification module"
    ''' <summary>
    ''' Get The Data for STR Notification For Checking SiteCode and TillNo
    ''' </summary>
    ''' <param name="StrSiteCode"></param>
    ''' <param name="TerminalId"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSTRNotificationSiteDetails(ByVal StrSiteCode As String, ByVal TerminalId As String) As DataTable
        Try
            Dim dtSTRNotification As DataTable
            Dim strString As String = "select *from StrNotificationSiteTill where sitecode='" & StrSiteCode & "' and Tillcode='" & TerminalId & "'and status=1"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim daSTRNotification As New SqlDataAdapter(cmd)
            dtSTRNotification = New DataTable
            daSTRNotification.Fill(dtSTRNotification)
            Return dtSTRNotification
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Getting The STR Notification Timer Details
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSTRNotificationTimerDetails() As DataTable
        Try
            Dim dtSTRNotificationTimer As DataTable
            Dim strString As String = "select *from StrNotificationTimer where status=1 AND StrNotification=1 order by FromTime"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim daSTRNotificationTimer As New SqlDataAdapter(cmd)
            dtSTRNotificationTimer = New DataTable
            daSTRNotificationTimer.Fill(dtSTRNotificationTimer)
            Return dtSTRNotificationTimer
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>Getting The SalesOrder Details In STR Notification Form
    Public Function GetSTRNotificationData(ByVal SiteCode As String) As DataTable
        Try
            Dim dtSTR As New DataTable
            Dim daSTR As SqlDataAdapter
            daSTR = New SqlDataAdapter(" EXEC GetSTRNotificationData '" & SiteCode & "'", SpectrumCon)
            daSTR.Fill(dtSTR)
            Return dtSTR
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#End Region

    Public Function GetLastVisited(ByVal clpno As String) As DateTime
        Try
            Dim dataTable As New DataTable
            Dim query As String = "select ISNull(MAX(CAST(BillTime AS DATE) ),GETDATE()) As lastVisited from CashMemoHdr where CLPNo='" & clpno & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dataTable)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return GetCurrentDate()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function AutoDatabaseBackup(ByVal DbName As String, ByVal SavePath As String, ByVal bakFileName As String) As Boolean
        Try
            AutoDatabaseBackup = False

            If DbName <> Nothing AndAlso SavePath <> Nothing Then
                SpectrumCon.Open()
                Dim cmd As SqlCommand = New SqlCommand("EXEC master.dbo.xp_create_subdir '" + SavePath + "' ;Backup database " + DbName + " to disk='" + SavePath + "\" + bakFileName + "'with INIT", SpectrumCon)
                'Dim cmd As SqlCommand = New SqlCommand(" exec UDP_GetDatabaseBackup '" + DbName + "','" + SavePath + "'", SpectrumCon)
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
                AutoDatabaseBackup = True
            Else
                AutoDatabaseBackup = False
            End If
            SpectrumCon.Close()
            Return AutoDatabaseBackup
        Catch ex1 As Exception
            LogException(ex1)
            SpectrumCon.Close()
        Finally
            SpectrumCon.Close()
        End Try


    End Function
    Public Function GetPayoutValue(ByVal siteCode As String) As String
        Try
            Dim dt As New DataTable
            Dim strString As String = "select IsNull(payoutvalue,0) as PayOutValue from PayOut WHERE  status=1 and SiteCode='" + siteCode + "'"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDetailsExtendScreen() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(numeric(18,0),0) as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleDesc," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Qty," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Price, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DiscAmt," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DiscPer," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Tax," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Gross" & vbCrLf)

            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetArticleONPoledisplay(ByVal SiteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UDP_GetPoleDisplayArticle"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function
    Dim _20x2Line As SerialPort
    Public Sub Display20x2Line(ByVal line1String As String, ByVal line2String As String, ByVal portname As String, Optional ByVal ClearScreen As Boolean = False)
        Try
            _20x2Line = New SerialPort()
            _20x2Line.PortName = portname
            _20x2Line.BaudRate = 9600
            _20x2Line.DataBits = 8
            _20x2Line.StopBits = StopBits.One
            _20x2Line.Parity = Parity.None
            _20x2Line.ReadTimeout = 5000
            _20x2Line.Handshake = Handshake.None
            _20x2Line.WriteTimeout = 500

            If _20x2Line.IsOpen = False Then ' Checks whether port is already OPEN or Close 
                _20x2Line.Open()             'If COM port is not opened then OPEN for communication
                _20x2Line.Write(line1String + line2String)
                If _20x2Line.IsOpen = True Then           ' Checks whether port is already OPEN, If OPEN then Close it before exiting the function
                    _20x2Line.Close()
                    _20x2Line.Dispose()
                    Console.ReadLine()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    '-------------------------------------------------------------------------
    'added on 18 may ashma
    Public Function SaveInnovitiData(ByVal Billno As String, ByVal TerminalID As String, ByVal Amount As String, ByVal SiteCode As String, ByVal finyear As String, ByVal UserId As String, ByVal DocumentType As String, ByVal RetrievalReferenceNumber As String, ByVal TransactionTime As String, ByRef tran As SqlTransaction, Optional SalesInvoiceNumber As String = "") As Boolean
        Try
            Dim newBillNo As String = String.Empty
            Dim objType = "FO_DOC"
            Dim docno As String = getDocumentNo("TransInnoviti", SiteCode, objType)
            newBillNo = GenDocNo("TI" & TerminalID & finyear.Substring(finyear.Length - 2, 2), 15, docno)

            Dim strString As String = ""
            strString = " Insert into TransactionInnovitiiMap(SiteCode, FinYear,ReferenceNumber, BillNo, SInvNumber,RetrievalReferenceNumber, TransactionTime, DoucumentType,Amount, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS) "
            strString = strString & " values ('" & SiteCode & "','" & finyear & "','" & newBillNo & "','" & Billno & "','" & SalesInvoiceNumber & "','" & RetrievalReferenceNumber & "','" & TransactionTime & "','" & DocumentType & "','" & Amount & "','" & SiteCode & "','" & UserId & "',GetDate(),'" & SiteCode & "','" & UserId & "',GetDate(),1) "
            ' Dim cmd As New SqlCommand(strCkeckListHdrString, con, tran)
            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)

            If Not cmd.ExecuteNonQuery() > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetArticleForDefineCombo(ByVal SiteCode As String, ByVal FrmoDate As Date, ByVal TERMINALID As String) As DataTable 'vipin JK 21.03.2018
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "USP_TILLWISECONVERSION"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@SITECODE", SiteCode)
            cmd1.Parameters.AddWithValue("@FROMDATE", FrmoDate)
            cmd1.Parameters.AddWithValue("@TERMINALID", TERMINALID)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#Region "POS TAB"

    'added by khusrao adil on 04-05-2018 for Pos tab fetch groups and their child groups for soft delete
    Public Function GetButtonGroupAndChildForDelete(ByVal _PgroupId As String, Optional ByVal groupIds As String = "") As String
        Try
            Dim StrGroupIds As String = ""
            StrGroupIds = groupIds
            Dim Sqlquery As New StringBuilder
            Dim SqlDataAdapter As SqlDataAdapter()
            Sqlquery.Length = 0
            Sqlquery.Append("select GROUPID,ParentGroupID from buttonGroup where ParentGroupID='" + _PgroupId + "'")
            Dim dt As DataTable
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If groupIds <> "" Then
                    StrGroupIds = groupIds + _PgroupId + ","
                Else
                    StrGroupIds = _PgroupId + ","
                End If
                For Each dr As DataRow In dt.Rows
                    'If StrGroupIds <> "" Then
                    '    StrGroupIds = _PgroupId + "," + dr("GROUPID") + ","
                    'End If
                    StrGroupIds = GetButtonGroupAndChildForDelete(dr("GROUPID"), StrGroupIds)
                Next
            Else
                StrGroupIds = StrGroupIds + _PgroupId + ","
            End If
            Return StrGroupIds
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 13-03-2018 for spectrum new developement 
    Public Function GetCreditSalesPopupDetails(ByVal Sitecode As String, ByVal RecordsHoursBefore As Integer) As DataTable
        Dim da As SqlDataAdapter
        Dim CreditSalesPopupDetails As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            da = New SqlDataAdapter(String.Format("EXEC GetCreditSalesDetailsPopUpDetail '{0}','{1}'", Sitecode, RecordsHoursBefore), SpectrumCon)
            da.Fill(CreditSalesPopupDetails)
            CreditSalesPopupDetails.TableName = "CreditSales"
            Return CreditSalesPopupDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by irfan on 14-06-2018 for product expiry popup 
    Public Function GetProductExpiryPopupDetails(ByVal Sitecode As String) As DataTable
        Dim da As SqlDataAdapter
        Dim CreditSalesPopupDetails As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            da = New SqlDataAdapter(String.Format("EXEC GetProductExpiryPopupDetails '{0}'", Sitecode), SpectrumCon)
            da.Fill(CreditSalesPopupDetails)
            CreditSalesPopupDetails.TableName = "ProductExpiry"
            Return CreditSalesPopupDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'added by khusrao adil on 14-03-2018 for spectrum new developement 
    Public Function GetSites(ByVal Sitecode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select distinct m.siteCode,m.siteOfficialName from MstSite m inner join DefaultConfig dc on m.SiteCode=dc.Sitecode where m.status = 1 and dc.STATUS=1 and m.SiteCode='" + Sitecode + "'"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetbtnGroupStruc() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from ButtonGroup where 1=0"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetCurrentArticleStruc() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select '0' as ArticleCode,'0' as ArticleName,'0' as ArticleCodeName,'0' as AdditonalInfo,'0' as 'Delete'"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetDeleteViewArticleStruc() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select '0' as ArticleCode,'0' as GroupId,'0' as isDelete"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetFinalArticleStruc() As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from buttonarticle where 1=0"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 14-03-2018 for spectrum new developement 
    'Public Function GetButtonGroup(ByVal Sitecode As String, Optional parentGroupID As String = "", Optional CallAddGroups As Boolean = False, Optional isViewArticleCall As Boolean = False) As DataTable
    '    Try
    '        Dim Sqlquery As New StringBuilder

    '        Dim SqlDataAdapter As SqlDataAdapter()
    '        Sqlquery.Length = 0
    '        Dim dt As DataTable
    '        If isViewArticleCall = False Then
    '            If CallAddGroups = False Then
    '                If parentGroupID = String.Empty Then
    '                    Sqlquery.Append("select '0' as groupID,'--Select--' as groupName union all")
    '                    Sqlquery.Append(" select g.groupID, g.groupName from ButtonGroup g where g.status=1 and g.isActive =1 and (g.parentGroupId is null or g.ParentGroupID ='') and ")
    '                    Sqlquery.Append(" g.groupID NOT IN (select distinct a.groupID from ButtonArticle a  where a.status =1 and a.siteCode ='" + Sitecode + "' and a.isActive =1) ")
    '                    Sqlquery.Append(" and g.siteCode ='" + Sitecode + "' ")

    '                Else
    '                    Sqlquery.Append("select '0' as groupID,'--Select--' as groupName union all")
    '                    Sqlquery.Append(" select g.groupID, g.groupName from ButtonGroup g where g.status=1 and g.isActive =1 and g.parentGroupID='" + parentGroupID + "' and ")
    '                    Sqlquery.Append(" g.groupID  NOT IN (select distinct a.groupID from ButtonArticle a  where a.status =1 and a.siteCode ='" + Sitecode + "' and a.isActive =1) ")
    '                    Sqlquery.Append(" and g.siteCode ='" + Sitecode + "'")
    '                End If
    '                Dim cmd1 As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
    '                Dim da As New SqlDataAdapter(cmd1)
    '                dt = New DataTable
    '                da.Fill(dt)
    '                Return dt
    '            Else
    '                Sqlquery.Length = 0
    '                Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where ")
    '                Sqlquery.Append("g.groupID NOT IN (select distinct g.parentGroupID from ButtonGroup g where (g.parentGroupID IS NOT NULL or g.ParentGroupID <> '') and ")
    '                Sqlquery.Append("g.siteCode in('" + Sitecode + "','CCE') and g.isActive ='1' and g.status='1' ) and ")
    '                Sqlquery.Append("g.isActive ='1' and g.status='1' and g.siteCode in ('" + Sitecode + "','CCE')  order by g.GROUPNAME ")
    '                Dim cmd2 As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
    '                Dim da As New SqlDataAdapter(cmd2)
    '                dt = New DataTable
    '                da.Fill(dt)
    '                If dt.Rows.Count > 0 Then
    '                    Return dt
    '                Else
    '                    Sqlquery.Length = 0
    '                    Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where  g.isActive ='1' and g.status='1' and g.siteCode '" + Sitecode + "'")
    '                    Dim cmd3 As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
    '                    Dim da1 As New SqlDataAdapter(cmd3)
    '                    dt = New DataTable
    '                    da1.Fill(dt)
    '                End If
    '            End If
    '        Else
    '            If parentGroupID <> "" Then
    '                Sqlquery.Length = 0
    '                Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where g.parentGroupID='" + parentGroupID + "' and g.SITECODE in ('" & Sitecode & "','CCE') and g.status=1   ")
    '            Else
    '                Sqlquery.Length = 0
    '                Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where (g.parentGroupID IS NULL or g.parentGroupID='') and g.SITECODE in ('" & Sitecode & "','CCE') and g.status=1  ")
    '            End If
    '            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
    '            Dim da As New SqlDataAdapter(cmd)
    '            dt = New DataTable
    '            da.Fill(dt)
    '            Return dt
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    'added by khusrao adil on 20-03-2018 for spectrum new developement 
    'added by khusrao adil on 26-04-2018 for pos tab screen
    Public Function GetButtonGroupByparentId(ByVal Sitecode As String, ByVal parentGroupID As String, Optional callFromViewArticles As Boolean = False) As DataTable
        Try
            Dim Sqlquery As New StringBuilder
            Dim SqlDataAdapter As SqlDataAdapter()
            Sqlquery.Length = 0
            If callFromViewArticles = False Then
                Sqlquery.Append("select '0' as groupID,'--Select--' as groupName union all ")
            End If
            Sqlquery.Append("select g.groupID as groupID , g.groupName as groupName from ButtonGroup g where g.parentGroupID='" + parentGroupID + "' and g.SITECODE in ('" & Sitecode & "','CCE') and g.status=1   ")
            Dim dt As DataTable
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetButtonGroupForAddArticles(ByVal Sitecode As String) As DataTable
        Try
            Dim Sqlquery As New StringBuilder
            Dim SqlDataAdapter As SqlDataAdapter()
            Dim dt As DataTable
            Sqlquery.Length = 0
            Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where ")
            Sqlquery.Append("g.groupID NOT IN (select distinct g.parentGroupID from ButtonGroup g where (g.parentGroupID IS NOT NULL or g.ParentGroupID <> '') and ")
            Sqlquery.Append("g.siteCode in('" + Sitecode + "','CCE') and g.isActive ='1' and g.status='1' ) and ")
            Sqlquery.Append("g.isActive ='1' and g.status='1' and g.siteCode in ('" + Sitecode + "','CCE')  order by g.GROUPNAME ")
            Dim cmd1 As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd1)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Sqlquery.Length = 0
                Sqlquery.Append("select g.groupID, g.groupName from ButtonGroup g where  g.isActive ='1' and g.status='1' and g.siteCode in ('" + Sitecode + "','CCE')  order by g.GROUPNAME ")
                Dim cmd3 As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
                Dim da2 As New SqlDataAdapter(cmd3)
                dt = New DataTable
                da2.Fill(dt)
            End If
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetButtonGroups(ByVal Sitecode As String)
        Try
            Dim Sqlquery As New StringBuilder
            Dim SqlDataAdapter As SqlDataAdapter()
            Sqlquery.Length = 0
            Sqlquery.Append("select '0' as groupID,'--Select--' as groupName union all")
            Sqlquery.Append(" select g.groupID, g.groupName from ButtonGroup g where g.status=1 and g.isActive =1 and (g.parentGroupId is null or g.ParentGroupID ='') and ")
            Sqlquery.Append(" g.groupID NOT IN (select distinct a.groupID from ButtonArticle a  where a.status =1 and a.siteCode in ('" + Sitecode + "','CCE') and a.isActive =1) ")
            Sqlquery.Append(" and g.siteCode in ('" + Sitecode + "','CCE') ")
            Dim dt As DataTable
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function GetButtonGroupsViewArticles(ByVal Sitecode As String)
        Try
            Dim Sqlquery As New StringBuilder
            Dim SqlDataAdapter As SqlDataAdapter()
            Sqlquery.Length = 0
            Sqlquery.Append(" select g.groupID, g.groupName from ButtonGroup g where (g.parentGroupID IS NULL or g.parentGroupID='') and g.SITECODE in ('" + Sitecode + "','CCE') and g.status=1  ")
            Dim dt As DataTable
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function fetchParentHierarchyInfo(ByVal input As String, ByVal selectedGroupId As String, ByVal siteCode As String) As String
        Try
            If selectedGroupId Is Nothing OrElse selectedGroupId = "" OrElse selectedGroupId = "0" Then
                Return input
            End If
            Dim qry As String = "select isnull(b.parentGroupID,'') as parentGroupID,b.groupName,b.SiteCode from ButtonGroup b where b.groupID ='" & selectedGroupId & "' and b.siteCode in ('" & siteCode & "','CCE')"
            Dim cmd As New SqlCommand(qry, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim parentGroupId As String = ""
                Dim groupName As String = ""
                If dt.Rows.Count > 1 Then
                    Dim Result = dt.Select("SiteCode='" + siteCode + "'")
                    If Result.Length > 0 Then
                        parentGroupId = Result(0)(0)
                        groupName = Result(0)(1)
                    Else
                        parentGroupId = ""
                        groupName = ""
                    End If
                Else
                    parentGroupId = dt.Rows(0)(0).ToString()
                    groupName = dt.Rows(0)(1).ToString()
                End If
                groupName = If(groupName Is Nothing, "", groupName)
                If parentGroupId Is Nothing OrElse parentGroupId = "" Then
                    Dim inChild As String = input
                    input = groupName
                    input = input + inChild
                Else
                    Dim inChild As String = input
                    input = "/" + groupName
                    input = input + inChild
                    input = fetchParentHierarchyInfo(input, parentGroupId, siteCode)
                End If
            End If
            Return input
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Public Function FetchCurrentArticles(ByVal siteCode As String, ByVal selectedGroupId As String) As DataTable
        Try
            Dim Sqlquery As New StringBuilder
            Sqlquery.Length = 0
            Sqlquery.Length = 0
            Sqlquery.Append(" select ba.ARTICLECODE as ArticleCode,ma.ArticleName as ArticleName, (ba.ARTICLECODE+'==>'+ ma.ArticleName) as ArticleCodeName, ")
            Sqlquery.Append(" (ma.articalTypeCode +' '+ma.baseUnitofMeasure +' '+ma.materialTypeCode ) as AdditonalInfo,'False' as 'Delete' ")
            Sqlquery.Append(" from BUTTONARTICLE ba, MstArticle ma where ma.ArticleCode=ba.ARTICLECODE and ba.SITECODE in('" + siteCode + "','CCE') and ba.status='1' and ba.GROUPID ='" + selectedGroupId + "'")
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function FetchPartialArticles(ByVal siteCode As String, ByVal LikeValue As String) As DataTable
        Try
            Dim Sqlquery As New StringBuilder
            Sqlquery.Length = 0
            If LikeValue <> "" Then
                Sqlquery.Append(" select a.articleCode , a.articleName, a.materialTypeCode,a.articalTypeCode ,a.baseUnitofMeasure ")
                Sqlquery.Append(" from MstArticle a where a.status = 1 and ArticleActive=1 and Salable=1 and upper(a.articleName) LIKE '%" + LikeValue + "%'   order by a.articleName ASC ")
            Else
                Sqlquery.Append(" select a.articleCode , a.articleName, a.materialTypeCode,a.articalTypeCode ,a.baseUnitofMeasure ")
                Sqlquery.Append(" from MstArticle a where a.status =1 and ArticleActive=1 and Salable=1  order by a.articleName ASC ")
            End If
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function FetchCetegoryWiseArticle(ByVal LastNodeCode As String) As DataTable
        Try
            Dim Sqlquery As New StringBuilder
            Sqlquery.Length = 0
            Sqlquery.Length = 0
            Sqlquery.Append(" select ma.articleCode as ArticleCode,ma.articleName as ArticleName, (ma.articleCode+'==>'+ ma.articleName) as ArticleCodeName, ")
            Sqlquery.Append(" (ma.materialTypeCode +' '+ma.articalTypeCode +' '+ma.baseUnitofMeasure ) as AdditonalInfo,'False' as 'Delete' ")
            Sqlquery.Append(" from MstArticle ma where ma.STATUS=1  and ma.ArticleActive=1 and Ma.Salable=1 and  ma.LastNodeCode='" + LastNodeCode + "' order by ma.articleCode ASC")
            Dim cmd As New SqlCommand(Sqlquery.ToString(), SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'added by khusrao adil on 16-03-2018 for spectrum new developement 
    Public Function SaveAndEditBtnGroup(ByVal dt As DataTable, ByVal condition As String) As Boolean
        Try
            If Not String.IsNullOrEmpty(condition) Then
                Dim objcls As New clsCashMemo
                For Each dr As DataRow In dt.Rows
                    If condition = "Add" Then
                        Dim result = objcls.SaveAndEditButtonGroup(dr("GroupId"), dr("groupName"), condition, dr("CreatedBy"), dr("SiteCode"), dr("ParentGroupID"))
                        If result Then
                            Continue For
                        Else
                            Exit For
                        End If
                    ElseIf condition = "Edit" Then
                    ElseIf condition = "Remove" Then
                    End If
                Next
                Return True

            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'added by khusrao adil on 21-03-2018 for spectrum new developement 
    Public Function SaveAndDeleteButtonArticleData(ByVal dt As DataTable, ByVal condition As String) As Boolean
        Try
            Dim result As Boolean = False
            If Not String.IsNullOrEmpty(condition) Then
                Dim objcls As New clsCashMemo
                For Each dr As DataRow In dt.Rows
                    If condition = "Add" Then
                        'dr("GroupId"), dr("groupName"), condition, dr("CreatedBy"), dr("SiteCode"), dr("ParentGroupID")
                        result = objcls.SaveAndDeleteButtonArticleData("", dr("GroupId"), dr("articlecode"), dr("articlename"), condition, dr("CreatedBy"), dr("SiteCode"), dr("Status"))
                        If result Then
                            Continue For
                        Else
                            Exit For
                        End If
                    ElseIf condition = "Edit" Then
                    ElseIf condition = "Remove" Then
                    End If
                Next
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
#End Region
#Region "Define Combo" 'added by vipin
    Public Function GetComboArticle(ByVal siteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            ''  Dim strString As String = "select ArticleCode, ArticleShortName 'Discription', ArticleCode + ' '+  ArticleShortName ArticleCodeDesc from MstArticle  where ArticalTypeCode ='Combo' AND ArticleActive=1 "
            '  Dim strString As String = "select ArticleCode, ArticleShortName 'Discription', ArticleCode + ' '+  ArticleShortName ArticleCodeDesc from MstArticle  where ArticalTypeCode ='Combo' AND status=1 "
            ' Dim strString As String = "select ArticleCode, ArticleShortName 'Discription', ArticleCode + ' '+  ArticleShortName ArticleCodeDesc from MstArticle  where ArticalTypeCode ='Combo' AND status=1 "
            Dim strString As String = " select MA.ArticleCode, MA.ArticleShortName 'Discription',MA.ArticleCode + ' '+  MA.ArticleShortName 'ArticleCodeDesc'"
            strString = strString + "from MstArticle MA inner join SalesInfoRecord SI on MA.ArticleCode =SI.ArticleCode and MA.status=1 and  SI.STATUS =1"
            strString = strString + "where MA.ArticalTypeCode ='Combo' and SI.SiteCode ='" & siteCode & "'"

            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetGroupDtl(ByVal siteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = "select Groupid,GROUPNAME, Groupid +' '+GROUPNAME 'GroupIdName' from BUTTONGROUP where status =1 and sitecode in ('" & siteCode & "','CCE')"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Public Function GetComboDtl(ByVal siteCode As String, ByVal ArticleCode As String) As DataTable
    '    Try
    '        Dim dt As New DataTable
    '        Dim strString As String = "select ArticleShortName,Man.nodename'LastNodeCode',(select nodename from MstArticleNode where nodecode =MT.ParentArt and status =1)'ParentArt' ,MAT.treename 'TreeID',MT.ArticleCode,ArticalTypeCode,materialTypeCode,SR.sellingprice,mt.status from mstarticle MT"
    '        strString = strString + "  INNER JOIN  salesinforecord  SR on mt.ArticleCode = SR.ArticleCode and sr.status =1 LEFT JOIN MstArticleNode MAN On MAN.nodecode = MT.LastNodeCode and MAN.status =1  LEFT JOIN MstArticleTree MAT on MAT.status =1 And MAT.TreeCode = mt.TreeID where SR.sitecode= '" + siteCode + "' and MT.ArticleCode = '" + ArticleCode + " '"
    '        Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
    '        Dim da As New SqlDataAdapter(cmdTrn)
    '        da.Fill(dt)
    '        '  If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '        Return dt
    '        '    End If
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetComboDtl(ByVal siteCode As String, ByVal ArticleCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = "select  ME.EAN,ArticleShortName,Man.nodename'LastNodeCode',(select nodename from MstArticleNode where nodecode =MT.ParentArt and status =1)'ParentArt' ,MAT.treename 'TreeID',MT.ArticleCode,ArticalTypeCode,materialTypeCode,SR.sellingprice,mt.status,case when mt.ArticleActive=0 then 'In-Active' else 'Active' end as ArticleActive,mt.BaseUnitofMeasure  from mstarticle MT"
            strString = strString + "  INNER JOIN  salesinforecord  SR on mt.ArticleCode = SR.ArticleCode and sr.status =1 LEFT JOIN MstArticleNode MAN On MAN.nodecode = MT.LastNodeCode and MAN.status =1  LEFT JOIN MstArticleTree MAT on MAT.status =1 And MAT.TreeCode = mt.TreeID  left join MstEAN  ME on ME.ArticleCode=MT.ArticleCode  where SR.sitecode= '" + siteCode + "' and MT.ArticleCode = '" + ArticleCode + " '"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMstArticleComboStruct() As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = "select * from MstArticleCombo where 1=0"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetComboDefinedDetail(ByVal ComboCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = "select * from MstArticleCombo where combocode ='" & ComboCode & "'"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveMstArticleCombo(ByVal DsTemp As DataSet, ByVal MaxNo As String) As Boolean
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            '  If SaveData(DsTemp, SpectrumCon, tran) = False And UpdateDocumentNo("DefineCombo", SpectrumCon, tran, MaxNo, "FO_DOC") = False Then
            If SaveData(DsTemp, SpectrumCon, tran) = False And UpdateDocumentNo("CB", SpectrumCon, tran, MaxNo, "BK_DOC") = False Then
                Return False
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ActivateDeActivateComboDtl(ByRef ComboCode As String, ByRef SiteCode As String, ByRef UserCOde As String, ByRef isActivete As Boolean) As Boolean
        Try
            Dim Status As Integer = 0
            Dim query As String = ""
            Dim con As SqlConnection
            Dim cmd As SqlCommand
            If isActivete = True Then
                Status = 1
            Else
                Status = 0
            End If
            query = "update mstarticlecombo SET status ='" & Status & "', UpdatedAt= '" & SiteCode & "' , UPDATEDBY ='" & UserCOde & "' ,UPDATEDON =GETDATE() where combocode ='" & ComboCode & "' "
            query += " update MstArticle set ArticleActive='" & Status & "',STATUS='" & Status & "',UPDATEDAT='" & SiteCode & "',UPDATEDBY='" & UserCOde & "',UPDATEDON=GETDATE() where ArticleCode='" & ComboCode & "' "
            cmd = New SqlClient.SqlCommand(query, SpectrumCon)
            OpenConnection()
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
#End Region 'added by vipin
#Region "Define Kit" 'added by vipul

    'code added by vipul for Kit Structure

    Public Function GetMstArticleKitStruct() As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = "select * from MstArticleKit where 1=0"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetKitArticle(ByVal Sitecode As String) As DataTable
        Try
            Dim dt As New DataTable
            'Dim strString As String = "select ArticleCode, ArticleShortName 'Discription', ArticleCode + ' '+  ArticleShortName ArticleCodeDesc from MstArticle  where ArticalTypeCode ='Kit' and status=1 "

            Dim strString As String = " select MA.ArticleCode, MA.ArticleShortName 'Discription',MA.ArticleCode + ' '+  MA.ArticleShortName 'ArticleCodeDesc'"
            strString = strString + "from MstArticle MA inner join SalesInfoRecord SI on MA.ArticleCode =SI.ArticleCode and MA.status=1 and  SI.STATUS =1"
            strString = strString + "where MA.ArticalTypeCode ='Kit' and SI.SiteCode ='" & Sitecode & "'"

            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    Public Function GetKiTDefinedDetail(ByVal KitArticlecode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = " select MAK.Ean,MAK.ArticleCode,MA.ArticleShortName,MAK.SellingPrice ,MAK.SaleUnitofMeasure ,MAK.Quantity  from MstArticleKit MAK "
            strString = strString + "LEFT JOIN MstArticle MA ON MA.ArticleCode =MAK.ArticleCode where Mak.KitArticleCode ='" & KitArticlecode & "'and  Mak.Status=1"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateMstArticleActiveStatus(ByRef tran As SqlTransaction, ByVal KitArticleCode As String, ByVal SiteCode As String, ByVal UserCode As String) As Boolean
        Try
            Dim Status As Integer = 1
            Dim query As String = ""
            Dim cmd As SqlCommand

            query = "update mstarticle SET ArticleActive ='" & Status & "',Status=1,Salable=1, UpdatedAt= '" & SiteCode & "' , UPDATEDBY ='" & UserCode & "' ,UPDATEDON =GETDATE() where ArticleCode ='" & KitArticleCode & "' and ArticalTypeCode ='Kit'"
            cmd = New SqlClient.SqlCommand(query, SpectrumCon(), tran)

            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function ActivateOrDeActivateKit(ByVal KitArticleCode As String, ByVal SiteCode As String, ByVal UserCode As String, ByVal ActivateOrDeActivate As Boolean) As Boolean
        Try
            Dim query As String = ""
            Dim status As Integer = 0
            If ActivateOrDeActivate = True Then
                status = 1
            End If

            'Dim tran As SqlTransaction
            'OpenConnection()
            'tran = SpectrumCon.BeginTransaction()
            Dim cmd As SqlCommand

            query = "update mstarticle SET ArticleActive =" & status & ",Status=1, UpdatedAt= '" & SiteCode & "' , UPDATEDBY ='" & UserCode & "' ,UPDATEDON =GETDATE() where ArticleCode ='" & KitArticleCode & "' and ArticalTypeCode ='Kit'"
            cmd = New SqlClient.SqlCommand(query, SpectrumCon())
            OpenConnection()
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveMstArticleKit(ByVal DsTemp As DataSet, ByVal KitArticleCode As String, ByVal SiteCode As String, ByVal UserCode As String) As Boolean
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(DsTemp, SpectrumCon, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            If UpdateMstArticleActiveStatus(tran, KitArticleCode, SiteCode, UserCode) = False Then
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region


    'added by Roshan on 16-03-2018 for spectrum new developement 
    Public Function GetLowStockNotificationDetails(ByVal Sitecode As String) As DataTable
        Dim da As SqlDataAdapter
        Dim LowStockNotificationDetails As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            Dim strString As String = "select ab.ArticleCode as ArticleCode,ma.ArticleName as ArticleName  ,ab.uom as BaseUOM,ar.SafetyStockQty as Minimumstock,ab.PhysicalQty as Availablestock from ArticleReplenishment ar inner join ArticleStockBalances ab on ar.ArticleCode =ab.ArticleCode inner join mstarticle ma on ma.ArticleCode =ab.ArticleCode    inner join mstsite ms on ms.SiteCode =ab.SiteCode where ms.IsARSApplicable=1 and ar.SafetyStockQty > ab.PhysicalQty and ar.SafetyStockQty<> 0 and ab.STATUS=1 and ms.SiteCode='" + Sitecode + "'"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            da = New SqlDataAdapter(cmd)
            da.Fill(LowStockNotificationDetails)
            LowStockNotificationDetails.TableName = "LowStock"
            Return LowStockNotificationDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''GetSalesOrderPopupDetails
    Public Function GetSalesOrderPopupDetails(ByVal Sitecode As String) As DataTable
        Dim da As SqlDataAdapter
        Dim SalesOrderPopupDetails As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try

            da = New SqlDataAdapter(String.Format("EXEC GetSalesOrderDetailsPopUpDetail '{0}'", Sitecode), SpectrumCon)
            da.Fill(SalesOrderPopupDetails)
            SalesOrderPopupDetails.TableName = "SalesOrder"
            Return SalesOrderPopupDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function SaveCashMemoReciptFromEnquiry(ByVal DsTemp As DataSet, ByVal BillNo As String, ByVal SiteCode As String, ByVal UserCode As String) As Boolean
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(DsTemp, SpectrumCon, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            If UpdateMembershipEnquiry(tran, BillNo, SiteCode, UserCode) = False Then
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function UpdateMembershipEnquiry(ByRef tran As SqlTransaction, ByVal BillNo As String, ByVal SiteCode As String, ByVal UserCode As String) As Boolean
        Try
            Dim Status As Integer = 0
            Dim query As String = ""
            Dim cmd As SqlCommand

            query = "update ClpCustomerServiceArticlePeriodMap SET isEnquiry ='" & Status & "',Status=1, isConvertedToMember=1, UpdatedAt= '" & SiteCode & "' , UPDATEDBY ='" & UserCode & "' ,UPDATEDON =GETDATE() where BillNo ='" & BillNo & "'"
            cmd = New SqlClient.SqlCommand(query, SpectrumCon(), tran)

            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Public Function GetCustomerEnquiryData(ByVal cardNo As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select BillNo, sum(Amount) as Amount from ClpCustomerServiceArticlePeriodMap where cardno='" & cardNo & "'  group by BillNo"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticleOfAnHierarachy(ByVal Articlenode As String, ByVal SiteCode As String) As DataTable
        Try

            Articlenode = Articlenode.Replace("'", "")

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UDP_LblHierarachyDtl"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@LastNodeCode", Articlenode)
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetStructOfBarcodeArticle() As DataTable
        'Try
        '    Dim vStmtQry As New StringBuilder
        '    vStmtQry.Length = 0

        '    vStmtQry.Append("select  convert(bit,1) as 'Select'," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(100),'') as ArticleShortName," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(100),'') as SellingPrice," & vbCrLf)
        '    vStmtQry.Append(" Convert(numeric (10),0) as NoOfCopies," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(100),'') as ExpiryInDays," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(max),'') as Neutrition," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(max),'') as Ingredient," & vbCrLf)
        '    vStmtQry.Append(" Convert(Varchar(4),'') as BatchNo" & vbCrLf)
        '    Dim daReservation As New SqlDataAdapter
        '    daReservation = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
        '    Dim dtReservation As New DataTable
        '    daReservation.Fill(dtReservation)

        '    Return dtReservation
        'Catch ex As Exception
        '    LogException(ex)
        '    Return Nothing
        'End Try
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append("select  convert(bit,1) as 'Select'," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as EAN," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleShortName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as SellingPrice," & vbCrLf)
            vStmtQry.Append(" Convert(numeric (10),0) as NoOfCopies," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ExpiryInDays," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(max),'') as Neutrition," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(max),'') as Ingredient," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(4),'') as BatchNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Characteristics" & vbCrLf)
            Dim daReservation As New SqlDataAdapter
            daReservation = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtReservation As New DataTable
            daReservation.Fill(dtReservation)

            Return dtReservation
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function checkDiscSpce(ByVal SysytmDriveName As String) As Boolean ''PC SO Merge vipin 02-05-2018
        Try
            'Dim ReportPath As String = clsDefaultConfiguration.DayCloseReportPath
            'ReportPath = ReportPath.Substring(0, 3)
            'ReportPath = "D" + ReportPath.Substring(1)
            Dim CountOfDrive = My.Computer.FileSystem.Drives.Count

            Dim TotalFreeSpace
            Dim TotalFreeSpaceInMB = 0.0
            Dim TotalFreeSpaceInGB = 0.0
            For index = 0 To CountOfDrive - 1
                Dim driveName = My.Computer.FileSystem.Drives.Item(index).Name.ToString
                If driveName = SysytmDriveName Then
                    TotalFreeSpace = My.Computer.FileSystem.Drives.Item(index).TotalFreeSpace
                    TotalFreeSpaceInMB = Format(TotalFreeSpace / 1024 ^ 2, "0.00") '--MB
                    TotalFreeSpaceInGB = Format(TotalFreeSpace / 1024 ^ 3, "0.00") '--GB
                    TotalFreeSpaceInGB = Format(250 / 1024 ^ 3, "0") '--GB
                    If TotalFreeSpaceInMB < 500 Then
                        ' MessageBox.Show("Insufficient disk space for saving files")
                        Return False
                        Exit Function
                    Else
                        Return True
                    End If

                End If
            Next

            'Dim ddC = My.Computer.FileSystem.Drives.Item(0).TotalSize.ToString()
            'Dim dd = My.Computer.FileSystem.Drives.Item(0).AvailableFreeSpace.ToString()
            'Dim d = My.Computer.FileSystem.Drives.Item(1).AvailableFreeSpace.ToString
            'Dim dK = My.Computer.FileSystem.Drives.Item(1).Name.ToString
            'Dim TotalSize = My.Computer.FileSystem.Drives.Item(0).TotalSize
            'TotalFreeSpace = My.Computer.FileSystem.Drives.Item(0).TotalFreeSpace
            'Dim Diff = TotalSize - TotalFreeSpace
            'TotalFreeSpaceInMB = Format(TotalFreeSpace / 1024 ^ 2, "0") '--MB
            'TotalFreeSpaceInGB = Format(TotalFreeSpace / 1024 ^ 3, "0") '--GB

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetCustomerWisePriceForSO(ByVal SiteCode As String, ByVal CardNo As String, ByVal ArticleCode As String) As DataTable
        Try
            Dim cmd = New SqlCommand("GetCustomerWisePriceForSO", SpectrumCon)
            Dim ds As New DataTable
            cmd.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            cmd.Parameters.AddWithValue("@V_CardNo", CardNo)
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@V_ArticleCode", ArticleCode)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            Return ds

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function NumberInIndainFormat(ByVal Number As String) As String

        Dim parsed As Double = Decimal.Parse(Number, System.Globalization.CultureInfo.InvariantCulture)
        Dim hindi As New System.Globalization.CultureInfo("hi-IN")
        Dim text As String = String.Format(hindi, "{0:c}", parsed)
        text = text.Substring(2, text.Length - 5)
        Return LTrim(text)
    End Function
    Public Function GetCreditSaleSettledDataToPrint(ByVal siteCode As String) As DataTable 'vipin
        Try
            Dim cmd = New SqlCommand("UDP_CreditSaleSettledFHeader", SpectrumCon)
            Dim dt As New DataTable
            cmd.Parameters.AddWithValue("@V_SiteCode", siteCode)
            cmd.CommandTimeout = 0
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function getPackageScreen(ByVal _siteCode As String, ByVal _dayOpenDate As Date) As DataTable
        Try
            'Dim dtOrederPackages As New DataTable
            'Dim daOrederPackage As SqlDataAdapter
            'daOrederPackage = New SqlDataAdapter(" EXEC USP_GETORDERPACKAGES", SpectrumCon)
            'daOrederPackage.Fill(dtOrederPackages)
            'Return dtOrederPackages
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "USP_GETORDERPACKAGES"
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@Sitecode", _siteCode)
            cmd.Parameters.AddWithValue("@DayCloseDate", _dayOpenDate)
            OpenConnection()
            cmd.Connection = SpectrumCon()
            ''Dim query As String = "select top 100 ROW_NUMBER() over (order by sl.UPDATEDON desc) as 'Sr. No.' ,sl.InvoiceCustName as 'Customer Name',clp.CompanyName As 'Company Name',sl.SaleOrderNumber as 'Sales Order No.',mst.SiteOfficialName as 'Factory-Snacks (STR details)',mst.SiteShortName as 'Factory Sweets (STR details)',mst.BusinessCode as 'WareHouse-DF Namkeen (STR details)',sl.SOStatus As 'Delivery Type',case sl.IsMultiDelivery  when 0 then 'No' else 'Yes' end as 'Multi Delivery',sl.ActualDeliveryDate as 'Delivery Date',sl.GrossAmt,sl.BalanceAmount from ((SalesOrderHdr sl inner join CLPCustomers clp on sl.SiteCode=clp.SiteCode) inner join MstSite mst on sl.sitecode=mst.SiteCode) "
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            CloseConnection()
        End Try
    End Function
    Public Function GenerateTallyReport(ByVal SiteCode As String, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Try
            'vipin
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "StandAloneTallySalesImport"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@FromDate", FromDate)
            cmd1.Parameters.AddWithValue("@Todate", ToDate)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)

            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function FoDayCloseMailSend(ByVal SiteCode As String, ByVal UserId As String, ByVal MailSend As Boolean, ByVal DayCloseDate As Date)
        Try
            Dim cmd = New SqlCommand("USP_IsMailSendOnDayClose", SpectrumCon)

            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd.Parameters.AddWithValue("@UserID", UserId)
            SpectrumCon.Open()
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@IsMailSend", MailSend)
            cmd.Parameters.AddWithValue("@DayCloseDate", DayCloseDate)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
            SpectrumCon.Close()


        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SplitBillDetail(ByVal BillNo As String, ByVal Sitecode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim query As String = "select A.ArticleCode,b.ArticleShortName,sellingPrice,Quantity,Grossamt,TotaltaxAmount 'TaxAmount',TotalDiscount,NetAmount "
            query = query + "  from CashMemoDtl A LEFT JOIN MStArticle  B on A.ArticleCode = b.ArticleCode where a.BillNo ='" & BillNo & "' and a.sitecode ='" & Sitecode & "' and a.STATUS=1"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CashMemoHdrDetail(ByVal BillNo As String, ByVal Sitecode As String) As DataTable
        Try
            Dim dt As DataTable
            Dim query As String = "select  * from cashmemohdr where BillNo= '" & BillNo & "' and sitecode ='" & Sitecode & "' and status =1"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCashMemoSplitDtlStruct() As DataTable
        Try
            Dim dt As DataTable
            Dim query As String = "select  * from CashMemoSplitDtl where 1=2"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveSplitBillDtl(ByVal DtSaveSplitBill As DataTable) As Boolean
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(DtSaveSplitBill, SpectrumCon, tran) = False Then
                Return False
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetInvoiceMailSendData(ByVal BillNo As String, ByVal SiteCode As String) As DataSet 'vipul
        Try
            Dim dt As New DataSet
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "GetInvoiceMailSendData"
            cmd.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd.Connection = SpectrumCon()
            cmd.Parameters.AddWithValue("@BillNo", BillNo)
            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSMSSendData(ByVal BillNo As String, ByVal SiteCode As String) As DataSet 'vipul
        Try
            Dim dt As New DataSet
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "GetSMSSendData"
            cmd.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd.Connection = SpectrumCon()
            cmd.Parameters.AddWithValue("@BillNo", BillNo)
            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveImprestAmount(ByVal Amount As String, ByVal SiteCode As String, ByVal terminal As String, ByVal shiftid As Integer, ByVal TillDate As DateTime, ByVal userid As String, ByVal action As String) As Boolean

        Dim dtDetails As DataTable = Nothing
        Try
            Dim sqlComm As New SqlCommand("USP_ImprestDetails", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@V_TerminalId", terminal)
            sqlComm.Parameters.AddWithValue("@V_ShiftId", shiftid)
            sqlComm.Parameters.AddWithValue("@V_Action", action)
            sqlComm.Parameters.AddWithValue("@V_UserId", userid)
            sqlComm.Parameters.AddWithValue("@V_DateTime", TillDate.Date)
            sqlComm.Parameters.AddWithValue("@V_Value", Amount)
            sqlComm.CommandType = CommandType.StoredProcedure
            dtDetails = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dtDetails)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetPreviusDayTillFloatingData(ByVal TerminalId As String, ByVal SiteCode As String) As DataTable

        Try
            Dim dt As New DataTable
            Dim cmd As New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "GetPreviusDayTillFloatingData"
            cmd.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd.Connection = SpectrumCon()
            cmd.Parameters.AddWithValue("@TerminalId", TerminalId)
            cmd.Parameters.AddWithValue("@SiteCode", SiteCode)
            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetFailedMailData(ByVal SiteCode As String, ByVal Terminalid As String) As DataSet
        Try

            Dim dt As New DataSet
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "GetFailedMailData"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            'cmd1.Parameters.AddWithValue("@LastNodeCode", Articlenode)
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@TerminalId", Terminalid)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDocumentMailDtl() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append("select * from DocumentMailDtl where 1=0")
            Dim daReservation As New SqlDataAdapter
            daReservation = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtReservation As New DataTable
            daReservation.Fill(dtReservation)
            Return dtReservation
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function insertMailFailedDtl(ByVal SiteCode As String, ByVal DocumentNumber As String, ByVal DocumentType As String, ByVal NanoId As Long, ByVal Path As String, ByVal CSVTO As String, ByVal Subject As String, ByVal IsMailSend As Boolean, ByVal UserId As String, ByVal TerminalId As String, Optional ByVal MailBody As String = "", Optional ByVal StrError As String = "", Optional ByVal CSVCC As String = "", Optional ByVal CSVBCC As String = "", Optional ByVal CSVDocSubType As String = "", Optional ByVal ContentType As String = "")
        ' insert logic goes here with 
        Try
            Dim DtDocumentMailDtl = GetDocumentMailDtl()
            Dim DrDocDtl = DtDocumentMailDtl.NewRow()
            DrDocDtl("SiteCode") = SiteCode
            DrDocDtl("DocumentNumber") = DocumentNumber
            DrDocDtl("DocumentType") = DocumentType
            DrDocDtl("NanoId") = NanoId
            DrDocDtl("CSVTO") = CSVTO
            DrDocDtl("CSVCC") = CSVCC
            DrDocDtl("CSVBCC") = CSVBCC
            DrDocDtl("CSVDocSubType") = CSVDocSubType
            DrDocDtl("CSVDocPath") = Path
            DrDocDtl("ContentType") = ContentType
            DrDocDtl("Subject") = Subject
            DrDocDtl("MailBody") = MailBody
            DrDocDtl("IsMailSend") = IsMailSend '
            DrDocDtl("MailSendAttempt") = "0"
            DrDocDtl("Error") = StrError
            DrDocDtl("TerminalId") = TerminalId
            DrDocDtl("Createdat") = SiteCode
            DrDocDtl("CreatedBy") = UserId
            DrDocDtl("CreatedOn") = DateTime.Now()
            DrDocDtl("UpdatedAt") = SiteCode
            DrDocDtl("UpdatedBy") = UserId
            DrDocDtl("Updatedon") = DateTime.Now()
            DrDocDtl("Status") = 1
            DtDocumentMailDtl.Rows.Add(DrDocDtl)
            DtDocumentMailDtl.TableName = "DocumentMailDtl"
            Dim Ds As New DataSet
            Ds.Tables.Add(DtDocumentMailDtl)
            Try
                Dim tran As SqlTransaction
                OpenConnection()
                tran = SpectrumCon.BeginTransaction()
                If SaveData(Ds, SpectrumCon, tran) = False Then
                    Return False
                    tran.Rollback()
                    CloseConnection()
                    Exit Try
                End If
                tran.Commit()
                CloseConnection()
                Return True
            Catch ex As Exception
                LogException(ex)
                Return Nothing
            End Try
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateMailFailedDtl(ByVal SiteCode As String, ByVal DocumentNumber As String, ByVal DocumentType As String, ByVal NanoId As Long, ByVal MAilSendSucces As Boolean, ByVal StrError As String, ByVal UserID As String)
        Try
            Dim query As String = ""
            Dim con As SqlConnection
            Dim cmd As SqlCommand
            If MAilSendSucces Then
                query = "update DocumentMailDtl set ISMailSend = '1', UPDATEDON=getdate(),UPDATEDAT='" & SiteCode & "', UPDATEDBY= '" & UserID & "' WHERE Sitecode='" & SiteCode & "' and DocumentNumber = '" & DocumentNumber & "'and DocumentType = '" & DocumentType & "' and NanoId = '" & NanoId & "'"
            Else
                query = "SET QUOTED_IDENTIFIER OFF  update DocumentMailDtl set ISMailSend = '0',MailSendAttempt = MailSendAttempt +1,Error= """ & StrError & """ , UPDATEDON=getdate(),UPDATEDAT='" & SiteCode & "', UPDATEDBY= '" & UserID & "' WHERE Sitecode='" & SiteCode & "' and DocumentNumber = '" & DocumentNumber & "'and DocumentType = '" & DocumentType & "' and NanoId = '" & NanoId & "'"
            End If
            OpenConnection()
            Dim sqlcom As New SqlCommand(query, SpectrumCon)
            If sqlcom.ExecuteNonQuery > 0 Then
                CloseConnection()
                Return True
            Else
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function DeletePettyCashVoucher(ByVal UserCode As String, ByVal VoucherId As String, ByVal SiteCode As String, ByVal DeleteRemark As String) As Boolean 'vipul
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strString As String = ""
            strString = "UPDATE VoucherHDR SET STATUS=0,UPDATEDON=GETDATE(),UPDATEDBY='" & UserCode & "',DeleteRemark='" & DeleteRemark & "'  WHERE SITECODE='" & SiteCode & "' AND VoucherID='" & VoucherId & "'"

            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)

            If cmd.ExecuteNonQuery() > 0 Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            CloseConnection()
            Return False

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdateReprintStatusOfPettyCashVoucher(ByVal VoucherId As String, ByVal SiteCode As String) As Boolean 'vipul
        Try
            Dim tran As SqlTransaction
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim strString As String = ""
            strString = "UPDATE VoucherHDR SET reprint=IsNull(reprint,0) + 1,UPDATEDON=GETDATE() WHERE SITECODE='" & SiteCode & "' AND VoucherID='" & VoucherId & "'"

            Dim cmd As New SqlCommand(strString, SpectrumCon, tran)

            If cmd.ExecuteNonQuery() > 0 Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            CloseConnection()
            Return False

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function IsTodaysPettyCashVoucher(ByVal VoucherId As String, ByVal SiteCode As String, ByVal FinYear As String) As Boolean 'vipul
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable

            Dim sqlComm As New SqlCommand("USP_IsTodaysPettyCashVoucher", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@VoucherId", VoucherId)
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@FinYear", FinYear)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function
    Public Function GetTenderTypes(ByVal SiteCodeT As String) As DataTable
        Try
            Dim strQuery As String = ""
            strQuery = " SELECT TENDERHEADCODE,TENDERHEADNAME,TENDERTYPE,POSITIVE_NEGATIVE,MAXNO,MAXVALUE,MINBILLVALUE,DEFAULTVALUE  "
            strQuery += " FROM MSTTENDER WHERE SITECODE = '" & SiteCodeT & "' AND  STATUS = '1' AND Positive_Negative='+' "
            strQuery += " AND TENDERTYPE NOT IN "
            '  strQuery += " ('BenowUPI','Cash','Credit','CreditCard','Neft','Rtgs','MealPass','Others','QuickWallet','Paytm','JioMoney','PhonePe' "
            ' strQuery += " ,'Sodexo','SwiggyOnline','Tr','TktRestaurant','ZomatoOnline','OnlinePayment','MobiKwik','FoodPandaCash','FoodPandaOnline','SodexoCpn',"
            ' strQuery += " 'UberEatsCash','UberEatsOnline','ScootsyCash','ScootsyOnline','SwiggyCash' )"
            strQuery += " ('Cash','Neft','Rtgs','MealPass','Paytm','QuickWallet','Sodexo','TicketRestaurant','OnlinePayment','Others','Payso','SodexoCards','MobiKwik', "
            strQuery += " 'SodexoCpn','ZomatoCash','SwiggyCash','ScootsyOnline','ScootsyCash','UberEatsOnline','UberEatsCash','ZomatoOnline','FoodPandaCash','FoodPandaOnline','SwiggyOnline','PhonePe')"
            Dim dtTend As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTend)
            Return dtTend
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDynamicTenderTypes(ByVal SiteCode As String) As DataTable
        Try
            Dim strQuery As String = ""
            strQuery = " select t.TenderHeadCode as TenderHeadCode, t.TenderType as TenderType from MStTEnder t"
            strQuery += "  inner join MstTenderType tt on t.TenderType=tt.TenderType"
            strQuery += "  where sitecode = '" & SiteCode & "'and t.isVisible=1 and tt.STATUS =1 and t.status=1  AND t.Positive_Negative='+' "
            strQuery += "  AND t.TENDERTYPE NOT IN "
            strQuery += " ('Cash','Card','Payso','Cheque','Credit','Sodexo','SodexoCpn','Others','Paytm','CreditCard','Sodexo','MobiKwik', "
            strQuery += " 'SodexoCards','FoodPandaCash','FoodPandaOnline','TktRestaurant','SodexoCards','JioMoney','Tr','Payso','PhonePe')"
            Dim dtTend As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTend)
            Return dtTend
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function StringTojsonObject(ByVal JsonTring As String) As Dictionary(Of String, Object)
        Try
            Dim jss As New JavaScriptSerializer()
            Dim dictforCancel As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(JsonTring)
            Return dictforCancel
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


#Region "PhonePe"
    Public Shared Function EncodeTo64(ByVal JsonString As String) As String
        Dim toEncodeAsBytes As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(JsonString)
        Dim returnValue As String = System.Convert.ToBase64String(toEncodeAsBytes)
        Return returnValue
    End Function
    Public Shared Function GenerateSHA256String(ByVal inputString) As String
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()
        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next
        Return stringBuilder.ToString()
    End Function
    Public Function UpdateDocumentNoForPhonepe() As Boolean
        Try
            Dim trans As SqlTransaction
            If UpdateDocumentNo("PhonePe", SpectrumCon, trans) = False Then
                trans.Rollback()
                CloseConnection()
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetPhonePePaymentRequestResponseStruct() As DataTable
        Try
            Dim strrquery = "SELECT * FROM PhonePePaymentRequestResponseDtl WHERE  1= 0 "
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(strrquery.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)
            Sqldt.TableName = "PhonePePaymentRequestResponseDtl"
            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function
    Public Function SaveOnlineTenderData(ByVal dtData As DataTable, ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction
        Try

            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(dtData, SpectrumCon, tran) Then
                tran.Commit()
                CloseConnection()
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function UpdatePhonePeRequestResponse(ByVal Sitecode As String, ByVal TransactionId As String, ByVal RequestResponse As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  paymentrequestresponse='" & RequestResponse & "', updatedon=GetDate() WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdatePhonePeCheckRequest(ByVal Sitecode As String, ByVal TransactionId As String, ByVal RequestResponse As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  CheckRequest='" & RequestResponse & "', updatedon=GetDate() WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdatePhonePeCheckRequestResponse(ByVal Sitecode As String, ByVal TransactionId As String, ByVal RequestResponse As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  CheckRequestResponse='" & RequestResponse & "', updatedon=GetDate() WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdatePhonePePaymentStatus(ByVal Sitecode As String, ByVal TransactionId As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  IsPaymentDone='1' WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdatePhonePeCancelRequest(ByVal Sitecode As String, ByVal TransactionId As String, ByVal RequestResponse As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  CancelRequest='" & RequestResponse & "', updatedon=GetDate() WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdatePhonePeCancelRequestResponse(ByVal Sitecode As String, ByVal TransactionId As String, ByVal RequestResponse As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update PhonePePaymentRequestResponseDtl set  CancelRequestResponse='" & RequestResponse & "', updatedon=GetDate() WHERE TransactionId='" & TransactionId & "'  and SiteCode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
#End Region
#Region "Hashatag"
    Public Function UpdateHashtagResponse(ByVal Sitecode As String, ByVal HashtagResponse As String, ByVal HashtagRequestId As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update HashtagRequestResponseDtl set  HashtagResponse='" & HashtagResponse & "', updatedon=GetDate() WHERE Id='" & HashtagRequestId & "' "
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function SaveHashtagRequestData(ByVal SiteCode As String, ByVal HashtagRequest As String, ByVal UserId As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "SaveAndGetHashtagRequestId"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@HashtagRequest", HashtagRequest)
            cmd1.Parameters.AddWithValue("@UserId", UserId)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOfferTypeByOfferNo(ByVal OFFERNO As String) As DataTable
        Try
            Dim query As String = "select OfferType from promotions where offerno='" & OFFERNO & "' and OfferActive =1 and status=1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCustomerNameAndCardNo(ByVal mobileno As String) As DataTable
        Try
            Dim query As String = " select CardNo ,NameOnCard from clpcustomers where Mobileno ='" & mobileno & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetCustomerMobileNo(ByVal CARDNO As String) As DataTable
        Try
            Dim query As String = " SELECT MOBILENO FROM CLPCUSTOMERS WHERE  CARDNO ='" & CARDNO & "'AND STATUS=1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateVoidBillRequestForHashTag(ByVal VoidJsonRequest As String, ByVal billno As String, ByVal sitecode As String, ByVal finyear As String) As Boolean
        Dim tran As SqlTransaction
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim str As String = ""

            str = " Update CustomerBillInfoForHashTag set  VoidJsonRequest='" & VoidJsonRequest & "',VoidHashtagSynchStatus='NOT_INITIATED', updatedon=GetDate(), IsBillVoid=1 WHERE billno='" & billno & "' and sitecode= '" & sitecode & "' and finyear= '" & finyear & "' "
            Dim cmd As New SqlCommand(str, SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
#End Region

#Region "ManualSync"

    Public Function ExecMSTArticleGroup(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMSTArticleGroup"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MSTArticleGroup", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MSTArticleGroupType"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function
    Public Function GetTableStruct(ByVal TableName As String, Optional ByVal strCols As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim strString As String = ""
            If strCols <> "" Then
                strString = "select " & strCols & " from " & TableName & " where 1=0"
            Else
                strString = "select * from " & TableName & " where 1=0"
            End If
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            da.Fill(dt)
            dt.TableName = TableName
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function ExecArticleUOM(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execArticleUOM"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@ArticleUOM", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "ArticleUOMtype"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function ExecBUTTONGROUP(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execBUTTONGROUP"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@BUTTONGROUP", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "BUTTONGROUPType"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecButtonarticle(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execButtonarticle"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@Buttonarticle", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "BUTTONARTICLEType"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMSTArticleGroupDtl(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMSTArticleGroupDtl"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MSTArticleGroupDtl", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MSTArticleGroupDtlType"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMstArticleImage(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstArticleImage"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim newColumn As New Data.DataColumn("GroupID", GetType(System.String))
            newColumn.DefaultValue = ""
            dtSP.Columns.Add(newColumn)
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticleImage", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleImageTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMstArticleKit(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstArticleKit"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticleKit", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleKitTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecAuthUserSiteRoleMap(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execAuthUserSiteRoleMap"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@AuthUserSiteRoleMap", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "AuthUserSiteRoleMapTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecAuthRoleTransactionMap(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execAuthRoleTransactionMap"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@AuthRoleTransactionMap", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "AuthRoleTransactionMapTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecAuthUsers(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execAuthUsers"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@AuthUsers", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "AuthUsersTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecDefaultConfig(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execDefaultConfig"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@DefaultConfig", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "DefaultConfigTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecArticleTreeNodeMap(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execArticleTreeNodeMap"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@ArticleTreeNodeMap", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "ArticleTreeNodeMapTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function Execarticlenodemap(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execarticlenodemap"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@articlenodemap", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "ArticleNodeMapTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecArticleMAP(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execArticleMAP"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@ArticleMAP", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "ArticleMAPType"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            'CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMstArticleNode(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstArticleNode"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticleNode", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleNodeTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            'CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecArticleStockBalances(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execArticleStockBalances"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@ArticleStockBalances", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "ArticleStockBalancesTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            ' CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function Execprintingdetail(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execprintingdetail"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@printingdetail", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "printingdetailTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            'CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMstArticleCombo(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstArticleCombo"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticleCombo", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleComboTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecMstArticle(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstArticle"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticle", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            'CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function ExecPurchaseInfoRecord(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execPurchaseInfoRecord"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@PurchaseInfoRecord", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "PurchaseInfoRecordTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            ' CloseConnection()
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function UpdatePurchaseInfoRecord(ByVal SiteCode1 As String, ByVal SupplierCode1 As String, ByVal EAN1 As String, ByVal CPBaseCurr1 As Double, ByVal CPLocalCurr1 As Double, ByRef trans1 As SqlTransaction, ByRef con1 As SqlConnection, ByRef Errormsg1 As String) As Boolean
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "udp_UpdatePurchaseInfoRecord"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con1
            cmd1.Transaction = trans1
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode1)
            cmd1.Parameters.AddWithValue("@SupplierCode", SupplierCode1)
            cmd1.Parameters.AddWithValue("@EAN", EAN1)
            cmd1.Parameters.AddWithValue("@CPBaseCurr", CPBaseCurr1)
            cmd1.Parameters.AddWithValue("@CPLocalCurr", CPLocalCurr1)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return True
        Catch ex As Exception
            LogException(ex)
            Errormsg1 = ex.Message
            Return False
        End Try
    End Function

    Public Function ExecSalesInfoRecord(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execSalesInfoRecord"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@SalesInfoRecord", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "SalesInfoRecordTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function ExecMstEAN(ByVal dtSP As DataTable, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "execMstEAN"
            cmd1.CommandType = CommandType.StoredProcedure
            cmd1.Connection = con
            cmd1.Transaction = trans
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstEAN", SqlDbType.Structured)
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstEANTYPE"
            myparam.Value = dtSP
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            Return dtSP
        Catch ex As Exception
            LogException(ex)
            Errormsg = ex.Message
            Return Nothing
        Finally
        End Try
    End Function

    Public Function Exec_USP_MatchRecords(ByVal dtSP As DataTable, ByVal SiteCode As String, ByRef trans As SqlTransaction, ByRef con As SqlConnection) As DataTable
        Try
            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.Connection = con
            cmd1.Transaction = trans
            cmd1.CommandText = "USP_MatchRecords"
            cmd1.CommandType = CommandType.StoredProcedure
            Dim myparam As SqlParameter = cmd1.Parameters.Add("@MstArticle", SqlDbType.Structured)
            Dim myParam2 As SqlParameter = cmd1.Parameters.Add("@SITECODE", SqlDbType.NVarChar)
            myParam2.SqlDbType = SqlDbType.NVarChar
            myParam2.Value = SiteCode
            myparam.SqlDbType = SqlDbType.Structured
            myparam.TypeName = "MstArticleTYPE"
            myparam.Value = dtSP
            Dim dadata As New SqlDataAdapter(cmd1)
            dadata.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Public Function getStruct(ByVal tablename As String) As DataTable
    '    Try
    '        Dim dt As New DataTable
    '        Dim clonedTable As New DataTable
    '        Dim strString As String = ""
    '        If tablename.ToUpper = "MSTARTICLE" Then
    '            strString = "select top 1 ArticleCode,ArticalTypeCode,ArticalCatCode,ArticleShortName,ArticleName,BaseUnitofMeasure,DistributionUnitofMeasure,SaleUnitofMeasure,OrderUnitofMeasure,CataloguedOn,ArticleActive,IssueFreeGift,ProductImage,TotalShelfLife,RemainingShelfLife,DecimalQtyApplicable,IsMrpOpen,SerialNumber,WarrantyPeriod,ParentArt,TreeID,LastNodeCode,CharProfileCode,SupplierRef,STLocCode,Salable,ToleranceValue,LegacyArticleCode,Purchaser,IntraStatCodeEurope,IntraStatCodeUSA,NetWeight,NetWeightUOM,GrossWeight,GrossWeightUOM,Volume,VolumeUOM,Style,Season,Theme,IsPremaman,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,NONRETUNABLE,MaterialTypeCode,ConsumptionUoM,isExpiry,Printable,IsEanAutoGenerate,IsBatchBarcodeAutoGenerate,SalesUomValue,ManufacturerCode,BrandCode,DistributionUomValue,Description,HSNCode from " & tablename & " where 1=0"
    '        Else
    '            strString = "select top 1 * from " & tablename & " where 1=0"
    '        End If
    '        Dim cmd As New SqlCommand(strString, SpectrumCon())
    '        Dim da As New SqlDataAdapter(cmd)
    '        dt = New DataTable
    '        da.Fill(dt)
    '        clonedTable = dt.Clone()
    '        Return clonedTable
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function getStruct(ByVal tablename As String, ByRef trans As SqlTransaction, ByRef con As SqlConnection, ByRef Errormsg As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim clonedTable As New DataTable
            Dim strString As String = ""
            If tablename.ToUpper = "MSTARTICLE" Then
                strString = "select top 1 ArticleCode,ArticalTypeCode,ArticalCatCode,ArticleShortName,ArticleName,BaseUnitofMeasure,DistributionUnitofMeasure,SaleUnitofMeasure,OrderUnitofMeasure,CataloguedOn,ArticleActive,IssueFreeGift,ProductImage,TotalShelfLife,RemainingShelfLife,DecimalQtyApplicable,IsMrpOpen,SerialNumber,WarrantyPeriod,ParentArt,TreeID,LastNodeCode,CharProfileCode,SupplierRef,STLocCode,Salable,ToleranceValue,LegacyArticleCode,Purchaser,IntraStatCodeEurope,IntraStatCodeUSA,NetWeight,NetWeightUOM,GrossWeight,GrossWeightUOM,Volume,VolumeUOM,Style,Season,Theme,IsPremaman,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,NONRETUNABLE,MaterialTypeCode,ConsumptionUoM,isExpiry,Printable,IsEanAutoGenerate,IsBatchBarcodeAutoGenerate,SalesUomValue,ManufacturerCode,BrandCode,DistributionUomValue,Description,HSNCode from " & tablename & " where 1=0"
            Else
                strString = "select top 1 * from " & tablename & " where 1=0"
            End If
            Dim cmd As New SqlCommand(strString, con, trans)
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            clonedTable = dt.Clone()
            Return clonedTable
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function SavePullSyncAuditLog(ByVal SiteCode As String, ByVal ServiceName As String, ByVal userid As String) As Integer
        Dim RequestId As Integer = 0
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "SavePullSyncAuditLog"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@ServiceName", ServiceName)
            cmd1.Parameters.AddWithValue("@UserId", userid)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                RequestId = dt.Rows(0)("RequestId")
            End If
            CloseConnection()
            Return RequestId
        Catch ex As Exception
            LogException(ex)
            Return RequestId
        Finally
        End Try
    End Function

    Public Function UpdatePullSyncAuditLog(ByVal SiteCode As String, ByVal ServiceName As String, ByVal userid As String, ByVal ReqID As String, ByVal ErrorLog As String) As Boolean
        Dim RequestId As Integer = 0
        Try

            Dim dt As New DataTable
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "UpdatePullSyncAuditLog"
            cmd1.CommandType = CommandType.StoredProcedure
            OpenConnection()
            cmd1.Connection = SpectrumCon()
            cmd1.Parameters.AddWithValue("@SiteCode", SiteCode)
            cmd1.Parameters.AddWithValue("@ServiceName", ServiceName)
            cmd1.Parameters.AddWithValue("@UserId", userid)
            cmd1.Parameters.AddWithValue("@ReqID", ReqID)
            cmd1.Parameters.AddWithValue("@ErrorLog", ErrorLog)
            Dim daData As New SqlDataAdapter(cmd1)
            daData.Fill(dt)
            CloseConnection()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
        End Try
    End Function

    Public Function GetCsvData(ByVal strFolderPath As String, ByVal strFileName As String) As DataTable



        Try
            ' Dim folder = "Z:\"
            ' Dim CnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""text;HDR=No;FMT=Delimited"";"

            OpenConnection()
            Dim cmd As New SqlCommand("SELECT * FROM [" & strFileName & "]", SpectrumCon())
            Dim da As New SqlDataAdapter()

            da.SelectCommand = cmd

            Dim ds As New DataTable()

            da.Fill(ds)
            da.Dispose()

            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try

    End Function

    Public Function GetSummaryDataOnDayClose(ByVal BillDate As String) As DataSet
        Try
            Try
                Dim dt As New DataSet
                Dim cmd1 As New SqlCommand
                cmd1.CommandType = CommandType.Text
                cmd1.CommandText = "GetSummaryDataOnDayClose"
                cmd1.CommandType = CommandType.StoredProcedure
                OpenConnection()
                cmd1.Connection = SpectrumCon()
                cmd1.Parameters.AddWithValue("@BillDate", BillDate)
                Dim daData As New SqlDataAdapter(cmd1)
                daData.Fill(dt)
                CloseConnection()
                Return dt
            Catch ex As Exception
                LogException(ex)
                Return Nothing
            End Try
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
End Class


Public Module CommanFuntion
    Private _strLogFilePath As String
    Private Property LogFilePath() As String
        Get
            Return _strLogFilePath
        End Get
        Set(ByVal value As String)
            _strLogFilePath = value
        End Set
    End Property
    Private _strApplicationFilePath As String
    Private Property ApplicationPath() As String
        Get
            Return _strApplicationFilePath
        End Get
        Set(ByVal value As String)
            _strApplicationFilePath = value
        End Set
    End Property
    ''' <summary>
    ''' Shift Activity 
    ''' </summary>
    ''' <remarks></remarks>
    Private _strShiftLogFilePath As String
    Private Property ShiftLogFilePath() As String
        Get
            Return _strShiftLogFilePath
        End Get
        Set(ByVal value As String)
            _strShiftLogFilePath = value
        End Set
    End Property
    Private _strShiftApplicationFilePath As String
    Private Property ShiftApplicationPath() As String
        Get
            Return _strShiftApplicationFilePath
        End Get
        Set(ByVal value As String)
            _strShiftApplicationFilePath = value
        End Set
    End Property
    ''' <summary>
    '''  Log Exception messages into txt files 
    ''' </summary>
    ''' <returns>On success, return true otherwise false </returns>
    ''' <remarks></remarks> 
    Public Function LogException(ByRef objException As Exception, Optional ByVal strSiteCode As String = "", Optional ByVal strTerminalID As String = "", Optional ByVal strUserName As String = "", Optional ByVal strApplicationPath As String = "", Optional ByVal strLogFilePath As String = "") As Boolean
        Try
            If strApplicationPath = String.Empty Then
                strApplicationPath = ApplicationPath
            End If
            If strLogFilePath = String.Empty Then
                strLogFilePath = LogFilePath
            End If
            Dim strDate As DateTime = DateAndTime.Now
            Dim strFormatDate As String = strDate.Date
            Dim sbParaText As New StringBuilder
            strFormatDate = strFormatDate.Replace("/", "-")
            strFormatDate = strFormatDate.Replace(":", "-")
            strFormatDate = strFormatDate.Replace(" ", "")
            Dim objStreamWriter As StreamWriter
            If Not strLogFilePath = String.Empty Then
                If (File.Exists(strLogFilePath)) Then
                    objStreamWriter = File.AppendText(strLogFilePath)
                Else
                    objStreamWriter = File.CreateText(strLogFilePath)
                    sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                    sbParaText.AppendLine("------------------ Spectrum Log  -------------------------" & vbCrLf)
                    sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                    sbParaText.AppendLine(String.Format("SiteCode: {0} ", strSiteCode) & vbCrLf)
                    sbParaText.AppendLine(String.Format("Terminal ID: {0} ", strTerminalID) & vbCrLf)
                    sbParaText.AppendLine(String.Format("User Name : {0} ", strUserName) & vbCrLf)
                    LogFilePath = strLogFilePath
                End If
                sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                sbParaText.AppendLine(objException.ToString() & vbCrLf)
                objStreamWriter.Write(sbParaText.ToString)
                objStreamWriter.Flush()
                objStreamWriter.Close()
            Else
                LogFileCreate(strSiteCode, strTerminalID, strUserName, strApplicationPath, strLogFilePath)
                objStreamWriter = File.AppendText(strLogFilePath)
                sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                sbParaText.AppendLine(objException.ToString() & vbCrLf)
                objStreamWriter.Write(sbParaText.ToString)
                objStreamWriter.Flush()
                objStreamWriter.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    '''  File Create at login time 
    ''' </summary>
    ''' <returns>On Success return true,otherwise return false  </returns>
    ''' <remarks></remarks>
    Public Function LogFileCreate(ByVal strSiteCode As String, ByVal strTerminalID As String, ByVal strUserName As String, ByVal strApplicationPath As String, Optional ByRef strLogFilePath As String = "") As Boolean
        Try
            Dim strDate As DateTime = DateAndTime.Now
            Dim strDateS As String = strDate.Date.ToString()
            Dim strTime As String = strDate.TimeOfDay.ToString()
            ' Dim strFormatDate As String = String.Concat(strDateS, strTime)
            Dim strFormatDate As String = strDateS
            Dim sbParaText As New StringBuilder
            strFormatDate = strFormatDate.Replace(".", "")
            strFormatDate = strFormatDate.Replace("/", "-")
            strFormatDate = strFormatDate.Replace(":", "_")
            strFormatDate = strFormatDate.Replace(" ", "")
            Dim strFilePath As String = String.Format("{0}\SpectrumLog_{1}.txt", strApplicationPath, strFormatDate)
            strLogFilePath = strFilePath
            Dim objStreamWriter As StreamWriter
            If Not (File.Exists(strFilePath)) Then
                objStreamWriter = File.CreateText(strFilePath)
                sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                sbParaText.AppendLine("------------------ Spectrum Log  -------------------------" & vbCrLf)
                sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                sbParaText.AppendLine(String.Format("SiteCode: {0} ", strSiteCode) & vbCrLf)
                sbParaText.AppendLine(String.Format("Terminal ID: {0} ", strTerminalID) & vbCrLf)
                sbParaText.AppendLine(String.Format("User Name : {0} ", strUserName) & vbCrLf)
                objStreamWriter.Write(sbParaText.ToString)
                objStreamWriter.Flush()
                objStreamWriter.Close()
                LogFilePath = strFilePath
                ApplicationPath = strApplicationPath
            Else
                LogFilePath = strFilePath
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LogFileForShiftActivityCreate(ByVal strSiteCode As String, ByVal strTerminalID As String, ByVal strUserName As String, ByVal strApplicationPath As String, Optional ByRef strLogFilePath As String = "", Optional ByVal strShiftID As String = "") As Boolean
        Try
            Dim strDate As DateTime = DateAndTime.Now
            Dim strDateS As String = strDate.Date.ToString()
            Dim strTime As String = strDate.TimeOfDay.ToString()
            ' Dim strFormatDate As String = String.Concat(strDateS, strTime)
            Dim strFormatDate As String = strDateS
            Dim sbParaText As New StringBuilder
            strFormatDate = strFormatDate.Replace(".", "")
            strFormatDate = strFormatDate.Replace("/", "-")
            strFormatDate = strFormatDate.Replace(":", "_")
            strFormatDate = strFormatDate.Replace(" ", "")

            Dim strFilePath As String = String.Format("{0}\ActivityLogForShift_{1}_{2}.txt", strApplicationPath, strFormatDate, strTerminalID)

            strLogFilePath = strFilePath

            Dim objStreamWriter As StreamWriter
            If Not (File.Exists(strFilePath)) Then
                objStreamWriter = File.CreateText(strFilePath)
                sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                sbParaText.AppendLine("------------------ Shift Activity Log  -------------------------" & vbCrLf)
                sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                sbParaText.AppendLine(String.Format("SiteCode: {0} ", strSiteCode) & vbCrLf)
                sbParaText.AppendLine(String.Format("Terminal ID: {0} ", strTerminalID) & vbCrLf)
                'sbParaText.AppendLine(String.Format("Shift ID: {0} ", strShiftID) & vbCrLf)
                sbParaText.AppendLine(String.Format("User Name : {0} ", strUserName) & vbCrLf)
                objStreamWriter.Write(sbParaText.ToString)
                objStreamWriter.Flush()
                objStreamWriter.Close()
                ShiftLogFilePath = strFilePath
                ShiftApplicationPath = strApplicationPath
            Else
                ShiftLogFilePath = strFilePath
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Sub SetCostPrice(ByVal isMAP As Boolean, ByRef TargetDataTable As DataTable, ByVal StrSiteCode As String, ByVal CostColumnName As String, Optional isBatchManagementReq As Boolean = False)
        Try
            'If isBatchManagementReq Then
            Dim cmdCostPrice As New SqlCommand
            cmdCostPrice.CommandType = CommandType.Text
            cmdCostPrice.CommandText = ""
            cmdCostPrice.Connection = SpectrumCon()
            DBConnection.OpenConnection()
            ' Create a SqlParameter for each parameter in the stored procedure.
            Dim PSiteCode As New SqlParameter("@PSiteCode", "a%")
            Dim PBarCode As New SqlParameter("@PBARCODE", "a%")
            Dim PArticleCode As New SqlParameter("@PARTICLECODE", "a%")
            cmdCostPrice.Parameters.Add(PSiteCode)
            cmdCostPrice.Parameters.Add(PBarCode)
            cmdCostPrice.Parameters.Add(PArticleCode)
            PSiteCode.Value = StrSiteCode
            For Each dtRow As DataRow In TargetDataTable.Rows
                If Not dtRow.RowState = DataRowState.Deleted Then

                    If Not TargetDataTable.Columns.Contains("BatchBarcode") Then
                        If isMAP = True Then
                            cmdCostPrice.CommandText = "SELECT ISNULL(MAP,0) FROM PurchaseInfoRecord WHERE SITECODE=@PSITECODE AND ARTICLECODE = @PARTICLECODE AND STATUS = 1 and IsDefaultSupplier=1"
                        Else
                            cmdCostPrice.CommandText = " SELECT ISNULL(CPBaseCurr,0) FROM PurchaseInfoRecord WHERE SITECODE=@PSITECODE AND ARTICLECODE = @PARTICLECODE AND STATUS = 1 and IsDefaultSupplier=1"
                        End If
                        'cmdCostPrice.Parameters.Add(PArticleCode)
                        PArticleCode.Value = dtRow("ArticleCode")
                    Else
                        cmdCostPrice.CommandText = "select LandingCostPrice from BatchDtl where SiteCode=@PSITECODE and BatchBarcode =@PBARCODE "
                        'cmdCostPrice.Parameters.Add(PBarCode)
                        If IsDBNull(dtRow("BatchBarcode")) Then
                        Else
                            PBarCode.Value = dtRow("BatchBarcode")
                        End If

                    End If
                    If dtRow(CostColumnName) Is DBNull.Value Then
                        Try
                            Dim dblCost As Double = cmdCostPrice.ExecuteScalar()
                            dtRow(CostColumnName) = IIf(dblCost = Nothing, DBNull.Value, dblCost)
                            dtRow("ClpRequire") = True
                        Catch ex As Exception

                        End Try

                    End If
                End If

            Next
            'Else
            'Dim cmdCostPrice As New SqlCommand
            'cmdCostPrice.CommandType = CommandType.Text
            'If isMAP = True Then
            '    cmdCostPrice.CommandText = "SELECT ISNULL(MAP,0) FROM ARTICLEMAP WHERE SITECODE=@PSITECODE AND ARTICLECODE = @PARTICLECODE AND STATUS = 1 ORDER BY TRANSACTIONDATE DESC"
            'Else
            '    cmdCostPrice.CommandText = " SELECT ISNULL(CPLocalCurr,0) FROM dbo.PurchaseInfoRecord WHERE SITECODE=@PSITECODE AND ARTICLECODE = @PARTICLECODE AND STATUS = 1 ORDER BY FromDate DESC"
            'End If

            'cmdCostPrice.Connection = SpectrumCon()
            'DBConnection.OpenConnection()
            '' Create a SqlParameter for each parameter in the stored procedure.
            'Dim PSiteCode As New SqlParameter("@PSiteCode", "a%")
            'Dim PArticleCode As New SqlParameter("@PARTICLECODE", "a%")
            'cmdCostPrice.Parameters.Add(PSiteCode)
            'cmdCostPrice.Parameters.Add(PArticleCode)
            'PSiteCode.Value = StrSiteCode
            'For Each dtRow As DataRow In TargetDataTable.Rows
            '    If Not dtRow.RowState = DataRowState.Deleted Then
            '        PArticleCode.Value = dtRow("ArticleCode")
            '        If dtRow(CostColumnName) Is DBNull.Value Then
            '            Try
            '                Dim dblCost As Double = cmdCostPrice.ExecuteScalar()
            '                dtRow(CostColumnName) = IIf(dblCost = Nothing, DBNull.Value, dblCost)
            '            Catch ex As Exception

            '            End Try

            '        End If
            '    End If

            'Next
            'End If
        Catch ex As Exception

        Finally
            DBConnection.CloseConnection()
        End Try

    End Sub

    Public Function MyRound(ByVal Amount As Double, ByVal RoundedAt As Int32, Optional ByVal IsRoundOffRequired As Boolean = True) As Double
        Try
            If IsRoundOffRequired Then
                Dim Amt As Double = Amount
                Amount = Math.Floor(Amount)
                Amt = Amt - Amount
                Amt = Math.Round(Amt, 2)
                Amt = MyOwnRound(RoundedAt, Amt * 100)
                Amount = Amount + (Amt * 0.01)
                Return Amount
            Else
                Return Amount
            End If
            Return Amount
        Catch ex As Exception
            Return Amount
        End Try
    End Function

    Private Function MyOwnRound(ByVal NumberToRound As Integer, _
                                ByVal ValueToRound As Integer) As Integer
        ' changed the datatype of "ValueToRound" from integer to double .
        'Changed by rahul katkar . for roundoff correction in cashmemo screen.

        Dim HalfRound As Integer = NumberToRound \ 2
        If NumberToRound < 0 Then
            NumberToRound = Math.Abs(NumberToRound)
        End If
        If NumberToRound = 0 Then Exit Function
        If ValueToRound Mod NumberToRound > HalfRound Then
            MyOwnRound = ValueToRound + (NumberToRound - (ValueToRound Mod NumberToRound))
        Else
            MyOwnRound = ValueToRound - (ValueToRound Mod NumberToRound)
        End If
    End Function

    Public _IMaxQuantity As Integer = 999999999
    Public Property MaxQuantity() As Integer
        Get
            Return _IMaxQuantity
        End Get
        Set(ByVal value As Integer)
            _IMaxQuantity = value
        End Set
    End Property

    Public Function GetTenderInfo(ByVal SiteCode As String, Optional ByVal vTenderType As String = "") As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" SELECT A.TenderHeadCode, A.TenderHeadName, A.TenderType, A.Positive_Negative,  ")
            vStmtQry.Append("            A.MaxNo, A.MaxValue, A.MinBillValue, A.DefaultValue  ")
            vStmtQry.Append(" FROM MstTender A  ")
            vStmtQry.Append(" INNER JOIN MstTenderType B ON A.TenderType = B.TenderType  ")
            vStmtQry.Append(" Where A.SiteCode = '" & SiteCode & "' AND A.status=1 --And B.status = 1")

            If Not String.IsNullOrEmpty(vTenderType) Then
                vStmtQry.Append(" AND ")
                vStmtQry.Append(" (B.TenderType = '" & vTenderType & "')")
            End If

            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try
    End Function

    Public Function CheckVoucherSiteMapForVoucherType(ByVal sitecode As String, ByVal VourcherType As String) As Boolean
        Try
            Dim strQuery As New StringBuilder
            strQuery.Length = 0
            strQuery.Append(" SELECT MV.VoucherCode  ")
            strQuery.Append(" FROM  MSTVoucher as MV  ")
            strQuery.Append("         INNER JOIN vouchersitemap as VSM ON MV.VoucherCode = VSM.VoucherCode ")
            strQuery.Append(" Where MV.VourcherType ='" & VourcherType & "'")
            strQuery.Append(" AND VSM.SiteCode = '" & sitecode & "'")
            strQuery.Append("  AND MV.STATUS = 1 AND VSM.STATUS = 1 ")

            Dim dt As New DataTable
            Dim daVoucher As New SqlDataAdapter(strQuery.ToString, ConString)
            daVoucher.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetFormTabStop(ByRef objForm As Form, Optional ByVal tabStopValue As Boolean = False) As Boolean
        SetFormTabStop = False
        Try
            Dim stack = New Stack(Of Control)()
            For Each ctrl As Control In objForm.Controls
                ctrl.TabStop = tabStopValue
                stack.Push(ctrl)
            Next

            While stack.Count > 0
                Dim nextctrl As Control = stack.Pop()
                For Each child As Control In nextctrl.Controls
                    child.TabStop = tabStopValue
                    stack.Push(child)
                Next
            End While
            SetFormTabStop = True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetFormTabIndex(ByRef ctrTablIndex As Dictionary(Of Object, Int16)) As Boolean
        SetFormTabIndex = False
        Try
            For controlCtr = 0 To ctrTablIndex.Count - 1 Step 1
                Dim nextctrl As Control = ctrTablIndex.Keys(controlCtr)
                nextctrl.TabStop = True
                nextctrl.TabIndex = ctrTablIndex.Values(controlCtr)
            Next controlCtr
            SetFormTabIndex = True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub CheckTabOrder(ByRef objForm As Form)
        Dim msg As String = ""
        Dim stack = New Stack(Of Control)()
        For Each ctrl As Control In objForm.Controls
            msg = msg & ctrl.Name & " " & ctrl.TabStop.ToString() & " " & ctrl.TabIndex & vbCrLf
            stack.Push(ctrl)
        Next

        While stack.Count > 0
            Dim nextctrl As Control = stack.Pop()
            For Each child As Control In nextctrl.Controls
                msg = msg & child.Name & " " & child.TabStop.ToString() & " " & child.TabIndex & vbCrLf
                stack.Push(child)
            Next
        End While
        MsgBox(msg)
    End Sub

    Public Function CalculateAmountTenderedExchangeRate(ByVal iselectedCurrencyIndex As String, ByVal ibaseCurrencyIndex As String) As Double

        Dim decCalculateTotalAmount As Decimal
        Try
            Dim dsCurrencyRate As New DataSet
            Dim decCurrentCurrencyRateAgainstBaseCurrency As Decimal = 1.0
            Dim isExchangeRateFound As Boolean = True
            If ibaseCurrencyIndex <> iselectedCurrencyIndex Then
                Try
                    OpenConnection()
                    Dim sqlQuery As New StringBuilder
                    sqlQuery.Append("SELECT A.CurrencyDescription,B.ExchangeQty,B.ExchangeRate " + vbCrLf)
                    sqlQuery.Append("FROM MSTCurrency A" + vbCrLf)
                    sqlQuery.Append("INNER JOIN MSTCurrencyRate B ON A.CurrencyCode = B.CurrencyCode " + vbCrLf)
                    sqlQuery.Append("WHERE B.CurrencyCode='" & ibaseCurrencyIndex & "' AND B.RelationalCurrency='" & iselectedCurrencyIndex & "' " + vbCrLf)
                    sqlQuery.Append("AND EndDate IS NULL")

                    Using sqlSelectCommand As New SqlCommand(sqlQuery.ToString(), SpectrumCon)
                        Using sqlAdapter As New SqlDataAdapter()
                            sqlAdapter.SelectCommand = sqlSelectCommand
                            sqlAdapter.Fill(dsCurrencyRate, "tblCurrencyRate")
                        End Using
                    End Using

                    If dsCurrencyRate.Tables("tblCurrencyRate").Rows.Count > 0 Then
                        Dim decCurrentCurrencyQtyAgainstBaseCurrency As Double
                        decCurrentCurrencyQtyAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeQty"))
                        'decCurrentCurrencyRateAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeQty"))
                        decCurrentCurrencyRateAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeRate"))
                        decCurrentCurrencyRateAgainstBaseCurrency = decCurrentCurrencyRateAgainstBaseCurrency / decCurrentCurrencyQtyAgainstBaseCurrency

                    Else
                        isExchangeRateFound = False
                    End If
                Catch ex As Exception
                    decCurrentCurrencyRateAgainstBaseCurrency = 0
                    isExchangeRateFound = False
                Finally
                    CloseConnection()
                End Try
            End If
            Return decCurrentCurrencyRateAgainstBaseCurrency
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function ActivityLogForShift(ByVal dt As DataTable, ByVal message As String, Optional ByVal strShiftID As String = "", Optional ByVal strSiteCode As String = "", Optional ByVal strTerminalID As String = "", Optional ByVal strUserName As String = "", Optional ByVal strApplicationPath As String = "", Optional ByVal strFilePath As String = "") As Boolean
        Try
            Dim strDate As DateTime = DateAndTime.Now
            Dim strFormatDate As String = strDate.Date
            Dim sbParaText As New StringBuilder
            strFormatDate = strFormatDate.Replace("/", "-")
            strFormatDate = strFormatDate.Replace(":", "-")
            strFormatDate = strFormatDate.Replace(" ", "")

            If strApplicationPath = String.Empty Then
                strApplicationPath = ShiftApplicationPath
            End If
            If strFilePath = String.Empty Then
                strFilePath = ShiftLogFilePath
            End If

            Dim objStreamWriter As StreamWriter
            If Not strFilePath = String.Empty Then
                If (File.Exists(strFilePath)) Then
                    objStreamWriter = File.AppendText(strFilePath)
                Else
                    objStreamWriter = File.CreateText(strFilePath)
                    sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                    sbParaText.AppendLine("------------------ Shift Activity Log  -------------------------" & vbCrLf)
                    sbParaText.AppendLine(String.Format("Date : {0} Time:{1} ", strDate.ToShortDateString(), strDate.ToShortTimeString()) & vbCrLf)
                    sbParaText.AppendLine(String.Format("SiteCode: {0} ", strSiteCode) & vbCrLf)
                    sbParaText.AppendLine(String.Format("Terminal ID: {0} ", strTerminalID) & vbCrLf)
                    'sbParaText.AppendLine(String.Format("Shift ID: {0} ", strShiftID) & vbCrLf)
                    sbParaText.AppendLine(String.Format("User Name : {0} ", strUserName) & vbCrLf)
                    ' LogFilePath = strFilePath
                End If
                sbParaText.AppendLine("----------------------------------------------------------" & vbCrLf)
                sbParaText.AppendLine(String.Format("Date : {0}  Time:{1} ShiftId :{2}  ", strDate.ToShortDateString(), strDate.ToShortTimeString(), strShiftID) & vbCrLf)
                sbParaText.AppendLine(message)
                objStreamWriter.Write(sbParaText.ToString)

                Dim txt As String = String.Empty
                If dt IsNot Nothing Then
                    txt = BuildTable(dt)
                End If


                objStreamWriter.Write(txt.ToString)
                objStreamWriter.Flush()
                objStreamWriter.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

   


    Public Function BuildTable(ByVal dt As DataTable) As String

        Dim result As New StringBuilder
        Dim widths As New List(Of Integer)
        Const ColumnSeparator As Char = "|"c
        Const HeadingUnderline As Char = "-"c

        ' determine width of each column based on widest of either column heading or values in that column
        For Each col As DataColumn In dt.Columns
            Dim colWidth As Integer = Integer.MinValue
            For Each row As DataRow In dt.Rows
                Dim len As Integer = row(col.ColumnName).ToString.Length
                If len > colWidth Then
                    colWidth = len
                End If
            Next
            widths.Add(CInt(IIf(colWidth < col.ColumnName.Length, col.ColumnName.Length + 1, colWidth + 1)))
        Next

        ' write column headers
        For Each col As DataColumn In dt.Columns
            result.Append(col.ColumnName.PadLeft(widths(col.Ordinal)))
            result.Append(ColumnSeparator)
        Next
        result.AppendLine()

        ' write heading underline
        For Each col As DataColumn In dt.Columns
            Dim horizontal As String = New String(HeadingUnderline, widths(col.Ordinal))
            result.Append(horizontal.PadLeft(widths(col.Ordinal)))
            result.Append(ColumnSeparator)
        Next
        result.AppendLine()

        ' write each row
        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                result.Append(row(col.ColumnName).ToString.PadLeft(widths(col.Ordinal)))
                result.Append(ColumnSeparator)
            Next
            result.AppendLine()
        Next

        Return result.ToString()

    End Function

    'jk sprint 25

    'list of department map for user
    Public Function GetDepartmentMapForUser(Optional ByVal userid1 As String = "") As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from userdepartmentmap where status=1 and userid ='" + userid1 + "' And  DepartmentOwner='1'"

            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'list of site map for user
    Public Function GetSiteMapForUser(Optional ByVal userid1 As String = "") As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from AuthUserSiteRoleMap where status=1 and userid ='" + userid1 + "'"

            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'edit ticket INFO
    Public Function GetGrievanceTicketInfo(Optional ByVal id As String = "") As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select * from GrievanceDetails where GrievanceId='" & id & "' "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function DeptMapTOTicket(ByVal GrievanceId As String) As DataTable

        Try
            Dim dt As DataTable
            Dim strString As String = "select CCDepartment from GrievanceDetails where GrievanceId in ('" + GrievanceId + "')"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added for issue id 2189 by vipul
    Public Function GetBillTenderInfo(ByVal refbillno As String) As DataTable
        Try
            Dim strrquery = "select * from creditreceipt where RefBillNo ='" & refbillno & "'"
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(strrquery.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try

    End Function

    ' code added by vipul for stocktake
    Public Function ConvertToDataTable(Of t)(ByVal list As IList(Of t)) As DataTable

        Try
            Dim table As New DataTable()
            If Not list.Any Then
                'don't know schema ....
                Return table
            End If
            Dim fields() = list.First.GetType.GetProperties
            For Each field In fields
                If field.Name.Equals("EnteredQty") Or field.Name.Equals("ArticleName") Or field.Name.Equals("ArticleCode") Then
                    table.Columns.Add(field.Name)
                End If


            Next
            For Each item In list

                Dim row As DataRow = table.NewRow()
                For Each field In fields
                    Dim p = item.GetType.GetProperty(field.Name)

                    If table.Columns.Contains(field.Name) Then
                        row(field.Name) = p.GetValue(item, Nothing)
                    End If
                Next

                table.Rows.Add(row)
            Next
            Return table
        Catch ex As Exception
            LogException(ex)
            ' MessageBox()
        End Try
    End Function

    Public Function BuildStockTakeDataTable(ByVal dt As DataTable) As String

        Dim result As New StringBuilder
        Dim widths As New List(Of Integer)
        Const ColumnSeparator As Char = "|"c
        Const HeadingUnderline As Char = "-"c

        ' determine width of each column based on widest of either column heading or values in that column
        For Each col As DataColumn In dt.Columns
            Dim colWidth As Integer = Integer.MinValue
            For Each row As DataRow In dt.Rows
                Dim len As Integer = row(col.ColumnName).ToString.Length
                If len > colWidth Then
                    colWidth = len
                End If
            Next
            widths.Add(CInt(IIf(colWidth < col.ColumnName.Length, col.ColumnName.Length + 1, colWidth + 1)))
        Next

        ' write column headers
        For Each col As DataColumn In dt.Columns
            result.Append(col.ColumnName.PadLeft(widths(col.Ordinal)))
            result.Append(ColumnSeparator)
        Next
        result.AppendLine()

        ' write heading underline
        For Each col As DataColumn In dt.Columns
            Dim horizontal As String = New String(HeadingUnderline, widths(col.Ordinal))
            result.Append(horizontal.PadLeft(widths(col.Ordinal)))
            result.Append(ColumnSeparator)
        Next
        result.AppendLine()

        ' write each row
        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                result.Append(row(col.ColumnName).ToString.PadLeft(widths(col.Ordinal)))
                result.Append(ColumnSeparator)
            Next
            result.AppendLine()
        Next

        Return result.ToString()

    End Function
    Public Function LogForStokeTake(Of t)(ByVal list As IList(Of t)) As String
        Dim message As String = String.Empty
        Try
            message = BuildStockTakeDataTable(ConvertToDataTable(list))
            Return message
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try

    End Function
    '==========================================================================================================
    Public Function SpectrumMettlerKotDtl(ByVal SMbillno As String) As DataTable
        Try
            Dim strrquery = "SELECT Billno,Scaleno  AS TerminalId,'' AS DeliveryPersonID,legacyarticlecode as ArticleCode,c_article_name as DISCRIPTION,Quantity FROM SpectrumMettlerDtl WHERE STATUS =1 AND BillNo ='" & SMbillno & "' "
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(strrquery.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try

    End Function

End Module
