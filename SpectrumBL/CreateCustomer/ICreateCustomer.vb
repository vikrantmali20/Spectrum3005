Imports SpectrumCommon
Public Interface ICreateCustomer
    Function GetCustomerMaster() As GetCustomerMasterResponse

    Function GetCustomerGroup() As GetCustomerMasterResponse

    Function GetCustomerInfo(ByVal clpProgramId As String, ByVal PrimaryKeyNo As String, ByVal KeyIsMobileNo As Boolean) As GetCustomerResponse

    Function SaveCustomerInfo(ByRef request As SaveCustomerRequest) As Boolean
End Interface
