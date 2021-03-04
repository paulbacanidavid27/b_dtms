Public Class ucPrompt
    Inherits System.Web.UI.UserControl
    Public Event e_OK()
    Public WriteOnly Property pTitle As String
        Set(ByVal value As String)
            lsTitle.Text = value
        End Set
    End Property

    Public WriteOnly Property pCloseLabel As String
        Set(ByVal value As String)
            btClose.Text = value
        End Set
    End Property

    Public WriteOnly Property pOKLabel As String
        Set(ByVal value As String)
            btOK.Text = value
        End Set
    End Property

    Public WriteOnly Property pMessage As String
        Set(ByVal value As String)
            lMsg.Text = value
        End Set
    End Property

    Public WriteOnly Property pId As String
        Set(ByVal value As String)
            imgClose.OnClientClick = "hideWindow('" & value & "')"
            btClose.OnClientClick = "hideWindow('" & value & "');return true"
            btOK.OnClientClick = "hideWindow('" & value & "');return true"
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub btOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOK.Click
        RaiseEvent e_OK()
    End Sub
End Class