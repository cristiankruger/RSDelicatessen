Imports RS.Modelo
Imports RS.Servico
Imports System.Web.Services
Imports RS.Util

Public Class Perfil
    Inherits System.Web.UI.Page
#Region "OBJETOS"
    Private objUsuarioLogado As New ModeloFuncionario
    Private objServicoPerfil As New ServicoPerfil
    Private objUtiliservice As New UtilidadesService
    Private ServicoMensagem As New ServicoMensagemSistema
#End Region

#Region "SUB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(btnGravar)

        objUsuarioLogado = DirectCast(Session("objusuario"), ModeloFuncionario)

        If IsNothing(objUsuarioLogado) Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            scriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "GetPerfil(); GetListBox(); ", True)
        End If
    End Sub

    Protected Sub btnGravar_Click(sender As Object, e As EventArgs) Handles btnGravar.Click
        Try
            Dim objModeloPerfilCategoria As New ModeloPerfilCategoria
            Dim objServicoPerfilCategoria As New ServicoPerfilCategoria
            Dim objPerfil As New ModeloPerfil
            Dim verifica As Boolean

            objPerfil.Descricao = txtDescricao.Text

            Dim idsCategoria As String = idCategoria.Value
            Dim arrayids() As String = idsCategoria.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim lista As New List(Of Integer)

            For Each id As Integer In arrayids
                If Not lista.Contains(id) Then
                    lista.Add(id)
                End If
            Next


            Select Case pAcao.Value
                Case "Inserir"
                    verifica = objServicoPerfil.VerificaPerfil(objPerfil)
                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = ServicoMensagem.GetMensagemSistemaString("msg", "erroPerfil")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPerfil', true); GetListBox(); limpaidCategoria();", True)
                    Else

                        Dim objModeloPerfil As New ModeloPerfil
                        objModeloPerfil = objServicoPerfil.InserePerfil(objPerfil, objUsuarioLogado)

                        'Dim idsCategoria As String = idCategoria.Value
                        'Dim arrayids() As String = idsCategoria.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
                        'Dim lista As New List(Of Integer)

                        'For Each id As Integer In arrayids
                        '    If Not lista.Contains(id) Then
                        '        lista.Add(id)
                        '    End If
                        'Next

                        For Each id As Integer In lista
                            objModeloPerfilCategoria.IdCategoria = id
                            objModeloPerfilCategoria.IdPerfil = objModeloPerfil.IdPerfil
                            objServicoPerfilCategoria.InserePerfilCategoria(objModeloPerfilCategoria, objUsuarioLogado)
                        Next

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "OpenModal('dialogAlerta', true); GetListBox(); limpaidCategoria();", True)
                    End If

                Case "Alterar"
                    objPerfil.IdPerfil = hdnIdPerfil.Value

                    verifica = objServicoPerfil.VerificaPerfil(objPerfil)

                    If verifica = True Then
                        divModalErro.Visible = True
                        lblErro.Text = ServicoMensagem.GetMensagemSistemaString("msg", "erroPerfil")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogPerfil', true);", True)
                    Else
                        idCategoria.Value = String.Join(";", lista.ToArray)
                        idCategoria.Value += ";"

                        objModeloPerfilCategoria.IdCategoria = idCategoria.Value
                        objModeloPerfilCategoria.IdPerfil = hdnIdPerfil.Value
                        objPerfil.IdPerfil = hdnIdPerfil.Value
                        objPerfil.Descricao = txtDescricao.Text

                        objServicoPerfil.AlteraPerfil(objPerfil, objUsuarioLogado)
                        objServicoPerfilCategoria.AlteraPerfilCategoria(objModeloPerfilCategoria, objUsuarioLogado)

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
                    End If

            End Select

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmExclusao_Click(sender As Object, e As EventArgs) Handles btnConfirmExclusao.Click
        Try
            Dim ObjPerfil As New ModeloPerfil

            ObjPerfil.IdPerfil = hdnIdPerfil.Value

            objServicoPerfil.DeletaPerfil(ObjPerfil, objUsuarioLogado)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString().GetHashCode().ToString(), "OpenModal('dialogAlerta', true);", True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region
#Region "WEBMETHOD"
    <WebMethod>
    Public Shared Function TablePerfil() As String
        Try
            Dim objServicoPerfil As New ServicoPerfil
            Return objServicoPerfil.TablePerfil()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    <WebMethod>
    Public Shared Function PopulaPerfilById(ByVal idPerfil As String) As String
        Try
            Dim objServicoPerfil As New ServicoPerfil
            Return objServicoPerfil.TablePerfilById(idPerfil)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaListBox() As String
        Try
            Dim objServicoCategoria As New ServicoCategoria
            Return objServicoCategoria.ListBox()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaListBoxEsquerda(ByVal idPerfil As Integer) As String
        Try
            Dim objServicoCategoria As New ServicoCategoria
            Return objServicoCategoria.ListBoxEsquerda(idPerfil)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    <WebMethod>
    Public Shared Function PopulaListBoxDireita(ByVal idPerfil As Integer) As String
        Try
            Dim objServicoCategoria As New ServicoCategoria
            Return objServicoCategoria.ListBoxDireita(idPerfil)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class