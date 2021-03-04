Imports System.Data.SqlClient
Imports System.Data.Odbc
Public Class DbParam
    Dim loParam As SqlParameter

    Public Sub New()
    End Sub

    Public Property pParam As SqlParameter
        Get
            Return loParam
        End Get
        Set(ByVal value As SqlParameter)
            loParam = value
        End Set
    End Property
    Public Sub Dispose()
        If Not loParam Is Nothing Then
            loParam = Nothing
        End If
    End Sub
End Class

