Public Class UserControlReport
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If DocSession.sUserRole = "U" OrElse DocSession.sUserRole = "L" Then
            pDocumentStatus.Visible = False
            pGroupStatus.Visible = False
            pDocumentsUploaded.Visible = False
            If DocSession.sOfcCode = "CRD" Then
                pDocumentsUploadedHourly.Visible = True
            Else
                pDocumentsUploadedHourly.Visible = False
            End If
            pDocumentChanges.Visible = False
            pDocumentIndex.Visible = False
            pDocumentIndexStatus.Visible = False
            pDocumentRoutingHistory.Visible = False
            pDocumentArchived.Visible = True
            pDocumentArchivedPerUser.Visible = False
            pDocumentReceived.Visible = True
            pDocumentListPerUser.Visible = False
            pTotalAmount.Visible = False
            pRecordsDisposition.Visible = False
            pTotalFiles.Visible = False
            pDeletedDocuments.Visible = False
        ElseIf DocSession.sUserRole = "R" OrElse DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
            pDocumentStatus.Visible = True
            pGroupStatus.Visible = True
            pDocumentsUploaded.Visible = True
            pDocumentsUploadedHourly.Visible = True
            If DocSession.sOfcCode = "CRD" Then

                pRecordsDisposition.Visible = True
            Else
                'pDocumentsUploadedHourly.Visible = False
                pRecordsDisposition.Visible = False
            End If
            pDocumentChanges.Visible = True
            pDocumentIndex.Visible = True
            pDocumentIndexStatus.Visible = True
            pDocumentRoutingHistory.Visible = True
            pDocumentArchived.Visible = True
            pDocumentArchivedPerUser.Visible = True
            pDocumentReceived.Visible = True
            pDocumentListPerUser.Visible = True
            pTotalAmount.Visible = False

            pTotalFiles.Visible = False
            pDeletedDocuments.Visible = False
        ElseIf DocSession.sUserRole = "A" Then
            pDocumentStatus.Visible = True
            pGroupStatus.Visible = True
            pDocumentsUploaded.Visible = True
            pDocumentsUploadedHourly.Visible = True
            pDocumentChanges.Visible = True
            pDocumentIndex.Visible = True
            pDocumentIndexStatus.Visible = True
            pDocumentRoutingHistory.Visible = True
            pDocumentArchived.Visible = True
            pDocumentArchivedPerUser.Visible = True
            pDocumentReceived.Visible = True
            pDocumentListPerUser.Visible = True
            pTotalAmount.Visible = True
            pRecordsDisposition.Visible = True
            pTotalFiles.Visible = True
            pDeletedDocuments.Visible = True

        End If
    End Sub

End Class