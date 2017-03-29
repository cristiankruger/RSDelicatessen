Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoPedido
    Private objData As New DataPedido

    Public Function GetPedido(Optional ByVal IdPedido As Integer = Nothing) As List(Of ModeloPedidoDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloPedidoDTO)
            dt = objData.GetPedido(IdPedido)

            For Each row As DataRow In dt.Rows
                Dim objPedido As New ModeloPedidoDTO
                objPedido.DataPedido = CDate(row("dataPedido"))
                objPedido.IdCliente = CInt(row("idCliente"))
                objPedido.IdFuncionario = CInt(row("idFuncionario"))
                objPedido.IdPedido = CInt(row("idPedido"))
                objPedido.IdSituacao = CInt(row("idSituacao"))
                objPedido.NomeCliente = row("nomeCliente").ToString
                objPedido.NomeFuncionario = row("nomeFuncionario").ToString
                objPedido.SituacaoPedido = row("situacaoPedido").ToString
                objPedido.Status = CBool(row("status"))
                objPedido.TelefoneCliente = row("telefoneCliente").ToString
                objPedido.ValorPedido = String.Format("{0:C}", row("valorPedido"))
                If row("dataValidade").ToString <> "" Then
                    objPedido.DataValidade = CDate(row("dataValidade"))
                Else
                    objPedido.DataValidade = "-"
                End If
                If row("descricao").ToString <> "" Then
                    objPedido.Descricao = row("Descricao").ToString
                Else
                    objPedido.Descricao = "-"
                End If

                lista.Add(objPedido)
            Next

            Return lista

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TablePedido() As String
        Try
            Dim dt As New DataTable
            Dim lista As New List(Of ModeloPedidoDTO)
            Dim objServicoPedido As New ServicoPedido
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("DataPedido")
            dtRetorno.Columns.Add("IdPedido")
            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("NomeCliente")
            dtRetorno.Columns.Add("TelefoneCliente")
            dtRetorno.Columns.Add("NomeFuncionario")
            dtRetorno.Columns.Add("SituacaoPedido")
            dtRetorno.Columns.Add("ValorPedido")
            dtRetorno.Columns.Add("IdSituacao")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String = ""

            lista = objServicoPedido.GetPedido()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TablePedido"

            For Each objModeloPedidoDTO As ModeloPedidoDTO In lista

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                           "<ul class='dropdown-menu'>"

                If objModeloPedidoDTO.IdSituacao = 1 Then
                    'situação orçado
                    drop += "<li><a href='javascript:limparCampos();PopulaDadosAlteracao(" + objModeloPedidoDTO.IdPedido.ToString + "); '>Editar</a></li>" &
                            "<li><a href='javascript:ExcluiPedido(" + objModeloPedidoDTO.IdPedido.ToString + ");'>Excluir</a></li>"
                
                ElseIf objModeloPedidoDTO.IdSituacao = 2 Then
                    'aguardando pagamento
                    drop += "<li><a href='javascript:limparCampos(); PopulaDadosAlteracao(" + objModeloPedidoDTO.IdPedido.ToString + ");'>Visualizar</a></li>" &
                            "<li><a href='javascript:EstornaPedido(" + objModeloPedidoDTO.IdPedido.ToString + ");'>Estornar Pedido</a></li>" &
                            "<li><a  href='../Pagamento/Pagamento.aspx?Pagamento=" + objModeloPedidoDTO.IdPedido.ToString + "'>Pagamento</a></li>"

                ElseIf objModeloPedidoDTO.IdSituacao = 3 Then
                    'situação Finalizado
                    drop += "<li><a href='javascript:limparCampos(); PopulaDadosAlteracao(" + objModeloPedidoDTO.IdPedido.ToString + ");'>Visualizar</a></li>" &
                            "<li><a  href='../Pagamento/Pagamento.aspx?Pagamento=" + objModeloPedidoDTO.IdPedido.ToString + "'>Pagamento</a></li>"
                Else
                    'situacao cancelado
                    drop += "<li><a href='javascript:limparCampos(); PopulaDadosAlteracao(" + objModeloPedidoDTO.IdPedido.ToString + ");'>Visualizar</a></li>" &
                            "<li><a  href='../Pagamento/Pagamento.aspx?Pagamento=" + objModeloPedidoDTO.IdPedido.ToString + "'>Pagamento</a></li>"
                End If

                drop += "</ul></div>"

                Dim linha() As String = {objModeloPedidoDTO.DataPedido, objModeloPedidoDTO.IdPedido, objModeloPedidoDTO.Descricao, objModeloPedidoDTO.NomeCliente,
                                         objModeloPedidoDTO.TelefoneCliente, objModeloPedidoDTO.NomeFuncionario, objModeloPedidoDTO.SituacaoPedido, objModeloPedidoDTO.ValorPedido,
                                         objModeloPedidoDTO.IdSituacao, drop}
                dtRetorno.Rows.Add(linha)
            Next

            If dtRetorno.Rows.Count > 0 Then
                Return objJson.JsonDados(dtRetorno)
            Else
                Return "0"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetPedidoById(ByVal idPedido As Integer) As ModeloPedidoDTO
        Try
            Dim dt As New DataTable()
            Dim objModeloPedido As New ModeloPedidoDTO
            dt = objData.GetPedido(idPedido)

            objModeloPedido.DataPedido = CDate(dt.Rows(0).Item("dataPedido"))
            If dt.Rows(0).Item("dataValidade").ToString <> "" And CInt(dt.Rows(0).Item("idSituacao")) = 1 Then
                objModeloPedido.DataValidade = CDate(dt.Rows(0).Item("dataValidade"))
            Else
                objModeloPedido.DataValidade = ""
            End If
            objModeloPedido.Descricao = dt.Rows(0).Item("descricao").ToString
            objModeloPedido.IdCliente = CInt(dt.Rows(0).Item("idCliente"))
            objModeloPedido.IdPedido = CInt(dt.Rows(0).Item("idPedido"))
            If CInt(dt.Rows(0).Item("idSituacao")) <> 1 Then
                objModeloPedido.IdSituacao = 2
            Else
                objModeloPedido.IdSituacao = CInt(dt.Rows(0).Item("idSituacao"))
            End If

            objModeloPedido.ValorPedido = FormatCurrency(dt.Rows(0).Item("ValorPedido"), 2).Replace("R$", "")

            Return objModeloPedido
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub DeletaPedido(ByVal objModeloPedido As ModeloPedido, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaPedido(objModeloPedido, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function InserePedido(ByVal objModeloPedido As ModeloPedido, ByVal objFuncionarioLogado As ModeloFuncionario) As Integer
        Try
            Dim idPedido As Integer
            idPedido = objData.InserePedido(objModeloPedido, objFuncionarioLogado)
            Return idPedido
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraPedido(objModeloPedido As ModeloPedido, objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraPedido(objModeloPedido, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraStatusPedido(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraStatusPedido(objModeloPagamento, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function GetRelatorioPedidos(ByVal IdSituacao As Integer, ByVal NomeCliente As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloPedidoDTO)
        Try
            Dim lista As List(Of ModeloPedidoDTO)
            lista = objData.GetRelatorioPedidos(IdSituacao, NomeCliente, DataInicial, DataFinal)
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
