Public Class ModeloMensagemSistema
    Private _IdParametro As Integer
    Private _Tipo As String
    Private _Nome As String
    Private _Valor As String
    Private _Status As Integer

    Public Property IdParametro() As Integer
        Get
            Return _IdParametro
        End Get
        Set(ByVal value As Integer)
            _IdParametro = value
        End Set
    End Property

    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return _Valor
        End Get
        Set(ByVal value As String)
            _Valor = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

End Class
