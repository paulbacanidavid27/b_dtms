Public Class DocAddress
    Dim AddrCode As String = ""
    Dim AddrDesc As String = ""

    Public Sub New()

    End Sub

    Public Property pAddrCode() As String
        Get
            Return AddrCode
        End Get
        Set(ByVal value As String)
            AddrCode = value
        End Set

    End Property

    
    Public Property pAddrDesc() As String
        Get
            Return AddrDesc
        End Get
        Set(ByVal value As String)
            AddrDesc = value
        End Set

    End Property



    Public Sub AddAddress()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "INSERT INTO OfficeAddress " & _
           "(AddressCode " & _
           ",AddressDesc) " & _
        "VALUES  " & _
           "('" & Replace(pAddrCode, "'", "''") & "'" & _
           ",'" & Replace(pAddrDesc, "'", "''") & "') "

            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()
            ''If DocSession.OraClient Then
            'Dim osp As New cls_storedproc
            'osp.pDocId = pDocId
            'osp.pNote = pNote
            'osp.pIPAddress = pIpAddress
            'osp.pUserId = pUserId
            'osp.pRecordId = pNoteId

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

        Catch ex As Exception
            If InStr(ex.Message.ToLower, "primary key") > 0 Then
                Throw New Exception("DBM ROs " & pAddrCode & " already exists.")
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
    Public Sub UpdateAddress()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "UPDATE OfficeAddress SET " & _
           "AddressDesc = '" & Replace(pAddrDesc, "'", "''") & "' " & _
           "WHERE AddressCode = '" & Replace(pAddrCode, "'", "''") & "'"


            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()
            ''If DocSession.OraClient Then
            'Dim osp As New cls_storedproc
            'osp.pDocId = pDocId
            'osp.pNote = pNote
            'osp.pIPAddress = pIpAddress
            'osp.pUserId = pUserId
            'osp.pRecordId = pNoteId

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

            'objCommand.pCommandType = CommandType.Text
            'objCommand.pCommandText = osp.DMSF_ADDHISTORY
            'osp.pAction = "Added note (" & pNote & ")" '-- to document '" & pTitle & "'"
            'objCommand.ExecTranNonQuery()
            'Else
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "XMSP_DOCNOTESADD"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@NOTES", pNote)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)
            'objCommand.ParametersAddWithValue("@CreateIPAddress", pIpAddress)
            'objCommand.ExecTranNonQuery()
            'objCommand.ParametersClear()
            'Dim Ohist As New DocHistory
            'Ohist.pTask = "Notes"
            'Ohist.pAction = "Added note (" & pNote & ")" '& "-- to document '" & pTitle & "'"
            'Ohist.pUserId = pUserId
            'Ohist.pIpAddress = pIpAddress
            'Ohist.pDocId = pDocId
            'Ohist.AddHistory(objCommand)
            'End If
            'ltr.pTran.Commit()

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

    Public Sub DeleteAddress()

        Dim s_sql As String
        Dim objCommand As clsSqlConn
        'Dim ltr As DbTran
        Try
            s_sql = "DELETE FROM OfficeAddress " & _
           "WHERE AddressCode = '" & Replace(pAddrCode, "'", "''") & "'"


            'ltr = New DbTran
            'objCommand = New clsSqlConn(ltr.pTran)
            objCommand = New clsSqlConn()
            ''If DocSession.OraClient Then
            'Dim osp As New cls_storedproc
            'osp.pDocId = pDocId
            'osp.pNote = pNote
            'osp.pIPAddress = pIpAddress
            'osp.pUserId = pUserId
            'osp.pRecordId = pNoteId

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            objCommand.ExecNonQuery()

            'objCommand.pCommandType = CommandType.Text
            'objCommand.pCommandText = osp.DMSF_ADDHISTORY
            'osp.pAction = "Added note (" & pNote & ")" '-- to document '" & pTitle & "'"
            'objCommand.ExecTranNonQuery()
            'Else
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "XMSP_DOCNOTESADD"
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@NOTES", pNote)
            'objCommand.ParametersAddWithValue("@CreatedBy", pUserId)
            'objCommand.ParametersAddWithValue("@CreateIPAddress", pIpAddress)
            'objCommand.ExecTranNonQuery()
            'objCommand.ParametersClear()
            'Dim Ohist As New DocHistory
            'Ohist.pTask = "Notes"
            'Ohist.pAction = "Added note (" & pNote & ")" '& "-- to document '" & pTitle & "'"
            'Ohist.pUserId = pUserId
            'Ohist.pIpAddress = pIpAddress
            'Ohist.pDocId = pDocId
            'Ohist.AddHistory(objCommand)
            'End If
            'ltr.pTran.Commit()

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

    Public Function RetrieveAddress() As DataTable
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT AddressCode,AddressDesc,(AddressCode+' - '+AddressDesc) AS AddrDesc " & _
                "FROM OfficeAddress "

            If pAddrCode.Trim <> "" Then
                s_sql = s_sql & " WHERE AddressCode = '" & Replace(pAddrCode, "'", "''") & "'"
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
    Public Function RetrieveAddressDesc() As String
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT AddressDesc " & _
                "FROM OfficeAddress "

            If pAddrCode.Trim <> "" Then
                s_sql = s_sql & " WHERE AddressCode = '" & Replace(pAddrCode, "'", "''") & "'"
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
