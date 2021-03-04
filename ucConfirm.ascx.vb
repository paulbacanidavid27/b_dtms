Public Class ucConfirm
    Inherits System.Web.UI.UserControl
    Public Event e_ok_click()

    Public Property pText As String
        Get
            Return ltext.Text
        End Get
        Set(ByVal value As String)
            ltext.Text = value
        End Set
    End Property

    Public Property pDesc As String
        Get
            Return lsDesc.Text
        End Get
        Set(ByVal value As String)
            lsDesc.Text = value
        End Set
    End Property

    Public Property pID As String
        Get
            Return lsID.Text
        End Get
        Set(ByVal value As String)
            lsID.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub imgSaveCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSaveCancel.Click
        Me.Visible = False
        'pnlConfirm.Update()
    End Sub

    Private Sub btContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btContinue.Click
        RaiseEvent e_ok_click()
        Me.Visible = False
        'pnlConfirm.Update()
    End Sub

    Private Sub btSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveCancel.Click
        Me.Visible = False
        'pnlConfirm.Update()
    End Sub
End Class