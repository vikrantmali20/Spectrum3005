Imports SpectrumCommon
Public Interface ICreateNewCustomer
    Function GetCustomerMaster() As GetCustomerMasterResponse

    Function GetCustomerInfo(ByVal clpProgramId As String, ByVal PrimaryKeyNo As String, ByVal KeyIsMobileNo As Boolean) As GetCustomerResponse

    Function SaveCustomerInfo(ByRef request As SaveCustomerRequest, Optional ByVal dtDeleteAddress As DataTable = Nothing, Optional ByVal dtDeleteContact As DataTable = Nothing) As Boolean
End Interface
