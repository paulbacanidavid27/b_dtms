Public Class UserControlAddOfficeHoliday
    Inherits System.Web.UI.UserControl

    Public Event e_click()

    Public ReadOnly Property pYear As String
        Get
            If IsDate(txHoliday.Text) Then
                Return Year(CDate(txHoliday.Text)).ToString
            Else
                Return Year(Date.Now).ToString
            End If

        End Get


    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveGroups()
        End If
    End Sub
    Private Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Try
            lMsg.Visible = False

            If txHoliday.Text <> "Add New Holiday" Then
                Dim oHoliday As New DocHoliday
                oHoliday.pHoliday = txHoliday.Text
                oHoliday.pOfficeId = dlOffice.SelectedValue
                If oHoliday.CheckIfOfficeHolidayExists() Then
                    lMsg.Visible = True
                    lMsg.Text = "Holiday already exists."
                    'pnlHoliday.Update()
                Else


                    oHoliday.pYear = Year(CDate(txHoliday.Text))
                    oHoliday.pDescription = txDesc.Text
                    oHoliday.pUserId = DocSession.sUserId

                    oHoliday.SaveOfficeHoliday()
                    'RetrieveHoliday()
                    'txHoliday.Focus()
                End If
                RaiseEvent e_click()
            End If
        Catch ex As Exception
            lMsg.Visible = True
            lMsg.Text = "An error occured while saving ( " & ex.Message & " ). Please try again"
        End Try
    End Sub

    Private Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        Me.Visible = False
        'pnlHoliday.Update()
    End Sub

    Public Sub RetrieveGroups()
        Dim oGrp As New DocGroup
        If DocSession.sUserRole = "D" OrElse DocSession.sUserRole = "G" Then
            oGrp.pOfficeCode = DocSession.sOfcCode

        Else

        End If

        Using lodata As DataTable = oGrp.RetrieveOffice

            dlOffice.DataSource = lodata
            dlOffice.DataValueField = "OfficeCode"
            dlOffice.DataTextField = "Description"
            dlOffice.DataBind()
        End Using

    End Sub
End Class