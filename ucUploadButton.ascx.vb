Public Class ucUploadButton
    Inherits System.Web.UI.UserControl
    Public Event e_click()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lbAddDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddDoc.Click
        RaiseEvent e_click()
    End Sub
End Class