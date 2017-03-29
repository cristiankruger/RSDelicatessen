Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataDataComemorativa
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetDataComemorativa(ByVal idPessoa As Integer) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", idPessoa)

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

    Public Function GetDataComemorativaById(ByVal idDataComemorativa As Integer) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetDataComemorativaById", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdDataComemorativa", idDataComemorativa)

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

    Public Sub InsereDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloDataComemorativa.IdPessoa)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloDataComemorativa.Descricao)
            cmd.Parameters.AddWithValue("@pData", objModeloDataComemorativa.DataComemorativa)
            cmd.Parameters.AddWithValue("@pIdPessoaLogada", objFuncionarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdDataComemorativa", objModeloDataComemorativa.IdDataComemorativa)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloDataComemorativa.Descricao)
            cmd.Parameters.AddWithValue("@pData", objModeloDataComemorativa.DataComemorativa)
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

    Public Sub DeleteDataComemorativa(objModeloDataComemorativa As ModeloDataComemorativa, objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdDataComemorativa", objModeloDataComemorativa.IdDataComemorativa)
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

    Public Function VerificaDataComemorativa(objModeloDataComemorativa As ModeloDataComemorativa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pData", objModeloDataComemorativa.DataComemorativa)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloDataComemorativa.Descricao)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloDataComemorativa.IdPessoa)

            Dim verifica As Boolean
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

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

    Public Function VerificaAlteracaoDataComemorativa(objModeloDataComemorativa As ModeloDataComemorativa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pData", objModeloDataComemorativa.DataComemorativa)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloDataComemorativa.Descricao)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloDataComemorativa.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdDataComemorativa", objModeloDataComemorativa.IdDataComemorativa)

            Dim verifica As Boolean
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

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
End Class
