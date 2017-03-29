Imports System.Windows.Forms.PictureBox
Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports System.Web.Services
Imports System.Data
Imports System.IO

Public Class Cliente
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objServicoCliente As New ServicoCliente
    Private objServicoPessoa As New ServicoPessoa
    Private objServicoPerfil As New ServicoPerfil
    Private objServicoUF As New ServicoUF
    Private objServicoContato As New ServicoContato
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objUtil As New UtilidadesService
    Private objValidacao As New UtilValidacao
    Private objModeloCliente As New ModeloCliente
    Private objContato As New ModeloContato
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
            scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetCliente();", True)
            carregaDropUF()
        End If
    End Sub

    Protected Sub carregaDropUF()
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


    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Try
            Dim verifica As Boolean
            Dim nome As String
            Dim extensao As String = Path.GetExtension(FileUpload.FileName)
            Dim caminho As String = Server.MapPath("~\Foto\FotoCliente\")

            objModeloCliente.Nome = txtNomeCliente.Text
            objModeloCliente.Email = txtEmail.Text
            objModeloCliente.Bairro = txtBairro.Text
            objModeloCliente.CEP = txtCEP.Text
            objModeloCliente.Cidade = txtCidade.Text
            objModeloCliente.Complemento = txtComplemento.Text
            objModeloCliente.CPF = txtCPF.Text
            objModeloCliente.Logradouro = txtLogradouro.Text
            objModeloCliente.NumeroResidencia = txtNumero.Text
            objModeloCliente.RG = txtRG.Text
            objModeloCliente.IdUF = CInt(drpUF.SelectedValue)

            Select Case pAcao.Value
                Case "Inserir"

                    If FileUpload.HasFile Then
                        If extensao = ".jpg" Or extensao = ".png" Or extensao = ".jpeg" Then
                            nome = Path.GetFileName(FileUpload.FileName)
                            FileUpload.SaveAs(caminho + nome)

                            objModeloCliente.Foto = "/Foto/FotoCliente/" + nome
                        Else
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFoto")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true); ", True)
                            imgCliente.ImageUrl = "/Foto/FotoCliente/semImagem.jpg"
                            Exit Sub
                        End If
                    Else
                        objModeloCliente.Foto = "/Foto/FotoCliente/" + "semImagem.jpg"
                    End If

                    verifica = objServicoPessoa.VerificaCPFCliente(objModeloCliente)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCPFCliente")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true); ", True)
                        imgCliente.ImageUrl = hdnImgCliente.Value
                    Else
                        verifica = objValidacao.ValidaCPF(txtCPF.Text)

                        If verifica = False Then
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "CPFinvalido")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true); ", True)
                            imgCliente.ImageUrl = hdnImgCliente.Value
                        Else

                            Dim idPessoa As Integer
                            idPessoa = objServicoCliente.InsereCliente(objModeloCliente, objFuncionarioLogado)

                            objContato.IdPessoa = idPessoa
                            objContato.Telefone = txtTel1.Text
                            objContato.Status = CBool(1)
                            objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                            objContato.Telefone = txtTel2.Text
                            objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                            objContato.Telefone = txtTel3.Text
                            objServicoContato.InsereContato(objContato, objFuncionarioLogado)

                            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "clienteInserido")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
                        End If
                    End If

                Case "Alterar"

                    objModeloCliente.IdPessoa = hdnIdPessoa.Value

                    If FileUpload.HasFile Then
                        If extensao = ".jpg" Or extensao = ".png" Or extensao = ".jpeg" Then
                            nome = Path.GetFileName(FileUpload.FileName)
                            FileUpload.SaveAs(caminho + nome)

                            objModeloCliente.Foto = "/Foto/FotoCliente/" + nome
                        Else
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFoto")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true); ", True)
                            imgCliente.ImageUrl = "/Foto/FotoCliente/semImagem.jpg"
                            Exit Sub
                        End If
                    End If

                    verifica = objServicoPessoa.VerificaAlteracaoCPFCliente(objModeloCliente)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCPFCliente")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true);", True)
                        imgCliente.ImageUrl = hdnImgCliente.Value
                    Else
                        verifica = objValidacao.ValidaCPF(txtCPF.Text)

                        If verifica = False Then
                            divModalErro.Visible = True
                            imgCliente.ImageUrl = hdnImgCliente.Value
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "CPFinvalido")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCliente', true); ", True)
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

                            objServicoCliente.AlteraCliente(objModeloCliente, objFuncionarioLogado)
                            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "clienteAlterado")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); LimpaHiddenContatos();", True)
                        End If
                    End If
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            objModeloCliente.IdPessoa = hdnIdPessoa.Value
            objContato.IdPessoa = hdnIdPessoa.Value

            objServicoCliente.DeletaCliente(objModeloCliente, objFuncionarioLogado)
            objServicoContato.DeletaContato(objContato, objFuncionarioLogado)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "clienteDeletado")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETOD"
    <WebMethod>
    Public Shared Function TableCliente() As String
        Try
            Dim objServicoCliente As New ServicoCliente
            Return objServicoCliente.TableCliente()

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
    Public Shared Function PopulaClienteById(ByVal idPessoa As Integer) As String
        Try
            Dim objServicoCliente As New ServicoCliente
            Return objServicoCliente.TableClienteById(idPessoa)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
#End Region
End Class