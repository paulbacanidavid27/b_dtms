Public Class UserControlAddTag
    Inherits System.Web.UI.UserControl
    Public Event e_click()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Try


            If txtTags.Text <> "Add New Tag" Then
                Dim oTag As New DocTags
                oTag.pDocId = CInt(DocSession.sDocID)
                oTag.pIpAddress = Request.UserHostAddress
                oTag.pTag = txtTags.Text
                oTag.pUserId = DocSession.sUserId
                oTag.pDocName = DocSession.sDocFileName
                oTag.SaveDocTag()
                lcheckmsg.Text = "Tag has been saved successfully."
                lcheckmsg.CssClass = "msg_green"
                'RetrieveTags()
                'pDocTags.Update() 'need to check again
                txtTags.Text = ""
                txtTags.Focus()
                upAddTag.Update()
            End If

        Catch ex As Exception
            lcheckmsg.Text = "There's an error while saving the record ( " & ex.Message & " ) . Please try again"
            lcheckmsg.CssClass = "msg_red"
            upAddTag.Update()
        End Try

    End Sub

    Private Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        Me.Visible = False
        RaiseEvent e_click()
    End Sub
End Class