Imports RS.Modelo
Imports RS.Servico
Imports System.Web.Services
Imports RS.Util

Public Class DataComemorativa
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objServicoCliente As New ServicoCliente
    Private objServicoContato As New ServicoContato
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objServicoDataComemorativa As New ServicoDataComemorativa
    Private objUtilValidacao As New utilValidacao
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(btnGravar)

            objFuncionarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

            Dim idPessoa = Request.QueryString("DataComemorativa")
            hdnIdPessoa.Value = idPessoa

            If Not Page.IsPostBack Then
                scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetDataComemorativa(" + idPessoa + ");", True)
                ExibeInfoData(idPessoa)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub ExibeInfoData(ByVal idPessoa As Integer)
        Try
            Dim objClienteInfo = objServicoCliente.GetClienteById(idPessoa)
            txtNome.Text = objClienteInfo.Nome

            If objClienteInfo.Email <> "" Then
                txtEmail.Text = objClienteInfo.Email
            Else
                txtEmail.Text = "E-Mail não Cadastrado."
            End If
            txtTelefone.Text = objClienteInfo.Telefone
            imgCliente.ImageUrl = objClienteInfo.Foto
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Try
            Response.Redirect("~/Cliente/Cliente.aspx", False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            Dim objModeloDataComemorativa As New ModeloDataComemorativa

            objModeloDataComemorativa.IdDataComemorativa = hdnIdDataComemorativa.Value

            objServicoDataComemorativa.DeletaDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "dataDeletada")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs)
        Try
            Dim objModeloDataComemorativa As New ModeloDataComemorativa
            Dim verifica As Boolean

            objModeloDataComemorativa.DataComemorativa = CDate(txtData.Text)
            objModeloDataComemorativa.Descricao = txtDescricao.Text
            objModeloDataComemorativa.IdPessoa = hdnIdPessoa.Value

            Select Case pAcao.Value
                Case "Inserir"
                    verifica = ValidaData(txtData)

                    If verifica = False Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "naoEData")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogData', true); ", True)
                    Else
                        verifica = objServicoDataComemorativa.VerificaDataComemorativa(objModeloDataComemorativa)

                        If verifica = True Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataComemorativa")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogData', true); ", True)
                        Else
                            objServicoDataComemorativa.InsereDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
                            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "dataInserida")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
                        End If
                    End If

                Case "Alterar"

                    objModeloDataComemorativa.IdDataComemorativa = hdnIdDataComemorativa.Value

                    verifica = ValidaData(txtData)

                    If verifica = False Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "naoEData")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogData', true); ", True)
                    Else
                        verifica = objServicoDataComemorativa.VerificaAlteracaoDataComemorativa(objModeloDataComemorativa)

                        If verifica = True Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroDataComemorativa")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogData', true);", True)
                        Else
                            objServicoDataComemorativa.AlteraDataComemorativa(objModeloDataComemorativa, objFuncionarioLogado)
                            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "dataAlterada")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); ", True)
                        End If
                    End If
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TableDataComemorativa(ByVal idPessoa As Integer) As String
        Try
            Dim objServicoDataComemorativa As New ServicoDataComemorativa
            Return objServicoDataComemorativa.TableDataComemorativa(idPessoa)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function TableDataComemorativaById(ByVal idDataComemorativa As Integer) As String
        Try
            Dim objServicoDataComemorativa As New ServicoDataComemorativa
            Return objServicoDataComemorativa.TableDataComemorativaById(idDataComemorativa)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "FUNCTION"
    Protected Function ValidaData(ByVal txtData As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.VerificaData(txtData)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class