Imports System.Data.SqlClient
Imports Rs.Modelo
Imports System.Data

Public Class DataFuncionario
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function getLogin(ByVal pUsuario As String, ByVal psenha As String) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetLogin", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pUsuario", pUsuario)
            cmd.Parameters.AddWithValue("@pSenha", psenha)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            trans.Commit()
            Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function GetFuncionario(Optional ByVal CodigoModuloFuncionarioPerfil As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not CodigoModuloFuncionarioPerfil = Nothing Then
                cmd.Parameters.AddWithValue("@pIdModuloFuncionarioPerfil", CodigoModuloFuncionarioPerfil)
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            trans.Commit()
            Return dt
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub DeletaFuncionario(ByVal objModeloFuncionario As ModeloFuncionario, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloFuncionario.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objModeloFuncionario.IdPessoa)

            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function VerificaAlteracaoFuncionario(ByVal objModeloFuncionario As ModeloFuncionario) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaAlteracaoFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pUsuario", objModeloFuncionario.Usuario)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloFuncionario.IdPessoa)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim verifica As Boolean
            If dt.Rows.Count > 0 Then
                verifica = True
            Else
                verifica = False
            End If

            trans.Commit()
            Return verifica
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function InsereFuncionario(ByVal objModeloFuncionario As ModeloFuncionario, ByVal objUsuarioLogado As ModeloFuncionario, ByVal objModuloFuncionarioPerfil As ModeloModuloFuncionarioPerfil) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdModulo", objModuloFuncionarioPerfil.IdModulo)
            cmd.Parameters.AddWithValue("@pIdPerfil", objModuloFuncionarioPerfil.IdPerfil)
            cmd.Parameters.AddWithValue("@pUsuario", objModeloFuncionario.Usuario)
            cmd.Parameters.AddWithValue("@pSenha", objModeloFuncionario.Senha)
            cmd.Parameters.AddWithValue("@pNome", objModeloFuncionario.Nome)
            cmd.Parameters.AddWithValue("@pCPF", objModeloFuncionario.CPF)
            cmd.Parameters.AddWithValue("@pRG", objModeloFuncionario.RG)
            cmd.Parameters.AddWithValue("@pEmail", objModeloFuncionario.Email)
            cmd.Parameters.AddWithValue("@pCEP", objModeloFuncionario.CEP)
            cmd.Parameters.AddWithValue("@pLogradouro", objModeloFuncionario.Logradouro)
            cmd.Parameters.AddWithValue("@pNumeroResidencia", objModeloFuncionario.NumeroResidencia)
            cmd.Parameters.AddWithValue("@pComplemento", objModeloFuncionario.Complemento)
            cmd.Parameters.AddWithValue("@pBairro", objModeloFuncionario.Bairro)
            cmd.Parameters.AddWithValue("@pIdUF", objModeloFuncionario.IdUF)
            cmd.Parameters.AddWithValue("@pCidade", objModeloFuncionario.Cidade)

            Dim idPessoa As Integer

            idPessoa = cmd.ExecuteScalar()

            trans.Commit()
            Return idPessoa
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function VerificaFuncionario(ByVal objUsuario As ModeloFuncionario) As Boolean
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spVerificaFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pUsuario", objUsuario.Usuario)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim verifica As Boolean

            If dt.Rows.Count > 0 Then
                verifica = True
            Else
                verifica = False
            End If

            trans.Commit()
            Return verifica
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Sub AlteraFuncionario(ByVal objModeloFuncionario As ModeloFuncionario, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", ObjUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloFuncionario.IdPessoa)
            cmd.Parameters.AddWithValue("@pNome", objModeloFuncionario.Nome)
            If Not objModeloFuncionario.CPF Is Nothing Then
                cmd.Parameters.AddWithValue("@pCPF", objModeloFuncionario.CPF)
            End If
            If Not objModeloFuncionario.RG Is Nothing Then
                cmd.Parameters.AddWithValue("@pRG", objModeloFuncionario.RG)
            End If
            cmd.Parameters.AddWithValue("@pEmail", objModeloFuncionario.Email)
            If Not objModeloFuncionario.CEP Is Nothing Then
                cmd.Parameters.AddWithValue("@pCEP", objModeloFuncionario.CEP)
            End If
            If Not objModeloFuncionario.Logradouro Is Nothing Then
                cmd.Parameters.AddWithValue("@pLogradouro", objModeloFuncionario.Logradouro)
            End If
            If Not objModeloFuncionario.NumeroResidencia Is Nothing Then
                cmd.Parameters.AddWithValue("@pNumeroResidencia", objModeloFuncionario.NumeroResidencia)
            End If
            If Not objModeloFuncionario.Complemento Is Nothing Then
                cmd.Parameters.AddWithValue("@pComplemento", objModeloFuncionario.Complemento)
            End If
            If Not objModeloFuncionario.Bairro Is Nothing Then
                cmd.Parameters.AddWithValue("@pBairro", objModeloFuncionario.Bairro)
            End If
            If objModeloFuncionario.IdUF <> 0 Then
                cmd.Parameters.AddWithValue("@pIdUF", objModeloFuncionario.IdUF)
            End If
            If Not objModeloFuncionario.Cidade Is Nothing Then
                cmd.Parameters.AddWithValue("@pCidade", objModeloFuncionario.Cidade)
            End If
            cmd.Parameters.AddWithValue("@pUsuario", objModeloFuncionario.Usuario)
            If objModeloFuncionario.PrimeiroAcesso <> -1 Then
                cmd.Parameters.AddWithValue("@pPrimeiroAcesso", objModeloFuncionario.PrimeiroAcesso)
            End If

            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Sub AlteraSenhaFuncionario(objUsuario As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraSenhaFuncionario", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@pIdPessoa", objUsuario.IdPessoa)
            cmd.Parameters.AddWithValue("@pSenha", objUsuario.Senha)
            If objUsuario.PrimeiroAcesso <> -1 Then
                cmd.Parameters.AddWithValue("@pPrimeiroAcesso", objUsuario.PrimeiroAcesso)
            End If

            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub
End Class
