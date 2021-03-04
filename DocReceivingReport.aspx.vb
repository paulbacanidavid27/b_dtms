Imports Microsoft.Reporting.WebForms
Public Class DocReceivingReport
    Inherits System.Web.UI.Page
    Dim sqlwhere1 As String = ""
    Dim sqlwhere2 As String = ""
    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        
    End Sub
    Private Sub SetParameters()
        Dim parms(10) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        parms(0) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
        parms(1) = New ReportParameter("ImgPath", DeriveLogo(), False)
        parms(2) = New ReportParameter("DateRange", DeriveDateRange, False)
        parms(3) = New ReportParameter("DeliveredBy", tbDeliveredBy.Text, False)
        parms(4) = New ReportParameter("PreparedBy", tbPreparedBy.Text, False)
        parms(5) = New ReportParameter("PreparedByPosition", tbPreparedByDesignation.Text, False)
        parms(6) = New ReportParameter("ReviewedBy", tbReviewedBy.Text, False)
        parms(7) = New ReportParameter("ReviewedByPosition", tbReviewedByDesignation.Text, False)
        parms(8) = New ReportParameter("SubmittedBy", tbSubmittedBy.Text, False)
        parms(9) = New ReportParameter("SubmittedByPosition", tbSubmittedByDesignation.Text, False)
        parms(10) = New ReportParameter("OfficeAddress", "General Solano St, San Miguel, Manila", False)
        Me.ReportViewer1.LocalReport.SetParameters(parms)
    End Sub

    Private Function DeriveDateRange() As String

        Dim lsParam As String = ""
        
        lsParam = dlMonth.SelectedItem.Text & " " & tbYear.Text
            
        

        Return lsParam
    End Function
    Private Function DeriveLogo() As String

        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadCookies()
            Select12()
            SetParameters()
            If sqlwhere1 <> "" Then
                LoadReport()
                lmsg.Text = ""
                Me.ReportViewer1.Visible = True
            Else
                lmsg.Text = "No records found!"
                Me.ReportViewer1.Visible = False
            End If

        End If
    End Sub
    Private Sub LoadReport()


        Try
            Dim s_sql As String

            s_sql = "select remarks, rcvdate,subtotalrecv,totalrcv from (SELECT " & _
                " case dmr.remarks when 'Late' then 2 when 'On Time' then 1 else 3 end as OrderNo, " & _
                "convert(char(10),dmr.CreatedDate,101) as rcvdate,dmr.remarks, isnull(COUNT(*),0) as subtotalrecv " & _
                ",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where convert(char(10),d.CreatedDate,101) = convert(char(10),dmr.CreatedDate,101) and (d.remarks = 'Late' or d.remarks = 'On Time')) " & _
                "FROM docmonitoringreceiving dmr " & _
                " WHERE convert(char(10),dmr.CreatedDate,101) in (" & sqlwhere1 & ") " & _
                "GROUP BY convert(char(10),dmr.CreatedDate,101),dmr.remarks) a order by orderno "

            ds_reportdata.SelectCommand = s_sql
            If sqlwhere2 <> "" Then
                s_sql = "select remarks, rcvdate,subtotalrecv,totalrcv from (SELECT " & _
                " case dmr.remarks when 'Late' then 2 when 'On Time' then 1 else 3 end as OrderNo, " & _
                "convert(char(10),dmr.CreatedDate,101) as rcvdate,dmr.remarks, isnull(COUNT(*),0) as subtotalrecv " & _
                ",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where convert(char(10),d.CreatedDate,101) = convert(char(10),dmr.CreatedDate,101) and (d.remarks = 'Late' or d.remarks = 'On Time')) " & _
                "FROM docmonitoringreceiving dmr " & _
                " WHERE convert(char(10),dmr.CreatedDate,101) in (" & sqlwhere2 & ") " & _
                "GROUP BY convert(char(10),dmr.CreatedDate,101),dmr.remarks ) a order by orderno "
            Else
                s_sql = "SELECT " & _
                "convert(char(10),dmr.CreatedDate,101) as rcvdate,dmr.remarks, COUNT(*) as subtotalrecv " & _
                ",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where convert(char(10),d.CreatedDate,101) = convert(char(10),dmr.CreatedDate,101) and (d.remarks = 'Late' or d.remarks = 'On Time')) " & _
                "FROM docmonitoringreceiving dmr " & _
                " WHERE receivingid = 0 " & _
                "GROUP BY convert(char(10),dmr.CreatedDate,101),dmr.remarks "
            End If
            ds_reportdata2.SelectCommand = s_sql

            'summary
            s_sql = "select remarks, subtotalrecv,totalrcv from (SELECT " & _
                " case dmr.remarks when 'Late' then 2 when 'On Time' then 1 else 3 end as OrderNo, " & _
                " dmr.remarks, isnull(COUNT(*),0) as subtotalrecv " & _
                ",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where d.[Month] = " & dlMonth.SelectedValue & " And d.[Year] = " & tbYear.Text & " and (d.remarks = 'Late' or d.remarks = 'On Time')) " & _
                " FROM docmonitoringreceiving dmr " & _
                " where dmr.[Month] = " & dlMonth.SelectedValue & " And dmr.[Year] = " & tbYear.Text & _
                " GROUP BY dmr.remarks ) a order by orderno  "
            ds_reportdatasummary.SelectCommand = s_sql

            'graph
            s_sql = " select remarks,subtotalrecv from ( " & _
                    " select case remarks when 'Late' then 2 else 1 end as OrderNo, " & _
                    " case remarks when 'Late' then 'Meet Target(No)' else 'Meet Target(Yes)' end as remarks,totalrcv, " & _
                    " Convert(integer,round((convert(decimal,subtotalrecv)/totalrcv) * 100,0)) as subtotalrecv   " & _
                    " from (select  " & _
"dmr.remarks, COUNT(receivingid) as subtotalrecv " & _
",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where (d.remarks = 'Late' or d.remarks = 'On Time')  " & _
" and [Month] = " & dlMonth.SelectedValue & " And [Year] = " & tbYear.Text & ") " & _
"from docmonitoringreceiving dmr " & _
            " where [Month] = " & dlMonth.SelectedValue & " And [Year] = " & tbYear.Text & _
            " and (dmr.remarks = 'Late' or dmr.remarks = 'On Time') " & _
" group by dmr.remarks) b " & _
" UNION ALL " & _
" select 3 as OrderNo,'Total Docs Delivered By CRD' as remarks, count(receivingid) as totalrcv,100 as subtotalrecv " & _
"from docmonitoringreceiving dmr " & _
            " where [Month] = " & dlMonth.SelectedValue & " And [Year] = " & tbYear.Text & _
            " and (dmr.remarks = 'Late' or dmr.remarks = 'On Time') " & _
"  ) c order by orderno "

            ds_reportdatagraphsummary.SelectCommand = s_sql
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Function Select12() As String
        Dim clsData As clsDocMonitoringReceiving
        Dim ldata As DataTable
        Try
            clsData = New clsDocMonitoringReceiving
            clsData.pYearFrom = tbYear.Text
            clsData.pMonth = dlMonth.SelectedValue
            ldata = clsData.RetrieveMonitoringReceiving()
            Dim lctr As Integer = 1
            For Each ldr As DataRow In ldata.Rows
                If ldr(0).ToString <> "" Then
                    If lctr < 13 Then
                        If sqlwhere1 = "" Then

                            sqlwhere1 = "'" & ldr(0) & "'"
                        Else
                            sqlwhere1 = sqlwhere1 & ",'" & ldr(0) & "'"
                        End If
                    Else
                        If sqlwhere2 = "" Then
                            sqlwhere2 = "'" & ldr(0) & "'"
                        Else
                            sqlwhere2 = sqlwhere2 & ",'" & ldr(0) & "'"
                        End If
                    End If
                    lctr = lctr + 1
                End If

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
            End If
        End Try

    End Function
    Protected Sub btPreview_Click(sender As Object, e As EventArgs) Handles btPreview.Click
        Select12()
        SetCookies()
        SetParameters()
        LoadReport()
    End Sub
    Private Sub SetCookies()
        Dim mycookie As HttpCookie = New HttpCookie("ckRecvRptMonth")
        mycookie.Value = dlMonth.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptYear")
        mycookie.Value = tbYear.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptDeliveredBy")
        mycookie.Value = tbDeliveredBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptPreparedBy")
        mycookie.Value = tbPreparedBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptPreparedByDesignation")
        mycookie.Value = tbPreparedByDesignation.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptReviewedBy")
        mycookie.Value = tbReviewedBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptReviewedByDesignation")
        mycookie.Value = tbReviewedByDesignation.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptSubmittedBy")
        mycookie.Value = tbSubmittedBy.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("ckRecvRptSubmittedByDesignation")
        mycookie.Value = tbSubmittedByDesignation.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        'Response.Redirect("PendingApprovalList.aspx")
    End Sub
    Private Sub LoadCookies()
        If Not Request.Cookies("ckRecvRptMonth") Is Nothing Then
            If Request.Cookies("ckRecvRptMonth").Value <> "" Then
                dlMonth.SelectedValue = Request.Cookies("ckRecvRptMonth").Value
            Else
                dlMonth.SelectedValue = Month(DateTime.Now).ToString
            End If
        Else
            dlMonth.SelectedValue = Month(DateTime.Now).ToString
        End If
        If Not Request.Cookies("ckRecvRptYear") Is Nothing Then
            If Request.Cookies("ckRecvRptYear").Value <> "" Then
                tbYear.Text = Request.Cookies("ckRecvRptYear").Value
            Else
                tbYear.Text = Year(DateTime.Now).ToString
            End If
        Else
            tbYear.Text = Year(DateTime.Now).ToString
        End If
        If Not Request.Cookies("ckRecvRptDeliveredBy") Is Nothing Then
            If Request.Cookies("ckRecvRptDeliveredBy").Value <> "" Then
                tbDeliveredBy.Text = Request.Cookies("ckRecvRptDeliveredBy").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptPreparedBy") Is Nothing Then
            If Request.Cookies("ckRecvRptPreparedBy").Value <> "" Then
                tbPreparedBy.Text = Request.Cookies("ckRecvRptPreparedBy").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptPreparedByDesignation") Is Nothing Then
            If Request.Cookies("ckRecvRptPreparedByDesignation").Value <> "" Then
                tbPreparedByDesignation.Text = Request.Cookies("ckRecvRptPreparedByDesignation").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptReviewedBy") Is Nothing Then
            If Request.Cookies("ckRecvRptReviewedBy").Value <> "" Then
                tbReviewedBy.Text = Request.Cookies("ckRecvRptReviewedBy").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptReviewedByDesignation") Is Nothing Then
            If Request.Cookies("ckRecvRptReviewedByDesignation").Value <> "" Then
                tbReviewedByDesignation.Text = Request.Cookies("ckRecvRptReviewedByDesignation").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptSubmittedBy") Is Nothing Then
            If Request.Cookies("ckRecvRptSubmittedBy").Value <> "" Then
                tbSubmittedBy.Text = Request.Cookies("ckRecvRptSubmittedBy").Value
            End If
        End If
        If Not Request.Cookies("ckRecvRptSubmittedByDesignation") Is Nothing Then
            If Request.Cookies("ckRecvRptSubmittedByDesignation").Value <> "" Then
                tbSubmittedByDesignation.Text = Request.Cookies("ckRecvRptSubmittedByDesignation").Value
            End If
        End If
    End Sub
End Class