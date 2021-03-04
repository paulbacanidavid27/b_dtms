Imports System
Imports System.Data.SqlClient
Public Class _user
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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
        End If

        Master.SelectTab("Account")
        UserControlAdminMenuH1.SelectTab("User")
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        AddHandler ucMerge.e_click, AddressOf MergeUsers
    End Sub
    Dim DelDate As String
    Private Property pDelDate As String
        Get
            Return DelDate
        End Get
        Set(ByVal value As String)
            DelDate = value
        End Set
    End Property
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveUsers()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
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
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        If Not IsPostBack Then
            Using ldata As DataTable = RetrieveGroups()
                ddlGroup.DataTextField = "groupname"
                ddlGroup.DataValueField = "groupid"
                ddlGroup.DataSource = ldata
                ddlGroup.DataBind()
                ddlGroupAdd.DataTextField = "groupname"
                ddlGroupAdd.DataValueField = "groupid"
                ddlGroupAdd.DataSource = ldata
                ddlGroupAdd.DataBind()
            End Using
            RetrieveUsers()
            ShowResult()


            'regKey.Text = DocSession.sRegKey  ---for debugging purposes only
        End If
    End Sub

    Function RetrieveGroups() As DataTable
        

        'Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim lrow As DataRow
        Try
            'objCommand = New clsSqlConn
            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERGETGROUPS"
            Dim oGrp As New DocGroup
            ldata = oGrp.RetrieveGroups
            lrow = ldata.NewRow
            lrow(0) = ""
            ldata.Rows.InsertAt(lrow, 0)
            Return ldata

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

    End Function

    Private Sub RetrieveUsers()

        
        Dim ldata As DataTable
        Try
            
            Dim odoc As New DocUser
            odoc.pUserLogin = UserLogin.Text.Trim
            odoc.pFirstName = FirstName.Text.Trim
            odoc.pLastName = LastName.Text.Trim
            odoc.pUserEmail = txEmail.Text.Trim
            odoc.pGroup = ddlGroup.SelectedValue
            odoc.pRole = ddlRole.SelectedValue
            odoc.pIdx = hfCurrent.Value 'DocSession.doc_DocCurrentPage
            odoc.pRowsPerPage = DocSession.RowsPerPage

            odoc.pSortOrder = hfSortOrder.Value
            odoc.pSortCol = hfSortCol.Value
            ldata = odoc.RetrieveUser
            If ldata.Rows.Count > 0 Then

                If ldata.Rows.Count > DocSession.RowsPerPage Then

                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)

                Else

                End If

                Dim lstotalrows As String = odoc.CountUser

                hfTotalRows.Value = lstotalrows 'oDocs.pRetVal
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
        End Try

    End Sub

    'Function retrievedata() As DataTable

    '    Dim loData As DataTable
    '    Dim loRow As DataRow
    '    Try
    '        loData = New DataTable("tblUsers")
    '        loData.Columns.Add("UserName", Type.GetType("System.String"))
    '        loData.Columns.Add("FirstName", Type.GetType("System.String"))
    '        loData.Columns.Add("LastName", Type.GetType("System.String"))
    '        loData.Columns.Add("Group", Type.GetType("System.String"))

    '        loRow = loData.NewRow()

    '        loRow("UserName") = "MOriarte"
    '        loRow("FirstName") = "Michael"
    '        loRow("LastName") = "Oriarte"
    '        loRow("Group") = "Manager"

    '        loData.Rows.Add(loRow)
    '        loRow = loData.NewRow()
    '        loRow("UserName") = "GSotelo"
    '        loRow("FirstName") = "George"
    '        loRow("LastName") = "Sotelo"
    '        loRow("Group") = "Admin"

    '        loData.Rows.Add(loRow)
    '        loRow = loData.NewRow()
    '        loRow("UserName") = "JDoe"
    '        loRow("FirstName") = "John"
    '        loRow("LastName") = "Doe"
    '        loRow("Group") = "User"
    '        loData.Rows.Add(loRow)
    '        Return loData

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not loData Is Nothing Then
    '            loData.Dispose()
    '            loData = Nothing
    '        End If

    '    End Try
    'End Function

    'Protected Sub imgAddUser_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAddUser.Click
    '    ShowAdd()
    'End Sub

    Protected Sub imgPlus(ByVal sender As Object, ByVal e As System.EventArgs)

        Using oImg As ImageButton = DirectCast(sender, ImageButton)

            Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)
            Dim lbl As Literal = DirectCast(oItem.FindControl("FirstName"), Literal)
            Dim img As ImageButton = DirectCast(oItem.FindControl("imgOpen"), ImageButton)
            img.Visible = Not img.Visible
            Dim img2 As ImageButton = DirectCast(oItem.FindControl("imgClose"), ImageButton)
            img2.Visible = Not img2.Visible
            Dim pnl As Panel = DirectCast(oItem.FindControl("userDetails"), Panel)
            pnl.Visible = Not pnl.Visible
            lbl.Visible = Not lbl.Visible

            lbl = DirectCast(oItem.FindControl("LastName"), Literal)
            lbl.Visible = Not lbl.Visible

            lbl = DirectCast(oItem.FindControl("UserLogin"), Literal)
            lbl.Visible = Not lbl.Visible

            'lbl = DirectCast(oItem.FindControl("MiddleName"), Literal)
            'lbl.Visible = Not lbl.Visible

            lbl = DirectCast(oItem.FindControl("Role"), Literal)
            lbl.Visible = Not lbl.Visible

            lbl = DirectCast(oItem.FindControl("lRole"), Literal)


            Dim ddl As DropDownList = DirectCast(oItem.FindControl("ddlRoleEdit"), DropDownList)
            'ddl.SelectedItem.Text = lbl.Text
            ddl.Visible = Not ddl.Visible
            ddl.SelectedValue = lbl.Text

            lbl = DirectCast(oItem.FindControl("Group"), Literal)
            lbl.Visible = Not lbl.Visible

            lbl = DirectCast(oItem.FindControl("lGroup"), Literal)


            ddl = DirectCast(oItem.FindControl("ddlGroupEdit"), DropDownList)
            ddl.DataSource = RetrieveGroups()
            ddl.DataTextField = "groupname"
            ddl.DataValueField = "groupid"
            ddl.DataBind()
            ddl.SelectedValue = lbl.Text
            ddl.Visible = Not ddl.Visible

            'ddl.DataSource = RetrieveGroups()
            'ddl.DataBind()
            'ddl.DataValueField = ""

            lbl = DirectCast(oItem.FindControl("PassWordExpiration"), Literal)
            lbl.Visible = False

            lbl = DirectCast(oItem.FindControl("Lockout"), Literal)
            lbl.Visible = Not lbl.Visible

            ddl = DirectCast(oItem.FindControl("ddlLockout"), DropDownList)
            ddl.SelectedValue = lbl.Text
            ddl.Visible = True

            lbl = DirectCast(oItem.FindControl("Title"), Literal)
            lbl.Visible = False

            lbl = DirectCast(oItem.FindControl("CanChange"), Literal)
            lbl.Visible = False

            Dim cbx As CheckBox = DirectCast(oItem.FindControl("cbCanChange"), CheckBox)
            cbx.Checked = IIf(lbl.Text.ToLower = "yes", True, False)
            cbx.Visible = True

            lbl = DirectCast(oItem.FindControl("Locked"), Literal)
            lbl.Visible = False

            cbx = DirectCast(oItem.FindControl("cbLocked"), CheckBox)
            cbx.Visible = True
            cbx.Checked = IIf(lbl.Text.ToLower = "yes", True, False)

            lbl = DirectCast(oItem.FindControl("lActive"), Literal)
            lbl.Visible = False

            cbx = DirectCast(oItem.FindControl("cbActive"), CheckBox)
            cbx.Visible = True
            cbx.Checked = IIf(lbl.Text.Trim.ToLower <> "", False, True)

            lbl = DirectCast(oItem.FindControl("Email"), Literal)
            lbl.Visible = Not lbl.Visible

            Dim txt As TextBox = DirectCast(oItem.FindControl("tbFirstName"), TextBox)
            txt.Visible = Not txt.Visible

            txt = DirectCast(oItem.FindControl("tbLastName"), TextBox)
            txt.Visible = Not txt.Visible

            txt = DirectCast(oItem.FindControl("tbUserLogin"), TextBox)
            txt.Visible = Not txt.Visible

            'txt = DirectCast(oItem.FindControl("tbMiddleName"), TextBox)
            'txt.Visible = Not txt.Visible

            txt = DirectCast(oItem.FindControl("tbPassWordExpiration"), TextBox)
            txt.Visible = True

            txt = DirectCast(oItem.FindControl("tbTitle"), TextBox)
            txt.Visible = True

            txt = DirectCast(oItem.FindControl("tbEmail"), TextBox)
            txt.Visible = Not txt.Visible

            txt = DirectCast(oItem.FindControl("tbNewPassword"), TextBox)
            txt.Visible = True

            txt = DirectCast(oItem.FindControl("tbConfirmPassword"), TextBox)
            txt.Visible = True
        End Using

        'Dim xpnl As UpdatePanel = DirectCast(oItem.FindControl("pnlUser"), UpdatePanel)
        'xpnl.Update()
    End Sub

    Protected Sub UpdateUser(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oImg As Button = DirectCast(sender, Button)

        Dim ri As RepeaterItem = DirectCast(oImg.NamingContainer, RepeaterItem)
        Dim lbl As Literal
        'Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        'Dim ltr As New DbTran
        'Dim lbel As Label = DirectCast(ri.FindControl("msg"), Label)

        Try
            Dim lsuserid As String = DirectCast(ri.FindControl("UserId"), Literal).Text
            Dim lsPass As String = DirectCast(ri.FindControl("tbNewPassword"), TextBox).Text
            Dim lsPassExpire As String = DirectCast(ri.FindControl("tbPassWordExpiration"), TextBox).Text.Trim
            If DirectCast(ri.FindControl("tbUserLogin"), TextBox).Text.Trim.IndexOf("'") >= 0 Then
                Master.ShowMessage("User Login cannot contain single quote character (').")
                Exit Sub
            ElseIf UserExist(lsuserid, DirectCast(ri.FindControl("tbUserLogin"), TextBox).Text, DirectCast(ri.FindControl("tbEmail"), TextBox).Text) Then
                Exit Sub
            End If
            If Not IsDate(lsPassExpire) Then
                'lbel.CssClass = "msg_red"
                'lbel.Text = "**Invalid date of password expiration."
                Master.ShowMessage("**Invalid date of password expiration.")
                Exit Sub
            ElseIf IsDate(lsPassExpire) AndAlso Date.Now >= CDate(lsPassExpire) Then
                Master.ShowMessage("**Password expiration date should be greater than the current date.")
                Exit Sub
            End If
            If lsPass <> DirectCast(ri.FindControl("tbConfirmPassword"), TextBox).Text Then
                'lbel.CssClass = "msg_red"
                'lbel.Text = "**Password does not match."
                Master.ShowMessage("**Password does not match.")
                Exit Sub
            End If
            Dim oCrypt As New crypt
            If lsPass.Trim <> "" Then

                lsPass = oCrypt.Encrypt(lsPass)
            End If



            'objCommand = New clsSqlConn(ltr.pTran)

            'objCommand.CommandType = CommandType.StoredProcedure

            'objCommand.CommandText = "xMSP_USERUPDATE"

            'Dim pnl As UpdatePanel = DirectCast(ri.FindControl("pnlUser"), UpdatePanel)

            Dim oDuser As New DocUser
            oDuser.pUserId = lsuserid
            Using ldt As DataTable = oDuser.RetrieveOrigUserInfo()
                'setup old value for admin data changes
                oDuser.pFirstName_o = ldt(0)("FirstName")
                oDuser.pLastName_o = ldt(0)("LastName")
                oDuser.pGroup_o = ldt(0)("UserGroup")
                oDuser.pRole_o = ldt(0)("userRole")
                oDuser.pUserLogin_o = ldt(0)("UserLogin")
                oDuser.pExpirationDate_o = ldt(0)("PassExpirationDate")
                oDuser.pCanChangePassword_o = IIf(ldt(0)("CanChangePass") = "True", "1", "0")
                oDuser.pTitle_o = ldt(0)("Title")
                oDuser.pLockAttempt_o = ldt(0)("LockOutAttempts")
                oDuser.pLocked_o = IIf(ldt(0)("Locked") = "True", "1", "0")
                oDuser.pUserEmail_o = ldt(0)("Email")

                oDuser.pPassword_o = oCrypt.Encrypt(ldt(0)("Password"))
                oDuser.pProfilePic_o = IIf(IsDBNull(ldt(0)("ProfilePic")), "", ldt(0)("ProfilePic"))
                oDuser.pDelDate_o = IIf(IsDBNull(ldt(0)("deldate")), "", "0")
            End Using
            'new value    
            oDuser.pUserId = lsuserid
            oDuser.pUserLogin = DirectCast(ri.FindControl("tbUserLogin"), TextBox).Text
            oDuser.pFirstName = DirectCast(ri.FindControl("tbFirstName"), TextBox).Text
            oDuser.pLastName = DirectCast(ri.FindControl("tbLastName"), TextBox).Text
            oDuser.pMiddleName = ""
            oDuser.pGroup = DirectCast(ri.FindControl("ddlGroupEdit"), DropDownList).SelectedValue
            oDuser.pRole = DirectCast(ri.FindControl("ddlRoleEdit"), DropDownList).SelectedValue
            oDuser.pExpirationDate = lsPassExpire ' DirectCast(ri.FindControl("tbPassWordExpiration"), TextBox).Text
            oDuser.pCanChangePassword = IIf(DirectCast(ri.FindControl("cbCanChange"), CheckBox).Checked, "1", "0")
            oDuser.pTitle = DirectCast(ri.FindControl("tbTitle"), TextBox).Text
            oDuser.pLockAttempt = DirectCast(ri.FindControl("ddlLockout"), DropDownList).SelectedValue
            oDuser.pLocked = IIf(DirectCast(ri.FindControl("cbLocked"), CheckBox).Checked, "1", "0")
            oDuser.pDelDate = IIf(DirectCast(ri.FindControl("cbActive"), CheckBox).Checked, "1", "0")
            If oDuser.pDelDate_o = oDuser.pDelDate Then
                oDuser.pDelDate = ""
            End If

            oDuser.pUserEmail = DirectCast(ri.FindControl("tbEmail"), TextBox).Text
            oDuser.pPassword = lsPass
            oDuser.pIPAddress = Request.UserHostAddress
            oDuser.pFirstTimeuser = ""
            oDuser.UpdateUser()
            'objCommand.ExecTranNonQuery()
            'ltr.pTran.Commit()

            lbl = DirectCast(ri.FindControl("FirstName"), Literal)
            lbl.Text = DirectCast(ri.FindControl("tbFirstName"), TextBox).Text

            lbl = DirectCast(ri.FindControl("UserLogin"), Literal)
            lbl.Text = DirectCast(ri.FindControl("tbUserLogin"), TextBox).Text

            lbl = DirectCast(ri.FindControl("LastName"), Literal)
            lbl.Text = DirectCast(ri.FindControl("tbLastName"), TextBox).Text

            'lbl = DirectCast(ri.FindControl("MiddleName"), Literal)
            'lbl.Text = DirectCast(ri.FindControl("tbMiddleName"), TextBox).Text

            lbl = DirectCast(ri.FindControl("Group"), Literal)
            lbl.Text = DirectCast(ri.FindControl("ddlGroupEdit"), DropDownList).SelectedItem.Text

            lbl = DirectCast(ri.FindControl("Role"), Literal)
            lbl.Text = DirectCast(ri.FindControl("ddlRoleEdit"), DropDownList).SelectedItem.Text

            'lbel.CssClass = "msg_green"
            'lbel.Text = "**Update successful."
            Master.ShowMessage("**Update successful.")
            'pnl.Update()
            pnlRepeater.Update()

        Catch ex As Exception
            'ltr.pTran.Rollback()
            'lbel.CssClass = "msg_red"
            'lbel.Text = "There's an error while updating the user information ( " & ex.Message & " ). Please try again."
            'Throw New Exception(ex.Message)
            Master.ShowMessage("There's an error while updating the user information ( " & ex.Message & " ). Please try again.")
        Finally

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If
            If Not oImg Is Nothing Then
                oImg.Dispose()
                oImg = Nothing
            End If
            If Not ri Is Nothing Then
                ri.Dispose()
                ri = Nothing
            End If

            If Not lbl Is Nothing Then
                lbl.Dispose()
                lbl = Nothing
            End If

        End Try


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
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()
        'RetrieveUsers()
        'ShowResult()

    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        If tbUserID.Text.Trim = "" Then
            Master.ShowMessage("User Login is a required field.")
        ElseIf tbUserID.Text.Trim.IndexOf("'") >= 0 Then
            Master.ShowMessage("User Login cannot contain single quote character (').")
        ElseIf ddlRoleAdd.SelectedValue.Trim = "" Then
            Master.ShowMessage("Role is a required field.")
        ElseIf ddlGroupAdd.SelectedValue.Trim = "" Then
            Master.ShowMessage("Group is a required field.")
        ElseIf Not IsDate(tbExpiration.Text) Then
            Master.ShowMessage("Invalid date of password expiration.")
        ElseIf IsDate(tbExpiration.Text) AndAlso Date.Now >= CDate(tbExpiration.Text) Then
            Master.ShowMessage("Password expiration should be a future date.")
        ElseIf tbPassword.Text.Trim.Length < 8 Then
            Master.ShowMessage("Password should contain 8 or more characters.")
            'ElseIf regKey.Text <> DocSession.sRegKey Then
            '   msg.CssClass = "msg_red"
            '  msg.Text = "** Invalid registration key. Please provide the correct registration key to add a new user."
        ElseIf tbConfirmPassword.Text = tbPassword.Text Then
            If UserDoesNotExist() Then
                SaveUsers()
            End If
        Else

            Master.ShowMessage("Password does not match.")
        End If

        pnlMsg.Update()
    End Sub

    Private Function UserDoesNotExist() As Boolean

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT delDate FROM users WHERE userlogin = '" & tbUserID.Text.Trim & "'"
            'objCommand.ParametersAddWithValue("@UserID", tbUserID.Text)
            ldata = objCommand.ExecData
            If ldata.Rows.Count >= 1 Then

                msg.CssClass = "msg_red"
                Master.ShowMessage("User login " & tbUserID.Text & " already exist. Please try another user login.")
                Return False
            Else
                'remove validation according to Sheen    
                'objCommand = New clsSqlConn
                'objCommand.CommandType = CommandType.Text
                'objCommand.CommandText = "SELECT * FROM users WHERE email = '" & tbEmail.Text.Trim & "'"

                'If objCommand.ExecHasRow Then
                '    msg.CssClass = "msg_red"
                '    Master.ShowMessage("Email address " & tbEmail.Text.Trim & " already exist. Please try another email id.")
                '    Return False
                'Else
                Dim lipos As Integer = InStr(tbEmail.Text.Trim, "@")
                If lipos > 0 Then
                    Dim lsStr As String = tbEmail.Text.Trim.Substring(lipos - 1)
                    Dim lsEmailLayout As String = System.Configuration.ConfigurationManager.AppSettings("EmailLayout")
                    If lsStr <> lsEmailLayout Then
                        'msg.CssClass = "msg_red"
                        'msg.Text = "Email address should be in " & lsEmailLayout & " format."
                        Return True
                    Else
                        Return True
                    End If
                Else
                    Master.ShowMessage("Invalid email address. Please try another email id.")
                    Return False
                End If


                'End If


            End If


        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Master.ShowMessage("There's an error while validating user information ( " & ex.Message & " ). Please try again.")
        Finally


            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Function

    Private Function UserExist(ByVal asUserId As String, ByVal asUserLogin As String, ByVal asEmail As String) As Boolean

        Dim objCommand As clsSqlConn
        'Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT delDate FROM users WHERE userid <> '" & asUserId & "' and userlogin = '" & asUserLogin.Trim & "'"
            
            If objCommand.ExecHasRow Then

                Master.ShowMessage("User Login '" & asUserLogin.Trim & "' already exist. Please try another user login.")
                Return True
            Else

                'objCommand = New clsSqlConn
                'objCommand.CommandType = CommandType.Text
                'objCommand.CommandText = "SELECT deldate FROM users WHERE userid <> '" & asUserId & "' and email = '" & asEmail.Trim & "'"

                'If objCommand.ExecHasRow Then
                '    Master.ShowMessage("Email address " & asEmail & " already exist. Please try another email id.")
                '    Return True
                'Else
                Dim lipos As Integer = InStr(asEmail.Trim, "@")
                If lipos > 0 Then
                    Dim lsStr As String = tbEmail.Text.Trim.Substring(lipos - 1)
                    Dim lsEmailLayout As String = System.Configuration.ConfigurationManager.AppSettings("EmailLayout")
                    If lsStr <> lsEmailLayout Then
                        'msg.CssClass = "msg_red"
                        'msg.Text = "Email address should be in " & lsEmailLayout & " format."
                        Return False
                    Else
                        Return False
                    End If
                Else
                    Master.ShowMessage("Invalid email address. Please try another email id.")
                    Return True
                End If


                'End If


            End If


        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Master.ShowMessage("There's an error while validating user information ( " & ex.Message & " ). Please try again.")
        Finally


            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Function

    Private Sub SaveUsers()

        Dim ec As New crypt
        'Dim objCommand As clsSqlConn
        Dim oDocUSer As DocUser

        Try
    
            oDocUSer = New DocUser
            oDocUSer.pUserId = DocSession.getNextID("userid")
            oDocUSer.pUserLogin = tbUserID.Text
            oDocUSer.pFirstName = tbFirstName.Text
            oDocUSer.pLastName = tbLastName.Text
            oDocUSer.pMiddleName = ""
            oDocUSer.pGroup = ddlGroupAdd.SelectedValue
            oDocUSer.pRole = ddlRoleAdd.SelectedValue
            oDocUSer.pPassword = ec.Encrypt(tbPassword.Text)
            oDocUSer.pCanChangePassword = IIf(cbCanChangePass.Checked, "1", "0")
            oDocUSer.pLoginUserId = DocSession.sUserId  ' this should be from the login person")
            oDocUSer.pTitle = tbTitle.Text
            oDocUSer.pLockAttempt = ddlAttempt.SelectedValue
            oDocUSer.pUserEmail = tbEmail.Text
            oDocUSer.pIPAddress = Request.UserHostAddress
            oDocUSer.pExpirationDate = tbExpiration.Text
            oDocUSer.pFirstTimeuser = IIf(cbCanChangePass.Checked, "1", "0")
            If pDelDate <> "" Then
                oDocUSer.AddInactiveUser()
            Else
                oDocUSer.AddUser()
            End If


            Master.ShowMessage("User Login '" & tbUserID.Text.Trim & "' has been created successfully.")
            tbUserID.Text = ""
            RetrieveUsers()
            pnlRepeater.Update()


        Catch ex As Exception
            'Throw New Exception(ex.Message)
            Master.ShowMessage("Error while saving user (" & ex.Message & "). Please try again")
        Finally

            'If Not objCommand Is Nothing Then
            '    objCommand.Dispose()
            '    objCommand = Nothing
            'End If

        End Try
    End Sub

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        Dim imgDel As ImageButton
        If e.Item.ItemType = ListItemType.Header Then
            imgDel = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)
            AddHandler imgDel.Click, AddressOf DeleteUser

        End If
    End Sub

    Private Sub DeleteUser()
        Dim ri As RepeaterItem
        Dim cbox As CheckBox
        Dim loData As DataTable
        Dim loRow As DataRow

        Try
            loData = New DataTable("tblApprovers")
            loData.Columns.Add("UserLogin", Type.GetType("System.String"))
            loData.Columns.Add("UserId", Type.GetType("System.String"))
            loData.Columns.Add("FirstName", Type.GetType("System.String"))
            loData.Columns.Add("LastName", Type.GetType("System.String"))
            loData.Columns.Add("Title", Type.GetType("System.String"))
            loData.Columns.Add("Group", Type.GetType("System.String"))
            loData.Columns.Add("Role", Type.GetType("System.String"))

            For Each ri In Repeater1.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    cbox = CType(ri.FindControl("cbxDelete"), CheckBox)
                    If cbox.Checked Then
                        loRow = loData.NewRow()
                        loRow("UserId") = DirectCast(ri.FindControl("UserId"), Literal).Text
                        loRow("UserLogin") = DirectCast(ri.FindControl("UserLogin"), Literal).Text
                        loRow("FirstName") = DirectCast(ri.FindControl("FirstName"), Literal).Text
                        loRow("LastName") = DirectCast(ri.FindControl("LastName"), Literal).Text
                        loRow("Title") = DirectCast(ri.FindControl("Title"), Literal).Text
                        loRow("Group") = DirectCast(ri.FindControl("Group"), Literal).Text
                        loRow("Role") = DirectCast(ri.FindControl("Role"), Literal).Text
                        loData.Rows.Add(loRow)
                    End If
                End If
            Next
            If loData.Rows.Count > 0 Then
                Repeater2.DataSource = loData
                Repeater2.DataBind()
                ShowDelete()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try
    End Sub

    'Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
    '    Dim lblFirstName, lblLastName, lblPassWordExpiration, lblTitle, lblRole, lblGroup, lblCanChange, lblLocked As Literal
    '    'lblLockout, 
    '    Dim tFirstName, tLastName, tPassWordExpiration, tTitle As TextBox
    '    Dim cCanChange, cLocked As CheckBox
    '    Dim dlGroupEdit, dlRoleEdit As DropDownList
    '    'dlLockout 
    '    Dim lRole, lGroup As Literal

    '    If cbUpdate.Checked Then
    '        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
    '            lblFirstName = DirectCast(e.Item.FindControl("FirstName"), Literal)
    '            lblFirstName.Visible = False
    '            tFirstName = DirectCast(e.Item.FindControl("tbFirstName"), TextBox)
    '            tFirstName.Visible = True

    '            lblLastName = DirectCast(e.Item.FindControl("LastName"), Literal)
    '            lblLastName.Visible = False
    '            tLastName = DirectCast(e.Item.FindControl("tbLastName"), TextBox)
    '            tLastName.Visible = True

    '            lblTitle = DirectCast(e.Item.FindControl("Title"), Literal)
    '            lblTitle.Visible = False
    '            tTitle = DirectCast(e.Item.FindControl("tbTitle"), TextBox)
    '            tTitle.Visible = True

    '            lblRole = DirectCast(e.Item.FindControl("Role"), Literal)
    '            lRole = DirectCast(e.Item.FindControl("lRole"), Literal)
    '            lblRole.Visible = False
    '            dlRoleEdit = DirectCast(e.Item.FindControl("ddlRoleEdit"), DropDownList)
    '            dlRoleEdit.Visible = True
    '            dlRoleEdit.SelectedValue = lRole.Text

    '            lblGroup = DirectCast(e.Item.FindControl("Group"), Literal)
    '            lGroup = DirectCast(e.Item.FindControl("lGroup"), Literal)
    '            lblGroup.Visible = False
    '            dlGroupEdit = DirectCast(e.Item.FindControl("ddlGroupEdit"), DropDownList)
    '            dlGroupEdit.Visible = True
    '            dlGroupEdit.DataSource = RetrieveGroups()
    '            dlGroupEdit.DataTextField = "groupname"
    '            dlGroupEdit.DataValueField = "groupid"
    '            dlGroupEdit.DataBind()
    '            dlGroupEdit.SelectedValue = lGroup.Text

    '            'lblLockout = DirectCast(e.Item.FindControl("Lockout"), Literal)
    '            'lblLockout.Visible = False
    '            'dlLockout = DirectCast(e.Item.FindControl("ddlLockOut"), DropDownList)
    '            'dlLockout.Visible = True
    '            'dlLockout.SelectedValue = lblLockout.Text.Trim


    '            lblPassWordExpiration = DirectCast(e.Item.FindControl("PasswordExpiration"), Literal)
    '            lblPassWordExpiration.Visible = False
    '            tPassWordExpiration = DirectCast(e.Item.FindControl("tbPassWordExpiration"), TextBox)
    '            tPassWordExpiration.Visible = True

    '            lblCanChange = DirectCast(e.Item.FindControl("CanChange"), Literal)
    '            lblCanChange.Visible = False
    '            cCanChange = DirectCast(e.Item.FindControl("cbCanchange"), CheckBox)
    '            cCanChange.Checked = (lblCanChange.Text = "Yes")
    '            cCanChange.Visible = True

    '            lblLocked = DirectCast(e.Item.FindControl("Locked"), Literal)
    '            lblLocked.Visible = False
    '            cLocked = DirectCast(e.Item.FindControl("cbLocked"), CheckBox)
    '            cLocked.Checked = (lblLocked.Text = "Yes")
    '            cLocked.Visible = True





    '        End If
    '    End If
    'End Sub

    Protected Sub btDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btDelete.Click
        Dim ri As RepeaterItem

        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As DbTran

        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)


            Dim oDocUsers As New DocUser
            For Each ri In Repeater2.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    oDocUsers.pFirstName = DirectCast(ri.FindControl("FirstName"), Literal).Text
                    oDocUsers.pLastName = DirectCast(ri.FindControl("LastName"), Literal).Text
                    oDocUsers.pIPAddress = Request.UserHostAddress
                    oDocUsers.pUserId = DirectCast(ri.FindControl("UserId"), Literal).Text
                    oDocUsers.DeleteUser(objCommand)

                End If
            Next
            ltr.pTran.Commit()
            RetrieveUsers()
            ShowResult()

        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            'Throw New Exception(ex.Message)
            Master.ShowMessage("Error while deleting user (" & ex.Message & "). Please try again")
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
    'Private Sub ShowCriteria()
    '    pSearchCriteria.Visible = Not pSearchCriteria.Visible
    '    pnlSearchCriteria.Update()
    '    Repeater1.Visible = Not Repeater1.Visible
    '    pnlRepeater.Update()

    '    pAddUser.Visible = False
    '    pnlAddUser.Update()
    '    pDeleteUser.Visible = Not pDeleteUser.Visible
    '    pnlDeleteUser.Update()
    'End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btCancel.Click
        ShowResult()
    End Sub

    

    'Protected Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
    '    pSearchCriteria.Visible = Not pSearchCriteria.Visible
    '    pnlSearchCriteria.Update()
    'End Sub

    'Protected Sub imgUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
    '    Dim ri As RepeaterItem

    '    Dim objCommand As clsSqlConn
    '    'Dim ltr As SqlTransaction
    '    Dim ltr As New DbTran

    '    Try
    '        objCommand = New clsSqlConn(ltr.pTran)

    '        'objCommand.CommandType = CommandType.StoredProcedure

    '        'objCommand.CommandText = "xMSP_USERUPDATE"
    '        Dim oDocUser As New DocUser


    '        For Each ri In Repeater1.Items
    '            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
    '                oDocUser.pUserId = DirectCast(ri.FindControl("UserName"), Literal).Text
    '                oDocUser.pFirstName = DirectCast(ri.FindControl("tbFirstName"), TextBox).Text
    '                oDocUser.pLastName = DirectCast(ri.FindControl("tbLastName"), TextBox).Text
    '                oDocUser.pMiddleName = ""
    '                oDocUser.pGroup = DirectCast(ri.FindControl("ddlGroupEdit"), DropDownList).SelectedValue
    '                oDocUser.pRole = DirectCast(ri.FindControl("ddlRoleEdit"), DropDownList).SelectedValue
    '                'objCommand.ParametersAddWithValue("@Password", )
    '                oDocUser.pExpirationDate = DirectCast(ri.FindControl("tbPassWordExpiration"), TextBox).Text
    '                oDocUser.pCanChangePassword = IIf(DirectCast(ri.FindControl("cbCanChange"), CheckBox).Checked, "1", "0")
    '                'objCommand.ParametersAddWithValue("@CreatedBy")
    '                'objCommand.ParametersAddWithValue("@CreatedDate")
    '                'oDocUser.p.pModifiedBy = DocSession.sUserId
    '                'objCommand.ParametersAddWithValue("@ModifiedDat", )
    '                oDocUser.pTitle = DirectCast(ri.FindControl("tbTitle"), TextBox).Text
    '                oDocUser.pLockAttempt = DirectCast(ri.FindControl("ddlLockout"), DropDownList).SelectedValue
    '                oDocUser.pLocked = IIf(DirectCast(ri.FindControl("cbLocked"), CheckBox).Checked, "1", "0")
    '                oDocUser.UpdateUser(objC)
    '                'objCommand.ExecTranNonQuery()
    '                'objCommand.ParametersClear()
    '            End If
    '        Next
    '        ltr.pTran.Commit()
    '        RetrieveUsers()
    '        'ShowCriteria()

    '    Catch ex As Exception
    '        ltr.pTran.Rollback()
    '        Throw New Exception(ex.Message)

    '    Finally

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If
    '        If Not ltr Is Nothing Then
    '            ltr.Dispose()
    '            ltr = Nothing
    '        End If


    '    End Try
    'End Sub

    Private Sub ShowResult()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        pAddUser.Visible = False
        pnlAddUser.Update()

        pDeleteUser.Visible = False
        pnlDeleteUser.Update()

        'pRepeater.Visible = True
        pnlRepeater.Update()

        'imgAddUser.Visible = True
        Master.ShowImageDocument = False


    End Sub

    Private Sub ShowDeletes()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        pRepeater.Visible = False
        pnlRepeater.Update()

        pAddUser.Visible = False
        pnlAddUser.Update()

        pDeleteUser.Visible = True
        pnlDeleteUser.Update()

    End Sub

    Private Sub ShowDelete()

        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pAddUser.Visible = False
        'pnlAddUser.Update()

        pDeleteUser.Visible = True
        pnlDeleteUser.Update()
        Master.ShowImageDocument = True
    End Sub

    Private Sub ShowAdd()
        'pSearchCriteria.Visible = False
        'pnlSearchCriteria.Update()

        'pRepeater.Visible = False
        'pnlRepeater.Update()

        'pDeleteUser.Visible = False
        'pnlDeleteUser.Update()
        'If DocSession.sRegType <> "etp" Then


        '    Dim lnoou As Integer
        '    If DocSession.sRegType = "bsx" Then
        '        lnoou = 2
        '    ElseIf DocSession.sRegType = "std" Then
        '        lnoou = 5
        '    End If

        '    If Repeater1.Items.Count > (lnoou - 1) Then
        '        Master.ShowMessage("You are not allowed to create more than " & CStr(lnoou) & " users. Please upgrade your registration to create more users. ")
        '        Exit Sub
        '    End If
        'End If

        pAddUser.Visible = Not pAddUser.Visible
        pnlAddUser.Update()
        'imgAddUser.Visible = False
        Master.ShowImageDocument = True


    End Sub

    'Private Sub ShowSearch()

    '    pSearchCriteria.Visible = Not pSearchCriteria.Visible
    '    pnlSearchCriteria.Update()

    'End Sub


    

    Protected Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        ShowResult()
    End Sub

    Private Sub imgClose2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose2.Click
        ShowResult()
    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image

        If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort1
            imgSort1.Visible = True
        Else
            imgSort1.Visible = False
        End If
        If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort2
            imgSort2.Visible = True
        Else
            imgSort2.Visible = False

        End If
        If imgSort3.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort3
            imgSort3.Visible = True
        Else
            imgSort3.Visible = False

        End If
        If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort4
            imgSort4.Visible = True
        Else
            imgSort4.Visible = False

        End If
        If imgSort5.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort5
            imgSort5.Visible = True
        Else
            imgSort5.Visible = False

        End If
        If imgSort6.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort6
            imgSort6.Visible = True
        Else
            imgSort6.Visible = False

        End If

        Dim oDocUser As New DocUser

        If img.Visible Then
            If img.ImageUrl.ToLower = "images/asc.png" Then
                img.ImageUrl = "images/desc.png"
                oDocUser.pSortOrder = "Desc"
                hfSortOrder.Value = "Desc"
            Else
                img.ImageUrl = "images/asc.png"
                oDocUser.pSortOrder = "Asc"
                hfSortOrder.Value = "Asc"
            End If
        Else
            img.ImageUrl = "images/asc.png"
            oDocUser.pSortOrder = "Asc"
            hfSortOrder.Value = "Asc"
            img.Visible = True
        End If
        'oDocUser.pUserId = UserName.Text
        'oDocUser.pFirstName = FirstName.Text
        'oDocUser.pLastName = LastName.Text
        'oDocUser.pGroup = ddlGroup.SelectedValue
        'oDocUser.pRole = ddlRole.SelectedValue

        oDocUser.pSortCol = lbSort.Text
        hfSortCol.Value = lbSort.Text
        'Repeater1.DataSource = oDocUser.RetrieveUser
        'Repeater1.DataBind()
        'pnlRepeater.Update()
        'pPager.update()
        RetAction()


    End Sub

    Private Sub btClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClear.Click
        tbExpiration.Text = ""
        tbConfirmPassword.Text = ""
        tbEmail.Text = ""
        tbUserID.Text = ""
        tbFirstName.Text = ""
        tbLastName.Text = ""
        tbTitle.Text = ""
        tbPassword.Text = ""
        cbCanChangePass.Checked = False
        ddlGroupAdd.SelectedValue = ""
        ddlRoleAdd.SelectedValue = ""
    End Sub

    Private Sub lbAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddUser.Click
        ShowAdd()
    End Sub

    Private Sub btClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btClose.Click
        ShowResult()
    End Sub

    Private Sub btCancelMerge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancelMerge.Click
        Master.ShowImageDocument = False
        pCopy.Visible = False
        pnlCopy.Update()
    End Sub

    Private Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Master.ShowImageDocument = False
        pCopy.Visible = False
        pnlCopy.Update()
    End Sub
    Private Sub MergeUsers()
        Master.ShowImageDocument = True

        RetrieveMergeUsers()
        pCopy.Visible = True
        pnlCopy.Update()
    End Sub
    Public Sub RetrieveMergeUsers()
        Dim oUsr As New DocUser

        Using lodata As DataTable = oUsr.UserListWithUserID

            dlSourceUser.DataSource = lodata
            dlSourceUser.DataValueField = "userid"
            dlSourceUser.DataTextField = "UserName"
            dlSourceUser.DataBind()


            dlMergeUser.DataSource = lodata
            dlMergeUser.DataValueField = "Userid"
            dlMergeUser.DataTextField = "UserName"
            dlMergeUser.DataBind()
        End Using
    End Sub

    Private Sub btMerge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMerge.Click
        If dlMergeUser.SelectedValue = dlSourceUser.SelectedValue Then
            mergemsg.Text = "Please select two different users to proceed with merging."
            UpdatePanel1.Update()
        Else
            Dim oUsr As New DocUser
            oUsr.pMergeUserName = dlMergeUser.SelectedItem.Text
            oUsr.pSourceUserName = dlSourceUser.SelectedItem.Text
            oUsr.pMergeUser = dlMergeUser.SelectedValue
            oUsr.pSourceUser = dlSourceUser.SelectedValue
            Try
                oUsr.Merge2User()
                mergemsg.CssClass = "msg_green"
                mergemsg.Text = "The two users has been merged successfully. User ID '" & oUsr.pSourceUser & "' has been deactivated. "
                RetrieveMergeUsers()
                RetrieveUsers()
                pnlCopy.Update()
            Catch ex As Exception
                mergemsg.CssClass = "msg_red"
                mergemsg.Text = "An error occurred while merging users ('" & ex.Message & "'). Please try again."
                UpdatePanel1.Update()
            End Try

        End If
    End Sub

    
    
End Class