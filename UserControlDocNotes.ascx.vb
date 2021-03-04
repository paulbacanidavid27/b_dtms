Public Class UserControlDocNotes
    Inherits System.Web.UI.UserControl
    Public Event e_Count()
    Public ReadOnly Property pCount As String
        Get
            Return hfCount.value
        End Get
    End Property

    Public Sub RetrieveNotes()
        Dim oNotes As DocNotes
        Dim ldata As DataTable
        Try
            oNotes = New DocNotes
            ldata = oNotes.RetrieveDocNote(DocSession.sDocID)
            rptNotes.DataSource = ldata
            If DocSession.docDisable Then
                txtNotes.Visible = False
                lbSave.Visible = False
            Else
                txtNotes.Visible = True
                lbSave.Visible = True
            End If


            rptNotes.DataBind()
            pnlNotes.Update()

            If ldata.Rows.Count > 0 Then
                hfCount.Value = ldata.Rows.Count.ToString
            Else
                hfCount.Value = ""
            End If

            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub

    Public Sub CountNotes()
        Dim oNotes As DocNotes
        Dim lcnt As Integer
        Try
            oNotes = New DocNotes
            lcnt = oNotes.CountDocNote(DocSession.sDocID)
            If lcnt > 0 Then
                hfCount.Value = lcnt.ToString
            Else
                hfCount.Value = ""
            End If

            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Public Sub updateDocNotes(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)

        Try
            Dim lSend As Button = DirectCast(sender, Button)
            Dim ri As RepeaterItem = DirectCast(lSend.NamingContainer, RepeaterItem)

            Dim oNotes As New DocNotes

            oNotes.pDocId = DocSession.sDocID
            oNotes.pUserId = DocSession.sUserId
            oNotes.pIpAddress = Request.UserHostAddress
            oNotes.pNote = DirectCast(ri.FindControl("txtNotes"), TextBox).Text
            oNotes.pNoteId = DirectCast(ri.FindControl("lid"), Literal).Text
            oNotes.UpdateDocNotes()

            RetrieveNotes()
            pnlNotes.Update()
            txtNotes.Text = ""
            txtNotes.Focus()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub

    Private Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        If txtNotes.Text <> "Add New Note" Then
            Try
                Dim oNotes As New DocNotes

                oNotes.pDocId = DocSession.sDocID
                oNotes.pUserId = DocSession.sUserId
                oNotes.pIpAddress = Request.UserHostAddress
                oNotes.pNote = Left(txtNotes.Text.Trim, 1000)
                oNotes.pNoteId = DocSession.getNextID("doc_notes")
                oNotes.SaveDocNote()

                RetrieveNotes()
                pnlNotes.Update()
                txtNotes.Text = ""
                txtNotes.Focus()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If

    End Sub

    Public Sub DeleteNotes(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Dim oNotes As New DocNotes
        Dim ImgBtnSelected As ImageButton
        Dim lNotes, lNoteId As Literal

        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn
        Dim liCtr As Integer
        Dim lsNotes As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)
            For Each ri In rptNotes.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    lNoteId = DirectCast(ri.FindControl("lID"), Literal)
                    lNotes = DirectCast(ri.FindControl("lTag"), Literal)


                    If ImgBtnSelected.Visible Then
                        If lsNotes = "" Then
                            lsNotes = lNotes.Text
                            lsdesc = "note"
                        Else
                            lsNotes = lsNotes & ", " & lNotes.Text
                            lsdesc = "notes"
                        End If
                        oNotes.pDocId = DocSession.sDocID
                        oNotes.pIpAddress = Request.UserHostAddress
                        oNotes.pNoteId = lNoteId.Text
                        oNotes.pUserId = DocSession.sUserId
                        oNotes.DeleteDocNotes(objCommand)
                        liCtr += 1
                    End If


                End If

            Next

            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            If liCtr >= 1 Then

                Dim Ohist As New DocHistory
                Ohist.pTask = "Notes"
                Ohist.pAction = "Deleted " & lsdesc & " (" & lsNotes & ")"
                Ohist.pUserId = DocSession.sUserId
                Ohist.pIpAddress = Request.UserHostAddress
                Ohist.pDocId = DocSession.sDocID
                Ohist.AddHistory(objCommand)
                ltr.pTran.Commit()
                RetrieveNotes()
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

    Private Sub rptNotes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptNotes.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("imgDeleteNotes"), ImageButton).Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("txtNotes"), TextBox).Visible = False
                DirectCast(e.Item.FindControl("lTag"), Literal).Visible = True
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
                DirectCast(e.Item.FindControl("lbSave"), Button).Visible = False
            Else
                'If DocSession.sUserRole = "A" OrElse DocSession.sDeleteDoc = "1" OrElse DirectCast(e.Item.FindControl("lBY"), Literal).Text.Trim.ToLower = DocSession.sUserId.Trim.ToLower Then
                If DocSession.sUserRole = "A" OrElse DirectCast(e.Item.FindControl("lBY"), Literal).Text.Trim.ToLower = DocSession.sUserId.Trim.ToLower Then
                    DirectCast(e.Item.FindControl("txtNotes"), TextBox).Visible = True
                    DirectCast(e.Item.FindControl("lTag"), Literal).Visible = False
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
                    DirectCast(e.Item.FindControl("lbSave"), Button).Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CountNotes()
        End If
    End Sub
End Class
