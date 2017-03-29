Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data

Public Class DataPessoa
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function VerificaCPFFuncionario(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaCPFFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pCPF", objModeloPessoa.CPF)

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

    Public Function VerificaAlteracaoCPFFuncionario(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoCPFFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloPessoa.IdPessoa)
            cmd.Parameters.AddWithValue("@pCPF", objModeloPessoa.CPF)

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

    Public Function VerificaCPFCliente(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaCPFCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pCPF", objModeloPessoa.CPF)

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

    Public Function VerificaAlteracaoCPFCliente(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoCPFCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloPessoa.IdPessoa)
            cmd.Parameters.AddWithValue("@pCPF", objModeloPessoa.CPF)

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
End Class
