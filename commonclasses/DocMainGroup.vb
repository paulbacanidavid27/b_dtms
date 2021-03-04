Public Class DocMainGroup
    Dim MainGroupId As String = ""
    Dim MainGroupDesc As String = ""

    Public Sub New()

    End Sub

    Public Property pMainGroupId() As String
        Get
            Return MainGroupId
        End Get
        Set(ByVal value As String)
            MainGroupId = value
        End Set

    End Property

    Public Property pMainGroupDesc() As String
        Get
            Return MainGroupDesc
        End Get
        Set(ByVal value As String)
            MainGroupDesc = value
        End Set

    End Property



    Public Sub AddMainGroup()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "INSERT INTO MainGroup " & _
           "(MainGroupId " & _
           ",Description) " & _
        "VALUES  " & _
           "('" & Replace(pMainGroupId, "'", "''") & "'" & _
           ",'" & Replace(pMainGroupDesc, "'", "''") & "') "


            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()



        Catch ex As Exception
            If InStr(ex.Message.ToLower, "primary key") > 0 Then
                Throw New Exception("Main Group Code " & pMainGroupId & " already exists.")
            Else
                Throw New Exception(ex.Message)
            End If

        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub
    Public Sub UpdateMainGroup()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "UPDATE MainGroup SET " & _
           "Description = '" & Replace(pMainGroupDesc, "'", "''") & "' " & _
           "WHERE MainGroupId = '" & Replace(pMainGroupId, "'", "''") & "'"

            objCommand = New clsSqlConn()
            
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()


        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub

    Public Sub DeleteMainGroup()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "DELETE FROM MainGroup " & _
           "WHERE MainGroupId = '" & Replace(pMainGroupId, "'", "''") & "'"
            objCommand = New clsSqlConn()

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

     
        Catch ex As Exception
            'ltr.pTran.Rollback()
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try

    End Sub

    Public Function RetrieveMainGroup() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT MainGroupId,Description,(MainGroupId+' - '+Description) AS MainGroupDesc " & _
                "FROM MainGroup "

            If pMainGroupId.Trim <> "" Then
                s_sql = s_sql & " WHERE MainGroupId = '" & Replace(pMainGroupId, "'", "''") & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

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
    Public Function RetrieveMainGroupDesc() As String
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT Description " & _
                "FROM MainGroup "

            If pMainGroupId.Trim <> "" Then
                s_sql = s_sql & " WHERE MainGroupId = '" & Replace(pMainGroupId, "'", "''") & "'"
            End If


            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            Return objCommand.ExecScalar3


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
