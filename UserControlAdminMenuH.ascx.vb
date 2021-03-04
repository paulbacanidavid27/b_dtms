Public Class UserControlAdminMenuH
    Inherits System.Web.UI.UserControl

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If System.Configuration.ConfigurationManager.AppSettings("hidebackup") = "1" OrElse DocSession.OraClient Then
            div_7.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'HideMenu()  
        
    End Sub
    Private Sub HideMenu()
        If DocSession.sRegType <> "etp" Then
            'div_4.Visible = False
            div_5.Visible = False
            div_6.Visible = False
            div_7.Visible = False
        End If
        
    End Sub

    Public Sub SelectTab(ByVal lsTabSelected As String)
        'If lsTabSelected = "User" Then
        '    div_1.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink1.Attributes("onmouseover") = ""
        '    HyperLink1.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Group" Then
        '    div_2.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink2.Attributes("onmouseover") = ""
        '    HyperLink2.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Doctype" Then
        '    div_3.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink3.Attributes("onmouseover") = ""
        '    HyperLink3.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Import" Then
        '    div_4.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink4.Attributes("onmouseover") = ""
        '    HyperLink4.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Purge" Then
        '    div_5.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink5.Attributes("onmouseover") = ""
        '    HyperLink5.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Changelog" Then
        '    div_6.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink6.Attributes("onmouseover") = ""
        '    HyperLink6.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Backup" Then
        '    div_7.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink7.Attributes("onmouseover") = ""
        '    HyperLink7.Attributes("onmouseout") = ""
        'ElseIf lsTabSelected = "Holiday" Then
        '    div_8.Style.Item("background-image") = "url('images/tabs/btn_bg.png')"
        '    HyperLink8.Attributes("onmouseover") = ""
        '    HyperLink8.Attributes("onmouseout") = ""
        'End If

    End Sub

End Class