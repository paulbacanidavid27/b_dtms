Public Class cls_storedproc
    Dim v_userid As String
    Dim v_locked As String
    Dim v_groupid As String
    Dim v_docid As String
    Dim v_linkdocid As String
    Dim v_IPAddress As String
    Dim v_deletedBy As String
    Dim v_GroupName As String
    Dim v_SortCol As String
    Dim v_SortOrder As String
    Dim v_action As String
    Dim v_CDate As String
    Dim v_Idx As Integer
    Dim v_RowsPerPage As Integer
    Dim v_tags As String
    Dim v_note As String
    Dim v_task As String
    Dim v_top As String
    Dim v_status As String

    Dim v_tableName As String
    Dim v_RecordId As String
    Dim v_ModifiedBy As String
    Dim v_ColumnName As String
    Dim v_ModifiedDate As String
    Dim v_OldValue As String
    Dim v_NewValue As String


    Public Property ptableName As String
        Get
            Return v_tableName
        End Get
        Set(ByVal value As String)
            v_tableName = value
        End Set
    End Property

    Public Property pRecordId As String
        Get
            Return v_RecordId
        End Get
        Set(ByVal value As String)
            v_RecordId = value
        End Set
    End Property

    Public Property pModifiedBy As String
        Get
            Return v_ModifiedBy
        End Get
        Set(ByVal value As String)
            v_ModifiedBy = value
        End Set
    End Property
    Public Property pModifiedDate As String
        Get
            Return v_ModifiedDate
        End Get
        Set(ByVal value As String)
            v_ModifiedDate = value
        End Set
    End Property

    Public Property pColumnName As String
        Get
            Return v_ColumnName
        End Get
        Set(ByVal value As String)
            v_ColumnName = value
        End Set
    End Property

    Public Property pOldValue As String
        Get
            Return v_OldValue
        End Get
        Set(ByVal value As String)
            v_OldValue = value
        End Set
    End Property

    Public Property pNewValue As String
        Get
            Return v_NewValue
        End Get
        Set(ByVal value As String)
            v_NewValue = value
        End Set
    End Property

    Public Property pTop As String
        Get
            Return v_top
        End Get
        Set(ByVal value As String)
            v_top = value
        End Set
    End Property



    Public Property pStatus As String
        Get
            Return v_status
        End Get
        Set(ByVal value As String)
            v_status = value
        End Set
    End Property
    Public Property pTask As String
        Get
            Return v_task
        End Get
        Set(ByVal value As String)
            v_task = value
        End Set
    End Property
    Public Property pTags As String
        Get
            Return v_tags
        End Get
        Set(ByVal value As String)
            v_tags = value
        End Set
    End Property

    Public Property pNote As String
        Get
            Return v_note
        End Get
        Set(ByVal value As String)
            v_note = value
        End Set
    End Property
    Public Property pAction As String
        Get
            Return v_action
        End Get
        Set(ByVal value As String)
            v_action = value
        End Set
    End Property

    Public Property pDate As String
        Get
            Return v_CDate
        End Get
        Set(ByVal value As String)
            v_CDate = value
        End Set
    End Property

    Public Property pRowsPerPage As Integer
        Get
            Return v_RowsPerPage
        End Get
        Set(ByVal value As Integer)
            v_RowsPerPage = value
        End Set
    End Property
    Public Property pIdx As Integer
        Get
            Return v_Idx
        End Get
        Set(ByVal value As Integer)
            v_Idx = value
        End Set
    End Property

    Public Property pDeletedBy As String
        Get
            Return v_deletedBy
        End Get
        Set(ByVal value As String)
            v_deletedBy = value
        End Set
    End Property

    Public Property pGroupName As String
        Get
            Return v_GroupName
        End Get
        Set(ByVal value As String)
            v_GroupName = value
        End Set
    End Property
    Public Property pSortCol As String
        Get
            Return v_SortCol
        End Get
        Set(ByVal value As String)
            v_SortCol = value
        End Set
    End Property

    Public Property pSortOrder As String
        Get
            Return v_SortOrder
        End Get
        Set(ByVal value As String)
            v_SortOrder = value
        End Set
    End Property



    Public Property pIPAddress As String
        Get
            Return v_IPAddress
        End Get
        Set(ByVal value As String)
            v_IPAddress = value
        End Set
    End Property

    Public Property pDocId As String
        Get
            Return v_docid
        End Get
        Set(ByVal value As String)
            v_docid = value
        End Set
    End Property

    Public Property pLinkDocId As String
        Get
            Return v_docid
        End Get
        Set(ByVal value As String)
            v_docid = value
        End Set
    End Property

    Public Property pUserId As String
        Get
            Return v_userid
        End Get
        Set(ByVal value As String)
            v_userid = value
        End Set
    End Property

    Public Property pGroupId As String
        Get
            Return v_groupid
        End Get
        Set(ByVal value As String)
            v_groupid = value
        End Set
    End Property

    Public Property pLocked As String
        Get
            Return v_locked
        End Get
        Set(ByVal value As String)
            v_locked = value
        End Set
    End Property

    Public ReadOnly Property pGetDate As String
        Get
            Return Date.Now.ToShortDateString
        End Get
    End Property

    Public Sub New()

    End Sub

    
    Public Function DMSF_GROUPACCESSGET2_sql() As String
        Dim s_sql As String = ""
        s_sql = "Select gda.GroupId,gda.DocId,dt.docname,gda.GroupAccessId "
        If DocSession.OraClient Then
            s_sql = s_sql & ", (case when NVL(gda.CanPrint,0) = 1 then 'Yes' else 'No' end) AS pPrint" & _
        ", (case when NVL(gda.VersionControl,0) = 1 then 'Yes' else 'No' end) AS pVersion " & _
        ", (case when NVL(gda.CanDownload,0) = 1 then 'Yes' else 'No' end) AS pDownload " & _
        ", (case when NVL(gda.CanPrintReceipt,0) = 1 then 'Yes' else 'No' end) AS pReceipt "
            
        Else
            s_sql = s_sql & ", (case when isnull(gda.CanPrint,0) = 1 then 'Yes' else 'No' end) AS pPrint" & _
        ", (case when isnull(gda.VersionControl,0) = 1 then 'Yes' else 'No' end) AS pVersion " & _
        ", (case when isnull(gda.CanDownload,0) = 1 then 'Yes' else 'No' end) AS pDownload " & _
        ", (case when isnull(gda.CanPrintReceipt,0) = 1 then 'Yes' else 'No' end) AS pReceipt "
        End If

        s_sql = s_sql & ",ga.GroupAccess " & _
        "FROM dbo.GroupDocAccess gda " & _
        "INNER JOIN dbo.Doctype dt " & _
            "ON dt.doctype = gda.docId " & _
        "INNER JOIN dbo.GroupAccess ga " & _
            "ON ga.GroupAccessId = gda.GroupAccessId " & _
        "WHERE GroupId = '" & pGroupId & "' "
        Return s_sql
    End Function
    'Public Function DMSF_GROUPACCESSGET() As String
    '    Dim lsSQL As String = "SELECT gdoctype = NVL(g.docID,''), " & _
    '                "doctype = NVL(d.doctype,''), " & _
    '                "docname = NVL(d.docname,''), " & _
    '                "GroupAccessId = NVL(g.GroupAccessId,0), " & _
    '                "CanPrint = case when NVL(g.CanPrint,0) = 1 then 'Yes' else 'No' end, " & _
    '                "CanControl = case when NVL(g.VersionControl,0) = 1 then 'Yes' else 'No' end, " & _
    '                "CanDownload = case when NVL(g.CanDownload,0) = 1 then 'Yes' else 'No' end " & _
    '                "FROM doctype d " & _
    '                "Left Join  " & _
    '                "(" & _
    '                    "select docid ,groupaccessid,CanPrint,CanDownload,VersionControl from groupdocaccess "
    '    If pGroupId <> "" Then
    '        lsSQL = lsSQL & " WHERE groupid = '" & pGroupId & "' "
    '    End If

    '    lsSQL = lsSQL & ") g " & _
    '           "ON g.docid = d.doctype "

    '    Return lsSQL
    'End Function

    Public Function DMSF_GROUPACCESSGET() As String
        Return "SELECT GroupAccessId " & _
                ",GroupAccess " & _
                ",GroupAccessDesc " & _
                "FROM GroupAccess "
    End Function

    Public Function DMSF_GROUPCODEEXISTS() As String
        If DocSession.OraClient Then
            Return "SELECT usergroup FROM USERS WHERE usergroup = '" & pGroupId & "' and rownum = 1"
        Else
            Return "SELECT TOP 1 usergroup FROM USERS WHERE usergroup = '" & pGroupId & "' and deldate is null "
        End If

    End Function

    'Public Function DMSF_GROUPGETACCESS() As String
    '    Dim lsSQL As String = "SELECT  NVL(g.docID,'') gdoctype, " & _
    '                "NVL(d.doctype,'') doctype, " & _
    '                "NVL(d.docname,'') docname, " & _
    '                "NVL(g.GroupAccessId,0) GroupAccessId, " & _
    '                "(case when NVL(g.CanPrint,0) = 1 then 'Yes' else 'No' end) CanPrint, " & _
    '                "(case when NVL(g.VersionControl,0) = 1 then 'Yes' else 'No' end) CanControl, " & _
    '                "(case when NVL(g.CanDownload,0) = 1 then 'Yes' else 'No' end) CanDownload " & _
    '                "FROM doctype d " & _
    '                "Left Join  " & _
    '                "(" & _
    '                    "select docid ,groupaccessid,CanPrint,CanDownload,VersionControl from groupdocaccess "
    '    If pGroupId <> "" Then
    '        lsSQL = lsSQL & " WHERE groupid = '" & pGroupId & "' "
    '    End If

    '    lsSQL = lsSQL & ") g " & _
    '           "ON g.docid = d.doctype "

    '    Return lsSQL
    'End Function

    Public Function DMSF_GROUPGETACCESS_sql() As String
        Dim s_sql As String
        s_sql = "SELECT "
        If DocSession.OraClient Then
            s_sql = s_sql & "NVL(g.docID,'') AS gdoctype, " & _
         "NVL(d.doctype,'') AS doctype, " & _
         "NVL(d.docname,'') AS docname, " & _
         "NVL(g.GroupAccessId,0) AS GroupAccessId, " & _
         "NVL(case when g.CanPrint = 1 then 'Yes' else 'No' end,'No') AS CanPrint, " & _
         "NVL(case when g.VersionControl = 1 then 'Yes' else 'No' end,'No') AS CanControl, " & _
         "NVL(case when g.CanDownload = 1 then 'Yes' else 'No' end,'No') AS CanDownload, " & _
         "NVL(case when g.CanPrintReceipt = 1 then 'Yes' else 'No' end,'No') AS CanPrintReceipt "
        Else
            s_sql = s_sql & "isnull(g.docID,'') gdoctype, " & _
         "isnull(d.doctype,'') doctype, " & _
         "isnull(d.docname,'') docname, " & _
         "isnull(g.GroupAccessId,0) GroupAccessId, " & _
         "isnull(case when g.CanPrint = 1 then 'Yes' else 'No' end,'No') CanPrint, " & _
         "isnull(case when g.VersionControl = 1 then 'Yes' else 'No' end,'No') CanControl, " & _
         "isnull(case when g.CanDownload = 1 then 'Yes' else 'No' end,'No') CanDownload, " & _
         "isnull(case when g.CanPrintReceipt = 1 then 'Yes' else 'No' end,'No') CanPrintReceipt "
        End If
         
        s_sql = s_sql & "FROM " & _
         "doctype d " & _
         "LEFT JOIN " & _
         "(select docid ,groupaccessid,CanPrint,CanDownload,VersionControl,CanPrintReceipt from groupdocaccess "

        If pGroupId <> "" Then
            s_sql = s_sql & "where (groupid = '" & pGroupId & "') "
        End If

        s_sql = s_sql & ") g " & _
        "ON " & _
        "g.docid = d.doctype " & _
      "WHERE d.inactive is null or d.inactive = 0 "

        Return s_sql
    End Function

    'Public Function DMSF_GROUPGET()
    '    Dim sSQl As String = "SELECT " & _
    '    "g.groupid, " & _
    '    "g.groupname, " & _
    '    "NVL(case when g.ReportAccess = 1 then 'Yes' else 'No' end,'No') CanReport, " & _
    '    "NVL(g.AlwaysAllowed,0) AlwaysAllowed, " & _
    '    "NVL(gd.doctype,'') doctype, " & _
    '    "NVL(gd.docname,'') docname, " & _
    '    " '' gdoctype, " & _
    '    "gd.groupaccessid, " & _
    '    "gd.GroupAccess,gd.GroupAccessDesc,NVL(case when gd.CanPrint = 1 then 'Yes' else 'No' end,'No') CanPrint, " & _
    '    "NVL(case when gd.VersionControl = 1 then 'Yes' else 'No' end,'No') CanControl" & _
    '    ",NVL(case when gd.CanDownload = 1 then ''Yes'' else ''No'' end,''No'') " & _
    '    "FROM " & _
    '    "groups g " & _
    '    "LEFT JOIN " & _
    '    "(select gda.groupid, d.doctype, d.docname,NVL(gda.groupaccessid,0) groupaccessid,gda.VersionControl,gda.canPrint,gda.CanDownload, " & _
    '    "NVL(ga.GroupAccess,'') GroupAccess,NVL(ga.GroupAccessDesc,'') GroupAccessDesc " & _
    '    "FROM groupdocaccess gda " & _
    '    "LEFT JOIN doctype d ON " & _
    '    "gda.docid = d.doctype " & _
    '    "LEFT JOIN groupaccess ga ON " & _
    '    "ga.groupaccessid = gda.groupaccessid " & _
    '    ") gd  " & _
    '     " & _ON g.groupid = gd.groupid  " & _
    '    "WHERE "
    '    If pGroupId <> "" Then
    '        sSQl = sSQl & "g.groupid = " & pGroupId
    '    End If

    '    If pGroupName <> "" Then
    '        sSQl = sSQl & "g.groupname = " & pGroupName
    '    End If


    '    sSQl = sSQl & " ORDER BY "
    '    If pSortCol = "Group Code" Then
    '        sSQl = sSQl & "g.groupid"
    '    ElseIf pSortCol = "Group Name" Then
    '        sSQl = sSQl & "g.groupname"
    '    ElseIf pSortCol = "Document" Then
    '        sSQl = sSQl & "docname"
    '    ElseIf pSortCol = "Access" Then
    '        sSQl = sSQl & "gd.GroupAccessDesc"
    '    ElseIf pSortCol = "Allow Printing" Then
    '        sSQl = sSQl & "CanPrint"
    '    Else
    '        sSQl = sSQl & "g.groupid"
    '    End If

    '    If pSortOrder <> "" Then
    '        sSQl = sSQl & pSortOrder
    '    End If

    'End Function

    Public Function DMSF_DOCLINKSGET() As String
        'oracle george
        Dim s_sql As String
        If DocSession.OraClient Then
            s_sql = "SELECT d.DocId" & _
                ",dl.LinkDocId" & _
                ",d.Title" & _
                ",d.RefNo" & _
                ",d.FileName" & _
                ",d.Location" & _
                ",d.StatusId" & _
                ",dl.createdbye" & _
                ",ds.description AS statusdesc" & _
                ",(u.FirstName || ' ' || u.LastName) AS username" & _
                ",dl.createddate " & _
                "FROM DocLinks dl INNER JOIN " & _
                    "DocList d ON dl.LinkDocId = d.docId " & _
                    "INNER JOIN DocType dt ON d.DocType = dt.DocType " & _
                    "INNER JOIN DocStatus DS ON d.statusid=ds.statusid " & _
                    "INNER JOIN Users u ON dl.createdbye = u.userId " & _
                    "WHERE " & _
                    "dl.DocId = " & pDocId & " and dl.deletedby is null "
        Else
            s_sql = "SELECT d.DocId" & _
                ",dl.LinkDocId" & _
                ",d.Title" & _
                ",d.refno" & _
                ",d.FileName" & _
                ",d.Location" & _
                ",d.StatusId" & _
                ",dl.createdbye" & _
                ",ds.description AS statusdesc" & _
                ",(u.FirstName + ' ' + u.LastName) AS username" & _
                ",dl.createddate " & _
                "FROM DocLinks dl INNER JOIN " & _
                    "DocList d ON dl.LinkDocId = d.docId " & _
                    "INNER JOIN DocType dt ON d.DocType = dt.DocType " & _
                    "INNER JOIN DocStatus DS ON d.statusid=ds.statusid " & _
                    "INNER JOIN Users u ON dl.createdbye = u.userId " & _
                    "WHERE " & _
                    "dl.DocId = " & pDocId & " and dl.deletedby is null "
        End If
        
        Return s_sql


    End Function
    

    
    Public Function DMSF_DOCNOTESADD() As String
        Return "INSERT INTO DocNOTES " & _
           "(DocId " & _
           ",Notes " & _
           ",CreatedBy " & _
            ",CreatedDate " & _
           ",CreateIPAddress,NoteId) " & _
        "VALUES  " & _
           "(" & pDocId & _
           ",'" & Replace(pNote, "'", "''") & "' " & _
           ",'" & pUserId & "' " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
           ",'" & pIPAddress & "'," & pRecordId & ") "

    End Function

    Public Function DMSF_DOCTAGADD() As String
        'oracle george - change the function to avoid calling multiple sql in one statement
        If DocSession.OraClient Then
            Return "BEGIN INSERT INTO DocTags " & _
               "(DocId " & _
               ",Tags " & _
               ",CreatedBy " & _
                ",CreatedDate " & _
               ",CreateIPAddress) " & _
            "VALUES  " & _
               "(" & pDocId & _
               ",'" & Replace(pTags, "'", "''") & "' " & _
               ",'" & pUserId & "' " & _
               "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
               ",'" & pIPAddress & "'); " & _
                " END; "
        Else

            Return "INSERT INTO DocTags " & _
               "(DocId " & _
               ",Tags " & _
               ",CreatedBy " & _
                ",CreatedDate " & _
               ",CreateIPAddress) " & _
            "VALUES  " & _
               "(" & pDocId & _
               ",'" & Replace(pTags, "'", "''") & "' " & _
               ",'" & pUserId & "' " & _
               "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
               ",'" & pIPAddress & "') " 
            'oracle george
        End If

    End Function


    Public Function DMSF_ADDHISTORY()


        Return "INSERT INTO DocHistory " & _
           "(DocId " & _
           ",UserId " & _
           ",Action " & _
           ",ActionDate " & _
           ",IPAddress,ApproverID)  " & _
        "VALUES " & _
           "(" & pDocId & _
           ",'" & pUserId & "'" & _
           ",'" & Replace(pAction, "'", "''") & "'" & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
           ",'" & pIPAddress & "'" & _
           ",'" & Replace(pTask, "'", "''") & "')"

        'added tag: - for tags
    End Function

    Public Function DMSF_DOCTAGSGET()
        Return "SELECT dt.DocId " & _
      ",dt.Tags " & _
      ",dt.CreatedBy " & _
      ",dt.CreatedDate " & _
      ",dt.ModifiedBy " & _
      ",dt.ModifiedDate " & _
      ",dt.DeletedBy " & _
      ",dt.DeletedDate " & _
      ",dt.DelIPAddress " & _
      ",dt.ModIPAddress " & _
      ",dt.CreateIPAddress " & _
      IIf(DocSession.OraClient, ",(NVL(u.FirstName,'') || ' ' || NVL(u.LastName,''))", ",(ISNULL(u.FirstName,'') + ' ' + ISNULL(u.LastName,''))") & " AS username " & _
  "FROM DocTags  dt " & _
 "INNER JOIN Users u ON dt.createdby = u.userId " & _
 "WHERE " & _
 "(DocId = " & pDocId & ") and deletedby is null order by dt.CreatedDate desc "

    End Function
    Public Function DMSF_DOCTAGSDELETE() As String
        Return "UPDATE DocTags " & _
           "SET " & _
           "DeletedBy = '" & pDeletedBy & "' " & _
           ",DeletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
           ",DelIPAddress = '" & pIPAddress & "' " & _
        "WHERE " & _
           "DocId = " & pDocId & _
            "AND Tags = " & pTags


    End Function
    

    Public Function DMSF_USERGETGROUPS() As String
        Return "SELECT groupid,groupname FROM groups WHERE deletedby is null"
    End Function

    Public Function DMSF_DataChanges() As String
        Return "INSERT INTO DataChanges " & _
           "(tableName " & _
           ",RecordId " & _
           ",ModifiedBy " & _
           ",ColumnName " & _
           ",ModifiedDate " & _
           ",OldValue " & _
           ",NewValue " & _
           ",IPAddress) " & _
        "VALUES " & _
           "('" & ptableName & "'" & _
           ",'" & pRecordId & "'" & _
           ",'" & pModifiedBy & "'" & _
           ",'" & pColumnName & "'" & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
           ",'" & pOldValue & "'" & _
           ",'" & pNewValue & "'" & _
           ",'" & pIPAddress & "')"


    End Function
End Class
