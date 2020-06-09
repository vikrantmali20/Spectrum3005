Partial Class POSDBDataSet
    Partial Class BirthListSoldItemPriceDataTable

        Private Sub BirthListSoldItemPriceDataTable_BirthListSoldItemPriceRowChanging(ByVal sender As System.Object, ByVal e As BirthListSoldItemPriceRowChangeEvent) Handles Me.BirthListSoldItemPriceRowChanging

        End Sub

    End Class

    Partial Class BirthListArticleDtlsDataTable

        Private Sub BirthListArticleDtlsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IsAmountPaidFromOpenAmountColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    Partial Class OpenQtyBirthListSalesDtlDataTable

        Private Sub OpenQtyBirthListSalesDtlDataTable_OpenQtyBirthListSalesDtlRowChanging(ByVal sender As System.Object, ByVal e As OpenQtyBirthListSalesDtlRowChangeEvent) Handles Me.OpenQtyBirthListSalesDtlRowChanging

        End Sub

    End Class

    Partial Class BirthListCLPtransactionDataTable

    End Class

    Partial Class CLPCustomerAddressDataTable

        Private Sub CLPCustomerAddressDataTable_CLPCustomerAddressRowChanging(ByVal sender As System.Object, ByVal e As CLPCustomerAddressRowChangeEvent) Handles Me.CLPCustomerAddressRowChanging

        End Sub


    End Class

    Partial Class BirthListCancelDataTable

    End Class

End Class



Namespace POSDBDataSetTableAdapters
    
    Partial Class FloatingDetailTableAdapter

    End Class

    Partial Public Class BirthListArticleDtlsTableAdapter
    End Class
End Namespace
