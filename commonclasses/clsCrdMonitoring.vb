Public Class clsCrdMonitoring
    Dim SearchCriteria As String
    Dim RecordNo As String
    Dim ParentRecordNo As String
    Dim OtherOffice As String
    Dim nYearFrom As String
    Dim nYearTo As String
    Dim nMonth As String
    Dim DocId As String
    Dim RefNo As String
    Dim ReceivedBy As String
    Dim DateTimeReceived As String
    Dim DateFrom As String
    Dim DateTo As String
    Dim RequestingOfcCode As String
    Dim Description As String
    Dim MainStatus As String
    Dim SortingStatus As String
    Dim SortedBy As String
    Dim SortedCompleted As String
    Dim SortedReceived As String
    Dim DeliveryStatus As String
    Dim DeliveredBy As String
    Dim PickupBy As String
    Dim DeliveryReceived As String
    Dim DeliveryCompleted As String
    Dim MailingReceived As String
    Dim MailingCompleted As String
    Dim MailingStatus As String
    Dim DueDate As String
    Dim Duration As String
    Dim Remarks As String
    Dim DateOfLetter As String
    Dim DateReceivedByRecipient As String
    Dim Location As String
    Dim MailedBy As String
    Dim CreatedBy As String
    Dim CreatedDate As String
    Dim CourierName As String
    Dim FileVersion As String
    Dim GroupCode As String
    Dim PersonalDelivery

    Dim oRecordNo As String
    Dim oParentRecordNo As String
    Dim oOtherOffice As String
    Dim onYearFrom As String
    Dim onYearTo As String
    Dim onMonth As String
    Dim oDocId As String
    Dim oRefNo As String
    Dim oReceivedBy As String
    Dim oDateTimeReceived As String
    Dim oRequestingOfcCode As String
    Dim oDescription As String
    Dim oMainStatus As String
    Dim oSortingStatus As String
    Dim oSortedBy As String
    Dim oSortedCompleted As String
    Dim oSortedReceived As String
    Dim oDeliveryStatus As String
    Dim oDeliveredBy As String
    Dim oDeliveryReceived As String
    Dim oDeliveryCompleted As String
    Dim oMailingReceived As String
    Dim oMailingCompleted As String
    Dim oMailingStatus As String
    Dim oDueDate As String
    Dim oDuration As String
    Dim oRemarks As String
    Dim oDateOfLetter As String
    Dim oDateReceivedByRecipient As String
    Dim oLocation As String
    Dim oMailedBy As String
    Dim oCreatedBy As String
    Dim oCreatedDate As String
    Dim oCourierName As String
    Dim oFileVersion As String
    Dim oGroupCode As String
    Dim oPersonalDelivery
    Dim UserId As String
    Dim SortOrder As String
    Dim SortCol As String
    Dim IPAddress As String
    Dim TrackingNumber As String
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
    Public Property pYearTo() As String
        Get
            Return nYearTo
        End Get
        Set(ByVal value As String)
            nYearTo = value
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
    Public Property pPickupBy() As String
        Get
            Return PickupBy
        End Get
        Set(ByVal value As String)
            PickupBy = value
        End Set
    End Property
    Public Property pDateFrom() As String
        Get
            Return DateFrom
        End Get
        Set(ByVal value As String)
            DateFrom = value
        End Set
    End Property
    Public Property pDateTo() As String
        Get
            Return DateTo
        End Get
        Set(ByVal value As String)
            DateTo = value
        End Set
    End Property
    Public Property pCourierName() As String
        Get
            Return CourierName
        End Get
        Set(ByVal value As String)
            CourierName = value
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
    Public Property pOtherOffice() As String
        Get
            Return OtherOffice
        End Get
        Set(ByVal value As String)
            OtherOffice = value
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
    Public Property pTrackingNumber() As String
        Get
            Return TrackingNumber
        End Get
        Set(ByVal value As String)
            TrackingNumber = value
        End Set
    End Property
    Public Property pFileVersion() As String
        Get
            Return FileVersion
        End Get
        Set(ByVal value As String)
            FileVersion = value
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
    Public Property pParentRecordNo() As String
        Get
            Return ParentRecordNo
        End Get
        Set(ByVal value As String)
            ParentRecordNo = value
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
    Public Property pDateTimeReceived() As String
        Get
            Return DateTimeReceived
        End Get
        Set(ByVal value As String)
            DateTimeReceived = value
        End Set
    End Property
    Public Property pPersonalDelivery() As String
        Get
            Return PersonalDelivery
        End Get
        Set(ByVal value As String)
            PersonalDelivery = value
        End Set

    End Property
    Public Property pRequestingOfcCode() As String
        Get
            Return RequestingOfcCode
        End Get
        Set(ByVal value As String)
            RequestingOfcCode = value
        End Set
    End Property
    Public Property pDescription() As String
        Get
            Return Description
        End Get
        Set(ByVal value As String)
            Description = value
        End Set
    End Property
    Public Property pMainStatus() As String
        Get
            Return MainStatus
        End Get
        Set(ByVal value As String)
            MainStatus = value
        End Set
    End Property
    Public Property pSortingStatus() As String
        Get
            Return SortingStatus
        End Get
        Set(ByVal value As String)
            SortingStatus = value
        End Set
    End Property
    Public Property pSortedBy() As String
        Get
            Return SortedBy
        End Get
        Set(ByVal value As String)
            SortedBy = value
        End Set
    End Property
    Public Property pSortedCompleted() As String
        Get
            Return SortedCompleted
        End Get
        Set(ByVal value As String)
            SortedCompleted = value
        End Set
    End Property
    Public Property pSortedReceived() As String
        Get
            Return SortedReceived
        End Get
        Set(ByVal value As String)
            SortedReceived = value
        End Set
    End Property
    Public Property pDeliveryStatus() As String
        Get
            Return DeliveryStatus
        End Get
        Set(ByVal value As String)
            DeliveryStatus = value
        End Set
    End Property
    Public Property pDeliveredBy() As String
        Get
            Return DeliveredBy
        End Get
        Set(ByVal value As String)
            DeliveredBy = value
        End Set
    End Property
    Public Property pMailedBy() As String
        Get
            Return MailedBy
        End Get
        Set(ByVal value As String)
            MailedBy = value
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
    Public Property pCreatedDate() As String
        Get
            Return CreatedDate
        End Get
        Set(ByVal value As String)
            CreatedDate = value
        End Set
    End Property
    Public Property pDeliveryReceived() As String
        Get
            Return DeliveryReceived
        End Get
        Set(ByVal value As String)
            DeliveryReceived = value
        End Set
    End Property
    Public Property pDeliveryCompleted() As String
        Get
            Return DeliveryCompleted
        End Get
        Set(ByVal value As String)
            DeliveryCompleted = value
        End Set
    End Property
    Public Property pMailingReceived() As String
        Get
            Return MailingReceived
        End Get
        Set(ByVal value As String)
            MailingReceived = value
        End Set
    End Property
    Public Property pMailingCompleted() As String
        Get
            Return MailingCompleted
        End Get
        Set(ByVal value As String)
            MailingCompleted = value
        End Set
    End Property
    Public Property pMailingStatus() As String
        Get
            Return MailingStatus
        End Get
        Set(ByVal value As String)
            MailingStatus = value
        End Set
    End Property
    Public Property pDueDate() As String
        Get
            Return DueDate
        End Get
        Set(ByVal value As String)
            DueDate = value
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
    Public Property pDateOfLetter() As String
        Get
            Return DateOfLetter
        End Get
        Set(ByVal value As String)
            DateOfLetter = value
        End Set
    End Property
    Public Property pDateReceivedByRecipient() As String
        Get
            Return DateReceivedByRecipient
        End Get
        Set(ByVal value As String)
            DateReceivedByRecipient = value
        End Set
    End Property
    Public Property pLocation() As String
        Get
            Return Location
        End Get
        Set(ByVal value As String)
            Location = value
        End Set
    End Property


#End Region
#Region "CUD Methods"

    Public Sub DeleteMonitoring()

        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn
            Dim s_sql As String = "UPDATE CrdMonitoring SET MainStatus = 8 WHERE RecordNo = " & pRecordNo

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


            Dim lsSql As String = "INSERT INTO CrdMonitoring " &
           "(" &
"ParentRecordNo" &
",DocId" &
",RefNo" &
",ReceivedBy" &
",DateTimeReceived" &
",RequestingOfcCode" &
",Description" &
",MainStatus" &
",SortingStatus" &
",SortedBy" &
",SortedCompleted" &
",SortedReceived" &
",DeliveryStatus" &
",DeliveredBy" &
",DeliveryReceived" &
",DeliveryCompleted" &
",MailingReceived" &
",MailingCompleted" &
",MailingStatus" &
",DueDate" &
",Duration" &
",Remarks" &
",DateOfLetter" &
",DateReceivedByRecipient" &
",Location" &
",MailedBy" &
",CreatedBy" &
",CreatedDate" &
",CourierName" &
",FileVersion" &
",GroupCode" &
",OtherOffice" &
",PersonalDelivery" &
",PickupBy" &
")" &
            " VALUES (" &
"'" & Replace(pParentRecordNo, "'", "''") & "'" &
",'" & Replace(pDocId, "'", "''") & "'" &
",'" & Replace(Left(pRefNo, 250), "'", "''") & "'" &
",'" & Replace(pReceivedBy, "'", "''") & "'" &
",'" & Replace(pDateTimeReceived, "'", "''") & "'" &
",'" & Replace(pRequestingOfcCode, "'", "''") & "'" &
",'" & Replace(Left(pDescription, 200), "'", "''") & "'" &
",'" & Replace(pMainStatus, "'", "''") & "'" &
",'" & Replace(pSortingStatus, "'", "''") & "'" &
",'" & Replace(pSortedBy, "'", "''") & "'" &
",'" & Replace(pSortedCompleted, "'", "''") & "'" &
",'" & Replace(pSortedReceived, "'", "''") & "'" &
",'" & Replace(pDeliveryStatus, "'", "''") & "'" &
",'" & Replace(pDeliveredBy, "'", "''") & "'" &
",'" & Replace(pDeliveryReceived, "'", "''") & "'" &
",'" & Replace(pDeliveryCompleted, "'", "''") & "'" &
",'" & Replace(pMailingReceived, "'", "''") & "'" &
",'" & Replace(pMailingCompleted, "'", "''") & "'" &
",'" & Replace(pMailingStatus, "'", "''") & "'" &
",'" & Replace(pDueDate, "'", "''") & "'"

            If pDuration Is Nothing Then
                lsSql = lsSql & ",null"
            Else
                If pDuration = "" Then
                    lsSql = lsSql & ",null"
                Else
                    lsSql = lsSql & "," & Replace(pDuration, "'", "''") & ""
                End If
            End If

            lsSql = lsSql & ",'" & Replace(Left(pRemarks, 200), "'", "''") & "'" &
",'" & Replace(pDateOfLetter, "'", "''") & "'" &
",'" & Replace(pDateReceivedByRecipient, "'", "''") & "'" &
",'" & Replace(pLocation, "'", "''") & "'" &
",'" & Replace(pMailedBy, "'", "''") & "'" &
",'" & Replace(pCreatedBy, "'", "''") & "'" &
",'" & Replace(pCreatedDate, "'", "''") & "'" &
",'" & Replace(pCourierName, "'", "''") & "'" &
",'" & Replace(pFileVersion, "'", "''") & "'" &
",'" & Replace(pGroupCode, "'", "''") & "'" &
",'" & Replace(Left(pOtherOffice, 200), "'", "''") & "'" &
",'" & Replace(pPersonalDelivery, "'", "''") & "'" &
",'" & Replace(pPickupBy, "'", "''") & "'" &
            ");Select SCOPE_IDENTITY(); "
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

    Public Sub AddMonitoring(ByVal objCommand As clsSqlConn)

        Try



            Dim lsSql As String = "INSERT INTO CrdMonitoring " &
           "(" &
"ParentRecordNo" &
",DocId" &
",RefNo" &
",ReceivedBy" &
",DateTimeReceived" &
",RequestingOfcCode" &
",Description" &
",MainStatus" &
",SortingStatus" &
",SortedBy" &
",SortedCompleted" &
",SortedReceived" &
",DeliveryStatus" &
",DeliveredBy" &
",DeliveryReceived" &
",DeliveryCompleted" &
",MailingReceived" &
",MailingCompleted" &
",MailingStatus" &
",DueDate" &
",Duration" &
",Remarks" &
",DateOfLetter" &
",DateReceivedByRecipient" &
",Location" &
",MailedBy" &
",CreatedBy" &
",CreatedDate" &
",CourierName" &
",TrackingNumber" &
",FileVersion" &
",GroupCode" &
",OtherOffice" &
",PersonalDelivery" &
")" &
            " VALUES (" &
"'" & Replace(pParentRecordNo, "'", "''") & "'" &
",'" & Replace(pDocId, "'", "''") & "'" &
",'" & Replace(Left(pRefNo, 250), "'", "''") & "'" &
",'" & Replace(pReceivedBy, "'", "''") & "'" &
",'" & Replace(pDateTimeReceived, "'", "''") & "'" &
",'" & Replace(pRequestingOfcCode, "'", "''") & "'" &
",'" & Replace(Left(pDescription, 200), "'", "''") & "'" &
",'" & Replace(pMainStatus, "'", "''") & "'" &
",'" & Replace(pSortingStatus, "'", "''") & "'" &
",'" & Replace(pSortedBy, "'", "''") & "'" &
",'" & Replace(pSortedCompleted, "'", "''") & "'" &
",'" & Replace(pSortedReceived, "'", "''") & "'" &
",'" & Replace(pDeliveryStatus, "'", "''") & "'" &
",'" & Replace(pDeliveredBy, "'", "''") & "'" &
",'" & Replace(pDeliveryReceived, "'", "''") & "'" &
",'" & Replace(pDeliveryCompleted, "'", "''") & "'" &
",'" & Replace(pMailingReceived, "'", "''") & "'" &
",'" & Replace(pMailingCompleted, "'", "''") & "'" &
",'" & Replace(pMailingStatus, "'", "''") & "'" &
",'" & Replace(pDueDate, "'", "''") & "'"

            If pDuration Is Nothing Then
                lsSql = lsSql & ",null"
            Else
                If pDuration = "" Then
                    lsSql = lsSql & ",null"
                Else
                    lsSql = lsSql & "," & Replace(pDuration, "'", "''") & ""
                End If
            End If

            lsSql = lsSql & ",'" & Replace(Left(pRemarks, 200), "'", "''") & "'" &
",'" & Replace(pDateOfLetter, "'", "''") & "'" &
",'" & Replace(pDateReceivedByRecipient, "'", "''") & "'" &
",'" & Replace(pLocation, "'", "''") & "'" &
",'" & Replace(pMailedBy, "'", "''") & "'" &
",'" & Replace(pCreatedBy, "'", "''") & "'" &
",'" & Replace(pCreatedDate, "'", "''") & "'" &
",'" & Replace(pCourierName, "'", "''") & "'" &
",'" & Replace(pTrackingNumber, "'", "''") & "'" &
",'" & Replace(pFileVersion, "'", "''") & "'" &
",'" & Replace(pGroupCode, "'", "''") & "'" &
",'" & Replace(Left(pOtherOffice, 200), "'", "''") & "'" &
",'" & Replace(pPersonalDelivery, "'", "''") & "'" &
            ") "
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = lsSql
            objCommand.ExecTranNonQuery()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Sub
    Public Sub UpdateMonitoring()
        Dim objCommand As clsSqlConn
        Try
            objCommand = New clsSqlConn


            Dim lsSql As String = "UPDATE CrdMonitoring " & _
           "SET " & _
            "PersonalDelivery=" & pPersonalDelivery & " "

            If pParentRecordNo <> "" Then
                lsSql = lsSql & ",ParentRecordNo='" & Replace(pParentRecordNo, "'", "''") & "'"
            End If
            If pRefNo <> "" Then
                lsSql = lsSql & ",RefNo='" & Replace(pRefNo, "'", "''") & "'"
            End If
            If pReceivedBy <> "" Then
                lsSql = lsSql & ",ReceivedBy='" & Replace(pReceivedBy, "'", "''") & "'"
            End If
            If pDateTimeReceived <> "" Then
                lsSql = lsSql & ",DateTimeReceived='" & Replace(pDateTimeReceived, "'", "''") & "'"
            End If
            If pRequestingOfcCode <> "" Then
                lsSql = lsSql & ",RequestingOfcCode='" & Replace(pRequestingOfcCode, "'", "''") & "'"
            End If
            If pDescription <> "" Then
                lsSql = lsSql & ",Description='" & Replace(pDescription, "'", "''") & "'"
            End If
            If pMainStatus <> "" Then
                lsSql = lsSql & ",MainStatus='" & Replace(pMainStatus, "'", "''") & "'"
            End If
            If pSortingStatus <> "" Then
                lsSql = lsSql & ",SortingStatus='" & Replace(pSortingStatus, "'", "''") & "'"
            End If
            If pSortedBy <> "" Then
                lsSql = lsSql & ",SortedBy='" & Replace(pSortedBy, "'", "''") & "'"
            End If
            If pSortedCompleted <> "" Then
                lsSql = lsSql & ",SortedCompleted='" & Replace(pSortedCompleted, "'", "''") & "'"
            End If
            If pSortedReceived <> "" Then
                lsSql = lsSql & ",SortedReceived='" & Replace(pSortedReceived, "'", "''") & "'"
            End If
            'delivery
            If pDeliveryStatus <> "" Then
                lsSql = lsSql & ",DeliveryStatus='" & Replace(pDeliveryStatus, "'", "''") & "'"
            End If
            If pDeliveredBy <> "" Then
                lsSql = lsSql & ",DeliveredBy='" & Replace(pDeliveredBy, "'", "''") & "'"
            End If
            If pDeliveryReceived <> "" Then
                lsSql = lsSql & ",DeliveryReceived='" & Replace(pDeliveryReceived, "'", "''") & "'"
            End If
            If pDeliveryCompleted <> "" Then
                lsSql = lsSql & ",DeliveryCompleted='" & Replace(pDeliveryCompleted, "'", "''") & "'"
            End If
            If pPickupBy <> "" Then
                lsSql = lsSql & ",PickupBy='" & Replace(pPickupBy, "'", "''") & "' "
            End If

            'end delivery
            If pMailingReceived <> "" Then
                lsSql = lsSql & ",MailingReceived='" & Replace(pMailingReceived, "'", "''") & "'"
            End If
            If pMailingCompleted <> "" Then
                lsSql = lsSql & ",MailingCompleted='" & Replace(pMailingCompleted, "'", "''") & "'"
            End If
            If pMailingStatus <> "" Then
                lsSql = lsSql & ",MailingStatus='" & Replace(pMailingStatus, "'", "''") & "'"
            End If
            If pCourierName <> "" Then
                lsSql = lsSql & ",CourierName='" & Replace(pCourierName, "'", "''") & "'"
            End If
            'If pTrackingNumber <> "" Then
            lsSql = lsSql & ",TrackingNumber='" & Replace(pTrackingNumber, "'", "''") & "'"
            'End If
            If pDateOfLetter <> "" Then
                lsSql = lsSql & ",DateOfLetter='" & Replace(pDateOfLetter, "'", "''") & "'"
            End If
            If pOtherOffice <> "" Then
                lsSql = lsSql & ",OtherOffice='" & Replace(pOtherOffice, "'", "''") & "'"
            End If
            If pDateReceivedByRecipient <> "" Then
                lsSql = lsSql & ",DateReceivedByRecipient='" & Replace(pDateReceivedByRecipient, "'", "''") & "'"
            End If
            If pLocation <> "" Then
                lsSql = lsSql & ",Location='" & Replace(pLocation, "'", "''") & "'"
            End If

            If pDuration <> "" Then
                lsSql = lsSql & ",Duration=" & pDuration & " "
            End If
            If pDueDate <> "" Then
                lsSql = lsSql & ",DueDate='" & pDueDate & "' "
            End If
            If pRemarks <> "" Then
                lsSql = lsSql & ",Remarks='" & Replace(pRemarks, "'", "''") & "' "
            End If

            lsSql = lsSql & " WHERE RecordNo='" & Replace(pRecordNo, "'", "''") & "' and GroupCode = '" & pGroupCode & "' "

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


            sSQL = "select " & _
"RecordNo" & _
",ParentRecordNo" & _
",DocId" & _
",RefNo" & _
",ReceivedBy" & _
",DateTimeReceived" & _
",RequestingOfcCode" & _
",Description" & _
",MainStatus" & _
",SortingStatus" & _
",SortedBy" & _
",SortedCompleted" & _
",SortedReceived" & _
",DeliveryStatus" & _
",DeliveredBy" & _
",DeliveryReceived" & _
",DeliveryCompleted" & _
",MailingReceived" & _
",MailingCompleted" & _
",MailingStatus" & _
",DueDate" & _
",isnull(Duration,0) as Duration" & _
",Remarks" & _
",DateOfLetter" & _
",DateReceivedByRecipient" & _
",Location " & _
",Isnull(PersonalDelivery,0) as PersonalDelivery " & _
" from CrdMonitoring where RecordNo = " & pRecordNo & " and GroupCode = '" & pGroupCode & "' "

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

            sSQL = "select Count(RecordNo) " & _
" from CrdMonitoring cm " & _
" INNER JOIN CrdMonitoringStatus ms On cm.MainStatus = ms.StatusId " & _
" LEFT JOIN Users mbu On cm.MailedBy = mbu.UserId " & _
                " LEFT JOIN Users sbu On cm.SortedBy = sbu.UserId " & _
                " LEFT JOIN Users dbu On cm.DeliveredBy = dbu.UserId " & _
                " LEFT JOIN Users rbu On cm.ReceivedBy = rbu.UserId " & _
                " LEFT JOIN Office o On cm.RequestingOfcCode = o.OfficeCode " & _
"WHERE MainStatus <> 8 " & SQLWhereClause()
            'If IsDate(pRefNo) Then
            '    sSQL = sSQL & " AND cm.DateTimeReceived >= '" & pRefNo & "' and cm.DateTimeReceived < DateAdd(d,1,'" & pRefNo & "') " & " and GroupCode = '" & pGroupCode & "' "
            'ElseIf pRefNo <> "" Then
            '    If pRefNo.Split(",").Length > 1 Then
            '        Dim aRefNo As Array = pRefNo.Split(",")
            '        Dim swhere As String = ""
            '        For ctr = 0 To aRefNo.Length - 1
            '            If swhere = "" Then
            '                sSQL = sSQL & " AND (cm.RefNo Like '%" & aRefNo(ctr).ToString.Trim & "%' "
            '            Else
            '                sSQL = sSQL & " OR cm.RefNo Like '%" & aRefNo(ctr).ToString.Trim & "%' "
            '            End If
            '            swhere = "x"

            '        Next
            '        sSQL = sSQL & ") " & " and GroupCode = '" & pGroupCode & "' "
            '    Else
            '        sSQL = sSQL & " AND cm.RefNo Like '%" & pRefNo & "%' " & " and GroupCode = '" & pGroupCode & "' "
            '    End If
            'End If
            ''sSQL = sSQL & " ORDER BY DateTimeReceived desc "

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
            sSQL = sSQL & " AND cm.DateTimeReceived >= '" & pRefNo & "' and cm.DateTimeReceived < DateAdd(d,1,'" & pRefNo & "') "
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
                sSQL = sSQL & " AND cm.DateTimeReceived >= '" & pSearchCriteria & "' and cm.DateTimeReceived < DateAdd(d,1,'" & pSearchCriteria & "') "
            Else
                sSQL = sSQL & " AND (cm.RefNo like '%" & pSearchCriteria & "%' or cm.Description like '%" & pSearchCriteria & "%' or cm.Remarks like '%" & pSearchCriteria & "%' or o.Description like '%" & pSearchCriteria & "%') "
            End If
        ElseIf pDueDate <> "" Then
            sSQL = sSQL & " AND (cm.DueDate between '" & pDueDate & " 00:00:00' and '" & pDueDate & " 23:59:59') "
        ElseIf pDateOfLetter <> "" Then
            sSQL = sSQL & " AND (cm.DateOfLetter between '" & pDateOfLetter & " 00:00:00' and '" & pDateOfLetter & " 23:59:59') "
        ElseIf pDeliveryCompleted <> "" Then
            sSQL = sSQL & " AND (cm.DeliveryCompleted between '" & pDeliveryCompleted & " 00:00:00' and '" & pDeliveryCompleted & " 23:59:59') "
        ElseIf pSortedCompleted <> "" Then
            sSQL = sSQL & " AND (cm.SortedCompleted between '" & pSortedCompleted & " 00:00:00' and '" & pSortedCompleted & " 23:59:59') "
        ElseIf pMailingCompleted <> "" Then
            sSQL = sSQL & " AND (cm.MailingCompleted between '" & pMailingCompleted & " 00:00:00' and '" & pMailingCompleted & " 23:59:59') "
        ElseIf pDateReceivedByRecipient <> "" Then
            sSQL = sSQL & " AND (cm.DateReceivedByRecipient between '" & pDateReceivedByRecipient & " 00:00:00' and '" & pDateReceivedByRecipient & " 23:59:59') "
        ElseIf pReceivedBy <> "" Then
            sSQL = sSQL & " AND (rbu.FirstName+' '+rbu.LastName like '%" & pReceivedBy & "%') "
        ElseIf pMailedBy <> "" Then
            sSQL = sSQL & " AND (mbu.FirstName+' '+mbu.LastName like '%" & pMailedBy & "%') "
        ElseIf pSortedBy <> "" Then
            sSQL = sSQL & " AND (sbu.FirstName+' '+sbu.LastName like '%" & pSortedBy & "%') "
        ElseIf pDeliveredBy <> "" Then
            sSQL = sSQL & " AND (dbu.FirstName+' '+dbu.LastName like '%" & pDeliveredBy & "%') "
        ElseIf pDescription <> "" Then
            sSQL = sSQL & " AND (cm.Description like '%" & pDescription & "%') "
        ElseIf pCourierName <> "" Then
            sSQL = sSQL & " AND (cm.CourierName like '%" & pCourierName & "%') "
        ElseIf pDuration <> "" Then
            If IsNumeric(pDuration) Then
                sSQL = sSQL & " AND (cm.Duration = " & pDuration & ") "
            End If
        ElseIf pLocation <> "" Then
            sSQL = sSQL & " AND (cm.Location like '%" & pLocation & "%') "
        ElseIf pRemarks <> "" Then

            sSQL = sSQL & " AND (cm.Remarks like '%" & pRemarks & "%') "
        
        ElseIf pRequestingOfcCode <> "" Then

            sSQL = sSQL & " AND (o.Description like '%" & pRequestingOfcCode & "%') "
        End If


            Dim lsWhere As String = DateYearWhereClause()

            'If pYearFrom <> "" AndAlso pYearTo <> "" Then
            '    If pMonth = "" Then
            '        lsWhere = " AND cm.DateTimeReceived >= '1/1/" & pYearFrom & "' and cm.DateTimeReceived <= '12/31/" & pYearTo & "' "
            '    Else
            '        lsWhere = " AND cm.DateTimeReceived >= '" & pMonth & "/1/" & pYearFrom & "' and cm.DateTimeReceived <= '" & pMonth & "/31/" & pYearTo & "' "
            '    End If
            'ElseIf pYearFrom <> "" AndAlso pYearTo = "" Then
            '    If pMonth = "" Then
            '        lsWhere = " AND cm.DateTimeReceived >= '" & "1/1/" & pYearFrom & "' "
            '    Else
            '        lsWhere = " AND cm.DateTimeReceived >= '" & pMonth & "/1/" & pYearFrom & "' "
            '    End If
            'ElseIf pYearFrom = "" AndAlso pYearTo <> "" Then
            '    If pMonth = "" Then
            '        lsWhere = " AND cm.DateTimeReceived <= '" & "12/1/" & pYearTo & "' "
            '    Else
            '        lsWhere = " AND cm.DateTimeReceived <= '" & pMonth & "/1/" & pYearTo & "' "
            '    End If
            'End If
            sSQL = sSQL & lsWhere
            Return sSQL
    End Function
    Public Function DateYearWhereClause() As String
        Dim lsWhere As String = ""
        Dim llastdate As String = ""
        'If pMonth Is Nothing Then
        '    pMonth = Month(Date.Now).ToString
        'End If
        'If pYearFrom Is Nothing Then
        '    pYearFrom = Year(Date.Now).ToString
        'End If
        If Not pMonth Is Nothing AndAlso pMonth <> "" Then
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
            If pYearTo <> "" AndAlso pYearFrom <> "" Then
                lsWhere = " AND cm.DateTimeReceived >= '" & pMonth & "/1/" & pYearFrom & "' and cm.DateTimeReceived <= '" & pMonth & "/" & llastdate & "/" & pYearTo & "' "
            ElseIf pYearFrom <> "" Then
                lsWhere = " AND cm.DateTimeReceived >= '" & pMonth & "/1/" & pYearFrom & "' "
            ElseIf pYearTo <> "" Then
                lsWhere = " AND cm.DateTimeReceived <= '" & pMonth & "/" & llastdate & "/" & pYearTo & "' "
            End If
        Else
            If pYearTo <> "" AndAlso pYearFrom <> "" Then
                lsWhere = " AND cm.DateTimeReceived >= '1/1/" & pYearFrom & "' and cm.DateTimeReceived <= '12/31/" & pYearTo & "' "
            ElseIf pYearFrom <> "" Then
                lsWhere = " AND cm.DateTimeReceived >= '1/1/" & pYearFrom & "' "
            ElseIf pYearTo <> "" Then
                lsWhere = " AND cm.DateTimeReceived <= '12/31/" & pYearTo & "' "
            End If
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
            sSQL = sSQL & ",cm.RecordNo" &
                ",cm.ParentRecordNo" &
                ",cm.DocId" &
                ",cm.RefNo" &
                ",cm.ReceivedBy" &
                ",rbu.FirstName+' '+rbu.LastName as ReceivedByName" &
                ",Convert(char(10),cm.DateTimeReceived,101) as DateReceived" &
                ",CONVERT(varchar(15),CAST(cm.DateTimeReceived AS TIME),100) as TimeReceived" &
                ",cm.RequestingOfcCode" &
                ",isnull(o.Description,'') as RequestingOfcDesc" &
                ",isnull(cm.TrackingNumber,'') as TrackingNumber" &
                ",cm.Description" &
                ",cm.MainStatus" &
                ",ms.Description as MainStatusDesc" &
                ",cm.SortingStatus" &
                ",ss.Description as SortingStatusDesc" &
                ",cm.SortedBy" &
                ",isnull(sbu.FirstName,'')+' '+isnull(sbu.LastName,'') as SortedByName" &
                ",cm.SortedCompleted" &
                ",cm.SortedReceived" &
                ",cm.DeliveryStatus" &
                ",ds.Description as DeliveryStatusDesc" &
                ",cm.DeliveredBy" &
                ",isnull(dbu.FirstName,'')+' '+isnull(dbu.LastName,'') as DeliveredByName" &
                ",cm.DeliveryReceived" &
                ",cm.DeliveryCompleted" &
                ",PickupBy = isnull(cm.PickupBy,'')" &
                ",cm.MailingReceived" &
                ",cm.MailingCompleted" &
                ",cm.MailingStatus" &
                ",mls.Description as MailingStatusDesc" &
                ",convert(char(10),cm.DueDate,101) as DueDate" &
                ",isnull(cm.Duration,0) as Duration" &
                ",cm.Remarks" &
                ",cm.DateOfLetter" &
                ",cm.DateReceivedByRecipient" &
                ",cm.Location " &
                ",cm.MailedBy " &
                ",cm.CourierName " &
                ",cm.FileVersion " &
                ",isnull(cm.OtherOffice,'') as OtherOffice" &
                ",isnull(mbu.FirstName,'')+' '+isnull(mbu.LastName,'') as MailedByName" &
                ",Isnull(PersonalDelivery,0) as PersonalDelivery " &
                " from CrdMonitoring cm " &
                " INNER JOIN CrdMonitoringStatus ms On cm.MainStatus = ms.StatusId " &
                " LEFT JOIN CrdMonitoringStatus mls On cm.MailingStatus = mls.StatusId " &
                " LEFT JOIN CrdMonitoringStatus ss On cm.SortingStatus = ss.StatusId " &
                " LEFT JOIN CrdMonitoringStatus ds On cm.DeliveryStatus = ds.StatusId " &
                " LEFT JOIN Users mbu On cm.MailedBy = mbu.UserId " &
                " LEFT JOIN Users sbu On cm.SortedBy = sbu.UserId " &
                " LEFT JOIN Users dbu On cm.DeliveredBy = dbu.UserId " &
                " LEFT JOIN Users rbu On cm.ReceivedBy = rbu.UserId " &
                " LEFT JOIN Office o On cm.RequestingOfcCode = o.OfficeCode " &
                " WHERE cm.Mainstatus <> 8 " & SQLWhereClause()

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

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "select " & _
                "rn= row_number() over (ORDER BY DateTimeReceived desc)" '" & SortOrder & " " & pSortOrder & ") " & _
            sSQL = sSQL & ",cm.RecordNo" &
                ",cm.ParentRecordNo" &
                ",cm.DocId" &
                ",cm.RefNo" &
                ",cm.ReceivedBy" &
                ",rbu.FirstName+' '+rbu.LastName as ReceivedByName" &
                ",Convert(char(10),cm.DateTimeReceived,101) as DateReceived" &
                ",CONVERT(varchar(15),CAST(cm.DateTimeReceived AS TIME),100) as TimeReceived" &
                ",cm.RequestingOfcCode" &
                ",isnull(o.Description,'') as RequestingOfcDesc" &
                ",isnull(cm.TrackingNumber,'') as TrackingNumber" &
                ",cm.Description" &
                ",cm.MainStatus" &
                ",ms.Description as MainStatusDesc" &
                ",cm.SortingStatus" &
                ",ss.Description as SortingStatusDesc" &
                ",cm.SortedBy" &
                ",isnull(sbu.FirstName,'')+' '+isnull(rbu.LastName,'') as SortedByName" &
                ",cm.SortedCompleted" &
                ",cm.SortedReceived" &
                ",cm.DeliveryStatus" &
                ",ds.Description as DeliveryStatusDesc" &
                ",cm.DeliveredBy" &
                ",isnull(dbu.FirstName,'')+' '+isnull(dbu.LastName,'') as DeliveredByName" &
                ",cm.DeliveryReceived" &
                ",cm.DeliveryCompleted" &
                ",cm.MailingReceived" &
                ",cm.MailingCompleted" &
                ",cm.MailingStatus" &
                ",mls.Description as MailingStatusDesc" &
                ",convert(char(10),cm.DueDate,101) as DueDate" &
                ",isnull(cm.Duration,0) as Duration" &
                ",cm.Remarks" &
                ",cm.DateOfLetter" &
                ",cm.DateReceivedByRecipient" &
                ",cm.Location " &
                ",cm.MailedBy " &
                ",cm.CourierName " &
                ",cm.FileVersion " &
                ",isnull(cm.OtherOffice,'') as OtherOffice" &
                ",isnull(mbu.FirstName,'')+' '+isnull(mbu.LastName,'') as MailedByName" &
                ",Isnull(PersonalDelivery,0) as PersonalDelivery " &
                ",Isnull(PickupBy,'') as PickupBy " &
                " from CrdMonitoring cm " &
                " INNER JOIN CrdMonitoringStatus ms On cm.MainStatus = ms.StatusId " &
                " LEFT JOIN CrdMonitoringStatus mls On cm.MailingStatus = mls.StatusId " &
                " LEFT JOIN CrdMonitoringStatus ss On cm.SortingStatus = ss.StatusId " &
                " LEFT JOIN CrdMonitoringStatus ds On cm.DeliveryStatus = ds.StatusId " &
                " LEFT JOIN Users mbu On cm.MailedBy = mbu.UserId " &
                " LEFT JOIN Users sbu On cm.SortedBy = sbu.UserId " &
                " LEFT JOIN Users dbu On cm.DeliveredBy = dbu.UserId " &
                " LEFT JOIN Users rbu On cm.ReceivedBy = rbu.UserId " &
                " LEFT JOIN Office o On cm.RequestingOfcCode = o.OfficeCode"

            sSQL = sSQL & " WHERE cm.RecordNo= " & pRecordNo & " "

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

    Public Function RetrieveDefaults() As DataTable
        Dim objCommand As clsSqlConn

        Dim ldata As DataTable

        Try
            objCommand = New clsSqlConn
            Dim sSQL As String

            sSQL = "SELECT DueDateDays,CrdGroupCode,DefaultMailer,DefaultSorter,DefaultSender,DefaultCourier,DefaultUser " & _
            " FROM CrdMonitoringDefaults "


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
    Public Function CountMonitoring() As Integer

        Dim objCommand As clsSqlConn
        Dim s_sql As String

        Try
            s_sql = "SELECT count(*) " & _
                     "FROM  " & _
            "CrdMonitoring " & _
           "WHERE RecordNo is not null " & WhereClause()

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


#End Region
#Region "Where Clause/Order By Methods"
    Private Function WhereClause() As String
        Dim lswhere As String = ""
        If pRecordNo <> "" Then
            lswhere = lswhere & " " & "AND RecordNo = " & pRecordNo & " "
        End If
        If pParentRecordNo <> "" Then
            lswhere = lswhere & " " & "AND ParentRecordNo = " & pParentRecordNo & " "
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
        If pDateTimeReceived <> "" Then
            lswhere = lswhere & " " & "AND DateTimeReceived = " & pDateTimeReceived & " "
        End If
        If pRequestingOfcCode <> "" Then
            lswhere = lswhere & " " & "AND RequestingOfcCode = " & pRequestingOfcCode & " "
        End If
        If pDescription <> "" Then
            lswhere = lswhere & " " & "AND Description = " & pDescription & " "
        End If
        If pMainStatus <> "" Then
            lswhere = lswhere & " " & "AND MainStatus = " & pMainStatus & " "
        End If
        If pSortingStatus <> "" Then
            lswhere = lswhere & " " & "AND SortingStatus = " & pSortingStatus & " "
        End If
        If pSortedBy <> "" Then
            lswhere = lswhere & " " & "AND SortedBy = " & pSortedBy & " "
        End If
        If pSortedCompleted <> "" Then
            lswhere = lswhere & " " & "AND SortedCompleted = " & pSortedCompleted & " "
        End If
        If pSortedReceived <> "" Then
            lswhere = lswhere & " " & "AND SortedReceived = " & pSortedReceived & " "
        End If
        If pDeliveryStatus <> "" Then
            lswhere = lswhere & " " & "AND DeliveryStatus = " & pDeliveryStatus & " "
        End If
        If pDeliveredBy <> "" Then
            lswhere = lswhere & " " & "AND DeliveredBy = " & pDeliveredBy & " "
        End If
        If pDeliveryReceived <> "" Then
            lswhere = lswhere & " " & "AND DeliveryReceived = " & pDeliveryReceived & " "
        End If
        If pDeliveryCompleted <> "" Then
            lswhere = lswhere & " " & "AND DeliveryCompleted = " & pDeliveryCompleted & " "
        End If
        If pMailingReceived <> "" Then
            lswhere = lswhere & " " & "AND MailingReceived = " & pMailingReceived & " "
        End If
        If pMailingCompleted <> "" Then
            lswhere = lswhere & " " & "AND MailingCompleted = " & pMailingCompleted & " "
        End If
        If pMailingStatus <> "" Then
            lswhere = lswhere & " " & "AND MailingStatus = " & pMailingStatus & " "
        End If
        If pDueDate <> "" Then
            lswhere = lswhere & " " & "AND DueDate = " & pDueDate & " "
        End If
        If pDuration <> "" Then
            lswhere = lswhere & " " & "AND Duration = " & pDuration & " "
        End If
        If pRemarks <> "" Then
            lswhere = lswhere & " " & "AND Remarks = " & pRemarks & " "
        End If
        If pDateOfLetter <> "" Then
            lswhere = lswhere & " " & "AND DateOfLetter = " & pDateOfLetter & " "
        End If
        If pDateReceivedByRecipient <> "" Then
            lswhere = lswhere & " " & "AND DateReceivedByRecipient = " & pDateReceivedByRecipient & " "
        End If
        If pLocation <> "" Then
            lswhere = lswhere & " " & "AND Location = " & pLocation & " "
        End If

        Return lswhere
    End Function
    Private Function OrderBy() As String
        If pSortCol = "Name" Then
            Return " g.cName "
        Else
            Return " g.TraceNo "
        End If

    End Function
#End Region


#Region "Validate Methods"
    Public Function CheckIfMonitoringExists() As Boolean


        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            'If DocSession.OraClient Then


            objCommand.pCommandType = CommandType.Text

            objCommand.pCommandText = "SELECT TOP 1 refno FROM CrdMonitoring WHERE RefNo = '" & Replace(pRefNo, "'", "''") & "'"


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
        Dim lsSql = "select convert(char(10),holiday,101) as Holidate,Description from OfficeWorkHoliday where OfficeCode = '" & pOtherOffice & "'" &
        " And (Year between " & pYearFrom & " and " & pYearTo & ") Order By Holidate "

        '" And Month(Holiday) = " & pMonth
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

    Public Function RetrieveMonthHolidays() As DataTable
        Dim lsSql = "select convert(char(10),holiday,101) as Holidate from OfficeWorkHoliday where OfficeCode = '" & pOtherOffice & "'" &
        " And (holiday between '" & pDateFrom & "' and '" & pDateTo & "')"

        '" And Month(Holiday) = " & pMonth
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
"And cm.DueDate > case when personaldelivery = 1 then DeliveryCompleted else MailingCompleted end  " & _
"then 1 else null end) as OnTime " & _
",COUNT(case when mainstatus = 3  " & _
"And cm.DueDate < case when personaldelivery = 1 then DeliveryCompleted else MailingCompleted end  " & _
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
            Throw New Exception(ex.Message)
        Finally
            If Not oConn Is Nothing Then
                oConn.Dispose()
            End If
        End Try
    End Function
End Class
