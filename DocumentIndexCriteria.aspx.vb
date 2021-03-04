Public Class DocumentIndexCriteria
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
                Dim oDoc As DocTypes
                Dim ldata As DataTable
                Try


                    idAdvSrch.Visible = True
                    'pnlImg.Visible = False
                    oDoc = New DocTypes
                    oDoc.pGroupId = DocSession.sUserGroup
                    ldata = oDoc.GetDocType()
                    If ldata.Rows.Count > 0 Then


                        dlDocType.DataTextField = "DocName"
                        dlDocType.DataValueField = "DocType"
                        'Dim lrow As DataRow
                        'lrow = ldata.NewRow()

                        'ldata.Rows.InsertAt(lrow, 0)
                        dlDocType.DataSource = ldata
                        dlDocType.DataBind()
                    Else
                        'If DocSession.sUserRole <> "R" Then
                        '    Master.ShowMessage("You don't have access to any of the document types. You will not be able to view the report. Please contact the administrator.")
                        'End If
                        'ReportViewer1.Visible = False
                    End If

                    RetrieveIndex()
                Catch ex As Exception
                    Master.ShowMessage("An error occurred while displaying criteria (" & ex.Message & "). Please try again.")
                Finally
                    If Not ldata Is Nothing Then
                        ldata.Dispose()
                        ldata = Nothing
                    End If
                End Try
            Else

            End If


        End If


    End Sub
    Private Function ufBuildCriteria() As String
        Dim lsQuery, lsVal As String
        lsQuery = ""
        Dim lColId, lDataType As Literal
        Dim rptIndex As Repeater = ucDocIndex.rIndex
        For Each ri In rptIndex.Items
            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                lColId = DirectCast(ri.FindControl("lColId"), Literal)
                lDataType = DirectCast(ri.FindControl("lDataType"), Literal)
                If lDataType.Text = "3" Then
                    lsVal = DirectCast(ri.FindControl("dlColValue"), DropDownList).SelectedValue
                ElseIf lDataType.Text = "4" Then
                    lsVal = DirectCast(ri.FindControl("tbDateValue"), TextBox).Text
                Else
                    lsVal = DirectCast(ri.FindControl("tbColValue"), TextBox).Text
                End If
                'lsVal = DirectCast(ri.FindControl("lColValue"), TextBox).Text
                If lsVal <> "" Then
                    If lsQuery <> "" Then
                        lsQuery = lsQuery & " OR "
                    End If
                    lsQuery = lsQuery & "(ColumnId = " & DirectCast(ri.FindControl("lColId"), Literal).Text
                    lsQuery = lsQuery & " AND ColValue Like '%" & lsVal.Replace("'", "''") & "%') "
                    lsQuery = "(" & lsQuery & ")"
                End If
            End If
        Next

        Return lsQuery
    End Function

    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click


        LoadReport()

    End Sub

    Private Sub LoadReport()

        tbDCFrom.Text = tbDCFrom.Text.Trim
        If (tbDCFrom.Text <> "" AndAlso Not IsDate(tbDCFrom.Text)) Or (tbDCTo.Text <> "" AndAlso Not IsDate(tbDCTo.Text)) Then
            Master.ShowMessage("Please provide a valid created date.")
            Exit Sub
        End If
        hfColValue.Value = ufBuildCriteria()
        DocSession.rpt_DocType = dlDocType.SelectedValue
        DocSession.rpt_StartDate = tbDCFrom.Text
        DocSession.rpt_EndDate = tbDCTo.Text
        DocSession.rpt_ColValue = hfColValue.Value
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "ViewRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocumentIndex.aspx', 'RptViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ViewRpt", lsScript, False)
        End If

        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Sub RetrieveIndex()
        ucDocIndex.pDIS = False
        ucDocIndex.RetrieveDocIndex(dlDocType.SelectedValue)
        plIndex.Update()
    End Sub

    Private Sub dlDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocType.SelectedIndexChanged
        RetrieveIndex()
        'LoadReport()
    End Sub
End Class
