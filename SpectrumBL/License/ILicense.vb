Imports SpectrumCommon
Public Interface ILicense

    Function FetchSupportPhoneFromDB(Sitecode As String) As String
    Function FetchSupportEmailIDFromDB(Sitecode As String) As String

    Function FetchLicenseFromDB(ByVal hardwareKey As String) As LicenseModel
    Function FetchLicenseFromDB(ByVal Sitecode As String, ByVal hardwareKey As String, ByVal TerminalID As String) As LicenseModel
    Function SaveLicenseinDB(ByVal License As LicenseModel) As Boolean
    Function UpdateLicenseInDB(ByVal license As LicenseModel) As Boolean

    Function DecryptKey(ByVal EncryptedKey As String) As String
    Function EncryptKey(ByVal DecryptedKey As String) As String
    Function GetdatafromKey(ByVal LicenseKey As String) As String
    Function ValidateKey(ByVal LicenseKey As String) As Boolean
    Function ValidateHardwareKey(LicenseKey As String) As Boolean
    Function FromHex(ByVal HexString As String) As Byte()
    Function Strip(value As String, characters As String) As String
    Function identifier(wmiClass As String, wmiProperty As String) As String
    Function ValidateCurrentLicense(ByVal SiteCode As String, ByVal HDDKey As String, ByVal TerminalID As String) As LicenseStatus
    Function InstallNewLicense(ByVal License As LicenseModel) As Boolean

    Function CreateTrialLicense(ByVal SiteCode As String, ByVal TerminlID As String, ByVal userCode As String, Optional ByVal OffLineLicenseActivationDuration As Integer = 30) As Boolean
    Function GetEncryptedHDDKey() As String
    Function OnlineLicense(ByVal License As LicenseModel) As Boolean
    Function SaveBackDatedLicenseinDB(ByVal License As LicenseModel) As Boolean
    Function InstallBackDatedLicense(ByVal License As LicenseModel) As Boolean
End Interface
