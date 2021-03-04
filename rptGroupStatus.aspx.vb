Imports Microsoft.Reporting.WebForms

Public Class rptGroupStatus
    Inherits System.Web.UI.Page

    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(2) As ReportParameter
        Dim lsAddr As String = IIf(DocSession.sGroupAddress = "", "General Solano Street, San Miguel, Manila", DocSession.sGroupAddress)

        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", Request.Cookies("udrGrpStatGroupDesc").Value, False)
        params(2) = New ReportParameter("OfficeAddress", lsAddr, False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String
        If Request.Cookies("udrGrpStatAssgnFrom").Value <> "" OrElse Request.Cookies("udrGrpStatAssgnTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrGrpStatAssgnFrom").Value)
            lde = CDate(Request.Cookies("udrGrpStatAssgnTo").Value)
            'udrGrpStatAssgnFrom()
            lsMF = MonthName(ldf.Month)
            lsME = MonthName(lde.Month)

            If ldf.Year < lde.Year Then
                lsParam = "Assigned Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            Else
                lsParam = "Assigned Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
        Else
            If Request.Cookies("udrGrpStatRecvFrom").Value <> "" OrElse Request.Cookies("udrGrpStatRecvTo").Value <> "" Then
                ldf = CDate(Request.Cookies("udrGrpStatRecvFrom").Value)
                lde = CDate(Request.Cookies("udrGrpStatRecvTo").Value)

                lsMF = MonthName(ldf.Month)
                lsME = MonthName(lde.Month)
                If ldf.Year < lde.Year Then
                    lsParam = "Received Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
                Else
                    lsParam = "Received Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
                End If
            Else
            End If

        End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then


        Dim s_sql As String = "SELECT " & _
         "dl.refno,	" & _
         "dl.title,	" & _
         "dt.docname, " & _
         " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc, "
        '" case when dl.statusid = 12 then 'Completed/Closed' when dl.statusid = 15 then 'Completed/Closed' when dl.statusid = 8 then 'Completed/Closed' when dl.statusid = 18 then 'Completed/Closed' else 'Open' end as statusdesc, "
        '" case when dr.statusid = 12 then 'Completed/Closed' when dr.statusid = 14 then 'Completed/Closed' when dr.statusid = 8 then 'Completed/Closed' when ds.Description = 'No Action Required' then 'Completed/Closed' when dr.statusid = 15 then 'Partially Completed' when dr.statusid = 16 then 'On-Hold' else 'Open' end as statusdesc, "

        If DocSession.OraClient Then
            s_sql = s_sql & "(NVL(u2.FirstName,u.FirstName)+' '+NVL(u2.LastName,u.LastName)) AS assignedto, " & _
                        "TO_DATE(dl.createddate,'mm/dd/yyyy') AS cdate, " & _
                        "TO_DATE(dr.assigneddate,'mm/dd/yyyy') AS adate, " & _
                        "NVL(TO_DATE(dr.actiondate,'mm/dd/yyyy'),'') AS actdate, " & _
                        "(dr.assigneddate - NVL(dr.actiondate,getdate())) as pendingdays, " & _
                        "ds.description as statremarks "
            '" case when dl.statusid = 12 or dl.statusid = 8 or dl.statusid = 14 or dl.statusid = 15 or dl.statusid = 16 then " & _
            '"   case when dl.routingseqno is null then TO_DATE(dl.createddate,'mm/dd/yyyy') else TO_DATE(dr.actiondate,'mm/dd/yyyy') end " & _
            '" else ds.description end as statremarks "
        Else
            s_sql = s_sql & "(isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _
                        "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                        "convert(varchar(10),dr.assigneddate,101) AS adate, " & _
                        "Isnull(convert(varchar(10),dr.actiondate,101),'') AS actdate, " & _
                        "pendingdays=dbo.CalcAge('" & DocSession.sUserGroup & "',5,dr.assigneddate,isnull(dr.actiondate,getdate())), " & _
                        "ds.description as statremarks "
            '" case when dl.statusid = 12 or dl.statusid = 8 or dl.statusid = 14 or dl.statusid = 15 or dl.statusid = 16 then " & _
            '"   case when dl.routingseqno is null then convert(varchar(10),dl.createddate,101) else convert(varchar(10),dr.actiondate,101) end " & _
            '" else ds.description end as statremarks "
        End If

        s_sql = s_sql & "FROM doclist dl " & _
        "INNER JOIN doctype dt " & _
          "ON dl.doctype = dt.doctype	" & _
        "INNER JOIN GroupDocAccess G ON	" & _
          "g.groupId = '" & DocSession.sUserGroup & "' AND " & _
          "g.docId = dl.docType and g.GroupAccessId > 0 " & _
        "INNER JOIN docstatus ds ON	" & _
          "ds.statusid = dl.statusId " & _
        "INNER JOIN users u " & _
          "ON dl.createdby = u.userid	" & _
        "INNER JOIN docRouting dr ON	" & _
          "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
        "INNER JOIN users u2 " & _
          "ON dr.approverid = u2.userid	AND " & _
          "u2.usergroup = '" & Request.Cookies("udrGrpStatGroupId").Value & "'	" & _
        "WHERE dl.statusid <> 5 "



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
        If Request.Cookies("udrGrpStatRemarks").Value <> "" Then
            
            s_sql = s_sql & " AND dr.Comment like '%" & Request.Cookies("udrGrpStatRemarks").Value & "%' "


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

        'If Request.Cookies("udrGrpStatOffice").Value <> "" Then
        '    s_sql = s_sql & " AND dl.OfficeCode =  '" & Request.Cookies("udrGrpStatOffice").Value & "'"
        'End If

        'If Request.Cookies("udrGrpStatReqType").Value <> "" Then
        '    s_sql = s_sql & " AND dl.RequestType =  '" & Request.Cookies("udrGrpStatReqType").Value & "'"
        'End If

        's_sql = s_sql & " ORDER by dl.refno "
        If Request.Cookies("udrGrpStatSortBy").Value <> "" Then
            s_sql = s_sql & " ORDER by " & Request.Cookies("udrGrpStatSortBy").Value & " " & Request.Cookies("udrGrpStatSortOption").Value
        Else
            s_sql = s_sql & " ORDER by dt.docname,dl.title "
        End If


        'If DocSession.rpt_StartDate <> "" Then
        '    s_sql = s_sql & " AND dr.assigneddate >= '" & DocSession.rpt_StartDate & "'"
        'End If
        'If DocSession.rpt_EndDate <> "" Then
        '    s_sql = s_sql & " AND dr.assigneddate < dateadd(day,1,'" & DocSession.rpt_EndDate & "')"
        'End If

        'If DocSession.rpt_UploadStartDate <> "" Then
        '    s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_UploadStartDate & "'"
        'End If
        'If DocSession.rpt_UploadEndDate <> "" Then
        '    s_sql = s_sql & " AND dl.createddate < dateadd(day,1,'" & DocSession.rpt_UploadEndDate & "')"
        'End If

        'If DocSession.rpt_ColValue <> "" Then
        '    s_sql = s_sql & " AND (u.firstname like '%" & DocSession.rpt_ColValue & "%' or u.lastname like '%" & DocSession.rpt_ColValue & "%') "
        'End If

        'If DocSession.rpt_ColValue2 <> "" Then
        '    s_sql = s_sql & " AND (isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) like '%" & DocSession.rpt_ColValue2 & "%') "
        'End If

        'If DocSession.rpt_DocType.Trim <> "" Then
        '    s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"
        'End If

        'Dim lsDocStatus, lsStatus As String
        'lsStatus = DocSession.doc_DocStatus.Split("^")(0)
        'lsDocStatus = DocSession.doc_DocStatus.Split("^")(1)
        'If lsStatus.Trim <> "" Then
        '    s_sql = s_sql & " AND dl.statusid =  " & lsStatus
        'End If
        'If lsDocStatus.Trim = "C" Then
        '    s_sql = s_sql & " AND dl.statusid in (18,15,8,12,19)"
        'ElseIf lsDocStatus.Trim = "O" Then
        '    s_sql = s_sql & " AND dl.statusid not in (18,15,8,12,19)"
        'End If

        'If DocSession.rpt_OfficeCode.Trim <> "" Then
        '    s_sql = s_sql & " AND (udl.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"

        'End If

        's_sql = s_sql & " ORDER by assignedto "
        dsDocStatus.SelectCommand = s_sql


        'Dim params As ReportParameter

        'params = New ReportParameter("dfrom", DateTime.Now, False)
        ''params(1) = New ReportParameter("dto", DateTime.Now, False)

        'Me.ReportViewer1.LocalReport.SetParameters(params)

        'End If
    End Sub
    
End Class