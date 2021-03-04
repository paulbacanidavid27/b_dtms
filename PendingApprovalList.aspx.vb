Imports Microsoft.Reporting.WebForms
Public Class PendingApprovalList
    Inherits System.Web.UI.Page
    Private Sub DocumentUploaded_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Not IsPostBack Then
        Dim parms(2) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        parms(0) = New ReportParameter("ImgPath", DeriveLogo(), False)
        parms(1) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
        parms(2) = New ReportParameter("DateRange", DeriveDateRange, False)
        Me.ReportViewer1.LocalReport.SetParameters(parms)


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
        
        Dim lsParam As String = ""
        If Not Request.Cookies("s_pal_datestart") Is Nothing AndAlso Not Request.Cookies("s_pal_dateend") Is Nothing Then
            If Request.Cookies("s_pal_datestart").Value <> "" AndAlso Request.Cookies("s_pal_dateend").Value <> "" Then

                lsParam = "Upload Date Range: " & Request.Cookies("s_pal_datestart").Value & " - " & Request.Cookies("s_pal_dateend").Value
                tbFrom.Text = Request.Cookies("s_pal_datestart").Value
                tbTo.Text = Request.Cookies("s_pal_dateend").Value
            ElseIf Request.Cookies("s_pal_datestart").Value <> "" AndAlso Request.Cookies("s_pal_dateend").Value = "" Then
                lsParam = "Upload Date Range: " & Request.Cookies("s_pal_datestart").Value & " to present. "
                tbFrom.Text = Request.Cookies("s_pal_datestart").Value
                tbTo.Text = ""
            ElseIf Request.Cookies("s_pal_dateend").Value <> "" AndAlso Request.Cookies("s_pal_datestart").Value = "" Then
                lsParam = "Upload Date Range: Up to " & Request.Cookies("s_pal_dateend").Value
                tbFrom.Text = ""
                tbTo.Text = Request.Cookies("s_pal_dateend").Value
            Else
                lsParam = "Upload Date Range: -All-"
                tbFrom.Text = ""
                tbTo.Text = ""
            End If
        Else
            lsParam = "Upload Date Range: -All-"
            tbFrom.Text = ""
            tbTo.Text = ""
        End If

        'End If

        Return lsParam
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadReport()
        End If

    End Sub
    
    
    Private Sub LoadReport()
        Dim s_sql As String = "SELECT  isnull(dl.refno,'') AS refno , " & _
"isnull(S.FirstName,'') + ' ' + isnull(S.LastName,'') as approvername ," & _
"cc = isnull(da.CarbonCopy,0) ,urgent = isnull(da.Urgent,0) ," & _
"ReceivedDate = isnull(convert(varchar,dl.ReceivedDate),'') ," & _
"ReceivedBy = isnull(dl.ReceivedBy,'')  ," & _
"assigneddate = isnull(da.assigneddate,'01/01/1900') ," & _
            "dl.Title " & _
"FROM (" & _
"Select rno = ROW_NUMBER() OVER (partition by dr.docid order by dr.seqno)," & _
"dr.statusid,dr.DocId,dr.ApproverId,dr.sender ,dr.remarks,dr.seqno,dr.OutStatusId,dr.assigneddate,dr.Urgent,dr.CarbonCopy " & _
"from docrouting dr inner join doclist dl on dr.DocId = dl.DocId and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
") da  " & _
"INNER JOIN doclist dl ON da.docId = dl.docId and dl.StatusId > 0  " & _
"and (dl.Confidential is null or dl.Confidential = 0) and dl.CreatedBy = '" & DocSession.sUserId & "' " & _
"INNER JOIN docStatus ds ON ds.StatusId = dl.StatusId " & _
"LEFT JOIN docStatus ds2 ON ds2.StatusId = da.OutStatusId " & _
"INNER JOIN users u ON dl.CreatedBy = u.userid " & _
"LEFT JOIN users s ON da.ApproverId = s.userid " & _
"WHERE da.rno = 1 and da.statusid = 2 and dl.statusid <> 5 "
        If tbFrom.Text.Trim <> "" AndAlso tbTo.Text.Trim <> "" Then
            s_sql = s_sql & " and da.AssignedDate >= '" & tbFrom.Text.Trim & "'  and da.AssignedDate < DateAdd(d,1,'" & tbTo.Text.Trim & "') "
        ElseIf tbFrom.Text.Trim <> "" Then
            s_sql = s_sql & " and da.AssignedDate >= '" & tbFrom.Text.Trim & "' "
        ElseIf tbTo.Text.Trim <> "" Then
            s_sql = s_sql & " and da.AssignedDate < DateAdd(d,1,'" & tbTo.Text.Trim & "') "
        End If

        s_sql = s_sql & " ORDER BY isnull(S.FirstName,'') + ' ' + isnull(S.LastName,''),da.AssignedDate  Desc"

        's_sql = s_sql & " SELECT ORDER BY u.Firstname,u.LastName,dt.docname,convert(datetime,convert(varchar(10),dl.createddate,101)) "
        ds_reportdata.SelectCommand = s_sql

    End Sub

    Private Sub btPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPreview.Click
        Dim mycookie As HttpCookie = New HttpCookie("s_pal_datestart")
        mycookie.Value = tbFrom.Text.Trim
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("s_pal_dateend")
        mycookie.Value = tbTo.Text.Trim
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        Response.Redirect("PendingApprovalList.aspx")

    End Sub
End Class