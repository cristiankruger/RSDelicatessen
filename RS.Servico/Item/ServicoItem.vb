Imports RS.Data
Imports RS.Modelo
Imports System.Data
Imports System.Collections.Generic
Imports RS.Util

Public Class ServicoItem
    Private objData As New DataItem

    Public Function GetItemPedido(Optional ByVal idPedido As Integer = Nothing) As List(Of ModeloItemDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloItemDTO)
            dt = objData.GetItem(idPedido)

            For Each row As DataRow In dt.Rows
                Dim objItem As New ModeloItemDTO
                objItem.IdItem = CInt(row("idItem"))
                objItem.IdPedido = CInt(row("idPedido"))
                objItem.IdProduto = CInt(row("idProduto"))
                objItem.Nome = row("produtoCategoria").ToString
                objItem.Quantidade = row("quantidade").ToString
                objItem.Status = row("status").ToString
                objItem.Valor = FormatCurrency(row("subTotal"), 2).Replace("R$", "")

                lista.Add(objItem)
            Next

            Return lista

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableItem(ByVal pLista As List(Of ModeloItemDTO), ByVal Bool As Boolean) As String
        Try

            Dim html As String
            html = "'<table id='tbItemPedido' class='table table-striped table-bordered'> " &
                "<thead> " &
                "<tr> " &
                "<th style='width: 68.0909091234207px;'></th> " &
                "<th>Item</th> " &
                "<th>Quantidade</th> " &
                "<th>Valor</th> " &
                "</tr> " &
                "</thead> " &
                "<tbody>"

            For Each objModeloItemDTO As ModeloItemDTO In pLista
                If Bool = True Then
                    html += "<tr>" &
                    "<td> <a class='btn btn-primary margin hidden'></a>" &
                    "<td>" + objModeloItemDTO.Nome + "</td>" &
                    "<td>" + objModeloItemDTO.Quantidade.ToString + "</td>" &
                    "<td>" + FormatCurrency(objModeloItemDTO.Valor, 2).Replace("R$", "") + "</td>" &
                    "</tr>"
                Else
                    html += "<tr>" &
                    "<td> <a href='javascript:ExcluiItemPedido(" + objModeloItemDTO.IdItem.ToString + ");' class='btn btn-primary margin'>Excluir</a>" &
                    "<td>" + objModeloItemDTO.Nome + "</td>" &
                    "<td>" + objModeloItemDTO.Quantidade.ToString + "</td>" &
                    "<td>" + FormatCurrency(objModeloItemDTO.Valor, 2).Replace("R$", "") + "</td>" &
                    "</tr>"
                End If

            Next

            html += "</tbody> " &
            "</table> "

            Return html

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function verificaListaVazia(ByVal pLista As List(Of ModeloItemDTO)) As Boolean
        Try
            Dim verifica As Boolean

            If pLista Is Nothing Then
                verifica = False
            ElseIf pLista.Count > 0 Then
                verifica = True
            Else
                verifica = False
            End If

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub InsereItemPedido(ByVal objModeloItem As ModeloItemDTO, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.InsereItemPedido(objModeloItem, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub DeletaItemPedido(ByVal objModeloItem As ModeloItemDTO, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaItemPedido(objModeloItem, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class
