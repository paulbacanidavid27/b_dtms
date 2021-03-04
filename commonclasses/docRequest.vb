Public Class docRequest

    Dim UserId As String
    Dim RequestType As String
    Dim RequestDescription As String
    Dim AgeCalc As String
    Dim Office As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String

    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim RowsPerPage As String
    Dim Idx As String


    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
        End Set

    End Property

    Public Property pAgeCalc() As String
        Get
            Return AgeCalc
        End Get
        Set(ByVal value As String)
            AgeCalc = value
        End Set

    End Property


    Public Property pIdx() As String
        Get
            Return Idx
        End Get
        Set(ByVal value As String)
            Idx = value
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
    Public Property pRequestType() As String
        Get
            Return RequestType
        End Get
        Set(ByVal value As String)
            RequestType = value
        End Set

    End Property
    Public Property pOffice() As String
        Get
            Return office
        End Get
        Set(ByVal value As String)
            office = value
        End Set

    End Property
    Public Property pRequestDescription() As String
        Get
            Return RequestDescription
        End Get
        Set(ByVal value As String)
            RequestDescription = value
        End Set

    End Property

    Public Property pSortOrder() As String
        Get
            Return SortOrder
        End Get
        Set(ByVal value As String)
            SortOrder = value
        End Set

    End Property

    Public Property pSortCol() As String
        Get
            Return SortCol
        End Get
        Set(ByVal value As String)
            SortCol = value
        End Set

    End Property


    Public Property pIPAddress() As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set

    End Property

    Public Sub New()

    End Sub
    Public Function RetrieveReceiptList() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            If DocSession.OraClient Then
                sSQL = "select * from (SELECT " & _
                          "row_number() over (ORDER BY " & s_order & " " & pSortOrder & ") as rn, " & _
                "r.RequestType, " & _
                "isnull(r.AgeCalc,'C') as AgeCalc,isnull(Office,'DBM') as Office, " & _
                "AgeCalcDesc = case when r.agecalc = 'R' then 'Regular' else 'Calendar' end, " & _
                "r.RequestDescription "

                sSQL = sSQL & "FROM  " & _
                " DocRequestType r " & _
               "WHERE rownum <= " & lTop.ToString & " and r.Inactive is null " & WhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                          "rn= row_number() over (ORDER BY " & s_order & " " & pSortOrder & "), " & _
                "r.RequestType, " & _
                "isnull(r.AgeCalc,'C') as AgeCalc,isnull(Office,'DBM') as Office, " & _
                "AgeCalcDesc = case when agecalc = 'R' then 'Regular' else 'Calendar' end, " & _
                "r.RequestDescription "

                sSQL = sSQL & "FROM  " & _
                " DocRequestType r " & _
               "WHERE r.Inactive is null " & WhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            End If


            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = sSQL



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

    Public Function RetrieveReceipts() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "select RequestType,RequestDescription,isnull(Office,'DBM') as Office,AgeCalc,AgeCalcDesc = case when agecalc = 'R' then 'Regular' else 'Calendar' end from DocRequestType order by RequestDescription "

            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = sSQL



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

    Private Function OrderBy() As String
        If pSortCol = "Description" Then
            Return " r.RequestDescription"
        Else
            Return " r.RequestDescription "
        End If

    End Function



    Private Function WhereClause() As String
        Dim lswhere As String = ""

        If pRequestDescription <> "" Then
            lswhere = lswhere & " AND r.RequestDescription like '%" & Replace(pRequestDescription, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    Public Sub DeleteReceipt(ByVal objcommand As clsSqlConn)

        Try

            Dim s_sql As String = "UPDATE DocRequestType Set Inactive = " & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & " WHERE RequestType = '" & pRequestType & "' "

            objcommand.CommandType = CommandType.Text
            objcommand.CommandText = s_sql
            objcommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Receipt"
            oHist.pRecordId = Replace(pRequestType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue =
            oHist.pNewValue = "Deleted Request Type (" & pRequestDescription & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objcommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try

    End Sub
    Public Sub AddRequest(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "INSERT INTO DocRequestType " & _
           "(RequestType,RequestDescription,Office,Inactive,AgeCalc) " & _
            "VALUES " & _
           "('" & Replace(pRequestType, "'", "''") & "'" & _
           ",'" & Replace(pRequestDescription, "'", "''") & "'" & _
           ",'" & Replace(pOffice, "'", "''") & "'" & _
           ",Null,'" & pAgeCalc & "')"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Request"
            oHist.pRecordId = Replace(pRequestType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Request Type (" & Replace(pRequestDescription, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub UpdateReceipt(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "UPDATE DocRequestType " & _
           "SET RequestDescription = '" & Replace(pRequestDescription, "'", "''") & "' " & _
           ",AgeCalc = '" & Replace(pAgeCalc, "'", "''") & "' " & _
           ",Office = '" & Replace(pOffice, "'", "''") & "' " & _
            " WHERE RequestType =  '" & Replace(pRequestType, "'", "''") & "' "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Receipt"
            oHist.pRecordId = Replace(pRequestType, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Updated Request Type (" & Replace(pRequestDescription, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Function CountDocRequest() As Integer

        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "DocRequestType r " & _
           "WHERE r.Inactive is null " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





            Return objCommand.ExecScalar


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

    Public Function CheckIfRequestExists(ByVal asRequestType As String) As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc

            objCommand.pCommandType = CommandType.Text
            If DocSession.OraClient Then

                objCommand.pCommandText = "SELECT RequestType FROM DocList WHERE rownum = 1 and RequestType = '" & asRequestType & "'"
            Else
                objCommand.pCommandText = "SELECT TOP 1 RequestType FROM DocList WHERE RequestType = '" & asRequestType & "'"
            End If


            Return objCommand.ExecHasRow()


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
