Imports System
Imports System.Data.SqlClient

Public Class forgot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            tbUserID.Focus()
        End If


    End Sub



    Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLogin.Click
        Try

        
        Using ldt As DataTable = retrieve_userinfo()
            If ldt.Rows.Count > 0 Then
                If ldt(0)("Locked") = "1" Then
                    msg.Text = "Your account is currently locked. Please contact the administrator."
                    msg.CssClass = "msg_red"
                Else
                    Send_email(ldt(0)("email"), ldt(0)("password"))
                    msg.Text = "Your password has been sent to your email address."
                    msg.CssClass = "msg_green"
                    Button1.Visible = True
                    btLogin.Visible = False
                End If
            Else
                msg.Text = "Your User ID or email does not exist."
                msg.CssClass = "msg_red"
            End If
            End Using
        Catch ex As Exception
            msg.Text = "An error occurred while sending email (" & ex.Message & "). Please try again."
            msg.CssClass = "msg_red"
        End Try
        pnl.Update()
    End Sub

    Public Function retrieve_userinfo() As DataTable
        Dim ldt As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT UserId " &
      ",Password " &
      ",FirstName " &
      ",LastName " &
      ",Email " &
      ",Locked " &
 "FROM users " &
 "WHERE Email =  '" & tbEmail.Text.Trim & "' or UserLogin = '" & tbUserID.Text.Trim & "'"
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_VALIDATEEMAIL"
            'objCommand.ParametersAddWithValue("@UserId", tbUserID.Text)
            'objCommand.ParametersAddWithValue("@email", tbEmail.Text)


            ldt = objCommand.Fill

            Return ldt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldt Is Nothing Then
                ldt.Dispose()
                ldt = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Sub Send_email(ByVal asEmail As String, ByVal asPassword As String)
        '(1) Create the MailMessage instance
        '(3) Create the SmtpClient object
        Dim smtp_s As New System.Net.Mail.SmtpClient
        Dim mm As New System.Net.Mail.MailMessage(DocSession.AdminEmail, asEmail)
        Dim ec As New crypt
        Try


            ''(2) Assign the MailMessage's properties
            'mm.Subject = "Document Management System Password Request"
            'mm.Body =
            'mm.IsBodyHtml = True

            'smtp_s.Host = System.Configuration.ConfigurationManager.AppSettings("mailserver")
            ''smtp.Host = "localhost"
            ''(4) Send the MailMessage (will use the Web.config settings)
            'smtp_s.Port = CInt(System.Configuration.ConfigurationManager.AppSettings("mailserverport"))
            'smtp_s.Send(mm)

            Dim oEmail As New DocEmail
            oEmail.pEmailFrom = DocSession.AdminEmail
            oEmail.pEmailTo = asEmail
            If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
                oEmail.pEmailSubject = "Document Management System Password Request"
            Else
                oEmail.pEmailSubject = "Document Management System Password Request"
            End If


            oEmail.pEmailBody = Email_Body(ec.Decrypt(asPassword))
            oEmail.pEmailIsHTML = True
            oEmail.SendEmail()
        Catch ex As MailException
            Throw New MailException(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not mm Is Nothing Then
                mm.Dispose()
                mm = Nothing
            End If
            If Not smtp_s Is Nothing Then
                smtp_s = Nothing
            End If


        End Try


    End Sub
    Function Email_Body(ByVal asPassword As String) As String
        Dim lsEmail As New StringBuilder
        lsEmail.Append("<table style='width: 100%;' border='0' cellpadding='0' cellspacing='0'>")
        lsEmail.Append("<tr style='background-color: #003399'>")
        If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
            lsEmail.Append("<td align='left' style='height:30px;padding-left:10px' colspan='2'><span style='font-family:helvetica;font-weight:bold;font-size:1.2em;color:#FFFFF0;'>COOP MANAGEMENT SYSTEM</span></td>")
        Else
            lsEmail.Append("<td align='left' style='height:30px;padding-left:10px' colspan='2'><span style='font-family:helvetica;font-weight:bold;font-size:1.2em;color:#FFFFF0;'>DOCUMENT MANAGEMENT SYSTEM</span></td>")
        End If
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>You have requested to send your password to this email.</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>User ID:</td>")
        lsEmail.Append("<td>&nbsp;" & tbUserID.Text & "</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>Your Current Password:</td>")
        lsEmail.Append("<td>&nbsp;" & asPassword & "</td>")
        lsEmail.Append("</tr>")
        lsEmail.Append("<tr>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("<td>&nbsp;</td>")
        lsEmail.Append("</tr>    ")
        lsEmail.Append("</table>")
        lsEmail.Append("<br /><br /><div style='font-size:8pt;font-family:arial'><i>This is an auto-generated email notification, please do not reply.</i></div>")
        Return lsEmail.ToString
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Response.Redirect("default.aspx")
    End Sub

End Class