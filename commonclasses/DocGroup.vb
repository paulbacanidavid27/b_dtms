Public Class DocGroup
    Dim DocType As String
    Dim DocTypeDesc As String
    Dim GroupLogo As String
    Dim StatusType As String
    Dim UserId As String
    Dim StatusId As String
    Dim StatusDesc As String
    Dim ReportAccess As String = ""
    Dim Archived As String = ""
    Dim EditIndex As String = ""
    'Dim Complete As String = ""
    Dim ImportDoc As String = ""
    Dim DeleteDoc As String = ""
    Dim ReportAccess_o As String = ""
    Dim GroupDesc As String = ""
    Dim GroupDesc_o As String = ""
    Dim GroupId As String
    Dim GroupAccessId As String
    Dim GroupAccessDesc As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String
    Dim CanPrint As String
    Dim CanPrintReceipt As String
    Dim CanDownLoad As String
    Dim TrackingColor As String
    Dim TextColor As String
    Dim VersionControl As String
    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim RowsPerPage As String
    Dim Idx As String


    Public Property pTextColor() As String
        Get
            Return TextColor
        End Get
        Set(ByVal value As String)
            TextColor = value
        End Set

    End Property
    Public Property pTrackingColor() As String
        Get
            Return TrackingColor
        End Get
        Set(ByVal value As String)
            TrackingColor = value
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

    Public Sub New()

    End Sub

    Public Property pVersionControl() As String
        Get
            Return VersionControl
        End Get
        Set(ByVal value As String)
            VersionControl = value
        End Set

    End Property
    Public Property pGroupAccessId() As String
        Get
            Return GroupAccessId
        End Get
        Set(ByVal value As String)
            GroupAccessId = value
        End Set

    End Property
    Public Property pGroupAccessDesc() As String
        Get
            Return GroupAccessDesc
        End Get
        Set(ByVal value As String)
            GroupAccessDesc = value
        End Set

    End Property
    Dim officeCode As String
    Public Property pOfficeCode() As String
        Get
            Return officeCode
        End Get
        Set(ByVal value As String)
            officeCode = value
        End Set

    End Property
    Dim MainGroupId As String
    Public Property pMainGroupId() As String
        Get
            Return MainGroupId
        End Get
        Set(ByVal value As String)
            MainGroupId = value
        End Set

    End Property
    Public Property pCanDownLoad() As String
        Get
            Return CanDownLoad
        End Get
        Set(ByVal value As String)
            CanDownLoad = value
        End Set

    End Property
    Public Property pCanPrint() As String
        Get
            Return CanPrint
        End Get
        Set(ByVal value As String)
            CanPrint = value
        End Set

    End Property
    Public Property pCanPrintReceipt() As String
        Get
            Return CanPrintReceipt
        End Get
        Set(ByVal value As String)
            CanPrintReceipt = value
        End Set

    End Property


    Public Property pReportAccess() As Integer
        Get
            Return ReportAccess
        End Get
        Set(ByVal value As Integer)
            ReportAccess = value
        End Set

    End Property

    Public Property pEditIndex() As String
        Get
            Return EditIndex
        End Get
        Set(ByVal value As String)
            EditIndex = value
        End Set

    End Property
    Public Property pArchived() As Integer
        Get
            Return Archived
        End Get
        Set(ByVal value As Integer)
            Archived = value
        End Set

    End Property

    'Public Property pComplete() As Integer
    '    Get
    '        Return Complete
    '    End Get
    '    Set(ByVal value As Integer)
    '        Complete = value
    '    End Set

    'End Property
    Public Property pImportDoc() As Integer
        Get
            Return ImportDoc
        End Get
        Set(ByVal value As Integer)
            ImportDoc = value
        End Set

    End Property
    Public Property pDeleteDoc() As Integer
        Get
            Return DeleteDoc
        End Get
        Set(ByVal value As Integer)
            DeleteDoc = value
        End Set

    End Property
    Public Property pReportAccess_o() As Integer
        Get
            Return ReportAccess_o
        End Get
        Set(ByVal value As Integer)
            ReportAccess_o = value
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
    Public Property pStatusId() As String
        Get
            Return StatusId
        End Get
        Set(ByVal value As String)
            StatusId = value
        End Set

    End Property
    Public Property pStatusDesc() As String
        Get
            Return StatusDesc
        End Get
        Set(ByVal value As String)
            StatusDesc = value
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

    Public Property pDocTypeDesc() As String
        Get
            Return DocTypeDesc
        End Get
        Set(ByVal value As String)
            DocTypeDesc = value
        End Set

    End Property

    Public Property pStatusType() As String
        Get
            Return StatusType
        End Get
        Set(ByVal value As String)
            StatusType = value
        End Set

    End Property
    Public Property pDesc() As String
        Get
            Return GroupDesc
        End Get
        Set(ByVal value As String)
            GroupDesc = value
        End Set

    End Property
    Public Property pDesc_o() As String
        Get
            Return GroupDesc_o
        End Get
        Set(ByVal value As String)
            GroupDesc_o = value
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
    Public Property pGroupID() As String
        Get
            Return GroupId
        End Get
        Set(ByVal value As String)
            GroupId = value
        End Set

    End Property
    Public Property pGroupLogo() As String
        Get
            Return GroupLogo
        End Get
        Set(ByVal value As String)
            GroupLogo = value
        End Set

    End Property
    Dim RRTitle As String
    Public Property pRRTitle() As String
        Get
            Return RRTitle
        End Get
        Set(ByVal value As String)
            RRTitle = value
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

    'Private Function RetrieveDdlGroups() As DataTable


    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Try
    '        objCommand = New clsSqlConn
    '        If DocSession.OraClient Then
    '            Dim osp As New cls_storedproc
    '            osp.pGroupId = pGroupID
    '            osp.pGroupName = pDesc
    '            objCommand.pCommandType = CommandType.Text
    '            objCommand.pCommandText = osp.xMSP_GROUPGET
    '        Else
    '            objCommand.CommandType = CommandType.StoredProcedure

    '            objCommand.CommandText = "xMSP_GROUPGET"

    '            If pGroupID <> "" Then
    '                objCommand.ParametersAddWithValue("@GroupId", pGroupID)
    '            End If

    '            If pDesc <> "" Then
    '                objCommand.ParametersAddWithValue("@GroupName", pDesc)
    '            End If

    '        End If

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

    Public Function RetrieveGroupAccess() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            Dim osp As New cls_storedproc
            osp.pGroupId = pGroupID

            objCommand.CommandText = osp.DMSF_GROUPGETACCESS_sql()


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
    Public Function RetrieveGroupDocTypeAccess() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Try
            Dim s_sql As String
            s_sql = "SELECT "
            
            s_sql = s_sql & "isnull(g.doctype,'') gdoctype, " & _
             "isnull(d.doctype,'') doctype, " & _
             "isnull(d.docname,'') docname " & _
            "FROM " & _
             "doctype d " & _
             "LEFT JOIN " & _
             " groupdoctypeaccess g on g.doctype = d.doctype and g.groupid = '" & pGroupID & "' " & _
                  "WHERE d.inactive is null or d.inactive = 0 "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            'Dim osp As New cls_storedproc
            'osp.pGroupId = pGroupID

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

    Public Function RetrieveAccess() As Integer


        Dim objCommand As clsSqlConn

        'Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            Dim osp As New cls_storedproc
            osp.pGroupId = pGroupID
            If DocSession.OraClient Then
                objCommand.CommandText = "select GroupAccessId from groupdocaccess where GroupId = '" & pGroupID & "' and rownum = 1 order by GroupAccessId desc"
            Else
                objCommand.CommandText = "select top 1 GroupAccessId from groupdocaccess where GroupId = '" & pGroupID & "' order by GroupAccessId desc"
            End If


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

    Public Function RetrieveDocHierarchy() As DataTable


        Dim objCommand As clsSqlConn

        'Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            Dim osp As New cls_storedproc
            osp.pGroupId = pGroupID
            If DocSession.OraClient Then
                objCommand.CommandText = "SELECT StatusId,NVL(hierarchy,0) as Hierarchy FROM DocStatus ORDER BY StatusId"
            Else
                objCommand.CommandText = "SELECT StatusId,hierarchy=isnull(hierarchy,0) FROM DocStatus ORDER BY StatusId"
            End If


            Return objCommand.ExecData

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

    Public Function CountGroups() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "groups g " & _
           "WHERE g.groupid is not null " & WhereClause()




            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





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

    Public Function CountDocStatus() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "DocStatus ds " & _
           "WHERE ds.statusid is not null " & StatusWhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





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

    Public Function RetrieveOffice() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try

            'ldata = RetrieveGroupOffice()

            'If ldata.Rows.Count > 0 Then

            'Else
            s_sql = "SELECT OfficeCode,Description " & _
                     "FROM  " & _
            " Office WHERE (deleted is null or deleted = 0) "

            If pOfficeCode <> "" Then
                s_sql = s_sql & " AND OfficeCode = '" & pOfficeCode & "'"
            End If

            s_sql = s_sql & " ORDER BY description "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"



            ldata = objCommand.Fill




            'End If
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

    Public Function RetrieveGroupOffice() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String
        'routing problem-george
        Try
            s_sql = "SELECT o.OfficeCode,o.Description " & _
                     "FROM GroupOfficeAccess og " & _
            " INNER JOIN Office o ON o.OfficeCode = og.OfficeCode " & _
            " WHERE (o.deleted is null or o.deleted = 0) "

            If DocSession.sUserGroup <> "" Then
                s_sql = s_sql & " AND og.GroupId = '" & DocSession.sUserGroup & "'"
            End If

            s_sql = s_sql & " ORDER BY o.description "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"



            Return objCommand.Fill


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

    Public Function RetrieveOfficeMainGroup() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT OfficeCode,Description " & _
                     "FROM  " & _
            " Office WHERE OfficeCode IN ( SELECT DISTINCT officeCode FROM Groups WHERE MainGroupId = 'OG')  ORDER BY description "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"



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

    Public Function RetrieveMainGroup() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT MainGroupId,Description " & _
                     "FROM  " & _
            " MainGroup "

            If pMainGroupId <> "" Then
                s_sql = s_sql & " WHERE MainGroupId = '" & pMainGroupId & "'"
            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"



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

    Public Function CheckIfGroupCodeExists(ByVal asGroupCode As String) As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pGroupId = asGroupCode
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_GROUPCODEEXISTS
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_GROUPCODEEXISTS" 'OK NA
            'objCommand.ParametersAddWithValue("@GroupCode", asGroupCode)
            'End If

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

    Public Function CheckIfStatusExists(ByVal asStatusId As String) As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc

            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.pCommandText = "SELECT statusid FROM DocList WHERE statusid = '" & asStatusId & "' and rownum = 1"
            Else
                objCommand.pCommandText = "SELECT TOP 1 statusid FROM DocList WHERE statusid = '" & asStatusId & "'"
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

    'Public Function RetrieveGroup() As DataTable
    '    'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    'Dim oConn As New SqlClient.SqlConnection(str)

    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable
    '    Try
    '        objCommand = New clsSqlConn
    '        If DocSession.OraClient Then
    '            Dim osp As New cls_storedproc
    '            osp.pGroupId = pGroupID
    '            osp.pSortCol = pSortCol
    '            osp.pSortOrder = pSortOrder
    '            osp.pGroupName = pDesc
    '            objCommand.pCommandType = CommandType.Text
    '            objCommand.pCommandText = osp.xMSP_GROUPGET
    '        Else
    '            objCommand.CommandType = CommandType.StoredProcedure

    '            objCommand.CommandText = "xMSP_GROUPGET"

    '            If pGroupID.Trim <> "" Then
    '                objCommand.ParametersAddWithValue("@GroupId", pGroupID)
    '            End If

    '            If pDesc.Trim <> "" Then
    '                objCommand.ParametersAddWithValue("@GroupName", pDesc)
    '            End If
    '            objCommand.ParametersAddWithValue("@SortCol", pSortCol)
    '            objCommand.ParametersAddWithValue("@SortOrder", pSortOrder)
    '        End If

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

    'Public Function RetrieveGroupNew2() As DataTable


    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable
    '    Try
    '        objCommand = New clsSqlConn
    '        If DocSession.OraClient Then
    '            Dim osp As New cls_storedproc
    '            If pGroupID.Trim <> "" Then
    '                osp.pGroupId = pGroupID
    '            End If

    '            If pDesc.Trim <> "" Then
    '                osp.pGroupName = pDesc
    '            End If

    '            osp.pSortCol = pSortCol
    '            osp.pSortOrder = pSortOrder

    '            objCommand.pCommandType = CommandType.Text
    '            objCommand.pCommandText = osp.xMSP_GROUPGET_NEW
    '        Else
    '            objCommand.CommandType = CommandType.StoredProcedure

    '            objCommand.CommandText = "xMSP_GROUPGET_NEW"

    '            If pGroupID.Trim <> "" Then
    '                objCommand.ParametersAddWithValue("@GroupId", pGroupID)
    '            End If

    '            If pDesc.Trim <> "" Then
    '                objCommand.ParametersAddWithValue("@Desc", pDesc)
    '            End If
    '            objCommand.ParametersAddWithValue("@SortCol", pSortCol)
    '            objCommand.ParametersAddWithValue("@SortOrder", pSortOrder)
    '        End If

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



    Public Function RetrieveGroupNew() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "select * from (SELECT "
            If Not DocSession.OraClient Then
                sSQL = sSQL & " TOP " & lTop.ToString & " "
            End If

            sSQL = sSQL & " row_number() over (ORDER BY " & s_order & " " & pSortOrder & ") as rn, " & _
            "grouplogo = isnull(g.grouplogo,'dbm.png'), " & _
            "ReceiptReplyName = isnull(g.ReceiptReplyName,'DEPARTMENT OF BUDGET AND MANAGEMENT'), " & _
            "g.groupid, " & _
            "g.groupname, "

            '"(case when NVL(g.Complete,0) = 1 then 'Yes' else 'No' end) as CanComplete, " & _
            '"(case when isnull(g.Complete,0) = 1 then 'Yes' else 'No' end) CanComplete, " & _

            If DocSession.OraClient Then
                sSQL = sSQL & "NVL(g.officecode,' ') as officecode, " & _
                    "NVL(g.MainGroupId,' ') as MainGroupId, " & _
            "(case when NVL(g.ReportAccess,0) = 1 then 'Yes' else 'No' end) as CanReport, " & _
            "(case when NVL(g.EditIndex,0) = 1 then 'Yes' else 'No' end) as CanEditIndex, " & _
            "(case when NVL(g.ImportDoc,0) = 1 then 'Yes' else 'No' end) as CanImport, " & _
            "(case when NVL(g.CanPrint,0) = 1 then 'Yes' else 'No' end) as CanPrint, " & _
            "(case when NVL(g.VersionControl,0) = 1 then 'Yes' else 'No' end) as CanVersionControl, " & _
            "(case when NVL(g.CanDownload,0) = 1 then 'Yes' else 'No' end) as CanDownload, " & _
            "(case when NVL(g.AlwaysAllowed,0) = 1 then 'Always Allowed' else 'Limited Days' end) as AlwaysAllowedDesc, " & _
            "NVL(g.trackingcolor,' ') AS trackingcolor, " & _
            "NVL(g.textcolor,' ') AS textcolor, " & _
            "NVL(g.AlwaysAllowed,0) as AlwaysAllowed "
            Else
                sSQL = sSQL & "isnull(g.officecode,'') officecode, " & _
                    "Isnull(g.MainGroupId,'') as MainGroupId, " & _
            "(case when isnull(g.ReportAccess,0) = 1 then 'Yes' else 'No' end) CanReport, " & _
            "(case when isnull(g.EditIndex,0) = 1 then 'Yes' else 'No' end) CanEditIndex, " & _
            "(case when isnull(g.ImportDoc,0) = 1 then 'Yes' else 'No' end) CanImport, " & _
             "(case when isnull(g.CanPrint,0) = 1 then 'Yes' else 'No' end) as CanPrint, " & _
            "(case when isnull(g.VersionControl,0) = 1 then 'Yes' else 'No' end) as CanVersionControl, " & _
            "(case when isnull(g.CanDownload,0) = 1 then 'Yes' else 'No' end) as CanDownload, " & _
            "(case when isnull(g.AlwaysAllowed,0) = 1 then 'Always Allowed' else 'Limited Days' end) AlwaysAllowedDesc, " & _
            "trackingcolor = ISNULL(g.trackingcolor,''), " & _
            "textcolor = ISNULL(g.textcolor,''), " & _
            "isnull(g.AlwaysAllowed,0) AlwaysAllowed "
            End If

            sSQL = sSQL & "FROM  " & _
            "groups g " & _
           "WHERE g.groupid is not null " & WhereClause() & " " & _
                      ") dtbl " & _
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

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

    Private Function StatusOrderBy() As String
        If pSortCol = "Document Status" Then
            Return " ds.Description "
        ElseIf pSortCol = "Status Type" Then
            Return " ds.StatusType "
        Else
            Return " ds.Description "
        End If

    End Function

    Private Function OrderBy() As String
        If pSortCol = "Group Code" Then
            Return " g.groupid "
        ElseIf pSortCol = "Group Name" Then
            Return "g.groupname "
        ElseIf pSortCol = "Report Access" Then
            Return "g.ReportAccess "
        ElseIf pSortCol = "Edit Index" Then
            Return "g.EditIndex "
            'ElseIf pSortCol = "Complete Doc" Then
            '    Return "g.Complete "
        ElseIf pSortCol = "Import Doc" Then
            Return "g.ImportDoc "
        ElseIf pSortCol = "Delete Doc" Then
            Return "g.DeleteDoc "
        ElseIf pSortCol = "Work Schedules" Then
            Return "g.AlwaysAllowed "
        ElseIf pSortCol = "Office Code" Then
            Return "g.OfficeCode "
        Else
            Return "g.groupid "
        End If

    End Function

    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pGroupID <> "" Then
            lswhere = lswhere & " AND g.groupid like '%" & Replace(pGroupID, "'", "''") & "%' "
        End If
        If pDesc <> "" Then
            lswhere = lswhere & " AND g.groupname like '%" & Replace(pDesc, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    Private Function StatusWhereClause() As String
        Dim lswhere As String = ""

        If pDesc <> "" Then
            lswhere = lswhere & " AND ds.Description like '%" & Replace(pDesc, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    Public Function RetrieveWorkSchedule() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn



            Dim s_sql As String = "SELECT " & _
                   "'" & pGroupID & "' AS Gid " & _
                ",W.DAYNAME " & _
                ",W.Weekday "

            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(WS.StartTime,'00:00 AM') AS ST " & _
                ",NVL(WS.EndTime,'00:00 AM') AS ET "

            Else
                s_sql = s_sql & ",isnull(WS.StartTime,'00:00 AM') AS ST " & _
                    ",isnull(WS.EndTime,'00:00 AM') AS ET "
            End If

            s_sql = s_sql & ",(case when WS.StartTime is null then 0 else 1 end) AS chk " & _
                 "FROM Weekdays W " & _
                  "LEFT JOIN WorkSchedule WS " & _
                   "ON W.WEEKDAY = WS.WEEKDAY AND WS.groupId = '" & Replace(pGroupID, "'", "''") & "' " & _
                  "LEFT JOIN Groups G " & _
                   "ON G.GroupId = WS.GroupId "
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_WORKSCHEDULE"

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

    Public Function RetrieveStatusList() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = StatusOrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select * from (SELECT " & _
                      "row_number() over (ORDER BY " & s_order & " " & pSortOrder & ") as rn, " & _
            "ds.StatusId, " & _
            "NVL(ds.StatusType,' ') AS StatusType, " & _
            "(Case When ds.StatusType = 'O' Then 'Outgoing' When ds.statustype='I' Then 'Incoming' Else ' ' End) as TypeDesc, " & _
            "ds.Description "

                sSQL = sSQL & "FROM  " & _
                " DocStatus ds " & _
               "WHERE ds.Statusid is not null " & StatusWhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                      "rn= row_number() over (ORDER BY " & s_order & " " & pSortOrder & "), " & _
            "ds.StatusId, " & _
            "StatusType=isnull(ds.StatusType,''), " & _
            "(Case When ds.StatusType = 'O' Then 'Outgoing' When ds.statustype='I' Then 'Incoming' Else '' End) as TypeDesc, " & _
            "ds.Description "

                sSQL = sSQL & "FROM  " & _
                " DocStatus ds " & _
               "WHERE ds.Statusid is not null " & StatusWhereClause() & " " & _
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



    Function RetrieveDocStatus() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT StatusId,Description FROM DocStatus WHERE StatusId > 2 Order By Description"
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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
    Function RetrieveDocStatusByID() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT StatusId,Description FROM DocStatus WHERE StatusId > 2 and StatusId = " & pStatusId & " Order By Description"
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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
    Function RetrieveDocStatusGroup() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT gs.StatusId,g.groupId,g.groupname FROM GroupStatus gs  " & _
                    "INNER JOIN Groups g ON gs.GroupId = g.GroupId " & _
                    "WHERE gs.statusid = " & pStatusId & _
                    " Order By g.groupName "
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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

    Function RetrieveOfficeGroup() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT gs.OfficeCode,g.groupId,g.groupname FROM GroupOfficeAccess gs  " & _
                    "INNER JOIN Groups g ON gs.GroupId = g.GroupId " & _
                    "WHERE gs.OfficeCode = '" & Replace(pOfficeCode, "'", "''") & "' " & _
                    " Order By g.groupName "
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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


    Function RetrieveDocStatusGroupByID() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.pCommandText = "SELECT g.groupID,g.groupname, NVL(gs.StatusId,0) AS StatusId FROM Groups g " & _
                        "LEFT JOIN GroupStatus gs ON g.GroupId = gs.GroupId AND gs.StatusId = '" & Replace(pStatusId, "'", "''") & "' "
            Else
                objCommand.pCommandText = "SELECT g.groupID,g.groupname, StatusId=isnull(gs.StatusId,0) FROM Groups g " & _
                    "LEFT JOIN GroupStatus gs ON g.GroupId = gs.GroupId AND gs.StatusId = '" & Replace(pStatusId, "'", "''") & "' Order By g.groupname "
            End If


            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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

    Function RetrieveOfficeGroupByID() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT g.groupID,g.groupname, OfficeCode=isnull(gs.OfficeCode,'0') FROM Groups g " &
                "LEFT JOIN GroupOfficeAccess gs ON g.GroupId = gs.GroupId AND gs.OfficeCode = '" & Replace(pOfficeCode, "'", "''") & "' Order By g.groupname "



            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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

    Function RetrieveGroups() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String
        Try
            s_sql = "SELECT GroupID,GroupName " & _
                     "FROM  " & _
            " Groups "

            If pGroupID <> "" Then
                s_sql = s_sql & " WHERE GroupId = '" & pGroupID & "'"
            End If

            If pOfficeCode <> "" Then
                s_sql = s_sql & " WHERE OfficeCode = '" & pOfficeCode & "'"
            End If

            s_sql = s_sql & " order by groupname "
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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

    Function RetrieveDocTypeGroups() As DataTable


        Dim objCommand As clsSqlConn
        Dim s_sql As String = "SELECT gda.groupid, g.groupname FROM GroupDocTypeAccess gda INNER JOIN groups g " &
    "ON gda.groupid = g.groupid WHERE gda.DocType ='" & pDocType & "' Order By g.groupname "
        Dim ldata As DataTable
        'Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            'Else
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            'End If

            ldata = objCommand.Fill
            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
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


    Public Sub UpdateGroup(ByVal objCommand As clsSqlConn)

        Try
            '",Complete = " & pComplete & _
            Dim lsSql As String = "UPDATE Groups SET " & _
                    "GroupName = '" & Replace(pDesc, "'", "''") & "'" & _
                    ",OfficeCode = '" & Replace(pOfficeCode, "'", "''") & "'" & _
                    ",MainGroupId = '" & Replace(pMainGroupId, "'", "''") & "'" & _
                    ",ModifiedBy = '" & pUserId & "' " & _
                    ",ModifiedDate = " & DocSession.CurrentDate & " " & _
                    ",ReportAccess = " & pReportAccess & _
                    ",CanPrint = " & pCanPrint & _
                    ",CanDownload = " & pCanDownLoad & _
                    ",VersionControl = " & pVersionControl & _
                    ",EditIndex = " & pEditIndex & _
                    ",ImportDoc = " & pImportDoc & _
                    ",TrackingColor = '" & pTrackingColor & "' " & _
                    ",TextColor = '" & pTextColor & "' " & _
                    ",GroupLogo = '" & Replace(pGroupLogo, "'", "''") & "'" & _
                    ",ReceiptReplyName = '" & Replace(pRRTitle, "'", "''") & "'" & _
                    " WHERE GroupId = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory
            If pDesc <> "" And pDesc <> pDesc_o Then
                oHist.pTableName = "Group"
                oHist.pRecordId = Replace(pGroupID, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Group Name"
                oHist.pModifiedDate = ModifiedDate
                oHist.pOldValue = Replace(pDesc_o, "'", "''")
                oHist.pNewValue = Replace(pDesc, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pReportAccess <> pReportAccess_o Then
                oHist.pTableName = "Group"
                oHist.pRecordId = Replace(pGroupID, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Report Access"
                oHist.pModifiedDate = ModifiedDate
                oHist.pOldValue = IIf(pReportAccess_o = "1", "Yes", "No")
                oHist.pNewValue = IIf(pReportAccess = "1", "Yes", "No")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
    Public Sub DeleteGroup(ByVal objcommand As clsSqlConn)

        Try
            Dim s_sql As String = ""
            If DocSession.OraClient Then
                s_sql = "BEGIN DELETE FROM GroupDocAccess WHERE GroupId = '" & pGroupID & "'; " & _
                        "DELETE FROM Groups   WHERE GroupId = '" & pGroupID & "'; END; "
            Else
                s_sql = "DELETE FROM GroupDocAccess WHERE GroupId = '" & pGroupID & "' " & _
    "DELETE FROM Groups   WHERE GroupId = '" & pGroupID & "' "
            End If
            

            objcommand.CommandType = CommandType.Text
            objcommand.CommandText = s_sql
            objcommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pGroupID, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue =
            oHist.pNewValue = "Deleted Group (" & pDesc & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objcommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try

    End Sub

    Public Sub DeleteStatus(ByVal objcommand As clsSqlConn)

        Try

            Dim s_sql As String = ""
            If DocSession.OraClient Then
                s_sql = "BEGIN DELETE FROM GroupStatus WHERE StatusId = '" & pStatusId & "'; " & _
    "DELETE FROM DocStatus  WHERE StatusId = '" & pStatusId & "'; END; "
            Else
                s_sql = "DELETE FROM GroupStatus WHERE StatusId = '" & pStatusId & "' " & _
    "DELETE FROM DocStatus  WHERE StatusId = '" & pStatusId & "' "
            End If

            objcommand.CommandType = CommandType.Text
            objcommand.CommandText = s_sql
            objcommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Status"
            oHist.pRecordId = Replace(pStatusId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue =
            oHist.pNewValue = "Deleted Status (" & pStatusDesc & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objcommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try

    End Sub

    Public Sub DeleteGroupDocAccess(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupDocAccess WHERE groupid = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pGroupID, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Updated Group Access"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
    Public Sub DeleteGroupDocTypeAccess(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupDocTypeAccess WHERE groupid = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pGroupID, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Updated Group Access"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub DeleteGroupStatus(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupStatus WHERE statusid = " & pStatusId & " and groupid = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            'Dim oHist As New DocHistory
            'oHist.pTableName = "Group"
            'oHist.pRecordId = Replace(pGroupID, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = ""
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue = ""
            'oHist.pNewValue = "Updated Group Access"
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
    Public Sub DeleteOfficeAccess(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupOfficeAccess WHERE officecode = '" & Replace(pOfficeCode, "'", "''") & "' and groupid = '" & pGroupID & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub


    Public Sub AddGroup(ByVal objCommand As clsSqlConn)

        Try

            'objCommand.ClearParameter()
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)
            'NOTE: USED ARCHIVED column to know if group does have Edit Index Security Group
            Dim lsSql As String = "INSERT INTO groups " & _
           "(GroupId,GroupName,PasswordExpiryDays,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,EditIndex,ImportDoc,VersionControl,CanDownload,CanPrint,TrackingColor,TextColor,ReportAccess,AlwaysAllowed,OfficeCode,MainGroupId) " & _
            "VALUES " & _
           "('" & Replace(pGroupID.Trim.ToUpper, "'", "''") & "'" & _
           ",'" & Replace(pDesc, "'", "''") & "'" & _
           ",30 " & _
           ",'" & pUserId & "'" & _
           "," & DocSession.CurrentDate & " " & _
           ",'" & pUserId & "'" & _
           "," & DocSession.CurrentDate & _
           "," & pEditIndex & "" & _
           "," & pImportDoc & "" & _
            "," & pVersionControl & "" & _
            "," & pCanPrint & "" & _
           "," & pCanDownLoad & "" & _
           ",'" & pTrackingColor & "'" & _
           ",'" & pTextColor & "'" & _
           "," & CStr(pReportAccess) & ",1" & ",'" & Replace(pOfficeCode, "'", "''") & "'" & ",'" & Replace(pMainGroupId.Trim.ToUpper, "'", "''") & "')"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pGroupID, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Group (" & Replace(pDesc, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub AddGroupDocAccess(ByVal objCommand As clsSqlConn)

        Try

            'objCommand.ClearParameter()'
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)

            Dim lsSql As String = "INSERT INTO GroupDocAccess " & _
                "(GroupId,DocId,GroupAccessId,CanPrint,VersionControl,CanDownload,CanPrintReceipt) " & _
                "VALUES " & _
                "('" & Replace(pGroupID, "'", "''") & "'" & _
                ",'" & Replace(pDocType, "'", "''") & "'" & _
           "," & pGroupAccessId & _
           "," & pCanPrint & _
           "," & pVersionControl & _
           "," & pCanDownLoad & _
           "," & pCanPrintReceipt & ")"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            Dim lsNewValue As String = ""
            lsNewValue = "Added  (" & Replace(pDocTypeDesc, "'", "''") & ": Access - " & Replace(pGroupAccessDesc, "'", "''")
            If pCanDownLoad = "1" Then
                lsNewValue = lsNewValue & "-" & "Download/Share"
            End If
            If pCanPrint = "1" Then
                lsNewValue = lsNewValue & "-" & "Print"
            End If
            If pVersionControl = "1" Then
                lsNewValue = lsNewValue & "-" & "Version Control"
            End If
            If pCanPrintReceipt = "1" Then
                lsNewValue = lsNewValue & "-" & "Print Receipt"
            End If
            lsNewValue = lsNewValue & ")"
            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Access"
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = lsNewValue
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
    Public Sub AddGroupDocTypeAccess(ByVal objCommand As clsSqlConn)

        Try

            'objCommand.ClearParameter()'
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)

            Dim lsSql As String = "INSERT INTO GroupDocTypeAccess " & _
                "(GroupId,DocType) " & _
                "VALUES " & _
                "('" & Replace(pGroupID, "'", "''") & "'" & _
                ",'" & Replace(pDocType, "'", "''") & "')"
         

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            Dim lsNewValue As String = ""
            lsNewValue = "Added (" & Replace(pDocTypeDesc, "'", "''")
            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Access"
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = lsNewValue
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub
    Public Sub CopyGroupDocAccess(ByVal objCommand As clsSqlConn, ByVal aGroupId As String)

        Try

            'objCommand.ClearParameter()'
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)

            Dim lsSql As String = "INSERT INTO GroupDocAccess " & _
                "(GroupId,DocId,GroupAccessId,CanPrint,VersionControl,CanDownload,CanPrintReceipt) " & _
                "select '" & Replace(aGroupId, "'", "''") & "',DocId,GroupAccessId, CanPrint, VersionControl, CanDownload, CanPrintReceipt from groupdocaccess " & _
                " where groupid = '" & Replace(pGroupID, "'", "''") & "' "


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            Dim lsNewValue As String = ""
            lsNewValue = "Copied access from group  " & Replace(pGroupID, "'", "''")

            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Access"
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = lsNewValue
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub
    Public Sub CopyGroupDocTypeAccess(ByVal objCommand As clsSqlConn, ByVal aGroupId As String)

        Try

            'objCommand.ClearParameter()'
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)

            Dim lsSql As String = "INSERT INTO GroupDocTypeAccess " & _
                "(GroupId,DocType) " & _
                "select '" & Replace(aGroupId, "'", "''") & "',DocType from groupdoctypeaccess " & _
                " where groupid = '" & Replace(pGroupID, "'", "''") & "' "


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            Dim lsNewValue As String = ""
            lsNewValue = "Copied access from group  " & Replace(pGroupID, "'", "''")

            oHist.pTableName = "Group"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Access"
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = lsNewValue
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub
    Public Sub DeleteStatusGroupDocAccess(ByVal objCommand As clsSqlConn)

        Try

            Dim lsSql As String = "DELETE FROM GroupStatus WHERE Statusid = '" & pStatusId & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            'Dim oHist As New DocHistory
            'oHist.pTableName = "Status"
            'oHist.pRecordId = Replace(pStatusId, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = ""
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue = ""
            'oHist.pNewValue = "Updated Group Status Access"
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    
    Public Sub AddDocStatusGroupAccess(ByVal objCommand As clsSqlConn)

        Try

            'objCommand.ClearParameter()'
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@GroupId", pGroupID)
            'objCommand.ParametersAddWithValue("@GroupName", pDesc)
            'objCommand.ParametersAddWithValue("@UserId", pUserId)
            'objCommand.ParametersAddWithValue("@IPAddress", pIPAddress)
            'objCommand.ParametersAddWithValue("@ReportAccess", pReportAccess)

            Dim lsSql As String = "INSERT INTO GroupStatus " & _
                "(GroupId,StatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy) " & _
                "VALUES " & _
                "('" & Replace(pGroupID, "'", "''") & "'" & _
                ",'" & Replace(pStatusId, "'", "''") & "'" & _
                "," & DocSession.InsertDate(ModifiedDate) & _
           ",'" & pUserId & "'" & _
           "," & DocSession.InsertDate(ModifiedDate) & _
           ",'" & pUserId & "'" & ")"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            'Dim oHist As New DocHistory
            'Dim lsNewValue As String = ""
            'lsNewValue = "Added  (" & Replace(pDocTypeDesc, "'", "''") & ": Access - " & Replace(pGroupAccessDesc, "'", "''")
            'If pCanDownLoad = "1" Then
            '    lsNewValue = lsNewValue & "-" & "Download/Share"
            'End If
            'If pCanPrint = "1" Then
            '    lsNewValue = lsNewValue & "-" & "Print"
            'End If
            'If pVersionControl = "1" Then
            '    lsNewValue = lsNewValue & "-" & "Version Control"
            'End If
            'If pCanPrintReceipt = "1" Then
            '    lsNewValue = lsNewValue & "-" & "Print Receipt"
            'End If
            'lsNewValue = lsNewValue & ")"
            'oHist.pTableName = "Status"
            'oHist.pRecordId = Replace(pDocType, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = "Document Access"
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue = ""
            'oHist.pNewValue = lsNewValue
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub AddOfficeGroupAccess(ByVal objCommand As clsSqlConn)

        Try

            
            Dim lsSql As String = "INSERT INTO GroupOfficeAccess " & _
                "(GroupId,OfficeCode) " & _
                "VALUES " & _
                "('" & Replace(pGroupID, "'", "''") & "'" & _
                ",'" & Replace(pOfficeCode, "'", "''") & "')" 

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()

            

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub AddDocStatus(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "INSERT INTO DocStatus " & _
           "(StatusId,Description,StatusType,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Hierarchy) " & _
            "VALUES " & _
           "('" & Replace(pStatusId, "'", "''") & "'" & _
           ",'" & Replace(pStatusDesc, "'", "''") & "'" & _
           ",'" & pStatusType & "'" & _
           ",'" & pUserId & "'" & _
           "," & DocSession.CurrentDate & _
           ",'" & pUserId & "'" & _
           "," & DocSession.CurrentDate & ",0)"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Status"
            oHist.pRecordId = Replace(pStatusId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Status (" & Replace(pStatusDesc, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub UpdateDocStatus(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "UPDATE DocStatus " & _
           "SET Description = '" & Replace(pStatusDesc, "'", "''") & "',StatusType = '" & Replace(pStatusType, "'", "''") & "' ,ModifiedBy = '" & pUserId & "',ModifiedDate = '" & ModifiedDate & "' " & _
            " WHERE StatusId = " & Replace(pStatusId, "'", "''") & " "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Status"
            oHist.pRecordId = Replace(pStatusId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Status (" & Replace(pStatusDesc, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Function RetrieveOfficePartially() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = "Order By Description "
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select * from (SELECT " & _
                      "row_number() over (ORDER BY Description) as rn, " & _
            "g.Description, " & _
            "g.officecode " & _
            "FROM  " & _
            "Office g WHERE rownum <= " & lTop.ToString & " " & _
             ") dtbl " & _
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                      "rn= row_number() over (ORDER BY Description), " & _
            "g.Description, " & _
            "g.officecode " & _
            "FROM  " & _
            "Office g " & _
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
    Public Function RetrieveGroupPartially2() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = "Order By GroupName "
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select * from (SELECT " & _
                      "row_number() over (ORDER BY groupname) as rn, " & _
            "g.groupid, " & _
            "g.groupname, " & _
            "g.officecode " & _
            "FROM  " & _
            "groups WHERE rownum <= " & lTop.ToString & _
             ") dtbl " & _
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else


                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                          "rn= row_number() over (ORDER BY groupname), " & _
                "g.groupid, " & _
                "g.groupname, " & _
                "g.officecode " & _
                "FROM  " & _
                "groups g " & _
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
End Class
