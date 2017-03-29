Public Class ModeloModuloFuncionarioPerfil
    Private _IdModuloFuncionarioPerfil As Integer
    Private _IdModulo As Integer
    Private _IdPessoa As Integer
    Private _IdPerfil As Integer

    Public Property IdModuloFuncionarioPerfil() As Integer
        Get
            Return _IdModuloFuncionarioPerfil
        End Get
        Set(ByVal value As Integer)
            _IdModuloFuncionarioPerfil = value
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

    Public Property IdPessoa() As Integer
        Get
            Return _IdPessoa
        End Get
        Set(ByVal value As Integer)
            _IdPessoa = value
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
End Class
