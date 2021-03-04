Imports System.Globalization
Public Class DocumentUploadedHourlyCriteria
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Reports")
        imgUpload.Visible = True
        imgDownload.Visible = True
        If DocSession.sOfcCode = "CRD" Then
            
            btGraph.Visible = True
            btMonthGraph.Visible = True
            imgIcon.Visible = True

        Else
            'imgUpload.Visible = False
            'imgDownload.Visible = False
            imgIcon.Visible = False
            btGraph.Visible = False
            btMonthGraph.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sReportAccess <> "1" Then
            Master.ShowMessage("You don't have access to this report. Please contact the administrator.")

            btSearch.Visible = False
        Else
            If Not IsPostBack Then
                tbDate.Text = Date.Now.ToString("MM/dd/yyyy")
                tbTDate.Text = Date.Now.ToString("MM/dd/yyyy")
                GetOfficeCode()
                LoadPreviousCriteria()
            End If
        End If

    End Sub


    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        tbDate.Text = tbDate.Text.Trim
        tbTDate.Text = tbTDate.Text.Trim
        If (tbDate.Text <> "" AndAlso Not IsDate(tbDate.Text)) Then
            msg.Text = "Please provide a valid upload From date."
            Exit Sub
        End If
        If (tbTDate.Text <> "" AndAlso Not IsDate(tbTDate.Text)) Then
            msg.Text = "Please provide a valid upload To date."
            Exit Sub
        End If

        'DocSession.rpt_StartDate = tbDate.Text.Trim & " " & txtStart.Text.Trim
        'DocSession.rpt_EndDate = tbTDate.Text.Trim & " " & txtEnd.Text.Trim
        'DocSession.rpt_ColValue = tbAuthor.Text

        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentUploadedHourly.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
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
            ElseIf DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "R" OrElse DocSession.sUserRole = "L" Then
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
    Private Sub LoadGraph()
        msg.Text = ""
        tbDate.Text = tbDate.Text.Trim
        If (tbDate.Text <> "" AndAlso Not IsDate(tbDate.Text)) Then
            msg.Text = "Please provide a valid upload From date."
            tbDate.Focus()
            Exit Sub
        End If
        tbTDate.Text = tbTDate.Text.Trim
        If (tbTDate.Text <> "" AndAlso Not IsDate(tbTDate.Text)) Then
            msg.Text = "Please provide a valid upload To date."
            tbTDate.Focus()
            Exit Sub
        End If

        'DocSession.rpt_StartDate = tbDate.Text.Trim & " " & txtStart.Text.Trim
        'DocSession.rpt_EndDate = tbTDate.Text.Trim & " " & txtEnd.Text.Trim
        'DocSession.rpt_ColValue = tbAuthor.Text

        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRptGraph") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('rptDocUploadedHourlyGraph.aspx', 'RptViewerGraph' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRptGraph", lsScript, False)
        End If
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
    End Sub


    Private Sub btGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btGraph.Click
        LoadGraph()
    End Sub

    Private Sub imgDownload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDownload.Click
        msg.Text = ""
        tbDate.Text = tbDate.Text.Trim
        If (tbDate.Text <> "" AndAlso Not IsDate(tbDate.Text)) Then
            msg.Text = "Please provide a valid upload From date."
            Exit Sub
        End If
        tbTDate.Text = tbTDate.Text.Trim
        If (tbTDate.Text <> "" AndAlso Not IsDate(tbTDate.Text)) Then
            msg.Text = "Please provide a valid upload To date."
            Exit Sub
        End If
        'DocSession.rpt_DocType = lbDocType.SelectedValue
        DocSession.rpt_StartDate = tbDate.Text.Trim & " " & txtStart.Text.Trim
        DocSession.rpt_EndDate = tbTDate.Text.Trim & " " & txtEnd.Text.Trim
        DocSession.rpt_ColValue = tbAuthor.Text
        DataTable2CSV(RetrieveUploaded, "uploadedhourly.csv", ",")
    End Sub

    Private Function RetrieveUploaded() As DataTable
        Try
            Dim oDoc As DocList
            oDoc = New DocList
            'oDoc.pOfficeCode = dlOfficeCode.SelectedValue
            Return oDoc.RetrieveUploaded()

        Catch ex As Exception

        End Try
    End Function

    Private Sub DataTable2CSV(ByVal table As DataTable, ByVal filename As String, ByVal sepChar As String)
        'Dim writer As System.IO.StreamWriter
        'writer = New System.IO.StreamWriter(filename)
        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)

        Response.ContentType = "text/plain"
        ' first write a line with the columns name
        Dim sep As String = ""

        Dim builder, builderdata As System.Text.StringBuilder
        builder = New System.Text.StringBuilder
        builderdata = New System.Text.StringBuilder
        For Each col As DataColumn In table.Columns
            builderdata.Append(sep).Append(col.ColumnName)
            sep = sepChar
        Next
        builder.AppendLine(builderdata.ToString)
        'writer.WriteLine(builder.ToString())


        ' then write all the rows
        For Each row As DataRow In table.Rows
            sep = ""
            builderdata = New System.Text.StringBuilder

            For Each col As DataColumn In table.Columns
                builderdata.Append(sep).Append(row(col.ColumnName))
                sep = sepChar
            Next
            builder.AppendLine(builderdata.ToString)
        Next
        Response.Write(builder.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Private Sub imgUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpload.Click
        pnlUpload.Visible = Not pnlUpload.Visible
        pView.Visible = False
        pnlView.Update()
    End Sub

    Private Sub btUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpload.Click
        Dim ldata As DataTable
        Try
            Dim oCSV As New ConvertCSV
            oCSV.pComma = ","
            ldata = oCSV.uf_read_csv_dtl(fUpload)
            If ldata.Rows.Count = 0 Then
                Master.ShowMessage("Import file is empty. Please load another file.")
            ElseIf ldata.Columns.Count < 7 Then
                Master.ShowMessage("Import file columns is incomplete. Please the csv file.")
            Else
                Dim lsrefno, rby, rdate, rtime As String
                Dim oDoc As New DocList
                Dim outDate As DateTime
                Dim liCtr As Integer = 0
                Dim lsMsg As String
                For Each drows As DataRow In ldata.Rows
                    lsMsg = ""
                    If ldata.Rows.IndexOf(drows) <> 0 Then
                        lsrefno = drows(0).ToString.Trim()
                        If lsrefno <> "" Then

                            rby = drows(4).ToString.Trim()
                            rdate = drows(5).ToString.Trim()
                            rtime = drows(6).ToString.Trim()
                            oDoc.pRefNo = lsrefno
                            oDoc.pReceivedBy = rby
                            oDoc.pReceivedDate = rdate
                            oDoc.pReceivedTime = rtime
                            oDoc.pUserId = DocSession.sUserId
                            oDoc.pDocId = "-1"
                            oDoc.pCreatedDate = drows(1).ToString.Trim()
                            oDoc.pDocAge = "0"
                            'oDoc.UpdateDocMonitoringReceiving()
                            If rby <> "" AndAlso rdate <> "" AndAlso rtime <> "" Then
                                If DateTime.TryParse(rdate & " " & rtime, outDate) Then

                                    
                                    oDoc.UpdateDocReceivedBy()
                                    
                                    lsMsg = "OK"
                                Else
                                    'Master.ShowMessage("Invalid date time " & rdate & " " & rtime)
                                    'Exit Sub
                                    lsMsg = "Invalid date time " & rdate & " " & rtime
                                End If
                            Else


                                If rby = "" AndAlso rdate = "" AndAlso rtime = "" Then
                                    lsMsg = "<Not Imported>"
                                Else
                                    If rby = "" Then
                                        lsMsg = lsMsg & "<Empty Received By>"

                                    End If
                                    If rdate = "" Then
                                        lsMsg = lsMsg & "<Empty Received Date>"

                                    End If
                                    If rtime = "" Then
                                        lsMsg = lsMsg & "<Empty Received Time>"

                                    End If

                                End If

                            End If

                        End If

                    Else
                        lsMsg = "Import Remarks"
                    End If
                    ldata(liCtr)(7) = lsMsg
                    liCtr = liCtr + 1
                Next
                If lsrefno <> "" Then
                    Master.ShowMessage("File has been imported. Please see below for the results.")
                End If
                If ldata.Rows.Count > 0 Then
                    pView.Visible = True
                    GridView1.DataSource = ldata
                    GridView1.DataBind()
                    pnlView.Update()
                End If
            End If
        Catch ex As Exception
            Master.ShowMessage("An error occurred while updating received by (" & ex.Message & ").")
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Sub btMonthGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMonthGraph.Click
        LoadMonthGraph()
    End Sub
    Private Sub LoadMonthGraph()
        msg.Text = ""
        tbDate.Text = tbDate.Text.Trim
        If (tbDate.Text <> "" AndAlso Not IsDate(tbDate.Text)) Then
            msg.Text = "Please provide a valid upload From date."
            tbDate.Focus()
            Exit Sub
        End If
        tbTDate.Text = tbTDate.Text.Trim
        If (tbTDate.Text <> "" AndAlso Not IsDate(tbTDate.Text)) Then
            msg.Text = "Please provide a valid upload To date."
            tbTDate.Focus()
            Exit Sub
        End If

        'DocSession.rpt_StartDate = tbDate.Text.Trim & " " & txtStart.Text.Trim
        'DocSession.rpt_EndDate = tbTDate.Text.Trim & " " & txtEnd.Text.Trim
        'DocSession.rpt_ColValue = tbAuthor.Text

        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRptMGraph") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('rptDocUploadedMonthlyGraph.aspx', 'RptViewerMGraph' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRptMGraph", lsScript, False)
        End If
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

    End Sub

    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("udrDocUpHrlyAuthor")
        mycookie.Value = tbAuthor.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        If DocSession.sUserRole = "A" Then
            'DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
            'DocSession.rpt_OfficeDesc = dlOfficeCode.SelectedItem.Text
            mycookie = New HttpCookie("udrDocUpHrlyOffice")
            mycookie.Value = dlOfficeCode.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpHrlyOfficeDesc")
            mycookie.Value = dlOfficeCode.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "R" Then
            'DocSession.rpt_OfficeCode = DocSession.sOfcCode
            'DocSession.rpt_OfficeDesc = lOfficeName.Text
            mycookie = New HttpCookie("udrDocUpHrlyOffice")
            mycookie.Value = DocSession.sOfcCode
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpHrlyOfficeDesc")
            mycookie.Value = lOfficeName.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrDocUpHrlyDateFrom")
        mycookie.Value = tbDate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpHrlyDateTo")
        mycookie.Value = tbTDate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpHrlyTimeFrom")
        mycookie.Value = txtStart.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpHrlyTimeTo")
        mycookie.Value = txtEnd.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)




    End Sub

    Private Sub LoadPreviousCriteria()
        If DocSession.sUserRole = "A" Then
            If Not Request.Cookies("udrDocUpHrlyOffice") Is Nothing Then
                If Request.Cookies("udrDocUpHrlyOffice").Value <> "" Then
                    dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyOffice").Value)
                End If
            End If

        End If
        If Not Request.Cookies("udrDocUpHrlyAuthor") Is Nothing Then
            If Request.Cookies("udrDocUpHrlyAuthor").Value <> "" Then
                tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyAuthor").Value)
            End If
        End If
        If Not Request.Cookies("udrDocUpHrlyDateFrom") Is Nothing Then
            If Request.Cookies("udrDocUpHrlyDateFrom").Value <> "" Then
                tbDate.Text = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyDateFrom").Value)
            End If
        End If
        If Not Request.Cookies("udrDocUpHrlyDateTo") Is Nothing Then
            If Request.Cookies("udrDocUpHrlyDateTo").Value <> "" Then
                tbTDate.Text = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyDateTo").Value)
            End If
        End If
        If Not Request.Cookies("udrDocUpHrlyTimeFrom") Is Nothing Then
            If Request.Cookies("udrDocUpHrlyTimeFrom").Value <> "" Then
                txtStart.Text = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyTimeFrom").Value)
            End If
        End If
        If Not Request.Cookies("udrDocUpHrlyTimeTo") Is Nothing Then
            If Request.Cookies("udrDocUpHrlyTimeTo").Value <> "" Then
                txtEnd.Text = Server.HtmlEncode(Request.Cookies("udrDocUpHrlyTimeTo").Value)
            End If
        End If
    End Sub

    Private Sub imgIcon_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgIcon.Click
        Response.Redirect("DocReceiving.aspx")
    End Sub
End Class
