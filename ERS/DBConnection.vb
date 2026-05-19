Imports System.Configuration
Imports MySql.Data.MySqlClient

Public NotInheritable Class DBConnection
    Private Sub New()
    End Sub

    Public Shared Function GetConnection() As MySqlConnection
        Dim connStr As String = ConfigurationManager.ConnectionStrings("TwoCRentals").ConnectionString
        Return New MySqlConnection(connStr)
    End Function
End Class
