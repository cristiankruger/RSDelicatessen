Public Class ModeloSituacaoPedido
    Private _IdSituacaoPedido As Integer
    Private _SituacaoPedido As String

    Public Property IdSituacaoPedido() As Integer
        Get
            Return _IdSituacaoPedido
        End Get
        Set(value As Integer)
            _IdSituacaoPedido = value
        End Set
    End Property

    Public Property SituacaoPedido() As String
        Get
            Return _SituacaoPedido
        End Get
        Set(value As String)
            _SituacaoPedido = value
        End Set
    End Property
End Class
