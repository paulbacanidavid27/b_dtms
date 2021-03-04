Public Class UserControlOfficeHoliday
    Inherits System.Web.UI.UserControl

    Public Property pYear As String
        Get
            Return txYear.Text
        End Get
        Set(ByVal value As String)
            txYear.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txYear.Text = CStr(Year(Date.Now))
            RetrieveOffice()
            RetrieveHoliday()

        End If
    End Sub

    Public ReadOnly Property pOfficeSelectedValue As String
        Get
            Return dlOffice.SelectedValue
        End Get
    End Property

    Public Sub RetrieveHoliday()
        Dim oHoliday As New DocHoliday
        oHoliday.pYear = CInt(txYear.Text)
        oHoliday.pOfficeId = dlOffice.SelectedValue
        Using lodata As DataTable = oHoliday.RetrieveOfficeHoliday

            rptHoliday.DataSource = lodata
            rptHoliday.DataBind()
            If lodata.Rows.Count = 0 Then
                lMsg.Text = "No records retrieved."
                lMsg.Visible = True
            Else
                lMsg.Visible = False
            End If
        End Using
        pnlHoliday.Update()
    End Sub

    Public Sub RetrieveOffice()
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



    Public Sub DeleteHoliday(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Dim oHoliday As New DocHoliday
        Dim ImgBtnSelected As ImageButton
        Dim lHoliday As Literal

        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer

        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)

            For Each ri In rptHoliday.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    lHoliday = DirectCast(ri.FindControl("lHoliday"), Literal)

                    If ImgBtnSelected.Visible Then
                        oHoliday.pHoliday = lHoliday.Text
                        oHoliday.pOfficeId = dlOffice.SelectedValue
                        oHoliday.DeleteOfficeHoliday(objCommand)
                        liCtr += 1
                    End If


                End If

            Next

            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            If liCtr >= 1 Then
                ltr.pTran.Commit()
                RetrieveHoliday()
            Else
                ltr.pTran.Rollback()
            End If

        Catch ex As Exception

            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If


        End Try
    End Sub

    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Private Sub txYear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txYear.TextChanged
        RetrieveHoliday()
        pnlHoliday.Update()
    End Sub



    Protected Sub dlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dlOffice.SelectedIndexChanged
        RetrieveHoliday()
        pnlHoliday.Update()
    End Sub

    Private Sub imgLeft_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLeft.Click
        txYear.Text = CStr(CInt(txYear.Text) - 1)
        RetrieveHoliday()
        pnlHoliday.Update()
    End Sub

    Private Sub imgRight_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRight.Click
        txYear.Text = CStr(CInt(txYear.Text) + 1)
        RetrieveHoliday()
        pnlHoliday.Update()
    End Sub
End Class