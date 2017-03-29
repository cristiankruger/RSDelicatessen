Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data

Public Class DataTipoPagamento
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetTipoPagamento(Optional ByVal idTipoPagamento As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetTipoPagamento", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idTipoPagamento = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPedido", idTipoPagamento)
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
End Class
