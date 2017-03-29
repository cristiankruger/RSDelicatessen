Imports Rs.Modelo
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class DataParametroSistema
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetMensagemSistemaString(ByVal Tipo As String, ByVal Nome As String) As String
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetMensagemSistema", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure

            If Not Tipo Is Nothing Then
                cmd.Parameters.AddWithValue("@pTipo", Tipo)
            End If

            If Not Nome Is Nothing Then
                cmd.Parameters.AddWithValue("@pNome", Nome)
            End If

            Dim msg As String = cmd.ExecuteScalar

            trans.Commit()
            Return msg
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function
End Class
