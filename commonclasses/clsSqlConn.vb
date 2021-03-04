Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Data.Odbc
Public Class clsSqlConn
    Dim sqlC As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    Dim isOracle As Boolean = IIf(System.Configuration.ConfigurationManager.AppSettings("oracledb") = "1", True, False)
    'Dim sqlC As String = "dsn=sndocuvu;uid=dbo_docuvu;pwd=dbo_docuvu;"
    Dim sqlCTime As String = IIf(IsNothing(System.Configuration.ConfigurationManager.AppSettings("commandtimeout")), "0", System.Configuration.ConfigurationManager.AppSettings("commandtimeout"))
    Dim sSqlString As String
    Dim sCommType As String
    'Dim oSqlC As OracleConnection
    'Dim oSqlComm As OracleCommand
    'Dim oSqlC As OdbcConnection
    'Dim oSqlComm As OdbcCommand
    Dim oSqlC As SqlConnection
    Dim oSqlComm As SqlCommand
    ''Dim oTran As SqlTransaction
    ''Dim oSqlA As SqlDataAdapter
    'Dim oData As DataTable
    'Dim oRdr As OracleDataReader
    'Dim retval As OracleParameter
    'Dim oRdr As OdbcDataReader
    'Dim retval As OdbcParameter
    'Dim oRdr As SqlDataReader
    Dim retval As SqlParameter

    Public Property pSqlConnection As SqlConnection
        Get
            Return oSqlC
        End Get
        Set(ByVal value As SqlConnection)
            oSqlC = value
        End Set
    End Property

    Public Property pCommandTimeout As Integer
        Get
            If sqlCTime.Trim <> "" AndAlso IsNumeric(sqlCTime.Trim) Then
                Return CInt(sqlCTime)
            Else
                Return 180
            End If
        End Get
        Set(ByVal value As Integer)
            sqlCTime = value
        End Set
    End Property

    'Public Property pSqlTransaction As SqlTransaction
    '    Get
    '        Return oTran
    '    End Get
    '    Set(ByVal value As SqlTransaction)
    '        oTran = value
    '    End Set
    'End Property

    'Public Property pSqlAdapter As SqlDataAdapter
    '    Get
    '        Return oSqlA
    '    End Get
    '    Set(ByVal value As SqlDataAdapter)
    '        oSqlA = value
    '    End Set
    'End Property

    Public Property pSqlCommand As SqlCommand
        Get
            Return oSqlComm
        End Get
        Set(ByVal value As SqlCommand)
            oSqlComm = value
        End Set
    End Property


    Public Property pConnectionString As String
        Get
            Return sqlC
        End Get
        Set(ByVal value As String)
            sqlC = value
        End Set
    End Property

    Public Property pSqlString() As String
        Get
            Return sSqlString
        End Get
        Set(ByVal value As String)
            If pOracle Then
                Replace(value, "isnull(", "NVL(")
            End If
            sSqlString = value
        End Set
    End Property

    Public Property pCommandType() As String
        Get
            Return sCommType
        End Get
        Set(ByVal value As String)
            sCommType = value
        End Set
    End Property

    Public ReadOnly Property pOracle() As Boolean
        Get
            Return isOracle
        End Get

    End Property

    Public Property pCommandText() As String
        Get
            Return sSqlString
        End Get
        Set(ByVal value As String)
            If pOracle Then
                Replace(value, "isnull(", "NVL(")
            End If
            sSqlString = value
        End Set
    End Property

    Public Property CommandType() As String
        Get
            Return sCommType
        End Get
        Set(ByVal value As String)
            sCommType = value
        End Set
    End Property



    Public Property CommandText() As String
        Get
            Return sSqlString
        End Get
        Set(ByVal value As String)
            If pOracle Then
                Replace(value, "isnull(", "NVL(")
            End If
            sSqlString = value
        End Set
    End Property

    Public Sub New()
        oSqlC = New SqlConnection(pConnectionString)
        oSqlC.Open()
        oSqlComm = New SqlCommand
        oSqlComm.Connection = oSqlC
        oSqlComm.CommandTimeout = pCommandTimeout

    End Sub

    Public Sub New(ByRef pTran As SqlTransaction)
        oSqlC = New SqlConnection(pConnectionString)
        oSqlC.Open()
        pTran = oSqlC.BeginTransaction
        oSqlComm = New SqlCommand



        oSqlComm.Transaction = pTran
        oSqlComm.Connection = oSqlC
        oSqlComm.CommandTimeout = pCommandTimeout

    End Sub

    Public Sub AddParameters(ByVal asParam As String, ByVal asValue As String)
        pSqlCommand.Parameters.AddWithValue(asParam, asValue)
    End Sub

    Public Sub ParametersAddWithValue(ByVal asParam As String, ByVal asValue As String)
        pSqlCommand.Parameters.AddWithValue(asParam, asValue)
    End Sub
    Public Sub ParametersReturnValue()
        retval = pSqlCommand.Parameters.AddWithValue("ReturnVal", SqlDbType.Int)
        retval.Direction = ParameterDirection.ReturnValue
    End Sub

    Public Property RetValue() As SqlParameter
        Get
            Return retval
        End Get
        Set(ByVal value As SqlParameter)
            retval = value
        End Set
    End Property


    Public Sub ClearParameter()
        pSqlCommand.Parameters.Clear()
    End Sub

    Public Sub ParametersClear()
        pSqlCommand.Parameters.Clear()
    End Sub

    Public Sub Dispose()
        If Not oSqlComm Is Nothing Then
            oSqlComm.Dispose()
            oSqlComm = Nothing
        End If
        If Not oSqlC Is Nothing Then
            oSqlC.Close()
            oSqlC.Dispose()
            oSqlC = Nothing
        End If
    End Sub

    Public Function ExecData() As DataTable
        Dim lodata As DataTable
        Dim oSqlA As SqlDataAdapter
        Try
            lodata = New DataTable
            oSqlA = New SqlDataAdapter
            'oSqlComm.Connection = oSqlC
            oSqlComm.CommandText = pSqlString
            oSqlComm.CommandType = pCommandType
            oSqlA.SelectCommand = oSqlComm
            oSqlA.Fill(lodata)

            Return lodata

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If

            If Not oSqlA Is Nothing Then
                oSqlA.Dispose()
                oSqlA = Nothing
            End If

            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try


    End Function

    Public Function ExecDataSet() As DataSet
        Dim lodata As DataSet
        Dim oSqlA As SqlDataAdapter
        Try
            lodata = New DataSet
            oSqlA = New SqlDataAdapter

            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pSqlString
            pSqlCommand.CommandType = pCommandType
            oSqlA.SelectCommand = pSqlCommand
            oSqlA.Fill(lodata)

            Return lodata

        Catch ex As Exception
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If

            If Not oSqlA Is Nothing Then
                oSqlA.Dispose()
                oSqlA = Nothing
            End If

            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try

    End Function


    Public Function Fill() As DataTable
        Dim lodata As DataTable
        Dim oSqlA As SqlDataAdapter
        Try
            lodata = New DataTable
            oSqlA = New SqlDataAdapter
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pSqlString
            pSqlCommand.CommandType = pCommandType
            oSqlA.SelectCommand = pSqlCommand
            oSqlA.Fill(lodata)

            Return lodata

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If

            If Not oSqlA Is Nothing Then
                oSqlA.Dispose()
                oSqlA = Nothing
            End If

            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try
    End Function

    Public Function ExecScalar() As Integer

        'Dim oSqlA As SqlDataAdapter
        Try

            'oSqlA = New SqlDataAdapter
            'pSqlConnection.Open()
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType
            Return pSqlCommand.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not oSqlA Is Nothing Then
            '    oSqlA.Dispose()
            '    oSqlA = Nothing
            'End If

            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try



    End Function

    Public Function ExecScalar3() As String

        'Dim oSqlA As SqlDataAdapter
        Try

            'oSqlA = New SqlDataAdapter
            'pSqlConnection.Open()
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType
            Return pSqlCommand.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not oSqlA Is Nothing Then
            '    oSqlA.Dispose()
            '    oSqlA = Nothing
            'End If

            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try
    End Function

    Public Function ExecScalar2() As Integer
        Try

            'oSqlA = New SqlDataAdapter
            'pSqlConnection.Open()
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType
            Return pSqlCommand.ExecuteScalar()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try



    End Function

    Public Function ExecHasRow() As Boolean
        Dim rdr As SqlDataReader
        Try

            'pSqlConnection.Open()
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType

            rdr = pSqlCommand.ExecuteReader
            Return rdr.HasRows
        Catch ex As Exception
        Finally
            If Not rdr Is Nothing Then
                rdr.Close()
                rdr.Dispose()
                rdr = Nothing
            End If

            'If Not oSqlComm Is Nothing Then
            '    oSqlComm.Dispose()
            '    oSqlComm = Nothing
            'End If
            'If Not oSqlC Is Nothing Then
            '    oSqlC.Close()
            '    oSqlC.Dispose()
            '    oSqlC = Nothing
            'End If
        End Try



    End Function


    Public Sub ExecNonQuery()


        Try
            'pSqlConnection.Open()
            'pSqlCommand.Connection = pSqlConnection
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType
            pSqlCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not oSqlComm Is Nothing Then
                oSqlComm.Dispose()
                oSqlComm = Nothing
            End If
            If Not oSqlC Is Nothing Then
                oSqlC.Close()
                oSqlC.Dispose()
                oSqlC = Nothing
            End If
        End Try



    End Sub


    Public Sub ExecTranNonQuery()


        Try
            pSqlCommand.CommandText = pCommandText
            pSqlCommand.CommandType = pCommandType
            pSqlCommand.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try



    End Sub


End Class
