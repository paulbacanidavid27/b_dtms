Imports System
Public Class DocTags
    Dim IpAdd As String
    Dim UserId As String
    Dim DocId As Integer
    Dim DocTag As String
    Dim DocName As String
    Dim Title As String
    Dim FileName As String

    Public Sub New()

    End Sub
    Public Property pFileName() As String
        Get
            Return FileName
        End Get
        Set(ByVal value As String)
            FileName = value
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
    Public Property pTag() As String
        Get
            Return DocTag
        End Get
        Set(ByVal value As String)
            DocTag = value
        End Set

    End Property

    Public Property pDocName() As String
        Get
            Return DocName
        End Get
        Set(ByVal value As String)
            DocName = value
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

    Public Sub SaveDocTag()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pDocId = pDocId
            osp.pTags = pTag
            osp.pIPAddress = pIpAddress
            osp.pUserId = pUserId
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCTAGADD
            objCommand.ExecTranNonQuery()
            'osp.pAction = "Added tag (" & pTag & ")"
            'objCommand.pCommandType = CommandType.Text

            'objCommand.pCommandText = osp.DMSF_ADDHISTORY

            'objCommand.ExecTranNonQuery()
            'Else
            '    objCommand.pCommandType = CommandType.StoredProcedure
            '    objCommand.pCommandText = "xMSP_DOCTAGADD"
            '    objCommand.ParametersAddWithValue("@DocId", pDocId)
            '    objCommand.ParametersAddWithValue("@Tags", pTag)
            '    objCommand.ParametersAddWithValue("@CreatedBy", pUserId)
            '    objCommand.ParametersAddWithValue("@CreateIPAddress", pIpAddress)
            '    objCommand.ExecTranNonQuery()
            '    objCommand.ParametersClear()
            Dim Ohist As New DocHistory
            Ohist.pTask = "Tag"
            Ohist.pAction = "Added tag (" & pTag & ")"
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

    Public Function RetrieveDocTag(ByVal aiDocId As Integer) As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc
            osp.pDocId = aiDocId
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCTAGSGET

            'Else
            '    objCommand.pCommandType = CommandType.StoredProcedure
            '    objCommand.pCommandText = "xMSP_DOCTAGSGET"
            '    objCommand.ParametersAddWithValue("@DocId", aiDocId)
            'End If

            ldata = objCommand.ExecData

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

    Public Sub DeleteDocTags(ByVal objCommand As clsSqlConn)

        Try
            Dim s_sql As String = "UPDATE DocTags " & _
           "SET " & _
           "DeletedBy = '" & pUserId & "' " & _
           ",DeletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           ",DelIPAddress = '" & pIpAddress & "' " & _
     "WHERE " & _
           "DocId = " & pDocId & " " & _
            "AND Tags = '" & Replace(pTag, "'", "''") & "'"
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql
            'objCommand.ClearParameter()
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@Tags", pTag)
            'objCommand.ParametersAddWithValue("@DeletedBy", pUserId)
            'objCommand.ParametersAddWithValue("@DelIPAddress", pIpAddress)


            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
End Class
