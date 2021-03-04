Public Class ucRetention
    Inherits System.Web.UI.UserControl

#Region "Properties"


    Dim DocId As Integer
    Dim DocStatusId As String
    Dim smsg As String

    Public Event e_ShowMessage()

    Public Property Message As String
        Get
            Return smsg
        End Get
        Set(ByVal value As String)
            smsg = value
        End Set
    End Property
    Public Property pDocID As Integer
        Get
            Return DocId
        End Get
        Set(ByVal value As Integer)
            DocId = value
        End Set
    End Property

    Public Property pDocStatusId As String
        Get
            Return DocStatusId
        End Get
        Set(ByVal value As String)
            DocStatusId = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Function RetentionType(ByVal asDay As String, ByVal asPeriod As String) As String
        Dim lsret As String = ""
        If asPeriod = "D" Then
            'lsActiveEnd = DateAdd(DateInterval.Day, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
            If CInt(asDay) > 1 Then
                lsret = " Days"
            Else
                lsret = " Day"
            End If

        ElseIf asPeriod = "M" Then
            'lsActiveEnd = DateAdd(DateInterval.Month, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
            If CInt(asDay) > 1 Then
                lsret = " Months"
            Else
                lsret = " Month"
            End If
        ElseIf asPeriod = "Y" Then
            'lsActiveEnd = DateAdd(DateInterval.Year, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
            If CInt(asDay) > 1 Then
                lsret = " Years"
            Else
                lsret = " Year"
            End If
        End If
        Return asDay & " " & lsret
    End Function
    Public Sub RetrieveRetention()
        Dim oData As DataTable
        Try
            Dim lsActiveEnd As String = ""
            Dim lsStorageStart As String = ""
            Dim lsPeriod As String = ""

            Dim oDL As New DocList
            oDL.pDocId = pDocID

            oData = oDL.RetrieveRetentionInfo
            If oData.Rows.Count > 0 Then
                If oData(0)("EnableRetention") = "True" Then
                    lRetentionSetup.Text = "Retention Setup For " & oData(0)("DocName")
                    ap.Text = RetentionType(oData(0)("RetentionDays"), oData(0)("RetentionPeriod"))
                    sp.Text = RetentionType(oData(0)("PurgeDays"), oData(0)("PurgePeriod"))

                    If oData(0)("ActiveStart").ToString.Trim <> "" Then
                        lRetentionStatus.Text = "Retention Period started from the time the Document was set to Archived and it was received by agency."
                        lad.Text = oData(0)("StatDesc")
                        ldrba.Text = oData(0)("DateIssued")
                        'active period
                        lAP.Text = "From " & oData(0)("ActiveStart") & " To "
                        If oData(0)("RetentionPeriod") = "D" Then
                            lsActiveEnd = DateAdd(DateInterval.Day, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
                            
                        ElseIf oData(0)("RetentionPeriod") = "M" Then
                            lsActiveEnd = DateAdd(DateInterval.Month, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
                            
                        ElseIf oData(0)("RetentionPeriod") = "Y" Then
                            lsActiveEnd = DateAdd(DateInterval.Year, oData(0)("RetentionDays"), CDate(oData(0)("ActiveStart"))).ToShortDateString()
                            
                        End If
                        'ap.Text = oData(0)("RetentionDays") & " " & lsPeriod
                        lAP.Text = lAP.Text & lsActiveEnd '& " (" & oData(0)("RetentionDays") & " " & lsPeriod & ")"
                        'storage period
                        If oData(0)("StorageStart").ToString.Trim = "" Then
                            lsStorageStart = DateAdd(DateInterval.Day, 1, CDate(lsActiveEnd)).ToShortDateString

                        Else
                            lsStorageStart = oData(0)("StorageStart")

                        End If


                        lSP.Text = "From " & lsStorageStart & " To "
                        lsActiveEnd = ""
                        If oData(0)("PurgePeriod") = "D" Then
                            lsActiveEnd = DateAdd(DateInterval.Day, oData(0)("PurgeDays"), CDate(lsStorageStart)).ToShortDateString()
                            
                        ElseIf oData(0)("PurgePeriod") = "M" Then
                            lsActiveEnd = DateAdd(DateInterval.Month, oData(0)("PurgeDays"), CDate(lsStorageStart)).ToShortDateString()
                            
                        ElseIf oData(0)("PurgePeriod") = "Y" Then

                            lsActiveEnd = DateAdd(DateInterval.Year, oData(0)("PurgeDays"), CDate(lsStorageStart)).ToShortDateString()

                            
                        End If
                        'sp.Text = oData(0)("PurgeDays") & " " & lsPeriod
                        lSP.Text = lSP.Text & lsActiveEnd '& " (" & oData(0)("PurgeDays") & " " & lsPeriod & ")"
                    Else
                        lRetentionStatus.Text = "Retention Disposable Schedule was not yet started for this document."
                        lAP.Text = "Not Applicable"
                        lSP.Text = "Not Applicable"
                    End If

                Else
                    lRetentionStatus.Text = "Retention Disposable Schedule was not enabled for this document."
                    lAP.Text = "Not Applicable"
                    lSP.Text = "Not Applicable"
                End If
            End If

        Catch ex As Exception
        Finally
            If Not oData Is Nothing Then
                oData.Dispose()
            End If
        End Try
    End Sub

    Public Sub RemoveRetention()
        Dim oIdx As DocIndex
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            If ldrba.Text = "Not Applicable" AndAlso lad.Text = "Not Applicable" Then

                Message = "No settings to be removed."
                RaiseEvent e_ShowMessage()
            Else
                oIdx = New DocIndex
                oIdx.pDocId = DocSession.sDocID
                If IsDate(ldrba.Text) Then
                    oIdx.pRetentionStartDate = ldrba.Text
                Else
                    oIdx.pRetentionStartDate = DateTime.Now.ToShortDateString
                End If

                oIdx.pSetRetention = ""
                ltr = New DbTran
                objCommand = New clsSqlConn(ltr.pTran)
                oIdx.UpdateRetention(objCommand)
                Dim ohist As New DocHistory

                ohist.pAction = "Remove retention period. " + ldrba.Text
                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Retention"
                ohist.pUserId = DocSession.sUserId

                ohist.AddHistory(objCommand)  'george 11/3
                ltr.pTran.Commit()

                Message = "Retention period has been removed."
                RaiseEvent e_ShowMessage()
                pDocID = DocSession.sDocID
                RetrieveRetention()
            End If
        Catch ex As Exception
            ltr.pTran.Rollback()
            Message = ex.Message
            RaiseEvent e_ShowMessage()
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
            End If
        End Try
    End Sub

    Public Sub ResetRetention()
        Dim oIdx As DocIndex
        Dim objCommand As clsSqlConn
        Dim ltr As DbTran
        Try
            If ldrba.Text = "Not Applicable" AndAlso lad.Text = "Not Applicable" Then

                Message = "Cannot reset retention since document status is not yet archived and document has not been received by agency yet."
                RaiseEvent e_ShowMessage()
            Else


                oIdx = New DocIndex
                oIdx.pDocId = DocSession.sDocID
                If IsDate(ldrba.Text) Then
                    oIdx.pRetentionStartDate = ldrba.Text
                Else
                    oIdx.pRetentionStartDate = DateTime.Now.ToShortDateString
                End If

                oIdx.pSetRetention = "Y"
                ltr = New DbTran
                objCommand = New clsSqlConn(ltr.pTran)
                oIdx.UpdateRetention(objCommand)
                Dim ohist As New DocHistory

                ohist.pAction = "Reset retention period to " & ldrba.Text
                ohist.pDocId = DocSession.sDocID
                ohist.pIpAddress = Request.UserHostAddress
                ohist.pTask = "Retention"
                ohist.pUserId = DocSession.sUserId

                ohist.AddHistory(objCommand)  'george 11/3
                ltr.pTran.Commit()

                Message = "Retention period has been reset."
                RaiseEvent e_ShowMessage()
                pDocID = DocSession.sDocID
                RetrieveRetention()
            End If
        Catch ex As Exception
            ltr.pTran.Rollback()
            Message = ex.Message
            RaiseEvent e_ShowMessage()
        Finally
            If Not objCommand Is Nothing Then
                objCommand.Dispose()
            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
            End If
        End Try
    End Sub
End Class