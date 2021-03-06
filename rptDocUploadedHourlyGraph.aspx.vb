﻿Imports Microsoft.Reporting.WebForms

Public Class rptDocUploadedHourlyGraph
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(1) As ReportParameter


        params(0) = New ReportParameter("DateTimeRange", DeriveDateRange(), False)
        params(1) = New ReportParameter("DeliveredBy", DocSession.sUserName, False)
        'params(2) = New ReportParameter("TimeRange", "Time", False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            Dim oDl As New DocList
            Dim pGroup As String = System.Configuration.ConfigurationManager.AppSettings("ontimegroup")
            Dim s_sql As String = "SELECT " & _
"ddot = sum(case when (regionrecord = 1 and (ddl.ReceivedDate is not null or ddl.ReceivedBy is not null)) then 1 " & _
    " when regionrecord = 0 and ddl.ReceivedDate is not null and DATEDIFF(MINUTE,ddl.delstarttime,ddl.ReceivedDate) <= 60 then 1 else 0 end) " & _
",ddnot = sum(case when regionrecord = 0 and ddl.ReceivedDate is not null and DATEDIFF(MINUTE,ddl.delstarttime,ddl.ReceivedDate) > 60 then 1 else 0 end) " & _
",others = SUM(case when (ddl.ReceivedBy is null or ddl.ReceivedDate is null) then 1 else 0 end) " & _
",totaldocs = count(ddl.createddate) " & _
"            from " & _
"(  " & _
"select  " & _
            "dl.ReceivedBy, " & _
            "dl.createddate " & _
",delstarttime = case  " & _
     "when dl.createddate between cast(convert(char(10),dl.createddate,101)+' 11am' as datetime) and cast(convert(char(10),dl.createddate,101)+' 1pm' as datetime)  " & _
      "then cast(convert(char(10),dl.createddate,101)+' 1pm' as datetime) " & _
     "when dl.createddate between cast(convert(char(10),dl.createddate,101)+' 3pm' as datetime) and cast(convert(char(10),dl.createddate,101)+' 11:59:59pm' as datetime) " & _
      "then cast(convert(char(10),dateadd(day,1,dl.createddate),101)+' 9am' as datetime)" & _
     "when dl.createddate between cast(convert(char(10),dl.createddate,101)+' 12am' as datetime) and cast(convert(char(10),dl.createddate,101)+' 07:59:59am' as datetime) " & _
      "then cast(convert(char(10),dl.createddate,101)+' 9am' as datetime)  " & _
        "Else  dl.createddate " & _
            "End " & _
",dl.ReceivedDate " & _
",regionrecord = case when (u.UserGroup= '" & pGroup & "') then 0 else 1 end " & _
"from DocList dl " & _
oDl.DocPendingReportSql(Request.Cookies("udrDocUpHrlyOffice").Value) & " " & _
     "INNER JOIN GroupDocAccess G ON " & _
      "g.groupId = '" & DocSession.sUserGroup & "' AND " & _
      "g.docId = dl.docType and g.GroupAccessId > 0 " & _
      "INNER JOIN Users u ON " & _
      "dl.createdby = u.userid " & _
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

            s_sql = s_sql & ") ddl "


            'If DocSession.rpt_ColValue <> "" Then
            '    s_sql = s_sql & " AND (u.firstname like '%" & DocSession.rpt_ColValue & "%' or u.lastname like '%" & DocSession.rpt_ColValue & "%') "
            'End If

            'If DocSession.rpt_DocType.Trim <> "" Then
            '    s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"

            'End If
            'If DocSession.rpt_OfficeCode.Trim <> "" Then
            '    s_sql = s_sql & " AND dl.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"

            'End If
            's_sql = s_sql & " GROUP BY u.Firstname,u.LastName,dt.docname,"
            'If DocSession.OraClient Then
            '    s_sql = s_sql & "TO_DATE(dl.createddate,'mm/dd/yyyy') "
            'Else
            '    s_sql = s_sql & "convert(varchar(10),dl.createddate,101) "
            'End If

            SqlDataSource1.SelectCommand = s_sql
        End If

    End Sub
    Private Function DeriveDateRange() As String

        Dim lsParam As String
        lsParam = "Date Range: " & DocSession.rpt_StartDate & " - " & DocSession.rpt_EndDate

        Return lsParam
    End Function
End Class