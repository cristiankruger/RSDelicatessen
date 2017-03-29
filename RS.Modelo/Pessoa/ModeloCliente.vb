Public Class ModeloCliente
    Inherits ModeloPessoa

    Private _Foto As String
    Private _Telefone As String

    Public Property Foto() As String
        Get
            Return _Foto
        End Get
        Set(ByVal value As String)
            _Foto = value
        End Set
    End Property

    Public Property Telefone() As String
        Get
            Return _Telefone
        End Get
        Set(ByVal value As String)
            _Telefone = value
        End Set
    End Property

End Class
