Public Class rptOverdue
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s_sql As String = "SELECT	" & _
 "dl.title,	" & _
 "dt.docname, "
        If DocSession.OraClient Then
            s_sql = s_sql & "NVL(u.FirstName,'')+' '+NVL(u.LastName,'') AS author,	" & _
                 "NVL(a.FirstName,'')+' '+NVL(a.LastName,'') AS assigned,	" & _
                 "TO_DATE(dl.createddate,'mm/dd/yyyy') AS cdate, " & _
                 "TO_DATE(dr.assigneddate,'mm/dd/yyyy') AS adate,	" & _
                 "TO_DATE(dr.duedate,'mm/dd/yyyy') AS ddate,	" & _
                 "(CURRENT_DATE-dr.duedate) AS overduedays "
        Else
            s_sql = s_sql & "isnull(u.FirstName,'')+' '+isnull(u.LastName,'') AS author,	" & _
                 "isnull(a.FirstName,'')+' '+isnull(a.LastName,'') AS assigned,	" & _
                 "convert(varchar(10),dl.createddate,101) AS cdate, " & _
                 "convert(varchar(10),dr.assigneddate,101) AS adate,	" & _
                 "convert(varchar(10),dr.duedate,101) AS ddate,	" & _
                 "DateDiff(day,dr.duedate,getdate()) AS overduedays "
        End If
        

        s_sql = s_sql & "FROM doclist dl " & _
 "INNER JOIN users u	" & _
  "ON dl.createdby = u.userid	" & _
 "INNER JOIN doctype dt " & _
  "ON dl.doctype = dt.doctype	" & _
 "INNER JOIN GroupDocAccess G ON	" & _
  "g.groupId = '" & DocSession.sUserGroup & "' AND " & _
  "g.docId = dl.docType and g.GroupAccessId > 0 " & _
 "LEFT JOIN docrouting dr ON	" & _
  "dr.docid = dl.docid " & _
 "LEFT JOIN users a	" & _
  "ON dr.approverid = a.userid " & _
  "WHERE dl.statusid <> 5 "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate & "'"
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND dl.createddate <= '" & DocSession.rpt_EndDate & "'"
        End If
        If DocSession.rpt_ColValue <> "" Then
            s_sql = s_sql & " AND (u.firstname like '%" & DocSession.rpt_ColValue & "%' or u.lastname like '%" & DocSession.rpt_ColValue & "%') "
        End If

        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"

        End If

        dsOverDueDays.SelectCommand = s_sql


    End Sub

End Class