Public Class UserControlBookMark
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveBookmark()
        End If

    End Sub
    Public Sub RetrieveBookmark()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable

        Try
            ldata = New DataTable
            'objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            '    Dim osp As New cls_storedproc
            '    osp.pGroupId = DocSession.sUserGroup
            '    osp.pUserId = DocSession.sUserId
            '    objCommand.pCommandType = CommandType.Text
            '    objCommand.pCommandText = osp.xMSP_DOCBOOKMARKGET5
            'Else
            '    objCommand.pCommandType = CommandType.StoredProcedure
            '    objCommand.pCommandText = "xMSP_DOCBOOKMARKGET5"
            '    objCommand.ParametersAddWithValue("@userId", DocSession.sUserId)
            '    objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            'End If
            Dim osp As New DocBookmark
            osp.pGroupId = DocSession.sUserGroup
            osp.pUserId = DocSession.sUserId


            ldata = osp.RetrieveBookMarkTop("4")

            If ldata.Rows.Count > 3 Then
                ldata(3).Delete()
                lbMoreBookmark.Visible = True
            ElseIf ldata.Rows.Count = 0 Then
                ShowHide()

            End If
            rBookmark.DataSource = ldata
            rBookmark.DataBind()
            pnlBk.Update()
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

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        DocSession.sDocType = DirectCast(ri.FindControl("lDocType"), Literal).Text
        DocSession.sDocTypeAccess = DirectCast(ri.FindControl("lGroupAccessId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Private Sub lbMoreBookmark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbMoreBookmark.Click
        Response.Redirect("bookmark.aspx")
    End Sub

    Private Sub lbBookmrk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBookMrk.Click
        Response.Redirect("bookmark.aspx")
    End Sub

    Private Sub rBookmark_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rBookmark.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgUB As ImageButton = DirectCast(e.Item.FindControl("ImgUnbookmark"), ImageButton)
            AddHandler imgUB.Click, AddressOf UnBookmarkDocument2
        End If
    End Sub

    Private Sub UnBookmarkDocument2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lrDocId As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim bm As DocBookmark = New DocBookmark

        'delete bookmark
        bm.pDocId = CInt(lrDocId.Text)
        bm.pUserId = DocSession.sUserId
        bm.DocBookmark("D")

        RetrieveBookmark()

    End Sub

    'Private Sub DocBookmark(ByVal aiDocId As Integer, ByVal asBM As String)
    '    'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    'Dim oConn As New SqlConnection(str)

    '    Dim objCommand As clsSqlConn

    '    Try
    '        Dim dm As DocBookmark = New DocBookmark
    '        dm.pUserId = aiDocId
    '            dm.pDocId=

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

    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        ShowHide()
    End Sub
    Private Sub ShowHide()
        Pbk.Visible = Not Pbk.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If
        pnlBk.Update()
    End Sub
End Class