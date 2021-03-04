Imports System.Web.HttpContext

Public Class DocSession
    '01/17/2014
    'Public Shared Function NVL(ByVal asCol As String, ByVal asAlias As String) As String
    '    If DocSession.OraClient Then
    '        Return "NVL(" & asCol & ",' ') as " & asAlias
    '    Else
    '        Return "ISNULL(" & asCol & ",'') as " & asAlias
    '    End If
    'End Function

    Public Shared Function IsTrue(ByVal asVal As String) As Boolean
        If asVal.Trim = "True" OrElse asVal.Trim = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function InsertDate(ByVal asVal As String) As String
        Return IIf(DocSession.OraClient, "TO_DATE('" & asVal & "','mm/dd/yyyy hh:mi:ss am')", "'" & asVal & "'")
    End Function
    Public Shared Function CurrentDate() As String
        Return IIf(DocSession.OraClient, "SYSDATE", "GETDATE()")
    End Function
    Public Shared Function CTrue(ByVal asVal As String) As String
        If asVal.Trim = "True" OrElse asVal.Trim = "1" Then
            Return "True"
        Else
            Return "False"
        End If

    End Function
    Public Shared Function DocClosed(ByVal aiStatusId As Int16) As Boolean
        If aiStatusId = 12 OrElse aiStatusId = 5 OrElse aiStatusId = 8 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetDocHierarchy(ByVal asStatusId As Integer) As Integer
        Using ldata As DataTable = DocSession.sDocHierarchy
            ldata.TableName = "DocStatus"
            Dim lrow() As DataRow
            Dim keys(1) As DataColumn
            Dim column As DataColumn
            column = ldata.Columns.Item("StatusId")

            'column.DataType = System.Type.GetType("System.String")
            'column.ColumnName = "StatusId"
            keys(0) = column

            ldata.PrimaryKey = keys

            lrow = ldata.Select("StatusId=" & asStatusId)
            If Not lrow Is Nothing Then
                Return CInt(lrow(0).Item("Hierarchy"))
            Else
                Return 1
            End If
        End Using

    End Function
    Public Shared Sub IsExpired()
        If Not HttpContext.Current.Session Is Nothing Then
            If HttpContext.Current.Session.IsNewSession Then
                Dim lsCook As String = HttpContext.Current.Request.Headers("cookie")
                If (Not lsCook Is Nothing AndAlso lsCook.IndexOf("ASP.NET_SessionId") >= 0) Then
                    'msg.Text = "Session expired. Please click reload or refresh button of the browser before signing in."
                    'lbOk = False
                    HttpContext.Current.Response.Redirect("Login.aspx")
                End If
            End If
            If DocSession.sUserId Is Nothing Or DocSession.sUserId = "" Then
                HttpContext.Current.Response.Redirect("login.aspx")
            End If
        End If
    End Sub
    Public Shared Sub IsAdmin()
        IsExpired()
        If DocSession.sUserRole <> "A" Then
            HttpContext.Current.Response.Redirect("accessdenied.aspx")
        End If
    End Sub
    Public Shared Function GetDocHierarchy(ByVal asStatusId As String) As Integer
        Using ldata As DataTable = DocSession.sDocHierarchy
            ldata.TableName = "DocStatus"
            Dim lrow() As DataRow
            Dim keys(1) As DataColumn
            Dim column As DataColumn
            column = ldata.Columns.Item("StatusId")

            'column.DataType = System.Type.GetType("System.String")
            'column.ColumnName = "StatusId"
            keys(0) = column

            ldata.PrimaryKey = keys

            lrow = ldata.Select("StatusId=" & asStatusId)
            If Not lrow Is Nothing Then
                Return CInt(lrow(0).Item("Hierarchy"))
            Else
                Return 1
            End If
        End Using

    End Function
    Public Shared Function CheckNextID(ByVal asColId As String) As String
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT nextValue FROM NextValue Where colid = '" & asColId & "'"
            'If Not objCommand.ExecHasRow Then
            'objCommand.CommandText = "INSERT INTO nextvalue (colid,nextvalue) VALUES('" & asColId & "',0)"
            Return objCommand.ExecScalar
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Shared Sub UpdateNextID(ByVal asColId As String, ByVal asVal As String)
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "UPDATE nextValue SET NextValue='" & asVal & "' Where colid = '" & asColId & "'"
            'If Not objCommand.ExecHasRow Then
            'objCommand.CommandText = "INSERT INTO nextvalue (colid,nextvalue) VALUES('" & asColId & "',0)"
            objCommand.ExecNonQuery()
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Sub

    Public Shared Function GenerateRefNo(ByVal asOfficeCode As String, ByVal asNumber As String) As String
        Return Year(DateTime.Now).ToString & "-" & asOfficeCode.Trim & "-" & Right("0000000" & asNumber, 7)
    End Function

    Public Shared Function getNextID(ByVal asColId As String) As String
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT nextValue FROM NextValue Where colid = '" & asColId & "'"
            If objCommand.ExecHasRow Then
                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "UPDATE NextValue SET nextvalue = nextvalue + 1 WHERE colid = '" & asColId & "'"
                objCommand.ExecNonQuery()
            Else
                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "INSERT INTO NextValue(colid,nextvalue) VALUES ('" & asColId & "',1)"
                objCommand.ExecNonQuery()
            End If

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT nextValue FROM NextValue Where colid = '" & asColId & "'"
            Return objCommand.ExecScalar

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Shared Function AddNextID(ByVal asColId As String, ByVal asVal As String) As String
        Dim objCommand As clsSqlConn

        Try

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "INSERT INTO NextValue(colid,nextvalue) VALUES ('" & asColId & "'," & asVal & ")"
            objCommand.ExecNonQuery()
            Return "Ok"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Shared Property sfilter As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_sfilter") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_sfilter").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_sfilter")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property DocumentPage As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_DocPage") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_DocPage").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_DocPage")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property sDashboardSql As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_sDashboardSql") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_sDashboardSql").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_sDashboardSql")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property
    Public Shared Property sRegType As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_sregtype") Is Nothing Then
                Return CStr(Current.Session("s_sregtype"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_sregtype") = value
        End Set
    End Property

    Public Shared Property sRegType2 As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_sregtype2") Is Nothing Then
                Return CStr(Current.Session("s_sregtype2"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_sregtype2") = value
        End Set
    End Property

    Public Shared Property sRegDemo As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_sRegDemo") Is Nothing Then
                Return CStr(Current.Session("s_sRegDemo"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_sRegDemo") = value
        End Set
    End Property
    Public Shared Property sRegKey As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_sRegKey") Is Nothing Then
                Return CStr(Current.Session("s_sRegKey"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_sRegKey") = value
        End Set
    End Property
    Public Shared Property sSelectedTab As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_SelectedTab") Is Nothing Then
                Return CStr(Current.Session("s_SelectedTab"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_SelectedTab") = value
        End Set
    End Property

    Public Shared Property sDocTitle As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocTitle") Is Nothing Then
                Return CStr(Current.Session("s_DocTitle"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocTitle") = value
        End Set
    End Property

    Public Shared Property sVersion As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_Version") Is Nothing Then
                Return CStr(Current.Session("s_Version"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_Version") = value
        End Set
    End Property

    Public Shared Property sCheckout As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_Checkout") Is Nothing Then
                Return CStr(Current.Session("s_Checkout"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_Checkout") = value
        End Set
    End Property

    Public Shared Property sUserId As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_userId") Is Nothing Then
                Return CStr(Current.Session("s_userId"))
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_userId") = value
        End Set
    End Property
    Public Shared Property sUserLogin As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_userlogin") Is Nothing Then
                Return CStr(Current.Session("s_userlogin"))
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_userlogin") = value
        End Set
    End Property
    Public Shared ReadOnly Property docDisable As Boolean
        Get
            If DocSession.Archived OrElse CInt(DocSession.sDocTypeAccess) <= 1 OrElse _
                (DocSession.sCheckout = "Yes" AndAlso DocSession.sCheckOutBy <> DocSession.sUserId) Then
                If DocSession.sUserRole = "A" Then
                    Return False
                Else
                    Return True
                End If

            Else
                Return False
            End If

        End Get
    End Property

    Public Shared Property sUploaderOfc As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_UploaderOfc") Is Nothing Then
                Return CStr(Current.Session("s_UploaderOfc"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_UploaderOfc") = value
        End Set
    End Property
    Public Shared Property sOfcCode As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ofccode") Is Nothing Then
                Return CStr(Current.Session("s_ofccode"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ofccode") = value
        End Set
    End Property
    Public Shared Property sOfcName As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ofcname") Is Nothing Then
                Return CStr(Current.Session("s_ofcname"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ofcname") = value
        End Set
    End Property
    Public Shared Property sDocOfcCode As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocOfcCode") Is Nothing Then
                Return CStr(Current.Session("s_DocOfcCode"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocOfcCode") = value
        End Set
    End Property
    'Public Shared Property sArchived As String
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("s_Archived") Is Nothing Then
    '            Return CStr(Current.Session("s_Archived"))
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("s_Archived") = value
    '    End Set
    'End Property

    
    'Public Shared Property sDeleteDoc As String
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("s_DeleteDoc") Is Nothing Then
    '            Return CStr(Current.Session("s_DeleteDoc"))
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("s_DeleteDoc") = value
    '    End Set
    'End Property
    Public Shared Property sPasswordExpiry As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_PasswordExpiry") Is Nothing Then
                Return CStr(Current.Session("s_PasswordExpiry"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_PasswordExpiry") = value
        End Set
    End Property

    

    Public Shared Property GroupLogo As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_GroupLogo") Is Nothing Then
                Return CStr(Current.Session("s_GroupLogo"))
            Else
                Return "dbm.png"
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_GroupLogo") = value
        End Set
    End Property

    Public Shared Property ReceiptReplyName As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ReceiptReplyName") Is Nothing Then
                Return CStr(Current.Session("s_ReceiptReplyName"))
            Else
                Return "DEPARTMENT OF BUDGET AND MANAGEMENT"
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ReceiptReplyName") = value
        End Set
    End Property
    Public Shared Property sFirstTimeUser As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_FirstTimeUser") Is Nothing Then
                Return CStr(Current.Session("s_FirstTimeUser"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_FirstTimeUser") = value
        End Set
    End Property

    Public Shared Property sprofilePic As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_profilePic") Is Nothing Then
                Return CStr(Current.Session("s_profilePic"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_profilePic") = value
        End Set
    End Property

    Public Shared Property sUserName As String
        Get
            Try
                If Current IsNot Nothing AndAlso Current.Session("s_username") IsNot Nothing Then
                    Return CStr(Current.Session("s_userName"))
                Else
                    Return ""
                End If

            Catch ex As Exception
                'Server.ClearError()
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            Current.Session("s_userName") = value
        End Set
    End Property

    Public Shared Property EmailNotification As String
        Get
            Try
                If Current IsNot Nothing AndAlso Current.Session("s_EmailNotification") IsNot Nothing Then
                    Return CStr(Current.Session("s_EmailNotification"))
                Else
                    Return ""
                End If

            Catch ex As Exception
                'Server.ClearError()
                Return ""
            End Try
        End Get
        Set(ByVal value As String)
            Current.Session("s_EmailNotification") = value
        End Set
    End Property

    Public Shared ReadOnly Property RDSColumn As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("RDSColumn")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return "date received by agency"
            End If

        End Get
    End Property

    Public Shared ReadOnly Property NCAColumn As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("ncacolumnname")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return "NCA No"
            End If

        End Get
    End Property
    Public Shared ReadOnly Property IssuancesDocType As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("IssuancesDocType")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return "DBMIssuances"
            End If

        End Get
    End Property
    Public Shared ReadOnly Property HideConfidential As Boolean
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("HideConf")
            If lsCol = "1" Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property
    Public Shared ReadOnly Property CompleteStatus As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("complete")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return ""
            End If

        End Get
    End Property
    Public Shared ReadOnly Property SAROColumn As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("sarocolumnname")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return "SARO No"
            End If

        End Get
    End Property
    Public Shared ReadOnly Property BOXColumn As String
        Get
            Dim lsCol As String = System.Configuration.ConfigurationManager.AppSettings("boxcolumnname")
            If Not lsCol Is Nothing AndAlso lsCol <> "" Then
                Return lsCol
            Else
                Return "Box No"
            End If

        End Get
    End Property
    Public Shared Property sUserEmail As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_userEmail") Is Nothing Then
                Return CStr(Current.Session("s_userEmail"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_userEmail") = value
        End Set
    End Property

    Public Shared Property sUserGroup As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_userGroup") Is Nothing Then
                Return CStr(Current.Session("s_userGroup"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_userGroup") = value
        End Set
    End Property

    Public Shared Property sGroupAddress As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_grpAddress") Is Nothing Then
                Return CStr(Current.Session("s_grpAddress"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_grpAddress") = value
        End Set
    End Property

    Public Shared Property sUserRole As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_userRole") Is Nothing Then
                Return CStr(Current.Session("s_userRole"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_userRole") = value
        End Set
    End Property

    Public Shared Property sDocHierarchy As DataTable
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocHierarchy") Is Nothing Then
                Return DirectCast(Current.Session("s_DocHierarchy"), DataTable)
            Else
                Return New DataTable
            End If
        End Get
        Set(ByVal value As DataTable)
            Current.Session("s_DocHierarchy") = value
        End Set
    End Property

    Public Shared Property sParentDocID As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ParentDocID") Is Nothing Then
                Return CStr(Current.Session("s_ParentDocID"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ParentDocID") = value
        End Set
    End Property

    Public Shared Property sDocID As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocID") Is Nothing Then
                Return CStr(Current.Session("s_DocID"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocID") = value
        End Set
    End Property
    Public Shared Property sFolderDesc As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_FolderDesc") Is Nothing Then
                Return CStr(Current.Session("s_FolderDesc"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_FolderDesc") = value
        End Set
    End Property
    Public Shared Property sFolderID As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_FolderID") Is Nothing Then
                Return CStr(Current.Session("s_FolderID"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_FolderID") = value
        End Set
    End Property

    Public Shared Property sDocStatus As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocStatus") Is Nothing Then
                Return CStr(Current.Session("s_DocStatus"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocStatus") = value
        End Set
    End Property

    'Public Shared ReadOnly Property ArchivedDoc As Boolean
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("s_DocStatus") Is Nothing Then
    '            Return IIf(CStr(Current.Session("s_DocStatus")) = "8" OrElse CStr(Current.Session("s_DocStatus")) = "5", True, False)
    '        Else
    '            Return False
    '        End If
    '    End Get
    'End Property

    Public Shared ReadOnly Property Archived As Boolean
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocStatus") Is Nothing Then
                'Return IIf(CStr(Current.Session("s_DocStatus")) = "8" OrElse CStr(Current.Session("s_DocStatus")) = "5", True, False)
                Return IIf(CStr(Current.Session("s_DocStatus")) = "8", True, False)
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared Property ArchivedBy As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ArchivedBy") Is Nothing Then
                Return CStr(Current.Session("s_ArchivedBy"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ArchivedBy") = value
        End Set
    End Property

    Public Shared ReadOnly Property Completed As Boolean
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocStatus") Is Nothing Then
                'Return IIf(CStr(Current.Session("s_DocStatus")) = "8" OrElse CStr(Current.Session("s_DocStatus")) = "5", True, False)
                Return IIf(CStr(Current.Session("s_DocStatus")) = "12", True, False)
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Deleted As Boolean
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocStatus") Is Nothing Then
                Return IIf(CStr(Current.Session("s_DocStatus")) = "5", True, False)
            Else
                Return False
            End If
        End Get
    End Property

    Public Shared Property sDocType As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocType") Is Nothing Then
                Return CStr(Current.Session("s_DocType"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocType") = value
        End Set
    End Property

    Public Shared Property sDocTypeAccess As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_DocTypeAccess") Is Nothing Then
                Return CStr(Current.Session("s_DocTypeAccess"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_DocTypeAccess") = value
        End Set
    End Property
    Public Shared Property sReportAccess As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ReportAccess") Is Nothing Then
                Return CStr(Current.Session("s_ReportAccess"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ReportAccess") = value
        End Set
    End Property
    Public Shared Property sEditIndex As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_EditIndex") Is Nothing Then
                'Return CStr(Current.Session("s_EditIndex"))
                'If CStr(Current.Session("s_EditIndex")) = "1" Then
                '    Return "True"
                'ElseIf CStr(Current.Session("s_EditIndex")) = "0" Then
                '    Return "False"
                'Else
                Return CStr(Current.Session("s_EditIndex"))
                'End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_EditIndex") = value
        End Set
    End Property
    Public Shared Property sImportDoc As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_ImportDoc") Is Nothing Then
                'Return CStr(Current.Session("s_ImportDoc"))
                'If CStr(Current.Session("s_ImportDoc")) = "1" Then
                'Return "True"
                'ElseIf CStr(Current.Session("s_ImportDoc")) = "0" Then
                'Return "False"
                'Else
                Return CStr(Current.Session("s_ImportDoc"))
                'End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_ImportDoc") = value
        End Set
    End Property
    Public Shared Property sCanPrint As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_CanPrint") Is Nothing Then
                If CStr(Current.Session("s_CanPrint")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_CanPrint")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_CanPrint"))
                End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_CanPrint") = value
        End Set
    End Property

    Public Shared Property sCanView As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_CanView") Is Nothing Then
                'Return CStr(Current.Session("s_CanView"))
                If CStr(Current.Session("s_CanView")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_CanView")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_CanView"))
                End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_CanView") = value
        End Set
    End Property
    Public Shared Property sCanPrintReceipt As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_CanPrintReceipt") Is Nothing Then
                'Return CStr(Current.Session("s_CanPrintReceipt"))
                If CStr(Current.Session("s_CanPrintReceipt")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_CanPrintReceipt")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_CanPrintReceipt"))
                End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_CanPrintReceipt") = value
        End Set
    End Property

    Public Shared Property sCanDownload As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_CanDownload") Is Nothing Then
                'Return CStr(Current.Session("s_CanDownload"))
                If CStr(Current.Session("s_CanDownload")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_CanDownload")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_CanDownload"))
                End If

            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_CanDownload") = value
        End Set
    End Property

    Public Shared Property sCanDownloadDoc As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_CanDownloadDoc") Is Nothing Then
                'Return CStr(Current.Session("s_CanDownload"))
                If CStr(Current.Session("s_CanDownloadDoc")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_CanDownloadDoc")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_CanDownloadDoc"))
                End If

            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_CanDownloadDoc") = value
        End Set
    End Property

    Public Shared Property sVersionControl As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_VersionControl") Is Nothing Then
                'Return CStr(Current.Session("s_VersionControl"))
                If CStr(Current.Session("s_VersionControl")) = "1" Then
                    Return "True"
                ElseIf CStr(Current.Session("s_VersionControl")) = "0" Then
                    Return "False"
                Else
                    Return CStr(Current.Session("s_VersionControl"))
                End If
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_VersionControl") = value
        End Set
    End Property

    'Public Shared Property sDocFilter As String
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("_sDocFilter") Is Nothing Then
    '            Return CStr(Current.Session("_sDocFilter"))
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("_sDocFilter") = value
    '    End Set
    'End Property

    Public Shared Property sDocFileName As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("sDocFileName") Is Nothing Then
                Return CStr(Current.Session("sDocFileName"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("sDocFileName") = value
        End Set
    End Property
    Public Shared Property sFileName As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("sFileName") Is Nothing Then
                Return CStr(Current.Session("sFileName"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("sFileName") = value
        End Set
    End Property

    Public Shared Property sReferenceNo As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("sReferenceNo") Is Nothing Then
                Return CStr(Current.Session("sReferenceNo"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("sReferenceNo") = value
        End Set
    End Property

    Public Shared Property sCurrentFile As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("sCurrentFile") Is Nothing Then
                Return CStr(Current.Session("sCurrentFile"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("sCurrentFile") = value
        End Set
    End Property

    Public Shared Property sCheckOutBy As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("sCheckOutBy") Is Nothing Then
                Return CStr(Current.Session("sCheckOutBy"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("sCheckOutBy") = value
        End Set
    End Property

    Public Shared Property sDocAccess As Integer
        Get
            'If Current IsNot Nothing AndAlso Not Current.Session("s_DocAccess") Is Nothing Then
            '    Return DirectCast(Current.Session("s_DocAccess"), Integer)
            'Else
            '    Return 1
            'End If
            Return 5
        End Get
        Set(ByVal value As Integer)
            Current.Session("s_DocAccess") = value
        End Set
    End Property

    'Public Shared Property sGuid As String
    '    Get

    '        If Current IsNot Nothing AndAlso Not Current.Session("s_Guid") Is Nothing Then
    '            Return CStr(Current.Session("s_Guid"))
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("s_Guid") = value
    '    End Set
    'End Property

    Public Shared ReadOnly Property FileLoc As String
        Get
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("fileloc")
            If lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "\" AndAlso lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "/" Then
                lsLoc = lsLoc & "\"
            End If
            Return lsLoc
        End Get
    End Property

    Public Shared ReadOnly Property sSendEmail As Boolean
        Get
            Dim lsNoEmail As String = System.Configuration.ConfigurationManager.AppSettings("SendEmail")
            If lsNoEmail = "N" Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property


    Public Shared ReadOnly Property DocLoc As String
        Get
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doclocation")
            If lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "\" AndAlso lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "/" Then
                lsLoc = lsLoc & "\"
            End If
            Return lsLoc
        End Get
    End Property

    Public Shared ReadOnly Property UploadLoc As String
        Get
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("uploadlocation")
            If lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "\" AndAlso lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "/" Then
                lsLoc = lsLoc & "\"
            End If
            Return lsLoc
        End Get
    End Property

    Public Shared ReadOnly Property BackupLoc As String
        Get
            Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("backuploc")
            If lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "\" AndAlso lsLoc.Substring(lsLoc.Trim.Length - 1, 1) <> "/" Then
                lsLoc = lsLoc & "\"
            End If
            Return lsLoc
        End Get
    End Property

    Public Shared Property RowsPerPage As Integer
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_RowsPerPage") Is Nothing Then
                Return CStr(Current.Session("r_RowsPerPage"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As Integer)
            Current.Session("r_RowsPerPage") = value
        End Set
    End Property

    Public Shared Property PageTimeOut As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_PageTimeOut") Is Nothing Then
                Return CStr(Current.Session("r_PageTimeOut"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_PageTimeOut") = value
        End Set
    End Property

    Public Shared ReadOnly Property OraClient As Boolean
        Get
            Dim lsOra As String = System.Configuration.ConfigurationManager.AppSettings("Oracle")
            If lsOra = "1" Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    Public Shared Property AdminEmail As String

        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_AdminEmail") Is Nothing Then
                Return CStr(Current.Session("r_AdminEmail"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_AdminEmail") = value
        End Set
    End Property
    Public Shared ReadOnly Property MaxFileSize As Integer
        Get
            Dim liMaxFileSize As Integer = System.Configuration.ConfigurationManager.AppSettings("MaxFileSize")
            Return liMaxFileSize
        End Get
    End Property

    Public Shared Property rpt_DocType As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_DocType") Is Nothing Then
                Return CStr(Current.Session("r_DocType"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_DocType") = value
        End Set
    End Property
    Public Shared Property rpt_OfficeCode As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_OfficeCode") Is Nothing Then
                Return CStr(Current.Session("r_OfficeCode"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_OfficeCode") = value
        End Set
    End Property

    Public Shared Property rpt_OfficeDesc As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_OfficeDesc") Is Nothing Then
                Return CStr(Current.Session("r_OfficeDesc"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_OfficeDesc") = value
        End Set
    End Property

    Public Shared Property rpt_ColValue As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_ColValue") Is Nothing Then
                Return CStr(Current.Session("r_ColValue"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_ColValue") = value
        End Set
    End Property

    Public Shared Property rpt_ColValue2 As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_ColValue2") Is Nothing Then
                Return CStr(Current.Session("r_ColValue2"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_ColValue2") = value
        End Set
    End Property

    Public Shared Property rpt_ColValue3 As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_ColValue3") Is Nothing Then
                Return CStr(Current.Session("r_ColValue3"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_ColValue3") = value
        End Set
    End Property

    Public Shared Property rpt_Rtype As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_Rtype") Is Nothing Then
                Return CStr(Current.Session("r_Rtype"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_Rtype") = value
        End Set
    End Property

    Public Shared Property rpt_StartDate As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_StartDate") Is Nothing Then
                Return CStr(Current.Session("r_StartDate"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_StartDate") = value
        End Set
    End Property
    Public Shared Property rpt_EndDate As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_EndDate") Is Nothing Then
                Return CStr(Current.Session("r_EndDate"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_EndDate") = value
        End Set
    End Property

    Public Shared Property rpt_UploadStartDate As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_UploadStartDate") Is Nothing Then
                Return CStr(Current.Session("r_UploadStartDate"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_UploadStartDate") = value
        End Set
    End Property

    Public Shared Property rpt_UploadEndDate As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("r_UploadEndDate") Is Nothing Then
                Return CStr(Current.Session("r_UploadEndDate"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("r_UploadEndDate") = value
        End Set
    End Property

    Public Shared Property doc_DocCurrentPage As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocCurrentPage") Is Nothing Then
                Return CStr(Current.Session("s_doc_DocCurrentPage"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_doc_DocCurrentPage") = value
        End Set
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocCurrentPage") Is Nothing Then
        '        Return CStr(Current.Request.Cookies("s_doc_DocCurrentPage").Value)
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)

        '    Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocCurrentPage")
        '    mycookie.Value = value
        '    mycookie.Expires = DateTime.Now.AddDays(30)
        '    Current.Response.Cookies.Add(mycookie)


        'End Set
    End Property

    Public Shared Property doc_DocType As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocType") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocType"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocType") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocType") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocType").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocType")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocStatus As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocStatus") Is Nothing Then
                Return CStr(Current.Session("s_doc_DocStatus"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_doc_DocStatus") = value
        End Set
    End Property

    Public Shared Property doc_DocSent As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocSent") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocSent"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocSent") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocSent") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocSent").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocSent")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocReceived As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocReceived") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocReceived"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocReceived") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocReceived") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocReceived").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocReceived")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property
    Public Shared Property doc_DocAuthor As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocAuthor") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocAuthor"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocAuthor") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocAuthor") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocAuthor").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocAuthor")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property
    'Public Shared Property doc_DocAgency As String
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocAgency") Is Nothing Then
    '            Return CStr(Current.Session("s_doc_DocAgency"))
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("s_doc_DocAgency") = value
    '    End Set
    'End Property

    Public Shared Property doc_DocAuthorId As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocAuthorId") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocAuthorId"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocAuthorId") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocAuthorId") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocAuthorId").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocAuthorId")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocCreated As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocCreated") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocCreated"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocCreated") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocCreated") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocCreated").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocCreated")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocCreatedFrom As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocCreated") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocCreated"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocCreated") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocCreatedFrom") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocCreatedFrom").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocCreatedFrom")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocCreatedTo As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocCreated") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocCreated"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocCreated") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocCreatedTo") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocCreatedTo").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocCreatedTo")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_DocClassification As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocCreated") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocCreated"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocCreated") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocClassification") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocClassification").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocClassification")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    Public Shared Property doc_SortOrder As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_doc_SortOrder") Is Nothing Then
                Return CStr(Current.Session("s_doc_SortOrder"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_doc_SortOrder") = value
        End Set
    End Property
    Public Shared Property doc_SortCol As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_doc_SortCol") Is Nothing Then
                Return CStr(Current.Session("s_doc_SortCol"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_doc_SortCol") = value
        End Set
    End Property
    Public Shared Property doc_DocTitle As String
        'Get
        '    If Current IsNot Nothing AndAlso Not Current.Session("s_doc_DocTitle") Is Nothing Then
        '        Return CStr(Current.Session("s_doc_DocTitle"))
        '    Else
        '        Return ""
        '    End If
        'End Get
        'Set(ByVal value As String)
        '    Current.Session("s_doc_DocTitle") = value
        'End Set
        Get
            If Current IsNot Nothing AndAlso Not Current.Request.Cookies("s_doc_DocTitle") Is Nothing Then
                Return CStr(Current.Request.Cookies("s_doc_DocTitle").Value)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)

            Dim mycookie As HttpCookie = New HttpCookie("s_doc_DocTitle")
            mycookie.Value = value
            mycookie.Expires = DateTime.Now.AddDays(30)
            Current.Response.Cookies.Add(mycookie)

        End Set
    End Property

    'Public Shared Property doc_page As String
    '    Get
    '        If Current IsNot Nothing AndAlso Not Current.Session("s_doc_page") Is Nothing Then
    '            Return CStr(Current.Session("s_doc_page"))
    '        Else
    '            Return "documents.aspx"
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Current.Session("s_doc_page") = value
    '    End Set
    'End Property

    Public Shared Property srchAS As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_srchAS") Is Nothing Then
                Return CStr(Current.Session("s_srchAS"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_srchAS") = value
        End Set
    End Property
    Public Shared Property srchASCurrentPage As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_srchASCurrentPage") Is Nothing Then
                Return CStr(Current.Session("s_srchASCurrentPage"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_srchASCurrentPage") = value
        End Set
    End Property

    Public Shared Property SearchOption As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_SearchOption") Is Nothing Then
                Return CStr(Current.Session("s_SearchOption"))
            Else
                Return "E"
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_SearchOption") = value
        End Set
    End Property

    Public Shared Property SearchCriteria As String
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_SearchCriteria") Is Nothing Then
                Return CStr(Current.Session("s_SearchCriteria"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Current.Session("s_SearchCriteria") = value
        End Set
    End Property

    Public Shared Property sOwner As Boolean
        Get
            If Current IsNot Nothing AndAlso Not Current.Session("s_Owner") Is Nothing Then
                Return CBool(Current.Session("s_Owner"))
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            Current.Session("s_Owner") = CStr(value)
        End Set
    End Property
End Class
