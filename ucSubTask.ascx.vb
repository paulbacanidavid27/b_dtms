Public Class ucSubTask
    Inherits System.Web.UI.UserControl
    Public Event e_refresh()
#Region "Code for displaying error message in Master page"
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
    Private Sub ShowMessage(ByVal asMsg As String)
        Message = asMsg
        RaiseEvent e_ShowMessage()
    End Sub
#End Region
    Public Event e_Count()
    Public ReadOnly Property pCount As String
        Get
            Return hfCount.value
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CountSubTask()
        End If
    End Sub
    Public Property pDocId As String
        Get
            Return hfDocId.Value.Trim
        End Get
        Set(ByVal value As String)
            hfDocId.Value = value
        End Set
    End Property


    Public Sub RetrieveSubTasks()
        Dim oDocs As New DocList
        Dim ldata As DataTable
        Try
            oDocs.pDocId = DocSession.sDocID

            If DocSession.sParentDocID <> "" Then
                oDocs.pParentDocId = DocSession.sParentDocID
            Else
                oDocs.pParentDocId = DocSession.sDocID
            End If
            ldata = oDocs.RetrieveSubTask
            rptSubTasks.DataSource = ldata
            rptSubTasks.DataBind()
            pSubTask.Update()
            If ldata.Rows.Count > 0 Then
                hfCount.Value = ldata.Rows.Count.ToString
            Else
                hfCount.Value = ""
            End If
            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try
    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub
    Public Sub DeleteSubTask()
        Dim ltr As DbTran
        Dim objCommand As clsSqlConn
        'Dim liCtr As Integer
        'Dim oIndex As New DocIndex

        'liCtr = 0
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)
            Dim oDocs As New DocList
            oDocs.pDocId = pDocId.Split(";")(0)
            'oDocs.pRefNo = pDocId.Split(";")(1)
            oDocs.DeleteDoc(objCommand)

            Dim ohist As New DocHistory
            ohist.pDocId = oDocs.pDocId
            ohist.pIpAddress = Request.UserHostAddress
            ohist.pTask = "Delete"
            ohist.pUserId = DocSession.sUserId
            ohist.pAction = "Deleted Subtask " & pDocId.Split(";")(1) & "."
            ohist.AddHistory(objCommand)

            
            ltr.pTran.Commit()
            ShowMessage("Subtask '" & pDocId.Split(";")(1) & "' has been deleted successfully.")

        Catch ex As Exception

            ltr.pTran.Rollback()
            ShowMessage("There's an error while saving the record. Please try again.")

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

    Private Sub rptSubTasks_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptSubTasks.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgCan = DirectCast(e.Item.FindControl("imgDelete"), ImageButton)

            If DocSession.sUserRole = "A" OrElse _
                (DirectCast(e.Item.FindControl("lcby"), Literal).Text.Trim = DocSession.sUserId AndAlso _
                    DateDiff(DateInterval.Minute, DateTime.Parse(DirectCast(e.Item.FindControl("lcdt"), Literal).Text.Trim), DateTime.Now) <= 60) Then
                imgCan.Visible = True

                imgCan.OnClientClick = "setValue('hfDocId','" & DirectCast(e.Item.FindControl("lDocId"), Literal).Text.Trim & ";" & DirectCast(e.Item.FindControl("lTB"), Literal).Text.Trim & "');showWindow('dDelSubTask')"
            End If
        End If
    End Sub

    Public Sub CountSubTask()
        Dim oNotes As DocList
        Dim lcnt As Integer
        Try
            oNotes = New DocList
            lcnt = oNotes.CountSubTask(DocSession.sParentDocID, DocSession.sDocID)
            If lcnt > 0 Then
                hfCount.Value = lcnt.ToString
            Else
                hfCount.Value = ""
            End If

            RaiseEvent e_Count()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub
End Class