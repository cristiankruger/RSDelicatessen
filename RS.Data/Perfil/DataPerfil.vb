Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataPerfil
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetPerfil(Optional ByVal IdPerfil As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetPerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If IdPerfil <> 0 Then
                cmd.Parameters.AddWithValue("@pIdPerfil", IdPerfil)
            End If
            cmd.ExecuteNonQuery()

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

    Public Function InserePerfil(ByVal objPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInserePerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pDescricao", objPerfil.Descricao)
            cmd.Parameters.AddWithValue("@pCodigoUsuarioLogado", objUsuarioLogado.IdPessoa)

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

    Public Function VerificaPerfil(ByVal objPerfil As ModeloPerfil) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaPerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPerfil", objPerfil.IdPerfil)
            cmd.Parameters.AddWithValue("@pDescricao", objPerfil.Descricao)
            cmd.ExecuteNonQuery()

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

    Public Sub AlteraPerfil(ByVal objPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraPerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPerfil", objPerfil.IdPerfil)
            cmd.Parameters.AddWithValue("@pDescricao", objPerfil.Descricao)
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub DeletaPerfil(ByVal objPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaPerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPerfil", objPerfil.IdPerfil)
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objUsuarioLogado.IdPessoa)
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
