Imports Microsoft.Reporting.WebForms

Public Class DocumentListPerUser
    Inherits System.Web.UI.Page

    Private Sub rptUserDoc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(3) As ReportParameter
        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("Classification", DeriveClassification(), False)
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

        If Request.Cookies("udrUserDocReceivedFrom").Value <> "" OrElse Request.Cookies("udrUserDocReceivedTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrUserDocReceivedFrom").Value)
            lde = CDate(Request.Cookies("udrUserDocReceivedTo").Value)

            lsMF = MonthName(ldf.Month) & " " & CStr(Day(ldf))
            lsME = MonthName(lde.Month) & " " & CStr(Day(lde))
            If ldf.Year < lde.Year Then
                lsParam = "Received Date Range: " & lsMF & ", " & CStr(ldf.Year) & " - " & lsME & ", " & CStr(lde.Year)
            Else
                lsParam = "Received Date Range: " & lsMF & " - " & lsME & ", " & CStr(lde.Year)
            End If
        Else

            lsParam = "Received Date Range: -All-"
        End If



        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim s_sql As String

        If DocSession.OraClient Then
            s_sql = "SELECT " & _
"dr.docid,dr.seqno,u.FirstName,u.LastName, " & _
"dl.RefNo,dl.Title,convert(char(10),dr.AssignedDate,101) as AssignedDate,dr.Sender,convert(char(10),dr.ActionDate,101) as ActionDate,  " & _
"datediff(d,dr.assigneddate,isnull(dr.ActionDate,getdate())) as age,dr.remarks " & _
"FROM DocRouting dr " & _
"INNER JOIN DocList dl " & _
"	ON dr.DocId = dl.DocId and dl.statusid <> 5 " & _
"INNER JOIN Users u " & _
"	ON dr.ApproverId = u.UserId " & _
"WHERE dr.ApproverId = 'ADMIN' " & _
"order by u.firstname,u.lastname,dr.docid,dr.seqno "

        Else
            s_sql = "SELECT " &
"dr.approverid,dr.docid,dr.seqno,SenderName = ltrim(rtrim(su.LastName)) +', '+rtrim(ltrim(su.FirstName)),  " &
"ApproverName = ltrim(rtrim(su2.LastName)) +', '+rtrim(ltrim(su2.FirstName)),  " &
"dl.RefNo,dl.Title,AssignedDate=convert(char(10),dr.AssignedDate,101),RoutedDate=convert(char(10),dr.CompletedDate,101),ltrim(rtrim(isnull(su3.LastName,''))) +', '+rtrim(ltrim(isnull(su3.FirstName,''))) as Sender,ActionDate=convert(char(10),dr.ActionDate,101),Category=case when InternalDoc = 1 then 'Internal' else 'External' end,  " &
"dbo.CalcAge('" & DocSession.sUserGroup & "',5,dr.assigneddate,isnull(dr.CompletedDate,isnull(dl.CompletedDate,Getdate()))) as age,dr.remarks,dr.StatusId,DocAction=ds2.Description,UserAction=ds.Description,DStatus=case when (ds2.StatusId = 18 or ds2.StatusId=8 or ds2.StatusId = 19 or ds2.StatusId = 15 or ds2.StatusId = 12) then 'Completed/Closed' else 'Open' end  " &
"FROM DocRouting dr  " &
"INNER JOIN DocList dl  " &
"	ON dr.DocId = dl.DocId and dl.statusid <> 5 " &
"LEFT JOIN DocRouting dr2  " &
    "	ON dr2.SeqNo = dl.RoutingSeqNo " &
"INNER JOIN Users su  " &
"	ON dr.Sender = su.UserId " &
"INNER JOIN Users su2  " &
"	ON dr.ApproverId = su2.UserId " &
"LEFT JOIN Users su3 ON su3.userId = dr2.approverid " &
"INNER JOIN DocStatus ds " &
"	ON ds.StatusId = dr.StatusId " &
"INNER JOIN DocStatus ds2 " &
"	ON dl.StatusId = ds2.StatusId " &
"WHERE dr.ApproverId IN (" & Request.Cookies("udrUserDocUserList").Value & ") "

            If Request.Cookies("udrUserDocClassification").Value <> "" Then
                If Request.Cookies("udrUserDocClassification").Value = "0" Then
                    s_sql = s_sql & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                Else
                    s_sql = s_sql & " AND dl.InternalDoc = 1 "
                End If

            End If

            If Request.Cookies("udrUserDocReceivedFrom").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND trunc(dr.AssignedDate) >= TO_DATE('" & Request.Cookies("udrUserDocReceivedFrom").Value & "','mm/dd/yyyy')"
                Else
                    s_sql = s_sql & " AND dr.AssignedDate >= '" & Request.Cookies("udrUserDocReceivedFrom").Value & "' "
                End If

            End If
            If Request.Cookies("udrUserDocReceivedTo").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND trunc(dr.AssignedDate) <= TO_DATE('" & Request.Cookies("udrUserDocReceivedTo").Value & "','mm/dd/yyyy')"
                Else
                    s_sql = s_sql & " AND dr.AssignedDate < dateAdd(dd,1,'" & Request.Cookies("udrUserDocReceivedTo").Value & "') "
                End If

            End If

        End If

        s_sql = s_sql & " order by " & Request.Cookies("udrUserDocSortBy").Value & " " & Request.Cookies("udrUserDocSortOption").Value

        dsUserDocReceived.SelectCommand = s_sql

    End Sub
    Private Function DeriveClassification() As String

        Dim lsClass As String = "Classification: -All-"
        If Request.Cookies("udrUserDocClassification").Value <> "" Then
            If Request.Cookies("udrUserDocClassification").Value = "1" Then
                lsClass = "Classification: Internal"
            Else
                lsClass = "Classification: External"
            End If
        End If



        Return lsClass
    End Function
End Class