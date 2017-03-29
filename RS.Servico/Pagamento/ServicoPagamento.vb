Imports RS.Data
Imports RS.Modelo
Imports System.Data

Public Class ServicoPagamento
    Private objData As New DataPagamento

    Public Function GetPagamento(ByVal idPedido As Integer) As ModeloPagamento
        Try
            Dim objPagamento As New ModeloPagamento
            Dim dt As New DataTable()

            dt = objData.GetPagamento(idPedido)

            If dt.Rows.Count > 0 Then
                objPagamento.DataInicial = dt.Rows(0).Item("dataInicial").ToString
                objPagamento.Desconto = CInt(dt.Rows(0).Item("desconto"))
                objPagamento.NumeroDeParcelas = CInt(dt.Rows(0).Item("numeroDeParcelas"))
                objPagamento.Observacao = dt.Rows(0).Item("observacao").ToString
                objPagamento.IdPagamento = CInt(dt.Rows(0).Item("idPagamento"))
                objPagamento.Status = CInt(dt.Rows(0).Item("status"))
                objPagamento.SaldoPago = FormatCurrency(dt.Rows(0).Item("saldoPago"), 2).Replace("R$", "")
            Else
                objPagamento = Nothing
            End If
            
            Return objPagamento
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InserePagamento(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario) As Integer
        Try
            Dim idPagamento As Integer
            idPagamento = objData.InserePagamento(objModeloPagamento, objFuncionarioLogado)
            Return idPagamento
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub DeletaPagamento(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaPagamento(objModeloPagamento, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
