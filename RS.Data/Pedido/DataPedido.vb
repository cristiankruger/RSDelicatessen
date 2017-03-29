Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class DataPedido
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetPedido(Optional ByVal idPedido As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idPedido = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPedido", idPedido)
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

    Public Sub DeletaPedido(ByVal objModeloPedido As ModeloPedido, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPedido", objModeloPedido.IdPedido)
            cmd.Parameters.AddWithValue("@pIdFuncionarioLogado", objFuncionarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function InserePedido(ByVal objModeloPedido As ModeloPedido, ByVal objFuncionarioLogado As ModeloFuncionario) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInserePedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pDataPedido", objModeloPedido.DataPedido)
            If Not objModeloPedido.DataValidade = Nothing Then
                cmd.Parameters.AddWithValue("@pDataValidade", objModeloPedido.DataValidade)
            End If
            If Not objModeloPedido.Descricao = Nothing Then
                cmd.Parameters.AddWithValue("@pDescricao", objModeloPedido.Descricao)
            End If
            cmd.Parameters.AddWithValue("@pIdCliente", objModeloPedido.IdCliente)
            cmd.Parameters.AddWithValue("@pIdSituacao", objModeloPedido.IdSituacao)

            Dim idPedido As Integer
            idPedido = cmd.ExecuteScalar()

            trans.Commit()
            Return idPedido
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub AlteraPedido(objModeloPedido As ModeloPedido, objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pDataPedido", objModeloPedido.DataPedido)
            cmd.Parameters.AddWithValue("@pDataValidade", objModeloPedido.DataValidade)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloPedido.Descricao)
            cmd.Parameters.AddWithValue("@pIdCliente", objModeloPedido.IdCliente)
            cmd.Parameters.AddWithValue("@pIdSituacao", objModeloPedido.IdSituacao)
            cmd.Parameters.AddWithValue("@pIdPedido", objModeloPedido.IdPedido)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraStatusPedido(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraStatusPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdPedido", objModeloPagamento.IdPedido)

            cmd.ExecuteNonQuery()
       
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function GetRelatorioPedidos(ByVal IdSituacao As Integer, ByVal NomeCliente As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloPedidoDTO)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spRelatorioPedidos", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not IdSituacao = Nothing Then
                cmd.Parameters.AddWithValue("@pIdSituacao", IdSituacao)
            End If
            If Not NomeCliente = Nothing Then
                cmd.Parameters.AddWithValue("@pNomeCliente", NomeCliente)
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

            Dim lista As New List(Of ModeloPedidoDTO)
            For Each row As DataRow In dt.Rows
                Dim objPedido As New ModeloPedidoDTO
                objPedido.DataPedido = CDate(row("dataPedido"))
                objPedido.Descricao = row("descricao").ToString
                objPedido.NomeCliente = row("nomeCliente").ToString
                objPedido.NomeFuncionario = row("nomeFuncionario").ToString
                objPedido.TelefoneCliente = row("telefone").ToString
                objPedido.Email = row("email").ToString
                objPedido.SituacaoPedido = row("situacaoPedido").ToString
                lista.Add(objPedido)
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
