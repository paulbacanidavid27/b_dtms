Imports Microsoft.Reporting.WebForms

Public Class DocumentIndexStatus
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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

        Dim s_sql As String =
            "SELECT * FROM (select " & _
"distinct dl.refno, " & _
"dt.docname, " & _
"dl.title, " & _
"bureau = o.description, " & _
"indexed = case when div.docid is null then 'No' else 'Yes' end " & _
"from DocList dl  " & _
"inner join DocType dt " & _
"ON dl.DocType = dt.DocType " & _
"left join DocIndexValues div " & _
"on dl.docid = div.docid " & _
"LEFT JOIN Office o " & _
"on dl.OfficeCode = o.OfficeCode  " & _
"Where dl.statusid <> 5  "
        If Request.Cookies("disRefno").Value <> "" Then
            
            Dim sDocNo As String = Request.Cookies("disRefno").Value
            If sDocNo.IndexOf(",") > 0 Then
                s_sql = s_sql & " AND dl.refno in ('" & sDocNo.Replace(",", "','") & "') "
            Else
                s_sql = s_sql & " AND (dl.refno like '%" & sDocNo & "%') "
            End If

            'End If
        End If

        If Request.Cookies("disDocType").Value <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & Request.Cookies("disDocType").Value & "'"
        End If

        If Request.Cookies("disOfficeCode").Value <> "" Then
            s_sql = s_sql & " AND dl.OfficeCode =  '" & Request.Cookies("disOfficeCode").Value & "'"

        End If
        s_sql = s_sql & ") d "
        If Request.Cookies("disIndexed").Value <> "" Then
            s_sql = s_sql & " WHERE d.Indexed =  '" & Request.Cookies("disIndexed").Value & "'"

        End If
        s_sql = s_sql & " ORDER by d.bureau "

        SqlDataSource1.SelectCommand = s_sql

    End Sub

End Class