Imports Microsoft.Reporting.WebForms
Public Class rptDocHistory
    Inherits System.Web.UI.Page
    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(1) As ReportParameter


        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("OfficeName", DocSession.rpt_OfficeDesc, False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String
        If DocSession.rpt_StartDate <> "" OrElse DocSession.rpt_EndDate <> "" Then
            ldf = CDate(DocSession.rpt_StartDate)
            lde = CDate(DocSession.rpt_EndDate)

            lsMF = MonthName(ldf.Month)
            lsME = MonthName(lde.Month)

            If ldf.Year < lde.Year Then
                lsParam = "Action Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
            Else
                lsParam = "Action Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
        Else
            lsParam = "Action Date Range: -All-"

        End If

        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s_sql As String = " SELECT " & _
       "(u1.FirstName + ' ' + u1.LastName) AS docstat, " & _
       "dh.action, " & _
       "dl.Title, " & _
    "dl.doctype, " & _
    "dt.docname, " & _
    "dl.docid, " & _
    "dl.refno, "
        If DocSession.OraClient Then
            s_sql = s_sql & "TO_DATE(dh.actiondate,'mm/dd/yyyy') AS createddate "
        Else
            s_sql = s_sql & "convert(char(10),dh.actiondate,101) AS createddate "
        End If

        s_sql = s_sql & "FROM dochistory dh " & _
   "INNER JOIN doclist dl ON " & _
    "dh.docid = dl.docid " & _
   "INNER JOIN users u1 ON " & _
       "dh.UserId = u1.userid " & _
   "INNER JOIN users u2 ON " & _
       "dl.CreatedBy = u2.userid " & _
   "INNER JOIN doctype dt ON " & _
    "dl.doctype = dt.doctype " & _
 "INNER JOIN GroupDocAccess G ON g.groupId = '" & DocSession.sUserGroup & "' AND g.docId = dl.docType and g.GroupAccessId > 0 " & _
"WHERE (dh.DocId <> 0)  "
        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.doctype = '" & DocSession.rpt_DocType.Trim & "' "
        End If
        If DocSession.rpt_StartDate.Trim <> "" Then
            's_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate.Trim & "' "
            s_sql = s_sql & " AND dh.actiondate >= '" & DocSession.rpt_StartDate.Trim & "' "
        End If
        If DocSession.rpt_EndDate.Trim <> "" Then
            's_sql = s_sql & " AND dl.createddate <= '" & DocSession.rpt_EndDate.Trim & "' "
            s_sql = s_sql & " AND dh.actiondate <= '" & DocSession.rpt_EndDate.Trim & "' "
        End If
        If DocSession.rpt_ColValue.Trim <> "" Then

            s_sql = s_sql & " AND dl.title like '" & DocSession.rpt_ColValue.Trim & "%' "
        End If
        If DocSession.rpt_OfficeCode.Trim <> "" Then
            s_sql = s_sql & " AND dl.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"

        End If
        s_sql = s_sql & " ORDER BY dh.actiondate desc "
        ds_reportdata.SelectCommand = s_sql
    End Sub

End Class