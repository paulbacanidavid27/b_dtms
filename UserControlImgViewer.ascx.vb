Public Class UserControlImgViewer
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ViewImg()
    End Sub
    Public Sub ViewImg()
        imgHandler.ImageUrl = "viewDoc.aspx?dt=" & Date.Now()
        imgHandler.Visible = True
        imgHandler.Style("width") = "100%"
        imgHandler.Style("Height") = "100%"
        dlZoom.Visible = True
        lZoom.Visible = True
        hfWidth.Value = imgHandler.Width.ToString
        hfHeight.Value = imgHandler.Height.ToString
        If DocSession.sCanPrint = "True" Then
            'imgSrc.Visible = True
        Else
            imgSrc.Visible = False
        End If
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Me.Visible = False
    End Sub
End Class