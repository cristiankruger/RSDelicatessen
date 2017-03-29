Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataContato
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetContato(Optional ByVal idPessoa As Integer = 0, Optional ByVal idPedido As Integer = 0) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetContato", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idPessoa = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPessoa", idPessoa)
            End If
            If Not idPedido = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPessoa", idPedido)
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

    Public Sub InsereContato(ByVal objModeloContato As ModeloContato, objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereContato", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloContato.IdPessoa)
            cmd.Parameters.AddWithValue("@pStatus", objModeloContato.Status)
            cmd.Parameters.AddWithValue("@pTelefone", objModeloContato.Telefone)
            cmd.Parameters.AddWithValue("@pUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraContato(ByVal objModeloContato As ModeloContato, objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraContato", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdContato", objModeloContato.IdContato)
            cmd.Parameters.AddWithValue("@pTelefone", objModeloContato.Telefone)
            cmd.Parameters.AddWithValue("@pUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub DeletaContato(ByVal objModeloContato As ModeloContato, objUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaContato", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloContato.IdPessoa)
            cmd.Parameters.AddWithValue("@pUsuarioLogado", objUsuarioLogado.IdPessoa)
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
