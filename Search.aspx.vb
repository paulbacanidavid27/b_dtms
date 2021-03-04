Imports System.Web.HttpContext
Public Class Search
    Inherits System.Web.UI.Page
    Dim lsTime As DateTime
    Dim lsLabel As String = ""

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Search")
        AddHandler ucUBtn.e_click, AddressOf ShowUpload
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage
    End Sub
    Private Sub ShowUploadMessage()

        Master.ShowMessage(ucUpload.Message)

    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        Try

            SetCurrent(hfCurrent.Value)
            If pnlAS.Visible Then
                RetrieveDocs()
            Else
                'RetrieveDocAction(Master.SetSearchCriteria)
                SearchRecords(Master.SetSearchCriteria)
            End If

            pnlRepeater.Update()
            'ucDB.pIdx = CInt(hfCurrent.Value)
            'ucDB.RetrieveAction(DocSession.sUserId)
            'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
            'hfTotalRows.Value = CStr(ucDB.pRetVal)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub imgLess_Click()
        Try
            Dim lIdx As Integer
            lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
            hfCurrent.Value = CStr(lIdx)
            RetAction()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub

    Private Sub imgGreater_Click()

        Try
            Dim lIdx As Integer
            lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
            hfCurrent.Value = CStr(lIdx)
            DocSession.srchASCurrentPage = CStr(lIdx)
            RetAction()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Try


            Dim lIdx As Integer
            If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
                lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
            Else
                lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
            End If
            hfCurrent.Value = CStr(lIdx)
            RetAction()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

#End Region
    Public Sub ShowUpload()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub

    'Private Sub lbAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddDoc.Click
    '    ucUpload.Visible = True
    'End Sub
    Protected Sub Page_LoadOrig(ByVal sender As Object, ByVal e As System.EventArgs)
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        lsTime = DateTime.Now
        If Not IsPostBack Then
            Dim asParam As String = GetSearchTypeCookie(Request.QueryString("as"))
            hfCurrent.Value = GetCurrent()
            GetRequestType()
            If asParam = "1" Then
                'SetSearchTypeCookie("1")
                'idAdvSrch.Visible = True
                idSrchRslt.Visible = False
                BindCriteria()
                GetCriteria()

                ShowResult()
                'ElseIf Request.QueryString("as") = "1" Then

            Else
                'SetSearchTypeCookie(Request.QueryString("p"))
                HidePanel()
                If hfCurrent.Value = "" Then
                    hfCurrent.Value = "1"
                End If
                If DocSession.SearchOption = "P" Then
                    Master.SetPartialSearch = True
                Else
                    Master.SetExactSearch = True
                End If


                Master.SetSearchCriteria = DocSession.SearchCriteria 'Request.QueryString("p")
                If Master.SetSearchCriteria = "" Then
                    'lMsg.Text = "Please provide a search criteria before clicking the Search button."

                Else
                    SearchRecords(DocSession.SearchCriteria)
                    'RetrieveDocAction(DocSession.SearchCriteria)

                End If
            End If
            If DocSession.sDocAccess > 2 Then
                ucUBtn.Visible = True
            Else
                ucUBtn.Visible = False
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        lsTime = DateTime.Now
        If Not IsPostBack Then
            
            hfCurrent.Value = GetCurrent()
            GetRequestType()
            If DocSession.srchAS = "S" Then
                
                'idSrchRslt.Visible = False
                BindCriteria()
                GetCriteria()
                ShowResult()
            ElseIf DocSession.srchAS = "Q" Then

                HidePanel()

                If hfCurrent.Value = "" Then
                    hfCurrent.Value = "1"
                End If
                If DocSession.SearchOption = "P" Then
                    Master.SetPartialSearch = True
                Else
                    Master.SetExactSearch = True
                End If


                Master.SetSearchCriteria = DocSession.SearchCriteria 'Request.QueryString("p")
                If Master.SetSearchCriteria = "" Then
                    'lMsg.Text = "Please provide a search criteria before clicking the Search button."

                Else
                    SearchRecords(DocSession.SearchCriteria)
                    'RetrieveDocAction(DocSession.SearchCriteria)

                End If
            Else
                ShowPanel()
                BindCriteria()
                GetCriteria()
            End If
            If DocSession.sDocAccess > 2 Then
                ucUBtn.Visible = True
            Else
                ucUBtn.Visible = False
            End If
        End If

    End Sub
    Private Sub BindCriteria()
        Dim oDoc As DocTypes
        Dim ldata As DataTable
        Dim lrow As DataRow
        Try
            oDoc = New DocTypes
            oDoc.pGroupId = DocSession.sUserGroup
            ldata = oDoc.GetDocType()
            dlDocType.DataTextField = "DocName"
            dlDocType.DataValueField = "DocType"

            lrow = ldata.NewRow()

            ldata.Rows.InsertAt(lrow, 0)
            dlDocType.DataSource = ldata
            dlDocType.DataBind()
            ldata = oDoc.GetDocWoDeleteStatus
            lrow = ldata.NewRow()
            ldata.Rows.InsertAt(lrow, 0)
            dlStatus.DataSource = ldata

            dlStatus.DataValueField = "StatusId"
            dlStatus.DataTextField = "Description"
            dlStatus.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub
    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Try
            Response.Redirect("view.aspx")
        Catch ex As Exception
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If

        End Try

    End Sub

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

    Private Sub SearchRecords(ByVal asCriteria As String)
        Dim dlist As DocList
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            If hfTotalRows.Value = "0" OrElse hfTotalRows.Value = "" Then
                hfTotalRows.Value = QuickSearchCount(asCriteria)
            End If

            If CInt(hfTotalRows.Value) > 0 Then

                ldata = QuickSearch(asCriteria)

                If ldata.Rows.Count > 0 Then
                    If ldata.Rows.Count > DocSession.RowsPerPage Then
                        ldata.Rows.RemoveAt(DocSession.RowsPerPage)
                    Else
                    End If


                    ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                    pPager.Update()


                    Repeater1.DataSource = ldata
                    Repeater1.DataBind()
                    lNo.Text = CInt(hfTotalRows.Value).ToString("#,##0")
                Else
                    lNo.Text = "0"
                    hfTotalRows.Value = "0"
                    Master.ShowMessage("No records found for the selected criteria.")
                    Repeater1.DataSource = ldata
                    Repeater1.DataBind()
                    pPager.Update()
                End If
            Else
                lNo.Text = "0"
                hfTotalRows.Value = "0"
                Master.ShowMessage("No records found for the selected criteria.")
            End If

            'lNo.Text = hfTotalRows.Value
            lNoOfRecord.Visible = True
            lNo.Visible = True
            idSrchRslt.Visible = True
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

    'Private Sub RetrieveDocAction(ByVal asCriteria As String)
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim rowcnt As Integer = 0

    '    Try
    '        If Left(asCriteria, 6).ToLower = "refno:" OrElse Left(asCriteria, 6).ToLower = "title:" OrElse Left(asCriteria, 10).ToLower = "attachdoc:" OrElse _
    '            Left(asCriteria, 6).ToLower = "ncano:" OrElse Left(asCriteria, 7).ToLower = "sarono:" OrElse Left(asCriteria, 6).ToLower = "boxno:" OrElse _
    '            Left(asCriteria, 5).ToLower = "aidx:" OrElse Left(asCriteria, 10).ToLower = "aidxncano:" OrElse Left(asCriteria, 11).ToLower = "aidxsarono:" OrElse Left(asCriteria, 10).ToLower = "aidxboxno:" Then

    '        Else
    '            asCriteria = ufParseCriteria(Server.UrlDecode(asCriteria), DocSession.SearchOption)
    '        End If

    '        If CInt(hfTotalRows.Value) = 0 Then
    '            If Left(asCriteria, 6).ToLower.Trim = "ncano:" Then
    '                rowcnt = SaroNCACount(DocSession.NCAColumn, Mid(asCriteria, 7).Trim)
    '            ElseIf Left(asCriteria, 7).ToLower.Trim = "sarono:" Then
    '                rowcnt = SaroNCACount(DocSession.SAROColumn, Mid(asCriteria, 8).Trim)
    '            ElseIf Left(asCriteria, 6).ToLower.Trim = "boxno:" Then
    '                rowcnt = SaroNCACount(DocSession.BOXColumn, Mid(asCriteria, 7).Trim)
    '            ElseIf Left(asCriteria, 10).ToLower.Trim = "attachdoc:" Then
    '                rowcnt = AttachmentCount(Mid(asCriteria, 11).Trim)
    '            Else
    '                rowcnt = RetrieveDocActionCount(asCriteria)
    '            End If

    '            hfTotalRows.Value = CStr(rowcnt)
    '            lNo.Text = CInt(hfTotalRows.Value).ToString("#,##0")
    '        End If

    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text

    '        'objCommand.CommandText = "xMSP_SEARCH"
    '        'If DocSession.SearchOption = "E" Then
    '        'Master.SetExactSearch = False
    '        'Master.SetPartialSearch = True
    '        If Left(asCriteria, 6).ToLower = "ncano:" Then
    '            lsLabel = "NCA No:"
    '            objCommand.CommandText = SearchSaroNCA(Mid(asCriteria, 7).Trim, DocSession.NCAColumn, hfCurrent.Value, DocSession.RowsPerPage) '"XMSP_SEARCH_OPTIMIZED_EXACT"
    '        ElseIf Left(asCriteria, 7).ToLower.Trim = "sarono:" Then
    '            lsLabel = "SARO No:"
    '            objCommand.CommandText = SearchSaroNCA(Mid(asCriteria, 8).Trim, DocSession.SAROColumn, hfCurrent.Value, DocSession.RowsPerPage) '"XMSP_SEARCH_OPTIMIZED_EXACT"
    '        ElseIf Left(asCriteria, 10).ToLower.Trim = "attachdoc:" Then
    '            lsLabel = "Attachment:"
    '            objCommand.CommandText = SearchAttachment(Mid(asCriteria, 11).Trim, hfCurrent.Value, DocSession.RowsPerPage) '"XMSP_SEARCH_OPTIMIZED_EXACT"
    '        ElseIf Left(asCriteria, 6).ToLower = "boxno:" Then
    '            lsLabel = "Box No:"
    '            objCommand.CommandText = SearchSaroNCA(Mid(asCriteria, 7).Trim, DocSession.BOXColumn, hfCurrent.Value, DocSession.RowsPerPage) '"XMSP_SEARCH_OPTIMIZED_EXACT"
    '        Else
    '            lsLabel = ""
    '            'Master.SetExactSearch = True
    '            'Master.SetPartialSearch = False
    '            objCommand.CommandText = BuildSqlSearch(asCriteria, hfCurrent.Value, DocSession.RowsPerPage) '"XMSP_SEARCH_OPTIMIZED"
    '        End If


    '        'objCommand.ParametersAddWithValue("@asCriteria", asCriteria)
    '        'objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
    '        'objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
    '        'objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
    '        'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)


    '        ldata = objCommand.Fill

    '        If ldata.Rows.Count > 0 Then

    '            If ldata.Rows.Count > DocSession.RowsPerPage Then

    '                ldata.Rows.RemoveAt(DocSession.RowsPerPage)

    '            Else

    '            End If

    '            'Dim lstotalrows As String = oDoc.CountGroups

    '            'hfTotalRows.Value = lstotalrows 'oDocs.pRetVal
    '            ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
    '            pPager.Update()


    '            Repeater1.DataSource = ldata
    '            Repeater1.DataBind()
    '        Else
    '            Master.ShowMessage("No records found for the selected criteria.")
    '            Repeater1.DataSource = ldata
    '            Repeater1.DataBind()
    '            pPager.Update()
    '        End If


    '        lNoOfRecord.Visible = True
    '        lNo.Visible = True
    '        idSrchRslt.Visible = True

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

    'End Sub

    Private Function BuildCountSqlSearch(ByVal asCriteria As String) As String
        Dim s_table As String = " ( SELECT distinct dl1.docid FROM DOCLIST dl1 " & _
            " inner join (SELECT " & _
    "dl.docid " & _
    "FROM doclist dl " & _
        "INNER Join " & _
            "( SELECT dl.docId " & _
                "FROM doclist dl " & _
                    "INNER JOIN doctype dt ON  " & _
                        "dl.doctype = dt.doctype " & _
                "WHERE	 " & BuildWhereClause(asCriteria) & _
        " ) " & _
    "tbl_result " & _
        "ON dl.docid = tbl_result.docid " & _
    "WHERE dl.statusid <> 5 AND dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) " & _
    ") dlid " & _
    " on dl1.docid = dlid.docid " & _
    " LEFT JOIN docRouting dr1 " & _
    " on dr1.docid = dl1.docid " & _
    " WHERE dl1.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.CreatedBy = '" & DocSession.sUserId & "' " & _
    " OR dr1.ApproverId = '" & DocSession.sUserId & "' " & _
    ") "


        Dim s_sql As String = " Select count(dl.docid) " & _
            " FROM doclist dl " & _
" INNER JOIN " & _
    s_table & _
" T " & _
    "ON dl.docid = t.docid " & _
"INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
"INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " & _
"INNER JOIN users u ON dl.ModifiedBy = u.userid " & _
"INNER JOIN users u1 ON dl.CreatedBy = u1.userid " & _
"LEFT JOIN DocBookmark db ON dl.docId = db.docId and db.userid= '" & DocSession.sUserId & "' "

        Return s_sql
    End Function
    Private Function BuildCountSqlSearchData(ByVal asCriteria As String) As String
        Dim s_table As String = " ( SELECT distinct dl1.docid FROM DOCLIST dl1 " & _
            " inner join (SELECT " & _
    "dl.docid " & _
    "FROM doclist dl " & _
        "INNER Join " & _
            "( SELECT dl.docId " & _
                "FROM doclist dl " & _
                    "INNER JOIN doctype dt ON  " & _
                        "dl.doctype = dt.doctype " & _
                "WHERE	 " & BuildWhereClause(asCriteria) & _
        " ) " & _
    "tbl_result " & _
        "ON dl.docid = tbl_result.docid " & _
    "WHERE dl.statusid <> 5 AND dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) " & _
    ") dlid " & _
    " on dl1.docid = dlid.docid " & _
    " LEFT JOIN docRouting dr1 " & _
    " on dr1.docid = dl1.docid " & _
    " WHERE dl1.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.CreatedBy = '" & DocSession.sUserId & "' " & _
    " OR dr1.ApproverId = '" & DocSession.sUserId & "' " & _
    ") "


        Dim s_sql As String = " Select count(dl.docid) " & _
            " FROM doclist dl " & _
" INNER JOIN " & _
    s_table & _
" T " & _
    "ON dl.docid = t.docid " & _
"INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
"INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " & _
"INNER JOIN users u ON dl.ModifiedBy = u.userid " & _
"INNER JOIN users u1 ON dl.CreatedBy = u1.userid " & _
"LEFT JOIN DocBookmark db ON dl.docId = db.docId and db.userid= '" & DocSession.sUserId & "' "

        Return s_sql
    End Function
    Private Function BuildWhereClause(ByVal asCriteria) As String
        Dim s_sql As String = ""
        If Left(asCriteria, 6).ToLower = "refno:" Then
            asCriteria = Mid(asCriteria, 7).Trim
            If DocSession.SearchOption = "E" Then
                s_sql = s_sql & "dl.refno = '" & asCriteria & "' "
            Else
                s_sql = s_sql & "dl.refno Like '%" & asCriteria & "%' "
            End If
        ElseIf Left(asCriteria, 6).ToLower = "local:" Then
            s_sql = s_sql & "isnull(dl.IsLocal,0) = 1 "
        ElseIf Left(asCriteria, 6).ToLower = "title:" Then
            asCriteria = Mid(asCriteria, 7).Trim
            If DocSession.SearchOption = "E" Then
                s_sql = s_sql & "dl.title = '" & asCriteria & "' "
            Else
                s_sql = s_sql & "dl.title Like '%" & asCriteria & "%' "
            End If
        Else
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

        End If

        Return s_sql
    End Function
    Private Function BuildSqlSearch(ByVal asCriteria As String, ByVal asIdx As String, ByVal asRowsPerPage As String) As String
        Dim s_sql As String
        s_sql = "SELECT " & _
        "t.rn,colvalue='',docAction = '', " & _
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
        "," & IIf(DocSession.OraClient, "NVL(", "ISNULL(") & "dl.DocPurgedDate,'') as PurgedDate " & _
        ",ds.description "

        If DocSession.OraClient Then
            s_sql = s_sql & ",1 AS GroupAccessId  " & _
                        ",(NVL(u.FirstName,'') + ' ' + NVL(u.LastName,'')) AS userName, " & _
        ",(NVL(u1.FirstName,'') + ' ' + NVL(u1.LastName,'')) AS Originator " & _
                        ",NVL(dl.refno,'') AS refno " & _
                    ", isnull(u2.FirstName,u1.FirstName)+' '+isnull(u2.LastName,u1.LastName) as PersonnelInCharge "

        Else
            s_sql = s_sql & ",1 AS GroupAccessId " & _
                    ",(ISNULL(u.FirstName,'') + ' ' + ISNULL(u.LastName,'')) AS userName, " & _
        "(ISNULL(u1.FirstName,'') + ' ' + ISNULL(u1.LastName,'')) AS Originator " & _
                    ",refno=isnull(dl.refno,'') " & _
                    ",ReceivedBy=isnull(dl.ReceivedBy,'') " & _
                    ",ReceivedDate=isnull(convert(char(10),dl.ReceivedDate,101),'') " & _
                    ",ReceivedTime=isnull(right(convert(varchar,dl.ReceivedDate),8),'') " & _
                    ", isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) as PersonnelInCharge "
        End If


        s_sql = s_sql & " FROM doclist dl " & _
        " INNER JOIN " & _
        "( " & _
        " SELECT " & _
            "rn = row_number() over (order by dl.Title asc),dl.docid FROM doclist dl INNER JOIN  " & _
            "( SELECT distinct dl1.docid FROM doclist dl1 INNER JOIN ( SELECT dl.docid " & _
            "FROM doclist dl " & _
                "INNER Join " & _
                    "( SELECT dl.docId " & _
                        "FROM doclist dl " & _
                            "INNER JOIN doctype dt ON  " & _
                                "dl.doctype = dt.doctype " & _
                                "WHERE	 " & BuildWhereClause(asCriteria)


        s_sql = s_sql & " ) " & _
    "tbl_result " & _
        "ON dl.docid = tbl_result.docid  " & _
    "WHERE dl.statusid <> 5 and dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) " & _
    ") dlid " & _
    " on dl1.docid = dlid.docid " & _
    " LEFT JOIN docRouting dr1 " & _
    " on dr1.docid = dl1.docid " & _
    " WHERE dl1.OfficeCode = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.UploaderOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.ArchiverOfc = '" & DocSession.sOfcCode & "' " & _
    " OR dl1.CreatedBy = '" & DocSession.sUserId & "' " & _
    " OR dr1.ApproverId = '" & DocSession.sUserId & "' ) dlx on dlx.DocId =dl.DocId "

        'If DocSession.SearchOption = "E" Then
        '    's_sql = s_sql & " and tbl_result.PersonnelInCharge IN (" & asCriteria & ") "
        '    s_sql = s_sql & " and tbl_result.PersonnelInCharge = '%" & asCriteria & "%' "
        'Else
        '    s_sql = s_sql & " and (" & PartialSearch(asCriteria, "tbl_result.PersonnelInCharge") & ") "
        '    's_sql = s_sql & " and tbl_result.PersonnelInCharge like '%" & asCriteria & "%' "
        'End If


        s_sql = s_sql & ") " & _
        " T " & _
            "ON dl.docid = t.docid " & _
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
            s_sql = s_sql & "WHERE (T.rn between " & asIdx & " and " & asIdx & "+" & asRowsPerPage & ") "
        End If
        Return s_sql

    End Function
    Private Function PartialSearch(ByVal asCriteria As String, ByVal asColumn As String) As String
        If DocSession.SearchOption = "E" Then
            Return asCriteria
        Else
            Return Replace(asCriteria, "<ZZ@@ZZ>", asColumn)
        End If

    End Function

    'Private Function RetrieveDocActionCount(ByVal asCriteria As String) As Integer

    '    Dim objCommand As clsSqlConn

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

    '        'objCommand.CommandText = "xMSP_SEARCH"
    '        'If DocSession.SearchOption = "E" Then
    '        '    objCommand.CommandText = "xMSP_SEARCH_OPTIMIZED_COUNT_EXACT"
    '        'Else
    '        '    objCommand.CommandText = "xMSP_SEARCH_OPTIMIZED_COUNT"
    '        'End If
    '        objCommand.CommandText = BuildCountSqlSearch(asCriteria)
    '        'objCommand.ParametersAddWithValue("@asCriteria", asCriteria)
    '        'objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
    '        'objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
    '        'objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
    '        'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
    '        'objCommand.ParametersReturnValue()
    '        Return objCommand.ExecScalar()


    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        'If Not ldata Is Nothing Then
    '        '    ldata.Dispose()
    '        '    ldata = Nothing
    '        'End If
    '        'If Not adpSecurity Is Nothing Then
    '        '    adpSecurity.Dispose()
    '        '    adpSecurity = Nothing
    '        'End If

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If


    '    End Try

    'End Function
    Private Function QuickSearchCount(ByVal asCriteria As String) As Integer

        Dim objCommand As clsSqlConn
        Dim dlist As DocList
        Try
            dlist = New DocList
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure

            objCommand.CommandText = dlist.QuickCountSql(asCriteria, DocSession.SearchOption)
            
            Return objCommand.ExecScalar()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If
            'If Not adpSecurity Is Nothing Then
            '    adpSecurity.Dispose()
            '    adpSecurity = Nothing
            'End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Function
    Private Function QuickSearch(ByVal asCriteria As String) As DataTable

        Dim objCommand As clsSqlConn
        Dim dlist As DocList
        Try
            dlist = New DocList
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            Dim s_sql As String = dlist.QuickSearchSql(asCriteria, DocSession.SearchOption, DocSession.RowsPerPage, hfCurrent.Value)

            objCommand.CommandText = s_sql
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
    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgB As ImageButton = DirectCast(e.Item.FindControl("ImgBookmark"), ImageButton)
            Dim imgT As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
            Dim imgL As ImageButton = DirectCast(e.Item.FindControl("ImgLink"), ImageButton)
            Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
            Dim imgBx As ImageButton = DirectCast(e.Item.FindControl("ImgBox"), ImageButton)
            Dim imgCkBx As ImageButton = DirectCast(e.Item.FindControl("ImgCheckBox"), ImageButton)
            Dim imgU As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            'Dim btn As Button = DirectCast(e.Item.FindControl("btSaveTag"), Button)
            'Dim btnSaveRecv As Button = DirectCast(e.Item.FindControl("btReceivedBy"), Button)


            AddHandler imgB.Click, AddressOf BookmarkDocument
            AddHandler imgUB.Click, AddressOf UnBookmarkDocument
            AddHandler imgT.Click, AddressOf TagDocument
            AddHandler imgL.Click, AddressOf LinkDocument
            AddHandler imgBx.Click, AddressOf CheckDoc
            AddHandler imgCkBx.Click, AddressOf CheckDoc
            'AddHandler btn.Click, AddressOf SaveTagDocument
            'AddHandler btnSaveRecv.Click, AddressOf SaveReceiving
            AddHandler imgU.Click, AddressOf ShowDocument
        End If

    End Sub

    Private Sub ShowDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lD As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lDT As Literal = DirectCast(rptItem.FindControl("lDocType"), Literal)
        Dim lDTAccess As Literal = DirectCast(rptItem.FindControl("lGroupAccessId"), Literal)


        Try
            DocSession.sDocID = lD.Text
            DocSession.sDocType = lDT.Text
            DocSession.sDocTypeAccess = lDTAccess.Text
            Response.Redirect("view.aspx")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not imgButton Is Nothing Then
                imgButton.Dispose()
            End If
            If Not rptItem Is Nothing Then
                rptItem.Dispose()
            End If
            If Not lD Is Nothing Then
                lD.Dispose()
            End If
            If Not lDT Is Nothing Then
                lDT.Dispose()
            End If
            If Not lDTAccess Is Nothing Then
                lDTAccess.Dispose()
            End If
        End Try

    End Sub

    Private Sub CheckDoc(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim imgB As ImageButton = DirectCast(rptItem.FindControl("imgBox"), ImageButton)
        Dim imgCB As ImageButton = DirectCast(rptItem.FindControl("imgCheckBox"), ImageButton)

        imgB.Visible = Not imgB.Visible
        imgCB.Visible = Not imgCB.Visible
        If imgCB.Visible Then
            DirectCast(rptItem.FindControl("rw1"), Web.UI.HtmlControls.HtmlTableRow).Style.Item("background-color") = "#c2c2c2"
        Else
            DirectCast(rptItem.FindControl("rw1"), Web.UI.HtmlControls.HtmlTableRow).Style.Item("background-color") = "white"
        End If

    End Sub

    Private Sub LinkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim imgCB As ImageButton = DirectCast(rptItem.FindControl("ImgCheckBox"), ImageButton)
        Dim rptL As Repeater = DirectCast(rptItem.FindControl("rptLinks"), Repeater)
        'Dim pnl As Panel = DirectCast(rptItem.FindControl("pLinks"), Panel)
        Try


            If imgCB.Visible Then
                AddLinks(lrDocId.Text)
            End If


            AddHandler rptL.ItemCreated, AddressOf rptLItemCreated
            rptL.DataSource = RetrieveDocLinks(CInt(lrDocId.Text))
            rptL.DataBind()
            rptL.Visible = True
            'pnl.Visible = Not pnl.Visible


            pnlRepeater.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not imgButton Is Nothing Then
                imgButton.Dispose()
            End If
            If Not rptItem Is Nothing Then
                rptItem.Dispose()

            End If
            If Not lrDocId Is Nothing Then
                lrDocId.Dispose()

            End If
            If Not imgCB Is Nothing Then
                imgCB.Dispose()
            End If
            If Not rptL Is Nothing Then
                rptL.Dispose()
            End If
        End Try
    End Sub

    Private Function RetrieveDocLinks(ByVal aiDocId As Integer) As DataTable


        Dim objCommand As clsSqlConn

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
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Private Function RetrieveDocTags(ByVal aiDocId As Integer) As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            Dim oDoc As New DocTags
            ldata = oDoc.RetrieveDocTag(aiDocId)

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
    Private Sub rptLItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgD As ImageButton = DirectCast(e.Item.FindControl("imgLinkDelete"), ImageButton)
            Try
                AddHandler imgD.Click, AddressOf DeleteDocLink
            Catch ex As Exception
            Finally
                If Not imgD Is Nothing Then
                    imgD.Dispose()
                End If
            End Try


        End If

    End Sub

    Private Sub DeleteDocLink(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As Button = DirectCast(sender, Button)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lLDocId As Literal = DirectCast(rptItem.FindControl("lLinkDocId"), Literal)


        DeleteDocLink(CInt(lrDocId.Text), CInt(lLDocId.Text))
        pnlRepeater.Update()

    End Sub

    Private Sub DeleteDocLink(ByVal aiDocId As Integer, ByVal aiLDocId As Integer)
        Try
            Dim oLink As New DocLinks
            oLink.pDocId = aiDocId
            oLink.pLinkDocId = aiLDocId
            oLink.DeleteDocLinks()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub TagDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
        Dim rptT As Repeater = DirectCast(rptItem.FindControl("rptTags"), Repeater)

        lPnl.Visible = Not lPnl.Visible
        'AddHandler rptT.ItemCreated, AddressOf rptLItemCreated
        Try


            If lPnl.Visible Then
                rptT.DataSource = RetrieveDocTags(CInt(lrDocId.Text))
                rptT.DataBind()
                rptT.Visible = True
            End If

            pnlRepeater.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not imgButton Is Nothing Then
                imgButton.Dispose()
            End If
            If Not rptItem Is Nothing Then
                rptItem.Dispose()
            End If
            If Not lrDocId Is Nothing Then
                lrDocId.Dispose()
            End If
            If Not lPnl Is Nothing Then
                lPnl.Dispose()
            End If
            If Not rptT Is Nothing Then
                rptT.Dispose()
            End If
        End Try
    End Sub

    Private Sub SaveTagDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgButton As Button = DirectCast(sender, Button)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lT As Literal = DirectCast(rptItem.FindControl("lTitle"), Literal)
        Dim lttags As Literal = DirectCast(rptItem.FindControl("ltags"), Literal)
        Dim ltxt As TextBox = DirectCast(rptItem.FindControl("txtTag"), TextBox)
        Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
        Dim oTag As New DocTags
        oTag.pDocId = CInt(lrDocId.Text)
        oTag.pIpAddress = Request.UserHostAddress
        oTag.pTag = ltxt.Text
        oTag.pUserId = DocSession.sUserId 'Session("userid")
        oTag.pDocName = lT.Text
        oTag.SaveDocTag()

        lttags.Text = IIf(lttags.Text.Trim = "", ltxt.Text, lttags.Text.Trim & ", " & ltxt.Text)
        lPnl.Visible = Not lPnl.Visible
        pnlRepeater.Update()

    End Sub

    'Private Sub SaveReceiving(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim imgButton As Button = DirectCast(sender, Button)
    '    Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
    '    Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
    '    Dim lT As Label = DirectCast(rptItem.FindControl("lTitle"), Label)
    '    Dim lttags As Literal = DirectCast(rptItem.FindControl("ltags"), Literal)
    '    Dim ltxtRB As TextBox = DirectCast(rptItem.FindControl("txtReceivedBy"), TextBox)
    '    Dim ltxtRT As TextBox = DirectCast(rptItem.FindControl("txtReceivedTime"), TextBox)
    '    Dim ltxtRD As TextBox = DirectCast(rptItem.FindControl("txtReceivedDate"), TextBox)
    '    Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
    '    Dim oTag As New DocList
    '    oTag.pDocId = CInt(lrDocId.Text)
    '    oTag.pIpAddress = Request.UserHostAddress
    '    oTag.pReceivedBy = ltxtRB.Text
    '    oTag.pReceivedDate = ltxtRD.Text
    '    oTag.pReceivedTime = ltxtRT.Text
    '    oTag.pUserId = DocSession.sUserId
    '    oTag.UpdateDoc()


    '    'lPnl.Visible = Not lPnl.Visible
    '    pnlRepeater.Update()

    'End Sub



    Private Sub BookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lBM As Literal = DirectCast(rptItem.FindControl("lBookmarked"), Literal)
        Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgUnbookmark"), ImageButton)
        Dim bm As DocBookmark = New DocBookmark
        Try
            bm.pDocId = CInt(lrDocId.Text)
            bm.pUserId = DocSession.sUserId
            bm.DocBookmark("A")
            'DocBookmark(CInt(lrDocId.Text), "0")
            imgButton.Visible = Not imgButton.Visible
            imgUnButton.Visible = Not imgUnButton.Visible

            pnlRepeater.Update()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not imgButton Is Nothing Then
                imgButton.Dispose()
            End If
            If Not imgUnButton Is Nothing Then
                imgUnButton.Dispose()
            End If
            If Not rptItem Is Nothing Then
                rptItem.Dispose()
            End If
            If Not lrDocId Is Nothing Then
                lrDocId.Dispose()
            End If
            If Not lBM Is Nothing Then
                lBM.Dispose()
            End If
        End Try
    End Sub

    Private Sub AddLinks(ByVal asDocId As Integer)
        Dim ImgCB As ImageButton

        Dim lDocId As Literal

        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer

        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)

            For Each ri In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    ImgCB = CType(ri.FindControl("imgCheckBox"), ImageButton)
                    If ImgCB.Visible Then
                        'If liCtr = 0 Then
                        lDocId = DirectCast(ri.FindControl("lDocId"), Literal)
                        'Else
                        'llDocId = DirectCast(ri.FindControl("lDocId"), Literal)
                        'End If

                        liCtr += 1
                        If lDocId.Text <> asDocId Then
                            SaveDocLinks(objCommand, CInt(asDocId), CInt(lDocId.Text))
                        End If


                    End If
                End If
            Next


            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            'If liCtr > 1 Then
            ltr.pTran.Commit()
            'Else

            'End If

        Catch ex As Exception

            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If


        End Try
    End Sub

    Private Sub UnBookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgBookmark"), ImageButton)
        Dim bm As DocBookmark = New DocBookmark
        Try


            bm.pDocId = CInt(lrDocId.Text)
            bm.pUserId = DocSession.sUserId
            bm.DocBookmark("D")
            'DocBookmark(CInt(lrDocId.Text), "1")
            imgButton.Visible = Not imgButton.Visible
            imgUnButton.Visible = Not imgUnButton.Visible

            pnlRepeater.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not imgButton Is Nothing Then
                imgButton.Dispose()
            End If
            If Not imgUnButton Is Nothing Then
                imgUnButton.Dispose()
            End If
            If Not rptItem Is Nothing Then
                rptItem.Dispose()
            End If
            If Not lrDocId Is Nothing Then
                lrDocId.Dispose()
            End If
        End Try
    End Sub

    'Private Sub DocBookmark(ByVal aiDocId As Integer, ByVal asBM As String)


    '    Dim objCommand As clsSqlConn

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.StoredProcedure

    '        If asBM = 1 Then
    '            objCommand.CommandText = "xMSP_DOCBOOKMARKDELETE"
    '        Else
    '            objCommand.CommandText = "xMSP_DOCBOOKMARKADD"

    '        End If
    '        objCommand.ParametersAddWithValue("@DocId", aiDocId)
    '        objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)

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

    Private Sub SaveDocLinks(ByVal objCommand As clsSqlConn, ByVal aiDocId As Integer, ByVal aiLDocId As Integer)

        Try
            Dim oLink As New DocLinks
            oLink.pDocId = aiDocId
            oLink.pLinkDocId = aiLDocId
            oLink.pUserId = DocSession.sUserId
            oLink.pIpAddress = Request.UserHostAddress
            oLink.SaveDocLinks(objCommand)
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_DOCLINKSADD"

            'objCommand.ParametersClear()
            'objCommand.ParametersAddWithValue("@DocId", aiDocId)
            'objCommand.ParametersAddWithValue("@LinkDocId", aiLDocId)
            'objCommand.ParametersAddWithValue("@CreatedBye", DocSession.sUserId)
            'objCommand.ParametersAddWithValue("@CreateIPAddress", Request.UserHostAddress)

            'objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgB As ImageButton = DirectCast(e.Item.FindControl("ImgBookmark"), ImageButton)
            Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
            Dim lBookmarked As Literal = DirectCast(e.Item.FindControl("lBookmarked"), Literal)
            Dim llGroupAccessId As Literal = DirectCast(e.Item.FindControl("lGroupAccessId"), Literal)
            Dim imgT As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
            Dim imgBx As ImageButton = DirectCast(e.Item.FindControl("ImgBox"), ImageButton)
            'Dim imgL As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
            'Dim pNl As Panel = DirectCast(e.Item.FindControl("pReceiving"), Panel)
            'If DocSession.sUserId = "masantos@dbm.gov.ph" OrElse DocSession.sUserId = "admin" Then
            'pNl.Visible = True
            'End If

            Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))
            If lsLabel <> "" Then
                DirectCast(e.Item.FindControl("valueLabel"), Label).Text = lsLabel
                DirectCast(e.Item.FindControl("pBox"), Panel).Visible = True
            End If
            Try
                lblDocName.ImageUrl = lext
                If CInt(llGroupAccessId.Text) < 2 Then
                    'imgT.Visible = False
                    imgBx.Visible = False
                End If
                If lBookmarked.Text = "1" Then
                    imgB.Visible = False
                    imgUB.Visible = True
                Else
                    imgB.Visible = True
                    imgUB.Visible = False

                End If
                If DirectCast(e.Item.FindControl("lPurgedDate"), Literal).Text.Trim <> "" Then
                    lblDocName.Enabled = False
                    DirectCast(e.Item.FindControl("lbBM"), LinkButton).Enabled = False
                    DirectCast(e.Item.FindControl("lTitle"), Literal).Text = "(Purged) " & HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
                Else
                    DirectCast(e.Item.FindControl("lTitle"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
                End If
                DirectCast(e.Item.FindControl("lrfno"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lrfno"), Literal).Text, DocSession.SearchCriteria)


            Catch ex As Exception
            Finally
                If Not imgB Is Nothing Then
                    imgB.Dispose()
                End If
                If Not imgUB Is Nothing Then
                    imgUB.Dispose()
                End If
                If Not lBookmarked Is Nothing Then
                    lBookmarked.Dispose()
                End If
                If Not llGroupAccessId Is Nothing Then
                    llGroupAccessId.Dispose()
                End If
                If Not imgT Is Nothing Then
                    imgT.Dispose()
                End If
                If Not imgBx Is Nothing Then
                    imgBx.Dispose()
                End If
                If Not lblActive Is Nothing Then
                    lblActive.Dispose()
                End If
                If Not lblDocName Is Nothing Then
                    lblDocName.Dispose()
                End If
            End Try
        End If
    End Sub
    Private Function HighlightText(ByVal asSrc As String, ByVal asSrcVal As String) As String
        If pnlAS.Visible Then
            Return asSrc
        Else

            Dim squery, mystr, str, mystr2, strwithqoute As String
            Dim strArr As Array = Nothing
            Dim ind, ind2 As Integer


            mystr2 = ""
            strwithqoute = ""
            squery = Trim(asSrcVal)
            squery = RemoveSearchKey(squery)
            squery = Replace(squery, "'", "''")

            If squery.Contains(Chr(34)) Then
                squery = Replace(squery, """", "")
                asSrc = Replace(asSrc, squery.ToUpper(), "<span style='background-color:yellow'>" & squery.ToUpper & "</span>")
            Else
                If squery.Contains(" ") Then
                    strArr = squery.Split(" ")

                    For ind = 0 To strArr.Length - 1
                        Replace(asSrc, Trim(strArr(ind)), "<span style='background-color:yellow'>" & Trim(strArr(ind)) & "</span>")
                    Next
                Else
                    asSrc = Replace(asSrc, Replace(squery.ToUpper(), """", ""), "<span style='background-color:yellow'>" & squery.ToUpper & "</span>")
                End If

            End If



            Return asSrc
        End If

    End Function
    Private Function RemoveSearchKey(ByVal asCriteria As String)
        Dim CriteriaValue As String = asCriteria
        If Left(asCriteria, 6).ToLower.Trim = "refno:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
        ElseIf Left(asCriteria, 6).ToLower.Trim = "title:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
        ElseIf Left(asCriteria, 11).ToLower.Trim = "officecode:" Then
            CriteriaValue = Mid(asCriteria, 12).Trim
        ElseIf Left(asCriteria, 9).ToLower.Trim = "filename:" Then
            CriteriaValue = Mid(asCriteria, 10).Trim
        ElseIf Left(asCriteria, 9).ToLower.Trim = "statusid:" Then
            CriteriaValue = Mid(asCriteria, 10).Trim
        ElseIf Left(asCriteria, 10).ToLower.Trim = "ipaddress:" Then
            CriteriaValue = Mid(asCriteria, 11).Trim
        ElseIf Left(asCriteria, 8).ToLower.Trim = "duedate:" Then
            CriteriaValue = Mid(asCriteria, 9).Trim
        ElseIf Left(asCriteria, 14).ToLower.Trim = "completeddate:" Then
            CriteriaValue = Mid(asCriteria, 15).Trim
        ElseIf Left(asCriteria, 13).ToLower.Trim = "archiveddate:" Then
            CriteriaValue = Mid(asCriteria, 14).Trim
        ElseIf Left(asCriteria, 7).ToLower.Trim = "sarono:" Then
            CriteriaValue = Mid(asCriteria, 8).Trim
        ElseIf Left(asCriteria, 6).ToLower.Trim = "ncano:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
        ElseIf Left(asCriteria, 6).ToLower.Trim = "boxno:" Then
            CriteriaValue = Mid(asCriteria, 7).Trim
        End If
        Return CriteriaValue
    End Function
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        hfCurrent.Value = "1"
        lMsg.Text = ""
        SetSearchTypeCookie("1")
        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) Or (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            lMsg.Text = "Please provide a valid created date."
            Exit Sub
        End If

        RetrieveDocs()
        DocSession.srchAS = "S"
    End Sub

    Private Function ufBuildCriteria() As String
        Dim lsQuery, lsVal As String
        lsQuery = ""
        Dim lColId, lDataType As Literal
        Dim rptIndex As Repeater
        Try
            rptIndex = ucDocIndex.rIndex
            For Each ri In rptIndex.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    lColId = DirectCast(ri.FindControl("lColId"), Literal)
                    lDataType = DirectCast(ri.FindControl("lDataType"), Literal)
                    If lDataType.Text = "3" Then
                        lsVal = DirectCast(ri.FindControl("dlColValue"), DropDownList).SelectedValue
                    ElseIf lDataType.Text = "4" Then
                        lsVal = DirectCast(ri.FindControl("tbDateValue"), TextBox).Text
                    ElseIf lDataType.Text = "5" Then
                        lsVal = DirectCast(ri.FindControl("dllist"), DropDownList).SelectedValue
                        If lsVal = "0" Then
                            lsVal = ""
                        End If
                    Else
                        lsVal = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                    End If
                    'lsVal = DirectCast(ri.FindControl("lColValue"), TextBox).Text
                    If lsVal <> "" Then
                        If lsQuery <> "" Then
                            lsQuery = lsQuery & " OR "
                        End If
                        lsQuery = lsQuery & "(ColumnId = " & DirectCast(ri.FindControl("lColId"), Literal).Text
                        lsQuery = lsQuery & " AND ColValue Like '%" & lsVal.Replace("'", "''") & "%') "
                        lsQuery = "(" & lsQuery & ")"
                    End If
                End If
            Next

            Return lsQuery
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lColId Is Nothing Then
                lColId.Dispose()
            End If
            If Not lDataType Is Nothing Then
                lDataType.Dispose()
            End If
            If Not rptIndex Is Nothing Then
                rptIndex.Dispose()
            End If
        End Try

    End Function
    Public Function CountRecord(ByVal as_join_tags As String, ByVal as_join_author As String, ByVal as_join_val As String, ByVal as_where As String, ByVal as_where2 As String) As Integer
        Dim objCommand As clsSqlConn

        Dim lsCrit As String = ""

        Try

            objCommand = New clsSqlConn
            Dim s_sql As String = ""


            If as_join_tags <> "" Then
                s_sql = s_sql & as_join_tags
            End If

            If as_join_author <> "" Then
                s_sql = s_sql & as_join_author
            End If

            If as_join_val <> "" Then
                s_sql = s_sql & as_join_val
            End If

            If cbPurged.Checked Then
                s_sql = s_sql & " WHERE dl.docpurgeddate is not null and dl.statusid <> 5 " & as_where & " " & as_where2
            Else
                s_sql = s_sql & " WHERE dl.statusid <> 5 " & as_where & " " & as_where2
            End If
            Dim s_sql2 As String = ""
            If DocSession.sUserRole <> "A" Then
                s_sql2 = s_sql2 & " UNION "

                s_sql2 = "SELECT dl.docid FROM doclist dl " & _
                        " INNER JOIN docrouting dro ON dro.docid = dl.docid  and dro.approverid = '" & DocSession.sUserId & "' "

                If as_join_tags <> "" Then
                    s_sql2 = s_sql2 & as_join_tags
                End If

                If as_join_author <> "" Then
                    s_sql2 = s_sql2 & as_join_author
                End If

                If as_join_val <> "" Then
                    s_sql2 = s_sql2 & as_join_val
                End If

                If cbPurged.Checked Then
                    s_sql2 = s_sql2 & " WHERE dl.docpurgeddate is not null and dl.statusid <> 5 " & as_where
                Else
                    s_sql2 = s_sql2 & " WHERE dl.statusid <> 5 " & as_where
                End If
            End If

            If s_sql2 <> "" Then
                s_sql = "SELECT count(docid) FROM (" & _
                        "SELECT dl.docid FROM doclist dl " & " " & s_sql & " UNION " & s_sql2 & _
                        ") x "

            Else
                s_sql = "SELECT count(docid) FROM doclist dl " & s_sql
            End If

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
    Public Function SearchRecord(ByVal as_join_tags As String, ByVal as_join_author As String, ByVal as_join_val As String, ByVal as_where As String, ByVal as_where2 As String) As DataTable
        Dim objCommand As clsSqlConn

        Dim lsCrit As String = ""

        Try

            Dim s_sql As String = ""


            If as_join_tags <> "" Then
                s_sql = s_sql & as_join_tags
            End If

            If as_join_author <> "" Then
                s_sql = s_sql & as_join_author
            End If

            If as_join_val <> "" Then
                s_sql = s_sql & as_join_val
            End If

            If cbPurged.Checked Then
                s_sql = s_sql & " WHERE dl.docpurgeddate is not null and dl.statusid <> 5 " & as_where & " " & as_where2
            Else
                s_sql = s_sql & " WHERE dl.statusid <> 5 " & as_where & " " & as_where2
            End If
            Dim s_sql2 As String = ""
            If DocSession.sUserRole <> "A" Then
                s_sql2 = s_sql2 & " UNION "

                s_sql2 = "SELECT dl.docid FROM doclist dl " & _
                        " INNER JOIN docrouting dro ON dro.docid = dl.docid and dro.approverid = '" & DocSession.sUserId & "' "

                If as_join_tags <> "" Then
                    s_sql2 = s_sql2 & as_join_tags
                End If

                If as_join_author <> "" Then
                    s_sql2 = s_sql2 & as_join_author
                End If

                If as_join_val <> "" Then
                    s_sql2 = s_sql2 & as_join_val
                End If

                If cbPurged.Checked Then
                    s_sql2 = s_sql2 & " WHERE dl.docpurgeddate is not null and dl.statusid <> 5 " & as_where
                Else
                    s_sql2 = s_sql2 & " WHERE dl.docid is not null and dl.statusid <> 5 " & as_where
                End If
            End If
            If s_sql2 <> "" Then
                s_sql2 = "SELECT TOP " & CStr(CInt(DocSession.RowsPerPage) + CInt(hfCurrent.Value) - 1) & _
                        " (row_number() over (order by dl.docid desc)) AS rn,dl.docid " & _
                        " FROM (" & _
                        "SELECT dl.docid FROM doclist dl " & " " & s_sql & " UNION " & s_sql2 & _
                        ") dl "

            Else
                s_sql2 = "SELECT TOP " & CStr(CInt(DocSession.RowsPerPage) + CInt(hfCurrent.Value) - 1) & _
                        " (row_number() over (order by dl.createddate desc)) AS rn,dl.docid FROM doclist dl " & s_sql
            End If



            s_sql = "SELECT rn,colvalue='',docAction = '', 5 as groupAccessId," & _
            "dlx.docId, " & _
            "dlx.doctype, " & _
            "dlx.Title, " & _
            "dlx.ModifiedBy, " & _
            "dlx.filename, " & _
            "dlx.ModifiedDate, " & _
            "dlx.IPAddress, " & _
            "dlx.statusid, " & _
            "dlx.createddate " & _
            ",(case when db.DocId is null then 0 else 1 end) AS BookMarked " & _
            ",(case when dlx.InternalDoc = 1 then 'Internal' else 'External' end) AS Classification " & _
            ",isnull(pic.FirstName,'')+' '+isnull(pic.LastName,'') as  PersonnelInCharge " & _
            ",dt.docname " & _
            "," & IIf(DocSession.OraClient, "NVL(", "ISNULL(") & "dlx.DocPurgedDate,'') as PurgedDate " & _
            ",ds.description "
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(dlx.refno,'') AS refno " & _
                                ",(NVL(u.FirstName,'') + ' ' + NVL(u.LastName,'')) AS userName, " & _
                                "(NVL(u1.FirstName,'') + ' ' + NVL(u1.LastName,'')) AS Originator "
            Else
                s_sql = s_sql & ",isnull(dlx.refno,'') AS refno " & _
                                ",(isnull(u.FirstName,'') + ' ' + isnull(u.LastName,'')) AS userName, " & _
                                "(isnull(u1.FirstName,'') + ' ' + isnull(u1.LastName,'')) AS Originator "
            End If


            s_sql = s_sql & " FROM doclist dlx INNER JOIN (" & s_sql2 & ") dtbl on dlx.docid = dtbl.docid  " & _
                "INNER JOIN doctype dt ON dlx.doctype = dt.doctype " & _
                "INNER JOIN DocStatus ds ON dlx.statusid = ds.statusid " & _
                "INNER JOIN users u ON dlx.ModifiedBy = u.userid " & _
                "INNER JOIN users u1 ON dlx.CreatedBy = u1.userid " & _
                "INNER JOIN docrouting dr ON dr.docid = dlx.docid and dr.seqno = dlx.routingseqno and (dr.carboncopy is null or dr.carboncopy = 0)  " & _
                "LEFT JOIN users pic ON pic.userId = dr.approverid " & _
                "LEFT JOIN DocBookmark db ON dlx.docId = db.docId and db.userid= '" & DocSession.sUserId & "'"
            s_sql = s_sql & " WHERE dtbl.rn between " & hfCurrent.Value & " and " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage)) & " - 1 "

            'Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            'Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql

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

    Private Sub RetrieveDocs()

        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doctypeloc")
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim lsCrit As String = ""
        Dim lsSql2 As String = ""
        Try
            '1. save criteria
            SaveCriteria()

            '2. get index criteria
            If dlDocType.SelectedValue <> "" Then
                lsCrit = ufBuildCriteria()
            End If

            Dim s_join_val As String = ""
            If lsCrit.Trim() <> "" Then
                s_join_val = " INNER JOIN (SELECT Distinct " & _
                                "div.docId " & _
                            " FROM DocIndexValues div " & _
                            " WHERE " & lsCrit.Trim() & ") divd ON dl.docid = divd.docid "
            End If


            Dim s_where As String = ""
            Dim s_join_tags As String = ""

            If tbRefNo.Text <> "" Then
                s_where = s_where & " AND dl.refno " & IIf(rbPartial.Checked, " like '%" & tbRefNo.Text & "%'", " = '" & tbRefNo.Text & "'")
            End If
            If dlDocType.SelectedValue <> "" Then
                s_where = s_where & " AND dl.doctype = '" & dlDocType.SelectedValue & "'"
            End If
            If dlStatus.SelectedValue <> "" Then
                s_where = s_where & " AND dl.statusid = " & dlStatus.SelectedValue
            End If

            If dlDocStatus.SelectedValue <> "" Then
                If dlDocStatus.SelectedValue = "O" Then
                    s_where = s_where & " AND dl.statusid NOT IN (" & DocSession.CompleteStatus & ") " '& dlStatus.SelectedValue
                ElseIf dlDocStatus.SelectedValue = "C" Then
                    s_where = s_where & " AND dl.statusid IN (" & DocSession.CompleteStatus & ") " '& dlStatus.SelectedValue
                End If
            End If

            If dlRequestType.SelectedValue <> "" AndAlso dlRequestType.SelectedValue <> "0" Then
                s_where = s_where & " AND dl.RequestType = '" & dlRequestType.SelectedValue & "'"
            End If

            If dlClassification.SelectedValue <> "" Then
                If dlClassification.SelectedValue = "1" Then
                    s_where = s_where & " AND dl.InternalDoc = 1 "
                Else
                    s_where = s_where & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                End If

            End If

            If tbSender.Text.Trim <> "" Then
                s_where = s_where & " AND dl.DocSender " & IIf(rbPartial.Checked, "like '%" & tbSender.Text.Trim & "%'", "='" & tbSender.Text.Trim & "'")
            End If

            If tbTags.Text <> "" Then
                s_join_tags = " INNER JOIN (SELECT distinct docid FROM doctags WHERE tags " & IIf(rbPartial.Checked, "like '%" & tbTags.Text.Trim & "%'", "='" & tbTags.Text.Trim & "'") & ") dtd ON dtd.docid = dl.docid "
            End If

            If tbDocTitle.Text <> "" Then
                s_where = s_where & " AND dl.title " & IIf(rbPartial.Checked, "like '%" & tbDocTitle.Text.Trim & "%'", "='" & tbDocTitle.Text & "'")
            End If

            If tbDCFrom.Text <> "" Then
                s_where = s_where & " AND convert(datetime,convert(char(10),dl.CreatedDate,101)) >= '" & tbDCFrom.Text.Trim & "'"
            End If

            If tbDCTo.Text <> "" Then
                s_where = s_where & " AND convert(datetime,convert(char(10),dl.CreatedDate,101)) <= '" & tbDCTo.Text.Trim & "'"
            End If

            If tbAuthor.Text <> "" Then
                s_join_tags = s_join_tags & " INNER JOIN users u ON u.userid = dl.createdby AND ISNULL(u.firstname,'')+' '+ISNULL(u.lastname,'') " & IIf(rbPartial.Checked, "like '%" & tbAuthor.Text.Trim & "%'", "= '" & tbAuthor.Text.Trim & "'")
            End If

            If tbArchivedBy.Text <> "" Then
                s_join_tags = s_join_tags & " INNER JOIN users ab ON ab.userid = dl.archivedby AND ISNULL(ab.firstname,'')+' '+ISNULL(ab.lastname,'') " & IIf(rbPartial.Checked, "like '%" & tbArchivedBy.Text.Trim & "%'", "= '" & tbArchivedBy.Text.Trim & "'")
            End If

            If tbPIC.Text <> "" Then

                s_join_tags = s_join_tags & " INNER JOIN docRouting dr ON " & _
                    " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0) " & _
                    " INNER JOIN users pic ON pic.userid = dr.approverid " & _
                    " AND isnull(pic.FirstName,'')+' '+isnull(pic.LastName,'') " & IIf(rbPartial.Checked, "like '%" & tbPIC.Text.Trim & "%'", "= '" & tbPIC.Text.Trim & "'")

            End If
            Dim s_join_author As String = ""
            Dim dlist As New DocList

            Dim TotalRecord As Integer = CountRecord(s_join_tags, s_join_author, s_join_val, s_where, dlist.pDocListUser)
            hfTotalRows.Value = CStr(TotalRecord)
            If TotalRecord > 0 Then

                Repeater1.Visible = True


                ldata = SearchRecord(s_join_tags, s_join_author, s_join_val, s_where, dlist.pDocListUser)


                lNo.Text = TotalRecord.ToString("#,##0")
                lNoOfRecord.Visible = True
                lNo.Visible = True

                Repeater1.DataSource = ldata
                Repeater1.DataBind()

                If ldata.Rows.Count = 0 Then
                    lMsg.Text = "No records found. Please try another search criteria."
                    'lMsg.Visible = True
                Else

                    ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                    pPager.Update()
                    lMsg.Text = ""

                End If
            Else
                Repeater1.Visible = False
                lNo.Text = "0"
                lMsg.Text = "No records found. Please try another search criteria."
            End If

            idSrchRslt.Visible = True
            pnlRepeater.Update()
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



    Private Sub dlDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocType.SelectedIndexChanged

        'Dim oDoc As New DocIndex
        ucDocIndex.pDIS = False
        ucDocIndex.RetrieveDocIndex(dlDocType.SelectedValue)
        'Dim lodata As DataTable = oDoc.RetrieveDocTypeIndex()
        'If lodata.Rows.Count > 0 Then


        '    rptIndex.Visible = True
        '    rptIndex.DataSource = lodata
        '    rptIndex.DataBind()


        'Else
        '    rptIndex.Visible = False
        'End If
        plIndex.Update()
    End Sub

    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        pnlAS.Visible = Not pnlAS.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
            'SetSearchTypeCookie("1")
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If
        pnlBk.Update()
    End Sub
    Private Sub HidePanel()
        'idAdvSrch.Visible = True
        pnlAS.Visible = False
        BindCriteria()
        imgBk.ImageUrl = "images/showpanel.png"
    End Sub
    Private Sub ShowPanel()
        'idAdvSrch.Visible = False
        pnlAS.Visible = True
        'BindCriteria()
        imgBk.ImageUrl = "images/hidepanel.png"
    End Sub
    Private Sub GetRequestType()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocList
        Try
            oType = New DocList
            If Left(DocSession.sOfcCode, 2) = "PS" Then
                oType.pOfficeCode = "PS"
            ElseIf DocSession.sUserRole <> "A" Then
                oType.pOfficeCode = "DBM"
            End If
            ldata = oType.RetrieveRequestType
            If Not ldata Is Nothing Then
                lrow = ldata.NewRow
                lrow(0) = ""
                ldata.Rows.InsertAt(lrow, 0)
                dlRequestType.DataSource = ldata
                dlRequestType.DataValueField = "RequestType"
                dlRequestType.DataTextField = "RequestDescription"
                dlRequestType.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim lval As String = DateTime.Now.Subtract(lsTime).TotalSeconds.ToString
        lNoOfRecord.Text = " records(s) found     ------------> " & lval & " seconds"
    End Sub
    Private Sub SetCurrent(ByVal asval As String)
        'Dim mycookie As HttpCookie = New HttpCookie("srchCurrent")
        'mycookie.Value = asval
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)
        DocSession.srchASCurrentPage = asval
    End Sub

    Private Function GetCurrent() As String
        If Current IsNot Nothing AndAlso Not DocSession.srchASCurrentPage Is Nothing Then
            Return DocSession.srchASCurrentPage
        Else
            Return ""
        End If
    End Function

    'ok
    Private Sub SaveCriteria()
        Dim mycookie As HttpCookie = New HttpCookie("srchTitle")
        mycookie.Value = tbDocTitle.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchRefNo")
        mycookie.Value = tbRefNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchStatus")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchDocStatus")
        mycookie.Value = dlDocStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchTypeRequest")
        mycookie.Value = dlRequestType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchDocType")
        mycookie.Value = dlDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchAuthor")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchArchivedBy")
        mycookie.Value = tbArchivedBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchTags")
        mycookie.Value = tbTags.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchCrTo")
        mycookie.Value = tbDCTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchCrFrom")
        mycookie.Value = tbDCFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchSender")
        mycookie.Value = tbSender.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchInCharge")
        mycookie.Value = tbPIC.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        mycookie = New HttpCookie("srchCurrent")
        mycookie.Value = hfCurrent.Value
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchPartialExact")
        mycookie.Value = IIf(rbPartial.Checked, "P", "E")
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("srchPurgeOnly")
        mycookie.Value = IIf(cbPurged.Checked, "1", "0")
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub
    Private Sub GetCriteria()
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchTitle") Is Nothing Then
            tbDocTitle.Text = Current.Request.Cookies("srchTitle").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchRefNo") Is Nothing Then
            tbRefNo.Text = Current.Request.Cookies("srchRefNo").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchDocType") Is Nothing Then
            dlDocType.SelectedValue = Current.Request.Cookies("srchDocType").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchStatus") Is Nothing Then
            dlStatus.SelectedValue = Current.Request.Cookies("srchStatus").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchDocStatus") Is Nothing Then
            dlDocStatus.SelectedValue = Current.Request.Cookies("srchDocStatus").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchClassification") Is Nothing Then
            dlClassification.SelectedValue = Current.Request.Cookies("srchClassification").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchTypeRequest") Is Nothing Then
            dlRequestType.SelectedValue = Current.Request.Cookies("srchTypeRequest").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchSender") Is Nothing Then
            tbSender.Text = Current.Request.Cookies("srchSender").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchAuthor") Is Nothing Then
            tbAuthor.Text = Current.Request.Cookies("srchAuthor").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchArchivedBy") Is Nothing Then
            tbArchivedBy.Text = Current.Request.Cookies("srchArchivedBy").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchTags") Is Nothing Then
            tbTags.Text = Current.Request.Cookies("srchTags").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchCrTo") Is Nothing Then
            tbDCTo.Text = Current.Request.Cookies("srchCrTo").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchCrFrom") Is Nothing Then
            tbDCFrom.Text = Current.Request.Cookies("srchCrFrom").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchInCharge") Is Nothing Then
            tbPIC.Text = Current.Request.Cookies("srchInCharge").Value
        End If
        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchPartialExact") Is Nothing Then
            If Current.Request.Cookies("srchPartialExact").Value = "P" Then
                rbPartial.Checked = True
                rbExact.Checked = False
            Else
                rbExact.Checked = True
                rbPartial.Checked = False
            End If

        End If

        If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchPurgeOnly") Is Nothing Then
            If Current.Request.Cookies("srchPurgeOnly").Value = "1" Then
                cbPurged.Checked = True
            Else
                cbPurged.Checked = False
            End If

        End If
        'If dlDocType.SelectedValue <> "" Then
        '    If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchIndexVal") Is Nothing Then
        '        ShowIndex()
        '        If Request.Cookies("srchIndexVal").Value.Trim <> "" Then
        '            ucDocIndex.SetCookie()
        '        End If

        '    End If

        'End If

    End Sub

    Private Function GetSearchParam() As String
        'Dim mycookie As HttpCookie = New HttpCookie("srchAS")
        If Not Current.Request.Cookies("srchParam") Is Nothing Then
            Return Current.Request.Cookies("srchParam").Value
        Else
            Return ""
        End If

        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)
    End Function
    Private Function GetSearchTypeCookie(ByVal asParam) As String
        'Dim mycookie As HttpCookie = New HttpCookie("srchAS")
        If Not Current.Request.Cookies("srchAS") Is Nothing Then
            If Current.Request.Cookies("srchAS").Value = "" Then
                Return asParam
            Else
                Return Current.Request.Cookies("srchAS").Value
            End If
        Else
            Return asParam
        End If

    End Function
    Private Sub SetSearchTypeCookie(ByVal asval As String)
        Dim mycookie As HttpCookie = New HttpCookie("srchAS")
        mycookie.Value = asval
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
    Private Sub SetIndexCookie(ByVal asval As String)
        Dim mycookie As HttpCookie = New HttpCookie("srchIndexVal")
        mycookie.Value = asval
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
    Private Sub ShowResult()
        Dim lIdx As Integer
        hfCurrent.Value = GetCurrent()
        If hfCurrent.Value.Trim <> "" Then
            'lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
            'hfCurrent.Value = CStr(lIdx)
            RetAction()
        End If


    End Sub

    Private Sub btClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClear.Click
        tbAuthor.Text = ""
        tbArchivedBy.Text = ""
        tbDCFrom.Text = ""
        tbDCTo.Text = ""
        tbDocTitle.Text = ""
        tbSender.Text = ""
        tbRefNo.Text = ""
        tbPIC.Text = ""
        tbTags.Text = ""
        dlDocStatus.SelectedValue = ""
        dlStatus.SelectedValue = ""
        dlRequestType.SelectedValue = ""
        dlClassification.SelectedValue = ""
        dlDocType.SelectedValue = ""
        cbPurged.Checked = False
        lMsg.Text = ""
        pnlRepeater.Update()
    End Sub
    Private Function SearchSaroNCA(ByVal asCriteria As String, ByVal asColumn As String, ByVal asIdx As String, ByVal asRowsPerPage As String) As String
        Dim s_sql As String
        s_sql = "SELECT " & _
        "t.rn,t.Colvalue,docAction = '', " & _
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
        ",ISNULL(dl.DocPurgedDate,'') as PurgedDate " & _
        ",ds.description "

        s_sql = s_sql & ",1 AS GroupAccessId " & _
                    ",(ISNULL(u.FirstName,'') + ' ' + ISNULL(u.LastName,'')) AS userName, " & _
                    "(ISNULL(u1.FirstName,'') + ' ' + ISNULL(u1.LastName,'')) AS Originator " & _
                    ",refno=isnull(dl.refno,'') " & _
                    ",ReceivedBy=isnull(dl.ReceivedBy,'') " & _
                    ",ReceivedDate=isnull(convert(char(10),dl.ReceivedDate,101),'') " & _
                    ",ReceivedTime=isnull(right(convert(varchar,dl.ReceivedDate),8),'') " & _
                    ", isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) as PersonnelInCharge "

        s_sql = s_sql & " FROM doclist dl " & _
            "INNER JOIN " & _
            " (SELECT div.ColValue,dl.docid,rn= ROW_NUMBER() over (Order By dl.createddate desc) FROM doclist dl " & _
        "INNER JOIN docindexvalues div on dl.DocId = div.DocId and div.ColumnId = div.ColumnId " & _
"INNER JOIN docindex di on div.columnid = di.columnid and dl.doctype= di.doctype " & _
"WHERE di.ColumnName = '" & asColumn & "' and div.ColValue " & IIf(DocSession.SearchOption = "E", "= '" & asCriteria.Trim & "'", "like '%" & asCriteria.Trim & "%' ") & _
"AND dl.statusid <> 5 and dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) "



        s_sql = s_sql & ") " & _
        " T " & _
            "ON dl.docid = t.docid " & _
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
            s_sql = s_sql & "WHERE (T.rn between " & asIdx & " and " & asIdx & "+" & asRowsPerPage & ") "
        End If
        s_sql = s_sql & " ORDER BY t.rn"


        Return s_sql

    End Function
    Private Function SearchAttachment(ByVal asCriteria As String, ByVal asIdx As String, ByVal asRowsPerPage As String) As String
        Dim s_sql As String
        s_sql = "SELECT " & _
        "t.rn,Colvalue = T.afname,docAction = '', " & _
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
        ",ISNULL(dl.DocPurgedDate,'') as PurgedDate " & _
        ",ds.description "


        s_sql = s_sql & ",1 AS GroupAccessId " & _
                    ",(ISNULL(u.FirstName,'') + ' ' + ISNULL(u.LastName,'')) AS userName, " & _
                    "(ISNULL(u1.FirstName,'') + ' ' + ISNULL(u1.LastName,'')) AS Originator " & _
                    ",refno=isnull(dl.refno,'') " & _
                    ",ReceivedBy=isnull(dl.ReceivedBy,'') " & _
                    ",ReceivedDate=isnull(convert(char(10),dl.ReceivedDate,101),'') " & _
                    ",ReceivedTime=isnull(right(convert(varchar,dl.ReceivedDate),8),'') " & _
                    ", isnull(u2.FirstName,u.FirstName)+' '+isnull(u2.LastName,u.LastName) as PersonnelInCharge "

        s_sql = s_sql & " FROM doclist dl " & _
            "INNER JOIN " & _
            " (select docid,min(docfilename) as afname,rn = row_number() over (partition by docid order by docid) " & _
        " from DocAttachment " & _
        " where docfilename like '%" & asCriteria.Trim.Replace("'", "''") & "%' " & _
        "group by docid ) T ON T.docid = dl.docid " & _
        "INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
        "INNER JOIN DocStatus ds ON dl.statusid = ds.statusid " & _
        "INNER JOIN users u ON dl.ModifiedBy = u.userid " & _
        "INNER JOIN users u1 ON dl.CreatedBy = u1.userid " & _
        "LEFT JOIN DocBookmark db ON dl.docId = db.docId and db.userid= '" & DocSession.sUserId & "' " & _
        " LEFT JOIN docRouting dr ON	" & _
        " dr.docid = dl.docid And dr.seqno = dl.routingseqno And (dr.CarbonCopy Is null Or dr.CarbonCopy = 0)" & _
        " LEFT JOIN users u2 " & _
        "  ON dr.approverid = u2.userid	" & _
        "WHERE dl.statusid <> 5 and dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) "

        If asIdx <> "" Then
            s_sql = s_sql & " AND (T.rn between " & asIdx & " and " & asIdx & "+" & asRowsPerPage & ") "
        End If

        s_sql = s_sql & " ORDER BY t.rn"


        Return s_sql

    End Function
    Private Function SaroNCACount(ByVal asColumn As String, ByVal asCriteria As String) As Integer

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl " & _
                                    "INNER JOIN docindexvalues div on dl.DocId = div.DocId and div.ColumnId = div.ColumnId " & _
                                    "INNER JOIN docindex di on div.columnid = di.columnid and dl.doctype= di.doctype " & _
                                    "WHERE di.ColumnName = '" & asColumn & "' and div.ColValue like '%" & asCriteria & "%' " & _
                                    "AND dl.statusid <> 5 and dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 )"

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
    Private Function AttachmentCount(ByVal asCriteria As String) As Integer

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "SELECT count(dl.docid) FROM doclist dl " & _
            "INNER JOIN " & _
            " (select docid,min(docfilename) as afname " & _
        " from DocAttachment " & _
        " where docfilename like '%" & asCriteria.Trim.Replace("'", "''") & "%' " & _
        "group by docid ) T ON t.docid = dl.docid " & _
        "WHERE dl.statusid <> 5 and dl.doctype IN (SELECT g.docId FROM GroupDocAccess G WHERE g.groupId = '" & DocSession.sUserGroup & "' and g.GroupAccessId > 0 ) "

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
End Class
