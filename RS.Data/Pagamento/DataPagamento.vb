Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data

Public Class DataPagamento
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetPagamento(ByVal idPedido As Integer) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetPagamento", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPedido", idPedido)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            trans.Commit()
            Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function InserePagamento(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInserePagamento", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pDataInicial", objModeloPagamento.DataInicial)
            cmd.Parameters.AddWithValue("@pDesconto", objModeloPagamento.Desconto)
            cmd.Parameters.AddWithValue("@pIdPedido", objModeloPagamento.IdPedido)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloPagamento.IdPessoa)
            cmd.Parameters.AddWithValue("@pNumeroDeParcelas", objModeloPagamento.NumeroDeParcelas)
            cmd.Parameters.AddWithValue("@pObservacao", objModeloPagamento.Observacao)
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)

            Dim idPagamento As Integer
            idPagamento = cmd.ExecuteScalar()

            trans.Commit()
            Return idPagamento
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub DeletaPagamento(ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaPagamento", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPagamento", objModeloPagamento.IdPagamento)
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

End Class
