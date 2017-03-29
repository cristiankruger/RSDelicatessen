Public Class ModeloUF
    Private _IdUF As Integer
    Private _Sigla As String
    Private _NomeEstado As String

    Public Property IdUF() As Integer
        Get
            Return _IdUF
        End Get
        Set(ByVal value As Integer)
            _IdUF = value
        End Set
    End Property

    Public Property Sigla() As String
        Get
            Return _Sigla
        End Get
        Set(ByVal value As String)
            _Sigla = value
        End Set
    End Property

    Public Property NomeEstado() As String
        Get
            Return _NomeEstado
        End Get
        Set(ByVal value As String)
            _NomeEstado = value
        End Set
    End Property
End Class
