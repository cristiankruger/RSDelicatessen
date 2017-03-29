Imports RS.Data
Imports RS.Modelo
Imports System.Data
Imports RS.Util
Imports System.Collections.Generic

Public Class ServicoParcela
    Private objData As New DataParcela

    Public Function GetParcela(Optional ByVal idPagamento As Integer = Nothing) As List(Of ModeloParcelaDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloParcelaDTO)
            dt = objData.GetParcela(idPagamento, 0)

            For Each row As DataRow In dt.Rows
                Dim objParcela As New ModeloParcelaDTO

                objParcela.StatusEstorno = row("statusEstorno")
                objParcela.DataVencimento = CDate(row("dataVencimento"))
                objParcela.IdParcela = CInt(row("idParcela"))
                If row("dataPagamento").ToString <> "" Then
                    objParcela.DataPagamento = CDate(row("dataPagamento"))
                Else
                    objParcela.DataPagamento = "-"
                End If

                If row("valorParcela").ToString <> "" Then
                    objParcela.ValorParcela = FormatCurrency(row("valorParcela"), 2)
                Else
                    objParcela.ValorParcela = "R$0,00"
                End If

                If row("idTipoPagamento").ToString <> "" Then
                    objParcela.IdTipoPagamento = CInt(row("idTipoPagamento"))
                Else
                    objParcela.IdTipoPagamento = 0
                End If

                If row("descricao").ToString <> "" Then
                    objParcela.Descricao = row("descricao").ToString
                Else
                    objParcela.Descricao = "-"
                End If

                If row("valorParcela").ToString <> "" Then
                    objParcela.Situacao = "Pago"
                Else
                    objParcela.Situacao = "Em Aberto"
                End If

                lista.Add(objParcela)
            Next

            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableParcela(ByVal idPagamento As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoParcela As New ServicoParcela
            Dim itemModeloParcelaDTO As New List(Of ModeloParcelaDTO)
            Dim objJson As New SharedJson

            itemModeloParcelaDTO = objServicoParcela.GetParcela(idPagamento)

            dt.TableName = "TableParcela"

            dt.Columns.Add("Acao")
            dt.Columns.Add("DataVencimento")
            dt.Columns.Add("DataPagamento")
            dt.Columns.Add("ValorParcela")
            dt.Columns.Add("TipoPagamento")
            dt.Columns.Add("Situacao")
            dt.Columns.Add("ParcelasEstornadas")

            For Each item As ModeloParcelaDTO In itemModeloParcelaDTO
                Dim drop As String
                Dim situacao As String
                Dim contador As Integer = 0

                If item.StatusEstorno = True Then
                    contador += 1
                End If

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                           "<ul class='dropdown-menu'>"
                If item.StatusEstorno = True Then
                    drop += "<li><a href='javascript:PopulaParcelaById(" + item.IdParcela.ToString + ");'>Visualizar</a></li>"

                ElseIf item.Situacao = "Pago" Then
                    drop += "<li><a href='javascript:PopulaParcelaById(" + item.IdParcela.ToString + ");'>Visualizar</a></li>" &
                            "<li><a href='javascript:EstornaParcela(" + item.IdParcela.ToString + ");'>Estornar</a></li>"
                Else
                    drop += "<li><a href='javascript:PopulaParcelaById(" + item.IdParcela.ToString + ");'>Pagar Parcela</a></li>"
                End If

                drop += "</ul></div>"

                If item.StatusEstorno = True Then
                    situacao = "Estornado"
                Else
                    situacao = item.Situacao
                End If

                Dim linha() As String = {drop, item.DataVencimento, item.DataPagamento, item.ValorParcela, item.Descricao, situacao, contador}

                dt.Rows.Add(linha)
            Next

            If dt.Rows.Count > 0 Then
                Return objJson.JsonDados(dt)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub InsereParcelas(ByVal objPagamento As ModeloPagamento, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            Dim vencimento = objPagamento.DataInicial
            Dim Nparcelas = objPagamento.NumeroDeParcelas

            For index = 1 To Nparcelas
                Dim objParcela As New ModeloParcela
                objParcela.IdPagamento = objPagamento.IdPagamento
                objParcela.DataVencimento = vencimento
                vencimento = vencimento.AddMonths(1)
                objData.InsereParcela(objParcela, objUsuarioLogado)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function GetParcelaById(ByVal idParcela As Integer) As List(Of ModeloParcelaDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloParcelaDTO)
            dt = objData.GetParcela(0, idParcela)

            For Each row As DataRow In dt.Rows
                Dim objParcela As New ModeloParcelaDTO

                objParcela.DataVencimento = CDate(row("dataVencimento"))
                objParcela.IdParcela = CInt(row("idParcela"))
                If row("dataPagamento").ToString <> "" Then
                    objParcela.DataPagamento = CDate(row("dataPagamento"))
                Else
                    objParcela.DataPagamento = "-"
                End If

                If row("valorParcela").ToString <> "" Then
                    objParcela.ValorParcela = FormatCurrency(row("valorParcela"), 2)
                Else
                    objParcela.ValorParcela = ""
                End If

                If row("idTipoPagamento").ToString <> "" Then
                    objParcela.IdTipoPagamento = CInt(row("idTipoPagamento"))
                Else
                    objParcela.IdTipoPagamento = 0
                End If

                If row("descricao").ToString <> "" Then
                    objParcela.Descricao = row("descricao").ToString
                Else
                    objParcela.Descricao = "-"
                End If

                lista.Add(objParcela)
            Next

            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableParcelaById(ByVal idParcela As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoParcela As New ServicoParcela
            Dim itemModeloParcelaDTO As New List(Of ModeloParcelaDTO)
            Dim objJson As New SharedJson

            itemModeloParcelaDTO = objServicoParcela.GetParcelaById(idParcela)

            dt.TableName = "TableParcelaById"

            dt.Columns.Add("IdParcela")
            dt.Columns.Add("DataVencimento")
            dt.Columns.Add("DataPagamento")
            dt.Columns.Add("ValorParcela")
            dt.Columns.Add("TipoPagamento")
            'dt.Columns.Add("Estorno")

            For Each item As ModeloParcelaDTO In itemModeloParcelaDTO

                Dim linha() As String = {item.IdParcela, item.DataVencimento, item.DataPagamento, item.ValorParcela, item.IdTipoPagamento}

                dt.Rows.Add(linha)
            Next

            If dt.Rows.Count > 0 Then
                Return objJson.JsonDados(dt)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraParcela(ByVal objModeloParcela As ModeloParcela, ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraParcela(objModeloParcela, objModeloPagamento, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaUltimaParcela(ByVal idPagamento As Integer, ByVal idParcela As Integer) As Integer
        Try
            Dim quantidade As Integer
            quantidade = objData.VerificaUltimaParcela(idPagamento, idParcela)
            Return quantidade
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub EstornaParcela(Optional ByVal idpedido As Integer = 0, Optional ByVal idParcela As Integer = 0)
        Try
            objData.EstornaParcela(idpedido, idParcela)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraEstornadas(ByVal idPagamentoAntigo As Integer, ByVal idPagamentoNovo As Integer, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraEstornadas(idPagamentoAntigo, idPagamentoNovo, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaParcelasPagas(ByVal idPedido As Integer) As Integer
        Try
            Dim ParcelasPagas As Integer
            ParcelasPagas = objData.VerificaParcelasPagas(idPedido)
            Return ParcelasPagas
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
