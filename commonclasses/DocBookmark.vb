Public Class DocBookmark
    
    Public Sub New()

    End Sub

    Dim docType As String
    Dim FileName As String
    Dim App As String
    Dim Stat As String
    Dim Title As String

    Dim docId As Integer
    Dim colId As Integer
    Dim colValue As String
    Dim sortCol As String
    Dim groupid As String
    Dim userid As String
    Dim SortOrder As String
    Dim IPAddress As String
    Public Property pUserId() As String
        Get
            Return userid
        End Get
        Set(ByVal value As String)
            userid = value
        End Set

    End Property
    Public Property pGroupId() As String
        Get
            Return groupid
        End Get
        Set(ByVal value As String)
            groupid = value
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

    Public Property pDocId() As Integer
        Get
            Return docId
        End Get
        Set(ByVal value As Integer)
            docId = value
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



    'Public Sub SaveDocIndexValues(ByVal objCommand As clsSqlConn)

    '    Try

    '        objCommand.CommandType = CommandType.StoredProcedure
    '        objCommand.CommandText = "xMSP_DOCINDEXVALUEADD"

    '        objCommand.ParametersClear()
    '        objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        objCommand.ParametersAddWithValue("@DocType", pDocType)
    '        objCommand.ParametersAddWithValue("@ColumnId", pColId)
    '        objCommand.ParametersAddWithValue("@ColValue", pColValue)

    '        objCommand.ExecTranNonQuery()

    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '    Finally


    '    End Try
    'End Sub

    Public Function RetrieveBookMarkTop(ByVal asVal As String) As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim oDL As New DocList
            Dim s_sql As String = ""

            s_sql = "SELECT top " & asVal & " " &
   "dl.docId, " &
"dl.doctype, " &
"dl.refno, " &
   "dl.Title, " &
"u1.FirstName + ' ' + u1.LastName as Originator " &
",('images/doctype/' + case right(dl.FileName,4) " &
"when '.doc' then 'doc.png' " &
"when 'docx' then 'doc.png' " &
"when '.xls' then 'excel.png' " &
"when 'xlsx' then 'excel.png' " &
"when '.ppt' then 'doc.png' " &
"when 'pptx' then 'doc.png' " &
"when '.bmp' then 'bmp.png' " &
"when '.tif' then 'tiff.png' " &
"when 'tiff' then 'tiff.png' " &
"when 'jpeg' then 'jpg.png' " &
"when '.jpg' then 'jpg.png' " &
"when '.gif' then 'gif.png' " &
"when '.png' then 'png.png' " &
"when '.pdf' then 'pdf.png' " &
"when '.zip' then 'zip.png'  " &
"when '.rar' then 'zip.png'  " &
"else 'dms.png' end) AS doctypeimg " &
",5 as GroupAccessId " &
"FROM  DocBookmark db " &
"INNER JOIN	doclist dl ON " &
"db.docId = dl.docId " &
            oDL.DocPendingCountUserSuperSql() 'ok-george-8/10/2016


            s_sql = s_sql & " INNER JOIN users u1 ON " & _
               "dl.CreatedBy = u1.userid " & _
            "WHERE db.userId = '" & pUserId & "' and dl.statusid <> 5 " & _
            "ORDER BY db.datebookmark desc "

            '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _



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
    

    Public Function RetrieveBookMark() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim oDl As New DocList
            Dim s_sql As String

            s_sql = "SELECT " &
   "docAction = '', " &
   "dl.docId, " &
   "dl.refno, " &
"dl.doctype, " &
   "dl.Title, " &
   "dl.ModifiedBy, " &
"dl.filename, " &
   "userName = u.FirstName + ' ' + u.LastName, " &
"Originator = u1.FirstName + ' ' + u1.LastName, " &
   "dl.ModifiedDate, " &
   "dl.IPAddress, " &
   "dl.statusid, " &
"dl.createddate " &
",doctypeimg='images/doctype/' + case right(dl.FileName,4)  " &
"when '.doc' then 'doc.png' " &
"when 'docx' then 'doc.png' " &
"when '.xls' then 'excel.png' " &
"when 'xlsx' then 'excel.png' " &
"when '.ppt' then 'doc.png' " &
"when 'pptx' then 'doc.png' " &
"when '.bmp' then 'bmp.png' " &
"when '.tif' then 'tiff.png' " &
"when 'tiff' then 'tiff.png' " &
"when 'jpeg' then 'jpg.png' " &
"when '.jpg' then 'jpg.png' " &
"when '.gif' then 'gif.png' " &
"when '.png' then 'png.png' " &
"when '.pdf' then 'pdf.png' " &
"when '.zip' then 'zip.png'  " &
"when '.rar' then 'zip.png'  " &
"else 'dms.png' end " &
",BookMarked = case when db.DocId is null then 0 else 1 end " &
",doctags = case when dth.doctags is null then '' else dth.doctags end	 " &
",description= case when ds.description is null then '' else ds.description end " &
",5 as GroupAccessId " &
",dt.docname " &
"FROM  DocBookmark db " &
"INNER JOIN	doclist dl ON " &
"db.docId = dl.docId " &
        oDl.DocPendingCountUserSuperSql() & " " &
"INNER JOIN users u ON " &
   "dl.ModifiedBy = u.userid " &
"INNER JOIN users u1 ON " &
   "dl.CreatedBy = u1.userid " &
"INNER JOIN doctype dt ON  " &
"dl.doctype = dt.doctype " &
"LEFT JOIN DocTagsHeader dth " &
"ON dl.docId = dth.docId " &
"LEFT JOIN DocStatus ds " &
    "ON dl.statusid = ds.statusid " &
"WHERE db.userId = '" & pUserId & "' " &
"ORDER BY db.datebookmark desc "
            '"INNER JOIN GroupDocAccess G ON g.groupId = '" & pGroupId & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _

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

    Public Sub DocBookmark(ByVal asBM As String)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text

            If asBM = "D" Then
                objCommand.pCommandText = "DELETE FROM DocBookmark WHERE docId = '" & pDocId & "' and userId = '" & pUserId & "' "
                '"xMSP_DOCBOOKMARKDELETE"
            Else
                objCommand.pCommandText = "INSERT INTO DocBookmark " & _
           "(DocId,UserId,dateBookmark) " & _
                "VALUES (" & CStr(pDocId) & ",'" & pUserId & "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ")"    '"xMSP_DOCBOOKMARKADD"

            End If
            'objCommand.ParametersAddWithValue("@DocId", aiDocId)
            'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
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

    

    
End Class

