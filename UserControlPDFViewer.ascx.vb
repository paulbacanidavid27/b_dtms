Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api
Public Class UserControlPDFViewer
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ViewPDF()
        If Not IsPostBack Then
            If DocSession.sCanPrintReceipt = "True" Then
                'lbpermitPrint = True 
                imgPrint.Visible = False ' always false so printing can be handle on the page only
            Else
                imgPrint.Visible = False
            End If
            If DocSession.sCanDownloadDoc = "True" Then
                'lbpermitPrint = True 
                imgDownload.Visible = True ' always false so printing can be handle on the page only
            Else
                imgDownload.Visible = False
            End If
        End If
    End Sub
    Public Sub ViewPDF()
        docprint.Attributes("src") = ""
        docvw.Attributes("src") = "Pdf2Viewer.ashx?dt=" & Date.Now()
        docvw.Visible = True
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Me.Visible = False
    End Sub

    Private Sub imgPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPrint.Click
        docprint.Attributes("src") = "Pdf2Printer.ashx?dt=" & Date.Now()
        Dim ohist As New DocHistory
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Printed"
        ohist.pUserId = DocSession.sUserId
        ohist.pAction = "Printed file '" & DocSession.sCurrentFile & "'"
        ohist.AddHistory()
        'docvw.Attributes("src") = "Pdf2Viewer.ashx?dt=" & Date.Now()
        'docvw.Visible = True
    End Sub

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
        lsPath = psPath 'DocSession.FileLoc & lsFileName
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
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & DocSession.sFileName & ","
            Dim lss2 As String = "printed from the Document Management System " & _
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" & lsFileName)
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
    Private Sub Dfile()
        Dim lsFile As String = getCurrentFile()
        Dim linfo As New System.IO.FileInfo(lsFile)
        If linfo.Extension.ToLower = ".pdf" Then
            DownLoadPDFFile(lsFile)
            Dim ohist As New DocHistory
            ohist.pDocId = DocSession.sDocID
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Download"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Downloaded file '" & DocSession.sFileName & "'"
            ohist.AddHistory()
        Else
            DownloadFile(lsFile)
        End If

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

        ohist.pAction = "Downloaded file " & asFile '& " from document '" & DocSession.sDocTitle & "'"
        ohist.pDocId = DocSession.sDocID
        ohist.pIpAddress = Request.UserHostAddress
        ohist.pTask = "Download"
        ohist.pUserId = DocSession.sUserId
        ohist.AddHistory()
        'ucDocHistory.RetrieveDocAction(DocSession.sDocID)



    End Sub
End Class