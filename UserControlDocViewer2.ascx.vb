Public Class UserControlDocViewer2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ViewPDF()
    End Sub
    Public Sub ViewDoc()
        docvx.Attributes("src") = "DocViewer2.ashx?dt=" & Date.Now()
        docvx.Visible = True
    End Sub

End Class