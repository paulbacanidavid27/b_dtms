Public Class DocUser
#Region "new Property"
    Dim IPAddress As String = ""
    Dim LoginUserId As String = ""
    Dim UserId As String = ""
    Dim UserLogin As String = ""
    Dim UserLogin_o As String = ""
    Dim DelDate As String = ""
    Dim DelDate_o As String = ""
    Dim UserName As String = ""
    Dim FirstName As String = ""
    Dim LastName As String = ""
    Dim EmailNotification As String = ""
    Dim SearchString As String = ""
    Dim Title As String = ""
    Dim UserEmail As String = ""
    Dim MiddleName As String = ""
    Dim Password As String = ""
    Dim ProfilePic As String = ""
    Dim CanChangePassword As String = ""
    Dim DocType As String = ""
    Dim SortOrder As String = ""
    Dim SortCol As String = ""
    Dim GroupAccess As String = ""
    Dim RoleAccess As String = ""
    Dim FirstTimeUser As String = ""
    Dim LockAttempt As String = ""
    Dim ExpirationDate As String = ""
    Dim Locked As String = ""
    Dim RowsPerPage As String
    Dim OfficeCode As String
    Dim MergeUser As String = ""
    Dim SourceUser As String = ""
    Dim MergeUserName As String = ""
    Dim SourceUserName As String = ""
    Dim Idx As String
    Public Sub New()

    End Sub

    Public Property pOfficeCode() As String
        Get
            Return OfficeCode
        End Get
        Set(ByVal value As String)
            OfficeCode = value
        End Set

    End Property
    Public Property pMergeUser() As String
        Get
            Return MergeUser
        End Get
        Set(ByVal value As String)
            MergeUser = value
        End Set

    End Property

    Public Property pTurnOffEmailNotification() As String
        Get
            Return EmailNotification
        End Get
        Set(ByVal value As String)
            EmailNotification = value
        End Set

    End Property

    Public Property pSourceUser() As String
        Get
            Return SourceUser
        End Get
        Set(ByVal value As String)
            SourceUser = value
        End Set

    End Property

    Public Property pMergeUserName() As String
        Get
            Return MergeUserName
        End Get
        Set(ByVal value As String)
            MergeUserName = value
        End Set

    End Property

    Public Property pSourceUserName() As String
        Get
            Return SourceUserName
        End Get
        Set(ByVal value As String)
            SourceUserName = value
        End Set

    End Property

    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
        End Set

    End Property

    Public Property pIdx() As String
        Get
            Return Idx
        End Get
        Set(ByVal value As String)
            Idx = value
        End Set

    End Property

    Public Property pLockAttempt() As String
        Get
            Return LockAttempt
        End Get
        Set(ByVal value As String)
            LockAttempt = value
        End Set

    End Property

    Public Property pLocked() As String
        Get
            Return Locked
        End Get
        Set(ByVal value As String)
            Locked = value
        End Set

    End Property

    Public Property pExpirationDate() As String
        Get
            Return ExpirationDate
        End Get
        Set(ByVal value As String)
            ExpirationDate = value
        End Set

    End Property
    Public Property pSortOrder() As String
        Get
            Return SortOrder
        End Get
        Set(ByVal value As String)
            SortOrder = value
        End Set

    End Property

    Public Property pGroup() As String
        Get
            Return GroupAccess
        End Get
        Set(ByVal value As String)
            GroupAccess = value
        End Set

    End Property

    Public Property pRole() As String
        Get
            Return RoleAccess
        End Get
        Set(ByVal value As String)
            RoleAccess = value
        End Set

    End Property

    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set

    End Property
    Public Property pSortCol() As String
        Get
            Return SortCol
        End Get
        Set(ByVal value As String)
            SortCol = value
        End Set

    End Property

    Public Property pFirstTimeuser() As String
        Get
            Return FirstTimeUser
        End Get
        Set(ByVal value As String)
            FirstTimeUser = value
        End Set

    End Property


    Public Property pProfilePic() As String
        Get
            Return ProfilePic
        End Get
        Set(ByVal value As String)
            ProfilePic = value
        End Set

    End Property

    Public Property pTitle() As String
        Get
            Return Title
        End Get
        Set(ByVal value As String)
            Title = value
        End Set

    End Property

    Public Property pCanChangePassword() As String
        Get
            Return CanChangePassword
        End Get
        Set(ByVal value As String)
            CanChangePassword = value
        End Set

    End Property

    Public Property pUserEmail() As String
        Get
            Return UserEmail
        End Get
        Set(ByVal value As String)
            UserEmail = value
        End Set

    End Property

    

    Public Property pMiddleName() As String
        Get
            Return MiddleName
        End Get
        Set(ByVal value As String)
            MiddleName = value
        End Set

    End Property
    Public Property pDelDate() As String
        Get
            Return DelDate
        End Get
        Set(ByVal value As String)
            DelDate = value
        End Set

    End Property

    Public Property pDelDate_o() As String
        Get
            Return DelDate_o
        End Get
        Set(ByVal value As String)
            DelDate_o = value
        End Set

    End Property

    Public Property pPassword() As String
        Get
            Return Password
        End Get
        Set(ByVal value As String)
            Password = value
        End Set

    End Property
    Public Property pSearchString() As String
        Get
            Return SearchString
        End Get
        Set(ByVal value As String)
            SearchString = value
        End Set

    End Property

    Public Property pUserId() As String
        Get
            Return UserId
        End Get
        Set(ByVal value As String)
            UserId = value
        End Set

    End Property
    Public Property pUserLogin() As String
        Get
            Return UserLogin
        End Get
        Set(ByVal value As String)
            UserLogin = value
        End Set

    End Property
    Public Property pUserLogin_o() As String
        Get
            Return UserLogin_o
        End Get
        Set(ByVal value As String)
            UserLogin_o = value
        End Set

    End Property
    Public Property pLoginUserId() As String
        Get
            Return LoginUserId
        End Get
        Set(ByVal value As String)
            LoginUserId = value
        End Set

    End Property

    Public Property pFirstName() As String
        Get
            Return FirstName
        End Get
        Set(ByVal value As String)
            FirstName = value
        End Set

    End Property

    Public Property pLastName() As String
        Get
            Return LastName
        End Get
        Set(ByVal value As String)
            LastName = value
        End Set

    End Property

    Public Property pDocType() As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set

    End Property
#End Region
#Region "Old Property"

    Dim oFirstName As String
    Dim oLastName As String
    Dim oTitle As String
    Dim oUserEmail As String
    Dim oMiddleName As String
    Dim oPassword As String
    Dim oProfilePic As String
    Dim oCanChangePassword As String
    Dim oGroupAccess As String
    Dim oRoleAccess As String
    Dim oLockAttempt As String
    Dim oExpirationDate As String
    Dim oLocked As String

    Public Property pLockAttempt_o() As String
        Get
            Return oLockAttempt
        End Get
        Set(ByVal value As String)
            oLockAttempt = value
        End Set

    End Property

    Public Property pLocked_o() As String
        Get
            Return oLocked
        End Get
        Set(ByVal value As String)
            oLocked = value
        End Set

    End Property

    Public Property pExpirationDate_o() As String
        Get
            Return oExpirationDate
        End Get
        Set(ByVal value As String)
            oExpirationDate = value
        End Set

    End Property

    Public Property pGroup_o() As String
        Get
            Return oGroupAccess
        End Get
        Set(ByVal value As String)
            oGroupAccess = value
        End Set

    End Property

    Public Property pRole_o() As String
        Get
            Return oRoleAccess
        End Get
        Set(ByVal value As String)
            oRoleAccess = value
        End Set

    End Property

    Public Property pProfilePic_o() As String
        Get
            Return oProfilePic
        End Get
        Set(ByVal value As String)
            oProfilePic = value
        End Set

    End Property

    Public Property pTitle_o() As String
        Get
            Return oTitle
        End Get
        Set(ByVal value As String)
            oTitle = value
        End Set

    End Property

    Public Property pCanChangePassword_o() As String
        Get
            Return oCanChangePassword
        End Get
        Set(ByVal value As String)
            oCanChangePassword = value
        End Set

    End Property

    Public Property pUserEmail_o() As String
        Get
            Return oUserEmail
        End Get
        Set(ByVal value As String)
            oUserEmail = value
        End Set

    End Property

    Public Property pMiddleName_o() As String
        Get
            Return oMiddleName
        End Get
        Set(ByVal value As String)
            oMiddleName = value
        End Set

    End Property

    Public Property pPassword_o() As String
        Get
            Return oPassword
        End Get
        Set(ByVal value As String)
            oPassword = value
        End Set

    End Property
    

    Public Property pFirstName_o() As String
        Get
            Return oFirstName
        End Get
        Set(ByVal value As String)
            oFirstName = value
        End Set

    End Property

    Public Property pLastName_o() As String
        Get
            Return oLastName
        End Get
        Set(ByVal value As String)
            oLastName = value
        End Set

    End Property
    
#End Region

    Public Function RetrieveUsersWithAccess() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn = Nothing

        Dim ldata As DataTable = Nothing
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text 'CommandType.StoredProcedure
            'objCommand.CommandText = "xMSP_USERLISTWITHACCESS"

            Dim s_sql As String = "SELECT " & _
                    "u.userid, "

            If DocSession.OraClient Then
                s_sql = s_sql & " (NVL(u.FirstName,'') || ' ' ||NVL(u.LastName,'')) AS username, " & _
                    "NVL(u.email,'') AS email "
            Else
                s_sql = s_sql & " (isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'')) AS username, " & _
                    "isnull(u.email,'') AS email "
            End If
            

            s_sql = s_sql & "FROM " & _
            "users u inner join Groups g " & _
            "ON u.usergroup = g.groupid " & _
            " Where u.delDate is null and u.locked <> 1 " 'and g.officecode = '" & pOfficeCode & "' "
            If pSearchString <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & " AND (lower(u.firstname)||' '||lower(u.lastname) like '%" & Replace(pSearchString, "'", "''").ToLower() & "%' OR u.email like '%" & Replace(pSearchString, "'", "''") & "%') and u.user <> '" & Replace(pUserId, "'", "''").ToLower() & "' "
                Else
                    s_sql = s_sql & " AND (u.firstname+' '+u.lastname like '%" & Replace(pSearchString, "'", "''") & "%' OR u.email like '%" & Replace(pSearchString, "'", "''") & "%') and u.userid <> '" & Replace(pUserId, "'", "''") & "' "
                End If

            End If



            objCommand.CommandText = s_sql
            ldata = objCommand.Fill

            Return ldata

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

    Public Function RetrieveUserByGroup() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text


            'Dim s_sql As String = "SELECT " & _
            '        "u.userid, " & _
            '        "username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''), " & _
            '        "email = isnull(u.email,'') " & _
            '        "FROM " & _

            Dim s_sql As String = "SELECT " & _
                    "u.userid, "

            If DocSession.OraClient Then
                s_sql = s_sql & " (NVL(u.FirstName,'') || ' ' ||NVL(u.LastName,'')) AS username, " & _
                    "NVL(u.email,'') AS email "
            Else
                s_sql = s_sql & " (isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'')) AS username, " & _
                    "isnull(u.email,'') AS email "
            End If


            s_sql = s_sql & "FROM " & _
            "users u " 

            If pDocType <> "" Then
                s_sql = s_sql & " inner join GroupDocAccess gda " & _
                                 "ON u.usergroup = gda.groupid and gda.docId = '" & Replace(pDocType, "'", "''") & "' and gda.GroupAccessId >= 1 "
            End If

            s_sql = s_sql & "  WHERE deldate is null  and locked <> 1 and u.usergroup = '" & pGroup.ToLower & "' and u.userid <> '" & Replace(pUserId, "'", "''").ToLower & "' "

            objCommand.CommandText = s_sql
            ldata = objCommand.Fill

            Return ldata

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


    Public Function RetrieveUsers() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            'Dim s_sql As String = "SELECT u.userid,username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''),email = isnull(u.email,'') " & _
            '                        "FROM users u "

            Dim s_sql As String = "SELECT " & _
                    "u.userid, "

            If DocSession.OraClient Then
                s_sql = s_sql & " (NVL(u.FirstName,'') || ' ' ||NVL(u.LastName,'')) AS username, " & _
                    "NVL(u.email,'') AS email "
            Else
                s_sql = s_sql & " (isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'')) AS username, " & _
                    "isnull(u.email,'') AS email "
            End If


            s_sql = s_sql & "FROM users u "

            If pSearchString <> "" Then
                If DocSession.OraClient Then
                    s_sql = s_sql & "WHERE ( u.firstname||' '||u.lastname like '%" & Replace(pSearchString, "'", "''") & "%')"
                Else
                    s_sql = s_sql & "WHERE ( u.firstname+' '+u.lastname like '%" & Replace(pSearchString, "'", "''") & "%')"
                End If
                If pGroup <> "" Then
                    s_sql = s_sql & " AND u.UserGroup = '" & pGroup & "' "
                End If
            Else
                If pGroup <> "" Then
                    s_sql = s_sql & " WHERE u.UserGroup = '" & pGroup & "' "
                End If
            End If

            s_sql = s_sql & " order by u.firstname,u.lastname "
            objCommand.CommandText = s_sql '"xMSP_USERLIST"

            'If pSearchString <> "" Then
            '    objCommand.ParametersAddWithValue("@SearchString", pSearchString)
            'End If
            ldata = objCommand.Fill()
            Return ldata

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

    Public Function UserList() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String = "SELECT upper(u.userid) as userid,username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''),email = isnull(u.email,'') " & _
                                    "FROM users u WHERE delDate is null and (locked is null OR locked = 0) "

            If (Not pGroup Is Nothing) AndAlso pGroup <> "" Then
                s_sql = s_sql & " AND usergroup = '" & pGroup & "' "
            End If

            s_sql = s_sql & " ORDER BY UserName "
            
            objCommand.CommandText = s_sql '"xMSP_USERLIST"

            
            ldata = objCommand.Fill()
            Return ldata

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

    Public Function UserListWithUserID() As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim s_sql As String = "SELECT upper(u.userid) as userid,username = u.userlogin+' - ' + isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''),email = isnull(u.email,'') " & _
                                    "FROM users u WHERE delDate is null and (locked is null OR locked = 0) ORDER BY UserName"


            objCommand.CommandText = s_sql '"xMSP_USERLIST"


            ldata = objCommand.Fill()
            Return ldata

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

    Public Function RetrieveUserEmail() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Try
            '           Dim s_sql As String = "SELECT u.userid, " & _
            ' "username = isnull(u.FirstName,'') + ' ' +isnull(u.LastName,''), " & _
            ' "email = isnull(u.email,'') " & _
            '"FROM " & _

            Dim s_sql As String = "SELECT " & _
                    "u.userid, " & _
                    "userlogin, "
            If DocSession.OraClient Then
                s_sql = s_sql & " (NVL(u.FirstName,'') + ' ' +NVL(u.LastName,'')) AS username, " & _
                    "NVL(u.email,'') AS email "
            Else
                s_sql = s_sql & " (isnull(u.FirstName,'') + ' ' +isnull(u.LastName,'')) AS username, " & _
                    "isnull(u.email,'') AS email "
            End If


            s_sql = s_sql & "FROM " & _
  "users u " & _
 "WHERE (u.email is not null or u.email <> '') "
            If pSearchString <> "" Then
                s_sql = s_sql & " AND (u.email like '%" & Replace(pSearchString, "'", "''") & "%' " & _
                                         "OR u.firstname like '%" & Replace(pSearchString, "'", "''") & "%' " & _
                                         "OR u.lastname like '%" & Replace(pSearchString, "'", "''") & "%') "
            End If
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_USERLISTEMAIL"

            If pSearchString <> "" Then
                objCommand.ParametersAddWithValue("@SearchString", pSearchString)
            End If
            ldata = objCommand.Fill

            Return ldata

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
    Public Function RetrieveOrigUserInfo() As DataTable
        Dim s_sql As String = "SELECT FirstName,LastName,UserGroup,userRole,PassExpirationDate,CanChangePass,Title,LockOutAttempts,Locked,Email,Password,ProfilePic,UserLogin,deldate FROM Users " & _
        " WHERE UserId = '" & Replace(pUserId, "'", "''") & "'"
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql



            ldata = objCommand.Fill()

            Return ldata


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
    Public Function RetrieveUser() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) + CInt(pRowsPerPage)
        Try
            s_sql = "select * from (SELECT "
            If Not DocSession.OraClient Then
                s_sql = s_sql & " TOP " & lTop.ToString & " "
            End If


            s_sql = s_sql & " (row_number() over (ORDER BY " & s_order & " " & pSortOrder & ")) AS rn, " &
                      "u.userid, " &
                      "u.userlogin, " &
                      "u.firstname, " &
                      "u.middlename, " &
                      "u.lastname, " &
                      "u.usergroup, " &
                      "g.groupname, " &
                      "u.userrole, " &
                      "u.title, " &
                      "u.email, " &
                      "u.lockoutattempts, " &
                      "(case u.userrole when 'A' then 'Admin' when 'G' then 'Group Officer' when 'D' then 'Super User' when 'R' then 'Report Generator' when 'L' then 'Archiving' when 'H' then 'Helpdesk' else 'User' end) AS urole, " &
                      "(case u.canchangepass when 1 then 'Yes' else 'No' end) AS canchangepass , " &
                      "(case u.locked when 1 then 'Yes' else 'No' end) AS locked , u.deldate, "
            If DocSession.OraClient Then
                s_sql = s_sql & "TO_CHAR(u.passexpirationdate,'mm/dd/yyyy') AS passexpirationdate, " & _
                      "(NVL(u1.FirstName,'') || ' ' ||NVL(u1.LastName,'')) AS cby , " & _
                      "TO_CHAR(u.createddate,'mm/dd/yyyy') AS cdate, " & _
                      "NVL(TO_CHAR(u.deldate,'mm/dd/yyyy'),' ') AS ddate, " & _
                      "NVL(u.profilepic,'default.png') AS profilepic "
            Else
                s_sql = s_sql & "convert(char(10),u.passexpirationdate,101) AS passexpirationdate, " & _
                      "(isnull(u1.FirstName,'') + ' ' +isnull(u1.LastName,'')) AS cby , " & _
                      "convert(char(10),u.createddate,101) AS cdate, " & _
                      "isnull(convert(char(10),u.deldate,101),'') AS ddate, " & _
                      "isnull(u.profilepic,'default.png') AS profilepic "
            End If


            s_sql = s_sql & "FROM " &
                      "users u " &
                      "INNER JOIN groups g " &
                       "ON u.usergroup = g.groupid " &
                      "INNER JOIN users u1 " &
                       "ON u1.userid = u.createdby " &
                        " Where u.userid is not null  " & WhereClause() & " " &
                      ") dtbl " &
            " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx & " "

            's_sql = s_sql '& " ORDER BY " & s_order


            's_sql = s_sql & " " & pSortOrder

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"



            ldata = objCommand.Fill()

            Return ldata


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
    Public Function CountUser() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String
        
        Try
            s_sql = "SELECT count(*) " & _
                     "FROM " & _
                      "users u " & _
                      "INNER JOIN groups g " & _
                       "ON u.usergroup = g.groupid " & _
                       "INNER JOIN users u1 ON u1.userid = u.createdby " & _
                        "WHERE u.deldate is null " & WhereClause()




            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





            Return objCommand.ExecScalar


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

    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pUserId <> "" Then
            lswhere = lswhere & "AND u.userid like '%" & Replace(pUserId, "'", "''") & "%' "
        End If
        If pUserLogin <> "" Then
            lswhere = lswhere & "AND u.userlogin like '%" & Replace(pUserLogin, "'", "''") & "%' "
        End If
        If pFirstName <> "" Then
            lswhere = lswhere & "AND u.firstname like '%" & Replace(pFirstName, "'", "''") & "%' "
        End If

        If pLastName <> "" Then
            lswhere = lswhere & "AND u.lastname like '%" & Replace(pLastName, "'", "''") & "%' "
        End If

        If pUserEmail <> "" Then
            lswhere = lswhere & "AND u.email like '%" & Replace(pUserEmail, "'", "''") & "%' "
        End If

        If pGroup <> "" Then
            lswhere = lswhere & "AND u.usergroup = '" & Replace(pGroup, "'", "''") & "' "
        End If

        If pRole <> "" Then
            lswhere = lswhere & "AND u.userrole = '" & Replace(pRole,"'","''") & "' "

        End If
        Return lswhere
    End Function

    Private Function OrderBy() As String
        If pSortCol = "User ID" Then
            Return "u.userid"
        ElseIf pSortCol = "User Login" Then
            Return "u.userlogin"
        ElseIf pSortCol = "First Name" Then
            Return "u.firstname"
        ElseIf pSortCol = "Email" Then
            Return "u.email"
        ElseIf pSortCol = "Last Name" Then
            Return "u.lastname"
        ElseIf pSortCol = "Group" Then
            Return "g.groupname"
        ElseIf pSortCol = "Role" Then
            Return "u.userrole"
        Else
            Return "u.userlogin"
        End If
        
    End Function

    Public Function RetrieveUserInfo() As DataTable

        Dim objCommand As clsSqlConn

        Dim ldt As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "SELECT " & _
                      "u.userid,u.userlogin,u.password, " & _
                      "u.locked, " & _
                      "u.firstname, " & _
                      "u.lastname, " & _
                      "u.email, " & _
                      "u.userrole, " & _
                      "u.usergroup, "
            If DocSession.OraClient Then
                s_sql = s_sql & "TO_CHAR(u.passexpirationdate,'mm/dd/yyyy') AS expiry, " & _
                      " NVL(u.profilePic,'default.png') AS profilePic, " & _
                      "NVL(u.firsttimeuser,0) AS firsttimeuser, " & _
                      "NVL(case when g.reportaccess = 1 then '1' else '0' end,'0') AS reportaccess, " & _
                      "NVL(case when g.alwaysallowed = 1 then '1' else '0' end,'0') AS alwaysallowed "
            Else
                s_sql = s_sql & "convert(char(10),u.passexpirationdate,101) AS expiry, " & _
                      " isnull(u.profilePic,'default.png') AS profilePic, " & _
                      "isnull(u.firsttimeuser,0) AS firsttimeuser, " & _
                      "isnull(case when g.reportaccess = 1 then '1' else '0' end,'0') AS reportaccess, " & _
                      "isnull(case when g.alwaysallowed = 1 then '1' else '0' end,'0') AS alwaysallowed "

            End If
            

            s_sql = s_sql & "FROM " & _
                      "users u inner join groups g " & _
                      "on u.usergroup = g.groupid " & _
                     "WHERE " & _
                      "(u.userid = '" & Replace(pUserId, "'", "''") & "') "
            
            objCommand.CommandText = s_sql
            ldt = objCommand.Fill()
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
    Public Function GetUserProfile()

        Dim objCommand As clsSqlConn

        Dim ldt As DataTable
        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            Dim s_sql As String = "SELECT " & _
                      "u.userid,u.userlogin,u.firstname,u.middlename,u.lastname,u.usergroup,g.groupname,u.userrole,o.Description as Office, " & _
                      "(case u.userrole when 'A' then 'Admin' when 'G' then 'Group Officer' when 'R' then 'Report Generator' when 'L' then 'Archiving' when 'D' then 'Super User' else 'User' end) AS urole, " & _
                      "(case u.canchangepass when 1 then 'Yes' else 'No' end) AS canchangepass, " & _
                      "(case u.TurnOffEmailNotification when 1 then 'Yes' else 'No' end) AS emailnotificationoff, " & _
                      "u.title,u.email, "

            If DocSession.OraClient Then
                s_sql = s_sql & " NVL(u.profilepic,'default.png') AS profilepic "
            Else
                s_sql = s_sql & " isnull(u.profilepic,'default.png') AS profilepic "
            End If


            s_sql = s_sql & "FROM " & _
             "users u " & _
             "INNER JOIN groups g " & _
              "ON u.usergroup = g.groupid " & _
              "INNER JOIN office o " & _
              "ON g.OfficeCode = o.OfficeCode " & _
            "WHERE " & _
             "u.userid = '" & Replace(pUserId, "'", "''") & "' " 'XMSP_USERGETPROFILE"
            'objCommand.ParametersAddWithValue("@Userid", pUserId)
            objCommand.CommandText = s_sql
            ldt = objCommand.Fill()

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

    Public Sub UpdateUser()

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            Dim s_sql As String = "UPDATE Users " &
           "SET " &
           "FirstName = '" & Replace(pFirstName, "'", "''") & "' " &
           ",LastName = '" & Replace(pLastName, "'", "''") & "' " &
           ",MiddleName = '" & Replace(pMiddleName, "'", "''") & "' " &
           ",ModifiedBy = '" & Replace(DocSession.sUserId, "'", "''") & "' " &
           ",ModifiedDat = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " &
           ",Title = '" & Replace(pTitle, "'", "''") & "' " &
           ",Email = '" & Replace(pUserEmail, "'", "''") & "' "

            If pPassword <> "" Then
                s_sql = s_sql & ",Password = '" & Replace(pPassword, "'", "''") & "' "
            End If

            If pLockAttempt <> "" Then
                s_sql = s_sql & ",LockOutAttempts  = '" & pLockAttempt & "' "
            End If


            If pGroup <> "" Then
                s_sql = s_sql & ",UserGroup = '" & Replace(pGroup, "'", "''") & "' "
            End If


            If pRole <> "" Then
                s_sql = s_sql & ",UserRole = '" & pRole & "' "
            End If

            If pUserLogin <> "" Then
                s_sql = s_sql & ",UserLogin = '" & pUserLogin & "' "
            End If

            If pLocked <> "" Then
                s_sql = s_sql & ",Locked = " & pLocked & " "
            End If

            If pExpirationDate <> "" Then
                s_sql = s_sql & ",PassExpirationDate = " & IIf(DocSession.OraClient, "TO_DATE('" & pExpirationDate & "')", "'" & pExpirationDate & "'") & " "
            End If

            If pCanChangePassword <> "" Then
                s_sql = s_sql & ",CanChangePass = " & pCanChangePassword & " "
            End If

            If pTurnOffEmailNotification <> "" Then
                s_sql = s_sql & ",TurnOffEmailNotification = " & pTurnOffEmailNotification & " "
            End If

            If pProfilePic <> "" Then
                s_sql = s_sql & ",profilepic = '" & pProfilePic & "' "
            End If
            If pFirstTimeuser <> "" Then
                s_sql = s_sql & ",FirstTimeUser = " & pFirstTimeuser & " "
            End If
            If pDelDate = "0" Then
                s_sql = s_sql & ",Deldate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " "
            ElseIf pDelDate = "1" Then
                s_sql = s_sql & ",Deldate = null "
            End If


            s_sql = s_sql & "WHERE " &
                  "UserId = '" & pUserId & "' "

            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()
            Dim oHist As New DocHistory
            Dim lsDate As String = DateTime.Now.ToString

            If pExpirationDate <> "" AndAlso CDate(pExpirationDate) <> CDate(pExpirationDate_o) Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Password Expiration"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pExpirationDate_o
                oHist.pNewValue = pExpirationDate
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pLockAttempt <> "" AndAlso pLockAttempt <> pLockAttempt_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Lockout Attempt"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pLockAttempt_o
                oHist.pNewValue = pLockAttempt
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pLocked <> "" AndAlso pLocked <> pLocked_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Locked"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = IIf(pLocked_o = "1", "Yes", "No")
                oHist.pNewValue = IIf(pLocked = "1", "Yes", "No")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pTitle <> "" AndAlso pTitle <> pTitle_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Title"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pTitle_o
                oHist.pNewValue = pTitle
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pUserEmail <> "" AndAlso pUserEmail <> pUserEmail_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Email"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pUserEmail_o
                oHist.pNewValue = pUserEmail
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pProfilePic <> "" AndAlso pProfilePic <> pProfilePic_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Profile Picture"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pProfilePic_o
                oHist.pNewValue = pProfilePic
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pCanChangePassword <> "" AndAlso pCanChangePassword <> pCanChangePassword_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Can Change Password"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = IIf(pCanChangePassword_o = "1", "Yes", "No")
                oHist.pNewValue = IIf(pCanChangePassword = "1", "Yes", "No")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pGroup <> "" AndAlso pGroup <> pGroup_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Group"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = Replace(pGroup_o, "'", "''")
                oHist.pNewValue = Replace(pGroup, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pRole <> "" AndAlso pRole <> pRole_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Role"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pRole_o
                oHist.pNewValue = pRole
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pLastName <> "" AndAlso pLastName <> pLastName_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Last Name"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = Replace(pLastName_o, "'", "''")
                oHist.pNewValue = Replace(pLastName, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pFirstName <> "" AndAlso pFirstName <> pFirstName_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "First Name"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = Replace(pFirstName_o, "'", "''")
                oHist.pNewValue = Replace(pFirstName, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pPassword <> "" AndAlso pPassword <> pPassword_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Password"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = "****"
                oHist.pNewValue = "Updated Password"
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pUserLogin <> "" AndAlso pUserLogin <> pUserLogin_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Password"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = Replace(pUserLogin_o, "'", "''")
                oHist.pNewValue = Replace(pUserLogin, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            If pDelDate <> "" AndAlso pDelDate <> pDelDate_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Active"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = Replace(pDelDate_o, "'", "''")
                oHist.pNewValue = Replace(pDelDate, "'", "''")
                oHist.pIpAddress = pIPAddress
                oHist.LogChanges(objCommand)
            End If
            ltr.pTran.Commit()
        Catch ex As Exception
            ltr.pTran.Rollback()

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
    Public Sub UnlockDeactivateUpdateExpiryUser(asJRFNo As String)

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            Dim s_sql As String = "UPDATE Users " &
           "SET "

            If pLocked <> "" Then
                s_sql = s_sql & " Locked = " & pLocked & " "
            End If

            If pExpirationDate <> "" Then
                s_sql = s_sql & " PassExpirationDate = " & IIf(DocSession.OraClient, "TO_DATE('" & pExpirationDate & "')", "'" & pExpirationDate & "'") & " "
            End If

            s_sql = s_sql & "WHERE " &
                  "UserId = '" & pUserId & "' "

            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()
            Dim oHist As New DocHistory
            Dim lsDate As String = DateTime.Now.ToString

            If pExpirationDate <> "" AndAlso CDate(pExpirationDate) <> CDate(pExpirationDate_o) Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Update Password Expiration"
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = pExpirationDate_o
                oHist.pNewValue = pExpirationDate
                oHist.pIpAddress = pIPAddress
                oHist.pJRFNo = asJRFNo
                oHist.LogChanges(objCommand)
            End If


            If pLocked <> "" AndAlso pLocked <> pLocked_o Then
                oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
                oHist.pColumnName = "Unlock User - JRF: " & asJRFNo
                oHist.pModifiedDate = lsDate
                oHist.pOldValue = IIf(pLocked_o = "1", "Yes", "No")
                oHist.pNewValue = IIf(pLocked = "1", "Yes", "No")
                oHist.pIpAddress = pIPAddress
                oHist.pJRFNo = asJRFNo
                oHist.LogChanges(objCommand)
            End If
            'If pCanChangePassword <> "" AndAlso pCanChangePassword <> pCanChangePassword_o Then
            '    oHist.pTableName = "Users"
            '    oHist.pRecordId = Replace(pUserId, "'", "''")
            '    oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            '    oHist.pColumnName = "Can Change Password - JRF: " & asJRFNo
            '    oHist.pModifiedDate = lsDate
            '    oHist.pOldValue = IIf(pCanChangePassword_o = "1", "Yes", "No")
            '    oHist.pNewValue = IIf(pCanChangePassword = "1", "Yes", "No")
            '    oHist.pIpAddress = pIPAddress
            '    oHist.LogChanges(objCommand)
            'End If
            ltr.pTran.Commit()
        Catch ex As Exception
            ltr.pTran.Rollback()

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

    Public Sub DeactivateUser(asJRFNo As String)

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            Dim s_sql As String = "UPDATE Users " &
           "SET deldate = '" & pDelDate & "' "


            s_sql = s_sql & "WHERE " &
                  "UserId = '" & pUserId & "' "

            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()
            Dim oHist As New DocHistory
            Dim lsDate As String = DateTime.Now.ToString


            oHist.pTableName = "Users"
                oHist.pRecordId = Replace(pUserId, "'", "''")
                oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = "Deactivate User"
            oHist.pModifiedDate = lsDate
            oHist.pOldValue = ""
            oHist.pNewValue = pDelDate
            oHist.pIpAddress = pIPAddress
            oHist.pJRFNo = asJRFNo
            oHist.LogChanges(objCommand)


            ltr.pTran.Commit()
        Catch ex As Exception
            ltr.pTran.Rollback()

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

    Public Sub AddInactiveUser()
        'Dim ec As New crypt
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try

            Dim s_sql As String = ReactivateUser()

            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_USERADD"

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory

            oHist.pTableName = "Users"
            oHist.pRecordId = Replace(pUserId, "'", "''")
            oHist.pModifiedBy = DocSession.sUserId
            oHist.pColumnName = " "
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = " "
            oHist.pNewValue = "Added User (" & Replace(pLastName, "'", "''") & ", " & Replace(pFirstName, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)






            ltr.pTran.Commit()

        Catch ex As Exception
            ltr.pTran.Rollback()
            Throw New Exception(ex.Message)

        Finally
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub
    Public Function ReactivateUser() As String

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        
        Dim s_sql As String = "UPDATE Users " & _
       "SET " & _
       "FirstName = '" & Replace(pFirstName, "'", "''") & "' " & _
       ",LastName = '" & Replace(pLastName, "'", "''") & "' " & _
       ",MiddleName = '" & Replace(pMiddleName, "'", "''") & "' " & _
       ",ModifiedBy = '" & Replace(DocSession.sUserId, "'", "''") & "' " & _
       ",ModifiedDat = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
       ",Title = '" & Replace(pTitle, "'", "''") & "' " & _
       ",Email = '" & Replace(pUserEmail, "'", "''") & "' " & _
        ",DelDate = Null "

        If pPassword <> "" Then
            s_sql = s_sql & ",Password = '" & Replace(pPassword, "'", "''") & "' "
        End If

        If pUserLogin <> "" Then
            s_sql = s_sql & ",UserLogin = '" & Replace(pUserLogin, "'", "''") & "' "
        End If

        If pLockAttempt <> "" Then
            s_sql = s_sql & ",LockOutAttempts  = '" & pLockAttempt & "' "
        End If


        If pGroup <> "" Then
            s_sql = s_sql & ",UserGroup = '" & Replace(pGroup, "'", "''") & "' "
        End If


        If pRole <> "" Then
            s_sql = s_sql & ",UserRole = '" & pRole & "' "
        End If

        If pLocked <> "" Then
            s_sql = s_sql & ",Locked = " & pLocked & " "
        End If

        If pExpirationDate <> "" Then
            s_sql = s_sql & ",PassExpirationDate = " & IIf(DocSession.OraClient, "TO_DATE('" & pExpirationDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & pExpirationDate & "'") & " "
        End If



        If pCanChangePassword <> "" Then
            s_sql = s_sql & ",CanChangePass = " & pCanChangePassword & " "
        End If
        If pProfilePic <> "" Then
            s_sql = s_sql & ",profilepic = '" & pProfilePic & "' "
        End If
        If pFirstTimeuser <> "" Then
            s_sql = s_sql & ",FirstTimeUser = " & pFirstTimeuser & " "
        End If


        s_sql = s_sql & "WHERE " & _
                "UserId = '" & pUserId & "' "

        Return s_sql


    End Function

    Public Sub AddUser(Optional ByVal asJRF As String = "")

        'Dim ec As New crypt
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try

            Dim s_sql As String = "INSERT INTO Users " &
           "(UserId,FirstName,LastName,MiddleName,UserGroup,UserRole,Password,Locked,PassExpirationDate,CanChangePass,CreatedBy " &
           ",CreatedDate,ModifiedBy,ModifiedDat,Title,LockOutAttempts,Email,FirstTimeUser,UserLogin,DelDate) " &
     "VALUES " &
           "('" & Replace(pUserId, "'", "''").ToUpper() & "','" & Replace(pFirstName, "'", "''") & "','" & Replace(pLastName, "'", "''") & "','" & pMiddleName & "','" & Replace(pGroup, "'", "''").ToUpper & "','" & pRole.ToUpper & "','" & Replace(pPassword, "'", "''") & "',0," & IIf(DocSession.OraClient, "TO_DATE('" & pExpirationDate & "','mm/dd/yyyy')", "'" & pExpirationDate & "'") & " " &
           "," & pCanChangePassword & ",'" & Replace(pLoginUserId, "'", "''") & "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",'" & Replace(pLoginUserId, "'", "''") &
           "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",'" & Replace(pTitle, "'", "''") & "','" & pLockAttempt & "','" & Replace(pUserEmail, "'", "''").ToLower & "'," & pFirstTimeuser & ",'" & Replace(pUserLogin, "'", "''") & "','" & pDelDate & "') "

            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_USERADD"

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory

            oHist.pTableName = "Users"
            oHist.pRecordId = Replace(pUserId, "'", "''")
            oHist.pModifiedBy = DocSession.sUserId
            oHist.pColumnName = "Add User"
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = " "
            oHist.pNewValue = "Added User (" & Replace(pLastName, "'", "''") & ", " & Replace(pFirstName, "'", "''") & ")"


            oHist.pIpAddress = pIPAddress
            oHist.pJRFNo = asJRF
            oHist.LogChanges(objCommand)

            ltr.pTran.Commit()

        Catch ex As Exception
            ltr.pTran.Rollback()
            Throw New Exception(ex.Message)

        Finally
            If Not ltr Is Nothing Then
                ltr.Dispose()
                ltr = Nothing
            End If
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Sub



    Public Sub DeleteUser()


        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            Dim s_sql As String = "DELETE FROM Users WHERE userid = '" & Replace(pUserId, "'", "''") & "'"
            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql


            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory

            oHist.pTableName = "Users"
            oHist.pRecordId = Replace(pUserId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = " "
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = " "
            oHist.pNewValue = "Deleted User (" & Replace(pLastName, "'", "''") & ", " & Replace(pFirstName, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)
            ltr.pTran.Commit()
        Catch ex As Exception
            ltr.pTran.Rollback()
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

    Public Sub DeleteUser(ByVal objCommand As clsSqlConn)

        Dim ec As New crypt

        Try
            Dim s_sql As String = "UPDATE Users SET delDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ",ModifiedBy='" & DocSession.sUserId & "' WHERE userid = '" & Replace(pUserId, "'", "''") & "'"

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory

            oHist.pTableName = "Users"
            oHist.pRecordId = Replace(pUserId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = " "
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = " "
            oHist.pNewValue = "Deleted User (" & Replace(pLastName, "'", "''") & ", " & Replace(pFirstName, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally


        End Try
    End Sub

    Public Sub Merge2User()
        Dim lsSQL As String = " update DocAttachment set Attachedby = '" & pMergeUser & "' where attachedby = '" & pSourceUser & "'"

        lsSQL = lsSQL & " delete from docbookmark where userid = '" & pSourceUser & "'"
        lsSQL = lsSQL & " delete from docbookmark_hist where userid = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocChanges set Modifiedby = '" & pMergeUser & "' where Modifiedby = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocChanges set Deletedby = '" & pMergeUser & "' where Deletedby = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocFileVersion set Uploadedby = '" & pMergeUser & "' where Uploadedby = '" & pSourceUser & "'"
        lsSQL = lsSQL & " update DocFileVersion_hist set Uploadedby = '" & pMergeUser & "' where Uploadedby = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocHistory set UserId = '" & pMergeUser & "' where UserId = '" & pSourceUser & "'"
        lsSQL = lsSQL & " update DocHistory set ApproverId = '" & pMergeUser & "' where ApproverId = '" & pSourceUser & "'"

        'lsSQL = lsSQL & " update DocHistory_hist set UserId = '" & pMergeUser & "' where UserId = '" & pSourceUser & "'"
        'lsSQL = lsSQL & " update DocHistory_hist set ApproverId = '" & pMergeUser & "' where ApproverId = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocInbox set SendBy = '" & pMergeUser & "' where SendBy = '" & pSourceUser & "'"
        lsSQL = lsSQL & " update DocInbox set UserId = '" & pMergeUser & "' where UserId = '" & pSourceUser & "' " & _
         " and docid not in (select docid from docinbox dl where userid = '" & pMergeUser & "') "

        lsSQL = lsSQL & " delete from docInbox where userid = '" & pSourceUser & "'"

        'doclinks

        lsSQL = lsSQL & " update DoclIst set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DoclIst set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DoclIst set ApprovedBy =  '" & pMergeUser & "' where ApprovedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocList set PrintedBy =  '" & pMergeUser & "' where PrintedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocNotes set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocNotes set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocNotes set DeletedBy =  '" & pMergeUser & "' where DeletedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocReceipts set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocReceipts set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocReply set PrintedBy =  '" & pMergeUser & "' where PrintedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocRouting set ApproverId =  '" & pMergeUser & "' where ApproverId = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocRouting set Sender =  '" & pMergeUser & "' where Sender = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocStatus set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocStatus set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocTags set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocTags set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocTags set DeletedBy =  '" & pMergeUser & "' where DeletedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocType set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update DocType set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update Groups set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update Groups set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update Groups set DeletedBy =  '" & pMergeUser & "' where DeletedBy = '" & pSourceUser & "'"
        lsSQL = lsSQL & " update GroupStatus set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update GroupStatus set ModifiedBy =  '" & pMergeUser & "' where ModifiedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update GroupWorkHoliday set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " delete from LegalMailContent where userid = '" & pSourceUser & "'"

        lsSQL = lsSQL & " delete from RecipientList where userid = '" & pSourceUser & "'"

        'lsSQL = lsSQL & " delete from docChanges where userid = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update UserFolder set UserId = '" & pMergeUser & "' where UserId = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update Users set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update users set deldate = getdate() where UserId = '" & pSourceUser & "'"

        lsSQL = lsSQL & " update WorkSchedule set CreatedBy = '" & pMergeUser & "' where CreatedBy = '" & pSourceUser & "'"
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            ltr = New DbTran

            objCommand = New clsSqlConn(ltr.pTran)
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = lsSQL

            objCommand.ExecTranNonQuery()

            Dim oHist As New DocHistory

            oHist.pTableName = "Users"
            oHist.pRecordId = Replace(pUserId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = " "
            oHist.pModifiedDate = DateTime.Now.ToString
            oHist.pOldValue = " "
            oHist.pNewValue = "Merged User (" & Replace(pSourceUserName, "'", "''") & " to " & Replace(pMergeUserName, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)
            ltr.pTran.Commit()
        Catch ex As Exception
            ltr.pTran.Rollback()
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

    Public Function ExistsInPwdHist() As Boolean

        Dim objCommand As clsSqlConn
        Dim ldt As New DataTable
        Dim lrow() As DataRow
        Dim keys(1) As DataColumn
        Dim column As DataColumn
        Dim blnReturn As Boolean = False

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            Dim strPwdHist As String = System.Configuration.ConfigurationManager.AppSettings("PasswordHistory")
            Dim s_sql As String = ""
            s_sql = "SELECT P.* " & _
                    "FROM USERS U " & _
                    "INNER JOIN (SELECT RN = ROW_NUMBER() OVER (ORDER BY T.CREATEDDATE DESC), * " & _
                                "FROM (SELECT USERID, CREATEDDATE, PASSWORD  FROM PASSWORDTRACK WHERE USERID='" & pUserId & "' )T " & _
                                ")P ON P.USERID = U.USERID " & _
                    "WHERE U.USERID = '" & pUserId & "' " & _
                    "AND P.RN <= " & strPwdHist

            objCommand.CommandText = s_sql
            ldt = objCommand.Fill()

            If IsNothing(ldt) OrElse ldt.Rows.Count = 0 Then
                'strPwdHist = "0"
                GoTo ExitFunction
            Else
                'strPwdHist = ldt(0)("PwdHist")

                column = ldt.Columns.Item("Password")
                keys(0) = column

                ldt.PrimaryKey = keys

                lrow = ldt.Select("Password='" & pPassword & "'")
                If Not lrow Is Nothing AndAlso lrow.Length > 0 Then
                    blnReturn = True
                End If
            End If
ExitFunction:
            Return blnReturn
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
    Public Sub AddPasswordTrack()
        Dim objCommand As clsSqlConn

        Try

            Dim s_sql As String = "INSERT INTO PasswordTrack (UserID,CreatedDate,Password) " & _
            "VALUES ('" & Replace(pUserId, "'", "''") & "','" & DateTime.Now & "','" & Replace(pPassword, "'", "''") & "') "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_USERADD"

            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Sub
End Class
