Imports RS.Modelo
Imports RS.Servico
Imports System.Web.Services

Public Class CategoriaProduto
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objFuncionarioLogado As New ModeloFuncionario
    Private objModeloCategoriaProduto As New ModeloCategoriaProduto
    Private objServicoCategoriaProduto As New ServicoCategoriaProduto
    Private objServicoMensagemSistema As New ServicoMensagemSistema
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
            scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetCategoria();", True)
        End If
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            objModeloCategoriaProduto.IdCategoria = hdnIdCategoria.Value

            Dim verifica As Boolean

            verifica = objServicoCategoriaProduto.VerificaProdutoAssociado(objModeloCategoriaProduto)

            If verifica = True Then
                lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "categoriaAssociada")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
            Else
                objServicoCategoriaProduto.DeletaCategoriaProduto(objModeloCategoriaProduto, objFuncionarioLogado)
                lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "categoriaDeletada")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs)
        Try
            objModeloCategoriaProduto.Descricao = txtNomeCategoria.Text
            Dim verifica As Boolean

            Select Case pAcao.Value
                Case "Inserir"
                    verifica = objServicoCategoriaProduto.verificaCategoriaProduto(objModeloCategoriaProduto)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCategoria")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCategoria', true); ", True)
                        Exit Sub
                    Else
                        objServicoCategoriaProduto.InsereCategoriaProduto(objModeloCategoriaProduto, objFuncionarioLogado)
                        lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "categoriaCadastrada")
                    End If
                Case "Alterar"
                    objModeloCategoriaProduto.IdCategoria = hdnIdCategoria.Value

                    verifica = objServicoCategoriaProduto.VerificaAlteracaoCategoriaProduto(objModeloCategoriaProduto)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "erroCategoria")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogCategoria', true); ", True)
                        Exit Sub
                    Else
                        objServicoCategoriaProduto.AlteraCategoriaProduto(objModeloCategoriaProduto, objFuncionarioLogado)
                        lblMsg.Text = objServicoMensagemSistema.GetMensagemSistemaString("msg", "categoriaAlterada")
                    End If
            End Select
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true); ", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TableCategoria() As String
        Try
            Dim objServicoCategoria As New ServicoCategoriaProduto
            Return objServicoCategoria.TableCategoria()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    <WebMethod>
    Public Shared Function PopulaCategoriaById(ByVal idCategoria As Integer) As String
        Try
            Dim objServicoCategoria As New ServicoCategoriaProduto
            Return objServicoCategoria.TableCategoriaById(idCategoria)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class