Public Class UserControlOverDue
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveApproval()
        End If
    End Sub

    Private Sub RetrieveApproval()

        Dim oApprove As DocApproval
        Dim ldata As DataTable
        Try
            oApprove = New DocApproval
            oApprove.pApproverId = DocSession.sUserId
            oApprove.pGroupId = DocSession.sUserGroup
            ldata = oApprove.RetrieveOverDue
            If ldata.Rows.Count > 5 Then
                ldata(5).Delete()
                lbMoreApproval.Visible = True
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

    Private Sub Repeater2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemCreated

        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then


            Dim ib As ImageButton = DirectCast(e.Item.FindControl("imgDetails"), ImageButton)
            Dim ibM As ImageButton = DirectCast(e.Item.FindControl("imgDetailsMinus"), ImageButton)
            AddHandler ib.Click, AddressOf showApprovalStatus
            AddHandler ibM.Click, AddressOf showApprovalStatus

        End If

    End Sub
    Private Sub showApprovalStatus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ib As ImageButton = DirectCast(sender, ImageButton)

        Dim ri As RepeaterItem = DirectCast(ib.NamingContainer, RepeaterItem)
        Dim rptr As Repeater = DirectCast(ri.FindControl("rptApprovalStatus"), Repeater)
        Dim ibD As ImageButton = DirectCast(ri.FindControl("imgDetails"), ImageButton)
        Dim ibM As ImageButton = DirectCast(ri.FindControl("imgDetailsMinus"), ImageButton)
        ibD.Visible = Not ibD.Visible
        ibM.Visible = Not ibM.Visible

        rptr.Visible = Not rptr.Visible
        If rptr.Visible Then
            Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)
            Dim oApp As New DocApproval
            oApp.pDocId = lDocId.Text
            'oApp.pUserId = DocSession.sUserId
            rptr.DataSource = oApp.ApproverStatus
            rptr.DataBind()
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
            Dim imgDT As ImageButton = DirectCast(e.Item.FindControl("imgDocType"), ImageButton)
            Dim lFN As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lFN.Text.Trim, 4))
            imgDT.ImageUrl = lext
            Dim lDue As Label = DirectCast(e.Item.FindControl("lDue"), Label)

            Dim lyr As Integer = DateDiff(DateInterval.Year, CDate(lDue.Text), DateTime.Now)
            Dim lmonth As Integer = DateDiff(DateInterval.Month, CDate(lDue.Text), DateTime.Now)
            Dim lweek As Integer = DateDiff(DateInterval.Weekday, CDate(lDue.Text), DateTime.Now)
            Dim ldays As Integer = DateDiff(DateInterval.Day, CDate(lDue.Text), DateTime.Now)
            Dim lsdues As String = ""
            If lyr > 1 Then
                lsdues = "More than a year ago"
            ElseIf lyr = 1 Then
                lsdues = "A year ago"
            ElseIf lmonth > 1 AndAlso ldays < 12 Then
                lsdues = lmonth.ToString & " months ago"
            ElseIf lmonth = 1 And ldays >= 30 Then
                lsdues = "A month ago"
            ElseIf ldays > 1 Then
                lsdues = ldays.ToString & " days ago"
            ElseIf ldays = 1 Then
                lsdues = "Yesterday"
            End If
            lDue.Text = lsdues

        End If
    End Sub
End Class