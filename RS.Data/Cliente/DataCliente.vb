Imports System.Data.SqlClient
Imports RS.Modelo
Imports System.Data
Imports System.Collections.Generic

Public Class DataCliente
    Inherits ConnBase
    Private conn As SqlConnection = getConnection()
    Private trans As SqlTransaction

    Public Function GetCliente(Optional ByVal idPessoa As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idPessoa = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPessoa", idPessoa)
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

    Public Function GetClienteByPedido(Optional ByVal idPedido As Integer = Nothing) As DataTable
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spGetClienteByPedido", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not idPedido = Nothing Then
                cmd.Parameters.AddWithValue("@pIdPedido", idPedido)
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

    Public Function InsereCliente(ByVal objModeloCliente As ModeloCliente, ByVal objUsuarioLogado As ModeloFuncionario) As Integer
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spInsereCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", objUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pNome", objModeloCliente.Nome)
            cmd.Parameters.AddWithValue("@pCPF", objModeloCliente.CPF)
            cmd.Parameters.AddWithValue("@pRG", objModeloCliente.RG)
            cmd.Parameters.AddWithValue("@pEmail", objModeloCliente.Email)
            cmd.Parameters.AddWithValue("@pCEP", objModeloCliente.CEP)
            cmd.Parameters.AddWithValue("@pLogradouro", objModeloCliente.Logradouro)
            cmd.Parameters.AddWithValue("@pNumeroResidencia", objModeloCliente.NumeroResidencia)
            cmd.Parameters.AddWithValue("@pComplemento", objModeloCliente.Complemento)
            cmd.Parameters.AddWithValue("@pBairro", objModeloCliente.Bairro)
            cmd.Parameters.AddWithValue("@pIdUF", objModeloCliente.IdUF)
            cmd.Parameters.AddWithValue("@pCidade", objModeloCliente.Cidade)
            cmd.Parameters.AddWithValue("@pFoto", objModeloCliente.Foto)

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

    Public Sub AlteraCliente(ByVal objModeloCliente As ModeloCliente, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spAlteraCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", ObjUsuarioLogado.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloCliente.IdPessoa)
            cmd.Parameters.AddWithValue("@pNome", objModeloCliente.Nome)
            If Not objModeloCliente.CPF Is Nothing Then
                cmd.Parameters.AddWithValue("@pCPF", objModeloCliente.CPF)
            End If
            If Not objModeloCliente.RG Is Nothing Then
                cmd.Parameters.AddWithValue("@pRG", objModeloCliente.RG)
            End If
            cmd.Parameters.AddWithValue("@pEmail", objModeloCliente.Email)
            If Not objModeloCliente.CEP Is Nothing Then
                cmd.Parameters.AddWithValue("@pCEP", objModeloCliente.CEP)
            End If
            If Not objModeloCliente.Logradouro Is Nothing Then
                cmd.Parameters.AddWithValue("@pLogradouro", objModeloCliente.Logradouro)
            End If
            If Not objModeloCliente.NumeroResidencia Is Nothing Then
                cmd.Parameters.AddWithValue("@pNumeroResidencia", objModeloCliente.NumeroResidencia)
            End If
            If Not objModeloCliente.Complemento Is Nothing Then
                cmd.Parameters.AddWithValue("@pComplemento", objModeloCliente.Complemento)
            End If
            If Not objModeloCliente.Bairro Is Nothing Then
                cmd.Parameters.AddWithValue("@pBairro", objModeloCliente.Bairro)
            End If
            If objModeloCliente.IdUF <> 0 Then
                cmd.Parameters.AddWithValue("@pIdUF", objModeloCliente.IdUF)
            End If
            If Not objModeloCliente.Cidade Is Nothing Then
                cmd.Parameters.AddWithValue("@pCidade", objModeloCliente.Cidade)
            End If
            If Not objModeloCliente.Foto Is Nothing Then
                cmd.Parameters.AddWithValue("@pFoto", objModeloCliente.Foto)
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

    Public Sub DeletaCliente(ByVal objModeloCliente As ModeloCliente, ByVal ObjUsuarioLogado As ModeloFuncionario)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spDeletaCliente", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pIdPessoa", objModeloCliente.IdPessoa)
            cmd.Parameters.AddWithValue("@pIdUsuarioLogado", ObjUsuarioLogado.IdPessoa)

            cmd.ExecuteNonQuery()

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Sub

    Public Function GetRelatorioDataComemorativaCliente(ByVal Nome As String, ByVal DataInicial As Date, ByVal DataFinal As Date) As List(Of ModeloClienteDataDTO)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spRelatorioDataComemorativa", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not Nome = "" Then
                cmd.Parameters.AddWithValue("@pNome", Nome)
            End If
            If Not DataInicial = "#1/1/0001#" Then
                cmd.Parameters.AddWithValue("@pDataInicial", DataInicial)
            End If
            If Not DataFinal = "#1/1/0001#" Then
                cmd.Parameters.AddWithValue("@pDataFinal", DataFinal)
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim lista As New List(Of ModeloClienteDataDTO)

            For Each row As DataRow In dt.Rows
                Dim objModeloClienteDataDTO As New ModeloClienteDataDTO

                objModeloClienteDataDTO.Nome = row("nome").ToString
                objModeloClienteDataDTO.Bairro = row("bairro").ToString
                objModeloClienteDataDTO.Cidade = row("cidade").ToString
                objModeloClienteDataDTO.SiglaUF = row("sigla").ToString
                objModeloClienteDataDTO.Email = row("email").ToString
                objModeloClienteDataDTO.Telefone = row("telefone").ToString
                objModeloClienteDataDTO.DataComemorativa = CDate(row("dataComemorativa"))
                objModeloClienteDataDTO.Descricao = row("descricao").ToString

                lista.Add(objModeloClienteDataDTO)
            Next

            trans.Commit()
            Return lista
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function

    Public Function GetRelatorioCliente(ByVal Nome As String, ByVal Cidade As String, ByVal Bairro As String, ByVal IdUF As Integer) As List(Of ModeloClienteDataDTO)
        Try
            conn.Open()
            trans = conn.BeginTransaction()
            Dim cmd As New SqlCommand("spRelatorioClientes", conn, trans)
            cmd.CommandType = CommandType.StoredProcedure
            If Not Nome = Nothing Then
                cmd.Parameters.AddWithValue("@pNome", Nome)
            End If
            If Not Bairro = Nothing Then
                cmd.Parameters.AddWithValue("@pBairro", Bairro)
            End If
            If Not Cidade = Nothing Then
                cmd.Parameters.AddWithValue("@pCidade", Cidade)
            End If
            If IdUF <> 0 Then
                cmd.Parameters.AddWithValue("@pIdUF", IdUF)
            End If

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            da.Dispose()

            Dim lista As New List(Of ModeloClienteDataDTO)

            For Each row As DataRow In dt.Rows
                Dim objModeloCliente As New ModeloClienteDataDTO

                objModeloCliente.Nome = row("nome").ToString
                objModeloCliente.SiglaUF = row("sigla").ToString
                objModeloCliente.Bairro = row("bairro").ToString
                objModeloCliente.Cidade = row("cidade").ToString
                objModeloCliente.Logradouro = row("logradouro").ToString
                objModeloCliente.NumeroResidencia = row("numeroResidencia").ToString
                objModeloCliente.Complemento = row("complemento").ToString
                objModeloCliente.Email = row("email").ToString
                objModeloCliente.Telefone = row("telefone").ToString
                objModeloCliente.CEP = row("cep").ToString

                lista.Add(objModeloCliente)
            Next

            trans.Commit()
            Return lista
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            fecharConexao(conn)
        End Try
    End Function
End Class
