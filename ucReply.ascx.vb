Public Class ucReply
    Inherits System.Web.UI.UserControl

#Region "Properties"


    Dim ReplyID As String
    Dim DocId As String
    Dim ReplyDate As String
    Dim ReferenceNo As String
    Dim SubmittedDate As String
    Dim ReceivedDate As String
    Dim ReturnTheDocuments As String
    Dim ReturnComplete As String
    Dim ReturnPartial As String
    Dim ReturnNoPages As String
    Dim Deficiency As String
    Dim WithdrawalRequest As String
    Dim WithdrawalReference As String
    Dim Client As String
    Dim ClientAddress As String
    Dim BureauHead As String
    Dim RequireFile1 As String
    Dim RequireFile2 As String
    Dim RequireFile3 As String
    Dim SubmitLaterDate As String
    Dim RequireAdditional As String
    Dim Deficiency1 As String
    Dim Deficiency2 As String
    Dim Deficiency3 As String

    Private Property pReplyID As String
        Get
            Return ReplyID
        End Get
        Set(ByVal value As String)
            ReplyID = value
        End Set
    End Property
    Public Property pDocId As String
        Get
            Return ldocid.Text
        End Get
        Set(ByVal value As String)
            ldocid.Text = value
        End Set
    End Property
    Private Property pReplyDate As String
        Get
            Return ReplyDate
        End Get
        Set(ByVal value As String)
            ReplyDate = value
        End Set
    End Property
    Public Property pReferenceNo As String
        Get
            Return ReferenceNo
        End Get
        Set(ByVal value As String)
            ReferenceNo = value
        End Set
    End Property
    Private Property pSubmittedDate As String
        Get
            Return SubmittedDate
        End Get
        Set(ByVal value As String)
            SubmittedDate = value
        End Set
    End Property
    Private Property pReceivedDate As String
        Get
            Return ReceivedDate
        End Get
        Set(ByVal value As String)
            ReceivedDate = value
        End Set
    End Property
    Private Property pReturnTheDocuments As String
        Get
            Return ReturnTheDocuments
        End Get
        Set(ByVal value As String)
            ReturnTheDocuments = value
        End Set
    End Property
    Private Property pReturnComplete As String
        Get
            Return ReturnComplete
        End Get
        Set(ByVal value As String)
            ReturnComplete = value
        End Set
    End Property
    Private Property pReturnPartial As String
        Get
            Return ReturnPartial
        End Get
        Set(ByVal value As String)
            ReturnPartial = value
        End Set
    End Property
    Private Property pReturnNoPages As String
        Get
            Return ReturnNoPages
        End Get
        Set(ByVal value As String)
            ReturnNoPages = value
        End Set
    End Property
    Private Property pDeficiency As String
        Get
            Return Deficiency
        End Get
        Set(ByVal value As String)
            Deficiency = value
        End Set
    End Property

    Private Property pWithdrawalRequest As String
        Get
            Return WithdrawalRequest
        End Get
        Set(ByVal value As String)
            WithdrawalRequest = value
        End Set
    End Property

    Private Property pWithdrawalReference As String
        Get
            Return WithdrawalReference
        End Get
        Set(ByVal value As String)
            WithdrawalReference = value
        End Set
    End Property

    Private Property pClient As String
        Get
            Return Client
        End Get
        Set(ByVal value As String)
            Client = value
        End Set
    End Property

    Private Property pClientAddress As String
        Get
            Return ClientAddress
        End Get
        Set(ByVal value As String)
            ClientAddress = value
        End Set
    End Property

    Private Property pBureauHead As String
        Get
            Return BureauHead
        End Get
        Set(ByVal value As String)
            BureauHead = value
        End Set
    End Property

    Private Property pRequireFile1 As String
        Get
            Return RequireFile1
        End Get
        Set(ByVal value As String)
            RequireFile1 = value
        End Set
    End Property

    Private Property pRequireFile2 As String
        Get
            Return RequireFile2
        End Get
        Set(ByVal value As String)
            RequireFile2 = value
        End Set
    End Property

    Private Property pRequireFile3 As String
        Get
            Return RequireFile3
        End Get
        Set(ByVal value As String)
            RequireFile3 = value
        End Set
    End Property

    Private Property pSubmitLaterDate As String
        Get
            Return SubmitLaterDate
        End Get
        Set(ByVal value As String)
            SubmitLaterDate = value
        End Set
    End Property

    Private Property pRequireAdditional As String
        Get
            Return RequireAdditional
        End Get
        Set(ByVal value As String)
            RequireAdditional = value
        End Set
    End Property

    Private Property pDeficiency1 As String
        Get
            Return Deficiency1
        End Get
        Set(ByVal value As String)
            Deficiency1 = value
        End Set
    End Property

    Private Property pDeficiency2 As String
        Get
            Return Deficiency2
        End Get
        Set(ByVal value As String)
            Deficiency2 = value
        End Set
    End Property

    Private Property pDeficiency3 As String
        Get
            Return Deficiency3
        End Get
        Set(ByVal value As String)
            Deficiency3 = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'btPrint.Visible = False
            btPost.Visible = True
            lAddress.Text = IIf(DocSession.sGroupAddress = "", "General Solano St, San Miguel, Manila", DocSession.sGroupAddress)
            'lTitle.Text = IIf(DocSession.ReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", DocSession.ReceiptReplyName.ToUpper())
            lTitle.Text = IIf(DocSession.ReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", Replace(DocSession.ReceiptReplyName, "\n", "<br/>"))

            imgLogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "dbm.png", DocSession.GroupLogo)
            'imgLogo.ImageUrl = "images/logo/" & DocSession.GroupLogo
            imgLogo.DataBind()
        End If
    End Sub



    Private Sub AddReply()
        Dim s_sql As String = "INSERT INTO dbo.DocReply " & _
           "(ReplyID " & _
           ",DocId " & _
           ",ReplyDate " & _
           ",ReferenceNo " & _
           ",SubmittedDate " & _
           ",ReceivedDate " & _
           ",RequireAdditional " & _
           ",AdditionalDoc1 " & _
           ",AdditionalDoc2 " & _
           ",AdditionalDoc3 " & _
           ",AdditionalDoc4 " & _
           ",AdditionalDoc5 " & _
           ",ReturnTheDocuments " & _
           ",ReturnComplete " & _
           ",ReturnPartial " & _
           ",ReturnNoPages " & _
           ",Deficiency " & _
           ",SpecificDoc1 " & _
           ",SpecificDoc2 " & _
           ",SpecificDoc3 " & _
           ",SubmitDate " & _
           ",WithdrawalRequest " & _
           ",WithdrawalReference " & _
           ",Client " & _
           ",ClientAddress " & _
           ",PrintedDate " & _
           ",PrintedBy " & _
           ",BureauHead " & _
           ",ThisPertains " & _
    ",WhichWas " & _
    ",InOrder " & _
    ",PleaseIndicate " & _
    ",AdditionalDoc " & _
    ",SpecificDoc " & _
    ",WithdrawalComment " & _
    ",BasedOn " & _
           ") " & _
        "VALUES " & _
           "('" & DocSession.getNextID("replyid") & "' " & _
           ",'" & ldocid.Text & "' " & _
           ",'" & tbReplyDate.Text.Replace("'", "''") & "' " & _
           ",'" & tbRefNo.Text.Replace("'", "''") & "' " & _
           ",'" & tbSubmittedDate.Text.Replace("'", "''") & "' " & _
           ",'" & tbReceivedDate.Text.Replace("'", "''") & "' " & _
           "," & IIf(imgcbxAdditional.BoxCheck, "1", "0") & " " & _
           ",'" & tbAdditionalDoc1.Text.Replace("'", "''") & "' " & _
           ",'" & tbAdditionalDoc2.Text.Replace("'", "''") & "' " & _
           ",'" & tbAdditionalDoc3.Text.Replace("'", "''") & "' " & _
           ",'" & tbAdditionalDoc4.Text.Replace("'", "''") & "' " & _
           ",'" & tbAdditionalDoc5.Text.Replace("'", "''") & "' " & _
           "," & IIf(imgcbxReturn.BoxCheck, "1", "0") & " " & _
           ",'" & IIf(imgcbxComplete.BoxCheck, "1", "0") & "' " & _
           ",'" & IIf(imgcbxPartial.BoxCheck, "1", "0") & "' " & _
           ",'" & IIf(tbNoPages.Text.Trim = "", "0", tbNoPages.Text.Trim.Replace("'", "''")) & "' " & _
           "," & IIf(imgcbxDeficiency.BoxCheck, "1", "0") & " " & _
           ",'" & tbSpecificDoc1.Text.Replace("'", "''") & "' " & _
           ",'" & tbSpecificDoc2.Text.Replace("'", "''") & "' " & _
           ",'" & tbSpecificDoc3.Text.Replace("'", "''") & "' " & _
           ",'" & tbSubmitDate.Text.Replace("'", "''") & "' " & _
           "," & IIf(imgcbxWithdrawal.BoxCheck, "1", "0") & " " & _
           ",'" & tbWReferenceNo.Text.Replace("'", "''") & "' " & _
           ",'" & tbClientAgency.Text.Replace("'", "''") & "' " & _
           ",'" & tbAddress.Text.Replace("'", "''") & "' " & _
           ",'" & DateTime.Now.ToString & "' " & _
           ",'" & DocSession.sUserId & "' " & _
           ",'" & tbBureauHead.Text.Replace("'", "''") & "'" & _
            ",'" & tbThisPertains.Text.Replace("'", "''") & "' " & _
            ",'" & tbWhichWas.Text.Replace("'", "''") & "' " & _
            ",'" & tbInOrder.Text.Replace("'", "''") & "' " & _
            ",'" & tbPleaseIndicate.Text.Replace("'", "''") & "' " & _
            ",'" & EditorAddDoc.Content.Replace("'", "''") & "'" & _
            ",'" & EditorSpecific.Content.Replace("'", "''") & "'" & _
            ",'" & EditorWithdrawal.Content.Replace("'", "''") & "'" & _
            ",'" & tbBasedOn.Text.Replace("'", "''") & "')"
        'Dim ldata As DataTable
        Dim objcmd As clsSqlConn
        Dim ltr As DbTran
        Try

            ltr = New DbTran
            objcmd = New clsSqlConn(ltr.pTran)
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = s_sql
            objcmd.ExecTranNonQuery()

            Dim oDoc As New DocHistory
            oDoc.pDocId = DocSession.sDocID
            oDoc.pTask = "Reply"
            oDoc.pAction = "Printed Reply Document"
            oDoc.pUserId = DocSession.sUserId
            oDoc.pIpAddress = Request.UserHostAddress
            oDoc.AddHistory(objcmd)
            ltr.pTran.Commit()
            ShowDocument()
        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally

            If Not objcmd Is Nothing Then
                objcmd.Dispose()
            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
            End If
        End Try


    End Sub

    Private Sub UpdateReply()
        Dim s_sql As String = "UPDATE DocReply " & _
           "(ReplyID " & _
           ",DocId " & _
           ",ReplyDate='" & tbReplyDate.Text.Replace("'", "''") & "' " & _
           ",ReferenceNo='" & tbReplyDate.Text.Replace("'", "''") & "' " & _
           ",SubmittedDate='" & tbSubmittedDate.Text.Replace("'", "''") & "' " & _
           ",ReceivedDate='" & tbReceivedDate.Text.Replace("'", "''") & "' " & _
           ",RequireAdditional=" & IIf(imgcbxAdditional.BoxCheck, "1", "0") & " " & _
           ",AdditionalDoc1='" & tbAdditionalDoc1.Text.Replace("'", "''") & "' " & _
           ",AdditionalDoc2='" & tbAdditionalDoc2.Text.Replace("'", "''") & "' " & _
           ",AdditionalDoc3='" & tbAdditionalDoc3.Text.Replace("'", "''") & "' " & _
           ",AdditionalDoc4='" & tbAdditionalDoc4.Text.Replace("'", "''") & "' " & _
           ",AdditionalDoc5='" & tbAdditionalDoc5.Text.Replace("'", "''") & "' " & _
           ",ReturnTheDocuments=" & IIf(imgcbxReturn.BoxCheck, "1", "0") & " " & _
           ",ReturnComplete='" & IIf(imgcbxComplete.BoxCheck, "1", "0") & "' " & _
           ",ReturnPartial='" & IIf(imgcbxPartial.BoxCheck, "1", "0") & "' " & _
           ",ReturnNoPages='" & IIf(tbNoPages.Text.Trim = "", "0", tbNoPages.Text.Trim.Replace("'", "''")) & "' " & _
           ",Deficiency=" & IIf(imgcbxDeficiency.BoxCheck, "1", "0") & " " & _
           ",SpecificDoc1='" & tbSpecificDoc1.Text.Replace("'", "''") & "' " & _
           ",SpecificDoc2='" & tbSpecificDoc2.Text.Replace("'", "''") & "' " & _
           ",SpecificDoc3='" & tbSpecificDoc3.Text.Replace("'", "''") & "' " & _
           ",SubmitDate='" & tbSubmitDate.Text & "' " & _
           ",WithdrawalRequest=" & IIf(imgcbxWithdrawal.BoxCheck, "1", "0") & " " & _
           ",WithdrawalReference='" & tbWReferenceNo.Text & "' " & _
           ",Client='" & tbClientAgency.Text.Replace("'", "''") & "' " & _
           ",ClientAddress='" & tbAddress.Text.Replace("'", "''") & "' " & _
            ",BureauHead='" & tbBureauHead.Text.Replace("'", "''") & "'" & _
            ",AdditionalDoc='" & EditorAddDoc.Content.Replace("'", "''") & "'" & _
            ",SpecificDoc='" & EditorSpecific.Content.Replace("'", "''") & "'" & _
            ",WithdrawalComment='" & EditorWithdrawal.Content.Replace("'", "''") & "'" & _
           ",ThisPertains='" & tbThisPertains.Text.Replace("'", "''") & "' " & _
    ",WhichWas='" & tbWhichWas.Text.Replace("'", "''") & "' " & _
    ",InOrder='" & tbInOrder.Text.Replace("'", "''") & "' " & _
    ",PleaseIndicate='" & tbPleaseIndicate.Text.Replace("'", "''") & "' " & _
    ",BasedOn='" & tbBasedOn.Text.Replace("'", "''") & "' " & _
           "WHERE replyid = " & lreplyid.Text


        'Dim ldata As DataTable
        Dim objcmd As clsSqlConn
        Dim ltr As DbTran
        Try

            ltr = New DbTran
            objcmd = New clsSqlConn(ltr.pTran)
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = s_sql
            objcmd.ExecTranNonQuery()

            Dim oDoc As New DocHistory
            oDoc.pDocId = DocSession.sDocID
            oDoc.pTask = "Reply"
            oDoc.pAction = "Printed Reply Document"
            oDoc.pUserId = DocSession.sUserId
            oDoc.pIpAddress = Request.UserHostAddress
            oDoc.AddHistory(objcmd)
            ltr.pTran.Commit()
            ShowDocument()
        Catch ex As Exception
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
        Finally

            If Not objcmd Is Nothing Then
                objcmd.Dispose()
            End If
            If Not ltr Is Nothing Then
                ltr.Dispose()
            End If
        End Try


    End Sub

    Private Sub btPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPrint.Click
        Try

            AddReply()
            'btPrint.Visible = False
            'btPost.Visible = True

            'pSaveInfo.Update()
            'ShowDocument()
        Catch ex As Exception
        Finally

        End Try

    End Sub
    Private Function ValidData() As Boolean
        If tbReplyDate.Text.Trim <> "" AndAlso Not IsDate(tbReplyDate.Text) Then
            lmsg.Text = "Invalid Reply Date."
            Return False
        End If
        If tbSubmittedDate.Text.Trim <> "" AndAlso Not IsDate(tbSubmittedDate.Text) Then
            lmsg.Text = "Invalid Submitted Date."
            Return False
        End If
        If tbReceivedDate.Text.Trim <> "" AndAlso Not IsDate(tbReceivedDate.Text) Then
            lmsg.Text = "Invalid Received Date."
            Return False
        End If
        If tbSubmitDate.Text.Trim <> "" AndAlso Not IsDate(tbSubmitDate.Text) Then
            lmsg.Text = "Invalid Submission Date."
            Return False
        End If
        If tbNoPages.Text AndAlso Not IsNumeric(tbNoPages.Text) Then
            lmsg.Text = "Invalid No of Pages."
            Return False
        End If
    End Function
    Private Function RetrieveReply(ByVal asID As String, ByVal asAct As String) As DataTable

        Dim s_sql As String
        s_sql = "SELECT "

        If Not DocSession.OraClient Then
            s_sql = s_sql & "Top 2 "
        End If

        s_sql = s_sql & " dr.ReplyID " & _
               ",'' as Refno " & _
               ",'' as CreatedDate " & _
               ",dr.DocId " & _
               ",dr.ReplyDate " & _
               ",dr.ReferenceNo " & _
               ",dr.SubmittedDate " & _
               ",dr.ReceivedDate " & _
               ",dr.ReturnTheDocuments " & _
               ",dr.AdditionalDoc1 " & _
               ",dr.AdditionalDoc2 " & _
               ",dr.AdditionalDoc3 " & _
               ",dr.AdditionalDoc4 " & _
               ",dr.AdditionalDoc5 " & _
               ",dr.ReturnComplete " & _
               ",dr.ReturnPartial " & _
               ",dr.ReturnNoPages " & _
               ",dr.Deficiency " & _
                ",dr.SpecificDoc1 " & _
               ",dr.SpecificDoc2 " & _
               ",dr.SpecificDoc3 " & _
               ",dr.WithdrawalRequest " & _
               ",dr.WithdrawalReference " & _
               ",dr.Client " & _
               ",dr.ClientAddress " & _
               ",dr.BureauHead " & _
               ",dr.SubmitDate " & _
               ",dr.RequireAdditional " & _
               ",dr.PrintedBy " & _
               ",dr.PrintedDate " & _
               ",dr.ThisPertains " & _
    ",dr.WhichWas " & _
    ",dr.InOrder " & _
    ",dr.PleaseIndicate " & _
    ",dr.AdditionalDoc " & _
    ",dr.SpecificDoc " & _
    ",dr.WithdrawalComment " & _
    ",dr.BasedOn " & _
               " FROM DocReply dr " & _
               " WHERE docId = " & DocSession.sDocID & " and replyid " & asAct & "= " & asID & " "

        If DocSession.OraClient Then
            s_sql = s_sql & " and rownum < 3 "
        End If

        s_sql = s_sql & "  ORDER BY replyId " & IIf(asAct = "<", "Desc", "Asc")

        Dim ldata As DataTable
        Dim objcmd As clsSqlConn
        Try
            objcmd = New clsSqlConn
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = s_sql
            ldata = objcmd.ExecData
            If ldata.Rows.Count > 0 Then
                DisplayValues(ldata, asAct, asID)
            End If
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not objcmd Is Nothing Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
        End Try

    End Function

    Private Function RetrieveSql(ByVal asRecordType As String) As String
        Dim s_sql As String
        s_sql = "SELECT " & _
   " dr.ReplyID " & _
   ",dl.DocId " & _
   ",isnull(dr.ReplyDate,getdate()) as ReplyDate " & _
   ",dl.RefNo " & _
   ",dr.ReferenceNo " & _
   ",isnull(dr.SubmittedDate,dl.createddate) as SubmittedDate " & _
   ",dl.createddate " & _
   ",isnull(dr.ReceivedDate,dl.ReceivedDate) as ReceivedDate " & _
   ",dr.ReturnTheDocuments " & _
   ",dr.AdditionalDoc1 " & _
   ",dr.AdditionalDoc2 " & _
   ",dr.AdditionalDoc3 " & _
   ",dr.AdditionalDoc4 " & _
   ",dr.AdditionalDoc5 " & _
   ",dr.AdditionalDoc " & _
   ",dr.ReturnComplete " & _
   ",dr.ReturnPartial " & _
   ",dr.ReturnNoPages " & _
   ",dr.Deficiency " & _
    ",dr.SpecificDoc1 " & _
   ",dr.SpecificDoc2 " & _
   ",dr.SpecificDoc3 " & _
   ",dr.SpecificDoc " & _
   ",dr.WithdrawalComment " & _
   ",dr.WithdrawalRequest " & _
   ",dr.WithdrawalReference " & _
   ",dr.Client " & _
   ",dr.ClientAddress " & _
   ",dr.BureauHead " & _
   ",dr.SubmitDate " & _
   ",dr.PrintedBy " & _
   ",dr.PrintedDate " & _
   ",dr.RequireAdditional " & _
        ",case when dr.ThisPertains is null Then 'This pertains to the request submitted to this Office dated' else dr.thisPertains end as ThisPertains " & _
    ",case when dr.WhichWas  is null then 'which was received on' else dr.WhichWas end as WhichWas " & _
    ",case when dr.InOrder  is null then 'In order for us to proceed with the processing of the request, may we request for submission of the following additional documents (attach additional list if necessary):' else dr.InOrder end as InOrder " & _
    ",case when dr.PleaseIndicate  is null then 'Please indicate the DMS Reference Number in the cover page/transmittal sheet and submit the same to our <<office>> not later than' else dr.PleaseIndicate end as PleaseIndicate " & _
    ",case when dr.BasedOn  is null then 'Based on our review, it is necessary to return the documents you have submitted due to the following reason:' else dr.BasedOn end as BasedOn "
        If asRecordType = "C" Then 'current record
            s_sql = s_sql & " FROM ("

            s_sql = s_sql & "SELECT "

            If Not DocSession.OraClient Then
                s_sql = s_sql & " Top 2 "
            End If

            s_sql = s_sql & " * FROM DocReply " & _
                            " WHERE docid = " & pDocId

            If DocSession.OraClient Then
                s_sql = s_sql & " and rownum < 3 "
            End If

            s_sql = s_sql & " Order By replyId desc) dr INNER JOIN doclist dl "
            s_sql = s_sql & "  ON dl.docid = dr.docid "
        Else
            s_sql = s_sql & " FROM DocList dl Left Join " & _
                            " (SELECT " & pDocId & " as docid2,ReplyID,DocId,ReplyDate,ReferenceNo,SubmittedDate,ReceivedDate,ReturnTheDocuments " & _
      ",RequireAdditional,AdditionalDoc1,AdditionalDoc2,AdditionalDoc3,AdditionalDoc4,AdditionalDoc5,AdditionalDoc,ReturnComplete,ReturnPartial,ReturnNoPages,Deficiency,SubmitDate,SpecificDoc,WithdrawalComment,SpecificDoc1,SpecificDoc2,SpecificDoc3 " & _
      ",WithdrawalRequest,WithdrawalReference,Client,ClientAddress,BureauHead,PrintedDate,PrintedBy,ThisPertains,WhichWas,InOrder,PleaseIndicate,BasedOn " & _
      " FROM DocReply WHERE replyid in (SELECT max(replyid) FROM docreply WHERE docid = " & pDocId & " )) dr "
            s_sql = s_sql & "  ON dl.docid = dr.docid2 WHERE dl.docid = " & pDocId
        End If



        Return s_sql

    End Function
    Public Sub ShowDocument()
        Dim ldata As DataTable
        Dim s_sql As String
        Dim objcmd As clsSqlConn
        Try
            s_sql = RetrieveSql("C")
            hprevid.Value = ""
            hcurrid.Value = ""
            hprevid.Value = ""
            objcmd = New clsSqlConn
            objcmd.CommandType = CommandType.Text
            objcmd.CommandText = s_sql
            ldata = objcmd.ExecData()
            If ldata.Rows.Count = 0 Then
                s_sql = RetrieveSql("M")
                objcmd = New clsSqlConn
                objcmd.CommandType = CommandType.Text
                objcmd.CommandText = s_sql
                ldata = objcmd.ExecData()
            End If

            DisplayValues(ldata, "<", "")

            btPost.Visible = True
            btPrint.Visible = False
            pContentAddDoc.Visible = False
            EditorAddDoc.Visible = True
            pContentSpecific.Visible = False
            pContentWithdrawal.Visible = False
            EditorSpecific.Visible = True
            EditorWithdrawal.Visible = True
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not objcmd Is Nothing Then
                objcmd.Dispose()
                objcmd = Nothing
            End If
        End Try
    End Sub
    Private Sub DisplayValues(ByVal lData As DataTable, ByVal asAct As String, ByVal asId As String)
        Try


            If lData.Rows.Count > 0 Then
                Dim lsval
                tbRefNo.Text = IIf(IsDBNull(lData(0)("ReferenceNo")), lData(0)("RefNo"), lData(0)("ReferenceNo"))

                If (IsDBNull(lData(0)("ReplyDate"))) Then
                    tbReplyDate.Text = ""
                Else
                    tbReplyDate.Text = CDate(lData(0)("ReplyDate")).ToString("MM/dd/yyyy")
                    If tbReplyDate.Text = "01/01/1900" Then
                        tbReplyDate.Text = ""
                    End If
                End If

                tbClientAgency.Text = IIf(IsDBNull(lData(0)("Client")), "", lData(0)("Client"))
                tbAddress.Text = IIf(IsDBNull(lData(0)("ClientAddress")), "", lData(0)("ClientAddress"))

                If (IsDBNull(lData(0)("SubmittedDate"))) Then
                    tbSubmittedDate.Text = ""
                Else
                    tbSubmittedDate.Text = CDate(lData(0)("SubmittedDate")).ToString("MM/dd/yyyy")
                    If tbSubmittedDate.Text = "01/01/1900" Then
                        tbSubmittedDate.Text = ""
                    End If
                End If

                If (IsDBNull(lData(0)("ReceivedDate"))) Then
                    tbReceivedDate.Text = ""
                Else
                    tbReceivedDate.Text = CDate(lData(0)("ReceivedDate")).ToString("MM/dd/yyyy")
                    If tbReceivedDate.Text = "01/01/1900" Then
                        tbReceivedDate.Text = ""
                    End If
                End If

                lsval = IIf(IsDBNull(lData(0)("RequireAdditional")), "False", lData(0)("RequireAdditional"))
                If lsval = "True" Then
                    imgcbxAdditional.BoxCheck = True
                Else
                    imgcbxAdditional.BoxCheck = False
                End If
                tbAdditionalDoc1.Text = IIf(IsDBNull(lData(0)("AdditionalDoc1")), "", lData(0)("AdditionalDoc1"))
                tbAdditionalDoc2.Text = IIf(IsDBNull(lData(0)("AdditionalDoc2")), "", lData(0)("AdditionalDoc2"))
                tbAdditionalDoc3.Text = IIf(IsDBNull(lData(0)("AdditionalDoc3")), "", lData(0)("AdditionalDoc3"))
                tbAdditionalDoc4.Text = IIf(IsDBNull(lData(0)("AdditionalDoc4")), "", lData(0)("AdditionalDoc4"))
                tbAdditionalDoc5.Text = IIf(IsDBNull(lData(0)("AdditionalDoc5")), "", lData(0)("AdditionalDoc5"))
                EditorAddDoc.Content = IIf(IsDBNull(lData(0)("AdditionalDoc")), "", lData(0)("AdditionalDoc"))
                EditorSpecific.Content = IIf(IsDBNull(lData(0)("SpecificDoc")), "", lData(0)("SpecificDoc"))
                EditorWithdrawal.Content = IIf(IsDBNull(lData(0)("WithdrawalComment")), "", lData(0)("WithdrawalComment"))

                tbThisPertains.Text = IIf(IsDBNull(lData(0)("ThisPertains")), "", lData(0)("ThisPertains"))
                tbWhichWas.Text = IIf(IsDBNull(lData(0)("WhichWas")), "", lData(0)("WhichWas"))
                tbInOrder.Text = IIf(IsDBNull(lData(0)("InOrder")), "", lData(0)("InOrder"))
                tbPleaseIndicate.Text = IIf(IsDBNull(lData(0)("PleaseIndicate")), "", Replace(lData(0)("PleaseIndicate"), "<<office>>", IIf(DocSession.sOfcName = "", "Administrative Service, Central Records Division", DocSession.sOfcName)))
                tbBasedOn.Text = IIf(IsDBNull(lData(0)("BasedOn")), "", lData(0)("BasedOn"))
                lThisPertains.Text = IIf(IsDBNull(lData(0)("ThisPertains")), "", lData(0)("ThisPertains"))
                lWhichWas.Text = IIf(IsDBNull(lData(0)("WhichWas")), "", lData(0)("WhichWas"))
                lInOrder.Text = IIf(IsDBNull(lData(0)("InOrder")), "", lData(0)("InOrder"))
                lPleaseIndicate.Text = IIf(IsDBNull(lData(0)("PleaseIndicate")), "", Replace(lData(0)("PleaseIndicate"), "<<office>>", IIf(DocSession.sOfcName = "", "Administrative Service, Central Records Division", DocSession.sOfcName)))
                lBasedOn.Text = IIf(IsDBNull(lData(0)("BasedOn")), "", lData(0)("BasedOn"))


                If (IsDBNull(lData(0)("SubmitDate"))) Then
                    tbSubmitDate.Text = ""
                Else
                    tbSubmitDate.Text = CDate(lData(0)("SubmitDate")).ToString("MM/dd/yyyy")
                    If tbSubmitDate.Text = "01/01/1900" Then
                        tbSubmitDate.Text = ""
                    End If
                End If
                If (IsDBNull(lData(0)("PrintedDate"))) Then
                    lPdate.Text = ""
                Else
                    lPdate.Text = CDate(lData(0)("PrintedDate")).ToString("MM/dd/yyyy")
                End If

                lsval = IIf(IsDBNull(lData(0)("ReturnTheDocuments")), "False", lData(0)("ReturnTheDocuments"))
                If lsval = "True" Then
                    imgcbxReturn.BoxCheck = True
                Else
                    imgcbxReturn.BoxCheck = False
                End If
                lsval = IIf(IsDBNull(lData(0)("ReturnComplete")), "False", lData(0)("ReturnComplete"))
                If lsval = "True" Then
                    imgcbxComplete.BoxCheck = True
                Else
                    imgcbxComplete.BoxCheck = False
                End If
                lsval = IIf(IsDBNull(lData(0)("ReturnPartial")), "False", lData(0)("ReturnPartial"))
                If lsval = "True" Then
                    imgcbxPartial.BoxCheck = True
                Else
                    imgcbxPartial.BoxCheck = False
                End If

                If IsDBNull(lData(0)("ReturnNoPages")) Then
                    tbNoPages.Text = ""
                Else

                    If CInt(lData(0)("ReturnNoPages")) > 0 Then
                        tbNoPages.Text = lData(0)("ReturnNoPages")
                    Else
                        tbNoPages.Text = ""
                    End If


                End If

                lsval = IIf(IsDBNull(lData(0)("Deficiency")), "False", lData(0)("Deficiency"))
                If lsval = "True" Then
                    imgcbxDeficiency.BoxCheck = True
                Else
                    imgcbxDeficiency.BoxCheck = False
                End If
                tbSpecificDoc1.Text = IIf(IsDBNull(lData(0)("SpecificDoc1")), "", lData(0)("SpecificDoc1"))
                tbSpecificDoc2.Text = IIf(IsDBNull(lData(0)("SpecificDoc2")), "", lData(0)("SpecificDoc2"))
                tbSpecificDoc3.Text = IIf(IsDBNull(lData(0)("SpecificDoc3")), "", lData(0)("SpecificDoc3"))
                lsval = IIf(IsDBNull(lData(0)("WithdrawalRequest")), "False", lData(0)("WithdrawalRequest"))

                If lsval = "True" Then
                    imgcbxWithdrawal.BoxCheck = True
                Else
                    imgcbxWithdrawal.BoxCheck = False
                End If

                tbWReferenceNo.Text = IIf(IsDBNull(lData(0)("WithdrawalReference")), "", lData(0)("WithdrawalReference"))
                tbBureauHead.Text = IIf(IsDBNull(lData(0)("BureauHead")), "", lData(0)("BureauHead"))
                lPby.Text = IIf(IsDBNull(lData(0)("PrintedBy")), "", lData(0)("PrintedBy"))
                If lData.Rows.Count > 1 Then
                    If asAct = "<" Then
                        'If hnextid.Value = "" Then
                        If hcurrid.Value = "" Then
                            hcurrid.Value = lData(0)("replyid") 'hnextid.Value
                            hnextid.Value = lData(1)("replyid")
                        Else
                            hprevid.Value = hcurrid.Value 'hnextid.Value
                            hcurrid.Value = lData(0)("replyid")
                            hnextid.Value = lData(1)("replyid")
                        End If
                    Else
                        If hcurrid.Value = "" Then
                            hcurrid.Value = lData(0)("replyid") 'hnextid.Value
                            hprevid.Value = lData(1)("replyid")
                        Else
                            hnextid.Value = hcurrid.Value 'hnextid.Value
                            hcurrid.Value = lData(0)("replyid")
                            hprevid.Value = lData(1)("replyid")
                        End If
                    End If
                ElseIf lData.Rows.Count = 1 Then
                    If asAct = "<" Then
                        If hcurrid.Value <> "" Then
                            hprevid.Value = hcurrid.Value 'hnextid.Value
                            hnextid.Value = ""
                        Else
                            If hprevid.Value <> "" Then
                                hprevid.Value = hcurrid.Value
                                hnextid.Value = ""
                            End If
                        End If
                    Else
                        If hcurrid.Value <> "" Then
                            hnextid.Value = hcurrid.Value 'hprevid.Value
                            hprevid.Value = ""
                        Else
                            If hnextid.Value <> "" Then
                                hnextid.Value = hcurrid.Value
                                hprevid.Value = ""
                            End If
                        End If
                    End If
                    hcurrid.Value = lData(0)("replyid")
                End If
            End If
            If hnextid.Value <> "" Then
                imgRight.Visible = True
            Else
                imgRight.Visible = False
            End If
            If hprevid.Value <> "" Then
                imgLeft.Visible = True
            Else
                imgLeft.Visible = False
            End If
            imgRight.ToolTip = hnextid.Value
            imgLeft.ToolTip = hprevid.Value
            pSaveInfo.Update()
        Catch ex As Exception
        Finally
            If Not lData Is Nothing Then
                lData.Dispose()
                lData = Nothing
            End If
        End Try
    End Sub
    Private Sub imgCloseAck_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseAck.Click
        Me.Visible = False
        pSaveInfo.Update()
    End Sub

    Protected Sub btSaveReply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSaveReply.Click
        'If lreplyid.Text <> "" Then
        '    UpdateReply()
        'Else
        AddReply()
        'End If

        HideShowFields(False)
    End Sub

    Private Sub imgLeft_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLeft.Click
        RetrieveReply(hprevid.Value, ">")
    End Sub

    Private Sub imgRight_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRight.Click
        RetrieveReply(hnextid.Value, "<")
    End Sub

    Private Sub btEditReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEditReply.Click
        HideShowFields(True)
    End Sub
    Private Sub HideShowFields(ByVal asbool As Boolean)
        btSaveReply.Visible = asbool
        btEditReply.Visible = Not asbool
        btPost.Visible = Not asbool
        'btPrint.Visible = Not asbool
        tbPleaseIndicate.Visible = asbool
        tbWhichWas.Visible = asbool
        tbThisPertains.Visible = asbool
        tbBasedOn.Visible = asbool
        tbInOrder.Visible = asbool

        lPleaseIndicate.Visible = Not asbool
        lWhichWas.Visible = Not asbool
        lThisPertains.Visible = Not asbool
        lBasedOn.Visible = Not asbool
        lInOrder.Visible = Not asbool
    End Sub


    Private Sub btPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPost.Click
        btPrint.Visible = True
        btPost.Visible = False

        EditorAddDoc.Visible = False
        EditorSpecific.Visible = False
        EditorWithdrawal.Visible = False
        pContentAddDoc.Visible = True
        pContentAddDoc.Text = EditorAddDoc.Content
        pContentSpecific.Visible = True
        pContentWithdrawal.Visible = True
        pContentSpecific.Text = EditorSpecific.Content
        pContentWithdrawal.Text = EditorWithdrawal.Content
        pSaveInfo.Update()

    End Sub
End Class