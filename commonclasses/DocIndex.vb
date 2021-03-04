Public Class DocIndex

    Public Sub New()

    End Sub
    Dim docType As String
    Dim FileName As String
    Dim Confidential As String
    Dim App As String
    Dim Stat As String
    Dim Title As String
    Dim CreatedDate As String
    Dim OfcCode As String
    Dim RequestType As String
    Dim docId As Integer
    Dim docAttachId As String
    Dim colId As Integer
    Dim SeqNo As String
    Dim colValue As String
    Dim sortCol As String
    Dim SortOrder As String
    Dim IPAddress As String
    Dim CCTo As String
    Dim Internal As String
    Dim SetRetention As String
    Dim ReturnCard As String
    Dim UploaderOfc As String = ""
    Dim ArchiverOfc As String = ""
    Dim UploaderGrp As String = ""
    Dim ArchiverGrp As String = ""
    Public Property pConfidential() As String
        Get
            Return Confidential
        End Get
        Set(ByVal value As String)
            Confidential = value
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
    Dim RetentionStartDate As String = ""
    Public Property pRetentionStartDate() As String
        Get
            Return RetentionStartDate
        End Get
        Set(ByVal value As String)
            RetentionStartDate = value
        End Set

    End Property
    Public Property pReturnCard() As String
        Get
            Return ReturnCard
        End Get
        Set(ByVal value As String)
            ReturnCard = value
        End Set

    End Property
    Public Property pInternal() As String
        Get
            Return Internal
        End Get
        Set(ByVal value As String)
            Internal = value
        End Set

    End Property

    Dim AddRemarks As String

    Public Property pAddRemarks() As String
        Get
            Return AddRemarks
        End Get
        Set(ByVal value As String)
            AddRemarks = value
        End Set

    End Property
    Dim FileSize As String

    Public Property pFileSize() As String
        Get
            Return FileSize
        End Get
        Set(ByVal value As String)
            FileSize = value
        End Set

    End Property
    Public Property pOfcCode() As String
        Get
            Return OfcCode
        End Get
        Set(ByVal value As String)
            OfcCode = value
        End Set

    End Property

    Public Property pArchiverOfc() As String
        Get
            Return ArchiverOfc
        End Get
        Set(ByVal value As String)
            ArchiverOfc = value
        End Set

    End Property
    
    Public Property pUploaderOfc() As String
        Get
            Return UploaderOfc
        End Get
        Set(ByVal value As String)
            UploaderOfc = value
        End Set

    End Property
    Public Property pArchiverGrp() As String
        Get
            Return ArchiverGrp
        End Get
        Set(ByVal value As String)
            ArchiverGrp = value
        End Set

    End Property

    Public Property pUploaderGrp() As String
        Get
            Return UploaderGrp
        End Get
        Set(ByVal value As String)
            UploaderGrp = value
        End Set

    End Property
    Public Property pCCTo() As String
        Get
            Return CCTo
        End Get
        Set(ByVal value As String)
            CCTo = value
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
    Dim DocSender As String
    Public Property pDocSender() As String
        Get
            Return DocSender
        End Get
        Set(ByVal value As String)
            DocSender = value
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
    Public Property pStat() As String
        Get
            Return Stat
        End Get
        Set(ByVal value As String)
            Stat = value
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
    Dim NoCopies As String
    Public Property pCopies() As String
        Get
            Return NoCopies
        End Get
        Set(ByVal value As String)
            NoCopies = value
        End Set

    End Property
    Dim NoPages As String
    Public Property pPages() As String
        Get
            Return NoPages
        End Get
        Set(ByVal value As String)
            NoPages = value
        End Set

    End Property
    Dim MannerReceipt As String
    Public Property pMannerReceipt() As String
        Get
            Return MannerReceipt
        End Get
        Set(ByVal value As String)
            MannerReceipt = value
        End Set

    End Property
    Dim refno As String
    Public Property pRefNo() As String
        Get
            Return refno
        End Get
        Set(ByVal value As String)
            refno = value
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
    Public Property pApp() As String
        Get
            Return App
        End Get
        Set(ByVal value As String)
            App = value
        End Set

    End Property
    Public Property pDocType() As String
        Get
            Return docType
        End Get
        Set(ByVal value As String)
            docType = value
        End Set

    End Property

    Public Property pRequestType() As String
        Get
            Return RequestType
        End Get
        Set(ByVal value As String)
            RequestType = value
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
    Public Property pDocAttachId() As String
        Get
            Return docAttachId
        End Get
        Set(ByVal value As String)
            docAttachId = value
        End Set

    End Property

    Public Property pColValue() As String
        Get
            Return colValue
        End Get
        Set(ByVal value As String)
            colValue = value
        End Set

    End Property

    Public Property pColId() As Integer
        Get
            Return colId
        End Get
        Set(ByVal value As Integer)
            colId = value
        End Set

    End Property

    Public Property pSortCol() As String
        Get
            Return sortCol
        End Get
        Set(ByVal value As String)
            sortCol = value
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

    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set

    End Property

    Dim dis As Boolean = False
    Public Property pDIS() As Boolean
        Get
            Return dis
        End Get
        Set(ByVal value As Boolean)
            dis = value
        End Set

    End Property
    

    Public Sub SaveDocIndexValues(ByVal objCommand As clsSqlConn)

        Try
            Dim s_upd As String = "If exists(SELECT * FROM docIndexValues WHERE docId=" & pDocId & " and doctype='" & pDocType & "' and ColumnID = " & CStr(pColId) & ") " & _
  "UPDATE docIndexValues SET [ColValue] = '" & Replace(pColValue, "'", "''") & "' " & _
 "WHERE docId= " & pDocId & " and doctype= '" & Replace(pDocType, "'", "''") & "' and ColumnID = " & CStr(pColId) & " " & _
"ELSE " & _
    "INSERT INTO DocIndexValues " & _
           "(DocId,DocType,ColumnId,ColValue) " & _
     "VALUES " & _
           "('" & pDocId & "', " & _
   "'" & Replace(pDocType, "'", "''") & "', " & _
           " " & CStr(pColId) & ", " & _
           " '" & Replace(pColValue, "'", "''") & "')  "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_upd '"XMSP_DOCINDEXVALUEADD"
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_DOCINDEXVALUEADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@DocType", pDocType)
            'objCommand.ParametersAddWithValue("@ColumnId", pColId)
            'objCommand.ParametersAddWithValue("@ColValue", pColValue)

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception("si " & ex.Message)

        Finally


        End Try
    End Sub

    Public Sub SaveDocAttachIndexValues(ByVal objCommand As clsSqlConn)

        Try
            Dim s_upd As String = "If exists(SELECT * FROM DocAttachIndexValues WHERE docId=" & pDocId & " and docAttachId=" & pDocAttachId & " and doctype='" & pDocType & "' and ColumnID = " & CStr(pColId) & ") " & _
  "UPDATE DocAttachIndexValues SET [ColValue] = '" & Replace(pColValue, "'", "''") & "' " & _
 "WHERE docId= " & pDocId & " and docAttachId=" & pDocAttachId & " and doctype= '" & Replace(pDocType, "'", "''") & "' and ColumnID = " & CStr(pColId) & " " & _
"ELSE " & _
    "INSERT INTO DocAttachIndexValues " & _
           "(DocId,docAttachId,DocType,ColumnId,ColValue) " & _
     "VALUES " & _
           "('" & pDocId & "', " & _
           "'" & Replace(pDocAttachId, "'", "''") & "', " & _
   "'" & Replace(pDocType, "'", "''") & "', " & _
           " " & CStr(pColId) & ", " & _
           " '" & Replace(pColValue, "'", "''") & "')  "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_upd '"XMSP_DOCINDEXVALUEADD"
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_DOCINDEXVALUEADD"
            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@DocType", pDocType)
            'objCommand.ParametersAddWithValue("@ColumnId", pColId)
            'objCommand.ParametersAddWithValue("@ColValue", pColValue)

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception("si " & ex.Message)

        Finally


        End Try
    End Sub

    Public Function RetrieveDocIndex() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim s_sql As String = "SELECT " & pDocId & " AS docid,di.DocType " & _
                ",di.ColumnId " & _
                ",di.ColumnName " & _
                ",di.DataType " & _
             ",(CASE di.DATATYPE WHEN 1 THEN 'Character' WHEN 2 THEN 'Numeric' WHEN 3 THEN 'Boolean' WHEN 4 THEN 'Date' WHEN 5 THEN 'List' ELSE '' END) AS DataTypedESC " & _
                ",di.DataLength " & _
                ",di.DataDecimal "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(div.ColValue,'') AS colValue,CASE NVL(di.DisplayInScreen,1) when 1 then 'Yes' else 'No' end as Display " & _
                "FROM  DocIndex di LEFT JOIN DocIndexValues div " & _
                "ON " & _
                "di.doctype = div.doctype AND " & _
                "div.DocId = " & pDocId & " AND " & _
                "di.columnid = div.columnid  " & _
                "WHERE  di.DocType = '" & pDocType & "'"
                If pDIS Then
                    s_sql = s_sql & " and NVL(di.displayinScreen,0) <> 0 "
                End If

                s_sql = s_sql & "ORDER BY dataDecimal "
            Else
                s_sql = s_sql & ",isnull(div.ColValue,'') AS colValue,CASE ISNULL(di.DisplayInScreen,1) when 1 then 'Yes' else 'No' end as Display  " & _
                "FROM  DocIndex di LEFT JOIN DocIndexValues div " & _
                "ON " & _
                "di.doctype = div.doctype AND " & _
                "div.DocId = " & pDocId & " AND " & _
                "di.columnid = div.columnid  " & _
                "WHERE  di.DocType = '" & pDocType & "'"
                If pDIS Then
                    s_sql = s_sql & " and isnull(di.displayinScreen,0) <> 0 "
                End If

                s_sql = s_sql & "ORDER BY dataDecimal "
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCINDEXGETVALUES"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@DocType", pDocType)


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
    Public Function RetrieveDocAttachIndex() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim s_sql As String = "SELECT " & pDocId & " AS docid,di.DocType " & _
                ",isnull(div.DocAttachId,'') as DocAttachId " & _
                ",di.ColumnId " & _
                ",di.ColumnName " & _
                ",di.DataType " & _
             ",(CASE di.DATATYPE WHEN 1 THEN 'Character' WHEN 2 THEN 'Numeric' WHEN 3 THEN 'Boolean' WHEN 4 THEN 'Date' WHEN 5 THEN 'List' ELSE '' END) AS DataTypedESC " & _
                ",di.DataLength " & _
                ",di.DataDecimal "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(div.ColValue,'') AS colValue,CASE NVL(di.DisplayInScreen,1) when 1 then 'Yes' else 'No' end as Display " & _
                "FROM  DocIndex di LEFT JOIN DocAttachIndexValues div " & _
                "ON " & _
                "di.doctype = div.doctype AND " & _
                "div.DocId = " & pDocId & " AND " & _
                "div.DocAttachId = " & pDocAttachId & " AND " & _
                "di.columnid = div.columnid  " & _
                "WHERE  di.DocType = '" & pDocType & "'"
                If pDIS Then
                    s_sql = s_sql & " and NVL(di.displayinScreen,0) <> 0 "
                End If

                s_sql = s_sql & "ORDER BY dataDecimal "
            Else
                s_sql = s_sql & ",isnull(div.ColValue,'') AS colValue,CASE ISNULL(di.DisplayInScreen,1) when 1 then 'Yes' else 'No' end as Display  " & _
                "FROM  DocIndex di LEFT JOIN DocAttachIndexValues div " & _
                "ON " & _
                "di.doctype = div.doctype AND " & _
                "div.DocId = " & pDocId & " AND " & _
                "div.DocAttachId = " & pDocAttachId & " AND " & _
                "di.columnid = div.columnid  " & _
                "WHERE  di.DocType = '" & pDocType & "'"
                If pDIS Then
                    s_sql = s_sql & " and isnull(di.displayinScreen,0) <> 0 "
                End If

                s_sql = s_sql & "ORDER BY dataDecimal "
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCINDEXGETVALUES"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@DocType", pDocType)


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

    Public Function RetrieveDocTypeIndex() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim s_sql As String = "SELECT di.DocType " & _
      ",di.ColumnId " & _
      ",di.ColumnName " & _
      ",di.DataType " & _
   ", (CASE di.DATATYPE WHEN 1 THEN 'Character' WHEN 2 THEN 'Numeric' WHEN 3 THEN 'Boolean' WHEN 4 THEN 'Date' WHEN 5 THEN 'List' ELSE '' END) AS DataTypedESC " & _
      ",di.DataLength " & _
      ",di.DataDecimal " & _
 ",'' AS colValue " & _
  "FROM  DocIndex DI " & _
"WHERE  di.DocType = '" & Replace(pDocType, "'", "''") & "'"
            If pDIS Then
                s_sql = s_sql & " and isnull(di.displayinScreen,0) <> 0 "
            End If
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCTYPEINDEXGETVALUES"
            'objCommand.ParametersAddWithValue("@DocType", pDocType)


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

    Public Function RetrieveDocIndexPerDocType() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlClient.SqlDataAdapter
        Dim ldata As New DataTable
        Try
            Dim s_sql As String = "SELECT DocType " & _
      ",ColumnId " & _
      ",ColumnName " & _
      ",DataType " & _
   ",(CASE DATATYPE WHEN 1 THEN 'Character' WHEN 2 THEN 'Numeric' WHEN 3 THEN 'Boolean' WHEN 4 THEN 'Date' WHEN 5 THEN 'List' ELSE '' END) AS DataTypedESC " & _
      ",DataLength" & _
      ",DataDecimal " & _
      ",Case When DisplayInScreen = 0 Then 'No' Else 'Yes' End As Display " & _
      ",Case When DisplayInScreen = 0 Then 'False' Else 'True' End As DisplayInScreen " & _
  "FROM DocIndex " & _
"WHERE DocType = '" & Replace(pDocType, "'", "''") & "'" & _
"ORDER BY  "

            If pSortCol = "Column Name" Then
                s_sql = s_sql & " columnName "
            ElseIf pSortCol = "Data Type" Then
                s_sql = s_sql & " DataType "
            ElseIf pSortCol = "Data Length" Then
                s_sql = s_sql & " DataLength "
            ElseIf pSortCol = "Import Col Seq" Then
                s_sql = s_sql & " DataDecimal "
            Else
                s_sql = s_sql & " DataDecimal "
            End If
            s_sql = s_sql & " " & pSortOrder

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

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

    Public Sub UpdateDocList(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            'objCommand.pCommandText = lssql
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "UPDATE DocList SET " & _
           " Title = '" & Replace(pTitle, "'", "''") & "', " & _
           " RoutedTo = '" & Replace(pApp, "'", "''") & "', " & _
           " TotalCopies = '" & Replace(pCopies, "'", "''") & "', " & _
           " TotalPages = '" & Replace(pPages, "'", "''") & "', " & _
           " DocSender = '" & Replace(pDocSender, "'", "''") & "', " & _
           " RequestType = '" & Replace(pRequestType, "'", "''") & "', " & _
           " MannerReceipt = '" & Replace(pMannerReceipt, "'", "''") & "', " & _
           " InternalDoc = " & pInternal & ", " & _
           " AdditionalInfo = '" & Replace(pAddRemarks, "'", "''") & "' "
            If pSetRetention = "Y" Then
                lssql = lssql & ",RetentionStartDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
            Else
                lssql = lssql & ",RetentionStartDate = null"
            End If
            lssql = lssql & "  WHERE docid = " & pDocId

            objCommand.pCommandText = lssql
            objCommand.ExecTranNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception(ex.Message)



        Finally


        End Try
    End Sub
    Public Sub UpdateRetention(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            'objCommand.pCommandText = lssql
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "UPDATE DocList SET "
           
            If pSetRetention = "Y" Then
                If pRetentionStartDate <> "" Then
                    lssql = lssql & "PurgeStartDate = null,RetentionStartDate = '" & pRetentionStartDate & "' "
                Else
                    lssql = lssql & "PurgeStartDate = null,RetentionStartDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
                End If


            Else
                lssql = lssql & "PurgeStartDate = null,RetentionStartDate = null "
            End If
                lssql = lssql & "  WHERE docid = " & pDocId

                objCommand.pCommandText = lssql
                objCommand.ExecTranNonQuery()

                'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception(ex.Message)



        Finally


        End Try
    End Sub
    Public Sub SaveDocList(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            'objCommand.pCommandText = lssql
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "INSERT INTO DocList " & _
           "(DocId,DocType,Title,FileName,CreatedDate,CreatedBy,ModifiedBy,RequestType " & _
           ",ModifiedDate,IsBeingModified,StatusId,RoutedTo,FirstApprover,OfficeCode,TotalCopies,TotalPages,MannerReceipt,IPAddress,FileVersion,RefNo,CCTo,RoutingSeqNo,InternalDoc,AdditionalInfo,ReturnCard,DocSender,UploaderOfc,UploaderGrp"
            If pSetRetention = "Y" Then
                lssql = lssql & ",RetentionStartDate"
            End If
            If pStat = "8" Then
                lssql = lssql & ",CompletedDate,ArchivedDate,ArchiverOfc,ArchiverGrp,ArchivedBy"
            End If
            If DocSession.sIsLocal Then
                lssql = lssql & ",IsLocal"
            End If
            lssql = lssql & ",Confidential) " & _
            "VALUES " & _
           "(" & pDocId & "," & _
           "'" & pDocType.ToUpper & "'," & _
           "'" & Replace(IIf(pTitle = "", pDocType, pTitle), "'", "''") & "'," & _
           "'" & Replace(pFileName, "'", "''") & "'," & _
           "'" & pCreatedDate & "'," & _
           "'" & DocSession.sUserId & "'," & _
           "'" & DocSession.sUserId & "'," & _
           "'" & Replace(pRequestType, "'", "''") & "'," & _
            "" & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & "," & _
               "0," & _
           "" & pStat & "," & _
           "'" & Replace(pApp, "'", "''") & "'," & _
           "'" & Replace(pApp, "'", "''") & "'," & _
           "'" & Replace(pOfcCode, "'", "''") & "'," & _
           "'" & Replace(pCopies, "'", "''") & "'," & _
           "'" & Replace(pPages, "'", "''") & "'," & _
           "'" & Replace(pMannerReceipt, "'", "''") & "'," & _
           "'" & pIPAddress & "',1,'" & pRefNo & "','" & pCCTo & "','" & pSeqNo & "'," & _
           "'" & Replace(pInternal, "'", "''") & "'," & _
           "'" & Replace(pAddRemarks, "'", "''") & "'," & _
           "'" & Replace(pReturnCard, "'", "''") & "'," & _
           "'" & Replace(pDocSender, "'", "''") & "'," & _
            "'" & Replace(pUploaderOfc, "'", "''") & "'," & _
            "'" & Replace(pUploaderGrp, "'", "''") & "'"

            If pSetRetention = "Y" Then
                lssql = lssql & "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
            End If
            If pStat = "8" Then
                lssql = lssql & ",GETDATE(),GETDATE(),'" & DocSession.sOfcCode & "','" & DocSession.sUserGroup & "','" & DocSession.sUserId & "'"
            End If
            If DocSession.sIsLocal Then
                lssql = lssql & ",1"
            End If
            lssql = lssql & "," & pConfidential & ") "

            'objCommand.ClearParameter()
            'objCommand.ParametersAddWithValue("@DocType", asDocType)
            'objCommand.ParametersAddWithValue("@Title", asTitle)
            'objCommand.ParametersAddWithValue("@FileName", asFileName)
            'objCommand.ParametersAddWithValue("@Location", "")
            'objCommand.ParametersAddWithValue("@UserID", DocSession.sUserId)
            'objCommand.ParametersAddWithValue("@StatusID", aiStat)
            'objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
            'objCommand.ParametersReturnValue()
            ''Dim retval As SqlParameter = objCommand.RetValue
            ''Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue
            objCommand.pCommandText = lssql
            objCommand.ExecTranNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception("d - " & ex.Message)



        Finally


        End Try
    End Sub

    Public Sub SaveDocListFileVersion(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            '"xMSP_DOCLISTADD"
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "INSERT INTO DocFileVersion(docId,docVersion,uploadedDate,uploadedby,FileName,comments,FileSize) " & _
" VALUES (" & pDocId & ",1," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",'" & DocSession.sUserId & "','" & Replace(pFileName, "'", "''") & "','Initial Creation'," & pFileSize & ")"

            objCommand.pCommandText = lssql
            objCommand.ExecTranNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try
    End Sub

    

    
End Class

