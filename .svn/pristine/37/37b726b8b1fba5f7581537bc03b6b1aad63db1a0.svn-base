Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class clsHotelReservation
#Region "Global Variables"
    Dim vStmtQry As New System.Text.StringBuilder
    Dim daScan As New SqlDataAdapter
    Dim dtScan As DataTable
    Dim dsScan As DataSet

    Dim dtAccomodation As DataTable


    Dim dtTEMP As New DataTable

#End Region

#Region "Common methods"

    'added by khusrao adil on 21-04-2017 for only validate numric value
    Public Function ValidateNumberic(ByVal value As String) As Boolean
        Dim pattern As String = "^[0-9 ]+$"
        Dim test = Regex.Match(value, pattern)
        Return test.Success
    End Function
    'added by khusrao adil on 21-04-2017 for validate  email
    Public Function ValidateEmail(ByVal emailaddress As String) As Boolean
        Dim pattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim test = Regex.Match(emailaddress, pattern)
        Return test.Success
    End Function
    'added by khusrao adil on 21-04-2017 for validate for only alphabet
    Public Function ValidateAlphabet(ByVal value As String) As Boolean
        Dim pattern As String = "^[a-zA-Z ]+$" '"\^[a-zA-Z\s]+$"
        Dim test = Regex.Match(value, pattern)
        Return test.Success
    End Function
    'private static bool IsNumeric(string value)
    '   {
    '       Regex rex = new Regex(@"^(([0-9]*)|(([0-9]*).([0-9]*)))$");
    '       //return value.Trim().length > 0 && value.Trim().Ism(/^[0-9]*(\.[0-9]+)?$/)
    '       return rex.IsMatch(value);
    '   }
    Public Enum enumStatus
        ACTIVE
        INACTIVE
        OPEN
        CHECKED_IN
        RESERVED
        BLOCKED
        CHECKED_OUT
        CANCELLED
    End Enum
    Public Enum enumStatusTypes
        RECORD_STATUS
        ROOM_STATUS
        RESERVATION_STATUS
    End Enum
    Public Enum enumRecordStatus
        Inserted
        Updated
    End Enum
    'added by khusrao adil 
    Public Function ImageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Try
            If (imageIn IsNot Nothing) Then
                Dim ms As New MemoryStream()
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Return ms.ToArray()
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 17-02-2017
    Public Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Try
            If Not (byteArrayIn Is Nothing) Then
                Dim returnImage As Image
                If byteArrayIn.Length <> 0 Then
                    Dim ms As New MemoryStream(byteArrayIn)
                    returnImage = Image.FromStream(ms)
                Else
                    Return Nothing
                End If
                Return returnImage
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    'added by khusrao adil on 20-01-2017 for promotions
    Public Function GetRoomTypeWisePromotions(ByVal sitecode As String, ByVal checkInDate As Date, ByVal currentYear As String, ByVal roomTypeList As String) As DataTable
        Dim cmd As New SqlCommand("Host_SP_PromotionDetails", SpectrumCon)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 0
        cmd.Parameters.AddWithValue("@SiteCode", sitecode)
        cmd.Parameters.AddWithValue("@CheckInDate", checkInDate)
        cmd.Parameters.AddWithValue("@Year", currentYear)
        cmd.Parameters.AddWithValue("@RoomTypeId", roomTypeList)
        daScan = New SqlClient.SqlDataAdapter(cmd)
        dtScan = New DataTable
        daScan.Fill(dtScan)
        Return dtScan
    End Function
    'added by khusrao adil on 20-02-2017 for room type promotion
    Public Function GetPromotionByRoomTypeId(ByVal _RoomTypId As String, Optional ByVal allValue As Boolean = False) As DataTable
        Try
            Dim qry As String = ""
            If allValue = True Then
                qry = "select * from Host_mstPromotion where mstSiteRoomTypeMap='" + _RoomTypId + "'"
            Else
                qry = "select mstPromotionID,promotionName from Host_mstPromotion where mstSiteRoomTypeMap='" + _RoomTypId + "'"
            End If
            Dim cmd As New SqlCommand(qry, SpectrumCon)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            daScan = New SqlClient.SqlDataAdapter(cmd)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by nikhil
    Public Function GetAllServices(ByVal ReservationId As String, ByVal SiteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select ROW_NUMBER() Over (Order by servicesID) As [SrNo] , ArticleCode ,ArticleName As ServiceName ,1 as Quantity,cost as Cost,IsPaid  from Host_services where reservationID='" & ReservationId & "'" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by nikhil
    ' updated by khusrao adil on 20-02-2017 two  new optional param added 
    Public Function GetRoomType(sitecode As String, Optional _roomTypeIdList As String = "", Optional ByVal allValue As Boolean = False)
        Try
            vStmtQry.Length = 0
            If allValue = True Then
                vStmtQry.Append("SELECt A.mstRoomTypeId,A.Name  from Host_MstRoomType A" & vbCrLf)
                vStmtQry.Append("INNER JOIN Host_MstSiteRoomTypeMap  B ON A.mstRoomTypeId= B.mstRoomTypeId AND B.mstStatusId=1" & vbCrLf)
                vStmtQry.Append("WHERE A.mstStatusId=1 AND SITECODE = '" & sitecode & "' " & vbCrLf)
                If _roomTypeIdList <> "" Then
                    vStmtQry.Append(" and A.mstRoomTypeID in (" & _roomTypeIdList & ") " & vbCrLf)
                End If
            Else
                vStmtQry.Append("SELECt A.mstRoomTypeId,A.Name from Host_MstRoomType A" & vbCrLf)
                vStmtQry.Append("INNER JOIN Host_MstSiteRoomTypeMap  B ON A.mstRoomTypeId= B.mstRoomTypeId AND B.mstStatusId=1" & vbCrLf)
                vStmtQry.Append("WHERE A.mstStatusId=1 AND SITECODE = '" & sitecode & "' " & vbCrLf)
            End If
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            dt.Columns.Add("mstRoomTypeId", GetType(Integer))
            dt.Columns.Add("Name", GetType(String))
            dt.Rows.Add(0, "ALL")
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by nikhil
    Public Function GetBedType(sitecode As String)
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("SELECT mstBedTypeId,Name  from  Host_MstBedType  WHERE mstStatusId= 1 " & vbCrLf)
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            dt.Columns.Add("mstBedTypeId", GetType(Integer))
            dt.Columns.Add("Name", GetType(String))
            dt.Rows.Add(0, "ALL")
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''added by nikhil
    Public Function GetReservationGuest(ByVal CardNo As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select reservationID, guestMobileNumber,isPrimaryGuest,CardNo,ClpProgramId from Host_ReservationGuestDetail where CardNo='" & CardNo & "' And isPrimaryGuest=1 " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by nikhil
    Public Function GetNoOfBedType(sitecode As String)
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("SELECt Distinct A.noOfBeds as noOfBeds,A.noOfBeds noOfBeds1 from Host_MstRoom A  INNER JOIN  " & vbCrLf)
            vStmtQry.Append("Host_MstSiteRoomTypeMap B ON A. mstSiteRoomTypeMap = B.mstSiteRoomTypeMap" & vbCrLf)
            vStmtQry.Append("WHERE  B.SITECODE ='" & sitecode & "' AND A.mstStatusId= 1  " & vbCrLf)
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by nikhil
    Public Function getStatusType(ByVal statusType As String, Optional statusName As String = "")
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select mstStatusID,name from Host_MstStatus  where StatusType='" & statusType & "'" & vbCrLf)
            If statusName <> "" Then
                vStmtQry.Append("and name='" & statusName & "'" & vbCrLf)
            End If
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function SaveServices(ByVal dtSaveServices As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveServices = False


            For Each drSaveSaveService In dtSaveServices.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                SqlQuery.Append("insert into Host_Services (servicesID ,reservationID ,ArticleCode ,ArticleName ,cost ,remarks ,createdAt ,createdBy," & vbCrLf)
                SqlQuery.Append("createdOn ,updatedAt ,updatedBy ,updatedOn ,mstStatusID,IsPaid)" & vbCrLf)
                SqlQuery.Append("values(" & drSaveSaveService("servicesID") & "," & drSaveSaveService("reservationID") & "," & vbCrLf)

                SqlQuery.Append("'" & drSaveSaveService("ArticleCode") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveSaveService("ArticleName") & "','" & drSaveSaveService("cost") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveSaveService("remarks") & "','" & drSaveSaveService("createdAt") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveSaveService("createdBy") & "'," & vbCrLf)
                SqlQuery.Append("GETDATE()," & vbCrLf)
                ' SqlQuery.Append("'" & drSaveSaveService("createdBy") & "','" & drSaveSaveService("createdOn") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveSaveService("updatedAt") & "','" & drSaveSaveService("updatedBy") & "'," & vbCrLf)
                SqlQuery.Append("GETDATE(),'" & drSaveSaveService("mstStatusID") & "','" & drSaveSaveService("IsPaid") & "')" & vbCrLf)
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)

                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveServices = True
                Else
                    Return False
                End If
            Next
            Return True


        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    '' added by nikhil
    Public Function GetViewReservationDetails(ByVal sitecode As String, ByVal Status As String, Optional ByVal ReservationId As String = "", Optional ByVal MobileNo As String = "", Optional ByVal GuestName As String = Nothing) As DataTable
        Try
            Dim dtViewDetails As New DataTable
            dtViewDetails.Clear()
            Dim sqlComm As SqlCommand = New SqlCommand("Sp_HostViewReservation", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@MobileNumber", MobileNo)
            sqlComm.Parameters.AddWithValue("@ReservationID", ReservationId)
            sqlComm.Parameters.AddWithValue("@CustName", GuestName)
            sqlComm.Parameters.AddWithValue("@ReservationStatusId", Status)

            sqlComm.CommandType = CommandType.StoredProcedure
            dtViewDetails = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter(sqlComm)
            da.Fill(dtViewDetails)
            Return dtViewDetails
            SpectrumCon.Close()

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetServiceType(sitecode As String)
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select ArticleName,ArticleCode,cost from host_services" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 09-02-2017 for get status id for all scena
    Public Function GetStatusTypeId(ByVal statusType As String, Optional statusName As String = "") As Integer
        GetStatusTypeId = 0
        Dim dt = getStatusType(statusType, statusName)
        If dt.Rows.Count > 0 Then
            GetStatusTypeId = dt.Rows(0)("mstStatusID").ToString
        End If
        Return GetStatusTypeId
    End Function
    ''added by nikhil
    Public Function GetReservedRoomAvailable(ByVal ReservationId As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select * from Host_Reservation where ReservationID='" & ReservationId & "'" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''added by nikhil 
    Public Function GetAccomodationDetails(ByVal ReservationId As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select totalGrossAmount As Price from Host_Reservation  where reservationID ='" & ReservationId & "'")
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''added by nikhil
    Public Function GetFoodPaymentCharges(ByVal reservationId As String) As DataTable
        Try
            Dim dtFoodDetails As New DataTable
            dtFoodDetails.Clear()
            Dim sqlComm As SqlCommand = New SqlCommand("Host_SP_ViewFoodPaymentDetails", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@ReservationId", reservationId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dtFoodDetails = New DataTable
            Dim da As SqlDataAdapter = New SqlDataAdapter(sqlComm)
            da.Fill(dtFoodDetails)
            Return dtFoodDetails
            SpectrumCon.Close()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''added by nikhil
    Public Function UpdateReservationDetails(ByVal Sitecode As String, ByVal reservationid As String, ByVal ReservationStatusId As String, ByVal updatedBy As String, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateReservationDetails = False
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim Rstatus As String = ReservationStatusId
            Dim RId As Integer = Convert.ToInt32(reservationid)
            ' Dim Query As String
            Dim Query As New StringBuilder
            Query.Length = 0
            Query.Append("Update Host_Reservation set reservationStatusID= " & Rstatus & ",UPDATEDON=getdate(),UPDATEDAT='" & Sitecode & "', UPDATEDBY='" & updatedBy & "'" & vbCrLf)
            Query.Append("where ReservationId =" & RId & " And SiteCode='" & Sitecode & "'")
            Dim cmdTrn As New SqlCommand(Query.ToString(), SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            Dim a = cmdTrn.ExecuteNonQuery()
            If a > 0 Then
                tran.Commit()
                CloseConnection()
                UpdateReservationDetails = True
            Else
                tran.Rollback()
                CloseConnection()
                UpdateReservationDetails = False
            End If
            Return UpdateReservationDetails
        Catch ex As Exception
            LogException(ex)
            CloseConnection()
            Return False
        End Try
    End Function
    'code is added by irfan on 10/4/2018 for host_reservation.
    Public Function updatebalamt(ByVal RId As Int32, ByVal Sitecode As String, ByVal remianingamt As Double, ByVal paidAmt As Double) As Boolean
        Try

            Dim Query As New StringBuilder
            Query.Length = 0

            Query.Append("Update Host_Reservation set totalAmountPaid= " & paidAmt & ",remainingAmountToPay='" & remianingamt & "'" & vbCrLf)
            Query.Append("where ReservationId =" & RId & " And SiteCode='" & Sitecode & "'")
            Dim cmdTrn As New SqlCommand(Query.ToString(), SpectrumCon)

            OpenConnection()
            Dim a = cmdTrn.ExecuteNonQuery()
            If a > 0 Then
                Return True
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''added by nikhil
    Public Function SaveServiceAllData(ByVal dsMainHost As DataSet, Optional ByVal SaveServiceWithoutPayment As Boolean = False) As Boolean
        Dim objcomm As New clsCommon
        Dim tran As SqlTransaction = Nothing
        OpenConnection()
        tran = SpectrumCon.BeginTransaction()

        If SaveServices(dsMainHost.Tables("Host_Services"), tran) = False Then
            tran.Rollback()
            CloseConnection()
            Return False
        Else
            tran.Commit()
            CloseConnection()
            Return True
        End If
    End Function
    ''added by nikhil
    '_remainingAmountToPay => while service saving pass as zero or dont pass any value
    Public Function UpdateReservationForServices(ByVal sitecode As String, ByVal ServiceCharge As String, ByVal reservationId As String, ByVal updatedBy As String, ByRef tran As SqlTransaction, Optional ByVal TotalAmtPaid As String = "0", Optional _remainingAmountToPay As String = "0") As Boolean
        Try
            If String.IsNullOrEmpty(ServiceCharge) Then
                ServiceCharge = 0
            End If
            If String.IsNullOrEmpty(TotalAmtPaid) Then
                TotalAmtPaid = 0
            End If
            UpdateReservationForServices = False
            Dim query As New StringBuilder
            query.Append("Update Host_Reservation Set totalServicesCharges='" & ServiceCharge & "',totalAmountPaid='" & TotalAmtPaid & "', UPDATEDON=getdate(),UPDATEDAT='" & sitecode & "', UPDATEDBY='" & updatedBy & "',remainingAmountToPay='" + _remainingAmountToPay + "'" & vbCrLf)
            'If _remainingAmountToPay <> "0" Then
            '    query.Append("remainingAmountToPay='" + _remainingAmountToPay + "'", vbCrLf)
            'End If
            query.Append("where ReservationId='" & reservationId & "' And SiteCode='" & sitecode & "'")
            Dim cmd As New SqlCommand(query.ToString(), SpectrumCon)
            cmd.Transaction = tran
            OpenConnection()
            cmd.ExecuteNonQuery()
            UpdateReservationForServices = True
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'added by khusrao adil on 2-02-2017 for room count ,room type wise
    Public Function GetDocumentType(sitecode As String)
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select mstSupportedDocumentTypeID as mstDocumentId ,name as DocumentName from Host_MstSupportedDocumentType where mstStatusID=1" & vbCrLf)
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by nikhil
    Public Function GetReservationRooms(ByVal siteCode As String, ByVal checkIndate As DateTime, ByVal chkYear As String, ByVal Roomtype As String, ByVal bedType As String, Optional ByVal NoOfBed As String = "", Optional ByVal chkoutDate As DateTime = Nothing, Optional ReservationId As String = "0") As DataTable
        Try
            ' Dim chkIn As Date = checkIndate.ToString("yyyy-MM-dd")
            Dim chkOut As Date = chkoutDate.ToString("yyyy-MM-dd")
            Dim dtRoom As New DataTable
            dtRoom = Nothing
            Dim sqlComm As SqlCommand = New SqlCommand("Host_SP_SearchReservation", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", siteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@CheckIn", checkIndate)
            sqlComm.Parameters.AddWithValue("@Year", chkYear)

            sqlComm.Parameters.AddWithValue("@RoomTypeId", Roomtype)

            sqlComm.Parameters.AddWithValue("@BedTypeId", bedType)
            sqlComm.Parameters.AddWithValue("@CheckOut", chkOut)
            sqlComm.Parameters.AddWithValue("@ReservationId", ReservationId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dtRoom = New DataTable()
            Dim da As SqlDataAdapter = New SqlDataAdapter(sqlComm)
            da.Fill(dtRoom)
            Return dtRoom
            SpectrumCon.Close()

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    'added by khusrao adil on 2-02-2017 for room count ,room type wise
    Public Function GetRoomTypeWiseRoomCount() As DataTable
        Dim cmd As New SqlCommand("select '' as RoomNumberList,'0' as RoomCount,'0' as PromotionCount,mstRoomTypeID, name from Host_MstRoomType order by mstRoomTypeID", SpectrumCon)
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 0
        daScan = New SqlClient.SqlDataAdapter(cmd)
        dtScan = New DataTable
        daScan.Fill(dtScan)
        Return dtScan
    End Function

    'added by khusrao adil  on 02-02-2017 for get Max id of table
    Public Function GetMaxId(ByVal tableName, ByVal columnName) As Integer
        GetMaxId = 0
        Dim cmd As New SqlCommand("SELECT Isnull(max(" + columnName + "),0)+1 as maxId FROM " + tableName + "  ", SpectrumCon)
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 0
        daScan = New SqlClient.SqlDataAdapter(cmd)
        dtScan = New DataTable
        daScan.Fill(dtScan)
        GetMaxId = Convert.ToInt32(dtScan.Rows(0)("maxId"))
        Return GetMaxId
    End Function
    'added by khusrao adil  on 02-02-2017 for Bill Structure for saving (insert/update)
    Public Function HOSTGetStruc(Optional ByVal ReservationId As Integer = 0) As DataSet
        Try
            Dim ds As New DataSet
            Dim sqlComm As SqlCommand = New SqlCommand()
            sqlComm.CommandText = "HOST_SP_STRUC"
            sqlComm.CommandType = CommandType.StoredProcedure
            ', SpectrumCon
            sqlComm.Connection = SpectrumCon()
            'daScan = New SqlDataAdapter(" EXEC HOSt_SP_STRUC ", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@ReservationId", ReservationId)
            Using daScan As New SqlDataAdapter(sqlComm)
                daScan.Fill(ds)
            End Using
            '    daScan = New SqlDataAdapter(sqlComm)

            'da.Fill(ds)
            ds.Tables(0).TableName = "Host_Reservation"
            ds.Tables(1).TableName = "Host_ReservationRoomMap"
            ds.Tables(2).TableName = "Host_ReservationTaxMap"
            ds.Tables(3).TableName = "Host_ReservationRoomTypePromotionMap"
            ds.Tables(4).TableName = "Host_ReservationGuestDetail"
            ds.Tables(5).TableName = "Host_ReservationReceipt"
            ds.Tables(6).TableName = "Host_Services"
            ds.Tables(7).TableName = "clpcustomers"
            'ds.Tables(7).TableName = "Host_ReservationCashMemoHdrMap"
            SpectrumCon.Close()
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 09-02-2017 for hotel taxes
    Public Function GetHotelTax(ByVal sitecode As String, ByVal documentType As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select  ROW_NUMBER() over (ORDER BY MT.TaxCode) as TaxLineNo, MT.TaxCode,MT.TaxName,MT.Value,TSD.TaxValue as Tax,TSD.SiteCode ,TSD.DocumentType as DocumentType  from MstTax MT inner join TaxSiteDocMap TSD on MT.TaxCode=TSD.TaxCode" & vbCrLf)
            vStmtQry.Append("where TSD.SiteCode='" & sitecode & "' and TSD.DocumentType='" & documentType & "' and MT.STATUS=1" & vbCrLf)
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
    'added by nikhil
    Public Function GetDetailsForCreateReservation() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(bit,'False') as Selects," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as RoomNo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as RoomType," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as Ameneties," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as Cost," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as Tax," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as Bed" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 09-02-2017 for ReservationTaxMap  table structrue
    Public Function GetDetailsForReservationTaxMap(Optional ByVal _reservationTaxMapId As String = "", Optional ByVal _reservationId As String = "") As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(bit,'False') as Selects," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as reservationTaxMapID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as reservationID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as SiteCode," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as TaxLineNo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as DocumentType," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as TaxCode," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as TaxValueInPercent," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as TaxValueInAmount," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'Inserted') as RecordStatus" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'added by khusrao adil on 10-02-2017 for reservation promotions applied details schema
    Public Function GetDetailsForReservationPromotionMap() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(bit,'False') as Selects," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as reservationRoomTypePromotionMapID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as reservationID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as reservationRoomMapID," & vbCrLf) 'added on 27-03-2017
            vStmtQry.Append("Convert(Varchar(30),'') as PromotionLineNo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as mstSiteRoomTypeMap," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as rateDate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as yearInyyyy," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as weekDay," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as mstPromotionID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as mstPromotionName," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as rate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as discountInPercent," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as discountAmount," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as RoomTypeId," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as CostBeforePromo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as CostAfterPromo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as description," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'Inserted') as RecordStatus" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'added by khusrao adil on 21-02-2017 for promotion
    Public Function GetSelectedPromoRoomTypeWiseSchema() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(bit,'False') as Selected," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as mstRoomTypeId," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as mstPromotionId," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as rate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as DiscountInPercent," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as CostAfterPromo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as FinaliseCostAfterPromo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'Inserted') as RecordStatus" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'added by khusrao adil on 13-02-2017 for reservation receipt schema
    Public Function GetDetailsForReservationReceiptSchema() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(bit,'False') as Selects," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as TenderHeadCode," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as receiptLineNo," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'')as CardNo," & vbCrLf) ' as  Card No as credit card number
            vStmtQry.Append("Convert(Varchar(30),'') as ExchangeRate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as TenderTypeCode," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as AmountTendered," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as CurrencyCode," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as AmountinCurrency," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as billReceiptDate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as billReceiptTime," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(50),'') as StaffID," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as ManagersKeytoUpdate," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as ChangeLine," & vbCrLf)
            vStmtQry.Append("Convert(Varchar(30),'') as BankAccNo" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'added by khusrao adil on 14-03-2017 for promotion total discount schema
    Public Function GetPromotionTotalDisountSchema()
        Dim VstmQry As New StringBuilder
        VstmQry.Length = 0
        VstmQry.Append("SELECT Convert(Varchar(20),'') As RoomTypeId," & vbCrLf)
        VstmQry.Append(" Convert(decimal,0) as TotalPromotionCost" & vbCrLf)
        daScan = New SqlClient.SqlDataAdapter(VstmQry.ToString, SpectrumCon)
        dtScan = New DataTable
        daScan.Fill(dtScan)
        Return dtScan
    End Function
    'added by khsurao adil for SiteRoomMap id
    Public Function GetMstSiteRoomTypeMap(ByVal _mapId As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" select * from Host_MstSiteRoomTypeMap where mstSiteRoomTypeMap='" & _mapId & "'" & vbCrLf)
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function GetDetailsForGuest() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            'vStmtQry.Append("Select convert(varchar(50),'') as Del," & vbCrLf)
            vStmtQry.Append(" select Convert(Varchar(100),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as reservationGuestDetailID," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as FirstName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as MiddleName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as LastName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as MobileNumber," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as EmailId," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Age," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Gender," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DocumentType," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DocumentTypeId," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'false') as PrimaryGuest," & vbCrLf) '
            vStmtQry.Append(" Convert(image,null) as GuestImage," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CardNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ClpProgramId," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'Inserted') as RecordStatus," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as supportedDocumentDetails" & vbCrLf)
          
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
    'added by nikhil
    Public Function GetDetailsForSummaryTab() As DataTable
        Try
            Dim VstmQry As New StringBuilder
            VstmQry.Length = 0
            VstmQry.Append("SELECT Convert(Varchar(20),'') As RoomNo," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As reservationRoomMapID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As reservationID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As MstRoomId," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As RoomType," & vbCrLf)
            VstmQry.Append("Convert(Varchar(100),'')As Ameneties," & vbCrLf)
            VstmQry.Append("Convert(Varchar(100),'')As Promotions," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As Price," & vbCrLf)
            VstmQry.Append("Convert(varchar(50),'')As Tax," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As Discount," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As FinalCost," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As Selected" & vbCrLf) ' added by khusrao adil on 13-02-2017 for selected promotions
            Dim daSummary As New SqlDataAdapter
            daSummary = New SqlClient.SqlDataAdapter(VstmQry.ToString, SpectrumCon)
            Dim dtSummary As New DataTable
            daSummary.Fill(dtSummary)
            Return dtSummary
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ' added by khusrao adil on 06-02-2017 for three tab schema for saving
    Public Function GetReservationTabSaveSchema()
        Try
            Dim VstmQry As New StringBuilder
            VstmQry.Length = 0
            VstmQry.Append("SELECT Convert(Varchar(20),'') As reservationID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As reservationStatusID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As mstSupportedDocumentTypeID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As TerminalID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As siteCode," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As yearInyyyy," & vbCrLf)
            VstmQry.Append("Convert(Varchar(100),'')As reservationNumber," & vbCrLf)
            VstmQry.Append("Convert(Varchar(100),'')As reservationDateTime," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalPromotionDiscountAmount," & vbCrLf)
            VstmQry.Append("Convert(varchar(50),'')As totalGrossAmount," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalAmountPaid," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As remainingAmountToPay," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalAccommodationCharges," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalServicesCharges," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalFoodCharges," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As primaryGuestDocumentNumber," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As primaryGuestDocumentDetails," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As primaryGuestDocumentImage," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalNoOfAdults," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As totalNoOfChildren," & vbCrLf)

            VstmQry.Append("Convert(Varchar(50),'')As reservationRoomTaxMapID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As reservationRoomMapID," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As DocumentType," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As TaxCode," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As TaxType," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As TaxValueInPercent," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As TaxValueInAmount," & vbCrLf)
            VstmQry.Append("Convert(Varchar(50),'')As description" & vbCrLf)


            daScan = New SqlClient.SqlDataAdapter(VstmQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMembershipTableStructure(ByVal Sitecode As String) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select * from HotelReservation Where SiteCode='" & Sitecode & "' " & vbCrLf)

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan)
            dsScan.Tables(0).TableName = "HotelReservation"

            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetReservationListForms(checkindt As DateTime, chkoutDate As DateTime, Optional ByVal RoomType As String = "", Optional ByVal Bed As String = Nothing, Optional ByVal NoOfPeople As String = Nothing) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Bit,'') as Sel,SI.SaleInvNumber as InvoiceNo,SI.SOInvDate AS InvoiceDate,SI.UPDATEDON  AS UpdatedOn," & vbCrLf)
            vStmtQry.Append(" CUST.NameOnCard AS CustomerName,SUM(SI.AmountTendered) AS Amount,CUSTDET.Description" & vbCrLf)
            vStmtQry.Append(",SOH.CustomerNo from HotelReservation SI  " & vbCrLf)
            daScan = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function




    ''added by nikhil

    Public Function GetCheckoutPaymentGuestDetails(ByVal ReservationId As String, Optional ByVal MobileNo As String = Nothing) As DataTable
        Try
            Dim dtGuestDetails As New DataTable
            dtGuestDetails.Clear()
            Dim sqlComm As SqlCommand = New SqlCommand("Host_SP_ViewReservationGuestDetails", SpectrumCon)
            ' sqlComm.Parameters.AddWithValue("@SiteCode", sitecode)
            sqlComm.Parameters.AddWithValue("@ReservationId", ReservationId)
            'sqlComm.Parameters.AddWithValue("@MobileNo", MobileNo)
            sqlComm.CommandType = CommandType.StoredProcedure
            dtGuestDetails = New DataTable
            Dim da As SqlDataAdapter = New SqlDataAdapter(sqlComm)
            da.Fill(dtGuestDetails)
            Return dtGuestDetails
            SpectrumCon.Close()

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetAccomodationSchema() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as Del," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False')as Sel," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'')  as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Particular, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Price " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtAccomodation = New DataTable
            daScan.Fill(dtAccomodation)
            Return dtAccomodation
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''added by nikhil
    Public Function GetServicesSchema() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as Del," & vbCrLf)
            ' vStmtQry.Append(" Convert(bit,'False')as Sel," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'')  as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Particular, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Description, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Cost " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''added by nikhil
    Public Function GetServiceDetails() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            'vStmtQry.Append("Select convert(varchar(50),'') as Del," & vbCrLf)
            vStmtQry.Append(" select Convert(Varchar(100),'') as SrNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as serviceID," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as reservationID," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ServiceName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as MobileNumber," & vbCrLf)
            vStmtQry.Append(" Convert(decimal,0) as Cost," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Quantity" & vbCrLf)


            Dim daServie As New SqlDataAdapter
            daServie = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtService As New DataTable
            daServie.Fill(dtService)

            Return dtService
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''added by nikhil
    Public Function GetClosedGuestDetails(ByVal siteCode As String, ByVal ReservationId As String) As DataTable
        Try
            Dim cmd As New SqlCommand("SP_ClosedViewReservation", SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@ReservationId", ReservationId)
            daScan = New SqlClient.SqlDataAdapter(cmd)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing

        End Try

    End Function

    'added by khusrao adil on 27-03-2017 to get selected promotions of reservation
    Public Function GetSelectedPromotionByReservationId(ByVal ReservationID As String) As DataTable
        Try
            vStmtQry.Length = 0
            'vStmtQry.Append("select  * from Host_ReservationRoomMap rmp inner join Host_ReservationRoomTypePromotionMap rmtp on rmp.reservationID=rmtp.reservationID " & vbCrLf)
            vStmtQry.Append("select distinct rmp.reservationRoomMapID, rmtp.reservationID,MS.mstRoomTypeID,rmtp.mstSiteRoomTypeMap," & vbCrLf)
            vStmtQry.Append("rmtp.reservationRoomTypePromotionMapID,rmtp.mstPromotionID,rmtp.promotionName,rmtp.discountAmount," & vbCrLf)
            vStmtQry.Append("rmtp.discountInPercent,rmtp.rateDate,rmtp.weekDay,rmtp.yearInyyyy,rmtp.mstStatusID,rmtp.applicableWithOtherPromotions," & vbCrLf)
            vStmtQry.Append("rmtp.description,rmp.mstStandardRoomRateID,rmp.rate,Ms.name from Host_ReservationRoomMap rmp  " & vbCrLf)
            vStmtQry.Append("left outer join Host_ReservationRoomTypePromotionMap rmtp on rmp.reservationID=rmtp.reservationID" & vbCrLf)
            vStmtQry.Append("left outer join Host_MstSiteRoomTypeMap MS on rmtp.mstSiteRoomTypeMap=ms.mstSiteRoomTypeMap" & vbCrLf)
            'vStmtQry.Append("select  * from Host_ReservationRoomMap rmp inner join Host_ReservationRoomTypePromotionMap rmtp on rmp.reservationID=rmtp.reservationID " & vbCrLf)
            vStmtQry.Append("where rmtp.reservationID='" & ReservationID & "' and rmtp.mstStatusID=1 and rmp.mstStatusID=1 order by mstRoomTypeID asc " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil for finding room type id on 29-03-2017
    Public Function GetRoomTypeBySiteRoomTypeMap(ByVal mstSiteRoomTypeMap As String) As DataTable
        Try
            vStmtQry.Length = 0
            'vStmtQry.Append("select  * from Host_ReservationRoomMap rmp inner join Host_ReservationRoomTypePromotionMap rmtp on rmp.reservationID=rmtp.reservationID " & vbCrLf)
            vStmtQry.Append("select m.mstRoomTypeID,mr.name,mr.mstRoomTypeID from Host_MstSiteRoomTypeMap m " & vbCrLf)
            vStmtQry.Append("inner join Host_MstRoomType mr on m.mstRoomTypeID=mr.mstRoomTypeID " & vbCrLf)
            vStmtQry.Append("where m.mstSiteRoomTypeMap='" & mstSiteRoomTypeMap & "' and  m.mstStatusID=1 and mr.mstStatusID=1 " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 06-02-2017 for getting primary guest details
    Public Function GetClpCustomer(ByVal MobileNumber As String, Optional ByVal isCustomerExistCheck As Boolean = False) As DataTable
        Try
            vStmtQry.Length = 0
            If isCustomerExistCheck = True Then
                vStmtQry.Append(" select * from Host_ReservationGuestDetail where  mstStatusID=1 and guestMobileNumber='" & MobileNumber & "'" & vbCrLf)
            Else
                vStmtQry.Append(" Select CardNo,ClpProgramId,CardType,Mobileno from CLPCustomers where Mobileno='" & MobileNumber & "' and STATUS=1" & vbCrLf)
            End If
            daScan = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function SaveReservationAllData(ByVal dsMainHost As DataSet, Optional saveWioutPayement As Boolean = False) As Boolean
        Try
            Dim objcomm As New clsCommon
            Dim tran As SqlTransaction = Nothing
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            'If objcomm.SaveData(dsMainHost, SpectrumCon, tran) Then
            '    tran.Rollback()
            '    CloseConnection()
            '    Return False
            'ElseIf SaveGuestDetails(dsMainHost.Tables("Host_ReservationGuestDetail"), tran) Then
            '    If objcomm.UpdateDocumentNo("RESERVATION NUMBER", SpectrumCon, tran) = False Then
            '        tran.Rollback()
            '        CloseConnection()
            '        Return False
            '    End If
            '    tran.Commit()
            '    CloseConnection()
            '    Return True
            'Else
            '    Return False
            'End If
            If SaveReservation(dsMainHost.Tables("Host_Reservation"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveReservationRoomMap(dsMainHost.Tables("Host_ReservationRoomMap"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveReservationRoomTaxMap(dsMainHost.Tables("Host_ReservationTaxMap"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveReservationRoomTypePromotionMap(dsMainHost.Tables("Host_ReservationRoomTypePromotionMap"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveCustomersDetails(dsMainHost.Tables("clpcustomers"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveGuestDetails(dsMainHost.Tables("Host_ReservationGuestDetail"), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            ElseIf SaveReservationReceipt(dsMainHost.Tables("Host_ReservationReceipt"), tran, saveWioutPayement) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
                'ElseIf SaveServices(dsMainHost.Tables("Host_Services")) Then
                '    tran.Rollback()
                '    CloseConnection()
                '    Return False
            Else
                If dsMainHost.Tables("Host_Reservation").Rows(0)("RecordStatus").ToString() <> "Updated" Then
                    If objcomm.UpdateDocumentNo("RESERVATION NUMBER", SpectrumCon, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
                tran.Commit()
                CloseConnection()
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 02-02-2017 for save reservation
    'updated by khusrao adil on 16-02-2017 for update reservation details
    Public Function SaveReservation(ByVal dtSaveReservation As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveReservation = False
            For Each drSaveReservation In dtSaveReservation.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0


                Dim _tmpdrtotalAccommodationCharges = IIf(Not (drSaveReservation("totalAccommodationCharges") Is DBNull.Value), drSaveReservation("totalAccommodationCharges"), 0)
                Dim _tmpdrtotalServicesCharges = IIf(Not (drSaveReservation("totalServicesCharges") Is DBNull.Value), drSaveReservation("totalServicesCharges"), 0)
                Dim _tmpdrtotalFoodCharges = IIf(Not (drSaveReservation("totalFoodCharges") Is DBNull.Value), drSaveReservation("totalFoodCharges"), 0)
                Dim txtrese = Convert.ToDateTime(drSaveReservation("reservationDateTime").ToString())
                'Dim dtReservationDate As Date = DateTime.ParseExact(drSaveReservation("reservationDateTime").ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                'Dim dtCheckInDate As Date = DateTime.ParseExact(drSaveReservation("checkInDateTime").ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                'Dim dtCheckOutDate As Date = DateTime.ParseExact(drSaveReservation("checkOutDateTime").ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                If drSaveReservation("RecordStatus").ToString() = "Update" Then
                    SqlQuery.Append("update Host_Reservation set" & vbCrLf)
                    SqlQuery.Append("reservationStatusID='" & drSaveReservation("reservationStatusID") & "'," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID='" & drSaveReservation("mstSupportedDocumentTypeID") & "'," & vbCrLf)
                    SqlQuery.Append("TerminalID='" & drSaveReservation("TerminalID") & "',siteCode='" & drSaveReservation("siteCode") & "'," & vbCrLf)
                    SqlQuery.Append("yearInyyyy='" & drSaveReservation("yearInyyyy") & "'," & vbCrLf)
                    SqlQuery.Append("reservationDateTime=@ReservationDate,checkInDateTime=@checkInDate,checkOutDateTime=@CheckOutDate," & vbCrLf)
                    SqlQuery.Append("totalDaysOfStay='" & drSaveReservation("totalDaysOfStay") & "'," & vbCrLf)
                    SqlQuery.Append("totalNightsOfStay='" & drSaveReservation("totalNightsOfStay") & "'," & vbCrLf)

                    SqlQuery.Append("totalNoOfRooms='" & drSaveReservation("totalNoOfRooms") & "'," & vbCrLf)
                    SqlQuery.Append("totalNetAmount=" & drSaveReservation("totalNetAmount") & "," & vbCrLf)
                    SqlQuery.Append("totalTaxAmount=" & drSaveReservation("totalTaxAmount") & "," & vbCrLf)
                    SqlQuery.Append("totalPromotionDiscountAmount=" & drSaveReservation("totalPromotionDiscountAmount") & "," & vbCrLf)
                    SqlQuery.Append("totalGrossAmount=" & drSaveReservation("totalGrossAmount") & "," & vbCrLf)
                    SqlQuery.Append("totalAmountPaid=" & drSaveReservation("totalAmountPaid") & "," & vbCrLf)
                    SqlQuery.Append("remainingAmountToPay=" & drSaveReservation("remainingAmountToPay") & "," & vbCrLf)
                    SqlQuery.Append("totalAccommodationCharges=" & _tmpdrtotalAccommodationCharges & "," & vbCrLf)
                    SqlQuery.Append("totalServicesCharges=" & _tmpdrtotalServicesCharges & "," & vbCrLf)
                    SqlQuery.Append("totalFoodCharges=" & _tmpdrtotalFoodCharges & "," & vbCrLf)
                    SqlQuery.Append("primaryGuestDocumentNumber='" & drSaveReservation("primaryGuestDocumentNumber") & "'," & vbCrLf)
                    SqlQuery.Append("primaryGuestDocumentDetails='" & drSaveReservation("primaryGuestDocumentDetails") & "'," & vbCrLf)
                    '  SqlQuery.Append("primaryGuestDocumentImage='" & drSaveReservation("primaryGuestDocumentImage") & "'," & vbCrLf)
                    SqlQuery.Append("totalNoOfAdults='" & drSaveReservation("totalNoOfAdults") & "',totalNoOfChildren='" & drSaveReservation("totalNoOfChildren") & "'," & vbCrLf)
                    SqlQuery.Append("remarks1='" & drSaveReservation("remarks1") & "',remarks2='" & drSaveReservation("remarks2") & "'," & vbCrLf)
                    SqlQuery.Append("updatedAt='" & drSaveReservation("updatedAt") & "',updatedBy='" & drSaveReservation("updatedBy") & "'," & vbCrLf)
                    SqlQuery.Append("updatedOn=GETDATE(),mstStatusID='" & drSaveReservation("mstStatusID") & "'" & vbCrLf)
                    SqlQuery.Append("where reservationID='" & drSaveReservation("reservationID") & "' and reservationNumber='" & drSaveReservation("reservationNumber") & "'" & vbCrLf)
                Else
                    SqlQuery.Append("Insert Into Host_Reservation (reservationID,reservationStatusID,mstSupportedDocumentTypeID,TerminalID," & vbCrLf)
                    SqlQuery.Append("siteCode,yearInyyyy,reservationNumber,reservationDateTime,checkInDateTime,checkOutDateTime,totalDaysOfStay,totalNightsOfStay," & vbCrLf)
                    SqlQuery.Append("totalNoOfRooms,totalNetAmount,totalTaxAmount,totalPromotionDiscountAmount,totalGrossAmount,totalAmountPaid,remainingAmountToPay," & vbCrLf)
                    SqlQuery.Append("totalAccommodationCharges,totalServicesCharges,totalFoodCharges,primaryGuestDocumentNumber,primaryGuestDocumentDetails," & vbCrLf)
                    SqlQuery.Append("primaryGuestDocumentImage,totalNoOfAdults,totalNoOfChildren,remarks1,remarks2," & vbCrLf)
                    SqlQuery.Append("createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID)" & vbCrLf)
                    SqlQuery.Append("values('" & drSaveReservation("reservationID") & "','" & drSaveReservation("reservationStatusID") & "','" & drSaveReservation("mstSupportedDocumentTypeID") & "','" & drSaveReservation("TerminalID") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservation("siteCode") & "','" & drSaveReservation("yearInyyyy") & "','" & drSaveReservation("reservationNumber") & "', " & vbCrLf)
                    SqlQuery.Append("@ReservationDate,@checkInDate,@CheckOutDate, " & vbCrLf)

                    SqlQuery.Append("'" & drSaveReservation("totalDaysOfStay") & "','" & drSaveReservation("totalNightsOfStay") & "','" & drSaveReservation("totalNoOfRooms") & "', " & vbCrLf)
                    SqlQuery.Append("" & drSaveReservation("totalNetAmount") & "," & drSaveReservation("totalTaxAmount") & "," & drSaveReservation("totalPromotionDiscountAmount") & "," & vbCrLf)
                    SqlQuery.Append("" & drSaveReservation("totalGrossAmount") & "," & drSaveReservation("totalAmountPaid") & "," & drSaveReservation("remainingAmountToPay") & ", " & vbCrLf)

                    SqlQuery.Append("" & _tmpdrtotalAccommodationCharges & "," & vbCrLf) 'S
                    SqlQuery.Append("" & _tmpdrtotalServicesCharges & ", " & vbCrLf)
                    SqlQuery.Append("" & _tmpdrtotalFoodCharges & ", " & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservation("primaryGuestDocumentNumber") & "','" & drSaveReservation("primaryGuestDocumentDetails") & "','" & drSaveReservation("primaryGuestDocumentImage") & "', " & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservation("totalNoOfAdults") & "','" & drSaveReservation("totalNoOfChildren") & "','" & drSaveReservation("remarks1") & "', " & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservation("remarks2") & "','" & drSaveReservation("createdAt") & "','" & drSaveReservation("createdBy") & "', " & vbCrLf)
                    SqlQuery.Append("GETDATE(),'" & drSaveReservation("updatedAt") & "','" & drSaveReservation("updatedBy") & "', " & vbCrLf)
                    SqlQuery.Append("GETDATE(),'" & drSaveReservation("mstStatusID") & "') " & vbCrLf)
                End If
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)

                cmdTrn.Parameters.Add("@ReservationDate", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@checkInDate", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@CheckOutDate", SqlDbType.DateTime)
                cmdTrn.Parameters("@ReservationDate").Value = drSaveReservation("reservationDateTime")
                cmdTrn.Parameters("@checkInDate").Value = drSaveReservation("checkInDateTime")
                cmdTrn.Parameters("@CheckOutDate").Value = drSaveReservation("checkOutDateTime")
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservation = True
                Else
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'Code is added by irfan on 04/04/2018 For hotel reservation.
    Public Function SaveCustomersDetails(ByVal dtSaveCustmers As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveCustomersDetails = False
            If dtSaveCustmers.Rows.Count > 0 Then
                For Each drcus In dtSaveCustmers.Rows
                    Dim sqlquery As New StringBuilder
                    sqlquery.Length = 0
                    If drcus("RecordStatus").ToString = "Updated" Then
                        sqlquery.Append("update clpcustomers set ClpProgramId='" & drcus("ClpProgramId") & "', SiteCode='" & drcus("SiteCode") & "' ," & vbCrLf)
                        sqlquery.Append(" FirstName='" & drcus("FirstName") & "',MiddleName='" & drcus("MiddleName") & "',SurName='" & drcus("SurName") & "'," & vbCrLf)
                        sqlquery.Append("Gender='" & drcus("Gender") & "',MobileNo='" & drcus("MobileNo") & "' ," & vbCrLf)
                        'sqlquery.Append(,CREATEDAT='" & drcus("CREATEDAT") & "',"CREATEDBY='" & drcus("CREATEDBY") & "',CREATEDON='" & drcus("CREATEDON") & "'  ," & vbCrLf)
                        sqlquery.Append("UPDATEDAT='" & drcus("UPDATEDAT") & "',UPDATEDBY='" & drcus("UPDATEDBY") & "',UPDATEDON='" & drcus("UPDATEDON") & "' ," & vbCrLf)
                        sqlquery.Append("STATUS='" & drcus("STATUS") & "',accountno='" & drcus("accountno") & "',CardExpiryDT='" & Format(drcus("CardExpiryDT"), "yyyy-MM-dd") & "' ," & vbCrLf)
                        sqlquery.Append("cardisactive='" & drcus("cardisactive") & "' where  CardNo='" & drcus("CardNo") & "'" & vbCrLf)
                    Else
                        sqlquery.Append("insert into clpcustomers(CardNo,ClpProgramId,SiteCode,FirstName,MiddleName,SurName,Gender,MobileNo,CREATEDAT,CREATEDBY,CREATEDON," & vbCrLf)
                        sqlquery.Append("UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,accountno,CardExpiryDT,cardisactive)" & vbCrLf)
                        sqlquery.Append("Values('" & drcus("CardNo") & "','" & drcus("ClpProgramId") & "','" & drcus("SiteCode") & "', '" & drcus("FirstName") & "'," & vbCrLf)
                        sqlquery.Append("'" & drcus("MiddleName") & "','" & drcus("SurName") & "','" & drcus("Gender") & "','" & drcus("MobileNo") & "'," & vbCrLf)
                        sqlquery.Append("'" & drcus("CREATEDAT") & "','" & drcus("CREATEDBY") & "','" & drcus("CREATEDON") & "','" & drcus("UPDATEDAT") & "' ," & vbCrLf)
                        sqlquery.Append("'" & drcus("UPDATEDBY") & "','" & drcus("UPDATEDON") & "','" & drcus("STATUS") & "','" & drcus("accountno") & "'," & vbCrLf)
                        sqlquery.Append("'" & Format(drcus("CardExpiryDT"), "yyyy-MM-dd") & "','" & drcus("cardisactive") & "')" & vbCrLf)
                    End If
                    Dim cmdTrn As New SqlCommand(sqlquery.ToString(), SpectrumCon)
                    cmdTrn.Transaction = tran
                    OpenConnection()
                    If cmdTrn.ExecuteNonQuery() > 0 Then
                        SaveCustomersDetails = True
                    Else
                        Return False
                    End If
                Next
            End If

        Catch ex As Exception
            LogException(ex)
        End Try


    End Function
    'added by khusrao adil on 05-02-2017 for save guest details
    'updated by khusrao adil on 16-02-2017 for update guest details
    Public Function SaveGuestDetails(ByVal dtSaveGuest As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveGuestDetails = False
            For Each drSaveGuest In dtSaveGuest.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                If drSaveGuest("RecordStatus").ToString() = "Update" Then
                    Dim img = drSaveGuest("supportedDocumentImage_1")
                    SqlQuery.Append("update Host_ReservationGuestDetail set  guestFirstName='" & drSaveGuest("guestFirstName") & "',guestMiddleName='" & drSaveGuest("guestMiddleName") & "',guestLastName='" & drSaveGuest("guestLastName") & "'," & vbCrLf)
                    SqlQuery.Append("guestEmailID='" & drSaveGuest("guestEmailID") & "',guestMobileNumber='" & drSaveGuest("guestMobileNumber") & "',guestGender='" & drSaveGuest("guestGender") & "'," & vbCrLf)
                    SqlQuery.Append("guestAgeInYears='" & drSaveGuest("guestAgeInYears") & "',remarks='" & drSaveGuest("remarks") & "'," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_1='" & drSaveGuest("mstSupportedDocumentTypeID_1") & "'," & vbCrLf)
                    SqlQuery.Append("supportedDocumentNumber_1='" & drSaveGuest("supportedDocumentNumber_1") & "'," & vbCrLf)
                    SqlQuery.Append("supportedDocumentDetails_1='" & drSaveGuest("supportedDocumentDetails_1") & "'," & vbCrLf)
                    'SqlQuery.Append("supportedDocumentImage_1='" & img & "'," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_2='" & drSaveGuest("mstSupportedDocumentTypeID_2") & "',supportedDocumentNumber_2='" & drSaveGuest("supportedDocumentNumber_2") & "',supportedDocumentDetails_2='" & drSaveGuest("supportedDocumentDetails_2") & "'," & vbCrLf)
                    'SqlQuery.Append(",supportedDocumentImage_2='" & drSaveGuest("supportedDocumentImage_2") & "'," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_3='" & drSaveGuest("mstSupportedDocumentTypeID_3") & "'," & vbCrLf)
                    'SqlQuery.Append(",supportedDocumentImage_3='" & drSaveGuest("supportedDocumentImage_3") & "'," & vbCrLf)
                    SqlQuery.Append("supportedDocumentDetails='" & drSaveGuest("supportedDocumentDetails") & "'," & vbCrLf)
                    SqlQuery.Append("isPrimaryGuest='" & drSaveGuest("isPrimaryGuest") & "',updatedOn=GETDATE(),updatedAt='" & drSaveGuest("updatedAt") & "',updatedBy='" & drSaveGuest("updatedBy") & "' " & vbCrLf)
                    SqlQuery.Append("where reservationGuestDetailID=" & drSaveGuest("reservationGuestDetailID") & "" & vbCrLf)
                Else
                    SqlQuery.Append("insert into Host_ReservationGuestDetail(reservationGuestDetailID,reservationID," & vbCrLf)
                    SqlQuery.Append("guestFirstName,guestMiddleName,guestLastName,guestEmailID,guestMobileNumber,guestAgeInYears,guestGender," & vbCrLf)
                    SqlQuery.Append("remarks,createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_1,supportedDocumentNumber_1,supportedDocumentDetails_1,supportedDocumentImage_1," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_2,supportedDocumentNumber_2,supportedDocumentDetails_2,supportedDocumentImage_2," & vbCrLf)
                    SqlQuery.Append("mstSupportedDocumentTypeID_3,supportedDocumentNumber_3,supportedDocumentDetails_3,supportedDocumentImage_3," & vbCrLf)
                    SqlQuery.Append("isPrimaryGuest,CardNo,ClpProgramId,supportedDocumentDetails)" & vbCrLf)
                    'SqlQuery.Append("isPrimaryGuest,CardNo,ClpProgramId)" & vbCrLf)
                    SqlQuery.Append("values(" & drSaveGuest("reservationGuestDetailID") & "," & drSaveGuest("reservationID") & "," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("guestFirstName") & "','" & drSaveGuest("guestMiddleName") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("guestLastName") & "','" & drSaveGuest("guestEmailID") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("guestMobileNumber") & "','" & drSaveGuest("guestAgeInYears") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("guestGender") & "','" & drSaveGuest("remarks") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("createdAt") & "','" & drSaveGuest("createdBy") & "'," & vbCrLf)
                    SqlQuery.Append("GETDATE(),'" & drSaveGuest("updatedAt") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("updatedBy") & "',GETDATE()," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("mstStatusID") & "','" & drSaveGuest("mstSupportedDocumentTypeID_1") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentNumber_1") & "','" & drSaveGuest("supportedDocumentDetails_1") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentImage_1") & "','" & drSaveGuest("mstSupportedDocumentTypeID_2") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentNumber_2") & "','" & drSaveGuest("supportedDocumentDetails_2") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentImage_2") & "','" & drSaveGuest("mstSupportedDocumentTypeID_3") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentNumber_3") & "','" & drSaveGuest("supportedDocumentDetails_3") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentImage_3") & "','" & drSaveGuest("isPrimaryGuest") & "'," & vbCrLf)
                    ' SqlQuery.Append("'" & drSaveGuest("CardNo") & "','" & drSaveGuest("ClpProgramId") & "')" & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("CardNo") & "','" & drSaveGuest("ClpProgramId") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveGuest("supportedDocumentDetails") & "')" & vbCrLf)
                End If
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveGuestDetails = True
                Else
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 07-02-2017 for save reservation's rooms
    'updated by khusrao adil on 16-02-2017 for update room dtails
    Public Function SaveReservationRoomMap(ByVal dtSaveReservationRoomMap As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveReservationRoomMap = False
            For Each drSaveReservationRoomMap In dtSaveReservationRoomMap.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                If drSaveReservationRoomMap("RecordStatus").ToString() = "Update" Then
                    SqlQuery.Append("update Host_ReservationRoomMap set mstRoomID='" & drSaveReservationRoomMap("mstRoomID") & "'," & vbCrLf)
                    SqlQuery.Append("mstStandardRoomRateID='" & drSaveReservationRoomMap("mstStandardRoomRateID") & "',rateDate=@RateDate," & vbCrLf)
                    SqlQuery.Append("yearInyyyy='" & drSaveReservationRoomMap("yearInyyyy") & "'," & vbCrLf)
                    SqlQuery.Append("weekDay='" & drSaveReservationRoomMap("weekDay") & "',rate='" & drSaveReservationRoomMap("rate") & "'," & vbCrLf)
                    SqlQuery.Append("totalTaxInPercent='" & drSaveReservationRoomMap("totalTaxInPercent") & "'," & vbCrLf)
                    SqlQuery.Append("totalTaxInAmount='" & drSaveReservationRoomMap("totalTaxInAmount") & "'," & vbCrLf)
                    SqlQuery.Append("totalPromotionDiscountInPercent='" & drSaveReservationRoomMap("totalPromotionDiscountInPercent") & "'," & vbCrLf)
                    SqlQuery.Append("totalPromotionDiscountInAmount='" & drSaveReservationRoomMap("totalPromotionDiscountInAmount") & "'," & vbCrLf)
                    SqlQuery.Append("totalNetAmount='" & drSaveReservationRoomMap("totalNetAmount") & "',totalGrossAmount='" & drSaveReservationRoomMap("totalGrossAmount") & "'," & vbCrLf)
                    SqlQuery.Append("remarks='" & drSaveReservationRoomMap("remarks") & "',updatedAt='" & drSaveReservationRoomMap("updatedAt") & "',updatedBy='" & drSaveReservationRoomMap("updatedBy") & "'," & vbCrLf)
                    SqlQuery.Append("updatedOn=GETDATE(),mstStatusID='" & drSaveReservationRoomMap("mstStatusID") & "'" & vbCrLf)
                    SqlQuery.Append("where reservationRoomMapID=" & drSaveReservationRoomMap("reservationRoomMapID") & " and reservationID=" & drSaveReservationRoomMap("reservationID") & "" & vbCrLf)
                Else
                    SqlQuery.Append("insert into Host_ReservationRoomMap(reservationRoomMapID,reservationID," & vbCrLf)
                    SqlQuery.Append("mstRoomID,mstStandardRoomRateID,rateDate,yearInyyyy,weekDay,rate," & vbCrLf)
                    SqlQuery.Append("totalTaxInPercent,totalTaxInAmount,totalPromotionDiscountInPercent,totalPromotionDiscountInAmount," & vbCrLf)
                    SqlQuery.Append("totalNetAmount,totalGrossAmount,remarks," & vbCrLf)
                    SqlQuery.Append("createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID)" & vbCrLf)
                    ' SqlQuery.Append("@ReservationDate,@checkInDate,@CheckOutDate, " & vbCrLf)
                    SqlQuery.Append("values(" & drSaveReservationRoomMap("reservationRoomMapID") & "," & drSaveReservationRoomMap("reservationID") & "," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("mstRoomID") & "','" & drSaveReservationRoomMap("mstStandardRoomRateID") & "'," & vbCrLf)
                    SqlQuery.Append("@RateDate,'" & drSaveReservationRoomMap("yearInyyyy") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("weekDay") & "','" & drSaveReservationRoomMap("rate") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("totalTaxInPercent") & "','" & drSaveReservationRoomMap("totalTaxInAmount") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("totalPromotionDiscountInPercent") & "','" & drSaveReservationRoomMap("totalPromotionDiscountInAmount") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("totalNetAmount") & "','" & drSaveReservationRoomMap("totalGrossAmount") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("remarks") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("createdAt") & "','" & drSaveReservationRoomMap("createdBy") & "'," & vbCrLf)
                    SqlQuery.Append("GETDATE(),'" & drSaveReservationRoomMap("updatedAt") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("updatedBy") & "',GETDATE()," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomMap("mstStatusID") & "')" & vbCrLf)
                End If
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                cmdTrn.Parameters.Add("@RateDate", SqlDbType.DateTime)
                cmdTrn.Parameters("@RateDate").Value = drSaveReservationRoomMap("rateDate")
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservationRoomMap = True
                Else
                    Return False
                End If

            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 09-02-2017 for save reservation's taxes
    'updated by khusrao adil on 20-02-2017 for update tax detail
    Public Function SaveReservationRoomTaxMap(ByVal dtSaveReservationRoomTaxMap As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            If dtSaveReservationRoomTaxMap.Rows.Count > 0 Then
                For Each drSaveReservationRoomTaxMap In dtSaveReservationRoomTaxMap.Rows
                    Dim SqlQuery As New StringBuilder
                    SqlQuery.Length = 0
                    If drSaveReservationRoomTaxMap("RecordStatus").ToString() = "Update" Then
                        SqlQuery.Append("update Host_ReservationTaxMap set" & vbCrLf)
                        SqlQuery.Append("SiteCode='" & drSaveReservationRoomTaxMap("SiteCode") & "',TaxLineNo='" & drSaveReservationRoomTaxMap("TaxLineNo") & "'," & vbCrLf)
                        SqlQuery.Append("DocumentType='" & drSaveReservationRoomTaxMap("DocumentType") & "',TaxCode='" & drSaveReservationRoomTaxMap("TaxCode") & "'," & vbCrLf)
                        SqlQuery.Append("TaxValueInPercent='" & drSaveReservationRoomTaxMap("TaxValueInPercent") & "'," & vbCrLf)
                        SqlQuery.Append("TaxValueInAmount='" & drSaveReservationRoomTaxMap("TaxValueInAmount") & "'," & vbCrLf)
                        SqlQuery.Append("updatedAt='" & drSaveReservationRoomTaxMap("updatedAt") & "',updatedBy='" & drSaveReservationRoomTaxMap("updatedBy") & "'," & vbCrLf)
                        SqlQuery.Append("updatedOn=GETDATE(),mstStatusID='" & drSaveReservationRoomTaxMap("mstStatusID") & "'" & vbCrLf)
                        SqlQuery.Append("where reservationTaxMapID=" & drSaveReservationRoomTaxMap("reservationTaxMapID") & " and reservationID=" & drSaveReservationRoomTaxMap("reservationID") & "" & vbCrLf)
                    Else
                        SqlQuery.Append("insert into Host_ReservationTaxMap(reservationTaxMapID,reservationID,SiteCode," & vbCrLf)
                        SqlQuery.Append("TaxLineNo,DocumentType,TaxCode,TaxValueInPercent,TaxValueInAmount," & vbCrLf)
                        SqlQuery.Append("createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID)" & vbCrLf)
                        SqlQuery.Append("values(" & drSaveReservationRoomTaxMap("reservationTaxMapID") & "," & drSaveReservationRoomTaxMap("reservationID") & "," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("SiteCode") & "'," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("TaxLineNo") & "','" & drSaveReservationRoomTaxMap("DocumentType") & "'," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("TaxCode") & "','" & drSaveReservationRoomTaxMap("TaxValueInPercent") & "'," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("TaxValueInAmount") & "'," & vbCrLf)
                        'SqlQuery.Append("'" & drSaveReservationRoomTaxMap("totalTaxInPercent") & "'," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("createdAt") & "','" & drSaveReservationRoomTaxMap("createdBy") & "'," & vbCrLf)
                        SqlQuery.Append("GETDATE(),'" & drSaveReservationRoomTaxMap("updatedAt") & "'," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("updatedBy") & "',GETDATE()," & vbCrLf)
                        SqlQuery.Append("'" & drSaveReservationRoomTaxMap("mstStatusID") & "')" & vbCrLf)
                    End If

                    Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                    cmdTrn.Transaction = tran
                    OpenConnection()
                    If cmdTrn.ExecuteNonQuery() > 0 Then
                        drSaveReservationRoomTaxMap = True
                    Else
                        Return False
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 10-02-2017 for save reservation's promotion's
    'updated by khusrao adil on 20-02-2017 for update prmotion's details
    Public Function SaveReservationRoomTypePromotionMap(ByVal dtSaveReservationRoomTypePromotionMap As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveReservationRoomTypePromotionMap = False
            For Each drSaveReservationRoomTypePromotionMap In dtSaveReservationRoomTypePromotionMap.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                If drSaveReservationRoomTypePromotionMap("RecordStatus").ToString() = "Update" Then
                    SqlQuery.Append("update Host_ReservationRoomTypePromotionMap set" & vbCrLf)
                    SqlQuery.Append("mstSiteRoomTypeMap='" & drSaveReservationRoomTypePromotionMap("mstSiteRoomTypeMap") & "',mstPromotionID='" & drSaveReservationRoomTypePromotionMap("mstPromotionID") & "'," & vbCrLf)
                    SqlQuery.Append("rateDate=@RateDate,yearInyyyy='" & drSaveReservationRoomTypePromotionMap("yearInyyyy") & "'," & vbCrLf)
                    SqlQuery.Append("promotionName='" & drSaveReservationRoomTypePromotionMap("promotionName") & "',weekDay='" & drSaveReservationRoomTypePromotionMap("weekDay") & "'," & vbCrLf)
                    SqlQuery.Append("discountInPercent='" & drSaveReservationRoomTypePromotionMap("discountInPercent") & "'," & vbCrLf)
                    SqlQuery.Append("applicableWithOtherPromotions='" & drSaveReservationRoomTypePromotionMap("applicableWithOtherPromotions") & "'," & vbCrLf)
                    SqlQuery.Append("discountAmount='" & drSaveReservationRoomTypePromotionMap("discountAmount") & "'," & vbCrLf)
                    SqlQuery.Append("updatedAt='" & drSaveReservationRoomTypePromotionMap("updatedAt") & "',updatedBy='" & drSaveReservationRoomTypePromotionMap("updatedBy") & "'," & vbCrLf)
                    SqlQuery.Append("updatedOn=GETDATE(), mstStatusID=" & drSaveReservationRoomTypePromotionMap("mstStatusID") & "" & vbCrLf)
                    SqlQuery.Append("where reservationRoomTypePromotionMapID=" & drSaveReservationRoomTypePromotionMap("reservationRoomTypePromotionMapID") & " and reservationID=" & drSaveReservationRoomTypePromotionMap("reservationID") & "" & vbCrLf)
                Else
                    SqlQuery.Append("insert into Host_ReservationRoomTypePromotionMap (reservationRoomTypePromotionMapID,reservationID," & vbCrLf)
                    SqlQuery.Append("mstSiteRoomTypeMap,mstPromotionID,promotionName," & vbCrLf)
                    SqlQuery.Append("rateDate,yearInyyyy,weekDay,discountInPercent,discountAmount," & vbCrLf)
                    SqlQuery.Append("applicableWithOtherPromotions,description," & vbCrLf)
                    SqlQuery.Append("createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID,PromotionLineNo)" & vbCrLf)

                    SqlQuery.Append("values(" & drSaveReservationRoomTypePromotionMap("reservationRoomTypePromotionMapID") & "," & drSaveReservationRoomTypePromotionMap("reservationID") & "," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("mstSiteRoomTypeMap") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("mstPromotionID") & "','" & drSaveReservationRoomTypePromotionMap("promotionName") & "'," & vbCrLf)
                    SqlQuery.Append("@RateDate,'" & drSaveReservationRoomTypePromotionMap("yearInyyyy") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("weekDay") & "'," & vbCrLf)
                    'SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("totalTaxInPercent") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("discountInPercent") & "','" & drSaveReservationRoomTypePromotionMap("discountAmount") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("applicableWithOtherPromotions") & "','" & drSaveReservationRoomTypePromotionMap("description") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("createdAt") & "','" & drSaveReservationRoomTypePromotionMap("createdBy") & "'," & vbCrLf)
                    SqlQuery.Append("GETDATE(),'" & drSaveReservationRoomTypePromotionMap("updatedAt") & "'," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("updatedBy") & "',GETDATE()," & vbCrLf)
                    SqlQuery.Append("'" & drSaveReservationRoomTypePromotionMap("mstStatusID") & "'," & vbCrLf)
                    SqlQuery.Append("isNull(('" & drSaveReservationRoomTypePromotionMap("PromotionLineNo") & "'),0))" & vbCrLf)
                End If


                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                cmdTrn.Parameters.Add("@RateDate", SqlDbType.DateTime)
                cmdTrn.Parameters("@RateDate").Value = drSaveReservationRoomTypePromotionMap("rateDate")
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservationRoomTypePromotionMap = True
                Else
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 13-02-2017 for save reservation receipt
    Public Function SaveReservationReceipt(ByVal dtSaveReservationReceipt As DataTable, ByRef tran As SqlTransaction, ByVal saveWithoutPayment As Boolean) As Boolean
        Try
            SaveReservationReceipt = False
            If saveWithoutPayment = True Then
                Return True
            End If

            For Each drSaveReservationReceipt In dtSaveReservationReceipt.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                SqlQuery.Append("insert into Host_ReservationReceipt (reservationReceiptID,reservationID," & vbCrLf)
                SqlQuery.Append("TenderHeadCode,SiteCode,yearInyyyy,reservationNumber," & vbCrLf)
                SqlQuery.Append("receiptLineNo,TerminalID,CardNo,ExchangeRate," & vbCrLf)
                SqlQuery.Append("TenderTypeCode,AmountTendered,CurrencyCode,AmountinCurrency," & vbCrLf)
                SqlQuery.Append("billReceiptDate,billReceiptTime,StaffID,ManagersKeytoUpdate,ChangeLine," & vbCrLf)
                SqlQuery.Append("RefNo_1,RefNo_2,RefNo_3,RefNo_4,RefDate,BankAccNo,remarks," & vbCrLf)
                SqlQuery.Append("createdAt,createdBy,createdOn,updatedAt,updatedBy,updatedOn,mstStatusID)" & vbCrLf)

                SqlQuery.Append("values(" & drSaveReservationReceipt("reservationReceiptID") & "," & drSaveReservationReceipt("reservationID") & "," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("TenderHeadCode") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("SiteCode") & "','" & drSaveReservationReceipt("yearInyyyy") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("reservationNumber") & "'," & drSaveReservationReceipt("receiptLineNo") & "," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("TerminalID") & "','" & drSaveReservationReceipt("CardNo") & "'," & vbCrLf)
                SqlQuery.Append("" & drSaveReservationReceipt("ExchangeRate") & "," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("TenderTypeCode") & "','" & drSaveReservationReceipt("AmountTendered") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("CurrencyCode") & "'," & vbCrLf)
                SqlQuery.Append("" & drSaveReservationReceipt("AmountinCurrency") & "," & vbCrLf)
                SqlQuery.Append("@billReceiptDate," & vbCrLf)
                SqlQuery.Append("@billReceiptTime,'" & drSaveReservationReceipt("StaffID") & "'," & vbCrLf)
                SqlQuery.Append("" & drSaveReservationReceipt("ManagersKeytoUpdate") & ",0," & vbCrLf) '-----------'" & drSaveReservationReceipt("ChangeLine") & "'
                SqlQuery.Append("'" & drSaveReservationReceipt("RefNo_1") & "','" & drSaveReservationReceipt("RefNo_2") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("RefNo_3") & "','" & drSaveReservationReceipt("RefNo_4") & "'," & vbCrLf)
                SqlQuery.Append("@RefDate,'" & drSaveReservationReceipt("BankAccNo") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("remarks") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("createdAt") & "','" & drSaveReservationReceipt("createdBy") & "'," & vbCrLf)
                SqlQuery.Append("GETDATE(),'" & drSaveReservationReceipt("updatedAt") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("updatedBy") & "',GETDATE()," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceipt("mstStatusID") & "')" & vbCrLf)
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                'cmdTrn.Parameters.Add("@ExchangeRate", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@billReceiptDate", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@billReceiptTime", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@RefDate", SqlDbType.DateTime)
                'cmdTrn.Parameters("@ExchangeRate").Value = drSaveReservationReceipt("ExchangeRate")
                cmdTrn.Parameters("@billReceiptDate").Value = drSaveReservationReceipt("billReceiptDate")
                cmdTrn.Parameters("@billReceiptTime").Value = drSaveReservationReceipt("billReceiptTime")
                cmdTrn.Parameters("@RefDate").Value = drSaveReservationReceipt("RefDate")
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservationReceipt = True
                Else
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 14-02-2017 for save payment by check 
    ' Note : not in use right now need a discussion for it
    Public Function SaveReservationReceiptCheckDetails(ByVal dtSaveReservationReceiptCheck As DataTable, ByRef tran As SqlTransaction) As Boolean
        Try
            SaveReservationReceiptCheckDetails = False
            For Each drSaveReservationReceiptCheck In dtSaveReservationReceiptCheck.Rows
                Dim SqlQuery As New StringBuilder
                SqlQuery.Length = 0
                SqlQuery.Append("INSERT INTO CHECKDTLS (SITECODE,FINYEAR,BILLNO," & vbCrLf)
                SqlQuery.Append("PAYLINENO,CHECKNO,DOCUMENTNO,DOCUMENTTYPE," & vbCrLf)
                SqlQuery.Append("BILLDATE,AMOUNT,DUEDATE," & vbCrLf)
                SqlQuery.Append("REMARKS,BANKNAME,CUSTOMERNAME,TELEPHONENUMBER," & vbCrLf)
                SqlQuery.Append("CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,)" & vbCrLf)

                SqlQuery.Append("values(" & drSaveReservationReceiptCheck("SITECODE") & "," & drSaveReservationReceiptCheck("FINYEAR") & "," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("BILLNO") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("PAYLINENO") & "','" & drSaveReservationReceiptCheck("DOCUMENTTYPE") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("DOCUMENTNO") & "'," & drSaveReservationReceiptCheck("receiptLineNo") & "," & vbCrLf)
                SqlQuery.Append("@BILLDATE,'" & drSaveReservationReceiptCheck("AMOUNT") & "'," & vbCrLf)
                SqlQuery.Append("@DUEDATE,'" & drSaveReservationReceiptCheck("REMARKS") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("BANKNAME") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("CUSTOMERNAME") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("TELEPHONENUMBER") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("createdAt") & "','" & drSaveReservationReceiptCheck("createdBy") & "'," & vbCrLf)
                SqlQuery.Append("GETDATE(),'" & drSaveReservationReceiptCheck("updatedAt") & "'," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("updatedBy") & "',GETDATE()," & vbCrLf)
                SqlQuery.Append("'" & drSaveReservationReceiptCheck("mstStatusID") & "')" & vbCrLf)
                Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)
                cmdTrn.Parameters.Add("@BILLDATE", SqlDbType.DateTime)
                cmdTrn.Parameters.Add("@DUEDATE", SqlDbType.DateTime)
                cmdTrn.Parameters("@BILLDATE").Value = drSaveReservationReceiptCheck("BILLDATE")
                cmdTrn.Parameters("@DUEDATE").Value = drSaveReservationReceiptCheck("DUEDATE")
                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservationReceiptCheckDetails = True
                Else
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 06-02-2017 for save reservation

End Class
