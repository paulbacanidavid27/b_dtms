Public Class GroupStatusCriteria
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
                        If DocSession.sUserRole <> "R" Then
                            Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        End If

                    End If
                End Using
                'ldata = oDoc.GetSelectedDocStatus
                Using ldata2 As DataTable = oDoc.GetDocStatus
                    Dim lrow As DataRow
                    If ldata2.Rows.Count > 0 Then
                        lrow = ldata2.NewRow
                        lrow("statusid") = "0"
                        lrow("description") = "All"
                        ldata2.Rows.InsertAt(lrow, 0)
                    End If

                    dlStatus.DataTextField = "description"
                    dlStatus.DataValueField = "statusid"
                    dlStatus.DataSource = ldata2

                    dlStatus.DataBind()
                End Using
                GetOfficeCode()
                GetUserGroups()

                LoadPreviousCriteria()
            End If

        End If

    End Sub


    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        tbADRFrom.Text = tbADRFrom.Text.Trim
        'If (tbDCFrom.Text = "" OrElse tbDCTo.Text = "") Then
        '    msg.Text = "Assigned date range is required."
        '    Exit Sub
        'End If
        If (tbADRFrom.Text <> "" AndAlso Not IsDate(tbADRFrom.Text)) OrElse (tbADRTo.Text <> "" AndAlso Not IsDate(tbADRTo.Text)) Then
            msg.Text = "Please provide a valid Assigned date."
            Exit Sub
        End If

        tbRDRFrom.Text = tbRDRFrom.Text.Trim
        'If (tbDCFrom.Text = "" OrElse tbDCTo.Text = "") Then
        '    msg.Text = "Assigned date range is required."
        '    Exit Sub
        'End If
        If (tbRDRFrom.Text <> "" AndAlso Not IsDate(tbRDRFrom.Text)) OrElse (tbRDRTo.Text <> "" AndAlso Not IsDate(tbRDRTo.Text)) Then
            msg.Text = "Please provide a valid Received date."
            Exit Sub
        End If

        

        SetCookies()
        If cbSummary.Checked AndAlso Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRptSummary") Then
            Dim lsScript2 As String = "<script type='text/javascript'>window.open('GroupStatusSummary.aspx', 'RptViewerSummary' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRptSummary", lsScript2, False)
        Else
            If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
                Dim lsScript As String = "<script type='text/javascript'>window.open('GroupStatus.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
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
                oType.pOfficeCode = dlOfficeCode.SelectedValue
                ldata = oType.RetrieveGroups

            ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
                oType.pOfficeCode = DocSession.sOfcCode
                ldata = oType.RetrieveGroups

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

    Private Sub GetOfficeCode()
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
                ldata = oType.RetrieveOffice

                dlOfficeCode.DataSource = ldata
                dlOfficeCode.DataValueField = "OfficeCode"
                dlOfficeCode.DataTextField = "Description"
                dlOfficeCode.DataBind()
                lOfficeName.Visible = False
            ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "R" OrElse DocSession.sUserRole = "G" Then
                oType.pOfficeCode = DocSession.sOfcCode
                ldata = oType.RetrieveOffice
                If ldata.Rows.Count > 0 Then

                    dlOfficeCode.Visible = False
                    lOfficeName.Text = ldata(0)("Description")
                    lOfficeName.Visible = True

                End If
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("udrGrpStatDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        If DocSession.sUserRole = "A" Then
            mycookie = New HttpCookie("udrGrpStatGroupId")
            mycookie.Value = dlGroup.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatGroupDesc")
            mycookie.Value = dlGroup.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatOffice")
            mycookie.Value = dlOfficeCode.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatOfficeDesc")
            mycookie.Value = dlOfficeCode.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
            'DocSession.rpt_OfficeCode = DocSession.sOfcCode
            'DocSession.rpt_OfficeDesc = lOfficeName.Text
            mycookie = New HttpCookie("udrGrpStatGroupId")
            mycookie.Value = dlGroup.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatGroupDesc")
            mycookie.Value = dlGroup.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatOffice")
            mycookie.Value = DocSession.sOfcCode
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrGrpStatOfficeDesc")
            mycookie.Value = lOfficeName.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrGrpStatUploadedBy")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatActionTaken")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatActionTakenDesc")
        mycookie.Value = dlStatus.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatStatus")
        mycookie.Value = dlDocStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRecvFrom")
        mycookie.Value = tbRDRFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRecvTo")
        mycookie.Value = tbRDRTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatAssgnFrom")
        mycookie.Value = tbADRFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatAssgnTo")
        mycookie.Value = tbADRTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatPersonnel")
        mycookie.Value = tbPersonnel.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatRemarks")
        mycookie.Value = tbRemarks.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatDocNo")
        mycookie.Value = tbDocNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrGrpStatSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrGrpStatDocType") Is Nothing Then
            If Request.Cookies("udrGrpStatDocType").Value <> "" Then
                lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatDocType").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatGroupId") Is Nothing Then
            If Request.Cookies("udrGrpStatGroupId").Value <> "" Then
                dlGroup.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatGroupId").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatDocNo") Is Nothing Then
            If Request.Cookies("udrGrpStatDocNo").Value <> "" Then
                tbDocNo.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatDocNo").Value)
            End If
        End If
        'If Not Request.Cookies("udrGrpStatOffice") Is Nothing Then
        '    If Request.Cookies("udrGrpStatOffice").Value <> "" Then
        '        dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatOffice").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrGrpStatUploadedBy") Is Nothing Then
            If Request.Cookies("udrGrpStatUploadedBy").Value <> "" Then
                tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatUploadedBy").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatActionTaken") Is Nothing Then

            If Request.Cookies("udrGrpStatActionTaken").Value <> "" Then
                dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatActionTaken").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatStatus") Is Nothing Then
            If Request.Cookies("udrGrpStatStatus").Value <> "" Then
                dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatStatus").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatRecvFrom") Is Nothing Then
            If Request.Cookies("udrGrpStatRecvFrom").Value <> "" Then
                tbRDRFrom.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRecvFrom").Value)
            Else
                tbRDRFrom.Text = "" 'DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbRDRFrom.Text = "" 'DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRecvTo") Is Nothing Then
            If Request.Cookies("udrGrpStatRecvTo").Value <> "" Then
                tbRDRTo.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRecvTo").Value)
            Else
                tbRDRTo.Text = "" 'Date.Now.ToShortDateString
            End If
        Else
            tbRDRTo.Text = "" 'Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatAssgnFrom") Is Nothing Then
            If Request.Cookies("udrGrpStatAssgnFrom").Value <> "" Then
                tbADRFrom.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatAssgnFrom").Value)
            Else
                tbADRFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbADRFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatAssgnTo") Is Nothing Then
            If Request.Cookies("udrGrpStatAssgnTo").Value <> "" Then
                tbADRTo.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatAssgnTo").Value)
            Else
                tbADRTo.Text = Date.Now.ToShortDateString
            End If
        Else
            tbADRTo.Text = Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrGrpStatRemarks") Is Nothing Then
            If Request.Cookies("udrGrpStatRemarks").Value <> "" Then
                tbRemarks.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatRemarks").Value)
            End If
        End If

        If Not Request.Cookies("udrGrpStatPersonnel") Is Nothing Then
            If Request.Cookies("udrGrpStatPersonnel").Value <> "" Then
                tbPersonnel.Text = Server.HtmlEncode(Request.Cookies("udrGrpStatPersonnel").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatSortBy") Is Nothing Then
            If Request.Cookies("udrGrpStatSortBy").Value <> "" Then
                dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatSortBy").Value)
            End If
        End If
        If Not Request.Cookies("udrGrpStatSortOption") Is Nothing Then
            If Request.Cookies("udrGrpStatSortOption").Value <> "" Then
                dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrGrpStatSortOption").Value)
            End If
        End If


    End Sub
    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
    End Sub

    Private Sub dlOfficeCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlOfficeCode.SelectedIndexChanged
        GetUserGroups()
        pnlGroup.Update()
    End Sub
End Class
