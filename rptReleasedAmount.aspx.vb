Imports Microsoft.Reporting.WebForms

Public Class rptReleasedAmount
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(3) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("DateTimeRange", DeriveDateRange(), False)
        params(1) = New ReportParameter("DocType", DeriveDocType, False)
        'params(2) = New ReportParameter("TimeRange", "Time", False)

        params(2) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(3) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveLogo() As String
        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim s_sql As String =
        "select dl.officecode,amt= replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')',''), drange = div2.colvalue " & _
"into #temptble from doclist dl " & _
"inner join docindex di " & _
"on dl.doctype = di.doctype and di.columnname ='amount' " & _
"inner join docindexvalues div " & _
"on div.docid = dl.docid " & _
"and div.doctype = dl.doctype " & _
"and div.columnid = di.columnid " & _
"and isnumeric(div.colvalue) = 1 " & _
"and div.colvalue <> '' and len(replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')','')) < 15 " & _
"inner join docindex di2 " & _
"on dl.doctype = di2.doctype and di2.columnname ='Date Issued' " & _
"inner join docindexvalues div2 " & _
"on div2.docid = dl.docid " & _
"and div2.doctype = dl.doctype " & _
"and div2.columnid = di2.columnid and IsDate(div2.colvalue) = 1 " & _
" where dl.statusid <> 5 "

        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"
        End If

        If DocSession.rpt_OfficeCode.Trim <> "" Then
            s_sql = s_sql & " AND dl.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"
        Else
            s_sql = s_sql & " AND dl.OfficeCode IN (SELECT DISTINCT officecode FROM groups WHERE maingroupid = 'OG') "
        End If

        s_sql = s_sql & " select o.description,amount = sum(convert(money,d.amt)) " & _
"from #temptble  d " & _
"inner join office o " & _
"on o.officeCode = d.officeCode " & _
"where isnumeric(d.amt) = 1 "

        If DocSession.rpt_StartDate <> "" Then
            s_sql = s_sql & " AND d.drange >= '" & DocSession.rpt_StartDate & "' "
        End If
        If DocSession.rpt_EndDate <> "" Then
            s_sql = s_sql & " AND d.drange < dateadd(day,1,'" & DocSession.rpt_EndDate & "') "
        End If


        s_sql = s_sql & " group by o.description drop table #temptble "


        SqlDataSource1.SelectCommand = s_sql

    End Sub
    Private Function DeriveDateRange() As String

        Dim lsParam As String
        If DocSession.rpt_EndDate.Trim = "" Then
            DocSession.rpt_EndDate = Today.ToString("mm/dd/yyyy")
        End If
        lsParam = "Date Issued: " & DocSession.rpt_StartDate & " - " & DocSession.rpt_EndDate

        Return lsParam
    End Function
    Private Function DeriveDocType() As String

        Dim lsParam As String
        If DocSession.rpt_DocType.Trim = "" Then
            lsParam = "Document Type: All"
        Else
            lsParam = "Document Type: " & DocSession.rpt_DocType
        End If

        Return lsParam
    End Function

End Class