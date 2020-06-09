Public Class frmNBirthListSearch
    Private objclsBirthlist As New SpectrumBL.clsBirthList(clsAdmin.Financialyear)
    Private dtSearchResult As DataTable
    Private _drSearchResult As DataRow
    Private _iSelectedRowIndex As Integer
    Private blnCloseBirthListShow As Boolean
    ''' <summary>
    ''' Return Selected BirthList information
    ''' </summary>
    ''' <value>DataRow</value>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Public Property SearchBirthListInformation() As DataRow
        Get
            Return _drSearchResult
        End Get
        Set(ByVal value As DataRow)
            _drSearchResult = value
        End Set
    End Property
    ''' <summary>
    ''' Loading BirthList Searxh form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmBirthListSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strErrorMsg As String = ""
        Dim SqlQuery As String
        'SqlQuery = "select   BL.BirthListId as BirthListId ,BL.SiteCode,BL.customerid,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus ,BL.EventDate, BL.BirthDate ,CLPC.SurName + ' ' +  CLPC.FirstName   as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty ,sum(BLR.ReservedQty) as ReservedQty,CLPC.Mobileno as PhoneNo ,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress from BirthList BL left join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode left join CLPProgramSiteMap clps on bl.sitecode=clps.sitecode and clps.status=1 inner  join CLPCustomers CLPC on BL.customerid=CLPC.CardNo and clpc.ClpProgramId=clps.ClpProgramId inner join  dbo.CLPCustomerAddress CA on BL.customerid=CA.CardNo and ca.ClpProgramId=clpc.ClpProgramId and ca.AddressType='1' where   BL.BirthListStatus = 'Open' or   BL.BirthListStatus = 'True'  and Bl.CustomerType='CLP' group by BL.BirthListId, BL.SiteCode, BL.customerid,BL.EventId, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, CLPC.FirstName,clps.ClpProgramId,CLPC.SurName,BL.CustomerType,BL.EventId, CLPC.Mobileno ,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4  union Select distinct BL.BirthListId as BirthListId, BL.SiteCode, BL.customerid,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus,BL.EventDate, BL.BirthDate,SOC.LastName + ' ' + SOC.FirstName as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty ,sum(BLR.ReservedQty) as ReservedQty ,SOC.MobilePhone as PhoneNo,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress from BirthList BL left join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode Left join CustomerSaleOrder SOC on BL.customerid=SOC.CustomerNo and bl.sitecode=soc.sitecode inner join  dbo.CustomerAddress ca on bl.customerid=ca.CustomerNo  and bl.CustomerType=ca.CustomerType and ca.AddressType='1' where   BL.BirthListStatus= 'Open' or  BL.BirthListStatus = 'True' and Bl.CustomerType='SO' group by BL.BirthListId, BL.SiteCode, BL.customerid, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, BL.CustomerType, SOC.FirstName, SOC.LastName,BL.CustomerType,BL.EventId ,SOC.MobilePhone,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4 order by BL.BirthListId desc"
        If blnCloseBirthListShow Then
            SqlQuery = "select   BL.BirthListId as BirthListId ,BL.SiteCode,BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus ,BL.EventDate, BL.BirthDate ,CLPC.SurName + ' ' +  CLPC.FirstName   as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty , sum(BLR.ReservedQty) as ReservedQty,CLPC.Mobileno as PhoneNo ,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress, BL.UpdatedOn from BirthList BL left join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode left join CLPProgramSiteMap clps on bl.sitecode=clps.sitecode and clps.status=1 inner  join CLPCustomers CLPC on BL.customerid=CLPC.CardNo and clpc.ClpProgramId=clps.ClpProgramId  left outer join  dbo.CLPCustomerAddress CA on BL.customerid=CA.CardNo and ca.ClpProgramId=clpc.ClpProgramId  and ca.Defaults=1 where  BL.Status = 1  and Bl.CustomerType='CLP' group by BL.BirthListId, BL.SiteCode, BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, CLPC.FirstName,clps.ClpProgramId,CLPC.SurName,BL.CustomerType,BL.FINYEAR,BL.EventId, CLPC.Mobileno ,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4, BL.UpdatedOn  union Select distinct BL.BirthListId as BirthListId, BL.SiteCode, BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus,BL.EventDate, BL.BirthDate,SOC.LastName + ' ' + SOC.FirstName as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty ,sum(BLR.ReservedQty) as ReservedQty ,SOC.MobilePhone as PhoneNo,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress, BL.UpdatedOn from BirthList BL left outer join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode right outer  join CustomerSaleOrder SOC on BL.customerid=SOC.CustomerNo and bl.sitecode=soc.sitecode and Bl.CustomerType='SO' and  soc.CustomerType='SO' left outer join  dbo.CustomerAddress ca on SOC.CustomerNo=ca.CustomerNo  and SOC.CustomerType=ca.CustomerType and ca.AddressType='1' and ca.CustomerType='SO' where   BL.Status = 1  group by BL.BirthListId, BL.SiteCode, BL.customerid, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, BL.CustomerType, SOC.FirstName, SOC.LastName,BL.CustomerType,BL.FINYEAR,BL.EventId ,SOC.MobilePhone,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4, BL.UpdatedOn order by BL.UpdatedOn Desc, BL.BirthListId desc"
        Else
            SqlQuery = "select   BL.BirthListId as BirthListId ,BL.SiteCode,BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus ,BL.EventDate, BL.BirthDate ,CLPC.SurName + ' ' +  CLPC.FirstName   as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty , sum(BLR.ReservedQty) as ReservedQty,CLPC.Mobileno as PhoneNo ,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress, BL.UpdatedOn from BirthList BL left join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode left join CLPProgramSiteMap clps on bl.sitecode=clps.sitecode and clps.status=1 inner  join CLPCustomers CLPC on BL.customerid=CLPC.CardNo and clpc.ClpProgramId=clps.ClpProgramId  left outer join  dbo.CLPCustomerAddress CA on BL.customerid=CA.CardNo and ca.ClpProgramId=clpc.ClpProgramId  and ca.Defaults=1 where   (BL.BirthListStatus = 'Open' or   BL.BirthListStatus = 'True')  and Bl.CustomerType='CLP' group by BL.BirthListId, BL.SiteCode, BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, CLPC.FirstName,clps.ClpProgramId,CLPC.SurName,BL.CustomerType,BL.FINYEAR,BL.EventId, CLPC.Mobileno ,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4,BL.UpdatedOn  union Select distinct BL.BirthListId as BirthListId, BL.SiteCode, BL.customerid,BL.FINYEAR,BL.EventId, BL.DeliveryDate,BL.CustomerType, BL.BirthListStatus as BirthListStatus,BL.EventDate, BL.BirthDate,SOC.LastName + ' ' + SOC.FirstName as CustomerName,sum(BLR.RequstedQty) as RequstedQty, sum(BLR.BookedQty ) as BookedQty,sum(BLR.DeliveredQty) as DeliveredQty ,sum(BLR.ReservedQty) as ReservedQty ,SOC.MobilePhone as PhoneNo,CA.AddressLn1+ ' '+ CA.AddressLn2 + ' ' + CA.AddressLn3 + ' '+ CA.AddressLn4 as CustomerAddress, BL.UpdatedOn  from BirthList BL left outer join BirthListRequestedItems BLR on BL.BirthListId = BLR.BirthListId and BL.SiteCode=BLR.SiteCode right outer  join CustomerSaleOrder SOC on BL.customerid=SOC.CustomerNo and bl.sitecode=soc.sitecode and Bl.CustomerType='SO' and  soc.CustomerType='SO' left outer join  dbo.CustomerAddress ca on SOC.CustomerNo=ca.CustomerNo  and SOC.CustomerType=ca.CustomerType and ca.AddressType='1' and ca.CustomerType='SO' where   BL.BirthListStatus= 'Open' or  BL.BirthListStatus = 'True'  group by BL.BirthListId, BL.SiteCode, BL.customerid, BL.DeliveryDate, BL.BirthListStatus, BL.EventDate, BL.BirthDate, BL.CustomerType, SOC.FirstName, SOC.LastName,BL.CustomerType,BL.FINYEAR,BL.EventId ,SOC.MobilePhone,CA.AddressLn1,CA.AddressLn2,CA.AddressLn3,CA.AddressLn4, BL.UpdatedOn order by BL.UpdatedOn Desc, BL.BirthListId desc"
        End If
        dtSearchResult = objclsBirthlist.RetrieveQuery(SqlQuery.ToString(), strErrorMsg)


        grdSearchBirthListId.SetDataBinding(dtSearchResult, "", True)
        grdSearchBirthListId.AllowSort = True
        For Each col In grdSearchBirthListId.Columns
            'col.filterdropdown = True
            col.FilterEscape = ""

            'grdSearchBirthListId.Columns("EventDate").NumberFormat = clsAdmin.SqlDBDateFormat
            grdSearchBirthListId.HeadingStyle.WrapText = False
        Next
        For Each disColumn As C1.Win.C1TrueDBGrid.C1DisplayColumn In grdSearchBirthListId.Splits(0).DisplayColumns
            disColumn.AutoSize()
            disColumn.Width = disColumn.Width + 5
            If (disColumn.Width > 300) Then
                disColumn.Width = 300
            End If
        Next

        If Not (strErrorMsg = String.Empty) Then
            ShowMessage(strErrorMsg, getValueByKey("CLAE04"))
        End If
        SetCulture(Me, Me.Name)
    End Sub
    ''' <summary>
    ''' setting for Display grid
    ''' </summary>
    ''' <param name="querydT"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DisplayDataTable(ByVal querydT As DataTable) As DataTable
        Dim dtDisplayDataTable As New DataTable
        dtDisplayDataTable.Columns.Add(New DataColumn("BirthListID"))
        Dim dcCustomerName As New DataColumn
        dcCustomerName.ColumnName = "CustomerName"
        dtDisplayDataTable.Columns.Add(dcCustomerName)
        dtDisplayDataTable.Columns.Add(New DataColumn("EventDate"))
        dtDisplayDataTable.Columns.Add(New DataColumn("DeliveryDate"))
        dtDisplayDataTable.Columns.Add(New DataColumn("RequstedQty"))
        dtDisplayDataTable.Columns.Add(New DataColumn("BookedQty"))
        dtDisplayDataTable.Columns.Add(New DataColumn("DeliveredQty"))
        'dtDisplayDataTable.Columns.Add(New DataColumn("Amount"))

        dtDisplayDataTable.Load(querydT.CreateDataReader())
        dtDisplayDataTable.AcceptChanges()
        Return dtDisplayDataTable
    End Function

    ''' <summary>
    ''' Return BirthList information on double click on grid row and close the  form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSearchBirthListId_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSearchBirthListId.DoubleClick
        'Changed by Rohit on 19/04/2011

        'BirthListIndex(sender)
        BirthListInfo(grdSearchBirthListId.Row)
        'End Change
        Me.Close()
    End Sub

    ' ''' <summary>
    ' ''' Retrieve birthlist information
    ' ''' </summary>
    ' ''' <param name="objgrid"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function BirthListIndex(ByVal objgrid As Object) As Boolean
    '    Try
    '        Dim grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid = objgrid

    '        BirthListInfo(grdSearchBirthListId.Row)
    '        'Dim drselected As C1.Win.C1TrueDBGrid.SelectedRowCollection = grid.SelectedRows
    '        'If (drselected.Count > 0) Then
    '        'For Each dr As C1.Win.C1TrueDBGrid.SelectedRowCollection In drselected
    '        '    _index = dr.Item(

    '        'Next
    '        'End If
    '        Return True
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function

    Private Function BirthListInfo(ByVal _index As Integer) As Boolean
        Try
            'Changed by Rohit on 19/04/2011
            'Dim strBirthlistId As String = grdSearchBirthListId.Item(grdSearchBirthListId.Row, "BirthListId").ToString()
            Dim strBirthlistId As String = grdSearchBirthListId.Item(_index, "BirthListId").ToString()
            'End Change

            Dim dvInfo As DataView
            dvInfo = New DataView(dtSearchResult, "BirthListId='" + strBirthlistId + "'", "", DataViewRowState.CurrentRows)
            For Each dr As DataRowView In dvInfo
                SearchBirthListInformation = dr.Row
                Exit For
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            'Changed by Rohit on 19/04/2011
            'BirthListIndex(grdSearchBirthListId)
            BirthListInfo(grdSearchBirthListId.Row)
            'End Change
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub


    '    Private Sub dgSearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '        'BirthListIndex(sender)
    '        BirthListIndex_TrueDB(sender)
    '        Me.Close()
    '    End Sub

    '    Private Function BirthListIndex_TrueDB(ByVal objgrid As Object) As Boolean

    '        Try
    '            Dim grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid = objgrid

    '            Dim drselected As C1.Win.C1TrueDBGrid.SelectedRowCollection = grid.SelectedRows
    '            If (drselected.Count > 0) Then
    '                For Each dr As DataRow In drselected
    '                    SearchBirthListInformation = dr
    '                Next
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function

    Public Sub New(Optional ByVal isShowCloseBirthList As Boolean = False)
        blnCloseBirthListShow = isShowCloseBirthList
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub




End Class

