Public Class DocSetting
    Dim rpp As Integer
    Dim pto As Integer
    Dim ademail As String
    Dim archiveperson As String
    Dim EnableSSL As Boolean = False
    Dim EmailHost As String = ""
    Dim EmailPort As String = ""
    Dim EmailPassword As String = ""
    Dim EmailUserName As String = ""
    Dim PurgeEmailRecipient As String = ""
    Public Sub New()

    End Sub
    Public Property pPurgeEmailRecipient As String
        Get
            Return PurgeEmailRecipient
        End Get
        Set(ByVal value As String)
            PurgeEmailRecipient = value
        End Set
    End Property
    Public Property pEnableSSL As Boolean
        Get
            Return EnableSSL
        End Get
        Set(ByVal value As Boolean)
            EnableSSL = value
        End Set
    End Property
    Public Property pEmailHost As String
        Get
            Return EmailHost
        End Get
        Set(ByVal value As String)
            EmailHost = value
        End Set
    End Property
    Public Property pEmailUserName As String
        Get
            Return EmailUserName
        End Get
        Set(ByVal value As String)
            EmailUserName = value
        End Set
    End Property
    Public Property pEmailPassword As String
        Get
            Return EmailPassword
        End Get
        Set(ByVal value As String)
            EmailPassword = value
        End Set
    End Property
    Public Property pEmailPort As String
        Get
            Return EmailPort
        End Get
        Set(ByVal value As String)
            EmailPort = value
        End Set
    End Property
    Public Property pRowsPerPage As Integer
        Get
            Return rpp
        End Get
        Set(ByVal value As Integer)
            rpp = value
        End Set
    End Property

    Public Property pArchiveperson As String
        Get
            Return archiveperson
        End Get
        Set(ByVal value As String)
            archiveperson = value
        End Set
    End Property

    Public Property pAdminEmail As String
        Get
            Return ademail
        End Get
        Set(ByVal value As String)
            ademail = value
        End Set
    End Property
    Public Property pPageTimeOut As String
        Get
            Return pto
        End Get
        Set(ByVal value As String)
            pto = value
        End Set
    End Property


    Public Function getTimeoutValue() As Integer
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT pagetimeout,rowsperpage,adminemail FROM docsettings"
            ldata = objCommand.Fill()
            If ldata.Rows.Count > 0 Then
                DocSession.PageTimeOut = IIf(IsDBNull(ldata(0)("pagetimeout")), System.Configuration.ConfigurationManager.AppSettings("pagetimeout"), ldata(0)("pagetimeout"))
                DocSession.RowsPerPage = CInt(IIf(IsDBNull(ldata(0)("rowsperpage")), System.Configuration.ConfigurationManager.AppSettings("rowsperpage"), ldata(0)("rowsperpage")))
                DocSession.AdminEmail = IIf(IsDBNull(ldata(0)("adminemail")), System.Configuration.ConfigurationManager.AppSettings("adminemail"), ldata(0)("adminemail"))
                Return CInt(ldata(0)("pagetimeout").ToString.Trim)
            Else
                DocSession.RowsPerPage = CInt(System.Configuration.ConfigurationManager.AppSettings("rowsperpage"))
                DocSession.AdminEmail = System.Configuration.ConfigurationManager.AppSettings("adminemail")
                DocSession.PageTimeOut = System.Configuration.ConfigurationManager.AppSettings("pagetimeout")
                objCommand.Dispose()
                objCommand = New clsSqlConn
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "INSERT INTO docsettings(pagetimeout,rowsperpage,adminemail,archivepointperson) VALUES (" & System.Configuration.ConfigurationManager.AppSettings("pagetimeout") & "," & DocSession.RowsPerPage & ", '" & DocSession.AdminEmail & "','')"
                objCommand.ExecNonQuery()

                Return 15
            End If



        Catch ex As Exception
            Throw New Exception("An error occurred while processing your information (" & ex.Message & ").")
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

    Public Function getArchivePointPerson() As String
        Dim objCommand As clsSqlConn
        Try

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "SELECT archivepointperson FROM docsettings where rownum = 1"
            Else
                objCommand.CommandText = "SELECT top 1 archivepointperson FROM docsettings"
            End If

            Return objCommand.ExecScalar3
        Catch ex As Exception
            Throw New Exception("An error occurred while processing your information (" & ex.Message & ").")
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Sub getSettings()
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            If DocSession.OraClient Then
                objCommand.CommandText = "SELECT pagetimeout,rowsperpage,adminemail,NVL(archivepointperson,' ') AS archivepointperson FROM docsettings where rownum = 1"
            Else
                objCommand.CommandText = "SELECT top 1 pagetimeout,rowsperpage,adminemail,archivepointperson =isnull(archivepointperson,'') " & _
                                            ",PurgePointPersonEmail,EmailPort,EmailHost,EmailEnableSSL,EmailUserName,EmailPassword FROM docsettings"
            End If

            ldata = objCommand.Fill()
            If ldata.Rows.Count > 0 Then
                pPageTimeOut = ldata(0)("pagetimeout")
                pRowsPerPage = ldata(0)("rowsperpage")
                pAdminEmail = ldata(0)("adminemail")
                pArchiveperson = ldata(0)("archivepointperson")
                pEnableSSL = ldata(0)("EmailEnableSSL")
                pEmailPort = ldata(0)("EmailPort")
                pEmailHost = ldata(0)("EmailHost")
                pEmailPassword = ldata(0)("EmailPassword")
                pEmailUserName = ldata(0)("EmailUserName")
                pPurgeEmailRecipient = ldata(0)("PurgePointPersonEmail")
            Else
                pPageTimeOut = System.Configuration.ConfigurationManager.AppSettings("pagetimeout")
                pRowsPerPage = System.Configuration.ConfigurationManager.AppSettings("rowsperpage")
                pAdminEmail = System.Configuration.ConfigurationManager.AppSettings("adminemail")
            End If

        Catch ex As Exception
            Throw New Exception("An error occurred while processing your information (" & ex.Message & ").")
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

    Public Sub SaveSettings()
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            'Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "UPDATE docsettings SET archivepointperson = '" & pArchiveperson & "',pagetimeout = " & pPageTimeOut & ",rowsperpage = " & pRowsPerPage & ",adminemail = '" & pAdminEmail & "'" & _
                ",EmailUserName = '" & pEmailUserName & "',EmailPassword = '" & pEmailPassword & "',EmailHost = '" & pEmailHost & "',EmailPort = '" & pEmailPort & "',EmailEnableSSL = " & IIf(pEnableSSL, "1", "0") & "," & _
                "PurgePointPersonEmail = '" & pPurgeEmailRecipient & "'"
            objCommand.ExecNonQuery()

        Catch ex As Exception
            Throw New Exception("An error occurred while processing your information (" & ex.Message & ").")
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

End Class
