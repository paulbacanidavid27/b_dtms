Imports System.Data.SqlClient

Public Class DocRouting
    Public Sub New()

    End Sub
    Dim RoutingSeqno As String
    Dim IpAdd As String
    Dim ApproverId As String
    Dim Sender As String
    Dim DocId As String
    Dim SeqNo As String
    Dim OldSeqNo As Integer
    Dim StatusId As Integer
    Dim OutStatusId As String
    Dim DocType As String
    Dim Comment As String
    Dim CarbonCopy As String
    Dim Urgent As String
    Dim Remarks As String
    Dim UserId As String
    Dim Action As String
    Dim DueDate As String
    Dim AssignedDate As String
    Dim RoutedTo As String
    Dim ExistsInbox As String

    Public Property pExistsInbox As Boolean
        Get
            Return ExistsInbox
        End Get
        Set(ByVal value As Boolean)
            ExistsInbox = value
        End Set
    End Property

    Public Property pRoutedTo() As String
        Get
            Return RoutedTo
        End Get
        Set(ByVal value As String)
            RoutedTo = value
        End Set
    End Property

    Public Property pSender() As String
        Get
            Return Sender
        End Get
        Set(ByVal value As String)
            Sender = value
        End Set

    End Property
    Public Property pOutStatusId() As String
        Get
            Return OutStatusId
        End Get
        Set(ByVal value As String)
            OutStatusId = value
        End Set

    End Property
    Public Property pCarbonCopy() As String
        Get
            Return CarbonCopy
        End Get
        Set(ByVal value As String)
            CarbonCopy = value
        End Set

    End Property
    Public Property pUrgent() As String
        Get
            Return Urgent
        End Get
        Set(ByVal value As String)
            Urgent = value
        End Set

    End Property

    Public Property pSeqNo() As String
        Get
            Return SeqNo
        End Get
        Set(ByVal value As String)
            SeqNo = value
        End Set

    End Property
    Public Property pRoutingSeqNo() As String
        Get
            Return RoutingSeqNo
        End Get
        Set(ByVal value As String)
            RoutingSeqNo = value
        End Set

    End Property
    Public Property pOldSeqNo() As Integer
        Get
            Return OldSeqNo
        End Get
        Set(ByVal value As Integer)
            OldSeqNo = value
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

    Public Property pStatusId() As Integer
        Get
            Return StatusId
        End Get
        Set(ByVal value As Integer)
            StatusId = value
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

    Public Property pDueDate() As String
        Get
            Return DueDate
        End Get
        Set(ByVal value As String)
            DueDate = value
        End Set

    End Property

    Public Property pAssignedDate() As String
        Get
            Return AssignedDate
        End Get
        Set(ByVal value As String)
            AssignedDate = value
        End Set

    End Property

    Public Property pRemarks() As String
        Get
            Return Remarks
        End Get
        Set(ByVal value As String)
            Remarks = value
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

    Public Property pUserId() As String
        Get
            Return UserId
        End Get
        Set(ByVal value As String)
            UserId = value
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

    Public Function RetrieveRouting() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCROUTINGGET"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@approverId", pApproverId)

            Dim s_sql As String
            If DocSession.OraClient Then
                s_sql = "SELECT dr.SeqNo " & _
                      ",dr.DocId " & _
                      ",dr.ApproverId " & _
                      ",case when dr.ActionDate is null then '' else TO_CHAR(dr.ActionDate,'mm/dd/yyyy')  end AS actdate " & _
                      ",dr.StatusId " & _
                      ",dl.StatusId AS DocStatusId " & _
                      ",NVL(dr.Comment,' ') AS comment " & _
                      ",NVL(ua.FirstName,'') || ' ' || NVL(ua.LastName,'') AS approver " & _
                          ",NVL(s.FirstName,'') || ' ' || NVL(s.LastName,'') AS sendername " & _
                      ",ds.description AS statusdesc " & _
                      ",dr.Remarks " & _
                      ",dr.DueDate " & _
                      ",dr.sender " & _
                      ",ua.email " & _
                      ",NVL(dr.CarbonCopy,0) AS cc " & _
                      ",NVL(dr.Urgent,0) AS urgent " & _
                      ",NVL(dr.OutStatusId,'0') as OutStatusId " & _
                    ",NVL(ds2.Description,' ') as OutStatus " & _
                  "FROM dbo.DocRouting dr " & _
                  "INNER JOIN Users ua " & _
                  "ON dr.approverid = ua.userid " & _
                  "LEFT JOIN Users s " & _
                  "ON dr.sender = s.userid " & _
                  "INNER JOIN DocStatus ds " & _
                  "ON dr.statusid = ds.statusid " & _
                  "LEFT JOIN docStatus ds2 ON " & _
                    "ds2.StatusId = dr.OutStatusId " & _
                  "INNER JOIN DocList dl " & _
                  "ON dl.docid = dr.docid " & _
                  "WHERE dr.docid is not null "
            Else
                s_sql = "SELECT dr.SeqNo " & _
                      ",dr.DocId " & _
                      ",dr.ApproverId " & _
                      ",actdate = case when dr.ActionDate is null then '' else convert(char(10),dr.ActionDate,101) end " & _
                      ",dr.StatusId " & _
                      ",DocStatusId = dl.StatusId " & _
                      ",comment=isnull(dr.Comment,'') " & _
                      ",approver = isnull(ua.FirstName,'') + ' ' + isnull(ua.LastName,'') " & _
                          ",sendername = isnull(s.FirstName,'') + ' ' + isnull(s.LastName,'') " & _
                      ",statusdesc = ds.description " & _
                      ",dr.Remarks " & _
                      ",dr.DueDate " & _
                      ",dr.sender " & _
                      ",ua.email " & _
                      ",cc = isnull(dr.CarbonCopy,0) " & _
                      ",urgent = isnull(dr.Urgent,0) " & _
                      ",OutStatusId = isnull(dr.OutStatusId,'0') " & _
                        ",OutStatus = isnull(ds2.Description,'') " & _
                  "FROM dbo.DocRouting dr " & _
                  "INNER JOIN Users ua " & _
                  "ON dr.approverid = ua.userid " & _
                  "LEFT JOIN Users s " & _
                  "ON dr.sender = s.userid " & _
                  "INNER JOIN DocStatus ds " & _
                  "ON dr.statusid = ds.statusid " & _
                  "LEFT JOIN docStatus ds2 ON " & _
                    "ds2.StatusId = dr.OutStatusId " & _
                  "INNER JOIN DocList dl " & _
                  "ON dl.docid = dr.docid " & _
                  "WHERE dr.docid is not null  "
            End If

            If pDocId <> "" Then
                s_sql = s_sql & " AND dr.DocId = " & pDocId
            End If
            If pApproverId <> "" Then
                s_sql = s_sql & " AND dr.approverid = '" & pApproverId & "' "
            End If
            If pCarbonCopy <> "" Then
                If pCarbonCopy = "1" Then
                    s_sql = s_sql & " AND dr.CarbonCopy = 1 "
                Else
                    s_sql = s_sql & " AND (dr.CarbonCopy is null or dr.CarbonCopy = 0) "
                End If

            End If

            s_sql = s_sql & "ORDER BY dr.SeqNo desc "
            objCommand.CommandText = s_sql
            ldata = objCommand.Fill()

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

    Public Function IsApprover() As Boolean
        Dim objCommand As clsSqlConn
        Dim s_sql As String
        'Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 

            s_sql = "SELECT dr.SeqNo " & _
                  "FROM dbo.DocRouting dr " & _
                  "WHERE " & _
                    " dr.DocId = " & pDocId & " " & _
                    " AND dr.approverid = '" & pApproverId & "' "


            objCommand.CommandText = s_sql
            Return objCommand.ExecHasRow()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function RetrieveCC() As String
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCROUTINGGET"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@approverId", pApproverId)

            Dim s_sql As String
            If DocSession.OraClient Then
                s_sql = "select NVL(u.firstname,'')||' '||NVL(u.lastname,'') as CCName " & _
                        " from DocRouting dr Inner Join users u " & _
                        " on dr.approverid = u.userid where DocId = " & pDocId & " and CarbonCopy = 1 "
            Else
                s_sql = "select ISNULL(u.firstname,'')+' '+ISNULL(u.lastname,'') as CCName " & _
                        " from DocRouting dr Inner Join users u " & _
                        " on dr.approverid = u.userid where DocId = " & pDocId & " and CarbonCopy = 1 "
            End If

            s_sql = s_sql & " Order By u.firstname,u.lastname "

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill()
            Dim lsRet As String = ""
            For Each lrow As DataRow In ldata.Rows
                If lsRet = "" Then
                    lsRet = lrow("ccname")
                Else
                    lsRet = lsRet & ", " & lrow("ccname")
                End If

            Next
            Return lsRet

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
    Public Function RetrieveRoutingQuick() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCROUTINGGET"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@approverId", pApproverId)

            Dim s_sql As String
            If DocSession.OraClient Then
                s_sql = "SELECT dr.SeqNo " & _
                      ",dr.DocId " & _
                      ",dr.ApproverId " & _
                      ",case when dr.ActionDate is null then '' else TO_CHAR(dr.ActionDate,'mm/dd/yyyy')  end AS actdate " & _
                      ",dr.StatusId " & _
                      ",dl.StatusId AS DocStatusId " & _
                      ",nvl(dr.OutStatusId,0) as OutStatusId " & _
                      ",nvl(dsr.Description,'') as OutStatusDesc" & _
                      ",NVL(dr.Comment,'') AS comment " & _
                      ",NVL(ua.FirstName,'') || ' ' || NVL(ua.LastName,'') AS approver " & _
                          ",NVL(s.FirstName,'') || ' ' || NVL(s.LastName,'') AS sendername " & _
                      ",ds.description AS statusdesc " & _
                      ",dr.Remarks " & _
                      ",dr.DueDate " & _
                      ",dr.sender " & _
                      ",ua.email " & _
                      ",NVL(dr.CarbonCopy,0) AS cc " & _
                      ",NVL(dr.Urgent,0) AS cc " & _
                  "FROM dbo.DocRouting dr " & _
                  "INNER JOIN Users ua " & _
                  "ON dr.approverid = ua.userid " & _
                  "LEFT JOIN Users s " & _
                  "ON dr.sender = s.userid " & _
                  "INNER JOIN DocStatus ds " & _
                  "ON dr.statusid = ds.statusid " & _
                  "INNER JOIN DocList dl " & _
                  "ON dl.docid = dr.docid " & _
                  "WHERE dr.docid is not null "
            Else
                s_sql = "SELECT dr.SeqNo " & _
                      ",dr.DocId " & _
                      ",dr.ApproverId " & _
                      ",actdate = case when dr.ActionDate is null then '' else convert(char(10),dr.ActionDate,101) end " & _
                      ",dr.StatusId " & _
                      ",DocStatusId = dl.StatusId " & _
                      ",OutStatusId = isnull(dr.OutStatusId,0) " & _
                      ",OutStatusDesc = isnull(dsr.Description,'') " & _
                      ",comment=isnull(dr.Comment,'') " & _
                      ",approver = isnull(ua.FirstName,'') + ' ' + isnull(ua.LastName,'') " & _
                          ",sendername = isnull(s.FirstName,'') + ' ' + isnull(s.LastName,'') " & _
                      ",statusdesc = ds.description " & _
                      ",dr.Remarks " & _
                      ",dr.DueDate " & _
                      ",dr.sender " & _
                      ",ua.email " & _
                      ",cc = isnull(dr.CarbonCopy,0) " & _
                      ",cc = isnull(dr.Urgent,0) " & _
                  "FROM dbo.DocRouting dr " & _
                  "INNER JOIN Users ua " & _
                  "ON dr.approverid = ua.userid " & _
                  "LEFT JOIN Users s " & _
                  "ON dr.sender = s.userid " & _
                  "INNER JOIN DocStatus ds " & _
                  "ON dr.statusid = ds.statusid " & _
                  "LEFT JOIN DocStatus dsr " & _
                  "ON dr.outstatusid = dsr.statusid " & _
                  "INNER JOIN DocList dl " & _
                  "ON dl.docid = dr.docid " & _
                  "WHERE dr.docid is not null  "
            End If

            If pDocId <> "" Then
                s_sql = s_sql & " AND dr.DocId = " & pDocId
            End If
            If pApproverId <> "" Then
                s_sql = s_sql & " AND dr.approverid = '" & pApproverId & "' "
            End If
            s_sql = s_sql & "ORDER BY dr.SeqNo desc "
            objCommand.CommandText = s_sql
            ldata = objCommand.Fill()

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

    Public Function ValidRecipient() As Boolean
        Dim objCommand As clsSqlConn



        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCROUTINGGET"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@approverId", pApproverId)

            Dim s_sql As String
            s_sql = "SELECT dr.SeqNo " & _
              "FROM dbo.DocRouting dr " & _
              "WHERE dr.statusid = 2 "
            If pDocId <> "" Then
                s_sql = s_sql & " AND dr.DocId = " & pDocId
            End If
            If pApproverId <> "" Then
                s_sql = s_sql & " AND dr.approverid = '" & pApproverId & "' "
            End If

            objCommand.CommandText = s_sql
            If objCommand.ExecHasRow() Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    'Public Function RetrieveRoutingOther() As DataTable
    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

    '        'objCommand.CommandText = "xMSP_DOCROUTINGGET"
    '        'objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        'objCommand.ParametersAddWithValue("@approverId", pApproverId)

    '        Dim s_sql As String
    '        s_sql = "SELECT dr.SeqNo " & _
    '              ",dr.DocId " & _
    '              ",dr.ApproverId " & _
    '              ",actdate = case when dr.ActionDate is null then '' else convert(char(10),dr.ActionDate,101) end " & _
    '              ",dr.StatusId " & _
    '              ",comment=isnull(dr.Comment,'') " & _
    '              ",approver = isnull(ua.FirstName,'') + ' ' + isnull(ua.LastName,'') " & _
    '                  ",sendername = isnull(s.FirstName,'') + ' ' + isnull(s.LastName,'') " & _
    '              ",statusdesc = ds.description " & _
    '              ",dr.Remarks " & _
    '              ",dr.DueDate " & _
    '              ",dr.sender " & _
    '              ",ua.email " & _
    '              ",urgent = isnull(dr.Urgent,0) " & _
    '          "FROM dbo.DocRouting dr " & _
    '          "INNER JOIN Users ua " & _
    '          "ON dr.approverid = ua.userid " & _
    '          "LEFT JOIN Users s " & _
    '          "ON dr.sender = s.userid " & _
    '          "INNER JOIN DocStatus ds " & _
    '          "ON dr.statusid = ds.statusid " & _
    '          "WHERE dr.docid is not null "
    '        If pDocId <> "" Then
    '            s_sql = s_sql & " AND dr.DocId = " & pDocId
    '        End If
    '        If pApproverId <> "" Then
    '            s_sql = s_sql & " AND dr.approverid <> '" & pApproverId & "' "
    '        End If
    '        s_sql = s_sql & "ORDER BY dr.SeqNo "
    '        objCommand.CommandText = s_sql
    '        ldata = objCommand.Fill()

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

    'Public Function RetrieveRoutingCopy() As DataTable
    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

    '        'objCommand.CommandText = "xMSP_DOCROUTINGGET"
    '        'objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        'objCommand.ParametersAddWithValue("@approverId", pApproverId)

    '        Dim s_sql As String
    '        s_sql = "SELECT dr.SeqNo " & _
    '              ",dr.DocId " & _
    '              ",dr.ApproverId " & _
    '              ",actdate = case when dr.ActionDate is null then '' else convert(char(10),dr.ActionDate,101) end " & _
    '              ",dr.StatusId " & _
    '              ",StatusDesc = case when dr.StatusId = 3 then 'Acknowledged - '+isnull(dr.comment,'') else 'Pending' end " & _
    '              ",comment=isnull(dr.Comment,'') " & _
    '              ",approver = isnull(ua.FirstName,'') + ' ' + isnull(ua.LastName,'') " & _
    '                  ",sendername = isnull(s.FirstName,'') + ' ' + isnull(s.LastName,'') " & _
    '              ",dr.Remarks " & _
    '              ",dr.DueDate " & _
    '              ",dr.sender " & _
    '              ",ua.email " & _
    '          "FROM dbo.DocRouting dr " & _
    '          "INNER JOIN Users ua " & _
    '          "ON dr.approverid = ua.userid " & _
    '          "LEFT JOIN Users s " & _
    '          "ON dr.sender = s.userid " & _
    '          "WHERE dr.docid is not null and (dr.CarbonCopy = 1 or dr.StatusId = -2) "
    '        If pDocId <> "" Then
    '            s_sql = s_sql & " AND dr.DocId = " & pDocId
    '        End If
    '        'If pApproverId <> "" Then
    '        's_sql = s_sql & " AND dr.approverid <> '" & pApproverId & "' "
    '        'End If
    '        s_sql = s_sql & " ORDER BY dr.SeqNo "
    '        objCommand.CommandText = s_sql
    '        ldata = objCommand.Fill()

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
    'Public Sub ApproveDoc(ByVal objCommand As SqlCommand)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCAPPROVALUPDATE"

    '        objCommand.Parameters.Clear()
    '        objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        objCommand.ParametersAddWithValue("@lineNumber", pLineNumber)
    '        objCommand.ParametersAddWithValue("@approverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@seqNo", pSeqNo)
    '        objCommand.ParametersAddWithValue("@statusId", pStatusId)
    '        objCommand.ParametersAddWithValue("@IpAddress", pIpAddress)
    '        objCommand.ParametersAddWithValue("@Comment", pComment)


    '        objCommand.ExecuteNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

    'Public Function ApproverStatus() As DataTable

    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim oConn As New SqlConnection(str)

    '    Dim objCommand As New SqlCommand
    '    Dim adpSecurity As New SqlDataAdapter
    '    Dim ldata As New DataTable
    '    Dim lrow As DataRow = ldata.NewRow
    '    Try
    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.Connection = oConn
    '        objCommand.CommandText = "xMSP_DOCAPPROVERSTATUS"
    '        objCommand.ParametersAddWithValue("@docId", pDocId)
    '        objCommand.Connection.Open()
    '        adpSecurity.SelectCommand = objCommand
    '        adpSecurity.Fill(ldata)

    '        Return ldata



    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If
    '        If Not adpSecurity Is Nothing Then
    '            adpSecurity.Dispose()
    '            adpSecurity = Nothing
    '        End If

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If

    '    End Try
    'End Function



    'Public Sub DeleteApprovers(ByVal objCommand As SqlClient.SqlCommand)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCTYPEAPPROVERSDELETE"


    '        objCommand.Parameters.Clear()

    '        objCommand.ParametersAddWithValue("@DocType", pDocType)
    '        objCommand.ParametersAddWithValue("@approverId", pApproverId)
    '        objCommand.ParametersAddWithValue("@seqNo", pSeqNo)
    '        objCommand.ExecuteNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

    'Public Function RetrieveDocTypeApprovers() As DataTable
    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim oConn As New SqlConnection(str)

    '    Dim objCommand As New SqlCommand
    '    Dim adpSecurity As New SqlDataAdapter
    '    Dim ldata As New DataTable
    '    Dim lrow As DataRow = ldata.NewRow
    '    Try
    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.Connection = oConn
    '        objCommand.CommandText = "xMSP_DOCTYPEAPPROVERSGET"
    '        objCommand.ParametersAddWithValue("@DocType", pDocType)
    '        objCommand.Connection.Open()
    '        adpSecurity.SelectCommand = objCommand
    '        adpSecurity.Fill(ldata)

    '        Return ldata



    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If
    '        If Not adpSecurity Is Nothing Then
    '            adpSecurity.Dispose()
    '            adpSecurity = Nothing
    '        End If

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If

    '    End Try

    'End Function
    Public Sub CancelRouting()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "DELETE FROM DocRouting " & _
                " WHERE DocId = " & pDocId & " AND seqno = " & pSeqNo


            objCommand.ExecNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Sub
    Public Function RetriveCurrentApprover()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT u.Firstname+' '+u.LastName as RoutedTo " & _
            "FROM DocRouting dr Inner Join Users u " & _
            "ON dr.approverid = u.UserId " & _
            "WHERE dr.DocId = " & pDocId & " And dr.seqno = " & pSeqNo

            Return objCommand.ExecScalar3()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    'Public Sub UpdateDocRoutingSeqNoAndRoutedTo()
    '    Dim objCommand As clsSqlConn
    '    Try
    '        objCommand = New clsSqlConn

    '        objCommand.pCommandType = CommandType.Text
    '        objCommand.pCommandText = "Update Doclist Set routingseqno = " & pSeqNo & ", RoutedTo = '" & pRoutedTo & "'" & _
    '            " WHERE DocId = " & pDocId


    '        objCommand.ExecNonQuery()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '    End Try
    'End Sub

    Public Sub DeleteRouting(ByVal objCommand As clsSqlConn)
        Try


            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.pCommandText = "BEGIN DELETE FROM DocRouting " & _
                " WHERE DocId = " & pDocId & "; " & _
                " DELETE FROM DocInbox " & _
                " WHERE DocId = " & pDocId & "; END;"
            Else
                objCommand.pCommandText = "DELETE FROM DocRouting " & _
                " WHERE DocId = " & pDocId & " " & _
                " DELETE FROM DocInbox " & _
                " WHERE DocId = " & pDocId
            End If


            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Sub

    Public Sub RouteDocument(ByVal objCommand As clsSqlConn)
        Dim s_sql As String
        Try


            If pDueDate = "" Then
                pDueDate = "01/01/1900"
            End If
            If pOutStatusId Is Nothing OrElse pOutStatusId = "" Then
                pOutStatusId = "null"
            End If
            'ORACLE GEORGE
            If DocSession.OraClient Then
                s_sql = "BEGIN update docrouting set CarbonCopy = CarbonCopy WHERE docid = " & pDocId & " and ApproverId = '" & pApproverId & "' and StatusId = " & pStatusId & " and (CarbonCopy is null or CarbonCopy = 0); " & _
                        "IF SQL%ROWCOUNT = 0 THEN INSERT INTO DocRouting " & _
                        "(SeqNo,DocId,ApproverId,StatusId,DueDate,remarks,Sender,AssignedDate,Urgent,OutStatusId) " & _
                        "VALUES (" & pSeqNo & ", " & pDocId & ",'" & pApproverId & "'," & pStatusId & ",TO_DATE('" & pDueDate & "','mm/dd/yyyy'),'" & pRemarks & "','" & pSender & "',sysdate," & pUrgent & "," & pOutStatusId & "); END IF; END; "
            Else
                s_sql = "update docrouting set CarbonCopy = CarbonCopy WHERE docid = " & pDocId & " and ApproverId = '" & pApproverId & "' and StatusId = " & pStatusId & " and (CarbonCopy is null or CarbonCopy = 0) " & _
                        "if @@rowcount = 0 INSERT INTO DocRouting " & _
                        "(SeqNo,DocId,ApproverId,StatusId,DueDate,remarks,Sender,AssignedDate,Urgent,OutStatusId) " & _
                        "VALUES (" & pSeqNo & ", " & pDocId & ",'" & pApproverId & "'," & pStatusId & ",'" & pDueDate & "','" & pRemarks & "','" & pSender & "','" & DateTime.Now.ToString & "'," & pUrgent & "," & pOutStatusId & ") "
            End If
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecTranNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try
    End Sub
    'ORACLE GEORGE
    Public Sub RouteDocumentCopy(ByVal objCommand As clsSqlConn) ', ByVal aExistsInInbox As Boolean)
        Dim s_sql As String
        Try
            If pDueDate = "" Then
                pDueDate = "01/01/1900"
            End If
            Dim lsSqNo As String
            lsSqNo = DocSession.getNextID("SeqNo")

            If DocSession.OraClient Then
                s_sql = "BEGIN update docrouting set CarbonCopy = CarbonCopy WHERE docid = " & pDocId & " and ApproverId = '" & pApproverId & "' and StatusId = " & pStatusId & " and CarbonCopy = 1; " & _
            "IF SQL%ROWCOUNT = 0 THEN INSERT INTO DocRouting " & _
            "(SeqNo,DocId,ApproverId,StatusId,DueDate,remarks,Sender,AssignedDate,CarbonCopy,Urgent) " & _
            "VALUES (" & lsSqNo & "," & pDocId & ",'" & pApproverId & "','" & pStatusId & "',TO_DATE('" & pDueDate & "','mm/dd/yyyy'),'" & pRemarks & "','" & pSender & "','" & pAssignedDate & "'," & pCarbonCopy & "," & pUrgent & "); END IF; END;"


            Else
                s_sql = "update docrouting set CarbonCopy = CarbonCopy WHERE docid = " & pDocId & " and ApproverId = '" & pApproverId & "' and StatusId = " & pStatusId & " and CarbonCopy = 1 " & _
                "if @@rowcount = 0 INSERT INTO DocRouting " & _
                "(SeqNo,DocId,ApproverId,StatusId,DueDate,remarks,Sender,AssignedDate,CarbonCopy,Urgent) " & _
                "VALUES (" & lsSqNo & "," & pDocId & ",'" & pApproverId & "','" & pStatusId & "','" & pDueDate & "','" & pRemarks & "','" & pSender & "','" & pAssignedDate & "'," & pCarbonCopy & "," & pUrgent & ") "
            End If
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecTranNonQuery()

            Dim oDocs As New DocList
            oDocs.pDocId = pDocId
            oDocs.pUserId = pApproverId
            oDocs.pSeqNo = lsSqNo

            oDocs.AddToInbox(objCommand) ', aExistsInInbox)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try
    End Sub


    Public Function CheckIfCopyExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.pCommandText = "SELECT docid FROM docRouting WHERE docId = " & pDocId & " AND approverid = '" & pApproverId & "' AND RowNum = 1"
            Else
                objCommand.pCommandText = "SELECT TOP 1 docid FROM docRouting WHERE docId = " & pDocId & " AND approverid = '" & pApproverId & "' "
            End If



            Return objCommand.ExecHasRow()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Function CountApprover() As Integer


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT count(docid) FROM docRouting WHERE docId = " & pDocId & " AND CarbonCopy = 0 "

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

    Public Sub UpdateDueDate(ByVal objCommand As clsSqlConn)

        Try

            If pDueDate = "" Then
                pDueDate = "01/01/1900"
            End If
            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "UPDATE DocRouting " & _
                "SET DueDate =" & IIf(DocSession.OraClient, "TO_DATE('" & pDueDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & pDueDate & "'") & " " & _
                "WHERE  seqno  = " & pSeqNo

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    'Public Sub UpdateDueDate2(ByVal objCommand As clsSqlConn)

    '    Try


    '        If pDueDate = "" Then
    '            pDueDate = "01/01/1900"
    '        End If
    '        objCommand.pCommandType = CommandType.Text
    '        objCommand.pCommandText = "UPDATE DocRouting " & _
    '            "SET DueDate ='" & pDueDate & "', statusid = 2,actiondate=null  " & _
    '            "WHERE  seqno  = " & pSeqNo

    '        objCommand.ExecTranNonQuery()
    '        Dim oDocs As New DocList
    '        oDocs.pDocId = pDocId
    '        oDocs.pUserId = pApproverId
    '        oDocs.pSeqNo = pSeqNo
    '        oDocs.AddToInbox(objCommand, pExistsInbox)
    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub
    'oracle - datetime
    Public Function updateroute() As String
        Return "UPDATE DocRouting SET actiondate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",StatusId = " & CStr(pStatusId) & _
      ",Comment = '" & pComment & "' " & _
 "WHERE SeqNo = " & CStr(pOldSeqNo) & " and DocId = " & CStr(pDocId) & " and ApproverId = '" & pApproverId & "'"

    End Function
    Public Sub UpdateRouteStatus(ByVal objCommand As clsSqlConn)

        Try
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = updateroute()


            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub DenyRouteStatus()

        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            pStatusId = 4
            objCommand.CommandText = updateroute()

            objCommand.ExecNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Sub

    Public Function MaxRoutingSeqNo() As Integer

        Dim objCommand As clsSqlConn
        Dim s_sql As String

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            If DocSession.OraClient Then
                s_sql = "SELECT NVL(max(seqno),0) FROM docrouting WHERE (carboncopy is null or carboncopy = 0) and docid = " & DocSession.sDocID
            Else
                s_sql = "SELECT isnull(max(seqno),0) FROM docrouting WHERE (carboncopy is null or carboncopy = 0) and docid = " & DocSession.sDocID
            End If

            objCommand.CommandText = s_sql

            Return objCommand.ExecScalar()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function MaxRoutingSeqNo(docid As String) As Integer

        Dim objCommand As clsSqlConn
        Dim s_sql As String

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            If DocSession.OraClient Then
                s_sql = "SELECT NVL(max(seqno),0) FROM docrouting WHERE (carboncopy is null or carboncopy = 0) and docid = " & docid
            Else
                s_sql = "SELECT isnull(max(seqno),0) FROM docrouting WHERE (carboncopy is null or carboncopy = 0) and docid = " & docid
            End If

            objCommand.CommandText = s_sql

            Return objCommand.ExecScalar()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function


    'oracle - datetime
    Public Sub UpdateDocApprover(ByVal objCommand As clsSqlConn)


        Try
            Dim s_sql As String = "UPDATE DOCLIST SET " & _
                "modifiedby = '" & pUserId & "' "

            If pSeqNo <> "" Then
                s_sql = s_sql & ",routingSeqNo = " & pSeqNo & " "  'case when @Title is null then Title else @Title end " & _
            End If

            If pRoutedTo <> "" Then
                s_sql = s_sql & ",RoutedTo = '" & pRoutedTo & "' "  'case when @Title is null then Title else @Title end " & _
            End If

            s_sql = s_sql & ",modifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                "WHERE  " & _
                    "DocId = " & pDocId

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub

    Public Sub UpdateDocRouting(ByVal objCommand As clsSqlConn)


        Try
            Dim s_sql As String = "UPDATE DOCROUTING SET " & _
                "CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                ",RoutingSeqno = " & pSeqNo & " " & _
                "WHERE  " & _
                    "Seqno = " & pRoutingSeqNo

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub
End Class
