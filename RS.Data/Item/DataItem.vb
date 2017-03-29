Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataItem
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetItem(Optional ByVal idPedido As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetItem", conn, trans)
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

    Public Sub InsereItemPedido(ByVal objModeloItem As ModeloItemDTO, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereItemPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdFuncionario", objFuncionarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdProduto", objModeloItem.IdProduto)
            cmd.Parameters.AddWithValue("@pIdPedido", objModeloItem.IdPedido)
            cmd.Parameters.AddWithValue("@pQuantidade", objModeloItem.Quantidade)
            cmd.Parameters.AddWithValue("@pValor", objModeloItem.Valor)

            cmd.ExecuteNonQuery()
        
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub DeletaItemPedido(objModeloItem As ModeloItemDTO, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaItemPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdItem", objModeloItem.IdItem)
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
