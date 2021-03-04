Public Class docPurge

    Dim ProcessedRecords As String
    Dim ProcessId As String
    Dim ProcessDate As String
    Dim EmailSent As String
    Dim ExecutionTimeInMinutes As String
    Dim DeletedRecords As String
    Dim emailexception As String

    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String

    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim RowsPerPage As String
    Dim Idx As String




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



    Public Property pProcessId() As String
        Get
            Return ProcessId
        End Get
        Set(ByVal value As String)
            ProcessId = value
        End Set

    End Property
    Public Property pProcessedRecords() As String
        Get
            Return ProcessedRecords
        End Get
        Set(ByVal value As String)
            ProcessedRecords = value
        End Set

    End Property

    Public Property pProcessDate() As String
        Get
            Return ProcessDate
        End Get
        Set(ByVal value As String)
            ProcessDate = value
        End Set

    End Property

    Public Property pEmailSent() As String
        Get
            Return ProcessId
        End Get
        Set(ByVal value As String)
            ProcessId = value
        End Set

    End Property
    Public Property pExecutionTimeInMinutes() As String
        Get
            Return ExecutionTimeInMinutes
        End Get
        Set(ByVal value As String)
            ExecutionTimeInMinutes = value
        End Set

    End Property

    Public Property pDeletedRecords() As String
        Get
            Return DeletedRecords
        End Get
        Set(ByVal value As String)
            DeletedRecords = value
        End Set

    End Property

    Public Property pEmailException() As String
        Get
            Return emailexception
        End Get
        Set(ByVal value As String)
            emailexception = value
        End Set

    End Property

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


    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set

    End Property

    Public Sub New()

    End Sub
    Public Function RetrieveRecordsByPage() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select * from (SELECT " & _
                          "row_number() over (ORDER BY " & s_order & " " & pSortOrder & ") as rn, " & _
                "cast(ppl.ProcessId as nvarchar) as ProcessId, " & _
                "ppl.ProcessDate, " & _
                "ppl.ProcessedRecords, " & _
                "ppl.EmailSent, " & _
                "ppl.DeletedRecords, " & _
                "ppl.ExecutionTimeInMinutes, " & _
                "ppl.EmailException "

                sSQL = sSQL & "FROM  " & _
                " PurgedProcessLog ddl " & _
               "WHERE rownum <= " & lTop.ToString & " " & WhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                         "rn= row_number() over (ORDER BY " & s_order & " " & pSortOrder & "), " & _
                 "ppl.ProcessId, " & _
                "ppl.ProcessDate, " & _
                "ppl.ProcessedRecords, " & _
                "ppl.EmailSent, " & _
                "ppl.DeletedRecords, " & _
                "ppl.ExecutionTimeInMinutes, " & _
                "ppl.EmailException "

                sSQL = sSQL & "FROM  " & _
                " PurgedProcessLog ppl " & _
               "WHERE ppl.ProcessId is not null " & WhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            End If

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = sSQL



            ldata = objCommand.Fill

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

    Public Function RetrieveRecords() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "select processid,processdate,processedrecords,emailsent,executiontimeinminutes,deletedrecords,emailexception from PurgedProcessLog order by processdate desc "

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = sSQL



            ldata = objCommand.Fill

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

    Private Function OrderBy() As String
        If pSortCol = "Process Date" Then
            Return " ppl.processdate"
        Else
            Return " ppl.processdate"
        End If

    End Function



    Private Function WhereClause() As String
        Dim lswhere As String = ""

        If pProcessDate <> "" Then
            lswhere = lswhere & " AND ppl.ProcessDate like '%" & Replace(pProcessDate, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    
    

    Public Function CountDocPurge() As Integer

        Dim objCommand As clsSqlConn

        'Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "PurgedProcessLog ppl " & _
           "WHERE ppl.ProcessId is not null " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





            Return objCommand.ExecScalar


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

   
End Class
