Imports System
Imports System.Data.SqlClient
Public Class office
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Account")
        UserControlAdminMenuH1.SelectTab("Group")
        AddHandler ucAdd.e_click, AddressOf AddReceipt
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()

        RetrieveOffice()
        pnlRepeater.Update()

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

            ''RetrieveGroups()
            'Repeater3.DataSource = RetrieveDocType()
            'Repeater3.DataBind()

            RetrieveOffice()
            ShowResult()
            GetUsers()
            RetrieveAddress()
        End If
    End Sub
    Private Sub GetUsers()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oUser As DocUser
        Try
            oUser = New DocUser

            ldata = oUser.UserList

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = ""
            ldata.Rows.InsertAt(lrow, 0)
            dlUser.DataSource = ldata
            dlUser.DataValueField = "UserId"
            dlUser.DataTextField = "UserName"
            dlUser.DataBind()
            Try
                dlUser.SelectedValue = DocSession.sUserId
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub RetrieveAddress()
        Dim oOfc As DocAddress
        Dim ldata As DataTable
        Dim lrow As DataRow
        Try
            If dlAddress Is Nothing OrElse dlAddress.Items.Count <= 0 Then
                oOfc = New DocAddress
                ldata = oOfc.RetrieveAddress()

                lrow = ldata.NewRow
                lrow("AddressCode") = ""
                lrow("AddrDesc") = ""
                ldata.Rows.InsertAt(lrow, 0)
                dlAddress.DataSource = ldata
                dlAddress.DataTextField = "AddrDesc"
                dlAddress.DataValueField = "AddressCode"
                dlAddress.DataBind()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not oOfc Is Nothing Then
                oOfc = Nothing
            End If
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    Private Sub RetrieveOffice()
        'Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Dim oDoc As DocOffice
        Try

            oDoc = New DocOffice

            'oDoc.pGroupID = GroupCode.Text.Trim
            oDoc.pOfcDesc = tbSearchDesc.Text.Trim
            oDoc.pIdx = hfCurrent.Value
            oDoc.pRowsPerPage = DocSession.RowsPerPage
            oDoc.pSortCol = hfSortCol.Value
            oDoc.pSortOrder = hfSortOrder.Value
            ldata = oDoc.RetrieveOffice()

            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = oDoc.CountOffice

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



    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        RetrieveOffice()
        ShowResult()
        pnlRepeater.Update()
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        If tbDesc.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Office Name is a required field."
        Else
            If lAction.Text.ToLower = "add" Then
                If OfficeDoesNotExist() Then
                    AddOffice()
                End If
            Else
                UpdateDocOffice()
            End If
        End If
        pnlMsg.Update()
    End Sub

    Private Function OfficeDoesNotExist() As Boolean

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_GROUPVALIDATE"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT d.OfficeCode,d.Description FROM DocOffice d WHERE d.Description = '" & tbDesc.Text & "'"
            'objCommand.ParametersAddWithValue("@groupid", tbGroupCode.Text)

            If objCommand.ExecHasRow Then
                msg.CssClass = "msg_red"
                msg.Text = "Office " & tbDesc.Text & " already exist. Please try another Office Name."
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


    Private Sub AddOffice()
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Dim oOfc As DocOffice

        Try
            oOfc = New DocOffice
            oOfc.pOfcCode = tbCode.Text.Trim
            oOfc.pOfcDesc = tbDesc.Text.Trim
            oOfc.pPointPersonId = dlUser.SelectedValue
            oOfc.pAddrCode = dlAddress.SelectedValue
            oOfc.pShowAsDefault = IIf(cbDefault.Checked, "1", "0")
            oOfc.pLandLineNo = tbLandLineNo.Text.Trim
            oOfc.pLocalNo = tbLocalNo.Text.Trim
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            oOfc.AddOffice(objCommand)
            oOfc.DeleteOfficeGroupDocAccess(objCommand)
            AddOfficeAccess(objCommand, tbCode.Text.Trim)
            ltr.pTran.Commit()
            If lAction.Text.ToLower = "add" Then
                msg.CssClass = "msg_green"
                msg.Text = "** Office " & tbDesc.Text.Trim & " has been created successfully."
            Else
                msg.CssClass = "msg_green"
                msg.Text = "** Office " & tbDesc.Text.Trim & " has been updated successfully."
            End If

            tbDesc.Text = ""
            tbCode.Text = ""
            dlAddress.SelectedValue = ""
            dlUser.SelectedValue = ""

            RetrieveOffice()
            pnlRepeater.Update()
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            If Not ltr.pTran Is Nothing Then
                ltr.pTran.Rollback()
            End If
            msg.CssClass = "msg_red"
            msg.Text = "** Error while saving (" & ex.Message & "). Please try again"

        Finally
            If Not ltr.pTran Is Nothing Then
                ltr.pTran.Dispose()
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
            End If
        End Try
    End Sub




    Private Sub UpdateDocOffice()

        'Dim ec As New crypt
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran


        Dim oDoc As DocOffice
        Dim lbAdd As Boolean
        Try

            oDoc = New DocOffice
            lbAdd = False
            lAction.Text = "update"
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            oDoc.pPointPersonId = dlUser.SelectedValue
            oDoc.pAddrCode = dlAddress.SelectedValue
            oDoc.pOfcCode = tbCode.Text
            oDoc.pOfcDesc = tbDesc.Text
            oDoc.pShowAsDefault = IIf(cbDefault.Checked, "1", "0")
            oDoc.pLandLineNo = tbLandLineNo.Text
            oDoc.pLocalNo = tbLocalNo.Text
            oDoc.UpdateOffice(objCommand)
            oDoc.DeleteOfficeGroupDocAccess(objCommand)
            AddOfficeAccess(objCommand, tbCode.Text)

            ltr.pTran.Commit()

            msg.CssClass = "msg_green"
            msg.Text = "** Office " & tbDesc.Text.Trim & " has been updated successfully."

            RetrieveOffice()
            pnlRepeater.Update()
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            If Not ltr.pTran Is Nothing Then
                ltr.pTran.Rollback()
            End If
            msg.CssClass = "msg_red"
            msg.Text = "** Error while saving (" & ex.Message & "). Please try again"

        Finally
            If Not ltr.pTran Is Nothing Then
                ltr.pTran.Dispose()
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
            End If
        End Try
    End Sub

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        Dim imgUpd As ImageButton
        'If e.Item.ItemType = ListItemType.Header Then
        '    imgDel = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
        '    AddHandler imgDel.Click, AddressOf DeleteGroup

        'End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            imgUpd = DirectCast(e.Item.FindControl("imgUpdate"), ImageButton)
            AddHandler imgUpd.Click, AddressOf UpdateRecord

        End If
    End Sub
    Public Sub showprofile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgB As LinkButton = DirectCast(sender, LinkButton)
        Dim ri As RepeaterItem = DirectCast(imgB.NamingContainer, RepeaterItem)

        Master.showProf(DirectCast(ri.FindControl("lPPID"), Literal).Text)

    End Sub

    Private Sub UpdateRecord(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgB As ImageButton = DirectCast(sender, ImageButton)
        Dim ri As RepeaterItem = DirectCast(imgB.NamingContainer, RepeaterItem)

        lAction.Text = "Update"

        tbDesc.Text = DirectCast(ri.FindControl("lDesc"), Literal).Text
        tbCode.Text = DirectCast(ri.FindControl("lOfcCode"), Literal).Text
        tbLandLineNo.Text = DirectCast(ri.FindControl("lLandLineNo"), Literal).Text
        tbLocalNo.Text = DirectCast(ri.FindControl("lLocalNo"), Literal).Text
        Dim li As New WebControls.ListItem
        li = dlUser.Items.FindByValue(DirectCast(ri.FindControl("lPPID"), Literal).Text)
        If Not IsNothing(li) Then
            dlUser.SelectedValue = DirectCast(ri.FindControl("lPPID"), Literal).Text
        End If
        li = dlAddress.Items.FindByValue(DirectCast(ri.FindControl("lAddrCode"), Literal).Text)
        If Not IsNothing(li) Then
            dlAddress.SelectedValue = DirectCast(ri.FindControl("lAddrCode"), Literal).Text
        End If

        cbDefault.Checked = IIf(DirectCast(ri.FindControl("lShow"), Literal).Text = "Yes", True, False)
        msg.Text = ""
        RetrieveAddress()
        'Master.ShowImageDocument = True
        'pAdd.Visible = True
        'pnlAdd.Update()
        Dim oGroup As New DocGroup
        oGroup.pOfficeCode = Server.HtmlDecode(tbCode.Text)
        Using ldata As DataTable = oGroup.RetrieveOfficeGroupByID

            cbGroups.DataSource = ldata
            cbGroups.DataTextField = "groupName"
            cbGroups.DataValueField = "groupId"
            cbGroups.DataBind()

            For i = 0 To cbGroups.Items.Count - 1
                If ldata.Rows(i).Item("OfficeCode") <> "0" Then
                    cbGroups.Items(i).Selected = True
                End If
            Next

            'tbDesc.Text = Server.HtmlDecode(ltldn.Text)
            'ltStatusId.Text = lsi.Text
            'Dim li As New WebControls.ListItem
            'li = dlStatusType.Items.FindByValue(ltype.Text)
            'If Not IsNothing(li) Then
            '    dlStatusType.SelectedValue = ltype.Text
            'End If

            msg.Text = ""

            ShowAdd()
        End Using
    End Sub

    Private Sub DeleteRecord()
        Dim ri As RepeaterItem
        Dim cbox As CheckBox

        Dim oDoc As New DocOffice
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblData")
            loData.Columns.Add("OfficeCode", Type.GetType("System.String"))
            loData.Columns.Add("OfficeName", Type.GetType("System.String"))
            loData.Columns.Add("Comment", Type.GetType("System.String"))

            For Each ri In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbxDelete"), CheckBox)
                    If cbox.Checked Then
                        loRow = loData.NewRow()
                        loRow("OfficeCode") = DirectCast(ri.FindControl("lOfcCode"), Literal).Text
                        loRow("OfficeName") = DirectCast(ri.FindControl("lDesc"), Literal).Text
                        loRow("Comment") = IIf(oDoc.CheckIfOfficeInUse(loRow("OfficeCode")), "Cannot delete record being used.", "OK to delete.")
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
            Dim odg As New DocOffice

            For Each ri In Repeater2.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    If DirectCast(ri.FindControl("lComment"), Literal).Text = "OK to delete." Then

                        odg.pOfcCode = DirectCast(ri.FindControl("lOfcCode"), Literal).Text
                        odg.pOfcDesc = DirectCast(ri.FindControl("lOfcName"), Literal).Text

                        odg.pIPAddress = Request.UserHostAddress
                        odg.pUserId = DocSession.sUserId
                        odg.DeleteRecord(objCommand)

                    End If
                End If
            Next
            ltr.pTran.Commit()
            RetrieveOffice()
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
        'Repeater1.Visible = Not Repeater1.Visible
        'pnlRepeater.Update()

        pAdd.Visible = False
        'pnlAdd.Update()
        pDeleteReceipt.Visible = Not pDeleteReceipt.Visible
        Master.ShowImageDocument = True
        'pnlDeleteReceipt.Update()
    End Sub


    Private Sub ShowResult()

        ' pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()


        'pAdd.Visible = False
        'pnlAdd.Update()

        'pDeleteReceipt.Visible = False
        'pnlDeleteReceipt.Update()

        'pRepeater.Visible = True
        'pnlRepeater.Update()

        'imgAddGroup.Visible = True
        Master.ShowImageDocument = False
        'pnlTab.Update()
    End Sub

    'Private Sub ShowDeletes()

    '    'pSearchCriteria.Visible = False
    '    'pnlSearchCriteria.Update()

    '    pRepeater.Visible = False
    '    pnlRepeater.Update()

    '    pAdd.Visible = False
    '    pnlAdd.Update()

    '    pDeleteReceipt.Visible = True
    '    pnlDeleteReceipt.Update()

    'End Sub

    Private Sub ShowDelete()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pAddGroup.Visible = False
        'pnlAddGroup.Update()

        pDeleteReceipt.Visible = True
        pAdd.Visible = False

        Master.ShowImageDocument = True

    End Sub

    Private Sub ShowAdd()


        msg.Text = ""
        RetrieveAddress()
        pAdd.Visible = Not pAdd.Visible
        pDeleteReceipt.Visible = False
        Dim oGroup As New DocGroup
        Using ldata As DataTable = oGroup.RetrieveGroups
            cbGroups.DataSource = ldata
            cbGroups.DataTextField = "groupname"
            cbGroups.DataValueField = "groupid"
            cbGroups.DataBind()
            'pAddStatus.Visible = Not pAddStatus.Visible
            imgUnSelectAll.Visible = False
            imgSelectAll.Visible = True
            'pnlAddStatus.Update()
           
        End Using
        'pnlAdd.Update()
        Master.ShowImageDocument = True


    End Sub



    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ShowResult()
    End Sub



    Private Sub ShowReceiptDetails(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim oAccess As New DocGroupAccess
        'Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        'Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        'Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelected"), ImageButton)
        ''Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        ''Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        ''ImgNA.Visible = True
        ''ImgA.Visible = False
        'ImgS.Visible = False
        'Img.Visible = True
        'Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        'If rbList.SelectedValue = "" Then
        '    rbList.SelectedValue = "0"
        '    rbList.DataSource = oAccess.RetrieveGroupAccess()
        '    rbList.DataTextField = "GroupAccess"
        '    rbList.DataValueField = "GroupAccessId"
        'End If
        'rbList.DataBind()
        'rbList.Visible = True
        'Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)

        'updPnl.Visible = True
        'updPnl.Update()
        ''pnlGAccess.Visible = True
        ''pnlGAccess.Update
        ''Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        ''updPnlCbox.Update()

    End Sub
    Private Sub HideReceiptDetails(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim oAccess As New DocGroupAccess
        'Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        'Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        'Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelect"), ImageButton)
        ''Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        ''Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        ''ImgNA.Visible = False
        ''ImgA.Visible = False

        'ImgS.Visible = False
        'Img.Visible = True

        'Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        'Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)
        'updPnl.Visible = False
        'rbList.Visible = False
        'updPnl.Update()

        ''Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        ''updPnlCbox.Update()

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
        'If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort4
        '    imgSort4.Visible = True
        'Else
        '    imgSort4.Visible = False
        'End If
        'If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
        '    img = imgSort5
        '    imgSort5.Visible = True
        'Else
        '    imgSort5.Visible = False
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
        DeleteRecord()
    End Sub

    
    
    Private Sub AddReceipt()

        tbDesc.Text = ""
        dlAddress.SelectedValue = ""
        dlUser.SelectedValue = ""
        tbCode.Text = ""
        lAction.Text = "Add"
        ShowAdd()

    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        ShowResult()
    End Sub
#Region "Group Access"
    Public Sub imgShowGroupAccess(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim odoc As New DocGroup
        Dim oImg As ImageButton = DirectCast(sender, ImageButton)

        Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)

        Dim img As ImageButton = DirectCast(oItem.FindControl("imgMinus"), ImageButton)
        img.Visible = Not img.Visible
        Dim img2 As ImageButton = DirectCast(oItem.FindControl("imgPlus"), ImageButton)
        img2.Visible = Not img2.Visible
        Dim rptGA As Repeater = DirectCast(oItem.FindControl("rptGroupAccess"), Repeater)
        Dim lsi As Literal = DirectCast(oItem.FindControl("lOfcCode"), Literal)
        rptGA.Visible = Not rptGA.Visible
        AddHandler rptGA.ItemDataBound, AddressOf ufDatabound
        If rptGA.Visible Then
            odoc.pOfficeCode = lsi.Text
            rptGA.DataSource = odoc.RetrieveOfficeGroup
            rptGA.DataBind()
        End If

    End Sub
    Public Sub ufDatabound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        'If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
        If e.Item.ItemType = ListItemType.Header Then
            AddHandler DirectCast(e.Item.FindControl("imgDelete"), ImageButton).Click, AddressOf ufDeleteGroups
        End If
    End Sub
    Private Function CheckSelected(ByVal rpt As Repeater) As Boolean
        Dim lbRet As Boolean = False
        For Each rptitem In rpt.Items
            If DirectCast(rptitem.FindControl("cbxDelete"), CheckBox).Checked Then
                lbRet = True
                Exit For
            End If
        Next
        Return lbRet
    End Function
    Public Sub ufDeleteGroupOffice(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim osql As clsSqlConn
        Dim otran As DbTran
        Dim rpt As Repeater
        Try
            
            rpt = DirectCast(DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem).NamingContainer, Repeater)
            If Not CheckSelected(rpt) Then
                Master.ShowMessage("Select record to remove before clicking on Remove button.")
            Else
                Dim rpti As RepeaterItem = DirectCast(rpt.NamingContainer, RepeaterItem)
                Dim lsi As Literal = DirectCast(rpti.FindControl("lOfcCode"), Literal)
                otran = New DbTran
                osql = New clsSqlConn(otran.pTran)
                For Each rptitem In rpt.Items
                    If DirectCast(rptitem.FindControl("cbxDelete"), CheckBox).Checked Then
                        DeleteOfficeAccess(osql, lsi.Text, DirectCast(rptitem.FindControl("lGroupId"), Literal).Text)
                    End If
                Next
                otran.pTran.Commit()
                Dim pga As UpdatePanel = DirectCast(rpti.FindControl("pga"), UpdatePanel)
                pga.Update()
                Dim odoc As New DocGroup
                odoc.pOfficeCode = lsi.Text
                rpt.DataSource = odoc.RetrieveOfficeGroup
                rpt.DataBind()
            End If
        Catch ex As Exception
            If Not otran.pTran Is Nothing Then
                otran.pTran.Rollback()
            End If
            Master.ShowMessage("Error occurred while deleting office group access('" & ex.Message & "'). Please try again.")
        Finally
            If Not rpt Is Nothing Then
                rpt.Dispose()
            End If
            If Not osql Is Nothing Then
                osql.Dispose()
            End If
            If Not otran Is Nothing Then
                otran.Dispose()
            End If
        End Try


    End Sub


    Private Sub DeleteOfficeAccess(ByVal osql As clsSqlConn, ByVal asofficecode As String, ByVal groupid As String)
        Dim ogrp As New DocGroup
        ogrp.pOfficeCode = asofficecode
        ogrp.pGroupID = groupid
        ogrp.DeleteOfficeAccess(osql)
    End Sub

    Public Sub ufDeleteGroups(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim osql As clsSqlConn
        Dim otran As DbTran
        Try
            otran = New DbTran
            osql = New clsSqlConn(otran.pTran)
            Dim rpt As Repeater = DirectCast(DirectCast(sender, ImageButton).NamingContainer, Repeater)
            Dim rpti As RepeaterItem = DirectCast(rpt.NamingContainer, RepeaterItem)
            Dim lsi As Literal = DirectCast(rpti.FindControl("lOfcCode"), Literal)

            For Each rptitem In rpt.Items
                If DirectCast(rptitem.FindControl("cbDelete"), CheckBox).Checked Then

                    DeleteOfficeAccess(osql, lsi.Text, DirectCast(rptitem.FindControl("lGroupId"), Literal).Text)
                End If
            Next
            otran.pTran.Commit()
        Catch ex As Exception
            otran.pTran.Rollback()
        Finally
            If Not osql Is Nothing Then
                osql.Dispose()
            End If
            If Not otran Is Nothing Then
                otran.Dispose()
            End If
        End Try


    End Sub

    Private Sub AddOfficeAccess(ByVal objCommand As clsSqlConn, ByVal asOfficeCode As String)
        Dim loDocGroup As New DocGroup
        Try
            Dim i

            For i = 0 To cbGroups.Items.Count - 1
                If cbGroups.Items(i).Selected Then
                    loDocGroup.pOfficeCode = asOfficeCode
                    loDocGroup.pGroupID = cbGroups.Items(i).Value
                    loDocGroup.pUserId = DocSession.sUserId
                    loDocGroup.AddOfficeGroupAccess(objCommand)
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub ufSelectAll()
        imgSelectAll.Visible = False
        imgUnSelectAll.Visible = True
        Try
            Dim i

            For i = 0 To cbGroups.Items.Count - 1
                cbGroups.Items(i).Selected = True
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        'pnlAdd.Update()
    End Sub
    Public Sub ufUnSelectAll()
        imgSelectAll.Visible = True
        imgUnSelectAll.Visible = False

        Try
            Dim i

            For i = 0 To cbGroups.Items.Count - 1
                cbGroups.Items(i).Selected = False
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        'pnlAdd.Update()
    End Sub
#End Region
End Class