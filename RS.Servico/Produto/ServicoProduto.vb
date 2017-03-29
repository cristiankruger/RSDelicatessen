Imports RS.Modelo
Imports RS.Data
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoProduto
    Private objData As New DataProduto

    Public Function GetProduto(Optional ByVal idProduto As Integer = Nothing) As List(Of ModeloProdutoCategoriaDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            dt = objData.GetProduto(idProduto)

            For Each row As DataRow In dt.Rows
                Dim objProduto As New ModeloProdutoCategoriaDTO
                objProduto.IdCategoria = CInt(row("idCategoria"))
                objProduto.IdProduto = CInt(row("idProduto"))
                objProduto.Foto = row("foto").ToString
                objProduto.Nome = row("nome").ToString
                objProduto.DataFabricacao = CDate(row("dataFabricacao"))
                objProduto.DataValidade = CDate(row("dataValidade"))
                objProduto.StatusCategoria = CBool(row("statusCategoria"))
                objProduto.Status = CBool(row("status"))
                objProduto.Categoria = row("categoria").ToString
                objProduto.ProdutoCategoria = row("produtoCategoria").ToString

                objProduto.Valor = String.Format("{0 :C}", row("valor"))
                If row("descricao").ToString <> "" Then
                    objProduto.Descricao = row("descricao").ToString
                Else
                    objProduto.Descricao = "-"
                End If

                lista.Add(objProduto)
            Next

            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetProdutoById(ByVal idProduto As Integer) As ModeloProdutoCategoriaDTO
        Try
            Dim dt As New DataTable()

            dt = objData.GetProduto(idProduto)

            Dim objProduto As New ModeloProdutoCategoriaDTO

            objProduto.IdCategoria = CInt(dt.Rows(0).Item("idCategoria"))
            objProduto.IdProduto = CInt(dt.Rows(0).Item("idProduto"))
            objProduto.Valor = FormatCurrency(dt.Rows(0).Item("valor"), 2).Replace("R$", "")
            objProduto.Descricao = dt.Rows(0).Item("descricao").ToString
            objProduto.Foto = dt.Rows(0).Item("foto").ToString
            objProduto.Nome = dt.Rows(0).Item("nome").ToString
            objProduto.DataFabricacao = CDate(dt.Rows(0).Item("dataFabricacao"))
            objProduto.DataValidade = CDate(dt.Rows(0).Item("dataValidade"))
            objProduto.StatusCategoria = CBool(dt.Rows(0).Item("statusCategoria"))
            objProduto.Status = CBool(dt.Rows(0).Item("status"))
            objProduto.Categoria = dt.Rows(0).Item("categoria").ToString

            Return objProduto
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableProdutoById(ByVal idProduto As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoProduto As New ServicoProduto
            Dim itemModeloProdutoCategoriaDTO As New ModeloProdutoCategoriaDTO
            Dim objJson As New SharedJson

            itemModeloProdutoCategoriaDTO = objServicoProduto.GetProdutoById(idProduto)

            dt.TableName = "TableProdutoById"

            dt.Columns.Add("IdCategoria")
            dt.Columns.Add("idProduto")
            dt.Columns.Add("Valor")
            dt.Columns.Add("Descricao")
            dt.Columns.Add("Foto")
            dt.Columns.Add("Nome")
            dt.Columns.Add("DataFabricacao")
            dt.Columns.Add("DataValidade")
            dt.Columns.Add("StatusCategoria")
            dt.Columns.Add("Status")
            dt.Columns.Add("Categoria")

            Dim linha() As String = {itemModeloProdutoCategoriaDTO.IdCategoria, itemModeloProdutoCategoriaDTO.IdProduto, itemModeloProdutoCategoriaDTO.Valor,
                                     itemModeloProdutoCategoriaDTO.Descricao, itemModeloProdutoCategoriaDTO.Foto, itemModeloProdutoCategoriaDTO.Nome,
                                     itemModeloProdutoCategoriaDTO.DataFabricacao, itemModeloProdutoCategoriaDTO.DataValidade, itemModeloProdutoCategoriaDTO.StatusCategoria,
                                     itemModeloProdutoCategoriaDTO.Status, itemModeloProdutoCategoriaDTO.Categoria}
            dt.Rows.Add(linha)

            If dt.Rows.Count > 0 Then
                Return objJson.JsonDados(dt)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableProduto() As String
        Try
            Dim dt As New DataTable
            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            Dim objServicoProduto As New ServicoProduto
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Nome")
            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("Categoria")
            dtRetorno.Columns.Add("Valor")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            lista = objServicoProduto.GetProduto()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableProduto"

            For Each objModeloProduto As ModeloProdutoCategoriaDTO In lista

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href='javascript:PopulaProduto(" + objModeloProduto.IdProduto.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiProduto(" + objModeloProduto.IdProduto.ToString + ");'>Excluir</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {objModeloProduto.Nome, objModeloProduto.Descricao, objModeloProduto.Categoria, objModeloProduto.Valor, drop}
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

    Public Sub DeletaProduto(ByVal objModeloProduto As ModeloProduto, ByVal ObjFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaProduto(objModeloProduto, ObjFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function verificaProduto(ByVal objModeloProduto As ModeloProduto) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objData.VerificaProduto(objModeloProduto)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub InsereProduto(ByVal objModeloProduto As ModeloProduto, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.InsereProduto(objModeloProduto, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaAlteracaoProduto(ByVal objModeloProduto As ModeloProduto)
        Try
            Dim verifica As Boolean
            verifica = objData.VerificaAlteracaoProduto(objModeloProduto)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraProduto(ByVal objModeloProduto As ModeloProduto, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraProduto(objModeloProduto, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function GetRelatorioProduto(ByVal Nome As String, ByVal Categoria As String, ByVal Descricao As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloProdutoCategoriaDTO)
        Try
            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            lista = objData.GetRelatorioProduto(Nome, Categoria, Descricao, DataInicial, DataFinal)
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetRelatorioProdutosVendidos(ByVal IdCategoria As Integer, ByVal DescricaoCategoria As String, ByVal DescricaoProduto As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloProdutoCategoriaDTO)
        Try
            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            lista = objData.GetRelatorioProdutosVendidos(IdCategoria, DescricaoCategoria, DescricaoProduto, DataInicial, DataFinal)
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class