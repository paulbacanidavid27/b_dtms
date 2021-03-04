Imports System.Web
Imports System.Web.Services
Imports System
Imports System.IO
'Imports System.IO.Packaging
Imports System.Xml


Public Class DocViewer2
    Implements System.Web.IHttpHandler
    Implements System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'Dim lsPath, lsFileName As String
        'lsFileName = context.Request.QueryString("filename") 'DocSession.soldDocFileName
        'lsPath = context.Request.QueryString("location") & lsFileName
        Dim lsPath, lsFileName As String
        lsFileName = DocSession.sCurrentFile
        lsPath = DocSession.FileLoc & lsFileName
        Try

            If System.IO.File.Exists(DocSession.FileLoc & lsFileName) Then
                Dim TargetFile As New System.IO.FileInfo(DocSession.FileLoc & lsFileName)
                context.Response.Clear()
                context.Response.AddHeader("Content-Disposition", "inline; filename=" + TargetFile.Name)
                context.Response.AddHeader("Content-Length", TargetFile.Length.ToString())
                context.Response.ContentType = "application/octet-stream"
                context.Response.WriteFile(TargetFile.FullName)
                context.Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
                TargetFile = Nothing

            ElseIf System.IO.File.Exists(DocSession.FileLoc2 & lsFileName) Then
                Dim TargetFile As New System.IO.FileInfo(DocSession.FileLoc2 & lsFileName)
                context.Response.Clear()
                context.Response.AddHeader("Content-Disposition", "inline; filename=" + TargetFile.Name)
                context.Response.AddHeader("Content-Length", TargetFile.Length.ToString())
                context.Response.ContentType = "application/octet-stream"
                context.Response.WriteFile(TargetFile.FullName)
                context.Response.End()
                'ClientScript.RegisterStartupScript(Me.GetType(), "ViewReport", "<script type=text/javascript>window.open('viewDoc.aspx', 'ReportViewer' ,'location=no,toolbar=no,menubar=yes,status=yes,height=450, width=750,left=20, top=20, resizable=yes, scrollbars=yes')</script>")
                TargetFile = Nothing


            Else
                context.Response.Write("File Not Found! Contact the administrator.")
            End If

        Catch ex As Exception

        End Try

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    'Public Sub ReadDocx()
    '    Dim pack As Package = Package.Open("c:\Data\Hello.docx", _
    '                                   FileMode.Open, _
    '                                   FileAccess.Read)

    '    '*** Write code here to create parts and add content

    '    '*** Close package
    '    Dim xd As XmlDocument = New XmlDocument
    '    XmlConvert.

    '    pack.Close()



    'End Sub

End Class