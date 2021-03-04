Imports System
Imports System.Data.SqlClient

Public Class backupdb
    Inherits System.Web.UI.Page

    Private Sub backupdb_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AddHandler ucPager.eGreaterClick, AddressOf imgGreater_Click
        AddHandler ucPager.eLessClick, AddressOf imgLess_Click
        AddHandler ucPager.eFirstClick, AddressOf imgFirst_Click
        AddHandler ucPager.eLastClick, AddressOf imgLast_Click
        ucMenu.SelectTab("Backup")
        Master.SelectTab("Account")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DocSession.IsAdmin()
        If Not IsPostBack Then
            tbLocation.Text = DocSession.BackupLoc
            RetrieveBackup()
        End If
    End Sub

    Public Sub sortColumnHeader(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbSort As LinkButton = DirectCast(sender, LinkButton)

        Dim img As Image
        hfCurrent.Value = "1"
        If imgSort1.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort1
            imgSort1.Visible = True
        Else
            imgSort1.Visible = False
        End If
        If imgSort2.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort2
            imgSort2.Visible = True
        Else
            imgSort2.Visible = False

        End If
        If imgSort3.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort3
            imgSort3.Visible = True
        Else
            imgSort3.Visible = False

        End If
        If imgSort4.ID = "imgSort" & Right(lbSort.ID, 1) Then
            img = imgSort4
            imgSort4.Visible = True
        Else
            imgSort4.Visible = False

        End If


        Dim oDocList As DocBackup
        Dim ldata As DataTable


        Try
            oDocList = New DocBackup
            If img.Visible Then
                If img.ImageUrl.ToLower = "images/asc.png" Then
                    img.ImageUrl = "images/desc.png"
                    oDocList.pSortOrder = "Desc"
                Else
                    img.ImageUrl = "images/asc.png"
                    oDocList.pSortOrder = "Asc"
                End If
            Else
                img.ImageUrl = "images/asc.png"
                oDocList.pSortOrder = "Asc"
                img.Visible = True
            End If

            oDocList.pIdx = hfCurrent.Value
            oDocList.pRowsPerPage = DocSession.RowsPerPage
            oDocList.pSortCol = lbSort.Text
            oDocList.pUserId = DocSession.sUserId
            ldata = oDocList.RetrieveBackup
            If ldata.Rows.Count > DocSession.RowsPerPage Then
                ucPager.pImgGreater = True
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            Else
                ucPager.pImgGreater = False
            End If

            If CInt(hfCurrent.Value) > 1 Then
                ucPager.pImgLess = True
            Else
                ucPager.pImgLess = False
            End If

            Repeater1.DataSource = ldata
            Repeater1.DataBind()
        Catch ex As Exception
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing

            End If
        End Try


    End Sub

    Private Sub btBackup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBackup.Click
        If tbFileName.Text.Trim = "" Then
            'msg.CssClass = "msg_red"
            Master.ShowMessage("File name is required.")
            tbFileName.Focus()

            Exit Sub
        End If
        If FileNameDoesNotExist() Then


            'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
            Dim dbname As String = System.Configuration.ConfigurationManager.AppSettings("dbname")
            'Dim oConn As New SqlConnection(str)

            Dim objCommand As clsSqlConn

            Try
                objCommand = New clsSqlConn
                objCommand.pCommandType = CommandType.StoredProcedure

                objCommand.pCommandText = "DMSP_BACKUPDB"
                objCommand.ParametersAddWithValue("@DbBackupFilename", tbFileName.Text)
                objCommand.ParametersAddWithValue("@dbname", dbname)
                objCommand.ParametersAddWithValue("@BackupBy", DocSession.sUserId)
                objCommand.ParametersAddWithValue("@BackupLocation", tbLocation.Text)

                objCommand.ExecNonQuery()
                'msg.CssClass = "msg_green"
                Master.ShowMessage("Backup has been created successfully.")
                RetrieveBackup()

            Catch ex As Exception
                'msg.CssClass = "msg_green"
                Master.ShowMessage("An error occurred while creating backup ( " & ex.Message & " )")
            Finally

                If Not objCommand Is Nothing Then
                    objCommand.Dispose()
                    objCommand = Nothing
                End If
                

            End Try
        End If
    End Sub

    Private Sub RetrieveBackup()

        Dim oDocList As DocBackup
        Dim ldata As DataTable
        
        Try
            oDocList = New DocBackup
            oDocList.pIdx = hfCurrent.Value
            oDocList.pRowsPerPage = DocSession.RowsPerPage
            oDocList.pSortCol = "Backup Date"
            oDocList.pUserId = DocSession.sUserId
            oDocList.pSortOrder = "Desc"

            ldata = oDocList.RetrieveBackup

            If ldata.Rows.Count > DocSession.RowsPerPage Then
                'ucPager.pImgGreater = True
                ldata.Rows.RemoveAt(DocSession.RowsPerPage)
            Else
                'ucPager.pImgGreater = False
            End If

            'If CInt(hfCurrent.Value) > 1 Then
            '    ucPager.pImgLess = True
            'Else
            '    ucPager.pImgLess = False
            'End If




            If ldata.Rows.Count <= 0 Then
                Master.ShowMessage("No records found.")
                'lmsg.Visible = True
                'lmsg.CssClass = "msg_red"
            Else
                'lmsg.Visible = False
                hfTotalRows.Value = CStr(oDocList.pReturnVal)
                ucPager.EnableButtons(CInt(hfCurrent.Value), CInt(oDocList.pReturnVal))

                'ucPager.pRecordCount = "Total record(s): " & CStr(oDocList.pReturnVal)
                'If (CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) > CInt(oDocList.pReturnVal) Then
                '    ucPager.pPageCount = "Row " & hfCurrent.Value & " -  " & oDocList.pReturnVal & " of " & oDocList.pReturnVal
                'Else
                '    ucPager.pPageCount = "Row " & hfCurrent.Value & " -  " & CStr(CInt(hfCurrent.Value) + CInt(DocSession.RowsPerPage) - 1) & " of " & oDocList.pReturnVal
                'End If
            End If

            Repeater1.DataSource = ldata
            Repeater1.DataBind()



        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If


        End Try

    End Sub

    Private Function FileNameDoesNotExist() As Boolean
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        Dim objCommand As clsSqlConn
        Dim lbrow As Boolean

        Try
            Dim s_sql As String = " SELECT DBBACKUPFILENAME,BACKUPDATE FROM BackupDB Bd " & _
 " WHERE (bd.DBBACKUPFILENAME = '" & tbFileName.Text & "') "
            objCommand = New clsSqlConn
            objCommand.pCommandType = CommandType.StoredProcedure

            objCommand.pCommandText = s_sql '"xMSP_FILENAMEVALIDATE"
            'objCommand.ParametersAddWithValue("@FileName", tbFileName.Text)


            lbrow = objCommand.ExecHasRow()

            If lbrow Then
                'msg.CssClass = "msg_red"
                Master.ShowMessage("Backup file name " & tbFileName.Text & " already exist. Please try another file name.")
                Return False
            Else
                Return True
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

    Private Sub imgLess_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) - DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetrieveBackup()

    End Sub

    Private Sub imgGreater_Click()
        Dim lIdx As Integer
        lIdx = CInt(hfCurrent.Value) + DocSession.RowsPerPage
        hfCurrent.Value = CStr(lIdx)
        RetrieveBackup()


    End Sub

    Private Sub imgFirst_Click()
        Dim lIdx As Integer
        lIdx = 1
        hfCurrent.Value = CStr(lIdx)
        RetrieveBackup()

    End Sub

    Private Sub imgLast_Click()
        Dim lIdx As Integer
        If CInt(hfTotalRows.Value) Mod DocSession.RowsPerPage > 0 Then
            lIdx = Math.Abs(CInt(hfTotalRows.Value) / DocSession.RowsPerPage) * CInt(DocSession.RowsPerPage) + 1
        Else
            lIdx = (Math.Abs(CInt(hfTotalRows.Value) / DocSession.RowsPerPage) - 1) * CInt(DocSession.RowsPerPage) + 1
        End If
        hfCurrent.Value = CStr(lIdx)
        RetrieveBackup()


    End Sub

End Class