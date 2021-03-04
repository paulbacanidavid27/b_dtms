Public Class ucDashboardGraph
    Inherits System.Web.UI.UserControl

    Dim iretVal As Integer
    Dim iIdx As Integer
    Dim ADate As String
    Dim Task As String
    Dim ShowDocLink As Boolean = False
    Public ReadOnly Property pRetVal As Integer
        Get
            Return iretVal
        End Get
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'RetrieveAction(DocSession.sUserId)
            LoadChart()
        End If
    End Sub

    Public Sub RetrieveAction()


        Try
            Dim od As New DocList

            ltdr.Text = od.DocAllCount() ' total document received
            lnadr.Text = od.DocStatusCount("18", " = ")
            ladr.Text = od.DocStatusCount("18", " <> ")
            lpd.Text = od.DocPendingCount("P")
            lcd.Text = od.DocCompleteCount

            pDB.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Using ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
            DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
            Response.Redirect("view.aspx")
        End Using
    End Sub

    Private Sub LoadChart()
        Try

        
            Chart1.Series(0).ChartType = "Pie"
            Chart1.Series(0).Points.AddXY("Internal Group", 300)
            Chart1.Series(0).Points.AddXY("OSEC", 100)
            Chart1.Series(0).Points.AddXY("Policy Group", 400)
            Chart1.Series(0).Points.AddXY("Operation Group", 200)
        Catch ex As Exception

        Finally
            Chart1.Dispose()
        End Try
    End Sub


End Class