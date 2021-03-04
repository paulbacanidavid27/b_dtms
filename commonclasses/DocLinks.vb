Imports System.Data.SqlClient

Public Class DocLinks

    Public Sub New()

    End Sub

    Dim IpAdd As String
    Dim UserId As String
    Dim DocId As Integer
    Dim DocLinkId As String
    Dim DocDate As String
    Dim DocDesc As String

    Public Property pLinkDocId() As Integer
        Get
            Return DocLinkId
        End Get
        Set(ByVal value As Integer)
            DocLinkId = value
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

    Public Property pDocDesc() As String
        Get
            Return DocDesc
        End Get
        Set(ByVal value As String)
            DocDesc = value
        End Set

    End Property

    Public Property pDocDate() As String
        Get
            Return DocDate
        End Get
        Set(ByVal value As String)
            DocDate = value
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
    Public Function CountDocLinks(ByVal aiDocId As Integer) As Integer

        Dim objCommand As clsSqlConn

        Try
            Dim s_sql As String = "SELECT count(dl.DocId) "


            s_sql = s_sql & "FROM DocLinks dl INNER JOIN " & _
    "DocList d ON dl.LinkDocId = d.docId " & _
                    "INNER JOIN DocType dt ON d.DocType = dt.DocType " & _
                    "INNER JOIN DocStatus DS ON d.statusid=ds.statusid " & _
                    "INNER JOIN Users u ON dl.createdbye = u.userId " & _
    "WHERE " & _
    "(dl.DocId = " & CStr(aiDocId) & ") and dl.deletedby is null "

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
    Public Function RetrieveDocLinks(ByVal aiDocId As Integer) As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As New DataTable

        Try
            objCommand = New clsSqlConn
            Dim osp As New cls_storedproc
            osp.pDocId = aiDocId
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = osp.DMSF_DOCLINKSGET

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

    Public Function RetrieveDocLinkDocID(ByVal aiDocId As Integer) As DataTable
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim ldata As New DataTable

        Try
            objCommand = New clsSqlConn
            'Dim osp As New cls_storedproc
            Dim s_sql As String = "SELECT dl.docId,dls.LinkdocId,dl.refno,cdate = convert(char(10),dl.createddate,101),dl.CreatedDate " &
      ", dl.CreatedBy ,dl.Title,isnull(o.Description,'') as Office,isnull(g.OfficeCode,'') as OfficeCode ,isnull(dr.approverid,0) as ApproverID ,isnull(u.Firstname,'') + ' ' + isnull(u.LastName,'') AS createdbyName " &
", isnull(dl.RoutingSeqNo, 0) RoutingSeqNo " &
                "FROM DocLinks dls INNER JOIN " &
                    "DocList dl ON dls.LinkDocId = dl.docId " &
                    "left JOIN DocRouting dr ON " &
                      "dr.SeqNo = dl.RoutingSeqNo " &
                      "LEFT JOIN Users u ON " &
                      "dr.approverid = u.userid " &
                      "LEFT JOIN Groups g ON " &
                      "g.GroupID = u.UserGroup " &
                      "LEFT JOIN Office o ON " &
                      "g.OfficeCode = o.OfficeCode " &
                    "WHERE " &
                    "dls.DocId = " & aiDocId & " and dls.deletedby is null "

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql

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

    Public Sub SaveDocLinks(ByVal objCommand As clsSqlConn)

        Try

            Dim s_sql As String

            If DocSession.OraClient Then
                s_sql = "BEGIN INSERT INTO DocLinks " & _
           "(DocId " & _
           ",LinkDocId " & _
           ",CreatedBye " & _
     ",CreatedDate " & _
           ",CreateIPAddress " & _
           ") VALUES (" & _
           "" & pDocId & _
           "," & pLinkDocId & _
           ",'" & pUserId & "'" & _
           ",TO_DATE('" & pDocDate & "','mm/dd/yyyy hh:mi:ss am') " & _
           ",'" & pIpAddress & "'); "



                s_sql = s_sql & " IF SQL%ROWCOUNT = 1 THEN INSERT INTO DocLinks " & _
               "(DocId " & _
               ",LinkDocId " & _
               ",CreatedBye " & _
                ",CreatedDate " & _
               ",CreateIPAddress " & _
               ") VALUES (" & _
               "" & pLinkDocId & _
                "," & pDocId & _
               ",'" & pUserId & "'" & _
               ",TO_DATE('" & pDocDate & "','mm/dd/yyyy hh:mi:ss am') " & _
               ",'" & pIpAddress & "'); END IF; END; "

            Else
                s_sql = "INSERT INTO DocLinks " & _
           "(DocId " & _
           ",LinkDocId " & _
           ",CreatedBye " & _
     ",CreatedDate " & _
           ",CreateIPAddress " & _
           ") VALUES (" & _
           "" & pDocId & _
           "," & pLinkDocId & _
           ",'" & pUserId & "'" & _
           ",'" & pDocDate & "'" & _
           ",'" & pIpAddress & "')  "



                s_sql = s_sql & " IF @@ROWCOUNT = 1 INSERT INTO DocLinks " & _
               "(DocId " & _
               ",LinkDocId " & _
               ",CreatedBye " & _
                ",CreatedDate " & _
               ",CreateIPAddress " & _
               ") VALUES (" & _
               "" & pLinkDocId & _
                "," & pDocId & _
               ",'" & pUserId & "'" & _
               ",'" & pDocDate & "'" & _
               ",'" & pIpAddress & "') "
            End If

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

            Dim Ohist As New DocHistory
            Ohist.pTask = "Link"
            Ohist.pAction = "Added link (" & pDocDesc & ")" '-- to document '" & DocSession.sDocTitle & "'"
            Ohist.pUserId = DocSession.sUserId
            Ohist.pIpAddress = pIpAddress
            Ohist.pDocId = pLinkDocId
            Ohist.AddHistory(objCommand)
            objCommand.ExecTranNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub
    Public Function DMSF_DOCLINKSDELETE() As String
        'Return "UPDATE DocLinks " & _
        '   "SET " & _
        '   "DeletedBy = '" & Replace(pUserId, "'", "''") & "' " & _
        '   ",DeletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
        '   ",DelIPAddress = '" & pIpAddress & "' " & _
        '"WHERE " & _
        '   "DocId = " & pDocId & _
        '    "AND LinkDocId = " & pLinkDocId
        Return "DELETE FROM DocLinks " & _
        "WHERE " & _
           "DocId = " & pDocId & _
            "AND LinkDocId = " & pLinkDocId
    End Function

    Public Function DMSF_DOCLINKSDELETEtwist() As String
        'Return "UPDATE DocLinks " & _
        '   "SET " & _
        '   "DeletedBy = '" & Replace(pUserId, "'", "''") & "' " & _
        '   ",DeletedDate = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " " & _
        '   ",DelIPAddress = '" & pIpAddress & "' " & _
        '"WHERE " & _
        '   "DocId = " & pLinkDocId & _
        '    "AND LinkDocId = " & pDocId
        Return "DELETE FROM DocLinks " & _
        "WHERE " & _
           "DocId = " & pLinkDocId & _
            "AND LinkDocId = " & pDocId
    End Function

    Public Sub DeleteDocLinks(ByVal objCommand As clsSqlConn)

        Try

            'objCommand.ClearParameter()
            'objCommand.ParametersAddWithValue("@DocId", pDocId)
            'objCommand.ParametersAddWithValue("@LinkDocId", pLinkDocId)
            'objCommand.ParametersAddWithValue("@DeletedBy", pUserId)
            'objCommand.ParametersAddWithValue("@DelIPAddress", pIpAddress)
            'objCommand.ExecTranNonQuery()
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = DMSF_DOCLINKSDELETE()
            objCommand.ExecTranNonQuery()

            objCommand.CommandText = DMSF_DOCLINKSDELETEtwist()
            objCommand.ExecTranNonQuery()
        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally


        End Try
    End Sub

    Public Sub DeleteDocLinks()

        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = DMSF_DOCLINKSDELETE() & " " & DMSF_DOCLINKSDELETEtwist()
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
End Class
