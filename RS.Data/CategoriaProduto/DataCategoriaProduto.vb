Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data

Public Class DataCategoriaProduto
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetCategoriaProduto(Optional ByVal idCategoria As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idCategoria = Nothing Then
                cmd.Parameters.AddWithValue("@pIdCategoria", idCategoria)
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

    Public Sub InsereCategoriaProduto(ByVal objModeloCategoria As ModeloCategoriaProduto, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloCategoria.Descricao)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function VerificaCategoriaProduto(ByVal objModeloCategoria As ModeloCategoriaProduto) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pDescricao", objModeloCategoria.Descricao)

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

    Public Function VerificaAlteracaoCategoriaProduto(ByVal objModeloCategoria As ModeloCategoriaProduto) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pDescricao", objModeloCategoria.Descricao)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloCategoria.IdCategoria)

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

    Public Sub AlteraCategoriaProduto(ByVal objModeloCategoria As ModeloCategoriaProduto, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", ObjUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloCategoria.IdCategoria)
            cmd.Parameters.AddWithValue("@pDescricao", objModeloCategoria.Descricao)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub DeletaCategoriaProduto(ByVal objModeloCategoria As ModeloCategoriaProduto, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaCategoriaProduto", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloCategoria.IdCategoria)
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

    Public Function VerificaProdutoAssociado(ByVal objModeloCategoria As ModeloCategoriaProduto) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaProdutoAssociado", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloCategoria.IdCategoria)

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