Public Class ModeloFuncionario
    Inherits ModeloPessoa

    Private _IdFuncionario As Integer
    Private _Usuario As String
    Private _Senha As String
    Private _PrimeiroAcesso As Boolean
    Private _IdPerfil As Integer
    Private _Descricao As String

   
    Public Property IdPerfil() As Integer
        Get
            Return _IdPerfil
        End Get
        Set(ByVal value As Integer)
            _IdPerfil = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property


    Public Property IdFuncionario() As Integer
        Get
            Return _IdFuncionario
        End Get
        Set(ByVal value As Integer)
            _IdFuncionario = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return _Usuario
        End Get
        Set(ByVal value As String)
            _Usuario = value
        End Set
    End Property

    Public Property Senha() As String
        Get
            Return _Senha
        End Get
        Set(ByVal value As String)
            _Senha = value
        End Set
    End Property

    Public Property PrimeiroAcesso() As Boolean
        Get
            Return _PrimeiroAcesso
        End Get
        Set(ByVal value As Boolean)
            _PrimeiroAcesso = value
        End Set
    End Property

End Class
