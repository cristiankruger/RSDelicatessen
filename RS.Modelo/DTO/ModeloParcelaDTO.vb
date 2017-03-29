Public Class ModeloParcelaDTO
    Inherits ModeloParcela
    Private _Descricao As String
    Private _Situacao As String
    Private _Estorno As String

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(value As String)
            _Descricao = value
        End Set
    End Property

    Public Property Situacao() As String
        Get
            Return _Situacao
        End Get
        Set(value As String)
            _Situacao = value
        End Set
    End Property

    Public Property Estorno() As String
        Get
            Return _Estorno
        End Get
        Set(value As String)
            _Estorno = value
        End Set
    End Property
End Class
