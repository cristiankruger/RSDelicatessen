Imports RS.Data
Imports System.Data
Imports System.Collections.Generic
Imports RS.Modelo

Public Class ServicoUF
    Dim objDataUF As New DataUF

    Public Function GetUF(Optional ByVal idUF As Integer = Nothing) As List(Of ModeloUF)
        Try
            Dim dt As New DataTable()
            Dim listaUF As New List(Of ModeloUF)
            dt = objDataUF.GetUF(idUF)

            For Each row As DataRow In dt.Rows
                Dim objUF As New ModeloUF
                objUF.IdUF = CInt(row("idUF"))
                objUF.Sigla = row("sigla").ToString
                objUF.NomeEstado = row("nomeEstado").ToString

                listaUF.Add(objUF)
            Next

            Return listaUF

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
