Public Class ModuloFuncionarioPerfilDTO
    Private _IdModuloFuncionarioPerfil As Integer
    Private _IdModulo As Integer
    Private _IdPerfil As Integer
    Private _IdPessoa As Integer
    Private _Email As String
    Private _Nome As String
    Private _Bairro As String
    Private _CEP As String
    Private _Cidade As String
    Private _Complemento As String
    Private _CPF As String
    Private _Logradouro As String
    Private _NumeroResidencia As String
    Private _RG As String
    Private _Status As Boolean
    Private _IdFuncionario As Integer
    Private _IdUF As Integer
    Private _Usuario As String
    Private _PrimeiroAcesso As Boolean
    Private _Descricao As String

    Public Property IdModuloFuncionarioPerfil() As Integer
        Get
            Return _IdModuloFuncionarioPerfil
        End Get
        Set(ByVal value As Integer)
            _IdModuloFuncionarioPerfil = value
        End Set
    End Property

    Public Property IdUF() As Integer
        Get
            Return _IdUF
        End Get
        Set(ByVal value As Integer)
            _IdUF = value
        End Set
    End Property


    Public Property IdModulo() As Integer
        Get
            Return _IdModulo
        End Get
        Set(ByVal value As Integer)
            _IdModulo = value
        End Set
    End Property

    Public Property IdPerfil() As Integer
        Get
            Return _IdPerfil
        End Get
        Set(ByVal value As Integer)
            _IdPerfil = value
        End Set
    End Property

    Public Property IdPessoa() As Integer
        Get
            Return _IdPessoa
        End Get
        Set(ByVal value As Integer)
            _IdPessoa = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property Bairro() As String
        Get
            Return _Bairro
        End Get
        Set(ByVal value As String)
            _Bairro = value
        End Set
    End Property

    Public Property CEP() As String
        Get
            Return _CEP
        End Get
        Set(ByVal value As String)
            _CEP = value
        End Set
    End Property

    Public Property Cidade() As String
        Get
            Return _Cidade
        End Get
        Set(ByVal value As String)
            _Cidade = value
        End Set
    End Property

    Public Property Complemento() As String
        Get
            Return _Complemento
        End Get
        Set(ByVal value As String)
            _Complemento = value
        End Set
    End Property

    Public Property CPF() As String
        Get
            Return _CPF
        End Get
        Set(ByVal value As String)
            _CPF = value
        End Set
    End Property

    Public Property Logradouro() As String
        Get
            Return _Logradouro
        End Get
        Set(ByVal value As String)
            _Logradouro = value
        End Set
    End Property

    Public Property NumeroResidencia() As String
        Get
            Return _NumeroResidencia
        End Get
        Set(ByVal value As String)
            _NumeroResidencia = value
        End Set
    End Property

    Public Property RG() As String
        Get
            Return _RG
        End Get
        Set(ByVal value As String)
            _RG = value
        End Set
    End Property

   Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
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

    Public Property PrimeiroAcesso() As Boolean
        Get
            Return _PrimeiroAcesso
        End Get
        Set(ByVal value As Boolean)
            _PrimeiroAcesso = value
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

End Class
