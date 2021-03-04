Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()

    End Sub

    Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLogin.Click
        Response.Redirect("default.aspx")
    End Sub
End Class