Imports System
Imports System.Data.SqlClient
Public Class UserControlWorkSched
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property AlwaysAllowed() As Boolean
        Set(ByVal value As Boolean)
            ucAlwaysAllowed.BoxCheck = True
        End Set
        Get
            Return ucAlwaysAllowed.BoxCheck
        End Get
    End Property

    Public Property GroupId() As String
        Set(ByVal value As String)
            lGroupId.Text = value
        End Set
        Get
            Return lGroupId.Text
        End Get
    End Property


    Public Sub RetrieveData()
        Dim oGroup As New DocGroup
        Dim lodata As DataTable
        Try


            oGroup.pGroupID = GroupId
            lodata = oGroup.RetrieveWorkSchedule()
            rptWeekdays.DataSource = lodata
            rptWeekdays.DataBind()
        Catch ex As Exception

        Finally
            If Not lodata Is Nothing Then
                lodata.Dispose()
                lodata = Nothing
            End If
        End Try

        'pnlA.Update()


    End Sub

    Protected Sub enableTime(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oImg As ImageButton = DirectCast(sender, ImageButton)

        Dim oItem As RepeaterItem = DirectCast(oImg.NamingContainer.NamingContainer, RepeaterItem)

        Dim ucbx As UserControlCheckBox = DirectCast(oItem.FindControl("ucCheck"), UserControlCheckBox)

        DirectCast(oItem.FindControl("txtStart"), TextBox).Enabled = ucbx.BoxCheck
        DirectCast(oItem.FindControl("txtEnd"), TextBox).Enabled = ucbx.BoxCheck

    End Sub

    Private Sub rptWeekdays_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptWeekdays.ItemDataBound
        If Not ucAlwaysAllowed.BoxCheck Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

                If DirectCast(e.Item.FindControl("lchk"), Literal).Text.Trim = "1" Then
                    DirectCast(e.Item.FindControl("ucCheck"), UserControlCheckBox).Visible = True
                    DirectCast(e.Item.FindControl("ucCheck"), UserControlCheckBox).BoxCheck = True
                    DirectCast(e.Item.FindControl("txtStart"), TextBox).Enabled = True
                    DirectCast(e.Item.FindControl("txtEnd"), TextBox).Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub DisplayWorkSchedule(ByVal bEnabled As Boolean)
        For Each ri In rptWeekdays.Items
            If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                DirectCast(ri.FindControl("ucCheck"), UserControlCheckBox).Visible = bEnabled
                DirectCast(ri.FindControl("txtStart"), TextBox).Enabled = DirectCast(ri.FindControl("ucCheck"), UserControlCheckBox).BoxCheck
                DirectCast(ri.FindControl("txtEnd"), TextBox).Enabled = DirectCast(ri.FindControl("ucCheck"), UserControlCheckBox).BoxCheck
            End If
        Next
    End Sub

    Private Sub ucAlwaysAllowed_e_check() Handles ucAlwaysAllowed.e_check

        DisplayWorkSchedule(Not ucAlwaysAllowed.BoxCheck)

    End Sub

    Private Sub imgSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSave.Click
        SaveWorkSchedule()
    End Sub
    Private Function WorkScheduleInsert(ByVal pGroupId As String, ByVal pWeekday As String, ByVal pstarttime As String, ByVal pendtime As String, ByVal pcreatedby As String) As String
        Return "INSERT INTO WorkSchedule " & _
          "(GroupId " & _
          ",Weekday " & _
          ",StartTime " & _
          ",EndTime " & _
          ",CreatedBy " & _
          ",CreatedDate) " & _
    "VALUES " & _
          "(" & pGroupId & " " & _
          "," & pWeekday & " " & _
          ",'" & pstarttime & "' " & _
          ",'" & pendtime & "' " & _
          ",'" & pcreatedby & "' " & _
          "," & IIf(DocSession.OraClient, "SYSDATE", "GETDATE()") & ") "
    End Function
    Private Sub SaveWorkSchedule()
        

        Dim objCommand As clsSqlConn
        'Dim ltr As SqlTransaction
        Dim ltr As DbTran
        Dim lbCommit As Boolean
        lbCommit = False

        Try

            updateAlwaysAllowed()

            If Not ucAlwaysAllowed.BoxCheck Then
                ltr = New DbTran
                objCommand = New clsSqlConn(ltr.pTran)

                objCommand.CommandType = CommandType.StoredProcedure


                For Each ri In rptWeekdays.Items
                    If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                        If DirectCast(ri.FindControl("ucCheck"), UserControlCheckBox).BoxCheck Then
                            objCommand.CommandText = WorkScheduleInsert(GroupId, DirectCast(ri.FindControl("lwd"), Literal).Text, DirectCast(ri.FindControl("txtStart"), TextBox).Text _
                            , DirectCast(ri.FindControl("txtEnd"), TextBox).Text, DocSession.sUserId) '"xMSP_WORKSCHEDULEINSERT"

                            objCommand.ExecTranNonQuery()
                            lbCommit = True
                        End If

                    End If
                Next

                If lbCommit Then
                    ltr.pTran.Commit()
                    msg.CssClass = "msg_green"
                    msg.Text = "** Work schedule has been created successfully."
                Else
                    ltr.pTran.Rollback()
                End If

            End If

            
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            If Not ltr Is Nothing Then
                ltr.pTran.Rollback()
            End If
            msg.CssClass = "msg_red"
            msg.Text = "** Error while saving (" & ex.Message & "). Please try again"

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

    Private Sub updateAlwaysAllowed()
        

        Dim objCommand As clsSqlConn

        Try
            Dim s_allow As String = IIf(ucAlwaysAllowed.BoxCheck, "1", "0")
            Dim s_sql As String = "UPDATE Groups " & _
   "SET " & _
      "AlwaysAllowed = " & s_allow & " " & _
 "WHERE GroupId = '" & GroupId & "' " & _
"DELETE FROM WorkSchedule WHERE GroupId = '" & GroupId & "'"

            objCommand = New clsSqlConn
            objCommand.CommandType = CommandType.StoredProcedure

            objCommand.CommandText = s_sql '"xMSP_ALWAYSALLOWED"
            'objCommand.ParametersAddWithValue("@GroupId", GroupId)
            'objCommand.ParametersAddWithValue("@AlwaysAllowed", ucAlwaysAllowed.BoxCheck)

            objCommand.ExecNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If


        End Try
    End Sub

End Class