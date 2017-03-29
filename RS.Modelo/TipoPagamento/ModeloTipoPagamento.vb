Public Class ModeloTipoPagamento
    Private _Status As Boolean
    Private _Descricao As String
    Private _IdTipoPagamento As Integer

    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(value As Boolean)
            _Status = value
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

    Public Property IdTipoPagamento() As Integer
        Get
            Return _IdTipoPagamento
        End Get
        Set(value As Integer)
            _IdTipoPagamento = value
        End Set
    End Property
End Class
