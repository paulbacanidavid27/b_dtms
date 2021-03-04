Public Class UserControlApproval
    Inherits System.Web.UI.UserControl
    Public Event e_ShowMessage()
    Public Event e_UpdateCount()

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            UpdateApproval()
            RetrieveApproval()
        End If
    End Sub
#Region "Pager Section"
    Public Sub RetAction()
        DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveApproval()
        'pnlPending.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub
    'pager: step 3
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

    'Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

    '    Dim img As Image
    '    hfCurrent.Value = "1"
    '    DocSession.doc_DocCurrentPage = hfCurrent.Value
    '    If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort1
    '        imgSort1.Visible = True
    '    Else
    '        imgSort1.Visible = False
    '    End If
    '    If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort2
    '        imgSort2.Visible = True
    '    Else
    '        imgSort2.Visible = False

    '    End If
    '    If imgSort3.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort3
    '        imgSort3.Visible = True
    '    Else
    '        imgSort3.Visible = False

    '    End If
    '    If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort4
    '        imgSort4.Visible = True
    '    Else
    '        imgSort4.Visible = False

    '    End If
    '    If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort5
    '        imgSort5.Visible = True
    '    Else
    '        imgSort5.Visible = False

    '    End If

    '    If imgSort6.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort6
    '        imgSort6.Visible = True
    '    Else
    '        imgSort6.Visible = False
    '    End If

    '    If imgSort7.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort7
    '        imgSort7.Visible = True
    '    Else
    '        imgSort7.Visible = False
    '    End If
    '    If imgSort8.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort8
    '        imgSort8.Visible = True
    '    Else
    '        imgSort8.Visible = False
    '    End If
    '    If imgSort9.ID = "imgSort" & Right(lbSort.ID, 1) Then
    '        img = imgSort9
    '        imgSort9.Visible = True
    '    Else
    '        imgSort9.Visible = False
    '    End If

    '    'Dim oDocList As New DocList

    '    If img.Visible Then
    '        If img.ImageUrl.ToLower = "images/asc.png" Then
    '            img.ImageUrl = "images/desc.png"
    '            'oDocList.pSortOrder = "Desc"
    '            DocSession.doc_SortOrder = "Desc"
    '        Else
    '            img.ImageUrl = "images/asc.png"
    '            'oDocList.pSortOrder = "Asc"
    '            DocSession.doc_SortOrder = "Asc"
    '        End If
    '    Else
    '        img.ImageUrl = "images/asc.png"
    '        'oDocList.pSortOrder = "Asc"

    '        DocSession.doc_SortOrder = "Asc"
    '        img.Visible = True
    '    End If
    '    DocSession.doc_SortCol = lbSort.Text
    '    'oDocList.pDocId = ""
    '    'oDocList.pDocType = dlFilterDocType.SelectedValue
    '    'oDocList.pDocTitle = tbFilterTitle.Text
    '    'oDocList.pDocStatus = dlFilterDocStatus.SelectedValue
    '    'oDocList.pGroupId = DocSession.sUserGroup
    '    'oDocList.pIdx = hfCurrent.Value
    '    'oDocList.pRowsPerPage = DocSession.RowsPerPage
    '    'oDocList.pSortCol = lbSort.Text
    '    'oDocList.pUserId = DocSession.sUserId
    '    'Dim ldata As DataTable = oDocList.RetrieveDocs

    '    'If ldata.Rows.Count > DocSession.RowsPerPage Then
    '    '    'imgGreater.Visible = True
    '    '    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
    '    'Else
    '    '    'imgGreater.Visible = False
    '    'End If

    '    'lMsg.Visible = False
    '    ''hfTotalRows.Value = CStr(retval.pParam.Value)
    '    'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))

    '    'Repeater1.DataSource = ldata

    '    'Repeater1.DataBind()
    '    'pnlRepeater.Update()

    '    RetAction()

    'End Sub
#End Region
    Dim DocTypeList As DataTable
    Public Property pDocTypeList As DataTable
        Get
            Return DocTypeList
        End Get
        Set(ByVal value As DataTable)
            DocTypeList = value
        End Set
    End Property


    Public Sub fQuickRouting(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim ucDoc As UserControlDocRouting = DirectCast(rptItem.FindControl("ucDocRouting"), UserControlDocRouting)
        ucDoc.pDocType = DirectCast(rptItem.FindControl("lDocType"), Literal).Text
        DocSession.sDocType = ucDoc.pDocType
        ucDoc.pDocId = DirectCast(rptItem.FindControl("lDocId"), Literal).Text
        ucDoc.pDocType2 = DirectCast(rptItem.FindControl("lDocType"), Literal).Text
        'ucDoc.pOfficeCode = DirectCast(rptItem.FindControl("lOfficeCode"), Literal).Text
        ucDoc.pSeqNo = DirectCast(rptItem.FindControl("lSeqNo"), Literal).Text
        ucDoc.pRouteStatusId = DirectCast(rptItem.FindControl("lStatusId"), Literal).Text
        ucDoc.pDocTitle = DirectCast(rptItem.FindControl("lTitle"), Label).Text
        ucDoc.pRefno = DirectCast(rptItem.FindControl("lrefno"), Literal).Text
        AddHandler ucDoc.e_UpdateCount, AddressOf RefreshCount
        ucDoc.ShowQuickRouting()


    End Sub
    Public Sub RefreshCount()
        RaiseEvent e_UpdateCount()
    End Sub

    Private Sub UpdateApproval()

        Dim oApprove As DocApproval

        Try
            oApprove = New DocApproval
            oApprove.pApproverId = DocSession.sUserId
            oApprove.pGroupId = DocSession.sUserGroup
            oApprove.ReturnToPendingTask()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not oApprove Is Nothing Then
                oApprove = Nothing
            End If
        End Try

    End Sub

    Private Sub HideSortImage(ByVal rptItem As RepeaterItem)
        'For ctr = 1 To 6
        '    DirectCast(rptItem.FindControl("imgSort" & ctr.ToString), Image).Visible = False

        'Next
        'imgSort1.Visible = False
        'imgSort2.Visible = False
        'imgSort3.Visible = False
        'imgSort4.Visible = False
        'imgSort5.Visible = False
        'imgSort6.Visible = False


    End Sub


    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image

        If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort1
            imgSort1.Visible = True
        Else
            imgSort1.Visible = False
        End If
        If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort2
            imgSort2.Visible = True
        Else
            imgSort2.Visible = False

        End If
        If imgSort3.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort3
            imgSort3.Visible = True
        Else
            imgSort3.Visible = False

        End If
        If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort4
            imgSort4.Visible = True
        Else
            imgSort4.Visible = False

        End If
        If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort5
            imgSort5.Visible = True
        Else
            imgSort5.Visible = False

        End If
        If imgSort6.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort6
            imgSort6.Visible = True
        Else
            imgSort6.Visible = False
        End If

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"

                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"

                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"

            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If


        'If hfSortOrder.Value = "Asc" Then
        '    hfSortOrder.Value = "Desc"
        'Else
        '    hfSortOrder.Value = "Asc"
        'End If

        hfSortCol.Value = lbSort.Text
        RetAction()




    End Sub
    Private Sub RetrieveApproval()

        Dim oApprove As New DocApproval
        Dim lsTotalRows As String
        Dim ldata As DataTable
        Try

            oApprove.pApproverId = DocSession.sUserId
            oApprove.pGroupId = DocSession.sUserGroup
            'page1
            oApprove.pIdx = hfCurrent.Value 'DocSession.doc_DocCurrentPage
            oApprove.pRowsPerPage = CInt(DocSession.RowsPerPage)
            lsTotalRows = oApprove.CountPending
            oApprove.pSortCol = hfSortCol.Value
            oApprove.pSortOrder = hfSortOrder.Value
            ldata = oApprove.RetrievePending

            If ldata.Rows.Count > DocSession.RowsPerPage Then
                'imgGreaterTop.Visible = True
                'imgGreater.Visible = True
                'imgGreaterD.Visible = False
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
                'Else
                '    'imgGreaterTop.Visible = False
                '    imgGreater.Visible = False
                '    imgGreaterD.Visible = True
            Else

            End If
            If ldata.Rows.Count = 0 Then
                'Master.ShowMessage("No records found for the selected filter.")
                ucPager.Visible = False
                pPager.Update()
                pConfidential.Visible = False
            Else
                pConfidential.Visible = True
                hfTotalRows.Value = lsTotalRows 'oDocs.pRetVal
                'Master.ShowMessage(oDocs.pRetVal & " documents found. ")
                'lMsg.Visible = False
                'hfTotalRows.Value = CStr(retval.pParam.Value)

                'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(retval.pParam.Value))
                ucPager.Visible = True
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()
                '    lMsg.Visible = False
                '    If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > CInt(retval.pParam.Value) Then
                '        lPageCount.Text = "Row " & hfCurrent.Value & " -  " & retval.pParam.Value & " of " & retval.pParam.Value
                '    Else
                '        lPageCount.Text = "Row " & hfCurrent.Value & " -  " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) & " of " & retval.pParam.Value
                '    End If
            End If
            Repeater2.DataSource = ldata
            Repeater2.DataBind()
            pnlPending.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub RetrieveGroupStatus()
        Dim oDocs As New DocTypes
        oDocs.pGroupId = DocSession.sUserGroup
        pDocTypeList = oDocs.GetDocStatusByGroup
        Dim lrow As DataRow
        lrow = pDocTypeList.NewRow()
        lrow("StatusId") = "0"
        lrow("Description") = ""
        pDocTypeList.Rows.InsertAt(lrow, 0)

    End Sub
    Private Sub Repeater2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemCreated

        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then


            Dim ib As ImageButton = DirectCast(e.Item.FindControl("imgDetails"), ImageButton)
            Dim ibM As ImageButton = DirectCast(e.Item.FindControl("imgDetailsMinus"), ImageButton)
            Dim ucD As UserControlDocRouting = DirectCast(e.Item.FindControl("ucDocRouting"), UserControlDocRouting)
            AddHandler ib.Click, AddressOf showApprovalStatus
            AddHandler ibM.Click, AddressOf showApprovalStatus
            AddHandler ucD.e_ShowMessage, AddressOf ShowMessage
            AddHandler ucD.e_UpdateCount, AddressOf RefreshCount

        End If

    End Sub

    Private Sub ShowMessage()
        RetrieveApproval()
        RaiseEvent e_ShowMessage()
    End Sub
    Private Sub showApprovalStatus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ib As ImageButton = DirectCast(sender, ImageButton)

        Dim ri As RepeaterItem = DirectCast(ib.NamingContainer, RepeaterItem)
        Dim rptr As Repeater = DirectCast(ri.FindControl("rptApprovalStatus"), Repeater)
        Dim ibD As ImageButton = DirectCast(ri.FindControl("imgDetails"), ImageButton)
        'Dim lStat As Literal = DirectCast(ri.FindControl("lStatusId"), Literal)
        Dim ibM As ImageButton = DirectCast(ri.FindControl("imgDetailsMinus"), ImageButton)
        ibD.Visible = Not ibD.Visible
        ibM.Visible = Not ibM.Visible
        RetrieveGroupStatus()
        rptr.Visible = Not rptr.Visible
        If rptr.Visible Then
            DirectCast(ri.FindControl("pMsg"), Panel).Visible = True
            Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)
            Dim oApp As New DocApproval
            oApp.pDocId = lDocId.Text
            'oApp.pUserId = DocSession.sUserId
            rptr.DataSource = oApp.ApproverStatusQuick
            rptr.DataBind()
        Else
            DirectCast(ri.FindControl("lMsg"), Label).Text = ""
            DirectCast(ri.FindControl("pMsg"), Panel).Visible = False
        End If
        pnlPending.Update()
    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        DocSession.sSelectedTab = "Routing"
        Response.Redirect("view.aspx")
    End Sub
    Public Sub ViewDoc2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        DocSession.sSelectedTab = "Routing"
        Response.Redirect("view.aspx")
    End Sub


    Public Sub RouteDocument(ByVal sender As Object, ByVal e As EventArgs)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")

        Dim oDoc As DocList
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        'If 
        'End If

        'Dim losender As ImageButton = DirectCast(sender, ImageButton)

        Dim fSel As ImageButton
        Dim loRptItem As RepeaterItem = DirectCast(sender.NamingContainer, RepeaterItem)
        'Dim rbAck As RadioButton = DirectCast(loRptItem.FindControl("rbAck"), RadioButton)
        Dim dlstat As DropDownList
        Dim tComment As TextBox = DirectCast(loRptItem.FindControl("tbComment"), TextBox)
        Dim lAppid As Literal = DirectCast(loRptItem.FindControl("lAppid"), Literal)
        Dim lSeqno As Literal = DirectCast(loRptItem.FindControl("lSeqNo"), Literal)
        Dim lStat As Literal = DirectCast(loRptItem.FindControl("lDocStatusId"), Literal)
        Dim lOutStat As Literal = DirectCast(loRptItem.FindControl("lOutStatusId"), Literal)
        Dim lMsg As Label = DirectCast(loRptItem.NamingContainer.NamingContainer.FindControl("lMsg"), Label)
        Dim liCtr As Integer
        Dim lsDocId As String = DirectCast(loRptItem.FindControl("lDocId"), Literal).Text
        Dim bCommit As Boolean = False
        liCtr = 0
        Try
            oDoc = New DocList
            'oDoc.ExistsInInbox()
            Dim lsact As String
            Dim oDocs As New DocRouting
            If DirectCast(loRptItem.FindControl("lCC"), Literal).Text = "1" OrElse DirectCast(loRptItem.FindControl("lCC"), Literal).Text = "True" Then
                dlstat = DirectCast(loRptItem.FindControl("dlStatus2"), DropDownList)
            Else
                dlstat = DirectCast(loRptItem.FindControl("dlStatus"), DropDownList)
            End If

            If dlstat.SelectedValue.Trim = "0" Then
                lMsg.Text = "Status is required. Please select status to proceed."
                lMsg.CssClass = "msg_red"
                'ElseIf tComment.Text.Trim = "" Then
                '    lMsg.Text = "Remarks is required. Please enter your remarks to proceed."
                '    lMsg.CssClass = "msg_red"
            Else
                Dim iStat As Integer

                'If dlstat.SelectedValue = "4" Then
                'iStat = 4
                'lsact = "Set document to 'For Evaluation' status"
                'Else
                'iStat = 3
                'lsact = "Acknowledged document"
                'End If


                'If losender.ID = "imgDeny" Then
                '    iStat = 4
                '    lsact = "Set document to 'For Evaluation' status"
                'Else
                '    iStat = 3
                '    lsact = "Acknowledged document"
                'End If

                objCommand = New clsSqlConn(ltr.pTran)

                oDocs.pComment = tComment.Text
                oDocs.pDocId = lsDocId
                oDocs.pApproverId = lAppid.Text
                oDocs.pOldSeqNo = lSeqno.Text
                oDocs.pStatusId = dlstat.SelectedValue 'iStat 'approved
                oDocs.pAction = dlstat.SelectedItem.Text 'lsact
                oDocs.pIpAddress = Request.UserHostAddress
                oDocs.pUserId = DocSession.sUserId
                oDocs.UpdateRouteStatus(objCommand)

                oDoc.pDocId = lsDocId
                oDoc.pUserId = oDocs.pApproverId
                oDoc.pSeqNo = lSeqno.Text
                oDoc.AddToInbox(objCommand) ', oDoc.pExistsInInbox)

                'update document status
                'If lStat.Text <> "4" AndAlso lStat.Text <> "9" AndAlso lStat.Text <> "7" AndAlso lStat.Text <> "8" And lStat.Text <> "10" AndAlso lStat.Text <> "11" Then
                'If Not DocSession.Archived Then
                ' logic for document updating
                If lOutStat.Text <> "0" AndAlso lStat.Text = "3" AndAlso dlstat.SelectedValue = "3" Then
                    Dim oList As New DocList
                    oList.pDocStatus = lOutStat.Text 'iStat
                    oList.pDocId = lsDocId
                    oList.pUserId = DocSession.sUserId
                    oList.UpdateDoc(objCommand)
                Else
                    Dim liNewHierarchy, liCurrentHierarchy As Integer
                    liNewHierarchy = DocSession.GetDocHierarchy(dlstat.SelectedValue)
                    liCurrentHierarchy = DocSession.GetDocHierarchy(lStat.Text)
                    ' logic for document status updating
                    If (lStat.Text.Trim <> dlstat.SelectedValue AndAlso liNewHierarchy >= liCurrentHierarchy) OrElse liNewHierarchy = 0 Then
                        Dim oList As New DocList
                        oList.pDocStatus = dlstat.SelectedValue 'iStat
                        oList.pDocId = lsDocId
                        oList.pUserId = DocSession.sUserId
                        oList.UpdateDoc(objCommand)
                    End If
                End If

                'If pAddHistory Then
                Dim ohist As New DocHistory
                ohist.pDocId = lsDocId
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Routing"
                ohist.pUserId = DocSession.sUserId

                ohist.pAction = dlstat.SelectedItem.Text 'lsact
                ohist.AddHistory(objCommand)
                'End If

                'If iStat = 4 Then
                '  lMsg.Text = "Document has been set for evaluation."
                '   lMsg.CssClass = "msg_green"
                'Else
                lMsg.Text = "Document has been set to '" & dlstat.SelectedItem.Text & "'."
                lMsg.CssClass = "msg_green"
                'End If


                ltr.pTran.Commit()
                bCommit = True
                'RetrieveDocRouting(oDocs.pDocId)


                Dim oApp As New DocApproval
                oApp.pDocId = lsDocId
                'oApp.pUserId = DocSession.sUserId
                Dim rptr As Repeater = DirectCast(loRptItem.NamingContainer, Repeater)
                rptr.DataSource = oApp.ApproverStatusQuick
                rptr.DataBind()

                pnlPending.Update()
                'pnl.Update()
                RaiseEvent e_UpdateCount()
            End If
        Catch ex As Exception
            If bCommit = False andalso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            lMsg.Text = "Error occurred while updating the document (" & ex.Message & "). Please try again."
            lMsg.CssClass = "msg_red"
            'Throw New Exception(ex.Message)
            pnlPending.Update()
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
        End Try

    End Sub

    Public Sub fDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lAppid"), Literal).Text.ToLower = DocSession.sUserId.ToLower And DirectCast(e.Item.FindControl("lStatus"), Label).Text.ToLower = "pending" Then
                DirectCast(e.Item.FindControl("lStatus"), Label).Visible = False
                If DirectCast(e.Item.FindControl("lCC"), Literal).Text.ToLower = "1" OrElse DirectCast(e.Item.FindControl("lCC"), Literal).Text.ToLower = "true" Then
                    DirectCast(e.Item.FindControl("dlStatus2"), DropDownList).Visible = True
                    DirectCast(e.Item.FindControl("lCCText"), Literal).Visible = True
                Else
                    Dim dlstat As DropDownList = DirectCast(e.Item.FindControl("dlStatus"), DropDownList)
                    dlstat.Visible = True
                    dlstat.DataSource = pDocTypeList
                    dlstat.DataTextField = "Description"
                    dlstat.DataValueField = "StatusId"
                    dlstat.DataBind()

                End If


                DirectCast(e.Item.FindControl("lComment"), Literal).Visible = False
                DirectCast(e.Item.FindControl("tbComment"), TextBox).Visible = True
                DirectCast(e.Item.FindControl("imgSave"), ImageButton).Visible = True
            End If
        End If
    End Sub

    Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '    Dim imgDT As ImageButton = DirectCast(e.Item.FindControl("imgDocType"), ImageButton)
        '    Dim lFN As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
        '    Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lFN.Text.Trim, 4))
        '    imgDT.ImageUrl = lext
        'End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgDT As ImageButton = DirectCast(e.Item.FindControl("imgDocType"), ImageButton)
            Dim lFN As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lTitle As Label = DirectCast(e.Item.FindControl("lTitle"), Label)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lFN.Text.Trim, 4))

            imgDT.ImageUrl = lext
            Dim lDue As Literal = DirectCast(e.Item.FindControl("lcdate"), Literal)
            Dim lOrigDue As Literal = DirectCast(e.Item.FindControl("lOrigDue"), Literal)

            Dim lsdues As String = ""

            'If DirectCast(e.Item.FindControl("lCC"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lCC"), Literal).Text = "True" Then
            '    DirectCast(e.Item.FindControl("imgRoute"), ImageButton).Visible = False
            '    DirectCast(e.Item.FindControl("lTitle"), Label).Text = "CC:" & DirectCast(e.Item.FindControl("lTitle"), Label).Text
            '    'DirectCast(e.Item.FindControl("pDue"), HtmlGenericControl).Visible = False

            'End If

            If DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lUrgent"), Literal).Text = "True" Then
                DirectCast(e.Item.FindControl("imgUrgent"), Image).Visible = True
            End If

            If DirectCast(e.Item.FindControl("lOutStatus"), Literal).Text.Trim <> "" Then
                DirectCast(e.Item.FindControl("lOutStatus"), Literal).Text = "<i>(" & DirectCast(e.Item.FindControl("lOutStatus"), Literal).Text.Trim & ")</i>"
            End If

            If DirectCast(e.Item.FindControl("lcdate"), Literal).Text <> "01/01/1900" Then
                If IsDate(DirectCast(e.Item.FindControl("lcdate"), Literal).Text) AndAlso DirectCast(e.Item.FindControl("lstatusid"), Literal).Text = "2" Then
                    If DateDiff(DateInterval.Hour, CDate(DirectCast(e.Item.FindControl("lcdate"), Literal).Text), DateTime.Now) > 24 Then
                        DirectCast(e.Item.FindControl("rowColorMain"), HtmlTableRow).Style("color") = "red"
                        DirectCast(e.Item.FindControl("lTitle"), Label).Style("color") = "red"
                    End If
                End If
            End If


            If lDue.Text <> "Date not set" Then

                Dim ldays As Integer = DateDiff(DateInterval.Day, CDate(lDue.Text), DateTime.Now)
                Dim lyr As Integer = DateDiff(DateInterval.Year, CDate(lDue.Text), DateTime.Now)
                Dim lmonth As Integer = DateDiff(DateInterval.Month, CDate(lDue.Text), DateTime.Now)
                Dim lweek As Integer = DateDiff(DateInterval.Weekday, CDate(lDue.Text), DateTime.Now)

                If ldays > 0 Then

                    If lmonth > 12 Then
                        lsdues = "More than a year ago"
                    ElseIf lmonth = 12 Then
                        lsdues = "A year ago"
                    ElseIf lmonth > 1 AndAlso lmonth < 12 Then
                        lsdues = lmonth.ToString & " months ago"
                    ElseIf lmonth = 1 And ldays >= 30 Then
                        lsdues = "A month ago"
                    ElseIf ldays > 1 Then
                        lsdues = ldays.ToString & " days ago"
                    ElseIf ldays = 1 Then
                        lsdues = "Yesterday"
                    End If
                    'lDue.Style.Item("color") = "red"
                    DirectCast(e.Item.FindControl("pDue"), HtmlGenericControl).Style.Item("color") = "red"
                    lTitle.Style.Item("color") = "red"
                    lDue.Text = lsdues
                End If
                If DirectCast(e.Item.FindControl("lDocStatusId"), Literal).Text <> "12" AndAlso DirectCast(e.Item.FindControl("lDocStatusId"), Literal).Text <> "9" Then

                    If (lOrigDue.Text <> "01/01/1900" AndAlso lOrigDue.Text <> "1/1/1900") AndAlso DateDiff(DateInterval.Day, Date.Now, CDate(lOrigDue.Text)) <= 0 Then

                        Dim oDoc As New DocList
                        oDoc.pDocId = DirectCast(e.Item.FindControl("lDocId"), Literal).Text
                        oDoc.pUserId = DocSession.sUserId
                        oDoc.pDocStatus = "9" 'set to overdue
                        oDoc.UpdateDoc()

                    End If
                End If

            End If

        End If
    End Sub
End Class
