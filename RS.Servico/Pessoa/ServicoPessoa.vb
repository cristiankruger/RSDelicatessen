Imports RS.Data
Imports RS.Modelo
Imports RS.Util
Imports System.Data
Imports System.Collections.Generic

Public Class ServicoPessoa

    Private objDataPessoa As New DataPessoa

    Public Function VerificaCPFFuncionario(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataPessoa.VerificaCPFFuncionario(objModeloPessoa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaAlteracaoCPFFuncionario(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataPessoa.VerificaAlteracaoCPFFuncionario(objModeloPessoa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function VerificaCPFCliente(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataPessoa.VerificaCPFCliente(objModeloPessoa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function VerificaAlteracaoCPFCliente(ByVal objModeloPessoa As ModeloPessoa) As Boolean
        Try
            Dim verifica As Boolean
            verifica = objDataPessoa.VerificaAlteracaoCPFCliente(objModeloPessoa)
            Return verifica
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
