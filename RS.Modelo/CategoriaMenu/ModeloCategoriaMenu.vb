Public Class ModeloCategoriaMenu
    Private _IdCategoria As Integer
    Private _Categoria As String
    Private _Tipo As String
    Private _Link As String

    Public Property IdCategoria() As Integer
        Get
            Return _IdCategoria
        End Get
        Set(ByVal value As Integer)
            _IdCategoria = value
        End Set
    End Property

    Public Property Categoria() As String
        Get
            Return _Categoria
        End Get
        Set(ByVal value As String)
            _Categoria = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property

    Public Property Link() As String
        Get
            Return _Link
        End Get
        Set(ByVal value As String)
            _Link = value
        End Set
    End Property
End Class
