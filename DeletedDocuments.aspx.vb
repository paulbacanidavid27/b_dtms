Imports Microsoft.Reporting.WebForms
Public Class DeletedDocuments
    Inherits System.Web.UI.Page
    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(3) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", "-All-", False)
        params(2) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(3) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
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
        If Request.Cookies("docdelDelDateStart").Value <> "" OrElse Request.Cookies("docdelDelDateEnd").Value <> "" Then
            ldf = CDate(Request.Cookies("docdelDelDateStart").Value)
            lde = CDate(Request.Cookies("docdelDelDateEnd").Value)

            lsMF = MonthName(ldf.Month)
            lsME = MonthName(lde.Month)

            If ldf.Year < lde.Year Then
                lsParam = "Deleted Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            Else
                lsParam = "Deleted Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
        Else
            lsParam = "Deleted Date Range: -All-"

        End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s_sql As String = " SELECT " & _
       "(u1.FirstName + ' ' + u1.LastName) AS docstat, " & _
       "dl.deletereason as action, " & _
       "dl.Title, " & _
    "dl.doctype, " & _
    "dt.docname, " & _
    "dl.docid, " & _
    "dl.refno, "
        If DocSession.OraClient Then
            s_sql = s_sql & "TO_DATE(dh.modifieddate,'mm/dd/yyyy') AS createddate "
        Else
            s_sql = s_sql & "convert(char(10),dl.modifieddate,101) AS createddate "
        End If

        s_sql = s_sql & "FROM doclist dl " & _
   "INNER JOIN users u1 ON " & _
       "dl.modifiedby = u1.userid " & _
   "INNER JOIN doctype dt ON " & _
    "dl.doctype = dt.doctype " & _
"WHERE (dl.StatusId = 5)  "
        If Not Request.Cookies("docdelDocType") Is Nothing Then
            If Request.Cookies("docdelDocType").Value <> "" Then
                s_sql = s_sql & " AND dl.doctype = '" & Request.Cookies("docdelDocType").Value & "' "
            End If
        End If
        If Not Request.Cookies("docdelDelDateStart") Is Nothing Then
            If Request.Cookies("docdelDelDateStart").Value <> "" Then
                's_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate.Trim & "' "
                s_sql = s_sql & " AND dl.modifieddate >= '" & Request.Cookies("docdelDelDateStart").Value & "' "
            End If
        End If
        If Not Request.Cookies("docdelDelDateEnd") Is Nothing Then
            If Request.Cookies("docdelDelDateEnd").Value <> "" Then
                's_sql = s_sql & " AND dl.createddate <= '" & DocSession.rpt_EndDate.Trim & "' "
                s_sql = s_sql & " AND dl.modifieddate <= '" & Request.Cookies("docdelDelDateEnd").Value & "' "
            End If
        End If
        If Not Request.Cookies("docdelTitle") Is Nothing Then
            If Request.Cookies("docdelTitle").Value <> "" Then

                s_sql = s_sql & " AND dl.title like '" & Request.Cookies("docdelTitle").Value & "%' "
            End If
        End If
        If Not Request.Cookies("docdelRemarks") Is Nothing Then
            If Request.Cookies("docdelRemarks").Value <> "" Then
                s_sql = s_sql & " AND dl.deletereason like '%" & Request.Cookies("docdelRemarks").Value & "%'"

            End If
        End If

        If Not Request.Cookies("docdelRefNo") Is Nothing Then
            If Request.Cookies("docdelRefNo").Value <> "" Then

                If Request.Cookies("docdelRefNo").Value.IndexOf(",") > 0 Then
                    s_sql = s_sql & " AND dl.refno IN ('" & Request.Cookies("docdelRefNo").Value.Replace(",", "','") & "')"
                Else
                    s_sql = s_sql & " AND dl.refno like '%" & Request.Cookies("docdelRefNo").Value & "%'"
                End If
            End If
        End If

        s_sql = s_sql & " ORDER BY dl.modifieddate desc,dl.refno "
        ds_reportdata.SelectCommand = s_sql
    End Sub

End Class