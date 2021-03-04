Imports System
Imports System.Web
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api

Public Class UserControlAttachment
    Inherits System.Web.UI.UserControl

    Public Event e_click()
    Public Event e_attach()
    Public DocFileName As String
    Public AttachedBy As String
    Public AttachedDate As String
    Dim liTotal As Long = 0

#Region "Code for displaying error message in Master page"
    Dim smsg As String
    Public Event e_ShowMessage()
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property
    Private Sub ErrorMsg(ByVal asMsg As String)
        Message = asMsg
        RaiseEvent e_ShowMessage()
    End Sub
#End Region
    Public Event e_Count()
    Public ReadOnly Property pCount As String
        Get
            Return hfCount.value
        End Get
    End Property
    Public Property pDocFileName As String
        Get
            Return DocFileName
        End Get
        Set(ByVal value As String)
            DocFileName = value
        End Set
    End Property
    Public Property pAttachedBy As String
        Get
            Return AttachedBy
        End Get
        Set(ByVal value As String)
            AttachedBy = value
        End Set
    End Property
    Public Property pAttachedDate As String
        Get
            Return AttachedDate
        End Get
        Set(ByVal value As String)
            AttachedDate = value
        End Set
    End Property

    'Dim CreatedDate As String
    Public Property pCreatedDate As String
        Get
            Return hfCreatedDate.Value
        End Get
        Set(ByVal value As String)
            hfCreatedDate.Value = value
        End Set
    End Property

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CountAttach()

        End If
    End Sub

    Private Sub DownloadDoc(ByVal psPath As String)

        Dim tFDload As New System.IO.FileInfo(psPath)

        If System.IO.File.Exists(psPath) Then
            ' clear the current output content from the buffer
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
    
    Private Sub ShareDoc(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
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

    Public Sub SaveChanges(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Dim oList As DocAttachment
        Dim lsOld As String = ""
        Dim lsNew As String = ""
        Dim lbFileError As Boolean = False
        Try
            
            ri = DirectCast(DirectCast(sender, TextBox).NamingContainer, RepeaterItem)
            'If NewNameExist(DirectCast(ri.FindControl("lFN"), Literal).Text) Then
            '    Exit Sub
            'End If
            lsNew = DirectCast(sender, TextBox).Text
            lsOld = DirectCast(ri.FindControl("lFN"), Literal).Text
            lbFileError = UpdateFileName(lsNew, lsOld)
            If lbFileError = False Then
                If DocSession.sCanView = "True" OrElse DocSession.sUserId = DirectCast(ri.FindControl("lA"), Literal).Text Then

                    'DirectCast(ri.FindControl("lF"), LinkButton).Visible = True
                    DirectCast(ri.FindControl("hlNewTab"), HyperLink).Visible = True
                    DirectCast(ri.FindControl("lFN"), Literal).Visible = False
                Else
                    'DirectCast(ri.FindControl("lF"), LinkButton).Visible = False
                    DirectCast(ri.FindControl("hlNewTab"), HyperLink).Visible = False
                    DirectCast(ri.FindControl("lFN"), Literal).Visible = True
                End If
                DirectCast(ri.FindControl("imgSelect"), ImageButton).Visible = True
                DirectCast(ri.FindControl("imgSelected"), ImageButton).Visible = False
                oList = New DocAttachment
                oList.pDocId = DocSession.sDocID
                oList.pAttachId = DirectCast(ri.FindControl("lAI"), Literal).Text
                oList.pDocFileName = lsNew
                oList.pOldDocFileName = DirectCast(ri.FindControl("lFN"), Literal).Text
                oList.pIPAddress = Request.UserHostAddress
                DirectCast(sender, TextBox).Visible = False
                oList.UpadateAttachment()

                DirectCast(ri.FindControl("lFN"), Literal).Text = lsNew
                'DirectCast(ri.FindControl("lF"), LinkButton).Text = lsNew
                DirectCast(ri.FindControl("hlNewTab"), HyperLink).Text = lsNew
            End If


        Catch ex As Exception

            'If lbFileError = False Then
            '    UpdateFileName(lsOld, lsNew)
            'End If
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If
        End Try

    End Sub
    'Private Sub RenameFile(ByVal pFileName As String)
    '    If System.IO.File.Exists(DocSession.FileLoc & "previewdocs\" & pFileName) Then
    '        Dim sYear As String
    '        Dim sMonth As String
    '        sYear = Year(CDate(DocSession..pCreatedDate)).ToString()
    '        sMonth = MonthName(Month(CDate(oIndex.pCreatedDate)))
    '        If Not System.IO.Directory.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno) Then
    '            System.IO.Directory.CreateDirectory(DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno)
    '        End If
    '        System.IO.File.Move(DocSession.FileLoc & "previewdocs\" & hfFileUploaded.Value, DocSession.FileLoc & sYear & "\" & sMonth & "\" & srefno & "\" & liRet & "_1_" & lsFile)
    '    End If
    'End Sub
    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
        Dim ri As RepeaterItem
        Try
            If DocSession.sUserRole = "A" Then
                ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)

                DirectCast(ri.FindControl("tbFileName"), TextBox).Visible = True
                DirectCast(ri.FindControl("tbFileName"), TextBox).Focus()
                DirectCast(ri.FindControl("tbFileName"), TextBox).Text = DirectCast(ri.FindControl("lFN"), Literal).Text
                DirectCast(ri.FindControl("lFN"), Literal).Visible = False
                'DirectCast(ri.FindControl("lF"), LinkButton).Visible = False
                DirectCast(ri.FindControl("hlNewTab"), HyperLink).Visible = False

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If
        End Try

    End Sub
    Public Function UpdateFileName(ByVal pFileName As String, ByVal pOldFileName As String) As Boolean
        Try

            Dim sYear As String = Year(CDate(pCreatedDate))
            Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
            'Dim lsCurrentFile As String = sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName
            Dim lsCurrentFile As String = "attachment\" & DocSession.sDocID & "_" & pFileName
            Dim lsOldFile As String = "attachment\" & DocSession.sDocID & "_" & pOldFileName
            If System.IO.File.Exists(DocSession.FileLoc & lsCurrentFile) Then
                If Not System.IO.File.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    System.IO.File.Move(DocSession.FileLoc & lsCurrentFile, DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False
                Else
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                End If
            ElseIf System.IO.File.Exists(DocSession.FileLoc & lsOldFile) Then
                If Not System.IO.File.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    System.IO.File.Move(DocSession.FileLoc & lsOldFile, DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False
                Else
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                End If
            ElseIf System.IO.File.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pOldFileName) Then
                If System.IO.File.Exists(DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                Else
                    System.IO.File.Move(DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pOldFileName, DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False

                End If

            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsCurrentFile) Then
                If Not System.IO.File.Exists(DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    System.IO.File.Move(DocSession.FileLoc2 & lsCurrentFile, DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False
                Else
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                End If
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsOldFile) Then
                If Not System.IO.File.Exists(DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    System.IO.File.Move(DocSession.FileLoc2 & lsOldFile, DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False
                Else
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                End If
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pOldFileName) Then
                If System.IO.File.Exists(DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName) Then
                    ErrorMsg("New File name (" & pFileName & ") already exist in the attachment folder. Please check with the administrator.")
                    Return True
                Else
                    System.IO.File.Move(DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pOldFileName, DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName)
                    Return False

                End If
            Else
                Return False
            End If

        Catch ex As Exception
            ErrorMsg("Error occurred while renaming attachment (" & ex.Message & "). Please try again.")
            Return True
        Finally

        End Try

    End Function
    Public Function NewNameExist(ByVal pFileName As String) As Boolean
        Try

            Dim sYear As String = Year(CDate(pCreatedDate))
            Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
            Dim lsCurrentFile As String = sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & pFileName
            If System.IO.File.Exists(DocSession.FileLoc & lsCurrentFile) Then
                ErrorMsg("New Attachment File Name already exist in the server. Please try another name.")
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ErrorMsg("Error occurred while Renaming attachment (" & ex.Message & "). Please try again.")
            Return True
        Finally

        End Try

    End Function
    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
        Dim ri As RepeaterItem
        Try
            ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)

            DirectCast(ri.FindControl("tbFileName"), TextBox).Visible = False
            'DirectCast(ri.FindControl("tbFileName"), TextBox).Focus()
            'If DocSession.docDisable Then
            'DirectCast(e.Item.FindControl("hlNewWindow"), HyperLink).NavigateUrl = "viewfile.aspx?d_id=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text & "&att=y"
            If DocSession.sDocType.ToLower = "letters" OrElse DocSession.sCanView = "True" OrElse DocSession.sUserId = DirectCast(ri.FindControl("lA"), Literal).Text Then
                DirectCast(ri.FindControl("lFN"), Literal).Visible = False
                'DirectCast(ri.FindControl("lF"), LinkButton).Visible = True
                DirectCast(ri.FindControl("hlNewTab"), HyperLink).Visible = True
            Else

                'DirectCast(ri.FindControl("lF"), LinkButton).Visible = False
                DirectCast(ri.FindControl("hlNewTab"), HyperLink).Visible = False
                DirectCast(ri.FindControl("lFN"), Literal).Visible = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If
        End Try
    End Sub

    'Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btnDownload As ImageButton = DirectCast(sender, ImageButton)
    '    'Dim NewScriptManager As ScriptManagerProxy = DirectCast(Me.FindControl("ScriptManagerProxy1"), ScriptManagerProxy)
    '    'Dim NewScriptManager2 As ScriptManager = DirectCast(NewScriptManager.FindControl("ScriptManager1"), ScriptManager)
    '    'NewScriptManager2.RegisterPostBackControl(btnDownload)
    'End Sub
    Private Sub rptAttachment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAttachment.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("iDA"), ImageButton).Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            If DocSession.docDisable Then
                'DirectCast(e.Item.FindControl("hlNewWindow"), HyperLink).NavigateUrl = "viewfile.aspx?d_id=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text & "&att=y"
                If DocSession.sDocType.ToLower = "letters" OrElse DocSession.sCanView = "True" Then
                    'DirectCast(e.Item.FindControl("lF"), LinkButton).Visible = True
                    DirectCast(e.Item.FindControl("hlNewTab"), HyperLink).Visible = True
                    DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = True
                    DirectCast(e.Item.FindControl("lFN"), Literal).Visible = False
                Else
                    'DirectCast(e.Item.FindControl("lF"), LinkButton).Visible = False
                    DirectCast(e.Item.FindControl("hlNewTab"), HyperLink).Visible = False
                    DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = False
                    DirectCast(e.Item.FindControl("lFN"), Literal).Visible = True
                End If
                DirectCast(e.Item.FindControl("iD"), ImageButton).Visible = False
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
            Else
                'If DocSession.sUserRole = "A" OrElse DocSession.sDeleteDoc = "1" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lA"), Literal).Text Then
                If DocSession.sUserRole = "A" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lA"), Literal).Text Then
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
                End If
                If DocSession.sCanDownload = "True" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lA"), Literal).Text Then
                    DirectCast(e.Item.FindControl("iD"), ImageButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("iD"), ImageButton).Visible = False
                End If
                If DocSession.sCanView = "True" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lA"), Literal).Text Then
                    'DirectCast(e.Item.FindControl("lF"), LinkButton).Visible = True
                    DirectCast(e.Item.FindControl("hlNewTab"), HyperLink).Visible = True
                    If DocSession.sCanPrintReceipt = "True" Then
                        DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = True
                    Else
                        DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = False
                    End If

                    DirectCast(e.Item.FindControl("lFN"), Literal).Visible = False
                Else
                    'DirectCast(e.Item.FindControl("lF"), LinkButton).Visible = 
                    If DocSession.sUserRole = "A" Then
                        DirectCast(e.Item.FindControl("hlNewTab"), HyperLink).Visible = True
                        DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = True
                    Else
                        DirectCast(e.Item.FindControl("hlNewTab"), HyperLink).Visible = False
                        DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).Visible = False
                    End If

                    DirectCast(e.Item.FindControl("lFN"), Literal).Visible = True
                End If


            End If

            'link for viewing and printing
            Dim hlink As HyperLink = DirectCast(e.Item.FindControl("hlNewTab"), HyperLink)
            Dim lsPrintFile As String = Server.HtmlDecode(DirectCast(e.Item.FindControl("lFN"), Literal).Text)
            Dim lsYear As String = Year(CDate(pCreatedDate))
            Dim lsMonth As String = MonthName(Month(CDate(pCreatedDate)))

            Dim hlink2 As HyperLink = DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink)
            Dim lsFile As String = lsYear & "\" & lsMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & lsPrintFile
            Dim lbFileExist As Boolean = False
            If hlink.Visible OrElse hlink2.Visible Then
                lbFileExist = FileExists(lsFile)
            End If

            Dim lec As New crypt
            If hlink.Visible Then
                If lbFileExist Then
                    hlink.NavigateUrl = "viewfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&att=Y&v_no=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text
                Else
                    DirectCast(e.Item.FindControl("iD"), ImageButton).Enabled = False
                    DirectCast(e.Item.FindControl("iD"), ImageButton).ToolTip = "File does not exist. *"
                    'If hlink.Visible Then
                    '    hlink.NavigateUrl = DocSession.LocalPath & "/viewfilelocal.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&att=Y&v_no=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text &
                    '                "&r_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sReferenceNo & "dbm")) & "&o=" &
                    '                IIf(DocSession.sOwner, "1", "") & "&d=" & IIf(DocSession.sCanDownloadDoc = "True", "1", "") & "&r=" & DocSession.sUserRole & "&u=" & Server.UrlEncode(lec.Encrypt(DocSession.sUserId & "smd"))
                    'End If
                End If
            End If


            If DocSession.sCanView = "True" Then
                If Right(lsPrintFile, 4) = ".doc" OrElse Right(lsPrintFile, 4) = "docx" OrElse
                        Right(lsPrintFile, 4) = ".xls" OrElse Right(lsPrintFile, 4) = "xlsx" OrElse
                        Right(lsPrintFile, 4) = ".ppt" OrElse Right(lsPrintFile, 4) = "pptx" Then


                    hlink2.Visible = False

                End If

                If hlink2.Visible = True Then
                    If lbFileExist Then
                        hlink2.NavigateUrl = "printfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&att=Y&v_no=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text
                    Else
                        'If DocSession.IsLocalDoc = "Y" And Not DocSession.sIsLocal Then
                        'hlink2.NavigateUrl = DocSession.LocalPath & "/printfilelocal.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&att=Y&v_no=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text &
                        '        "&r_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sReferenceNo & "dbm")) & "&o=" &
                        '        IIf(DocSession.sOwner, "1", "") & "&d=" & IIf(DocSession.sCanDownloadDoc = "True", "1", "") & "&r=" & DocSession.sUserRole & "&u=" & Server.UrlEncode(lec.Encrypt(DocSession.sUserId & "smd"))
                        'Else
                        '    DirectCast(e.Item.FindControl("hlNewTab2"), HyperLink).NavigateUrl = "printfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&att=Y&v_no=" & DirectCast(e.Item.FindControl("lAI"), Literal).Text
                        'End If
                        'hlNewTab2.NavigateUrl = "printfile.aspx?d_id=" & Server.UrlEncode(lec.Encrypt(DocSession.sDocID & "dbm")) & "&v_no=" & lVersion.Text
                    End If
                End If
                'End If



            End If
            'end--
            liTotal = liTotal + CLng(DirectCast(e.Item.FindControl("lFileSize"), Literal).Text)
                DirectCast(e.Item.FindControl("lFileSize"), Literal).Text = FormatBytes(CLng(DirectCast(e.Item.FindControl("lFileSize"), Literal).Text))
            If DocSession.sOfcCode = "CRD" OrElse DocSession.sUserGroup = "CRD_Archiving" Then
                DirectCast(e.Item.FindControl("imgExpand"), ImageButton).Visible = True
            Else
                DirectCast(e.Item.FindControl("imgExpand"), ImageButton).Visible = True
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
                DirectCast(e.Item.FindControl("ltotalfilesize"), Literal).Text = "Total File Size: " & FormatBytes(liTotal)
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
    'Public Sub showDoc(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rItems As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
    '    pDocFileName = Server.HtmlDecode(DirectCast(rItems.FindControl("lF"), LinkButton).Text)

    '    RaiseEvent e_click()


    'End Sub

    'Public Sub showVersionInfo(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Dim rItems As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
    '    'lDVFileName.Text = Server.HtmlDecode(DirectCast(rItems.FindControl("lFileName"), Literal).Text)

    '    'DocSession.soldDocFileName = lDVFileName.Text
    '    'lDVUploadedBy.Text = DirectCast(rItems.FindControl("lUploadedByName"), Literal).Text
    '    'lDVUploadedDate.Text = DirectCast(rItems.FindControl("lUploadedDate"), Literal).Text
    '    'lDVComments.Text = DirectCast(rItems.FindControl("lComments"), Literal).Text
    '    'lDVVersion.Text = DirectCast(rItems.FindControl("lVersion"), Literal).Text

    '    'DocSession.soldDocFileName = DocSession.sDocID & "_" & lDVVersion.Text & "_" & lDVFileName.Text

    '    'DisplayDoc(DocSession.sDocID, lDVFileName.Text, lDVVersion.Text)
    '    'pDocView.Update()


    'End Sub

    'Private Sub DownloadFile(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim psPath As String = ""
    '    Dim tFDload As New System.IO.FileInfo(psPath)

    '    If System.IO.File.Exists(psPath) Then
    '        ' clear the current output content from the buffer
    '        Response.Clear()
    '        Response.ClearContent()
    '        Response.ClearHeaders()

    '        Response.AddHeader("Content-Disposition", "attachment; filename=" + _
    '        tFDload.Name)

    '        Response.AddHeader("Content-Length", tFDload.Length.ToString())

    '        Response.ContentType = "application/octet-stream"

    '        Response.WriteFile(tFDload.FullName)

    '        Response.End()
    '    Else
    '        'MyBase.DisplayMessage("File does not exist.")
    '    End If

    'End Sub
    Public Sub DeleteAttachment(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Dim oTag As New DocAttachment
        Dim ImgBtnSelected As ImageButton
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim liCtr As Integer
        Dim lsTags As String = ""
        Dim lsTag As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)


            'objCommand.pCommandText = "xMSP_DOCTAGSDELETE"
            For Each ri In rptAttachment.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)

                    'lsTag = DirectCast(ri.FindControl("lF"), LinkButton).Text
                    lsTag = DirectCast(ri.FindControl("hlNewTab"), HyperLink).Text
                    If ImgBtnSelected.Visible Then
                        If lsTags = "" Then
                            lsTags = lsTag
                            lsdesc = "attachment"
                        Else
                            lsTags = lsTags & ", " & lsTag
                            lsdesc = "attachments"
                        End If
                        oTag.pAttachId = DirectCast(ri.FindControl("lAI"), Literal).Text
                        oTag.DeleteAttachment(objCommand)
                        liCtr += 1
                    End If


                End If

            Next


            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            If liCtr >= 1 Then
                Dim Ohist As New DocHistory
                Ohist.pTask = "Attachment"
                Ohist.pAction = "Deleted " & lsdesc & " (" & lsTags & ")"
                Ohist.pUserId = DocSession.sUserId
                Ohist.pIpAddress = Request.UserHostAddress
                Ohist.pDocId = DocSession.sDocID
                Ohist.AddHistory(objCommand)

                ltr.pTran.Commit()
                RetrieveAttachment()
            Else
                ltr.pTran.Rollback()
                ErrorMsg("Please select a file before clicking delete button.")
            End If

        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            ErrorMsg("Error occurred while deleting attachment (" & ex.Message & "). Please try again.")
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
    Public Sub RetrieveAttachment()
        Dim ldata As DataTable
        Try
            Dim oDocs As New DocAttachment
            oDocs.pDocId = DocSession.sDocID
            ldata = oDocs.RetrieveAttachment

            rptAttachment.DataSource = ldata
            rptAttachment.DataBind()

            If ldata.Rows.Count > 0 Then
                hfCount.Value = ldata.Rows.Count.ToString
            Else
                hfCount.Value = ""
            End If
            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub
    'Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
    '    Dim DocID As String = DocSession.sDocID
    '    Dim FileName As String = Server.HtmlDecode(DirectCast(rptitem.FindControl("lF"), LinkButton).Text)
    '    '--new folder
    '    Dim sYear As String = Year(CDate(pCreatedDate))
    '    Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
    '    DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocID & "_" & FileName
    '    If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then

    '        If Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
    '            DocSession.sCurrentFile = "attachment\" & DocID & "_" & FileName
    '        End If

    '    End If
    '    Dim aCanDownload As Boolean = DirectCast(rptitem.FindControl("iD"), ImageButton).Visible
    '    DocSession.sFileName = FileName
    '    DisplayDoc(aCanDownload)

    'End Sub

    'Private Sub DisplayDoc(ByVal aCanDl As Boolean)

    '    Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
    '    Dim lsFilePath As String = lsLoc
    '    ucViewer.Visible = False
    '    ucPDFViewer.Visible = False
    '    ucDocViewer.Visible = False
    '    If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
    '        Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc & DocSession.sCurrentFile).ToLower

    '        If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then

    '            ucViewer.ViewImg()
    '            ucViewer.Visible = True


    '        ElseIf lsext = ".pdf" Then

    '            'docvw.Attributes("src") = "119_1_blank5.pdf"
    '            ucPDFViewer.ViewPDF()
    '            ucPDFViewer.Visible = True

    '        ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then
    '            If aCanDl Then
    '                Message = "This file type cannot be viewed directly in the system. Please download to view this file."
    '            Else
    '                If DocSession.sCanView = "True" Then
    '                    Message = "This file type cannot be viewed directly in the system."
    '                Else
    '                    Message = "You don't have enough access to view this file."
    '                End If

    '            End If
    '            'ucDocViewer.ViewDoc()
    '            'ucDocViewer.Visible = True
    '            RaiseEvent e_ShowMessage()
    '            'ucDocViewer.ViewDoc()
    '            'ucDocViewer.Visible = True
    '        End If
    '    ElseIf System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
    '        Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc2 & DocSession.sCurrentFile).ToLower

    '        If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then

    '            ucViewer.ViewImg()
    '            ucViewer.Visible = True


    '        ElseIf lsext = ".pdf" Then

    '            'docvw.Attributes("src") = "119_1_blank5.pdf"
    '            ucPDFViewer.ViewPDF()
    '            ucPDFViewer.Visible = True

    '        ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then
    '            If aCanDl Then
    '                Message = "This file type cannot be viewed directly in the system. Please download to view this file."
    '            Else
    '                If DocSession.sCanView = "True" Then
    '                    Message = "This file type cannot be viewed directly in the system."
    '                Else
    '                    Message = "You don't have enough access to view this file."
    '                End If

    '            End If
    '            'ucDocViewer.ViewDoc()
    '            'ucDocViewer.Visible = True
    '            RaiseEvent e_ShowMessage()
    '            'ucDocViewer.ViewDoc()
    '            'ucDocViewer.Visible = True
    '        End If
    '    Else
    '        lsLoc = System.Configuration.ConfigurationManager.AppSettings("altattachloc")
    '        lsFilePath = lsLoc & "\"
    '        If System.IO.File.Exists(lsFilePath & DocSession.sCurrentFile) Then
    '            Dim lsext As String = System.IO.Path.GetExtension(lsFilePath & DocSession.sCurrentFile).ToLower

    '            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then

    '                ucViewer.ViewImg()
    '                ucViewer.Visible = True


    '            ElseIf lsext = ".pdf" Then

    '                'docvw.Attributes("src") = "119_1_blank5.pdf"
    '                ucPDFViewer.ViewPDF()
    '                ucPDFViewer.Visible = True

    '            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then
    '                If aCanDl Then
    '                    Message = "This file type cannot be viewed directly in the system. Please download to view this file."
    '                Else
    '                    If DocSession.sCanView = "True" Then
    '                        Message = "This file type cannot be viewed directly in the system."
    '                    Else
    '                        Message = "You don't have enough access to view this file."
    '                    End If

    '                End If
    '                'ucDocViewer.ViewDoc()
    '                'ucDocViewer.Visible = True
    '                RaiseEvent e_ShowMessage()
    '            End If
    '        Else
    '            Message = "File associated with this document does not exists on the server. Please contact administrator."
    '            RaiseEvent e_ShowMessage()

    '        End If
    '    End If
    'End Sub

    Public Sub fDownload(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lsFN As String = DirectCast(lImg.Parent.FindControl("hlNewTab"), HyperLink).Text
        '--new folder
        Dim sYear As String = Year(CDate(pCreatedDate))
        Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
        Dim lsFile As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & lsFN
        'Dim lsFile As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFN
        If Not System.IO.File.Exists(lsFile) Then
            lsFile = DocSession.FileLoc2 & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment\" & DocSession.sDocID & "_" & lsFN

        End If
        Dim linfo As New System.IO.FileInfo(lsFile)
        If System.IO.File.Exists(lsFile) Then
            LogHistory(lsFN)
            If linfo.Extension.ToLower = ".pdf" Then

                DownLoadPDFFile(lsFN, lsFile)
            Else
                DownloadFile(lsFile)
            End If


            'Dim ohist As New DocHistory
            'ohist.pDocId = DocSession.sDocID
            'ohist.pIpAddress = Request.UserHostAddress
            'ohist.pTask = "Download"
            'ohist.pUserId = DocSession.sUserId
            'ohist.pAction = "Downloaded file '" & lsFN & "'"
            'ohist.AddHistory()
        Else
            'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("altattachloc") & "\" & lsFN
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("altattachloc") & "\attachment\" & DocSession.sDocID & "_" & lsFN
                If System.IO.File.Exists(lsLoc) Then
                    LogHistory(lsFN)
                    If linfo.Extension.ToLower = ".pdf" Then

                        DownLoadPDFFile(lsFN, lsLoc)
                    Else
                        DownloadFile(lsLoc)
                    End If
                Else
                    ErrorMsg("File does not exist in the server.")
                End If


            End If
    End Sub
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
    Private Sub DownLoadPDFFile(ByVal psFN As String, ByVal psFile As String)
        'Dim document As iTextSharp.text.Document = Nothing
        'Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        'Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim overContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0
        Dim lsMessage As String = ""
        'watermark variables

        Dim lsPath As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        Dim Encoding As New System.Text.UTF8Encoding()
        'lsPath = context.Request.QueryString("location") & lsFileName
        'lsPath = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & psFN
        lsPath = psFile
        Try
            'Dim lbpermitprint As Boolean
            'If context.Session("s_CanPrint") = "True" Then
            'lbpermitPrint = True 
            'lbpermitprint = True ' always false so printing can be handle on the page only
            'Else
            'lbpermitprint = False
            'End If


            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & psFN & ","
            Dim lss2 As String = "printed from the Document Management System " & _
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

            'End If


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()

                reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                'reader.unethicalreading = True

                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                pageCount = reader.NumberOfPages()

                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
                Dim backgroundColor As BaseColor = New BaseColor(0, 0, 0)
                Dim fontColor As BaseColor = New BaseColor(0, 0, 0)

                'stamper.SetEncryption(Nothing, Encoding.GetBytes("docuvu"), PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
                'stamper.SetEncryption(Nothing, Nothing, PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH40BITS)
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" & psFN.Replace(",", ""))
                ' Add a HTTP header to the output stream that contains the 
                ' content length(File Size). This lets the browser know how much data is being transfered
                Response.AddHeader("Content-Length", cntnt.Length.ToString())
                'Set the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then

                'Master.ShowMessage("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                'Master.ShowMessage(ex.Message)
                'Context.Response.Write("File is missing. Please contact the administrator.")
            End If
        Finally

            If Not reader Is Nothing Then
                reader.Close()
                reader = Nothing
            End If
            If Not stamper Is Nothing Then
                stamper.Close()
                stamper.Dispose()
                stamper = Nothing
            End If

        End Try
    End Sub
    Private Sub LogHistory(ByVal asFile As String)
        Dim ohist As New DocHistory

        ohist.pAction = "Downloaded attachment " & asFile
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Download"
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()
        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)
        'RetrieveAttachment()


    End Sub
    Private Sub DownloadFile(ByVal psPath As String)

        Dim tFDload As New System.IO.FileInfo(psPath)


        ' clear the current output content from the buffer
        'LogHistory(tFDload.Name)

        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()

        Response.AddHeader("Content-Disposition", "attachment; filename=" + _
        tFDload.Name)

        Response.AddHeader("Content-Length", tFDload.Length.ToString())

        Response.ContentType = "application/octet-stream"

        Response.WriteFile(tFDload.FullName)

        Response.End()




    End Sub
    Public Sub CountAttach()
        Dim oNotes As DocAttachment
        Dim lcnt As Integer
        Try

            If DocSession.sDocID <> "" Then
                oNotes = New DocAttachment
                lcnt = oNotes.CountAttachment(DocSession.sDocID)
                If lcnt > 0 Then
                    hfCount.Value = lcnt.ToString
                Else
                    hfCount.Value = ""
                End If

                RaiseEvent e_Count()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Public Sub ShowIndex(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim ri As RepeaterItem

        If DirectCast(sender, ImageButton).ImageUrl = "images/minus.jpg" Then
            DirectCast(sender, ImageButton).ImageUrl = "images/plus.jpg"
        Else
            DirectCast(sender, ImageButton).ImageUrl = "images/minus.jpg"
        End If
        ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)


        Dim ucIndex As UserControlDocumentAttachIndex = DirectCast(ri.FindControl("ucIndex"), UserControlDocumentAttachIndex)

        ucIndex.Visible = Not ucIndex.Visible

        If ucIndex.Visible Then
            If ucIndex.pAttachId = "" Then
                ucIndex.RetrieveDocIndex(DocSession.sDocID, DirectCast(ri.FindControl("lDT"), Literal).Text, DirectCast(ri.FindControl("lAI"), Literal).Text)
            End If
        End If



    End Sub
End Class
