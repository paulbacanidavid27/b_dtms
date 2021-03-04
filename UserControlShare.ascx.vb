
Public Class UserControlShare
    Inherits System.Web.UI.UserControl
    Dim lFN As String
    Dim lsMessage As String

    Public Event e_click()
    Public ReadOnly Property pMessage As String
        Get
            Return lsMessage
        End Get
    End Property

    Public WriteOnly Property pFileName As String

        Set(ByVal value As String)
            lFN = value
            lblFile.Text = value
        End Set
    End Property

    Public WriteOnly Property pFile As String

        Set(ByVal value As String)
            lblAttachment.Text = value
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucEmail.e_check, AddressOf SelectEmail
        AddHandler ucEmail.e_uncheck, AddressOf RemoveEmail
        AddHandler ucEmail.e_click, AddressOf CloseEmail
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblFrom.Text = DocSession.sUserEmail
        If DocSession.sUserRole = "L" OrElse DocSession.sUserRole = "A" Then
            btSettings.Visible = True
        Else
            btSettings.Visible = False
        End If

    End Sub


    Public Sub LoadRecipients()
        Dim ldata As DataTable
        Try
            lblEmailMsg.Text = ""
            Dim oList As New DocEmail
            ldata = oList.RecipientList()
            Dim sEmail As String = ""
            For Each lrow As DataRow In ldata.Rows
                If sEmail = "" Then
                    sEmail = lrow("RecipientEmail")
                Else
                    sEmail = sEmail & ", " & lrow("RecipientEmail")
                End If
            Next
            tbRecipient.Text = sEmail
            ldata.Dispose()
            ldata = oList.LegalContent
            If ldata.Rows.Count > 0 Then
                tbSubject.Text = ldata(0)("Subject")
                tbBody.Text = ldata(0)("MailContent")
            End If
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    Public Sub ufReshEmail()
        ucEmail.RefreshEmail()
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

    Private Sub btCancelEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancelEmail.Click
        'PanelEmail1.Visible = False
        'PanelEmail2.Visible = False
        'If hfRestorePDFViewer.Value = "Y" Then
        '    hfRestorePDFViewer.Value = "N"
        '    'docvw.Visible = True
        'End If
        Me.Visible = False
    End Sub

    Private Sub BtSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtSend.Click
        Dim lsAdminEmail As String = System.Configuration.ConfigurationManager.AppSettings("adminemail")
        Dim rgex As New RegularExpressionValidator
        'rgex.ValidationExpression = "\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        'rgex.
        'rgex.ControlToValidate = "tbRecipient"
        'rgex.Validate()
        lblEmailMsg.Text = ""

        Try
            If DocSession.sUserEmail <> "" Then
                lsAdminEmail = DocSession.sUserName & " <" & DocSession.sUserEmail & ">"
            End If
            Dim oEmail As New DocEmail
            oEmail.pEmailFrom = lsAdminEmail
            oEmail.pEmailTo = tbRecipient.Text
            oEmail.pEmailSubject = tbSubject.Text
            If tbRecipient.Text.Trim = "" Then
                lblEmailMsg.Text = "Please enter recipient."
                tbRecipient.Focus()
                Exit Sub
            End If
            'If Not rgex.IsValid Then
            '    lblEmailMsg.Text = "Invalid recipient email."
            '    tbRecipient.Focus()
            '    Exit Sub
            'End If
            If System.IO.File.Exists(lblFile.Text) Then

                'oEmail.pEmailAttachment = lblFile.Text
                'Dim oPDF As New pdfObject
                'oEmail.pSEmailAttachment = oPDF.ProcessRequest(lblFile.Text)
                oEmail.pPath = lblFile.Text
                oEmail.pAttachFilename = lblAttachment.Text
            Else
                lblEmailMsg.Text = "Attachment does not exists."

                Exit Sub
            End If


            oEmail.pEmailBody = "<pre style='font-size:12pt;font-family:Times New Roman'>" & Replace(tbBody.Text, "<docrefno>", DocSession.sReferenceNo) & "</pre>"
            oEmail.pEmailIsHTML = True

            Dim linfo As New System.IO.FileInfo(lblFile.Text)
            If linfo.Extension.ToLower = ".pdf" Then
                oEmail.SendEmailSAttach()
            Else
                oEmail.SendEmailAttach()
            End If

            tbRecipient.Text = ""
            tbSubject.Text = ""
            tbBody.Text = ""
            lsMessage = "Email has been sent."
            Dim oHist As New DocHistory
            oHist.pAction = "Shared document to " & oEmail.pEmailTo
            oHist.pDocId = DocSession.sDocID
            oHist.pIpAddress = Request.UserHostAddress
            oHist.pTask = "Email"
            oHist.pUserId = DocSession.sUserId
            oHist.AddHistory()
            RaiseEvent e_click()
            Me.Visible = False
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            lblEmailMsg.Text = "Email occurred while sending email. ( " & ex.Message & ") Please try again."
        Finally
            If Not rgex Is Nothing Then
                rgex.Dispose()
            End If


        End Try
    End Sub

    Private Sub imgUserEmail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUserEmail.Click
        pnlUserEmail.Visible = Not pnlUserEmail.Visible

        'ucEmail.Visible = True
        pemail.Update()
    End Sub

    Private Sub CloseEmail()
        pnlUserEmail.Visible = False 'Not pnlUserEmail.Visible
        pemail.Update()
    End Sub

    Private Sub SelectEmail()
        If tbRecipient.Text.Trim = "" Then
            tbRecipient.Text = ucEmail.pUserEmail
        Else
            tbRecipient.Text = tbRecipient.Text.Trim & "," & ucEmail.pUserEmail()
        End If

        pnlrecipient.Update()
    End Sub
    Private Sub RemoveEmail()
        tbRecipient.Text = "," & tbRecipient.Text
        tbRecipient.Text = tbRecipient.Text.Replace("," & ucEmail.pUserEmail, "")
        'tbRecipient.Text = tbRecipient.Text.Replace(",,", ",")
        pnlrecipient.Update()
    End Sub

    Private Sub btSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSettings.Click
        Try
            Dim oEmail As New DocEmail
            oEmail.pSubject = tbSubject.Text
            oEmail.pMailContent = tbBody.Text
            oEmail.pLink = ""
            oEmail.UpdateLegalContent()
            oEmail.UpdateEmailAddress(tbRecipient.Text)
            lblEmailMsg.Text = "Settings has been saved successfully."
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class