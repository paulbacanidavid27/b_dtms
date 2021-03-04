Public Class UserControlUpload
    Inherits System.Web.UI.UserControl

    Public Event e_exec()
    Public Event e_ShowMessage()

    Dim smsg As String
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'DocSession.CheckNextID(DocSession.sOfcCode)
            If DocSession.sOfcCode = "CRD" OrElse DocSession.sUserRole = "A" Then
                hlAgency.NavigateUrl = "agency.aspx"
                hlAgency.Enabled = True
            Else
                hlAgency.Enabled = False
                hlAgency.NavigateUrl = ""
            End If
            ucDocRouting.pDocType2 = ""
            GetDocType()
            GetOfficeCode()
            GetReceipt()
            GetRequestType()
            FileUpload1.ToolTip = "File size of the document to upload should not exceed " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB"
            lAddress.Text = IIf(DocSession.sGroupAddress = "", "General Solano St, San Miguel, Manila", DocSession.sGroupAddress)
            lTitle.Text = IIf(DocSession.ReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", Replace(DocSession.ReceiptReplyName, "\n", "<br />"))
            lTitle2.Text = IIf(DocSession.ReceiptReplyName = "", "Department of Budget and Management", Replace(DocSession.ReceiptReplyName, "\n", " - "))
            If DocSession.HideConfidential Then
                cbConfidential.Visible = False
            Else
                cbConfidential.Visible = True
            End If
            imgLogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "dbm.png", DocSession.GroupLogo)
            imgLogo.DataBind()
            imgBottomLogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "logo.png", "otherlogo.png")
            imgBottomLogo.DataBind()
        End If
    End Sub

    Private Sub FileUpload1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileUpload1.Init

        ucDocRouting.ShowSearch()
        ucDocRouting.HideAdd()
    End Sub

    Private Sub FileUpload1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileUpload1.Load

    End Sub

    Private Sub GetOfficeCode()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        'Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            If DocSession.sUserRole = "A" Then
                ldata = oType.RetrieveOffice
            ElseIf DocSession.sUserRole = "B" Then
                oType.pOfficeCode = "BAC"
                ldata = oType.RetrieveOffice
            Else
                ldata = oType.RetrieveGroupOffice()
            End If

            'lrow = ldata.NewRow
            'lrow(0) = ""
            'ldata.Rows.InsertAt(lrow, 0)
            dlOfficeCode.DataSource = ldata
            dlOfficeCode.DataValueField = "OfficeCode"
            dlOfficeCode.DataTextField = "Description"
            dlOfficeCode.DataBind()
            'If ldata.Rows.Count > 0 AndAlso DocSession.sUserRole = "B" Then
            '    dlOfficeCode.SelectedValue = oType.pOfficeCode
            '    ucDocRouting.SelectDefault(dlOfficeCode.SelectedValue)
            'End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub GetDocType()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocTypes
        Try
            oType = New DocTypes
            oType.pGroupId = DocSession.sUserGroup
            ldata = oType.GetGroupDocType
            'If DocSession.sUserRole <> "B" Then
            lrow = ldata.NewRow
            lrow(0) = ""
            ldata.Rows.InsertAt(lrow, 0)
            'End If

            dlDocType.DataSource = ldata
            dlDocType.DataValueField = "DocType"
            dlDocType.DataTextField = "DocName"
            dlDocType.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub GetRequestType()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocList
        Try
            oType = New DocList
            If Left(DocSession.sOfcCode, 2) = "PS" Then
                oType.pOfficeCode = "PS"
            ElseIf DocSession.sUserRole <> "A" Then
                oType.pOfficeCode = "DBM"
            End If
            ldata = oType.RetrieveRequestType
            If Not ldata Is Nothing Then


                lrow = ldata.NewRow
                lrow(0) = ""
                ldata.Rows.InsertAt(lrow, 0)
                dlRequestType.DataSource = ldata
                dlRequestType.DataValueField = "RequestType"
                dlRequestType.DataTextField = "RequestDescription"
                dlRequestType.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
    Private Sub GetReceipt()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As docReceipts
        Try
            oType = New docReceipts

            ldata = oType.RetrieveReceipts

            lrow = ldata.NewRow
            lrow(0) = "0"
            ldata.Rows.InsertAt(lrow, 0)
            dlReceipt.DataSource = ldata
            dlReceipt.DataValueField = "ReceiptId"
            dlReceipt.DataTextField = "ReceiptDesc"
            dlReceipt.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Sub fErrorMsg(ByVal asParam As String)
        lMsg2.CssClass = "msg_red"
        lMsg2.Text = asParam

    End Sub

    Private Sub fWarningMsg(ByVal asParam As String)
        lMsg2.CssClass = "msg_green"
        lMsg2.Text = asParam

    End Sub

    Private Sub DisplayDoc()

        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim lsFilePath As String = lsLoc
        ucViewer.Visible = False
        ucPDFViewer.Visible = False
        ucDocViewer.Visible = False
        If System.IO.File.Exists(lsFilePath & DocSession.sCurrentFile) Then
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

        End If
    End Sub
    Private Sub fPreview()
        If FileUpload1.HasFile Then
            If ValidFile() Then
                Dim linfo As New System.IO.FileInfo(FileUpload1.PostedFile.FileName)

                'If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                Dim lsGuid As String = Guid.NewGuid().ToString()
                'DocSession.sGuid = lsGuid
                '--new folder
                FileUpload1.SaveAs(DocSession.FileLoc & "previewdocs\" & lsGuid & linfo.Extension.ToLower)
                DocSession.sDocFileName = "previewdocs\" & lsGuid & linfo.Extension.ToLower
                DocSession.sCurrentFile = "previewdocs\" & lsGuid & linfo.Extension.ToLower

                'FileUpload1.SaveAs(DocSession.FileLoc & lsGuid & linfo.Extension.ToLower)
                'DocSession.sDocFileName = lsGuid & linfo.Extension.ToLower
                'DocSession.sCurrentFile = lsGuid & linfo.Extension.ToLower
                hfFileUploaded.Value = DocSession.sDocFileName
                lbFileUploaded.Text = FileUpload1.FileName
                hfFileSize.Value = FileUpload1.PostedFile.ContentLength.ToString
                'lbFileUploaded.Visible = True
                'imgDeleteFile.Visible = True
                'lbFTBU.Visible = True
                pnlUploadFile.Visible = True
                fileup.Update()
                'DisplayDoc()
                'Dim csname1 As String = "viewDocument"
                'Dim cstype As Type = Me.GetType()
                'Dim cs As ClientScriptManager = Page.ClientScript

                '' Check to see if the startup script is already registered.
                'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

                '    Dim Srpt As String = "window.open('viewDoc.aspx');"
                '    'Dim cstext1 As String = "alert('pogi');"
                '    cs.RegisterStartupScript(cstype, csname1, Srpt, True)

                'End If
                'Else
                '   fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff, .tif files are accepted.")
                'pnlMsg.Update()
                'Exit Sub
                'End If
            End If
        End If
    End Sub
    Private Function ValidFile() As Boolean
        If FileUpload1.HasFile Then


            If Len(FileUpload1.FileName) > 200 Then
                fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                Return False
            End If
            Dim linfo As New System.IO.FileInfo(FileUpload1.PostedFile.FileName)
            If DocSession.sUserRole <> "B" Then
                If FileUpload1.PostedFile.ContentLength > DocSession.MaxFileSize Then
                    fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB is allowed.")
                    Return False
                End If
            Else
                If FileUpload1.PostedFile.ContentLength > DocSession.ZipMaxFileSize Then
                    fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.ZipMaxFileSize / 1000000)) & "MB is allowed.")
                    Return False
                End If
            End If

            If Len(FileUpload1.FileName) > 200 Then
                fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                Return False
            End If

            If DocSession.sUserRole = "B" Then
                If linfo.Extension.ToLower() = ".zip" OrElse linfo.Extension.ToLower() = ".rar" Then
                Else
                    fErrorMsg("** Invalid file. Only .zip or .rar file is accepted.")
                    Return False
                End If
            Else
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then

                Else
                    fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted.")
                    Return False
                End If

            End If

        End If

        Return True
    End Function
    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim lbAdd1 As Boolean = False
        Dim lbRename As Boolean = False
        Dim lbOk As Boolean = False
        Dim srefno, srefnoyear As String
        fErrorMsg("")
        'If lbFileUploaded.Text.Trim <> "" Then
        If pnlUploadFile.Visible Then
            lbRename = True
        End If
        Try
            If Not lbRename AndAlso Not FileUpload1.HasFile Then
                'fErrorMsg("** File is required.") 'File is not required anymore as of discussion with chris last 10/22/2013
                'Exit Sub
                lbOk = True

            ElseIf FileUpload1.HasFile Then
                If Not ValidFile() Then
                    Exit Sub
                Else
                    lbAdd1 = True
                    hfFileSize.Value = FileUpload1.PostedFile.ContentLength.ToString
                End If
            Else
                lbOk = True
            End If




            'DocSession.CheckNextID(DocSession.sOfcCode)

            If tbDocTitle.Text.Trim = "" Then
                fErrorMsg("** Title is a required field.")
                'lMsg2.Text = "** Title is a required field."
                'lMsg2.CssClass = "msg_red"
                'pnlMsg.Update()
                fPreview()
                Exit Sub
            ElseIf dlDocType.SelectedValue = "" Then
                fErrorMsg("** Document Type is a required field.")
                fPreview()
                'pnlMsg.Update()
                Exit Sub

                'ElseIf tbNoCopies.Text.Trim = "" Then
                '    fErrorMsg("** No of Copies a required field.")
                '    'lMsg2.Text = "** Title is a required field."
                '    'lMsg2.CssClass = "msg_red"
                '    'pnlMsg.Update()
                '    fPreview()
                '    Exit Sub
                'ElseIf tbNoPages.Text.Trim = "" Then
                '    fErrorMsg("** No of Pages is a required field.")
                '    'lMsg2.Text = "** Title is a required field."
                '    'lMsg2.CssClass = "msg_red"
                '    'pnlMsg.Update()
                '    fPreview()
                '    Exit Sub
                'ElseIf dlReceipt.SelectedValue = "0" Then
                '    fErrorMsg("** Manner of Receipt is a required field.")
                '    fPreview()
                '    'pnlMsg.Update()
                '    Exit Sub
            ElseIf tbNoCopies.Text.Trim <> "" AndAlso Not IsNumeric(tbNoCopies.Text) Then
                fErrorMsg("** Invalid No of Copies.")
                fPreview()
                'pnlMsg.Update()
                Exit Sub
            ElseIf tbNoPages.Text.Trim <> "" AndAlso Not IsNumeric(tbNoPages.Text) Then
                fErrorMsg("** Invalid No of Pages.")
                fPreview()
                'pnlMsg.Update()
                Exit Sub
            ElseIf dlOfficeCode.SelectedValue.Trim = "" Then
                fErrorMsg("** Office Code is a required for generating the document reference no. Please select Office code from the list.")
                fPreview()
                Exit Sub
            ElseIf Not ucDocRouting.WithApprovers AndAlso Not cbArchive.Checked Then
                fErrorMsg("** Please select one user for document routing.")
                'lMsg2.Text = "** Title is a required field."
                'lMsg2.CssClass = "msg_red"
                'pnlMsg.Update()
                fPreview()
                Exit Sub
                'ElseIf Not ucDocRouting.ValidateDueDate() Then
                '   fErrorMsg("** " & ucDocRouting.ValidateMessage)
                '  fPreview()
                ' Exit Sub
            ElseIf dlDocType.SelectedValue <> "" Then
                Dim oType As New DocTypes
                oType.pDocType = dlDocType.SelectedValue
                Dim lbFileReq As Boolean = oType.IsFileRequired
                If lbFileReq AndAlso Not (lbAdd1 OrElse lbRename) Then
                    fErrorMsg("** File is required for the selected document type.")
                    fPreview()
                    'pnlMsg.Update()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            fErrorMsg("Error while uploading your document ( " & ex.Message & " ). Please try again. ")
            Exit Sub
        End Try
        Dim oList As New DocList
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liRet As String
        Dim lsFile As String
        Dim lsSeqNo As String
        Dim lDataEmail As DataTable
        Dim bCommit As Boolean = False

        Try
            'oList.ExistsInInbox()



            Dim liStat As Integer
            If lbOk OrElse lbAdd1 OrElse lbRename Then
                If ucDocRouting.WithApprovers Then
                    liStat = 2 'Pending
                Else
                    If cbArchive.Checked Then
                        liStat = 8
                    Else
                        fErrorMsg("** Please select one user for document routing.")
                        fPreview()
                        Exit Sub
                    End If

                End If
                If lbAdd1 Then
                    lsFile = FileUpload1.FileName
                ElseIf lbRename Then
                    lsFile = lbFileUploaded.Text
                Else
                    lsFile = ""
                End If
                ucDocRouting.GetApprovers()
                ucDocRouting.GetCCs()

                liRet = DocSession.getNextID("DocId")
                lsSeqNo = DocSession.getNextID("SeqNo")
                'srefno = DocSession.getNextID(dlOfficeCode.SelectedValue)
                srefno = DocSession.getNextID("refnodoc")
                srefnoyear = DocSession.CheckNextID("refnoyear")
                If srefnoyear = 0 Then
                    srefnoyear = Year(Date.Now).ToString
                    DocSession.AddNextID("refnoyear", srefnoyear)
                End If
                If Year(Date.Now) <> CInt(srefnoyear) Then
                    srefnoyear = Year(Date.Now).ToString
                    DocSession.UpdateNextID("refnoyear", srefnoyear)
                    DocSession.UpdateNextID("refnodoc", "1")
                    srefno = "1"
                End If
                srefno = DocSession.GenerateRefNo(dlOfficeCode.SelectedValue.Trim, srefno) 'Year(DateTime.Now).ToString & "-" & dlOfficeCode.SelectedValue.Trim & "-" & Right("00000" & srefno, 5)

                'lsFile = lsFile.Replace(" ", "_")
                lsFile = lsFile.Replace(",", "")
                lsFile = lsFile.Replace("&", "n")

                Dim oIndex As New DocIndex
                oIndex.pApp = ucDocRouting.pApprovers
                oIndex.pCCTo = ucDocRouting.pCCs
                oIndex.pIPAddress = Request.UserHostAddress
                oIndex.pDocId = liRet
                oIndex.pFileName = lsFile.ToLower
                oIndex.pTitle = tbDocTitle.Text
                oIndex.pDocType = dlDocType.SelectedValue
                oIndex.pRequestType = dlRequestType.SelectedValue
                oIndex.pStat = CStr(liStat)
                oIndex.pCreatedDate = DateTime.Now.ToString
                oIndex.pRefNo = srefno
                oIndex.pOfcCode = dlOfficeCode.SelectedValue
                oIndex.pUploaderOfc = DocSession.sOfcCode
                oIndex.pUploaderGrp = DocSession.sUserGroup

                oIndex.pCopies = IIf(tbNoCopies.Text.Trim = "", "0", tbNoCopies.Text.Trim)
                oIndex.pPages = IIf(tbNoPages.Text.Trim = "", "0", tbNoPages.Text.Trim)
                oIndex.pMannerReceipt = dlReceipt.SelectedValue
                oIndex.pSeqNo = lsSeqNo
                oIndex.pDocSender = tbDocSender.Text.Trim
                oIndex.pReturnCard = tbRetCard.Text.Trim
                'internal/remarks
                oIndex.pInternal = IIf(rbInt.Checked, "1", "0")
                oIndex.pAddRemarks = tbNotes.Text
                oIndex.pFileSize = hfFileSize.Value
                oIndex.pConfidential = IIf(cbConfidential.Checked, "1", "0")
                If oIndex.pFileSize = "" Then
                    oIndex.pFileSize = "0"
                End If
                'If tbNotes.Text <> "" Then
                '    pNotes.Visible = True
                '    oIndex.pAddRemarks = tbNotes.Text
                'Else
                '    pNotes.Visible = False
                'End If


                If lbAdd1 Then
                    Dim sYear As String
                    Dim sMonth As String
                    sYear = Year(CDate(oIndex.pCreatedDate)).ToString()
                    sMonth = MonthName(Month(CDate(oIndex.pCreatedDate)))
                    If Not System.IO.Directory.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno) Then
                        System.IO.Directory.CreateDirectory(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno)
                    End If
                    FileUpload1.SaveAs(DocSession.FileLoc & sYear & "\" & sMonth & "\" & "\" & srefno & "\" & liRet & "_1_" & lsFile)

                ElseIf lbRename Then
                    If System.IO.File.Exists(DocSession.FileLoc & hfFileUploaded.Value) Then
                        Dim sYear As String
                        Dim sMonth As String
                        sYear = Year(CDate(oIndex.pCreatedDate)).ToString()
                        sMonth = MonthName(Month(CDate(oIndex.pCreatedDate)))
                        If Not System.IO.Directory.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno) Then
                            System.IO.Directory.CreateDirectory(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno)
                        End If
                        System.IO.File.Move(DocSession.FileLoc & hfFileUploaded.Value, DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno & "\" & liRet & "_1_" & lsFile)
                    End If
                End If

                ltr = New DbTran
                objCommand = New clsSqlConn(ltr.pTran)
                'lMsg2.Text = "sdl"
                oIndex.SaveDocList(objCommand)
                'lMsg2.Text = "sdlfv"
                oIndex.SaveDocListFileVersion(objCommand)

                oList.pDocId = liRet
                oList.pUserId = DocSession.sUserId
                oList.pSeqNo = lsSeqNo
                'lMsg2.Text = "ati" & liRet.ToString
                oList.AddToInbox(objCommand) ', oList.pExistsInInbox)
                'lMsg2.Text = "ati1"
                If tbNotes.Text.Trim <> "" Then
                    Dim oNotes As New DocNotes
                    'lMsg2.Text = "ati3"
                    oNotes.pDocId = liRet
                    oNotes.pUserId = DocSession.sUserId
                    oNotes.pIpAddress = Request.UserHostAddress
                    oNotes.pNote = Left(tbNotes.Text.Trim, 1000)
                    oNotes.pNoteId = DocSession.getNextID("doc_notes")
                    hfNoteId.Value = oNotes.pNoteId
                    'lMsg2.Text = "sdn"
                    oNotes.SaveDocNote(objCommand)
                End If
                'lMsg2.Text = "ati4"
                ucDocRouting.pDocId = liRet
                ucDocRouting.pDocTitle = tbDocTitle.Text
                ucDocRouting.pAuthor = DocSession.sUserName
                ucDocRouting.pSeqNo = lsSeqNo
                ucDocRouting.pRefno = srefno
                'lMsg2.Text = "sa"
                lDataEmail = ucDocRouting.SaveApprovers(objCommand)

                di.pAddHistory = False
                'lMsg2.Text = "si"
                di.SaveIndex(liRet, dlDocType.SelectedValue, objCommand)

                Dim ohist As New DocHistory
                Dim lsAction As String
                If lsFile.Trim <> "" Then
                    lsAction = "Uploaded file " & lsFile & "."
                Else
                    lsAction = "No file uploaded."
                End If

                If ucDocRouting.pApprovers <> "" Then
                    lsAction = lsAction & " Routed document to " & ucDocRouting.pApprovers & ". "
                End If
                ohist.pAction = lsAction
                ohist.pDocId = liRet
                DocSession.sDocID = liRet

                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Upload"
                ohist.pUserId = DocSession.sUserId
                'lMsg2.Text = "ah"
                ohist.AddHistory(objCommand)  'george 11/3

                ltr.pTran.Commit()
                bCommit = True

                lackDate.Text = CDate(oIndex.pCreatedDate).ToLongDateString & " " & CDate(oIndex.pCreatedDate).ToLongTimeString
                lackTitle.Text = tbDocTitle.Text

                lCreatedBy.Text = DocSession.sUserName

                lRoutedTo.Text = ucDocRouting.pApprovers
                lCarbon.Text = ucDocRouting.pCCs


                lackFilename.Text = srefno
                lrefno.Text = srefno

                'lSender.Text = tbDocSender.Text

                Dim lsm As String = ""
                If tbNoCopies.Text.Trim <> "" Then
                    If CInt(tbNoCopies.Text.Trim) = 1 Then
                        lsm = " 1 copy "
                    ElseIf CInt(tbNoCopies.Text.Trim) > 1 Then
                        lsm = tbNoCopies.Text.Trim & " copies "
                    End If
                End If
                If tbNoPages.Text.Trim <> "" Then


                    If CInt(tbNoPages.Text.Trim) = 1 Then
                        If lsm <> "" Then
                            lsm = lsm & " and 1 page "
                        Else
                            lsm = " 1 page "
                        End If
                    ElseIf CInt(tbNoPages.Text.Trim) > 1 Then
                        If lsm <> "" Then
                            lsm = lsm & " and " & tbNoPages.Text.Trim & " pages "
                        Else
                            lsm = tbNoPages.Text.Trim & " pages "
                        End If
                    End If
                End If
                If lsm = "" Then
                    lsm = "N/A"
                End If

                lPages.Text = lsm
                'lReceipt.Text = dlReceipt.SelectedItem.Text
                lSender.Text = tbDocSender.Text

                If tbNotes.Text <> "" Then
                    pNotes.Visible = True
                    lRemarks.Text = tbNotes.Text
                Else
                    lRemarks.Text = ""
                    pNotes.Visible = False
                End If

                'PrintRoute()

                hfDocId.Value = liRet
                fileup.Update()
            End If

            If liStat = 6 Then
                lMsg2.Text = ""
                'msg.Text = "Document " & tbDocTitle.Text & " has been created successfully."
            Else
                lMsg2.Text = ""
                'msg.Text = "Document " & tbDocTitle.Text & " has been created and will undergo approval to the selected users. Email notification has been sent to the Approvers."
            End If
            'DocSession.sDocType = ""







            'RetrieveDocs()
            pAddDoc.Visible = False
            If lRoutedTo.Text.Trim <> "" Then
                ackreceipt.Visible = True
                btPrint.Visible = True
            Else
                pWarning.Visible = True
                btPrint.Visible = False
            End If
            If DocSession.sUserRole = "B" Then
                btAdd.Visible = False
                btRoute.Visible = False
                btBack.Visible = False
            End If
            pMsg.Visible = True
            pnlMsg.Visible = True


            If Not cbArchive.Checked AndAlso DocSession.sSendEmail Then
                If lDataEmail.Rows.Count > 0 Then
                    ucDocRouting.pDocTitle = tbDocTitle.Text.Trim
                    ucDocRouting.pRefno = srefno
                    ucDocRouting.EmailApprovers(lDataEmail)
                    ShowMessage("Document " & tbDocTitle.Text & " has been created successfully. Email notification has been sent to the point person.")
                Else
                    ShowMessage("Document " & tbDocTitle.Text & " has been created successfully.")
                End If
            Else
                ShowMessage("Document " & tbDocTitle.Text & " has been created successfully.")
            End If
            pnlMsg.Update()
        Catch ex As MailException
            ShowMessage(ex.Message)
            'RaiseEvent e_ShowMessage()
        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If

            ShowMessage("Warning: An error occurred while uploading your document ('" & ex.Message & "'). Please try again.")


        Finally
            If Not lDataEmail Is Nothing Then
                lDataEmail.Dispose()
                lDataEmail = Nothing
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
    Private Sub PrintRoute()
        uReceiptRoute.pAuthor = DocSession.sUserId
        uReceiptRoute.pCreatedBy = DocSession.sUserName
        uReceiptRoute.pCreatedDate = lackDate.Text
        uReceiptRoute.pDocTitle = lackTitle.Text
        uReceiptRoute.pRefNo = lrefno.Text
        uReceiptRoute.pSender = lSender.Text
        uReceiptRoute.pCopiesPages = GenerateCopies(tbNoCopies.Text, tbNoPages.Text) 'ok
        uReceiptRoute.pCarbonDisplay = True
        uReceiptRoute.pRoutedToDisplay = True
        uReceiptRoute.pNotes = lRemarks.Text
        uReceiptRoute.pCarbon = lCarbon.Text 'ok
        uReceiptRoute.pRoutedTo = lRoutedTo.Text ' ok
        uReceiptRoute.pRemarks = System.Configuration.ConfigurationManager.AppSettings("RoutedToRemarks").Trim 'IIf(ucDocRouting.pRemarks = "", "FAA", ucDocRouting.pRemarks)
        uReceiptRoute.SetInfo()
        uReceiptRoute.ShowReceipt()
        uReceiptRoute.Visible = True
        uReceiptRoute.ShowRemarks()

        'uReceiptRoute.pCopiesPages = lsm
        'uReceiptRoute.pRefNo = lrefno.Text
        'uReceiptRoute.pDocTitle = lackTitle.Text
        'uReceiptRoute.pCreatedDate = lackDate.Text
        'uReceiptRoute.pCreatedBy = lCreatedBy.Text
        'uReceiptRoute.pRoutedTo = lRoutedTo.Text
        'uReceiptRoute.pCarbon = lCarbon.Text
        'uReceiptRoute.pSender = lSender.Text

        'uReceiptRoute.pRemarks = tbNotes.Text



    End Sub
    Private Sub ShowMessage(ByVal asMsg As String)
        Message = asMsg
        RaiseEvent e_ShowMessage()
    End Sub

    Private Function GenerateCopies(ByVal asCopies As String, ByVal asPages As String) As String
        Dim lsCopies As String = ""

        If Not IsDBNull(asCopies) Then
            If IsNumeric(asCopies) Then
                If CInt(asCopies) > 1 Then
                    lsCopies = asCopies & " copies "
                ElseIf CInt(asCopies) = 1 Then
                    lsCopies = asCopies & " copy "
                End If
            End If
        End If

        If Not IsDBNull(asPages) Then
            If IsNumeric(asPages) Then
                If CInt(asPages) > 1 Then
                    If lsCopies.Trim <> "" Then
                        lsCopies = lsCopies & " and " & asPages & " pages "
                    Else
                        lsCopies = asPages & " pages "
                    End If
                ElseIf CInt(asPages) = 1 Then
                    If lsCopies.Trim <> "" Then
                        lsCopies = lsCopies & " and " & asPages & " page "
                    Else
                        lsCopies = asPages & " page "
                    End If

                End If
            End If
        End If

        If lsCopies = "" Then
            lsCopies = "N/A"
        End If
        Return lsCopies
    End Function
    'Private Sub SaveDocList(ByVal objCommand As clsSqlConn, ByVal asDocId As String, ByVal asDocType As String, ByVal asTitle As String, ByVal asFileName As String, ByVal aiStat As Integer, ByVal asApp As String)

    '    Try

    '        objCommand.pCommandType = CommandType.Text
    '        'If lAction.Text = "Add" Then
    '        'objCommand.pCommandText = lssql
    '        'Else
    '        'objCommand.pCommandText = "xMSP_DOCLISTADD"
    '        'End If
    '        Dim lssql As String
    '        lssql = "INSERT INTO DocList " & _
    '       "(DocId,DocType,Title,FileName,Location,CreatedDate,CreatedBy,ModifiedBy " & _
    '       ",ModifiedDate,IsBeingModified,StatusId,RoutedTo,IPAddress,FileVersion,InternalDoc,AdditionalInfo) " & _
    '        "VALUES " & _
    '       "(" & asDocId & "," & _
    '       "'" & asDocType & "'," & _
    '       "'" & Replace(asTitle, "'", "''") & "'," & _
    '       "'" & Replace(asFileName, "'", "''") & "'," & _
    '       "''," & _
    '       "'" & DateTime.Now.ToString & "'," & _
    '       "'" & DocSession.sUserId & "'," & _
    '       "'" & DocSession.sUserId & "'," & _
    '        "'" & DateTime.Now.ToString & "'," & _
    '           "0," & _
    '       "" & CStr(aiStat) & "," & _
    '       "'" & Replace(asApp, "'", "''") & "'," & _
    '       "'" & Request.UserHostAddress & "',1" & IIf(rbInt.Checked, "1", "0") & ")"
    '        'objCommand.ClearParameter()
    '        'objCommand.ParametersAddWithValue("@DocType", asDocType)
    '        'objCommand.ParametersAddWithValue("@Title", asTitle)
    '        'objCommand.ParametersAddWithValue("@FileName", asFileName)
    '        'objCommand.ParametersAddWithValue("@Location", "")
    '        'objCommand.ParametersAddWithValue("@UserID", DocSession.sUserId)
    '        'objCommand.ParametersAddWithValue("@StatusID", aiStat)
    '        'objCommand.ParametersAddWithValue("@IPAddress", Request.UserHostAddress)
    '        'objCommand.ParametersReturnValue()
    '        ''Dim retval As SqlParameter = objCommand.RetValue
    '        ''Dim retval As OracleClient.OracleParameter = objCommand.RetValue
    '        'Dim retval As New DbParam
    '        'retval.pParam = objCommand.RetValue
    '        objCommand.pCommandText = lssql
    '        objCommand.ExecTranNonQuery()

    '        'Return retval.pParam.Value



    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)

    '        lMsg2.Text = "** Error while saving (" & ex.Message & "). Please try again"

    '    Finally


    '    End Try
    'End Sub

    Private Sub SaveDocListFileVersion(ByVal objCommand As clsSqlConn, ByVal asDocId As String, ByVal asFN As String)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            '"xMSP_DOCLISTADD"
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "INSERT INTO DocFileVersion(docId,docVersion,uploadedDate,uploadedby,FileName,comments) " &
    " VALUES (" & asDocId & ",1," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",'" & DocSession.sUserId & "','" & Replace(asFN, "'", "''") & "','Initial Creation')"

            objCommand.pCommandText = lssql
            objCommand.ExecTranNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            'Throw New Exception(ex.Message)

            lMsg2.Text = "** Error while saving (" & ex.Message & "). Please try again"

        Finally


        End Try
    End Sub

    Private Sub lbFileUploaded_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFileUploaded.Click
        DocSession.sDocFileName = hfFileUploaded.Value
        DisplayDoc()
        'Dim csname1 As String = "viewDocument"
        'Dim cstype As Type = Me.GetType()
        'Dim cs As ClientScriptManager = Page.ClientScript

        '' Check to see if the startup script is already registered.
        'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

        '    Dim Srpt As String = "window.open('viewDoc.aspx');"
        '    'Dim cstext1 As String = "alert('pogi');"
        '    cs.RegisterStartupScript(cstype, csname1, Srpt, True)

        'End If
    End Sub

    Private Sub imgHelp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHelp.Click
        ucDocRouting.ShowRouteInstruction()

    End Sub

    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ShowResult()
    End Sub

    Private Sub ShowResult()
        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()


        'pAddDoc.Visible = False
        ''pnlAddDoc.Update()

        'pDeleteDoc.Visible = False
        'pnlDeleteDoc.Update()

        'pRepeater.Visible = True
        'pnlRepeater.Update()

        'imgAddDoc.Visible = True
        'pUpload1.Visible = False
        'pUpload2.Visible = False
        Me.Visible = False
        RaiseEvent e_exec()
        'Master.ShowImageDocument = False
        'pnlPage.Update()
    End Sub

    'Private Sub btCloseAddScreen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCloseAddScreen.Click
    '    ShowResult()
    'End Sub

    Private Sub btAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btAdd.Click

        ShowAdd()
    End Sub

    Private Sub dlDocType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlDocType.SelectedIndexChanged
        'DocSession.sDocType = 
        'ucDocRouting.pOfficeCode = dlOfficeCode.SelectedValue
        ucDocRouting.pDocType2 = dlDocType.SelectedValue
        di.pDIS = True
        di.RetrieveDocIndex(dlDocType.SelectedValue)
        SetTitle()
        ucDocRouting.ResetRouting()
        ucDocRouting.SelectDefault(dlOfficeCode.SelectedValue)
        dlRequestType.Focus()
        pnDR.Update()

    End Sub

    Private Sub btPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPreview.Click
        Try
            fErrorMsg("")
            If Not pnlUploadFile.Visible AndAlso Not FileUpload1.HasFile Then
                fErrorMsg("** Please select a file before clicking on Preview button.")
            ElseIf ValidFile() Then
                'If Len(FileUpload1.FileName) > 200 Then
                '    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                '    Exit Sub
                'End If

                Dim linfo As New System.IO.FileInfo(FileUpload1.PostedFile.FileName)

                'If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then

                    Dim lsGuid As String = Guid.NewGuid().ToString()
                    'DocSession.sGuid = lsGuid
                    FileUpload1.SaveAs(DocSession.FileLoc & "previewdocs\" & lsGuid & linfo.Extension.ToLower)
                    DocSession.sDocFileName = "previewdocs\" & lsGuid & linfo.Extension.ToLower
                    DocSession.sCurrentFile = "previewdocs\" & lsGuid & linfo.Extension.ToLower

                    'FileUpload1.SaveAs(DocSession.FileLoc & lsGuid & linfo.Extension.ToLower)
                    'DocSession.sDocFileName = lsGuid & linfo.Extension.ToLower
                    'DocSession.sCurrentFile = lsGuid & linfo.Extension.ToLower

                    hfFileUploaded.Value = DocSession.sDocFileName
                    lbFileUploaded.Text = FileUpload1.FileName
                    SetTitle()
                    'lbFileUploaded.Visible = True
                    'imgDeleteFile.Visible = True
                    'lbFTBU.Visible = True
                    pnlUploadFile.Visible = True
                    fileup.Update()
                    DisplayDoc()
                    'Dim csname1 As String = "viewDocument"
                    'Dim cstype As Type = Me.GetType()
                    'Dim cs As ClientScriptManager = Page.ClientScript

                    '' Check to see if the startup script is already registered.
                    'If (Not cs.IsStartupScriptRegistered(cstype, csname1)) Then

                    '    Dim Srpt As String = "window.open('viewDoc.aspx');"
                    '    'Dim cstext1 As String = "alert('pogi');"
                    '    cs.RegisterStartupScript(cstype, csname1, Srpt, True)

                    'End If
                    hfFileSize.Value = FileUpload1.PostedFile.ContentLength.ToString
                Else

                    'fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff, .tif files are accepted.")
                    fErrorMsg("** Only the .pdf, .gif, .png, .jpeg, .jpg, .tiff, .tif files can be previewed.")

                    'pnlMsg.Update()
                    Exit Sub

                End If
            ElseIf pnlUploadFile.Visible Then
                DisplayDoc()

            End If

        Catch ex As Exception
            fErrorMsg(ex.Message)
        End Try
    End Sub
    Private Sub SetTitle()
        If tbDocTitle.Text.Trim = "" Then
            tbDocTitle.Text = FileUpload1.FileName.Split(".")(0).ToString.ToUpper
        End If
    End Sub
    Public Sub ShowAdd()
        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pDeleteDoc.Visible = False
        'pnlDeleteDoc.Update()
        pWarning.Visible = False
        ackreceipt.Visible = False
        tbDocTitle.Text = ""
        pUpload1.Visible = True
        pUpload2.Visible = True
        'lbFTBU.Visible = False
        'lbFileUploaded.Visible = False
        'imgDeleteFile.Visible = False
        pnlUploadFile.Visible = False
        hfFileUploaded.Value = ""
        ucDocRouting.pDocId = ""
        fileup.Update()
        dlDocType.Enabled = True
        FileUpload1.Enabled = True
        pAddDoc.Visible = True
        pnlMsg.Visible = False
        pnlMsg.Update()
        pMsg.Visible = False
        If DocSession.sUserRole <> "B" Then
            dlDocType.SelectedValue = ""
        End If

        'dlOfficeCode.SelectedValue = ""
        di.rIndex.Visible = False
        'di.Visible = False
        DocSession.sDocType = ""

        ucDocRouting.ResetRouting()
        lMsg2.Text = ""
        'plButtons.Update()
        btUpdate.Visible = False
        btSave.Visible = True
        'removeScrollBar()
        ucPDFViewer.Visible = False
        ucDocViewer.Visible = False
        ucViewer.Visible = False
    End Sub

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        ShowResult()
    End Sub

    Private Sub imgCloseAck_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseAck.Click
        ShowResult()
    End Sub

    Private Sub btView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btView.Click
        DocSession.sDocID = hfDocId.Value
        Response.Redirect("view.aspx")
    End Sub

    Private Sub btBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBack.Click
        pMsg.Visible = False
        pAddDoc.Visible = True
        ackreceipt.Visible = False
        btSave.Visible = False
        btCancel.Visible = False
        btUpdate.Visible = True
        dlDocType.Enabled = False
        FileUpload1.Enabled = False
        pnlMsg.Update()
    End Sub

    Private Sub btUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpdate.Click

        fErrorMsg("")
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn
        Dim liRet As String
        Dim lsFile As String
        Dim oList As DocList
        Dim ldataEmail As DataTable
        Dim oIndex As DocIndex
        Dim bCommit As Boolean = False
        Try
            'ucDocRouting.ValidateDueDate()

            If ucDocRouting.ValidateMessage <> "" Then
                'ltr.pTran.Rollback()
            Else
                oList = New DocList
                ltr = New DbTran
                'oList.ExistsInInbox()
                objCommand = New clsSqlConn(ltr.pTran)


                ucDocRouting.GetApprovers()

                oIndex = New DocIndex
                oIndex.pApp = ucDocRouting.pApprovers

                oIndex.pDocId = hfDocId.Value 'DocSession.sDocID
                oIndex.pDocSender = tbDocSender.Text
                oIndex.pTitle = tbDocTitle.Text
                oIndex.pCopies = IIf(tbNoCopies.Text.Trim = "", "0", tbNoCopies.Text.Trim)
                oIndex.pPages = IIf(tbNoPages.Text.Trim = "", "0", tbNoPages.Text.Trim)
                oIndex.pRequestType = dlRequestType.SelectedValue
                oIndex.pMannerReceipt = dlReceipt.SelectedValue
                'internal/remarks
                oIndex.pInternal = IIf(rbInt.Checked, "1", "0")
                oIndex.pAddRemarks = ""

                oIndex.UpdateDocList(objCommand)

                If tbNotes.Text.Trim <> "" Then
                    Dim oNotes As New DocNotes

                    oNotes.pDocId = liRet
                    oNotes.pUserId = DocSession.sUserId
                    oNotes.pIpAddress = Request.UserHostAddress
                    oNotes.pNote = Left(tbNotes.Text.Trim, 1000)
                    oNotes.pNoteId = hfNoteId.Value
                    oNotes.UpdateDocNotes(objCommand)
                End If

                ucDocRouting.pDocId = hfDocId.Value 'DocSession.sDocID
                ucDocRouting.DeleteRouting(objCommand)
                ucDocRouting.pDocTitle = tbDocTitle.Text
                ucDocRouting.pAuthor = DocSession.sUserName
                ucDocRouting.pSendEmail = False
                ldataEmail = ucDocRouting.SaveApprovers(objCommand)
                di.pAddHistory = False
                'di.SaveIndex(DocSession.sDocID, DocSession.sDocType, objCommand)
                di.SaveIndex(hfDocId.Value, DocSession.sDocType, objCommand)
                'Dim ohist As New DocHistory
                'Dim lsAction As String = ""
                'If ucDocRouting.pApprovers <> "" Then
                '    lsAction = "Changed routing to " & ucDocRouting.pApprovers & ". "
                'End If
                'ohist.pAction = lsAction
                'ohist.pDocId = DocSession.sDocID
                'ohist.pIpAddress = Request.UserHostAddress
                'ohist.pTask = "Upload"
                'ohist.pUserId = DocSession.sUserId
                'ohist.AddHistory()
                'di.SaveIndex(liRet, dlDocType.SelectedValue, objCommand)


                ltr.pTran.Commit()
                bCommit = True

                'RetrieveDocs()
                pAddDoc.Visible = False
                lRoutedTo.Text = ucDocRouting.pApprovers
                lCarbon.Text = ucDocRouting.pCCs
                Dim lsm As String = ""

                If tbNoCopies.Text.Trim <> "" Then


                    If CInt(tbNoCopies.Text.Trim) = 1 Then
                        lsm = " 1 copy "
                    ElseIf CInt(tbNoCopies.Text.Trim) > 1 Then
                        lsm = tbNoCopies.Text.Trim & " copies "
                    End If
                End If
                If tbNoPages.Text.Trim <> "" Then


                    If CInt(tbNoPages.Text.Trim) = 1 Then
                        If lsm <> "" Then
                            lsm = lsm & " and 1 page "
                        Else
                            lsm = " 1 page "
                        End If
                    ElseIf CInt(tbNoPages.Text.Trim) > 1 Then
                        If lsm <> "" Then
                            lsm = lsm & " and " & tbNoPages.Text.Trim & " pages "
                        Else
                            lsm = tbNoPages.Text.Trim & " pages "
                        End If
                    End If
                End If

                If lsm = "" Then
                    lsm = "N/A"
                End If

                lPages.Text = lsm
                'lReceipt.Text = dlReceipt.SelectedItem.Text
                lSender.Text = tbDocSender.Text
                'internal/remarks
                If tbNotes.Text <> "" Then
                    pNotes.Visible = True
                    lRemarks.Text = tbNotes.Text
                Else
                    lRemarks.Text = ""
                    pNotes.Visible = False
                End If

                If lRoutedTo.Text.Trim <> "" Then

                    ackreceipt.Visible = True
                    btPrint.Visible = True
                    lackTitle.Text = tbDocTitle.Text

                Else
                    pWarning.Visible = True
                    btPrint.Visible = False
                End If
                pMsg.Visible = True
                pnlMsg.Visible = True
            End If
            If ldataEmail Is Nothing AndAlso ldataEmail.Rows.Count > 0 Then
                ucDocRouting.EmailApprovers(ldataEmail)
            End If
            pnlMsg.Update()
        Catch ex As MailException
            Message = ex.Message
            RaiseEvent e_ShowMessage()
        Catch ex As Exception
            If bCommit = False AndAlso Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            fErrorMsg("Warning: An error occurred while uploading your document ('" & ex.Message & "'). Please try again.")
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

    Private Sub imgDeleteFile_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteFile.Click
        pnlUploadFile.Visible = False
        fileup.Update()
    End Sub

    Private Sub btPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPrint.Click
        Dim oDoc As DocList
        Try
            oDoc = New DocList
            oDoc.pUserId = DocSession.sUserId
            oDoc.pDocId = hfDocId.Value
            oDoc.PrintDoc()
        Catch ex As Exception
            fErrorMsg(ex.Message)
        End Try

    End Sub

    Private Sub dlOfficeCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlOfficeCode.SelectedIndexChanged
        ucDocRouting.SelectDefault(dlOfficeCode.SelectedValue)
    End Sub

    Private Sub dlReceipt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlReceipt.SelectedIndexChanged

        If dlReceipt.SelectedValue = "3" OrElse dlReceipt.SelectedValue = "Courrier" Then
            tbRetCard.Visible = True
            'tbRetCard.Focus()
        Else
            tbRetCard.Visible = False
        End If
        tbDocSender.Text = " - " & dlReceipt.SelectedItem.Text.ToUpper
        tbDocSender.Focus()
    End Sub

    Private Sub cbArchive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbArchive.CheckedChanged
        If cbArchive.Checked Then
            ucDocRouting.Visible = False
        Else
            ucDocRouting.Visible = True
        End If
    End Sub

    Private Sub dlRequestType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlRequestType.SelectedIndexChanged
        tbNoCopies.Focus()
    End Sub

    Private Sub btRoute_Click(sender As Object, e As EventArgs) Handles btRoute.Click
        PrintRoute()
    End Sub

    Private Sub UserControlUpload_Init(sender As Object, e As EventArgs) Handles Me.Init
        If DocSession.sUserRole = "B" Then
            dlRequestType.Visible = False
            cbArchive.Visible = False
            tbNoPages.Visible = False
            btPreview.Visible = False
            lArchiveLabel.Visible = False
            lTotalNoPages.Visible = False
            ltypeofrequest.Visible = False
            lMannerofReceipt.Visible = False
            dlReceipt.Visible = False
        End If
    End Sub
End Class
