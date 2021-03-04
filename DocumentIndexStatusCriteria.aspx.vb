Public Class DocumentIndexStatusCriteria
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
                        'If DocSession.sUserRole <> "R" Then
                        '    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        'End If
                    End If
                End Using
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

        SetCookies()

        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentIndexStatus.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=650, width=1000,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If

    End Sub

    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
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
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("disDocType")
        mycookie.Value = lbDocType.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("disOfficeCode")
        mycookie.Value = dlOfficeCode.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("disIndexed")
        mycookie.Value = dlIndexed.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("disRefno")
        mycookie.Value = tbRefNo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

    End Sub

    Private Sub LoadPreviousCriteria()
        If Not Request.Cookies("disDocType") Is Nothing Then
            If Request.Cookies("disDocType").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = lbDocType.Items.FindByValue(Server.HtmlEncode(Request.Cookies("disDocType").Value))
                If Not IsNothing(li) Then
                    lbDocType.SelectedValue = Server.HtmlEncode(Request.Cookies("disDocType").Value)
                End If
            End If
        End If
        If Not Request.Cookies("disOfficeCode") Is Nothing Then
            If Request.Cookies("disOfficeCode").Value <> "" Then

                Dim li As New WebControls.ListItem
                li = dlOfficeCode.Items.FindByValue(Server.HtmlEncode(Request.Cookies("disOfficeCode").Value))
                If Not IsNothing(li) Then
                    dlOfficeCode.SelectedValue = Server.HtmlEncode(Request.Cookies("disOfficeCode").Value)
                End If
            End If
        End If

        If Not Request.Cookies("disIndexed") Is Nothing Then
            If Request.Cookies("disIndexed").Value <> "" Then
                Dim li As New WebControls.ListItem
                li = dlIndexed.Items.FindByValue(Server.HtmlEncode(Request.Cookies("disIndexed").Value))
                If Not IsNothing(li) Then
                    dlIndexed.SelectedValue = Server.HtmlEncode(Request.Cookies("disIndexed").Value)
                End If
            End If
        End If



        If Not Request.Cookies("disRefno") Is Nothing Then
            If Request.Cookies("disRefno").Value <> "" Then
                tbRefNo.Text = Server.HtmlEncode(Request.Cookies("disRefno").Value)
            End If
        End If
        

    End Sub
End Class
