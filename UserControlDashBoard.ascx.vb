Public Class UserControlDashBoard
    Inherits System.Web.UI.UserControl

    Dim iretVal As Integer
    Dim iIdx As Integer
    Dim ADate As String
    Dim TDate As String
    Dim Task As String
    Dim IPAddress As String
    Dim ShowDocLink As Boolean = False
    Dim SortOrder As String
    Dim SortCol As String
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
    Public ReadOnly Property pRetVal As Integer
        Get
            Return iretVal
        End Get
    End Property
    Public Property pIPAddress As String
        Get
            Return IPAddress
        End Get
        Set(ByVal value As String)
            IPAddress = value
        End Set
    End Property
    Public Property pIdx As Integer
        Get
            Return iIdx
        End Get
        Set(ByVal value As Integer)
            iIdx = value
        End Set
    End Property

    Public Property pTask As String
        Get
            Return Task
        End Get
        Set(ByVal value As String)
            Task = value
        End Set
    End Property

    Public Property pADate As String
        Get
            Return ADate
        End Get
        Set(ByVal value As String)
            ADate = value
        End Set
    End Property

    Public Property pTDate As String
        Get
            Return TDate
        End Get
        Set(ByVal value As String)
            TDate = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'RetrieveAction(DocSession.sUserId)
        End If
    End Sub

    Public Sub RetrieveAction(ByVal asuser As String)

        Dim lsLoc As String = System.Configuration.ConfigurationManager.AppSettings("doctypeloc")

        Dim ldata As DataTable


        Try
            ShowDocLink = True
            'objCommand.pCommandType = CommandType.StoredProcedure
            'objCommand.pCommandText = "xMSP_DOCHISTORYGET"
            'objCommand.ParametersAddWithValue("@Idx", CInt(pIdx))
            'objCommand.ParametersAddWithValue("@RowsPerPage", CInt(DocSession.RowsPerPage))
            'If pTask <> "" Then
            'objCommand.ParametersAddWithValue("@Task", pTask)
            '   End If

            'If pADate <> "" Then
            'objCommand.ParametersAddWithValue("@ADate", pADate)
            'End If
            'objCommand.ParametersAddWithValue("@UserId", asuser)
            'objCommand.ParametersReturnValue()

            '   End If



            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue
            Dim odh As New DocHistory
            odh.pTask = pTask
            odh.pUserId = asuser
            odh.pActionDate = pADate
            odh.pActionDateTo = pTDate
            odh.pIpAddress = pIPAddress
            iretVal = odh.GetHistoryCount()
            odh.pIdx = pIdx
            odh.pRowsPerPage = DocSession.RowsPerPage
            odh.pSortCol = pSortCol
            odh.pSortOrder = pSortOrder

            'iretVal = CInt(retval.pParam.Value)
            ldata = odh.GetHistory()
            If ldata.Rows.Count > DocSession.RowsPerPage Then
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            End If
            rptDashboard.DataSource = ldata
            rptDashboard.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If


        End Try

    End Sub
    Public Sub showprofile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim imgB As LinkButton = DirectCast(sender, LinkButton)
        Dim ri As RepeaterItem = DirectCast(imgB.NamingContainer, RepeaterItem)
        Dim page1 As System.Web.UI.Page = DirectCast(Me.Page, System.Web.UI.Page)
        Dim mp1 As Site = DirectCast(page1.Master, Site)
        mp1.showProf(DirectCast(ri.FindControl("lPPID"), Literal).Text)

    End Sub
    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
        DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
        Response.Redirect("view.aspx")
    End Sub

    Public Sub RetrieveDocAction(ByVal aDocId As Integer)

        
        Dim ldata As DataTable


        Try

            
            Dim odh As New DocHistory
            odh.pDocId = aDocId
            If Not pTask Is Nothing Then
                odh.pTask = pTask
            End If
            iretVal = odh.GetHistoryCount()
            odh.pIdx = pIdx
            
            odh.pRowsPerPage = DocSession.RowsPerPage

            ldata = odh.GetHistory()
            If ldata.Rows.Count > DocSession.RowsPerPage Then
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            End If
            rptDashboard.DataSource = ldata
            rptDashboard.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If


        End Try

    End Sub

    Private Sub rptDashboard_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDashboard.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lt As HtmlTableRow = DirectCast(e.Item.FindControl("trow"), HtmlTableRow)
            Dim lTc As Label = DirectCast(e.Item.FindControl("lblTask"), Label)
            Dim lbD As LinkButton = DirectCast(e.Item.FindControl("lbDoc"), LinkButton)
            If ShowDocLink Then
                lbD.Visible = True
            End If

            If lTc.Text.ToLower = "system" Then 'gray
                lTc.Text = "Login"
                lt.Style.Item("background-color") = "#D1D3D4"
                lt.Style.Item("color") = "#222222" '"#939598"
                lTc.Style.Item("color") = "#939598" '"#939598"
            ElseIf lTc.Text.ToLower = "logout" Then 'gray
                lt.Style.Item("background-color") = "#E2E2E2"
                lt.Style.Item("color") = "#222222" '"#939598"
                lTc.Style.Item("color") = "#939598" '"#939598"
            ElseIf lTc.Text.ToLower = "link" Then 'gray
                lt.Style.Item("background-color") = "#B5E2E9"
                lt.Style.Item("color") = "#222222" '"#939598"
                lTc.Style.Item("color") = "#26C2EC" '"#939598"
            ElseIf lTc.Text.ToLower = "tag" Then 'blue
                lt.Style.Item("background-color") = "#AA9ECD"
                lt.Style.Item("color") = "#222222"
                lTc.Style.Item("color") = "#4B3491"
            ElseIf lTc.Text.ToLower = "notes" Then 'blue
                lt.Style.Item("background-color") = "#F6A8CA"
                lt.Style.Item("color") = "#222222"
                lTc.Style.Item("color") = "#EC2656"
            ElseIf lTc.Text.ToLower = "upload" OrElse lTc.Text.ToLower = "download" Then 'yellow
                lt.Style.Item("background-color") = "#F8F5BF"
                lTc.Style.Item("color") = "#A8A800"
                lt.Style.Item("color") = "#222222"
            ElseIf lTc.Text.ToLower = "index" OrElse lTc.Text.ToLower = "edit" Then 'green
                lt.Style.Item("background-color") = "#D2E8C8"
                lTc.Style.Item("color") = "#58954D"
                lt.Style.Item("color") = "#222222"
            ElseIf lTc.Text.ToLower = "checkout" OrElse lTc.Text.ToLower = "checkin" Then 'violet
                lt.Style.Item("background-color") = "#E0C1DD"
                lTc.Style.Item("color") = "#783293"
                lt.Style.Item("color") = "#222222"
            ElseIf lTc.Text.ToLower = "printed" Then 'red
                lt.Style.Item("background-color") = "#F4C6C6"
                lTc.Style.Item("color") = "#A71E22"
                lt.Style.Item("color") = "#222222"
            ElseIf lTc.Text.ToLower = "email" OrElse lTc.Text.ToLower = "routing" Then 'orange
                lt.Style.Item("background-color") = "#FDCCA7"
                lTc.Style.Item("color") = "#EE5C23"
                lt.Style.Item("color") = "#222222"
            ElseIf lTc.Text.ToLower = "delete" Then

            End If

        End If
    End Sub
End Class