Imports RS.Modelo
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class DataSituacaoPedido
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetSituacaopedido() As List(Of ModeloSituacaoPedido)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetSituacaoPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim lista As New List(Of ModeloSituacaoPedido)
            For Each row As DataRow In dt.Rows
                Dim objSituacaoPedido As New ModeloSituacaoPedido
                objSituacaoPedido.IdSituacaoPedido = CInt(row("idSituacaoPedido"))
                objSituacaoPedido.SituacaoPedido = row("situacaoPedido").ToString

                lista.Add(objSituacaoPedido)
            Next

            trans.Commit()
            Return lista

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function
End Class
