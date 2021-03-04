Public Class UserControlDocViewer
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ViewPDF()
    End Sub
    Public Sub ViewDoc()
        docvw.Attributes("src") = "DocViewer2.ashx?dt=" & Date.Now()
        docvw.Visible = True
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Me.Visible = False
    End Sub
End Class