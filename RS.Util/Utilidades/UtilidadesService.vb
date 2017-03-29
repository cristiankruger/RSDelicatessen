Imports System.Security.Cryptography
Public Class UtilidadesService

    Public Function GeraSenhaProvisoria(ByVal tamanho As Integer) As String
        Try
            Dim rdm As New Random()
            Dim vetorString() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray()
            Dim senha As String = ""

            For i As Integer = 0 To tamanho - 1
                senha += vetorString(rdm.Next(0, vetorString.Length))
            Next

            Return senha
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function criptografaSenha(ByVal senha As String) As String
        Try

            'Criamos a instância do Provider sha1
            Dim provider As New SHA1CryptoServiceProvider()
            Dim bytHash As Byte() = Nothing
            Dim hash As String = String.Empty
            bytHash = provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha))
            provider.Clear()
            hash = BitConverter.ToString(bytHash).Replace("-", String.Empty)

            Return hash

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
