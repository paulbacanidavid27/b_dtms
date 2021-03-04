Public Class OfficeHoliday
    Inherits System.Web.UI.Page

    Private Sub Holiday_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucAddHoliday.e_click, AddressOf ufRefresh
        'AddHandler ucNewHoliday.e_click, AddressOf AddHoliday
        'AddHandler ucCopyHoliday.e_click, AddressOf CopyHoliday
        Master.SelectTab("Holiday")
        'ucMenu.SelectTab("Holiday")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsSuperUser()
        If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
            ucMenu.Visible = False
            'btImportHoliday.Visible = True
            lbImportHoliday.Visible = True
            lbCopy.Visible = False
            imgCpyHoliday.Visible = False
            'btCopyHoliday.Visible = False
        Else
            'btImportHoliday.Visible = True
            'btCopyHoliday.Visible = True
            lbImportHoliday.Visible = True
            lbCopy.Visible = True
            imgCpyHoliday.Visible = True
        End If
    End Sub

    Private Sub imgCopyHoliday_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCopyHoliday.Click
        Master.ShowImageDocument = True
        txYear.Text = Year(Date.Now).ToString
        RetrieveOffice()
        pCopyOffice.Visible = Not pCopyOffice.Visible
        pnlCopy.Update()
    End Sub

    Public Sub RetrieveOffice()
        Dim oOfc As New DocGroup

        Using lodata As DataTable = oOfc.RetrieveOffice

            dlOffice1.DataSource = lodata
            dlOffice1.DataValueField = "OfficeCode"
            dlOffice1.DataTextField = "Description"
            dlOffice1.DataBind()
            dlOffice2.DataSource = lodata
            dlOffice2.DataValueField = "OfficeCode"
            dlOffice2.DataTextField = "Description"
            dlOffice2.DataBind()
        End Using
    End Sub
    Private Sub ufRefresh()
        ucHoliday.pYear = ucAddHoliday.pYear
        ucHoliday.RetrieveHoliday()
    End Sub

    Private Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        Dim oHoliday As DocHoliday
        Try


            If Not IsNumeric(txYear.Text) Then
                msg.CssClass = "msg_red"
                msg.Text = "Please enter a valid year."
                Exit Sub

            End If
            If dlOffice1.SelectedValue <> dlOffice2.SelectedValue Then
                oHoliday = New DocHoliday
                oHoliday.pOfficeId = dlOffice1.SelectedValue
                oHoliday.pCopyOfficeId = dlOffice2.SelectedValue

                oHoliday.pUserId = DocSession.sUserId
                oHoliday.pYear = txYear.Text
                oHoliday.CopyOfficeHoliday()
                msg.CssClass = "msg_green"
                msg.Text = "Holidays successfully copied."
            Else
                'msg.CssClass = "msg_red"
                Master.ShowMessage("You cannot copy the same office. Please select a different office to proceed.")
            End If
        Catch ex As Exception
            'msg.CssClass = "msg_red"
            Master.ShowMessage("An error occurred while copying holidays ( " & ex.Message & " ). Please try again.")

        End Try

    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pCopyOffice.Visible = False
        pnlCopy.Update()
        Master.ShowImageDocument = False
    End Sub
    Private Sub imgImportClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseImport.Click
        pImport.Visible = False
        pnlImport.Update()
        Master.ShowImageDocument = False
    End Sub
    'Private Sub imgAddHoliday_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddHoliday.Click
    '    ucAddHoliday.Visible = True

    'End Sub

    Private Sub AddHoliday()
        ucAddHoliday.Visible = True
        pnlHoliday.Update()
    End Sub

    Private Sub CopyHoliday()
        Master.ShowImageDocument = True
        txYear.Text = Year(Date.Now).ToString
        RetrieveOffice()
        pCopyOffice.Visible = Not pCopyOffice.Visible
        pnlCopy.Update()
    End Sub

    Private Sub ImportHoliday()
        Master.ShowImageDocument = True
        tbCopyToYear.Text = Year(Date.Now) + 1.ToString
        tbCopyFromYear.Text = Year(Date.Now).ToString
        'RetrieveOffice()
        pImport.Visible = Not pImport.Visible
        pnlImport.Update()
    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        pCopyOffice.Visible = False
        pnlCopy.Update()
        Master.ShowImageDocument = False
    End Sub

    'Private Sub btAddNewHoliday_Click(sender As Object, e As EventArgs) Handles btAddNewHoliday.Click
    '    AddHoliday()
    'End Sub

    'Private Sub btCopyHoliday_Click(sender As Object, e As EventArgs) Handles btCopyHoliday.Click
    '    CopyHoliday()
    'End Sub

    'Private Sub btImportHoliday_Click(sender As Object, e As EventArgs) Handles btImportHoliday.Click
    '    ImportHoliday()
    'End Sub

    Private Sub btCloseImport_Click(sender As Object, e As EventArgs) Handles btCloseImport.Click
        pImport.Visible = False
        pnlImport.Update()
        Master.ShowImageDocument = False
    End Sub

    Private Sub btImportSave_Click(sender As Object, e As EventArgs) Handles btImportSave.Click
        Dim oHoliday As DocHoliday
        Try


            If Not IsNumeric(tbCopyFromYear.Text) OrElse Not IsNumeric(tbCopyToYear.Text) Then
                'msg.CssClass = "msg_red"
                Master.ShowMessage("Please enter a valid year.")
                Exit Sub

            End If
            If 1 = 1 Then
                oHoliday = New DocHoliday

                oHoliday.pOfficeId = ucHoliday.pOfficeSelectedValue
                oHoliday.pCopyOfficeId = ucHoliday.pOfficeSelectedValue

                oHoliday.pUserId = DocSession.sUserId
                oHoliday.pYear = tbCopyToYear.Text
                oHoliday.CopyFromHoliday()
                'msg.CssClass = "msg_green"
                Master.ShowMessage("Holidays successfully copied.")
            Else
                'msg.CssClass = "msg_red"
                Master.ShowMessage("You cannot copy the same office. Please select a different office to proceed.")
            End If
        Catch ex As Exception
            'msg.CssClass = "msg_red"
            Master.ShowMessage("An error occurred while copying holidays ( " & ex.Message & " ). Please try again.")

        End Try
    End Sub

    Private Sub tbCopyToYear_TextChanged(sender As Object, e As EventArgs) Handles tbCopyToYear.TextChanged
        If IsNumeric(tbCopyToYear.Text) Then
            tbCopyFromYear.Text = CInt(tbCopyToYear.Text) - 1
        Else
        End If

    End Sub

    Private Sub lbAddHoliday_Click(sender As Object, e As EventArgs) Handles lbAddHoliday.Click
        AddHoliday()
    End Sub

    Private Sub lbImportHoliday_Click(sender As Object, e As EventArgs) Handles lbImportHoliday.Click
        ImportHoliday()
    End Sub

    Private Sub lbCopy_Click(sender As Object, e As EventArgs) Handles lbCopy.Click
        CopyHoliday()
    End Sub

    Private Sub imgCpyHoliday_Click(sender As Object, e As ImageClickEventArgs) Handles imgCpyHoliday.Click
        CopyHoliday()
    End Sub

    Private Sub imgImportHoliday_Click(sender As Object, e As ImageClickEventArgs) Handles imgImportHoliday.Click
        ImportHoliday()
    End Sub

    Private Sub imgAddHoliday_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddHoliday.Click
        AddHoliday()
    End Sub
End Class