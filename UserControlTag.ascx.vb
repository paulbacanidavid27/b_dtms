Public Class UserControlTag
    Inherits System.Web.UI.UserControl
#Region "Code for displaying error message in Master page"
    Dim smsg As String
    Public Event e_ShowMessage()
    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property
    Private Sub ErrorMsg(ByVal asMsg As String)
        Message = asMsg
        RaiseEvent e_ShowMessage()
    End Sub
#End Region
    Dim lsTitle As String

    Public Property pTitle As String
        Get
            Return lsTitle
        End Get
        Set(ByVal value As String)
            lsTitle = value
        End Set
    End Property

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
    Public Sub DeleteTags(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Dim oTag As New DocTags
        Dim ImgBtnSelected As ImageButton
        Dim lTags As Literal
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        'Dim ltr As SqlClient.SqlTransaction
        Dim ltr As New DbTran
        Dim objCommand As clsSqlConn

        Dim liCtr As Integer
        Dim lsTags As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            objCommand = New clsSqlConn(ltr.pTran)


            'objCommand.pCommandText = "xMSP_DOCTAGSDELETE"
            For Each ri In rptTags.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    lTags = DirectCast(ri.FindControl("lTag"), Literal)
                    
                    If ImgBtnSelected.Visible Then
                        If lsTags = "" Then
                            lsTags = lTags.Text
                            lsdesc = "tag"
                        Else
                            lsTags = lsTags & ", " & lTags.Text
                            lsdesc = "tags"
                        End If
                        oTag.pDocId = CInt(DocSession.sDocID)
                        oTag.pIpAddress = Request.UserHostAddress
                        oTag.pTag = lTags.Text
                        oTag.pUserId = DocSession.sUserId
                        oTag.DeleteDocTags(objCommand)
                        liCtr += 1
                    End If


                End If

            Next


            'msg.Text = "Document " & tbDocTitle.Text & " has been created"
            If liCtr >= 1 Then
                Dim Ohist As New DocHistory
                Ohist.pTask = "Tag"
                Ohist.pAction = "Deleted " & lsdesc & " (" & lsTags & ")"
                Ohist.pUserId = DocSession.sUserId
                Ohist.pIpAddress = Request.UserHostAddress
                Ohist.pDocId = DocSession.sDocID
                Ohist.AddHistory(objCommand)

                ltr.pTran.Commit()
                RetrieveTags()

            Else
                ErrorMsg("Please select a tag before clicking delete button.")
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
    Public Sub RetrieveTags()
        Dim oTags As New DocTags
        rptTags.DataSource = oTags.RetrieveDocTag(DocSession.sDocID)
        rptTags.DataBind()
        'pDocTags.Update()
        'If DocSession.docDisable Then
        '    txtTags.Visible = False  'need to check again
        '    lbSave.Visible = False   'need to check again
        'Else
        txtTags.Visible = True 'need to check again
        lbSave.Visible = True  'need to check again
        'End If
        pnlTag.Update()
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
                oTag.pDocName = pTitle
                oTag.SaveDocTag()
                'lcheckmsg.Text = "Tag has been saved successfully."
                'lcheckmsg.CssClass = "msg_green"
                RetrieveTags()
                'pDocTags.Update() 'need to check again
                txtTags.Text = ""
                txtTags.Focus()
                pnlTag.Update()
            End If

        Catch ex As Exception
            lcheckmsg.Text = "There's an error while saving the record ( " & ex.Message & " ) . Please try again"
            lcheckmsg.CssClass = "msg_red"
            pnlTag.Update()
        End Try

    End Sub

    Private Sub rptTags_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTags.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("imgDeleteTags"), ImageButton).Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            If DocSession.docDisable Then
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
            Else
                'If DocSession.sUserRole = "A" OrElse DocSession.sDeleteDoc = "1" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
                If DocSession.sUserRole = "A" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
                End If
            End If

        End If


    End Sub
End Class