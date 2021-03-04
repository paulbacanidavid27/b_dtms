Public Class Login2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If DocSession.sUserLogin <> "admin" Then
                MaintenanceMode()
            Else
                tlusd.Value = ""
            End If

            ReadVersion()

            End If
    End Sub
    Private Sub ReadVersion()
        Dim lsPath As String = Server.MapPath(".")
        Using sr As System.IO.StreamReader = New System.IO.StreamReader(lsPath & "\version.txt")
            Dim line = sr.ReadToEnd
            imgLogo.ToolTip = "Version " & line
        End Using

    End Sub
    Private Sub MaintenanceMode()
        Dim soffline As String = tlusd.Value 'System.Configuration.ConfigurationManager.AppSettings("OfflineDate").Trim
        If soffline <> "" Then
            If IsDate(soffline) Then

                Dim lisec As Integer = DateDiff(DateInterval.Second, DateTime.Now, CDate(soffline))
                If lisec > 0 Then
                    lSM.Text = soffline
                    tlusd.Value = lisec.ToString
                Else
                    Response.Redirect("maintenance.aspx")
                End If
            End If
        End If

    End Sub
End Class