Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataModuloFuncionarioPerfil
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Sub AlteraModuloFuncionarioPerfil(ByVal objModeloModuloFuncionarioPerfil As ModeloModuloFuncionarioPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraModuloFuncionarioPerfil", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdModuloFuncionarioPerfil", objModeloModuloFuncionarioPerfil.IdModuloFuncionarioPerfil)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloModuloFuncionarioPerfil.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdModulo", objModeloModuloFuncionarioPerfil.IdModulo)
            cmd.Parameters.AddWithValue("@pIdPerfil", objModeloModuloFuncionarioPerfil.IdPerfil)
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
