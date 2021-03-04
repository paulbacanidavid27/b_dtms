Public Class UserControlAdminMenu
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'HideMenu()
    End Sub
    Private Sub HideMenu()
        If DocSession.sRegType <> "etp" Then
            div_4.Visible = False
            div_5.Visible = False
            div_6.Visible = False
            div_7.Visible = False
        End If
        If DocSession.sRegType = "bsx" Then
            div_8.Visible = False

        End If
    End Sub

    Public Sub SelectTab(ByVal lsTabSelected As String)
        If lsTabSelected = "User" Then
            div_1.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink1.Style.Item("color") = "#EEEEEE"
            HyperLink1.Attributes("onmouseover") = ""
            HyperLink1.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Group" Then
            div_2.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink2.Style.Item("color") = "#EEEEEE"
            HyperLink2.Attributes("onmouseover") = ""
            HyperLink2.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Doctype" Then
            div_3.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink3.Style.Item("color") = "#EEEEEE"
            HyperLink3.Attributes("onmouseover") = ""
            HyperLink3.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Import" Then
            div_4.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink4.Style.Item("color") = "#EEEEEE"
            HyperLink4.Attributes("onmouseover") = ""
            HyperLink4.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Purge" Then
            div_5.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink5.Style.Item("color") = "#EEEEEE"
            HyperLink5.Attributes("onmouseover") = ""
            HyperLink5.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Changelog" Then
            div_6.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink6.Style.Item("color") = "#EEEEEE"
            HyperLink6.Attributes("onmouseover") = ""
            HyperLink6.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Backup" Then
            div_7.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink7.Style.Item("color") = "#EEEEEE"
            HyperLink7.Attributes("onmouseover") = ""
            HyperLink7.Attributes("onmouseout") = ""
        ElseIf lsTabSelected = "Holiday" Then
            div_8.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
            HyperLink8.Style.Item("color") = "#EEEEEE"
            HyperLink8.Attributes("onmouseover") = ""
            HyperLink8.Attributes("onmouseout") = ""
        End If

    End Sub

End Class