Imports RS.Modelo
Imports RS.Data

Public Class ServicoPerfilCategoria
    Dim objDataPerfilCategoria As New DataPerfilCategoria

    Public Sub InserePerfilCategoria(ByVal objPerfilCategoriaModel As ModeloPerfilCategoria, ByVal objUsuarioLogado As ModeloFuncionario)
        Try
            objDataPerfilCategoria.InserePerfilCategoria(objPerfilCategoriaModel, objUsuarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub AlteraPerfilCategoria(ByVal objPerfilCategoriaModel As ModeloPerfilCategoria, objUsuarioLogado As ModeloFuncionario)
        Try
            objDataPerfilCategoria.AlteraPerfilCategoria(objPerfilCategoriaModel, objUsuarioLogado)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
