Public Class DocNotes
    Dim IpAdd As String
    Dim UserId As String
    Dim DocId As Integer
    Dim DocNote As String
    Dim Title As String
    Dim NoteId As String
    Public Sub New()

    End Sub

    Public Property pTitle() As String
        Get
            Return Title
        End Get
        Set(ByVal value As String)
            Title = value
        End Set

    End Property

    Public Property pNoteId() As String
        Get
            Return NoteId
        End Get
        Set(ByVal value As String)
            NoteId = value
        End Set

    End Property

    Public Property pNote() As String
        Get
            Return DocNote
        End Get
        Set(ByVal value As String)
            DocNote = value
        End Set

    End Property

    Public Property pDocId() As Integer
        Get
            Return DocId
        End Get
        Set(ByVal value As Integer)
            DocId = value
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

    Public Property pIpAddress() As String
        Get
            Return IpAdd
        End Get
        Set(ByVal value As String)
            IpAdd = value
        End Set

    End Property

    Public Sub SaveDocNote()


        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pDocId = pDocId
            osp.pNote = pNote
            osp.pIPAddress = pIpAddress
            osp.pUserId = pUserId
            osp.pRecordId = pNoteId

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCNOTESADD
            objCommand.ExecTranNonQuery()

            
            Dim Ohist As New DocHistory
            Ohist.pTask = "Notes"
            Ohist.pAction = "Added note (" & Replace(pNote, "'", "''") & ")" '& "-- to document '" & pTitle & "'"
            Ohist.pUserId = pUserId
            Ohist.pIpAddress = pIpAddress
            Ohist.pDocId = pDocId
            Ohist.AddHistory(objCommand)
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

    Public Sub SaveDocNote(ByVal objCommand As clsSqlConn)



        Try

            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pDocId = pDocId
            osp.pNote = pNote
            osp.pIPAddress = pIpAddress
            osp.pUserId = pUserId
            osp.pRecordId = pNoteId

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCNOTESADD
            objCommand.ExecTranNonQuery()

            Dim Ohist As New DocHistory
            Ohist.pTask = "Notes"
            Ohist.pAction = "Added note (" & Replace(pNote, "'", "''") & ")" '& "-- to document '" & pTitle & "'"
            Ohist.pUserId = pUserId
            Ohist.pIpAddress = pIpAddress
            Ohist.pDocId = pDocId
            Ohist.AddHistory(objCommand)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        Finally



        End Try

    End Sub

    'Public Sub SaveDocNote2()

    '    Dim objCommand As clsSqlConn

    '    Try
    '        Dim s_sql As String = "INSERT INTO DocNOTES" & _
    '       "(DocId" & _
    '       ",NOTES" & _
    '       ",CreatedBy" & _
    '       ",CreatedDate" & _
    '       ",CreateIPAddress) " & _
    ' "VALUES" & _
    '       "(" & CStr(pDocId) & "," & _
    '       "'" & pNote & "'," & _
    '       "'" & pUserId & "," & _
    '       "'" & DateTime.Now & "'," & _
    '       "'" & pIpAddress & "')"


    '        objCommand = New clsSqlConn

    '        objCommand.CommandType = CommandType.Text


    '        objCommand.CommandText = s_sql '"xMSP_DOCNOTESADD"
    '        objCommand.ParametersAddWithValue("@DocId", pDocId)
    '        objCommand.ParametersAddWithValue("@Notes", pNote)
    '        objCommand.ParametersAddWithValue("@CreatedBy", pUserId)
    '        objCommand.ParametersAddWithValue("@CreateIPAddress", pIpAddress)

    '        objCommand.ExecNonQuery()
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If


    '    End Try

    'End Sub

    Public Function RetrieveDocNote(ByVal aiDocId As Integer) As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT dn.DocId " & _
      ",dn.NOTES " & _
      ",dn.CreatedBy " & _
      ",dn.CreatedDate " & _
      ",dn.ModifiedBy " & _
      ",dn.ModifiedDate " & _
      ",dn.DeletedBy " & _
      ",dn.DeletedDate " & _
      ",dn.DelIPAddress " & _
      ",dn.ModIPAddress " & _
      ",dn.CreateIPAddress " 
            If DocSession.OraClient Then
                s_sql = s_sql & ",NVL(dn.noteId,'') AS noteid " & _
                ",(u.FirstName || ' ' || u.LastName) AS username "
            Else
                s_sql = s_sql & ",isnull(dn.noteId,'') AS noteid " & _
                        ",(u.FirstName + ' ' + u.LastName) AS username "
            End If

            s_sql = s_sql & "FROM DocNOTES dn " & _
    "INNER JOIN Users u ON dn.createdby = u.userId " & _
    "WHERE " & _
    "(DocId = " & CStr(aiDocId) & ") and deletedby is null "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCNOTESGET"


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

    Public Function CountDocNote(ByVal aiDocId As Integer) As Integer

        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT count(dn.DocId) "


            s_sql = s_sql & "FROM DocNOTES dn " & _
    "INNER JOIN Users u ON dn.createdby = u.userId " & _
    "WHERE " & _
    "(DocId = " & CStr(aiDocId) & ") and deletedby is null "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"xMSP_DOCNOTESGET"


            Return objCommand.ExecScalar2





        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Sub DeleteDocNotes(ByVal objCommand As clsSqlConn)

        Try
            Dim s_sql As String = "UPDATE DocNOTES " & _
           "SET " & _
           "DeletedBy = '" & pUserId & "' " & _
           ",DeletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           ",DelIPAddress = '" & pIpAddress & "' " & _
     "WHERE " & _
           "NoteId = " & pNoteId & " "


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql '"xMSP_DOCNOTESDELETE"


            'objCommand.ParametersClear()

            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@Notes", pNote)
            'objCommand.ParametersAddWithValue("@DeletedBy", pUserId)
            'objCommand.ParametersAddWithValue("@DelIPAddress", pIpAddress)
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub UpdateDocNotes()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = "UPDATE DocNOTES " & _
           "SET " & _
           "Notes = '" & Replace(pNote, "'", "''") & "' " & _
           ",ModifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           ",ModIPAddress = '" & pIpAddress & "' " & _
     "WHERE " & _
           "NoteId = " & pNoteId & " "


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql '"xMSP_DOCNOTESDELETE"

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

    Public Sub UpdateDocNotes(ByVal objCommand As clsSqlConn)

        Try

            Dim s_sql As String = "UPDATE DocNOTES " & _
           "SET " & _
           "Notes = '" & Replace(pNote, "'", "''") & "' " & _
           ",ModifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           ",ModIPAddress = '" & pIpAddress & "' " & _
     "WHERE " & _
           "NoteId = " & pNoteId & " "


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql '"xMSP_DOCNOTESDELETE"


        
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
End Class
