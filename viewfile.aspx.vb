﻿Imports System
Imports System.Web
Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api


Public Class viewfile
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
            Response.Redirect("login.aspx")
        End If
        'AddHandler ucCheckOut.e_check, AddressOf ShowCheckOutInstruction
        'AddHandler ucCheckIn.e_check, AddressOf ShowCheckInInstruction
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim lsId As String = Request.QueryString("d_id")
            Dim lsRefNo As String = Request.QueryString("r_id")
            Dim lsVno As String = Request.QueryString("v_no")
            Dim lsAtt As String = IIf(IsNothing(Request.QueryString("att")), "", Request.QueryString("att"))
            If lsRefNo Is Nothing Then
                lsRefNo = ""
            End If
            If lsId Is Nothing Then
                lsId = ""
            End If

            If lsVno Is Nothing Then
                lsVno = ""
            End If
            If lsId.Trim() = "" Then
                If lsRefNo <> "" Then
                    Dim lec As New crypt
                    'lsId = lec.Decrypt(Server.UrlDecode(lsId))
                    lsRefNo = lec.Decrypt(lsRefNo)
                    lsRefNo = Left(lsRefNo, Len(lsRefNo) - 3)
                Else
                    lsId = DocSession.sDocID
                    lsVno = "1"
                End If
                
            Else
                Dim lec As New crypt
                'lsId = lec.Decrypt(Server.UrlDecode(lsId))
                lsId = lec.Decrypt(lsId)
                lsId = Left(lsId, Len(lsId) - 3)
            End If
            'If DocSession.sCanPrintReceipt = "True" Then
            '    'lbpermitPrint = True 
            '    imgPrint.Visible = True ' always false so printing can be handle on the page only
            'Else
            '    imgPrint.Visible = False
            'End If
            If DocSession.sOwner OrElse DocSession.sCanDownloadDoc = "True" OrElse DocSession.sUserRole = "A" Then
                imgDownload.Visible = True
            Else
                imgDownload.Visible = False
            End If
            If lsAtt.Trim = "Y" Then
                'DisplayAttachment(lsId)
                If lsId <> "" Then
                    RetrieveAttach(lsId, lsVno)
                Else
                    RetrieveAttachByRefNo(lsRefNo)
                End If

            Else

                If lsId <> "" Then
                    RetrieveDoc(lsId, lsVno)
                Else
                    RetrieveDocByRefNo(lsRefNo)
                End If
            End If

            'DisplayDoc()
        End If
    End Sub
    Private Sub RetrieveDocByRefNo(ByVal asref As String)
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim oDocs As New DocList

        Dim lodata As DataTable
        Try
            oDocs.pRefNo = asref
            'oDocs.pDocVersion = asVno
            'oDocs.pGroupId = DocSession.sUserGroup
            'oDocs.pSortOrder = "Asc"
            'oDocs.pUserId = DocSession.sUserId
            lodata = oDocs.RetrieveFileVersionByRefNo

            If lodata.Rows.Count > 0 Then
                '--new folder
                Dim sYear As String = Year(CDate(lodata(0).Item("CreatedDate")))
                Dim sMonth As String = MonthName(Month(CDate(lodata(0).Item("CreatedDate"))))
                DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lodata(0).Item("refno") & "\" & lodata(0)("docid") & "_" & lodata(0)("FileVersion") & "_" & lodata(0).Item("FileName")

                DocSession.sFileName = lodata(0).Item("FileName")
                If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                    If Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
                        DocSession.sCurrentFile = lodata(0)("docid") & "_" & lodata(0)("FileVersion") & "_" & lodata(0).Item("FileName")
                        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                            lMsg.Text = "File associated with this document does not exists on the server. Please contact administrator."
                            imgDownload.Visible = False
                            'imgPrint.Visible = False
                            lMsg.Visible = True
                        Else
                            lMsg.Visible = False
                            DisplayDoc()
                        End If
                    Else
                        lMsg.Visible = False
                        DisplayDoc()
                    End If
                Else
                        lMsg.Visible = False
                    DisplayDoc()
                End If
            Else
                lMsg.Text = "File associated with this document does not exists on the server. Please contact administrator."
                imgDownload.Visible = False
                'imgPrint.Visible = False
                lMsg.Visible = True
            End If

        Catch ex As Exception
        Finally
            If Not oDocs Is Nothing Then
                oDocs = Nothing
            End If

            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing

            End If
        End Try

    End Sub

    Private Sub RetrieveAttachByRefNo(ByVal asRef As String)
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim oDocs As New DocList

        Dim lodata As DataTable
        Try
            oDocs.pRefNo = asRef

            lodata = oDocs.RetrieveFileVersionByRefNo

            If lodata.Rows.Count > 0 Then
                '--new folder
                Dim sYear As String = Year(CDate(lodata(0).Item("CreatedDate")))
                Dim sMonth As String = MonthName(Month(CDate(lodata(0).Item("CreatedDate"))))
                DocSession.sFileName = lodata(0).Item("FileName")
                DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lodata(0).Item("refno") & "\attachment\" & lodata(0).Item("docId") & "_" & DocSession.sFileName

                If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                    If Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
                        DocSession.sCurrentFile = "attachment\" & lodata(0).Item("docId") & "_" & DocSession.sFileName
                        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                            lMsg.Text = "File associated with this document does not exists on the server. Please contact administrator."
                            imgDownload.Visible = False
                            'imgPrint.Visible = False
                            lMsg.Visible = True
                        Else
                            lMsg.Visible = False
                            DisplayDoc()
                        End If
                    Else
                        lMsg.Visible = False
                        DisplayDoc()
                    End If
                Else
                        lMsg.Visible = False
                    DisplayDoc()
                End If


            End If

        Catch ex As Exception
        Finally
            If Not oDocs Is Nothing Then
                oDocs = Nothing
            End If

            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing

            End If
        End Try

    End Sub

    Private Sub RetrieveDoc(ByVal asId As String, ByVal asVno As String)
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim oDocs As New DocList

        Dim lodata As DataTable
        Try
            oDocs.pDocId = asId
            oDocs.pDocVersion = asVno
            oDocs.pGroupId = DocSession.sUserGroup
            oDocs.pSortOrder = "Asc"
            oDocs.pUserId = DocSession.sUserId
            lodata = oDocs.RetrieveFileVersion

            If lodata.Rows.Count > 0 Then
                '--new folder
                Dim sYear As String = Year(CDate(lodata(0).Item("CreatedDate")))
                Dim sMonth As String = MonthName(Month(CDate(lodata(0).Item("CreatedDate"))))
                'DocSession.sCanPrint = lodata(0).Item("CanPrint")
                'DocSession.sDocFileName = asId & "_" & asVno & "_" & lodata(0).Item("FileName")
                DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lodata(0).Item("refno") & "\" & asId & "_" & asVno & "_" & lodata(0).Item("FileName")

                DocSession.sFileName = lodata(0).Item("FileName")
                If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                    If Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
                        DocSession.sCurrentFile = asId & "_" & asVno & "_" & lodata(0).Item("FileName")
                        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                            lMsg.Text = "File associated with this document does not exists on the server. Please contact administrator."
                            imgDownload.Visible = False
                            'imgPrint.Visible = False
                            lMsg.Visible = True
                        Else
                            lMsg.Visible = False
                            DisplayDoc()
                        End If
                    Else
                        lMsg.Visible = False
                        DisplayDoc()
                    End If
                Else
                        lMsg.Visible = False
                    DisplayDoc()
                End If


            End If

        Catch ex As Exception
        Finally
            If Not oDocs Is Nothing Then
                oDocs = Nothing
            End If

            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing

            End If
        End Try

    End Sub
    Private Sub RetrieveAttach(ByVal asId As String, ByVal asVno As String)
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        Dim oDocs As New DocList

        Dim lodata As DataTable
        Try
            oDocs.pDocId = asId
            oDocs.pDocVersion = asVno
            lodata = oDocs.RetrieveRefNoCreatedDate

            If lodata.Rows.Count > 0 Then
                '--new folder
                Dim sYear As String = Year(CDate(lodata(0).Item("CreatedDate")))
                Dim sMonth As String = MonthName(Month(CDate(lodata(0).Item("CreatedDate"))))
                DocSession.sFileName = lodata(0).Item("DocFileName")
                DocSession.sCurrentFile = sYear & "\" & sMonth & "\" & lodata(0).Item("refno") & "\attachment\" & asId & "_" & DocSession.sFileName

                If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
                    If Not System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
                        DocSession.sCurrentFile = "attachment\" & asId & "_" & DocSession.sFileName
                        If Not System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then

                            lMsg.Text = "File associated with this document does not exists on the server. Please contact administrator."
                            imgDownload.Visible = False
                            'imgPrint.Visible = False
                            lMsg.Visible = True
                        Else
                            lMsg.Visible = False
                            DisplayDoc()
                        End If
                    Else
                        lMsg.Visible = False
                        DisplayDoc()
                    End If


                Else
                    lMsg.Visible = False
                    DisplayDoc()
                End If


            End If

        Catch ex As Exception
        Finally
            If Not oDocs Is Nothing Then
                oDocs = Nothing
            End If

            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing

            End If
        End Try

    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lLinkDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Private Sub DisplayDoc()

        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        'Dim lsFilePath As String = DocSession.FileLoc & asDocId & "_" & asDocVersion & "_"
        'If System.IO.File.Exists(Server.MapPath("") & "\" & lsFilePath & asFileName) Then
        ' DocSession.soldDocFileName = asDocId & "_" & asDocVersion & "_" & asFileName

        If System.IO.File.Exists(DocSession.FileLoc & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
                'docvw.Visible = False

                ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
                pnlImageDisp.Visible = True
                imgHandler2.ImageUrl = "viewDoc.aspx?dt=" & Date.Now()
                imgHandler2.Visible = True

                'docvw.Attributes("src") = "viewDoc.aspx"
                docvw2.Visible = False
                'imgPrint.Visible = False
                imgDownload.Visible = False
                'pnlDocView.Update()
                'pDocView.Update()

            ElseIf lsext = ".pdf" Then

                'docvw.Attributes("src") = "119_1_blank5.pdf"
                'docvw.Attributes("src") = "Pdf2Viewer.ashx?location=" & DocSession.FileLoc
                docvw2.Attributes("src") = "Pdf2Viewer.ashx"
                docvw2.Visible = True

                'pnlDocView.Update()

                'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                docvw2.Attributes("src") = "viewDoc.aspx?dt=" & Date.Now()
                'docvw2.Attributes("src") = "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc
                docvw2.Visible = True
                'imgPrint.Visible = False
                imgDownload.Visible = False
                'pnlDocView.Update()
            ElseIf lsext = ".zip" OrElse lsext = ".rar" Then
                If DocSession.sOwner OrElse DocSession.sCanDownloadDoc = "True" OrElse DocSession.sUserRole = "A" Then
                    lMsg.Text = "Zip file cannot be viewed. Please download the file."
                Else
                    lMsg.Text = "Zip file cannot be viewed in the system. "
                End If

                lMsg.Visible = True
                End If
            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & DocSession.sCurrentFile) Then
            Dim lsext As String = System.IO.Path.GetExtension(DocSession.FileLoc2 & DocSession.sCurrentFile).ToLower

            If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
                'docvw.Visible = False

                ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
                pnlImageDisp.Visible = True
                imgHandler2.ImageUrl = "viewDoc.aspx?dt=" & Date.Now()
                imgHandler2.Visible = True

                'docvw.Attributes("src") = "viewDoc.aspx"
                docvw2.Visible = False
                'imgPrint.Visible = False
                imgDownload.Visible = False
                'pnlDocView.Update()
                'pDocView.Update()

            ElseIf lsext = ".pdf" Then

                'docvw.Attributes("src") = "119_1_blank5.pdf"
                'docvw.Attributes("src") = "Pdf2Viewer.ashx?location=" & DocSession.FileLoc
                docvw2.Attributes("src") = "Pdf2Viewer.ashx"
                docvw2.Visible = True

                'pnlDocView.Update()

                'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

                docvw2.Attributes("src") = "viewDoc.aspx?dt=" & Date.Now()
                'docvw2.Attributes("src") = "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc
                docvw2.Visible = True
                'imgPrint.Visible = False
                imgDownload.Visible = False
                'pnlDocView.Update()

            End If

        End If
    End Sub
    'Private Sub DisplayAttachment(ByVal asAtt As String)

    '    'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
    '    'Dim lsFilePath As String = DocSession.FileLoc & asDocId & "_" & asDocVersion & "_"
    '    'If System.IO.File.Exists(Server.MapPath("") & "\" & lsFilePath & asFileName) Then
    '    ' DocSession.soldDocFileName = asDocId & "_" & asDocVersion & "_" & asFileName

    '    If System.IO.File.Exists(DocSession.DocLoc & "attachment\" & asAtt) Then
    '        Dim lsext As String = System.IO.Path.GetExtension(DocSession.DocLoc & "attachment\" & asAtt).ToLower

    '        If lsext = ".jpg" OrElse lsext = ".jpeg" OrElse lsext = ".png" OrElse lsext = ".gif" OrElse lsext = ".tiff" OrElse lsext = ".tif" Then
    '            'docvw.Visible = False

    '            ''Literal1.Text = "<img src='" & lsLoc & oDocs.pDocId & "__" & lodata(0).Item("FileName") & "' />"
    '            pnlImageDisp.Visible = True
    '            imgHandler2.ImageUrl = "viewDoc.aspx?dt=" & Date.Now()
    '            imgHandler2.Visible = True

    '            'docvw.Attributes("src") = "viewDoc.aspx"
    '            docvw2.Visible = False

    '            'pnlDocView.Update()
    '            'pDocView.Update()

    '        ElseIf lsext = ".pdf" Then

    '            'docvw.Attributes("src") = "119_1_blank5.pdf"
    '            'docvw.Attributes("src") = "Pdf2Viewer.ashx?location=" & DocSession.FileLoc
    '            docvw2.Attributes("src") = "Pdf2Viewer.ashx"
    '            docvw2.Visible = True

    '            'pnlDocView.Update()

    '            'window.open('apecs_batch_report_lookup.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')
    '            'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
    '        ElseIf lsext = ".xls" Or lsext = ".doc" Or lsext = ".docx" Or lsext = ".xlsx" Or lsext = ".ppt" Or lsext = ".pptx" Then

    '            docvw2.Attributes("src") = "viewDoc.aspx?dt=" & Date.Now()
    '            'docvw2.Attributes("src") = "DocViewer2.ashx?filename=" & DocSession.soldDocFileName & "&location=" & DocSession.FileLoc
    '            docvw2.Visible = True

    '            'pnlDocView.Update()

    '        End If

    '    End If
    'End Sub
    'Private Sub imgPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPrint.Click
    '    docprint.Attributes("src") = "Pdf2Printer.ashx?dt=" & Date.Now()
    '    Dim ohist As New DocHistory
    '    ohist.pDocId = DocSession.sDocID
    '    ohist.pIpAddress = Request.UserHostAddress
    '    ohist.pTask = "Printed"
    '    ohist.pUserId = DocSession.sUserId
    '    ohist.pAction = "Printed file '" & DocSession.sFileName & "'"
    '    ohist.AddHistory()
    '    'docvw.Attributes("src") = "Pdf2Viewer.ashx?dt=" & Date.Now()
    '    'docvw.Visible = True
    'End Sub
    Private Sub imgDownload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDownload.Click
        Dfile()
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
            'Dim lbpermitprint As Boolean
            'If context.Session("s_CanPrint") = "True" Then
            'lbpermitPrint = True 
            'lbpermitprint = True ' always false so printing can be handle on the page only
            'Else
            'lbpermitprint = False
            'End If
            If Not System.IO.File.Exists(lsPath) Then
                lsPath = DocSession.FileLoc2 & lsFileName
            End If

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
                Response.AddHeader("Content-Disposition", "attachment; filename=" & lsFileName.Replace(",", ""))
                ' Add a HTTP header to the output stream that contains the 
                ' content length(File Size). This lets the browser know how much data is being transfered
                Response.AddHeader("Content-Length", cntnt.Length.ToString())
                'Set the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf"
                Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then
                Throw New Exception("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                Throw New Exception(ex.Message)
                'Context.Response.Write("File is missing. Please contact the administrator.")
            End If

        End Try
    End Sub
    Private Function getCurrentFile() As String
        Dim lsFile As String = DocSession.FileLoc & DocSession.sCurrentFile
        If Not System.IO.File.Exists(lsFile) Then
            lsFile = DocSession.FileLoc2 & DocSession.sCurrentFile
        End If
        Return lsFile
    End Function
    'Private Function getCurrentFile() As String

    '    Return DocSession.FileLoc & DocSession.sCurrentFile
    'End Function
    Private Sub Dfile()
        Dim lsFile As String = getCurrentFile()
        Dim linfo As New System.IO.FileInfo(lsFile)
        'If linfo.Extension.ToLower = ".pdf" AndAlso DocSession.isEncrypted = "" Then
        '    DownLoadPDFFile(lsFile)
        '    Dim ohist As New DocHistory
        '    ohist.pDocId = DocSession.sDocID
        '    ohist.pIpAddress = Request.UserHostAddress
        '    ohist.pTask = "Download"
        '    ohist.pUserId = DocSession.sUserId
        '    ohist.pAction = "Downloaded file '" & DocSession.sFileName & "'"
        '    ohist.AddHistory()
        'Else
        DownloadFile(lsFile)
        'End If

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

    Private Sub LogHistory(ByVal asFile As String)
        Dim ohist As New DocHistory

        ohist.pAction = "Downloaded file " & DocSession.sFileName '& " from document '" & DocSession.sDocTitle & "'"
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Download"
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()
        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)



    End Sub

End Class
