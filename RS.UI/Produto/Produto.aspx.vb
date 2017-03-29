Imports RS.Modelo
Imports RS.Servico
Imports RS.Util
Imports System.Web.Services
Imports System.IO

Public Class Produto
    Inherits System.Web.UI.Page

#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objModeloProduto As New ModeloProduto
    Private objServicoProduto As New ServicoProduto
    Private objServicoCategoria As New ServicoCategoriaProduto
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objUtilValidacao As New UtilValidacao
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(btnGravar)

            objFuncionarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

            If IsNothing(objFuncionarioLogado) Then
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetProduto();", True)
                carregaDropCategoria()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            objModeloProduto.IdProduto = hdnIdProduto.Value

            objServicoProduto.DeletaProduto(objModeloProduto, objFuncionarioLogado)
            lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "produtoDeletado")
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Try
            Dim verifica As Boolean
            Dim nome As String
            Dim extensao As String = Path.GetExtension(FileUpload.FileName)
            Dim caminho As String = Server.MapPath("~\Foto\FotoProduto\")

            objModeloProduto.Nome = txtNomeProduto.Text
            objModeloProduto.DataFabricacao = CDate(txtDataFabricacao.Text)
            objModeloProduto.DataValidade = CDate(txtDataValidade.Text)
            objModeloProduto.Descricao = txtDescricao.Text
            objModeloProduto.IdCategoria = CInt(drpCategoria.SelectedValue)
            objModeloProduto.Valor = String.Format("{0:C}", txtValor.Text).Replace(".", "").Replace(",", ".").Replace("R$", "")

            verifica = ValidaDatas(txtDataFabricacao, txtDataValidade)

            If verifica = False Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroData")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true); ", True)
                Exit Sub
            Else
                verifica = validaDataFabricaValidade(txtDataFabricacao, txtDataValidade)
                If verifica = False Then
                    divModalErro.Visible = True
                    lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "fabricacaoMaiorValidade")
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true); ", True)
                    Exit Sub
                End If
            End If

            Select Case pAcao.Value
                Case "Inserir"

                    If FileUpload.HasFile Then
                        If extensao = ".jpg" Or extensao = ".png" Or extensao = ".jpeg" Then
                            nome = Path.GetFileName(FileUpload.FileName)
                            FileUpload.SaveAs(caminho + nome)

                            objModeloProduto.Foto = "/Foto/FotoProduto/" + nome
                        Else
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFoto")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true); ", True)
                            imgProduto.ImageUrl = "/Foto/FotoProduto/imagemNaoCadastrada.png"
                            Exit Sub
                        End If
                    Else
                        objModeloProduto.Foto = "/Foto/FotoProduto/imagemNaoCadastrada.png"
                    End If

                    verifica = objServicoProduto.verificaProduto(objModeloProduto)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroProduto")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true); ", True)
                        imgProduto.ImageUrl = hdnImgProduto.Value
                    Else
                        objServicoProduto.InsereProduto(objModeloProduto, objFuncionarioLogado)

                        lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "produtoCadastrado")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)

                    End If
                Case "Alterar"

                    objModeloProduto.IdProduto = hdnIdProduto.Value

                    If FileUpload.HasFile Then
                        If extensao = ".jpg" Or extensao = ".png" Or extensao = ".jpeg" Then
                            nome = Path.GetFileName(FileUpload.FileName)
                            FileUpload.SaveAs(caminho + nome)

                            objModeloProduto.Foto = "/Foto/FotoProduto/" + nome
                        Else
                            divModalErro.Visible = True
                            lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroFoto")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true); ", True)
                            imgProduto.ImageUrl = "/Foto/FotoProduto/imagemNaoCadastrada.png"
                            Exit Sub
                        End If
                    End If

                    verifica = objServicoProduto.VerificaAlteracaoProduto(objModeloProduto)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroProduto")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogProduto', true);", True)
                        imgProduto.ImageUrl = hdnImgProduto.Value
                    Else
                        objServicoProduto.AlteraProduto(objModeloProduto, objFuncionarioLogado)
                        lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "produtoAlterado")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); ", True)

                    End If
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub carregaDropCategoria()
        Try
            drpCategoria.DataSource = objServicoCategoria.GetCategoriaProduto()
            drpCategoria.DataTextField = "descricao"
            drpCategoria.DataValueField = "idCategoria"
            drpCategoria.DataBind()
            drpCategoria.Items.Insert(0, New ListItem("---Selecione---", "0"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TableProduto() As String
        Try
            Dim objServicoProduto As New ServicoProduto
            Return objServicoProduto.TableProduto()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaProdutoById(ByVal idProduto As Integer) As String
        Try
            Dim objServicoProduto As New ServicoProduto
            Return objServicoProduto.TableProdutoById(idProduto)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

#Region "FUNCTION"
    Protected Function validaDataFabricaValidade(ByVal txtDataFabricacao As TextBox, ByVal txtDataValidade As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.validaDataTxtBox(txtDataFabricacao, txtDataValidade)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Function ValidaDatas(ByVal txtDataFabricacao As TextBox, ByVal txtDataValidade As TextBox) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objUtilValidacao.VerificaDatas(txtDataFabricacao, txtDataValidade)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Function VerificaDataFormatoErrado() As Boolean
    '    Try
    '        Return objUtilValidacao.verificaDataFormatoErrado(txtDataFabricacao, txtDataValidade)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
#End Region
End Class