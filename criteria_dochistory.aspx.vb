Public Class criteria_dochistory
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
                Dim oDoc As DocTypes
                Dim ldata As DataTable
                Dim lrow As DataRow
                Try


                    idAdvSrch.Visible = True
                    'pnlImg.Visible = False
                    oDoc = New DocTypes
                    oDoc.pGroupId = DocSession.sUserGroup
                    ldata = oDoc.GetDocType()

                    If ldata.Rows.Count > 0 Then

                        lrow = ldata.NewRow()
                        lrow("DocType") = ""
                        lrow("DocName") = "-All-"
                        ldata.Rows.InsertAt(lrow, 0)

                        lbDocType.DataTextField = "DocName"
                        lbDocType.DataValueField = "DocType"
                        lbDocType.DataSource = ldata
                        lbDocType.DataBind()
                        GetOfficeCode()
                        LoadPreviousCriteria()
                    Else
                        If DocSession.sUserRole <> "R" Then
                            Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        End If
                        'ReportViewer1.Visible = False
                    End If

                Catch ex As Exception
                    Master.ShowMessage("An error occurred while displaying criteria (" & ex.Message & "). Please try again.")
                Finally
                    If Not ldata Is Nothing Then
                        ldata.Dispose()
                        ldata = Nothing

                    End If
                End Try
            End If

        End If


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
                If ldata.Rows.Count > 0 Then
                    lrow = ldata.NewRow
                    lrow("OfficeCode") = ""
                    lrow("Description") = "-All-"
                    ldata.Rows.InsertAt(lrow, 0)

                End If
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
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click
       

        LoadReport()

    End Sub

    Private Sub LoadReport()
       msg.Text = ""
        If Not ValidateChangesDate() Then
            Exit Sub
        End If
        If Not ValidateUploadDate() Then
            Exit Sub
        End If
        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewChangesRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentChanges.aspx', 'RptChangesViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewChangesRpt", lsScript, False)
        End If
        
    End Sub

    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("udrDocChangesDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrDocChangesReqType")
        'mycookie.Value = dlRequestType.SelectedValue
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesSubject")
        mycookie.Value = tbTitle.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        If DocSession.sUserRole = "A" Then
            'DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
            'DocSession.rpt_OfficeDesc = dlOfficeCode.SelectedItem.Text
            mycookie = New HttpCookie("udrDocChangesOffice")
            mycookie.Value = dlOfficeCode.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocChangesOfficeDesc")
            mycookie.Value = dlOfficeCode.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "R" OrElse DocSession.sUserRole = "G" Then
            'DocSession.rpt_OfficeCode = DocSession.sOfcCode
            'DocSession.rpt_OfficeDesc = lOfficeName.Text
            mycookie = New HttpCookie("udrDocChangesOffice")
            mycookie.Value = DocSession.sOfcCode
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocChangesOfficeDesc")
            mycookie.Value = lOfficeName.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrDocChangesUploadedBy")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrDocChangesActionTaken")
        'mycookie.Value = dlStatus.SelectedValue
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrDocChangesActionTakenDesc")
        'mycookie.Value = dlStatus.SelectedItem.Text
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrDocChangesStatus")
        'mycookie.Value = dlDocStatus.SelectedValue
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesChangesFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesChangesTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesUploadFrom")
        mycookie.Value = tbCDFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesUploadTo")
        mycookie.Value = tbCDTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesDocNo")
        mycookie.Value = tbDocNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        
        'mycookie = New HttpCookie("udrDocChangesPersonnel")
        'mycookie.Value = tbPersonnel.Text
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesShow")
        mycookie.Value = dlShow.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'mycookie = New HttpCookie("udrDocChangesClassification")
        'mycookie.Value = dlClassification.SelectedValue
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocChangesSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub
    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrDocChangesDocType") Is Nothing Then
            If Request.Cookies("udrDocChangesDocType").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocChangesDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesDocType").Value)
                End If
                'lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesDocType").Value)
            End If
        End If

        If DocSession.sUserRole = "A" Then
            If Not Request.Cookies("udrDocChangesOffice") Is Nothing Then
                If Request.Cookies("udrDocChangesOffice").Value <> "" Then
                    Dim li As New WebControls.ListItem
                    li = dlOfficeCode.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocChangesOffice").Value))
                    If Not IsNothing(li) Then
                        dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesOffice").Value)
                    End If
                    'dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesOffice").Value)
                End If
            End If

        End If

        If Not Request.Cookies("udrDocChangesUploadedBy") Is Nothing Then
            If Request.Cookies("udrDocChangesUploadedBy").Value <> "" Then
                tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesUploadedBy").Value)
            End If
        End If

        If Not Request.Cookies("udrDocChangesDocNo") Is Nothing Then
            If Request.Cookies("udrDocChangesDocNo").Value <> "" Then
                tbDocNo.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesDocNo").Value)
            End If
        End If
        'If Not Request.Cookies("udrDocChangesActionTaken") Is Nothing Then

        '    If Request.Cookies("udrDocChangesActionTaken").Value <> "" Then
        '        dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesActionTaken").Value)
        '    End If
        'End If
        'If Not Request.Cookies("udrDocChangesStatus") Is Nothing Then
        '    If Request.Cookies("udrDocChangesStatus").Value <> "" Then
        '        dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesStatus").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrDocChangesChangesFrom") Is Nothing Then
            If Request.Cookies("udrDocChangesChangesFrom").Value <> "" Then
                uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesChangesFrom").Value)
            Else
                uploadsdate.Text = ""
            End If
        Else
            uploadsdate.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If

        If Not Request.Cookies("udrDocChangesChangesTo") Is Nothing Then
            If Request.Cookies("udrDocChangesChangesTo").Value <> "" Then
                uploadedate.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesChangesTo").Value)
            Else
                uploadedate.Text = "" 'Date.Now.ToShortDateString
            End If
        Else
            uploadedate.Text = Date.Now.ToShortDateString
        End If
        If Not Request.Cookies("udrDocChangesUploadFrom") Is Nothing Then
            If Request.Cookies("udrDocChangesUploadFrom").Value <> "" Then
                tbCDFrom.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesUploadFrom").Value)
            Else
                tbCDFrom.Text = ""
            End If
        Else
            tbCDFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If

        If Not Request.Cookies("udrDocChangesUploadTo") Is Nothing Then
            If Request.Cookies("udrDocChangesUploadTo").Value <> "" Then
                tbCDTo.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesUploadTo").Value)
            Else
                tbCDTo.Text = "" 'Date.Now.ToShortDateString
            End If
        Else
            tbCDTo.Text = Date.Now.ToShortDateString
        End If
        'If Not Request.Cookies("udrDocChangesPersonnel") Is Nothing Then
        '    If Request.Cookies("udrDocChangesPersonnel").Value <> "" Then
        '        tbPersonnel.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesPersonnel").Value)
        '    End If
        'End If
        'If Not Request.Cookies("udrDocChangesShow") Is Nothing Then
        '    If Request.Cookies("udrDocChangesShow").Value <> "" Then
        '        dlShow.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesShow").Value)
        '    End If
        'End If
        'If Not Request.Cookies("udrDocChangesClassification") Is Nothing Then
        '    If Request.Cookies("udrDocChangesClassification").Value <> "" Then
        '        dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesClassification").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrDocChangesSortBy") Is Nothing Then
            If Request.Cookies("udrDocChangesSortBy").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlSortOption.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocChangesSortBy").Value))
                If Not IsNothing(li) Then
                    dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesSortBy").Value)
                End If
                'dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesSortBy").Value)
            End If
        End If
        If Not Request.Cookies("udrDocChangesSortOption") Is Nothing Then
            If Request.Cookies("udrDocChangesSortOption").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlSortOption.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocChangesSortOption").Value))
                If Not IsNothing(li) Then
                    dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesSortOption").Value)
                End If
                'dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocChangesSortOption").Value)
            End If
        End If
        If Not Request.Cookies("udrDocChangesSubject") Is Nothing Then
            If Request.Cookies("udrDocChangesSubject").Value <> "" Then
                tbTitle.Text = Server.HtmlEncode(Request.Cookies("udrDocChangesSubject").Value)
            End If
        End If

    End Sub
    Private Function ValidateUploadDate() As Boolean

        tbCDFrom.Text = tbCDFrom.Text.Trim
        If (tbCDFrom.Text = "" OrElse tbCDTo.Text = "") Then
            msg.Text = "Upload date range is required."
            Return False
        End If
        If (tbCDFrom.Text <> "" AndAlso Not IsDate(tbCDFrom.Text)) OrElse (tbCDTo.Text <> "" AndAlso Not IsDate(tbCDTo.Text)) Then
            msg.Text = "Please provide a valid Upload date."
            Return False
        End If
        If (CDate(tbCDFrom.Text) > CDate(tbCDTo.Text)) Then
            msg.Text = "Upload End date should not be prior to Upload Start Date."
            Return False
        End If
        Return True
    End Function
    Private Function ValidateChangesDate() As Boolean

        uploadsdate.Text = uploadsdate.Text.Trim
        If (uploadsdate.Text = "" OrElse uploadedate.Text = "") Then
            msg.Text = "Changes date range is required."
            Return False
        End If
        If (uploadsdate.Text <> "" AndAlso Not IsDate(uploadsdate.Text)) OrElse (uploadedate.Text <> "" AndAlso Not IsDate(uploadedate.Text)) Then
            msg.Text = "Please provide a valid Changes date."
            Return False
        End If
        If (CDate(uploadedate.Text) > CDate(uploadedate.Text)) Then
            msg.Text = "Changes End date should not be prior to Changes Start Date."
            Return False
        End If
        Return True
    End Function
End Class
