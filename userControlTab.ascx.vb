Public Class userControlTab
    Inherits System.Web.UI.UserControl
    Dim lsTabSelected As String

    Public Property pSelectedTab As String
        Get
            Return lsTabSelected
        End Get
        Set(ByVal value As String)
            lsTabSelected = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If DocSession.sUserRole = "A" Then
                pAdmin.Visible = True
                pTracking.Visible = True
            Else
                pAdmin.Visible = False
                If DocSession.sUserRole = "D" Then
                    pTracking.Visible = True
                Else
                    pTracking.Visible = False
                End If
            End If
            If DocSession.sUserRole <> "B" Then
                pFormsIssuances.Visible = True
                pDashboard.Visible = True
                pSearch.Visible = True
            Else
                pFormsIssuances.Visible = False
                pDashboard.Visible = False
                pSearch.Visible = False
            End If
            If DocSession.sReportAccess = "1" Then
                pReport.Visible = True
            Else
                pReport.Visible = False
            End If
            'If DocSession.sImportDoc = "1" Then
            '    pImport.Visible = True
            'End If

            If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "A" Then
                pHoliday.Visible = True
            Else
                pHoliday.Visible = False
            End If
        End If
    End Sub
    Public Sub SelectTab()
        If lsTabSelected <> "Home" AndAlso lsTabSelected <> "Documents" AndAlso DocSession.sUserRole = "B" Then
            Response.Redirect("home.aspx")
        End If
        If lsTabSelected = "Home" Then
            lbHome.Visible = False
            lbHome2.Visible = True
            lHome.Visible = False
            t10.Attributes("class") = "sl"
            t11.Attributes("class") = "sm"
            t12.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Dashboard" Then
            lbDashBoard.Visible = False
            lDashboard.Visible = True
            t20.Attributes("class") = "sl"
            t21.Attributes("class") = "sm"
            t22.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Documents" Then
            lbDocuments.Visible = False
            lDocuments.Visible = True
            t30.Attributes("class") = "sl"
            t31.Attributes("class") = "sm"
            t32.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Forms" Then
            lbBookmarked.Visible = False
            lBookmarked.Visible = True
            t40.Attributes("class") = "sl"
            t41.Attributes("class") = "sm"
            t42.Attributes("class") = "sr"
            'ElseIf lsTabSelected = "Issuances" Then
            '    lbIssuances.Visible = False
            '    lIssuances.Visible = True
            '    Td10.Attributes("class") = "sl"
            '    Td11.Attributes("class") = "sm"
            '    Td12.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Reports" Then
            lbReports.Visible = False
            lReports.Visible = True
            t50.Attributes("class") = "sl"
            t51.Attributes("class") = "sm"
            t52.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Admin" Then
            'lbAdmin.Visible = False
            'lAdmin.Visible = True
            't60.Attributes("class") = "sl"
            't61.Attributes("class") = "sm"
            't62.Attributes("class") = "sr"
        ElseIf lsTabSelected = "Account" Then
            lnkAdmin.Visible = False
            lblAdmin.Visible = True
            Td1.Attributes("class") = "slg"
            Td2.Attributes("class") = "smg"
            Td3.Attributes("class") = "srg"
        ElseIf lsTabSelected = "Import" Then
            lbImport.Visible = False
            lImport.Visible = True
            Td7.Attributes("class") = "slg"
            Td8.Attributes("class") = "smg"
            Td9.Attributes("class") = "srg"
        ElseIf lsTabSelected = "Search" Then
            lnkSearch.Visible = False
            lblSearch.Visible = True
            Td4.Attributes("class") = "slg"
            Td5.Attributes("class") = "smg"
            Td6.Attributes("class") = "srg"
        ElseIf lsTabSelected = "Tracking" Then
            lnkTrack.Visible = False
            lblTrack.Visible = True
            Td16.Attributes("class") = "slg"
            Td17.Attributes("class") = "smg"
            Td18.Attributes("class") = "srg"
        ElseIf lsTabSelected = "Holiday" Then
            lbHoliday.Visible = False
            lHoliday.Visible = True
            Td19.Attributes("class") = "slg"
            Td20.Attributes("class") = "smg"
            Td21.Attributes("class") = "srg"
        End If

    End Sub

    Protected Sub lbHome_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbHome.Click
        Response.Redirect("home.aspx")
    End Sub
    Protected Sub lbHome2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbHome2.Click
        Response.Redirect("home.aspx")
    End Sub

    'Private Sub lbAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAdmin.Click
    '    Response.Redirect("user.aspx")
    'End Sub

    Private Sub lbDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocuments.Click
        If DocSession.DocumentPage = "alldocs.aspx" Then
            Response.Redirect("alldocs.aspx")
        ElseIf DocSession.DocumentPage = "newdocs.aspx" Then
            Response.Redirect("newdocs.aspx")
        ElseIf DocSession.DocumentPage = "sent.aspx" Then
            Response.Redirect("sent.aspx")
        Else
            Response.Redirect("inbox.aspx")
        End If

    End Sub

    Private Sub lbDashBoard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDashBoard.Click
        Response.Redirect("dashboard.aspx")
    End Sub

    Private Sub lbReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbReports.Click
        If DocSession.sUserRole = "U" Then
            Response.Redirect("DocumentReceivedCriteria.aspx")
        Else
            Response.Redirect("DocumentStatusCriteria.aspx")
        End If

    End Sub

    Private Sub lbBookmarked_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBookmarked.Click
        Response.Redirect("DocForms.aspx")
    End Sub
    Private Sub lbImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImport.Click
        Response.Redirect("Import.aspx")
    End Sub

    Private Sub lnkAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdmin.Click
        Response.Redirect("user.aspx")
    End Sub

    Private Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        If Not Request.Cookies("srchAS") Is Nothing Then
            If Request.Cookies("srchAS").Value <> "" Then
                Response.Redirect("search.aspx?as=" & Request.Cookies("srchAS").Value)
            Else
                Response.Redirect("search.aspx?as=1")
            End If

        Else
            Response.Redirect("search.aspx?as=1")
        End If

    End Sub


    Private Sub lnkTrack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTrack.Click
        Response.Redirect("TrackRefNo.aspx")
    End Sub

    Private Sub lbHoliday_Click(sender As Object, e As EventArgs) Handles lbHoliday.Click
        Response.Redirect("OfficeHoliday.aspx")
    End Sub
End Class