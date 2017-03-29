Public Class ModeloPagamento
    Private _IdPagamento As Integer
    Private _IdPedido As Integer
    Private _IdPessoa As Integer
    Private _Desconto As Integer
    Private _NumeroeParcelas As Integer
    Private _DataInicial As Date
    Private _Observacao As String
    Private _Status As Boolean
    Private _SaldoPago As Double

    Public Property IdPagamento() As Integer
        Get
            Return _IdPagamento
        End Get
        Set(value As Integer)
            _IdPagamento = value
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

    Public Property IdPessoa() As Integer
        Get
            Return _IdPessoa
        End Get
        Set(value As Integer)
            _IdPessoa = value
        End Set
    End Property

    Public Property Desconto() As Integer
        Get
            Return _Desconto
        End Get
        Set(value As Integer)
            _Desconto = value
        End Set
    End Property

    Public Property NumeroDeParcelas() As Integer
        Get
            Return _NumeroeParcelas
        End Get
        Set(value As Integer)
            _NumeroeParcelas = value
        End Set
    End Property

    Public Property DataInicial() As Date
        Get
            Return _DataInicial
        End Get
        Set(value As Date)
            _DataInicial = value
        End Set
    End Property

    Public Property Observacao() As String
        Get
            Return _Observacao
        End Get
        Set(value As String)
            _Observacao = value
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

    Public Property SaldoPago() As Double
        Get
            Return _SaldoPago
        End Get
        Set(value As Double)
            _SaldoPago = value
        End Set
    End Property
End Class
