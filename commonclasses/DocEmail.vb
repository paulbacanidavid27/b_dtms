Imports System
Imports System.Net
Imports System.Net.Mime
Imports System.IO

Imports System.Web
Imports System.Web.Services
'Imports System.IO
Imports System.Diagnostics
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text
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
Public Class DocEmail
    Implements IDisposable

    Dim EmailFrom As String
    Dim EmailTo As String
    Dim EmailCC As String
    Dim EmailSubject As String
    Dim EmailBody As String
    Dim EmailHost As String
    Dim EmailPort As String
    Dim EmailIsHTML As String
    Dim EmailPassword As String
    Dim EmailAttachment As String
    Dim AttachFileName As String
    Dim sPath As String
    Dim SEmailAttachment As MemoryStream
    Dim GoogleLink As String
    Dim MailContent As String
    Dim MailSubject As String

    Public Sub New()

    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
    Public Property pLink() As String
        Get
            Return GoogleLink
        End Get
        Set(ByVal value As String)
            GoogleLink = value
        End Set

    End Property
    Public Property pMailContent() As String
        Get
            Return MailContent
        End Get
        Set(ByVal value As String)
            MailContent = value
        End Set

    End Property
    Public Property pSubject() As String
        Get
            Return MailSubject
        End Get
        Set(ByVal value As String)
            MailSubject = value
        End Set

    End Property
    Public Property pEmailPort() As String
        Get
            Return EmailPort
        End Get
        Set(ByVal value As String)
            EmailPort = value
        End Set

    End Property
    Public Property pPath() As String
        Get
            Return sPath
        End Get
        Set(ByVal value As String)
            sPath = value
        End Set

    End Property
    Public Property pAttachFilename() As String
        Get
            Return AttachFileName
        End Get
        Set(ByVal value As String)
            AttachFileName = value
        End Set

    End Property

    Public Property pEmailIsHTML() As Boolean
        Get
            Return EmailIsHTML
        End Get
        Set(ByVal value As Boolean)
            EmailIsHTML = value
        End Set

    End Property

    Public Property pEmailSubject() As String
        Get
            If System.Configuration.ConfigurationManager.AppSettings("debugnow") = "1" Then
                Return "DMS Email Testing (debug mode)"
            Else
                Return EmailSubject
            End If

        End Get
        Set(ByVal value As String)
            EmailSubject = value
        End Set

    End Property

    Public Property pEmailBody() As String
        Get
            Return EmailBody
        End Get
        Set(ByVal value As String)
            EmailBody = value
        End Set

    End Property

    Public Property pEmailHost() As String
        Get
            Return EmailHost
        End Get
        Set(ByVal value As String)
            EmailHost = value
        End Set

    End Property

    Public Property pEmailFrom() As String
        Get
            Return EmailFrom
        End Get
        Set(ByVal value As String)
            EmailFrom = value
        End Set

    End Property

    Public Property pEmailTo() As String
        Get
            Return EmailTo
        End Get
        Set(ByVal value As String)
            EmailTo = value
        End Set

    End Property

    Public Property pEmailAttachment() As String
        Get
            Return EmailAttachment
        End Get
        Set(ByVal value As String)
            EmailAttachment = value
        End Set

    End Property

    Public Property pSEmailAttachment() As MemoryStream
        Get
            Return SEmailAttachment
        End Get
        Set(ByVal value As MemoryStream)
            SEmailAttachment = value
        End Set

    End Property

    Public Property pEmailCC() As String
        Get
            Return EmailCC
        End Get
        Set(ByVal value As String)
            EmailCC = value
        End Set

    End Property
    Public Property pEmailPassword() As String
        Get
            Return EmailPassword
        End Get
        Set(ByVal value As String)
            EmailPassword = value
        End Set

    End Property
    Public Sub SendEmail()

      
        Dim osmtp As System.Net.Mail.SmtpClient
        Dim osendEmail As Net.Mail.MailMessage
        Try
            osmtp = New System.Net.Mail.SmtpClient
            osendEmail = New Net.Mail.MailMessage(pEmailFrom, pEmailTo)
            osendEmail.Subject = pEmailSubject
            osendEmail.Body = pEmailBody
            osendEmail.IsBodyHtml = pEmailIsHTML
            'Dim data As New Net.Mail.Attachment(EmailAttachment, MediaTypeNames.Application.Octet)
            '' Add time stamp information for the file.
            'Dim disposition As ContentDisposition = data.ContentDisposition
            'disposition.CreationDate = IO.File.GetCreationTime(EmailAttachment)
            'disposition.ModificationDate = IO.File.GetLastWriteTime(EmailAttachment)
            'disposition.ReadDate = IO.File.GetLastAccessTime(EmailAttachment)
            '' Add the file attachment to this e-mail message.
            'sendEmail.Attachments.Add(data)
            'smtp.UseDefaultCredentials = True
            'osmtp..Timeout = 100000
            If DocSession.sSendEmail Then
                osmtp.Send(osendEmail)
            End If
        Catch smtpex As System.Net.Mail.SmtpException
            Throw New Exception("(Smtp)Email Warning: " & smtpex.Message)
        Catch mailex As MailException
            Throw New Exception("(Mail)Email Warning: " & mailex.Message)
        Catch ex As Exception
            Throw New Exception("Email Warning: " & ex.Message)
        Finally
            If Not osendEmail Is Nothing Then
                osendEmail.Dispose()
                osendEmail = Nothing
            End If
            If Not osmtp Is Nothing Then
                osmtp.Dispose()
                osmtp = Nothing
            End If
        End Try


    End Sub

    Public Sub SendEmailAttach()

        Dim smtp As System.Net.Mail.SmtpClient
        Dim sendEmail As Net.Mail.MailMessage
        Dim data As Net.Mail.Attachment
        Try
            smtp = New System.Net.Mail.SmtpClient
            sendEmail = New Net.Mail.MailMessage(pEmailFrom, pEmailTo)
            sendEmail.Subject = pEmailSubject
            sendEmail.Body = pEmailBody
            sendEmail.IsBodyHtml = pEmailIsHTML
            data = New Net.Mail.Attachment(EmailAttachment, MediaTypeNames.Application.Octet)

            ' Add time stamp information for the file.
            Dim disposition As ContentDisposition = data.ContentDisposition
            disposition.CreationDate = System.IO.File.GetCreationTime(EmailAttachment)
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(EmailAttachment)
            disposition.ReadDate = System.IO.File.GetLastAccessTime(EmailAttachment)
            ' Add the file attachment to this e-mail message.
            sendEmail.Attachments.Add(data)
            'smtp.UseDefaultCredentials = True
            smtp.Send(sendEmail)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not sendEmail Is Nothing Then
                sendEmail.Dispose()
                sendEmail = Nothing
            End If
            If Not smtp Is Nothing Then
                smtp.Dispose()
                smtp = Nothing
            End If

            If Not data Is Nothing Then
                data.Dispose()
                data = Nothing
            End If

        End Try


    End Sub
    Public Sub SendEmailSAttach()

        Dim smtp As System.Net.Mail.SmtpClient
        Dim sendEmail As Net.Mail.MailMessage
        Dim document As PdfSharp.Pdf.PdfDocument
        Dim ximg As PdfSharp.Drawing.XImage
        Dim page As PdfSharp.Pdf.PdfPage
        Dim gfx As XGraphics
        Try
            smtp = New System.Net.Mail.SmtpClient
            sendEmail = New Net.Mail.MailMessage(pEmailFrom, pEmailTo)
            Dim lbpermitprint As Boolean
            If DocSession.sCanPrint = "True" Then
                lbpermitprint = True
            Else
                lbpermitprint = False
            End If

            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                '' Get a fresh copy of the sample PDF file
                'Const filename As String = "Portable Document Format.pdf"
                'File.Copy(Path.Combine("../../../../../PDFs/", filename), Path.Combine(Directory.GetCurrentDirectory(), filename), True)

                ' Create the font for drawing the watermark
                'Dim font As New XFont("Times New Roman", emSize, XFontStyle.BoldItalic)

                ' Open an existing document for editing and loop through its pages
                document = OpenPDF(pPath)
                document.SecuritySettings.PermitPrint = lbpermitprint
                document.SecuritySettings.PermitExtractContent = False
                document.SecuritySettings.PermitAccessibilityExtractContent = False
                document.SecuritySettings.PermitModifyDocument = False
                document.SecuritySettings.PermitFullQualityPrint = lbpermitprint
                document.SecuritySettings.PermitFormsFill = False
                document.SecuritySettings.PermitAnnotations = False
                document.SecuritySettings.PermitAssembleDocument = False
                document.SecuritySettings.OwnerPassword = "docuvu"


                ' Set version to PDF 1.4 (Acrobat 5) because we use transparency.
                If document.Version < 14 Then
                    document.Version = 14
                End If

                For idx As Integer = 0 To document.Pages.Count - 1
                    'if (idx == 1) break;
                    page = document.Pages(idx)
                    ' Get an XGraphics object for drawing beneath the existing content
                    gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append)

                    ' Get the size (in point) of the text
                    'Dim size As XSize = gfx.MeasureString(watermark, font)

                    ' Define a rotation transformation at the center of the page
                    Dim dWidth As Double = CInt(page.Width)
                    Dim dHeight As Double = CInt(page.Height)
                    Dim pWidth As Double = 0
                    Dim pHeight As Double = 0

                    'gfx.TranslateTransform(dWidth / 2, dHeight / 2)
                    'gfx.RotateTransform(-Math.Atan(dHeight / dWidth) * 180 / Math.PI)
                    'gfx.TranslateTransform(-dWidth / 2, -dHeight / 2)

                    ' Create a string format
                    'Dim format As New XStringFormat()
                    'format.Alignment = XStringAlignment.Near
                    'format.LineAlignment = XLineAlignment.Near

                    ' Create a dimmed red brush
                    ' Dim brush As XBrush = New XSolidBrush(XColor.FromArgb(128, 255, 0, 0))

                    ' Draw the string
                    'gfx.DrawString(watermark, font, brush, New XPoint((dWidth - size.Width) / 2, (dHeight - size.Height) / 2), format)
                    'Dim ximg As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(context.Request.QueryString("location") & "watermark\watermark.png")
                    ximg = PdfSharp.Drawing.XImage.FromFile(DocSession.FileLoc & "watermark\watermark.png")

                    'If ximg.PixelWidth > CInt(page.Width) OrElse ximg.PixelHeight > CInt(page.Height) Then
                    '    'ximg.For.ScaleToFit(page.Width, page.Height)
                    '    dWidth = ximg.PixelWidth
                    '    dHeight = ximg.PixelWidth
                    'Else

                    '    dWidth = (CInt(page.Width) - ximg.PixelWidth) / 2
                    '    dHeight = (CInt(page.Height) - ximg.PixelHeight) / 2
                    '    pWidth = dWidth
                    '    pHeight = dHeight
                    '    dWidth = ximg.PixelWidth
                    '    dHeight = ximg.PixelHeight
                    'End If

                    If ximg.PixelWidth > CInt(page.Width) Then

                        dWidth = page.Width
                    Else
                        dWidth = (CInt(page.Width) - ximg.PixelWidth) / 2
                        pWidth = dWidth
                        dWidth = ximg.PixelWidth
                    End If

                    If ximg.PixelHeight > CInt(page.Height) Then

                        dHeight = page.Height
                    Else
                        dHeight = (CInt(page.Height) - ximg.PixelHeight) / 2
                        pHeight = dHeight
                        dHeight = ximg.PixelHeight
                    End If

                    gfx.DrawImage(ximg, pWidth, pHeight, dWidth, dHeight)

                    If lbpermitprint Then
                        Dim stime As DateTime = DateTime.Now
                        Dim lsMessage As String = "This is a hard copy of the " & DocSession.sReferenceNo & " - " & DocSession.sFileName & " printed from the Document Management System "
                        Dim lsMessage2 As String = "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
                            " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId & "."
                        Dim xloc, yloc As Double
                        Dim lxFnt As New XFont("Helvetica", 7)

                        Dim lsize As XSize = gfx.MeasureString(lsMessage, lxFnt)
                        xloc = 20
                        yloc = ximg.PixelHeight - 40 'CDbl(page.Height) - CDbl(lsize.Height) - 5
                        gfx.DrawString(lsMessage, lxFnt, XBrushes.Black, xloc, yloc, XStringFormats.Default)
                        gfx.DrawString(lsMessage2, lxFnt, XBrushes.Black, xloc, yloc + 15, XStringFormats.Default)
                    End If
                Next

                document.Save(outputStream, False)
                sendEmail.Subject = pEmailSubject
                sendEmail.Body = pEmailBody
                sendEmail.IsBodyHtml = pEmailIsHTML

                Dim ldata As Net.Mail.Attachment
                Try
                    ldata = New Net.Mail.Attachment(outputStream, pAttachFilename, "application/pdf")
                    sendEmail.Attachments.Add(ldata)
                    smtp.Send(sendEmail)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If Not ldata Is Nothing Then
                        ldata.Dispose()
                        ldata = Nothing

                    End If

                End Try



                'smtp.UseDefaultCredentials = True

                outputStream.Flush()
                outputStream.Close()
                outputStream.Dispose()


            End Using


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not sendEmail Is Nothing Then
                sendEmail.Dispose()
                sendEmail = Nothing
            End If
            If Not smtp Is Nothing Then
                smtp.Dispose()
                smtp = Nothing
            End If
            If Not document Is Nothing Then
                document.Dispose()
                document = Nothing
            End If
            If Not ximg Is Nothing Then
                ximg.Dispose()
                ximg = Nothing
            End If
        End Try


    End Sub

    'Public Sub SendEmailSAttachNew()

    '    Dim smtp As New System.Net.Mail.SmtpClient
    '    Dim sendEmail As New Net.Mail.MailMessage(pEmailFrom, pEmailTo)

    '    Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
    '    Dim img As iTextSharp.text.Image = Nothing
    '    Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
    '    Dim overContent As iTextSharp.text.pdf.PdfContentByte = Nothing
    '    Dim rect As iTextSharp.text.Rectangle = Nothing
    '    Dim X, Y As Single
    '    Dim pageCount As Integer = 0
    '    Dim lsMessage As String = ""
    '    'watermark variables
    '    Dim oDoc As DocEmail
    '    Dim lsPath, lsFileName As String
    '    'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
    '    lsFileName = DocSession.sCurrentFile
    '    Dim Encoding As New System.Text.UTF8Encoding()
    '    'lsPath = context.Request.QueryString("location") & lsFileName
    '    lsPath = DocSession.FileLoc & lsFileName
    '    Try
    '        oDoc = New DocEmail


    '        'If lbpermitprint ThenoNew
    '        Dim stime As DateTime = DateTime.Now
    '        Dim lss As String = "This is a hard copy of " & DocSession.sDocFileName & ","
    '        Dim lss2 As String = "printed from the Document Management System " & _
    '                    "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
    '            " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

    '        'End If


    '        Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
    '            Dim reader As New iTextSharp.text.pdf.PdfReader(lsPath)
    '            'Dim output_stream As New System.IO.MemoryStream
    '            'start watermeark
    '            img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
    '            stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
    '            pageCount = reader.NumberOfPages()

    '            Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
    '            Dim backgroundColor As BaseColor = New BaseColor(0, 0, 0)
    '            Dim fontColor As BaseColor = New BaseColor(0, 0, 0)

    '            stamper.SetEncryption(Nothing, Encoding.GetBytes("docuvu"), PdfWriter.AllowScreenReaders, PdfWriter.STRENGTH128BITS)
    '            For ii As Integer = 1 To pageCount
    '                rect = reader.GetPageSizeWithRotation(ii)
    '                img.ScalePercent(100)
    '                img.ScaleToFit(rect.Width, rect.Height)
    '                X = (rect.Width - img.ScaledWidth) / 2
    '                Y = (rect.Height - img.ScaledHeight) / 2
    '                img.SetAbsolutePosition(X, Y)
    '                overContent = stamper.GetOverContent(ii)
    '                overContent.AddImage(img)
    '                overContent.BeginText()
    '                overContent.SetFontAndSize(font, 6.0F)
    '                overContent.SetColorFill(fontColor)

    '                overContent.SetTextMatrix(15, 18)
    '                overContent.ShowText(lss)
    '                overContent.SetTextMatrix(15, 10)
    '                overContent.ShowText(lss2)
    '                overContent.EndText()
    '                'overContent.SaveState()
    '                'overContent.RestoreState()

    '            Next
    '            'stamper.Close()

    '            sendEmail.Subject = pEmailSubject
    '            sendEmail.Body = pEmailBody
    '            sendEmail.IsBodyHtml = pEmailIsHTML

    '            Dim ldata As Net.Mail.Attachment
    '            ldata = New Net.Mail.Attachment(outputStream, pAttachFilename, "application/pdf")

    '            sendEmail.Attachments.Add(ldata)
    '            'smtp.UseDefaultCredentials = True
    '            smtp.Send(sendEmail)
    '            outputStream.Flush()
    '            outputStream.Close()
    '            outputStream.Dispose()
    '            'stamper.Close()
    '            ldata.Dispose()
    '        End Using
    '    Catch ex As Exception

    '        If ex.Message = "PdfReader not opened with owner password" Then
    '            Throw New Exception("Cannot open encrypted file. Please upload pdf file without password.")
    '        Else
    '            Throw New Exception(ex.Message)

    '        End If

    '    Finally
    '        sendEmail.Dispose()
    '        smtp.Dispose()

    '    End Try


    'End Sub
    
    'Public Function ProcessRequest(ByVal lsPath As String) As MemoryStream
    '    'Dim lsPath, lsFileName As String
    '    ''lsFileName = 'context.Request.QueryString("filename") 'DocSession.solDocFileName
    '    'lsFileName = DocSession.sCurrentFile

    '    ''lsPath = context.Request.QueryString("location") & lsFileName
    '    'lsPath = DocSession.FileLoc & lsFileName
    '    Try
    '        Dim lbpermitprint As Boolean
    '        If DocSession.sCanPrint = "True" Then
    '            lbpermitprint = True
    '        Else
    '            lbpermitprint = False
    '        End If

    '        Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
    '            '' Get a fresh copy of the sample PDF file
    '            'Const filename As String = "Portable Document Format.pdf"
    '            'File.Copy(Path.Combine("../../../../../PDFs/", filename), Path.Combine(Directory.GetCurrentDirectory(), filename), True)

    '            ' Create the font for drawing the watermark
    '            'Dim font As New XFont("Times New Roman", emSize, XFontStyle.BoldItalic)

    '            ' Open an existing document for editing and loop through its pages
    '            Dim document As PdfSharp.Pdf.PdfDocument = OpenPDF(lsPath)
    '            document.SecuritySettings.PermitPrint = lbpermitprint
    '            document.SecuritySettings.PermitExtractContent = False
    '            document.SecuritySettings.PermitAccessibilityExtractContent = False
    '            document.SecuritySettings.PermitModifyDocument = False
    '            document.SecuritySettings.PermitFullQualityPrint = lbpermitprint
    '            document.SecuritySettings.PermitFormsFill = False
    '            document.SecuritySettings.PermitAnnotations = False
    '            document.SecuritySettings.PermitAssembleDocument = False
    '            document.SecuritySettings.OwnerPassword = "docuvu"

    '            ' Set version to PDF 1.4 (Acrobat 5) because we use transparency.
    '            If document.Version < 14 Then
    '                document.Version = 14
    '            End If

    '            For idx As Integer = 0 To document.Pages.Count - 1
    '                'if (idx == 1) break;
    '                Dim page As PdfSharp.Pdf.PdfPage = document.Pages(idx)
    '                ' Get an XGraphics object for drawing beneath the existing content
    '                Dim gfx As XGraphics = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append)
    '                ' Get the size (in point) of the text
    '                'Dim size As XSize = gfx.MeasureString(watermark, font)

    '                ' Define a rotation transformation at the center of the page
    '                Dim dWidth As Double = CInt(page.Width)
    '                Dim dHeight As Double = CInt(page.Height)
    '                Dim pWidth As Double = CInt(page.Width)
    '                Dim pHeight As Double = CInt(page.Height)
    '                Dim pY As Double = 0
    '                Dim pX As Double = 0
    '                'gfx.TranslateTransform(dWidth / 2, dHeight / 2)
    '                'gfx.RotateTransform(-Math.Atan(dHeight / dWidth) * 180 / Math.PI)
    '                'gfx.TranslateTransform(-dWidth / 2, -dHeight / 2)

    '                ' Create a string format
    '                'Dim format As New XStringFormat()
    '                'format.Alignment = XStringAlignment.Near
    '                'format.LineAlignment = XLineAlignment.Near

    '                ' Create a dimmed red brush
    '                ' Dim brush As XBrush = New XSolidBrush(XColor.FromArgb(128, 255, 0, 0))

    '                ' Draw the string
    '                'gfx.DrawString(watermark, font, brush, New XPoint((dWidth - size.Width) / 2, (dHeight - size.Height) / 2), format)
    '                'Dim ximg As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(context.Request.QueryString("location") & "watermark\watermark.png")
    '                Dim ximg As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(DocSession.FileLoc & "watermark\watermark.png")

    '                If ximg.PixelWidth > CInt(page.Width) OrElse ximg.PixelHeight > CInt(page.Height) Then
    '                    'ximg.ScaleToFit(rect.Width, rect.Height)
    '                    dWidth = (CInt(page.Width) - ximg.PixelWidth) / 2
    '                    dHeight = (CInt(page.Height) - ximg.PixelHeight) / 2
    '                Else
    '                    dWidth = (CInt(page.Width) - ximg.PixelWidth) / 2
    '                    dHeight = (CInt(page.Height) - ximg.PixelHeight) / 2
    '                End If
    '                dWidth = page.Width
    '                dHeight = page.Height
    '                'gfx.DrawImage(ximg, dWidth, dHeight)
    '                'gfx.DrawImage(ximg, pWidth / 4, pHeight / 4, pWidth / 2, pHeight / 2)
    '                gfx.DrawImage(ximg, 10, 10, pWidth - 20, pHeight - 20)

    '                'If lbpermitprint Then
    '                '    Dim xloc, yloc As Double
    '                '    Dim lxFnt As New XFont("Helvetica", 6)
    '                '    Dim lsMessage As String = "This is a hard copy of the " & DocSession.soldDocFileName & " printed from the Document Management System "
    '                '    Dim lsMessage2 As String = "on " & DateTime.Now.ToString("g") & _
    '                '        " by " & DocSession.sUserName & "."
    '                '    Dim lsize As XSize = gfx.MeasureString(lsMessage, lxFnt)
    '                '    xloc = 20
    '                '    yloc = 10 'CDbl(page.Height) - CDbl(lsize.Height) - 5
    '                '    gfx.DrawString(lsMessage, lxFnt, XBrushes.Black, xloc, yloc, XStringFormats.Default)
    '                '    gfx.DrawString(lsMessage2, lxFnt, XBrushes.Black, xloc, yloc, XStringFormats.Default)
    '                'End If
    '            Next
    '            document.Save(outputStream)

    '        End Using

    '    Catch ex As Exception

    '        If ex.Message = "PdfReader not opened with owner password" Then
    '            Throw New Exception("Cannot open encrypted file. Please upload pdf file without password.")
    '        Else
    '            Throw New Exception("File is missing. Please contact the administrator.")
    '        End If
    '    Finally

    '    End Try
    '    '
    'End Function

    ''' <summary>
    ''' uses itextsharp 4.1.6 to convert any pdf to 1.4 compatible pdf, called instead of PdfReader.open
    ''' </summary>
    ''' <param name="sFilename"></param>
    ''' <returns></returns>
    Public Function OpenPDF(ByVal sFilename As String) As PdfSharp.Pdf.PdfDocument

        Dim reader As PdfSharp.Pdf.PdfDocument

        Try
            reader = New PdfSharp.Pdf.PdfDocument()
            reader = PdfSharp.Pdf.IO.PdfReader.Open(sFilename, PdfDocumentOpenMode.Modify)
            Return reader
        Catch generatedExceptionName As PdfSharp.Pdf.IO.PdfReaderException
            'workaround if pdfsharp doesnt dupport this pdf
            Dim newName As MemoryStream
            Try
                newName = ReturnCompatiblePdf(sFilename)
                reader = PdfSharp.Pdf.IO.PdfReader.Open(newName, PdfDocumentOpenMode.Modify)
                Return reader
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If Not newName Is Nothing Then
                    newName.Dispose()
                    newName = Nothing
                End If
                If Not reader Is Nothing Then
                    reader.Dispose()
                    reader = Nothing
                End If
            End Try
        Finally
            If Not reader Is Nothing Then
                reader.Dispose()
                reader = Nothing
            End If
        End Try


    End Function


    ''' <summary>
    ''' uses itextsharp 4.1.6 to convert any pdf to 1.4 compatible pdf
    ''' </summary>
    ''' <param name="sFilename"></param>
    ''' <returns></returns>
    Private Function ReturnCompatiblePdf(ByVal sFilename As String) As MemoryStream
        Dim reader As iTextSharp.text.pdf.PdfReader
        Dim output_stream As New MemoryStream
        Dim n As Integer
        Dim document As iTextSharp.text.Document
        Dim writer As iTextSharp.text.pdf.PdfWriter
        Dim cb As iTextSharp.text.pdf.PdfContentByte
        Dim page As iTextSharp.text.pdf.PdfImportedPage

        Dim rotation As Integer
        Dim i As Integer = 0
        Try



            reader = New iTextSharp.text.pdf.PdfReader(sFilename)

            output_stream = New MemoryStream

            ' we retrieve the total number of pages
            n = reader.NumberOfPages
            ' step 1: creation of a document-object

            document = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1))
            ' step 2: we create a writer that listens to the document
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, output_stream)
            'write pdf that pdfsharp can understand

            writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_4)
            ' step 3: we open the document
            document.Open()
            cb = writer.DirectContent



            While i < n
                i += 1
                document.SetPageSize(reader.GetPageSizeWithRotation(i))
                document.NewPage()
                page = writer.GetImportedPage(reader, i)
                rotation = reader.GetPageRotation(i)
                If rotation = 90 OrElse rotation = 270 Then
                    cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, _
                    reader.GetPageSizeWithRotation(i).Height)
                Else
                    cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, _
                    0)
                End If
            End While

            '---- Keep the stream open!
            writer.CloseStream = False

            ' step 5: we close the document
            document.Close()

            Return output_stream
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not reader Is Nothing Then
                reader.Close()
                reader = Nothing
            End If
            If Not output_stream Is Nothing Then
                output_stream.Close()
                output_stream.Dispose()
                output_stream = Nothing
            End If
            If Not document Is Nothing Then
                document.Close()
                document.Dispose()
                document = Nothing
            End If
            If Not document Is Nothing Then
                writer.Close()
                writer.Dispose()
                writer = Nothing
            End If
            If Not cb Is Nothing Then
                
                cb = Nothing
            End If
            If Not page Is Nothing Then
                page = Nothing
            End If
        End Try
    End Function
   
    Public Function RecipientList() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            'Dim s_sql As String = "SELECT u.userid,username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''),email = isnull(u.email,'') " & _
            '                        "FROM users u "
            Dim s_sql As String = ""
            If DocSession.OraClient Then
                s_sql = "SELECT NVL(recipientname,' ') as recipientname,NVL(recipientemail,' ') as recipientemail FROM recipientlist WHERE userid = '" & DocSession.sUserId & "'"
            Else
                s_sql = "SELECT isnull(recipientname,'') as recipientname,isnull(recipientemail,'') as recipientemail FROM recipientlist WHERE userid = '" & DocSession.sUserId & "'"
            End If

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill()
            Return ldata

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Sub UpdateEmailAddress(ByVal asAddress As String)
        Dim objCommand As clsSqlConn

        Try
            Dim sEmail As String() = asAddress.Split({","}, StringSplitOptions.RemoveEmptyEntries)
            Dim s_sql As String = ""

            If DocSession.OraClient Then
                s_sql = "BEGIN DELETE FROM RecipientList WHERE userid = '" & DocSession.sUserId & "'; "
                For i = 0 To sEmail.Length - 1
                    s_sql = s_sql & " INSERT INTO RecipientList(userid,RecipientEmail,RecipientName) VALUES('" & DocSession.sUserId & "','" & sEmail(i) & "',''); "
                Next
                s_sql = s_sql & " END; "
            Else
                s_sql = "DELETE FROM RecipientList WHERE userid = '" & DocSession.sUserId & "' "
                For i = 0 To sEmail.Length - 1
                    s_sql = s_sql & " INSERT INTO RecipientList(userid,RecipientEmail,RecipientName) VALUES('" & DocSession.sUserId & "','" & sEmail(i) & "','') "
                Next
            End If


            
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql


            objCommand.ExecNonQuery()

        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub

    Public Sub UpdateLegalContent()
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "BEGIN UPDATE LegalMailContent SET MailContent='" & Replace(pMailContent, "'", "''") & "',GoogleDriveSpaceLink ='" & Replace(pLink, "'", "''") & "',Subject ='" & Replace(pSubject, "'", "''") & "'  WHERE  " & _
            " userId='" & DocSession.sUserId & "'; " & _
            " IF SQL%ROWCOUNT = 0 THEN " & _
            "INSERT INTO LegalMailContent(Userid, MailContent,GoogleDriveSpaceLink,Subject) " & _
            "VALUES ('" & DocSession.sUserId & "','" & Replace(pMailContent, "'", "''") & "','" & Replace(pLink, "'", "''") & "','" & Replace(pSubject, "'", "''") & "'); " & _
            "END IF; " & _
            "END;"

            Else


                objCommand.CommandText = "UPDATE LegalMailContent SET MailContent='" & Replace(pMailContent, "'", "''") & "',GoogleDriveSpaceLink ='" & Replace(pLink, "'", "''") & "',Subject ='" & Replace(pSubject, "'", "''") & "'  WHERE  " & _
                            " userId='" & DocSession.sUserId & "' " & _
                            " if @@rowcount = 0 " & _
                            "INSERT INTO LegalMailContent(Userid, MailContent,GoogleDriveSpaceLink,Subject) " & _
                            "VALUES ('" & DocSession.sUserId & "','" & Replace(pMailContent, "'", "''") & "','" & Replace(pLink, "'", "''") & "','" & Replace(pSubject, "'", "''") & "') "
            End If

            objCommand.ExecNonQuery()

        Finally


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub
    Public Function LegalContent() As DataTable
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String = ""
            If DocSession.OraClient Then
                s_sql = "SELECT NVL(MailContent,' ') as MailContent,NVL(Subject,' ') as Subject,NVL(GoogleDriveSpaceLink,' ') as GoogleDriveSpaceLink FROM LegalMailContent WHERE rownum = 1 "
            Else
                s_sql = "SELECT TOP 1 isnull(MailContent,'') as MailContent,isnull(Subject,'') as Subject,isnull(GoogleDriveSpaceLink,'') as GoogleDriveSpaceLink FROM LegalMailContent "
            End If

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill()
            Return ldata
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
End Class


