Imports System
Imports System.Data.SqlClient

Public Class _Default
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If IsCookieDisabled() Then
            msg.Text = "Your browser's cookie functionality is currently turn-off. You'll need to turn cookies on to use Document Management System effectively."
            hlCookies.Visible = True
        End If
    End Sub

    Private Function IsCookieDisabled() As Boolean
        Dim currentUrl As String = Request.RawUrl
        If Request.QueryString("c") Is Nothing Then
            Try
                Dim c As HttpCookie = New HttpCookie("SupportCookies", "true")
                Response.Cookies.Add(c)
                If currentUrl.IndexOf("?") > 0 Then
                    currentUrl = currentUrl + "&c=1"
                Else
                    currentUrl = currentUrl + "?c=1"
                End If
                Response.Redirect(currentUrl)
            Catch
            End Try
        End If
        If Not Request.Browser.Cookies OrElse Request.Cookies("SupportCookies") Is Nothing Then
            Return True
        End If

        Return False
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then

            'If DocSession.sUserId <> "" Then
            '    Response.Redirect("logout.aspx")
            'End If

            Dim oset As New DocSetting
            'tbUserID.Text = UriPartial.Authority & " " & Request.Url.GetLeftPart(UriPartial.Authority) & " " & Server.MapPath("~/")
            Session.Timeout = oset.getTimeoutValue()
            'RetrieveRegistration()
            DocSession.SearchOption = "P"
            If Not Request.Cookies("DbmMemberxInfo") Is Nothing Then
                tbUserID.Text = Request.Cookies("DbmMemberxInfo").Value
            End If
            If Request.QueryString("cnau") = "y" Then
                Cnau()
            End If


        End If

    End Sub
    
    Private Sub Cnau()

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT userid FROM users WHERE userid = 'ADMIN'"
            Dim semail As String = System.Configuration.ConfigurationManager.AppSettings("adminemail")
            ldata = objCommand.Fill()
            If ldata.Rows.Count = 0 Then
                If semail = "" Then
                    semail = "admin@dbm.gov.ph"
                End If
                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "INSERT INTO Users " & _
           "(UserId,FirstName,LastName,MiddleName,UserGroup,UserRole,Password,Locked,PassExpirationDate,CanChangePass,CreatedBy " & _
           ",CreatedDate,ModifiedBy,ModifiedDat,Title,LockOutAttempts,Email,FirstTimeUser,UserLogin) " & _
     "VALUES " & _
           "('ADMIN','Administrator','DBM','','ADM','A','" & lcr.Encrypt("12345678") & "',0,getdate()+730,1,'ADMIN',getdate(),'ADMIN',getdate(),'Administrator',0,'" & semail & "',0,'ADMIN')"
                objCommand.ExecNonQuery()

            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT groupid FROM groups WHERE groupid = 'ADM'"
            ldata = objCommand.Fill()
            If ldata.Rows.Count = 0 Then

                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "INSERT INTO groups " & _
           "(GroupId,GroupName,PasswordExpiryDays,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,EditIndex,ImportDoc,TrackingColor,TextColor,ReportAccess,AlwaysAllowed,OfficeCode,MainGroupId) " & _
            "VALUES " & _
           "('ADM','Administrator',30,'ADMIN',getdate(),'ADMIN',getdate(),1,1,'','',1,1,'AS','')"
                objCommand.ExecNonQuery()

            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT officecode FROM OFFICE WHERE officecode = 'AS'"
            ldata = objCommand.Fill()
            If ldata.Rows.Count = 0 Then

                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "INSERT INTO Office " & _
           "(OfficeCode,Description,AddressCode,PointPerson) " & _
            "VALUES " & _
           "('AS','Administrative Services','','')"
                objCommand.ExecNonQuery()

            End If

        Catch ex As Exception
            msg.Text = "An error occurred while processing your information (" & ex.Message & ")."
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
    End Sub
    Private Sub RetrieveRegistration()

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT col1,col2,col3,col4,col5,col6,col7,col8,col9 FROM dbDocuments"
            ldata = objCommand.Fill()
            If ldata.Rows.Count = 0 OrElse ldata(0)("col9").ToString.Trim <> "" Then

                Response.Redirect("register.aspx")
            ElseIf ldata.Rows.Count > 1 Then
                objCommand = New clsSqlConn

                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "UPDATE dbDocuments SET col9 = '" & lcr.Encrypt("Your registration has been altered!") & "'"
                objCommand.ExecNonQuery()
                Response.Redirect("register.aspx")
            Else
                Dim lsreg As String = lcr.Decrypt(ldata(0)("col7").ToString.Trim)
                Dim lslastlogin As String = ldata(0)("col8").ToString.Trim
                If lslastlogin.Trim() <> "" Then
                    lslastlogin = lcr.Decrypt(lslastlogin)
                End If
                If lsreg.Split("^")(0) = "Demo" Then
                    If DateDiff(DateInterval.Day, CDate(lcr.Decrypt(ldata(0)("col6").ToString.Trim).Split("^")(0)), Date.Now) >= 30 Then
                        objCommand = New clsSqlConn

                        objCommand.CommandType = CommandType.Text
                        objCommand.CommandText = "UPDATE dbDocuments SET col9 = '" & lcr.Encrypt("Your registration has ended!") & "'"
                        objCommand.ExecNonQuery()
                        Response.Redirect("register.aspx")
                    Else
                        If lcr.Decrypt(ldata(0)("col2").ToString.Trim) <> Request.ServerVariables("remote_addr") Then
                            objCommand = New clsSqlConn

                            objCommand.CommandType = CommandType.Text
                            objCommand.CommandText = "UPDATE dbDocuments SET col9 = '" & lcr.Encrypt("Your registration has been altered!") & "'"
                            objCommand.ExecNonQuery()
                            Response.Redirect("register.aspx")
                        ElseIf lslastlogin <> "" AndAlso CDate(lslastlogin) > Date.Now Then
                            objCommand = New clsSqlConn

                            objCommand.CommandType = CommandType.Text
                            objCommand.CommandText = "UPDATE dbDocuments SET col9 = '" & lcr.Encrypt("Your registration has been altered!") & "'"
                            objCommand.ExecNonQuery()
                            Response.Redirect("register.aspx")
                        Else
                            DocSession.sRegKey = lcr.Decrypt(ldata(0)("col1").ToString.Trim)
                            DocSession.sRegType = lsreg.Split("^")(1)
                            DocSession.sRegDemo = lsreg.Split("^")(0)
                            objCommand = New clsSqlConn

                            objCommand.CommandType = CommandType.Text
                            objCommand.CommandText = "UPDATE dbDocuments SET col8 = '" & lcr.Encrypt(Date.Now.ToShortDateString) & "'"
                            objCommand.ExecNonQuery()
                        End If
                    End If
                Else
                    If lcr.Decrypt(ldata(0)("col2").ToString.Trim) <> Request.ServerVariables("remote_addr") Then
                        objCommand = New clsSqlConn

                        objCommand.CommandType = CommandType.Text
                        objCommand.CommandText = "UPDATE dbDocuments SET col9 = '" & lcr.Encrypt("Your registration has been altered!") & "'"
                        objCommand.ExecNonQuery()
                        Response.Redirect("register.aspx")
                    Else
                        DocSession.sRegKey = lcr.Decrypt(ldata(0)("col1").ToString.Trim)
                        DocSession.sRegType = lsreg.Split("^")(1)
                        DocSession.sRegDemo = lsreg.Split("^")(0)
                    End If
                End If
            End If



        Catch ex As Exception
            msg.Text = "An error occurred while processing your information (" & ex.Message & ")."
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
    End Sub

    Private Function retrieve_userinfo(ByVal asUserLogin As String) As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As New SqlCommand
        'Dim adpSecurity As New SqlDataAdapter

        'Dim oConn As New Odbc.OdbcConnection(str)
        'Dim objCommand As New Odbc.OdbcCommand
        'Dim adpSecurity As New Odbc.OdbcDataAdapter
        Dim ldt As DataTable
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn



            Dim s_sql As String = "SELECT " &
              "u.userid,u.userlogin,u.password, " &
              "u.locked, " &
              "u.firstname, " &
              "u.lastname, " &
              "u.email, " &
              "u.userrole, " &
              "u.usergroup, " &
              "u.deldate, " &
                "u.LockOutAttempts, "

            s_sql = s_sql & "expiry = convert(char(10), u.passexpirationdate,101), " & _
                            "profilePic = isnull(u.profilePic,'default.png'), " & _
                            "grouplogo = isnull(g.grouplogo,'dbm.png'), " & _
                            "ReceiptReplyName = isnull(g.ReceiptReplyName,'DEPARTMENT OF BUDGET AND MANAGEMENT'), " & _
                            "firsttimeuser = isnull(u.firsttimeuser,0), " & _
                            "reportaccess = isnull(case when g.reportaccess = 1 then '1' else '0' end,'0'), " & _
                            "EditIndex = isnull(case when g.EditIndex = 1 then '1' else '0' end,'0'), " & _
                            "importdoc = isnull(case when g.importdoc = 1 then '1' else '0' end,'0'), " & _
                            "CanPrint = isnull(case when g.CanPrint = 1 then '1' else '0' end,'0'), " & _
                            "CanDownload = isnull(case when g.CanDownload = 1 then '1' else '0' end,'0'), " & _
                            "VersionControl = isnull(case when g.VersionControl = 1 then '1' else '0' end,'0'), " & _
                            "alwaysallowed = isnull(case when g.alwaysallowed = 1 then '1' else '0' end,'0'), " & _
                            "TurnOffEmailNotification = isnull(case when u.TurnOffEmailNotification = 1 then 'Yes' else 'No' end,'Yes'), " & _
                            "ofccode = isnull(g.officecode,''), " & _
                            "ofcname = isnull(o.Description,''), " & _
                            "AddressDesc = isnull(oa.AddressDesc,'') "


            s_sql = s_sql & "FROM " &
              "users u inner join groups g " &
              "on u.usergroup = g.groupid " &
              " left join office o " &
              "on o.OfficeCode = g.OfficeCode " &
              " left join officeaddress oa " &
              "on oa.AddressCode = o.AddressCode " &
             "WHERE " &
            "(u.userlogin = '" & Replace(asUserLogin, "'", "''").ToUpper & "') "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            'objCommand.ParametersAddWithValue("@Userid", )


            ldt = objCommand.Fill
            Return ldt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not ldt Is Nothing Then
                ldt.Dispose()
                ldt = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try

    End Function

    Private Sub lockuser(ByVal asUserLogin As String)
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As New SqlCommand
        'Dim adpSecurity As New SqlDataAdapter
        'Dim ldt As New DataTable
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            'Dim osp As New cls_storedproc
            'osp.pUserId = asUserName
            'osp.pLocked = "1"
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "UPDATE Users SET Locked = 1,ModifiedDat = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
                "WHERE UserLogin = '" & asUserLogin & "'"
            'Else
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "xMSP_USERLOCK"
            'objCommand.ParametersAddWithValue("@Userid", asUserName)
            'objCommand.ParametersAddWithValue("@Locked", 1)
            'End If
            objCommand.ExecNonQuery()

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Sub

    Protected Sub btLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btLogin.Click
        'need to comment this out before sending it to mike
        'tbPassword.Text = "1234567890"
        'tbUserID.Text = "gsotelo"
        Dim lbOk As Boolean = True
        If Not Context.Session Is Nothing Then
            If Session.IsNewSession Then
                Dim lsCook As String = Request.Headers("cookie")
                If (Not lsCook Is Nothing AndAlso lsCook.IndexOf("ASP.NET_SessionId") >= 0) Then
                    'msg.Text = "Session expired. Please click reload or refresh button of the browser before signing in."
                    'lbOk = False
                    Response.Redirect("Login.aspx")
                End If
            End If
        End If
        If lbOk Then


            Try


                If tbPassword.Text = "" OrElse tbUserID.Text = "" Then
                    msg.Text = "Invalid user login or password."
                Else
                    Dim ec As New crypt
                    Dim spass As String = ec.Encrypt(tbPassword.Text)


                    Using ldt As DataTable = retrieve_userinfo(tbUserID.Text)
                        If ldt.Rows.Count > 0 Then

                            If ldt(0)("UserRole") <> "A" AndAlso CDate(ldt(0)("Expiry")) <= Date.Now Then
                                msg.Text = "Your password is already expired. Please contact the administrator."
                            ElseIf ldt(0)("Locked") = "1" AndAlso CInt(ldt(0)("LockOutAttempts")) > 0 Then 'AndAlso CInt(ldt(0)("LockOutAttempts")) > 0 Then
                                msg.Text = "Your account is currently locked. Please contact the administrator."
                            ElseIf isdate(ldt(0)("deldate")) Then 'AndAlso CInt(ldt(0)("LockOutAttempts")) > 0 Then
                                msg.Text = "Your account is currently deactivated. Please contact the administrator."

                            ElseIf ldt(0)("password") <> spass Then
                                msg.Text = "Invalid user login or password."
                                'Session("username") = ""
                                DocSession.sUserId = ""
                                DocSession.sUserLogin = ""

                                tbPassword.Text = ""
                                Dim lAttempt As Integer = CInt(ldt(0)("LockOutAttempts"))
                                If lAttempt > 0 Then

                                    hfCtr.Value = CStr(CInt(hfCtr.Value) + 1)
                                    If CInt(hfCtr.Value) < lAttempt Then
                                        'msg.Text = "Please note that you have entered wrong password for the 2nd time. One more wrong attempt will lock your account."
                                        msg.Text = "You only have " & CStr(lAttempt - CInt(hfCtr.Value)) & " attempt(s) left to login in the system with the correct user/password. Otherwise, your account will be locked. "
                                    ElseIf CInt(hfCtr.Value) >= lAttempt Then
                                        lockuser(tbUserID.Text)
                                        If lAttempt > 1 Then
                                            msg.Text = "Your account has been locked because you reached the maximum consecutive attempts of invalid password. Please contact the administrator to be able to login again."
                                        Else
                                            msg.Text = "Your account has been locked. Please contact the administrator to be able to login again."
                                        End If
                                    End If

                                End If

                            Else
                                Dim oGroupAcc As New DocGroupAccess()
                                oGroupAcc.pGroupId = ldt(0)("UserGroup")
                                If ldt(0)("AlwaysAllowed") = "0" And ldt(0)("UserRole") <> "A" Then

                                    If oGroupAcc.RetrieveWorkSchedule().Rows.Count = 0 Then
                                        msg.Text = "You are not allowed to login at this time. Please contact administrator to know your schedule."
                                        Exit Sub
                                    End If
                                End If
                                'If ldt(0)("UserRole") <> "A" Then
                                '    oGroupAcc.pHoliday = Date.Now.ToShortDateString
                                '    If oGroupAcc.IsHoliday() Then
                                '        msg.Text = "You are not allowed to login during holidays. Please contact the administrator."
                                '        Exit Sub
                                '    End If
                                'End If



                                'Dim lsG As String = ldt(0)("UserGroup")
                                'If IsHoliday(lsG) = True AndAlso ldt(0)("UserRole") <> "A" Then
                                'msg.Text = "You are not allowed to login during holiday. Please contact administrator."
                                'Exit Sub
                                'Else


                                'Session("dt") = ldt
                                'Session("userid") = tbUserID.Text.ToUpper
                                DocSession.sUserId = ldt(0)("UserId") 'tbUserID.Text.ToUpper
                                DocSession.sUserLogin = ldt(0)("UserLogin") 'tbUserID.Text.ToUpper
                                AddHistory()
                                'Session("username") = ldt(0)("FirstName") & " " & ldt(0)("LastName")
                                DocSession.sUserName = ldt(0)("FirstName") & " " & ldt(0)("LastName")
                                'Session("email") = ldt(0)("Email")
                                DocSession.sUserEmail = ldt(0)("Email")
                                'Session("UserRole") = ldt(0)("UserRole")
                                DocSession.sUserRole = IIf(ldt(0)("UserRole") = "A", "AC", ldt(0)("UserRole"))
                                DocSession.sOfcCode = ldt(0)("ofcCode")
                                DocSession.sOfcName = ldt(0)("ofcName")
                                DocSession.sEditIndex = ldt(0)("EditIndex")
                                DocSession.sCanPrint = ldt(0)("CanPrint")
                                DocSession.sCanDownload = ldt(0)("CanDownload")
                                DocSession.sVersionControl = ldt(0)("VersionControl")
                                DocSession.sImportDoc = ldt(0)("ImportDoc")
                                DocSession.sReportAccess = ldt(0)("reportaccess")
                                DocSession.sDocAccess = 5 'oDoc.RetrieveAccess 'oDoc.RetrieveGroupAccess()
                                'DocSession.sDeleteDoc = ldt(0)("DeleteDoc")
                                'Session("UserGroup") = ldt(0)("UserGroup")
                                DocSession.sUserGroup = ldt(0)("UserGroup")
                                DocSession.sGroupAddress = ldt(0)("AddressDesc")
                                DocSession.sFirstTimeUser = IIf(DocSession.IsTrue(ldt(0)("FirstTimeUser")), "True", "False")
                                Dim oDoc As New DocGroup
                                oDoc.pGroupID = DocSession.sUserGroup
                                DocSession.sDocHierarchy = oDoc.RetrieveDocHierarchy 'oDoc.RetrieveGroupAccess()
                                If ldt(0)("profilePic").ToString().Trim() = "" Then
                                Else

                                End If
                                DocSession.sprofilePic = ldt(0)("profilePic")

                                DocSession.GroupLogo = ldt(0)("grouplogo")
                                DocSession.ReceiptReplyName = ldt(0)("ReceiptReplyName")
                                DocSession.sPasswordExpiry = ldt(0)("Expiry")
                                DocSession.EmailNotification = ldt(0)("TurnOffEmailNotification")
                                'If Request.Cookies("DbmMemberxInfo") Is Nothing Then
                                'If Request.Cookies("DbmMemberxInfo").Value <> tbUserID.Text Then
                                Dim mycookie As HttpCookie = New HttpCookie("DbmMemberxInfo")
                                mycookie.Value = tbUserID.Text
                                mycookie.Expires = DateTime.Now.AddDays(10)
                                Response.Cookies.Add(mycookie)

                                If DocSession.sUserRole = "H" Then
                                    Response.Redirect("userhelpdesk.aspx")
                                Else
                                    'Response.Redirect("userhelpdesk.aspx")
                                    Response.Redirect("home.aspx")
                                End If


                                'Server.Transfer("home.aspx")
                                'End If
                            End If
                                Else
                            msg.Text = "Invalid user login or password."
                            'Session("dt") = ""
                            DocSession.sUserId = ""
                            DocSession.sUserLogin = ""
                            DocSession.sUserName = ""
                            tbPassword.Text = ""
                        End If
                    End Using
                End If

                pnlMsg.Update()
            Catch ex As Exception
                msg.Text = "Error occurred while processing your request (" & ex.Message & "). Please Try again later."


            End Try
        End If
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
            ohist.pAction = "Logged in from " & Request.UserHostAddress
            ohist.pTask = "System"
            ohist.AddHistory()

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Private Function IsHoliday(ByVal aGID As String) As Boolean
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlClient.SqlDataAdapter
        Dim ldata As DataTable
        'Dim lrow As DataRow = ldata.NewRow
        Try

            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pGroupId = aGID
            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.pCommandText = "SELECT Holiday FROM GroupWorkHoliday " & _
                "WHERE TRUNC(Holiday) = TO_DATE('" & Date.Now.ToShortDateString & "','mm/dd/yyyy') AND groupId = '" & aGID & "' "
            Else
                objCommand.pCommandText = "SELECT Holiday FROM GroupWorkHoliday " & _
                "WHERE Holiday = '" & Date.Now.ToShortDateString & "' AND groupId = '" & aGID & "' "
            End If
            
            'Else
            '    objCommand.pCommandType = CommandType.StoredProcedure

            '    objCommand.pSqlString = "xMSP_GETHOLIDAY"
            '    objCommand.AddParameters("@GroupId", aGID)

            'End If

            ldata = objCommand.ExecData()

            If ldata.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
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
End Class
