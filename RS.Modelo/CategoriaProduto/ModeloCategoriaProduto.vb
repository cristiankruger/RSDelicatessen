Public Class ModeloCategoriaProduto
    Private _IdCategoria As Integer
    Private _Descricao As String
    Private _Status As Boolean

    Public Property IdCategoria() As Integer
        Get
            Return _IdCategoria
        End Get
        Set(value As Integer)
            _IdCategoria = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(value As String)
            _Descricao = value
        End Set
    End Property

    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(value As Boolean)
            _Status = value
        End Set
    End Property

End Class
