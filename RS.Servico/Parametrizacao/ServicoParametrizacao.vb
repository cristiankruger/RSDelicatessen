Imports Rs.Data
Imports System.Data

Public Class ServicoParametrizacao
    Dim objBanco As New DataParametrizacao
    Public Function getMenu(ByVal idPerfil As Integer) As DataTable
        Try
            Dim dt As DataTable
            dt = objBanco.getMenu(idPerfil)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
