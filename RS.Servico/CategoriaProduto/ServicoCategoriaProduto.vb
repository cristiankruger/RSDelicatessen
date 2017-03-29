Imports RS.Util
Imports RS.Modelo
Imports RS.Data
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoCategoriaProduto
    Private objData As New DataCategoriaProduto

    Public Function GetCategoriaProduto(Optional ByVal idCategoria As Integer = Nothing) As List(Of ModeloCategoriaProduto)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloCategoriaProduto)
            dt = objData.GetCategoriaProduto(idCategoria)

            For Each row As DataRow In dt.Rows
                Dim objCategoria As New ModeloCategoriaProduto
                objCategoria.IdCategoria = CInt(row("idCategoria"))
                objCategoria.Descricao = row("descricao").ToString
                objCategoria.Status = CBool(row("status"))

                lista.Add(objCategoria)
            Next

            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetCategoriaProdutoById(ByVal idCategoria As Integer) As ModeloCategoriaProduto
        Try
            Dim dt As New DataTable()

            dt = objData.GetCategoriaProduto(idCategoria)

            Dim objCategoria As New ModeloCategoriaProduto

            objCategoria.IdCategoria = dt.Rows(0).Item("idCategoria").ToString
            objCategoria.Descricao = dt.Rows(0).Item("descricao").ToString
            objCategoria.Status = dt.Rows(0).Item("status").ToString

            Return objCategoria
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableCategoriaById(ByVal idCategoria As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoCategoriaProduto As New ServicoCategoriaProduto
            Dim itemModeloCategoriaProduto As New ModeloCategoriaProduto
            Dim objJson As New SharedJson

            itemModeloCategoriaProduto = objServicoCategoriaProduto.GetCategoriaProdutoById(idCategoria)

            dt.TableName = "TableCategoriaById"

            dt.Columns.Add("Descricao")
            dt.Columns.Add("IdCategoria")
            dt.Columns.Add("Status")

            Dim linha() As String = {itemModeloCategoriaProduto.Descricao, itemModeloCategoriaProduto.IdCategoria, itemModeloCategoriaProduto.Status}
            dt.Rows.Add(linha)

            If dt.Rows.Count > 0 Then
                Return objJson.JsonDados(dt)
            Else
                Return "0"
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableCategoria() As String
        Try
            Dim dt As New DataTable
            Dim lista As New List(Of ModeloCategoriaProduto)
            Dim objServicoCategoriaProduto As New ServicoCategoriaProduto
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("IdCategoria")
            dtRetorno.Columns.Add("Status")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            lista = objServicoCategoriaProduto.GetCategoriaProduto()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableCategoria"

            For Each objModeloCategoriaProduto As ModeloCategoriaProduto In lista

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href='javascript:PopulaCategoria(" + objModeloCategoriaProduto.IdCategoria.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiCategoria(" + objModeloCategoriaProduto.IdCategoria.ToString + ");'>Excluir</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {objModeloCategoriaProduto.Descricao, objModeloCategoriaProduto.IdCategoria, objModeloCategoriaProduto.Status, drop}
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

    Public Sub DeletaCategoriaProduto(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto, ByVal ObjFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaCategoriaProduto(objModeloCategoriaProduto, ObjFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function verificaCategoriaProduto(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto) As Boolean
        Try
            Dim verifica As Boolean

            verifica = objData.VerificaCategoriaProduto(objModeloCategoriaProduto)

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub InsereCategoriaProduto(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.InsereCategoriaProduto(objModeloCategoriaProduto, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaAlteracaoCategoriaProduto(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto)
        Try
            Dim verifica As Boolean

            verifica = objData.VerificaAlteracaoCategoriaProduto(objModeloCategoriaProduto)

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraCategoriaProduto(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraCategoriaProduto(objModeloCategoriaProduto, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaProdutoAssociado(ByVal objModeloCategoriaProduto As ModeloCategoriaProduto) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objData.VerificaProdutoAssociado(objModeloCategoriaProduto)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
