Public Class FileUpload
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("username") Is Nothing Then
            If Session("username") <> "" Then


                lUserInfo.Text = Session("username")
                lUserInfo.Visible = True
                pSearch.Visible = True
                pUser.Visible = True

            Else
                lUserInfo.Text = Session("username")
                lUserInfo.Visible = False
                pSearch.Visible = False
                pUser.Visible = False
            End If
        End If
        pnlLogin.Update()


    End Sub

    'Protected Sub imgLogout_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogout.Click
    '    Session("userid") = ""
    '    Session("username") = ""
    '    Session("email") = ""
    '    Session("UserRole") = ""
    '    Session("UserGroup") = ""

    '    Response.Redirect("default.aspx")

    'End Sub

    Protected Sub imgUserInfo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUserInfo.Click
        pLogin.Visible = Not pLogin.Visible
        pnlLogin.Update()


    End Sub

    Protected Sub Logout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Logout.Click
        Session("username") = ""
        Response.Redirect("default.aspx")
    End Sub
End Class