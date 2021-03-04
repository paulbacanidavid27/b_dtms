Public Class DocumentArchivedPerUserCriteria
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
                'LoadActionTaken()
                GetRequestType()
                LoadPreviousCriteria()
                'GetUsers()
            End If

        End If

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

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udapClassification") Is Nothing Then
            dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udapClassification").Value)
        Else

        End If

        'If Not Request.Cookies("udrActionTaken") Is Nothing Then
        '    dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrActionTaken").Value)
        'Else

        'End If

        'If Not Request.Cookies("udrStatus") Is Nothing Then
        '    dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrStatus").Value)
        'Else

        'End If

        If Not Request.Cookies("udapTitle") Is Nothing Then
            tbSubject.Text = Server.HtmlEncode(Request.Cookies("udapTitle").Value)
        Else

        End If

        If Not Request.Cookies("udapUserDocReqType") Is Nothing Then
            dlRequestType.SelectedValue = Server.HtmlEncode(Request.Cookies("udapUserDocReqType").Value)
        Else

        End If

        If Not Request.Cookies("udapArchivedFrom") Is Nothing Then
            uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udapArchivedFrom").Value)
        Else

        End If
        If Not Request.Cookies("udapArchivedTo") Is Nothing Then
            uploadedate.Text = Server.HtmlEncode(Request.Cookies("udapArchivedTo").Value)
        Else

        End If
        If Not Request.Cookies("udapSortBy") Is Nothing Then
            dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udapSortBy").Value)
        Else

        End If
        If Not Request.Cookies("udapSortOption") Is Nothing Then
            dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udapSortOption").Value)
        Else

        End If
    End Sub

    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub
    Private Sub GetRequestType()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocList
        Try
            oType = New DocList
            If Left(DocSession.sOfcCode, 2) = "PS" Then
                oType.pOfficeCode = "PS"
            ElseIf DocSession.sUserRole <> "A" Then
                oType.pOfficeCode = "DBM"
            End If
            ldata = oType.RetrieveRequestType

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "-All-"
            ldata.Rows.InsertAt(lrow, 0)
            dlRequestType.DataSource = ldata
            dlRequestType.DataValueField = "RequestType"
            dlRequestType.DataTextField = "RequestDescription"
            dlRequestType.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    'Private Sub LoadActionTaken()
    '    Dim oDoc As New DocTypes

    '    'ldata = oDoc.GetSelectedDocStatus
    '    Using ldata2 As DataTable = oDoc.GetDocStatus
    '        Dim lrow As DataRow
    '        If ldata2.Rows.Count > 0 Then
    '            lrow = ldata2.NewRow
    '            lrow("statusid") = "0"
    '            lrow("description") = "-All-"
    '            ldata2.Rows.InsertAt(lrow, 0)
    '        End If

    '        dlStatus.DataTextField = "description"
    '        dlStatus.DataValueField = "statusid"
    '        dlStatus.DataSource = ldata2

    '        dlStatus.DataBind()

    '    End Using
    'End Sub
    Private Sub LoadReport()
        msg.Text = ""
        'Dim lsUsers As String = ""

        'msg.Text = ""
        Dim lsUsers As String = GetUserList()
        If lsUsers.Trim = "" Then
            Master.ShowMessage("Please select atleast one personnel.")
            Exit Sub
        End If



        Dim mycookie As HttpCookie = New HttpCookie("udapArchivedFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapUserDocUserList")
        mycookie.Value = lsUsers
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapArchivedTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapClassificationDesc")
        mycookie.Value = dlClassification.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapTitle")
        mycookie.Value = tbSubject.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapUserDocReqType")
        mycookie.Value = dlRequestType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapUserDocReqTypeDesc")
        mycookie.Value = dlRequestType.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        mycookie = New HttpCookie("udapSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udapSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentArchivedPerUserReport.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes').focus()</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If


    End Sub


End Class
