Public Class DocSearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("as") = "1" Then
                idAdvSrch.Visible = True
                idSrchRslt.Visible = False
                Dim oDoc As New DocTypes
                oDoc.pGroupId = DocSession.sUserGroup
                Dim ldata As DataTable = oDoc.GetDocType()
                dlDocType.DataTextField = "DocName"
                dlDocType.DataValueField = "DocType"
                Dim lrow As DataRow
                lrow = ldata.NewRow()

                ldata.Rows.InsertAt(lrow, 0)
                dlDocType.DataSource = ldata
                dlDocType.DataBind()
                ldata = oDoc.GetDocStatus
                lrow = ldata.NewRow()
                ldata.Rows.InsertAt(lrow, 0)
                dlStatus.DataSource = ldata

                dlStatus.DataValueField = "StatusId"
                dlStatus.DataTextField = "Description"
                dlStatus.DataBind()
                imgGreater.Visible = False
                imgLess.Visible = False
                imgGreaterD.Visible = False
                imgLessD.Visible = False
            Else
                hfTotalValue.Value = "0"
                Master.SetSearchCriteria = Request.QueryString("p")
                If Master.SetSearchCriteria = "" Then
                    lMsg.Text = "Please provide a search criteria before click the Search button."
                Else
                    RetrieveDocAction(Request.QueryString("p"))
                    imgAGreater.Visible = False
                    imgALess.Visible = False
                    imgAGreaterD.Visible = False
                    imgALessD.Visible = False
                End If
            End If

        End If

    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Private Function ufParseCriteria(ByVal asCriteria As String, ByVal scol As String, ByVal spartial As String) As String

        Dim squery, mystr, str, mystr2, strwithqoute As String
        Dim strArr As Array = Nothing
        Dim ind, ind2 As Integer


        mystr2 = ""
        strwithqoute = ""
        squery = Trim(asCriteria)

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
                    If spartial = "Y" Then
                        str = scol & " like '%" & Trim(mystr) & "%'"
                        'str = Trim(mystr)
                    Else
                        str = scol & " = '" & Trim(mystr) & "'"
                    End If


                Else
                    If spartial = "Y" Then
                        str = str & " or " & scol & " like '%" & Trim(mystr) & "%'"
                        'str = str & "," & Trim(mystr)
                    Else
                        str = str & " or " & scol & " = '" & Trim(mystr) & "'"
                    End If
                End If

            Next

        Else

            strArr = squery.Split(" ")

            For ind = 0 To strArr.Length - 1

                If str = "" Then
                    If spartial = "Y" Then
                    
                        str = scol & " like '%" & Trim(strArr(ind)) & "%'"
                        'str = Trim(strArr(ind))
                    Else
                        str = scol & " = '" & Trim(strArr(ind)) & "'"
                    End If
                Else
                    If spartial = "Y" Then
                        str = str & " or " & scol & " like '%" & Trim(strArr(ind)) & "%'"
                    Else
                        str = str & " or " & scol & " = '" & Trim(strArr(ind)) & "'"
                    End If

                End If


            Next

        End If

        Return str

    End Function



    Private Sub RetrieveDocAction(ByVal asCriteria As String)
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim rowcnt As Integer = 0
        Dim ssql As String
        Try
            objCommand = New clsSqlConn
            asCriteria = ufParseCriteria(Server.UrlDecode(asCriteria), "title", "p")
            ssql = " SELECT"
            If CInt(hfTotalValue.Value) = 0 Then
                rowcnt = RetrieveDocActionCount(asCriteria)
                hfTotalValue.Value = CStr(rowcnt)
            End If

            objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_SEARCH"
            If DocSession.SearchOption = "E" Then
                'Master.SetExactSearch = False
                'Master.SetPartialSearch = True
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED_EXACT"
            Else
                'Master.SetExactSearch = True
                'Master.SetPartialSearch = False
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED"

            End If


            objCommand.ParametersAddWithValue("@asCriteria", asCriteria)
            objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
            objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
            objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)


            ldata = objCommand.Fill

            If ldata.Rows.Count > DocSession.RowsPerPage Then
                imgGreater.Visible = True
                imgGreaterD.Visible = False
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            Else
                imgGreater.Visible = False
                imgGreaterD.Visible = True
            End If

            If CInt(hfCurrent.Value) > 1 Then
                imgLess.Visible = True
                imgLessD.Visible = False
            Else
                imgLess.Visible = False
                imgLessD.Visible = True
            End If

            Repeater1.DataSource = ldata
            Repeater1.DataBind()
            'lNo.Text = CStr(ldata.Rows.Count)
            lNo.Text = CStr(hfTotalValue.Value)
            If ldata.Rows.Count = 0 Then
                lMsg.Text = "No records found. Please try another search criteria."
                'lMsg.Visible = True
            Else
                lMsg.Text = ""
                'lMsg.Visible = False

                'If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > CInt(retval.pParam.Value) Then
                '    lPageCount.Text = "Row " & hfCurrent.Value & " -  " & retval.pParam.Value & " of " & retval.pParam.Value
                If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > CInt(hfTotalValue.Value) Then
                    lPageCount.Text = "Row " & hfCurrent.Value & " -  " & hfTotalValue.Value & " of " & hfTotalValue.Value
                Else
                    lPageCount.Text = "Row " & hfCurrent.Value & " -  " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) & " of " & hfTotalValue.Value
                End If

            End If
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


    Private Function RetrieveDocActionCount(ByVal asCriteria As String) As Integer

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_SEARCH"
            If DocSession.SearchOption = "E" Then
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED_COUNT_EXACT"
            Else
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED_COUNT"
            End If

            objCommand.ParametersAddWithValue("@asCriteria", asCriteria)
            objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
            objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
            objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
            objCommand.ParametersReturnValue()
            objCommand.ExecScalar()
            'Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            'Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            Dim retval As New DbParam
            retval.pParam = objCommand.RetValue
            Return CInt(retval.pParam.Value)

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

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgB As ImageButton = DirectCast(e.Item.FindControl("ImgBookmark"), ImageButton)
            Dim imgT As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
            Dim imgL As ImageButton = DirectCast(e.Item.FindControl("ImgLink"), ImageButton)
            Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
            Dim imgBx As ImageButton = DirectCast(e.Item.FindControl("ImgBox"), ImageButton)
            Dim imgCkBx As ImageButton = DirectCast(e.Item.FindControl("ImgCheckBox"), ImageButton)
            Dim imgU As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim btn As Button = DirectCast(e.Item.FindControl("btSaveTag"), Button)

            AddHandler imgB.Click, AddressOf BookmarkDocument
            AddHandler imgUB.Click, AddressOf UnBookmarkDocument
            AddHandler imgT.Click, AddressOf TagDocument
            AddHandler imgL.Click, AddressOf LinkDocument
            AddHandler imgBx.Click, AddressOf CheckDoc
            AddHandler imgCkBx.Click, AddressOf CheckDoc
            AddHandler btn.Click, AddressOf SaveTagDocument
            AddHandler imgU.Click, AddressOf ShowDocument
        End If

    End Sub

    Private Sub ShowDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lD As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lDT As Literal = DirectCast(rptItem.FindControl("lDocType"), Literal)
        Dim lDTAccess As Literal = DirectCast(rptItem.FindControl("lGroupAccessId"), Literal)

        DocSession.sDocID = lD.Text
        DocSession.sDocType = lDT.Text
        DocSession.sDocTypeAccess = lDTAccess.Text
        Response.Redirect("view.aspx")

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
        If imgCB.Visible Then
            AddLinks(lrDocId.Text)
        End If


        AddHandler rptL.ItemCreated, AddressOf rptLItemCreated
        rptL.DataSource = RetrieveDocLinks(CInt(lrDocId.Text))
        rptL.DataBind()
        rptL.Visible = True
        'pnl.Visible = Not pnl.Visible


        pnlRepeater.Update()

    End Sub

    Private Function RetrieveDocLinks(ByVal aiDocId As Integer) As DataTable


      
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

    Private Sub rptLItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgD As ImageButton = DirectCast(e.Item.FindControl("imgLinkDelete"), ImageButton)

            AddHandler imgD.Click, AddressOf DeleteDocLink

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
        lPnl.Visible = Not lPnl.Visible

        pnlRepeater.Update()

    End Sub

    Private Sub SaveTagDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgButton As Button = DirectCast(sender, Button)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lttags As Literal = DirectCast(rptItem.FindControl("ltags"), Literal)
        Dim ltxt As TextBox = DirectCast(rptItem.FindControl("txtTag"), TextBox)
        Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
        Dim ltitle As Label = DirectCast(rptItem.FindControl("ltitle"), Label)
        Dim oTag As New DocTags
        oTag.pDocId = CInt(lrDocId.Text)
        oTag.pIpAddress = Request.UserHostAddress
        oTag.pTag = ltxt.Text
        oTag.pUserId = DocSession.sUserId 'Session("userid")
        oTag.pDocName = ltitle.Text
        oTag.SaveDocTag()

        lttags.Text = IIf(lttags.Text.Trim = "", ltxt.Text, lttags.Text.Trim & ", " & ltxt.Text)
        lPnl.Visible = Not lPnl.Visible
        pnlRepeater.Update()

    End Sub



    Private Sub BookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lBM As Literal = DirectCast(rptItem.FindControl("lBookmarked"), Literal)
        Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgUnbookmark"), ImageButton)

        DocBookmark(CInt(lrDocId.Text), "0")
        imgButton.Visible = Not imgButton.Visible
        imgUnButton.Visible = Not imgUnButton.Visible

        pnlRepeater.Update()

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
            If liCtr > 1 Then
                ltr.pTran.Commit()
            Else

            End If

        Catch ex As Exception

            ltr.pTran.Rollback()
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
        bm.pDocId = CInt(lrDocId.Text)
        bm.pUserId = DocSession.sUserId
        bm.DocBookmark("D")
        'DocBookmark(CInt(lrDocId.Text), "1")
        imgButton.Visible = Not imgButton.Visible
        imgUnButton.Visible = Not imgUnButton.Visible

        pnlRepeater.Update()

    End Sub

    Private Sub DocBookmark(ByVal aiDocId As Integer, ByVal asBM As String)


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.StoredProcedure

            If asBM = 1 Then
                objCommand.CommandText = "DMSP_DOCBOOKMARKDELETE"
            Else
                objCommand.CommandText = "DMSP_DOCBOOKMARKADD"

            End If
            objCommand.ParametersAddWithValue("@DocId", aiDocId)
            objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)

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

            If CInt(llGroupAccessId.Text) < 2 Then
                imgT.Visible = False
                imgBx.Visible = False
            End If
            If lBookmarked.Text = "1" Then
                imgB.Visible = False
                imgUB.Visible = True
            Else
                imgB.Visible = True
                imgUB.Visible = False

            End If


            Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))
            lblDocName.ImageUrl = lext





        End If
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        hfCurrent.Value = "1"
        lMsg.Text = ""
        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) Or (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            lMsg.Text = "Please provide a valid created date."
            Exit Sub
        End If

        RetrieveDocs()
    End Sub

    Private Function ufBuildCriteria() As String
        Dim lsQuery, lsVal As String
        lsQuery = ""
        Dim lColId, lDataType As Literal
        Dim rptIndex As Repeater = ucDocIndex.rIndex
        For Each ri In rptIndex.Items
            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                lColId = DirectCast(ri.FindControl("lColId"), Literal)
                lDataType = DirectCast(ri.FindControl("lDataType"), Literal)
                If lDataType.Text = "3" Then
                    lsVal = DirectCast(ri.FindControl("dlColValue"), DropDownList).SelectedValue
                ElseIf lDataType.Text = "4" Then
                    lsVal = DirectCast(ri.FindControl("tbDateValue"), TextBox).Text
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
    End Function

    Private Sub RetrieveDocs()

        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doctypeloc")


        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim lsCrit As String = ""

        Try

            objCommand = New clsSqlConn
            If dlDocType.SelectedValue <> "" Then
                lsCrit = ufBuildCriteria()
            End If
            If lsCrit.Trim() <> "" Then
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED2_W_DI"
                objCommand.ParametersAddWithValue("@asColValue", lsCrit)
            Else
                objCommand.CommandText = "DMSP_SEARCH_OPTIMIZED2_WO_DI"
                objCommand.ParametersAddWithValue("@asColValue", lsCrit)
            End If
            objCommand.CommandType = CommandType.StoredProcedure
            If dlDocType.SelectedValue <> "" Then
                objCommand.ParametersAddWithValue("@asDocType", dlDocType.SelectedValue)
            End If
            If dlStatus.SelectedValue <> "" Then
                objCommand.ParametersAddWithValue("@aiStatus", dlStatus.SelectedValue)
            End If
            If tbTags.Text <> "" Then
                objCommand.ParametersAddWithValue("@asTags", tbTags.Text)
            End If
            If tbDocTitle.Text <> "" Then
                objCommand.ParametersAddWithValue("@asDocTitle", tbDocTitle.Text)
            End If
            If tbDCFrom.Text <> "" Then
                objCommand.ParametersAddWithValue("@asDateFrom ", tbDCFrom.Text)
            End If
            If tbDCTo.Text <> "" Then
                objCommand.ParametersAddWithValue("@asDateTo", tbDCTo.Text)
            End If

            'objCommand.ParametersAddWithValue("@asLoc", lsLoc)
            objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
            objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
            objCommand.ParametersReturnValue()
            'Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            'Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            Dim retval As New DbParam
            retval.pParam = objCommand.RetValue
            ldata = objCommand.Fill


            'If ldata.Rows.Count > 0 Then
            '    Dim lsCrit As String = ufBuildCriteria()
            '    If lsCrit <> "" Then
            '        Dim ldr As DataRow() = ldata.Select(lsCrit)
            '        If ldr.Count() > 0 Then
            '            ldata = ldata.Select(lsCrit).CopyToDataTable
            '        Else
            '            ldata.Clear()
            '        End If
            '    End If

            'End If

            If ldata.Rows.Count > DocSession.RowsPerPage Then
                imgAGreater.Visible = True
                imgAGreaterD.Visible = False
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            Else
                imgAGreater.Visible = False
                imgAGreaterD.Visible = True
            End If

            If CInt(hfCurrent.Value) > 1 Then
                imgALess.Visible = True
                imgALessD.Visible = False
            Else
                imgALess.Visible = False
                imgALessD.Visible = True
            End If

            Repeater1.DataSource = ldata
            Repeater1.DataBind()
            'lNo.Text = CStr(ldata.Rows.Count)
            lNo.Text = CStr(retval.pParam.Value)
            lNoOfRecord.Visible = True
            lNo.Visible = True
            If ldata.Rows.Count = 0 Then
                lMsg.Text = "No records found. Please try another search criteria."
                'lMsg.Visible = True
            Else
                lMsg.Text = ""
                'lMsg.Visible = False
                If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > CInt(retval.pParam.Value) Then
                    lPageCount.Text = "Row " & hfCurrent.Value & " -  " & retval.pParam.Value & " of " & retval.pParam.Value
                Else
                    lPageCount.Text = "Row " & hfCurrent.Value & " -  " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) & " of " & retval.pParam.Value
                End If
            End If

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

    Private Sub dlDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocType.SelectedIndexChanged

        'Dim oDoc As New DocIndex

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

    Protected Sub imgLess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLess.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        'pnlRepeater.Update()

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = lIdx

        RetrieveDocAction(Master.SetSearchCriteria)
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

    Protected Sub imgGreater_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGreater.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = lIdx
        RetrieveDocAction(Master.SetSearchCriteria)
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

    Protected Sub imgALess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgALess.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        'pnlRepeater.Update()

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = lIdx
        RetrieveDocs()
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

    Protected Sub imgAGreater_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAGreater.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = lIdx
        RetrieveDocs()
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

End Class
