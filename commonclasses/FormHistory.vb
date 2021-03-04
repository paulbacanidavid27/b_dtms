Public Class FormHistory
    Dim IpAdd As String
    Dim JRFNo As String = ""
    Dim UserId As String
    Dim UserGroup As String
    Dim DocId As String
    Dim Action As String
    Dim ActionDate As String
    Dim ActionDateTo As String
    Dim Task As String
    Dim Title As String
    Dim FileName As String
    Dim TableName As String
    Dim RecordId As String
    Dim ModifiedBy As String
    Dim ColumnName As String
    Dim ModifiedDate As String
    Dim OldValue As String
    Dim NewValue As String
    Dim idx As String
    Dim StartSeqno As String
    Dim EndSeqno As String
    Dim rpp As String
    Dim SortOrder As String
    Dim SortCol As String

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

    Public Property pJRFNo() As String
        Get
            Return JRFNo
        End Get
        Set(ByVal value As String)
            JRFNo = value
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
    Public Property pIdx() As String
        Get
            Return idx
        End Get
        Set(ByVal value As String)
            idx = value
        End Set

    End Property
    Public Property pStartSeqno() As String
        Get
            Return StartSeqno
        End Get
        Set(ByVal value As String)
            StartSeqno = value
        End Set

    End Property
    Public Property pEndSeqNo() As String
        Get
            Return EndSeqno
        End Get
        Set(ByVal value As String)
            EndSeqno = value
        End Set

    End Property
    Public Property pRowsPerPage() As String
        Get
            Return rpp
        End Get
        Set(ByVal value As String)
            rpp = value
        End Set

    End Property
    Public Property pAction() As String
        Get
            Return Action
        End Get
        Set(ByVal value As String)
            Action = value
        End Set

    End Property
    Public Property pActionDate() As String
        Get
            Return ActionDate
        End Get
        Set(ByVal value As String)
            ActionDate = value
        End Set

    End Property
    Public Property pActionDateTo() As String
        Get
            Return ActionDateTo
        End Get
        Set(ByVal value As String)
            ActionDateTo = value
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

    Public Property pTitle() As String
        Get
            Return Title
        End Get
        Set(ByVal value As String)
            Title = value
        End Set

    End Property

    Public Property pTask() As String
        Get
            Return Task
        End Get
        Set(ByVal value As String)
            Task = value
        End Set

    End Property

    Public Property pFormId() As String
        Get
            Return DocId
        End Get
        Set(ByVal value As String)
            DocId = value
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

    Public Property pUserGroup() As String
        Get
            Return UserGroup
        End Get
        Set(ByVal value As String)
            UserGroup = value
        End Set

    End Property

    Public Property pIpAddress() As String
        Get
            Return IpAdd
        End Get
        Set(ByVal value As String)
            IpAdd = value
        End Set

    End Property
    Public Property pNewValue() As String
        Get
            Return NewValue
        End Get
        Set(ByVal value As String)
            NewValue = value
        End Set

    End Property
    Public Property pTableName() As String
        Get
            Return TableName
        End Get
        Set(ByVal value As String)
            TableName = value
        End Set

    End Property
    Public Property pOldValue() As String
        Get
            Return OldValue
        End Get
        Set(ByVal value As String)
            OldValue = value
        End Set

    End Property
    Public Property pModifiedDate() As String
        Get
            Return ModifiedDate
        End Get
        Set(ByVal value As String)
            ModifiedDate = value
        End Set

    End Property
    Public Property pColumnName() As String
        Get
            Return ColumnName
        End Get
        Set(ByVal value As String)
            ColumnName = value
        End Set

    End Property
    Public Property pModifiedBy() As String
        Get
            Return ModifiedBy
        End Get
        Set(ByVal value As String)
            ModifiedBy = value
        End Set

    End Property
    Public Property pRecordId() As String
        Get
            Return RecordId
        End Get
        Set(ByVal value As String)
            RecordId = value
        End Set

    End Property




    Public Sub AddHistory()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()


            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "INSERT INTO FormHistory " &
           "(FormId " &
           ",UserId " &
           ",Action " &
           ",ActionDate " &
           ",IPAddress)  " &
        "VALUES " &
           "(" & pFormId &
           ",'" & pUserId & "'" &
           ",'" & Replace(pAction, "'", "''") & "'" &
           ",GETDATE() " &
           ",'" & pIpAddress & "')"


            objCommand.ExecNonQuery()


        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub

    Public Sub AddHistory(ByVal objCommand As clsSqlConn)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            'objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "INSERT INTO FormHistory " &
           "(FormId " &
           ",UserId " &
           ",Action " &
           ",ActionDate " &
           ",IPAddress)  " &
        "VALUES " &
           "(" & pFormId &
           ",'" & pUserId & "'" &
           ",'" & Replace(pAction, "'", "''") & "'" &
           ",GETDATE() " &
           ",'" & pIpAddress & "')"

            objCommand.ExecTranNonQuery()

            'ltr.pTran.Commit()

        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If


        End Try

    End Sub




    Public Function GetHistoryCount() As Integer



        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT COUNT(fh.formid) FROM formhistory fh inner join docForms df on dh.docid = df.docid  WHERE df.statusid <> 5 "
            If pUserId <> "" Then
                s_sql = s_sql & " AND fh.UserId = '" & pUserId & "' "
            End If
            If pFormId <> "" Then
                s_sql = s_sql & " AND fh.FormId= " & pFormId & " "
            End If
            If pAction <> "" Then
                s_sql = s_sql & " AND fh.Action = '" & pAction & "'"
            End If
            If pIpAddress <> "" Then
                s_sql = s_sql & " AND fh.IPAddress = '" & pIpAddress & "'"
            End If
            If pActionDate <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND TO_CHAR(fh.actiondate,'mm/dd/yyyy') >= '" & pActionDate & "' "
                Else
                    s_sql = s_sql & " AND fh.actiondate >= convert(datetime,'" & pActionDate & "') "
                End If

            End If
            If pActionDateTo <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND TO_CHAR(dh.actiondate,'mm/dd/yyyy') <= '" & pActionDateTo & "' "
                Else
                    s_sql = s_sql & " AND fh.actiondate < dateadd(day,1,'" & pActionDateTo & "') "
                End If

            End If

            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try
    End Function



    Public Function GetHistory() As DataTable

        Dim objCommand As clsSqlConn

        Try
            Dim s_where As String = ""

            Dim s_sql As String

            If DocSession.OraClient Then
                s_sql = "SELECT t.*,(case when t.docid > 0 then NVL('('||dl.title ||')','') else ' ' end) AS doctitle " &
",(u.FirstName||' '||u.LastName) AS uname FROM " &
"( " &
 "SELECT " &
   "row_number() over (order by dh.actiondate desc) AS rn" &
           ",dh.Action " &
           ",dh.docid " &
           ",dh.actiondate " &
            ",dh.UserId "

                s_sql = s_sql & ",NVL(dh.ApproverId,' ') AS task" &
                   ",(case when TO_CHAR(dh.actiondate,'mm/dd/yyyy') = TO_CHAR(SYSDATE,'mm/dd/yyyy') " &
                            "then 'Today at ' || ltrim(substr(TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24'),-7,7)) " &
                                "when TO_CHAR(dh.actiondate,'mm/dd/yyyy') = TO_CHAR(SYSDATE-1,'mm/dd/yyyy') " &
                            "then 'Yesterday at ' || ltrim(substr(TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24'),-7,7)) " &
                            "else TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24') " &
                    "End) AS adate " &
                   ",NVL(dh.IPAddress,' ') AS ipaddres "
            Else
                s_sql = "SELECT t.*,(case when t.docid > 0 then isnull('(' +dl.title +')','') else '' end) AS doctitle " &
",(u.FirstName + ' ' + u.LastName) AS uname FROM " &
"( " &
 "SELECT " &
   "row_number() over (order by dh.actiondate desc) AS rn" &
           ",dh.Action " &
           ",dh.docid " &
           ",dh.actiondate " &
            ",dh.UserId "

                s_sql = s_sql & ",isnull(dh.ApproverId,'') AS task" &
                   ",(case when convert(char(10),dh.actiondate,101) = convert(char(10),getdate(),101) " &
                            "then 'Today at ' + ltrim(right(convert(varchar,dh.ActionDate,100),7)) " &
                                "when convert(char(10),dh.actiondate,101) = convert(char(10),dateadd(dd,-1,getdate()),101) " &
                            "then 'Yesterday at ' + ltrim(right(convert(varchar,dh.ActionDate,100),7)) " &
                            "else convert(varchar,dh.ActionDate,100) " &
                    "End) AS adate " &
                   ",isnull(dh.IPAddress,'') AS ipaddres "
            End If

            s_sql = s_sql & "FROM " &
           "DOCHISTORY dh  " &
     "WHERE dh.histid is not null "

            If pUserId <> "" Then
                s_sql = s_sql & " AND dh.UserId = '" & pUserId & "' "
            End If

            If pFormId <> "" Then
                s_sql = s_sql & " AND dh.DocId= " & pFormId & " "
            End If

            If pTask <> "" Then
                s_sql = s_sql & " AND dh.approverid = '" & pTask & "'"
            End If
            If pIpAddress <> "" Then
                s_sql = s_sql & " AND dh.IPAddress = '" & pIpAddress & "'"
            End If
            If pActionDate <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND TO_CHAR(dh.actiondate,'mm/dd/yyyy') >= '" & pActionDate & "' "
                Else
                    s_sql = s_sql & " AND dh.actiondate >= convert(datetime,'" & pActionDate & "') "
                End If

            End If
            If pActionDateTo <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND TO_CHAR(dh.actiondate,'mm/dd/yyyy') <= '" & pActionDateTo & "' "
                Else
                    s_sql = s_sql & " AND dh.actiondate < dateadd(day,1,'" & pActionDateTo & "') "
                End If

            End If

            s_sql = s_sql & ") t " &
            "INNER JOIN Users U " &
                       "on t.userid = u.userid " &
            "LEFT JOIN doclist dl ON " &
               "dl.docid = t.docid and dl.StatusId > 0 "

            If pIdx <> "" Then
                s_sql = s_sql & " WHERE (rn between " & pIdx & " and " & pIdx & "+" & pRowsPerPage & ") "
            End If
            If pSortCol <> "" Then
                s_sql = s_sql & " ORDER BY " & pSortCol & " " & pSortOrder
            Else
                s_sql = s_sql & " ORDER BY t.actiondate desc "
            End If


            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.Fill

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try
    End Function
End Class
