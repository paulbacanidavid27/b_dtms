Imports Microsoft.Reporting.WebForms

Public Class DocumentReceived
    Inherits System.Web.UI.Page

    Private Sub rptUserDoc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(4) As ReportParameter
        ReportViewer1.LocalReport.EnableExternalImages = True

        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("EmpName", Request.Cookies("udrURcvUserText").Value, False)
        params(2) = New ReportParameter("ReqType", DeriveReqType(), False)
        params(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        params(4) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

    Private Function DeriveLogo() As String
        Dim imagePath As String = New Uri(Server.MapPath("~/images/logo/" & DocSession.GroupLogo)).AbsoluteUri
        Return imagePath
    End Function

    Private Function DeriveReqType() As String

        Dim lsClass As String = "-All-"
        If Request.Cookies("udrURcvReqType").Value <> "" Then
            lsClass = Request.Cookies("udrURcvReqTypeDesc").Value
        End If
        Return lsClass
    End Function

    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String

        If Request.Cookies("udrURcvReceivedFrom").Value <> "" OrElse Request.Cookies("udrURcvReceivedTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udrURcvReceivedFrom").Value)
            lde = CDate(Request.Cookies("udrURcvReceivedTo").Value)

            lsMF = MonthName(ldf.Month) & " " & CStr(Day(ldf))
            lsME = MonthName(lde.Month) & " " & CStr(Day(lde))
            If ldf.Year < lde.Year Then
                lsParam = "Received Date Range: " & lsMF & ", " & CStr(ldf.Year) & " - " & lsME & ", " & CStr(lde.Year)
            Else
                lsParam = "Received Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
        Else

            lsParam = "Received Date Range: -All-"
        End If



        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim s_sql As String
            Try


                s_sql = "SELECT " &
    "dr.approverid,dr.docid,dr.seqno,SenderName = ltrim(rtrim(su.LastName)) +', '+rtrim(ltrim(su.FirstName)) +'/'+ltrim(rtrim(isnull(su3.LastName,''))) +', '+rtrim(ltrim(isnull(su3.FirstName,''))),rt.RequestDescription,  " &
    "dl.RefNo,dl.Title,AssignedDate=convert(char(10),dr.AssignedDate,101),RoutedDate=convert(char(10),dr.CompletedDate,101),dr.Sender,ActionDate=convert(char(10),dr.ActionDate,101),Category=ISNULL(rt.RequestDescription,''),  " &
    "case when rt.agecalc = 'R' then dbo.CalcAge('" & DocSession.sUserGroup & "',5,dr.assigneddate,isnull(dr.CompletedDate,isnull(dl.CompletedDate,Getdate()))) else datediff(d,dr.assigneddate,isnull(dr.CompletedDate,isnull(dl.CompletedDate,getdate()))) end as age,dr.remarks,dr.StatusId,DocAction=ds2.Description,UserAction=ds.Description,DStatus=case when (ds2.StatusId in (" & DocSession.CompleteStatus & ")) then 'Completed/Closed' else 'Open' end  " &
    "FROM DocRouting dr  " &
    "INNER JOIN DocList dl  " &
    "	ON dr.DocId = dl.DocId and dl.statusid <> 5 " &
    "Left JOIN DocRouting dr2  " &
    "	ON dr2.SeqNo = dl.RoutingSeqNo " &
    "LEFT JOIN DocRequestType rt  " &
    "	ON rt.RequestType = dl.RequestType " &
    "INNER JOIN Users su  " &
    "	ON dr.Sender = su.UserId " &
    "LEFT JOIN Users su3 ON su3.userId = dr2.approverid " &
    "INNER JOIN DocStatus ds " &
    "	ON ds.StatusId = dr.StatusId " &
    "INNER JOIN DocStatus ds2 " &
    "	ON dl.StatusId = ds2.StatusId " &
    "WHERE "
                If DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "D" Then
                    If Request.Cookies("udrURcvUser").Value <> "" Then
                        s_sql = s_sql & " dr.ApproverId = '" & Request.Cookies("udrURcvUser").Value & "' "
                    End If
                Else
                    s_sql = s_sql & " dr.ApproverId = '" & DocSession.sUserId & "' "
                End If
                If Request.Cookies("udrURcvClassification").Value <> "" Then
                    If Request.Cookies("udrURcvClassification").Value = "0" Then
                        s_sql = s_sql & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                    Else
                        s_sql = s_sql & " AND dl.InternalDoc = 1 "
                    End If

                End If

                If Request.Cookies("udrURcvReceivedFrom").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND trunc(dr.AssignedDate) >= TO_DATE('" & Request.Cookies("udrURcvReceivedFrom").Value & "','mm/dd/yyyy')"
                    Else
                        s_sql = s_sql & " AND dr.AssignedDate >= '" & Request.Cookies("udrURcvReceivedFrom").Value & "' "
                    End If

                End If
                If Request.Cookies("udrURcvReceivedTo").Value <> "" Then
                    If DocSession.OraClient Then
                        s_sql = s_sql & " AND trunc(dr.AssignedDate) <= TO_DATE('" & Request.Cookies("udrURcvReceivedTo").Value & "','mm/dd/yyyy')"
                    Else
                        s_sql = s_sql & " AND dr.AssignedDate < dateadd(dd,1,'" & Request.Cookies("udrURcvReceivedTo").Value & "') "
                    End If

                End If



                If Request.Cookies("udrURcvActionTaken").Value <> "" AndAlso Request.Cookies("udrURcvActionTaken").Value <> "0" Then
                    s_sql = s_sql & " AND dr.statusid =  " & Request.Cookies("udrURcvActionTaken").Value
                End If

                If Request.Cookies("udrURcvStatus").Value = "C" Then
                    s_sql = s_sql & " AND dl.statusid in (" & DocSession.CompleteStatus & ")"
                ElseIf Request.Cookies("udrURcvStatus").Value = "O" Then
                    s_sql = s_sql & " AND dl.statusid not in (" & DocSession.CompleteStatus & ")"
                End If
                If Request.Cookies("udrURcvTitle").Value <> "" Then
                    s_sql = s_sql & " AND dl.title like '%" & Request.Cookies("udrURcvTitle").Value & "%'"
                End If

                If Request.Cookies("udrURcvReqType").Value <> "" Then

                    s_sql = s_sql & " AND dl.requesttype = '" & Request.Cookies("udrURcvReqType").Value & "' "

                End If

                If Request.Cookies("udrURcvSender").Value <> "" Then

                    s_sql = s_sql & " AND dl.docsender like '%" & Request.Cookies("udrURcvSender").Value & "%' "

                End If



                s_sql = s_sql & " order by " & Request.Cookies("udrURcvSortBy").Value & " " & Request.Cookies("udrURcvSortOption").Value

                dsUserDocReceived.SelectCommand = s_sql
            Catch ex As Exception
                Throw New Exception(ex.Message)

            Finally
                If Not dsUserDocReceived Is Nothing Then
                    dsUserDocReceived.Dispose()
                End If

            End Try

        End If
    End Sub
End Class