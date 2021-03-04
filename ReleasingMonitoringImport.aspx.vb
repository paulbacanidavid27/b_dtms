Public Class ReleasingMonitoringImport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            loadStatus()

        End If
    End Sub
    Private Sub loadStatus()
        Dim oDoc As New DocTypes


        Dim ldata As DataTable = oDoc.GetDocStatus

        dlStatus.DataTextField = "description"
        dlStatus.DataValueField = "statusid"
        dlStatus.DataSource = ldata
        dlStatus.SelectedValue = "2"
        dlStatus.DataBind()
    End Sub

    Private Sub btUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUpload.Click
        If uploadFileName.HasFile Then
            Dim linfo As New System.IO.FileInfo(uploadFileName.PostedFile.FileName)
            If uploadFileName.PostedFile.ContentLength > DocSession.MaxFileSize Then
                Master.ShowMessage("** File size too big. Only a maximum of " + CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) + "MB is allowed.")
                Exit Sub
            End If

        End If

        Dim ls_fileextension As String = System.IO.Path.GetExtension(uploadFileName.FileName).ToLower()
        If Not ls_fileextension = ".csv" Then
            Master.ShowMessage("Invalid file type. Only CSV is allowed.")
            Exit Sub
        End If
        'If Not IsNumeric(tbFileNameColNumber.Text) Then
        '    Master.ShowMessage("Specify column position of the file name in the import file.")
        '    Exit Sub
        'End If
        Try

            lImportFileName.Text = uploadFileName.FileName
            Using ldata As DataTable = uf_read_csv_dtl()
                'If ldata.Rows.Count > 200 Then
                'msg.Text = "Please import not more than 200 records to avoid performance issue."
                If ldata.Rows.Count = 0 Then
                    Master.ShowMessage("Import file is empty. Please load another file.")

                    'imgImport.Visible = False
                    btImport.Visible = False
                    step5.Visible = False
                    'btUpload.Visible = True
                    btUpload.Visible = True
                    btCancel.Visible = False
                Else

                    'btUpload.Visible = False
                    'step 1
                    'ldata.DefaultView.Sort = "col0 asc"

                    Dim loOffice As New DocOffice
                    'Dim lsOfc As String
                    Dim lodata As DataTable = loOffice.RetrieveEquivalentOfficeCode("")
                    Dim drow() As DataRow
                    For Each drows As DataRow In ldata.Rows
                        If drows(5).ToString <> "" Then
                            drow = lodata.Select("OfficeCode = '" & drows(5) & "' or Description = '" & drows(5) & "'")
                            If drow.Length > 0 Then
                                drows(6) = drow(0).Item(0)
                            End If

                        End If

                    Next
                    
                    lRecordCount.Text = "No of rows to import: " & ldata.Rows.Count.ToString
                    rptUpload.Visible = True
                    

                    rptUpload.DataSource = ldata

                    rptUpload.DataBind()
                    
                    btImport.Visible = True
                    btUpload.Visible = False
                    btCancel.Visible = True
                    step5.Visible = True
                    'pnlDocType.Update()
                End If
                pnlMsg.Update()
                'pnlImp.Update()

            End Using
        Catch ex As Exception
            Master.ShowMessage(ex.Message)
            btUpload.Visible = True
            step5.Visible = False
            btCancel.Visible = False

        End Try
    End Sub

    Private Function uf_read_csv_dtl() As DataTable
        Dim lo_rows As String()
        Dim li_row_index, li_tbl_count, li_tbl0_end_ind, li_start_ind As Integer
        Dim ls_file_content As String

        Dim ldr As DataRow

        ls_file_content = uf_get_file_content(uploadFileName.FileContent)

        lo_rows = Split(ls_file_content, vbCrLf)
        li_tbl_count = uf_get_table_count(lo_rows, li_tbl0_end_ind)

        If li_tbl_count > 1 Then
            li_start_ind = li_tbl0_end_ind + 1
        Else
            li_start_ind = 0
        End If

        Using ldt As New DataTable
            If (lo_rows.Length - li_start_ind) > 0 Then
                uf_create_table_columns(uf_get_column_count(lo_rows(li_start_ind)), ldt)

                '  For li_row_index = li_start_ind + 1 To lo_rows.Length - 1 'with header
                For li_row_index = li_start_ind To lo_rows.Length - 1  'no header
                    lo_rows(li_row_index) = lo_rows(li_row_index).Trim

                    If lo_rows(li_row_index).Length > 0 Then
                        ldr = ldt.NewRow
                        uf_append_row(lo_rows(li_row_index), ldr)

                        ldt.Rows.Add(ldr)
                    End If
                Next
            End If
            Return ldt
        End Using
    End Function

    Private Function uf_filter_values(ByVal as_word As String) As String
        Dim ls_qoute As String = """"
        If as_word <> """""" Then
            as_word = as_word.Replace("""""", ls_qoute)
        End If

        If as_word.StartsWith(ls_qoute) And as_word.EndsWith(ls_qoute) Then
            as_word = as_word.Remove(0, 1)
            as_word = as_word.Remove(as_word.Length - 1, 1)
        End If

        Return as_word
    End Function

    Private Sub uf_append_row(ByVal as_line As String, ByVal adr_row As DataRow)
        Dim ls_comma As String = ","
        Dim ls_qoute As String = """"

        Dim li_ind As Integer
        Dim ls_curr_char, ls_word As String
        Dim li_col_count As Integer = 0
        Dim li_qoute_count As Integer = 0
        Dim li_start As Integer = 0

        For li_ind = 0 To as_line.Length - 1
            ls_curr_char = as_line(li_ind)

            If ls_curr_char = ls_comma And (li_qoute_count Mod 2) = 0 Then
                ls_word = as_line.Substring(li_start, li_ind - li_start)
                If li_col_count < 0 Then
                    adr_row("Document_Type") = uf_filter_values(ls_word)
                Else
                    adr_row("col" & CStr(li_col_count)) = uf_filter_values(ls_word)
                End If

                li_start = li_ind + 1
                li_col_count += 1
                li_qoute_count = 0
            End If

            If ls_curr_char = ls_qoute Then
                li_qoute_count += 1
            End If
        Next

        ls_word = as_line.Substring(li_start, as_line.Length - li_start)
        If li_col_count < 0 Then
            adr_row("Document_Type") = uf_filter_values(ls_word)
        Else
            adr_row("col" & CStr(li_col_count)) = uf_filter_values(ls_word)
        End If

    End Sub

    Private Function uf_get_column_count(ByVal as_line As String) As Integer
        Dim ls_comma As String = ","
        Dim ls_qoute As String = """"

        Dim li_ind As Integer
        Dim li_qoute_count As Integer = 0
        Dim ls_curr_char As String
        Dim li_col_count As Integer = 0

        For li_ind = 0 To as_line.Length - 1
            ls_curr_char = as_line(li_ind)

            If ls_curr_char = ls_comma And (li_qoute_count Mod 2) = 0 Then
                li_col_count += 1
                li_qoute_count = 0
            End If

            If ls_curr_char = ls_qoute Then
                li_qoute_count += 1
            End If
        Next

        li_col_count += 1

        Return li_col_count
    End Function

    Private Sub uf_create_table_columns(ByVal ai_total_cols As Integer, ByRef adt_table As DataTable)
        Dim li_index As Integer

        For li_index = 0 To ai_total_cols - 1
            If li_index < 0 Then

                adt_table.Columns.Add("Document_Type")
            Else
                adt_table.Columns.Add("col" & CStr(li_index))
            End If

        Next
    End Sub
    Private Function uf_get_table_count(ByVal as_row As String(), ByRef ai_tbl0_end_ind As Integer) As Integer
        Dim li_ind As Integer
        Dim li_count As Integer = 0

        For li_ind = 0 To as_row.Length - 1
            If as_row(li_ind).Trim = "" Then
                If li_count = 0 Then ai_tbl0_end_ind = li_ind
                li_count += 1
            End If
        Next

        Return li_count
    End Function

    Private Function uf_get_file_content(ByVal as_file_stream As System.IO.Stream) As String
        Dim ls_file_content As String
        Dim lo_stream_reader As System.IO.StreamReader

        lo_stream_reader = New System.IO.StreamReader(as_file_stream)

        ls_file_content = lo_stream_reader.ReadToEnd

        lo_stream_reader.Close()

        Return ls_file_content
    End Function


    'Private Sub cbDocType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDocType.CheckedChanged
    '    If cbDocType.Checked Then
    '        tbDocTypeColNumber.ReadOnly = False
    '        tbDocTypeColNumber.CssClass = "entryfldint"
    '        tbFileNameColNumber.ReadOnly = False
    '        tbFileNameColNumber.CssClass = "entryfldint"
    '    Else
    '        tbDocTypeColNumber.Text = ""
    '        tbDocTypeColNumber.ReadOnly = True
    '        tbDocTypeColNumber.CssClass = "entryfldintdisabled"
    '        tbFileNameColNumber.Text = ""
    '        tbFileNameColNumber.ReadOnly = True
    '        tbFileNameColNumber.CssClass = "entryfldintdisabled"
    '    End If


    'End Sub

    'Private Sub MoveDocuments(ByVal asFile As String, ByVal asDocId As String)
    '    If System.IO.File.Exists(tbLocation.Text.Trim & asFile) Then
    '        System.IO.File.Move(tbLocation.Text.Trim & asFile, DocSession.FileLoc & asDocId & "_1_" & asFile)
    '    End If

    'End Sub

    'Private Function DocumentExists(ByVal asFile As String) As Boolean

    '    If System.IO.File.Exists(tbLocation.Text.Trim & asFile) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Function GetCellByName(ByVal oRow As GridViewRow, ByVal CellName As String) As String
        For Each oCell As DataControlFieldCell In oRow.Cells
            If oCell.ContainingField.ToString = CellName Then
                Return IIf(oCell.Text = "&nbsp;", "", oCell.Text)
            End If
        Next
    End Function

    Private Function getColumnId(ByVal asDocType As String) As DataTable
        Dim lDT As Literal
        Dim loData As New DataTable
        Dim loRow As DataRow
        loData.Columns.Add("DocType", Type.GetType("System.String"))
        loData.Columns.Add("ColSeq", Type.GetType("System.String"))
        loData.Columns.Add("ColId", Type.GetType("System.String"))
        Try
            For Each ri In rptDocIndex.Items
                If ri.ItemType = ListItemType.Item Or ri.ItemType = ListItemType.AlternatingItem Then
                    lDT = CType(ri.FindControl("lDocType"), Literal)
                    If lDT.Text = asDocType Then
                        loRow = loData.NewRow()
                        loRow("DocType") = DirectCast(ri.FindControl("ldoctype"), Literal).Text
                        loRow("ColSeq") = DirectCast(ri.FindControl("lColSeq"), Literal).Text
                        loRow("ColId") = DirectCast(ri.FindControl("lColId"), Literal).Text
                        loData.Rows.Add(loRow)
                    End If
                End If
            Next
            Return loData
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            loData.Dispose()

        End Try

    End Function

    Private Function GenRefNo() As Boolean
        'Return (tbOfficeColumnNumber.Visible AndAlso tbOfficeColumnNumber.Text <> "") OrElse (dlOffice.Visible AndAlso dlOffice.SelectedValue <> "")
    End Function

    Private Sub btImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btImport.Click
        
        Dim objCommand As clsSqlConn

        Dim ltr As DbTran
        Try
            ltr = New DbTran
            objCommand = New clsSqlConn(ltr.pTran)


            Dim lbImported As Boolean = False
            Dim ctr As Integer = 0
            Dim loCls As clsCrdMonitoring
            Dim lsdatereceived As String
            For Each grv As GridViewRow In rptUpload.Rows
                ctr = ctr + 1
                If ctr <> 1 AndAlso Server.HtmlDecode(grv.Cells(0).Text.Trim).Trim <> "" AndAlso Server.HtmlDecode(grv.Cells(3).Text.Trim).Trim <> "" Then

                    loCls = New clsCrdMonitoring
                    loCls.pFileVersion = "1"
                    loCls.pRefNo = Server.HtmlDecode(grv.Cells(2).Text.Trim).Trim

                    loCls.pParentRecordNo = "-1"
                    loCls.pDocId = "0" 'lRefNoData(0)("DocId")
                    'loCls.pRefNo = Master.SetSearchCriteria
                    loCls.pReceivedBy = DocSession.sUserId
                    lsdatereceived = Server.HtmlDecode(grv.Cells(3).Text.Trim).Trim & " " & Server.HtmlDecode(grv.Cells(4).Text.Trim).Trim
                    loCls.pDateTimeReceived = IIf(IsDate(lsdatereceived), lsdatereceived, DateTime.Now.ToString).Trim
                    If Server.HtmlDecode(grv.Cells(6).Text.Trim).Trim = "" Then
                        loCls.pRequestingOfcCode = ""
                        loCls.pOtherOffice = Server.HtmlDecode(grv.Cells(5).Text.Trim).Trim
                    Else
                        loCls.pRequestingOfcCode = Server.HtmlDecode(grv.Cells(6).Text.Trim).Trim
                        loCls.pOtherOffice = ""
                    End If
                    loCls.pDescription = Server.HtmlDecode(grv.Cells(7).Text.Trim).Trim
                    loCls.pMainStatus = IIf(Server.HtmlDecode(grv.Cells(0).Text.Trim).Trim.ToUpper = "DONE", "3", IIf(Server.HtmlDecode(grv.Cells(0).Text.Trim).Trim.ToUpper = "OVERDUE", "10", "1"))
                    loCls.pSortingStatus = IIf(Server.HtmlDecode(grv.Cells(8).Text.Trim).Trim.ToUpper = "DONE", "3", "1")
                    If Not Request.Cookies("docMonDefaultSorter") Is Nothing Then
                        If Request.Cookies("docMonDefaultSorter").Value <> "" Then
                            loCls.pSortedBy = Request.Cookies("docMonDefaultSorter").Value
                        Else
                            loCls.pSortedBy = ""
                        End If
                    Else
                        loCls.pSortedBy = ""
                    End If

                    loCls.pSortedCompleted = IIf(IsDate(Server.HtmlDecode(grv.Cells(10).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(10).Text.Trim).Trim, "01/01/1980")
                    loCls.pSortedReceived = IIf(IsDate(Server.HtmlDecode(grv.Cells(9).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(9).Text.Trim).Trim, "01/01/1980")
                    loCls.pDeliveryStatus = IIf(Server.HtmlDecode(grv.Cells(11).Text.Trim).Trim.ToUpper = "DONE", "3", "1")
                    loCls.pDeliveredBy = IIf(Server.HtmlDecode(grv.Cells(14).Text.Trim).Trim <> "", Server.HtmlDecode(grv.Cells(14).Text.Trim).Trim, "")
                    loCls.pDeliveryReceived = IIf(IsDate(Server.HtmlDecode(grv.Cells(12).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(12).Text.Trim).Trim, "01/01/1980")
                    loCls.pDeliveryCompleted = IIf(IsDate(Server.HtmlDecode(grv.Cells(13).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(13).Text.Trim).Trim, "01/01/1980")
                    loCls.pMailingReceived = IIf(IsDate(Server.HtmlDecode(grv.Cells(17).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(17).Text.Trim).Trim, "01/01/1980")
                    loCls.pMailingCompleted = IIf(IsDate(Server.HtmlDecode(grv.Cells(18).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(18).Text.Trim).Trim, "01/01/1980")
                    loCls.pMailingStatus = IIf(Server.HtmlDecode(grv.Cells(16).Text.Trim).Trim.ToUpper = "DONE", "3", "1")
                    loCls.pDueDate = IIf(IsDate(Server.HtmlDecode(grv.Cells(22).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(22).Text.Trim).Trim, "01/01/1980")
                    loCls.pDuration = IIf(Server.HtmlDecode(grv.Cells(19).Text.Trim).Trim = "", "0", Server.HtmlDecode(grv.Cells(19).Text.Trim).Trim)
                    loCls.pRemarks = Server.HtmlDecode(grv.Cells(24).Text.Trim).Trim
                    loCls.pDateOfLetter = IIf(IsDate(Server.HtmlDecode(grv.Cells(26).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(26).Text.Trim).Trim, "01/01/1980")
                    loCls.pDateReceivedByRecipient = IIf(IsDate(Server.HtmlDecode(grv.Cells(27).Text.Trim).Trim), Server.HtmlDecode(grv.Cells(27).Text.Trim).Trim, "01/01/1980")
                    loCls.pLocation = Server.HtmlDecode(grv.Cells(28).Text.Trim).Trim
                    loCls.pMailedBy = ""
                    loCls.pCreatedBy = DocSession.sUserId
                    loCls.pCreatedDate = DateTime.Now.ToString
                    loCls.pCourierName = ""
                    loCls.pGroupCode = DocSession.sUserGroup
                    If loCls.pDeliveredBy <> "" Then
                        loCls.pPersonalDelivery = "1"
                    Else
                        loCls.pPersonalDelivery = "0"
                    End If
                    loCls.AddMonitoring(objCommand)
                    grv.Cells(0).Text = "Imported"
                    pnlMsg.Update()
                    lbImported = True
                End If
            Next


            If lbImported Then
                ltr.pTran.Commit()
                Master.ShowMessage("Documents has been successfully imported.")
            Else
                Master.ShowMessage("No document has been imported. Please check your import file and try again.")
            End If

            rptUpload.Visible = False
            btUpload.Visible = True
            btImport.Visible = False
            step5.Visible = False

        Catch ex As Exception

            ltr.pTran.Rollback()
            Master.ShowMessage("There's an error while importing the record (" & ex.Message & "). Please try again.")
            'msg.CssClass = "msg_red"
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

  

    Private Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        'imgImport.Visible = False
        btImport.Visible = False
        step5.Visible = False
        'btUpload.Visible = True
        btUpload.Visible = True
        btCancel.Visible = False
        rptUpload.Visible = False
        'msg.Text = ""
    End Sub

    'Private Sub btUploadFiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUploadFiles.Click
    '    'ucUploadFiles1.Visible = True
    '    '  ucUploadFiles1.getFiles()
    'End Sub

    

    
    Private Sub GetOffices()
        'Dim str As String = System.Configuration.ConfigurationManager.AppSettings("master_db")
        'Dim oConn As New SqlConnection(str)

        'Dim objCommand As clsSqlConn
        'Dim adpSecurity As New SqlDataAdapter
        Dim ldata As DataTable
        Dim lrow As DataRow
        Dim oType As DocGroup
        Try
            oType = New DocGroup

            ldata = oType.RetrieveOffice
            If ldata.Rows.Count > 0 Then
                lrow = ldata.NewRow
                lrow("OfficeCode") = ""
                lrow("Description") = ""
                ldata.Rows.InsertAt(lrow, 0)
            End If

            'dlOffice.DataSource = ldata
            'dlOffice.DataValueField = "OfficeCode"
            'dlOffice.DataTextField = "Description"
            'dlOffice.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If Not ldata Is Nothing Then
                ldata.Dispose()
                ldata = Nothing
            End If

        End Try

    End Sub

    Private Function GetOfficeCode(ByVal asOffice As String) As String

        Dim lsOfficeCode As String
        Try
            lsOfficeCode = ""
            'For i = 0 To dlOffice.Items.Count - 1
            '    If dlOffice.Items(i).Text.ToLower.Trim = asOffice.ToLower.Trim Then
            '        lsOfficeCode = dlOffice.Items(i).Value

            '    End If
            'Next

            Return lsOfficeCode

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try

    End Function

    Private Sub btUploadFileName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btUploadFileName.Click
        If uploadFileName.HasFile Then
            Dim linfo As New System.IO.FileInfo(uploadFileName.PostedFile.FileName)
            If uploadFileName.PostedFile.ContentLength > DocSession.MaxFileSize Then
                Master.ShowMessage("** File size too big. Only a maximum of " + CStr(Math.Floor(DocSession.MaxFileSize / 1000000)) + "MB is allowed.")
                Exit Sub
            End If

        End If

        Dim ls_fileextension As String = System.IO.Path.GetExtension(uploadFileName.FileName).ToLower()
        If Not ls_fileextension = ".csv" Then
            Master.ShowMessage("Invalid file type. Only CSV is allowed.")
            Exit Sub
        End If
        Try

            lImportFileName.Text = uploadFileName.FileName
            Using ldata As DataTable = uf_read_csv_dtl()
                'If ldata.Rows.Count > 200 Then
                'msg.Text = "Please import not more than 200 records to avoid performance issue."
                If ldata.Rows.Count = 0 Then
                    Master.ShowMessage("Import file is empty. Please load another file.")

                    'imgImport.Visible = False
                    btImport.Visible = False
                    step5.Visible = False
                    'btUpload.Visible = True
                    btUpload.Visible = True
                    btCancel.Visible = False
                ElseIf ldata.Columns.Count > 1 Then
                    Master.ShowMessage("File should only contain 1 column that contains the file name to be imported.")

                    'imgImport.Visible = False
                    btImport.Visible = False
                    step5.Visible = False
                    'btUpload.Visible = True
                    btUpload.Visible = True
                    btCancel.Visible = False
                Else


                    'ldata.DefaultView.Sort = "col0 asc"



                End If
                Dim loOffice As New DocOffice
                'Dim lsOfc As String
                Dim lodata As DataTable = loOffice.RetrieveEquivalentOfficeCode("")
                Dim drow() As DataRow
                For Each drows As DataRow In ldata.Rows
                    drow = lodata.Select("OfficeCode = '" & drows(5) & "' or Description = '" & drows(5) & "'")
                    drows(6) = drow(0)
                Next

                lRecordCount.Text = "No of rows to import: " & ldata.Rows.Count.ToString
                rptUpload.Visible = True
               

                rptUpload.DataSource = ldata

                rptUpload.DataBind()
                'rptUpload.Sort("col0", SortDirection.Ascending)
                'cbxSelectAll.Checked = True

                btImport.Visible = True
                btUpload.Visible = False
                btCancel.Visible = True
                step5.Visible = True
                'pnlDocType.Update()
                'End If
                pnlMsg.Update()
                'pnlImp.Update()

            End Using
        Catch ex As Exception
            btUpload.Visible = True
            step5.Visible = False
            btCancel.Visible = False

        End Try
    End Sub

    
End Class