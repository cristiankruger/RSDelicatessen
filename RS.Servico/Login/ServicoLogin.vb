Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data

Public Class ServicoLogin

    Private objBanco As New DataFuncionario
    Private objUsuario As New ModeloFuncionario
    Private objUtil As New UtilidadesService

    Public Function getLogin(ByVal pUsuario As String, ByVal psenha As String) As ModeloFuncionario
        Try

            psenha = objUtil.criptografaSenha(psenha)

            Dim dt As DataTable
            dt = objBanco.getLogin(pUsuario, psenha)
            If dt.Rows.Count > 0 Then
                objUsuario.IdPessoa = Convert.ToInt32(dt.Rows(0)("idPessoa"))
                objUsuario.IdFuncionario = Convert.ToInt32(dt.Rows(0)("idFuncionario"))
                objUsuario.Senha = dt.Rows(0)("senha")
                objUsuario.Usuario = dt.Rows(0)("usuario")
                objUsuario.PrimeiroAcesso = Convert.ToInt32(dt.Rows(0)("PrimeiroAcesso"))
                objUsuario.IdPerfil = Convert.ToInt32(dt.Rows(0)("idPerfil"))
                objUsuario.Nome = dt.Rows(0)("nome")
                objUsuario.Email = dt.Rows(0)("email")
                objUsuario.Descricao = dt.Rows(0)("descricao")

            End If
            Return objUsuario
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class