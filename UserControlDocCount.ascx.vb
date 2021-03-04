Public Class UserControlDocCount
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveDocs()
        End If
    End Sub
    Public Sub RetrieveDocs()
        Dim odoc As New DocList
        odoc.pUserId = DocSession.sUserId
        odoc.pGroupId = DocSession.sUserGroup

        'If ldata.Rows.Count > 0 Then
        lbsentd.Text = CStr(odoc.CountSentDoc) & " item(s)"
        lbnewd.Text = CStr(odoc.CountNewDoc) & " item(s)"
        lbyoursd.Text = CStr(odoc.CountOwnDoc) & " item(s)"

        'If ldata(0)("nd") = "0" Then
        '    lblnd.Visible = True
        '    lbnd.Visible = False
        'End If
        'End If
        upDocCount.update()

    End Sub

    Private Sub lbsentd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbsentd.Click
        
        Response.Redirect("sent.aspx?c=1")
    End Sub

    Private Sub lbnewd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbnewd.Click

        Response.Redirect("newdocs.aspx?c=1")
    End Sub

    Private Sub lbyoursd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbyoursd.Click
        
        Response.Redirect("inbox.aspx?c=1")
    End Sub
End Class