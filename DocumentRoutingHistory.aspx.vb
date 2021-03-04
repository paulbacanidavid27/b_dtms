Imports Microsoft.Reporting.WebForms

Public Class DocumentRoutingHistory
    Inherits System.Web.UI.Page

    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(4) As ReportParameter
        Dim lsAddr As String = IIf(DocSession.sGroupAddress = "", "General Solano Street, San Miguel, Manila", DocSession.sGroupAddress)
        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", Request.Cookies("udrGrpStatRtHistGroupDesc").Value, False)
        params(2) = New ReportParameter("OfficeAddress", lsAddr, False)
        params(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(4) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function
    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String
        If Request.Cookies("udrGrpStatRtHistAssgnFrom").Value <> "" OrElse Request.Cookies("udrGrpStatRtHistAssgnTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrGrpStatRtHistAssgnFrom").Value)
            lde = CDate(Request.Cookies("udrGrpStatRtHistAssgnTo").Value)
            'udrGrpStatRtHistAssgnFrom()
            lsMF = MonthName(ldf.Month) & " " & Day(ldf).ToString() & ","
            lsME = MonthName(lde.Month) & " " & Day(lde).ToString() & ","

            'If ldf.Year < lde.Year Then
            lsParam = "Assigned Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            'Else
            '    lsParam = "Assigned Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            'End If
        Else
            If Request.Cookies("udrGrpStatRtHistRecvFrom").Value <> "" OrElse Request.Cookies("udrGrpStatRtHistRecvTo").Value <> "" Then
                ldf = CDate(Request.Cookies("udrGrpStatRtHistRecvFrom").Value)
                lde = CDate(Request.Cookies("udrGrpStatRtHistRecvTo").Value)

                lsMF = MonthName(ldf.Month) & " " & Day(ldf).ToString() & ","
                lsME = MonthName(lde.Month) & " " & Day(lde).ToString() & ","
                If ldf.Year < lde.Year Then
                    lsParam = "Received Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
                Else
                    lsParam = "Received Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
                End If
            Else
            End If

        End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        Dim lsRefno As String = Request.Cookies("udrGrpStatRtHistRefNo").Value

        Dim s_sql_all As String = "SELECT distinct dl.docid FROM DocList dl " & _
            "INNER JOIN DocRouting dr " & _
            " ON dr.docid = dl.docid and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
            "INNER JOIN users u2 " & _
          "ON dr.approverid = u2.userid	AND " & _
          "u2.usergroup = '" & Request.Cookies("udrGrpStatRtHistGroupId").Value & "' " & _
          "INNER JOIN GroupDocAccess G ON	" & _
          "g.groupId = '" & DocSession.sUserGroup & "' AND " & _
          "g.docId = dl.docType and g.GroupAccessId > 0 " & _
            "WHERE dl.statusid <> 5 "
        If lsRefno <> "" Then

            If lsRefno.IndexOf(",") > 0 Then
                s_sql_all = s_sql_all & " AND dl.refno IN ('" & lsRefno.Replace(",", "','") & "')"
            Else
                If lsRefno <> "" Then
                    s_sql_all = s_sql_all & " AND dl.refno LIKE '%" & lsRefno & "%' "
                End If

            End If
        End If
        If Request.Cookies("udrGrpStatRtHistDocType").Value <> "" Then
            s_sql_all = s_sql_all & " AND dl.DocType =  '" & Request.Cookies("udrGrpStatRtHistDocType").Value & "'"
        End If

        If Request.Cookies("udrGrpStatRtHistAssgnFrom").Value <> "" Then
            s_sql_all = s_sql_all & " AND dr.assigneddate >= '" & Request.Cookies("udrGrpStatRtHistAssgnFrom").Value & "'"
        End If
        If Request.Cookies("udrGrpStatRtHistAssgnTo").Value <> "" Then
            s_sql_all = s_sql_all & " AND dr.assigneddate < dateadd(day,1,'" & Request.Cookies("udrGrpStatRtHistAssgnTo").Value & "')"
        End If
        If Request.Cookies("udrGrpStatRtHistRecvFrom").Value <> "" Then
            s_sql_all = s_sql_all & " AND dl.createddate >= '" & Request.Cookies("udrGrpStatRtHistRecvFrom").Value & "'"
        End If
        If Request.Cookies("udrGrpStatRtHistRecvTo").Value <> "" Then
            s_sql_all = s_sql_all & " AND dl.createddate < dateadd(day,1,'" & Request.Cookies("udrGrpStatRtHistRecvTo").Value & "')"
        End If
        If Request.Cookies("udrGrpStatRtHistActionTaken").Value <> "" AndAlso Request.Cookies("udrGrpStatRtHistActionTaken").Value <> "0" Then
            s_sql_all = s_sql_all & " AND dl.statusid =  " & Request.Cookies("udrGrpStatRtHistActionTaken").Value
        End If
        If Request.Cookies("udrGrpStatRtHistStatus").Value = "C" Then
            s_sql_all = s_sql_all & " AND dl.statusid in (" & DocSession.CompleteStatus & ")"
        ElseIf Request.Cookies("udrGrpStatRtHistStatus").Value = "O" Then
            s_sql_all = s_sql_all & " AND dl.statusid not in (" & DocSession.CompleteStatus & ")"
        End If
        If Request.Cookies("udrGrpStatRtHistRemarks").Value <> "" Then
            s_sql_all = s_sql_all & " AND dr.Comment like '%" & Request.Cookies("udrGrpStatRtHistRemarks").Value & "%' "
        End If

        Dim s_sql As String = "SELECT " & _
         "dl.refno,	" & _
         "dl.title,	" & _
         "docname = dr.comment, " & _
         "dlds.description as statusdesc, " & _
         " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc2, "
        s_sql = s_sql & "(isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _
                    "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                    "convert(varchar(10),dr.assigneddate,101) AS adate, " & _
                    "Isnull(convert(varchar(10),dr.actiondate,101),'') AS actdate, " & _
                    "pendingdays=dbo.CalcAge('" & DocSession.sUserGroup & "',5,dr.assigneddate,isnull(dr.actiondate,getdate())), " & _
                    "ds.description as statremarks "


        s_sql = s_sql & "FROM doclist dl " & _
        "INNER JOIN docstatus dlds ON	" & _
          "dlds.statusid = dl.statusId " & _
        "INNER JOIN doctype dt " & _
          "ON dl.doctype = dt.doctype	" & _
        "INNER JOIN users u " & _
          "ON dl.createdby = u.userid	" & _
        "INNER JOIN docRouting dr ON	" & _
          "dr.docid = dl.docid and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
         "INNER JOIN docstatus ds ON	" & _
          "ds.statusid = dr.statusId " & _
        "INNER JOIN users u2 " & _
          "ON dr.approverid = u2.userid	" & _
        "WHERE dl.docid in (" & s_sql_all & ") "

        If Request.Cookies("udrGrpStatRtHistUploadedBy").Value <> "" Then
            If DocSession.OraClient Then
                s_sql = s_sql & " AND ((u.FirstName||' '||u.LastName) like '%" & Request.Cookies("udrGrpStatRtHistUploadedBy").Value & "%') "
            Else
                s_sql = s_sql & " AND ((u.FirstName+' '+u.LastName) like '%" & Request.Cookies("udrGrpStatRtHistUploadedBy").Value & "%') "
            End If
        End If

        If Request.Cookies("udrGrpStatRtHistPersonnel").Value <> "" Then
            If DocSession.OraClient Then
                s_sql = s_sql & " AND (NVL(u2.FirstName,u.LastName)||' '||NVL(u2.LastName,u.LastName) like '%" & Request.Cookies("udrGrpStatRtHistPersonnel").Value & "%') "
            Else
                s_sql = s_sql & " AND (isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) like '%" & Request.Cookies("udrGrpStatRtHistPersonnel").Value & "%') "
            End If

        End If

        If Request.Cookies("udrGrpStatRtHistSortBy").Value = "dl.refno" Then
            s_sql = s_sql & " ORDER by " & Request.Cookies("udrGrpStatRtHistSortBy").Value & " " & Request.Cookies("udrGrpStatRtHistSortOption").Value & ",dr.assigneddate,dr.actiondate"
        ElseIf Request.Cookies("udrGrpStatRtHistSortBy").Value <> "" Then
            s_sql = s_sql & " ORDER by " & Request.Cookies("udrGrpStatRtHistSortBy").Value & " " & Request.Cookies("udrGrpStatRtHistSortOption").Value & ",dl.refno,dr.assigneddate,dr.actiondate"
        Else
            s_sql = s_sql & " ORDER by dl.refno,dr.assigneddate,dr.actiondate"
        End If

        dsDocStatus.SelectCommand = s_sql

    End Sub

End Class