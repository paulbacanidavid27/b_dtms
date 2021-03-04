Public Class UserControlPDFViewer2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub ViewPDF()
        'Dim lsPath, lsFileName As String
        'lsFileName = 'context.Request.QueryString("filename") 'DocSession.soldDocFileName
        'lsFileName = DocSession.sCurrentFile
        'Dim Encoding As New System.Text.UTF8Encoding()
        'lsPath = context.Request.QueryString("location") & lsFileName
        'lsPath = DocSession.FileLoc & lsFileName
        docvx.Attributes("src") = "Pdf2Viewer.ashx?dt=" & Date.Now()
        'docvx.Attributes("src") = lsPath & "?dt=" & Date.Now()
        docvx.Visible = True
    End Sub

    

    
End Class