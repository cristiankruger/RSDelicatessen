Imports Rs.Data
Imports Rs.Modelo
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoMensagemSistema
    Private objDataParametroSistema As New DataParametroSistema

    Public Function GetMensagemSistemaString(ByVal tipo As String, ByVal nome As String) As String
        Try
            Return objDataParametroSistema.GetMensagemSistemaString(tipo, nome)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
