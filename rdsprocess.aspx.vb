Imports System
Imports System.Data.SqlClient
Public Class rdsprocess
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Account")
        UserControlAdminMenuH1.SelectTab("Group")
        AddHandler ucAdd.e_click, AddressOf AddReceipt
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()

        RetrieveReceipts()
        pnlRepeater.Update()

    End Sub
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub
    Private Sub imgUser_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUser.Click
        pFilter.Visible = Not pFilter.Visible
        If InStr(imgUser.ImageUrl, "show") > 0 Then
            imgUser.ImageUrl = "images/hidepanel.png"
        Else
            imgUser.ImageUrl = "images/showpanel.png"
        End If
        pnlFilter.Update()
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        If Not IsPostBack Then
            RetrieveReceipts()
            ShowResult()
        End If
    End Sub


    Private Sub RetrieveReceipts()
        Dim ldata As DataTable

        Try
            Dim oDoc As New docPurge
            oDoc.pProcessDate = tbProcessDateStart.Text.Trim
            oDoc.pIdx = hfCurrent.Value
            oDoc.pRowsPerPage = DocSession.RowsPerPage
            oDoc.pSortCol = hfSortCol.Value
            oDoc.pSortOrder = hfSortOrder.Value
            ldata = oDoc.RetrieveRecordsByPage

            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = oDoc.CountDocPurge

                hfTotalRows.Value = lstotalrows
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))
                pPager.Update()

                Repeater1.DataSource = ldata
                Repeater1.DataBind()
            Else
                Master.ShowMessage("No records found for the selected filter.")
                Repeater1.DataSource = ldata
                Repeater1.DataBind()
                pPager.Update()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If

        End Try

    End Sub



    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click

        RetrieveReceipts()
        ShowResult()

    End Sub



    


    

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        Dim imgUpd As ImageButton
        'If e.Item.ItemType = ListItemType.Header Then
        '    imgDel = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
        '    AddHandler imgDel.Click, AddressOf DeleteGroup

        'End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            imgUpd = DirectCast(e.Item.FindControl("imgUpdate"), ImageButton)
            AddHandler imgUpd.Click, AddressOf UpdateReceipt

        End If
    End Sub
    Private Sub UpdateReceipt(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim imgB As ImageButton = DirectCast(sender, ImageButton)
        Dim ri As RepeaterItem = DirectCast(imgB.NamingContainer, RepeaterItem)
        Dim lsi As Literal = DirectCast(ri.FindControl("lReceiptID"), Literal)
        Dim ltldn As Literal = DirectCast(ri.FindControl("lReceiptDesc"), Literal)

        Dim oGroup As New DocGroup

        lAction.Text = "Update"

        pnlAddReceipt.Update()
        oGroup.pStatusId = Server.HtmlDecode(lsi.Text)
        'Dim ldata As DataTable = oGroup.RetrieveDocStatusGroupByID

        'cbGroups.DataSource = ldata
        'cbGroups.DataTextField = "groupName"
        'cbGroups.DataValueField = "groupId"
        'cbGroups.DataBind()

        'For i = 0 To cbGroups.Items.Count - 1
        '    If ldata.Rows(i).Item("StatusID") <> "0" Then
        '        cbGroups.Items(i).Selected = True
        '    End If
        'Next

        tbDesc.Text = Server.HtmlDecode(ltldn.Text)
        ltReceiptId.Text = lsi.Text

        msg.Text = ""

        Master.ShowImageDocument = True
        pAddReceipt.Visible = True

    End Sub

   

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
    End Sub

    
    Private Sub ShowCriteria()
        'pSearchCriteria.Visible = Not pSearchCriteria.Visible
        'pnlSearchCriteria.Update()
        Repeater1.Visible = Not Repeater1.Visible
        pnlRepeater.Update()

        pAddReceipt.Visible = False
        pnlAddReceipt.Update()
        'pDeleteReceipt.Visible = Not pDeleteReceipt.Visible
        'pnlDeleteReceipt.Update()
    End Sub


    Private Sub ShowResult()

        ' pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()


        pAddReceipt.Visible = False
        pnlAddReceipt.Update()

        'pDeleteReceipt.Visible = False
        'pnlDeleteReceipt.Update()

        pRepeater.Visible = True
        pnlRepeater.Update()

        'imgAddGroup.Visible = True
        Master.ShowImageDocument = False
        'pnlTab.Update()
    End Sub

   

    

    Private Sub ShowAdd()


        msg.Text = ""

        pAddReceipt.Visible = Not pAddReceipt.Visible

        pnlAddReceipt.Update()
        Master.ShowImageDocument = True


    End Sub



    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ShowResult()
    End Sub



    Private Sub ShowReceiptDetails(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim oAccess As New DocGroupAccess
        'Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        'Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        'Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelected"), ImageButton)
        ''Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        ''Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        ''ImgNA.Visible = True
        ''ImgA.Visible = False
        'ImgS.Visible = False
        'Img.Visible = True
        'Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        'If rbList.SelectedValue = "" Then
        '    rbList.SelectedValue = "0"
        '    rbList.DataSource = oAccess.RetrieveGroupAccess()
        '    rbList.DataTextField = "GroupAccess"
        '    rbList.DataValueField = "GroupAccessId"
        'End If
        'rbList.DataBind()
        'rbList.Visible = True
        'Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)

        'updPnl.Visible = True
        'updPnl.Update()
        ''pnlGAccess.Visible = True
        ''pnlGAccess.Update
        ''Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        ''updPnlCbox.Update()

    End Sub
    Private Sub HideReceiptDetails(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Dim oAccess As New DocGroupAccess
        'Dim ImgS As ImageButton = DirectCast(sender, ImageButton)
        'Dim ri As RepeaterItem = DirectCast(ImgS.NamingContainer, RepeaterItem)
        'Dim Img As ImageButton = DirectCast(ri.FindControl("ImgSelect"), ImageButton)
        ''Dim ImgA As ImageButton = DirectCast(ri.FindControl("ImgAllow"), ImageButton)
        ''Dim ImgNA As ImageButton = DirectCast(ri.FindControl("ImgNotAllow"), ImageButton)
        ''ImgNA.Visible = False
        ''ImgA.Visible = False

        'ImgS.Visible = False
        'Img.Visible = True

        'Dim rbList As RadioButtonList = DirectCast(ri.FindControl("rbGroupAccess"), RadioButtonList)
        'Dim updPnl As UpdatePanel = DirectCast(ri.FindControl("pnlGAccess"), UpdatePanel)
        'updPnl.Visible = False
        'rbList.Visible = False
        'updPnl.Update()

        ''Dim updPnlCbox As UpdatePanel = DirectCast(ri.FindControl("pnlCbox"), UpdatePanel)
        ''updPnlCbox.Update()

    End Sub

    Public Sub fSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgVersionAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgVersionNotAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fSelect3(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgDownload"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect3(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotDownload"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fSelect4(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgReceipt"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect4(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotReceipt"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub


    Public Sub fSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub

    Public Sub fUnSelect(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgNotAllow"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible
    End Sub



    


    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image

        If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort2
            imgSort2.Visible = True
        Else
            imgSort2.Visible = False
        End If


        Dim oDocGroup As New DocGroup

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                oDocGroup.pSortOrder = "Desc"
                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                oDocGroup.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            oDocGroup.pSortOrder = "Asc"
            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If
        'oDocGroup.pGroupID = ""
        'oDocGroup.pDesc = ""

        oDocGroup.pSortCol = lbSort.Text
        hfSortCol.Value = lbSort.Text
        RetAction()

        'Repeater1.DataSource = oDocGroup.RetrieveGroupNew
        'Repeater1.DataBind()
        'pnlRepeater.Update()


    End Sub

    

    Public Sub imgShowGroupAccess(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim odoc As New DocGroup
        Dim oImg As ImageButton = DirectCast(sender, ImageButton)

        Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)

        Dim img As ImageButton = DirectCast(oItem.FindControl("imgMinus"), ImageButton)
        img.Visible = Not img.Visible
        Dim img2 As ImageButton = DirectCast(oItem.FindControl("imgPlus"), ImageButton)
        img2.Visible = Not img2.Visible
        Dim rptGA As Repeater = DirectCast(oItem.FindControl("rptGroupAccess"), Repeater)
        Dim lsi As Literal = DirectCast(oItem.FindControl("lStatusID"), Literal)
        rptGA.Visible = Not rptGA.Visible
        AddHandler rptGA.ItemDataBound, AddressOf ufDatabound
        If rptGA.Visible Then
            odoc.pStatusId = lsi.Text
            rptGA.DataSource = odoc.RetrieveDocStatusGroup
            rptGA.DataBind()
        End If

    End Sub
    Public Sub ufDatabound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        'If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
        If e.Item.ItemType = ListItemType.Header Then
            AddHandler DirectCast(e.Item.FindControl("imgDelete"), ImageButton).Click, AddressOf ufDeleteGroups
        End If
    End Sub
    Public Sub ufDeleteGroupStatus(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim osql As clsSqlConn
        Dim otran As DbTran
        Try
            otran = New DbTran
            osql = New clsSqlConn(otran.pTran)
            Dim rpt As Repeater = DirectCast(DirectCast(DirectCast(sender, ImageButton).NamingContainer, RepeaterItem).NamingContainer, Repeater)
            Dim rpti As RepeaterItem = DirectCast(rpt.NamingContainer, RepeaterItem)
            Dim lsi As Literal = DirectCast(rpti.FindControl("lStatusID"), Literal)

            For Each rptitem In rpt.Items
                If DirectCast(rptitem.FindControl("cbxDelete"), CheckBox).Checked Then

                    DeleteGroupAccess(osql, lsi.Text, DirectCast(rptitem.FindControl("lgroupid"), Literal).Text)
                End If
            Next
            otran.pTran.Commit()
            Dim pga As UpdatePanel = DirectCast(rpti.FindControl("pga"), UpdatePanel)
            pga.Update()
            Dim odoc As New DocGroup
            odoc.pStatusId = lsi.Text
            rpt.DataSource = odoc.RetrieveDocStatusGroup
            rpt.DataBind()
        Catch ex As Exception
            If Not otran Is Nothing Then
                otran.pTran.Rollback()
            End If

        Finally
            If Not osql Is Nothing Then
                osql.Dispose()
            End If
            If Not otran Is Nothing Then
                otran.Dispose()
            End If
        End Try


    End Sub


    Private Sub DeleteGroupAccess(ByVal osql As clsSqlConn, ByVal statusid As String, ByVal groupid As String)
        Dim ogrp As New DocGroup
        ogrp.pStatusId = statusid
        ogrp.pGroupID = groupid
        ogrp.DeleteGroupStatus(osql)
    End Sub

    Public Sub ufDeleteGroups(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        Dim osql As clsSqlConn
        Dim otran As DbTran
        Try
            otran = New DbTran
            osql = New clsSqlConn(otran.pTran)
            Dim rpt As Repeater = DirectCast(DirectCast(sender, ImageButton).NamingContainer, Repeater)
            Dim rpti As RepeaterItem = DirectCast(rpt.NamingContainer, RepeaterItem)
            Dim lsi As Literal = DirectCast(rpti.FindControl("lStatusID"), Literal)

            For Each rptitem In rpt.Items
                If DirectCast(rptitem.FindControl("cbDelete"), CheckBox).Checked Then

                    DeleteGroupAccess(osql, lsi.Text, DirectCast(rptitem.FindControl("lGroupId"), Literal).Text)
                End If
            Next
            otran.pTran.Commit()
        Catch ex As Exception
            otran.pTran.Rollback()
        Finally
            If Not osql Is Nothing Then
                osql.Dispose()
            End If
            If Not otran Is Nothing Then
                otran.Dispose()
            End If
        End Try


    End Sub
    Private Sub AddReceipt()

        tbDesc.Text = ""
        lAction.Text = "Add"
        ShowAdd()

    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        ShowResult()
    End Sub

End Class