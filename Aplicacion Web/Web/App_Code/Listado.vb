Imports Microsoft.VisualBasic

Public Class Listado
    Private vCodigo As String
    Private vDescripcion As String

    Public Property Codigo() As String
        Get
            Codigo = vCodigo
        End Get
        Set(ByVal value As String)
            vCodigo = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = vDescripcion
        End Get
        Set(ByVal value As String)
            vDescripcion = value
        End Set
    End Property

    Public Sub New(ByVal COD As String, ByVal DESC As String)
        vCodigo = COD
        vDescripcion = DESC
    End Sub

End Class
