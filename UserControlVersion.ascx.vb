Imports System
Imports System.Web
Imports System.IO

Public Class UserControlVersion
    Inherits System.Web.UI.UserControl

    Dim smsg As String
    Public Event e_click()
    Public FileName As String
    Public Version As String
    Public OfficeCode As String
    Dim litotal As Long = 0
    Public Event e_ShowMessage()
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property

    Public Property pFileName As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
        End Set
    End Property
    Public Property pVersion As String
        Get
            Return Version
        End Get
        Set(ByVal value As String)
            Version = value
        End Set
    End Property
    Public Property pOfficeCode As String
        Get
            Return OfficeCode
        End Get
        Set(ByVal value As String)
            OfficeCode = value
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
    
    Private Function IsApprover() As Boolean
        Dim oRoute As New DocRouting
        'If DocSession.sDocID IsNot Nothing Then
        Try
            oRoute.pDocId = DocSession.sDocID
            oRoute.pApproverId = DocSession.sUserId

            If oRoute.RetrieveRouting().Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try




    End Function
    Private Sub rptFileVersion_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFileVersion.ItemCreated
        'If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        'AddHandler DirectCast(e.Item.FindControl("imgShow"), ImageButton).Click, AddressOf ShowDoc
        'AddHandler DirectCast(e.Item.FindControl("imgDownload"), ImageButton).Click, AddressOf DownloadFile
        'End If
    End Sub
    Private Sub rptFileVersion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFileVersion.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'If lDVVersion.Text.Trim = "" Then

            '    lDVFileName.Text = DirectCast(e.Item.FindControl("lFileName"), Literal).Text
            '    lDVUploadedBy.Text = DirectCast(e.Item.FindControl("lUploadedByName"), Literal).Text
            '    lDVUploadedDate.Text = DirectCast(e.Item.FindControl("lUploadedDate"), Literal).Text
            '    lDVComments.Text = DirectCast(e.Item.FindControl("lComments"), Literal).Text
            '    lDVVersion.Text = DirectCast(e.Item.FindControl("lVersion"), Literal).Text.Trim


            'End If
            If DocSession.sCanView = "True" Then
                'DirectCast(e.Item.FindControl("hlNewWindow"), HyperLink).NavigateUrl = "viewfile.aspx?d_id=" & DirectCast(e.Item.FindControl("lvDocId"), Literal).Text & "&v_no=" & DirectCast(e.Item.FindControl("lVersion"), Literal).Text
                DirectCast(e.Item.FindControl("lTextVersion"), Literal).Visible = False
                DirectCast(e.Item.FindControl("lbDocVersion"), LinkButton).Visible = True
            Else
                DirectCast(e.Item.FindControl("lTextVersion"), Literal).Visible = True
                DirectCast(e.Item.FindControl("lbDocVersion"), LinkButton).Visible = False
            End If
            'DirectCast(e.Item.FindControl("lFileSize"), Literal).Text = FormatBytes(CULng(DirectCast(e.Item.FindControl("lFileSize"), Literal).Text))
            litotal = litotal + CLng(DirectCast(e.Item.FindControl("lFileSize"), Literal).Text)
            DirectCast(e.Item.FindControl("lFileSize"), Literal).Text = FormatBytes(CLng(DirectCast(e.Item.FindControl("lFileSize"), Literal).Text))

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            DirectCast(e.Item.FindControl("ltotalfilesize"), Literal).Text = "Total File Size: " & FormatBytes(litotal)
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
    Public Sub showDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rItems As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        pFileName = Server.HtmlDecode(DirectCast(rItems.FindControl("lFileName"), Label).Text)
        pVersion = DirectCast(rItems.FindControl("lVersion"), Literal).Text
        RaiseEvent e_click()

        'DocSession.soldDocFileName = lDVFileName.Text
        'lDVUploadedBy.Text = DirectCast(rItems.FindControl("lUploadedByName"), Literal).Text
        'lDVUploadedDate.Text = DirectCast(rItems.FindControl("lUploadedDate"), Literal).Text
        'lDVComments.Text = DirectCast(rItems.FindControl("lComments"), Literal).Text
        'lDVVersion.Text = DirectCast(rItems.FindControl("lVersion"), Literal).Text

        'DocSession.soldDocFileName = DocSession.sDocID & "_" & lDVVersion.Text & "_" & lDVFileName.Text

        'DisplayDoc(DocSession.sDocID, lDVFileName.Text, lDVVersion.Text)
        'pDocView.Update()


    End Sub

    Public Sub showVersionInfo(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim rItems As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        'lDVFileName.Text = Server.HtmlDecode(DirectCast(rItems.FindControl("lFileName"), Literal).Text)

        'DocSession.soldDocFileName = lDVFileName.Text
        'lDVUploadedBy.Text = DirectCast(rItems.FindControl("lUploadedByName"), Literal).Text
        'lDVUploadedDate.Text = DirectCast(rItems.FindControl("lUploadedDate"), Literal).Text
        'lDVComments.Text = DirectCast(rItems.FindControl("lComments"), Literal).Text
        'lDVVersion.Text = DirectCast(rItems.FindControl("lVersion"), Literal).Text

        'DocSession.soldDocFileName = DocSession.sDocID & "_" & lDVVersion.Text & "_" & lDVFileName.Text

        'DisplayDoc(DocSession.sDocID, lDVFileName.Text, lDVVersion.Text)
        'pDocView.Update()


    End Sub

    Private Sub DownloadFile(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim psPath As String = ""
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

    Public Sub showVersionInfo2(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim rItems As RepeaterItem = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
        'lDVFileName.Text = Server.HtmlDecode(DirectCast(rItems.FindControl("lFileName"), Literal).Text)

        'DocSession.soldDocFileName = lDVFileName.Text
        'lDVUploadedBy.Text = DirectCast(rItems.FindControl("lUploadedByName"), Literal).Text
        'lDVUploadedDate.Text = DirectCast(rItems.FindControl("lUploadedDate"), Literal).Text
        'lDVComments.Text = DirectCast(rItems.FindControl("lComments"), Literal).Text
        'lDVVersion.Text = DirectCast(rItems.FindControl("lVersion"), Literal).Text

        'DocSession.soldDocFileName = DocSession.sDocID & "_" & lDVVersion.Text & "_" & lDVFileName.Text

        'Dim lsScript As String = "<script type='text/javascript'>window.open('xiewfile.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", lsScript)


    End Sub

    Public Sub RetrieveVersion()
        Dim oDocs As New DocList
        oDocs.pDocId = DocSession.sDocID
        rptFileVersion.DataSource = oDocs.RetrieveDocVersion()
        rptFileVersion.DataBind()
        'DocSession.soldDocFileName = DocSession.sDocID & "_" & lVersion.Text & "_" & lFileName.Text
        'DisplayDoc(DocSession.sDocID, lFileName.Text, lVersion.Text)
    End Sub
    Public Sub openDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rptitem As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        Dim DocID As String = DirectCast(rptitem.FindControl("lvDocId"), Literal).Text
        Dim FVersion As String = DirectCast(rptitem.FindControl("lVersion"), Literal).Text
        Dim FileName As String = Server.HtmlDecode(DirectCast(rptitem.FindControl("lFileName"), Label).Text)
        '--new folder
        Dim sYear, sMonth As String
        sYear = Year(CDate(pCreatedDate)).ToString
        sMonth = MonthName(Month(CDate(pCreatedDate))).ToString
        DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\" & DocID & "_" & FVersion & "_" & FileName
        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) AndAlso Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            DocSession.sCurrentFile = DocID & "_" & FVersion & "_" & FileName
        End If

        DocSession.sFileName = FileName
        DisplayDoc()
    End Sub

    Private Sub DisplayDoc()

        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        'Dim lsFilePath As String = lsLoc
        ucViewer.Visible = False
        ucPDFViewer.Visible = False
        ucDocViewer.Visible = False
        If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc & DocSession.sCurrentFile).ToLower

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

            ElseIf lsext = ".xls" OrElse lsext = ".doc" OrElse lsext = ".docx" OrElse lsext = ".xlsx" OrElse lsext = ".ppt" OrElse lsext = ".pptx" Then

                'ucDocViewer.ViewDoc()
                'ucDocViewer.Visible = True
                If DocSession.sCanDownload = "True" Then
                    Message = "This file type cannot be viewed directly in the system. Please download to view this file."
                Else
                    If DocSession.sCanView = "True" Then
                        Message = "This file type cannot be viewed directly in the system."
                    Else
                        Message = "You don't have enough access to view this file."
                    End If

                End If

                RaiseEvent e_ShowMessage()

            End If
        ElseIf System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc2 & DocSession.sCurrentFile).ToLower

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

            ElseIf lsext = ".xls" OrElse lsext = ".doc" OrElse lsext = ".docx" OrElse lsext = ".xlsx" OrElse lsext = ".ppt" OrElse lsext = ".pptx" Then

                'ucDocViewer.ViewDoc()
                'ucDocViewer.Visible = True
                If DocSession.sCanDownload = "True" Then
                    Message = "This file type cannot be viewed directly in the system. Please download to view this file."
                Else
                    If DocSession.sCanView = "True" Then
                        Message = "This file type cannot be viewed directly in the system."
                    Else
                        Message = "You don't have enough access to view this file."
                    End If

                End If

                RaiseEvent e_ShowMessage()

            End If
        Else
            Message = "File associated with this document does not exists on the server. Please contact administrator."
            RaiseEvent e_ShowMessage()

        End If
    End Sub
End Class
