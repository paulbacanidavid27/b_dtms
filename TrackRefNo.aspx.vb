Imports System.Web.HttpContext
Public Class TrackRefNo
    Inherits System.Web.UI.Page
    Dim lsTime As DateTime
    Dim lsLabel As String = ""

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Tracking")
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

           
                'RetrieveDocAction(Master.SetSearchCriteria)
            SearchRecord()


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

    
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        lsTime = DateTime.Now
        If Not IsPostBack Then
            If DocSession.sDocAccess > 2 Then
                ucUBtn.Visible = True
            Else
                ucUBtn.Visible = False
            End If
        End If
    End Sub
   
   
    
    'Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim imgB As ImageButton = DirectCast(e.Item.FindControl("ImgBookmark"), ImageButton)
    '        Dim imgT As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
    '        Dim imgL As ImageButton = DirectCast(e.Item.FindControl("ImgLink"), ImageButton)
    '        Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
    '        Dim imgBx As ImageButton = DirectCast(e.Item.FindControl("ImgBox"), ImageButton)
    '        Dim imgCkBx As ImageButton = DirectCast(e.Item.FindControl("ImgCheckBox"), ImageButton)
    '        Dim imgU As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
    '        'Dim btn As Button = DirectCast(e.Item.FindControl("btSaveTag"), Button)
    '        'Dim btnSaveRecv As Button = DirectCast(e.Item.FindControl("btReceivedBy"), Button)


    '        AddHandler imgB.Click, AddressOf BookmarkDocument
    '        AddHandler imgUB.Click, AddressOf UnBookmarkDocument
    '        AddHandler imgT.Click, AddressOf TagDocument
    '        AddHandler imgL.Click, AddressOf LinkDocument
    '        AddHandler imgBx.Click, AddressOf CheckDoc
    '        AddHandler imgCkBx.Click, AddressOf CheckDoc
    '        'AddHandler btn.Click, AddressOf SaveTagDocument
    '        'AddHandler btnSaveRecv.Click, AddressOf SaveReceiving
    '        AddHandler imgU.Click, AddressOf ShowDocument
    '    End If

    'End Sub

    

    'Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim imgB As ImageButton = DirectCast(e.Item.FindControl("ImgBookmark"), ImageButton)
    '        Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
    '        Dim lBookmarked As Literal = DirectCast(e.Item.FindControl("lBookmarked"), Literal)
    '        Dim llGroupAccessId As Literal = DirectCast(e.Item.FindControl("lGroupAccessId"), Literal)
    '        Dim imgT As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
    '        Dim imgBx As ImageButton = DirectCast(e.Item.FindControl("ImgBox"), ImageButton)
    '        'Dim imgL As ImageButton = DirectCast(e.Item.FindControl("ImgTag"), ImageButton)
    '        'Dim pNl As Panel = DirectCast(e.Item.FindControl("pReceiving"), Panel)
    '        'If DocSession.sUserId = "masantos@dbm.gov.ph" OrElse DocSession.sUserId = "admin" Then
    '        'pNl.Visible = True
    '        'End If

    '        Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
    '        Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
    '        Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))
    '        If lsLabel <> "" Then
    '            DirectCast(e.Item.FindControl("valueLabel"), Label).Text = lsLabel
    '            DirectCast(e.Item.FindControl("pBox"), Panel).Visible = True
    '        End If
    '        Try
    '            lblDocName.ImageUrl = lext
    '            If CInt(llGroupAccessId.Text) < 2 Then
    '                'imgT.Visible = False
    '                imgBx.Visible = False
    '            End If
    '            If lBookmarked.Text = "1" Then
    '                imgB.Visible = False
    '                imgUB.Visible = True
    '            Else
    '                imgB.Visible = True
    '                imgUB.Visible = False

    '            End If
    '            If DirectCast(e.Item.FindControl("lPurgedDate"), Literal).Text.Trim <> "" Then
    '                lblDocName.Enabled = False
    '                DirectCast(e.Item.FindControl("lbBM"), LinkButton).Enabled = False
    '                DirectCast(e.Item.FindControl("lTitle"), Literal).Text = "(Purged) " & HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
    '            Else
    '                DirectCast(e.Item.FindControl("lTitle"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
    '            End If
    '            DirectCast(e.Item.FindControl("lrfno"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lrfno"), Literal).Text, DocSession.SearchCriteria)


    '        Catch ex As Exception
    '        Finally
    '            If Not imgB Is Nothing Then
    '                imgB.Dispose()
    '            End If
    '            If Not imgUB Is Nothing Then
    '                imgUB.Dispose()
    '            End If
    '            If Not lBookmarked Is Nothing Then
    '                lBookmarked.Dispose()
    '            End If
    '            If Not llGroupAccessId Is Nothing Then
    '                llGroupAccessId.Dispose()
    '            End If
    '            If Not imgT Is Nothing Then
    '                imgT.Dispose()
    '            End If
    '            If Not imgBx Is Nothing Then
    '                imgBx.Dispose()
    '            End If
    '            If Not lblActive Is Nothing Then
    '                lblActive.Dispose()
    '            End If
    '            If Not lblDocName Is Nothing Then
    '                lblDocName.Dispose()
    '            End If
    '        End Try
    '    End If
    'End Sub
    'Private Function HighlightText(ByVal asSrc As String, ByVal asSrcVal As String) As String
    '    If pnlAS.Visible Then
    '        Return asSrc
    '    Else

    '        Dim squery, mystr, str, mystr2, strwithqoute As String
    '        Dim strArr As Array = Nothing
    '        Dim ind, ind2 As Integer


    '        mystr2 = ""
    '        strwithqoute = ""
    '        squery = Trim(asSrcVal)
    '        squery = RemoveSearchKey(squery)
    '        squery = Replace(squery, "'", "''")

    '        If squery.Contains(Chr(34)) Then
    '            squery = Replace(squery, """", "")
    '            asSrc = Replace(asSrc, squery.ToUpper(), "<span style='background-color:yellow'>" & squery.ToUpper & "</span>")
    '        Else
    '            If squery.Contains(" ") Then
    '                strArr = squery.Split(" ")

    '                For ind = 0 To strArr.Length - 1
    '                    Replace(asSrc, Trim(strArr(ind)), "<span style='background-color:yellow'>" & Trim(strArr(ind)) & "</span>")
    '                Next
    '            Else
    '                asSrc = Replace(asSrc, Replace(squery.ToUpper(), """", ""), "<span style='background-color:yellow'>" & squery.ToUpper & "</span>")
    '            End If

    '        End If



    '        Return asSrc
    '    End If

    'End Function
    
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        hfCurrent.Value = "1"
        lMsg.Text = ""
        If tbRefNo.Text.Trim <> "" Then
            SearchRecord()
        Else
            Master.ShowMessage("Please enter reference no before clicking on Search button.")
        End If


        
    End Sub

    
   
    Public Function SearchRecord() As DataTable
        Dim ldata As DataTable
        Dim odt As DocList
        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCTYPEGET"
            odt = New DocList

            odt.pRowsPerPage = DocSession.RowsPerPage
            odt.pIdx = hfCurrent.Value
            odt.pSortOrder = hfSortOrder.Value
            odt.pSortCol = hfSortCol.Value
            odt.pRefNo = IIf(rbPartial.Checked, " like '%" & tbRefNo.Text & "%'", " = '" & tbRefNo.Text & "'")
            ldata = odt.RetrieveRefno

            'Repeater1.DataSource = ldata
            'Repeater1.DataBind()
            'lRecordCount.Text = CStr(ldata.Rows.Count)


            'ldata = odoc.RetrieveUser
            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = odt.CountRefno

                hfTotalRows.Value = lstotalrows 'oDocs.pRetVal
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()

                lNo.Text = CInt(lstotalrows).ToString("#,##0")
                

                Repeater1.DataSource = ldata
                Repeater1.DataBind()
            Else
                lNo.Text = "0"
                hfTotalRows.Value = "0"
                Master.ShowMessage("No records found for the selected filter.")
                Repeater1.DataSource = ldata
                Repeater1.DataBind()
                pPager.Update()
            End If
            lNoOfRecord.Visible = True
            lNo.Visible = True
            pnlRepeater.Update()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            'objCommand.Dispose()
            'objCommand = Nothing
            'End If
        End Try

    End Function

    'Private Sub RetrieveDocs()

    '    Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doctypeloc")
    '    Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim lsCrit As String = ""
    '    Dim lsSql2 As String = ""
    '    Try
    '        '1. save criteria
    '        SaveCriteria()

    '        Dim s_join_val As String = ""
    '        If lsCrit.Trim() <> "" Then
    '            s_join_val = " SELECT Distinct " & _
    '                            "div.docId " & _
    '                        " FROM DocIndexValues div " & _
    '                        " WHERE " & lsCrit.Trim() & ") divd ON dl.docid = divd.docid "
    '        End If
    '        s_sql2 = "SELECT dl.docid,dl.title,dl.refno, FROM doclist dl " & _
    '                    " INNER JOIN docrouting dro ON dro.docid = dl.docid and dro.approverid = '" & DocSession.sUserId & "' "4

    '        Dim s_where As String = ""
    '        Dim s_join_tags As String = ""

    '        If tbRefNo.Text <> "" Then
    '            s_where = s_where & " AND dl.refno " & IIf(rbPartial.Checked, " like '%" & tbRefNo.Text & "%'", " = '" & tbRefNo.Text & "'")
    '        End If

    '        Dim s_join_author As String = ""
    '        Dim dlist As New DocList

    '        Dim TotalRecord As Integer = CountRecord(s_join_tags, s_join_author, s_join_val, s_where, dlist.pDocListUser)
    '        hfTotalRows.Value = CStr(TotalRecord)
    '        If TotalRecord > 0 Then

    '            Repeater1.Visible = True


    '            ldata = SearchRecord(s_join_tags, s_join_author, s_join_val, s_where, dlist.pDocListUser)


    '            lNo.Text = TotalRecord.ToString("#,##0")
    '            lNoOfRecord.Visible = True
    '            lNo.Visible = True

    '            Repeater1.DataSource = ldata
    '            Repeater1.DataBind()

    '            If ldata.Rows.Count = 0 Then
    '                lMsg.Text = "No records found. Please try another search criteria."
    '                'lMsg.Visible = True
    '            Else

    '                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
    '                pPager.Update()
    '                lMsg.Text = ""

    '            End If
    '        Else
    '            Repeater1.Visible = False
    '            lNo.Text = "0"
    '            lMsg.Text = "No records found. Please try another search criteria."
    '        End If

    '        idSrchRslt.Visible = True
    '        pnlRepeater.Update()
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
        'BindCriteria()
        imgBk.ImageUrl = "images/showpanel.png"
    End Sub
    Private Sub ShowPanel()
        'idAdvSrch.Visible = False
        pnlAS.Visible = True
        'BindCriteria()
        imgBk.ImageUrl = "images/hidepanel.png"
    End Sub
    

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim lval As String = DateTime.Now.Subtract(lsTime).TotalSeconds.ToString
        lNoOfRecord.Text = " records(s) found     ------------> " & lval & " seconds"
    End Sub
    Private Sub SetCurrent(ByVal asval As String)
        
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
    'Private Sub SaveCriteria()
    '    Dim mycookie As HttpCookie = New HttpCookie("srchRefNo")
    '    mycookie.Value = tbRefNo.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    'End Sub
    'Private Sub GetCriteria()

    '    If Current IsNot Nothing AndAlso Not Current.Request.Cookies("srchRefNo") Is Nothing Then
    '        tbRefNo.Text = Current.Request.Cookies("srchRefNo").Value
    '    End If


    'End Sub

    'Private Function GetSearchParam() As String
    '    'Dim mycookie As HttpCookie = New HttpCookie("srchAS")
    '    If Not Current.Request.Cookies("srchParam") Is Nothing Then
    '        Return Current.Request.Cookies("srchParam").Value
    '    Else
    '        Return ""
    '    End If

    '    'mycookie.Expires = DateTime.Now.AddDays(30)
    '    'Response.Cookies.Add(mycookie)
    'End Function
    'Private Function GetSearchTypeCookie(ByVal asParam) As String
    '    'Dim mycookie As HttpCookie = New HttpCookie("srchAS")
    '    If Not Current.Request.Cookies("srchAS") Is Nothing Then
    '        If Current.Request.Cookies("srchAS").Value = "" Then
    '            Return asParam
    '        Else
    '            Return Current.Request.Cookies("srchAS").Value
    '        End If
    '    Else
    '        Return asParam
    '    End If

    'End Function
    'Private Sub SetSearchTypeCookie(ByVal asval As String)
    '    Dim mycookie As HttpCookie = New HttpCookie("srchAS")
    '    mycookie.Value = asval
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)
    'End Sub
    'Private Sub SetIndexCookie(ByVal asval As String)
    '    Dim mycookie As HttpCookie = New HttpCookie("srchIndexVal")
    '    mycookie.Value = asval
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)
    'End Sub
    'Private Sub ShowResult()
    '    Dim lIdx As Integer
    '    hfCurrent.Value = GetCurrent()
    '    If hfCurrent.Value.Trim <> "" Then
    '        'lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
    '        'hfCurrent.Value = CStr(lIdx)
    '        RetAction()
    '    End If


    'End Sub

    
    
    Public Sub RetRoutingHistory(ByVal asID As String)
        pnlRoutingHistory.Visible = Not pnlRoutingHistory.Visible
        uTrackStatus.pDocID = asID
        DocSession.sDocID = asID
        uTrackStatus.RetrieveStatus()
        Master.ShowImageDocument = True
    End Sub

    Public Sub ViewTracking(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        Try

            RetRoutingHistory(DirectCast(ri.FindControl("lDocId"), Literal).Text)
        Catch ex As Exception
            Master.ShowMessage("Error occurred in viewing tracking record (" & ex.Message & "). Please try again.")
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If

        End Try

    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            

            Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))
            'If lsLabel <> "" Then
            '    DirectCast(e.Item.FindControl("valueLabel"), Label).Text = lsLabel
            '    DirectCast(e.Item.FindControl("pBox"), Panel).Visible = True
            'End If
            Try
                lblDocName.ImageUrl = lext
                'If CInt(llGroupAccessId.Text) < 2 Then
                '    'imgT.Visible = False
                '    imgBx.Visible = False
                'End If
                'If lBookmarked.Text = "1" Then
                '    imgB.Visible = False
                '    imgUB.Visible = True
                'Else
                '    imgB.Visible = True
                '    imgUB.Visible = False

                'End If
                'If DirectCast(e.Item.FindControl("lPurgedDate"), Literal).Text.Trim <> "" Then
                '    lblDocName.Enabled = False
                '    DirectCast(e.Item.FindControl("lbBM"), LinkButton).Enabled = False
                '    DirectCast(e.Item.FindControl("lTitle"), Literal).Text = "(Purged) " & HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
                'Else
                '    DirectCast(e.Item.FindControl("lTitle"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lTitle"), Literal).Text, DocSession.SearchCriteria)
                'End If
                'DirectCast(e.Item.FindControl("lrfno"), Literal).Text = HighlightText(DirectCast(e.Item.FindControl("lrfno"), Literal).Text, DocSession.SearchCriteria)


            Catch ex As Exception
            Finally
                
                If Not lblActive Is Nothing Then
                    lblActive.Dispose()
                End If
                If Not lblDocName Is Nothing Then
                    lblDocName.Dispose()
                End If
            End Try
        End If
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pnlRoutingHistory.Visible = Not pnlRoutingHistory.Visible
        Master.ShowImageDocument = False
    End Sub
End Class
