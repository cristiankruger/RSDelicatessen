Imports RS.Modelo
Imports RS.Servico

Public Class Login
    Inherits System.Web.UI.Page

    Private objLogin As New ServicoLogin
    Private objUsuario As New ModeloFuncionario
    Private ReadOnly objServicoMensagemSistema As New ServicoMensagemSistema

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session.Abandon()
            Session("objUsuario") = Nothing
        End If
    End Sub

    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        objUsuario = objLogin.getLogin(txtUsuario.Text, txtSenha.Text)
        If objUsuario.Nome <> "" Then
            Session("objUsuario") = objUsuario
            If objUsuario.PrimeiroAcesso = True Then
                Dim valorParametroUrl = objServicoMensagemSistema.GetMensagemSistemaString("caminho", "alterarSenha")

                Response.Redirect(valorParametroUrl)
            Else
                'Session("dtoBase") = dtoBaseAtual
                'Response.Redirect("~/Default.aspx")
                Response.Redirect("~/Inicio.aspx")
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Div", "$('.alert').show();", True)
        End If

    End Sub


End Class