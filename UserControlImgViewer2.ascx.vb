Public Class UserControlImgViewer2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ViewImg()
    End Sub
    Public Sub ViewImg()
        imgHandlerx.ImageUrl = "viewDoc.aspx?dt=" & Date.Now()
        'imgHandlerx.Visible = True
        imgHandlerx.Style("width") = "100%"
        imgHandlerx.Style("Height") = "100%"
        imgHandlerx.Visible = True

        'dlZoom.Visible = True
        'lZoom.Visible = True
        'hfWidth.Value = imgHandler.Width.ToString
        'hfHeight.Value = imgHandler.Height.ToString
        'If DocSession.sCanPrint = "True" Then
        '    'imgSrc.Visible = True
        'Else
        '    imgSrc.Visible = False
        'End If
    End Sub

    'Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
    '    Me.Visible = False
    'End Sub

    Private Sub imgHandlerx_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgHandlerx.Load
        Image1.Visible = False

    End Sub

    Private Sub imgHandlerx_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgHandlerx.PreRender

    End Sub

    Private Sub imgHandlerx_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgHandlerx.Unload
        Image1.Visible = False
    End Sub

    Protected Overrides Sub Finalize()
        Image1.Visible = False
        MyBase.Finalize()
    End Sub
End Class