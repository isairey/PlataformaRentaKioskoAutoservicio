Imports System.Security.Cryptography
Imports System.Text

Public NotInheritable Class HashHelper
    Private Sub New()
    End Sub

    Public Shared Function ComputeSHA256(plainText As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText))
            Dim sb As New StringBuilder()
            For Each b As Byte In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function
End Class
