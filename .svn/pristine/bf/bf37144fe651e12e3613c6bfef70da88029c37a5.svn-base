Imports System.Data.SqlClient
Imports SpectrumCommon

Public Module CLP_Data
#Region "Global Veriable's for Class"
    Private _CLPDS As DataSet
    Private _SiteCode As String
    Private _CustomerID As String
    Public _SlabPoints As Decimal
#End Region
#Region "public properties"
    Public ReadOnly Property CLPConfigdata() As DataSet
        Get
            Return _CLPDS
        End Get
        'Set(ByVal value As DataSet)
        '    _CLPDS = value
        'End Set
    End Property
    Public Property Sitecode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Public ReadOnly Property RedPoint As Decimal
        Get
            Return _SlabPoints
        End Get

    End Property
#End Region
#Region "Data Fetching Functions"
    Public Function getclpsettings() As DataSet
        'Static datatbl As DataSet
        _CLPDS = getclpdata()
        Return _CLPDS
    End Function
    Public Function getclpdata() As DataSet

        'Try

        Dim ds As New DataSet
        Dim CLPRedDetails, CLPHeader, dtClpArticleHierarchyMap, dtClpItemRedemption As New DataTable
        'Dim ds As New DataSet
        Dim da As New SqlDataAdapter("SELECT CLPPROGRAMID FROM CLPPROGRAMSITEMAP WHERE SITECODE='" & Sitecode & "' AND Status=1", ConString)
        CLPHeader = New DataTable
        da.Fill(CLPHeader)
        If CLPHeader.Rows.Count > 0 Then
            'dt = New DataTable
            da = New SqlDataAdapter("SELECT A.CLPPROGRAMID,A.ACCUMLATIONTYPE,B.TYPEVALUE,A.RedemptionType,A.MAXLIMITONACC,A.MAXACCUMULATIONLIMIT,A.MAXLIMITONRED, IsNull(A.ONLYATCREATEDSITE,0) as ONLYATCREATEDSITE,A.ACCWHENRED,A.APPLICABLEONPROMOTION,ISNULL( A.IsPointsExpiry,'0') as IsPointsExpiry, ISNULL(A.PointsExpiryDays,1) as PointsExpiryDays , ISNULL(A.IsJoiningReward,'0') as IsJoiningReward, ISNULL(A.ValueJoiningReward,0) as ValueJoiningReward, ISNULL(A.IsJoiningRewardOnEnrollment,'0') as IsJoiningRewardOnEnrollment, ISNULL(A.IsRedemptionOnEnrollment,'0') as IsRedemptionOnEnrollment, ISNULL(A.IsDayLimtOnRedemption,'0') as IsDayLimtOnRedemption, ISNULL(A.ValueDayLimtRedemption,'0') as ValueDayLimtRedemption, ISNULL(A.IsPOSPasskey,'0') as IsPOSPasskey, ISNULL(A.PosPasskeyValidity,'15') as PosPasskeyValidity,ISNULL(A.IsOnlinePasskey,'0') as IsOnlinePasskey, ISNULL(A.IsMinPointsForRedemption,'0') as IsMinPointsForRedemption, ISNULL(A.ValueMinPointsForRedemption ,'0') as ValueMinPointsForRedemption, C.RedemptionApplicable FROM MSTCLPPROGRAM A INNER JOIN MSTCLPTYPE B ON A.ACCUMLATIONTYPE=B.TYPEKEY  INNER JOIN CLPPROGRAMSITEMAP C  ON A.CLPPROGRAMID= C.CLPPROGRAMID AND C.STATUS='1'  WHERE A.CLPPROGRAMID='" & CLPHeader.Rows(0)("CLPPROGRAMID").ToString() & "' and A.Status=1 AND Getdate() between Startdate and enddate ", ConString)
            CLPHeader = New DataTable
            da.Fill(CLPHeader)
            CLPHeader.TableName = "CLPHeader"
            ds.Tables.Add(CLPHeader)

            If Not CLPHeader Is Nothing AndAlso CLPHeader.Rows.Count > 0 Then
                If CLPHeader.Rows(0)("RedemptionType") = "Rdt3" Then
                    da = New SqlDataAdapter("SELECT CLPProgramid,SRNO,CARDTYPE,slabpoints,redemptionamount FROM CLPACCUMLATIONDETAIL WHERE Status=1 and CLPPROGRAMID='" + CLPHeader.Rows(0)("CLPPROGRAMID") + "' AND ACCTYPE='" + CLPHeader.Rows(0)("RedemptionType").ToString() + "' and status='True' order by slabpoints desc ", ConString)
                    da.Fill(CLPRedDetails)
                    CLPRedDetails.TableName = "CLPRedDetails"
                    ds.Tables.Add(CLPRedDetails)
                ElseIf CLPHeader.Rows(0)("RedemptionType") = "Rdt2" OrElse CLPHeader.Rows(0)("RedemptionType") = "Rdt1" Then
                    da = New SqlDataAdapter("SELECT CLPProgramid,SRNO,CARDTYPE,points,AmtValue FROM CLPRedemptionDetail WHERE CLPPROGRAMID='" + CLPHeader.Rows(0)("CLPPROGRAMID") + "' AND RdtType='" + CLPHeader.Rows(0)("RedemptionType").ToString() + "' and status='True' ", ConString)
                    da.Fill(CLPRedDetails)
                    CLPRedDetails.TableName = "CLPRedDetails"
                    ds.Tables.Add(CLPRedDetails)
                End If
                dtClpArticleHierarchyMap = New DataTable
                da.SelectCommand.CommandText = "SELECT CLPPROGRAMID,SITECODE,NODECODE,ISTREE,ACTIVE,CREATEDON,CREATEDBY,CREATEDAT,UPDATEDON,UPDATEDBY,UPDATEDAT,STATUS FROM CLPARTICLEHIERARCHYMAP WHERE STATUS = 1 AND CLPPROGRAMID = '" + CLPHeader.Rows(0)("CLPPROGRAMID") + "' AND SITECODE = '" + Sitecode + "' AND ACTIVE = 1"
                da.Fill(dtClpArticleHierarchyMap)
                dtClpArticleHierarchyMap.TableName = "CLPARTICLEHIERARCHYMAP"
                ds.Tables.Add(dtClpArticleHierarchyMap)

                'to collect item veriable details

                If CLPHeader.Rows(0)("RedemptionType") = "Rdt1" Then
                    dtClpItemRedemption = New DataTable
                    da.SelectCommand.CommandText = "select * from ClpRedemptionItemDetail where Sitecode='" & Sitecode & "' and clpprogramid='" & CLPHeader.Rows(0)("CLPPROGRAMID") & "' and RedemptionType='" & CLPHeader.Rows(0)("RedemptionType") & "' and Status='1'"
                    da.Fill(dtClpItemRedemption)
                    dtClpItemRedemption.TableName = "ClpRedemptionItemDetail"
                    ds.Tables.Add(dtClpItemRedemption)
                    
                End If
            End If


        End If
        Return ds
        'Catch ex As Exception
        '    LogException(ex)
        'End Try
    End Function
    Public Function getCLPcustomerdata(ByVal _CustomerId As String, ByVal _ProgramID As String) As DataTable
        Dim dtCLPCustomers As New DataTable
        Dim da As New SqlDataAdapter("SELECT SiteCode,CardNo,ClpProgramId,AccountNo,CardType,CardExpiryDT,CardisActive,TotalBalancePoint,PointsAccumlated,PointsRedeemed, STATUS FROM CLPCUSTOMERS WHERE  STATUS = 1 AND CLPPROGRAMID = '" + _ProgramID + "' AND CardExpiryDT >= getdate() AND CardisActive = 'True' and cardno='" + _CustomerId + "'", SpectrumCon)
        da.Fill(dtCLPCustomers)
        dtCLPCustomers.TableName = "CLPCustomer"
        Return dtCLPCustomers
    End Function

    Public Function getrelativesCLPData(ByVal Birtlistid As String, ByVal tra As SqlTransaction) As DataSet
        Dim dtCLPCustomers As New DataTable
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter("select bls.SiteCode,bls.BirthListId,bls.FinYear,bls.CustomerId,bls.SaleInvNumber,bls.EAN,bls.ArticleCode,bls.CustomerType,bls.CostAmt,bls.ExclusiveTax,bls.TaxAmount,bls.DiscAmt,bls.IsCLP,bls.NetAmt as NetAmount ,bls.CLPPoints,bls.CLPDiscount,bls.DeliveryDate,bls.TotalDiscountAmt,bls.UPDATEDON,bls.srno,bls.FreeTexts from  birthlist BL JOIN birthlistsalesdtl BLS ON BL.SiteCode=BLS.SITECODE AND BL.BirthListId=BLS.BirthListId where  ISNULL(BLS.isclp,0)=1 and ISNULL(BLS.CLPPoints,0)=0 AND  BLS.birthlistID='" + Birtlistid + "'", SpectrumCon)
        da.SelectCommand.Transaction = tra
        '  da.Fill(dtCLPCustomers, "birthlistsalesdtl")
        da.Fill(dtCLPCustomers)


        dtCLPCustomers.TableName = "birthlistsalesdtl"
        ds.Tables.Add(dtCLPCustomers)

        'Dim da1 As New SqlDataAdapter("select * from birthlist BL JOIN birthlistsalesdtl BLS ON BL.SiteCode=BLS.SITECODE AND BL.BirthListId=BLS.BirthListId where  ISNULL(BLS.isclp,0)=1 and ISNULL(BLS.CLPPoints,0)=0 AND  BLS.birthlistID='" + Birtlistid + "'", SpectrumCon)
        '' da1.Fill(dtCLPCustomers, "birthlistsaleshdr")
        'da.SelectCommand.Transaction = tra
        'da1.Fill(dtCLPCustomers)
        'dtCLPCustomers.TableName = "birthlistsaleshdr"

        'ds.Tables.Add(dtCLPCustomers)
        Return ds
    End Function
    Public Function updaterelativedata(ByVal tran As SqlTransaction, ByVal relpointsdata As DataTable) As Boolean
        Dim comm As New clsCommon
        relpointsdata.Columns("NetAmount").ColumnName = "Netamt"
        Dim dcArray(7) As DataColumn
        dcArray(0) = relpointsdata.Columns("SiteCode")
        dcArray(1) = relpointsdata.Columns("BirthListId")
        dcArray(2) = relpointsdata.Columns("FinYear")
        dcArray(3) = relpointsdata.Columns("CustomerId")
        dcArray(4) = relpointsdata.Columns("SaleInvNumber")
        dcArray(5) = relpointsdata.Columns("EAN")
        dcArray(6) = relpointsdata.Columns("SRNO")

        relpointsdata.PrimaryKey = dcArray
        comm.SaveData(relpointsdata, SpectrumCon, tran)


        Return (True)
    End Function
    Public Function Getdetails(ByVal cardno As String) As DataRow
        Dim dtCLPCustomers As New DataTable
        Dim da As New SqlDataAdapter("select * from clpcustomers where cardno='" + cardno + "'", SpectrumCon)

        '  da.Fill(dtCLPCustomers, "birthlistsalesdtl")
        da.Fill(dtCLPCustomers)
        Return dtCLPCustomers(0)
    End Function
    Public Function getclphier(ByVal articlecode As String, ByVal sitecode As String) As DataRow
        Dim dtCLPCustomers As New DataTable
        Dim da As New SqlDataAdapter("exec SPRCLPHierarchy '" + articlecode + "','" + sitecode + "'", SpectrumCon)

        '  da.Fill(dtCLPCustomers, "birthlistsalesdtl")
        da.Fill(dtCLPCustomers)
        Return dtCLPCustomers(0)
    End Function
    Public Function getslabpoint(ByVal billno As String) As String
        Dim dtCLPCustomers As New DataTable
        Dim da As New SqlDataAdapter("select refno_3 from cashmemoreceipt where status=1 and billno='" + billno + "' and tendertypecode='CLPPoint'", SpectrumCon)

        '  da.Fill(dtCLPCustomers, "birthlistsalesdtl")
        da.Fill(dtCLPCustomers)
        Return dtCLPCustomers.Rows(0)(0).ToString()
    End Function

    ''' <summary>
    ''' This Function is responsible for generating passkey for CLP points
    ''' </summary>
    ''' <param name="PassKey"></param>
    ''' <returns>True if New record inserted</returns>
    ''' <remarks></remarks>
    Public Function GeneratePassKey(ByVal PassKey As PassKey) As Boolean
        Try
            '----- Changed By Mahesh Passkeynumber is now timestamp(timer) and passkey column field is actual key that is used for redeem and sms 
            Dim Query = "INSERT INTO [CLPPassKey] ([SiteCode],[PasskeyNumber],[PasskeyValue],[ExpiryDateTime],[DocumentNumber],[DocumentType],[DocumentDate],[IsRedeemed],[CREATEDAT],[CREATEDBY] ,[CREATEDON] ,[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS],[PassKey])" & _
                    "VALUES('" & PassKey.SiteCode & "','" & DateTime.Now.ToString("yyMMddhhmmssfff") & "', '" & PassKey.PasskeyValue & "', @ExpiryDateTime , '" & PassKey.DocumentNumber & "', '" & PassKey.DocumentType & "',@DocDate,'" & PassKey.IsRedeemed & "','" & PassKey.CreatedAt & "'," & _
                    "'" & PassKey.CreatedBy & "', GETDATE() ,'" & PassKey.UpdatedAt & "','" & PassKey.UpdatedBy & "', GETDATE() , '" & PassKey.Status & "','" & PassKey.Passkey & "' )"
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim cmd As New SqlCommand(Query, SpectrumCon())
            cmd.Parameters.Add("@ExpiryDateTime", SqlDbType.DateTime).Value = PassKey.ExpiryDateTime
            cmd.Parameters.Add("@DocDate", SqlDbType.DateTime).Value = PassKey.DocumentDate
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' This Function will return total points redeemed for a day by CLP Customer.
    ''' </summary>
    ''' <param name="ClpCustomerID"></param>
    ''' <param name="dayopendate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetdayRedemptionValue(ByVal ClpCustomerID As String, ByVal dayopendate As DateTime) As Double
        Dim Pointsredeemed = 0.0
        Dim Query = "select ISNULL(SUM(RedemptionPoints),0) dayredemption from CLPTransaction where ClpCustomerId='" & ClpCustomerID & "' and Convert(varchar(10),CreatedOn,112)='" & dayopendate.ToString("yyyyMMdd") & "'  and SiteCode='" & Sitecode & "'"
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim cmd As New SqlCommand(Query, SpectrumCon())
        Pointsredeemed = cmd.ExecuteScalar()
        Return Pointsredeemed
    End Function


    Public Function GetPassKeyDigits() As Integer
        GetPassKeyDigits = 3
        Dim Query = " Select CASE When FldValue < 3 THEN 3 WHEN FldValue > 15 THEN 15 ELSE FldValue END   FROM DEFAULTCONFIG  WHERE UPPER(FLDLABEL) = 'PASSKEY.LENGTH'"
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim cmd As New SqlCommand(Query, SpectrumCon())
        GetPassKeyDigits = cmd.ExecuteScalar()
    End Function

    Public Function getCLPcustomerByMobileNo(ByVal ClpProgramId As String, ByVal MobileNo As String) As String
        getCLPcustomerByMobileNo = ""
        Dim Query = "SELECT  top(1) CardNo  FROM    CLPCUSTOMERS WHERE ClpProgramId ='" & ClpProgramId & "' and Mobileno ='" & MobileNo & "'  "
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim cmd As New SqlCommand(Query, SpectrumCon())
        getCLPcustomerByMobileNo = cmd.ExecuteScalar()
    End Function



    ''' <summary>
    ''' Returns PassKey data if exist
    ''' </summary>
    ''' <param name="passkey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPassKeydata(ByVal passkey As String) As SpectrumCommon.PassKey
        Dim passkeyVal As PassKey
        'Dim Query = "select * from CLPPassKey where PasskeyNumber='" & passkey & "' and IsRedeemed='0'"
        Dim Query = "select * from CLPPassKey where Passkey='" & passkey & "'" '--- and IsRedeemed='0'"
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim cmd As New SqlCommand(Query, SpectrumCon())
        Dim rdr = cmd.ExecuteReader()
        While rdr.Read()
            passkeyVal = New PassKey With {.ExpiryDateTime = CDate(rdr("ExpiryDateTime").ToString()), .IsRedeemed = CBool(rdr("IsRedeemed").ToString()), .PasskeyValue = CDbl(rdr("PasskeyValue").ToString()), .Passkey = passkey}

        End While
        rdr.Close()
        Return passkeyVal
    End Function
    ''' <summary>
    ''' Redeemes Pass key entered by user
    ''' </summary>
    ''' <param name="passkey"></param>
    ''' <param name="BillNO"></param>
    ''' <param name="Billdate"></param>
    ''' <param name="Trans"></param>
    ''' <remarks></remarks>
    Public Sub RedeemPassKey(ByVal passkey As String, ByVal BillNO As String, ByVal Billdate As Date, ByVal RedPoints As Double, ByVal Trans As SqlTransaction, ByVal CLPProgramID As String, ByVal CLPCustomerID As String)
        Dim Query = "update CLPPassKey set IsRedeemed='1', DocumentNumber='" & BillNO & "', DocumentDate=@Billdate, DocumentType='CMS' where Passkey='" & passkey & "' and IsRedeemed='0'"
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim cmd As New SqlCommand(Query, SpectrumCon(), Trans)
        cmd.Parameters.Add("@Billdate", SqlDbType.Date)
        cmd.Parameters("@Billdate").Value = Billdate
        If cmd.ExecuteNonQuery() > 0 Then
        End If

    End Sub

    Public Function GetCLPTiers(ByVal CLPProgramID As String) As IList(Of String)
        Dim tierlist As New List(Of String)
        Dim Sqlstr = "select Top(1) CardType from CLPACCUMLATIONDETAIL where ClpProgramid='" & CLPProgramID & "' and STATUS='1' order by Sequence"
        If SpectrumCon().State <> ConnectionState.Open Then
            SpectrumCon().Open()
        End If
        Dim sqlcmd As New SqlCommand(Sqlstr, SpectrumCon())
        Dim rdr = sqlcmd.ExecuteReader()
        While rdr.Read()
            tierlist.Add(rdr("CardType"))
        End While
        rdr.Close()
        Return tierlist
    End Function
#End Region
End Module
