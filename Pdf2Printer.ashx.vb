Imports System.Web
Imports System.Web.Services
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api

Public Class Pdf2Printer
    Implements System.Web.IHttpHandler
    Implements System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim document As iTextSharp.text.Document = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0
        Dim lsMessage As String = ""
        'watermark variables

        Dim lsPath, lsFileName As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        lsFileName = DocSession.sCurrentFile

        'lsPath = context.Request.QueryString("location") & lsFileName
        lsPath = DocSession.FileLoc & lsFileName
        Try
            If Not System.IO.File.Exists(lsPath) Then

                'lsPath = System.Configuration.ConfigurationManager.AppSettings("altattachloc") & "\" & lsFileName
                If Not System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
                    lsPath = System.Configuration.ConfigurationManager.AppSettings("altattachloc") & "\" & lsFileName
                Else
                    lsPath = DocSession.FileLoc2 & lsFileName
                End If
            End If
            'Dim lbpermitprint As Boolean
            'If context.Session("s_CanPrint") = "True" Then
            'lbpermitPrint = True 
            'lbpermitprint = True ' always false so printing can be handle on the page only
            'Else
            'lbpermitprint = False
            'End If
            Dim imsg As New iTextSharp.text.Phrase

            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim ochunk As New iTextSharp.text.Chunk("This is a hard copy of the " & DocSession.sReferenceNo & " - " & DocSession.sFileName & " printed from the Document Management System ")
            Dim ochunk1 As New iTextSharp.text.Chunk("on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId & ".")

            ochunk.SetBackground(iTextSharp.text.BaseColor.WHITE)
            ochunk.Font.Size = 7
            ochunk.Font.Color = iTextSharp.text.BaseColor.BLACK
            ochunk1.SetBackground(iTextSharp.text.BaseColor.WHITE)
            ochunk1.Font.Size = 7
            ochunk1.Font.Color = iTextSharp.text.BaseColor.BLACK
            Dim opa As New iTextSharp.text.Paragraph(ochunk)
            opa.Add(ochunk1)

            'imsg.Add(ochunk)

            'End If
            'Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & DocSession.sFileName & ","
            Dim lss2 As String = "printed from the Document Management System " & _
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                reader.unethicalreading = True
                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                rect = reader.GetPageSizeWithRotation(1)

                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                'If img.Width > rect.Width OrElse img.Height > rect.Height Then
                img.ScaleToFit(rect.Width, rect.Height)
                X = (rect.Width - img.ScaledWidth) / 2
                Y = (rect.Height - img.ScaledHeight) / 2
                'Else
                'X = (rect.Width - img.Width) / 2
                'Y = (rect.Height - img.Height) / 2
                'End If
                img.SetAbsolutePosition(X, Y)


                'stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                'pageCount = reader.NumberOfPages()

                'For ii As Integer = 1 To pageCount
                '    underContent = stamper.GetOverContent(ii) '.GetOverContent(ii)
                '    underContent.AddImage(img)
                'Next
                'stamper.Close()
                'end watermark

                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
                ' we retrieve the total number of pages
                Dim n As Integer = reader.NumberOfPages
                ' step 1: creation of a document-object
                document = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1))
                ' step 2: we create a writer that listens to the document
                writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, outputStream)

                'write pdf that pdfsharp can understand
                'writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_4)
                ' step 3: we open the document
                document.Open()
                Dim jAction As iTextSharp.text.pdf.PdfAction = iTextSharp.text.pdf.PdfAction.JavaScript("this.print(true);", writer)
                writer.AddJavaScript(jAction)
                Dim cb As iTextSharp.text.pdf.PdfContentByte = writer.DirectContent

                Dim page As iTextSharp.text.pdf.PdfImportedPage
                document.AddDocListener(writer)
                Dim rotation As Integer
                Dim i As Integer = 0
                'Dim topM As Single
                While i < n
                    i += 1


                    document.SetPageSize(reader.GetPageSizeWithRotation(i))
                    document.NewPage()

                    page = writer.GetImportedPage(reader, i)

                    'document.Add(img)
                    document.Add(opa)
                    document.Add(imsg)

                    'topM = document.TopMargin
                    'document.SetMargins(document.LeftMargin, document.RightMargin, 0, document.BottomMargin)


                    rotation = reader.GetPageRotation(i)
                    If rotation = 90 OrElse rotation = 270 Then
                        cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, _
                        reader.GetPageSizeWithRotation(i).Height)
                    Else
                        cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                    End If
                    'Dim footerTmpl As iTextSharp.text.pdf.PdfTemplate = cb.CreateTemplate(rect.Width, 20)


                    'footerTmpl.SetFontAndSize(font, 6.0F)
                    'footerTmpl.SetTextMatrix(X, Y)
                    'footerTmpl.ShowText(lss)
                    'page.AddTemplate(footerTmpl, 0, 0)
                End While


                '---- Keep the stream open!
                writer.CloseStream = False


                ' step 5: we close the document
                document.Close()

                'outputStream.Close()
                Dim cntnt As Byte() = outputStream.ToArray
                context.Response.Clear()
                context.Response.ContentType = "application/pdf"
                context.Response.BinaryWrite(cntnt)
            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then
                context.Response.Write("Cannot open encrypted file. Please upload pdf file without password.")
            Else
                context.Response.Write(ex.Message)
                'context.Response.Write("File is missing. Please contact the administrator.")
            End If
        Finally
            If Not writer Is Nothing Then
                writer.Close()
                writer.Dispose()
                writer = Nothing
            End If
            If Not document Is Nothing Then
                document.Close()
                document = Nothing
            End If
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


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    


End Class