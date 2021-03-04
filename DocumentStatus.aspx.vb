Imports Microsoft.Reporting.WebForms

Public Class DocumentStatus
    Inherits System.Web.UI.Page

    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(3) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", OfficeDesc(), False)
        params(2) = New ReportParameter("ReportTitle", DocSession.ReceiptReplyName, False)
        params(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        Me.ReportViewer1.LocalReport.SetParameters(params)

    End Sub

    Private Function DeriveLogo() As String
        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function

    Private Function OfficeDesc() As String
        Dim lsDesc As String
        If Not Request.Cookies("udrDocStatOfficeDesc") Is Nothing Then
            lsDesc = Request.Cookies("udrDocStatOfficeDesc").Value
        Else
            lsDesc = ""
        End If
        Return lsDesc
    End Function
    Private Function DeriveTitle() As String

        Return "All " & "Documents"

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
        If Request.Cookies("udrDocStatRecvFrom").Value <> "" OrElse Request.Cookies("udrDocStatRecvTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrDocStatRecvFrom").Value)
            lde = CDate(Request.Cookies("udrDocStatRecvTo").Value)

            lsMF = MonthName(ldf.Month)
            lsME = MonthName(lde.Month)
            If ldf.Year < lde.Year Then
                lsParam = "Received Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            Else
                lsParam = "Received Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
            'Else
        End If

        'End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Try
                Dim oDL As New DocList
                Dim s_sql As String = "SELECT " & _
                 "dl.refno,	" & _
                 "dl.title,	" & _
                 "dt.docname, " & _
                 " case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc, "

                s_sql = s_sql & "(isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName)) AS assignedto, " & _
                           "isnull(convert(varchar(10),dl.completeddate,101),'') AS addate, " & _
                           "case when isdate(div.colvalue)=1 then convert(char(10),convert(datetime,div.colvalue),101) else isnull(convert(char(10),dl.completeddate,101),'') end AS adate," & _
                           "isnull(dl.DocSender,'') AS SenderName, " & _
                           "isnull(o.Description,'') AS officename, " & _
                           "isnull(dl.RequestType,'') AS RequestType, " & _
                           "isnull(rt.RequestDescription,'') AS RequestDescription, " & _
                           "convert(varchar(10),dl.createddate,101) as cdate," & _
                            "dbo.CalcAgeByOffice('" & Request.Cookies("udrDocStatOffice").Value & "',5,dl.createddate,case when isdate(div.colvalue)=1 then convert(datetime,div.colvalue) else isnull(dl.completeddate,getdate()) end) AS age, " & _
                            "isnull(ds.description,'') as statremarks "

                s_sql = s_sql & "FROM doclist dl " & _
                    oDL.DocPendingReportSql(Request.Cookies("udrDocStatOffice").Value, True) & _
                "INNER JOIN doctype dt " & _
                  "ON dl.doctype = dt.doctype	" & _
                "INNER JOIN users u " & _
                  "ON dl.createdby = u.userid	" & _
                "LEFT JOIN docRouting dr ON	" & _
                  "dr.docid = dl.docid and dr.seqno = dl.routingseqno and (dr.CarbonCopy is null or dr.CarbonCopy = 0) " & _
                "LEFT JOIN docstatus ds ON	" & _
                  "ds.statusid = dl.statusId " & _
                "LEFT JOIN (" & _
                            "select div.DocId,div.DocType,div.ColValue from docindex di INNER JOIN docindexvalues div ON  " & _
                    "div.ColumnId = di.ColumnId And div.DocType = di.DocType " & _
                            "where di.ColumnName = '" & DocSession.RDSColumn & "' " & _
                    ") " & _
                    " div ON div.docid=dl.docId " & _
                "LEFT JOIN users u2 " & _
                  "ON dr.approverid = u2.userid	" & _
                "LEFT JOIN users s " & _
                  "ON dr.sender = s.userid	" & _
                  "LEFT JOIN office o " & _
                  "ON dl.OfficeCode = o.OfficeCode	" & _
                  "LEFT JOIN DocRequestType rt " & _
                  "ON rt.RequestType = dl.RequestType	" & _
                "WHERE dl.statusid <> 5 "
                'If DocSession.sUserRole = "A" Then
                '    s_sql = s_sql & " AND (dl.OfficeCode = '" & Request.Cookies("udrDocStatOffice").Value & "'" & _
                '            " OR dl.ArchiverOfc = '" & Request.Cookies("udrDocStatOffice").Value & "'" & _
                '            " OR dl.UploaderOfc = '" & Request.Cookies("udrDocStatOffice").Value & "') "
                'End If

                If Request.Cookies("udrDocStatSubject").Value <> "" Then
                    s_sql = s_sql & " AND dl.title like '%" & Request.Cookies("udrDocStatSubject").Value & "%'"
                End If

                If Request.Cookies("udrDocStatRecvFrom").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND TRUNC(dr.assigneddate) >= TO_DATE('" & Request.Cookies("udrDocStatRecvFrom").Value & "','mm/dd/yyyy')"
                    Else
                        's_sql = s_sql & " AND dr.assigneddate >= '" & Request.Cookies("udrDocStatRecvFrom").Value & "'"
                        s_sql = s_sql & " AND dl.createddate >= '" & Request.Cookies("udrDocStatRecvFrom").Value & "'"
                    End If

                End If

                If Request.Cookies("udrDocStatShow").Value <> "" Then
                    If Request.Cookies("udrDocStatShow").Value = "1" Then
                        s_sql = s_sql & " AND dl.Confidential = 1 "
                    Else
                        's_sql = s_sql & " AND dr.assigneddate < dateadd(day,1,'" & Request.Cookies("udrDocStatRecvTo").Value & "')"
                        s_sql = s_sql & " AND (dl.Confidential is null or dl.Confidential = 0) "
                    End If

                End If
                If Request.Cookies("udrDocStatClassification").Value <> "" Then
                    If Request.Cookies("udrDocStatClassification").Value = "1" Then
                        s_sql = s_sql & " AND dl.InternalDoc = 1 "
                    Else
                        's_sql = s_sql & " AND dr.assigneddate < dateadd(day,1,'" & Request.Cookies("udrDocStatRecvTo").Value & "')"
                        s_sql = s_sql & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                    End If

                End If
                If Request.Cookies("udrDocStatRecvTo").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND trunc(dr.assigneddate) <= TO_DATE('" & Request.Cookies("udrDocStatRecvTo").Value & "','mm/dd/yyyy')"
                    Else
                        's_sql = s_sql & " AND dr.assigneddate < dateadd(day,1,'" & Request.Cookies("udrDocStatRecvTo").Value & "')"
                        s_sql = s_sql & " AND dl.createddate < dateadd(day,1,'" & Request.Cookies("udrDocStatRecvTo").Value & "')"
                    End If

                End If

                If Request.Cookies("udrDocStatDocNo").Value <> "" Then
                    'If DocSession.OraClient Then
                    '    s_sql = s_sql & " AND ((u.LastName||' '||u.LastName) like '%" & Request.Cookies("udrDocStatDocNo").Value & "%') "
                    'Else
                    Dim sDocNo As String = Request.Cookies("udrDocStatDocNo").Value
                    If sDocNo.IndexOf(",") > 0 Then
                        s_sql = s_sql & " AND refno in ('" & sDocNo.Replace(",", "','") & "') "
                    Else
                        s_sql = s_sql & " AND (refno like '%" & sDocNo & "%') "
                    End If

                    'End If
                End If

                If Request.Cookies("udrDocStatUploadedBy").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND ((u.LastName||' '||u.LastName) like '%" & Request.Cookies("udrDocStatUploadedBy").Value & "%') "
                    Else
                        s_sql = s_sql & " AND ((u.LastName+' '+u.LastName) like '%" & Request.Cookies("udrDocStatUploadedBy").Value & "%') "
                    End If
                End If

                If Request.Cookies("udrDocStatPersonnel").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND (NVL(u2.FirstName,u.LastName)||' '||NVL(u2.LastName,u.LastName) like '%" & Request.Cookies("udrDocStatPersonnel").Value & "%') "
                    Else
                        s_sql = s_sql & " AND (isnull(u2.FirstName,u.LastName)+' '+isnull(u2.LastName,u.LastName) like '%" & Request.Cookies("udrDocStatPersonnel").Value & "%') "
                    End If

                End If

                If Request.Cookies("udrDocStatDocType").Value <> "" Then
                    s_sql = s_sql & " AND dl.DocType =  '" & Request.Cookies("udrDocStatDocType").Value & "'"
                End If

                If Request.Cookies("udrDocStatActionTaken").Value <> "" AndAlso Request.Cookies("udrDocStatActionTaken").Value <> "0" Then
                    s_sql = s_sql & " AND dl.statusid =  " & Request.Cookies("udrDocStatActionTaken").Value
                End If
                If Request.Cookies("udrDocStatStatus").Value = "C" Then
                    s_sql = s_sql & " AND dl.statusid in (" & DocSession.CompleteStatus & ")"
                ElseIf Request.Cookies("udrDocStatStatus").Value = "O" Then
                    s_sql = s_sql & " AND dl.statusid not in (" & DocSession.CompleteStatus & ")"
                End If

                'If Request.Cookies("udrDocStatOffice").Value <> "" Then
                '    s_sql = s_sql & " AND dl.OfficeCode =  '" & Request.Cookies("udrDocStatOffice").Value & "'"
                'End If

                If Request.Cookies("udrDocStatReqType").Value <> "" Then
                    s_sql = s_sql & " AND dl.RequestType =  '" & Request.Cookies("udrDocStatReqType").Value & "'"
                End If

                's_sql = s_sql & " ORDER by dl.refno "
                If Request.Cookies("udrDocStatSortBy").Value <> "" Then
                    s_sql = s_sql & " ORDER by " & Request.Cookies("udrDocStatSortBy").Value & " " & Request.Cookies("udrDocStatSortOption").Value
                Else
                    s_sql = s_sql & " ORDER by dt.docname,dl.title "
                End If

                dsDocStatus.SelectCommand = s_sql


                'Dim params As ReportParameter

                'params = New ReportParameter("dfrom", DateTime.Now, False)
                ''params(1) = New ReportParameter("dto", DateTime.Now, False)

                'Me.ReportViewer1.LocalReport.SetParameters(params)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If Not dsDocStatus Is Nothing Then
                    dsDocStatus.Dispose()
                End If

            End Try
        End If

    End Sub
    'If DocSession.OraClient Then
    '    s_sql = s_sql & "LEFT JOIN  (select docid, sn = max(seqNo),actiondate = max(nvl(actiondate,assignedDate)) from docRouting d group by docid)  dr ON	"
    'Else
    '    s_sql = s_sql & "LEFT JOIN  (select docid, sn = max(seqNo),actiondate = max(isnull(actiondate,assignedDate)) from docRouting d group by docid)  dr ON	"
    'End If

    's_sql = s_sql & "dl.docid = dr.docid and actionDate is not null  " & _
End Class