Imports Microsoft.Reporting.WebForms

Public Class DashboardDocList
    Inherits System.Web.UI.Page

    Private Sub rptDocStatus_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Dim params(2) As ReportParameter


        'params(0) = New ReportParameter("dfrom", DeriveDateRange(), False)
        'params(1) = New ReportParameter("OfficeName", "", False)
        'params(2) = New ReportParameter("ReportTitle", DeriveTitle(), False)

        'Me.ReportViewer1.LocalReport.SetParameters(params)
    End Sub
    Private Function DeriveTitle() As String

        Return "All " & "Documents"

    End Function
    Private Function DeriveDateRange() As String
        Dim lsMF As String = ""
        Dim lsME As String = ""
        Dim ldf As Date
        Dim lde As Date
        Dim lsParam As String
        'If DocSession.rpt_StartDate <> "" OrElse DocSession.rpt_EndDate <> "" Then
        '    ldf = CDate(DocSession.rpt_StartDate)
        '    lde = CDate(DocSession.rpt_EndDate)

        '    lsMF = MonthName(ldf.Month)
        '    lsME = MonthName(lde.Month)

        '    If ldf.Year < lde.Year Then
        '        lsParam = "Assigned Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
        '    Else
        '        lsParam = "Assigned Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
        '    End If
        'Else
        'If Request.Cookies("udrDocStatRecvFrom").Value <> "" OrElse Request.Cookies("udrDocStatRecvTo").Value <> "" Then
        '    ldf = CDate(Request.Cookies("udrDocStatRecvFrom").Value)
        '    lde = CDate(Request.Cookies("udrDocStatRecvTo").Value)

        '    lsMF = MonthName(ldf.Month)
        '    lsME = MonthName(lde.Month)
        '    If ldf.Year < lde.Year Then
        '        lsParam = "Received Date Range: " & lsMF & " " & CStr(ldf.Year) & " - " & lsME & " " & CStr(lde.Year)
        '    Else
        '        lsParam = "Received Date Range: " & lsMF & " - " & lsME & " " & CStr(lde.Year)
        '    End If
        '    'Else
        'End If

        'End If

        Return "" 'lsParam
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

            

                'Dim od As New DocList
                'Dim asAction As String = ""
                'If Request.Cookies("udrDDLAction").Value <> "" Then
                '    asAction = Request.Cookies("udrDDLAction").Value
                'End If

                'If Request.Cookies("udrDDLOfficeCode").Value <> "" Then
                '    od.pOfficeCode = Request.Cookies("udrDDLOfficeCode").Value
                'End If

                'od.pCreatedDateFrom = ""
                'If Request.Cookies("udrDDLRangeFrom").Value <> "" Then
                '    od.pCreatedDateFrom = Request.Cookies("udrDDLRangeFrom").Value
                'End If
                'od.pCreatedDateTo = ""
                'If Request.Cookies("udrDDLRangeTo").Value <> "" Then
                '    od.pCreatedDateTo = Request.Cookies("udrDDLRangeTo").Value
                'End If

                'If Request.Cookies("udrDDLSortCol").Value <> "" Then
                '    od.pSortCol = Request.Cookies("udrDDLSortCol").Value
                'End If

                'If Request.Cookies("udrDDLSortOrder").Value <> "" Then
                '    od.pSortOrder = Request.Cookies("udrDDLSortOrder").Value
                'End If

                'If Request.Cookies("udrDDLRefno").Value <> "" Then
                '    od.pRefNo = Request.Cookies("udrDDLRefno").Value
                'End If

                'If Request.Cookies("udrDDLSubject").Value <> "" Then
                '    od.pSubject = Request.Cookies("udrDDLSubject").Value
                'End If

                'If Request.Cookies("udrDDLDueDate").Value <> "" Then
                '    od.pDueDate = Request.Cookies("udrDDLDueDate").Value
                'End If

                'If Request.Cookies("udrDDLPersonnelInCharge").Value <> "" Then
                '    od.pPersonnelInCharge = Request.Cookies("udrDDLPersonnelInCharge").Value
                'End If

                'If Request.Cookies("udrDDLStatusId").Value <> "" Then
                '    od.pStatusId = Request.Cookies("udrDDLStatusId").Value
                'End If


                dsDashBoardStatusList.SelectCommand = DocSession.sRegType2 'od.DocRequestListReport(asAction)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If Not dsDashBoardStatusList Is Nothing Then
                    dsDashBoardStatusList.Dispose()
                End If

            End Try
        End If
    End Sub
    
End Class