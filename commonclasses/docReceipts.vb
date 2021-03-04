Public Class docReceipts
    
    Dim UserId As String
    Dim ReceiptId As String
    Dim ReceiptDesc As String
    
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
    Public Property pReceiptId() As String
        Get
            Return ReceiptId
        End Get
        Set(ByVal value As String)
            ReceiptId = value
        End Set

    End Property

    Public Property pReceiptDesc() As String
        Get
            Return ReceiptDesc
        End Get
        Set(ByVal value As String)
            ReceiptDesc = value
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
                "r.ReceiptId, " & _
                "r.ReceiptDesc "

                sSQL = sSQL & "FROM  " & _
                " DocReceipts r " & _
               "WHERE rownum <= " & lTop.ToString & " " & WhereClause() & " " & _
                          ") dtbl " & _
                " WHERE dtbl.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx
            Else
                sSQL = "select * from (SELECT TOP " & lTop.ToString & " " & _
                         "rn= row_number() over (ORDER BY " & s_order & " " & pSortOrder & "), " & _
               "r.ReceiptId, " & _
               "r.ReceiptDesc "

                sSQL = sSQL & "FROM  " & _
                " DocReceipts r " & _
               "WHERE r.Receiptid is not null " & WhereClause() & " " & _
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

            sSQL = "select receiptid,receiptdesc from docReceipts order by receiptdesc "

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
            Return " r.ReceiptDesc"
        Else
            Return " r.ReceiptDesc "
        End If

    End Function

    

    Private Function WhereClause() As String
        Dim lswhere As String = ""

        If pReceiptDesc <> "" Then
            lswhere = lswhere & " AND r.ReceiptDesc like '%" & Replace(pReceiptDesc, "'", "''") & "%' "
        End If
        Return lswhere
    End Function

    Public Sub DeleteReceipt(ByVal objcommand As clsSqlConn)

        Try

            Dim s_sql As String = "DELETE FROM DocReceipts WHERE ReceiptId = '" & pReceiptId & "' " 

            objcommand.CommandType = CommandType.Text
            objcommand.CommandText = s_sql
            objcommand.ExecTranNonQuery()

            Dim oHist As New DocHistory
            oHist.pTableName = "Receipt"
            oHist.pRecordId = Replace(pReceiptId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue =
            oHist.pNewValue = "Deleted Receipt (" & pReceiptDesc & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objcommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try

    End Sub
    Public Sub AddReceipt(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "INSERT INTO DocReceipts " & _
           "(ReceiptId,ReceiptDesc,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate) " & _
            "VALUES " & _
           "('" & Replace(pReceiptId, "'", "''") & "'" & _
           ",'" & Replace(pReceiptDesc, "'", "''") & "'" & _
           ",'" & DocSession.sUserId & "'" & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & _
           ",'" & DocSession.sUserId & "'" & _
           "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ")"
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Status"
            oHist.pRecordId = Replace(pReceiptId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Added Manner of Receipt (" & Replace(pReceiptDesc, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Sub UpdateReceipt(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "UPDATE DocReceipts " & _
           "SET ReceiptDesc = '" & Replace(pReceiptDesc, "'", "''") & "',ModifiedBy = '" & DocSession.sUserId & "',ModifiedDate = " & IIf(DocSession.OraClient, "TO_DATE('" & ModifiedDate & "','mm/dd/yyyy hh:mi:ss am')", "'" & ModifiedDate & "'") & _
            " WHERE ReceiptId =  " & Replace(pReceiptId, "'", "''") & " "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()


            Dim oHist As New DocHistory

            oHist.pTableName = "Receipt"
            oHist.pRecordId = Replace(pReceiptId, "'", "''")
            oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            oHist.pColumnName = ""
            oHist.pModifiedDate = ModifiedDate
            oHist.pOldValue = ""
            oHist.pNewValue = "Updated Manner of Receipt (" & Replace(pReceiptDesc, "'", "''") & ")"
            oHist.pIpAddress = pIPAddress
            oHist.LogChanges(objCommand)

        Catch ex As Exception
            'Throw New Exception(ex.Message)

        Finally



        End Try
    End Sub

    Public Function CountDocReceipt() As Integer

        Dim objCommand As clsSqlConn

        'Dim ldata As DataTable
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "DocReceipts r " & _
           "WHERE r.Receiptid is not null " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





            Return objCommand.ExecScalar


        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            'If Not ldata Is Nothing Then
            '    ldata.Dispose()
            '    ldata = Nothing
            'End If


            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function

    Public Function CheckIfReceiptExists(ByVal asReceiptId As String) As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then
            Dim osp As New cls_storedproc

            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = "SELECT TOP 1 receiptid FROM DocList WHERE receiptid = '" & asReceiptId & "'"

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
