Imports System
Imports System.Data.SqlClient
Public Class issuances
    Inherits System.Web.UI.Page
#Region "Page Events"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If

        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucFolder.e_ShowMessage, AddressOf ShowFolderMessage

        '01/17/2014
        AddHandler ucFolder.e_LinkButton, AddressOf SearchFolderDocs

        Master.SelectTab("Issuances")
        'ge ucDocRouting.ShowSearch()
    End Sub
    Private Sub ShowFolderMessage()
        Master.ShowMessage(ucFolder.Message)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'DocSession.doc_DocAuthor = DocSession.sUserName
            If DocSession.sfilter.Trim = "1" Then
                'DocSession.doc_DocCurrentPage = "1"
                hfCurrent.Value = "1"
            Else
                If DocSession.doc_DocCurrentPage <> "" Then
                    hfCurrent.Value = DocSession.doc_DocCurrentPage
                Else
                    hfCurrent.Value = "1"
                End If
            End If

            DocSession.sfilter = ""
            'GetDocType()
            'GetDocStatus()
            If DocSession.sUserRole = "A" Then
                RetrieveUserFolders()
            End If

            SetCookies()

            RetrieveDocs()

            DocSession.DocumentPage = "issuances.aspx"
        End If
    End Sub
    Private Sub SetCookies()
        'If DocSession.doc_DocType <> "" Then
        '    dlFilterDocType.SelectedValue = DocSession.doc_DocType

        'End If
        If DocSession.DocTitle <> "" Then
            tbFilterTitle.Text = DocSession.DocTitle

        End If

        If DocSession.DocAuthor <> "" Then
            txAuthor.Text = DocSession.DocAuthor
        End If
        If DocSession.DocCreatedFrom <> "" Then
            txDateCreatedFrom.Text = DocSession.DocCreatedFrom
        End If
        If DocSession.DocCreatedTo <> "" Then
            txDateCreatedTo.Text = DocSession.DocCreatedTo
        End If
        If DocSession.DocClassification <> "" Then
            dlClassification.SelectedValue = DocSession.DocClassification
        End If
    End Sub
#End Region

#Region "Populator Methods"



    'Private Sub GetDocType()
    '    'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    'Dim oConn As New SqlConnection(str)

    '    'Dim objCommand As clsSqlConn
    '    'Dim adpSecurity As New SqlDataAdapter
    '    Dim ldata As DataTable
    '    Dim lrow As DataRow

    '    Dim oType As DocTypes
    '    Try
    '        oType = New DocTypes
    '        oType.pGroupId = DocSession.sUserGroup
    '        ldata = oType.GetDocType

    '        lrow = ldata.NewRow

    '        If ldata.Rows.Count = 0 Then
    '            'imgAddDoc.ToolTip = "You don't have permission to create a new document."
    '            Master.ShowMessage("You don't have permission to create a new document.")
    '            'imgAddDoc.Enabled = False
    '            ucAddDoc.Visible = False
    '        Else

    '            lrow("DocType") = ""
    '            lrow("DocName") = "-All-"

    '        End If

    '        ldata.Rows.InsertAt(lrow, 0)
    '        dlFilterDocType.DataSource = ldata
    '        dlFilterDocType.DataValueField = "DocType"
    '        dlFilterDocType.DataTextField = "DocName"
    '        dlFilterDocType.DataBind()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '    End Try

    'End Sub

    'Private Sub GetDocStatus()

    '    Dim oDoc As New DocTypes
    '    Using ldata As DataTable = oDoc.GetDocStatus
    '        Dim lrow As DataRow
    '        If ldata.Rows.Count > 0 Then
    '            lrow = ldata.NewRow
    '            lrow("statusid") = "0"
    '            lrow("description") = "-All-"
    '            ldata.Rows.InsertAt(lrow, 0)
    '        End If

    '        dlStatus.DataTextField = "description"
    '        dlStatus.DataValueField = "statusid"
    '        dlStatus.DataSource = ldata

    '        dlStatus.DataBind()
    '    End Using

    'End Sub


    Private Sub RetrieveDocs()

        Dim ldata As DataTable
        Master.HideMessage()
        Try

            Dim oDocs As New DocList
            Dim lsTotalRows As String
            oDocs.pRefNo = tbFilterRefNo.Text
            oDocs.pDocType = DocSession.IssuancesDocType
            oDocs.pDocTitle = tbFilterTitle.Text
            'oDocs.pRoutedTo = tbRoutedTo.Text
            'oDocs.pDocStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
            oDocs.pClassificationCode = dlClassification.SelectedValue
            oDocs.pAuthor = txAuthor.Text.Trim
            oDocs.pCreatedDateFrom = txDateCreatedFrom.Text
            oDocs.pCreatedDateTo = txDateCreatedTo.Text
            oDocs.pGroupId = DocSession.sUserGroup
            oDocs.pIdx = hfCurrent.Value 'DocSession.doc_DocCurrentPage
            oDocs.pRowsPerPage = CInt(DocSession.RowsPerPage)
            oDocs.pUserId = DocSession.sUserId
            oDocs.pSortOrder = hfSortOrder.Value
            oDocs.pSortCol = hfSortCol.Value
            'lType.Text = "DBM Issuances"
            oDocs.pFolderId = ucFolder.pFolderId
            lsTotalRows = CStr(oDocs.IssuancesCount)
            If CInt(lsTotalRows) > 0 Then

                ldata = oDocs.IssuancesRetrieve
                If ldata.Rows.Count > DocSession.RowsPerPage Then
                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
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
                PopupMenu.Visible = False
                pPager.Update()
            End If

            pnlRepeater.Update()
            'upTHeader.Update()
            'pnlUFolder.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try

    End Sub


#End Region


#Region "Repeater Events"
    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated

        Dim imgU As ImageButton
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.sUserRole = "A" Then
                DirectCast(e.Item.FindControl("imgMove"), ImageButton).Visible = True

            Else
                DirectCast(e.Item.FindControl("imgMove"), ImageButton).Visible = False

            End If

        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            imgU = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            AddHandler imgU.Click, AddressOf UpdateDocType
        End If

    End Sub
    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgFT As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lFN As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lFN.Text.Trim, 4))

            imgFT.ImageUrl = lext

            If DirectCast(e.Item.FindControl("lOffice"), Literal).Text <> "" Then
                DirectCast(e.Item.FindControl("AssignedTo"), Literal).Text = DirectCast(e.Item.FindControl("AssignedTo"), Literal).Text & "(" & DirectCast(e.Item.FindControl("lOffice"), Literal).Text & ")"
            End If
            If DirectCast(e.Item.FindControl("lcby"), Literal).Text = DocSession.sUserId OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "A" Then
                imgFT.Enabled = True
            Else
                imgFT.Enabled = False
            End If
            If DocSession.sUserRole = "A" Then
                DirectCast(e.Item.FindControl("cbMove"), CheckBox).Visible = True
            Else
                DirectCast(e.Item.FindControl("cbMove"), CheckBox).Visible = False
            End If
        End If
        'End If
    End Sub


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
            If DirectCast(rItems.FindControl("cbMove"), CheckBox).Checked Then
                lbret = True
                Exit For
            End If
        Next
        Return lbret
    End Function

    Private Sub UpdateDocType(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        'Dim DocID As String = DirectCast(rptitem.FindControl("lDocId"), Literal).Text
        'Dim FVersion As String = DirectCast(rptitem.FindControl("lFileVersion"), Literal).Text
        'Dim FileName As String = DirectCast(rptitem.FindControl("lFileName"), Literal).Text
        'Dim lRef As String = DirectCast(rptitem.FindControl("lRef"), Literal).Text

        ''--new folder
        'Dim CreatedDate As String = DirectCast(rptitem.FindControl("lCDate"), Label).Text
        'Dim sYear, sMonth As String
        'sYear = Year(CDate(CreatedDate)).ToString
        'sMonth = MonthName(Month(CDate(CreatedDate))).ToString
        'DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lRef & "\" & DocID & "_" & FVersion & "_" & FileName
        'If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) AndAlso
        ' Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then

        '    DocSession.sCurrentFile = DocID & "_" & FVersion & "_" & FileName
        'End If
        ''DocSession.sCanPrint = True
        ''DocSession.sCanDownload = True

        'DisplayDoc()


        Dim ri As RepeaterItem
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        'Dim lDocTypeAccess As Literal = DirectCast(ri.FindControl("lDocTypeAccess"), Literal)
        Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)

        DocSession.sDocID = lDocId.Text
        DocSession.sDocType = lDocType.Text
        'DocSession.sDocTypeAccess = lDocTypeAccess.Text
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
    'Private Sub DeleteDocList()
    '    If WithSelected() Then
    '        ucCon.Visible = True
    '    Else
    '        Master.ShowMessage("Please select a record before clicking Delete button.")
    '    End If
    '    pnlRepeater.Update()


    'End Sub
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
        'If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort1
        '    imgSort1.Visible = True
        'Else
        '    imgSort1.Visible = False
        'End If
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
        'If imgSort8.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort8
        '    imgSort8.Visible = True
        'Else
        '    imgSort8.Visible = False
        'End If
        'If imgSort9.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort9
        '    imgSort9.Visible = True
        'Else
        '    imgSort9.Visible = False
        'End If

        'Dim oDocList As New DocList

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                img.ToolTip = "Descending"
                'oDocList.pSortOrder = "Desc"
                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                img.ToolTip = "Ascending"
                'oDocList.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            'oDocList.pSortOrder = "Asc"
            img.ToolTip = "Ascending"
            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If
        hfSortCol.Value = lbSort.Text
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
        RetrieveDocs()
    End Sub
    Private Sub SearchFolderDocs()
        hfCurrent.Value = "1"
        'dlFilterDocType.SelectedValue = ""
        tbFilterTitle.Text = ""
        txAuthor.Text = ""
        txDateCreatedFrom.Text = ""
        txDateCreatedTo.Text = ""
        dlClassification.SelectedValue = ""
        
        RetrieveDocs()
        ShowResult()
        ucFolder.RetrieveIssuanceFolders()
        'RetrieveUserFolders()
    End Sub

    Private Sub ShowResult()
        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()


        'pAddDoc.Visible = False
        ''pnlAddDoc.Update()

        'pDeleteDoc.Visible = False
        'pnlDeleteDoc.Update()

        'pRepeater.Visible = True
        'pnlRepeater.Update()

        'imgAddDoc.Visible = True
        'pUpload1.Visible = False
        'pUpload2.Visible = False
        Master.ShowImageDocument = False
        pnlPage.Update()
    End Sub


#End Region
    Private Sub RetrieveUserFolders()
        Dim ldata As DataTable
        Try


            Dim oList As New DocList
            oList.pUserId = DocSession.sUserId
            ldata = oList.RetrieveIssuanceFolder()
            Dim lrow As DataRow
            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "New"
            lrow(2) = "0"
            ldata.Rows.InsertAt(lrow, 0)
            rptFolder.DataSource = ldata
            rptFolder.DataBind()
            PopupMenu.Visible = True
            updFolderMenu.Update()
        Catch ex As Exception
            Master.ShowMessage("Error occurred while retrieve document folders (" & ex.Message & ").")
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    Public Sub MoveDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgButton As LinkButton = DirectCast(sender, LinkButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lsFolderId As Literal = DirectCast(rptItem.FindControl("lFolderId"), Literal)
        Dim lsFolderDesc As Literal = DirectCast(rptItem.FindControl("lFolder"), Literal)

        If WithSelected() Then
            ucCon2.Visible = True
            ucCon2.pDesc = lsFolderDesc.Text
            ucCon2.pID = lsFolderId.Text
            pnlCon2.Update()
        Else
            Master.ShowMessage("Please select a record before clicking Move button.")
        End If
        pnlRepeater.Update()
    End Sub


    'Private Sub ucCon_e_ok_click() Handles ucCon.e_ok_click

    '    Dim cbox As UserControlCheckBox

    '    Dim odoc As DocList
    '    Dim objcommand As clsSqlConn
    '    Dim ltr As DbTran
    '    Dim lsMsg As String = ""
    '    'Dim ri As RepeaterItem
    '    Try
    '        ltr = New DbTran
    '        odoc = New DocList

    '        objcommand = New clsSqlConn(ltr.pTran)
    '        Dim lsDate As String = DateTime.Now.ToString
    '        For Each ri As RepeaterItem In Repeater1.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
    '                cbox = CType(ri.FindControl("cbArchive"), UserControlCheckBox)
    '                If cbox.BoxCheck Then
    '                    odoc.pDocId = DirectCast(ri.FindControl("lDocId"), Literal).Text
    '                    'odoc.pModifiedDate = lsDate
    '                    odoc.pUserId = DocSession.sUserId
    '                    'odoc.ArchiveDoc(objcommand)
    '                    If lType.Text.Trim = "New" Then
    '                        odoc.DeleteFromOutbox(objcommand)
    '                        lsMsg = "Document has been deleted from your Sent box."
    '                    Else
    '                        odoc.DeleteFromInbox(objcommand)
    '                        lsMsg = "Document has been deleted from your Inbox."

    '                    End If

    '                End If
    '            End If

    '        Next
    '        ltr.pTran.Commit()
    '        Master.ShowMessage(lsMsg)
    '        'Repeater1.Visible = False
    '        'ucPager.Visible = False
    '        'pPager.Update()
    '        hfCurrent.Value = "1"
    '        RetrieveDocs()

    '        pnlRepeater.Update()
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")
    '        If Not ltr Is Nothing Then
    '            ltr.pTran.Rollback()
    '        End If
    '    Finally
    '        If Not objcommand Is Nothing Then
    '            objcommand.Dispose()
    '            objcommand = Nothing

    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If
    '    End Try
    'End Sub


    Private Sub ucCon2_e_ok_click() Handles ucCon2.e_ok_click
        'Dim cbox As CheckBox

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
                    'cbox = DirectCast(ri.FindControl("cbMove"), CheckBox)
                    If DirectCast(ri.FindControl("cbMove"), CheckBox).Checked Then
                        odoc.pDocId = DirectCast(ri.FindControl("lDocId"), Literal).Text.Trim
                        odoc.pFolderId = Trim(ucCon2.pID)
                        odoc.pUserId = DocSession.sUserId
                        odoc.pSeqNo = "0"
                        'If lType.Text.Trim = "Sent" Then
                        '    odoc.DeleteFromOutbox(objcommand)
                        '    lsMsg = "Document has been deleted from your Sent box."
                        'Else
                        odoc.MoveDocIssuanceFolder(objcommand) ', odoc.pExistsInInbox)
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
            ucFolder.RetrieveIssuanceFolders()

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

    'Private Sub imgInbox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgInbox.Click
    '    DocSession.sFolderID = ""
    '    DocSession.sFolderDesc = ""
    '    SearchDocs()
    '    pnlRepeater.Update()
    'End Sub

    'Private Sub lbNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNew.Click
    '    DocSession.sfilter = "1"
    '    'DocSession.doc_DocCreated = Right("0" & Date.Now.ToShortDateString, 10)
    '    DocSession.doc_DocAuthor = "" 'DocSession.sUserName
    '    DocSession.doc_DocAuthorId = "" 'DocSession.sUserId
    '    DocSession.doc_DocCreatedFrom = ""
    '    DocSession.doc_DocCreatedTo = ""
    '    DocSession.doc_DocType = ""
    '    DocSession.doc_DocTitle = ""
    '    DocSession.doc_DocReceived = ""
    '    DocSession.doc_DocSent = ""
    '    DocSession.doc_DocClassification = ""
    '    Response.Redirect("newdocs.aspx")
    'End Sub

    'Private Sub lbSent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSent.Click
    '    DocSession.sfilter = "1"
    '    DocSession.doc_DocAuthor = "" 'DocSession.sUserName
    '    DocSession.doc_DocAuthorId = ""
    '    DocSession.doc_DocCreatedFrom = ""
    '    DocSession.doc_DocCreatedTo = ""
    '    DocSession.doc_DocType = ""
    '    DocSession.doc_DocTitle = ""
    '    DocSession.doc_DocReceived = ""
    '    DocSession.doc_DocSent = "1"
    '    DocSession.doc_DocClassification = ""
    '    Response.Redirect("sent.aspx")
    'End Sub

    ''Private Sub lbAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbOthers.Click
    ''    DocSession.sfilter = "1"
    ''    DocSession.doc_DocAuthor = "" 'DocSession.sUserName
    ''    DocSession.doc_DocAuthorId = ""
    ''    DocSession.doc_DocCreatedFrom = ""
    ''    DocSession.doc_DocCreatedTo = ""
    ''    DocSession.doc_DocType = ""
    ''    DocSession.doc_DocTitle = ""
    ''    DocSession.doc_DocReceived = ""
    ''    DocSession.doc_DocSent = "1"
    ''    DocSession.doc_DocClassification = ""
    ''    Response.Redirect("alldocs.aspx")
    ''End Sub
    'Private Sub lbyoursd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInbox.Click
    '    DocSession.sfilter = "1"
    '    DocSession.doc_DocAuthor = ""
    '    DocSession.doc_DocAuthorId = ""
    '    DocSession.doc_DocCreatedFrom = ""
    '    DocSession.doc_DocCreatedTo = ""
    '    DocSession.doc_DocType = ""
    '    DocSession.doc_DocTitle = ""
    '    DocSession.doc_DocReceived = "1"
    '    DocSession.doc_DocSent = ""
    '    DocSession.doc_DocClassification = ""
    '    Response.Redirect("inbox.aspx")
    'End Sub
#Region "Viewer"
    Private Sub DisplayDoc()

        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        'Dim lsFilePath As String = lsLoc
        ucViewer.Visible = False
        ucPDFViewer.Visible = False
        ucDocViewer.Visible = False
        If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
                'docvw.Visible = False
                'pnlImg.Visible = True
                ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
                'pnlImageDisp.Visible = True
                ucViewer.ViewImg()
                ucViewer.Visible = True
                'docvw.Attributes("src") = "viewDoc.aspx"
                'docvw.Visible = False

                'pnlDocView.Update()
                'pDocView.Update()

            ElseIf lsext = ".pdf" Then

                'docvw.Attributes("src") = "119_1_blank5.pdf"
                ucPDFViewer.ViewPDF()
                ucPDFViewer.Visible = True


                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                'pnlImg.Visible = False
                'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                ucDocViewer.ViewDoc()
                ucDocViewer.Visible = True

                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                ' pnlImg.Visible = False
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('" & "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc & "', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            End If
        ElseIf System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc2 & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then

                ucViewer.ViewImg()
                ucViewer.Visible = True

            ElseIf lsext = ".pdf" Then


                ucPDFViewer.ViewPDF()
                ucPDFViewer.Visible = True

            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                ucDocViewer.ViewDoc()
                ucDocViewer.Visible = True

            End If
        Else
            Master.ShowMessage("File associated with this document does not exists on the server. Please contact administrator.")
        End If
    End Sub
    Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
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
End Class
