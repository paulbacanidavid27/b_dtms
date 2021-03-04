Public Class criteria_release
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

                    If ldata.Rows.Count > 0 Then
                        lbDocType.DataTextField = "DocName"
                        lbDocType.DataValueField = "DocType"
                        Dim lrow As DataRow = ldata.NewRow
                        lrow("DocType") = ""
                        lrow("DocName") = "-All-"
                        ldata.Rows.InsertAt(lrow, 0)
                        lbDocType.DataSource = ldata
                        lbDocType.DataBind()
                    Else
                        'If DocSession.sUserRole <> "R" Then
                        '    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        'End If
                    End If
                End Using

                GetOfficeCode()
                tbDCFrom.Text = DateAdd(DateInterval.Month, -2, Date.Now).ToShortDateString
                tbDCTo.Text = Date.Now.ToShortDateString
            End If

        End If

    End Sub


    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click

        LoadReport()

    End Sub

    Private Sub LoadReport()
        msg.Text = ""
        
        DocSession.rpt_DocType = lbDocType.SelectedValue
        DocSession.rpt_OfficeCode = dlOfficeCode.SelectedValue
        DocSession.rpt_StartDate = tbDCFrom.Text
        DocSession.rpt_EndDate = tbDCTo.Text
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('rptReleasedAmount.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If
        
    End Sub

    Private Sub lbDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDocType.SelectedIndexChanged
        'LoadReport()
    End Sub
    Private Sub GetOfficeCode()
        
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            ldata = oType.RetrieveOfficeMainGroup
            If ldata.Rows.Count > 0 Then


                lrow = ldata.NewRow
                lrow("OfficeCode") = ""
                lrow("Description") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
            End If
            dlOfficeCode.DataSource = ldata
            dlOfficeCode.DataValueField = "OfficeCode"
            dlOfficeCode.DataTextField = "Description"
            dlOfficeCode.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub lbShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbShow.Click
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('rptReleasedAmountInvalid.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If
    End Sub
End Class
