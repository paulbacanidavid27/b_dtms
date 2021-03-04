Imports Microsoft.Reporting.WebForms
Public Class DocumentIndex
    Inherits System.Web.UI.Page
    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(1) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True

        params(0) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(1) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim s_sql As String = "SELECT  " & _
        "dl.docid " & _
        ",dl.title " & _
        ",dt.docname " & _
        ",dl.filename " & _
        ",u.FirstName+''+u.LastName AS originator " & _
        ",ds.description AS statusdesc " & _
        ",dl.createddate AS created_date,di.columnName " & _
        ",div.colValue "
        If DocSession.OraClient Then
            s_sql = s_sql & ",TO_DATE(dl.CreatedDate,'mm/dd/yyyy')  "
        Else
            s_sql = s_sql & ",convert(char(10),dl.CreatedDate,101)  "
        End If

        s_sql = s_sql & "FROM doclist dl  " & _
        "INNER JOIN docType dt  " & _
         "ON dl.doctype = dt.docType  " & _
        "INNER JOIN docIndexValues div  " & _
         "ON dl.docId = div.docId and  " & _
          "dl.docType = div.docType "

        If DocSession.rpt_ColValue <> "" Then
            s_sql = s_sql & " AND " & DocSession.rpt_ColValue & ""
        End If

        s_sql = s_sql + " INNER JOIN docIndex di  " & _
 "ON div.columnId = di.ColumnId and  " & _
  "div.docType = di.docType  " & _
"INNER JOIN users u  " & _
 "ON u.userId = dl.createdby  " & _
"INNER JOIN GroupDocAccess G  " & _
 "ON g.groupId = '" & DocSession.sUserGroup & "' and  " & _
  "g.docId = dl.docType and g.GroupAccessId > 0  " & _
"INNER JOIN DocStatus DS  " & _
 "ON dl.statusid=ds.statusid  " & _
"WHERE dl.Docid is not null "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND dl.createddate >= '" & DocSession.rpt_StartDate & "'"
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND dl.createddate < DateAdd(day,1,'" & DocSession.rpt_EndDate & "')"
        End If

        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"

        End If
        ds1.SelectCommand = s_sql
    End Sub

End Class