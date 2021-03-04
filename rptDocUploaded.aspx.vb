Public Class rptDocUploaded
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s_sql As String = "SELECT " & _
            "dt.docname, "

        If DocSession.OraClient Then
            s_sql = s_sql & "NVL(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                    "TO_DATE(dl.createddate,'mm/dd/yyyy') AS cdate, "
        Else
            s_sql = s_sql & "isnull(u.FirstName,'')+' '+isnull(u.LastName,'') AS author, " & _
                    "convert(varchar(10),dl.createddate,101) AS cdate, "
        End If
        
        s_sql = s_sql & "count(dl.docId) AS totalcount " & _
"FROM doclist dl " & _
 "INNER JOIN users u " & _
  "ON dl.createdby = u.userid " & _
 "INNER JOIN doctype dt " & _
  "ON dl.doctype = dt.doctype " & _
 "INNER JOIN GroupDocAccess G ON " & _
  "g.groupId = '" & DocSession.sUserGroup & "' AND " & _
  "g.docId = dl.docType and g.GroupAccessId > 0 " & _
  "WHERE dl.statusid <> 5 "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate & "'"
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND dl.createddate < DateAdd(day,1,'" & DocSession.rpt_EndDate & "')"
        End If
        If DocSession.rpt_ColValue <> "" Then
            s_sql = s_sql & " AND (u.firstname like '%" & DocSession.rpt_ColValue & "%' or u.lastname like '%" & DocSession.rpt_ColValue & "%') "
        End If

        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"

        End If
        If DocSession.rpt_OfficeCode.Trim <> "" Then
            s_sql = s_sql & " AND dl.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"

        End If
        s_sql = s_sql & " GROUP BY u.Firstname,u.LastName,dt.docname,"
        If DocSession.OraClient Then
            s_sql = s_sql & "TO_DATE(dl.createddate,'mm/dd/yyyy') "
        Else
            s_sql = s_sql & "convert(varchar(10),dl.createddate,101) "
        End If

            ds_reportdata.SelectCommand = s_sql

    End Sub

End Class