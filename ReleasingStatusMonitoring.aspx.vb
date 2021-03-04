Imports Microsoft.Reporting.WebForms
Public Class ReleasingStatusMonitoring
    Inherits System.Web.UI.Page

    Dim gDateRange As String = ""
    Private Sub ReleasingStatusMonitoring_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Not IsPostBack Then
        Dim parms(2) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        parms(0) = New ReportParameter("DateRange", DeriveDateRange, False)
        parms(1) = New ReportParameter("ImgPath", DeriveLogo(), False)
        parms(2) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)


        Me.ReportViewer1.LocalReport.SetParameters(parms)
        'End If

    End Sub
    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function

    Private Function DeriveGroup() As String

        Return Request.Cookies("udrDocUpGroupDesc").Value

    End Function
    'Private Function DeriveOffice() As String

    '    Return Request.Cookies("udrDocUpOfficeDesc").Value

    'End Function
    Private Function DeriveDateRange() As String
        
        Dim lsParam As String = ""

        If Request.Cookies("docMonReportDateFrom").Value <> "" AndAlso Request.Cookies("docMonReportDateTo").Value <> "" Then
            lsParam = "Date Range: " & Request.Cookies("docMonReportDateFrom").Value & " - " & Request.Cookies("docMonReportDateTo").Value
        ElseIf Request.Cookies("docMonReportDateFrom").Value <> "" AndAlso Request.Cookies("docMonReportDateTo").Value = "" Then
            lsParam = "Date Range: " & Request.Cookies("udrDocUpRecvFrom").Value & " to present. "
        ElseIf Request.Cookies("docMonReportDateTo").Value <> "" AndAlso Request.Cookies("docMonReportDateFrom").Value = "" Then
            lsParam = "Date Range: Up to " & Request.Cookies("docMonReportDateTo").Value
            gDateRange = ""
        Else
            lsParam = "Date Range: -All-"
            gDateRange = ""
        End If

        'End If

        Return lsParam
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            Dim s_sql As String = "SELECT * FROM ( " &
"SELECT " &
"cm.RefNo,cm.Description,cm.MainStatus,cms.Description as StatDesc,convert(char(20),cm.DateTimeReceived,100) as DateTimeReceived, " &
"(case when cm.personaldelivery = 1 then cm.deliverycompleted else cm.mailingcompleted end) as CompletedDate " &
" FROM crdMonitoring cm inner join crdMonitoringStatus cms  " &
 " on cm.Mainstatus = cms.StatusId "
            If Request.Cookies("docMonReportStatus").Value <> "" Then
                s_sql = s_sql & " Where cm.Mainstatus = " & Request.Cookies("docMonReportStatus").Value
            Else
                s_sql = s_sql & " Where cm.Mainstatus <> 8 "
            End If

            s_sql = s_sql & ")  A Where a.Mainstatus <> 8 "
            If Request.Cookies("docMonReportDateFrom").Value <> "" Then
                s_sql = s_sql & " and a.DateTimeReceived >= Convert(datetime,'" & CDate(Request.Cookies("docMonReportDateFrom").Value).ToString("MM/dd/yyyy") & " 00:00:00')"

            End If
            If Request.Cookies("docMonReportDateTo").Value <> "" Then
                s_sql = s_sql & " and a.DateTimeReceived < DateAdd(d,1,'" & CDate(Request.Cookies("docMonReportDateTo").Value).ToString("MM/dd/yyyy") & "') "
            End If
            If Request.Cookies("docMonSortBy").Value <> "" Then
                s_sql = s_sql & " ORDER by " & Request.Cookies("docMonSortBy").Value & " " & Request.Cookies("docMonSortOption").Value
            Else
                s_sql = s_sql & " ORDER by a.CompletedDate "
            End If
            ds_reportdata.SelectCommand = s_sql
        End If

    End Sub

End Class