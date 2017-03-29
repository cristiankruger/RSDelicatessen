Imports Rs.Modelo
Imports System.Data

Public Class GetMenu

    Public Function carregaMenu(ByVal objMenu As DataTable) As String
        Try
            Dim htmlMenu As String = ""

            Dim idPai, idFilho As Integer
            'Dim idPai, idFilho, totalSub, contadorSub As Integer


            For Each row As DataRow In objMenu.Rows

                If row.Item("IdCategoriaPai").ToString = "" & row.Item("totalSubCats").ToString <> "" Then
                    If idPai <> "" Then
                        If idPai <> row.Item("IdCategoriaPai").ToString Then
                            htmlMenu += "</li>"
                        End If
                    Else
                        idPai = row.Item("IdCategoriaPai").ToString
                    End If

                    htmlMenu += "<li class='treeview'><a href=" + row.Item("Link").ToString + "><i class='fa objIcone'></i><span>" + row.Item("Categoria") + "</span><i class='fa fa-angle-left pull-right'></i></a>"

                ElseIf row.Item("IdCategoriaPai").ToString = "" & row.Item("totalSubCats").ToString = "" Then

                    htmlMenu += "<li><a href=" + row.Item("Link").ToString + "><i class='fa fa-list-ul'></i><span>" + row.Item("Categoria") + "</span></a></li>"

                ElseIf row.Item("IdCategoriaPai").ToString <> "" & row.Item("totalSubCats").ToString <> "" Then
                    If idFilho <> "" Then
                        If idFilho <> row.Item("IdCategoriaPai").ToString Then
                            htmlMenu = +"   </li></ul>"
                        End If
                    Else
                        idFilho = row.Item("IdCategoriaPai").ToString
                    End If

                    htmlMenu += "<ul Class='treeview-menu'><li><a href='" + row.Item("Link").ToString + "'><i class='fa  fa-caret-right'></i>" + row.Item("Categoria") + "<i class='fa fa-angle-left pull-right'></i></a>"

                ElseIf row.Item("IdCategoriaPai").ToString <> "" & row.Item("totalSubCats").ToString = "" Then
                    htmlMenu = +"<li><a href='" + row.Item("Link").ToString + "'><i class='fa  fa-caret-right'></i>" + row.Item("Categoria") + "</a></li>"
                End If

            Next

            Return htmlMenu
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
