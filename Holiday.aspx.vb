Public Class Holiday
    Inherits System.Web.UI.Page

    Private Sub Holiday_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucAddHoliday.e_click, AddressOf ufRefresh
        AddHandler ucNewHoliday.e_click, AddressOf AddHoliday
        AddHandler ucCopyHoliday.e_click, AddressOf CopyHoliday
        Master.SelectTab("Account")
        ucMenu.SelectTab("Holiday")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub imgCopyHoliday_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCopyHoliday.Click
        Master.ShowImageDocument = True
        txYear.Text = Year(Date.Now).ToString
        RetrieveGroups()
        pCopyGroup.Visible = Not pCopyGroup.Visible
        pnlCopy.Update()
    End Sub

    Public Sub RetrieveGroups()
        Dim oGrp As New DocGroup

        Using lodata As DataTable = oGrp.RetrieveGroups

            dlGroup1.DataSource = lodata
            dlGroup1.DataValueField = "groupid"
            dlGroup1.DataTextField = "groupname"
            dlGroup1.DataBind()
            dlGroup1.SelectedValue = ucHoliday.pGroupSelectedValue

            dlGroup2.DataSource = lodata
            dlGroup2.DataValueField = "groupid"
            dlGroup2.DataTextField = "groupname"
            dlGroup2.DataBind()
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
        If dlGroup1.SelectedValue <> dlGroup2.SelectedValue Then
            oHoliday = New DocHoliday
            oHoliday.pGroupId = dlGroup1.SelectedValue
            oHoliday.pCopyGroupId = dlGroup2.SelectedValue

            oHoliday.pUserId = DocSession.sUserId
            oHoliday.pYear = txYear.Text
                oHoliday.CopyHoliday()
                msg.CssClass = "msg_green"
                msg.Text = "Holidays successfully copied."
        Else
            msg.CssClass = "msg_red"
            msg.Text = "You cannot copy the same group. Please select a different group to proceed."
        End If
        Catch ex As Exception
            msg.CssClass = "msg_red"
            msg.Text = "An error occurred while copying holidays ( " & ex.Message & " ). Please try again."

        End Try

    End Sub

    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        pCopyGroup.Visible = False
        pnlCopy.Update()
        Master.ShowImageDocument = False
    End Sub

    Private Sub imgAddHoliday_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddHoliday.Click
        ucAddHoliday.Visible = True

    End Sub

    Private Sub AddHoliday()
        ucAddHoliday.Visible = True
    End Sub

    Private Sub CopyHoliday()
        Master.ShowImageDocument = True
        txYear.Text = Year(Date.Now).ToString
        RetrieveGroups()
        pCopyGroup.Visible = Not pCopyGroup.Visible
        pnlCopy.Update()
    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        pCopyGroup.Visible = False
        pnlCopy.Update()
        Master.ShowImageDocument = False
    End Sub
End Class