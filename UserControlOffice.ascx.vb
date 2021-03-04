Public Class UserControlOffice
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveOffice()
            RetrieveAddress()
        End If
    End Sub

    Private Sub RetrieveOffice()
        Dim oOfc As DocOffice
        Dim ldata As DataTable
        Try
            oOfc = New DocOffice
            ldata = oOfc.RetrieveOffice()
            dlOffices.DataSource = ldata
            dlOffices.DataTextField = "OfcDesc"
            dlOffices.DataValueField = "OfficeCode"
            dlOffices.DataBind()
            If ldata.Rows.Count > 0 Then
                imgUpdate.Visible = True
                imgDelete.Visible = True
            Else
                imgUpdate.Visible = False
                imgDelete.Visible = False
            End If
            'pnlOffice.Update()
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

    Private Sub RetrieveAddress()
        Dim oOfc As DocAddress
        Dim ldata As DataTable
        Dim lrow As DataRow
        Try
            oOfc = New DocAddress
            ldata = oOfc.RetrieveAddress()
            dlAddress.DataSource = ldata
            lrow = ldata.NewRow
            lrow("AddressCode") = ""
            lrow("AddrDesc") = ""
            ldata.Rows.InsertAt(lrow, 0)
            dlAddress.DataTextField = "AddrDesc"
            dlAddress.DataValueField = "AddressCode"
            dlAddress.DataBind()
            
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

    Private Sub AddOffice()
        Dim oOfc As DocOffice
        Try
            oOfc = New DocOffice
            oOfc.pOfcCode = tbOfcCode.Text.Trim
            oOfc.pOfcDesc = tbOfcDesc.Text.Trim
            oOfc.AddOffice()
            
            lblMsg.Text = "Save successfull."
            RetrieveOffice()
            dlOffices.SelectedValue = tbOfcCode.Text.Trim
            tbOfcCode.Text = ""
            tbOfcDesc.Text = ""
            pnlOffice.Update()
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally

        End Try
    End Sub
    Private Sub UpdateOffice()
        Dim oOfc As DocOffice
        Try
            oOfc = New DocOffice
            oOfc.pOfcCode = tbOfcCode.Text.Trim
            oOfc.pOfcDesc = tbOfcDesc.Text.Trim
            oOfc.pAddrCode = dlAddress.SelectedValue
            oOfc.UpdateOffice()
            RetrieveOffice()
            dlOffices.SelectedValue = tbOfcCode.Text.Trim
            pAddOffice.Visible = False
            pnlOffice.Update()
        Catch ex As Exception
            lblMsg.Text = "Error while updating Office. Please try again."
        Finally

        End Try
    End Sub
    Private Sub DeleteOffice()
        Dim oOfc As DocOffice
        Try
            oOfc = New DocOffice
            oOfc.pOfcCode = lblOfcCode.Text
            oOfc.DeleteOffice()
            RetrieveOffice()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub imgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAdd.Click
        If Not pAddOffice.Visible Then
            pAddOffice.Visible = True
            pDeleteOffice.Visible = False

        End If
        tbOfcCode.Enabled = True
        tbOfcCode.Text = ""
        tbOfcDesc.Text = ""
        tbOfcCode.CssClass = "entryfld"
        tbOfcCode.Focus()
        pnlOffice.Update()
    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pAddOffice.Visible = False
        pnlOffice.Update()
    End Sub

    Private Sub imgDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDelete.Click
        Dim oOFc As DocOffice
        Try
            oOFc = New DocOffice
            pAddOffice.Visible = False
            pnlOffice.Update()
            pDeleteOffice.Visible = True
            lblOfcCode.Text = dlOffices.SelectedValue
            oOFc.pOfcCode = dlOffices.SelectedValue
            lblOfcDesc.Text = oOFc.RetrieveOfficeDesc()
            pnlOffice.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Dim oOFc As DocOffice
        Dim ldata As DataTable
        Try
            oOFc = New DocOffice
            oOFc.pOfcCode = dlOffices.SelectedValue
            ldata = oOFc.RetrieveOfficeInfo()
            If ldata.Rows.Count > 0 Then
                tbOfcDesc.Text = ldata(0)("Description")
                dlAddress.SelectedValue = IIf(IsDBNull(ldata(0)("AddressCode")), "", ldata(0)("AddressCode"))
            Else
                tbOfcDesc.Text = ""
                dlAddress.SelectedValue = ""
            End If
            tbOfcDesc.Focus()
            tbOfcCode.Text = dlOffices.SelectedValue
            pAddOffice.Visible = True
            tbOfcCode.CssClass = "entryflddisabled"
            tbOfcCode.Enabled = False
            lblMsg.Visible = False
            pnlOffice.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not oOFc Is Nothing Then
                oOFc = Nothing
            End If
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try


        
    End Sub

    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click

        If tbOfcCode.Enabled Then
            AddOffice()
            

        Else
            UpdateOffice()
        End If

    End Sub

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        pDeleteOffice.Visible = False
        pnlOffice.Update()
    End Sub

    Private Sub btOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btOk.Click
        DeleteOffice()
        pDeleteOffice.Visible = False
        pnlOffice.Update()
    End Sub
End Class