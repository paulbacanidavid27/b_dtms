Public Class ucDocUpload
    Inherits System.Web.UI.UserControl
    Public Event e_refresh()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public WriteOnly Property setRefresh As String
        Set(ByVal value As String)
            hfrefresh.Value = value
            DisplayMsg("")
        End Set
    End Property

    Public WriteOnly Property pCreatedDate As String
        Set(ByVal value As String)
            hfCreatedDate.Value = value

        End Set
    End Property
    Public WriteOnly Property pVersion As String
        Set(ByVal value As String)
            hfVersion.Value = value

        End Set
    End Property
    Public WriteOnly Property pRefNo As String
        Set(ByVal value As String)
            hfRefNo.Value = value

        End Set
    End Property
    Private Sub fErrorMsg(ByVal asMsg As String)
        lmsg.CssClass = "msg_red"
        lmsg.Text = asMsg
    End Sub
    Private Sub DisplayMsg(ByVal asMsg As String)
        lmsg.CssClass = "msg_green"
        lmsg.Text = asMsg
    End Sub
    Private Sub btUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpload.Click
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn
        
        Try


            If fileAttachment.HasFile Then
                Dim lsFile As String = fileAttachment.FileName
                Dim lsFile2 As String = lsFile
                lsFile.Replace(" ", "_")
                'Dim lsF As String = DocSession.FileLoc & DocSession.sDocID & "_1_" & lsFile
                Dim sYear As String = Year(CDate(hfCreatedDate.Value))
                Dim sMonth As String = MonthName(Month(CDate(hfCreatedDate.Value)))

                Dim lsF As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & hfRefNo.Value & "\" & DocSession.sDocID & "_" & hfVersion.Value & "_" & lsFile
                Dim lsFolder As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & hfRefNo.Value
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If

                If ValidFile() Then
                    fileAttachment.SaveAs(lsF)
                    ltr = New DbTran
                    objCommand = New clsSqlConn(ltr.pTran)

                    Dim lodoc As New DocAttachment
                    lodoc.pFileSize = fileAttachment.PostedFile.ContentLength.ToString
                    lodoc.pDocFileName = lsFile2
                    lodoc.pDocId = DocSession.sDocID
                    lodoc.pDocFileName = lsFile
                    lodoc.UploadFile(objCommand)
                    lodoc.UpdateDocListFileVersion(objCommand)
                    Dim ohist As New DocHistory
                    ohist.pDocId = DocSession.sDocID
                    ohist.pIpAddress = Request.UserHostAddress
                    ohist.pTask = "Upload"
                    ohist.pUserId = DocSession.sUserId
                    ohist.pAction = "Uploaded '" & lsFile2 & "'."
                    ohist.AddHistory(objCommand)
                    ltr.pTran.Commit()
                    DisplayMsg("File has been successfully uploaded.")
                    If hfrefresh.Value = "y" Then
                        RaiseEvent e_refresh()
                    End If

                End If

            End If

        Catch ex As Exception

            fErrorMsg("Error while uploading the file (" & ex.Message & "). Please try again.")
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally

        End Try
    End Sub
    Private Sub imgEmailClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        Me.Visible = False
        'PanelEmail1.Visible = False
        'PanelEmail2.Visible = False
        'If hfRestorePDFViewer.Value = "Y" Then
        '    hfRestorePDFViewer.Value = "N"
        '    'docvw.Visible = True
        'End If
    End Sub
    Private Function ValidFile() As Boolean
        If fileAttachment.HasFile Then
            Dim lsFile As String = fileAttachment.FileName
            Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
            If System.IO.File.Exists(lsF) Then
                fErrorMsg("File already attached.")
            ElseIf Len(lsFile) > 200 Then
                fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                Return False
            End If
            Dim linfo As New System.IO.FileInfo(fileAttachment.PostedFile.FileName)
            If fileAttachment.PostedFile.ContentLength > DocSession.MaxFileSize Then
                fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB is allowed.")
                Return False
            End If
            If Len(lsFile) > 200 Then
                fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                Return False
            End If

            If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
            Else
                fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted.")
                'pnlMsg.Update()
                Return False
            End If
        End If
        Return True
    End Function
End Class