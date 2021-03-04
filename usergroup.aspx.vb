Imports System
Imports System.Data.SqlClient
Public Class usergroup
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Account")
        UserControlAdminMenuH1.SelectTab("Group")
        AddHandler ucAdd.e_click, AddressOf AddGroup
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveGroups()
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
    Private Sub imgUser_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUser.Click
        pFilter.Visible = Not pFilter.Visible
        If InStr(imgUser.ImageUrl, "show") > 0 Then
            imgUser.ImageUrl = "images/hidepanel.png"
        Else
            imgUser.ImageUrl = "images/showpanel.png"
        End If
        pnlFilter.Update()
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        
        If Not IsPostBack Then

            'RetrieveGroups()
            Repeater3.DataSource = RetrieveDocType()
            Repeater3.DataBind()
            RetrieveExistingGroup()
            RetrieveGroups()
            ShowResult()
        End If
    End Sub
    Private Sub RetrieveExistingGroup()
        Dim oGrp As DocGroup
        Dim ldata As DataTable
        Dim lrow As DataRow
        Try
            oGrp = New DocGroup
            ldata = oGrp.RetrieveGroups
            lrow = ldata.NewRow
            lrow(0) = ""
            ldata.Rows.InsertAt(lrow, 0)
            ddlGroup.DataSource = ldata
            ddlGroup.DataTextField = "groupname"
            ddlGroup.DataValueField = "groupid"
            ddlGroup.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Private Function RetrieveDocType() As DataTable
        'Dim objCommand As clsSqlConn
        'Dim ldata As DataTable

        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCTYPE"
            'objCommand.ParametersAddWithValue("@groupId", DocSession.sUserGroup)

            'ldata = objCommand.Fill

            ''lrow(0) = ""
            ''ldata.Rows.InsertAt(lrow, 0)
            'Return ldata
            Dim oDoc As New DocTypes
            oDoc.pGroupId = DocSession.sUserGroup
            Return oDoc.GetDocType()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function

    Private Function RetrieveAllDocTypes() As DataTable
        'Dim objCommand As clsSqlConn
        'Dim ldata As DataTable

        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCTYPE"
            'objCommand.ParametersAddWithValue("@groupId", DocSession.sUserGroup)

            'ldata = objCommand.Fill

            ''lrow(0) = ""
            ''ldata.Rows.InsertAt(lrow, 0)
            'Return ldata
            Dim oDoc As New DocTypes
            oDoc.pGroupId = DocSession.sUserGroup
            Return oDoc.GetAllDocTypes()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function


    Private Sub RetrieveGroups()
        'Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_GROUPGET_NEW"

            'If GroupCode.Text.Trim <> "" Then
            '    objCommand.ParametersAddWithValue("@GroupId", GroupCode.Text.Trim)
            'End If

            'If tbDesc.Text.Trim <> "" Then
            '    objCommand.ParametersAddWithValue("@Desc", tbDesc.Text.Trim)
            'End If

            Dim oDoc As New DocGroup
            oDoc.pGroupID = GroupCode.Text.Trim
            oDoc.pDesc = tbDesc.Text.Trim
            oDoc.pIdx = hfCurrent.Value
            oDoc.pRowsPerPage = DocSession.RowsPerPage
            oDoc.pSortCol = hfSortCol.Value
            oDoc.pSortOrder = hfSortOrder.Value
            ldata = oDoc.RetrieveGroupNew

            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = oDoc.CountGroups

                hfTotalRows.Value = lstotalrows 'oDocs.pRetVal
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()


                Repeater1.DataSource = ldata
                Repeater1.DataBind()
            Else
                Master.ShowMessage("No records found for the selected filter.")
                Repeater1.DataSource = ldata
                Repeater1.DataBind()
                pPager.Update()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If

        End Try

    End Sub



    'Protected Sub imgAddGroup_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddGroup.Click
    '    tbGroupCode.ReadOnly = False
    '    tbGroupCode.Text = ""
    '    tbGroupName.Text = ""

    '    lAction.Text = "Add"
    '    ShowAdd()


    'End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        RetrieveGroups()
        ShowResult()

    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        If tbGroupCode.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Group Code is a required field."

        ElseIf tbGroupName.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Group Name is a required field."
            'ElseIf tbOfficeCode.Text.Trim = "" Then
        ElseIf dlOfficeCode.SelectedValue.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Office Code is a required field."
        ElseIf ddlMainGroup.SelectedValue.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Main Group is a required field."
        Else
            If lAction.Text.ToLower = "add" Then
                If GroupDoesNotExist() Then
                    SaveGroup()


                End If
            Else
                UpdateGroup()
            End If
        End If
        pnlMsg.Update()
    End Sub

    Private Function GroupDoesNotExist() As Boolean

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPVALIDATE"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT g.groupid, g.groupname FROM groups g WHERE g.groupid = '" & tbGroupCode.Text & "'"
            'objCommand.ParametersAddWithValue("@groupid", tbGroupCode.Text)

            If objCommand.ExecHasRow Then
                msg.CssClass = "msg_red"
                msg.Text = "Group Code " & tbGroupCode.Text & " already exist. Please try another Group Code."
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try
    End Function


    Private Sub SaveGroup()

        Dim ec As crypt
        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As New DbTran
        Dim loDocGroup As DocGroup
        Dim lbAdd As Boolean
        Try
            loDocGroup = New DocGroup
            If uploadPic.HasFile Then
                Dim linfo As New System.IO.FileInfo(uploadPic.PostedFile.FileName)

                Dim lsFN As String = tbGroupCode.Text & linfo.Extension
                If lsFN.ToLower = "dbm.png" Then
                    lsFN = "dbm1.png"
                End If
                Dim lsPic As String = Server.MapPath("") & "\images\logo\" & lsFN

                tbGroupLogo.Text = lsFN

                uploadPic.SaveAs(lsPic)
                imgPic.ImageUrl = "images/logo/" & lsFN

                
                pnlpic.Update()
            Else
                imgPic.ImageUrl = "images/logo/" & tbGroupLogo.Text
            End If
            ec = New crypt

            lbAdd = False
            objCommand = New clsSqlConn(ltr.pTran)

            objCommand.CommandType = CommandType.Text
            loDocGroup.pGroupID = tbGroupCode.Text
            loDocGroup.pDesc = tbGroupName.Text
            loDocGroup.pTrackingColor = tbgColor.Text
            loDocGroup.pTextColor = tColor.Text
            loDocGroup.pOfficeCode = dlOfficeCode.SelectedValue 'tbOfficeCode.Text
            loDocGroup.pMainGroupId = ddlMainGroup.SelectedValue 'tbOfficeCode.Text
            loDocGroup.pUserId = DocSession.sUserId
            loDocGroup.pIPAddress = Request.UserHostAddress
            loDocGroup.pReportAccess = IIf(cbReportAccess.Checked, 1, 0)
            loDocGroup.pEditIndex = IIf(cbEditIndex.Checked, 1, 0)
            loDocGroup.pVersionControl = IIf(cbVersionControl.Checked, 1, 0)
            loDocGroup.pCanDownLoad = IIf(cbDownload.Checked, 1, 0)
            loDocGroup.pCanPrint = IIf(cbPrint.Checked, 1, 0)
            'loDocGroup.pComplete = IIf(cbComplete.Checked, 1, 0)
            'loDocGroup.pImportDoc = IIf(cbImport.Checked, 1, 0)
            loDocGroup.pGroupLogo = tbGroupLogo.Text
            loDocGroup.pRRTitle = tbRRTitle.Text
            'loDocGroup.pDeleteDoc = IIf(cbDelete.Checked, 1, 0)
            loDocGroup.AddGroup(objCommand)


            If ddlGroup.SelectedValue <> "" Then
                CopyAccess(objCommand)
            Else
                AddAccess(objCommand)
            End If



            Dim locls As New cls_storedproc
            locls.ptableName = "Group"
            locls.pRecordId = tbGroupCode.Text
            locls.pModifiedBy = DocSession.sUserId
            locls.pColumnName = "Added"
            locls.pOldValue = "Group ID"
            locls.pNewValue = tbGroupCode.Text
            locls.pIPAddress = Request.UserHostAddress

            ltr.pTran.Commit()
            Dim lsGrp As String
            If (tbGroupName.Text.Trim.Length > 30) Then
                lsGrp = tbGroupName.Text.Trim.Substring(0, 25) & "..."
            Else
                lsGrp = tbGroupName.Text.Trim
            End If

            If lAction.Text.ToLower = "add" Then
                msg.CssClass = "msg_green"
                msg.Text = "** Group Name """ & lsGrp & """ has been created successfully."

            Else
                msg.CssClass = "msg_green"
                msg.Text = "** Group Name """ & lsGrp & """ has been updated successfully."
            End If


            tbGroupCode.Text = ""
            RetrieveGroups()
            pnlRepeater.Update()
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            msg.CssClass = "msg_red"
            msg.Text = "** Error while saving (" & ex.Message & "). Please try again"

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
    Private Sub AddAccess(ByVal objCommand As clsSqlConn)
        Dim loDocGroup As New DocGroup
        Try


            For Each ri In Repeater3.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'If DirectCast(ri.FindControl("cbxSelect"), CheckBox).Checked Then
                    'If DirectCast(ri.FindControl("ImgSelected"), ImageButton).Visible Then
                    If DirectCast(ri.FindControl("cbSelect"), CheckBox).Checked Then



                        loDocGroup.pGroupID = tbGroupCode.Text


                        loDocGroup.pDocType = DirectCast(ri.FindControl("DocType"), Literal).Text

                        loDocGroup.pUserId = DocSession.sUserId


                        loDocGroup.pDocTypeDesc = DirectCast(ri.FindControl("DocName"), Literal).Text
                        loDocGroup.pIPAddress = Request.UserHostAddress
                        loDocGroup.AddGroupDocTypeAccess(objCommand)


                        'objCommand.ExecTranNonQuery()
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    
    Private Sub CopyAccess(ByVal objCommand As clsSqlConn)
        Dim loDocGroup As DocGroup
        Try
            loDocGroup = New DocGroup
            loDocGroup.pGroupID = ddlGroup.SelectedValue
            loDocGroup.CopyGroupDocTypeAccess(objCommand, tbGroupCode.Text)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub UpdateGroup()

        Dim ec As New crypt
        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As New DbTran

        Dim loDocGroup As DocGroup
        Dim lbAdd As Boolean
        Try
            If uploadPic.HasFile Then
                Dim linfo As New System.IO.FileInfo(uploadPic.PostedFile.FileName)

                Dim lsFN As String = tbGroupCode.Text & linfo.Extension
                If lsFN.ToLower = "dbm.png" Then
                    lsFN = "dbm1.png"
                End If
                Dim lsPic As String = Server.MapPath("") & "\images\logo\" & lsFN

                tbGroupLogo.Text = lsFN

                uploadPic.SaveAs(lsPic)
                imgPic.ImageUrl = "images/logo/" & lsFN


                pnlpic.Update()
            Else
                imgPic.ImageUrl = "images/logo/" & tbGroupLogo.Text
            End If
            loDocGroup = New DocGroup

            lbAdd = False
            lAction.Text = "update"
            objCommand = New clsSqlConn(ltr.pTran)

            loDocGroup.pGroupID = tbGroupCode.Text
            loDocGroup.pDesc = tbGroupName.Text
            loDocGroup.pDesc_o = lGroupNameOrig.Text
            loDocGroup.pTextColor = tColor.Text
            loDocGroup.pTrackingColor = tbgColor.Text
            loDocGroup.pUserId = DocSession.sUserId
            loDocGroup.pReportAccess = IIf(cbReportAccess.Checked, "1", "0")
            loDocGroup.pReportAccess_o = lRepAccess.Text.Trim
            loDocGroup.pIPAddress = Request.UserHostAddress
            loDocGroup.pOfficeCode = dlOfficeCode.SelectedValue 'tbOfficeCode.Text
            loDocGroup.pMainGroupId = ddlMainGroup.SelectedValue 'tbOfficeCode.Text
            loDocGroup.pEditIndex = IIf(cbEditIndex.Checked, "1", "0")
            loDocGroup.pCanPrint = IIf(cbPrint.Checked, "1", "0")
            loDocGroup.pCanDownLoad = IIf(cbDownload.Checked, "1", "0")
            loDocGroup.pVersionControl = IIf(cbVersionControl.Checked, "1", "0")
            'loDocGroup.pImportDoc = IIf(cbImport.Checked, "1", "0")
            loDocGroup.pGroupLogo = tbGroupLogo.Text
            loDocGroup.pRRTitle = tbRRTitle.Text
            loDocGroup.UpdateGroup(objCommand)
            loDocGroup.DeleteGroupDocTypeAccess(objCommand)
            If ddlGroup.SelectedValue <> "" Then
                CopyAccess(objCommand)
            Else
                AddAccess(objCommand)
            End If

            ltr.pTran.Commit()
            Dim lsGrp As String
            If (tbGroupName.Text.Trim.Length > 30) Then
                lsGrp = tbGroupName.Text.Trim.Substring(0, 25) & "..."
            Else
                lsGrp = tbGroupName.Text.Trim
            End If
            If lAction.Text.ToLower = "add" Then
                msg.CssClass = "msg_green"
                msg.Text = "** Group Name """ & lsGrp & """ has been created successfully."
                tbGroupCode.Text = ""
            Else
                msg.CssClass = "msg_green"
                msg.Text = "** Group Name """ & lsGrp & """ has been updated successfully."
            End If

            RetrieveGroups()
            pnlRepeater.Update()
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            msg.CssClass = "msg_red"
            msg.Text = "** Error while saving (" & ex.Message & "). Please try again"

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

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        Dim imgDel As ImageButton
        'If e.Item.ItemType = ListItemType.Header Then
        '    imgDel = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
        '    AddHandler imgDel.Click, AddressOf DeleteGroup

        'End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            imgDel = DirectCast(e.Item.FindControl("imgUpdate"), ImageButton)
            AddHandler imgDel.Click, AddressOf UpdateGroup

        End If
    End Sub
    Private Sub UpdateGroup(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgB As ImageButton = DirectCast(sender, ImageButton)
        Dim ri As RepeaterItem = DirectCast(imgB.NamingContainer, RepeaterItem)
        Dim ltl As Literal = DirectCast(ri.FindControl("GroupCode"), Literal)
        Dim ltldn As Literal = DirectCast(ri.FindControl("GroupName"), Literal)
        Dim lOfc As Literal = DirectCast(ri.FindControl("OfficeCode"), Literal)
        Dim lMainGroupId As Literal = DirectCast(ri.FindControl("MainGroupId"), Literal)
        Dim oGroup As New DocGroup
        lAction.Text = "Update"
        tColor.Text = DirectCast(ri.FindControl("lColor"), Literal).Text
        tbgColor.Text = DirectCast(ri.FindControl("lTColor"), Literal).Text
        tbGroupLogo.Text = DirectCast(ri.FindControl("lGroupLogo"), Literal).Text
        tbRRTitle.Text = DirectCast(ri.FindControl("lReceiptReplyTitle"), Literal).Text
        oGroup.pGroupID = Server.HtmlDecode(ltl.Text)
        tbGroupCode.Text = Server.HtmlDecode(ltl.Text)
        tbGroupCode.CssClass = "entryflddisabled"
        tbGroupName.Text = Server.HtmlDecode(ltldn.Text)
        lGroupNameOrig.Text = Server.HtmlDecode(ltldn.Text)
        'tbOfficeCode.Text = Server.HtmlDecode(lOfc.Text)
        ddlGroup.SelectedValue = ""
        Try
            dlOfficeCode.SelectedValue = Server.HtmlDecode(lOfc.Text)
        Catch ex As Exception

        End Try

        Try
            ddlMainGroup.SelectedValue = Server.HtmlDecode(lMainGroupId.Text)
        Catch ex As Exception

        End Try

        tbGroupCode.ReadOnly = True
        If DirectCast(ri.FindControl("lReportAccess"), Label).Text = "Yes" Then
            cbReportAccess.Checked = True
            lRepAccess.Text = "1"
        Else
            cbReportAccess.Checked = False
            lRepAccess.Text = "0"
        End If

        If DirectCast(ri.FindControl("lEditIndexDoc"), Label).Text = "Yes" Then
            cbEditIndex.Checked = True
            lEditIndexAccess.Text = "1"
        Else
            cbEditIndex.Checked = False
            lEditIndexAccess.Text = "0"
        End If
        If DirectCast(ri.FindControl("lVersionControl"), Label).Text = "Yes" Then
            cbVersionControl.Checked = True
            lVersionControl.Text = "1"
        Else
            cbVersionControl.Checked = False
            lVersionControl.Text = "0"
        End If
        If DirectCast(ri.FindControl("lCanPrint"), Label).Text = "Yes" Then
            cbPrint.Checked = True
            lPrint.Text = "1"
        Else
            cbPrint.Checked = False
            lPrint.Text = "0"
        End If
        If DirectCast(ri.FindControl("lCanDownload"), Label).Text = "Yes" Then
            cbDownload.Checked = True
            lDownload.Text = "1"
        Else
            cbDownload.Checked = False
            lDownload.Text = "0"
        End If
        'If DirectCast(ri.FindControl("lCompleteDoc"), Label).Text = "Yes" Then
        '    cbComplete.Checked = True
        '    lCompleteAccess.Text = "1"
        'Else
        '    cbComplete.Checked = False
        '    lCompleteAccess.Text = "0"
        'End If

        'If DirectCast(ri.FindControl("lImportDoc"), Label).Text = "Yes" Then
        '    cbImport.Checked = True
        '    lImportAccess.Text = "1"
        'Else
        '    cbImport.Checked = False
        '    lImportAccess.Text = "0"
        'End If

        'If DirectCast(ri.FindControl("lDeleteDoc"), Label).Text = "Yes" Then
        '    cbDelete.Checked = True
        '    lDeleteAccess.Text = "1"
        'Else
        '    cbDelete.Checked = False
        '    lDeleteAccess.Text = "0"
        'End If
        Dim sLogo As String = "\images\logo\" & DirectCast(ri.FindControl("lGroupLogo"), Literal).Text
        If System.IO.File.Exists(Server.MapPath("") & sLogo) Then
            imgPic.ImageUrl = sLogo
        Else
            imgPic.ImageUrl = "images/logo/default.png"
        End If

        Dim ldata As DataTable
        Try


            ldata = oGroup.RetrieveGroupDocTypeAccess()

            If ldata.Rows.Count = 0 Then
                ldata = RetrieveDocType()
            End If

            msg.Text = ""
            If lAction.Text.ToLower = "add" Then
                ddlGroup.Visible = False
            Else
                ddlGroup.Visible = True
            End If
            Repeater3.DataSource = ldata
            Repeater3.DataBind()
            Master.ShowImageDocument = True
            pAddGroup.Visible = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try
    End Sub

    Private Sub DeleteGroup()
        Dim ri As RepeaterItem
        Dim cbox As CheckBox

        Dim oDoc As New DocGroup
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblGroup")
            loData.Columns.Add("GroupCode", Type.GetType("System.String"))
            loData.Columns.Add("GroupName", Type.GetType("System.String"))
            loData.Columns.Add("ReportAccess", Type.GetType("System.String"))
            loData.Columns.Add("WorkSchedules", Type.GetType("System.String"))
            loData.Columns.Add("Comment", Type.GetType("System.String"))
            'loData.Columns.Add("Access", Type.GetType("System.String"))


            For Each ri In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbxDelete"), CheckBox)
                    If cbox.Checked Then
                        loRow = loData.NewRow()
                        loRow("GRoupCode") = DirectCast(ri.FindControl("GroupCode"), Literal).Text
                        loRow("GroupName") = DirectCast(ri.FindControl("GroupName"), Literal).Text
                        loRow("ReportAccess") = DirectCast(ri.FindControl("lReportAccess"), Label).Text
                        loRow("WorkSchedules") = DirectCast(ri.FindControl("lWorkSched"), Literal).Text
                        loRow("Comment") = IIf(oDoc.CheckIfGroupCodeExists(loRow("GroupCode")), "Cannot delete record being used.", "OK to delete.")
                        loData.Rows.Add(loRow)
                    End If
                End If
            Next

            If loData.Rows.Count > 0 Then
                Repeater2.DataSource = loData
                Repeater2.DataBind()
                ShowDelete()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim lblGroupName As Literal
        ', lblActive As Literal

        Dim tGRoupName As TextBox
        'Dim cActive As CheckBox



        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'If cbUpdate.Checked Then
            '    lblGroupName = DirectCast(e.Item.FindControl("GroupName"), Literal)
            '    lblGroupName.Visible = False
            '    tGRoupName = DirectCast(e.Item.FindControl("tbGroupName"), TextBox)
            '    tGRoupName.Visible = True
            'End If

            If DirectCast(e.Item.FindControl("lAllowed"), Literal).Text.Trim = "True" Then
                DirectCast(e.Item.FindControl("ucWorkSched"), UserControlWorkSched).AlwaysAllowed = True
                'DirectCast(e.Item.FindControl("ucWorkSched"), UserControlWorkSched).GroupId = DirectCast(e.Item.FindControl("GroupCode"), Literal).Text
            End If
            DirectCast(e.Item.FindControl("ucWorkSched"), UserControlWorkSched).GroupId = DirectCast(e.Item.FindControl("GroupCode"), Literal).Text
            DirectCast(e.Item.FindControl("ucDocAccess"), UserControlGroupDocTypeAccess).GroupId = DirectCast(e.Item.FindControl("GroupCode"), Literal).Text

        End If
    End Sub

    Protected Sub btDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btDelete.Click
        Dim ri As RepeaterItem

        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As New DbTran
        'Dim lsDT As String, lsAccess As String

        Try

            objCommand = New clsSqlConn(ltr.pTran)

            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "XMSP_GROUPDELETE"
            Dim odg As New DocGroup

            For Each ri In Repeater2.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    If DirectCast(ri.FindControl("lComment"), Literal).Text = "OK to delete." Then

                        odg.pGroupID = DirectCast(ri.FindControl("GroupCode"), Literal).Text
                        odg.pDesc = DirectCast(ri.FindControl("GroupName"), Literal).Text
                        'lsDT = DirectCast(ri.FindControl("DocType"), Literal).Text
                        'lsAccess = DirectCast(ri.FindControl("lAccess"), Literal).Text
                        'If lsDT.Trim <> "" Then
                        'objCommand.ParametersAddWithValue("@DocType", lsDT)
                        'If
                        odg.pIPAddress = Request.UserHostAddress
                        odg.pUserId = DocSession.sUserId
                        odg.DeleteGroup(objCommand)
                        'objCommand.ParametersAddWithValue("@GroupAccessDesc", lsAccess)
                        'objCommand.ExecTranNonQuery()
                        'objCommand.ParametersClear()
                    End If
                End If
            Next
            ltr.pTran.Commit()
            RetrieveGroups()
            ShowResult()

        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Throw New Exception(ex.Message)

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
    Private Sub ShowCriteria()
        'pSearchCriteria.Visible = Not pSearchCriteria.Visible
        'pnlSearchCriteria.Update()
        Repeater1.Visible = Not Repeater1.Visible
        pnlRepeater.Update()

        pAddGroup.Visible = False
        pnlAddGroup.Update()
        pDeleteGroup.Visible = Not pDeleteGroup.Visible
        pnlDeleteGroup.Update()
    End Sub

    'Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
    '    pSearchCriteria.Visible = Not pSearchCriteria.Visible
    '    pnlSearchCriteria.Update()
    'End Sub

    'Protected Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
    '    Dim ri As RepeaterItem
    '    Dim objCommand As clsSqlConn
    '    'Dim ltr As SqlTransaction
    '    Dim ltr As New DbTran
    '    Try
    '        objCommand = New clsSqlConn(ltr.pTran)
    '        objCommand.CommandType = CommandType.StoredProcedure

    '        objCommand.CommandText = "XMSP_GROUPUPDATE"
    '        For Each ri In Repeater1.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
    '                objCommand.ParametersAddWithValue("@GroupId", DirectCast(ri.FindControl("GroupCode"), Literal).Text)
    '                objCommand.ParametersAddWithValue("@GroupName", DirectCast(ri.FindControl("tbDesc"), TextBox).Text)
    '                objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
    '                objCommand.ParametersAddWithValue("@UserID", DocSession.sUserId)

    '                objCommand.ExecTranNonQuery()
    '                objCommand.ParametersClear()
    '            End If
    '        Next
    '        ltr.pTran.Commit()
    '        RetrieveGroups()
    '        'ShowCriteria()

    '    Catch ex As Exception
    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)

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

    Private Sub ShowResult()




        ' pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()


        pAddGroup.Visible = False
        pnlAddGroup.Update()

        pDeleteGroup.Visible = False
        pnlDeleteGroup.Update()

        pRepeater.Visible = True
        pnlRepeater.Update()

        'imgAddGroup.Visible = True
        Master.ShowImageDocument = False
        'pnlTab.Update()
    End Sub

    Private Sub ShowDeletes()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        pRepeater.Visible = False
        pnlRepeater.Update()

        pAddGroup.Visible = False
        pnlAddGroup.Update()

        pDeleteGroup.Visible = True
        pnlDeleteGroup.Update()

    End Sub

    Private Sub ShowDelete()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pAddGroup.Visible = False
        'pnlAddGroup.Update()

        pDeleteGroup.Visible = True
        pnlDeleteGroup.Update()
        Master.ShowImageDocument = True

    End Sub

    Private Sub ShowAdd()
        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pDeleteGroup.Visible = False
        'pnlDeleteGroup.Update()

        msg.Text = ""
        GetOfficeCode()
        GetMainGroup()
        Repeater3.DataSource = RetrieveAllDocTypes()
        Repeater3.DataBind()
        pAddGroup.Visible = Not pAddGroup.Visible
        pnlAddGroup.Update()
        Master.ShowImageDocument = True
        'imgAddGroup.Visible = False
        'pnlTab.Update()

    End Sub

    'Private Sub ShowSearch()

    '    pSearchCriteria.Visible = Not pSearchCriteria.Visible
    '    pnlSearchCriteria.Update()

    'End Sub


    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ShowResult()
    End Sub

    Private Sub Repeater3_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater3.ItemCreated
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            'Dim img As ImageButton = DirectCast(e.Item.FindControl("imgSelect"), ImageButton)
            'AddHandler img.Click, AddressOf ShowGroupAccess
            'Dim imgS As ImageButton = DirectCast(e.Item.FindControl("imgSelected"), ImageButton)
            'AddHandler imgS.Click, AddressOf HideGroupAccess

        End If

    End Sub

    Private Sub ShowGroupAccess(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim oAccess As New DocGroupAccess
        Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelected"), ImageButton)
        'Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        'Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        'ImgNA.Visible = True
        'ImgA.Visible = False
        ImgS.Visible = False
        Img.Visible = True
        Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        If rbList.SelectedValue = "" Then
            rbList.SelectedValue = "0"
            rbList.DataSource = oAccess.RetrieveGroupAccess()
            rbList.DataTextField = "GroupAccess"
            rbList.DataValueField = "GroupAccessId"
        End If
        rbList.DataBind()
        rbList.Visible = True
        Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)

        updPnl.Visible = True
        updPnl.Update()
        'pnlGAccess.Visible = True
        'pnlGAccess.Update
        'Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        'updPnlCbox.Update()

    End Sub
    Private Sub HideGroupAccess(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim oAccess As New DocGroupAccess
        Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelect"), ImageButton)
        'Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        'Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        'ImgNA.Visible = False
        'ImgA.Visible = False

        ImgS.Visible = False
        Img.Visible = True

        Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)
        updPnl.Visible = False
        rbList.Visible = False
        updPnl.Update()

        'Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        'updPnlCbox.Update()

    End Sub

    Public Sub fSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgVersionAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgVersionNotAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fSelect3(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgDownload"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect3(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotDownload"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fSelect4(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgReceipt"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect4(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotReceipt"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fSelectDocType(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbBool As Boolean = True
        Dim oAccess As New DocGroupAccess
        Dim iCtr As Integer = 1
        For Each rptItems As RepeaterItem In Repeater3.Items
            

            
            DirectCast(rptItems.FindControl("cbSelect"), CheckBox).Checked = Not DirectCast(rptItems.FindControl("cbSelect"), CheckBox).Checked


           


        Next
        pnlGAccess.Update()
    End Sub

    Public Sub fSelectPrinting(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbBool As Boolean = True
        Dim oAccess As New DocGroupAccess
        Dim iCtr As Integer = 1
        For Each rptItems As RepeaterItem In Repeater3.Items
            DirectCast(rptItems.FindControl("ImgNotAllow"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgNotAllow"), ImageButton).Visible
            DirectCast(rptItems.FindControl("ImgAllow"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgAllow"), ImageButton).Visible
            Dim updPnl As UpdatePanel = DirectCast(rptItems.FindControl("pnlGAccess"), UpdatePanel)

            updPnl.Update()



        Next
    End Sub
    Public Sub fSelectVersion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbBool As Boolean = True
        Dim oAccess As New DocGroupAccess
        Dim iCtr As Integer = 1
        For Each rptItems As RepeaterItem In Repeater3.Items
            DirectCast(rptItems.FindControl("ImgVersionNotAllow"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgNotAllow"), ImageButton).Visible
            DirectCast(rptItems.FindControl("ImgVersionAllow"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgAllow"), ImageButton).Visible
            Dim updPnl As UpdatePanel = DirectCast(rptItems.FindControl("pnlGAccess"), UpdatePanel)

            updPnl.Update()



        Next
    End Sub
    Public Sub fSelectDownload(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbBool As Boolean = True
        Dim oAccess As New DocGroupAccess
        Dim iCtr As Integer = 1
        For Each rptItems As RepeaterItem In Repeater3.Items
            DirectCast(rptItems.FindControl("ImgNotDownload"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgNotAllow"), ImageButton).Visible
            DirectCast(rptItems.FindControl("ImgDownload"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgAllow"), ImageButton).Visible
            Dim updPnl As UpdatePanel = DirectCast(rptItems.FindControl("pnlGAccess"), UpdatePanel)

            updPnl.Update()



        Next
    End Sub
    Public Sub fSelectReceipt(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbBool As Boolean = True
        Dim oAccess As New DocGroupAccess
        Dim iCtr As Integer = 1
        For Each rptItems As RepeaterItem In Repeater3.Items
            DirectCast(rptItems.FindControl("ImgNotReceipt"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgNotAllow"), ImageButton).Visible
            DirectCast(rptItems.FindControl("ImgReceipt"), ImageButton).Visible = Not DirectCast(rptItems.FindControl("ImgAllow"), ImageButton).Visible
            Dim updPnl As UpdatePanel = DirectCast(rptItems.FindControl("pnlGAccess"), UpdatePanel)

            updPnl.Update()



        Next
        pnlGAccess.Update()
    End Sub
    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Private Sub Repeater3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater3.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ltlDT As Literal = DirectCast(e.Item.FindControl("GDocType"), Literal)
            If ltlDT.Text.Trim <> "" Then
                DirectCast(e.Item.FindControl("cbSelect"), CheckBox).Checked = True
            End If
            
        End If

    End Sub

    Private Sub imgClose2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose2.Click
        ShowResult()
    End Sub

    Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lComment"), Literal).Text = "OK to delete." Then
                DirectCast(e.Item.FindControl("rw"), HtmlControls.HtmlTableRow).Attributes("class") = "greenHigh"
            End If
        End If
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
        'If imgSort7.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort7
        '    imgSort7.Visible = True
        'Else
        '    imgSort7.Visible = False

        'End If
        Dim oDocGroup As New DocGroup

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                oDocGroup.pSortOrder = "Desc"
                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                oDocGroup.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            oDocGroup.pSortOrder = "Asc"
            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If
        'oDocGroup.pGroupID = ""
        'oDocGroup.pDesc = ""

        oDocGroup.pSortCol = lbSort.Text
        hfSortCol.Value = lbSort.Text
        RetAction()

        'Repeater1.DataSource = oDocGroup.RetrieveGroupNew
        'Repeater1.DataBind()
        'pnlRepeater.Update()


    End Sub

    Private Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
        DeleteGroup()
    End Sub

    Protected Sub imgShowWorkSched(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oImg As ImageButton = DirectCast(sender, ImageButton)

        Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)

        Dim img As ImageButton = DirectCast(oItem.FindControl("imgMinus"), ImageButton)
        img.Visible = Not img.Visible
        Dim img2 As ImageButton = DirectCast(oItem.FindControl("imgPlus"), ImageButton)
        img2.Visible = Not img2.Visible
        Dim userCntrl As UserControlWorkSched
        userCntrl = DirectCast(oItem.FindControl("ucWorkSched"), UserControlWorkSched)
        Dim userDocAccess As UserControlGroupDocTypeAccess
        userDocAccess = DirectCast(oItem.FindControl("ucDocAccess"), UserControlGroupDocTypeAccess)

        Dim wDetails As Panel = DirectCast(oItem.FindControl("WorkDetails"), Panel)
        Dim wDocAccess As Panel = DirectCast(oItem.FindControl("pDocAccess"), Panel)
        Dim pLogo As Panel = DirectCast(oItem.FindControl("pLogo"), Panel)
        Dim GrpId As Literal = DirectCast(oItem.FindControl("GroupCode"), Literal)
        If img.Visible Then

            wDetails.Visible = True
            wDocAccess.Visible = True
            pLogo.Visible = True
            userCntrl.GroupId = GrpId.Text
            userCntrl.RetrieveData()

            userDocAccess.GroupId = GrpId.Text
            userDocAccess.RetrieveDocAccess()

        Else
            wDetails.Visible = False
            wDocAccess.Visible = False
            pLogo.Visible = False
        End If
        'pnlRepeater.Update()
    End Sub

    Private Sub AddGroup()
        tbGroupCode.ReadOnly = False
        tbGroupCode.Text = ""
        tbGroupName.Text = ""
        tbgColor.Text = ""
        tColor.Text = ""
        tbGroupCode.CssClass = "entryfld"
        lAction.Text = "Add"
        ShowAdd()

    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        ShowResult()
    End Sub

    Private Sub GetOfficeCode()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            ldata = oType.RetrieveOffice

            lrow = ldata.NewRow
            lrow(0) = ""
            ldata.Rows.InsertAt(lrow, 0)
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
    Private Sub GetMainGroup()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            ldata = oType.RetrieveMainGroup

            lrow = ldata.NewRow
            lrow(0) = ""
            ldata.Rows.InsertAt(lrow, 0)
            ddlMainGroup.DataSource = ldata
            ddlMainGroup.DataValueField = "MainGroupId"
            ddlMainGroup.DataTextField = "Description"
            ddlMainGroup.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        If Not IsPostBack Then
            GetOfficeCode()
            GetMainGroup()
        End If

    End Sub
End Class