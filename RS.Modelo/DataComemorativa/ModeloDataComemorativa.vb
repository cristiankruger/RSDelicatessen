Public Class ModeloDataComemorativa
    Private _Descricao As String
    Private _DataComemorativa As Date
    Private _IdDataComemorativa As Integer
    Private _Status As Boolean
    Private _IdPessoa As Integer

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Public Property DataComemorativa() As Date
        Get
            Return _DataComemorativa
        End Get
        Set(ByVal value As Date)
            _DataComemorativa = value
        End Set
    End Property

    Public Property IdDataComemorativa() As Integer
        Get
            Return _IdDataComemorativa
        End Get
        Set(ByVal value As Integer)
            _IdDataComemorativa = value
        End Set
    End Property

    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property

    Public Property IdPessoa() As Integer
        Get
            Return _IdPessoa
        End Get
        Set(ByVal value As Integer)
            _IdPessoa = value
        End Set
    End Property
End Class
