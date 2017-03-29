Public Class ModeloProduto
    Private _IdProduto As Integer
    Private _IdCategoria As Integer
    Private _DataValidade As Date
    Private _DataFabricacao As Date
    Private _Nome As String
    Private _Foto As String
    Private _Descricao As String
    Private _Status As Boolean
    Private _Valor As String

    Public Property IdProduto() As Integer
        Get
            Return _IdProduto
        End Get
        Set(value As Integer)
            _IdProduto = value
        End Set
    End Property

    Public Property IdCategoria() As Integer
        Get
            Return _IdCategoria
        End Get
        Set(value As Integer)
            _IdCategoria = value
        End Set
    End Property

    Public Property DataValidade() As Date
        Get
            Return _DataValidade
        End Get
        Set(value As Date)
            _DataValidade = value
        End Set
    End Property

    Public Property DataFabricacao() As Date
        Get
            Return _DataFabricacao
        End Get
        Set(value As Date)
            _DataFabricacao = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(value As String)
            _Nome = value
        End Set
    End Property

    Public Property Foto() As String
        Get
            Return _Foto
        End Get
        Set(value As String)
            _Foto = value
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

    Public Property Valor() As String
        Get
            Return _Valor
        End Get
        Set(value As String)
            _Valor = value
        End Set
    End Property
End Class
