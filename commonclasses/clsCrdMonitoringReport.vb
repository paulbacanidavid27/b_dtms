Public Class clsCrdMonitoringReport
    Dim ReportNo As String
    Dim RecordNo As String
    Dim CreatedDate As String
    Dim CreatedBy As String
    Dim Month As String
    Dim Year As String
    Dim GroupCode As String

    Dim UserId As String

    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String

    Dim ModifiedDate As String = DateTime.Now.ToString
    Dim RowsPerPage As String
    Dim Idx As String
#Region "Creator"
    Public Sub New()

    End Sub
#End Region
#Region "Default Properties"
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
    Public Property pRowsPerPage() As String
        Get
            Return RowsPerPage
        End Get
        Set(ByVal value As String)
            RowsPerPage = value
        End Set

    End Property
#End Region
#Region "Table Columns"
    Public Property pGroupCode() As String
        Get
            Return GroupCode
        End Get
        Set(ByVal value As String)
            GroupCode = value
        End Set
    End Property
    Public Property pReportNo() As String
        Get
            Return ReportNo
        End Get
        Set(ByVal value As String)
            ReportNo = value
        End Set
    End Property
    Public Property pRecordNo() As String
        Get
            Return RecordNo
        End Get
        Set(ByVal value As String)
            RecordNo = value
        End Set
    End Property
    Public Property pCreatedDate() As String
        Get
            Return CreatedDate
        End Get
        Set(ByVal value As String)
            CreatedDate = value
        End Set
    End Property
    Public Property pCreatedBy() As String
        Get
            Return CreatedBy
        End Get
        Set(ByVal value As String)
            CreatedBy = value
        End Set
    End Property
    Public Property pMonth() As String
        Get
            Return Month
        End Get
        Set(ByVal value As String)
            Month = value
        End Set
    End Property
    Public Property pYear() As String
        Get
            Return Year
        End Get
        Set(ByVal value As String)
            Year = value
        End Set
    End Property

#End Region
#Region "CUD Methods"

    Public Sub DeleteMonitoringReport()

        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = "DELETE FROM crdMonitoringReport WHERE ReportNo = " & pReportNo

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = s_sql
            objCommand.ExecTranNonQuery()

            'Dim oHist As New DocHistory
            'oHist.pTableName = "Receipt"
            'oHist.pRecordId = Replace(pRequestType, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = ""
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue =
            'oHist.pNewValue = "Deleted Request Type (" & pRequestDescription & ")"
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objcommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try

    End Sub
    Public Function AddMonitoringReport() As Integer
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "INSERT INTO crdMonitoringReport " & _
           "(RecordNo" & _
",CreatedDate" & _
",CreatedBy" & _
",Month" & _
",GroupCode" & _
",Year)" & _
            "VALUES (" & _
"'" & Replace(pRecordNo, "'", "''") & "'" & _
",'" & Replace(pCreatedDate, "'", "''") & "'" & _
",'" & Replace(pCreatedBy, "'", "''") & "'" & _
",'" & Replace(pMonth, "'", "''") & "'" & _
",'" & Replace(pGroupCode, "'", "''") & "'" & _
",'" & Replace(pYear, "'", "''") & "'" & _
  ");Select SCOPE_IDENTITY(); "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            Return objCommand.ExecScalar()


            'Dim oHist As New DocHistory

            'oHist.pTableName = "Request"
            'oHist.pRecordId = Replace(pRequestType, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = ""
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue = ""
            'oHist.pNewValue = "Added Request Type (" & Replace(pRequestDescription, "'", "''") & ")"
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Sub UpdateMonitoringReport()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "UPDATE crdMonitoringReport " & _
   "SET RecordNo='" & Replace(pRecordNo, "'", "''") & "'" & _
",CreatedDate='" & Replace(pCreatedDate, "'", "''") & "'" & _
",CreatedBy='" & Replace(pCreatedBy, "'", "''") & "'" & _
",Month='" & Replace(pMonth, "'", "''") & "'" & _
",Year='" & Replace(pYear, "'", "''") & "'" & _
 " WHERE ReportNo " & pReportNo

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecNonQuery()


            'Dim oHist As New DocHistory

            'oHist.pTableName = "Request"
            'oHist.pRecordId = Replace(pRequestType, "'", "''")
            'oHist.pModifiedBy = Replace(DocSession.sUserId, "'", "''")
            'oHist.pColumnName = ""
            'oHist.pModifiedDate = ModifiedDate
            'oHist.pOldValue = ""
            'oHist.pNewValue = "Added Request Type (" & Replace(pRequestDescription, "'", "''") & ")"
            'oHist.pIpAddress = pIPAddress
            'oHist.LogChanges(objCommand)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Sub
    

#End Region
#Region "Retrieval Methods"

    Public Function RetrieveCrdMonitoringReport() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable
        Dim s_order As String = OrderBy()
        Dim lTop As Integer
        lTop = CInt(pIdx) * CInt(pRowsPerPage)
        Try
            objCommand = New clsSqlConn
            Dim sSQL, sWHERE As String


            sSQL = "SELECT tc.ReportNo" & _
",tc.RecordNo" & _
",tc.CreatedDate" & _
",tc.CreatedBy" & _
",tc.Month" & _
",tc.Year" & _
            "FROM crdMonitoringReport b "
            sWHERE = WhereClause()
            If sWHERE <> "" Then
                sSQL = sSQL & " WHERE " & sWHERE.Substring(4)
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

    Public Function RetrieveLatestMonitoringReport() As DataTable


        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL, sWHERE As String

            sSQL = "SELECT tc.ReportNo" & _
",tc.RecordNo" & _
",tc.CreatedDate" & _
",tc.CreatedBy" & _
",tc.Month" & _
",tc.Year" & _
",u.FirstName+' '+u.LastName as CreatedByName " & _
            "FROM crdMonitoringReport tc inner join users u on tc.CreatedBy = u.userid "
            sWHERE = WhereClause()
            If sWHERE <> "" Then
                sSQL = sSQL & " WHERE " & sWHERE.Substring(4)
            End If

            sSQL = sSQL & " ORDER BY createddate desc "
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
        If pSortCol = "Brgy. Code" Then
            Return " b.nBrgy_Code "
        ElseIf pSortCol = "Brgy. Name" Then
            Return " b.Brgy_Name "
        ElseIf pSortCol = "District Code" Then
            Return " b.nDistrict_Code "
        Else
            Return " b.IDNo "
        End If

    End Function

#End Region

    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pReportNo <> "" Then
            lswhere = lswhere & " " & "AND ReportNo = " & pReportNo & " "
        End If
        If pRecordNo <> "" Then
            lswhere = lswhere & " " & "AND RecordNo = " & pRecordNo & " "
        End If
        If pCreatedDate <> "" Then
            lswhere = lswhere & " " & "AND CreatedDate = " & pCreatedDate & " "
        End If
        If pCreatedBy <> "" Then
            lswhere = lswhere & " " & "AND CreatedBy = " & pCreatedBy & " "
        End If
        If pMonth <> "" Then
            lswhere = lswhere & " " & "AND Month = " & pMonth & " "
        End If
        If pYear <> "" Then
            lswhere = lswhere & " " & "AND Year = " & pYear & " "
        End If

        Return lswhere
    End Function


    Public Function GetMax() As Integer

        Dim objCommand As clsSqlConn


        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "crdMonitoringReport b " & _
           "WHERE b.IDNO is not null " & WhereClause()

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql '"XMSP_USERGET"





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

    Public Function CheckIfMonitoringReportExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then


            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT TOP 1 FROM crdMonitoringReport WHERE pRecordNo = '" & Replace(pRecordNo, "'", "''") & "'"


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
    'Public Function CheckIfBusinessNameExists() As Boolean


    '    Dim objCommand As clsSqlConn

    '    Try
    '        objCommand = New clsSqlConn
    '        'If DocSession.OraClient Then


    '        objCommand.pCommandType = CommandType.Text

    '        objCommand.pCommandText = "SELECT TOP 1 FROM BP_MASTER WHERE cBusiName = " & pcBusiName & ""


    '        Return objCommand.ExecHasRow()


    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '    End Try

    'End Function
End Class
