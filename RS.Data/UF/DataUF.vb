Imports RS.Modelo
Imports System.Data.SqlClient
Imports System.Data

Public Class DataUF
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetUF(Optional ByVal IdUF As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetUF", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not IdUF = Nothing Then
                cmd.Parameters.AddWithValue("@pIdUF", IdUF)
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

End Class
