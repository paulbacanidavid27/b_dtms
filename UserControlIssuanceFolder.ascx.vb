﻿Public Class UserControlIssuanceFolder
    Inherits System.Web.UI.UserControl

#Region "Code for displaying error message in Master page"
    Dim smsg As String
    Public Event e_ShowMessage()
    Public Event e_LinkButton()
    Public Property pFolderId() As String
        Get
            Return hfFolderId.Value
        End Get
        Set(ByVal value As String)
            hfFolderId.Value = value
        End Set

    End Property
    Public Property pFolderDesc() As String
        Get
            Return hfFolderDesc.Value
        End Get
        Set(ByVal value As String)
            hfFolderDesc.Value = value
        End Set

    End Property

    Public Sub ShowMain()

        hfFolderId.Value = ""
        hfFolderDesc.Value = ""
        'DirectCast(ri.FindControl("Image2"), Image).ImageUrl = "images/fOpen.png"
        RaiseEvent e_LinkButton()
    End Sub

    Public Sub SearchDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Try
            ri = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
            hfFolderId.Value = DirectCast(ri.FindControl("lFolderId"), Literal).Text
            hfFolderDesc.Value = DirectCast(ri.FindControl("lFolder"), Literal).Text

            RaiseEvent e_LinkButton()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ri Is Nothing Then
                ri.Dispose()
            End If
        End Try

    End Sub

    Public Sub UpdateFolder(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem
        Try
            ri = DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem)
            If DirectCast(ri.FindControl("tbFolderName"), TextBox).Text <> "New" Then
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
            oList.UpdateIssuanceFolder()
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
                    'ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    lFold = DirectCast(ri.FindControl("lFolder"), Literal)

                    'If ImgBtnSelected.Visible Then
                    If DirectCast(ri.FindControl("cbDelete"), CheckBox).Checked Then
                        If lsFolders = "" Then
                            lsFolders = lFold.Text
                            lsdesc = "folder"
                        Else
                            lsFolders = lsFolders & ", " & lFold.Text
                            lsdesc = "folders"
                        End If
                        oList.pUserId = DocSession.sUserId
                        oList.pFolderId = DirectCast(ri.FindControl("lFolderId"), Literal).Text
                        oList.DeleteIssuanceFolders(objCommand)
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
                RetrieveIssuanceFolders()
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

    Public Sub RetrieveIssuanceFolders()
        Dim oList As New DocList
        oList.pUserId = DocSession.sUserId
        Using ldata As DataTable = oList.RetrieveIssuanceFolder()
            If DocSession.sUserRole = "A" Then
                Dim lrow As DataRow
                lrow = ldata.NewRow
                lrow(0) = ""
                lrow(1) = "New"
                lrow(2) = "0"
                ldata.Rows.InsertAt(lrow, 0)
            Else
                If ldata.Rows.Count > 0 Then
                    hfFolderId.Value = ldata(0)("folderid")
                    hfFolderDesc.Value = ldata(0)("folderDesc")
                End If
            End If

            
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
                oList.SaveIssuanceFolder()
                'lcheckmsg.Text = "Tag has been saved successfully."
                'lcheckmsg.CssClass = "msg_green"
                RetrieveIssuanceFolders()
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

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RetrieveIssuanceFolders()
            
        End If
    End Sub

    Private Sub rptFolder_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFolder.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            If DocSession.sUserRole = "A" Then
                DirectCast(e.Item.FindControl("imgDelFolder"), ImageButton).Visible = True
                DirectCast(e.Item.FindControl("imgAddFolder"), ImageButton).Visible = True
            Else
                DirectCast(e.Item.FindControl("imgDelFolder"), ImageButton).Visible = False
                DirectCast(e.Item.FindControl("imgAddFolder"), ImageButton).Visible = False
            End If

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            'If DirectCast(e.Item.FindControl("lFolderId"), Literal).Text.Trim <> "" Then
            AddHandler DirectCast(e.Item.FindControl("Image2"), ImageButton).Click, AddressOf UpdateFolder
            'End If

        End If
    End Sub

    Private Sub rptFolder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptFolder.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("lFolderId"), Literal).Text.Trim = hfFolderId.Value.Trim Then
                DirectCast(e.Item.FindControl("Image2"), ImageButton).ImageUrl = "images/fopen.png"
            End If

            If CInt(DirectCast(e.Item.FindControl("lcnt"), Literal).Text) = 1 Then
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = "(" & DirectCast(e.Item.FindControl("lcnt"), Literal).Text & " document)"
            ElseIf CInt(DirectCast(e.Item.FindControl("lcnt"), Literal).Text) > 1 Then
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = "(" & DirectCast(e.Item.FindControl("lcnt"), Literal).Text & " documents)"
            Else
                If DirectCast(e.Item.FindControl("lFolder"), Literal).Text.Trim = "New" Then
                    DirectCast(e.Item.FindControl("cbDelete"), CheckBox).Visible = False
                Else
                    DirectCast(e.Item.FindControl("cbDelete"), CheckBox).Visible = True
                End If
                DirectCast(e.Item.FindControl("lCnt"), Literal).Text = ""

            End If

        End If
    End Sub
End Class