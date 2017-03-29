Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports System.Web.Services

Public Class Funcionario
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objServicoFuncionario As New ServicoFuncionario
    Private objServicoPessoa As New ServicoPessoa
    Private objUtiliservice As New UtilidadesService
    Private objServicoPerfil As New ServicoPerfil
    Private objServicoUF As New ServicoUF
    Private objServicoContato As New ServicoContato
    Private objModeloModuloFuncionarioPerfil As New ModeloModuloFuncionarioPerfil
    Private objServicoModuloFuncionarioPerfil As New ServicoModuloFuncionarioPerfil
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objUtil As New UtilidadesService
    Private objValidacao As New UtilValidacao
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(btnGravar)

        objFuncionarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

        If IsNothing(objFuncionarioLogado) Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetFuncionario();", True)
            carregaDropDown()
        End If
    End Sub

    Public Sub carregaDropDown()
        carregaDropPerfil()
        carregaDropUF()
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Try
            Dim objModeloFuncionario As New ModeloFuncionario
            Dim objModeloPessoa As New ModeloPessoa
            Dim objContato As New ModeloContato
            Dim verifica As Boolean

            objModeloFuncionario.Nome = txtNomeFuncionario.Text
            objModeloFuncionario.Email = txtEmail.Text
            objModeloFuncionario.Usuario = txtUsuario.Text
            objModeloFuncionario.Bairro = txtBairro.Text
            objModeloFuncionario.CEP = txtCEP.Text
            objModeloFuncionario.Cidade = txtCidade.Text
            objModeloFuncionario.Complemento = txtComplemento.Text
            objModeloFuncionario.CPF = txtCPF.Text
            objModeloFuncionario.Logradouro = txtLogradouro.Text
            objModeloFuncionario.NumeroResidencia = txtNumero.Text
            objModeloFuncionario.RG = txtRG.Text
            objModeloFuncionario.IdUF = CInt(drpUF.SelectedValue)
            objModeloModuloFuncionarioPerfil.IdModulo = 1
            objModeloModuloFuncionarioPerfil.IdPerfil = CInt(drpPerfil.SelectedValue)
            objModeloPessoa.CPF = txtCPF.Text

            Select Case pAcao.Value
                Case "Inserir"

                    verifica = objServicoFuncionario.VerificaFuncionario(objModeloFuncionario)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFuncionario")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true); ", True)
                    Else

                        verifica = objValidacao.ValidaCPF(txtCPF.Text)

                        If verifica = False Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "CPFinvalido")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true); ", True)
                        Else

                            verifica = objServicoPessoa.VerificaCPFFuncionario(objModeloPessoa)

                            If verifica = True Then
                                divModalErro.Visible = True
                                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCPFFuncionario")
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true); ", True)
                            Else

                                objModeloFuncionario.Senha = GeraSenhaProvisoria(8)
                                objModeloFuncionario.PrimeiroAcesso = 1
                                lblMsg.Text = String.Format(objServicoMensagemSistema.GetMensagemSistemaString("msg", "cadFuncionario"), objModeloFuncionario.Usuario.ToUpper, objModeloFuncionario.Senha)

                                objModeloFuncionario.Senha = criptografaSenha(objModeloFuncionario.Senha.ToString)

                                Dim idPessoa As Integer
                                idPessoa = objServicoFuncionario.InsereFuncionario(objModeloFuncionario, objFuncionarioLogado, objModeloModuloFuncionarioPerfil)

                                objContato.IdPessoa = idPessoa
                                objContato.Telefone = txtTel1.Text
                                objContato.Status = CBool(1)
                                objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                                objContato.Telefone = txtTel2.Text
                                objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                                objContato.Telefone = txtTel3.Text
                                objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
                            End If
                        End If
                    End If


                Case "Alterar"

                    objModeloFuncionario.PrimeiroAcesso = -1
                    objModeloFuncionario.IdPessoa = hdnIdPessoa.Value
                    objModeloModuloFuncionarioPerfil.IdPessoa = hdnIdPessoa.Value
                    objModeloModuloFuncionarioPerfil.IdModuloFuncionarioPerfil = hdnIdModuloFuncionarioPerfil.Value
                    objModeloPessoa.IdPessoa = hdnIdPessoa.Value

                    verifica = objServicoFuncionario.VerificaAlteracaoFuncionario(objModeloFuncionario)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFuncionario")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true);", True)
                    Else
                        verifica = objValidacao.ValidaCPF(txtCPF.Text)

                        If verifica = False Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "CPFinvalido")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true); ", True)
                        Else
                            verifica = objServicoPessoa.VerificaAlteracaoCPFFuncionario(objModeloPessoa)

                            If verifica = True Then
                                divModalErro.Visible = True
                                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCPFFuncionario")
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogFuncionario', true);", True)
                            Else

                                objContato.IdContato = hdnIdContato1.Value
                                objContato.Telefone = txtTel1.Text
                                objServicoContato.AlteraContato(objContato, objFuncionarioLogado)

                                objContato.IdContato = hdnIdContato2.Value
                                objContato.Telefone = txtTel2.Text
                                objServicoContato.AlteraContato(objContato, objFuncionarioLogado)

                                objContato.IdContato = hdnIdContato3.Value
                                objContato.Telefone = txtTel3.Text
                                objServicoContato.AlteraContato(objContato, objFuncionarioLogado)

                                objServicoFuncionario.AlteraFuncionario(objModeloFuncionario, objFuncionarioLogado)
                                objServicoModuloFuncionarioPerfil.AlteraModuloFuncionarioPerfil(objModeloModuloFuncionarioPerfil, objFuncionarioLogado)
                                lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "funcionarioAlterado")
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); GetFuncionario(); LimpaHiddenContatos();", True)
                            End If
                        End If
                    End If
            End Select

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            Dim objModeloFuncionario As New ModeloFuncionario
            Dim objModeloContato As New ModeloContato

            objModeloFuncionario.IdPessoa = hdnIdPessoa.Value
            objModeloContato.IdPessoa = hdnIdPessoa.Value

            objServicoFuncionario.DeletaFuncionario(objModeloFuncionario, objFuncionarioLogado)
            objServicoContato.DeletaContato(objModeloContato, objFuncionarioLogado)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "funcionarioDeletado")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub carregaDropPerfil()
        Try
            drpPerfil.DataSource = objServicoPerfil.GetPerfil()
            drpPerfil.DataTextField = "descricao"
            drpPerfil.DataValueField = "idPerfil"
            drpPerfil.DataBind()
            drpPerfil.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub carregaDropUF()
        Try
            drpUF.DataSource = objServicoUF.GetUF()
            drpUF.DataTextField = "sigla"
            drpUF.DataValueField = "idUF"
            drpUF.DataBind()
            drpUF.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TableFuncionario() As String
        Try
            Dim objServicoFuncionario As New ServicoFuncionario
            Return objServicoFuncionario.TableFuncionario()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function TableContato(ByVal idPessoa As Integer) As String
        Try
            Dim objServicoContato As New ServicoContato
            Return objServicoContato.TableContato(idPessoa)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaFuncionarioById(ByVal idModuloFuncionarioPerfil As Integer) As String
        Try
            Dim objServicoFuncionario As New ServicoFuncionario
            Return objServicoFuncionario.TableFuncionarioById(idModuloFuncionarioPerfil)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "FUNCTION"
    Public Function GeraSenhaProvisoria(ByVal tamanho As Integer) As String
        Try
            Return objUtil.GeraSenhaProvisoria(tamanho)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function criptografaSenha(ByVal senha As String) As String
        Try
            Return objUtil.criptografaSenha(senha)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class