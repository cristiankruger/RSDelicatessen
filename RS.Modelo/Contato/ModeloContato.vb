Public Class ModeloContato
    Private _IdContato As Integer
    Private _Telefone As String
    Private _Status As Boolean
    Private _IdPessoa As Integer

    Public Property IdContato() As Integer
        Get
            Return _IdContato
        End Get
        Set(ByVal value As Integer)
            _IdContato = value
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
