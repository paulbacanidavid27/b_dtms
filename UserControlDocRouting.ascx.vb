Public Class UserControlDocRouting
    Inherits System.Web.UI.UserControl
    Dim Author As String = ""
    Dim DocTitle As String
    Dim DocId As String
    Dim DocType As String
    Dim lRemarks As String
    Dim StatusId As String
    Dim StatusDesc As String
    Dim lsApprovers As String = ""
    Dim lsCC As String = ""
    Dim smsg As String
    Dim vmsg As String
    Dim MaxSeqNo As String
    Dim lApprover As Boolean = False
    Dim lCC As Boolean = False
    Dim lPendingStatus As Boolean = False
    Dim ApproverCount As Integer = 0

    Public Event e_ShowMessage()
    Public Event e_UpdateStatus()
    Public Event e_UpdateCount()
    Public Event e_ShowCancelWindow()
    Public Event e_RoutedTo()


    Dim bSendEmail As Boolean = True
    Dim bAdd As Boolean = True
    Public Property pRefno As String
        Get
            Return hfRefNo.Value.Trim
        End Get
        Set(ByVal value As String)
            hfRefNo.Value = value
        End Set
    End Property
    Public Property pApproverCount As Integer
        Get
            Return ApproverCount
        End Get
        Set(ByVal value As Integer)
            ApproverCount = value
        End Set
    End Property

    Public Property pAddHistory As Boolean
        Get
            Return bAdd
        End Get
        Set(ByVal value As Boolean)
            bAdd = value
        End Set
    End Property
    Dim DocTypeList As DataTable
    Public Property pDocTypeList As DataTable
        Get
            Return DocTypeList
        End Get
        Set(ByVal value As DataTable)
            DocTypeList = value
        End Set
    End Property

    Public Property pShowAddApprover As Boolean
        Get
            Return lApprover
        End Get
        Set(ByVal value As Boolean)
            lApprover = value
        End Set
    End Property
    Public Property pCCs As String
        Get
            Return lsCC
        End Get
        Set(ByVal value As String)
            lsCC = value
        End Set
    End Property
    Public Property pPendingStatus As Boolean
        Get
            Return lPendingStatus
        End Get
        Set(ByVal value As Boolean)
            lPendingStatus = value
        End Set
    End Property
    Public Property pCC As Boolean
        Get
            Return lCC
        End Get
        Set(ByVal value As Boolean)
            lCC = value
        End Set
    End Property
    Public Property pSendEmail As Boolean
        Get
            Return bSendEmail
        End Get
        Set(ByVal value As Boolean)
            bSendEmail = value
        End Set
    End Property
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property
    Public Property pStatusId As String
        Get
            Return StatusId
        End Get
        Set(ByVal value As String)
            StatusId = value
        End Set
    End Property
    Public Property pStatusDesc As String
        Get
            Return StatusDesc
        End Get
        Set(ByVal value As String)
            StatusDesc = value
        End Set
    End Property

    Public Property ValidateMessage As String
        Get
            Return vmsg
        End Get
        Set(ByVal value As String)
            vmsg = value
        End Set
    End Property
    Public Property pRemarks As String
        Get
            Return lRemarks
        End Get
        Set(ByVal value As String)
            lRemarks = value
        End Set
    End Property
    Public Property pApprovers As String
        Get
            Return lsApprovers
        End Get
        Set(ByVal value As String)
            lsApprovers = value
        End Set
    End Property
    Public Property pAuthor As String
        Get
            Return Author
        End Get
        Set(ByVal value As String)
            Author = value
        End Set
    End Property

    Public Property pDocTitle As String
        Get
            Return hfTitle.Value
        End Get
        Set(ByVal value As String)
            hfTitle.Value = value
        End Set
    End Property

    Public Property pDocId As String
        Get
            Return hfDocid.Value.Trim
        End Get
        Set(ByVal value As String)
            hfDocid.Value = value
        End Set
    End Property
    Public Property pRouteStatusId As String
        Get
            Return hfStatusId.Value.Trim
        End Get
        Set(ByVal value As String)
            hfStatusId.Value = value
        End Set
    End Property
    Public Property pSeqNo As String
        Get
            Return hfSeqNo.Value.Trim
        End Get
        Set(ByVal value As String)
            hfSeqNo.Value = value
        End Set
    End Property
    Public Property pMaxSeqNo As String
        Get
            Return hfMaxSeqNo.Value
        End Get
        Set(ByVal value As String)
            hfMaxSeqNo.Value = value
        End Set
    End Property
    Public Property pDocType2 As String
        Get
            Return hfDocType.Value.Trim
        End Get
        Set(ByVal value As String)
            hfDocType.Value = value
        End Set
    End Property
    Public Property pOfficeCode As String
        Get
            Return hfOfficeCode.Value.Trim
        End Get
        Set(ByVal value As String)
            hfOfficeCode.Value = value
        End Set
    End Property
    Public Property pDocType As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If DocSession.Archived Then
                ApplyArchive()

            End If
        End If
    End Sub
    Public Sub ApplyArchive()
        imgAddApprover.Visible = False
        'imgSaveDueDate.Visible = False
        'linkSearch.Visible = False
        imgAddCC.Visible = False
    End Sub
    Public Sub ResetRouting()
        RouteInstruction.Visible = False
        pnlRtInstruction.Update()
        If DocSession.sUserRole = "B" Then
            rptList.Visible = True
        Else
            rptList.Visible = False
        End If

        rptSub.Visible = False
        lApproverSelected.Visible = True
        dlGroups.Visible = False
        txtBx.Visible = True
        dlSearchType.SelectedValue = "u"
        ShowSearch()
        pnl.Update()
    End Sub

    Public Sub HideAdd()
        imgAddApprover.Visible = False
    End Sub

    Public Sub HideSearch()
        If DocSession.sUserRole = "B" Then
            pSearchCriteria.Visible = True
        Else
            pSearchCriteria.Visible = False
        End If
        pSearchCriteria2.Visible = False
        pnlCC.Visible = False
        pCopyFurnish.Visible = False
    End Sub
    'Public Sub HideSearchOnly()
    '    pSearchCriteria.Visible = True
    '    pSearchCriteria2.Visible = False
    '    pnlCC.Visible = False
    '    pCopyFurnish.Visible = False
    'End Sub
    Public Sub ShowHeader()
        pHeader.Visible = True
    End Sub

    Public Sub ShowSearch()
        If DocSession.sUserRole = "B" Then
            imgSendCC.Visible = False
            pSearchCriteria.Visible = True
            pSearchCriteria2.Visible = False
            pCopyFurnish.Visible = False
            pnlCC.Visible = False
        Else
            imgSendCC.Visible = False
            pSearchCriteria.Visible = True
            pSearchCriteria2.Visible = True
            pCopyFurnish.Visible = True
            pnlCC.Visible = True
        End If

    End Sub

    Public Sub HideCCSearch()
        imgSendCC.Visible = False
        pSearchCriteria.Visible = False
        pSearchCriteria2.Visible = False
        pnlCC.Visible = False
        pCopyFurnish.Visible = False
    End Sub

    Public Sub ShowCCSearch()
        ShowHeader()
        imgSendCC.Visible = True
        pSearchCriteria.Visible = False
        pSearchCriteria2.Visible = False
        pCopyFurnish.Visible = True
        pnlCC.Visible = True

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
    Public Sub RetrieveDocRouting(ByVal aDocId As Integer)
        Dim oRoute As New DocRouting
        'If DocSession.sDocID IsNot Nothing Then

        If pDocId IsNot Nothing And pDocId <> "" Then
            'oRoute.pDocId = DocSession.sDocID

            If DocSession.Archived OrElse CInt(DocSession.sDocTypeAccess) <= 1 OrElse (DocSession.sCheckout = "Yes" AndAlso DocSession.sCheckOutBy <> DocSession.sUserId) Then
                imgAddApprover.Visible = False
                imgAddCC.Visible = False
            Else
                imgAddApprover.Visible = True
                If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "A" Then
                    imgAddCC.Visible = True
                Else
                    imgAddCC.Visible = False
                End If
            End If

            RetrieveGroupStatus()

            pMaxSeqNo = oRoute.MaxRoutingSeqNo

            oRoute.pDocId = pDocId
            'oRoute.pApproverId = DocSession.sUserId ' remove this to retrieve all approvers
            oRoute.pCarbonCopy = "0"
            rpt.Visible = True
            rpt.DataSource = oRoute.RetrieveRouting()
            rpt.DataBind()

            'oRoute.pDocId = pDocId
            'oRoute.pApproverId = DocSession.sUserId



            'rptOthers.Visible = False
            'rptOthers.DataSource = oRoute.RetrieveRoutingOther()
            'rptOthers.DataBind()
            'rptCopyFurnish.Visible = False
            'rptCopyFurnish.DataSource = oRoute.RetrieveRoutingCopy
            'rptCopyFurnish.DataBind()
            pApproverCount = rpt.Items.Count
            If pApproverCount <= 0 Then 'AndAlso rptOthers.Items.Count <= 0 Then
                lMsg.Text = "No approvers for the selected document. "
                If pAuthor.ToLower.Trim = DocSession.sUserId.ToLower.Trim OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "A" Then
                    pShowAddApprover = True
                    lMsg.Text = lMsg.Text & "Click on Add icon to add approvers. "
                    lMsg.CssClass = "msg_red"
                End If
            Else

            End If

            oRoute.pCarbonCopy = "1"
            rptCC.DataSource = oRoute.RetrieveRouting()
            rptCC.DataBind()
            rptCC.Visible = (rptCC.Items.Count > 0)

            'If rptOthers.Items.Count > 0 Then
            '    lOther.Visible = True1

            'End If
            'If rptCopyFurnish.Items.Count > 0 Then
            'lOther.Visible = True
            'End If
            'If pPendingStatus OrElse (pCC AndAlso pApprover = False) Then
            'If pShowAddApprover Then
            '    HideAdd()
            'End If
            imgAddApprover.Visible = pShowAddApprover

            pnl.Update()
        End If
        oRoute = Nothing
    End Sub

    Public Sub CancelRouting(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'RaiseEvent e_ShowCancelWindow()
        pConfirm.Visible = Not pConfirm.Visible
        pnl.Update()
    End Sub



    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUrgent(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("imgUrgent2"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub
    Public Sub fUnUrgent(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("imgUrgent"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub
    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub
    Public Sub SelectDefault(ByVal asOfc As String)
        

        Dim loData As DataTable
        'Dim loRow As DataRow
        Dim oOfc As DocOffice
        Try
            oOfc = New DocOffice
            loData = oOfc.RetrievePointPerson(asOfc)
            'If loData.Rows.Count > 0 Then
            SelectUser(loData)
            'End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
        UpdatePanel1.Update()

    End Sub
    Public Sub fSelectAll(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelectedh"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        'lImg.Visible = Not lImg.Visible

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("Approver", Type.GetType("System.String"))
            loData.Columns.Add("Email", Type.GetType("System.String"))
            'loData.Columns.Add("tbDue", Type.GetType("System.String"))
            loData.Columns.Add("tbRem", Type.GetType("System.String"))

            For Each rptItems As RepeaterItem In rptSub.Items
                loRow = loData.NewRow()
                loRow("Email") = DirectCast(rptItems.FindControl("lEmail"), Literal).Text
                loRow("UserId") = DirectCast(rptItems.FindControl("lUserId"), Literal).Text
                loRow("Approver") = DirectCast(rptItems.FindControl("lApprover"), Literal).Text
                'loRow("tbDue") = ""
                loRow("tbRem") = ""
                loData.Rows.Add(loRow)
                rptItems.Visible = False
            Next

            SelectUser(loData)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
        UpdatePanel1.Update()

    End Sub
    Public Sub fUnSelectAll(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg2 As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg As ImageButton = DirectCast(lImg2.Parent.FindControl("ImgSelecth"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        UpdatePanel1.Update()

    End Sub

    Public Sub fSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        If Not pDocId Is Nothing AndAlso pDocId <> "" Then
            Dim ort As New DocRouting
            ort.pDocId = pDocId 'DocSession.sDocID
            ort.pApproverId = DirectCast(rpt.FindControl("lUserId"), Literal).Text
            If ort.ValidRecipient() Then
                lMsg.Text = "You cannot route this document to the same recipient that has not acknowledge this document yet."
                lMsg.CssClass = "msg_red"
                pnl.Update()
                Exit Sub
            End If
        End If

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("Approver", Type.GetType("System.String"))
            loData.Columns.Add("Email", Type.GetType("System.String"))
            'loData.Columns.Add("tbDue", Type.GetType("System.String"))
            loData.Columns.Add("tbRem", Type.GetType("System.String"))
            loRow = loData.NewRow()

            loRow("Email") = DirectCast(rpt.FindControl("lEmail"), Literal).Text
            loRow("UserId") = DirectCast(rpt.FindControl("lUserId"), Literal).Text
            loRow("Approver") = DirectCast(rpt.FindControl("lApprover"), Literal).Text
            'loRow("tbDue") = ""
            loRow("tbRem") = ""
            loData.Rows.Add(loRow)
            rpt.Visible = False

            'If imgCbsy.Visible Then
            '    CopyUser(loData)
            'Else
            SelectUser(loData)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Sub

    Public Sub fUnSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        'Dim loData As New DataTable("tblApprovers")
        'Dim loRow As DataRow
        'loData.Columns.Add("UserId", Type.GetType("System.String"))
        'loData.Columns.Add("Approver", Type.GetType("System.String"))
        'loData.Columns.Add("Email", Type.GetType("System.String"))
        'loRow = loData.NewRow()

        'loRow("Email") = DirectCast(rpt.FindControl("lEmail"), Literal).Text
        'loRow("UserId") = DirectCast(rpt.FindControl("lUserId"), Literal).Text
        'loRow("Approver") = DirectCast(rpt.FindControl("lApprover"), Literal).Text
        'loData.Rows.Add(loRow)
        rpt.Visible = False
        pnl.Update()

    End Sub

    Public Sub fSelectCC(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        If Not pDocId Is Nothing AndAlso pDocId <> "" Then
            Dim ort As New DocRouting
            ort.pDocId = pDocId 'DocSession.sDocID
            ort.pApproverId = DirectCast(rpt.FindControl("lUserId"), Literal).Text
            If ort.ValidRecipient() Then
                lMsg.Text = "You cannot CC this document to the same recipient that has not acknowledge this document yet."
                lMsg.CssClass = "msg_red"
                pnl.Update()
                Exit Sub
            End If
        End If


        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("Approver", Type.GetType("System.String"))
            loData.Columns.Add("Email", Type.GetType("System.String"))
            'loData.Columns.Add("tbDue", Type.GetType("System.String"))
            loData.Columns.Add("tbRem", Type.GetType("System.String"))
            loRow = loData.NewRow()

            loRow("Email") = DirectCast(rpt.FindControl("lEmail"), Literal).Text
            loRow("UserId") = DirectCast(rpt.FindControl("lUserId"), Literal).Text
            loRow("Approver") = DirectCast(rpt.FindControl("lApprover"), Literal).Text
            'loRow("tbDue") = ""
            loRow("tbRem") = ""
            loData.Rows.Add(loRow)
            rpt.Visible = False


            CopyUser(loData)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try



    End Sub

    Private Sub SelectUser(ByVal loData As DataTable)
        'Dim liCtr As Integer
        'Dim imgBtnSelected As ImageButton
        'Dim loRow As DataRow
        Try


            'liCtr = 0
            'For Each ri In rptList.Items
            '    If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
            '        imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
            '        If imgBtnSelected.Visible Then
            '            loRow = loData.NewRow()
            '            loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
            '            loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
            '                loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text
            '                loRow("tbDue") = DirectCast(ri.FindControl("tbDue"), TextBox).Text
            '                loRow("tbRem") = DirectCast(ri.FindControl("tbRem"), TextBox).Text
            '            loData.Rows.Add(loRow)
            '            liCtr = liCtr + 1
            '        End If

            '    End If
            'Next

            If loData.Rows.Count > 0 Then
                lApproverSelected.Visible = False
            Else
                lApproverSelected.Visible = True
            End If
            rptList.DataSource = loData
            rptList.DataBind()
            rptList.Visible = True
            pnl.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If
        End Try
    End Sub

    Private Sub CopyUser(ByVal loData As DataTable)
        Dim liCtr As Integer
        Dim imgBtnSelected As ImageButton
        Dim loRow As DataRow
        Try


            liCtr = 0
            For Each ri In rptCopy.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    If imgBtnSelected.Visible Then
                        If loData(0)("UserId") <> DirectCast(ri.FindControl("lUserId"), Literal).Text Then
                            loRow = loData.NewRow()
                            loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
                            loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
                            loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text

                            loData.Rows.Add(loRow)
                            liCtr = liCtr + 1
                        End If

                    End If

                End If
            Next



            rptCopy.DataSource = loData
            rptCopy.DataBind()
            rptCopy.Visible = True
            pnl.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If
        End Try
    End Sub

    'Private Sub imgHelp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHelp.Click
    '    RouteInstruction.Visible = Not RouteInstruction.Visible

    'End Sub

    Public Sub ShowRouteInstruction()
        RouteInstruction.Visible = Not RouteInstruction.Visible
        pnlRtInstruction.Update()
    End Sub
    Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        Dim lodata As DataTable
        Try
            lSearchMsg.Visible = False
            Dim oDocs As New DocUser
            If dlSearchType.SelectedValue = "u" AndAlso txtBx.Text.Trim = "Enter User Name..." Then
                lSearchMsg.Visible = True
                lSearchMsg.Text = "Please enter user name first before clicking Search icon."
            ElseIf pDocType2 = "" Then
                lSearchMsg.Visible = True
                lSearchMsg.Text = "Please select a Document Type before clicking Search icon."
            Else
                oDocs.pSearchString = txtBx.Text
                oDocs.pDocType = pDocType2 'DocSession.sDocType
                'oDocs.pOfficeCode = pOfficeCode
                oDocs.pUserId = DocSession.sUserId
                If dlSearchType.SelectedValue = "g" Then
                    oDocs.pGroup = dlGroups.SelectedValue

                    lodata = oDocs.RetrieveUserByGroup
                Else
                    lodata = oDocs.RetrieveUsersWithAccess
                End If

                If lodata.Rows.Count > 0 Then
                    rptSub.DataSource = lodata
                    rptSub.DataBind()
                    rptSub.Visible = True
                    rptSub.Focus()
                Else
                    rptSub.Visible = False
                    lSearchMsg.Visible = True
                    lSearchMsg.Text = "No records found."
                End If



            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If
        End Try

        pnl.Update()
    End Sub

    Public Function GetSelectedUser() As DataTable
        Dim imgBtnSelected As ImageButton
        Dim loData As DataTable
        Dim loRow As DataRow
        Try

            loData = New DataTable("tblApprovers")
        loData.Columns.Add("UserId", Type.GetType("System.String"))
        loData.Columns.Add("Approver", Type.GetType("System.String"))
        loData.Columns.Add("Email", Type.GetType("System.String"))


        Dim liCtr As Integer
        liCtr = 0
        For Each ri In rptSub.Items
            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                If imgBtnSelected.Visible Then
                    loRow = loData.NewRow()
                    loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
                    loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
                    loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text
                    loData.Rows.Add(loRow)
                    liCtr = liCtr + 1
                End If

            End If
        Next

            Return loData
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Function

    'Protected Sub btSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSelect.Click
    '    Dim imgBtnSelected As ImageButton
    '    Dim loData As New DataTable("tblApprovers")
    '    Dim loRow As DataRow
    '    loData.Columns.Add("UserId", Type.GetType("System.String"))
    '    loData.Columns.Add("Approver", Type.GetType("System.String"))
    '    loData.Columns.Add("Email", Type.GetType("System.String"))

    '    Dim liCtr As Integer
    '    liCtr = 0
    '    For Each ri In rptList.Items
    '        If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
    '            imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '            If imgBtnSelected.Visible Then
    '                loRow = loData.NewRow()
    '                loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
    '                loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
    '                loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text
    '                'loRow("tbDue") = DirectCast(ri.FindControl("tbDue"), TextBox).Text
    '                loRow("tbRem") = DirectCast(ri.FindControl("tbRem"), TextBox).Text
    '                loData.Rows.Add(loRow)
    '                liCtr = liCtr + 1
    '            End If

    '        End If
    '    Next

    '    For Each ri In rptSub.Items
    '        If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
    '            imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '            If imgBtnSelected.Visible Then
    '                loRow = loData.NewRow()
    '                loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
    '                loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
    '                loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text

    '                loData.Rows.Add(loRow)
    '                liCtr = liCtr + 1
    '            End If

    '        End If
    '    Next
    '    If loData.Rows.Count > 0 Then
    '        lApproverSelected.Visible = False
    '    Else
    '        lApproverSelected.Visible = True
    '    End If

    '    rptList.DataSource = loData
    '    rptList.DataBind()
    '    rptList.Visible = True
    '    rptSub.Visible = False
    '    txtBx.Text = "Enter User Name..."
    '    pnl.Update()
    'End Sub

    Private Sub rpt_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpt.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'imgB As ImageButton = DirectCast(e.Item.FindControl("imgRoute"), ImageButton)
            'AddHandler imgB.Click, AddressOf ShowRouting
            'Dim imgB = DirectCast(e.Item.FindControl("imgDeny"), ImageButton)
            'AddHandler imgB.Click, AddressOf RouteDocument
            'imgB = DirectCast(e.Item.FindControl("imgApprove"), ImageButton)
            'AddHandler imgB.Click, AddressOf RouteDocument
            Dim imgB As ImageButton = DirectCast(e.Item.FindControl("imgSend"), ImageButton)
            AddHandler imgB.Click, AddressOf RouteDocument


        End If
    End Sub

    Private Sub rptCC_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCC.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim imgB As ImageButton = DirectCast(e.Item.FindControl("imgSend"), ImageButton)
            AddHandler imgB.Click, AddressOf RouteCopy


        End If
    End Sub

    Private Sub rpt_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpt.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lAD As Label = DirectCast(e.Item.FindControl("lActionDate"), Label)
            Dim lAppId = DirectCast(e.Item.FindControl("lApproverId"), Literal)
            Dim lSender = DirectCast(e.Item.FindControl("lSenderId"), Literal)
            Dim lUrgent = DirectCast(e.Item.FindControl("lUrgent"), Literal)
            Dim imgUr = DirectCast(e.Item.FindControl("imgUrg"), Image)
            Dim imgCan = DirectCast(e.Item.FindControl("imgCancel"), Image)
            'If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text = "2" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "1" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "True" Then
            ' pPendingStatus = True
            'End If
            If DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text = pMaxSeqNo _
                    AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "1" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "True" Then
                If lAD.Text.Trim <> "" AndAlso DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.ToLower.Trim <> "2" _
                    AndAlso (DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" OrElse DocSession.sUserRole = "A") Then
                    pShowAddApprover = True
                End If
                If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.ToLower.Trim = "2" _
                    AndAlso (DirectCast(e.Item.FindControl("lSenderId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim _
                                OrElse DocSession.sUserRole = "A" OrElse ((DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G") AndAlso (DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim <> DocSession.sUserId.ToLower.Trim))) Then
                    If DocSession.sUserRole <> "B" Then
                        imgCan.Visible = True
                    End If

                    hfMaxSeqNoToCancel.Value = DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text
                End If



            End If

            If DirectCast(e.Item.FindControl("lOutStatus"), Label).Text.Trim <> "" Then
                DirectCast(e.Item.FindControl("lOutStatus"), Label).Text = "Outgoing Action:  " & DirectCast(e.Item.FindControl("lOutStatus"), Label).Text & ""
            End If
            'If DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim AndAlso (DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True") Then
            'pCC = True
            'End If

            If (lSender.Text.Trim.ToLower = DocSession.sUserId.ToLower OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G") AndAlso DocSession.sCheckout <> "Yes" AndAlso CInt(DocSession.sDocTypeAccess) > 1 Then
                'DirectCast(e.Item.FindControl("imgApprove"), ImageButton).Visible = True
                'Visible = True
                If DocSession.Archived Then
                Else
                    If DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text = pMaxSeqNo Then
                        'DirectCast(e.Item.FindControl("txDueDate"), TextBox).Visible = True
                        'DirectCast(e.Item.FindControl("lDueDate1"), Label).Visible = False
                        'DirectCast(e.Item.FindControl("lDueDate"), Label).Visible = False
                        'DirectCast(e.Item.FindControl("lDueDate2"), Label).Visible = False
                        'imgSaveDueDate.Visible = True
                    End If

                End If

            End If

            'If DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True" Then
            '    DirectCast(e.Item.FindControl("ltask"), Literal).Text = "CC:Task"

            'End If
            If lUrgent.Text = "1" OrElse lUrgent.Text = "True" Then
                imgUr.Visible = True
            End If

            If lAD.Text.Trim = "" AndAlso lAppId.Text.Trim.ToLower = DocSession.sUserId.ToLower AndAlso DocSession.sCheckout <> "Yes" Then ' AndAlso CInt(DocSession.sDocTypeAccess) > 1
                'DirectCast(e.Item.FindControl("imgApprove"), ImageButton).Visible = True
                'DirectCast(e.Item.FindControl("imgDeny"), ImageButton).Visible = True
                DirectCast(e.Item.FindControl("imgSend"), ImageButton).Visible = True
                DirectCast(e.Item.FindControl("txtComment"), TextBox).Visible = True
                '''DirectCast(e.Item.FindControl("rbAck"), RadioButton).Visible = false

                Dim dlStat As DropDownList = DirectCast(e.Item.FindControl("dlStatus"), DropDownList)
                dlStat.Visible = True
                'oDocs.pGroupId = DocSession.sUserGroup
                dlStat.DataSource = pDocTypeList
                dlStat.DataTextField = "description"
                dlStat.DataValueField = "statusid"
                dlStat.DataBind()

                If DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#FFFFFF"
                    '''DirectCast(e.Item.FindControl("rbEval"), RadioButton).Visible = False
                Else
                    '''DirectCast(e.Item.FindControl("rbEval"), RadioButton).Visible = True
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#D9EEF8"
                    DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#0060A9"
                End If

                DirectCast(e.Item.FindControl("lComment"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate1"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate2"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate"), Label).Visible = False
                DirectCast(e.Item.FindControl("lbstat"), Label).Visible = False
                DirectCast(e.Item.FindControl("lUser"), Label).Visible = False
                DirectCast(e.Item.FindControl("lbRemarks"), Label).Visible = True
                DirectCast(e.Item.FindControl("lbAction"), Label).Visible = True

            Else
                DirectCast(e.Item.FindControl("lComment"), Label).Visible = True
                If DirectCast(e.Item.FindControl("lCC"), Literal).Text.Trim = "1" OrElse DirectCast(e.Item.FindControl("lCC"), Literal).Text.Trim = "True" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#FFFFFF"
                    If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "2" Then
                        DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#0060A9"
                    End If

                ElseIf DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "2" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#D9EEF8"
                    DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#0060A9"
                    DirectCast(e.Item.FindControl("lActionDate1"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lActionDate2"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lActionDate"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lComment"), Label).Visible = False
                ElseIf DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "4" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#F4C6C6"
                    DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#A71E22"

                End If
            End If
        End If
    End Sub
    Private Sub rptCC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCC.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lAD As Label = DirectCast(e.Item.FindControl("lActionDate"), Label)
            Dim lAppId = DirectCast(e.Item.FindControl("lApproverId"), Literal)
            Dim lSender = DirectCast(e.Item.FindControl("lSenderId"), Literal)
            Dim imgCan = DirectCast(e.Item.FindControl("imgCancelCC"), ImageButton)
            'If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text = "2" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "1" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "True" Then
            '    pPendingStatus = True
            'End If
            'If DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "1" AndAlso DirectCast(e.Item.FindControl("lcc"), Literal).Text <> "True" Then
            '    pApprover = True
            'End If
            'If DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim AndAlso (DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True") Then
            '    pCC = True
            'End If

            'If (lSender.Text.Trim.ToLower = DocSession.sUserId.ToLower OrElse DocSession.sUserRole = "D") AndAlso DocSession.sCheckout <> "Yes" AndAlso CInt(DocSession.sDocTypeAccess) > 1 Then
            '    'DirectCast(e.Item.FindControl("imgApprove"), ImageButton).Visible = True
            '    'Visible = True
            '    If DocSession.Archived Then
            '    Else
            '        If DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text = pMaxSeqNo Then
            '            DirectCast(e.Item.FindControl("txDueDate"), TextBox).Visible = True
            '            DirectCast(e.Item.FindControl("lDueDate1"), Label).Visible = False
            '            DirectCast(e.Item.FindControl("lDueDate"), Label).Visible = False
            '            DirectCast(e.Item.FindControl("lDueDate2"), Label).Visible = False
            '            imgSaveDueDate.Visible = True
            '        End If

            '    End If

            'End If

            'If DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True" Then
            '    DirectCast(e.Item.FindControl("ltask"), Literal).Text = "CC:Task"

            'End If

            If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.ToLower.Trim = "2" _
                    AndAlso (DirectCast(e.Item.FindControl("lSenderId"), Literal).Text.ToLower.Trim = DocSession.sUserId.ToLower.Trim _
                                OrElse ((DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G") AndAlso (DirectCast(e.Item.FindControl("lApproverId"), Literal).Text.ToLower.Trim <> DocSession.sUserId.ToLower.Trim))) Then
                imgCan.Visible = True
                hfCCSeqNoToCancel.Value = DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text
                imgCan.OnClientClick = "setValue('hfCCSeqNoToCancel','" & DirectCast(e.Item.FindControl("lSeqNo"), Literal).Text.Trim & "');showWindow('filtercc')"
            End If

            If lAD.Text.Trim = "" And lAppId.Text.Trim.ToLower = DocSession.sUserId.ToLower AndAlso DocSession.sCheckout <> "Yes" Then ' AndAlso CInt(DocSession.sDocTypeAccess) > 1
                'DirectCast(e.Item.FindControl("imgApprove"), ImageButton).Visible = True
                'DirectCast(e.Item.FindControl("imgDeny"), ImageButton).Visible = True
                DirectCast(e.Item.FindControl("imgSend"), ImageButton).Visible = True
                DirectCast(e.Item.FindControl("txtComment"), TextBox).Visible = True
                'DirectCast(e.Item.FindControl("rbAck"), RadioButton).Visible = True
                If DirectCast(e.Item.FindControl("lcc"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lcc"), Literal).Text = "True" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#FFFFFF"
                    'DirectCast(e.Item.FindControl("rbEval"), RadioButton).Visible = False
                Else
                    'DirectCast(e.Item.FindControl("rbEval"), RadioButton).Visible = True
                End If

                DirectCast(e.Item.FindControl("lComment"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate1"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate2"), Label).Visible = False
                DirectCast(e.Item.FindControl("lActionDate"), Label).Visible = False
                DirectCast(e.Item.FindControl("lbstat"), Label).Visible = False
                'DirectCast(e.Item.FindControl("lbstat"), Label).Text = "Acknowledged"
                DirectCast(e.Item.FindControl("dlStatus"), DropDownList).Visible = True
                DirectCast(e.Item.FindControl("lUser"), Label).Visible = False
                DirectCast(e.Item.FindControl("lbRemarks"), Label).Visible = True
                DirectCast(e.Item.FindControl("lbAction"), Label).Visible = True

            Else
                DirectCast(e.Item.FindControl("lComment"), Label).Visible = True
                If DirectCast(e.Item.FindControl("lCC"), Literal).Text.Trim = "1" OrElse DirectCast(e.Item.FindControl("lCC"), Literal).Text.Trim = "True" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#FFFFFF"
                    If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "2" Then
                        DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#0060A9"
                        DirectCast(e.Item.FindControl("lActionDate1"), Label).Visible = False
                        DirectCast(e.Item.FindControl("lActionDate2"), Label).Visible = False
                        DirectCast(e.Item.FindControl("lActionDate"), Label).Visible = False
                    End If

                ElseIf DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "2" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#D9EEF8"
                    DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#0060A9"
                    DirectCast(e.Item.FindControl("lActionDate1"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lActionDate2"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lActionDate"), Label).Visible = False
                    DirectCast(e.Item.FindControl("lComment"), Label).Visible = False
                ElseIf DirectCast(e.Item.FindControl("lStatusId"), Literal).Text.Trim = "4" Then
                    DirectCast(e.Item.FindControl("row1"), HtmlTable).Style.Item("background-color") = "#F4C6C6"
                    DirectCast(e.Item.FindControl("lbstat"), Label).Style.Item("color") = "#A71E22"

                End If
            End If
        End If
    End Sub
    Private Sub ShowRouting(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim losender As ImageButton = DirectCast(sender, ImageButton)
        Dim loRptItem As RepeaterItem = DirectCast(losender.NamingContainer, RepeaterItem)
        'Dim loRpt As Repeater = DirectCast(loRptItem.FindControl("rptList"), Repeater)
        'Dim iSend As ImageButton = DirectCast(loRptItem.FindControl("ImgSend"), ImageButton)
        'iSend.Visible = True
        losender.Visible = False
        pTitle.Visible = True
        ShowSearch()
        pnl.Update()

    End Sub

    Private Sub HideRouting()
        'iSend.Visible = True

        pTitle.Visible = False
        HideSearch()
        pnl.Update()

    End Sub
    Public Sub ShowRouting()
        'iSend.Visible = True
        ResetRouting()
        pTitle.Visible = True
        lOutS.Visible = True
        imgSendApprover.Visible = True
        imgQuickApprover.Visible = False
        pSearchCriteria.DefaultButton = "imgSendApprover"
        ShowSearch()
        pnl.Update()

    End Sub

    Public Sub ShowQuickRouting()
        'iSend.Visible = True

        ResetRouting()
        pTitle.Visible = True
        imgQuickApprover.Visible = True
        imgSendApprover.Visible = False
        pSearchCriteria.DefaultButton = "imgQuickApprover"
        ShowSearch()
        pnl.Update()

    End Sub

    'route document and update current doc routing status
    Public Sub RouteDocument3()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")

        Dim oDoc As DocList
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim ldataEmail As DataTable
        Dim ldr As DataRow
        Dim liCtr As Integer
        Dim bCommit As Boolean = False
        liCtr = 0
        Try
            ldataEmail = New DataTable("EmailList")
            ldataEmail.Columns.Add("Email", Type.GetType("System.String"))
            ldataEmail.Columns.Add("Name", Type.GetType("System.String"))
            ldataEmail.Columns.Add("RType", Type.GetType("System.String"))

            oDoc = New DocList
            'oDoc.ExistsInInbox()
            ' oDoc.ExistsInOutbox()
            lMsg.Text = ""
            'If Not ValidateDueDate() Then
            '    Exit Sub
            'End If
            ValidateCopy()
            Dim oDocs As New DocRouting
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)

            Dim fsel As ImageButton
            Dim lsEmail As String = ""

            Dim lsApprovername As String = ""

            oDocs.pComment = ""
            oDocs.pDocId = hfDocid.Value
            oDocs.pApproverId = DocSession.sUserId
            oDocs.pOldSeqNo = pSeqNo 'DocSession.getNextID("seqno")
            oDocs.pStatusId = 3 'acknowledged
            oDocs.pAction = "Acknowledged document"
            oDocs.pIpAddress = Request.UserHostAddress
            oDocs.pUserId = DocSession.sUserId
            oDocs.UpdateRouteStatus(objCommand)

            oDoc.pDocId = oDocs.pDocId
            oDoc.pUserId = oDocs.pApproverId
            oDoc.pSeqNo = oDocs.pOldSeqNo
            oDoc.AddToInbox(objCommand) ', oDoc.pExistsInInbox)

            Dim lsCopy As String = ""
            For Each loRptItem In rptList.Items

                If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                    fsel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                    If fsel.Visible Then

                        lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                        lsApprovername = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                        If pApprovers = "" Then
                            pApprovers = lsApprovername
                        Else
                            pApprovers = pApprovers & ", " & lsApprovername
                        End If
                        pRemarks = DirectCast(loRptItem.FindControl("tbRem"), TextBox).Text.Trim
                        oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                        oDocs.pDocId = hfDocid.Value 'DocSession.sDocID
                        oDocs.pStatusId = 2
                        oDocs.pIpAddress = Request.UserHostAddress
                        oDocs.pUserId = DocSession.sUserId
                        'oDocs.pDueDate = DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text
                        oDocs.pRemarks = DirectCast(loRptItem.FindControl("tbRem"), TextBox).Text
                        oDocs.pSender = DocSession.sUserId
                        oDocs.pSeqNo = DocSession.getNextID("seqno") 'pSeqNo
                        oDocs.pUrgent = IIf(DirectCast(loRptItem.FindControl("imgUrgent2"), ImageButton).Visible, 1, 0)
                        oDocs.pOutStatusId = DirectCast(loRptItem.FindControl("dlOutgoingStatus"), DropDownList).SelectedValue
                        oDocs.RouteDocument(objCommand)

                        oDocs.pRoutingSeqNo = hfSeqNo.Value
                        oDocs.UpdateDocRouting(objCommand)


                        'oDocs.UpdateDocApprover(objCommand)

                        'Dim oDocL As New DocList

                        'If pRouteStatusId = "2" Then
                        '    oDocL.pDocStatus = 3
                        'End If
                        'oDocL.pDocId = hfDocid.Value
                        'oDocL.pUserId = DocSession.sUserId
                        'oDocL.pSeqNo = oDocs.pSeqNo
                        'oDocL.UpdateDoc()

                        'oDocL.pDocId = pDocId
                        'oDocL.pUserId = DocSession.sUserId
                        'oDocL.pSeqNo = oDocs.pSeqNo
                        'oDocL.AddToOutbox(objCommand)

                        'Dim oDocL As New DocList

                        If pRouteStatusId = "2" OrElse pRouteStatusId = "8" OrElse pRouteStatusId = "12" OrElse pRouteStatusId = "15" OrElse pRouteStatusId = "18" OrElse pRouteStatusId = "19" Then
                            oDoc.pDocStatus = 3
                        End If
                        oDoc.pDocId = hfDocid.Value
                        oDoc.pUserId = DocSession.sUserId
                        oDoc.pSeqNo = oDocs.pSeqNo
                        oDoc.UpdateDoc(objCommand)

                        oDoc.pDocId = pDocId
                        oDoc.pUserId = DocSession.sUserId
                        oDoc.pSeqNo = oDocs.pSeqNo
                        oDoc.AddToOutbox(objCommand) ', oDoc.pExistsInOutbox)

                        If lsEmail.Trim <> "" Then
                            'EmailApprovers(lsEmail, lsApprovername)
                            ldr = ldataEmail.NewRow
                            ldr("Email") = lsEmail
                            ldr("Name") = lsApprovername
                            ldr("RType") = "R"
                            ldataEmail.Rows.Add(ldr)

                        End If

                        lsCopy = RouteCopy(objCommand, ldataEmail) ', oDoc.pExistsInInbox)

                        liCtr = liCtr + 1
                    End If
                End If
            Next

            If liCtr > 0 Then
                Dim ohist As New DocHistory
                ohist.pDocId = hfDocid.Value
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Routing"
                ohist.pUserId = DocSession.sUserId

                ohist.pAction = "Routed document to " & pApprovers
                ohist.AddHistory(objCommand)
            End If

            ltr.pTran.Commit()
            bCommit = True
            If Not ldataEmail Is Nothing AndAlso ldataEmail.Rows.Count > 0 Then
                EmailApprovers(ldataEmail)
            End If

            If liCtr > 0 Then
                'send email
                RaiseEvent e_ShowMessage()
                RaiseEvent e_UpdateCount()
                'lMsg.Text = "Document has been routed to the selected user. Email notification was sent to the approver."
                'lMsg.CssClass = "msg_green"
                'RetrieveDocRouting(oDocs.pDocId)
                'HideRouting()
                'imgAddApprover.Visible = True
                'imgSendApprover.Visible = False
            Else
                lMsg.Text = "No user has been selected."
                lMsg.CssClass = "msg_red"
            End If


            pnl.Update()

        Catch ex As MailException
            ShowMessage(ex.Message)
        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage("Error occurred while updating the document (" & ex.Message & "). Please try again.**")
            'lMsg.CssClass = "msg_red"
            'Throw New Exception(ex.Message)
        Finally
            If Not ldataEmail Is Nothing Then
                ldataEmail.Dispose()
                ldataEmail = Nothing
            End If

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
    Private Sub ShowMessage(ByVal asMsg As String)
        Message = asMsg
        RaiseEvent e_ShowMessage()


    End Sub

    'route from view screen
    Public Sub RouteDocument(ByVal sender As Object, ByVal e As EventArgs)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")


        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim losender As ImageButton = DirectCast(sender, ImageButton)

        Dim fSel As ImageButton
        Dim loRptItem As RepeaterItem = DirectCast(losender.NamingContainer, RepeaterItem)
        '''Dim rbAck As RadioButton = DirectCast(loRptItem.FindControl("rbAck"), RadioButton)
        '''Dim rbEval As RadioButton = DirectCast(loRptItem.FindControl("rbEval"), RadioButton)
        Dim dlstat As DropDownList = DirectCast(loRptItem.FindControl("dlStatus"), DropDownList)
        Dim tComment As TextBox = DirectCast(loRptItem.FindControl("txtComment"), TextBox)
        Dim lAppid As Literal = DirectCast(loRptItem.FindControl("lApproverId"), Literal)
        Dim lStatusId As Literal = DirectCast(loRptItem.FindControl("lStatusId"), Literal)
        Dim oDoc1 As DocList
        Dim lSeqno As Literal = DirectCast(loRptItem.FindControl("lSeqNo"), Literal)
        Dim liCtr As Integer
        Dim ldataEmail As DataTable
        Dim ldr As DataRow
        Dim bCommit As Boolean = False
        liCtr = 0

        Try
            ldataEmail = New DataTable("EmailList")
            ldataEmail.Columns.Add("Email", Type.GetType("System.String"))
            ldataEmail.Columns.Add("Name", Type.GetType("System.String"))
            ldataEmail.Columns.Add("RType", Type.GetType("System.String"))

            oDoc1 = New DocList
            'oDoc1.ExistsInInbox()
            lMsg.Text = ""
            Dim lsact As String
            Dim oDocs As New DocRouting
            If dlstat.Items.Count = 0 Then
                ShowMessage("Status is not setup for your group. Please contact administrator.")
                'lMsg.CssClass = "msg_red"
            ElseIf dlstat.SelectedValue = "0" Then
                ShowMessage("Status is required. Please select a status to proceed.")
                'lMsg.CssClass = "msg_red"
                'ElseIf tComment.Text.Trim = "" Then
                '    'lMsg.Text = "Remarks is required. Please enter your remarks to proceed."
                '    'lMsg.CssClass = "msg_red"
                '    ShowMessage("Remarks is required. Please enter your remarks to proceed.")
            Else
                Dim iStat As Integer = 0

                ' ''If rbEval.Checked Then
                ' ''    iStat = 4
                ' ''    lsact = "Set document to 'For Evaluation' status"
                ' ''Else
                ' ''    iStat = 3
                ' ''    lsact = "Acknowledged document"
                ' ''End If
                lsact = dlstat.SelectedItem.Text
                iStat = CInt(dlstat.SelectedValue)

                'If losender.ID = "imgDeny" Then
                '    iStat = 4
                '    lsact = "Set document to 'For Evaluation' status"
                'Else
                '    iStat = 3
                '    lsact = "Acknowledged document"
                'End If
                ltr = New DbTran
                objCommand = New clsSqlConn(ltr.pTran)
                oDocs.pComment = tComment.Text
                oDocs.pDocId = DocSession.sDocID
                oDocs.pApproverId = lAppid.Text
                oDocs.pOldSeqNo = lSeqno.Text
                oDocs.pStatusId = iStat 'approved
                oDocs.pAction = lsact
                oDocs.pIpAddress = Request.UserHostAddress
                oDocs.pUserId = DocSession.sUserId
                oDocs.UpdateRouteStatus(objCommand)

                oDoc1.pDocId = oDocs.pDocId
                oDoc1.pUserId = lAppid.Text
                oDoc1.pSeqNo = oDocs.pOldSeqNo
                oDoc1.AddToInbox(objCommand) ', oDoc1.pExistsInInbox)

                'update document status
                'If DocSession.sDocStatus <> "5" AndAlso DocSession.sDocStatus <> "12" AndAlso DocSession.sDocStatus <> "8" Then
                'If Not DocSession.Archived Then
                Dim liNewHierarchy, liCurrentHierarchy As Integer
                liNewHierarchy = DocSession.GetDocHierarchy(iStat)
                liCurrentHierarchy = DocSession.GetDocHierarchy(DocSession.sDocStatus)
                ' logic for document status updating
                If (DocSession.sDocStatus = "8") OrElse (iStat <> lStatusId.Text AndAlso liNewHierarchy > liCurrentHierarchy) OrElse liNewHierarchy = 0 Then
                    Dim oList As New DocList
                    oList.pDocStatus = iStat
                    oList.pDocId = DocSession.sDocID
                    oList.pUserId = DocSession.sUserId
                    oList.pSeqNo = lSeqno.Text
                    oList.UpdateDoc(objCommand)
                    pStatusId = CStr(iStat)
                    pStatusDesc = lsact 'IIf(iStat = 3, "Acknowledged", "For Evaluation")
                    RaiseEvent e_UpdateStatus()
                End If

                'End If


                If pAddHistory Then
                    Dim ohist As New DocHistory
                    ohist.pDocId = DocSession.sDocID
                    ohist.pIpAddress = Request.UserHostAddress
                    ohist.pTask = "Routing"
                    ohist.pUserId = DocSession.sUserId

                    ohist.pAction = lsact
                    ohist.AddHistory(objCommand)
                End If
                Dim lbadd As Boolean = False
                If iStat > 0 Then
                    Dim lsCopy As String = ""
                    Dim lsEmail As String = ""
                    Dim lsApprovername As String = ""
                    For Each loRptItem In rptList.Items
                        If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                            fSel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                            If fSel.Visible Then

                                lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                                lsApprovername = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                                If pApprovers = "" Then
                                    pApprovers = lsApprovername
                                Else
                                    pApprovers = pApprovers & ", " & lsApprovername
                                End If
                                oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                                oDocs.pDocId = DocSession.sDocID
                                oDocs.pStatusId = 2
                                oDocs.pIpAddress = Request.UserHostAddress
                                oDocs.pUserId = DocSession.sUserId
                                oDocs.pUrgent = IIf(DirectCast(loRptItem.FindControl("imgUrgent2"), ImageButton).Visible, 1, 0)
                                oDocs.pSeqNo = DocSession.getNextID("seqno")
                                oDocs.pOutStatusId = DirectCast(loRptItem.FindControl("dlOutgoingStatus"), DropDownList).SelectedValue
                                oDocs.RouteDocument(objCommand)
                                oDocs.UpdateDocApprover(objCommand)
                                oDocs.pRoutingSeqNo = lSeqno.Text
                                oDocs.UpdateDocRouting(objCommand)
                                'Dim oDoc As New DocList
                                'oDoc.pDocId = pDocId
                                'oDoc.pUserId = oDocs.pApproverId
                                'oDoc.pSeqNo = oDocs.pSeqNo
                                'oDoc.AddToInbox(objCommand)

                                If lsEmail.Trim <> "" Then
                                    'EmailApprovers(lsEmail, lsApprovername)
                                    ldr = ldataEmail.NewRow
                                    ldr("Email") = lsEmail
                                    ldr("Name") = lsApprovername
                                    ldr("RType") = "R"
                                    ldataEmail.Rows.Add(ldr)

                                End If
                                lsCopy = RouteCopy(objCommand, ldataEmail) ', oDoc1.pExistsInInbox)
                                liCtr = liCtr + 1
                            End If
                        End If
                        'lbadd = True

                    Next

                    'If Not lbadd Then
                    '    Dim oDoc1 As New DocList
                    '    oDoc1.pDocId = pDocId
                    '    oDoc1.pUserId = DocSession.sUserId
                    '    oDoc1.pSeqNo = oDocs.pSeqNo
                    '    oDoc1.AddToInbox(objCommand)
                    'End If
                    If liCtr > 0 Then
                        'send email
                        ShowMessage("Document has been routed to the selected users.")
                        'lMsg.CssClass = "msg_green"
                    Else
                        If iStat = 4 Then
                            ShowMessage("Document has been set for evaluation.")
                            'lMsg.CssClass = "msg_green"
                        Else
                            ShowMessage("Document has been acknowledged.")
                            'lMsg.CssClass = "msg_green"
                        End If

                    End If


                    'Else
                    '    lMsg.Text = "Document has been set for evaluation."
                    '    lMsg.CssClass = "msg_green"
                End If
                ltr.pTran.Commit()
                bCommit = True
                If Not ldataEmail Is Nothing AndAlso ldataEmail.Rows.Count > 0 Then
                    EmailApprovers(ldataEmail)
                End If
                RetrieveDocRouting(oDocs.pDocId)

                pnl.Update()

            End If
        Catch ex As MailException
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage(ex.Message)
            pnl.Update()
        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage("Error occurred while routing the document (" & ex.Message & "). Please try again.")
            'lMsg.CssClass = "msg_red"
            'Throw New Exception(ex.Message)
            pnl.Update()
        Finally
            If Not ldataEmail Is Nothing Then
                ldataEmail.Dispose()
                ldataEmail = Nothing
            End If
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

    Public Function FirstApprover() As String
        Dim lsFirstApprover As String = ""
        For Each loRptItem In rpt.Items
            If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then

                'If DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton).Visible Then

                'lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                lsFirstApprover = DirectCast(loRptItem.FindControl("lRemarks"), Literal).Text.Trim
                Exit For
                'End If
            End If
        Next
        Return lsFirstApprover
    End Function

    Public Sub RouteCopy(ByVal sender As Object, ByVal e As EventArgs)

        Dim ltr As DbTran
        Dim objCommand As clsSqlConn
        Dim oDoc1 As DocList
        Dim losender As ImageButton = DirectCast(sender, ImageButton)

        Dim fSel As ImageButton
        Dim loRptItem As RepeaterItem = DirectCast(losender.NamingContainer, RepeaterItem)
        '''Dim rbAck As RadioButton = DirectCast(loRptItem.FindControl("rbAck"), RadioButton)
        '''Dim rbEval As RadioButton = DirectCast(loRptItem.FindControl("rbEval"), RadioButton)
        Dim dlstat As DropDownList = DirectCast(loRptItem.FindControl("dlStatus"), DropDownList)
        If dlstat.SelectedValue = "" Then
            ShowMessage("Please select action to continue.")
            Exit Sub
        End If
        Dim tComment As TextBox = DirectCast(loRptItem.FindControl("txtComment"), TextBox)
        Dim lAppid As Literal = DirectCast(loRptItem.FindControl("lApproverId"), Literal)
        Dim lSeqno As Literal = DirectCast(loRptItem.FindControl("lSeqNo"), Literal)
        Dim lStatusId As Literal = DirectCast(loRptItem.FindControl("lStatusId"), Literal)
        Dim oDocs As DocRouting
        Dim liCtr As Integer
        liCtr = 0
        Dim iStat As Integer = 3
        Try
            
            Dim lsact As String = "Acknowledged document."


            'If tComment.Text.Trim = "" Then
            '    lMsg.Text = "Remarks is required. Please enter your remarks to proceed."
            '    lMsg.CssClass = "msg_red"
            'Else
            oDoc1 = New DocList
            'oDoc1.ExistsInInbox()
            oDocs = New DocRouting

            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            oDocs.pComment = tComment.Text
            oDocs.pDocId = DocSession.sDocID
            oDocs.pApproverId = lAppid.Text
            oDocs.pOldSeqNo = lSeqno.Text
            oDocs.pStatusId = iStat 'approved
            oDocs.pAction = lsact
            oDocs.pIpAddress = Request.UserHostAddress
            oDocs.pUserId = DocSession.sUserId
            oDocs.UpdateRouteStatus(objCommand)


            oDoc1.pDocId = oDocs.pDocId
            oDoc1.pUserId = lAppid.Text
            oDoc1.pSeqNo = oDocs.pOldSeqNo
            oDoc1.AddToInbox(objCommand) ', oDoc1.pExistsInInbox)

            'update document status
            'Dim liNewHierarchy, liCurrentHierarchy As Integer
            'liNewHierarchy = DocSession.GetDocHierarchy(iStat)
            'liCurrentHierarchy = DocSession.GetDocHierarchy(DocSession.sDocStatus)
            ' logic for document status updating
            'If (lStatusId.Text AndAlso liNewHierarchy > liCurrentHierarchy) OrElse liNewHierarchy = 0 Then
            'If DocSession.sDocStatus <> "5" AndAlso DocSession.sDocStatus <> "12" AndAlso DocSession.sDocStatus <> "8" Then
            'If Not DocSession.Archived Then
            If CInt(DocSession.sDocStatus) = 2 Then
                Dim oList As New DocList
                oList.pDocStatus = iStat
                oList.pDocId = DocSession.sDocID
                oList.pUserId = DocSession.sUserId
                oList.pSeqNo = lSeqno.Text
                oList.UpdateDocCopy(objCommand)
                pStatusId = CStr(iStat)
                pStatusDesc = "Acknowledged" 'lsact 'IIf(iStat = 3, "Acknowledged", "For Evaluation")
                RaiseEvent e_UpdateStatus()
            End If

            'End If

            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Routing"
            ohist.pUserId = DocSession.sUserId

            ohist.pAction = lsact
            ohist.AddHistory(objCommand)


            ShowMessage("Document has been acknowledged.")
            'lMsg.CssClass = "msg_green"

            ltr.pTran.Commit()
            RetrieveDocRouting(oDocs.pDocId)

            pnl.Update()

            'End If
        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage("Error occurred while routing the document (" & ex.Message & "). Please try again.")
            'lMsg.CssClass = "msg_red"
            'Throw New Exception(ex.Message)
            pnl.Update()
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
    'Public Function ValidateDueDate() As Boolean
    '    Dim lbReturn As Boolean = True
    '    Dim fsel As ImageButton
    '    For Each loRptItem In rptList.Items
    '        lMsg.Text = ""
    '        If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
    '            fsel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
    '            If fsel.Visible Then


    '                'If DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text.Trim = "" Then
    '                '    lMsg.CssClass = "msg_red"
    '                '    lMsg.Text = "Due Date is required to route a document."
    '                '    ValidateMessage = "Due Date is required to route a document."
    '                '    pnl.Update()
    '                '    lbReturn = False
    '                '    Exit For
    '                'ElseIf Not IsDate(DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text) Then
    '                '    lMsg.CssClass = "msg_red"
    '                '    lMsg.Text = "Invalid Due Date."
    '                '    ValidateMessage = "Invalid Due Date."
    '                '    pnl.Update()
    '                '    lbReturn = False
    '                '    Exit For
    '                'ElseIf CDate(DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text) <= Date.Now Then
    '                '    lMsg.CssClass = "msg_red"
    '                '    lMsg.Text = "Due Date should be greater than the current date."
    '                '    ValidateMessage = "Due Date should be greater than the current date."
    '                '    pnl.Update()
    '                '    lbReturn = False
    '                '    Exit For
    '                'Else
    '                If DirectCast(loRptItem.FindControl("tbRem"), TextBox).Text.Trim = "" Then
    '                    lMsg.CssClass = "msg_red"
    '                    lMsg.Text = "Remarks is required."
    '                    ValidateMessage = "Remarks is required."
    '                    pnl.Update()
    '                    lbReturn = False
    '                    Exit For
    '                End If

    '            End If
    '        End If


    '    Next
    '    Return lbReturn
    'End Function

    'Public Function ValidateDueDate2() As Boolean
    '    Dim lbReturn As Boolean = True
    '    Dim fsel As ImageButton
    '    For Each loRptItem In rpt.Items
    '        lMsg.Text = ""
    '        If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then

    '            If CDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) <> CDate(DirectCast(loRptItem.FindControl("lDueDate"), Label).Text) Then
    '                If DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text.Trim = "" Then
    '                    lMsg.CssClass = "msg_red"
    '                    lMsg.Text = "Due Date is required to route a document."
    '                    ValidateMessage = "Due Date is required to route a document."
    '                    pnl.Update()
    '                    lbReturn = False
    '                    Exit For
    '                ElseIf Not IsDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) Then
    '                    lMsg.CssClass = "msg_red"
    '                    lMsg.Text = "Invalid Due Date."
    '                    ValidateMessage = "Invalid Due Date."
    '                    pnl.Update()
    '                    lbReturn = False
    '                    Exit For
    '                ElseIf CDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) <= Date.Now Then
    '                    lMsg.CssClass = "msg_red"
    '                    lMsg.Text = "Date should be greater than the current date."
    '                    ValidateMessage = "Date should be greater than the current date."
    '                    pnl.Update()
    '                    lbReturn = False
    '                    Exit For
    '                End If

    '            End If
    '        End If


    '    Next

    '    Return lbReturn
    'End Function

    Public Sub DeleteRouting(ByVal objCommand As clsSqlConn)
        Dim oRoute As New DocRouting
        oRoute.pDocId = pDocId
        oRoute.DeleteRouting(objCommand)
    End Sub

    Public Sub RouteDocument2()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")


        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim oDoc As DocList

        Dim liCtr As Integer
        liCtr = 0
        Dim ldataEmail As DataTable
        Dim ldr As DataRow
        Dim bCommit As Boolean = False
        Try
            ldataEmail = New DataTable("EmailList")
            ldataEmail.Columns.Add("Email", Type.GetType("System.String"))
            ldataEmail.Columns.Add("Name", Type.GetType("System.String"))
            ldataEmail.Columns.Add("RType", Type.GetType("System.String"))

            oDoc = New DocList
            'oDoc.ExistsInInbox()
            'oDoc.ExistsInOutbox()
            lMsg.Text = ""
            'If Not ValidateDueDate() Then
            '    Exit Sub
            'End If
            ValidateCopy()

            Dim oDocs As New DocRouting
            oDocs.pDocId = DocSession.sDocID
            Dim licount As Integer = oDocs.CountApprover()
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)

            Dim fsel As ImageButton
            Dim lsEmail As String = ""

            Dim lsApprovername As String = ""
            Dim lsCopy As String = ""
            For Each loRptItem In rptList.Items

                If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                    fsel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                    If fsel.Visible Then

                        lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                        lsApprovername = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                        If pApprovers = "" Then
                            pApprovers = lsApprovername
                        Else
                            pApprovers = pApprovers & ", " & lsApprovername
                        End If
                        oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                        oDocs.pDocId = DocSession.sDocID
                        oDocs.pStatusId = 2
                        oDocs.pIpAddress = Request.UserHostAddress
                        oDocs.pUserId = DocSession.sUserId
                        'oDocs.pDueDate = DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text
                        oDocs.pRemarks = DirectCast(loRptItem.FindControl("tbRem"), TextBox).Text
                        oDocs.pSender = DocSession.sUserId
                        oDocs.pUrgent = IIf(DirectCast(loRptItem.FindControl("imgUrgent2"), ImageButton).Visible, 1, 0)
                        oDocs.pSeqNo = DocSession.getNextID("SeqNo")
                        oDocs.pOutStatusId = DirectCast(loRptItem.FindControl("dlOutgoingStatus"), DropDownList).SelectedValue
                        oDocs.RouteDocument(objCommand)
                        If licount = 0 Then
                            oDocs.pRoutedTo = pApprovers
                            RaiseEvent e_RoutedTo()
                        End If
                        oDocs.UpdateDocApprover(objCommand)
                        oDocs.pRoutingSeqNo = pMaxSeqNo
                        oDocs.UpdateDocRouting(objCommand)
                        'Dim oList As New DocList
                        'If DocSession.sDocStatus = "2" Then
                        '    oList.pDocStatus = 3
                        'End If
                        'oList.pDocId = hfDocid.Value
                        'oList.pUserId = DocSession.sUserId
                        'oList.pSeqNo = oDocs.pSeqNo
                        'oList.UpdateDoc()
                        'pStatusId = CStr(3)
                        'pStatusDesc = "Acknowledged"
                        'RaiseEvent e_UpdateStatus()


                        'oDoc.pDocId = pDocId
                        'oDoc.pUserId = oDocs.pApproverId
                        'oDoc.pSeqNo = oDocs.pSeqNo
                        'oDoc.AddToInbox(objCommand)

                        If pRouteStatusId = "2" OrElse pRouteStatusId = "8" OrElse pRouteStatusId = "12" OrElse pRouteStatusId = "15" OrElse pRouteStatusId = "18" OrElse pRouteStatusId = "19" Then
                            oDoc.pDocStatus = 3
                            oDoc.pDocId = pDocId
                            oDoc.pUserId = DocSession.sUserId
                            oDoc.pSeqNo = oDocs.pSeqNo
                            oDoc.UpdateDoc(objCommand)
                            pStatusId = 3
                            pStatusDesc = "Acknowledged"
                            RaiseEvent e_UpdateStatus()
                        End If
                        

                        oDoc.pDocId = pDocId
                        oDoc.pUserId = DocSession.sUserId
                        oDoc.pSeqNo = oDocs.pSeqNo
                        oDoc.AddToOutbox(objCommand) ', oDoc.pExistsInOutbox)

                        If lsEmail.Trim <> "" AndAlso DocSession.sSendEmail Then
                            'EmailApprovers(lsEmail, lsApprovername)

                            ldr = ldataEmail.NewRow
                            ldr("Email") = lsEmail
                            ldr("Name") = lsApprovername
                            ldr("RType") = "R"
                            ldataEmail.Rows.Add(ldr)
                        End If

                        lsCopy = RouteCopy(objCommand, ldataEmail) ', oDoc.pExistsInInbox)

                        liCtr = liCtr + 1
                    End If
                End If


            Next

            If liCtr > 0 Then
                Dim ohist As New DocHistory
                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Routing"
                ohist.pUserId = DocSession.sUserId
                Dim lsAct As String = "Routed document to " & pApprovers
                If lsCopy <> "" Then
                    lsAct = lsAct & " and copy furnish to " & lsCopy
                End If
                ohist.pAction = lsAct
                ohist.AddHistory(objCommand)

            End If
            ltr.pTran.Commit()
            bCommit = True
            Dim lbErrorEmail As Boolean = False
            If liCtr > 0 Then
                If DocSession.sSendEmail Then
                    If ldataEmail.Rows.Count > 0 Then
                        Try
                            pDocTitle = DocSession.sDocTitle
                            pRefno = DocSession.sReferenceNo
                            EmailApprovers(ldataEmail)
                        Catch ex As MailException
                            lbErrorEmail = True
                            ShowMessage("Document has been routed to the selected user. Email notification is currently unavailable. Please notify the approver manually.")
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try


                    End If
                    If Not lbErrorEmail Then
                        ShowMessage("Document has been routed to the selected user. Email notification was sent to the approver.")
                    End If

                Else
                    ShowMessage("Document has been routed to the selected user. Email notification is currently disabled. Please notify the approver manually.")
                End If

                'lMsg.CssClass = "msg_green"
                RetrieveDocRouting(oDocs.pDocId)

                If rpt.Items.Count = 1 Then
                    RaiseEvent e_RoutedTo()
                End If

                HideRouting()
                'imgAddApprover.Visible = True
                imgSendApprover.Visible = False
            Else
                lMsg.Text = "No user has been selected."
                lMsg.CssClass = "msg_red"
            End If


            pnl.Update()


        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage("Error occurred while routing the document (" & ex.Message & "). Please try again.*")

            'Throw New Exception(ex.Message)
        Finally
            If Not ldataEmail Is Nothing Then
                ldataEmail.Dispose()
                ldataEmail = Nothing
            End If

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

    Public Sub RouteDocumentCC()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")


        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim oDoc As DocList

        Dim liCtr As Integer
        liCtr = 0
        Dim ldataEmail As DataTable
        Dim ldr As DataRow
        Dim bCommit As Boolean = False
        Try
            ldataEmail = New DataTable("EmailList")
            ldataEmail.Columns.Add("Email", Type.GetType("System.String"))
            ldataEmail.Columns.Add("Name", Type.GetType("System.String"))
            ldataEmail.Columns.Add("RType", Type.GetType("System.String"))

            oDoc = New DocList
            'oDoc.ExistsInInbox()
            'oDoc.ExistsInOutbox()
            lMsg.Text = ""
            'If Not ValidateDueDate() Then
            '    Exit Sub
            'End If
            ValidateCopy()

            
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)

            

            Dim lsCopy As String = RouteCopy(objCommand, ldataEmail) ', oDoc.pExistsInInbox)

             

            If lsCopy <> "" Then
                Dim ohist As New DocHistory
                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Routing"
                ohist.pUserId = DocSession.sUserId
                Dim lsAct As String = "Document copy furnish to " & lsCopy

                ohist.pAction = lsAct
                ohist.AddHistory(objCommand)
                ltr.pTran.Commit()
                bCommit = True
            End If


            Dim lbErrorEmail As Boolean = False
            If bCommit Then
                If DocSession.sSendEmail Then
                    If ldataEmail.Rows.Count > 0 Then
                        Try
                            EmailApprovers(ldataEmail)
                        Catch ex As MailException
                            lbErrorEmail = True
                            ShowMessage("Document has been routed to the selected user. Email notification is currently unavailable. Please notify the approver manually.")
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try


                    End If
                    If Not lbErrorEmail Then
                        ShowMessage("Document has been routed to the selected user. Email notification was sent to the approver.")
                    End If

                Else
                    ShowMessage("Document has been routed to the selected user. Email notification is currently disabled. Please notify the approver manually.")
                End If

                'lMsg.CssClass = "msg_green"
                RetrieveDocRouting(DocSession.sDocID)

                HideRouting()
                'imgAddApprover.Visible = True
                imgAddCC.Visible = False
            Else
                lMsg.Text = "No user has been selected."
                lMsg.CssClass = "msg_red"
            End If


            pnl.Update()


        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ShowMessage("Error occurred while routing the document (" & ex.Message & "). Please try again.*")

            'Throw New Exception(ex.Message)
        Finally
            If Not ldataEmail Is Nothing Then
                ldataEmail.Dispose()
                ldataEmail = Nothing
            End If

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

    Private Sub ValidateCopy()
        Dim lsEmail, lsApprovername, lsAppList, lsEmailList As String
        lsAppList = ""
        lsEmailList = ""

        For Each loRptItem In rptCopy.Items
            Dim oDocs As New DocRouting
            If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then

                If DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton).Visible Then

                    'lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                    'lsApprovername = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                    'If lsAppList = "" Then
                    '    lsAppList = lsApprovername
                    '    lsEmailList = lsEmail
                    'Else
                    '    lsAppList = lsAppList & ", " & lsApprovername
                    '    lsEmailList = lsEmailList & ", " & lsEmail
                    'End If
                    oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                    oDocs.pDocId = pDocId 'DocSession.sDocID                    
                    If oDocs.CheckIfCopyExists Then
                        DirectCast(loRptItem.FindControl("lExist"), Literal).Text = "T"
                    End If


                End If
            End If


        Next

    End Sub

    Private Function RouteCopy(ByVal objCommand As clsSqlConn, ByRef adata As DataTable) As String
        Dim lsEmail, lsApprovername, lsAppList, lsEmailList As String
        lsAppList = ""
        lsEmailList = ""
        Dim ldr As DataRow
        Try


            For Each loRptItem In rptCopy.Items
                Dim oDocs As New DocRouting
                If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then

                    If DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton).Visible Then

                        lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                        lsApprovername = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                        If lsAppList = "" Then
                            lsAppList = lsApprovername
                            lsEmailList = lsEmail
                        Else
                            lsAppList = lsAppList & ", " & lsApprovername
                            lsEmailList = lsEmailList & ", " & lsEmail
                        End If
                        oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                        oDocs.pDocId = pDocId 'DocSession.sDocID
                        oDocs.pStatusId = 2
                        oDocs.pIpAddress = Request.UserHostAddress
                        oDocs.pUserId = DocSession.sUserId
                        'oDocs.pDueDate = DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text
                        oDocs.pRemarks = "Copy Furnish"
                        oDocs.pSender = DocSession.sUserId
                        oDocs.pAssignedDate = DateTime.Now.ToString
                        oDocs.pCarbonCopy = "1"
                        oDocs.pUrgent = "0"
                        'oDocs.pOutStatusId = DirectCast(loRptItem.FindControl("dlOutgoingStatus"), DropDownList).SelectedValue
                        'If Not oDocs.CheckIfCopyExists Then
                        If DirectCast(loRptItem.FindControl("lExist"), Literal).Text <> "T" Then
                            oDocs.RouteDocumentCopy(objCommand) ', aExistsInInbox)
                            If lsEmail.Trim <> "" AndAlso DocSession.sSendEmail Then
                                'EmailApproversCopy(lsEmail, lsApprovername)
                                ldr = adata.NewRow
                                ldr("Email") = lsEmail
                                ldr("Name") = lsApprovername
                                ldr("RType") = "C"
                                adata.Rows.Add(ldr)
                            End If
                        End If

                        'End If


                    End If
                End If


            Next

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return lsAppList
    End Function
    Public Function WithApprovers() As Boolean
        Return (rptList.Visible AndAlso rptList.Items.Count >= 1)
    End Function

    Public Function RouteMode() As Boolean
        'Return ((rpt.Items.Count >= 1) OrElse (rptOthers.Items.Count >= 1))
        Return (rpt.Items.Count >= 1)
    End Function

    Public Sub ShowAddApprover()
        'If DocSession.sUserId.ToLower.Trim = pAuthor.ToLower.Trim Then
        'imgAddApprover.Visible = (rpt.Items.Count <= 0)
        'imgSendApprover.Visible = False

        'End If

    End Sub

    Public Sub GetApprovers()

        Dim lsApproverName As String = ""
        pApprovers = lsApproverName
        Dim fSel As ImageButton

        For Each loRptItem In rptList.Items

            If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                fSel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                If fSel.Visible Then

                    lsApproverName = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                    If pApprovers = "" Then
                        pApprovers = lsApproverName
                    Else
                        pApprovers = pApprovers & ", " & lsApproverName
                    End If


                End If

            End If
        Next


    End Sub

    Public Sub GetCCs()

        Dim lsApproverName As String = ""
        pCCs = lsApproverName
        Dim fSel As ImageButton

        For Each loRptItem In rptCopy.Items

            If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                fSel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                If fSel.Visible Then

                    lsApproverName = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                    If pCCs = "" Then
                        pCCs = lsApproverName
                    Else
                        pCCs = pCCs & ", " & lsApproverName
                    End If


                End If

            End If
        Next


    End Sub

    ' for uploading document
    Public Function SaveApprovers(ByVal objCommand As clsSqlConn) As DataTable
        Dim ldataEmail As DataTable
        Dim ldr As DataRow
        Try
            ldataEmail = New DataTable("EmailList")
            ldataEmail.Columns.Add("Email", Type.GetType("System.String"))
            ldataEmail.Columns.Add("Name", Type.GetType("System.String"))
            ldataEmail.Columns.Add("RType", Type.GetType("System.String"))

            'If Not ValidateDueDate() Then
            '    Exit Sub
            'End If
            Dim oDocs As New DocRouting

            Dim lsEmail As String = ""
            Dim lsApproverName As String = ""
            Dim lsApproverNameHist As String = ""
            Dim fSel As ImageButton
            oDocs.pAssignedDate = Date.Now.ToShortDateString
            Dim lsCopy As String = ""
            For Each loRptItem In rptList.Items
                If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
                    fSel = DirectCast(loRptItem.FindControl("ImgSelected"), ImageButton)
                    If fSel.Visible Then
                        lsEmail = DirectCast(loRptItem.FindControl("lEmail"), Literal).Text.Trim
                        lsApproverName = DirectCast(loRptItem.FindControl("lApprover"), Literal).Text.Trim
                        'If pApprovers = "" Then
                        '    pApprovers = lsApproverName
                        'Else
                        '    pApprovers = pApprovers & ", " & lsApproverName
                        'End If
                        oDocs.pSeqNo = pSeqNo
                        oDocs.pApproverId = DirectCast(loRptItem.FindControl("lUserId"), Literal).Text
                        oDocs.pDocId = pDocId 'DocSession.sDocID
                        oDocs.pStatusId = 2
                        oDocs.pUserId = DocSession.sUserId
                        oDocs.pIpAddress = Request.UserHostAddress
                        oDocs.pRemarks = DirectCast(loRptItem.FindControl("tbRem"), TextBox).Text
                        'oDocs.pDueDate = DirectCast(loRptItem.FindControl("tbDue"), TextBox).Text
                        oDocs.pSender = DocSession.sUserId
                        oDocs.pUrgent = IIf(DirectCast(loRptItem.FindControl("imgUrgent2"), ImageButton).Visible, 1, 0)
                        oDocs.pOutStatusId = DirectCast(loRptItem.FindControl("dlOutgoingStatus"), DropDownList).SelectedValue
                        oDocs.RouteDocument(objCommand)

                        If lsEmail.Trim <> "" AndAlso pSendEmail AndAlso DocSession.sSendEmail Then
                            'EmailApprovers(lsEmail, lsApproverName)
                            ldr = ldataEmail.NewRow
                            ldr("Email") = lsEmail
                            ldr("Name") = lsApproverName
                            ldr("RType") = "R"
                            ldataEmail.Rows.Add(ldr)
                        End If

                        'Dim oDoc As New DocList
                        'oDoc.pDocId = pDocId
                        'oDoc.pUserId = oDocs.pApproverId
                        'oDoc.pSeqNo = oDocs.pSeqNo
                        'oDoc.AddToInbox(objCommand)

                        lsCopy = RouteCopy(objCommand, ldataEmail) ', aExistsInInbox)

                    End If

                End If
            Next

            Return ldataEmail
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldataEmail Is Nothing Then
                ldataEmail.Dispose()
                ldataEmail = Nothing
            End If
        End Try

    End Function

    Public Sub EmailApprovers(ByVal adata As DataTable)
        Dim oEmail As DocEmail
        Dim lsAdminEmail As String = System.Configuration.ConfigurationManager.AppSettings("adminemail")
        Dim lsVir As String = System.Configuration.ConfigurationManager.AppSettings("virtualdir")
        Dim asBody As StringBuilder

        Try
            If DocSession.sUserEmail <> "" Then
                lsAdminEmail = DocSession.sUserName & " <" & DocSession.sUserEmail & ">"
            End If

            oEmail = New DocEmail
            For Each ldr As DataRow In adata.Rows

                oEmail.pEmailFrom = lsAdminEmail
                oEmail.pEmailTo = ldr("Email")
                oEmail.pEmailSubject = "Document Management System - Awaiting your Approval"
                'testing


                asBody = New StringBuilder
                If ldr("RType") = "R" Then
                    asBody.Append("<div>Hi ")
                    asBody.Append(ldr("Name"))
                    asBody.Append(",</div>")
                    asBody.Append("<p>You are selected to be one of the approver of this document:</p>")
                    asBody.Append("<p>Title:&nbsp;&nbsp;<b>")
                    asBody.Append(pDocTitle)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Reference No:&nbsp;&nbsp;<b>")
                    asBody.Append(pRefno)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Uploaded By:&nbsp;&nbsp;<b>")
                    asBody.Append(DocSession.sUserName)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Date Uploaded:&nbsp;&nbsp;<b>")
                    asBody.Append(DateTime.Now.ToShortDateString)
                    asBody.Append("</b></p>")
                    asBody.Append("<div>&nbsp;</div>")
                    asBody.Append("<div>&nbsp;</div>")
                    asBody.Append("<div>Please click <a href='")
                    asBody.Append(Request.Url.GetLeftPart(UriPartial.Authority) & IIf(Left(lsVir, 1) = "/", "", "/") & lsVir & "/")
                    asBody.Append("default.aspx'>here</a> to login to the DMS System.</div>")

                    asBody.Append("<br /><br /><div style='font-size:8pt;font-family:arial'><i>This is an auto-generated email notification, please do not reply.</i></div>")
                Else
                    asBody.Append("<div>Hi ")
                    asBody.Append(ldr("Name"))
                    asBody.Append(",</div>")
                    asBody.Append("<p>You have been selected to have a copy of this document:</p>")
                    asBody.Append("<p>Title:&nbsp;&nbsp;<b>")
                    asBody.Append(pDocTitle)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Reference No:&nbsp;&nbsp;<b>")
                    asBody.Append(pRefno)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Uploaded By:&nbsp;&nbsp;<b>")
                    asBody.Append(DocSession.sUserName)
                    asBody.Append("</b></p>")
                    asBody.Append("<p>Date Uploaded:&nbsp;&nbsp;<b>")
                    asBody.Append(DateTime.Now.ToShortDateString)
                    asBody.Append("</b></p>")
                    asBody.Append("<div>&nbsp;</div>")
                    asBody.Append("<div>&nbsp;</div>")
                    asBody.Append("<div>Please click <a href='")
                    asBody.Append(Request.Url.GetLeftPart(UriPartial.Authority) & IIf(Left(lsVir, 1) = "/", "", "/") & lsVir & "/")
                    asBody.Append("/default.aspx'>here</a> to login to the DMS System.</div>")

                    asBody.Append("<br /><br /><div style='font-size:8pt;font-family:arial'><i>This is an auto-generated email notification, please do not reply.</i></div>")
                End If

                oEmail.pEmailBody = asBody.ToString
                oEmail.pEmailIsHTML = True

                oEmail.SendEmail()
            Next
        Catch ex As MailException
            Throw New MailException(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not adata Is Nothing Then
                adata.Dispose()
                adata = Nothing
            End If
        End Try
    End Sub




    Private Sub imgHelp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHelp.Click
        ShowRouteInstruction()

    End Sub

    Private Sub imgAddApprover_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddApprover.Click
        lMsg.Text = ""
        ShowRouting()
        imgAddApprover.Visible = False

    End Sub

    Private Sub imgSendApprover_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSendApprover.Click
        RouteDocument2()
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        lMsg.Text = ""
        HideRouting()
        imgAddApprover.Visible = True
        imgSendApprover.Visible = False
    End Sub

    'Private Sub imgSaveDueDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSaveDueDate.Click
    '    Dim orouting As DocRouting
    '    Dim ltr As New DbTran
    '    Dim objCommand As clsSqlConn
    '    Try
    '        'If Not ValidateDueDate2() Then
    '        'Exit Sub
    '        'End If

    '        lMsg.Text = ""
    '        orouting = New DocRouting
    '        objCommand = New clsSqlConn(ltr.pTran)
    '        Dim ohist As New DocHistory
    '        Dim lsTask As String
    '        For Each loRptItem In rpt.Items
    '            If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
    '                'If IsDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) Then
    '                'If CDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) <> CDate(DirectCast(loRptItem.FindControl("lDueDate"), Label).Text) Then
    '                'orouting.pDueDate = DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text
    '                orouting.pSeqNo = DirectCast(loRptItem.FindControl("lSeqNo"), Literal).Text

    '                If DocSession.sUserRole = "D" Then
    '                    orouting.UpdateDueDate2(objCommand)
    '                    ReEmailApprover(DirectCast(loRptItem.FindControl("lAppEmail"), Literal).Text, DirectCast(loRptItem.FindControl("lApprover"), Literal).Text)
    '                Else
    '                    orouting.UpdateDueDate(objCommand)

    '                End If
    '                ohist.pDocId = DocSession.sDocID
    '                ohist.pIpAddress = Request.UserHostAddress
    '                ohist.pTask = "Routing"
    '                ohist.pUserId = DocSession.sUserId
    '                If DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim.Length > 500 Then
    '                    lsTask = DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim.Substring(0, 500) & "..."
    '                Else
    '                    lsTask = DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim
    '                End If
    '                ohist.pAction = "Updated Task(" & lsTask & ") due date from " & CDate(DirectCast(loRptItem.FindControl("lDueDate"), Label).Text).ToShortDateString & " to " & DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text & "."
    '                ohist.AddHistory(objCommand)
    '                'End If
    '                'End If

    '            End If
    '        Next
    '        'For Each loRptItem In rptOthers.Items
    '        '    If loRptItem.ItemType = ListItemType.Item Or loRptItem.ItemType = ListItemType.AlternatingItem Then
    '        '        If IsDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) Then
    '        '            If CDate(DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text) <> CDate(DirectCast(loRptItem.FindControl("lDueDate"), Label).Text) Then
    '        '                orouting.pDueDate = DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text
    '        '                orouting.pSeqNo = DirectCast(loRptItem.FindControl("lSeqNo"), Literal).Text

    '        '                If DocSession.sUserRole = "D" Then
    '        '                    orouting.UpdateDueDate2(objCommand)
    '        '                    ReEmailApprover(DirectCast(loRptItem.FindControl("lAppEmail"), Literal).Text, DirectCast(loRptItem.FindControl("lApprover"), Literal).Text)
    '        '                Else
    '        '                    orouting.UpdateDueDate(objCommand)

    '        '                End If
    '        '                ohist.pDocId = DocSession.sDocID
    '        '                ohist.pIpAddress = Request.UserHostAddress
    '        '                ohist.pTask = "Routing"
    '        '                ohist.pUserId = DocSession.sUserId
    '        '                If DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim.Length > 500 Then
    '        '                    lsTask = DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim.Substring(0, 500) & "..."
    '        '                Else
    '        '                    lsTask = DirectCast(loRptItem.FindControl("lRemarks"), Label).Text.Trim
    '        '                End If
    '        '                ohist.pAction = "Updated Task(" & lsTask & ") due date from " & CDate(DirectCast(loRptItem.FindControl("lDueDate"), Label).Text).ToShortDateString & " to " & DirectCast(loRptItem.FindControl("txDueDate"), TextBox).Text & "."
    '        '                ohist.AddHistory(objCommand)
    '        '            End If
    '        '        End If

    '        '    End If
    '        'Next
    '        ltr.pTran.Commit()
    '        lMsg.Text = "Update successful."
    '        lMsg.CssClass = "msg_green"
    '        pnl.Update()
    '    Catch ex As Exception
    '        ltr.pTran.Rollback()
    '        lMsg.Text = "Error occurred while updating the due date (" & ex.Message & "). Please try again."
    '        lMsg.CssClass = "msg_red"
    '        'Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If
    '    End Try
    'End Sub


    Private Sub dlSearchType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlSearchType.SelectedIndexChanged
        'If DocSession.sDocType = "" Then
        If pDocType2 = "" Then
            lSearchMsg.Visible = True
            lSearchMsg.Text = "Please select a Document Type before selecting a group."
        Else
            lSearchMsg.Text = ""
            If dlSearchType.Text = "g" Then
                dlGroups.Visible = True
                Dim odg As New DocGroup
                odg.pDocType = pDocType2 'DocSession.sDocType
                dlGroups.DataSource = odg.RetrieveDocTypeGroups()

                dlGroups.DataValueField = "groupid"
                dlGroups.DataTextField = "groupname"
                dlGroups.DataBind()
                txtBx.Visible = False
            Else
                txtBx.Visible = True
                dlGroups.Visible = False

            End If
        End If
        pnl.Update()
    End Sub

    'Private Sub rptOthers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptOthers.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim lAD As Label = DirectCast(e.Item.FindControl("lActionDate"), Label)
    '        Dim lAppId = DirectCast(e.Item.FindControl("lApproverId"), Literal)
    '        Dim lSender = DirectCast(e.Item.FindControl("lSenderId"), Literal)
    '        If DirectCast(e.Item.FindControl("lStatusId"), Literal).Text = "2" Then
    '            HideAdd()
    '        End If
    '        If (lSender.Text.Trim.ToLower = DocSession.sUserId.ToLower OrElse DocSession.sUserRole = "D") AndAlso DocSession.sCheckout <> "Yes" AndAlso CInt(DocSession.sDocTypeAccess) > 1 Then
    '            'DirectCast(e.Item.FindControl("imgApprove"), ImageButton).Visible = True
    '            'DirectCast(e.Item.FindControl("imgDeny"), ImageButton).Visible = True
    '            If DocSession.Archived Then
    '            Else
    '                DirectCast(e.Item.FindControl("txDueDate"), TextBox).Visible = True
    '                DirectCast(e.Item.FindControl("lDueDate1"), Label).Visible = False
    '                DirectCast(e.Item.FindControl("lDueDate"), Label).Visible = False
    '                DirectCast(e.Item.FindControl("lDueDate2"), Label).Visible = False
    '                imgSaveDueDate.Visible = True
    '            End If

    '        End If
    '    End If
    'End Sub

    Private Sub imgQuickApprover_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgQuickApprover.Click
        If imgQuickApprover.Visible Then
            RouteDocument3()
        Else
            RouteDocument2()
        End If


    End Sub

    'Private Sub imgCbsy_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCbsy.Click
    '    imgCbsy.Visible = False
    '    imgCbs.Visible = True
    '    CopyOption.Update()

    'End Sub

    'Private Sub imgCbs_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCbs.Click
    '    imgCbsy.Visible = True
    '    imgCbs.Visible = False
    '    CopyOption.Update()

    'End Sub

    'Private Sub rptList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptList.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '        If DirectCast(e.Item.FindControl("tbRem"), Literal).Text = "(Copy)" Then
    '            DirectCast(e.Item.FindControl("tbRem"), TextBox).Visible = False
    '            DirectCast(e.Item.FindControl("tbDue"), TextBox).Visible = False
    '            DirectCast(e.Item.FindControl("lCopy"), Literal).Visible = True
    '        End If
    '    End If
    'End Sub

    Private Sub imgCCSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCCSearch.Click
        Dim lodata As DataTable
        Try
            lCCSearchMsg.Visible = False
            Dim oDocs As New DocUser
            If dlCCSearchType.SelectedValue = "u" AndAlso txtCCBx.Text.Trim = "Enter User Name..." Then
                lCCSearchMsg.Visible = True
                lCCSearchMsg.Text = "Please enter user name first before clicking Search icon."
            ElseIf pDocType2 = "" Then
                lCCSearchMsg.Visible = True
                lCCSearchMsg.Text = "Please select a Document Type before clicking Search icon."
            Else
                oDocs.pSearchString = txtCCBx.Text
                oDocs.pDocType = pDocType2 'DocSession.sDocType
                oDocs.pUserId = DocSession.sUserId
                If dlCCSearchType.SelectedValue = "g" Then
                    oDocs.pGroup = dlCCGroups.SelectedValue

                    lodata = oDocs.RetrieveUserByGroup
                Else
                    lodata = oDocs.RetrieveUsersWithAccess
                End If

                If lodata.Rows.Count > 0 Then
                    rptCCSub.DataSource = lodata
                    rptCCSub.DataBind()
                    rptCCSub.Visible = True
                    rptCCSub.Focus()
                Else
                    rptCCSub.Visible = False
                    lCCSearchMsg.Visible = True
                    lCCSearchMsg.Text = "No records found."
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If
        End Try

        pnl.Update()
    End Sub

    Private Sub btContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btContinue.Click

        CancelRouting(pMaxSeqNo)

    End Sub

    Public Sub CancelCCRouting()
        CancelCopyFurnish(hfCCSeqNoToCancel.Value)
    End Sub
    Public Sub CancelRoute()
        CancelRouting(hfMaxSeqNoToCancel.Value)
    End Sub

    Public Sub CancelRouting(ByVal aSeqNo As String)
        Try
            Dim oDoc As DocRouting
            oDoc = New DocRouting
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pSeqNo = aSeqNo
            'oDoc.ArchiveDoc()
            oDoc.CancelRouting()
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Cancel"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Cancel document routing"
            ohist.AddHistory()
            RetrieveDocRouting(DocSession.sDocID)
            Dim oDocList As New DocList
            If pApproverCount <= 0 Then

                oDocList.pDocId = DocSession.sDocID
                oDocList.pUserId = DocSession.sUserId
                oDocList.pSeqNo = pMaxSeqNo
                oDocList.pRoutedTo = ""
                oDocList.UpdateDoc()

            Else
                oDocList.pDocId = DocSession.sDocID
                oDocList.pUserId = DocSession.sUserId
                oDocList.pSeqNo = pMaxSeqNo                'oDocList.pRoutedTo = " "
                oDoc.pSeqNo = pMaxSeqNo
                oDoc.pDocId = DocSession.sDocID
                oDocList.pRoutedTo = oDoc.RetriveCurrentApprover

                oDocList.UpdateDoc()

            End If
            pApprovers = oDocList.pRoutedTo
            RaiseEvent e_RoutedTo()
            ShowMessage("Document routing has been canceled successfully.")
        Catch ex As Exception
            ShowMessage("Error occured while processing your transaction(" & ex.Message & "). Please try again.")
        End Try
    End Sub


    Public Sub CancelCopyFurnish(ByVal aSeqNo As String)
        Try
            Dim oDoc As DocRouting
            oDoc = New DocRouting
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pSeqNo = aSeqNo
            'oDoc.ArchiveDoc()
            oDoc.CancelRouting()
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Cancel"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Cancel Copy Furnish"
            ohist.AddHistory()
            RetrieveDocRouting(DocSession.sDocID)

            ShowMessage("Copy Furnish has been canceled successfully.")
        Catch ex As Exception
            ShowMessage("Error occured while processing your transaction(" & ex.Message & "). Please try again.")
        End Try
    End Sub

    Private Sub dlCCSearchType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlCCSearchType.SelectedIndexChanged

        'If DocSession.sDocType = "" Then
        If pDocType2 = "" Then
            lCCSearchMsg.Visible = True
            lCCSearchMsg.Text = "Please select a Document Type before selecting a group."
        Else
            lCCSearchMsg.Text = ""
            If dlCCSearchType.Text = "g" Then
                dlCCGroups.Visible = True
                Dim odg As New DocGroup
                odg.pDocType = pDocType2 'DocSession.sDocType
                dlCCGroups.DataSource = odg.RetrieveDocTypeGroups()

                dlCCGroups.DataValueField = "groupid"
                dlCCGroups.DataTextField = "groupname"
                dlCCGroups.DataBind()
                txtCCBx.Visible = False
            Else
                txtCCBx.Visible = True
                dlCCGroups.Visible = False

            End If
        End If
        pnl.Update()

    End Sub

    Private Sub btSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveCancel.Click
        pConfirm.Visible = False
        pnl.Update()
    End Sub

    Private Sub rptList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dlOS As DropDownList = DirectCast(e.Item.FindControl("dlOutgoingStatus"), DropDownList)
            Dim oDocType As New DocTypes
            oDocType.pGroupId = DocSession.sUserGroup
            If pTitle.Visible Then
                dlOS.Visible = True
                dlOS.DataSource = oDocType.GetOutgoingDocStatus
                dlOS.DataTextField = "Description"
                dlOS.DataValueField = "StatusId"
                dlOS.DataBind()
            Else
                dlOS.Visible = False
            End If
        End If
    End Sub

    Private Sub imgAddCC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddCC.Click
        ShowCCSearch()
    End Sub

    Private Sub imgSendCC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSendCC.Click
        RouteDocumentCC()
    End Sub

    Private Sub imgCloseCC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseCC.Click
        lMsg.Text = ""
        HideCCSearch()
        imgAddCC.Visible = True
    End Sub
End Class
