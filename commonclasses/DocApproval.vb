Imports System.Data.SqlClient

Public Class DocApproval
    Public Sub New()

    End Sub
    Dim RowsPerPage As String
    Dim Idx As String
    Dim IpAdd As String
    Dim UserId As String
    Dim CreatedBy As String
    Dim ApproverId As String
    Dim DocId As Integer
    Dim SeqNo As Integer
    Dim StatusId As Integer
    Dim LineNumber As Integer
    Dim DocLinkId As String
    Dim DocType As String
    Dim Comment As String
    Dim GroupId As String
    Dim SortOrder As String
    Dim SortCol As String
    'page1
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
    Public Property pLinkDocId() As Integer
        Get
            Return DocLinkId
        End Get
        Set(ByVal value As Integer)
            DocLinkId = value
        End Set

    End Property

    Public Property pSeqNo() As Integer
        Get
            Return SeqNo
        End Get
        Set(ByVal value As Integer)
            SeqNo = value
        End Set

    End Property
    Public Property pLineNumber() As Integer
        Get
            Return LineNumber
        End Get
        Set(ByVal value As Integer)
            LineNumber = value
        End Set

    End Property

    Public Property pDocId() As Integer
        Get
            Return DocId
        End Get
        Set(ByVal value As Integer)
            DocId = value
        End Set

    End Property

    Public Property pStatusId() As Integer
        Get
            Return StatusId
        End Get
        Set(ByVal value As Integer)
            StatusId = value
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
    Public Property pUserId() As String
        Get
            Return UserId
        End Get
        Set(ByVal value As String)
            UserId = value
        End Set

    End Property
    Public Property pCreatedBy() As String
        Get
            Return CreatedBy
        End Get
        Set(ByVal value As String)
            CreatedBy = value
        End Set

    End Property
    Public Property pApproverId() As String
        Get
            Return ApproverId
        End Get
        Set(ByVal value As String)
            ApproverId = value
        End Set

    End Property


    Public Property pDocType() As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set

    End Property

    Public Property pComment() As String
        Get
            Return Comment
        End Get
        Set(ByVal value As String)
            Comment = value
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

    Public Property pGroupId() As String
        Get
            Return GroupId
        End Get
        Set(ByVal value As String)
            GroupId = value
        End Set

    End Property

    Private Function OrderBy() As String
        If pSortCol = "Reference No" Then
            Return "dl.refno "
        ElseIf pSortCol = "Subject" Then
            Return "dl.Title "
        ElseIf pSortCol = "Date Sent" Then
            Return "da.AssignedDate "
        ElseIf pSortCol = "Approver" Then
            Return "isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'')"
           
        ElseIf pSortCol = "Outgoing Action" Then
            Return "da.OutStatusId "
        ElseIf pSortCol = "Remarks" Then
            Return "da.Remarks "
        Else
            Return "da.AssignedDate "
        End If

    End Function

    
    Public Function CountPending() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = ""
            s_sql = " SELECT count(*) " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0  and (dl.Confidential is null or dl.Confidential = 0) " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "
            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and (da.CarbonCopy is null or da.CarbonCopy = 0) and da.statusid = 2 and dl.statusid <> 5 "

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql




            Return objCommand.ExecScalar



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
    Public Function CountPendingCarbonCopy() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = ""
            s_sql = " SELECT count(*) " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0  and (dl.Confidential is null or dl.Confidential = 0) " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "
            's_sql = s_sql & " LEFT JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "
            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and da.CarbonCopy = 1 and da.statusid = 2 and dl.statusid <> 5 "

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql




            Return objCommand.ExecScalar



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
    Public Function CountPendingConfidential() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = ""
            s_sql = " SELECT count(*) " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0 and dl.Confidential = 1 " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "
            's_sql = s_sql & " LEFT JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "
            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and da.statusid = 2 and dl.statusid <> 5 "

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql




            Return objCommand.ExecScalar



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
    Public Function RetrievePending() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim sortorder As String = OrderBy()
            objCommand = New clsSqlConn

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1

            Dim s_sql As String = ""
            s_sql = "SELECT * FROM ( SELECT "
                s_sql = s_sql & " TOP " & lTop.ToString & " " & _
                        "rn= row_number() over (ORDER BY " & sortorder & " " & pSortOrder & ") " & _
                    ",isnull(dl.refno,'') AS refno " & _
                ",isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'') as sendername " & _
                ",cc = isnull(da.CarbonCopy,0) " & _
                ",urgent = isnull(da.Urgent,0) " & _
                ",duedate = isnull(dl.duedate,'01/01/1900') " & _
                ",isnull(dl.filename,'') as FileName" & _
                ",createddate = isnull(da.assigneddate,'01/01/1900') " & _
                ",OutStatusId = isnull(da.OutStatusId,'0') " & _
                ",u.FirstName + ' ' + u.LastName as username " & _
                ",OutStatus = isnull(ds2.Description,'') "
        
            s_sql = s_sql & ",dl.docId " & _
                ",dl.Title " & _
                ",dl.docType " & _
                ",5 as groupaccessId " & _
                ",da.remarks " & _
                ",da.seqno " & _
                ",dl.statusId " & _
                ",ds.statusId as DocStatusId" & _
                ",ds.Hierarchy " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0  and (dl.Confidential is null or dl.Confidential = 0) " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "LEFT JOIN docStatus ds2 ON " & _
                "ds2.StatusId = da.OutStatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "


            's_sql = s_sql & " INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "


            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and (da.CarbonCopy is null or da.CarbonCopy = 0) and da.statusid = 2 and dl.statusid <> 5 ORDER BY " & sortorder & " " & pSortOrder & ") ps " & _
                 " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql


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
    Public Function CountPendingList() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = ""
            s_sql = " SELECT count(da.docid) " & _
                "FROM " & _
                "(" & _
                        "select rno = ROW_NUMBER() OVER (partition by dr.docid order by dr.seqno),dr.statusid,dr.DocId,dr.ApproverId,dr.sender " & _
                        ",dr.remarks,dr.seqno,dr.OutStatusId,dr.assigneddate,dr.Urgent,dr.CarbonCopy " & _
                        "from docrouting dr " & _
                        "inner join doclist dl on dr.DocId = dl.DocId and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
                ")" & _
                " da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0  and (dl.Confidential is null or dl.Confidential = 0) and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " 





            s_sql = s_sql & "WHERE da.rno = 1 and da.statusid = 2 and dl.statusid <> 5"
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql




            Return objCommand.ExecScalar



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
    Public Function RetrievePendingList() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim sortorder As String = OrderBy()
            objCommand = New clsSqlConn

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1

            Dim s_sql As String = ""
            s_sql = "SELECT * FROM ( SELECT "
            s_sql = s_sql & " TOP " & lTop.ToString & " " & _
                    "rn= row_number() over (ORDER BY " & sortorder & " " & pSortOrder & ") " & _
                ",isnull(dl.refno,'') AS refno " & _
            ",isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'') as approvername " & _
            ",cc = isnull(da.CarbonCopy,0) " & _
            ",urgent = isnull(da.Urgent,0) " & _
            ",ReceivedDate = isnull(convert(varchar,dl.ReceivedDate),'') " & _
            ",ReceivedBy = isnull(dl.ReceivedBy,'') " & _
            ",duedate = isnull(dl.duedate,'01/01/1900') " & _
            ",isnull(dl.filename,'') as FileName" & _
            ",createddate = isnull(da.assigneddate,'01/01/1900') " & _
            ",OutStatusId = isnull(da.OutStatusId,'0') " & _
            ",u.FirstName + ' ' + u.LastName as username " & _
            ",OutStatus = isnull(ds2.Description,'') "

            s_sql = s_sql & ",dl.docId " & _
                ",dl.Title " & _
                ",dl.docType " & _
                ",5 as groupaccessId " & _
                ",da.remarks " & _
                ",da.seqno " & _
                ",dl.statusId " & _
                ",ds.statusId as DocStatusId" & _
                ",ds.Hierarchy " & _
                "FROM " & _
                "(" & _
                        "select rno = ROW_NUMBER() OVER (partition by dr.docid order by dr.seqno),dr.statusid,dr.DocId,dr.ApproverId,dr.sender " & _
                        ",dr.remarks,dr.seqno,dr.OutStatusId,dr.assigneddate,dr.Urgent,dr.CarbonCopy " & _
                        "from docrouting dr " & _
                        "inner join doclist dl on dr.DocId = dl.DocId and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
                ")" & _
                " da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0  and (dl.Confidential is null or dl.Confidential = 0) and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "LEFT JOIN docStatus ds2 ON " & _
                "ds2.StatusId = da.OutStatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.ApproverId = s.userid "


                's_sql = s_sql & " INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "


            s_sql = s_sql & "WHERE da.rno = 1 and da.statusid = 2 and dl.statusid <> 5 ORDER BY " & sortorder & " " & pSortOrder & ") ps " & _
                 " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql


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
    Public Function RetrievePendingCarbonCopy() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim sortorder As String = OrderBy()
            objCommand = New clsSqlConn

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1

            Dim s_sql As String = ""
            s_sql = "SELECT * FROM ( SELECT "
            s_sql = s_sql & " TOP " & lTop.ToString & " " & _
                    "rn= row_number() over (ORDER BY " & sortorder & " " & pSortOrder & ") " & _
                ",isnull(dl.refno,'') AS refno " & _
            ",isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'') as sendername " & _
            ",cc = isnull(da.CarbonCopy,0) " & _
            ",urgent = isnull(da.Urgent,0) " & _
            ",duedate = isnull(dl.duedate,'01/01/1900') " & _
            ",isnull(dl.filename,'') as FileName" & _
            ",createddate = isnull(da.assigneddate,'01/01/1900') " & _
            ",OutStatusId = isnull(da.OutStatusId,'0') " & _
            ",u.FirstName + ' ' + u.LastName as username " & _
            ",OutStatus = isnull(ds2.Description,'') "

            s_sql = s_sql & ",dl.docId " & _
                ",dl.Title " & _
                ",dl.docType " & _
                ",5 as groupaccessId " & _
                ",da.remarks " & _
                ",da.seqno " & _
                ",dl.statusId " & _
                ",ds.statusId as DocStatusId" & _
                ",ds.Hierarchy " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0 and (dl.Confidential is null or dl.Confidential = 0) " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "LEFT JOIN docStatus ds2 ON " & _
                "ds2.StatusId = da.OutStatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "


            's_sql = s_sql & " INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "


            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and da.CarbonCopy = 1 and da.statusid = 2 and dl.statusid <> 5 ORDER BY " & sortorder & " " & pSortOrder & ") ps " & _
                 " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql


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
    Public Function RetrievePendingConfidential() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim sortorder As String = OrderBy()
            objCommand = New clsSqlConn

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1

            Dim s_sql As String = ""
            s_sql = "SELECT * FROM ( SELECT "
            s_sql = s_sql & " TOP " & lTop.ToString & " " & _
                    "rn= row_number() over (ORDER BY " & sortorder & " " & pSortOrder & ") " & _
                ",isnull(dl.refno,'') AS refno " & _
            ",isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'') as sendername " & _
            ",cc = isnull(da.CarbonCopy,0) " & _
            ",urgent = isnull(da.Urgent,0) " & _
            ",duedate = isnull(dl.duedate,'01/01/1900') " & _
            ",isnull(dl.filename,'') as FileName" & _
            ",createddate = isnull(da.assigneddate,'01/01/1900') " & _
            ",OutStatusId = isnull(da.OutStatusId,'0') " & _
            ",u.FirstName + ' ' + u.LastName as username " & _
            ",OutStatus = isnull(ds2.Description,'') "

            s_sql = s_sql & ",dl.docId " & _
                ",dl.Title " & _
                ",dl.docType " & _
                ",5 as groupaccessId " & _
                ",da.remarks " & _
                ",da.seqno " & _
                ",dl.statusId " & _
                ",ds.statusId as DocStatusId" & _
                ",ds.Hierarchy " & _
                "FROM docRouting da  " & _
                "INNER JOIN doclist dl ON " & _
                "da.docId = dl.docId and dl.StatusId > 0 and dl.Confidential = 1 " & _
                "INNER JOIN docStatus ds ON " & _
                "ds.StatusId = dl.StatusId " & _
                "LEFT JOIN docStatus ds2 ON " & _
                "ds2.StatusId = da.OutStatusId " & _
                "INNER JOIN users u ON " & _
                "dl.CreatedBy = u.userid " & _
                "LEFT JOIN users s ON " & _
                "da.sender = s.userid "


            's_sql = s_sql & " INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "


            s_sql = s_sql & "WHERE da.approverId = '" & pApproverId & "' and da.statusid = 2 and dl.statusid <> 5 ORDER BY " & sortorder & " " & pSortOrder & ") ps " & _
                 " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql


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

    Public Sub ReturnToPendingTask()

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            
            Dim s_sql As String = ""


            If DocSession.OraClient Then
                s_sql = "Update  " & _
                "(SELECT dr.StatusId StatusId1,dr.actionDate actionDate1 FROM DocRouting dr INNER JOIN " & _
                "DocList dl " & _
                "ON dr.DocId = dl.DocId AND " & _
                    "dr.SeqNo = dl.RoutingSeqNo " & _
                    "and dl.StatusId NOT IN (" & DocSession.CompleteStatus & ")  " & _
                    "and ((NVL(dl.duedate,SYSDATE+5)-SYSDATE) < 4) WHERE dr.approverId = '" & DocSession.sUserId & "') set StatusId1=2,actionDate1=null"
            Else
                s_sql = "Update DocRouting set StatusId=2,actionDate=null " & _
                "FROM DocRouting dr INNER JOIN " & _
                "DocList dl " & _
                "ON dr.DocId = dl.DocId AND " & _
                    "dr.SeqNo = dl.RoutingSeqNo " & _
                    "and dl.StatusId NOT IN (" & DocSession.CompleteStatus & ")  " & _
                    "and DATEDIFF(dd,getdate(),isnull(dl.duedate,dateadd(dd,5,getdate()))) < 4 WHERE dr.approverId = '" & DocSession.sUserId & "' and convert(char(10),dr.actiondate,101)<convert(char(10),getdate(),101)"
            End If

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecNonQuery()


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
    End Sub

    Public Function RetrieveOverDue() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_APPROVERLISTGET5"
            'objCommand.ParametersAddWithValue("@approverId", pApproverId)
            'objCommand.ParametersAddWithValue("@GroupId", pGroupId)
            Dim s_sql As String = ""
            

           
                s_sql = "SELECT TOP 6 " & _
                    "dl.docId " & _
                    ",dl.Title " & _
                    ",u.FirstName + ' ' + u.LastName as username " & _
                    ",dl.FileName  " & _
                    ",dl.docType " & _
                    ",dl.filename " & _
                    ",dl.createddate " & _
                    ",da.duedate " & _
                    ",5 as groupaccessId " & _
                    ",da.remarks " & _
                    "FROM docRouting da  " & _
                    "INNER JOIN doclist dl ON " & _
                    "da.docId = dl.docId and dl.StatusId > 0 " & _
                    "INNER JOIN users u ON " & _
                    "dl.CreatedBy = u.userid " & _
                    "WHERE da.approverId = '" & pApproverId & "' and da.statusid = 2"
                s_sql = s_sql & " and (da.duedate <> '01/01/1900' AND convert(datetime,da.duedate) < convert(char(10),getdate(),101)) "

                '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _



            'Dim osp As New cls_storedproc
            'osp.pGroupId = pGroupId
            'osp.pUserId = pApproverId
            'osp.pTop = 6
            'osp.pStatus = 2
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql 'osp.xDMSF_OVERDUETASK

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

    Public Function RetrieveDocApproval() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim s_sql As String = "" 
            
                s_sql = s_sql & "SELECT TOP 11 " & _
                    "dl.docId, " & _
                    "dl.docType, " & _
                    "dl.Title, " & _
                    "profilepic=isnull(u.profilepic,'default.png'), " & _
                               "ApproverName = isnull(u.FirstName + ' ' + u.LastName,''), " & _
                               "sactiondate = replace(substring(convert(varchar,da.actiondate,107),1,6),' 0',' '), " & _
                               "ActionDate = convert(varchar,da.ActionDate,100), "

            s_sql = s_sql & "da.statusid " &
   ",doctypeimg='images/doctype/' + case right(dl.FileName,4)  " &
"when '.doc' then 'doc.png'  " &
"when 'docx' then 'doc.png'  " &
"when '.xls' then 'excel.png'  " &
"when 'xlsx' then 'excel.png'  " &
"when '.ppt' then 'doc.png'  " &
"when 'pptx' then 'doc.png'  " &
"when '.bmp' then 'bmp.png'  " &
"when '.tif' then 'tiff.png'  " &
"when 'tiff' then 'tiff.png'  " &
"when 'jpeg' then 'jpg.png'  " &
"when '.jpg' then 'jpg.png'  " &
"when '.gif' then 'gif.png'  " &
"when '.png' then 'png.png'  " &
"when '.pdf' then 'pdf.png'  " &
"when '.zip' then 'zip.png'  " &
"when '.rar' then 'zip.png'  " &
"else 'dms.png' end, " &
"ds.Description, " &
"5 as groupaccessId " &
"FROM docRouting da  " &
"INNER JOIN doclist dl ON " &
"da.docId = dl.docId and dl.StatusId > 0 " &
"LEFT JOIN users u ON " &
"da.ApproverId = u.userid " &
"INNER JOIN DocStatus ds ON " &
"ds.StatusId = da.StatusId " &
"WHERE dl.createdby = '" & pCreatedBy & "' and da.statusid <> 2 and dl.statusid <> 5 " &
"ORDER BY da.ActionDate desc "

            '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _


            objCommand = New clsSqlConn

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

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

    Public Function RetrieveConfidentialApproval() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim s_sql As String = ""

            s_sql = s_sql & "SELECT TOP 11 " & _
                "dl.docId, " & _
                "dl.docType, " & _
                "dl.Title, " & _
                "profilepic=isnull(u.profilepic,'default.png'), " & _
                           "ApproverName = isnull(u.FirstName + ' ' + u.LastName,''), " & _
                           "sactiondate = replace(substring(convert(varchar,da.actiondate,107),1,6),' 0',' '), " & _
                           "ActionDate = convert(varchar,da.ActionDate,100), "

            s_sql = s_sql & "da.statusid " &
   ",doctypeimg='images/doctype/' + case right(dl.FileName,4)  " &
"when '.doc' then 'doc.png'  " &
"when 'docx' then 'doc.png'  " &
"when '.xls' then 'excel.png'  " &
"when 'xlsx' then 'excel.png'  " &
"when '.ppt' then 'doc.png'  " &
"when 'pptx' then 'doc.png'  " &
"when '.bmp' then 'bmp.png'  " &
"when '.tif' then 'tiff.png'  " &
"when 'tiff' then 'tiff.png'  " &
"when 'jpeg' then 'jpg.png'  " &
"when '.jpg' then 'jpg.png'  " &
"when '.gif' then 'gif.png'  " &
"when '.png' then 'png.png'  " &
"when '.pdf' then 'pdf.png'  " &
"when '.zip' then 'zip.png'  " &
"when '.rar' then 'zip.png'  " &
"else 'dms.png' end, " &
"ds.Description, " &
"5 as groupaccessId " &
"FROM docRouting da  " &
"INNER JOIN doclist dl ON " &
"da.docId = dl.docId and dl.StatusId > 0 " &
"LEFT JOIN users u ON " &
"da.ApproverId = u.userid " &
"INNER JOIN DocStatus ds ON " &
"ds.StatusId = da.StatusId " &
"WHERE dl.createdby = '" & pCreatedBy & "' and da.statusid <> 2 and dl.statusid <> 5 " &
"ORDER BY da.ActionDate desc "

            '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _


            objCommand = New clsSqlConn

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

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

    'Public Function RetrieveApprovalAll() As DataTable

    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.StoredProcedure

    '        objCommand.CommandText = "XMSP_APPROVERLISTGET"
    '        objCommand.ParametersAddWithValue("@approverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@GroupId", pGroupId)

    '        ldata = objCommand.Fill


    '        Return ldata



    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If


    '    End Try

    'End Function

    'Public Sub ApproveDoc(ByVal objCommand As clsSqlConn)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCAPPROVALUPDATE"

    '        objCommand.ParametersClear()
    '        objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        objCommand.ParametersAddWithValue("@lineNumber", pLineNumber)
    '        objCommand.ParametersAddWithValue("@approverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@seqNo", pSeqNo)
    '        objCommand.ParametersAddWithValue("@statusId", pStatusId)
    '        objCommand.ParametersAddWithValue("@IpAddress", pIpAddress)
    '        objCommand.ParametersAddWithValue("@Comment", pComment)


    '        objCommand.ExecTranNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

    Public Function ApproverStatus() As DataTable
        'Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            ''objCommand.CommandText = "xMSP_DOCAPPROVERSTATUS"
            'objCommand.CommandText = "xMSP_DOCROUTINGGET"
            'objCommand.ParametersAddWithValue("@docId", pDocId)
            'objCommand.ParametersAddWithValue("@approverId", pUserId)

            'ldata = objCommand.Fill
            Dim oRoute As New DocRouting
            oRoute.pDocId = pDocId
            oRoute.pApproverId = pUserId
            ldata = oRoute.RetrieveRouting
            Return ldata



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If
        End Try
    End Function

    Public Function ApproverStatusQuick() As DataTable
        'Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim oRoute As DocRouting
        Try
            
            oRoute = New DocRouting
            oRoute.pDocId = pDocId
            oRoute.pApproverId = pUserId
            ldata = oRoute.RetrieveRoutingQuick
            Return ldata



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try
    End Function


    'Public Sub DeleteApprovers(ByVal objCommand As clsSqlConn)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCTYPEAPPROVERSDELETE"


    '        objCommand.ParametersClear()

    '        objCommand.ParametersAddWithValue("@DocType", pDocType)
    '        objCommand.ParametersAddWithValue("@approverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@seqNo", pSeqNo)
    '        objCommand.ExecTranNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

    'Public Function RetrieveDocTypeApprovers() As DataTable

    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.StoredProcedure

    '        objCommand.CommandText = "xMSP_DOCTYPEAPPROVERSGET"
    '        objCommand.ParametersAddWithValue("@DocType", pDocType)

    '        ldata = objCommand.Fill


    '        Return ldata



    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If


    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '    End Try

    'End Function

    'Public Sub SaveDoctypeApprovers(ByVal objCommand As clsSqlConn)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCTYPEAPPROVERSADD"

    '        objCommand.ParametersClear()
    '        objCommand.ParametersAddWithValue("@DocType", pDocType)
    '        objCommand.ParametersAddWithValue("@ApproverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@SeqNo", pSeqNo)
    '        objCommand.ParametersAddWithValue("@IPAddress", pIpAddress)
    '        objCommand.ParametersAddWithValue("@UserID", pUserId)

    '        objCommand.ExecTranNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

End Class
