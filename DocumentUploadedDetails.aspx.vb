Imports Microsoft.Reporting.WebForms

Public Class DocumentUploadedDetails
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim parms(4) As ReportParameter

        ReportViewer1.LocalReport.EnableExternalImages = True
        parms(0) = New ReportParameter("OfficeCode", DeriveOffice(), False)
        parms(1) = New ReportParameter("GroupCode", DeriveGroup(), False)
        parms(2) = New ReportParameter("DFrom", DeriveDateRange(), False)

        parms(3) = New ReportParameter("ImgPath", DeriveLogo(), False)
        parms(4) = New ReportParameter("ReportName", DocSession.ReceiptReplyName, False)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            Try

                Dim s_sql As String = "select refno,totalpages,createddate,uploaddate = convert(char(10),createddate,101), " & _
"uploadtime = CONVERT(VARCHAR(8), createddate, 108) + ' ' + RIGHT(CONVERT(VARCHAR(30), createddate, 9), 2) ,  " & _
"convert(char(12),dateadd(minute,datediff(minute,dl.createddate, " & _
 "COALESCE( " & _
       "( " & _
  "   SELECT TOP 1 Value=mi.createddate " & _
  "   FROM doclist mi " & _
  "   WHERE mi.docid > dl.docid and CreatedBy = '" & Request.Cookies("udrDocUpUploadedBy").Value & "' and convert(char(10),createddate,101) = '" & CDate(Request.Cookies("udrDocUpRecvFrom").Value).ToString("MM/dd/yyyy") & "' " & _
  "   ORDER BY mi.createddate " & _
       "), dl.createddate) " & _
       "),0),114) AS diff " & _
"from DocList dl where CreatedBy = '" & Request.Cookies("udrDocUpUploadedBy").Value & "' and convert(char(10),createddate,101) = '" & CDate(Request.Cookies("udrDocUpRecvFrom").Value).ToString("MM/dd/yyyy") & "' order by createddate"


                SqlDataSource1.SelectCommand = s_sql

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String = ""

        If Request.Cookies("udrDocUpUploadedByName").Value <> "" Then

            lsParam = "Upload By: " & Request.Cookies("udrDocUpUploadedByName").Value

        End If

        Return lsParam
    End Function
End Class