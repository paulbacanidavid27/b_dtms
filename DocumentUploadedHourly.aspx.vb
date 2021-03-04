Imports Microsoft.Reporting.WebForms

Public Class DocumentUploadedHourly
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(3) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("DateTimeRange", DeriveDateRange(), False)
        params(1) = New ReportParameter("DeliveredBy", DocSession.sUserName, False)
        params(2) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(3) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            Try


                Dim oDL As New DocList


                Dim s_sql As String = "SELECT " & _
                    "dl.refno, " & _
                    "dl.routedTo, " & _
                    "dl.title, "
                If DocSession.OraClient Then
                    s_sql = s_sql & "NVL(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                            "TO_DATE(dl.createddate,'mon dd yyyy hh:mi:ss:mmmAM') AS cdate,NVL(dl.receivedby,'') as receivedby "
                Else
                    s_sql = s_sql & "isnull(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                            "convert(varchar,dl.createddate,109) AS cdate,ISNULL(dl.receivedby,'')+' '+ISNULL(CONVERT(char(10),dl.receiveddate,101),'')+' '+ISNULL(right(CONVERT(varchar,dl.receiveddate),7),'')  as receivedby  "
                End If

                s_sql = s_sql & _
        "FROM doclist dl " & _
        oDL.DocPendingReportSql(Request.Cookies("udrDocUpHrlyOffice").Value) & _
     " INNER JOIN users u " & _
      "ON dl.createdby = u.userid " & _
      "WHERE dl.statusid <> 5 "
                'If DocSession.sUserRole = "A" Then
                '    s_sql = s_sql & " AND (dl.OfficeCode = '" & Request.Cookies("udrDocUpHrlyOffice").Value & "'" & _
                '            " OR dl.ArchiverOfc = '" & Request.Cookies("udrDocUpHrlyOffice").Value & "'" & _
                '            " OR dl.UploaderOfc = '" & Request.Cookies("udrDocUpHrlyOffice").Value & "') "
                'End If

                If Request.Cookies("udrDocUpHrlyDateFrom").Value <> "" Then
                    s_sql = s_sql & " AND dl.createddate >= '" & Request.Cookies("udrDocUpHrlyDateFrom").Value & " " & "'"
                End If
                If Request.Cookies("udrDocUpHrlyDateTo").Value <> "" Then
                    s_sql = s_sql & " AND dl.createddate < DateAdd(day,1,'" & Request.Cookies("udrDocUpHrlyDateTo").Value & "')"
                End If

                s_sql = s_sql & " ORDER BY dl.officeCode,dl.createddate "



                SqlDataSource1.SelectCommand = s_sql

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Function DeriveDateRange() As String

        Dim lsParam As String
        lsParam = "Date Range: " & DocSession.rpt_StartDate & " - " & DocSession.rpt_EndDate

        Return lsParam
    End Function
End Class