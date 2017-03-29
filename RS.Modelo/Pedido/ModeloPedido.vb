Public Class ModeloPedido
    Private _IdPedido As Integer
    Private _IdCliente As Integer
    Private _IdFuncionario As Integer
    Private _IdSituacao As Integer
    Private _Status As Boolean
    Private _DataPedido As Date
    Private _DataValidade As String
    Private _Descricao As String

    Public Property IdPedido() As Integer
        Get
            Return _IdPedido
        End Get
        Set(value As Integer)
            _IdPedido = value
        End Set
    End Property

    Public Property IdCliente() As Integer
        Get
            Return _IdCliente
        End Get
        Set(value As Integer)
            _IdCliente = value
        End Set
    End Property

    Public Property IdFuncionario() As Integer
        Get
            Return _IdFuncionario
        End Get
        Set(value As Integer)
            _IdFuncionario = value
        End Set
    End Property

    Public Property IdSituacao() As Integer
        Get
            Return _IdSituacao
        End Get
        Set(value As Integer)
            _IdSituacao = value
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

    Public Property DataPedido() As Date
        Get
            Return _DataPedido
        End Get
        Set(value As Date)
            _DataPedido = value
        End Set
    End Property

    Public Property DataValidade() As String
        Get
            Return _DataValidade
        End Get
        Set(value As String)
            _DataValidade = value
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
End Class
