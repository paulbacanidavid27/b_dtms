Public Class UserControlDocLink
    Inherits System.Web.UI.UserControl
    Public Event e_Count()
    
    Public Event e_ViewDoc()
    Dim lDociD As String
    Dim lFileName As String
    Dim lVersion As String
    Dim lCreatedDate As String
    Dim smsg As String
    Public Event e_ShowMessage()
    Public ReadOnly Property pCount As String
        Get
            Return hfCount.value
        End Get
    End Property
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property

    Public Property DocID As String
        Get
            Return lDociD
        End Get
        Set(ByVal value As String)
            lDociD = value
        End Set
    End Property

    Public Property FileName As String
        Get
            Return lFileName
        End Get
        Set(ByVal value As String)
            lFileName = value
        End Set
    End Property

    Public Property CreatedDate As String
        Get
            Return lCreatedDate
        End Get
        Set(ByVal value As String)
            lCreatedDate = value
        End Set
    End Property

    Public Property FVersion As String
        Get
            Return lVersion
        End Get
        Set(ByVal value As String)
            lVersion = value
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
    'pager: step 3
    Public Sub RetAction()
        Dim ldata As DataTable
        Try

            If txtLinks.Text.Trim = "Search Document to Link" Then
                Exit Sub
            End If
            Dim oDocs As New DocList
            oDocs.pSortOrder = ""
            oDocs.pDocId = DocSession.sDocID
            oDocs.pIdx = CInt(hfCurrent.Value)
            oDocs.pDocTitle = txtLinks.Text
            oDocs.pGroupId = DocSession.sUserGroup
            oDocs.pUserId = DocSession.sUserId
            oDocs.pRowsPerPage = CInt(DocSession.RowsPerPage)

            Dim lsTotalRows As String = CStr(oDocs.CountLinkDoc)
            'If CInt(lsTotalRows) > 0 Then
            '    hfCount.Value = lsTotalRows
            'Else
            '    hfCount.Value = ""
            'End If
            ldata = oDocs.RetrieveDocLinks
            If ldata.Rows.Count > 0 Then
                If ldata.Rows.Count > DocSession.RowsPerPage Then
                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
                End If
                rptDocList.DataSource = ldata
                ucPager.EnableButtons(CInt(hfCurrent.Value), lsTotalRows)
                hfTotalRows.Value = lsTotalRows ' CStr(oDocs.pRetVal)
                ucPager.Visible = True
                rptDocList.DataBind()
                rptDocList.Visible = True
                imgClose.Visible = True
                btSave.Visible = True
            Else
                btSave.Visible = False
                Message = "No records found for the selected criteria."
                RaiseEvent e_ShowMessage()
            End If
            pnlDocLinks.Update()
        Catch ex As Exception
            Message = "An error occured while retrieving records (" & ex.Message & "). Please try again."
            RaiseEvent e_ShowMessage()
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
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

    Public Sub DeleteLinks(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ImgBtnSelected As ImageButton
        Dim oDocLinks As New DocLinks
        Dim lnkId As Literal
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer
        Dim LnkBtn As LinkButton
        Dim lsDocs As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)

            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "xMSP_DOCLINKSDELETE"
            For Each ri In rptLinks.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    LnkBtn = DirectCast(ri.FindControl("lbDoc"), LinkButton)
                    lnkId = DirectCast(ri.FindControl("lLinkDocId"), Literal)
                   
                    If ImgBtnSelected.Visible Then
                        If lsDocs = "" Then
                            lsDocs = LnkBtn.Text
                            lsdesc = "link"
                        Else
                            lsDocs = lsDocs & ", " & LnkBtn.Text
                            lsdesc = "links"
                        End If
                        oDocLinks.pDocId = DocSession.sDocID
                        oDocLinks.pLinkDocId = lnkId.Text
                        oDocLinks.pUserId = DocSession.sUserId
                        oDocLinks.pIpAddress = Request.UserHostAddress
                        oDocLinks.DeleteDocLinks(objCommand)
                        liCtr += 1
                    End If

                End If

            Next

            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            If liCtr >= 1 Then
                Dim Ohist As New DocHistory
                Ohist.pTask = "Link"
                Ohist.pAction = "Deleted " & lsdesc & " (" & lsDocs & ")" '-- to document '" & DocSession.sDocTitle & "'"
                Ohist.pUserId = DocSession.sUserId
                Ohist.pIpAddress = Request.UserHostAddress
                Ohist.pDocId = DocSession.sDocID
                Ohist.AddHistory(objCommand)
                ltr.pTran.Commit()
                rptDocList.Visible = False
                RetrieveLinks()
            Else
                ltr.pTran.Rollback()
                Message = "Please select a document before clicking on delete button."
                RaiseEvent e_ShowMessage()
            End If

        Catch ex As Exception

            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Message = "Error occurred while deleting links (" & ex.Message & "). Please try again."
            RaiseEvent e_ShowMessage()
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

    Public Sub RetrieveLinks()
        Dim oLinks As New DocLinks
        Dim ldata As DataTable
        Try

        
            ldata = oLinks.RetrieveDocLinks(DocSession.sDocID)
            rptLinks.DataSource = ldata
            'If ldata.Rows.Count > 0 Then
            '    hfCount.Value = ldata.Rows.Count.ToString("#,##0")
            'Else
            '    hfCount.Value = ""
            'End If
            rptLinks.DataBind()

        'If DocSession.docDisable Then
        '    txtLinks.Visible = False
        '    imgSearch.Visible = False
        '    'btSaveLinks.Visible = False
        '    'imgSave.Visible = False
        '    'btSave.Visible = False
        'Else
        txtLinks.Visible = True
        imgSearch.Visible = True
        'btSaveLinks.Visible = True
        'imgSave.Visible = True
        'btSave.Visible = True
        'End If
            pnlDocLinks.Update()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
            End If
        End Try
    End Sub

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

    'Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
    '    DocID = DirectCast(rptitem.FindControl("lnkDocId"), Literal).Text
    '    FVersion = DirectCast(rptitem.FindControl("lVersion"), Literal).Text
    '    FileName = DirectCast(rptitem.FindControl("lFileName"), Literal).Text
    '    Dim refno As String = DirectCast(rptitem.FindControl("lref"), Literal).Text
    '    '--new folder
    '    CreatedDate = DirectCast(rptitem.FindControl("lCDate"), Literal).Text
    '    Dim sYear, sMonth As String
    '    sYear = Year(CDate(CreatedDate)).ToString
    '    sMonth = MonthName(Month(CDate(CreatedDate))).ToString
    '    DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & refno & "\" & DocID & "_" & FVersion & "_" & FileName
    '    If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) AndAlso Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
    '        DocSession.sCurrentFile = DocID & "_" & FVersion & "_" & FileName
    '    End If

    '    DisplayDoc()
    'End Sub

    'Private Sub DisplayDoc()

    '    Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
    '    Dim lsFilePath As String = lsLoc
    '    ucViewer.Visible = False
    '    ucPDFViewer.Visible = False
    '    ucDocViewer.Visible = False
    '    If System.IO.File.Exists(lsFilePath & DocSession.sCurrentFile) Then
    '        Dim lsext As String = System.IO.Path.GetExtension(lsFilePath & DocSession.sCurrentFile).ToLower

    '        If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
    '            'docvw.Visible = False
    '            'pnlImg.Visible = True
    '            ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
    '            'pnlImageDisp.Visible = True
    '            ucViewer.ViewImg()
    '            ucViewer.Visible = True
    '            'docvw.Attributes("src") = "viewDoc.aspx"
    '            'docvw.Visible = False

    '            'pnlDocView.Update()
    '            'pDocView.Update()

    '        ElseIf lsext = ".pdf" Then

    '            'docvw.Attributes("src") = "119_1_blank5.pdf"
    '            ucPDFViewer.ViewPDF()
    '            ucPDFViewer.Visible = True


    '            'pnlDocView.Update()
    '            'pnlImageDisp.Visible = False
    '            'pnlImg.Visible = False
    '            'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
    '            'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

    '        ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

    '            ucDocViewer.ViewDoc()
    '            ucDocViewer.Visible = True

    '            'pnlDocView.Update()
    '            'pnlImageDisp.Visible = False
    '            ' pnlImg.Visible = False
    '            'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('" & "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc & "', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
    '        End If
    '    Else
    '        Message = "File associated with this document does not exists on the server. Please contact administrator."
    '        RaiseEvent e_ShowMessage()

    '    End If
    'End Sub

    Public Function withRecords() As Boolean
        Dim lbret As Boolean = False
        For Each rItems As RepeaterItem In rptDocList.Items
            If DirectCast(rItems.FindControl("ImgSelected"), ImageButton).Visible Then
                lbret = True
                Exit For
            End If
        Next
        Return lbret

    End Function

    Private Sub btSaveLinks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveLinks.Click
        Savelinks()
    End Sub

    Private Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        If rptDocList.Visible AndAlso withRecords() Then
            Savelinks()
            RetAction()
        Else
            Message = "Please retrieve and select a document before clicking Link button."
            RaiseEvent e_ShowMessage()
        End If
    End Sub

    'Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click
    '    If rptDocList.Visible AndAlso withRecords() Then
    '        Savelinks()
    '        RetAction()
    '    Else
    '        Message = "Please retrieve and select a document before clicking Link button."
    '        RaiseEvent e_ShowMessage()
    '    End If

    'End Sub

    Private Sub Savelinks()
        Dim ImgBtnSelected As ImageButton

        Dim oDocLinks As New DocLinks
        Dim lnkId As Literal
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer
        Dim LnkBtn As Literal
        Dim lsDocs As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "xMSP_DOCLINKSADD"
            For Each ri In rptDocList.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    LnkBtn = DirectCast(ri.FindControl("lbDoc"), Literal)
                    lnkId = DirectCast(ri.FindControl("lnkDocId"), Literal)

                    If ImgBtnSelected.Visible AndAlso lnkId.Text <> DocSession.sDocID Then
                        If lsDocs = "" Then
                            lsDocs = LnkBtn.Text
                            lsdesc = "link"
                        Else
                            lsDocs = lsDocs & ", " & LnkBtn.Text
                            lsdesc = "links"
                        End If
                        oDocLinks.pDocId = DocSession.sDocID
                        oDocLinks.pLinkDocId = lnkId.Text
                        oDocLinks.pUserId = DocSession.sUserId
                        oDocLinks.pIpAddress = Request.UserHostAddress
                        oDocLinks.pDocDate = DateTime.Now.ToString
                        oDocLinks.pDocDesc = DocSession.sDocTitle
                        oDocLinks.SaveDocLinks(objCommand)
                        liCtr += 1
                    End If


                End If

            Next

            If liCtr >= 1 Then
                Dim Ohist As New DocHistory
                Ohist.pTask = "Link"
                Ohist.pAction = "Added " & lsdesc & " (" & lsDocs & ")" '-- to document '" & DocSession.sDocTitle & "'"
                Ohist.pUserId = DocSession.sUserId
                Ohist.pIpAddress = Request.UserHostAddress
                Ohist.pDocId = DocSession.sDocID
                Ohist.AddHistory(objCommand)
                'lcheckmsg.Text = "Link has been added successfully."
                'lcheckmsg.CssClass = "msg_green"
                ltr.pTran.Commit()
                'rptDocList.Visible = False
                RetrieveLinks()
            Else
                ltr.pTran.Rollback()
            End If

        Catch ex As Exception

            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Message = "Error occurred while saving links (" & ex.Message & "). Please try again."
            'lcheckmsg.Text = "There's an error while saving the record. Please try again."
            'lcheckmsg.CssClass = "msg_red"
            RaiseEvent e_ShowMessage()
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

    Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        RetAction()
    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lLinkDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        rptDocList.Visible = False
        ucPager.Visible = False
        imgClose.Visible = False
        btSave.Visible = False
    End Sub

    Private Sub rptDocList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDocList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lec As New crypt
            Dim lsLinkDocID As String = DirectCast(e.Item.FindControl("lnkDocId"), Literal).Text
            Dim lsVno As String = DirectCast(e.Item.FindControl("lVersion"), Literal).Text
            Dim lsFileName As String = DirectCast(e.Item.FindControl("lFileName"), Literal).Text
            Dim lsMonth As String = MonthName(Month(CDate(DirectCast(e.Item.FindControl("lCDate"), Literal).Text)))
            Dim lsYear As String = Year(CDate(DirectCast(e.Item.FindControl("lCDate"), Literal).Text))
            Dim hlink As HyperLink = DirectCast(e.Item.FindControl("hlNewTab"), HyperLink)
            Dim lsFile As String = lsYear & "\" & lsMonth & "\" & hlink.Text & "\" & lsLinkDocID & "_" & lsVno & "_" & lsFileName
            If FileExists(lsFile) Then
                hlink.NavigateUrl = "viewfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(lsLinkDocID & "dbm")) & "&att=N&v_no=" & lsVno
            Else
                If (DirectCast(e.Item.FindControl("lIsLocal"), Literal).Text = "1" OrElse DirectCast(e.Item.FindControl("lIsLocal"), Literal).Text = "True") Then
                    If hlink.Visible Then
                        hlink.NavigateUrl = DocSession.LocalPath & "/viewfilelocal.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(lsLinkDocID & "dbm")) & "&att=N&v_no=" & lsVno &
                                    "&r_id=" & Server.UrlEncode(lec.Encrypt(hlink.Text & "dbm")) & "&o=" &
                                    IIf(DocSession.sOwner, "1", "") & "&d=" & IIf(DocSession.sCanDownloadDoc = "True", "1", "") & "&r=" & DocSession.sUserRole & "&u=" & Server.UrlEncode(lec.Encrypt(DocSession.sUserId & "smd"))
                    End If
                Else
                    hlink.NavigateUrl = "viewfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(lsLinkDocID & "dbm")) & "&att=N&v_no=" & lsVno
                End If
            End If

        End If
    End Sub
    Private Function FileExists(lsFile As String) As Boolean

        Try
            'Dim sYear As String = Year(CDate(lCreatedDate.Text))
            'Dim sMonth As String = MonthName(Month(CDate(lCreatedDate.Text)))

            'Dim lsFile As String = sYear & "\" & sMonth & "\" & lrefno.Text & "\" & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text

            If System.IO.File.Exists(DocSession.FileLoc & lsFile) Then
                'If DocSession.sUserRole = "A" Then
                'Dim linfo As New System.IO.FileInfo(DocSession.FileLoc & lsFile)
                'lFileSize.Text = FormatBytes(linfo.Length)
                'lFileSize.Visible = True
                'End If

                Return True
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFile) Then
                'If DocSession.sUserRole = "A" Then
                '    Dim linfo As New System.IO.FileInfo(DocSession.FileLoc2 & lsFile)
                '    lFileSize.Text = FormatBytes(linfo.Length)
                '    lFileSize.Visible = True
                'End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function
    Private Sub rptLinks_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLinks.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
            Else
                'If DocSession.sUserRole = "A" OrElse DocSession.sDeleteDoc = "1" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
                If DocSession.sUserRole = "A" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
                End If
            End If
        End If
    End Sub

    Public Sub CountLinks()
        Dim oNotes As DocLinks
        Dim lcnt As Integer
        Try
            oNotes = New DocLinks
            lcnt = oNotes.CountDocLinks(DocSession.sDocID)
            If lcnt > 0 Then
                hfCount.Value = lcnt.ToString("#,##0")
            Else
                hfCount.Value = ""
            End If

            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CountLinks()
        End If
    End Sub
End Class