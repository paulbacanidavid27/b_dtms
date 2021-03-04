Public Class DocumentStatusCriteria
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Reports")
    End Sub
    Private Sub GetDocType()
        Dim oDoc As New DocTypes
        'oDoc.pGroupId = DocSession.sUserGroup
        Using ldata As DataTable = oDoc.GetDocType2()

            If ldata.Rows.Count > 0 Then
                lbDocType.DataTextField = "DocName"
                lbDocType.DataValueField = "DocType"
                Dim lrow As DataRow = ldata.NewRow
                lrow("DocType") = ""
                lrow("DocName") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
                lbDocType.DataSource = ldata
                lbDocType.DataBind()
            Else
                If DocSession.sUserRole <> "R" Then
                    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                End If
                'ReportViewer1.Visible = False
            End If
        End Using
    End Sub
    Private Sub GetDocStatus()
        Dim oDoc As New DocTypes
        Using ldata2 As DataTable = oDoc.GetDocStatus
            Dim lrow As DataRow
            If ldata2.Rows.Count > 0 Then
                lrow = ldata2.NewRow
                lrow("statusid") = "0"
                lrow("description") = "-All-"
                ldata2.Rows.InsertAt(lrow, 0)
            End If

            dlStatus.DataTextField = "description"
            dlStatus.DataValueField = "statusid"
            dlStatus.DataSource = ldata2

            dlStatus.DataBind()

        End Using
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sReportAccess <> "1" Then
            Master.ShowMessage("You don't have access to this report. Please contact the administrator.")
            'ReportViewer1.Visible = False
            btSearch.Visible = False
        ElseIf DocSession.sUserRole = "U" OrElse DocSession.sUserRole = "L" Then
            Response.Redirect("DocumentReceivedCriteria.aspx")
        Else
            If Not IsPostBack Then


                GetDocType()

                GetDocStatus()

                GetOfficeCode()
                GetRequestType()

                LoadPreviousCriteria()
            End If

        End If

    End Sub

    Private Sub GetOfficeCode()

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
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub
    Private Function ValidateReceivedDate() As Boolean

        uploadsdate.Text = uploadsdate.Text.Trim
        If (uploadsdate.Text = "") Then
            Master.ShowMessage("Received Start Date is required.")
            uploadsdate.Focus()
            Return False
        End If
        If (uploadedate.Text = "") Then
            Master.ShowMessage("Received End Date is required.")
            uploadedate.Focus()
            Return False
        End If
        If (uploadsdate.Text <> "" AndAlso Not IsDate(uploadsdate.Text)) Then
            Master.ShowMessage("Please provide a valid Received Start Date.")
            Return False
        End If
        If (uploadedate.Text <> "" AndAlso Not IsDate(uploadedate.Text)) Then
            Master.ShowMessage("Please provide a valid Received End Date.")
            Return False
        End If
        If (CDate(uploadsdate.Text) > CDate(uploadedate.Text)) Then
            Master.ShowMessage("Received End date should not be prior to Received Start Date.")
            Return False
        End If
        Return True
    End Function
    Private Sub LoadReport()
        Try


            If Not ValidateReceivedDate() Then
                Exit Sub
            End If
            SetCookies()

            If cbSummary.Checked AndAlso Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRptSummary") Then
                Dim lsScript2 As String = "<script type='text/javascript'>window.open('DocumentStatusSummary.aspx', 'ViewRptSummary' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRptSummary", lsScript2, False)
            Else
                If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "StatRpt") Then
                    Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentStatus.aspx', 'ViewDocStatRpt' ,'location=no,toolbar=no,menubar=yes,status=yes,height=650, width=1000,left=20, top=20, resizable=yes, scrollbars=yes')</script>"

                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "StatRpt", lsScript, False)

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("udrDocStatDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatReqType")
        mycookie.Value = dlRequestType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatSubject")
        mycookie.Value = tbSubject.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        If DocSession.sUserRole = "A" Then
            'DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
            'DocSession.rpt_OfficeDesc = dlOfficeCode.SelectedItem.Text
            mycookie = New HttpCookie("udrDocStatOffice")
            mycookie.Value = dlOfficeCode.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocStatOfficeDesc")
            mycookie.Value = dlOfficeCode.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
            'DocSession.rpt_OfficeCode = DocSession.sOfcCode
            'DocSession.rpt_OfficeDesc = lOfficeName.Text
            mycookie = New HttpCookie("udrDocStatOffice")
            mycookie.Value = DocSession.sOfcCode
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocStatOfficeDesc")
            mycookie.Value = lOfficeName.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrDocStatUploadedBy")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatActionTaken")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatActionTakenDesc")
        mycookie.Value = dlStatus.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatStatus")
        mycookie.Value = dlDocStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatRecvFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatDocNo")
        mycookie.Value = tbDocNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatRecvTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatPersonnel")
        mycookie.Value = tbPersonnel.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatShow")
        mycookie.Value = dlShow.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocStatSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrDocStatDocType") Is Nothing Then
            If Request.Cookies("udrDocStatDocType").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatDocType").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrDocStatReqType") Is Nothing Then
            If Request.Cookies("udrDocStatReqType").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlRequestType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatReqType").Value))
                If Not IsNothing(li) Then
                    dlRequestType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatReqType").Value)
                End If
            End If
        End If
        If DocSession.sUserRole = "A" Then
            If Not Request.Cookies("udrDocStatOffice") Is Nothing Then
                If Request.Cookies("udrDocStatOffice").Value <> "" Then
                    Dim li As New WebControls.ListItem
                    li = dlOfficeCode.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatOffice").Value))
                    If Not IsNothing(li) Then
                        dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatOffice").Value)
                    End If
                End If
            End If

        End If

        If Not Request.Cookies("udrDocStatUploadedBy") Is Nothing Then
            If Request.Cookies("udrDocStatUploadedBy").Value <> "" Then
                tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrDocStatUploadedBy").Value)
            End If
        End If
        If Not Request.Cookies("udrDocStatDocNo") Is Nothing Then
            If Request.Cookies("udrDocStatDocNo").Value <> "" Then
                tbDocNo.Text = Server.HtmlEncode(Request.Cookies("udrDocStatDocNo").Value)
            End If
        End If
        If Not Request.Cookies("udrDocStatActionTaken") Is Nothing Then

            If Request.Cookies("udrDocStatActionTaken").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlStatus.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatActionTaken").Value))
                If Not IsNothing(li) Then
                    dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatActionTaken").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrDocStatStatus") Is Nothing Then
            If Request.Cookies("udrDocStatStatus").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlDocStatus.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatStatus").Value))
                If Not IsNothing(li) Then
                    dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatStatus").Value)
                End If

            End If
        End If
        If Not Request.Cookies("udrDocStatRecvFrom") Is Nothing Then
            If Request.Cookies("udrDocStatRecvFrom").Value <> "" Then
                uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udrDocStatRecvFrom").Value)
            Else
                uploadsdate.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            uploadsdate.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrDocStatRecvTo") Is Nothing Then
            If Request.Cookies("udrDocStatRecvTo").Value <> "" Then
                uploadedate.Text = Server.HtmlEncode(Request.Cookies("udrDocStatRecvTo").Value)
            Else
                uploadedate.Text = Date.Now.ToShortDateString
            End If
        Else
            uploadedate.Text = Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrDocStatPersonnel") Is Nothing Then
            If Request.Cookies("udrDocStatPersonnel").Value <> "" Then
                tbPersonnel.Text = Server.HtmlEncode(Request.Cookies("udrDocStatPersonnel").Value)
            End If
        End If
        'If Not Request.Cookies("udrDocStatShow") Is Nothing Then
        '    If Request.Cookies("udrDocStatShow").Value <> "" Then
        '        dlShow.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatShow").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrDocStatClassification") Is Nothing Then
            If Request.Cookies("udrDocStatClassification").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlClassification.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatClassification").Value))
                If Not IsNothing(li) Then
                    dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatClassification").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrDocStatSortBy") Is Nothing Then
            If Request.Cookies("udrDocStatSortBy").Value <> "" Then
                dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatSortBy").Value)
                Dim li As New WebControls.ListItem
                li = dlColumns.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatSortBy").Value))
                If Not IsNothing(li) Then
                    dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatSortBy").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrDocStatSortOption") Is Nothing Then
            If Request.Cookies("udrDocStatSortOption").Value <> "" Then
                'dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatSortOption").Value)
                Dim li As New WebControls.ListItem
                li = dlSortOption.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocStatSortOption").Value))
                If Not IsNothing(li) Then
                    dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocStatSortOption").Value)
                End If
            End If
        End If
        If Not Request.Cookies("udrDocStatSubject") Is Nothing Then
            If Request.Cookies("udrDocStatSubject").Value <> "" Then
                tbSubject.Text = Server.HtmlEncode(Request.Cookies("udrDocStatSubject").Value)
            End If
        End If

    End Sub
End Class
