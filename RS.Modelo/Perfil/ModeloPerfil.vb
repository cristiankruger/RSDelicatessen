Public Class ModeloPerfil
    Private _IdPerfil As Integer
    Private _Descricao As String
    Private _Status As Integer

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

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

End Class
