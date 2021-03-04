Imports System.Web.UI.DataVisualization.Charting
Imports System.Globalization

Public Class ucDashboardCount
    Inherits System.Web.UI.UserControl
    Dim iCtr As Integer = 0
    Dim iretVal As Integer
    Dim iIdx As Integer
    Dim ADate As String
    Dim Task As String
    Dim ShowDocLink As Boolean = False
    Dim Bureau As String
    Dim OfficeCode As String
#Region "Events"
    Public Event e_completequick()
    Public Event e_completelong()
    Public Event e_pendingnewest()
    Public Event e_pendingoldest()
    Public Event e_showBureauComplete()
    Public Event e_showBureauPending()
#End Region
#Region "Properties"


    'Dim dStatus As String

    Public Property pStatus As String
        Get
            Return hfStatus.Value
        End Get
        Set(ByVal value As String)
            hfStatus.Value = value
        End Set
    End Property

    'Dim RefNo As String

    Public Property pRefNo As String
        Get
            Return hfRefNo.Value
        End Get
        Set(ByVal value As String)
            hfRefNo.Value = value
        End Set
    End Property

    'Dim PersonnelInCharge As String
    Public Property pPersonnelInCharge As String
        Get
            Return hfPersonnelInCharge.Value
        End Get
        Set(ByVal value As String)
            hfPersonnelInCharge.Value = value
        End Set
    End Property

    'Dim StatusId As String
    Public Property pStatusId As String
        Get
            Return hfStatus.Value
        End Get
        Set(ByVal value As String)
            hfStatus.Value = value
        End Set
    End Property

    'Dim Subject As String
    Public Property pSubject As String
        Get
            Return hfSubject.Value
        End Get
        Set(ByVal value As String)
            hfSubject.Value = value
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
    'Dim DueDate As String
    Public Property pDueDate As String
        Get
            Return hfDueDate.Value
        End Get
        Set(ByVal value As String)
            hfDueDate.Value = value
        End Set
    End Property
    'Dim lsAction As String

    Public Property pAction As String
        Get
            Return hfAction.Value
        End Get
        Set(ByVal value As String)
            hfAction.Value = value
        End Set
    End Property

    Public Property pReset As String
        Get
            Return hfReset.Value
        End Get
        Set(ByVal value As String)
            hfReset.Value = value
        End Set
    End Property

    Public Property pBureau As String
        Get
            Return Bureau
        End Get
        Set(ByVal value As String)
            Bureau = value
        End Set
    End Property

    Public Property pOfficeCode As String
        Get
            Return hfOfcCode.Value
        End Get
        Set(ByVal value As String)
            hfOfcCode.Value = value
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
    Dim ToDate As String = ""
    Dim FromDate As String = ""

    Public Property pFromDate As String
        Get
            Return hfStartDate.Value
        End Get
        Set(ByVal value As String)
            hfStartDate.Value = value
        End Set
    End Property

    Public Property pToDate As String
        Get
            Return hfEndDate.Value
        End Get
        Set(ByVal value As String)
            hfEndDate.Value = value
        End Set
    End Property
    Dim OfcCode As String = ""
    Public Property pOfcCode As String
        Get
            Return hfOfcCode.Value
        End Get
        Set(ByVal value As String)
            hfOfcCode.Value = value
        End Set
    End Property
#End Region

    Public Sub ResetChart()
        rptAged.Visible = False
        rptTotalAmount.Visible = False
        rptQuick.Visible = False
        ChartDays.Visible = False
        trChartDays.Visible = False
        tblChartDaysPage.Visible = False
        hfChartPending.Value = "5"
        hfCurrentChartDays.Value = "5"
        pChartDays.Update()
        ChartPending.Visible = False
        tblChartPending.Visible = False
        trChartPending.Visible = False
        pChartPending.Update()
        ChartDistribution.Visible = False
        imgDays.ImageUrl = "images/showpanelr.png"
        imgAged.ImageUrl = "images/showpanelr.png"
        imgBk.ImageUrl = "images/showpanelr.png"
        imgPending.ImageUrl = "images/showpanelr.png"
        imgDistribution.ImageUrl = "images/showpanelr.png"
        imgQuick.ImageUrl = "images/showpanelr.png"
    End Sub
    Public Sub HideGraph()
        gDistribution.Visible = False
        gTotalAmount.Visible = False
        gAged.visible = False
        gQuick.visible = False
    End Sub
    Public Sub RetrieveAction()
        Dim od As DocList
        Dim ldata As DataTable
        Try
            od = New DocList
            If DocSession.sUserRole <> "A" Then
                'od.pOfficeCode = DocSession.sOfcCode
            Else
                If pOfcCode <> "" Then
                    od.pOfficeCode = pOfcCode
                End If

            End If


                od.pRefNo = pRefNo
                od.pDueDate = pDueDate
                od.pSubject = pSubject
                od.pPersonnelInCharge = pPersonnelInCharge
                od.pCreatedDateFrom = pFromDate
                od.pCreatedDateTo = pToDate
                od.pStatusId = pStatusId
            'Dim lrcv As Integer = od.DocAllCount()
            Dim lrcv As Integer = od.DocPendingCountAdmin("")
                ltdr.Text = lrcv.ToString("#,##0") ' total document received
                lnadr.Text = od.DocStatusCount("18", " = ").ToString("#,##0")
                ladr.Text = od.DocStatusCount("18", " <> ").ToString("#,##0")

                'pending

            ldata = od.DocPendingAdmin
                Dim lipd As Integer = CInt(ldata(0)("pd"))
                Dim liaday As Integer = CInt(ldata(0)("aday"))
                Dim liamin As Integer = CInt(ldata(0)("amin"))
                Dim liahour As Integer = CInt(ldata(0)("ahour"))

                Dim limday As Integer = CInt(ldata(0)("mday"))
                Dim limmin As Integer = CInt(ldata(0)("mmin"))
                Dim limhour As Integer = CInt(ldata(0)("mhour"))

                Dim linday As Integer = CInt(ldata(0)("nday"))
                Dim linmin As Integer = CInt(ldata(0)("nmin"))
                Dim linhour As Integer = CInt(ldata(0)("nhour"))
                Dim lsaDay As String = "day"
                Dim lsaHr As String = "hour"
                Dim lsaMin As String = "min"
                Dim lsmDay As String = "day"
                Dim lsmHr As String = "hour"
                Dim lsmMin As String = "min"
                Dim lsnDay As String = "day"
                Dim lsnHr As String = "hour"
                Dim lsnMin As String = "min"

                lpd.Text = Math.Round(((lipd / lrcv) * 100), 2).ToString("##0.00") & "%"

                If liaday > 1 Then
                    lapd.Text = liaday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    lapd.Text = liaday.ToString("#,##0") & " day"
                ElseIf liahour > 1 Then
                    lapd.Text = liahour.ToString("#,##0") & " hours"
                ElseIf liahour = 1 Then
                    lapd.Text = liahour.ToString("#,##0") & " hour"
                End If

                If limday > 1 Then
                    lopd.Text = limday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    lopd.Text = limday.ToString("#,##0") & " day"
                ElseIf limhour > 1 Then
                    lopd.Text = limhour.ToString("#,##0") & " hours"
                ElseIf limhour = 1 Then
                    lopd.Text = limhour.ToString("#,##0") & " hour"
                Else
                    lopd.Text = limmin.ToString("#,##0") & " min"
                End If

                If linday > 1 Then
                    lnpd.Text = linday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    lnpd.Text = linday.ToString("#,##0") & " day"
                ElseIf linhour > 1 Then
                    lnpd.Text = linhour.ToString("#,##0") & " hours"
                ElseIf linhour = 1 Then
                    lnpd.Text = linhour.ToString("#,##0") & " hour"
                Else
                    lnpd.Text = linmin.ToString("#,##0") & " min"
                End If

                'completed
            ldata = od.DocCompletedAdmin
                lipd = CInt(ldata(0)("cd"))
                liaday = CInt(ldata(0)("aday"))
                liamin = CInt(ldata(0)("amin"))
                liahour = CInt(ldata(0)("ahour"))

                limday = CInt(ldata(0)("mday"))
                limmin = CInt(ldata(0)("mmin"))
                limhour = CInt(ldata(0)("mhour"))

                linday = CInt(ldata(0)("nday"))
                linmin = CInt(ldata(0)("nmin"))
                linhour = CInt(ldata(0)("nhour"))
                lsaDay = "day"
                lsaHr = "hour"
                lsaMin = "min"
                lsmDay = "day"
                lsmHr = "hour"
                lsmMin = "min"
                lsnDay = "day"
                lsnHr = "hour"
                lsnMin = "min"

                lcd.Text = Math.Round(((lipd / lrcv) * 100), 2).ToString("##0.00") & "%"

                If liaday > 1 Then
                    lacd.Text = liaday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    lacd.Text = liaday.ToString("#,##0") & " day"
                ElseIf liahour > 1 Then
                    lacd.Text = liahour.ToString("#,##0") & " hours"
                ElseIf liahour = 1 Then
                    lacd.Text = liahour.ToString("#,##0") & " hour"
                Else
                    lacd.Text = liamin.ToString("#,##0") & " min"
                End If

                If limday > 1 Then
                    llcd.Text = limday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    llcd.Text = limday.ToString("#,##0") & " day"
                ElseIf limhour > 1 Then
                    llcd.Text = limhour.ToString("#,##0") & " hours"
                ElseIf limhour = 1 Then
                    llcd.Text = limhour.ToString("#,##0") & " hour"
                Else
                    llcd.Text = limmin.ToString("#,##0") & " min"
                End If

                If linday > 1 Then
                    lqcd.Text = linday.ToString("#,##0") & " days"
                ElseIf liaday = 1 Then
                    lqcd.Text = linday.ToString("#,##0") & " day"
                ElseIf linhour > 1 Then
                    lqcd.Text = linhour.ToString("#,##0") & " hours"
                ElseIf linhour = 1 Then
                    lqcd.Text = linhour.ToString("#,##0") & " hour"
                Else
                    lqcd.Text = linmin.ToString("#,##0") & " min"
                End If


                pDB.Update()
        Catch ex As Exception
            Throw New Exception("Error Message: 1" & ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
        End Try

    End Sub

    Public Sub fSelectComplete(ByVal sender As Object, ByVal e As System.EventArgs)
        Using lbSort As LinkButton = DirectCast(sender, LinkButton)
            pBureau = DirectCast(lbSort.Parent.FindControl("lbBureau"), LinkButton).Text
            pOfficeCode = DirectCast(lbSort.Parent.FindControl("lOfficeCode"), Literal).Text
            RaiseEvent e_showBureauComplete()
        End Using

    End Sub

    Public Sub fSelectPending(ByVal sender As Object, ByVal e As System.EventArgs)
        Using lbSort As LinkButton = DirectCast(sender, LinkButton)
            pBureau = DirectCast(lbSort.Parent.FindControl("lbBureau"), LinkButton).Text
            pOfficeCode = DirectCast(lbSort.Parent.FindControl("lOfficeCode"), Literal).Text
            RaiseEvent e_showBureauPending()
        End Using

    End Sub

    Public Sub ViewDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Using ri As RepeaterItem = DirectCast(DirectCast(sender, LinkButton).NamingContainer, RepeaterItem)
            DocSession.sDocID = DirectCast(ri.FindControl("lDocId"), Literal).Text
            Response.Redirect("view.aspx")
        End Using

    End Sub

    Public Sub LoadAgedBureau()
        Dim od As DocList
        Try
            If DocSession.sUserRole = "A" Then
                od = New DocList
                od.pOfficeCode = ""

                od.pCreatedDateFrom = pFromDate
                od.pCreatedDateTo = pToDate
                iCtr = 1
                rptAged.Visible = True
                rptAged.DataSource = od.RetrieveTop10Pending
                rptAged.DataBind()
                
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadQuickBureau()
        Dim od As DocList
        Try
            If DocSession.sUserRole = "A" Then
                od = New DocList
                od.pOfficeCode = ""

                od.pCreatedDateFrom = pFromDate
                od.pCreatedDateTo = pToDate
                
                iCtr = 1
                rptQuick.Visible = True
                rptQuick.DataSource = od.RetrieveTop10Quick
                rptQuick.DataBind()

            End If

        Catch ex As Exception

        End Try
    End Sub
#Region "Total Documents"
    Private Sub imgShowAction_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgShowAction.Click
        pnlAction.Visible = Not pnlAction.Visible
        If InStr(imgShowAction.ImageUrl, "show") > 0 Then
            imgShowAction.ImageUrl = "images/hidepanel.png"
        Else
            imgShowAction.ImageUrl = "images/showpanel.png"
        End If
        pDB.Update()
    End Sub
#End Region
#Region "Button Event"
    Private Sub lblcd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblcd.Click
        RaiseEvent e_completelong()
    End Sub

    Private Sub lbnpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbnpd.Click
        RaiseEvent e_pendingnewest()
    End Sub

    Private Sub lbopd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbopd.Click
        RaiseEvent e_pendingoldest()
    End Sub

    Private Sub lbqcd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbqcd.Click
        RaiseEvent e_completequick()
    End Sub
#End Region

#Region "Total Amount Released"
    Public Sub LoadTotalAmountReleased()
        Dim od As DocList
        Try
            If DocSession.sUserRole = "A" Then
                od = New DocList
                od.pOfficeCode = ""

                od.pCreatedDateFrom = pFromDate
                od.pCreatedDateTo = pToDate
                iCtr = 1
                rptTotalAmount.Visible = True
                rptTotalAmount.DataSource = od.RetrieveTotalAmountReleased2
                rptTotalAmount.DataBind()

                pnlTotalAmount.Update()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rptTotalAmount_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTotalAmount.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim ci As New CultureInfo("en-us")
            DirectCast(e.Item.FindControl("lno"), Literal).Text = iCtr.ToString("#,##0")
            DirectCast(e.Item.FindControl("lAmount"), Literal).Text = CDbl(DirectCast(e.Item.FindControl("lAmount"), Literal).Text).ToString("#,##0.00")
            iCtr += 1
        End If
    End Sub

    Private Sub imgBk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBk.Click
        If imgBk.ImageUrl = "images/showpanelr.png" Then
            LoadTotalAmountReleased()
        ElseIf rptTotalAmount.Items.Count > 0 Then
            rptTotalAmount.Visible = Not rptTotalAmount.Visible
        Else
            LoadTotalAmountReleased()
        End If
        'imgBk.Visible = Not imgBk.Visible
        If InStr(imgBk.ImageUrl, "show") > 0 Then
            imgBk.ImageUrl = "images/hidepanel.png"
        Else
            imgBk.ImageUrl = "images/showpanel.png"
        End If

    End Sub
#End Region
#Region "Quick"
    Private Sub rptQuick_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptQuick.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            DirectCast(e.Item.FindControl("lno"), Literal).Text = iCtr.ToString("#,##0")
            iCtr += 1
        End If
    End Sub

    Private Sub imgQuick_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgQuick.Click
        If imgQuick.ImageUrl = "images/showpanelr.png" Then
            LoadQuickBureau()
        ElseIf rptQuick.Items.Count > 0 Then
            rptQuick.Visible = Not rptQuick.Visible
        Else
            LoadQuickBureau()
        End If
        'imgBk.Visible = Not imgBk.Visible
        If InStr(imgQuick.ImageUrl, "show") > 0 Then
            imgQuick.ImageUrl = "images/hidepanel.png"
        Else
            imgQuick.ImageUrl = "images/showpanel.png"
        End If
    End Sub
#End Region
#Region "Aged"
    Private Sub rptAged_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAged.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            DirectCast(e.Item.FindControl("lno"), Literal).Text = iCtr.ToString("#,##0")
            iCtr += 1
        End If
    End Sub

    Private Sub imgAged_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAged.Click
        If imgAged.ImageUrl = "images/showpanelr.png" Then
            LoadAgedBureau()
        ElseIf rptAged.Items.Count > 0 Then
            rptAged.Visible = Not rptAged.Visible
        Else
            LoadAgedBureau()
        End If
        'imgBk.Visible = Not imgBk.Visible
        If InStr(imgAged.ImageUrl, "show") > 0 Then
            imgAged.ImageUrl = "images/hidepanel.png"
        Else
            imgAged.ImageUrl = "images/showpanel.png"
        End If
    End Sub
#End Region
#Region "Distribution"

    Public Sub LoadDistributionChart()
        Dim od As DocList
        Dim ldata As DataTable
        Try
            od = New DocList

            od.pOfficeCode = ""

            od.pCreatedDateFrom = pFromDate
            od.pCreatedDateTo = pToDate

            If DocSession.sUserRole = "A" Then
                ChartDistribution.Visible = True
                'Chart1.DataSource = od.DocPendingDistribution
                'Chart1.Series(0).XValueMember = "Description"
                'Chart1.Series(0).YValueMembers = "DocCount"
                'Chart1.DataBind()
                'ChartDistribution.DataSourceID = "a"

                ldata = od.DocPendingDistribution

                Dim yValues As Double() = {0} '= {65.62, 75.54, 60.45, 34.73, 85.42}
                Dim xValues As String() = {""} '= {"France", "Canada", "Germany", "USA", "Italy"}
                Dim aiCtr As Integer
                Dim iSmallest As Double = 0
                xValues.Resize(xValues, ldata.Rows.Count)
                yValues.Resize(yValues, ldata.Rows.Count)
                For Each lrow As DataRow In ldata.Rows

                    xValues.SetValue(lrow("Description"), aiCtr)
                    yValues.SetValue(CDbl(lrow("DocCount")), aiCtr)
                    aiCtr += 1
                    If CDbl(lrow("DocCount")) > iSmallest Then
                        iSmallest = CDbl(lrow("DocCount"))
                    End If
                Next

                'Chart1.Series("Series1").Points.DataBind(od.DocPendingDistribution, "description", "docCount", "")
                ChartDistribution.Series("Series1").Points.DataBindXY(xValues, yValues)
                ChartDistribution.Series("Series1")("PieLabelStyle") = "Outside"
                For liCtr As Integer = 0 To xValues.Length - 1
                    ChartDistribution.Series("Series1").Points(liCtr).LegendText = xValues(liCtr)
                    If yValues(liCtr) = iSmallest Then
                        ChartDistribution.Series("Series1").Points(liCtr).CustomProperties = "Exploded=True"
                    End If
                Next
                ChartDistribution.Legends(0).Docking = Docking.Top
            Else
                ChartDistribution.Visible = False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try




    End Sub

    Private Sub imgDistribution_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDistribution.Click

        If imgDistribution.ImageUrl = "images/showpanelr.png" Then
            LoadDistributionChart()
        ElseIf ChartDistribution.Legends(0).Docking = Docking.Top Then
            ChartDistribution.Visible = Not ChartDistribution.Visible
        Else
            LoadDistributionChart()
        End If
        'imgBk.Visible = Not imgBk.Visible
        If InStr(imgDistribution.ImageUrl, "show") > 0 Then
            imgDistribution.ImageUrl = "images/hidepanel.png"
        Else
            imgDistribution.ImageUrl = "images/showpanel.png"
        End If

    End Sub
#End Region
#Region "Pending"
    Private Function ProcessPendingDaysData(ByVal cv As String, ByVal db As DataTable) As DataTable
        Dim ldt As DataTable = db.Clone
        Dim srow As Int16
        Dim erow As Int16
        Dim ctr As Int16 = 1
        srow = CInt(cv) - 4
        erow = CInt(cv)
        If erow <= db.Rows.Count Then
            lChartPending.Text = CStr(srow) & " - " & CStr(erow) & " of " & db.Rows.Count.ToString
        Else
            If db.Rows.Count > 0 Then
                lChartPending.Text = CStr(srow) & " - " & CStr(db.Rows.Count) & " of " & db.Rows.Count.ToString
            Else
                lChartPending.Text = "No records retrived."
            End If

        End If

        For Each drow As DataRow In db.Rows
            If ctr >= srow AndAlso ctr <= erow Then
                ldt.ImportRow(drow)
            End If
            ctr = ctr + 1
        Next
        Return ldt
    End Function

    Private Sub ShowNextPreviousChartPending(ByVal irow As Integer, ByVal cv As Integer)
        If irow > cv Then
            imgChartPendingNext.Visible = True
            If hfChartPending.Value = "5" Then
                imgChartPendingPrevious.Visible = False
            Else
                imgChartPendingPrevious.Visible = True
            End If
        Else
            If CInt(hfChartPending.Value) > 5 Then
                imgChartPendingNext.Visible = False
                imgChartPendingPrevious.Visible = True
            Else
                imgChartPendingNext.Visible = False
                imgChartPendingPrevious.Visible = False
            End If
        End If

    End Sub
    Public Sub LoadPendingDays()
        Dim od As DocList
        Dim ldt As DataTable
        Try
            od = New DocList
            If DocSession.sUserRole <> "A" Then
                'od.pOfficeCode = DocSession.sOfcCode
            Else
                od.pOfficeCode = pOfcCode
            End If
            od.pCreatedDateFrom = pFromDate
            od.pCreatedDateTo = pToDate

            ldt = od.DocPendingCountPerDocType
            ViewState("PendingCount") = ldt
            ChartPending.DataSource = ProcessPendingDaysData(hfChartPending.Value, ldt)
            ShowNextPreviousChartPending(ldt.Rows.Count, CInt(hfChartPending.Value))
            ChartPending.Series(0).XValueMember = "DocType"
            ChartPending.Series(0).YValueMembers = "PendingCount"
            ChartPending.DataBind()
            ChartPending.Visible = True
            trChartPending.Visible = True
            tblChartPending.Visible = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldt Is Nothing Then
                ldt.Dispose()
                ldt = Nothing
            End If
        End Try




    End Sub

    Private Sub imgPending_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPending.Click
        If imgPending.ImageUrl = "images/showpanelr.png" Then
            LoadPendingDays()
        ElseIf ChartPending.Series(0).XValueMember = "DocType" Then
            ChartPending.Visible = Not ChartPending.Visible
            trChartPending.Visible = Not trChartPending.Visible
            tblChartPending.Visible = Not tblChartPending.Visible
        Else
            LoadPendingDays()
        End If
        'imgBk.Visible = Not imgBk.Visible
        If InStr(imgPending.ImageUrl, "show") > 0 Then
            imgPending.ImageUrl = "images/hidepanel.png"
        Else
            imgPending.ImageUrl = "images/showpanel.png"
        End If
    End Sub
    Private Sub imgChartPendingNext_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChartPendingNext.Click
        hfChartPending.Value = (CInt(hfChartPending.Value) + 5).ToString
        Using ldt As DataTable = DirectCast(ViewState("PendingCount"), DataTable)

            ChartPending.DataSource = ProcessPendingDaysData(hfChartPending.Value, ldt)

            ChartPending.Series(0).XValueMember = "DocType"
            ChartPending.Series(0).YValueMembers = "PendingCount"
            ChartPending.DataBind()
            ChartPending.Visible = True
            ShowNextPreviousChartPending(ldt.Rows.Count, CInt(hfChartPending.Value))
        End Using


    End Sub

    Private Sub imgChartPendingPrevious_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChartPendingPrevious.Click
        hfChartPending.Value = (CInt(hfChartPending.Value) - 5).ToString
        Using ldt As DataTable = DirectCast(ViewState("PendingCount"), DataTable)

            ChartPending.DataSource = ProcessPendingDaysData(hfChartPending.Value, ldt)

            ChartPending.Series(0).XValueMember = "DocType"
            ChartPending.Series(0).YValueMembers = "PendingCount"
            ChartPending.DataBind()
            ChartPending.Visible = True
            ShowNextPreviousChartPending(ldt.Rows.Count, CInt(hfChartPending.Value))
        End Using

    End Sub
#End Region
#Region "Chart Days"
    Private Function ProcessChartDaysData(ByVal cv As String, ByVal db As DataTable) As DataTable
        Dim ldt As DataTable
        Dim srow As Int16
        Dim erow As Int16
        Dim ctr As Int16 = 1
        Try
            ldt = db.Clone
            srow = CInt(cv) - 4
            erow = CInt(cv)
            If erow <= db.Rows.Count Then
                lTotalChartDays.Text = CStr(srow) & " - " & CStr(erow) & " of " & db.Rows.Count.ToString
            Else
                If db.Rows.Count > 0 Then
                    lTotalChartDays.Text = CStr(srow) & " - " & CStr(db.Rows.Count) & " of " & db.Rows.Count.ToString
                Else
                    lTotalChartDays.Text = "No records retrived."
                End If

            End If

            For Each drow As DataRow In db.Rows
                If ctr >= srow AndAlso ctr <= erow Then
                    ldt.ImportRow(drow)
                End If
                ctr = ctr + 1
            Next
            Return ldt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldt Is Nothing Then
                ldt.Dispose()
            End If
            If Not db Is Nothing Then
                db.Dispose()
            End If
        End Try
    End Function
    Public Sub LoadChartDays()
        Dim od As DocList
        Dim ldt As DataTable
        Try
            ChartDays.Visible = True
            od = New DocList
            If DocSession.sUserRole <> "A" Then
                'od.pOfficeCode = DocSession.sOfcCode
            Else
                od.pOfficeCode = pOfcCode
            End If
            'od.pRefNo = pRefNo
            'od.pDueDate = pDueDate
            'od.pStatusId = pStatusId
            'od.pPersonnelInCharge = pPersonnelInCharge
            'od.pSubject = pSubject
            'od.pIdx = hfCurrentChartDays.Value
            'od.pRowsPerPage = 5
            od.pCreatedDateFrom = pFromDate
            od.pCreatedDateTo = pToDate
            ldt = od.DocCompletionDays
            ViewState("CompletionDays") = ldt
            ChartDays.DataSource = ProcessChartDaysData(hfCurrentChartDays.Value, ldt)
            'If DocSession.sUserRole <> "A" Then
            'ChartDays.Titles(0).Text = "AGEING OF DOCUMENTS PER TYPE"
            'End If
            ShowNextPreviousChartDays(ldt.Rows.Count, CInt(hfCurrentChartDays.Value))

            ChartDays.Series(0).XValueMember = "DocType"
            ChartDays.Series(0).YValueMembers = "DaysCompleted"

            ChartDays.DataBind()

            trChartDays.Visible = True
            tblChartDaysPage.Visible = True
            pChartDays.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)

        Finally
            If Not ldt Is Nothing Then
                ldt.Dispose()
                ldt = Nothing
            End If
        End Try




    End Sub

    Private Sub ShowNextPreviousChartDays(ByVal irow As Integer, ByVal cv As Integer)
        If irow > cv Then
            imgChartDaysNext.Visible = True
            If hfCurrentChartDays.Value = "5" Then
                imgChartDaysPrevious.Visible = False
            Else
                imgChartDaysPrevious.Visible = True
            End If
        Else
            If CInt(hfCurrentChartDays.Value) > 5 Then
                imgChartDaysNext.Visible = False
                imgChartDaysPrevious.Visible = True
            Else
                imgChartDaysNext.Visible = False
                imgChartDaysPrevious.Visible = False
            End If
        End If

    End Sub

    Private Sub imgDays_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDays.Click
        Try

        
        If imgDays.ImageUrl = "images/showpanelr.png" Then
            LoadChartDays()
        ElseIf ChartDays.Series(0).XValueMember = "DocType" Then
            ChartDays.Visible = Not ChartDays.Visible
            trChartDays.Visible = Not trChartDays.Visible
            tblChartDaysPage.Visible = Not tblChartDaysPage.Visible
        Else
            LoadChartDays()
        End If

        If InStr(imgDays.ImageUrl, "show") > 0 Then
            imgDays.ImageUrl = "images/hidepanel.png"
        Else
            imgDays.ImageUrl = "images/showpanel.png"
        End If
            pChartDays.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub imgChartDaysNext_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChartDaysNext.Click
        hfCurrentChartDays.Value = (CInt(hfCurrentChartDays.Value) + 5).ToString
        Using ldt As DataTable = DirectCast(ViewState("CompletionDays"), DataTable)
            ChartDays.DataSource = ProcessChartDaysData(hfCurrentChartDays.Value, ldt)
            ChartDays.Series(0).XValueMember = "DocType"
            ChartDays.Series(0).YValueMembers = "DaysCompleted"

            ChartDays.DataBind()
            ChartDays.Visible = True
            ShowNextPreviousChartDays(ldt.Rows.Count, CInt(hfCurrentChartDays.Value))
        End Using


    End Sub

    Private Sub imgChartDaysPrevious_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgChartDaysPrevious.Click
        hfCurrentChartDays.Value = (CInt(hfCurrentChartDays.Value) - 5).ToString
        Using ldt As DataTable = DirectCast(ViewState("CompletionDays"), DataTable)
            ChartDays.DataSource = ProcessChartDaysData(hfCurrentChartDays.Value, ldt)
            ChartDays.Series(0).XValueMember = "DocType"
            ChartDays.Series(0).YValueMembers = "DaysCompleted"

            ChartDays.DataBind()
            ChartDays.Visible = True
            ShowNextPreviousChartDays(ldt.Rows.Count, CInt(hfCurrentChartDays.Value))
        End Using

    End Sub
#End Region
End Class