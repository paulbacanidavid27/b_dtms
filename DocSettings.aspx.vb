Public Class DocSettings
    Inherits System.Web.UI.Page

    Private Sub DocSettings_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ucMenu.SelectTab("Settings")
        Master.SelectTab("Account")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        If Not IsPostBack Then
            SetValues()
        End If
    End Sub
    Private Sub SetValues()
        Dim oset As New DocSetting
        oset.getSettings()
        tbRows.Text = oset.pRowsPerPage
        tbTimeout.Text = oset.pPageTimeOut
        tbAdminEmail.Text = oset.pAdminEmail
        tbArchivePerson.Text = oset.pArchiveperson
        tbEmailPassword.Text = oset.pEmailPassword
        tbEmailUserName.Text = oset.pEmailUserName
        tbEmailHost.Text = oset.pEmailHost
        tbEmailPort.Text = oset.pEmailPort
        cbEmailEnableSSL.Checked = oset.pEnableSSL
        tbPurgePointPerson.Text = oset.pPurgeEmailRecipient
    End Sub

    Private Function getValues() As Integer
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT top 1 pagetimeout,rowsperpage FROM docsettings"
            ldata = objCommand.Fill()
            If ldata.Rows.Count > 0 Then
                'DocSession.RowsPerPage = CInt(IIf(IsDBNull(ldata(0)("pagetimeout")), System.Configuration.ConfigurationManager.AppSettings("pagetimeout"), ldata(0)("pagetimeout")))
                tbRows.Text = CInt(IIf(IsDBNull(ldata(0)("timeout")), "0", ldata(0)("rowsperpage")))
                tbTimeout.Text = CInt(IIf(IsDBNull(ldata(0)("rowsperpage")), "0", ldata(0)("rowsperpage")))
                Return CInt(ldata(0)("pagetimeout").ToString.Trim)
            Else
                Return 15
            End If



        Catch ex As Exception
            msg.Text = "An error occurred while processing your information (" & ex.Message & ")."
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Function

    Private Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        Try



            Dim oset As New DocSetting
            If tbTimeout.Text.Trim() = "" Then
                Master.ShowMessage("Page Timeout is required. It should have a minimum value of 15 or greater.")
                Exit Sub
            End If
            If IsNumeric(tbTimeout.Text.Trim) = 0 Then
                Master.ShowMessage("Invalid Page Timeout. It should have a minimum value of 15 or greater.")
                Exit Sub
            End If
            If CInt(tbTimeout.Text.Trim) < 15 Then
                Master.ShowMessage("Page Time out should have a minimum value of 15 or greater.")
                Exit Sub
            End If
            If tbRows.Text.Trim() = "" Then
                Master.ShowMessage("Rows Per Page is required. It should have a value of 1 or greater.")
                Exit Sub
            End If
            If IsNumeric(tbRows.Text.Trim) = 0 Then
                Master.ShowMessage("Invalid Rows Per Page. It should be numeric.")
                Exit Sub
            End If
            If tbAdminEmail.Text.Trim() = "" Then
                Master.ShowMessage("Admin email is required.")
                Exit Sub
            End If
            oset.pPageTimeOut = tbTimeout.Text.Trim
            oset.pAdminEmail = tbAdminEmail.Text
            oset.pArchiveperson = tbArchivePerson.Text
            oset.pRowsPerPage = CInt(tbRows.Text.Trim)
            oset.pEmailPassword = tbEmailPassword.Text
            oset.pEmailUserName = tbEmailUserName.Text
            oset.pEmailPort = tbEmailPort.Text
            oset.pEmailHost = tbEmailHost.Text
            oset.pEnableSSL = cbEmailEnableSSL.Checked
            oset.pPurgeEmailRecipient = tbPurgePointPerson.Text
            DocSession.RowsPerPage = oset.pRowsPerPage
            DocSession.AdminEmail = oset.pAdminEmail
            DocSession.PageTimeOut = oset.pPageTimeOut
            oset.SaveSettings()
            Master.ShowMessage("Save successful. Please login to the system again to take effect the changes.")
        Catch ex As Exception
            Master.ShowMessage("Error while trying to save records (" & ex.Message & "). PLease try again.")
        End Try

    End Sub
End Class
