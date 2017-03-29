Imports RS.Modelo
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class DataProduto
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetProduto(Optional ByVal idProduto As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idProduto = Nothing Then
                cmd.Parameters.AddWithValue("@pIdProduto", idProduto)
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            trans.Commit()
            Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub InsereProduto(ByVal objModeloProduto As ModeloProduto, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloProduto.IdCategoria)
            cmd.Parameters.AddWithValue("@pValor", objModeloProduto.Valor)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloProduto.Descricao)
            cmd.Parameters.AddWithValue("@pNome", objModeloProduto.Nome)
            cmd.Parameters.AddWithValue("@pFoto", objModeloProduto.Foto)
            cmd.Parameters.AddWithValue("@pDataFabricacao", objModeloProduto.DataFabricacao)
            cmd.Parameters.AddWithValue("@pDataValidade", objModeloProduto.DataValidade)

            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function VerificaProduto(ByVal objModeloProduto As ModeloProduto) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pNome", objModeloProduto.Nome)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloProduto.IdCategoria)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim verifica As Boolean

            If dt.Rows.Count > 0 Then
                verifica = True
            Else
                verifica = False
            End If

            trans.Commit()
            Return verifica
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function VerificaAlteracaoProduto(ByVal objModeloProduto As ModeloProduto) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pNome", objModeloProduto.Nome)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloProduto.IdCategoria)
            cmd.Parameters.AddWithValue("@pIdProduto", objModeloProduto.IdProduto)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim verifica As Boolean

            If dt.Rows.Count > 0 Then
                verifica = True
            Else
                verifica = False
            End If

            trans.Commit()
            Return verifica
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub AlteraProduto(ByVal objModeloProduto As ModeloProduto, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", ObjUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloProduto.IdCategoria)
            cmd.Parameters.AddWithValue("@pIdProduto", objModeloProduto.IdProduto)
            cmd.Parameters.AddWithValue("@pValor", objModeloProduto.Valor)
            cmd.Parameters.AddWithValue("@pNome", objModeloProduto.Nome)
            cmd.Parameters.AddWithValue("@pDataFabricacao", objModeloProduto.DataFabricacao)
            cmd.Parameters.AddWithValue("@pDataValidade", objModeloProduto.DataValidade)
            If Not objModeloProduto.Descricao Is Nothing Then
                cmd.Parameters.AddWithValue("@pDescricao", objModeloProduto.Descricao)
            End If
            If Not objModeloProduto.Foto Is Nothing Then
                cmd.Parameters.AddWithValue("@pFoto", objModeloProduto.Foto)
            End If

            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub DeletaProduto(ByVal objModeloProduto As ModeloProduto, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdProduto", objModeloProduto.IdProduto)
            cmd.Parameters.AddWithValue("@pIdPessoa", ObjUsuarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function GetRelatorioProduto(ByVal Nome As String, ByVal Categoria As String, ByVal Descricao As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloProdutoCategoriaDTO)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spRelatorioProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not Nome = Nothing Then
                cmd.Parameters.AddWithValue("@pNomeProduto", Nome)
            End If
            If Not Categoria = Nothing Then
                cmd.Parameters.AddWithValue("@pCategoria", Categoria)
            End If
            If Not Descricao = Nothing Then
                cmd.Parameters.AddWithValue("@pDescricao", Descricao)
            End If
            If Not DataInicial = Nothing Then
                cmd.Parameters.AddWithValue("@pDataInicial", DataInicial)
            End If
            If Not DataFinal = Nothing Then
                cmd.Parameters.AddWithValue("@pDataFinal", DataFinal)
            End If

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()

            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            For Each row As DataRow In dt.Rows
                Dim objProduto As New ModeloProdutoCategoriaDTO
                objProduto.Nome = row("nome").ToString
                objProduto.Descricao = row("descricao").ToString
                objProduto.Valor = String.Format("{0:C}", row("valor"))
                objProduto.DataFabricacao = CDate(row("dataFabricacao"))
                objProduto.DataValidade = CDate(row("dataValidade"))
                objProduto.Categoria = row("categoria").ToString

                lista.Add(objProduto)
            Next

            trans.Commit()
            Return lista
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function GetRelatorioProdutosVendidos(ByVal IdCategoria As Integer, ByVal DescricaoCategoria As String, ByVal DescricaoProduto As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloProdutoCategoriaDTO)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spRelatorioProdutosVendidos", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not IdCategoria = Nothing Then
                cmd.Parameters.AddWithValue("@pIdCategoria", IdCategoria)
            End If
            If Not DescricaoCategoria = Nothing Then
                cmd.Parameters.AddWithValue("@pDescricaoCategoria", DescricaoCategoria)
            End If
            If Not DescricaoProduto = Nothing Then
                cmd.Parameters.AddWithValue("@pDescricaoProduto", DescricaoProduto)
            End If
            If Not DataInicial = Nothing Then
                cmd.Parameters.AddWithValue("@pDataInicial", DataInicial)
            End If
            If Not DataFinal = Nothing Then
                cmd.Parameters.AddWithValue("@pDataFinal", DataFinal)
            End If

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()

            Dim lista As New List(Of ModeloProdutoCategoriaDTO)
            For Each row As DataRow In dt.Rows
                Dim objProduto As New ModeloProdutoCategoriaDTO
                objProduto.Nome = row("nome").ToString
                objProduto.Descricao = row("descricaoProduto").ToString
                objProduto.Categoria = row("descricaoCategoria").ToString
                objProduto.Valor = FormatCurrency(row("valor"), 2)
                objProduto.QuantidadeVendida = CInt(row("quantidadeVendida"))
                objProduto.ValorTotal = FormatCurrency(row("valorTotal"), 2)
                lista.Add(objProduto)
            Next

            trans.Commit()
            Return lista
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

End Class
