Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Public Class ConnBase
    Private _conn As SqlConnection

    Protected Function getConnection() As SqlConnection
        _conn = New SqlConnection(ConfigurationManager.ConnectionStrings("conStringRSDelicatessen").ToString())
        Return _conn
    End Function

    Public Sub fecharConexao(ByVal conn As SqlConnection)
        If Not conn Is Nothing Then
            conn.Close()
        End If
    End Sub
End Class
