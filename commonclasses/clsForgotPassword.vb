Public Class ClsForgotPassword

    Public Sub New()

    End Sub

    Function Email_Body(ByVal asPassword As String, asUserId As String) As String
        Dim lsEmail As New StringBuilder
        lsEmail.Append("<table style='width: 100%;' border='0' cellpadding='0' cellspacing='0'>")
        lsEmail.Append("<tr style='background-color: #003399'>")
        If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
            lsEmail.Append("<td align='left' style='height:30px;padding-left:10px' colspan='2'><span style='font-family:helvetica;font-weight:bold;font-size:1.2em;color:#FFFFF0;'>Document Management System</span></td>")
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
        lsEmail.Append("<td>&nbsp;" & asUserId & "</td>")
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
End Class
