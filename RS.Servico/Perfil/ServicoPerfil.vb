Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoPerfil
    Dim objDataPerfil As New DataPerfil

    Public Function TablePerfil() As String
        Try
            Dim dt As New DataTable
            Dim objPerfil As New ModeloPerfil
            Dim listaPerfil As New List(Of ModeloPerfil)
            Dim objServicoPerfil As New ServicoPerfil
            Dim objJson As New SharedJson

            Dim dtRetorno As New DataTable()

            dtRetorno.Columns.Add("Descricao")
            dtRetorno.Columns.Add("Acao")

            Dim drop As String

            listaPerfil = objServicoPerfil.GetPerfil()

            'Para montar a table dinamicamente
            dtRetorno.TableName = "TablePerfil"

            For Each Perfil As ModeloPerfil In listaPerfil

                drop = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-primary dropdown-toggle'>Ação<span class='caret'></span></button>" &
                       "<ul class='dropdown-menu'>" &
                       "<li><a href=' javascript:PopulaPerfil(" + Perfil.IdPerfil.ToString + "); javascript:GetListBoxEsquerda(" + Perfil.IdPerfil.ToString + "); javascript:GetListBoxDireita(" + Perfil.IdPerfil.ToString + ");'>Editar</a></li>" &
                       "<li><a href='javascript:ExcluiPerfil(" + Perfil.IdPerfil.ToString + ");'>Excluir</a></li>" &
                       "</ul></div>"

                Dim linha() As String = {Perfil.Descricao, drop}
                dtRetorno.Rows.Add(linha)
            Next

            If dtRetorno.Rows.Count > 0 Then
                Return objJson.JsonDados(dtRetorno)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetPerfil(Optional ByVal idPerfil As Integer = Nothing) As List(Of ModeloPerfil)
        Try
            Dim dt As New DataTable()
            Dim listaPerfil As New List(Of ModeloPerfil)
            dt = objDataPerfil.GetPerfil(idPerfil)

            For Each row As DataRow In dt.Rows
                Dim objPerfil As New ModeloPerfil
                objPerfil.IdPerfil = CInt(row("idPerfil"))
                objPerfil.Status = CInt(row("status"))
                objPerfil.Descricao = row("descricao").ToString

                listaPerfil.Add(objPerfil)
            Next

            Return listaPerfil

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetPerfilById(ByVal idPerfil As Integer) As ModeloPerfil
        Try
            Dim dt As New DataTable()

            dt = objDataPerfil.GetPerfil(idPerfil)

            Dim objPerfil As New ModeloPerfil

            objPerfil.IdPerfil = CInt(dt.Rows(0).Item("idPerfil"))
            objPerfil.Descricao = dt.Rows(0).Item("descricao").ToString

            Return objPerfil
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function TablePerfilById(ByVal codigoPerfil As String) As String
        Try
            Dim dt As New DataTable()
            Dim objServicoPerfil As New ServicoPerfil
            Dim itemModeloPerfil As New ModeloPerfil
            Dim objJson As New SharedJson

            itemModeloPerfil = objServicoPerfil.GetPerfilById(codigoPerfil)

            dt.TableName = "TablePerfilById"

            dt.Columns.Add("Descricao")
            dt.Columns.Add("IdPerfil")

            Dim linha() As String = {itemModeloPerfil.Descricao, itemModeloPerfil.IdPerfil}
            dt.Rows.Add(linha)

            If dt.Rows.Count > 0 Then
                Return objJson.JsonDados(dt)
            Else
                Return "0"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    'Public Function VerificaPerfil(ByVal objPerfil As ModeloPerfil) As Boolean
    '    Try
    '        Dim verifica As Boolean
    '        verifica = objDataPerfil.VerificaPerfil(objPerfil)
    '        Return verifica
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function InserePerfil(ByVal ObjPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario) As ModeloPerfil
        Try
            Dim dt As New DataTable()

            dt = objDataPerfil.InserePerfil(ObjPerfil, objUsuarioLogado)

            Dim IdPerfil As New ModeloPerfil

            IdPerfil.IdPerfil = CInt(dt.Rows(0).Item("codigoPerfil"))

            Return IdPerfil
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaPerfil(ByVal objPerfil As ModeloPerfil) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataPerfil.VerificaPerfil(objPerfil)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub AlteraPerfil(ByVal objPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            objDataPerfil.AlteraPerfil(objPerfil, objUsuarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub DeletaPerfil(ByVal objPerfil As ModeloPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            objDataPerfil.DeletaPerfil(objPerfil, objUsuarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
