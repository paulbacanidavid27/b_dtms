Public Class AccessDenied
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DocSession.sUserId = ""
        End If

    End Sub

    Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLogin.Click
        Response.Redirect("default.aspx")
    End Sub
End Class