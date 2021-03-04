Public Class DocHistory
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
            Return EndSeqNo
        End Get
        Set(ByVal value As String)
            EndSeqNo = value
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

    Public Property pDocId() As String
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
            Dim osp As New cls_storedproc
            If DocSession.OraClient Then

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecNonQuery()
            Else
                'objCommand.pCommandType = CommandType.StoredProcedure
                'objCommand.pCommandText = "xMSP_DOCHISTORYADD"
                'objCommand.ParametersAddWithValue("@DocId", pDocId)
                'objCommand.ParametersAddWithValue("@Action", pAction)
                'objCommand.ParametersAddWithValue("@Task", pTask)
                'objCommand.ParametersAddWithValue("@UserId", pUserId)
                'objCommand.ParametersAddWithValue("@IPAddress", pIpAddress)
                'objCommand.ExecNonQuery()

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecNonQuery()
            End If
            'ltr.pTran.Commit()

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
            Dim osp As New cls_storedproc
            If DocSession.OraClient Then

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecTranNonQuery()
            Else
                'objCommand.ClearParameter()

                'objCommand.pCommandType = CommandType.StoredProcedure
                'objCommand.pCommandText = "xMSP_DOCHISTORYADD"
                'objCommand.ParametersAddWithValue("@DocId", pDocId)
                'objCommand.ParametersAddWithValue("@Action", pAction)
                'objCommand.ParametersAddWithValue("@Task", pTask)
                'objCommand.ParametersAddWithValue("@UserId", pUserId)
                'objCommand.ParametersAddWithValue("@IPAddress", pIpAddress)
                'objCommand.ExecTranNonQuery()

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecTranNonQuery()
            End If
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

    Public Sub LogChanges()


        Dim s_sql As String = "INSERT INTO DataChanges " &
           "(tableName " &
           ",RecordId " &
           ",ModifiedBy " &
           ",ColumnName " &
           ",ModifiedDate " &
           ",OldValue " &
           ",NewValue " &
           ",JRFNo " &
           ",IPAddress) " &
     "VALUES " &
           "('" & pTableName & "' " &
           ",'" & pRecordId & "' " &
           ",'" & pModifiedBy & "' " &
           ",'" & pColumnName & "' " &
           "," & IIf(DocSession.OraClient, "TO_DATE('" & pModifiedDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & pModifiedDate & "'") &
           ",'" & pOldValue & "' " &
           ",'" & pNewValue & "' " &
           ",'" & pJRFNo & "' " &
           ",'" & pIpAddress & "') "
        Dim objCommand As clsSqlConn

        Try

            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

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


    Public Sub LogChanges(ByVal objCommand As clsSqlConn)


        Dim s_sql As String = "INSERT INTO DataChanges " &
           "(tableName " &
           ",RecordId " &
           ",ModifiedBy " &
           ",ColumnName " &
           ",ModifiedDate " &
           ",OldValue " &
           ",NewValue " &
           ",JRFNo " &
           ",IPAddress) " &
     "VALUES " &
        "('" & pTableName & "' " &
           ",'" & pRecordId & "' " &
           ",'" & pModifiedBy & "' " &
           ",'" & Replace(pColumnName, "'", "''") & "' " &
           "," & IIf(DocSession.OraClient, "TO_DATE('" & pModifiedDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & pModifiedDate & "'") &
           ",'" & Replace(pOldValue, "'", "''") & "' " &
           ",'" & Replace(pNewValue, "'", "''") & "' " &
           ",'" & Replace(pJRFNo, "'", "''") & "' " &
           ",'" & Replace(pIpAddress, "'", "''") & "') "
        Try



            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecTranNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try

    End Sub
    Public Sub AddAdminChanges()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()
            Dim osp As New cls_storedproc
            If DocSession.OraClient Then

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecNonQuery()
            Else
                'objCommand.pCommandType = CommandType.StoredProcedure
                'objCommand.pCommandText = "xMSP_DOCHISTORYADD"
                'objCommand.ParametersAddWithValue("@DocId", pDocId)
                'objCommand.ParametersAddWithValue("@Action", pAction)
                'objCommand.ParametersAddWithValue("@Task", pTask)
                'objCommand.ParametersAddWithValue("@UserId", pUserId)
                'objCommand.ParametersAddWithValue("@IPAddress", pIpAddress)
                'objCommand.ExecNonQuery()

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecNonQuery()
            End If
            'ltr.pTran.Commit()

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

    Public Sub AddAdminChanges(ByVal objCommand As clsSqlConn)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            'objCommand = New clsSqlConn()
            Dim osp As New cls_storedproc
            If DocSession.OraClient Then

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecTranNonQuery()
            Else
                'objCommand.ClearParameter()

                'objCommand.pCommandType = CommandType.StoredProcedure
                'objCommand.pCommandText = "xMSP_DOCHISTORYADD"
                'objCommand.ParametersAddWithValue("@DocId", pDocId)
                'objCommand.ParametersAddWithValue("@Action", pAction)
                'objCommand.ParametersAddWithValue("@Task", pTask)
                'objCommand.ParametersAddWithValue("@UserId", pUserId)
                'objCommand.ParametersAddWithValue("@IPAddress", pIpAddress)
                'objCommand.ExecTranNonQuery()

                osp.pDocId = pDocId
                osp.pTask = pTask
                osp.pIPAddress = pIpAddress
                osp.pUserId = pUserId
                osp.pAction = pAction

                objCommand.pCommandType = CommandType.Text
                objCommand.pCommandText = osp.DMSF_ADDHISTORY

                objCommand.ExecTranNonQuery()
            End If
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
            Dim s_sql As String = "SELECT COUNT(dh.docid) FROM dochistory dh inner join Doclist dl on dh.docid = dl.docid  WHERE dl.statusid <> 5 "
            If pUserId <> "" Then
                s_sql = s_sql & " AND dh.UserId = '" & pUserId & "' "
            End If
            If pDocId <> "" Then
                s_sql = s_sql & " AND dh.DocId= " & pDocId & " "
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

    Public Function GetGroupApprovers() As DataTable

        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try
            If DocSession.OraClient Then
                

                If pStartSeqno = "0" Then

                    s_sql = "select * from (select u.userid,dr.seqno,u.firstname||' '||u.lastname as username,u.usergroup,dr.statusid,s.description,duedate,actiondate,completeddate,assigneddate,NVL(u.profilePic,'default.png') as profilePic,NVL(dr.CarbonCopy,0) as CC,' ' as actn from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "left join docstatus s on s.statusid = dr.statusid " & _
                    "where dr.docId = " & pDocId & _
                    " and u.usergroup = '" & pUserGroup & "'" & _
                    " and dr.seqno >= " & pStartSeqno & " " & _
                    " and dr.seqno < " & pEndSeqNo & " " & _
                    " union " & _
                    " select dl.createdby, 0," & _
                    " u.firstname||' '||u.lastname as username,u.usergroup,dl.statusid,'N/A' as description,null as duedate," & _
                    " null as actiondate,null as completeddate,null as assigneddate,nvl(u.profilePic,'default.png') as profilePic,null as CC,' ' as actn from doclist dl " & _
                    "inner join users u on dl.createdby = u.userid " & _
                    "left join docstatus s on s.statusid = dl.statusid " & _
                    " where dl.docId = " & pDocId & ") tbl " & _
                    " order by seqno "
                Else
                    s_sql = "select u.userid,dr.seqno,u.firstname+' '+u.lastname as username,u.usergroup,dr.statusid,s.description,duedate,actiondate,completeddate,assigneddate,NVL(u.profilePic,'default.png') as profilePic,NVL(dr.CarbonCopy,0) as CC,'' as actn from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "left join docstatus s on s.statusid = dr.statusid " & _
                    "where dr.docId = " & pDocId & _
                    " and u.usergroup = '" & pUserGroup & "'" & _
                    " and NVL(dr.CarbonCopy,0) = 0 " & _
                    " and dr.seqno >= " & pStartSeqno & " " & _
                    " and dr.seqno < " & pEndSeqNo & " " & _
                    " order by dr.seqno "
                End If
            Else
                
                If pStartSeqno = "0" Then

                    s_sql = "select * from (select u.userid,dr.seqno,u.firstname+' '+u.lastname as username,u.usergroup,dr.statusid,s.description,duedate,actiondate,assigneddate,completeddate,Isnull(u.profilePic,'default.png') as profilePic,isnull(dr.CarbonCopy,0) as CC,'' as actn from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "left join docstatus s on s.statusid = dr.statusid " & _
                    "where dr.docId = " & pDocId & _
                    " and u.usergroup = '" & pUserGroup & "'" & _
                    " and dr.seqno >= '" & pStartSeqno & "'" & _
                    " and dr.seqno <= '" & pEndSeqNo & "'" & _
                    " union " & _
                    " select dl.createdby, 0," & _
                    " u.firstname+' '+u.lastname as username,u.usergroup,dl.statusid,'N/A' as description,null as duedate," & _
                    " null as actiondate,null as completeddate,null as assigneddate,Isnull(u.profilePic,'default.png') as profilePic,null as CC,'' as actn from doclist dl " & _
                    "inner join users u on dl.createdby = u.userid " & _
                    "left join docstatus s on s.statusid = dl.statusid " & _
                    " where dl.docId = " & pDocId & ") tbl " & _
                    " order by seqno "
                Else
                    s_sql = "select u.userid,dr.seqno,u.firstname+' '+u.lastname as username,u.usergroup,dr.statusid,s.description,duedate,actiondate,completeddate,assigneddate,Isnull(u.profilePic,'default.png') as profilePic,isnull(dr.CarbonCopy,0) as CC,'' as actn from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "left join docstatus s on s.statusid = dr.statusid " & _
                    "where dr.docId = " & pDocId & _
                    " and u.usergroup = '" & pUserGroup & "'" & _
                    " and ISNULL(dr.CarbonCopy,0) = 0 " & _
                    " and dr.seqno >= '" & pStartSeqno & "'" & _
                    " and dr.seqno <= '" & pEndSeqNo & "'" & _
                    " order by dr.seqno "
                End If
                    
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
    Public Function GetTrackingStatus() As DataTable

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        Try
            If DocSession.OraClient Then
                s_sql = "SELECT * FROM (select dr.seqno,u.usergroup,g.groupname,g.trackingcolor,g.textcolor from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "inner join groups g on g.groupid = u.usergroup " & _
                    "where nvl(dr.carboncopy,0) = 0 and dr.docId = " & pDocId & _
                    " UNION " & _
                    "select seqno = 0,u.usergroup,g.groupname,g.trackingcolor,g.textcolor from doclist dl " & _
                    "inner join users u on dl.createdby = u.userid " & _
                    "inner join groups g on g.groupid = u.usergroup " & _
                    "where dl.docId = " & pDocId & ") A " & _
                    " ORDER by seqno "
            Else
                s_sql = "SELECT * FROM (select dr.seqno,u.usergroup,g.groupname,g.trackingcolor,g.textcolor from docrouting dr " & _
                    "inner join users u on dr.approverid = u.userid " & _
                    "inner join groups g on g.groupid = u.usergroup " & _
                    "where isnull(dr.carboncopy,0) = 0 and dr.docId = " & pDocId & _
                    " UNION " & _
                    "select seqno = 0,u.usergroup,g.groupname,g.trackingcolor,g.textcolor from doclist dl " & _
                    "inner join users u on dl.createdby = u.userid " & _
                    "inner join groups g on g.groupid = u.usergroup " & _
                    "where dl.docId = " & pDocId & ") A " & _
                    " ORDER by seqno "
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

    Public Function GetHistory() As DataTable

        Dim objCommand As clsSqlConn

        Try
            Dim s_where As String = ""
            
            Dim s_sql As String

            If DocSession.OraClient Then
                s_sql = "SELECT t.*,(case when t.docid > 0 then NVL('('||dl.title ||')','') else ' ' end) AS doctitle " & _
",(u.FirstName||' '||u.LastName) AS uname FROM " & _
"( " & _
 "SELECT " & _
   "row_number() over (order by dh.actiondate desc) AS rn" & _
           ",dh.Action " & _
           ",dh.docid " & _
           ",dh.actiondate " & _
            ",dh.UserId "

                s_sql = s_sql & ",NVL(dh.ApproverId,' ') AS task" & _
                   ",(case when TO_CHAR(dh.actiondate,'mm/dd/yyyy') = TO_CHAR(SYSDATE,'mm/dd/yyyy') " & _
                            "then 'Today at ' || ltrim(substr(TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24'),-7,7)) " & _
                                "when TO_CHAR(dh.actiondate,'mm/dd/yyyy') = TO_CHAR(SYSDATE-1,'mm/dd/yyyy') " & _
                            "then 'Yesterday at ' || ltrim(substr(TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24'),-7,7)) " & _
                            "else TO_CHAR(dh.ActionDate,'mon dd, yyyy HH24') " & _
                    "End) AS adate " & _
                   ",NVL(dh.IPAddress,' ') AS ipaddres "
            Else
                s_sql = "SELECT t.*,(case when t.docid > 0 then isnull('(' +dl.title +')','') else '' end) AS doctitle " & _
",(u.FirstName + ' ' + u.LastName) AS uname FROM " & _
"( " & _
 "SELECT " & _
   "row_number() over (order by dh.actiondate desc) AS rn" & _
           ",dh.Action " & _
           ",dh.docid " & _
           ",dh.actiondate " & _
            ",dh.UserId "

                s_sql = s_sql & ",isnull(dh.ApproverId,'') AS task" & _
                   ",(case when convert(char(10),dh.actiondate,101) = convert(char(10),getdate(),101) " & _
                            "then 'Today at ' + ltrim(right(convert(varchar,dh.ActionDate,100),7)) " & _
                                "when convert(char(10),dh.actiondate,101) = convert(char(10),dateadd(dd,-1,getdate()),101) " & _
                            "then 'Yesterday at ' + ltrim(right(convert(varchar,dh.ActionDate,100),7)) " & _
                            "else convert(varchar,dh.ActionDate,100) " & _
                    "End) AS adate " & _
                   ",isnull(dh.IPAddress,'') AS ipaddres "
            End If

            s_sql = s_sql & "FROM " & _
           "DOCHISTORY dh  " & _
     "WHERE dh.histid is not null "

            If pUserId <> "" Then
                s_sql = s_sql & " AND dh.UserId = '" & pUserId & "' "
            End If

            If pDocId <> "" Then
                s_sql = s_sql & " AND dh.DocId= " & pDocId & " "
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

            s_sql = s_sql & ") t " & _
            "INNER JOIN Users U " & _
                       "on t.userid = u.userid " & _
            "LEFT JOIN doclist dl ON " & _
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
