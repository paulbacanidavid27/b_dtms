Public Class ucCheckBox
    Inherits System.Web.UI.UserControl

    Public Event e_check()

    Public Event e_check2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public WriteOnly Property pImageUrl As String
        Set(ByVal value As String)
            imgSelect.ImageUrl = value
        End Set
    End Property


    Public WriteOnly Property DefaultCheck As Boolean

        Set(ByVal value As Boolean)
            ImgSelected.Visible = value
            imgSelect.Visible = Not value
        End Set

    End Property

    Public Property BoxCheck As Boolean
        Get
            Return imgSelected.Visible
        End Get
        Set(ByVal value As Boolean)
            imgSelected.Visible = value
            imgSelect.Visible = Not value
        End Set

    End Property


    Protected Sub imgSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSelect.Click
        imgSelected.Visible = Not imgSelected.Visible
        imgSelect.Visible = Not imgSelect.Visible
        cbBox.Update()
        RaiseEvent e_check()
        RaiseEvent e_check2(sender, e)
    End Sub

    Protected Sub ImgSelected_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSelected.Click
        imgSelect.Visible = Not imgSelect.Visible
        imgSelected.Visible = Not imgSelected.Visible
        cbBox.Update()
        RaiseEvent e_check()
        RaiseEvent e_check2(sender, e)
    End Sub
End Class