Public Class DeletedDocumentCriteria
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
                        'GetOfficeCode()
                    Else
                        'If DocSession.sUserRole <> "R" Then
                        '    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        'End If
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

    
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click


        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) Or (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            msg.Text = "Please provide a valid deleted date."
            Exit Sub
        End If
        DocSession.rpt_DocType = lbDocType.SelectedValue
        DocSession.rpt_StartDate = tbDCFrom.Text
        DocSession.rpt_EndDate = tbDCTo.Text
        DocSession.rpt_ColValue = tbTitle.Text

        If DocSession.sUserRole = "A" Then
            DocSession.rpt_OfficeCode = tbRemarks.Text
            DocSession.rpt_OfficeDesc = tbRemarks.Text
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
            DocSession.rpt_OfficeCode = tbRemarks.Text
            DocSession.rpt_OfficeDesc = tbRemarks.Text
        End If

        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DeletedDocuments.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
    End Sub

    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
    End Sub
    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("docdelRefno") Is Nothing Then
            tbRefNo.Text = Server.HtmlEncode(Request.Cookies("docdelRefno").Value)
        Else

        End If

        If Not Request.Cookies("docdelRemarks") Is Nothing Then
            tbRemarks.Text = Server.HtmlEncode(Request.Cookies("docdelRemarks").Value)
        Else

        End If
        If Not Request.Cookies("docdelDocType") Is Nothing Then

            If Request.Cookies("docdelDocType").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("docdelDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("docdelDocType").Value)
                End If

            End If
        End If
        If Not Request.Cookies("docdelTitle") Is Nothing Then
            tbTitle.Text = Server.HtmlEncode(Request.Cookies("docdelTitle").Value)

        End If
        If Not Request.Cookies("docdelDelDateStart") Is Nothing Then
            tbDCFrom.Text = Server.HtmlEncode(Request.Cookies("docdelDelDateStart").Value)
        End If
        If Not Request.Cookies("docdelDelDateEnd") Is Nothing Then
            tbDCTo.Text = Server.HtmlEncode(Request.Cookies("docdelDelDateEnd").Value)
        End If
    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("docdelRefNo")
        mycookie.Value = tbRefNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docdelRemarks")
        mycookie.Value = tbRemarks.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docdelDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docdelTitle")
        mycookie.Value = tbTitle.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docdelDelDateStart")
        mycookie.Value = tbDCFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docdelDelDateEnd")
        mycookie.Value = tbDCTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
End Class
