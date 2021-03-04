Public Class DocReceiving
    Inherits System.Web.UI.Page
    Dim ctrNotOnTime As Integer = 0
    Dim ctrOnTime As Integer = 0
    Dim ctrTotalCRD As Integer = 0
    Dim ctrTotalSummary As Integer = 0
    Dim ctrOther As Integer = 0
    Dim liTotalMin As Integer = 0
    Dim pCutOffDate As String = ""
    Private Sub DocReceiving_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click

        Master.SelectTab("Monitoring")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDefaults()
            LoadHolidays()
            LoadMonitoring()

        End If
    End Sub
#Region "Pager Section"
    Public Sub RetAction()
        DocSession.doc_DocCurrentPage = hfCurrent.Value
        LoadMonitoring()
        'pnlPending.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub
    'pager: step 3
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - IIf(tbRows.Text.Trim = "", DocSession.RowsPerPage, CInt(tbRows.Text.Trim))
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + IIf(tbRows.Text.Trim = "", DocSession.RowsPerPage, CInt(tbRows.Text.Trim))
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        Dim lRowsPerPage As Integer = IIf(tbRows.Text.Trim = "", DocSession.RowsPerPage, CInt(tbRows.Text.Trim))
        If CInt(hfTotalRows.Value) Mod lRowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod lRowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - lRowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub


#End Region
#Region "Retrieval Methods"
    'Private Sub SetSummary()
    '    lDeliveredOnTime.Text = ctrOnTime.ToString("#,##0")
    '    lDeliveredLate.Text = ctrNotOnTime.ToString("#,##0")
    '    lSentByOthers.Text = ctrOther.ToString("#,##0")
    '    lDeliveredByCrd.Text = (ctrOnTime + ctrNotOnTime).ToString("#,##0")
    '    lTotalDocuments.Text = (ctrOther + ctrOnTime + ctrNotOnTime).ToString("#,##0")
    'End Sub
    Private Sub LoadMonitoring(Optional ByVal asRefNo As String = "")
        Dim oData As clsDocMonitoringReceiving
        Dim ldata As DataTable

        Try
            oData = New clsDocMonitoringReceiving
            oData.pRefNo = ""
            oData.pSearchCriteria = ""
            oData.pReceivedBy = ""
            oData.pRemarks = ""
            If asRefNo <> "" Then
                oData.pRefNo = asRefNo.Trim

            Else
                If Left(tbSearch.Text, 4).Trim.ToLower = "rby:" Then
                    oData.pReceivedBy = Mid(tbSearch.Text, 5).Trim

                ElseIf Left(tbSearch.Text, 9).Trim.ToLower = "duration:" Then
                    oData.pDuration = Mid(tbSearch.Text, 10).Trim

                ElseIf Left(tbSearch.Text, 4).Trim.ToLower = "rem:" Then
                    oData.pRemarks = Mid(tbSearch.Text, 5).Trim

                ElseIf Left(tbSearch.Text, 6).Trim.ToLower = "refno:" Then
                    oData.pRefNo = Mid(tbSearch.Text, 7).Trim

                Else
                    oData.pSearchCriteria = tbSearch.Text.Trim
                End If
            End If

            'oData.pYearFrom = tbYearFrom.Text
            'oData.pMonth = dlMonth.SelectedValue
            'oData.pYearTo = tbYearTo.Text
            oData.pSelectedDate = tbSelectedDate.Text
            If DocSession.sUserRole = "A" Then
                oData.pGroupCode = tbGroupCode.Text
            Else
                oData.pGroupCode = DocSession.sUserGroup
            End If

            Dim liTotalRows As Integer = oData.CountTopMonitoring

            If liTotalRows = 0 Then
                Master.ShowMessage("No records found for the selected filter.")
                rptMonitoring.Visible = False
                ucPager.Visible = False
                pPager.Update()
                ' SetSummary()
            Else
                rptMonitoring.Visible = True
                hfTotalRows.Value = liTotalRows.ToString
                oData.pRowsPerPage = IIf(tbRows.Text.Trim = "", DocSession.RowsPerPage, tbRows.Text.Trim)
                oData.pIdx = hfCurrent.Value
                oData.pSortCol = dlDefaultSort.SelectedValue
                oData.pSortOrder = dlDefaultSortOrder.SelectedValue
                ldata = oData.RetrieveTopMonitoring
                If ldata.Rows.Count > oData.pRowsPerPage Then
                    ldata.Rows.RemoveAt(oData.pRowsPerPage)
                Else
                End If

                ucPager.Visible = True
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value), oData.pRowsPerPage)
                pPager.Update()
                rptMonitoring.DataSource = ldata
                rptMonitoring.DataBind()

            End If
            pData.Update()
        Catch ex As Exception
            Master.ShowMessage("Error while loading records (" & ex.Message & "). Please try again.")
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Sub LoadAdd(Optional ByVal asRecNo As String = "")
        Dim oData As clsCrdMonitoring
        Dim ldata As DataTable

        Try

            oData = New clsCrdMonitoring

            oData.pRecordNo = asRecNo.Trim
            ldata = oData.RetrieveAddMonitoring


            ucPager.Visible = False
            'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
            pPager.Update()
            rptMonitoring.DataSource = ldata
            rptMonitoring.DataBind()


            pData.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Sub LoadHolidays()
        Dim ocls = New clsCrdMonitoring
        ocls.pMonth = dlMonth.SelectedValue
        Dim lsYear As String
        If tbYearFrom.Text <> "" Then
            lsYear = tbYearFrom.Text
            'ElseIf tbYearTo.Text <> "" Then
            '    lsYear = tbYearTo.Text
        Else
            Master.ShowMessage("Please select a Year in the Settings screen to view the holidays for the selected month.")
            Exit Sub
        End If
        Dim lsYearTo As String
        lsYearTo = lsYear

        ocls.pYearFrom = lsYear
        ocls.pYearTo = IIf(lsYearTo.Trim() = "", lsYear, lsYearTo)
        'ocls.pYearFrom = lsYear
        If DocSession.sUserRole = "A" Then
            ocls.pOtherOffice = tbGroupCode.Text
        Else
            ocls.pOtherOffice = DocSession.sOfcCode
        End If
        Dim ldata As DataTable = ocls.RetrieveHolidays
        Dim ldrow() As DataRow
        Dim lsDay As String = ""
        Dim lrow As DataRow
        For ctr = 1 To 31
            lsDay = ocls.pMonth & "/" & ctr.ToString & "/" & ocls.pYearFrom
            If IsDate(lsDay) Then
                If WeekdayName(Weekday(CDate(lsDay))) = "Saturday" OrElse WeekdayName(Weekday(CDate(lsDay))) = "Sunday" Then
                    ldrow = ldata.Select("holidate = '" & CDate(lsDay).ToString("MM/dd/yyyy") & "'")
                    If ldrow.Count > 0 Then
                    Else
                        lrow = ldata.NewRow()
                        lrow(0) = CDate(lsDay).ToString("MM/dd/yyyy")
                        lrow(1) = WeekdayName(Weekday(CDate(lsDay)))
                        ldata.Rows.Add(lrow)
                    End If
                End If
            Else
                Exit For
            End If
        Next

        If ldata.Rows.Count > 0 Then
            rptHolidays.DataSource = ldata
            rptHolidays.DataBind()
            'rptHolidays.Visible = True
            lsMsg.Visible = False
        Else
            rptHolidays.Visible = False
            lsMsg.Visible = True
        End If
    End Sub
    Private Sub LoadDefaults()
        Dim oData As clsCrdMonitoring
        Dim ldata As DataTable

        Try
            oData = New clsCrdMonitoring


            'If Not Request.Cookies("docMonRcvMonth") Is Nothing Then
            '    Dim li As New WebControls.ListItem
            '    li = dlMonth.Items.FindByValue(Request.Cookies("docMonRcvMonth").Value)
            '    If Not IsNothing(li) Then
            '        dlMonth.SelectedValue = Request.Cookies("docMonRcvMonth").Value
            '    End If
            'Else
            '    dlMonth.SelectedValue = Month(Date.Now).ToString
            'End If
            If Not Request.Cookies("docMonRcvSeparator") Is Nothing Then
                If Request.Cookies("docMonRcvSeparator").Value <> "" Then
                    tbSeparator.Text = Request.Cookies("docMonRcvSeparator").Value
                Else
                    tbSeparator.Text = ";"
                End If
            Else
                tbSeparator.Text = ";"
            End If
            If Not Request.Cookies("docMonRcvDeliveryTime") Is Nothing Then
                If Request.Cookies("docMonRcvDeliveryTime").Value <> "" Then
                    tbDeliveryTime.Text = Request.Cookies("docMonRcvDeliveryTime").Value
                Else
                    tbDeliveryTime.Text = "60"
                End If


            Else
                tbDeliveryTime.Text = "60"
            End If
            If Not Request.Cookies("docMonRcvDate") Is Nothing Then
                If Request.Cookies("docMonRcvDate").Value <> "" Then
                    tbSelectedDate.Text = Request.Cookies("docMonRcvDate").Value
                Else
                    tbSelectedDate.Text = Date.Now.ToShortDateString
                End If


            Else
                tbSelectedDate.Text = Date.Now.ToShortDateString
            End If

            If IsDate(tbSelectedDate.Text) Then
                tbYearFrom.Text = CDate(tbSelectedDate.Text).Year.ToString
                dlMonth.SelectedValue = Month(CDate(tbSelectedDate.Text)).ToString
            Else
                tbYearFrom.Text = Year(Date.Now).ToString
                dlMonth.SelectedValue = Month(Date.Now).ToString
            End If

            'lmonthyear.Text = tbSelectedDate.Text 'dlMonth.SelectedItem.Text & "/" & tbYearFrom.Text
            upSelected.Update()

            If Not Request.Cookies("docMonRcvBackgroundImage") Is Nothing Then
                tbBackgroundImage.Text = Request.Cookies("docMonRcvBackgroundImage").Value
            Else

            End If

            If Not Request.Cookies("docMonRcvDefaultRows") Is Nothing Then
                tbRows.Text = Request.Cookies("docMonRcvDefaultRows").Value
                If tbRows.Text = "" Then
                    tbRows.Text = DocSession.RowsPerPage
                End If
            Else
                tbRows.Text = DocSession.RowsPerPage
            End If


            If Not Request.Cookies("docMonRcvDefaultGroupCode") Is Nothing Then
                tbGroupCode.Text = Request.Cookies("docMonRcvDefaultGroupCode").Value
            Else
                tbGroupCode.Text = "CRD"
            End If
            If Not Request.Cookies("docMonRcvDefaultSort") Is Nothing Then
                If Request.Cookies("docMonRcvDefaultSort").Value <> "" Then
                    'hfDefaultDelivery.Value = Request.Cookies("docMonRcvDefaultSort").Value
                    Dim li As New WebControls.ListItem
                    li = dlDefaultSort.Items.FindByValue(Request.Cookies("docMonRcvDefaultSort").Value)
                    If Not IsNothing(li) Then
                        dlDefaultSort.SelectedValue = Request.Cookies("docMonRcvDefaultSort").Value
                    End If
                End If
            Else

            End If
            If Not Request.Cookies("docMonRcvRcvCutOffMorning") Is Nothing Then
                If Request.Cookies("docMonRcvRcvCutOffMorning").Value <> "" Then
                    tbCutOffMorning.Text = Request.Cookies("docMonRcvRcvCutOffMorning").Value
                End If
            Else

            End If
            If Not Request.Cookies("docMonRcvRcvCutOffAfternoon") Is Nothing Then
                If Request.Cookies("docMonRcvRcvCutOffAfternoon").Value <> "" Then
                    tbCutOffAfternoon.Text = Request.Cookies("docMonRcvRcvCutOffAfternoon").Value
                End If
            Else
            End If
            If Not Request.Cookies("docMonRcvRcvCutOffLunchFrom") Is Nothing Then
                If Request.Cookies("docMonRcvRcvCutOffLunchFrom").Value <> "" Then
                    tbLunchFrom.Text = Request.Cookies("docMonRcvRcvCutOffLunchFrom").Value
                End If
            Else
            End If
            If Not Request.Cookies("docMonRcvRcvCutOffLunchTo") Is Nothing Then
                If Request.Cookies("docMonRcvRcvCutOffLunchTo").Value <> "" Then
                    tbLunchTo.Text = Request.Cookies("docMonRcvRcvCutOffLunchTo").Value
                End If
            Else
            End If
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Function GetOfficeCode() As DataTable

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup
            oType.pOfficeCode = ""
            ldata = oType.RetrieveOffice

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "<--Other-->"
            ldata.Rows.InsertAt(lrow, 0)
            Return ldata

            'Return ldata
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If

        End Try

    End Function

    Private Function GetUsers() As DataTable

        'Dim ldata As DataTable
        'Dim lrow As DataRow
        Dim oType As clsCrdMonitoring
        Try
            oType = New clsCrdMonitoring

            Return oType.RetrieveUsers(tbGroupCode.Text)

            'Return ldata
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If

        End Try

    End Function
#End Region
    Protected Sub RefreshHolidays(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadHolidays()
    End Sub
    Protected Sub ShowDelete(ByVal sender As Object, ByVal e As System.EventArgs)

        Using oImg As ImageButton = DirectCast(sender, ImageButton)

            Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)
            lSelectedReceivingId.Text = DirectCast(oItem.FindControl("lReceivingId"), Literal).Text
            pDeleteMonitoringConfirmation.Visible = Not pDeleteMonitoringConfirmation.Visible
            Master.ShowImageDocument = True
        End Using

        'Dim xpnl As UpdatePanel = DirectCast(oItem.FindControl("pnlUser"), UpdatePanel)
        'xpnl.Update()
    End Sub
#Region "Web Form Methods"
    Protected Sub UpdateMonitoring(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oImg As ImageButton = DirectCast(sender, ImageButton)

        Dim ri As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)
        Dim lbl As Literal
        Dim lRefNoData As DataTable
        Dim clsDM As clsCrdMonitoring
        Try
            If DirectCast(ri.FindControl("lMainStatus"), Literal).Text = "7" Then

                'ShowProcessedOKMessage(True)
                Exit Sub
            End If
            clsDM = New clsCrdMonitoring
            Dim lsRefNo As String = DirectCast(ri.FindControl("tbRefNo"), TextBox).Text.Trim
            '--george
            'If Not ValidRefno(lsRefNo) Then
            '    Exit Sub
            'End If
            clsDM.pFileVersion = "1" 'lRefNoData(0)("FileVersion")
            clsDM.pDateTimeReceived = DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text & " " & DirectCast(ri.FindControl("tbTimeReceived"), TextBox).Text





            If clsDM.pDateTimeReceived <> "" AndAlso Not IsDate(clsDM.pDateTimeReceived) Then
                Master.ShowMessage("Invalid Date and Time Received.")
                Exit Sub
            ElseIf Month(clsDM.pDateTimeReceived) <> CInt(dlMonth.SelectedValue) OrElse Year(clsDM.pDateTimeReceived) <> CInt(tbYearFrom.Text) Then
                Master.ShowMessage("Date Received is not within the selected Month/Year range.")
                Exit Sub
            Else

                If DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text <> DirectCast(ri.FindControl("lDateReceived"), Literal).Text Then
                    'DirectCast(ri.FindControl("tbDueDate"), TextBox).Text = GetDueDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text)
                    'clsDM.pDueDate = GetDueDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text)


                Else
                    If CDate(DirectCast(ri.FindControl("tbDueDate"), TextBox).Text) < CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text) Then
                        Master.ShowMessage("Due Date should not be prior to Received Date.")
                        Exit Sub
                    End If
                    clsDM.pDueDate = DirectCast(ri.FindControl("tbDueDate"), TextBox).Text
                End If
            End If

            'check for changes
            If CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text & " " & DirectCast(ri.FindControl("tbTimeReceived"), TextBox).Text) <> _
                 CDate(DirectCast(ri.FindControl("lDateReceived"), Literal).Text & " " & DirectCast(ri.FindControl("lTimeReceived"), Literal).Text) Then
            Else
                clsDM.pDateTimeReceived = ""
            End If
            'check for changes
            If DirectCast(ri.FindControl("lDueDate"), Literal).Text = clsDM.pDueDate Then
                clsDM.pDueDate = ""
            Else
                DirectCast(ri.FindControl("tbDueDate"), TextBox).Text = clsDM.pDueDate
            End If

            If DirectCast(ri.FindControl("tbSortedReceived"), TextBox).Text.Trim = "" Then
                clsDM.pSortedReceived = "1/1/1900"
            ElseIf Not IsDate(DirectCast(ri.FindControl("tbSortedReceived"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Sorting Date Received.")
                Exit Sub
            Else
                clsDM.pSortedReceived = DirectCast(ri.FindControl("tbSortedReceived"), TextBox).Text
            End If
            'check for changes
            If clsDM.pSortedReceived <> DirectCast(ri.FindControl("lSortedReceived"), Literal).Text Then
                clsDM.pSortedReceived = DirectCast(ri.FindControl("tbSortedReceived"), TextBox).Text
            Else
                clsDM.pSortedReceived = ""
            End If

            If DirectCast(ri.FindControl("tbSortedCompleted"), TextBox).Text.Trim = "" Then
                clsDM.pSortedCompleted = "1/1/1900"
            ElseIf Not IsDate(DirectCast(ri.FindControl("tbSortedCompleted"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Sorting Date Completed.")
                Exit Sub
            Else
                clsDM.pSortedCompleted = DirectCast(ri.FindControl("tbSortedCompleted"), TextBox).Text
            End If
            'check for changes
            If clsDM.pSortedCompleted <> DirectCast(ri.FindControl("lSortedCompleted"), Literal).Text Then
                clsDM.pSortedCompleted = DirectCast(ri.FindControl("tbSortedCompleted"), TextBox).Text
            Else
                clsDM.pSortedCompleted = ""
            End If
            If DirectCast(ri.FindControl("tbDeliveryReceived"), TextBox).Text.Trim = "" Then
                clsDM.pDeliveryReceived = "1/1/1900"
            ElseIf DirectCast(ri.FindControl("tbDeliveryReceived"), TextBox).Text.Trim <> "" AndAlso Not IsDate(DirectCast(ri.FindControl("tbDeliveryReceived"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Delivery Date Received.")
                Exit Sub
            Else
                clsDM.pDeliveryReceived = DirectCast(ri.FindControl("tbDeliveryReceived"), TextBox).Text
            End If

            'check for changes
            If clsDM.pDeliveryReceived <> DirectCast(ri.FindControl("lDeliveryReceived"), Literal).Text Then
                clsDM.pDeliveryReceived = DirectCast(ri.FindControl("tbDeliveryReceived"), TextBox).Text
            Else
                clsDM.pDeliveryReceived = ""
            End If
            If DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim = "" Then
                clsDM.pDeliveryCompleted = "1/1/1900"
            ElseIf DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim <> "" AndAlso Not IsDate(DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Date Delivered.")
                Exit Sub
            Else
                clsDM.pDeliveryCompleted = DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text
            End If
            'check for changes
            If clsDM.pDeliveryCompleted <> DirectCast(ri.FindControl("lDeliveryCompleted"), Literal).Text Then
                clsDM.pDeliveryCompleted = DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text
            Else
                clsDM.pDeliveryCompleted = ""
            End If
            If DirectCast(ri.FindControl("tbMailingReceived"), TextBox).Text.Trim = "" Then
                clsDM.pMailingReceived = "1/1/1900"
            ElseIf DirectCast(ri.FindControl("tbMailingReceived"), TextBox).Text.Trim <> "" AndAlso Not IsDate(DirectCast(ri.FindControl("tbMailingReceived"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Mailing Date Received.")
                Exit Sub
            Else
                clsDM.pMailingReceived = DirectCast(ri.FindControl("tbMailingReceived"), TextBox).Text

            End If
            'check for changes
            If clsDM.pMailingReceived <> DirectCast(ri.FindControl("lMailingReceived"), Literal).Text Then
                clsDM.pMailingReceived = DirectCast(ri.FindControl("tbMailingReceived"), TextBox).Text
            Else
                clsDM.pMailingReceived = ""
            End If
            If DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim = "" Then
                clsDM.pMailingCompleted = "1/1/1900"
            ElseIf DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim <> "" AndAlso Not IsDate(DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Date Mailed.")
                Exit Sub
            Else
                clsDM.pMailingCompleted = DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim
            End If
            'check for changes
            If clsDM.pMailingCompleted <> DirectCast(ri.FindControl("lMailingCompleted"), Literal).Text Then
                clsDM.pMailingCompleted = DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text
            Else
                clsDM.pMailingCompleted = ""
            End If

            If DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text.Trim = "" Then
                clsDM.pDateOfLetter = "1/1/1900"
            ElseIf DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text.Trim <> "" AndAlso Not IsDate(DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Date of Letter.")
                Exit Sub
            Else
                clsDM.pDateOfLetter = DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text.Trim
            End If
            'check for changes
            If clsDM.pDateOfLetter <> DirectCast(ri.FindControl("lDateOfLetter"), Literal).Text Then
                clsDM.pDateOfLetter = DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text
            Else
                clsDM.pDateOfLetter = ""
            End If
            If DirectCast(ri.FindControl("tbDateReceivedByRecipient"), TextBox).Text.Trim = "" Then
                clsDM.pDateReceivedByRecipient = "1/1/1900"
            ElseIf Not IsDate(DirectCast(ri.FindControl("tbDateReceivedByRecipient"), TextBox).Text.Trim) Then
                Master.ShowMessage("Invalid Date Received by Recipient.")
                Exit Sub
            Else
                clsDM.pDateReceivedByRecipient = DirectCast(ri.FindControl("tbDateReceivedByRecipient"), TextBox).Text.Trim
            End If
            'check for changes
            If clsDM.pDateReceivedByRecipient <> DirectCast(ri.FindControl("lDateReceivedByRecipient"), Literal).Text Then
                clsDM.pDateReceivedByRecipient = DirectCast(ri.FindControl("tbDateReceivedByRecipient"), TextBox).Text
            Else
                clsDM.pDateReceivedByRecipient = ""
            End If
            clsDM.pParentRecordNo = DirectCast(ri.FindControl("lParentDocNo"), Literal).Text
            clsDM.pRecordNo = DirectCast(ri.FindControl("lRecordNo"), Literal).Text
            'clsDM.pParentRecordNo = DirectCast(ri.FindControl("lParentRecordNo"), Literal).Text
            clsDM.pDocId = "1" 'lRefNoData(0)("DocId")
            If lsRefNo.Trim <> DirectCast(ri.FindControl("lRefNo"), Literal).Text Then
                clsDM.pRefNo = lsRefNo.Trim
            Else
                clsDM.pRefNo = ""
            End If


            'check for changes
            If DirectCast(ri.FindControl("dlReceivedBy"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lReceivedBy"), Literal).Text.Trim.ToLower Then
                clsDM.pReceivedBy = DirectCast(ri.FindControl("dlReceivedBy"), DropDownList).SelectedValue
            Else
                clsDM.pReceivedBy = ""
            End If
            'check for changes
            If DirectCast(ri.FindControl("dlRequestingOfcCode"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lRequestingOfcCode"), Literal).Text.Trim.ToLower Then
                clsDM.pRequestingOfcCode = DirectCast(ri.FindControl("dlRequestingOfcCode"), DropDownList).SelectedValue
            Else
                clsDM.pRequestingOfcCode = ""
            End If
            clsDM.pDescription = DirectCast(ri.FindControl("tbDescription"), TextBox).Text
            'check for changes
            If DirectCast(ri.FindControl("tbDescription"), TextBox).Text <> DirectCast(ri.FindControl("lDescription"), Literal).Text.Trim.ToLower Then
                clsDM.pDescription = DirectCast(ri.FindControl("tbDescription"), TextBox).Text
            Else
                clsDM.pDescription = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlSortingStatus"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lSortingStatus"), Literal).Text.Trim.ToLower Then
                clsDM.pSortingStatus = DirectCast(ri.FindControl("dlSortingStatus"), DropDownList).SelectedValue
            Else
                clsDM.pSortingStatus = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlSortedBy"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lSortedBy"), Literal).Text.Trim.ToLower Then
                clsDM.pSortedBy = DirectCast(ri.FindControl("dlSortedBy"), DropDownList).SelectedValue
            Else
                clsDM.pSortedBy = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlDeliveryStatus"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lDeliveryStatus"), Literal).Text.Trim.ToLower Then
                clsDM.pDeliveryStatus = DirectCast(ri.FindControl("dlDeliveryStatus"), DropDownList).SelectedValue
            Else
                clsDM.pDeliveryStatus = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlDeliveredBy"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lDeliveredBy"), Literal).Text.Trim.ToLower Then
                clsDM.pDeliveredBy = DirectCast(ri.FindControl("dlDeliveredBy"), DropDownList).SelectedValue
            Else
                clsDM.pDeliveredBy = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlMailingStatus"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lMailingStatus"), Literal).Text.Trim.ToLower Then
                clsDM.pMailingStatus = DirectCast(ri.FindControl("dlMailingStatus"), DropDownList).SelectedValue
            Else
                clsDM.pMailingStatus = ""
            End If

            'check for changes
            If DirectCast(ri.FindControl("dlMailedBy"), DropDownList).SelectedValue.Trim.ToLower <> DirectCast(ri.FindControl("lMailedBy"), Literal).Text.Trim.ToLower Then
                clsDM.pMailedBy = DirectCast(ri.FindControl("dlMailedBy"), DropDownList).SelectedValue
            Else
                clsDM.pMailedBy = ""
            End If

            If (clsDM.pDeliveryCompleted <> "" AndAlso CDate(clsDM.pDeliveryCompleted) <> CDate("01/01/1900")) OrElse _
                (clsDM.pMailingCompleted <> "" AndAlso CDate(clsDM.pMailingCompleted) <> CDate("01/01/1900")) Then
                clsDM.pMainStatus = "3" 'Complete

            Else
                clsDM.pMainStatus = DirectCast(ri.FindControl("lMainStatus"), Literal).Text
            End If
            If (clsDM.pDeliveryCompleted <> "" AndAlso CDate(clsDM.pDeliveryCompleted) <> CDate("01/01/1900")) Then
                clsDM.pDeliveryStatus = "3"
            End If

            If (clsDM.pMailingCompleted <> "" AndAlso CDate(clsDM.pMailingCompleted) <> CDate("01/01/1900")) Then
                clsDM.pMailingStatus = "3"
            End If

            'clsDM.pDueDate = DirectCast(ri.FindControl("lDueDate"), Literal).Text
            'clsDM.pDuration = DateDiff(DateInterval.Day, CDate(DirectCast(ri.FindControl("lDueDate"), Literal).Text), CDate(clsDM.pSortedCompleted))

            'check for changes
            If DirectCast(ri.FindControl("tbRemarks"), TextBox).Text <> DirectCast(ri.FindControl("lRemarks"), Literal).Text.Trim.ToLower Then
                clsDM.pRemarks = DirectCast(ri.FindControl("tbRemarks"), TextBox).Text
            Else
                clsDM.pRemarks = ""
            End If
            'clsDM.pDateOfLetter = DirectCast(ri.FindControl("lDateOfLetter"), Literal).Text

            'check for changes
            If DirectCast(ri.FindControl("tbDescription"), TextBox).Text <> DirectCast(ri.FindControl("lDescription"), Literal).Text.Trim.ToLower Then
                clsDM.pDescription = DirectCast(ri.FindControl("tbDescription"), TextBox).Text
            Else
                clsDM.pDescription = ""
            End If

            If DirectCast(ri.FindControl("tbLocation"), TextBox).Text <> DirectCast(ri.FindControl("lLocation"), Literal).Text.Trim.ToLower Then
                clsDM.pLocation = DirectCast(ri.FindControl("tbLocation"), TextBox).Text
            Else
                clsDM.pLocation = ""
            End If

            If DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text <> DirectCast(ri.FindControl("lDateOfLetter"), Literal).Text.Trim.ToLower Then
                clsDM.pDateOfLetter = DirectCast(ri.FindControl("tbDateOfLetter"), TextBox).Text
            Else
                clsDM.pDateOfLetter = ""
            End If

            If DirectCast(ri.FindControl("rbDelivery"), CheckBox).Checked Then
                clsDM.pPersonalDelivery = "1"
                If DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim <> "" AndAlso IsDate(DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim) Then
                    If CDate(DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim) < CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text.Trim) Then
                        Master.ShowMessage("Delivery Date Completed should not be prior to Received Date.")
                        Exit Sub

                    End If
                End If
                DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text = DirectCast(ri.FindControl("tbDeliveryCompleted"), TextBox).Text.Trim
            Else
                clsDM.pPersonalDelivery = "0"
                If DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim <> "" AndAlso IsDate(DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim) Then
                    If CDate(DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim) < CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text.Trim) Then
                        Master.ShowMessage("Mailing Date Completed should not be prior to Received Date.")
                        Exit Sub

                    End If
                End If
                DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text = DirectCast(ri.FindControl("tbMailingCompleted"), TextBox).Text.Trim
            End If

            Dim liDurationHours As Integer = 0
            Dim liDurationDays As Integer = 0
            Dim liDurationDays2 As Integer = 0
            Dim liTotalHours As Integer = 0
            Dim lsDuration As String = "0"
            Dim lsCDate As String

            If IsDate(DirectCast(ri.FindControl("lDueDate"), Literal).Text.Trim) Then
                If DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim <> "" Then
                    If IsDate(DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim) Then
                        liTotalHours = DateDiff(DateInterval.Hour, CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text.Trim), CDate(DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim))
                    Else
                        Master.ShowMessage("Invalid Date Completd.")
                        Exit Sub
                    End If
                    lsCDate = DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim
                Else
                    lsCDate = Date.Now.ToString
                    liTotalHours = DateDiff(DateInterval.Hour, CDate(DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text.Trim), CDate(lsCDate))
                End If
                liDurationHours = liTotalHours Mod 24
                liDurationDays = GetDuration(lsCDate, DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text.Trim) 'Math.Floor(liTotalHours / 24)
                If liDurationDays > 0 Then
                    lsDuration = liDurationDays.ToString("#,##0") & " " & IIf(liDurationDays > 1, "days", "day")
                End If
                If liDurationHours > 0 Then
                    lsDuration = lsDuration & " " & liDurationHours.ToString("#,##0") & " " & IIf(liDurationHours > 1, "hours", "hour")
                End If

                DirectCast(ri.FindControl("lDuration"), Literal).Text = lsDuration
                If DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim <> "" Then
                    If CDate(DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text.Trim) > CDate(DirectCast(ri.FindControl("tbDueDate"), TextBox).Text.Trim) Then
                        DirectCast(ri.FindControl("lDurationStatus"), Literal).Text = "Status - Overdue."
                    End If
                End If

            Else
                DirectCast(ri.FindControl("lDurationStatus"), Literal).Text = "Status - Due Date not assigned."
            End If
            DirectCast(ri.FindControl("lTDuration"), Literal).Text = liDurationDays.ToString
            DirectCast(ri.FindControl("tbDuration"), TextBox).Text = liDurationDays.ToString
            clsDM.pDuration = liDurationDays.ToString

            If DirectCast(ri.FindControl("tbCourierName"), TextBox).Text.Trim.ToLower <> DirectCast(ri.FindControl("lCourierName"), Literal).Text.Trim.ToLower Then
                clsDM.pCourierName = DirectCast(ri.FindControl("tbCourierName"), TextBox).Text
            Else
                clsDM.pCourierName = ""
            End If
            'clsDM.pCreatedBy = DocSession.sUserId
            'clsDM.pCreatedDate = DirectCast(ri.FindControl("lCreatedDate"), Literal).Text
            If DirectCast(ri.FindControl("tbFinalDateCompleted"), TextBox).Text <> "" Then
                If clsDM.pMailingStatus = "3" OrElse clsDM.pDeliveryStatus = "3" Then
                    clsDM.pMainStatus = "3"
                Else
                    clsDM.pMainStatus = 2
                End If
            Else
                clsDM.pMainStatus = 2
            End If

            If DirectCast(ri.FindControl("tbOtherOffice"), TextBox).Text <> DirectCast(ri.FindControl("lOtherOffice"), Literal).Text Then
                clsDM.pOtherOffice = DirectCast(ri.FindControl("tbOtherOffice"), TextBox).Text
            Else
                clsDM.pOtherOffice = ""
            End If

            clsDM.pGroupCode = IIf(DocSession.sUserRole = "A", tbGroupCode.Text, DocSession.sUserGroup)

            clsDM.UpdateMonitoring()
            LogHistory("Updated record no '" & clsDM.pRecordNo & "'.", "Monitoring")
            'Received
            DirectCast(ri.FindControl("lReceivedBy"), Literal).Text = clsDM.pReceivedBy
            DirectCast(ri.FindControl("lReceivedByName"), Literal).Text = DirectCast(ri.FindControl("dlReceivedBy"), DropDownList).SelectedItem.Text

            'Requesting Office
            DirectCast(ri.FindControl("lRequestingOfcCode"), Literal).Text = clsDM.pRequestingOfcCode
            DirectCast(ri.FindControl("lRequestingOfcDesc"), Literal).Text = DirectCast(ri.FindControl("dlRequestingOfcCode"), DropDownList).SelectedItem.Text
            DirectCast(ri.FindControl("lOtherOffice"), Literal).Text = clsDM.pRequestingOfcCode
            DirectCast(ri.FindControl("lLocation"), Literal).Text = clsDM.pLocation
            'Date Time Received
            DirectCast(ri.FindControl("lTimeReceived"), Literal).Text = DirectCast(ri.FindControl("tbTimeReceived"), TextBox).Text
            DirectCast(ri.FindControl("lDateReceived"), Literal).Text = DirectCast(ri.FindControl("tbDateReceived"), TextBox).Text

            DirectCast(ri.FindControl("lDescription"), Literal).Text = clsDM.pDescription

            DirectCast(ri.FindControl("lDateOfLetter"), Literal).Text = clsDM.pDateOfLetter
            DirectCast(ri.FindControl("lDateReceivedByRecipient"), Literal).Text = clsDM.pDateReceivedByRecipient
            DirectCast(ri.FindControl("lSortedReceived"), Literal).Text = clsDM.pSortedReceived
            DirectCast(ri.FindControl("lSortedCompleted"), Literal).Text = clsDM.pSortedCompleted
            DirectCast(ri.FindControl("lDeliveryReceived"), Literal).Text = clsDM.pDeliveryReceived
            DirectCast(ri.FindControl("lDeliveryCompleted"), Literal).Text = clsDM.pDeliveryCompleted
            DirectCast(ri.FindControl("lMailingReceived"), Literal).Text = clsDM.pMailingReceived
            DirectCast(ri.FindControl("lMailingCompleted"), Literal).Text = clsDM.pMailingCompleted
            DirectCast(ri.FindControl("lDescription"), Literal).Text = clsDM.pDescription
            Master.ShowMessage("**Update successful.")
            pData.Update()
        Catch ex As Exception

            Master.ShowMessage("There's an error while updating the monitoring ( " & ex.Message & " ). Please try again.")
        Finally


            If Not oImg Is Nothing Then
                oImg.Dispose()
                oImg = Nothing
            End If
            If Not ri Is Nothing Then
                ri.Dispose()
                ri = Nothing
            End If

            If Not lbl Is Nothing Then
                lbl.Dispose()
                lbl = Nothing
            End If
            If Not lRefNoData Is Nothing Then
                lRefNoData.Dispose()
                lRefNoData = Nothing
            End If
        End Try


    End Sub
#End Region
#Region "Log History"
    Private Sub LogHistory(ByVal asAction As String, ByVal asTask As String)
        Dim ohist As New DocHistory

        ohist.pAction = asAction
        ohist.pDocId = "0"
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = asTask
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()
        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)



    End Sub
#End Region
    'Private Sub btProcessedOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btProcessedOk.Click
    '    ShowProcessedOKMessage(False)
    'End Sub
    'Private Sub ShowProcessedOKMessage(ByVal asShow As Boolean)
    '    pProcessedMessage.Visible = Not pProcessedMessage.Visible
    '    Master.ShowImageDocument = asShow
    'End Sub
    Private Sub ProcessRecords()
        Dim ldata As DataTable
        Dim clsDM As clsCrdMonitoring
        Try
            clsDM = New clsCrdMonitoring
            If dlMonth.SelectedValue = "" Then
                Master.ShowMessage("Please select a Month to process.")
                Exit Sub
            End If
            If tbYearFrom.Text = "" Then
                Master.ShowMessage("Please select a Year to process.")
                Exit Sub
            ElseIf Not IsDate(tbYearFrom.Text) Then
                Master.ShowMessage("Please enter a valid Year to process.")
                Exit Sub
            End If
            clsDM.pMonth = dlMonth.SelectedValue
            clsDM.pYearFrom = tbYearFrom.Text

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetDuration(ByVal asDateCompleted As String, ByVal asDateReceived As String) As Integer

        Try


            Dim lsHolidays As String = RetrieveHolidays()
            Dim ictr As Integer = 0
            'Dim lidayofweek As Integer
            Dim lsDays As String
            Dim iirow As Integer = 0
            Dim ldCurrent As Date = CDate(asDateReceived)

            While ldCurrent.ToString("MM/dd/yyyy") <> CDate(asDateCompleted).ToString("MM/dd/yyyy")

                lsDays = "," & ldCurrent.ToString("MM/dd/yyyy") & ","
                If lsHolidays.IndexOf(lsDays) > 0 Then

                Else
                    ictr = ictr + 1
                End If
                'iirow = iirow + 1
                ldCurrent = DateAdd(DateInterval.Day, 1, ldCurrent)
            End While

            Return ictr
        Catch ex As Exception
            Throw New Exception("Duration: " & ex.Message)
        End Try
    End Function

    Private Function CountHolidays(ByVal asDateCreated As String, ByVal asDateReceived As String) As Integer

        Try


            Dim lsHolidays As String = RetrieveHolidays()
            Dim holidayCtr As Integer = 0
            'Dim lidayofweek As Integer
            Dim lsDays As String
            Dim iirow As Integer = 0
            Dim ldCurrent As Date = CDate(asDateCreated)

            While ldCurrent.ToString("MM/dd/yyyy") <> CDate(asDateReceived).ToString("MM/dd/yyyy")

                lsDays = "," & ldCurrent.ToString("MM/dd/yyyy") & ","
                If lsHolidays.IndexOf(lsDays) >= 0 Then
                    holidayCtr = holidayCtr + 1
                End If
                'iirow = iirow + 1
                ldCurrent = DateAdd(DateInterval.Day, 1, ldCurrent)
            End While

            Return holidayCtr
        Catch ex As Exception
            Throw New Exception("Duration: " & ex.Message)
        End Try
    End Function
    Private Function RetrieveHolidays() As String
        Dim lsDays As String = ""
        For Each rptItm As RepeaterItem In rptHolidays.Items

            lsDays = lsDays & "," & DirectCast(rptItm.FindControl("ldate"), Literal).Text

        Next
        If lsDays <> "" Then
            lsDays = lsDays & ","
        End If
        Return lsDays
    End Function

    Private Sub imgHoliday_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHoliday.Click
        If rptHolidays.Visible Then


            rptHolidays.Visible = False
            'lsMsg.Visible = False
        Else

            rptHolidays.Visible = True
            'lsMsg.Visible = False

        End If
        upHoliday.Update()

    End Sub

    Private Sub imgSettings_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSettings.Click
        pSettings.Visible = Not pSettings.Visible
        upSettings.Update()
    End Sub

    Private Sub imgShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgShow.Click
        Try


            If imgShow.ImageUrl = "images/button/down.png" Then
                imgShow.ImageUrl = "images/button/up.png"
                'RetrieveSummary()
                'RetrieveReports()
            Else
                imgShow.ImageUrl = "images/button/down.png"
            End If

            processtbl.Visible = Not processtbl.Visible
            If processtbl.Visible Then
                lSummaryMonthYear.Text = dlMonth.SelectedItem.Text & " " & tbYearFrom.Text
                Dim clsRecv As New clsDocMonitoringReceiving
                clsRecv.pMonth = dlMonth.SelectedValue
                clsRecv.pYearFrom = tbYearFrom.Text
                rptSummary.DataSource = clsRecv.RetrieveSummaryReceiving
                rptSummary.DataBind()
            End If

            upProcess.Update()
        Catch ex As Exception
            Master.ShowMessage("Error occurred while retrieveing Reports (" & ex.Message & "). Please try again.")
        End Try
    End Sub
    Private Sub SaveCookies()
        Dim mycookie As HttpCookie = New HttpCookie("docMonRcvDate")
        mycookie.Value = tbSelectedDate.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvSearch")
        mycookie.Value = tbSearch.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click
        Dim mycookie As HttpCookie = New HttpCookie("docMonRcvBackgroundImage")
        mycookie.Value = tbBackgroundImage.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvDefaultRows")
        mycookie.Value = tbRows.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvRcvCutOffMorning")
        mycookie.Value = tbCutOffMorning.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvRcvCutOffAfternoon")
        mycookie.Value = tbCutOffAfternoon.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvRcvCutOffLunchFrom")
        mycookie.Value = tbLunchFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvRcvCutOffLunchTo")
        mycookie.Value = tbLunchTo.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvDefaultSort")
        mycookie.Value = dlDefaultSort.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvDefaultGroupCode")
        mycookie.Value = tbGroupCode.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)



        mycookie = New HttpCookie("docMonRcvMonth")
        mycookie.Value = dlMonth.SelectedValue
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvYearFrom")
        mycookie.Value = tbYearFrom.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvSeparator")
        mycookie.Value = tbSeparator.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)

        mycookie = New HttpCookie("docMonRcvDeliveryTime")
        mycookie.Value = tbDeliveryTime.Text
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
        'mycookie = New HttpCookie("docMonRcvYearTo")
        'mycookie.Value = tbYearTo.Text
        'mycookie.Expires = DateTime.Now.AddDays(30)
        'Response.Cookies.Add(mycookie)

        'lmonthyear.Text = tbSelectedDate.Text
        'upSelected.Update()
        'Master.ShowMessage("Defaults has been saved successfully.")
        Response.Redirect("docReceiving.aspx")
    End Sub

    Private Sub rptMonitoring_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMonitoring.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lDeliveryStatus As Literal = DirectCast(e.Item.FindControl("lDeliveryStatus"), Literal)
            Dim lDuration As Literal = DirectCast(e.Item.FindControl("lDuration"), Literal)
            If DirectCast(e.Item.FindControl("lRemarks"), Literal).Text.Trim = "On Time" Then
                ctrOnTime = ctrOnTime + 1
            ElseIf DirectCast(e.Item.FindControl("lRemarks"), Literal).Text.Trim = "Late" Then
                ctrNotOnTime = ctrNotOnTime + 1
            Else
                'lDeliveryStatus.Text = "Others"
                ctrOther = ctrOther + 1
            End If
            Dim TotalMin As Integer = CInt(lDuration.Text.Trim)
            If TotalMin < 0 Then
                lDuration.Text = "n/a"
            ElseIf TotalMin < 60 Then
                lDuration.Text = "00:" & Right("0" & liTotalMin.ToString, 2)

            Else
                Dim liHr As Integer = Math.Floor(TotalMin / 60)
                Dim liMin As Integer = IIf(TotalMin < 60, liTotalMin, TotalMin Mod 60)
                lDuration.Text = Right("0" & liHr.ToString, 2) & ":" & Right("0" & liMin.ToString, 2)
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            'SetSummary()
            DirectCast(e.Item.FindControl("lLate"), Literal).Text = ctrNotOnTime.ToString
            DirectCast(e.Item.FindControl("lOnTime"), Literal).Text = ctrOnTime.ToString
            DirectCast(e.Item.FindControl("lOthers"), Literal).Text = ctrOther.ToString
        End If
        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim lDeliveryStatus As Literal = DirectCast(e.Item.FindControl("lDeliveryStatus"), Literal)
        '    If DirectCast(e.Item.FindControl("lReceivedDate"), Literal).Text.Trim <> "" Then
        '        Dim lCreatedDateTime As String = "" 'CDate(DirectCast(e.Item.FindControl("lCreatedDate"), Literal).Text.Trim).ToShortTimeString
        '        Dim dtCreatedDate As DateTime = CDate(DirectCast(e.Item.FindControl("lCreatedDate"), Literal).Text.Trim)
        '        Dim dtReceivedDate As DateTime = CDate(DirectCast(e.Item.FindControl("lReceivedDate"), Literal).Text.Trim)
        '        Dim lReceivedTime As String = CDate(DirectCast(e.Item.FindControl("lReceivedDate"), Literal).Text.Trim).ToShortTimeString
        '        Dim lStatus As String = DirectCast(e.Item.FindControl("lStatus"), Literal).Text.Trim
        '        Dim lDuration As Literal = DirectCast(e.Item.FindControl("lDuration"), Literal)
        '        Dim lCuttoff As Literal = DirectCast(e.Item.FindControl("lCuttoff"), Literal)

        '        If IsBetweenMorningCutOff(dtCreatedDate) Then
        '            lCreatedDateTime = dtCreatedDate.ToShortDateString & " " & tbCutOffMorning.Text
        '            lCuttoff.Text = lCreatedDateTime.ToString
        '            If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate, lDuration, lDeliveryStatus) Then
        '                ctrOnTime = ctrOnTime + 1
        '            Else
        '                ctrNotOnTime = ctrNotOnTime + 1
        '            End If
        '        ElseIf IsBetweenLunchCutOff(dtCreatedDate) Then
        '            lCreatedDateTime = dtCreatedDate.ToShortDateString & " " & tbLunchTo.Text
        '            lCuttoff.Text = lCreatedDateTime.ToString
        '            If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate, lDuration, lDeliveryStatus) Then
        '                ctrOnTime = ctrOnTime + 1
        '            Else
        '                ctrNotOnTime = ctrNotOnTime + 1
        '            End If
        '        ElseIf IsBetweenAfternoonCutOff(dtCreatedDate) Then
        '            lCreatedDateTime = DateAdd(DateInterval.Day, 1, dtCreatedDate).ToShortDateString & " " & tbCutOffMorning.Text
        '            lCuttoff.Text = lCreatedDateTime.ToString
        '            If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate, lDuration, lDeliveryStatus) Then
        '                ctrOnTime = ctrOnTime + 1
        '            Else
        '                ctrNotOnTime = ctrNotOnTime + 1
        '            End If
        '        Else
        '            'lCreatedDateTime = dtCreatedDate.ToShortDateString
        '            lCuttoff.Text = DateAdd(DateInterval.Minute, CInt(tbDeliveryTime.Text), dtCreatedDate).ToString
        '            If IsOnTime(dtCreatedDate, dtCreatedDate, dtReceivedDate, lDuration, lDeliveryStatus) Then
        '                ctrOnTime = ctrOnTime + 1
        '            Else
        '                ctrNotOnTime = ctrNotOnTime + 1
        '            End If
        '        End If
        '    Else
        '        lDeliveryStatus.Text = "Others"
        '        ctrOther = ctrOther + 1
        '    End If
        'ElseIf e.Item.ItemType = ListItemType.Footer Then
        '    SetSummary()
        '    'lDeliveredOnTime.Text = ctrOnTime.ToString("#,##0")
        '    'lDeliveredLate.Text = ctrNotOnTime.ToString("#,##0")
        '    'lSentByOthers.Text = ctrOther.ToString("#,##0")
        '    'lDeliveredByCrd.Text = (ctrOnTime + ctrNotOnTime).ToString("#,##0")
        '    'lTotalDocuments.Text = (ctrOther + ctrOnTime + ctrNotOnTime).ToString("#,##0")
        'End If
    End Sub
    Public Function IsOnTime(ByVal CreatedDate As DateTime, ByVal OrigCreatedDate As DateTime, ByVal ReceivedDate As DateTime, ByVal aDuration As Literal, ByVal aDeliveryStatus As Literal) As Boolean
        'Dim liTotalMin As Integer = 0
        If CreatedDate > ReceivedDate Then
            liTotalMin = 0
        Else
            liTotalMin = DateDiff(DateInterval.Minute, CreatedDate, ReceivedDate)
        End If


        Dim lbRet As Boolean = False
        Dim iDeliveryTime As Integer = IIf(IsNumeric(tbDeliveryTime.Text), CInt(tbDeliveryTime.Text), 60)
        If liTotalMin <= 0 Then
            aDuration.Text = "n/a"
            aDeliveryStatus.Text = "On Time"
            lbRet = True
        ElseIf liTotalMin <= iDeliveryTime Then

            If liTotalMin = iDeliveryTime Then
                aDuration.Text = "01:00"
            Else
                aDuration.Text = "00:" & Right("0" & liTotalMin.ToString, 2)
            End If
            aDeliveryStatus.Text = "On Time"
            lbRet = True
        Else
            Dim holidayctr As Integer = CountHolidays(CreatedDate.ToShortDateString, ReceivedDate.ToShortDateString)
            If holidayctr > 0 Then
                Dim HolidayMin As Integer = holidayctr * 24 * 60
                liTotalMin = liTotalMin - HolidayMin
                If liTotalMin <= 60 Then

                    aDeliveryStatus.Text = "On Time"
                    lbRet = True
                Else
                    aDeliveryStatus.Text = "Late"
                    lbRet = False
                End If
            Else
                aDeliveryStatus.Text = "Late"
                lbRet = False

            End If
            Dim liHr As Integer = Math.Floor(liTotalMin / 60)
            Dim liMin As Integer = IIf(liTotalMin < 60, liTotalMin, liTotalMin Mod 60)
            aDuration.Text = Right("0" & liHr.ToString, 2) & ":" & Right("0" & liMin.ToString, 2)
        End If

        
        Return lbRet
    End Function
    Public Function IsOnTime(ByVal CreatedDate As DateTime, ByVal OrigCreatedDate As DateTime, ByVal ReceivedDate As DateTime) As Boolean

        If CreatedDate > ReceivedDate Then
            liTotalMin = 0
        Else
            liTotalMin = DateDiff(DateInterval.Minute, CreatedDate, ReceivedDate)
        End If


        Dim lbRet As Boolean = False
        Dim iDeliveryTime As Integer = IIf(IsNumeric(tbDeliveryTime.Text), CInt(tbDeliveryTime.Text), 60)
        If liTotalMin <= 0 Then
            'aDuration.Text = "n/a"
            'aDeliveryStatus.Text = "On Time"
            lbRet = True
        ElseIf liTotalMin <= iDeliveryTime Then

            'If liTotalMin = iDeliveryTime Then
            'aDuration.Text = "01:00"
            'Else
            'aDuration.Text = "00:" & Right("0" & liTotalMin.ToString, 2)
            'End If
            'aDeliveryStatus.Text = "On Time"
            lbRet = True
        Else
            Dim holidayctr As Integer = CountHolidays(CreatedDate.ToShortDateString, ReceivedDate.ToShortDateString)
            If holidayctr > 0 Then
                Dim HolidayMin As Integer = holidayctr * 24 * 60
                liTotalMin = liTotalMin - HolidayMin
                If liTotalMin <= 60 Then

                    'aDeliveryStatus.Text = "On Time"
                    lbRet = True
                Else
                    'aDeliveryStatus.Text = "Late"
                    lbRet = False
                End If
            Else
                'aDeliveryStatus.Text = "Late"
                lbRet = False

            End If
            'Dim liHr As Integer = Math.Floor(liTotalMin / 60)
            'Dim liMin As Integer = IIf(liTotalMin < 60, liTotalMin, liTotalMin Mod 60)
            'aDuration.Text = Right("0" & liHr.ToString, 2) & ":" & Right("0" & liMin.ToString, 2)
        End If


        Return lbRet
    End Function
    Public Function IsBetweenMorningCutOff(ByVal CreatedDate As DateTime) As Boolean
        Return CreatedDate.TimeOfDay >= CDate(CreatedDate.ToShortDateString & " 12:00 AM ").TimeOfDay AndAlso _
            CreatedDate.TimeOfDay <= CDate(CreatedDate.ToShortDateString & " " & tbCutOffMorning.Text).TimeOfDay
    End Function
    Public Function IsBetweenLunchCutOff(ByVal CreatedDate As DateTime) As Boolean
        Return CreatedDate.TimeOfDay >= CDate(CreatedDate.ToShortDateString & " " & tbLunchFrom.Text).TimeOfDay AndAlso _
            CreatedDate.TimeOfDay <= CDate(CreatedDate.ToShortDateString & " " & tbLunchTo.Text).TimeOfDay
    End Function
    Public Function IsBetweenAfternoonCutOff(ByVal CreatedDate As DateTime) As Boolean
        Return CreatedDate.TimeOfDay >= CDate(CreatedDate.ToShortDateString & " " & tbCutOffAfternoon.Text).TimeOfDay AndAlso _
            CreatedDate.TimeOfDay <= CDate(CreatedDate.ToShortDateString & " 11:59 PM ").TimeOfDay
    End Function

    Private Sub tbSelectedDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSelectedDate.TextChanged
        LoadMonitoring()
        SaveCookies()
    End Sub

    Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        LoadMonitoring()
        SaveCookies()
    End Sub

    Private Sub btUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpload.Click
        Dim ldata As DataTable
        Try
            Dim oCSV As New ConvertCSV
            oCSV.pComma = tbSeparator.Text
            ldata = oCSV.uf_read_csv_dtl(fUpload)
            If ldata.Rows.Count = 0 Then
                Master.ShowMessage("Import file is empty. Please load another file.")
            ElseIf ldata.Columns.Count < 7 Then
                Master.ShowMessage("Import file columns is incomplete. Please the csv file.")
            Else
                Dim lsrefno, rby, rdate, rtime As String
                Dim oDoc As New DocList
                Dim outDate As DateTime
                Dim liCtr As Integer = 0
                Dim lsMsg As String
                For Each drows As DataRow In ldata.Rows
                    lsMsg = ""
                    If ldata.Rows.IndexOf(drows) <> 0 Then
                        lsrefno = drows(2).ToString.Trim()
                        If lsrefno <> "" Then

                            rby = drows(6).ToString.Trim()
                            rdate = drows(7).ToString.Trim()
                            rtime = drows(8).ToString.Trim()
                            oDoc.pRefNo = lsrefno
                            oDoc.pReceivedBy = IIf(rby = "", drows(5).ToString.Trim(), rby)
                            oDoc.pReceivedDate = IIf(rdate = "", drows(4).ToString.Trim(), rdate)
                            oDoc.pReceivedTime = rtime
                            oDoc.pUserId = DocSession.sUserId
                            oDoc.pDocId = drows(0).ToString.Trim()
                            oDoc.pCreatedDate = drows(3).ToString.Trim()
                            oDoc.pGroupId = tbGroupCode.Text
                            If drows(1).ToString.Trim().ToLower <> tbGroupCode.Text.Trim.ToLower Then
                                oDoc.pComment = "Other"
                                oDoc.pDocAge = "0"

                                pCutOffDate = DateAdd(DateInterval.Minute, CInt(tbDeliveryTime.Text), CDate(oDoc.pCreatedDate)).ToString
                            Else
                                oDoc.pComment = DailyMonitoringStatus(oDoc.pReceivedDate & " " & oDoc.pReceivedTime, oDoc.pCreatedDate)
                                oDoc.pDocAge = liTotalMin.ToString
                            End If
                            
                            oDoc.pCutOffDate = pCutOffDate
                            oDoc.UpdateDocMonitoringReceiving()
                            If rby <> "" AndAlso rdate <> "" AndAlso rtime <> "" Then
                                If DateTime.TryParse(rdate & " " & rtime, outDate) Then
                                    oDoc.pReceivedBy = rby
                                    oDoc.pReceivedDate = rdate
                                    oDoc.UpdateDocReceivedBy()
                                    lsMsg = "OK"
                                Else

                                    lsMsg = "Invalid date time " & rdate & " " & rtime
                                End If
                            Else


                                If rby = "" AndAlso rdate = "" AndAlso rtime = "" Then
                                    lsMsg = "<Not Imported>"
                                Else
                                    If rby = "" Then
                                        lsMsg = lsMsg & "<Empty Received By>"

                                    End If
                                    If rdate = "" Then
                                        lsMsg = lsMsg & "<Empty Received Date>"

                                    End If
                                    If rtime = "" Then
                                        lsMsg = lsMsg & "<Empty Received Time>"

                                    End If

                                End If

                            End If

                        End If

                    Else
                        lsMsg = "Import Remarks"
                    End If
                    ldata(liCtr)(9) = lsMsg
                    liCtr = liCtr + 1
                Next
                If lsrefno <> "" Then
                    Master.ShowMessage("File has been imported. Please see below for the results.")
                End If
                If ldata.Rows.Count > 0 Then
                    pView.Visible = True
                    GridView1.DataSource = ldata
                    GridView1.DataBind()
                    pnlView.Update()
                    LoadMonitoring()
                End If
            End If
        Catch ex As Exception
            Master.ShowMessage("An error occurred while updating received by (" & ex.Message & ").")
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    Private Function DailyMonitoringStatus(ByVal asReceivedDate As String, ByVal asCreateDate As String) As String
        Dim lsRet As String
        If asReceivedDate.Trim <> "" Then
            Dim lCreatedDateTime As String = "" 'CDate(DirectCast(e.Item.FindControl("lCreatedDate"), Literal).Text.Trim).ToShortTimeString
            Dim dtCreatedDate As DateTime = CDate(asCreateDate)
            Dim dtReceivedDate As DateTime = CDate(asReceivedDate)
            Dim lReceivedTime As String = CDate(asReceivedDate).ToShortTimeString
            'Dim lStatus As String = DirectCast(e.Item.FindControl("lStatus"), Literal).Text.Trim
            'Dim lDuration As Literal = DirectCast(e.Item.FindControl("lDuration"), Literal)
            'Dim lCuttoff As Literal = DirectCast(e.Item.FindControl("lCuttoff"), Literal)

            If IsBetweenMorningCutOff(dtCreatedDate) Then
                lCreatedDateTime = dtCreatedDate.ToShortDateString & " " & tbCutOffMorning.Text
                pCutOffDate = lCreatedDateTime.ToString
                If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate) Then
                    ctrOnTime = ctrOnTime + 1
                    lsRet = "On Time"
                Else
                    ctrNotOnTime = ctrNotOnTime + 1
                    lsRet = "Late"
                End If
            ElseIf IsBetweenLunchCutOff(dtCreatedDate) Then
                lCreatedDateTime = dtCreatedDate.ToShortDateString & " " & tbLunchTo.Text
                pCutOffDate = lCreatedDateTime.ToString
                If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate) Then
                    ctrOnTime = ctrOnTime + 1
                    lsRet = "On Time"
                Else
                    ctrNotOnTime = ctrNotOnTime + 1
                    lsRet = "Late"
                End If
            ElseIf IsBetweenAfternoonCutOff(dtCreatedDate) Then
                lCreatedDateTime = DateAdd(DateInterval.Day, 1, dtCreatedDate).ToShortDateString & " " & tbCutOffAfternoon.Text
                pCutOffDate = lCreatedDateTime.ToString
                If IsOnTime(CDate(lCreatedDateTime), dtCreatedDate, dtReceivedDate) Then
                    ctrOnTime = ctrOnTime + 1
                    lsRet = "On Time"
                Else
                    ctrNotOnTime = ctrNotOnTime + 1
                    lsRet = "Late"
                End If
            Else
                'lCreatedDateTime = dtCreatedDate.ToShortDateString
                pCutOffDate = DateAdd(DateInterval.Minute, CInt(tbDeliveryTime.Text), dtCreatedDate).ToString
                If IsOnTime(dtCreatedDate, dtCreatedDate, dtReceivedDate) Then
                    ctrOnTime = ctrOnTime + 1
                    lsRet = "On Time"
                Else
                    ctrNotOnTime = ctrNotOnTime + 1
                    lsRet = "Late"
                End If
            End If
        Else
            'lDeliveryStatus.Text = "Others"
            liTotalMin = 0
            lsRet = "Other"
            ctrOther = ctrOther + 1
        End If

        Return lsRet
    End Function
    Private Sub imgDownload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDownload.Click
        
        If (tbSelectedDate.Text.Trim = "" OrElse Not IsDate(tbSelectedDate.Text.Trim)) Then
            Master.ShowMessage("Please provide a valid date.")
            Exit Sub
        End If
        

        DocSession.rpt_StartDate = tbSelectedDate.Text.Trim & " 00:00:00 "
        DocSession.rpt_EndDate = tbSelectedDate.Text.Trim & " 23:59:59 "

        DataTable2CSV(RetrieveUploaded, "uploadedhourly.csv", ",")
    End Sub
    Private Function RetrieveUploaded() As DataTable
        Try
            Dim oDoc As DocList
            oDoc = New DocList
            'oDoc.pOfficeCode = dlOfficeCode.SelectedValue
            Return oDoc.RetrieveUploadedHourly()

        Catch ex As Exception

        End Try
    End Function

    Private Sub DataTable2CSV(ByVal table As DataTable, ByVal filename As String, ByVal sepChar As String)
        'Dim writer As System.IO.StreamWriter
        'writer = New System.IO.StreamWriter(filename)
        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)

        Response.ContentType = "text/plain"
        ' first write a line with the columns name
        Dim sep As String = ""
        sepChar = tbSeparator.Text
        Dim builder, builderdata As System.Text.StringBuilder
        builder = New System.Text.StringBuilder
        builderdata = New System.Text.StringBuilder
        For Each col As DataColumn In table.Columns
            builderdata.Append(sep).Append(col.ColumnName)
            sep = sepChar
        Next
        builder.AppendLine(builderdata.ToString)
        'writer.WriteLine(builder.ToString())


        ' then write all the rows
        For Each row As DataRow In table.Rows
            sep = ""
            builderdata = New System.Text.StringBuilder

            For Each col As DataColumn In table.Columns
                builderdata.Append(sep).Append(row(col.ColumnName))
                sep = sepChar
            Next
            builder.AppendLine(builderdata.ToString)
        Next
        Response.Write(builder.ToString())
        Response.Flush()
        Response.End()

    End Sub
    Private Sub imgUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpload.Click
        pnlUpload.Visible = Not pnlUpload.Visible
        pView.Visible = False
        pnlView.Update()
    End Sub

    Private Sub btGenReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btGenReport.Click
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "DocRecvStatRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DocReceivingReport.aspx', 'DocRecvViewStatRpt' ,'location=no,toolbar=no,menubar=yes,status=yes,height=650, width=1000,left=20, top=20, resizable=yes, scrollbars=yes')</script>"

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DocRecvStatRpt", lsScript, False)

        End If
    End Sub

    Private Sub rptSummary_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lDeliveryStatus As Literal = DirectCast(e.Item.FindControl("lDeliveryStatus"), Literal)
            Dim lpercent As Literal = DirectCast(e.Item.FindControl("lpercentage"), Literal)
            If DirectCast(e.Item.FindControl("lOrder"), Literal).Text <> "3" Then
                ctrTotalCRD = ctrTotalCRD + CInt(DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text)
                If DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text <> "" AndAlso CInt(DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text) > 0 Then
                    lpercent.Text = CStr(Math.Round(CInt(DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text) / CInt(DirectCast(e.Item.FindControl("ltotal"), Literal).Text) * 100, 0)) & "%"
                Else
                    lpercent.Text = "0%"
                End If
            Else
                lpercent.Text = "100%"
            End If

            ctrTotalSummary = ctrTotalSummary + CInt(DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text)
            DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text = CInt(DirectCast(e.Item.FindControl("lsubtotal"), Literal).Text).ToString("#,##0")
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            'SetSummary()
            DirectCast(e.Item.FindControl("ltotaldelivered"), Literal).Text = ctrTotalCRD.ToString("#,##0")
            DirectCast(e.Item.FindControl("lTotalDocs"), Literal).Text = ctrTotalSummary.ToString("#,##0")

        End If
    End Sub
End Class