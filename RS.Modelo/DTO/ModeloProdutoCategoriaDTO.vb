Public Class ModeloProdutoCategoriaDTO
    Inherits ModeloProduto
    Private _Categoria As String
    Private _StatusCategoria As Boolean
    Private _ProdutoCategoria As String
    Private _ValorTotal As String
    Private _QuantidadeVendida As Integer

    Public Property Categoria() As String
        Get
            Return _Categoria
        End Get
        Set(value As String)
            _Categoria = value
        End Set
    End Property

    Public Property StatusCategoria() As Boolean
        Get
            Return _StatusCategoria
        End Get
        Set(value As Boolean)
            _StatusCategoria = value
        End Set
    End Property

    Public Property ProdutoCategoria() As String
        Get
            Return _ProdutoCategoria
        End Get
        Set(value As String)
            _ProdutoCategoria = value
        End Set
    End Property

    Public Property ValorTotal() As String
        Get
            Return _ValorTotal
        End Get
        Set(value As String)
            _ValorTotal = value
        End Set
    End Property

    Public Property QuantidadeVendida() As Integer
        Get
            Return _QuantidadeVendida
        End Get
        Set(value As Integer)
            _QuantidadeVendida = value
        End Set
    End Property
End Class
