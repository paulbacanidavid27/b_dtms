Public Class ucReceipt
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lAddress.Text = IIf(DocSession.sGroupAddress = "", "General Solano St, San Miguel, Manila", DocSession.sGroupAddress)
            'lTitle.Text = IIf(DocSession.ReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", DocSession.ReceiptReplyName.ToUpper())
            'lTitle2.Text = IIf(DocSession.ReceiptReplyName = "", "Department of Budget and Management", DocSession.ReceiptReplyName
            lTitle.Text = IIf(DocSession.ReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", Replace(DocSession.ReceiptReplyName, "\n", "<br/>"))
            lTitle2.Text = IIf(DocSession.ReceiptReplyName = "", "Department of Budget and Management", Replace(DocSession.ReceiptReplyName, "\n", " - "))
            imgLogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "dbm.png", DocSession.GroupLogo)
            imgLogo.DataBind()
            imgBottomlogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "logo.png", "otherlogo.png")
            imgBottomlogo.DataBind()
        End If
    End Sub
    Public Sub SetInfo()
        RetrieveOffice(pAuthor)
        lAddress.Text = IIf(pGroupAddress = "", "General Solano St, San Miguel, Manila", pGroupAddress)
        lTitle.Text = IIf(pReceiptReplyName = "", "DEPARTMENT OF BUDGET AND MANAGEMENT", Replace(pReceiptReplyName.ToUpper(), "\N", "<br/>"))
        lTitle2.Text = IIf(pReceiptReplyName = "", "Department of Budget and Management", Replace(pReceiptReplyName, "\n", " "))
        imgLogo.ImageUrl = "images/logo/" & IIf(pGroupLogo = "", "dbm.png", pGroupLogo)
        imgLogo.DataBind()
        imgBottomlogo.ImageUrl = "images/logo/" & IIf(DocSession.GroupLogo = "", "logo.png", "otherlogo.png")
        imgBottomlogo.DataBind()
    End Sub
    Public Sub AdditionalButtons()
        btAdd.Visible = True
        btView.Visible = True
    End Sub

    Public Sub ShowReceipt()
        'Dim odoc As New DocList
        'odoc.pDocId = DocSession.sDocID
        'lSender.Text = odoc.GetDocSender
        pMsg.Visible = True
    End Sub
    Public Property pAuthor As String
        Get
            Return hfAuthor.Value
        End Get
        Set(ByVal value As String)
            hfAuthor.Value = value
        End Set
    End Property
    Public Property pCreatedDate As String
        Get
            Return lackDate.Text
        End Get
        Set(ByVal value As String)
            lackDate.Text = value
        End Set
    End Property

    Dim lGroupAddress As String = ""

    Public Property pGroupAddress As String
        Get
            Return lGroupAddress
        End Get
        Set(ByVal value As String)
            lGroupAddress = value
        End Set
    End Property

    Dim lReceiptReplyName As String = ""

    Public Property pReceiptReplyName As String
        Get
            Return lReceiptReplyName
        End Get
        Set(ByVal value As String)
            lReceiptReplyName = value
        End Set
    End Property

    Dim lGroupLogo As String

    Public Property pGroupLogo As String
        Get
            Return lGroupLogo
        End Get
        Set(ByVal value As String)
            lGroupLogo = value
        End Set
    End Property
    Public Property pDocTitle As String
        Get
            Return lackTitle.Text
        End Get
        Set(ByVal value As String)
            lackTitle.Text = value
        End Set
    End Property

    Public Property pRoutedTo As String
        Get
            Return lRoutedTo.Text
        End Get
        Set(ByVal value As String)
            lRoutedTo.Text = value
        End Set
    End Property
    Public Property pRoutedToDisplay As Boolean
        Get
            Return lRoutedTo.Visible
        End Get
        Set(ByVal value As Boolean)
            lRoutedTo.Visible = value
        End Set
    End Property

    Public Property pCarbon As String
        Get
            Return lCarbon.Text
        End Get
        Set(ByVal value As String)
            lCarbon.Text = value
        End Set
    End Property

    Public Property pCarbonDisplay As Boolean
        Get
            Return lCarbon.Visible
        End Get
        Set(ByVal value As Boolean)
            lCarbon.Visible = value
        End Set
    End Property

    Public Property pSender As String
        Get
            Return lSender.Text
        End Get
        Set(ByVal value As String)
            lSender.Text = value
        End Set
    End Property
    Public Sub ShowRemarks()
        If pRemarks <> "" Then
            pNotes.Visible = True
        End If
    End Sub
    Public Property pRemarks As String
        Get
            Return lRemarks.Text
        End Get
        Set(ByVal value As String)
            lRemarks.Text = value
        End Set
    End Property
    Public Property pCopiesPages As String
        Get
            Return lCopiesPages.Text
        End Get
        Set(ByVal value As String)
            lCopiesPages.Text = value
        End Set
    End Property


    Public Property pCreatedBy As String
        Get
            Return lCreatedBy.Text
        End Get
        Set(ByVal value As String)
            lCreatedBy.Text = value
        End Set
    End Property

    Public Property pRefNo As String
        Get
            Return lackFilename.Text
        End Get
        Set(ByVal value As String)
            lackFilename.Text = value
            lrefno.Text = value
        End Set
    End Property
    
    Private Sub imgCloseAck_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCloseAck.Click
        Me.Visible = False
    End Sub

    Private Sub btView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btView.Click
        Response.Redirect("view.aspx")
    End Sub

    Private Sub btPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btPrint.Click
        Dim oDoc As DocList
        Try
            oDoc = New DocList
            oDoc.pUserId = DocSession.sUserId
            oDoc.pDocId = DocSession.sDocID
            oDoc.PrintDoc()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function RetrieveOffice(ByVal asUserId As String) As DataTable
        Dim s_sql As String = "select " & _
"GroupLogo = isnull(g.GroupLogo,''), " & _
        "AddressDesc = isnull(oa.AddressDesc,''), " & _
        "ReceiptReplyName = isnull(g.ReceiptReplyName,'') " & _
"from Users u inner join Groups g " & _
"on u.UserGroup = g.GroupId " & _
"inner join Office o " & _
"on o.OfficeCode = g.OfficeCode " & _
"inner join OfficeAddress oa " & _
"on oa.AddressCode = o.AddressCode " & _
"where u.UserId = '" & asUserId & "'"
        Dim ldata As DataTable
        Dim objCommand As clsSqlConn

        Try
            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.Text

            objCommand.CommandText = s_sql
            ldata = objCommand.ExecData()

            If ldata.Rows.Count > 0 Then
                pReceiptReplyName = ldata(0)("ReceiptReplyName")
                pGroupLogo = ldata(0)("GroupLogo")
                pGroupAddress = ldata(0)("AddressDesc")
            Else
                pReceiptReplyName = ""
                pGroupLogo = ""
                pGroupAddress = ""
            End If

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