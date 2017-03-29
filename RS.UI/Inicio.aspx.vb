Imports RS.Modelo

Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If Not Session("objUsuario") Is Nothing Then

                Dim objUser As ModeloFuncionario = Session("objUsuario")
                lblMsg.Text = "Seja bem-vindo " & objUser.Nome.ToUpper.Trim

            End If

        End If

    End Sub

End Class