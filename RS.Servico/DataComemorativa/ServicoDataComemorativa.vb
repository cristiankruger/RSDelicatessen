Imports RS.Modelo
Imports RS.Data
Imports System.Data
Imports System.Collections.Generic
Imports RS.Util

Public Class ServicoDataComemorativa
    Private objDataDataComemorativa As New DataDataComemorativa

    Public Function GetData(ByVal idPessoa As Integer) As List(Of ModeloDataComemorativa)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloDataComemorativa)
            dt = objDataDataComemorativa.GetDataComemorativa(idPessoa)

            For Each row As DataRow In dt.Rows
                Dim objData As New ModeloDataComemorativa
                objData.Descricao = row("descricao").ToString
                objData.DataComemorativa = CDate(row("dataComemorativa"))
                objData.IdPessoa = CInt(row("idPessoa"))
                objData.IdDataComemorativa = CInt(row("idDataComemorativa"))
                objData.Status = CBool(row("status"))

                lista.Add(objData)
            Next

            Return lista

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableDataComemorativa(ByVal idPessoa As Integer) As String
        Try
            Dim dt As New DataTable
            Dim lista As New List(Of ModeloDataComemorativa)
            Dim objServicoDataComemorativa As New ServicoDataComemorativa
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("DataComemorativa")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            lista = objServicoDataComemorativa.GetData(idPessoa)

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableData"

            For Each objModeloDataComemorativa As ModeloDataComemorativa In lista

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href='javascript:PopulaDataComemorativa(" + objModeloDataComemorativa.IdDataComemorativa.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiDataComemorativa(" + objModeloDataComemorativa.IdDataComemorativa.ToString + ");'>Excluir</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {objModeloDataComemorativa.Descricao, objModeloDataComemorativa.DataComemorativa, drop}
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

    Public Function GetDataById(ByVal idDataComemorativa As Integer) As ModeloDataComemorativa
        Try
            Dim dt As New DataTable()
            dt = objDataDataComemorativa.GetDataComemorativaById(idDataComemorativa)

            Dim objData As New ModeloDataComemorativa
            objData.Descricao = dt.Rows(0)("descricao").ToString
            objData.DataComemorativa = CDate(dt.Rows(0)("dataComemorativa"))
            objData.IdPessoa = CInt(dt.Rows(0)("idPessoa"))
            objData.IdDataComemorativa = CInt(dt.Rows(0)("idDataComemorativa"))
            objData.Status = CBool(dt.Rows(0)("status"))

            Return objData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableDataComemorativaById(ByVal idDataComemorativa As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoDataComemorativa As New ServicoDataComemorativa
            Dim itemData As New ModeloDataComemorativa
            Dim objJson As New SharedJson

            itemData = objServicoDataComemorativa.GetDataById(idDataComemorativa)

            dt.TableName = "TableDataById"

            dt.Columns.Add("IdDataComemorativa")
            dt.Columns.Add("Descricao")
            dt.Columns.Add("DataComemorativa")
            dt.Columns.Add("Status")
            dt.Columns.Add("IdPessoa")
            
            Dim linha() As String = {itemData.IdDataComemorativa, itemData.Descricao, itemData.DataComemorativa, itemData.Status, itemData.IdPessoa}
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
    Public Sub InsereDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objDataDataComemorativa.InsereDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub AlteraDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objDataDataComemorativa.AlteraDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub DeletaDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objDataDataComemorativa.DeleteDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function VerificaDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataDataComemorativa.VerificaDataComemorativa(objModeloDataComemorativa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaAlteracaoDataComemorativa(ByVal objModeloDataComemorativa As ModeloDataComemorativa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataDataComemorativa.VerificaAlteracaoDataComemorativa(objModeloDataComemorativa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
