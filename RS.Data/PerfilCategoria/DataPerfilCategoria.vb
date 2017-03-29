Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data

Public Class DataPerfilCategoria
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Sub InserePerfilCategoria(ByVal objModeloPerfilCategoria As ModeloPerfilCategoria, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInserePerfilCategoria", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPerfil", objModeloPerfilCategoria.IdPerfil)
            cmd.Parameters.AddWithValue("@pIdCategoria", objModeloPerfilCategoria.IdCategoria)
            cmd.Parameters.AddWithValue("@pIdUsuario", objUsuarioLogado.IdPessoa)
            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraPerfilCategoria(ByVal objModeloPerfilCategoria As ModeloPerfilCategoria, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraPerfilCategoria", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pArray", objModeloPerfilCategoria.IdCategoria)
            cmd.Parameters.AddWithValue("@pIdPerfil", objModeloPerfilCategoria.IdPerfil)
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