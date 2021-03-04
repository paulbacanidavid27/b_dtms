Public Class ucDashboard
    Inherits System.Web.UI.UserControl
    Public Event e_action()
    Dim iretVal As Integer
    Dim iIdx As Integer
    Dim ToDate As String
    Dim FromDate As String
    Dim OfcCode As String
    Dim Task As String
    Dim ShowDocLink As Boolean = False
    Dim PersonnelInCharge As String
    Dim Status As String
    Dim Subject As String
    Dim DueDate As String

    Dim RefNo As String

    Public Property pRefNo As String
        Get
            Return RefNo
        End Get
        Set(ByVal value As String)
            RefNo = value
        End Set
    End Property

    Public Property pDueDate As String
        Get
            Return DueDate
        End Get
        Set(ByVal value As String)
            DueDate = value
        End Set
    End Property

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

    Dim lsAction As String

    Public Property pAction As String
        Get
            Return lsAction
        End Get
        Set(ByVal value As String)
            lsAction = value
        End Set
    End Property

    Dim lsSortCol As String
    Public Property pSortCol As String
        Get
            Return lsSortCol
        End Get
        Set(ByVal value As String)
            lsSortCol = value
        End Set
    End Property
    Dim lsSortOrder As String = "Asc"


    Public Property pSortOrder As String
        Get
            Return lsSortOrder
        End Get
        Set(ByVal value As String)
            lsSortOrder = value
        End Set
    End Property

    Public Property pOfcCode As String
        Get
            Return OfcCode
        End Get
        Set(ByVal value As String)
            OfcCode = value
        End Set
    End Property

    Public Property pFromDate As String
        Get
            Return FromDate
        End Get
        Set(ByVal value As String)
            FromDate = value
        End Set
    End Property

    Public Property pToDate As String
        Get
            Return ToDate
        End Get
        Set(ByVal value As String)
            ToDate = value
        End Set
    End Property

    Dim dStatus As String

    Public Property pStatus As String
        Get
            Return dStatus
        End Get
        Set(ByVal value As String)
            dStatus = value
        End Set
    End Property

    

    Public Property pPersonnelInCharge As String
        Get
            Return PersonnelInCharge
        End Get
        Set(ByVal value As String)
            PersonnelInCharge = value
        End Set
    End Property

    Dim StatusId As String
    Public Property pStatusId As String
        Get
            Return StatusId
        End Get
        Set(ByVal value As String)
            StatusId = value
        End Set
    End Property


    Public Property pSubject As String
        Get
            Return Subject
        End Get
        Set(ByVal value As String)
            Subject = value
        End Set
    End Property

    Dim ReceivedDate As String
    Public Property pReceivedDate As String
        Get
            Return ReceivedDate
        End Get
        Set(ByVal value As String)
            ReceivedDate = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'RetrieveAction(DocSession.sUserId)
        End If
    End Sub

    Public Sub RetrieveAction(ByVal aOfcCode As String)

        Dim ldata As DataTable
        Try
            Dim od As New DocList
            od.pOfficeCode = ""
            If DocSession.sUserRole <> "A" Then
                'od.pOfficeCode = DocSession.sOfcCode
            Else
                If aOfcCode <> "" Then
                    od.pOfficeCode = aOfcCode
                End If
            End If
            od.pCreatedDateFrom = pFromDate
            od.pCreatedDateTo = pToDate
            od.pSortOrder = pSortOrder
            od.pSortCol = pSortCol
            od.pRefNo = pRefNo
            od.pSubject = pSubject
            od.pDueDate = pDueDate
            od.pPersonnelInCharge = pPersonnelInCharge
            od.pStatusId = pStatusId
            'If pAction <> "P" AndAlso DocSession.doc_SortCol = "Age" Then
            'od.pSortCol = "Age2"
            'Else
            'od.pSortCol = DocSession.doc_SortCol
            'End If

            iretVal = od.DocPendingCountAdmin(pAction)
            If iretVal > 0 Then
                od.pIdx = pIdx
                od.pRowsPerPage = DocSession.RowsPerPage

                ldata = od.DocRequestListAdmin(pAction)
                If ldata.Rows.Count > DocSession.RowsPerPage Then
                    ldata.Rows.RemoveAt(DocSession.RowsPerPage)
                End If

                rptDashboard.DataSource = ldata
                rptDashboard.DataBind()
                pNo.Visible = False
                rptDashboard.Visible = True
            Else
                pNo.Visible = True
                rptDashboard.Visible = False
            End If

            pDB.Update()
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
        Using ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
            DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
            Response.Redirect("view.aspx")
        End Using

    End Sub

   

    
    Private Sub rptDashboard_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDashboard.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If DirectCast(e.Item.FindControl("ldd"), Literal).Text = "1/1/1900" Then
                DirectCast(e.Item.FindControl("ldd"), Literal).Text = ""
            End If

        End If
    End Sub

    Public Sub sortColumnHeader(ByVal asCol As String, Optional ByVal asOrder As String = "", Optional ByVal asColumn As String = "")

        Dim img As Image
        Dim lsSort As String
        'DocSession.doc_DocCurrentPage = 1
        Try

        
            If imgSort1.ID = "imgSort" & asCol Then
                img = imgSort1
                lsSort = lbSort1.Text
                imgSort1.Visible = True
            Else
                imgSort1.Visible = False
            End If
            If imgSort2.ID = "imgSort" & asCol Then
                img = imgSort2
                lsSort = lbSort2.Text
                imgSort2.Visible = True
            Else
                imgSort2.Visible = False

            End If
            If imgSort3.ID = "imgSort" & asCol Then
                img = imgSort3
                lsSort = lbSort3.Text
                imgSort3.Visible = True
            Else
                imgSort3.Visible = False

            End If
            If imgSort4.ID = "imgSort" & asCol Then
                img = imgSort4
                lsSort = lbSort4.Text
                imgSort4.Visible = True
            Else
                imgSort4.Visible = False

            End If
            If imgSort5.ID = "imgSort" & asCol Then
                img = imgSort5
                lsSort = lbSort5.Text
                imgSort5.Visible = True
            Else
                imgSort5.Visible = False

            End If

            If imgSort6.ID = "imgSort" & asCol Then
                img = imgSort6
                lsSort = lbSort6.Text
                imgSort6.Visible = True
            Else
                imgSort6.Visible = False
            End If

            If imgSort7.ID = "imgSort" & asCol Then
                img = imgSort7
                lsSort = lbSort7.Text
                imgSort7.Visible = True
            Else
                imgSort7.Visible = False
            End If

            If imgSort8.ID = "imgSort" & asCol Then
                img = imgSort8
                lsSort = lbSort8.Text
                imgSort8.Visible = True
            Else
                imgSort8.Visible = False
            End If
            If asOrder <> "" Then
                pSortOrder = asOrder
                lsSort = asColumn
                img.ImageUrl = "images/" & asOrder & ".png"
                img.Visible = True
            Else

                If img.Visible Then
                    If img.ImageUrl.ToLower = "images/asc.png" Then
                        img.ImageUrl = "images/desc.png"
                        pSortOrder = "Desc"
                    Else
                        img.ImageUrl = "images/asc.png"
                        'oDocList.pSortOrder = "Asc"
                        pSortOrder = "Asc"
                    End If
                Else
                    img.ImageUrl = "images/Asc.png"
                    'oDocList.pSortOrder = "Asc"

                    'DocSession.doc_SortOrder = "Asc"
                    pSortOrder = "Asc"
                    img.Visible = True
                End If
            End If
            pSortCol = lsSort

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not img Is Nothing Then
                img.Dispose()
            End If
        End Try
        'RaiseEvent e_action()

    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Using lbSort As LinkButton = DirectCast(sender, LinkButton)
            sortColumnHeader(Right(lbSort.ID, 1))
            RaiseEvent e_action()
        End Using

    End Sub
End Class