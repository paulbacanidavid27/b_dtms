Imports System.Data.SqlClient

Public Class DocTypes
    Public Sub New()

    End Sub

    Dim FileRequired As String
    Dim IpAdd As String
    Dim UserId As String
    Dim DocId As Integer
    Dim GroupId As String
    Dim DocLinkId As String
    Dim DocType As String
    Dim ColumnId As String
    Dim Code As String
    Dim CodeDesc As String
    Dim DocName As String
    Dim DocName_o As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim DataType As String
    Dim ColumnName As String
    Dim DataLength As String
    Dim DataDecimal As String
    Dim RowsPerPage As String
    Dim Idx As String
    Dim Purge As String
    Dim Retention As String
    Dim RetentionPeriod As String
    Dim RetentionStatusId As String
    Dim PurgePeriod As String
    Dim UseCreatedDate As String
    Dim EnableRetention As String
    Dim Confidential As String
    Dim AllowPrinting As String
    Public Property pConfidential() As String
        Get
            Return Confidential
        End Get
        Set(ByVal value As String)
            Confidential = value
        End Set

    End Property
    Public Property pEnableRetention() As String
        Get
            Return EnableRetention
        End Get
        Set(ByVal value As String)
            EnableRetention = value
        End Set

    End Property
    Public Property pUseCreatedDate() As String
        Get
            Return UseCreatedDate
        End Get
        Set(ByVal value As String)
            UseCreatedDate = value
        End Set

    End Property
    Public Property pPurgeDays() As String
        Get
            Return Purge
        End Get
        Set(ByVal value As String)
            Purge = value
        End Set

    End Property

    Public Property pRetentionDays() As String
        Get
            Return Retention
        End Get
        Set(ByVal value As String)
            Retention = value
        End Set

    End Property
    Public Property pPurgePeriod() As String
        Get
            Return PurgePeriod
        End Get
        Set(ByVal value As String)
            PurgePeriod = value
        End Set

    End Property
    Public Property pRetentionPeriod() As String
        Get
            Return RetentionPeriod
        End Get
        Set(ByVal value As String)
            RetentionPeriod = value
        End Set

    End Property
    Public Property pRetentionStatusId() As String
        Get
            Return RetentionStatusId
        End Get
        Set(ByVal value As String)
            RetentionStatusId = value
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
    Public Property pColumnName() As String
        Get
            Return ColumnName
        End Get
        Set(ByVal value As String)
            ColumnName = value
        End Set

    End Property
    Public Property pDataType() As String
        Get
            Return DataType
        End Get
        Set(ByVal value As String)
            DataType = value
        End Set

    End Property

    Public Property pDataLength() As Integer
        Get
            Return DataLength
        End Get
        Set(ByVal value As Integer)
            DataLength = value
        End Set

    End Property

    Public Property pDataDecimal() As Integer
        Get
            Return DataDecimal
        End Get
        Set(ByVal value As Integer)
            DataDecimal = value
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

    Public Property pDocId() As Integer
        Get
            Return DocId
        End Get
        Set(ByVal value As Integer)
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
    Dim Display As String
    Public Property pDisplay() As String
        Get
            Return Display
        End Get
        Set(ByVal value As String)
            Display = value
        End Set

    End Property

    Public Property pColumnId() As String
        Get
            Return ColumnId
        End Get
        Set(ByVal value As String)
            ColumnId = value
        End Set

    End Property

    Public Property pCode() As String
        Get
            Return Code
        End Get
        Set(ByVal value As String)
            Code = value
        End Set

    End Property

    Public Property pCodeDesc() As String
        Get
            Return CodeDesc
        End Get
        Set(ByVal value As String)
            CodeDesc = value
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

    Public Property pIpAddress() As String
        Get
            Return IpAdd
        End Get
        Set(ByVal value As String)
            IpAdd = value
        End Set

    End Property
    Public Property pFileRequired() As String
        Get
            Return FileRequired
        End Get
        Set(ByVal value As String)
            FileRequired = value
        End Set

    End Property
    Public Property pAllowPrinting() As String
        Get
            Return AllowPrinting
        End Get
        Set(ByVal value As String)
            AllowPrinting = value
        End Set

    End Property
    Public Property pDocName() As String
        Get
            Return DocName
        End Get
        Set(ByVal value As String)
            DocName = value
        End Set

    End Property

    Public Property pDocName_o() As String
        Get
            Return DocName_o
        End Get
        Set(ByVal value As String)
            DocName_o = value
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

    Public Function RetrieveDocLinks(ByVal aiDocId As Integer) As DataTable
        Dim ldata As DataTable


        Try
            Dim oLink As New DocLinks
            ldata = oLink.RetrieveDocLinks(aiDocId)
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

    Public Function CheckIfDocTypeExists(ByVal asDocType As String) As Boolean

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT top 1 * FROM doclist WHERE docType = '" & Replace(pDocType, "'", "''") & "' "
            '"xMSP_DOCTYPEEXISTS"
            'objCommand.ParametersAddWithValue("@DocType", asDocType)



            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Function

    Public Function CheckIfInactiveDocTypeExists(ByVal asDocType As String) As Boolean

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT top 1 * FROM doctype WHERE docType = '" & Replace(pDocType, "'", "''") & "' and inactive = 1 "
            '"xMSP_DOCTYPEEXISTS"
            'objCommand.ParametersAddWithValue("@DocType", asDocType)



            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Function
    Public Function CheckIfDocExists(ByVal asDocType As String, ByVal asFileName As String, ByVal asTitle As String) As Boolean

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT * FROM doclist WHERE docType = '" & Replace(asDocType, "'", "''") & "' and FileName = '" & Replace(asFileName, "'", "''") & "' and Title = '" & Replace(asTitle, "'", "''") & "'"
            '"xMSP_DOCEXISTS"
            'objCommand.ParametersAddWithValue("@DocType", asDocType)
            'objCommand.ParametersAddWithValue("@FileName", asFileName)
            'objCommand.ParametersAddWithValue("@Title", asTitle)


            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally



            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Sub SaveDocLinks(ByVal objCommand As clsSqlConn)

        Try
            Dim oLink As New DocLinks
            oLink.pDocId = pDocId
            oLink.pLinkDocId = pLinkDocId
            oLink.pUserId = pUserId
            oLink.pIpAddress = pIpAddress
            oLink.SaveDocLinks(objCommand)
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_DOCLINKSADD"

            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@LinkDocId", pLinkDocId)
            'objCommand.ParametersAddWithValue("@CreatedBye", pUserId)
            'objCommand.ParametersAddWithValue("@CreateIPAddress", pIpAddress)

            'objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Function GetDocType() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            s_sql = "SELECT D.DocType,D.DocName,' ' as GDocType " ',G.GroupAccessId,g.groupId "
            
            s_sql = s_sql & "FROM DocType D " & _
                                        "WHERE (inactive is null or inactive = 0) "

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
    Public Function GetGroupDocType() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_DOCTYPE"
            s_sql = "SELECT D.DocType,D.DocName " & _
            "FROM DocType D inner join " & _
            "GroupDocTypeAccess G ON D.docType = g.doctype and g.GroupId = '" & Replace(pGroupId, "'", "''") & "' " & _
            "WHERE (inactive is null or inactive = 0) "

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
    Public Function GetDocType2() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_DOCTYPE"
            s_sql = "SELECT D.DocType,D.DocName "
            
            s_sql = s_sql & "FROM DocType D WHERE (inactive is null or inactive = 0) ORDER BY d.docname"

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

    Public Function GetDocTypeForDDL() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_DOCTYPE"
            s_sql = "SELECT D.DocName, "
            If DocSession.OraClient Then
                s_sql = s_sql & "D.DocType "
            Else
                s_sql = s_sql & " D.DocType "
            End If
            s_sql = s_sql & "FROM DocType D inner join " & _
                                    "GROUPDOCACCESS G ON D.docType = g.docId and G.GroupAccessId > 1 and g.GroupId = '" & Replace(pGroupId, "'", "''") & "' " & _
                                        "WHERE (inactive is null or inactive = 0) "

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
    Public Function GetDocTypeForDL() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_DOCTYPE"
            s_sql = "SELECT D.DocName, "
            If DocSession.OraClient Then
                s_sql = s_sql & "D.DocType||';'|| case when NVL(d.EnableRetention,0) = 1 AND NVL(d.UseCreatedDate,0) = 1 then 'Y' else 'N' end  AS DocType "
            Else
                s_sql = s_sql & " D.DocType+';'+case when ISnull(d.EnableRetention,0) = 1 AND ISnull(d.UseCreatedDate,0) = 1 then 'Y' else 'N' end AS DocType "
            End If
            s_sql = s_sql & "FROM DocType D inner join " & _
                                    "GROUPDOCACCESS G ON D.docType = g.docId and G.GroupAccessId > 1 and g.GroupId = '" & Replace(pGroupId, "'", "''") & "' " & _
                                        "WHERE (inactive is null or inactive = 0) "

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
    Public Function GetSelectedDocStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "SELECT statusid,description " & _
                                        "FROM docstatus " & _
                                        "WHERE statusid in (1,3,4,7,8,9) "


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

    Public Function GetAllDocTypes() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_sql As String = ""
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "xMSP_DOCTYPE"
            s_sql = "SELECT D.DocType,D.DocName,' ' as GDocType,g.groupId, "
            If DocSession.OraClient Then
                s_sql = s_sql & "NVL(G.GroupAccessId,0) AS GroupAccessId, " & _
                                        "NVL(case when g.CanPrint = 1 then 'Yes' else 'No' end,'No') AS CanPrint, " & _
                                        "NVL(case when g.VersionControl = 1 then 'Yes' else 'No' end,'No') AS CanControl" & _
                                        ",NVL(case when g.CanDownload = 1 then 'Yes' else 'No' end,'No') AS CanDownload" & _
                                        ",NVL(case when g.CanPrintReceipt = 1 then 'Yes' else 'No' end,'No') AS CanPrintReceipt "
            Else
                s_sql = s_sql & "isnull(G.GroupAccessId,0) AS GroupAccessId, " & _
                                        "isnull(case when g.CanPrint = 1 then 'Yes' else 'No' end,'No') AS CanPrint, " & _
                                        "isnull(case when g.VersionControl = 1 then 'Yes' else 'No' end,'No') AS CanControl" & _
                                        ",isnull(case when g.CanDownload = 1 then 'Yes' else 'No' end,'No') AS CanDownload" & _
                                        ",isnull(case when g.CanPrintReceipt = 1 then 'Yes' else 'No' end,'No') AS CanPrintReceipt "
            End If


            s_sql = s_sql & "FROM DocType D left join " & _
                                    "GROUPDOCACCESS G ON D.docType = g.docId and G.GroupAccessId > 2 and g.GroupId = '0' " & _
                                        "WHERE (inactive is null or inactive = 0) "
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

    Public Function GetDocTypeIndex() As DataSet
        Dim ldata As DataSet
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT distinct " & _
  "di.ColumnName" & _
  ",di.DataDecimal " & _
  "FROM DocIndex DI " & _
  "WHERE di.DocType IN (" & pDocType & ") AND di.DataDecimal > 0 ORDER BY di.datadecimal" & _
  " " & _
  "SELECT  " & _
  "di.DocType" & _
  ",di.ColumnId" & _
  ",di.ColumnName" & _
  ",ColSeq = di.DataDecimal " & _
  ",(case when DisplayInScreen=1 then 'True' Else 'False' End) AS DisplayInScreen " & _
  "FROM DocIndex DI " & _
  "WHERE di.DocType IN (" & pDocType & ") AND di.DataDecimal > 0 ORDER BY di.doctype,di.datadecimal "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCTYPEINDEXES"
            'objCommand.ParametersAddWithValue("@Doctypes", pDocType)

            ldata = objCommand.ExecDataSet


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

    Public Function GetDocStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT StatusId,Description FROM DocStatus WHERE statusid <> 5 ORDER BY Description "
            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

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

    Public Function GetDocStatus(ByVal asParam) As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT StatusId,Description FROM DocStatus WHERE statusid <> 5 " & asParam & " ORDER BY Description "
            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

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

    Public Function IsFileRequired() As Boolean
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT DocumentRequired FROM DocType Where Doctype = '" & pDocType & "' "
            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

            If objCommand.ExecScalar3 = "True" Then
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

    Public Function GetDocTypeRetentionInfo() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "SELECT nvl(EnableRetention,0) as EnableRetention," & _
                                         "nvl(UseCreatedDate,0) as UseCreatedDate,nvl(RetentionStatus,0) as RetentionStatus " & _
                                         "FROM DocType Where Doctype = '" & pDocType & "' "
            Else
                objCommand.CommandText = "SELECT Isnull(EnableRetention,0) as EnableRetention," & _
                                         "Isnull(UseCreatedDate,0) as UseCreatedDate,Isnull(RetentionStatus,0) as RetentionStatus " & _
                                         "FROM DocType Where Doctype = '" & pDocType & "' "
            End If

            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

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

    Public Function GetDocStatusByGroup() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.sUserRole = "U" Then
                objCommand.CommandText = "SELECT gs.StatusId,ds.Description FROM GroupStatus gs Inner Join DocStatus ds ON gs.statusid = ds.statusid and ds.StatusId in (3,4,18) Where gs.GroupId = '" & pGroupId & "' ORDER BY ds.Description "
            Else
                objCommand.CommandText = "SELECT gs.StatusId,ds.Description FROM GroupStatus gs Inner Join DocStatus ds ON gs.statusid = ds.statusid Where gs.GroupId = '" & pGroupId & "' and ds.StatusType='I' ORDER BY ds.Description "
            End If

            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

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

    Public Function GetOutgoingDocStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = "SELECT gs.StatusId,ds.Description FROM GroupStatus gs Inner Join DocStatus ds ON gs.statusid = ds.statusid Where gs.GroupId = '" & pGroupId & "' and ds.StatusType='O' ORDER BY ds.Description "


            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

            ldata = objCommand.Fill
            lrow = ldata.NewRow()
            lrow("StatusId") = 0
            lrow("Description") = ""
            ldata.Rows.InsertAt(lrow, 0)
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

    Public Function GetUserDocStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim lrow As DataRow
        Try
            
            Dim s_sql As String = "SELECT gs.StatusId,ds.Description FROM GroupStatus gs Inner Join DocStatus ds ON gs.statusid = ds.statusid Where gs.GroupId = '" & pGroupId & "' "

            If DocSession.sUserRole = "U" Then
                s_sql = s_sql & " AND gs.statusid <> 8 "
            End If

            s_sql = s_sql & " ORDER BY ds.Description "


            'objCommand.CommandText = "select statusid,description from docstatus where Description in ('Acknowledged','For Approval','For Printing','For Re-routing to CRD','For Release by CRD/CPRU','For Return (Partial)', 'For Review & Evaluation','For Withdrawal') "
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill
            lrow = ldata.NewRow()
            lrow("StatusId") = 0
            lrow("Description") = ""
            ldata.Rows.InsertAt(lrow, 0)
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

    Public Function GetFinalDocStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim lrow As DataRow
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            'objCommand.CommandText = "SELECT gs.StatusId,ds.Description FROM GroupStatus gs Inner Join DocStatus ds ON gs.statusid = ds.statusid Where gs.GroupId = '" & pGroupId & "' ORDER BY ds.Description "
            objCommand.CommandText = "select statusid,description from docstatus where Description in " & _
                "('Acknowledged','For Approval','For Printing','For Re-routing to CRD','For Release by CRD/CPRU','For Return (Partial)', " & _
                "'For Review & Evaluation','For Withdrawal') "


            ldata = objCommand.Fill
            lrow = ldata.NewRow()
            lrow("StatusId") = 0
            lrow("Description") = ""
            ldata.Rows.InsertAt(lrow, 0)
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

    Public Function GetDocWoDeleteStatus() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT StatusId,Description FROM DocStatus WHERE statusid <> 5 ORDER BY Description "
            'objCommand.ParametersAddWithValue("@groupId", pGroupId)

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
    Public Function CountDocType() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM " & _
              "doctype d INNER JOIN users u ON d.createdby = u.userid " & _
            "WHERE d.inactive = 0 " & WhereClause()




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

    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pDocType <> "" Then
            lswhere = lswhere & " AND d.DocType like '%" & Replace(pDocType, "'", "''") & "%' "
        End If
        If pDocName <> "" Then
            lswhere = lswhere & " AND d.DocName like '%" & Replace(pDocName, "'", "''") & "%' "
        End If
        Return lswhere
    End Function




    Public Function RetrieveDocType() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            Dim s_sql As String = ""
            If DocSession.OraClient Then
                s_sql = "select * from (SELECT " & _
                      "(row_number() over (ORDER BY " & s_order & " " & pSortOrder & ")) AS rn, " & _
              "d.doctype, " & _
              "d.docname, "

                s_sql = s_sql & "NVL(d.approvaltype,'') AS approvaltype, " & _
                    "NVL(d.EnableRetention,0) AS EnableRetention, " & _
                    "NVL(d.retentionperiod,'') as aperiod,NVL(d.retentiondays,0) as adays,NVL(ds.Description,'') as TriggerStatus,NVL(d.retentionstatus,0) as retstatus,NVL(d.purgeperiod,'') as speriod,NVL(d.purgedays,0) as sdays,NVL(d.usecreateddate,0) as ucdt," & _
                      "(Case NVL(d.approvaltype,'') when 'O' then 'Originator' " & _
                         "when 'D' then 'Document Type'  " & _
                         "when 'B' then 'Both' " & _
                         "else '' end) AS apptype, " & _
                      "(NVL(u.FirstName,'')||' '||NVL(u.LastName,'')) AS cby , "
            Else

                s_sql = "select * from (SELECT TOP " & lTop.ToString & " " & _
                                      "(row_number() over (ORDER BY " & s_order & " " & pSortOrder & ")) AS rn, " & _
                              "d.doctype, " & _
                              "d.docname, "

                s_sql = s_sql & "isnull(d.approvaltype,'') AS approvaltype, " & _
                    "isnull(d.EnableRetention,0) AS EnableRetention, " & _
                    "isnull(d.AllowPrinting,0) AS AllowPrinting, " & _
                    "isnull(d.retentionperiod,'') as aperiod,isnull(d.retentiondays,0) as adays,isnull(ds.Description,'') as TriggerStatus,isnull(d.retentionstatus,0) as retstatus,isnull(d.purgeperiod,'') as speriod,isnull(d.purgedays,0) as sdays,isnull(d.usecreateddate,0) as ucdt," & _
                      "(Case isnull(d.approvaltype,'') when 'O' then 'Originator' " & _
                         "when 'D' then 'Document Type'  " & _
                         "when 'B' then 'Both' " & _
                         "else '' end) AS apptype, " & _
                      "(isnull(u.FirstName,'') +' '+ isnull(u.LastName,'')) AS cby , "
            End If

            s_sql = s_sql & "d.createddate,d.DocumentRequired, " & _
                "'' AS gdoctype " & _
             "FROM " & _
              "doctype d INNER JOIN users u ON d.createdby = u.userid " & _
              "left join DocStatus ds on ds.StatusId = d.Retentionstatus " & _
            "WHERE d.inactive = 0 " & WhereClause() & " " & _
                      ") dtbl " & _
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            's_sql = s_sql & "ORDER BY  " & OrderBy() & " " & pSortOrder

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '  "xMSP_DOCTYPEGET"

            'If pDocType <> "" Then
            '    objCommand.ParametersAddWithValue("@DocType", pDocType)
            'End If

            'If pDocName <> "" Then
            '    objCommand.ParametersAddWithValue("@DocName", pDocName)
            'End If

            'objCommand.ParametersAddWithValue("@SortCol", pSortCol)
            'objCommand.ParametersAddWithValue("@SortOrder", pSortOrder)

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
            Return "d.DocType"
        ElseIf pSortCol = "Description" Then
            Return "d.DocName"
        ElseIf pSortCol = "Created Date" Then
            Return "d.CreatedDate"
        ElseIf pSortCol = "Created By" Then
            If DocSession.OraClient Then
                Return "(NVL(u.FirstName,'') +' '+ NVL(u.LastName,''))"
            Else
                Return "(isnull(u.FirstName,'') +' '+ isnull(u.LastName,''))"
            End If

        Else
            Return "d.DocType"
        End If
    End Function
    Public Function RetrieveIndexList(ByVal asData As DataTable, ByVal noofrows As Integer) As DataTable
        Dim loData As New DataTable("tblData")
        Try



            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("Code", Type.GetType("System.String"))
            loData.Columns.Add("CodeDesc", Type.GetType("System.String"))


            Dim loRow As DataRow
            Dim liCtr As Integer
            liCtr = 0
            If asData.Rows.Count > noofrows Then
                noofrows = asData.Rows.Count
            End If

            For liCtr = 1 To noofrows
                loRow = loData.NewRow()

                If Not asData Is Nothing And asData.Rows.Count >= liCtr Then
                    loRow("rowno") = CStr(liCtr)
                    loRow("DocType") = asData(liCtr - 1)("doctype")
                    loRow("ColumnID") = asData(liCtr - 1)("ColumnId")
                    loRow("Code") = asData(liCtr - 1)("Code")
                    loRow("CodeDesc") = asData(liCtr - 1)("CodeDesc")
                Else
                    loRow("rowno") = CStr(liCtr)
                    loRow("DocType") = ""
                    loRow("ColumnID") = ""
                    loRow("Code") = ""
                    loRow("CodeDesc") = ""
                End If
                loData.Rows.Add(loRow)
            Next

            Return loData
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If
            If Not asData Is Nothing Then
                asData.Dispose()
                asData = Nothing
            End If
        End Try
    End Function
    Public Function RetrieveIndexList() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT doctype,columnId,Code,CodeDesc FROM DocIndexList WHERE doctype = '" & Replace(pDocType, "'", "''") & "' AND columnId = " & pColumnId
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


#Region "CUD - Doctype Index List"
    Public Sub SaveIndexList(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "INSERT INTO DocIndexList " & _
           "(DocType,ColumnId,Code,CodeDesc) VALUES ('" & pDocType & "'," & pColumnId & "," & pCode & ",'" & pCodeDesc & "')"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub UpdateIndexList(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "UPDATE DocIndexList SET CodeDesc = '" & pCodeDesc & "' WHERE DocType = '" & pDocType & "' AND ColumnId = " & pColumnId & " AND Code = " & pCode

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub DeleteIndexListItem(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "DELETE FROM DocIndexList  WHERE DocType = '" & pDocType & "' AND ColumnId = " & pColumnId & " AND Code = " & pCode & ""

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
#End Region

#Region "CUD - Doctype"

    Public Sub AddDocType(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String
            If CheckIfInactiveDocTypeExists(pDocType) Then
                lsSQL = "UPDATE DOCTYPE SET Inactive=0,DocName ='" & Replace(pDocName, "'", "''") & "'" & _
                    ",CreatedBy='" & Replace(pUserId, "'", "''") & "',CreatedDate=" & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
                    ",ModifiedBy='" & Replace(pUserId, "'", "''") & "',ModifiedDate=" & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
                    ",ApprovalType='',DocumentRequired=" & pFileRequired & " " & _
                    ",AllowPrinting=" & pAllowPrinting & " " & _
                    ",RetentionDays = " & pRetentionDays & " " & _
                ",RetentionStatus = " & pRetentionStatusId & " " & _
            ",PurgeDays = " & pPurgeDays & " " & _
            ",RetentionPeriod = '" & pRetentionPeriod & "' " & _
            ",PurgePeriod = '" & pPurgePeriod & "' " & _
            ",UseCreatedDate = " & pUseCreatedDate & " " & _
                    " where doctype = '" & Replace(pDocType, "'", "''") & "' "
            Else

                lsSQL = "INSERT INTO DOCTYPE (DocType,DocName,Inactive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ApprovalType,DocumentRequired," & _
                    "UseCreatedDate,RetentionDays,RetentionPeriod,RetentionStatus,PurgePeriod,PurgeDays,EnableRetention,AllowPrinting) " & _
                    " VALUES ('" & Replace(pDocType, "'", "''") & "','" & Replace(pDocName, "'", "''") & "',0,'" & Replace(pUserId, "'", "''") & "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",'" & Replace(pUserId, "'", "''") & "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",''," & pFileRequired & "," & _
                    pUseCreatedDate & ", " & pRetentionDays & ",'" & pRetentionPeriod & "'," & pRetentionStatusId & ",'" & pPurgePeriod & "'," & pPurgeDays & "," & pEnableRetention & "," & pAllowPrinting & ")"

            End If

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "DocType"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Document Type (" & Replace(pDocName, "'", "''") & ")"
            oHist.pIpAddress = pIpAddress
            oHist.LogChanges(objCommand)
        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub UpdateDocType(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "UPDATE DOCTYPE SET DocName = '" & Replace(pDocName, "'", "''") & "',DocumentRequired = " & pFileRequired & _
            ",RetentionDays = " & pRetentionDays & " " & _
                ",RetentionStatus = " & pRetentionStatusId & " " & _
            ",PurgeDays = " & pPurgeDays & " " & _
            ",RetentionPeriod = '" & pRetentionPeriod & "' " & _
            ",PurgePeriod = '" & pPurgePeriod & "' " & _
            ",UseCreatedDate = " & pUseCreatedDate & " " & _
            ",EnableRetention = " & pEnableRetention & " " & _
            ",AllowPrinting = " & pAllowPrinting & " " & _
            " WHERE doctype = '" & Replace(pDocType, "'", "''") & "'"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            If pDocName <> "" And pDocName <> pDocName_o Then
                Dim oHist As New DocHistory
                oHist.pTableName = "DocType"
                oHist.pRecordId = Replace(pDocType, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Description"
                oHist.pModifiedDate = DateTime.Now.ToString
                oHist.pOldValue = Replace(pDocName_o, "'", "''")
                oHist.pNewValue = Replace(pDocName, "'", "''")
                oHist.pIpAddress = pIpAddress
                oHist.LogChanges(objCommand)
            End If

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub DeleteDocType(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "UPDATE DOCTYPE SET Inactive = 1 WHERE doctype = '" & Replace(pDocType, "'", "''") & "'"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()
            Dim oHist As New DocHistory
            oHist.pTableName = "DocType"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = ""
            oHist.pNewValue = "Deleted Document Type (" & Replace(pDocName, "'", "''") & ")"
            oHist.pIpAddress = pIpAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    'Public Sub DeleteDocType()
    '    Dim objCommand As clsSqlConn
    '    Try
    '        objCommand = New clsSqlConn
    '        Dim lsSQL As String = "UPDATE DOCTYPE SET Inactive = 1 WHERE doctype = '" & pDocType & "'"

    '        objCommand.CommandType = CommandType.Text
    '        objCommand.CommandText = lsSQL

    '        objCommand.ExecTranNonQuery()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally


    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '    End Try
    'End Sub


#End Region

#Region "CUD - Doctype Index"


    Public Sub AddIndex(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "INSERT INTO DocIndex (DocType,ColumnId,ColumnName,DataType,DataLength,DataDecimal,DisplayInScreen) " & _
            " VALUES ('" & Replace(pDocType, "'", "''") & "'," & pColumnId & ",'" & Replace(pColumnName, "'", "''") & "'," & pDataType & "," & pDataLength & "," & pDataDecimal & "," & pDisplay & ")"

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "DocType"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Index"
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Index (" & Replace(pColumnName, "'", "''") & ")"
            oHist.pIpAddress = pIpAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub UpdateDocIndex(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "UPDATE DocIndex SET ColumnName = '" & pColumnName & "',DataType = " & pDataType & ",DataLength = " & pDataLength & ",DataDecimal = " & pDataDecimal & ",DisplayInScreen = " & pDisplay & " WHERE doctype = '" & pDocType & "' AND ColumnId = " & pColumnId

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "DocType"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Index"
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = ""
            oHist.pNewValue = "Updated Index (" & Replace(pColumnName, "'", "''") & ")"
            oHist.pIpAddress = pIpAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
    Public Sub DeleteIndex(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "DELETE FROM DocIndex  " & _
            " WHERE docType = '" & pDocType & "' AND columnId = " & pColumnId

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "DocType"
            oHist.pRecordId = Replace(pDocType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Document Index"
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = ""
            oHist.pNewValue = "Deleted Index (" & Replace(pColumnName, "'", "''") & ")"
            oHist.pIpAddress = pIpAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
    Public Sub DeleteIndexList(ByVal objCommand As clsSqlConn)

        Try
            Dim lsSQL As String = "DELETE FROM DocIndexList  " & _
            " WHERE docType = '" & pDocType & "' AND columnId = " & pColumnId

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            'Dim oHist As New DocHistory
            'oHist.pTableName = "DocType"
            'oHist.pRecordId = Replace(pDocType, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = "Document Index"
            'oHist.pModifiedDate = DateTime.Now.ToString
            'oHist.pOldValue = ""
            'oHist.pNewValue = "Deleted Index (" & Replace(pColumnName, "'", "''") & ")"
            'oHist.pIpAddress = pIpAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
#End Region
    Public Function IndexListIsUsed() As Boolean


        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT doctype FROM DocIndexValues WHERE doctype = '" & Replace(pDocType, "'", "''") & "' AND columnId = " & pColumnId

            Return objCommand.ExecHasRow

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try

    End Function

    Public Function IndexListItemIsUsed() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT doctype FROM DocIndexValues WHERE doctype = '" & Replace(pDocType, "'", "''") & "' AND columnId = " & pColumnId & " AND Colvalue = '" & pCode & "'"

            Return objCommand.ExecHasRow

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
