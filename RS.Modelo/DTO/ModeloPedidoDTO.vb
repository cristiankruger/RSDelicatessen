Public Class ModeloPedidoDTO
    Inherits ModeloPedido

    Private _SituacaoPedido As String
    Private _NomeCliente As String
    Private _NomeFuncionario As String
    Private _TelefoneCliente As String
    Private _ValorPedido As String
    Private _Email As String

    Public Property SituacaoPedido() As String
        Get
            Return _SituacaoPedido
        End Get
        Set(value As String)
            _SituacaoPedido = value
        End Set
    End Property

    Public Property NomeCliente() As String
        Get
            Return _NomeCliente
        End Get
        Set(value As String)
            _NomeCliente = value
        End Set
    End Property

    Public Property NomeFuncionario() As String
        Get
            Return _NomeFuncionario
        End Get
        Set(value As String)
            _NomeFuncionario = value
        End Set
    End Property

    Public Property TelefoneCliente() As String
        Get
            Return _TelefoneCliente
        End Get
        Set(value As String)
            _TelefoneCliente = value
        End Set
    End Property

    Public Property ValorPedido() As String
        Get
            Return _ValorPedido
        End Get
        Set(value As String)
            _ValorPedido = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property
End Class
