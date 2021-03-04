Public Class ucAttach
    Inherits System.Web.UI.UserControl
    Public Event e_refresh()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Property pCreatedDate As String
        Get
            Return hfCreatedDate.Value
        End Get
        Set(ByVal value As String)
            hfCreatedDate.Value = value
        End Set
    End Property
    Public WriteOnly Property setRefresh As String
        Set(ByVal value As String)
            hfrefresh.Value = value
            DisplayMsg("")
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
        Dim lodoc As DocAttachment
        Dim ohist As DocHistory
        Dim lsFile As String

        Dim sYear As String
        Dim sMonth As String
        Dim lsFolder As String
        Dim lsF As String
        Dim bAdded As Boolean = False
        Try
            lodoc = New DocAttachment
            ohist = New DocHistory

            If ValidFile() Then
                lsFile = fileAttachment.FileName
                lsFile = Replace(lsFile, ",", "") 'remove comma
                lsFile = Replace(lsFile, "&", "n") 'remove comma
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                lsF = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                lsF = Replace(lsF, ",", "") 'remove comma
                lsF = Replace(lsF, "&", "n") 'remove comma
                fileAttachment.SaveAs(lsF)

                lodoc.pAttachId = DocSession.getNextID("attachid")
                lodoc.pDocId = DocSession.sDocID
                lodoc.pDocFileName = Replace(lsFile, ",", "")
                lodoc.pAttachedBy = DocSession.sUserId
                lodoc.pFileSize = fileAttachment.PostedFile.ContentLength.ToString
                lodoc.AddAttachment()

                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Attachment"
                ohist.pUserId = DocSession.sUserId
                ohist.pAction = "Added attachment '" & lsFile & "'"
                ohist.AddHistory()
                bAdded = True

            End If
            If ValidFile2() Then
                lsFile = fileAttachment2.FileName
                lsFile = Replace(lsFile, ",", "") 'remove comma
                lsFile = Replace(lsFile, "&", "n") 'remove comma
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                lsF = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                lsF = Replace(lsF, ",", "") 'remove comma
                lsF = Replace(lsF, "&", "n") 'remove comma
                fileAttachment2.SaveAs(lsF)

                lodoc.pAttachId = DocSession.getNextID("attachid")
                lodoc.pDocId = DocSession.sDocID
                lodoc.pDocFileName = Replace(lsFile, ",", "")
                lodoc.pAttachedBy = DocSession.sUserId
                lodoc.pFileSize = fileAttachment2.PostedFile.ContentLength.ToString
                lodoc.AddAttachment()

                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Attachment"
                ohist.pUserId = DocSession.sUserId
                ohist.pAction = "Added attachment '" & lsFile & "'"
                ohist.AddHistory()
                bAdded = True

            End If
            If ValidFile3() Then
                lsFile = fileAttachment3.FileName
                lsFile = Replace(lsFile, ",", "") 'remove comma
                lsFile = Replace(lsFile, "&", "n") 'remove comma
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                lsF = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                lsF = Replace(lsF, ",", "") 'remove comma
                fileAttachment3.SaveAs(lsF)

                lodoc.pAttachId = DocSession.getNextID("attachid")
                lodoc.pDocId = DocSession.sDocID
                lodoc.pDocFileName = Replace(lsFile, ",", "")
                lodoc.pAttachedBy = DocSession.sUserId

                lodoc.pFileSize = fileAttachment3.PostedFile.ContentLength.ToString
                If lodoc.pFileSize = "" Then
                    lodoc.pFileSize = "0"
                End If
                lodoc.AddAttachment()

                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Attachment"
                ohist.pUserId = DocSession.sUserId
                ohist.pAction = "Added attachment '" & lsFile & "'"
                ohist.AddHistory()
                bAdded = True

            End If
            If bAdded Then


                DisplayMsg("File Attachment(s) has been successfully added.")
                If hfrefresh.Value = "y" Then
                    RaiseEvent e_refresh()
                End If
            Else
                If Not fileAttachment.HasFile AndAlso Not fileAttachment2.HasFile AndAlso Not fileAttachment3.HasFile Then
                    DisplayMsg("Please select a File to be attached before clicking on Save.")
                End If

            End If

        Catch ex As Exception
            fErrorMsg("Error while uploading the file attachments (" & ex.Message & "). Please try again.")
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

            Try
                Dim lsFile As String = fileAttachment.FileName
                lsFile = Replace(lsFile, ",", "")
                lsFile = Replace(lsFile, ";", "")
                Dim sYear As String = Year(CDate(pCreatedDate))
                Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
                Dim lsFolder As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                Dim lsF As String = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                If lsFile.IndexOf(".") = 0 Then
                    fErrorMsg("Invalid file. File name should have a valid extension (e.g. .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff, .tif)")
                    Return False
                ElseIf lsFile.Split(".").Length > 2 Then
                    fErrorMsg("Invalid file name. File name should only contain one period before the file name extension.")
                    Return False
                ElseIf System.IO.File.Exists(lsF) Then
                    fErrorMsg("Attachment with the same file name already exist.")
                    Return False
                ElseIf Len(lsFile) > 200 Then
                    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                    Return False
                End If
                Dim linfo As New System.IO.FileInfo(fileAttachment.PostedFile.FileName)
                If fileAttachment.PostedFile.ContentLength > DocSession.MaxFileSize Then
                    fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB is allowed.")
                    Return False
                End If
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                Else
                    fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted.")
                    'pnlMsg.Update()
                    Return False
                End If
            Catch ex As Exception
                fErrorMsg("Error occurred while uploading attachment (" & ex.Message & "). Please try again.")
                Return False
            Finally

            End Try

            'If Len(lsFile) > 200 Then
            '    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
            '    Return False
            'End If
            Return True
        Else
            Return False
        End If

    End Function

    Private Function ValidFile2() As Boolean
        If fileAttachment2.HasFile Then

            Try
                Dim lsFile As String = fileAttachment2.FileName
                lsFile = Replace(lsFile, ",", "")
                lsFile = Replace(lsFile, "&", "n")
                Dim sYear As String = Year(CDate(pCreatedDate))
                Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
                Dim lsFolder As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                Dim lsF As String = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                If System.IO.File.Exists(lsF) Then
                    fErrorMsg("Attachment with the same file name already exist.")
                    Return False
                ElseIf Len(lsFile) > 200 Then
                    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                    Return False
                End If
                Dim linfo As New System.IO.FileInfo(fileAttachment2.PostedFile.FileName)
                If fileAttachment2.PostedFile.ContentLength > DocSession.MaxFileSize Then
                    fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB is allowed.")
                    Return False
                End If
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                Else
                    fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted.")
                    'pnlMsg.Update()
                    Return False
                End If
            Catch ex As Exception
                fErrorMsg("Error occurred while uploading attachment (" & ex.Message & "). Please try again.")
                Return False
            Finally

            End Try

            'If Len(lsFile) > 200 Then
            '    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
            '    Return False
            'End If


            Return True
        Else
            Return False
        End If

    End Function

    Private Function ValidFile3() As Boolean
        If fileAttachment3.HasFile Then

            Try
                Dim lsFile As String = fileAttachment3.FileName
                lsFile = Replace(lsFile, ",", "")
                lsFile = Replace(lsFile, "&", "n")
                Dim sYear As String = Year(CDate(pCreatedDate))
                Dim sMonth As String = MonthName(Month(CDate(pCreatedDate)))
                Dim lsFolder As String = DocSession.FileLoc & sYear & "\" & sMonth & "\" & DocSession.sReferenceNo & "\attachment"
                Dim lsF As String = lsFolder & "\" & DocSession.sDocID & "_" & lsFile
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                If System.IO.File.Exists(lsF) Then
                    fErrorMsg("Attachment with the same file name already exist.")
                    Return False
                ElseIf Len(lsFile) > 200 Then
                    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
                    Return False
                End If
                Dim linfo As New System.IO.FileInfo(fileAttachment3.PostedFile.FileName)
                If fileAttachment3.PostedFile.ContentLength > DocSession.MaxFileSize Then
                    fErrorMsg("** File size too big. Only a maximum of " & CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) & "MB is allowed.")
                    Return False
                End If
                If linfo.Extension.ToLower() = ".pdf" OrElse linfo.Extension.ToLower() = ".gif" OrElse linfo.Extension.ToLower() = ".doc" OrElse linfo.Extension.ToLower() = ".docx" OrElse linfo.Extension.ToLower() = ".xls" OrElse linfo.Extension.ToLower() = ".xlsx" OrElse linfo.Extension.ToLower() = ".ppt" OrElse linfo.Extension.ToLower() = ".pptx" OrElse linfo.Extension.ToLower() = ".tiff" OrElse linfo.Extension.ToLower() = ".tif" OrElse linfo.Extension.ToLower() = ".png" OrElse linfo.Extension.ToLower() = ".jpg" OrElse linfo.Extension.ToLower() = ".jpeg" Then
                Else
                    fErrorMsg("** Invalid file. Only the .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .gif, .png, .jpeg, .jpg, .tiff,.tif files are accepted.")
                    'pnlMsg.Update()
                    Return False
                End If
            Catch ex As Exception
                fErrorMsg("Error occurred while uploading attachment (" & ex.Message & "). Please try again.")
                Return False
            Finally

            End Try

            'If Len(lsFile) > 200 Then
            '    fErrorMsg("** File name is too long (should not be more than 200 characters). Please rename the file before uploading.")
            '    Return False
            'End If


            Return True
        Else
            Return False
        End If

    End Function

End Class