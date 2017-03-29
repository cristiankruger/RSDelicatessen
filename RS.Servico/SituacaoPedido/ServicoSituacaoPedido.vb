Imports RS.Data
Imports RS.Modelo
Imports System.Collections.Generic

Public Class ServicoSituacaoPedido
    Private objData As New DataSituacaoPedido

    Public Function GetSituacaoPedido() As List(Of ModeloSituacaoPedido)
        Try
            Dim lista As New List(Of ModeloSituacaoPedido)
            lista = objData.GetSituacaopedido()
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
