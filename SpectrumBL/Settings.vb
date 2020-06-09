Imports System.Resources
Namespace My
    
    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings
        'Public resourceMgrbl As ResourceManager = ResourceManager.CreateFileBasedResourceManager("MyResource", ".\MyResource", Nothing)
        Private Sub MySettings_SettingsLoaded(ByVal sender As Object, ByVal e As System.Configuration.SettingsLoadedEventArgs) Handles Me.SettingsLoaded
            POSDBConnectionString = DBConnection.ConString
        End Sub
    End Class
End Namespace
