Public Class UserControlFolder
    Inherits System.Web.UI.UserControl

#Region "Code for displaying error message in Master page"
    Dim smsg As String
    Public Event e_ShowMessage()
    Public Event e_LinkButton()
    Public Sub ShowInbox(ByVal sender As Object, ByVal e As System.EventArgs)

        DocSession.sFolderID = ""
        DocSession.sFolderDesc = ""
        'DirectCast(ri.FindControl("Image2"), Image).ImageUrl = "images/fOpen.png"
        RaiseEvent e_LinkButton()
    End Sub
    Public Sub SearchDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Try
            ri = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
            DocSession.sFolderID = DirectCast(ri.FindControl("lFolderId"), Literal).Text
            DocSession.sFolderDesc = DirectCast(ri.FindControl("lFolder"), Literal).Text
            RaiseEvent e_LinkButton()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                'ri.Dispose()
            End If
        End Try
        
    End Sub
    Public Sub UpdateFolder(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Try
            ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
            If DirectCast(ri.FindControl("tbFolderName"), TextBox).Text <> "Inbox" Then
                DirectCast(ri.FindControl("tbFolderName"), TextBox).Visible = True
                DirectCast(ri.FindControl("tbFolderName"), TextBox).Focus()
                DirectCast(ri.FindControl("lbFolder"), LinkButton).Visible = False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                'ri.Dispose()
            End If
        End Try
        


    End Sub

    Public Sub SaveChanges(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Dim oList As DocList
        Try
            ri = DirectCast(DirectCast(sender, TextBox).NamingContainer, RepeaterItem)

            DirectCast(ri.FindControl("lFolder"), Literal).Text = DirectCast(sender, TextBox).Text
            DirectCast(ri.FindControl("lbFolder"), LinkButton).Visible = True
            oList = New DocList
            oList.pUserId = DocSession.sUserId
            oList.pFolderId = DirectCast(ri.FindControl("lFolderId"), Literal).Text
            oList.pFolderDesc = DirectCast(sender, TextBox).Text
            DirectCast(sender, TextBox).Visible = False
            oList.UpdateUserFolder()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If
        End Try
        
    End Sub
    Public Sub AddFolder()
        FolderAdd.Visible = Not FolderAdd.Visible
        pnlFolder.Update()
    End Sub
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

    Public Sub DeleteFolders(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim oList As New DocList
        Dim ImgBtnSelected As ImageButton
        Dim lFold As Literal
        
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn

        Dim liCtr As Integer
        Dim lsFolders As String = ""
        Dim lsdesc As String = ""
        liCtr = 0
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            For Each ri In rptFolder.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

                    'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
                    ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    lFold = DirectCast(ri.FindControl("lFolder"), Literal)

                    If ImgBtnSelected.Visible Then
                        If lsFolders = "" Then
                            lsFolders = lFold.Text
                            lsdesc = "folder"
                        Else
                            lsFolders = lsFolders & ", " & lFold.Text
                            lsdesc = "folders"
                        End If
                        oList.pUserId = DocSession.sUserId
                        oList.pFolderId = DirectCast(ri.FindControl("lFolderId"), Literal).Text
                        oList.DeleteUserFolders(objCommand)
                        liCtr += 1
                    End If


                End If

            Next

            If liCtr >= 1 Then
                'Dim Ohist As New DocHistory
                'Ohist.pTask = "Folder"
                'Ohist.pAction = "Deleted " & lsdesc & " (" & lsFolders & ")"
                'Ohist.pUserId = DocSession.sUserId
                'Ohist.pIpAddress = Request.UserHostAddress
                'Ohist.pDocId = "" 'DocSession.sDocID
                'Ohist.AddHistory(objCommand)

                ltr.pTran.Commit()
                RetrieveUserFolders()
                ErrorMsg("Folder has been deleted.")
            Else
                ErrorMsg("Please select a folder before clicking delete button.")
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

    Public Sub RetrieveUserFolders()
        Dim oList As New DocList
        oList.pUserId = DocSession.sUserId
        Using ldata As DataTable = oList.RetrieveUserFolder()
            Dim lrow As DataRow
            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "Inbox"
            lrow(2) = "0"
            ldata.Rows.InsertAt(lrow, 0)
            rptFolder.DataSource = ldata
            rptFolder.DataBind()

            txtFolder.Visible = True 'need to check again
            lbSave.Visible = True  'need to check again
            'End If

        End Using
        pnlFolder.Update()
    End Sub

    Private Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        'Dim txtBox As TextBox = DirectCast(sender, TextBox)
        Try


            If txtFolder.Text <> "Add New Folder" Then
                Dim oList As New DocList
                oList.pUserId = DocSession.sUserId
                oList.pFolderId = DocSession.getNextID("folderid")
                oList.pFolderDesc = txtFolder.Text
                oList.SaveUserFolder()
                'lcheckmsg.Text = "Tag has been saved successfully."
                'lcheckmsg.CssClass = "msg_green"
                RetrieveUserFolders()
                ErrorMsg("Folder has been saved successfully.")
                txtFolder.Text = ""
                txtFolder.Focus()
                pnlFolder.Update()

            End If

        Catch ex As Exception
            ErrorMsg("There's an error while saving the record ( " & ex.Message & " ) . Please try again")
            pnlFolder.Update()
        End Try

    End Sub

    'Private Sub rptFolder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFolder.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Header Then
    '        If DocSession.docDisable Then
    '            DirectCast(e.Item.FindControl("imgDeleteTags"), ImageButton).Visible = False
    '        End If
    '    ElseIf e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
    '        If DocSession.docDisable Then
    '            DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
    '        Else
    '            If DocSession.sUserRole = "A" OrElse DocSession.sDeleteDoc = "1" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
    '                If DocSession.sUserRole = "A" OrElse DocSession.sUserId = DirectCast(e.Item.FindControl("lTB"), Literal).Text Then
    '                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
    '                Else
    '                    DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = False
    '                End If
    '            End If

    '    End If


    'End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveUserFolders()
        End If
    End Sub

    Private Sub rptFolder_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFolder.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            
            'If DirectCast(e.Item.FindControl("lFolderId"), Literal).Text.Trim <> "" Then
            AddHandler DirectCast(e.Item.FindControl("Image2"), ImageButton).Click, AddressOf UpdateFolder
            'End If

        End If
    End Sub

    Private Sub rptFolder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFolder.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lFolderId"), Literal).Text.Trim = DocSession.sFolderID.Trim Then
                DirectCast(e.Item.FindControl("Image2"), ImageButton).ImageUrl = "images/fopen.png"
            End If

            
            If CInt(DirectCast(e.Item.FindControl("lcnt"), Literal).Text) = 1 Then
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = "(" & DirectCast(e.Item.FindControl("lcnt"), Literal).Text & " document)"
            ElseIf CInt(DirectCast(e.Item.FindControl("lcnt"), Literal).Text) > 1 Then
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = "(" & DirectCast(e.Item.FindControl("lcnt"), Literal).Text & " documents)"
            Else
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = ""
                DirectCast(e.Item.FindControl("imgSelect"), ImageButton).Visible = True
            End If

        End If
    End Sub
End Class