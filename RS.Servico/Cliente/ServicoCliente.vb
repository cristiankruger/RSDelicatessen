Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoCliente
    Private objDataCliente As New DataCliente

    Public Function GetCliente(Optional ByVal idPessoa As Integer = Nothing) As List(Of ModeloClienteDataDTO)
        Try
            Dim dt As New DataTable()
            Dim lista As New List(Of ModeloClienteDataDTO)
            dt = objDataCliente.GetCliente(idPessoa)

            For Each row As DataRow In dt.Rows
                Dim objClienteDataDTO As New ModeloClienteDataDTO
                objClienteDataDTO.Nome = row("nome").ToString
                objClienteDataDTO.IdPessoa = CInt(row("idPessoa"))
                objClienteDataDTO.CPF = row("cpf").ToString
                objClienteDataDTO.Telefone = row("telefone").ToString

                If row("email").ToString <> "" Then
                    objClienteDataDTO.Email = row("email").ToString
                Else
                    objClienteDataDTO.Email = "-"
                End If
                If row("dataComemorativa").ToString <> "" Then
                    objClienteDataDTO.DataComemorativa = CDate(row("dataComemorativa"))
                Else
                    objClienteDataDTO.DataComemorativa = "-"
                End If
                If row("descricao").ToString <> "" Then
                    objClienteDataDTO.Descricao = row("descricao").ToString
                Else
                    objClienteDataDTO.Descricao = "-"
                End If
                If row("anos").ToString <> "" Then
                    objClienteDataDTO.Anos = row("anos").ToString
                Else
                    objClienteDataDTO.Anos = "0"
                End If


                lista.Add(objClienteDataDTO)
            Next

            Return lista

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableCliente() As String
        Try
            Dim dt As New DataTable
            Dim lista As New List(Of ModeloClienteDataDTO)
            Dim objServicoCliente As New ServicoCliente
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Nome")
            dtRetorno.Columns.Add("CPF")
            dtRetorno.Columns.Add("Email")
            dtRetorno.Columns.Add("Telefone")
            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("DataComemorativa")
            dtRetorno.Columns.Add("Anos")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            lista = objServicoCliente.GetCliente()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableCliente"

            For Each objModeloClienteDataDTO As ModeloClienteDataDTO In lista

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href='javascript:PopulaCliente(" + objModeloClienteDataDTO.IdPessoa.ToString + "); GetContatos(" + objModeloClienteDataDTO.IdPessoa.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiCliente(" + objModeloClienteDataDTO.IdPessoa.ToString + ");'>Excluir</a></li>" &
                       "<li><a href='../DataComemorativa/DataComemorativa.aspx?DataComemorativa=" + objModeloClienteDataDTO.IdPessoa.ToString + "'>Datas Especiais</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {objModeloClienteDataDTO.Nome, objModeloClienteDataDTO.CPF, objModeloClienteDataDTO.Email, objModeloClienteDataDTO.Telefone,
                                         objModeloClienteDataDTO.Descricao, objModeloClienteDataDTO.DataComemorativa, objModeloClienteDataDTO.Anos, drop}
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

    'Public Function GetClienteById(ByVal idPessoa As Integer, ByVal idPedido As Integer) As ModeloCliente
    Public Function GetClienteById(ByVal idPessoa As Integer) As ModeloCliente
        Try
            Dim dt As New DataTable()

            'dt = objDataCliente.GetCliente(idPessoa, idPedido)
            dt = objDataCliente.GetCliente(idPessoa)

            Dim objCliente As New ModeloCliente

            objCliente.Nome = dt.Rows(0).Item("nome").ToString
            objCliente.Email = dt.Rows(0).Item("email").ToString
            objCliente.Bairro = dt.Rows(0).Item("bairro").ToString
            objCliente.CEP = dt.Rows(0).Item("cep").ToString
            objCliente.Cidade = dt.Rows(0).Item("cidade").ToString
            objCliente.Complemento = dt.Rows(0).Item("complemento").ToString
            objCliente.CPF = dt.Rows(0).Item("cpf").ToString
            objCliente.IdPessoa = CInt(dt.Rows(0).Item("idPessoa"))
            objCliente.Logradouro = dt.Rows(0).Item("logradouro").ToString
            objCliente.NumeroResidencia = dt.Rows(0).Item("numeroResidencia").ToString
            objCliente.RG = dt.Rows(0).Item("rg").ToString
            objCliente.Status = CBool(dt.Rows(0).Item("status"))
            objCliente.IdUF = CInt(dt.Rows(0).Item("idUF"))
            objCliente.Foto = dt.Rows(0).Item("foto").ToString
            objCliente.Telefone = dt.Rows(0).Item("telefone").ToString

            Return objCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetClienteByPedido(ByVal idPedido As Integer) As ModeloCliente
        Try
            Dim dt As New DataTable()

            'dt = objDataCliente.GetCliente(idPessoa, idPedido)
            dt = objDataCliente.GetClienteByPedido(idPedido)

            Dim objCliente As New ModeloCliente

            objCliente.Nome = dt.Rows(0).Item("nome").ToString
            objCliente.Email = dt.Rows(0).Item("email").ToString
            objCliente.Bairro = dt.Rows(0).Item("bairro").ToString
            objCliente.CEP = dt.Rows(0).Item("cep").ToString
            objCliente.Cidade = dt.Rows(0).Item("cidade").ToString
            objCliente.Complemento = dt.Rows(0).Item("complemento").ToString
            objCliente.CPF = dt.Rows(0).Item("cpf").ToString
            objCliente.IdPessoa = CInt(dt.Rows(0).Item("idPessoa"))
            objCliente.Logradouro = dt.Rows(0).Item("logradouro").ToString
            objCliente.NumeroResidencia = dt.Rows(0).Item("numeroResidencia").ToString
            objCliente.RG = dt.Rows(0).Item("rg").ToString
            objCliente.Status = CBool(dt.Rows(0).Item("status"))
            objCliente.IdUF = CInt(dt.Rows(0).Item("idUF"))
            objCliente.Foto = dt.Rows(0).Item("foto").ToString
            objCliente.Telefone = dt.Rows(0).Item("telefone").ToString

            Return objCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableClienteById(ByVal idPessoa As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoCliente As New ServicoCliente
            Dim itemCliente As New ModeloCliente
            Dim objJson As New SharedJson

            itemCliente = objServicoCliente.GetClienteById(idPessoa)

            dt.TableName = "TableClienteById"

            dt.Columns.Add("IdPessoa")
            dt.Columns.Add("Nome")
            dt.Columns.Add("Email")
            dt.Columns.Add("Bairro")
            dt.Columns.Add("CEP")
            dt.Columns.Add("Cidade")
            dt.Columns.Add("Complemento")
            dt.Columns.Add("CPF")
            dt.Columns.Add("Logradouro")
            dt.Columns.Add("NumeroResidencia")
            dt.Columns.Add("RG")
            dt.Columns.Add("Status")
            dt.Columns.Add("IdUF")
            dt.Columns.Add("Foto")

            Dim linha() As String = {itemCliente.IdPessoa, itemCliente.Nome, itemCliente.Email, itemCliente.Bairro, itemCliente.CEP, itemCliente.Cidade, itemCliente.Complemento,
                                     itemCliente.CPF, itemCliente.Logradouro, itemCliente.NumeroResidencia, itemCliente.RG, itemCliente.Status, itemCliente.IdUF, itemCliente.Foto}
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

    Public Sub DeletaCliente(ByVal objCliente As ModeloCliente, ByVal ObjFuncionarioLogado As ModeloFuncionario)
        Try
            objDataCliente.DeletaCliente(objCliente, ObjFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraCliente(ByVal objCliente As ModeloCliente, ByVal ObjFuncionarioLogado As ModeloFuncionario)
        Try
            objDataCliente.AlteraCliente(objCliente, ObjFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function InsereCliente(ByVal objCliente As ModeloCliente, ByVal ObjFuncionarioLogado As ModeloFuncionario) As Integer
        Try
            Dim idPessoa As Integer
            idPessoa = objDataCliente.InsereCliente(objCliente, ObjFuncionarioLogado)
            Return idPessoa
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetRelatorioDataComemorativaCliente(ByVal Nome As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloClienteDataDTO)
        Try
            Dim lista As New List(Of ModeloClienteDataDTO)
            lista = objDataCliente.GetRelatorioDataComemorativaCliente(Nome, DataInicial, DataFinal)
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetRelatorioCliente(ByVal Nome As String, ByVal Cidade As String, ByVal Bairro As String, ByVal IdUF As Integer) As List(Of ModeloClienteDataDTO)
        Try
            Dim lista As New List(Of ModeloClienteDataDTO)
            lista = objDataCliente.GetRelatorioCliente(Nome, Cidade, Bairro, IdUF)
            Return lista
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
