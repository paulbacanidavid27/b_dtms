Imports System
Imports System.Web
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api

Public Class view
    Inherits System.Web.UI.Page
    Dim OfficeCode As String
    Dim OfficeName As String
    Dim asAOfc, asUOfc, asDocOfc, asAGrp, asUGrp As String
#Region "Properties"
    Public Property pOfficeCode As String
        Get
            Return OfficeCode
        End Get
        Set(ByVal value As String)
            OfficeCode = value
        End Set
    End Property
    Public Property pOfficeName As String
        Get
            Return OfficeName
        End Get
        Set(ByVal value As String)
            OfficeName = value
        End Set
    End Property
#End Region
#Region "Page Event"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        
        'pager: step 4
        AddHandler ucAtt.e_refresh, AddressOf RetrieveAttach
        AddHandler ucUp.e_refresh, AddressOf ReloadView
        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        'AddHandler UserDocLinks.e_ViewDoc, AddressOf openDoc
        AddHandler UserDocLinks.e_ShowMessage, AddressOf ShowLinksMessage
        AddHandler uControlAttach.e_ShowMessage, AddressOf ShowAttachmentMessage
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucPagerRoutingHistory.eGreaterClick, AddressOf imgGreater2_Click
        AddHandler ucPagerRoutingHistory.eLessClick, AddressOf imgLess2_Click
        AddHandler ucPagerRoutingHistory.eFirstClick, AddressOf imgFirst2_Click
        AddHandler ucPagerRoutingHistory.eLastClick, AddressOf imgLast2_Click
        'AddHandler ucVersion.e_click, AddressOf DisplayDocVersion
        AddHandler ucShare.e_click, AddressOf DisplayEmailMessage
        AddHandler ucDocRouting.e_ShowMessage, AddressOf ShowRoutingMessage
        AddHandler ucDocRouting.e_RoutedTo, AddressOf UpdateRoutedTo
        AddHandler uPromptCancelCC.e_OK, AddressOf ucDocRouting.CancelCCRouting
        AddHandler uPromptCancelRoute.e_OK, AddressOf ucDocRouting.CancelRoute
        AddHandler uPromptDelSubTask.e_OK, AddressOf DeleteSubTask
        AddHandler uSTask.e_ShowMessage, AddressOf ShowSubTaskMessage
        AddHandler uEmail.e_showmessage, AddressOf ShowEmailMessage
        AddHandler uEmail.e_close, AddressOf CloseEmail
        AddHandler UserDocNotes.e_Count, AddressOf ShowNotesCount
        AddHandler UserDocLinks.e_Count, AddressOf ShowLinkCount
        AddHandler uControlAttach.e_Count, AddressOf ShowAttachCount
        AddHandler uSTask.e_Count, AddressOf ShowSubTaskCount
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage
        AddHandler ucVersion.e_ShowMessage, AddressOf ShowVersionMessage

        'AddHandler uControlAttach.e_attach, AddressOf showAttach



        'AddHandler UserControlAddTag1.e_click, AddressOf CloseAddTag


        'AddHandler ucCheckIn.e_check, AddressOf ShowCheckInInstruction
        If DocSession.DocumentPage = "issuances.aspx" Then
            Master.SelectTab("Issuances")
        Else
            Master.SelectTab("Documents")
        End If


    End Sub
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserId Is Nothing OrElse DocSession.sUserId = "" Then
            Response.Redirect("Login.aspx")
        End If
        Dim lscriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        lscriptManager.RegisterPostBackControl(ucVersion.FindControl("ucPDFViewer").FindControl("imgDownload"))
        If Not IsPostBack Then
            Try

                pDownload.Visible = False
                lbDownload.Visible = False
                btDownload.Visible = False
                LoadDoc()
                If DocSession.sDocAccess > 2 Then
                    ucAddDoc.Visible = True
                Else
                    ucAddDoc.Visible = False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub
#End Region
#Region "Email Functions"
    Private Sub CloseEmail()
        uEmail.Visible = False
        upEmail.Update()
    End Sub
#End Region
#Region "Attachment Functions"
    Private Sub ShowAttachCount()
        lAttachCount.Text = uControlAttach.pCount

        If lAttachCount.Text.Trim = "" OrElse lAttachCount.Text.Trim = "0" Then
            lAttachCount.Visible = False
        Else
            lAttachCount.Visible = True
        End If

        pnlAttachCount.Update()
    End Sub
#End Region
#Region "Subtask Functions"
    Private Sub ShowSubTaskCount()
        lSubTaskCount.Text = uSTask.pCount

        If lSubTaskCount.Text.Trim = "" OrElse lSubTaskCount.Text.Trim = "0" Then
            lSubTaskCount.Visible = False
        Else
            lSubTaskCount.Visible = True
        End If

        pnlSubTaskCount.Update()
    End Sub
#End Region
#Region "Notes Functions"
    Private Sub ShowNotesCount()
        lNoteCount.Text = UserDocNotes.pCount

        If lNoteCount.Text.Trim = "" OrElse lNoteCount.Text.Trim = "0" Then
            lNoteCount.Visible = False
        Else
            lNoteCount.Visible = True
        End If

        pnlNoteCount.Update()
    End Sub
#End Region
#Region "Link Functions"
    Private Sub ShowLinkCount()
        lLinksCount.Text = UserDocLinks.pCount

        If lLinksCount.Text.Trim = "" OrElse lLinksCount.Text.Trim = "0" Then
            lLinksCount.Visible = False
        Else
            lLinksCount.Visible = True
        End If

        pnlLinkCount.Update()
    End Sub
#End Region
#Region "Routing Functions"
    Private Sub UpdateRoutedTo()
        uReceipt.pRoutedTo = ucDocRouting.pApprovers
    End Sub
#End Region
    
    
    
    Private Sub LoadDoc()

        Try
            GetOfficeCode()

            GetRequestType()
            GetMannerReceipt()
            RetrieveDocStatus()

            If RetrieveDoc() Then
                If lManner.Text = "Courrier" Or lMannerId.Text = "3" Then
                    pnlRet.Visible = True
                    'lReturnNumber.Visible = True
                End If
                ucDocRouting.pDocId = DocSession.sDocID
                ucDocRouting.pDocType = DocSession.sDocType
                'ucDocRouting.pOfficeCode = 
                ucDocRouting.pDocType2 = DocSession.sDocType
                ucDocRouting.pRouteStatusId = lstatusid.Text
                ucDocRouting.ShowHeader()
                If DocSession.sSelectedTab = "Routing" Then
                    SelTabRouting()
                    DocSession.sSelectedTab = ""
                ElseIf DocSession.sSelectedTab = "Version" Then
                    SelTabVersion()
                    DocSession.sSelectedTab = ""
                Else
                    SelTabHistory()
                    
                End If

                
                If DocSession.sEditIndex <> "1" Then
                    btSaveIndex.Visible = False
                End If


                ShowAdd()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


#Region "Pager Routines"
    'pager: step 3
    Public Sub RetAction()
        ucDocHistory.pIdx = CInt(hfCurrent.Value)
        ucDocHistory.RetrieveDocAction(DocSession.sDocID)
        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDocHistory.pRetVal)
        hfTotalRows.Value = CStr(ucDocHistory.pRetVal)
        pDocHistory.Update()
    End Sub
    Public Sub RetRoutingHistory()
        uTrackStatus.pDocID = DocSession.sDocID
        uTrackStatus.RetrieveStatus()
        pDocHistory.Update()
    End Sub
    Private Sub imgLess2_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfRoutingHistoryCurrentRows.Value) - DocSession.RowsPerPage
        hfRoutingHistoryCurrentRows.Value = CStr(lIdx)
        RetRoutingHistory()
    End Sub

    Private Sub imgGreater2_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfRoutingHistoryCurrentRows.Value) + DocSession.RowsPerPage
        hfRoutingHistoryCurrentRows.Value = CStr(lIdx)
        RetRoutingHistory()


    End Sub

    Private Sub imgFirst2_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfRoutingHistoryCurrentRows.Value = CStr(lIdx)
        RetRoutingHistory()

    End Sub

    Private Sub imgLast2_Click()
        Dim lIdx As Integer
        If CInt(hfRoutingHistoryTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfRoutingHistoryTotalRows.Value) - (CInt(hfRoutingHistoryTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfRoutingHistoryTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfRoutingHistoryCurrentRows.Value = CStr(lIdx)
        RetRoutingHistory()


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

#Region "Master Page Methods"
    Private Sub ShowVersionMessage()

        Master.ShowMessage(ucVersion.Message)

    End Sub

    Private Sub ShowUploadMessage()

        Master.ShowMessage(ucUpload.Message)

    End Sub
    Private Sub ShowSubTaskMessage()

        Master.ShowMessage(uSTask.Message)

    End Sub

    Private Sub ShowRoutingMessage()
        Master.ShowMessage(ucDocRouting.Message)
    End Sub
    Private Sub ShowLinksMessage()
        Master.ShowMessage(UserDocLinks.Message)
    End Sub
    Private Sub ShowAttachmentMessage()
        Master.ShowMessage(uControlAttach.Message)
    End Sub
    Private Sub ShowEmailMessage()
        Master.ShowMessage(uEmail.pMessage)
    End Sub
#End Region
    Private Sub HideDocViewAndAttachment()
        hlNewTab.Enabled = False
        hlNewTab2.Visible = False

        hlNewTab.ToolTip = "Document is Archived. You are not allowed to view this document."
        ucAtt.Visible = False

    End Sub
    Private Sub DisableViewing()
        hlNewTab.Enabled = False
        hlNewTab2.Visible = False
        hlNewTab.ToolTip = "You are not allowed to view this document."
        ucAtt.Visible = False
    End Sub
    Private Sub DisableFields()
        tbTitle.Visible = False
        lTitle.Visible = True
        'imgSave.Visible = False
        pEdit.Visible = False
        pReply.Visible = False
        pCheckIn.Visible = False
        pCheckOut.Visible = False
        'imgCheckIn.Visible = False
        'imgCheckout.Visible = False
        'btDownload.Visible = False
        pDownload.Visible = False
        pDelDoc.Visible = False
        pArchiveTrue.Visible = False
        lDocName.Visible = True
        'lForRelease.Visible = False
        'ucForRelease.Visible = False
        'lInProcess.Visible = False
        'ucInProcess.Visible = False
        'lRelease.Visible = False
        'ucForRelease.Visible = False
        'lActedUpon.Visible = False
        'ucActedUpon.Visible = False
        'rbActedUpon.Visible = False
        'rbForRelease.Visible = False
        'rbForApproval.Visible = False

        pnAddAttachment.Visible = False
    End Sub

    Private Sub EnableFields()
        'tbTitle.Visible = True
        'lTitle.Visible = False
        'imgSave.Visible = True
        'If DocSession.sOwner OrElse IsApprover() OrElse DocSession.sUserRole = "A" OrElse (DocSession.sUserRole = "D" AndAlso DocSession.sOfcCode = DocSession.sDocOfcCode) Then
        '    pEdit.Visible = True
        'Else
        '    pEdit.Visible = False
        'End If

        pReply.Visible = True
        lDocName.Visible = True
        pCheckIn.Visible = False
        'imgCheckIn.Visible = False
        If DocSession.sUserRole = "A" Then
            pCheckOut.Visible = True
        End If

        'imgCheckout.Visible = True
        'btDownload.Visible = False
        pDownload.Visible = False
        tbTitle.Focus()
    End Sub
    Private Sub ShowAdd()
        'If CInt(DocSession.sDocTypeAccess) <= 1 Then
        '    Master.ShowMessage("You only have read access. You cannot modify the document.")
        '    DisableFields()
        '    'ElseIf DocSession.Archived AndAlso DocSession.sUserRole <> "A" Then
        '    '    'Master.ShowMessage("Document is currently Archived. You can only approve the document. Modification is not allowed.")
        '    '    'DisableFields()
        '    '    HideDocViewAndAttachment()
        'Else
        If DocSession.sUserRole = "B" Then
            pEdit.Visible = False
            pReceipt.Visible = False
            pBlankReceipt.Visible = False
            pRouteSlip.Visible = False
            pBRouteSlip.Visible = False
            pBRouteSlip2.Visible = False
            pReply.Visible = False
        Else


            If (lIsCheckOut.Text = "Yes") AndAlso (DocSession.sCheckOutBy <> DocSession.sUserId AndAlso DocSession.sUserRole <> "A") Then
                'lcheckmsg.Text = "Document is currently checked-out. Modification is not allowed."
                'lcheckmsg.CssClass = "msg_red"
                Master.ShowMessage("Document is currently checked-out. Document cannot be approved or modified.")
                DisableFields()
            ElseIf (lIsCheckOut.Text = "Yes") AndAlso (DocSession.sCheckOutBy = DocSession.sUserId) Then
                Master.ShowMessage("You have checked-out this document. Document cannot be modified by other users.")
                'imgCheckout.Visible = False
                pCheckOut.Visible = False
                'imgCheckIn.Visible = True
                If DocSession.sUserRole = "A" Then
                    pCheckIn.Visible = True
                End If

                'btDownload.Visible = True
            ElseIf (lIsCheckOut.Text = "Yes") AndAlso (DocSession.sUserRole = "A") Then
                Master.ShowMessage(lCheckoutBy.Text & " has checked-out this document. As an admin, you will be able to check-in this document.")
                'imgCheckout.Visible = False
                pCheckOut.Visible = False
                'imgCheckIn.Visible = True
                If DocSession.sUserRole = "A" Then
                    pCheckIn.Visible = True
                End If
                'btDownload.Visible = True
                'ElseIf CInt(DocSession.sDocTypeAccess) > 1 Then
                '    EnableFields()
            End If

            If (DocSession.sCanDownload = "True" AndAlso (IsApprover() OrElse (DocSession.sUserRole <> "A" AndAlso (SameOffice() OrElse SameGroup())))) OrElse DocSession.sOwner OrElse DocSession.sUserRole = "A" Then
                'btDownload.Visible = True
                'pDownload.Visible = True
                DocSession.sCanDownloadDoc = "True"
            Else
                'btDownload.Visible = False
                'pDownload.Visible = False
                pShare.Visible = False
            End If

            If DocSession.Archived Then

                pEdit.Visible = False
                pReply.Visible = False
                If DocSession.sUserRole = "A" OrElse DocSession.sOwner OrElse DocSession.ArchivedBy = DocSession.sUserId OrElse ((DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G") AndAlso (SameOffice() OrElse SameGroup())) Then
                    pEdit.Visible = True
                Else
                    pEdit.Visible = False
                End If
            Else
                If lConf.Text = "Yes" AndAlso Not DocSession.sOwner AndAlso DocSession.sUserRole <> "A" Then
                    pEdit.Visible = False
                Else
                    If DocSession.sOwner OrElse IsApprover() OrElse DocSession.sUserRole = "A" OrElse ((DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G") AndAlso (SameOffice() OrElse SameGroup())) Then
                        pEdit.Visible = True
                    Else
                        pEdit.Visible = False
                    End If
                End If

                If DocSession.sVersionControl = "True" AndAlso DocSession.sUserRole = "A" Then
                Else
                    'imgCheckout.Visible = False
                    pCheckOut.Visible = False
                    'imgCheckIn.Visible = False
                    pCheckIn.Visible = False
                End If
                'check for overdue status
                If IsDate(lOrigDue.Text.Trim) Then
                    If CDate(lOrigDue.Text.Trim) = Date.Now AndAlso DocSession.DocClosed(CInt(lstatusid.Text)) Then
                        Dim oDoc As DocList
                        oDoc = New DocList
                        oDoc.pDocId = DocSession.sDocID
                        oDoc.pUserId = DocSession.sUserId
                        oDoc.pDocStatus = "9" ' Overdue
                        oDoc.UpdateDoc()
                        SetStatus("9", "Overdue")
                    End If
                End If
            End If
        End If
    End Sub

#Region "Checkout Methods"
    Private Sub ShowCheckOutInstruction()

        lAction.Text = "Checkout"
        'lcheckmsg.Text = ""

        pUpload1.Visible = True
        pUpload2.Visible = True
        pInstuction.Visible = True
        pDoc.Visible = True
    End Sub

    Private Sub ShowCheckInInstruction()

        'lcheckmsg.Text = ""
        lAction.Text = "Checkin"
        pnlCheckin.Visible = Not pnlCheckin.Visible
        '''btDownload.Visible = Not btDownload.Visible



        pDoc.Visible = True
        pUpload1.Visible = True
        pUpload2.Visible = True
    End Sub

    Private Sub btCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCheckOut.Click

        Try
            'lcheckmsg.Text = ""
            Dim oDoc As New DocList
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pIPAddress = Request.UserHostAddress
            oDoc.CheckOutDoc()
            If pnlDocHistory.Visible Then
                RetAction()
            End If
            lCheckoutBy.Text = DocSession.sUserName
            lIsCheckOut.Text = "Yes"
            'lcheckmsg.Text = "File has been check-out successfully."
            Master.ShowMessage("File has been Check-Out successfully.")
            'lcheckmsg.CssClass = "msg_green"
            pInstuction.Visible = False
            'ucCheckOut.Visible = Not ucCheckOut.Visible
            'lCheckout.Visible = Not lCheckout.Visible
            'ucCheckIn.Visible = Not ucCheckIn.Visible
            'lCheckIn.Visible = Not lCheckIn.Visib1le
            '''btDownload.Visible = True
            pDoc.Visible = False
            pUpload1.Visible = False
            pUpload2.Visible = False
            'imgCheckout.Visible = False
            pCheckOut.Visible = False
            'imgCheckIn.Visible = True
            If DocSession.sUserRole = "A" Then
                pCheckIn.Visible = True
            End If
            'imgSave.Visible = False
            pEdit.Visible = False
            pReply.Visible = True
            tbTitle.Visible = False
            lTitle.Visible = True
            tbSenderName.Visible = False
            lsendername.Visible = True
            tbNoCopies.Visible = False
            lNoCopies.Visible = True
            tbNoPages.Visible = False
            lNoPages.Visible = True
            tbReceivedBy.Visible = False
            lReceivedBy.Visible = True
            tbReceivedDate.Visible = False
            lReceivedDate.Visible = True
            If lView.Visible Then
                docvw.Visible = True
            End If
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Checkout"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Checkout file '" & lFileName.Text & "'"
            ohist.AddHistory()
            pDocView.Update()
            pDocInfo.Update()
        Catch ex As Exception

        End Try



    End Sub

    Private Sub btCheckoutCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCheckoutCancel.Click
        'pInstuction.Visible = Not pInstuction.Visible
        'ucCheckOut.DefaultCheck = False
        'ucCheckOut.Visible = True
        'lcheckmsg.Text = ""


        pnlCheckin.Visible = False
        pInstuction.Visible = False
        pUpload1.Visible = False
        pUpload2.Visible = False
        pDoc.Visible = Not pDoc.Visible
        If lView.Visible Then
            docvw.Visible = True
        End If

        pDocView.Update()
    End Sub
#End Region
#Region "Retrieval Methods"

#End Region
    Private Function SameOffice() As Boolean 'SameOffice(ByVal asAOfc As String, ByVal asUOfc As String, ByVal asDocOfc As String) As Boolean
        Return (asUOfc = DocSession.sOfcCode OrElse asUOfc = DocSession.sOfcCode OrElse asDocOfc = DocSession.sOfcCode)
    End Function

    Private Function SameGroup() As Boolean 'SameGroup(ByVal asAGrp As String, ByVal asUGrp As String) As Boolean
        Return (DocSession.sUserGroup = "CRD" OrElse asUGrp = DocSession.sUserGroup OrElse (lstatusid.Text = "8" AndAlso asAGrp = DocSession.sUserGroup))
    End Function

    Private Sub SetCanPrint(ByVal asStatusId As String, ByVal asAllowPrintingArchivedDocuments As String)
        'DocSession.sCanPrint = abPrint
        'If DocSession.sCanPrint = "True" Then
        '    If asStatusId = "8" Then
        '        If DocSession.ArchivedBy = DocSession.sUserId Then
        '            DocSession.sCanPrint = "True"
        '        ElseIf DocSession.sUserRole = "D" Then
        '            DocSession.sCanPrint = "True"
        '        End If
        '    End If
        'End If
        If DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
            DocSession.sCanPrintReceipt = "True"
        ElseIf DocSession.sCanPrint = "True" Then
            If (DocSession.sUserRole = "D") AndAlso (SameOffice() OrElse SameGroup()) Then
                DocSession.sCanPrintReceipt = "True" 'DocSession.sCanPrint
            ElseIf lAuthorID.Text = DocSession.sUserId Then
                DocSession.sCanPrintReceipt = "True"
            ElseIf IsApprover() Then
                DocSession.sCanPrintReceipt = "True"
            ElseIf asStatusId = "8" AndAlso asAllowPrintingArchivedDocuments = "True" Then
                If DocSession.ArchivedBy = DocSession.sUserId Then
                    DocSession.sCanPrintReceipt = "True"
                Else
                    DocSession.sCanPrintReceipt = "False"
                End If

            Else
                DocSession.sCanPrintReceipt = "False" 'DocSession.sCanPrint
            End If
        Else
            DocSession.sCanPrintReceipt = "False" 'DocSession.sCanPrint
        End If

    End Sub


    Private Sub IsAllowedViewing(ByVal asArchiverOfc As String, ByVal isApprover As Boolean)
        If isApprover Then
            DocSession.sCanView = "True"
            'ElseIf DocSession.sCanDownload = "True" Then 'should not allow view if download access is true
            'DocSession.sCanView = "True"
        ElseIf DocSession.sOfcCode.ToLower = DocSession.sDocOfcCode.ToLower Then
            DocSession.sCanView = "True"
        ElseIf DocSession.sOfcCode.ToLower = DocSession.sUploaderOfc.ToLower Then
            DocSession.sCanView = "True"
        ElseIf DocSession.sOfcCode.ToLower = asArchiverOfc Then
            DocSession.sCanView = "True"
        ElseIf DocSession.sUserRole = "A" OrElse DocSession.sUserRole = "G" Then
            DocSession.sCanView = "True"
        Else
            DocSession.sCanView = "True"
            'DocSession.sCanDownload = "False" 'download access should not take effect if the user cannot view the document
        End If

    End Sub

    Private Function FileDoesNotExists() As Boolean

        Try
            Dim sYear As String = Year(CDate(lCreatedDate.Text))
            Dim sMonth As String = MonthName(Month(CDate(lCreatedDate.Text)))

            Dim lsFile As String = sYear & "\" & sMonth & "\" & lrefno.Text & "\" & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text

            If System.IO.File.Exists(DocSession.FileLoc & lsFile) Then
                If DocSession.sUserRole = "A" Then
                    Dim linfo As New System.IO.FileInfo(DocSession.FileLoc & lsFile)
                    lFileSize.Text = FormatBytes(linfo.Length)
                    lFileSize.Visible = True
                End If

                Return False
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFile) Then
                If DocSession.sUserRole = "A" Then
                    Dim linfo As New System.IO.FileInfo(DocSession.FileLoc2 & lsFile)
                    lFileSize.Text = FormatBytes(linfo.Length)
                    lFileSize.Visible = True
                End If

                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function

    Private Function FileExists() As Boolean

        Try
            'Dim sYear As String = Year(CDate(lCreatedDate.Text))
            'Dim sMonth As String = MonthName(Month(CDate(lCreatedDate.Text)))

            'Dim lsFile As String = sYear & "\" & sMonth & "\" & lrefno.Text & "\" & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text

            If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                'If DocSession.sUserRole = "A" Then
                'Dim linfo As New System.IO.FileInfo(DocSession.FileLoc & lsFile)
                'lFileSize.Text = FormatBytes(linfo.Length)
                'lFileSize.Visible = True
                'End If

                Return True
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 &  DocSession.sCurrentFile) Then
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
    Public Function FormatBytes(ByVal BytesCaller As ULong) As String
        Dim DoubleBytes As Double
        Try
            Select Case BytesCaller
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(BytesCaller / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2) & " TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(BytesCaller / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2) & " GB"
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(BytesCaller / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2) & " MB"
                Case 1024 To 1048575
                    DoubleBytes = CDbl(BytesCaller / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2) & " KB"
                Case 1 To 1023
                    DoubleBytes = BytesCaller ' bytes
                    Return FormatNumber(DoubleBytes, 2) & " bytes"
                Case Else
                    Return ""
            End Select
        Catch
            Return ""
        End Try

    End Function
    Private Function RetrieveDoc() As Boolean
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim oDocs As New DocList
        'hlNewTab2.Visible = False
        Dim lodata As DataTable
        Try
            oDocs.pDocId = DocSession.sDocID
            oDocs.pGroupId = DocSession.sUserGroup
            oDocs.pUserId = DocSession.sUserId
            lodata = oDocs.RetrieveDocId()

            If lodata.Rows.Count > 0 Then
                '--new folder
                Dim sYear, sMonth As String

                'session variables
                DocSession.sDocType = lodata(0).Item("DocType")
                DocSession.sVersion = lodata(0).Item("FileVersion")
                DocSession.sDocTypeAccess = 5 'lodata(0).Item("GroupAccessId")
                DocSession.ArchivedBy = lodata(0).Item("ArchivedBy")
                DocSession.IsLocalDoc = IIf(lodata(0).Item("IsLocal") = "1" OrElse lodata(0).Item("IsLocal") = "True", "Y", "")
                asAOfc = lodata(0).Item("ArchiverOfficeCode")
                asAGrp = lodata(0).Item("ArchiverGroupId")
                asUOfc = lodata(0).Item("UploadOfficeCode")
                asUGrp = lodata(0).Item("UploadGroupId")
                asDocOfc = lodata(0).Item("OfficeCode")

                SetCanPrint(lodata(0).Item("StatusId"), lodata(0).Item("AllowPrinting"))
                If DocSession.sUserRole = "A" Then
                    lrefno.ToolTip = "Archived By: " & lodata(0).Item("ArchiverName") & " (Office: " & lodata(0).Item("ArchiverOfficeCode") & "/Group: " & lodata(0).Item("ArchiverGroupId") & ") " & _
                                    " Uploader (Office: " & lodata(0).Item("UploadOfficeCode") & "/Group: " & lodata(0).Item("UploadGroupId") & ")"
                End If


                lCdate.Text = lodata(0).Item("cdate").ToString()
                'DocSession.sCanPrintReceipt = CTrue(lodata(0).Item("CanPrintReceipt"))
                'DocSession.sCanDownload = IIf(CTrue(lodata(0).Item("CanDownload")), True, (lodata(0).Item("CreatedBy") = DocSession.sUserId))
                'DocSession.sVersionControl = CTrue(lodata(0).Item("VersionControl"))
                DocSession.sDocTitle = lodata(0).Item("Title")
                DocSession.sOwner = (lodata(0).Item("CreatedBy") = DocSession.sUserId)
                uReceipt.pAuthor = lodata(0).Item("CreatedBy")
                uReceiptRoute.pAuthor = lodata(0).Item("CreatedBy")
                uReceiptRoute.pRemarks = System.Configuration.ConfigurationManager.AppSettings("RoutedToRemarks").Trim
                uBlankRoute.pAuthor = lodata(0).Item("CreatedBy")

                '--new folder
                sYear = Year(CDate(lodata(0).Item("CreatedDate"))).ToString
                sMonth = MonthName(Month(CDate(lodata(0).Item("CreatedDate"))))
                uControlAttach.pCreatedDate = lodata(0).Item("CreatedDate")
                ucVersion.pCreatedDate = lodata(0).Item("CreatedDate")

                DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lodata(0).Item("refno") & "\" & DocSession.sDocID & "_" & DocSession.sVersion & "_" & lodata(0).Item("FileName")
                If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) AndAlso Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
                    DocSession.sCurrentFile = DocSession.sDocID & "_" & DocSession.sVersion & "_" & lodata(0).Item("FileName")


                End If

                DocSession.sFileName = lodata(0).Item("FileName")
                DocSession.sReferenceNo = lodata(0).Item("refno")
                DocSession.sDocStatus = lodata(0).Item("StatusId")
                'If DocSession.sCanPrintReceipt Then
                pReceipt.Visible = True
                pRouteSlip.Visible = True
                pBRouteSlip.Visible = True
                pBRouteSlip2.Visible = True
                'Else
                '    pReceipt.Visible = False
                'End If
                If DocSession.sUserRole = "A" Then
                    pBlankReceipt.Visible = True
                End If

                SetStatus(lodata(0).Item("StatusId"), lodata(0).Item("StatusDesc"))
                If lstatusid.Text = "8" Then
                    lStatus.ToolTip = lodata(0)("ArchiverName")
                End If
                If lstatusid.Text = "8" AndAlso (lodata(0).Item("ModifiedBy") = DocSession.sUserId OrElse DocSession.sUserRole = "A") Then
                    imgEmailPointPerson.Visible = True

                Else
                    imgEmailPointPerson.Visible = False
                End If

                DocSession.sCheckOutBy = lodata(0).Item("ModifiedBy")
                If IsDBNull(lodata(0).Item("RoutedTo")) Then
                    uReceipt.pRoutedTo = ""
                    uReceiptRoute.pRoutedTo = ""
                Else
                    uReceipt.pRoutedTo = lodata(0).Item("RoutedTo")
                    'uReceiptRoute.pRoutedTo =
                    uReceiptRoute.pRoutedTo = lodata(0).Item("RoutedTo") '& " " & ucDocRouting.FirstApprover
                End If

                'transfer this to click Print Receipt
                'If IsDBNull(lodata(0).Item("CCTo")) Then
                '    uReceipt.pCarbon = ""
                'Else
                '    uReceipt.pCarbon = RetrieveCC() 'lodata(0).Item("CCTo")
                'End If

                If IsDBNull(lodata(0).Item("DocSender")) Then
                    uReceipt.pSender = ""
                    uReceiptRoute.pSender = ""
                    uBlankRoute.pSender = ""
                    lsendername.Text = ""
                    tbSenderName.Text = ""
                Else
                    uReceipt.pSender = lodata(0).Item("DocSender")
                    uReceiptRoute.pSender = lodata(0).Item("DocSender")
                    uBlankRoute.pSender = lodata(0).Item("DocSender")
                    lsendername.Text = lodata(0).Item("DocSender")
                    tbSenderName.Text = lodata(0).Item("DocSender")
                End If
                '--additional remarks
                If IsDBNull(lodata(0).Item("AdditionalInfo")) Then
                    uReceipt.pRemarks = ""
                    uReceiptRoute.pRemarks = ""
                    uBlankRoute.pRemarks = ""

                    'lsendername.Text = ""
                    'tbSenderName.Text = ""
                Else
                    uReceipt.pRemarks = lodata(0).Item("AdditionalInfo")
                    uReceiptRoute.pNotes = lodata(0).Item("AdditionalInfo")
                    uBlankRoute.pNotes = lodata(0).Item("AdditionalInfo")
                    'lRemarks.Text = lodata(0).Item("AdditionalInfo")
                    'tRemarks.Text = lodata(0).Item("AdditionalInfo")
                End If
                'If uReceipt.pRemarks <> "" Then
                '    'uReceipt.Visible = True
                '    'uReceipt.ShowRemarks()
                'Else
                '    'uReceipt.Visible = False
                'End If
                Dim lsCopies As String = GenerateCopies(lodata(0).Item("TotalCopies"), lodata(0).Item("TotalPages"))

                If lsCopies = "" Then
                    lsCopies = "N/A"
                End If
                uReceipt.pCopiesPages = lsCopies
                uReceiptRoute.pCopiesPages = lsCopies
                uBlankRoute.pCopiesPages = lsCopies
                pOfficeCode = lodata(0).Item("OfficeCode").ToString.Trim
                pOfficeName = lodata(0).Item("OfficeName").ToString.Trim
                If IsDBNull(lodata(0).Item("ParentDocId")) Then
                    DocSession.sParentDocID = ""
                    lTitle.Text = lodata(0).Item("Title")
                    TdSubTask1.Visible = True
                    TdSubTask2.Visible = True
                    TdSubTask3.Visible = True
                    'If DocSession.sUserGroup = "CRV" OrElse DocSession.sUserRole = "A" Then
                    '    'trCopies.Visible = True
                    'Else
                    '    'trCopies.Visible = False
                    'End If

                Else
                    DocSession.sParentDocID = lodata(0).Item("ParentDocId")
                    lTitle.Text = "A:" & lodata(0).Item("Title")

                    TdSubTask1.Visible = True
                    TdSubTask2.Visible = True
                    TdSubTask3.Visible = True
                    lbAddSubTask.Visible = False
                    imgAddSubTask.Visible = False
                    'trCopies.Visible = True
                End If



                lManner.Text = lodata(0).Item("MannerReceiptDesc")
                lMannerId.Text = lodata(0).Item("MannerReceipt").ToString.Trim

                SetMannerReceipt()
                SetDocStatus()
                SetOffice()

                DocSession.sDocOfcCode = pOfficeCode

                lsOfficeCode.Text = pOfficeCode
                lsOfficeName.Text = pOfficeName

                lPrinted.Text = lodata(0).Item("Printed")
                tbTitle.Text = lodata(0).Item("Title")
                lCreatedDate.Text = CDate(lodata(0).Item("CreatedDate")).ToLongDateString & " " & CDate(lodata(0).Item("CreatedDate")).ToLongTimeString
                lAuthor.Text = lodata(0).Item("Originator")
                lAuthorID.Text = lodata(0).Item("CreatedBy")
                lDocName.Text = lodata(0).Item("DocName")
                lrefno.Text = lodata(0).Item("RefNo")
                lRequestType.Text = lodata(0).Item("RequestType")
                lRequestDescription.Text = lodata(0).Item("RequestDescription")
                lClassification.Text = lodata(0).Item("Classification")
                lClassificationCode.Text = lodata(0).Item("InternalDoc")
                lNoCopies.Text = lodata(0).Item("TotalCopies")
                lNoPages.Text = lodata(0).Item("TotalPages")
                tbNoCopies.Text = lodata(0).Item("TotalCopies")
                tbNoPages.Text = lodata(0).Item("TotalPages")
                dlClassification.SelectedValue = lodata(0).Item("InternalDoc")
                lReturnCard.Text = IIf(IsDBNull(lodata(0).Item("ReturnCard")), "", lodata(0).Item("ReturnCard"))
                tbReturnCard.Text = IIf(IsDBNull(lodata(0).Item("ReturnCard")), "", lodata(0).Item("ReturnCard"))
                lReceivedBy.Text = IIf(IsDBNull(lodata(0).Item("ReceivedBy")), "", lodata(0).Item("ReceivedBy"))
                tbReceivedBy.Text = IIf(IsDBNull(lodata(0).Item("ReceivedBy")), "", lodata(0).Item("ReceivedBy"))
                lReceivedDate.Text = IIf(IsDBNull(lodata(0).Item("ReceivedDate")), "", lodata(0).Item("ReceivedDate"))
                tbReceivedDate.Text = IIf(IsDBNull(lodata(0).Item("ReceivedDate")), "", lodata(0).Item("ReceivedDate"))
                If IsDBNull(lodata(0).Item("Confidential")) Then
                    cbConf.Checked = False
                Else
                    If lodata(0).Item("Confidential") = "True" Then
                        cbConf.Checked = True
                    Else
                        cbConf.Checked = False
                    End If
                End If


                lConf.Text = IIf(cbConf.Checked, "Yes", "No")
                If DocSession.HideConfidential Then
                    lconftext.Visible = False
                    lConf.Visible = False
                    cbConf.Visible = False
                Else
                    SetConfidentialBackground()
                End If


                lReceivedTime.Text = IIf(IsDBNull(lodata(0).Item("ReceivedTime")), "", lodata(0).Item("ReceivedTime"))
                tbReceivedTime.Text = IIf(IsDBNull(lodata(0).Item("ReceivedTime")), "", lodata(0).Item("ReceivedTime"))


                SetRequestType()

                If IsDBNull(lodata(0).Item("DueDate")) Then
                    tbDueDate.Text = ""
                    lOrigDue.Text = "Not set"
                Else
                    tbDueDate.Text = CDate(lodata(0).Item("DueDate")).ToString("MM/dd/yyyy")
                    lOrigDue.Text = tbDueDate.Text
                End If

                lFileName.Text = lodata(0).Item("FileName")

                lFileName.ToolTip = "ID: " & DocSession.sDocID
                lVersionShow.Text = "(Version " & lodata(0).Item("FileVersion") & ".0)"
                lVersion.Text = lodata(0).Item("FileVersion")
                If lodata(0).Item("bookmarked").ToString.Trim.ToLower = "1" Then
                    imgBook.Visible = False
                    imgBookM.Visible = True
                End If
                DocSession.sUploaderOfc = lodata(0).Item("UploadOfficeCode").ToString.Trim.ToLower
                'If (Not DocSession.Archived AndAlso (DocSession.sOfcCode.ToLower = DocSession.sUploaderOfc.ToLower OrElse IsApprover() OrElse (DocSession.sUserGroup = lodata(0).Item("UserGroup") andalso DocSession.sUserRole="D" )) OrElse DocSession.sUserRole = "A" Then
                '1. Allow viewing of Documents if the logon user office is the same with the uploader office.
                '2. If the logon user is the approver of the document.
                '3. If logon user group = uploader logon user group and the logon user should be Super User (D).
                '4. If Admin and Super user only.
                'If (DocSession.sOfcCode.ToLower = DocSession.sUploaderOfc.ToLower OrElse IsApprover() OrElse _
                '        (DocSession.sUserGroup = lodata(0).Item("UserGroup") AndAlso DocSession.sUserRole = "D") OrElse _
                '        (DocSession.sUserGroup = lodata(0).Item("UserGroup") AndAlso DocSession.sDocType.ToLower = "letters") OrElse _
                '        DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "A") Then
                IsAllowedViewing(lodata(0).Item("ArchiverOfficeCode"), IsApprover())
                Dim fnotexists As Boolean = FileDoesNotExists()
                Dim localdoc As Boolean = (DocSession.IsLocalDoc = "Y" AndAlso DocSession.sIsLocal)
                Dim Inlocaldoc As Boolean = (DocSession.IsLocalDoc = "Y" AndAlso Not DocSession.sIsLocal)
                Dim clouddoc As Boolean = (DocSession.IsLocalDoc <> "Y" AndAlso Not DocSession.sIsLocal)
                Dim Inclouddoc As Boolean = (DocSession.IsLocalDoc <> "Y" AndAlso DocSession.sIsLocal)
                If fnotexists AndAlso (localdoc OrElse clouddoc) Then
                    lbDownload.Visible = False
                    btDownload.Visible = False
                    lbDownload.Enabled = False
                    'lbDownload.ToolTip = "Download is temporarily not available for this file."
                    btDownload.Enabled = False
                    'btDownload.ToolTip = "Download is temporarily not available for this file."

                End If

                If DocSession.sCanView = "True" Then
                        If (localdoc OrElse clouddoc) AndAlso (lFileName.Text = "" OrElse fnotexists) Then
                            If DocSession.sOwner OrElse DocSession.sUserRole = "A" Then
                                hlNewTab.Visible = False
                                hlNewTab2.Visible = False
                                lbUpload.Visible = True
                                'lbUpload.ToolTip = DocSession.sIsLocal & " " & DocSession.IsLocalDoc
                                lVersion.Visible = False
                            Else
                                hlNewTab.Visible = True
                                hlNewTab2.Visible = False
                                lbUpload.Visible = False
                                hlNewTab.ToolTip = "File does not exist. Please contact your System Administrator."
                            End If


                        Else

                            If Right(lodata(0).Item("FileName").trim, 4) = ".doc" OrElse Right(lodata(0).Item("FileName").trim, 4) = "docx" OrElse
                            Right(lodata(0).Item("FileName").trim, 4) = ".xls" OrElse Right(lodata(0).Item("FileName").trim, 4) = "xlsx" OrElse
                            Right(lodata(0).Item("FileName").trim, 4) = ".ppt" OrElse Right(lodata(0).Item("FileName").trim, 4) = "pptx" Then

                                hlNewTab.Visible = True
                                hlNewTab.Enabled = False
                                hlNewTab2.Visible = False
                                If DocSession.sCanDownload = "True" Then
                                    hlNewTab.ToolTip = "This file Type cannot be viewed directly in the system. Please download to view this file."
                                Else
                                    If DocSession.sCanView = "True" Then
                                        hlNewTab.ToolTip = "This file Type cannot be viewed directly in the system."
                                    Else
                                        hlNewTab.ToolTip = "You don't have enough access to view this file."
                                    End If

                                End If
                            Else

                                Dim lec As New crypt
                                'If DocSession.sParentDocID <> "" Then
                                '    hlNewTab.NavigateUrl = "viewfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sParentDocID & "dbm")) & "&v_no=" & lVersion.Text
                                'Else
                                'If DocSession.IsLocalDoc = "Y" AndAlso Not DocSession.sIsLocal AndAlso fnotexists Then
                                If (Inlocaldoc Or Inclouddoc) Then
                                    If DocSession.sUserRole = "A" Then
                                        hlNewTab.Style("background-color") = "yellow"
                                        hlNewTab.ToolTip = "This File has been uploaded in another server."
                                    End If
                                    hlNewTab.NavigateUrl = DocSession.LocalPath & "/viewfilelocal.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&v_no=" & lVersion.Text &
                                    "&r_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sReferenceNo & "dbm")) & "&o=" &
                                    IIf(DocSession.sOwner, "1", "") & "&d=" & IIf(DocSession.sCanDownloadDoc = "True", "1", "") & "&r=" & DocSession.sUserRole & "&u=" & Server.UrlEncode(lec.Encrypt(DocSession.sUserId & "smd"))
                                Else
                                    hlNewTab.NavigateUrl = "viewfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&v_no=" & lVersion.Text
                                End If

                                'hlNewTab2.Visible = True
                                If DocSession.sCanPrintReceipt = "True" Then
                                    If (Inlocaldoc Or Inclouddoc) Then
                                        hlNewTab2.NavigateUrl = DocSession.LocalPath & "/printfilelocal.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&v_no=" & lVersion.Text &
                                        "&r_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sReferenceNo & "dbm")) & "&o=" &
                                        IIf(DocSession.sOwner, "1", "") & "&d=" & IIf(DocSession.sCanDownloadDoc = "True", "1", "") & "&r=" & DocSession.sUserRole & "&u=" & Server.UrlEncode(lec.Encrypt(DocSession.sUserId & "smd"))

                                    Else
                                        hlNewTab2.NavigateUrl = "printfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&v_no=" & lVersion.Text
                                    End If

                                Else
                                    hlNewTab2.Visible = False
                                End If

                                'End If
                            End If
                        End If
                        'If Not DocSession.Archived Then
                        pnAddAttachment.Visible = True
                        'End If

                        'pEdit.Visible = True
                    Else
                        If DocSession.Archived Then
                            HideDocViewAndAttachment()
                        Else
                            DisableViewing()
                        End If
                        'hlNewTab.NavigateUrl = "viewfile.aspx?d_id=" & DocSession.sDocID & "&v_no=" & lVersion.Text
                        'If DocSession.Archived andalso
                    End If


                    lIsCheckOut.Text = lodata(0).Item("IsBeingModified")
                    lDateUploaded.Text = lodata(0).Item("CreatedDate")
                    DocSession.sDocFileName = DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text

                    'hlOpenDoc.ImageUrl = DocTypeLogo.DocTypeBitMap(Right(lodata(0).Item("FileName").trim, 4))
                    DocTypeImage(Right(lodata(0).Item("FileName").trim, 4))

                    If IsTrue(lodata(0).Item("IsBeingModified").ToString.Trim) Then
                        DocSession.sCheckout = "Yes"
                        lIsCheckOut.Text = "Yes"
                        lCheckoutBy.Text = lodata(0).Item("checkoutby")
                        pInstuction.Visible = False
                        'btDownload.Visible = True
                        'imgCheckIn.Visible = True
                        If DocSession.sUserRole = "A" Then
                            pCheckIn.Visible = True
                        End If

                        'lCheckIn.Visible = True
                        'lCheckout.Visible = False
                        'ucCheckIn.Visible = True
                        'ucCheckOut.Visible = False
                        pnlCheckin.Visible = False
                    Else
                        DocSession.sCheckout = "No"
                        lIsCheckOut.Text = "No"
                        'lCheckout.Visible = True
                        'lCheckIn.Visible = False
                        pnlCheckin.Visible = False
                        'imgCheckout.Visible = True
                        If DocSession.sUserRole = "A" Then
                            pCheckOut.Visible = True
                        End If



                    End If
                    'If DocSession.Archived Then
                    'DisableFields()
                    'End If
                    'uSTask.CountSubTask()
                    Return True
                Else
                    Master.ShowMessage("Reference number does Not exist.")
                Return False

            End If

        Catch ex As Exception
            Master.ShowMessage("Error occurred While retrieving document information (" & ex.Message & "). Please Try again.")
        Finally
            If Not oDocs Is Nothing Then
                oDocs = Nothing
            End If

            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing

            End If
        End Try

    End Function
    Private Function RetrieveCC() As String
        Dim oDoc As New DocRouting
        oDoc.pDocId = DocSession.sDocID
        Return oDoc.RetrieveCC

    End Function
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
                        lsCopies = lsCopies & " And " & asPages & " pages "
                    Else
                        lsCopies = asPages & " pages "
                    End If
                ElseIf CInt(asPages) = 1 Then
                    If lsCopies.Trim <> "" Then
                        lsCopies = lsCopies & " And " & asPages & " page "
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
    'Private Function getDocTypeValue() As String
    '    If dlDocType.SelectedValue.IndexOf(";") > 0 Then
    '        Return dlDocType.SelectedValue.Split(";")(0)
    '    Else
    '        Return dlDocType.SelectedValue
    '    End If

    'End Function
    'Private Function getRetentionValue() As String
    '    If dlDocType.SelectedValue.IndexOf(";") > 0 Then
    '        Return dlDocType.SelectedValue.Split(";")(1)
    '    Else
    '        Return ""
    '    End If

    'End Function
#Region "Reference Table"
    Private Sub RetrieveDocTypes()
        Dim oDoc As New DocTypes
        oDoc.pGroupId = DocSession.sUserGroup
        Using ldata As DataTable = oDoc.GetDocType2()

            dlDocType.DataSource = ldata
            dlDocType.DataTextField = "DocName"
            dlDocType.DataValueField = "DocType"
            dlDocType.DataBind()
            'dlDocType.SelectedValue = DocSession.sDocType
            If DocSession.sDocType <> "" Then
                Dim li As New WebControls.ListItem
                li = dlDocType.Items.FindByValue(DocSession.sDocType)
                If Not IsNothing(li) Then
                    dlDocType.SelectedValue = DocSession.sDocType
                End If
            End If
        End Using

    End Sub
    Private Sub GetOfficeCode()

        Dim ldata As DataTable

        Dim oType As DocGroup
        Try
            oType = New DocGroup
            'If DocSession.sUserRole = "A" Then
            ldata = oType.RetrieveOffice
            'Else
            'ldata = oType.RetrieveGroupOffice()
            ' End If

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

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = ""
            ldata.Rows.InsertAt(lrow, 0)
            dlRequestType.DataSource = ldata
            dlRequestType.DataValueField = "RequestType"
            dlRequestType.DataTextField = "RequestDescription"
            dlRequestType.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
#End Region

    Private Sub SetRequestType()
        If lRequestType.Text.Trim <> "" Then
            Dim li As New WebControls.ListItem
            li = dlRequestType.Items.FindByValue(lRequestType.Text.Trim)
            If Not IsNothing(li) Then
                dlRequestType.SelectedValue = lRequestType.Text.Trim
            End If
        End If
    End Sub
    Private Sub SetOffice()
        If pOfficeCode <> "" Then
            Dim li As New WebControls.ListItem
            li = dlOfficeCode.Items.FindByValue(pOfficeCode)
            If Not IsNothing(li) Then
                dlOfficeCode.SelectedValue = pOfficeCode
            End If
        End If
    End Sub
    Private Sub SetMannerReceipt()
        If lMannerId.Text <> "" Then
            Dim li As New WebControls.ListItem
            li = dlManner.Items.FindByValue(lMannerId.Text.Trim)
            If Not IsNothing(li) Then
                dlManner.SelectedValue = lMannerId.Text.Trim
            End If
        End If
    End Sub
    Private Sub GetMannerReceipt()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As docReceipts
        Try
            oType = New docReceipts

            ldata = oType.RetrieveReceipts
            lrow = ldata.NewRow
            lrow(0) = 0
            lrow(1) = ""
            ldata.Rows.InsertAt(lrow, 0)
            dlManner.DataSource = ldata
            dlManner.DataValueField = "ReceiptId"
            dlManner.DataTextField = "ReceiptDesc"
            dlManner.DataBind()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    'Private Sub GetUsers()

    '    Dim ldata As DataTable
    '    Dim lrow As DataRow
    '    Dim oUser As DocUser
    '    Try
    '        oUser = New DocUser

    '        ldata = oUser.UserList

    '        lrow = ldata.NewRow
    '        lrow(0) = ""
    '        lrow(1) = ""
    '        ldata.Rows.InsertAt(lrow, 0)
    '        dlUser.DataSource = ldata
    '        dlUser.DataValueField = "UserId"
    '        dlUser.DataTextField = "UserName"
    '        dlUser.DataBind()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '    End Try

    'End Sub
    Private Sub SetDocStatus()
        If lstatusid.Text <> "" Then
            Dim li As New WebControls.ListItem
            li = dlStatus.Items.FindByValue(lstatusid.Text.Trim)
            If Not IsNothing(li) Then
                dlStatus.SelectedValue = lstatusid.Text.Trim
            End If
        End If
    End Sub
    Private Sub RetrieveDocStatus()
        Dim ldata As DataTable
        Try
            Dim oDoc As New DocTypes
            oDoc.pGroupId = DocSession.sUserGroup
            ldata = oDoc.GetUserDocStatus

            dlStatus.DataSource = ldata
            dlStatus.DataTextField = "Description"
            dlStatus.DataValueField = "StatusId"
            dlStatus.DataBind()
            'dlStatus.SelectedValue = lstatusid.Text
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub

    Private Function IsApprover() As Boolean
        Dim oRoute As New DocRouting
        'If DocSession.sDocID IsNot Nothing Then
        Try
            oRoute.pDocId = DocSession.sDocID
            oRoute.pApproverId = DocSession.sUserId
            Return oRoute.IsApprover
            'If oRoute.RetrieveRouting().Rows.Count > 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try




    End Function


    Public Sub RetrieveDocIndex()
        'Dim oIndex As New DocIndex
        'oIndex.pDocId = DocSession.sDocID
        'oIndex.pDocType = DocSession.sDocType

        'rptIndex.DataSource = oIndex.RetrieveDocIndex()
        'rptIndex.DataBind()
        'pnlIndex.Update()
        If DocSession.sEditIndex <> "1" OrElse (DocSession.sCheckout = "Yes" AndAlso DocSession.sCheckOutBy <> DocSession.sUserId) Then
            ucDocIndex.pEnableFields = False
        Else
            'If DocSession.Archived Then
            If DocSession.sEditIndex <> "1" Then
                ucDocIndex.pEnableFields = False
            Else
                ucDocIndex.pEnableFields = True
            End If

        End If

        ucDocIndex.RetrieveDocIndex(DocSession.sDocID, DocSession.sDocType)
    End Sub


#Region "Repeater Methods"


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
#Region "Document Index Methods"
    Private Sub btSaveIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveIndex.Click
        SaveDocIndexValues()
    End Sub

    Public Sub SaveDocIndexValues()

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim lColId As Literal
        'Dim lColValue As TextBox
        ' Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn
        'Dim liCtr As Integer
        'Dim oIndex As New DocIndex

        'liCtr = 0
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            ucDocIndex.SaveIndex(DocSession.sDocID, DocSession.sDocType, objCommand)

            Master.ShowMessage("Document Index has been saved successfully.")
            'lcheckmsg.CssClass = "msg_green"
            ltr.pTran.Commit()

        Catch ex As Exception

            ltr.pTran.Rollback()
            Master.ShowMessage("There's an error while saving document index ('" & ex.Message & "') . Please try again.")
            'lcheckmsg.CssClass = "msg_red"
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
#End Region

    Private Sub btUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpload.Click
        'fileCheckIn.SaveAs()
        Try
            If fileCheckIn.HasFile Then
                If Len(fileCheckIn.FileName) > 200 Then
                    lcinmessage.Text = "** File name is too long (should not be more than 200 characters). Please rename the file before uploading."
                    Exit Sub
                End If
                Dim linfo As New System.IO.FileInfo(fileCheckIn.PostedFile.FileName)
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                Else
                    lcinmessage.Text = "** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted."
                    'pnlMsg.Update()
                    Exit Sub
                End If
                If fileCheckIn.PostedFile.ContentLength > DocSession.MaxFileSize Then
                    lcinmessage.Text = "** File size too big. Only a maximum of " + CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) + "MB is allowed."
                    Exit Sub
                End If

            End If
            If tbComments.Text.Trim = "" Then
                lcinmessage.Text = "** Comments is required."
                Exit Sub

            End If
            'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
            'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
            Dim lsFile As String = fileCheckIn.FileName

            Dim lsFilePath As String
            'lsFile = lsFile.Replace(" ", "_")
            lsFile = lsFile.Replace(",", "")
            lsFile = lsFile.Replace("&", "n")
            Dim oDocs As New DocList
            oDocs.pDocId = DocSession.sDocID
            oDocs.pVersion = CInt(DocSession.sVersion) + 1
            oDocs.pUserId = DocSession.sUserId
            oDocs.pComment = tbComments.Text
            oDocs.pFileName = lsFile
            oDocs.pIPAddress = Request.UserHostAddress

            oDocs.pFileSize = fileCheckIn.PostedFile.ContentLength.ToString
            If oDocs.pFileSize = "" Then
                oDocs.pFileSize = "0"
            End If
            oDocs.UpdateFileVersion()

            DocSession.sVersion = oDocs.pVersion
            DocSession.sDocFileName = getFile(oDocs.pVersion, lsFile)

            lsFilePath = DocSession.FileLoc & DocSession.sDocFileName

            fileCheckIn.SaveAs(lsFilePath)
            lcinmessage.Text = "File has been checked-in. Click on Reload button to refresh the screen."
            btCancelCheckout.Visible = False
            'lcinmessage.Text = "File has been checked-in."
            lcinmessage.CssClass = "msg_green"

            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Checkin"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Checkin file '" & lsFile & "'"
            ohist.AddHistory()

            btClose.Visible = True
            btUpload.Visible = False
            tbComments.Text = ""
            'imgCheckout.Visible = True
            If DocSession.sUserRole = "A" Then
                pCheckOut.Visible = True
            End If

            'imgCheckIn.Visible = False
            pCheckIn.Visible = False

        Catch ex As Exception
            lcinmessage.Text = "There's an error while saving the document. Please try again"
            lcinmessage.CssClass = "msg_red"
        End Try
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click

        pnlCheckin.Visible = False
        pInstuction.Visible = False
        pUpload1.Visible = False
        pUpload2.Visible = False
        pDoc.Visible = Not pDoc.Visible
        If lView.Visible Then
            docvw.Visible = True
        End If
    End Sub

#Region "Document Checkout/Checkin Methods"
    Private Sub ImgCheckout_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCheckout.Click
        CheckOutDoc()
    End Sub

    Private Sub lbCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCheckOut.Click
        CheckOutDoc()
    End Sub

    Private Sub CheckOutDoc()
        lAction.Text = "Checkout"
        pUpload1.Visible = True
        pUpload2.Visible = True
        pInstuction.Visible = True
        pnlCheckin.Visible = False
        pDoc.Visible = True

        If lView.Visible Then
            docvw.Visible = False
        End If
    End Sub

    Private Sub imgCheckIn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCheckIn.Click
        CheckInDoc()
    End Sub

    Private Sub CheckInDoc()
        lAction.Text = "Checkin"
        pUpload1.Visible = True
        pUpload2.Visible = True
        pInstuction.Visible = False
        pnlCheckin.Visible = True
        pDoc.Visible = True
        'tbTitle.Visible = True
        'lTitle.Visible = False
        'imgSave.Visible = True
        'pEdit.Visible = True 'george 02/13/2015 need to test
        If lView.Visible Then
            docvw.Visible = False
        End If
    End Sub

    Private Sub lbCheckin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCheckin.Click
        CheckInDoc()
    End Sub
#End Region

    Private Sub ClosePanel()
        'lcheckmsg.Text = ""
        'lcheckmsg.Visible = False

        pnlDocRouting.Visible = False
        pnlDocIndex.Visible = False

        pnlDocLinks.Visible = False
        pnlDocNotes.Visible = False
        pnlDocTags.Visible = False
        'pnlDocView.Visible = False
        pnlDocHistory.Visible = False
        'ucPager.Visible = False
        'pDocView.Visible = False   
        pnlVersion.Visible = False
        pnlAttachment.Visible = False
        pnlRoutingHistory.Visible = False

        pDocIndex.Update()
        pDocTags.Update()
        pDocLinks.Update()
        pDocView.Update()
        pDocRouting.Update()
        pDocNotes.Update()
        pDocHistory.Update()
        pAttachment.Update()
        pRoutingHistory.Update()

        pnlSubTask.Visible = False

        pSubTask.Update()

        'lnkAttach.Visible = False
    End Sub

    Private Sub ResetTab()
        'lcheckmsg.Text = ""
        'lcheckmsg.Visible = False
        TdRouting1.Attributes("class") = "unselLRTab"
        TdRouting2.Attributes("class") = "unselMidTab"
        TdRouting3.Attributes("class") = "unselLRTab"
        TdSubTask1.Attributes("class") = "unselLRTab"
        TdSubTask2.Attributes("class") = "unselMidTab"
        TdSubTask3.Attributes("class") = "unselLRTab"
        TdView1.Attributes("class") = "unselLRTab"
        TdView2.Attributes("class") = "unselMidTab"
        TdView3.Attributes("class") = "unselLRTab"
        TdIndex1.Attributes("class") = "unselLRTab"
        TdIndex2.Attributes("class") = "unselMidTab"
        TdIndex3.Attributes("class") = "unselLRTab"
        TdLinks1.Attributes("class") = "unselLRTab"
        TdLinks2.Attributes("class") = "unselMidTab"
        TdLinks3.Attributes("class") = "unselLRTab"
        TdNotes1.Attributes("class") = "unselLRTab"
        TdNotes2.Attributes("class") = "unselMidTab"
        TdNotes3.Attributes("class") = "unselLRTab"
        TdTags1.Attributes("class") = "unselLRTab"
        TdTags2.Attributes("class") = "unselMidTab"
        TdTags3.Attributes("class") = "unselLRTab"
        TdHistory1.Attributes("class") = "unselLRTab"
        TdHistory2.Attributes("class") = "unselMidTab"
        TdHistory3.Attributes("class") = "unselLRTab"
        TdAttach1.Attributes("class") = "unselLRTab"
        TdAttach2.Attributes("class") = "unselMidTab"
        TdAttach3.Attributes("class") = "unselLRTab"
        TdRoutingHistory1.Attributes("class") = "unselLRTab"
        TdRoutingHistory2.Attributes("class") = "unselMidTab"
        TdRoutingHistory3.Attributes("class") = "unselLRTab"
        TdSubTask1.Attributes("class") = "unselLRTab"
        TdSubTask2.Attributes("class") = "unselMidTab"
        TdSubTask3.Attributes("class") = "unselLRTab"

        lbHistory.Visible = True
        lHistory.Visible = False
        lbTags.Visible = True
        lTags.Visible = False
        lbNotes.Visible = True
        lNotes.Visible = False
        lbLinks.Visible = True
        lLinks.Visible = False
        lbIndex.Visible = True
        lIndex.Visible = False
        lbView.Visible = True
        lView.Visible = False
        lbRouting.Visible = True
        lRouting.Visible = False
        lbAttach.Visible = True
        lAttach.Visible = False
        lbRouteHistory.Visible = True
        lRouteHistory.Visible = False
        lbSubTask.Visible = True
        lSubTask.Visible = False
    End Sub

    Private Sub lbView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbView.Click
        ResetTab()

        TdView1.Attributes("class") = "selLTab"
        TdView2.Attributes("class") = "selMidTab"
        TdView3.Attributes("class") = "selRTab"
        lbView.Visible = False
        lView.Visible = True
        pnlTab.Update()
        ClosePanel()

        SelTabVersion()

        pnlVersion.Visible = True

        pDocView.Update()


    End Sub

    Private Sub lbRouting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRouting.Click

        SelTabRouting()

    End Sub

    Private Sub SelTabRouting()
        ResetTab()
        ucDocRouting.pAuthor = lAuthorID.Text
        ucDocRouting.ApplyArchive()
        Dim lodoc As New DocRouting
        ucDocRouting.pMaxSeqNo = lodoc.MaxRoutingSeqNo
        ucDocRouting.RetrieveDocRouting(DocSession.sDocID)
        TdRouting1.Attributes("class") = "selLTab"
        TdRouting2.Attributes("class") = "selMidTab"
        TdRouting3.Attributes("class") = "selRTab"
        lbRouting.Visible = False
        lRouting.Visible = True
        pnlTab.Update()
        ClosePanel()
        pnlDocRouting.Visible = True
        ucDocRouting.pDocId = DocSession.sDocID
        ucDocRouting.pAuthor = lAuthorID.Text
        ucDocRouting.ShowAddApprover()
        pDocRouting.Update()
    End Sub

    Private Sub lbRouteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRouteHistory.Click

        SelTabRoutingHistory()

    End Sub

    Private Sub SelTabRoutingHistory()
        ResetTab()
        ClosePanel()
        TdRoutingHistory1.Attributes("class") = "selLTab"
        TdRoutingHistory2.Attributes("class") = "selMidTab"
        TdRoutingHistory3.Attributes("class") = "selRTab"
        lbRouteHistory.Visible = False
        lRouteHistory.Visible = True

        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)
        RetRoutingHistory()
        pnlRoutingHistory.Visible = True
        'ucPager.Visible = True
        pRoutingHistory.Update()

        pnlTab.Update()
    End Sub
    Private Sub lbSubTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSubTask.Click

        SelTabSubTask()

    End Sub

    Private Sub SelTabSubTask()
        ResetTab()
        ClosePanel()
        TdSubTask1.Attributes("class") = "selLTab"
        TdSubTask2.Attributes("class") = "selMidTab"
        TdSubTask3.Attributes("class") = "selRTab"
        lbSubTask.Visible = False
        lSubTask.Visible = True


        uSTask.RetrieveSubTasks()
        pnlSubTask.Visible = True

        pSubTask.Update()

        pnlTab.Update()
    End Sub

    Private Sub SelTabVersion()
        ResetTab()
        ucVersion.pCreatedDate = lCreatedDate.Text
        ucVersion.pOfficeCode = DocSession.sUploaderOfc
        ucVersion.RetrieveVersion()

        TdView1.Attributes("class") = "selLTab"
        TdView2.Attributes("class") = "selMidTab"
        TdView3.Attributes("class") = "selRTab"
        lbView.Visible = False
        lView.Visible = True

        ClosePanel()
        pnlVersion.Visible = True
        pDocView.Visible = True
        pDocView.Update()
    End Sub

    Private Sub lbIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbIndex.Click

        SelTabIndex()

    End Sub
    Private Sub SelTabHistory()
        ResetTab()
        ClosePanel()
        TdHistory1.Attributes("class") = "selLTab"
        TdHistory2.Attributes("class") = "selMidTab"
        TdHistory3.Attributes("class") = "selRTab"
        lbHistory.Visible = False
        lHistory.Visible = True

        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)
        RetAction()
        pnlDocHistory.Visible = True
        'ucPager.Visible = True
        pDocHistory.Update()

        pnlTab.Update()
    End Sub
    Private Sub SelTabIndex()
        ResetTab()
        RetrieveDocIndex()
        TdIndex1.Attributes("class") = "selLTab"
        TdIndex2.Attributes("class") = "selMidTab"
        TdIndex3.Attributes("class") = "selRTab"
        lbIndex.Visible = False
        lIndex.Visible = True
        pnlTab.Update()
        ClosePanel()
        pnlDocIndex.Visible = True
        pDocIndex.Update()
        If DocSession.sEditIndex <> "1" OrElse (DocSession.sCheckout = "Yes" AndAlso DocSession.sCheckOutBy <> DocSession.sUserId) Then
            btSaveIndex.Visible = False
        Else
            If DocSession.sEditIndex <> "1" Then
                btSaveIndex.Visible = False
            Else
                btSaveIndex.Visible = True
            End If

        End If
    End Sub

    Private Sub lbLinks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLinks.Click
        ResetTab()
        ClosePanel()
        TdLinks1.Attributes("class") = "selLTab"
        TdLinks2.Attributes("class") = "selMidTab"
        TdLinks3.Attributes("class") = "selRTab"
        lbLinks.Visible = False
        lLinks.Visible = True
        UserDocLinks.RetrieveLinks()
        pnlDocLinks.Visible = True
        pDocLinks.Update()
        pnlTab.Update()
    End Sub

    Private Sub lbNotes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNotes.Click
        ResetTab()


        TdNotes1.Attributes("class") = "selLTab"
        TdNotes2.Attributes("class") = "selMidTab"
        TdNotes3.Attributes("class") = "selRTab"
        lbNotes.Visible = False
        lNotes.Visible = True
        pnlTab.Update()
        ClosePanel()
        UserDocNotes.RetrieveNotes()
        pnlDocNotes.Visible = True
        pDocNotes.Update()


    End Sub

    Private Sub lbTags_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbTags.Click
        ResetTab()
        ClosePanel()

        TdTags1.Attributes("class") = "selLTab"
        TdTags2.Attributes("class") = "selMidTab"
        TdTags3.Attributes("class") = "selRTab"
        lbTags.Visible = False
        lTags.Visible = True
        pnlDocTags.Visible = True


        'pMAddTag.Visible = True
        UControlTAG.pTitle = lTitle.Text
        UControlTAG.RetrieveTags()
        pDocTags.Update()

        pnlTab.Update()
    End Sub

    Private Sub lbHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbHistory.Click
        SelTabHistory()
    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        DocSession.sSelectedTab = "Version"
        Response.Redirect("view.aspx")
        'pnlCheckin.Visible = False
        'pInstuction.Visible = False
        'pUpload1.Visible = False
        'pUpload2.Visible = False
        'pDoc.Visible = Not pDoc.Visible
        'If lView.Visible Then
        '    docvw.Visible = True
        'End If
    End Sub

    'Private Sub DisplayDocVersion()
    '    DisplayDoc(DocSession.sDocID, ucVersion.pFileName, ucVersion.pVersion)
    'End Sub

    Private Sub DisplayEmailMessage()
        Master.ShowMessage(ucShare.pMessage)
    End Sub
    Private Sub ShowCancelWindow()
        ucCancel.Visible = True
    End Sub
    'Private Sub openDoc()
    '    DisplayDoc(UserDocLinks.DocID, UserDocLinks.FileName, UserDocLinks.FVersion)
    'End Sub
    'Private Sub DisplayDoc(ByVal asDocId As String, ByVal asFileName As String, ByVal asDocVersion As String)

    '    Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
    '    Dim lsFilePath As String = lsLoc & asDocId & "_" & asDocVersion & "_"
    '    'If System.IO.File.Exists(Server.MapPath("") & "\" & lsFilePath & asFileName) Then
    '    'DocSession.soldDocFileName = asDocId & "_" & asDocVersion & "_" & asFileName
    '    'Dim sYear As String = Year(CDate(lCreatedDate.Text)).ToString
    '    'Dim sMonth As String = MonthName(Month(CDate(lCreatedDate.Text)))
    '    'DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lrefno.Text & "\" & asDocId & "_" & asDocVersion & "_" & asFileName
    '    DocSession.sCurrentFile = asDocId & "_" & asDocVersion & "_" & asFileName
    '    'dlZoom.Visible = False
    '    'lZoom.Visible = False
    '    docvw.Visible = False
    '    'pnlImageDisp.Visible = False
    '    'pnlImg.Visible = False
    '    If System.IO.File.Exists(lsFilePath & asFileName) Then
    '        Dim lsext As String = System.IO.Path.GetExtension(lsFilePath & asFileName).ToLower

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
    '        Master.ShowMessage("File associated with this document does not exist on the server. Please contact administrator.")
    '    End If
    'End Sub

    

#Region "Download Methods"
    Private Sub Dfile()
        Dim lsFile As String = getCurrentFile()
        Dim linfo As New System.IO.FileInfo(lsFile)
        If linfo.Extension.ToLower = ".pdf" Then
            DownLoadPDFFile(lsFile)
        Else
            DownloadFile(lsFile)
        End If
        Dim ohist As New DocHistory
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Download"
        ohist.pUserId = DocSession.sUserId
        ohist.pAction = "Downloaded file '" & lFileName.Text & "'"
        ohist.AddHistory()
    End Sub
    Private Sub DownLoadPDFFile(ByVal psPath As String)
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim overContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0
        Dim lsMessage As String = ""
        'watermark variables

        Dim lsPath, lsFileName As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        lsFileName = DocSession.sCurrentFile
        Dim Encoding As New System.Text.UTF8Encoding()
        'lsPath = context.Request.QueryString("location") & lsFileName
        lsPath = DocSession.FileLoc & lsFileName
        Try


            If Not System.IO.File.Exists(lsPath) Then
                lsPath = DocSession.FileLoc2 & lsFileName
            End If
            'Dim lbpermitprint As Boolean
            'If context.Session("s_CanPrint") = "True" Then
            'lbpermitPrint = True 
            'lbpermitprint = True ' always false so printing can be handle on the page only
            'Else
            'lbpermitprint = False
            'End If


            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & DocSession.sFileName & ","
            Dim lss2 As String = "printed from the Document Management System " &
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") &
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

            'End If


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                Dim reader As New iTextSharp.text.pdf.PdfReader(lsPath)
                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                pageCount = reader.NumberOfPages()

                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
                Dim backgroundColor As BaseColor = New BaseColor(0, 0, 0)
                Dim fontColor As BaseColor = New BaseColor(0, 0, 0)

                'stamper.SetEncryption(Nothing, Encoding.GetBytes("docuvu"), PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                stamper.SetEncryption(Nothing, Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                'stamper.SetEncryption((Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                For ii As Integer = 1 To pageCount
                    rect = reader.GetPageSizeWithRotation(ii)
                    img.ScalePercent(100)
                    img.ScaleToFit(rect.Width, rect.Height)
                    X = (rect.Width - img.ScaledWidth) / 2
                    Y = (rect.Height - img.ScaledHeight) / 2
                    img.SetAbsolutePosition(X, Y)
                    overContent = stamper.GetOverContent(ii)
                    overContent.AddImage(img)
                    overContent.BeginText()
                    overContent.SetFontAndSize(font, 6.0F)
                    overContent.SetColorFill(fontColor)

                    overContent.SetTextMatrix(15, 18)
                    overContent.ShowText(lss)
                    overContent.SetTextMatrix(15, 10)
                    overContent.ShowText(lss2)
                    overContent.EndText()
                    'overContent.SaveState()
                    'overContent.RestoreState()

                Next
                stamper.Close()

                Dim cntnt As Byte() = outputStream.ToArray


                outputStream.Flush()
                outputStream.Close()
                outputStream.Dispose()
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & lFileName.Text.Replace(",", ""))
                ' Add a HTTP header to the output stream that contains the 
                ' content length(File Size). This lets the browser know how much data is being transfered
                Response.AddHeader("Content-Length", cntnt.Length.ToString())
                'Set the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then
                Master.ShowMessage("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                Master.ShowMessage(ex.Message)
                'Context.Response.Write("File is missing. Please contact the administrator.")
            End If

        End Try
    End Sub

    Private Sub DownloadFile(ByVal psPath As String)

        Dim tFDload As New System.IO.FileInfo(psPath)

        If System.IO.File.Exists(psPath) Then
            ' clear the current output content from the buffer
            LogHistory(tFDload.Name)

            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()

            Response.AddHeader("Content-Disposition", "attachment; filename=" + _
            tFDload.Name)

            Response.AddHeader("Content-Length", tFDload.Length.ToString())

            Response.ContentType = "application/octet-stream"

            Response.WriteFile(tFDload.FullName)

            Response.End()


        Else
            'MyBase.DisplayMessage("File does not exist.")
        End If

    End Sub

    Private Sub btDownload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btDownload.Click
        'Dim lsFilePath As String = DocSession.FileLoc & DocSession.sDocID & "_" & lDVVersion.Text & "_" & lDVFileName.Text
        Dfile()



    End Sub


    Private Sub DownLoadPDFFile2(ByVal psPath As String)
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim overContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0
        Dim lsMessage As String = ""
        'watermark variables
        Dim oDoc As DocEmail
        Dim lsPath, lsFileName As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        lsFileName = DocSession.sCurrentFile
        Dim Encoding As New System.Text.UTF8Encoding()
        'lsPath = context.Request.QueryString("location") & lsFileName
        lsPath = DocSession.FileLoc & lsFileName
        Try

            If Not System.IO.File.Exists(lsPath) Then
                lsPath = DocSession.FileLoc2 & lsFileName
            End If
            oDoc = New DocEmail


            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & DocSession.sFileName & ","
            Dim lss2 As String = "printed from the Document Management System " &
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") &
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

            'End If


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                Dim reader As New iTextSharp.text.pdf.PdfReader(lsPath)
                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                pageCount = reader.NumberOfPages()

                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
                Dim backgroundColor As BaseColor = New BaseColor(0, 0, 0)
                Dim fontColor As BaseColor = New BaseColor(0, 0, 0)

                'stamper.SetEncryption(Nothing, Encoding.GetBytes("docuvu"), PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH128BITS)
                stamper.SetEncryption(Nothing, Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                For ii As Integer = 1 To pageCount
                    rect = reader.GetPageSizeWithRotation(ii)
                    img.ScalePercent(100)
                    img.ScaleToFit(rect.Width, rect.Height)
                    X = (rect.Width - img.ScaledWidth) / 2
                    Y = (rect.Height - img.ScaledHeight) / 2
                    img.SetAbsolutePosition(X, Y)
                    overContent = stamper.GetOverContent(ii)
                    overContent.AddImage(img)
                    overContent.BeginText()
                    overContent.SetFontAndSize(font, 6.0F)
                    overContent.SetColorFill(fontColor)

                    overContent.SetTextMatrix(15, 18)
                    overContent.ShowText(lss)
                    overContent.SetTextMatrix(15, 10)
                    overContent.ShowText(lss2)
                    overContent.EndText()
                    'overContent.SaveState()
                    'overContent.RestoreState()

                Next
                stamper.Close()

                Dim cntnt As Byte() = outputStream.ToArray


                outputStream.Flush()
                outputStream.Close()
                outputStream.Dispose()
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & lFileName.Text)
                ' Add a HTTP header to the output stream that contains the 
                ' content length(File Size). This lets the browser know how much data is being transfered
                Response.AddHeader("Content-Length", cntnt.Length.ToString())
                'Set the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then
                Master.ShowMessage("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                Master.ShowMessage(ex.Message)
                'Context.Response.Write("File is missing. Please contact the administrator.")
            End If

        End Try
    End Sub

    

#End Region

    Private Function getFile(ByVal asVersion As String, ByVal asFileName As String) As String
        '--new folder
        Dim srefno As String
        Dim sMonth As String
        Dim sYear As String


        sMonth = MonthName(Month(CDate(lCreatedDate.Text)))
        sYear = Year(CDate(lCreatedDate.Text)).ToString
        srefno = lrefno.Text
        Dim lsFile As String = sYear & "\" & sMonth & "\" & srefno & "\" & DocSession.sDocID & "_" & asVersion & "_" & asFileName
        If Not System.IO.File.Exists(DocSession.FileLoc & lsFile) Then
            Return lsFile 'DocSession.sDocID & "_" & asVersion & "_" & asFileName
        Else
            Return lsFile
        End If

    End Function

    Private Function getCurrentFile() As String
        '--new folder
        Dim srefno As String
        Dim sMonth As String
        Dim sYear As String
        'Dim lsFile As String
        Dim lsFilename As String
        sMonth = MonthName(Month(CDate(lCreatedDate.Text)))
        sYear = Year(CDate(lCreatedDate.Text)).ToString
        srefno = lrefno.Text
        'lsFile = DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno & "\" & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text
        lsFilename = sYear & "\" & sMonth & "\" & srefno & "\" & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text
        If Not System.IO.File.Exists(DocSession.FileLoc & lsFilename) Then
            If Not System.IO.File.Exists(DocSession.FileLoc2 & lsFilename) Then
                Return DocSession.FileLoc & DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text
            Else
                Return DocSession.FileLoc2 & lsFilename
            End If
        Else
            Return DocSession.FileLoc & lsFilename
        End If

    End Function

    Private Sub imgHideShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHideShow.Click
        pnlDocInfo.Visible = Not pnlDocInfo.Visible
        If pnlDocInfo.Visible Then
            imgHideShow.ImageUrl = "images/show.png"

        Else
            imgHideShow.ImageUrl = "images/hide.png"

        End If

        pDocInfo.Update()
        'pDocInfo.Visible = Not pDocInfo.Visible
        pnlTab.Update()
    End Sub



    Private Sub imgEmail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEmail.Click
        ShareDoc()
    End Sub
    Private Sub ShareDoc()

        ucShare.ufReshEmail()
        If DocSession.sUserRole = "L" Or DocSession.sUserRole = "A" Then
            ucShare.LoadRecipients()
        End If


        ucShare.pFileName = getCurrentFile() 'lFileName.Text
        ucShare.pFile = lFileName.Text
        ucShare.Visible = True
        'PanelEmail1.Visible = True
        'PanelEmail2.Visible = True
        'lblEmailMsg.Text = ""
        'lblFrom.Text = DocSession.sUserEmail
        'lblAttachment.Text = lFileName.Text
        'ucEmail.RefreshEmail()
        'If docvw.Visible Then
        '    docvw.Visible = False
        '    hfRestorePDFViewer.Value = "Y"
        'Else
        '    hfRestorePDFViewer.Value = "N"
        'End If
    End Sub

    Private Sub lbShare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbShare.Click
        ShareDoc()
    End Sub

    Private Sub AddDoc()
        ucUpload.Visible = True
    End Sub
    Private Sub showAttach()
        If lAttach.Visible Then
            ucAtt.setRefresh = "y"
        End If
        ucAtt.pCreatedDate = lCreatedDate.Text
        ucAtt.Visible = True
    End Sub

    Private Sub showUpload()
        'If lAttach.Visible Then
        ucUp.pRefNo = lrefno.Text
        ucUp.pVersion = lVersion.Text
        ucUp.pCreatedDate = lCreatedDate.Text
        ucUp.setRefresh = "y"
        'End If
        ucUp.Visible = True
    End Sub

    

    Private Sub imgBack_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBack.Click
        If DocSession.DocumentPage = "alldocs.aspx" Then
            Response.Redirect("alldocs.aspx")
        ElseIf DocSession.DocumentPage = "newdocs.aspx" Then
            Response.Redirect("newdocs.aspx")
        ElseIf DocSession.DocumentPage = "sent.aspx" Then
            Response.Redirect("sent.aspx")
        ElseIf DocSession.DocumentPage = "issuances.aspx" Then
            Response.Redirect("issuances.aspx")
        Else
            Response.Redirect("inbox.aspx")
        End If

    End Sub


    Private Sub lbEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEdit.Click
        'GetUsers()
        RetrieveDocTypes()
        UpdateDoc(True)

    End Sub

    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click
        RetrieveDocTypes()
        UpdateDoc(True)
    End Sub
    Private Function GetDateIssued() As String
        Dim oDoc As DocList
        Try
            oDoc = New DocList
            oDoc.pDocId = DocSession.sDocID
            Return oDoc.RetrieveDateIssued
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Sub UpdateDoc(ByVal abUpd As Boolean)

        tbTitle.Visible = abUpd
        lTitle.Visible = Not abUpd
        dlDocType.Visible = abUpd
        dlOfficeCode.Visible = abUpd
        lToNewOffice.Visible = abUpd
        lDocName.Visible = Not abUpd
        dlStatus.Visible = abUpd
        dlClassification.Visible = abUpd
        lToClassification.Visible = abUpd
        'lClassification.Visible = abUpd
        'dlFinalStatus.Visible = abUpd
        'lDocStat.Visible = Not abUpd
        lToNewStatus.Visible = abUpd
        lsendername.Visible = Not abUpd
        lNoPages.Visible = Not abUpd
        lNoCopies.Visible = Not abUpd
        tbNoCopies.Visible = abUpd
        tbNoPages.Visible = abUpd
        dlRequestType.Visible = abUpd
        dlManner.Visible = abUpd
        tbSenderName.Visible = abUpd
        lToNewRequestType.Visible = abUpd
        lTOManner.Visible = abUpd
        'lStatus.Visible = Not abUpd
        If DocSession.sUserRole <> "U" Then
            tbDueDate.Visible = abUpd
            lOrigDueLabel.Visible = abUpd
        End If

        tbNoCopies.Visible = abUpd
        tbReturnCard.Visible = abUpd
        lReturnCard.Visible = Not abUpd
        pMUpdate.Visible = abUpd
        If DocSession.sUserRole = "A" OrElse DocSession.sUserGroup = "CRD" Then
            lReceivedBy.Visible = Not abUpd
            tbReceivedBy.Visible = abUpd
            lReceivedDate.Visible = Not abUpd
            tbReceivedDate.Visible = abUpd
            lReceivedTime.Visible = Not abUpd
            tbReceivedTime.Visible = abUpd

            'If tbReceivedDate.Text = "" Then
            '    tbReceivedDate.Text = ""

            'End If
        End If
        If DocSession.HideConfidential Then
        Else
            If DocSession.sUserId = lAuthorID.Text Then
                cbConf.Visible = False
                lConf.Visible = True
            Else
                cbConf.Visible = abUpd
                lConf.Visible = Not abUpd
            End If
        End If

    End Sub
    Private Function UpdateData() As Boolean
        If dlDocType.SelectedValue.Trim.ToLower <> DocSession.sDocType.Trim.ToLower Then
            Return True
        End If
        If (dlStatus.SelectedValue.Trim.ToLower <> "0" AndAlso dlStatus.SelectedValue.Trim.ToLower <> lstatusid.Text.Trim.ToLower) Then
            Return True
        End If
        If (dlRequestType.SelectedValue.Trim.ToLower <> "" AndAlso dlRequestType.SelectedValue.Trim.ToLower <> lRequestType.Text.Trim.ToLower) Then
            Return True
        End If
        If (dlManner.SelectedValue.Trim.ToLower <> "" AndAlso dlManner.SelectedValue.Trim.ToLower <> lMannerId.Text.Trim.ToLower) Then
            Return True
        End If
        If (dlOfficeCode.SelectedValue.Trim.ToLower <> "0" AndAlso dlOfficeCode.SelectedValue.Trim.ToLower <> lsOfficeCode.Text.Trim.ToLower) Then
            Return True
        End If
        If lTitle.Text.Trim.ToLower <> tbTitle.Text.Trim.ToLower Then
            Return True
        End If
        If lsendername.Text.Trim.ToLower <> tbSenderName.Text.Trim.ToLower Then
            Return True
        End If
        If tbNoPages.Text.Trim.ToLower <> lNoPages.Text.Trim.ToLower Then
            Return True
        End If
        If tbNoCopies.Text.Trim.ToLower <> lNoCopies.Text.Trim.ToLower Then
            Return True
        End If
        If lClassificationCode.Text.ToLower <> dlClassification.SelectedValue.Trim.ToLower Then
            Return True
        End If
        If lOrigDue.Text.Trim.ToLower <> tbDueDate.Text.Trim.ToLower Then
            Return True
        End If
        If lMannerId.Text.Trim.ToLower <> dlManner.SelectedValue Then
            Return True
        End If
        If lReturnCard.Text.Trim.ToLower <> tbReturnCard.Text.Trim.ToLower Then
            Return True
        End If
        If lReceivedBy.Text.Trim.ToLower <> tbReceivedBy.Text.Trim.ToLower Then
            Return True
        End If
        If lReceivedDate.Text.Trim.ToLower <> tbReceivedDate.Text.Trim.ToLower Then
            Return True
        End If
        If lReceivedTime.Text.Trim.ToLower <> tbReceivedTime.Text.Trim.ToLower Then
            Return True
        End If
        If lReturnCard.Text.Trim.ToLower <> tbReturnCard.Text.Trim.ToLower Then
            Return True
        End If
        If IIf(cbConf.Checked, "Yes", "No") <> lConf.Text.Trim.ToLower Then
            Return True
        End If
        Return False

    End Function
    Private Sub btSaveUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveUpdate.Click
        If UpdateData() Then

            pConfirm.Visible = True
            pnlConfirm.Update()
            Master.ShowImageDocument = True
        Else
            Master.ShowMessage("No changes to save.")
            'UpdateDocument()
        End If

    End Sub

    Private Function ValidData() As Boolean
        If tbDueDate.Text.Trim <> "" AndAlso Not IsDate(tbDueDate.Text) Then
            Master.ShowMessage("Invalid Due Date")
            Return False
        End If
        If tbReceivedDate.Text.Trim <> "" AndAlso Not IsDate(tbReceivedDate.Text) Then
            Master.ShowMessage("Invalid Received Date")
            Return False
        End If

    End Function

    'Private Sub EmailPointPerson()
    '    uEmail.Visible = True
    '    upEmail.Update()
    'End Sub

    Private Sub UpdateDocument()
        Dim oDoc As New DocList

        Dim lbUpdate As Boolean
        Dim lRetention As String = ""
        Dim lbDTUpdate As Boolean = False
        Dim lbDSUpdate As Boolean = False
        Dim lsRetention As String = ""
        Dim lsAction As String = ""
        Try

            lbUpdate = False
            oDoc.pDocTitle = ""
            oDoc.pDocStatus = ""
            If lTitle.Text.Trim.ToLower <> tbTitle.Text.Trim.ToLower Then

                oDoc.pDocTitle = tbTitle.Text
                lsAction = "Updated document title from '" & Replace(lTitle.Text, "'", "''") & "' to '" & tbTitle.Text & "'"

                lbUpdate = True

            End If

            If dlDocType.SelectedValue.Trim.ToLower <> DocSession.sDocType.Trim.ToLower Then
                lbDTUpdate = True
                If lsAction <> "" Then
                    lsAction = lsAction & " and Document Type from '" & Replace(lDocName.Text, "'", "''") & "' to '" & dlDocType.SelectedItem.Text & "'"
                Else
                    lsAction = "Updated the Document Type of document '" & DocSession.sDocTitle & "' from '" & lDocName.Text & "' to '" & dlDocType.SelectedItem.Text & "'"
                End If

                lbUpdate = True

            End If
            If dlRequestType.SelectedValue.Trim.ToLower <> "" AndAlso dlRequestType.SelectedValue.Trim.ToLower <> lRequestType.Text.Trim.ToLower Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Request Type from '" & lRequestDescription.Text & "' to '" & dlRequestType.SelectedItem.Text & "'"
                Else
                    lsAction = "Updated the Request Type of document '" & DocSession.sDocTitle & "' from " & lRequestDescription.Text & " to " & dlRequestType.SelectedItem.Text & "'"
                End If

                lbUpdate = True
            End If
            If dlManner.SelectedValue.Trim.ToLower <> "" AndAlso dlManner.SelectedValue.Trim.ToLower <> lMannerId.Text.Trim.ToLower Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Manner of Request from '" & lManner.Text & "' to '" & dlManner.SelectedItem.Text & "'"
                Else
                    lsAction = "Updated the Manner of Request of document '" & DocSession.sDocTitle & "' from '" & lManner.Text & "' to '" & dlManner.SelectedItem.Text & "'"
                End If

                lbUpdate = True
            End If
            If dlStatus.SelectedValue.Trim.ToLower <> "0" AndAlso dlStatus.SelectedValue.Trim.ToLower <> lstatusid.Text.Trim.ToLower Then
                'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                lbDSUpdate = True
                If lsAction <> "" Then
                    lsAction = lsAction & " and Status from " & lStatus.Text & " to " & dlStatus.SelectedItem.Text
                Else
                    lsAction = "Updated the Document Status of document '" & DocSession.sDocTitle & "' from " & lStatus.Text & " to " & dlStatus.SelectedItem.Text
                End If
                lbUpdate = True
                'if archived
                lRetention = ""
                Dim sDateIssued As String = GetDateIssued()
                If dlStatus.SelectedValue = "8" Then



                    If sDateIssued <> "" Then
                        lsRetention = "Retention Active Period started on " & DateTime.Now.ToShortDateString
                        lRetention = "C"
                    End If
                Else
                    If lstatusid.Text = "8" Then
                        If sDateIssued <> "" Then
                            lsRetention = "Retention Active Period was removed due to change of status from Archive to " & dlStatus.SelectedItem.Text
                            lRetention = "R"
                        End If
                    End If
                End If
            End If
            If dlOfficeCode.SelectedValue.Trim.ToLower <> "0" AndAlso dlOfficeCode.SelectedValue.Trim.ToLower <> lsOfficeCode.Text.Trim.ToLower Then
                'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                If lsAction <> "" Then
                    lsAction = lsAction & " and Office from '" & lsOfficeName.Text & "' to '" & dlOfficeCode.SelectedItem.Text & "'"
                Else
                    lsAction = "Updated the Office of document '" & DocSession.sDocTitle & "' from '" & lsOfficeName.Text & "' to '" & dlOfficeCode.SelectedItem.Text & "'"
                End If

                lbUpdate = True
            End If
            If tbDueDate.Text.Trim = "" AndAlso lOrigDue.Text.Trim = "Not set" Then

            ElseIf tbDueDate.Text.Trim = "" AndAlso IsDate(lOrigDue.Text.Trim) Then
                If lsAction <> "" Then
                    lsAction = lsAction & " and Due Date from '" & lOrigDue.Text & "' to blank"
                Else
                    lsAction = "Updated Due Date from '" & lOrigDue.Text & "' to blank"
                End If
                lbUpdate = True
            Else
                If tbDueDate.Text.Trim <> lOrigDue.Text.Trim Then
                    'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                    If lsAction <> "" Then
                        lsAction = lsAction & " and Due Date from '" & lOrigDue.Text & "' to '" & tbDueDate.Text & "'"
                    Else
                        lsAction = "Updated Due Date from '" & lOrigDue.Text & "' to '" & tbDueDate.Text
                    End If

                    lbUpdate = True
                End If
            End If

            If tbSenderName.Text.Trim <> lsendername.Text.Trim Then
                'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                If lsAction <> "" Then
                    lsAction = lsAction & " and Sender from '" & lsendername.Text & "' to '" & tbSenderName.Text & "'"
                Else
                    lsAction = "Updated Sender from '" & lsendername.Text & "' to '" & tbSenderName.Text & "'"

                End If

                lbUpdate = True
            End If

            If tbNoCopies.Text.Trim <> lNoCopies.Text.Trim Then
                'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                If lsAction <> "" Then
                    lsAction = lsAction & " and No. of Copies from '" & lNoCopies.Text & "' to '" & tbNoCopies.Text & "'"
                Else
                    lsAction = "Updated No. of Copies from '" & lNoCopies.Text & "' to '" & tbNoCopies.Text & "'"

                End If

                lbUpdate = True
            End If
            If tbNoPages.Text.Trim <> lNoPages.Text.Trim Then
                'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
                If lsAction <> "" Then
                    lsAction = lsAction & " and No. of Copies from '" & lNoPages.Text & "' to '" & tbNoPages.Text & "'"
                Else
                    lsAction = "Updated No. of Copies from '" & lNoPages.Text & "' to '" & tbNoPages.Text & "'"

                End If

                lbUpdate = True
            End If
            'If tbSenderName.Text.Trim <> lsendername.Text.Trim Then
            '    'oDoc.pDocStatus = dlStatus.SelectedValue.Trim
            '    If lsAction <> "" Then
            '        lsAction = lsAction & " and Sender from '" & lsendername.Text & "' to '" & tbSenderName.Text & "'"
            '    Else
            '        lsAction = "Updated Sender from '" & lsendername.Text & "' to '" & tbSenderName.Text & "'"

            '    End If

            '    lbUpdate = True
            'End If

            If dlClassification.SelectedValue.Trim.ToLower <> lClassificationCode.Text.Trim.ToLower Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Classification from '" & lClassification.Text & "' to '" & dlClassification.SelectedItem.Text & "'"
                Else
                    lsAction = "Updated Document Classification from '" & lClassification.Text & "' to '" & dlClassification.SelectedItem.Text & "'"
                End If
                lbUpdate = True
            End If
            If tbReturnCard.Text.Trim <> lReturnCard.Text.Trim Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Return Card from '" & lReturnCard.Text & "' to '" & tbReturnCard.Text & "'"
                Else
                    lsAction = "Updated Return Card from '" & lReturnCard.Text & "' to '" & tbReturnCard.Text & "'"

                End If

                lbUpdate = True
            End If

            If tbReceivedBy.Text.Trim <> lReceivedBy.Text.Trim Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Received By from " & lReceivedBy.Text & "' to '" & tbReceivedBy.Text & "'"
                Else
                    lsAction = "Updated Received By from " & lReceivedBy.Text & "' to '" & tbReceivedBy.Text & "'"
                End If

                lbUpdate = True
            End If

            If tbReceivedDate.Text.Trim <> lReceivedDate.Text.Trim Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Received Date from '" & lReceivedDate.Text & "' to '" & tbReceivedDate.Text & "'"
                Else
                    lsAction = "Updated Received Date from '" & lReceivedDate.Text & "' to '" & tbReceivedDate.Text & "'"
                End If

                lbUpdate = True
            End If

            If tbReceivedTime.Text.Trim <> lReceivedTime.Text.Trim Then

                If lsAction <> "" Then
                    lsAction = lsAction & " and Received Time from '" & lReceivedTime.Text & "' to '" & tbReceivedTime.Text & "'"
                Else
                    lsAction = "Updated Received Time from '" & lReceivedTime.Text & "' to '" & tbReceivedTime.Text & "'"
                End If

                lbUpdate = True
            End If

            If IIf(cbConf.Checked, "Yes", "No") <> lConf.Text.Trim Then

                If lsAction <> "" Then
                    If cbConf.Checked Then
                        lsAction = lsAction & " and Set the document to Confidential "
                    Else
                        lsAction = lsAction & " and Removed the Confidential tag"
                    End If

                Else
                    If cbConf.Checked Then
                        lsAction = "Set the document to Confidential "
                    Else
                        lsAction = "Removed the Confidential tag"
                    End If
                End If

                lbUpdate = True
            End If
            If lbUpdate Then
                If lbDSUpdate OrElse lbDTUpdate Then
                    Dim oDT As New DocTypes
                    oDT.pDocType = dlDocType.SelectedValue
                    Dim oData As DataTable = oDT.GetDocTypeRetentionInfo
                    If oData.Rows.Count > 0 Then
                        If IsTrue(oData(0)("EnableRetention").ToString.Trim) Then
                            If IsTrue(oData(0)("UseCreatedDate").ToString.Trim) Then
                                oDoc.pSetRetention = "Y"
                            Else
                                If oData(0)("RetentionStatus") <> "0" AndAlso (oData(0)("RetentionStatus").ToString.Trim = dlStatus.SelectedValue.Trim) Then
                                    oDoc.pSetRetention = "C"
                                Else
                                    oDoc.pSetRetention = "R"
                                End If
                            End If
                        Else
                            oDoc.pSetRetention = "R"
                        End If
                    End If
                End If
                oDoc.pDocId = DocSession.sDocID
                oDoc.pUserId = DocSession.sUserId
                oDoc.pIPAddress = Request.UserHostAddress
                oDoc.pDocType = dlDocType.SelectedValue
                oDoc.pDocTypeOrig = DocSession.sDocType
                If dlStatus.SelectedValue <> "0" AndAlso dlStatus.SelectedValue.Trim.ToLower <> lstatusid.Text.Trim.ToLower Then
                    oDoc.pDocStatus = dlStatus.SelectedValue
                    Dim lsCloseStatus As String = "," & DocSession.CompleteStatus() & ","
                    If lsCloseStatus.IndexOf(oDoc.pDocStatus) >= 0 Then
                        lCdate.Text = Date.Now.ToShortDateString()
                    End If
                End If

                'oDoc.pFinalDocStatus = dlFinalStatus.SelectedValue
                oDoc.pRequestType = dlRequestType.SelectedValue
                oDoc.pManner = dlManner.SelectedValue
                oDoc.pClassificationCode = dlClassification.SelectedValue
                oDoc.pOfficeCode = dlOfficeCode.SelectedValue
                oDoc.pDueDate = tbDueDate.Text
                oDoc.pSenderName = tbSenderName.Text
                oDoc.pNoCopies = tbNoCopies.Text
                oDoc.pNoPages = tbNoPages.Text
                oDoc.pReturnCard = tbReturnCard.Text
                oDoc.pReceivedDate = tbReceivedDate.Text
                oDoc.pReceivedTime = tbReceivedTime.Text
                oDoc.pReceivedBy = tbReceivedBy.Text
                oDoc.pSetRetention = lRetention
                oDoc.pConfidential = IIf(cbConf.Checked, "1", "0")


                oDoc.UpdateDoc()


                lDocName.Text = dlDocType.SelectedItem.Text
                lClassification.Text = dlClassification.SelectedItem.Text
                'lDocName.Visible = True
                'dlDocType.Visible = False
                DocSession.sDocType = dlDocType.SelectedValue
                'lcheckmsg.CssClass = "msg_green"
                'lcheckmsg.Text = "Document has been updated successfully."
                Master.ShowMessage("Document has been updated successfully.")
                lTitle.Text = tbTitle.Text
                lsendername.Text = tbSenderName.Text
                lNoCopies.Text = tbNoCopies.Text
                lNoPages.Text = tbNoPages.Text
                lsOfficeName.Text = dlOfficeCode.SelectedItem.Text
                lsOfficeCode.Text = dlOfficeCode.SelectedValue
                'lTitle.Visible = True
                'tbTitle.Visible = False
                'lStatus.Text = dlStatus.SelectedItem.Text
                'lstatusid.Text = dlStatus.SelectedValue
                DocSession.sDocTitle = lTitle.Text
                'lStatus.Text = dlStatus.SelectedItem.Text
                'lstatusid.Text = dlStatus.SelectedValue

                If dlStatus.SelectedValue <> "0" Then
                    SetStatus(dlStatus.SelectedValue, dlStatus.SelectedItem.Text)
                End If

                lRequestType.Text = dlRequestType.SelectedValue
                lRequestDescription.Text = dlRequestType.SelectedItem.Text

                lMannerId.Text = dlManner.SelectedValue
                lManner.Text = dlManner.SelectedItem.Text
                lReceivedBy.Text = tbReceivedBy.Text.Trim
                lReceivedTime.Text = tbReceivedTime.Text
                lReceivedDate.Text = tbReceivedDate.Text
                lConf.Text = IIf(cbConf.Checked, "Yes", "No")
                lReturnCard.Text = tbReturnCard.Text
                DocSession.sDocStatus = lstatusid.Text
                lOrigDue.Text = tbDueDate.Text
                Dim ohist As New DocHistory

                ohist.pAction = lsAction
                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Edit"
                ohist.pUserId = DocSession.sUserId
                ohist.AddHistory()
                'ucDocHistory.RetrieveDocAction(DocSession.sDocID)
                'retention disposable schedule
                If lRetention <> "" Then
                    ohist.pAction = lsRetention
                    ohist.pDocId = DocSession.sDocID
                    ohist.pIpAddress = Request.UserHostAddress
                    ohist.pTask = "Edit"
                    ohist.pUserId = DocSession.sUserId
                    ohist.AddHistory()
                End If
                
                RetAction()
                pDocHistory.Update()

            End If
            UpdateDoc(False)
            upMenu.Update()
            pDocInfo.Update()
            'pDocInfo2.Update()
            If Left(dlStatus.SelectedItem.ToString.Trim.ToLower, 7) = "archive" AndAlso lbUpdate Then
                imgEmailPointPerson.Visible = True
            End If
        Catch ex As Exception
            'lcheckmsg.CssClass = "msg_red"
            'lcheckmsg.Text = "There's an error while updating the document title ( " & ex.Message & " ). Please try again."
            'lcheckmsg.Visible = True
            Master.ShowMessage("There's an error while updating the document properties ( " & ex.Message & " ). Please try again.")
            pDocInfo.Update()
        Finally

        End Try

    End Sub

    Private Sub btCancelUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancelUpdate.Click
        'lcheckmsg.Text = ""
        'lcheckmsg.Visible = False
        UpdateDoc(False)
        upMenu.Update()
        pDocInfo.Update()
        'pDocInfo2.Update()
    End Sub

    Private Sub lbDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDownload.Click
        Dfile()
    End Sub

    Private Sub LogHistory(ByVal asFile As String)
        Dim ohist As New DocHistory

        ohist.pAction = "Downloaded file " & asFile & " from document '" & DocSession.sDocTitle & "'"
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Download"
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()
        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)
        RetAction()


    End Sub

    Public Sub DocTypeImage(ByVal docExt As String)
        Dim lext As String = "images/doctype/"

        If (docExt = ".doc" OrElse docExt = "docx") Then
            lext = lext & "wordlogo.png"
        ElseIf (docExt = ".xls" OrElse docExt = "xlsx") Then
            lext = lext & "excellogo.png"
        ElseIf (docExt = ".ppt" OrElse docExt = "pptx") Then
            lext = lext & "pptlogo.png"
            'ElseIf (docExt = ".jpg" OrElse docExt = "jpeg") Then
            '    lext = lext & "jpg.png"
            'ElseIf (docExt = ".tif" OrElse docExt = "tiff") Then
            '    lext = lext & "tif.png"
            'ElseIf (docExt = ".gif") Then
            '    lext = lext & "gif.png"
            'ElseIf (docExt = ".bmp") Then
            '    lext = lext & "bmp.png"
            'ElseIf (docExt = ".png") Then
            '    lext = lext & "png.png"
        ElseIf (docExt = ".pdf") Then
            lext = lext & "pdflogo.png"
        ElseIf (docExt = ".zip") OrElse (docExt = ".rar") Then
            lext = lext & "ziplogo.png"
        Else
            lext = "viewDoc.aspx?dt=" & Date.Now()
        End If
        'hlOpenDoc.ImageUrl = lext
        hlOpen.ImageUrl = lext
        'Return lext
    End Sub

    Private Sub imgBook_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBook.Click
        Dim bm As DocBookmark = New DocBookmark
        bm.pDocId = CInt(DocSession.sDocID)
        bm.pUserId = DocSession.sUserId
        bm.DocBookmark("A")
        ucb.RetrieveBookmark()
        'DocBookmark(CInt(DocSession.sDocID), "0")
        imgBook.Visible = Not imgBook.Visible
        imgBookM.Visible = Not imgBookM.Visible
        upnlBkMark.Update()
    End Sub

    Private Sub imgBookM_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBookM.Click
        Dim bm As DocBookmark = New DocBookmark
        bm.pDocId = CInt(DocSession.sDocID)
        bm.pUserId = DocSession.sUserId
        bm.DocBookmark("D")
        'DocBookmark(CInt(DocSession.sDocID), "1")
        imgBook.Visible = Not imgBook.Visible
        imgBookM.Visible = Not imgBookM.Visible
        upnlBkMark.Update()
    End Sub

    

    Private Sub btContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btContinue.Click
        pConfirm.Visible = False
        pnlConfirm.Update()

        UpdateDocument()
        Master.ShowImageDocument = False
    End Sub

    Private Sub btSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveCancel.Click
        pConfirm.Visible = False
        UpdateDoc(False)
        pnlConfirm.Update()

        Master.ShowImageDocument = False
    End Sub

    Private Sub imgSaveCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSaveCancel.Click
        pConfirm.Visible = False
        pnlConfirm.Update()
        Master.ShowImageDocument = False
    End Sub

    Private Sub imgReceipt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgReceipt.Click
        If uReceipt.pRoutedTo = "" Then
            Master.ShowMessage("You cannot re-print acknowledged receipt because the document was not routed.")
        Else
            'If DocSession.sCanPrintReceipt Then

            PrintReceipt()
            'Else
            '    Master.ShowMessage("You don't have access to re-print the acknowledged receipt.")
            'End If
        End If

    End Sub
    Private Sub imgBRouteSlip_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBRouteSlip.Click
        'If uBlankRoute.pRoutedTo = "" Then
        '    Master.ShowMessage("You cannot re-print routing slip because the document was not routed.")
        'Else
        'If DocSession.sCanPrintReceipt Then

        PrintBlankRoute()
            'Else
        '    Master.ShowMessage("You don't have access to re-print the acknowledged receipt.")
        'End If
        'End If

    End Sub
    Private Sub imgRouteSlip_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRouteSlip.Click
        'If uReceiptRoute.pRoutedTo = "" Then
        '    Master.ShowMessage("You cannot re-print routing slip because the document was not routed.")
        'Else
        'If DocSession.sCanPrintReceipt Then

        PrintReceiptRoute()
        'Else
        '    Master.ShowMessage("You don't have access to re-print the acknowledged receipt.")
        'End If
        'End If

    End Sub


    Private Sub lbReceipt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbReceipt.Click
        If uReceipt.pRoutedTo = "" Then
            Master.ShowMessage("You cannot re-print acknowledged receipt because the document was not routed.")
        Else
            'If DocSession.sCanPrintReceipt Then
            PrintReceipt()
            'Else
            '    Master.ShowMessage("You don't have access to re-print the acknowledged receipt.")
            'End If
        End If
    End Sub

    Private Sub PrintReceipt()
        uReceipt.SetInfo()
        uReceipt.pCreatedBy = lAuthor.Text
        uReceipt.pCreatedDate = lCreatedDate.Text
        uReceipt.pDocTitle = lTitle.Text
        uReceipt.pRefNo = lrefno.Text
        uReceipt.pSender = lsendername.Text
        uReceipt.pCopiesPages = GenerateCopies(lNoCopies.Text, lNoPages.Text)
        uReceipt.pCarbonDisplay = True
        uReceipt.pRoutedToDisplay = True
        uReceipt.pCarbon = RetrieveCC()
        uReceipt.ShowReceipt()
        uReceipt.Visible = True
        uReceipt.ShowRemarks()
    End Sub

    Private Sub PrintReceiptRoute()
        uReceiptRoute.SetInfo()
        uReceiptRoute.pCreatedBy = lAuthor.Text
        uReceiptRoute.pCreatedDate = lCreatedDate.Text
        uReceiptRoute.pDocTitle = lTitle.Text
        uReceiptRoute.pRefNo = lrefno.Text
        uReceiptRoute.pSender = lsendername.Text
        uReceiptRoute.pCopiesPages = GenerateCopies(lNoCopies.Text, lNoPages.Text)
        uReceiptRoute.pCarbonDisplay = True
        uReceiptRoute.pRoutedToDisplay = True
        uReceiptRoute.pCarbon = RetrieveCC()
        uReceiptRoute.ShowReceipt()
        uReceiptRoute.Visible = True
        uReceiptRoute.ShowRemarks()
    End Sub

    Private Sub PrintBlankRoute()
        uBlankRoute.SetInfo()
        uBlankRoute.pCreatedBy = lAuthor.Text
        uBlankRoute.pCreatedDate = lCreatedDate.Text
        uBlankRoute.pDocTitle = lTitle.Text
        uBlankRoute.pRefNo = lrefno.Text
        uBlankRoute.pSender = lsendername.Text
        uBlankRoute.pCopiesPages = GenerateCopies(lNoCopies.Text, lNoPages.Text)
        uBlankRoute.pCarbonDisplay = True
        uBlankRoute.pRoutedToDisplay = True
        uBlankRoute.pCarbon = RetrieveCC()
        uBlankRoute.ShowReceipt()
        uBlankRoute.Visible = True
        uBlankRoute.ShowRemarks()
    End Sub
    Private Sub PrintBlankRoute2()
        uBlankRoute2.SetInfo()
        uBlankRoute2.pCreatedBy = lAuthor.Text
        uBlankRoute2.pCreatedDate = lCreatedDate.Text
        uBlankRoute2.pDocTitle = lTitle.Text
        uBlankRoute2.pRefNo = lrefno.Text
        uBlankRoute2.pSender = lsendername.Text
        uBlankRoute2.pCopiesPages = GenerateCopies(lNoCopies.Text, lNoPages.Text)
        uBlankRoute2.pCarbonDisplay = True
        uBlankRoute2.pRoutedToDisplay = True
        uBlankRoute2.pCarbon = RetrieveCC()
        uBlankRoute2.ShowReceipt()
        uBlankRoute2.Visible = True
        uBlankRoute2.ShowRemarks()
    End Sub
    Private Sub PrintBlankReceipt()
        uReceipt.pCreatedBy = ""
        uReceipt.pCreatedDate = ""
        uReceipt.pDocTitle = ""
        uReceipt.pRefNo = ""
        uReceipt.pSender = ""
        uReceipt.pCopiesPages = "" 'GenerateCopies(lNoCopies.Text, lNoPages.Text)
        uReceipt.pCarbonDisplay = False
        uReceipt.pRoutedToDisplay = False
        uReceipt.ShowReceipt()
        uReceipt.Visible = True
    End Sub

    Private Sub PrintReply()
        uReply.pDocId = DocSession.sDocID
        uReply.ShowDocument()
        uReply.Visible = True
    End Sub

    Private Sub imgDelDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelDoc.Click
        ucCon.Visible = True
        'Master.ShowImageDocument = True
    End Sub

    Private Sub ucCon_e_ok_click() Handles ucCon.e_ok_click
        Try
            Dim oDoc As DocList
            oDoc = New DocList
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            'oDoc.ArchiveDoc()
            oDoc.DeleteDoc()
            Dim ohist As New DocHistory
            ohist.pDocId = "0"
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Delete"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Deleted document " & lTitle.Text & ""
            ohist.AddHistory()
            Master.ShowMessage("Document has been deleted successfully.")
            Response.Redirect("inbox.aspx")
        Catch ex As Exception
            Master.ShowMessage(ex.Message)
        End Try
        'Master.ShowImageDocument = False
    End Sub

    Private Sub imgArchiveTrue_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgArchiveTrue.Click
        fArchive()
    End Sub

    Private Sub fArchive()
        Try
            Dim oDoc As DocList
            oDoc = New DocList
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pDocStatus = "8" ' archive
            oDoc.UpdateDoc()
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Archive"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Archived document '" & lTitle.Text & "'"
            ohist.AddHistory()
            Master.ShowMessage("Document has been archived successfully.")
            lStatus.Text = "Archived"
            lStatus.ToolTip = "Archived By: " & DocSession.sUserName
            lstatusid.Text = "8"
            DocSession.sDocStatus = "8"
            pDocInfo.Update()
            LoadDoc()
            pnlTab.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub lbArchiveTrue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbArchiveTrue.Click
        fArchive()
    End Sub
    Private Sub ucCancel_e_ok_click() Handles ucCancel.e_ok_click
        Try
            Dim oDoc As DocRouting
            oDoc = New DocRouting
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pSeqNo = ucDocRouting.pMaxSeqNo
            'oDoc.ArchiveDoc()
            oDoc.CancelRouting()
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Cancel"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Cancel document routing"
            ohist.AddHistory()
            Master.ShowMessage("Document routing has been canceled successfully.")
        Catch ex As Exception
            Master.ShowMessage(ex.Message)

        End Try


        'Master.ShowImageDocument = False
    End Sub
    Private Sub SetStatus(ByVal siD As String, ByVal sDesc As String)
        lStatus.Text = sDesc
        lstatusid.Text = siD
        
        Dim lsCloseStatus As String = "," & DocSession.CompleteStatus() & ","
        'If lStatus.Text.Trim.ToLower = "no action required" OrElse lStatus.Text.Trim.ToLower = "archived" OrElse lStatus.Text.Trim.ToLower = "returned" Then
        'If siD = "18" OrElse siD = "8" OrElse siD = "12" OrElse siD = "19" Then
        If lsCloseStatus.IndexOf("," & siD & ",") >= 0 Then
            lDocStat.Text = "Completed/Closed"
            lDocStat.ToolTip = lCdate.Text
        Else
            lDocStat.Text = "Open"
            lDocStat.ToolTip = ""
        End If
        pStatusUpd.Update()
        'dlFinalStatus.SelectedValue = asFinalStatus
        'dlStatus.SelectedValue = siD
    End Sub

    Private Sub ucDocRouting_e_UpdateStatus() Handles ucDocRouting.e_UpdateStatus
        SetStatus(ucDocRouting.pStatusId, ucDocRouting.pStatusDesc)
        pDocInfo.Update()
    End Sub

    Private Sub lbDelDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelDoc.Click
        ucCon.Visible = True
    End Sub

    Private Sub lbAttach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAttach.Click
        ResetTab()
        ClosePanel()

        TdAttach1.Attributes("class") = "selLTab"
        TdAttach2.Attributes("class") = "selMidTab"
        TdAttach3.Attributes("class") = "selRTab"
        lbAttach.Visible = False
        lAttach.Visible = True
        pnlAttachment.Visible = True
        RetrieveAttach()
        pnlTab.Update()
    End Sub
    Private Sub ReloadView()
        Response.Redirect("view.aspx")
    End Sub
    Private Sub RetrieveAttach()
        uControlAttach.RetrieveAttachment()
        'lnkAttach.Visible = True
        uControlAttach.pCreatedDate = lCreatedDate.Text
        pAttachment.Update()
    End Sub
    Private Sub imgAttachment_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAttachment.Click

        showAttach()
    End Sub

    Private Sub lbAttachment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAttachment.Click
        showAttach()
    End Sub

    'Private Sub ucActedUpon_e_check() Handles ucActedUpon.e_check
    '    Try

    '        Dim lsStat As String = ""
    '        Dim lsMsg As String = ""
    '        If ucActedUpon.BoxCheck Then
    '            lsStat = "10"
    '            lsMsg = "Acted Upon"
    '            ucInProcess.BoxCheck = False
    '            ucForRelease.BoxCheck = False
    '        Else
    '            lsStat = "4"
    '            lsMsg = "For Evaluation"
    '        End If
    '        DocSession.sDocStatus = lsStat
    '        Dim olist As New DocList
    '        olist.pDocId = DocSession.sDocID
    '        olist.pUserId = DocSession.sUserId
    '        olist.pDocStatus = lsStat
    '        olist.UpdateDoc()
    '        Master.ShowMessage("Document status has been set to '" & lsMsg & "'.")

    '        SetStatus(lsStat, lsMsg)
    '        'lstatusid.Text = lsStat
    '        'dlStatus.SelectedValue = lsStat
    '        'lStatus.Text = lsMsg
    '        Dim ohist As New DocHistory
    '        ohist.pDocId = DocSession.sDocID
    '        ohist.pIpAddress = Request.UserHostAddress
    '        ohist.pTask = "Edit"
    '        ohist.pUserId = DocSession.sUserId
    '        ohist.pAction = "Set document status to " & lsMsg
    '        ohist.AddHistory()
    '        'ucInProcess.Visible = False
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while updating document status (" & ex.Message & "). Please try again.")
    '    End Try
    'End Sub

    'Private Sub rbForApproval_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbForApproval.CheckedChanged
    '    Try

    '        Dim lsStat As String = ""
    '        Dim lsMsg As String = ""
    '        If rbForApproval.Checked Then
    '            lsStat = "7"
    '            lsMsg = "For Approval"
    '            rbActedUpon.Checked = False
    '            rbForRelease.Checked = False
    '        Else
    '            lsStat = "4"
    '            lsMsg = "For Evaluation"
    '        End If
    '        DocSession.sDocStatus = lsStat
    '        Dim olist As New DocList
    '        olist.pDocId = DocSession.sDocID
    '        olist.pUserId = DocSession.sUserId
    '        olist.pDocStatus = lsStat
    '        olist.UpdateDoc()
    '        Master.ShowMessage("Document status has been set " & lsMsg & ".")
    '        lstatusid.Text = lsStat
    '        dlStatus.SelectedValue = lsStat
    '        lStatus.Text = lsMsg
    '        Dim ohist As New DocHistory
    '        ohist.pDocId = DocSession.sDocID
    '        ohist.pIpAddress = Request.UserHostAddress
    '        ohist.pTask = "Edit"
    '        ohist.pUserId = DocSession.sUserId
    '        ohist.pAction = "Set document status to " & lsMsg
    '        ohist.AddHistory()
    '        'ucInProcess.Visible = False
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while updating document status (" & ex.Message & "). Please try again.")
    '    End Try
    'End Sub



    'Private Sub rbForRelease_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbForRelease.CheckedChanged
    '    Try

    '        Dim lsStat As String = ""
    '        Dim lsMsg As String = ""
    '        If rbForRelease.Checked Then
    '            lsStat = "10"
    '            lsMsg = "For Release"
    '            rbActedUpon.Checked = False
    '            rbForApproval.Checked = False
    '        Else
    '            lsStat = "4"
    '            lsMsg = "For Evaluation"
    '        End If
    '        DocSession.sDocStatus = lsStat
    '        Dim olist As New DocList
    '        olist.pDocId = DocSession.sDocID
    '        olist.pUserId = DocSession.sUserId
    '        olist.pDocStatus = lsStat
    '        olist.UpdateDoc()
    '        Master.ShowMessage("Document status has been set " & lsMsg & ".")
    '        lstatusid.Text = lsStat
    '        dlStatus.SelectedValue = lsStat
    '        lStatus.Text = lsMsg
    '        Dim ohist As New DocHistory
    '        ohist.pDocId = DocSession.sDocID
    '        ohist.pIpAddress = Request.UserHostAddress
    '        ohist.pTask = "Edit"
    '        ohist.pUserId = DocSession.sUserId
    '        ohist.pAction = "Set document status to " & lsMsg
    '        ohist.AddHistory()
    '        'ucInProcess.Visible = False
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while updating document status (" & ex.Message & "). Please try again.")
    '    End Try
    'End Sub
    'Private Sub ucInProcess_e_check() Handles ucInProcess.e_check
    '    Try

    '        Dim lsStat As String = ""
    '        Dim lsMsg As String = ""
    '        If ucInProcess.BoxCheck Then
    '            lsStat = "7"
    '            lsMsg = "For Approval"
    '            ucActedUpon.BoxCheck = False
    '            ucForRelease.BoxCheck = False
    '        Else
    '            lsStat = "4"
    '            lsMsg = "For Evaluation"
    '        End If
    '        DocSession.sDocStatus = lsStat
    '        Dim olist As New DocList
    '        olist.pDocId = DocSession.sDocID
    '        olist.pUserId = DocSession.sUserId
    '        olist.pDocStatus = lsStat
    '        olist.UpdateDoc()
    '        Master.ShowMessage("Document status has been set to '" & lsMsg & "'.")
    '        SetStatus(lsStat, lsMsg)
    '        'lstatusid.Text = lsStat
    '        'dlStatus.SelectedValue = lsStat
    '        'lStatus.Text = lsMsg
    '        Dim ohist As New DocHistory
    '        ohist.pDocId = DocSession.sDocID
    '        ohist.pIpAddress = Request.UserHostAddress
    '        ohist.pTask = "Edit"
    '        ohist.pUserId = DocSession.sUserId
    '        ohist.pAction = "Set document status to " & lsMsg
    '        ohist.AddHistory()
    '        'ucInProcess.Visible = False
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while updating document status (" & ex.Message & "). Please try again.")
    '    End Try

    'End Sub
    'Private Sub ucForRelease_e_check() Handles ucForRelease.e_check
    '    Try

    '        Dim lsStat As String = ""
    '        Dim lsMsg As String = ""
    '        If ucForRelease.BoxCheck Then
    '            lsStat = "11"
    '            lsMsg = "For Release"
    '            ucInProcess.BoxCheck = False
    '            ucActedUpon.BoxCheck = False
    '        Else
    '            lsStat = "4"
    '            lsMsg = "For Evaluation"
    '        End If
    '        DocSession.sDocStatus = lsStat
    '        Dim olist As New DocList
    '        olist.pDocId = DocSession.sDocID
    '        olist.pUserId = DocSession.sUserId
    '        olist.pDocStatus = lsStat
    '        olist.UpdateDoc()
    '        Master.ShowMessage("Document status has been set to '" & lsMsg & "'.")
    '        SetStatus(lsStat, lsMsg)
    '        'lstatusid.Text = lsStat
    '        'dlStatus.SelectedValue = lsStat
    '        'lStatus.Text = lsMsg
    '        Dim ohist As New DocHistory
    '        ohist.pDocId = DocSession.sDocID
    '        ohist.pIpAddress = Request.UserHostAddress
    '        ohist.pTask = "Edit"
    '        ohist.pUserId = DocSession.sUserId
    '        ohist.pAction = "Set document status to " & lsMsg
    '        ohist.AddHistory()
    '        'ucInProcess.Visible = False
    '    Catch ex As Exception
    '        Master.ShowMessage("Error occurred while updating document status (" & ex.Message & "). Please try again.")
    '    End Try

    'End Sub

    Private Sub lbUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUpload.Click
        showUpload()
    End Sub

    Private Sub lbReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbReply.Click
        PrintReply()
    End Sub

    Private Sub imgReply_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgReply.Click
        PrintReply()
    End Sub

    Private Sub lbAddSubTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddSubTask.Click
        ucCreateSubTask.Visible = True
    End Sub

    Private Sub CreateSubTask()
        Dim odoc As DocList
        Dim srefno As String
        Dim objCommand As clsSqlConn
        Dim otran As DbTran
        Try
            odoc = New DocList
            'odoc.ExistsInInbox()


            odoc.pDocId = DocSession.getNextID("DocId")
            odoc.pParentDocId = DocSession.sDocID
            srefno = DocSession.sReferenceNo & odoc.GetMaxSuffix()
            odoc.pRefNo = srefno
            odoc.pIPAddress = Request.UserHostAddress

            otran = New DbTran
            objCommand = New clsSqlConn(otran.pTran)

            odoc.CreateSubTask(objCommand)



            odoc.pUserId = DocSession.sUserId
            odoc.pSeqNo = "0"
            odoc.AddToInbox(objCommand) ', odoc.pExistsInInbox)

            Dim ohist As New DocHistory
            ohist.pAction = "Created Subtask " & srefno
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Subtask"
            ohist.pUserId = DocSession.sUserId
            ohist.AddHistory(objCommand)
            otran.pTran.Commit()
            Master.ShowMessage("Subtask " & srefno & " created successfully.")
        Catch ex As Exception
            If Not otran Is Nothing Then
                otran.pTran.Rollback()
            End If
            Master.ShowMessage("Error occurred while creating (" & ex.Message & "). Please try again.")
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
            If Not otran Is Nothing Then
                otran.Dispose()
                otran = Nothing
            End If
        End Try
    End Sub



    Private Sub imgBReceipt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBReceipt.Click
        PrintBlankReceipt()
    End Sub

    Private Sub lbBReceipt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBReceipt.Click
        PrintBlankReceipt()
    End Sub

    Private Sub dlManner_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlManner.SelectedIndexChanged
        If dlManner.SelectedValue = "3" OrElse dlManner.SelectedItem.Text = "Courrier" Then
            pnlRet.Visible = True
        End If
    End Sub

    Private Sub Page_SaveStateComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SaveStateComplete

    End Sub
#Region "Miscellaneos Functions"
    Private Function IsTrue(ByVal asVal As String) As Boolean
        If asVal.Trim = "True" OrElse asVal.Trim = "1" Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Function CTrue(ByVal asVal As String) As String
        If asVal.Trim = "True" OrElse asVal.Trim = "1" Then
            Return "True"
        Else
            Return "False"
        End If

    End Function

#End Region

    Private Sub DeleteSubTask()
        uSTask.DeleteSubTask()

        uSTask.RetrieveSubTasks()
        pSubTask.Update()
    End Sub

    Private Sub ucCreateSubTask_e_ok_click() Handles ucCreateSubTask.e_ok_click

        Try
            CreateSubTask()
            uSTask.RetrieveSubTasks()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btCancelCheckout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancelCheckout.Click
        Try
            'lcheckmsg.Text = ""
            Dim oDoc As New DocList
            oDoc.pDocId = DocSession.sDocID
            oDoc.pUserId = DocSession.sUserId
            oDoc.pIPAddress = Request.UserHostAddress
            oDoc.CancelCheckOutDoc()
            'If pnlDocHistory.Visible Then
            'RetAction()
            'End If
            lCheckoutBy.Text = ""
            lIsCheckOut.Text = "No"
            'lcheckmsg.Text = "Please click on Reload button to refresh View screen."
            Master.ShowMessage("File Check-Out has been cancelled. Please click on Reload button to refresh View screen.")
            'lcheckmsg.CssClass = "msg_green"
            btClose.Visible = True
            btCancelCheckout.Visible = False
            btUpload.Visible = False
            If lView.Visible Then
                docvw.Visible = True
            End If
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Checkout"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Check-Out has been cancelled."
            imgClose.Visible = False

            ohist.AddHistory()
            pDocView.Update()
            pDocInfo.Update()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbConf_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbConf.CheckedChanged
        SetConfidentialBackground()
    End Sub
    Private Sub SetConfidentialBackground()
        If cbConf.Checked Then
            pnlDocInfo.Style("background-image") = "url('images/confidential_stamp.png')"
        Else
            pnlDocInfo.Style("background-image") = "none"
        End If
    End Sub

    Private Sub lbBRouteSlip_Click(sender As Object, e As EventArgs) Handles lbBRouteSlip.Click
        PrintBlankRoute()
    End Sub

    Private Sub lbRouteSlip_Click(sender As Object, e As EventArgs) Handles lbRouteSlip.Click
        PrintReceiptRoute()
    End Sub

    Private Sub imgBRouteSlip2_Click(sender As Object, e As ImageClickEventArgs) Handles imgBRouteSlip2.Click
        PrintBlankRoute2()
    End Sub

    Private Sub lbBRouteSlip2_Click(sender As Object, e As EventArgs) Handles lbBRouteSlip2.Click
        PrintBlankRoute2()
    End Sub

End Class
