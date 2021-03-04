Public Class DocumentListPerUserCriteria
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Reports")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sReportAccess <> "1" Then
            Master.ShowMessage("You don't have access to this report. Please contact the administrator.")
            'ReportViewer1.Visible = False
            btSearch.Visible = False
        Else
            If Not IsPostBack Then
                LoadPreviousCriteria()
            End If

        End If

    End Sub

    'Private Sub GetUsers()

    '    Dim ldata As DataTable
    '    Dim lrow As DataRow
    '    Dim oType As DocUser
    '    Try
    '        oType = New DocUser
    '        If DocSession.sUserRole <> "A" Then
    '            oType.pGroup = DocSession.sUserGroup
    '        End If

    '        ldata = oType.RetrieveUsers

    '        'lrow = ldata.NewRow
    '        'lrow(0) = ""
    '        'lrow(1) = ""
    '        'ldata.Rows.InsertAt(lrow, 0)
    '        cbUsers.DataSource = ldata
    '        cbUsers.DataValueField = "UserId"
    '        cbUsers.DataTextField = "UserName"
    '        cbUsers.DataBind()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '    End Try

    'End Sub
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Function GetUserList() As String
        Dim lsAppList As String
        lsAppList = ""
        Dim lsUser As String = ""
        Dim rpt As Repeater
        Try

            rpt = ucList.rptItems

            For Each loRptItem In rpt.Items
                Dim oDocs As New DocRouting
                If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then

                    If DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton).Visible Then


                        lsUser = "'" & Replace(DirectCast(loRptItem.FindControl("lUserId"), Literal).Text.Trim, "'", "''") & "'"
                        If lsAppList = "" Then
                            lsAppList = lsUser

                        Else
                            lsAppList = lsAppList & ", " & lsUser

                        End If


                        'End If


                    End If
                End If


            Next

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally
            If Not rpt Is Nothing Then
                rpt.Dispose()
            End If
        End Try
        Return lsAppList
    End Function

    Private Sub SetCookies(ByVal asValue As String)
        Dim mycookie As HttpCookie = New HttpCookie("udrUserDocReceivedFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrUserDocReceivedTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrUserDocClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrUserDocSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrUserDocSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrUserDocUserList")
        mycookie.Value = asValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrUserDocClassification") Is Nothing Then
            dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udrUserDocClassification").Value)
        Else

        End If
        If Not Request.Cookies("udrUserDocReceivedFrom") Is Nothing Then
            uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udrUserDocReceivedFrom").Value)
        Else

        End If
        If Not Request.Cookies("udrUserDocReceivedTo") Is Nothing Then
            uploadedate.Text = Server.HtmlEncode(Request.Cookies("udrUserDocReceivedTo").Value)
        Else

        End If
        If Not Request.Cookies("udrUserDocSortBy") Is Nothing Then
            dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrUserDocSortBy").Value)
        Else

        End If
        If Not Request.Cookies("udrUserDocSortOption") Is Nothing Then
            dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrUserDocSortOption").Value)
        Else

        End If
    End Sub
    Private Sub LoadReport()
        msg.Text = ""
        Dim lsUsers As String = GetUserList()
        If lsUsers.Trim = "" Then
            Master.ShowMessage("Please select atleast one personnel.")
            Exit Sub
        End If
        'For i As Int16 = 0 To cbUsers.Items.Count - 1 Step 1
        '    If cbUsers.Items(i).Selected Then
        '        If lsUsers = "" Then
        '            lsUsers = "'" & cbUsers.Items(i).Value & "'"
        '        Else
        '            lsUsers = lsUsers & ",'" & cbUsers.Items(i).Value & "'"
        '        End If

        '    End If

        'Next
        SetCookies(lsUsers)
        'DocSession.rpt_ColValue = lsUsers
        'DocSession.rpt_UploadStartDate = uploadsdate.Text
        'DocSession.rpt_UploadEndDate = uploadedate.Text

        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentListPerUser.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If


    End Sub


End Class
