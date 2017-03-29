Public Class ModeloPerfilCategoria
    Private _ID As Integer
    Private _IdPerfil As Integer
    Private _IdCategoria As String

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
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

    Public Property IdCategoria() As String
        Get
            Return _IdCategoria
        End Get
        Set(ByVal value As String)
            _IdCategoria = value
        End Set
    End Property

End Class
