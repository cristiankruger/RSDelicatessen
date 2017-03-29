Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Collections.Generic
Imports System.Data

Public Class ServicoCategoria
    Dim objDataCategoria As New DataCategoria

    Public Function GetListBox(Optional ByVal IdCategoria As String = Nothing) As List(Of ModeloCategoriaMenu)
        Try
            Dim dt As New DataTable()
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)

            dt = objDataCategoria.GetCategoria()

            For Each row As DataRow In dt.Rows
                Dim objCategoria As New ModeloCategoriaMenu
                objCategoria.IdCategoria = CInt(row("IdCategoria"))
                objCategoria.Categoria = row("descricaoCompleta").ToString

                listaCategoria.Add(objCategoria)
            Next

            Return listaCategoria

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ListBox() As String
        Try
            Dim dt As New DataTable
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)
            Dim objServicoCategoria As New ServicoCategoria
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Categoria")
            dtRetorno.Columns.Add("IdCategoria")

            Dim item As String

            listaCategoria = objServicoCategoria.GetListBox()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "ListBox"

            For Each objCategoria As ModeloCategoriaMenu In listaCategoria
                item = "" + objCategoria.IdCategoria.ToString + ""

                Dim linha() As String = {objCategoria.Categoria, item}
                dtRetorno.Rows.Add(linha)
            Next

            If dtRetorno.Rows.Count > 0 Then
                Return objJson.JsonDados(dtRetorno)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetListBoxEsquerda(ByVal idPerfil As Integer) As List(Of ModeloCategoriaMenu)
        Try
            Dim dt As New DataTable()
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)

            dt = objDataCategoria.GetCategoriaEsquerda(idPerfil)

            For Each row As DataRow In dt.Rows
                Dim objCategoria As New ModeloCategoriaMenu
                objCategoria.IdCategoria = CInt(row("IdCategoria"))
                objCategoria.Categoria = row("descricaoCompleta").ToString

                listaCategoria.Add(objCategoria)
            Next

            Return listaCategoria

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ListBoxEsquerda(ByVal idPerfil As Integer) As String
        Try
            Dim dt As New DataTable
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)
            Dim objServicoCategoria As New ServicoCategoria
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Categoria")
            dtRetorno.Columns.Add("IdCategoria")

            Dim item As String

            listaCategoria = objServicoCategoria.GetListBoxEsquerda(idPerfil)

            'Para montar a table dinamicamente
            dtRetorno.TableName = "ListBoxEsquerda"

            For Each objCategoria As ModeloCategoriaMenu In listaCategoria
                item = "" + objCategoria.IdCategoria.ToString + ""

                Dim linha() As String = {objCategoria.Categoria, item}
                dtRetorno.Rows.Add(linha)
            Next

            If dtRetorno.Rows.Count > 0 Then
                Return objJson.JsonDados(dtRetorno)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetListBoxDireita(ByVal idPerfil As Integer) As List(Of ModeloCategoriaMenu)
        Try
            Dim dt As New DataTable()
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)

            dt = objDataCategoria.GetCategoriaDireita(idPerfil)

            For Each row As DataRow In dt.Rows
                Dim objCategoria As New ModeloCategoriaMenu
                objCategoria.IdCategoria = CInt(row("IdCategoria"))
                objCategoria.Categoria = row("descricaoCompleta").ToString

                listaCategoria.Add(objCategoria)
            Next

            Return listaCategoria

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ListBoxDireita(ByVal idPerfil As Integer) As String
        Try
            Dim dt As New DataTable
            'Dim objCategoria As New ModeloCategoria
            Dim listaCategoria As New List(Of ModeloCategoriaMenu)
            Dim objServicoCategoria As New ServicoCategoria
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Categoria")
            dtRetorno.Columns.Add("IdCategoria")

            Dim item As String

            listaCategoria = objServicoCategoria.GetListBoxDireita(idPerfil)

            'Para montar a table dinamicamente
            dtRetorno.TableName = "ListBoxDireita"

            For Each objCategoria As ModeloCategoriaMenu In listaCategoria
                item = "" + objCategoria.IdCategoria.ToString + ""

                Dim linha() As String = {objCategoria.Categoria, item}
                dtRetorno.Rows.Add(linha)
            Next

            If dtRetorno.Rows.Count > 0 Then
                Return objJson.JsonDados(dtRetorno)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class