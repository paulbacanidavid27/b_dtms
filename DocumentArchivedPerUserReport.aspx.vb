Imports Microsoft.Reporting.WebForms

Public Class DocumentArchivedPerUserReport
    Inherits System.Web.UI.Page

    Private Sub rptUserDoc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim params(4) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        params(1) = New ReportParameter("Classification", DeriveClass(), False)
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
        If Request.Cookies("udapUserDocReqType").Value <> "" Then
            lsClass = Request.Cookies("udapUserDocReqTypeDesc").Value
        End If
        Return lsClass
    End Function

    Private Function DeriveClass() As String

        Dim lsClass As String = "-All-"
        If Request.Cookies("udapClassification").Value <> "" Then
            lsClass = Request.Cookies("udapClassificationDesc").Value
        End If
        Return lsClass
    End Function

    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String

        If Request.Cookies("udapArchivedFrom").Value <> "" OrElse Request.Cookies("udapArchivedTo").Value <> "" Then
            ldf = CDate(Request.Cookies("udapArchivedFrom").Value)
            lde = CDate(Request.Cookies("udapArchivedTo").Value)

            lsMF = MonthName(ldf.Month) & " " & CStr(Day(ldf))
            lsME = MonthName(lde.Month) & " " & CStr(Day(lde))
            If ldf.Year < lde.Year Then
                lsParam = "Archived Date Range: " & lsMF & ", " & CStr(ldf.Year) & " - " & lsME & ", " & CStr(lde.Year)
            Else
                lsParam = "Archived Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
            End If
        Else

            lsParam = "Archived Date Range: -All-"
        End If



        Return lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            Dim s_sql As String
            Try
                s_sql = "select dl.RefNo,dl.Title,CreatedDate = convert(char(10),dl.CreatedDate,101),dl.CreatedBy,dl.ArchivedBy,ArchivedDate = convert(char(10),dl.ArchivedDate,101),ab.FirstName+' '+ab.LastName as Archiver, " & _
  "cb.FirstName+' '+cb.LastName as Author, " & _
  "case when rt.agecalc = 'R' " & _
  "then dbo.CalcAge('" & DocSession.sUserGroup & "',5,dl.createddate,isnull(dl.ArchivedDate,Getdate())) " & _
  "else datediff(d,dl.createddate,isnull(dl.ArchivedDate,getdate())) " & _
  "end as age " & _
  ",rt.RequestDescription, " & _
  "dl.InternalDoc from DocList dl " & _
"inner join Users ab On " & _
                "dl.ArchivedBy = ab.UserId " & _
"inner join Users cb On " & _
                "dl.CreatedBy = cb.UserId " & _
"left join DocRequestType rt " & _
"on dl.RequestType = rt.RequestType " & _
"where dl.StatusId = 8 and dl.archivedby IN (" & Request.Cookies("udapUserDocUserList").Value & ") "

                If Request.Cookies("udapClassification").Value <> "" Then
                    If Request.Cookies("udapClassification").Value = "0" Then
                        s_sql = s_sql & " AND (dl.InternalDoc is null or dl.InternalDoc = 0) "
                    Else
                        s_sql = s_sql & " AND dl.InternalDoc = 1 "
                    End If

                End If

                If Request.Cookies("udapArchivedFrom").Value <> "" Then
                    s_sql = s_sql & " AND dl.ArchivedDate >= '" & Request.Cookies("udapArchivedFrom").Value & "' "

                End If
                If Request.Cookies("udapArchivedTo").Value <> "" Then

                    s_sql = s_sql & " AND dl.ArchivedDate < dateadd(dd,1,'" & Request.Cookies("udapArchivedTo").Value & "') "

                End If




                If Request.Cookies("udapTitle").Value <> "" Then
                    s_sql = s_sql & " AND dl.title like '%" & Request.Cookies("udapTitle").Value & "%'"
                End If

                If Request.Cookies("udapUserDocReqType").Value <> "" Then

                    s_sql = s_sql & " AND dl.requesttype = '" & Request.Cookies("udapUserDocReqType").Value & "' "

                End If


                s_sql = s_sql & " order by " & Request.Cookies("udapSortBy").Value & " " & Request.Cookies("udapSortOption").Value

                DocArchivedDs.SelectCommand = s_sql
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If Not DocArchivedDs Is Nothing Then
                    DocArchivedDs.Dispose()
                End If

            End Try

        End If
    End Sub
End Class