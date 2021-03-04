
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Web

Public Class DocBackup

    Dim UserId As String
    Dim FileName As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim RowsPerPage As String
    Dim Idx As String
    Dim ReturnVal As Integer
    Public Sub New()

    End Sub

    Public Property pSortOrder() As String
        Get
            Return SortOrder
        End Get
        Set(ByVal value As String)
            SortOrder = value
        End Set

    End Property

    Public Property pSortCol() As String
        Get
            Return SortCol
        End Get
        Set(ByVal value As String)
            SortCol = value
        End Set

    End Property

    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
        End Set

    End Property

    Public Property pIdx() As String
        Get
            Return Idx
        End Get
        Set(ByVal value As String)
            Idx = value
        End Set

    End Property

    Public Property pReturnVal() As Integer
        Get
            Return ReturnVal
        End Get
        Set(ByVal value As Integer)
            ReturnVal = value
        End Set

    End Property

    Public Property pUserId() As String
        Get
            Return UserId
        End Get
        Set(ByVal value As String)
            UserId = value
        End Set

    End Property

   

    Public Property pFileName() As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
        End Set

    End Property

    

    


    Public Function RetrieveBackup() As DataTable

        Dim objCommand As clsSqlConn
        Dim adpSecurity As New SqlClient.SqlDataAdapter
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "DMSP_BACKUP_GET"

            If pIdx <> "" Then
                objCommand.ParametersAddWithValue("@Idx", CInt(pIdx))
            End If

            If pRowsPerPage <> "" Then
                objCommand.ParametersAddWithValue("@RowsPerPage", CInt(pRowsPerPage))
            End If

            objCommand.ParametersAddWithValue("@SortOrder", pSortOrder)
            objCommand.ParametersAddWithValue("@SortCol", pSortCol)
            objCommand.ParametersAddWithValue("@UserId", pUserId)
            objCommand.ParametersReturnValue()
            'Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            'Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            Dim retval As New DbParam
            retval.pParam = objCommand.RetValue

            ldata = objCommand.Fill

            pReturnVal = retval.pParam.Value
            Return ldata


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try

    End Function

    
End Class
