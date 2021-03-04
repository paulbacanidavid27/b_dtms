Public Class ucTrackStatusUsers
    Inherits System.Web.UI.UserControl

    Dim DocId As Integer
    Dim FirstLoad As Boolean = False
    Public Property pFirstLoad As Boolean
        Get
            Return FirstLoad
        End Get
        Set(ByVal value As Boolean)
            FirstLoad = value
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
    Dim UserGroup As String

    Public Property pUserGroup As String
        Get
            Return UserGroup
        End Get
        Set(ByVal value As String)
            UserGroup = value
        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'RetrieveAction(DocSession.sUserId)
            'RetrieveStatus()

        End If
    End Sub



    Public Sub fClose()
        Me.Visible = False
    End Sub

    Public Sub RetrieveStatus(ByVal usrgrp As String, ByVal sseqno As String, ByVal eseqno As String)


        Dim ldata As DataTable
        Dim loData As DataTable
        Dim loRow As DataRow

        Try

            lHdr.Text = usrgrp
            Dim odh As New DocHistory
            odh.pDocId = pDocID
            odh.pUserGroup = pUserGroup
            odh.pStartSeqno = sseqno
            odh.pEndSeqNo = eseqno
            ldata = odh.GetGroupApprovers

            loData = New DataTable("tblGroup")


            loData.Columns.Add("profilePic1", Type.GetType("System.String"))
            loData.Columns.Add("profilePic2", Type.GetType("System.String"))
            loData.Columns.Add("profilePic3", Type.GetType("System.String"))
            loData.Columns.Add("assignedDate1", Type.GetType("System.String"))
            loData.Columns.Add("assignedDate2", Type.GetType("System.String"))
            loData.Columns.Add("assignedDate3", Type.GetType("System.String"))
            loData.Columns.Add("actionDate1", Type.GetType("System.String"))
            loData.Columns.Add("actionDate2", Type.GetType("System.String"))
            loData.Columns.Add("actionDate3", Type.GetType("System.String"))
            loData.Columns.Add("userName1", Type.GetType("System.String"))
            loData.Columns.Add("userName2", Type.GetType("System.String"))
            loData.Columns.Add("userName3", Type.GetType("System.String"))
            loData.Columns.Add("Duration1", Type.GetType("System.String"))
            loData.Columns.Add("Duration2", Type.GetType("System.String"))
            loData.Columns.Add("Duration3", Type.GetType("System.String"))
            loData.Columns.Add("Description1", Type.GetType("System.String"))
            loData.Columns.Add("Description2", Type.GetType("System.String"))
            loData.Columns.Add("Description3", Type.GetType("System.String"))

            Dim lsDurationDay, lactiondate, lassdate As String
            Dim lctr As Integer = 0

            'constant
            Dim liYearMin As Integer = 525600
            Dim liMonthMin As Integer = 43200
            Dim liDayMin As Integer = 1440
            Dim liHrMIn As Integer = 60

            Dim liDiff As Integer
            Dim liYear As Integer
            Dim liMonth As Integer
            Dim liDay As Integer

            Dim liHr As Integer
            Dim liMin As Integer
            Dim lsAct As String = ""
            lctr += 1
            If ldata.Rows.Count > 0 Then
                loRow = loData.NewRow()
                For Each lrow As DataRow In ldata.Rows
                    liYear = 0
                    liMonth = 0
                    liDay = 0
                    liHr = 0
                    loRow("profilePic" & CStr(lctr)) = lrow("profilePic")
                    loRow("userName" & CStr(lctr)) = lrow("username")
                    loRow("description" & CStr(lctr)) = lrow("description")
                    'loRow("Duration" & CStr(lctr)) = clong(lrow("duration"))
                    'liDuration = CLng(lrow("duration"))
                    'If IsDBNull(lrow("completeddate")) Then
                    If IsDBNull(lrow("actiondate")) Then
                        lactiondate = DateTime.Now.ToString
                        lsAct = "None"
                    Else
                        'lactiondate =
                        'lactiondate = CStr(lrow("completeddate"))
                        lactiondate = CStr(lrow("actiondate"))
                        lsAct = lactiondate
                    End If

                    If lsAct = "None" Then
                        loRow("actionDate" & CStr(lctr)) = "N/A"
                        loRow("assignedDate" & CStr(lctr)) = "N/A"
                        loRow("Duration" & CStr(lctr)) = "N/A"
                    Else

                        'need to change the label in ascx with received date when this is implemented
                        'lassdate = CStr(lrow("actiondate"))
                        lassdate = CStr(lrow("assigneddate"))
                        loRow("actionDate" & CStr(lctr)) = lsAct
                        loRow("assignedDate" & CStr(lctr)) = lassdate
                        If CDate(lassdate).ToShortDateString = "01/01/1900" OrElse CDate(lassdate).ToShortDateString = "1/1/1900" Then
                            liDiff = -1
                        Else
                            liDiff = DateDiff(DateInterval.Minute, CDate(lassdate), CDate(lactiondate))
                        End If

                        lsDurationDay = ""


                        If liDiff >= liYearMin Then
                            liYear = Math.Floor(liDiff / liYearMin)
                            liDiff = liDiff Mod liYearMin
                        End If

                        If liDiff >= liMonthMin Then
                            liMonth = Math.Floor(liDiff / liMonthMin)
                            liDiff = liDiff Mod liMonthMin

                        End If
                        If liDiff >= liDayMin Then
                            liDay = Math.Floor(liDiff / liDayMin)
                            liDiff = liDiff Mod liDayMin

                        End If
                        If liDiff >= liHrMIn Then
                            liHr = Math.Floor(liDiff / liHrMIn)
                            liDiff = liDiff Mod liHrMIn
                        End If
                        lsDurationDay = ""
                        If liYear > 1 Then
                            lsDurationDay = lsDurationDay & " " & liYear.ToString & " years "
                        ElseIf liYear = 1 Then
                            lsDurationDay = lsDurationDay & " " & liYear.ToString & " year "
                        End If
                        If liMonth > 1 Then
                            lsDurationDay = lsDurationDay & " " & liMonth.ToString & " months "
                        ElseIf liMonth = 1 Then
                            lsDurationDay = lsDurationDay & " " & liMonth.ToString & " month "
                        End If
                        If liDay > 1 Then
                            lsDurationDay = lsDurationDay & " " & liDay.ToString & " days "
                        ElseIf liDay = 1 Then
                            lsDurationDay = lsDurationDay & " " & liDay.ToString & " day "
                        End If
                        If liHr > 1 Then
                            lsDurationDay = lsDurationDay & " " & liHr.ToString & " hours "
                        ElseIf liHr = 1 Then
                            lsDurationDay = lsDurationDay & " " & liHr.ToString & " hour "
                        End If
                        If liDiff > 1 Then
                            lsDurationDay = lsDurationDay & " " & liDiff.ToString & " minutes "
                        ElseIf liDiff = 1 Then
                            lsDurationDay = lsDurationDay & " " & liDiff.ToString & " minute "
                        Else
                            If lsDurationDay = "" Then
                                lsDurationDay = "Less than a minute"
                            End If
                        End If
                        If liDiff = -1 Then
                            'lsDurationDay = "No Action Date" 
                            'changed it back to Assignment date the computation of Age as per Jewel -- may 28,2015
                            lsDurationDay = "No Assigned Date"
                        End If


                        loRow("Duration" & CStr(lctr)) = lsDurationDay '& " " & lsDurationHours
                    End If

                    If lctr = 3 Then
                        loData.Rows.Add(loRow)
                        loRow = loData.NewRow()
                        lctr = 1
                    Else
                        lctr += 1
                    End If

                Next
            End If


            If lctr = 2 Then
                loRow("profilePic" & CStr(lctr)) = ""
                loRow("userName" & CStr(lctr)) = ""
                loRow("Duration" & CStr(lctr)) = ""
                loRow("assignedDate" & CStr(lctr)) = ""
                loRow("actionDate" & CStr(lctr)) = ""
                loRow("description" & CStr(lctr)) = ""
                lctr += 1
            End If
            If lctr = 3 Then

                loRow("profilePic" & CStr(lctr)) = ""
                loRow("userName" & CStr(lctr)) = ""
                loRow("Duration" & CStr(lctr)) = ""
                loRow("assignedDate" & CStr(lctr)) = ""
                loRow("actionDate" & CStr(lctr)) = ""
                loRow("description" & CStr(lctr)) = ""
                loData.Rows.Add(loRow)

            End If

            rptTrackStatus.DataSource = loData
            rptTrackStatus.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If
            If Not loData Is Nothing Then
                loData.Dispose()
                loData = Nothing
            End If

        End Try

    End Sub

    Private Sub rptTrackStatus_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTrackStatus.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            'DirectCast(e.Item.FindControl("lHdr"), Literal).Text = pUserGroup.ToUpper
        ElseIf e.Item.ItemType = ListItemType.Item Then
            Dim ltl As Literal
            ltl = DirectCast(e.Item.FindControl("lName1"), Literal)

            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl1"), HtmlTable).Visible = False
            Else
                If pFirstLoad Then
                    DirectCast(e.Item.FindControl("imgIArrow3"), Image).Visible = True
                End If
            End If
            
            pFirstLoad = True

            ltl = DirectCast(e.Item.FindControl("lName2"), Literal)


            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl2"), HtmlTable).Visible = False
                DirectCast(e.Item.FindControl("imgIArrow1"), Image).Visible = False
            End If

            ltl = DirectCast(e.Item.FindControl("lName3"), Literal)


            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl3"), HtmlTable).Visible = False
                DirectCast(e.Item.FindControl("imgIArrow2"), Image).Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.AlternatingItem Then


            Dim ltl As Literal


            ltl = DirectCast(e.Item.FindControl("lName1"), Literal)

            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl1"), HtmlTable).Visible = False
            Else
                DirectCast(e.Item.FindControl("imgIArrow3"), Image).Visible = True
            End If

            ltl = DirectCast(e.Item.FindControl("lName2"), Literal)


            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl2"), HtmlTable).Visible = False
                DirectCast(e.Item.FindControl("imgIArrow1"), Image).Visible = False
            End If

            ltl = DirectCast(e.Item.FindControl("lName3"), Literal)


            If ltl.Text = "" Then
                DirectCast(e.Item.FindControl("tbl3"), HtmlTable).Visible = False
                DirectCast(e.Item.FindControl("imgIArrow2"), Image).Visible = False
            End If



        End If
    End Sub
End Class