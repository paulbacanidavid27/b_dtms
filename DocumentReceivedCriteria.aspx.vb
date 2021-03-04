Public Class DocumentReceivedCriteria
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
                LoadActionTaken()
                GetRequestType()
                If DocSession.sUserRole = "A" Then
                    GetUsers()
                ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
                    GetUsers()
                End If

                LoadPreviousCriteria()

                'GetUsers()

            End If

        End If

    End Sub
    Private Sub GetUsers(Optional ByVal GrpCode As String = "")

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oUser As DocUser
        Try
            oUser = New DocUser
            oUser.pGroup = GrpCode
            ldata = oUser.UserList

            'lrow = ldata.NewRow
            'lrow(0) = ""
            'lrow(1) = "-All-"
            'ldata.Rows.InsertAt(lrow, 0)
            dlUser.DataSource = ldata
            dlUser.DataValueField = "UserId"
            dlUser.DataTextField = "UserName"
            dlUser.DataBind()
            Try
                'dlUser.SelectedValue = DocSession.sUserId
                dlUser.Visible = True
                labelUser.Visible = True
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrURcvClassification") Is Nothing Then
            dlClassification.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvClassification").Value)
        Else

        End If
        If Not Request.Cookies("udrURcvUser") Is Nothing Then
            If Request.Cookies("udrURcvUser").Value <> "" Then
                If DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
                    dlUser.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvUser").Value)
                Else
                End If

            End If
        End If

        If Not Request.Cookies("udrURcvActionTaken") Is Nothing Then
            dlStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvActionTaken").Value)
        Else

        End If

        If Not Request.Cookies("udrURcvStatus") Is Nothing Then
            dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvStatus").Value)
        Else

        End If

        If Not Request.Cookies("udrURcvTitle") Is Nothing Then
            tbSubject.Text = Server.HtmlEncode(Request.Cookies("udrURcvTitle").Value)
        Else

        End If

        If Not Request.Cookies("udrURcvReqType") Is Nothing Then
            dlRequestType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvReqType").Value)
        Else

        End If

        If Not Request.Cookies("udrURcvSender") Is Nothing Then
            tbSender.Text = Server.HtmlEncode(Request.Cookies("udrURcvSender").Value)
        Else

        End If

        If Not Request.Cookies("udrURcvReceivedFrom") Is Nothing Then
            uploadsdate.Text = Server.HtmlEncode(Request.Cookies("udrURcvReceivedFrom").Value)
        Else

        End If
        If Not Request.Cookies("udrURcvReceivedTo") Is Nothing Then
            uploadedate.Text = Server.HtmlEncode(Request.Cookies("udrURcvReceivedTo").Value)
        Else

        End If
        If Not Request.Cookies("udrURcvSortBy") Is Nothing Then
            dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvSortBy").Value)
        Else

        End If
        If Not Request.Cookies("udrURcvSortOption") Is Nothing Then
            dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrURcvSortOption").Value)
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
    Private Sub LoadActionTaken()
        Dim oDoc As New DocTypes

        'ldata = oDoc.GetSelectedDocStatus
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
    Private Sub LoadReport()
        msg.Text = ""
        Dim lsUsers As String = ""
        'For i As Int16 = 0 To cbUsers.Items.Count - 1 Step 1
        '    If cbUsers.Items(i).Selected Then
        '        If lsUsers = "" Then
        '            lsUsers = "'" & cbUsers.Items(i).Value & "'"
        '        Else
        '            lsUsers = lsUsers & ",'" & cbUsers.Items(i).Value & "'"
        '        End If

        '    End If

        'Next

        'DocSession.rpt_ColValue = lsUsers
        'DocSession.rpt_UploadStartDate = uploadsdate.Text
        Dim mycookie As HttpCookie = New HttpCookie("udrURcvReceivedFrom")
        mycookie.Value = uploadsdate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvReceivedTo")
        mycookie.Value = uploadedate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvClassification")
        mycookie.Value = dlClassification.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvActionTaken")
        mycookie.Value = dlStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvStatus")
        mycookie.Value = dlDocStatus.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvTitle")
        mycookie.Value = tbSubject.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvReqType")
        mycookie.Value = dlRequestType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvSender")
        mycookie.Value = tbSender.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvReqTypeDesc")
        mycookie.Value = dlRequestType.SelectedItem.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        mycookie = New HttpCookie("udrURcvSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrURcvSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        If DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
            'DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
            'DocSession.rpt_OfficeDesc = dlOfficeCode.SelectedItem.Text
            mycookie = New HttpCookie("udrURcvUser")
            mycookie.Value = dlUser.SelectedValue
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrURcvUserText")
            mycookie.Value = dlUser.SelectedItem.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        Else
            mycookie = New HttpCookie("udrURcvUserText")
            mycookie.Value = DocSession.sUserName
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If

        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentReceived.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If


    End Sub


End Class
