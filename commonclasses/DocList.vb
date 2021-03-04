
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Web

Public Class DocList
    Dim Confidential As String
    Dim SeqNo As String
    Dim RefNo As String
    Dim DocId As String
    Dim FolderId As String
    Dim FolderDesc As String
    Dim ParentDocId As String
    Dim DocTitle As String
    Dim DocType As String
    Dim DocTypeOrig As String
    Dim UserId As String
    Dim FileName As String
    Dim Comment As String
    Dim GroupId As String
    Dim Version As Integer
    Dim Checkout As Boolean
    Dim IPAddress As String
    Dim DocStatus As String
    Dim DocStatusId As String = ""
    Dim FinalDocStatus As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim RowsPerPage As String
    Dim Idx As String
    Dim DocVersion As String
    Dim RetVal As String
    Dim CreatedDate As String
    Dim DueDate As String
    Dim ModifiedDate As String
    Dim Author As String
    Dim RowCount As Integer
    Dim DocAge As String
    Dim AgeProcess As String
    Dim ArchivedBy As String
    Dim ArchiveFrom As String
    Dim ArchiveTo As String
    Dim Classification As String
    Dim OfficeCode As String
    Dim _Process As String = "<"
    Dim RoutedTo As String
    Dim CreatedDateFrom As String = "" '"01/01/" & Year(Date.Now)
    Dim CreatedDateTo As String = "" 'Date.Now.ToShortDateString
    Dim ReceivedDate As String
    Dim PersonnelInCharge As String
    Dim Subject As String
    Dim StatusId As String
    Dim SetRetention As String
    Dim Manner As String
    Dim DeleteReason As String = ""
    Dim CompletedDate As String
    Dim CutOffDate As String
    Dim Prefix As String = ""
#Region "sql for retrieval"
    Dim sqlDocRouting As String = "SELECT distinct dl.docid FROM doclist dl inner join docrouting dr on dl.docid = dr.docid " & _
                                    "INNER JOIN doctype dt on dt.doctype = dl.doctype" & _
                                    " WHERE dl.statusid <> 5 and dr.approverid = '" & DocSession.sUserId & "' and (dl.Confidential is null or dl.Confidential = 0) "
    Dim sqlUserRouting As String = "SELECT dl.docid FROM doclist dl inner join docrouting dr on dl.docid = dr.docid " & _
                                    "INNER JOIN doctype dt on dt.doctype = dl.doctype " & _
                                    "INNER JOIN users u ON u.userid = dr.approverid " & _
                                    "INNER JOIN groups g ON g.groupid = u.usergroup and g.OfficeCode = '" & DocSession.sOfcCode & "'" & _
                                    " WHERE dl.statusid <> 5 and (dl.Confidential is null or dl.Confidential = 0) "
#End Region
    Public Sub New()

    End Sub
    Public Property pPrefix() As String
        Get
            Return Prefix
        End Get
        Set(ByVal value As String)
            Prefix = value
        End Set

    End Property
    Dim FileSize As String
    Public Property pConfidential() As String
        Get
            Return Confidential
        End Get
        Set(ByVal value As String)
            Confidential = value
        End Set

    End Property
    Public Property pFileSize() As String
        Get
            Return FileSize
        End Get
        Set(ByVal value As String)
            FileSize = value
        End Set

    End Property

    Dim RecCount As Integer

    Public Property pCount() As Integer
        Get
            Return RecCount
        End Get
        Set(ByVal value As Integer)
            RecCount = value
        End Set

    End Property
    Public Property pCompletedDate() As String
        Get
            Return CompletedDate
        End Get
        Set(ByVal value As String)
            CompletedDate = value
        End Set

    End Property
    Public Property pCutOffDate() As String
        Get
            Return CutOffDate
        End Get
        Set(ByVal value As String)
            CutOffDate = value
        End Set

    End Property
    Public Property pDeleteReason() As String
        Get
            Return DeleteReason
        End Get
        Set(ByVal value As String)
            DeleteReason = value
        End Set

    End Property
    Public Property pManner() As String
        Get
            Return Manner
        End Get
        Set(ByVal value As String)
            Manner = value
        End Set

    End Property
    Public Property pSetRetention() As String
        Get
            Return SetRetention
        End Get
        Set(ByVal value As String)
            SetRetention = value
        End Set

    End Property
    Dim bExistsInInbox As Boolean
    Public Property pExistsInInbox As Boolean
        Get
            Return bExistsInInbox
        End Get
        Set(ByVal value As Boolean)
            bExistsInInbox = value
        End Set
    End Property

    Dim bExistsInOutbox As Boolean
    Public Property pExistsInOutbox As Boolean
        Get
            Return bExistsInOutbox
        End Get
        Set(ByVal value As Boolean)
            bExistsInOutbox = value
        End Set
    End Property

    Public Property pPersonnelInCharge As String
        Get
            Return PersonnelInCharge
        End Get
        Set(ByVal value As String)
            PersonnelInCharge = value
        End Set
    End Property

    Public Property pClassification As String
        Get
            Return Classification
        End Get
        Set(ByVal value As String)
            Classification = value
        End Set
    End Property

    Public Property pStatusId As String
        Get
            Return StatusId
        End Get
        Set(ByVal value As String)
            StatusId = value
        End Set
    End Property

    Public Property pSubject As String
        Get
            Return Subject
        End Get
        Set(ByVal value As String)
            Subject = value
        End Set
    End Property

    'Public Property pReceivedDate As String
    '    Get
    '        Return ReceivedDate
    '    End Get
    '    Set(ByVal value As String)
    '        ReceivedDate = value
    '    End Set
    'End Property

    Public Property pRefNo() As String
        Get
            Return RefNo
        End Get
        Set(ByVal value As String)
            RefNo = value
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
    Public Property pParentDocId() As String
        Get
            Return ParentDocId
        End Get
        Set(ByVal value As String)
            ParentDocId = value
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
    Public Property pProcess() As String
        Get
            Return _Process
        End Get
        Set(ByVal value As String)
            _Process = value
        End Set

    End Property
    Public Property pOfficeCode() As String
        Get
            Return OfficeCode
        End Get
        Set(ByVal value As String)
            OfficeCode = value
        End Set

    End Property
    Public Property pArchiveTo() As String
        Get
            Return ArchiveTo
        End Get
        Set(ByVal value As String)
            ArchiveTo = value
        End Set

    End Property
    Public Property pArchivedBy() As String
        Get
            Return ArchivedBy
        End Get
        Set(ByVal value As String)
            ArchivedBy = value
        End Set

    End Property
    Public Property pArchiveFrom() As String
        Get
            Return ArchiveFrom
        End Get
        Set(ByVal value As String)
            ArchiveFrom = value
        End Set

    End Property
    Public Property pAgeProcess() As String
        Get
            Return AgeProcess
        End Get
        Set(ByVal value As String)
            AgeProcess = value
        End Set

    End Property
    Public Property pDocAge() As String
        Get
            Return DocAge
        End Get
        Set(ByVal value As String)
            DocAge = value
        End Set

    End Property

    Public Property pAuthor() As String
        Get
            Return Author
        End Get
        Set(ByVal value As String)
            Author = value
        End Set

    End Property
    Public Property pCreatedDateTo() As String
        Get
            Return CreatedDateTo
        End Get
        Set(ByVal value As String)
            CreatedDateTo = value
        End Set

    End Property

    Public Property pCreatedDateFrom() As String
        Get
            Return CreatedDateFrom
        End Get
        Set(ByVal value As String)
            CreatedDateFrom = value
        End Set

    End Property
    Public Property pCreatedDate() As String
        Get
            Return CreatedDate
        End Get
        Set(ByVal value As String)
            CreatedDate = value
        End Set

    End Property

    Dim SenderName As String

    Public Property pSenderName() As String
        Get
            Return SenderName
        End Get
        Set(ByVal value As String)
            SenderName = value
        End Set

    End Property

    Dim NoPages As String = ""

    Public Property pNoPages() As String
        Get
            Return NoPages
        End Get
        Set(ByVal value As String)
            NoPages = value
        End Set

    End Property

    Dim lsReceivedBy As String
    Public Property pReceivedBy() As String
        Get
            Return lsReceivedBy
        End Get
        Set(ByVal value As String)
            lsReceivedBy = value
        End Set

    End Property

    Dim lsReceivedDate As String
    Public Property pReceivedDate() As String
        Get
            Return lsReceivedDate
        End Get
        Set(ByVal value As String)
            lsReceivedDate = value
        End Set

    End Property

    Dim lsReceivedTime As String
    Public Property pReceivedTime() As String
        Get
            Return lsReceivedTime
        End Get
        Set(ByVal value As String)
            lsReceivedTime = value
        End Set

    End Property

    Dim lsReturnCard As String
    Public Property pReturnCard() As String
        Get
            Return lsReturnCard
        End Get
        Set(ByVal value As String)
            lsReturnCard = value
        End Set

    End Property

    Dim NoCopies As String = ""

    Public Property pNoCopies() As String
        Get
            Return NoCopies
        End Get
        Set(ByVal value As String)
            NoCopies = value
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

    Public Property pModifiedDate() As String
        Get
            Return ModifiedDate
        End Get
        Set(ByVal value As String)
            ModifiedDate = value
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

    Public Property pRetVal() As String
        Get
            Return RetVal
        End Get
        Set(ByVal value As String)
            RetVal = value
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

    Public Property pDocStatus() As String
        Get
            Return DocStatus
        End Get
        Set(ByVal value As String)
            DocStatus = value
        End Set

    End Property

    Public Property pDocStatusId() As String
        Get
            Return DocStatusId
        End Get
        Set(ByVal value As String)
            DocStatusId = value
        End Set

    End Property
    Public Property pFinalDocStatus() As String
        Get
            Return FinalDocStatus
        End Get
        Set(ByVal value As String)
            FinalDocStatus = value
        End Set

    End Property

    Public Property pCheckout() As Boolean
        Get
            Return Checkout
        End Get
        Set(ByVal value As Boolean)
            Checkout = value
        End Set

    End Property
    Public Property pFolderId() As String
        Get
            Return FolderId
        End Get
        Set(ByVal value As String)
            FolderId = value
        End Set

    End Property
    Public Property pFolderDesc() As String
        Get
            Return FolderDesc
        End Get
        Set(ByVal value As String)
            FolderDesc = value
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

    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
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

    Public Property pFileName() As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
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

    Public Property pVersion() As Integer
        Get
            Return Version
        End Get
        Set(ByVal value As Integer)
            Version = value
        End Set

    End Property

    Public Property pRowCount() As Integer
        Get
            Return RowCount
        End Get
        Set(ByVal value As Integer)
            RowCount = value
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

    Public Property pDocTypeOrig() As String
        Get
            Return DocTypeOrig
        End Get
        Set(ByVal value As String)
            DocTypeOrig = value
        End Set

    End Property

    Dim DateIssued As String
    Public Property pDateIssued() As String
        Get
            Return DateIssued
        End Get
        Set(ByVal value As String)
            RequestType = value
        End Set

    End Property

    Dim RequestType As String
    Public Property pRequestType() As String
        Get
            Return RequestType
        End Get
        Set(ByVal value As String)
            RequestType = value
        End Set

    End Property

    Dim ClassificationCode As String
    Public Property pClassificationCode() As String
        Get
            Return ClassificationCode
        End Get
        Set(ByVal value As String)
            ClassificationCode = value
        End Set

    End Property
    Public Property pDocTitle() As String
        Get
            Return DocTitle
        End Get
        Set(ByVal value As String)
            DocTitle = value
        End Set

    End Property

    Public Property pDocVersion() As String
        Get
            Return DocVersion
        End Get
        Set(ByVal value As String)
            DocVersion = value
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

    Public Sub CheckOutDoc()
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "UPDATE DOCLIST SET IsBeingModified = 1 " &
",modifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",modifiedby = '" & pUserId & "' " & _
"WHERE DocId = '" & pDocId & "'" '"xMSP_DOCLISTCHECKOUT"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)

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

    Public Sub CancelCheckOutDoc()
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "UPDATE DOCLIST SET IsBeingModified = 0 " &
",modifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",modifiedby = '" & pUserId & "' " & _
"WHERE DocId = '" & pDocId & "'" '"xMSP_DOCLISTCHECKOUT"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)

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
    'not used
    'Public Function CountAllDoc() As Integer
    '    Dim objCommand As clsSqlConn

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        Dim lssql As String
    '        Dim DocListApprover As String = "SELECT distinct docid FROM docrouting dr " & _
    '                                " WHERE dr.approverid = '" & DocSession.sUserId & "'"
    '        Dim DocList As String = "SELECT docid FROM docrouting dr " & _
    '                                " WHERE dr.approverid = '" & DocSession.sUserId & "'"

    '        lssql = "SELECT COUNT(dl.docid) FROM DOCLIST dl INNER JOIN " & _
    '                                     "DocType dt ON dl.DocType = dt.DocType " & _
    '                "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
    '                "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
    '                "INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 "
    '        'If DocSession.sUserRole <> "A" Then
    '        '    lssql = lssql & " LEFT JOIN docRouting dr1 " & _
    '        '    " on dr1.docid = dl.docid "
    '        'End If
    '        lssql = lssql & " WHERE dl.statusid > 0 and dl.statusid <> 5 "
    '        If DocSession.sUserRole <> "A" Then
    '            lssql = lssql & " AND (dl.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.CreatedBy = '" & DocSession.sUserId & "' " & _
    '            " OR dl.DocId In (" & DocList & ")) "
    '        Else

    '        End If
    '        lssql = lssql & BuildWhere()



    '        objCommand.CommandText = lssql
    '        Return objCommand.ExecScalar
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '    End Try

    'End Function

    Public Function CountLinkDoc() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String

            s_sql = "SELECT COUNT(*) FROM DOCLIST dl " & _
            "WHERE dl.statusid > 0 and dl.statusid <> 5 "

            s_sql = s_sql & " AND dl.docid <> " & pDocId & " and NOT EXISTS ( SELECT docid FROM doclinks dls WHERE dls.docid = " & pDocId & " AND dls.linkdocid = dl.docid AND dls.deleteddate is null) "

            If pDocTitle <> "" Then
                s_sql = s_sql & " AND dl.title like '%" & pDocTitle & "%' "
            End If

            objCommand.CommandText = s_sql
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

    Public Function CountArchiveDoc() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim lssql As String


            lssql = "SELECT COUNT(*) FROM DOCLIST dl INNER JOIN " & _
                                         "DocType dt ON dl.DocType = dt.DocType " & _
 "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
            "WHERE dl.statusid " & pProcess & " 0 "
            If pProcess = ">" Then
                lssql = lssql & BuildArchiveWhere()
            Else
                lssql = lssql & BuildRestoreWhere()
            End If


            objCommand.CommandText = lssql
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

    'Public Function RetrieveCurrentDocs() As DataTable
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim sOrder As String
    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        sOrder = OrderBy()

    '        Dim lTop As Integer
    '        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1
    '        Dim s_sql As String = "SELECT * FROM ( " & _
    '                                                " SELECT TOP " & lTop.ToString & " " & _
    '                                                        "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
    '                                                        ",dl.DocId " & _
    '                                                        ",dl.DocType " & _
    '                                                        ",dt.DocName " & _
    '                                                        ",dl.Title " & _
    '                                                        ",dl.FileName " & _
    '                                                        ",dl.CreatedDate " & _
    '                                                        ",dl.CreatedBy " & _
    '                                                        ",dl.ModifiedBy " & _
    '                                                        ",dl.ModifiedDate " & _
    '                                                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
    '                                                  "FROM doclist dl "


    '        s_sql = s_sql & "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "'  " & _
    '        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
    '        "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
    '        "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
    '        " WHERE dl.StatusId > 0 "


    '        s_sql = s_sql & BuildArchiveWhere()


    '        s_sql = s_sql & " ) ps " & _
    '        " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

    '        objCommand.CommandText = s_sql
    '        ldata = objCommand.Fill
    '        'If ldata.Rows.Count < CInt(pRowsPerPage) Then
    '        Return ldata
    '        'Else

    '        'End If
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


    Public Function RetrieveArchiveDocs() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            sOrder = OrderBy()

            Dim lTop As Integer
            lTop = CInt(pIdx) * CInt(pRowsPerPage)
            Dim s_sql As String = "SELECT * FROM ( "

            If Not DocSession.OraClient Then
                s_sql = s_sql & " SELECT TOP " & lTop.ToString & " "
            End If

            s_sql = s_sql & "row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") as rn" & _
            ",dl.refno " & _
            ",dl.DocId " & _
            ",dl.DocType " & _
            ",dt.DocName " & _
            ",dl.Title " & _
            ",dl.FileName " & _
            ",dl.CreatedDate " & _
            ",dl.CreatedBy " & _
            ",dl.ModifiedBy " & _
            ",dl.ModifiedDate " & _
            ",dl.FileVersion "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NLV(u.FirstName,'')+' '+NLV(u.LastName,'') AS originator "

            Else
                s_sql = s_sql & ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') "
            End If

            s_sql = s_sql & ",groupaccessid = 5" & _
        "FROM doclist dl "


            s_sql = s_sql & "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
            "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
            "WHERE dl.statusid " & pProcess & " 5 "
            If pProcess = ">" Then
                s_sql = s_sql & BuildArchiveWhere()
            Else
                s_sql = s_sql & BuildRestoreWhere()
            End If

            If DocSession.OraClient Then
                s_sql = s_sql & " and rownum <= " & lTop.ToString
            End If

            s_sql = s_sql & " ) ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            'If ldata.Rows.Count < CInt(pRowsPerPage) Then
            Return ldata
            'Else

            'End If
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

    Public Function CountOwnDoc() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text



            Dim s_sql As String = "SELECT COUNT(di.docid) FROM DocInbox di Inner Join Doclist dl on di.docid = dl.docid " & _
                " and dl.statusid <> 5 "

            If pConfidential <> "" Then
                If pConfidential = "1" Then
                    s_sql = s_sql & " and (dl.Confidential = 1) "
                Else
                    s_sql = s_sql & " and (dl.Confidential is null or dl.Confidential = 0) "
                End If
                pConfidential = ""
            End If

            If pAuthor <> "" Then
                s_sql = s_sql & " INNER JOIN users u ON u.userId = dl.createdby " & _
                                " AND u.FirstName+' '+u.LastName like '%" & pAuthor & "%' "
                pAuthor = ""
            End If

            s_sql = s_sql & " WHERE DI.userid = '" & pUserId & "' "

            If (Not DocSession.sFolderID Is Nothing) AndAlso DocSession.sFolderID.Trim <> "" Then
                s_sql = s_sql & " AND di.FolderId =  '" & DocSession.sFolderID.Trim & "'"
            Else
                s_sql = s_sql & " AND (di.FolderId is null or  di.FolderId='') "
            End If


            s_sql = s_sql & BuildWhere()


            objCommand.CommandText = s_sql

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
    Public Function CountInboxDoc() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text



            'Dim s_sql As String = "SELECT COUNT(*) FROM DocInbox di Inner Join Doclist dl on di.docid = dl.docid WHERE DI.userid = '" & pUserId & "' and dl.StatusId > 0 and dl.statusid <> 5 "
            Dim s_Sql As String = "SELECT count(*) " & _
                                " FROM DOCINBOX dls " & _
                                " INNER JOIN doclist dl1 " & _
                                    "ON dl1.docId = dls.docId " & _
                                    "WHERE dls.UserId = '" & pUserId & "'"



            If (Not DocSession.sFolderID Is Nothing) AndAlso DocSession.sFolderID.Trim <> "" Then
                s_Sql = s_Sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
            Else
                s_Sql = s_Sql & " AND (dls.FolderId is null or  dls.FolderId='') "
            End If

            objCommand.CommandText = s_Sql & BuildWhere()

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
    Public Function CountSentDoc() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "SELECT COUNT(*) FROM DocOutbox do Inner Join Doclist dl on do.docid = dl.docid LEFT JOIN docrouting dr on dr.seqno = do.routingseqno WHERE userid = '" & pUserId & "' and dl.StatusId > 0 and dl.statusid <> 5 "

            '"SELECT COUNT(distinct dr.docid) FROM DOCROUTING dR " & _
            '                        "INNER JOIN doclist dl on dl.docid = dr.docid and dl.StatusId > 0 and dl.StatusId <> 5 " & _
            '                        "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "' and g.GroupAccessId > 0 " & _
            '                        "WHERE sender =  '" & pUserId & "'"

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

    Public Function CountNewDoc() As Integer
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            If DocSession.OraClient Then

                s_sql = "SELECT COUNT(docId) FROM docRouting  " & _
                    " WHERE approverid = '" & pUserId & "' AND (((sysdate-assigneddate)*24)  <= 24) " 'oracle george
            Else

                s_sql = "SELECT COUNT(dr.docId) FROM docRouting dr Inner Join Doclist dl on dr.docid = dl.docid " & _
                " and dl.statusid <> 5 "

                If pConfidential <> "" Then
                    If pConfidential = "1" Then
                        s_sql = s_sql & " and (dl.Confidential = 1) "
                    Else
                        s_sql = s_sql & " and (dl.Confidential is null or dl.Confidential = 0) "
                    End If
                    pConfidential = ""
                End If

                If pAuthor <> "" Then
                    s_sql = s_sql & " INNER JOIN users u ON u.userId = dl.createdby " & _
                                    " AND u.FirstName+' '+u.LastName like '%" & pAuthor & "%' "
                    pAuthor = ""
                End If

                s_sql = s_sql & " WHERE approverid = '" & pUserId & "' AND datediff(hh,assigneddate, getdate()) <= 24 " & BuildWhere()

            End If

            objCommand.CommandText = s_sql
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


    'Public Function RetrieveDocs(ByVal asParam As String) As DataTable
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim sOrder As String
    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        sOrder = OrderBy()

    '        Dim lTop As Integer
    '        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
    '        Dim s_sql As String = " SELECT tbl.* " & _
    '                                    ",(isnull(u2.FirstName,'')+' '+isnull(u2.LastName,'')) as checkoutby " & _
    '                                    ",(case when db.datebookmark is null then 0 else 1 end) as bookmarked" & _
    '                                    ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
    '                                " FROM (" & _
    '                                        "SELECT * FROM ( " & _
    '                                                " SELECT TOP " & lTop.ToString & " " & _
    '                                                        "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
    '                                                        ",dl.DocId " & _
    '                                                        ",refno = isnull(dl.RefNo,'') " & _
    '                                                        ",dl.DocType " & _
    '                                                        ",dt.DocName " & _
    '                                                        ",dl.Title " & _
    '                                                        ",FileName = isnull(dl.FileName,'') " & _
    '                                                        ",ISNULL(O.ColValue,'') as Agency " & _
    '                                                        ",dl.Location " & _
    '                                                        ",dl.CreatedDate " & _
    '                                                        ",dl.CreatedBy " & _
    '                                                        ",dl.ModifiedBy " & _
    '                                                        ",dl.ModifiedDate " & _
    '                                                        ",dl.IsBeingModified " & _
    '                                                        ",dl.StatusId " & _
    '                                                        ",dl.RoutingSeqNo " & _
    '                                                        ",dl.FileVersion " & _
    '                                                        ",statusdesc = ds.description " & _
    '                                                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
    '                                                        ",g.GroupAccessId " & _
    '                                                        ",CanPrint=isnull(g.CanPrint,0) " & _
    '                                                        ",VersionControl=isnull(g.VersionControl,0) " & _
    '                                                        ",CanDownload=isnull(g.CanDownload,0) " & _
    '                                                        ",CanPrintReceipt=isnull(g.CanPrintReceipt,0) "
    '        If asParam = "Y" Then
    '            s_sql = s_sql & ",d_l.urgent,d_l.doc_id "
    '        ElseIf asParam = "S" OrElse asParam = "N" Then
    '            s_sql = s_sql & ",dr.urgent,dr.doc_id "
    '        Else
    '            s_sql = s_sql & ",urgent = 0,doc_id = -1"
    '        End If


    '        s_sql = s_sql & "FROM doclist dl "
    '        If asParam = "Y" Then 'yours inbox
    '            's_sql = s_sql & " INNER JOIN ( SELECT dls.docid FROM DOCLIST dls " & _
    '            '                        "WHERE dls.StatusId > 0 and dls.StatusId <> 5  and dls.createdby = '" & pUserId & "' " & _
    '            '                        "UNION " & _
    '            '                        "SELECT drs.docid FROM DOCROUTING drs " & _
    '            '                        "WHERE drs.approverid = '" & pUserId & "' and (drs.statusid > 2 or drs.statusid = -2)) d_l ON d_l.docid = dl.docId  "

    '            s_sql = s_sql & " INNER JOIN ( SELECT dls.docid,urgent= isnull(dr.urgent,0),doc_id = case when dr2.seqno is null then -1 else case when dr2.seqno is not null and dl1.statusid <> 5 and dl1.statusid <> 12 and dl1.statusid <> 8 then 1 else -1 end end FROM DOCINBOX dls " & _
    '                                " INNER JOIN doclist dl1 " & _
    '                                    "ON dl1.docId = dls.docId " & _
    '                                " LEFT JOIN docrouting dr " & _
    '                                    "ON dls.routingseqno = dr.seqno " & _
    '                                    " LEFT JOIN docrouting dr2 " & _
    '                                    "ON dl1.RoutingSeqNo = dr2.seqno " & _
    '                                    " and dr2.approverid = '" & pUserId & "' " & _
    '                                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
    '                                    "WHERE dls.UserId = '" & pUserId & "'"

    '            '01/17/2014
    '            'If Not DocSession.sFolderID Is Nothing Then
    '            If Not DocSession.sFolderID Is Nothing AndAlso DocSession.sFolderID.Trim <> "" Then
    '                s_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
    '            Else
    '                s_sql = s_sql & " AND (dls.FolderId is null or  dls.FolderId='') "
    '            End If
    '            'End If

    '            s_sql = s_sql & ") d_l ON d_l.docid = dl.docId  "

    '        ElseIf asParam = "S" Then 'sent
    '            's_sql = s_sql & " INNER JOIN (SELECT distinct docid FROM DOCROUTING WHERE sender = '" & pUserId & "') dr " & _
    '            '                        " ON dr.docid = dl.docId  "
    '            s_sql = s_sql & " INNER JOIN (SELECT dls.docid,urgent= isnull(dcr.urgent,0),doc_id = -1 FROM DOCOUTBOX dls " & _
    '                                " LEFT JOIN docrouting dcr " & _
    '                                    "ON dls.routingseqno = dcr.seqno " & _
    '                                " WHERE dls.UserId = '" & pUserId & "'"
    '            '01/17/2014
    '            'If Not DocSession.sFolderID Is Nothing Then
    '            'If DocSession.sFolderID.Trim <> "" Then
    '            's_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
    '            'End If
    '            'End If

    '            s_sql = s_sql & ") dr ON dr.docid = dl.docId  "

    '        ElseIf asParam = "N" Then 'new
    '            's_sql = s_sql & " INNER JOIN (SELECT distinct docid FROM DOCROUTING WHERE sender = '" & pUserId & "') dr " & _
    '            '                        " ON dr.docid = dl.docId  "

    '            If DocSession.OraClient Then
    '                's_sql = s_sql & " LEFT JOIN docInbox dr " & _
    '                '                    " ON dr.docId = dl.docId " & _
    '                '                    " AND dr.approverid = '" & pUserId & "' AND TO_DATE(dr.assigneddate,'mm/dd/yyyy') = TO_DATE(getdate(),'mm/dd/yyyy') "
    '                s_sql = s_sql & " INNER JOIN (" & _
    '                    "SELECT dls.docid,urgent= isnull(dr.urgent,0),doc_id = case when dr2.seqno is null then -1 else case when dr2.seqno is not null and dl1.statusid <> 5 and dl1.statusid <> 12 and dl1.statusid <> 8 then 1 else -1 end end FROM (" & _
    '                " SELECT dl.docId FROM DOCLIST dl " & _
    '                                    " INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "' and g.GroupAccessId > 0  "
    '                s_sql = s_sql & " WHERE (TO_DATE(dl.createddate,'mm/dd/yyyy') = TO_DATE(CURRENT_DATE,'mm/dd/yyyy')  and dl.StatusId <> 5 " & _
    '                    " AND dl.StatusId > 0 and dl.createdby = '" & pUserId & "') " & _
    '                    " union " & _
    '                "SELECT docId FROM docRouting  " & _
    '                                        " WHERE approverid = '" & pUserId & "' AND statusid <> 2 AND TO_DATE(assigneddate,'mm/dd/yyyy') = TO_DATE(getdate(),'mm/dd/yyyy') " & _
    '                                        " ) dls " & _
    '                            " INNER JOIN doclist dl1 ON dls.docid = dl1.docId " & _
    '                            " LEFT JOIN docrouting dr ON dr.seqno = dl1.routingSeqNo " & _
    '                            " LEFT JOIN docrouting dr2 ON dl1.RoutingSeqNo = dr2.SeqNo " & _
    '                            " and dr2.approverid = '" & pUserId & "' " & _
    '                                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) "
    '                '01/17/2014
    '                If (Not DocSession.sFolderID Is Nothing) AndAlso DocSession.sFolderID.Trim <> "" Then
    '                    s_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
    '                Else
    '                    s_sql = s_sql & " AND (dls.FolderId is null or dls.FolderId = '')"
    '                End If


    '                s_sql = s_sql & ") dr ON dr.docid = dl.docId  "
    '                '"SELECT docId FROM docInbox  " & _
    '                '                        " WHERE userid = '" & pUserId & "' AND TO_DATE(senddate,'mm/dd/yyyy') = TO_DATE(getdate(),'mm/dd/yyyy') " & _
    '                '                        " ) dls " & _
    '                '            " INNER JOIN doclist dl1 ON dls.docid = dl1.docId " & _
    '                '            " LEFT JOIN docrouting dr ON dr.seqno = dl1.routingSeqNo " & _
    '                '            " LEFT JOIN docrouting dr2 ON dl1.RoutingSeqNo = dr2.SeqNo " & _
    '                '            " and dr2.approverid = '" & pUserId & "' " & _
    '                '                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
    '                '            ") dr ON dr.docId = dl.docId "
    '            Else
    '                s_sql = s_sql & " INNER JOIN (" & _
    '                    "SELECT dls.docid,urgent= isnull(dr.urgent,0),doc_id = case when dr2.seqno is null then -1 else case when dr2.seqno is not null and dl1.statusid <> 5 and dl1.statusid <> 12 and dl1.statusid <> 8 then 1 else -1 end end FROM (" & _
    '                                    "SELECT docId FROM docRouting  " & _
    '                " WHERE approverid = '" & pUserId & "'  AND datediff(hh,assigneddate, getdate()) <= 24 " & _
    '                                        " ) dls " & _
    '                            " INNER JOIN doclist dl1 ON dls.docid = dl1.docId " & _
    '                            " LEFT JOIN docrouting dr ON dr.seqno = dl1.routingSeqNo " & _
    '                            " LEFT JOIN docrouting dr2 ON dl1.RoutingSeqNo = dr2.SeqNo " & _
    '                            " and dr2.approverid = '" & pUserId & "' " & _
    '                                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
    '                            ") dr ON dr.docId = dl.docId "

    '                's_sql = s_sql & "INNER JOIN (SELECT docid FROM (SELECT dl.docid FROM DOCLIST dl " & _
    '                '                    " INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "' and g.GroupAccessId > 0 " & _
    '                '                    " WHERE (convert(char(10),dl.createddate,101) = convert(char(10),getdate(),101) and dl.StatusId <> 5 " & _
    '                '                    " AND dl.StatusId > 0 and dl.createdby = '" & pUserId & "') " & _
    '                '        " union " & _
    '                '        "SELECT docId FROM docInbox  " & _
    '                '        " WHERE userid = '" & pUserId & "' AND convert(char(10),senddate,101) = convert(char(10),getdate(),101) ) dlst ) dr ON dr.docId = dl.docId "
    '            End If
    '        End If

    '        s_sql = s_sql & "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "'  " & _
    '        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
    '        "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
    '        "LEFT JOIN (" & _
    '                "select div.DocId,div.DocType,div.ColValue from docindex di INNER JOIN docindexvalues div ON  " & _
    '        "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
    '                "where di.ColumnName = 'Agency' " & _
    '        ") " & _
    '        " O ON O.docid=dl.docId " & _
    '        "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
    '        " WHERE dl.StatusId > 0 and dl.statusid <> 5 " '& IIf(DocSession.sUserRole <> "A", " and dl.officeCode = '" & DocSession.sOfcCode & "' ", "")



    '        'If asParam = "N" Then
    '        '    If DocSession.OraClient Then
    '        '        s_sql = s_sql & " AND ((TO_DATE(dl.createddate,'mm/dd/yyyy') = TO_DATE(getdate(),'mm/dd/yyyy') and dl.StatusId <> 5 " & _
    '        '            " AND dl.createdby = '" & pUserId & "'))"
    '        '        '" AND dl.officeCode = '" & DocSession.sOfcCode & "' "

    '        '    Else
    '        '        s_sql = s_sql & " AND ((convert(char(10),dl.createddate,101) = convert(char(10),getdate(),101) and dl.StatusId <> 5 " & _
    '        '            " AND dl.createdby = '" & pUserId & "')  )"
    '        '        '" AND dl.officeCode = '" & DocSession.sOfcCode & "' "

    '        '    End If
    '        'Else
    '        If asParam = "A" Then
    '            s_sql = s_sql & BuildWhere()
    '        Else
    '            s_sql = s_sql & BuildClassificationWhere()
    '        End If

    '        s_sql = s_sql & " ORDER BY " & _
    '               sOrder & " " & pSortOrder & _
    '        " ) ps " & _
    '        " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & ") tbl " & _
    '        "LEFT JOIN Users U2 ON U2.userId = tbl.ModifiedBy " & _
    '        "LEFT JOIN DocBookMark db ON tbl.docId = db.docId and db.userid = '" & pUserId & "'" & _
    '        "LEFT JOIN docRouting dr ON	" & _
    '            "dr.docid = tbl.docid and dr.seqno = tbl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
    '            "LEFT JOIN Users U3 ON U3.userId = dr.approverid "
    '        objCommand.CommandText = s_sql
    '        ldata = objCommand.Fill
    '        'If ldata.Rows.Count < CInt(pRowsPerPage) Then
    '        Return ldata
    '        'Else

    '        'End If
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
    'not used
    'Public Function RetrieveAllDoc(ByVal asParam As String) As DataTable
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim sOrder As String
    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        sOrder = OrderBy()
    '        Dim DocList As String = "SELECT distinct docid FROM docrouting dr " & _
    '                                " WHERE dr.approverid = '" & DocSession.sUserId & "'"


    '        Dim lTop As Integer
    '        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
    '        Dim s_sql As String = "SELECT * FROM ( " & _
    '                                                " SELECT TOP " & lTop.ToString & " " & _
    '                                                        "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
    '                                                        ",dl.DocId " & _
    '                                                        ",refno = isnull(dl.RefNo,'') " & _
    '                                                        ",dl.DocType " & _
    '                                                        ",dl.Title " & _
    '                                                        ",FileName = isnull(dl.FileName,'') " & _
    '                                                        ",dl.CreatedDate " & _
    '                                                        ",dl.CreatedBy " & _
    '                                                        ",dl.ModifiedBy " & _
    '                                                        ",dl.StatusId " & _
    '                                                        ",dl.RoutingSeqNo " & _
    '                                                        ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
    '                                                        ",dl.FileVersion " & _
    '                                                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
    '                                                        ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
    '                                                        ",dt.DocName " & _
    '                                                        ",Office = isnull(ug.OfficeCode,'') " & _
    '                                                        ",statusdesc = ds.description " & _
    '                                                        ",g.GroupAccessId "

    '        s_sql = s_sql & ",urgent = 0,doc_id = -1"


    '        s_sql = s_sql & "FROM docList dl "


    '        s_sql = s_sql & "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "'  " & _
    '        "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
    '        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
    '        "INNER JOIN DocStatus ds ON dl.statusid=ds.statusid " & _
    '        "LEFT JOIN docRouting dr ON	" & _
    '        "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
    '        "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
    '        "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId "

    '        'If DocSession.sUserRole <> "A" Then
    '        '    s_sql = s_sql & " LEFT JOIN docRouting dr1 " & _
    '        '    " on dr1.docid = dl.docid "
    '        'End If
    '        s_sql = s_sql & " WHERE dl.statusid > 0 and dl.statusid <> 5 "
    '        If DocSession.sUserRole <> "A" Then
    '            s_sql = s_sql & " AND (dl.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    '            " OR dl.CreatedBy = '" & DocSession.sUserId & "' " & _
    '            " OR dl.DocId in (" & DocList & ")) "
    '        Else

    '        End If

    '        s_sql = s_sql & BuildWhere()

    '        s_sql = s_sql & " ) ps " & _
    '        " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY ps.rn "


    '        objCommand.CommandText = s_sql
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
    'ok
    Public Function RetrieveNew(ByVal asParam As String) As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            sOrder = OrderBy()

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
            'Dim s_sql As String = "SELECT * FROM ( " & _
            '                                        " SELECT TOP " & lTop.ToString & " " & _
            '                                                "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
            '                                                ",dl.DocId " & _
            '                                                ",refno = isnull(dl.RefNo,'') " & _
            '                                                ",dl.DocType " & _
            '                                                ",dt.DocName " & _
            '                                                ",dl.Title " & _
            '                                                ",FileName = isnull(dl.FileName,'') " & _
            '                                                ",dl.CreatedDate " & _
            '                                                ",dl.CreatedBy " & _
            '                                                ",dl.ModifiedBy " & _
            '                                                ",dl.StatusId " & _
            '                                                ",dl.RoutingSeqNo " & _
            '                                                ",dl.FileVersion " & _
            '                                                ",statusdesc = ds.description " & _
            '                                                ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
            '                                                ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
            '                                                ",Office = isnull(ug.OfficeCode,'') " & _
            '                                                ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
            '                                                ",g.GroupAccessId "

            's_sql = s_sql & ",dr.urgent,dr.doc_id "


            's_sql = s_sql & "FROM doclist dl "

            'If DocSession.OraClient Then
            'Else
            Dim s_sql As String = " SELECT * FROM (" & _
                "SELECT TOP " & lTop.ToString & " " & _
                    "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & "), " & _
                    "refno = isnull(dl.RefNo,''), " & _
                    "assignedto = '" & DocSession.sUserName & "'," & _
                    "dt.DocName, " & _
                    "statusdesc = ds.description, " & _
                    "dl.StatusId, " & _
                    "dl.CreatedDate, " & _
                    "dl.docid, " & _
                    "dl.doctype, " & _
                    "dl.routingseqno, " & _
                    "dl.FileVersion, " & _
                    "dl.filename," & _
                    "dl.title," & _
                    "GroupAccessId = 5, " & _
                    "FirstApprover= isnull(dl.FirstApprover,''),  " & _
                    "urgent= isnull(dls.urgent,0), " & _
                    "originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,''), " & _
                    "doc_id = case when dl.routingseqno is null then -1 " & _
                                "else " & _
                                    "case when dl.routingseqno is not null and dl.statusid <> 5 and dl.statusid <> 12 and dl.statusid <> 8 then 1 " & _
                                          "else -1 end " & _
                                "end " & _
                    " FROM doclist dl " & _
                    " INNER JOIN (" & _
                                "SELECT * FROM ( " & _
                                "SELECT dr.docId,dr.seqno,dr.urgent," & _
                                "rn=row_number() over (partition by dr.docid order by dr.docid,dr.seqno desc) " & _
                                "FROM docRouting dr  " & _
                                " WHERE dr.approverid = '" & pUserId & "' AND datediff(hh,dr.assigneddate, getdate()) <= 24 " & _
                                ") drp WHERE drp.rn = 1 " & _
                            " ) dls ON dls.docid = dl.docId " & _
                        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
                        "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
                        "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
                        "WHERE dl.StatusId > 0 and dl.statusid <> 5 "


            s_sql = s_sql & BuildWhere()

            s_sql = s_sql & _
            " ) ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY ps.rn "

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            'If ldata.Rows.Count < CInt(pRowsPerPage) Then
            Return ldata
            'Else

            'End If
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
    'ok
    'Public Function RetrieveSent(ByVal asParam As String) As DataTable
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim sOrder As String
    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        sOrder = OrderBy()

    '        Dim lTop As Integer
    '        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
    '        Dim s_sql As String = " SELECT * FROM ( " & _
    '                                                " SELECT TOP " & lTop.ToString & " " & _
    '                                                        "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
    '                                                        ",dl.DocId " & _
    '                                                        ",refno = isnull(dl.RefNo,'') " & _
    '                                                        ",dl.DocType " & _
    '                                                        ",dt.DocName " & _
    '                                                        ",dl.Title " & _
    '                                                        ",FileName = isnull(dl.FileName,'') " & _
    '                                                        ",dl.CreatedDate " & _
    '                                                        ",dl.CreatedBy " & _
    '                                                        ",dl.ModifiedBy " & _
    '                                                        ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
    '                                                        ",FirstApprover = isnull(dl.FirstApprover,'') " & _
    '                                                        ",dl.StatusId " & _
    '                                                        ",dl.RoutingSeqNo " & _
    '                                                        ",Office = isnull(ug.OfficeCode,'') " & _
    '                                                        ",dl.FileVersion " & _
    '                                                        ",statusdesc = ds.description " & _
    '                                                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') "


    '        s_sql = s_sql & ",drt.urgent,drt.doc_id "


    '        s_sql = s_sql & "FROM doclist dl "

    '        s_sql = s_sql & " INNER JOIN (SELECT dls.docid,urgent= isnull(dcr.urgent,0),doc_id = -1 FROM DOCOUTBOX dls " & _
    '                                " LEFT JOIN docrouting dcr " & _
    '                                    "ON dls.routingseqno = dcr.seqno " & _
    '                                " WHERE dls.UserId = '" & pUserId & "'"

    '        s_sql = s_sql & ") drt ON drt.docid = dl.docId  "


    '        s_sql = s_sql & "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
    '        "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
    '        "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
    '        "LEFT JOIN docRouting dr ON	" & _
    '            "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
    '            "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
    '        "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId " & _
    '        " WHERE dl.statusid <> 5 " '& IIf(DocSession.sUserRole <> "A", " and dl.officeCode = '" & DocSession.sOfcCode & "' ", "")
    '        s_sql = s_sql & BuildWhere() & _
    '        " ) ps " & _
    '        " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " order by ps.rn "

    '        objCommand.CommandText = s_sql
    '        ldata = objCommand.Fill
    '        'If ldata.Rows.Count < CInt(pRowsPerPage) Then
    '        Return ldata
    '        'Else

    '        'End If
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

    'ok
    'Public Function RetrieveInbox(ByVal asParam As String) As DataTable
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim sOrder As String
    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text
    '        sOrder = OrderBy()

    '        Dim lTop As Integer
    '        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
    '        Dim s_sql As String = " SELECT * FROM ( " & _
    '                                                " SELECT TOP " & lTop.ToString & " " & _
    '                                                        "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
    '                                                        ",dl.DocId " & _
    '                                                        ",refno = isnull(dl.RefNo,'') " & _
    '                                                        ",dl.DocType " & _
    '                                                        ",dt.DocName " & _
    '                                                        ",dl.Title " & _
    '                                                        ",FileName = isnull(dl.FileName,'') " & _
    '                                                        ",dl.CreatedDate " & _
    '                                                        ",dl.CreatedBy " & _
    '                                                        ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
    '                                                        ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
    '                                                        ",Office = isnull(ug.OfficeCode,'') " & _
    '                                                        ",dl.ModifiedBy " & _
    '                                                        ",dl.StatusId " & _
    '                                                        ",dl.RoutingSeqNo " & _
    '                                                        ",dl.FileVersion " & _
    '                                                        ",statusdesc = ds.description " & _
    '                                                        ",d_l.originator " & _
    '                                                        ",d_l.GroupAccessId "


    '        's_sql = s_sql & ",d_l.urgent,d_l.doc_id "
    '        s_sql = s_sql & ",urgent= isnull(dr.urgent,0),doc_id = case when dr2.seqno is null then -1 else " & _
    '                            " case when dr2.seqno is not null and dl.statusid <> 5 and dl.statusid <> 12 and dl.statusid <> 8 then 1 else -1 end " & _
    '                            "end "

    '        s_sql = s_sql & "FROM doclist dl "


    '        s_sql = s_sql & " INNER JOIN ( SELECT dls.docid " & _
    '                            ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,''),g.GroupAccessId " & _
    '                            " FROM DOCINBOX dls " & _
    '                            " INNER JOIN doclist dl " & _
    '                                "ON dl.docId = dls.docId "

    '        s_sql = s_sql & "INNER JOIN users u On u.userid = dl.createdby "


    '        s_sql = s_sql & "WHERE dl.statusid > 0 and dl.statusid <> 5 and dls.UserId = '" & pUserId & "'"


    '        If Not DocSession.sFolderID Is Nothing AndAlso DocSession.sFolderID.Trim <> "" Then
    '            s_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
    '        Else
    '            s_sql = s_sql & " AND (dls.FolderId is null or  dls.FolderId='') "
    '        End If

    '        s_sql = s_sql & BuildWhere()


    '        s_sql = s_sql & ") d_l ON d_l.docid = dl.docId  "


    '        s_sql = s_sql & "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
    '        "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid "
    '        s_sql = s_sql & " LEFT JOIN docrouting dr " & _
    '                                "ON dr.docid = dl.docid and dl.routingseqno = dr.seqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
    '                                " LEFT JOIN docrouting dr2 " & _
    '                                "ON dl.RoutingSeqNo = dr2.seqno " & _
    '                                " and dr2.approverid = '" & pUserId & "' " & _
    '                                " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
    '            "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
    '            "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId "


    '        s_sql = s_sql & " ) ps " & _
    '        " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY ps.rn "


    '        objCommand.CommandText = s_sql
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

    Public Function RetrieveOwnDoc() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            sOrder = OrderBy()

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
            Dim s_sql As String = " SELECT * FROM ( " & _
                                                    " SELECT TOP " & lTop.ToString & " " & _
                                                            "rn= row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") " & _
                                                            ",dl.DocId " & _
                                                            ",dl.RefNo " & _
                                                            ",dl.DocType " & _
                                                            ",dt.DocName " & _
                                                            ",dl.Title " & _
                                                            ",FileName = isnull(dl.FileName,'') " & _
                                                            ",dl.CreatedDate " & _
                                                            ",dl.CreatedBy " & _
                                                            ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
                                                            ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
                                                            ",Office = isnull(ug.OfficeCode,'') " & _
                                                            ",dl.ModifiedBy " & _
                                                            ",dl.StatusId " & _
                                                            ",dl.RoutingSeqNo " & _
                                                            ",dl.FileVersion " & _
                                                            ",statusdesc = ds.description " & _
                                                            ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') "



            's_sql = s_sql & ",d_l.urgent,d_l.doc_id "
            s_sql = s_sql & ",urgent= isnull(dr.urgent,0),doc_id = case when dr2.seqno is null then -1 else " & _
                                " case when dr2.seqno is not null and dl.statusid <> 5  " & _
                                " and dl.statusid not in (" & DocSession.CompleteStatus & ")  then 1 else -1 end " & _
                                "end "

            s_sql = s_sql & "FROM doclist dl "


            s_sql = s_sql & " INNER JOIN ( SELECT dls.docid " & _
                                " FROM DOCINBOX dls " & _
                                " INNER JOIN doclist dl " & _
                                    "ON dl.docId = dls.docId and dl.statusid <> 5 "

            If pConfidential <> "" Then
                If pConfidential = "1" Then
                    s_sql = s_sql & " and (dl.Confidential = 1) "
                Else
                    s_sql = s_sql & " and (dl.Confidential is null or dl.Confidential = 0) "
                End If
                pConfidential = ""
            End If

            If pAuthor <> "" Then
                s_sql = s_sql & "INNER JOIN users u On u.userid = dl.createdby " & _
                                " AND u.FirstName+' '+u.LastName like '%" & pAuthor & "%' "
                pAuthor = ""
            End If

            s_sql = s_sql & "WHERE dls.UserId = '" & pUserId & "'"


            If Not DocSession.sFolderID Is Nothing AndAlso DocSession.sFolderID.Trim <> "" Then
                s_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
            Else
                s_sql = s_sql & " AND (dls.FolderId is null or  dls.FolderId='') "
            End If

            s_sql = s_sql & BuildWhere()


            s_sql = s_sql & ") d_l ON d_l.docid = dl.docId  "

            s_sql = s_sql & "INNER JOIN users u On u.userid = dl.createdby "
            s_sql = s_sql & "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
            "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid "
            s_sql = s_sql & " LEFT JOIN docrouting dr " & _
                                    "ON dr.docid = dl.docid and dl.routingseqno = dr.seqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                                    " LEFT JOIN docrouting dr2 " & _
                                    "ON dl.RoutingSeqNo = dr2.seqno " & _
                                    " and dr2.approverid = '" & pUserId & "' " & _
                                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
                "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
                "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId "


            s_sql = s_sql & " ) ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY ps.rn "


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
    'not used
    Public Function RetrieveDocsOra(ByVal asParam As String) As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            sOrder = OrderBy()

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
            Dim s_sql As String = " SELECT tbl.* " & _
                                        ",(NVL(u2.FirstName,' ')||' '||NVL(u2.LastName,' ')) as checkoutby " & _
                                        ",(case when db.datebookmark is null then 0 else 1 end) as bookmarked" & _
                                        ",(NVL(u3.FirstName,'')||' '||NVL(u3.LastName,'')) AS assignedto " & _
                                    " FROM (" & _
                                            "SELECT * FROM ( " & _
                                                    " SELECT row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") as rn " & _
                                                            ",dl.DocId " & _
                                                            ",NVL(dl.RefNo,' ') AS refno " & _
                                                            ",dl.DocType " & _
                                                            ",dt.DocName " & _
                                                            ",dl.Title " & _
                                                            ",NVL(dl.FileName,' ') AS FileName " & _
                                                            ",NVL(O.ColValue,' ') as Agency " & _
                                                            ",dl.Location " & _
                                                            ",dl.CreatedDate " & _
                                                            ",dl.CreatedBy " & _
                                                            ",dl.ModifiedBy " & _
                                                            ",dl.ModifiedDate " & _
                                                            ",dl.IsBeingModified " & _
                                                            ",dl.StatusId " & _
                                                            ",dl.RoutingSeqNo " & _
                                                            ",dl.FileVersion " & _
                                                            ",ds.description AS statusdesc" & _
                                                            ",NVL(u.FirstName,' ')||' '||NVL(u.LastName,' ') AS originator " & _
                                                            ",g.GroupAccessId " & _
                                                            ",NVL(g.CanPrint,0) AS CanPrint" & _
                                                            ",NVL(g.VersionControl,0) AS VersionControl " & _
                                                            ",NVL(g.CanDownload,0) AS CanDownload" & _
                                                            ",NVL(g.CanPrintReceipt,0) AS CanPrintReceipt"
            If asParam = "Y" Then
                s_sql = s_sql & ",d_l.urgent,d_l.doc_id "
            ElseIf asParam = "S" OrElse asParam = "N" Then
                s_sql = s_sql & ",dr.urgent,dr.doc_id "
            Else
                s_sql = s_sql & ",0 AS urgent,-1 AS doc_id "
            End If


            s_sql = s_sql & "FROM doclist dl "
            If asParam = "Y" Then 'yours inbox


                s_sql = s_sql & " INNER JOIN ( SELECT dls.docid,NVL(dr.urgent,0) AS urgent,case when dr2.seqno is null then -1 else case when dr2.seqno is not null and dl1.statusid <> 5 and dl1.statusid <> 12 and dl1.statusid <> 8 then 1 else -1 end end AS doc_id FROM DOCINBOX dls " & _
                                    " INNER JOIN doclist dl1 " & _
                                        "ON dl1.docId = dls.docId " & _
                                    " LEFT JOIN docrouting dr " & _
                                        "ON dls.routingseqno = dr.seqno " & _
                                        " LEFT JOIN docrouting dr2 " & _
                                        "ON dl1.RoutingSeqNo = dr2.seqno " & _
                                        " and dr2.approverid = '" & pUserId & "' " & _
                                        " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
                                        "WHERE dls.UserId = '" & pUserId & "'"

                '01/17/2014
                'If Not DocSession.sFolderID Is Nothing Then
                If Not DocSession.sFolderID Is Nothing AndAlso DocSession.sFolderID.Trim <> "" Then
                    s_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
                Else
                    s_sql = s_sql & " AND (dls.FolderId is null or  dls.FolderId='') "
                End If
                'End If

                s_sql = s_sql & ") d_l ON d_l.docid = dl.docId  "

            ElseIf asParam = "S" Then 'sent
                's_sql = s_sql & " INNER JOIN (SELECT distinct docid FROM DOCROUTING WHERE sender = '" & pUserId & "') dr " & _
                '                        " ON dr.docid = dl.docId  "
                s_sql = s_sql & " INNER JOIN (SELECT dls.docid,NVL(dcr.urgent,0) AS urgent,-1 AS doc_id FROM DOCOUTBOX dls " & _
                                    " LEFT JOIN docrouting dcr " & _
                                        "ON dls.routingseqno = dcr.seqno " & _
                                    " WHERE dls.UserId = '" & pUserId & "'"
                '01/17/2014
                'If Not DocSession.sFolderID Is Nothing Then
                'If DocSession.sFolderID.Trim <> "" Then
                's_sql = s_sql & " AND dls.FolderId =  '" & DocSession.sFolderID.Trim & "'"
                'End If
                'End If

                s_sql = s_sql & ") dr ON dr.docid = dl.docId  "

            ElseIf asParam = "N" Then 'new
                'oracle george
                s_sql = s_sql & " INNER JOIN (" & _
                    "SELECT dls.docid,NVL(dr.urgent,0) AS urgent,case when dr2.seqno is null then -1 else case when dr2.seqno is not null and dl1.statusid <> 5 and dl1.statusid <> 12 and dl1.statusid <> 8 then 1 else -1 end end AS doc_id FROM (" & _
                                    "SELECT docId FROM docRouting  " & _
                " WHERE approverid = '" & pUserId & "' AND (((sysdate-assigneddate)*24)  <= 24) " & _
                                        " ) dls " & _
                            " INNER JOIN doclist dl1 ON dls.docid = dl1.docId " & _
                            " LEFT JOIN docrouting dr ON dr.seqno = dl1.routingSeqNo " & _
                            " LEFT JOIN docrouting dr2 ON dl1.RoutingSeqNo = dr2.SeqNo " & _
                            " and dr2.approverid = '" & pUserId & "' " & _
                                    " and (dr2.CarbonCopy is null or dr2.CarbonCopy = 0) " & _
                            ") dr ON dr.docId = dl.docId "


            End If

            s_sql = s_sql & "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "'  " & _
            "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
            "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
            "LEFT JOIN (" & _
                    "select div.DocId,div.DocType,div.ColValue from docindex di INNER JOIN docindexvalues div ON  " & _
            "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                    "where di.ColumnName = 'Agency' " & _
            ") " & _
            " O ON O.docid=dl.docId " & _
            "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
            " WHERE rownum <= " & lTop.ToString & " and " & _
                " dl.StatusId > 0 and dl.statusid <> 5 " '& IIf(DocSession.sUserRole <> "A", " and dl.officeCode = '" & DocSession.sOfcCode & "' ", "")




            If asParam = "A" Then
                s_sql = s_sql & BuildWhere()
            Else
                s_sql = s_sql & BuildClassificationWhere()
            End If

            s_sql = s_sql & " ORDER BY " & _
                   sOrder & " " & pSortOrder & _
            " ) ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & ") tbl " & _
            "LEFT JOIN Users U2 ON U2.userId = tbl.ModifiedBy " & _
            "LEFT JOIN DocBookMark db ON tbl.docId = db.docId and db.userid = '" & pUserId & "'" & _
            "LEFT JOIN docRouting dr ON	" & _
                "dr.docid = tbl.docid and dr.seqno = tbl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                "LEFT JOIN Users U3 ON U3.userId = dr.approverid "
            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            'If ldata.Rows.Count < CInt(pRowsPerPage) Then
            Return ldata
            'Else

            'End If
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

    'ok
    Public Function RetrieveDocLinks() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            sOrder = OrderBy()

            Dim lTop As Integer
            lTop = CInt(pIdx) * CInt(pRowsPerPage)
            Dim s_sql As String = "SELECT * FROM ( " & _
                                                    " SELECT "
            If Not DocSession.OraClient Then
                s_sql = s_sql & "TOP " & lTop.ToString & " "
            End If

            s_sql = s_sql & "row_number() over (ORDER BY dl.title asc) as rn" &
                                                            ",dl.DocId " &
                                                            ",dl.DocType " &
                                                            ",dl.Title " &
                                                            ",dl.Refno " &
                                                            ",dl.CreatedDate " &
                                                            "," & IIf(DocSession.OraClient, "NVL", "isnull") & "(dl.FileName,'') as Filename" &
                                                            ",dl.FileVersion " &
                                                            ",isnull(dl.IsLocal,0) as IsLocal " &
                                                      "FROM doclist dl "


            's_sql = s_sql & "INNER JOIN GroupDocAccess G ON dl.doctype = g.docid and g.groupId = '" & pGroupId & "'  " & _
            s_sql = s_sql & " WHERE dl.StatusId > 0 and dl.statusid <> 5  "

            If DocSession.OraClient Then
                s_sql = s_sql & " and rownum <= " & lTop.ToString & " "
            End If

            s_sql = s_sql & " AND dl.docid <> " & pDocId & " and NOT EXISTS ( SELECT docid FROM doclinks dls WHERE dls.docid = " & pDocId & " AND dls.linkdocid = dl.docid ) "

            If pDocTitle <> "" Then
                s_sql = s_sql & " AND (dl.title like '%" & pDocTitle & "%' or dl.refno like '%" & pDocTitle & "%') "
            End If

            s_sql = s_sql & " ) ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx


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
    'ok
    Public Sub AddToInbox(ByVal objCommand As clsSqlConn) ', ByVal aExistsInInbox As Boolean)


        Try
            objCommand.CommandType = CommandType.Text

            'oracle george - rowcount
            If DocSession.OraClient Then
                objCommand.CommandText = "BEGIN UPDATE DocInbox SET SendDate=SYSDATE,SendBy='" & DocSession.sUserId & "',RoutingSeqNo=" & pSeqNo & " WHERE  " & _
                                        " docId = " & pDocId & " and userId='" & pUserId & "'; " & _
                                        " IF SQL%ROWCOUNT = 0 THEN INSERT INTO DocInbox(docId,userid,SendDate,SendBy,RoutingSeqNo) " & _
                                        "VALUES (" & pDocId & ",'" & pUserId & "',SYSDATE,'" & DocSession.sUserId & "'," & pSeqNo & " ); " & _
                                        " END IF; END; "






            Else
                objCommand.CommandText = "UPDATE DocInbox SET SendDate='" & DateTime.Now.ToString & "',SendBy='" & DocSession.sUserId & "',RoutingSeqNo=" & pSeqNo & " WHERE  " & _
                        " docId = " & pDocId & " and userId='" & pUserId & "' " & _
                        " if @@rowcount = 0 " & _
                        "INSERT INTO DocInbox(docId,userid,SendDate,SendBy,RoutingSeqNo) " & _
                        "VALUES (" & pDocId & ",'" & pUserId & "','" & DateTime.Now.ToString & "','" & DocSession.sUserId & "'," & pSeqNo & " ) "

            End If


            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If


        End Try

    End Sub

    Public Sub MoveDocFolder(ByVal objCommand As clsSqlConn) ', ByVal aExistsInInbox As Boolean)


        Try
            objCommand.CommandType = CommandType.Text
            'oracle george
            If DocSession.OraClient Then
                objCommand.CommandText = "BEGIN UPDATE DocInbox SET FolderId='" & pFolderId & "'" & _
                        " WHERE docId = " & pDocId & " and UserId = '" & pUserId & "'; "

                objCommand.CommandText = "IF SQL%ROWCOUNT = 0 THEN INSERT INTO DocInbox(docId,userid,SendDate,SendBy,RoutingSeqNo,FolderId) " & _
                        "VALUES (" & pDocId & ",'" & pUserId & "',SYSDATE,'" & DocSession.sUserId & "'," & pSeqNo & ",'" & pFolderId & "' ); END IF; END; "
            Else
                objCommand.CommandText = "UPDATE DocInbox SET FolderId='" & pFolderId & "'" & _
                        " WHERE docId = " & pDocId & " and UserId = '" & pUserId & "' " & _
                        " if @@rowcount = 0 " & _
                        "INSERT INTO DocInbox(docId,userid,SendDate,SendBy,RoutingSeqNo,FolderId) " & _
                        "VALUES (" & pDocId & ",'" & pUserId & "','" & DateTime.Now & "','" & DocSession.sUserId & "'," & pSeqNo & ",'" & pFolderId & "' ) "

            End If


            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub

    Public Sub DeleteFromInbox(ByVal objCommand As clsSqlConn)


        Try
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM DocInbox WHERE docId = " & pDocId & " and userid  = '" & pUserId & "' "

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            '    If Not objCommand Is Nothing Then
            'objCommand.Dispose()
            'objCommand = Nothing
            'End If


        End Try

    End Sub


    Public Function ExistsInInbox() As Boolean
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT count(*) FROM DocInbox WHERE docId = " & pDocId & " and  userid = '" & pUserId & "' "
            If objCommand.ExecScalar() > 0 Then
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

    Public Sub AddToOutbox(ByVal objCommand As clsSqlConn) ', ByVal aExistsInOutbox As Boolean)


        Try

            'oracle george
            If DocSession.OraClient Then
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "BEGIN UPDATE DocOutbox SET userId=userId WHERE  " & _
                        " docId = " & pDocId & " and userId='" & pUserId & "'; " & _
                " IF SQL%ROWCOUNT = 0 THEN INSERT INTO DocOutbox(docId,SendTo,SendDate,userid,RoutingSeqNo) " & _
                            "VALUES (" & pDocId & ",'" & pUserId & "',SYSDATE,'" & DocSession.sUserId & "'," & pSeqNo & " ); END IF; END; "
                objCommand.ExecTranNonQuery()

            Else
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "UPDATE DocOutbox SET userId=userId WHERE  " & _
                        " docId = " & pDocId & " and userId='" & pUserId & "' " & _
                        " if @@rowcount = 0 " & _
    "INSERT INTO DocOutbox(docId,SendTo,SendDate,userid,RoutingSeqNo) " & _
                            "VALUES (" & pDocId & ",'" & pUserId & "','" & DateTime.Now & "','" & DocSession.sUserId & "'," & pSeqNo & " ) "

                objCommand.ExecTranNonQuery()

            End If

            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub

    Public Sub DeleteFromOutbox(ByVal objCommand As clsSqlConn)


        Try

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "DELETE FROM DocOutbox WHERE docId = " & pDocId & " and userid  = '" & pUserId & "' "

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If


        End Try

    End Sub


    Public Function ExistsInOutbox() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT count(docid) FROM DocOutbox WHERE docId = " & pDocId & " and  userid = '" & pUserId & "' "

            If objCommand.ExecScalar() > 0 Then
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

    Public Sub UpdateFileVersion()
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn

            objCommand.CommandType = CommandType.Text

            If DocSession.OraClient Then
                objCommand.CommandText = "BEGIN INSERT INTO DocFileVersion(docId,docVersion,uploadedDate,uploadedby,FileName,comments) " & _
                        "VALUES (" & pDocId & "," & pVersion & ",SYSDATE,'" & pUserId & "','" & pFileName & "','" & pComment & "'); " & _
                            "UPDATE DOCLIST SET " & _
                        "FileName = '" & pFileName & "' " & _
                        ",FileVersion = " & pVersion & " " & _
                        ",IsBeingModified = 0 " & _
                        "WHERE docId = " & pDocId & "; END; "
                objCommand.ExecNonQuery()
            Else
                objCommand.CommandText = "INSERT INTO DocFileVersion(docId,docVersion,uploadedDate,uploadedby,FileName,comments,FileSize) " & _
                        "VALUES (" & pDocId & "," & pVersion & ",GETDATE(),'" & pUserId & "','" & pFileName & "','" & pComment & "'," & pFileSize & ") " & _
                        "UPDATE DOCLIST SET " & _
                        "FileName = '" & pFileName & "' " & _
                        ",FileVersion = " & pVersion & " " & _
                        ",IsBeingModified = 0 " & _
                        "WHERE docId = " & pDocId

                objCommand.ExecNonQuery()
            End If




        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub
    Public Sub PrintDoc()
        Dim objCommand As clsSqlConn
        Dim s_sql As String


        Try
            If DocSession.OraClient Then
                s_sql = "UPDATE DOCLIST SET " & _
 "ReceiptPrinted = 1 " & _
",PrintedDate = SYSDATE " & _
",Printedby = '" & pUserId & "' " & _
"WHERE  " & _
"DocId = " & pDocId & " "
            Else
                s_sql = "UPDATE DOCLIST SET " & _
 "ReceiptPrinted = 1 " & _
",PrintedDate = '" & DateTime.Now & "' " & _
",Printedby = '" & pUserId & "' " & _
"WHERE  " & _
"DocId = " & pDocId & " "
            End If
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql

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

    Public Sub DeleteDoc()
        Dim objCommand As clsSqlConn
        Dim s_sql As String

        Try
            If DocSession.OraClient Then
                s_sql = "BEGIN UPDATE DOCLIST SET " & _
                 "StatusId = 5 " & _
                ",modifiedDate = SYSDATE" & _
                ",modifiedby = '" & pUserId & "' " & _
                "WHERE  " & _
                "DocId = " & pDocId & "; " & _
                " DELETE FROM docBookmark WHERE docid = " & pDocId & "; END;"
            Else
                s_sql = "UPDATE DOCLIST SET " & _
 "StatusId = 5 " & _
",modifiedDate = GETDATE() " & _
",modifiedby = '" & pUserId & "' " & _
"WHERE  " & _
"DocId = " & pDocId & " " & _
" DELETE FROM docBookmark WHERE docid = " & pDocId
            End If
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql

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

    Public Sub DeleteDoc(ByVal objCommand As clsSqlConn)
        Dim s_sql As String
        Try
            If DocSession.OraClient Then
                s_sql = "UPDATE DOCLIST SET " & _
                     "StatusId = 5 " & _
                    ",modifiedDate = SYSDATE" & _
                    ",modifiedby = '" & pUserId & "' " & _
                    "WHERE  " & _
                    "DocId = " & pDocId
            Else
                s_sql = "UPDATE DOCLIST SET " & _
                 "StatusId = 5 " & _
                ",modifiedDate = GETDATE() " & _
                ",modifiedby = '" & pUserId & "' " & _
                ",DeleteReason = '" & Replace(pDeleteReason, "'", "''") & "' " & _
                "WHERE  " & _
                "DocId = " & pDocId

                s_sql = s_sql & " UPDATE DocInbox SET " & _
                 "FolderId = null " & _
                "WHERE  " & _
                "DocId = " & pDocId
            End If

            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub
    Public Sub ArchiveDoc()
        Dim objCommand As clsSqlConn
        Dim s_sql As String
        Try
            If DocSession.OraClient Then
                s_sql = "UPDATE DOCLIST SET " & _
                 "StatusId = StatusId * -1 " & _
                ",modifiedDate = SYSDATE " & _
                ",modifiedby = '" & pUserId & "' " & _
                "WHERE  " & _
                "DocId = " & pDocId
            Else
                s_sql = "UPDATE DOCLIST SET " & _
                 "StatusId = StatusId * -1 " & _
                ",modifiedDate = GETDATE() " & _
                ",modifiedby = '" & pUserId & "' " & _
                "WHERE  " & _
                "DocId = " & pDocId
            End If
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql

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

    Public Sub RestoreDoc(ByVal objCommand As clsSqlConn) 'now a restore function
        Dim s_sql As String
        Try
            If DocSession.OraClient Then
                s_sql = "UPDATE DOCLIST SET " & _
                     "StatusId = 6 " & _
                    ",modifiedDate = TO_DATE('" & pModifiedDate & "','mm/dd/yyyy hh:mi:ss am') " & _
                    ",modifiedby = '" & pUserId & "' " & _
                    "WHERE  " & _
                    "DocId = " & pDocId
            Else
                s_sql = "UPDATE DOCLIST SET " & _
                     "StatusId = 6 " & _
                    ",modifiedDate = '" & pModifiedDate & "' " & _
                    ",modifiedby = '" & pUserId & "' " & _
                    "WHERE  " & _
                    "DocId = " & pDocId
            End If

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub

    Public Sub PurgeDoc(ByVal objCommand As clsSqlConn)
        'oracle george
        Dim s_sql_doclist As String = "DELETE FROM DOCLIST  " & _
"WHERE  " & _
"DocId = " & pDocId
        Dim s_sql_docindex As String = "DELETE FROM DOCINDEXVALUES  " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_docattachindex As String = "DELETE FROM DOCATTACHINDEXVALUES  " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_docnotes As String = "DELETE FROM DOCNOTES  " & _
"WHERE  " & _
"DocId = " & pDocId
        Dim s_sql_doclinks As String = "DELETE FROM DOCLINKS  " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_docversion As String = "DELETE FROM DOCFILEVERSION  " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_docrouting As String = "DELETE FROM DOCROUTING " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_doctags As String = "DELETE FROM DOCTAGS " & _
"WHERE  " & _
"DocId = " & pDocId

        Dim s_sql_bookmark As String = " DELETE FROM DOCBOOKMARK " & _
"WHERE  " & _
"DocId = " & pDocId
        Try


            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "BEGIN " & s_sql_doclist & "; " & s_sql_docindex & "; " & s_sql_docnotes & "; " & s_sql_doclinks & "; " & s_sql_docversion & "; " & s_sql_docrouting & "; " & s_sql_doctags & "; " & s_sql_bookmark & "; END; "
            Else
                objCommand.CommandText = s_sql_doclist & " " & s_sql_docindex & " " & s_sql_docattachindex & " " & s_sql_docnotes & " " & s_sql_doclinks & " " & s_sql_docversion & " " & s_sql_docrouting & " " & s_sql_doctags & " " & s_sql_bookmark
            End If


            objCommand.ExecTranNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub
    Public Function GetDocSender() As String
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT RoutedTo FROM doclist WHERE docid = " & pDocId
            objCommand = New clsSqlConn

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
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

    Public Sub UpdateDoc()
        Dim objCommand As clsSqlConn
        Dim ltran As DbTran
        Try
            ltran = New DbTran
            objCommand = New clsSqlConn(ltran.pTran)
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "UPDATE DOCLIST SET " & _
"modifiedby = '" & pUserId & "' "

            If pDocTitle <> "" Then
                s_sql = s_sql & ",title = '" & Replace(pDocTitle, "'", "''") & "' "  'case when @Title is null then Title else @Title end " & _
            End If
            If pDocStatus <> "" Then
                s_sql = s_sql & ",ApprovedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                                ",StatusId = " & pDocStatus
                Dim lsComStat As String = "," & DocSession.CompleteStatus & ","
                If lsComStat.IndexOf("," & pDocStatus & ",") >= 0 Then
                    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                    If pDocStatus = "8" Then
                        s_sql = s_sql & ",ArchivedBy = '" & DocSession.sUserId & "'"
                        s_sql = s_sql & ",ArchivedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                        s_sql = s_sql & ",ArchiverOfc = '" & DocSession.sOfcCode & "'"
                        s_sql = s_sql & ",ArchiverGrp = '" & DocSession.sUserGroup & "'"
                    End If
                Else
                    s_sql = s_sql & ",CompletedDate = null "
                    s_sql = s_sql & ",ArchivedBy = null"
                    s_sql = s_sql & ",ArchivedDate = null"
                    s_sql = s_sql & ",ArchiverOfc = null"
                    s_sql = s_sql & ",ArchiverGrp = null"
                End If
                'If pDocStatus = "18" OrElse pDocStatus = "12" OrElse pDocStatus = "15" Then
                '    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                'ElseIf pDocStatus = "8" Then
                '    s_sql = s_sql & ",ArchivedBy = '" & DocSession.sUserId & "'"
                '    s_sql = s_sql & ",ArchivedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                '    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                'Else
                '    s_sql = s_sql & ",CompletedDate = null "
                'End If
            End If
            'If pFinalDocStatus <> "" Then
            '    s_sql = s_sql & ",DocFinalStatus = " & pFinalDocStatus 'is null then StatusId else @DocStatus end " & _
            'End If
            If pDocType <> "" Then
                s_sql = s_sql & ",DocType = '" & Replace(pDocType, "'", "''") & "'"
            End If
            If pSeqNo <> "" Then
                s_sql = s_sql & ",RoutingSeqNo = '" & pSeqNo & "'"
            End If
            If pOfficeCode <> "" Then
                s_sql = s_sql & ",OfficeCode = '" & Replace(pOfficeCode, "'", "''") & "'"
            End If
            If pClassificationCode <> "" Then
                s_sql = s_sql & ",InternalDoc = '" & Replace(pClassificationCode, "'", "''") & "'"
            End If
            If pDueDate <> "" Then
                s_sql = s_sql & ",DueDate = " & IIf(DocSession.OraClient, "TO_DATE('" & pDueDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & pDueDate & "'") & " "
            End If
            If pRoutedTo <> "" Then
                s_sql = s_sql & ",RoutedTo = '" & Replace(pRoutedTo, "'", "''") & "'"
            End If
            If pRequestType <> "" Then
                s_sql = s_sql & ",RequestType = '" & Replace(pRequestType, "'", "''") & "'"
            End If
            If pConfidential <> "" Then
                s_sql = s_sql & ",Confidential = " & Replace(pConfidential, "'", "''")
            End If
            If pManner <> "" Then
                s_sql = s_sql & ",MannerReceipt = " & pManner & " "
            End If
            If pSenderName <> "" Then
                s_sql = s_sql & ",DocSender = '" & Replace(pSenderName, "'", "''") & "'"
            End If
            If pNoCopies <> "" Then
                s_sql = s_sql & ",TotalCopies = '" & Replace(pNoCopies, "'", "''") & "'"
            End If
            If pNoPages <> "" Then
                s_sql = s_sql & ",TotalPages = '" & Replace(pNoPages, "'", "''") & "'"
            End If
            If pReceivedBy <> "" Then
                s_sql = s_sql & ",ReceivedBy = '" & Replace(pReceivedBy, "'", "''") & "'"
            End If

            If pReceivedDate <> "" AndAlso pReceivedTime <> "" Then
                s_sql = s_sql & ",ReceivedDate = " & IIf(DocSession.OraClient, "TODATE('" & pReceivedDate & " " & pReceivedTime & "','mm/dd/yyyy hh:mm:ss') ", "'" & pReceivedDate & " " & pReceivedTime & "'")
            End If

            If pReturnCard <> "" Then
                s_sql = s_sql & ",ReturnCard = '" & pReturnCard & "'"
            End If
            s_sql = s_sql & ",modifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " "

            If pSetRetention = "Y" Then
                s_sql = s_sql & ",PurgeStartDate = null,RetentionStartDate = CreatedDate "
            ElseIf pSetRetention = "C" Then
                s_sql = s_sql & ",PurgeStartDate = null,RetentionStartDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " "
            ElseIf pSetRetention = "R" Then
                s_sql = s_sql & ",RetentionStartDate = null,PurgeStartDate = null "
            End If

            s_sql = s_sql & " WHERE  " & _
            "DocId = " & pDocId

            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

            If pDocType <> "" Then
                s_sql = " UPDATE DocIndexValues SET doctype = '" & pDocType & "' WHERE docID = " & pDocId & " and doctype = '" & pDocTypeOrig & "' "
                objCommand.CommandText = s_sql
                objCommand.ExecTranNonQuery()
            End If

            ltran.pTran.Commit()


        Catch ex As Exception
            ltran.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
            If Not ltran Is Nothing Then
                ltran.Dispose()
                ltran = Nothing
            End If

        End Try

    End Sub

    Public Sub UpdateDoc(ByVal objCommand As clsSqlConn)


        Try
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String = "UPDATE DOCLIST SET " & _
"modifiedby = '" & pUserId & "' "

            If pDocTitle <> "" Then
                s_sql = s_sql & ",title = '" & pDocTitle & "' "  'case when @Title is null then Title else @Title end " & _
            End If
            If pDocStatus <> "" Then
                s_sql = s_sql & ",ApprovedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                                ",StatusId = " & pDocStatus
                'If pDocStatus = "18" OrElse pDocStatus = "12" OrElse pDocStatus = "8" OrElse pDocStatus = "15" Then
                '    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                'Else
                '    s_sql = s_sql & ",CompletedDate = null "
                'End If
                Dim lsComStat As String = "," & DocSession.CompleteStatus & ","
                If lsComStat.IndexOf("," & pDocStatus & ",") >= 0 Then
                    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                    If pDocStatus = "8" Then
                        s_sql = s_sql & ",ArchivedBy = '" & DocSession.sUserId & "'"
                        s_sql = s_sql & ",ArchivedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                    End If
                Else
                    s_sql = s_sql & ",CompletedDate = null "
                    s_sql = s_sql & ",ArchivedBy = null"
                    s_sql = s_sql & ",ArchivedDate = null"
                End If
            End If
            If pDocType <> "" Then
                s_sql = s_sql & ",DocType = '" & pDocType & "'"
            End If
            If pSeqNo <> "" Then
                s_sql = s_sql & ",RoutingSeqNo = '" & pSeqNo & "'"
            End If
            If pDueDate <> "" Then
                s_sql = s_sql & ",DueDate = '" & pDueDate & "'"
            End If
            If pSenderName <> "" Then
                s_sql = s_sql & ",DocSender = '" & pSenderName & "'"
            End If
            s_sql = s_sql & ",modifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
"WHERE  " & _
"DocId = " & pDocId
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

            If pDocType <> "" Then
                s_sql = " update docIndexValues " & _
                                    " ColumnId = di2.ColumnId " & _
                              ", DocType = '" & pDocType & "' " & _
                                    "from " & _
                                    " docIndexValues div1 " & _
                      " inner join DocIndex di" & _
                       " on div1.ColumnId = di.ColumnId" & _
                        " and div1.DocType = di.DocType" & _
                      " inner join DocIndex di2" & _
                       " on di2.DocType = '" & pDocType & "' " & _
                        " and di2.ColumnName = di.ColumnName" & _
                     " where div1.DocId = " & pDocId & " " '& s_sql
                objCommand.CommandText = s_sql
                objCommand.ExecTranNonQuery()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub

    Public Sub UpdateDocCopy(ByVal objCommand As clsSqlConn)


        Try
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String = "UPDATE DOCLIST SET " & _
"modifiedby = '" & pUserId & "' "


            If pDocStatus <> "" Then
                s_sql = s_sql & ",ApprovedDate = case when statusid = 2 then " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " else ApprovedDate end " & _
                                ",StatusId = case when statusid = 2 then " & pDocStatus & " else StatusId end "
                'If pDocStatus = "18" OrElse pDocStatus = "12" OrElse pDocStatus = "8" OrElse pDocStatus = "15" Then
                '    s_sql = s_sql & ",CompletedDate = GetDate() "
                'Else
                '    s_sql = s_sql & ",CompletedDate = case when statusid = 2 then Null else CompletedDate end "
                'End If
                Dim lsComStat As String = "," & DocSession.CompleteStatus & ","
                If lsComStat.IndexOf("," & pDocStatus & ",") >= 0 Then
                    s_sql = s_sql & ",CompletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")

                Else
                    s_sql = s_sql & ",CompletedDate = case when statusid = 2 then Null else CompletedDate end "

                End If
            End If

            If pSeqNo <> "" Then
                s_sql = s_sql & ",RoutingSeqNo = '" & pSeqNo & "'"
            End If

            s_sql = s_sql & ",modifiedDate = GetDate() " & _
"WHERE  " & _
"DocId = " & pDocId
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub
    Public Sub UpdateDocReceivedBy()
        Dim objCommand As clsSqlConn

        Try

            objCommand = New clsSqlConn()
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "UPDATE DOCLIST SET " & _
"modifiedby = '" & pUserId & "' "


            If pReceivedBy <> "" Then
                s_sql = s_sql & ",ReceivedBy = '" & pReceivedBy & "'"
            End If

            If pReceivedDate <> "" AndAlso pReceivedTime <> "" Then
                s_sql = s_sql & ",ReceivedDate = " & IIf(DocSession.OraClient, "TODATE('" & pReceivedDate & " " & pReceivedTime & "','mm/dd/yyyy hh:mm:ss') ", "'" & pReceivedDate & " " & pReceivedTime & "'")
            End If

            s_sql = s_sql & " WHERE  " & _
            "RefNo = '" & pRefNo & "' "

            objCommand.CommandText = s_sql
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
    Public Sub UpdateDocMonitoringReceiving()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            
            Dim lssql As String
            lssql = "UPDATE DocMonitoringReceiving set ReceivedBy = '" & Replace(pReceivedBy, "'", "''") & "',ReceivedDate = '" & pReceivedDate & " " & pReceivedTime & "', EncodedBy = '" & DocSession.sUserId & "' " & _
",EncodedDate = GetDate() " & _
",Remarks = '" & Replace(pComment, "'", "''") & "' " & _
",CutOffDate = '" & pCutOffDate & "' " & _
",Duration = " & pDocAge & " " & _
" WHERE DocId = " & pDocId & " " & _
" if @@rowcount = 0 "
            lssql = lssql & " INSERT INTO DocMonitoringReceiving(docId,Month,year,ReceivedBy,ReceivedDate,EncodedBy,EncodedDate,RefNo,StatusId,GroupCode,Remarks,Duration,CutOffDate,CreatedDate) " & _
" VALUES (" & pDocId & ", Month('" & pCreatedDate & "') , Year('" & pCreatedDate & "') " & _
",'" & pReceivedBy & "'," & IIf(pReceivedDate.Trim = "", "null", "'" & pReceivedDate & " " & pReceivedTime & "'") & ",'" & DocSession.sUserId & "',getdate(),'" & pRefNo & "',2,'" & Replace(pGroupId, "'", "''") & "','" & pComment & "'," & pDocAge & ",'" & pCutOffDate & "','" & pCreatedDate & "')"

            objCommand.pCommandText = lssql
            objCommand.ExecNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
            End If
        End Try
    End Sub
    Public Function RetrieveDocId() As DataTable

        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try

            Dim s_sql As String
            s_sql = "SELECT dl.DocId " &
                  ",upper(dl.DocType) as DocType " &
               ",dt.DocName " &
                  ",dl.Title " &
                  ",dl.Location " &
                  ",dl.CreatedDate " &
                  ",isnull(convert(char(10),dl.CompletedDate,101),'') as cdate " &
                  ",dl.CreatedBy " &
                  ",dl.ModifiedBy " &
                  ",isnull(dl.ArchivedBy,'') as ArchivedBy " &
                  ",dl.ArchivedDate " &
                  ",dl.ModifiedDate " &
                  ",dl.IsBeingModified " &
                  ",dl.StatusId " &
               ",ds.description AS statusdesc" &
               ",dl.FileVersion " &
               ",dl.DueDate " &
               ",5 as GroupAccessId " &
               ",RoutedTo " &
               ",CCTo " &
               ",TotalCopies " &
               ",TotalPages " &
               ",r.ReceiptDesc " &
               ",dl.ParentDocId " &
               ",InternalDoc = case when dl.InternalDoc = 1 then '1' else '0' end " &
               ",dl.AdditionalInfo " &
               ",dl.ReceivedBy " &
               ",dl.ReturnCard " &
               ",u.usergroup " &
               ",dl.Confidential " &
               ",Isnull(dl.IsLocal,0) as IsLocal " &
               ",right(CONVERT(varchar,dl.receiveddate),7) as ReceivedTime " &
               ",Convert(char(10),dl.ReceivedDate,101) as ReceivedDate " &
                ",(case when db.datebookmark is null then 0 else 1 end) AS bookmarked " &
                ",(case when dl.receiptprinted = 1 then 'Yes' else 'No' end) AS printed "



            s_sql = s_sql & ",1 AS CanPrint " & _
           ",isnull(dl.FileName,'') AS FileName " & _
           ",isnull(u.FirstName,'')+' '+isnull(u.LastName,'') AS originator " & _
           ",isnull(Ab.FirstName,'')+' '+isnull(Ab.LastName,'') AS ArchiverName " & _
           ",isnull(u2.FirstName,'')+' '+isnull(u2.LastName,'') AS checkoutby " & _
           ",1 AS VersionControl " & _
           ",1 AS CanDownload " & _
           ",isnull(dl.RequestType,'') AS RequestType " & _
           ",isnull(rt.RequestDescription,'') AS RequestDescription " & _
           ",1 AS CanPrintReceipt " & _
           ",isnull(dl.refno,'') AS refno " & _
           ",isnull(dl.officeCode,'') AS officeCode " & _
           ",isnull(O.Description,'') AS officeName " & _
           ",isnull(dl.MannerReceipt,'') AS MannerReceipt " & _
           ",isnull(r.ReceiptDesc,'') AS MannerReceiptDesc " & _
           ",DocSender = isnull(dl.DocSender,'') " & _
           ",Classification = case when dl.InternalDoc = 1 then 'Internal' else 'External' end " & _
            ",isnull(ug.officeCode,'') AS UploadOfficeCode " & _
            ",isnull(ug.GroupId,'') AS UploadGroupId " & _
            ",isnull(dt.AllowPrinting,0) AS AllowPrinting " & _
            ",isnull(AbGrp.GroupId,0) AS ArchiverGroupId " & _
            ",isnull(AbGrp.officeCode,'') AS ArchiverOfficeCode "

            s_sql = s_sql & "FROM DocList dl INNER JOIN " & _
             "DocType dt ON dl.DocType = dt.DocType " & _
             "INNER JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
             "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
             "INNER JOIN groups ug ON U.UserGroup = ug.GroupId " & _
             "LEFT JOIN Users U2 ON U2.userId = dl.ModifiedBy " & _
             "LEFT JOIN Users Ab ON Ab.userId = dl.ArchivedBy " & _
             "LEFT JOIN groups AbGrp ON Ab.UserGroup = AbGrp.GroupId " & _
             "LEFT JOIN Office O ON O.OfficeCode = dl.OfficeCode " & _
             "LEFT JOIN docReceipts r ON r.ReceiptId = dl.MannerReceipt " & _
             "LEFT JOIN docRequestType rt ON rt.RequestType = dl.RequestType " & _
             "LEFT JOIN DocBookMark db ON dl.docId = db.docId and db.userid = '" & pUserId & "' " & _
             "WHERE " & _
              " dl.StatusId > 0 and dl.DocId = " & pDocId

            '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue

            'pRetVal = retval.pParam.Value

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

    Public Function RetrieveRequestType() As DataTable

        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String
            s_sql = "SELECT rt.RequestType " & _
                  ",rt.RequestDescription " & _
               " FROM DocRequestType rt " & _
             "WHERE " & _
              " rt.Inactive Is Null "
            If pOfficeCode <> "" Then
                s_sql = s_sql & " and rt.Office = '" & pOfficeCode & "' "
            End If

            s_sql = s_sql & " ORDER BY rt.RequestDescription "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            objCommand.CommandText = s_sql  '"xMSP_DOCID"


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
        If pSortCol = "Document Type" Then
            Return "dt.docname"
        ElseIf pSortCol = "Title" Then
            Return "dl.title"
        ElseIf pSortCol = "In-Charge" OrElse pSortCol = "In-Charge(Office)" OrElse pSortCol = "Location(Office)" Then
            Return "(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,''))"
        ElseIf pSortCol = "Routed To" OrElse pSortCol = "Point Person" Then
            Return "dl.FirstApprover"
        ElseIf pSortCol = "Created Date" Then
            Return "dl.CreatedDate"

        ElseIf pSortCol = "Author" Then
            If DocSession.OraClient Then
                Return "Originator"
            Else
                Return "Originator"
            End If


        ElseIf pSortCol = "Status" Then
            Return "statusdesc"
        ElseIf pSortCol = "Deleted Date" Then
            Return "dl.ModifiedDate"
        ElseIf pSortCol = "Status Desc" Then
            Return "ds.description"
        ElseIf pSortCol = "Reference No" Then
            Return "dl.refno"
        ElseIf pSortCol = "Agency" Then
            Return "O.Description"
        ElseIf pSortCol = "!" Then
            Return "Urgent"
        ElseIf pSortCol = "F" Then
            Return "doc_id"
        Else
            Return "dl.docid"
        End If
    End Function
    Private Function OrderByForms() As String
        If pSortCol = "Document Type" Then
            Return "dt.docname"
        ElseIf pSortCol = "Title" Then
            Return "dl.title"
        ElseIf pSortCol = "In-Charge" OrElse pSortCol = "In-Charge(Office)" OrElse pSortCol = "Location(Office)" Then
            Return "(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,''))"
        ElseIf pSortCol = "Routed To" OrElse pSortCol = "Point Person" Then
            Return "dl.FirstApprover"
        ElseIf pSortCol = "Created Date" Then
            Return "dl.CreatedDate"

        ElseIf pSortCol = "Author" Then
            If DocSession.OraClient Then
                Return "Originator"
            Else
                Return "Originator"
            End If


        ElseIf pSortCol = "Status" Then
            Return "statusdesc"
        ElseIf pSortCol = "Deleted Date" Then
            Return "dl.ModifiedDate"
        ElseIf pSortCol = "Status Desc" Then
            Return "ds.description"
        ElseIf pSortCol = "Reference No" Then
            Return "dl.refno"
        ElseIf pSortCol = "Agency" Then
            Return "O.Description"
        ElseIf pSortCol = "!" Then
            Return "Urgent"
        ElseIf pSortCol = "F" Then
            Return "doc_id"
        Else
            Return "dl.docid"
        End If
    End Function
    Private Function OrderByReqList() As String
        If pSortCol = "Received" Then
            Return "dl.CreatedDate"
        ElseIf pSortCol = "Due Date" Then
            Return "dl.DueDate"
        ElseIf pSortCol = "Reference No" Then
            Return "dl.RefNo"
        ElseIf pSortCol = "Status" Then
            Return "ds.description"
        ElseIf pSortCol = "Last Action" Then
            Return "ds.description"
        ElseIf pSortCol = "Subject" Then
            Return "dl.Title"
        ElseIf pSortCol = "Personnel In-Charge" OrElse pSortCol = "In-Charge" Then
            Return "isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName)"
        ElseIf pSortCol = "Bureau/Office" Then
            Return "dl.OfficeCode"
        ElseIf pSortCol = "Age" Then
            If DocSession.OraClient Then
                Return "age"
            Else
                Return "datediff(day,dl.createddate,isnull(div.colvalue,isnull(dl.completeddate,getdate())))"
                'Return "Age"
            End If

        ElseIf pSortCol = "Age2" Then
            If DocSession.OraClient Then
                Return "age"
            Else
                Return "datediff(day,dl.createddate,isnull(div.colvalue,isnull(dl.completeddate,getdate())))"

            End If

        Else
            Return "dl.CreatedDate"
        End If
    End Function

    Private Function BuildRestoreWhere() As String

        Dim lssql As String = ""
        If DocSession.OraClient Then
            If pArchiveFrom <> "" Then
                lssql = lssql & " and trunc(dl.ModifiedDate) >= TO_DATE('" & pArchiveFrom & "','mm/dd/yyyy')"
            End If

            If pArchiveTo <> "" Then
                lssql = lssql & " and trunc(dl.ModifiedDate) <= TO_DATE('" & pArchiveTo & "','mm/dd/yyyy')"
            End If
        Else
            If pArchiveFrom <> "" Then
                lssql = lssql & " and dl.ModifiedDate >= '" & pArchiveFrom & "'"
            End If

            If pArchiveTo <> "" Then
                lssql = lssql & " and dl.ModifiedDate <= '" & pArchiveTo & "'"
            End If
            If pRefNo <> "" Then
                lssql = lssql & " and dl.refno " & pRefNo
            End If
        End If

        If pUserId <> "" Then
            lssql = lssql & " and dl.CreatedBy= '" & pUserId & "'"
        End If

        If pDocType <> "" Then
            lssql = lssql & " and dl.DocType = '" & pDocType & "'"
        End If
        If pDocTitle <> "" Then
            lssql = lssql & " and dl.Title like '%" & pDocTitle & "%'"
        End If
        If pOfficeCode <> "" Then
            lssql = lssql & " and dl.OfficeCode = '" & pOfficeCode & "'"
        End If


        Return lssql
    End Function

    Private Function BuildArchiveWhere() As String
        Dim lsFrom As String
        Dim lsTo As String
        Dim lsNow As String = DateTime.Now.ToShortDateString
        If pAgeProcess = "month" Then
            lsFrom = CDate(lsNow).AddMonths((CInt(pDocAge) + 1) * -1).ToShortDateString
            lsTo = CDate(lsNow).AddMonths(CInt(pDocAge) * -1).ToShortDateString
        Else
            lsFrom = CDate(lsNow).AddYears((CInt(pDocAge) + 1) * -1).ToShortDateString
            lsTo = CDate(lsNow).AddYears(CInt(pDocAge) * -1).ToShortDateString
        End If

        Dim lssql As String
        If DocSession.OraClient Then
            lssql = " and trunc(dl.CreatedDate) >= TO_DATE('" & lsFrom & "','mm/dd/yyyy') and trunc(dl.CreatedDate) <= TO_DATE('" & lsTo & "','mm/dd/yyyy') "
        Else
            lssql = " and dl.CreatedDate >= '" & lsFrom & "' and dl.CreatedDate <= '" & lsTo & "'"
        End If


        If pDocType <> "" Then
            lssql = lssql & " and dl.DocType = '" & pDocType & "'"
        End If

        If pOfficeCode <> "" Then
            lssql = lssql & " and dl.OfficeCode = '" & pOfficeCode & "'"
        End If


        Return lssql
    End Function
    Private Function BuildClassificationWhere() As String
        Dim lssql As String = ""
        If pClassificationCode <> "" Then
            If pClassificationCode = "0" Then
                lssql = lssql & " AND (dl.InternalDoc Is Null or dl.InternalDoc = 0) "
            Else
                lssql = lssql & " AND dl.InternalDoc = 1 "
            End If
        End If
        Return lssql
    End Function

    Private Function BuildWhere() As String
        Dim lssql As String = ""
        If pRoutedTo <> "" Then
            lssql = lssql & " AND dl.FirstApprover like '%" & Replace(pRoutedTo, "'", "''") & "%'"
        End If
        If pConfidential <> "" Then
            If pConfidential = "1" Then
                lssql = lssql & " AND dl.Confidential = 1 "
            Else
                lssql = lssql & " AND (dl.Confidential is null or dl.Confidential = 0)"
            End If

        End If
        If pDocStatusId <> "" Then
            lssql = lssql & " AND dl.StatusId = " & pDocStatusId
        End If
        If pRefNo <> "" Then
            lssql = lssql & " AND dl.refno like '%" & pRefNo & "%'"
        End If

        If pDocId <> "" Then
            lssql = lssql & " AND dl.DocId = " & pDocId
        End If
        If pDocTitle <> "" Then
            lssql = lssql & " AND dl.Title LIKE  '%" & Replace(pDocTitle, "'", "''") & "%'"
        End If
        If pDocType <> "" Then
            lssql = lssql & " AND dl.DocType = '" & pDocType & "' "
        End If

        If pClassificationCode <> "" Then
            If pClassificationCode = "0" Then
                lssql = lssql & " AND (dl.InternalDoc Is Null or dl.InternalDoc = 0) "
            Else
                lssql = lssql & " AND dl.InternalDoc = 1 "
            End If
        End If

        If pManner <> "" Then
            lssql = lssql & " AND (dl.MannerReceipt = '" & pManner & "') "
            If pReturnCard <> "" Then
                lssql = lssql & " AND (dl.ReturnCard Like '%" & pReturnCard & "%') "

            End If
        End If


        If pCreatedDate <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) = TO_DATE('" & pCreatedDate & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND convert(datetime,convert(varchar(10),dl.CreatedDate,101)) = convert(datetime,'" & pCreatedDate & "') "
            End If

        End If
        If pCreatedDateFrom <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) >= TO_DATE('" & pCreatedDateFrom & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND dl.CreatedDate >= '" & pCreatedDateFrom & "' "
            End If

        End If

        If pCreatedDateTo <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) <= TO_DATE('" & pCreatedDateTo & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND dl.CreatedDate < dateadd(day,1,'" & pCreatedDateTo & "') "
            End If

        End If

        If pAuthor <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND u.FirstName||' '||u.LastName like '%" & pAuthor & "%' "
            Else
                lssql = lssql & " AND u.FirstName+' '+u.LastName like '%" & pAuthor & "%' "
            End If

        End If
        Return lssql
    End Function

    Private Function BuildWhereAllDoc() As String
        Dim lssql As String = ""
        If pRoutedTo <> "" Then
            lssql = lssql & " AND dl.FirstApprover like '%" & Replace(pRoutedTo, "'", "''") & "%'"
        End If
        If pDocStatusId <> "" Then
            lssql = lssql & " AND dl.StatusId = " & pDocStatusId
        End If
        If pRefNo <> "" Then
            lssql = lssql & " AND dl.refno like '%" & pRefNo & "%'"
        End If

        If pDocId <> "" Then
            lssql = lssql & " AND dl.DocId = " & pDocId
        End If
        If pDocTitle <> "" Then
            lssql = lssql & " AND dl.Title LIKE  '%" & Replace(pDocTitle, "'", "''") & "%'"
        End If
        If pDocType <> "" Then
            lssql = lssql & " AND dl.DocType = '" & pDocType & "' "
        End If

        If pClassificationCode <> "" Then
            If pClassificationCode = "0" Then
                lssql = lssql & " AND (dl.InternalDoc Is Null or dl.InternalDoc = 0) "
            Else
                lssql = lssql & " AND dl.InternalDoc = 1 "
            End If
        End If

        If pCreatedDate <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) = TO_DATE('" & pCreatedDate & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND convert(datetime,convert(varchar(10),dl.CreatedDate,101)) = convert(datetime,'" & pCreatedDate & "') "
            End If

        End If
        If pCreatedDateFrom <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) >= TO_DATE('" & pCreatedDateFrom & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND convert(datetime,convert(varchar(10),dl.CreatedDate,101)) >= convert(datetime,'" & pCreatedDateFrom & "') "
            End If

        End If

        If pCreatedDateTo <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND trunc(dl.CreatedDate) <= TO_DATE('" & pCreatedDateTo & "','mm/dd/yyyy') "
            Else
                lssql = lssql & " AND convert(datetime,convert(varchar(10),dl.CreatedDate,101)) <= convert(datetime,'" & pCreatedDateTo & "') "
            End If

        End If

        If pAuthor <> "" Then
            If DocSession.OraClient Then
                lssql = lssql & " AND u.FirstName||' '||u.LastName like '%" & pAuthor & "%' "
            Else
                lssql = lssql & " AND u.FirstName+' '+u.LastName like '%" & pAuthor & "%' "
            End If

        End If
        Return lssql
    End Function
    Private Function BuildWhereAllDocInnerJoin() As String
        Dim asSql As String = ""
        'personnel in charge should be in inner join
        If pAuthor <> "" Then
            asSql = asSql & " INNER JOIN users u " & _
                            "  ON dl.createdby = u.userid  and u.FirstName+' '+u.LastName like '%" & pAuthor & "%'"
        End If

        Return asSql

    End Function
    Private Function BuildWhereAllDocLeftJoin() As String
        Dim asSql As String = ""
        'personnel in charge should be in inner join
        If pAuthor <> "" Then
            asSql = asSql & " left JOIN users u " & _
                            "  ON dl.createdby = u.userid  and u.FirstName+' '+u.LastName like '%" & pAuthor & "%'"
        End If

        Return asSql

    End Function
    Public Function RetrieveFileVersion() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dfv.docId " & _
      ",dfv.docVersion " & _
      ",dfv.uploadedDate " & _
      ",dl.CreatedDate " & _
      ",dl.refno " & _
      ",dfv.uploadedby "

            s_sql = s_sql & ",isnull(dfv.comments,'') AS comments " &
                    ",isnull(dfv.filename,'') AS fileName "

            s_sql = s_sql & "FROM DocFileVersion  dfv " & _
  "INNER JOIN doclist dl on dfv.docid = dl.docid and dl.StatusId > 0  " & _
  "WHERE dfv.docId = " & pDocId & " " & _
    "and dfv.docversion = " & pDocVersion

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCFILEVERSION"

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

    Public Function RetrieveFileVersionByRefNo() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dl.docId " & _
      ",dl.CreatedDate " & _
      ",dl.refno " & _
      ",dl.fileName " & _
      ",dl.FileVersion "
           
                
           
            s_sql = s_sql & "FROM doclist  dl " & _
  "WHERE dl.refno = '" & pRefNo & "' "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql '"xMSP_DOCFILEVERSION"




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

    Public Function RetrieveRefNoCreatedDate() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dl.DocId " & _
      ",dl.CreatedDate " & _
      ",da.DocFileName " & _
      ",dl.refno "
            

            s_sql = s_sql & "FROM DocList dl " & _
                " INNER JOIN DocAttachment da ON dl.docid = da.docid and da.DocAttachId = " & pDocVersion & _
  " WHERE dl.docId = " & pDocId

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = s_sql '"xMSP_DOCFILEVERSION"




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
    Public Function RetrieveDateIssued() As String

        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT  "

            If DocSession.OraClient Then
                s_sql = s_sql & "NVL(div.colvalue,'') AS colval "
            Else
                s_sql = s_sql & "ISNULL(div.colvalue,'') AS colval "
            End If

            s_sql = s_sql & "FROM doclist dl " & _
                      "INNER JOIN docindex di ON " & _
                      " dl.doctype = di.doctype " & _
                      "LEFT JOIN docindexvalues div ON " & _
                      " div.doctype = dl.doctype and div.docid = dl.docid and div.columnid = di.columnid " & _
                      "WHERE dl.docId = " & pDocId & " and di.ColumnName='date issued' "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql



            Return objCommand.ExecScalar3


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Function
    Public Function RetrieveDocVersion() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT docId " & _
      ",docVersion " & _
      ",uploadedDate " & _
      ",uploadedby " & _
      ",comments " & _
      ",filename " & _
            ",FileSize=isnull(FileSize,'') "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(u.Firstname,'') || ' ' || NVL(u.LastName,'') AS uploadedbyName "
            Else
                s_sql = s_sql & ",isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS uploadedbyName "
            End If

            s_sql = s_sql & "FROM DocFileVersion  dfv " & _
                      "INNER JOIN Users U ON " & _
                      "dfv.uploadedby = u.userid WHERE docId = " & pDocId & " " & _
                      "ORDER BY docversion desc"

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_DOCFILEVERSIONGET"
            'objCommand.ParametersAddWithValue("@DocId", CInt(pDocId))


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

    Public Sub AddWatermark(ByVal inStream As FileStream, ByVal watermarkText As String, ByVal outStream As Stream)


        Dim newImg As Drawing.Image = Drawing.Image.FromStream(inStream)
        Dim font As New Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel)

        'Adds a transparent watermark with an 100 alpha value.
        Dim color As Color = Drawing.Color.FromArgb(50, 0, 0, 0)

        'The position where to draw the watermark on the image
        Dim point As New Point(10, 10)
        Dim myBrush As New SolidBrush(color)
        Dim gr As Graphics = Nothing

        Try
            gr = Graphics.FromImage(newImg)
        Catch
            Dim img1 As Drawing.Image = newImg
            newImg = New Bitmap(newImg.Width, newImg.Height)
            gr = Graphics.FromImage(newImg)
            gr.DrawImage(img1, New Rectangle(0, 0, newImg.Width, newImg.Height), 0, 0, newImg.Width, newImg.Height, GraphicsUnit.Pixel)
            img1.Dispose()
        End Try

        gr.DrawString(watermarkText, font, myBrush, point)
        gr.Dispose()

        newImg.Save(outStream, ImageFormat.Jpeg)
    End Sub

    Public Function RetrieveSubTask()
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dl.docId,dl.refno,cdate = convert(char(10),dl.createddate,101),dl.CreatedDate " &
      ",dl.CreatedBy " &
      ",dl.Title "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(u.Firstname,'') || ' ' || NVL(u.LastName,'') AS createdbyName "
            Else
                s_sql = s_sql & ",isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS createdbyName "
            End If

            s_sql = s_sql & "FROM DocList  dl " &
                      "INNER JOIN Users u ON " &
                      "dl.createdby = u.userid WHERE dl.statusid <> 5 and dl.ParentDocId = " & pParentDocId & " and dl.docId <> " & pDocId

            Dim s_sql2 As String = "SELECT dl.docId,dl.refno,cdate = convert(char(10),dl.createddate,101),dl.CreatedDate " &
      ",dl.CreatedBy " &
      ",dl.Title "
            If DocSession.OraClient Then
                s_sql2 = s_sql2 & ",NVL(u.Firstname,'') || ' ' || NVL(u.LastName,'') AS createdbyName "
            Else
                s_sql2 = s_sql2 & ",isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS createdbyName "
            End If

            s_sql2 = s_sql2 & "FROM DocList  dl " &
                      "INNER JOIN Users u ON " &
                      "dl.createdby = u.userid WHERE dl.statusid <> 5 and dl.DocId = " & pParentDocId & " union "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If pParentDocId <> pDocId Then
                objCommand.CommandText = "select * from (" & s_sql2 & s_sql & ") tbl order by docid "
            Else
                objCommand.CommandText = s_sql & " order by docid "
            End If


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

    Public Function RetrieveSubTask2()
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dl.docId,0 as linkdocid,dl.refno,cdate = convert(char(10),dl.createddate,101),dl.CreatedDate " &
      ", dl.CreatedBy ,dl.Title,isnull(o.Description,'') as Office,isnull(g.OfficeCode,'') as OfficeCode ,isnull(dr.approverid,0) as ApproverID ,isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS createdbyName " &
", isnull(dl.RoutingSeqNo, 0) RoutingSeqNo "

            s_sql = s_sql & "FROM DocList  dl " &
                      "left JOIN DocRouting dr ON " &
                      "dr.SeqNo = dl.RoutingSeqNo " &
                      "left JOIN Users u ON " &
                      "dr.approverid = u.userid " &
                      "left JOIN Groups g ON " &
                      "g.GroupID = u.UserGroup " &
                      "LEFT JOIN Office o ON " &
                      "g.OfficeCode = o.OfficeCode " &
                      " WHERE dl.statusid <> 5 and dl.ParentDocId = " & pParentDocId & " and dl.docId <> " & pDocId

            Dim s_sql2 As String = "SELECT dl.docId,0 as linkdocid,dl.refno,cdate = convert(char(10),dl.createddate,101),dl.CreatedDate " &
      ", dl.CreatedBy ,dl.Title,isnull(o.Description,'') as Office,isnull(g.OfficeCode,'') as OfficeCode ,isnull(dr.approverid,0) as ApproverID ,isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS createdbyName " &
", isnull(dl.RoutingSeqNo, 0) RoutingSeqNo "

            s_sql2 = s_sql2 & "FROM DocList  dl " &
                       "LEFT JOIN DocRouting dr ON " &
                      "dr.SeqNo = dl.RoutingSeqNo " &
                      "LEFT JOIN Users u ON " &
                      "dr.approverid = u.userid " &
                      "LEFT JOIN Groups g ON " &
                      "g.GroupID = u.UserGroup " &
                      "LEFT JOIN Office o ON " &
                      "g.OfficeCode = o.OfficeCode " &
                      " WHERE dl.statusid <> 5 and dl.DocId = " & pParentDocId & " union "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If pParentDocId <> pDocId Then
                objCommand.CommandText = "select * from (" & s_sql2 & s_sql & ") tbl order by docid "
            Else
                objCommand.CommandText = s_sql & " order by docid "
            End If


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

    Public Function CountSubTask(ByVal asParentDocId As String, ByVal asDocId As String) As Integer

        Dim objCommand As clsSqlConn

        Try
            If asParentDocId = "" Then
                asParentDocId = asDocId
            End If
            Dim s_sql As String = "SELECT count(dl.docid) "

            s_sql = s_sql & "FROM DocList  dl " & _
                      " WHERE dl.statusid <> 5 and dl.ParentDocId = " & asParentDocId & " and dl.docId <> " & asDocId

            Dim rcnt As Integer
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            rcnt = objCommand.ExecScalar
            If asParentDocId <> asDocId Then
                rcnt = rcnt + 1
            End If

            Return rcnt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try
    End Function
    Public Function GetMaxSuffix() As String
        Dim s_sql As String

        Dim objCommand As clsSqlConn

        Try
            If DocSession.OraClient Then
                s_sql = "SELECT nvl(max(refno),'') FROM doclist WHERE parentDocId = " & pDocId
            Else
                s_sql = "SELECT top 1 refno FROM doclist WHERE parentDocId = " & pParentDocId & " ORDER BY docid desc "
            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Dim s_suffix As String = objCommand.ExecScalar3()

            If s_suffix <> "" Then
                Dim finalsuffix As String = s_suffix.Split("-")(2)
                Dim sfx As String = ""
                If finalsuffix.IndexOf("Z") >= 0 Then
                    sfx = Mid(finalsuffix, finalsuffix.IndexOf("Z") + 1)
                Else
                    sfx = Right(finalsuffix, 1)
                End If

                If sfx = "Z" Then
                    Return "Z1"
                ElseIf Left(sfx, 1) = "Z" Then
                    Return "Z" & CStr(CInt(Mid(sfx, 2)) + 1)
                Else
                    Dim liAsc As Integer = Asc(Right(s_suffix, 1)) + 1

                    Return Chr(liAsc)
                End If
            Else
                Return "A"
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
#Region "Subtask Functionality"
    Public Sub CreateSubTask(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            'objCommand.pCommandText = lssql
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim s_sql As String
            If DocSession.OraClient Then
                s_sql = "BEGIN INSERT INTO DocList " & _
           "(DocId " & _
           ",DocType " & _
           ",Title " & _
           ",FileName " & _
           ",Location " & _
           ",CreatedDate " & _
           ",CreatedBy" & _
           ",ModifiedBy" & _
           ",ModifiedDate" & _
           ",IsBeingModified" & _
           ",StatusId" & _
           ",IPAddress" & _
           ",ApprovedBy" & _
           ",ApprovedDate" & _
           ",FileVersion" & _
           ",RoutedTo" & _
           ",RefNo" & _
           ",OfficeCode" & _
           ",ReceiptPrinted" & _
           ",TotalCopies" & _
           ",TotalPages" & _
           ",MannerReceipt" & _
           ",CCTo" & _
           ",DocSender" & _
           ",RequestType" & _
           ",RoutingSeqNo" & _
           ",PrintedDate" & _
           ",PrintedBy" & _
           ",DueDate" & _
           ",UploaderOfc" & _
           ",UploaderGrp" & _
           ",ParentDocId)" & _
            "SELECT " & pDocId & _
                              ",DocType " & _
                              ",Title " & _
                              ",FileName " & _
                              ",Location " & _
                              "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                              ",'" & DocSession.sUserId & "' " & _
                              ",'" & DocSession.sUserId & "' " & _
                              "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                              ",IsBeingModified " & _
                              ",2 " & _
                              ",'" & pIPAddress & "'" & _
                              ",ApprovedBy " & _
                              ",ApprovedDate " & _
                              ",1 " & _
                              ",RoutedTo " & _
                              ",'" & pRefNo & "' " & _
                              ",OfficeCode " & _
                              ",0 " & _
                              ",TotalCopies " & _
                              ",TotalPages " & _
                              ",MannerReceipt " & _
                              ",CCTo " & _
                              ",DocSender " & _
                              ",RequestType " & _
                              ",Null " & _
                              ",Null " & _
                              ",Null " & _
                              ",Null " & _
                              ",UploaderOfc " & _
                              ",UploaderGrp " & _
                              "," & pParentDocId & _
                          " FROM DocList " & _
                          " WHERE docid = " & pParentDocId & "; "



                s_sql = s_sql & " INSERT INTO DocIndexValues (DocId,DocType,ColumnId,ColValue) (SELECT " & pDocId & ",DocType,ColumnId,ColValue " & _
                                    "FROM DocIndexValues WHERE DocId = " & pParentDocId & "); END;"
            Else
                s_sql = "INSERT INTO DocList " & _
           "(DocId " & _
           ",DocType " & _
           ",Title " & _
           ",FileName " & _
           ",Location " & _
           ",CreatedDate " & _
           ",CreatedBy" & _
           ",ModifiedBy" & _
           ",ModifiedDate" & _
           ",IsBeingModified" & _
           ",StatusId" & _
           ",IPAddress" & _
           ",ApprovedBy" & _
           ",ApprovedDate" & _
           ",FileVersion" & _
           ",RoutedTo" & _
           ",RefNo" & _
           ",OfficeCode" & _
           ",ReceiptPrinted" & _
           ",TotalCopies" & _
           ",TotalPages" & _
           ",MannerReceipt" & _
           ",CCTo" & _
           ",DocSender " & _
           ",RequestType " & _
           ",FirstApprover " & _
           ",RoutingSeqNo" & _
           ",PrintedDate" & _
           ",PrintedBy" & _
           ",DueDate" & _
           ",UploaderOfc" & _
           ",UploaderGrp" & _
           ",ParentDocId)" & _
            "SELECT " & pDocId & _
                              ",DocType " & _
                              ",Title " & _
                              ",FileName " & _
                              ",Location " & _
                              "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                              ",'" & DocSession.sUserId & "' " & _
                              ",'" & DocSession.sUserId & "' " & _
                              "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                              ",IsBeingModified " & _
                              ",2 " & _
                              ",'" & pIPAddress & "'" & _
                              ",ApprovedBy " & _
                              ",ApprovedDate " & _
                              ",1 " & _
                              ",RoutedTo " & _
                              ",'" & pRefNo & "' " & _
                              ",OfficeCode " & _
                              ",0 " & _
                              ",TotalCopies " & _
                              ",TotalPages " & _
                              ",MannerReceipt " & _
                              ",CCTo " & _
                              ",DocSender " & _
                              ",RequestType " & _
                              ",FirstApprover " & _
                              ",Null " & _
                              ",Null " & _
                              ",Null " & _
                              ",Null " & _
                              ",UploaderOfc" & _
                               ",UploaderGrp" & _
                              "," & pParentDocId & _
                          " FROM DocList " & _
                          " WHERE docid = " & pParentDocId



                s_sql = s_sql & " INSERT INTO DocIndexValues (DocId,DocType,ColumnId,ColValue) (SELECT " & pDocId & ",DocType,ColumnId,ColValue " & _
                                    "FROM DocIndexValues WHERE DocId = " & pParentDocId & ")"
            End If


            objCommand.pCommandText = s_sql
            objCommand.ExecTranNonQuery()


            pSeqNo = "0"
            AddToInbox(objCommand) ', pExistsInInbox)
            'Return retval.pParam.Value

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
#End Region
#Region "Inbox Folder Functionality"
    Public Sub SaveUserFolder()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()

            Dim s_sql As String
            s_sql = "INSERT INTO UserFolder " & _
           "(UserId " & _
           ",FolderId " & _
           ",FolderDesc " & _
            ",CreatedDate) " & _
        "VALUES  " & _
           "('" & Replace(pUserId, "'", "''") & "' " & _
           ",'" & pFolderId & "' " & _
           ",'" & Replace(pFolderDesc, "'", "''") & "' " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ") "


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

    Public Sub UpdateUserFolder()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()

            Dim s_sql As String
            s_sql = "UPDATE UserFolder SET FolderDesc = '" & pFolderDesc & "'" & _
           " WHERE folderId = '" & pFolderId & "' AND userid = '" & pUserId & "'"


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

    Public Function RetrieveUserFolder() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT u.folderId,u.FolderDesc," & IIf(DocSession.OraClient, "NVL(", "ISNULL(") & "COUNT(di.FolderId),0) as cnt FROM UserFolder u LEFT JOIN docInbox di ON u.FolderId = di.FolderId WHERE u.UserId = '" & Replace(pUserId, "'", "''") & "' GROUP BY u.FolderId,u.FolderDesc ORDER BY u.folderdesc"
            ldata = objCommand.ExecData

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

    Public Sub DeleteUserFolders(ByVal objCommand As clsSqlConn)

        Try
            Dim s_sql As String = "Delete From UserFolder " & _
     "WHERE " & _
           "FolderId = " & pFolderId & " " & _
            "AND UserId = '" & Replace(pUserId, "'", "''") & "' " & _
            " Update DocInbox Set FolderId = null where folderid = '" & pFolderId & "' and UserId = '" & pUserId & "' "
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

#End Region
#Region "Issuance Folder Functionality"
    Public Sub SaveIssuanceFolder()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()

            Dim s_sql As String
            s_sql = "INSERT INTO IssuanceFolder " & _
           "(UserId " & _
           ",FolderId " & _
           ",FolderDesc " & _
            ",CreatedDate) " & _
        "VALUES  " & _
           "('" & Replace(pUserId, "'", "''") & "' " & _
           ",'" & pFolderId & "' " & _
           ",'" & Replace(pFolderDesc, "'", "''") & "' " & _
           ",GETDATE()) "


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

    Public Sub UpdateIssuanceFolder()

        Dim objCommand As clsSqlConn

        Try

            objCommand = New clsSqlConn()

            Dim s_sql As String
            s_sql = "UPDATE IssuanceFolder SET FolderDesc = '" & pFolderDesc & "'" & _
           " WHERE folderId = '" & pFolderId & "' AND userid = '" & pUserId & "'"


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
    Public Sub MoveDocIssuanceFolder(ByVal objCommand As clsSqlConn) ', ByVal aExistsInInbox As Boolean)


        Try
            objCommand.CommandType = CommandType.Text
            'oracle george
            
            objCommand.CommandText = "UPDATE DocIssuances SET FolderId='" & pFolderId & "'" & _
                        " WHERE docId = " & pDocId & " and UserId = '" & pUserId & "' " & _
                        " if @@rowcount = 0 " & _
                        "INSERT INTO DocIssuances(docId,userid,AddDate,FolderId) " & _
                        "VALUES (" & pDocId & ",'" & pUserId & "','" & DateTime.Now & "','" & pFolderId & "' ) "




            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub
    Public Function RetrieveIssuanceFolder() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT u.folderId,u.FolderDesc," & IIf(DocSession.OraClient, "NVL(", "ISNULL(") & "COUNT(di.FolderId),0) as cnt FROM IssuanceFolder u " & _
                "LEFT JOIN docIssuances di ON u.FolderId = di.FolderId GROUP BY u.FolderId,u.FolderDesc ORDER BY u.folderdesc"
            ldata = objCommand.ExecData

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

    Public Sub DeleteIssuanceFolders(ByVal objCommand As clsSqlConn)

        Try
            Dim s_sql As String = "Delete From IssuanceFolder " & _
     "WHERE " & _
           "FolderId = " & pFolderId & " " & _
            "AND UserId = '" & Replace(pUserId, "'", "''") & "'" & _
            " Update DocIssuances Set FolderId = null where folderid = '" & pFolderId & "' and UserId = '" & pUserId & "' "
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

#End Region


#Region "Dashboard Graph Functions"
    Public Function DocCompleteCount() As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT count(dl.docid) " & _
                                                    "FROM doclist dl " & _
                                                    "INNER JOIN docstatus ds ON	" & _
                                                        "ds.statusid = dl.statusId " & _
                                                    "INNER JOIN users u " & _
                                                        "ON dl.createdby = u.userid	" & _
                                                    "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "

            If pCreatedDateFrom <> "" Then
                s_sql = s_sql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
            End If

            If pCreatedDateTo <> "" Then
                s_sql = s_sql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
            End If

            If pOfficeCode <> "" Then
                s_sql = s_sql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
#End Region
#Region "Dashboard Functions"

    Public Function DocRequestList(ByVal asAction As String) As DataTable
        Dim s_sql, s_where As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            s_sql = "SELECT " & _
                "dl.docid,	" & _
         "dl.refno,	" & _
         "dl.title,	" & _
         "dl.createddate, " & _
            "dl.DateCompleted, "

            s_sql = s_sql & " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc, "

            If DocSession.OraClient Then
                'If asAction = "P" Then
                '    s_sql = s_sql & " (trunc(sysdate) - trunc(dl.createddate)) as age, "
                'Else
                s_sql = s_sql & " (trunc(nvl(dl.completeddate,getdate())) - trunc(dl.createddate)) as age, "
                'End If

                s_sql = s_sql & "d.personnelincharge AS assignedto, " & _
                            "TO_CHAR(dl.createddate,'mm/dd/yyyy') AS cdate, " & _
                            "NVL(dl.DueDate,' ') AS duedt, " & _
                            "NVL(o.Description,' ') AS ofc, " & _
                            " ds.description as docstat "
            Else
                's_sql = s_sql & "(isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _

                'If asAction = "P" Then
                '    s_sql = s_sql & " datediff(day,dl.createddate,getdate()) as age, "
                'Else
                s_sql = s_sql & " dl.age, "
                'End If

                s_sql = s_sql & "dl.personnelincharge AS assignedto, " & _
                            "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                            "isnull(dl.DueDate,'') AS duedt, " & _
                            "isnull(o.Description,'') AS ofc, " & _
                            "ds.description as docstat "
            End If

            s_sql = s_sql & "FROM (" & RetrieveRequestListSQL(asAction) & ") dl " & _
                "INNER JOIN docstatus ds ON	" & _
              "ds.statusid = dl.statusId " & _
            "INNER JOIN users u " & _
              "ON dl.createdby = u.userid	"

            s_sql = s_sql & "LEFT JOIN office o " & _
              "ON dl.OfficeCode = o.OfficeCode	" & _
              "LEFT JOIN DocRequestType rt " & _
              "ON rt.RequestType = dl.RequestType	"

            If pSortCol = "Age" Then
                s_sql = s_sql & " ORDER by dl.age " & pSortOrder
            ElseIf pSortCol = "Personnel In-Charge" OrElse pSortCol = "In-Charge" Then
                s_sql = s_sql & " ORDER by dl.PersonnelInCharge " & pSortOrder
            Else
                s_sql = s_sql & " ORDER by " & OrderByReqList() & " " & pSortOrder
            End If



            DocSession.sRegType2 = DocRequestListReport(asAction)
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            ldata = objCommand.ExecData

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

    Public Function DocRequestListAdmin(ByVal asAction As String) As DataTable
        Dim s_sql, s_where As String
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            s_sql = "SELECT " & _
                "dl.docid,	" & _
         "dl.refno,	" & _
         "dl.title,	" & _
         "dl.createddate, " & _
            "dl.DateCompleted, "

            s_sql = s_sql & " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc, "


            s_sql = s_sql & " dl.age, "
            'End If

            s_sql = s_sql & "dl.personnelincharge AS assignedto, " & _
                        "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                        "isnull(dl.DueDate,'') AS duedt, " & _
                        "isnull(o.Description,'') AS ofc, " & _
                        "ds.description as docstat "

            s_sql = s_sql & "FROM (" & RetrieveRequestListSQLAdmin(asAction) & ") dl " & _
                "INNER JOIN docstatus ds ON	" & _
              "ds.statusid = dl.statusId " & _
            "INNER JOIN users u " & _
              "ON dl.createdby = u.userid	"

            s_sql = s_sql & "LEFT JOIN office o " & _
              "ON dl.OfficeCode = o.OfficeCode	" & _
              "LEFT JOIN DocRequestType rt " & _
              "ON rt.RequestType = dl.RequestType	"

            If pSortCol = "Age" Then
                s_sql = s_sql & " ORDER by dl.age " & pSortOrder
            ElseIf pSortCol = "Personnel In-Charge" OrElse pSortCol = "In-Charge" Then
                s_sql = s_sql & " ORDER by dl.PersonnelInCharge " & pSortOrder
            Else
                s_sql = s_sql & " ORDER by " & OrderByReqList() & " " & pSortOrder
            End If



            DocSession.sRegType2 = DocRequestListReport(asAction)
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            ldata = objCommand.ExecData

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

    Public Function DocRequestListReport(ByVal asAction As String) As String
        Dim s_sql, s_sql2, s_where As String
        'Dim objCommand As clsSqlConn
        'Dim ldata As DataTable
        Try
            s_sql = "SELECT " & _
                "dl.docid,	" & _
         "dl.refno,	" & _
         "dl.title,	" & _
         "dl.createddate, " & _
            "dl.DateCompleted, "

            s_sql = s_sql & " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc, "

            If DocSession.OraClient Then
                'If asAction = "P" Then
                '    s_sql = s_sql & " (trunc(sysdate) - trunc(dl.createddate)) as age, "
                'Else
                s_sql = s_sql & " (trunc(nvl(dl.completeddate,getdate())) - trunc(dl.createddate)) as age, "
                'End If

                s_sql = s_sql & "d.personnelincharge AS assignedto, " & _
                            "TO_CHAR(dl.createddate,'mm/dd/yyyy') AS cdate, " & _
                            "NVL(dl.DueDate,' ') AS duedt, " & _
                            "NVL(o.Description,' ') AS ofc, " & _
                            " ds.description as docstat "
            Else
                's_sql = s_sql & "(isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _

                'If asAction = "P" Then
                '    s_sql = s_sql & " datediff(day,dl.createddate,getdate()) as age, "
                'Else
                s_sql = s_sql & " dl.age, "
                'End If

                s_sql = s_sql & "dl.personnelincharge AS assignedto, " & _
                            "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                            "isnull(dl.DueDate,'') AS duedt, " & _
                            "isnull(o.Description,'') AS ofc, " & _
                            "ds.description as docstat "
            End If

            s_sql = s_sql & "FROM (" & RetrieveRequestListReport(asAction) & ") dl " & _
                "INNER JOIN docstatus ds ON	" & _
              "ds.statusid = dl.statusId " & _
            "INNER JOIN users u " & _
              "ON dl.createdby = u.userid	"

            s_sql = s_sql & "LEFT JOIN office o " & _
              "ON dl.OfficeCode = o.OfficeCode	" & _
              "LEFT JOIN DocRequestType rt " & _
              "ON rt.RequestType = dl.RequestType	"

            If pSortCol = "Age" Then
                s_sql = s_sql & " ORDER by dl.age " & pSortOrder
            ElseIf pSortCol = "Personnel In-Charge" OrElse pSortCol = "In-Charge" Then
                s_sql = s_sql & " ORDER by dl.PersonnelInCharge " & pSortOrder
            Else
                s_sql = s_sql & " ORDER by " & OrderByReqList() & " " & pSortOrder
            End If

            Return s_sql

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If
        End Try



    End Function


    Private Function RetrieveRequestListSQL(ByVal asAction As String) As String
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1
        Dim sOrder As String
        sOrder = OrderByReqList()
        Dim s_sql As String = " SELECT dl.* FROM ( SELECT "
        If Not DocSession.OraClient Then
            s_sql = s_sql & " TOP " & lTop.ToString & " "
        End If

        s_sql = s_sql & "row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") as rn" & _
                                                        ",dl.docid " & _
                                                        ",dl.refno " & _
                                                        ",dl.title " & _
                                                        ",dl.CreatedBy " & _
                                                        ",dl.CreatedDate " & _
                                                        ",dl.CompletedDate " & _
                                                        ",dl.StatusId " & _
                                                        ",dl.DueDate " & _
                                                        ",dl.OfficeCode " & _
                                                        ",dl.RequestType " & _
                                                        ",datediff(day,dl.createddate,isnull(div.colvalue,isnull(dl.completeddate,getdate()))) as Age " & _
                                                        ",dl.routingseqno, " & _
                                                        "'Date Completed: '+isnull(div.colvalue,isnull(convert(char(10),dl.completeddate,101),'N/A')) as DateCompleted "

        If DocSession.OraClient Then
            s_sql = s_sql & ", NVL(u2.FirstName,u.FirstName)||' '||NVL(u2.LastName,u.LastName) as personnelincharge "

        Else
            s_sql = s_sql & ",personnelincharge = isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) "
        End If

        s_sql = s_sql & " FROM doclist dl " & _
                                                    "INNER JOIN docstatus ds ON	" & _
                                                        "ds.statusid = dl.statusId "
        'If pPersonnelInCharge <> "" Then
        s_sql = s_sql & "INNER JOIN users u " & _
                "ON dl.CreatedBy = u.userid	"
        If DocSession.sUserRole = "U" Then
            s_sql = s_sql & "INNER JOIN docRouting dr ON	" & _
                "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                " and dr.ApproverId = '" & DocSession.sUserId & "'" & _
            "INNER JOIN users u2 " & _
                "ON dr.ApproverId = u2.userid	"
        Else
            s_sql = s_sql & "LEFT JOIN docRouting dr ON	" & _
                "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
            "LEFT JOIN users u2 " & _
                "ON dr.ApproverId = u2.userid	"
        End If

        s_sql = s_sql & "LEFT JOIN (" & _
                        "select div.DocId,div.DocType,case when div.colvalue = '' then null when isdate(div.colvalue) = 1 then div.colvalue else null end as colvalue from docindex di INNER JOIN docindexvalues div ON  " & _
                "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                        "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                ") " & _
                " div ON div.docid=dl.docId "
        'End If

        If pStatusId <> "" AndAlso pStatusId <> "0" Then
            s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
        Else
            If asAction = "P" Then
                s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "
            ElseIf asAction = "C" Then
                s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "
            Else
                s_sql = s_sql & "WHERE dl.statusid <> 5 "
            End If
        End If

        s_sql = DashBoardWhereClause(s_sql)

        s_sql = s_sql & ") dl "


        If pIdx <> "" Then
            s_sql = s_sql & " WHERE (rn between " & pIdx & " and " & pIdx & "+" & pRowsPerPage & ") "
        End If

        Return s_sql
    End Function

    Private Function RetrieveRequestListSQLAdmin(ByVal asAction As String) As String
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1
        Dim sOrder As String
        sOrder = OrderByReqList()
        Dim s_sql As String = " SELECT dl.* FROM ( SELECT "
        If Not DocSession.OraClient Then
            s_sql = s_sql & " TOP " & lTop.ToString & " "
        End If

        s_sql = s_sql & "row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") as rn" & _
                                                        ",dl.docid " & _
                                                        ",dl.refno " & _
                                                        ",dl.title " & _
                                                        ",dl.CreatedBy " & _
                                                        ",dl.CreatedDate " & _
                                                        ",dl.CompletedDate " & _
                                                        ",dl.StatusId " & _
                                                        ",dl.DueDate " & _
                                                        ",dl.OfficeCode " & _
                                                        ",dl.RequestType " & _
                                                        ",datediff(day,dl.createddate,isnull(div.colvalue,isnull(dl.completeddate,getdate()))) as Age " & _
                                                        ",dl.routingseqno, " & _
                                                        "'Date Completed: '+isnull(div.colvalue,isnull(convert(char(10),dl.completeddate,101),'N/A')) as DateCompleted "


        s_sql = s_sql & ",personnelincharge = isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) "

        s_sql = s_sql & " FROM doclist dl "
        s_sql = s_sql & DocPendingCountUserSuperSql()
        s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
        "ds.statusid = dl.statusId "

        s_sql = s_sql & "INNER JOIN users u " & _
                "ON dl.CreatedBy = u.userid	" & _
        IIf(pPersonnelInCharge <> "", DashBoardInnerClauseAdmin(), DashBoardLeftClauseAdmin())

        's_sql = s_sql & "LEFT JOIN docRouting dr ON	" & _
        '        "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
        '    "LEFT JOIN users u2 " & _
        '        "ON dr.ApproverId = u2.userid	"


        s_sql = s_sql & "LEFT JOIN (" & _
                        "select div.DocId,div.DocType,case when div.colvalue = '' then null when isdate(div.colvalue) = 1 then div.colvalue else null end as colvalue from docindex di INNER JOIN docindexvalues div ON  " & _
                "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                        "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                ") " & _
                " div ON div.docid=dl.docId "


        If pStatusId <> "" AndAlso pStatusId <> "0" Then
            s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
        Else
            If asAction = "P" Then
                s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "
            ElseIf asAction = "C" Then
                s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "
            Else
                s_sql = s_sql & "WHERE dl.statusid <> 5 "
            End If
        End If

        s_sql = DashBoardWhereClauseAdmin(s_sql)

        s_sql = s_sql & ") dl "


        If pIdx <> "" Then
            s_sql = s_sql & " WHERE (rn between " & pIdx & " and " & pIdx & "+" & pRowsPerPage & ") "
        End If

        Return s_sql
    End Function

    Private Function RetrieveRequestListReport(ByVal asAction As String) As String
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1
        Dim sOrder As String
        sOrder = OrderByReqList()
        Dim s_sql As String = " SELECT dl.* FROM ( SELECT "
        'If Not DocSession.OraClient Then
        '    s_sql = s_sql & " TOP " & lTop.ToString & " "
        'End If

        s_sql = s_sql & "row_number() over (ORDER BY " & sOrder & " " & pSortOrder & ") as rn" & _
                                                        ",dl.docid " & _
                                                        ",dl.refno " & _
                                                        ",dl.title " & _
                                                        ",dl.CreatedBy " & _
                                                        ",dl.CreatedDate " & _
                                                        ",dl.CompletedDate " & _
                                                        ",dl.StatusId " & _
                                                        ",dl.DueDate " & _
                                                        ",dl.OfficeCode " & _
                                                        ",dl.RequestType " & _
                                                        ",datediff(day,dl.createddate,isnull(div.colvalue,isnull(dl.completeddate,getdate()))) as Age " & _
                                                        ",dl.routingseqno, " & _
                                                        "'Date Completed: '+isnull(div.colvalue,isnull(convert(char(10),dl.completeddate,101),'N/A')) as DateCompleted "

        If DocSession.OraClient Then
            s_sql = s_sql & ", NVL(u2.FirstName,u.FirstName)||' '||NVL(u2.LastName,u.LastName) as personnelincharge "

        Else
            s_sql = s_sql & ",personnelincharge = isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) "
        End If

        s_sql = s_sql & " FROM doclist dl " & _
                                                    "INNER JOIN docstatus ds ON	" & _
                                                        "ds.statusid = dl.statusId "
        'If pPersonnelInCharge <> "" Then
        s_sql = s_sql & "INNER JOIN users u " & _
                "ON dl.CreatedBy = u.userid	" & _
            "LEFT JOIN docRouting dr ON	" & _
                "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
            "LEFT JOIN users u2 " & _
                "ON dr.ApproverId = u2.userid	" & _
            "LEFT JOIN (" & _
                            "select div.DocId,div.DocType,case when div.colvalue = '' then null when isdate(div.colvalue) = 1 then div.colvalue else null end as colvalue from docindex di INNER JOIN docindexvalues div ON  " & _
                    "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                            "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                    ") " & _
                    " div ON div.docid=dl.docId "
        'End If

        If pStatusId <> "" AndAlso pStatusId <> "0" Then
            s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
        Else
            If asAction = "P" Then
                s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "
            ElseIf asAction = "C" Then
                s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "
            Else
                s_sql = s_sql & "WHERE dl.statusid <> 5 "
            End If
        End If

        s_sql = DashBoardWhereClause(s_sql)

        s_sql = s_sql & ") dl "


        Return s_sql
    End Function
    Private Function DashBoardInnerJoin() As String
        Dim s_sql As String = ""
        If pStatusId <> "" AndAlso pStatusId <> "0" Then

            s_sql = s_sql & "INNER JOIN docstatus ds ON	" & _
                             "ds.statusid = dl.statusId "
        End If
        If DocSession.sUserRole = "U" Then
            s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.createdby = u.userid	" & _
                    "INNER JOIN docRouting dr ON	" & _
                " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0) AND " & _
                " dr.approverid = '" & DocSession.sUserId & "' " & _
                "INNER JOIN users u2 " & _
                            "  ON dr.approverid = u2.userid	"

        Else
            If pPersonnelInCharge <> "" Then
                s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.createdby = u.userid	" & _
                    "LEFT JOIN docRouting dr ON	" & _
                " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
                "LEFT JOIN users u2 " & _
                            "  ON dr.approverid = u2.userid	" &
                            " AND u2.Firstname+' '+u2.LastName like '%" & pPersonnelInCharge & "%' "
            End If
        End If

        Return s_sql
    End Function
    Private Function DashBoardWhereClause(ByVal asSql As String) As String
        If pSubject <> "" Then
            's_sql = s_sql + " WHERE "
            asSql = asSql & " AND dl.title like  '%" & Subject & "%'"
        End If

        If pCreatedDateFrom <> "" Then
            If DocSession.OraClient Then
                asSql = asSql & " AND trunc(dl.createddate) >= TO_DATE('" & pCreatedDateFrom & "','mm/dd/yyyy')"
            Else
                asSql = asSql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
            End If

        End If

        If pCreatedDateTo <> "" Then
            If DocSession.OraClient Then
                asSql = asSql & " AND dl.trunc(createddate) >= TO_DATE('" & pCreatedDateFrom & "','mm/dd/yyyy')"
            Else
                asSql = asSql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
            End If

        End If

        If pOfficeCode <> "" Then
            asSql = asSql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
        End If

        If pDueDate <> "" Then
            If DocSession.OraClient Then
                asSql = asSql & " AND trunc(dl.duedate) = TO_DATE('" & pDueDate & "','mm/dd/yyyy')"
            Else
                asSql = asSql & " AND convert(char(10),dl.duedate,'mm/dd/yyyy') = convert(datetime,'" & pDueDate & "')"
            End If

        End If

        If pRefNo <> "" Then
            asSql = asSql & " AND dl.refno = '" & pRefNo & "'"
        End If

        If pStatusId <> "" AndAlso pStatusId <> "0" Then
            asSql = asSql & " AND dl.StatusId = '" & pStatusId & "'"
        End If
        'personnel in charge should be in inner join
        If pPersonnelInCharge <> "" Then
            If DocSession.OraClient Then
                asSql = asSql & " AND NVL(u2.FirstName,u.FirstName)||' '||NVL(u2.LastName,u.LastName) like '%" & pPersonnelInCharge & "%' "
            Else
                asSql = asSql & " AND isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) like '%" & pPersonnelInCharge & "%' "
            End If
        End If
        Return asSql

    End Function
    Private Function DashBoardWhereClauseAdmin(ByVal asSql As String) As String
        If pSubject <> "" Then

            asSql = asSql & " AND dl.title like  '%" & Subject & "%'"
        End If

        If pCreatedDateFrom <> "" Then
            asSql = asSql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
        End If

        If pCreatedDateTo <> "" Then
            asSql = asSql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
        End If

        If pOfficeCode <> "" Then
            asSql = asSql & " AND dl.OfficeCode = '" & pOfficeCode & "'"
        End If

        If pDueDate <> "" Then
            If DocSession.OraClient Then
                asSql = asSql & " AND trunc(dl.duedate) = TO_DATE('" & pDueDate & "','mm/dd/yyyy')"
            Else
                asSql = asSql & " AND convert(char(10),dl.duedate,'mm/dd/yyyy') = convert(datetime,'" & pDueDate & "')"
            End If

        End If

        If pRefNo <> "" Then
            asSql = asSql & " AND dl.refno = '" & pRefNo & "'"
        End If

        If pStatusId <> "" AndAlso pStatusId <> "0" Then
            asSql = asSql & " AND dl.StatusId = '" & pStatusId & "'"
        End If

        Return asSql

    End Function
    Private Function DashBoardInnerClauseAdmin() As String
        Dim asSql As String = ""
        'personnel in charge should be in inner join
        If pPersonnelInCharge <> "" Then
            asSql = asSql & " INNER JOIN docRouting dr ON	" & _
                " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0) " & _
                " INNER JOIN users u2 " & _
                            "  ON dr.approverid = u2.userid	" & _
                " AND isnull(u2.FirstName,'')+' '+isnull(u2.LastName,'') like '%" & pPersonnelInCharge & "%' "
        End If

        Return asSql

    End Function
    Private Function DashBoardLeftClauseAdmin() As String
        Dim asSql As String = ""
        'personnel in charge should be in inner join

        asSql = asSql & " LEFT JOIN docRouting dr ON	" & _
            " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0) " & _
            " LEFT JOIN users u2 " & _
                        "  ON dr.approverid = u2.userid	"


        Return asSql

    End Function
    Public Function DocPendingCount(ByVal asAction As String) As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try
            If DocSession.OraClient Then
                s_sql = " SELECT count(d.docid) " & _
                                                    "FROM (SELECT dl.docId, NVL(u2.FirstName,u.FirstName)||' '||NVL(u2.LastName,u.LastName) as PersonnelInCharge FROM doclist dl " & _
                                                    "INNER JOIN docstatus ds ON	" & _
                                                        "ds.statusid = dl.statusId " & _
                                                    "INNER JOIN users u " & _
                                                        "ON dl.createdby = u.userid	"

            Else
                s_sql = " SELECT count(d.docid) " & _
                                                    "FROM (SELECT dl.docId, isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) as PersonnelInCharge FROM doclist dl " & _
                                                    "INNER JOIN docstatus ds ON	" & _
                                                        "ds.statusid = dl.statusId " & _
                                                    "INNER JOIN users u " & _
                                                        "ON dl.createdby = u.userid	"

            End If
            If DocSession.sUserRole = "U" Then
                s_sql = s_sql & " INNER JOIN docRouting dr ON	" & _
                " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0) " & _
                "   And dr.approverid = '" & DocSession.sUserId & "' " & _
                " INNER JOIN users u2 " & _
                            "  ON dr.approverid = u2.userid	"
            Else
                s_sql = s_sql & " LEFT JOIN docRouting dr ON	" & _
        " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
                " LEFT JOIN users u2 " & _
                            "  ON dr.approverid = u2.userid	"
            End If




            If pStatusId <> "" AndAlso pStatusId <> "0" Then
                s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
            Else
                If asAction = "P" Then
                    s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "
                ElseIf asAction = "C" Then
                    s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "
                Else
                    s_sql = s_sql & "WHERE dl.statusid <> 5 "
                End If
            End If

            s_sql = s_sql & " AND (dl.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    " OR dl.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl.CreatedBy = '" & DocSession.sUserId & "' " & _
    " OR dr.ApproverId = '" & DocSession.sUserId & "' )  "

            'If pCreatedDateFrom <> "" Then
            '    s_sql = s_sql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
            'End If

            'If pCreatedDateTo <> "" Then
            '    s_sql = s_sql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
            'End If

            'If pOfficeCode <> "" Then
            '    s_sql = s_sql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
            'End If

            s_sql = DashBoardWhereClause(s_sql)
            s_sql = s_sql & ") d "


            If DocSession.sUserRole = "U" Then

                s_sql = s_sql & " WHERE d.PersonnelInCharge like '%" & pPersonnelInCharge & "%' "

            Else
                If pPersonnelInCharge <> "" Then
                    s_sql = s_sql & " WHERE d.PersonnelInCharge like '%" & pPersonnelInCharge & "%' "
                End If
            End If

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function DocPendingCountAdmin(ByVal asAction As String) As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT count(dl.docid) FROM doclist dl "

            s_sql = s_sql & DocPendingCountUserSuperSql()

            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
                        "ds.statusid = dl.statusId " & _
                    "INNER JOIN users u " & _
                        "ON dl.createdby = u.userid	" & _
                        DashBoardInnerClauseAdmin() & " "


            If pStatusId <> "" AndAlso pStatusId <> "0" Then
                s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
            Else
                If asAction = "P" Then
                    s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "
                ElseIf asAction = "C" Then
                    s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "
                Else
                    s_sql = s_sql & "WHERE dl.statusid <> 5 "
                End If
            End If

            s_sql = DashBoardWhereClauseAdmin(s_sql)

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception("dcpa." & ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function DocPendingCountUserSuperSql() As String
        Dim s_sql As String = ""


        Try
            If DocSession.sUserRole <> "A" AndAlso DocSession.sUserRole <> "G" Then


                s_sql = " INNER JOIN ( SELECT dl.docid FROM doclist dl "
                s_sql = s_sql & "WHERE dl.statusid <> 5 "

                If DocSession.sUserRole = "U" OrElse DocSession.sUserRole = "R" Then
                    s_sql = s_sql & sqlDocListUserWhere
                ElseIf DocSession.sUserRole = "D" Then
                    s_sql = s_sql & sqlDocListSuperUserWhere
                End If

                s_sql = s_sql & " UNION "

                s_sql = s_sql & " " & sqlDocRouting
                s_sql = s_sql & ") dlx on dlx.docid = dl.docid"
            End If
            Return s_sql
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Function DocPendingReportSql(ByVal asOfc As String, Optional ByVal bIncludeArchiver As Boolean = False) As String
        Dim s_sql As String = ""
        Dim sqlRouting As String = "SELECT dl.docid FROM doclist dl inner join docrouting dr on dl.docid = dr.docid " & _
                                    "INNER JOIN users u ON u.userid = dr.approverid " & _
                                    "INNER JOIN groups g ON g.groupid = u.usergroup " & _
                                    " WHERE dl.statusid <> 5 and ((g.OfficeCode = '" & Replace(asOfc, "'", "''") & "' and (dl.Confidential is null or dl.Confidential = 0)) " & _
                                    " OR (dl.Confidential = 1 and dr.approverid =  '" & Replace(DocSession.sUserId, "'", "''") & "')) "
        Dim sqlRoutingAdmin As String = "SELECT dl.docid FROM doclist dl inner join docrouting dr on dl.docid = dr.docid " & _
                                    "INNER JOIN users u ON u.userid = dr.approverid " & _
                                    "INNER JOIN groups g ON g.groupid = u.usergroup " & _
                                    " WHERE dl.statusid <> 5 and (g.OfficeCode = '" & Replace(asOfc, "'", "''") & "')"
        Try
            'If DocSession.sUserRole <> "A" Then
            If asOfc <> "" Then
                '" OR dl.ArchiverOfc = '" & asOfc & "'" & _
                s_sql = " INNER JOIN ( SELECT dl.docid FROM doclist dl "
                s_sql = s_sql & "WHERE dl.statusid <> 5 and (( " & _
                " (dl.OfficeCode = '" & asOfc & "'"
                If bIncludeArchiver Then
                    s_sql = s_sql & " OR dl.ArchiverOfc = '" & asOfc & "'"
                End If
                s_sql = s_sql & " OR dl.UploaderOfc = '" & asOfc & "') "
                If DocSession.sUserRole = "A" Then
                    s_sql = s_sql & " )) "
                Else
                    s_sql = s_sql & " AND (dl.Confidential is null or dl.Confidential = 0)) OR (dl.Confidential=1 and dl.CreatedBy ='" & Replace(DocSession.sUserId, "'", "''") & "')) "
                End If

                s_sql = s_sql & " UNION "

                If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
                    s_sql = s_sql & " " & sqlRouting
                ElseIf DocSession.sUserRole = "A" Then
                    s_sql = s_sql & " " & sqlRoutingAdmin
                Else
                    s_sql = s_sql & " " & sqlDocRouting
                End If

                s_sql = s_sql & ") dlx on dlx.docid = dl.docid "

            End If
            Return s_sql
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Function DocListSQL(ByVal asOfc, ByVal asGroup, ByVal asUserId) As String
        Dim s_sql As String = ""
        Dim sql_UserWhere As String = " AND (((dl.ArchiverGrp = '" & asGroup & "' OR dl.OfficeCode = '" & asOfc & "'" & _
                            " OR dl.UploaderGrp = '" & asGroup & "') and (dl.confidential is null or dl.confidential = 0) )" & _
                            " OR dl.CreatedBy = '" & asUserId & "'" & _
                            ")"

        Dim sql_SuperUserWhere As String = " AND (((dl.ArchiverGrp = '" & asGroup & "'" & _
                                " OR dl.UploaderGrp = '" & asGroup & "'" & _
                                " OR dl.OfficeCode = '" & asOfc & "'" & _
                                " OR dl.ArchiverOfc = '" & asOfc & "'" & _
                                " OR dl.UploaderOfc = '" & asOfc & "' ) and (dl.confidential is null or dl.confidential = 0) )" & _
                                " OR dl.CreatedBy = '" & asUserId & "'" & _
                                ")"


        Try
            If DocSession.sUserRole <> "A" Then


                s_sql = " INNER JOIN ( SELECT dl.docid FROM doclist dl "
                s_sql = s_sql & "WHERE dl.statusid <> 5 "

                If DocSession.sUserRole = "U" OrElse DocSession.sUserRole = "R" Then
                    s_sql = s_sql & sql_UserWhere
                ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
                    s_sql = s_sql & sql_SuperUserWhere
                End If

                s_sql = s_sql & " UNION "

                s_sql = s_sql & " " & sqlDocRouting
                s_sql = s_sql & ") dlx on dlx.docid = dl.docid"
            End If
            Return s_sql
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Function DocPendingCountPerDocType() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT dt.DocName,count(dl.doctype) as pendingcount,dl.docType FROM doclist dl  "

            s_sql = s_sql & DocPendingCountUserSuperSql()

            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
            "ds.statusid = dl.statusId " & _
            " INNER JOIN doctype dt " & _
              " ON dt.DocType = dl.doctype "

            s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.CreatedBy = u.userid	" & _
            IIf(pPersonnelInCharge <> "", DashBoardInnerClauseAdmin(), DashBoardLeftClauseAdmin())


            s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.StatusId NOT in (" & DocSession.CompleteStatus & ") "

            s_sql = DashBoardWhereClauseAdmin(s_sql)

            s_sql = DashBoardWhereClause(s_sql)
            s_sql = s_sql & " Group BY dt.DocName,dl.doctype "

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    'not affected by Filter
    Public Function DocPendingDistribution() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try
            If DocSession.OraClient Then
                s_sql = " SELECT mg.Description,COUNT(dl.docid) as doccount FROM doclist dl " & _
            " INNER JOIN docRouting dr ON	" & _
            " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
            " INNER JOIN users u2 " & _
             " ON dr.approverid = u2.userid" & _
            " INNER JOIN Groups G" & _
            " ON u2.UserGroup=g.GroupId" & _
            " INNER JOIN MainGroup MG" & _
            " ON G.MainGroupId = MG.MainGroupId" & _
            " WHERE dl.StatusId not in (" & DocSession.CompleteStatus & ") and dl.StatusId <> 5 "



                If pCreatedDateFrom <> "" Then
                    s_sql = s_sql & " AND dl.createddate >= TO_DATE('" & pCreatedDateFrom & "','mm/dd/yyyy')"
                End If

                If pCreatedDateTo <> "" Then
                    s_sql = s_sql & " AND dl.createddate < (TO_DATE('" & pCreatedDateTo & "','mm/dd/yyyy')+1) "
                End If

                If pOfficeCode <> "" Then
                    s_sql = s_sql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
                End If
                s_sql = s_sql & " group by mg.description"
            Else
                s_sql = " SELECT mg.Description,COUNT(dl.docid) as doccount FROM doclist dl " & _
                " LEFT JOIN docRouting dr ON	" & _
                " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
                " INNER JOIN users u2 " & _
                 " ON dr.approverid = u2.userid" & _
                " INNER JOIN Groups G" & _
                " ON u2.UserGroup=g.GroupId" & _
                " INNER JOIN MainGroup MG" & _
                " ON G.MainGroupId = MG.MainGroupId" & _
                " WHERE dl.StatusId <> 5 and dl.StatusId not in (" & DocSession.CompleteStatus & ")"

                If pCreatedDateFrom <> "" Then
                    s_sql = s_sql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
                End If

                If pCreatedDateTo <> "" Then
                    s_sql = s_sql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
                End If

                If pOfficeCode <> "" Then
                    s_sql = s_sql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
                End If
                s_sql = s_sql & " group by mg.description"
            End If
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function DocCompleted() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select " & _
            "isnull(avg(cast(dateDiff(DAY,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as aday," & _
            "isnull(avg(cast(dateDiff(MI,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as amin," & _
            "isnull(avg(cast(dateDiff(hh,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as ahour," & _
            "isnull(max(cast(dateDiff(DAY,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as mday," & _
            "isnull(max(cast(dateDiff(MI,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as mmin," & _
            "isnull(max(cast(dateDiff(hh,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as mhour," & _
            "isnull(min(cast(dateDiff(DAY,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as nday," & _
            "isnull(min(cast(dateDiff(MI,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as nmin," & _
            "isnull(min(cast(dateDiff(hh,dl.CreatedDate,dl.ApprovedDate) as bigint)),0) as nhour," & _
            "count(*) as cd " & _
            "from DocList dl " & DashBoardInnerJoin() & " where dl.StatusId in (" & DocSession.CompleteStatus & ") "
            s_sql = DashBoardWhereClause(s_sql)

            'If pCreatedDateFrom <> "" Then
            '    s_sql = s_sql & " AND dl.createddate >= convert(datetime,'" & pCreatedDateFrom & "')"
            'End If

            'If pCreatedDateTo <> "" Then
            '    s_sql = s_sql & " AND dl.createddate < dateadd(day,1,convert(datetime,'" & pCreatedDateTo & "'))"
            'End If

            'If pOfficeCode <> "" Then
            '    s_sql = s_sql & " AND dl.OfficeCode =  '" & pOfficeCode & "'"
            'End If


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function DocCompletedAdmin() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select " & _
            "isnull(avg(cast(dateDiff(DAY,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as aday," & _
            "isnull(avg(cast(dateDiff(MI,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as amin," & _
            "isnull(avg(cast(dateDiff(hh,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as ahour," & _
            "isnull(max(cast(dateDiff(DAY,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as mday," & _
            "isnull(max(cast(dateDiff(MI,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as mmin," & _
            "isnull(max(cast(dateDiff(hh,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as mhour," & _
            "isnull(min(cast(dateDiff(DAY,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as nday," & _
            "isnull(min(cast(dateDiff(MI,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as nmin," & _
            "isnull(min(cast(dateDiff(hh,dl.CreatedDate,isnull(div.colvalue,isnull(dl.CompletedDate,getdate()))) as bigint)),0) as nhour," & _
            "count(*) as cd "
            s_sql = s_sql & " FROM doclist dl "
            s_sql = s_sql & DocPendingCountUserSuperSql()
            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
            "ds.statusid = dl.statusId "

            s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.CreatedBy = u.userid	" & _
            IIf(pPersonnelInCharge <> "", DashBoardInnerClauseAdmin(), DashBoardLeftClauseAdmin())

            s_sql = s_sql & "LEFT JOIN (" & _
                        "select div.DocId,div.DocType,case when div.colvalue = '' then null when isdate(div.colvalue) = 1 then replace(div.colvalue,'-','/') else null end as colvalue from docindex di INNER JOIN docindexvalues div ON  " & _
                "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                        "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                ") " & _
                " div ON div.docid=dl.docId "



            If pStatusId <> "" AndAlso pStatusId <> "0" Then
                s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
            Else

                s_sql = s_sql & "WHERE dl.statusid in (" & DocSession.CompleteStatus & ") "

            End If

            s_sql = DashBoardWhereClauseAdmin(s_sql)


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception("dcoma." & ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function DocPending() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select " & _
            "isnull(avg(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as aday," & _
            "isnull(avg(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as amin," & _
            "isnull(avg(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as ahour," & _
            "isnull(max(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as mday," & _
            "isnull(max(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as mmin," & _
            "isnull(max(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as mhour," & _
            "isnull(min(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as nday," & _
            "isnull(min(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as nmin," & _
            "isnull(min(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as nhour," & _
            "count(*) as pd " & _
            "from DocList dl "
            s_sql = s_sql & DashBoardInnerJoin()
            s_sql = s_sql & " where dl.StatusId <> 5 and dl.StatusId not in (" & DocSession.CompleteStatus & ") "

            s_sql = DashBoardWhereClause(s_sql)


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function DocPendingAdmin() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select " & _
            "isnull(avg(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as aday," & _
            "isnull(avg(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as amin," & _
            "isnull(avg(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as ahour," & _
            "isnull(max(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as mday," & _
            "isnull(max(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as mmin," & _
            "isnull(max(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as mhour," & _
            "isnull(min(cast(dateDiff(DAY,dl.CreatedDate,getdate()) as bigint)),0) as nday," & _
            "isnull(min(cast(dateDiff(MI,dl.CreatedDate,getdate()) as bigint)),0) as nmin," & _
            "isnull(min(cast(dateDiff(hh,dl.CreatedDate,getdate()) as bigint)),0) as nhour," & _
            "count(*) as pd "

            s_sql = s_sql & " FROM doclist dl "
            s_sql = s_sql & DocPendingCountUserSuperSql()
            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
            "ds.statusid = dl.statusId "

            s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.CreatedBy = u.userid	" & _
            IIf(pPersonnelInCharge <> "", DashBoardInnerClauseAdmin(), DashBoardLeftClauseAdmin())





            If pStatusId <> "" AndAlso pStatusId <> "0" Then
                s_sql = s_sql & "WHERE dl.statusid = " & pStatusId
            Else

                s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid not in (" & DocSession.CompleteStatus & ") "

            End If

            s_sql = DashBoardWhereClauseAdmin(s_sql)







            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception("dpa." & ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function DocCompletionDays() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT dt.DocName,avg(ISNULL(CAST(dateDiff(day,dl.CreatedDate,dl.ApprovedDate) as bigint),0)) as dayscompleted,dl.doctype " & _
                "FROM doclist dl  "

            s_sql = s_sql & DocPendingCountUserSuperSql()

            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
            "ds.statusid = dl.statusId " & _
            " INNER JOIN doctype dt " & _
              " ON dt.DocType = dl.doctype "

            s_sql = s_sql & "INNER JOIN users u " & _
                    "ON dl.CreatedBy = u.userid	" & _
            IIf(pPersonnelInCharge <> "", DashBoardInnerClauseAdmin(), DashBoardLeftClauseAdmin())


            s_sql = s_sql & "WHERE dl.StatusId in (" & DocSession.CompleteStatus & ") "

            s_sql = DashBoardWhereClauseAdmin(s_sql)

            s_sql = s_sql & " Group BY dt.DocName,dl.doctype "

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function DocStatusCount(ByVal asStatusID As String, Optional ByVal asAction As String = "=") As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try


            s_sql = " SELECT count(dl.docid) " & _
                                                    "FROM doclist dl "
            s_sql = s_sql & DocPendingReportSql(pOfficeCode, True) 'DocPendingCountUserSuperSql()
            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
                        "ds.statusid = dl.statusId " & _
                    "INNER JOIN users u " & _
                        "ON dl.createdby = u.userid	" & _
                        DashBoardInnerClauseAdmin() & " "



            s_sql = s_sql & "WHERE dl.statusid <> 5 and dl.statusid " & asAction & " " & asStatusID


            s_sql = DashBoardWhereClauseAdmin(s_sql)



            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function DocAllCount() As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT count(dl.docid) " & _
                                                    "FROM doclist dl "
            s_sql = s_sql & DashBoardInnerJoin()

            s_sql = s_sql & " WHERE dl.statusid <> 5  "

            s_sql = DashBoardWhereClause(s_sql)

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function RetrieveTop10Pending() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select top 10 " & _
            "o.officecode,o.Description as bureau,isnull((max(dateDiff(DAY,dl.CreatedDate,getdate()))),0) as docage " & _
            "from DocList dl  " & _
            "inner join office o on  " & _
            "dl.officecode = o.officecode "

            s_sql = s_sql & DashBoardInnerJoin() & " where dl.StatusId <> 5 and dl.StatusId  not in (" & DocSession.CompleteStatus & ") "

            s_sql = DashBoardWhereClause(s_sql)

            s_sql = s_sql & "group by o.OfficeCode,o.Description " & _
            "order by docage desc"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function RetrieveTop10Quick() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " select top 10 " & _
            "o.officecode,o.Description as bureau,isnull(max(dateDiff(DAY,dl.CreatedDate,dl.approveddate)),0) as docage " & _
            "from DocList dl  " & _
            "inner join office o on  " & _
            "dl.officecode = o.officecode "
            s_sql = s_sql & DashBoardInnerJoin()
            s_sql = s_sql & "where dl.StatusId <> 5 and dl.StatusId in (" & DocSession.CompleteStatus & ") "

            s_sql = DashBoardWhereClause(s_sql)

            s_sql = s_sql & "group by o.OfficeCode,o.Description " & _
            "order by docage asc"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function RetrieveTotalAmountReleased2() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try
            'oracle george
            s_sql =
        "select dl.officecode,amt= replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')',''), drange = convert(datetime,replace(div2.colvalue,'-','/')) " & _
"from doclist dl " & _
"inner join docindex di " & _
"on dl.doctype = di.doctype and di.columnname ='amount' " & _
"inner join docindexvalues div " & _
"on div.docid = dl.docid " & _
"and div.doctype = dl.doctype " & _
"and div.columnid = di.columnid " & _
"and isnumeric(div.colvalue) = 1 " & _
"and div.colvalue <> '' and div.colvalue <> '-' and len(replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')','')) < 15 " & _
"inner join docindex di2 " & _
"on dl.doctype = di2.doctype and di2.columnname ='Date Issued' " & _
"inner join docindexvalues div2 " & _
"on div2.docid = dl.docid " & _
"and div2.doctype = dl.doctype " & _
"and div2.columnid = di2.columnid and IsDate(div2.colvalue) = 1 " & _
" where dl.statusid <> 5 " '& _
            '" AND dl.OfficeCode in ( " & _
            '" select distinct OFFICECODE from groups where MainGroupId ='OG' " & _
            '") "

            '--where MainGroupId ='OG'

            s_sql = " select o.description,amount = sum(convert(money,d.amt)) " & _
    "from ( " & s_sql & " ) d " & _
    "inner join office o " & _
    "on o.officeCode = d.officeCode " & _
    "where isnumeric(d.amt) = 1 "

            If pCreatedDateFrom <> "" Then
                s_sql = s_sql & " AND d.drange >= '" & pCreatedDateFrom & "' "
            End If
            If pCreatedDateTo <> "" Then
                s_sql = s_sql & " AND d.drange < dateadd(day,1,'" & pCreatedDateTo & "') "
            End If


            s_sql = s_sql & " group by o.description "

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function RetrieveTotalAmountReleased() As DataTable
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try
            'oracle george
            s_sql =
        "select dl.officecode,amt= replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')',''), drange = div2.colvalue " & _
"into #temptble from doclist dl " & _
"inner join docindex di " & _
"on dl.doctype = di.doctype and di.columnname ='amount' " & _
"inner join docindexvalues div " & _
"on div.docid = dl.docid " & _
"and div.doctype = dl.doctype " & _
"and div.columnid = di.columnid " & _
"and isnumeric(div.colvalue) = 1 " & _
"and div.colvalue <> '' and len(replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')','')) < 15 " & _
"inner join docindex di2 " & _
"on dl.doctype = di2.doctype and di2.columnname ='Date Issued' " & _
"inner join docindexvalues div2 " & _
"on div2.docid = dl.docid " & _
"and div2.doctype = dl.doctype " & _
"and div2.columnid = di2.columnid and IsDate(div2.colvalue) = 1 " & _
" where dl.statusid <> 5 " '& _
            '" AND dl.OfficeCode in ( " & _
            '" select distinct OFFICECODE from groups where MainGroupId ='OG' " & _
            '") "

            '--where MainGroupId ='OG'

            s_sql = s_sql & " select o.description,amount = sum(convert(money,d.amt)) " & _
    "from #temptble  d " & _
    "inner join office o " & _
    "on o.officeCode = d.officeCode " & _
    "where isnumeric(d.amt) = 1 "

            If pCreatedDateFrom <> "" Then
                s_sql = s_sql & " AND d.drange >= '" & pCreatedDateFrom & "' "
            End If
            If pCreatedDateTo <> "" Then
                s_sql = s_sql & " AND d.drange < dateadd(day,1,'" & pCreatedDateTo & "') "
            End If


            s_sql = s_sql & " group by o.description drop table #temptble "

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
#End Region
#Region "Retention Disposable Schedule"
    Public Function RetrieveRetentionInfo() As DataTable

        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try

            Dim s_sql As String

            If DocSession.OraClient Then
                s_sql = "SELECT dl.DocId " & _
                  ",upper(dl.DocType) as DocType " & _
               ",dt.DocName " & _
                  ",NVL(convert(char(10),dl.RetentionStartDate,101),'') as ActiveStart " & _
                  ",NVL(convert(char(10),dl.PurgeStartDate,101),'') as StorageStart " & _
                  ",NVL(dt.EnableRetention,0) as EnableRetention " & _
                ",NVL(ds.Description,'') as RetentionStatus " & _
                ",NVL(dt.RetentionStatus,'') as RetentionStatusId " & _
                ",NVL(dt.UseCreatedDate,0) as UseCreatedDate " & _
                ",NVL(dt.RetentionDays,0) as RetentionDays " & _
                ",NVL(dt.RetentionPeriod,'') as RetentionPeriod " & _
                ",NVL(dt.PurgePeriod,'') as PurgePeriod " & _
                ",NVL(dt.PurgeDays,'') as PurgeDays "
            Else
                s_sql = "SELECT dl.DocId " & _
                  ",upper(dl.DocType) as DocType " & _
               ",dt.DocName " & _
               ",ds.Description as StatDesc " & _
                  ",isnull(convert(char(10),dl.RetentionStartDate,101),'') as ActiveStart " & _
                  ",isnull(convert(char(10),dl.PurgeStartDate,101),'') as StorageStart " & _
                    ",isnull(convert(char(10),dl.ArchivedDate,101),'') as ArchivedDate " & _
                    ",isnull(div.colvalue,'') as DateIssued " & _
                ",isnull(dt.EnableRetention,0) as EnableRetention " & _
                ",isnull(ds.Description,'') as RetentionStatus " & _
                ",isnull(dt.RetentionStatus,'') as RetentionStatusId " & _
                ",isnull(dt.UseCreatedDate,0) as UseCreatedDate " & _
                ",isnull(dt.RetentionDays,0) as RetentionDays " & _
                ",isnull(dt.RetentionPeriod,'') as RetentionPeriod " & _
                ",isnull(dt.PurgePeriod,'') as PurgePeriod " & _
                ",isnull(dt.PurgeDays,'') as PurgeDays "

            End If
            s_sql = s_sql & "FROM DocList dl INNER JOIN " & _
             "DocType dt ON dl.DocType = dt.DocType " & _
             "LEFT JOIN DocStatus DS ON dl.statusid=ds.statusid " & _
             "LEFT JOIN (" & _
                        "select div.DocId,div.DocType,case when div.colvalue = '' then null when isdate(div.colvalue) = 1 then div.colvalue else null end as colvalue from docindex di INNER JOIN docindexvalues div ON  " & _
                "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                        "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                ") " & _
                " div ON div.docid=dl.docId " & _
             "WHERE " & _
              " dl.StatusId > 0 and dl.DocId = " & pDocId
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue

            'pRetVal = retval.pParam.Value

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
#End Region
#Region "Uploaded Hourly Functions"
    Public Function RetrieveUploaded() As DataTable


        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Dim s_sql As String = "SELECT " & _
            "dl.refno, "
        If DocSession.OraClient Then
            s_sql = s_sql & "TO_DATE(dl.createddate,'mon dd yyyy hh:mi:ss:mmmAM') AS cdate, " & _
                    "NVL(TO_DATE(dl.receiveddate,'mon dd yyyy hh:mi:ss:mmmAM'),' ') AS rdate, " & _
                    "NVL(dl.receivedby,' ') as receivedby, "
        Else
            s_sql = s_sql & "convert(varchar,dl.createddate,109) AS cdate, " & _
                    "Isnull(convert(varchar,dl.receiveddate,109),'') AS rdate, " & _
                    "ISNULL(dl.receivedby,'')  as receivedby, "
        End If

        s_sql = s_sql & _
        "' ' as updatedreceivedby, " & _
        "' ' as updatedreceiveddate, " & _
        "' ' as updatedreceivedtime " & _
        "FROM doclist dl " & _
         "INNER JOIN users u " & _
          "ON dl.createdby = u.userid " & _
          "WHERE dl.statusid <> 5 " 'AND (uploaderOfc = 'CRD' OR dl.routedto like '%CRD%') "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate & "'"
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND dl.createddate < '" & DocSession.rpt_EndDate & "'"
        End If
        If pOfficeCode <> "" Then
            s_sql = s_sql & " AND dl.officeCode = '" & pOfficeCode & "'"
        End If
        s_sql = s_sql & " ORDER BY dl.createddate "

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandText = s_sql
            objCommand.pCommandType = CommandType.Text
            Return objCommand.ExecData()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Function

    Public Function RetrieveUploadedHourly() As DataTable


        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Dim s_sql As String = "SELECT " & _
            "dl.docid,dl.uploaderGrp,dl.refno, "

        s_sql = s_sql & "convert(varchar,dl.createddate,120) AS cdate, " & _
                    "Isnull(convert(varchar,dl.receiveddate,120),'') AS rdate, " & _
                    "ISNULL(dl.receivedby,'')  as receivedby, "


        s_sql = s_sql & _
        "' ' as updatedreceivedby, " & _
        "' ' as updatedreceiveddate, " & _
        "' ' as updatedreceivedtime " & _
        "FROM doclist dl " & _
         "INNER JOIN users u " & _
          "ON dl.createdby = u.userid " & _
          "WHERE dl.statusid <> 5 " 'AND (uploaderOfc = 'CRD' OR dl.routedto like '%CRD%') "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate & "'"
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND dl.createddate < '" & DocSession.rpt_EndDate & "'"
        End If
        If pOfficeCode <> "" Then
            s_sql = s_sql & " AND dl.officeCode = '" & pOfficeCode & "'"
        End If
        s_sql = s_sql & " ORDER BY dl.createddate "

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandText = s_sql
            objCommand.pCommandType = CommandType.Text
            Return objCommand.ExecData()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Function

    Public Function RetrieveFileSize() As DataTable

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Dim s_sql As String = "select Convert(char(10),fv.uploadedDate,101) as UploadDate " &
",case when isnull(sum(fv.filesize),0) <> 0 then round(isnull(sum(fv.filesize),0),2) else isnull(sum(fv.filesize),0) end as TotalFileSize,BytesVal = '' " &
",count(fv.filesize) as TotalFiles,FileVal = '' " &
        "from DocFileVersion fv " &
        "inner join doclist dl on fv.docId = dl.docid " &
        "WHERE dl.statusid <> 5 "

        If pCreatedDateFrom <> "" Then
            s_sql = s_sql & " AND fv.uploadedDate >= '" & pCreatedDateFrom & "'"
        End If
        If pCreatedDateTo <> "" Then
            s_sql = s_sql & " AND fv.uploadedDate < DateAdd(day,1,'" & pCreatedDateTo & "') "
        End If
        s_sql = s_sql & " group by Convert(char(10),fv.uploadedDate,101) " & _
"order by cast(Convert(char(10),fv.uploadedDate,101) as datetime) "


        Try
            objCommand = New clsSqlConn
            objCommand.pCommandText = s_sql
            objCommand.pCommandType = CommandType.Text
            Return objCommand.ExecData()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Function
    Public Function RetrieveFileSizeMonthly() As DataTable


        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Dim s_sql As String = "select " &
                                "DateName(Month, DateAdd(Month, DatePart(mm, fv.uploadedDate), 0) - 1)+' '+ " &
                                "Convert(char(4),Year(fv.uploadedDate)) as UploadDate, " &
                                "case when isnull(sum(fv.filesize),0) <> 0 then round(isnull(sum(fv.filesize),0),2) else isnull(sum(fv.filesize),0) end as TotalFileSize,BytesVal = '' " &
                                ",count(fv.filesize) as TotalFiles,FileVal = '' " &
                            "from docfileversion fv " &
                            "inner join doclist dl " &
"on fv.docid = dl.docid " &
"WHERE dl.statusid <> 5 "
        '"case when isnull(sum(fv.filesize),0) <> 0 then round(isnull(sum(fv.filesize),0) / 1048576,2) else isnull(sum(fv.filesize),0) end as TotalFileSize,BytesVal = '' " 

        If pCreatedDateFrom <> "" Then
            s_sql = s_sql & " AND fv.uploadedDate >= '" & pCreatedDateFrom & "'"
        End If
        If pCreatedDateTo <> "" Then
            s_sql = s_sql & " AND fv.uploadedDate < DateAdd(day,1,'" & pCreatedDateTo & "') "
        End If

        s_sql = s_sql & "Group By " & _
"DatePart(mm,fv.uploadedDate), " & _
"YEAR(fv.uploadedDate) " & "Order By " & _
"DatePart(mm,fv.uploadedDate), " & _
"YEAR(fv.uploadedDate)"

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandText = s_sql
            objCommand.pCommandType = CommandType.Text
            Return objCommand.ExecData()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Function
#End Region

#Region "New Logic of Retrieval"
    Public Function IssuancesRetrieve() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Dim sWhere As String
        Try

            sOrder = OrderBy() & " " & pSortOrder

            sWhere = BuildWhere()



            Dim main_sql As String = ""

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)

            main_sql = IssuancesSQLRetrieve(lTop, sWhere, sOrder)



            Dim s_sqloutput As String = "Select * from (" & main_sql & ") m " & _
                        " WHERE m.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY m.rn "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sqloutput
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
    Public Function IssuancesCount() As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT count(dl.docid) FROM doclist dl "

            If pFolderId <> "" Then
                s_sql = s_sql & " inner join DocIssuances di on dl.docid = di.docid and di.folderid = '" & pFolderId & "' "
            Else
                s_sql = s_sql & " left join DocIssuances di on dl.docid = di.docid "
            End If

            s_sql = s_sql & " Where dl.statusid in (" & DocSession.CompleteStatus & ") "

            If pFolderId <> "" Then
            Else
                s_sql = s_sql & " and di.docid is null "
            End If
           
            'If pRequestType <> "" Then
            s_sql = s_sql & BuildWhere()
            'End If

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Private Function IssuancesSQLRetrieve(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
        'Dim s_sql As String = "SELECT top " & asTop & _
        '    " rn= row_number() over (ORDER BY " & asSort & "),dl.docid " & sqlDocListSuperUser & " " & asWhere

        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.* FROM " & _
                    "( " & _
                     "select " & _
                        "dl.DocId " & _
                        ",refno = isnull(dl.RefNo,'') " & _
                        ",dl.DocType " & _
                        ",dl.Title " & _
                        ",FileName = isnull(dl.FileName,'') " & _
                        ",dl.CreatedDate " & _
                        ",dl.CreatedBy " & _
                        ",dl.ModifiedBy " & _
                        ",dl.StatusId " & _
                        ",dl.RoutingSeqNo " & _
                        ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
                        ",dl.FileVersion " & _
                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
                        ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
                        ",dt.DocName " & _
                        ",Office = isnull(ug.OfficeCode,'') " & _
                        ",statusdesc = ds.description " & _
                        ",urgent = 0,doc_id = -1" & _
                     "from doclist dl "

        If pFolderId <> "" Then
            s_sql = s_sql & " inner join DocIssuances di on dl.docid = di.docid and di.folderid = '" & pFolderId & "' "
        Else
            s_sql = s_sql & " left join DocIssuances di on dl.docid = di.docid "
        End If

        s_sql = s_sql & " INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
                        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
                        "INNER JOIN DocStatus ds ON dl.statusid=ds.statusid " & _
                        "LEFT JOIN docRouting dr ON	" & _
                            "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                         "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
                     "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId " & _
                     " WHERE dl.statusid in (" & DocSession.CompleteStatus & ")  "

        If pFolderId <> "" Then
        Else
            s_sql = s_sql & " and di.docid is null "
        End If

        s_sql = s_sql & " " & asWhere


        s_sql = s_sql & ") dl "

        Return s_sql

    End Function
    Public Function AllDocCount(ByVal asOfc As String) As Integer
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = " SELECT count(dl.docid) FROM doclist dl "
            If asOfc <> "" AndAlso DocSession.sUserRole <> "A" AndAlso DocSession.sUserRole <> "G" Then
                s_sql = s_sql & DocPendingReportSql(asOfc, True) 'DocPendingCountUserSuperSql()
            End If

            s_sql = s_sql & " INNER JOIN docstatus ds ON	" & _
                        "ds.statusid = dl.statusId " & _
                    "INNER JOIN users u " & _
                        "ON dl.createdby = u.userid	"
            '& _
            'DashBoardInnerClauseAdmin() & " "



            s_sql = s_sql & "WHERE dl.statusid <> 5 "

            s_sql = s_sql & BuildWhere()
            If asOfc <> "" AndAlso (DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G") Then
                s_sql = s_sql & " AND dl.OfficeCode = '" & asOfc & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function AllDocRetrieve(ByVal asOfc As String) As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Dim sWhere As String
        Try

            sOrder = OrderBy() & " " & pSortOrder
            sWhere = BuildWhere()
            If asOfc <> "" AndAlso (DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G") Then
                sWhere = sWhere & " AND dl.OfficeCode = '" & asOfc & "'"
            End If
            Dim main_sql As String = ""

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)
            
            main_sql = AllDoclistSqlSuperUser(lTop, sWhere, sOrder, asOfc)

            

            Dim s_sqloutput As String = "Select * from (" & main_sql & ") m " & _
                        " WHERE m.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY m.rn "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sqloutput
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
    Public Function InboxDocCount() As Integer
        Dim objCommand As clsSqlConn


        Dim sWhere As String
        Try


            sWhere = BuildWhere()


            Dim main_count_sql As String = DocInboxCountUser(sWhere, DocSession.sUserId)

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = main_count_sql
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
    Public Function InboxDocRetrieve() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Dim sWhere As String
        Try

            sOrder = OrderBy()
            sWhere = BuildWhere()

            Dim main_sql As String = ""

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)

            main_sql = DocInboxSqlUser(lTop, sWhere, sOrder, DocSession.sUserId)






            Dim s_sqloutput As String = " SELECT " & _
                                                            "m.rn, " & _
                                                            "dl.DocId " & _
                                                            ",refno = isnull(dl.RefNo,'') " & _
                                                            ",dl.DocType " & _
                                                            ",dl.Title " & _
                                                            ",FileName = isnull(dl.FileName,'') " & _
                                                            ",dl.CreatedDate " & _
                                                            ",dl.CreatedBy " & _
                                                            ",dl.ModifiedBy " & _
                                                            ",dl.StatusId " & _
                                                            ",dl.RoutingSeqNo " & _
                                                            ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
                                                            ",dl.FileVersion " & _
                                                            ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
                                                            ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
                                                            ",dt.DocName " & _
                                                            ",Office = isnull(ug.OfficeCode,'') " & _
                                                            ",statusdesc = ds.description " & _
                                                            ",urgent = 0,doc_id = -1" & _
                                                            "FROM docList dl " & _
                                                                " inner join ( " & main_sql & " ) m ON m.docid = dl.docid  " & _
            "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
            "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
            "INNER JOIN DocStatus ds ON dl.statusid=ds.statusid " & _
            "LEFT JOIN docRouting dr ON	" & _
            "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
            "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
            "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId " & _
            " WHERE m.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY m.rn "


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sqloutput
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
    Public Function SentDocCount() As Integer
        Dim objCommand As clsSqlConn


        Dim sWhere As String
        Try


            sWhere = BuildWhere()


            Dim main_count_sql As String = DocSentCountUser(sWhere, DocSession.sUserId)

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = main_count_sql
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
    Public Function SentDocRetrieve() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim sOrder As String
        Dim sWhere As String
        Try

            sOrder = OrderBy()
            sWhere = BuildWhere()

            Dim main_sql As String = ""

            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1 'CInt(pIdx) * CInt(pRowsPerPage)

            main_sql = DocSentSqlUser(lTop, sWhere, sOrder, DocSession.sUserId)

            Dim s_sqloutput As String = " SELECT " & _
                                                            "m.rn, " & _
                                                            "dl.DocId " & _
                                                            ",refno = isnull(dl.RefNo,'') " & _
                                                            ",dl.DocType " & _
                                                            ",dl.Title " & _
                                                            ",FileName = isnull(dl.FileName,'') " & _
                                                            ",dl.CreatedDate " & _
                                                            ",dl.CreatedBy " & _
                                                            ",dl.ModifiedBy " & _
                                                            ",dl.StatusId " & _
                                                            ",dl.RoutingSeqNo " & _
                                                            ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
                                                            ",dl.FileVersion " & _
                                                            ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
                                                            ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
                                                            ",dt.DocName " & _
                                                            ",Office = isnull(ug.OfficeCode,'') " & _
                                                            ",ds.description " & _
                                                            ",urgent = 0,doc_id = -1" & _
                                                            "FROM docList dl " & _
                                                                " inner join ( " & main_sql & " ) m ON m.docid = dl.docid  " & _
            "INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
            "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
            "INNER JOIN DocStatus ds ON dl.statusid=ds.statusid " & _
            "LEFT JOIN docRouting dr ON	" & _
            "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
            "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
            "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId " & _
            " WHERE m.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " ORDER BY m.rn "


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sqloutput
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
#End Region
#Region "Document Forms"
    Public Function DocFormsCount() As Integer
        Dim objCommand As clsSqlConn


        Dim sWhere As String
        Try


            Dim s_sql As String = "SELECT count(df.FormID) From DocForms df "



            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
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

#End Region
#Region "Sql Functions"

    Dim sqlDocListUserWhere As String = " AND (((dl.ArchiverGrp = '" & DocSession.sUserGroup & "' OR dl.OfficeCode = '" & DocSession.sOfcCode & "'" & _
                            " OR dl.UploaderGrp = '" & DocSession.sUserGroup & "') and (dl.confidential is null or dl.confidential = 0) )" & _
                            " OR dl.CreatedBy = '" & DocSession.sUserId & "'" & _
                            ")"

    Dim sqlDocListSuperUserWhere As String = " AND (((dl.ArchiverGrp = '" & DocSession.sUserGroup & "'" & _
                            " OR dl.UploaderGrp = '" & DocSession.sUserGroup & "'" & _
                            " OR dl.OfficeCode = '" & DocSession.sOfcCode & "'" & _
                            " OR dl.ArchiverOfc = '" & DocSession.sOfcCode & "'" & _
                            " OR dl.UploaderOfc = '" & DocSession.sOfcCode & "' ) and (dl.confidential is null or dl.confidential = 0) )" & _
                            " OR dl.CreatedBy = '" & DocSession.sUserId & "'" & _
                            ")"

    Dim sqlDocListUser As String = " FROM doclist dl " & _
                            " WHERE dl.statusid <> 5 " & _
                            sqlDocListUserWhere & " "


    Dim sqlDocListSuperUser As String = " FROM doclist dl " & _
                            " WHERE dl.statusid <> 5 " & _
                            sqlDocListSuperUserWhere & " "


    Public ReadOnly Property pDocListUser As String
        Get
            If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
                Return pDocListSuperUserWhere
            ElseIf DocSession.sUserRole = "A" Then
                Return ""
            Else
                Return pDocListUserWhere
            End If
            Return sqlDocListUser
        End Get
    End Property
    Public ReadOnly Property pDocListUserWhere As String
        Get
            Return sqlDocListUserWhere
        End Get
    End Property
    Public ReadOnly Property pDocListSuperUser As String
        Get
            Return sqlDocListSuperUser & sqlDocListSuperUserWhere
        End Get
    End Property
    Public ReadOnly Property pDocListSuperUserWhere As String
        Get
            Return sqlDocListSuperUserWhere
        End Get
    End Property
    Private Function DoclistSqlSuperUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
        'Dim s_sql As String = "SELECT top " & asTop & _
        '    " rn= row_number() over (ORDER BY " & asSort & "),dl.docid " & sqlDocListSuperUser & " " & asWhere

        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.docid FROM " & _
                    "( " & _
                     "select dl.docid " & sqlDocListSuperUser & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"

        Return s_sql

    End Function
    Private Function AllDoclistSqlSuperUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String, ByVal asOfc As String) As String
        'Dim s_sql As String = "SELECT top " & asTop & _
        '    " rn= row_number() over (ORDER BY " & asSort & "),dl.docid " & sqlDocListSuperUser & " " & asWhere

        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.* FROM " & _
                    "( " & _
                     "select " & _
                        "dl.DocId " & _
                        ",refno = isnull(dl.RefNo,'') " & _
                        ",dl.DocType " & _
                        ",dl.Title " & _
                        ",FileName = isnull(dl.FileName,'') " & _
                        ",dl.CreatedDate " & _
                        ",dl.CreatedBy " & _
                        ",dl.ModifiedBy " & _
                        ",dl.StatusId " & _
                        ",dl.RoutingSeqNo " & _
                        ",FirstApprover= isnull(dl.FirstApprover,'')  " & _
                        ",dl.FileVersion " & _
                        ",originator = isnull(u.FirstName,'')+' '+isnull(u.LastName,'') " & _
                        ",(isnull(u3.FirstName,'')+' '+isnull(u3.LastName,'')) AS assignedto " & _
                        ",dt.DocName " & _
                        ",Office = isnull(ug.OfficeCode,'') " & _
                        ",statusdesc = ds.description " & _
                        ",urgent = 0,doc_id = -1 " & _
                     "from doclist dl "

        If asOfc <> "" AndAlso DocSession.sUserRole <> "A" AndAlso DocSession.sUserRole <> "G" Then
            s_sql = s_sql & DocPendingReportSql(asOfc, True) '
            'DocPendingCountUserSuperSql()
        End If

        s_sql = s_sql & " INNER JOIN Users U ON U.userId = dl.CreatedBy " & _
                        "INNER JOIN DocType dt ON dl.DocType = dt.DocType " & _
                        "INNER JOIN DocStatus ds ON dl.statusid=ds.statusid " & _
                        "LEFT JOIN docRouting dr ON	" & _
                            "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                         "LEFT JOIN Users U3 ON U3.userId = dr.approverid " & _
                     "LEFT JOIN groups ug ON U3.usergroup = ug.GroupId " & _
                     " WHERE dl.statusid <> 5 "

        s_sql = s_sql & " " & asWhere


        s_sql = s_sql & ") dl "

        Return s_sql

    End Function

    Private Function DoclistSearchSqlSuperUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String, ByVal asSQL As String) As String
        'Dim s_sql As String = "SELECT top " & asTop & _
        '    " rn= row_number() over (ORDER BY " & asSort & "),dl.docid " & sqlDocListSuperUser & " " & asWhere

        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.docid FROM " & _
                    "( " & _
                     asSQL & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"

        Return s_sql

    End Function
    Private Function DoclistCountSuperUser(ByVal asWhere As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM " & _
                    "( " & _
                     "SELECT dl.docid " & sqlDocListSuperUser & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"
        Return s_sql
    End Function
    Private Function DoclistSearchCountSuperUser(ByVal asWhere As String, ByVal asSql As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM " & _
                    "( " & _
                     asSql & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"
        Return s_sql
    End Function
    Private Function DoclistSqlUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.docid FROM " & _
                    "( " & _
                     "select dl.docid " & sqlDocListUser & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"

        Return s_sql
    End Function
    Private Function DoclistCountUser(ByVal asWhere As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM " & _
                     "( " & _
                     "select dl.docid " & sqlDocListUser & " " & asWhere & _
                     "UNION " & _
                     sqlDocRouting & " " & asWhere & _
                    ") dl"
        Return s_sql
    End Function

    Private Function DoclistSqlAdmin(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.docid From Doclist dl Where dl.statusid <> 5 " & asWhere

        Return s_sql
    End Function
    Private Function DoclistCountAdmin(ByVal asWhere As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) From doclist dl where dl.statusid <> 5 " & asWhere

        Return s_sql
    End Function

    'Private Function DocInboxSqlAdmin(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
    '    Dim s_sql As String = "SELECT top " & asTop & _
    '        " rn= row_number() over (ORDER BY " & asSort & "),dl.docid From Doclist dl " & _
    '        " Inner Join docInbox di on di.docid = dl.docid Where dl.statusid <> 5 " & asWhere

    '    Return s_sql
    'End Function
    'Private Function DocInboxCountAdmin(ByVal asWhere As String, ByVal asUserId As String) As String
    '    Dim s_sql As String = "SELECT count(dl.docid) From doclist dl  " & _
    '        " Inner Join docInbox di on di.docid = dl.docid Where dl.UserId = '" & asUserId & "' and dl.statusid <> 5 " & asWhere

    '    Return s_sql
    'End Function

    'Private Function DocSentSqlAdmin(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String) As String
    '    Dim s_sql As String = "SELECT top " & asTop & _
    '        " rn= row_number() over (ORDER BY " & asSort & "),dl.docid From Doclist dl " & _
    '        " Inner Join DocOutbox di on di.docid = dl.docid Where dl.statusid <> 5 " & asWhere

    '    Return s_sql
    'End Function
    'Private Function DocSentCountAdmin(ByVal asWhere As String, ByVal asUserId As String) As String
    '    Dim s_sql As String = "SELECT count(dl.docid) From doclist dl  " & _
    '        " Inner Join docSent di on di.docid = dl.docid Where dl.UserId = '" & asUserId & "' and dl.statusid <> 5 " & asWhere

    '    Return s_sql
    'End Function
    Private Function DocInboxSqlUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String, ByVal asUserId As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),dl.docid From Doclist dl " & _
            " Inner Join docInbox di on di.docid = dl.docid Where di.UserId = '" & asUserId & "' and dl.statusid <> 5 " & asWhere

        Return s_sql
    End Function
    Private Function DocInboxCountUser(ByVal asWhere As String, ByVal asUserId As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) From doclist dl  " & _
            " Inner Join docInbox di on di.docid = dl.docid Where di.UserId = '" & asUserId & "' and dl.statusid <> 5 " & asWhere

        Return s_sql
    End Function

    Private Function DocSentSqlUser(ByVal asTop As String, ByVal asWhere As String, ByVal asSort As String, ByVal asUserId As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " rn= row_number() over (ORDER BY " & asSort & "),di.docid From DocOutBox di  " & _
            " Inner Join Doclist dl on di.docid = dl.docid and dl.statusid <> 5 "

        If pAuthor <> "" Then
            s_sql = s_sql & " INNER JOIN users u ON u.userId = dl.createdby " 
        End If
        s_sql = s_sql & "  Where di.UserId = '" & asUserId & "'  " & asWhere

        Return s_sql
    End Function
    Private Function DocSentCountUser(ByVal asWhere As String, ByVal asUserId As String) As String
        Dim s_sql As String = "SELECT count(di.docid) From DocOutBox di " & _
            " Inner Join doclist dl on di.docid = dl.docid and dl.statusid <> 5 "
        

        If pAuthor <> "" Then
            s_sql = s_sql & " INNER JOIN users u ON u.userId = dl.createdby " 

        End If
        s_sql = s_sql & " Where di.UserId = '" & asUserId & "' " & asWhere

        Return s_sql
    End Function
#End Region
#Region "DocIndexValues"
    Private Function DocIndexValuesSuperUser(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM " & _
        "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
        " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "

        Return s_sql

    End Function
    Private Function DocIndexValuesUser(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM " & _
        "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
        " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "
        Return s_sql

    End Function
    Private Function DocIndexValuesAdmin(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM doclist dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function
    Private Function DocIndexValuesCountSuperUser(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM " & _
            "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
            " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "

        Return s_sql

    End Function
    Private Function DocIndexValuesCountUser(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl inner join " & _
            "(" & _
        "SELECT dl.docid " & sqlDocListUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
            " dl2 on dl2.docid = dl.docid inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListUserWhere
        Return s_sql

    End Function
    Private Function DocIndexValuesCountAdmin(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docindexvalues div " & _
"INNER JOIN docindex di on div.columnid = di.columnid " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function

#End Region
#Region "DocAttachIndexValues"
    Private Function DocAttachIndexValuesSuperUser(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM " & _
        "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
        " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docattachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "
        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "

        Return s_sql

    End Function
    Private Function DocAttachIndexValuesUser(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM " & _
        "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
        " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docAttachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "
        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "
        Return s_sql

    End Function
    Private Function DocAttachIndexValuesAdmin(ByVal asTop As String, ByVal asColumn As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM doclist dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docattachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "
        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function
    Private Function DocAttachIndexValuesCountSuperUser(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM " & _
            "(" & _
        "SELECT dl.docid " & sqlDocListSuperUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
            " dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docattachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "
        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId "

        Return s_sql

    End Function
    Private Function DocAttachIndexValuesCountUser(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl inner join " & _
            "(" & _
        "SELECT dl.docid " & sqlDocListUser & _
        " UNION " & _
        sqlDocRouting & _
        ")" & _
            " dl2 on dl2.docid = dl.docid inner join  " & _
        "( SELECT DISTINCT div.DocId From docattachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "
        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListUserWhere
        Return s_sql

    End Function
    Private Function DocAttachIndexValuesCountAdmin(ByVal asColumn As String, ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl inner join  " & _
        "( SELECT DISTINCT div.DocId From docattachindexvalues div " & _
        "INNER JOIN docattachment da on da.docattachid = div.docattachid and da.docid = div.docid and div.doctype = da.attachtype " & _
"INNER JOIN docindex di on div.columnid = di.columnid "


        s_sql = s_sql & "WHERE "

        If asColumn <> "" Then
            s_sql = s_sql & "di.ColumnName = '" & asColumn & "' and "
        End If

        s_sql = s_sql & " div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
" ) div on dl.DocId = div.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function

#End Region
#Region "DocListSearchValues"
    Private Function DocListSearchSuperUser(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") From Doclist dl Inner Join " & _
            "(" & _
            " SELECT dl.docid FROM doclist dl inner join " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListSuperUserWhere & _
        " UNION " & _
        sqlDocRouting & " AND ( " & QuickSearchWhereClause(asCriteria) & ") " & _
        ") x on x.docid = dl.docid "
        Return s_sql

    End Function
    Private Function DocListSearchUser(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") From Doclist dl Inner Join " & _
            "(" & _
            " SELECT dl.docid FROM doclist dl inner join " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListUserWhere & _
        " UNION " & _
        sqlDocRouting & " AND ( " & QuickSearchWhereClause(asCriteria) & ") " & _
        ") x on x.docid = dl.docid "
        Return s_sql

    End Function
    Private Function DocListSearchAdmin(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,rn= ROW_NUMBER() over (Order By " & asSort & ") FROM doclist dl inner join  " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function
    Private Function DocListSearchCountSuperUser(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(x.docid) From " & _
            "(" & _
            " SELECT DL.docid FROM doclist dl inner join " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListSuperUserWhere & _
        " UNION " & _
        sqlDocRouting & " AND ( " & QuickSearchWhereClause(asCriteria) & ") " & _
        ") x "
        Return s_sql

    End Function
    Private Function DocListSearchCountUser(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(x.docid) From " & _
            "(" & _
            " SELECT dl.docid FROM doclist dl inner join " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 " & _
        sqlDocListUserWhere &
        " UNION " &
        sqlDocRouting & " AND ( " & QuickSearchWhereClause(asCriteria) & ") " & _
        ") x "
        Return s_sql

    End Function
    Private Function DocListSearchCountAdmin(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl inner join  " & _
        "( " & MainWhereClause(asCriteria) & ") " & _
" dt on dl.DocId = dt.DocId " & _
"WHERE dl.statusid <> 5 "
        Return s_sql

    End Function

#End Region
#Region "DocAttachement"
    Dim sqlDocAttachment As String = "SELECT distinct docid from DocAttachment " & _
        " where docfilename "
    Private Function DocAttachmentSuperUser(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,'' as afname,rn= ROW_NUMBER() over (partition by dl.docid Order By " & asSort & ") FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 " &
            sqlDocListSuperUserWhere

        Return s_sql

    End Function
    Private Function DocAttachmentUser(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,'' as afname,rn= ROW_NUMBER() over (partition by dl.docid Order By " & asSort & ") FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 " &
            sqlDocListUserWhere

        Return s_sql

    End Function
    Private Function DocAttachmentAdmin(ByVal asTop As String, ByVal asCriteria As String, ByVal asSort As String) As String
        Dim s_sql As String = "SELECT top " & asTop & _
            " dl.docid,'' as afname,rn= ROW_NUMBER() over (partition by dl.docid Order By " & asSort & ") FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 "

        Return s_sql

    End Function
    Private Function DocAttachmentCountSuperUser(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 " &
            sqlDocListSuperUserWhere

        Return s_sql

    End Function
    Private Function DocAttachmentCountUser(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 " &
            sqlDocListUserWhere

        Return s_sql

    End Function
    Private Function DocAttachmentCountAdmin(ByVal asCriteria As String) As String
        Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl " & _
            "INNER JOIN (" & sqlDocAttachment & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & ") da " & _
            " on dl.DocId = da.DocId " & _
            "WHERE dl.statusid <> 5 "

        Return s_sql

    End Function
#End Region
#Region "Quick Search"
    Public Function QuickCountSql(ByVal asCriteria As String, ByVal asSearchOption As String) As String
        Return SearchCountSql(asCriteria, asSearchOption)
    End Function
    Public Function QuickSearchSql(ByVal asCriteria As String, ByVal asSearchOption As String, ByVal asRowsPerPage As String, ByVal asIdx As String) As String
        Dim asTop As String = (CInt(asRowsPerPage) + CInt(asIdx) - 1).ToString
        Return MainSearchSql(SearchSql(asCriteria, asSearchOption, asTop), asIdx)
    End Function
    Private Function s_f_where(ByVal asCriteria As String) As String
        Dim s_where As String = ""
        Dim CriteriaValue As String = ""
        Dim asSearchOption As String = DocSession.SearchOption
        If Left(asCriteria, 6).ToLower.Trim = "refno:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
            If asSearchOption = "E" Then
                s_where = "dl.refno = '" & CriteriaValue & "' "
            Else
                s_where = "dl.refno Like '%" & CriteriaValue & "%' "
            End If
        ElseIf Left(asCriteria, 6).ToLower.Trim = "local:" Then
            s_where = "isnull(dl.Islocal,0) = 1 "
        ElseIf Left(asCriteria, 6).ToLower.Trim = "title:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
            If asSearchOption = "E" Then
                s_where = "dl.title = '" & CriteriaValue & "' "
            Else
                s_where = "dl.title Like '%" & CriteriaValue & "%' "
            End If

        ElseIf Left(asCriteria, 11).ToLower.Trim = "officecode:" Then
            CriteriaValue = Mid(asCriteria, 12).Trim
            If asSearchOption = "E" Then
                s_where = "dl.officecode = '" & CriteriaValue & "' "
            Else
                s_where = "dl.officecode Like '%" & CriteriaValue & "%' "
            End If

        ElseIf Left(asCriteria, 9).ToLower.Trim = "filename:" Then
            CriteriaValue = Mid(asCriteria, 10).Trim
            If asSearchOption = "E" Then
                s_where = "dl.filename = '" & CriteriaValue & "' "
            Else
                s_where = "dl.filename Like '%" & CriteriaValue & "%' "
            End If

        ElseIf Left(asCriteria, 9).ToLower.Trim = "statusid:" Then
            CriteriaValue = Mid(asCriteria, 10).Trim
            If IsNumeric(asCriteria) Then
                s_where = "dl.statusid = " & CriteriaValue & " "
            Else
                s_where = "dl.statusid = 2 "
            End If
        ElseIf Left(asCriteria, 10).ToLower.Trim = "ipaddress:" Then
            CriteriaValue = Mid(asCriteria, 11).Trim
            If asSearchOption = "E" Then
                s_where = "dl.ipaddress = '" & CriteriaValue & "' "
            Else
                s_where = "dl.ipaddress Like '%" & asCriteria & "%' "
            End If
        ElseIf Left(asCriteria, 8).ToLower.Trim = "duedate:" Then
            CriteriaValue = Mid(asCriteria, 9).Trim
            If IsDate(asCriteria) Then
                s_where = "convert(Char(10),dl.duedate,101) = '" & CriteriaValue & "' "
            Else
                s_where = "convert(Char(10),dl.duedate,101) = '" & Date.Now.ToShortDateString & "' "
            End If
        ElseIf Left(asCriteria, 14).ToLower.Trim = "completeddate:" Then
            CriteriaValue = Mid(asCriteria, 15).Trim
            If IsDate(asCriteria) Then
                s_where = "convert(Char(10),dl.completeddate,101) = '" & CriteriaValue & "' "
            Else
                s_where = "convert(Char(10),dl.completeddate,101) = '" & Date.Now.ToShortDateString & "' "
            End If
        ElseIf Left(asCriteria, 13).ToLower.Trim = "archiveddate:" Then
            CriteriaValue = Mid(asCriteria, 14).Trim
            If IsDate(asCriteria) Then
                s_where = "convert(Char(10),dl.archiveddate,101) = '" & CriteriaValue & "' "
            Else
                s_where = "convert(Char(10),dl.archiveddate,101) = '" & Date.Now.ToShortDateString & "' "
            End If
        ElseIf Left(asCriteria, 11).ToLower.Trim = "attachment:" OrElse Left(asCriteria, 5).ToLower.Trim = "aidx:" OrElse Left(asCriteria, 11).ToLower.Trim = "aidxsarono:" OrElse Left(asCriteria, 10).ToLower.Trim = "aidxncano:" OrElse Left(asCriteria, 10).ToLower.Trim = "aidxboxno:" Then

            s_where = ""

        Else
            CriteriaValue = asCriteria
            If asSearchOption = "E" Then
                s_where = "(dl.refno = '" & CriteriaValue & "' OR  dl.title = '" & CriteriaValue & "') "
            Else
                s_where = "(dl.refno Like '%" & CriteriaValue & "%' OR dl.title Like '%" & CriteriaValue & "%') "
            End If

        End If
        Return s_where
    End Function
    Private Function s_f_index(ByRef asCriteria As String) As String
        Dim asColumn As String = ""
        Dim asSearchOption As String = DocSession.SearchOption
        If Left(asCriteria, 7).ToLower.Trim = "sarono:" Then
            asCriteria = Mid(asCriteria, 8).Trim
            asColumn = DocSession.SAROColumn


        ElseIf Left(asCriteria, 6).ToLower.Trim = "ncano:" Then
            asCriteria = Mid(asCriteria, 7).Trim
            asColumn = DocSession.NCAColumn

        ElseIf Left(asCriteria, 6).ToLower.Trim = "boxno:" Then
            asCriteria = Mid(asCriteria, 7).Trim
            asColumn = DocSession.BOXColumn
        Else
            asCriteria = ""
        End If
        Return asColumn
    End Function
    'attachment index
    Private Function s_f_attachindex(ByRef asCriteria As String) As String
        Dim asColumn As String = ""
        Dim asSearchOption As String = DocSession.SearchOption
        If Left(asCriteria, 5).ToLower.Trim = "aidx:" Then
            asCriteria = Mid(asCriteria, 6).Trim
            asColumn = ""

        ElseIf Left(asCriteria, 11).ToLower.Trim = "aidxsarono:" Then
            asCriteria = Mid(asCriteria, 12).Trim
            asColumn = DocSession.SAROColumn
        ElseIf Left(asCriteria, 10).ToLower.Trim = "aidxncano:" Then
            asCriteria = Mid(asCriteria, 11).Trim
            asColumn = DocSession.NCAColumn

        ElseIf Left(asCriteria, 10).ToLower.Trim = "aidxboxno:" Then
            asCriteria = Mid(asCriteria, 11).Trim
            asColumn = DocSession.BOXColumn
        Else
            asCriteria = ""
        End If
        Return asColumn
    End Function
    Private Function SearchCountSql(ByVal asCriteria As String, ByVal asSearchOption As String) As String
        Dim main_sql As String
        Dim s_where As String
        Dim CriteriaValue As String = ""

        asCriteria = asCriteria.Replace("'", "''")
        s_where = s_f_where(asCriteria)

        If s_where <> "" Then
            s_where = " and " & s_where
            If DocSession.sUserRole = "D" Then
                main_sql = DoclistCountSuperUser(s_where)
            ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                main_sql = DoclistCountAdmin(s_where)
            Else
                main_sql = DoclistCountUser(s_where)

            End If
        Else
            Dim asColumn As String
            CriteriaValue = asCriteria
            asColumn = s_f_index(CriteriaValue)

            If CriteriaValue <> "" Then
                If DocSession.sUserRole = "D" Then
                    main_sql = DocIndexValuesCountSuperUser(asColumn, CriteriaValue)
                ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                    main_sql = DocIndexValuesCountAdmin(asColumn, CriteriaValue)
                Else
                    main_sql = DocIndexValuesCountUser(asColumn, CriteriaValue)
                End If
            Else
                'CriteriaValue = ""
                CriteriaValue = asCriteria
                asColumn = s_f_attachindex(CriteriaValue)

                If CriteriaValue <> "" Then
                    If DocSession.sUserRole = "D" Then
                        main_sql = DocAttachIndexValuesCountSuperUser(asColumn, CriteriaValue)
                    ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                        main_sql = DocAttachIndexValuesCountAdmin(asColumn, CriteriaValue)
                    Else
                        main_sql = DocAttachIndexValuesCountUser(asColumn, CriteriaValue)
                    End If
                Else
                    CriteriaValue = ""
                    If Left(asCriteria, 11).ToLower.Trim = "attachment:" Then
                        CriteriaValue = Mid(asCriteria, 12).Trim
                        'asColumn = Left(asCriteria, 11).ToLower.Trim
                    End If
                    If CriteriaValue <> "" Then

                        If DocSession.sUserRole = "D" Then
                            main_sql = DocAttachmentCountSuperUser(CriteriaValue)
                        ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                            main_sql = DocAttachmentCountAdmin(CriteriaValue)
                        Else
                            main_sql = DocAttachmentCountUser(CriteriaValue)
                        End If
                    Else
                        asCriteria = ufParseCriteria(asCriteria, asSearchOption)
                        If DocSession.sUserRole = "D" Then
                            main_sql = DoclistSearchCountSuperUser(asCriteria)
                        ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                            main_sql = DocListSearchCountAdmin(asCriteria)

                        Else
                            main_sql = DocListSearchCountUser(asCriteria)
                        End If
                    End If
                End If
            End If

        End If
        Return main_sql 'MainSearchSql(main_sql, asIdx)

    End Function
    Private Function SearchSql(ByVal asCriteria As String, ByVal asSearchOption As String, ByVal asTop As String) As String
        Dim main_sql As String
        Dim s_where As String
        Dim CriteriaValue As String = ""
        asCriteria = asCriteria.Replace("'", "''")
        s_where = s_f_where(asCriteria)

        If s_where <> "" Then
            s_where = " AND " & s_where
            If DocSession.sUserRole = "D" Then
                main_sql = DoclistSqlSuperUser(asTop, s_where, "dl.docid desc")
            ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                main_sql = DoclistSqlAdmin(asTop, s_where, "dl.docid desc")
            Else
                main_sql = DoclistSqlUser(asTop, s_where, "dl.docid desc")
            End If
        Else
            Dim asColumn As String
            CriteriaValue = asCriteria
            asColumn = s_f_index(CriteriaValue)
            If CriteriaValue <> "" Then
                If DocSession.sUserRole = "D" Then
                    main_sql = DocIndexValuesSuperUser(asTop, asColumn, CriteriaValue, "dl.docid desc")
                ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                    main_sql = DocIndexValuesAdmin(asTop, asColumn, CriteriaValue, "dl.docid desc")
                Else
                    main_sql = DocIndexValuesUser(asTop, asColumn, CriteriaValue, "dl.docid desc")
                End If
            Else

                CriteriaValue = asCriteria
                asColumn = s_f_attachindex(CriteriaValue)

                If CriteriaValue <> "" Then
                    If DocSession.sUserRole = "D" Then
                        main_sql = DocAttachIndexValuesSuperUser(asTop, asColumn, CriteriaValue, "dl.docid desc")
                    ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                        main_sql = DocAttachIndexValuesAdmin(asTop, asColumn, CriteriaValue, "dl.docid desc")
                    Else
                        main_sql = DocAttachIndexValuesUser(asTop, asColumn, CriteriaValue, "dl.docid desc")
                    End If
                Else
                    CriteriaValue = ""
                    If Left(asCriteria, 11).ToLower.Trim = "attachment:" Then
                        CriteriaValue = Mid(asCriteria, 12).Trim
                        asColumn = Left(asCriteria, 11).ToLower.Trim
                    End If
                    If CriteriaValue <> "" Then
                        If DocSession.sUserRole = "D" Then
                            main_sql = DocAttachmentSuperUser(asTop, CriteriaValue, "dl.createddate desc")
                        ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                            main_sql = DocAttachmentAdmin(asTop, CriteriaValue, "dl.createddate desc")
                        Else
                            main_sql = DocAttachmentUser(asTop, CriteriaValue, "dl.createddate desc")
                        End If
                    Else
                        asCriteria = ufParseCriteria(asCriteria, asSearchOption)
                        If DocSession.sUserRole = "D" Then
                            main_sql = DocListSearchSuperUser(asTop, asCriteria, "dl.createddate desc")
                        ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
                            main_sql = DocListSearchAdmin(asTop, asCriteria, "dl.createddate desc")
                        Else
                            main_sql = DocListSearchUser(asTop, asCriteria, "dl.createddate desc")
                        End If
                    End If
                End If
            End If

        End If
        Return main_sql 'MainSearchSql(main_sql, asIdx)

    End Function
    Private Function CountSearchSql(ByVal asInnerJoin As String) As String
        Return " SELECT COUNT(dl.docid) FROM doclist dl " & _
            "(" & asInnerJoin & ") dt on dt.docid = dl.docid "
    End Function
    Private Function MainSearchSql(ByVal asInnerJoin As String, ByVal asIdx As String) As String
        Dim s_sql As String
        s_sql = "SELECT " & _
        "di.rn,colvalue='',docAction = '', " & _
        "dl.docId, " & _
        "dl.doctype, " & _
        "dl.Title, " & _
        "dl.ModifiedBy, " & _
        "dl.filename, " & _
        "dl.ModifiedDate, " & _
        "dl.IPAddress, " & _
        "dl.statusid, " & _
        "dl.createddate " & _
        ",(case when db.DocId is null then 0 else 1 end) AS BookMarked " & _
        ",(case when dl.InternalDoc = 1 then 'Internal' else 'External' end) AS Classification " & _
        ",dt.docname " & _
        ",isnull(dl.DocPurgedDate,'') as PurgedDate " & _
        ",ds.description " & _
        ",1 AS GroupAccessId " & _
        ",(ISNULL(u.FirstName,'') + ' ' + ISNULL(u.LastName,'')) AS userName, " & _
        "(ISNULL(u1.FirstName,'') + ' ' + ISNULL(u1.LastName,'')) AS Originator " & _
        ",refno=isnull(dl.refno,'') " & _
        ",ReceivedBy=isnull(dl.ReceivedBy,'') " & _
        ",ReceivedDate=isnull(convert(char(10),dl.ReceivedDate,101),'') " & _
        ",ReceivedTime=isnull(right(convert(varchar,dl.ReceivedDate),8),'') " & _
        ", isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) as PersonnelInCharge " & _
        " From Doclist dl " & _
        " INNER JOIN (" & asInnerJoin & ") di " & _
        "  ON di.docid = dl.docid " & _
        "INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
        "INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " & _
        "INNER JOIN users u ON dl.ModifiedBy = u.userid " & _
        "INNER JOIN users u1 ON dl.CreatedBy = u1.userid " & _
        "LEFT JOIN DocBookmark db ON dl.docId = db.docId and db.userid= '" & DocSession.sUserId & "' " & _
        " LEFT JOIN docRouting dr ON	" & _
        " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
        " LEFT JOIN users u2 " & _
        "  ON dr.approverid = u2.userid	"
        If asIdx <> "" Then
            s_sql = s_sql & "WHERE (di.rn between " & asIdx & " and " & asIdx & "+" & DocSession.RowsPerPage & ") "
        End If
        Return s_sql
    End Function
    Private Function QuickSearchWhereClause(ByVal asCriteria As String) As String
        Dim s_sql As String = ""
        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dl.refno IN (" & asCriteria & ") OR "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dl.refno") & ") OR "
        End If
        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dl.Title IN (" & asCriteria & ") OR "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dl.Title") & ") OR "
        End If
        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dl.FileName IN (" & asCriteria & ") OR "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dl.FileName") & ") OR "
        End If

        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dl.DocSender IN (" & asCriteria & ") OR "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dl.DocSender") & ") OR "
        End If

        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dt.DocName IN (" & asCriteria & ") "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dt.DocName") & ") "
        End If
        Return s_sql
    End Function
    Private Function MainWhereClause(ByVal asCriteria) As String
        Dim s_sql As String = "SELECT dl.docId " & _
                "FROM doclist dl " & _
                    "INNER JOIN doctype dt ON  " & _
                        "dl.doctype = dt.doctype WHERE " & _
        QuickSearchWhereClause(asCriteria)

        s_sql = s_sql & "UNION " & _
              "SELECT dtag.docId " & _
                "FROM DocTags dtag " & _
                "WHERE "

        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dtag.tags IN (" & asCriteria & ") "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dtag.tags") & ") "
        End If
        s_sql = s_sql & "UNION " & _
              "SELECT dn.docId " & _
                "FROM DocNotes dn " & _
                "WHERE "

        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "dn.Notes IN (" & asCriteria & ") "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "dn.Notes") & ") "
        End If

        s_sql = s_sql & "UNION " & _
              "SELECT div.docId " & _
                "FROM DocIndexValues div " & _
                "WHERE "
        If DocSession.SearchOption = "E" Then
            s_sql = s_sql & "div.ColValue IN (" & asCriteria & ") "
        Else
            s_sql = s_sql & "(" & PartialSearch(asCriteria, "div.ColValue") & ") "
        End If



        Return s_sql
    End Function
    Private Function ufxParseCriteria(ByVal asCriteria As String, ByVal asSearchOption As String) As String

        Dim squery, mystr, str, mystr2, strwithqoute As String
        Dim strArr As Array = Nothing
        Dim ind, ind2 As Integer


        mystr2 = ""
        strwithqoute = ""
        squery = Trim(asCriteria)
        squery = Replace(squery, "'", "''")

        If squery.Contains(Chr(34)) Then

            ind = squery.IndexOf(Chr(34)) 'get first instance of ["]'

            While ind < squery.Length 'loop for another instance

                ind2 = squery.IndexOf(Chr(34), ind + 1)

                If ind2 > 0 Then

                    'If squery.IndexOf(" ", ind, ind2 - ind) = ind Then

                    'ind = ind2 + 1
                    strwithqoute = squery.Substring(ind, (ind2 - ind) + 1).Trim
                    mystr = squery.Substring(ind + 1, (ind2 - ind) - 1).Trim
                    If mystr.IndexOf(" ") > 0 Then

                        mystr2 = mystr.Replace(" ", "_<<<$&$>>>_")

                        squery = squery.Replace(strwithqoute, mystr2)
                    Else

                        squery = squery.Replace(strwithqoute, mystr)
                    End If

                    ind = squery.IndexOf(Chr(34))

                ElseIf ind2 < 0 Then

                    Exit While

                End If

            End While

            strArr = squery.Split(" ")

            ind = 0

            mystr = ""

            For ind = 0 To strArr.Length - 1

                mystr = strArr(ind)

                If mystr.Contains("_<<<$&$>>>_") Then

                    mystr = mystr.Replace("_<<<$&$>>>_", " ")

                    If mystr.Contains("""") Then

                        mystr = mystr.Replace("""", "")

                    End If

                ElseIf mystr.IndexOf("""") = 0 And mystr.LastIndexOf("""") = mystr.Length - 1 Then

                    mystr = mystr.Replace("""", "")



                End If

                If str = "" Then
                    If asSearchOption = "E" Then
                        str = "'" & Trim(mystr) & "'"
                        'str = Trim(mystr)
                    Else
                        str = " <ZZ@@ZZ> LIKE '%" & Trim(mystr) & "%'"
                    End If


                Else
                    If asSearchOption = "E" Then
                        str = str & ",'" & Trim(mystr) & "'"
                        'str = str & "," & Trim(mystr)
                    Else
                        str = str & " OR <ZZ@@ZZ> LIKE '%" & Trim(mystr) & "%'"
                    End If


                End If

            Next

        Else

            strArr = squery.Split(" ")

            For ind = 0 To strArr.Length - 1

                If str = "" Then
                    If asSearchOption = "E" Then
                        str = "'" & Trim(strArr(ind)) & "'"
                        'str = Trim(strArr(ind))
                    Else
                        str = " <ZZ@@ZZ> LIKE '%" & Trim(strArr(ind)) & "%'"
                    End If

                Else
                    If asSearchOption = "E" Then
                        str = str & ",'" & Trim(strArr(ind)) & "'"
                        'str = str & "," & Trim(strArr(ind))
                    Else
                        str = str & " OR <ZZ@@ZZ> LIKE '%" & Trim(strArr(ind)) & "%'"
                    End If

                End If

            Next

        End If

        Return str

    End Function
    Private Function PartialSearch(ByVal asCriteria As String, ByVal asColumn As String) As String
        If DocSession.SearchOption = "E" Then
            Return asCriteria
        Else
            Return Replace(asCriteria, "<ZZ@@ZZ>", asColumn)
        End If

    End Function

    Private Function ufParseCriteria(ByVal asCriteria As String, ByVal asSearchOption As String) As String

        Dim squery, mystr, str, mystr2, strwithqoute As String
        Dim strArr As Array = Nothing
        Dim ind, ind2 As Integer


        mystr2 = ""
        strwithqoute = ""
        squery = Trim(asCriteria)
        squery = Replace(squery, "'", "''")

        If squery.Contains(Chr(34)) Then

            ind = squery.IndexOf(Chr(34)) 'get first instance of ["]'

            While ind < squery.Length 'loop for another instance

                ind2 = squery.IndexOf(Chr(34), ind + 1)

                If ind2 > 0 Then

                    'If squery.IndexOf(" ", ind, ind2 - ind) = ind Then

                    'ind = ind2 + 1
                    strwithqoute = squery.Substring(ind, (ind2 - ind) + 1).Trim
                    mystr = squery.Substring(ind + 1, (ind2 - ind) - 1).Trim
                    If mystr.IndexOf(" ") > 0 Then

                        mystr2 = mystr.Replace(" ", "_<<<$&$>>>_")

                        squery = squery.Replace(strwithqoute, mystr2)
                    Else

                        squery = squery.Replace(strwithqoute, mystr)
                    End If

                    ind = squery.IndexOf(Chr(34))

                ElseIf ind2 < 0 Then

                    Exit While

                End If

            End While

            strArr = squery.Split(" ")

            ind = 0

            mystr = ""

            For ind = 0 To strArr.Length - 1

                mystr = strArr(ind)

                If mystr.Contains("_<<<$&$>>>_") Then

                    mystr = mystr.Replace("_<<<$&$>>>_", " ")

                    If mystr.Contains("""") Then

                        mystr = mystr.Replace("""", "")

                    End If

                ElseIf mystr.IndexOf("""") = 0 And mystr.LastIndexOf("""") = mystr.Length - 1 Then

                    mystr = mystr.Replace("""", "")



                End If

                If str = "" Then
                    If asSearchOption = "E" Then
                        str = "'" & Trim(mystr) & "'"
                        'str = Trim(mystr)
                    Else
                        str = " <ZZ@@ZZ> LIKE '%" & Trim(mystr) & "%'"
                    End If


                Else
                    If asSearchOption = "E" Then
                        str = str & ",'" & Trim(mystr) & "'"
                        'str = str & "," & Trim(mystr)
                    Else
                        str = str & " OR <ZZ@@ZZ> LIKE '%" & Trim(mystr) & "%'"
                    End If


                End If

            Next

        Else

            strArr = squery.Split(" ")

            For ind = 0 To strArr.Length - 1

                If str = "" Then
                    If asSearchOption = "E" Then
                        str = "'" & Trim(strArr(ind)) & "'"
                        'str = Trim(strArr(ind))
                    Else
                        str = " <ZZ@@ZZ> LIKE '%" & Trim(strArr(ind)) & "%'"
                    End If

                Else
                    If asSearchOption = "E" Then
                        str = str & ",'" & Trim(strArr(ind)) & "'"
                        'str = str & "," & Trim(strArr(ind))
                    Else
                        str = str & " OR <ZZ@@ZZ> LIKE '%" & Trim(strArr(ind)) & "%'"
                    End If

                End If

            Next

        End If

        Return str

    End Function
#End Region
#Region "Count All Docs"
    Public Function CntApproverOffice(ByVal asOfc As String) As String
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = "SELECT count(distinct dl.docid) FROM doclist dl inner join docrouting dr on dl.docid = dr.docid " & _
                                    "INNER JOIN users u ON u.userid = dr.approverid " & _
                                    "INNER JOIN groups g ON g.groupid = u.usergroup " & _
                                    " WHERE dl.statusid <> 5 and g.OfficeCode = '" & Replace(asOfc, "'", "''") & "'"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2.ToString

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    'Public Function CntArchiverOffice(ByVal asOfc As String) As String
    '    Dim s_sql As String
    '    Dim objCommand As clsSqlConn

    '    Try

    '        s_sql = "SELECT count(dl.docid) FROM doclist dl " & _
    '                                " WHERE dl.statusid <> 5 and dl.ArchiverOfc = '" & Replace(asOfc, "'", "''") & "'"

    '        objCommand = New clsSqlConn
    '        objCommand.pCommandType = CommandType.Text
    '        objCommand.pCommandText = s_sql

    '        Return objCommand.ExecScalar2.ToString

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '    End Try
    'End Function
    Public Function CntArchiverOffice(ByVal asOfc As String) As String
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = "SELECT count(dl.docid) FROM doclist dl " & _
                                    " WHERE dl.statusid <> 5 and dl.ArchiverOfc = '" & Replace(asOfc, "'", "''") & "'"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2.ToString

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function CntUploaderOffice(ByVal asOfc As String) As String
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = "SELECT count(dl.docid) FROM doclist dl " & _
                                    " WHERE dl.statusid <> 5 and dl.UploaderOfc = '" & Replace(asOfc, "'", "''") & "'"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2.ToString

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Function CntOfficeDocuments(ByVal asOfc As String) As String
        Dim s_sql As String
        Dim objCommand As clsSqlConn

        Try

            s_sql = "SELECT count(dl.docid) FROM doclist dl " & _
                                    " WHERE dl.statusid <> 5 and dl.OfficeCode = '" & Replace(asOfc, "'", "''") & "'"

            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

            Return objCommand.ExecScalar2.ToString

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
#End Region
    Public Function DataShow() As DataTable
        Dim loData As DataTable
        Dim loRow As DataRow
        Try

            loData = New DataTable("showrecords")
            loData.Columns.Add("recid", Type.GetType("System.String"))
            loData.Columns.Add("RecDesc", Type.GetType("System.String"))
            loRow = loData.NewRow()
            loRow("recid") = ""
            loRow("recdesc") = "All"

            loData.Rows.Add(loRow)
            If Not DocSession.HideConfidential Then
                loRow = loData.NewRow()
                loRow("recid") = "1"
                loRow("recdesc") = "Confidential"

                loData.Rows.Add(loRow)
                loRow = loData.NewRow()
                loRow("recid") = "0"
                loRow("recdesc") = "Non-Confidential"

                loData.Rows.Add(loRow)
            End If


            Return loData
            


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Function

    Public Function RetrieveRefno() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            Dim s_sql As String = ""


            s_sql = "select * from (SELECT TOP " & lTop.ToString & " " &
                                  "(row_number() over (ORDER BY " & s_order & " " & pSortOrder & ")) AS rn, " &
                          "u.FirstName+' '+u.LastName as Originator," &
            "dl.CreatedDate, dt.DocName, ds.Description,dl.Title,dl.FileName," &
            "isnull(dru.FirstName,'')+' '+isnull(dru.LastName,'') as PersonnelInCharge,isnull(o.Description,'') as OfficeDesc,isnull(o.LandLineNo,'') as LandLineNo,isnull(o.LocalNo,'') as LocalNo,isnull(o.OfficeCode,'') as OfficeCode,dl.RefNo,dl.DocId," &
"(case when dl.InternalDoc = 1 then 'Internal' else 'External' end) AS Classification " &
"FROM doclist dl " &
"INNER JOIN doctype dt ON dl.doctype = dt.doctype " &
"INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " &
"inner join Users u on u.UserId = dl.CreatedBy " &
"left join docrouting dr on dl.RoutingSeqNo = dr.SeqNo " &
"left join Users dru on dru.UserId = dr.ApproverId " &
"left join groups g on g.GroupId = dru.UserGroup " &
"left join Office o on o.OfficeCode = g.OfficeCode " &
            "WHERE dl.statusid <> 5  "

            If pPrefix = "title:" Then
                s_sql = s_sql & " AND dl.title " & pDocTitle
            ElseIf pPrefix = "refno:" Then
                s_sql = s_sql & " AND dl.refno " & pRefNo
            ElseIf pPrefix = "local:" Then
                s_sql = s_sql & " AND isnull(dl.IsLocal,0) = 1"
            ElseIf Prefix = "^-*" Then
                s_sql = s_sql & " AND (dl.refno " & pRefNo & " or dl.title " & pDocTitle & ") "
            Else
                s_sql = s_sql & " AND dl.refno " & pRefNo & " "
            End If


            s_sql = s_sql & ") dtbl " & _
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            's_sql = s_sql & "ORDER BY  " & OrderBy() & " " & pSortOrder

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '  "xMSP_DOCTYPEGET"

            
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
    Public Function CountRefno() As String
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
       
        Try
            Dim s_sql As String = ""


            s_sql = "select count(dl.docid) " & _
                        "FROM doclist dl " & _
                        "INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
                        "INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " & _
                        "inner join Users u on u.UserId = dl.CreatedBy " & _
                                    "WHERE dl.statusid <> 5  "
            If pRefNo <> "" Then
                s_sql = s_sql & " AND dl.refno " & pRefNo
            End If

           

            's_sql = s_sql & "ORDER BY  " & OrderBy() & " " & pSortOrder

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '  "xMSP_DOCTYPEGET"


            Return objCommand.ExecScalar3

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
