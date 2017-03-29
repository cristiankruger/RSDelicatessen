Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic
Public Class ServicoContato
    Private objData As New DataContato

    Public Function GetContato(Optional ByVal idPessoa As Integer = Nothing) As List(Of ModeloContato)
        Try
            Dim dt As New DataTable()
            Dim listModeloContato As New List(Of ModeloContato)
            dt = objData.GetContato(idPessoa)

            For Each row As DataRow In dt.Rows
                Dim objContato As New ModeloContato
                objContato.IdContato = CInt(row("idContato"))
                objContato.IdPessoa = CInt(row("idPessoa"))
                objContato.Status = CBool(row("status"))
                objContato.Telefone = row("telefone").ToString


                listModeloContato.Add(objContato)
            Next

            Return listModeloContato

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetContatoByCliente(ByVal idPessoa As Integer, ByVal idPedido As Integer) As ModeloContato
        Try
            Dim dt As New DataTable()

            dt = objData.GetContato(idPessoa, idPedido)

            Dim objContato As New ModeloContato

            objContato.IdContato = CInt(dt.Rows(0).Item("idContato"))
            objContato.IdPessoa = CInt(dt.Rows(0).Item("idPessoa"))
            objContato.Status = CBool(dt.Rows(0).Item("status"))
            objContato.Telefone = dt.Rows(0).Item("telefone").ToString

            Return objContato
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableContato(ByVal idPessoa As Integer) As String
        Try
            Dim dt As New DataTable
            Dim listaContato As New List(Of ModeloContato)
            Dim objServicoContato As New ServicoContato
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("IdContato")
            dtRetorno.Columns.Add("IdPessoa")
            dtRetorno.Columns.Add("Telefone")

            listaContato = objServicoContato.GetContato(idPessoa)

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableContato"

            For Each objContato As ModeloContato In listaContato
                Dim linha() As String = {objContato.IdContato, objContato.IdPessoa, objContato.Telefone}
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

    Public Sub InsereContato(ByVal objContato As ModeloContato, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.InsereContato(objContato, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraContato(ByVal objContato As ModeloContato, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraContato(objContato, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub DeletaContato(ByVal objContato As ModeloContato, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaContato(objContato, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
