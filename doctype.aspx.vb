Imports System
Imports System.Data.SqlClient
Public Class doctype
    Inherits System.Web.UI.Page
#Region "Page Events"

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Master.SelectTab("Account")
        UserControlAdminMenuH1.SelectTab("Doctype")
        AddHandler ucAdd.e_click, AddressOf AddDoctype
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveDocType()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub

    Private Sub RetrieveDocTypes()
        Dim oDoc As New DocTypes
        oDoc.pGroupId = DocSession.sUserGroup
        Using ldata As DataTable = oDoc.GetAllDocTypes

            dlDocType.DataSource = ldata
            dlDocType.DataTextField = "DocName"
            dlDocType.DataValueField = "DocType"
            dlDocType.DataBind()
        End Using



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
            RetrieveDocTypes()

            RetrieveDocType()
            'GetStatus()


        End If
    End Sub
#End Region

    Private Function RetrieveDocIndex(ByVal asDoctype As String, ByVal asSortCol As String) As DataTable


        Dim ldata As DataTable
        Dim odi As DocIndex
        Try
            'objCommand = New clsSqlConn
            'objCommand.pCommandType = CommandType.StoredProcedure
            odi = New DocIndex
            odi.pDocType = asDoctype
            odi.pSortCol = asSortCol

            ldata = odi.RetrieveDocIndexPerDocType()
            'objCommand.pCommandText = "xMSP_DOCINDEXGET"
            'objCommand.ParametersAddWithValue("@doctype", asDoctype)

            'ldata = objCommand.ExecData

            Return ldata

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            'objCommand.Dispose()
            'objCommand = Nothing
            'End If

        End Try

    End Function

    Private Sub RetrieveDocType()


        'Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim odt As DocTypes
        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_DOCTYPEGET"
            odt = New DocTypes
            If tDocType.Text.Trim <> "" Then
                'objCommand.ParametersAddWithValue("@DocType", tDocType.Text.Trim)
                odt.pDocType = tDocType.Text.Trim
            End If

            If tbDesc.Text.Trim <> "" Then
                'objCommand.ParametersAddWithValue("@DocName", tbDesc.Text.Trim)
                odt.pDocName = tbDesc.Text.Trim
            End If
            odt.pRowsPerPage = DocSession.RowsPerPage
            odt.pIdx = hfCurrent.Value
            odt.pSortOrder = hfSortOrder.Value
            odt.pSortCol = hfSortCol.Value

            ldata = odt.RetrieveDocType

            'Repeater1.DataSource = ldata
            'Repeater1.DataBind()
            'lRecordCount.Text = CStr(ldata.Rows.Count)


            'ldata = odoc.RetrieveUser
            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = odt.CountDocType

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

            pnlRepeater.Update()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            'objCommand.Dispose()
            'objCommand = Nothing
            'End If
        End Try

    End Sub

    Protected Sub imgAddDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddDoc.Click
        lAction.Text = "Add"
        tbDocType.ReadOnly = False
        tbDocType.CssClass = "entryfld"
        tbDocType.Text = ""
        tbDocTypeDesc.Text = ""
        cbRequired.Checked = True
        cbAllowPrinting.Checked = False
        msg.Text = ""

        cbRetention.Checked = False
        EnableRetention(False)

        'rbCreate.Checked = False
        'rbStat.Checked = True
        'dlRetentionStatus.SelectedValue = DirectCast(ri.FindControl("lRetStat"), Literal).Text

        tbAP.Text = ""
        tbSP.Text = ""
        dlAP.SelectedValue = "Y"
        dlSP.SelectedValue = "Y"

        Repeater4.DataSource = retrievedata()
        Repeater4.DataBind()


        ShowAdd()




    End Sub

    Private Sub AddDoctype()
        lAction.Text = "Add"
        tbDocType.ReadOnly = False
        tbDocType.CssClass = "entryfld"
        tbDocType.Text = ""
        tbDocTypeDesc.Text = ""
        cbRequired.Checked = True
        cbAllowPrinting.Checked = False
        msg.Text = ""
        Repeater4.DataSource = retrievedata()
        Repeater4.DataBind()
        ShowAdd()
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        RetrieveDocType()
        ShowResult()

    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        If tbDocType.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Document Type is a required field."
        ElseIf tbDocTypeDesc.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Description is a required field."
        ElseIf cbRetention.Checked AndAlso tbAP.Text.Trim = "" Then

            msg.CssClass = "msg_red"
            msg.Text = "** Active Period is required when Retention is enabled."
        ElseIf cbRetention.Checked AndAlso tbSP.Text.Trim = "" Then
            msg.CssClass = "msg_red"
            msg.Text = "** Storage Period is required when Retention is enabled."

        Else
            If lAction.Text = "Update" OrElse DocTypeDoesNotExist() Then
                SaveDocType()
                RetrieveDocType()
            End If
        End If
        pnlMsg.Update()
    End Sub

    Private Function DocTypeDoesNotExist() As Boolean


        Dim objCommand As clsSqlConn


        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT d.doctype, d.docname FROM doctype d WHERE (d.doctype = '" & tbDocType.Text & "') and isnull(d.inactive,0) = 0 "
            'objCommand.ParametersAddWithValue("@DocType", )

            If objCommand.ExecHasRow Then
                msg.CssClass = "msg_red"
                msg.Text = "Document Type " & tDocType.Text & " already exist. Please try another Document Type."
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
    'Private Sub GetStatus()
    '    'Dim objCommand As clsSqlConn
    '    Dim ldata As DataTable
    '    Dim oDoc As DocTypes
    '    Try
    '        oDoc = New DocTypes
    '        ldata = oDoc.GetDocStatus()

    '        dlRetentionStatus.DataSource = ldata
    '        dlRetentionStatus.DataTextField = "description"
    '        dlRetentionStatus.DataValueField = "statusid"
    '        dlRetentionStatus.DataBind()
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '    End Try

    'End Sub
    Private Sub SaveDocType()

        Dim ec As crypt
        Dim objCommand As clsSqlConn
        Dim txtName, txtDataLength, txtDataDecimal As TextBox
        Dim dlDataType As DropDownList
        'Dim ltr As SqlTransaction
        Dim ltr As DbTran
        Dim lbAdd As Boolean
        Dim lbErr As Boolean = False
        Dim lmsg As String = ""
        Dim intDataLength, intDataDecimal As Integer
        Dim lCID, txtName2, lDataType As Literal
        Dim lOldCodeDesc As String = ""
        Dim lOldColDesc As String = ""
        Dim cDisplay As CheckBox
        'Dim oDocApp As DocApproval

        Try
            ec = New crypt
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            Dim oIn As New DocTypes
            objCommand.CommandType = CommandType.Text
            tbDocType.Text = tbDocType.Text.ToUpper
            objCommand.ParametersClear()
            oIn.pDocName = tbDocTypeDesc.Text
            oIn.pDocType = tbDocType.Text.ToUpper
            oIn.pUserId = DocSession.sUserId
            oIn.pIpAddress = Request.UserHostAddress
            oIn.pFileRequired = IIf(cbRequired.Checked, "1", "0")
            oIn.pAllowPrinting = IIf(cbAllowPrinting.Checked, "1", "0")
            oIn.pRetentionStatusId = "0" 'dlRetentionStatus.SelectedValue
            oIn.pRetentionPeriod = dlAP.SelectedValue
            oIn.pPurgePeriod = dlSP.SelectedValue
            If tbAP.Text.Trim = "" Then
                oIn.pRetentionDays = "null"
            Else
                oIn.pRetentionDays = tbAP.Text
            End If
            If tbSP.Text.Trim = "" Then
                oIn.pPurgeDays = "null"
            Else
                oIn.pPurgeDays = tbSP.Text
            End If

            oIn.pUseCreatedDate = "0" 'IIf(rbCreate.Checked, "1", "0")
            oIn.pEnableRetention = IIf(cbRetention.Checked, "1", "0")
            If lAction.Text = "Add" Then
                'objCommand.CommandText = "xMSP_DOCTYPEADD"

                oIn.AddDocType(objCommand)
            Else
                oIn.pDocName_o = Server.HtmlDecode(lDocTypeDesc.Text)
                oIn.UpdateDocType(objCommand)
            End If

            Dim repeaterList As Repeater
            For Each ri In Repeater4.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    'If DirectCast(ri.FindControl("cbxSelect"), CheckBox).Checked Then

                    txtName = DirectCast(ri.FindControl("tColumnName"), TextBox)
                    txtName2 = DirectCast(ri.FindControl("lColumnname"), Literal)
                    txtDataLength = DirectCast(ri.FindControl("tDataLength"), TextBox)
                    txtDataDecimal = DirectCast(ri.FindControl("tDataDecimal"), TextBox)
                    dlDataType = DirectCast(ri.FindControl("dlDataType"), DropDownList)
                    lCID = DirectCast(ri.FindControl("lColumnID"), Literal)
                    lDataType = DirectCast(ri.FindControl("tDataType"), Literal)
                    cDisplay = DirectCast(ri.FindControl("cbDisplay"), CheckBox)

                    lbAdd = False

                    If dlDataType.SelectedValue = "1" Then 'character
                        If txtName.Text.Trim <> "" And txtDataLength.Text.Trim = "" Then
                            lbErr = True
                            lmsg = "Length for column " & txtName.Text.Trim & " is required."
                            Exit For
                        ElseIf txtName.Text.Trim <> "" And txtDataLength.Text.Trim <> "" Then
                            intDataLength = CInt(txtDataLength.Text.Trim)
                            If intDataLength > 500 Then
                                lbErr = True
                                lmsg = "Length for column " & txtName.Text.Trim & " should not exceed 500."
                                Exit For
                            Else
                                lbAdd = True
                            End If
                            intDataDecimal = 0

                        Else
                            lbAdd = False
                        End If
                    ElseIf dlDataType.SelectedValue = "2" Then 'numeric
                        If txtName.Text.Trim <> "" And txtDataLength.Text.Trim = "" Then
                            lbErr = True
                            lmsg = "Length for column " & txtName.Text.Trim & " is required."
                            Exit For
                        ElseIf txtName.Text.Trim <> "" And txtDataLength.Text.Trim <> "" Then
                            intDataLength = CInt(txtDataLength.Text.Trim)
                            If txtDataDecimal.Text.Trim = "" Then
                                intDataDecimal = 0
                            Else

                            End If
                            lbAdd = True
                        Else
                            lbAdd = False
                        End If

                    ElseIf dlDataType.SelectedValue = "3" Then
                        If txtName.Text.Trim <> "" Then
                            intDataLength = 0
                            intDataDecimal = 0
                            lbAdd = True
                        Else
                            lbAdd = False
                        End If
                    ElseIf dlDataType.SelectedValue = "4" Then
                        If txtName.Text.Trim <> "" Then
                            intDataLength = 0
                            intDataDecimal = 0
                            lbAdd = True
                        Else
                            lbAdd = False
                        End If
                    ElseIf dlDataType.SelectedValue = "5" Then
                        'If txtName.Text.Trim <> "" And txtDataLength.Text.Trim = "" Then
                        '    lbErr = True
                        '    lmsg = "Length for column " & txtName.Text.Trim & " is required."
                        '    Exit For
                        'Else
                        If txtName.Text.Trim <> "" Then
                            'intDataLength = CInt(txtDataLength.Text.Trim)
                            intDataLength = 0
                            If txtDataDecimal.Text.Trim = "" Then
                                intDataDecimal = 0
                            Else

                            End If
                            lbAdd = True
                        Else
                            lbAdd = False
                        End If
                    End If

                    If lbAdd Then


                        oIn.pColumnName = txtName.Text

                        oIn.pDataDecimal = IIf(txtDataDecimal.Text.Trim = "", "0", txtDataDecimal.Text.Trim())
                        oIn.pDataLength = CStr(intDataLength)
                        oIn.pDataType = dlDataType.SelectedValue
                        'oIn.pdatatypedesc = dlDataType.SelectedItem.Text
                        oIn.pUserId = DocSession.sUserId
                        oIn.pDisplay = IIf(cDisplay.Checked, "1", "0")
                        If lCID.Text <> "" Then
                            oIn.pColumnId = lCID.Text
                            oIn.UpdateDocIndex(objCommand)
                        Else
                            Dim lColId As String = DocSession.getNextID("ColumnId")
                            oIn.pColumnId = lColId
                            oIn.AddIndex(objCommand)
                        End If

                        'objCommand.ParametersClear()
                        'objCommand.CommandText = "xMSP_DOCINDEXADD"
                        'objCommand.ParametersAddWithValue("@DocType", tbDocType.Text)
                        'objCommand.ParametersAddWithValue("@ColumnName", txtName.Text)
                        'objCommand.ParametersAddWithValue("@ColumnID", CInt(IIf(lCID.Text.Trim = "", "0", lCID.Text.Trim)))
                        'objCommand.ParametersAddWithValue("@DataType", CInt(dlDataType.SelectedValue))
                        'objCommand.ParametersAddWithValue("@DataTypeDesc", dlDataType.SelectedItem.Text)
                        'objCommand.ParametersAddWithValue("@DataLength", intDataLength)
                        'objCommand.ParametersAddWithValue("@DataDecimal", CInt(IIf(txtDataDecimal.Text.Trim = "", "0", txtDataDecimal.Text.Trim())))
                        'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
                        'objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
                        'objCommand.ExecTranNonQuery()
                        If dlDataType.SelectedValue = "5" Then

                            repeaterList = DirectCast(ri.FindControl("RepeaterList"), Repeater)

                            For Each ri2 In repeaterList.Items
                                If ri2.ItemType = ListItemType.Item Or ri2.ItemType = ListItemType.AlternatingItem Then
                                    oIn.pCodeDesc = DirectCast(ri2.FindControl("tDesc"), TextBox).Text
                                    lOldCodeDesc = DirectCast(ri2.FindControl("lDesc"), Literal).Text
                                    oIn.pCode = DirectCast(ri2.FindControl("lCode"), Literal).Text
                                    If oIn.pCode <> "" OrElse oIn.pCodeDesc <> "" Then

                                        If oIn.pCode <> "" AndAlso oIn.pCodeDesc = "" Then
                                            If oIn.IndexListItemIsUsed Then
                                                lbErr = True
                                                lmsg = "List item " & lOldCodeDesc & " is being used by another document. Cannot be deleted."
                                                Exit For
                                            Else
                                                oIn.DeleteIndexListItem(objCommand)
                                            End If


                                        ElseIf oIn.pCode <> "" AndAlso oIn.pCodeDesc <> lOldCodeDesc Then
                                            oIn.UpdateIndexList(objCommand)
                                        ElseIf oIn.pCode = "" AndAlso oIn.pCodeDesc <> "" Then
                                            oIn.pCode = DocSession.getNextID("IndexListId")
                                            oIn.SaveIndexList(objCommand)
                                        End If

                                    End If
                                End If
                            Next
                        End If
                    Else
                        If lCID.Text.Trim <> "" AndAlso txtName.Text.Trim = "" Then
                            'objCommand.ParametersClear()
                            'objCommand.CommandText = "xMSP_DOCINDEXDELETE"
                            'objCommand.ParametersAddWithValue("@DocType", tbDocType.Text)
                            'objCommand.ParametersAddWithValue("@ColumnID", CInt(IIf(lCID.Text.Trim = "", "0", lCID.Text.Trim)))
                            'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId)
                            'objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
                            'objCommand.ParametersAddWithValue("@ColumnName", txtName2.Text)
                            'objCommand.ExecTranNonQuery()
                            oIn.pDocType = tbDocType.Text
                            oIn.pColumnName = txtName.Text
                            oIn.pColumnId = lCID.Text
                            If oIn.IndexListIsUsed() Then
                                lbErr = True

                                lmsg = "Index column " & txtName2.Text & " is being used by another document. Cannot be deleted."
                                Exit For
                            Else
                                oIn.DeleteIndex(objCommand)


                                'If lDataType.Text = "5" Then
                                '    If oIn.IndexListItemIsUsed Then
                                '        lbErr = True
                                '        lmsg = "List item " & lOldCodeDesc & " is being used by another document. Cannot be deleted."
                                '        Exit For
                                '    Else
                                oIn.DeleteIndexList(objCommand)
                                'End If
                                'End If
                            End If

                        End If

                    End If



                End If
                'End If
            Next
            If lbErr Then
               
                If Not ltr Is Nothing Then
                    ltr.pTran.Rollback()
                End If

                msg.CssClass = "msg_red"
                msg.Text = lmsg
                pnlMsg.Update()
            Else
                If Not ltr Is Nothing Then
                    ltr.pTran.Commit()
                End If

                If lAction.Text = "Add" Then
                    msg.CssClass = "msg_gren"
                    msg.Text = "** Document Type " & tbDocType.Text.Trim & " has been created successfully."
                    tbDocType.Text = ""

                    tbDocTypeDesc.Text = ""

                Else
                    msg.CssClass = "msg_green"
                    msg.Text = "** Document Type " & tbDocType.Text.Trim & " has been updated successfully."

                End If

                Repeater4.DataSource = retrievedata(RetrieveDocIndex(tbDocType.Text, "Import Col Seq"), 10)
                Repeater4.DataBind()
                End If


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

    'Public Sub fSetLength(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
    '    Dim imgExp As ImageButton
    '    Dim imgU As ImageButton

    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        DirectCast(e.Item.FindControl("tDesc"), TextBox).MaxLength = pMaxLen
    '    End If
    '    'If e.Item.ItemType = ListItemType.Header Then
    '    '    Dim imgDel As ImageButton = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
    '    '    AddHandler imgDel.Click, AddressOf DeleteDocType
    '    'End If
    'End Sub

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        Dim imgExp As ImageButton
        Dim imgU As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            imgExp = DirectCast(e.Item.FindControl("imgExpand"), ImageButton)
            imgU = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            'ImgA = DirectCast(e.Item.FindControl("imgApprover"), ImageButton)
            'lT = DirectCast(e.Item.FindControl("lAppType"), Literal)
            AddHandler imgExp.Click, AddressOf ShowIndex
            AddHandler imgU.Click, AddressOf UpdateDocType

            'If lT.Text = "D" Then
            'AddHandler ImgA.Click, AddressOf ShowApprovers
            'End If

        End If
        'If e.Item.ItemType = ListItemType.Header Then
        '    Dim imgDel As ImageButton = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
        '    AddHandler imgDel.Click, AddressOf DeleteDocType
        'End If
    End Sub

    Private Sub ShowIndex(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ri As RepeaterItem

        If DirectCast(sender, ImageButton).ImageUrl = "images/minus.jpg" Then
            DirectCast(sender, ImageButton).ImageUrl = "images/plus.jpg"
        Else
            DirectCast(sender, ImageButton).ImageUrl = "images/minus.jpg"
        End If
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)


        Dim rptIndex As Repeater = DirectCast(ri.FindControl("Repeater5"), Repeater)
        AddHandler rptIndex.ItemDataBound, AddressOf ShowItemList

        Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        Dim pnlhdr As Panel = DirectCast(ri.FindControl("hdr"), Panel)
        Dim pnlftr As Panel = DirectCast(ri.FindControl("ftr"), Panel)
        pnlftr.Visible = Not pnlftr.Visible
        pnlhdr.Visible = Not pnlhdr.Visible
        rptIndex.DataSource = RetrieveDocIndex(lDocType.Text, "Import Col Seq")
        rptIndex.DataBind()

        rptIndex.Visible = Not rptIndex.Visible
        pnlRepeater.Update()


    End Sub

    Private Sub EnableEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim btnEdit As ImageButton
        If e.Item.ItemType = ListItemType.Header Then
            btnEdit = DirectCast(e.Item.FindControl("btEdit"), ImageButton)
            AddHandler btnEdit.Click, AddressOf UpdateDocType


        End If

    End Sub

    Private Sub ShowItemList(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim btnEdit As ImageButton
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lDataType"), Literal).Text = "5" Then
                Dim rpt As Repeater = DirectCast(e.Item.FindControl("RepeaterList"), Repeater)
                Dim oId As New DocTypes
                oId.pDocType = DirectCast(e.Item.FindControl("lDocType"), Literal).Text
                oId.pColumnId = DirectCast(e.Item.FindControl("lColumnId"), Literal).Text
                rpt.Visible = True
                rpt.DataSource = oId.RetrieveIndexList
                rpt.DataBind()
            End If

        End If

    End Sub

#Region "Button Events"

    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

#End Region

    Private Sub UpdateDocType(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ri As RepeaterItem
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
        Dim lDocName As Literal = DirectCast(ri.FindControl("DocName"), Literal)
        
        'Dim lAppType As Literal = DirectCast(ri.FindControl("lApprovalType"), Literal)
        lAction.Text = "Update"
        tbDocType.Text = Server.HtmlDecode(lDocType.Text)
        tbDocType.ReadOnly = True
        tbDocType.CssClass = "entryflddisabled"
        tbDocTypeDesc.Text = Server.HtmlDecode(lDocName.Text)
        lDocTypeDesc.Text = lDocName.Text

        If DirectCast(ri.FindControl("lFile"), Literal).Text = "True" Then
            cbRequired.Checked = True
        Else
            cbRequired.Checked = vbFalse
        End If

        If DirectCast(ri.FindControl("lAllowPrinting"), Literal).Text = "True" Then
            cbAllowPrinting.Checked = True
        Else
            cbAllowPrinting.Checked = vbFalse
        End If

        If DirectCast(ri.FindControl("lEnableRetn"), Literal).Text = "True" Then
            cbRetention.Checked = True
            EnableRetention(True)
            'If DirectCast(ri.FindControl("lTrigger"), Literal).Text = "True" Then
            '    rbCreate.Checked = True
            '    rbStat.Checked = False
            'Else
            '    rbCreate.Checked = False
            '    rbStat.Checked = True
            '    dlRetentionStatus.SelectedValue = DirectCast(ri.FindControl("lRetStat"), Literal).Text
            'End If

            tbAP.Text = DirectCast(ri.FindControl("ltrlAD"), Literal).Text
            tbSP.Text = DirectCast(ri.FindControl("ltrlSA"), Literal).Text
            dlAP.SelectedValue = DirectCast(ri.FindControl("ltrlAP"), Literal).Text
            dlSP.SelectedValue = DirectCast(ri.FindControl("ltrlSP"), Literal).Text

        Else
            cbRetention.Checked = False
            EnableRetention(False)

            'rbCreate.Checked = False
            'rbStat.Checked = True
            'dlRetentionStatus.SelectedValue = DirectCast(ri.FindControl("lRetStat"), Literal).Text

            tbAP.Text = ""
            tbSP.Text = ""
            dlAP.SelectedValue = "Y"
            dlSP.SelectedValue = "Y"
        End If

        'If lAppType.Text.Trim <> "" Then
        'ddlApprovalType.SelectedValue = lAppType.Text.Trim
        'End If
        Dim rows As Integer
        rows = 10
        Repeater4.DataSource = retrievedata(RetrieveDocIndex(lDocType.Text, "Import Col Seq"), rows)
        Repeater4.DataBind()

        ShowAdd()

    End Sub

    Public Sub DeleteDocType()
        Dim ri As RepeaterItem
        Dim cbox As CheckBox

        Dim loRow As DataRow
        Dim oDoc As New DocTypes
        Dim loData As DataTable

        Try
            loData = New DataTable("tblGroup")
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("DocName", Type.GetType("System.String"))
            loData.Columns.Add("Exists", Type.GetType("System.String"))


            For Each ri In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbxDelete"), CheckBox)
                    If cbox.Checked Then
                        loRow = loData.NewRow()
                        loRow("DocType") = DirectCast(ri.FindControl("ldoctype"), Literal).Text
                        loRow("DocName") = DirectCast(ri.FindControl("docName"), Literal).Text
                        loRow("Exists") = IIf(oDoc.CheckIfDocTypeExists(loRow("DocType")), "Cannot delete record being used.", "OK to delete.")
                        'xMSP_DOCTYPEEXISTS
                        loData.Rows.Add(loRow)
                    End If
                End If
            Next
            '09993782957
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

        'Dim lblDocName As Literal
        '', lblActive As Literal

        'Dim tDocName As TextBox
        ''Dim cActive As CheckBox


        'If cbUpdate.Checked Then
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lFile"), Literal).Text = "True" Then
                DirectCast(e.Item.FindControl("lRequired"), Literal).Text = "Yes"
            Else
                DirectCast(e.Item.FindControl("lRequired"), Literal).Text = "No"
            End If
            If DirectCast(e.Item.FindControl("lAllowPrinting"), Literal).Text = "True" Then
                DirectCast(e.Item.FindControl("lAllow"), Literal).Text = "Yes"
            Else
                DirectCast(e.Item.FindControl("lAllow"), Literal).Text = "No"
            End If
            If DirectCast(e.Item.FindControl("lEnableRetn"), Literal).Text = "True" Then
                'If DirectCast(e.Item.FindControl("lTrigger"), Literal).Text = "True" Then
                'DirectCast(e.Item.FindControl("lTriggerDesc"), Literal).Text = "Retention Active Period will start from time the document was created."
                'Else
                DirectCast(e.Item.FindControl("lTriggerDesc"), Literal).Text = "Retention Active Period wil start from the time document was set to 'Archived' status."
                'End If
                DirectCast(e.Item.FindControl("pRet"), Panel).Visible = True
                If DirectCast(e.Item.FindControl("ltrlAP"), Literal).Text = "D" Then
                    DirectCast(e.Item.FindControl("lAPDesc"), Literal).Text = " Day(s)"
                ElseIf DirectCast(e.Item.FindControl("ltrlAP"), Literal).Text = "M" Then
                    DirectCast(e.Item.FindControl("lAPDesc"), Literal).Text = " Month(s)"
                ElseIf DirectCast(e.Item.FindControl("ltrlAP"), Literal).Text = "Y" Then
                    DirectCast(e.Item.FindControl("lAPDesc"), Literal).Text = " Year(s)"
                Else
                    DirectCast(e.Item.FindControl("lAPDesc"), Literal).Text = " "
                End If

                If DirectCast(e.Item.FindControl("ltrlSP"), Literal).Text = "D" Then
                    DirectCast(e.Item.FindControl("lSPDesc"), Literal).Text = " Day(s)"
                ElseIf DirectCast(e.Item.FindControl("ltrlSP"), Literal).Text = "M" Then
                    DirectCast(e.Item.FindControl("lSPDesc"), Literal).Text = " Month(s)"
                ElseIf DirectCast(e.Item.FindControl("ltrlSP"), Literal).Text = "Y" Then
                    DirectCast(e.Item.FindControl("lSPDesc"), Literal).Text = " Year(s)"
                Else
                    DirectCast(e.Item.FindControl("lSPDesc"), Literal).Text = " "
                End If
            Else
                DirectCast(e.Item.FindControl("lTriggerDesc"), Literal).Text = "Not Enabled"
            End If
        End If
        '        lblDocName = DirectCast(e.Item.FindControl("DocName"), Literal)
        '        lblDocName.Visible = False
        '        tGRoupName = DirectCast(e.Item.FindControl("tbGroupName"), TextBox)
        '        tGRoupName.Visible = True



        '        'lblActive = DirectCast(e.Item.FindControl("Active"), Literal)
        '        'lblActive.Visible = False
        '        'cActive = DirectCast(e.Item.FindControl("cbLocked"), CheckBox)
        '        'cActive.Checked = (lblActive.Text = "Yes")
        '        'cActive.Visible = True


        '    End If
        'End If
    End Sub

    Protected Sub btDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btDelete.Click
        Dim ri As RepeaterItem

        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As New DbTran


        Try
            objCommand = New clsSqlConn(ltr.pTran)

            'objCommand.CommandType = CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_DOCTYPEDELETE"
            Dim odt As New DocTypes
            odt.pIpAddress = Request.UserHostAddress
            For Each ri In Repeater2.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    If DirectCast(ri.FindControl("lExists"), Literal).Text = "OK to delete." Then
                        'objCommand.ParametersAddWithValue("@DocType", DirectCast(ri.FindControl("tDocType"), Literal).Text)
                        'objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
                        'objCommand.ParametersAddWithValue("@UserId", DocSession.sUserId
                        odt.pDocType = DirectCast(ri.FindControl("tDocType"), Literal).Text
                        odt.DeleteDocType(objCommand)
                        'objCommand.ExecTranNonQuery()
                        'objCommand.ParametersClear()
                    End If

                End If
            Next
            ltr.pTran.Commit()
            RetrieveDocType()
            Master.ShowImageDocument = False
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

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
        ShowResult()
    End Sub

    Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        pSearchCriteria.Visible = Not pSearchCriteria.Visible
        pnlSearchCriteria.Update()
    End Sub

    Private Sub ShowResult()
        pSearchCriteria.Visible = False
        pnlSearchCriteria.Update()


        pAddDoc.Visible = False
        pnlAddDoc.Update()

        pDeleteDoc.Visible = False
        pnlDeleteDoc.Update()

        pRepeater.Visible = True
        pnlRepeater.Update()

        imgAddDoc.Visible = True
        'pnlTab.Update()
    End Sub

    Private Sub ShowDeletes()

        pSearchCriteria.Visible = False
        pnlSearchCriteria.Update()

        pRepeater.Visible = False
        pnlRepeater.Update()

        pAddDoc.Visible = False
        pnlAddDoc.Update()

        pDeleteDoc.Visible = True
        pnlDeleteDoc.Update()

    End Sub

    Private Sub ShowDelete()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pAddDoc.Visible = False
        'pnlAddDoc.Update()

        pDeleteDoc.Visible = True
        pnlDeleteDoc.Update()
        Master.ShowImageDocument = True
    End Sub

    Private Sub ShowAdd()
        msg.Text = ""
        pCopy.Visible = False
        'cbRequired.Checked = True
        Master.ShowImageDocument = True
        'Dim csname1 As String = "HidScroolBar"
        'Dim cstype As Type = Me.GetType()

        'Dim cs As ClientScriptManager = Page.ClientScript

        '' Check to see if the startup script is already registered.
        'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

        '    Dim cstext1 As String = "document.body.scroll='no';"
        '    ' Dim cstext1 As String = "alert('pogi');"
        '    cs.RegisterStartupScript(cstype, csname1, cstext1, True)

        'End If
        RetrieveDocTypes()
        pAddDoc.Visible = Not pAddDoc.Visible
        pnlAddDoc.Update()
    End Sub

    Private Sub ShowSearch()

        pSearchCriteria.Visible = Not pSearchCriteria.Visible
        pnlSearchCriteria.Update()

    End Sub

    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Master.ShowImageDocument = False
        ShowResult()
    End Sub

    Protected Sub imgClose2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose2.Click
        Master.ShowImageDocument = False
        ShowResult()
    End Sub
    Dim MaxLen As Integer
    Private Property pMaxLen As Integer
        Get
            Return MaxLen
        End Get
        Set(ByVal value As Integer)
            MaxLen = value
        End Set
    End Property
#Region "Index List Methods"

    Public Sub ShowList(ByVal sender As Object, ByVal e As EventArgs)
        Dim losender As DropDownList = DirectCast(sender, DropDownList)
        Dim loRptItem As RepeaterItem = DirectCast(losender.NamingContainer, RepeaterItem)
        Dim rpt As Repeater = DirectCast(loRptItem.FindControl("RepeaterList"), Repeater)
        Dim plist As Panel = DirectCast(loRptItem.FindControl("pnlList"), Panel)
        Dim pnl As UpdatePanel = DirectCast(loRptItem.FindControl("pnlCodeList"), UpdatePanel)
        Dim oDt As New DocTypes
        If losender.SelectedValue = "5" Then
            plist.Visible = True
            oDt.pDocType = tbDocType.Text
            oDt.pColumnId = IIf(DirectCast(loRptItem.FindControl("lColumnId"), Literal).Text = "", "0", DirectCast(loRptItem.FindControl("lColumnId"), Literal).Text)
            If Not rpt.Visible Then
                rpt.Visible = True
                'pMaxLen = DirectCast(loRptItem.FindControl("tDataLength"), TextBox).Text
                rpt.DataSource = oDt.RetrieveIndexList(oDt.RetrieveIndexList(), 5)


                rpt.DataBind()

            End If

        Else
            plist.Visible = False
        End If
        'DirectCast(loRptItem.FindControl("imgAddRow"), ImageButton).Focus()
        'losender.Focus()
        pnl.Update()
    End Sub
    Private Function retrieveindexlist() As DataTable

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblIndexList")
            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("Code", Type.GetType("System.String"))
            loData.Columns.Add("CodeDesc", Type.GetType("System.String"))

            'Dim loRow As DataRow
            Dim liCtr As Integer
            liCtr = 0
            For liCtr = 1 To 10
                loRow = loData.NewRow()

                loRow("rowno") = CStr(liCtr)
                loRow("ColumnID") = ""
                loRow("DocType") = ""
                loRow("Code") = ""
                loRow("CodeDesc") = ""
                loData.Rows.Add(loRow)
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
#End Region

#Region "Document Index Repeater"

    Private Sub Repeater4_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater4.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dlDT As DropDownList = DirectCast(e.Item.FindControl("dlDataType"), DropDownList)
            Dim btn As ImageButton = DirectCast(e.Item.FindControl("imgAddRow"), ImageButton)
            Dim rpt As Repeater = DirectCast(e.Item.FindControl("RepeaterList"), Repeater)
            AddHandler dlDT.SelectedIndexChanged, AddressOf ShowList
            AddHandler btn.Click, AddressOf AddItemRows
            AddHandler rpt.ItemDataBound, AddressOf FocusItem
        End If
    End Sub
    Private Sub FocusItem(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.FindControl("tDesc").Focus()
        End If
    End Sub

    Private Sub AddItemRows(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ImageButton = DirectCast(sender, ImageButton)
        Dim riSort As RepeaterItem = DirectCast(btn.NamingContainer, RepeaterItem)
        Dim rpt As Repeater = DirectCast(riSort.FindControl("repeaterlist"), Repeater)


        rpt.DataSource = AddRowItems(5, rpt)
        rpt.DataBind()







    End Sub

    Private Function AddRowItems(ByVal aiRow As Integer, ByVal rpt As Repeater) As DataTable
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblUsers")
            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("Code", Type.GetType("System.String"))
            loData.Columns.Add("CodeDesc", Type.GetType("System.String"))
            Dim ctr As Integer = 0
            For Each ri In rpt.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    ctr = ctr + 1
                    loRow = loData.NewRow()
                    loRow("rowno") = CStr(ctr)
                    loRow("DocType") = DirectCast(ri.FindControl("lDocType"), Literal).Text
                    loRow("ColumnID") = DirectCast(ri.FindControl("lColumnID"), Literal).Text
                    loRow("Code") = DirectCast(ri.FindControl("lCode"), Literal).Text
                    loRow("CodeDesc") = DirectCast(ri.FindControl("tDesc"), TextBox).Text

                    loData.Rows.Add(loRow)

                End If
            Next

            For liCtr = 1 To aiRow
                loRow = loData.NewRow()
                ctr = ctr + 1
                loRow("rowno") = CStr(ctr)
                loRow("ColumnID") = ""
                loRow("DocType") = ""
                loRow("Code") = ""
                loRow("CodeDesc") = ""

                loData.Rows.Add(loRow)
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
    Private Sub Repeater4_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater4.ItemDataBound
        Dim ltType As Literal
        Dim ltDl As DropDownList
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ltType = DirectCast(e.Item.FindControl("tDataType"), Literal)
            ltDl = DirectCast(e.Item.FindControl("dlDataType"), DropDownList)
            ltDl.SelectedValue = ltType.Text

            If ltType.Text = "5" Then
                If lAction.Text = "Update" Then
                    Dim oData As New DocTypes
                    oData.pDocType = tbDocType.Text
                    oData.pColumnId = DirectCast(e.Item.FindControl("lColumnID"), Literal).Text
                    Dim pnl As Panel = DirectCast(e.Item.FindControl("pnlList"), Panel)
                    Dim rpt As Repeater = DirectCast(e.Item.FindControl("RepeaterList"), Repeater)
                    pnl.Visible = True
                    rpt.Visible = True
                    rpt.DataSource = oData.RetrieveIndexList(oData.RetrieveIndexList(), 5)
                    rpt.DataBind()

                End If
            End If
        End If
    End Sub

    Private Function retrievedata(ByVal asData As DataTable, ByVal noofrows As Integer) As DataTable

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblUsers")
            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("ColumnName", Type.GetType("System.String"))
            loData.Columns.Add("DataType", Type.GetType("System.String"))
            loData.Columns.Add("DataTypeDesc", Type.GetType("System.String"))
            loData.Columns.Add("DataLength", Type.GetType("System.String"))
            loData.Columns.Add("DataDecimal", Type.GetType("System.String"))
            loData.Columns.Add("DisplayInScreen", Type.GetType("System.String"))

            'Dim loRow As DataRow
            Dim liCtr As Integer
            liCtr = 0
            If asData.Rows.Count > noofrows Then
                noofrows = asData.Rows.Count
            End If

            For liCtr = 1 To noofrows
                loRow = loData.NewRow()

                If Not asData Is Nothing And asData.Rows.Count >= liCtr Then
                    loRow("rowno") = CStr(liCtr)
                    loRow("DocType") = asData(liCtr - 1)("doctype")
                    loRow("ColumnID") = asData(liCtr - 1)("ColumnId")
                    loRow("ColumnName") = asData(liCtr - 1)("ColumnName")
                    loRow("DataType") = asData(liCtr - 1)("DataType")
                    loRow("DataTypeDesc") = asData(liCtr - 1)("DataTypeDesc")
                    loRow("DataLength") = asData(liCtr - 1)("DataLength")
                    loRow("DataDecimal") = asData(liCtr - 1)("DataDecimal")
                    loRow("DisplayInScreen") = asData(liCtr - 1)("DisplayInScreen")
                Else
                    loRow("rowno") = CStr(liCtr)
                    loRow("DocType") = ""
                    loRow("ColumnID") = ""
                    loRow("ColumnName") = ""
                    loRow("DataType") = "1"
                    loRow("DataTypeDesc") = ""
                    loRow("DataLength") = ""
                    loRow("DataDecimal") = ""
                    loRow("DisplayInScreen") = "False"
                End If
                loData.Rows.Add(loRow)
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

    Private Function retrievedata() As DataTable
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblUsers")
            'Dim loData As New DataTable("tblUsers")
            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("ColumnName", Type.GetType("System.String"))
            loData.Columns.Add("DataType", Type.GetType("System.String"))
            loData.Columns.Add("DataTypeDesc", Type.GetType("System.String"))
            loData.Columns.Add("DataLength", Type.GetType("System.String"))
            loData.Columns.Add("DataDecimal", Type.GetType("System.String"))
            loData.Columns.Add("DisplayInScreen", Type.GetType("System.String"))


            'Dim loRow As DataRow
            Dim liCtr As Integer
            liCtr = 0
            For liCtr = 1 To 10
                loRow = loData.NewRow()

                loRow("rowno") = CStr(liCtr)
                loRow("ColumnID") = ""
                loRow("DocType") = ""
                loRow("ColumnName") = ""
                loRow("DataType") = "1"
                loRow("DataTypeDesc") = ""
                loRow("DataLength") = ""
                loRow("DataDecimal") = ""
                loRow("DisplayInScreen") = "False"

                loData.Rows.Add(loRow)
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
#End Region

#Region "not used - can be deleted"
    Private Sub removeScrollBar()
        Dim csname1 As String = "HidScroolBar"
        Dim cstype As Type = Me.GetType()

        Dim cs As ClientScriptManager = Page.ClientScript

        ' Check to see if the startup script is already registered.
        If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

            Dim cstext1 As String = "document.body.scroll='no';"
            'Dim cstext1 As String = "alert('pogi');"
            cs.RegisterStartupScript(cstype, csname1, cstext1, True)

        End If
    End Sub
    Private Sub ShowCriteria()
        pSearchCriteria.Visible = Not pSearchCriteria.Visible
        pnlSearchCriteria.Update()
        Repeater1.Visible = Not Repeater1.Visible
        pnlRepeater.Update()

        pAddDoc.Visible = False
        pnlAddDoc.Update()
        pDeleteDoc.Visible = Not pDeleteDoc.Visible
        pnlDeleteDoc.Update()
    End Sub
    'Private Sub ddlApprovalType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlApprovalType.SelectedIndexChanged
    '    rptUserList.Visible = False
    '    If DirectCast(sender, DropDownList).SelectedValue = "D" Then
    '        Dim oApp As New DocApproval
    '        oApp.pDocType = tbDocType.Text
    '        rptApprovers.DataSource = oApp.RetrieveDocTypeApprovers
    '        pApp.Visible = True
    '        rptUserList.Visible = False
    '    Else

    '        pApp.Visible = False
    '    End If
    '    pnlApprovers.Update()
    'End Sub
    'Public Sub DeleteApprovers(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    'Dim txtBox As TextBox = DirectCast(sender, TextBox)
    '    Dim oApprover As New DocApproval
    '    Dim ImgBtnSelected As ImageButton

    '    'Dim ltr As SqlClient.SqlTransaction
    '    Dim ltr As New DbTran
    '    Dim objCommand As clsSqlConn
    '    Dim liCtr As Integer
    '    Dim lId As Literal

    '    liCtr = 0
    '    Try
    '        objCommand = New clsSqlConn(ltr.pTran)

    '        For Each ri In rptApprovers.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '                ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '                lId = DirectCast(ri.FindControl("lApproverId"), Literal)

    '                If ImgBtnSelected.Visible Then
    '                    oApprover.pDocType = tbDocType.Text
    '                    oApprover.pUserId = lId.Text
    '                    oApprover.DeleteApprovers(objCommand)

    '                    liCtr += 1
    '                End If


    '            End If

    '        Next

    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '            RetrieveDocType()

    '        End If

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



    'Protected Sub btSaveApprover_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSaveApprover.Click
    '    Dim imgBtnSelected As ImageButton
    '    Dim loData As New DataTable("tblApprovers")
    '    Dim loRow As DataRow
    '    loData.Columns.Add("ApproverId", Type.GetType("System.String"))
    '    loData.Columns.Add("Approver", Type.GetType("System.String"))
    '    loData.Columns.Add("Seqno", Type.GetType("System.String"))
    '    Dim liCtr As Integer
    '    liCtr = 0
    '    For Each ri In rptApprovers.Items
    '        If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '            loRow = loData.NewRow()
    '            loRow("SeqNo") = DirectCast(ri.FindControl("tSeqNo"), TextBox).Text
    '            loRow("ApproverId") = DirectCast(ri.FindControl("lApproverId"), Literal).Text
    '            loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text
    '            loData.Rows.Add(loRow)
    '            liCtr = liCtr + 1
    '        End If
    '    Next

    '    For Each ri In rptUserList.Items
    '        If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '            'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '            imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)

    '            If imgBtnSelected.Visible Then
    '                loRow = loData.NewRow()
    '                loRow("ApproverId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
    '                loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text
    '                liCtr = liCtr + 1
    '                loRow("SeqNo") = liCtr
    '                loData.Rows.Add(loRow)
    '            End If
    '        End If
    '    Next
    '    rptApprovers.DataSource = loData
    '    rptApprovers.DataBind()
    '    rptUserList.Visible = False
    '    txtApprovers.Text = "Add New Approver"
    '    pnlApprovers.Update()


    'End Sub

    'Private Sub f()
    '    Dim ImgBtnSelected As ImageButton
    '    Dim oDoc As DocApproval
    '    Dim lnkId As Literal

    '    'Dim ltr As SqlClient.SqlTransaction
    '    Dim ltr As New DbTran
    '    Dim objCommand As clsSqlConn
    '    Dim liCtr As Integer

    '    liCtr = 0
    '    Try
    '        oDoc = New DocApproval
    '        objCommand = New clsSqlConn(ltr.pTran)

    '        For Each ri In rptUserList.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '                ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '                lnkId = DirectCast(ri.FindControl("lUserId"), Literal)

    '                If ImgBtnSelected.Visible Then
    '                    oDoc.pApproverId = lnkId.Text
    '                    oDoc.pIpAddress = Request.UserHostAddress
    '                    oDoc.pDocType = tbDocType.Text
    '                    oDoc.SaveDoctypeApprovers(objCommand)
    '                    liCtr += 1
    '                End If


    '            End If

    '        Next

    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '            rptUserList.Visible = False
    '            'RetrieveLinks()

    '        End If

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
    'Private Sub ShowApprovers(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    '    Dim ri As RepeaterItem

    '    ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)


    '    Dim rptApp As Repeater = DirectCast(ri.FindControl("rptApprover"), Repeater)

    '    Dim lDocType As Literal = DirectCast(ri.FindControl("lDocType"), Literal)
    '    Dim oApp As New DocApproval
    '    oApp.pDocType = lDocType.Text
    '    rptApp.DataSource = oApp.RetrieveDocTypeApprovers()
    '    rptApp.DataBind()

    '    rptApp.Visible = Not rptApp.Visible
    '    pnlRepeater.Update()


    'End Sub
#End Region

#Region "Adding Rows - Doc Index"

    Private Sub btRows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRows.Click
        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblDocType")
            If tbRows.Text = "" OrElse Not IsNumeric(tbRows.Text) Then
                tbRows.ToolTip = "Please input numeric value"
                tbRows.Text = ""
                btRows.ToolTip = "Please input numeric value before clicking."
                tbRows.Focus()
                Exit Sub
            End If
            btRows.ToolTip = ""
            tbRows.ToolTip = ""

            'Dim loData As New DataTable("tblDocType")
            'Dim loRow As DataRow

            loData.Columns.Add("rowno", Type.GetType("System.String"))
            loData.Columns.Add("DocType", Type.GetType("System.String"))
            loData.Columns.Add("ColumnID", Type.GetType("System.String"))
            loData.Columns.Add("ColumnName", Type.GetType("System.String"))
            loData.Columns.Add("DataType", Type.GetType("System.String"))
            loData.Columns.Add("DataTypeDesc", Type.GetType("System.String"))
            loData.Columns.Add("DataLength", Type.GetType("System.String"))
            loData.Columns.Add("DataDecimal", Type.GetType("System.String"))
            loData.Columns.Add("DisplayInScreen", Type.GetType("System.String"))

            Dim ctr As Integer = 0
            For Each ri In Repeater4.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    ctr = ctr + 1
                    loRow = loData.NewRow()
                    loRow("rowno") = CStr(ctr)
                    loRow("DocType") = DirectCast(ri.FindControl("lDocType"), Literal).Text
                    loRow("ColumnID") = DirectCast(ri.FindControl("lColumnID"), Literal).Text
                    loRow("ColumnName") = DirectCast(ri.FindControl("tColumnName"), TextBox).Text
                    loRow("DataType") = DirectCast(ri.FindControl("dlDataType"), DropDownList).SelectedValue
                    loRow("DataTypeDesc") = DirectCast(ri.FindControl("dlDataType"), DropDownList).SelectedItem
                    loRow("DataLength") = DirectCast(ri.FindControl("tDataLength"), TextBox).Text
                    loRow("DataDecimal") = DirectCast(ri.FindControl("tDataDecimal"), TextBox).Text
                    loRow("DisplayInScreen") = DirectCast(ri.FindControl("cbDisplay"), CheckBox).Checked.ToString

                    loData.Rows.Add(loRow)

                End If
            Next

            For liCtr = 1 To CInt(tbRows.Text)
                loRow = loData.NewRow()

                loRow("rowno") = CStr(liCtr + ctr)
                loRow("DocType") = ""
                loRow("ColumnID") = ""
                loRow("ColumnName") = ""
                loRow("DataType") = "1"
                loRow("DataTypeDesc") = ""
                loRow("DataLength") = ""
                loRow("DataDecimal") = ""
                loRow("DisplayInScreen") = "true"
                loData.Rows.Add(loRow)
            Next

            Repeater4.DataSource = loData
            Repeater4.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Sub
#End Region

#Region "Deleting Document Types"
    Private Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lExists"), Literal).Text = "OK to delete." Then
                DirectCast(e.Item.FindControl("rw"), HtmlControls.HtmlTableRow).Attributes("class") = "greenHigh"
            End If
        End If
    End Sub
#End Region

#Region "Sorting records"


    Public Sub sortColumn(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)
        Dim riSort As RepeaterItem = DirectCast(lbSort.NamingContainer, RepeaterItem)

        Dim img1 As Image = DirectCast(riSort.FindControl("imgSort1"), Image)
        Dim img2 As Image = DirectCast(riSort.FindControl("imgSort2"), Image)
        Dim img3 As Image = DirectCast(riSort.FindControl("imgSort3"), Image)
        Dim img4 As Image = DirectCast(riSort.FindControl("imgSort4"), Image)
        Dim img As Image = DirectCast(riSort.FindControl("imgSort" & Right(lbSort.ID, 1)), Image)

        If img1.ID = img.ID And img.Visible Then
            img.Visible = True
        Else
            img1.Visible = False
        End If
        If img2.ID = img.ID And img.Visible Then
            img.Visible = True
        Else
            img2.Visible = False

        End If
        If img3.ID = img.ID And img.Visible Then
            img.Visible = True
        Else
            img3.Visible = False

        End If
        If img4.ID = img.ID And img.Visible Then
            img.Visible = True
        Else
            img4.Visible = False

        End If

        Dim reptr5 As Repeater = DirectCast(riSort.FindControl("Repeater5"), Repeater)
        Dim lDocType As Literal = DirectCast(riSort.FindControl("lDocType"), Literal)
        Dim oDocIndex As New DocIndex
        oDocIndex.pDocType = lDocType.Text
        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                oDocIndex.pSortOrder = "Desc"
                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                oDocIndex.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            oDocIndex.pSortOrder = "Asc"
            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If
        oDocIndex.pSortCol = lbSort.Text

        hfSortCol.Value = lbSort.Text
        RetAction()

        'reptr5.DataSource = oDocIndex.RetrieveDocIndexPerDocType()
        'reptr5.DataBind()
        'pnlRepeater.Update()


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

        Dim oDocType As New DocTypes

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                oDocType.pSortOrder = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                oDocType.pSortOrder = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            oDocType.pSortOrder = "Asc"
            img.Visible = True
        End If
        oDocType.pSortCol = lbSort.Text
        oDocType.pIdx = hfCurrent.Value
        oDocType.pRowsPerPage = DocSession.RowsPerPage

        Repeater1.DataSource = oDocType.RetrieveDocType()
        Repeater1.DataBind()
        pnlRepeater.Update()


    End Sub
#End Region


    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        Master.ShowImageDocument = False
        ShowResult()
    End Sub

    Private Sub btCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCopy.Click
        Dim s_sql As String = " DELETE FROM DOCINDEXLIST where DocType = '" & dlDocType.SelectedValue & "' " & _
                                " DELETE FROM DOCINDEX where DocType = '" & dlDocType.SelectedValue & "' " & _
                                " INSERT INTO DocIndex(DocType,ColumnId,ColumnName,DataType,DataLength,DataDecimal) " & _
                                " SELECT '" & dlDocType.SelectedValue & "',ColumnId,ColumnName,DataType,DataLength,DataDecimal FROM DocIndex  " & _
                                " WHERE DocType = '" & tbDocType.Text & "' " & _
                                " INSERT INTO docindexlist(DocType,ColumnId,Code,CodeDesc) " & _
                                " SELECT '" & dlDocType.SelectedValue & "',ColumnId,Code,CodeDesc " & _
                                " FROM DocIndexList Where DocType='" & tbDocType.Text & "' "

        Dim objCommand As clsSqlConn
        
        Try
            objCommand = New clsSqlConn

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecNonQuery()
            msg.Text = "Document Type Index has been copied to " & dlDocType.SelectedItem.Text & "."

            pnlMsg.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
                End If
            End Try

    End Sub

    Private Sub imgCopy_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCopy.Click
        pCopy.Visible = Not pCopy.Visible
    End Sub

    Private Sub cbRetention_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbRetention.CheckedChanged
        If cbRetention.Checked Then
            EnableRetention(True)
        Else
            EnableRetention(False)
        End If
    End Sub
    Private Sub EnableRetention(ByVal enable As Boolean)
        'rbCreate.Enabled = enable
        'rbStat.Enabled = enable
        dlAP.Enabled = enable
        dlSP.Enabled = enable
        tbAP.Enabled = enable
        tbSP.Enabled = enable
        'dlRetentionStatus.Enabled = enable
    End Sub
End Class

