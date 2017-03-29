Imports RS.Modelo
Imports RS.Servico

Public Class SiteMaster
    Inherits MasterPage
    Public objUsuario As New ModeloFuncionario
    Public objParametro As New ServicoParametrizacao
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objUsuario = DirectCast(Session("objUsuario"), ModeloFuncionario)
        Session("ob") = "a"

        If IsNothing(objUsuario) Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If

        Dim dt As New DataTable

        If Session("sDtMenu") Is Nothing Then
            Session("sDtMenu") = objParametro.getMenu(objUsuario.idPerfil)
        End If

        dt = Session("sDtMenu")

        Dim menu As String

        menu = carregaMenu(dt)
        menusection.InnerHtml = menu

    End Sub

    Protected Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Session.RemoveAll()
        Response.Redirect("~/Login.aspx")
    End Sub

    Public Function carregaMenu(ByVal objMenu As DataTable) As String
        Try

            Dim htmlMenu As String = ""

            htmlMenu = "<ul class='sidebar-menu'>"
            htmlMenu += "<li class='header'>Sistema <br />RS Delicatessen<br /></li>"
            htmlMenu += "<li class='treeview'>" &
                        "<a href='#'><i class='glyphicon glyphicon-folder-open'></i><span>Módulo <br/> de Vendas</span><i class='fa fa-angle-left pull-right'></i> </a>" &
                                "<ul class='treeview-menu'>"

            Dim _dtListaPai As DataTable = objMenu.Select("(idCategoriaPai IS NULL )", "ordem").CopyToDataTable

            For Each row As DataRow In _dtListaPai.Rows
                If row.Item("totalSubCats").ToString <> "" Then
                    If row.Item("Link").ToString.Trim() = "" Or row.Item("Link").ToString.Trim() = "paginaInicial.aspx" Or row.Item("Link").ToString.Trim() = "#" Or row.Item("Link").ToString.Trim() = "##" Then

                        If row.Item("Link").ToString.Trim() = "##" Then
                            htmlMenu += "<li><a href='#' onclick='notificaLinkDesativ()'><i class='fa  fa-caret-right'></i>" + row.Item("Categoria") + "<i class='fa fa-angle-left pull-right'></i></a>"
                        Else
                            htmlMenu += "<li><a href='" & ResolveUrl("~/" + row.Item("Link").ToString) + "'><i class='fa  fa-caret-right'></i>" + row.Item("Categoria") + "<i class='fa fa-angle-left pull-right'></i></a>"
                            htmlMenu += AdicionaMenuFilho(CInt(row.Item("IdCategoria")), objMenu)
                        End If

                    End If
                Else

                    If row.Item("Link").ToString.Trim() = "##" Then
                        htmlMenu += "<li><a href='#' onclick='notificaLinkDesativ()'><i class='fa objIcone'></i><span>" + row.Item("Categoria") + "</span><i class='fa fa-angle-left pull-right'></i></a></li>"
                    Else
                        htmlMenu += "<li><a href='" & ResolveUrl("~/" + row.Item("Link").ToString) & "'><i class='fa objIcone'></i><span>" + row.Item("Categoria").ToString + "</span><i class='fa fa-angle-left pull-right'></i></a></li>"
                    End If

                End If

            Next

            htmlMenu += "</ul></li></ul>" & Chr(10)

            Return htmlMenu
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function AdicionaMenuFilho(ByVal idCategoria As Int32, ByVal objMenu As DataTable) As String
        Try

            Dim dtFilhos As DataTable = objMenu.Select("idCategoriaPai = " + idCategoria.ToString() + "", "Ordem").CopyToDataTable

            Dim htmlMenuFilho As String = ""


            If dtFilhos.Rows.Count > 0 Then
                htmlMenuFilho += "<ul class='treeview-menu'>"

                For Each filho As DataRow In dtFilhos.Rows

                    If filho.Item("totalSubCats").ToString = "" Then

                        If filho.Item("Link").ToString.Trim() = "##" Then
                            '-------- NOTIFICA QUE LINK INDISPONIVEL
                            htmlMenuFilho += "<li><a href='#' onclick='notificaLinkDesativ()'><i class='fa  fa-caret-right'></i>" + filho.Item("Categoria") + "<i class='fa fa-angle-left pull-right'></i></a>"
                        Else
                            htmlMenuFilho += "<li><a href='" & ResolveUrl("~/" + filho.Item("Link").ToString) + "'><i class='fa  fa-caret-right'></i>" + filho.Item("Categoria").ToString + "</a></li>" & Chr(10)
                        End If

                    Else
                        htmlMenuFilho += "<li><a href='" & ResolveUrl("~/" + filho.Item("Link").ToString) + "'><i class='fa  fa-caret-right'></i>" + filho.Item("Categoria").ToString + "</a>" & Chr(10)
                        htmlMenuFilho += AdicionaMenuFilhoDoFilho(CInt(filho.Item("IdCategoria")), objMenu)
                        htmlMenuFilho += "</li>"
                    End If
                Next

                htmlMenuFilho += "</ul>"
                htmlMenuFilho += "</li>"

            End If

            Return htmlMenuFilho
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function AdicionaMenuFilhoDoFilho(ByVal idCategoria As Int32, ByVal objMenu As DataTable) As String
        Try
            Dim dtFilhos As DataTable = objMenu.Select("idCategoriaPai = " + idCategoria.ToString() + "", "Ordem").CopyToDataTable


            Dim htmlMenuFilho As String = ""


            If dtFilhos.Rows.Count > 0 Then
                htmlMenuFilho += "<ul Class='treeview-menu'>"

                For Each filho As DataRow In dtFilhos.Rows
                    htmlMenuFilho += "<li><a href='" & ResolveUrl("~/" + filho.Item("Link").ToString) + "'><i class='fa  fa-caret-right'></i>" + filho.Item("Categoria").ToString + "</a></li>" & Chr(10)
                Next
                htmlMenuFilho += "</ul>"


            End If

            Return htmlMenuFilho
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class