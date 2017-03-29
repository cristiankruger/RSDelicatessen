Imports RS.Modelo
Imports RS.Util
Imports RS.Servico

Public Class AlterarSenha
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objModeloFuncionario As New ModeloFuncionario
    Private objUsuarioLogado As New ModeloFuncionario
    Private objServicoFuncionario As New ServicoFuncionario
    Private objServicoMensagemSistema As New ServicoMensagemSistema
    Private objUtilValidacao As New UtilValidacao
    Private objUtil As New UtilidadesService
#End Region
#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            objUsuarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)
            objModeloFuncionario = objUsuarioLogado

            If IsNothing(objModeloFuncionario) Then
                Response.Redirect("~/Login.aspx")
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                carregaInfoUsuario()
            End If
        Catch ex As Exception
            lblErro.Text = ex.Message.Trim
        End Try
    End Sub

    Protected Sub carregaInfoUsuario()
        Try
            lblInfo.Text = String.Format(objServicoMensagemSistema.GetMensagemSistemaString("info", "FuncionarioInfo"), objModeloFuncionario.Nome, objModeloFuncionario.Email, objModeloFuncionario.Usuario)
        Catch ex As Exception
            lblErro.Text = ex.Message.Trim
        End Try
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs)
        Try
            If verificaTamanhoSenhas() = True Then
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("info", "tamanhoSenhas")
            ElseIf verificaSenhas() = True Then
                objModeloFuncionario.Senha = objUtil.criptografaSenha(txtSenhaNova1.Text)
                objModeloFuncionario.PrimeiroAcesso = 0

                objServicoFuncionario.AlteraSenhaFuncionario(objModeloFuncionario)

                divModalErro.Visible = False
                lblAlertaModal.Text = objServicoMensagemSistema.GetMensagemSistemaString("info", "senhaAlterada")
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
            Else
                divModalErro.Visible = True
                lblErro.Text = objServicoMensagemSistema.GetMensagemSistemaString("info", "erroSenhas1e2")
            End If
        Catch ex As Exception
            lblErro.Text = ex.Message.Trim
        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("~/Login.aspx")
        Catch ex As Exception
            lblErro.Text = ex.Message.Trim
        End Try
    End Sub
#End Region

#Region "FUNCTION"
    Public Function verificaSenhas() As Boolean
        Try
            Return objUtilValidacao.VerificaSenhas(txtSenhaNova1, txtSenhaNova2)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Function verificaTamanhoSenhas() As Boolean
        Try
            Return objUtilValidacao.VerificaTamanhoSenhas(txtSenhaNova1, txtSenhaNova2)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class