Public Class clsDocMonitoringReceiving
    Dim SearchCriteria As String
    Dim ReceivingId As String
    Dim DocId As String
    Dim GroupCode As String
    Dim nMonth As String
    Dim nYearFrom As String
    Dim nYearTo As String
    Dim SelectedDate As String
    Dim Remarks As String
    Dim RefNo As String
    Dim ReceivedBy As String
    Dim ReceivedDate As String
    Dim EncodedBy As String
    Dim EncodedDate As String
    Dim StatusId As String
    Dim Duration As String
    Dim OtherOffice As String

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
    Public Property pYearFrom() As String
        Get
            Return nYearFrom
        End Get
        Set(ByVal value As String)
            nYearFrom = value
        End Set

    End Property
    Public Property pSelectedDate() As String
        Get
            Return SelectedDate
        End Get
        Set(ByVal value As String)
            SelectedDate = value
        End Set

    End Property
    Public Property pMonth() As String
        Get
            Return nMonth
        End Get
        Set(ByVal value As String)
            nMonth = value
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
    Public Property pOtherOffice() As String
        Get
            Return OtherOffice
        End Get
        Set(ByVal value As String)
            OtherOffice = value
        End Set
    End Property
    Public Property pReceivingId() As String
        Get
            Return ReceivingId
        End Get
        Set(ByVal value As String)
            ReceivingId = value
        End Set
    End Property
    Public Property pSearchCriteria() As String
        Get
            Return SearchCriteria
        End Get
        Set(ByVal value As String)
            SearchCriteria = value
        End Set
    End Property
    Public Property pDocId() As String
        Get
            Return DocId
        End Get
        Set(ByVal value As String)
            DocId = value
        End Set
    End Property
    Public Property pGroupCode() As String
        Get
            Return GroupCode
        End Get
        Set(ByVal value As String)
            GroupCode = value
        End Set
    End Property


    Public Property pRefNo() As String
        Get
            Return RefNo
        End Get
        Set(ByVal value As String)
            RefNo = value
        End Set
    End Property
    Public Property pReceivedBy() As String
        Get
            Return ReceivedBy
        End Get
        Set(ByVal value As String)
            ReceivedBy = value
        End Set
    End Property
    Public Property pReceivedDate() As String
        Get
            Return ReceivedDate
        End Get
        Set(ByVal value As String)
            ReceivedDate = value
        End Set
    End Property
    Public Property pStatusId() As String
        Get
            Return StatusId
        End Get
        Set(ByVal value As String)
            StatusId = value
        End Set
    End Property

    Public Property pEncodedBy() As String
        Get
            Return EncodedBy
        End Get
        Set(ByVal value As String)
            EncodedBy = value
        End Set
    End Property
    Public Property pEncodedDate() As String
        Get
            Return EncodedDate
        End Get
        Set(ByVal value As String)
            EncodedDate = value
        End Set
    End Property

    
    Public Property pDuration() As String
        Get
            Return Duration
        End Get
        Set(ByVal value As String)
            Duration = value
        End Set
    End Property
    Public Property pRemarks() As String
        Get
            Return Remarks
        End Get
        Set(ByVal value As String)
            Remarks = value
        End Set
    End Property

#End Region
#Region "CUD Methods"

    Public Sub DeleteMonitoring()

        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = "UPDATE DocMonitoringReceiving SET StatusId = 8 WHERE ReceivingId = " & pReceivingId

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
    Public Function AddMonitoring() As Integer
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "INSERT INTO DocMonitoringReceiving " & _
           "(" & _
        "DocId" & _
      ",Month" & _
      ",Year" & _
      ",ReceivedBy" & _
      ",ReceivedDate" & _
      ",EncodedBy" & _
      ",EncodedDate" & _
      ",RefNo" & _
      ",GroupCode" & _
      ",StatusId" & _
      ",Remarks" & _
")" & _
            " VALUES (" & _
"'" & Replace(pDocId, "'", "''") & "'" & _
",'" & Replace(pMonth, "'", "''") & "'" & _
",'" & Replace(pYearFrom, "'", "''") & "'" & _
",'" & Replace(pReceivedBy, "'", "''") & "'" & _
",'" & Replace(pReceivedDate, "'", "''") & "'" & _
",'" & Replace(pEncodedBy, "'", "''") & "'" & _
",'" & Replace(pEncodedDate, "'", "''") & "'" & _
",'" & Replace(pRefNo, "'", "''") & "'" & _
",'" & Replace(pGroupCode, "'", "''") & "'" & _
",'" & Replace(pStatusId, "'", "''") & "'" & _
",'" & Replace(pRemarks, "'", "''") & "'"

            If pDuration Is Nothing Then
                lsSql = lsSql & ",null"
            Else
                If pDuration = "" Then
                    lsSql = lsSql & ",null"
                Else
                    lsSql = lsSql & "," & Replace(pDuration, "'", "''") & ""
                End If
            End If

            lsSql = lsSql & ");Select SCOPE_IDENTITY(); "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            Return objCommand.ExecScalar()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function

    Public Function AddMonitoring(ByVal objCommand As clsSqlConn) As Integer


        Try
            Dim lsSql As String = "INSERT INTO DocMonitoringReceiving " & _
           "(" & _
        "DocId" & _
      ",Month" & _
      ",Year" & _
      ",ReceivedBy" & _
      ",ReceivedDate" & _
      ",EncodedBy" & _
      ",EncodedDate" & _
      ",RefNo" & _
      ",GroupCode" & _
      ",StatusId" & _
      ",Remarks" & _
")" & _
            " VALUES (" & _
"'" & Replace(pDocId, "'", "''") & "'" & _
",'" & Replace(pMonth, "'", "''") & "'" & _
",'" & Replace(pYearFrom, "'", "''") & "'" & _
",'" & Replace(pReceivedBy, "'", "''") & "'" & _
",'" & Replace(pReceivedDate, "'", "''") & "'" & _
",'" & Replace(pEncodedBy, "'", "''") & "'" & _
",'" & Replace(pEncodedDate, "'", "''") & "'" & _
",'" & Replace(pRefNo, "'", "''") & "'" & _
",'" & Replace(pGroupCode, "'", "''") & "'" & _
",'" & Replace(pStatusId, "'", "''") & "'" & _
",'" & Replace(pRemarks, "'", "''") & "'"

            If pDuration Is Nothing Then
                lsSql = lsSql & ",null"
            Else
                If pDuration = "" Then
                    lsSql = lsSql & ",null"
                Else
                    lsSql = lsSql & "," & Replace(pDuration, "'", "''") & ""
                End If
            End If

            lsSql = lsSql & ");Select SCOPE_IDENTITY(); "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            Return objCommand.ExecScalar()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If
        End Try
    End Function
    Public Sub UpdateMonitoring()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "UPDATE DocMonitoringReceiving " & _
           "SET " & _
            "EncodedDate=getDate() "

            If pReceivedBy <> "" Then
                lsSql = lsSql & ",ReceivedBy='" & Replace(pReceivedBy, "'", "''") & "' "
            End If
            If pReceivedDate <> "" Then
                lsSql = lsSql & ",ReceivedDate='" & Replace(pReceivedDate, "'", "''") & "' "
            End If
            If pGroupCode <> "" Then
                lsSql = lsSql & ",GroupCode='" & Replace(pGroupCode, "'", "''") & "'"
            End If
            If pStatusId <> "" Then
                lsSql = lsSql & ",StatusId='" & Replace(pStatusId, "'", "''") & "'"
            End If
            If pRemarks <> "" Then
                lsSql = lsSql & ",Remarks='" & Replace(pRemarks, "'", "''") & "'"
            End If

            lsSql = lsSql & " WHERE ReceivingId='" & Replace(pReceivingId, "'", "''") & "' and GroupCode = '" & pGroupCode & "' "

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
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

#End Region
#Region "Retrieval Methods"

    Public Function RetrieveMonitoring() As String
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String


            sSQL = " SELECT ReceivingId" & _
      ",DocId" & _
      ",Month" & _
      ",Year" & _
      ",ReceivedBy" & _
      ",ReceivedDate" & _
      ",EncodedBy" & _
      ",EncodedDate" & _
      ",RefNo" & _
  " FROM DocMonitoringReceiving " & _
  " WHERE month = '" & pMonth & "' and year = '" & pYearFrom & "'" & " and GroupCode = '" & pGroupCode & "' "

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL



            'ldata = objCommand.Fill

            Return objCommand.ExecScalar3

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function CountTopMonitoring() As Integer
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "select Count(cm.ReceivingId) " & _
" from DocMonitoringReceiving cm " & _
" INNER JOIN CrdMonitoringStatus ms On cm.StatusId = ms.StatusId " & _
" INNER JOIN docList dl On dl.refno = cm.RefNo " & _
" LEFT JOIN Users mbu On cm.EncodedBy = mbu.UserId " & _
                " LEFT JOIN Users rbu On cm.ReceivedBy = rbu.UserId " & _
"WHERE cm.StatusId <> 8 " & SQLWhereClause()
            

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL

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
    Private Function SQLWhereClause() As String
        Dim sSQL As String = " AND GroupCode = '" & pGroupCode & "' "
        If IsDate(pRefNo) Then
            sSQL = sSQL & " AND cm.ReceivedDate >= '" & pRefNo & "' and cm.DateTimeReceived < DateAdd(d,1,'" & pRefNo & "') "
        ElseIf pRefNo <> "" Then
            If pRefNo.Split(",").Length > 1 Then
                Dim aRefNo As Array = pRefNo.Split(",")
                Dim swhere As String = ""
                For ctr = 0 To aRefNo.Length - 1
                    If swhere = "" Then
                        sSQL = sSQL & " AND (cm.RefNo Like '%" & aRefNo(ctr).ToString.Trim & "%' "
                    Else
                        sSQL = sSQL & " OR cm.RefNo Like '%" & aRefNo(ctr).ToString.Trim & "%' "
                    End If
                    swhere = "x"

                Next
                sSQL = sSQL & ") "
            Else
                sSQL = sSQL & " AND cm.RefNo Like '%" & pRefNo & "%' "
            End If
        ElseIf pSearchCriteria <> "" Then
            If IsDate(pSearchCriteria) Then
                sSQL = sSQL & " AND cm.ReceivedDate >= '" & pSearchCriteria & "' and cm.ReceivedDate < DateAdd(d,1,'" & pSearchCriteria & "') "
            Else
                sSQL = sSQL & " AND (cm.RefNo like '%" & pSearchCriteria & "%' or cm.Remarks like '%" & pSearchCriteria & "%') "
            End If
        ElseIf pReceivedBy <> "" Then
            sSQL = sSQL & " AND (rbu.FirstName+' '+rbu.LastName like '%" & pReceivedBy & "%') "
        
        ElseIf pDuration <> "" Then
            If IsNumeric(pDuration) Then
                sSQL = sSQL & " AND (cm.Duration = " & pDuration & ") "
            End If
        ElseIf pSelectedDate <> "" Then
            If IsDate(pSelectedDate) Then
                sSQL = sSQL & " AND (dl.CreatedDate between '" & pSelectedDate & " 00:00:00' and '" & pSelectedDate & " 23:59:59') "
            End If
        End If


        'Dim lsWhere As String = DateYearWhereClause()

        
        'sSQL = sSQL & lsWhere
        Return sSQL
    End Function
    Public Function DateYearWhereClause() As String
        Dim lsWhere As String = ""
        Dim llastdate As String = ""
        If pMonth Is Nothing Then
            pMonth = Month(Date.Now).ToString
        End If
        If pYearFrom Is Nothing Then
            pYearFrom = Year(Date.Now).ToString
        End If
        If pYearFrom <> "" Then
            If pMonth = "1" OrElse pMonth = "3" OrElse pMonth = "5" OrElse pMonth = "7" OrElse pMonth = "8" OrElse pMonth = "10" OrElse pMonth = "12" Then
                llastdate = "31"
            ElseIf pMonth = "4" OrElse pMonth = "6" OrElse pMonth = "9" OrElse pMonth = "11" Then
                llastdate = "30"
            ElseIf pMonth = "2" Then
                If Date.IsLeapYear(CInt(pYearFrom)) Then
                    llastdate = "29"
                Else
                    llastdate = "28"
                End If

            End If
            lsWhere = " AND cm.ReceivedDate >= '" & pMonth & "/1/" & pYearFrom & "' and cm.ReceivedDate <= '" & pMonth & "/" & llastdate & "/" & pYearFrom & "' "
        End If
        Return lsWhere
    End Function
    Public Function RetrieveTopMonitoring() As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String
            Dim lTop As Integer
            lTop = CInt(pIdx) + CInt(pRowsPerPage) - 1

            sSQL = "SELECT * FROM (select " & _
                " TOP " & lTop.ToString & " " & _
                "rn= row_number() over (ORDER BY  " & pSortCol & " " & pSortOrder & ") " 'DateTimeReceived desc)"" & _"
            sSQL = sSQL & ",cm.ReceivingId" & _
                ",cm.DocId" & _
                ",cm.RefNo" & _
                ",cm.ReceivedBy" & _
                ",rbu.FirstName+' '+rbu.LastName as ReceivedByName" & _
                ",ISNULL(convert(varchar(10),cm.ReceivedDate, 101) + right(convert(varchar(32),cm.ReceivedDate,100),8),'') as ReceivedDate" & _
                ",convert(varchar(10),dl.CreatedDate, 101) + right(convert(varchar(32),dl.CreatedDate,100),8) as CreatedDate" & _
                ",dl.Title as Subject" & _
                ",cm.Remarks" & _
                ",cm.StatusId" & _
                ",ms.Description as StatusDesc" & _
                ",Duration,CutOffDate " & _
                ",dl.UploaderGrp " & _
                " from DocMonitoringReceiving cm " & _
                " INNER JOIN CrdMonitoringStatus ms On cm.StatusId = ms.StatusId " & _
                " INNER JOIN Doclist dl On dl.refno = cm.refno " & _
                " LEFT JOIN Users mbu On cm.EncodedBy = mbu.UserId " & _
                " LEFT JOIN Users rbu On cm.ReceivedBy = rbu.UserId " & _
                " WHERE cm.StatusId <> 8 " & SQLWhereClause()

            sSQL = sSQL & ") ps " & _
            " WHERE ps.rn between " & pIdx & " and " & pRowsPerPage & "+" & pIdx

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function RetrieveAddMonitoring() As DataTable
        Dim objCommand As clsSqlConn
        Dim sSQL As String
        Try
            objCommand = New clsSqlConn
            sSQL = " SELECT ReceivingId" & _
      ",DocId" & _
      ",Month" & _
      ",Year" & _
      ",ReceivedBy" & _
      ",ReceivedDate" & _
      ",EncodedBy" & _
      ",EncodedDate" & _
      ",RefNo" & _
  " FROM DocMonitoringReceiving " & _
  " WHERE month = '" & pMonth & "' and year = '" & pYearFrom & "'" & " and GroupCode = '" & pGroupCode & "' "

            sSQL = sSQL & " WHERE cm.ReceivingId = " & pReceivingId & " "

            'sSQL = sSQL & ") ps " 

            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = sSQL

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
    Public Function RetrieveRefNoLookup() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "SELECT docid,refno,title " & _
            " FROM doclist " & _
            " WHERE statusid <> 5 and refno = '" & pRefNo & "' "

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
    Public Function RetrieveUsers(ByVal asGroup As String) As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "SELECT userid,upper(FirstName)+' '+upper(LastName) as userName " & _
            " FROM Users " & _
            " WHERE userGroup = '" & asGroup & "' and deldate is null and (locked is null or locked = 0) ORDER BY FirstName+' '+LastName"

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

    'Public Function RetrieveDefaults() As DataTable
    '    Dim objCommand As clsSqlConn

    '    Dim ldata As DataTable

    '    Try
    '        objCommand = New clsSqlConn
    '        Dim sSQL As String

    '        sSQL = "SELECT DueDateDays,CrdGroupCode,DefaultMailer,DefaultSorter,DefaultSender,DefaultCourier " & _
    '        " FROM CrdMonitoringDefaults "


    '        objCommand.CommandType = CommandType.Text
    '        objCommand.CommandText = sSQL

    '        ldata = objCommand.Fill

    '        Return ldata

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        If Not ldata Is Nothing Then
    '            ldata.Dispose()
    '            ldata = Nothing
    '        End If

    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '    End Try

    'End Function
    'Public Function CountMonitoring() As Integer

    '    Dim objCommand As clsSqlConn
    '    Dim s_sql As String

    '    Try
    '        s_sql = "SELECT count(*) " & _
    '                 "FROM  " & _
    '        "CrdMonitoring " & _
    '       "WHERE RecordNo is not null " & WhereClause()

    '        objCommand = New clsSqlConn
    '        objCommand.CommandType = CommandType.Text

    '        objCommand.CommandText = s_sql '"XMSP_USERGET"





    '        Return objCommand.ExecScalar


    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally


    '        If Not objCommand Is Nothing Then
    '            objCommand.Dispose()
    '            objCommand = Nothing
    '        End If

    '    End Try

    'End Function


#End Region
#Region "Where Clause/Order By Methods"
    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pReceivingId <> "" Then
            lswhere = lswhere & " " & "AND ReceivingId = " & pReceivingId & " "
        End If
        If pDocId <> "" Then
            lswhere = lswhere & " " & "AND DocId = " & pDocId & " "
        End If
        If pRefNo <> "" Then
            lswhere = lswhere & " " & "AND RefNo = " & pRefNo & " "
        End If
        If pReceivedBy <> "" Then
            lswhere = lswhere & " " & "AND ReceivedBy = " & pReceivedBy & " "
        End If
        If pReceivedDate <> "" Then
            lswhere = lswhere & " " & "AND ReceivedDate = " & pReceivedDate & " "
        End If
        
        If pStatusId <> "" Then
            lswhere = lswhere & " " & "AND StatusId= " & pStatusId & " "
        End If
        
        If pDuration <> "" Then
            lswhere = lswhere & " " & "AND Duration = " & pDuration & " "
        End If
        If pRemarks <> "" Then
            lswhere = lswhere & " " & "AND Remarks = " & pRemarks & " "
        End If
        

        Return lswhere
    End Function
    
#End Region


#Region "Validate Methods"
    Public Function CheckIfMonitoringExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then


            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT TOP 1 refno FROM DocMonitoringReceiving WHERE RefNo = '" & Replace(pRefNo, "'", "''") & "'"


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

    Public Function CheckIfRefnoExists(ByVal asRefNo As String) As DataTable


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then


            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT docid,fileversion FROM DocList WHERE statusid <> 5 and RefNo = '" & Replace(asRefNo, "'", "''") & "'"

            Return objCommand.ExecData

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Function
#End Region

    Public Function RetrieveHolidays() As DataTable
        Dim lsSql = "select convert(char(10),holiday,101) as Holidate,Description from OfficeWorkHoliday where OfficeCode = '" & pOtherOffice & "' and Year = " & pYearFrom & " " & _
" And Month(Holiday) = " & pMonth
        Dim oConn As clsSqlConn
        Dim ldata As DataTable
        Try
            oConn = New clsSqlConn

            oConn.CommandType = CommandType.Text
            oConn.CommandText = lsSql

            'ldata = 

            Return oConn.ExecData() 'ldata
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not oConn Is Nothing Then
                oConn.Dispose()
            End If
        End Try
    End Function

    Public Function RetrieveSummary() As DataTable
        Dim lsSql As String = "select COUNT(case when mainstatus = 1 then 1 else null end) as Pending " & _
",COUNT(case when mainstatus = 2 then 1 else null end) as InProgress " & _
",COUNT(case when mainstatus = 3 then 1 else null end) as Complete " & _
",COUNT(case when mainstatus = 3 " & _
"and cm.DueDate > case when personaldelivery = 1 then DeliveryCompleted else MailingCompleted end  " & _
"then 1 else null end) as OnTime " & _
",COUNT(case when mainstatus = 3  " & _
"and cm.DueDate < case when personaldelivery = 1 then DeliveryCompleted else MailingCompleted end  " & _
"then 1 else null end) as OverDue,COUNT(*) as Total " & _
 "from crdMonitoring cm " & _
 "INNER JOIN CrdMonitoringStatus cms ON cms.StatusId = cm.MainStatus  " & _
 " where groupCode = '" & Replace(pGroupCode, "'", "''") & "' " & DateYearWhereClause()

        Dim oConn As clsSqlConn
        'Dim ldata As DataTable
        Try
            oConn = New clsSqlConn

            oConn.CommandType = CommandType.Text
            oConn.CommandText = lsSql

            'ldata = 

            Return oConn.ExecData() 'ldata
        Catch ex As Exception
        Finally
            If Not oConn Is Nothing Then
                oConn.Dispose()
            End If
        End Try
    End Function

    Public Function RetrieveMonitoringReceiving() As DataTable
        Dim lssql As String
        lssql = "SELECT distinct convert(char(10),createddate,101) as cdate FROM docmonitoringreceiving " & _
                " where [Month] = " & pMonth & " And [Year] = " & pYearFrom & " ORDER BY convert(char(10),createddate,101) "
        Dim oConn As clsSqlConn
        Dim ldata As DataTable
        Try
            oConn = New clsSqlConn

            oConn.CommandType = CommandType.Text
            oConn.CommandText = lssql

            'ldata = 

            Return oConn.ExecData()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not oConn Is Nothing Then
                oConn.Dispose()
            End If
        End Try
    End Function
    Public Function RetrieveSummaryReceiving() As DataTable
        Dim lssql As String
        lssql = "select RemDesc as remarks, isnull(subtotalrecv,0) as subtotalrecv,isnull(totalrcv,0) as totalrcv,OrderNo from (SELECT " & _
                " case dmr.remarks when 'Late' then 2 when 'On Time' then 1 else 3 end as OrderNo, " & _
                " case dmr.remarks when 'Late' then 'Meet Target (No)' when 'On Time' then 'Meet Target (Yes)' else 'Uploaded by other/s' end as RemDesc, " & _
                " dmr.remarks, COUNT(*) as subtotalrecv " & _
                ",totalrcv = (select count(d.receivingid) from docmonitoringreceiving d where d.[Month] = " & pMonth & " And d.[Year] = " & pYearFrom & " and (d.remarks = 'Late' or d.remarks = 'On Time')) " & _
                "FROM docmonitoringreceiving dmr " & _
                " where dmr.[Month] = " & pMonth & " And dmr.[Year] = " & pYearFrom & " " & _
                "GROUP BY dmr.remarks) a order by orderno "
        Dim oConn As clsSqlConn
        Dim ldata As DataTable
        Try
            oConn = New clsSqlConn

            oConn.CommandType = CommandType.Text
            oConn.CommandText = lssql

            'ldata = 

            Return oConn.ExecData()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not oConn Is Nothing Then
                oConn.Dispose()
            End If
        End Try
    End Function
End Class
