Imports System.Data.SqlClient

Public Class DocAttachment

    Dim smsg As String
    Public Event e_click()
    Public DocFileName As String
    Public OldDocFileName As String
    Public DocId As String
    Public Refno As String
    Public AttachId As String
    Public AttachedBy As String
    Public AttachType As String
    Public AttachedDate As String
    Public IPAddress As String
    Dim FileSize As String

    Public Property pRefNo() As String
        Get
            Return Refno
        End Get
        Set(ByVal value As String)
            Refno = value
        End Set

    End Property
    Public Property pFileSize() As String
        Get
            Return FileSize
        End Get
        Set(ByVal value As String)
            FileSize = value
        End Set

    End Property
    Public Property pAttachId As String
        Get
            Return AttachId
        End Get
        Set(ByVal value As String)
            AttachId = value
        End Set
    End Property
    Public Property pAttachType As String
        Get
            Return AttachType
        End Get
        Set(ByVal value As String)
            AttachType = value
        End Set
    End Property
    Public Property pIPAddress As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set
    End Property
    Public Property pDocId As String
        Get
            Return DocId
        End Get
        Set(ByVal value As String)
            DocId = value
        End Set
    End Property
    Public Property pDocFileName As String
        Get
            Return DocFileName
        End Get
        Set(ByVal value As String)
            DocFileName = value
        End Set
    End Property
    Public Property pOldDocFileName As String
        Get
            Return OldDocFileName
        End Get
        Set(ByVal value As String)
            OldDocFileName = value
        End Set
    End Property
    Public Property pAttachedBy As String
        Get
            Return AttachedBy
        End Get
        Set(ByVal value As String)
            AttachedBy = value
        End Set
    End Property
    Public Property pAttachedDate As String
        Get
            Return AttachedDate
        End Get
        Set(ByVal value As String)
            AttachedDate = value
        End Set
    End Property

    Public Sub New()

    End Sub


    Public Sub AddAttachment()


        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "INSERT INTO DocAttachment " & _
           "(DocAttachId,DocId " & _
           ",DocFileName " & _
           ",AttachedBy " & _
           ",FileSize " & _
           ",AttachedDate) " & _
           "VALUES (" & _
           "" & pAttachId & " " & _
           "," & pDocId & " " & _
           ",'" & pDocFileName & "' " & _
           ",'" & pAttachedBy & "' " & _
           "," & pFileSize & " " & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ") "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql 

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

    Public Sub UpadateAttachment()


        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            Dim s_sql As String = "UPDATE DocAttachment " & _
           "set " & _
           "DocFileName ='" & pDocFileName & "' " & _
           " WHERE DocAttachId = " & pAttachId & _
           " AND DocId = " & pDocId & " "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()

            Dim Ohist As New DocHistory
            Ohist.pTask = "Attachment"
            Ohist.pAction = "Changed attachment file name from '" & pOldDocFileName & "' to '" & pDocFileName & "'."
            Ohist.pUserId = DocSession.sUserId
            Ohist.pIpAddress = pIPAddress
            Ohist.pDocId = pDocId
            Ohist.AddHistory(objCommand)
            objCommand.ExecTranNonQuery()

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

    Public Sub UpadateAttachmentType()


        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            Dim s_sql As String = "UPDATE DocAttachment " & _
           "set " & _
           "AttachType ='" & pAttachType & "' " & _
           " WHERE DocAttachId = " & pAttachId & _
           " AND DocId = " & pDocId & " "

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()

            Dim Ohist As New DocHistory
            Ohist.pTask = "Attachment"
            Ohist.pAction = "Changed attachment type name to '" & pAttachType & "'."
            Ohist.pUserId = DocSession.sUserId
            Ohist.pIpAddress = pIPAddress
            Ohist.pDocId = pDocId
            Ohist.AddHistory(objCommand)
            objCommand.ExecTranNonQuery()

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

    Public Sub UpdateDocListFileVersion(ByVal objCommand As clsSqlConn)

        Try

            objCommand.pCommandType = CommandType.Text
            'If lAction.Text = "Add" Then
            '"xMSP_DOCLISTADD"
            'Else
            'objCommand.pCommandText = "xMSP_DOCLISTADD"
            'End If
            Dim lssql As String
            lssql = "UPDATE DocFileVersion set FileName = '" & Replace(pDocFileName, "'", "''") & "',UploadedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ", UploadedBy = '" & DocSession.sUserId & "' " &
" WHERE DocId = " & pDocId & "  and docVersion = 1 ; " &
" if @@rowcount = 0 "
            lssql = lssql & " INSERT INTO DocFileVersion(docId,docVersion,uploadedDate,uploadedby,FileName,comments,FileSize) " &
" VALUES (" & pDocId & ",1,getdate(),'" & DocSession.sUserId & "','" & Replace(pDocFileName, "'", "''") & "','Initial Creation'," & pFileSize & ")"

            objCommand.pCommandText = lssql
            objCommand.ExecTranNonQuery()

            'Return retval.pParam.Value



        Catch ex As Exception
            Throw New Exception(ex.Message)



        Finally


        End Try
    End Sub

    Public Sub DeleteAttachment(ByVal objCommand As clsSqlConn)
        Dim s_sql As String = "DELETE FROM DocAttachment WHERE DocAttachId = " & pAttachId
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = s_sql
        objCommand.ExecTranNonQuery()
    End Sub

    Public Sub UploadFile(ByVal objCommand As clsSqlConn)




        Try
            Dim s_sql As String = "UPDATE Doclist SET  FileName = '" & pDocFileName & "' " & _
           ",ModifiedBy = '" & DocSession.sUserId & "' ,ModifiedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
           " WHERE docId = " & pDocId

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql

            objCommand.ExecTranNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        

        End Try

    End Sub



    Public Function RetrieveAttachment() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT a.Docid,a.DocAttachId,a.DocFileName " & _
                ",a.AttachedBy,a.AttachedDate " & _
                ",u.FirstName +' '+u.LastName AS username " & _
                ",FileSize=Isnull(FileSize,0) " & _
                ",convert(varchar,a.AttachedDate,107) AS ADate " & _
                ",isnull(a.AttachType,'') AS AttachType " & _
        "FROM DocAttachment a " & _
        "INNER JOIN users u ON u.UserId = a.AttachedBy " & _
        "WHERE a.DocId = " & pDocId & " " & _
        "ORDER BY AttachedDate Desc "

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

    Public Function CountAttachment(ByVal asDocId As String) As Integer


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT Count(a.DocAttachId) " & _
        "FROM DocAttachment a " & _
        "WHERE a.DocId = " & asDocId & " "




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

    Public Function RetAttachFileName() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT a.DocFileName " & _
        "FROM DocAttachment a " & _
        "WHERE DocAttachId = " & pAttachId & " "

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

    Public Function RetrieveDocAttachment() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT dl.docid,dl.RefNo,a.DocAttachId,a.DocFileName " & _
        "FROM DocList dl Inner Join DocAttachment a on dl.docid = a.docid " & _
        "WHERE dl.refno in ('" & pRefNo.Replace(",", "','") & "') "

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

    Public Function CheckIfAttachmentExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = "SELECT * FROM DocAttachment a WHERE a.DocFileName = '" & pDocFileName & "' "
            Return objCommand.ExecHasRow


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
End Class
