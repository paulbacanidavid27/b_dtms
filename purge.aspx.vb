Public Class purge
    Inherits System.Web.UI.Page
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Account")
        ucMenu.SelectTab("Purge")
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        If Not IsPostBack Then
            'DisplayDeleteCriteria()
            SetDDL()
            GetUsers()
        End If
    End Sub
    Private Sub SetDDL()
        Dim oDoc As New DocTypes
        Dim ldata As DataTable
        Dim lrow As DataRow
        Try

            oDoc = New DocTypes

            oDoc.pGroupId = DocSession.sUserGroup

            ldata = oDoc.GetDocType
            lrow = ldata.NewRow
            lrow("DocType") = ""
            lrow("DocName") = "-All-"
            ldata.Rows.InsertAt(lrow, 0)
            'dlPDocType.DataTextField = "DocName"
            'dlPDocType.DataValueField = "DocType"
            'dlPDocType.DataSource = ldata
            'dlPDocType.DataBind()

            dlDDocType.DataTextField = "DocName"
            dlDDocType.DataValueField = "DocType"
            dlDDocType.DataSource = ldata
            dlDDocType.DataBind()

            'dlRDocType.DataTextField = "DocName"
            'dlRDocType.DataValueField = "DocType"
            'dlRDocType.DataSource = ldata
            'dlRDocType.DataBind()
            Dim oType As DocGroup
            oType = New DocGroup

            ldata = oType.RetrieveOffice
            If ldata.Rows.Count > 0 Then
                lrow = ldata.NewRow
                lrow("OfficeCode") = ""
                lrow("Description") = "-All-"
                ldata.Rows.InsertAt(lrow, 0)
            End If

            'dlPOffice.DataSource = ldata
            'dlPOffice.DataValueField = "OfficeCode"
            'dlPOffice.DataTextField = "Description"
            'dlPOffice.DataBind()

            'dlROffice.DataSource = ldata
            'dlROffice.DataValueField = "OfficeCode"
            'dlROffice.DataTextField = "Description"
            'dlROffice.DataBind()

            dlDOffice.DataSource = ldata
            dlDOffice.DataValueField = "OfficeCode"
            dlDOffice.DataTextField = "Description"
            dlDOffice.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    'Public Sub DisplayPurgeCriteria()
    '    pPurgeCriteria.Visible = True
    '    pDeleteCriteria.Visible = False
    '    pRestoreCriteria.Visible = False

    'End Sub
    'Public Sub DisplayDeleteCriteria()
    '    pPurgeCriteria.Visible = False
    '    pDeleteCriteria.Visible = True
    '    pRestoreCriteria.Visible = False

    'End Sub
    'Public Sub DisplayRestoreCriteria()
    '    pPurgeCriteria.Visible = False
    '    pDeleteCriteria.Visible = False
    '    pRestoreCriteria.Visible = True

    'End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        If Repeater1.Visible Then


            Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

            Dim img As Image
            hfCurrent.Value = "1"
            'DocSession.doc_DocCurrentPage = hfCurrent.Value
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
            If imgSort6.ID = "imgSort" & Right(lbSort.ID, 1) Then
                img = imgSort6
                imgSort6.Visible = True
            Else
                imgSort6.Visible = False

            End If
            'If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
            'img = imgSort5
            'imgSort5.Visible = True
            'Else
            'imgSort5.Visible = False

            'End If


            'Dim oDocList As New DocList

            If img.Visible Then
                If img.ImageUrl.ToLower = "images/asc.png" Then
                    img.ImageUrl = "images/desc.png"
                    'oDocList.pSortOrder = "Desc"
                    'DocSession.doc_SortOrder = "Desc"
                    hfSortOrder.Value = "Desc"
                Else
                    img.ImageUrl = "images/asc.png"
                    'oDocList.pSortOrder = "Asc"
                    'DocSession.doc_SortOrder = "Asc"
                    hfSortOrder.Value = "Asc"
                End If
            Else
                img.ImageUrl = "images/asc.png"
                'oDocList.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
                'DocSession.doc_SortOrder = "Asc"
                img.Visible = True
            End If
            'DocSession.doc_SortCol = lbSort.Text
            hfSortCol.Value = lbSort.Text
            'oDocList.pDocId = ""
            'oDocList.pDocType = dlFilterDocType.SelectedValue
            'oDocList.pDocTitle = tbFilterTitle.Text
            'oDocList.pDocStatus = dlFilterDocStatus.SelectedValue
            'oDocList.pGroupId = DocSession.sUserGroup
            'oDocList.pIdx = hfCurrent.Value
            'oDocList.pRowsPerPage = DocSession.RowsPerPage
            'oDocList.pSortCol = lbSort.Text
            'oDocList.pUserId = DocSession.sUserId
            'Dim ldata As DataTable = oDocList.RetrieveDocs

            'If ldata.Rows.Count > DocSession.RowsPerPage Then
            '    'imgGreater.Visible = True
            '    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            'Else
            '    'imgGreater.Visible = False
            'End If

            'lMsg.Visible = False
            ''hfTotalRows.Value = CStr(retval.pParam.Value)
            'ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))

            'Repeater1.DataSource = ldata

            'Repeater1.DataBind()
            'pnlRepeater.Update()

            RetAction()
        End If
    End Sub

#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveRecords()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub
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


    Private Sub btRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRetrieve.Click
        
            RetrieveRecords()
            pnlRepeater2.Update()


    End Sub

    Private Sub ShowDocument(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim imgButton As ImageButton = DirectCast(sender, ImageButton)
        Dim rptItem As RepeaterItem = DirectCast(imgButton.NamingContainer, RepeaterItem)
        Dim lD As Literal = DirectCast(rptItem.FindControl("lDocId"), Literal)
        Dim lDT As Literal = DirectCast(rptItem.FindControl("lDocType"), Literal)
        Dim lDTAccess As Literal = DirectCast(rptItem.FindControl("lGroupAccessId"), Literal)

        DocSession.sDocID = lD.Text
        DocSession.sDocType = lDT.Text
        DocSession.sDocTypeAccess = lDTAccess.Text
        Response.Redirect("view.aspx")

    End Sub

    Private Sub RetrieveRecords()


        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn

            'If rbProcess.SelectedValue = "Archive" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST"
            'ElseIf rbProcess.SelectedValue = "Purge" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST_HIST"
            'ElseIf rbProcess.SelectedValue = "Restore" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST_HIST"
            'End If
            Dim odocs As New DocList

            odocs.pIdx = hfCurrent.Value
            odocs.pRowsPerPage = DocSession.RowsPerPage
            odocs.pGroupId = DocSession.sUserGroup
            odocs.pSortCol = hfSortCol.Value
            odocs.pSortOrder = hfSortOrder.Value
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.ParametersAddWithValue("@AIYEAR", aiYears)
            'objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            ''objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            'objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
            'objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)
            'objCommand.ParametersReturnValue()
            ''Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            ''Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue

            If pDeleteCriteria.Visible Then
                If tbRefNo.Text.IndexOf(",") > 0 Then
                    odocs.pRefNo = " IN ('" & tbRefNo.Text.Replace(",", "','") & "')"
                Else
                    If tbRefNo.Text.Trim <> "" Then
                        odocs.pRefNo = " LIKE '%" & tbRefNo.Text & "%' "
                    End If

                End If

                If tbTitle.Text.Trim <> "" Then
                    odocs.pDocTitle = tbTitle.Text.Trim
                End If

                    If dlUser.SelectedValue <> "" Then
                        odocs.pUserId = dlUser.SelectedValue
                    End If

                    'odocs.pAgeProcess = rbAge.SelectedValue
                    odocs.pProcess = "<>"
                    odocs.pDocType = dlDDocType.SelectedValue
                    odocs.pOfficeCode = dlDOffice.SelectedValue
                    'odocs.pAgeProcess = tbAge.Text
                    'ElseIf lPurge.Visible Then
                    '    odocs.pArchiveFrom = txPurgeFrom.Text
                    '    odocs.pArchiveTo = txPurgeTo.Text
                    '    odocs.pDocType = dlPDocType.SelectedValue
                    '    odocs.pOfficeCode = dlPOffice.SelectedValue
                    '    odocs.pProcess = "="
                    'ElseIf lRestore.Visible Then
                    '    odocs.pArchiveFrom = txRestoreFrom.Text
                    '    odocs.pArchiveTo = txRestoreTo.Text
                    '    odocs.pDocType = dlRDocType.SelectedValue
                    '    odocs.pOfficeCode = dlROffice.SelectedValue
                    '    odocs.pProcess = "="
            End If
            ldata = odocs.RetrieveArchiveDocs
            If ldata.Rows.Count <= 0 Then
                'btProcess.Visible = False
                Master.ShowMessage("No records found.")
                Repeater1.Visible = False
                ucPager.Visible = False
            Else
                ucPager.Visible = True
                If ldata.Rows.Count > DocSession.RowsPerPage Then
                    'imgGreater.Visible = True
                    'imgGreaterD.Visible = False
                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
                Else
                    'imgGreater.Visible = False
                    'imgGreaterD.Visible = True
                End If

                Dim lstotalrows As String = odocs.CountArchiveDoc

                hfTotalRows.Value = lstotalrows 'oDocs.pRetVal
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                'ucPagerBottom.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()
                'pagerBottom.Update()


                Repeater1.DataSource = ldata
                Repeater1.DataBind()
                Repeater1.Visible = True
                'btProcess.Visible = True
            End If


        Catch ex As Exception
            'Throw New Exception(ex.Message)
            'lmsg.CssClass = "msg_red"
            Master.ShowMessage("An error occurred while processing records ( " & ex.Message & " ). Please try again")


        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Sub

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgU As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            'If rbProcess.SelectedValue = "Archive" Then
            '    AddHandler imgU.Click, AddressOf ShowDocument
            '    imgU.ToolTip = "View document info."
            'Else
            '    imgU.ToolTip = "Document info for archived records cannot be viewed."
            'End If

        End If
    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))

            lblDocName.ImageUrl = lext
            'If lDelete.Visible Then
            DirectCast(e.Item.FindControl("ModifiedDate"), Literal).Visible = True
            DirectCast(e.Item.FindControl("lArchivedDate"), Literal).Visible = False
            'Else
            '   DirectCast(e.Item.FindControl("lArchivedDate"), Literal).Visible = True
            'DirectCast(e.Item.FindControl("ModifiedDate"), Literal).Visible = False
            'End If

        End If
    End Sub

    'Private Sub btProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btProcess.Click
    '    If tbAge.Text <> "" AndAlso CInt(tbAge.Text) > 0 Then
    '        ShowConfirm()
    '        'ArchiveRecords(CInt(tbAge.Text))

    '    End If

    'End Sub

    Private Sub ArchiveRecords(ByVal aiYears As Integer)


        Dim objCommand As clsSqlConn

        Try

            objCommand = New clsSqlConn
            'If rbProcess.SelectedValue = "Archive" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST_ARCHIVE"
            'ElseIf rbProcess.SelectedValue = "Purge" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST_PURGE"
            'ElseIf rbProcess.SelectedValue = "Restore" Then
            '    objCommand.CommandText = "DMSP_PURGE_DOCLIST_RESTORE"
            'End If

            objCommand.CommandType = CommandType.StoredProcedure

            objCommand.ParametersAddWithValue("@AIYEAR", aiYears)
            objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            'objCommand.ParametersAddWithValue("@GroupId", DocSession.sUserGroup)
            'objCommand.ParametersAddWithValue("@Idx", CInt(hfCurrent.Value))
            'objCommand.ParametersAddWithValue("@RowsPerpage", DocSession.RowsPerPage)


            objCommand.ExecNonQuery()
            'adpSecurity.SelectCommand = objCommand
            'adpSecurity.Fill(ldata)



            'If ldata.Rows.Count > DocSession.RowsPerPage Then
            '    imgAGreater.Visible = True
            '    imgAGreaterD.Visible = False
            '    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            'Else
            '    imgAGreater.Visible = False
            '    imgAGreaterD.Visible = True
            'End If

            'If CInt(hfCurrent.Value) > 1 Then
            '    imgALess.Visible = True
            '    imgALessD.Visible = False
            'Else
            '    imgALess.Visible = False
            '    imgALessD.Visible = True
            'End If
            'lRecordCount.Text = CStr(ldata.Rows.Count)
            Repeater1.Visible = False
            'Repeater1.DataBind()

            'lNoOfRecord.Visible = True
            'lNo.Visible = True
            'If ldata.Rows.Count = 0 Then
            '    lMsg.Visible = True
            'Else
            '    lMsg.Visible = False
            'End If

            'idSrchRslt.Visible = True
            'lmsg.CssClass = "msg_green"
            'If rbProcess.SelectedValue = "Archive" Then
            '    Master.ShowMessage("Records has been archived successfully.")
            'ElseIf rbProcess.SelectedValue = "Purge" Then
            '    Master.ShowMessage("Records has been purged successfully.")
            'ElseIf rbProcess.SelectedValue = "Restore" Then

            'Master.ShowMessage("Records has been restored successfully.")
            'End If

            'lmsg.Visible = True
            'lPageCount.Visible = False
            Master.ShowImageDocument = False
            pnlRepeater2.Update()
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            'lmsg.CssClass = "msg_red"
            Master.ShowMessage("An error occurred while processing records ( " & ex.Message & " ). Please try again")
        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub

    Protected Sub imgLess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) 'Handles imgLess.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, -1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        'pnlRepeater.Update()

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = lIdx
        RetrieveRecords()
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

    Protected Sub imgGreater_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) ' Handles 'imgGreater.Click
        'Dim lsDate As String
        'lsDate = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToShortDateString
        'lDate.Text = DateAdd(DateInterval.Day, 1, CDate(lDate.Text)).ToLongDateString
        'RetrieveDocAction(lsDate)

        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = lIdx
        RetrieveRecords()
        'imgGreater.Visible = (lDate.Text.Trim <> DateTime.Now.ToLongDateString.Trim)
        pnlRepeater.Update()
    End Sub

    Private Sub ShowConfirm()
        'lblTitle.Text = rbProcess.SelectedValue.ToUpper & " " & "DOCUMENTS - Confirm"
        pDeleteDoc.Visible = True
        pnlDeleteDoc.Update()
        Master.ShowImageDocument = True
        'removeScrollBar()
    End Sub

    Private Sub btOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOK.Click
        ' ArchiveRecords(CInt(tbAge.Text))
    End Sub

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        Master.ShowImageDocument = False
    End Sub

    Private Sub imgClose2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose2.Click
        Master.ShowImageDocument = False
    End Sub

    Private Sub rbProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles rbProcess.SelectedIndexChanged
        'lmsg.Text = ""
        'lmsg.Visible = False
        'btProcess.Visible = False
        Repeater1.Visible = False
        'lRecordCount.Visible = False
        'imgGreater.Visible = False
        'imgLess.Visible = False
        'imgGreaterD.Visible = False
        'imgLessD.Visible = False
        'lPageCount.Visible = False
        hfCurrent.Value = "1"
    End Sub

   
    'Private Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
    '    'ResetTab()
    '    imgProcess.ImageUrl = "images/delete_doc.png"
    '    imgProcess.ToolTip = "Delete selected documents"
    '    lbSort3.Visible = True
    '    lbSort6.Visible = False
    '    'imgSort3.Visible = True
    '    'imgSort6.Visible = False

    '    'tdArchive1.Attributes("class") = "selLTab"
    '    'tdArchive2.Attributes("class") = "selMidTab"
    '    'tdArchive3.Attributes("class") = "selRTab"
    '    'lbDelete.Visible = False
    '    'lDelete.Visible = True
    '    'DisplayDeleteCriteria()
    '    'pnlTab.Update()
    '    Repeater1.Visible = False
    '    lxls.Visible = False
    '    lbSearch.Visible = False
    '    pnlRepeater2.Update()

    'End Sub

    'Private Sub lbRestore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRestore.Click
    '    ResetTab()
    '    imgProcess.ImageUrl = "images/restore.gif"
    '    imgProcess.ToolTip = "Restore selected documents"
    '    lbSort3.Visible = False
    '    lbSort6.Visible = True
    '    'imgSort3.Visible = False
    '    'imgSort6.Visible = True
    '    'tdRestore1.Attributes("class") = "selLTab"
    '    'tdRestore2.Attributes("class") = "selMidTab"
    '    'tdRestore3.Attributes("class") = "selRTab"
    '    'lbRestore.Visible = False
    '    'lRestore.Visible = True
    '    DisplayRestoreCriteria()
    '    pnlTab.Update()
    '    Repeater1.Visible = False
    '    lxls.Visible = True
    '    lbSearch.Visible = True
    '    pnlRepeater2.Update()

    'End Sub

    'Private Sub lbPurge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPurge.Click
    '    ResetTab()
    '    imgProcess.ImageUrl = "images/purge.png"
    '    imgProcess.ToolTip = "Purge selected documents"
    '    lbSort3.Visible = False
    '    lbSort6.Visible = True
    '    'imgSort3.Visible = False
    '    'imgSort6.Visible = True
    '    'tdPurge1.Attributes("class") = "selLTab"
    '    'tdPurge2.Attributes("class") = "selMidTab"
    '    'tdPurge3.Attributes("class") = "selRTab"
    '    'lbPurge.Visible = False
    '    'lPurge.Visible = True
    '    DisplayPurgeCriteria()
    '    pnlTab.Update()
    '    Repeater1.Visible = False
    '    lxls.Visible = False
    '    lbSearch.Visible = False
    '    pnlRepeater2.Update()
    'End Sub

    

    Private Sub cbSelectAll_e_check() Handles cbSelectAll.e_check
        Dim lbCheck As Boolean
        If cbSelectAll.BoxCheck Then
            lbCheck = True
        Else
            lbcheck = False

        End If
        For Each rItems As RepeaterItem In Repeater1.Items
            DirectCast(rItems.FindControl("cbPurge"), UserControlCheckBox).BoxCheck = lbCheck
        Next

    End Sub

    Private Sub imgProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcess.Click
        Dim lbProcess As Boolean = WithSelected()
        Dim lprocess As String = ""
        If lbProcess Then
            If pDeleteCriteria.Visible Then
                ucCon.pText = "Are you sure you want to delete the selected document(s)? Please click OK to confirm."
                ucCon.Visible = True

                'ElseIf lRestore.Visible Then
                '    ucCon.pText = "Are you sure you want to restore the selected document(s)? Please click OK to confirm."
                '    ucCon.Visible = True
                'ElseIf lPurge.Visible Then
                '    ucCon.pText = "Are you sure you want to purge the selected document(s)? Purge document cannot be restored. Please click OK to confirm."
                '    ucCon.Visible = True
            End If
        Else
            'If lDelete.Visible Then
            lprocess = "Delete"

            'ElseIf lRestore.Visible Then
            '    lprocess = "Restore"
            'ElseIf lPurge.Visible Then
            '    lprocess = "Purge"
            'End If
            Master.ShowMessage("Please select record before clicking the " & lprocess & " button.")
        End If
        pnlRepeater.Update()
    End Sub

    Public Function WithSelected() As Boolean
        Dim lbret As Boolean = False
        For Each rItems As RepeaterItem In Repeater1.Items
            If DirectCast(rItems.FindControl("cbPurge"), UserControlCheckBox).BoxCheck Then
                lbret = True
                Exit For
            End If
        Next
        Return lbret
    End Function
    Public Sub ProcessDoc()
        Dim odoc As DocList
        Dim objcommand As clsSqlConn
        Dim ltr As DbTran
        Dim lsMsg As String = ""
        Try
            ltr = New DbTran
            odoc = New DocList

            objcommand = New clsSqlConn(ltr.pTran)
            Dim lsDate As String = DateTime.Now.ToString
            For Each rItems As RepeaterItem In Repeater1.Items
                If DirectCast(rItems.FindControl("cbPurge"), UserControlCheckBox).BoxCheck Then
                    If pDeleteCriteria.Visible Then 'delete
                        odoc.pDocId = DirectCast(rItems.FindControl("lDocId"), Literal).Text
                        odoc.pModifiedDate = lsDate
                        odoc.pUserId = DocSession.sUserId
                        odoc.pDeleteReason = DirectCast(rItems.FindControl("tbReason"), TextBox).Text
                        odoc.DeleteDoc(objcommand)
                        lsMsg = "deleted"
                        'ElseIf lRestore.Visible Then
                        '    odoc.pDocId = DirectCast(rItems.FindControl("lDocId"), Literal).Text
                        '    odoc.pModifiedDate = lsDate
                        '    odoc.pUserId = DocSession.sUserId
                        '    odoc.RestoreDoc(objcommand)
                        '    lsMsg = "restored"
                        'ElseIf lPurge.Visible Then
                        '    odoc.pDocId = DirectCast(rItems.FindControl("lDocId"), Literal).Text
                        '    odoc.PurgeDoc(objcommand)
                        '    lsMsg = "purged"
                    End If
                End If
            Next
            ltr.pTran.Commit()
            Master.ShowMessage("Document has been " & lsMsg & " successfully.")
            RetrieveRecords()
            'Repeater1.Visible = False
            'ucPager.Visible = False
            'pPager.Update()
            pnlRepeater.Update()
        Catch ex As Exception
            Master.ShowMessage("Error occurred while processing your request (" & ex.Message & "). Please try again.")
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally
            If Not objcommand Is Nothing Then
                objcommand.Dispose()
                objcommand = Nothing

            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
        End Try
    End Sub
    
    Private Sub ucCon_e_ok_click() Handles ucCon.e_ok_click
        ProcessDoc()
    End Sub
    
    Public Sub viewDoc(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ri As RepeaterItem
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        'Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        'Dim lDocTypeAccess As Literal = DirectCast(ri.FindControl("lDocTypeAccess"), Literal)
        Dim lDocId As Literal = DirectCast(ri.FindControl("lDocId"), Literal)
        DocSession.sDocID = lDocId.Text
        'DocSession.sDocType = lDocType.Text
        'DocSession.sDocTypeAccess = lDocTypeAccess.Text
        Response.Redirect("view.aspx")
    End Sub
    Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        Dim DocID As String = DirectCast(rptitem.FindControl("lDocId"), Literal).Text
        Dim FVersion As String = DirectCast(rptitem.FindControl("lFileVersion"), Literal).Text
        Dim FileName As String = DirectCast(rptitem.FindControl("lFileName"), Literal).Text
        Dim lRef As String = DirectCast(rptitem.FindControl("lRef"), Literal).Text

        '--new folder
        Dim CreatedDate As String = DirectCast(rptitem.FindControl("lCDate"), Literal).Text
        Dim sYear, sMonth As String
        sYear = Year(CDate(CreatedDate)).ToString
        sMonth = MonthName(Month(CDate(CreatedDate))).ToString
        DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lRef & "\" & DocID & "_" & FVersion & "_" & FileName
        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) AndAlso Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            DocSession.sCurrentFile = DocID & "_" & FVersion & "_" & FileName
        End If


        DisplayDoc()
    End Sub

    'Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
    '    Dim DocID As String = DirectCast(rptitem.FindControl("lvDocId"), Literal).Text
    '    Dim FVersion As String = DirectCast(rptitem.FindControl("lVersion"), Literal).Text
    '    Dim FileName As String = DirectCast(rptitem.FindControl("lFileName"), Label).Text
    '    '--new folder
    '    Dim sYear, sMonth As String
    '    sYear = Year(CDate(pCreatedDate)).ToString
    '    sMonth = MonthName(Month(CDate(pCreatedDate))).ToString
    '    DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\" & DocID & "_" & FVersion & "_" & FileName
    '    If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
    '        DocSession.sCurrentFile = DocID & "_" & FVersion & "_" & FileName
    '    End If

    '    DocSession.sFileName = FileName
    '    DisplayDoc()
    'End Sub

    Private Sub DisplayDoc()

        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim lsFilePath As String = lsLoc
        ucViewer.Visible = False
        ucPDFViewer.Visible = False
        ucDocViewer.Visible = False
        If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(lsFilePath & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
                'docvw.Visible = False
                'pnlImg.Visible = True
                ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
                'pnlImageDisp.Visible = True
                ucViewer.ViewImg()
                ucViewer.Visible = True
                'docvw.Attributes("src") = "viewDoc.aspx"
                'docvw.Visible = False

                'pnlDocView.Update()
                'pDocView.Update()

            ElseIf lsext = ".pdf" Then

                'docvw.Attributes("src") = "119_1_blank5.pdf"
                ucPDFViewer.ViewPDF()
                ucPDFViewer.Visible = True


                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                'pnlImg.Visible = False
                'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                ucDocViewer.ViewDoc()
                ucDocViewer.Visible = True

                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                ' pnlImg.Visible = False
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('" & "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc & "', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            End If
        ElseIf System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(lsFilePath & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
                'docvw.Visible = False
                'pnlImg.Visible = True
                ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
                'pnlImageDisp.Visible = True
                ucViewer.ViewImg()
                ucViewer.Visible = True
                'docvw.Attributes("src") = "viewDoc.aspx"
                'docvw.Visible = False

                'pnlDocView.Update()
                'pDocView.Update()

            ElseIf lsext = ".pdf" Then

                'docvw.Attributes("src") = "119_1_blank5.pdf"
                ucPDFViewer.ViewPDF()
                ucPDFViewer.Visible = True


                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                'pnlImg.Visible = False
                'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                ucDocViewer.ViewDoc()
                ucDocViewer.Visible = True

                'pnlDocView.Update()
                'pnlImageDisp.Visible = False
                ' pnlImg.Visible = False
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('" & "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc & "', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            End If
        Else
            Master.ShowMessage("File associated with this document does not exists on the server. Please contact administrator.")
        End If
    End Sub

    Private Sub imgHelp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHelp.Click
        'If lDelete.Visible Then
        '    ArchiveInstruction.Visible = Not ArchiveInstruction.Visible
        '    RestoreInstruction.Visible = False
        '    PurgeInstruction.Visible = False
        '    'ElseIf lRestore.Visible Then
        '    '    ArchiveInstruction.Visible = False
        '    '    RestoreInstruction.Visible = Not RestoreInstruction.Visible
        '    '    PurgeInstruction.Visible = False
        '    'Else
        '    '    ArchiveInstruction.Visible = False
        '    '    RestoreInstruction.Visible = False
        '    '    PurgeInstruction.Visible = Not PurgeInstruction.Visible
        'End If


        'pnlPurge.Update()
    End Sub

    'Private Sub dlDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocType.SelectedIndexChanged

    '    'Dim oDoc As New DocIndex
    '    ucDocIndex.pDIS = False
    '    ucDocIndex.RetrieveDocIndex(dlDocType.SelectedValue)
    '    'Dim lodata As DataTable = oDoc.RetrieveDocTypeIndex()
    '    'If lodata.Rows.Count > 0 Then


    '    '    rptIndex.Visible = True
    '    '    rptIndex.DataSource = lodata
    '    '    rptIndex.DataBind()


    '    'Else
    '    '    rptIndex.Visible = False
    '    'End If
    '    plIndex.Update()
    'End Sub

    'Private Sub lbSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSearch.Click
    '    idAdvSrch.Visible = Not idAdvSrch.Visible
    '    pRestoreCriteria.Visible = Not pRestoreCriteria.Visible
    '    btRetrieve.Visible = Not btRetrieve.Visible
    '    pnlBk.Update()
    'End Sub
    Private Sub GetUsers()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oUser As DocUser
        Try
            oUser = New DocUser

            ldata = oUser.UserList

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "-All-"
            ldata.Rows.InsertAt(lrow, 0)
            dlUser.DataSource = ldata
            dlUser.DataValueField = "UserId"
            dlUser.DataTextField = "UserName"
            dlUser.DataBind()
            'Try
            '    'dlUser.SelectedValue = DocSession.sUserId
            'Catch ex As Exception

            'End Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub
End Class