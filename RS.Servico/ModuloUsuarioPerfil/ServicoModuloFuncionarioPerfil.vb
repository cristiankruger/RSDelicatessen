Imports Rs.Modelo
Imports Rs.Data

Public Class ServicoModuloFuncionarioPerfil

    Private objDataModuloUsuarioPerfil As New DataModuloFuncionarioPerfil

    Public Sub AlteraModuloFuncionarioPerfil(ByVal objModeloModuloUsuarioPerfil As ModeloModuloFuncionarioPerfil, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            objDataModuloUsuarioPerfil.AlteraModuloFuncionarioPerfil(objModeloModuloUsuarioPerfil, objUsuarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
