Public Class ucPromptEmail
    Inherits System.Web.UI.UserControl

    Public Event e_showmessage()
    Public Event e_close()
    Public WriteOnly Property pTitle As String
        Set(ByVal value As String)
            lsTitle.Text = value
        End Set
    End Property

    Public WriteOnly Property pCloseLabel As String
        Set(ByVal value As String)
            btClose.Text = value
        End Set
    End Property

    Public WriteOnly Property pOKLabel As String
        Set(ByVal value As String)
            btOK.Text = value
        End Set
    End Property

    Public Property pMessage As String
        Get
            Return lMsg.Text
        End Get
        Set(ByVal value As String)
            lMsg.Text = value
        End Set
    End Property
    Private Sub ShowMessage(ByVal asMsg As String)
        pMessage = asMsg
        RaiseEvent e_showmessage()
    End Sub

    Public WriteOnly Property pId As String
        Set(ByVal value As String)
            imgClose.OnClientClick = "hideWindow('" & value & "')"
            btClose.OnClientClick = "hideWindow('" & value & "');return true"
            btOK.OnClientClick = "hideWindow('" & value & "');return true"
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler uEmail.e_check, AddressOf ufAddEmail
    End Sub

    Private Sub ufaddemail()
        Repeater1.DataSource = GetData(uEmail.pUserEmail, uEmail.pAuthor)
        Repeater1.DataBind()
        pEmail.update()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If DocSession.sUserEmail <> "" Then
                lFrom.Text = DocSession.sUserName & " (" & DocSession.sUserEmail & ")"
            Else
                lFrom.Text = DocSession.AdminEmail
            End If

        End If
    End Sub

    Private Sub btOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOK.Click
        Try
            SendEmail()
        Catch ex As MailException
            'Throw New MailException(ex.Message)
            'pMessage = ex.Message
            ShowMessage(ex.Message)
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            ShowMessage("Warning: Error while sending email ('" & ex.Message & "'). Please try again.")
            'RaiseEvent e_showmessage()
        End Try

    End Sub

    Public Sub fDelete(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim rItem As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)
        rItem.Visible = False
    End Sub

    Private Function GetData(ByVal asEmail As String, ByVal asRecipient As String) As DataTable
        
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblEmail")
            loData.Columns.Add("Email", Type.GetType("System.String"))
            loData.Columns.Add("recipient", Type.GetType("System.String"))

            For Each rptItems As RepeaterItem In Repeater1.Items
                If rptItems.Visible Then
                    loRow = loData.NewRow()
                    loRow("Recipient") = DirectCast(rptItems.FindControl("lEmail"), Label).Text
                    loRow("Email") = DirectCast(rptItems.FindControl("lEmail"), Label).ToolTip
                    loData.Rows.Add(loRow)
                End If

            Next
            loRow = loData.NewRow()
            loRow("Email") = asEmail
            loRow("recipient") = asRecipient
            loData.Rows.Add(loRow)
            Return loData
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Function
    Private Function getArchivePointPerson() As String
        Dim lsEmail As String = ""
        For Each rptItems As RepeaterItem In Repeater1.Items
            If rptItems.Visible Then
                If lsEmail = "" Then
                    lsEmail = DirectCast(rptItems.FindControl("lEmail"), Label).ToolTip
                Else
                    lsEmail = lsEmail & "," & DirectCast(rptItems.FindControl("lEmail"), Label).ToolTip
                End If
            End If
        Next
        Return lsEmail
    End Function

    Private Sub SendEmail()
        Dim oSet As DocSetting
        Dim lsEmail As String

        Try
            lsEmail = getArchivePointPerson()
            If lsEmail.Trim = "" Then
                ShowMessage("Please select recipient before clicking Send button.")
            Else
                Dim oEmail As New DocEmail
                'oEmail.pEmailFrom = DocSession.AdminEmail
                If DocSession.sUserEmail <> "" Then
                    oEmail.pEmailFrom = DocSession.sUserName & " <" & DocSession.sUserEmail & ">"
                Else
                    oEmail.pEmailFrom = DocSession.AdminEmail
                End If

                oEmail.pEmailTo = lsEmail
                If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
                    oEmail.pEmailSubject = "Document Management System - Archive Notification"
                Else
                    oEmail.pEmailSubject = "Document Management System - Archive Notification"
                End If

                oEmail.pEmailBody = Email_Body(DocSession.sDocTitle, DocSession.sReferenceNo, DocSession.sUserName)
                oEmail.pEmailIsHTML = True
                oEmail.SendEmail()
                ShowMessage("An email notification has been successfully sent to the point person(s).")
                AddHistory(lsEmail)
                RaiseEvent e_close()
            End If
        Catch ex As MailException
            Throw New MailException(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Sub
    Private Sub AddHistory(ByVal asName As String)
        Dim ohist As New DocHistory
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Email"
        ohist.pUserId = DocSession.sUserId
        ohist.pAction = "Archived email notification sent to '" & asName & "' by " & DocSession.sUserName
        ohist.AddHistory()
    End Sub
    Private Function Email_Body(ByVal asTitle As String, ByVal asRefno As String, ByVal asArchivedBy As String) As String
        Dim lsEmail As New StringBuilder
        lsEmail.Append("<table style='width: 100%;' border='0' cellpadding='0' cellspacing='0'>")
        lsEmail.Append("<tr style='background-color: #003399'>")
        If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
            lsEmail.Append("<td align='left' style='height:30px;padding-left:10px' colspan='2'><span style='font-family:helvetica;font-weight:bold;font-size:1.2em;color:#FFFFF0;'>DOCUMENT MANAGEMENT SYSTEM</span></td>")
        Else
            lsEmail.Append("<td align='left' style='height:30px;padding-left:10px' colspan='2'><span style='font-family:helvetica;font-weight:bold;font-size:1.2em;color:#FFFFF0;'>DOCUMENT MANAGEMENT SYSTEM</span></td>")
        End If
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>Document " & asTitle & " with reference no " & asRefno & " has been archived by " & asArchivedBy & " on " & DateTime.Now.ToString & ".</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("</table>")
        lsEmail.Append("<br /><br /><div style='font-size:8pt;font-family:arial'><i>This is an auto-generated email notification, please do not reply.</i></div>")
        Return lsEmail.ToString
    End Function
End Class