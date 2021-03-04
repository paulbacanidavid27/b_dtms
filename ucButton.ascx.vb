Public Class ucButton
    Inherits System.Web.UI.UserControl
    Public Event e_click()

    Public Property pImage As String
        Get
            Return ImageButton1.ImageUrl
        End Get
        Set(ByVal value As String)
            ImageButton1.ImageUrl = value
        End Set
    End Property

    Public Property pText As String
        Get
            Return lbl.Text
        End Get
        Set(ByVal value As String)
            lbl.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lbAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAdd.Click
        RaiseEvent e_click()
    End Sub
End Class