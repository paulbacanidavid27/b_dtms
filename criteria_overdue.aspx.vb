Public Class criteria_overdue
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Reports")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sReportAccess <> "1" Then
            Master.ShowMessage("You don't have access to this report. Please contact the administrator.")
            'ReportViewer1.Visible = False
            btSearch.Visible = False
        Else
            If Not IsPostBack Then



                idAdvSrch.Visible = True
                'pnlImg.Visible = False
                Dim oDoc As New DocTypes
                oDoc.pGroupId = DocSession.sUserGroup
                Using ldata As DataTable = oDoc.GetDocType()


                    'dlDocType.DataTextField = "DocName"
                    'dlDocType.DataValueField = "DocType"

                    If ldata.Rows.Count > 0 Then
                        lbDocType.DataTextField = "DocName"
                        lbDocType.DataValueField = "DocType"
                        lbDocType.DataSource = ldata
                        lbDocType.DataBind()
                    Else
                        If DocSession.sUserRole <> "R" Then
                            Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        End If
                        'ReportViewer1.Visible = False
                    End If
                End Using
                'dlDocType.DataSource = ldata
                'dlDocType.DataBind()
                'ldata = oDoc.GetDocStatus
                'lrow = ldata.NewRow()
                'ldata.Rows.InsertAt(lrow, 0)
                'dlStatus.DataSource = ldata

                'dlStatus.DataValueField = "StatusId"
                'dlStatus.DataTextField = "Description"
                'dlStatus.DataBind()
                'imgGreater.Visible = False
                'imgLess.Visible = False
                'imgGreaterD.Visible = False
                'imgLessD.Visible = False
                'dlDocType.SelectedValue = DocSession.rpt_DocType
                'tbDCFrom.Text = DocSession.rpt_StartDate
                'tbDCTo.Text = DocSession.rpt_EndDate
            End If

        End If

    End Sub


    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) Or (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            msg.Text = "Please provide a valid created date."
            Exit Sub
        End If
        DocSession.rpt_DocType = lbDocType.SelectedValue
        DocSession.rpt_StartDate = tbDCFrom.Text
        DocSession.rpt_EndDate = tbDCTo.Text
        DocSession.rpt_ColValue = tbAuthor.Text
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('rptOverdue.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()








    End Sub

    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
    End Sub
End Class
