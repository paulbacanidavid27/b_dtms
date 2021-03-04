Imports System.Data.SqlClient
Imports System.Data.Odbc

Public Class DbTran
    Dim loTran As SqlTransaction

    Public Sub New()
    End Sub

    Public Property pTran As SqlTransaction
        Get
            Return loTran
        End Get
        Set(ByVal value As SqlTransaction)
            loTran = value
        End Set
    End Property
    Public Sub Dispose()
        If Not loTran Is Nothing Then
            loTran.Dispose()
            loTran = Nothing
        End If
    End Sub
End Class

