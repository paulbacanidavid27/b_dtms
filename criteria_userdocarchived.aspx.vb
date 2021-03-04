Public Class criteria_userdocarchived
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
    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udaClassification") Is Nothing Then
            dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udaClassification").Value)
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

        If Not Request.Cookies("udaTitle") Is Nothing Then
            tbSubject.Text = Server.HtmlEncode(Request.Cookies("udaTitle").Value)
        Else

        End If

        If Not Request.Cookies("udaUserDocReqType") Is Nothing Then
            dlRequestType.SelectedValue = Server.HtmlEncode(Request.Cookies("udaUserDocReqType").Value)
        Else

        End If

        If Not Request.Cookies("udaArchivedFrom") Is Nothing Then
            uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udaArchivedFrom").Value)
        Else

        End If
        If Not Request.Cookies("udaArchivedTo") Is Nothing Then
            uploadedate.Text = Server.HtmlEncode(Request.Cookies("udaArchivedTo").Value)
        Else

        End If
        If Not Request.Cookies("udaSortBy") Is Nothing Then
            dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udaSortBy").Value)
        Else

        End If
        If Not Request.Cookies("udaSortOption") Is Nothing Then
            dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udaSortOption").Value)
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
        Dim lsUsers As String = ""
        Dim mycookie As HttpCookie = New HttpCookie("udaArchivedFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udaArchivedTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udaClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        
        mycookie = New HttpCookie("udaTitle")
        mycookie.Value = tbSubject.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udaUserDocReqType")
        mycookie.Value = dlRequestType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udaUserDocReqTypeDesc")
        mycookie.Value = dlRequestType.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        mycookie = New HttpCookie("udaSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udaSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentArchivedReport.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes').focus()</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If


    End Sub


End Class
