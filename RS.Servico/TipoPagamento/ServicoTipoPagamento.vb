Imports RS.Modelo
Imports RS.Data
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoTipoPagamento
    Private objData As New DataTipoPagamento

    Public Function GetTipoPagamento(Optional ByVal idTipoPagamento As Integer = Nothing) As List(Of ModeloTipoPagamento)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloTipoPagamento)

            dt = objData.GetTipoPagamento(idTipoPagamento)

            For Each row As DataRow In dt.Rows
                Dim objTipoPagamento As New ModeloTipoPagamento
                objTipoPagamento.Descricao = row("descricao").ToString
                objTipoPagamento.IdTipoPagamento = CInt(row("idTipoPagamento"))
                objTipoPagamento.Status = CBool(row("status"))

                lista.Add(objTipoPagamento)
            Next

            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
