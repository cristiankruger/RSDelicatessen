Imports System.Data.SqlClient
Imports System.Data

Public Class DataParametrizacao
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function getMenu(ByVal IdPerfil As Integer) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim dt As New DataTable

            Dim _cmd As New SqlCommand("spGetMenu ", conn, trans)
            _cmd.CommandType = CommandType.StoredProcedure

            _cmd.Parameters.AddWithValue("@pIdPerfil", IdPerfil)

            Dim _da As New SqlDataAdapter(_cmd)
            Dim _dt As New DataTable()

            _da.Fill(_dt)

            trans.Commit()
            Return _dt
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function
End Class
