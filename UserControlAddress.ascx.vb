Public Class UserControlAddress
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveAddress()
        End If
    End Sub

    Private Sub RetrieveAddress()
        Dim oOfc As DocAddress
        Dim ldata As DataTable
        Try
            oOfc = New DocAddress
            ldata = oOfc.RetrieveAddress()
            dlAddresses.DataSource = ldata
            dlAddresses.DataTextField = "AddrDesc"
            dlAddresses.DataValueField = "AddressCode"
            dlAddresses.DataBind()
            If ldata.Rows.Count > 0 Then
                imgUpdate.Visible = True
                imgDelete.Visible = True
            Else
                imgUpdate.Visible = False
                imgDelete.Visible = False
            End If
            'pnlAddress.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not oOfc Is Nothing Then

                oOfc = Nothing
            End If
        End Try
    End Sub
    Private Sub AddAddress()
        Dim oOfc As DocAddress
        Try
            oOfc = New DocAddress
            oOfc.pAddrCode = tbCode.Text.Trim
            oOfc.pAddrDesc = tbDesc.Text.Trim
            oOfc.AddAddress()

            lblMsg.Text = "Save successfull."
            RetrieveAddress()
            dlAddresses.SelectedValue = tbCode.Text.Trim
            tbCode.Text = ""
            tbDesc.Text = ""
            pnlAddress.Update()
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally

        End Try
    End Sub
    Private Sub UpdateAddress()
        Dim oOfc As DocAddress
        Try
            oOfc = New DocAddress
            oOfc.pAddrCode = tbCode.Text.Trim
            oOfc.pAddrDesc = tbDesc.Text.Trim
            oOfc.UpdateAddress()
            RetrieveAddress()
            dlAddresses.SelectedValue = tbCode.Text.Trim
            pAddAddress.Visible = False
            pnlAddress.Update()
        Catch ex As Exception
            lblMsg.Text = "Error while updating Address. Please try again."
        Finally

        End Try
    End Sub
    Private Sub DeleteAddress()
        Dim oOfc As DocAddress
        Try
            oOfc = New DocAddress
            oOfc.pAddrCode = lblCode.Text
            oOfc.DeleteAddress()
            RetrieveAddress()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub imgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAdd.Click
        If Not pAddAddress.Visible Then
            pAddAddress.Visible = True
            pDelete.Visible = False

        End If
        tbCode.Enabled = True
        tbCode.Text = ""
        tbDesc.Text = ""
        tbCode.CssClass = "entryfld"
        tbCode.Focus()
        pnlAddress.Update()
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pAddAddress.Visible = False
        pnlAddress.Update()
    End Sub

    Private Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
        Dim oOFc As DocAddress
        Try
            oOFc = New DocAddress
            pAddAddress.Visible = False
            pnlAddress.Update()
            pDelete.Visible = True
            lblCode.Text = dlAddresses.SelectedValue
            oOFc.pAddrCode = dlAddresses.SelectedValue
            lblDesc.Text = oOFc.RetrieveAddressDesc()
            pnlAddress.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Dim oOFc As DocAddress
        Try
            oOFc = New DocAddress
            oOFc.pAddrCode = dlAddresses.SelectedValue
            tbDesc.Text = oOFc.RetrieveAddressDesc()
            tbDesc.Focus()
            tbCode.Text = dlAddresses.SelectedValue
            pAddAddress.Visible = True
            tbCode.CssClass = "entryflddisabled"
            tbCode.Enabled = False
            lblMsg.Visible = False
            pnlAddress.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try



    End Sub

    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click

        If tbCode.Enabled Then
            AddAddress()


        Else
            UpdateAddress()
        End If

    End Sub

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        pDelete.Visible = False
        pnlAddress.Update()
    End Sub

    Private Sub btOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOk.Click
        DeleteAddress()
        pDelete.Visible = False
        pnlAddress.Update()
    End Sub
End Class