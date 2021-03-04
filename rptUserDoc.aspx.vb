Imports Microsoft.Reporting.WebForms

Public Class rptUserDoc
    Inherits System.Web.UI.Page

    Private Sub rptUserDoc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(1) As ReportParameter


        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("Classification", DeriveClassification(), False)

        Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            Dim s_sql As String

            If DocSession.OraClient Then
                s_sql = "SELECT " & _
    "dr.docid,dr.seqno,u.FirstName,u.LastName, " & _
    "dl.RefNo,dl.Title,convert(char(10),dr.AssignedDate,101) as AssignedDate,dr.Sender,convert(char(10),dr.ActionDate,101) as ActionDate,  " & _
    "datediff(d,dr.assigneddate,isnull(dr.ActionDate,getdate())) as age,dr.remarks " & _
    "FROM DocRouting dr " & _
    "INNER JOIN DocList dl " & _
    "	ON dr.DocId = dl.DocId " & _
    "INNER JOIN Users u " & _
    "	ON dr.ApproverId = u.UserId " & _
    "WHERE dr.ApproverId = 'ADMIN' " & _
    "order by u.firstname,u.lastname,dr.docid,dr.seqno "

            Else
                s_sql = "select  " & _
    "u.LastName+', '+u.FirstName as Personnel " & _
    ",dl.RefNo " & _
    ",dl.Title " & _
    ",dl.CreatedDate as DateReceived " & _
    ",isnull(convert(char(10),dr.ActionDate,101),'') as DateAcknowledged " & _
    ",isnull(convert(char(10),Isnull(dl.ApprovedDate,dr.ActionDate),101),'') as LastActionDate " & _
    ",dl.StatusId " & _
    ",ds.Description as LastActionStatus " & _
    ",case when dl.statusid In (" & DocSession.CompleteStatus & ") then isnull(convert(char(10),Isnull(dl.ApprovedDate,dr.ActionDate),101),'') else 'N/A' end as CompletedDate " & _
    ",case when dl.statusid In (" & DocSession.CompleteStatus & ") then 'Completed/Closed'  else 'Open' end as statusdesc " & _
    ",DATEDIFF(d,Isnull(dr.ActionDate,getdate()),case when dl.statusid In (" & DocSession.CompleteStatus & ") then isnull(dl.ApprovedDate,dr.ActionDate) else GETDATE() end) AS Age " & _
    "from DocList dl " & _
    "            inner Join " & _
    "	( select  " & _
    "            ROW_NUMBER() " & _
    "            over " & _
    "				( " & _
    "					partition by dr1.docid " & _
    "					order by dr1.docid,dr1.seqno " & _
    "				) AS RN,dr1.DocId,dr1.seqno,dr1.ActionDate,dr1.ApproverId " & _
    "		From DocRouting dr1 " & _
    "	) dr " & _
    "ON dl.DocId =  dr.DocId and dr.RN = 1 " & _
    "inner join DocStatus ds " & _
    "on dl.StatusId =  ds.StatusId and dl.statusid <> 5 " & _
    "inner join Users u " & _
    "on dr.ApproverId =  u.UserId " & _
    "WHERE dr.ApproverId IN (" & Request.Cookies("udrUserDocUserList").Value & ") "
            End If

            If Request.Cookies("udrUserDocClassification").Value <> "" Then
                If Request.Cookies("udrUserDocClassification").Value = "0" Then
                    s_sql = s_sql & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                Else
                    s_sql = s_sql & " AND dl.InternalDoc = 1 "
                End If

            End If

            If Request.Cookies("udrUserDocReceivedFrom").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND trunc(dl.createddate) >= TO_DATE('" & Request.Cookies("udrUserDocReceivedFrom").Value & "','mm/dd/yyyy')"
                Else
                    s_sql = s_sql & " AND dl.createddate >= '" & Request.Cookies("udrUserDocReceivedFrom").Value & "' "
                End If

            End If
            If Request.Cookies("udrUserDocReceivedTo").Value <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND trunc(dl.createddate) <= TO_DATE('" & Request.Cookies("udrUserDocReceivedTo").Value & "','mm/dd/yyyy')"
                Else
                    s_sql = s_sql & " AND dl.createddate <= '" & Request.Cookies("udrUserDocReceivedTo").Value & "' "
                End If

            End If





            s_sql = s_sql & " order by " & Request.Cookies("udrUserDocSortBy").Value & " " & Request.Cookies("udrUserDocSortOption").Value

            dsUserDoc.SelectCommand = s_sql
        End If

    End Sub
End Class