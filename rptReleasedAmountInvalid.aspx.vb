Imports Microsoft.Reporting.WebForms

Public Class rptReleasedAmountInvalid
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Dim params(1) As ReportParameter
        'params(0) = New ReportParameter("DateTimeRange", DeriveDateRange(), False)
        'params(1) = New ReportParameter("DeliveredBy", DocSession.sUserName, False)
        'params(2) = New ReportParameter("TimeRange", "Time", False)

        'Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim s_sql As String =
            "select dt.docname,dl.refno,bureau=o.description,description=dl.title,amount= replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')',''),dateissued=div2.colvalue " & _
"from doclist dl " & _
"inner join docindex di2 " & _
"on dl.doctype = di2.doctype and di2.columnname ='Date Issued' " & _
"inner join docindexvalues div2 " & _
"on div2.docid = dl.docid  " & _
"and div2.doctype = dl.doctype " & _
"and div2.columnid = di2.columnid " & _
"inner join doctype dt " & _
"on dl.doctype = dt.doctype " & _
"inner join office o " & _
"on o.officecode = dl.officecode " & _
"inner join docindex di " & _
"on dl.doctype = di.doctype and di.columnname ='amount' " & _
"inner join docindexvalues div " & _
"on div.docid = dl.docid  " & _
"and div.doctype = dl.doctype " & _
"and div.columnid = di.columnid " & _
"and  " & _
"(div.colvalue = '' or len(replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')','')) > 14 or isnumeric(replace(replace(replace(replace(replace(div.colvalue,'p',''),',',''),' ',''),'(',''),')','')) = 0) " & _
" where dl.statusid <> 5 "

        If DocSession.rpt_DocType.Trim <> "" Then
            s_sql = s_sql & " AND dl.DocType =  '" & DocSession.rpt_DocType & "'"
        End If

        If DocSession.rpt_OfficeCode.Trim <> "" Then
            s_sql = s_sql & " AND d.OfficeCode =  '" & DocSession.rpt_OfficeCode.Trim & "'"
        Else
            s_sql = s_sql & " AND dl.OfficeCode IN (SELECT DISTINCT officecode FROM groups WHERE maingroupid = 'OG') "

        End If
        s_sql = s_sql & " order by dl.title"


        SqlDataSource1.SelectCommand = s_sql

    End Sub
    Private Function DeriveDateRange() As String

        Dim lsParam As String
        lsParam = "Date Range: " & DocSession.rpt_StartDate & " - " & DocSession.rpt_EndDate

        Return lsParam
    End Function
End Class