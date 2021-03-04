Public Class ucTrackStatus
    Inherits System.Web.UI.UserControl

    Dim DocId As Integer
    
    

    Public Property pDocID As Integer
        Get
            Return DocId
        End Get
        Set(ByVal value As Integer)
            DocId = value
        End Set
    End Property

    


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'RetrieveAction(DocSession.sUserId)
            'RetrieveStatus()
        End If
    End Sub

    

    

    Public Sub RetrieveStatus()


        Dim ldata As DataTable
        Dim loData As DataTable
        Dim loRow As DataRow

        Try


            Dim odh As New DocHistory
            odh.pDocId = pDocId
            ldata = odh.GetTrackingStatus()

            loData = New DataTable("tblGroup")

            loData.Columns.Add("StartSeqNo1", Type.GetType("System.String"))
            loData.Columns.Add("EndSeqNo1", Type.GetType("System.String"))
            loData.Columns.Add("StartSeqNo2", Type.GetType("System.String"))
            loData.Columns.Add("EndSeqNo2", Type.GetType("System.String"))
            loData.Columns.Add("StartSeqNo3", Type.GetType("System.String"))
            loData.Columns.Add("EndSeqNo3", Type.GetType("System.String"))
            loData.Columns.Add("StartSeqNo4", Type.GetType("System.String"))
            loData.Columns.Add("EndSeqNo4", Type.GetType("System.String"))

            loData.Columns.Add("Group1", Type.GetType("System.String"))
            loData.Columns.Add("Group2", Type.GetType("System.String"))
            loData.Columns.Add("Group3", Type.GetType("System.String"))
            loData.Columns.Add("Group4", Type.GetType("System.String"))
            loData.Columns.Add("GroupName1", Type.GetType("System.String"))
            loData.Columns.Add("GroupName2", Type.GetType("System.String"))
            loData.Columns.Add("GroupName3", Type.GetType("System.String"))
            loData.Columns.Add("GroupName4", Type.GetType("System.String"))
            loData.Columns.Add("TrackingColor1", Type.GetType("System.String"))
            loData.Columns.Add("TrackingColor2", Type.GetType("System.String"))
            loData.Columns.Add("TrackingColor3", Type.GetType("System.String"))
            loData.Columns.Add("TrackingColor4", Type.GetType("System.String"))
            loData.Columns.Add("TextColor1", Type.GetType("System.String"))
            loData.Columns.Add("TextColor2", Type.GetType("System.String"))
            loData.Columns.Add("TextColor3", Type.GetType("System.String"))
            loData.Columns.Add("TextColor4", Type.GetType("System.String"))
            loData.Columns.Add("BorderColor1", Type.GetType("System.String"))
            loData.Columns.Add("BorderColor2", Type.GetType("System.String"))
            loData.Columns.Add("BorderColor3", Type.GetType("System.String"))
            loData.Columns.Add("BorderColor4", Type.GetType("System.String"))


            Dim lctr As Integer = 1
            Dim lastctr As Integer = 0
            loRow = loData.NewRow()
            loRow("Group1") = "Start"
            loRow("GroupName1") = "Start"
            loRow("StartSeqNo1") = ""
            loRow("EndSeqNo1") = ""
            loRow("TrackingColor1") = "#0033CC"
            loRow("TextColor1") = "#FFFFFF"
            loRow("BorderColor1") = "#CCCCCC"
            Dim valno As Integer = 1
            Dim lsGroup As String = ""
            Dim lsFGroup As String = ""
            Dim lsSeqNo As String = ""
            Dim lsFSeqNo As String = ""
            Dim lastSeqNo As String = ""
            Dim lsTrackColor As String = ""
            Dim lsTextColor As String = ""
            Dim lsFTrackColor As String = ""
            Dim lsGroupName As String = ""
            lctr += 1
            If ldata.Rows.Count > 0 Then
                lsGroup = ldata(0)("UserGroup")
                lsGroupName = ldata(0)("GroupName")
                lsSeqNo = ldata(0)("SeqNo")
                lsFSeqNo = ldata(0)("SeqNo")
                If IsDBNull(ldata(0)("trackingColor")) Then
                    lsTrackColor = ""
                Else
                    lsTrackColor = ldata(0)("trackingColor")
                End If
                If IsDBNull(ldata(0)("TextColor")) Then
                    lsTextColor = ""
                Else
                    lsTextColor = ldata(0)("TextColor")
                End If

                For Each lrow As DataRow In ldata.Rows
                    If lrow("UserGroup") <> lsGroup Then

                        loRow("StartSeqNo" & CStr(lctr)) = lsFSeqNo
                        loRow("EndSeqNo" & CStr(lctr)) = lsSeqNo

                        loRow("Group" & CStr(lctr)) = lsGroup 'lrow("UserGroup")
                        loRow("GroupName" & CStr(lctr)) = lsGroupName 'lrow("GroupName")
                        loRow("TrackingColor" & CStr(lctr)) = IIf(lsTrackColor = "", "#CCCCCC", "#" & lsTrackColor)
                        loRow("TextColor" & CStr(lctr)) = IIf(lsTextColor = "", "#000000", "#" & lsTextColor) 'IIf(IsDBNull(lrow("TextColor")) OrElse lrow("TextColor") = "", "#FFFFFF", "#" & lrow("TextColor"))
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        'lsGroup = lrow("UserGroup")

                        If lctr = 4 AndAlso valno <> -1 Then

                            loData.Rows.Add(loRow)
                            loRow = loData.NewRow()
                            lctr = 4
                            valno = -1 'decrease counter
                        ElseIf lctr = 1 AndAlso valno <> 1 Then
                            If lastctr > 0 Then
                                loRow("EndSeqNo" & CStr(lastctr)) = lastSeqNo
                            End If
                            loData.Rows.Add(loRow)
                            loRow = loData.NewRow()
                            lctr = 1
                            valno = 1 'increase counter
                        Else
                            lctr += valno
                        End If

                        'first sequence per row
                        lsFSeqNo = lrow("SeqNo")
                   
                    End If

                    lsGroup = lrow("UserGroup")
                    lsSeqNo = lrow("SeqNo")

                    If IsDBNull(lrow("trackingColor")) Then
                        lsTrackColor = ""
                    Else
                        lsTrackColor = lrow("trackingColor")
                    End If
                    If IsDBNull(lrow("TextColor")) Then
                        lsTextColor = ""
                    Else
                        lsTextColor = lrow("TextColor")
                    End If

                    lsGroupName = lrow("GroupName")

                Next
            End If

            loRow("StartSeqNo" & CStr(lctr)) = lsFSeqNo
            loRow("EndSeqNo" & CStr(lctr)) = lsSeqNo
            loRow("Group" & CStr(lctr)) = lsGroup
            loRow("GroupName" & CStr(lctr)) = lsGroupName
            loRow("TrackingColor" & CStr(lctr)) = IIf(lsTrackColor = "", "#CCCCCC", "#" & lsTrackColor)
            loRow("TextColor" & CStr(lctr)) = IIf(lsTextColor = "", "#000000", "#" & lsTextColor)
            loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
            
            'loRow("StartSeqNo" & CStr(lctr)) = ""
            'loRow("EndSeqNo" & CStr(lctr)) = ""
            'loRow("Group" & CStr(lctr)) = "End"
            'loRow("GroupName" & CStr(lctr)) = "End"
            'loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
            'loRow("TextColor" & CStr(lctr)) = "#FFFFFF"

            If (lctr = 4 AndAlso valno <> -1) OrElse (lctr = 1 AndAlso valno <> 1) Then

                valno = valno * -1
                loData.Rows.Add(loRow)
                loRow = loData.NewRow()
                loRow("StartSeqNo" & CStr(lctr)) = ""
                loRow("EndSeqNo" & CStr(lctr)) = ""
                loRow("Group" & CStr(lctr)) = "End"
                loRow("GroupName" & CStr(lctr)) = "End"
                loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                lctr += valno
                loRow("StartSeqNo" & CStr(lctr)) = ""
                loRow("EndSeqNo" & CStr(lctr)) = ""
                loRow("Group" & CStr(lctr)) = ""
                loRow("GroupName" & CStr(lctr)) = ""
                loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                loRow("TextColor" & CStr(lctr)) = "Transparent"
                loRow("BorderColor" & CStr(lctr)) = "Transparent"
                lctr += valno
                loRow("StartSeqNo" & CStr(lctr)) = ""
                loRow("EndSeqNo" & CStr(lctr)) = ""
                loRow("Group" & CStr(lctr)) = ""
                loRow("GroupName" & CStr(lctr)) = ""
                loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                loRow("TextColor" & CStr(lctr)) = "Transparent"
                loRow("BorderColor" & CStr(lctr)) = "Transparent"
                lctr += valno
                loRow("StartSeqNo" & CStr(lctr)) = ""
                loRow("EndSeqNo" & CStr(lctr)) = ""
                loRow("Group" & CStr(lctr)) = ""
                loRow("GroupName" & CStr(lctr)) = ""
                loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                loRow("TextColor" & CStr(lctr)) = "Transparent"
                loRow("BorderColor" & CStr(lctr)) = "Transparent"
                loData.Rows.Add(loRow)
            Else



                lctr += valno
                If valno = 1 Then

                    If lctr = 2 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        loData.Rows.Add(loRow)
                    ElseIf lctr = 3 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        loData.Rows.Add(loRow)
                    ElseIf lctr = 4 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        loData.Rows.Add(loRow)
                    End If
                Else
                    If lctr = 3 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        loData.Rows.Add(loRow)
                    ElseIf lctr = 2 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        lctr += valno
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = ""
                        loRow("GroupName" & CStr(lctr)) = ""
                        loRow("TrackingColor" & CStr(lctr)) = "Transparent"
                        loRow("TextColor" & CStr(lctr)) = "Transparent"
                        loRow("BorderColor" & CStr(lctr)) = "Transparent"
                        loData.Rows.Add(loRow)
                    ElseIf lctr = 1 Then
                        loRow("StartSeqNo" & CStr(lctr)) = ""
                        loRow("EndSeqNo" & CStr(lctr)) = ""
                        loRow("Group" & CStr(lctr)) = "End"
                        loRow("GroupName" & CStr(lctr)) = "End"
                        loRow("TrackingColor" & CStr(lctr)) = "#0033CC"
                        loRow("TextColor" & CStr(lctr)) = "#FFFFFF"
                        loRow("BorderColor" & CStr(lctr)) = "#CCCCCC"
                        loData.Rows.Add(loRow)
                    End If
                End If

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

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lcell As HtmlTableCell
            Dim lbtn As LinkButton
            Dim lImg As Image
            'lcell = DirectCast(e.Item.FindControl("td1"), HtmlTableCell)
            lbtn = DirectCast(e.Item.FindControl("lIGroup1"), LinkButton)
            lImg = DirectCast(e.Item.FindControl("imgIArrow1"), Image)

            If lbtn.Text = "Start" Then
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text = "End" Then
                lImg.Visible = False
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text.Trim = "" Then
                lImg.Visible = False
                'lcell.Style.Clear()
            End If

            'lcell = DirectCast(e.Item.FindControl("td2"), HtmlTableCell)
            lbtn = DirectCast(e.Item.FindControl("lIGroup2"), LinkButton)
            lImg = DirectCast(e.Item.FindControl("imgIArrow2"), Image)

            If lbtn.Text = "Start" Then
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text = "End" Then
                lImg.Visible = False
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text.Trim = "" Then
                lImg.Visible = False
                'lcell.Style.Clear()
            End If

            'lcell = DirectCast(e.Item.FindControl("td3"), HtmlTableCell)
            lbtn = DirectCast(e.Item.FindControl("lIGroup3"), LinkButton)
            lImg = DirectCast(e.Item.FindControl("imgIArrow3"), Image)

            If lbtn.Text = "Start" Then
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text = "End" Then
                lImg.Visible = False
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text.Trim = "" Then
                lImg.Visible = False
                'lcell.Style.Clear()
            End If

            'lcell = DirectCast(e.Item.FindControl("td4"), HtmlTableCell)
            lbtn = DirectCast(e.Item.FindControl("lIGroup4"), LinkButton)
            lImg = DirectCast(e.Item.FindControl("imgIArrow4"), Image)

            If lbtn.Text = "Start" Then
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text = "End" Then
                lImg.Visible = False
                lbtn.Enabled = False
                lbtn.ForeColor = Drawing.Color.White
            ElseIf lbtn.Text.Trim = "" Then
                lImg.Visible = False
                'lcell.Style.Clear()
            End If



        End If
    End Sub
    
    Public Sub ShowUsers(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lImg As LinkButton = DirectCast(sender, LinkButton)
        Dim lSuffix As String = Right(lImg.ID, 1)
        ucSTU.pDocID = DocSession.sDocID
        ucSTU.pUserGroup = lImg.ValidationGroup
        Dim rpti As RepeaterItem = DirectCast(lImg.NamingContainer, RepeaterItem)
        Dim lSSNo As String = DirectCast(rpti.FindControl("lSSeqNo" & lSuffix), Literal).Text
        Dim lESNo As String = DirectCast(rpti.FindControl("lESeqNo" & lSuffix), Literal).Text
        ucSTU.RetrieveStatus(lImg.Text, lSSNo, lESNo)
        ucSTU.Visible = True
    End Sub
End Class