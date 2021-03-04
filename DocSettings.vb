Public Class DocSetting

    Public Sub New()

    End Sub
    Public Function getTimeoutValue() As Integer
        Dim objCommand As clsSqlConn
        Dim ldata As DataTable
        Try
            Dim lcr As New crypt
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "SELECT top 1 pagetimeout,rowsperpage,adminemail FROM docsettings"
            ldata = objCommand.Fill()
            If ldata.Rows.Count > 0 Then
                'DocSession.RowsPerPage = CInt(IIf(IsDBNull(ldata(0)("pagetimeout")), System.Configuration.ConfigurationManager.AppSettings("pagetimeout"), ldata(0)("pagetimeout")))
                DocSession.RowsPerPage = CInt(IIf(IsDBNull(ldata(0)("rowsperpage")), System.Configuration.ConfigurationManager.AppSettings("rowsperpage"), ldata(0)("rowsperpage")))
                DocSession.AdminEmail = CInt(IIf(IsDBNull(ldata(0)("adminemail")), System.Configuration.ConfigurationManager.AppSettings("adminemail"), ldata(0)("adminemail")))
                Return CInt(ldata(0)("pagetimeout").ToString.Trim)
            Else
                DocSession.RowsPerPage = System.Configuration.ConfigurationManager.AppSettings("rowsperpage")
                DocSession.AdminEmail = System.Configuration.ConfigurationManager.AppSettings("adminemail")
                Return 15
            End If



        Catch ex As Exception
            msg.Text = "An error occurred while processing your information (" & ex.Message & ")."
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try
    End Function
End Class
