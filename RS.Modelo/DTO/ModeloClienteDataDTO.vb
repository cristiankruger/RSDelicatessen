Public Class ModeloClienteDataDTO
    Inherits ModeloCliente

    Private _SiglaUF As String
    Private _Descricao As String
    Private _DataComemorativa As String
    'Private _Telefone As String
    Private _Anos As String

    Public Property SiglaUF() As String
        Get
            Return _SiglaUF
        End Get
        Set(ByVal value As String)
            _SiglaUF = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Public Property DataComemorativa() As String
        Get
            Return _DataComemorativa
        End Get
        Set(ByVal value As String)
            _DataComemorativa = value
        End Set
    End Property

    'Public Property Telefone() As String
    '    Get
    '        Return _Telefone
    '    End Get
    '    Set(ByVal value As String)
    '        _Telefone = value
    '    End Set
    'End Property

    Public Property Anos() As String
        Get
            Return _Anos
        End Get
        Set(ByVal value As String)
            _Anos = value
        End Set
    End Property

End Class
