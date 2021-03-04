Imports System
Imports System.Web
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D


Public Class ViewDoc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        Dim lsPath, lsFileName As String
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName
        If Not System.IO.File.Exists(lsPath) Then
            lsPath = DocSession.FileLoc2 & lsFileName
        End If
        Dim lsext As String = System.IO.Path.GetExtension(lsPath).ToLower
        If lsext = ".pdf" Then
            ViewPDF()
            'ViewDoc()
        ElseIf (lsext = ".doc" OrElse lsext = ".docx" OrElse lsext = ".xls" Or lsext = ".xlsx" OrElse lsext = ".ppt" Or lsext = ".pptx") Then
            ViewDoc()
        Else
            ViewImage()
        End If

    End Sub
    Private Sub ViewImage()


        Dim lsPath, lsFileName As String
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName

        If System.IO.File.Exists(lsPath) Then
            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                'Dim inputStream As System.IO.FileStream = New System.IO.FileStream(lsPath, IO.FileMode.Open)
                Dim aBitmap As Bitmap = System.Drawing.Image.FromFile(lsPath)
                Dim inputStream As Bitmap = New System.Drawing.Bitmap(aBitmap.Width, aBitmap.Height, PixelFormat.Format48bppRgb)

                'outputStream.SetLength(inputStream.Length)
                Dim xpos As Single, ypos As Single, xh As Single, yw As Single

                Dim canvas As Graphics = Graphics.FromImage(inputStream)
                canvas.DrawImage(aBitmap, 0, 0, aBitmap.Width, aBitmap.Height)
                Dim fwm As Drawing.Image = System.Drawing.Image.FromFile(DocSession.FileLoc & "watermark\watermark.png")

                If fwm.Width >= inputStream.Width Then
                    xpos = (inputStream.Width - (inputStream.Width / 2)) / 2
                    ypos = (inputStream.Height - (inputStream.Height / 2)) / 2
                    xh = (inputStream.Height / 2)
                    yw = (inputStream.Width / 2)
                    canvas.DrawImage(fwm, xpos, ypos, yw, xh)
                Else
                    Dim liCnt As Integer = Math.Floor(inputStream.Height / fwm.Height)
                    For i = 1 To liCnt
                        xpos = (inputStream.Width - fwm.Width) / 2
                        ypos = (i * fwm.Height) - fwm.Height
                        canvas.DrawImage(fwm, xpos, ypos)
                    Next
                End If


                'inputStream.Read(outputStream.GetBuffer, 0, inputStream.Length)
                inputStream.SetResolution(96, 96)
                inputStream.Save(outputStream, ImageFormat.Jpeg)

                Dim cntnt As Byte() = outputStream.ToArray

                outputStream.Close()
                inputStream.Dispose()
                canvas.Dispose()
                fwm.Dispose()
                aBitmap.Dispose()

                'Response.ContentType = "application/pdf"
                'Response.AddHeader("body", "oncontextmenu='return false;'")
                Response.BinaryWrite(cntnt)
                Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            End Using


        ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
            Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                'Dim inputStream As System.IO.FileStream = New System.IO.FileStream(lsPath, IO.FileMode.Open)
                Dim aBitmap As Bitmap = System.Drawing.Image.FromFile(lsPath)
                Dim inputStream As Bitmap = New System.Drawing.Bitmap(aBitmap.Width, aBitmap.Height, PixelFormat.Format48bppRgb)

                'outputStream.SetLength(inputStream.Length)
                Dim xpos As Single, ypos As Single, xh As Single, yw As Single

                Dim canvas As Graphics = Graphics.FromImage(inputStream)
                canvas.DrawImage(aBitmap, 0, 0, aBitmap.Width, aBitmap.Height)
                Dim fwm As Drawing.Image = System.Drawing.Image.FromFile(DocSession.FileLoc & "watermark\watermark.png")

                If fwm.Width >= inputStream.Width Then
                    xpos = (inputStream.Width - (inputStream.Width / 2)) / 2
                    ypos = (inputStream.Height - (inputStream.Height / 2)) / 2
                    xh = (inputStream.Height / 2)
                    yw = (inputStream.Width / 2)
                    canvas.DrawImage(fwm, xpos, ypos, yw, xh)
                Else
                    Dim liCnt As Integer = Math.Floor(inputStream.Height / fwm.Height)
                    For i = 1 To liCnt
                        xpos = (inputStream.Width - fwm.Width) / 2
                        ypos = (i * fwm.Height) - fwm.Height
                        canvas.DrawImage(fwm, xpos, ypos)
                    Next
                End If


                'inputStream.Read(outputStream.GetBuffer, 0, inputStream.Length)
                inputStream.SetResolution(96, 96)
                inputStream.Save(outputStream, ImageFormat.Jpeg)

                Dim cntnt As Byte() = outputStream.ToArray

                outputStream.Close()
                inputStream.Dispose()
                canvas.Dispose()
                fwm.Dispose()
                aBitmap.Dispose()

                'Response.ContentType = "application/pdf"
                'Response.AddHeader("body", "oncontextmenu='return false;'")
                Response.BinaryWrite(cntnt)
                Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
            End Using


        Else
            Response.Write("File Not Found! Contact the administrator.")
        End If


    End Sub

    Private Sub ViewDoc()
        Dim lsPath, lsFileName As String
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName
        Try

            If System.IO.File.Exists(lsPath) Then
                Dim TargetFile As New System.IO.FileInfo(lsPath)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "inline; filename=" + TargetFile.Name)
                Response.AddHeader("Content-Length", TargetFile.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(TargetFile.FullName)
                Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
                TargetFile = Nothing


            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
                Dim TargetFile As New System.IO.FileInfo(lsPath)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "inline; filename=" + TargetFile.Name)
                Response.AddHeader("Content-Length", TargetFile.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(TargetFile.FullName)
                Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
                TargetFile = Nothing


            Else
                Response.Write("File Not Found! Contact the administrator.")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ViewPDF()
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0

        Dim lsPath, lsFileName As String
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName
        Try

            If System.IO.File.Exists(lsPath) Then
                Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                    reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                    rect = reader.GetPageSizeWithRotation(1)
                    stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                    img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")

                    If img.Width > rect.Width OrElse img.Height > rect.Height Then
                        img.ScaleToFit(rect.Width, rect.Height)
                        X = (rect.Width - img.ScaledWidth) / 2
                        Y = (rect.Height - img.ScaledHeight) / 2
                    Else
                        X = (rect.Width - img.Width) / 2
                        Y = (rect.Height - img.Height) / 2
                    End If
                    img.SetAbsolutePosition(X, Y)
                    pageCount = reader.NumberOfPages()

                    For i As Integer = 1 To pageCount
                        underContent = stamper.GetOverContent(i)
                        underContent.AddImage(img)
                    Next

                    'underContent.SaveState()
                    'stamper.FormFlattening = True
                    'outputStream.SetLength(inputStream.Length)
                    stamper.Close()
                    'stamper = Nothing

                    'img = Nothing

                    reader.Close()
                    outputStream.Close()
                    Dim cntnt As Byte() = outputStream.ToArray
                    Response.Clear()
                    'Response.ContentType = "application/pdf"
                    Response.BinaryWrite(cntnt)


                    ''Response.AddHeader("Content-Disposition", "inline; filename=view.pdf")
                    'Response.AddHeader("Content-Length", cntnt.Length.ToString)
                    'Response.ContentType = "application/octet-stream"
                    'Response.WriteFile("viewpdf")
                    'Response.End()

                    'Response.End()
                    'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

                End Using

            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
                Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                    reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                    rect = reader.GetPageSizeWithRotation(1)
                    stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                    img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")

                    If img.Width > rect.Width OrElse img.Height > rect.Height Then
                        img.ScaleToFit(rect.Width, rect.Height)
                        X = (rect.Width - img.ScaledWidth) / 2
                        Y = (rect.Height - img.ScaledHeight) / 2
                    Else
                        X = (rect.Width - img.Width) / 2
                        Y = (rect.Height - img.Height) / 2
                    End If
                    img.SetAbsolutePosition(X, Y)
                    pageCount = reader.NumberOfPages()

                    For i As Integer = 1 To pageCount
                        underContent = stamper.GetOverContent(i)
                        underContent.AddImage(img)
                    Next

                    'underContent.SaveState()
                    'stamper.FormFlattening = True
                    'outputStream.SetLength(inputStream.Length)
                    stamper.Close()
                    'stamper = Nothing

                    'img = Nothing

                    reader.Close()
                    outputStream.Close()
                    Dim cntnt As Byte() = outputStream.ToArray
                    Response.Clear()
                    'Response.ContentType = "application/pdf"
                    Response.BinaryWrite(cntnt)


                    ''Response.AddHeader("Content-Disposition", "inline; filename=view.pdf")
                    'Response.AddHeader("Content-Length", cntnt.Length.ToString)
                    'Response.ContentType = "application/octet-stream"
                    'Response.WriteFile("viewpdf")
                    'Response.End()

                    'Response.End()
                    'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

                End Using

            Else
                Response.Write("File Not Found! Contact the administrator.")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ViewPDF2()
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0

        Dim lsPath, lsFileName As String
        'Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName
        Try

            If System.IO.File.Exists(lsPath) Then
                Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                    reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                    rect = reader.GetPageSizeWithRotation(1)
                    stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                    img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")

                    If img.Width > rect.Width OrElse img.Height > rect.Height Then
                        img.ScaleToFit(rect.Width, rect.Height)
                        X = (rect.Width - img.ScaledWidth) / 2
                        Y = (rect.Height - img.ScaledHeight) / 2
                    Else
                        X = (rect.Width - img.Width) / 2
                        Y = (rect.Height - img.Height) / 2
                    End If
                    img.SetAbsolutePosition(X, Y)
                    pageCount = reader.NumberOfPages()

                    For i As Integer = 1 To pageCount
                        underContent = stamper.GetOverContent(i)
                        underContent.AddImage(img)
                    Next

                    'underContent.SaveState()
                    'stamper.FormFlattening = True
                    'outputStream.SetLength(inputStream.Length)
                    stamper.Close()
                    'stamper = Nothing

                    'img = Nothing

                    reader.Close()
                    outputStream.Close()
                    Dim cntnt As Byte() = outputStream.ToArray
                    'Response.ContentType = "application/pdf"
                    Response.BinaryWrite(cntnt)
                    Response.End()
                    'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

                End Using

            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
                Using outputStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                    reader = New iTextSharp.text.pdf.PdfReader(lsPath)
                    rect = reader.GetPageSizeWithRotation(1)
                    stamper = New iTextSharp.text.pdf.PdfStamper(reader, outputStream)
                    img = iTextSharp.text.Image.GetInstance(DocSession.FileLoc & "watermark\watermark.png")

                    If img.Width > rect.Width OrElse img.Height > rect.Height Then
                        img.ScaleToFit(rect.Width, rect.Height)
                        X = (rect.Width - img.ScaledWidth) / 2
                        Y = (rect.Height - img.ScaledHeight) / 2
                    Else
                        X = (rect.Width - img.Width) / 2
                        Y = (rect.Height - img.Height) / 2
                    End If
                    img.SetAbsolutePosition(X, Y)
                    pageCount = reader.NumberOfPages()

                    For i As Integer = 1 To pageCount
                        underContent = stamper.GetOverContent(i)
                        underContent.AddImage(img)
                    Next

                    'underContent.SaveState()
                    'stamper.FormFlattening = True
                    'outputStream.SetLength(inputStream.Length)
                    stamper.Close()
                    'stamper = Nothing

                    'img = Nothing

                    reader.Close()
                    outputStream.Close()
                    Dim cntnt As Byte() = outputStream.ToArray
                    'Response.ContentType = "application/pdf"
                    Response.BinaryWrite(cntnt)
                    Response.End()
                    'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")

                End Using

            Else
                Response.Write("File Not Found! Contact the administrator.")
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub AddWatermarkImage(ByVal sourceFile As String, ByVal outputFile As String, ByVal watermarkImage As String)
    '    Dim reader As iTextSharp.text.pdf.PdfReader = Nothing

    '    Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing

    '    Dim img As iTextSharp.text.Image = Nothing

    '    Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing

    '    Dim rect As iTextSharp.text.Rectangle = Nothing

    '    Dim X, Y As Single

    '    Dim pageCount As Integer = 0
    '    Try

    '        If System.IO.File.Exists(sourceFile) Then
    '            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
    '            rect = reader.GetPageSizeWithRotation(1)

    '            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))
    '            img = iTextSharp.text.Image.GetInstance(watermarkImage)


    '            If img.Width > rect.Width OrElse img.Height > rect.Height Then

    '                img.ScaleToFit(rect.Width, rect.Height)

    '                X = (rect.Width - img.ScaledWidth) / 2

    '                Y = (rect.Height - img.ScaledHeight) / 2

    '            Else

    '                X = (rect.Width - img.Width) / 2

    '                Y = (rect.Height - img.Height) / 2

    '            End If
    '            img.SetAbsolutePosition(X, Y)


    '            pageCount = reader.NumberOfPages()

    '            For i As Integer = 1 To pageCount
    '                underContent = stamper.GetOverContent(i)

    '                underContent.AddImage(img)

    '            Next

    '            'underContent.SaveState()



    '            stamper.FormFlattening = True


    '        Else

    '            '("File Does Not Exist", "Missing File")
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        stamper.Close()
    '        'stamper = Nothing

    '        'img = Nothing

    '        reader.Close()
    '        'reader = Nothing
    '    End Try

    'End Sub
End Class