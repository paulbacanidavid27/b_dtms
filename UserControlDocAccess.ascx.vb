Public Class UserControlDocAccess
    Inherits System.Web.UI.UserControl

    Public Property GroupId() As String
        Set(ByVal value As String)
            lGroupId.Text = value
        End Set
        Get
            Return lGroupId.Text
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub RetrieveDocAccess()
        Dim oAcc As New DocGroupAccess
        oAcc.pGroupId = GroupId
        Repeater1.DataSource = oAcc.RetrieveGroupAccess2()
        Repeater1.DataBind()

    End Sub
End Class