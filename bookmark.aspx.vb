Imports System
Imports System.Data.SqlClient

Public Class bookmark
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage
        Master.SelectTab("Bookmarked")
    End Sub

    Private Sub ShowUploadMessage()
        Master.ShowMessage(ucUpload.Message)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveBookMark()
            If DocSession.sDocAccess > 2 Then
                ucAddDoc.Visible = True
            Else
                ucAddDoc.Visible = False
            End If
        End If

    End Sub

    Private Sub AddDoc()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Private Sub RetrieveBookMark()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doctypeloc")
        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Try
            'objCommand = New clsSqlConn
            'objCommand.pCommandType = CommandType.StoredProcedure

            'objCommand.pCommandText = "xMSP_DOCBOOKMARKGET"
            'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
            'objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            'objCommand.ParametersAddWithValue("@asLoc", lsLoc)
            Dim osp As New DocBookmark
            osp.pGroupId = DocSession.sUserGroup
            osp.pUserId = DocSession.sUserId
            ldata = osp.RetrieveBookMark

            Repeater1.DataSource = ldata
            Repeater1.DataBind()
            'lRecordCount.Text = ldata.Rows.Count()
            pnlRepeater.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If
        End Try

    End Sub

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
        End If
    End Sub

    Private Sub BookmarkDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lBM As Literal = DirectCast(rptItem.FindControl("lBookmarked"), Literal)
        Dim imgUnButton As ImageButton = DirectCast(rptItem.FindControl("ImgUnbookmark"), ImageButton)

        Dim bm As DocBookmark = New DocBookmark
        bm.pDocId = CInt(lrDocId.Text)
        bm.pUserId = DocSession.sUserId
        bm.DocBookmark("A")

        'DocBookmark(CInt(lrDocId.Text), "0")
        imgButton.Visible = Not imgButton.Visible
        imgUnButton.Visible = Not imgUnButton.Visible

        pnlRepeater.Update()

    End Sub

    Private Sub AddLinks(ByVal asDocId As Integer)
        Dim ImgCB As ImageButton
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        Dim lDocId As Literal
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer

        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "xMSP_DOCLINKSADD"
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
            'objCommand.ClearParameter()
            'objCommand.ParametersAddWithValue("@DocId", aiDocId)
            'objCommand.ParametersAddWithValue("@LinkDocId", aiLDocId)
            'objCommand.ParametersAddWithValue("@CreatedBye", DocSession.sUserId)
            'objCommand.ParametersAddWithValue("@CreateIPAddress", Request.UserHostAddress)

            'objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Private Sub ShowDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        'Dim lFile As Literal = DirectCast(rptItem.FindControl("lFileName"), Literal)
        'Dim lT As Label = DirectCast(rptItem.FindControl("lTitle"), Label)
        Dim lD As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lDT As Literal = DirectCast(rptItem.FindControl("lDocType"), Literal)
        Dim lDTAccess As Literal = DirectCast(rptItem.FindControl("lGroupAccessId"), Literal)
        'Dim lCreatedDate As Literal = DirectCast(rptItem.FindControl("lCreatedDate"), Literal)
        'Dim lAuthor As Literal = DirectCast(rptItem.FindControl("lAuthor"), Literal)
        'Master.AssignImage = lsLoc & lD.Text & "__" & lFile.Text
        'Master.SetTitle = lT.Text
        'Master.SetDocId = lD.Text
        'Master.SetDocType = lDT.Text
        'Master.SetAuthor = lAuthor.Text
        ' Master.SetCreatedDate = lCreatedDate.Text
        'Master.SetSource = lsLoc & lD.Text & "__" & lFile.Text
        'Master.ShowImageDocument = True
        'Session("DocId") = lD.Text
        'Session("DocType") = lDT.Text
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
        Finally
            If Not rptL Is Nothing Then
                rptL.Dispose()
            End If

            If Not rptItem Is Nothing Then
                rptItem.Dispose()
            End If
        End Try
    End Sub

    Private Function RetrieveDocLinks(ByVal aiDocId As Integer) As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlClient.SqlDataAdapter
        Dim ldata As DataTable
        'Dim lrow As DataRow = ldata.NewRow
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pDocId = aiDocId
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCLINKSGET
            'Else

            '    objCommand.pCommandType = CommandType.StoredProcedure
            '    objCommand.pCommandText = "xMSP_DOCLINKSGET"
            '    objCommand.ParametersAddWithValue("@DocId", aiDocId)

            'End If
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
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        
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

    Private Sub TagDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
        Dim rptT As Repeater = DirectCast(rptItem.FindControl("rptTags"), Repeater)
        lPnl.Visible = Not lPnl.Visible
        If lPnl.Visible Then
            rptT.DataSource = RetrieveDocTags(CInt(lrDocId.Text))
            rptT.DataBind()
            rptT.Visible = True
        End If
        pnlRepeater.Update()

    End Sub

    Private Sub SaveTagDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgButton As Button = DirectCast(sender, Button)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        'Dim lttags As Literal = DirectCast(rptItem.FindControl("ltags"), Literal)
        Dim ltxt As TextBox = DirectCast(rptItem.FindControl("txtTag"), TextBox)
        Dim ltitle As Label = DirectCast(rptItem.FindControl("ltitle"), Label)
        Dim lPnl As Panel = DirectCast(rptItem.FindControl("pTag"), Panel)
        Dim oTag As New DocTags
        oTag.pDocId = CInt(lrDocId.Text)
        oTag.pIpAddress = Request.UserHostAddress
        oTag.pTag = ltxt.Text
        oTag.pUserId = DocSession.sUserId 'Session("userid")
        oTag.pDocName = ltitle.Text
        oTag.SaveDocTag()
        ltitle.Text = ""
        'lttags.Text = IIf(lttags.Text.Trim = "", ltxt.Text, lttags.Text.Trim & ", " & ltxt.Text)
        'lPnl.Visible = Not lPnl.Visible
        RetrieveDocTags(lrDocId.Text)

        Dim rptT As Repeater = DirectCast(rptItem.FindControl("rptTags"), Repeater)
        'lPnl.Visible = Not lPnl.Visible
        If lPnl.Visible Then
            rptT.DataSource = RetrieveDocTags(CInt(lrDocId.Text))
            rptT.DataBind()
            rptT.Visible = True
        End If

        pnlRepeater.Update()

    End Sub
End Class