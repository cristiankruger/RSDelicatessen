Public Class ModeloItemDTO
    Private _IdItem As String
    Private _IProduto As Integer
    Private _IdPedido As Integer
    Private _Quantidade As Integer
    Private _Status As Boolean
    Private _Valor As Double
    Private _Nome As String

    Public Property IdItem() As String
        Get
            Return _IdItem
        End Get
        Set(value As String)
            _IdItem = value
        End Set
    End Property

    Public Property IdProduto() As Integer
        Get
            Return _IProduto
        End Get
        Set(value As Integer)
            _IProduto = value
        End Set
    End Property

    Public Property IdPedido() As Integer
        Get
            Return _IdPedido
        End Get
        Set(value As Integer)
            _IdPedido = value
        End Set
    End Property

    Public Property Quantidade() As Integer
        Get
            Return _Quantidade
        End Get
        Set(value As Integer)
            _Quantidade = value
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

    Public Property Valor() As Double
        Get
            Return _Valor
        End Get
        Set(value As Double)
            _Valor = value
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
End Class
