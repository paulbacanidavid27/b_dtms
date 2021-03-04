Imports System
Imports System.Data.SqlClient
Public Class sent
    Inherits System.Web.UI.Page
#Region "Page Events"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        

        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        'AddHandler ucFolder.e_ShowMessage, AddressOf ShowFolderMessage

        '01/17/2014
        'AddHandler ucFolder.e_LinkButton, AddressOf SearchDocs

        Master.SelectTab("Documents")
        'ge ucDocRouting.ShowSearch()
        If DocSession.sUserRole = "B" Then
            rbSelection.Items.RemoveAt(0)
        End If
    End Sub
    'Private Sub ShowFolderMessage()
    '    Master.ShowMessage(ucFolder.Message)
    'End Sub
    Private Sub PopulateShow()
        Dim lodl As DocList

        Try
            lodl = New DocList

            dlShow.DataSource = lodl.DataShow
            dlShow.DataTextField = "recdesc"
            dlShow.DataValueField = "recid"
            dlShow.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodl Is Nothing Then

                lodl = Nothing
            End If

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            GetDocStatus()
            If Request.QueryString("c") = "1" Then
                hfCurrent.Value = "1"
                ResetCookies()
            Else
                LoadCookies()
            End If

            PopulateShow()
            GetDocType()


            RetrieveDocs()

            DocSession.DocumentPage = "sent.aspx"
        End If
    End Sub
    
#End Region

#Region "Populator Methods"



    Private Sub GetDocType()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow

        Dim oType As DocTypes
        Try
            oType = New DocTypes
            oType.pGroupId = DocSession.sUserGroup
            ldata = oType.GetDocType

            lrow = ldata.NewRow

            If ldata.Rows.Count = 0 Then
                'imgAddDoc.ToolTip = "You don't have permission to create a new document."
                Master.ShowMessage("You don't have permission to create a new document.")
                'imgAddDoc.Enabled = False
                ucAddDoc.Visible = False
            Else

            End If

            ldata.Rows.InsertAt(lrow, 0)
            dlFilterDocType.DataSource = ldata
            dlFilterDocType.DataValueField = "DocType"
            dlFilterDocType.DataTextField = "DocName"
            dlFilterDocType.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub GetDocStatus()

        Dim oDoc As New DocTypes
        Using ldata As DataTable = oDoc.GetDocStatus
            Dim lrow As DataRow
            If ldata.Rows.Count > 0 Then
                lrow = ldata.NewRow
                lrow("statusid") = "0"
                lrow("description") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
            End If

            dlStatus.DataTextField = "description"
            dlStatus.DataValueField = "statusid"
            dlStatus.DataSource = ldata

            dlStatus.DataBind()
        End Using

    End Sub

    
    Private Sub RetrieveDocs()

        Dim ldata As DataTable
        Master.HideMessage()
        Try

            Dim oDocs As New DocList
            Dim lsTotalRows As String

            'oDocs.pDocType = dlFilterDocType.SelectedValue
            'oDocs.pDocTitle = tbFilterTitle.Text
            ''oDocs.pDocStatus = dlFilterDocStatus.SelectedValue
            'oDocs.pClassificationCode = dlClassification.SelectedValue
            'oDocs.pAuthor = txAuthor.Text.Trim
            'oDocs.pCreatedDateFrom = txDateCreatedFrom.Text
            'oDocs.pCreatedDateTo = txDateCreatedTo.Text
            'oDocs.pGroupId = DocSession.sUserGroup
            'oDocs.pIdx = hfCurrent.Value 'DocSession.doc_DocCurrentPage
            'oDocs.pRowsPerPage = CInt(DocSession.RowsPerPage)
            'oDocs.pUserId = DocSession.sUserId
            'oDocs.pSortOrder = DocSession.doc_SortOrder
            'oDocs.pSortCol = DocSession.doc_SortCol
            'oDocs.pConfidential = dlShow.SelectedValue
            'ucFolder.Visible = True

            oDocs.pRefNo = tbFilterRefNo.Text
            oDocs.pDocType = dlFilterDocType.SelectedValue
            oDocs.pDocTitle = tbFilterTitle.Text
            oDocs.pRoutedTo = tbRoutedTo.Text
            oDocs.pDocStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
            oDocs.pClassificationCode = dlClassification.SelectedValue
            oDocs.pAuthor = txAuthor.Text.Trim
            oDocs.pCreatedDateFrom = txDateCreatedFrom.Text
            oDocs.pCreatedDateTo = txDateCreatedTo.Text
            oDocs.pGroupId = DocSession.sUserGroup
            oDocs.pIdx = hfCurrent.Value 'DocSession.doc_DocCurrentPage
            oDocs.pRowsPerPage = CInt(DocSession.RowsPerPage)
            oDocs.pUserId = DocSession.sUserId
            oDocs.pSortOrder = hfSortOrder.Value 'DocSession.doc_SortOrder
            oDocs.pSortCol = hfSortCol.Value 'DocSession.doc_SortCol
            oDocs.pConfidential = dlShow.SelectedValue
            oDocs.pOfficeCode = dlOffice.SelectedValue
            lsTotalRows = CStr(oDocs.SentDocCount)
            If CInt(lsTotalRows) > 0 Then
                If DocSession.OraClient Then
                    ldata = oDocs.RetrieveDocsOra("S")
                Else
                    ldata = oDocs.SentDocRetrieve
                End If

                'If DocSession.sFolderDesc.Trim <> "" Then
                'lType.Text = "Sent - " & DocSession.sFolderDesc
                'Else
                'lType.Text = "Sent"
                'End If

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                hfTotalRows.Value = lsTotalRows
                ucPager.Visible = True
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()
                Repeater1.Visible = True
                Repeater1.DataSource = ldata
                Repeater1.DataBind()
            Else
                Repeater1.Visible = False
                Master.ShowMessage("No records found.")
                ucPager.Visible = False
                pPager.Update()
            End If

            pnlRepeater.Update()
            'upTHeader.Update()
            'pnlUFolder.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub


#End Region


#Region "Repeater Events"
    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        'Dim imgExp As ImageButton
        Dim imgU As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'imgExp = DirectCast(e.Item.FindControl("imgExpand"), ImageButton)
            imgU = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            AddHandler DirectCast(e.Item.FindControl("lbtnDoc"), LinkButton).Click, AddressOf UpdateDocType2

            'AddHandler imgExp.Click, AddressOf ShowIndex
            AddHandler imgU.Click, AddressOf UpdateDocType


            'If lType.Text.Trim = "" OrElse lType.Text.Trim = "New" Then
            '    DirectCast(e.Item.FindControl("cbArchive"), UserControlCheckBox).Visible = False
            'Else
            '    DirectCast(e.Item.FindControl("cbArchive"), UserControlCheckBox).Visible = True
            'End If

        End If
        'If e.Item.ItemType = ListItemType.Header Then

        '    Dim imgDel As ImageButton = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
        '    Dim imgMove As ImageButton = DirectCast(e.Item.FindControl("imgMove"), ImageButton)

        '    If lType.Text.Trim = "" OrElse lType.Text.Trim = "New" Then
        '        imgDel.Visible = False
        '    Else
        '        AddHandler imgDel.Click, AddressOf DeleteDocList
        '    End If
        '    'If Left(lType.Text.Trim.ToLower, 5) = "inbox" Then
        '    '    'imgMove.Visible = True
        '    '    'AddHandler imgMove.Click, AddressOf RetrieveUserFolders
        '    'Else
        '    '    imgMove.Visible = False
        '    '    PopupMenu.Visible = False
        '    'End If



        'End If

    End Sub
    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        'Dim lblDocName As Literal
        '', lblActive As Literal

        'Dim tDocName As TextBox
        ''Dim cActive As CheckBox


        'If cbUpdate.Checked Then
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))

            lblDocName.ImageUrl = lext

            If DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "True" Then
                DirectCast(e.Item.FindControl("imgUrgent"), Image).Visible = True
            End If
            If DirectCast(e.Item.FindControl("lFlag"), Literal).Text <> "-1" Then
                DirectCast(e.Item.FindControl("imgFlag"), Image).Visible = True
            End If
            If DirectCast(e.Item.FindControl("lOffice"), Literal).Text <> "" Then
                DirectCast(e.Item.FindControl("ModifiedBy"), Literal).Text = DirectCast(e.Item.FindControl("ModifiedBy"), Literal).Text & "(" & DirectCast(e.Item.FindControl("lOffice"), Literal).Text & ")"
            End If
        End If
        'End If
    End Sub

    'Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        If DirectCast(e.Item.FindControl("lComment"), Literal).Text = "OK to delete." Then
    '            DirectCast(e.Item.FindControl("rw"), HtmlControls.HtmlTableRow).Attributes("class") = "greenHigh"
    '        End If
    '    End If
    'End Sub
#End Region

#Region "Bookmark Methods"


    'Private Sub DocBookmark(ByVal aiDocId As Integer, ByVal asBM As String)
    '    'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    'Dim oConn As New SqlClient.SqlConnection(str)

    '    Dim objCommand As clsSqlConn

    '    Try
    '        objCommand = New clsSqlConn
    '        objCommand.pCommandType = CommandType.StoredProcedure

    '        If asBM = 1 Then
    '            objCommand.pCommandText = "xMSP_DOCBOOKMARKDELETE"
    '        Else
    '            objCommand.pCommandText = "xMSP_DOCBOOKMARKADD"

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

    'Private Sub UnBookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
    '    Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
    '    Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
    '    Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgBookmark"), ImageButton)
    '    Dim bm As DocBookmark = New DocBookmark
    '    bm.pDocId = CInt(lrDocId.Text)
    '    bm.pUserId = DocSession.sUserId
    '    bm.DocBookmark("D")
    '    'DocBookmark(CInt(lrDocId.Text), "1")
    '    imgButton.Visible = Not imgButton.Visible
    '    imgUnButton.Visible = Not imgUnButton.Visible

    '    pnlRepeater.Update()

    'End Sub
    'Private Sub BookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
    '    Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
    '    Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
    '    Dim lBM As Literal = DirectCast(rptItem.FindControl("lBookmarked"), Literal)
    '    Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgUnbookmark"), ImageButton)
    '    Dim bm As DocBookmark = New DocBookmark
    '    bm.pDocId = CInt(lrDocId.Text)
    '    bm.pUserId = DocSession.sUserId
    '    bm.DocBookmark("A")
    '    'DocBookmark(CInt(lrDocId.Text), "0")
    '    imgButton.Visible = Not imgButton.Visible
    '    imgUnButton.Visible = Not imgUnButton.Visible

    '    pnlRepeater.Update()

    'End Sub

    'Private Sub ReplyDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
    '    Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
    '    Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
    '    repDoc.pDocId = lrDocId.Text
    '    repDoc.Visible = True
    '    upReplyDoc.Update()
    '    'pnlRepeater.Update()

    'End Sub
#End Region

#Region "Document Index Methods"

#End Region

#Region "Edit Methods"


    'Private Sub EnableEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
    '    Dim btnEdit As ImageButton
    '    If e.Item.ItemType = ListItemType.Header Then
    '        btnEdit = DirectCast(e.Item.FindControl("btEdit"), ImageButton)
    '        AddHandler btnEdit.Click, AddressOf UpdateDocType


    '    End If

    'End Sub

    Public Function WithSelected() As Boolean
        Dim lbret As Boolean = False
        For Each rItems As RepeaterItem In Repeater1.Items
            If DirectCast(rItems.FindControl("cbArchive"), UserControlCheckBox).BoxCheck Then
                lbret = True
                Exit For
            End If
        Next
        Return lbret
    End Function

    Private Sub UpdateDocType(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ri As RepeaterItem
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        Dim lDocTypeAccess As Literal = DirectCast(ri.FindControl("lDocTypeAccess"), Literal)
        Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)

        DocSession.sDocID = lDocId.Text
        DocSession.sDocType = lDocType.Text
        DocSession.sDocTypeAccess = lDocTypeAccess.Text
        Response.Redirect("view.aspx")

    End Sub

    Private Sub UpdateDocType2(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ri As RepeaterItem
        ri = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        'Dim lDocTypeAccess As Literal = DirectCast(ri.FindControl("lDocTypeAccess"), Literal)
        Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)

        DocSession.sDocID = lDocId.Text
        DocSession.sDocType = lDocType.Text
        'DocSession.sDocTypeAccess = lDocTypeAccess.Text
        Response.Redirect("view.aspx")

    End Sub

#End Region

#Region "Delete Documents Properties"
    'Protected Sub imgClose2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose2.Click
    '    Master.ShowImageDocument = False
    '    ShowResult2()
    'End Sub
    'Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
    '    Master.ShowImageDocument = False
    '    ShowResult2()
    'End Sub
    'Private Sub ShowDelete()

    '    pDeleteDoc.Visible = True
    '    pnlDeleteDoc.Update()
    '    Master.ShowImageDocument = True
    '    'removeScrollBar()
    'End Sub
    Private Sub DeleteDocList()
        If WithSelected() Then
            ucCon.Visible = True
        Else
            Master.ShowMessage("Please select a record before clicking Delete button.")
        End If
        pnlRepeater.Update()


    End Sub
    '01/17/2014
    'Private Sub RetrieveUserFolders()
    '    Dim ldata As DataTable
    '    Try


    '        Dim oList As New DocList
    '        oList.pUserId = DocSession.sUserId
    '        ldata = oList.RetrieveUserFolder()
    '        Dim lrow As DataRow
    '        lrow = ldata.NewRow
    '        lrow(0) = ""
    '        lrow(1) = "Inbox"
    '        lrow(2) = "0"
    '        ldata.Rows.InsertAt(lrow, 0)
    '        rptFolder.DataSource = ldata
    '        rptFolder.DataBind()
    '        updFolderMenu.Update()
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while retrieve document folders (" & ex.Message & ").")
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If
    '    End Try
    'End Sub
    'Public Sub MoveDoc(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim imgButton As LinkButton = DirectCast(sender, LinkButton)
    '    Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
    '    Dim lsFolderId As Literal = DirectCast(rptItem.FindControl("lFolderId"), Literal)
    '    Dim lsFolderDesc As Literal = DirectCast(rptItem.FindControl("lFolder"), Literal)

    '    If WithSelected() Then
    '        ucCon2.Visible = True
    '        ucCon2.pDesc = lsFolderDesc.Text
    '        ucCon2.pID = lsFolderId.Text
    '        pnlCon2.Update()
    '    Else
    '        Master.ShowMessage("Please select a record before clicking Move button.")
    '    End If
    '    pnlRepeater.Update()
    'End Sub

    Private Sub ShowResult2()

        Master.ShowImageDocument = False
        pnlPage.Update()

    End Sub


#End Region

#Region "Pager Section"
    Public Sub RetAction()
        DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveDocs()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub

    'pager: step 3
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image
        hfCurrent.Value = "1"
        DocSession.doc_DocCurrentPage = hfCurrent.Value
        If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort1
            imgSort1.Visible = True
        Else
            imgSort1.Visible = False
        End If
        If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort2
            imgSort2.Visible = True
        Else
            imgSort2.Visible = False

        End If
        If imgSort3.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort3
            imgSort3.Visible = True
        Else
            imgSort3.Visible = False

        End If
        If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort4
            imgSort4.Visible = True
        Else
            imgSort4.Visible = False

        End If
        If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort5
            imgSort5.Visible = True
        Else
            imgSort5.Visible = False

        End If

        If imgSort6.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort6
            imgSort6.Visible = True
        Else
            imgSort6.Visible = False
        End If

        If imgSort7.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort7
            imgSort7.Visible = True
        Else
            imgSort7.Visible = False
        End If
        If imgSort8.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort8
            imgSort8.Visible = True
        Else
            imgSort8.Visible = False
        End If
        If imgSort9.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort9
            imgSort9.Visible = True
        Else
            imgSort9.Visible = False
        End If

        'Dim oDocList As New DocList

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                'oDocList.pSortOrder = "Desc"
                DocSession.doc_SortOrder = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                'oDocList.pSortOrder = "Asc"
                DocSession.doc_SortOrder = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            'oDocList.pSortOrder = "Asc"

            DocSession.doc_SortOrder = "Asc"
            img.Visible = True
        End If
        DocSession.doc_SortCol = lbSort.Text
        'oDocList.pDocId = ""
        'oDocList.pDocType = dlFilterDocType.SelectedValue
        'oDocList.pDocTitle = tbFilterTitle.Text
        'oDocList.pDocStatus = dlFilterDocStatus.SelectedValue
        'oDocList.pGroupId = DocSession.sUserGroup
        'oDocList.pIdx = hfCurrent.Value
        'oDocList.pRowsPerPage = DocSession.RowsPerPage
        'oDocList.pSortCol = lbSort.Text
        'oDocList.pUserId = DocSession.sUserId
        'Dim ldata As DataTable = oDocList.RetrieveDocs

        'If ldata.Rows.Count > DocSession.RowsPerPage Then
        '    'imgGreater.Visible = True
        '    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
        'Else
        '    'imgGreater.Visible = False
        'End If

        'lMsg.Visible = False
        ''hfTotalRows.Value = CStr(retval.pParam.Value)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))

        'Repeater1.DataSource = ldata

        'Repeater1.DataBind()
        'pnlRepeater.Update()

        RetAction()

    End Sub
#End Region

#Region "Upload Functionality"
    Private Sub AddDoc()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub

#End Region

#Region "Filter Records"
    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        pFilter.Visible = Not pFilter.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If
        pnlFilter.Update()
    End Sub
    '01/17/2014
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        SearchDocs()
    End Sub
    '01/17/2014
    Private Sub SearchDocs()
        hfCurrent.Value = "1"
        SetCookies()
        RetrieveDocs()
        ShowResult()
        'ucFolder.RetrieveUserFolders()
        'RetrieveUserFolders()
    End Sub

    Private Sub ShowResult()
        
        Master.ShowImageDocument = False
        pnlPage.Update()
    End Sub


#End Region



    Private Sub ucCon_e_ok_click() Handles ucCon.e_ok_click

        Dim cbox As UserControlCheckBox

        Dim odoc As DocList
        Dim objcommand As clsSqlConn
        Dim ltr As DbTran
        Dim lsMsg As String = ""
        'Dim ri As RepeaterItem
        Try
            ltr = New DbTran
            odoc = New DocList

            objcommand = New clsSqlConn(ltr.pTran)
            Dim lsDate As String = DateTime.Now.ToString
            For Each ri As RepeaterItem In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbArchive"), UserControlCheckBox)
                    If cbox.BoxCheck Then
                        odoc.pDocId = DirectCast(ri.FindControl("lDocId"), Literal).Text
                        'odoc.pModifiedDate = lsDate
                        odoc.pUserId = DocSession.sUserId
                        'odoc.ArchiveDoc(objcommand)
                        'If lType.Text.Trim = "Sent" Then
                        '    odoc.DeleteFromOutbox(objcommand)
                        '    lsMsg = "Document has been deleted from your Sent box."
                        'Else
                        '    odoc.DeleteFromInbox(objcommand)
                        '    lsMsg = "Document has been deleted from your Inbox."

                        'End If

                    End If
                End If

            Next
            ltr.pTran.Commit()
            Master.ShowMessage(lsMsg)
            'Repeater1.Visible = False
            'ucPager.Visible = False
            'pPager.Update()
            hfCurrent.Value = "1"
            RetrieveDocs()

            pnlRepeater.Update()
        Catch ex As Exception
            Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally
            If Not objcommand Is Nothing Then
                objcommand.Dispose()
                objcommand = Nothing

            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
        End Try
    End Sub


    Private Sub ucCon2_e_ok_click() Handles ucCon2.e_ok_click
        Dim cbox As UserControlCheckBox

        Dim odoc As DocList
        Dim objcommand As clsSqlConn
        Dim ltr As DbTran
        Dim lsMsg As String = ""
        'Dim ri As RepeaterItem
        Try
            ltr = New DbTran
            odoc = New DocList
            'odoc.pDocId =
            'odoc.ExistsInInbox()
            objcommand = New clsSqlConn(ltr.pTran)
            Dim lsDate As String = DateTime.Now.ToString
            For Each ri As RepeaterItem In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbArchive"), UserControlCheckBox)
                    If cbox.BoxCheck Then
                        odoc.pDocId = DirectCast(ri.FindControl("lDocId"), Literal).Text.Trim
                        odoc.pFolderId = Trim(ucCon2.pID)
                        odoc.pUserId = DocSession.sUserId
                        odoc.pSeqNo = "0"
                        'If lType.Text.Trim = "Sent" Then
                        '    odoc.DeleteFromOutbox(objcommand)
                        '    lsMsg = "Document has been deleted from your Sent box."
                        'Else
                        odoc.MoveDocFolder(objcommand) ', odoc.pExistsInInbox)
                        lsMsg = "Document has been moved to " & ucCon2.pDesc & " folder."

                        'End If

                    End If
                End If

            Next
            ltr.pTran.Commit()

            If lsMsg <> "" Then
                'DocSession.sFolderID = ucCon2.pID.Trim
                'DocSession.sFolderDesc = ucCon2.pDesc
            End If

            'Repeater1.Visible = False
            'ucPager.Visible = False
            'pPager.Update()
            hfCurrent.Value = "1"
            RetrieveDocs()
            'ucFolder.RetrieveUserFolders()

            pnlRepeater.Update()
            Master.ShowMessage(lsMsg)
        Catch ex As Exception
            Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally
            If Not objcommand Is Nothing Then
                objcommand.Dispose()
                objcommand = Nothing

            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
        End Try
    End Sub

    Private Sub rbSelection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSelection.SelectedIndexChanged
        If rbSelection.SelectedValue = "A" Then

            Response.Redirect("alldocs.aspx")
        ElseIf rbSelection.SelectedValue = "I" Then

            Response.Redirect("inbox.aspx")
        ElseIf rbSelection.SelectedValue = "S" Then

            Response.Redirect("sent.aspx")
        ElseIf rbSelection.SelectedValue = "N" Then

            Response.Redirect("newdocs.aspx")
        End If
    End Sub

    Private Sub LoadCookies()
        Dim filterVisible As Boolean = False
        If Not Request.Cookies("sentDocType") Is Nothing Then
            If Request.Cookies("sentDocType").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlFilterDocType.Items.FindByValue(Request.Cookies("sentDocType").Value)
                If Not IsNothing(li) Then
                    dlFilterDocType.SelectedValue = Request.Cookies("sentDocType").Value
                    filterVisible = True
                End If
            End If
        End If
        If Not Request.Cookies("sentClassification") Is Nothing Then
            If Request.Cookies("sentClassification").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlClassification.Items.FindByValue(Request.Cookies("sentClassification").Value)
                If Not IsNothing(li) Then
                    dlClassification.SelectedValue = Request.Cookies("sentClassification").Value
                    filterVisible = True
                End If
            End If
        End If
        If Not Request.Cookies("sentStatus") Is Nothing Then
            If Request.Cookies("sentStatus").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlStatus.Items.FindByValue(Request.Cookies("sentStatus").Value)
                If Not IsNothing(li) Then
                    dlStatus.SelectedValue = Request.Cookies("sentStatus").Value
                    filterVisible = True
                End If
            End If
        End If
        If Not Request.Cookies("sentOffice") Is Nothing Then
            If Request.Cookies("sentOffice").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlOffice.Items.FindByValue(Request.Cookies("sentOffice").Value)
                If Not IsNothing(li) Then
                    dlOffice.SelectedValue = Request.Cookies("sentOffice").Value
                    filterVisible = True
                End If
            End If
        End If
        If Not Request.Cookies("sentShow") Is Nothing Then
            If Request.Cookies("sentShow").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlFilterDocType.Items.FindByValue(Request.Cookies("sentShow").Value)
                If Not IsNothing(li) Then
                    dlFilterDocType.SelectedValue = Request.Cookies("sentShow").Value
                    filterVisible = True
                End If
            End If
        End If
        If Not Request.Cookies("sentAuthor") Is Nothing Then
            If Request.Cookies("sentAuthor").Value <> "" Then

                txAuthor.Text = Request.Cookies("sentAuthor").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentRefno") Is Nothing Then
            If Request.Cookies("sentRefno").Value <> "" Then

                tbFilterRefNo.Text = Request.Cookies("sentRefno").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentPointPerson") Is Nothing Then
            If Request.Cookies("sentPointPerson").Value <> "" Then

                tbRoutedTo.Text = Request.Cookies("sentPointPerson").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentDateCreatedFrom") Is Nothing Then
            If Request.Cookies("sentDateCreatedFrom").Value <> "" Then

                txDateCreatedFrom.Text = Request.Cookies("sentDateCreatedFrom").Value

            End If
        End If
        If Not Request.Cookies("sentDateCreatedTo") Is Nothing Then
            If Request.Cookies("sentDateCreatedTo").Value <> "" Then
                txDateCreatedTo.Text = Request.Cookies("sentDateCreatedTo").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentTitle") Is Nothing Then
            If Request.Cookies("sentTitle").Value <> "" Then
                tbFilterTitle.Text = Request.Cookies("sentTitle").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentCurrent") Is Nothing Then
            If Request.Cookies("sentCurrent").Value <> "" Then
                hfCurrent.Value = Request.Cookies("sentCurrent").Value
                filterVisible = True
            End If
        End If
        If Not Request.Cookies("sentSortCol") Is Nothing Then
            If Request.Cookies("sentSortCol").Value <> "" Then
                hfSortCol.Value = Request.Cookies("sentSortCol").Value

            End If
        End If
        If Not Request.Cookies("sentSortOrder") Is Nothing Then
            If Request.Cookies("sentSortOrder").Value <> "" Then
                hfSortOrder.Value = Request.Cookies("sentSortOrder").Value

            End If
        End If
        pFilter.Visible = filterVisible
    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("sentDocType")
        mycookie.Value = dlFilterDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentTitle")
        mycookie.Value = tbFilterTitle.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentCurrent")
        mycookie.Value = hfCurrent.Value
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentAuthor")
        mycookie.Value = txAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentShow")
        mycookie.Value = dlShow.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentStatus")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentDateCreatedFrom")
        mycookie.Value = txDateCreatedFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentDateCreatedTo")
        mycookie.Value = txDateCreatedTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentOffice")
        mycookie.Value = dlOffice.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentPointPerson")
        mycookie.Value = tbRoutedTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentRefno")
        mycookie.Value = tbFilterRefNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentSortCol")
        mycookie.Value = hfSortCol.Value
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentSortOrder")
        mycookie.Value = hfSortOrder.Value
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub
    Private Sub ResetCookies()

        Dim mycookie As HttpCookie = New HttpCookie("sentDocType")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentTitle")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentCurrent")
        mycookie.Value = hfCurrent.Value
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentAuthor")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentShow")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentStatus")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentDateCreatedFrom")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentDateCreatedTo")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentClassification")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentPointPerson")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentRefno")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("sentOffice")
        mycookie.Value = ""
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub
End Class
