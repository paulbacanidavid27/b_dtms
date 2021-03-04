Imports System
Imports System.Data.SqlClient
Public Class DashboardList
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'pager: step 4
        AddHandler ucAddDoc.e_click, AddressOf AddDoc
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucUpload.e_ShowMessage, AddressOf ShowUploadMessage

        Master.SelectTab("Activities")
    End Sub

    Private Sub ShowUploadMessage()
        Master.ShowMessage(ucUpload.Message)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ShowAll()
            RetAction()
            If DocSession.sDocAccess > 2 Then
                ucAddDoc.Visible = True
            Else
                ucAddDoc.Visible = False
            End If

        End If
    End Sub

    Private Sub ShowAll()
        If DocSession.sUserRole = "A" Then
            lAll.Visible = True
            'cbAll.Visible = True
            dlUser.Visible = True
            tbIPAdd.Visible = True
            lIP.Visible = True
            GetUsers()
        Else
            lAll.Visible = False
            'cbAll.Visible = False
            dlUser.Visible = False
            tbIPAdd.Visible = False
            lIP.Visible = False
        End If
    End Sub

    Private Sub AddDoc()
        ucUpload.ShowAdd()
        ucUpload.Visible = True
    End Sub
    Public Sub RetAction()
        ucDB.pIdx = CInt(hfCurrent.Value)
        ucDB.pTask = dlActivities.SelectedValue

        ucDB.pADate = txDateActivity.Text
        ucDB.pTDate = txDateActivityTo.Text
        ucDB.pIPAddress = tbIPAdd.Text
        ucDB.pSortCol = dlColumns.SelectedValue
        ucDB.pSortOrder = dlSortOption.SelectedValue
        If dlUser.Visible Then

            ucDB.RetrieveAction(dlUser.SelectedValue)
        Else
            ucDB.RetrieveAction(DocSession.sUserId)
        End If

        ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        hfTotalRows.Value = CStr(ucDB.pRetVal)
        upDB.Update()
        upPgr.Update()

    End Sub

    'pager: step 3
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

    Private Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        If (txDateActivity.Text.Trim <> "" AndAlso Not IsDate(txDateActivity.Text)) Then
            Master.ShowMessage("Please enter a valid activity date. Format should be mm/dd/yyyy.")
            txDateActivity.Focus()
        ElseIf (txDateActivityTo.Text.Trim <> "" AndAlso Not IsDate(txDateActivityTo.Text)) Then
            Master.ShowMessage("Please enter a valid activity date. Format should be mm/dd/yyyy.")
            txDateActivityTo.Focus()
        Else
            RetAction()
        End If

    End Sub

    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        pFilter.Visible = Not pFilter.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If
        pnlFilter.Update()
    End Sub

    Private Sub GetUsers()

        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oUser As DocUser
        Try
            oUser = New DocUser

            ldata = oUser.UserList

            lrow = ldata.NewRow
            lrow(0) = ""
            lrow(1) = "-All-"
            ldata.Rows.InsertAt(lrow, 0)
            dlUser.DataSource = ldata
            dlUser.DataValueField = "UserId"
            dlUser.DataTextField = "UserName"
            dlUser.DataBind()
            Try
                dlUser.SelectedValue = DocSession.sUserId
            Catch ex As Exception

            End Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub
End Class