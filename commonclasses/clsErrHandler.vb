Public Class clsErrHandler
    Dim objEx As Exception
    Public Property pException As Exception
        Get
            Return objEx
        End Get
        Set(ByVal value As Exception)
            objEx = value
        End Set
    End Property
    Dim LoginUser As String
    Public Property pLoginUser As String
        Get
            Return LoginUser
        End Get
        Set(ByVal value As String)
            LoginUser = value
        End Set
    End Property
    Dim lURI As String
    Public Property pURI As String
        Get
            Return lURI
        End Get
        Set(ByVal value As String)
            lURI = value
        End Set
    End Property
    Public Sub SaveException()
        Dim s_sql As String
        Dim oConn As clsSqlConn
        Try
            s_sql = "INSERT INTO apperror (source,stacktrace,message,loginuser,dateoccurred) " & _
                    " VALUES ('" & Left(Replace(pException.InnerException.Source, "'", "''"), 1000).Trim() & "','" & Left(Replace(pURI, "'", "''") & "-" & Replace(pException.InnerException.StackTrace, "'", "''"), 3000).Trim() & "','" & Replace(Left(pException.Message & "-Inner Exception: " & pException.InnerException.Message, 1000).Trim(), "'", "''") & "','" & pLoginUser & "'," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ")"
            oConn = New clsSqlConn
            oConn.pCommandText = s_sql
            oConn.pCommandType = CommandType.Text
            oConn.ExecNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If (Not oConn Is Nothing) Then
                oConn.Dispose()
                oConn = Nothing
            End If
        End Try
    End Sub
End Class
