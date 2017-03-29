Imports System.Data
Imports RS.Modelo
Imports System.Data.SqlClient

Public Class DataParcela
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetParcela(Optional ByVal idPagamento As Integer = 0, Optional ByVal idParcela As Integer = 0) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetParcela", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idPagamento = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPagamento", idPagamento)
            End If
            If Not idParcela = Nothing Then
                cmd.Parameters.AddWithValue("@pIdParcela", idParcela)
            End If
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

    Public Sub InsereParcela(ByVal objParcela As ModeloParcela, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereParcela", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdPagamento", objParcela.IdPagamento)
            cmd.Parameters.AddWithValue("@pDataVencimento", objParcela.DataVencimento)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraParcela(ByVal objModeloParcela As ModeloParcela, ByVal objModeloPagamento As ModeloPagamento, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraParcela", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdParcela", objModeloParcela.IdParcela)
            cmd.Parameters.AddWithValue("@pDataPagamento", objModeloParcela.DataPagamento)
            cmd.Parameters.AddWithValue("@pValorParcela", objModeloParcela.ValorParcela)
            cmd.Parameters.AddWithValue("@pIdTipoPagamento", objModeloParcela.IdTipoPagamento)
            cmd.Parameters.AddWithValue("@pIdPagamento", objModeloPagamento.IdPagamento)
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

    Public Function VerificaUltimaParcela(ByVal idPagamento As Integer, ByVal idParcela As Integer) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaUltimaParcela", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPagamento", idPagamento)
            cmd.Parameters.AddWithValue("@pIdParcela", idParcela)

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()

            trans.Commit()
            Return CInt(dt.Rows(0).Item("parcelasEmAberto"))
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub EstornaParcela(Optional ByVal idpedido As Integer = 0, Optional ByVal idParcela As Integer = 0)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spEstornaParcelaPaga", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idpedido = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPedido", idpedido)
            End If
            If Not idParcela = Nothing Then
                cmd.Parameters.AddWithValue("@pIdParcela", idParcela)
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

    Public Sub AlteraEstornadas(ByVal idPagamentoAntigo As Integer, ByVal idPagamentoNovo As Integer, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraEstornadas", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPagamentoAntigo", idPagamentoAntigo)
            cmd.Parameters.AddWithValue("@pIdPagamentoNovo", idPagamentoNovo)
            cmd.Parameters.AddWithValue("@pIdPessoa", objFuncionarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function VerificaParcelasPagas(ByVal idPedido As Integer) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaParcelasPagas", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPedido", idPedido)

            Dim ParcelasPagas As Integer
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()

            ParcelasPagas = dt.Rows(0).Item("parcelasPagas")

            trans.Commit()
            Return ParcelasPagas
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function
End Class
