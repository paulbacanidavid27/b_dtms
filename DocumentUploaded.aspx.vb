Imports Microsoft.Reporting.WebForms
Public Class DocumentUploaded
    Inherits System.Web.UI.Page
    Private Sub DocumentUploaded_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Not IsPostBack Then
        Dim parms(4) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        parms(0) = New ReportParameter("OfficeCode", DeriveOffice(), False)
        parms(1) = New ReportParameter("GroupCode", DeriveGroup(), False)
        parms(2) = New ReportParameter("DFrom", DeriveDateRange(), False)

        parms(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        parms(4) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
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
    Private Function DeriveOffice() As String

        Return Request.Cookies("udrDocUpOfficeDesc").Value

    End Function
    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String = ""
        
        If Request.Cookies("udrDocUpRecvFrom").Value <> "" AndAlso Request.Cookies("udrDocUpRecvTo").Value <> "" Then
            'ldf = CDate(Request.Cookies("udrDocUpRecvFrom").Value)
            'lde = CDate(Request.Cookies("udrDocUpRecvTo").Value)

            'lsMF = MonthName(ldf.Month)
            'lsME = MonthName(lde.Month)
            'If ldf.Year < lde.Year Then
            '    lsParam = "Upload Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            'Else
            '    lsParam = "Upload Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            'End If
            lsParam = "Upload Date Range: " & Request.Cookies("udrDocUpRecvFrom").Value & " - " & Request.Cookies("udrDocUpRecvTo").Value
        ElseIf Request.Cookies("udrDocUpRecvFrom").Value <> "" AndAlso Request.Cookies("udrDocUpRecvTo").Value = "" Then
            lsParam = "Upload Date Range: " & Request.Cookies("udrDocUpRecvFrom").Value & " to present. "
        ElseIf Request.Cookies("udrDocUpRecvTo").Value <> "" AndAlso Request.Cookies("udrDocUpRecvFrom").Value = "" Then
            lsParam = "Upload Date Range: Up to " & Request.Cookies("udrDocUpRecvTo").Value
        Else
            lsParam = "Upload Date Range: -All-"
        End If

        'End If

        Return lsParam
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            Dim s_sql As String = "SELECT " & _
                "dt.docname, "
            'Dim odl As New DocList

            If DocSession.OraClient Then
                s_sql = s_sql & "NVL(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                        "TO_DATE(dl.createddate,'mm/dd/yyyy') AS cdate, "
            Else
                s_sql = s_sql & "isnull(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                        "convert(varchar(10),dl.createddate,101) AS cdate, "
            End If

            s_sql = s_sql & "count(dl.docId) AS totalcount " & _
    "FROM doclist dl " & _
     " INNER JOIN users u " & _
      "ON dl.createdby = u.userid "
            If Request.Cookies("udrDocUpGroupId").Value <> "" Then
                s_sql = s_sql & " AND u.UserGroup = '" & Request.Cookies("udrDocUpGroupId").Value & "'"
            End If
            s_sql = s_sql & " INNER JOIN doctype dt " & _
      "ON dl.doctype = dt.doctype " & _
      "WHERE dl.statusid <> 5 "

            If Request.Cookies("udrDocUpRecvFrom").Value <> "" Then
                s_sql = s_sql & " AND dl.createddate >= '" & Request.Cookies("udrDocUpRecvFrom").Value & "'"
            End If
            If Request.Cookies("udrDocUpRecvTo").Value <> "" Then
                s_sql = s_sql & " AND dl.createddate < DateAdd(day,1,'" & Request.Cookies("udrDocUpRecvTo").Value & "')"
            End If
            If Request.Cookies("udrDocUpUploadedBy").Value <> "" Then
                s_sql = s_sql & " AND (u.firstname+' '+u.lastname like '%" & Request.Cookies("udrDocUpUploadedBy").Value & "%') "
            End If

            If Request.Cookies("udrDocUpDocType").Value <> "" Then
                s_sql = s_sql & " AND dl.DocType =  '" & Request.Cookies("udrDocUpDocType").Value & "'"
            End If

            s_sql = s_sql & " GROUP BY u.Firstname,u.LastName,dt.docname,"

            s_sql = s_sql & "convert(varchar(10),dl.createddate,101) "
            s_sql = "SELECT docname,cdate,author,totalcount FROM (" & _
                    s_sql & ") tbl "

            If Request.Cookies("udrDocUpSortBy").Value <> "" Then
                s_sql = s_sql & " ORDER by " & Request.Cookies("udrDocUpSortBy").Value & " " & Request.Cookies("udrDocUpSortOption").Value
                If Request.Cookies("udrDocUpSortBy").Value = "author" Then
                    s_sql = s_sql & ",docname,cdate,totalcount"
                ElseIf Request.Cookies("udrDocUpSortBy").Value = "docname" Then
                    s_sql = s_sql & ",author,cdate,totalcount "
                ElseIf Request.Cookies("udrDocUpSortBy").Value = "cdate" Then
                    s_sql = s_sql & ",author,docname,totalcount "
                ElseIf Request.Cookies("udrDocUpSortBy").Value = "totalcount" Then
                    s_sql = s_sql & ",author,docname,cdate"
                End If
            Else
                s_sql = s_sql & " ORDER by author,docname,cdate,totalcount "
            End If

            's_sql = s_sql & " SELECT ORDER BY u.Firstname,u.LastName,dt.docname,convert(datetime,convert(varchar(10),dl.createddate,101)) "
            ds_reportdata.SelectCommand = s_sql
        End If

    End Sub

End Class