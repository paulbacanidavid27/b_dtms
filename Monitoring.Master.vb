Public Class Monitoring
    Inherits System.Web.UI.MasterPage
    Public Event e_AddMonitoring()
    Public Event e_SearchMonitoring()

    Public WriteOnly Property SetImage() As String
        Set(ByVal value As String)
            'imgDoc.ImageUrl = value
        End Set
    End Property

    'Public Property SetSearchCriteria() As String
    '    Get
    '        Return tbTopSearch.Text
    '    End Get
    '    Set(ByVal value As String)
    '        tbTopSearch.Text = value
    '    End Set

    'End Property

    'Public Property SetPartialSearch() As Boolean
    '    Get
    '        Return imgCbsy.Visible 'pPartial.Visible 'imgPartial.Visible
    '    End Get
    '    Set(ByVal value As Boolean)
    '        'imgPartial.Visible = value
    '        'pPartial.Visible = value
    '        imgCbsy.Visible = value
    '    End Set

    'End Property

    'Public Property SetExactSearch() As Boolean
    '    Get
    '        Return imgCbs.Visible 'pExact.Visible 'imgExact.Visible
    '    End Get
    '    Set(ByVal value As Boolean)
    '        'imgExact.Visible = value
    '        'pExact.Visible = value
    '        imgCbs.Visible = value
    '    End Set

    'End Property
    'Public Property SetDocId() As String
    '    Get
    '        Return lDocId.Text
    '    End Get
    '    Set(ByVal value As String)
    '        lDocId.Text = value
    '    End Set
    'End Property
    'Public Property SetDocType() As String
    '    Get
    '        Return lDocType.Text
    '    End Get
    '    Set(ByVal value As String)
    '        lDocType.Text = value
    '    End Set
    'End Property
    'Public WriteOnly Property SetTitle() As String
    '    Set(ByVal value As String)
    '        lTitle.Text = value
    '    End Set
    'End Property
    'Public WriteOnly Property SetAuthor() As String
    '    Set(ByVal value As String)
    '        lAuthor.Text = value
    '    End Set
    'End Property
    'Public WriteOnly Property SetCreatedDate() As String
    '    Set(ByVal value As String)
    '        lCreatedDate.Text = value
    '    End Set
    'End Property
    'Public WriteOnly Property SetSource() As String
    '    Set(ByVal value As String)
    '        pdfvwr.FilePath = value

    '    End Set
    'End Property
    Public WriteOnly Property ShowImageDocument() As Boolean
        Set(ByVal value As Boolean)
            cfBoxLayout.Visible = value
            cfBoxLayout2.Visible = value
            'RetrieveTags()
            'RetrieveLinks()
            'RetrieveDocIndex()
            'pnlView.Visible = value
            pnlView.Update()
            'pview.Visible = value
            'pnlId.Update()
        End Set
    End Property

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler prf.e_click, AddressOf CloseProfile
        setBlue()
        If DocSession.sUserRole = "B" Then
            Response.Redirect("home.aspx")
        End If
    End Sub

    Public Sub SelectTab(ByVal asTab As String)
        'If asTab = "Account" OrElse asTab = "Search" OrElse asTab = "Import" Then
        '    setGreen()
        'Else

        'End If
        'userControlTab1.pSelectedTab = asTab
        'userControlTab1.SelectTab()
    End Sub

    Private Sub CloseProfile()
        DivProfile1.Visible = False
        DivProfile2.Visible = False
    End Sub
    Private Sub MaintenanceMode()
        Dim soffline As String = tlusd.Value 'System.Configuration.ConfigurationManager.AppSettings("OfflineDate").Trim
        If soffline <> "" Then
            If IsDate(soffline) Then

                Dim lisec As Integer = DateDiff(DateInterval.Second, DateTime.Now, CDate(soffline))
                If lisec > 0 Then
                    lSM.Text = soffline
                    tlusd.Value = lisec.ToString
                Else
                    Response.Redirect("maintenance.aspx")
                End If
            End If
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Context.Session Is Nothing Then
            If Session.IsNewSession Then
                Dim lsCook As String = Request.Headers("cookie")
                If (Not lsCook Is Nothing AndAlso lsCook.IndexOf("ASP.NET_SessionId") >= 0) Then

                    Response.Redirect("Login.aspx")
                End If
            End If
        End If
        If DocSession.sUserId Is Nothing OrElse DocSession.sUserId = "" Then
            Response.Redirect("Login.aspx")
        Else

            If Not IsPostBack Then
                imgProfile.ImageUrl = "images/avatar/" & DocSession.sprofilePic
                imgProfile.DataBind()
                'imgLogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "logo.png", "otherlogo.png")
                'imgLogo.DataBind()
                'If DocSession.GroupLogo = "" Then
                '    imgCoLogo.Visible = False
                'Else
                '    imgCoLogo.Visible = True
                '    imgCoLogo.ImageUrl = "images/logo/" & DocSession.GroupLogo
                'End If

                Dim lsTO As String = DocSession.PageTimeOut
                If DocSession.sUserLogin <> "admin" Then
                    MaintenanceMode()
                End If

                If lsTO.Trim <> "" AndAlso IsNumeric(lsTO) Then
                        lo_time_left_register.Value = CInt(lsTO) * 60
                    Else
                        lo_time_left_register.Value = 1800
                    End If

                    Dim lsDate As String
                    lsDate = DateTime.Now.ToShortDateString
                    lDateNow.Text = DateTime.Now.ToLongDateString
                    'If Not Session("username") Is Nothing Then
                    'If Session("username") <> "" Then
                    'lUserInfo.Text = Session("username")
                    If Not DocSession.sUserName Is Nothing Then
                        If DocSession.sUserName <> "" Then
                            lUserInfo.Text = DocSession.sUserName
                            lUserInfo.Visible = True
                            'pSearch.Visible = True
                            pUser.Visible = True
                        Else
                            lUserInfo.Text = DocSession.sUserName
                            lUserInfo.Visible = False
                            'pSearch.Visible = False
                            pUser.Visible = False
                        End If
                    End If

                    'If DocSession.sUserRole <> "A" Then
                    '    'pAdminMenu.Visible = False
                    '    mAdmin.Visible = False
                    'End If

                    'If DocSession.sReportAccess <> "1" Then
                    '    'pReport.Visible = False
                    '    mReport.Visible = False
                    'End If


                    'pnlLogin.Update()
                    If DocSession.sFirstTimeUser = "True" Then
                        showProf()
                    End If

                    If DocSession.SearchOption = "E" Then
                        'imgPartial.Visible = True
                        'imgExact.Visible = False
                        'pPartial.Visible = True
                        'pExact.Visible = False
                        'SetExactSearch = True
                        'searchoption.Update()
                    Else
                        'SetPartialSearch = True
                        'imgPartial.Visible = False
                        'imgExact.Visible = True
                        'pPartial.Visible = False
                        'pExact.Visible = True
                        'searchoption.Update()
                    End If
                    'If DocSession.SearchCriteria <> "" Then
                    '    tbTopSearch.Text = DocSession.SearchCriteria
                    'End If

                    'ReadVersion()

                End If
            End If
    End Sub
    'Private Sub ReadVersion()
    '    Dim lsPath As String = Server.MapPath(".")
    '    Using sr As System.IO.StreamReader = New System.IO.StreamReader(lsPath & "\version.txt")
    '        Dim line = sr.ReadToEnd
    '        If DocSession.sUserRole = "A" Then
    '            imgLogo.OnClientClick = "window.open('updates.html');"
    '        End If

    '        imgLogo.ToolTip = "Version " & line
    '    End Using

    'End Sub
    'Public Sub RetrieveTags()
    '    Dim oTags As New DocTags
    '    rptTags.DataSource = oTags.RetrieveDocTag(SetDocId)
    '    rptTags.DataBind()
    '    pnlTags.Update()
    'End Sub

    'Public Sub RetrieveLinks()
    '    Dim oLinks As New DocLinks
    '    rptLinks.DataSource = oLinks.RetrieveDocLinks(SetDocId)
    '    rptLinks.DataBind()
    '    pnlLinks.Update()
    'End Sub

    'Public Sub RetrieveDocIndex()
    '    Dim oIndex As New DocIndex
    '    oIndex.pDocId = SetDocId
    '    oIndex.pDocType = SetDocType

    '    rptIndex.DataSource = oIndex.RetrieveDocIndex()
    '    rptIndex.DataBind()
    '    pnlIndex.Update()
    'End Sub

    'Public Sub SaveDocIndexValues()

    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim lColId As Literal
    '    Dim lColValue As TextBox
    '    Dim oConn As New SqlClient.SqlConnection(str)
    '    Dim ltr As SqlClient.SqlTransaction
    '    Dim objCommand As New SqlClient.SqlCommand
    '    Dim liCtr As Integer
    '    Dim oIndex As New DocIndex

    '    liCtr = 0
    '    Try
    '        oConn.Open()
    '        ltr = oConn.BeginTransaction
    '        objCommand.Transaction = ltr
    '        objCommand.Connection = oConn
    '        For Each ri In rptIndex.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                lColId = DirectCast(ri.FindControl("lColId"), Literal)

    '                lColValue = DirectCast(ri.FindControl("lColValue"), TextBox)

    '                oIndex.pDocId = lDocId.Text
    '                oIndex.pDocType = lDocType.Text
    '                oIndex.pColId = lColId.Text
    '                oIndex.pColValue = lColValue.Text
    '                oIndex.SaveDocIndexValues(objCommand)
    '                liCtr = 1
    '            End If

    '        Next


    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '        End If

    '    Catch ex As Exception

    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If


    '    End Try
    'End Sub

    'Protected Sub imgLogout_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogout.Click
    '    Session("userid") = ""
    '    Session("username") = ""
    '    Session("email") = ""
    '    Session("UserRole") = ""
    '    Session("UserGroup") = ""

    '    Response.Redirect("default.aspx")

    'End Sub

    'Protected Sub imgUserInfo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUserInfo.Click
    '    pLogin.Visible = Not pLogin.Visible
    '    pnlLogin.Update()
    'End Sub

    'Protected Sub Logout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Logout.Click
    '    'Session("username") = ""
    '    DocSession.sUserId = ""
    '    DocSession.sUserName = ""
    '    Response.Redirect("default.aspx")
    'End Sub

    'Private Sub xClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles xClose.Click
    '    cfBoxLayout.Visible = False
    '    cfBoxLayout2.Visible = False
    '    pnlView.Update()
    'End Sub

    'Private Sub lbnLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbnLink.Click
    '    Dim loLink As DocLinks
    '    loLink = New DocLinks
    '    rptList.DataSource = loLink.RetrieveDocLinks(lDocId.Text)
    '    rptList.DataBind()
    '    rptList.Visible = Not rptList.Visible
    '    rptUpdate.update()

    'End Sub

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

    'Private Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
    '    'Dim txtBox As TextBox = DirectCast(sender, TextBox)
    '    Dim oTag As New DocTags
    '    oTag.pDocId = CInt(lDocId.Text)
    '    oTag.pIpAddress = Request.UserHostAddress
    '    oTag.pTag = txtTags.Text
    '    oTag.pUserId = DocSession.sUserId 'Session("userid")
    '    oTag.SaveDocTag()
    '    RetrieveTags()
    '    pnlTags.Update()
    '    txtTags.Text = ""
    '    txtTags.Focus()
    'End Sub

    'Public Sub DeleteTags(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    'Dim txtBox As TextBox = DirectCast(sender, TextBox)
    '    Dim oTag As New DocTags
    '    Dim ImgBtnSelected As ImageButton
    '    Dim lTags As Literal
    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim oConn As New SqlClient.SqlConnection(str)
    '    Dim ltr As SqlClient.SqlTransaction
    '    Dim objCommand As New SqlClient.SqlCommand
    '    Dim liCtr As Integer

    '    liCtr = 0
    '    Try
    '        oConn.Open()
    '        ltr = oConn.BeginTransaction
    '        objCommand.Transaction = ltr
    '        objCommand.Connection = oConn
    '        For Each ri In rptTags.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '                ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '                lTags = DirectCast(ri.FindControl("lTag"), Literal)

    '                If ImgBtnSelected.Visible Then
    '                    oTag.pDocId = CInt(lDocId.Text)
    '                    oTag.pIpAddress = Request.UserHostAddress
    '                    oTag.pTag = lTags.Text
    '                    oTag.pUserId = DocSession.sUserId ' Session("userid")
    '                    oTag.DeleteDocTags(objCommand)
    '                    liCtr += 1
    '                End If


    '            End If

    '        Next

    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '            RetrieveTags()

    '        End If

    '    Catch ex As Exception

    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If


    '    End Try
    'End Sub

    'Private Sub btSaveIndex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveIndex.Click
    '    SaveDocIndexValues()
    'End Sub

    'Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
    '    Dim oDocs As New DocList
    '    oDocs.pDocTitle = txtLinks.Text
    '    rptDocList.DataSource = oDocs.RetrieveDocs()
    '    rptDocList.DataBind()
    '    rptDocList.Visible = True
    '    pnlLinks.Update()
    'End Sub

    'Public Sub DeleteLinks(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    Dim ImgBtnSelected As ImageButton
    '    Dim oDocLinks As New DocLinks
    '    Dim lnkId As Literal
    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim oConn As New SqlClient.SqlConnection(str)
    '    Dim ltr As SqlClient.SqlTransaction
    '    Dim objCommand As New SqlClient.SqlCommand
    '    Dim liCtr As Integer

    '    liCtr = 0
    '    Try
    '        oConn.Open()
    '        ltr = oConn.BeginTransaction
    '        objCommand.Transaction = ltr
    '        objCommand.Connection = oConn
    '        For Each ri In rptLinks.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '                ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '                lnkId = DirectCast(ri.FindControl("lLinkDocId"), Literal)

    '                If ImgBtnSelected.Visible Then
    '                    oDocLinks.pDocId = lDocId.Text
    '                    oDocLinks.pLinkDocId = lnkId.Text
    '                    oDocLinks.pUserId = DocSession.sUserId 'Session("userid")
    '                    oDocLinks.pIpAddress = Request.UserHostAddress
    '                    oDocLinks.DeleteDocLinks(objCommand)
    '                    liCtr += 1
    '                End If


    '            End If

    '        Next

    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '            rptDocList.Visible = False
    '            RetrieveLinks()

    '        End If

    '    Catch ex As Exception

    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If


    '    End Try
    'End Sub

    'Private Sub btSaveLinks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSaveLinks.Click
    '    Dim ImgBtnSelected As ImageButton
    '    Dim oDocLinks As New DocLinks
    '    Dim lnkId As Literal
    '    Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
    '    Dim oConn As New SqlClient.SqlConnection(str)
    '    Dim ltr As SqlClient.SqlTransaction
    '    Dim objCommand As New SqlClient.SqlCommand
    '    Dim liCtr As Integer

    '    liCtr = 0
    '    Try
    '        oConn.Open()
    '        ltr = oConn.BeginTransaction
    '        objCommand.Transaction = ltr
    '        objCommand.Connection = oConn
    '        For Each ri In rptDocList.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then

    '                'ImgBtn = DirectCast(ri.FindControl("imgSelect"), ImageButton)
    '                ImgBtnSelected = DirectCast(ri.FindControl("imgSelected"), ImageButton)
    '                lnkId = DirectCast(ri.FindControl("lnkDocId"), Literal)

    '                If ImgBtnSelected.Visible Then
    '                    oDocLinks.pDocId = lDocId.Text
    '                    oDocLinks.pLinkDocId = lnkId.Text
    '                    oDocLinks.pUserId = DocSession.sUserId 'Session("userid")
    '                    oDocLinks.pIpAddress = Request.UserHostAddress
    '                    oDocLinks.SaveDocLinks(objCommand)
    '                    liCtr += 1
    '                End If


    '            End If

    '        Next

    '        'msg.Text = "Document " & tbDocTitle.Text & " has been created"
    '        If liCtr >= 1 Then
    '            ltr.pTran.Commit()
    '            rptDocList.Visible = False
    '            RetrieveLinks()

    '        End If

    '    Catch ex As Exception

    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not oConn Is Nothing Then
    '            oConn.Close()
    '            oConn.Dispose()
    '            oConn = Nothing
    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If


    '    End Try
    'End Sub

    'Protected Sub lbnIndex_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lbnIndex.Click
    '    pdfvwr.FilePath = "d:\SAMPLE.PDF"
    '    pnlId.Update()
    'End Sub
    Private Sub SetSearchParam(ByVal asval As String)
        Dim mycookie As HttpCookie = New HttpCookie("srchParam")
        mycookie.Value = asval
        mycookie.Expires = DateTime.Now.AddDays(30)
        Response.Cookies.Add(mycookie)
    End Sub
    'Protected Sub imgTopSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgTopSearch.Click
    '    If tbTopSearch.Text.Trim = "" OrElse tbTopSearch.Text.Trim = "Enter search criteria here ..." Then
    '        imgTopSearch.ToolTip = "Please provide a search criteria before clicking on search button..."
    '        ShowMessage("Please provide a search criteria before clicking on search button...")
    '        'searchoption.Update()
    '    Else
    '        RaiseEvent e_SearchMonitoring()
    '    End If

    'End Sub

    'Private Sub lnkAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdmin.Click
    '    pAdminMenu.Visible = Not pAdminMenu.Visible
    '    pnlAdminMenu.Update()
    'End Sub

    'Private Sub lnkDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocuments.Click
    '    Response.Redirect("documents.aspx")
    'End Sub

    'Private Sub lnkHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHome.Click
    '    Response.Redirect("home.aspx")
    'End Sub

    'Private Sub imgSearchAdv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearchAdv.Click
    '    Response.Redirect("search.aspx?as=1")
    'End Sub

    'Private Sub lnkBookmark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBookmark.Click
    '    Response.Redirect("bookmark.aspx")
    'End Sub

    'Private Sub lbProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbProfile.Click
    '    showProf()
    'End Sub

    Private Sub imgProfile_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProfile.Click
        showProf()
    End Sub

    Public Sub showProf(Optional ByVal asuserid As String = "")
        DivProfile1.Visible = True
        DivProfile2.Visible = True
        prf.RetrieveUserProfile(asuserid)
    End Sub

    'Private Sub imgPartial_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPartial.Click
    '    'imgPartial.Visible = False
    '    'imgExact.Visible = True
    '    SetExactSearch = True
    '    DocSession.SearchOption = "E"
    '    searchoption.Update()

    'End Sub

    'Private Sub imgExact_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExact.Click
    '    imgPartial.Visible = True
    '    imgExact.Visible = False
    '    DocSession.SearchOption = "P"
    '    searchoption.Update()

    'End Sub

    'Private Sub imgCbsy_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCbsy.Click
    '    'imgPartial.Visible = False
    '    'imgExact.Visible = True
    '    SetExactSearch = True
    '    SetPartialSearch = False
    '    DocSession.SearchOption = "E"
    '    searchoption.Update()

    'End Sub

    'Private Sub imgCbs_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCbs.Click
    '    'imgPartial.Visible = True
    '    'imgExact.Visible = False
    '    SetPartialSearch = True
    '    SetExactSearch = False
    '    DocSession.SearchOption = "P"
    '    searchoption.Update()

    'End Sub



    Private Sub btLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btLogin.Click
        Response.Redirect("default.aspx")
    End Sub

    Public Sub ShowMessage(ByVal asMessage As String)
        lmsgtxt.Text = asMessage
        pMsgDiv.Style("visibility") = "visible"
        pMsgDiv.Visible = True

        pnlMessage.Update()
    End Sub
    Public Sub setGreen()
        matt.Style("background-color") = "#D3F6CE"
    End Sub
    Public Sub setBlue(Optional ByVal asUrl As String = "")
        If asUrl = "" Then
            If Not Request.Cookies("docMonBackgroundImage") Is Nothing Then
                matt.Style("background-image") = Server.HtmlEncode(Request.Cookies("docMonBackgroundImage").Value)
                'matt.Style("background-repeat") = 
            Else
            End If
        Else
            matt.Style("background-image") = asUrl

        End If

    End Sub
    Public Sub HideMessage()

        pMsgDiv.Visible = False
        pnlMessage.Update()

    End Sub

    Private Sub imgCloseWindow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseWindow.Click
        pMsgDiv.Visible = False
        pnlMessage.Update()
    End Sub

    Public Sub AddHistory()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim ohist As DocHistory

        Try
            ohist = New DocHistory

            ohist.pDocId = "0"
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Logout from " & Request.UserHostAddress
            ohist.pTask = "Logout"
            ohist.AddHistory()

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Private Sub imgLogoff_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogoff.Click
        AddHistory()
        DocSession.sUserId = ""
        DocSession.sUserName = ""
        Session.Abandon()

        Response.Redirect("default.aspx")
    End Sub

    'Private Sub lbExact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbExact.Click
    '    'pPartial.Visible = True
    '    'pExact.Visible = False
    '    SetPartialSearch = False
    '    SetExactSearch = True
    '    DocSession.SearchOption = "E"
    '    searchoption.Update()
    'End Sub

    'Private Sub lbPartial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPartial.Click
    '    'pPartial.Visible = False
    '    'pExact.Visible = True
    '    SetPartialSearch = True
    '    SetExactSearch = False
    '    DocSession.SearchOption = "P"
    '    searchoption.Update()
    'End Sub


    Private Sub imgActivities_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgActivities.Click
        Response.Redirect("DashboardList.aspx")
    End Sub

    Private Sub hlHelp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlHelp.Load
        Dim lsLink As String = System.IO.Path.GetFileName(Request.PhysicalPath)

        hlHelp.NavigateUrl = lsLink.Replace(System.IO.Path.GetExtension(Request.PhysicalPath), ".html")
    End Sub

    Private Sub imgBookmark_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBookmark.Click
        Response.Redirect("bookmark.aspx")
    End Sub

    'Private Sub imgTopAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgTopAdd.Click
    '    RaiseEvent e_AddMonitoring()
    'End Sub

    Private Sub imgHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHome.Click
        Response.Redirect("home.aspx")
    End Sub
End Class