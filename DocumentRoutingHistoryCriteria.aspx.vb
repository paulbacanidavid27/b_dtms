Public Class DocumentRoutingHistoryCriteria
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
                idAdvSrch.Visible = True
                'pnlImg.Visible = False
                Dim oDoc As New DocTypes
                oDoc.pGroupId = DocSession.sUserGroup
                Using ldata As DataTable = oDoc.GetDocType()
                    Dim lrow As DataRow

                    'dlDocType.DataTextField = "DocName"
                    'dlDocType.DataValueField = "DocType"

                    If ldata.Rows.Count > 0 Then
                        lbDocType.DataTextField = "DocName"
                        lbDocType.DataValueField = "DocType"
                        lrow = ldata.NewRow
                        lrow("DocType") = ""
                        lrow("DocName") = "All"
                        ldata.Rows.InsertAt(lrow, 0)
                        lbDocType.DataSource = ldata
                        lbDocType.DataBind()
                    Else
                        'If DocSession.sUserRole <> "R" Then
                        '    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        'End If

                    End If
                End Using
                'ldata = oDoc.GetSelectedDocStatus

                GetUserGroups()

                LoadPreviousCriteria()
                GetDocStatus()
            End If

        End If

    End Sub

    Private Sub GetUserGroups()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            If DocSession.sUserRole = "A" Then
                ldata = oType.RetrieveGroups
                'If ldata.Rows.Count > 0 Then
                '    lrow = ldata.NewRow
                '    lrow("GroupId") = ""
                '    lrow("GroupName") = "-All-"
                '    ldata.Rows.InsertAt(lrow, 0)

                'End If
                'dlGroup.DataSource = ldata
                'dlGroup.DataValueField = "GroupId"
                'dlGroup.DataTextField = "GroupName"
                'dlGroup.DataBind()
                'lGroupName.Visible = False
            ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
                oType.pOfficeCode = DocSession.sOfcCode
                ldata = oType.RetrieveGroups
                'If ldata.Rows.Count > 0 Then

                '    dlGroup.Visible = True
                '    lGroupName.Text = ldata(0)("GroupName")
                '    lGroupName.Visible = True

                'End If
            End If
            dlGroup.DataSource = ldata
            dlGroup.DataValueField = "GroupId"
            dlGroup.DataTextField = "GroupName"
            dlGroup.DataBind()
            lGroupName.Visible = False


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        tbADRFrom.Text = tbADRFrom.Text.Trim

        If (tbADRFrom.Text <> "" AndAlso Not IsDate(tbADRFrom.Text)) OrElse (tbADRTo.Text <> "" AndAlso Not IsDate(tbADRTo.Text)) Then
            msg.Text = "Please provide a valid Assigned date."
            Exit Sub
        End If

        tbRDRFrom.Text = tbRDRFrom.Text.Trim

        If (tbRDRFrom.Text <> "" AndAlso Not IsDate(tbRDRFrom.Text)) OrElse (tbRDRTo.Text <> "" AndAlso Not IsDate(tbRDRTo.Text)) Then
            msg.Text = "Please provide a valid Received date."
            Exit Sub
        End If


        SetCookies()
        'If cbSummary.Checked AndAlso Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRptSummary") Then
        '    Dim lsScript2 As String = "<script type='text/javascript'>window.open('GroupStatusSummary.aspx', 'RptViewerSummary' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRptSummary", lsScript2, False)
        'Else
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "RptRoutingViewer") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentRoutingHistory.aspx', 'RptRoutingViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "RptRoutingViewer", lsScript, False)
        End If
        'End If

    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("udrGrpStatRtHistDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistRefNo")
        mycookie.Value = tbRefNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrGrpStatRtHistSubject")
        'mycookie.Value = tbSubject.Text
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        If DocSession.sUserRole = "A" Then
            'DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
            'DocSession.rpt_OfficeDesc = dlOfficeCode.SelectedItem.Text
            mycookie = New HttpCookie("udrGrpStatRtHistGroupId")
            mycookie.Value = dlGroup.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatRtHistGroupDesc")
            mycookie.Value = dlGroup.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
            'DocSession.rpt_OfficeCode = DocSession.sOfcCode
            'DocSession.rpt_OfficeDesc = lOfficeName.Text
            mycookie = New HttpCookie("udrGrpStatRtHistGroupId")
            mycookie.Value = dlGroup.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatRtHistGroupDesc")
            mycookie.Value = dlGroup.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrGrpStatRtHistUploadedBy")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistActionTaken")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistActionTakenDesc")
        mycookie.Value = dlStatus.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistStatus")
        mycookie.Value = dlDocStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistRecvFrom")
        mycookie.Value = tbRDRFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistRecvTo")
        mycookie.Value = tbRDRTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistAssgnFrom")
        mycookie.Value = tbADRFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistAssgnTo")
        mycookie.Value = tbADRTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistPersonnel")
        mycookie.Value = tbPersonnel.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistRemarks")
        mycookie.Value = tbRemarks.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRtHistSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrGrpStatRtHistDocType") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistDocType").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistDocType").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistGroupId") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistGroupId").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlGroup.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistGroupId").Value))
                If Not IsNothing(li) Then
                    dlGroup.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistGroupId").Value)
                End If
            End If
        End If
        'If Not Request.Cookies("udrGrpStatRtHistOffice") Is Nothing Then
        '    If Request.Cookies("udrGrpStatRtHistOffice").Value <> "" Then
        '        dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistOffice").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrGrpStatRtHistUploadedBy") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistUploadedBy").Value <> "" Then
                tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistUploadedBy").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistActionTaken") Is Nothing Then

            If Request.Cookies("udrGrpStatRtHistActionTaken").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlStatus.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistActionTaken").Value))
                If Not IsNothing(li) Then
                    dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistActionTaken").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistStatus") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistStatus").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlDocStatus.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistStatus").Value))
                If Not IsNothing(li) Then
                    dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistStatus").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistRecvFrom") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistRecvFrom").Value <> "" Then
                tbRDRFrom.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistRecvFrom").Value)
            Else
                tbRDRFrom.Text = "" 'DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbRDRFrom.Text = "" 'DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRtHistRecvTo") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistRecvTo").Value <> "" Then
                tbRDRTo.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistRecvTo").Value)
            Else
                tbRDRTo.Text = "" 'Date.Now.ToShortDateString
            End If
        Else
            tbRDRTo.Text = "" 'Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRtHistAssgnFrom") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistAssgnFrom").Value <> "" Then
                tbADRFrom.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistAssgnFrom").Value)
            Else
                tbADRFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbADRFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRtHistAssgnTo") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistAssgnTo").Value <> "" Then
                tbADRTo.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistAssgnTo").Value)
            Else
                tbADRTo.Text = Date.Now.ToShortDateString
            End If
        Else
            tbADRTo.Text = Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRtHistRemarks") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistRemarks").Value <> "" Then
                tbRemarks.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistRemarks").Value)
            End If
        End If

        If Not Request.Cookies("udrGrpStatRtHistPersonnel") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistPersonnel").Value <> "" Then
                tbPersonnel.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistPersonnel").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistSortBy") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistSortBy").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlColumns.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistSortBy").Value))
                If Not IsNothing(li) Then
                    dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistSortBy").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrGrpStatRtHistSortOption") Is Nothing Then
            If Request.Cookies("udrGrpStatRtHistSortOption").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlSortOption.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistSortOption").Value))
                If Not IsNothing(li) Then
                    dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatRtHistSortOption").Value)
                End If
            End If
        End If


    End Sub
    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocStatus.SelectedIndexChanged
        GetDocStatus()
    End Sub

    Private Sub GetDocStatus()

        Dim oDoc As New DocTypes
        Dim lsParam As String = ""
        If dlDocStatus.SelectedValue = "C" Then
            lsParam = "AND StatusId IN (" & DocSession.CompleteStatus & ")"
        ElseIf dlDocStatus.SelectedValue = "O" Then
            lsParam = "AND StatusId NOT IN (" & DocSession.CompleteStatus & ")"
        End If

        Using ldata As DataTable = oDoc.GetDocStatus(lsParam)
            Dim lrow As DataRow
            If ldata.Rows.Count > 0 Then
                lrow = ldata.NewRow
                lrow("statusid") = "0"
                lrow("description") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
            End If

            dlStatus.DataTextField = "description"
            dlStatus.DataValueField = "statusid"
            dlStatus.DataSource = ldata

            dlStatus.DataBind()
        End Using

    End Sub
End Class
