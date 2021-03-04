Imports System
Imports System.Data.SqlClient

Public Class dashboard
    Inherits System.Web.UI.Page
#Region "Properties"
    Public Property pAction As String
        Get
            Return hfAction.Value
        End Get
        Set(ByVal value As String)
            hfAction.Value = value
        End Set
    End Property

    Public Property pSortOrder As String
        Get
            Return hfSortOrder.Value
        End Get
        Set(ByVal value As String)
            hfSortOrder.Value = value
        End Set
    End Property

    Public Property pSortCol As String
        Get
            Return hfSortCol.Value
        End Get
        Set(ByVal value As String)
            hfSortCol.Value = value
        End Set
    End Property
#End Region
#Region "Page Event"
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucDB.e_action, AddressOf RetSortAction
        AddHandler ucDBCount.e_completequick, AddressOf RetCompleteQuick
        AddHandler ucDBCount.e_completelong, AddressOf RetCompleteLong
        AddHandler ucDBCount.e_pendingoldest, AddressOf RetPendingOldest
        AddHandler ucDBCount.e_pendingnewest, AddressOf RetPendingNewest
        AddHandler ucDBCount.e_showBureauPending, AddressOf RetrieveBureauPending
        AddHandler ucDBCount.e_showBureauComplete, AddressOf RetrieveBureauComplete
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage
        'GetCookies()
        Master.SelectTab("Dashboard")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Try

           
            tbDCFrom.Text = "01/01/" & Year(Date.Now)
            tbDCTo.Text = DateTime.Now.ToShortDateString

            'GetCookies()
            RetAction()
            RetrieveCounts()
            'RetrieveStatus()
            RetrieveGroups()
            GetStatus()
            If DocSession.sUserRole = "A" Then
                lHdrTitle.Text = "All"
            Else
                If DocSession.sUserRole = "U" Then
                    trPersonnel.Visible = False
                End If

                lHdrTitle.Text = DocSession.sOfcName
                pnlGroup.Visible = False
            End If

            If DocSession.sDocAccess > 2 Then
                ucAddDoc.Visible = True
            Else
                ucAddDoc.Visible = False
                End If
            Catch ex As Exception
                Master.ShowMessage(ex.Message)
            End Try
        End If
    End Sub
#End Region
    
    Private Sub ShowUploadMessage()
        Master.ShowMessage(ucUpload.Message)
    End Sub
    Private Sub AddDoc()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub

    Public Sub RetAction()
        'hfCurrent.Value = "1"

        'ucDB.pTask = dlActivities.SelectedValue
        Try

            ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.pADate = txDateActivity.Text
        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = hfAction.Value
        ucDB.pSortCol = hfSortCol.Value
        ucDB.pSortOrder = hfSortOrder.Value
        ucDB.pRefNo = txtRefNo.Text
        ucDB.pPersonnelInCharge = txtPersonnelInCharge.Text
        ucDB.pDueDate = txtDueDate.Text
        ucDB.pSubject = txtSubject.Text
        ucDB.pStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
        ucDB.RetrieveAction(hfOfcCode.Value)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        'upDB.Update()
        upPgr.Update()
        Catch ex As Exception
            Master.ShowMessage("Error in retrieving info (" & ex.Message & "). Please try againg.")
        End Try
    End Sub

    Public Sub RetrieveCounts()
        'hfCurrent.Value = "1"
        'ucDBCount.pIdx = CInt(hfCurrent.Value)
        'ucDB.pTask = dlActivities.SelectedValue
        Try
            'ucDB.pADate = txDateActivity.Text
            ucDBCount.pFromDate = tbDCFrom.Text
            ucDBCount.pToDate = tbDCTo.Text
            'ucDBCount.pAction = hfAction.Value
            'ucDBCount.pSortCol = hfSortCol.Value
            ucDBCount.pOfcCode = hfOfcCode.Value
            ucDBCount.pRefNo = txtRefNo.Text
            ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
            ucDBCount.pDueDate = txtDueDate.Text
            ucDBCount.pSubject = txtSubject.Text
            ucDBCount.pStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
            ucDBCount.RetrieveAction()
            ucDBCount.ResetChart()
            If DocSession.sUserRole <> "A" Then
                ucDBCount.HideGraph()
            End If
        Catch ex As Exception
            Master.ShowMessage("Error in retrieving dashboard data (" & ex.Message & "). Please try again.")
        End Try
    End Sub

    Public Sub RetSortAction()
        hfCurrent.Value = "1"
        hfSortCol.Value = ucDB.pSortCol
        hfSortOrder.Value = ucDB.pSortOrder
        upPgr.Update()
        RetAction()
    End Sub
    Public Sub RetCompleteLong()
        pAction = "C"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.pTask = dlActivities.SelectedValue

        'ucDB.pADate = txDateActivity.Text
        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction

        ucDB.sortColumnHeader("8", "Desc", "Age2")

        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        ResetFilter()
        ucDB.RetrieveAction(hfOfcCode.Value)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()

    End Sub

    Public Sub RetrieveBureauComplete()
        pAction = "C"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)

        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction

        ucDB.sortColumnHeader("8", "Asc", "Age2")
        ResetFilter()
        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        lHdrTitle.Text = ucDBCount.pBureau
        hfOfcCode.Value = ucDBCount.pOfficeCode
        ucDB.RetrieveAction(ucDBCount.pOfficeCode)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()
        ucDBCount.pFromDate = tbDCFrom.Text
        ucDBCount.pToDate = tbDCTo.Text
        ucDBCount.pOfcCode = hfOfcCode.Value

        ucDBCount.pRefNo = txtRefNo.Text
        ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
        ucDBCount.pDueDate = txtDueDate.Text
        ucDBCount.pSubject = txtSubject.Text
        ucDBCount.pStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
        ucDBCount.RetrieveAction()
        ucDBCount.ResetChart()
        'ucDBCount.LoadDistributionChart()
    End Sub

    Public Sub ResetFilter()
        dlStatus.SelectedValue = "0"
        txtDueDate.Text = ""
        txtRefNo.Text = ""
        txtSubject.Text = ""
        txtPersonnelInCharge.Text = ""
    End Sub
    Public Sub RetrieveBureauPending()
        pAction = "P"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)

        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction

        ucDB.sortColumnHeader("8", "Desc", "Age")

        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        lHdrTitle.Text = ucDBCount.pBureau
        hfOfcCode.Value = ucDBCount.pOfficeCode
        ResetFilter()
        ucDB.RetrieveAction(ucDBCount.pOfficeCode)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()
        ucDBCount.pFromDate = tbDCFrom.Text
        ucDBCount.pToDate = tbDCTo.Text
        ucDBCount.pOfcCode = hfOfcCode.Value
        ucDBCount.pRefNo = txtRefNo.Text
        ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
        ucDBCount.pDueDate = txtDueDate.Text
        ucDBCount.pSubject = txtSubject.Text
        ucDBCount.pStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
        ucDBCount.RetrieveAction()
        ucDBCount.ResetChart()
        'ucDBCount.LoadDistributionChart()
    End Sub

    Public Sub RetCompleteQuick()
        pAction = "C"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.pTask = dlActivities.SelectedValue

        'ucDB.pADate = txDateActivity.Text
        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction

        ucDB.sortColumnHeader("8", "Asc", "Age2")

        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        ResetFilter()
        ucDB.RetrieveAction(hfOfcCode.Value)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()

    End Sub

    Public Sub RetPendingNewest()
        hfAction.Value = "P"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.pTask = dlActivities.SelectedValue

        'ucDB.pADate = txDateActivity.Text
        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction

        ucDB.sortColumnHeader("8", "Asc", "Age")

        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        ResetFilter()
        ucDB.RetrieveAction(hfOfcCode.Value)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()

    End Sub
    Public Sub RetPendingOldest()
        hfAction.Value = "P"
        'dlStatus.SelectedValue = ""
        hfCurrent.Value = "1"
        ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.pTask = dlActivities.SelectedValue

        'ucDB.pADate = txDateActivity.Text
        ucDB.pFromDate = tbDCFrom.Text
        ucDB.pToDate = tbDCTo.Text
        ucDB.pAction = pAction
        ucDB.sortColumnHeader("8", "Desc", "Age")

        pSortOrder = ucDB.pSortOrder
        pSortCol = ucDB.pSortCol
        ResetFilter()
        ucDB.RetrieveAction(hfOfcCode.Value)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()

    End Sub
#Region "pager: step 3"
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
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
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub
#End Region

    'Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    '    Try


    '        If Not IsPostBack Then
    '            RetrieveAction()
    '        End If
    '    Catch ex As Exception
    '        Master.ShowMessage("An error occurred while processing your request (" & ex.Message & "). Please try again.")
    '    End Try
    'End Sub


    Private Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        Try


            'If Not IsPostBack Then
            '    ucDBCount.pFromDate = tbDCFrom.Text
            '    ucDBCount.pToDate = tbDCTo.Text
            '    ucDBCount.pOfcCode = hfOfcCode.Value
            '    ucDBCount.RetrieveAction()
            '    ucDBCount.LoadChartDays()
            '    ucDBCount.LoadDistributionChart()
            '    ucDBCount.LoadTopBureau()
            '    ucDBCount.LoadTotalAmountReleased()
            'End If
        Catch ex As Exception
            Master.ShowMessage("An error occurred while processing your request (" & ex.Message & "). Please try again.")
        End Try
    End Sub
    Private Sub imgRight_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRight.Click
        hfIndex.Value = (CInt(hfIndex.Value) + CInt(hfCount.Value)).ToString
        RetrieveGroups()
    End Sub

    Private Sub imgLeft_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLeft.Click
        hfIndex.Value = (CInt(hfIndex.Value) - CInt(hfCount.Value)).ToString
        RetrieveGroups()
    End Sub
    Private Sub RetrieveGroups()
        Dim loGroup As DocGroup
        Dim lData As DataTable
        Try
            loGroup = New DocGroup
            loGroup.pIdx = hfIndex.Value
            loGroup.pRowsPerPage = hfCount.Value

            lData = loGroup.RetrieveOfficePartially()


            If lData.Rows.Count > CInt(hfCount.Value) Then
                lData.Rows.RemoveAt(hfCount.Value)
                imgRight.Visible = True
            Else
                imgRight.Visible = False
            End If
            If CInt(hfIndex.Value) > 1 Then
                imgLeft.Visible = True
            Else
                imgLeft.Visible = False
            End If
            rptGroups.DataSource = lData
            rptGroups.DataBind()
            SelectGroup()
            pnlGroup.Update()
        Catch ex As Exception
            Master.ShowMessage("Error in retrieving records (" & ex.Message & "). Please try again.")
        Finally
            If Not lData Is Nothing Then
                lData.Dispose()
                lData = Nothing
            End If
        End Try
    End Sub
    Public Sub ResetFields()
        hfCount.Value = "5"
        hfIndex.Value = "1"
        hfAction.Value = "P"
    End Sub
    Public Sub fSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Using lbSort As LinkButton = DirectCast(sender, LinkButton)
            Dim lsGroup = DirectCast(lbSort.Parent.FindControl("lbGroup"), LinkButton).Text
            hfOfcCode.Value = DirectCast(lbSort.Parent.FindControl("lOfcCode"), Literal).Text
            lHdrTitle.Text = lsGroup
            SelectGroup()
            hfCurrent.Value = "1"
            ucDBCount.pRefNo = txtRefNo.Text
            ucDBCount.pStatusId = dlStatus.SelectedValue
            ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
            ucDBCount.pDueDate = txtDueDate.Text
            ucDBCount.pSubject = txtSubject.Text
            ucDBCount.pFromDate = tbDCFrom.Text
            ucDBCount.pToDate = tbDCTo.Text
            ucDBCount.pOfcCode = hfOfcCode.Value

            ucDBCount.RetrieveAction()
            ucDBCount.ResetChart()
            'ucDBCount.LoadDistributionChart()
            RetAction()
        End Using

        'RetrieveGroups()
    End Sub

    Private Sub SelectGroup()
        If lHdrTitle.Text.ToLower = "all" Then
            divGrpAll.Style("background-color") = "#26269B"
            divGrpAll.Style("color") = "#FFFFFF"
        Else

            divGrpAll.Style("background-color") = "Transparent"
            divGrpAll.Style("color") = "#222222"

        End If
        For Each ri In rptGroups.Items
            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                If DirectCast(ri.FindControl("lbGroup"), LinkButton).Text.ToLower = lHdrTitle.Text.ToLower Then
                    DirectCast(ri.FindControl("divGrp"), HtmlGenericControl).Style("background-color") = "#26269B"
                    DirectCast(ri.FindControl("divGrp"), HtmlGenericControl).Style("color") = "#FFFFFF"
                Else
                    DirectCast(ri.FindControl("divGrp"), HtmlGenericControl).Style("color") = "#222222"
                    DirectCast(ri.FindControl("divGrp"), HtmlGenericControl).Style("background-color") = "transparent"
                End If
            End If

        Next

    End Sub

    Private Sub lbGroupAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGroupAll.Click
        hfOfcCode.Value = ""
        lHdrTitle.Text = "All"
        SelectGroup()
        pnlGroup.Update()

        ResetFields()
        RetAction()
        ucDBCount.pFromDate = tbDCFrom.Text
        ucDBCount.pToDate = tbDCTo.Text
        ucDBCount.pOfcCode = hfOfcCode.Value

        ucDBCount.pRefNo = txtRefNo.Text
        ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
        ucDBCount.pDueDate = txtDueDate.Text
        ucDBCount.pSubject = txtSubject.Text
        ucDBCount.pStatusId = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
        ucDBCount.RetrieveAction()
        ucDBCount.ResetChart()
    End Sub
    Private Sub GetStatus()
        Dim oDoc As New DocTypes
        Dim ldata As DataTable
        Try
            ldata = oDoc.GetDocStatus


            Dim lrow As DataRow
            If ldata.Rows.Count > 0 Then
                lrow = ldata.NewRow
                lrow("statusid") = "0"
                lrow("description") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
            End If

            dlStatus.DataTextField = "description"
            dlStatus.DataValueField = "statusid"
            dlStatus.DataSource = ldata

            dlStatus.DataBind()
        Catch ex As Exception
            Master.ShowMessage("Error in getting status (" & ex.Message & "). Please try again.")
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        SearchRecords()
    End Sub
    Private Sub SearchRecords()
        'ResetFields()
        hfCurrent.Value = "1"
        'pAction = "P"
        'dlStatus.SelectedValue = ""
        'SetCookies(pAction)
        RetAction()
        'ucDBCount.pFromDate = tbDCFrom.Text
        'ucDBCount.pToDate = tbDCTo.Text
        'ucDBCount.pOfcCode = hfOfcCode.Value
        'ucDBCount.RetrieveAction()
        'ucDBCount.LoadChartDays()
        'pnlSearch.Update()
        'RetAction()
        ucDBCount.pPersonnelInCharge = txtPersonnelInCharge.Text
        ucDBCount.pStatusId = dlStatus.SelectedValue
        ucDBCount.pDueDate = txtDueDate.Text
        ucDBCount.pRefNo = txtRefNo.Text
        ucDBCount.pSubject = txtSubject.Text
        ucDBCount.pFromDate = tbDCFrom.Text
        ucDBCount.pToDate = tbDCTo.Text
        ucDBCount.pOfcCode = hfOfcCode.Value
        ucDBCount.RetrieveAction()
        ucDBCount.pReset = "000000"
        ucDBCount.ResetChart()
        'ucDBCount.LoadDistributionChart()
        'ucDBCount.LoadChartDays()
        'ucDBCount.LoadTopBureau()
        'ucDBCount.LoadTotalAmountReleased()
    End Sub

    Private Sub btFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btFilter.Click
        SearchRecords()
    End Sub

    Private Sub imgPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPrint.Click
        'SetCookies(pAction)
        If Not ClientScript.IsStartupScriptRegistered(Me.GetType(), "DashboardRpt") Then
            Dim lsScript As String = "<script type='text/javascript'>window.open('DashboardDoclist.aspx', 'DashboardViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "DashboardRpt", lsScript, False)

        End If
    End Sub

    'Private Sub SetCookies(ByVal asValue As String)
    '    Dim mycookie As HttpCookie = New HttpCookie("udrDDLOfficeCode")
    '    mycookie.Value = hfOfcCode.Value
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLAction")
    '    mycookie.Value = asValue
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)


    '    mycookie = New HttpCookie("udrDDLRangeFrom")
    '    mycookie.Value = tbDCFrom.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLRangeTo")
    '    mycookie.Value = tbDCTo.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLSortCol")
    '    mycookie.Value = hfSortCol.Value
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLSortOrder")
    '    mycookie.Value = hfSortOrder.Value
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLRefno")
    '    mycookie.Value = txtRefNo.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLSubject")
    '    mycookie.Value = txtSubject.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLDueDate")
    '    mycookie.Value = txtDueDate.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLPersonnelInCharge")
    '    mycookie.Value = txtPersonnelInCharge.Text
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)

    '    mycookie = New HttpCookie("udrDDLStatusId")
    '    mycookie.Value = IIf(dlStatus.SelectedValue = "0", "", dlStatus.SelectedValue)
    '    mycookie.Expires = DateTime.Now.AddDays(30)
    '    Response.Cookies.Add(mycookie)


    'End Sub

    'Private Sub GetCookies()
    '    If Not Request.Cookies("udrDDLOfficeCode") Is Nothing Then
    '        hfOfcCode.Value = Request.Cookies("udrDDLOfficeCode").Value
    '    End If

    '    If Not Request.Cookies("udrDDLRangeFrom") Is Nothing Then
    '        tbDCFrom.Text = Request.Cookies("udrDDLRangeFrom").Value
    '    End If

    '    If Not Request.Cookies("udrDDLRangeTo") Is Nothing Then
    '        tbDCTo.Text = Request.Cookies("udrDDLRangeTo").Value
    '    End If

    '    If Not Request.Cookies("udrDDLSortCol") Is Nothing Then
    '        hfSortCol.Value = Request.Cookies("udrDDLSortCol").Value
    '    End If

    '    If Not Request.Cookies("udrDDLSortOrder") Is Nothing Then
    '        hfSortOrder.Value = Request.Cookies("udrDDLSortOrder").Value
    '    End If

    '    If Not Request.Cookies("udrDDLRefno") Is Nothing Then
    '        txtRefNo.Text = Request.Cookies("udrDDLRefno").Value
    '    End If

    '    If Not Request.Cookies("udrDDLSubject") Is Nothing Then
    '        txtSubject.Text = Request.Cookies("udrDDLSubject").Value
    '    End If

    '    If Not Request.Cookies("udrDDLDueDate") Is Nothing Then
    '        txtDueDate.Text = Request.Cookies("udrDDLDueDate").Value
    '    End If

    '    If Not Request.Cookies("udrDDLPersonnelInCharge") Is Nothing Then
    '        txtPersonnelInCharge.Text = Request.Cookies("udrDDLPersonnelInCharge").Value
    '    End If

    '    If Not Request.Cookies("udrDDLStatusId") Is Nothing Then
    '        dlStatus.SelectedValue = IIf(Request.Cookies("udrDDLStatusId").Value.Trim = "", "0", Request.Cookies("udrDDLStatusId").Value)
    '    End If
    'End Sub

    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click
        SearchRecords()
    End Sub
End Class