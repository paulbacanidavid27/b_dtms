Public Class UserControlEmail
    Inherits System.Web.UI.UserControl
    Dim Author As String
    Dim UserEmail As String
    Dim DocId As String
    Dim DocType As String
    Public Event e_check()
    Public Event e_uncheck()
    Public Event e_click()

    Public Property pAuthor As String
        Get
            Return Author
        End Get
        Set(ByVal value As String)
            Author = value
        End Set
    End Property

    Public Property pUserEmail As String
        Get
            Return UserEmail
        End Get
        Set(ByVal value As String)
            UserEmail = value
        End Set
    End Property

    Public Property pDocId As String
        Get
            Return DocId
        End Get
        Set(ByVal value As String)
            DocId = value
        End Set
    End Property
    Public WriteOnly Property pHideCloseButton As Boolean

        Set(ByVal value As Boolean)
            imgClose.Visible = value
        End Set
    End Property
    Public Property pDocType As String
        Get
            Return DocType
        End Get
        Set(ByVal value As String)
            DocType = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'SearchEmail()
            imgClose.Focus()
        End If
    End Sub

    Public Sub fSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelected"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        pUserEmail = DirectCast(rpt.FindControl("lEmail"), Literal).Text
        pAuthor = DirectCast(rpt.FindControl("lApprover"), Literal).Text
        rpt.Visible = False
        RaiseEvent e_check()

    End Sub

    Public Sub fUnSelect2(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim lImg As ImageButton = DirectCast(sender, ImageButton)
        Dim lImg2 As ImageButton = DirectCast(lImg.Parent.FindControl("ImgSelect"), ImageButton)
        lImg2.Visible = Not lImg2.Visible
        lImg.Visible = Not lImg.Visible

        Dim rpt As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)

        pUserEmail = DirectCast(rpt.FindControl("lEmail"), Literal).Text

        RaiseEvent e_uncheck()

        pnl.Update()

    End Sub
    Public Sub RefreshEmail()
        txtBx.Text = "Enter Email Address or User Name..."
        SearchEmail()
    End Sub
    Private Sub SearchEmail()
        lSearchMsg.Visible = False
        Dim oDocs As New DocUser

        oDocs.pSearchString = IIf(txtBx.Text = "Enter Email Address or User Name...", "", txtBx.Text)
        oDocs.pDocType = DocSession.sDocType

        Using lodata As DataTable = oDocs.RetrieveUserEmail()
            If lodata.Rows.Count > 0 Then
                rptSub.DataSource = lodata
                rptSub.DataBind()
                rptSub.Visible = True
            Else
                rptSub.Visible = False
                lSearchMsg.Visible = True
                lSearchMsg.Text = "No records found."
            End If


        End Using

        pnl.Update()
    End Sub


    Private Sub imgSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearch.Click
        SearchEmail()
    End Sub

    
    Private Sub imgClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgClose.Click
        'Me.Visible = False
        pnl.Update()
        RaiseEvent e_click()
    End Sub
End Class
