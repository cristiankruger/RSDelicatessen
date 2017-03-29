Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoFuncionario
    Private objData As New DataFuncionario

    Public Function GetFuncionario(Optional ByVal IdModuloFuncionario As Integer = Nothing) As List(Of ModuloFuncionarioPerfilDTO)
        Try
            Dim dt As New DataTable()
            Dim listModuloFuncionarioPerfil As New List(Of ModuloFuncionarioPerfilDTO)
            dt = objData.GetFuncionario(IdModuloFuncionario)

            For Each row As DataRow In dt.Rows
                Dim objFuncionario As New ModuloFuncionarioPerfilDTO
                objFuncionario.IdModulo = CInt(row("idModulo"))
                objFuncionario.IdPessoa = CInt(row("idPessoa"))
                objFuncionario.IdModuloFuncionarioPerfil = CInt(row("idModuloFuncionarioPerfil"))
                objFuncionario.Usuario = row("usuario").ToString
                objFuncionario.Nome = row("nome").ToString
                objFuncionario.Email = row("email").ToString
                objFuncionario.Descricao = row("descricao").ToString

                listModuloFuncionarioPerfil.Add(objFuncionario)
            Next

            Return listModuloFuncionarioPerfil

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableFuncionario() As String
        Try
            Dim dt As New DataTable
            Dim listaFuncionario As New List(Of ModuloFuncionarioPerfilDTO)
            Dim objServicoFuncionario As New ServicoFuncionario
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Nome")
            dtRetorno.Columns.Add("Email")
            dtRetorno.Columns.Add("Usuario")
            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            listaFuncionario = objServicoFuncionario.GetFuncionario()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TableFuncionario"

            For Each objModuloFuncionarioPerfilDTO As ModuloFuncionarioPerfilDTO In listaFuncionario

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href='javascript:PopulaFuncionario(" + objModuloFuncionarioPerfilDTO.IdModuloFuncionarioPerfil.ToString + "); GetContatos(" + objModuloFuncionarioPerfilDTO.IdPessoa.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiFuncionario(" + objModuloFuncionarioPerfilDTO.IdPessoa.ToString + ");'>Excluir</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {objModuloFuncionarioPerfilDTO.Nome, objModuloFuncionarioPerfilDTO.Email, objModuloFuncionarioPerfilDTO.Usuario, objModuloFuncionarioPerfilDTO.Descricao, drop}
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

    Public Function GetFuncionarioById(ByVal IdModuloFuncionarioPerfil As Integer) As ModuloFuncionarioPerfilDTO
        Try
            Dim dt As New DataTable()

            dt = objData.GetFuncionario(IdModuloFuncionarioPerfil)

            Dim objFuncionario As New ModuloFuncionarioPerfilDTO

            objFuncionario.Nome = dt.Rows(0).Item("nome").ToString
            objFuncionario.Email = dt.Rows(0).Item("email").ToString
            objFuncionario.Usuario = dt.Rows(0).Item("usuario").ToString
            objFuncionario.Descricao = dt.Rows(0).Item("descricao").ToString
            objFuncionario.IdFuncionario = CInt(dt.Rows(0).Item("IdFuncionario"))
            objFuncionario.IdPerfil = CInt(dt.Rows(0).Item("IdPerfil"))
            objFuncionario.IdModulo = CInt(dt.Rows(0).Item("IdModulo"))
            objFuncionario.IdModuloFuncionarioPerfil = CInt(dt.Rows(0).Item("idModuloFuncionarioPerfil"))
            objFuncionario.PrimeiroAcesso = CBool(dt.Rows(0).Item("primeiroAcesso"))
            objFuncionario.Bairro = dt.Rows(0).Item("bairro").ToString
            objFuncionario.CEP = dt.Rows(0).Item("cep").ToString
            objFuncionario.Cidade = dt.Rows(0).Item("cidade").ToString
            objFuncionario.Complemento = dt.Rows(0).Item("complemento").ToString
            objFuncionario.CPF = dt.Rows(0).Item("cpf").ToString
            objFuncionario.IdPessoa = CInt(dt.Rows(0).Item("idPessoa"))
            objFuncionario.Logradouro = dt.Rows(0).Item("logradouro").ToString
            objFuncionario.NumeroResidencia = dt.Rows(0).Item("numeroResidencia").ToString
            objFuncionario.RG = dt.Rows(0).Item("rg").ToString
            objFuncionario.Status = CBool(dt.Rows(0).Item("status"))
            objFuncionario.IdUF = CInt(dt.Rows(0).Item("idUF"))

            Return objFuncionario
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TableFuncionarioById(ByVal idModuloFuncionarioPerfil As Integer) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoFuncionario As New ServicoFuncionario
            Dim itemModuloFuncionarioPerfil As New ModuloFuncionarioPerfilDTO
            Dim objJson As New SharedJson

            itemModuloFuncionarioPerfil = objServicoFuncionario.GetFuncionarioById(idModuloFuncionarioPerfil)

            dt.TableName = "TableFuncionarioById"

            dt.Columns.Add("Nome")
            dt.Columns.Add("Email")
            dt.Columns.Add("Usuario")
            dt.Columns.Add("Descricao")
            dt.Columns.Add("IdFuncionario")
            dt.Columns.Add("IdPerfil")
            dt.Columns.Add("IdModulo")
            dt.Columns.Add("IdModuloFuncionarioPerfil")
            dt.Columns.Add("PrimeiroAcesso")
            dt.Columns.Add("Bairro")
            dt.Columns.Add("CEP")
            dt.Columns.Add("Cidade")
            dt.Columns.Add("Complemento")
            dt.Columns.Add("CPF")
            dt.Columns.Add("IdPessoa")
            dt.Columns.Add("Logradouro")
            dt.Columns.Add("NumeroResidencia")
            dt.Columns.Add("RG")
            dt.Columns.Add("Status")
            dt.Columns.Add("IdUF")

            Dim linha() As String = {itemModuloFuncionarioPerfil.Nome, itemModuloFuncionarioPerfil.Email, itemModuloFuncionarioPerfil.Usuario, itemModuloFuncionarioPerfil.Descricao,
                                     itemModuloFuncionarioPerfil.IdFuncionario, itemModuloFuncionarioPerfil.IdPerfil, itemModuloFuncionarioPerfil.IdModulo,
                                     itemModuloFuncionarioPerfil.IdModuloFuncionarioPerfil, itemModuloFuncionarioPerfil.PrimeiroAcesso, itemModuloFuncionarioPerfil.Bairro,
                                     itemModuloFuncionarioPerfil.CEP, itemModuloFuncionarioPerfil.Cidade, itemModuloFuncionarioPerfil.Complemento, itemModuloFuncionarioPerfil.CPF,
                                     itemModuloFuncionarioPerfil.IdPessoa, itemModuloFuncionarioPerfil.Logradouro, itemModuloFuncionarioPerfil.NumeroResidencia, itemModuloFuncionarioPerfil.RG,
                                     itemModuloFuncionarioPerfil.Status, itemModuloFuncionarioPerfil.IdUF}
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

    Public Sub DeletaFuncionario(ByVal objFuncionario As ModeloFuncionario, ByVal ObjFuncionarioLogado As ModeloFuncionario)
        Try
            objData.DeletaFuncionario(objFuncionario, ObjFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function verificafuncionario(ByVal objfuncionario As ModeloFuncionario) As Boolean
        Try
            Dim verifica As Boolean

            verifica = objdata.verificafuncionario(objfuncionario)

            Return verifica
        Catch ex As exception
            Throw New exception(ex.message)
        End Try
    End Function

    Public Function InsereFuncionario(ByVal objFuncionario As ModeloFuncionario, ByVal objFuncionarioLogado As ModeloFuncionario, objModuloFuncionarioPerfil As ModeloModuloFuncionarioPerfil) As Integer
        Try
            Dim idPessoa As Integer

            idPessoa = objData.InsereFuncionario(objFuncionario, objFuncionarioLogado, objModuloFuncionarioPerfil)

            Return idPessoa
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaAlteracaoFuncionario(ByVal objFuncionario As ModeloFuncionario)
        Try
            Dim verifica As Boolean

            verifica = objData.VerificaAlteracaoFuncionario(objFuncionario)

            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraFuncionario(ByVal objFuncionario As ModeloFuncionario, ByVal objFuncionarioLogado As ModeloFuncionario)
        Try
            objData.AlteraFuncionario(objFuncionario, objFuncionarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraSenhaFuncionario(ByVal objFuncionario As ModeloFuncionario)
        Try
            objData.AlteraSenhaFuncionario(objFuncionario)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
