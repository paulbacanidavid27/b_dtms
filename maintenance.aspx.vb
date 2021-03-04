Public Class maintenance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If System.Configuration.ConfigurationManager.AppSettings("MaintenanceMode") = "0" Then
            '    Response.Redirect("default.aspx")
            'Else
            Label1.Text = System.Configuration.ConfigurationManager.AppSettings("onlinedate")
            'End If
        End If
    End Sub

    Private Sub MaintenanceMode()
        Dim soffline As String = System.Configuration.ConfigurationManager.AppSettings("OfflineDate").Trim
        If soffline <> "" AndAlso Request.QueryString("bpmm") <> "xTy" Then
            If IsDate(soffline) Then

                Dim lisec As Integer = DateDiff(DateInterval.Second, DateTime.Now, CDate(soffline))
                If lisec > 0 Then
                Else
                    Response.Redirect("maintenance.aspx")
                End If
            End If
        End If

    End Sub

End Class