Public Class DocumentUploadedCriteria
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Reports")
    End Sub
    Private Sub GetDocType()
        Dim oDoc As New DocTypes
        'oDoc.pGroupId = DocSession.sUserGroup
        Using ldata As DataTable = oDoc.GetDocType2()


            'dlDocType.DataTextField = "DocName"
            'dlDocType.DataValueField = "DocType"

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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsExpired()
        If DocSession.sReportAccess <> "1" Then
            Master.ShowMessage("You don't have access to this report. Please contact the administrator.")
            'ReportViewer1.Visible = False
            btSearch.Visible = False
        Else
            If Not IsPostBack Then
                GetDocType()
                GetOfficeCode()
                GetUserGroups()
                GetUsers()
                LoadPreviousCriteria()
                If DocSession.sUserRole = "A" Or DocSession.sUserGroup = "CRD" Then
                    btDetails.Visible = True
                Else
                    btDetails.Visible = False
                End If
            End If

        End If

    End Sub
    Private Function ValidateUploadDate() As Boolean

        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text = "" OrElse tbDCTo.Text = "") Then
            msg.Text = "Uploaded date range is required."
            Return False
        End If
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) OrElse (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            msg.Text = "Please provide a valid Uploaded date range."
            Return False
        End If
        If (CDate(tbDCFrom.Text) > CDate(tbDCTo.Text)) Then
            msg.Text = "Uploaded End date should not be prior to Uploaded Start Date."
            Return False
        End If
        Return True
    End Function
    Private Function ValidateUploadDate2() As Boolean

        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text = "") Then
            msg.Text = "Uploaded Start Date range is required."
            Return False
        End If

        If (dluser.SelectedValue.Trim = "") Then
            msg.Text = "Uploaded By is required."
            Return False
        End If
        Return True
    End Function
    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadDetails()

    End Sub

    Private Sub LoadReport2()
        msg.Text = ""
        If Not ValidateUploadDate2() Then
            Exit Sub
        End If
        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewUploadedDetailsRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentUploadedDetails.aspx', 'ViewUploadedDetailsRpt' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewUploadedDetailsRpt", lsScript, False)
        End If

    End Sub


    Private Sub GetUserGroups()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            If DocSession.sUserRole = "A" Then
                If Not Request.Cookies("udrDocUpOffice") Is Nothing Then
                    If Request.Cookies("udrDocUpOffice").Value <> "" Then
                        oType.pOfficeCode = Request.Cookies("udrDocUpOffice").Value
                    Else
                        oType.pOfficeCode = dlOfficeCode.SelectedValue
                    End If
                Else
                    oType.pOfficeCode = dlOfficeCode.SelectedValue
                End If


                ldata = oType.RetrieveGroups

            ElseIf DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "R" OrElse DocSession.sUserRole = "G" Then

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

    Private Sub GetGroups()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup


            oType.pOfficeCode = dlOfficeCode.SelectedValue

            ldata = oType.RetrieveGroups


            dlGroup.DataSource = ldata
            dlGroup.DataValueField = "GroupId"
            dlGroup.DataTextField = "GroupName"
            dlGroup.DataBind()

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
        'Dim lrow As DataRow
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
    Private Sub GetUsers()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oUser As DocUser
        Try
            oUser = New DocUser
            oUser.pGroup = dlGroup.SelectedValue
            ldata = oUser.RetrieveUsers
            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "-All-"
            ldata.Rows.InsertAt(lrow, 0)
            dlUser.DataSource = ldata
            dlUser.DataValueField = "UserId"
            dlUser.DataTextField = "UserName"
            dlUser.DataBind()
            'Try
            '    dlUser.SelectedValue = DocSession.sUserId
            'Catch ex As Exception

            'End Try

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
        Dim mycookie As HttpCookie = New HttpCookie("udrDocUpDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)


        If DocSession.sUserRole = "A" Then
            mycookie = New HttpCookie("udrDocUpGroupId")

            If Not dlGroup Is Nothing Then
                mycookie.Value = dlGroup.SelectedValue
            Else
                mycookie.Value = ""
            End If

            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpGroupDesc")

            If Not dlGroup Is Nothing Then
                mycookie.Value = dlGroup.SelectedItem.Text
            Else
                mycookie.Value = ""
            End If

            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpOffice")

            If Not dlOfficeCode Is Nothing Then
                mycookie.Value = dlOfficeCode.SelectedValue
            Else
                mycookie.Value = ""
            End If

            mycookie.Expires = DateTime.Now.AddDays(30)
                Response.Cookies.Add(mycookie)
                mycookie = New HttpCookie("udrDocUpOfficeDesc")

            If Not dlOfficeCode Is Nothing Then
                mycookie.Value = dlOfficeCode.SelectedItem.Text
            Else
                mycookie.Value = ""
            End If
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)

        ElseIf DocSession.sUserRole = "D" Then
                'DocSession.rpt_OfficeCode = DocSession.sOfcCode
                'DocSession.rpt_OfficeDesc = lOfficeName.Text
                mycookie = New HttpCookie("udrDocUpGroupId")

            If Not dlGroup Is Nothing Then
                mycookie.Value = dlGroup.SelectedValue
            Else
                mycookie.Value = ""
            End If
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpGroupDesc")
            If Not dlOfficeCode Is Nothing Then
                mycookie.Value = dlGroup.SelectedItem.Text
            Else
                mycookie.Value = ""
            End If

            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpOffice")
            mycookie.Value = DocSession.sOfcCode
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
            mycookie = New HttpCookie("udrDocUpOfficeDesc")
            mycookie.Value = lOfficeName.Text
            mycookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(mycookie)
        End If


        mycookie = New HttpCookie("udrDocUpUploadedByName")
        If Not dlOfficeCode Is Nothing Then
            mycookie.Value = dluser.SelectedItem.Text
        Else
            mycookie.Value = ""
        End If

        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpUploadedBy")
        If Not IsDBNull(dluser) AndAlso Not IsNothing(dluser) Then
            mycookie.Value = dluser.SelectedValue
        Else
            mycookie.Value = ""
        End If

        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpRecvFrom")
        mycookie.Value = tbDCFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpRecvTo")
        mycookie.Value = tbDCTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpSortBy")
        mycookie.Value = dlColumns.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("udrDocUpSortOption")
        mycookie.Value = dlSortOption.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("udrDocUpDocType") Is Nothing Then
            If Request.Cookies("udrDocUpDocType").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocUpDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpDocType").Value)
                End If
                'lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpDocType").Value)
            End If
        End If
        'If Not Request.Cookies("udrDocUpGroupId") Is Nothing Then
        '    If Request.Cookies("udrDocUpGroupId").Value <> "" Then
        '        dlGroup.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpGroupId").Value)
        '    End If
        'End If

        If Not Request.Cookies("udrDocUpUploadedBy") Is Nothing Then
            If Request.Cookies("udrDocUpUploadedBy").Value <> "" Then
                'tbAuthor.Text = Server.HtmlEncode(Request.Cookies("udrDocUpUploadedBy").Value)
                Dim li As New WebControls.ListItem
                li = dluser.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocUpUploadedBy").Value))
                If Not IsNothing(li) Then
                    dluser.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpUploadedBy").Value)
                End If
            End If
        End If
        If DocSession.sUserRole = "A" Then
            If Not Request.Cookies("udrDocUpOffice") Is Nothing Then
                If Request.Cookies("udrDocUpOffice").Value <> "" Then
                    Dim li As New WebControls.ListItem
                    li = dlOfficeCode.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocUpOffice").Value))
                    If Not IsNothing(li) Then
                        dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpOffice").Value)
                    End If
                    'dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpOffice").Value)
                End If
            End If
        End If

        'If Not Request.Cookies("udrDocUpStatus") Is Nothing Then
        '    If Request.Cookies("udrDocUpStatus").Value <> "" Then
        '        dlDocStatus.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpStatus").Value)
        '    End If
        'End If
        If Not Request.Cookies("udrDocUpRecvFrom") Is Nothing Then
            If Request.Cookies("udrDocUpRecvFrom").Value <> "" Then
                tbDCFrom.Text = Server.HtmlEncode(Request.Cookies("udrDocUpRecvFrom").Value)
            Else
                tbDCFrom.Text = "" 'DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
            End If
        Else
            tbDCFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
        End If
        If Not Request.Cookies("udrDocUpRecvTo") Is Nothing Then
            If Request.Cookies("udrDocUpRecvTo").Value <> "" Then
                tbDCTo.Text = Server.HtmlEncode(Request.Cookies("udrDocUpRecvTo").Value)
            Else
                tbDCTo.Text = "" 'Date.Now.ToShortDateString
            End If
        Else
            tbDCTo.Text = Date.Now.ToShortDateString
        End If

        If Not Request.Cookies("udrDocUpSortBy") Is Nothing Then
            If Request.Cookies("udrDocUpSortBy").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlColumns.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocUpSortBy").Value))
                If Not IsNothing(li) Then
                    dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpSortBy").Value)
                End If
                'dlColumns.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpSortBy").Value)
            End If
        End If
        If Not Request.Cookies("udrDocUpSortOption") Is Nothing Then
            If Request.Cookies("udrDocUpSortOption").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlSortOption.Items.FindByValue(Server.HtmlEncode(Request.Cookies("udrDocUpSortOption").Value))
                If Not IsNothing(li) Then
                    dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpSortOption").Value)
                End If
                'dlSortOption.SelectedValue = Server.HtmlEncode(Request.Cookies("udrDocUpSortOption").Value)
            End If
        End If


    End Sub

    Private Sub dlOfficeCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlOfficeCode.SelectedIndexChanged
        GetGroups()

    End Sub

    Private Sub dlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlGroup.SelectedIndexChanged
        GetUsers()
    End Sub

    Private Sub btDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btDetails.Click
        LoadReport2()
    End Sub

    Private Sub LoadDetails()
        msg.Text = ""
        If Not ValidateUploadDate() Then
            Exit Sub
        End If
        SetCookies()
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewUploadedRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentUploaded.aspx', 'ViewUploadedRpt' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewUploadedRpt", lsScript, False)
        End If

    End Sub
End Class
