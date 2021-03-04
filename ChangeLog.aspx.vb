Public Class ChangeLog
    Inherits System.Web.UI.Page

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Master.SelectTab("Account")
        ucMenu.SelectTab("Changelog")
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
    End Sub
#Region "pager section"
    'pager: step 3
    Public Sub RetAction()
        'DocSession.doc_DocCurrentPage = hfCurrent.Value
        RetrieveRecords()
        pnlRepeater.Update()
        'ucDB.pIdx = CInt(hfCurrent.Value)
        'ucDB.RetrieveAction(DocSession.sUserId)
        'ucPager.EnableButtons(CInt(hfCurrent.Value), ucDB.pRetVal)
        'hfTotalRows.Value = CStr(ucDB.pRetVal)
    End Sub
    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()



    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetAction()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = (CInt(hfTotalRows.Value) - (CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage)) + 1
        Else
            lIdx = (CInt(hfTotalRows.Value) - DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetAction()


    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRetrieve.Click
        If tbProcessDate.Text = "" OrElse IsDate(tbProcessDate.Text) Then
            RetrieveRecords()
        Else
            Master.ShowMessage("Please enter a valid date for Process date.")
        End If

    End Sub


    Private Sub RetrieveRecords()

        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlClient.SqlConnection(str)
        Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlClient.SqlDataAdapter
        Dim ldata As DataTable
        Dim s_sql_count As String = " SELECT count(recordid) " & _
                                        "FROM DataChanges DC "
        If tbUserName.Text <> "" Then
            s_sql_count = s_sql_count & "LEFT JOIN users u ON dc.modifiedby = u.userid "
        End If

        s_sql_count = s_sql_count & "WHERE tableName = '" & dlTables.SelectedValue & "' "



        Dim s_sql As String = ""
        Dim lbOra As Boolean = DocSession.OraClient

        If lbOra Then
            If tbProcessDate.Text <> "" Then
                s_sql_count = s_sql_count & " and ModifiedDate >= TO_DATE('" & Replace(tbProcessDate.Text, "'", "''") & "','mm/dd/yyyy') "
            End If

            If tbUserName.Text <> "" Then
                s_sql_count = s_sql_count & " and (lower(NVL(u.FirstName,'')) || ' ' || lower(NVL(u.LastName,'')) like '%" & Replace(tbUserName.Text.Trim, "'", "''") & "%' "
            End If

            s_sql = s_sql & " SELECT dt.*,(NVL(u.FirstName,'') || ' ' || NVL(u.LastName,'')) AS username FROM " & _
                "( SELECT  " & _
                    "(row_number() over (order by modifieddate desc)) AS rn " & _
                    ",tableName " & _
                  ",RecordId " & _
                  ",dc.ModifiedBy " & _
                  ",ColumnName " & _
                  ",ModifiedDate " & _
                  ",NVL(OldValue,'') as OldValue " & _
                  ",NewValue "
            s_sql = s_sql & ",NVL(IPAddress,'') AS IPAddress "

            s_sql = s_sql & "FROM DataChanges DC "

            If tbUserName.Text <> "" Then
                s_sql = s_sql & "LEFT JOIN users u ON dc.modifiedby = u.userid "
            End If

            s_sql = s_sql & "WHERE rownum <= " & CStr(CInt(hfCurrent.Value) * CInt(DocSession.RowsPerPage)) & " and tableName = '" & dlTables.SelectedValue & "'  "
            If tbProcessDate.Text <> "" Then
                s_sql = s_sql & " and ModifiedDate >= TO_DATE('" & Replace(tbProcessDate.Text, "'", "''") & "','mm/dd/yyyy') "
            End If

            If tbUserName.Text <> "" Then
                s_sql = s_sql & " and lower(NVL(u.FirstName,'')) || ' ' || lower(NVL(u.LastName,'')) like '%" & Replace(tbUserName.Text.Trim, "'", "''") & "%' "
            End If
            s_sql = s_sql & " ) dt " & _
                "LEFT JOIN USERS u " & _
                "ON dt.modifiedby = u.userid " & _
    " WHERE (rn between " & hfCurrent.Value & " and (" & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage)) & "-1))" & _
            " order by modifieddate desc "
        Else

            If tbProcessDate.Text <> "" Then
                s_sql_count = s_sql_count & " and ModifiedDate >= '" & Replace(tbProcessDate.Text, "'", "''") & "' "
            End If

            If tbUserName.Text <> "" Then
                s_sql_count = s_sql_count & " and u.FirstName+' '+u.LastName like '%" & Replace(tbUserName.Text.Trim, "'", "''") & "%' "
            End If

            s_sql = s_sql & " SELECT dt.*,(u.FirstName + ' ' + u.LastName) AS username FROM " & _
                "( SELECT top " & CStr(CInt(hfCurrent.Value) * CInt(DocSession.RowsPerPage)) & " " & _
              "(row_number() over (order by modifieddate desc)) AS rn " & _
              ",tableName " & _
                  ",RecordId " & _
                  ",dc.ModifiedBy " & _
                  ",ColumnName " & _
                  ",ModifiedDate " & _
                  ",OldValue = isnull(OldValue,'') " & _
                  ",NewValue "
            s_sql = s_sql & ",ISNULL(IPAddress,'') AS IPAddress "

            s_sql = s_sql & "FROM DataChanges DC "

            If tbUserName.Text <> "" Then
                s_sql = s_sql & "LEFT JOIN users u ON dc.modifiedby = u.userid "
            End If

            s_sql = s_sql & "WHERE tableName = '" & dlTables.SelectedValue & "'  "
            If tbProcessDate.Text <> "" Then
                s_sql = s_sql & " and ModifiedDate >= '" & Replace(tbProcessDate.Text, "'", "''") & "' "
            End If

            If tbUserName.Text <> "" Then
                s_sql = s_sql & " and u.FirstName+' '+u.LastName like '%" & Replace(tbUserName.Text.Trim, "'", "''") & "%' "
            End If
            s_sql = s_sql & ") dt " & _
    "LEFT JOIN USERS u " & _
    "ON dt.modifiedby = u.userid " & _
    " WHERE (rn between " & hfCurrent.Value & " and (" & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage)) & "-1))"
        End If

        

        Try
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.Text
            objCommand.pCommandText = s_sql_count
            hfTotalRows.Value = CStr(objCommand.ExecScalar2) 'oDocs.pRetVal

            objCommand.pCommandText = s_sql
            ldata = objCommand.ExecData

            'Dim retval As SqlClient.SqlParameter = objCommand.RetValue
            'Dim retval As OracleClient.OracleParameter = objCommand.RetValue
            'Dim retval As New DbParam
            'retval.pParam = objCommand.RetValue

            If ldata.Rows.Count > 0 Then
                


                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(hfTotalRows.Value))

            Else
                Master.ShowMessage("No records found for the selected criteria.")
            End If

            'If CInt(hfCurrent.Value) > 1 Then
            '    imgLess.Visible = True
            '    imgLessD.Visible = False
            'Else
            '    imgLess.Visible = False
            '    imgLessD.Visible = True
            'End If

            'If ldata.Rows.Count <= 0 Then
            '    Master.ShowMessage("No records found.")
            '    'lmsg.Visible = True
            '    lPageCount.Visible = False
            'Else
            '    'lmsg.Visible = False
            '    lPageCount.Visible = True
            '    lRecordCount.Text = "Total No of records retrieved: " & CStr(retv)
            '    If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > retv Then
            '        lPageCount.Text = "Rows " & hfCurrent.Value & " -  " & CStr(retv) & " of " & CStr(retv)
            '    Else
            '        lPageCount.Text = "Rows " & hfCurrent.Value & " -  " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) & " of " & CStr(retv)
            '    End If
            'End If

            Repeater1.Visible = True
            'lRecordCount.Visible = True
            'lPageCount.Visible = True

            Repeater1.DataSource = ldata
            Repeater1.DataBind()
            pPager.Update()
            'lNoOfRecord.Visible = True
            'lNo.Visible = True
            'If ldata.Rows.Count = 0 Then
            '    lMsg.Visible = True
            'Else
            '    lMsg.Visible = False
            'End If

            'idSrchRslt.Visible = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

            If Not objCommand Is Nothing Then
                objCommand.Dispose()
                objCommand = Nothing
            End If

        End Try

    End Sub

    Private Sub Repeater1_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgU As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)


        End If
    End Sub

    Private Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lblDocName As ImageButton = DirectCast(e.Item.FindControl("imgUpd"), ImageButton)
            'Dim lblActive As Literal = DirectCast(e.Item.FindControl("lFileName"), Literal)
            'Dim lext As String = DocTypeLogo.DocTypeBitMap(Right(lblActive.Text.Trim, 4))
            'lblDocName.ImageUrl = lext
        End If
    End Sub





    



End Class