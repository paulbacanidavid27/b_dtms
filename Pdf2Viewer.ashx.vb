Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Diagnostics
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
'Imports PdfSharp
'Imports PdfSharp.Drawing
'Imports PdfSharp.Pdf
'Imports PdfSharp.Pdf.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.api
Public Class Pdf2Viewer
    Implements System.Web.IHttpHandler
    Implements System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
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
            'If Not System.IO.File.Exists(lsPath) Then
            '    lsPath = System.Configuration.ConfigurationManager.AppSettings("altattachloc") & "\" & lsFileName
            'End If
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


            'If lbpermitprint ThenoNew
            Dim stime As DateTime = DateTime.Now
            Dim lss As String = "This is a hard copy of " & DocSession.sReferenceNo & " - " & DocSession.sFileName & ","
            Dim lss2 As String = "printed from the Document Management System " & _
                        "on " & stime.ToString("dd/MM/yyyy") & " at " & stime.ToString("HH:MM") & _
                " by: P" & stime.ToString("MM") & stime.ToString("HHmm") & stime.ToString("dd") & stime.ToString("yy") & DocSession.sUserId.ToUpper & "."

            'End If


            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                reader.unethicalreading = True
                'Dim output_stream As New System.IO.MemoryStream
                'start watermeark
                img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")
                stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                pageCount = reader.NumberOfPages()

                Dim font As BaseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.WINANSI, BaseFont.EMBEDDED)
                Dim backgroundColor As BaseColor = New BaseColor(0, 0, 0)
                Dim fontColor As BaseColor = New BaseColor(0, 0, 0)

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
                context.Response.Clear()
                context.Response.ContentType = "application/pdf"
                context.Response.BinaryWrite(cntnt)

            End Using
        Catch ex As Exception

            If ex.Message = "PdfReader not opened with owner password" Then
                context.Response.Write("Cannot open encrypted file. Please upload pdf file without password.")
            ElseIf (ex.Message = "Bad user password") Then
                context.Response.Write("Cannot open encrypted file.")
                context.Session("s_doc_encr") = "true"
            Else
                context.Response.Write("Error occurred while reading the file (error:" + ex.Message + ").")
                'context.Response.Write("File is missing. Please contact the administrator.")
                context.Session("s_doc_encr") = "true"
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


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    


End Class