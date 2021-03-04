Imports Microsoft.Reporting.WebForms

Public Class GroupStatus
    Inherits System.Web.UI.Page

    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(4) As ReportParameter
        Dim lsAddr As String = IIf(DocSession.sGroupAddress = "", "General Solano Street, San Miguel, Manila", DocSession.sGroupAddress)
        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", OfficeDesc(), False)
        params(2) = New ReportParameter("OfficeAddress", lsAddr, False)
        params(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(4) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function
    Private Function OfficeDesc() As String
        Dim lsDesc As String
        If Not Request.Cookies("udrGrpStatGroupDesc") Is Nothing Then
            lsDesc = Request.Cookies("udrGrpStatGroupDesc").Value
        Else
            lsDesc = ""
        End If
        Return lsDesc
    End Function
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
        
        
            s_sql = s_sql & "(isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _
                        "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                        "convert(varchar(10),dr.assigneddate,101) AS adate, " & _
                        "Isnull(convert(varchar(10),dr.actiondate,101),'') AS actdate, " & _
                        "pendingdays=dbo.CalcAgeByOffice('" & Request.Cookies("udrGrpStatOffice").Value & "',5,dr.assigneddate,isnull(dr.actiondate,getdate())), " & _
                        "ds.description as statremarks "
           

        s_sql = s_sql & "FROM doclist dl " & _
        "INNER JOIN doctype dt " & _
          "ON dl.doctype = dt.doctype	" & _
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


        If Request.Cookies("udrGrpStatDocNo").Value <> "" Then
            
            Dim sDocNo As String = Request.Cookies("udrGrpStatDocNo").Value
            If sDocNo.IndexOf(",") > 0 Then
                s_sql = s_sql & " AND refno in ('" & sDocNo.Replace(",", "','") & "') "
            Else
                s_sql = s_sql & " AND (refno like '%" & sDocNo & "%') "
            End If

        End If

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

        
        If Request.Cookies("udrGrpStatSortBy").Value <> "" Then
            s_sql = s_sql & " ORDER by " & Request.Cookies("udrGrpStatSortBy").Value & " " & Request.Cookies("udrGrpStatSortOption").Value
        Else
            s_sql = s_sql & " ORDER by dt.docname,dl.title "
        End If


        dsDocStatus.SelectCommand = s_sql


        
    End Sub

End Class