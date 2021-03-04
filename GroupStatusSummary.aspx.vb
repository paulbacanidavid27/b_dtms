Imports Microsoft.Reporting.WebForms
Public Class GroupStatusSummary
    Inherits System.Web.UI.Page

    Private Sub GroupStatusSummary_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim params(2) As ReportParameter


        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", Request.Cookies("udrGrpStatGroupDesc").Value, False)
        params(2) = New ReportParameter("ReportTitle", DeriveTitle(), False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveTitle() As String
        If Request.Cookies("udrGrpStatStatus").Value = "O" Then
            Return "Open Documents / " & Request.Cookies("udrGrpStatActionTakenDesc").Value & " " & "Last Action"
        ElseIf Request.Cookies("udrGrpStatStatus").Value = "C" Then
            Return "Completed/Closed Documents /" & Request.Cookies("udrGrpStatActionTakenDesc").Value & " " & "Last Action"
        Else
            Return "All " & "Status / " & Request.Cookies("udrGrpStatActionTakenDesc").Value & " " & "Last Action"
        End If

    End Function

    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String
        'If DocSession.rpt_StartDate <> "" OrElse DocSession.rpt_EndDate <> "" Then
        '    ldf = CDate(DocSession.rpt_StartDate)
        '    lde = CDate(DocSession.rpt_EndDate)

        '    lsMF = MonthName(ldf.Month)
        '    lsME = MonthName(lde.Month)

        '    If ldf.Year < lde.Year Then
        '        lsParam = "Assigned Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
        '    Else
        '        lsParam = "Assigned Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
        '    End If
        'Else
        If Request.Cookies("udrGrpStatAssgnFrom").Value <> "" OrElse Request.Cookies("udrGrpStatAssgnTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrGrpStatAssgnFrom").Value)
            lde = CDate(Request.Cookies("udrGrpStatAssgnTo").Value)

            lsMF = MonthName(ldf.Month)
            lsME = MonthName(lde.Month)
            If ldf.Year < lde.Year Then
                lsParam = "Assigned Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            Else
                lsParam = "Assigned Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
            'Else
        End If

        'End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then




            Dim s_sql As String = "SELECT " & _
    "(isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto,  " & _
    "count(dl.docid) as totaldocs " & _
    "FROM doclist dl  " & _
    "INNER JOIN doctype dt ON dl.doctype = dt.doctype " & _
    "INNER JOIN GroupDocAccess G ON	g.groupId = '" & DocSession.sUserGroup & "' " & _
     "AND g.docId = dl.docType and g.GroupAccessId > 0 " & _
    "INNER JOIN docstatus ds ON	ds.statusid = dl.statusId " & _
    "INNER JOIN users u ON dl.createdby = u.userid " & _
    "INNER JOIN docRouting dr ON	dr.docid = dl.docid " & _
     "and dr.seqno = dl.routingseqno " & _
     "and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
    "INNER JOIN users u2 ON dr.approverid = u2.userid " & _
     "AND u2.usergroup = '" & Request.Cookies("udrGrpStatGroupId").Value & "' " & _
            "WHERE(dl.statusid <> 5) "

            If Request.Cookies("udrGrpStatAssgnFrom").Value <> "" Then
                s_sql = s_sql & " AND dr.assigneddate >= '" & Request.Cookies("udrGrpStatAssgnFrom").Value & "'"

            End If
            If Request.Cookies("udrGrpStatAssgnTo").Value <> "" Then
                s_sql = s_sql & " AND dr.assigneddate < dateadd(day,1,'" & Request.Cookies("udrGrpStatAssgnTo").Value & "')"
            End If
            If Request.Cookies("udrGrpStatRecvFrom").Value <> "" Then
                s_sql = s_sql & " AND dl.createddate >= '" & Request.Cookies("udrGrpStatRecvFrom").Value & "'"

            End If
            If Request.Cookies("udrGrpStatRecvTo").Value <> "" Then
                s_sql = s_sql & " AND dl.createddate < dateadd(day,1,'" & Request.Cookies("udrGrpStatRecvTo").Value & "')"
            End If


            If Request.Cookies("udrGrpStatUploadedBy").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND ((u.FirstName||' '||u.LastName) like '%" & Request.Cookies("udrGrpStatUploadedBy").Value & "%') "
                Else
                    s_sql = s_sql & " AND ((u.FirstName+' '+u.LastName) like '%" & Request.Cookies("udrGrpStatUploadedBy").Value & "%') "
                End If
            End If

            If Request.Cookies("udrGrpStatPersonnel").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND (NVL(u2.FirstName,u.LastName)||' '||NVL(u2.LastName,u.LastName) like '%" & Request.Cookies("udrGrpStatPersonnel").Value & "%') "
                Else
                    s_sql = s_sql & " AND (isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) like '%" & Request.Cookies("udrGrpStatPersonnel").Value & "%') "
                End If

            End If

            If Request.Cookies("udrGrpStatDocType").Value <> "" Then
                s_sql = s_sql & " AND dl.DocType =  '" & Request.Cookies("udrGrpStatDocType").Value & "'"
            End If

            If Request.Cookies("udrGrpStatActionTaken").Value <> "" AndAlso Request.Cookies("udrGrpStatActionTaken").Value <> "0" Then
                s_sql = s_sql & " AND dl.statusid =  " & Request.Cookies("udrGrpStatActionTaken").Value
            End If
            If Request.Cookies("udrGrpStatStatus").Value = "C" Then
                s_sql = s_sql & " AND dl.statusid in (" & DocSession.CompleteStatus & ")"
            ElseIf Request.Cookies("udrGrpStatStatus").Value = "O" Then
                s_sql = s_sql & " AND dl.statusid not in (" & DocSession.CompleteStatus & ")"
            End If

            s_sql = s_sql & "GROUP BY isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) " & _
    "order by isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) "

            ds_summdata.SelectCommand = s_sql


        End If
    End Sub

End Class