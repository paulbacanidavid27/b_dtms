Public Class ucFormAttach
    Inherits System.Web.UI.UserControl
    Public Event e_refresh()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub ResetFields()
        tbDescription.Text = ""
        tbDescription2.Text = ""
        tbDescription3.Text = ""
        lmsg.Text = ""
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
        Dim lodoc As clsDocForms
        'Dim ohist As FormHistory
        Dim lsFile As String

        Dim sYear As String
        Dim sMonth As String
        Dim lsFolder As String
        Dim lsF As String
        Dim bAdded As Boolean = False
        Dim lsmsg As String = ""
        Try
            lodoc = New clsDocForms
            'ohist = New FormHistory

            If ValidFile() Then
                lsFile = fileAttachment.FileName
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\forms"
                lsF = lsFolder & "\" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If
                lsF = Replace(lsF, ",", "")
                If System.IO.File.Exists(lsF) Then
                    lsmsg = lsmsg & "File " & lsFile & " already exist. "
                Else
                    'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                    'remove comma
                    fileAttachment.SaveAs(lsF)

                    'lodoc.pAttachId = DocSession.getNextID("attachid")
                    'lodoc.pDocId = DocSession.sDocID
                    lodoc.pFormFileName = Replace(lsFile, ",", "")
                    lodoc.pUploadedBy = DocSession.sUserId
                    lodoc.pUploadedDate = DateTime.Now.ToString
                    lodoc.pDescription = tbDescription.Text
                    lodoc.pFileSize = fileAttachment.PostedFile.ContentLength.ToString
                    lodoc.AddDocForms()

                    'ohist.pFormId = DocSession.sFormID
                    'ohist.pIpAddress = Request.UserHostAddress
                    'ohist.pUserId = DocSession.sUserId
                    'ohist.pAction = "Added Form '" & lsFile & "'"
                    'ohist.AddHistory()
                    bAdded = True
                    lsmsg = lsmsg & "File " & lsFile & " was uploaded successfully. "
                End If
            End If
            If ValidFile2() Then
                lsFile = fileAttachment2.FileName
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\forms"
                lsF = lsFolder & "\" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If

                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                lsF = Replace(lsF, ",", "") 'remove comma
                If System.IO.File.Exists(lsF) Then
                    lsmsg = lsmsg & "File " & lsFile & " already exist. "
                Else
                    fileAttachment2.SaveAs(lsF)

                    lodoc.pFormFileName = Replace(lsFile, ",", "")
                    lodoc.pUploadedBy = DocSession.sUserId
                    lodoc.pUploadedDate = DateTime.Now.ToString
                    lodoc.pDescription = tbDescription2.Text


                    lodoc.pFileSize = fileAttachment2.PostedFile.ContentLength.ToString
                    If lodoc.pFileSize = "" Then
                        lodoc.pFileSize = "0"
                    End If
                    lodoc.AddDocForms()


                    bAdded = True
                    lsmsg = lsmsg & "File " & lsFile & " was uploaded successfully. "
                End If
            End If
            If ValidFile3() Then
                lsFile = fileAttachment3.FileName
                '--new folder
                sYear = Year(CDate(pCreatedDate))
                sMonth = MonthName(Month(CDate(pCreatedDate)))
                lsFolder = DocSession.FileLoc & sYear & "\" & sMonth & "\forms"
                lsF = lsFolder & "\" & lsFile
                If Not System.IO.Directory.Exists(lsFolder) Then
                    System.IO.Directory.CreateDirectory(lsFolder)
                End If
                'Dim lsF As String = DocSession.FileLoc & "attachment\" & DocSession.sDocID & "_" & lsFile
                lsF = Replace(lsF, ",", "") 'remove comma
                If System.IO.File.Exists(lsF) Then
                    lsmsg = lsmsg & "File " & lsFile & " already exist."
                Else
                    fileAttachment3.SaveAs(lsF)

                    lodoc.pFormFileName = Replace(lsFile, ",", "")
                    lodoc.pUploadedBy = DocSession.sUserId
                    lodoc.pUploadedDate = DateTime.Now.ToString
                    lodoc.pDescription = tbDescription3.Text

                    lodoc.pFileSize = fileAttachment3.PostedFile.ContentLength.ToString
                    If lodoc.pFileSize = "" Then
                        lodoc.pFileSize = "0"
                    End If
                    lodoc.AddDocForms()


                    bAdded = True
                    lsmsg = lsmsg & "File " & lsFile & " was uploaded successfully."
                End If

            End If
            If bAdded Then


                DisplayMsg(lsmsg)
                tbDescription.Text = ""
                tbDescription2.Text = ""
                tbDescription3.Text = ""
                RaiseEvent e_refresh()

            Else
                If lsmsg <> "" Then
                    fErrorMsg(lsmsg)
                Else
                    If Not fileAttachment.HasFile AndAlso Not fileAttachment2.HasFile AndAlso Not fileAttachment3.HasFile Then
                        DisplayMsg("Please select a File to be attached before clicking on Save.")
                    End If
                End If
            End If

        Catch ex As Exception
            fErrorMsg("Error while uploading the file forms (" & ex.Message & "). Please try again.")
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

                If lsFile.IndexOf(".") = 0 Then
                    fErrorMsg("Invalid file name.")
                    Return False
                ElseIf lsFile.Split(".").Length > 2 Then
                    fErrorMsg("Invalid file name. File name should only contain one period.")
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
                lsFile = Replace(lsFile, ";", "")

                If lsFile.IndexOf(".") = 0 Then
                    fErrorMsg("Invalid file name.")
                    Return False
                ElseIf lsFile.Split(".").Length > 2 Then
                    fErrorMsg("Invalid file name. File name should only contain one period.")
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
                lsFile = Replace(lsFile, ";", "")

                If lsFile.IndexOf(".") = 0 Then
                    fErrorMsg("Invalid file name.")
                    Return False
                ElseIf lsFile.Split(".").Length > 2 Then
                    fErrorMsg("Invalid file name. File name should only contain one period.")
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

            Return True
        Else
            Return False
        End If

    End Function

End Class