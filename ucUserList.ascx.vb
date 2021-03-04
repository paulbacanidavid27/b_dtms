Public Class ucUserList
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public ReadOnly Property rptItems As Repeater
        Get
            Return rptCopy
        End Get
        
    End Property
    Private Sub imgCCSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCCSearch.Click
        Dim lodata As DataTable
        Try
            lCCSearchMsg.Visible = False
            Dim oDocs As New DocUser
            If dlCCSearchType.SelectedValue = "u" AndAlso txtCCBx.Text.Trim = "" Then
                lCCSearchMsg.Visible = True
                lCCSearchMsg.Text = "Please enter personnel name first before clicking Search icon."
                imgCloseSearch.Visible = True
            Else
                oDocs.pSearchString = txtCCBx.Text
                'oDocs.pDocType = pDocType2 'DocSession.sDocType
                oDocs.pUserId = DocSession.sUserId
                If dlCCSearchType.SelectedValue = "g" Then
                    oDocs.pGroup = dlCCGroups.SelectedValue

                    lodata = oDocs.RetrieveUserByGroup
                Else
                    lodata = oDocs.RetrieveUsers
                End If

                If lodata.Rows.Count > 0 Then
                    rptCCSub.DataSource = lodata
                    rptCCSub.DataBind()
                    rptCCSub.Visible = True
                    rptCCSub.Focus()
                    imgCloseSearch.Visible = True
                Else
                    rptCCSub.Visible = False

                    lCCSearchMsg.Visible = True
                    lCCSearchMsg.Text = "No records found."
                    imgCloseSearch.Visible = True
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If
        End Try

        pnl.Update()
    End Sub

    Private Function getGroup() As DataTable
        Dim ldata As DataTable
        Try


            Dim oType As New DocGroup
            If DocSession.sUserRole = "A" Then
                ldata = oType.RetrieveGroups
            Else
                oType.pGroupID = DocSession.sUserGroup
                ldata = oType.RetrieveGroups
            End If
            Return ldata
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Function
   
    Private Sub dlCCSearchType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlCCSearchType.SelectedIndexChanged

        lCCSearchMsg.Text = ""
        If dlCCSearchType.Text = "g" Then
            dlCCGroups.Visible = True
            dlCCGroups.DataSource = getGroup()
            dlCCGroups.DataValueField = "groupid"
            dlCCGroups.DataTextField = "groupname"
            dlCCGroups.DataBind()
            txtCCBx.Visible = False
        Else
            txtCCBx.Visible = True
            dlCCGroups.Visible = False
        End If

        pnl.Update()

    End Sub

    Public Sub fSelectCC(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("Approver", Type.GetType("System.String"))
            loData.Columns.Add("Email", Type.GetType("System.String"))
            'loData.Columns.Add("tbDue", Type.GetType("System.String"))
            loData.Columns.Add("tbRem", Type.GetType("System.String"))
            loRow = loData.NewRow()

            loRow("Email") = DirectCast(rpt.FindControl("lEmail"), Literal).Text
            loRow("UserId") = DirectCast(rpt.FindControl("lUserId"), Literal).Text
            loRow("Approver") = DirectCast(rpt.FindControl("lApprover"), Literal).Text
            'loRow("tbDue") = ""
            loRow("tbRem") = ""
            loData.Rows.Add(loRow)
            rpt.Visible = False


            CopyUser(loData)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try



    End Sub
    Private Sub CopyUser(ByVal loData As DataTable)
        Dim liCtr As Integer
        Dim imgBtnSelected As ImageButton
        Dim loRow As DataRow
        Try


            liCtr = 0
            For Each ri In rptCopy.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    imgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
                    If imgBtnSelected.Visible Then
                        If loData(0)("UserId") <> DirectCast(ri.FindControl("lUserId"), Literal).Text Then
                            loRow = loData.NewRow()
                            loRow("Email") = DirectCast(ri.FindControl("lEmail"), Literal).Text
                            loRow("UserId") = DirectCast(ri.FindControl("lUserId"), Literal).Text
                            loRow("Approver") = DirectCast(ri.FindControl("lApprover"), Literal).Text

                            loData.Rows.Add(loRow)
                            liCtr = liCtr + 1
                            'Exit For
                        End If

                    End If

                End If
            Next
            rptCopy.DataSource = loData
            rptCopy.DataBind()
            rptCopy.Visible = True
            pnl.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If
        End Try
    End Sub

    Public Sub fUnSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        'Dim loData As New DataTable("tblApprovers")
        'Dim loRow As DataRow
        'loData.Columns.Add("UserId", Type.GetType("System.String"))
        'loData.Columns.Add("Approver", Type.GetType("System.String"))
        'loData.Columns.Add("Email", Type.GetType("System.String"))
        'loRow = loData.NewRow()

        'loRow("Email") = DirectCast(rpt.FindControl("lEmail"), Literal).Text
        'loRow("UserId") = DirectCast(rpt.FindControl("lUserId"), Literal).Text
        'loRow("Approver") = DirectCast(rpt.FindControl("lApprover"), Literal).Text
        'loData.Rows.Add(loRow)
        rpt.Visible = False
        pnl.Update()

    End Sub

    Public Sub fSelectAll(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        'Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelectedh"), ImageButton)
        'lImg2.Visible = Not lImg2.Visible
        'lImg.Visible = Not lImg.Visible

        Dim loData As DataTable
        Dim loRow As DataRow
        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("Approver", Type.GetType("System.String"))
            loData.Columns.Add("Email", Type.GetType("System.String"))
            'loData.Columns.Add("tbDue", Type.GetType("System.String"))
            loData.Columns.Add("tbRem", Type.GetType("System.String"))

            For Each rptItems As RepeaterItem In rptCCSub.Items
                loRow = loData.NewRow()
                loRow("Email") = DirectCast(rptItems.FindControl("lEmail"), Literal).Text
                loRow("UserId") = DirectCast(rptItems.FindControl("lUserId"), Literal).Text
                loRow("Approver") = DirectCast(rptItems.FindControl("lApprover"), Literal).Text
                'loRow("tbDue") = ""
                loRow("tbRem") = ""
                loData.Rows.Add(loRow)
                rptItems.Visible = False
            Next

            CopyUser(loData)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
        pnl.Update()

    End Sub
    Public Sub fUnSelectAll(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg2 As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg As ImageButton = DirectCast(lImg2.Parent.FindControl("ImgSelecth"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        pnl.Update()

    End Sub

    Private Sub imgCloseSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseSearch.Click
        rptCCSub.Visible = False
        imgCloseSearch.Visible = False
        lCCSearchMsg.Visible = False

    End Sub
End Class