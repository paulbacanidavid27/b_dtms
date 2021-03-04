Public Class ucImageButton
    Inherits System.Web.UI.UserControl
    Public Event e_click()

    Public Property pHeight As String
        Get
            Return imgBT.Style.Item("height")
        End Get
        Set(ByVal value As String)
            imgBT.Height = value
        End Set
    End Property

    Public Property pWidth As String
        Get
            Return imgBT.Style.Item("width")
        End Get
        Set(ByVal value As String)
            imgBT.Width = value
        End Set
    End Property

    Public Property pToolTip As String
        Get
            Return imgBT.ToolTip
        End Get
        Set(ByVal value As String)
            imgBT.ToolTip = value
        End Set
    End Property

    Public Property pOnMouseOut As String
        Get
            Return imgBT.Attributes("onMouseOut")
        End Get
        Set(ByVal value As String)
            imgBT.Attributes("onMouseOut") = value
        End Set
    End Property
    Public Property pOnMouseOver As String
        Get
            Return imgBT.Attributes("onMouseOver")
        End Get
        Set(ByVal value As String)
            imgBT.Attributes("onMouseOver") = value
        End Set
    End Property

    Public Property pVisible As Boolean
        Get
            Return imgBT.Visible
        End Get
        Set(ByVal value As Boolean)
            imgBT.Visible = value
        End Set
    End Property
    Public Property pImageUrl As String
        Get
            Return imgBT.ImageUrl
        End Get
        Set(ByVal value As String)
            imgBT.ImageUrl = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub imgBT_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBT.Click
        RaiseEvent e_click()
    End Sub
End Class