Public Class ModeloParcela
    Private _IdParcela As Integer
    Private _IdPagamento As Integer
    Private _idTipoPagamento As Integer
    Private _DataVencimento As Date
    Private _DataPagamento As String
    Private _ValorParcela As String
    Private _Status As Boolean
    Private _StatusEstorno As Boolean

    Public Property IdParcela() As Integer
        Get
            Return _IdParcela
        End Get
        Set(value As Integer)
            _IdParcela = value
        End Set
    End Property

    Public Property IdPagamento() As Integer
        Get
            Return _IdPagamento
        End Get
        Set(value As Integer)
            _IdPagamento = value
        End Set
    End Property

    Public Property IdTipoPagamento() As Integer
        Get
            Return _idTipoPagamento
        End Get
        Set(value As Integer)
            _idTipoPagamento = value
        End Set
    End Property

    Public Property DataVencimento() As Date
        Get
            Return _DataVencimento
        End Get
        Set(value As Date)
            _DataVencimento = value
        End Set
    End Property

    Public Property DataPagamento() As String
        Get
            Return _DataPagamento
        End Get
        Set(value As String)
            _DataPagamento = value
        End Set
    End Property

    Public Property ValorParcela() As String
        Get
            Return _ValorParcela
        End Get
        Set(value As String)
            _ValorParcela = value
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

    Public Property StatusEstorno() As Boolean
        Get
            Return _StatusEstorno
        End Get
        Set(value As Boolean)
            _StatusEstorno = value
        End Set
    End Property
End Class
