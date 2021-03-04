Public Class UserControlMainGroup
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveRecords()
        End If
    End Sub

    Private Sub RetrieveRecords()
        Dim oOfc As DocMainGroup
        Dim ldata As DataTable
        Try
            oOfc = New DocMainGroup
            ldata = oOfc.RetrieveMainGroup()
            dlMainGroups.DataSource = ldata
            dlMainGroups.DataTextField = "MainGroupDesc"
            dlMainGroups.DataValueField = "MainGroupId"
            dlMainGroups.DataBind()
            If ldata.Rows.Count > 0 Then
                imgUpdate.Visible = True
                imgDelete.Visible = True
            Else
                imgUpdate.Visible = False
                imgDelete.Visible = False
            End If
            'pnlMainGroup.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not oOfc Is Nothing Then
                oOfc = Nothing
            End If
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub
    Private Sub AddMainGroup()
        Dim oOfc As DocMainGroup
        Try
            oOfc = New DocMainGroup
            oOfc.pMainGroupId = tbMainGroupId.Text.Trim
            oOfc.pMainGroupDesc = tbMainGroupDesc.Text.Trim
            oOfc.AddMainGroup()

            lblMsg.Text = "Save successfull."
            RetrieveRecords()
            dlMainGroups.SelectedValue = tbMainGroupId.Text.Trim
            tbMainGroupId.Text = ""
            tbMainGroupDesc.Text = ""
            pnlMainGroup.Update()
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally

        End Try
    End Sub
    Private Sub UpdateMainGroup()
        Dim oOfc As DocMainGroup
        Try
            oOfc = New DocMainGroup
            oOfc.pMainGroupId = tbMainGroupId.Text.Trim
            oOfc.pMainGroupDesc = tbMainGroupDesc.Text.Trim
            oOfc.UpdateMainGroup()
            RetrieveRecords()
            dlMainGroups.SelectedValue = tbMainGroupId.Text.Trim
            pAddMainGroup.Visible = False
            pnlMainGroup.Update()
        Catch ex As Exception
            lblMsg.Text = "Error while updating MainGroup. Please try again."
        Finally

        End Try
    End Sub
    Private Sub DeleteMainGroup()
        Dim oOfc As DocMainGroup
        Try
            oOfc = New DocMainGroup
            oOfc.pMainGroupId = lblMainGroupId.Text
            oOfc.DeleteMainGroup()
            RetrieveRecords()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub imgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAdd.Click
        If Not pAddMainGroup.Visible Then
            pAddMainGroup.Visible = True
            pDeleteMainGroup.Visible = False

        End If
        tbMainGroupId.Enabled = True
        tbMainGroupId.Text = ""
        tbMainGroupDesc.Text = ""
        tbMainGroupId.CssClass = "entryfld"
        tbMainGroupId.Focus()
        pnlMainGroup.Update()
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pAddMainGroup.Visible = False
        pnlMainGroup.Update()
    End Sub

    Private Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
        Dim oOFc As DocMainGroup
        Try
            oOFc = New DocMainGroup
            pAddMainGroup.Visible = False
            pnlMainGroup.Update()
            pDeleteMainGroup.Visible = True
            lblMainGroupId.Text = dlMainGroups.SelectedValue
            oOFc.pMainGroupId = dlMainGroups.SelectedValue
            lblMainGroupDesc.Text = oOFc.RetrieveMainGroupDesc()
            pnlMainGroup.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Dim oOFc As DocMainGroup
        Try
            oOFc = New DocMainGroup
            oOFc.pMainGroupId = dlMainGroups.SelectedValue
            tbMainGroupDesc.Text = oOFc.RetrieveMainGroupDesc()
            tbMainGroupDesc.Focus()
            tbMainGroupId.Text = dlMainGroups.SelectedValue
            pAddMainGroup.Visible = True
            tbMainGroupId.CssClass = "entryflddisabled"
            tbMainGroupId.Enabled = False
            lblMsg.Visible = False
            pnlMainGroup.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try



    End Sub

    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click

        If tbMainGroupId.Enabled Then
            AddMainGroup()


        Else
            UpdateMainGroup()
        End If

    End Sub

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        pDeleteMainGroup.Visible = False
        pnlMainGroup.Update()
    End Sub

    Private Sub btOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOk.Click
        DeleteMainGroup()
        pDeleteMainGroup.Visible = False
        pnlMainGroup.Update()
    End Sub
End Class