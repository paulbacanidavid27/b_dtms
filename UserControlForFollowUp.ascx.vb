Public Class UserControlForFollowUp
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveApproval()
        End If
    End Sub

    Private Sub RetrieveApproval()

        Dim oApprove As DocApproval
        Dim lrow As String
        Dim ldata As DataTable
        Try
            oApprove = New DocApproval
            oApprove.pCreatedBy = DocSession.sUserId
            oApprove.pGroupId = DocSession.sUserGroup
            ldata = oApprove.RetrieveDocApproval
            If ldata.Rows.Count > 10 Then
                ldata(10).Delete()
                'lbMoreApproval.Visible = True
                lrow = "10"
            Else
                lrow = ldata.Rows.Count.ToString()
            End If

            If ldata.Rows.Count > 0 Then
                lNotification.Text = lrow
            End If
            Repeater2.DataSource = ldata
            Repeater2.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not oApprove Is Nothing Then
                oApprove = Nothing
            End If
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub

    'Private Sub Repeater2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemCreated

    '    If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then


    '        Dim ib As ImageButton = DirectCast(e.Item.FindControl("imgDetails"), ImageButton)
    '        Dim ibM As ImageButton = DirectCast(e.Item.FindControl("imgDetailsMinus"), ImageButton)
    '        AddHandler ib.Click, AddressOf showApprovalStatus
    '        AddHandler ibM.Click, AddressOf showApprovalStatus

    '    End If

    'End Sub
    'Private Sub showApprovalStatus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ib As ImageButton = DirectCast(sender, ImageButton)

    '    Dim ri As RepeaterItem = DirectCast(ib.NamingContainer, RepeaterItem)
    '    Dim rptr As Repeater = DirectCast(ri.FindControl("rptApprovalStatus"), Repeater)
    '    Dim ibD As ImageButton = DirectCast(ri.FindControl("imgDetails"), ImageButton)
    '    Dim ibM As ImageButton = DirectCast(ri.FindControl("imgDetailsMinus"), ImageButton)
    '    ibD.Visible = Not ibD.Visible
    '    ibM.Visible = Not ibM.Visible

    '    rptr.Visible = Not rptr.Visible
    '    If rptr.Visible Then
    '        Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)
    '        Dim oApp As New DocApproval
    '        oApp.pDocId = lDocId.Text
    '        'oApp.pUserId = DocSession.sUserId
    '        rptr.DataSource = oApp.ApproverStatus
    '        rptr.DataBind()
    '    End If
    '    pnlPending.Update()
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
        pnlPending.Update()
    End Sub


    Private Sub lbMoreApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbMoreApproval.Click
        Response.Redirect("approval.aspx")
    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        DocSession.sSelectedTab = "Routing"
        Response.Redirect("view.aspx")
    End Sub
    Public Sub ViewDoc2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        DocSession.sSelectedTab = "Routing"
        Response.Redirect("view.aspx")
    End Sub

    Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "2" Then
                DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("background-color") = "blue"

            ElseIf DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "4" Then
                DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("background-color") = "red"

            End If

        End If
    End Sub
End Class